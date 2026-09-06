# 08. Notification System (Strategy, Factory, Decorator)

## 📌 Context
The Notification System tests your ability to abstract third-party integrations (Twilio, SendGrid), allow the system to dynamically select the correct channel, and add cross-cutting concerns (like retries or logging) without violating the Open/Closed Principle.

---

## 1. Core Abstractions & The Strategy Pattern
Define a generic notification channel. Each concrete implementation (Email, SMS) acts as a Strategy.

```csharp
public record NotificationMessage(string UserId, string Content, string Subject = "");

// The Strategy Interface
public interface INotificationChannel
{
    bool Supports(NotificationType type);
    Task SendAsync(NotificationMessage message, CancellationToken ct);
}

public enum NotificationType { Email, Sms, Push }

// Concrete Strategy 1
public class EmailNotificationChannel : INotificationChannel
{
    public bool Supports(NotificationType type) => type == NotificationType.Email;

    public async Task SendAsync(NotificationMessage message, CancellationToken ct)
    {
        // Call SendGrid SDK...
        Console.WriteLine($"Sending Email to {message.UserId}: {message.Content}");
        await Task.Delay(100, ct); // Simulate I/O
    }
}

// Concrete Strategy 2
public class SmsNotificationChannel : INotificationChannel
{
    public bool Supports(NotificationType type) => type == NotificationType.Sms;

    public async Task SendAsync(NotificationMessage message, CancellationToken ct)
    {
        // Call Twilio SDK...
        Console.WriteLine($"Sending SMS to {message.UserId}: {message.Content}");
        await Task.Delay(50, ct);
    }
}
```

---

## 2. The Factory (Channel Resolution)
How do we grab the right channel? We use `IEnumerable<INotificationChannel>` injected by ASP.NET Core DI to resolve all registered strategies.

```csharp
public interface INotificationFactory
{
    INotificationChannel GetChannel(NotificationType type);
}

public class NotificationFactory : INotificationFactory
{
    private readonly IEnumerable<INotificationChannel> _channels;

    // DI automatically passes ALL registered classes that implement INotificationChannel
    public NotificationFactory(IEnumerable<INotificationChannel> channels)
    {
        _channels = channels;
    }

    public INotificationChannel GetChannel(NotificationType type)
    {
        var channel = _channels.FirstOrDefault(c => c.Supports(type));
        
        if (channel == null)
            throw new NotSupportedException($"Notification type {type} is not supported.");
            
        return channel;
    }
}
```

---

## 3. The Decorator Pattern (Retries & Logging)
Interviewers love this: "How do you add retries to the Email sender without modifying the `EmailNotificationChannel` class?"
*Answer:* The Decorator pattern. We wrap the `INotificationChannel`. (Note: In production C#, you'd often just use `Polly` inside `HttpClientFactory`, but the interviewer wants to see the GoF pattern).

```csharp
public class RetryNotificationDecorator : INotificationChannel
{
    private readonly INotificationChannel _innerChannel;
    private readonly ILogger<RetryNotificationDecorator> _logger;
    private readonly int _maxRetries = 3;

    public RetryNotificationDecorator(INotificationChannel innerChannel, ILogger<RetryNotificationDecorator> logger)
    {
        _innerChannel = innerChannel;
        _logger = logger;
    }

    public bool Supports(NotificationType type) => _innerChannel.Supports(type);

    public async Task SendAsync(NotificationMessage message, CancellationToken ct)
    {
        for (int i = 1; i <= _maxRetries; i++)
        {
            try
            {
                await _innerChannel.SendAsync(message, ct);
                return; // Success!
            }
            catch (Exception ex) when (i < _maxRetries)
            {
                _logger.LogWarning(ex, "Attempt {Attempt} failed. Retrying...", i);
                await Task.Delay(TimeSpan.FromMilliseconds(500 * i), ct); // Exponential backoff
            }
        }
        
        throw new Exception("Notification failed after max retries.");
    }
}
```

---

## 4. The Orchestrator / Service
The final service that ties it all together cleanly.

```csharp
public class NotificationService
{
    private readonly INotificationFactory _factory;

    public NotificationService(INotificationFactory factory)
    {
        _factory = factory;
    }

    public async Task SendNotificationAsync(NotificationMessage message, NotificationType type, CancellationToken ct)
    {
        // 1. Resolve strategy via Factory
        var channel = _factory.GetChannel(type);
        
        // 2. Execute (which might be secretly wrapped in a RetryDecorator via DI!)
        await channel.SendAsync(message, ct);
    }
}
```
### DI Registration (Using Scrutor or native DI)
```csharp
// Register all channels
services.AddTransient<INotificationChannel, EmailNotificationChannel>();
services.AddTransient<INotificationChannel, SmsNotificationChannel>();
services.AddSingleton<INotificationFactory, NotificationFactory>();

// Registering a decorator natively in .NET 8+:
// services.Decorate<INotificationChannel, RetryNotificationDecorator>();
```
---

## 🗣️ Interviewer Discussion & Tradeoffs

**Interviewer:** *"Why is `NotificationFactory` better than just injecting all `INotificationChannel` directly into `NotificationService` and writing an `if/else` block?"*
**You:** "It removes responsibility from the `NotificationService`. The service shouldn't care *how* a channel is selected; it just wants to send a message. The Factory encapsulates the creation/resolution logic. Furthermore, if we add a new `WhatsAppNotificationChannel`, we just register it in DI. Neither the Service nor the Factory needs to be modified (OCP)."

**Interviewer:** *"Why use the Decorator Pattern for retries instead of just putting the `for` loop inside `EmailNotificationChannel`?"*
**You:** "Separation of Concerns (SRP). The `EmailNotificationChannel` should only care about talking to the Email API. It shouldn't care about retry policies, logging, or metrics. By wrapping it in a `RetryNotificationDecorator`, we can apply the exact same retry logic to the SMS channel and the Push channel without duplicating code."

**Interviewer:** *"How would you implement Rate Limiting per user in this design?"*
**You:** "I would add another Decorator! I could create a `RateLimitNotificationDecorator`. Because decorators implement the exact same `INotificationChannel` interface, I can chain them: `RateLimiter` wraps `Retryer` wraps `EmailChannel`. The Rate Limiter checks a Redis cache for the User ID; if they exceed the limit, it throws an exception or queues the message, never even reaching the Email Channel."

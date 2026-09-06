# 02. Generic Message Processor (Azure + C# Generics)

## 📌 Context
This problem tests your understanding of **Event-Driven Architecture**, polymorphism, generics, and Dependency Injection in .NET. It's heavily requested in senior Microsoft/Azure ecosystem interviews.

**The Goal:** Receive messages from an Azure Service Bus, dynamically resolve the correct handler without writing massive `switch/if-else` blocks, and process it resiliently.

---

## 1. Defining the Core Interfaces

Avoid tying your domain events directly to Service Bus SDK types. Use clean marker interfaces.

```csharp
public interface IMessage 
{
    Guid MessageId { get; }
    DateTimeOffset CreatedAt { get; }
}

public interface IMessageHandler<in TMessage> where TMessage : IMessage
{
    Task HandleAsync(TMessage message, CancellationToken cancellationToken);
}
```

---

## 2. Concrete Messages and Handlers

```csharp
public record OrderSubmittedMessage(Guid MessageId, DateTimeOffset CreatedAt, Guid OrderId, decimal Amount) : IMessage;

public class OrderSubmittedHandler : IMessageHandler<OrderSubmittedMessage>
{
    private readonly ILogger<OrderSubmittedHandler> _logger;
    private readonly HttpClient _externalApiClient;

    public OrderSubmittedHandler(ILogger<OrderSubmittedHandler> logger, HttpClient externalApiClient)
    {
        _logger = logger;
        _externalApiClient = externalApiClient;
    }

    public async Task HandleAsync(OrderSubmittedMessage message, CancellationToken ct)
    {
        _logger.LogInformation("Processing Order {OrderId}", message.OrderId);
        
        // Example: Call an external API with HttpClient
        var response = await _externalApiClient.PostAsJsonAsync("/api/validate", message, ct);
        response.EnsureSuccessStatusCode();
    }
}
```

---

## 3. The Message Dispatcher (The Factory)

How do we take a raw JSON string from Service Bus and route it to OrderSubmittedHandler? We use reflection and IServiceProvider.

```csharp
public interface IMessageDispatcher
{
    Task DispatchAsync(string messageType, string payload, CancellationToken ct);
}

public class MessageDispatcher : IMessageDispatcher
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<MessageDispatcher> _logger;
    
    // Hardcoded for demo, normally use a registry or Assembly scanning
    private static readonly Dictionary<string, Type> _messageTypes = new()
    {
        { "OrderSubmitted", typeof(OrderSubmittedMessage) }
    };

    public MessageDispatcher(IServiceProvider serviceProvider, ILogger<MessageDispatcher> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task DispatchAsync(string messageType, string payload, CancellationToken ct)
    {
        if (!_messageTypes.TryGetValue(messageType, out var type))
        {
            _logger.LogWarning("Unknown message type: {MessageType}", messageType);
            return;
        }

        // 1. Deserialize to the concrete type
        var message = JsonSerializer.Deserialize(payload, type) as IMessage;
        if (message == null) throw new InvalidOperationException("Deserialization failed.");

        // 2. Resolve the handler generic type: IMessageHandler<T>
        var handlerType = typeof(IMessageHandler<>).MakeGenericType(type);

        // 3. Create a scope (Best practice for resolving scoped dependencies like DbContext)
        using var scope = _serviceProvider.CreateScope();
        var handler = scope.ServiceProvider.GetRequiredService(handlerType);

        // 4. Invoke HandleAsync via reflection
        var method = handlerType.GetMethod("HandleAsync");
        
        // Await the task returned by the handler
        var task = (Task)method!.Invoke(handler, new object[] { message, ct })!;
        await task;
    }
}
```
---

## 🗣️ Interviewer Discussion & Tradeoffs

**Interviewer:** *"Why go through the trouble of creating a Factory and using Reflection (`GetMethod("HandleAsync").Invoke`) instead of a simple `switch` statement?"*
**You:** "Because of the **Open/Closed Principle (OCP)**. If we use a `switch` statement, every time we add a new message type (e.g., `OrderCancelled`), we have to modify the dispatcher class. With this Factory and DI approach, adding a new message type requires zero changes to the dispatcher. We just create the new `OrderCancelledHandler` and register it in DI. The system is closed for modification but open for extension."

**Interviewer:** *"What happens if the `ExternalApiClient` times out and throws an exception during `HandleAsync`?"*
**You:** "The exception bubbles up to the Azure Function runtime. By default, Azure Service Bus will not `Complete` the message. It will unlock it, making it available for retry. After the `MaxDeliveryCount` is reached (usually 10), the message is automatically moved to the **Dead Letter Queue (DLQ)**, preventing poison messages from infinitely crashing our system."

**Interviewer:** *"Why did you use `using var scope = _serviceProvider.CreateScope();`?"*
**You:** "Azure Service Bus triggers are background singletons. If we inject a `DbContext` directly into a Singleton, it becomes a *Captive Dependency* and lives forever, eventually causing memory leaks or DB connection exhaustion. By creating a scope per message, we ensure that scoped services (like `DbContext` or `HttpClient`) are properly created and disposed of when the message processing finishes."

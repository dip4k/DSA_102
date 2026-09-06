# 13. Logger (Chain of Responsibility + Decorator + Concurrency)

## 📌 Context
Designing a Logger tests your understanding of the **Chain of Responsibility Pattern** (routing log levels), the **Decorator Pattern** (adding timestamps/formatting), and **Concurrency** (writing to a file from multiple threads without corrupting it).

---

## 1. The Chain of Responsibility
Each logger decides if it should process the message, and then optionally passes it to the next logger.

```csharp
public enum LogLevel { Debug, Info, Warning, Error }

public abstract class Logger
{
    protected LogLevel Level { get; }
    private Logger? _nextLogger;

    protected Logger(LogLevel level)
    {
        Level = level;
    }

    public void SetNext(Logger nextLogger)
    {
        _nextLogger = nextLogger;
    }

    public void Log(LogLevel level, string message)
    {
        // 1. Process if applicable
        if (level >= Level)
        {
            WriteMessage(message);
        }

        // 2. Pass to next in chain
        _nextLogger?.Log(level, message);
    }

    protected abstract void WriteMessage(string message);
}

// Concrete Handlers
public class ConsoleLogger : Logger
{
    public ConsoleLogger(LogLevel level) : base(level) { }
    protected override void WriteMessage(string message) => Console.WriteLine($"[CONSOLE]: {message}");
}

public class FileLogger : Logger
{
    private readonly string _filePath;
    private static readonly object _lock = new object();

    public FileLogger(LogLevel level, string filePath) : base(level) 
    { 
        _filePath = filePath;
    }

    protected override void WriteMessage(string message)
    {
        // Thread safety is critical here!
        lock (_lock)
        {
            File.AppendAllText(_filePath, $"[FILE]: {message}\n");
        }
    }
}
```

---

## 2. The Decorator Pattern (Formatting)
Instead of hardcoding timestamps inside the logger, we decorate it.

```csharp
public class TimestampLoggerDecorator : Logger
{
    private readonly Logger _innerLogger;

    public TimestampLoggerDecorator(Logger innerLogger) : base(LogLevel.Debug) // Catch all
    {
        _innerLogger = innerLogger;
    }

    protected override void WriteMessage(string message)
    {
        var formattedMessage = $"{DateTimeOffset.UtcNow:O} - {message}";
        _innerLogger.Log(LogLevel.Debug, formattedMessage); // Forward the modified message
    }
}
```

---

## 🗣️ Interviewer Discussion & Tradeoffs

**Interviewer:** *"Why use the Chain of Responsibility instead of an `if/else` block inside a single `Log()` method?"*
**You:** "The Open/Closed Principle (OCP). If we want to add an `ElasticSearchLogger` or a `SlackAlertLogger` for Errors, an `if/else` block would force us to modify the core logging class. With the Chain of Responsibility, we simply instantiate the new logger and link it into the chain (`consoleLogger.SetNext(slackLogger)`). The core logic remains untouched."

**Interviewer:** *"Your `FileLogger` uses `lock (_lock)`. If this is a high-traffic web application, what's the problem with this?"*
**You:** "Synchronous blocking. If 1000 requests per second try to write a log, 999 threads are blocked waiting for file I/O, which will quickly exhaust the ASP.NET Core Thread Pool and crash the server. This is called **Synchronous I/O Blocking**."

**Interviewer:** *"How do you fix that blocking issue?"*
**You:** "By implementing **Asynchronous Background Logging**. Instead of writing to the file directly in the web request thread, the `FileLogger` should push the log message into an in-memory `Channel<string>` (or `ConcurrentQueue`). A dedicated `BackgroundService` reads from that channel and writes to the file in batches using `await File.AppendAllLinesAsync()`. This completely unblocks the web requests."
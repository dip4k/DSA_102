# 14. Pub/Sub Message Queue (Observer + Concurrency)

## 📌 Context
Building an in-memory Pub/Sub system tests your ability to implement the **Observer Pattern** safely in a **multi-threaded environment**.

---

## 1. Domain Modeling: Topics and Subscribers

```csharp
public interface ISubscriber
{
    string Id { get; }
    Task ReceiveAsync(Message message);
}

public record Message(string Payload, DateTimeOffset Timestamp);

public class Topic
{
    public string Name { get; }
    
    // Thread-safe dictionary for subscribers
    private readonly ConcurrentDictionary<string, ISubscriber> _subscribers = new();

    public Topic(string name)
    {
        Name = name;
    }

    public void Subscribe(ISubscriber subscriber)
    {
        _subscribers.TryAdd(subscriber.Id, subscriber);
    }

    public void Unsubscribe(string subscriberId)
    {
        _subscribers.TryRemove(subscriberId, out _);
    }

    public async Task PublishAsync(Message message)
    {
        // Broadcast to all subscribers concurrently
        var tasks = _subscribers.Values.Select(sub => TrySendAsync(sub, message));
        await Task.WhenAll(tasks);
    }

    private async Task TrySendAsync(ISubscriber subscriber, Message message)
    {
        try
        {
            await subscriber.ReceiveAsync(message);
        }
        catch (Exception ex)
        {
            // Do not let one failing subscriber crash the publisher or other subscribers
            Console.WriteLine($"Subscriber {subscriber.Id} failed: {ex.Message}");
        }
    }
}
```

---

## 2. The Message Broker
The Broker orchestrates multiple topics.

```csharp
public class MessageBroker
{
    private readonly ConcurrentDictionary<string, Topic> _topics = new();

    public void CreateTopic(string topicName)
    {
        _topics.TryAdd(topicName, new Topic(topicName));
    }

    public void Subscribe(string topicName, ISubscriber subscriber)
    {
        var topic = _topics.GetOrAdd(topicName, name => new Topic(name));
        topic.Subscribe(subscriber);
    }

    public async Task PublishAsync(string topicName, Message message)
    {
        if (_topics.TryGetValue(topicName, out var topic))
        {
            await topic.PublishAsync(message);
        }
    }
}
```

---

## 🗣️ Interviewer Discussion & Tradeoffs

**Interviewer:** *"If a subscriber takes 10 seconds to process a message, does it block the `PublishAsync` method?"*
**You:** "Yes, currently it does. Because `PublishAsync` uses `Task.WhenAll`, the Publisher must wait for all subscribers to finish processing before the method returns. In a true Pub/Sub system, publishing should be fire-and-forget from the publisher's perspective."

**Interviewer:** *"How do we fix that and decouple the publisher from the subscriber's execution time?"*
**You:** "We need to introduce a **Queue** per subscriber. Instead of the Topic calling `await subscriber.ReceiveAsync()`, the Topic pushes the message into a `Channel<Message>` (or `ConcurrentQueue`) assigned to that subscriber. Each subscriber has a dedicated background thread reading from its own queue. This guarantees that publishing is O(1) and never blocks."

**Interviewer:** *"What happens if the publisher sends 10,000 messages/sec, but the subscriber can only process 100 messages/sec?"*
**You:** "This is the classic **Slow Consumer Problem**. The queue will grow infinitely and cause an Out-Of-Memory (OOM) crash. We can fix this by:
1. **Bounded Queues:** Limiting the queue size (e.g., 1000 max).
2. **Backpressure:** If the queue is full, the publisher is forced to `await` (blocking the publisher until space frees up).
3. **Drop Policies:** If the queue is full, drop the oldest message, or drop the newest message, depending on the business requirements."
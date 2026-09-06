# 09. Rate Limiter (Strategy + Concurrency)

## 📌 Context
Rate Limiting tests your understanding of **Algorithms (Strategy Pattern)** and **Thread Safety**. You must be able to switch between Token Bucket, Sliding Window, or Fixed Window based on requirements.

---

## 1. The Strategy Interface
Extract the algorithm behind an interface so the API middleware doesn't care which one is used.

```csharp
public interface IRateLimitStrategy
{
    bool IsAllowed(string clientId);
}
```

---

## 2. Token Bucket Implementation (In-Memory, Highly Concurrent)
The Token Bucket algorithm adds tokens at a fixed rate. Requests cost 1 token. 
*Challenge:* Updating the token count and the last refill time must be atomic.

```csharp
public class TokenBucketRateLimiter : IRateLimitStrategy
{
    private readonly int _maxBucketSize;
    private readonly int _refillRatePerSecond;
    
    // Using ConcurrentDictionary to track multiple clients
    private readonly ConcurrentDictionary<string, TokenBucket> _clientBuckets = new();

    public TokenBucketRateLimiter(int maxBucketSize, int refillRatePerSecond)
    {
        _maxBucketSize = maxBucketSize;
        _refillRatePerSecond = refillRatePerSecond;
    }

    public bool IsAllowed(string clientId)
    {
        var bucket = _clientBuckets.GetOrAdd(clientId, _ => new TokenBucket(_maxBucketSize));
        return bucket.TryConsume(_refillRatePerSecond, _maxBucketSize);
    }
}

// Inner class representing state for ONE client
internal class TokenBucket
{
    private double _currentTokens;
    private long _lastRefillTicks; // Use ticks for Interlocked operations

    public TokenBucket(int initialTokens)
    {
        _currentTokens = initialTokens;
        _lastRefillTicks = DateTimeOffset.UtcNow.Ticks;
    }

    public bool TryConsume(int refillRatePerSecond, int maxTokens)
    {
        // 1. We must lock because we are calculating time elapsed AND deducting a token.
        // Interlocked is not enough here because we need atomic updates across TWO variables.
        lock (this)
        {
            Refill(refillRatePerSecond, maxTokens);

            if (_currentTokens >= 1)
            {
                _currentTokens -= 1;
                return true; // Allowed
            }

            return false; // Rate limited
        }
    }

    private void Refill(int refillRatePerSecond, int maxTokens)
    {
        var now = DateTimeOffset.UtcNow;
        var elapsedSeconds = (now - new DateTimeOffset(_lastRefillTicks, TimeSpan.Zero)).TotalSeconds;

        if (elapsedSeconds > 0)
        {
            var tokensToAdd = elapsedSeconds * refillRatePerSecond;
            _currentTokens = Math.Min(_currentTokens + tokensToAdd, maxTokens);
            _lastRefillTicks = now.Ticks;
        }
    }
}
```

---

## 🗣️ Interviewer Discussion & Tradeoffs

**Interviewer:** *"Why did you use `lock(this)` inside `TokenBucket.TryConsume`? Isn't that slow?"*
**You:** "Since the `TokenBucket` instance is isolated per `clientId` (thanks to the `ConcurrentDictionary`), the lock only blocks requests coming from the *exact same user* at the *exact same microsecond*. It does not block other users. We must lock it because we read the time, calculate tokens, and deduct a token. If we don't lock, two concurrent requests from the same user could read 1 available token and both succeed, bypassing the limit."

**Interviewer:** *"What is the main drawback of the Fixed Window algorithm compared to this Token Bucket?"*
**You:** "The Fixed Window (e.g., 100 requests per minute) suffers from the **Boundary Spike Problem**. A user could send 100 requests at 12:00:59 and another 100 requests at 12:01:01. The system receives 200 requests in 2 seconds, potentially overwhelming the backend, while technically adhering to the '100 per minute' rule. Token Bucket smooths out bursts because tokens trickle in over time."

**Interviewer:** *"How would you scale this to a distributed environment with 10 Web API servers?"*
**You:** "An in-memory `ConcurrentDictionary` rate limits *per server*. If a user hits Server A and Server B via a Load Balancer, they get double the limit. To fix this, I would move the state to **Redis**. Specifically, I would use a **Redis Lua Script** to execute the Token Bucket calculation. Lua scripts run atomically on the Redis server, guaranteeing thread safety across all 10 Web APIs without needing distributed locks."
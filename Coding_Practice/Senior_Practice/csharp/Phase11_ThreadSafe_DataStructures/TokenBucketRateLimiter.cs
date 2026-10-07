namespace SeniorPractice.Phase11;

using System.Diagnostics;

/// <summary>
/// High-Throughput Token Bucket Rate Limiter with lazy replenishment.
/// Uses lock-free time calculations or minimal monitor locking for replenishment.
/// Capacity: Maximum burst allowance.
/// RefillRatePerSecond: Tokens generated per second.
/// </summary>
public class TokenBucketRateLimiter
{
    private readonly double _capacity;
    private readonly double _refillRatePerSecond;
    private double _tokens;
    private long _lastRefillTimestampTicks;
    private readonly object _lock = new();

    public double Capacity => _capacity;
    public double RefillRatePerSecond => _refillRatePerSecond;

    public TokenBucketRateLimiter(double capacity, double refillRatePerSecond)
    {
        if (capacity <= 0)
            throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be positive.");
        if (refillRatePerSecond <= 0)
            throw new ArgumentOutOfRangeException(nameof(refillRatePerSecond), "Refill rate must be positive.");

        _capacity = capacity;
        _refillRatePerSecond = refillRatePerSecond;
        _tokens = capacity; // Start full
        _lastRefillTimestampTicks = Stopwatch.GetTimestamp();
    }

    public bool TryConsume(double tokens = 1.0)
    {
        lock (_lock)
        {
            Refill();

            if (_tokens >= tokens)
            {
                _tokens -= tokens;
                return true;
            }

            return false;
        }
    }

    private void Refill()
    {
        long nowTicks = Stopwatch.GetTimestamp();
        double elapsedSeconds = (double)(nowTicks - _lastRefillTimestampTicks) / Stopwatch.Frequency;

        if (elapsedSeconds > 0)
        {
            double newTokens = elapsedSeconds * _refillRatePerSecond;
            _tokens = Math.Min(_capacity, _tokens + newTokens);
            _lastRefillTimestampTicks = nowTicks;
        }
    }
}

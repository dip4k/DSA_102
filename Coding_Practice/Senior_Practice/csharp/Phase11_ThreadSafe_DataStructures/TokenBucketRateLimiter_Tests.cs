namespace SeniorPractice.Phase11.Tests;

public class TokenBucketRateLimiter_Tests
{
    [Fact]
    public void RateLimiter_ShouldAllowBurstUpToCapacity_ThenThrottle()
    {
        var limiter = new TokenBucketRateLimiter(capacity: 3, refillRatePerSecond: 10);

        // Immediate burst of 3 should succeed
        Assert.True(limiter.TryConsume(1));
        Assert.True(limiter.TryConsume(1));
        Assert.True(limiter.TryConsume(1));

        // 4th immediate consume should be throttled
        Assert.False(limiter.TryConsume(1));

        // Wait a fraction of a second to refill
        Thread.Sleep(150); // refills ~1.5 tokens
        Assert.True(limiter.TryConsume(1));
    }
}

namespace LLDPractice.Problem07_CustomCache.Tests;

public class CustomCacheTests
{
    [Fact]
    public void BasicOperations_ShouldPutAndGetCorrectly()
    {
        using var cache = new LruCache<int, string>(3);

        cache.Put(1, "one");
        cache.Put(2, "two");
        cache.Put(3, "three");

        Assert.Equal(3, cache.Count);
        Assert.True(cache.TryGet(1, out var val1));
        Assert.Equal("one", val1);
        Assert.True(cache.TryGet(2, out var val2));
        Assert.Equal("two", val2);
        Assert.True(cache.TryGet(3, out var val3));
        Assert.Equal("three", val3);
    }

    [Fact]
    public void CapacityLimit_ShouldEvictLeastRecentlyUsed()
    {
        using var cache = new LruCache<int, string>(2);

        cache.Put(1, "one");
        cache.Put(2, "two");

        // Access 1 so 2 becomes LRU
        cache.TryGet(1, out _);

        // Put 3, should evict 2
        cache.Put(3, "three");

        Assert.True(cache.TryGet(1, out _));
        Assert.False(cache.TryGet(2, out _)); // Evicted
        Assert.True(cache.TryGet(3, out _));
        Assert.Equal(2, cache.Count);
    }

    [Fact]
    public void UpdateExistingKey_ShouldUpdateValueAndRetainCapacity()
    {
        using var cache = new LruCache<string, int>(2);

        cache.Put("alpha", 100);
        cache.Put("beta", 200);
        cache.Put("alpha", 999);

        Assert.Equal(2, cache.Count);
        Assert.True(cache.TryGet("alpha", out var val));
        Assert.Equal(999, val);
    }

    [Fact]
    public void Remove_ShouldDeleteEntry()
    {
        using var cache = new LruCache<int, int>(2);

        cache.Put(1, 10);
        Assert.True(cache.Remove(1));
        Assert.False(cache.TryGet(1, out _));
        Assert.Equal(0, cache.Count);
        Assert.False(cache.Remove(999));
    }

    [Fact]
    public async Task ConcurrentAccess_MultipleReadersAndWriters_ShouldMaintainConsistency()
    {
        using var cache = new LruCache<int, int>(50);
        const int threadCount = 8;
        const int opsPerThread = 500;
        var tasks = new Task[threadCount];

        for (int t = 0; t < threadCount; t++)
        {
            int threadId = t;
            tasks[t] = Task.Run(() =>
            {
                var rand = new Random(threadId * 100);
                for (int i = 0; i < opsPerThread; i++)
                {
                    int key = rand.Next(1, 100);
                    if (rand.Next(0, 2) == 0)
                    {
                        cache.Put(key, threadId * 1000 + i);
                    }
                    else
                    {
                        cache.TryGet(key, out _);
                    }
                }
            });
        }

        await Task.WhenAll(tasks);

        // Invariant check: Cache count must never exceed capacity
        Assert.InRange(cache.Count, 0, 50);
    }
}

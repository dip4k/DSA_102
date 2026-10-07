namespace SeniorPractice.Phase11.Tests;

public class ThreadSafeLruCache_Tests
{
    [Fact]
    public void BasicOperations_ShouldRespectLruOrder()
    {
        using var cache = new ThreadSafeLruCache<int, string>(2);
        cache.Put(1, "one");
        cache.Put(2, "two");
        Assert.True(cache.TryGet(1, out var val1));
        Assert.Equal("one", val1);

        cache.Put(3, "three"); // evicts key 2
        Assert.False(cache.TryGet(2, out _));
        Assert.True(cache.TryGet(3, out var val3));
        Assert.Equal("three", val3);
    }

    [Fact]
    public void ConcurrentPutsAndGets_ShouldMaintainIntegrityWithoutDeadlocks()
    {
        using var cache = new ThreadSafeLruCache<int, int>(50);
        const int threadCount = 8;
        const int itemsPerThread = 200;

        Parallel.For(0, threadCount, t =>
        {
            for (int i = 0; i < itemsPerThread; i++)
            {
                int key = (t * 100) + (i % 60);
                cache.Put(key, i);
                cache.TryGet(key, out _);
            }
        });

        Assert.True(cache.Count <= 50);
    }
}

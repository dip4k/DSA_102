namespace SeniorPractice.Phase11.Tests;

public class BoundedBlockingQueue_Tests
{
    [Fact]
    public async Task ProducerConsumer_ShouldTransferAllItemsInFifoOrder()
    {
        var queue = new BoundedBlockingQueue<int>(5);
        const int totalItems = 100;
        var received = new List<int>();

        var producer = Task.Run(() =>
        {
            for (int i = 0; i < totalItems; i++)
            {
                queue.Enqueue(i);
            }
        });

        var consumer = Task.Run(() =>
        {
            for (int i = 0; i < totalItems; i++)
            {
                received.Add(queue.Dequeue());
            }
        });

        await Task.WhenAll(producer, consumer);

        Assert.Equal(totalItems, received.Count);
        for (int i = 0; i < totalItems; i++)
        {
            Assert.Equal(i, received[i]);
        }
        Assert.Equal(0, queue.Size());
    }
}

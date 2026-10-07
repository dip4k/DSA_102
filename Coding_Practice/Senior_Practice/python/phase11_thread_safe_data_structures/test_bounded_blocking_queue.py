import unittest
import threading
from phase11_thread_safe_data_structures.bounded_blocking_queue import BoundedBlockingQueue

class TestBoundedBlockingQueue(unittest.TestCase):
    def test_producer_consumer(self):
        q = BoundedBlockingQueue[int](4)
        items_count = 50
        produced = []
        consumed = []

        def producer():
            for i in range(items_count):
                q.enqueue(i)
                produced.append(i)

        def consumer():
            for _ in range(items_count):
                item = q.dequeue()
                consumed.append(item)

        t1 = threading.Thread(target=producer)
        t2 = threading.Thread(target=consumer)
        t1.start()
        t2.start()
        t1.join()
        t2.join()

        self.assertEqual(consumed, list(range(items_count)))
        self.assertEqual(q.size(), 0)

if __name__ == "__main__":
    unittest.main()

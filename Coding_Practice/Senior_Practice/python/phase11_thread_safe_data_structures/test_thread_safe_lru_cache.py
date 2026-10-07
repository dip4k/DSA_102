import unittest
import threading
from phase11_thread_safe_data_structures.thread_safe_lru_cache import ThreadSafeLruCache

class TestThreadSafeLruCache(unittest.TestCase):
    def test_basic_lru(self):
        cache = ThreadSafeLruCache[int, str](2)
        cache.put(1, "one")
        cache.put(2, "two")
        self.assertEqual(cache.get(1), "one")
        cache.put(3, "three")
        self.assertIsNone(cache.get(2))
        self.assertEqual(cache.get(3), "three")

    def test_concurrent_access(self):
        cache = ThreadSafeLruCache[int, int](30)
        def worker(w_id: int):
            for i in range(100):
                k = (w_id * 50) + (i % 40)
                cache.put(k, i)
                cache.get(k)

        threads = [threading.Thread(target=worker, args=(t,)) for t in range(4)]
        for t in threads: t.start()
        for t in threads: t.join()

        self.assertLessEqual(cache.count, 30)

if __name__ == "__main__":
    unittest.main()

"""Unit and concurrent tests for Python LRU Cache."""

import random
import threading
import unittest
from Coding_Practice.LLD_Practice.python.problem07_custom_cache.custom_cache import LruCache


class TestLruCache(unittest.TestCase):
    def test_basic_operations(self):
        cache: LruCache[int, str] = LruCache(3)
        cache.put(1, "one")
        cache.put(2, "two")
        cache.put(3, "three")

        self.assertEqual(cache.count, 3)
        self.assertEqual(cache.get(1), "one")
        self.assertEqual(cache.get(2), "two")
        self.assertEqual(cache.get(3), "three")

    def test_capacity_eviction(self):
        cache: LruCache[int, str] = LruCache(2)
        cache.put(1, "one")
        cache.put(2, "two")

        # Access 1 to make 2 LRU
        self.assertEqual(cache.get(1), "one")

        # Put 3 -> evicts 2
        cache.put(3, "three")

        self.assertEqual(cache.get(1), "one")
        self.assertIsNone(cache.get(2))
        self.assertEqual(cache.get(3), "three")
        self.assertEqual(cache.count, 2)

    def test_update_existing_key(self):
        cache: LruCache[str, int] = LruCache(2)
        cache.put("alpha", 100)
        cache.put("beta", 200)
        cache.put("alpha", 999)

        self.assertEqual(cache.count, 2)
        self.assertEqual(cache.get("alpha"), 999)

    def test_remove(self):
        cache: LruCache[int, int] = LruCache(2)
        cache.put(1, 10)
        self.assertTrue(cache.remove(1))
        self.assertIsNone(cache.get(1))
        self.assertEqual(cache.count, 0)
        self.assertFalse(cache.remove(999))

    def test_concurrent_access(self):
        cache: LruCache[int, int] = LruCache(50)
        thread_count = 8
        ops_per_thread = 300
        threads = []

        def worker(thread_id: int):
            rand = random.Random(thread_id * 100)
            for i in range(ops_per_thread):
                key = rand.randint(1, 100)
                if rand.randint(0, 1) == 0:
                    cache.put(key, thread_id * 1000 + i)
                else:
                    cache.get(key)

        for t in range(thread_count):
            th = threading.Thread(target=worker, args=(t,))
            threads.append(th)
            th.start()

        for th in threads:
            th.join()

        # Invariant check: Cache count must never exceed capacity
        self.assertLessEqual(cache.count, 50)
        self.assertGreaterEqual(cache.count, 0)


if __name__ == "__main__":
    unittest.main()

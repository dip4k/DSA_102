import unittest
import time
from phase11_thread_safe_data_structures.token_bucket_rate_limiter import TokenBucketRateLimiter

class TestTokenBucketRateLimiter(unittest.TestCase):
    def test_burst_and_refill(self):
        limiter = TokenBucketRateLimiter(capacity=3.0, refill_rate_per_second=10.0)

        # Immediate burst of 3
        self.assertTrue(limiter.try_consume(1.0))
        self.assertTrue(limiter.try_consume(1.0))
        self.assertTrue(limiter.try_consume(1.0))
        # 4th throttled
        self.assertFalse(limiter.try_consume(1.0))

        # Sleep ~150ms for refill
        time.sleep(0.15)
        self.assertTrue(limiter.try_consume(1.0))

if __name__ == "__main__":
    unittest.main()

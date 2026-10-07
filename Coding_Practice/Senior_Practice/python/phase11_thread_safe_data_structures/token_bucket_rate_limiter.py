import time
import threading

class TokenBucketRateLimiter:
    def __init__(self, capacity: float, refill_rate_per_second: float):
        if capacity <= 0 or refill_rate_per_second <= 0:
            raise ValueError("Capacity and refill rate must be positive")
        self.capacity = capacity
        self.refill_rate = refill_rate_per_second
        self.tokens = capacity
        self.last_refill = time.monotonic()
        self.lock = threading.Lock()

    def try_consume(self, tokens: float = 1.0) -> bool:
        with self.lock:
            self._refill()
            if self.tokens >= tokens:
                self.tokens -= tokens
                return True
            return False

    def _refill(self) -> None:
        now = time.monotonic()
        elapsed = now - self.last_refill
        if elapsed > 0:
            self.tokens = min(self.capacity, self.tokens + elapsed * self.refill_rate)
            self.last_refill = now

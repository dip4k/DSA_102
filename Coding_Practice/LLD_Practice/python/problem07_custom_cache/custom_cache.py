"""Thread-safe LRU Cache implementation for LLD Practice."""

from collections import OrderedDict
import threading
from typing import Generic, Optional, TypeVar

K = TypeVar("K")
V = TypeVar("V")


class LruCache(Generic[K, V]):
    """Thread-safe Least Recently Used (LRU) Cache."""

    def __init__(self, capacity: int):
        if capacity <= 0:
            raise ValueError("Capacity must be greater than zero.")
        self._capacity: int = capacity
        self._cache: OrderedDict[K, V] = OrderedDict()
        self._lock: threading.RLock = threading.RLock()

    @property
    def capacity(self) -> int:
        return self._capacity

    @property
    def count(self) -> int:
        with self._lock:
            return len(self._cache)

    def get(self, key: K) -> Optional[V]:
        with self._lock:
            if key not in self._cache:
                return None
            self._cache.move_to_end(key)
            return self._cache[key]

    def put(self, key: K, value: V) -> None:
        with self._lock:
            if key in self._cache:
                self._cache[key] = value
                self._cache.move_to_end(key)
                return

            if len(self._cache) >= self._capacity:
                self._cache.popitem(last=False)

            self._cache[key] = value

    def remove(self, key: K) -> bool:
        with self._lock:
            if key in self._cache:
                del self._cache[key]
                return True
            return False

    def clear(self) -> None:
        with self._lock:
            self._cache.clear()

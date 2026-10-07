import threading
from typing import Generic, TypeVar

T = TypeVar('T')

class BoundedBlockingQueue(Generic[T]):
    def __init__(self, capacity: int):
        if capacity <= 0:
            raise ValueError("Capacity must be positive")
        self.capacity = capacity
        self.buffer: list[T] = [None] * capacity  # type: ignore
        self.head = 0
        self.tail = 0
        self.count = 0
        self.lock = threading.Lock()
        self.not_full = threading.Condition(self.lock)
        self.not_empty = threading.Condition(self.lock)

    def enqueue(self, item: T) -> None:
        with self.not_full:
            while self.count == self.capacity:
                self.not_full.wait()

            self.buffer[self.tail] = item
            self.tail = (self.tail + 1) % self.capacity
            self.count += 1
            self.not_empty.notify_all()

    def dequeue(self) -> T:
        with self.not_empty:
            while self.count == 0:
                self.not_empty.wait()

            item = self.buffer[self.head]
            self.head = (self.head + 1) % self.capacity
            self.count -= 1
            self.not_full.notify_all()
            return item

    def size(self) -> int:
        with self.lock:
            return self.count

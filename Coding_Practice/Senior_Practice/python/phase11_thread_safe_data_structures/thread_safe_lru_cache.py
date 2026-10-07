import threading
from typing import Generic, TypeVar, Optional

K = TypeVar('K')
V = TypeVar('V')

class Node(Generic[K, V]):
    def __init__(self, key: Optional[K] = None, val: Optional[V] = None):
        self.key = key
        self.val = val
        self.prev: Optional[Node[K, V]] = None
        self.next: Optional[Node[K, V]] = None

class ThreadSafeLruCache(Generic[K, V]):
    def __init__(self, capacity: int):
        if capacity <= 0:
            raise ValueError("Capacity must be positive")
        self.capacity = capacity
        self.map: dict[K, Node[K, V]] = {}
        self.head: Node[K, V] = Node()
        self.tail: Node[K, V] = Node()
        self.head.next = self.tail
        self.tail.prev = self.head
        self.lock = threading.RLock()

    @property
    def count(self) -> int:
        with self.lock:
            return len(self.map)

    def get(self, key: K) -> Optional[V]:
        with self.lock:
            if key not in self.map:
                return None
            node = self.map[key]
            self._detach(node)
            self._attach_head(node)
            return node.val

    def put(self, key: K, val: V) -> None:
        with self.lock:
            if key in self.map:
                existing = self.map[key]
                existing.val = val
                self._detach(existing)
                self._attach_head(existing)
                return

            if len(self.map) >= self.capacity:
                lru = self.tail.prev
                assert lru is not None and lru.key is not None
                self._detach(lru)
                del self.map[lru.key]

            new_node = Node(key, val)
            self.map[key] = new_node
            self._attach_head(new_node)

    def _detach(self, node: Node[K, V]) -> None:
        assert node.prev is not None and node.next is not None
        node.prev.next = node.next
        node.next.prev = node.prev

    def _attach_head(self, node: Node[K, V]) -> None:
        assert self.head.next is not None
        node.next = self.head.next
        node.prev = self.head
        self.head.next.prev = node
        self.head.next = node

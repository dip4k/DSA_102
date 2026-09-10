# Traversal Mastery: Heaps

## Summary Table

| Level | Mental Model | Pointer/State | Drill Problems |
|---|---|---|---|
| 1. Fundamentals | Root always holds global extremum | Heap size, root node | [LeetCode 1046: Last Stone Weight] (Easy) |
| 2. Top K Elements | Maintain size K heap for stream | Heap size bounded to K | [LeetCode 215: Kth Largest Element in an Array] (Medium) |
| 3. Merge K Sorted Lists | Multi-way merge via min-heap | 1 pointer per list in heap | [LeetCode 23: Merge k Sorted Lists] (Hard) |
| 4. Two Heaps | Partition data into lower/upper halves | Max-heap (left) + Min-heap (right) | [LeetCode 295: Find Median from Data Stream] (Hard) |

---

## Level 1: Min/Max Heap Fundamentals

### Mental Models & Invariants
*   **What does the state/index mean?** Represents the current set of available elements. Root is always the minimum (min-heap) or maximum (max-heap).
*   **What region is processed?** Entire collection of dynamically changing elements.
*   **What is the invariant/recurrence relation?** Parent node value <= children values (min-heap). Operations `push` and `pop` maintain this invariant in $O(\log N)$ time.

### Visual State Transitions
*Simulating [LeetCode 1046: Last Stone Weight] with `[2,7,4,1,8,1]`*:

| Step | Action | Heap State (Max-Heap) | Invariant |
|---|---|---|---|
| 1 | Heapify | `[8, 7, 4, 1, 2, 1]` | Max element at root |
| 2 | Pop 8, 7 | `[4, 2, 1, 1]` | Extracted largest two |
| 3 | Push 1 (8-7) | `[4, 2, 1, 1, 1]` | Heap property restored |
| 4 | Pop 4, 2 | `[1, 1, 1]` | Extracted largest two |
| 5 | Push 2 (4-2) | `[2, 1, 1, 1]` | Heap property restored |

### Code Snippets

Problem: Given an array of integers representing stone weights, repeatedly smash the two heaviest stones together until at most one stone remains. Return the weight of the last remaining stone.
```python
# Python uses min-heap by default. Multiply by -1 for max-heap.
import heapq

def lastStoneWeight(stones: list[int]) -> int:
    # 1. Initialize max-heap with negated values
    max_heap = [-s for s in stones]
    heapq.heapify(max_heap)
    
    # 2. Process elements: maintain invariant of comparing the top 2 heaviest stones
    while len(max_heap) > 1:
        # 3. Extract the two global maximums
        y = -heapq.heappop(max_heap)
        x = -heapq.heappop(max_heap)
        
        # 4. If they differ, push the remaining stone weight back into the heap
        if y != x:
            heapq.heappush(max_heap, -(y - x))
            
    # 5. Return the last remaining stone, or 0 if heap is empty
    return -max_heap[0] if max_heap else 0
```

Problem: Given an array of integers representing stone weights, repeatedly smash the two heaviest stones together until at most one stone remains. Return the weight of the last remaining stone.
```csharp
public int LastStoneWeight(int[] stones) {
    // 1. Initialize max-heap using custom comparer in C# 11+ PriorityQueue
    var pq = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
    foreach (var s in stones) pq.Enqueue(s, s);
    
    // 2. Process elements: maintain invariant of comparing the top 2 heaviest stones
    while (pq.Count > 1) {
        // 3. Extract the two global maximums
        int y = pq.Dequeue();
        int x = pq.Dequeue();
        
        // 4. If they differ, push the remaining stone weight back into the heap
        if (y != x) pq.Enqueue(y - x, y - x);
    }
    
    // 5. Return the last remaining stone, or 0 if heap is empty
    return pq.Count == 1 ? pq.Dequeue() : 0;
}
```

### ⚠️ Gotchas & Pitfalls
*   **Python Max-Heap:** Python only provides a min-heap. Must negate values for max-heap, which can lead to bugs if you forget to negate back upon extraction.
*   **Custom Comparators:** Storing complex objects requires carefully defined comparators (e.g., overriding `__lt__` in Python or implementing `IComparer` in C#).

### Drill Problems
*   [LeetCode 1046: Last Stone Weight] (Easy)
*   [LeetCode 703: Kth Largest Element in a Stream] (Easy)

---

## Level 2: Top K Elements

### Mental Models & Invariants
*   **What does the state/index mean?** A heap bounded to exactly size K.
*   **What region is processed?** Streaming elements or a 1D array.
*   **What is the invariant/recurrence relation?** For "Top K Largest", use a Min-Heap of size K. The root is the *smallest* of the *K largest* elements seen so far. Every new element > root kicks the root out.

### Visual State Transitions
*Finding Top 3 Largest in `[3, 2, 1, 5, 6, 4]`*:

| Step | Action | Min-Heap State | Invariant |
|---|---|---|---|
| 1 | Push 3, 2, 1 | `[1, 2, 3]` | Heap size = 3 |
| 2 | See 5. (5 > 1). Pop 1, Push 5 | `[2, 5, 3]` | Top 3 of `[3,2,1,5]` |
| 3 | See 6. (6 > 2). Pop 2, Push 6 | `[3, 5, 6]` | Top 3 of `[3,2,1,5,6]` |
| 4 | See 4. (4 > 3). Pop 3, Push 4 | `[4, 5, 6]` | Top 3 of `[3,2,1,5,6,4]` |

### Code Snippets

Problem: Find the kth largest element in an unsorted array using a bounded min-heap to keep track of the largest k elements seen so far.
```python
import heapq

def findKthLargest(nums: list[int], k: int) -> int:
    # 1. Initialize an empty min-heap
    min_heap = []
    
    # 2. Iterate through each number in the array
    for num in nums:
        # 3. Push the current number into the heap
        heapq.heappush(min_heap, num)
        
        # 4. Enforce invariant: keep heap size <= k
        if len(min_heap) > k:
            heapq.heappop(min_heap)
            
    # 5. The root holds the kth largest element globally
    return min_heap[0]
```

Problem: Find the kth largest element in an unsorted array using a bounded min-heap to keep track of the largest k elements seen so far.
```csharp
public int FindKthLargest(int[] nums, int k) {
    // 1. Initialize an empty min-heap
    var pq = new PriorityQueue<int, int>();
    
    // 2. Iterate through each number in the array
    foreach (var num in nums) {
        // 3. Push the current number into the heap
        pq.Enqueue(num, num);
        
        // 4. Enforce invariant: keep heap size <= k
        if (pq.Count > k) {
            pq.Dequeue();
        }
    }
    
    // 5. The root holds the kth largest element globally
    return pq.Peek();
}
```

### ⚠️ Gotchas & Pitfalls
*   **Heap Direction Mixup:** "Top K Largest" requires a **Min-Heap**. "Top K Smallest" requires a **Max-Heap**. Intuitively inverted.
*   **Premature Popping:** Ensure you only pop *after* pushing the new element, or only push if the new element satisfies the condition `> min_heap[0]`.

### Drill Problems
*   [LeetCode 215: Kth Largest Element in an Array] (Medium)
*   [LeetCode 347: Top K Frequent Elements] (Medium)

---

## Level 3: Merge K Sorted Lists

### Mental Models & Invariants
*   **What does the state/index mean?** Active pointer for each of the K lists.
*   **What region is processed?** K sorted linked lists or arrays.
*   **What is the invariant/recurrence relation?** The heap always contains exactly 1 element from each non-exhausted list. The minimum element globally must be the minimum of the current fronts of all lists.

### Visual State Transitions
*Merging `L1: [1,4,5]`, `L2: [1,3,4]`, `L3: [2,6]`*:

| Step | Action | Min-Heap State (Val, ListIdx, Ptr) | Invariant |
|---|---|---|---|
| 1 | Push heads | `[(1,0,ptr0), (1,1,ptr1), (2,2,ptr2)]` | One node per list |
| 2 | Pop (1,0,ptr0), Push L1 next | `[(1,1,ptr1), (2,2,ptr2), (4,0,ptr0)]` | Global min merged |
| 3 | Pop (1,1,ptr1), Push L2 next | `[(2,2,ptr2), (3,1,ptr1), (4,0,ptr0)]` | Global min merged |

### Code Snippets

Problem: Merge k sorted linked lists into a single sorted linked list by maintaining a min-heap of the current smallest elements from each list.
```python
import heapq

class ListNode:
    def __init__(self, val=0, next=None):
        self.val = val
        self.next = next

def mergeKLists(lists: list[ListNode]) -> ListNode:
    # 1. Initialize a min-heap to store the head of each list
    min_heap = []
    
    # 2. Push initial list heads into the heap
    # Note: Add index 'i' to break ties if node values are equal
    for i, node in enumerate(lists):
        if node:
            heapq.heappush(min_heap, (node.val, i, node))
            
    # 3. Dummy node to build the result list easily
    dummy = ListNode(0)
    curr = dummy
    
    # 4. Multi-way merge: extract global minimums
    while min_heap:
        val, i, node = heapq.heappop(min_heap)
        curr.next = node
        curr = curr.next
        
        # 5. Maintain invariant: 1 element per non-exhausted list
        if node.next:
            heapq.heappush(min_heap, (node.next.val, i, node.next))
            
    return dummy.next
```

Problem: Merge k sorted linked lists into a single sorted linked list by maintaining a min-heap of the current smallest elements from each list.
```csharp
public class ListNode {
    public int val;
    public ListNode next;
    public ListNode(int val=0, ListNode next=null) { this.val = val; this.next = next; }
}

public ListNode MergeKLists(ListNode[] lists) {
    // 1. Initialize a min-heap to store the head of each list
    var pq = new PriorityQueue<ListNode, int>();
    
    // 2. Push initial list heads into the heap
    foreach (var list in lists) {
        if (list != null) {
            pq.Enqueue(list, list.val);
        }
    }
    
    // 3. Dummy node to build the result list easily
    ListNode dummy = new ListNode(0);
    ListNode curr = dummy;
    
    // 4. Multi-way merge: extract global minimums
    while (pq.Count > 0) {
        var node = pq.Dequeue();
        curr.next = node;
        curr = curr.next;
        
        // 5. Maintain invariant: 1 element per non-exhausted list
        if (node.next != null) {
            pq.Enqueue(node.next, node.next.val);
        }
    }
    return dummy.next;
}
```

### ⚠️ Gotchas & Pitfalls
*   **Tie-Breaking in Python:** Heap tuples `(val, node)` will crash if `val` is equal because `ListNode` isn't comparable. Must insert an index: `(val, idx, node)`.
*   **Null List Heads:** Initializing the heap with null nodes from an empty list will cause `NullReferenceExceptions`. Always check `if node is not None` before initial insertion.

### Drill Problems
*   [LeetCode 23: Merge k Sorted Lists] (Hard)
*   [LeetCode 373: Find K Pairs with Smallest Sums] (Medium)

---

## Level 4: Two Heaps (Running Median)

### Mental Models & Invariants
*   **What does the state/index mean?** Data is partitioned. `small` (Max-Heap) holds the lower half. `large` (Min-Heap) holds the upper half.
*   **What region is processed?** Streaming numbers.
*   **What is the invariant/recurrence relation?** 
    1. Size invariant: `len(small) == len(large)` or `len(small) == len(large) + 1`.
    2. Value invariant: `max(small) <= min(large)`.

### Visual State Transitions
*Stream: `[2, 1, 5, 7, 2, 0, 5]`*

| Step | Input | Small (Max-Heap) | Large (Min-Heap) | Median | Invariant |
|---|---|---|---|---|---|
| 1 | 2 | `[2]` | `[]` | 2.0 | `small` is larger by 1 |
| 2 | 1 | `[1]` | `[2]` | 1.5 | Sizes equal |
| 3 | 5 | `[2, 1]` | `[5]` | 2.0 | `small` is larger by 1 |
| 4 | 7 | `[2, 1]` | `[5, 7]` | 3.5 | Sizes equal |

### Code Snippets

Problem: Design a data structure that supports adding integer numbers from a data stream and finding the median of all elements seen so far.
```python
import heapq

class MedianFinder:
    def __init__(self):
        # 1. Initialize two heaps: small (max-heap for lower half) and large (min-heap for upper half)
        self.small = [] # Max-Heap (needs negative values)
        self.large = [] # Min-Heap

    def addNum(self, num: int) -> None:
        # 2. Push new number to the small max-heap first
        heapq.heappush(self.small, -num)
        
        # 3. Enforce value invariant: max(small) <= min(large)
        if self.small and self.large and (-self.small[0] > self.large[0]):
            val = -heapq.heappop(self.small)
            heapq.heappush(self.large, val)
            
        # 4. Enforce size invariant: len(small) == len(large) or len(small) == len(large) + 1
        if len(self.small) > len(self.large) + 1:
            val = -heapq.heappop(self.small)
            heapq.heappush(self.large, val)
        elif len(self.large) > len(self.small):
            val = heapq.heappop(self.large)
            heapq.heappush(self.small, -val)

    def findMedian(self) -> float:
        # 5. Calculate median based on the size invariant
        if len(self.small) > len(self.large):
            return -float(self.small[0])
        return (-self.small[0] + self.large[0]) / 2.0
```

Problem: Design a data structure that supports adding integer numbers from a data stream and finding the median of all elements seen so far.
```csharp
public class MedianFinder {
    // 1. Initialize two heaps: small (max-heap for lower half) and large (min-heap for upper half)
    private PriorityQueue<int, int> small; // Max-Heap
    private PriorityQueue<int, int> large; // Min-Heap

    public MedianFinder() {
        small = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
        large = new PriorityQueue<int, int>();
    }
    
    public void AddNum(int num) {
        // 2. Push new number to the small max-heap first
        small.Enqueue(num, num);
        
        // 3. Enforce value invariant: max(small) <= min(large)
        if (small.Count > 0 && large.Count > 0 && small.Peek() > large.Peek()) {
            int val = small.Dequeue();
            large.Enqueue(val, val);
        }
        
        // 4. Enforce size invariant: small.Count == large.Count or small.Count == large.Count + 1
        if (small.Count > large.Count + 1) {
            int val = small.Dequeue();
            large.Enqueue(val, val);
        } else if (large.Count > small.Count) {
            int val = large.Dequeue();
            small.Enqueue(val, val);
        }
    }
    
    public double FindMedian() {
        // 5. Calculate median based on the size invariant
        if (small.Count > large.Count) return small.Peek();
        return (small.Peek() + large.Peek()) / 2.0;
    }
}
```

### ⚠️ Gotchas & Pitfalls
*   **Balancing Logic:** Pushing to one heap unconditionally without checking the value invariant (`max(small) <= min(large)`) breaks the sorted partition. Always route values properly.
*   **Size Constraints:** Maintaining `len(small) >= len(large)` is easier than keeping them perfectly flexible. Choose one invariant and strictly enforce it after every insert.

### Drill Problems
*   [LeetCode 295: Find Median from Data Stream] (Hard)
*   [LeetCode 480: Sliding Window Median] (Hard)

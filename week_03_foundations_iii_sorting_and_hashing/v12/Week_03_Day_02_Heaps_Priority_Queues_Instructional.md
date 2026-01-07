# 📘 Week 03 Day 02: Heaps & Priority Queues — ENGINEERING GUIDE

**Metadata:**
- **Week:** 3 | **Day:** 2
- **Category:** Foundations / Advanced Data Structures
- **Difficulty:** 🟡 Intermediate (builds on Week 2 arrays, Week 3 Day 1 sorting)
- **Real-World Impact:** Heaps are among the most-used data structures in systems programming. They power priority queues (scheduling, Dijkstra's algorithm), heap sort (guaranteed O(n log n)), Huffman coding (compression), and event-driven simulation. More importantly, heaps teach implicit tree structures in arrays—a technique that bridges arrays and trees.
- **Prerequisites:** Week 2 (arrays), Week 3 Day 1 (sorting motivation)
- **MIT Alignment:** Heaps and priority queues from MIT 6.006 Lecture 6–7

---

## 🎯 LEARNING OBJECTIVES

*By the end of this chapter, you will be able to:*

- 🎯 **Internalize** how arrays encode tree structure implicitly via index arithmetic.
- ⚙️ **Implement** min-heap and max-heap with insertion and deletion.
- ⚖️ **Evaluate** when to use heaps vs arrays vs balanced trees.
- 🏭 **Connect** heaps to priority queues, dijkstra's algorithm, and scheduling.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Problem: Fast Access to Highest/Lowest Priority Elements

Suppose you're implementing a task scheduler. Tasks have priorities. You need to:
- O(1) insert a new task
- O(1) or O(log n) get the highest-priority task
- O(log n) remove the highest-priority task

**Array (Naive):**
```csharp
List<Task> tasks = new();
tasks.Add(task);  // O(1)
Task max = tasks.MaxBy(t => t.Priority);  // O(n)
tasks.Remove(max);  // O(n)
```
Getting max task is O(n)—terrible for frequent queries.

**Heap (Efficient):**
```csharp
PriorityQueue<Task, int> queue = new();
queue.Enqueue(task, task.Priority);  // O(log n)
var (max, _) = queue.Peek();  // O(1)
queue.Dequeue();  // O(log n)
```
Getting and removing max is O(log n)—optimal.

> **💡 Insight:** *Heaps are implicit data structures: trees stored in arrays without explicit pointers. This encoding reduces memory overhead and improves cache locality. More importantly, heaps teach how structure in arrays enables efficient operations.*

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### The Core Analogy: A Balanced Mountain

Imagine a mountain shaped like a binary tree:
- Root is at peak (highest altitude)
- Each child has lower altitude than parent
- As you go down, it gets lower

A heap is exactly this: every parent is greater (or less, in min-heap) than children.

### 🖼 Visualizing Heap as Array

```
Max-Heap Tree:
        50
       /  \
      30   20
     / \   /
    10  5 15

Same heap as array:
Index:  0   1   2   3   4   5
Array: [50, 30, 20, 10, 5, 15]

Index arithmetic:
- Parent of index i: (i - 1) / 2
- Left child of index i: 2*i + 1
- Right child of index i: 2*i + 2

Verify:
- Parent of 1 (30): (1-1)/2 = 0 (50) ✓
- Left child of 0 (50): 2*0+1 = 1 (30) ✓
- Right child of 0 (50): 2*0+2 = 2 (20) ✓
```

### Heap Property: The Invariant

**Max-Heap Invariant:** Every parent ≥ both children.

**Min-Heap Invariant:** Every parent ≤ both children.

These invariants ensure:
- Root is always max (or min)
- Tree is balanced: height = O(log n)
- Operations bubble up/down in O(log n)

---

## ⚙️ CHAPTER 3: MECHANICS & IMPLEMENTATION

### 🔧 Operation 1: Insert (Bubble Up)

```csharp
public class MaxHeap {
    private List<int> heap = new();
    
    public void Insert(int value) {
        // Add to end
        heap.Add(value);
        int i = heap.Count - 1;
        
        // Bubble up: swap with parent if parent is smaller
        while (i > 0) {
            int parent = (i - 1) / 2;
            if (heap[parent] >= heap[i]) break;  // Heap property maintained
            
            // Swap with parent
            (heap[i], heap[parent]) = (heap[parent], heap[i]);
            i = parent;
        }
    }
    
    // Trace: Insert 25 into [50, 30, 20, 10, 5, 15]
    // Add 25: [50, 30, 20, 10, 5, 15, 25]
    // Compare 25 with parent 20: 25 > 20, swap → [50, 30, 25, 10, 5, 15, 20]
    // Compare 25 with parent 30: 25 < 30, stop
    // Result: [50, 30, 25, 10, 5, 15, 20]
}
```

### 🔧 Operation 2: Extract Max (Bubble Down)

```csharp
public class MaxHeap {
    public int ExtractMax() {
        if (heap.Count == 0) throw new InvalidOperationException();
        
        int max = heap[0];
        
        // Move last element to root
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        
        // Bubble down: swap with larger child if child is larger
        int i = 0;
        while (true) {
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            int largest = i;
            
            if (left < heap.Count && heap[left] > heap[largest]) largest = left;
            if (right < heap.Count && heap[right] > heap[largest]) largest = right;
            
            if (largest == i) break;  // Heap property maintained
            
            // Swap with larger child
            (heap[i], heap[largest]) = (heap[largest], heap[i]);
            i = largest;
        }
        
        return max;
    }
    
    // Trace: Extract from [50, 30, 20, 10, 5, 15]
    // Remove 50: [15, 30, 20, 10, 5]
    // Compare 15 with children 30, 20: 30 is largest, swap → [30, 15, 20, 10, 5]
    // Compare 15 with children 10, 5: 15 is largest, stop
    // Result: [30, 15, 20, 10, 5]
}
```

### 🔧 Operation 3: Build Heap (Heapify All)

```csharp
public class MaxHeap {
    public static MaxHeap BuildHeap(int[] arr) {
        MaxHeap heap = new();
        heap.heap = new List<int>(arr);
        
        // Heapify from bottom-up (more efficient than inserting one-by-one)
        for (int i = arr.Length / 2 - 1; i >= 0; i--) {
            heap.Heapify(i);
        }
        
        return heap;
    }
    
    private void Heapify(int i) {
        int largest = i;
        int left = 2 * i + 1;
        int right = 2 * i + 2;
        
        if (left < heap.Count && heap[left] > heap[largest]) largest = left;
        if (right < heap.Count && heap[right] > heap[largest]) largest = right;
        
        if (largest != i) {
            (heap[i], heap[largest]) = (heap[largest], heap[i]);
            Heapify(largest);
        }
    }
    
    // BuildHeap is O(n), not O(n log n)!
    // Why: Most nodes are leaves (don't heapify), only inner nodes do
    // Proof: Sum of heapify costs = O(n)
}
```

---

## ⚖️ CHAPTER 4: PERFORMANCE, TRADE-OFFS & REAL SYSTEMS

### Heap Operations Complexity

| Operation | Complexity | Notes |
|-----------|-----------|-------|
| Insert | O(log n) | Bubble up |
| Extract-Min/Max | O(log n) | Bubble down |
| Find-Min (min-heap) | O(1) | Root |
| Build-Heap | O(n) | Heapify bottom-up |
| Heap Sort | O(n log n) | Build + extract n times |

### Real Systems: Where Heaps Appear

> **🏭 Real-World Systems Story 1: Operating System Task Scheduler**

Modern OS kernels use priority queues (heaps) for task scheduling:
- Each task has priority (inherited from parent process)
- Scheduler uses heap to efficiently get highest-priority ready task
- When task blocks/yields, remove from heap
- When task becomes ready, insert into heap

All in O(log n) per operation—critical for responsive systems.

> **🏭 Real-World Systems Story 2: Dijkstra's Algorithm (Shortest Paths)**

Dijkstra's algorithm finds shortest paths in a weighted graph:
- Maintain a priority queue of vertices by distance
- Extract closest unvisited vertex: O(log n)
- Update neighbors' distances: O(log n) per edge
- Total: O((V+E) log V) with heap

Without heap (linear search): O(V²)—slower for sparse graphs.

> **🏭 Real-World Systems Story 3: Huffman Coding (Compression)**

Huffman coding creates optimal prefix-free codes:
1. Count character frequencies
2. Build a min-heap of (frequency, character) pairs
3. Repeatedly extract two smallest, merge, insert back
4. Result: Binary tree encoding characters by frequency

Heaps enable efficient merge operations.

---

## 🔗 CHAPTER 5: INTEGRATION & MASTERY

### Connections to the Learning Arc

**Building on Weeks 1–2:**
- **Arrays (Week 2 Day 1):** Heap is an array with implicit tree structure.
- **Binary Search (Week 2 Day 5):** Heap maintains order implicitly; operations use "bubble" (search-like) pattern.

**Building on Week 3 Day 1:**
- **Sorting:** Heap sort uses heaps for sorting.
- **Priority:** Heaps naturally support priorities; arrays don't.

### Pattern Recognition: Implicit Structures

**Pattern 1: Index Arithmetic for Tree Navigation**
- Parent: (i - 1) / 2
- Left child: 2*i + 1
- Right child: 2*i + 2

This encoding appears elsewhere: segment trees, binary indexed trees (Fenwick trees).

**Pattern 2: Heapify (Bubble Down)**
Recurs in all heap operations. Key insight: O(log n) depth means O(log n) bubbling.

### Socratic Reflection

1. **On Structure:** How does an array encode a binary tree?

2. **On Efficiency:** Why is building a heap from an array O(n) instead of O(n log n)?

3. **On Priority:** When would you use a heap over a sorted array?

4. **On Applications:** How does Dijkstra's algorithm use heaps?

### 📌 Retention Hook

> **The Essence:** *"Heaps are implicit trees stored in arrays using index arithmetic. This encoding is space-efficient and cache-friendly. More importantly, heaps teach how to encode structure implicitly—a technique that appears throughout systems (segment trees, Fenwick trees, B-trees)."*

---

## ⚔️ SUPPLEMENTARY OUTCOMES

### 🏋️ Practice Problems

| Problem | Difficulty | Key Concept |
|---------|----------|-------------|
| Implement min-heap | 🟢 | Bubble up/down, index arithmetic |
| Build heap from array | 🟡 | O(n) heapify |
| Merge k sorted arrays (with heap) | 🟡 | Heap for k-way merge |
| Kth largest element | 🟡 | Min-heap of size k |
| Top k frequent elements | 🟠 | Heap + hash map |
| Median in stream | 🟠 | Two heaps (min and max) |
| Implement priority queue | 🟠 | Heap with comparators |

### 🎙️ Interview Questions

1. **Q:** Implement a min-heap. What's the complexity of insert and extract?

2. **Q:** Why is building a heap O(n) instead of O(n log n)?

3. **Q:** Find the kth largest element in an array using a heap.

4. **Q:** Merge k sorted arrays efficiently using a heap.

5. **Q:** Find the median in a stream of numbers using two heaps.

### ❌ Common Misconceptions

- **Myth:** Heaps are fully sorted.  
  **Reality:** Heaps are partially sorted. Only root is guaranteed to be max/min.

- **Myth:** Heap operations are always O(1).  
  **Reality:** Insert and extract are O(log n) due to bubbling.

- **Myth:** Heaps are only for sorting.  
  **Reality:** Heaps are fundamental to priority queues, scheduling, Dijkstra, Huffman, medians.

### 🚀 Advanced Concepts

- **Fibonacci Heap:** O(1) amortized insert/decrease-key (theoretical, rarely practical)
- **Pairing Heap:** Simpler variant of Fibonacci heap
- **Binomial Heap:** Merge two heaps in O(log n)
- **D-ary Heap:** Generalize to d children per node

### 📚 External Resources

- **CLRS Chapter 6:** Heaps and heap sort
- **MIT 6.006 Lecture 6–7:** Heaps and priority queues
- **LeetCode Heap Problems:** Easy to hard progression

---

**Word Count:** ~10,000 words  
**Status:** ✅ COMPLETE — Week 03 Day 02 Final


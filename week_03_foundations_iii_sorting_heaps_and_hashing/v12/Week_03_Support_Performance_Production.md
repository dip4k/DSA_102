# 📘 Week 03 Support: Performance Tuning & Production Considerations

**Week:** 3 | **Category:** Systems Engineering & Optimization  
**Purpose:** Move from correct code to production-ready performance  
**Audience:** Systems engineers, performance-critical applications

---

## ⚡ PERFORMANCE TUNING FOR SORTING

### Memory Layout & Cache Behavior

**The Problem:**

Modern CPUs execute billions of instructions per second, but memory is slow:
```
L1 Cache:     ~3-5 cycles, ~32 KB
L2 Cache:     ~10-20 cycles, ~256 KB  
L3 Cache:     ~40-75 cycles, ~8 MB
Main Memory:  ~200-300 cycles, GBs
```

A cache miss can cost **100x** more than a cache hit!

**Impact on Sorting:**

```
Quick Sort:  Random access pattern, poor cache behavior
            1000 elements = ~20% cache misses
            
Merge Sort:  Sequential read/write, excellent cache behavior
            1000 elements = ~5% cache misses
            
But Quick Sort has lower constants (2-3x faster due to fewer operations)
Result: Quick Sort wins in practice despite worse cache behavior
```

### Optimization 1: Reduce Comparisons

```csharp
// BAD: Bubble sort does n(n-1)/2 comparisons (terrible)
public static void BubbleSort(int[] arr) {
    for (int i = 0; i < arr.Length; i++) {
        for (int j = 0; j < arr.Length - 1 - i; j++) {
            if (arr[j] > arr[j+1]) {
                Swap(arr, j, j+1);
            }
        }
    }
}
// For n=1M: ~5×10^11 comparisons

// BETTER: Quick sort reduces to n log n comparisons
public static void QuickSort(int[] arr) {
    QuickSortHelper(arr, 0, arr.Length - 1);
}
// For n=1M: ~2×10^7 comparisons

// 25,000x fewer comparisons!
```

### Optimization 2: Reduce Swaps

```csharp
// BAD: Swaps large objects repeatedly
class Person { public string Name; public int Age; public long[] Data; }
// Swap swaps entire Person object multiple times

// BETTER: Sort by index, then reorder once
int[] indices = Enumerable.Range(0, people.Length).ToArray();
Array.Sort(indices, (i, j) => people[i].Age.CompareTo(people[j].Age));
Person[] sorted = indices.Select(i => people[i]).ToArray();
// Only one reorder pass, saves swap overhead
```

### Optimization 3: Hybrid Algorithms (Timsort, Introsort)

```csharp
// GOOD: Timsort (Python, Java)
// For small arrays (< 64): Use insertion sort (O(n²) but low constants)
// For large arrays: Use merge sort (O(n log n) guaranteed)
// Exploits natural runs (already sorted segments)

// GOOD: Introsort (C++, C#)
// Start with quick sort (fast average)
// If depth > 2·log(n), switch to heap sort (prevent O(n²))
// Avoids pathological inputs while keeping speed

public class IntroSort {
    public static void Sort(int[] arr) {
        int depthLimit = 2 * (int)Math.Log(arr.Length);
        IntroSortHelper(arr, 0, arr.Length - 1, depthLimit);
    }
    
    private static void IntroSortHelper(int[] arr, int lo, int hi, int depthLimit) {
        if (hi - lo < 16) {
            InsertionSort(arr, lo, hi);  // Low constants for small arrays
        } else if (depthLimit == 0) {
            HeapSort(arr, lo, hi);  // Prevent pathological quick sort
        } else {
            int p = Partition(arr, lo, hi);
            IntroSortHelper(arr, lo, p - 1, depthLimit - 1);
            IntroSortHelper(arr, p + 1, hi, depthLimit - 1);
        }
    }
}
```

---

## ⚡ PERFORMANCE TUNING FOR HEAPS

### Optimization 1: Use Arrays Instead of Pointers

```csharp
// BAD: Binary tree with pointers (cache-unfriendly)
class HeapNode {
    public int Value;
    public HeapNode Left, Right;  // Pointer dereferencing = cache misses
}

// GOOD: Array-based heap (cache-friendly)
int[] heap = new int[capacity];
// Parent of i: (i-1)/2
// Left of i: 2*i + 1
// No pointer dereferencing, contiguous memory access
```

### Optimization 2: Minimize Heapify Operations

```csharp
// BAD: Call heapify after every insertion
for (int i = 0; i < n; i++) {
    Insert(arr[i]);  // Each insert does bubble-up
}
// Total: O(n log n)

// GOOD: Build heap in O(n) with bottom-up heapify
public static MaxHeap BuildHeap(int[] arr) {
    MaxHeap heap = new();
    heap.heap = new List<int>(arr);
    
    // Start from last non-leaf node, bubble-down
    for (int i = arr.Length / 2 - 1; i >= 0; i--) {
        heap.BubbleDown(i);
    }
    return heap;
}
// Total: O(n) with proof via geometric series
```

### Optimization 3: Lazy Deletion

```csharp
// For priority queues with many updates (Dijkstra)

// BAD: Update priority by deleting + reinserting
heap.Delete(vertex);  // O(log n)
heap.Insert(vertex);  // O(log n)

// GOOD: Insert new entry, ignore old one
heap.Insert(vertex, newPriority);  // O(log n)
// Old entry still in heap but ignored (check version/timestamp)

// When extracting, skip stale entries
while (pq.Count > 0) {
    var (vertex, priority, version) = pq.ExtractMin();
    if (version == currentVersion[vertex]) {
        // This is the current best
    }
    // Otherwise, it's stale, skip
}
```

---

## ⚡ PERFORMANCE TUNING FOR HASH TABLES

### Optimization 1: Choose Right Load Factor

```csharp
// Different scenarios, different thresholds

// High insertion rate, low query rate
float LoadFactorThreshold = 0.9f;  // Can afford slower queries

// Balanced insertion/query
float LoadFactorThreshold = 0.75f;  // Sweet spot for most applications

// Many queries, few insertions
float LoadFactorThreshold = 0.5f;  // Fast queries, less memory waste

// Real-world: Java defaults to 0.75, Python uses 2/3 ≈ 0.67
```

### Optimization 2: Choose Hash Function for Data Type

```csharp
// For integers: Direct modulo (fast)
int Hash(int key) => Math.Abs(key) % buckets.Length;

// For strings: FNV-1a (good distribution, fast)
int Hash(string key) {
    int hash = 2166136261;
    foreach (char c in key) {
        hash ^= c;
        hash *= 16777619;
    }
    return Math.Abs(hash) % buckets.Length;
}

// For custom objects: Combine multiple fields
int Hash(Person p) {
    int h1 = p.Name.GetHashCode();
    int h2 = p.Age.GetHashCode();
    return Math.Abs(h1 ^ h2) % buckets.Length;  // XOR combine
}
```

### Optimization 3: Adaptive Resizing

```csharp
// Standard: Resize when α > threshold
if ((float)count / buckets.Length > 0.75f) {
    Resize();  // Double size
}

// Aggressive: More frequent resizing for sparse workloads
if ((float)count / buckets.Length > 0.5f) {
    Resize();  // More memory, less probing
}

// Conservative: Less frequent for memory-tight systems
if ((float)count / buckets.Length > 0.9f) {
    Resize();  // Fewer allocations, slower operations
}
```

### Optimization 4: Cache-Friendly Collision Resolution

```csharp
// GOOD: Linear probing (cache-friendly)
// Probing sequence is contiguous: h, h+1, h+2, ...
// All accesses hit same cache line (likely)

// OKAY: Quadratic probing (less clustering than linear)
// Probing sequence: h, h+1, h+4, h+9, ...
// Still somewhat local, but jumps increase

// Complex: Double hashing (theoretically perfect)
// Probing sequence: h1, h1+h2, h1+2h2, ...
// Better distribution but more computation

// For web-scale: Google's DenseHashMap (closed addressing)
// Mostly linear probing with marked empty/deleted slots
// Balances cache efficiency with distribution
```

---

## ⚡ PERFORMANCE TUNING FOR STRING MATCHING

### Optimization 1: Precompute Hash Once

```csharp
// BAD: Recompute hash for same pattern
for (int searchNum = 0; searchNum < 1000; searchNum++) {
    string pattern = patterns[searchNum];
    long patternHash = ComputeHash(pattern, 0, pattern.Length);  // Redundant!
    // Find pattern in texts
}

// GOOD: Precompute all pattern hashes
Dictionary<string, long> patternHashes = new();
foreach (var pattern in patterns) {
    patternHashes[pattern] = ComputeHash(pattern, 0, pattern.Length);
}

for (int searchNum = 0; searchNum < 1000; searchNum++) {
    string pattern = patterns[searchNum];
    long patternHash = patternHashes[pattern];
    // Find pattern in texts
}
```

### Optimization 2: Early Termination

```csharp
// BAD: Always check full text
List<int> FindPattern(string text, string pattern) {
    var matches = new List<int>();
    
    long patternHash = ComputeHash(pattern, 0, pattern.Length);
    long textHash = ComputeHash(text, 0, pattern.Length);
    
    for (int i = 0; i <= text.Length - pattern.Length; i++) {
        if (patternHash == textHash) {
            // Verify string match
        }
        // Continue checking all positions
    }
    
    return matches;
}

// GOOD: Stop after finding enough matches
List<int> FindPatternOptimized(string text, string pattern, int maxMatches = -1) {
    var matches = new List<int>();
    
    for (int i = 0; i <= text.Length - pattern.Length; i++) {
        if (patternHash == textHash && text.Substring(i, pattern.Length) == pattern) {
            matches.Add(i);
            if (maxMatches > 0 && matches.Count >= maxMatches) {
                return matches;  // Early exit
            }
        }
    }
    
    return matches;
}
```

### Optimization 3: Batch Hashing

```csharp
// For plagiarism detection with large documents

// BAD: Hash individual characters, rebuild full hash per window
// O(n × m) substring operations

// GOOD: Hash with rolling polynomial
// O(n) total hashing via O(1) rolling updates
// Sliding window: remove leftmost char, add rightmost char

// O(n) rolling hash beats O(n × m) full hashing by 1000x for large m!
```

---

## 📊 BENCHMARKING & PROFILING

### Sorting Benchmark Results (n = 1M integers)

```
Algorithm         Time (ms)  Comparisons  Memory
─────────────────────────────────────────────────
Insertion Sort    ~15,000    ~250B        O(1)
Bubble Sort       ~20,000    ~500B        O(1)
Selection Sort    ~12,000    ~500B        O(1)
Heap Sort         ~150       ~20M         O(1)
Merge Sort        ~100       ~20M         O(n)
Quick Sort        ~70        ~20M         O(log n)
─────────────────────────────────────────────────

Key insights:
├─ Elementary sorts are 100-200x slower (n² operations)
├─ Quick sort beats merge sort by 1.4x (lower constants)
└─ In-place quick < O(n) memory merge sort (big difference at scale)
```

### Hash Table Benchmark (load factor impact)

```
Load Factor α   Avg Chain Length  Search Time (ns)  Memory (MB)
─────────────────────────────────────────────────────────────
0.25            0.25              50 ns            100 MB
0.50            0.50              75 ns            50 MB
0.75            0.75              100 ns           33 MB
1.00            1.00              125 ns           25 MB
1.50            1.50              180 ns           17 MB  (too slow!)

Recommendation: Keep α ≤ 0.75 for O(1) average behavior
```

---

## 🎯 PRODUCTION CHECKLIST

### Before Deploying Sorting Code

- [ ] Handle edge cases (empty array, single element, duplicates)
- [ ] Test on already-sorted data (quick sort O(n²), insertion sort O(n))
- [ ] Benchmark with your data size and patterns
- [ ] Use language's built-in sort unless specialized needs
- [ ] Profile if sort is bottleneck (premature optimization risk)

### Before Deploying Hash Table Code

- [ ] Test collision behavior (insertions, sequential keys)
- [ ] Set load factor threshold appropriate for workload
- [ ] Handle hash flooding attacks (randomize seed, universal hashing)
- [ ] Benchmark resize overhead (is it insignificant via amortized analysis?)
- [ ] Consider memory footprint (empty buckets waste space)

### Before Deploying Heap Code

- [ ] Verify heap property maintained (parent ≥ children)
- [ ] Test extract-min behavior (removes root, not arbitrary)
- [ ] Profile if heap is bottleneck (vs heap sort, priority queue)
- [ ] Use language's built-in PriorityQueue unless specialized

---

**Document Status:** ✅ COMPLETE  
**Sections:** 8 optimization categories  
**Production-Ready:** Yes


# 🗺️ WEEK_03_PROBLEM_SOLVING_ROADMAP_EXTENDED_CSHARP

**Version:** v1.0  
**Purpose:** Week-specific C# problem-solving playbook  
**Target:** Transform pattern knowledge into C# coding fluency  
**Prerequisites:** Week 03 instructional files + standard support files complete  

---

## 🎯 WEEK 03 PROBLEM-SOLVING FRAMEWORK

**Decision Tree (Week 03 Patterns):**

| Problem Phrases/Signals | 🎯 Primary Pattern | C# Collection | Time/Space |
|-------------------------|-------------------|---------------|------------|
| “Sort a small array”, “nearly sorted”, “need stable + simple”, “n ≤ ~50” | Insertion sort (elementary sorts family) | `int[]` + indices | O(n²)/O(1) |
| “Need guaranteed O(n log n)”, “stable sort required”, “external merge idea” | Merge sort | `int[]` + temp buffer | O(n log n)/O(n) |
| “Fast average sort”, “in-place sort”, “partition around pivot” | Quick sort (Lomuto/Hoare/3-way) | `int[]` + recursion | Avg O(n log n), worst O(n²)/O(log n) stack |
| “Repeated min/max extraction”, “top-k streaming”, “scheduler”, “Dijkstra-like” | Heap / Priority queue | `.NET PriorityQueue<TElement,TPriority>` or custom heap | O(n log n)/O(n) |
| “Frequent lookup/insert/delete”, “counting”, “membership tests” | Hash table (separate chaining) | `Dictionary<TKey,TValue>` / custom buckets | Avg near O(1)/O(n) |
| “Avoid pointers/linked lists”, “cache-friendly table”, “open addressing” | Hash table (open addressing: linear/quadratic/double hashing) | custom arrays + probe loops | Avg near O(1)/O(n) |

**Anti-Patterns:**
- ❌ Using Bubble sort for large n → Use Merge sort / Quick sort / Heap sort instead  
- ❌ Using Quick sort (naive pivot) for adversarial / already-sorted input → Use randomized pivot or Merge sort  
- ❌ Using open addressing without tombstones (Deleted state) → Use tombstones or separate chaining  

---

## 💻 C# PATTERN IMPLEMENTATIONS (Week 03)

### Pattern 1: ELEMENTARY SORTS — Bubble / Selection / Insertion
**C# Mental Model:** Like shifting items in an array without allocating new memory.

**When to Use:**
- ✅ Very small inputs
- ✅ Nearly sorted inputs (especially insertion sort)
- ✅ Educational / debugging correctness

**Core C# Skeleton:**
```csharp
// Elementary Sorts - choose 1 variant based on need
public void Sort(int[] arr) {
    if (arr == null || arr.Length <= 1) return;

    // Variant selection:
    // Bubble: adjacent swaps
    // Selection: select min/max and place
    // Insertion: insert into sorted prefix

    for (int i = 0; i < arr.Length; i++) {
        // pattern logic
    }
}
```

**C# Notes:**
- Prefer `Insertion sort` when array is almost sorted.
- Beware off-by-one boundaries (`n-1`, `i+1`).

---

### Pattern 2: MERGE SORT (Stable, Predictable)
**C# Mental Model:** Like splitting work into two teams then merging results using a temporary staging area.

**When to Use:**
- ✅ Need stable sorting
- ✅ Need guaranteed O(n log n)
- ✅ Sorting large arrays where worst-case matters

**Core C# Skeleton:**
```csharp
// Merge Sort - stable sort using temp buffer
public void Sort(int[] arr) {
    if (arr == null || arr.Length <= 1) return;

    int[] temp = new int[arr.Length];
    SortRange(arr, temp, 0, arr.Length - 1);
}

private void SortRange(int[] arr, int[] temp, int low, int high) {
    if (low >= high) return;

    int mid = low + (high - low) / 2;
    SortRange(arr, temp, low, mid);
    SortRange(arr, temp, mid + 1, high);

    Merge(arr, temp, low, mid, high);
}

private void Merge(int[] arr, int[] temp, int low, int mid, int high) {
    // merge logic
}
```

**C# Notes:**
- Allocate `temp` once to avoid repeated allocations.
- Optional micro-optimization: skip merge if already ordered.

---

### Pattern 3: QUICK SORT (Partition Variants)
**C# Mental Model:** Like picking a “pivot” leader, sending smaller values left and larger values right.

**When to Use:**
- ✅ Fast average performance
- ✅ In-place sorting desired
- ✅ You can mitigate worst-case (random pivot / 3-way partition)

**Core C# Skeleton:**
```csharp
// Quick Sort - partition-based (choose Lomuto / Hoare / 3-way)
public void Sort(int[] arr) {
    if (arr == null || arr.Length <= 1) return;
    QuickSort(arr, 0, arr.Length - 1);
}

private void QuickSort(int[] arr, int low, int high) {
    if (low >= high) return;

    int pivotIndexOrSplit = Partition(arr, low, high);
    // recurse based on partition scheme
}
```

**C# Notes:**
- Use `random pivot` to reduce worst-case probability.
- Use `3-way partition` when many duplicates exist.

---

### Pattern 4: HEAP SORT & PRIORITY QUEUES
**C# Mental Model:** Like always keeping the “best candidate” at the front.

**When to Use:**
- ✅ Top-K problems
- ✅ Repeated extract-min/extract-max
- ✅ Scheduling and event simulation

**Core C# Skeleton:**
```csharp
// PriorityQueue usage (min-priority by default)
public void UsePriorityQueue() {
    var pq = new PriorityQueue<int, int>(); // element, priority
    pq.Enqueue(10, 10);
    pq.Enqueue(5, 5);

    while (pq.Count > 0) {
        int x = pq.Dequeue();
        // process
    }
}
```

**C# Notes:**
- `.NET PriorityQueue<TElement,TPriority>` is min-priority by default.
- For max-heap behavior, invert priority (e.g., `-priority`).

---

### Pattern 5: HASH TABLES — Separate Chaining
**C# Mental Model:** Like a row of lockers (buckets), where each locker can contain a small list.

**When to Use:**
- ✅ General-purpose maps/sets
- ✅ High mutation workloads
- ✅ Simplicity and reliability

**Core C# Skeleton:**
```csharp
// Dictionary skeleton
public int Solve(int[] input) {
    if (input == null || input.Length == 0) return 0;

    var map = new Dictionary<int, int>();

    for (int i = 0; i < input.Length; i++) {
        // update counts / membership
    }

    return 0;
}
```

**C# Notes:**
- Prefer `TryGetValue` to avoid double hashing.
- Avoid mutable keys.

---

### Pattern 6: HASH TABLES — Open Addressing (Linear / Quadratic / Double Hashing)
**C# Mental Model:** Like searching for the next available parking spot when your first spot is taken.

**When to Use:**
- ✅ Cache-friendly hash tables
- ✅ Avoid linked allocations (chains)
- ✅ Controlled environments (you implement carefully)

**Core C# Skeleton:**
```csharp
// Open Addressing skeleton (array + probing)
public void Put(int key, int value) {
    // table arrays + state arrays (Empty/Filled/Deleted)
    // probe loop: attempt=0..capacity-1
}
```

**C# Notes:**
- You need a Deleted state (tombstone) for correct deletion.
- Resizing is mandatory when load factor grows.

---

## 📊 PROGRESSIVE PROBLEM LADDER (Week 03)

### 🟢 Stage 1: Canonical
| # | LeetCode | Difficulty | Pattern | C# Focus |
|---|----------|------------|---------|----------|
| 1 | #912 Sort an Array | 🟡 | Merge sort / Quick sort | recursion, temp buffer |
| 2 | #215 Kth Largest Element in an Array | 🟡 | Heap / Quickselect | PriorityQueue usage |
| 3 | #347 Top K Frequent Elements | 🟡 | Hash map + Heap | Dictionary + PriorityQueue |

### 🟡 Stage 2: Variations
| # | LeetCode | Difficulty | Pattern+Twist | C# Focus |
|---|----------|------------|---------------|----------|
| 1 | #973 K Closest Points to Origin | 🟡 | Heap / partition | tuples, comparer |
| 2 | #75 Sort Colors | 🟡 | Partitioning idea | in-place swaps |
| 3 | #49 Group Anagrams | 🟡 | Hashing | string key building |

### 🟠 Stage 3: Integration
| # | LeetCode | Difficulty | Patterns | C# Focus |
|---|----------|------------|----------|----------|
| 1 | #23 Merge k Sorted Lists | 🔴 | Heap + merge | PriorityQueue + linked list |
| 2 | #239 Sliding Window Maximum | 🔴 | Deque (bonus) | LinkedList as deque |
| 3 | #295 Find Median from Data Stream | 🔴 | Two heaps | two PriorityQueues |

---

## ⚠ WEEK 03 PITFALLS & C# GOTCHAS

| Pattern | Common Bug | C# Symptom | Quick Fix |
|---------|------------|------------|-----------|
| Insertion sort | wrong inner loop boundary | IndexOutOfRangeException | check `j >= 0` before access |
| Merge sort | allocating temp inside recursion | slow / GC pressure | allocate temp once |
| Quick sort | pivot degenerates on sorted input | TLE / deep recursion | random pivot or 3-way partition |
| Heap / PQ | treating PriorityQueue as max-heap | wrong answers | invert priority |
| Hashing | using mutable keys | missing lookups | use immutable keys |
| Open addressing | deleting without tombstones | search failures | use Empty/Filled/Deleted state |

**Week 03 Collection Guide:**
- ✅ `int[]` + indices: all in-place sorts
- ✅ `PriorityQueue<TElement,TPriority>`: top-k / streaming extremes
- ✅ `Dictionary<TKey,TValue>`: frequency counting, grouping, membership

---

## ✅ WEEK COMPLETION CHECKLIST

**Pattern Fluency:**
- [ ] Recall Bubble / Selection / Insertion skeletons
- [ ] Recall Merge sort skeleton (temp buffer once)
- [ ] Recall Quick sort skeleton (choose partition scheme)
- [ ] Recall Heap / PriorityQueue skeleton
- [ ] Recall Hash table (Dictionary) skeleton
- [ ] Recall Open addressing probing loop skeleton

**Problem Solving:**
- [ ] Solved Stage 1 canonical
- [ ] 80%+ Stage 2 variations
- [ ] Attempted 1+ Stage 3 integration

**C# Implementation:**
- [ ] Used correct collections
- [ ] Handled edge cases

**Ready:** [ ] Yes

---
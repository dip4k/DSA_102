# 🛠️ Week 03: Extended Support (C# Implementation)

## 📌 Introduction to Extended Support

**"Mental Models First, Code Second."**

The main instructional files for Week 3 focus on the _mechanics_ of Hashing and Recursion without getting bogged down in syntax. This file provides the **production-grade C# implementations** that complement those mental models.

### 🎯 How to Use This File

1. **Read the Instructional File** for the day to understand the _logic_.
2. **Trace the Logic** on paper.
3. **Consult this File** to see how that logic translates into efficient C# code.
4. **Run the Tests** to verify edge cases.

---

## 🧩 Day 1 & 2: Hashing & Hash Tables

### 💻 Canonical Implementation: Custom Hash Table (Chaining)

While you normally use `Dictionary<K,V>`, building one helps understand collisions.

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class SimpleHashTable<K, V>
{
    private struct Entry
    {
        public K Key;
        public V Value;
    }

    private LinkedList<Entry>[] _buckets;
    private int _capacity;
    private int _size;

    public SimpleHashTable(int capacity = 16)
    {
        _capacity = capacity;
        _buckets = new LinkedList<Entry>[capacity];
        _size = 0;
    }

    private int GetBucketIndex(K key)
    {
        // Use default HashCode and map to bucket index
        int hashCode = key.GetHashCode();
        int index = Math.Abs(hashCode) % _capacity;
        return index;
    }

    public void Put(K key, V value)
    {
        int index = GetBucketIndex(key);
        if (_buckets[index] == null)
        {
            _buckets[index] = new LinkedList<Entry>();
        }

        // Check if key exists (update)
        var bucket = _buckets[index];
        var node = bucket.First;
        while (node != null)
        {
            if (node.Value.Key.Equals(key))
            {
                // Update value (Note: Value types require replacement logic, simplified here)
                var entry = node.Value;
                entry.Value = value;
                node.Value = entry;
                return;
            }
            node = node.Next;
        }

        // Key not found, add new entry
        bucket.AddLast(new Entry { Key = key, Value = value });
        _size++;
    }

    public V Get(K key)
    {
        int index = GetBucketIndex(key);
        var bucket = _buckets[index];

        if (bucket != null)
        {
            foreach (var entry in bucket)
            {
                if (entry.Key.Equals(key))
                    return entry.Value;
            }
        }

        throw new KeyNotFoundException($"Key '{key}' not found.");
    }
}
```

### 🧪 Unit Tests (NUnit Style)

```csharp
[Test]
public void TestHashTable_Operations()
{
    var map = new SimpleHashTable<string, int>();

    // Test Put & Get
    map.Put("Alice", 25);
    map.Put("Bob", 30);
    Assert.AreEqual(25, map.Get("Alice"));

    // Test Update
    map.Put("Alice", 26);
    Assert.AreEqual(26, map.Get("Alice"));

    // Test Collision Handling (dependent on capacity/hash, theoretically)
    // Assuming "Key1" and "Key2" might collide or just distinct storage
    map.Put("Key1", 100);
    map.Put("Key2", 200);
    Assert.AreEqual(100, map.Get("Key1"));
}
```

### 🐞 Common Pitfalls

-   **Mutable Keys:** Never use a mutable object (like a `List<int>` or a class with changeable fields) as a key. If the hash code changes after insertion, the item is lost forever in the wrong bucket.
-   **Abs(HashCode):** `GetHashCode()` can return negative values. Always use `Math.Abs()` before modulo.

---

## 🧩 Day 3: Recursion & Stack Frames

### 💻 Implementation: Fibonacci (Naive vs Memoized)

```csharp
public class RecursionPatterns
{
    // 🔴 Naive Recursion: O(2^n) - Exponential Time
    // Do NOT use for n > 40
    public static long FibonacciNaive(int n)
    {
        if (n <= 1) return n;
        return FibonacciNaive(n - 1) + FibonacciNaive(n - 2);
    }

    // 🟢 Tail Recursion (Helper Pattern): O(n)
    // C# compiler doesn't guarantee tail-call optimization,
    // but this structure prevents stack growth logic-wise.
    public static long FibonacciTail(int n)
    {
        return FibHelper(n, 0, 1);
    }

    private static long FibHelper(int n, long a, long b)
    {
        if (n == 0) return a;
        if (n == 1) return b;
        return FibHelper(n - 1, b, a + b);
    }
}
```

### 🧠 Deep Dive: The Call Stack

For `FibonacciNaive(4)`, the stack grows:

1. `Fib(4)` calls `Fib(3)`
2. `Fib(3)` calls `Fib(2)`
3. `Fib(2)` calls `Fib(1)` -> returns 1
   ...
   Code helps verify that _no state is shared_ between stack frames unless passed explicitly.

---

## 🧩 Day 4: Basic Sorting (Insertion Sort)

### 💻 Implementation: Insertion Sort

Why Insertion Sort? It's **stable**, **adaptive** (O(n) for nearly sorted), and efficient for small datasets (N < 50).

```csharp
public static void InsertionSort(int[] arr)
{
    // Start from 2nd element (index 1)
    for (int i = 1; i < arr.Length; i++)
    {
        int key = arr[i];
        int j = i - 1;

        // Move elements of arr[0..i-1], that are greater than key,
        // to one position ahead of their current position
        while (j >= 0 && arr[j] > key)
        {
            arr[j + 1] = arr[j];
            j--;
        }
        arr[j + 1] = key;
    }
}
```

### 🧪 Unit Tests

```csharp
[Test]
public void TestInsertionSort()
{
    int[] input = { 5, 2, 9, 1, 5, 6 };
    InsertionSort(input);

    // Assert Sorted
    Assert.IsTrue(input.SequenceEqual(new int[] { 1, 2, 5, 5, 6, 9 }));

    // Assert Stability (Implicit in logic, harder to test with ints)
    // Logic: We only swap if arr[j] > key. If arr[j] == key, we stop.
    // This preserves original order of duplicates.
}
```

---

## 🧩 Day 5: Time Complexity in Practice

### 💻 Benchmarking Code

Use `System.Diagnostics.Stopwatch` to verify O(N) vs O(N^2).

```csharp
using System.Diagnostics;

public static void MeasureRuntime()
{
    int size = 10000;
    int[] data = Enumerable.Range(0, size).Reverse().ToArray(); // Worst case

    Stopwatch sw = Stopwatch.StartNew();
    InsertionSort(data); // O(N^2)
    sw.Stop();
    Console.WriteLine($"Insertion Sort (N={size}): {sw.ElapsedMilliseconds} ms");

    // Compare with Array.Sort (Quick/Introsort - O(N log N))
    data = Enumerable.Range(0, size).Reverse().ToArray();
    sw.Restart();
    Array.Sort(data);
    sw.Stop();
    Console.WriteLine($"Array.Sort (N={size}): {sw.ElapsedMilliseconds} ms");
}
```

### 📊 Expected Output (Approx)

-   Insertion Sort (N=10,000): ~150 ms
-   Array.Sort (N=10,000): ~1 ms
-   _Lesson:_ Logic complexity (Big-O) dominates hardware speed.

---

## ⚔️ C# Features for Interview Success

1.  **`Dictionary<TKey, TValue>.TryGetValue`**

    -   **Why:** Avoids double hashing (once for ContainsKey, once for indexer).
    -   **Pattern:**
        ```csharp
        if (map.TryGetValue(target - nums[i], out int index)) {
            return new [] { index, i };
        }
        ```

2.  **`HashSet<T>`**

    -   **Why:** O(1) lookups for "seen" elements.
    -   **Note:** `Set.Add()` returns `bool` (true if added, false if already exists).
    -   **Pattern:** `if (!seen.Add(curr)) { /* found duplicate */ }`

3.  **Tuples `(a, b)`**
    -   **Why:** Return multiple values without custom classes.
    -   **Pattern:** `return (min, max);`

---

---

# Week_03_Extended_Support_CSharp.md

## 🧭 Decision Tree – Week 3 Sorting & Hashing (C#)

```text
Incoming Problem → Ask:

1) "Need ordering? min/max? k-th?"
      → Sorting / Heap pattern

2) "Need membership / frequency / mapping?"
      → Hash Table pattern

3) "Need continuous best element under updates?"
      → Priority Queue (Heap) pattern
```

### Week 3 Patterns

| Problem Phrase / Signal                                  | Primary Pattern         | C# Collection        | Time / Space (Typical)      |
| -------------------------------------------------------- | ----------------------- | -------------------- | --------------------------- |
| “Sort small list”, “almost sorted”, “educational sort”   | Elementary Sorts        | `int[]`, loops       | O(n²) / O(1)                |
| “Large sort”, “stable sort”, “guaranteed n log n”        | Merge Sort              | arrays + temp buffer | O(n log n) / O(n)           |
| “In-place, average fast, large n”                        | Quick Sort              | arrays               | O(n log n) avg / O(1) stack |
| “Always get min/max quickly”, “task scheduling”, “top-k” | Binary Heap / Heap Sort | `List<T>` + heap ops | O(log n) ops / O(n) build   |
| “Count, group, membership, map keys to values”           | Hash Table – Chaining   | `Dictionary<K,V>`    | O(1) avg / O(n) worst       |
| “Dense insert/find without extra lists, open slots”      | Open Address Hashing    | custom `Entry[]`     | O(1) avg / O(n) worst       |

### Anti-Patterns (Week 3)

-   Week 3 mistake 1: Using `List<T>.Sort()` blindly when problem needs step-by-step sort logic → Use explicit **Merge Sort / Quick Sort skeletons** instead.
-   Week 3 mistake 2: Using `List<T>` linear scans for membership when keys are natural hash keys → Use **Dictionary / HashSet** or custom open addressing table.

---

## 🧱 Week 3 Problem-Solving Framework (C#)

### C# Mental Model

-   Sorting: “Rearrange `int[]` using swaps, comparisons, and partitions.”
-   Hashing: “Map `K` to index via `GetHashCode()` and resolve collisions with **chains** or **probing**.”

### When to Use

-   Sorting patterns:
    -   Scenario 1: Need global order or relative ranking.
    -   Scenario 2: Need k-th element or to binary-search later.
-   Hashing patterns:
    -   Scenario 1: Need fast membership / frequency lookup.
    -   Scenario 2: Need map from arbitrary key to value (string → count).

---

### Core C# Skeleton – Pattern 1: Elementary Sorts

**1-line purpose:** Sort small or nearly-sorted arrays with simple O(n²) mechanics.

```csharp
public void InsertionSort(int[] arr)
{
    if (arr == null || arr.Length <= 1) return;

    for (int i = 1; i < arr.Length; i++)
    {
        int key = arr[i];
        int j = i - 1;

        while (j >= 0 && arr[j] > key)
        {
            arr[j + 1] = arr[j];
            j--;
        }
        arr[j + 1] = key;
    }
}
```

**C# Notes**

-   Use for `n <= 50` or “almost sorted” inputs.
-   Stable by default because `==` does not move elements.

---

### Core C# Skeleton – Pattern 2: Merge Sort

**1-line purpose:** Stable O(n log n) sort using divide–merge with extra buffer.

```csharp
public void MergeSort(int[] arr)
{
    if (arr == null || arr.Length <= 1) return;
    int[] temp = new int[arr.Length];
    MergeSort(arr, temp, 0, arr.Length - 1);
}

private void MergeSort(int[] arr, int[] temp, int left, int right)
{
    if (left >= right) return;

    int mid = left + (right - left) / 2;
    MergeSort(arr, temp, left, mid);
    MergeSort(arr, temp, mid + 1, right);

    if (arr[mid] <= arr[mid + 1]) return; // optimization

    Merge(arr, temp, left, mid, right);
}

private void Merge(int[] arr, int[] temp, int left, int mid, int right)
{
    for (int i = left; i <= right; i++)
        temp[i] = arr[i];

    int iLeft = left, iRight = mid + 1, k = left;

    while (iLeft <= mid && iRight <= right)
    {
        if (temp[iLeft] <= temp[iRight])
            arr[k++] = temp[iLeft++];
        else
            arr[k++] = temp[iRight++];
    }

    while (iLeft <= mid)
        arr[k++] = temp[iLeft++];
}
```

**C# Notes**

-   Use when stability matters (e.g., sort by key then secondary key).
-   Use shared `temp` array to reduce allocations.

---

### Core C# Skeleton – Pattern 3: Quick Sort

**1-line purpose:** In-place average O(n log n) sort using partition around pivot.

```csharp
public void QuickSort(int[] arr)
{
    if (arr == null || arr.Length <= 1) return;
    QuickSort(arr, 0, arr.Length - 1);
}

private void QuickSort(int[] arr, int left, int right)
{
    if (left >= right) return;

    int pivotIndex = PartitionHoare(arr, left, right);
    QuickSort(arr, left, pivotIndex);
    QuickSort(arr, pivotIndex + 1, right);
}

private int PartitionHoare(int[] arr, int left, int right)
{
    int pivot = arr[left + (right - left) / 2];
    int i = left - 1;
    int j = right + 1;

    while (true)
    {
        do { i++; } while (arr[i] < pivot);
        do { j--; } while (arr[j] > pivot);

        if (i >= j) return j;

        (arr[i], arr[j]) = (arr[j], arr[i]);
    }
}
```

**C# Notes**

-   Hoare partition is robust; always ensure recursive ranges are `[left..pivotIndex]` and `[pivotIndex+1..right]`.
-   Randomized pivot or median-of-three mitigates worst-case.

---

### Core C# Skeleton – Pattern 4: Binary Heap & Heap Sort

**1-line purpose:** Maintain min/max at root for fast extraction and heap-based sorting.

```csharp
public class MaxHeap
{
    private readonly List<int> _data = new List<int>();

    public int Count => _data.Count;

    public void Push(int value)
    {
        _data.Add(value);
        SiftUp(Count - 1);
    }

    public int Pop()
    {
        if (Count == 0) throw new InvalidOperationException("Empty heap");
        int root = _data[0];
        _data[0] = _data[Count - 1];
        _data.RemoveAt(Count - 1);
        if (Count > 0) SiftDown(0);
        return root;
    }

    private void SiftUp(int index)
    {
        while (index > 0)
        {
            int parent = (index - 1) / 2;
            if (_data[parent] >= _data[index]) break;
            (_data[parent], _data[index]) = (_data[index], _data[parent]);
            index = parent;
        }
    }

    private void SiftDown(int index)
    {
        int last = Count - 1;
        while (true)
        {
            int left = index * 2 + 1;
            int right = index * 2 + 2;
            int largest = index;

            if (left <= last && _data[left] > _data[largest])
                largest = left;
            if (right <= last && _data[right] > _data[largest])
                largest = right;

            if (largest == index) break;

            (_data[index], _data[largest]) = (_data[largest], _data[index]);
            index = largest;
        }
    }
}
```

**C# Notes**

-   Use `MaxHeap` for “get largest quickly” scenarios; use `MinHeap` variant for smallest.
-   Heap sort: build heap then repeatedly `Pop()` into array from end to start.

---

### Core C# Skeleton – Pattern 5: Hash Table (Separate Chaining)

**1-line purpose:** Map key → value using buckets with linked lists to resolve collisions.

```csharp
public class ChainedHashMap<TKey, TValue>
{
    private class Entry
    {
        public TKey Key;
        public TValue Value;
    }

    private readonly LinkedList<Entry>[] _buckets;
    private readonly IEqualityComparer<TKey> _comparer;

    public ChainedHashMap(int capacity = 16, IEqualityComparer<TKey> comparer = null)
    {
        _buckets = new LinkedList<Entry>[capacity];
        _comparer = comparer ?? EqualityComparer<TKey>.Default;
    }

    private int IndexFor(TKey key)
    {
        int hash = _comparer.GetHashCode(key) & 0x7FFFFFFF;
        return hash % _buckets.Length;
    }

    public void Put(TKey key, TValue value)
    {
        int index = IndexFor(key);
        _buckets[index] ??= new LinkedList<Entry>();

        foreach (var node in _buckets[index])
        {
            if (_comparer.Equals(node.Key, key))
            {
                node.Value = value;
                return;
            }
        }

        _buckets[index].AddLast(new Entry { Key = key, Value = value });
    }

    public bool TryGet(TKey key, out TValue value)
    {
        int index = IndexFor(key);
        var bucket = _buckets[index];
        if (bucket != null)
        {
            foreach (var node in bucket)
            {
                if (_comparer.Equals(node.Key, key))
                {
                    value = node.Value;
                    return true;
                }
            }
        }
        value = default!;
        return false;
    }
}
```

**C# Notes**

-   Always use `IEqualityComparer<TKey>` to keep hashing and equality consistent.
-   Control load factor externally (no resizing here; you can wrap it with a rehash policy).

---

### Core C# Skeleton – Pattern 6: Hash Table (Open Addressing – Linear Probing)

**1-line purpose:** Resolve collisions by probing subsequent slots in an array.

```csharp
public class LinearProbingHashSet
{
    private enum SlotState : byte { Empty, Occupied, Deleted }

    private int[] _keys;
    private SlotState[] _state;
    private int _count;

    public LinearProbingHashSet(int capacity = 16)
    {
        _keys = new int[capacity];
        _state = new SlotState[capacity];
    }

    private int IndexFor(int key)
    {
        int hash = key.GetHashCode() & 0x7FFFFFFF;
        return hash % _keys.Length;
    }

    public bool Add(int key)
    {
        if (_count * 2 >= _keys.Length) Resize();

        int idx = IndexFor(key);

        for (int i = 0; i < _keys.Length; i++)
        {
            int probe = (idx + i) % _keys.Length;

            if (_state[probe] == SlotState.Empty || _state[probe] == SlotState.Deleted)
            {
                _keys[probe] = key;
                _state[probe] = SlotState.Occupied;
                _count++;
                return true;
            }

            if (_state[probe] == SlotState.Occupied && _keys[probe] == key)
            {
                return false; // already present
            }
        }

        return false;
    }

    public bool Contains(int key)
    {
        int idx = IndexFor(key);

        for (int i = 0; i < _keys.Length; i++)
        {
            int probe = (idx + i) % _keys.Length;
            if (_state[probe] == SlotState.Empty) return false;
            if (_state[probe] == SlotState.Occupied && _keys[probe] == key) return true;
        }

        return false;
    }

    private void Resize()
    {
        var oldKeys = _keys;
        var oldState = _state;
        _keys = new int[oldKeys.Length * 2];
        _state = new SlotState[_keys.Length];
        _count = 0;

        for (int i = 0; i < oldKeys.Length; i++)
        {
            if (oldState[i] == SlotState.Occupied)
                Add(oldKeys[i]);
        }
    }
}
```

**C# Notes**

-   Linear probing is simple but prone to primary clustering; quadratic probing and double hashing mitigate this.
-   `Deleted` state helps avoid breaking search chains.

---

## 🔧 C# Pattern Implementations – Week 3

### Week 3 – Pattern: Sorting (Elementary + n log n)

| LeetCode / Task              | Difficulty | Pattern          | C# Focus                          |
| ---------------------------- | ---------: | ---------------- | --------------------------------- |
| Sort Colors                  |       Easy | Counting / 3-way | In-place partition                |
| Insertion Sort List          |     Medium | Insertion Sort   | Linked list pointer manipulation  |
| Merge Intervals              |     Medium | Sort + Merge     | `Array.Sort` with custom comparer |
| Sort Characters by Frequency |     Medium | Sort by freq     | `Dictionary<char,int>` then sort  |

### Week 3 – Pattern: Heap / Priority Queue

| LeetCode / Task              | Difficulty | Pattern       | C# Focus                          |
| ---------------------------- | ---------: | ------------- | --------------------------------- |
| Kth Largest Element in Array |     Medium | Heap / Quick  | `PriorityQueue` or custom MaxHeap |
| Top K Frequent Elements      |     Medium | Bucket / Heap | `Dictionary` + MinHeap            |
| Merge k Sorted Lists         |       Hard | MinHeap       | `PriorityQueue<ListNode,int>`     |

### Week 3 – Pattern: Hashing (Chaining / Open Addressing)

| LeetCode / Task       | Difficulty | Pattern       | C# Focus                          |
| --------------------- | ---------: | ------------- | --------------------------------- |
| Two Sum               |       Easy | Hash Map      | `Dictionary<int,int>` indices     |
| Group Anagrams        |     Medium | Hashing key   | `Dictionary<string,List<string>>` |
| Subarray Sum Equals K |     Medium | Prefix + Hash | `Dictionary<int,int>` frequency   |

---

## 📈 Progressive Problem Ladder – Week 3

### Stage 1 – Canonical

| #   | Problem / Task                                | Pattern          | C# Note                      |
| --- | --------------------------------------------- | ---------------- | ---------------------------- |
| 1   | Implement Bubble / Selection / Insertion Sort | Elementary Sorts | Practice swaps, loops        |
| 2   | Implement Merge Sort on `int[]`               | Merge Sort       | Shared temp buffer           |
| 3   | Implement Quick Sort with Hoare partition     | Quick Sort       | Correct subranges            |
| 4   | Implement Max Heap and Heap Sort              | Heap             | SiftUp, SiftDown correctness |

### Stage 2 – Variations

| #   | Problem / Task           | Patterns           | C# Focus                     |
| --- | ------------------------ | ------------------ | ---------------------------- |
| 1   | Sort Colors (Dutch Flag) | 3-way partition    | Index invariants             |
| 2   | Kth Largest Element      | Heap / QuickSelect | Partial sort vs full sort    |
| 3   | Group Anagrams           | Sort + Hash        | Normalized key / char counts |
| 4   | Top K Frequent Elements  | Hash + Heap        | Custom comparer or PQ        |

### Stage 3 – Integration

| Pattern    | Common Bug                          | C# Symptom                      | Quick Fix                                 |
| ---------- | ----------------------------------- | ------------------------------- | ----------------------------------------- |
| Merge Sort | Extra allocations per recursion     | GC pressure, slow on big arrays | Reuse single temp buffer                  |
| Quick Sort | Infinite recursion / stack overflow | Wrong partition boundaries      | Ensure `[left..p]` and `[p+1..right]`     |
| Heap       | Wrong parent/child index math       | Heap property violated          | Use `2*i+1`, `2*i+2`, `(i-1)/2` exactly   |
| Hashing    | Mutable reference as key            | Keys “disappear” after mutation | Use immutable keys, avoid changing fields |

---

## 🧨 Week 3 Pitfalls & C# Gotchas

-   Using `List<T>.Sort()` when the interview expects manual quick/merge sort skeleton.
-   Forgetting to use `Math.Abs(hashCode)` or bitmask `& 0x7FFFFFFF` before modulo.
-   Using custom reference types as dictionary keys without overriding `Equals` and `GetHashCode`.
-   Not resizing open-address tables → performance decays to O(n).

---

## ✅ Week 3 Completion Checklist (C#)

Pattern Fluency

-   [ ] Recall elementary sort skeletons (Bubble / Selection / Insertion).
-   [ ] Recall Merge Sort skeleton (split, merge with temp).
-   [ ] Recall Quick Sort skeleton (partition, recursive ranges).
-   [ ] Recall basic Heap implementation (`Push`, `Pop`).
-   [ ] Recall Chained and Linear Probing hash map skeletons.

Problem Solving

-   [ ] Solved Stage 1 canonical problems in C#.
-   [ ] Reached at least 60–80% of Stage 2 variations.

C# Implementation

-   [ ] Used correct collections (`int[]`, `List<T>`, `Dictionary<K,V>`).
-   [ ] Avoided redundant allocations in recursive sorts.
-   [ ] Handled edge cases (empty arrays, already sorted arrays).

**Ready:** Week 3 Problem-Solving Roadmap Extended C# complete, matching Sorting & Hashing focus from syllabus v10.[4]

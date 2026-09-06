# 🗺️ Week_03_Extended_CSharp_Problem_Solving_Implementation — COMPLETE v13

**Week:** 3 – Foundations III: Sorting, Heaps & Hashing  
**Purpose:** Master sorting algorithms, heaps, and hash tables through pattern recognition, understanding, and practice  
**Target:** Transform Week 3 topics into interview-ready C# coding skills  
**Prerequisites:** Week 3 instructional files + standard support files complete

---

## 📚 WEEK 3 OVERVIEW

**Primary Goal:** Understand internal mechanics and trade-offs of sorting algorithms, heaps, and hash tables, including string hashing.

**Why This Week Comes Here:** Sorting and hashing are fundamental primitives; heaps introduce tree-like structure with strong performance guarantees.

**Topics by Day:**
- **Day 1:** Elementary Sorts (Bubble, Selection, Insertion) — O(n²) but small constants
- **Day 2:** Merge Sort & Quick Sort — Divide-and-conquer with O(n log n) guarantees
- **Day 3:** Heaps, Heapify & Heap Sort — Array-based tree structure with O(log n) operations
- **Day 4:** Hash Tables I: Separate Chaining — O(1) expected time with linked lists
- **Day 5:** Hash Tables II: Open Addressing & Rolling Hash (Rabin-Karp) — Probing strategies + string hashing

---

## SECTION 1️⃣: PATTERN RECOGNITION FRAMEWORK

### 🎯 Decision Tree — How to Identify & Choose Week 3 Patterns

Use this decision tree when you encounter a problem in Week 3:

| 🔍 Problem Phrases/Signals | 🎯 Pattern Name | ❓ Why This Pattern? | 💻 C# Collection | ⏱️ Time/Space |
|---|---|---|---|---|
| "Sort array", "Small n<50", "Nearly sorted", "Stable sort needed" | **Elementary Sorts (Bubble/Selection/Insertion)** | Small constant factors; O(n²) but often faster than O(n log n) for tiny inputs | `int[]`, hybrid | O(n²) / O(1) |
| "Stable sort", "Guaranteed O(n log n)", "External sorting", "Merge requirement" | **Merge Sort** | Divide-conquer guaranteed O(n log n); stable; external sorting benchmark | `int[]` + auxiliary | O(n log n) / O(n) |
| "Fast average case", "In-place", "Cache-friendly", "Adversarial input unlikely" | **Quick Sort** | Expected O(n log n) with O(log n) space; randomized pivot avoids O(n²) worst | `int[]` | O(n log n) avg / O(log n) |
| "Worst-case guarantee needed", "Adaptive algorithm", "Hybrid approach" | **Intro Sort (Hybrid)** | Quick sort → heap sort if depth exceeds limit; O(n log n) guaranteed | `int[]` | O(n log n) worst / O(log n) |
| "Priority queue", "K largest/smallest", "Dijkstra algorithm", "Event simulation" | **Heap (Min/Max)** | O(log n) insert/extract with O(1) peek; array-based tree | Custom `MinHeap<T>` or `PriorityQueue<T>` | O(log n) / O(n) |
| "Fast lookup", "Insert/delete O(1)", "Cache working set", "Expected O(1)" | **Hash Table (Separate Chaining)** | Linked lists in buckets; good hash + moderate load factor → O(1) expected | `Dictionary<K,V>` or custom | O(1) expected / O(n) |
| "Probing-based", "Cache locality", "No linked lists", "Load factor control" | **Hash Table (Open Addressing)** | Linear/quadratic/double hashing; fewer allocations; better cache | `Dictionary<K,V>` internal | O(1) expected / O(n) |
| "Substring search", "Pattern matching", "Plagiarism detection", "DNA sequences" | **Rolling Hash (Rabin-Karp)** | O(1) sliding window updates; polynomial hash modulo prime | `string` + hash | O(n+m) / O(1) |

**HOW TO READ THIS:**
- See "[Signal X]" in a problem? IMMEDIATELY think "[Pattern Y]"
- Ask yourself: "Why does this pattern solve it?" → Internalize the reasoning
- Check recommended collection → Learn why it's optimal

---

## SECTION 2️⃣: ANTI-PATTERNS — WHEN NOT TO USE & WHAT FAILS

### ⚠️ Week 3 Common Mistakes & Correct Alternatives

Learn WHAT NOT TO DO and WHY:

| ❌ Wrong Approach | 💥 Why It Fails | ⚡ Symptom/Consequence | ✅ Correct Alternative |
|---|---|---|---|
| Sorting large array with Bubble Sort | O(n²) is catastrophic for n>1000; simple swaps become bottleneck | Timeout on large inputs; 1M elements takes 10¹² operations | Use Merge Sort (guaranteed O(n log n)) or Quick Sort (fast average) |
| Quick Sort without pivot randomization | Adversarial input (reverse sorted) triggers O(n²) worst case | Stack overflow or timeout on malicious input | Randomize pivot selection OR use Intro Sort (fallback to heap sort) |
| Not maintaining heap invariant during insert/extract | Heap property violated; operations become O(n) | "Peek" gives wrong min/max; sort fails | Always bubble up after insert, bubble down after extract |
| Hash table with poor hash function | Clustered collisions; chains become O(n) long | "O(1) insert" degrades to O(n); lookup timeout | Use good hash (uniform distribution, randomized seed) |
| Hash table without resizing | Load factor grows unbounded; collisions explode | Performance degrades from O(1) to O(n) after ~70% full | Resize buckets (double) and rehash when load factor >0.75 |
| String substring search with naive O(n*m) | Repeated pattern matching redundant; catastrophic backtracking | TLE on long strings with patterns; 1M string vs 1K pattern → 10⁹ ops | Use Rabin-Karp rolling hash (O(n+m)) |
| Merge Sort without auxiliary array copy | Merging in-place requires complex logic; easy to corrupt | Wrong merged order; in-place merge O(n log n) space is a myth | Use auxiliary array (O(n) space); copy is cheaper than bugs |
| Heap sort with unstable comparison | Heap is inherently unstable; if stability needed, breaks | Equal elements change order (wrong for secondary sorts) | Use Merge Sort if stability required; heaps are fine for numeric sorts |

**ANTI-PATTERN LESSON:**
Understand not just the pattern, but understand its boundaries and when to choose alternatives.

---

## SECTION 3️⃣: PATTERN IMPLEMENTATIONS (Production-Grade)

### Pattern 1: Merge Sort (Stable, O(n log n) Guaranteed)

#### 🧠 Mental Model
Divide the array in half recursively until each piece is 1 element (trivially sorted). Merge two sorted halves by comparing front elements and taking the smaller one. The key insight: merging two sorted arrays is O(n) and stable.

#### ✅ When to Use This Pattern
- ✅ Guaranteed O(n log n) needed (worst-case critical).
- ✅ Stability required (equal elements preserve original order).
- ✅ External sorting (data doesn't fit in memory).
- ✅ When predictable performance > raw speed.

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Merge Sort - Stable O(n log n) guaranteed
/// Time: O(n log n) worst-case | Space: O(n) auxiliary
/// 
/// 🧠 MENTAL MODEL:
/// Divide-and-conquer: split in half, recursively sort, merge.
/// Merging two sorted halves is O(n) and stable (preserves equal order).
/// </summary>
public class MergeSortImplementation
{
    public static void Sort<T>(T[] arr) where T : IComparable<T>
    {
        // STEP 1: Guard - empty or single element
        if (arr == null || arr.Length <= 1) return;
        
        // STEP 2: Auxiliary array for merging
        T[] aux = new T[arr.Length];
        
        // STEP 3: Recursively sort and merge
        MergeSortHelper(arr, 0, arr.Length - 1, aux);
    }
    
    private static void MergeSortHelper<T>(T[] arr, int left, int right, T[] aux) 
        where T : IComparable<T>
    {
        // Base case: single element already sorted
        if (left >= right) return;
        
        // STEP 1: Find middle point
        int mid = left + (right - left) / 2;
        
        // STEP 2: Recursively sort left half
        MergeSortHelper(arr, left, mid, aux);
        
        // STEP 3: Recursively sort right half
        MergeSortHelper(arr, mid + 1, right, aux);
        
        // STEP 4: Merge sorted halves
        Merge(arr, left, mid, right, aux);
    }
    
    private static void Merge<T>(T[] arr, int left, int mid, int right, T[] aux) 
        where T : IComparable<T>
    {
        // STEP 1: Copy data to auxiliary array
        for (int i = left; i <= right; i++)
        {
            aux[i] = arr[i];
        }
        
        // STEP 2: Merge back - two pointers: left and right halves
        int i = left;      // Start of left half
        int j = mid + 1;   // Start of right half
        int k = left;      // Current position in original array
        
        // STEP 3: Compare and merge while both halves have elements
        while (i <= mid && j <= right)
        {
            // CRITICAL: Use <= for stability (preserves order of equal elements)
            if (aux[i].CompareTo(aux[j]) <= 0)
            {
                arr[k++] = aux[i++];  // Left is smaller or equal
            }
            else
            {
                arr[k++] = aux[j++];  // Right is smaller
            }
        }
        
        // STEP 4: Copy remaining elements from left half
        while (i <= mid)
        {
            arr[k++] = aux[i++];
        }
        
        // STEP 5: Remaining right elements already in place
        // (copying not necessary since they're in arr[k..right])
    }
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Use `<=` in merge comparison to maintain stability. `<` breaks it.
- 🟡 **PERFORMANCE:** O(n log n) guaranteed but slower than Quick Sort on average due to data movement.
- 🟢 **BEST PRACTICE:** Allocate auxiliary array once; pass to all recursive calls to avoid repeated allocation.

---

### Pattern 2: Quick Sort (Fast Average, Randomized Pivot)

#### 🧠 Mental Model
Choose a pivot element and partition the array so elements < pivot are left, > pivot are right. Recursively sort each partition. Average O(n log n) but O(n²) on bad pivots. Randomizing pivot selection defeats adversarial inputs.

#### ✅ When to Use This Pattern
- ✅ Fast average-case sorting needed (beats merge sort in practice).
- ✅ In-place sorting preferred (O(log n) space vs O(n)).
- ✅ Cache-friendly access patterns (partition involves local moves).
- ✅ Adversarial input possible (randomized pivot avoids O(n²)).

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
using System;

/// <summary>
/// Quick Sort - Fast average O(n log n), randomized pivot
/// Time: O(n log n) average, O(n²) worst | Space: O(log n) stack
/// 
/// 🧠 MENTAL MODEL:
/// Pick pivot, partition into < pivot and > pivot.
/// Recursively sort each side.
/// Randomized pivot defeats adversarial inputs.
/// </summary>
public class QuickSortImplementation
{
    private static readonly Random Rand = new();
    
    public static void Sort<T>(T[] arr) where T : IComparable<T>
    {
        // STEP 1: Guard - empty or single element
        if (arr == null || arr.Length <= 1) return;
        
        // STEP 2: Randomize to defeat adversarial inputs
        // Shuffle array randomly so pivot selection is unpredictable
        for (int i = arr.Length - 1; i > 0; i--)
        {
            int j = Rand.Next(0, i + 1);
            // Swap arr[i] and arr[j]
            (arr[i], arr[j]) = (arr[j], arr[i]);
        }
        
        // STEP 3: Sort
        QuickSortHelper(arr, 0, arr.Length - 1);
    }
    
    private static void QuickSortHelper<T>(T[] arr, int low, int high) 
        where T : IComparable<T>
    {
        // Base case: single element
        if (low < high)
        {
            // STEP 1: Partition and get pivot position
            int pivot = Partition(arr, low, high);
            
            // STEP 2: Recursively sort left partition
            QuickSortHelper(arr, low, pivot - 1);
            
            // STEP 3: Recursively sort right partition
            QuickSortHelper(arr, pivot + 1, high);
        }
    }
    
    private static int Partition<T>(T[] arr, int low, int high) 
        where T : IComparable<T>
    {
        // STEP 1: Choose pivot (last element)
        T pivot = arr[high];
        
        // STEP 2: Two pointers: one from left, one at end
        int i = low - 1;  // Pointer for smaller elements
        
        // STEP 3: Scan from left to high-1
        for (int j = low; j < high; j++)
        {
            // If current element < pivot, include in left partition
            if (arr[j].CompareTo(pivot) < 0)
            {
                i++;
                // Swap arr[i] and arr[j]
                (arr[i], arr[j]) = (arr[j], arr[i]);
            }
        }
        
        // STEP 4: Place pivot in correct position
        (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
        
        return i + 1;  // Return pivot position
    }
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Randomize array before sorting to avoid O(n²) on reverse-sorted or repeating input.
- 🟡 **PERFORMANCE:** In-place partition is cache-efficient; fewer data copies than merge sort.
- 🟢 **BEST PRACTICE:** Use randomized quick sort for production; beats merge sort 80% of the time despite worse worst-case.

---

### Pattern 3: Min-Heap (Priority Queue Implementation)

#### 🧠 Mental Model
Array-based binary tree where parent ≤ children (min-heap). Array index relationships: left child = 2i+1, right = 2i+2, parent = (i-1)/2. Insert: add to end, bubble up. Extract-min: swap root with last, remove, bubble down.

#### ✅ When to Use This Pattern
- ✅ Priority queue needed (Dijkstra, event simulation).
- ✅ K largest/smallest elements (build heap, extract K times).
- ✅ Median finding (two heaps: max-heap for smaller half, min-heap for larger).
- ✅ External sorting (heap sort uses O(1) extra space).

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
using System.Collections.Generic;

/// <summary>
/// Min Heap - O(log n) insert/extract, O(1) peek
/// Time: Insert/Extract O(log n), Peek O(1) | Space: O(n)
/// 
/// 🧠 MENTAL MODEL:
/// Complete binary tree in array: parent ≤ children.
/// Insert at end, bubble up. Extract root, move last to root, bubble down.
/// </summary>
public class MinHeap<T> where T : IComparable<T>
{
    private List<T> heap;
    
    public MinHeap()
    {
        heap = new List<T>();
    }
    
    public MinHeap(T[] arr)
    {
        heap = new List<T>(arr);
        // Build heap from existing array - O(n)
        for (int i = heap.Count / 2 - 1; i >= 0; i--)
        {
            BubbleDown(i);
        }
    }
    
    /// <summary>
    /// Insert element - O(log n)
    /// Add to end, bubble up until heap property restored
    /// </summary>
    public void Insert(T value)
    {
        heap.Add(value);
        BubbleUp(heap.Count - 1);
    }
    
    /// <summary>
    /// Extract minimum - O(log n)
    /// Swap root with last, remove, bubble down
    /// </summary>
    public T ExtractMin()
    {
        if (heap.Count == 0)
            throw new InvalidOperationException("Heap is empty");
        
        T min = heap[0];
        
        // Move last element to root
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        
        // Bubble down to restore heap property
        if (heap.Count > 0)
        {
            BubbleDown(0);
        }
        
        return min;
    }
    
    /// <summary>
    /// Peek minimum - O(1)
    /// </summary>
    public T Peek()
    {
        if (heap.Count == 0)
            throw new InvalidOperationException("Heap is empty");
        return heap[0];
    }
    
    private void BubbleUp(int index)
    {
        while (index > 0)
        {
            int parentIdx = (index - 1) / 2;
            
            if (heap[index].CompareTo(heap[parentIdx]) < 0)
            {
                // Swap with parent and continue up
                (heap[index], heap[parentIdx]) = (heap[parentIdx], heap[index]);
                index = parentIdx;
            }
            else
            {
                break;  // Heap property satisfied
            }
        }
    }
    
    private void BubbleDown(int index)
    {
        while (true)
        {
            int smallest = index;
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;
            
            // Check left child
            if (leftChild < heap.Count && heap[leftChild].CompareTo(heap[smallest]) < 0)
            {
                smallest = leftChild;
            }
            
            // Check right child
            if (rightChild < heap.Count && heap[rightChild].CompareTo(heap[smallest]) < 0)
            {
                smallest = rightChild;
            }
            
            if (smallest != index)
            {
                // Swap and continue down
                (heap[index], heap[smallest]) = (heap[smallest], heap[index]);
                index = smallest;
            }
            else
            {
                break;  // Heap property satisfied
            }
        }
    }
    
    public int Size => heap.Count;
    public bool IsEmpty => heap.Count == 0;
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Parent/child indices: parent = (i-1)/2, left = 2i+1, right = 2i+2. Off-by-one breaks heap.
- 🟡 **PERFORMANCE:** Building heap from array is O(n), not O(n log n). Use constructor for bulk inserts.
- 🟢 **BEST PRACTICE:** Use C#'s `PriorityQueue<T>` (C# 10+) when available; custom heap for learning.

---

### Pattern 4: Hash Table with Separate Chaining

#### 🧠 Mental Model
Array of buckets (linked lists). Hash function maps key to bucket index. Collision: add to bucket's linked list. Insert/lookup: O(1) expected if hash is good and load factor ≤ 0.75.

#### ✅ When to Use This Pattern
- ✅ Fast O(1) lookup/insert needed.
- ✅ Unknown number of elements (dynamic sizing).
- ✅ Chaining simpler than probing.
- ✅ Standard use case (99% of situations use `Dictionary<K,V>`).

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
using System;
using System.Collections.Generic;

/// <summary>
/// Hash Table with Separate Chaining
/// Time: O(1) expected (O(n) worst if all hash to same bucket) | Space: O(n)
/// 
/// 🧠 MENTAL MODEL:
/// Array of linked list buckets. Hash function maps key → bucket.
/// Insert/lookup: hash key, traverse bucket list.
/// Expected O(1) if hash uniform and load factor < 0.75.
/// </summary>
public class HashTableChaining<K, V> where K : notnull
{
    private const int INITIAL_CAPACITY = 16;
    private const float LOAD_FACTOR_THRESHOLD = 0.75f;
    
    private LinkedList<KeyValuePair<K, V>>[] buckets;
    private int count;
    
    public HashTableChaining()
    {
        buckets = new LinkedList<KeyValuePair<K, V>>[INITIAL_CAPACITY];
        for (int i = 0; i < buckets.Length; i++)
        {
            buckets[i] = new LinkedList<KeyValuePair<K, V>>();
        }
        count = 0;
    }
    
    /// <summary>
    /// Insert or update key-value pair - O(1) expected
    /// </summary>
    public void Insert(K key, V value)
    {
        // STEP 1: Get bucket index
        int bucketIdx = GetBucketIndex(key);
        LinkedList<KeyValuePair<K, V>> bucket = buckets[bucketIdx];
        
        // STEP 2: Check if key exists and update
        foreach (var node in bucket)
        {
            if (node.Key.Equals(key))
            {
                // Update existing key
                bucket.Remove(node);
                bucket.AddFirst(new KeyValuePair<K, V>(key, value));
                return;
            }
        }
        
        // STEP 3: Key doesn't exist, add new entry
        bucket.AddFirst(new KeyValuePair<K, V>(key, value));
        count++;
        
        // STEP 4: Resize if load factor exceeded
        if ((float)count / buckets.Length > LOAD_FACTOR_THRESHOLD)
        {
            Resize();
        }
    }
    
    /// <summary>
    /// Lookup value by key - O(1) expected
    /// </summary>
    public bool TryGetValue(K key, out V value)
    {
        value = default;
        
        // STEP 1: Get bucket
        int bucketIdx = GetBucketIndex(key);
        LinkedList<KeyValuePair<K, V>> bucket = buckets[bucketIdx];
        
        // STEP 2: Search bucket for key
        foreach (var entry in bucket)
        {
            if (entry.Key.Equals(key))
            {
                value = entry.Value;
                return true;
            }
        }
        
        return false;
    }
    
    /// <summary>
    /// Delete key - O(1) expected
    /// </summary>
    public bool Remove(K key)
    {
        int bucketIdx = GetBucketIndex(key);
        LinkedList<KeyValuePair<K, V>> bucket = buckets[bucketIdx];
        
        foreach (var entry in bucket)
        {
            if (entry.Key.Equals(key))
            {
                bucket.Remove(entry);
                count--;
                return true;
            }
        }
        
        return false;
    }
    
    private int GetBucketIndex(K key)
    {
        // CRITICAL: Use absolute value to avoid negative indices
        return Math.Abs(key.GetHashCode()) % buckets.Length;
    }
    
    private void Resize()
    {
        // STEP 1: Create new larger array
        int newCapacity = buckets.Length * 2;
        LinkedList<KeyValuePair<K, V>>[] newBuckets = 
            new LinkedList<KeyValuePair<K, V>>[newCapacity];
        for (int i = 0; i < newBuckets.Length; i++)
        {
            newBuckets[i] = new LinkedList<KeyValuePair<K, V>>();
        }
        
        // STEP 2: Rehash all entries
        for (int i = 0; i < buckets.Length; i++)
        {
            foreach (var entry in buckets[i])
            {
                int newIdx = Math.Abs(entry.Key.GetHashCode()) % newCapacity;
                newBuckets[newIdx].AddFirst(entry);
            }
        }
        
        // STEP 3: Replace buckets
        buckets = newBuckets;
    }
    
    public int Count => count;
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Use `Math.Abs(GetHashCode())` to handle negative hashes. Negative indices crash.
- 🟡 **PERFORMANCE:** Resize when load factor > 0.75 to keep chains small. Doubling capacity spreads entries.
- 🟢 **BEST PRACTICE:** Use `Dictionary<K,V>` for production. This implementation is for learning.

---

### Pattern 5: Rolling Hash (Rabin-Karp Substring Search)

#### 🧠 Mental Model
Compute polynomial hash of pattern. For each window in text, compute hash of window. If hashes match, verify substring match (check for hash collisions). Key insight: rolling hash updates in O(1) by removing old character and adding new one.

#### ✅ When to Use This Pattern
- ✅ Substring search in large text (O(n+m) vs naive O(n*m)).
- ✅ Pattern matching with wildcards or multiple patterns.
- ✅ Plagiarism detection (compare document hashes).
- ✅ DNA sequence matching.

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
using System.Collections.Generic;

/// <summary>
/// Rabin-Karp Rolling Hash - Substring Search
/// Time: O(n+m) average | Space: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// Polynomial hash: H = c₀*b⁰ + c₁*b¹ + ... + cₙ₋₁*bⁿ⁻¹ (mod prime)
/// Rolling window: remove leftmost char, add rightmost char in O(1).
/// Hash match → verify substring (avoid false positives).
/// </summary>
public class KarpRabinMatcher
{
    private const long PRIME = 101;  // Prime for modulo
    private const int BASE = 256;    // Alphabet size
    
    /// <summary>
    /// Find all occurrences of pattern in text
    /// </summary>
    public static List<int> FindPattern(string text, string pattern)
    {
        List<int> matches = new();
        
        if (pattern.Length > text.Length) return matches;
        if (pattern.Length == 0) return matches;
        
        // STEP 1: Precompute BASE^(m-1) mod PRIME
        long pow = 1;
        for (int i = 0; i < pattern.Length - 1; i++)
        {
            pow = (pow * BASE) % PRIME;
        }
        
        // STEP 2: Compute hash of pattern
        long patternHash = ComputeHash(pattern, pattern.Length);
        
        // STEP 3: Compute hash of first window in text
        long textHash = ComputeHash(text, pattern.Length);
        
        // STEP 4: Roll through text
        for (int i = 0; i <= text.Length - pattern.Length; i++)
        {
            // If hashes match, verify substring
            if (textHash == patternHash)
            {
                if (VerifyMatch(text, i, pattern))
                {
                    matches.Add(i);
                }
            }
            
            // Rolling hash: move to next window
            if (i < text.Length - pattern.Length)
            {
                // Remove leftmost character, add rightmost
                textHash = (BASE * (textHash - (text[i] * pow)) + text[i + pattern.Length]) % PRIME;
                
                // Ensure positive
                if (textHash < 0)
                    textHash = (textHash + PRIME);
            }
        }
        
        return matches;
    }
    
    private static long ComputeHash(string s, int length)
    {
        long hash = 0;
        for (int i = 0; i < length; i++)
        {
            hash = (hash * BASE + s[i]) % PRIME;
        }
        return hash;
    }
    
    private static bool VerifyMatch(string text, int startIdx, string pattern)
    {
        // Verify actual substring match (not just hash collision)
        for (int i = 0; i < pattern.Length; i++)
        {
            if (text[startIdx + i] != pattern[i])
                return false;
        }
        return true;
    }
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Verify substring on hash match. Hash collisions can give false positives.
- 🟡 **PERFORMANCE:** Use large prime for modulo to reduce collisions. BASE^(m-1) mod PRIME precomputed once.
- 🟢 **BEST PRACTICE:** For single pattern, use `string.IndexOf()`. Rabin-Karp shines for multiple patterns or streaming text.

---

## SECTION 4️⃣: C# Collection Decision GUIDE

### When to Use Each Collection for Week 3 Patterns

| Use Case | Collection | Why? | When NOT to Use | Alternative |
|---|---|---|---|---|
| Sorting large array, guaranteed O(n log n) | `Array.Sort()` (Intro Sort) | Built-in optimized hybrid (quick→heap); beats custom | For learning sort internals | Implement Merge Sort |
| Stable sort required, external sorting | Custom Merge Sort | O(n log n) guaranteed + stable | Performance critical & no stability need | Quick Sort |
| Fast average case, in-place preferred | Custom Quick Sort | Expected O(n log n), O(log n) space; cache-efficient | Worst-case must be O(n log n) | Merge Sort or Intro Sort |
| Priority queue, K-largest problems | `PriorityQueue<T>` (C# 10+) or Min-Heap | O(log n) insert/extract, O(1) peek | Need custom control | Implement Min Heap |
| Fast lookup/insert, unknown size | `Dictionary<K,V>` | Built-in optimized hash table | Need to understand internals | Custom Hash Table |
| Hash table learning, separate chaining | Custom `HashTableChaining<K,V>` | Illustrates collisions, resizing, load factor | Production code | `Dictionary<K,V>` |
| Substring search, multiple patterns | Rabin-Karp rolling hash | O(n+m) vs naive O(n*m); scales to multiple patterns | Single pattern search | `string.IndexOf()` |

**KEY INSIGHT:**
Sorting determines algorithm choice (elementary vs divide-conquer). Hashing speed depends on hash quality and load factor management.

---

## SECTION 5️⃣: PROGRESSIVE PROBLEM LADDER

### 🎯 Strategy: Solve Problems in Stages

**Stage 1 (Green):** Master core pattern skeleton  
**Stage 2 (Yellow):** Recognize pattern variations and edge cases  
**Stage 3 (Red):** Combine multiple patterns for complex problems

---

### 🟢 STAGE 1: CANONICAL PROBLEMS — Master Core Pattern

Solve these to cement the pattern. Can you code the skeleton without looking?

| # | LeetCode # | Difficulty | Pattern | C# Focus | Concept |
|---|---|---|---|---|---|
| 1 | #912 | 🟢 Easy | Sort Array | Built-in `Array.Sort()` behavior | Understand when hybrid sort applies |
| 2 | #215 | 🟢 Easy | Kth Largest | Min-Heap (k elements) | Heap extract-min pattern |
| 3 | #383 | 🟢 Easy | Ransom Note | Hash table count collisions | Character frequency with Dictionary |
| 4 | #242 | 🟢 Easy | Valid Anagram | Hash table or sort | Both approaches: hash O(n), sort O(n log n) |
| 5 | #1 | 🟢 Easy | Two Sum | Hash table (value → index) | O(1) lookup beats O(n log n) sort |
| 6 | #704 | 🟢 Easy | Binary Search | Pre-sorted (merge sort property) | When sort guarantees O(n log n) |
| 7 | #189 | 🟢 Easy | Rotate Array | In-place rotation (quick sort partition analogy) | O(1) space solution |

**STAGE 1 GOAL:** Pattern fluency. Can you implement each skeleton in < 5 minutes from memory?

---

### 🟡 STAGE 2: VARIATIONS — Recognize Pattern Boundaries

These problems twist the pattern. When does it work? When does it break?

| # | LeetCode # | Difficulty | Pattern + Twist | C# Focus | When Pattern Breaks |
|---|---|---|---|---|---|
| 1 | #148 | 🟡 Medium | Sort Linked List | Merge sort (no random access) | Quick sort fails on linked lists |
| 2 | #347 | 🟡 Medium | Top K Frequent | Min-Heap size K maintains | Max-Heap would need n-k extracts |
| 3 | #692 | 🟡 Medium | Top K Frequent Words | Hash + Heap with custom comparator | Lexicographic tie-breaking needed |
| 4 | #49 | 🟡 Medium | Group Anagrams | Hash of sorted string as key | Detect anagram groups in O(n*k log k) |
| 5 | #3 | 🟡 Medium | Longest Substring Without Repeating | Hash table + sliding window | Expand/contract window with hash state |
| 6 | #560 | 🟡 Medium | Subarray Sum Equals K | Hash of prefix sums | Cumulative sum collisions → target count |
| 7 | #28 | 🟡 Medium | Implement `strStr()` | Rabin-Karp or KMP | Rolling hash faster than naive |

**STAGE 2 GOAL:** Pattern boundaries. When do variations apply? When is core pattern insufficient?

---

### 🟠 STAGE 3: INTEGRATION — Combine Patterns

Hard problems rarely use just one pattern. These combine multiple:

| # | LeetCode # | Difficulty | Patterns Required | C# Focus | Pattern Combination Logic |
|---|---|---|---|---|---|
| 1 | #295 | 🔴 Hard | Two Heaps | Min-heap + Max-heap for median | Maintain balance: larger half in min-heap |
| 2 | #588 | 🔴 Hard | File System | Hash tables + tries (tree structure) | Nested hash dict for directory paths |
| 3 | #76 | 🔴 Hard | Minimum Window Substring | Rabin-Karp + sliding window + hash | Rolling hash optimization on character sets |

**STAGE 3 GOAL:** Real-world thinking. Professional problems combine multiple patterns.

---

## SECTION 6️⃣: WEEK 3 PITFALLS & C# GOTCHAS

### 🐛 Runtime Issues & Collection Pitfalls

Common bugs you'll hit and how to fix them:

| ❌ Pattern | 🐛 Bug | 💥 C# Symptom | 🔧 Quick Fix |
|---|---|---|---|
| Merge Sort | Not copying auxiliary array before merge | Overwriting source data; corrupted merge | Always: `for (int i = left; i <= right; i++) aux[i] = arr[i];` |
| Quick Sort | Bad pivot selection (always same element) | O(n²) on sorted/reverse-sorted data | Randomize pivot: `Rand.Next()` before sort |
| Heap Insert | Not bubbling up after insert | Heap property violated; peek gives wrong min | Always bubble up: `BubbleUp(heap.Count - 1);` |
| Heap Extract | Not bubbling down after moving last to root | Heap property violated; order wrong | Always bubble down after `heap.RemoveAt()` |
| Hash Insert | Not resizing when load factor > 0.75 | Chains grow; O(1) degrades to O(n) | Monitor `count / buckets.Length > 0.75` |
| Hash Collision | Negative hash code as index | `IndexOutOfRangeException` on negative index | Always: `Math.Abs(GetHashCode()) % capacity` |

### 🎯 Week 3 Collection Gotchas

These mistakes are EASY to make:

- ❌ Implementing Quick Sort without pivot randomization → O(n²) on reverse-sorted array → Randomize before sort
  - Example: Sorting `[n, n-1, ..., 2, 1]` picks n as pivot always → O(n²)

- ❌ Using unstable sort when stability needed → Equal elements change order → Use Merge Sort or mark stable sort
  - Example: `[(age:30, name:"Alice"), (age:30, name:"Bob")]` after sort: order might flip

- ❌ Heap without bubbling up/down → Heap property violated → Verify parent ≤ children always
  - Example: After insert, forgot bubble up → peek() returns wrong minimum

- ❌ Hash table without resizing → Load factor unbounded → Resize when count/capacity > 0.75
  - Example: 1000 inserts in 16 buckets = 62.5 load factor → all O(1) become O(n)

- ❌ Rabin-Karp without verification → Hash collisions give false positives → Always verify substring match on hash hit
  - Example: Pattern "ab" might hash to same as "cd" → check actual substring

- ❌ `(low + high) / 2` overflow in binary search before sort → Integer wraparound → Use `low + (high - low) / 2` and test with sorted array
  - Example: After Merge Sort sorts to [1, 2, 3], binary search fails on bad mid calculation

---

## SECTION 7️⃣: QUICK REFERENCE — INTERVIEW PREPARATION

### Mental Models for Fast Recall (30 seconds before interview!)

| Pattern | 1-Liner Mental Model | Code Symbol | When You See This... |
|---|---|---|---|
| **Merge Sort** | "Divide-conquer: split, sort, merge sorted halves" | `MergeSortHelper(arr, 0, mid); MergeSortHelper(arr, mid+1, high); Merge(...);` | "Stable", "Guaranteed O(n log n)", "External sort" |
| **Quick Sort** | "Partition around pivot, recursively sort sides" | `Partition(arr, low, high); QuickSortHelper(arr, low, p-1);` | "Fast average", "In-place", "Randomize pivot" |
| **Heap (Min)** | "Complete tree: parent ≤ children; bubble up/down" | `BubbleUp(i); BubbleDown(0); parent = (i-1)/2; left = 2i+1;` | "K largest", "Priority queue", "Dijkstra" |
| **Hash Table** | "Buckets + linked lists; hash → bucket, traverse list" | `int idx = Math.Abs(key.GetHashCode()) % capacity;` | "O(1) lookup", "Dictionary", "Resize > 0.75 load" |
| **Rabin-Karp** | "Rolling polynomial hash; O(1) window update" | `hash = (hash * BASE - (text[i] * pow) + text[i+m]) % PRIME;` | "Substring search", "Multiple patterns", "Plagiarism" |

---

## ✅ WEEK 3 COMPLETION CHECKLIST

### Pattern Fluency — Can You Recognize & Choose?

- [ ] Recognize Elementary Sort signals ("small n", "nearly sorted", "stable")
- [ ] Recall insertion sort skeleton without notes (< 2 min)
- [ ] Explain WHY O(n²) acceptable for small n
- [ ] Explain WHEN to use vs merge/quick sort

- [ ] Recognize Merge Sort signals ("stable", "guaranteed O(n log n)", "external sort")
- [ ] Recall merge sort skeleton without notes
- [ ] Explain WHY stable (use <= in merge comparison)
- [ ] Explain WHEN to use vs quick sort

- [ ] Recognize Quick Sort signals ("fast average", "in-place", "cache-friendly")
- [ ] Recall quick sort skeleton without notes (randomized pivot)
- [ ] Explain WHY randomization defeats adversarial input
- [ ] Explain WHEN pivot strategy matters

- [ ] Recognize Heap signals ("K largest", "priority queue", "Dijkstra")
- [ ] Recall heap insert/extract without notes (bubble up/down)
- [ ] Explain WHY array-based tree works (parent/child indices)
- [ ] Explain WHEN to use vs sort

- [ ] Recognize Hash Table signals ("O(1) lookup", "unknown size", "collision handling")
- [ ] Recall hash table insert/lookup without notes
- [ ] Explain WHY load factor matters (resize when > 0.75)
- [ ] Explain WHEN separate chaining vs open addressing

- [ ] Recognize Rabin-Karp signals ("substring search", "multiple patterns", "rolling hash")
- [ ] Recall rolling hash update without notes (O(1) window move)
- [ ] Explain WHY rolling hash O(n+m) vs naive O(n*m)
- [ ] Explain WHEN to verify on hash match

### Problem Solving — Can You Practice?

- [ ] Solved ALL Stage 1 canonical problems (7 problems) ✓
- [ ] Solved 80%+ Stage 2 variations (6+ of 7 problems)
- [ ] Solved 50%+ Stage 3 integration problems (got the ideas, even if not perfect)

### Production Code Quality — Can You Code?

- [ ] Used guard clauses on all inputs (empty, single element, boundary)
- [ ] Added mental model comments (MENTAL MODEL: ...)
- [ ] Chose correct collection (Array for sort, Heap for priority, Hash for lookup)
- [ ] Handled edge cases explicitly (stable sort with <=, hash resize logic)
- [ ] Code passes code review (clean, readable, efficient, no hacks)

### Interview Ready — Can You Communicate?

- [ ] Can sort array correctly and explain sort choice
- [ ] Can EXPLAIN why merge/quick/heap beats others for specific problem
- [ ] Can write PRODUCTION-GRADE sort, heap, hash (guards, comments, no shortcuts)
- [ ] Can discuss tradeoffs (time/space, stability, in-place)

---

### 🎯 Week 3 Mastery Status

- [ ] **YES - I've mastered Week 3. Ready for Week 4+.**
- [ ] **NO - Need to practice more. Focus on Stage 2 variations or weak patterns.**

---

## 📚 REFERENCE MATERIALS

This file is self-contained. You have:
- **Decision framework** (SECTION 1) — Recognize which pattern (elementary vs merge vs quick vs heap vs hash)
- **Anti-patterns knowledge** (SECTION 2) — What NOT to do and why
- **Production-grade code** (SECTION 3) — 5 complete implementations with guards and mental models
- **Collection guidance** (SECTION 4) — Which collection for which use case
- **Progressive practice** (SECTION 5) — Problems from easy to hard
- **Real gotchas** (SECTION 6) — Bugs you'll actually encounter
- **Quick interview reference** (SECTION 7) — 30-second recall before interview

**Everything you need to master Week 3 is here.**

---

## 🚀 HOW TO USE THIS FILE

### For Learning:
1. Start with **SECTION 1** → Understand the decision tree
2. Review **SECTION 2** → Learn what NOT to do
3. Study **SECTION 3** → Understand production implementations with mental models
4. Follow **SECTION 5** → Solve problems progressively (Stage 1 → 2 → 3)

### For Reference:
1. See a sorting problem? → Check **SECTION 1** (decision tree)
2. Stuck or getting wrong answer? → Check **SECTION 6** (gotchas)
3. Need code template? → Check **SECTION 3** (implementations)
4. Before interview? → Check **SECTION 7** (quick recall)

### For Interview Prep:
1. Day before: Review **SECTION 7** (mental models)
2. Day of: Skim **SECTION 1** (decision tree)
3. During interview: Use mental models from **SECTION 7** to explain your choices

---

## 📊 COMPLEXITY REFERENCE

| Pattern | Time | Space | Notes |
|---------|------|-------|-------|
| Elementary Sort (Bubble/Selection/Insertion) | O(n²) | O(1) | Small constant factors; use for n<50 |
| Merge Sort | O(n log n) | O(n) | Guaranteed; stable; external sorting |
| Quick Sort | O(n log n) avg / O(n²) worst | O(log n) | Fast average; randomized pivot |
| Intro Sort (Hybrid) | O(n log n) guaranteed | O(log n) | Quick→Heap fallback; .NET default |
| Heap Insert/Extract | O(log n) | O(1) | Per operation; building O(n) |
| Hash Table Ops | O(1) expected / O(n) worst | O(n) | Expected; depends on load factor |
| Rabin-Karp Search | O(n+m) average | O(1) | Sliding window hash update |

---

*End of Week 3 Extended C# Support — v13 Hybrid Format COMPLETE*

---

This file combines:
- ✅ **Pattern selection guidance** (v11 strength) — Know WHEN/WHY to choose
- ✅ **Production-grade code** (v12 strength) — Know HOW to implement
- ✅ **Anti-patterns & gotchas** (v11 strength) — Know what to AVOID  
- ✅ **Progressive learning** (v11 strength) — Practice from easy to hard
- ✅ **Interview readiness** (v13 integration) — Pass technical interviews
- ✅ **Complete coverage** (WEEK 3 TOPICS) — All sorting, heaps, hashing algorithms

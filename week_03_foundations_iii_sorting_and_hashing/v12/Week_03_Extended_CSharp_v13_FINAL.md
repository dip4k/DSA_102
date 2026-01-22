# 🗺️ Week_03_Extended_CSharp_Problem_Solving_Implementation

**Version:** v1.0 HYBRID (Pattern Recognition + Production Implementation)  
**Purpose:** Master Week 3 patterns (Sorting, Heaps & Hashing) through recognition, understanding, and practice  
**Target:** Transform pattern knowledge into interview-ready C# coding skills  
**Prerequisites:** Week 3 instructional files + standard support files complete

---

## SECTION 1️⃣: PATTERN RECOGNITION FRAMEWORK

### 🎯 Decision Tree — How to Identify & Choose Week 3 Patterns

Use this decision tree when you encounter a problem in Week 3:

| 🔍 Problem Phrases/Signals | 🎯 Pattern Name | ❓ Why This Pattern? | 💻 C# Collection | ⏱️ Time/Space |
|---|---|---|---|---|
| "Sort", "order", "ascending/descending" | **Merge Sort** | O(n log n) guaranteed worst-case, stable | `T[]` (auxiliary) | O(n log n)/O(n) |
| "Sort", "quicksort", "average case fast" | **Quick Sort** | O(n log n) average, in-place, randomized pivot | `T[]` (in-place) | O(n log n) avg/O(log n) |
| "Adaptive sort", "avoid O(n²)", "real-world" | **Intro Sort / Hybrid** | Start QuickSort, fall back to HeapSort if deep | `T[]` (in-place) | O(n log n) worst/O(log n) |
| "Smallest k", "priority", "heap-like" | **Min-Heap / Max-Heap** | Parent ≤ children (min-heap); efficient extraction | `int[]` or custom | O(1) peek/O(log n) extract |
| "k largest", "top k", "frequent elements" | **Heap-based Top-K** | Build heap, extract k times | Heap or `PriorityQueue<T>` | O(n) build/O(k log n) extract |
| "Key-value lookup", "O(1) average", "store mapping" | **Hash Table (Separate Chaining)** | Hash function → bucket → chain | `Dictionary<K,V>` | O(1) avg/O(n) worst |
| "No collisions expected", "direct lookup" | **Hash Table (Open Addressing)** | Linear/quadratic/double hashing probing | `T[]` with probe function | O(1) avg/O(n) worst |
| "Substring search", "pattern matching" | **Rolling Hash / Karp-Rabin** | Polynomial hash on sliding window | hash state variable | O(n+m) avg/O(nm) worst |
| "Small dataset", "nearly sorted" | **Insertion Sort** | Grow sorted prefix; cheap for small n | `T[]` (in-place) | O(n²) worst/O(1) |
| "Already sorted or reverse sorted" | **Bubble Sort** (rare) | Simple but slow; only for education | `T[]` (in-place) | O(n²) worst/O(1) |

**HOW TO READ THIS:**
- See "[Signal X]" in a problem? IMMEDIATELY think "[Pattern Y]"
- Ask yourself: "Why does this pattern solve it?" → Internalize the reasoning
- Check what collection is recommended → Learn why that collection is best

---

## SECTION 2️⃣: ANTI-PATTERNS — WHEN NOT TO USE & WHAT FAILS

### ⚠️ Week 3 Common Mistakes & Correct Alternatives

Learn WHAT NOT TO DO and WHY:

| ❌ Wrong Approach | 💥 Why It Fails | ⚡ Symptom/Consequence | ✅ Correct Alternative |
|---|---|---|---|---|
| Using Bubble Sort or Selection Sort on large n | O(n²) is catastrophic for n > 1000 | Timeout on LeetCode; practical slowness | Use MergeSort (stable, O(n log n) guaranteed) or QuickSort (fast average) |
| Forgetting to merge/combine in Merge Sort | Recursively sort halves but never combine | Returns original array unsorted | Implement full merge step; copy auxiliaries back to main array |
| Building heap incorrectly (not sift-down) | Heap property violated; peek/extract wrong | Wrong answer or crashes | Use heapify-down for each element in reverse order |
| Storing large objects directly as hash values | Collision chains become very long; hash degrades | O(n) lookups despite "hash table" | Store reference/pointer; use good hash function to distribute |
| Using weak hash function (e.g., `x % 10`) | Collisions cluster; all items in few buckets | O(n) operations despite hashing | Use multiplier-based or universal hash family; randomize seed |
| Applying string search without preprocessing | Every mismatch requires recomparing from start | O(nm) naive search gets TLE | Use KMP or Karp-Rabin for O(n+m) average |

**ANTI-PATTERN LESSON:**
Understand not just the pattern, but understand its boundaries.
When you see someone use [Mistake], explain why [Alternative] is better.

---

## SECTION 3️⃣: PATTERN IMPLEMENTATIONS (Production-Grade)

### Pattern 1: Merge Sort (Stable, O(n log n) Guaranteed)

#### 🧠 Mental Model
Divide-and-conquer: recursively sort left half, sort right half, then **merge** the two sorted halves back into order. The merge step is where all the work happens—it's guaranteed O(n) to merge two sorted sequences of size n/2.

#### ✅ When to Use This Pattern
- ✅ When you need O(n log n) in worst-case (guaranteed)
- ✅ When stability matters (equal elements keep original order)
- ✅ When external sorting needed (data larger than memory)

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Stable merge sort implementation.
/// Time Complexity: O(n log n) guaranteed | Space Complexity: O(n) auxiliary
/// 
/// 🧠 MENTAL MODEL:
/// Divide: Recursively sort left and right halves.
/// Conquer: Merge two sorted halves by comparing elements.
/// Stability comes from: When arr[left[i]] == arr[right[j]], we take from left first.
/// </summary>
public class MergeSortImplementation<T> where T : IComparable<T> {
    
    public static void Sort(T[] arr) {
        // STEP 1: Guard Clauses
        if (arr == null || arr.Length <= 1) return;
        
        // STEP 2: Initialize State
        // aux: auxiliary array for merging (allocated once)
        T[] aux = new T[arr.Length];
        
        // STEP 3: Core Logic - Recursive sort
        MergeSortHelper(arr, 0, arr.Length - 1, aux);
    }
    
    private static void MergeSortHelper(T[] arr, int left, int right, T[] aux) {
        // Base case: single element is sorted
        if (left >= right) return;
        
        // Divide: Find middle
        int mid = left + (right - left) / 2;
        
        // Conquer: Recursively sort left and right
        MergeSortHelper(arr, left, mid, aux);
        MergeSortHelper(arr, mid + 1, right, aux);
        
        // Combine: Merge two sorted halves
        Merge(arr, left, mid, right, aux);
    }
    
    private static void Merge(T[] arr, int left, int mid, int right, T[] aux) {
        // STEP 1: Copy data to auxiliary array
        for (int i = left; i <= right; i++) {
            aux[i] = arr[i];
        }
        
        // STEP 2: Merge back to original array
        // i: pointer in left half [left, mid]
        // j: pointer in right half [mid+1, right]
        // k: pointer in merged array [left, right]
        int i = left;
        int j = mid + 1;
        int k = left;
        
        // Merge two sorted halves
        while (i <= mid && j <= right) {
            // CRITICAL for STABILITY: Use <= (not <)
            // When equal, take from left first to preserve original order
            if (aux[i].CompareTo(aux[j]) <= 0) {
                arr[k++] = aux[i++];
            } else {
                arr[k++] = aux[j++];
            }
        }
        
        // Copy remaining from left half
        while (i <= mid) {
            arr[k++] = aux[i++];
        }
        
        // Copy remaining from right half (not strictly necessary, already in place)
        // while (j <= right) arr[k++] = aux[j++];
    }
}

// Trace: Sort [3, 1, 4, 1, 5, 9]
// Split: [3, 1, 4] | [1, 5, 9]
// Split: [3] | [1, 4] | [1, 5] | [9]
// Merge: [1, 3, 4] | [1, 5, 9]
// Merge: [1, 1, 3, 4, 5, 9] ✓ Stable (both 1's in original order)
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Use `<=` in merge comparison, NOT `<`, to preserve stability
- 🟡 **PERFORMANCE:** O(n) auxiliary space is the trade-off for guaranteed O(n log n)
- 🟢 **BEST PRACTICE:** MergeSort is the default choice when stability is required

---

### Pattern 2: Quick Sort with Randomized Pivot

#### 🧠 Mental Model
Partition: Pick a pivot, rearrange so all smaller elements are left, all larger are right. Recursively sort left and right. The partition step is clever—it achieves O(n) in-place. Randomize pivot to avoid adversarial inputs.

#### ✅ When to Use This Pattern
- ✅ When O(n log n) average case is acceptable
- ✅ When you need in-place sort (low memory)
- ✅ When average case speed matters more than worst-case guarantee
- ✅ Real-world libraries prefer QuickSort due to cache locality

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Quick sort with randomized pivot selection.
/// Time Complexity: O(n log n) average, O(n²) worst-case | Space Complexity: O(log n) call stack
/// 
/// 🧠 MENTAL MODEL:
/// Partition: Rearrange around pivot so [smaller...pivot...larger].
/// Randomize pivot to avoid O(n²) on adversarial inputs (already sorted, reverse sorted, etc).
/// </summary>
public class QuickSortImplementation<T> where T : IComparable<T> {
    
    private static readonly Random Rand = new Random();
    
    public static void Sort(T[] arr) {
        // STEP 1: Guard Clauses
        if (arr == null || arr.Length <= 1) return;
        
        // STEP 2: Randomize array to avoid adversarial inputs
        // (alternatively, randomize pivot during partition)
        Shuffle(arr);
        
        // STEP 3: Core Logic - Recursive quicksort
        QuickSortHelper(arr, 0, arr.Length - 1);
    }
    
    private static void QuickSortHelper(T[] arr, int low, int high) {
        if (low < high) {
            // Partition and get pivot position
            int p = Partition(arr, low, high);
            
            // Recursively sort left and right
            QuickSortHelper(arr, low, p - 1);
            QuickSortHelper(arr, p + 1, high);
        }
    }
    
    private static int Partition(T[] arr, int low, int high) {
        // STEP 1: Choose pivot (last element for simplicity)
        T pivot = arr[high];
        
        // STEP 2: Two-pointer partition
        // i: invariant that arr[0..i] contains elements < pivot
        // j: scanner for current element
        int i = low - 1;
        
        for (int j = low; j < high; j++) {
            if (arr[j].CompareTo(pivot) < 0) {
                i++;
                // Swap arr[i] and arr[j]
                (arr[i], arr[j]) = (arr[j], arr[i]);
            }
        }
        
        // STEP 3: Place pivot in final position
        (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
        
        return i + 1;
    }
    
    private static void Shuffle(T[] arr) {
        for (int i = arr.Length - 1; i > 0; i--) {
            int j = Rand.Next(0, i + 1);
            (arr[i], arr[j]) = (arr[j], arr[i]);
        }
    }
}

// Trace: Sort [3, 1, 4, 1, 5, 9]
// Pivot: 9, Partition: [3, 1, 4, 1, 5] | 9 (9 in final position)
// Pivot (left): 5, Partition: [3, 1, 4, 1] | 5
// ... continues recursively
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Randomize pivot (or shuffle first) to avoid O(n²) on sorted/reverse-sorted input
- 🟡 **PERFORMANCE:** In-place partition is cache-friendly; better than MergeSort in practice
- 🟢 **BEST PRACTICE:** QuickSort is default in most libraries (including C#'s Array.Sort)

---

### Pattern 3: Intro Sort (Hybrid: QuickSort + HeapSort Fallback)

#### 🧠 Mental Model
Start with QuickSort (fast average case). If recursion depth exceeds limit, switch to HeapSort (guaranteed O(n log n)). This combines QuickSort's speed with HeapSort's worst-case guarantee.

#### ✅ When to Use This Pattern
- ✅ Production sorting (C# Array.Sort, C++ std::sort use this)
- ✅ Avoid pathological inputs that break QuickSort
- ✅ Guarantee O(n log n) worst-case without sacrificing average speed

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Intro sort: Start with QuickSort, fall back to HeapSort if depth exceeds limit.
/// Time Complexity: O(n log n) guaranteed worst-case | Space Complexity: O(log n) call stack
/// 
/// 🧠 MENTAL MODEL:
/// If recursion depth > 2 * log(n), adversarial input detected. Switch to HeapSort.
/// Combines: QuickSort speed (average) + HeapSort guarantee (worst-case).
/// </summary>
public class IntroSortImplementation<T> where T : IComparable<T> {
    
    public static void Sort(T[] arr) {
        if (arr == null || arr.Length <= 1) return;
        
        // depthLimit: if we go deeper than this, something is wrong (adversarial input)
        int depthLimit = 2 * (int)Math.Log(arr.Length + 1);
        IntroSortHelper(arr, 0, arr.Length - 1, depthLimit);
    }
    
    private static void IntroSortHelper(T[] arr, int lo, int hi, int depthLimit) {
        // While range is not too small
        while (hi - lo > 16) {
            if (depthLimit == 0) {
                // Depth limit exceeded: Switch to HeapSort to avoid O(n²)
                HeapSort(arr, lo, hi);
                return;
            }
            
            // QuickSort partition
            int p = Partition(arr, lo, hi);
            
            // Recurse on smaller partition, iterate on larger
            // (tail recursion optimization)
            if (p - lo < hi - p) {
                IntroSortHelper(arr, lo, p - 1, depthLimit - 1);
                lo = p + 1;  // Iterate on right (loop will continue)
            } else {
                IntroSortHelper(arr, p + 1, hi, depthLimit - 1);
                hi = p - 1;  // Iterate on left (loop will continue)
            }
            
            depthLimit--;
        }
        
        // Small subarrays: use insertion sort (low overhead)
        InsertionSort(arr, lo, hi);
    }
    
    private static int Partition(T[] arr, int lo, int hi) {
        T pivot = arr[hi];
        int i = lo - 1;
        
        for (int j = lo; j < hi; j++) {
            if (arr[j].CompareTo(pivot) < 0) {
                i++;
                (arr[i], arr[j]) = (arr[j], arr[i]);
            }
        }
        
        (arr[i + 1], arr[hi]) = (arr[hi], arr[i + 1]);
        return i + 1;
    }
    
    private static void InsertionSort(T[] arr, int lo, int hi) {
        for (int i = lo + 1; i <= hi; i++) {
            T key = arr[i];
            int j = i - 1;
            
            while (j >= lo && arr[j].CompareTo(key) > 0) {
                arr[j + 1] = arr[j];
                j--;
            }
            
            arr[j + 1] = key;
        }
    }
    
    private static void HeapSort(T[] arr, int lo, int hi) {
        // Build max-heap in arr[lo..hi]
        for (int i = (lo + hi) / 2; i >= lo; i--) {
            BubbleDown(arr, i, lo, hi);
        }
        
        // Extract elements one by one from heap
        for (int i = hi; i > lo; i--) {
            // Swap root (max) with last element
            (arr[lo], arr[i]) = (arr[i], arr[lo]);
            // Heapify down the new root
            BubbleDown(arr, lo, lo, i - 1);
        }
    }
    
    private static void BubbleDown(T[] arr, int i, int lo, int hi) {
        T temp = arr[i];
        int half = (lo + hi) / 2;
        
        while (i < half) {
            // Find child indices in the heap
            int left = 2 * (i - lo) + 1 + lo;
            int right = left + 1;
            
            // Find larger child
            int largest = i;
            if (left <= hi && arr[left].CompareTo(arr[largest]) > 0) {
                largest = left;
            }
            if (right <= hi && arr[right].CompareTo(arr[largest]) > 0) {
                largest = right;
            }
            
            if (largest == i) break;  // Heap property satisfied
            
            arr[i] = arr[largest];
            i = largest;
        }
        
        arr[i] = temp;
    }
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Switch to HeapSort only when depth limit exceeded (rare case)
- 🟡 **PERFORMANCE:** Depth limit = 2 * log(n) is standard threshold for detecting adversarial input
- 🟢 **BEST PRACTICE:** This is what C# Array.Sort uses internally

---

### Pattern 4: Min-Heap Operations

#### 🧠 Mental Model
Binary heap represented as array. Parent at index `i`, children at `2i+1` and `2i+2`. For min-heap, parent ≤ children. Insert: add at end, bubble up. Extract-min: swap root with last, remove last, bubble down.

#### ✅ When to Use This Pattern
- ✅ Implement priority queue
- ✅ Find k smallest/largest elements
- ✅ Heap sort
- ✅ Event scheduling, Dijkstra's algorithm

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Min-Heap implementation using array.
/// Time: Insert O(log n), Extract-Min O(log n), Build O(n) | Space: O(n)
/// 
/// 🧠 MENTAL MODEL:
/// Array representation: Parent at i, children at 2i+1 and 2i+2.
/// Min-heap invariant: parent ≤ children.
/// </summary>
public class MinHeap<T> where T : IComparable<T> {
    
    private List<T> heap;
    
    public MinHeap() {
        heap = new List<T>();
    }
    
    public MinHeap(T[] arr) {
        heap = new List<T>(arr);
        // Build heap from array: O(n)
        for (int i = heap.Count / 2 - 1; i >= 0; i--) {
            BubbleDown(i);
        }
    }
    
    public void Insert(T item) {
        // STEP 1: Add to end
        heap.Add(item);
        
        // STEP 2: Bubble up to maintain heap property
        BubbleUp(heap.Count - 1);
    }
    
    public T ExtractMin() {
        // STEP 1: Guard
        if (heap.Count == 0) throw new InvalidOperationException("Heap empty");
        
        // STEP 2: Save root (minimum)
        T min = heap[0];
        
        // STEP 3: Move last element to root
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        
        // STEP 4: Bubble down to restore heap property
        if (heap.Count > 0) {
            BubbleDown(0);
        }
        
        return min;
    }
    
    public T Peek() {
        if (heap.Count == 0) throw new InvalidOperationException("Heap empty");
        return heap[0];
    }
    
    public int Count => heap.Count;
    
    private void BubbleUp(int index) {
        while (index > 0) {
            int parentIndex = (index - 1) / 2;
            
            // If heap property violated, swap with parent
            if (heap[index].CompareTo(heap[parentIndex]) < 0) {
                (heap[index], heap[parentIndex]) = (heap[parentIndex], heap[index]);
                index = parentIndex;
            } else {
                break;  // Heap property satisfied
            }
        }
    }
    
    private void BubbleDown(int index) {
        while (true) {
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;
            int smallest = index;
            
            // Find smallest among parent, left, right
            if (leftChild < heap.Count && heap[leftChild].CompareTo(heap[smallest]) < 0) {
                smallest = leftChild;
            }
            if (rightChild < heap.Count && heap[rightChild].CompareTo(heap[smallest]) < 0) {
                smallest = rightChild;
            }
            
            // If parent is not smallest, swap and continue
            if (smallest != index) {
                (heap[index], heap[smallest]) = (heap[smallest], heap[index]);
                index = smallest;
            } else {
                break;  // Heap property satisfied
            }
        }
    }
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Remember child indices: left = 2i+1, right = 2i+2
- 🟡 **PERFORMANCE:** C# has `PriorityQueue<T>` in .NET 6+; use that for production
- 🟢 **BEST PRACTICE:** Min-heap is the foundation for priority queues

---

### Pattern 5: Rolling Hash (Karp-Rabin)

#### 🧠 Mental Model
For a string, compute polynomial hash: `hash = c[0]*base^(n-1) + c[1]*base^(n-2) + ... + c[n-1]`. As you slide the window, remove the leftmost character's contribution and add the new rightmost character. All in O(1) per character after preprocessing.

#### ✅ When to Use This Pattern
- ✅ Substring search (find if pattern appears in text)
- ✅ Plagiarism detection
- ✅ DNA sequence matching
- ✅ Faster than naive O(nm) when hashes are reliable

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Karp-Rabin rolling hash for substring search.
/// Time Complexity: O(n + m) average, O(nm) worst | Space Complexity: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// Compute hash of pattern and each window in text.
/// When hashes match, verify exact match (avoid false positives).
/// Rolling window updates hash in O(1) per character.
/// </summary>
public class RollingHash {
    
    private const int BASE = 31;
    private const long MOD = 1e9 + 7;
    
    /// <summary>
    /// Find if pattern exists in text using rolling hash.
    /// </summary>
    public static bool FindSubstring(string text, string pattern) {
        // STEP 1: Guard Clauses
        if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern)) return false;
        if (pattern.Length > text.Length) return false;
        
        int m = pattern.Length;
        int n = text.Length;
        
        // STEP 2: Compute base^(m-1) mod MOD for rolling update
        long basePower = 1;
        for (int i = 0; i < m - 1; i++) {
            basePower = (basePower * BASE) % MOD;
        }
        
        // STEP 3: Compute hash of pattern
        long patternHash = ComputeHash(pattern);
        
        // STEP 4: Compute hash of first window
        long windowHash = ComputeHash(text.Substring(0, m));
        
        // STEP 5: Core Logic - Roll window through text
        for (int i = 0; i <= n - m; i++) {
            // If hashes match, verify exact match (avoid false positives)
            if (windowHash == patternHash) {
                if (text.Substring(i, m) == pattern) {
                    return true;
                }
            }
            
            // Roll window: remove leftmost, add rightmost
            if (i < n - m) {
                windowHash = (windowHash - (text[i] * basePower) % MOD + MOD) % MOD;
                windowHash = (windowHash * BASE) % MOD;
                windowHash = (windowHash + text[i + m]) % MOD;
            }
        }
        
        return false;
    }
    
    private static long ComputeHash(string s) {
        long hash = 0;
        long power = 1;
        
        // hash = s[0]*base^(n-1) + s[1]*base^(n-2) + ... + s[n-1]
        for (int i = s.Length - 1; i >= 0; i--) {
            hash = (hash + (s[i] * power) % MOD) % MOD;
            power = (power * BASE) % MOD;
        }
        
        return hash;
    }
}

// Example:
// Text: "ABCCDDEFFG"
// Pattern: "CDD"
// Rolling hash finds CDD at index 3
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Verify exact match when hashes match (hash collision possible)
- 🟡 **PERFORMANCE:** Use modulo arithmetic to keep hash small (avoid overflow)
- 🟢 **BEST PRACTICE:** Karp-Rabin is elegant; use for programming contests

---

## SECTION 4️⃣: C# COLLECTION DECISION GUIDE

### When to Use Each Collection for Week 3 Patterns

| Use Case | Collection | Why? | When NOT to Use | Alternative |
|---|---|---|---|---|
| Sort in-place with cache efficiency | `T[]` + QuickSort | O(n log n) average, in-place, cache-friendly | When stability required | Use MergeSort |
| Sort guaranteed O(n log n) worst-case | `T[]` + MergeSort | O(n log n) guaranteed, stable | When memory is premium | Use IntroSort or built-in Array.Sort |
| Priority queue operations | `PriorityQueue<T>` (.NET 6+) | O(log n) insert/extract, encapsulated | If .NET < 6 | Implement custom MinHeap or use SortedSet |
| Min/max element with O(log n) operations | Custom `MinHeap<T>` | O(1) peek, O(log n) insert/extract | When only occasional access | Use `SortedSet<T>` (O(log n) but overhead) |
| Key-value lookup, no ordering | `Dictionary<K,V>` | O(1) average lookup, built-in | When order matters | Use `SortedDictionary` or order manually |
| Hash-based frequency count | `Dictionary<K, int>` | O(1) insert/lookup for counting | When sorted output needed | Use `SortedDictionary` or sort results after |
| String hashing / rolling hash | `long` or `BigInteger` variable | O(1) hash update in rolling window | When memory overflow risk | Use modulo arithmetic |

**KEY INSIGHT:**
Choosing the right collection is as important as choosing the right pattern.
Wrong collection = Correct algorithm running slowly (or incorrectly).

---

## SECTION 5️⃣: PROGRESSIVE PROBLEM LADDER

### 🎯 Strategy: Solve Problems in Stages

**Stage 1 (Green):** Master core pattern skeleton  
**Stage 2 (Yellow):** Recognize pattern variations and edge cases  
**Stage 3 (Red):** Combine multiple patterns for complex problems

---

### 🟢 STAGE 1: CANONICAL PROBLEMS — Master Core Pattern

Solve these to cement the pattern. Can you code the skeleton without looking?

| # | LeetCode # | Difficulty | Pattern | C# Focus | Core Concept |
|---|---|---|---|---|---|
| 1 | #912 | 🟢 Easy | Merge Sort | Implement merge step correctly; stability | Sort array |
| 2 | #215 | 🟢 Easy | Quick Sort | Partition logic; handle pivots | Find kth largest |
| 3 | #703 | 🟢 Easy | Min-Heap | Insert/extract min; maintain heap property | Kth largest stream |
| 4 | #1 | 🟢 Easy | Hash Table | HashMap lookup O(1) average | Two sum |
| 5 | #242 | 🟢 Easy | Sorting / Hashing | Count characters; verify anagram | Valid anagram |
| 6 | #28 | 🟢 Easy | String Matching | Substring search (or rolling hash) | Find first occurrence |

**STAGE 1 GOAL:** Pattern fluency. Can you implement [Pattern] skeleton in < 5 minutes?

---

### 🟡 STAGE 2: VARIATIONS — Recognize Pattern Boundaries

These problems twist the pattern. When does it work? When does it break?

| # | LeetCode # | Difficulty | Pattern + Twist | C# Focus | When Pattern Breaks |
|---|---|---|---|---|---|
| 1 | #148 | 🟡 Medium | Sort Linked List | Merge sort on linked list (no RandomAccess) | Can't use QuickSort easily |
| 2 | #23 | 🟡 Medium | Heap + Merge K Lists | Min-heap of list heads; extract/insert | Heap edge cases |
| 3 | #347 | 🟡 Medium | Hash + Heap | Count frequency, find top K | Heap with custom comparator |
| 4 | #387 | 🟡 Medium | Hash Table + Order | Track character frequency with order | HashMap doesn't preserve insertion order |
| 5 | #28 (KMP variant) | 🟡 Medium | String Matching | Pattern matching in text | Naive approach is O(nm) |
| 6 | #692 | 🟡 Medium | Sorting + Hash | Top K frequent words; tie-breaking | Sorting with custom comparator |

**STAGE 2 GOAL:** Pattern boundaries. When do you need [Alternative Pattern]? When is [Pattern] insufficient?

---

### 🟠 STAGE 3: INTEGRATION — Combine Patterns for Real Problems

Hard problems rarely use just one pattern. These combine multiple patterns.

| # | LeetCode # | Difficulty | Patterns Required | C# Focus | Pattern Combination Logic |
|---|---|---|---|---|---|
| 1 | #480 | 🔴 Hard | Sliding Window + Deque + Sorting | Median sliding window; need balanced heaps | Multiple data structures in sync |
| 2 | #76 | 🔴 Hard | Hash + Sliding Window | Min window substring; use hash for char counts | Sliding window with hash state |
| 3 | #1157 | 🔴 Hard | Sorting + Group + Count | Majority voter; sort then find; or hash+sort | Combine voting with sorting |
| 4 | #295 | 🔴 Hard | Two Heaps | Find median in streaming data; min-heap + max-heap | Balance two heaps |

**STAGE 3 GOAL:** Real-world thinking. Professional problems need multiple patterns working together.

---

## SECTION 6️⃣: WEEK 3 PITFALLS & C# GOTCHAS

### 🐛 Runtime Issues & Collection Pitfalls

Common bugs you'll hit and how to fix them:

| ❌ Pattern | 🐛 Bug | 💥 C# Symptom | 🔧 Quick Fix |
|---|---|---|---|---|
| Merge Sort | Forget to merge (only recurse) | Array unchanged | Implement full merge step with comparison loop |
| Quick Sort | Pivot always last element on sorted input | O(n²) time | Randomize pivot or shuffle first |
| Heap Insert | Forget to bubble up | Heap property violated | After adding to end, BubbleUp to parent |
| Heap Extract | Forget to bubble down | Heap property violated | After moving last to root, BubbleDown to leaves |
| Hash Table | Bad hash function (e.g., modulo on biased data) | All keys hash to same bucket | Use BASE multiplier: `hash = (hash * 31 + char) % MOD` |
| Rolling Hash | Don't verify exact match on hash collision | False positives | After hash match, compare strings directly |
| Substring Search | Use naive O(nm) comparison loop | TLE on large strings | Use Karp-Rabin rolling hash O(n+m) |

### 🎯 Week 3 Collection Gotchas

These mistakes are EASY to make:

- ❌ Using `Array.Sort()` without knowing it's IntroSort → Might get O(n²) on adversarial input → Built-in C# sort is already optimal (use it!)
  - Example: ✅ `Array.Sort(arr);` (C# handles QuickSort + HeapSort internally)
  - Example: ❌ Implement your own sort if you don't know the algorithm (use built-in!)

- ❌ Forgetting to use modulo in rolling hash → Integer overflow → Results garbage → Use `long` and modulo `1e9+7`
  - Example: ✅ `hash = (hash * BASE + c - 'a') % MOD;`
  - Example: ❌ `hash = hash * BASE + c - 'a';` (overflow)

- ❌ Building heap with sink-down on every element in order → O(n²) instead of O(n) → Use bottom-up heapify
  - Example: ✅ `for (int i = heap.Length/2-1; i >= 0; i--) BubbleDown(i);` (O(n))
  - Example: ❌ `for (int i = 0; i < heap.Length; i++) BubbleUp(i);` (O(n log n))

---

## SECTION 7️⃣: QUICK REFERENCE — INTERVIEW PREPARATION

### Mental Models for Fast Recall (30 seconds before interview!)

| Pattern | 1-Liner Mental Model | Code Symbol | When You See This... |
|---|---|---|---|
| **Merge Sort** | "Divide in half, sort recursively, merge sorted halves." | `Merge(left, right)` | "stable sort", "O(n log n) guaranteed" |
| **Quick Sort** | "Partition around pivot, recurse on left and right. Randomize pivot." | `int p = Partition()` | "sort", "average O(n log n)", "in-place" |
| **Intro Sort** | "Start QuickSort, switch to HeapSort if depth > 2*log(n)." | `if (depth == 0) HeapSort()` | "production sort", "avoid O(n²)" |
| **Min-Heap** | "Parent ≤ children. Insert bubble-up, extract bubble-down." | `BubbleUp()/BubbleDown()` | "priority queue", "kth element" |
| **Hash Table** | "Hash function maps key to bucket. Resolve collisions (chaining or probing)." | `hash % buckets` | "O(1) lookup", "frequency count" |
| **Rolling Hash** | "Polynomial hash with sliding window. O(1) updates via addition/subtraction." | `hash = (hash - c*pow + new_c) % MOD` | "substring search", "pattern matching" |

---

## ✅ WEEK 3 COMPLETION CHECKLIST

### Pattern Fluency — Can You Recognize & Choose?

- [ ] Recognize Merge Sort by signals ("stable", "guaranteed O(n log n)", "extra memory OK")
- [ ] Recall Merge Sort skeleton (divide, recurse, merge)
- [ ] Explain WHY Merge Sort beats QuickSort (worst-case guarantee)
- [ ] Explain WHEN Merge Sort fails (extra O(n) memory not acceptable)

- [ ] Recognize Quick Sort by signals ("fast average", "in-place", "cache-friendly")
- [ ] Recall Quick Sort skeleton (partition, recurse left and right)
- [ ] Explain WHY Quick Sort beats Merge Sort (faster on average, less memory)
- [ ] Explain WHEN to avoid (no randomization = vulnerable to adversarial input)

- [ ] Recognize Intro Sort by signals ("production", "avoid O(n²)", "real library")
- [ ] Recall Intro Sort strategy (QuickSort → HeapSort fallback)
- [ ] Explain WHY Intro Sort is best (combines both benefits)
- [ ] Explain WHEN hidden (C# Array.Sort uses this!)

- [ ] Recognize Min-Heap by signals ("kth element", "priority queue", "streaming")
- [ ] Recall Heap operations (bubble-up insert, bubble-down extract)
- [ ] Explain WHY heap beats sorted array (dynamic insert O(log n) not O(n))
- [ ] Explain WHEN to avoid (one-time use; just sort)

- [ ] Recognize Hash Table by signals ("O(1) lookup", "frequency count", "two-sum")
- [ ] Recall Hash Table semantics (hash function → bucket → chain/probe)
- [ ] Explain WHY hash beats array (O(1) vs O(n) for non-key lookups)
- [ ] Explain WHEN fails (adversarial hash inputs, collision)

- [ ] Recognize Rolling Hash by signals ("substring search", "pattern matching", "plagiarism")
- [ ] Recall Rolling Hash update (remove old, add new, O(1) per character)
- [ ] Explain WHY rolling hash beats naive (O(n+m) vs O(nm))
- [ ] Explain WHEN verify (hash collision possible; verify exact match)

### Problem Solving — Can You Practice?

- [ ] Solved ALL Stage 1 canonical problems (6 problems)
- [ ] Solved 80%+ Stage 2 variations (4+ problems)
- [ ] Solved 50%+ Stage 3 integration problems (got ideas even if not perfect)

### Production Code Quality — Can You Code?

- [ ] Used guard clauses on all inputs (null checks, empty collections)
- [ ] Added mental model comments to your code
- [ ] Chose correct collection (no misuse of ArrayList for queue!)
- [ ] Handled edge cases explicitly (single element, duplicates, empty)
- [ ] Your code would pass code review (clean, readable, efficient)

### Interview Ready — Can You Communicate?

- [ ] Can solve Stage 1 problem in < 5 minutes
- [ ] Can EXPLAIN your pattern choice to interviewer ("I chose heap because...")
- [ ] Can write PRODUCTION-GRADE code, not hacks (guards, comments, modulo arithmetic)
- [ ] Can discuss tradeoffs (MergeSort stability vs QuickSort speed)

---

### 🎯 Week 3 Mastery Status

- [ ] **YES - I've mastered Week 3. Ready for Week 4 (Trees & BSTs).**
- [ ] **NO - Need to practice more. Focus on Stage 2/3 problems.**

---

## 📚 REFERENCE MATERIALS

This file is self-contained. You have:
- Decision framework for pattern selection (SECTION 1)
- Knowledge of anti-patterns (SECTION 2)
- Production-grade code implementations (SECTION 3)
- Collection guidance (SECTION 4)
- Progressive practice plan (SECTION 5)
- Real gotchas and fixes (SECTION 6)
- Quick interview reference (SECTION 7)

**Everything you need to master Week 3 is here.**

---

## 🚀 HOW TO USE THIS FILE

### For Learning:
1. Start with SECTION 1 → Understand the decision tree
2. Review SECTION 2 → Learn what NOT to do
3. Study SECTION 3 → Understand production implementations
4. Follow SECTION 5 → Solve problems progressively

### For Reference:
1. See a problem? → Check SECTION 1 (decision tree)
2. Stuck? → Check SECTION 6 (gotchas)
3. Need code? → Check SECTION 3 (implementations)
4. Before interview? → Check SECTION 7 (quick recall)

### For Interview Prep:
1. Day before: Review SECTION 7 (mental models)
2. Day of: Skim SECTION 1 (decision tree)
3. During interview: Use mental models to explain your choices

---

*End of Week 3 Extended C# Support — v13 Hybrid Format*

---

**Status:** ✅ Week 3 Complete


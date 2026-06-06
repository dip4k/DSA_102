# 📘 Week 03 Support: Quick Reference & Summary

**Week:** 3 | **Category:** Quick Reference  
**Purpose:** One-page summary for rapid lookup  
**Audience:** Everyone (quick refresher)

---

## ⚡ ONE-MINUTE SUMMARY

**Week 3 covers three fundamental algorithm families:**

1. **Sorting:** From O(n²) elementary sorts to O(n log n) optimal algorithms
2. **Heaps:** Priority-based data access with implicit tree encoding
3. **Hashing:** O(1) lookup with collision resolution strategies

---

## 🎯 ALGORITHM COMPLEXITY AT A GLANCE

```
╔════════════════════════════════════════════════════════════╗
║ SORTING ALGORITHMS                                         ║
╠════════════════════════════════════════════════════════════╣
║ Algorithm    │ Best      │ Average   │ Worst    │ Space   ║
╠════════════════════════════════════════════════════════════╣
║ Bubble       │ O(n)      │ O(n²)     │ O(n²)    │ O(1)    ║
║ Selection    │ O(n²)     │ O(n²)     │ O(n²)    │ O(1)    ║
║ Insertion    │ O(n)      │ O(n²)     │ O(n²)    │ O(1)    ║
║ Merge        │ O(n log n)│ O(n log n)│ O(n log n)│ O(n)   ║
║ Quick        │ O(n log n)│ O(n log n)│ O(n²)    │ O(log n)║
║ Heap         │ O(n log n)│ O(n log n)│ O(n log n)│ O(1)   ║
║ Counting     │ O(n+k)    │ O(n+k)    │ O(n+k)   │ O(k)    ║
║ Radix        │ O(d·n)    │ O(d·n)    │ O(d·n)   │ O(n)    ║
╚════════════════════════════════════════════════════════════╝

PICK BY CONSTRAINT:
├─ Need O(n log n) guarantee? → Merge or Heap
├─ Want fastest average? → Quick Sort
├─ Already sorted? → Insertion Sort
├─ Integer keys [0,k]? → Counting or Radix
└─ Data > RAM? → External Merge Sort
```

```
╔════════════════════════════════════════════════════════════╗
║ HEAP OPERATIONS                                            ║
╠════════════════════════════════════════════════════════════╣
║ Operation       │ Time        │ Notes                      ║
╠════════════════════════════════════════════════════════════╣
║ Insert          │ O(log n)    │ Bubble-up from leaf        ║
║ Extract-Min     │ O(log n)    │ Bubble-down from root      ║
║ Find-Min        │ O(1)        │ Root is always min         ║
║ Build-Heap      │ O(n)        │ Bottom-up heapify          ║
║ Heap Sort       │ O(n log n)  │ Build O(n) + extract O(n log n) ║
╚════════════════════════════════════════════════════════════╝

KEY: Index arithmetic (no pointers, cache-friendly)
├─ Parent of i: (i-1)/2
├─ Left child of i: 2*i + 1
└─ Right child of i: 2*i + 2
```

```
╔════════════════════════════════════════════════════════════╗
║ HASH TABLE OPERATIONS                                      ║
╠════════════════════════════════════════════════════════════╣
║ Operation       │ Average     │ Worst    │ Space           ║
╠════════════════════════════════════════════════════════════╣
║ Insert          │ O(1)        │ O(n)     │ O(n) + overhead ║
║ Search          │ O(1+α)      │ O(n)     │                 ║
║ Delete          │ O(1+α)      │ O(n)     │                 ║
║ Resize          │ O(n)        │ O(n)     │ (amortized O(1))║
╚════════════════════════════════════════════════════════════╝

COLLISION RESOLUTION:
├─ Chaining: Simple, flexible α, cache-unfriendly pointers
├─ Linear Probing: Fast, cache-friendly, clustering issues
├─ Double Hashing: Perfect distribution, more complex
└─ Open Addressing generally: α ≤ 0.75 for O(1/(1-α)) ≈ O(4)
```

```
╔════════════════════════════════════════════════════════════╗
║ STRING MATCHING (RABIN-KARP)                               ║
╠════════════════════════════════════════════════════════════╣
║ Operation       │ Time            │ Notes                  ║
╠════════════════════════════════════════════════════════════╣
║ Preprocess      │ O(m)            │ Pattern hash           ║
║ Scan            │ O(n)            │ Rolling hash updates   ║
║ Verify          │ O(m × matches)  │ Rare in practice       ║
║ Total           │ O(n + m)        │ Expected               ║
║                 │ O(n × m)        │ Worst-case            ║
╚════════════════════════════════════════════════════════════╝

KEY: Rolling hash O(1) per window
├─ Remove leftmost: hash -= text[i] * base^(m-1)
├─ Shift left: hash *= base
└─ Add rightmost: hash += text[i+m]
```

---

## 🧠 DECISION TREES

### Which Sorting Algorithm?

```
Need stable?
├─ YES → Merge Sort (O(n log n), O(n) space)
└─ NO → Continue

Need in-place?
├─ YES → Quick Sort (average O(n log n), O(log n) stack)
└─ NO → Merge Sort

Keys are integers [0, k]?
├─ YES → Counting (k < n log n) or Radix (k large)
└─ NO → Continue

Data nearly sorted?
├─ YES → Insertion Sort (O(n) best case)
└─ NO → Quick Sort (general choice)
```

### Which Collision Resolution?

```
Python/Java interpreter?
├─ YES → Separate Chaining (simple, flexible)
└─ NO → Continue

C++/Systems code?
├─ YES → Linear Probing (cache-friendly)
└─ NO → Continue

Worried about hash flooding?
├─ YES → Universal Hashing (randomized seed)
└─ NO → Standard hash function

Need theoretical perfection?
├─ YES → Double Hashing (optimal distribution)
└─ NO → Linear Probing (practical)
```

---

## 📊 COMPARISON MATRICES (PRINTABLE)

### When to Use Each Sorting Algorithm

| Scenario | Best | Why |
|----------|------|-----|
| Small n (<50) | Insertion | Low constants |
| Nearly sorted | Insertion | O(n) best case |
| Need stable | Merge | Only good stable O(n log n) |
| Worst-case guarantee | Merge/Heap | No O(n²) risk |
| Fastest average | Quick | Lowest constants |
| Integers [0,k] | Counting/Radix | Beats O(n log n) |
| Memory-limited | Heap/Quick | O(1) or O(log n) space |
| Cache-critical | Quick | Random access pattern less harmful than many swaps |
| Large data > RAM | External Merge | Minimizes disk I/O |
| Library function | Quick (with fallback) | Introsort = Quick + Heap |

### When to Use Each Hash Collision Resolution

| Scenario | Best | Why |
|----------|------|-----|
| Interpreter language | Chaining | Simple to implement |
| Performance critical | Linear Probing | Cache-friendly |
| Theoretical interest | Double Hashing | Perfect distribution |
| Hash flooding risk | Universal Hash | Adversarial-proof |
| Sparse data | Chaining | Less memory waste |
| Dense data (α > 0.75) | Resize table | Keep α constant |

---

## ⚡ CODE SNIPPETS (COPY-PASTE READY)

### Quick Sort Template

```csharp
void QuickSort(int[] arr) {
    QuickSortHelper(arr, 0, arr.Length - 1);
}

void QuickSortHelper(int[] arr, int low, int high) {
    if (low < high) {
        int p = Partition(arr, low, high);
        QuickSortHelper(arr, low, p - 1);
        QuickSortHelper(arr, p + 1, high);
    }
}

int Partition(int[] arr, int low, int high) {
    int pivot = arr[high];
    int i = low - 1;
    for (int j = low; j < high; j++) {
        if (arr[j] < pivot) {
            i++;
            (arr[i], arr[j]) = (arr[j], arr[i]);
        }
    }
    (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
    return i + 1;
}
```

### Heap Insert Template

```csharp
void Insert(int value) {
    heap.Add(value);
    int i = heap.Count - 1;
    
    while (i > 0) {
        int parent = (i - 1) / 2;
        if (heap[parent] >= heap[i]) break;
        
        (heap[i], heap[parent]) = (heap[parent], heap[i]);
        i = parent;
    }
}
```

### Hash Table with Separate Chaining Template

```csharp
void Insert(K key, V value) {
    int idx = Hash(key);
    var bucket = buckets[idx];
    
    for (int i = 0; i < bucket.Count; i++) {
        if (bucket[i].Key.Equals(key)) {
            bucket[i] = (key, value);
            return;
        }
    }
    
    bucket.Add((key, value));
    count++;
    
    if ((float)count / buckets.Length > 0.75f) {
        Resize();
    }
}

bool TryGetValue(K key, out V value) {
    int idx = Hash(key);
    
    foreach (var (k, v) in buckets[idx]) {
        if (k.Equals(key)) {
            value = v;
            return true;
        }
    }
    
    value = default!;
    return false;
}
```

### Rabin-Karp Rolling Hash Template

```csharp
const int PRIME = 101, BASE = 256;

long ComputeHash(string text, int start, int length) {
    long hash = 0, basePower = 1;
    for (int i = start + length - 1; i >= start; i--) {
        hash = (hash + (text[i] * basePower) % PRIME + PRIME) % PRIME;
        basePower = (basePower * BASE) % PRIME;
    }
    return hash;
}

long RollingHash(string text, long prevHash, int start, int length, long basePowerM) {
    long hash = (prevHash - (text[start] * basePowerM) % PRIME + PRIME) % PRIME;
    hash = (hash * BASE) % PRIME;
    hash = (hash + text[start + length] % PRIME + PRIME) % PRIME;
    return hash;
}
```

---

## 🚀 PERFORMANCE RULES OF THUMB

```
SORTING:
├─ n < 50: Use insertion sort (low constants)
├─ 50 ≤ n < 10,000: Use quick sort (fast average)
├─ n ≥ 10,000: Use quick sort or merge sort
├─ If already sorted: Use insertion sort
└─ If memory critical: Use heap sort

HEAPS:
├─ Use for priority queues (not sorting)
├─ Build-heap is O(n), not O(n log n)
├─ Extract-all is O(n log n) (sorted output)
└─ For Dijkstra: O((V+E) log V) with heap vs O(V²) without

HASHING:
├─ Keep load factor α ≤ 0.75
├─ Expected search is O(1 + α), so O(1) when α constant
├─ Resize triggers O(n) cost, amortized to O(1) per insert
├─ Use randomized hash seed against adversaries
└─ Rolling hash O(n+m) vs naive O(n×m) for strings
```

---

## ✅ CHECKLISTS

### Before Submitting Sort Code

- [ ] Handles empty array
- [ ] Handles single element
- [ ] Handles duplicates correctly
- [ ] Correct stability (if required)
- [ ] Correct in-place constraint (if required)
- [ ] Time complexity matches specification
- [ ] No off-by-one errors in loops
- [ ] Tested on already-sorted data

### Before Submitting Hash Table Code

- [ ] Handles collisions
- [ ] Resize logic correct
- [ ] Load factor maintained
- [ ] Insert, search, delete all work
- [ ] Tested with adversarial keys
- [ ] No memory leaks (if using pointers)
- [ ] Hash function has good distribution

### Before Submitting Heap Code

- [ ] Parent ≥ children maintained (max-heap)
- [ ] Parent ≤ children maintained (min-heap)
- [ ] Index arithmetic correct
- [ ] No off-by-one in parent/child calculations
- [ ] Extract removes root correctly
- [ ] Bubble-up and bubble-down correct

---

**Document Status:** ✅ COMPLETE  
**Format:** Printable, copy-paste ready  
**Purpose:** Rapid reference for Week 03 concepts



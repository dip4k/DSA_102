# 📚 Week 05 Day 01: Hash Map / Hash Set Patterns — Engineering Guide

> 🧭 **Navigation:** [← Week Overview](README.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_05_Day_02_Monotonic_Stack_Patterns_Instructional.md)
> 
> 💡 **Instructor Note:** *Not all sections or topics are mandatory. Adapt your pace and skim or focus based on your target interview timeline.*

---

## 🎯 LEARNING OBJECTIVES

By the end of this chapter, you will be able to:

- 🎯 **Internalize** the three core hash-based patterns: complement lookup, frequency counting, and membership deduplication.
- ⚙️ **Implement** Two-Sum, Top-K Frequent, Group Anagrams, and Valid Anagram in modern C# (.NET 8/9) and idiomatic Python (3.11+).
- ⚖️ **Evaluate** trade-offs between hash-based `O(N)` average-time approaches and sorting-based `O(N log N)` / two-pointer approaches.
- 🏭 **Connect** hash structures to real production engines (Redis, in-memory caches, database index lookups).
- 🎙️ **Articulate** invariants, collision resolution, and space-time trade-offs cleanly across a 45-minute technical interview.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Engineering Problem

Modern computing constantly handles queries over massive datasets: pairing complementary transaction records, detecting duplicate event IDs in streaming pipelines, or grouping items by shared attributes. A naive brute-force search comparing every pair takes `O(N^2)` time—completely unviable when processing millions of items per second.

Sorting can reduce pairing problems to `O(N log N)` with `O(1)` space via two pointers, but sorting requires data mutability and ordered comparison. When elements are unordered or arrive in real-time streams, hash tables provide the ultimate solution: **trading `O(N)` auxiliary space for `O(1)` average-case lookups**.

> [!NOTE]
> **Production Reality (Why FAANG Tests This):**
> Distributed key-value stores like Redis and Memcached, alongside database hash indexes (PostgreSQL, MySQL), achieve sub-millisecond retrieval by mapping arbitrary keys to bucket indices in `O(1)` average time. Interviewers evaluate whether you instinctively reach for hash-based complement tracking and frequency grouping instead of defaulting to expensive `O(N^2)` scans or unnecessary `O(N log N)` sorts.

### The Solution: Hash-Based Thinking

Hash patterns systematically answer three foundational query types:
1. **Complement Verification:** "Does `target - current` already exist in history?"
2. **Frequency Aggregation:** "How many times does each distinct element appear, and which exceed a threshold?"
3. **Membership Deduplication:** "Have I encountered this entity or state before?"

By maintaining a dynamic lookup structure during a single linear scan, each query executes in `O(1)` amortized time.

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### The Core Analogy

Think of a hash table as an array of numbered mail cubbies in a central post office. Instead of walking through every shelf searching for mail addressed to "Alice", a deterministic postal formula (the hash function) immediately calculates the exact cubby number from the name string. You jump directly to that cubby in `O(1)` time. If multiple people map to the same cubby (a hash collision), they are neatly organized in a short chained list inside that specific cubby.

### 🖼 Visualizing Hash Buckets & Separate Chaining

```
Key ("alice")   ──> hash("alice")   % 8 ──> Index 3
Key ("carol")   ──> hash("carol")   % 8 ──> Index 3 (Collision chained)
Key ("bob")     ──> hash("bob")     % 8 ──> Index 1

Array of Buckets (Size = 8):
Index
 [0] ──> null
 [1] ──> [ "bob" : 15 ] ──> null
 [2] ──> null
 [3] ──> [ "alice" : 42 ] ──> [ "carol" : 99 ] ──> null
 [4] ──> [ "dave" : 8 ] ──> null
 [5] ──> null
 [6] ──> [ "eve" : 12 ] ──> null
 [7] ──> null
```

### Invariants & Properties

1. **Deterministic Indexing Invariant:** Given key `K`, `hash(K)` always evaluates to the same integer value throughout process execution.
2. **Localized Search Invariant:** If key `K` exists in the table, it resides strictly within bucket `hash(K) % bucket_count`.
3. **Amortized `O(1)` Complexity:** When the load factor `alpha = N / buckets` remains below threshold (typically 0.75), bucket chains remain short (`O(1)` length). When exceeded, the table doubles bucket capacity and rehashes in amortized `O(1)` time per insertion.

### Taxonomy of Hash Patterns

| Pattern Variant | Core Mechanism | Key Operations | Time | Space | Canonical Example |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Complement Lookup** | Check for `target - x` before inserting `x` | `TryGetValue`, `ContainsKey` | `O(N)` | `O(N)` | Two Sum (LeetCode 1) |
| **Frequency Counting** | Tally occurrences, group by count | `dict[k]++`, Bucket Sort | `O(N)` | `O(N)` | Top K Frequent (LeetCode 347) |
| **Canonical Keying** | Normalize strings into sorted/tuple signature | String sorting, count signature | `O(N * K)` | `O(N * K)` | Group Anagrams (LeetCode 49) |
| **Frequency Array** | Direct index mapping via ASCII offset | `arr[c - 'a']++` | `O(N)` | `O(1)` | Valid Anagram (LeetCode 242) |

---

## 🔧 CHAPTER 3: CORE PATTERN MECHANICS & ASCII TRACES

### Complement Lookup Mechanics (Two Sum)

```
Input: nums = [2, 7, 11, 15], target = 9
Lookup Map: Dictionary<number, index>

Step 0 (i = 0, num = 2):
  Complement needed = 9 - 2 = 7
  Map lookup: ContainsKey(7)? -> FALSE
  Insert: Map[2] = 0
  State: { 2: 0 }

Step 1 (i = 1, num = 7):
  Complement needed = 9 - 7 = 2
  Map lookup: ContainsKey(2)? -> TRUE (Found at index 0)
  Return: [ Map[2], 1 ] => [0, 1]  <-- MATCH TERMINATES IN 1 PASS
```

### Frequency Bucketing Mechanics (Top K Frequent)

```
Input: nums = [1, 1, 1, 2, 2, 3], k = 2

Phase 1: Frequency Count Map
  counts = { 1: 3, 2: 2, 3: 1 }

Phase 2: Array of Buckets (Index = Frequency, Max Freq = N = 6)
  Bucket Index:  [0]   [1]     [2]     [3]     [4]    [5]    [6]
  Elements:      []    [3]     [2]     [1]     []     []     []

Phase 3: Reverse Scan from Frequency 6 down to 1:
  Freq 3: pick 1  -> result = [1]
  Freq 2: pick 2  -> result = [1, 2]  (result.Count == k == 2 -> STOP)
```

---

## 💻 CHAPTER 4: PRODUCTION-GRADE IMPLEMENTATIONS (C# & PYTHON)

### Problem 1: Two Sum (LeetCode 1) — Complement Lookup

#### 🎙️ 45-Minute Interview Talk Track
> *"A brute force comparison of all pairs requires `O(N^2)` time. While sorting enables opposite-direction two pointers in `O(N log N)` time, it disrupts original array indices and requires index-tracking objects. Instead, we can achieve optimal `O(N)` time with a single pass using a hash map. For each number, we compute its complement: `target - num`. We check if the complement already exists in our dictionary. If present, we immediately return the stored complement's index and current index. Otherwise, we record the current number and index in the dictionary. This maintains the invariant that each element checks only against previously examined elements, handling duplicate values gracefully without self-matching."*

#### C# Primary Implementation (.NET 8/9 — Production-Grade)
```csharp
using System;
using System.Collections.Generic;

public static class TwoSumSolver
{
    /// <summary>
    /// Finds two indices whose values sum to target using a one-pass hash map complement lookup.
    /// Time Complexity: O(N) | Auxiliary Space: O(N) | Output Space: O(1)
    /// </summary>
    public static int[] TwoSum(int[] nums, int target)
    {
        ArgumentNullException.ThrowIfNull(nums);
        if (nums.Length < 2) return [];

        // Pre-size dictionary to avoid rehashing allocations
        var map = new Dictionary<int, int>(nums.Length);

        for (int i = 0; i < nums.Length; i++)
        {
            int complement = target - nums[i];

            if (map.TryGetValue(complement, out int complementIndex))
            {
                return [complementIndex, i];
            }

            // Store current value with its index
            map[nums[i]] = i;
        }

        return [];
    }
}
```

#### Python Secondary Implementation (3.11+ — Clean & Idiomatic)
```python
def two_sum(nums: list[int], target: int) -> list[int]:
    """Finds two indices whose values sum to target in single linear pass.

    Time Complexity: O(N) | Auxiliary Space: O(N) | Output Space: O(1)
    """
    seen: dict[int, int] = {}
    for i, num in enumerate(nums):
        complement = target - num
        if complement in seen:
            return [seen[complement], i]
        seen[num] = i
    return []
```

#### 📊 Explicit Complexity Deconstruction
* **Time Complexity:** `O(N)` — Single pass evaluating at most `N` elements with `O(1)` average dictionary lookup and insertion.
* **Auxiliary Space:** `O(N)` — Stores up to `N` key-value pairs in the dictionary in worst case (no matching pair until end).
* **Output Space:** `O(1)` — Returns a fixed 2-element array.

---

### Problem 2: Top K Frequent Elements (LeetCode 347) — Bucket Sort

#### 🎙️ 45-Minute Interview Talk Track
> *"The naive approach sorts the unique element counts, taking `O(U log U)` where `U` is unique elements. A min-heap of size `K` improves this to `O(N log K)`. However, we can achieve strictly linear `O(N)` time using bucket sort. Because an element's frequency can never exceed the total array size `N`, we construct an array of buckets where the index represents frequency `[0..N]`. We first tally counts with a hash map in `O(N)`. Next, we place each number into its frequency bucket. Finally, we iterate backward from frequency `N` down to 1, collecting elements until we gather `K` values. This avoids all comparison sorting."*

#### C# Primary Implementation (.NET 8/9 — Production-Grade)
```csharp
using System;
using System.Collections.Generic;

public static class TopKFrequentSolver
{
    /// <summary>
    /// Returns the k most frequent elements using bucket sort.
    /// Time Complexity: O(N) | Auxiliary Space: O(N) | Output Space: O(K)
    /// </summary>
    public static int[] TopKFrequent(int[] nums, int k)
    {
        ArgumentNullException.ThrowIfNull(nums);
        if (nums.Length == 0 || k <= 0) return [];

        // Step 1: Count element frequencies
        var counts = new Dictionary<int, int>();
        foreach (int num in nums)
        {
            counts[num] = counts.GetValueOrDefault(num, 0) + 1;
        }

        // Step 2: Bucket numbers by frequency (max possible frequency is nums.Length)
        var buckets = new List<int>[nums.Length + 1];
        foreach (var (num, freq) in counts)
        {
            buckets[freq] ??= [];
            buckets[freq].Add(num);
        }

        // Step 3: Gather top k elements from highest to lowest frequency
        var result = new int[k];
        int writeIndex = 0;

        for (int freq = buckets.Length - 1; freq > 0 && writeIndex < k; freq--)
        {
            if (buckets[freq] is null) continue;

            foreach (int num in buckets[freq])
            {
                result[writeIndex++] = num;
                if (writeIndex == k) break;
            }
        }

        return result;
    }
}
```

#### Python Secondary Implementation (3.11+ — Clean & Idiomatic)
```python
from collections import Counter


def top_k_frequent(nums: list[int], k: int) -> list[int]:
    """Finds k most frequent elements in linear time via frequency buckets.

    Time Complexity: O(N) | Auxiliary Space: O(N) | Output Space: O(K)
    """
    counts = Counter(nums)
    buckets: list[list[int]] = [[] for _ in range(len(nums) + 1)]

    for num, freq in counts.items():
        buckets[freq].append(num)

    result: list[int] = []
    for freq in range(len(buckets) - 1, 0, -1):
        for num in buckets[freq]:
            result.append(num)
            if len(result) == k:
                return result
    return result
```

#### 📊 Explicit Complexity Deconstruction
* **Time Complexity:** `O(N)` — `O(N)` pass to count frequencies, `O(U)` where `U <= N` to bucket elements, and `O(N)` reverse pass to collect `K` elements.
* **Auxiliary Space:** `O(N)` — The frequency dictionary holds `U <= N` entries; bucket lists store exactly `U` integers across `N + 1` buckets.
* **Output Space:** `O(K)` — Returns an array containing the `K` most frequent values.

---

### Problem 3: Group Anagrams (LeetCode 49) — Categorization via Canonical Form

#### 🎙️ 45-Minute Interview Talk Track
> *"Two words are anagrams if and only if they contain the identical multiset of characters. To group them efficiently, we map each string to a canonical signature key. Sorting each string of length `K` produces a normalized key in `O(K log K)`. Alternatively, for lowercase English strings, a 26-element character frequency tuple produces a key in `O(K)`. We use this signature as the dictionary key and append matching original strings into the corresponding list. Grouping `N` strings of average length `K` runs in `O(N * K log K)` or `O(N * K)` time and linear space."*

#### C# Primary Implementation (.NET 8/9 — Production-Grade)
```csharp
using System;
using System.Collections.Generic;

public static class GroupAnagramsSolver
{
    /// <summary>
    /// Groups words into anagram collections using sorted string canonical keys.
    /// Time Complexity: O(N * K log K) | Auxiliary Space: O(N * K) | Output Space: O(N * K)
    /// </summary>
    public static IList<IList<string>> GroupAnagrams(string[] strs)
    {
        ArgumentNullException.ThrowIfNull(strs);
        if (strs.Length == 0) return [];

        var groups = new Dictionary<string, List<string>>();

        foreach (string s in strs)
        {
            char[] chars = s.ToCharArray();
            Array.Sort(chars);
            string key = new(chars);

            if (!groups.TryGetValue(key, out var list))
            {
                list = [];
                groups[key] = list;
            }
            list.Add(s);
        }

        var result = new List<IList<string>>(groups.Count);
        foreach (var list in groups.Values)
        {
            result.Add(list);
        }
        return result;
    }
}
```

#### Python Secondary Implementation (3.11+ — Clean & Idiomatic)
```python
from collections import defaultdict


def group_anagrams(strs: list[str]) -> list[list[str]]:
    """Groups strings by anagram relationship using sorted tuple signatures.

    Time Complexity: O(N * K log K) | Auxiliary Space: O(N * K) | Output Space: O(N * K)
    """
    groups: dict[str, list[str]] = defaultdict(list)
    for s in strs:
        key = "".join(sorted(s))
        groups[key].append(s)
    return list(groups.values())
```

#### 📊 Explicit Complexity Deconstruction
* **Time Complexity:** `O(N * K log K)` where `N` is number of strings and `K` is maximum string length. Sorting each string takes `O(K log K)`.
* **Auxiliary Space:** `O(N * K)` — The dictionary stores canonical key strings and list pointers for all `N` items.
* **Output Space:** `O(N * K)` — Returns nested lists containing all `N` strings.

---

### Problem 4: Valid Anagram (LeetCode 242) — Fixed-Array Frequency Matching

#### 🎙️ 45-Minute Interview Talk Track
> *"To verify whether string `t` is an anagram of `s`, we first assert length equality: if `s.Length != t.Length`, they cannot be anagrams (`O(1)` rejection). We then use a fixed-size 26-element integer frequency buffer allocated on the stack. In a single pass, we increment counts for characters in `s` and decrement counts for characters in `t`. Finally, if any bucket count is non-zero, the strings differ in frequency. This runs in strict `O(N)` time with `O(1)` auxiliary space, eliminating heap allocations."*

#### C# Primary Implementation (.NET 8/9 — Zero Allocation)
```csharp
using System;

public static class ValidAnagramSolver
{
    /// <summary>
    /// Determines whether s and t are anagrams using stack-allocated ASCII frequency buffer.
    /// Time Complexity: O(N) | Auxiliary Space: O(1) | Output Space: O(1)
    /// </summary>
    public static bool IsAnagram(string s, string t)
    {
        if (s.Length != t.Length) return false;

        Span<int> charCounts = stackalloc int[26];

        for (int i = 0; i < s.Length; i++)
        {
            charCounts[s[i] - 'a']++;
            charCounts[t[i] - 'a']--;
        }

        foreach (int count in charCounts)
        {
            if (count != 0) return false;
        }

        return true;
    }
}
```

#### Python Secondary Implementation (3.11+ — Clean & Idiomatic)
```python
from collections import Counter


def is_anagram(s: str, t: str) -> bool:
    """Verifies anagram relationship using character frequency tallying.

    Time Complexity: O(N) | Auxiliary Space: O(1) (fixed 26-char alphabet) | Output Space: O(1)
    """
    if len(s) != len(t):
        return False
    return Counter(s) == Counter(t)
```

#### 📊 Explicit Complexity Deconstruction
* **Time Complexity:** `O(N)` — Single pass over strings of length `N`, followed by constant `26`-iteration verification scan.
* **Auxiliary Space:** `O(1)` — A fixed 26-element array (stack-allocated in C#).
* **Output Space:** `O(1)` — Returns a single boolean.

---

## ⚖️ CHAPTER 5: PERFORMANCE, TRADE-OFFS & REAL SYSTEMS

### Cache Locality vs. Big-O Reality

On paper, hash table lookups are `O(1)` while binary search over a sorted array is `O(log N)`. In physical hardware:
- **Array Traversal:** Elements are stored in contiguous memory. When element `nums[i]` is loaded, the hardware prefetcher pulls entire 64-byte cache lines into L1/L2 CPU caches, minimizing latency.
- **Hash Table Traversal:** Each lookup hashes a key, jumps to an arbitrary memory address (bucket), and potentially chases pointers down a collision chain. This incurs CPU cache misses.

However, when `N` exceeds `10^4`, algorithmic complexity dominates: `O(N)` hash operations drastically outperform `O(N log N)` comparisons or `O(N^2)` scans regardless of cache misses.

### Memory Overhead Comparison

| Structure | Element Size | Total Overhead per Entry | Total RAM for 1,000,000 Entries |
| :--- | :--- | :--- | :--- |
| Contiguous `int[]` | 4 bytes | 0 bytes | **4 MB** |
| `Dictionary<int, int>` / `dict` | 8 bytes (key+val) | ~24-32 bytes (hash code, next pointer, bucket table) | **~36-48 MB** |

> [!NOTE]
> **Production Reality (Why FAANG Tests This):**
> Memory trade-offs matter in real infrastructure. Caches like Redis trade 10x higher RAM footprint per entry for deterministic sub-millisecond retrieval. In resource-constrained microservices, engineers choose fixed frequency arrays (`int[26]` or `int[256]`) instead of generic hash maps to achieve zero heap allocations and 100% L1 cache hits.

### Failure Modes & Defensive Engineering

1. **Hash Flooding Attacks:** Malicious users submitting keys with identical hash prefixes can degrade a hash table from `O(1)` to `O(N)` worst-case chained scan. Modern platforms (.NET and Python) randomize hash seeds per process invocation to prevent deterministic hash flooding.
2. **Missing Key Exceptions:** Direct index access (`map[key]`) in C# throws `KeyNotFoundException`. Always use `TryGetValue(key, out var val)` or `GetValueOrDefault()`.
3. **Mutable Object Keys:** If an object is used as a hash key and subsequently modified, its hash code changes. The entry becomes permanently lost in the bucket structure, causing catastrophic memory leaks. Always use immutable primitives or records as keys.

---

## 🎯 CHAPTER 6: FAANG INTERVIEW PATTERN SIGNALS & EDGE CASES

### 🎯 Pattern Recognition Signals
- ✅ **"Find two numbers that sum to target" (unsorted)** -> Single-pass Hash Map complement lookup (`O(N)` time).
- ✅ **"Count frequencies" / "Find Top K frequent"** -> Hash Map frequency count + Bucket Sort (`O(N)` time).
- ✅ **"Group words by identical letters"** -> Canonical signature keying (sorted word or frequency tuple).
- ✅ **"Check if element appeared before"** -> Hash Set deduplication.
- 🛑 **"Array is already sorted"** -> Do NOT use a hash table. Use opposite-direction two pointers (`O(1)` space).

### 🧪 Concrete Edge-Case Checklist
1. **Empty Array or Insufficient Elements (`N < 2`):** Guard clause must return empty result immediately.
2. **Duplicate Complements (`nums = [3, 3]`, `target = 6`):** Check complement before inserting current value to allow valid duplicate pairs without self-pairing.
3. **Case Sensitivity & Character Sets:** Clarify ASCII vs. Unicode (lowercase `a-z` vs. full UTF-8) before sizing fixed-frequency arrays.
4. **Extreme Numerical Ranges:** When summing numbers, beware of 32-bit integer overflow; cast to 64-bit `long` if values approach `2^31 - 1`.

---

## ⚔️ SUPPLEMENTARY OUTCOMES

### 🏋️ Practice Problems

| # | Problem | Source | Difficulty | Key Concept |
| :--- | :--- | :--- | :--- | :--- |
| 1 | Two Sum | LeetCode 1 | 🟢 Easy | Complement lookup (`target - x`) |
| 2 | Contains Duplicate | LeetCode 217 | 🟢 Easy | Set membership deduplication |
| 3 | Valid Anagram | LeetCode 242 | 🟢 Easy | Fixed frequency array buffer |
| 4 | Majority Element | LeetCode 169 | 🟢 Easy | Frequency counting / Boyer-Moore |
| 5 | Group Anagrams | LeetCode 49 | 🟡 Medium | Canonical key grouping |
| 6 | Top K Frequent Elements | LeetCode 347 | 🟡 Medium | Frequency count + Bucket sort |
| 7 | Longest Consecutive Sequence | LeetCode 128 | 🟡 Medium | Hash set boundary expansion |
| 8 | Subarray Sum Equals K | LeetCode 560 | 🟡 Medium | Prefix sum + Hash map complement |

### 🎙️ Interview Questions (Verbal Drills)

1. **Q:** Why is Two Sum optimal with a hash map compared to sorting and using two pointers?
   - **Answer:** Sorting costs `O(N log N)` time. A hash map solves the problem in `O(N)` time. If the array is already sorted, two pointers becomes optimal because it uses `O(1)` space without the sorting penalty.
2. **Q:** What is the worst-case time complexity of a hash table lookup, and when does it occur?
   - **Answer:** Worst case is `O(N)` when all keys collide into a single bucket. Modern engines mitigate this using randomized seed hashing and converting long chains into balanced trees.
3. **Q:** How does bucket sort achieve `O(N)` for Top K Frequent without violating comparison sort lower bounds (`O(N log N)`)?
   - **Answer:** Bucket sort is non-comparative. Because element frequencies are bounded integers between 1 and `N`, we can map frequencies directly to array indices in `O(1)` time.
4. **Q:** Why should you avoid using mutable objects as keys in a hash map?
   - **Answer:** If key fields mutate after insertion, the object's `GetHashCode()` changes. The hash table will search the wrong bucket on subsequent lookups, resulting in phantom misses and memory leaks.

### ❌ Common Misconceptions

- **Myth:** Hash table lookups are guaranteed `O(1)` in all situations.  
  *Reality:* `O(1)` is an amortized average. Pathological collision distributions degrade operations to `O(N)`.
- **Myth:** Hash maps are always faster than arrays for small collections.  
  *Reality:* For small `N <= 30`, linear scanning over a contiguous array often outperforms a hash table due to zero hashing overhead and optimal CPU cache line prefetching.
- **Myth:** You must sort the input array before solving Top K Frequent.  
  *Reality:* Frequency counting followed by bucket sort solves Top K in linear `O(N)` time without sorting input elements.

### 🚀 Advanced Concepts

1. **Bloom Filters:** Space-efficient probabilistic bit arrays used in database engines (Google BigTable, RocksDB) to test set membership with zero false negatives and controllable false positive rates.
2. **Consistent Hashing:** Partitioning strategy used in distributed caches (Memcached, Amazon DynamoDB) that minimizes key remapping when server nodes join or fail.
3. **Robin Hood Hashing:** Open-addressing collision resolution that minimizes variance in probe sequence lengths by stealing slots from "rich" elements for "poor" elements.

---

## 📌 CLOSING REFLECTION

Hash tables embody the core software engineering principle of **space-time optimization**. By spending auxiliary memory to build an indexed state representation during a linear pass, complex multi-pass searches collapse into instant `O(1)` queries. Master the complement lookup and frequency bucketing patterns, and you unlock the foundation for 40%+ of all technical interview problems.

---
> 🧭 **Navigation:** [← Week Overview](README.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_05_Day_02_Monotonic_Stack_Patterns_Instructional.md)

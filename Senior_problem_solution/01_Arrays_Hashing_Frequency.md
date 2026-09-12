# Phase 01: Arrays, Hashing & Frequency

> **Focus:** Hash Maps (`Dictionary<TKey, TValue>`), Hash Sets (`HashSet<T>`), Frequency Counting, Canonical Key Grouping, Array Invariants, and Space-Time Tradeoffs.  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 1 (Problems #1–#7)

---


## 1. Two Sum (LeetCode #1)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#hash-map` `#complement-lookup` `#array` `#one-pass` `#two-pointers` |
| **LeetCode Link** | [Two Sum](https://leetcode.com/problems/two-sum/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array of integers `nums` and an integer `target`, return indices of the two numbers such that they add up to `target`.
- **Assumptions & Contracts:**
  - Exactly one valid answer exists in the input array.
  - You may not use the same element twice (i.e., indices must be distinct: $i \neq j$).
  - Return the indices in any order.
- **Key Constraints:**
  - `2 <= nums.Length <= 10^4`
  - `-10^9 <= nums[i] <= 10^9`
  - `-10^9 <= target <= 10^9`
- **Senior Edge Cases to Defend:**
  - Duplicate values forming the target: E.g., `nums = [3, 3]`, `target = 6`. The algorithm must not overwrite the first index before evaluating the pair, or reuse index 0 twice.
  - Negative values with positive target: E.g., `nums = [-1, 5]`, `target = 4`. Arithmetic must preserve signed values without overflow surprises.
  - Target zero: E.g., `nums = [-3, 3]`, `target = 0` or `nums = [0, 4, 3, 0]`, `target = 0`.
  - Distant pairs: The complement resides at index 0 and index $N - 1$.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Find two distinct elements $a$ and $b$ such that $a + b = \text{target} \iff b = \text{target} - a$. Convert a quadratic $O(N^2)$ forward-scanning search into an amortized $O(1)$ historical lookup using an associative map.
- **Sample 1:**
  - **Input:** `nums = [2, 7, 11, 15]`, `target = 9`
  - **Output:** `[0, 1]`
  - **Explanation:** `nums[0] + nums[1] = 2 + 7 = 9`.
- **Sample 2 (Duplicate Values):**
  - **Input:** `nums = [3, 2, 4, 3]`, `target = 6`
  - **Output:** `[0, 3]`
  - **Explanation:** `nums[0] + nums[3] = 3 + 3 = 6`.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine standing by a high-speed airport luggage carousel. You hold a locked suitcase and need its exact key. The naive person waits for every single key to pass, grabs each one, and tests it against every other key on the belt—an exhausting quadratic chore. The senior engineer holds a single notebook. As each key glides past, you calculate the *exact unique shape* of the key that would unlock your suitcase: `complement = target - current`. You check your notebook in an instant: *"Did I see this complement earlier?"* If yes, you immediately grab both and exit. If not, you write down the current key's shape and position in your notebook. The problem transforms from searching the uncertain future to indexing the immutable past.

#### 3.2 The Naive Bottleneck & Redundant Computation
The brute-force algorithm utilizes two nested loops:
```csharp
for (int i = 0; i < n; i++)
    for (int j = i + 1; j < n; j++)
        if (nums[i] + nums[j] == target) return new int[] { i, j };
```
- **Total Comparisons:** $\frac{N(N - 1)}{2} = O(N^2)$.
- **Redundant Scans:** For every candidate $i$, the inner loop scans every element $j > i$ afresh, with zero memory of previous scans. When $nums[i] + nums[j] \neq \text{target}$, the CPU completely discards this failed equality test. It re-evaluates the same numbers over and over again in different pairings. The search space is an upper triangular $N \times N$ matrix where each cell is probed naively without exploiting algebraic symmetry.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Algebraic Inversion:** $a + b = \text{target} \iff b = \text{target} - a$.
- **Complement Lookup Invariant:** A pair $(i, j)$ with $i < j$ exists such that $nums[i] + nums[j] = \text{target}$. When our traversal cursor reaches index $j$, index $i$ has *already* been visited, processed, and registered in our historical hash map `seen`.
- **Discard Safety:** By verifying the complement at index $j$ against `seen`, we never need to look ahead into $[j + 1 \dots N - 1]$. If $nums[j]$ is part of the unique solution with some future element $k > j$, it will be safely discovered when the cursor arrives at $k$, because $nums[j]$ will have been stored in `seen`. Thus, advancing the cursor without looking ahead is 100% safe.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
At any execution step where cursor `i` points to candidate `nums[i]`:
- `i`: Active read cursor traversing strictly left-to-right from $0$ to $N - 1$.
- `seen`: Hash map storing `{ value -> index }` for all indices $k \in [0, i - 1]$.

```text
[ 0 ................... i - 1 ]      [    i    ]      [ i + 1 .............. N - 1 ]
└──────────────┬──────────────┘      └────┬────┘      └──────────────┬──────────────┘
      Settled & Indexed in 'seen'        Current                Unexplored stream
      (Historical Knowledge Base)    Target Probe
```

- **Invariant:**
  1. For every entry `(val, idx)` in `seen`, `idx < i`.
  2. No two elements in `seen` sum to `target` (otherwise the algorithm would have terminated earlier).
  3. If the solution pair is $(p, q)$ with $p < q$, the solution is detected at the exact instant $i = q$.

#### 3.5 State Transition Triggers & Decision Gates
1. **Compute Complement:** `complement = target - nums[i]`.
2. **Decision Gate 1 (Terminal Hit):**
   - Query: `seen.TryGetValue(complement, out int prevIdx)`.
   - Action: If found, terminate immediately. Return `new int[] { prevIdx, i }`.
3. **Decision Gate 2 (Miss / Ingestion):**
   - Query: Complement not in `seen`.
   - Action: Set `seen[nums[i]] = i`. Advance cursor $i \leftarrow i + 1$.
4. **Exhaustion Gate:**
   - If loop terminates without a return, throw `InvalidOperationException` (defends against invalid contract assumptions).

#### 3.6 Concrete Step-by-Step State Trace
Input: `nums = [2, 11, 7, 15]`, `target = 9`.

| Step ($i$) | Current Value (`nums[i]`) | Required Complement (`9 - nums[i]`) | Lookup in `seen` | Decision & Action | Dictionary State `seen` After Step |
| :---: | :---: | :---: | :---: | :---: | :--- |
| `i = 0` | `2` | `7` | Miss (`seen` is `{}`) | Ingest `seen[2] = 0` | `{ 2: 0 }` |
| `i = 1` | `11` | `-2` | Miss | Ingest `seen[11] = 1` | `{ 2: 0, 11: 1 }` |
| `i = 2` | `7` | `2` | **Hit!** (`seen[2] = 0`) | **Terminate!** Return `[0, 2]` | `{ 2: 0, 11: 1 }` |

The algorithm terminates in 3 iterations instead of $\frac{4 \times 3}{2} = 6$ comparisons.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: One-Pass Hash Map (Optimal Time & Standard)**
  - *When to Use:* Default choice in interviews. The array is unsorted, random access is available, and an auxiliary memory budget of $O(N)$ is permitted.
  - *Streaming Suitability:* High. Can process items arriving one-by-one from a network stream without knowing total count in advance.
- **Approach 2: Sort + Two Pointers (Optimal Space & Memory Constrained)**
  - *When to Use:* Embedded systems, zero-heap-allocation environments, or when the input array is already sorted (as in LeetCode #167). If original indices are required and the array is unsorted, you must allocate an array of indexed tuples, which degrades auxiliary space back to $O(N)$.
  - *Cache Locality:* Contiguous array memory access during sorting and pointer convergence yields superior CPU L1/L2 cache hit rates compared to hash table bucket traversals.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Step 1: Setup & Boundaries:** Guard against null or arrays with fewer than 2 elements. Instantiate `Dictionary<int, int>` with pre-allocated capacity `nums.Length` to eliminate dynamic rehashing and table resizing penalties.
2. **Step 2: Main Exploration Loop:** Single linear traversal $i = 0 \dots N - 1$.
3. **Step 3: Invariant Maintenance & Condition Gates:** Compute complement via primitive 32-bit subtraction. Perform atomic probe via `TryGetValue`.
4. **Step 4: Resolution & Return:** Return matched index pair immediately upon first hit; never continue looping.

#### 4.3 Alternative Approaches Analysis
- **Two-Pass Hash Map:** Pass 1 populates the map `{ nums[i] -> i }`. Pass 2 queries `target - nums[i]`. Flaw: Requires two full traversals, handles duplicate values awkwardly (overwrites index unless a list is stored), and does not terminate early.
- **Sort + Two Pointers:** Sort indexed pairs $(nums[i], i)$ in $O(N \log N)$ time. Position left pointer at $0$ and right pointer at $N - 1$. If sum equals target, return original indices; if sum < target, increment left; if sum > target, decrement right.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. One-Pass Hash Map** | $O(1)$ | $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ (2 ints) | Moderate (hash buckets) | Non-mutating | Excellent (online) |
| **2. Sort + Two Pointers** | $O(N \log N)$ | $O(N \log N)$ | $O(N \log N)$ | $O(N)$ (index tracking) | $O(1)$ (2 ints) | High (linear scan) | Mutates order / copies | Poor (requires full array) |
| **3. Brute Force** | $O(1)$ | $O(N^2)$ | $O(N^2)$ | $O(1)$ | $O(1)$ (2 ints) | High | Non-mutating | Poor |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Two Sum
 * ============================================================================
 * Core Pattern      : One-Pass Hash Map (Complement Lookup)
 * Time Complexity   : O(N) amortized single pass across N elements
 * Space Complexity  : O(N) auxiliary space for dictionary storage
 * Selection Rule    : Default to Approach 1 for unsorted input under standard memory budgets.
 *                     Transition to Approach 2 if input is pre-sorted or O(1) space is mandated.
 * Defensive Traps   : Do not store element before probing (prevents self-match bug).
 *                     Pre-allocate dictionary capacity to eliminate bucket resizing churn.
 * ============================================================================
 */

public class Solution
{
    /// <summary>
    /// Finds two indices such that nums[index1] + nums[index2] == target.
    /// Employs a one-pass hash map to achieve optimal O(N) runtime.
    /// </summary>
    public int[] TwoSum(int[] nums, int target)
    {
        // Guard Clause: Contract requires at least 2 elements to form a pair
        if (nums == null || nums.Length < 2)
        {
            throw new ArgumentException("Array must contain at least two elements.", nameof(nums));
        }

        // Memory Optimization: Pre-allocate capacity to avoid internal table doubling & re-hashing
        var seen = new Dictionary<int, int>(capacity: nums.Length);

        // Exploration Loop: Process elements in a single forward pass
        for (int i = 0; i < nums.Length; i++)
        {
            int current = nums[i];
            
            // Arithmetic Invariant: target - current yields the exact needed pair partner
            int complement = target - current;

            // Decision Gate: Check if the required complement exists in historical ledger
            // Invariant: seen contains ONLY indices < i, completely eliminating self-matching risk
            if (seen.TryGetValue(complement, out int complementIndex))
            {
                // Terminal Exit: First match found satisfies the contract
                return new int[] { complementIndex, i };
            }

            // State Mutation: Register current value and index for downstream candidates.
            // Note: If duplicate values exist, overwriting is safe because if current duplicate was part
            // of the answer, it would have matched the first occurrence via complement check above.
            seen[current] = i;
        }

        // Defensive Exception: Contract assumes exactly one solution exists.
        throw new InvalidOperationException("No two sum solution found matching target.");
    }
}

/// <summary>
/// Alternative Approach: Sort + Opposing Two Pointers.
/// Used when memory constraints forbid O(N) dictionary allocations on the heap.
/// </summary>
public class SolutionSortApproach
{
    public int[] TwoSum(int[] nums, int target)
    {
        if (nums == null || nums.Length < 2)
        {
            throw new ArgumentException("Array must contain at least two elements.", nameof(nums));
        }

        // To return original indices, pair each value with its original index
        var indexedNums = new (int Val, int OrigIdx)[nums.Length];
        for (int i = 0; i < nums.Length; i++)
        {
            indexedNums[i] = (nums[i], i);
        }

        // Sort by value in O(N log N) time
        Array.Sort(indexedNums, (a, b) => a.Val.CompareTo(b.Val));

        // Opposing pointer cursors at array extremities
        int left = 0;
        int right = nums.Length - 1;

        while (left < right)
        {
            // Defensive Cast: Prevent potential integer overflow when adding extreme values
            long sum = (long)indexedNums[left].Val + indexedNums[right].Val;

            if (sum == target)
            {
                return new int[] { indexedNums[left].OrigIdx, indexedNums[right].OrigIdx };
            }
            else if (sum < target)
            {
                // Invariant: Sum is too small; incrementing left increases the sum monotonically
                left++;
            }
            else
            {
                // Invariant: Sum is too large; decrementing right decreases the sum monotonically
                right--;
            }
        }

        throw new InvalidOperationException("No two sum solution found matching target.");
    }
}
```

---


## 2. Contains Duplicate (LeetCode #217)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⚪ Reinforcement |
| **Pattern Tags** | `#hash-set` `#lookup` `#early-exit` `#sorting` `#pigeonhole` |
| **LeetCode Link** | [Contains Duplicate](https://leetcode.com/problems/contains-duplicate/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an integer array `nums`, return `true` if any value appears at least twice in the array, and return `false` if every element is distinct.
- **Assumptions & Contracts:**
  - Return boolean flag indicating presence of any collision.
  - As soon as the first duplicate is identified, immediate termination is permitted.
- **Key Constraints:**
  - `1 <= nums.Length <= 10^5`
  - `-10^9 <= nums[i] <= 10^9`
- **Senior Edge Cases to Defend:**
  - Singleton array (`nums.Length == 1`): Must return `false` in $O(1)$ without allocating a set.
  - Early collision: Collision occurs at index 0 and 1 (best case $O(1)$).
  - Late collision: Collision occurs at index 0 and $N - 1$.
  - Extreme values: Array contains `int.MinValue` and `int.MaxValue`.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Detect set membership collision in an input stream with early short-circuit evaluation.
- **Sample 1:**
  - **Input:** `nums = [1, 2, 3, 1]`
  - **Output:** `true`
  - **Explanation:** Value `1` occurs at indices 0 and 3.
- **Sample 2:**
  - **Input:** `nums = [1, 2, 3, 4]`
  - **Output:** `false`
  - **Explanation:** All elements are mutually distinct.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a nightclub bouncer equipped with an instant fingerprint scanner. As guests arrive in line, each places their thumb on the glass. The scanner checks the internal memory bank. If the scanner chirps *"Already checked in!"*, the bouncer immediately sounds the alarm and stops the line (`return true`). The bouncer never needs to inspect the remaining 10,000 guests waiting outside. Only if every single guest has entered without a single duplicate chirp does the bouncer confirm the venue is distinct (`return false`).

#### 3.2 The Naive Bottleneck & Redundant Computation
The brute-force approach compares each element with every other element via nested loops:
- **Total Comparisons:** $\frac{N(N - 1)}{2} = O(N^2)$.
- **Redundant Scans:** At index $i$, scanning $[i + 1 \dots N - 1]$ repeatedly re-evaluates elements that were already proven distinct from earlier indices. It wastes CPU time re-reading memory without capturing the global distinctness invariant.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Pigeonhole & Set Uniqueness:** A `HashSet<T>` guarantees $O(1)$ amortized membership verification via hash-bucket mapping.
- **Single-Op Mutation & Query:** The .NET runtime method `HashSet<T>.Add(item)` combines lookup and insertion into a single operation:
  - If `item` already exists: returns `false` (collision detected).
  - If `item` is new: inserts and returns `true`.
- **Early Exit Invariant:** The moment `Add(item)` evaluates to `false`, a duplicate is mathematically proven. Discarding the remaining $N - i - 1$ elements is strictly optimal.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
At index $i$ scanning element `nums[i]`:
- `seenSet`: Hash set containing unique elements from $nums[0 \dots i - 1]$.

```text
[ 0 ......... i - 1 ]            [   i   ]           [ i + 1 ......... N - 1 ]
└─────────┬─────────┘            └───┬───┘           └───────────┬───────────┘
   Verified Unique Set          Candidate Ingestion        Uninspected Elements
   (Cardinality == i)
```

- **Invariant:**
  1. `seenSet.Count == i`.
  2. All elements in `seenSet` are mutually distinct.
  3. If $nums[i] \in seenSet$, collision confirmed.

#### 3.5 State Transition Triggers & Decision Gates
1. **Decision Gate 1 (Collision Check):**
   - Execute: `!seenSet.Add(nums[i])`.
   - If evaluated to `true`: Return `true` immediately (short-circuit).
2. **Decision Gate 2 (Advance):**
   - If added successfully: advance to next element.
3. **Loop Exhaustion:**
   - If stream ends: Return `false`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `nums = [1, 2, 3, 1]`

| Step ($i$) | Ingestion Value | `seenSet.Add(val)` Return | Decision & Action | Set State `seenSet` |
| :---: | :---: | :---: | :---: | :--- |
| `i = 0` | `1` | `true` (New item) | Invariant holds; advance | `{ 1 }` |
| `i = 1` | `2` | `true` (New item) | Invariant holds; advance | `{ 1, 2 }` |
| `i = 2` | `3` | `true` (New item) | Invariant holds; advance | `{ 1, 2, 3 }` |
| `i = 3` | `1` | `false` (**Collision!**) | **Short-circuit!** Return `true` | `{ 1, 2, 3 }` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: HashSet (Optimal Time)**
  - *When to Use:* Default production choice. Non-destructive to input, $O(N)$ linear time, with $O(1)$ best-case early termination.
  - *Trade-off:* Consumes $O(N)$ heap memory.
- **Approach 2: In-Place Sort (Optimal Auxiliary Space)**
  - *When to Use:* Memory-constrained environments where allocating an auxiliary hash table of $10^5$ integers is unacceptable.
  - *Trade-off:* Requires $O(N \log N)$ time and mutates the input array ordering (side effect).

#### 4.2 Step-by-Step Natural Progression Flow
1. **Step 1: Guard:** If `nums.Length <= 1`, return `false` immediately.
2. **Step 2: Allocation:** Instantiate `HashSet<int>` pre-allocated to `nums.Length`.
3. **Step 3: Exploration & Collision Detection:** Iterate through each number. Ingest via `!seen.Add(num)`. Return `true` on failure.
4. **Step 4: Exhaustion:** Return `false` once entire array is processed.

#### 4.3 Alternative Approaches Analysis
- **BitSet / Direct Addressing:** If numbers were restricted to a small bounded range $[0 \dots M]$, a `BitArray` or `bool[]` would yield $O(1)$ space and ultra-fast cache locality. However, since values span $[-10^9, 10^9]$, hash sets are mandatory.
- **In-Place Sorting:** Sort array using Introsort (`Array.Sort`), then scan adjacent pairs `nums[i] == nums[i - 1]`.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. HashSet** | $O(1)$ | $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ | Moderate | Non-mutating | Excellent (online) |
| **2. In-Place Sort** | $O(N \log N)$ | $O(N \log N)$ | $O(N \log N)$ | $O(1)$ (or $O(\log N)$ stack) | $O(1)$ | High | Mutates input array | Poor |
| **3. Brute Force** | $O(1)$ | $O(N^2)$ | $O(N^2)$ | $O(1)$ | $O(1)$ | High | Non-mutating | Poor |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Contains Duplicate
 * ============================================================================
 * Core Pattern      : HashSet Membership Collision with Short-Circuit
 * Time Complexity   : O(N) average, O(1) best-case early exit
 * Space Complexity  : O(N) worst-case auxiliary space
 * Selection Rule    : Default to Approach 1 for non-destructive linear time.
 *                     Use Approach 2 if memory budget is strictly O(1) and mutating input is allowed.
 * Defensive Traps   : Use HashSet.Add() directly—do not call Contains() followed by Add() (halves bucket lookups).
 * ============================================================================
 */

public class Solution
{
    /// <summary>
    /// Evaluates if any integer appears at least twice in the input array.
    /// Utilizes a HashSet for O(1) amortized insertion and membership verification.
    /// </summary>
    public bool ContainsDuplicate(int[] nums)
    {
        // Guard Clause: Single elements or null can never contain duplicates
        if (nums == null || nums.Length <= 1)
        {
            return false;
        }

        // Optimization: Pre-size HashSet capacity to avoid rehashing churn during insertion
        var seen = new HashSet<int>(capacity: nums.Length);

        // Streaming Scan: Inspect elements sequentially
        foreach (int num in nums)
        {
            // HashSet.Add returns false if the item was ALREADY present in the set.
            // This combines lookup and insertion into a single hash-probe cycle.
            if (!seen.Add(num))
            {
                // Terminal Short-Circuit: Collision detected, exit immediately
                return true;
            }
        }

        // Invariant: Entire array traversed with 100% distinct items
        return false;
    }
}

/// <summary>
/// Alternative Approach: In-Place Sort with Adjacent Comparison.
/// Minimizes memory footprint to O(1) auxiliary space by mutating the input array.
/// </summary>
public class SolutionInPlaceSort
{
    public bool ContainsDuplicate(int[] nums)
    {
        if (nums == null || nums.Length <= 1)
        {
            return false;
        }

        // Sort in O(N log N) using .NET Introsort (dual-pivot quicksort / heapsort hybrid)
        Array.Sort(nums);

        // Invariant: In a sorted array, any identical elements must be strictly adjacent
        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] == nums[i - 1])
            {
                return true; // Adjacent duplicate found
            }
        }

        return false;
    }
}
```

---


## 3. Valid Anagram (LeetCode #242)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⚪ Reinforcement |
| **Pattern Tags** | `#frequency-map` `#counting` `#string` `#fixed-alphabet` `#stackalloc` |
| **LeetCode Link** | [Valid Anagram](https://leetcode.com/problems/valid-anagram/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given two strings `s` and `t`, return `true` if `t` is an anagram of `s`, and `false` otherwise.
- **Assumptions & Contracts:**
  - An anagram is a word formed by rearranging the letters of another, using all the original letters exactly once.
- **Key Constraints:**
  - `1 <= s.Length, t.Length <= 5 * 10^4`
  - `s` and `t` consist of lowercase English letters (`'a'` to `'z'`).
- **Senior Follow-Up Challenge:** What if inputs contain arbitrary Unicode characters? (Handle via UTF-32 codepoints in `Dictionary<int, int>`).
- **Senior Edge Cases to Defend:**
  - Length mismatch: `s.Length != t.Length` (must short-circuit in $O(1)$).
  - Single character strings: Identical vs distinct.
  - Skewed frequencies: Same distinct characters but unequal counts (e.g., `"aa"` vs `"a"`).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Verify that two strings share an identical multiset of character frequencies without allocation overhead.
- **Sample 1:**
  - **Input:** `s = "anagram"`, `t = "nagaram"`
  - **Output:** `true`
- **Sample 2:**
  - **Input:** `s = "rat"`, `t = "car"`
  - **Output:** `false`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a two-pan laboratory balance scale equipped with 26 small containers, labeled `'a'` through `'z'`. String `s` acts as an importer: for every letter in `s`, you drop a 1g copper weight into that letter's container. String `t` acts as an exporter: for every letter in `t`, you remove a 1g copper weight from that container. At the end of the process, you inspect all 26 containers. If and only if every single container rests at exactly zero grams, the two strings are perfect anagrams. If any container has a surplus or deficit, they cannot be anagrams.

#### 3.2 The Naive Bottleneck & Redundant Computation
- **Naive Approach 1 (Character Searching / Removal):** For each character in `s`, find and delete it in `t`. String deletion is $O(N)$ due to shifting memory, leading to an overall $O(N^2)$ algorithm.
- **Naive Approach 2 (String Sorting):** Convert both strings to character arrays and sort them:
  ```csharp
  char[] a = s.ToCharArray(); char[] b = t.ToCharArray();
  Array.Sort(a); Array.Sort(b);
  return new string(a) == new string(b);
  ```
  - *Flaws:* $O(N \log N)$ time, allocates 4 separate heap objects (two `char[]` and two `string`), triggering garbage collector overhead for large strings.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Alphabet Cardinality Invariant:** Because characters are strictly lowercase English letters, the universe of possible symbols is fixed at $|\Sigma| = 26$.
- **Zero-Allocation Frequency Vector:** Instead of dynamic hashing or heap arrays, allocate an array of 26 integers on the CPU stack frame via `Span<int> charCounts = stackalloc int[26]`.
- **Zero-Sum Balance Invariant:**
  $$\sum_{i=0}^{N-1} (\mathbb{I}[s[i] = c] - \mathbb{I}[t[i] = c]) = 0 \quad \forall c \in ['a' \dots 'z']$$
  Processing $s$ (incrementing) and $t$ (decrementing) synchronously guarantees that any non-zero value at the end signals a frequency mismatch.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
Single synchronized cursor $i$ moving $0 \dots N - 1$:
- `s[i]`: Adds $+1$ to `charCounts[s[i] - 'a']`.
- `t[i]`: Subtracts $-1$ from `charCounts[t[i] - 'a']`.

```text
s: [ 0 ......... i - 1 ]        [   i   ]        [ i + 1 ......... N - 1 ]
   └─────────┬─────────┘        └───┬───┘        └───────────┬───────────┘
      Processed (+1)              Current                  Pending
t: [ 0 ......... i - 1 ]        [   i   ]        [ i + 1 ......... N - 1 ]
   └─────────┬─────────┘        └───┬───┘        └───────────┬───────────┘
      Processed (-1)              Current                  Pending

State: charCounts[0..25] stores net balance delta across processed prefixes
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Gate 1 (Length Mismatch):** If `s.Length != t.Length`, return `false` immediately in $O(1)$.
2. **Gate 2 (Synchronous Delta Accumulation):**
   - For $i = 0 \dots N - 1$:
     `charCounts[s[i] - 'a']++`
     `charCounts[t[i] - 'a']--`
3. **Gate 3 (Zero-Sum Verification):**
   - Loop $c = 0 \dots 25$:
     If `charCounts[c] != 0`, return `false`.
4. **Terminal Confirmation:** If all 26 buckets are zero, return `true`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `s = "anagram"`, `t = "nagaram"` ($N = 7$).
Relevant buckets: `'a'` (idx 0), `'g'` (idx 6), `'m'` (idx 12), `'n'` (idx 13), `'r'` (idx 17).

| Step ($i$) | `s[i]` (Inc) | `t[i]` (Dec) | Net Deltas for Active Characters (`a, g, m, n, r`) |
| :---: | :---: | :---: | :--- |
| Initial | - | - | `a: 0, g: 0, m: 0, n: 0, r: 0` |
| `i = 0` | `'a'` (+1) | `'n'` (-1) | `a: +1, n: -1` |
| `i = 1` | `'n'` (+1) | `'a'` (-1) | `a: 0, n: 0` |
| `i = 2` | `'a'` (+1) | `'g'` (-1) | `a: +1, g: -1` |
| `i = 3` | `'g'` (+1) | `'a'` (-1) | `a: 0, g: 0` |
| `i = 4` | `'r'` (+1) | `'r'` (-1) | `r: 0` |
| `i = 5` | `'a'` (+1) | `'a'` (-1) | `a: 0` |
| `i = 6` | `'m'` (+1) | `'m'` (-1) | `m: 0` |
| Final Check | All 26 buckets evaluated | All buckets == 0 | **Return `true`** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Fixed Frequency Array / Stackalloc (Optimal)**
  - *When to Use:* Always for bounded ASCII or lowercase English strings. $O(N)$ time, $O(1)$ auxiliary space, zero heap allocation.
- **Approach 2: Generic Codepoint Dictionary (Unicode Senior Follow-Up)**
  - *When to Use:* In an interview when asked: *"What if the input contains emojis, Chinese characters, or arbitrary Unicode?"* The alphabet size is up to $1,114,112$ codepoints. Allocating a fixed array is impractical; use `Dictionary<int, int>` with `char.ConvertToUtf32`.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Step 1: Length Guard:** Immediate length verification.
2. **Step 2: Stack Allocation:** `Span<int> charCounts = stackalloc int[26];` (allocates 104 bytes on the thread stack, zero GC interaction).
3. **Step 3: Traversal:** Single loop traversing length $N$, computing offsets `c - 'a'`.
4. **Step 4: Audit:** Fixed 26-iteration scan to guarantee zero delta.

#### 4.3 Alternative Approaches Analysis
- **Sorting:** $O(N \log N)$ time and $O(N)$ space. Easy to write in one line of LINQ (`s.OrderBy(c => c).SequenceEqual(t.OrderBy(c => c))`), but abysmal for senior engineering standards due to allocations and sorting overhead.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Stackalloc Array** | $O(1)$ (length mismatch) | $O(N)$ | $O(N)$ | $O(1)$ (104 bytes stack) | $O(1)$ | Near-perfect (L1 cache) | Non-mutating | Moderate |
| **2. Unicode Map** | $O(1)$ | $O(N)$ | $O(N)$ | $O(U)$ ($U \le N$ distinct) | $O(1)$ | Moderate (heap map) | Non-mutating | High |
| **3. Array.Sort** | $O(1)$ | $O(N \log N)$ | $O(N \log N)$ | $O(N)$ heap chars | $O(1)$ | High (sequential) | Non-mutating | Poor |

---

### 5. Production C# Implementation

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Valid Anagram
 * ============================================================================
 * Core Pattern      : Fixed Frequency Array (Zero-Sum Balance Delta)
 * Time Complexity   : O(N) single pass across length N
 * Space Complexity  : O(1) auxiliary space (stackalloc Span<int>[26])
 * Selection Rule    : Use fixed stackalloc array for ASCII/lowercase alphabets.
 *                     Transition to Dictionary<int, int> for arbitrary Unicode/UTF-32 inputs.
 * Defensive Traps   : Verify string lengths first (instant O(1) short circuit).
 *                     Ensure alphabet indexing uses lowercase offset: s[i] - 'a'.
 * ============================================================================
 */

public class Solution
{
    /// <summary>
    /// Determines whether string t is an anagram of string s.
    /// Employs stackalloc Span to achieve zero heap allocation and zero GC overhead.
    /// </summary>
    public bool IsAnagram(string s, string t)
    {
        // Guard Clause: Mismatched lengths can never be permutations of one another
        if (s == null || t == null || s.Length != t.Length)
        {
            return false;
        }

        // Performance Optimization: Allocate 26 integers (104 bytes) on thread execution stack.
        // Bypasses heap allocation and produces zero Garbage Collection pressure.
        Span<int> charCounts = stackalloc int[26];

        // Synchronous Traversal: Accumulate positive deltas for 's' and negative deltas for 't'
        for (int i = 0; i < s.Length; i++)
        {
            charCounts[s[i] - 'a']++;
            charCounts[t[i] - 'a']--;
        }

        // Zero-Sum Invariant Audit: If strings are anagrams, every single character bucket
        // must have arrived back at exactly zero net balance.
        for (int i = 0; i < 26; i++)
        {
            if (charCounts[i] != 0)
            {
                return false; // Surplus or deficit found
            }
        }

        return true;
    }
}

/// <summary>
/// Senior Interview Follow-Up Implementation: Supports arbitrary Unicode characters.
/// Parses UTF-32 surrogate pairs into distinct 32-bit codepoints.
/// </summary>
public class SolutionUnicodeAnagram
{
    public bool IsAnagram(string s, string t)
    {
        if (s == null || t == null || s.Length != t.Length)
        {
            return false;
        }

        var frequencyMap = new Dictionary<int, int>();

        // Ingest string s into codepoint frequencies
        for (int i = 0; i < s.Length; i += char.IsSurrogatePair(s, i) ? 2 : 1)
        {
            int codePoint = char.ConvertToUtf32(s, i);
            frequencyMap[codePoint] = frequencyMap.GetValueOrDefault(codePoint, 0) + 1;
        }

        // Decrement codepoint frequencies with string t
        for (int i = 0; i < t.Length; i += char.IsSurrogatePair(t, i) ? 2 : 1)
        {
            int codePoint = char.ConvertToUtf32(t, i);
            if (!frequencyMap.TryGetValue(codePoint, out int count) || count == 0)
            {
                return false; // Character not present or over-subscribed
            }
            frequencyMap[codePoint] = count - 1;
        }

        return true;
    }
}
```

---


## 4. Group Anagrams (LeetCode #49)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#hash-map` `#canonical-representation` `#string` `#equivalence-class` |
| **LeetCode Link** | [Group Anagrams](https://leetcode.com/problems/group-anagrams/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array of strings `strs`, group the anagrams together. You can return the answer in any order.
- **Assumptions & Contracts:**
  - An anagram group is an equivalence class under permutation.
  - Every string in `strs` belongs to exactly one group.
- **Key Constraints:**
  - `1 <= strs.Length <= 10^4`
  - `0 <= strs[i].Length <= 100`
  - `strs[i]` consists of lowercase English letters.
- **Senior Edge Cases to Defend:**
  - Empty strings: `strs = ["", ""]` $\implies$ `[["", ""]]`.
  - Singletons: No strings share an anagram partner.
  - Large homogeneous arrays: Hundreds of words that are all anagrams of each other.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Partition an array of strings into equivalence classes by projecting each word onto an invariant canonical signature key.
- **Sample 1:**
  - **Input:** `strs = ["eat","tea","tan","ate","nat","bat"]`
  - **Output:** `[["bat"],["nat","tan"],["ate","eat","tea"]]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a postal sorting facility handling millions of scrambled letters. Rather than comparing every letter to every other letter (which would take eternity), the postmaster establishes a standardized stamping rule: before putting a letter into a pigeonhole, you sort its letters alphabetically (or count its characters) to forge a unique *canonical barcode*.
- `"eat"` $\implies$ barcode `"aet"`
- `"tea"` $\implies$ barcode `"aet"`
- `"ate"` $\implies$ barcode `"aet"`
No matter how scrambled the envelope appears, its barcode is identical. The worker walks to slot `"aet"` and drops the envelope inside. Grouping becomes a simple dictionary mapping: `{ CanonicalBarcode -> List<Word> }`.

#### 3.2 The Naive Bottleneck & Redundant Computation
- **Pairwise Comparison ($O(N^2 \cdot K)$):** For $N$ words of length $K$, testing all pairs $(w_i, w_j)$ using `IsAnagram` requires $\frac{N(N - 1)}{2} \times O(K)$ operations. With $N = 10^4$, this equates to $\approx 5 \times 10^7 \times K$ operations, causing massive Time Limit Exceeded (TLE) errors.
- Redundant re-evaluation occurs because the algorithm treats anagram verification as an isolated binary test rather than recognizing it as an *equivalence relation* with a shared representative invariant.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Canonical Projection Function $f(w)$:**
  $$\text{Anagram}(w_1, w_2) \iff f(w_1) = f(w_2)$$
- Any word $w$ can be projected into its canonical representative via two primary methods:
  1. **Sorted Character Sequence:** $f(w) = \text{Sort}(w)$. Time: $O(K \log K)$.
  2. **Character Count Signature:** $f(w) = \text{Counts}(w)$ (e.g., `#1#0#0...`). Time: $O(K)$.
- **Hash Table Invariant:** By using $f(w)$ as the key in a `Dictionary<string, IList<string>>`, all elements belonging to the same equivalence class naturally accumulate in the exact same list in a single forward pass.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
At word index $i$ in `strs`:
- `strs[i]`: Word being processed.
- `groups`: Dictionary mapping `CanonicalKey -> List of original words`.

```text
[ strs[0] ... strs[i - 1] ]            [ strs[i] ]           [ strs[i + 1] ... strs[N - 1] ]
└────────────┬────────────┘            └────┬────┘           └──────────────┬──────────────┘
   Partitioned into Groups           Current Word                   Unprocessed
   in 'groups' dictionary            Projection: f(w)

Hash Table State:
"aet" ───► [ "eat", "tea", "ate" ]
"ant" ───► [ "tan", "nat" ]
"abt" ───► [ "bat" ]
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Canonical Key Generation:** For word $w = strs[i]$, compute `key = GenerateKey(w)`.
2. **Decision Gate 1 (Bucket Existence):**
   - Check `groups.TryGetValue(key, out var list)`.
   - If missing: allocate `list = new List<string>()`, set `groups[key] = list`.
3. **Decision Gate 2 (Ingestion):**
   - Append `list.Add(w)`.
4. **Resolution:** Return `groups.Values.ToList()`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `strs = ["eat", "tea", "tan", "ate", "nat", "bat"]`

| Word ($w$) | Canonical Key ($f(w)$) | Dictionary Match | Action | Resulting Group State |
| :---: | :---: | :---: | :---: | :--- |
| `"eat"` | `"aet"` | Miss | Create bucket `"aet"` | `"aet" -> ["eat"]` |
| `"tea"` | `"aet"` | Hit | Append to `"aet"` | `"aet" -> ["eat", "tea"]` |
| `"tan"` | `"ant"` | Miss | Create bucket `"ant"` | `"ant" -> ["tan"]` |
| `"ate"` | `"aet"` | Hit | Append to `"aet"` | `"aet" -> ["eat", "tea", "ate"]` |
| `"nat"` | `"ant"` | Hit | Append to `"ant"` | `"ant" -> ["tan", "nat"]` |
| `"bat"` | `"abt"` | Miss | Create bucket `"abt"` | `"abt" -> ["bat"]` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Sorted String Canonical Key**
  - *When to Use:* Ideal when max word length $K$ is modest ($K \le 100$, as constrained by LeetCode). Sorting a 100-character array in CPU L1 cache using `Array.Sort` is blazingly fast in .NET due to SIMD and optimized primitives.
  - *Trade-off:* $O(N \cdot K \log K)$ time.
- **Approach 2: 26-Count Frequency Signature Key**
  - *When to Use:* Mandatory if words are very long ($K \ge 10^4$). $O(K)$ counting avoids the $\log K$ sorting penalty.
  - *Trade-off:* String formatting overhead (e.g., building `"#1#0#2..."`) can create extra garbage collections unless using value tuples or custom struct keys.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Step 1: Guard:** If `strs` is null or empty, return empty list.
2. **Step 2: Dictionary Setup:** Initialize `Dictionary<string, IList<string>>`.
3. **Step 3: Traversal & Key Projection:** For each word, convert to char array, sort in-place, and instantiate key string.
4. **Step 4: Accumulation & Return:** Look up or add list, insert word. Return values collection.

#### 4.3 Alternative Approaches Analysis
- **Prime Number Product Hashing:** Assign each character `'a'..'z'` a prime number ($2, 3, 5, 7, \dots$). The canonical key is the product of primes (fundamental theorem of arithmetic guarantees unique prime factorization). Problem: 64-bit integer overflow happens rapidly for words longer than 15 characters, making it fragile in production.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Sorted Key** | $O(N \cdot K \log K)$ | $O(N \cdot K \log K)$ | $O(N \cdot K \log K)$ | $O(N \cdot K)$ | $O(N \cdot K)$ | High | Non-mutating | Excellent |
| **2. Frequency Signature** | $O(N \cdot K)$ | $O(N \cdot K)$ | $O(N \cdot K)$ | $O(N \cdot K)$ | $O(N \cdot K)$ | Moderate (string build) | Non-mutating | Excellent |
| **3. Pairwise IsAnagram** | $O(N \cdot K)$ | $O(N^2 \cdot K)$ | $O(N^2 \cdot K)$ | $O(1)$ | $O(N \cdot K)$ | Low | Non-mutating | Poor |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Group Anagrams
 * ============================================================================
 * Core Pattern      : Equivalence Class Grouping via Canonical Key Projection
 * Time Complexity   : O(N * K log K) for Approach 1; O(N * K) for Approach 2
 * Space Complexity  : O(N * K) auxiliary space for dictionary storage
 * Selection Rule    : Use Approach 1 (Sorted String Key) when K <= 100 for maximum practical throughput.
 *                     Use Approach 2 (Frequency Count) when word length K is arbitrarily large.
 * Defensive Traps   : Use string keys directly in dictionary; avoid prime-factor multiplication due to overflow.
 * ============================================================================
 */

public class Solution
{
    /// <summary>
    /// Groups anagrams together by projecting each word into an alphabetically sorted canonical string.
    /// Optimal for K <= 100 where Array.Sort operates entirely within CPU cache.
    /// </summary>
    public IList<IList<string>> GroupAnagrams(string[] strs)
    {
        // Guard Clause: Handle null or empty collection
        if (strs == null || strs.Length == 0)
        {
            return Array.Empty<IList<string>>();
        }

        // Dictionary mapping canonical signature -> list of matching anagram words
        var groups = new Dictionary<string, IList<string>>();

        foreach (string word in strs)
        {
            // Defensive Copy: ToCharArray allocates mutable buffer for in-place sorting
            char[] chars = word.ToCharArray();
            
            // Invariant: Sorting characters canonicalizes any anagram into identical character sequence
            Array.Sort(chars);
            string canonicalKey = new string(chars);

            // Decision Gate: Check if canonical equivalence class already exists
            if (!groups.TryGetValue(canonicalKey, out var list))
            {
                list = new List<string>();
                groups[canonicalKey] = list;
            }

            // Ingest current word into its proven equivalence class
            list.Add(word);
        }

        // Return materialized equivalence partitions
        return groups.Values.ToList();
    }
}

/// <summary>
/// Approach 2: 26-Count Frequency Key (Strictly Linear O(N * K)).
/// Avoids the O(K log K) sorting step by using character frequency signatures.
/// </summary>
public class SolutionFrequencyKey
{
    public IList<IList<string>> GroupAnagrams(string[] strs)
    {
        if (strs == null || strs.Length == 0)
        {
            return Array.Empty<IList<string>>();
        }

        var groups = new Dictionary<string, IList<string>>();

        foreach (string word in strs)
        {
            // Allocate 26-character frequency buffer on stack (zero GC)
            Span<int> counts = stackalloc int[26];
            foreach (char c in word)
            {
                counts[c - 'a']++;
            }

            // Construct delimiter-separated canonical signature: "#1#0#2#0..."
            var sb = new System.Text.StringBuilder(capacity: 64);
            for (int i = 0; i < 26; i++)
            {
                sb.Append('#');
                sb.Append(counts[i]);
            }
            string key = sb.ToString();

            if (!groups.TryGetValue(key, out var list))
            {
                list = new List<string>();
                groups[key] = list;
            }

            list.Add(word);
        }

        return groups.Values.ToList();
    }
}
```

---


## 5. Longest Consecutive Sequence (LeetCode #128)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#hash-set` `#sequence-building` `#array` `#O(n)` `#union-find` `#graph-components` |
| **LeetCode Link** | [Longest Consecutive Sequence](https://leetcode.com/problems/longest-consecutive-sequence/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an unsorted array of integers `nums`, return the length of the longest consecutive elements sequence.
- **Strict Requirement:** You must write an algorithm that runs in $O(N)$ time complexity.
- **Key Constraints:**
  - `0 <= nums.Length <= 10^5`
  - `-10^9 <= nums[i] <= 10^9`
- **Senior Edge Cases to Defend:**
  - Empty array: `nums = []` $\implies$ `0`.
  - Single element: `nums = [7]` $\implies$ `1`.
  - Severe duplicates: `nums = [0, 1, 1, 2]` $\implies$ `3`. Deduplication must occur upfront.
  - Negative values spanning zero: `nums = [-2, -1, 0, 1, 2]` $\implies$ `5`.
  - All distinct disjoint elements: `nums = [10, 30, 50]` $\implies$ `1`.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Find the diameter of the largest connected component in a continuous integer graph in strictly linear time.
- **Sample 1:**
  - **Input:** `nums = [100, 4, 200, 1, 3, 2]`
  - **Output:** `4`
  - **Explanation:** The longest consecutive sequence is `[1, 2, 3, 4]`. Length = 4.
- **Sample 2:**
  - **Input:** `nums = [0, 3, 7, 2, 5, 8, 4, 6, 0, 1]`
  - **Output:** `9`
  - **Explanation:** Sequence is `[0, 1, 2, 3, 4, 5, 6, 7, 8]`. Length = 9.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a massive train yard where hundreds of numbered railcars are randomly uncoupled and strewn about across multiple tracks. If you want to find the longest train, you don't inspect a car and try to build a train in both directions, and you certainly don't start building from middle cars—because doing so causes you to re-walk the same cars over and over.
Instead, you walk through the yard looking *only for locomotive engines*! What defines an engine? A car numbered $x$ is an engine if and only if car $x - 1$ **does not exist anywhere in the yard**.
Once you spot an engine, you couple car $x + 1, x + 2, x + 3, \dots$ until no more coupled cars exist. If a car has a preceding car $x - 1$, you ignore it completely: it is an internal car, and its engine will take care of it later.

#### 3.2 The Naive Bottleneck & Redundant Computation
- **Sorting ($O(N \log N)$):** Sorting the array takes $O(N \log N)$ time, which violates the strict $O(N)$ interview constraint.
- **Unconstrained Expansion ($O(N^2)$):** Ingesting all numbers into a `HashSet` and expanding upward from *every* element $x$:
  ```csharp
  foreach (int x in numSet) {
      int curr = x;
      while (numSet.Contains(curr + 1)) curr++;
  }
  ```
  - *Catastrophic Redundancy:* Consider `nums = [1, 2, 3, 4, 5]`.
    - For `1`: probes `2, 3, 4, 5` (5 checks).
    - For `2`: probes `3, 4, 5` (4 checks).
    - For `3`: probes `4, 5` (3 checks).
    - Total operations: $\frac{N(N + 1)}{2} = O(N^2)$ checks!

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **The Sequence Head Invariant:**
  $$\text{IsHead}(x) \iff (x - 1) \notin \text{HashSet}$$
- If $x - 1 \in \text{HashSet}$, then $x$ is strictly an internal node or tail of an existing chain. Expanding from $x$ is guaranteed to produce a streak *shorter* than the streak initiated by its true head. Therefore, skipping all internal nodes in $O(1)$ is mathematically safe.
- **Formal Proof of Amortized $O(N)$ Runtime:**
  - Let there be $K$ disjoint consecutive sequences $S_1, S_2, \dots, S_K$ with lengths $L_1, L_2, \dots, L_K$, where $\sum_{j=1}^K L_j \le N$.
  - For non-head elements: The `if (!set.Contains(x - 1))` check fails immediately $\implies 1$ hash probe per non-head element.
  - For head elements: The `while` loop executes exactly $L_j$ times for component $S_j$.
  - Total set lookups across the entire algorithm:
    $$\text{Total Checks} = N + \sum_{j=1}^K L_j \le N + N = 2N$$
  - Thus, the algorithm is bounded by strictly $2N$ lookups, guaranteeing $O(N)$ linear time!

#### 3.4 Cursor Semantics & Invariant Partition Architecture
- `numSet`: Hash set containing unique elements of `nums`.
- `num`: Outer cursor traversing unique elements of `numSet`.
- `currentNum`: Inner frontier cursor expanding forward from verified head.

```text
Decision Gate for Element x in numSet:
  x - 1 in set?
  ├── YES ──► Internal Node! SKIP immediately (0 work, O(1))
  └── NO  ──► Sequence Head! Initiate forward expansion:
              [ x ] ──► [ x + 1 ] ──► [ x + 2 ] ──► ... ──► [ x + L - 1 ]
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Deduplication:** Populate `HashSet<int> numSet = new HashSet<int>(nums)`.
2. **Decision Gate 1 (Head Verification):**
   - Check `numSet.Contains(num - 1)`.
   - If true: `continue` (skip internal node).
3. **Decision Gate 2 (Chain Traversal):**
   - If false (`num` is head): initialize `currentNum = num`, `currentStreak = 1`.
   - While `numSet.Contains(currentNum + 1)`:
     `currentNum++`
     `currentStreak++`
4. **State Mutation:** `longestStreak = Math.Max(longestStreak, currentStreak)`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `nums = [100, 4, 200, 1, 3, 2]` $\implies$ `numSet = { 100, 4, 200, 1, 3, 2 }`

| Element `num` | `Contains(num - 1)` | Head? | Expansion Steps (`currentNum + 1`) | Streak Length | Max Streak |
| :---: | :---: | :---: | :---: | :---: | :---: |
| `100` | `99 in set? NO` | **YES** | `101 in set? NO` | 1 | $\max(0, 1) = 1$ |
| `4` | `3 in set? YES` | **NO** | Skipped in $O(1)$ | - | 1 |
| `200` | `199 in set? NO` | **YES** | `201 in set? NO` | 1 | $\max(1, 1) = 1$ |
| `1` | `0 in set? NO` | **YES** | Probes `2, 3, 4` (Hit), `5` (Miss) | 4 | $\max(1, 4) = 4$ |
| `3` | `2 in set? YES` | **NO** | Skipped in $O(1)$ | - | 4 |
| `2` | `1 in set? YES` | **NO** | Skipped in $O(1)$ | - | 4 |

Result: 4. Notice that elements 4, 3, and 2 were skipped instantly.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: HashSet Sequence Heads (Optimal)**
  - *When to Use:* Always for batch processing of arrays where $O(N)$ time is required. Simplest to write, lowest constant factor, robust under all inputs.
- **Approach 2: Disjoint Set Union / Union-Find (DSU)**
  - *When to Use:* When numbers arrive dynamically as a continuous live stream, and the longest consecutive sequence must be reported after every insertion without reprocessing the entire dataset.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Step 1: Setup:** Handle null/empty edge case. Construct `HashSet<int>`.
2. **Step 2: Traversal:** Iterate through `numSet` elements.
3. **Step 3: Head Filter:** If `set.Contains(num - 1)`, immediately bypass.
4. **Step 4: Extension & Resolution:** While `set.Contains(curr + 1)`, increment streak. Update max streak.

#### 4.3 Alternative Approaches Analysis
- **Union-Find (DSU):** Maintain `parent` and `size` dictionaries. For each number $x$, union with $x - 1$ and $x + 1$ if they exist. Time: $O(N \cdot \alpha(N))$, where $\alpha$ is the inverse Ackermann function. Auxiliary space is $2N$ dictionary entries.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. HashSet Heads** | $O(N)$ | $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ | Moderate (hash table) | Non-mutating | Poor (requires batch) |
| **2. Union-Find** | $O(N)$ | $O(N \alpha(N))$ | $O(N \alpha(N))$ | $O(N)$ | $O(1)$ | Moderate | Non-mutating | **Excellent** (online) |
| **3. Sorting** | $O(N \log N)$ | $O(N \log N)$ | $O(N \log N)$ | $O(1)$ (or $O(N)$) | $O(1)$ | High | Mutates input | Poor |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Longest Consecutive Sequence
 * ============================================================================
 * Core Pattern      : HashSet Sequence Head Pruning (Disjoint Component Expansion)
 * Time Complexity   : O(N) strictly amortized across all element expansions
 * Space Complexity  : O(N) auxiliary space for HashSet deduplication and lookups
 * Selection Rule    : Default to Approach 1 for batch input.
 *                     Use Approach 2 (Union-Find) for online streaming where elements arrive continuously.
 * Defensive Traps   : Filter out non-heads: if set.Contains(num - 1), skip immediately.
 *                     Iterate over the HashSet, NOT the original array (avoids duplicate processing).
 * ============================================================================
 */

public class Solution
{
    /// <summary>
    /// Computes the length of the longest consecutive sequence in O(N) time.
    /// Employs sequence head filtering to guarantee each element is visited at most twice.
    /// </summary>
    public int LongestConsecutive(int[] nums)
    {
        // Guard Clause: Empty array yields sequence length of 0
        if (nums == null || nums.Length == 0)
        {
            return 0;
        }

        // Deduplication & Fast Lookups: Hash-index all numbers in O(N) time
        var numSet = new HashSet<int>(nums);
        int longestStreak = 0;

        // Invariant Loop: Iterate over the deduplicated set, NOT the raw array
        foreach (int num in numSet)
        {
            // Decision Gate: Check if 'num' is the TRUE HEAD of a consecutive sequence.
            // If (num - 1) exists in the set, 'num' is an internal node or tail.
            // Visiting an internal node is redundant; skip in O(1).
            if (!numSet.Contains(num - 1))
            {
                // Invariant: 'num' is guaranteed to be the lowest boundary of its component
                int currentNum = num;
                int currentStreak = 1;

                // Frontier Expansion: Walk along the consecutive chain
                while (numSet.Contains(currentNum + 1))
                {
                    currentNum++;
                    currentStreak++;
                }

                // Global State Resolution: Update the maximum streak observed
                longestStreak = Math.Max(longestStreak, currentStreak);
            }
        }

        return longestStreak;
    }
}

/// <summary>
/// Approach 2: Disjoint Set Union (Union-Find) with Path Compression & Union by Size.
/// Highly adaptable for dynamic streaming scenarios where numbers are inserted online.
/// </summary>
public class SolutionUnionFind
{
    public int LongestConsecutive(int[] nums)
    {
        if (nums == null || nums.Length == 0)
        {
            return 0;
        }

        var parent = new Dictionary<int, int>();
        var size = new Dictionary<int, int>();

        // Path compression find
        int Find(int i)
        {
            if (parent[i] == i) return i;
            return parent[i] = Find(parent[i]);
        }

        // Union by component size
        void Union(int i, int j)
        {
            int rootI = Find(i);
            int rootJ = Find(j);
            if (rootI != rootJ)
            {
                // Merge smaller tree into larger tree
                if (size[rootI] < size[rootJ])
                {
                    parent[rootI] = rootJ;
                    size[rootJ] += size[rootI];
                }
                else
                {
                    parent[rootJ] = rootI;
                    size[rootI] += size[rootJ];
                }
            }
        }

        // Streaming Ingestion
        foreach (int num in nums)
        {
            if (parent.ContainsKey(num)) continue; // Already processed

            parent[num] = num;
            size[num] = 1;

            // Connect with adjacent consecutive numbers if present
            if (parent.ContainsKey(num - 1)) Union(num, num - 1);
            if (parent.ContainsKey(num + 1)) Union(num, num + 1);
        }

        int maxStreak = 0;
        foreach (int componentSize in size.Values)
        {
            maxStreak = Math.Max(maxStreak, componentSize);
        }

        return maxStreak;
    }
}
```

---


## 6. Majority Element (LeetCode #169)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#boyer-moore-voting` `#frequency` `#array` `#O(1)-space` `#bit-manipulation` |
| **LeetCode Link** | [Majority Element](https://leetcode.com/problems/majority-element/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array `nums` of size $n$, return the majority element. The majority element is the element that appears more than $\lfloor n / 2 \rfloor$ times.
- **Contract / Guarantee:** You may assume that the majority element always exists in the array.
- **Key Constraints:**
  - `n == nums.Length`
  - `1 <= n <= 5 * 10^4`
  - `-10^9 <= nums[i] <= 10^9`
- **Senior Edge Cases to Defend:**
  - Singleton array: `nums = [42]` $\implies$ returns `42` instantly.
  - Alternating pattern: `nums = [1, 2, 1, 2, 1]` $\implies$ `1`. Candidate changes multiple times before final stabilization.
  - Uniform array: All elements identical $\implies$ count steadily reaches $n$.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Identify the dominant element with frequency strictly $> N / 2$ in $O(N)$ time and strictly $O(1)$ auxiliary space.
- **Sample 1:**
  - **Input:** `nums = [3, 2, 3]`
  - **Output:** `3`
- **Sample 2:**
  - **Input:** `nums = [2, 2, 1, 1, 1, 2, 2]`
  - **Output:** `2`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a gladiatorial battle royale arena. There are several factions, but one faction (the Majority) commands strictly more than 50% of the entire army ($> N / 2$).
The battle rule is *Mutually Assured Destruction*: whenever two warriors from *different* factions meet, they fight and eliminate each other 1-for-1.
Even in the worst possible scenario—where all minority factions unite, set aside their differences, and systematically sacrifice one of their soldiers to take down one majority soldier—the minority factions will run out of soldiers first! At the end of the war, the warriors left standing on the battlefield are guaranteed to belong to the majority faction.

#### 3.2 The Naive Bottleneck & Redundant Computation
- **Quadratic Counting ($O(N^2)$):** For every element, scanning the array to count its frequency wastes massive cycles.
- **Sorting ($O(N \log N)$):** Sorting the array places the majority element at index $\lfloor N / 2 \rfloor$. However, sorting requires comparing and ordering all elements, even though we do not care about relative order among minority elements.
- **Hash Map ($O(N)$ Time & $O(N)$ Space):** Storing counts in `Dictionary<int, int>` works in linear time, but consumes heap memory and incurs hashing overhead when $O(1)$ space is achievable.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Boyer-Moore Voting Invariant:**
  - Let $M$ be the majority element with count $c_M > N / 2$.
  - Let total non-majority elements have count $c_{\text{other}} < N / 2$.
  - When we cancel two distinct elements $(x, y)$ where $x \neq y$:
    - **Case 1: We cancel one majority and one non-majority.** Both counts decrement by 1. The new total size is $N - 2$. The new majority count is $c_M - 1$.
      $$\frac{c_M - 1}{N - 2} > \frac{N/2 - 1}{N - 2} = \frac{1}{2}$$
      $M$ remains strictly the majority in the remaining subproblem!
    - **Case 2: We cancel two non-majority elements.** $c_M$ remains unchanged while total size decreases by 2. $M$'s percentage dominance *increases*!
  - Therefore, whenever `count` drops to zero, the prefix traversed so far has completely balanced out and can be discarded without altering the majority identity of the remaining suffix!

#### 3.4 Cursor Semantics & Invariant Partition Architecture
Two CPU registers track state:
- `candidate`: The active champion currently holding the hill.
- `count`: The surplus headcount of the current candidate over challengers.

```text
[ 0 ................... i - 1 ]            [   i   ]           [ i + 1 ......... N - 1 ]
└──────────────┬──────────────┘            └───┬───┘           └───────────┬───────────┘
   Prefix elements paired and canceled       Challenger              Unprocessed Stream
   OR represented by 'candidate' with        num = nums[i]
   positive surplus 'count'
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Decision Gate 1 (Vacancy / Reseat):**
   - If `count == 0`: Crown current element as champion: `candidate = nums[i]`, `count = 1`.
2. **Decision Gate 2 (Reinforce vs Challenge):**
   - If `nums[i] == candidate`: Reinforce: `count++`.
   - If `nums[i] != candidate`: Mutually annihilate: `count--`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `nums = [2, 2, 1, 1, 1, 2, 2]`

| Step ($i$) | `nums[i]` | Pre-Condition `count` | Invariant Check | State Mutation | Post-State `(candidate, count)` |
| :---: | :---: | :---: | :---: | :---: | :---: |
| `i = 0` | `2` | `0` | Vacancy | Crown `2`, set `count = 1` | `(2, 1)` |
| `i = 1` | `2` | `1` | `2 == 2` | Reinforce `count++` | `(2, 2)` |
| `i = 2` | `1` | `2` | `1 != 2` | Annihilate `count--` | `(2, 1)` |
| `i = 3` | `1` | `1` | `1 != 2` | Annihilate `count--` | `(2, 0)` |
| `i = 4` | `1` | `0` | Vacancy | Crown `1`, set `count = 1` | `(1, 1)` |
| `i = 5` | `2` | `1` | `2 != 1` | Annihilate `count--` | `(1, 0)` |
| `i = 6` | `2` | `0` | Vacancy | Crown `2`, set `count = 1` | `(2, 1)` |

Final Answer: `candidate = 2`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Boyer-Moore Voting (Optimal)**
  - *When to Use:* Always. Strictly $O(N)$ runtime, $O(1)$ auxiliary memory, zero heap allocation.
  - *Caveat:* Relies on the contract that a majority element *actually exists*. If existence is not guaranteed, a second $O(N)$ verification pass is mandatory.
- **Approach 2: Bit Manipulation (Bit-by-Bit Reconstruction)**
  - *When to Use:* Great conceptual demonstration of bitwise aggregation. If the majority element appears $> N / 2$ times, each bit in its 32-bit binary representation must also appear $> N / 2$ times across the array.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Step 1: Setup:** Initialize `candidate = 0`, `count = 0`.
2. **Step 2: Stream Traversal:** Loop through each number in `nums`.
3. **Step 3: Crown or Adjust:** If `count == 0`, assign `candidate = num`. Increment if `num == candidate`, else decrement.
4. **Step 4: Return:** Return `candidate`.

#### 4.3 Alternative Approaches Analysis
- **Median via QuickSelect / Introselect:** Finding the median in $O(N)$ average time works because the majority element always spans across the median position. However, Boyer-Moore is simpler, non-destructive, and has vastly smaller constant factors.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Boyer-Moore** | $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ | $O(1)$ | Perfect (single stream) | Non-mutating | **Excellent** (online) |
| **2. Bit Manipulation** | $O(32N)$ | $O(32N)$ | $O(32N)$ | $O(1)$ | $O(1)$ | High | Non-mutating | Moderate |
| **3. Hash Map** | $O(N)$ | $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ | Moderate | Non-mutating | High |
| **4. Array.Sort** | $O(N \log N)$ | $O(N \log N)$ | $O(N \log N)$ | $O(1)$ | $O(1)$ | High | Mutates input | Poor |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Majority Element
 * ============================================================================
 * Core Pattern      : Boyer-Moore Majority Voting Algorithm (Pairwise Annihilation)
 * Time Complexity   : O(N) strictly linear single pass
 * Space Complexity  : O(1) auxiliary space (two scalar registers)
 * Selection Rule    : Default to Boyer-Moore whenever > N / 2 majority is guaranteed.
 * Defensive Traps   : If majority is not guaranteed by problem contract, a second O(N)
 *                     verification pass is mandatory to validate candidate frequency > N / 2.
 * ============================================================================
 */

public class Solution
{
    /// <summary>
    /// Identifies the majority element (> N / 2 occurrences) using the Boyer-Moore algorithm.
    /// Operates in O(N) time with strictly O(1) auxiliary registers.
    /// </summary>
    public int MajorityElement(int[] nums)
    {
        // Guard Clause: Single-element array trivially satisfies majority
        if (nums == null || nums.Length == 0)
        {
            throw new ArgumentException("Array cannot be null or empty.", nameof(nums));
        }

        int candidate = nums[0];
        int count = 0;

        // Invariant Loop: Process elements via pairwise cancellation
        foreach (int num in nums)
        {
            // Decision Gate 1: If current surplus is 0, the previous prefix has completely canceled out.
            // Adopt the current element as the new reigning candidate.
            if (count == 0)
            {
                candidate = num;
            }

            // Decision Gate 2: Reinforce candidate if matching; annihilate if challenger
            count += (num == candidate) ? 1 : -1;
        }

        // Post-Condition: Under the guarantee that a majority element exists,
        // the surviving candidate is mathematically proven to be the majority element.
        return candidate;
    }
}

/// <summary>
/// Approach 2: Bit Manipulation Aggregation.
/// Reconstructs the majority element bit-by-bit across 32 bit planes.
/// </summary>
public class SolutionBitManipulation
{
    public int MajorityElement(int[] nums)
    {
        int majority = 0;
        int n = nums.Length;
        int threshold = n / 2;

        // Inspect all 32 bits of standard signed integers
        for (int b = 0; b < 32; b++)
        {
            int bitMask = 1 << b;
            int bitCount = 0;

            // Count frequency of 1s at bit position b across all numbers
            foreach (int num in nums)
            {
                if ((num & bitMask) != 0)
                {
                    bitCount++;
                }
            }

            // Invariant: If bit 'b' appears more than N / 2 times,
            // it MUST belong to the majority element
            if (bitCount > threshold)
            {
                majority |= bitMask;
            }
        }

        return majority;
    }
}
```

---


## 7. Product of Array Except Self (LeetCode #238)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#prefix-suffix-product` `#array` `#space-optimization` `#O(1)-aux-space` |
| **LeetCode Link** | [Product of Array Except Self](https://leetcode.com/problems/product-of-array-except-self/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an integer array `nums`, return an array `answer` such that `answer[i]` is equal to the product of all the elements of `nums` except `nums[i]`.
- **Strict Constraints:**
  - Must run in $O(N)$ time.
  - **You must write an algorithm that runs without using the division operation.**
  - **Follow up:** Can you solve the problem in $O(1)$ extra auxiliary space? (The output array does not count as extra space for space complexity analysis).
- **Key Constraints:**
  - `2 <= nums.Length <= 10^5`
  - `-30 <= nums[i] <= 30`
  - The product of any prefix or suffix of `nums` is guaranteed to fit in a 32-bit integer.
- **Senior Edge Cases to Defend:**
  - Single zero: `nums = [1, 2, 0, 4]` $\implies$ `[0, 0, 8, 0]`.
  - Multiple zeros: `nums = [0, 2, 0, 4]` $\implies$ `[0, 0, 0, 0]`.
  - Negative values: Alternating positive and negative signs.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Decompose each element's product into independent left-prefix and right-suffix cumulative products, accumulated in-place within the output array.
- **Sample 1:**
  - **Input:** `nums = [1, 2, 3, 4]`
  - **Output:** `[24, 12, 8, 6]`
  - **Explanation:**
    - `answer[0] = 2 * 3 * 4 = 24`
    - `answer[1] = 1 * 3 * 4 = 12`
    - `answer[2] = 1 * 2 * 4 = 8`
    - `answer[3] = 1 * 2 * 3 = 6`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine standing on a straight highway at mile marker $i$. You want to know the combined weight of all vehicles on the highway except yours. You look through your rear-view mirror: you can see every vehicle behind you ($0 \dots i - 1$) and multiply their weights into a `PrefixProduct`. Then you turn your head forward through the windshield: you see every vehicle ahead of you ($i + 1 \dots N - 1$) and multiply their weights into a `SuffixProduct`.
The vehicle you are sitting in is the **blind spot**: it is never factored into either observation!
$$\text{Total Product Except Self} = (\text{Everything Behind}) \times (\text{Everything Ahead})$$

#### 3.2 The Naive Bottleneck & Redundant Computation
- **Brute Force ($O(N^2)$):** For each index $i$, running a loop over all $j \neq i$ takes $N(N - 1)$ multiplications.
- **Division Approach ($O(N)$ with Division):**
  ```csharp
  int total = 1;
  foreach (int x in nums) total *= x;
  for (int i = 0; i < n; i++) answer[i] = total / nums[i];
  ```
  - *Why this is rejected:*
    1. Strictly forbidden by problem contract.
    2. Crashes with `DivideByZeroException` when `nums[i] == 0`.
    3. If there are multiple zeros, managing division requires complex branch logic.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Algebraic Prefix/Suffix Split:**
  $$\text{answer}[i] = \prod_{k \neq i} nums[k] = \left( \prod_{k=0}^{i-1} nums[k] \right) \times \left( \prod_{k=i+1}^{N-1} nums[k] \right)$$
- **Output Array In-Place Reuse ($O(1)$ Auxiliary Space):**
  - Allocate output array `result = new int[N]`.
  - **Pass 1 (Left to Right):** Build prefix products directly into `result`:
    `result[i] = product of all elements in nums[0 .. i - 1]`.
  - **Pass 2 (Right to Left):** Maintain a single scalar variable `suffixProduct` (initially 1).
    Multiply `result[i]` by `suffixProduct`, then update `suffixProduct *= nums[i]`.
  - Result: Zero auxiliary arrays allocated! Auxiliary space is strictly $O(1)$.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
- **Pass 1 (Forward Cursor $i: 0 \dots N - 1$):**
  - `result[i]` holds $\prod_{k=0}^{i-1} nums[k]$. (Convention: $\prod \emptyset = 1$).
- **Pass 2 (Backward Cursor $i: N - 1 \dots 0$):**
  - `suffixProduct` holds $\prod_{k=i+1}^{N-1} nums[k]$.
  - `result[i] \leftarrow result[i] \times suffixProduct`.

```text
Forward Pass:
result[i] = [ nums[0] * nums[1] * ... * nums[i - 1] ]  (Prefix Cumulative)

Backward Pass:
result[i] = result[i] * [ nums[i + 1] * ... * nums[N - 1] ] (Combined with Suffix)
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Pass 1 Initialization:** `result[0] = 1`.
2. **Pass 1 Invariant Maintenance:**
   - For $i = 1 \dots N - 1$:
     `result[i] = result[i - 1] * nums[i - 1]`.
3. **Pass 2 Initialization:** `suffixProduct = 1`.
4. **Pass 2 Invariant Maintenance:**
   - For $i = N - 1 \text{ down to } 0$:
     `result[i] *= suffixProduct`
     `suffixProduct *= nums[i]`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `nums = [1, 2, 3, 4]`, $N = 4$.

**Pass 1: Left-to-Right (Prefix Product Population)**
- `result[0] = 1`
- `i = 1`: `result[1] = result[0] * nums[0] = 1 * 1 = 1`
- `i = 2`: `result[2] = result[1] * nums[1] = 1 * 2 = 2`
- `i = 3`: `result[3] = result[2] * nums[2] = 2 * 3 = 6`
- *State after Pass 1:* `result = [1, 1, 2, 6]`

**Pass 2: Right-to-Left (Suffix Product Accumulation)**
- Initial `suffixProduct = 1`.

| Step ($i$) | `nums[i]` | Pre `result[i]` | `suffixProduct` Before | `result[i] *= suffixProduct` | Next `suffixProduct` (`* nums[i]`) |
| :---: | :---: | :---: | :---: | :---: | :---: |
| `i = 3` | `4` | `6` | `1` | `6 * 1 = 6` | `1 * 4 = 4` |
| `i = 2` | `3` | `2` | `4` | `2 * 4 = 8` | `4 * 3 = 12` |
| `i = 1` | `2` | `1` | `12` | `1 * 12 = 12` | `12 * 2 = 24` |
| `i = 0` | `1` | `1` | `24` | `1 * 24 = 24` | `24 * 1 = 24` |

Final `result = [24, 12, 8, 6]`. Correct!

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Output Array Reuse + Rolling Suffix Scalar (Optimal)**
  - *When to Use:* Always. Strictly $O(N)$ runtime, $O(1)$ auxiliary memory (output array does not count per contract), zero heap garbage collection.
- **Approach 2: Explicit Left & Right Product Arrays**
  - *When to Use:* Pedagogical stepping stone in an interview to explain prefix/suffix separation before introducing the space optimization.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Step 1: Allocation:** Allocate `result = new int[nums.Length]`.
2. **Step 2: Forward Sweep:** Accumulate prefix products directly into `result`.
3. **Step 3: Backward Sweep:** Initialize scalar `suffixProduct = 1`. Multiply backwards and accumulate.
4. **Step 4: Return:** Return populated `result`.

#### 4.3 Alternative Approaches Analysis
- **Logarithmic Sums:** $\prod x_k = 10^{\sum \log_{10} x_k}$. Flaw: Floating-point precision truncation and inability to handle negative numbers or zeros cleanly.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Output Reuse + Scalar** | $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ | $O(N)$ | High (sequential scans) | Non-mutating | Poor (two passes) |
| **2. Explicit L & R Arrays** | $O(N)$ | $O(N)$ | $O(N)$ | $O(N)$ | $O(N)$ | High | Non-mutating | Poor |
| **3. Brute Force** | $O(N^2)$ | $O(N^2)$ | $O(N^2)$ | $O(1)$ | $O(N)$ | High | Non-mutating | Poor |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Product of Array Except Self
 * ============================================================================
 * Core Pattern      : Prefix-Suffix Product Decomposition (In-Place Output Reuse)
 * Time Complexity   : O(N) two linear passes
 * Space Complexity  : O(1) auxiliary space (output array is excluded from auxiliary budget)
 * Selection Rule    : Default to Approach 1 to fulfill the O(1) auxiliary space requirement.
 * Defensive Traps   : Do not use division. Handle zeros naturally via algebraic multiplication.
 *                     Base cases for prefix[0] and suffix[N-1] must be initialized to 1.
 * ============================================================================
 */

public class Solution
{
    /// <summary>
    /// Computes the product of all elements except nums[i] without division in O(N) time.
    /// Reuses the output array to achieve strictly O(1) auxiliary heap space.
    /// </summary>
    public int[] ProductExceptSelf(int[] nums)
    {
        // Guard Clause: Contract guarantees nums.Length >= 2
        if (nums == null || nums.Length < 2)
        {
            throw new ArgumentException("Array must contain at least 2 elements.", nameof(nums));
        }

        int n = nums.Length;
        int[] result = new int[n];

        // Pass 1: Accumulate prefix products strictly to the left of index i
        // Base case: There are no elements to the left of index 0, so prefix product is 1
        result[0] = 1;
        for (int i = 1; i < n; i++)
        {
            // Invariant: result[i] contains the product of all elements nums[0 .. i - 1]
            result[i] = result[i - 1] * nums[i - 1];
        }

        // Pass 2: Accumulate suffix products strictly to the right of index i using a rolling scalar
        int suffixProduct = 1;
        for (int i = n - 1; i >= 0; i--)
        {
            // Invariant: Multiply existing left prefix with the accumulated right suffix
            result[i] *= suffixProduct;

            // Update rolling suffix product for the next element to the left
            suffixProduct *= nums[i];
        }

        return result;
    }
}

/// <summary>
/// Approach 2: Explicit Left and Right Product Arrays.
/// Clear pedagogical separation of prefix and suffix concepts using O(N) auxiliary space.
/// </summary>
public class SolutionExplicitArrays
{
    public int[] ProductExceptSelf(int[] nums)
    {
        if (nums == null || nums.Length < 2)
        {
            throw new ArgumentException("Array must contain at least 2 elements.", nameof(nums));
        }

        int n = nums.Length;
        int[] leftProducts = new int[n];
        int[] rightProducts = new int[n];
        int[] result = new int[n];

        // Build Left Products
        leftProducts[0] = 1;
        for (int i = 1; i < n; i++)
        {
            leftProducts[i] = leftProducts[i - 1] * nums[i - 1];
        }

        // Build Right Products
        rightProducts[n - 1] = 1;
        for (int i = n - 2; i >= 0; i--)
        {
            rightProducts[i] = rightProducts[i + 1] * nums[i + 1];
        }

        // Combine Left and Right
        for (int i = 0; i < n; i++)
        {
            result[i] = leftProducts[i] * rightProducts[i];
        }

        return result;
    }
}
```

# Phase 17: Backtracking

> **Focus:** State-Space Decision Trees, Choose $\to$ Explore $\to$ Undo Lifecycle, Unbounded Choice with Sum Pruning, In-Place Grid Backtracking, and Diagonal Constraint Propagation in N-Queens (Boolean Arrays vs Bit Manipulation).  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 17 (Problems #93–#98)

---
## 93. Subsets (LeetCode #78)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#backtracking` `#cascade-inclusion` `#bitmask` `#power-set` |
| **LeetCode Link** | [Subsets](https://leetcode.com/problems/subsets/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an integer array `nums` of unique elements, return all possible subsets (the power set). The solution set must not contain duplicate subsets. Return the solution in any order.
- **Key Constraints:**
  - $1 \le nums.Length \le 10$.
  - $-10 \le nums[i] \le 10$.
  - All numbers in `nums` are unique.
- **Senior Edge Cases to Defend:**
  - Empty subset `[]` is always a valid subset and must be included in the output.
  - Snapshotting defense: When saving `current` into `result`, allocate a new list copy (`new List<int>(current)`). Saving a reference causes all outputs to mutate to empty upon backtracking unwind.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Power Set Generation ($2^N$ total configurations). Can be explored via a DFS state-space decision tree where each node represents a valid subset, or via binary bitmasks $0 \dots 2^N - 1$.
- **Sample 1:**
  - **Input:** `nums = [1, 2, 3]`
  - **Output:** `[[], [1], [2], [1, 2], [3], [1, 3], [2, 3], [1, 2, 3]]`
  - **Explanation:** $2^3 = 8$ distinct subsets generated.
- **Sample 2:**
  - **Input:** `nums = [0]`
  - **Output:** `[[], [0]]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine packing a backpack for a hike from a selection of $N$ unique items. For each item $nums[i]$, you face an independent binary decision: include it or leave it out. Across $N$ items, this binary branching creates a full decision tree of $2^N$ leaves.
Alternatively, view this as a **Prefix Expansion Tree**: Start with the empty bag `[]`. At each step, choose any item $nums[i]$ from the remaining available pool ($i \ge start$), add it to the bag, take a snapshot of the bag's current contents, and recurse with remaining pool starting at $i + 1$. Because every intermediate state is a valid subset, snapshots are taken at **every single node of the tree**, not just the leaves!

#### 3.2 The Naive Bottleneck & Redundant Computation
A naive permutation-style backtracking algorithm would explore all choices from index $0$ to $N - 1$ at every level:
```text
                  []
           /       |       \
         [1]      [2]      [3]
        /   \    /   \    /   \
      [1,2] [1,3] [2,1] [2,3] ...
```
This generates duplicate subset configurations like `[1, 2]` and `[2, 1]`, requiring an expensive `HashSet` or canonical sorting to deduplicate, blowing up complexity to $O(N! \cdot N)$.
By strictly enforcing **ascending index selection** via a `start` cursor, `[2, 1]` is never generated because 1 appears before 2 in the source array.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Strict Index Ordering Invariant:**
At any recursive depth, if the last chosen element was $nums[i]$, all future elements added to the subset must come from indices $j > i$:
$$\forall k_1 < k_2, \quad \text{Index}(subset[k_1]) < \text{Index}(subset[k_2])$$
- This guarantees a bijection between the search tree nodes and the $2^N$ subsets of `nums`. Zero duplicate subsets are explored; zero deduplication memory is wasted.
- **Backtracking Lifecycle (Choose $\to$ Explore $\to$ Undo):**
  1. `current.Add(nums[i])`: Choose candidate $nums[i]$.
  2. `Backtrack(i + 1)`: Explore all subsets containing $nums[i]$.
  3. `current.RemoveAt(current.Count - 1)`: Undo the choice, restoring the candidate list to explore subsequent choices.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
STATE-SPACE SUBSET DECISION TREE:
                                 []  <-- Snapshot []
                  ┌──────────────┼──────────────┐
                 [1]            [2]            [3]  <-- Snapshots [1], [2], [3]
             ┌────┴────┐         │
           [1,2]     [1,3]     [2,3]               <-- Snapshots [1,2], [1,3], [2,3]
             │
          [1,2,3]                                  <-- Snapshot [1,2,3]

Current Selection: [ nums[c_1], nums[c_2] ]
Candidate Pool:    i in [ start ... N - 1 ]
```

- `start`: The lower bound index of candidates permitted to be added to `current`.
- `current`: The dynamic accumulator tracking the subset path along the current branch.
- `result`: The output collection aggregating deep copies of each subset.
- **Invariant:** At any node in the tree, `current` is a valid, unique subset.

#### 3.5 State Transition Triggers & Decision Gates
At entry to `Backtrack(start)`:
1. **Snapshot Gate:** Always execute `result.Add(new List<int>(current))` immediately.
2. **Candidate Scan Gate:** Loop $i$ from $start$ to $N - 1$:
   - **Choose Gate:** `current.Add(nums[i])`.
   - **Explore Gate:** Recurse `Backtrack(i + 1)`.
   - **Undo Gate:** `current.RemoveAt(current.Count - 1)`.
3. **Termination Gate:** Loop naturally terminates when $start \ge N$.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `nums = [1, 2, 3]`.

| Tree Step | Action | `current` List | Action on `result` |
| :---: | :--- | :--- | :--- |
| **1** | Root call (`start = 0`) | `[]` | Add `[]` |
| **2** | Choose 1 (`start = 1`) | `[1]` | Add `[1]` |
| **3** | Choose 2 (`start = 2`) | `[1, 2]` | Add `[1, 2]` |
| **4** | Choose 3 (`start = 3`) | `[1, 2, 3]` | Add `[1, 2, 3]` |
| **5** | Undo 3 $\to$ Undo 2 | `[1]` | Backtrack |
| **6** | Choose 3 (`start = 3`) | `[1, 3]` | Add `[1, 3]` |
| **7** | Undo 3 $\to$ Undo 1 | `[]` | Backtrack to root |
| **8** | Choose 2 (`start = 2`) | `[2]` | Add `[2]` |
| **9** | Choose 3 (`start = 3`) | `[2, 3]` | Add `[2, 3]` |
| **10**| Undo 3 $\to$ Undo 2 | `[]` | Backtrack to root |
| **11**| Choose 3 (`start = 3`) | `[3]` | Add `[3]` |
| **12**| Undo 3 | `[]` | Search complete! |

Total subsets collected: 8.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Backtracking Choose-Explore-Undo):** Standard interview solution. Intuitive, easily extensible to problems with constraints (e.g. subset sum, combinations), and maintains $O(N)$ stack memory.
- **Approach 2 (Bitmask Enumeration):** Iterative, non-recursive solution. Useful when $N \le 32$ and you want a non-recursive, easily parallelizable method. It directly exploits the isomorphism between binary strings of length $N$ and subsets of an $N$-element set.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Allocate `result` list with pre-calculated capacity $1 \ll N$ ($2^N$).
- **Step 2: Initialize Recursion:** Call `Backtrack(0, current)`.
- **Step 3: Condition Gates & Recursion:** Snapshot at every call, loop $i$ from $start$ to $N - 1$, choose, recurse with $i + 1$, undo.
- **Step 4: Resolution & Return:** Return `result`.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Bitmask Enumeration ($O(N \cdot 2^N)$):**
  - Loop integer `mask` from $0$ to $(1 \ll N) - 1$.
  - For each bit $i \in [0 \dots N - 1]$: if `(mask & (1 << i)) != 0`, include `nums[i]`.
  - Add subset to result.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Backtracking (Choose-Explore-Undo) | Approach 2: Bitmask Enumeration |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(N \cdot 2^N)$ | $O(N \cdot 2^N)$ |
| **Auxiliary Space** | $O(N)$ recursion stack depth | $O(1)$ auxiliary storage |
| **Output Space** | $O(N \cdot 2^N)$ total elements stored | $O(N \cdot 2^N)$ total elements stored |
| **Cache Locality** | Moderate | High (pure sequential iteration) |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | High (yields subsets incrementally) | High |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #93 - Subsets
// Core Pattern: Backtracking State-Space Tree / Choose-Explore-Undo
// Primary Invariant: Candidates restricted to j >= start to prevent duplicate subsets.
// Snapshot Invariant: Every node in the recursion tree represents a distinct subset.
// Memory Defense: Pre-allocate capacity (1 << n) to eliminate list resizing overhead.
// ============================================================================
```

#### Implementation 1: Backtracking Decision Tree (Production Standard)
```csharp
public class Solution
{
    public IList<IList<int>> Subsets(int[] nums)
    {
        // Guard Clause
        if (nums == null || nums.Length == 0)
        {
            return new List<IList<int>> { new List<int>() };
        }

        int n = nums.Length;
        // Total subsets for N distinct elements is exactly 2^N
        int totalSubsets = 1 << n;

        var result = new List<IList<int>>(capacity: totalSubsets);
        var current = new List<int>(capacity: n);

        Backtrack(nums, 0, current, result);
        return result;
    }

    private static void Backtrack(int[] nums, int start, List<int> current, List<IList<int>> result)
    {
        // Snapshot Invariant: Take snapshot at EVERY node, including the root empty set
        // Must create a new list instance; otherwise future mutations affect stored results
        result.Add(new List<int>(current));

        // Explore all available candidates starting from 'start'
        for (int i = start; i < nums.Length; i++)
        {
            // 1. Choose: Place candidate in active subset
            current.Add(nums[i]);

            // 2. Explore: Recurse forward with i + 1 to enforce ascending index invariant
            Backtrack(nums, i + 1, current, result);

            // 3. Undo: Remove candidate before evaluating the next branch
            current.RemoveAt(current.Count - 1);
        }
    }
}
```

#### Implementation 2: Bitmask Enumeration ($O(N \cdot 2^N)$ Iterative)
```csharp
public class SolutionBitmask
{
    public IList<IList<int>> Subsets(int[] nums)
    {
        if (nums == null || nums.Length == 0)
        {
            return new List<IList<int>> { new List<int>() };
        }

        int n = nums.Length;
        int totalSubsets = 1 << n; // 2^N
        var result = new List<IList<int>>(capacity: totalSubsets);

        // Every integer from 0 to 2^N - 1 corresponds to a unique subset
        for (int mask = 0; mask < totalSubsets; mask++)
        {
            var subset = new List<int>();

            for (int i = 0; i < n; i++)
            {
                // Invariant: If the i-th bit is set, include nums[i]
                if ((mask & (1 << i)) != 0)
                {
                    subset.Add(nums[i]);
                }
            }

            result.Add(subset);
        }

        return result;
    }
}
```

---

## 94. Permutations (LeetCode #46)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#backtracking` `#in-place-swap` `#visited-array` |
| **LeetCode Link** | [Permutations](https://leetcode.com/problems/permutations/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array `nums` of distinct integers, return all possible permutations in any order.
- **Key Constraints:**
  - $1 \le nums.Length \le 6$.
  - $-10 \le nums[i] \le 10$.
  - All the integers of `nums` are unique.
- **Senior Edge Cases to Defend:**
  - Single element array ($N = 1 \implies [[nums[0]]]$).
  - Preserving input array state if caller expects non-destructive operations.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Generating all $N!$ distinct orderings. Can be solved with zero auxiliary memory via an in-place element swapping partition, or via a boolean `used` tracking array.
- **Sample 1:**
  - **Input:** `nums = [1, 2, 3]`
  - **Output:** `[[1,2,3],[1,3,2],[2,1,3],[2,3,1],[3,1,2],[3,2,1]]`
  - **Explanation:** $3! = 6$ total permutations.
- **Sample 2:**
  - **Input:** `nums = [0, 1]`
  - **Output:** `[[0,1],[1,0]]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a row of $N$ chairs. You have $N$ people standing before you.
For chair 0, you have $N$ choices. You invite person $i$ to sit in chair 0.
For chair 1, you have $N - 1$ remaining choices.
For chair 2, you have $N - 2$ remaining choices.
Proceeding down the row until all chairs are occupied forms an arrangement of length $N$. The total number of valid permutations is $N \times (N - 1) \times (N - 2) \dots \times 1 = N!$.
Rather than allocating a separate list of available people, you can partition the input array in-place: the people sitting in chairs $0 \dots first - 1$ are fixed, while people standing in positions $first \dots N - 1$ form the candidate pool. Swapping person $i$ into position $first$ makes the choice in $O(1)$ space!

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force generator generates all $N^N$ sequences of length $N$ and then checks if all elements are distinct:
$$T(N) = O(N^N \cdot N)$$
For $N = 6$, $6^6 = 46,656$ compared to $6! = 720$. Permutation backtracking prunes this completely by never choosing an already used element, visiting strictly $N!$ states.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**In-Place Array Swapping Partition:**
Divide `nums` into two virtual zones:
1. **Settled / Fixed Prefix:** `nums[0 .. first - 1]`.
2. **Unplaced Candidate Pool:** `nums[first .. N - 1]`.
- For each index $i \in [first \dots N - 1]$:
  1. `Swap(first, i)`: Elects `nums[i]` to be the occupant of position `first`.
  2. `Backtrack(first + 1)`: Recursively fills positions $first + 1 \dots N - 1$.
  3. `Swap(first, i)`: Restores `nums[i]` back to position $i$ so subsequent candidates can be tested cleanly.
- **Auxiliary Space:** Strictly $O(N)$ call-stack depth. Zero auxiliary lists or boolean arrays required during traversal!

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
IN-PLACE SWAP PARTITION ARCHITECTURE:
nums array:
[ 0 ................... first - 1 ]  |  [ first ]  |  [ first + 1 ............ N - 1 ]
└──────────────┬──────────────────┘     └───┬───┘     └──────────────┬────────────────┘
     Fixed & Settled Positions             Active        Unplaced Candidate Pool
                                            Slot

Swap nums[first] with nums[i] (where i >= first)
Recurse: Backtrack(first + 1)
Undo: Swap nums[first] with nums[i]
```

- `first`: The active slot index currently being filled ($0 \le first \le N$).
- `i`: The candidate element cursor scanning the unplaced pool ($first \le i < N$).
- **Base Case Invariant:** When `first == nums.Length`, all $N$ positions have been settled; snapshot `new List<int>(nums)`.

#### 3.5 State Transition Triggers & Decision Gates
At each recursive step:
1. **Terminal Gate:** If `first == nums.Length`, record snapshot and return.
2. **Candidate Sweep Gate:** Loop $i$ from $first$ to $N - 1$:
   - **Choose:** `(nums[first], nums[i]) = (nums[i], nums[first])`.
   - **Explore:** `Backtrack(first + 1)`.
   - **Undo:** `(nums[first], nums[i]) = (nums[i], nums[first])`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `nums = [1, 2, 3]`.

| Tree Depth (`first`) | Candidate $i$ | Action | Array State `nums` | Result Snapshot |
| :---: | :---: | :--- | :--- | :--- |
| **0** | — | Call `Backtrack(0)` | `[1, 2, 3]` | — |
| **0** | $i = 0$ | Swap(0, 0) | `[1, 2, 3]` | — |
| **1** | $i = 1$ | Swap(1, 1) | `[1, 2, 3]` | — |
| **2** | $i = 2$ | Swap(2, 2) | `[1, 2, 3]` | — |
| **3** | — | `first == 3` (Base) | `[1, 2, 3]` | Add `[1, 2, 3]` |
| **2** | Undo(2, 2) | | `[1, 2, 3]` | |
| **1** | $i = 2$ | Swap(1, 2) | `[1, 3, 2]` | — |
| **2** | $i = 2$ | Swap(2, 2) | `[1, 3, 2]` | — |
| **3** | — | `first == 3` (Base) | `[1, 3, 2]` | Add `[1, 3, 2]` |
| **1** | Undo(1, 2) | | `[1, 2, 3]` | |
| **0** | Undo(0, 0) | | `[1, 2, 3]` | |
| **0** | $i = 1$ | Swap(0, 1) | `[2, 1, 3]` | Proceeds to generate `[2, 1, 3]` and `[2, 3, 1]` |

Total permutations generated: $3! = 6$.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (In-Place Swapping):** The senior engineer's choice for minimal memory footprints. It mutates `nums` in-place, eliminating auxiliary visited arrays and dynamic list allocations until the base case snapshot.
- **Approach 2 (Boolean Visited Array):** Preferred when **lexicographical output order** is strictly required, because swapping re-orders elements arbitrarily. Uses a `bool[] used` array.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Handle single element array. Calculate factorial capacity for `result`.
- **Step 2: Recursive Search:** Call `Backtrack(0)`.
- **Step 3: Invariant Maintenance:** Swap candidate $i$ into slot `first`, recurse on `first + 1`, swap back.
- **Step 4: Base Case Snapshot:** When `first == N`, snapshot array into `result`. Return `result`.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Boolean Visited Array ($O(N \cdot N!)$):**
  - Maintain `current` list and `bool[] used` array.
  - At each step, iterate $i \in [0 \dots N - 1]$. If `!used[i]`, mark `used[i] = true`, add `nums[i]` to `current`, recurse, remove from `current`, mark `used[i] = false`.
  - Guarantees permutations are generated in lexicographical order if `nums` is pre-sorted.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: In-Place Swapping (Optimal Space) | Approach 2: Boolean Visited Array |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(N \cdot N!)$ | $O(N \cdot N!)$ |
| **Auxiliary Space** | $O(N)$ call stack depth | $O(N)$ visited array + $O(N)$ path list |
| **Output Space** | $O(N \cdot N!)$ permutations | $O(N \cdot N!)$ permutations |
| **Cache Locality** | High (in-place array mutations) | Moderate (list and array reads) |
| **In-Place Mutability** | Temporary mutation (restored upon return) | Non-destructive |
| **Streaming Suitability** | High | High |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #94 - Permutations
// Core Pattern: Backtracking In-Place Swap Partition
// Primary Invariant: [0 .. first - 1] is settled; [first .. N - 1] is candidate pool.
// Space Defense: Zero tracking arrays allocated; array mutated and restored in-place.
// Order Tradeoff: Swapping produces non-lexicographical order; use Visited array if sorted order required.
// ============================================================================
```

#### Implementation 1: In-Place Swapping (Optimal Space Standard)
```csharp
public class Solution
{
    public IList<IList<int>> Permute(int[] nums)
    {
        // Guard Clause
        if (nums == null || nums.Length == 0)
        {
            return new List<IList<int>>();
        }

        int n = nums.Length;
        int factorial = 1;
        for (int i = 2; i <= n; i++) factorial *= i;

        var result = new List<IList<int>>(capacity: factorial);

        Backtrack(nums, 0, result);
        return result;
    }

    private static void Backtrack(int[] nums, int first, List<IList<int>> result)
    {
        // Base Case: All positions 0 .. N - 1 have been settled
        if (first == nums.Length)
        {
            // Snapshot current permutation
            result.Add(new List<int>(nums));
            return;
        }

        // Iterate through all unplaced candidates in nums[first .. N - 1]
        for (int i = first; i < nums.Length; i++)
        {
            // 1. Choose: Place candidate nums[i] into the active position 'first'
            (nums[first], nums[i]) = (nums[i], nums[first]);

            // 2. Explore: Recurse to fill the next position (first + 1)
            Backtrack(nums, first + 1, result);

            // 3. Undo: Swap back to restore array configuration before trying next candidate
            (nums[first], nums[i]) = (nums[i], nums[first]);
        }
    }
}
```

#### Implementation 2: Boolean Visited Array (Lexicographical Order)
```csharp
public class SolutionVisited
{
    public IList<IList<int>> Permute(int[] nums)
    {
        if (nums == null || nums.Length == 0) return new List<IList<int>>();

        var result = new List<IList<int>>();
        var current = new List<int>(nums.Length);
        bool[] used = new bool[nums.Length];

        Backtrack(nums, used, current, result);
        return result;
    }

    private static void Backtrack(int[] nums, bool[] used, List<int> current, List<IList<int>> result)
    {
        // Base Case: All items selected
        if (current.Count == nums.Length)
        {
            result.Add(new List<int>(current));
            return;
        }

        for (int i = 0; i < nums.Length; i++)
        {
            // Skip already placed candidates
            if (used[i]) continue;

            // Choose
            used[i] = true;
            current.Add(nums[i]);

            // Explore
            Backtrack(nums, used, current, result);

            // Undo
            current.RemoveAt(current.Count - 1);
            used[i] = false;
        }
    }
}
```

---

## 95. Combination Sum (LeetCode #39)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#backtracking` `#unbounded-choice` `#sum-pruning` |
| **LeetCode Link** | [Combination Sum](https://leetcode.com/problems/combination-sum/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array of distinct integers `candidates` and a target integer `target`, return a list of all unique combinations of candidates where chosen numbers sum to target. The same candidate may be chosen unlimited times.
- **Key Constraints:**
  - $1 \le candidates.Length \le 30$.
  - $2 \le candidates[i] \le 40$.
  - All elements of `candidates` are distinct.
  - $1 \le target \le 40$.
- **Senior Edge Cases to Defend:**
  - Target smaller than smallest candidate: Handled cleanly by early sort and loop break.
  - Reuse of elements: Must allow taking candidate $i$ multiple times without allowing backward traversal (which would introduce duplicate permutation sets).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Unbounded Knapsack Search Tree with Early Pruning: Sort `candidates` ascending; when candidate exceeds remaining sum, break the loop to prune the entire branch.
- **Sample 1:**
  - **Input:** `candidates = [2, 3, 6, 7], target = 7`
  - **Output:** `[[2, 2, 3], [7]]`
  - **Explanation:** $2 + 2 + 3 = 7$ and $7 = 7$.
- **Sample 2:**
  - **Input:** `candidates = [2, 3, 5], target = 8`
  - **Output:** `[[2, 2, 2, 2], [2, 3, 3], [3, 5]]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a cash register coin drawer. You must pay an exact customer bill of `target` dollars using only the denominations available in `candidates`. You can use as many $2 bills as you want, but you cannot use a bill that exceeds the remaining balance.
To prevent counting `{2, 3, 2}` and `{2, 2, 3}` as distinct solutions, you impose an ironclad operational rule: **You may only take bills of equal or higher denomination than the last bill you took.**
Furthermore, if your drawer is neatly sorted from smallest to largest, the very first bill that exceeds the remaining balance tells you that every bill behind it will also exceed the balance! You can immediately slam that drawer section shut (early loop break).

#### 3.2 The Naive Bottleneck & Redundant Computation
Without sorting and index discipline:
1. Exploring choices from index 0 on every step generates permutations (e.g. $[2, 3, 2], [3, 2, 2], [2, 2, 3]$), creating duplicate sets that require expensive set-based deduplication.
2. Continuing to loop through candidates after one has already exceeded `remaining` wastes computation on branches guaranteed to exceed the target.
Pruning via ascending sort drops execution time by orders of magnitude.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Unbounded Progression Invariant (`start = i`):**
- In standard subset generation (0-1 choice), we recurse with `start = i + 1` to forbid reuse.
- In unbounded combination sum, we recurse with `start = i`!
- Passing `i` allows the current candidate to be selected again on the next recursive step, while simultaneously forbidding selection of any candidate at index $< i$.
- **Sorting & Early Loop Break Invariant:**
  $$\text{candidates}[0] < \text{candidates}[1] < \dots < \text{candidates}[N - 1]$$
  If $\text{candidates}[i] > remaining$, then $\forall j \ge i, \; \text{candidates}[j] > remaining$.
  We can `break` immediately, pruning the entire remaining loop!

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
SORTED CANDIDATE PRUNING ARCHITECTURE:
candidates: [ 2 , 3 , 6 , 7 ],  remaining = 4
Loop index i:
i = 0 (val 2): 2 <= 4  ==> Valid branch. Recurse with remaining = 2, start = 0
i = 1 (val 3): 3 <= 4  ==> Valid branch. Recurse with remaining = 1, start = 1
i = 2 (val 6): 6 > 4   ==> VIOLATION!
                           Break loop immediately! (Prunes 6 and 7)
```

- `remaining`: Active deficit remaining to reach target. Decreases with each candidate choice.
- `start`: Lower bound index of candidate selection to avoid backward permutation duplicates.
- `current`: Active combination list.
- **Base Case Invariant:** If `remaining == 0`, a valid combination is sealed; snapshot `new List<int>(current)`.

#### 3.5 State Transition Triggers & Decision Gates
At each call `Backtrack(remaining, start)`:
1. **Target Hit Gate:** If `remaining == 0`, add snapshot of `current` to `result` and return.
2. **Pruning Break Gate:** For $i = start \dots N - 1$:
   - If `candidates[i] > remaining`: `break` immediately!
3. **Choose-Explore-Undo Cycle:**
   - `current.Add(candidates[i])`.
   - `Backtrack(remaining - candidates[i], i)`.
   - `current.RemoveAt(current.Count - 1)`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `candidates = [2, 3, 6, 7], target = 7`.
Sorted: `[2, 3, 6, 7]`.

| Tree Path | `remaining` | Candidate $i$ (`val`) | Condition Check | Action |
| :--- | :---: | :---: | :---: | :--- |
| `[]` | 7 | $i=0$ (2) | $2 \le 7$ | Choose 2 |
| `[2]` | 5 | $i=0$ (2) | $2 \le 5$ | Choose 2 |
| `[2, 2]` | 3 | $i=0$ (2) | $2 \le 3$ | Choose 2 |
| `[2, 2, 2]` | 1 | $i=0$ (2) | $2 > 1$ | **Break!** (Prunes 2, 3, 6, 7) |
| `[2, 2]` | 3 | $i=1$ (3) | $3 \le 3$ | Choose 3 |
| `[2, 2, 3]` | 0 | — | `remaining == 0` | **Target Hit! Add `[2, 2, 3]`** |
| `[2]` | 5 | $i=1$ (3) | $3 \le 5$ | Choose 3 |
| `[2, 3]` | 2 | $i=1$ (3) | $3 > 2$ | **Break!** (Prunes 3, 6, 7) |
| `[]` | 7 | $i=3$ (7) | $7 \le 7$ | Choose 7 |
| `[7]` | 0 | — | `remaining == 0` | **Target Hit! Add `[7]`** |

Final result: `[[2, 2, 3], [7]]`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Sorted Backtracking with Early Break):** Universal gold standard. Sorting upfront enables aggressive branch pruning. Time is $O(N^{T / M})$ where $M = \min(candidates)$, with tree depth $T / M$.
- **Approach 2 (DP Combinations Generation):** Bottom-up DP table where $dp[t]$ stores lists of combinations forming sum $t$. Memory-heavy, causes significant GC allocations.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Sort:** Sort `candidates` ascending (`Array.Sort`).
- **Step 2: Initialize Recursion:** Call `Backtrack(target, 0, current, result)`.
- **Step 3: Condition Gates:** If `remaining == 0` snapshot and return. Loop $i \ge start$: if `candidates[i] > remaining` break.
- **Step 4: Lifecycle:** Add candidate, recurse passing `i`, remove candidate. Return `result`.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Tabulation List Generation:**
  - Allocate array of lists `List<List<int>>[] dp = new List<List<int>>[target + 1]`.
  - For each candidate, sweep capacity forward from `candidate` to `target`.
  - Incurs massive object allocations copying combination lists at every capacity.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Sorted Backtracking with Pruning | Approach 2: DP Combinations Generation |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(N \log N + N^{T / M})$ | $O(T \times N \times \text{combCount})$ |
| **Auxiliary Space** | $O(T / M)$ stack depth | $O(T \times \text{combCount})$ heap objects |
| **Output Space** | $O(\text{Valid Combinations})$ | $O(\text{Valid Combinations})$ |
| **Cache Locality** | High (single path list) | Low (fragmented heap lists) |
| **In-Place Mutability** | In-place sort on input array | Non-destructive |
| **Streaming Suitability** | High | Low |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #95 - Combination Sum
// Core Pattern: Unbounded Choice Backtracking with Ascending-Sort Pruning
// Primary Invariant: Passing start = i permits reuse of candidate[i] while preventing duplicates.
// Pruning Defense: Ascending sort guarantees that when candidate[i] > remaining, all subsequent candidates also exceed target.
// Space Defense: Depth of call stack is bounded by target / min(candidates).
// ============================================================================
```

#### Implementation 1: Sorted Backtracking with Early Break (Production Standard)
```csharp
public class Solution
{
    public IList<IList<int>> CombinationSum(int[] candidates, int target)
    {
        // Guard Clauses
        if (candidates == null || candidates.Length == 0 || target <= 0)
        {
            return new List<IList<int>>();
        }

        var result = new List<IList<int>>();

        // CRITICAL PRUNING STEP: Sort ascending upfront
        // Enables breaking the exploration loop immediately when candidates[i] > remaining
        Array.Sort(candidates);

        var current = new List<int>();
        Backtrack(candidates, target, 0, current, result);
        return result;
    }

    private static void Backtrack(int[] candidates, int remaining, int start, List<int> current, List<IList<int>> result)
    {
        // Target Hit Base Case: Exact sum reached
        if (remaining == 0)
        {
            result.Add(new List<int>(current));
            return;
        }

        for (int i = start; i < candidates.Length; i++)
        {
            int candidate = candidates[i];

            // PRUNING GATE:
            // Since candidates is sorted ascending, if the current candidate exceeds
            // remaining, every subsequent candidate (j > i) will also exceed remaining.
            // Breaking here prunes the entire subtree instantly!
            if (candidate > remaining)
            {
                break;
            }

            // 1. Choose
            current.Add(candidate);

            // 2. Explore: Pass 'i' (NOT 'i + 1') to permit unlimited reuse of candidate[i]
            Backtrack(candidates, remaining - candidate, i, current, result);

            // 3. Undo
            current.RemoveAt(current.Count - 1);
        }
    }
}
```

---

## 96. Letter Combinations of a Phone Number (LeetCode #17)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#backtracking` `#digit-mapping` `#cartesian-product` `#stackalloc` |
| **LeetCode Link** | [Letter Combinations of a Phone Number](https://leetcode.com/problems/letter-combinations-of-a-phone-number/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given a string containing digits from 2-9 inclusive, return all possible letter combinations that the number could represent. Return the answer in any order.
- **Key Constraints:**
  - $0 \le digits.Length \le 4$.
  - `digits[i]` is a digit in the range `['2' - '9']`.
- **Senior Edge Cases to Defend:**
  - Empty string `digits = ""` must return an empty list `[]` (not `[""]`).
  - Maximum output size: $4^4 = 256$ combinations (fits entirely in L1 cache).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Cartesian Product across telephone keypad character sets. Fixed-depth search tree of depth $N = digits.Length$.
- **Sample 1:**
  - **Input:** `digits = "23"`
  - **Output:** `["ad", "ae", "af", "bd", "be", "bf", "cd", "ce", "cf"]`
- **Sample 2:**
  - **Input:** `digits = ""`
  - **Output:** `[]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a combination padlock with $N$ revolving dials. Turning dial 0 cycles through the letters mapped to digit 0 (`'2' -> "abc"`). Turning dial 1 cycles through the letters mapped to digit 1 (`'3' -> "def"`).
Every distinct alignment of the $N$ dials reveals a valid letter combination. The total number of valid configurations is the Cartesian product:
$$\prod_{i=0}^{N-1} |\text{Keypad}(digits[i])|$$
Because the tree depth is strictly bounded by $N \le 4$, we can eliminate all dynamic heap memory churn by writing characters directly into a stack-allocated buffer (`stackalloc char[N]`).

#### 3.2 The Naive Bottleneck & Redundant Computation
Naive recursive string concatenation generates immutable string instances at every intermediate edge:
```text
level 0: "a"
level 1: "a" + "d" = "ad"
```
For deep trees or larger alphabets, creating throwaway strings generates heavy Garbage Collection (GC) pressure. Using an in-place mutable character buffer (or `Span<char>`) completely eliminates intermediate string churn.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Fixed-Depth Stack-Allocated Buffer Invariant:**
- Depth of recursion is fixed to $N = digits.Length$.
- We allocate a continuous span of memory on the thread stack:
  $$\text{Span}<\text{char}> current = \text{stackalloc char}[digits.Length]$$
- At recursive depth `index`, we simply assign `current[index] = letter`.
- When `index == digits.Length`, we materialize the finalized string once: `new string(current)`.
- **Zero intermediate heap memory allocations during traversal!**

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
STACK-ALLOCATED SPAN TRAVERSAL:
digits = "23"
Span<char> current (on thread stack):
[ index 0: 'a' ] [ index 1: 'd' ]
        ^               ^
   Level 0 ('2')    Level 1 ('3')

At index == digits.Length:
Add new string(current) to result list
```

- `index`: The active digit position being resolved ($0 \le index \le digits.Length$).
- `current`: A mutable `Span<char>` of fixed size $N$ residing strictly on the execution stack frame.
- `Keypad`: Constant mapping lookup table indexed by digit value.

#### 3.5 State Transition Triggers & Decision Gates
At each call `Backtrack(digits, index, current, result)`:
1. **Base Case Gate:** If `index == digits.Length`, add `new string(current)` to `result` and return.
2. **Keypad Lookup:** Extract letters string for `digits[index] - '0'`.
3. **Character Iteration:** For each `char c` in mapped letters:
   - `current[index] = c`.
   - Recurse `Backtrack(digits, index + 1, current, result)`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `digits = "23"`.
`Keypad[2] = "abc"`, `Keypad[3] = "def"`.
Stackalloc buffer `current = ['_', '_']`.

| Depth `index` | Digit | Letter Selected | `current` State | Result Addition |
| :---: | :---: | :---: | :---: | :--- |
| **0** | '2' | 'a' | `['a', '_']` | — |
| **1** | '3' | 'd' | `['a', 'd']` | Add `"ad"` |
| **1** | '3' | 'e' | `['a', 'e']` | Add `"ae"` |
| **1** | '3' | 'f' | `['a', 'f']` | Add `"af"` |
| **0** | '2' | 'b' | `['b', '_']` | — |
| **1** | '3' | 'd' | `['b', 'd']` | Add `"bd"` |
| **1** | '3' | 'e' | `['b', 'e']` | Add `"be"` |
| **1** | '3' | 'f' | `['b', 'f']` | Add `"bf"` |
| **0** | '2' | 'c' | `['c', '_']` | — |
| **1** | '3' | 'd' | `['c', 'd']` | Add `"cd"` |
| **1** | '3' | 'e' | `['c', 'e']` | Add `"ce"` |
| **1** | '3' | 'f' | `['c', 'f']` | Add `"cf"` |

Total output: 9 combinations.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Stackalloc Span DFS):** Peak performance in .NET. Zero heap garbage during backtracking; allocates only the final result strings.
- **Approach 2 (Iterative BFS Queue):** Breadth-first level-by-level generation. Generates combinations iteratively, but incurs heavy object allocation overhead in intermediate queues.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Guard Clause:** If `digits` is empty, return empty list immediately.
- **Step 2: Stackalloc Buffer:** Allocate `Span<char> current = stackalloc char[digits.Length]`.
- **Step 3: Recursive Cartesian DFS:** Traverse digits from index 0 to $N$.
- **Step 4: Materialization:** Instantiate final string upon reaching base depth $N$. Return result.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Iterative BFS (Queue Extension):**
  - Initialize queue with `[""]`.
  - For each digit, dequeue all existing strings, append each mapped letter, and enqueue the new strings.
  - Generates $O(4^N)$ intermediate string allocations.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Stackalloc Span DFS (Optimal) | Approach 2: Iterative BFS Queue |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(4^N \cdot N)$ | $O(4^N \cdot N)$ |
| **Auxiliary Space** | $O(N)$ thread stack frame | $O(4^N \cdot N)$ heap queue storage |
| **Output Space** | $O(4^N \cdot N)$ final strings | $O(4^N \cdot N)$ final strings |
| **Cache Locality** | Pure L1 stack memory | Low (garbage collected queue nodes) |
| **In-Place Mutability** | In-place stack write | Immutable string churn |
| **Streaming Suitability** | High | Low |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #96 - Letter Combinations of a Phone Number
// Core Pattern: Cartesian Product DFS / Stackalloc In-Place Span
// Primary Invariant: Fixed depth N = digits.Length maps to fixed stackalloc buffer.
// Garbage Defense: Span<char> on stack frame eliminates intermediate string allocations.
// Boundary Defense: Empty input returns empty collection (not a list containing "").
// ============================================================================
```

#### Implementation 1: Backtracking with Stackalloc Span (Production Optimal)
```csharp
public class Solution
{
    // Constant telephone keypad mapping table
    private static readonly string[] Keypad = {
        "",     // 0
        "",     // 1
        "abc",  // 2
        "def",  // 3
        "ghi",  // 4
        "jkl",  // 5
        "mno",  // 6
        "pqrs", // 7
        "tuv",  // 8
        "wxyz"  // 9
    };

    public IList<string> LetterCombinations(string digits)
    {
        // Guard Clause: Empty string returns empty list
        if (string.IsNullOrEmpty(digits))
        {
            return new List<string>();
        }

        // Calculate expected output capacity to prevent list resizing
        int capacity = 1;
        foreach (char d in digits)
        {
            capacity *= Keypad[d - '0'].Length;
        }

        var result = new List<string>(capacity: capacity);

        // STACKALLOC OPTIMIZATION:
        // Allocate buffer directly on the execution stack frame.
        // Zero GC heap allocations are incurred during tree traversal!
        Span<char> current = stackalloc char[digits.Length];

        Backtrack(digits, 0, current, result);
        return result;
    }

    private static void Backtrack(string digits, int index, Span<char> current, List<string> result)
    {
        // Base Case: All digits assigned; materialize string from stack buffer
        if (index == digits.Length)
        {
            result.Add(new string(current));
            return;
        }

        string letters = Keypad[digits[index] - '0'];

        for (int i = 0; i < letters.Length; i++)
        {
            // Overwrite position in stack buffer in-place
            current[index] = letters[i];

            // Explore next digit
            Backtrack(digits, index + 1, current, result);
        }
    }
}
```

#### Implementation 2: Iterative BFS Queue
```csharp
public class SolutionBfs
{
    private static readonly string[] Keypad = {
        "", "", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz"
    };

    public IList<string> LetterCombinations(string digits)
    {
        if (string.IsNullOrEmpty(digits)) return new List<string>();

        var queue = new Queue<string>();
        queue.Enqueue(string.Empty);

        foreach (char d in digits)
        {
            string letters = Keypad[d - '0'];
            int levelSize = queue.Count;

            for (int i = 0; i < levelSize; i++)
            {
                string prefix = queue.Dequeue();
                foreach (char c in letters)
                {
                    queue.Enqueue(prefix + c);
                }
            }
        }

        return queue.ToList();
    }
}
```

---

## 97. Word Search (LeetCode #79)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#backtracking` `#grid-dfs` `#in-place-masking` |
| **LeetCode Link** | [Word Search](https://leetcode.com/problems/word-search/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an $m \times n$ grid of characters `board` and a string `word`, return `true` if `word` exists in the grid. The word can be constructed from sequentially adjacent cells (horizontal or vertical). The same cell may not be used more than once in a path.
- **Key Constraints:**
  - $m == board.Length, n == board[i].Length \in [1, 6]$.
  - $1 \le word.Length \le 15$.
  - `board` and `word` consist of only lowercase and uppercase English letters.
- **Senior Edge Cases to Defend:**
  - Board character frequency deficit: If `word` requires 5 'A's and board only has 4, return `false` in $O(M \times N)$ before launching DFS.
  - Directional pruning: If `word[0]` occurs 50 times in `board` but `word[^1]` occurs only once, searching `word` in reverse prunes the search tree by $50\times$!

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** 2D Grid DFS Backtracking: At each matching letter, temporarily mask the cell `board[r][c] = '#'`, explore 4 directions, and restore the original character during the undo phase.
- **Sample 1:**
  - **Input:** `board = [["A","B","C","E"],["S","F","C","S"],["A","D","E","E"]], word = "ABCCED"`
  - **Output:** `true`
- **Sample 2:**
  - **Input:** `board = [["A","B","C","E"],["S","F","C","S"],["A","D","E","E"]], word = "ABCB"`
  - **Output:** `false`
  - **Explanation:** Cannot revisit cell $(0, 1)$ ('B') along the same path.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine crawling through a pitch-black labyrinth guided by a spellbook. Each room has an inscribed letter. To cast the spell, you must walk a continuous trail whose room inscriptions spell out `word`.
As you step into a room matching your next needed letter, you drop a glowing breadcrumb on the floor (`board[r][c] = '#'`). This breadcrumb prevents you from wandering into your own footsteps and forming an illegal loop.
If all 4 doorways leading out of the room fail to complete the spell, you pick up your breadcrumb (restoring `board[r][c] = originalLetter`) and retreat to the previous chamber. This in-place breadcrumb mechanism gives $O(1)$ auxiliary space without needing a separate visited matrix!

#### 3.2 The Naive Bottleneck & Redundant Computation
Without in-place masking, a naive DFS allocates an $M \times N$ boolean `visited` matrix on every path, causing severe GC thrashing.
Furthermore, without **frequency-based pre-filtering**, a board filled with `'A'`s searching for `"AAAAAB"` will launch thousands of futile DFS paths from every single `'A'`, only to fail on `'B'`. Checking total character counts upfront prunes impossible searches in $O(M \times N)$ time.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**In-Place Masking & Restitution Invariant:**
- Mutate `board[r][c] = '#'` before exploring adjacent neighbors.
- Explore 4 orthogonal directions: $(r + 1, c), (r - 1, c), (r, c + 1), (r, c - 1)$.
- Restore `board[r][c] = originalChar` before returning from the function call.
- **Search Inversion Optimization:**
  Count frequencies of `word[0]` and `word[word.Length - 1]` in `board`. If `word[^1]` has fewer occurrences than `word[0]`, reverse `word`! Starting the DFS from the rarer character dramatically reduces the branching factor of the decision tree.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
2D GRID IN-PLACE MASKING:
       (r - 1, c)
           ▲
(r, c-1) ◄ [ # ] ► (r, c+1)
           ▼
       (r + 1, c)

Active cell (r, c) masked with '#'
4 orthogonal recursive exploration probes
Restored to original char on backtrack unwind
```

- `r, c`: Grid row and column coordinates.
- `charIdx`: The target character position in `word` currently being matched ($0 \le charIdx \le word.Length$).
- **Invariant:** During exploration of cell $(r, c)$, `board[r][c]` equals `'#'`, preventing any descendant call from reusing it. Upon return, `board[r][c]` is unconditionally restored.

#### 3.5 State Transition Triggers & Decision Gates
At each call `Dfs(r, c, charIdx)`:
1. **Target Completed Gate:** If `charIdx == word.Length`, return `true`.
2. **Boundary & Match Gate:** If $r, c$ out of bounds OR `board[r][c] != word[charIdx]`, return `false`.
3. **Choose Gate:** Save `temp = board[r][c]`; set `board[r][c] = '#'`.
4. **Explore Gate:** Check if any of 4 directions return `true`:
   $$found = Dfs(r+1, c) \lor Dfs(r-1, c) \lor Dfs(r, c+1) \lor Dfs(r, c-1)$$
5. **Undo Gate:** Restore `board[r][c] = temp`.
6. **Return Gate:** Return `found`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `board = [["A","B"],["C","D"]], word = "ABDC"`.

| Step | Cell $(r, c)$ | Expected Char | Actual Char | Action | Board State |
| :---: | :---: | :---: | :---: | :--- | :--- |
| **1** | $(0, 0)$ | `'A'` ($charIdx = 0$) | `'A'` | Match! Mask with `'#'` | `[['#','B'],['C','D']]` |
| **2** | $(0, 1)$ | `'B'` ($charIdx = 1$) | `'B'` | Match! Mask with `'#'` | `[['#','#'],['C','D']]` |
| **3** | $(1, 1)$ | `'D'` ($charIdx = 2$) | `'D'` | Match! Mask with `'#'` | `[['#','#'],['C','#']]` |
| **4** | $(1, 0)$ | `'C'` ($charIdx = 3$) | `'C'` | Match! Mask with `'#'` | `[['#','#'],['#','#']]` |
| **5** | — | $charIdx = 4 == Length$ | — | **Word Complete! Return True** | — |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (In-Place Masking with Frequency Pruning):** Senior industry standard. Zero auxiliary matrix allocation, aggressive frequency validation, and optional search direction inversion.
- **Approach 2 (Visited Boolean Matrix):** Use when the input `board` is marked read-only in memory or across multithreaded readers where concurrent mutation is forbidden.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Character Frequency Defense:** Tally board characters. If any character in `word` is deficient, return `false`.
- **Step 2: Direction Optimization:** If `word[^1]` is rarer than `word[0]`, reverse the target word.
- **Step 3: Outer Origin Scan:** Scan cells $(r, c)$. If `board[r][c] == word[0]`, launch DFS.
- **Step 4: In-Place Backtracking:** Mask cell, probe 4 neighbors, restore cell, return result.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Boolean Visited Matrix:**
  - Allocate `bool[,] visited = new bool[m, n]`.
  - Mark `visited[r, c] = true` on enter, and `false` on exit.
  - Safe for concurrent read-only memory, but incurs $O(M \times N)$ auxiliary heap allocation.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: In-Place Masking DFS | Approach 2: Visited Boolean Matrix |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(M \times N)$ best / $O(M \times N \times 3^L)$ worst | $O(M \times N \times 3^L)$ |
| **Auxiliary Space** | $O(L)$ stack depth ($L = word.Length$) | $O(M \times N)$ visited matrix + $O(L)$ stack |
| **Output Space** | $O(1)$ boolean | $O(1)$ boolean |
| **Cache Locality** | Optimal (modifies contiguous grid) | Moderate |
| **In-Place Mutability** | Temporary in-place mutation (restored) | Non-destructive |
| **Streaming Suitability** | Low | Low |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #97 - Word Search
// Core Pattern: 2D Grid Backtracking / In-Place Breadcrumb Masking
// Primary Invariant: board[r][c] temporarily set to '#' to prevent self-intersection; restored on backtrack.
// Pre-filtering Defense: Frequency count check rejects impossible words in O(M x N) before DFS.
// Branching Defense: Reverses word if trailing character is rarer than leading character.
// ============================================================================
```

#### Implementation 1: In-Place Masking with Frequency Pruning (Production Optimal)
```csharp
public class Solution
{
    public bool Exist(char[][] board, string word)
    {
        // Guard Clauses
        if (board == null || board.Length == 0 || board[0].Length == 0 || string.IsNullOrEmpty(word))
        {
            return false;
        }

        int rows = board.Length;
        int cols = board[0].Length;

        if (word.Length > rows * cols)
        {
            return false;
        }

        // PRE-FILTERING DEFENSE 1: Character frequency audit
        // Verify board contains sufficient frequency of every character required by word
        var boardCounts = new Dictionary<char, int>();
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                char ch = board[r][c];
                boardCounts[ch] = boardCounts.GetValueOrDefault(ch) + 1;
            }
        }

        foreach (char ch in word)
        {
            if (!boardCounts.TryGetValue(ch, out int count) || count == 0)
            {
                return false; // Impossible to form word; abort before launching DFS
            }
            boardCounts[ch] = count - 1;
        }

        // PRE-FILTERING DEFENSE 2: Search direction inversion
        // If word's last character is rarer in board than first character, reverse search direction
        // This shrinks the branching factor of the DFS search tree dramatically
        char firstChar = word[0];
        char lastChar = word[word.Length - 1];
        if (boardCounts.GetValueOrDefault(lastChar) < boardCounts.GetValueOrDefault(firstChar))
        {
            char[] arr = word.ToCharArray();
            Array.Reverse(arr);
            word = new string(arr);
        }

        // Launch DFS from matching candidate starting cells
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (board[r][c] == word[0] && Dfs(board, r, c, word, 0))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool Dfs(char[][] board, int r, int c, string word, int charIdx)
    {
        // Success Base Case: All characters matched
        if (charIdx == word.Length)
        {
            return true;
        }

        // Boundary & Character Match Gate
        if (r < 0 || r >= board.Length || c < 0 || c >= board[0].Length || board[r][c] != word[charIdx])
        {
            return false;
        }

        // 1. Choose: Mask cell in-place to prevent revisit along current path
        char originalChar = board[r][c];
        board[r][c] = '#';

        // 2. Explore: 4-directional orthogonal traversal
        // Short-circuit evaluation ensures remaining branches are skipped once a path succeeds
        bool found = Dfs(board, r + 1, c, word, charIdx + 1) ||
                     Dfs(board, r - 1, c, word, charIdx + 1) ||
                     Dfs(board, r, c + 1, word, charIdx + 1) ||
                     Dfs(board, r, c - 1, word, charIdx + 1);

        // 3. Undo: Unconditionally restore original character before returning
        board[r][c] = originalChar;

        return found;
    }
}
```

#### Implementation 2: Read-Only Board with Visited Bitmask / Array
```csharp
public class SolutionReadOnly
{
    public bool Exist(char[][] board, string word)
    {
        if (board == null || board.Length == 0 || string.IsNullOrEmpty(word)) return false;

        int m = board.Length;
        int n = board[0].Length;
        bool[,] visited = new bool[m, n];

        for (int r = 0; r < m; r++)
        {
            for (int c = 0; c < n; c++)
            {
                if (board[r][c] == word[0] && Dfs(board, r, c, word, 0, visited))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool Dfs(char[][] board, int r, int c, string word, int charIdx, bool[,] visited)
    {
        if (charIdx == word.Length) return true;

        if (r < 0 || r >= board.Length || c < 0 || c >= board[0].Length ||
            visited[r, c] || board[r][c] != word[charIdx])
        {
            return false;
        }

        visited[r, c] = true;

        bool found = Dfs(board, r + 1, c, word, charIdx + 1, visited) ||
                     Dfs(board, r - 1, c, word, charIdx + 1, visited) ||
                     Dfs(board, r, c + 1, word, charIdx + 1, visited) ||
                     Dfs(board, r, c - 1, word, charIdx + 1, visited);

        visited[r, c] = false;
        return found;
    }
}
```

---

## 98. N-Queens (LeetCode #51)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | 💡 Advanced |
| **Pattern Tags** | `#backtracking` `#diagonal-bitsets` `#constraint-propagation` |
| **LeetCode Link** | [N-Queens](https://leetcode.com/problems/n-queens/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** The $n$-queens puzzle is the problem of placing $n$ queens on an $n \times n$ chessboard such that no two queens attack each other. Given an integer $n$, return all distinct solutions to the $n$-queens puzzle.
- **Key Constraints:**
  - $1 \le n \le 9$.
- **Senior Edge Cases to Defend:**
  - $n = 1 \implies [["Q"]]$.
  - $n = 2, 3 \implies []$ (no valid configuration exists where queens cannot attack).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Row-by-Row Constraint Satisfaction Problem (CSP): Exactly one queen per row. Detect collisions in $O(1)$ time across columns, main diagonals ($r - c$), and anti-diagonals ($r + c$) using boolean arrays or integer bitmasks.
- **Sample 1:**
  - **Input:** `n = 4`
  - **Output:** `[[".Q..","...Q","Q...","..Q."],["..Q.","Q...","...Q",".Q.."]]`
  - **Explanation:** 2 distinct valid board configurations.
- **Sample 2:**
  - **Input:** `n = 1`
  - **Output:** `[["Q"]]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a royal banquet table with $N$ rows and $N$ seats per row. You must seat $N$ rival queens such that no two queens share the same row, column, or diagonal line of sight.
Because queens threaten along rows, **no two queens can occupy the same row**. Since there are $N$ queens and $N$ rows, **every row must contain exactly one queen**.
This transforms the problem from placing $N$ pieces on $N^2$ squares into a row-by-row decision pipeline:
At row $r$, choose an unoccupied column $c$ that does not intersect with any previously placed queen's column or diagonal beams.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute force placement of $N$ queens across $N^2$ squares checks $\binom{N^2}{N}$ boards. For $N = 8$, $\binom{64}{8} = 4,426,165,368$ configurations.
Even row-by-row placement with an $O(N)$ board scan to verify safety evaluates $N^N = 8^8 \approx 1.67 \times 10^7$ calls.
By maintaining $O(1)$ collision lookup tables (or bitmasks) for the column and both diagonal axes, we prune invalid paths immediately, evaluating only $N!$ states with zero wasted checks.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Diagonal Index Invariants:**
1. **Column Collision:** Monitored directly via `col`.
2. **Anti-Diagonal ($/$) Collision:** Along any anti-diagonal line, the sum of coordinates $r + c$ is strictly constant! Range is $[0 \dots 2n - 2]$.
3. **Main Diagonal ($\backslash$) Collision:** Along any main diagonal line, the difference of coordinates $r - c$ is strictly constant! To prevent negative array indexing, shift by $n - 1$:
   $$\text{diag2Index} = r - c + (n - 1) \in [0 \dots 2n - 2]$$

**Bitwise Acceleration Invariant:**
We can represent columns, anti-diagonals, and main diagonals as integer bitmasks:
- `cols`: Bit $c$ indicates column $c$ is threatened.
- `diag1`: Bit $(r + c)$ indicates anti-diagonal is threatened. Shifted left by 1 when advancing row ($r \to r + 1$).
- `diag2`: Bit $(r - c)$ indicates main diagonal is threatened. Shifted right by 1 when advancing row ($r \to r + 1$).
- Available columns in row $r$ are computed in a single bitwise CPU operation:
  $$\text{available} = ((1 \ll n) - 1) \ \& \ \sim(\text{cols} \mid \text{diag1} \mid \text{diag2})$$
- Extract candidate column via lowest set bit: `bit = available & -available`.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
DIAGONAL GEOMETRY & BITWISE CONSTRAINT PROPAGATION:
Anti-Diagonal (/) : r + c = const    Main Diagonal (\) : r - c = const
   0   1   2   3                         0   1   2   3
0 [0] [1] [2] [3]                     0 [3] [2] [1] [0]
1 [1] [2] [3] [4]                     1 [4] [3] [2] [1]
2 [2] [3] [4] [5]                     2 [5] [4] [3] [2]
3 [3] [4] [5] [6]                     3 [6] [5] [4] [3]

Bitwise Union: threatened = (cols | diag1 | diag2)
Available:     available  = ((1 << n) - 1) & ~threatened
```

- `row`: Active row cursor advancing from $0$ to $n - 1$.
- `queens[row]`: Stores the column assigned to the queen at row `row`.
- `cols, diag1, diag2`: Tracking structures (boolean arrays or bitmasks) enforcing the $O(1)$ collision checks.

#### 3.5 State Transition Triggers & Decision Gates
At row `row`:
1. **Base Case Gate:** If `row == n`, all $n$ queens placed safely! Format board and add to `result`.
2. **Column Candidate Scan:** For each `col` from $0$ to $n - 1$:
   - **Conflict Gate:** Check `cols[col] || diag1[row + col] || diag2[row - col + n - 1]`. If true, skip.
   - **Choose Gate:** Set `queens[row] = col`; mark `cols, diag1, diag2` as true.
   - **Explore Gate:** Recurse `Backtrack(row + 1)`.
   - **Undo Gate:** Unmark `cols, diag1, diag2` as false.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: $n = 4$.

| Row | Valid Cols Evaluated | Collision Checks | Action Taken | Board State (`queens`) |
| :---: | :---: | :---: | :--- | :--- |
| **0** | $col = 0$ | Clear | Place at $(0, 0)$ | `[0, _, _, _]` |
| **1** | $col = 0$ (col), $1$ (diag) | Collisions | Skip 0, 1 | |
| **1** | $col = 2$ | Clear | Place at $(1, 2)$ | `[0, 2, _, _]` |
| **2** | $col = 0, 1, 2, 3$ | All collide! | Backtrack to row 1 | |
| **1** | $col = 3$ | Clear | Place at $(1, 3)$ | `[0, 3, _, _]` |
| **2** | $col = 1$ | Clear | Place at $(2, 1)$ | `[0, 3, 1, _]` |
| **3** | $col = 0, 1, 2, 3$ | All collide! | Backtrack to row 0 | |
| **0** | $col = 1$ | Clear | Place at $(0, 1)$ | `[1, _, _, _]` |
| **1** | $col = 3$ | Clear | Place at $(1, 3)$ | `[1, 3, _, _]` |
| **2** | $col = 0$ | Clear | Place at $(2, 0)$ | `[1, 3, 0, _]` |
| **3** | $col = 2$ | Clear | **Place at $(3, 2)$! Solution Found!** | `[1, 3, 0, 2]` |

Formatted Solution 1: `[".Q..", "...Q", "Q...", "..Q."]`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Boolean Constraint Arrays):** Recommended for senior interviews. Extremely readable, trivial to explain, avoids bit manipulation edge cases, and executes with optimal $O(1)$ check time.
- **Approach 2 (Bitwise Integer Masks):** World-class performance optimization. Replaces arrays with 3 integers (`cols`, `diag1`, `diag2`), utilizing CPU bitwise shifts and trailing zero counts to run $5\times$ faster.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Allocations:** Allocate boolean arrays `cols[n]`, `diag1[2n]`, `diag2[2n]`, and integer array `queens[n]`.
- **Step 2: Recursive Backtracking:** Call `Backtrack(0)`.
- **Step 3: Condition Check & Gates:** Test collision in $O(1)$. If safe, choose, explore `row + 1`, undo.
- **Step 4: Board Formatting:** When `row == n`, convert `queens` array into list of strings. Return `result`.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Bitwise Shifted Masks:**
  - At row `row`, `available = ((1 << n) - 1) & ~(cols | diag1 | diag2)`.
  - While `available != 0`: extract `bit = available & -available`, find `col = int.TrailingZeroCount(bit)`.
  - Recurse passing `cols | bit`, `(diag1 | bit) << 1`, `(diag2 | bit) >> 1`.
  - Automatically shifts diagonals without needing array index calculations!

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Boolean Constraint Arrays | Approach 2: Bitwise Integer Masks |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(N!)$ | $O(N!)$ (5x lower constant factor) |
| **Auxiliary Space** | $O(N)$ tracking arrays + $O(N)$ stack | $O(N)$ stack depth only |
| **Output Space** | $O(N^2 \cdot S)$ where $S$ = solutions | $O(N^2 \cdot S)$ where $S$ = solutions |
| **Cache Locality** | High | Extreme (pure register execution) |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | High | High |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #98 - N-Queens
// Core Pattern: Backtracking Constraint Satisfaction / O(1) Diagonal Lookup
// Primary Invariants: col fixed, anti-diag (r + c) constant, main-diag (r - c + n - 1) constant.
// Collision Defense: Three boolean arrays eliminate O(N) diagonal validation loops.
// Performance Variant: Bitwise integer masks evaluate available squares in a single CPU instruction.
// ============================================================================
```

#### Implementation 1: Boolean Constraint Arrays (Production Standard)
```csharp
public class Solution
{
    public IList<IList<string>> SolveNQueens(int n)
    {
        // Guard Clause
        if (n <= 0)
        {
            return new List<IList<string>>();
        }

        var result = new List<IList<string>>();

        // O(1) Collision Tracking Arrays:
        // cols tracks occupied columns: index c in [0 .. n - 1]
        bool[] cols = new bool[n];
        // diag1 tracks anti-diagonals (/): index (r + c) in [0 .. 2n - 2]
        bool[] diag1 = new bool[2 * n];
        // diag2 tracks main diagonals (\): index (r - c + n - 1) in [0 .. 2n - 2]
        bool[] diag2 = new bool[2 * n];

        // queens[r] records the column position of the queen in row r
        int[] queens = new int[n];

        Backtrack(0, n, cols, diag1, diag2, queens, result);
        return result;
    }

    private static void Backtrack(int row, int n, bool[] cols, bool[] diag1, bool[] diag2, int[] queens, List<IList<string>> result)
    {
        // Base Case: All n queens successfully placed
        if (row == n)
        {
            result.Add(FormatBoard(queens, n));
            return;
        }

        for (int col = 0; col < n; col++)
        {
            int d1 = row + col;
            int d2 = row - col + n - 1;

            // INVARIANT GATE: Check O(1) collision on column and both diagonals
            if (cols[col] || diag1[d1] || diag2[d2])
            {
                continue;
            }

            // 1. Choose: Place queen and activate attack beams
            queens[row] = col;
            cols[col] = true;
            diag1[d1] = true;
            diag2[d2] = true;

            // 2. Explore: Advance to place queen on next row
            Backtrack(row + 1, n, cols, diag1, diag2, queens, result);

            // 3. Undo: Remove queen and deactivate attack beams
            cols[col] = false;
            diag1[d1] = false;
            diag2[d2] = false;
        }
    }

    private static IList<string> FormatBoard(int[] queens, int n)
    {
        var board = new List<string>(capacity: n);

        for (int r = 0; r < n; r++)
        {
            char[] rowChars = new char[n];
            Array.Fill(rowChars, '.');
            rowChars[queens[r]] = 'Q';
            board.Add(new string(rowChars));
        }

        return board;
    }
}
```

#### Implementation 2: Bitwise Integer Masks (Ultra-Fast Hardware Optimization)
```csharp
public class SolutionBitwise
{
    public IList<IList<string>> SolveNQueens(int n)
    {
        if (n <= 0) return new List<IList<string>>();

        var result = new List<IList<string>>();
        int[] queens = new int[n];

        Backtrack(0, n, 0, 0, 0, queens, result);
        return result;
    }

    private static void Backtrack(int row, int n, int cols, int diag1, int diag2, int[] queens, List<IList<string>> result)
    {
        if (row == n)
        {
            var board = new List<string>(n);
            for (int r = 0; r < n; r++)
            {
                char[] rowChars = new char[n];
                Array.Fill(rowChars, '.');
                rowChars[queens[r]] = 'Q';
                board.Add(new string(rowChars));
            }
            result.Add(board);
            return;
        }

        // Available columns are represented by 1-bits in 'available'
        // (cols | diag1 | diag2) contains 1-bits for all threatened columns
        int available = ((1 << n) - 1) & ~(cols | diag1 | diag2);

        while (available != 0)
        {
            // Extract the lowest set bit using two's complement identity
            int bit = available & -available;
            int col = System.Numerics.BitOperations.TrailingZeroCount(bit);

            queens[row] = col;

            // Recurse to next row:
            // Shift diag1 left by 1 (anti-diagonal moves left as row increases)
            // Shift diag2 right by 1 (main diagonal moves right as row increases)
            Backtrack(row + 1, n, cols | bit, (diag1 | bit) << 1, (diag2 | bit) >> 1, queens, result);

            // Clear the lowest set bit to explore the next available column
            available &= available - 1;
        }
    }
}
```

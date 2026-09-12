# Phase 08: Binary Search

> **Focus:** Search Space Invariants, Boundary Searches (Lower/Upper Bound), Rotated Sorted Partitions, Monotonic Feasibility Predicates (Search on Answer Space), and Virtual 2D Coordinate Mapping.  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 8 (Problems #41–#47)

---

## 41. Binary Search (LeetCode #704)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#binary-search` `#search-template` `#overflow-prevention` |
| **LeetCode Link** | [Binary Search](https://leetcode.com/problems/binary-search/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array of integers `nums` which is sorted in ascending order, and an integer `target`, write a function to search `target` in `nums`. If `target` exists, then return its index. Otherwise, return `-1`. You must write an algorithm with $O(\log n)$ runtime complexity.
- **Key Constraints:**
  - `1 <= nums.Length <= 10^4`
  - `-10^4 < nums[i], target < 10^4`
  - All integers in `nums` are unique.
  - `nums` is sorted in ascending order.
- **Senior Arithmetic Edge Case:** Never calculate midpoint as `(left + right) / 2` because their sum can overflow a 32-bit signed integer when $left + right > 2^{31} - 1$. Always use:
  $$mid = left + \frac{right - left}{2}$$

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Halve the active candidate search space on every iteration by comparing the midpoint against the target value.
- **Sample 1:**
  - **Input:** `nums = [-1, 0, 3, 5, 9, 12]`, `target = 9`
  - **Output:** `4` (Explanation: 9 exists in nums and its index is 4)
- **Sample 2:**
  - **Input:** `nums = [-1, 0, 3, 5, 9, 12]`, `target = 2`
  - **Output:** `-1` (Explanation: 2 does not exist in nums so return -1)

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Picture a massive physical dictionary. When searching for "Newton", you do not read every word starting from page 1 ("Aardvark"). Instead, you open the book to the exact middle. If you see "Miller", you immediately know that "Newton" must appear in the second half of the book because the pages are strictly sorted alphabetically. You tear the first half of the book away in your mind and repeat the process on the remainder. Each split divides the remaining volume in half: $N \to N/2 \to N/4 \dots \to 1$ in approximately $\log_2 N$ steps.

#### 3.2 The Naive Bottleneck & Redundant Computation
A linear search scans sequentially from index $0$ to $N - 1$. While trivial to implement, it performs $O(N)$ comparisons. On $N = 10^9$ elements (common in large-scale database indexes), linear search requires up to $10^9$ operations ($\approx 1$ second on modern CPUs). Binary search finds the target in at most $\lceil \log_2 10^9 \rceil \approx 30$ comparisons ($< 1$ microsecond), an efficiency gain of seven orders of magnitude.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Monotonic Candidate Elimination Invariant:**
Because `nums` is strictly monotonically ascending:
$$\forall i < j, \quad nums[i] < nums[j]$$
Let our candidate window be $[left, right]$.
- If $nums[mid] < target$:
  Because the array is sorted, every element at index $k \le mid$ satisfies $nums[k] \le nums[mid] < target$. Therefore, no element in $[left, mid]$ can equal `target`. We safely eliminate the entire left half:
  $$left = mid + 1$$
- If $nums[mid] > target$:
  Every element at index $k \ge mid$ satisfies $nums[k] \ge nums[mid] > target$. No element in $[mid, right]$ can equal `target`. We safely eliminate the entire right half:
  $$right = mid - 1$$
- If $nums[mid] == target$:
  Target is found; return `mid` immediately.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
SEARCH SPACE PARTITION ARCHITECTURE:

[ Eliminated: nums[k] < target ]   [ Active Candidate Window ]   [ Eliminated: nums[k] > target ]
0 ..................... left - 1   left .............. right   right + 1 ................ N - 1
└──────────────┬───────────────┘   └────────────┬────────────┘   └──────────────┬───────────────┘
   Guaranteed < target                Contains target if present        Guaranteed > target

TRANSITION:
mid = left + (right - left) / 2
If nums[mid] < target ==> left = mid + 1
If nums[mid] > target ==> right = mid - 1
If left > right       ==> Window is empty; target does not exist.
```

- `left`: Lower bound of the active candidate window (0-indexed).
- `right`: Upper bound of the active candidate window (0-indexed).
- `mid`: Overflow-safe midpoint index.

#### 3.5 State Transition Triggers & Decision Gates
Loop while $left \le right$:
1. `mid = left + (right - left) / 2`
2. **Match Gate:** If $nums[mid] == target \implies$ return $mid$.
3. **Left-Shrink Gate:** Else if $nums[mid] < target \implies left = mid + 1$.
4. **Right-Shrink Gate:** Else $right = mid - 1$.
5. **Exhaustion Gate:** If $left > right$, candidate space is empty $\implies$ return `-1`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `nums = [-1, 0, 3, 5, 9, 12]`, `target = 9`

| Iteration | `left` | `right` | `mid` Calculation | `nums[mid]` | Comparison vs Target (`9`) | Next Action |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Iter 1** | 0 | 5 | $0 + (5 - 0) / 2 = 2$ | 3 | $3 < 9$ | Target in right half $\implies left = 3$ |
| **Iter 2** | 3 | 5 | $3 + (5 - 3) / 2 = 4$ | 9 | $9 == 9$ | **Target matched! Return `mid = 4`** |

Trace input: `nums = [-1, 0, 3, 5, 9, 12]`, `target = 2`

| Iteration | `left` | `right` | `mid` | `nums[mid]` | Comparison vs Target (`2`) | Next Action |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Iter 1** | 0 | 5 | 2 | 3 | $3 > 2$ | Target in left half $\implies right = 1$ |
| **Iter 2** | 0 | 1 | 0 | -1 | $-1 < 2$ | Target in right half $\implies left = 1$ |
| **Iter 3** | 1 | 1 | 1 | 0 | $0 < 2$ | Target in right half $\implies left = 2$ |
| **End** | 2 | 1 | — | — | $left > right$ | Loop terminates $\implies$ **Return `-1`** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Iterative Standard Inclusive Window):** Optimal $O(1)$ auxiliary space. Direct instruction pipelining, no call-stack overhead, resistant to recursion depth limitations.
- **Approach 2 (Tail-Recursive Binary Search):** Mathematically expressive. Uses $O(\log N)$ call-stack space. Useful in functional programming languages with guaranteed Tail Call Optimization (TCO), but avoided in C# where TCO is not guaranteed by the JIT compiler across all platforms.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Initialize `left = 0`, `right = nums.Length - 1`.
- **Step 2: Main Exploration Loop:** While `left <= right`.
- **Step 3: Midpoint & Condition Gates:** Calculate overflow-safe `mid`. Branch on equality, smaller, or greater.
- **Step 4: Exhaustion:** Return `-1` if loop terminates without match.

#### 4.3 Alternative Approaches Analysis
- **Left-Closed, Right-Open Window (`[left, right)`):**
  Uses `while (left < right)` and `right = mid`. Popular in C++ STL (`std::lower_bound`). Functionally equivalent, but requires different termination semantics.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Iterative Inclusive (Optimal) | Approach 2: Recursive Binary Search |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | `O(1)` / `O(log N)` / `O(log N)` | `O(1)` / `O(log N)` / `O(log N)` |
| **Auxiliary Space** | `O(1)` strictly | `O(log N)` call stack frames |
| **Output Space** | `O(1)` integer | `O(1)` integer |
| **JIT / Compiler Optimization** | Unrolled loop / register hoisting | Stack frame push / pop overhead |
| **Arithmetic Overflow Risk** | Defended via `left + (right - left) / 2` | Defended via `left + (right - left) / 2` |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #41 - Binary Search
// Core Pattern: Logarithmic Halving over Monotonically Sorted Window [left, right]
// Invariant: If target exists, it is strictly contained within [left, right]
// Overflow Defense: mid = left + (right - left) / 2 prevents 32-bit signed overflow
// ============================================================================
```

#### Implementation 1: Iterative Standard Template (Optimal `O(1)` Space)
```csharp
public class Solution
{
    public int Search(int[] nums, int target)
    {
        // Boundary Defense: Null or empty arrays cannot contain target
        if (nums == null || nums.Length == 0)
        {
            return -1;
        }

        int left = 0;
        int right = nums.Length - 1;

        // Invariant: Target index is within inclusive bounds [left, right] if present
        while (left <= right)
        {
            // Overflow Defense: Avoid (left + right) / 2 which overflows when sum > 2^31 - 1
            int mid = left + (right - left) / 2;

            // Gate 1: Exact match detected
            if (nums[mid] == target)
            {
                return mid;
            }
            // Gate 2: Target lies strictly in the right partition [mid + 1, right]
            else if (nums[mid] < target)
            {
                left = mid + 1;
            }
            // Gate 3: Target lies strictly in the left partition [left, mid - 1]
            else
            {
                right = mid - 1;
            }
        }

        // Exhaustion: left > right implies candidate search space is empty
        return -1;
    }
}
```

#### Implementation 2: Tail-Recursive Binary Search (`O(log N)` Stack Space)
```csharp
public class SolutionRecursive
{
    public int Search(int[] nums, int target)
    {
        if (nums == null || nums.Length == 0) return -1;
        return BinarySearchRecursive(nums, target, 0, nums.Length - 1);
    }

    private static int BinarySearchRecursive(int[] nums, int target, int left, int right)
    {
        // Base Case: Empty search window
        if (left > right)
        {
            return -1;
        }

        int mid = left + (right - left) / 2;

        if (nums[mid] == target) return mid;
        if (nums[mid] < target) return BinarySearchRecursive(nums, target, mid + 1, right);
        return BinarySearchRecursive(nums, target, left, mid - 1);
    }
}
```

---

## 42. Search Insert Position (LeetCode #35)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⚪ Reinforcement |
| **Pattern Tags** | `#binary-search` `#lower-bound` `#insert-position` |
| **LeetCode Link** | [Search Insert Position](https://leetcode.com/problems/search-insert-position/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given a sorted array of distinct integers and a target value, return the index if the target is found. If not, return the index where it would be if it were inserted in order. You must write an algorithm with $O(\log n)$ runtime complexity.
- **Key Constraints:**
  - `1 <= nums.Length <= 10^4`
  - `-10^4 <= nums[i], target <= 10^4`
  - `nums` contains distinct values sorted in ascending order.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Locate the **Lower Bound** index: the first element index $i$ such that $nums[i] \ge target$.
- **Sample 1:**
  - **Input:** `nums = [1, 3, 5, 6]`, `target = 5` $\implies$ `2`
- **Sample 2:**
  - **Input:** `nums = [1, 3, 5, 6]`, `target = 2` $\implies$ `1`
- **Sample 3:**
  - **Input:** `nums = [1, 3, 5, 6]`, `target = 7` $\implies$ `4`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine inserting a new book onto a pre-sorted bookshelf. If the book already exists, you place it at that exact spot. If it does not exist, you find the first book on the shelf that is alphabetically *greater* than your book, slide everything from that book rightward by one slot, and slip your book into the vacated index. Finding this insertion index is identical to finding the **first element that is greater than or equal to target**.

#### 3.2 The Naive Bottleneck & Redundant Computation
A naive approach performs a linear scan from left to right:
```csharp
for (int i = 0; i < nums.Length; i++) {
    if (nums[i] >= target) return i;
}
return nums.Length;
```
When `target` exceeds all elements, it scans all $N$ entries in $O(N)$ time. Binary search finds this boundary in $O(\log N)$ time.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**The Lower-Bound Termination Invariant:**
Consider what happens when standard binary search terminates without finding an exact match ($left > right$).
Throughout the search, the loop maintains:
1. Every element at index $k < left$ is strictly $< target$.
2. Every element at index $k > right$ is strictly $> target$.

When the loop exits, $left = right + 1$.
At this precise boundary crossover:
- $nums[0 \dots left - 1] < target$
- $nums[left \dots N - 1] \ge target$
Therefore, index $left$ is mathematically guaranteed to be the exact lower-bound insertion index!

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
TERMINATION CROSSOVER ARCHITECTURE:
Target = 2, Array = [ 1,   3,   5,   6 ]
Indices:              0    1    2    3

At termination:
right = 0 (nums[0] = 1 < 2)
left  = 1 (nums[1] = 3 > 2)

[ 0 ... left - 1 ]           [ left ... N - 1 ]
All values < target          All values >= target
nums[0] = 1                  nums[1] = 3, nums[2] = 5 ...
                 ▲
           Insertion Index = left (1)
```

- `left`: Converges to the exact lower-bound insertion index.
- `right`: Sits on the largest element strictly smaller than `target`.

#### 3.5 State Transition Triggers & Decision Gates
- While $left \le right$:
  - `mid = left + (right - left) / 2`
  - If $nums[mid] == target \implies$ return $mid$.
  - If $nums[mid] < target \implies left = mid + 1$.
  - Else $right = mid - 1$.
- Return `left`.

#### 3.6 Concrete Step-by-Step State Trace
Trace: `nums = [1, 3, 5, 6]`, `target = 2`

| Iteration | `left` | `right` | `mid` | `nums[mid]` | Comparison vs 2 | Action |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Iter 1** | 0 | 3 | 1 | 3 | $3 > 2$ | $right = mid - 1 = 0$ |
| **Iter 2** | 0 | 0 | 0 | 1 | $1 < 2$ | $left = mid + 1 = 1$ |
| **End** | 1 | 0 | — | — | $left > right$ | Loop terminates. **Return `left = 1`** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Inclusive Loop Returning `left`):** Extremely clean, highly defensible, reuses standard binary search template directly with zero custom boundary branches.
- **Approach 2 (Left-Closed Right-Open `[left, right)` Lower-Bound):** Classic STL `std::lower_bound` implementation. Essential for range queries in segment trees and binary indexed trees.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup:** Initialize `left = 0`, `right = nums.Length - 1`.
- **Step 2: Binary Search:** Narrow search space with `left <= right`.
- **Step 3: Exact Match Early Exit:** Return `mid` if $nums[mid] == target$.
- **Step 4: Return `left`:** When space is exhausted, `left` holds the insertion index.

#### 4.3 Alternative Approaches Analysis
- **Bisect-Left Template:**
  Set `right = nums.Length`. Loop `while (left < right)`. If $nums[mid] < target$, $left = mid + 1$; else $right = mid$. Return `left`.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Inclusive Template (Optimal) | Approach 2: Bisect-Left Style |
| :--- | :--- | :--- |
| **Time Complexity** | `O(log N)` | `O(log N)` |
| **Auxiliary Space** | `O(1)` | `O(1)` |
| **Boundary Invariant** | Termination at $left = right + 1$ | Termination at $left == right$ |
| **Edge Case Handling** | Handles insertion at index 0 and index $N$ automatically | Handles index 0 and $N$ automatically |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #42 - Search Insert Position
// Core Pattern: Binary Search Lower Bound (First index where nums[i] >= target)
// Key Invariant: At loop termination (left > right), left is the exact insertion index
// Edge Case Mastery: target > max(nums) yields left = N; target < min(nums) yields left = 0
// ============================================================================
```

#### Implementation 1: Standard Inclusive Range with Return `left` (Optimal `O(1)` Space)
```csharp
public class Solution
{
    public int SearchInsert(int[] nums, int target)
    {
        int left = 0;
        int right = nums.Length - 1;

        // Invariant: Target belongs in [left, right + 1]
        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (nums[mid] == target)
            {
                return mid; // Exact match found
            }
            else if (nums[mid] < target)
            {
                left = mid + 1; // Discard left half
            }
            else
            {
                right = mid - 1; // Discard right half
            }
        }

        // CRITICAL INVARIANT: Upon loop termination (left == right + 1),
        // 'left' points to the first element strictly greater than target (or nums.Length)
        return left;
    }
}
```

#### Implementation 2: Bisect-Left Style Lower Bound (`[left, right)`)
```csharp
public class SolutionBisectLeft
{
    public int SearchInsert(int[] nums, int target)
    {
        int left = 0;
        int right = nums.Length; // Half-open interval [left, right)

        while (left < right)
        {
            int mid = left + (right - left) / 2;

            if (nums[mid] < target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid; // Squeeze right bound to mid
            }
        }

        return left;
    }
}
```

---

## 43. Find First and Last Position of Element in Sorted Array (LeetCode #34)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#binary-search` `#lower-bound` `#upper-bound` |
| **LeetCode Link** | [Find First and Last Position](https://leetcode.com/problems/find-first-and-last-position-of-element-in-sorted-array/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array of integers `nums` sorted in non-decreasing order, find the starting and ending position of a given `target` value. If `target` is not found in the array, return `[-1, -1]`. You must write an algorithm with $O(\log n)$ runtime complexity.
- **Key Constraints:**
  - `0 <= nums.Length <= 10^5`
  - `-10^9 <= nums[i], target <= 10^9`
  - `nums` is a non-decreasing array.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Run two specialized logarithmic boundary searches: one finding the leftmost boundary (first occurrence), and the second finding the rightmost boundary (last occurrence).
- **Sample 1:**
  - **Input:** `nums = [5, 7, 7, 8, 8, 10]`, `target = 8`
  - **Output:** `[3, 4]`
- **Sample 2:**
  - **Input:** `nums = [5, 7, 7, 8, 8, 10]`, `target = 6`
  - **Output:** `[-1, -1]`
- **Sample 3:**
  - **Input:** `nums = []`, `target = 0`
  - **Output:** `[-1, -1]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine looking for the start and end volume of a 10-volume encyclopedia entry on "Astronomy". You pull a volume from the shelf and see it is indeed "Astronomy". You cannot stop there, nor can you linearly walk volume by volume (which would take too long if there were a million volumes). To find the **first volume**, you write down the current volume as your best candidate, but you intentionally force your search to continue **to the left**. To find the **last volume**, you record the candidate and force your search **to the right**.

#### 3.2 The Naive Bottleneck & Redundant Computation
A common naive mistake is using binary search to find *any* occurrence of `target` at index $m$, and then expanding linearly outward using two while loops:
```csharp
while (left >= 0 && nums[left] == target) left--;
while (right < n && nums[right] == target) right++;
```
**Catastrophic Flaw:** If the array contains $10^5$ identical elements (e.g. `[8, 8, 8, ..., 8]`), the linear expansion takes $O(N)$ operations, completely violating the mandatory $O(\log N)$ constraint and causing TLE! Both boundaries must be resolved purely logarithmically.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**The Boundary Squeeze Invariant:**
When $nums[mid] == target$:
- **To find the First Occurrence:** Record `candidate = mid`. Because earlier occurrences can only exist to the left of `mid`, discard `mid` and the entire right partition by contracting:
  $$right = mid - 1$$
- **To find the Last Occurrence:** Record `candidate = mid`. Because later occurrences can only exist to the right of `mid`, discard `mid` and the entire left partition by contracting:
  $$left = mid + 1$$
Each search runs in strictly $O(\log N)$ time, guaranteeing $2 \times O(\log N) = O(\log N)$ total runtime.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
FIRST OCCURRENCE SQUEEZE (nums[mid] == target):
Record candidate = mid
Contract search window leftward: right = mid - 1

[ left ............ mid - 1 ]   [ mid (Candidate) ]   [ Discarded Right Half ]
         ▲
New Search Window (Seeks earlier occurrence)

LAST OCCURRENCE SQUEEZE (nums[mid] == target):
Record candidate = mid
Contract search window rightward: left = mid + 1

[ Discarded Left Half ]   [ mid (Candidate) ]   [ mid + 1 ............ right ]
                                                              ▲
                                            New Search Window (Seeks later occurrence)
```

#### 3.5 State Transition Triggers & Decision Gates
Helper method `FindBound(nums, target, bool findFirst)`:
- If $nums[mid] == target$:
  - `bound = mid` (cache candidate)
  - If `findFirst == true` $\implies right = mid - 1$
  - Else $\implies left = mid + 1$
- Else if $nums[mid] < target \implies left = mid + 1$
- Else $right = mid - 1$
- Return `bound`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `nums = [5, 7, 7, 8, 8, 10]`, `target = 8`

**Pass 1: Find First Position (`findFirst = true`)**
| Iter | `left` | `right` | `mid` | `nums[mid]` | Action | Candidate `bound` |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | 0 | 5 | 2 | 7 | $7 < 8 \implies left = 3$ | -1 |
| 2 | 3 | 5 | 4 | 8 | $8 == 8 \implies bound = 4, right = 3$ | 4 |
| 3 | 3 | 3 | 3 | 8 | $8 == 8 \implies bound = 3, right = 2$ | 3 |
| End | 3 | 2 | — | — | $left > right \implies$ Exit | **Returns 3** |

**Pass 2: Find Last Position (`findFirst = false`)**
| Iter | `left` | `right` | `mid` | `nums[mid]` | Action | Candidate `bound` |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | 0 | 5 | 2 | 7 | $7 < 8 \implies left = 3$ | -1 |
| 2 | 3 | 5 | 4 | 8 | $8 == 8 \implies bound = 4, left = 5$ | 4 |
| 3 | 5 | 5 | 5 | 10 | $10 > 8 \implies right = 4$ | 4 |
| End | 5 | 4 | — | — | $left > right \implies$ Exit | **Returns 4** |

Result: `[3, 4]`

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Unified Parameterized Helper):** Encapsulates the boundary logic into a clean, reusable helper method with a boolean flag. Eliminates code duplication and prevents copy-paste variable bugs.
- **Approach 2 (C++ Style Lower Bound & Upper Bound):** Computes `lower_bound(target)` and `lower_bound(target + 1) - 1`. Mathematically elegant, standard in competitive programming.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Early Exit:** If array is empty, return `[-1, -1]`.
- **Step 2: Find Left Boundary:** Run `FindBound(findFirst: true)`. If result is `-1`, target is absent $\implies$ return `[-1, -1]`.
- **Step 3: Find Right Boundary:** Run `FindBound(findFirst: false)`.
- **Step 4: Return:** Return `new int[] { first, last }`.

#### 4.3 Alternative Approaches Analysis
- **Two Lower Bounds:**
  First occurrence is `idx1 = LowerBound(target)`. If `idx1 == N || nums[idx1] != target`, return `[-1, -1]`.
  Last occurrence is `idx2 = LowerBound(target + 1) - 1`. Return `[idx1, idx2]`.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Unified Parameterized Search | Approach 2: Dual Lower Bounds |
| :--- | :--- | :--- |
| **Time Complexity** | $2 \times O(\log N) = O(\log N)$ | $2 \times O(\log N) = O(\log N)$ |
| **Auxiliary Space** | `O(1)` | `O(1)` |
| **Linear Scan Risk** | Zero (strictly logarithmic) | Zero (strictly logarithmic) |
| **Readability** | High (explicit intent) | Moderate (requires understanding target + 1 shift) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #43 - Find First and Last Position
// Core Pattern: Decoupled Dual Logarithmic Boundary Squeezing
// Strict Complexity: 2 * O(log N) runtime; zero linear scan degradation on duplicate arrays
// Boundary Invariant: On match, contract search space in direction of desired bound
// ============================================================================
```

#### Implementation 1: Unified Parameterized Binary Search Helper (Optimal `O(log N)`)
```csharp
public class Solution
{
    public int[] SearchRange(int[] nums, int target)
    {
        // Boundary Defense: Empty array cannot contain target
        if (nums == null || nums.Length == 0)
        {
            return new int[] { -1, -1 };
        }

        // Pass 1: Find leftmost boundary
        int first = FindBound(nums, target, findFirst: true);

        // Optimization: If target does not exist, skip the second binary search entirely
        if (first == -1)
        {
            return new int[] { -1, -1 };
        }

        // Pass 2: Find rightmost boundary
        int last = FindBound(nums, target, findFirst: false);

        return new int[] { first, last };
    }

    private static int FindBound(int[] nums, int target, bool findFirst)
    {
        int left = 0;
        int right = nums.Length - 1;
        int bound = -1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (nums[mid] == target)
            {
                bound = mid; // Record valid occurrence candidate

                // Gate: Contract search space towards the desired boundary
                if (findFirst)
                {
                    right = mid - 1; // Seek earlier occurrence to the left
                }
                else
                {
                    left = mid + 1;  // Seek later occurrence to the right
                }
            }
            else if (nums[mid] < target)
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return bound;
    }
}
```

#### Implementation 2: Dual Lower Bounds (C++ STL Style)
```csharp
public class SolutionDualLowerBound
{
    public int[] SearchRange(int[] nums, int target)
    {
        if (nums == null || nums.Length == 0) return new int[] { -1, -1 };

        int first = LowerBound(nums, target);
        // If target is out of bounds or not equal, target does not exist
        if (first == nums.Length || nums[first] != target)
        {
            return new int[] { -1, -1 };
        }

        // Last position is the predecessor of lower bound for (target + 1)
        int last = LowerBound(nums, target + 1) - 1;

        return new int[] { first, last };
    }

    private static int LowerBound(int[] nums, int target)
    {
        int left = 0, right = nums.Length;
        while (left < right)
        {
            int mid = left + (right - left) / 2;
            if (nums[mid] < target) left = mid + 1;
            else right = mid;
        }
        return left;
    }
}
```

---

## 44. Search in Rotated Sorted Array (LeetCode #33)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#binary-search` `#rotated-sorted-array` `#pivot-partition` |
| **LeetCode Link** | [Search in Rotated Sorted Array](https://leetcode.com/problems/search-in-rotated-sorted-array/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** There is an integer array `nums` sorted in ascending order (with distinct values). Prior to being passed to your function, `nums` is possibly rotated at an unknown pivot index $k$ ($1 \le k < nums.length$). Given the array `nums` after the possible rotation and an integer `target`, return the index of `target` if it is in `nums`, or `-1` if it is not in `nums`. You must achieve $O(\log n)$ runtime complexity.
- **Key Constraints:**
  - `1 <= nums.Length <= 5000`
  - `-10^4 <= nums[i], target <= 10^4`
  - All values of `nums` are unique.
  - `nums` is an ascending array that is possibly rotated.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** At any midpoint division in a rotated sorted array, at least one of the two halves is guaranteed to be monotonically sorted. Test if the target resides within that sorted half to discard the opposite half.
- **Sample 1:**
  - **Input:** `nums = [4, 5, 6, 7, 0, 1, 2]`, `target = 0` $\implies$ `4`
- **Sample 2:**
  - **Input:** `nums = [4, 5, 6, 7, 0, 1, 2]`, `target = 3` $\implies$ `-1`
- **Sample 3:**
  - **Input:** `nums = [1]`, `target = 0` $\implies$ `-1`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Picture a straight staircase that was cut in two, with the top section placed at the bottom. It now consists of two ascending ramps separated by a sharp cliff drop (the inflection point). When you cut this staircase anywhere down the middle, **one of the two halves is guaranteed to be an unbroken, normally ascending ramp**. If your target elevation falls between the floor and ceiling of that unbroken ramp, you can search inside it; otherwise, the target must be on the other side. You never have to worry about the cliff!

#### 3.2 The Naive Bottleneck & Redundant Computation
A linear scan takes $O(N)$ operations. Another common multi-pass approach first runs a binary search to find the inflection pivot index, splits the array into two virtual subarrays, and runs a second binary search on the appropriate subarray. While $2 \times O(\log N) = O(\log N)$, it requires writing three distinct methods, complicating boundary edge cases. A single-pass binary search accomplishes the entire search in one concise loop.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**The Monotonic Half Invariant:**
Compare `nums[left]` with `nums[mid]`:
- **Condition A (`nums[left] <= nums[mid]`): The Left Half $[left, mid]$ is Sorted:**
  The left ramp has no inflection drop.
  - If `nums[left] <= target && target < nums[mid]`:
    Target is guaranteed to lie within the left half $\implies right = mid - 1$.
  - Else:
    Target must lie in the right half $\implies left = mid + 1$.
- **Condition B (`nums[left] > nums[mid]`): The Right Half $[mid, right]$ is Sorted:**
  The inflection drop occurred in the left half, so the right half is unbroken.
  - If `nums[mid] < target && target <= nums[right]`:
    Target is guaranteed to lie within the right half $\implies left = mid + 1$.
  - Else:
    Target must lie in the left half $\implies right = mid - 1$.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
ROTATED ARRAY GEOMETRY:
Array: [ 4,   5,   6,   7,   0,   1,   2 ]
         ▲              ▲              ▲
       left            mid           right

Left Half: [4, 5, 6, 7] is monotonically ascending (nums[left] <= nums[mid]).
Right Half: [7, 0, 1, 2] contains the inflection drop cliff (7 -> 0).

DECISION:
Target = 0.
Check left half: Is 4 <= 0 < 7? No!
Therefore, target must be in the right half!
==> Shift left = mid + 1.
```

- `left`, `right`: Active search boundaries.
- `mid`: Pivot point splitting array into one sorted half and one rotated half.

#### 3.5 State Transition Triggers & Decision Gates
While $left \le right$:
1. If $nums[mid] == target \implies$ return $mid$.
2. If $nums[left] \le nums[mid]$ (Left Half Sorted):
   - If $nums[left] \le target < nums[mid] \implies right = mid - 1$
   - Else $\implies left = mid + 1$
3. Else (Right Half Sorted):
   - If $nums[mid] < target \le nums[right] \implies left = mid + 1$
   - Else $\implies right = mid - 1$
4. Return `-1` on loop exhaustion.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `nums = [4, 5, 6, 7, 0, 1, 2]`, `target = 0`

| Iter | `left` | `right` | `mid` | `nums[mid]` | Sorted Half | In Range Check | Action |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1** | 0 | 6 | 3 | 7 | Left ($4 \le 7$) | Is $4 \le 0 < 7$? False | $left = mid + 1 = 4$ |
| **2** | 4 | 6 | 5 | 1 | Right ($1 \le 2$) | Is $1 < 0 \le 2$? False | $right = mid - 1 = 4$ |
| **3** | 4 | 4 | 4 | 0 | Match! | $nums[4] == 0$ | **Return `mid = 4`** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Single-Pass Partition Check):** The industry standard. Operates in a single pass of $O(\log N)$ time and $O(1)$ space with zero secondary function calls.
- **Approach 2 (Two-Pass: Find Pivot then Standard Binary Search):** Separates concerns cleanly: first finds minimum element index $p$, then searches standard binary search on $[0, p-1]$ or $[p, N-1]$. Useful when learning rotated arrays for the first time.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Initialize `left = 0`, `right = nums.Length - 1`.
- **Step 2: Loop:** While `left <= right`.
- **Step 3: Branch by Sorted Half:** Test `nums[left] <= nums[mid]` to determine which half is sorted, then test target bounds.
- **Step 4: Return:** Return index on match, `-1` on exhaustion.

#### 4.3 Alternative Approaches Analysis
- **Two-Pass Approach:**
  Find pivot using `FindMinIndex(nums)`. If `target >= nums[pivot] && target <= nums[n-1]`, search right ramp; else search left ramp. Takes $2 \times O(\log N) = O(\log N)$ time.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Single-Pass Monotonic Check | Approach 2: Two-Pass Pivot Search |
| :--- | :--- | :--- |
| **Time Complexity** | `O(log N)` (single pass) | $2 \times O(\log N) = O(\log N)$ |
| **Auxiliary Space** | `O(1)` | `O(1)` |
| **Code Footprint** | Compact (one loop) | Verbose (three separate functions) |
| **Branch Complexity** | Nested if-else decision tree | Sequential modular pipelines |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #44 - Search in Rotated Sorted Array
// Core Pattern: Monotonic Half Detection in Single-Pass Binary Search
// Fundamental Invariant: Any midpoint cut divides a rotated array into at least one sorted half
// Decision Boundary: Check target against bounds of the sorted half to discard the other half
// ============================================================================
```

#### Implementation 1: Single-Pass Monotonic Half Check (Optimal `O(log N)`)
```csharp
public class Solution
{
    public int Search(int[] nums, int target)
    {
        // Boundary Defense: Empty array check
        if (nums == null || nums.Length == 0)
        {
            return -1;
        }

        int left = 0;
        int right = nums.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            // Gate 1: Exact match found
            if (nums[mid] == target)
            {
                return mid;
            }

            // Gate 2: Determine which half is monotonically sorted
            if (nums[left] <= nums[mid])
            {
                // Invariant: The left partition [left ... mid] is strictly sorted
                if (nums[left] <= target && target < nums[mid])
                {
                    // Target lies strictly within sorted left partition
                    right = mid - 1;
                }
                else
                {
                    // Target must lie in the right partition
                    left = mid + 1;
                }
            }
            else
            {
                // Invariant: The right partition [mid ... right] is strictly sorted
                if (nums[mid] < target && target <= nums[right])
                {
                    // Target lies strictly within sorted right partition
                    left = mid + 1;
                }
                else
                {
                    // Target must lie in the left partition
                    right = mid - 1;
                }
            }
        }

        return -1; // Target not found
    }
}
```

#### Implementation 2: Two-Pass (Find Pivot Min Index + Standard Binary Search)
```csharp
public class SolutionTwoPass
{
    public int Search(int[] nums, int target)
    {
        if (nums == null || nums.Length == 0) return -1;

        // Step 1: Find inflection pivot index (minimum element index)
        int pivot = FindMinIndex(nums);

        // Step 2: Binary search appropriate sorted half
        if (target >= nums[pivot] && target <= nums[nums.Length - 1])
        {
            return BinarySearch(nums, pivot, nums.Length - 1, target);
        }
        else
        {
            return BinarySearch(nums, 0, pivot - 1, target);
        }
    }

    private static int FindMinIndex(int[] nums)
    {
        int l = 0, r = nums.Length - 1;
        while (l < r)
        {
            int m = l + (r - l) / 2;
            if (nums[m] > nums[r]) l = m + 1;
            else r = m;
        }
        return l;
    }

    private static int BinarySearch(int[] nums, int l, int r, int target)
    {
        while (l <= r)
        {
            int m = l + (r - l) / 2;
            if (nums[m] == target) return m;
            else if (nums[m] < target) l = m + 1;
            else r = m - 1;
        }
        return -1;
    }
}
```

---

## 45. Find Minimum in Rotated Sorted Array (LeetCode #153)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#binary-search` `#rotated-sorted-array` `#inflection-point` |
| **LeetCode Link** | [Find Minimum in Rotated Sorted Array](https://leetcode.com/problems/find-minimum-in-rotated-sorted-array/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Suppose an array of length $n$ sorted in ascending order is rotated between $1$ and $n$ times. Given the sorted rotated array `nums` of unique elements, return the minimum element of this array in $O(\log n)$ time.
- **Key Constraints:**
  - `n == nums.Length`
  - `1 <= n <= 5000`
  - `-5000 <= nums[i] <= 5000`
  - All integers of `nums` are unique.
  - `nums` is sorted and rotated between $1$ and $n$ times.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Binary search the inflection drop cliff where $nums[i] > nums[i+1]$ by comparing $nums[mid]$ against the right boundary $nums[right]$.
- **Sample 1:**
  - **Input:** `nums = [3, 4, 5, 1, 2]` $\implies$ `1`
- **Sample 2:**
  - **Input:** `nums = [4, 5, 6, 7, 0, 1, 2]` $\implies$ `0`
- **Sample 3:**
  - **Input:** `nums = [11, 13, 15, 17]` $\implies$ `11`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Think of an escalator that has been split into two sections: an upper ramp and a lower ramp. The minimum element is the very first step of the lower ramp (the base of the waterfall). How do we know whether our current position `mid` is on the upper ramp or the lower ramp? We simply compare our height to the **last step on the right** (`nums[right]`). If we are higher than the rightmost step, we are standing on the upper ramp, so the waterfall drop must be to our right. If we are lower than or equal to the rightmost step, we are on the lower ramp, so the start of this lower ramp must be at `mid` or to our left!

#### 3.2 The Naive Bottleneck & Redundant Computation
A linear scan takes $O(N)$ operations.
A dangerous mistake is comparing `nums[mid]` with `nums[left]`. Why?
Consider an unrotated array `[1, 2, 3, 4, 5]`: here `nums[mid] (3) > nums[left] (1)`.
Now consider a rotated array `[3, 4, 5, 1, 2]`: here `nums[mid] (5) > nums[left] (3)`.
In both cases, `nums[mid] > nums[left]` is True, but the minimum is on the left in the first case and on the right in the second! Comparing against `nums[left]` is ambiguous. Comparing against `nums[right]` is **unconditionally unambiguous**.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**The Right-Boundary Invariant:**
Compare `nums[mid]` against `nums[right]`:
- **Case 1: `nums[mid] > nums[right]`:**
  Because the array is sorted and rotated, if `nums[mid]` is larger than `nums[right]`, the inflection drop must lie strictly to the right of `mid`. The minimum cannot be `mid` or anything to its left:
  $$left = mid + 1$$
- **Case 2: `nums[mid] <= nums[right]`:**
  The subsegment $[mid, right]$ is monotonically ascending. The minimum element could be `mid` itself, or it could be to the left of `mid`. It can never be strictly to the right of `mid`:
  $$right = mid$$
Notice that `right` is set to `mid`, not `mid - 1`, because `mid` itself could be the minimum.
The loop terminates when $left == right$, pointing directly at the global minimum.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
RIGHT BOUNDARY COMPARISON:

Case 1: nums[mid] > nums[right]
Upper Ramp: [ 4,  5,  6,  7 ]
                          ▲ (mid: 7)
Waterfall Cliff:          │
                          ▼
Lower Ramp:                 [ 0,  1,  2 ]
                                      ▲ (right: 2)
7 > 2 ==> Inflection drop is to the right! Discard left: left = mid + 1.

Case 2: nums[mid] <= nums[right]
Upper Ramp: [ 4,  5 ]
Lower Ramp:           [ 0,  1,  2 ]
                            ▲       ▲
                           mid    right
1 <= 2 ==> mid is on lower ramp! Minimum is at or left of mid: right = mid.
```

#### 3.5 State Transition Triggers & Decision Gates
Loop while $left < right$:
1. `mid = left + (right - left) / 2`
2. If `nums[mid] > nums[right]` $\implies left = mid + 1$
3. Else $\implies right = mid$
4. Return `nums[left]` when $left == right$.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `nums = [3, 4, 5, 1, 2]`

| Iteration | `left` | `right` | `mid` | `nums[mid]` | `nums[right]` | Comparison | Action |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Iter 1** | 0 | 4 | 2 | 5 | 2 | $5 > 2$ | $left = mid + 1 = 3$ |
| **Iter 2** | 3 | 4 | 3 | 1 | 2 | $1 \le 2$ | $right = mid = 3$ |
| **End** | 3 | 3 | — | — | — | $left == right$ | Loop terminates. **Return `nums[3] = 1`** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Right-Boundary Squeeze `left < right`):** The optimal paradigm. Eliminates separate boundary checks, automatically handles 0-rotation lists, terminates with `left == right` in $O(1)$ space.
- **Approach 2 (Adjacent Cliff Detection `left <= right`):** Directly checks if $nums[mid] > nums[mid + 1]$ or $nums[mid - 1] > nums[mid]$. Requires extra index boundary guards.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup:** Initialize `left = 0`, `right = nums.Length - 1`.
- **Step 2: Convergent Loop:** While `left < right`.
- **Step 3: Right Boundary Comparison:** Branch $left = mid + 1$ or $right = mid$.
- **Step 4: Resolution:** Return `nums[left]`.

#### 4.3 Alternative Approaches Analysis
- **Adjacent Cliff Check:**
  Check `if (nums[mid] > nums[mid + 1]) return nums[mid + 1];`
  Requires checking `mid > 0` and `mid < n - 1` bounds. More error-prone than right-boundary squeeze.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Convergent Right-Squeeze (Optimal) | Approach 2: Adjacent Cliff Detection |
| :--- | :--- | :--- |
| **Time Complexity** | `O(log N)` | `O(log N)` |
| **Auxiliary Space** | `O(1)` | `O(1)` |
| **Loop Condition** | `while (left < right)` | `while (left <= right)` |
| **0-Rotation Handling** | Handled natively without extra code | Requires upfront check `nums[0] <= nums[n-1]` |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #45 - Find Minimum in Rotated Sorted Array
// Core Pattern: Right-Boundary Convergent Binary Search (left < right)
// Fundamental Invariant: nums[mid] > nums[right] proves minimum lies strictly in [mid + 1, right]
// Asymmetric Squeeze: right = mid preserves mid as candidate; left = mid + 1 discards mid
// ============================================================================
```

#### Implementation 1: Convergent Range `left < right` (Optimal `O(log N)`)
```csharp
public class Solution
{
    public int FindMin(int[] nums)
    {
        // Boundary Defense: Array must not be null or empty
        if (nums == null || nums.Length == 0)
        {
            throw new ArgumentException("Input array must contain at least one element.");
        }

        int left = 0;
        int right = nums.Length - 1;

        // Invariant: Minimum element is always within [left, right]
        // Loop terminates when left == right, pinpointing the minimum element
        while (left < right)
        {
            int mid = left + (right - left) / 2;

            // Gate 1: If nums[mid] > nums[right], mid is on the upper ramp.
            // The inflection drop MUST occur to the right of mid.
            if (nums[mid] > nums[right])
            {
                left = mid + 1; // Discard mid and everything to the left
            }
            // Gate 2: nums[mid] <= nums[right], mid is on the lower ramp.
            // mid could be the minimum, or the minimum is to its left.
            else
            {
                right = mid; // Preserve mid as a candidate
            }
        }

        // 'left' and 'right' converge on the exact minimum element index
        return nums[left];
    }
}
```

#### Implementation 2: Inclusive `left <= right` with Adjacent Inflection Check
```csharp
public class SolutionAdjacentCheck
{
    public int FindMin(int[] nums)
    {
        if (nums.Length == 1) return nums[0];

        // If list is not rotated at all (or rotated n times)
        if (nums[0] < nums[nums.Length - 1]) return nums[0];

        int left = 0;
        int right = nums.Length - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            // Direct inflection check: element drops immediately after mid
            if (mid < nums.Length - 1 && nums[mid] > nums[mid + 1])
            {
                return nums[mid + 1];
            }

            // Direct inflection check: mid itself is the drop from mid - 1
            if (mid > 0 && nums[mid - 1] > nums[mid])
            {
                return nums[mid];
            }

            // Determine search direction
            if (nums[mid] > nums[0])
            {
                left = mid + 1;
            }
            else
            {
                right = mid - 1;
            }
        }

        return nums[0];
    }
}
```

---

## 46. Koko Eating Bananas (LeetCode #875)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#binary-search-on-answer` `#monotonic-feasibility` |
| **LeetCode Link** | [Koko Eating Bananas](https://leetcode.com/problems/koko-eating-bananas/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Koko loves to eat bananas. There are `n` piles of bananas, the $i$-th pile has `piles[i]` bananas. The guards have gone and will come back in `h` hours. Koko can decide her bananas-per-hour eating speed of `k`. Each hour, she chooses some pile of bananas and eats `k` bananas from that pile. If the pile has less than `k` bananas, she eats all of them instead and will not eat any more bananas during this hour. Koko likes to eat slowly but still wants to finish eating all the bananas before the guards return. Return the minimum integer `k` such that she can eat all the bananas within `h` hours.
- **Key Constraints:**
  - `1 <= piles.Length <= 10^4`
  - `piles.Length <= h <= 10^9`
  - `1 <= piles[i] <= 10^9`

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Binary search on the monotonic answer space $[1, \max(piles)]$ to find the minimum feasible speed using a validation predicate function.
- **Sample 1:**
  - **Input:** `piles = [3, 6, 7, 11]`, `h = 8` $\implies$ `4`
- **Sample 2:**
  - **Input:** `piles = [30, 11, 23, 4, 20]`, `h = 5` $\implies$ `30`
- **Sample 3:**
  - **Input:** `piles = [30, 11, 23, 4, 20]`, `h = 6` $\implies$ `23`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Think of an adjustable speed valve on a water pipe.
- At speed $k = 1$ banana/hour, it takes forever; the guards will return and catch Koko.
- At speed $k = \max(piles)$, Koko eats each pile in exactly 1 hour, taking $N$ hours. Since $h \ge N$, this speed is always fast enough.
The feasibility predicate $\text{CanFinish}(k)$ is a **monotonic step function**:
$$[ \text{False}, \text{False}, \dots, \text{False}, \mathbf{True}, \text{True}, \dots, \text{True} ]$$
We do not search inside the array `piles`. We search across the **range of possible eating speeds** $[1, \max(piles)]$ to find the exact boundary cliff where the predicate flips from `False` to `True`.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force solution starts at speed $k = 1$ and increments $k$ by 1 until $\text{CanFinish}(k)$ returns true:
```csharp
for (int k = 1; k <= maxPile; k++) {
    if (CanFinish(piles, k, h)) return k;
}
```
In the worst case where $\max(piles) = 10^9$ and $N = 10^4$:
$$T(N) = 10^9 \times 10^4 = 10^{13} \text{ operations}$$
This would take hours to run. Binary search reduces the $10^9$ speed space to $\lceil \log_2 10^9 \rceil \approx 30$ evaluations! Total operations: $30 \times 10^4 \approx 3 \times 10^5$, executing in under $2$ milliseconds.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Monotonic Feasibility & Ceiling Arithmetic:**
For a given speed $k$, the hours required to consume pile $p$ is:
$$\text{hours}(p, k) = \lceil p / k \rceil = \frac{p + k - 1}{k} \quad (\text{integer division ceiling})$$
Total hours required is $\sum_{p \in piles} \lceil p / k \rceil$.
- If $\sum \text{hours} \le h$:
  Speed $k$ is feasible. A faster speed will also be feasible, but we want the **minimum** speed. We record $k$ as our best answer so far and explore slower speeds to the left:
  $$high = midSpeed - 1$$
- If $\sum \text{hours} > h$:
  Speed $k$ is too slow. Koko cannot finish. Any speed $\le k$ is also too slow. We must eat faster:
  $$low = midSpeed + 1$$
**Arithmetic Overflow Defense:**
Because $piles[i] \le 10^9$ and $N \le 10^4$, total hours can reach $10^4 \times 10^9 = 10^{13}$, which overflows a 32-bit signed integer (`int.MaxValue` $\approx 2 \times 10^9$). The accumulator variable `totalHours` **must be typed as `long`**.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
MONOTONIC FEASIBILITY PREDICATE CLIFF:

Speed k:       1       2       3      [ 4 ]      5       6  ...  11 (max)
Feasible?    False   False   False    TRUE     True    True ...  True
                                       ▲
                            Target: Minimum Feasible k

SEARCH RANGE [low, high]:
low = 1, high = max(piles)
midSpeed = low + (high - low) / 2

If CanFinish(midSpeed) == true:
  optimalSpeed = midSpeed; high = midSpeed - 1; (Seek slower viable speed)
Else:
  low = midSpeed + 1; (Must eat faster)
```

- `low`: Minimum conceivable speed (1).
- `high`: Maximum conceivable speed ($\max(piles)$).
- `midSpeed`: Active eating rate tested by the feasibility predicate.

#### 3.5 State Transition Triggers & Decision Gates
1. Find `high = max(piles)`.
2. While `low <= high`:
   - `midSpeed = low + (high - low) / 2`
   - If `CanFinish(piles, midSpeed, h)`:
     - `optimalSpeed = midSpeed`
     - `high = midSpeed - 1`
   - Else:
     - `low = midSpeed + 1`
3. Return `optimalSpeed`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `piles = [3, 6, 7, 11]`, `h = 8` ($\max = 11$)

| Iter | `low` | `high` | `midSpeed` | Hours Calculation: $\lceil p / k \rceil$ | Total Hours | $\le 8$? | Action |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1** | 1 | 11 | 6 | $\lceil 3/6 \rceil=1, \lceil 6/6 \rceil=1, \lceil 7/6 \rceil=2, \lceil 11/6 \rceil=2$ | $1+1+2+2 = 6$ | True | Feasible! `ans = 6, high = 5` |
| **2** | 1 | 5 | 3 | $\lceil 3/3 \rceil=1, \lceil 6/3 \rceil=2, \lceil 7/3 \rceil=3, \lceil 11/3 \rceil=4$ | $1+2+3+4 = 10$ | False | Too slow! `low = 4` |
| **3** | 4 | 5 | 4 | $\lceil 3/4 \rceil=1, \lceil 6/4 \rceil=2, \lceil 7/4 \rceil=2, \lceil 11/4 \rceil=3$ | $1+2+2+3 = 8$ | True | Feasible! `ans = 4, high = 3` |
| **End**| 4 | 3 | — | — | — | — | $low > high \implies$ **Return `4`** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Binary Search on Answer Space:** Identify this pattern when:
  1. The problem asks for the minimum or maximum parameter satisfying a constraint.
  2. Verifying a candidate answer takes polynomial time (here $O(N)$).
  3. The verification result is strictly monotonic with respect to the parameter.
- **Ceiling Division Trick:** Use `(pile + speed - 1) / speed` to compute $\lceil pile / speed \rceil$ in pure integer arithmetic, avoiding expensive floating-point `Math.Ceiling` and precision loss.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Determine Range:** Minimum speed `low = 1`, maximum speed `high = max(piles)`.
- **Step 2: Binary Search Answer:** Halve speed range using `low <= high`.
- **Step 3: Feasibility Gate:** Accumulate hours as `long`. Early exit if `totalHours > h`.
- **Step 4: Return:** Return `optimalSpeed`.

#### 4.3 Alternative Approaches Analysis
- **Convergent `low < high` Template:**
  Set `high = max(piles)`. While `low < high`: if `CanFinish(mid)` then `high = mid`, else `low = mid + 1`. Return `low`.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Binary Search on Answer Space (Optimal) | Linear Speed Search (Naive) |
| :--- | :--- | :--- |
| **Time Complexity** | `O(N * log(max(P)))` | `O(N * max(P))` |
| **Auxiliary Space** | `O(1)` | `O(1)` |
| **Operations ($N=10^4, P=10^9$)** | $\approx 3 \times 10^5$ ($< 2$ ms) | $\approx 10^{13}$ (Time Limit Exceeded) |
| **Integer Overflow Risk** | Defended via `long totalHours` | Vulnerable if accumulating `int` |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #46 - Koko Eating Bananas
// Core Pattern: Binary Search on Monotonic Answer Space (Speed Range [1, max(piles)])
// Feasibility Monotonicity: If speed k works, any k' > k works => binary search viable
// 64-Bit Arithmetic Defense: Total hours accumulator must be long to avoid 32-bit overflow
// Ceiling Trick: (pile + speed - 1) / speed computes ceil(pile / speed) in integer math
// ============================================================================
```

#### Implementation 1: Binary Search on Answer with Ceiling Arithmetic (Optimal `O(1)` Space)
```csharp
public class Solution
{
    public int MinEatingSpeed(int[] piles, int h)
    {
        // Boundary check
        if (piles == null || piles.Length == 0) return 0;

        int low = 1; // Minimum possible eating speed
        int high = 0;

        // Determine the maximum pile size to establish upper search boundary
        foreach (int pile in piles)
        {
            if (pile > high)
            {
                high = pile;
            }
        }

        int optimalSpeed = high;

        // Invariant: Optimal speed lies within [low, high]
        while (low <= high)
        {
            int midSpeed = low + (high - low) / 2;

            // Gate: Check feasibility of midSpeed
            if (CanFinish(piles, midSpeed, h))
            {
                optimalSpeed = midSpeed; // Record viable speed candidate
                high = midSpeed - 1;     // Seek slower viable speed to the left
            }
            else
            {
                low = midSpeed + 1;      // Speed is too slow; must eat faster
            }
        }

        return optimalSpeed;
    }

    private static bool CanFinish(int[] piles, int speed, int maxHours)
    {
        long totalHours = 0; // 64-bit integer prevents arithmetic overflow

        foreach (int pile in piles)
        {
            // Integer ceiling formula: ceil(a / b) = (a + b - 1) / b
            totalHours += (pile + (long)speed - 1) / speed;

            // Early exit optimization: prune iteration if budget already exceeded
            if (totalHours > maxHours)
            {
                return false;
            }
        }

        return totalHours <= maxHours;
    }
}
```

#### Implementation 2: Convergent Lower Bound Template (`low < high`)
```csharp
public class SolutionConvergent
{
    public int MinEatingSpeed(int[] piles, int h)
    {
        int low = 1, high = 1;
        foreach (int p in piles) if (p > high) high = p;

        while (low < high)
        {
            int mid = low + (high - low) / 2;
            long hours = 0;
            foreach (int p in piles)
            {
                hours += (p + (long)mid - 1) / mid;
            }

            if (hours <= h)
            {
                high = mid; // Preserve mid as possible minimum
            }
            else
            {
                low = mid + 1; // Discard mid
            }
        }

        return low;
    }
}
```

---

## 47. Search a 2D Matrix (LeetCode #74)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#binary-search` `#virtual-flattening` `#matrix-coordinate-mapping` |
| **LeetCode Link** | [Search a 2D Matrix](https://leetcode.com/problems/search-a-2d-matrix/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given an $m \times n$ integer matrix `matrix` with the following two properties:
  1. Each row is sorted in non-decreasing order.
  2. The first integer of each row is greater than the last integer of the previous row.
  Given an integer `target`, return `true` if `target` is in `matrix` or `false` otherwise.
- **Strict Requirement:** You must write a solution in $O(\log(m \times n))$ time complexity.
- **Key Constraints:**
  - $m == \text{matrix.length}$
  - $n == \text{matrix[i].length}$
  - $1 \le m, n \le 100$
  - $-10^4 \le \text{matrix}[i][j], \text{target} \le 10^4$

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Treat the $m \times n$ 2D matrix as a contiguous virtual 1D sorted array of length $m \times n$. Map any virtual index $k$ to 2D coordinates `(k / n, k % n)` in $O(1)$ time to run standard binary search without allocating memory.
- **Sample 1:**
  - **Input:** `matrix = [[1,3,5,7],[10,11,16,20],[23,30,34,60]]`, `target = 3`
  - **Output:** `true`
- **Sample 2:**
  - **Input:** `matrix = [[1,3,5,7],[10,11,16,20],[23,30,34,60]]`, `target = 13`
  - **Output:** `false`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine an encyclopedia divided into $m$ chapters, with exactly $n$ sentences per chapter. Because sentence 1 of chapter 2 begins strictly after the last sentence of chapter 1, the entire book forms **one continuous chronological story**. Even though the text is visually formatted into a 2D page layout, you can treat all $m \times n$ sentences as a single 1D scroll. If someone asks for sentence #7 in a book with 4 sentences per chapter, you know immediately that it is on Chapter $7 / 4 = 1$ (the second chapter), Sentence $7 \% 4 = 3$ (0-indexed).

#### 3.2 The Naive Bottleneck & Redundant Computation
1. **Physical Flattening:** Allocating a new 1D array of size $m \times n$ and copying all elements into it:
   - Takes $O(m \times n)$ time and $O(m \times n)$ auxiliary heap memory.
   - Completely violates the $O(\log(m \times n))$ time complexity constraint!
2. **Row-by-Row Search:** Performing binary search on every row takes $m \times O(\log n)$ time. For large $m$, this is significantly slower than the single global logarithmic binary search.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Virtual Coordinate Mapping Invariant:**
A matrix with $m$ rows and $n$ columns contains $M = m \times n$ elements indexed virtually from $0$ to $M - 1$.
Because each row is sorted and each row strictly exceeds the prior row, the virtual 1D sequence is **strictly monotonically ascending**:
$$\forall k_1 < k_2, \quad matrix[k_1 / n][k_1 \% n] \le matrix[k_2 / n][k_2 \% n]$$

To evaluate the element at virtual index `mid`:
$$\text{row} = mid / n, \quad \text{col} = mid \% n$$
$$\text{val} = matrix[\text{row}][\text{col}]$$
This coordinate mapping executes in a single CPU cycle ($O(1)$ time) with **zero auxiliary memory allocation**. Standard binary search applies directly.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
2D GRID TO VIRTUAL 1D MAPPING:
m = 3 rows, n = 4 cols => total elements = 3 * 4 = 12 (Indices 0 to 11)

Row 0: [  1,   3,   5,   7 ]  --> Indices  0,  1,  2,  3
Row 1: [ 10,  11,  16,  20 ]  --> Indices  4,  5,  6,  7
Row 2: [ 23,  30,  34,  60 ]  --> Indices  8,  9, 10, 11

VIRTUAL 1D ARRAY:
[ 1, 3, 5, 7, 10, 11, 16, 20, 23, 30, 34, 60 ]
  0  1  2  3   4   5   6   7   8   9  10  11

For mid = 6:
row = 6 / 4 = 1
col = 6 % 4 = 2
matrix[1][2] = 16
```

- `left`: Virtual index $0$.
- `right`: Virtual index $m \times n - 1$.
- `row = mid / n`, `col = mid % n`: $O(1)$ projection from 1D space into 2D memory.

#### 3.5 State Transition Triggers & Decision Gates
While $left \le right$:
1. `mid = left + (right - left) / 2`
2. `midVal = matrix[mid / n][mid % n]`
3. If `midVal == target` $\implies$ return `true`.
4. Else if `midVal < target` $\implies left = mid + 1$.
5. Else `right = mid - 1`.
6. Return `false` on loop exhaustion.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `matrix = [[1, 3, 5, 7], [10, 11, 16, 20], [23, 30, 34, 60]]`, `target = 3` ($m=3, n=4$)

| Iter | `left` | `right` | `mid` | `(row, col)` | `matrix[row][col]` | Comparison vs 3 | Action |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1** | 0 | 11 | 5 | $(5/4, 5\%4) = (1, 1)$ | 11 | $11 > 3$ | $right = mid - 1 = 4$ |
| **2** | 0 | 4 | 2 | $(2/4, 2\%4) = (0, 2)$ | 5 | $5 > 3$ | $right = mid - 1 = 1$ |
| **3** | 0 | 1 | 0 | $(0/4, 0\%4) = (0, 0)$ | 1 | $1 < 3$ | $left = mid + 1 = 1$ |
| **4** | 1 | 1 | 1 | $(1/4, 1\%4) = (0, 1)$ | 3 | $3 == 3$ | **Match found! Return `true`** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Virtual 1D Coordinate Mapping):** Strictly optimal. Achieves $O(\log(m \times n))$ runtime in a single unified loop with $O(1)$ memory.
- **Approach 2 (Two-Pass Binary Search):** Binary search column 0 to find the potential candidate row, then binary search within that row. Produces $O(\log m + \log n) = O(\log(m \times n))$ time. Modular, but requires writing two search loops.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Dimensions:** Extract $m = matrix.Length$, $n = matrix[0].Length$. Set `left = 0`, `right = m * n - 1`.
- **Step 2: Virtual 1D Search Loop:** While `left <= right`.
- **Step 3: Coordinate Mapping Gate:** Compute `row = mid / n`, `col = mid % n`.
- **Step 4: Comparison & Return:** Match returns `true`; loop termination returns `false`.

#### 4.3 Alternative Approaches Analysis
- **Two-Pass Binary Search:**
  1. Pass 1: Search row headers `matrix[r][0]` to find the row where `matrix[r][0] <= target <= matrix[r][n-1]`.
  2. Pass 2: Standard binary search within that row.
  Total comparisons: $\log_2 m + \log_2 n = \log_2(m \times n)$. Identical theoretical complexity, but double the code size.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Virtual 1D Mapping (Optimal) | Approach 2: Two-Pass (Row then Col) |
| :--- | :--- | :--- |
| **Time Complexity** | `O(log(m * n))` | `O(log m + log n) = O(log(m * n))` |
| **Auxiliary Space** | `O(1)` | `O(1)` |
| **Loop Count** | Exactly 1 loop | 2 sequential loops |
| **Memory Allocations** | Zero | Zero |
| **Arithmetic Operations** | Requires `/` and `%` on each iteration | Standard 1D index addition |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #47 - Search a 2D Matrix
// Core Pattern: Virtual 1D Coordinate Mapping over Monotonic Matrix
// Coordinate Invariant: row = mid / n, col = mid % n maps 1D index directly to 2D
// Complexity Guarantee: O(log(m * n)) strictly single pass with O(1) extra memory
// ============================================================================
```

#### Implementation 1: Virtual 1D Coordinate Mapping (Optimal `O(1)` Space)
```csharp
public class Solution
{
    public bool SearchMatrix(int[][] matrix, int target)
    {
        // Boundary Defense: Check for null or empty matrix
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0)
        {
            return false;
        }

        int m = matrix.Length;
        int n = matrix[0].Length;

        int left = 0;
        int right = m * n - 1; // Virtual 1D upper bound

        // Invariant: Target resides in virtual range [left, right] if present
        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            // Gate 1: O(1) Virtual 1D to 2D Coordinate Projection
            int row = mid / n;
            int col = mid % n;
            int midVal = matrix[row][col];

            // Gate 2: Binary search condition check
            if (midVal == target)
            {
                return true;
            }
            else if (midVal < target)
            {
                left = mid + 1; // Target lies in upper virtual half
            }
            else
            {
                right = mid - 1; // Target lies in lower virtual half
            }
        }

        // Virtual search space exhausted; target is absent
        return false;
    }
}
```

#### Implementation 2: Two-Pass Binary Search (Row Search + Column Search)
```csharp
public class SolutionTwoPass
{
    public bool SearchMatrix(int[][] matrix, int target)
    {
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return false;

        int m = matrix.Length;
        int n = matrix[0].Length;

        // Pass 1: Binary search to find the correct row
        int top = 0;
        int bottom = m - 1;
        int targetRow = -1;

        while (top <= bottom)
        {
            int midRow = top + (bottom - top) / 2;

            if (target >= matrix[midRow][0] && target <= matrix[midRow][n - 1])
            {
                targetRow = midRow;
                break;
            }
            else if (target < matrix[midRow][0])
            {
                bottom = midRow - 1;
            }
            else
            {
                top = midRow + 1;
            }
        }

        if (targetRow == -1) return false; // Target outside matrix range

        // Pass 2: Binary search within the identified target row
        int left = 0;
        int right = n - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (matrix[targetRow][mid] == target) return true;
            else if (matrix[targetRow][mid] < target) left = mid + 1;
            else right = mid - 1;
        }

        return false;
    }
}
```

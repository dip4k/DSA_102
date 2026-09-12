# Phase 20: Advanced / Senior-Level Patterns

> **Focus:** Monotonic Deque Queues, Binary Search on Partition Cuts, Bidirectional BFS, Grid Dijkstra / Binary Search on Answer, Longest Valid Parentheses Multi-Approach, State-Machine Regex Matching DP, Interval Dynamic Programming (Burst Balloons), and Binary Tree Pre-Order Serialization.  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 20 (Problems #106–#115)

---
## 106. Sliding Window Maximum (LeetCode #239)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#monotonic-queue` `#deque` `#sliding-window` `#O(n)` |
| **LeetCode Link** | [Sliding Window Maximum](https://leetcode.com/problems/sliding-window-maximum/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given an array of integers `nums`, and a sliding window of size `k` moving from the very left of the array to the very right. You can only see the `k` numbers in the window. Each step, the window slides right by one position. Return the max sliding window.
- **Strict Requirement:** Must achieve $O(N)$ linear time complexity.
- **Key Constraints:**
  - $1 \le nums.Length \le 10^5$.
  - $1 \le k \le nums.Length$.
  - $-10^4 \le nums[i] \le 10^4$.
- **Senior Edge Cases to Defend:**
  - $k = 1$: Window maximum is simply the array elements themselves.
  - $k = nums.Length$: Exactly 1 output element representing the global maximum.
  - Strictly decreasing array vs strictly increasing array (tests deque eviction behavior).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Monotonic Decreasing Deque: Maintain a double-ended queue of indices whose values are strictly decreasing. The front of the deque always holds the index of the maximum element for the active window.
- **Sample 1:**
  - **Input:** `nums = [1, 3, -1, -3, 5, 3, 6, 7], k = 3`
  - **Output:** `[3, 3, 5, 5, 6, 7]`
  - **Explanation:**
    - Window `[1, 3, -1]` $\implies$ max = 3
    - Window `[3, -1, -3]` $\implies$ max = 3
    - Window `[-1, -3, 5]` $\implies$ max = 5
    - Window `[-3, 5, 3]` $\implies$ max = 5
    - Window `[5, 3, 6]` $\implies$ max = 6
    - Window `[3, 6, 7]` $\implies$ max = 7

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a line of job applicants where your team window can hold $k$ candidates. A newly arriving candidate $B$ is both **younger** (will stay in the company longer) and **more qualified** (higher value $nums[i]$) than an existing candidate $A$ currently in the team.
Under this reality, candidate $A$ will **never** be the best person in the team for any current or future window because candidate $B$ outshines them and outlasts them!
Candidate $A$ is completely obsolete and can be eliminated immediately. By systematically popping all obsolete, weaker candidates from the back of our record book, the candidates naturally align in **strictly descending order of value**. The best candidate is always sitting right at the front of the line!

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force scan recalculates the maximum across the $k$ window elements at each of the $N - k + 1$ window positions:
$$T(N, k) = O((N - k + 1) \times k) = O(N \cdot k)$$
For $N = 10^5, k = 50,000$, $N \cdot k \approx 5 \times 10^9$ operations, causing catastrophic timeout.
A Max-Heap (PriorityQueue) takes $O(N \log k)$, but standard heaps do not support $O(1)$ arbitrary removals when elements exit the window. The Monotonic Deque achieves $O(N)$ amortized time because every element is added and removed at most once.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Monotonic Deque Invariant:**
Store array **indices** (not raw values) in a double-ended queue (`LinkedList<int>` or custom circular array buffer):
$$\forall j_1 < j_2 \in \text{deque}, \quad nums[j_1] > nums[j_2]$$
- **Dual Eviction Mechanism:**
  1. **Front Eviction (Expiry Gate):** Before recording the result, if `deque.First.Value <= i - k`, the front element has fallen outside the active window $[i - k + 1 \dots i]$. Pop it from the front!
  2. **Back Eviction (Obsolescence Gate):** Before inserting index $i$, while `deque.Count > 0` and `nums[deque.Last.Value] <= nums[i]`, pop from the back. These elements are permanently dominated by $nums[i]$.
- **Window Maximum Invariant:**
  After both evictions and inserting index $i$, `nums[deque.First.Value]` is mathematically guaranteed to be the maximum element in the active window.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
MONOTONIC DEQUE WINDOW ARCHITECTURE:
nums = [ 1 , 3 , -1 , -3 , 5 , 3 , 6 , 7 ],  k = 3
Active window [i - k + 1 ... i]

Deque state:
[ Front: Index of Window Max ] > [ Second Max ] > ... > [ Back: Newest Element ]

Front Eviction:  Drop if deque.First <= i - k (Out of window)
Back Eviction:   Drop while nums[deque.Last] <= nums[i] (Dominated)
Window Maximum:  nums[deque.First] (O(1) access)
```

- `i`: Current array scan cursor advancing from $0$ to $N - 1$.
- `deque`: Stores indices of potential maximum candidates in strictly decreasing value order.
- `result`: Array of size $N - k + 1$ recording window maxima once $i \ge k - 1$.

#### 3.5 State Transition Triggers & Decision Gates
For each index $i \in [0 \dots N - 1]$:
1. **Expiry Gate:** If `deque.Count > 0 && deque.First.Value <= i - k`, execute `deque.RemoveFirst()`.
2. **Obsolescence Gate:** While `deque.Count > 0 && nums[deque.Last.Value] <= nums[i]`, execute `deque.RemoveLast()`.
3. **Insertion Gate:** `deque.AddLast(i)`.
4. **Recording Gate:** If $i \ge k - 1$, record `result[i - k + 1] = nums[deque.First.Value]`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `nums = [1, 3, -1, -3, 5, 3, 6, 7]`, `k = 3`.

| $i$ | `nums[i]` | Front Eviction (`<= i - 3`) | Back Eviction (`<= nums[i]`) | Deque Indices (Values) | Result Recorded |
| :---: | :---: | :---: | :---: | :--- | :---: |
| **0** | 1 | None | None | `[0 (1)]` | — |
| **1** | 3 | None | Pop 0 (1 $\le$ 3) | `[1 (3)]` | — |
| **2** | -1 | None | None | `[1 (3), 2 (-1)]` | `result[0] = 3` |
| **3** | -3 | None | None | `[1 (3), 2 (-1), 3 (-3)]` | `result[1] = 3` |
| **4** | 5 | Pop 1 ($1 \le 4-3$) | Pop 3 (-3 $\le$ 5), Pop 2 (-1 $\le$ 5) | `[4 (5)]` | `result[2] = 5` |
| **5** | 3 | None | None | `[4 (5), 5 (3)]` | `result[3] = 5` |
| **6** | 6 | None | Pop 5 (3 $\le$ 6), Pop 4 (5 $\le$ 6) | `[6 (6)]` | `result[4] = 6` |
| **7** | 7 | None | Pop 6 (6 $\le$ 7) | `[7 (7)]` | `result[5] = 7` |

Final output: `[3, 3, 5, 5, 6, 7]`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Monotonic Deque):** Universal gold standard. $O(N)$ amortized time, $O(k)$ auxiliary space. Handles online streams where numbers arrive sequentially.
- **Approach 2 (Block Partition Prefix / Suffix Max):** Non-queue linear alternative. Divide array into fixed blocks of size $k$. Compute prefix-max and suffix-max within each block. Window max is $\max(\text{suffix}[i], \text{prefix}[i + k - 1])$. Extremely fast cache locality, but requires static offline array access.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Allocations:** Handle empty array or $k \le 0$. Allocate `result = new int[n - k + 1]`.
- **Step 2: Initialize Deque:** Create double-ended queue storing integer indices.
- **Step 3: Exploration Loop:** Advance $i$ from $0$ to $N - 1$. Apply expiry gate, then obsolescence gate, then push $i$.
- **Step 4: Window Output:** If $i \ge k - 1$, write `nums[deque.First.Value]` to result. Return `result`.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Block Partitioning ($O(N)$ Time, $O(N)$ Space):**
  - Partition array into blocks of length $k$.
  - Compute `left[i]`: max from block start to $i$.
  - Compute `right[i]`: max from block end down to $i$.
  - For any window $[i \dots i + k - 1]$: max is $\max(right[i], left[i + k - 1])$.
  - Zero deque node allocation overhead!

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Monotonic Deque (Standard) | Approach 2: Block Partition (Prefix/Suffix) |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(N)$ / $O(N)$ / $O(N)$ | $O(N)$ / $O(N)$ / $O(N)$ |
| **Auxiliary Space** | $O(k)$ deque storage | $O(N)$ prefix/suffix arrays |
| **Output Space** | $O(N - k + 1)$ array | $O(N - k + 1)$ array |
| **Cache Locality** | Moderate (node pointer links) | Optimal (sequential flat arrays) |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | High (processes stream online) | Low (requires full array upfront) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #106 - Sliding Window Maximum
// Core Pattern: Monotonic Decreasing Deque / Dual Eviction Invariant
// Primary Invariant: deque stores indices with values in strictly descending order.
// Front Eviction: Discards expired indices (index <= i - k).
// Back Eviction: Discards dominated indices (nums[back] <= nums[i]).
// Complexity Target: O(N) amortized linear time, O(k) auxiliary space.
// ============================================================================
```

#### Implementation 1: Monotonic Deque (Production Standard)
```csharp
public class Solution
{
    public int[] MaxSlidingWindow(int[] nums, int k)
    {
        // Guard Clauses
        if (nums == null || nums.Length == 0 || k <= 0)
        {
            return Array.Empty<int>();
        }

        int n = nums.Length;
        int[] result = new int[n - k + 1];

        // LinkedList acts as a double-ended queue storing indices
        var deque = new LinkedList<int>();

        for (int i = 0; i < n; i++)
        {
            // GATE 1: Evict element at front if it has expired out of the active window [i - k + 1 .. i]
            if (deque.Count > 0 && deque.First.Value <= i - k)
            {
                deque.RemoveFirst();
            }

            // GATE 2: Maintain strict monotonic decreasing order by evicting smaller elements from back
            // Any element smaller than nums[i] will expire before or with nums[i] and can never be window max
            while (deque.Count > 0 && nums[deque.Last.Value] <= nums[i])
            {
                deque.RemoveLast();
            }

            // GATE 3: Enqueue current index
            deque.AddLast(i);

            // GATE 4: Record window maximum once the first full window of size k is formed
            if (i >= k - 1)
            {
                result[i - k + 1] = nums[deque.First.Value];
            }
        }

        return result;
    }
}
```

#### Implementation 2: Block Partition Prefix / Suffix Max ($O(N)$ Space)
```csharp
public class SolutionBlockPartition
{
    public int[] MaxSlidingWindow(int[] nums, int k)
    {
        if (nums == null || nums.Length == 0 || k <= 0) return Array.Empty<int>();

        int n = nums.Length;
        int[] result = new int[n - k + 1];

        int[] leftMax = new int[n];
        int[] rightMax = new int[n];

        // Fill leftMax: running maximum from block start to right
        // Fill rightMax: running maximum from block end to left
        for (int i = 0; i < n; i++)
        {
            // Block start boundary
            if (i % k == 0)
                leftMax[i] = nums[i];
            else
                leftMax[i] = Math.Max(leftMax[i - 1], nums[i]);

            int j = n - 1 - i;
            // Block end boundary
            if ((j + 1) % k == 0 || j == n - 1)
                rightMax[j] = nums[j];
            else
                rightMax[j] = Math.Max(rightMax[j + 1], nums[j]);
        }

        // Window maximum spanning from i to i + k - 1
        for (int i = 0; i <= n - k; i++)
        {
            result[i] = Math.Max(rightMax[i], leftMax[i + k - 1]);
        }

        return result;
    }
}
```

---

## 107. Median of Two Sorted Arrays (LeetCode #4)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | 💡 Advanced |
| **Pattern Tags** | `#binary-search` `#partition-cut` `#O(log(min(m,n)))` |
| **LeetCode Link** | [Median of Two Sorted Arrays](https://leetcode.com/problems/median-of-two-sorted-arrays/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given two sorted arrays `nums1` and `nums2` of size $m$ and $n$ respectively, return the median of the two sorted arrays. The overall run time complexity should be $O(\log (m + n))$.
- **Key Constraints:**
  - $nums1.Length == m, nums2.Length == n$.
  - $0 \le m, n \le 1000$.
  - $1 \le m + n \le 2000$.
  - $-10^6 \le nums1[i], nums2[i] \le 10^6$.
- **Senior Edge Cases to Defend:**
  - One array completely empty ($m = 0$ or $n = 0$).
  - Partition cut at extreme boundaries ($i = 0$ or $i = m$). Handled via $\pm \infty$ sentinels.
  - Ensuring search is strictly performed on the shorter array to avoid out-of-bounds indexing in the longer array.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Binary Search on Partition Cut: Binary search the partition cut in the shorter array such that the combined left half contains $(m + n + 1) / 2$ elements and every element in the combined left half is $\le$ every element in the combined right half.
- **Sample 1:**
  - **Input:** `nums1 = [1, 3], nums2 = [2]`
  - **Output:** `2.0`
  - **Explanation:** Merged array = `[1, 2, 3]`, median is 2.0.
- **Sample 2:**
  - **Input:** `nums1 = [1, 2], nums2 = [3, 4]`
  - **Output:** `2.5`
  - **Explanation:** Merged array = `[1, 2, 3, 4]`, median is $(2 + 3) / 2 = 2.5$.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine dropping a single vertical guillotine blade across two parallel sorted conveyor belts $A$ and $B$.
You want the blade to divide the total collection of $m + n$ items into two equal halves: a Left Half and a Right Half.
- The blade cuts through belt $A$ at position $i$ (leaving $i$ items on the left) and belt $B$ at position $j$ (leaving $j$ items on the left).
- To balance the halves, the total left items must equal $half = (m + n + 1) / 2$. Therefore, once you choose cut $i$, cut $j$ is **uniquely fixed**: $j = half - i$.
- The cut is valid if and only if no item on the left is larger than any item on the right! Because each array is already internally sorted, this requires verifying only two cross-boundary inequalities:
  $$A[i - 1] \le B[j] \quad \land \quad B[j - 1] \le A[i]$$
Because the condition is monotonic, we can find the perfect cut $i$ via **Binary Search**!

#### 3.2 The Naive Bottleneck & Redundant Computation
Merging the two sorted arrays with two pointers takes $O(m + n)$ time and $O(m + n)$ space (or $O(1)$ space if counting up to the median index).
For large arrays, linear scanning wastes computation inspecting thousands of elements far from the median. Binary searching the cut space in the shorter array reduces runtime to $O(\log(\min(m, n)))$, taking at most $\approx 10$ iterations for $m = 1000$.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Cross-Boundary Partition Invariant:**
Let cut $i \in [0, m]$ in $A$, and $j = (m + n + 1) / 2 - i$ in $B$.
Define four boundary values using sentinels for edge cuts:
$$\begin{aligned} A_{left} &= (i == 0) \;?\; -\infty \;:\; A[i - 1] \\ A_{right} &= (i == m) \;?\; +\infty \;:\; A[i] \\ B_{left} &= (j == 0) \;?\; -\infty \;:\; B[j - 1] \\ B_{right} &= (j == n) \;?\; +\infty \;:\; B[j] \end{aligned}$$
- **Invariant:** A valid partition satisfies:
  $$A_{left} \le B_{right} \quad \land \quad B_{left} \le A_{right}$$
- **Binary Search Direction Gate:**
  - If $A_{left} > B_{right}$: Cut $i$ is too far to the right in $A$. Shift binary search left: `right = i - 1`.
  - If $B_{left} > A_{right}$: Cut $i$ is too far to the left in $A$. Shift binary search right: `left = i + 1`.
- **Median Resolution:**
  - If total elements $m + n$ is odd: Median is $\max(A_{left}, B_{left})$.
  - If total elements $m + n$ is even: Median is $\frac{\max(A_{left}, B_{left}) + \min(A_{right}, B_{right})}{2.0}$.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
GUILLOTINE PARTITION CUT ARCHITECTURE:
Array A: [ A[0] ... A[i-1] ] | [ A[i] ... A[m-1] ]
         └──────┬──────────┘   └───────┬─────────┘
             A_left                  A_right

Array B: [ B[0] ... B[j-1] ] | [ B[j] ... B[n-1] ]
         └──────┬──────────┘   └───────┬─────────┘
             B_left                  B_right
         <-- Combined Left -->   <-- Combined Right -->

Valid Cut Invariant: A_left <= B_right  AND  B_left <= A_right
```

- `i`: Partition cut in shorter array $A$ ($0 \le i \le m$).
- `j`: Partition cut in longer array $B$: $j = (m + n + 1) / 2 - i$.
- `A_left, A_right, B_left, B_right`: The four pivotal boundary elements surrounding the cut.

#### 3.5 State Transition Triggers & Decision Gates
While `left <= right`:
1. **Compute Cuts:** `i = left + (right - left) / 2`, `j = (m + n + 1) / 2 - i`.
2. **Sentinel Extraction:** Safely retrieve $A_{left}, A_{right}, B_{left}, B_{right}$ using `int.MinValue` and `int.MaxValue`.
3. **Validation Gate:**
   - If $A_{left} \le B_{right}$ AND $B_{left} \le A_{right}$: Valid partition found! Compute and return median.
   - Else if $A_{left} > B_{right}$: `right = i - 1`.
   - Else: `left = i + 1`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `nums1 = [1, 3]`, `nums2 = [2]`. $m = 2, n = 1$.
Swap arrays so $A$ is shorter: $A = [2]$ ($m = 1$), $B = [1, 3]$ ($n = 2$).
Total length = 3 (odd). $half = (1 + 2 + 1) / 2 = 2$.
Search range: `left = 0, right = 1`.

| Iteration | `left` | `right` | Cut $i$ in $A$ | Cut $j$ in $B$ | $A_{left}, A_{right}$ | $B_{left}, B_{right}$ | Invariant Check | Action |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :--- |
| **1** | 0 | 1 | 0 | $2 - 0 = 2$ | $-\infty, 2$ | $3, +\infty$ | $B_{left} (3) > A_{right} (2)$ | Cut $i$ too small! `left = 0 + 1 = 1` |
| **2** | 1 | 1 | 1 | $2 - 1 = 1$ | $2, +\infty$ | $1, 3$ | $A_{left} (2) \le B_{right} (3)$ and $B_{left} (1) \le A_{right} (\infty)$ | **Valid Partition!** |

Total is odd $\implies$ Median $= \max(A_{left}, B_{left}) = \max(2, 1) = 2.0$.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Binary Search on Shorter Partition Cut):** Strictly optimal. $O(\log(\min(m, n)))$ runtime, $O(1)$ auxiliary memory. Mandatory in interviews that demand sub-linear $O(\log(m + n))$ complexity.
- **Approach 2 (K-th Element Selection via Recursive Binary Search):** Generalizes median finding to finding the $k$-th smallest element in two sorted arrays by discarding $k/2$ elements per step in $O(\log(m + n))$ time.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Guarantee Shorter Array:** If $nums1.Length > nums2.Length$, swap parameters.
- **Step 2: Binary Search Bounds:** Set `left = 0, right = m`.
- **Step 3: Mid Calculation & Boundary Extraction:** Compute $i$ and $j$, extract boundary values with `int.MinValue` and `int.MaxValue`.
- **Step 4: Convergence & Resolution:** Return median on valid cut; otherwise adjust binary search bounds.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: K-th Element Divide and Conquer:**
  - Define `FindKth(nums1, i, nums2, j, k)`.
  - Compare $nums1[i + k/2 - 1]$ with $nums2[j + k/2 - 1]$.
  - Discard the smaller half ($k/2$ elements) and recurse with $k = k - k/2$.
  - Time is $O(\log(m + n))$.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Partition Cut Binary Search (Optimal) | Approach 2: K-th Element Recursive Discard |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(\log(\min(m, n)))$ | $O(\log(m + n))$ |
| **Auxiliary Space** | $O(1)$ scalar variables | $O(\log(m + n))$ call stack depth |
| **Output Space** | $O(1)$ double floating-point scalar | $O(1)$ double floating-point scalar |
| **Cache Locality** | Pure CPU register execution | Moderate |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | Low | Low |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #107 - Median of Two Sorted Arrays
// Core Pattern: Binary Search on Monotonic Partition Cut
// Primary Invariant: Combined Left half has (m + n + 1)/2 elements; max(Left) <= min(Right).
// Shorter Array Defense: Always binary search shorter array to guarantee j is non-negative and in-bounds.
// Sentinel Defense: int.MinValue and int.MaxValue cleanly handle cuts at array extremes (0 or m).
// ============================================================================
```

#### Implementation 1: Binary Search on Shorter Partition Cut (Production Optimal)
```csharp
public class Solution
{
    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        // Guard Clauses
        if (nums1 == null) nums1 = Array.Empty<int>();
        if (nums2 == null) nums2 = Array.Empty<int>();

        // DEFENSE: Guarantee nums1 is the shorter array
        // 1. Minimizes binary search iterations to O(log(min(M, N)))
        // 2. Guarantees calculated cut j in nums2 is always valid and >= 0
        if (nums1.Length > nums2.Length)
        {
            return FindMedianSortedArrays(nums2, nums1);
        }

        int m = nums1.Length;
        int n = nums2.Length;

        int left = 0;
        int right = m;
        int halfLen = (m + n + 1) / 2;

        while (left <= right)
        {
            // Defensive mid calculation
            int i = left + (right - left) / 2;
            int j = halfLen - i;

            // Extract boundary elements using sentinels for extreme cuts
            int aLeft = (i == 0) ? int.MinValue : nums1[i - 1];
            int aRight = (i == m) ? int.MaxValue : nums1[i];

            int bLeft = (j == 0) ? int.MinValue : nums2[j - 1];
            int bRight = (j == n) ? int.MaxValue : nums2[j];

            // INVARIANT VALIDATION:
            // Every element in combined Left half must be <= every element in combined Right half
            if (aLeft <= bRight && bLeft <= aRight)
            {
                // Odd total length: Median is the maximum element of the left half
                if (((m + n) & 1) == 1)
                {
                    return Math.Max(aLeft, bLeft);
                }

                // Even total length: Median is the average of max(Left) and min(Right)
                return (Math.Max(aLeft, bLeft) + Math.Min(aRight, bRight)) / 2.0;
            }
            else if (aLeft > bRight)
            {
                // aLeft is too large; move cut i to the left
                right = i - 1;
            }
            else
            {
                // bLeft is too large (aRight is too small); move cut i to the right
                left = i + 1;
            }
        }

        throw new ArgumentException("Input arrays are not sorted.");
    }
}
```

#### Implementation 2: K-th Element Recursive Discard ($O(\log(m + n))$)
```csharp
public class SolutionKthElement
{
    public double FindMedianSortedArrays(int[] nums1, int[] nums2)
    {
        int total = nums1.Length + nums2.Length;

        if ((total & 1) == 1)
        {
            return FindKth(nums1, 0, nums2, 0, (total + 1) / 2);
        }
        else
        {
            int mid1 = FindKth(nums1, 0, nums2, 0, total / 2);
            int mid2 = FindKth(nums1, 0, nums2, 0, (total / 2) + 1);
            return (mid1 + mid2) / 2.0;
        }
    }

    private static int FindKth(int[] a, int aStart, int[] b, int bStart, int k)
    {
        // If a is exhausted, return k-th element in b
        if (aStart >= a.Length) return b[bStart + k - 1];
        if (bStart >= b.Length) return a[aStart + k - 1];

        // Base case: 1st smallest element is the smaller of heads
        if (k == 1) return Math.Min(a[aStart], b[bStart]);

        int halfK = k / 2;
        int aMid = (aStart + halfK - 1 < a.Length) ? a[aStart + halfK - 1] : int.MaxValue;
        int bMid = (bStart + halfK - 1 < b.Length) ? b[bStart + halfK - 1] : int.MaxValue;

        // Discard the smaller half
        if (aMid < bMid)
        {
            return FindKth(a, aStart + halfK, b, bStart, k - halfK);
        }
        else
        {
            return FindKth(a, aStart, b, bStart + halfK, k - halfK);
        }
    }
}
```

---

## 108. Largest Rectangle in Histogram (LeetCode #84)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#monotonic-stack` `#histogram` `#boundary-expansion` |
| **LeetCode Link** | [Largest Rectangle in Histogram](https://leetcode.com/problems/largest-rectangle-in-histogram/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array of integers `heights` representing the histogram's bar height where the width of each bar is 1, return the area of the largest rectangle in the histogram.
- **Key Constraints:**
  - $1 \le heights.Length \le 10^5$.
  - $0 \le heights[i] \le 10^4$.
- **Senior Edge Cases to Defend:**
  - Monotonically increasing heights (e.g. `[1, 2, 3, 4, 5]`): Must flush remaining elements in stack after array traversal.
  - All heights equal: Handled cleanly by $\ge$ popping.
  - Histogram containing zeroes: Zero-height bars act as walls separating rectangles.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Monotonic Increasing Stack: For each bar $h$, its maximum rectangular area extends between the first strictly smaller bar to its left and the first strictly smaller bar to its right.
- **Sample 1:**
  - **Input:** `heights = [2, 1, 5, 6, 2, 3]`
  - **Output:** `10`
  - **Explanation:** The largest rectangle is formed by bars at indices 2 and 3 with height 5: $5 \times 2 = 10$.
- **Sample 2:**
  - **Input:** `heights = [2, 4]`
  - **Output:** `4`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine an architectural row of stone pillars of varying heights. You want to construct the largest possible rectangular billboard suspended between the pillars.
For any pillar $mid$ of height $H$, how wide can a billboard of height $H$ stretch?
- It can extend to the left until it hits the first pillar shorter than $H$.
- It can extend to the right until it hits the first pillar shorter than $H$.
If we maintain a **Monotonic Increasing Stack** of pillar indices, the moment an incoming pillar $i$ is shorter than the top of our stack, pillar $i$ forms the **right boundary wall** for the stack top! And because the stack is increasing, the pillar immediately beneath the stack top is its **left boundary wall**!
Thus, popping a bar from the stack resolves both its left and right limits simultaneously in $O(1)$!

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force algorithm tests every bar $i$, expanding outward to left and right until hitting a shorter bar:
$$T(N) = \sum_{i=0}^{N-1} O(N) = O(N^2)$$
For $N = 10^5$, $N^2 = 10^{10}$ operations, causing guaranteed timeout. Monotonic stack eliminates redundant boundary scans by resolving boundaries in a single $O(N)$ pass.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Concurrent Dual Boundary Resolution:**
Maintain a stack storing indices in strictly increasing order of heights:
$$\text{stack}: \quad heights[s_0] < heights[s_1] < \dots < heights[s_k]$$
When an incoming bar $heights[i]$ is shorter than $heights[mid]$ (where $mid = stack.Pop()$):
1. **Right Smaller Boundary:** Strictly index $i$ (the bar that triggered the pop).
2. **Left Smaller Boundary:** Strictly $stack.Peek()$ (the index immediately beneath $mid$ in the stack).
3. **Width Calculation:**
   $$width = i - \text{stack.Peek()} - 1$$
4. **Area:**
   $$area = heights[mid] \times width$$
- **Sentinel Invariant:** Initializing the stack with sentinel `-1` cleanly defines the left boundary when a bar extends all the way to the start of the array.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
MONOTONIC INCREASING STACK RESOLUTION:
heights = [ 2 , 1 , 5 , 6 , 2 , 3 ]
                      ▲   ▲
                     mid  i
Stack before popping: [ -1 , 1 (ht 1) , 2 (ht 5) , 3 (ht 6) ]
Incoming i = 4 (ht 2) < heights[3] (ht 6):
Pop mid = 3 (ht 6):
  Right boundary = i = 4
  Left boundary  = stack.Peek() = 2 (ht 5)
  Width          = 4 - 2 - 1 = 1
  Area           = 6 * 1 = 6
```

- `i`: Current array scan cursor representing the right boundary of popped bars.
- `mid`: The popped index whose rectangular area is being finalized.
- `stack.Peek()`: The left boundary index immediately strictly smaller than $heights[mid]$.
- `maxArea`: Running maximum rectangle area seen so far.

#### 3.5 State Transition Triggers & Decision Gates
For each index $i \in [0 \dots N - 1]$:
1. **Popping Gate:** While `stack.Peek() != -1 && heights[stack.Peek()] >= heights[i]`:
   - `mid = stack.Pop()`.
   - `height = heights[mid]`.
   - `width = i - stack.Peek() - 1`.
   - `maxArea = Math.Max(maxArea, height * width)`.
2. **Push Gate:** `stack.Push(i)`.
3. **Flush Gate:** After loop, flush remaining indices in stack using $N$ as the right boundary.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `heights = [2, 1, 5, 6, 2, 3]`. Stack initialized with `[-1]`.

| Step | $i$ | `heights[i]` | Stack State (Indices) | Popped `mid` | Width Calculation | Area | `maxArea` |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| **1** | 0 | 2 | `[-1, 0]` | None | — | — | 0 |
| **2** | 1 | 1 | Pop 0 | 0 (ht 2) | $1 - (-1) - 1 = 1$ | $2 \times 1 = 2$ | 2 |
| | | | `[-1, 1]` | Push 1 | — | — | 2 |
| **3** | 2 | 5 | `[-1, 1, 2]` | None | — | — | 2 |
| **4** | 3 | 6 | `[-1, 1, 2, 3]`| None | — | — | 2 |
| **5** | 4 | 2 | Pop 3 | 3 (ht 6) | $4 - 2 - 1 = 1$ | $6 \times 1 = 6$ | 6 |
| | | | Pop 2 | 2 (ht 5) | $4 - 1 - 1 = 2$ | $5 \times 2 = 10$ | **10** |
| | | | `[-1, 1, 4]` | Push 4 | — | — | 10 |
| **6** | 5 | 3 | `[-1, 1, 4, 5]`| None | — | — | 10 |
| **Flush**| — | $N=6$ | Pop 5 | 5 (ht 3) | $6 - 4 - 1 = 1$ | $3 \times 1 = 3$ | 10 |
| | | | Pop 4 | 4 (ht 2) | $6 - 1 - 1 = 4$ | $2 \times 4 = 8$ | 10 |
| | | | Pop 1 | 1 (ht 1) | $6 - (-1) - 1 = 6$ | $1 \times 6 = 6$ | 10 |

Final maximum rectangle area: `10`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Monotonic Stack with Sentinel):** Industry gold standard. Single pass, $O(N)$ linear time, $O(N)$ stack memory. Computes both left and right boundaries concurrently.
- **Approach 2 (Three-Pass Array Boundaries):** Precomputes `leftSmaller[i]` and `rightSmaller[i]` arrays using two monotonic passes, then computes areas in a third pass. Conceptually modular, but requires $3\times$ array allocations.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Sentinel:** Check empty array. Allocate `Stack<int>`, push sentinel `-1`.
- **Step 2: Exploration Loop:** Advance $i$ from $0$ to $N - 1$.
- **Step 3: Popping & Area Maximization:** While stack top is taller than current bar, pop, calculate width using `i - stack.Peek() - 1`, maximize area.
- **Step 4: Stack Flush & Return:** Flush remaining elements with right boundary $N$. Return `maxArea`.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Precomputed Boundary Arrays:**
  - Allocate `int[] left = new int[N]` and `int[] right = new int[N]`.
  - Pass 1: Monotonic stack left-to-right to fill `left` smaller boundaries.
  - Pass 2: Monotonic stack right-to-left to fill `right` smaller boundaries.
  - Pass 3: Loop $i \in [0 \dots N-1]$, area $= heights[i] \times (right[i] - left[i] - 1)$.
  - Takes $O(N)$ time and $O(N)$ space with 3 passes.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Monotonic Stack with Sentinel (Optimal) | Approach 2: Three-Pass Boundary Arrays |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(N)$ single pass | $O(N)$ three passes |
| **Auxiliary Space** | $O(N)$ single stack | $O(N)$ two boundary arrays + stack |
| **Output Space** | $O(1)$ scalar integer | $O(1)$ scalar integer |
| **Cache Locality** | High (single pass) | Moderate (multiple array passes) |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | High | Low |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #108 - Largest Rectangle in Histogram
// Core Pattern: Monotonic Increasing Stack / Dual Boundary Resolution
// Primary Invariant: Stack stores indices in strictly increasing order of heights.
// Right Boundary: Current index i when heights[i] < heights[mid].
// Left Boundary: New stack top stack.Peek() after popping mid.
// Sentinel Defense: Sentinel -1 handles rectangles extending to index 0 cleanly.
// ============================================================================
```

#### Implementation 1: Monotonic Stack with Sentinel (Production Standard)
```csharp
public class Solution
{
    public int LargestRectangleArea(int[] heights)
    {
        // Guard Clause
        if (heights == null || heights.Length == 0)
        {
            return 0;
        }

        int n = heights.Length;
        var stack = new Stack<int>();

        // SENTINEL DEFENSE:
        // Push -1 as the virtual left boundary wall preceding index 0
        stack.Push(-1);

        int maxArea = 0;

        for (int i = 0; i < n; i++)
        {
            // INVARIANT GATE: Maintain strictly increasing heights in stack
            // When current bar is shorter, it serves as the RIGHT boundary for stack top
            while (stack.Peek() != -1 && heights[stack.Peek()] >= heights[i])
            {
                int mid = stack.Pop();
                int height = heights[mid];

                // Width is distance between right boundary (i) and left boundary (stack.Peek())
                int width = i - stack.Peek() - 1;

                maxArea = Math.Max(maxArea, height * width);
            }

            stack.Push(i);
        }

        // FLUSH GATE: Process remaining bars in stack using n as the virtual right boundary
        while (stack.Peek() != -1)
        {
            int mid = stack.Pop();
            int height = heights[mid];
            int width = n - stack.Peek() - 1;

            maxArea = Math.Max(maxArea, height * width);
        }

        return maxArea;
    }
}
```

#### Implementation 2: Three-Pass Precomputed Boundary Arrays
```csharp
public class SolutionThreePass
{
    public int LargestRectangleArea(int[] heights)
    {
        if (heights == null || heights.Length == 0) return 0;

        int n = heights.Length;
        int[] left = new int[n];
        int[] right = new int[n];
        var stack = new Stack<int>();

        // Pass 1: Find nearest smaller element on the left
        for (int i = 0; i < n; i++)
        {
            while (stack.Count > 0 && heights[stack.Peek()] >= heights[i])
            {
                stack.Pop();
            }
            left[i] = stack.Count == 0 ? -1 : stack.Peek();
            stack.Push(i);
        }

        stack.Clear();

        // Pass 2: Find nearest smaller element on the right
        for (int i = n - 1; i >= 0; i--)
        {
            while (stack.Count > 0 && heights[stack.Peek()] >= heights[i])
            {
                stack.Pop();
            }
            right[i] = stack.Count == 0 ? n : stack.Peek();
            stack.Push(i);
        }

        // Pass 3: Calculate maximum rectangle area
        int maxArea = 0;
        for (int i = 0; i < n; i++)
        {
            int width = right[i] - left[i] - 1;
            maxArea = Math.Max(maxArea, heights[i] * width);
        }

        return maxArea;
    }
}
```

---

## 109. Trapping Rain Water (LeetCode #42)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#two-pointers` `#monotonic-stack` `#dynamic-programming` |
| **LeetCode Link** | [Trapping Rain Water](https://leetcode.com/problems/trapping-rain-water/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given $n$ non-negative integers representing an elevation map where the width of each bar is 1, compute how much water it can trap after raining.
- **Key Constraints:**
  - $n == height.Length \in [1, 2 \times 10^4]$.
  - $0 \le height[i] \le 10^5$.
- **Senior Edge Cases to Defend:**
  - Array length $< 3$: Cannot trap any water (requires at least 2 boundaries and 1 basin).
  - Monotonically increasing or decreasing heights (traps 0 water).
  - All bars of equal height (traps 0 water).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Boundary Bottleneck Invariant: Water trapped at column $i$ is strictly $\max(0, \min(leftMax, rightMax) - height[i])$. Using two opposing pointers converging inward, we advance whichever boundary is shorter in $O(N)$ time and $O(1)$ space.
- **Sample 1:**
  - **Input:** `height = [0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1]`
  - **Output:** `6`
- **Sample 2:**
  - **Input:** `height = [4, 2, 0, 3, 2, 5]`
  - **Output:** `9`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a mountain canyon between two great mountain ridges.
- When rain falls into the valley, water pools up to the elevation of the **lower** of the two surrounding ridges.
- You place two hydraulic surveyor flags at the opposite ends of the canyon: `left` at index 0 and `right` at index $N - 1$.
- As they march toward each other, they track the highest mountain peaks seen so far behind them: `leftMax` and `rightMax`.
- If the left surveyor sees a peak of height 2 (`leftMax = 2`), but the right surveyor sees a peak of height 5 (`rightMax = 5`), the water level above the left surveyor's current position can **never exceed 2**, regardless of what uncharted mountains lie in the intermediate valley!
- Because the lower boundary dictates the absolute water ceiling, we can safely compute trapped water at the lower boundary immediately and advance that surveyor inward.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force scan calculates the max height to the left and max height to the right for each bar independently:
$$T(N) = \sum_{i=0}^{N-1} O(N) = O(N^2)$$
Precomputing prefix and suffix max arrays eliminates redundant scans in $O(N)$ time, but consumes $O(N)$ auxiliary heap memory. The Two-Pointer approach eliminates memory allocation entirely, executing in $O(N)$ time and $O(1)$ space.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Two-Pointer Boundary Invariant:**
Let $leftMax = \max_{0 \le k \le L} height[k]$ and $rightMax = \max_{R \le k < N} height[k]$.
At any point where $height[L] \le height[R]$:
1. We know with mathematical certainty that the true global right maximum for cell $L$ is $\ge height[R] \ge height[L]$.
2. Therefore, the limiting bottleneck for cell $L$ is strictly $leftMax$:
   $$\min(leftMax, \text{trueRightMax}) = leftMax$$
3. We can immediately compute water trapped at column $L$:
   $$water = \max(0, leftMax - height[L])$$
4. Advance $L \to L + 1$. Symmetrically, if $height[R] < height[L]$, resolve column $R$ and decrement $R \to R - 1$.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
TWO-POINTER HYDRAULIC CONVERGENCE:
leftMax = 2                                    rightMax = 5
  ▼                                                 ▼
[ 0 , 1 , 0 , 2 ]  ... [ Unexplored Basin ] ... [ 2 , 1 , 3 , 5 ]
              ▲                                   ▲
           left (ht 2)                        right (ht 5)

Invariant: If height[left] <= height[right]:
Water at left is bounded by leftMax; advance left++
Else:
Water at right is bounded by rightMax; advance right--
```

- `left, right`: Opposing cursors converging inward from the array boundaries.
- `leftMax`: Running supremum height observed from index $0$ to `left`.
- `rightMax`: Running supremum height observed from index $N - 1$ down to `right`.
- **Invariant:** At every iteration, exactly one boundary element's trapped water is permanently resolved.

#### 3.5 State Transition Triggers & Decision Gates
While `left < right`:
1. **Boundary Gate:**
   - If `height[left] <= height[right]`:
     - If `height[left] >= leftMax`: update `leftMax = height[left]`.
     - Else: add `leftMax - height[left]` to `totalWater`.
     - Advance `left++`.
   - Else (`height[right] < height[left]`):
     - If `height[right] >= rightMax`: update `rightMax = height[right]`.
     - Else: add `rightMax - height[right]` to `totalWater`.
     - Decrement `right--`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `height = [4, 2, 0, 3, 2, 5]`.
Initial: `left = 0, right = 5, leftMax = 0, rightMax = 0, totalWater = 0`.

| Step | `left` (val) | `right` (val) | Condition | `leftMax` | `rightMax` | Water Added | `totalWater` | Pointer Shift |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :--- |
| **1** | 0 (4) | 5 (5) | $4 \le 5$ | 4 | 0 | $0$ (new peak) | 0 | `left -> 1` |
| **2** | 1 (2) | 5 (5) | $2 \le 5$ | 4 | 0 | $4 - 2 = 2$ | 2 | `left -> 2` |
| **3** | 2 (0) | 5 (5) | $0 \le 5$ | 4 | 0 | $4 - 0 = 4$ | 6 | `left -> 3` |
| **4** | 3 (3) | 5 (5) | $3 \le 5$ | 4 | 0 | $4 - 3 = 1$ | 7 | `left -> 4` |
| **5** | 4 (2) | 5 (5) | $2 \le 5$ | 4 | 0 | $4 - 2 = 2$ | 9 | `left -> 5` |
| **End**| 5 (5) | 5 (5) | `left == right` | Loop terminates | — | — | **9** | Return 9 |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Two Pointers):** Production standard. Single pass, $O(N)$ runtime, strictly $O(1)$ auxiliary space.
- **Approach 2 (Monotonic Stack Horizontal Slab Fill):** Fills water horizontally layer by layer rather than vertically column by column. Useful when understanding contour line problems or physical reservoir draining.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** If $height.Length < 3$, return 0. Initialize `left = 0, right = N - 1, leftMax = 0, rightMax = 0`.
- **Step 2: Converging Loop:** While `left < right`, test `height[left] <= height[right]`.
- **Step 3: Invariant Maintenance:** Update running max or accumulate water difference. Advance active pointer.
- **Step 4: Resolution & Return:** Return `totalWater`.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Monotonic Stack (Horizontal Slabs):**
  - Maintain decreasing stack of indices.
  - When $height[i] > height[stack.Peek()]$, pop $bottom = stack.Pop()$.
  - If stack non-empty: $boundedHeight = \min(height[i], height[stack.Peek()]) - height[bottom]$.
  - $distance = i - stack.Peek() - 1$.
  - Add $distance \times boundedHeight$ to water.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Two Pointers (Optimal) | Approach 2: Monotonic Stack (Horizontal Slabs) |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(N)$ / $O(N)$ / $O(N)$ | $O(N)$ / $O(N)$ / $O(N)$ |
| **Auxiliary Space** | $O(1)$ scalar variables | $O(N)$ stack storage |
| **Output Space** | $O(1)$ scalar integer | $O(1)$ scalar integer |
| **Cache Locality** | High (converging array reads) | Moderate (stack operations) |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | Low (requires two ends) | High (single forward pass) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #109 - Trapping Rain Water
// Core Pattern: Two-Pointer Boundary Invariant / O(1) Space Compression
// Primary Invariant: If height[left] <= height[right], water at left is strictly bounded by leftMax.
// Space Defense: Two opposing pointers eliminate O(N) prefix and suffix max arrays.
// Boundary Defense: height.Length < 3 immediately returns 0.
// ============================================================================
```

#### Implementation 1: Two Pointers (Production Standard)
```csharp
public class Solution
{
    public int Trap(int[] height)
    {
        // Guard Clause: Minimum 3 bars required to trap water (2 boundaries + 1 basin)
        if (height == null || height.Length < 3)
        {
            return 0;
        }

        int left = 0;
        int right = height.Length - 1;

        int leftMax = 0;
        int rightMax = 0;
        int totalWater = 0;

        while (left < right)
        {
            // INVARIANT GATE:
            // The water trapped is dictated by the smaller boundary.
            // If height[left] <= height[right], we know with certainty that the right boundary
            // is >= leftMax, meaning water above column 'left' depends solely on leftMax!
            if (height[left] <= height[right])
            {
                if (height[left] >= leftMax)
                {
                    leftMax = height[left]; // New left mountain peak
                }
                else
                {
                    totalWater += leftMax - height[left]; // Water pooled above left column
                }
                left++;
            }
            else
            {
                if (height[right] >= rightMax)
                {
                    rightMax = height[right]; // New right mountain peak
                }
                else
                {
                    totalWater += rightMax - height[right]; // Water pooled above right column
                }
                right--;
            }
        }

        return totalWater;
    }
}
```

#### Implementation 2: Monotonic Stack (Horizontal Slab Filling)
```csharp
public class SolutionStack
{
    public int Trap(int[] height)
    {
        if (height == null || height.Length < 3) return 0;

        var stack = new Stack<int>();
        int totalWater = 0;

        for (int i = 0; i < height.Length; i++)
        {
            // When current bar is taller than stack top, a horizontal water basin is formed
            while (stack.Count > 0 && height[i] > height[stack.Peek()])
            {
                int bottom = stack.Pop();

                // If no left boundary exists, water spills away
                if (stack.Count == 0) break;

                int leftBoundary = stack.Peek();
                int width = i - leftBoundary - 1;
                int boundedHeight = Math.Min(height[i], height[leftBoundary]) - height[bottom];

                totalWater += width * boundedHeight;
            }

            stack.Push(i);
        }

        return totalWater;
    }
}
```

---

## 110. Word Ladder (LeetCode #127)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#bfs` `#bidirectional-bfs` `#implicit-graph` `#shortest-path` |
| **LeetCode Link** | [Word Ladder](https://leetcode.com/problems/word-ladder/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given two words, `beginWord` and `endWord`, and a dictionary `wordList`, return the number of words in the shortest transformation sequence from `beginWord` to `endWord` such that only one letter changes at each step and each transformed word exists in `wordList`. If no such sequence exists, return 0.
- **Key Constraints:**
  - $1 \le beginWord.Length \le 10$.
  - $endWord.Length == beginWord.Length$.
  - $1 \le wordList.Length \le 5000$.
  - All words consist of lowercase English letters and are unique.
- **Senior Edge Cases to Defend:**
  - `endWord` not in `wordList`: Return 0 immediately.
  - Disconnected component (no path exists): Return 0.
  - Shortest path is 1 step (`beginWord` differs from `endWord` by 1 letter).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Unweighted Shortest Path on an Implicit Graph: Vertices are words; edges exist between words differing by 1 character. Bidirectional BFS expands from both ends simultaneously, halving the search tree depth.
- **Sample 1:**
  - **Input:** `beginWord = "hit", endWord = "cog", wordList = ["hot","dot","dog","lot","log","cog"]`
  - **Output:** `5`
  - **Explanation:** `hit -> hot -> dot -> dog -> cog` (5 words).
- **Sample 2:**
  - **Input:** `beginWord = "hit", endWord = "cog", wordList = ["hot","dot","dog","lot","log"]`
  - **Output:** `0`
  - **Explanation:** `cog` is not in `wordList`.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine two search parties in a sprawling underground cave system trying to find each other.
- Party A starts at the Entrance (`beginWord`), and Party B starts at the Treasure Vault (`endWord`).
- If Party A searches alone (unidirectional BFS), their search frontier expands exponentially as a sphere of radius $D$:
  $$\text{Frontier Size} = O(B^D)$$
- But if both Party A and Party B hike toward each other simultaneously (Bidirectional BFS), their search spheres meet in the middle at radius $D / 2$:
  $$\text{Frontier Size} = 2 \times O\left(B^{D / 2}\right)$$
For branching factor $B = 26$ and path length $D = 6$, $26^6 \approx 3.08 \times 10^8$ nodes vs $2 \times 26^3 = 35,152$ nodes! A reduction of nearly **$10,000\times$** in explored states.

#### 3.2 The Naive Bottleneck & Redundant Computation
Building an explicit adjacency list by comparing all pairs of words takes $O(N^2 \cdot L) = (5000)^2 \times 10 = 2.5 \times 10^8$ operations before search even begins.
Generating neighbors dynamically by changing each of the $L$ letters to `'a'..'z'` takes $O(L \cdot 26) = 260$ operations per word.
Unidirectional BFS wastes time expanding massive frontiers near the bottom of the tree. Bidirectional BFS with dynamic frontier swapping keeps the active search set as small as possible.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Dynamic Bidirectional Frontier Swapping:**
Maintain two active frontier sets: `beginSet` and `endSet`.
- **Frontier Swapping Invariant:** At the start of each level:
  $$\text{if } |\text{beginSet}| > |\text{endSet}|, \quad \text{Swap}(\text{beginSet}, \text{endSet})$$
  Always expanding the **smaller** frontier set mathematically minimizes the number of neighbor mutations generated in the current level.
- **Collision Invariant:**
  When generating neighbor `transformed`:
  $$\text{if } transformed \in endSet \implies \text{Path found! Return } level + 1$$
- Remove visited words from the dictionary (`dict.Remove(transformed)`) to prevent cycle revisitation with zero auxiliary memory!

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
BIDIRECTIONAL BFS COLLISION ARCHITECTURE:
Forward Sphere:   beginSet = { "hit" } (radius r_1)
Backward Sphere:  endSet   = { "cog" } (radius r_2)

Expansion Step:
Mutate word by 1 character in 'a' .. 'z'.
If mutated word in opposing set ==> COLLISION DETECTED! Return level + 1.
Else if in dict                 ==> Add to nextLevelSet; remove from dict.
```

- `beginSet`: Active exploration frontier currently expanding.
- `endSet`: Target frontier awaiting connection.
- `dict`: Remaining unvisited dictionary words.
- `level`: Current transformation sequence length.

#### 3.5 State Transition Triggers & Decision Gates
1. **Target Pre-Check Gate:** If `!dict.Contains(endWord)`, return 0 immediately.
2. **Frontier Swap Gate:** If `beginSet.Count > endSet.Count`, swap `(beginSet, endSet)`.
3. **Neighbor Mutation Gate:** For each position $i \in [0 \dots L - 1]$, for $c \in ['a'..'z']$:
   - If `endSet.Contains(transformed)`: Return `level + 1`.
   - If `dict.Contains(transformed)`: Add to `nextLevelSet`, remove from `dict`.
4. **Advance Level Gate:** Set `beginSet = nextLevelSet; level++`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `beginWord = "hit", endWord = "cog"`, `wordList = ["hot","dot","dog","lot","log","cog"]`.

| Level | `beginSet` | `endSet` | Action | Collision Check | Next Frontier |
| :---: | :---: | :---: | :--- | :---: | :--- |
| **1** | `{"hit"}` (size 1) | `{"cog"}` (size 1) | Mutate `"hit"` $\to$ `"hot"` | `"hot" \notin endSet` | `next = {"hot"}` |
| **2** | `{"hot"}` (size 1) | `{"cog"}` (size 1) | Mutate `"hot"` $\to$ `"dot"`, `"lot"` | Not in `endSet` | `next = {"dot", "lot"}` |
| **3** | `{"dot", "lot"}` (size 2)| `{"cog"}` (size 1) | **Swap!** `beginSet = {"cog"}` | — | — |
| | `{"cog"}` (size 1) | `{"dot", "lot"}` | Mutate `"cog"` $\to$ `"dog"`, `"log"` | Not in `endSet` | `next = {"dog", "log"}` |
| **4** | `{"dog", "log"}` (size 2)| `{"dot", "lot"}` | Mutate `"dog"` $\to$ `"dot"` | **`"dot"` in `endSet`!** | **Collision! Return $4 + 1 = 5$** |

Total path length: `5`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Bidirectional BFS with Frontier Swapping):** Peak performance. Drastically shrinks the branching factor; runs $50\times$ faster than unidirectional BFS on dense graphs.
- **Approach 2 (Standard Unidirectional BFS):** Classical queue-based BFS. Good for baseline explanation, but explores the full exponential search tree of depth $D$.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Pre-check:** Convert `wordList` to `HashSet<string>`. If `endWord` not in set, return 0.
- **Step 2: Seed Frontiers:** `beginSet = { beginWord }`, `endSet = { endWord }`.
- **Step 3: Exploration Loop:** While both sets are non-empty, swap to smaller set, generate 26 mutations per character, detect collision or enqueue.
- **Step 4: Increment Level & Return:** Return `level + 1` on collision, or 0 if frontiers exhaust without meeting.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Unidirectional Queue BFS:**
  - Enqueue `beginWord` with level 1.
  - For each word, generate neighbors. If neighbor == `endWord`, return `level + 1`.
  - Incurs memory overhead storing entire levels in a single queue.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Bidirectional BFS (Optimal) | Approach 2: Unidirectional BFS Queue |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(N \cdot L \cdot 26)$ / $O(B^{D/2})$ | $O(B^D)$ |
| **Auxiliary Space** | $O(N \cdot L)$ set storage | $O(N \cdot L)$ queue storage |
| **Output Space** | $O(1)$ scalar integer | $O(1)$ scalar integer |
| **Cache Locality** | Moderate (hash set probing) | Moderate (queue nodes) |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | Low | Low |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #110 - Word Ladder
// Core Pattern: Bidirectional Breadth-First Search / Implicit Graph Traversal
// Primary Invariant: Swapping to expand the smaller frontier minimizes branch factor B^(D/2).
// Collision Defense: When mutated word exists in opposing frontier, path is resolved.
// Cycle Defense: Mutated words are removed from dictionary upon discovery to prevent cycles.
// ============================================================================
```

#### Implementation 1: Bidirectional BFS with Frontier Swapping (Production Optimal)
```csharp
public class Solution
{
    public int LadderLength(string beginWord, string endWord, IList<string> wordList)
    {
        // Guard Clauses
        if (string.IsNullOrEmpty(beginWord) || string.IsNullOrEmpty(endWord) || wordList == null)
        {
            return 0;
        }

        // Store words in a HashSet for O(1) membership queries
        var dict = new HashSet<string>(wordList);

        // Pre-check: If endWord is not in the dictionary, transformation is impossible
        if (!dict.Contains(endWord))
        {
            return 0;
        }

        // Two converging frontiers: Forward from beginWord, Backward from endWord
        var beginSet = new HashSet<string> { beginWord };
        var endSet = new HashSet<string> { endWord };

        int level = 1;

        while (beginSet.Count > 0 && endSet.Count > 0)
        {
            // CRITICAL INVARIANT: Always expand the smaller frontier set
            // Halves the number of character mutations and neighbor probes
            if (beginSet.Count > endSet.Count)
            {
                var temp = beginSet;
                beginSet = endSet;
                endSet = temp;
            }

            var nextLevelSet = new HashSet<string>();

            foreach (string word in beginSet)
            {
                char[] chars = word.ToCharArray();

                for (int i = 0; i < chars.Length; i++)
                {
                    char originalChar = chars[i];

                    for (char c = 'a'; c <= 'z'; c++)
                    {
                        if (c == originalChar) continue;

                        chars[i] = c;
                        string transformed = new string(chars);

                        // COLLISION GATE: If transformed word is in the opposing frontier, paths meet!
                        if (endSet.Contains(transformed))
                        {
                            return level + 1;
                        }

                        // If valid dictionary word, add to next frontier and remove from dict to prevent revisits
                        if (dict.Contains(transformed))
                        {
                            nextLevelSet.Add(transformed);
                            dict.Remove(transformed);
                        }
                    }

                    chars[i] = originalChar;
                }
            }

            beginSet = nextLevelSet;
            level++;
        }

        return 0;
    }
}
```

#### Implementation 2: Unidirectional Queue BFS
```csharp
public class SolutionUnidirectional
{
    public int LadderLength(string beginWord, string endWord, IList<string> wordList)
    {
        var dict = new HashSet<string>(wordList);
        if (!dict.Contains(endWord)) return 0;

        var queue = new Queue<string>();
        queue.Enqueue(beginWord);
        dict.Remove(beginWord);

        int level = 1;

        while (queue.Count > 0)
        {
            int levelSize = queue.Count;

            for (int k = 0; k < levelSize; k++)
            {
                string curr = queue.Dequeue();
                if (curr == endWord) return level;

                char[] chars = curr.ToCharArray();
                for (int i = 0; i < chars.Length; i++)
                {
                    char orig = chars[i];
                    for (char c = 'a'; c <= 'z'; c++)
                    {
                        if (c == orig) continue;
                        chars[i] = c;
                        string next = new string(chars);

                        if (dict.Contains(next))
                        {
                            if (next == endWord) return level + 1;
                            dict.Remove(next);
                            queue.Enqueue(next);
                        }
                    }
                    chars[i] = orig;
                }
            }

            level++;
        }

        return 0;
    }
}
```

---

## 111. Swim in Rising Water (LeetCode #778)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | 💡 Advanced |
| **Pattern Tags** | `#dijkstras-algorithm` `#binary-search-on-answer` `#min-max-path` |
| **LeetCode Link** | [Swim in Rising Water](https://leetcode.com/problems/swim-in-rising-water/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given an $n \times n$ integer matrix `grid` where each value $grid[i][j]$ represents the elevation at that point. At time $t$, you can swim across any cell with elevation $\le t$. Return the least time until you can reach $(n - 1, n - 1)$ starting from $(0, 0)$.
- **Key Constraints:**
  - $n == grid.Length == grid[i].Length \in [1, 50]$.
  - $0 \le grid[i][j] < n^2$.
  - Each value in `grid` is unique.
- **Senior Edge Cases to Defend:**
  - $1 \times 1$ grid: Answer is simply $grid[0][0]$.
  - Starting cell higher than ending cell: Water must rise to at least $grid[0][0]$.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Minimax Shortest Path: Minimize the maximum elevation encountered along the path from $(0, 0)$ to $(n - 1, n - 1)$. Solved via modified Dijkstra's Algorithm with a Min-Heap, or Binary Search on Answer + BFS reachability.
- **Sample 1:**
  - **Input:** `grid = [[0, 2], [1, 3]]`
  - **Output:** `3`
  - **Explanation:** At time 3, all cells can be crossed, including target cell $(1, 1)$ of elevation 3.
- **Sample 2:**
  - **Input:** `grid = [[0,1,2,3,4],[24,23,22,21,5],[12,13,14,15,16],[11,17,18,19,20],[10,9,8,7,6]]`
  - **Output:** `16`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine an archipelago of jagged rock towers. Rising floodwaters submerge the towers progressively as time $t$ ticks upward.
You stand at the northwest tower $(0, 0)$ and must reach the southeast tower $(n - 1, n - 1)$.
You can only step onto an adjacent tower if the water level has risen to at least the height of that tower.
Therefore, your journey is not penalized by the **length** of the route, but strictly by the **tallest rock tower** you are forced to climb along your path!
To find the route requiring the least patience, you send an explorer equipped with a Priority Queue: always stepping next onto whichever neighboring rock has the **lowest maximum elevation** encountered so far.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute force path enumeration evaluates all self-avoiding walks across the grid ($O(4^{N^2})$).
Standard shortest path algorithms like BFS cannot be applied directly because edge costs are non-uniform and represent bottleneck maxima rather than additive sums.
Dijkstra's greedy relaxation with a Min-Heap expands the path with the smallest bottleneck height, reaching the destination in $O(N^2 \log N)$ operations.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Minimax Path Relaxation Invariant:**
Let $cost(u)$ be the minimum water height required to reach cell $u$.
When stepping from cell $u$ to neighbor $v$:
$$cost(v) = \max(cost(u), \; grid[v.r][v.c])$$
- **Greedy Priority Queue Selection:**
  Maintain a Min-Heap keyed on $cost$. Dequeueing cell $(r, c)$ guarantees that we have discovered the global minimal time to reach $(r, c)$.
- **Early Termination:** The first time $(n - 1, n - 1)$ is dequeued from the Min-Heap, its cost is guaranteed to be the global minimum answer!

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
MINIMAX DIJKSTRA FRONTIER:
PriorityQueue extracts (r, c, cost) with smallest cost:
Current Cell (r, c) [ cost ] ───► Neighbor (nr, nc) [ grid[nr][nc] ]
Transition:
nextCost = max(cost, grid[nr][nc])
Enqueue((nr, nc, nextCost), nextCost)
```

- `r, c`: Grid coordinates.
- `cost`: Maximum elevation encountered along the optimal path to $(r, c)$.
- `visited[n, n]`: Boolean matrix preventing redundant node evaluations.

#### 3.5 State Transition Triggers & Decision Gates
1. **Seed Priority Queue:** Enqueue $((0, 0, grid[0][0]), grid[0][0])$, mark `visited[0, 0] = true`.
2. **Dequeue Gate:** Extract `(r, c, cost)` with smallest `cost`.
3. **Destination Hit Gate:** If $r == n - 1 \land c == n - 1$, return `cost`.
4. **4-Way Relaxation Gate:** For each orthogonal neighbor $(nr, nc)$:
   - If in bounds and `!visited[nr, nc]`:
     - `visited[nr, nc] = true`.
     - `nextCost = Math.Max(cost, grid[nr][nc])`.
     - Enqueue $((nr, nc, nextCost), nextCost)$.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `grid = [[0, 2], [1, 3]]`.
Initial: Enqueue $(0, 0, 0)$.

| Dequeued `(r, c, cost)` | Destination? | Neighbors Checked | Action | Priority Queue State |
| :---: | :---: | :---: | :--- | :--- |
| $(0, 0, 0)$ | No | $(0, 1) \to \max(0, 2) = 2$ | Enqueue $(0, 1, 2)$ | `[(1, 0, 1), (0, 1, 2)]` |
| | | $(1, 0) \to \max(0, 1) = 1$ | Enqueue $(1, 0, 1)$ | |
| $(1, 0, 1)$ | No | $(1, 1) \to \max(1, 3) = 3$ | Enqueue $(1, 1, 3)$ | `[(0, 1, 2), (1, 1, 3)]` |
| $(0, 1, 2)$ | No | $(1, 1)$ already visited | Skip | `[(1, 1, 3)]` |
| $(1, 1, 3)$ | **YES!** | Target reached | Return cost 3 | — |

Final result: `3`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Dijkstra PriorityQueue):** The industry standard for minimax path problems. Optimal greedy frontier expansion, $O(N^2 \log N)$ time, terminates early upon reaching the target.
- **Approach 2 (Binary Search on Answer + BFS):** Clean alternative. Binary search the integer answer $T \in [0, N^2 - 1]$. For candidate $T$, run a standard BFS checking if a path exists using only cells $\le T$. Runs in $O(N^2 \log(N^2))$ time.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Allocations:** Allocate `visited` matrix of size $n \times n$.
- **Step 2: Initialize PriorityQueue:** Enqueue origin with priority $grid[0][0]$.
- **Step 3: Exploration Loop:** Dequeue min cost cell. Return immediately if target reached.
- **Step 4: Neighbor Relaxation:** Probe 4 neighbors, take `Math.Max(cost, grid[nr][nc])`, enqueue.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Binary Search on Answer + BFS:**
  - Range: `low = 0, high = N * N - 1`.
  - Mid $T$: Can reach $(N-1, N-1)$ from $(0, 0)$ with cell values $\le T$?
  - If BFS succeeds: `high = T`. Else: `low = T + 1`.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Dijkstra PriorityQueue (Optimal) | Approach 2: Binary Search on Answer + BFS |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(N^2 \log N)$ | $O(N^2 \log(N^2))$ |
| **Auxiliary Space** | $O(N^2)$ heap + visited matrix | $O(N^2)$ BFS queue + visited matrix |
| **Output Space** | $O(1)$ scalar integer | $O(1)$ scalar integer |
| **Cache Locality** | Moderate (heap operations) | High (BFS queue strides) |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | Low | Low |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #111 - Swim in Rising Water
// Core Pattern: Minimax Path Relaxation / Modified Dijkstra with PriorityQueue
// Primary Invariant: Path cost is defined by the maximum elevation encountered along the path.
// Greedy Defense: Min-Heap extracts cell with lowest bottleneck cost, guaranteeing optimal arrival.
// Space Defense: Boolean visited array prevents redundant queue re-insertions.
// ============================================================================
```

#### Implementation 1: Dijkstra PriorityQueue (Production Standard)
```csharp
public class Solution
{
    public int SwimInWater(int[][] grid)
    {
        // Guard Clause
        if (grid == null || grid.Length == 0 || grid[0].Length == 0)
        {
            return 0;
        }

        int n = grid.Length;
        bool[,] visited = new bool[n, n];

        // Min-Heap prioritized by the maximum elevation encountered along the path
        var minHeap = new PriorityQueue<(int r, int c, int cost), int>();

        // Seed origin
        minHeap.Enqueue((0, 0, grid[0][0]), grid[0][0]);
        visited[0, 0] = true;

        int[] dr = { 0, 1, 0, -1 };
        int[] dc = { 1, 0, -1, 0 };

        while (minHeap.Count > 0)
        {
            var (r, c, cost) = minHeap.Dequeue();

            // EARLY EXIT: First time destination is dequeued, its cost is strictly minimal
            if (r == n - 1 && c == n - 1)
            {
                return cost;
            }

            for (int d = 0; d < 4; d++)
            {
                int nr = r + dr[d];
                int nc = c + dc[d];

                // Boundary & Visited Check
                if (nr >= 0 && nr < n && nc >= 0 && nc < n && !visited[nr, nc])
                {
                    visited[nr, nc] = true;

                    // INVARIANT RELAXATION:
                    // Time to swim into neighbor is the max of current path cost and neighbor's elevation
                    int nextCost = Math.Max(cost, grid[nr][nc]);
                    minHeap.Enqueue((nr, nc, nextCost), nextCost);
                }
            }
        }

        return -1;
    }
}
```

#### Implementation 2: Binary Search on Answer + BFS ($O(N^2 \log N)$)
```csharp
public class SolutionBinarySearch
{
    public int SwimInWater(int[][] grid)
    {
        int n = grid.Length;
        int low = grid[0][0];
        int high = n * n - 1;

        while (low < high)
        {
            int mid = low + (high - low) / 2;

            if (CanReachDestination(grid, n, mid))
            {
                high = mid; // Try smaller time limit
            }
            else
            {
                low = mid + 1; // Insufficient time; increase water level
            }
        }

        return low;
    }

    private static bool CanReachDestination(int[][] grid, int n, int maxTime)
    {
        if (grid[0][0] > maxTime) return false;

        bool[,] visited = new bool[n, n];
        var queue = new Queue<(int r, int c)>();

        queue.Enqueue((0, 0));
        visited[0, 0] = true;

        int[] dr = { 0, 1, 0, -1 };
        int[] dc = { 1, 0, -1, 0 };

        while (queue.Count > 0)
        {
            var (r, c) = queue.Dequeue();
            if (r == n - 1 && c == n - 1) return true;

            for (int d = 0; d < 4; d++)
            {
                int nr = r + dr[d];
                int nc = c + dc[d];

                if (nr >= 0 && nr < n && nc >= 0 && nc < n && !visited[nr, nc] && grid[nr][nc] <= maxTime)
                {
                    visited[nr, nc] = true;
                    queue.Enqueue((nr, nc));
                }
            }
        }

        return false;
    }
}
```

---

## 112. Longest Valid Parentheses (LeetCode #32)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | 💡 Advanced |
| **Pattern Tags** | `#monotonic-stack` `#two-pass-counters` `#O(1)-space` |
| **LeetCode Link** | [Longest Valid Parentheses](https://leetcode.com/problems/longest-valid-parentheses/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given a string containing just the characters `'('` and `')'`, return the length of the longest valid (well-formed) parentheses substring.
- **Key Constraints:**
  - $0 \le s.Length \le 3 \times 10^4$.
  - `s[i]` is `'('` or `')'`.
- **Senior Edge Cases to Defend:**
  - Empty string: Returns 0.
  - No valid parentheses (e.g. `")((("` or `"))))"` $\implies 0$).
  - Interleaved valid segments separated by invalid closing brackets (e.g. `")()())()()"` $\implies 4$).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Barrier Tracking: Maintain indices of unmatched boundary characters using a Stack with sentinel `-1`, or execute a Two-Pass Left/Right scan using two counters for $O(1)$ extra space.
- **Sample 1:**
  - **Input:** `s = "(()"`
  - **Output:** `2`
  - **Explanation:** The longest valid parentheses substring is `"()"`.
- **Sample 2:**
  - **Input:** `s = ")()())"`
  - **Output:** `4`
  - **Explanation:** The longest valid parentheses substring is `"()()"`.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine walking along a shoreline.
- Every opening bracket `'('` steps you upward onto higher terrain.
- Every closing bracket `')'` steps you down toward sea level.
- When you descend and match an opening bracket, you have crossed a balanced valley. The width of that valley is your current position minus the last position where you stood at sea level.
- However, if you see a closing bracket `')'` when already at sea level, you fall into the ocean! This unmatched closing bracket becomes an impenetrable sea wall (barrier). No valid substring starting before this wall can ever bridge across it. The sea wall resets your baseline to the current index.

#### 3.2 The Naive Bottleneck & Redundant Computation
Testing all $O(N^2)$ substrings and validating each in $O(N)$ time takes $O(N^3)$.
Even caching validation reduces to $O(N^2)$.
By tracking the last unmatched barrier index using a stack, we compute the valid span in $O(1)$ upon every valid match, completing the entire analysis in a single $O(N)$ pass.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Stack Sentinel Invariant:**
Initialize the stack with index `-1` representing the virtual barrier wall before the start of the string:
1. When encountering `'('`: Push its index $i$ onto the stack.
2. When encountering `')'`:
   - Pop the top of the stack (matches the most recent `'('` or removes the prior barrier).
   - **If the stack is empty:** This closing bracket had no matching opening bracket. It becomes the **new barrier wall**! Push $i$ onto the stack.
   - **If the stack is non-empty:** The distance from the current index to the new top of the stack is guaranteed to be a valid, well-formed substring:
     $$length = i - \text{stack.Peek()}$$
     $$maxLen = \max(maxLen, length)$$

**Two-Pass Counters Invariant ($O(1)$ Auxiliary Space):**
- **Forward Pass (Left to Right):** Count `left` and `right`. If `left == right`, record $2 \times right$. If `right > left`, unmatched closing bracket detected; reset `left = right = 0`.
- **Backward Pass (Right to Left):** Symmetrically, if `left > right`, unmatched opening bracket detected; reset `left = right = 0`. Catches cases like `"(()"` that forward pass misses.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
STACK SENTINEL BARRIER ARCHITECTURE:
s = [  )   (   )   (   )   )  ]
Index: 0   1   2   3   4   5

Stack starts with sentinel: [ -1 ]
i = 0 (')'): Pop -1 -> Stack empty! Push 0 as new barrier: [ 0 ]
i = 1 ('('): Push 1: [ 0, 1 ]
i = 2 (')'): Pop 1 -> Stack has [ 0 ]. Valid span = 2 - 0 = 2!
i = 3 ('('): Push 3: [ 0, 3 ]
i = 4 (')'): Pop 3 -> Stack has [ 0 ]. Valid span = 4 - 0 = 4!
i = 5 (')'): Pop 0 -> Stack empty! Push 5 as new barrier: [ 5 ]
```

- `i`: Read cursor advancing sequentially from $0$ to $N - 1$.
- `stack.Peek()`: The index of the nearest unmatched barrier on the left.
- `maxLen`: Global maximum valid length discovered.

#### 3.5 State Transition Triggers & Decision Gates
For each index $i \in [0 \dots N - 1]$:
1. If $s[i] == '(': \; \text{stack.Push}(i)$.
2. If $s[i] == ')':$
   - `stack.Pop()`.
   - **Barrier Gate:** If `stack.Count == 0`, push $i$ as new barrier.
   - **Span Gate:** Else, `maxLen = Math.Max(maxLen, i - stack.Peek())`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `s = ")()())"`.

| $i$ | Char | Stack Action | Stack Content After Action | Current Valid Span | `maxLen` |
| :---: | :---: | :--- | :--- | :---: | :---: |
| **Init**| — | Initial sentinel | `[-1]` | — | 0 |
| **0** | `')'` | Pop -1 (empty) $\to$ Push 0 | `[0]` | Barrier reset | 0 |
| **1** | `'('` | Push 1 | `[0, 1]` | — | 0 |
| **2** | `')'` | Pop 1 (stack: `[0]`) | `[0]` | $2 - 0 = 2$ | 2 |
| **3** | `'('` | Push 3 | `[0, 3]` | — | 2 |
| **4** | `')'` | Pop 3 (stack: `[0]`) | `[0]` | $4 - 0 = 4$ | **4** |
| **5** | `')'` | Pop 0 (empty) $\to$ Push 5 | `[5]` | Barrier reset | 4 |

Final result: `4`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Stack with Sentinel -1):** The classic interview standard. Intuitive, single pass, $O(N)$ time and space.
- **Approach 2 (Two-Pass Left/Right Counters):** Elite memory optimization. Operates in $O(N)$ time with strictly $O(1)$ auxiliary space by scanning forward and backward.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Guard:** If string is null or empty, return 0. Allocate stack, push `-1`.
- **Step 2: Exploration Loop:** Advance $i$ through $s$. Push '('; on ')' pop and test if stack is empty.
- **Step 3: Invariant Maintenance:** If empty, push $i$ as barrier; else compute $i - stack.Peek()$.
- **Step 4: Resolution & Return:** Return `maxLen`.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Two-Pass Counters ($O(1)$ Space):**
  - Pass 1 (Forward): Track `left` and `right`. If `left == right`, update `maxLen = Math.Max(maxLen, 2 * right)`. If `right > left`, reset `left = right = 0`.
  - Pass 2 (Backward): Symmetrically reset when `left > right`. Update `maxLen = Math.Max(maxLen, 2 * left)`.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Stack with Sentinel | Approach 2: Two-Pass Counters ($O(1)$ Space) |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(N)$ single pass | $O(N)$ two passes |
| **Auxiliary Space** | $O(N)$ stack memory | $O(1)$ scalar variables |
| **Output Space** | $O(1)$ scalar integer | $O(1)$ scalar integer |
| **Cache Locality** | Moderate (stack operations) | Optimal (sequential array scans) |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | High | Low (requires reverse pass) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #112 - Longest Valid Parentheses
// Core Pattern: Unmatched Barrier Tracking / Stack vs Two-Pass Counters
// Primary Invariant: Valid substring length = current index i - last unmatched barrier index.
// Sentinel Defense: Initializing stack with -1 creates baseline for valid substrings starting at index 0.
// Space Defense: Two-pass left/right counter approach achieves strict O(1) auxiliary memory.
// ============================================================================
```

#### Implementation 1: Stack with Sentinel -1 (Production Standard)
```csharp
public class Solution
{
    public int LongestValidParentheses(string s)
    {
        // Guard Clause
        if (string.IsNullOrEmpty(s))
        {
            return 0;
        }

        var stack = new Stack<int>();

        // SENTINEL DEFENSE:
        // Push -1 as the virtual unmatched barrier index before string start
        stack.Push(-1);

        int maxLen = 0;

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '(')
            {
                // Push index of opening bracket onto stack
                stack.Push(i);
            }
            else
            {
                // Pop the matching '(' or the previous barrier
                stack.Pop();

                // BARRIER GATE:
                // If stack becomes empty, this ')' has no matching '('; it becomes the new barrier
                if (stack.Count == 0)
                {
                    stack.Push(i);
                }
                else
                {
                    // INVARIANT: Valid substring extends from current index back to the new stack top
                    maxLen = Math.Max(maxLen, i - stack.Peek());
                }
            }
        }

        return maxLen;
    }
}
```

#### Implementation 2: Two-Pass Left/Right Counters ($O(1)$ Auxiliary Space)
```csharp
public class SolutionTwoPass
{
    public int LongestValidParentheses(string s)
    {
        if (string.IsNullOrEmpty(s)) return 0;

        int left = 0, right = 0;
        int maxLen = 0;

        // PASS 1: Left to Right Scan
        // Detects valid substrings and resets when closing brackets exceed opening brackets
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '(') left++;
            else right++;

            if (left == right)
            {
                maxLen = Math.Max(maxLen, 2 * right);
            }
            else if (right > left)
            {
                // Barrier hit: More closing brackets than opening brackets
                left = right = 0;
            }
        }

        // PASS 2: Right to Left Scan
        // Symmetrically detects valid substrings and resets when opening brackets exceed closing brackets
        // Resolves strings with excess opening brackets like "(()" that pass 1 misses
        left = right = 0;
        for (int i = s.Length - 1; i >= 0; i--)
        {
            if (s[i] == '(') left++;
            else right++;

            if (left == right)
            {
                maxLen = Math.Max(maxLen, 2 * left);
            }
            else if (left > right)
            {
                // Barrier hit: More opening brackets than closing brackets
                left = right = 0;
            }
        }

        return maxLen;
    }
}
```

---

## 113. Regular Expression Matching (LeetCode #10)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | 💡 Advanced |
| **Pattern Tags** | `#2d-dp` `#regex-state-machine` `#string-matching` |
| **LeetCode Link** | [Regular Expression Matching](https://leetcode.com/problems/regular-expression-matching/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an input string `s` and a pattern `p`, implement regular expression matching with support for `'.'` (matches any single character) and `'*'` (matches zero or more of the preceding element). The matching should cover the entire input string (not partial).
- **Key Constraints:**
  - $1 \le s.Length, p.Length \le 20$.
  - `s` contains only lowercase English letters.
  - `p` contains only lowercase English letters, `'.'`, and `'*'`.
  - Guaranteed for each appearance of `'*'`, there is a valid preceding character to match.
- **Senior Edge Cases to Defend:**
  - Pattern matching empty string: Patterns like `"a*b*c*"` match `""`.
  - Kleene star consuming 0 characters vs consuming multiple characters.
  - Wildcard dot with star: `".*"` can match any string of any length.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** 2D State Machine Dynamic Programming: $dp[i][j]$ indicates whether prefix $s[0 \dots i - 1]$ matches prefix $p[0 \dots j - 1]$.
- **Sample 1:**
  - **Input:** `s = "aa", p = "a*"`
  - **Output:** `true`
  - **Explanation:** `'*'` means zero or more of the preceding element `'a'`.
- **Sample 2:**
  - **Input:** `s = "aab", p = "c*a*b"`
  - **Output:** `true`
  - **Explanation:** `c*` counts as 0 of 'c', `a*` counts as 2 of 'a', and `b` matches 'b'.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a compiler parser constructed as a Non-Deterministic Finite Automaton (NFA).
- Normal letters (`'a'..'z'`) and `'.'` represent deterministic forward transitions: to consume character $s[i-1]$, the pattern character $p[j-1]$ must match, stepping diagonally: $dp[i-1][j-1]$.
- The Kleene star `'*'` represents a special two-way junction:
  1. **Bypass Branch (0 occurrences):** The star ignores its preceding character completely. You jump 2 steps backward in the pattern: $dp[i][j - 2]$.
  2. **Looping Branch (1 or more occurrences):** If the preceding pattern character matches the current string character, the star can consume $s[i-1]$ while remaining active to potentially consume more characters: $dp[i - 1][j]$.
By combining these branches in a 2D matrix, we resolve the NFA deterministically.

#### 3.2 The Naive Bottleneck & Redundant Computation
A recursive backtracking function branches at every `'*'` into 0-match and 1-match branches:
$$T(M, N) = O(2^{M + N})$$
With multiple stars (e.g. `s = "aaaaaaaa"`, `p = "a*a*a*a*"`), redundant subproblem evaluations cause exponential explosion. 2D DP caches the $(M + 1) \times (N + 1)$ prefix states, running in strict $O(M \times N)$ polynomial time.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**2D DP State Transitions:**
Let $dp[i][j]$ be boolean indicating whether $s[0 \dots i - 1]$ matches $p[0 \dots j - 1]$.
- **Base Case 1:** $dp[0][0] = \text{true}$ (empty string matches empty pattern).
- **Base Case 2 (Empty String Matches Star Patterns):** For $j = 2 \dots N$:
  $$\text{if } p[j - 1] == '*' \implies dp[0][j] = dp[0][j - 2]$$
- **For $i \ge 1, j \ge 1$:**
  1. **Case A ($p[j - 1] == '*'$):**
     - Zero occurrences: $dp[i][j] = dp[i][j - 2]$.
     - One or more occurrences: If preceding pattern char matches $s[i-1]$ ($p[j-2] == '.' \lor p[j-2] == s[i-1]$):
       $$dp[i][j] = dp[i][j] \lor dp[i - 1][j]$$
  2. **Case B ($p[j - 1] \neq '*'$):**
     - If $p[j - 1] == '.' \lor p[j - 1] == s[i - 1]$:
       $$dp[i][j] = dp[i - 1][j - 1]$$

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
2D REGEX STATE TRANSITION ARCHITECTURE:
p[j - 1] == '*':
                  j - 2          j - 1            j
i - 1 [             ]        [       ]      [ dp[i-1, j]: Consume char s[i-1] ]
i     [ dp[i, j - 2]: 0 match] [       ] ──► [ dp[i, j]: Active State           ]

Transitions:
dp[i, j] = dp[i, j - 2]  (0 match)
           OR (if match) dp[i - 1, j]  (1+ match)
```

- `i`: Prefix length of string `s` ($0 \le i \le M$).
- `j`: Prefix length of pattern `p` ($0 \le j \le N$).
- `dp[i, j]`: Boolean match status for prefixes $s[0 \dots i - 1]$ and $p[0 \dots j - 1]$.

#### 3.5 State Transition Triggers & Decision Gates
1. **Initialize Base:** $dp[0, 0] = \text{true}$. For $j = 2 \dots N$ step 2: if $p[j - 1] == '*'$, $dp[0, j] = dp[0, j - 2]$.
2. **Exploration Grid:** Loop $i = 1 \dots M$, $j = 1 \dots N$:
   - If $p[j - 1] == '*' $:
     - `dp[i, j] = dp[i, j - 2]`.
     - If $p[j - 2] == '.' \lor p[j - 2] == s[i - 1]$, `dp[i, j] |= dp[i - 1, j]`.
   - Else:
     - If $p[j - 1] == '.' \lor p[j - 1] == s[i - 1]$, `dp[i, j] = dp[i - 1, j - 1]`.
3. **Return:** `dp[M, N]`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `s = "aab"`, `p = "c*a*b"`.
Matrix size: $(3 + 1) \times (5 + 1)$.

| $i \backslash j$ | `""` (0) | `'c'` (1) | `'*'` (2) | `'a'` (3) | `'*'` (4) | `'b'` (5) |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| **`""` (0)** | **T** | F | **T** ($c^* \to 0$) | F | **T** ($a^* \to 0$) | F |
| **`"a"` (1)** | F | F | F | **T** ($a=a$) | **T** ($a^* \to 1$) | F |
| **`"aa"` (2)**| F | F | F | F | **T** ($a^* \to 2$) | F |
| **`"aab"` (3)**| F | F | F | F | F | **T** ($b=b$) |

Final result: $dp[3, 5] = \text{true}$.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (2D Tabulation DP):** Standard production solution. Non-recursive, zero call-stack risk, easily verifiable table geometry. $O(M \times N)$ time and space.
- **Approach 2 (Top-Down Memoized DFS):** Natural recursive mapping of the NFA branching rules. Only computes reachable states.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Allocations:** Allocate `bool[m + 1, n + 1]`. Set $dp[0, 0] = \text{true}$.
- **Step 2: Seed Row 0:** Resolve star patterns matching the empty string.
- **Step 3: Nested State Fill:** Traverse $i \in [1 \dots m]$ and $j \in [1 \dots n]$. Apply Kleene star or literal matching gates.
- **Step 4: Resolution & Return:** Return $dp[m, n]$.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Top-Down Memoized DFS:**
  - Signature: `Dfs(i, j)`.
  - If $j == p.Length$, return $i == s.Length$.
  - First match: `i < s.Length && (s[i] == p[j] || p[j] == '.')`.
  - If $j + 1 < p.Length && p[j + 1] == '*' $: return `Dfs(i, j + 2) || (firstMatch && Dfs(i + 1, j))`.
  - Else: return `firstMatch && Dfs(i + 1, j + 1)`.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: 2D Tabulation DP | Approach 2: Top-Down Memoized DFS |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(M \times N)$ | $O(M \times N)$ |
| **Auxiliary Space** | $O(M \times N)$ table | $O(M \times N)$ memo + $O(M + N)$ stack |
| **Output Space** | $O(1)$ boolean | $O(1)$ boolean |
| **Cache Locality** | High (sequential 2D array reads) | Moderate |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | Low | Low |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #113 - Regular Expression Matching
// Core Pattern: 2D State Machine Dynamic Programming / Kleene Star NFA
// Primary Invariant: p[j-1] == '*' branches into 0-match (dp[i, j-2]) or 1+-match (dp[i-1, j]).
// Base Case Defense: Row 0 handles patterns like "a*b*c*" matching empty string "".
// Complexity Target: O(M x N) time and auxiliary space.
// ============================================================================
```

#### Implementation 1: 2D Tabulation DP (Production Standard)
```csharp
public class Solution
{
    public bool IsMatch(string s, string p)
    {
        // Guard Clauses
        if (s == null || p == null) return false;

        int m = s.Length;
        int n = p.Length;

        // dp[i, j] indicates whether prefix s[0 .. i - 1] matches prefix p[0 .. j - 1]
        bool[,] dp = new bool[m + 1, n + 1];

        // Base case: Empty string matches empty pattern
        dp[0, 0] = true;

        // Base case: Patterns like "a*", "a*b*", "a*b*c*" can match an empty string ""
        for (int j = 2; j <= n; j += 2)
        {
            if (p[j - 1] == '*')
            {
                dp[0, j] = dp[0, j - 2];
            }
        }

        for (int i = 1; i <= m; i++)
        {
            for (int j = 1; j <= n; j++)
            {
                char pChar = p[j - 1];

                // CASE 1: Kleene Star '*'
                if (pChar == '*')
                {
                    // Option A: 0 occurrences of preceding character (bypass star and preceding char)
                    dp[i, j] = dp[i, j - 2];

                    // Option B: 1 or more occurrences of preceding character
                    char prevPChar = p[j - 2];
                    if (prevPChar == '.' || prevPChar == s[i - 1])
                    {
                        // Invariant: If preceding char matches s[i-1], consume s[i-1] while retaining pattern
                        dp[i, j] = dp[i, j] || dp[i - 1, j];
                    }
                }
                // CASE 2: Literal character or wildcard dot '.'
                else
                {
                    if (pChar == '.' || pChar == s[i - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1];
                    }
                }
            }
        }

        return dp[m, n];
    }
}
```

#### Implementation 2: Top-Down Memoized DFS
```csharp
public class SolutionDfs
{
    public bool IsMatch(string s, string p)
    {
        if (s == null || p == null) return false;

        // memo[i, j]: 0 = uncomputed, 1 = true, 2 = false
        int[,] memo = new int[s.Length + 1, p.Length + 1];
        return Dfs(s, 0, p, 0, memo);
    }

    private static bool Dfs(string s, int i, string p, int j, int[,] memo)
    {
        if (memo[i, j] != 0)
        {
            return memo[i, j] == 1;
        }

        bool result;

        // If pattern is exhausted, string must also be exhausted
        if (j == p.Length)
        {
            result = (i == s.Length);
        }
        else
        {
            bool firstMatch = (i < s.Length) && (p[j] == s[i] || p[j] == '.');

            // Kleene star check on next character
            if (j + 1 < p.Length && p[j + 1] == '*')
            {
                // Branch 1: 0 occurrences (skip "c*")
                // Branch 2: 1 or more occurrences (consume s[i], keep pattern j)
                result = Dfs(s, i, p, j + 2, memo) || (firstMatch && Dfs(s, i + 1, p, j, memo));
            }
            else
            {
                result = firstMatch && Dfs(s, i + 1, p, j + 1, memo);
            }
        }

        memo[i, j] = result ? 1 : 2;
        return result;
    }
}
```

---

## 114. Burst Balloons (LeetCode #312)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | 💡 Advanced |
| **Pattern Tags** | `#interval-dp` `#divide-and-conquer` `#boundary-inversion` |
| **LeetCode Link** | [Burst Balloons](https://leetcode.com/problems/burst-balloons/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given $n$ balloons with values `nums`, bursting balloon $i$ yields $nums[i - 1] 	imes nums[i] 	imes nums[i + 1]$ coins. If $i - 1$ or $i + 1$ goes out of bounds, treat it as a balloon with value 1. Return the maximum coins collectible by bursting balloons wisely.
- **Key Constraints:**
  - $n == nums.Length \in [1, 300]$.
  - $0 \le nums[i] \le 100$.
- **Senior Edge Cases to Defend:**
  - $n = 1$: Return $1 \times nums[0] \times 1 = nums[0]$.
  - Zero-value balloons: Handled cleanly by multiplication.
  - Interval boundary padding: Pad array with virtual boundaries `[1, ...nums, 1]`.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Interval Dynamic Programming via Boundary Inversion: Invert the question from "which balloon to burst first" to "which balloon $k$ is burst **last** in open interval $(i, j)$".
- **Sample 1:**
  - **Input:** `nums = [3, 1, 5, 8]`
  - **Output:** `167`
  - **Explanation:**
    - Burst 1: coins = $3 \times 1 \times 5 = 15$
    - Burst 5: coins = $3 \times 5 \times 8 = 120$
    - Burst 3: coins = $1 \times 3 \times 8 = 24$
    - Burst 8: coins = $1 \times 8 \times 1 = 8$
    - Total = $15 + 120 + 24 + 8 = 167$.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a row of balloons pinned to a wall.
If you think forward—*"Which balloon should I burst first?"*—you face disaster:
When balloon $k$ pops, its left and right neighbors immediately snap together and touch each other. This creates dynamic, unpredictable dependencies where the subproblems left of $k$ and right of $k$ are hopelessly entangled!
**The Inversion Breakthrough:**
Instead, ask: **"Which balloon $k$ will be the VERY LAST balloon burst in interval $(i, j)$?"**
Because balloon $k$ is the last to burst in $(i, j)$, all other balloons between $i$ and $j$ have already vanished!
Therefore, when balloon $k$ finally pops, its immediate neighbors are **guaranteed to be the fixed boundary balloons $i$ and $j$**!
This completely decouples the subproblems:
$$\text{Coins}(i, j) = \max_{k \in (i, j)} \Big( dp[i, k] + dp[k, j] + nums[i] \times nums[k] \times nums[j] \Big)$$

#### 3.2 The Naive Bottleneck & Redundant Computation
Searching all burst orders naively evaluates all permutations:
$$T(N) = O(N!)$$
For $N = 300$, $300! \approx 3 \times 10^{614}$, completely impossible. Interval DP groups identical sub-intervals $(i, j)$ of increasing length, reducing runtime to $O(N^3) \le 300^3 = 2.7 \times 10^7$ operations.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Interval DP Recurrence with Virtual Padding:**
Pad `nums` with $1$ on both ends:
$$\text{padded} = [1, \; nums[0], \; nums[1], \; \dots, \; nums[n - 1], \; 1]$$
Length of padded array is $m = n + 2$.
Let $dp[i, j]$ be the maximum coins obtained by bursting all balloons strictly inside the open interval $(i, j)$ (excluding $i$ and $j$).
- **Recurrence:**
  $$dp[i, j] = \max_{k = i + 1}^{j - 1} \Big( dp[i, k] + dp[k, j] + padded[i] \times padded[k] \times padded[j] \Big)$$
- **Order of Evaluation Invariant:**
  We must solve smaller intervals before larger intervals!
  Outer loop: interval length $len = 2 \dots m - 1$.
  Middle loop: start index $i = 0 \dots m - len - 1$ (where $j = i + len$).
  Inner loop: last balloon $k = i + 1 \dots j - 1$.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
INTERVAL DP DECOUPLING ARCHITECTURE:
padded: [ 1 ,  3 ,  1 ,  5 ,  8 ,  1 ]
Indices:  0    1    2    3    4    5
          i ───►  [ k ]  ◄─── j
Open interval (i, j) with last balloon k:
Subproblem 1: dp[i, k] (Balloons between i and k)
Subproblem 2: dp[k, j] (Balloons between k and j)
Balloon k burst coins: padded[i] * padded[k] * padded[j]
```

- `len`: Length of the open interval $j - i$ ($2 \le len < m$).
- `i, j`: Left and right boundary anchors of the open interval.
- `k`: Candidate last-burst balloon strictly inside $(i, j)$.
- `dp[i, j]`: Maximum coins collectible by bursting all balloons in $(i, j)$.

#### 3.5 State Transition Triggers & Decision Gates
1. **Pad Array:** Create `padded` of size $n + 2$ with `padded[0] = padded[n + 1] = 1`.
2. **Interval Loop ($len = 2 \dots n + 1$):**
   - For $i = 0 \dots (n + 2 - len - 1)$:
     - Let $j = i + len$.
     - For $k = i + 1 \dots j - 1$:
       $$coins = dp[i, k] + dp[k, j] + padded[i] \times padded[k] \times padded[j]$$
       $$dp[i, j] = \max(dp[i, j], coins)$$
3. **Return:** $dp[0, n + 1]$.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `nums = [3, 1, 5, 8]`. Padded: `[1, 3, 1, 5, 8, 1]`. $m = 6$.

| Interval Length (`len`) | Interval $(i, j)$ | Candidate $k$ | Coins Calculation | $dp[i, j]$ |
| :---: | :---: | :---: | :--- | :---: |
| **2** | $(0, 2)$ | 1 (val 3) | $dp[0,1] + dp[1,2] + 1 \times 3 \times 1 = 3$ | 3 |
| **2** | $(1, 3)$ | 2 (val 1) | $dp[1,2] + dp[2,3] + 3 \times 1 \times 5 = 15$ | 15 |
| **2** | $(2, 4)$ | 3 (val 5) | $dp[2,3] + dp[3,4] + 1 \times 5 \times 8 = 40$ | 40 |
| **2** | $(3, 5)$ | 4 (val 8) | $dp[3,4] + dp[4,5] + 5 \times 8 \times 1 = 40$ | 40 |
| **...** | ... | ... | ... | ... |
| **5 (Full)** | $(0, 5)$ | 4 (val 8) | $dp[0,4] + dp[4,5] + 1 \times 8 \times 1 = 159 + 8 = 167$ | **167** |

Final maximum coins: `167`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Interval DP by Window Length):** The definitive standard. $O(N^3)$ time, $O(N^2)$ space. Decouples dependencies via the last-burst insight.
- **Approach 2 (Top-Down Memoized DFS):** Recursive formulation with memoization table `memo[i, j]`. Identical complexity, slightly higher constant factor due to function call overhead.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Pad Array:** Copy `nums` into `padded` with sentinel 1s at both boundaries.
- **Step 2: Table Allocation:** Allocate `dp = new int[m, m]` where $m = n + 2$.
- **Step 3: Three-Tier Loop:** Outer loop `len` ($2 \dots m - 1$), middle loop `i`, inner loop `k`.
- **Step 4: Resolution & Return:** Return $dp[0, m - 1]$.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: Top-Down Memoized DFS:**
  - Define `Dfs(i, j)`. If $i + 1 == j$, return 0 (no balloons between $i$ and $j$).
  - For $k = i + 1 \dots j - 1$: maximize $Dfs(i, k) + Dfs(k, j) + padded[i] \times padded[k] \times padded[j]$.
  - Cache in `memo[i, j]`.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Interval DP (Bottom-Up) | Approach 2: Top-Down Memoized DFS |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(N^3)$ | $O(N^3)$ |
| **Auxiliary Space** | $O(N^2)$ 2D matrix | $O(N^2)$ memo + $O(N)$ recursion stack |
| **Output Space** | $O(1)$ scalar integer | $O(1)$ scalar integer |
| **Cache Locality** | High (bottom-up table fill) | Moderate |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | Low | Low |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #114 - Burst Balloons
// Core Pattern: Interval Dynamic Programming / Last-Burst Boundary Inversion
// Primary Invariant: k is the LAST balloon burst in open interval (i, j), fixing i and j as neighbors.
// Padding Defense: Pad array with 1 at index 0 and n + 1 to eliminate edge bounds checking.
// Evaluation Order: Outer loop over interval length len ensures sub-intervals are settled first.
// ============================================================================
```

#### Implementation 1: Interval DP by Window Length (Production Standard)
```csharp
public class Solution
{
    public int MaxCoins(int[] nums)
    {
        // Guard Clause
        if (nums == null || nums.Length == 0)
        {
            return 0;
        }

        int n = nums.Length;

        // PADDING DEFENSE:
        // Pad array with virtual boundaries 1 at index 0 and index n + 1
        int[] padded = new int[n + 2];
        padded[0] = 1;
        padded[n + 1] = 1;
        for (int idx = 0; idx < n; idx++)
        {
            padded[idx + 1] = nums[idx];
        }

        int m = padded.Length;
        // dp[i, j] stores maximum coins from bursting all balloons strictly inside open interval (i, j)
        int[,] dp = new int[m, m];

        // CRITICAL INVARIANT: Evaluate intervals in increasing order of length (len = j - i)
        // Smaller sub-intervals must be fully settled before evaluating larger encompassing intervals
        for (int len = 2; len < m; len++)
        {
            for (int i = 0; i < m - len; i++)
            {
                int j = i + len;

                // k represents the LAST balloon burst strictly inside open interval (i, j)
                // Because k is last, balloons i and j are guaranteed to remain as k's immediate neighbors!
                for (int k = i + 1; k < j; k++)
                {
                    int coins = padded[i] * padded[k] * padded[j] + dp[i, k] + dp[k, j];
                    if (coins > dp[i, j])
                    {
                        dp[i, j] = coins;
                    }
                }
            }
        }

        // Return maximum coins for entire open interval (0, n + 1)
        return dp[0, m - 1];
    }
}
```

#### Implementation 2: Top-Down Memoized DFS
```csharp
public class SolutionMemo
{
    public int MaxCoins(int[] nums)
    {
        if (nums == null || nums.Length == 0) return 0;

        int n = nums.Length;
        int[] padded = new int[n + 2];
        padded[0] = 1;
        padded[n + 1] = 1;
        for (int i = 0; i < n; i++) padded[i + 1] = nums[i];

        int m = padded.Length;
        int[,] memo = new int[m, m];

        return Dfs(padded, 0, m - 1, memo);
    }

    private static int Dfs(int[] padded, int i, int j, int[,] memo)
    {
        // No balloons between i and j
        if (i + 1 >= j) return 0;

        if (memo[i, j] != 0) return memo[i, j];

        int maxCoins = 0;

        for (int k = i + 1; k < j; k++)
        {
            int coins = padded[i] * padded[k] * padded[j] +
                        Dfs(padded, i, k, memo) +
                        Dfs(padded, k, j, memo);

            if (coins > maxCoins)
            {
                maxCoins = coins;
            }
        }

        memo[i, j] = maxCoins;
        return maxCoins;
    }
}
```

---

## 115. Serialize and Deserialize Binary Tree (LeetCode #297)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#binary-tree` `#preorder-dfs` `#serialization` `#bfs-queue` |
| **LeetCode Link** | [Serialize and Deserialize Binary Tree](https://leetcode.com/problems/serialize-and-deserialize-binary-tree/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Design an algorithm to serialize and deserialize a binary tree. There is no restriction on how your serialization/deserialization algorithm should work. You just need to ensure that a binary tree can be serialized to a string and this string can be deserialized to the original tree structure.
- **Key Constraints:**
  - Number of nodes in the tree is in the range $[0, 10^4]$.
  - $-1000 \le Node.val \le 1000$.
- **Senior Edge Cases to Defend:**
  - Empty tree (`root == null` $\implies$ `"#,"` or `""`).
  - Single-node tree.
  - Skewed tree (linked list topology of depth $10^4$): Avoid quadratic string concatenation.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Pre-Order DFS with Explicit Null Markers: Pre-order traversal with explicit sentinel null tokens uniquely determines binary tree topology without requiring an auxiliary in-order traversal.
- **Sample 1:**
  - **Input:** `root = [1, 2, 3, null, null, 4, 5]`
  - **Output:** `[1, 2, 3, null, null, 4, 5]`
  - **Serialized Representation:** `"1,2,#,#,3,4,#,#,5,#,#"`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine transmitting a complex 3D skeletal sculpture over a 1D telegraph wire.
- In classical graph theory, a Pre-Order traversal alone cannot reconstruct a tree because you cannot tell where a left branch ends and a right branch begins (e.g. `[1, 2]` could be a root with a left child or a root with a right child).
- However, if you explicitly broadcast a **Null Sentinel Token (`#`)** whenever a branch terminates, every single leaf node announces its own boundaries!
- During reconstruction, a worker simply listens to the incoming telegraph stream:
  1. The next token is guaranteed to be the value of the active node.
  2. If the token is `#`, return `null`.
  3. Otherwise, create the node, and immediately recurse to assemble its left child, followed by its right child!

#### 3.2 The Naive Bottleneck & Redundant Computation
Older textbooks reconstruct trees using two simultaneous traversals: Pre-Order + In-Order. This requires unique node values (failing on trees with duplicate keys!) and takes $O(N^2)$ time unless pre-indexed with a Hash Map.
String concatenation inside recursion (`s += val + ","`) creates $O(N^2)$ garbage string allocations.
Using `StringBuilder` during serialization and a `Queue<string>` or token cursor during deserialization executes in clean $O(N)$ linear time.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Null-Token Pre-Order Invariant:**
- **Serialization (Pre-Order):**
  $$\text{Serialize}(node) = node.val + \text{Delimiter} + \text{Serialize}(node.left) + \text{Serialize}(node.right)$$
  When $node == null$, output `"#,"`.
- **Deserialization (FIFO Queue):**
  Split tokens into a sequential Queue.
  Dequeue token:
  - If token is `"#"`, return `null`.
  - Else instantiate `TreeNode(int.Parse(token))`.
  - Wire `node.left = BuildTree(queue)`.
  - Wire `node.right = BuildTree(queue)`.
  - Return `node`.
- **Bijection Proof:** Every binary tree of $N$ nodes has exactly $N + 1$ null pointers. The total token count is strictly $2N + 1$, guaranteeing unambiguous $O(N)$ reconstruction.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
PRE-ORDER NULL-TOKEN STREAM ARCHITECTURE:
Tree:
      1
     / \
    2   3
       / \
      4   5

Serialized Stream:
[ 1 , 2 , # , # , 3 , 4 , # , # , 5 , # , # ]
  ▲
Queue Head (Consumed sequentially to build: Root -> Left Subtree -> Right Subtree)
```

- `NullToken`: Constant string sentinel (`"#"`) marking empty child references.
- `Delimiter`: Separator character (`','`) segmenting token payloads.
- `queue`: FIFO stream dispensing tokens in strict pre-order execution sequence.

#### 3.5 State Transition Triggers & Decision Gates
1. **Serialization Base Gate:** If `node == null`, append `NullToken + Delimiter` and return.
2. **Serialization DFS Gate:** Append `node.val + Delimiter`, recurse left, recurse right.
3. **Deserialization Token Gate:** Dequeue next token:
   - If `token == NullToken`, return `null`.
   - Else instantiate `node = new TreeNode(int.Parse(token))`.
   - `node.left = BuildTree(queue)`.
   - `node.right = BuildTree(queue)`.
   - Return `node`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `root = [1, 2, null]`.
Serialized: `"1,2,#,#,#,"`. Tokens queue: `["1", "2", "#", "#", "#"]`.

| Step | Token Dequeued | Node Instantiated | Recursive Attachment |
| :---: | :---: | :--- | :--- |
| **1** | `"1"` | `Node(1)` | Active Root |
| **2** | `"2"` | `Node(2)` | `Node(1).left = Node(2)` |
| **3** | `"#"` | `null` | `Node(2).left = null` |
| **4** | `"#"` | `null` | `Node(2).right = null` (Node 2 complete) |
| **5** | `"#"` | `null` | `Node(1).right = null` (Node 1 complete) |

Reconstructed Tree: `1 -> left = 2, right = null`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Pre-Order DFS with Null Tokens):** Universal industry favorite. Extremely compact code, handles duplicate node values naturally, strictly $O(N)$ time and space.
- **Approach 2 (BFS Level-Order Serialization):** Generates output mimicking LeetCode's canonical string representation. Uses a queue during both serialization and deserialization.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Serialize DFS:** Traverse pre-order with `StringBuilder`. Append `"#,"` on nulls.
- **Step 2: String Split:** On deserialize, split by delimiter into an array or queue.
- **Step 3: Deserialize DFS:** Read tokens sequentially from queue. Return null on `"#"` or construct node and recurse children.
- **Step 4: Return Root:** Return reconstructed root node.

#### 4.3 Alternative Approaches Analysis
- **Approach 2: BFS Level-Order Serialization:**
  - Serialize using `Queue<TreeNode>`: enqueue root, write `node.val` or `"#"` per step.
  - Deserialize using `Queue<TreeNode>`: read tokens in pairs representing left and right children.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Pre-Order DFS with Null Markers | Approach 2: BFS Level-Order Queue |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | $O(N)$ / $O(N)$ / $O(N)$ | $O(N)$ / $O(N)$ / $O(N)$ |
| **Auxiliary Space** | $O(N)$ recursion stack + token queue | $O(W)$ queue width |
| **Output Space** | $O(N)$ serialized string | $O(N)$ serialized string |
| **Cache Locality** | High | High |
| **In-Place Mutability** | Non-destructive | Non-destructive |
| **Streaming Suitability** | High (can stream token by token) | Moderate |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #115 - Serialize and Deserialize Binary Tree
// Core Pattern: Pre-Order DFS with Explicit Null Sentinels / FIFO Queue Reconstruction
// Primary Invariant: Pre-order traversal with '#' null markers uniquely encodes tree topology.
// Duplicate Defense: Does not rely on value uniqueness (handles duplicate node values).
// Memory Defense: StringBuilder eliminates O(N^2) string allocation churn.
// ============================================================================
```

#### Implementation 1: Pre-Order DFS with Null Tokens (Production Standard)
```csharp
public class Codec
{
    private const string NullToken = "#";
    private const char Delimiter = ',';

    // Encodes a tree to a single string using Pre-Order DFS with null sentinels
    public string serialize(TreeNode root)
    {
        var sb = new System.Text.StringBuilder();
        BuildString(root, sb);
        return sb.ToString();
    }

    private static void BuildString(TreeNode node, System.Text.StringBuilder sb)
    {
        // Null Sentinel Gate: Explicitly record '#' to seal leaf boundaries
        if (node == null)
        {
            sb.Append(NullToken).Append(Delimiter);
            return;
        }

        // Pre-order sequence: Root -> Left Subtree -> Right Subtree
        sb.Append(node.val).Append(Delimiter);
        BuildString(node.left, sb);
        BuildString(node.right, sb);
    }

    // Decodes encoded string back to original binary tree using a FIFO Queue
    public TreeNode deserialize(string data)
    {
        if (string.IsNullOrEmpty(data)) return null;

        // Split tokens using delimiter
        string[] tokens = data.Split(Delimiter, StringSplitOptions.RemoveEmptyEntries);
        var queue = new Queue<string>(tokens);

        return BuildTree(queue);
    }

    private static TreeNode BuildTree(Queue<string> queue)
    {
        if (queue.Count == 0) return null;

        string token = queue.Dequeue();

        // Null token resolves to null child reference
        if (token == NullToken)
        {
            return null;
        }

        // Instantiate node and recursively reconstruct its left and right subtrees in-order
        var node = new TreeNode(int.Parse(token));
        node.left = BuildTree(queue);
        node.right = BuildTree(queue);

        return node;
    }
}
```

#### Implementation 2: BFS Level-Order Serialization
```csharp
public class CodecBfs
{
    private const string NullToken = "#";
    private const char Delimiter = ',';

    public string serialize(TreeNode root)
    {
        if (root == null) return string.Empty;

        var sb = new System.Text.StringBuilder();
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        while (queue.Count > 0)
        {
            TreeNode curr = queue.Dequeue();

            if (curr == null)
            {
                sb.Append(NullToken).Append(Delimiter);
            }
            else
            {
                sb.Append(curr.val).Append(Delimiter);
                queue.Enqueue(curr.left);
                queue.Enqueue(curr.right);
            }
        }

        return sb.ToString();
    }

    public TreeNode deserialize(string data)
    {
        if (string.IsNullOrEmpty(data)) return null;

        string[] tokens = data.Split(Delimiter, StringSplitOptions.RemoveEmptyEntries);
        if (tokens.Length == 0 || tokens[0] == NullToken) return null;

        var root = new TreeNode(int.Parse(tokens[0]));
        var queue = new Queue<TreeNode>();
        queue.Enqueue(root);

        int i = 1;
        while (queue.Count > 0 && i < tokens.Length)
        {
            TreeNode parent = queue.Dequeue();

            // Left child
            if (tokens[i] != NullToken)
            {
                parent.left = new TreeNode(int.Parse(tokens[i]));
                queue.Enqueue(parent.left);
            }
            i++;

            // Right child
            if (i < tokens.Length && tokens[i] != NullToken)
            {
                parent.right = new TreeNode(int.Parse(tokens[i]));
                queue.Enqueue(parent.right);
            }
            i++;
        }

        return root;
    }
}
```

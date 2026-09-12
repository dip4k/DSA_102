# Phase 04: Prefix Sum & Subarray Patterns

> **Focus:** Cumulative Running States, Balance Points, Prefix Sum + HashMap (`P[j] - P[i-1] = K`), Kadane's Algorithm, Divide & Conquer Parallel Sums, and Immutable Range Queries.  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 4 (Problems #19–#23)

---

## 19. Running Sum of 1d Array (LeetCode #1480)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⚪ Reinforcement |
| **Pattern Tags** | `#prefix-sum` `#running-sum` `#array` `#in-place-vs-immutable` |
| **LeetCode Link** | [Running Sum of 1d Array](https://leetcode.com/problems/running-sum-of-1d-array/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array `nums`. We define a running sum of an array as `runningSum[i] = sum(nums[0..i])`. Return the running sum of `nums`.
- **Key Constraints:**
  - `1 <= nums.Length <= 1000`
  - `-10^6 <= nums[i] <= 10^6`
- **Senior Edge Cases to Defend:**
  - `nums.Length == 1`: Loop does not execute; immediately returns `[nums[0]]`.
  - Integer overflow: While $1000 \times 10^6 = 10^9$ safely fits within a standard 32-bit signed integer (`int.MaxValue` $\approx 2.14 \times 10^9$), production streaming systems must defend against wider ranges using 64-bit accumulators if bounds expand.
  - Caller Side-Effects: Mutating `nums` in-place alters caller memory, which violates functional purity and thread safety in multi-threaded environments. Always clarify whether in-place mutation or immutable output allocation is desired.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Accumulate running prefix sums such that each element stores the cumulative sum from the origin up to its position: $P[i] = P[i-1] + nums[i]$.
- **Sample 1:**
  - **Input:** `nums = [1, 2, 3, 4]`
  - **Running Calculations:** `[1, 1+2, 1+2+3, 1+2+3+4]`
  - **Output:** `[1, 3, 6, 10]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine an odometer in a vehicle traveling along a highway. As the car completes each individual segment of the journey, the odometer does not recalculate the total distance traveled by driving back to the starting point and recounting every mile marker. Instead, it simply adds the length of the latest segment to the existing reading on the dashboard. The accumulated distance of the past is preserved as settled state.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force calculation computes each prefix sum independently:
$$\text{runningSum}[i] = \sum_{j=0}^{i} \text{nums}[j]$$
- For index $0$: $1$ operation.
- For index $1$: $2$ operations.
- For index $N-1$: $N$ operations.
- Total Additions: $\sum_{i=1}^{N} i = \frac{N(N+1)}{2} = O(N^2)$ additions.
- Redundancy: The sub-prefix $\text{nums}[0 \dots i-1]$ is summed over and over again $N - i$ times across subsequent iterations.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**The Dynamic Prefix Recurrence:**
Every prefix sum satisfies an exact first-order recurrence relation:
$$\text{runningSum}[i] = \text{runningSum}[i - 1] + \text{nums}[i] \quad (\forall i \ge 1)$$
With base case:
$$\text{runningSum}[0] = \text{nums}[0]$$
By caching the immediate predecessor's accumulated sum, each new prefix sum is derived in strictly **$1$ addition**, reducing overall runtime from $O(N^2)$ to $O(N)$.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
```text
[ 0 ................. i - 1 ]   [   i   ]   [ i + 1 ................. N - 1 ]
└─────────────┬─────────────┘   └───┬───┘   └──────────────┬───────────────┘
     Settled Prefix States          Active          Unprocessed Raw Elements
    result[j] = sum(0..j)          Element             (Future Stream)
```
- Cursor `i`: Sweeps linearly from $1$ to $N - 1$.
- Region `0 .. i - 1`: Completely evaluated and immutable running sums.
- Cell `i`: Transformed using `result[i - 1] + nums[i]`.

#### 3.5 State Transition Triggers & Decision Gates
1. **Base Ingestion:** `result[0] = nums[0]`.
2. **Inductive Transfer Gate ($1 \le i < N$):**
   - Read settled predecessor: `prev = result[i - 1]`.
   - Add current element: `result[i] = prev + nums[i]`.
3. **Completion Gate:** When $i = N$, emit `result`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `nums = [1, 2, 3, 4]`.

| Step $i$ | Raw Element `nums[i]` | Prior Settled `result[i-1]` | Computation | Stored `result[i]` | Invariant State |
| :---: | :---: | :---: | :---: | :---: | :---: |
| **0** | `1` | — | Base assignment | `1` | $\sum_{0}^{0} = 1$ |
| **1** | `2` | `1` | $1 + 2$ | `3` | $\sum_{0}^{1} = 3$ |
| **2** | `3` | `3` | $3 + 3$ | `6` | $\sum_{0}^{2} = 6$ |
| **3** | `4` | `6` | $6 + 4$ | `10` | $\sum_{0}^{3} = 10$ |

Final Output: `[1, 3, 6, 10]`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (New Array Allocation - Optimal for API Safety):** Allocates a fresh `int[N]` result array. Leaves the input buffer completely untouched. This is the required pattern in production microservices where parameters may be passed by reference or shared across async tasks.
- **Approach 2 (In-Place Mutation - Optimal for Memory Constraints):** Directly overwrites `nums[i] += nums[i-1]`. Zero additional allocations; $O(1)$ auxiliary space. Use when embedded systems or high-frequency loops demand absolute zero GC pressure.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Edge Validation:** If `nums` is null or empty, return empty array.
- **Step 2: Buffer Allocation:** Allocate `int[] result = new int[nums.Length]`.
- **Step 3: Base Initialization:** `result[0] = nums[0]`.
- **Step 4: Accumulation Loop:** Iterate $i = 1 \dots nums.Length - 1$, setting `result[i] = result[i - 1] + nums[i]`.
- **Step 5: Return:** Return `result`.

#### 4.3 Alternative Approaches Analysis
- **SIMD / Vectorized Prefix Sum (Hillis-Steele):** Using SIMD vector instructions (`Vector<int>`), parallel prefix scans can achieve $O(\log N)$ parallel depth on GPU architectures. For standard sequential CPU execution ($N \le 1000$), standard linear scan is far simpler, perfectly cache-friendly, and outperforms vectorization setup overhead.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Dimension | Approach 1: Immutable Output Array | Approach 2: In-Place Mutation |
| :--- | :--- | :--- |
| **Time Complexity** | `O(N)` | `O(N)` |
| **Auxiliary Space** | `O(1)` (excluding output) | `O(1)` |
| **Output Space** | `O(N)` (new array allocated) | `O(1)` (input reused) |
| **Thread Safety** | Safe (pure function, no mutation) | Unsafe (mutates shared array) |
| **Cache Locality** | Sequential read and sequential write | Single sequential read-modify-write stream |
| **Streaming Suitability** | Yes | No (requires mutable array buffer) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Running Sum of 1d Array (#1480)
// Core Pattern: First-Order Prefix Sum Accumulation
// Target Complexity: O(N) Time, O(1) Auxiliary Space
// Key Invariant: result[i] = result[i - 1] + nums[i]
// ============================================================================

public class Solution
{
    // ------------------------------------------------------------------------
    // Approach 1: Immutable Result Allocation (Production Standard)
    // Preserves caller data integrity and enforces functional purity.
    // ------------------------------------------------------------------------
    public int[] RunningSum(int[] nums)
    {
        if (nums == null || nums.Length == 0)
        {
            return Array.Empty<int>();
        }

        int[] result = new int[nums.Length];

        // Base Case: Index 0 running sum is identically the first element
        result[0] = nums[0];

        // Inductive Step: Accumulate running total in single linear pass
        for (int i = 1; i < nums.Length; i++)
        {
            // Invariant: result[i - 1] contains the exact sum of nums[0 .. i - 1]
            result[i] = result[i - 1] + nums[i];
        }

        return result;
    }
}

// ----------------------------------------------------------------------------
// Approach 2: In-Place Memory Reuse (Embedded / Zero-GC Alternative)
// ----------------------------------------------------------------------------
public class SolutionInPlace
{
    public int[] RunningSum(int[] nums)
    {
        if (nums == null || nums.Length <= 1)
        {
            return nums ?? Array.Empty<int>();
        }

        // Directly accumulate into input buffer to achieve O(1) output space
        for (int i = 1; i < nums.Length; i++)
        {
            nums[i] += nums[i - 1];
        }

        return nums;
    }
}
```

---

## 20. Find Pivot Index (LeetCode #724)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#prefix-sum` `#balance-point` `#total-sum` `#algebraic-reduction` |
| **LeetCode Link** | [Find Pivot Index](https://leetcode.com/problems/find-pivot-index/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array of integers `nums`, calculate the pivot index of this array. The pivot index is the index where the sum of all the numbers strictly to the left of the index is equal to the sum of all the numbers strictly to the index's right. If no such index exists, return `-1`. If there are multiple pivot indices, you should return the **leftmost** pivot index.
- **Key Constraints:**
  - `1 <= nums.Length <= 10^4`
  - `-1000 <= nums[i] <= 1000`
- **Senior Edge Cases to Defend:**
  - Pivot at boundary index $0$: Left sum is $0$ by definition. If elements at $1 \dots N-1$ sum to $0$, index $0$ is a valid pivot.
  - Pivot at boundary index $N - 1$: Right sum is $0$ by definition. If elements at $0 \dots N-2$ sum to $0$, index $N - 1$ is a valid pivot.
  - Negative numbers: The presence of negative numbers means running sums do not increase monotonically. A binary search or two-pointer approach will fail; exact algebraic prefix balance is required.
  - Multiple valid pivot indices: Must return the first (leftmost) index encountered.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Exploit the conservation of total sum: $\text{rightSum} = \text{totalSum} - \text{leftSum} - nums[i]$. A pivot occurs when $\text{leftSum} == \text{rightSum}$.
- **Sample 1:**
  - **Input:** `nums = [1, 7, 3, 6, 5, 6]`
  - **Calculations:**
    - At index $3$ (`nums[3] = 6`): $\text{leftSum} = 1 + 7 + 3 = 11$.
    - $\text{rightSum} = 5 + 6 = 11$.
  - **Output:** `3`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Picture a balanced seesaw. You have a long beam supporting an array of weights. You are looking for an index to position the fulcrum so that the beam balances horizontally. Any weight placed directly atop the fulcrum sits on the pivot point and exerts zero torque on either side. Instead of weighing both sides separately at every candidate position, you pre-weigh the entire beam once. At any candidate position $i$, you know that whatever weight is not on the left and not on the fulcrum must reside on the right.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force scan tests each candidate index $i \in [0, N - 1]$:
- Sum left slice $\sum_{j=0}^{i-1} \text{nums}[j]$ in $O(i)$.
- Sum right slice $\sum_{j=i+1}^{N-1} \text{nums}[j]$ in $O(N - i)$.
- Compare left and right sums.
- Overall complexity: $\sum_{i=0}^{N-1} (N - 1) = O(N^2)$ operations. For $N = 10^4$, this wastes $\approx 10^8$ operations recalculating identical sub-slices.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Conservation of Total Sum:**
Let $\text{totalSum} = \sum_{j=0}^{N-1} \text{nums}[j]$. For any candidate index $i$:
$$\text{totalSum} = \text{leftSum}_i + \text{nums}[i] + \text{rightSum}_i$$
Rearranging algebraically:
$$\text{rightSum}_i = \text{totalSum} - \text{leftSum}_i - \text{nums}[i]$$
The pivot condition $\text{leftSum}_i == \text{rightSum}_i$ reduces directly to:
$$\text{leftSum}_i == \text{totalSum} - \text{leftSum}_i - \text{nums}[i] \iff 2 \cdot \text{leftSum}_i + \text{nums}[i] == \text{totalSum}$$
With $\text{totalSum}$ computed in an initial pass, we evaluate $\text{rightSum}_i$ in strictly **$O(1)$ time** during the second pass.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
```text
[ 0 ......... i - 1 ]         [   i   ]         [ i + 1 ......... N - 1 ]
└─────────┬─────────┘         └───┬───┘         └───────────┬───────────┘
       leftSum                     Pivot                  rightSum
 (Explicit Scalar)               Candidate            (Derived in O(1))
```
- Pass 1: Computes scalar `totalSum`.
- Pass 2: Pointer $i$ advances from $0$ to $N - 1$.
- Scalar `leftSum`: Holds $\sum_{j=0}^{i-1} \text{nums}[j]$ before examining index $i$.

#### 3.5 State Transition Triggers & Decision Gates
1. **Pass 1:** `totalSum = sum(nums)`.
2. **Pass 2 (Evaluation at index $i$):**
   - **Balance Check Gate:** Does `leftSum == totalSum - leftSum - nums[i]`?
     - If **True**: Return $i$ immediately (guarantees leftmost match).
   - **Accumulation Gate:** `leftSum += nums[i]`.
3. **Exhaustion Gate:** If loop finishes without triggering balance, return `-1`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `nums = [1, 7, 3, 6, 5, 6]`. $\text{totalSum} = 1 + 7 + 3 + 6 + 5 + 6 = 28$.

| Index $i$ | Element `nums[i]` | `leftSum` | Derived `rightSum` ($28 - \text{left} - \text{nums}[i]$) | Balance Check (`left == right`) | Next `leftSum` |
| :---: | :---: | :---: | :---: | :---: | :---: |
| **0** | `1` | `0` | $28 - 0 - 1 = 27$ | $0 \ne 27$ | $0 + 1 = 1$ |
| **1** | `7` | `1` | $28 - 1 - 7 = 20$ | $1 \ne 20$ | $1 + 7 = 8$ |
| **2** | `3` | `8` | $28 - 8 - 3 = 17$ | $8 \ne 17$ | $8 + 3 = 11$ |
| **3** | `6` | `11` | $28 - 11 - 6 = 11$ | **$11 == 11$ (MATCH!)** | **Return 3** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Two-Pass Constant-Space Scan - Optimal):** The industry-standard approach. Pass 1 aggregates `totalSum`; Pass 2 identifies the balance point. $O(N)$ time, $O(1)$ space.
- **Approach 2 (Prefix and Suffix Arrays):** Allocates `prefix[N]` and `suffix[N]`. While conceptually straightforward, it consumes $2N$ extra heap memory with zero performance gain over Approach 1.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Input Validation:** Return `-1` if array is empty.
- **Step 2: Aggregate Total:** Sum all elements in `nums` into `totalSum`.
- **Step 3: Linear Balance Scan:** Initialize `leftSum = 0`. Iterate $i = 0 \dots N - 1$.
  - Evaluate algebraic balance condition.
  - If equal, return $i$.
  - Add `nums[i]` to `leftSum`.
- **Step 4: Default Exit:** If no index satisfies the equation, return `-1`.

#### 4.3 Alternative Approaches Analysis
- **Two Pointers Inward:** An opposing two-pointer technique ($L=0, R=N-1$) moving toward each other fails because negative numbers destroy monotonicity (moving a pointer inward might increase or decrease the sum arbitrarily).

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Dimension | Approach 1: Two-Pass Scalar Balance (Optimal) | Approach 2: Prefix & Suffix Arrays |
| :--- | :--- | :--- |
| **Time Complexity** | `O(N)` (Pass 1: $N$, Pass 2: $\le N$) | `O(N)` (3 linear passes) |
| **Auxiliary Space** | `O(1)` (scalars on stack) | `O(N)` (two length-$N$ arrays) |
| **Memory Access Pattern** | Sequential cache-line read | Multiple array passes, higher cache churn |
| **Negative Numbers Support** | Fully supported | Fully supported |
| **Streaming Suitability** | Requires 2 passes (bounded stream) | Requires 3 passes (bounded stream) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Find Pivot Index (#724)
// Core Pattern: Algebraic Prefix Balance Reduction
// Target Complexity: O(N) Time, O(1) Auxiliary Space
// Key Invariant: rightSum = totalSum - leftSum - nums[i]
// ============================================================================

public class Solution
{
    public int PivotIndex(int[] nums)
    {
        // --------------------------------------------------------------------
        // Defensive Guard: Validate input existence
        // --------------------------------------------------------------------
        if (nums == null || nums.Length == 0)
        {
            return -1;
        }

        // --------------------------------------------------------------------
        // Pass 1: Pre-calculate total sum of the entire array
        // --------------------------------------------------------------------
        int totalSum = 0;
        foreach (int num in nums)
        {
            totalSum += num;
        }

        // --------------------------------------------------------------------
        // Pass 2: Evaluate balance point at each candidate index
        // Invariant: At index i, leftSum represents sum(nums[0 .. i - 1])
        // --------------------------------------------------------------------
        int leftSum = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            // Algebraic deduction:
            // totalSum = leftSum + nums[i] + rightSum
            // => rightSum = totalSum - leftSum - nums[i]
            if (leftSum == totalSum - leftSum - nums[i])
            {
                // Return immediately upon first discovery to satisfy the
                // leftmost pivot constraint mandated by the specification
                return i;
            }

            // Accumulate current element into leftSum before moving to next candidate
            leftSum += nums[i];
        }

        return -1;
    }
}
```

---

## 21. Subarray Sum Equals K (LeetCode #560)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#prefix-sum` `#hash-map` `#complement-lookup` `#negative-numbers` |
| **LeetCode Link** | [Subarray Sum Equals K](https://leetcode.com/problems/subarray-sum-equals-k/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array of integers `nums` and an integer `k`, return the total number of subarrays whose sum equals to `k`. A subarray is a contiguous non-empty sequence of elements within an array.
- **Key Constraints:**
  - `1 <= nums.Length <= 2 * 10^4`
  - `-1000 <= nums[i] <= 1000`
  - `-10^7 <= k <= 10^7`
- **Senior Edge Cases & Critical Defenses:**
  - **Negative Numbers Invalidate Sliding Window:** A sliding window *cannot* solve this problem. In a sliding window, expanding $right$ must monotonically increase the sum, and advancing $left$ must monotonically decrease it. Negative values destroy this monotonicity completely: expanding can reduce the sum, and shrinking can increase it. A hash map of prefix sums is strictly necessary.
  - Subarrays starting at index $0$: Subarrays of the form `nums[0..j]` have sum equal to $P[j]$. When $P[j] = k$, we need $P[j] - k = 0$. Hence, the prefix sum $0$ must have an initial frequency of $1$ (`prefixCounts[0] = 1`).
  - Target $k = 0$: Valid subarrays can cancel out internally (e.g. `[1, -1, 1, -1]`).
  - Hash map capacity pre-allocation: Allocating initial capacity `nums.Length + 1` prevents costly internal dictionary rehashing.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Use the prefix difference identity:
  $$\sum_{m=i}^{j} \text{nums}[m] = P[j] - P[i-1] = k \iff P[i-1] = P[j] - k$$
- **Sample 1:**
  - **Input:** `nums = [1, 1, 1]`, `k = 2` $\implies$ `2` (`[1, 1]` at indices `0..1` and `1..2`)
- **Sample 2 (Negative Numbers):**
  - **Input:** `nums = [1, -1, 1, -1]`, `k = 0` $\implies$ `4`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine hiking along an undulating mountain trail. At every milestone $j$, your altimeter reads your cumulative altitude $P[j]$. You want to discover how many earlier viewpoints $i-1$ you passed that were at an altitude exactly $k$ meters below your current altitude:
$$P[i-1] = P[j] - k$$
Instead of turning around and hiking backward to re-check all previous milestones, you carry a ledger. Whenever you stand at altitude $P[j]$, you consult your ledger: *"How many times in the past have I stood at altitude $P[j] - k$?"* If the ledger says 3 times, then exactly 3 distinct trails ending at your current position have an altitude gain of $k$. You then record your current altitude into the ledger and continue forward.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force scan enumerates all pairs $(i, j)$ with $0 \le i \le j < N$:
- Summing `nums[i..j]` takes $O(N)$ without prefix arrays $\implies O(N^3)$ total time.
- Even with a precomputed prefix array $P$, evaluating all $\frac{N(N+1)}{2}$ pairs takes $O(N^2)$ time.
- For $N = 2 \times 10^4$, $N^2 = 4 \times 10^8$ iterations, which triggers a TLE in competitive and production SLA environments.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**The Prefix Sum Complement Identity:**
Any contiguous subarray sum can be expressed as the difference of two prefix sums:
$$\text{Sum}(nums[i \dots j]) = P[j] - P[i - 1]$$
We require:
$$P[j] - P[i - 1] = k \iff P[i - 1] = P[j] - k$$
At current index $j$ with running sum $P[j]$, any historical index $i - 1$ whose prefix sum was $P[j] - k$ forms a valid subarray ending at $j$.
By maintaining a frequency map of seen prefix sums:
- Looking up `prefixCounts[P[j] - k]` provides the exact count of valid subarrays ending at index $j$ in **$O(1)$ amortized time**.
- **Base Case Invariant:** `prefixCounts[0] = 1`. This accounts for valid subarrays that start at index $0$ (where $i - 1 = -1$ corresponds to an empty prefix with sum $0$).

#### 3.4 Cursor Semantics & Invariant Partition Architecture
```text
[ 0 ................. i - 1 ]   [ i ..................... j ]   [ j + 1 ......... N - 1 ]
└─────────────┬─────────────┘   └─────────────┬─────────────┘   └───────────┬───────────┘
        Prefix P[i-1]                 Valid Subarray                  Unexplored
      Stored in HashMap             Sum = P[j]-P[i-1] = k               Future
```
- Cursor $j$: Current element being processed.
- `currentSum`: Cumulative prefix sum $P[j]$.
- `targetPrefix = currentSum - k`: The exact historical prefix sum required to form sum $k$.
- `prefixCounts`: Map storing `{ prefixSum : occurrenceFrequency }`.

#### 3.5 State Transition Triggers & Decision Gates
1. **Bootstrap Gate:** Initialize `prefixCounts[0] = 1`. Set `currentSum = 0`, `totalSubarrays = 0`.
2. **Iteration Step ($j \in [0, N - 1]$):**
   - Accumulate: `currentSum += nums[j]`.
   - Calculate complement: `targetPrefix = currentSum - k`.
   - **Lookup Gate:** If `prefixCounts.TryGetValue(targetPrefix, out int count)`:
     - `totalSubarrays += count`.
   - **Registration Gate:** `prefixCounts[currentSum] = prefixCounts.GetValueOrDefault(currentSum, 0) + 1`.
3. **Termination:** Return `totalSubarrays`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `nums = [1, -1, 1, 1, 1]`, `k = 2`.
Initial State: `prefixCounts = { 0: 1 }`, `totalSubarrays = 0`.

| Step $j$ | Element `nums[j]` | `currentSum` | `targetPrefix` ($curr - 2$) | Matches in Map? | Count Added | `totalSubarrays` | Updated `prefixCounts` |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :--- |
| **Init** | — | `0` | — | — | — | `0` | `{ 0:1 }` |
| **0** | `1` | `1` | $1 - 2 = -1$ | No | 0 | 0 | `{ 0:1, 1:1 }` |
| **1** | `-1` | `0` | $0 - 2 = -2$ | No | 0 | 0 | `{ 0:2, 1:1 }` |
| **2** | `1` | `1` | $1 - 2 = -1$ | No | 0 | 0 | `{ 0:2, 1:2 }` |
| **3** | `1` | `2` | $2 - 2 = 0$ | **Yes (`0` has count 2)** | +2 | **2** | `{ 0:2, 1:2, 2:1 }` |
| **4** | `1` | `3` | $3 - 2 = 1$ | **Yes (`1` has count 2)** | +2 | **4** | `{ 0:2, 1:2, 2:1, 3:1 }` |

Subarrays found:
- At $j=3$ (`curr=2`):
  1. From empty prefix (idx 0..3: `[1, -1, 1, 1]`, sum = 2)
  2. From prefix at idx 1 (idx 2..3: `[1, 1]`, sum = 2)
- At $j=4$ (`curr=3`):
  1. From prefix at idx 0 (idx 1..4: `[-1, 1, 1, 1]`, sum = 2)
  2. From prefix at idx 2 (idx 3..4: `[1, 1]`, sum = 2)

Final Result: `4`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Prefix Sum + HashMap - Optimal):** The only viable linear solution for arrays with arbitrary (positive, zero, and negative) numbers. $O(N)$ time, $O(N)$ space.
- **Why Not Sliding Window?** Monotonicity is violated. Mentioning this distinction in senior engineering interviews proves deep structural mastery.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Input Validation:** Return 0 if array is null or empty.
- **Step 2: Dictionary Setup:** Create a `Dictionary<int, int>` with initial capacity `nums.Length + 1`. Insert seed entry `prefixCounts[0] = 1`.
- **Step 3: Linear Exploration:** Loop through each number in `nums`.
  - Add to `currentSum`.
  - Check for `currentSum - k` in dictionary; add matching occurrences to `totalSubarrays`.
  - Increment frequency of `currentSum` in dictionary.
- **Step 4: Return:** Return `totalSubarrays`.

#### 4.3 Alternative Approaches Analysis
- **Cumulative Prefix Array with Double Loop:** Build prefix array `P` of length $N + 1$. Iterate all $i < j$, checking if $P[j] - P[i] == k$. Time: $O(N^2)$, Space: $O(N)$. Useful only if memory is extremely constrained and hashing overhead cannot be tolerated, but unacceptable for large inputs.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Dimension | Approach 1: Prefix Sum + HashMap (Optimal) | Approach 2: Prefix Array Brute-Force |
| :--- | :--- | :--- |
| **Time Complexity** | `O(N)` average, `O(N^2)` worst-case hash collisions | `O(N^2)` deterministic |
| **Auxiliary Space** | `O(N)` (stores up to $N + 1$ unique prefix sums) | `O(N)` (stores $N + 1$ integers) |
| **Dictionary Overhead** | Boxing-free primitive types (`<int, int>`) | Zero hashing overhead |
| **Negative Value Handling**| Fully supported | Fully supported |
| **Streaming Suitability** | Yes (can run continuously on unbounded streams) | No |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Subarray Sum Equals K (#560)
// Core Pattern: Prefix Sum Difference with Hash Complement Lookup
// Target Complexity: O(N) Time, O(N) Auxiliary Space
// Key Invariant: P[j] - P[i-1] = k <=> P[i-1] = P[j] - k
// ============================================================================

public class Solution
{
    public int SubarraySum(int[] nums, int k)
    {
        // --------------------------------------------------------------------
        // Defensive Guard: Validate input existence
        // --------------------------------------------------------------------
        if (nums == null || nums.Length == 0)
        {
            return 0;
        }

        // --------------------------------------------------------------------
        // Pre-allocate Dictionary capacity to avoid dynamic rehashing during growth.
        // Map stores: { prefixSum : numberOfTimesEncounteredSoFar }
        // --------------------------------------------------------------------
        var prefixCounts = new Dictionary<int, int>(capacity: nums.Length + 1);

        // Crucial Base Invariant:
        // A prefix sum of 0 has occurred exactly once before processing index 0
        // (representing the empty subarray prefix). This ensures subarrays starting
        // at index 0 whose sum equals k (currentSum - k == 0) are correctly tallied.
        prefixCounts[0] = 1;

        int currentSum = 0;
        int totalSubarrays = 0;

        // --------------------------------------------------------------------
        // Single Pass Traversal: Update running sum and query complement
        // --------------------------------------------------------------------
        foreach (int num in nums)
        {
            currentSum += num;

            // Algebraic identity:
            // currentSum - targetPrefix = k => targetPrefix = currentSum - k
            int targetPrefix = currentSum - k;

            // Check if complement prefix sum exists in our historical ledger
            if (prefixCounts.TryGetValue(targetPrefix, out int matchingCount))
            {
                totalSubarrays += matchingCount;
            }

            // Register current prefix sum into the ledger
            // CollectionsMarshal.GetValueRefOrAddDefault could be used in .NET 6+
            // for zero-lookup insertion, but GetValueOrDefault is universally portable.
            prefixCounts[currentSum] = prefixCounts.GetValueOrDefault(currentSum, 0) + 1;
        }

        return totalSubarrays;
    }
}
```

---

## 22. Maximum Subarray (LeetCode #53)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#kadanes-algorithm` `#dynamic-programming` `#divide-and-conquer` `#greedy-reset` |
| **LeetCode Link** | [Maximum Subarray](https://leetcode.com/problems/maximum-subarray/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an integer array `nums`, find the subarray with the largest sum, and return its sum.
- **Key Constraints:**
  - `1 <= nums.Length <= 10^5`
  - `-10^4 <= nums[i] <= 10^4`
- **Senior Edge Cases to Defend:**
  - All negative numbers (e.g. `nums = [-5, -2, -8, -1]`): Returning `0` is a catastrophic failure. The answer must be `-1` (the single maximum negative element). `currentMax` and `globalMax` must be initialized strictly to `nums[0]`.
  - Single element array (e.g. `nums = [-10]`): Loop should not iterate; return `nums[0]`.
  - Very large sums: Max sum could reach $10^5 \times 10^4 = 10^9$, which safely fits within a 32-bit signed integer (`int`), but accumulators should be tracked cleanly without overflow risk.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** At each element, decide whether to extend the previous running subarray or discard it and start fresh, capturing the maximum contiguous sum in $O(N)$ time.
- **Sample 1:**
  - **Input:** `nums = [-2, 1, -3, 4, -1, 2, 1, -5, 4]`
  - **Optimal Subarray:** `[4, -1, 2, 1]`
  - **Output:** `6`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Think of financial risk management. You are managing an investment portfolio day by day. Every day brings a gain or a loss. If the cumulative balance of your current portfolio ever drops below zero, that portfolio has become a toxic liability. Carrying negative equity forward into tomorrow will strictly reduce whatever gains tomorrow might bring. The mathematically optimal decision is to declare bankruptcy on the past, wipe the slate clean, and start a brand-new fund beginning today.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force scan enumerates all $O(N^2)$ subarrays and computes their sums:
$$\text{MaxSum} = \max_{0 \le i \le j < N} \sum_{k=i}^{j} \text{nums}[k]$$
- Total Time: $O(N^2)$ with running accumulation, or $O(N^3)$ with naive re-summing.
- For $N = 10^5$, $N^2 = 10^{10}$ operations, causing an immediate TLE.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Kadane's Dynamic Recurrence (Greedy State Reset):**
Let $dp[i]$ be the maximum subarray sum that **must end at index $i$**.
At index $i$, we face a binary choice:
1. Extend the best subarray ending at $i - 1$: $dp[i - 1] + nums[i]$
2. Start a fresh subarray consisting solely of $nums[i]$: $nums[i]$
$$\therefore dp[i] = \max(nums[i], dp[i - 1] + nums[i])$$
Notice that $nums[i]$ is common to both terms:
$$dp[i] = nums[i] + \max(0, dp[i - 1])$$
If $dp[i - 1] < 0$, it is a net deficit; we discard it ($0$). If $dp[i - 1] \ge 0$, it is an asset; we retain it.
Because $dp[i]$ depends only on $dp[i - 1]$, we reduce the $O(N)$ DP table to a single scalar `currentMax`, achieving **$O(1)$ auxiliary space**.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
```text
[ 0 ......... i - 1 ]         [   i   ]         [ i + 1 ......... N - 1 ]
└─────────┬─────────┘         └───┬───┘         └───────────┬───────────┘
   Historical Best Streak        Current               Unexplored Future
   (Reset to 0 if negative)      Element
```
- `currentMax`: The maximum subarray sum ending strictly at the current index.
- `globalMax`: The maximum subarray sum observed anywhere in the array so far.

#### 3.5 State Transition Triggers & Decision Gates
1. **Bootstrap:** `currentMax = nums[0]`, `globalMax = nums[0]`.
2. **Step ($i = 1 \dots N - 1$):**
   - **Reset / Extend Gate:** `currentMax = Math.Max(nums[i], currentMax + nums[i])`.
   - **Global Extremum Gate:** `globalMax = Math.Max(globalMax, currentMax)`.
3. **Termination:** Return `globalMax`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `nums = [-2, 1, -3, 4, -1, 2, 1, -5, 4]`.

| $i$ | `nums[i]` | Choice 1: Start Fresh (`nums[i]`) | Choice 2: Extend (`curr + nums[i]`) | `currentMax` Result | `globalMax` | Decision Rationale |
| :---: | :---: | :---: | :---: | :---: | :---: | :--- |
| **0** | `-2` | `-2` | — | `-2` | `-2` | Base element |
| **1** | `1` | `1` | $-2 + 1 = -1$ | **1** | **1** | Discard negative debt ($-2$); start fresh |
| **2** | `-3` | `-3` | $1 + (-3) = -2$ | **-2** | 1 | Extend ($-2 > -3$) |
| **3** | `4` | `4` | $-2 + 4 = 2$ | **4** | **4** | Discard negative debt ($-2$); start fresh |
| **4** | `-1` | `-1` | $4 + (-1) = 3$ | **3** | 4 | Extend ($3 > -1$) |
| **5** | `2` | `2` | $3 + 2 = 5$ | **5** | **5** | Extend ($5 > 2$) |
| **6** | `1` | `1` | $5 + 1 = 6$ | **6** | **6** | Extend ($6 > 1$) |
| **7** | `-5` | `-5` | $6 + (-5) = 1$ | **1** | 6 | Extend ($1 > -5$) |
| **8** | `4` | `4` | $1 + 4 = 5$ | **5** | 6 | Extend ($5 > 4$) |

Final Maximum Subarray Sum: `6` (subarray `[4, -1, 2, 1]`).

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Kadane's Algorithm - Optimal for Single-Core):** $O(N)$ time, $O(1)$ space. Strict sequential traversal with optimal CPU cache utilization.
- **Approach 2 (Divide & Conquer - Segment Tree Merge):** Recursively splits the array into halves. Merges segments by computing 4 attributes: `TotalSum`, `LeftMax`, `RightMax`, and `MaxSubSum`.
  - **Why Learn Divide & Conquer?** Kadane's algorithm is fundamentally sequential and cannot be parallelized easily across a cluster of 1,000 machines. The Divide and Conquer approach allows MapReduce and GPU architectures to compute maximum subarrays across massive distributed datasets in parallel $O(\log N)$ time.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Input Validation:** Guard against empty array.
- **Step 2: Initialize States:** Set `currentMax` and `globalMax` to `nums[0]`.
- **Step 3: Exploration Loop:** Iterate $i = 1 \dots N - 1$.
  - Update `currentMax = Math.Max(nums[i], currentMax + nums[i])`.
  - Update `globalMax = Math.Max(globalMax, currentMax)`.
- **Step 4: Return:** Return `globalMax`.

#### 4.3 Alternative Approaches Analysis
- **Divide and Conquer Segment Tree Formulation:**
  For any segment $[L, R]$ with midpoint $M$:
  $$\text{MaxSubSum} = \max(\text{LeftSubSum}, \text{RightSubSum}, \text{Left.RightMax} + \text{Right.LeftMax})$$
  Recurrence: $T(N) = 2T(N/2) + O(1) \implies O(N)$ time by Master Theorem. Stack space: $O(\log N)$.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Dimension | Approach 1: Kadane's Algorithm (Optimal) | Approach 2: Divide & Conquer (Parallelizable) |
| :--- | :--- | :--- |
| **Time Complexity** | `O(N)` strictly linear | `O(N)` sequential ($O(\log N)$ parallel depth) |
| **Auxiliary Space** | `O(1)` strictly scalar | `O(\log N)` recursion stack |
| **Distributed / Parallel** | No (inherently sequential state) | Yes (ideal for multi-threaded / MapReduce) |
| **Cache Locality** | Sequential memory stream | Tree-based memory access |
| **In-Place Mutability** | Read-only | Read-only |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Maximum Subarray (#53)
// Core Pattern: Kadane's Algorithm & Divide and Conquer Segment Merge
// Target Complexity: O(N) Time, O(1) Auxiliary Space
// Key Invariant: currentMax[i] = max(nums[i], currentMax[i-1] + nums[i])
// ============================================================================

public class Solution
{
    // ------------------------------------------------------------------------
    // Approach 1: Kadane's Algorithm (Production Optimal O(N) Time, O(1) Space)
    // ------------------------------------------------------------------------
    public int MaxSubArray(int[] nums)
    {
        // Guard clause: Defend against invalid empty or null arrays
        if (nums == null || nums.Length == 0)
        {
            throw new ArgumentException("Array cannot be null or empty.", nameof(nums));
        }

        // Initialize strictly to nums[0] to correctly handle arrays of all negative numbers
        int currentMax = nums[0];
        int globalMax = nums[0];

        // Linear sweep: Decide at each element whether to extend or reset
        for (int i = 1; i < nums.Length; i++)
        {
            // Invariant:
            // If currentMax is negative, adding it to nums[i] makes the sum smaller
            // than nums[i] alone. Hence, reset to nums[i].
            // If currentMax >= 0, extend it by adding nums[i].
            currentMax = Math.Max(nums[i], currentMax + nums[i]);

            // Track the maximum contiguous sum observed across all examined prefixes
            if (currentMax > globalMax)
            {
                globalMax = currentMax;
            }
        }

        return globalMax;
    }
}

// ----------------------------------------------------------------------------
// Approach 2: Divide and Conquer (Parallel Segment Tree Merge Pattern)
// Used in high-performance distributed systems (MapReduce / GPU parallelism)
// ----------------------------------------------------------------------------
public class SolutionDivideAndConquer
{
    // Segment metadata representing range properties
    private readonly struct Segment
    {
        public readonly int TotalSum;
        public readonly int LeftMax;
        public readonly int RightMax;
        public readonly int MaxSubSum;

        public Segment(int totalSum, int leftMax, int rightMax, int maxSubSum)
        {
            TotalSum = totalSum;
            LeftMax = leftMax;
            RightMax = rightMax;
            MaxSubSum = maxSubSum;
        }
    }

    public int MaxSubArray(int[] nums)
    {
        if (nums == null || nums.Length == 0) return 0;
        return Divide(nums, 0, nums.Length - 1).MaxSubSum;
    }

    private static Segment Divide(int[] nums, int left, int right)
    {
        // Base Case: Single element leaf segment
        if (left == right)
        {
            int val = nums[left];
            return new Segment(val, val, val, val);
        }

        int mid = left + (right - left) / 2;

        // Conquer: Recursively solve left and right halves (can be parallelized across threads)
        Segment leftSeg = Divide(nums, left, mid);
        Segment rightSeg = Divide(nums, mid + 1, right);

        // Combine: Merge segment properties in O(1)
        int totalSum = leftSeg.TotalSum + rightSeg.TotalSum;
        int leftMax = Math.Max(leftSeg.LeftMax, leftSeg.TotalSum + rightSeg.LeftMax);
        int rightMax = Math.Max(rightSeg.RightMax, rightSeg.TotalSum + leftSeg.RightMax);

        // Best subarray is either entirely in left, entirely in right, or spans across the midpoint
        int maxSubSum = Math.Max(
            Math.Max(leftSeg.MaxSubSum, rightSeg.MaxSubSum),
            leftSeg.RightMax + rightSeg.LeftMax
        );

        return new Segment(totalSum, leftMax, rightMax, maxSubSum);
    }
}
```

---

## 23. Range Sum Query — Immutable (LeetCode #303)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⚪ Reinforcement |
| **Pattern Tags** | `#prefix-sum` `#range-queries` `#precomputation` `#sentinel-indexing` |
| **LeetCode Link** | [Range Sum Query - Immutable](https://leetcode.com/problems/range-sum-query-immutable/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an integer array `nums`, handle multiple queries of the following type: Calculate the sum of the elements of `nums` between indices `left` and `right` inclusive where `left <= right`. Implement the `NumArray` class:
  - `NumArray(int[] nums)`: Initializes the object with the integer array `nums`.
  - `int SumRange(int left, int right)`: Returns the sum of the elements of `nums` between indices `left` and `right` inclusive (i.e. `nums[left] + nums[left + 1] + ... + nums[right]`).
- **Key Constraints:**
  - `1 <= nums.Length <= 10^4`
  - `-10^5 <= nums[i] <= 10^5`
  - `0 <= left <= right < nums.Length`
  - At most $10^4$ calls will be made to `SumRange`.
- **Senior Edge Cases to Defend:**
  - $left = 0$: Query spans from the very beginning of the array. Without a 1-indexed sentinel, this requires a branch (`if (left == 0) return prefix[right];`).
  - $left = right$: Single-element range query; must return `nums[left]`.
  - Repeated high-throughput queries: $10^4$ queries against $10^4$ elements must execute in microseconds, forbidding any per-query looping.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Precompute a 1-indexed prefix sum array of size $N + 1$. Any range query $[left, right]$ evaluates in strictly $O(1)$ arithmetic via `prefix[right + 1] - prefix[left]`.
- **Sample 1:**
  - `nums = [-2, 0, 3, -5, 2, -1]`
  - `SumRange(0, 2)`: $(-2) + 0 + 3 = 1$
  - `SumRange(2, 5)`: $3 + (-5) + 2 + (-1) = -1$
  - `SumRange(0, 5)`: $(-2) + 0 + 3 + (-5) + 2 + (-1) = -3$

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Picture mile markers along a national highway. Marker 0 is at the state border (Mile 0). Marker 5 is at Mile 50. Marker 12 is at Mile 140. If a motorist asks, *"How long is the stretch of highway between Marker 5 and Marker 12?"*, the highway patrol does not dispatch a cruiser to drive and measure the asphalt between Marker 5 and Marker 12. They simply calculate:
$$140 - 50 = 90 \text{ miles}$$
By front-loading the measurement cost into milestone signposts during highway construction, every subsequent travel query is answered instantly.

#### 3.2 The Naive Bottleneck & Redundant Computation
Without precomputation, each call to `SumRange(left, right)` iterates through the array from `left` to `right`:
- Query Time: $O(R - L + 1) = O(N)$.
- Total Cost for $Q$ queries: $O(Q \cdot N)$.
- With $Q = 10^4$ and $N = 10^4$, total operations reach $10^8$. In server applications handling millions of user requests, this creates unacceptable request latency.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**The 1-Indexed Sentinel Trick (Branchless Invariant):**
Define prefix array $P$ of size $N + 1$ such that:
$$P[k] = \sum_{j=0}^{k - 1} \text{nums}[j] \quad (k \ge 1), \quad \text{with } P[0] = 0$$
For any range query $[L, R]$:
$$\sum_{j=L}^{R} \text{nums}[j] = \sum_{j=0}^{R} \text{nums}[j] - \sum_{j=0}^{L-1} \text{nums}[j] = P[R + 1] - P[L]$$
Notice the beauty of $P[0] = 0$:
When $L = 0$, the query becomes:
$$P[R + 1] - P[0] = P[R + 1] - 0 = P[R + 1]$$
This completely eliminates the need for an `if (left == 0)` conditional check inside `SumRange`. The code becomes strictly branchless, allowing CPU instruction pipelines to execute without branch misprediction penalties.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
```text
Prefix Array P (Length N + 1):
[  P[0]  ]   [ P[1] ... P[L] ]   [ P[L+1] ... P[R+1] ]   [ P[R+2] ... P[N] ]
└───┬────┘   └───────┬───────┘   └─────────┬─────────┘   └────────┬────────┘
 Sentinel         Sum before               Range Sum              Remaining
  ( = 0 )          Index L                [ L ... R ]              Elements
```
- `_prefix[0] = 0`: Dummy identity element.
- `_prefix[k]`: Stores sum of first $k$ elements ($nums[0 \dots k - 1]$).
- Query $[L, R]$ maps directly to `_prefix[R + 1] - _prefix[L]`.

#### 3.5 State Transition Triggers & Decision Gates
1. **Construction Phase:**
   - Allocate `_prefix = new int[nums.Length + 1]`.
   - Set `_prefix[0] = 0`.
   - Loop $i = 0 \dots N - 1$: `_prefix[i + 1] = _prefix[i] + nums[i]`.
2. **Query Phase (`SumRange(left, right)`):**
   - Strictly evaluate: `return _prefix[right + 1] - _prefix[left]`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `nums = [-2, 0, 3, -5, 2, -1]`.

Prefix Array Construction:

| Index $k$ | Corresponding Slice | Formula | Value `_prefix[k]` |
| :---: | :---: | :---: | :---: |
| **0** | Empty Prefix | Sentinel | `0` |
| **1** | `nums[0]` | $0 + (-2)$ | `-2` |
| **2** | `nums[0..1]` | $-2 + 0$ | `-2` |
| **3** | `nums[0..2]` | $-2 + 3$ | `1` |
| **4** | `nums[0..3]` | $1 + (-5)$ | `-4` |
| **5** | `nums[0..4]` | $-4 + 2$ | `-2` |
| **6** | `nums[0..5]` | $-2 + (-1)$ | `-3` |

Executing Queries:
- `SumRange(0, 2)`: `_prefix[3] - _prefix[0]` $= 1 - 0 = \mathbf{1}$.
- `SumRange(2, 5)`: `_prefix[6] - _prefix[2]` $= -3 - (-2) = \mathbf{-1}$.
- `SumRange(0, 5)`: `_prefix[6] - _prefix[0]` $= -3 - 0 = \mathbf{-3}$.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Prefix Sum Precomputation (Optimal):** Construction $O(N)$, Query $O(1)$, Auxiliary Space $O(N)$. When data is static (immutable) and queries are frequent, this is the globally optimal data structure.
- **Fenwick Tree (Binary Indexed Tree) or Segment Tree:** Used only when the underlying array is *mutable* (frequent point updates mixed with range queries). For immutable arrays, Fenwick/Segment trees add $O(\log N)$ query latency and unnecessary structural complexity.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Constructor Validation:** Verify `nums` is not null.
- **Step 2: Allocate Sentinel Buffer:** Allocate `_prefix` with size `nums.Length + 1`.
- **Step 3: Sequential Accumulation:** Populate `_prefix[i + 1] = _prefix[i] + nums[i]`.
- **Step 4: Branchless Query:** Implement `SumRange` returning `_prefix[right + 1] - _prefix[left]`.

#### 4.3 Alternative Approaches Analysis
- **0-Indexed Prefix Array:** Size $N$, where `prefix[i] = sum(0..i)`. Query requires an explicit branch: `left == 0 ? prefix[right] : prefix[right] - prefix[left - 1]`. The 1-indexed sentinel approach is universally preferred in production because it avoids branch misprediction on CPUs.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Dimension | Approach 1: 1-Indexed Sentinel Prefix (Optimal) | Approach 2: Naive Per-Query Scan |
| :--- | :--- | :--- |
| **Precomputation Time** | `O(N)` | `O(1)` (zero setup) |
| **Query Time** | `O(1)` strictly branchless | `O(N)` linear search |
| **Total Time ($Q$ Queries)** | `O(N + Q)` | `O(Q \cdot N)` |
| **Auxiliary Space** | `O(N)` (array of size $N + 1$) | `O(1)` |
| **Branch Predictability** | 100% branch-free query execution | Dependent on loop count |
| **Cache Behavior** | 2 memory reads per query | Sequential read of $(R - L + 1)$ elements |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Range Sum Query - Immutable (#303)
// Core Pattern: Precomputed Prefix Sum with 1-Indexed Sentinel Identity
// Target Complexity: O(N) Precomputation, O(1) Query Time, O(N) Space
// Key Invariant: SumRange(L, R) = _prefix[R + 1] - _prefix[L]
// ============================================================================

public class NumArray
{
    // Immutable prefix buffer storing cumulative sums
    private readonly int[] _prefix;

    public NumArray(int[] nums)
    {
        // Guard against null input
        if (nums == null)
        {
            throw new ArgumentNullException(nameof(nums), "Input array cannot be null.");
        }

        // Allocate buffer of size N + 1.
        // _prefix[0] is initialized to 0 by the CLR.
        // Invariant: _prefix[k] stores the exact sum of nums[0 .. k - 1].
        _prefix = new int[nums.Length + 1];

        // Populate prefix sums in a single sequential pass
        for (int i = 0; i < nums.Length; i++)
        {
            _prefix[i + 1] = _prefix[i] + nums[i];
        }
    }

    public int SumRange(int left, int right)
    {
        // --------------------------------------------------------------------
        // Defensive Range Validation (Optional in competitive, essential in prod)
        // --------------------------------------------------------------------
        if (left < 0 || right >= _prefix.Length - 1 || left > right)
        {
            throw new ArgumentOutOfRangeException("Provided range [left, right] is out of valid array bounds.");
        }

        // --------------------------------------------------------------------
        // Branchless O(1) Query Execution:
        // The 1-indexed offset eliminates any 'if (left == 0)' conditional checks.
        // When left = 0: returns _prefix[right + 1] - _prefix[0] = _prefix[right + 1] - 0
        // --------------------------------------------------------------------
        return _prefix[right + 1] - _prefix[left];
    }
}
```

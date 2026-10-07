# 📘 Week 05 Day 04 Part B: Kadane's Algorithm & Dynamic Programming — Engineering Guide

> 🧭 **Navigation:** [← Previous Day](Week_05_Day_04_Part_A_Partition_Cyclic_Sort_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_05_Day_05_Fast_Slow_Pointers_Instructional.md)
> 
> 💡 **Instructor Note:** *Not all sections or topics are mandatory. Focus on the local vs. global DP recurrence and the negative-swap product logic according to your interview timeline.*

---

## 🎯 LEARNING OBJECTIVES

By the end of this chapter, you will be able to:

- 🎯 **Internalize** the core DP invariant: tracking the optimal solution ending at the current index enables finding the global contiguous optimum in strict `O(N)` time and `O(1)` auxiliary space.
- ⚙️ **Implement** Maximum Subarray, Maximum Product Subarray, and Circular Maximum Subarray in modern C# (.NET 8/9) and Python (3.11+).
- ⚖️ **Evaluate** trade-offs between brute-force quadratic scans (`O(N^2)`), prefix sums with min-prefix tracking (`O(N)` time and space), and Kadane's space-optimized DP (`O(1)` auxiliary space).
- 🏭 **Connect** Kadane's algorithm to real systems (financial max-drawdown monitoring, high-frequency signal burst detection, packet queue backlog optimization).
- 🎙️ **Explain** the extend-versus-restart recurrence and circular complement subtraction with complete verbal clarity during a 45-minute technical interview.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Engineering Problem

Across financial market monitoring, audio signal analysis, and network packet monitoring, engineers frequently analyze continuous numerical series containing both positive and negative values. A key question is: "What is the maximum contiguous sum achievable across any contiguous subsegment?"

In an array of length `N`, there are `N * (N + 1) / 2` possible contiguous subarrays. Evaluating every subarray naively takes `O(N^2)` time. For high-frequency trading streams processing hundreds of thousands of price ticks per second, quadratic complexity is catastrophic.

While precomputing prefix sums reduces each range sum to `O(1)`, finding the maximum still requires searching pairs or storing auxiliary arrays.

Instead, **Kadane's Algorithm** solves the problem in a single linear pass with `O(1)` auxiliary space by recognizing optimal substructure: **at every index `i`, we decide whether to extend the previous subarray or start a new subarray fresh from `nums[i]`**.

> [!NOTE]
> **Production Reality (Why FAANG Tests This):**
> Financial trading engines and telemetry systems (Datadog, Prometheus) compute maximum consecutive drawdowns and sustained burst loads across streaming metrics. Calculating these statistics must execute in `O(1)` memory without buffering unbounded historical streams. FAANG interviewers test Kadane's to see if you understand how space-optimized Dynamic Programming reduces complex history to two scalar state variables.

### The Solution: The DP Recurrence

Kadane's algorithm maintains two quantities:
1. `max_ending_here`: The maximum sum of any non-empty subarray that **ends exactly at index `i`**.
2. `max_so_far`: The maximum subarray sum found anywhere across `nums[0..i]`.

The recurrence relation is:
```
max_ending_here[i] = max(nums[i], max_ending_here[i - 1] + nums[i])
max_so_far[i] = max(max_so_far[i - 1], max_ending_here[i])
```

If `max_ending_here[i - 1]` is negative, adding it to `nums[i]` produces a sum smaller than `nums[i]` alone. Hence, starting fresh from `nums[i]` is strictly superior.

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### The Core Analogy

Imagine hiking along an undulating trail while carrying an accumulated pack weight. At each step, you can either carry forward the momentum from your current march or drop the old march and declare the current spot as your brand new starting trailhead:
- If your previous march has accumulated positive elevation gain, adding the current hill keeps your momentum growing.
- If your previous march has plunged into a net-negative ditch, dragging that debt forward hurts your future prospects. You instantly abandon the old trail and begin a fresh journey from your current footing.

### 🖼 Visualizing Kadane's Execution Trace

```
Array: [ -2,   1,  -3,   4,  -1,   2,   1,  -5,   4 ]

Index 0 (x = -2):
  max_ending = -2
  max_so_far = -2

Index 1 (x =  1):
  max(1, -2 + 1) = 1  <-- RESTART FRESH AT INDEX 1!
  max_so_far = max(-2, 1) = 1

Index 2 (x = -3):
  max(-3, 1 + -3) = -2 <-- EXTEND PREVIOUS
  max_so_far = 1

Index 3 (x =  4):
  max(4, -2 + 4) = 4  <-- RESTART FRESH AT INDEX 3!
  max_so_far = max(1, 4) = 4

Index 4 (x = -1):
  max(-1, 4 + -1) = 3  <-- EXTEND PREVIOUS
  max_so_far = 4

Index 5 (x =  2):
  max(2, 3 + 2) = 5   <-- EXTEND PREVIOUS
  max_so_far = max(4, 5) = 5

Index 6 (x =  1):
  max(1, 5 + 1) = 6   <-- EXTEND PREVIOUS
  max_so_far = max(5, 6) = 6  <-- GLOBAL PEAK IDENTIFIED!

Index 7 (x = -5):
  max(-5, 6 + -5) = 1  <-- EXTEND PREVIOUS
  max_so_far = 6

Index 8 (x =  4):
  max(4, 1 + 4) = 5   <-- EXTEND PREVIOUS
  max_so_far = 6

Final Answer: 6 (Subarray [4, -1, 2, 1])
```

### Invariants & Properties

1. **Local Optimality Invariant:** At the conclusion of iteration `i`, `max_ending_here` holds the exact maximum sum of any contiguous subarray ending at index `i`.
2. **Global Optimality Invariant:** At the conclusion of iteration `i`, `max_so_far` holds the maximum subarray sum among all valid contiguous subarrays in `nums[0..i]`.
3. **Space Optimization Invariant:** Because computing `max_ending_here[i]` only depends on `max_ending_here[i - 1]`, we do not need a DP table of size `N`. A single integer scalar stores the required state, achieving strict `O(1)` auxiliary space.

---

## 🔧 CHAPTER 3: CORE PATTERN MECHANICS & ASCII TRACES

### 1. Maximum Product Subarray: Sign-Flip Mechanics

With multiplication, negative numbers reverse ordering: multiplying a large negative number by another negative number produces a large positive number. To handle this, we track both `max_ending_here` and `min_ending_here`. When encountering a negative number, the maximum and minimum swap roles:

```
Array: [ 2, 3, -2, 4 ]

i = 0 (x = 2):
  max_prod = 2, min_prod = 2, global = 2

i = 1 (x = 3):
  max_prod = max(3, 2 * 3) = 6
  min_prod = min(3, 2 * 3) = 3
  global = 6

i = 2 (x = -2):
  Negative number encountered! Swap max_prod (6) and min_prod (3):
  max_prod becomes 3, min_prod becomes 6
  max_prod = max(-2, 3 * -2) = -2
  min_prod = min(-2, 6 * -2) = -12
  global = 6

i = 3 (x = 4):
  max_prod = max(4, -2 * 4) = 4
  min_prod = min(4, -12 * 4) = -48
  global = 6

Result = 6
```

### 2. Circular Subarray: Complement Subtraction Mechanics

In a circular array, a maximum subarray can either be contiguous within the linear bounds (standard Kadane) or wrap around the ends:

```
Case 1: Standard Linear Subarray (No Wrap)
[ ... [ Maximum Subarray ] ... ]

Case 2: Wrap-Around Subarray
[ Max Part 1 ] ... [ Minimum Subarray ] ... [ Max Part 2 ]
 ────────────        ──────────────────        ────────────
                     Minimizing this middle
                     maximizes the wrap:
                     Wrap Sum = Total Sum - Minimum Subarray
```

---

## 💻 CHAPTER 4: PRODUCTION-GRADE IMPLEMENTATIONS (C# & PYTHON)

### Problem 1: Maximum Subarray (LeetCode 53) — Core Kadane's Algorithm

#### 🎙️ 45-Minute Interview Talk Track
> *"To find the contiguous subarray with the largest sum, checking all subarrays naively takes `O(N^2)` time. We can solve this in `O(N)` time and `O(1)` auxiliary space using Kadane's algorithm. We maintain two variables: `max_ending_here`, which represents the maximum subarray sum ending at the current index, and `max_so_far`, which tracks the overall maximum seen. For each element, we determine whether to extend the previous subarray or start a new subarray fresh from the current number: `max_ending_here = max(nums[i], max_ending_here + nums[i])`. We then update `max_so_far`. This handles negative numbers seamlessly because if all numbers are negative, it correctly picks the single least-negative element."*

#### C# Primary Implementation (.NET 8/9 — Production-Grade)
```csharp
using System;

public static class MaxSubArraySolver
{
    /// <summary>
    /// Computes maximum contiguous subarray sum in O(N) time and O(1) space.
    /// Time Complexity: O(N) | Auxiliary Space: O(1) | Output Space: O(1)
    /// </summary>
    public static int MaxSubArray(int[] nums)
    {
        ArgumentNullException.ThrowIfNull(nums);
        if (nums.Length == 0) return 0;

        int maxEndingHere = nums[0];
        int maxSoFar = nums[0];

        for (int i = 1; i < nums.Length; i++)
        {
            // Extend existing subarray or start fresh from current element
            maxEndingHere = Math.Max(nums[i], maxEndingHere + nums[i]);
            maxSoFar = Math.Max(maxSoFar, maxEndingHere);
        }

        return maxSoFar;
    }
}
```

#### Python Secondary Implementation (3.11+ — Clean & Idiomatic)
```python
def max_sub_array(nums: list[int]) -> int:
    """Finds maximum contiguous subarray sum using Kadane's algorithm.

    Time Complexity: O(N) | Auxiliary Space: O(1) | Output Space: O(1)
    """
    if not nums:
        return 0

    max_ending_here = max_so_far = nums[0]

    for x in nums[1:]:
        max_ending_here = max(x, max_ending_here + x)
        max_so_far = max(max_so_far, max_ending_here)

    return max_so_far
```

#### 📊 Explicit Complexity Deconstruction
* **Time Complexity:** `O(N)` — Single pass evaluating each of the `N` array elements with constant arithmetic operations.
* **Auxiliary Space:** `O(1)` — Only two scalar integer variables tracked in memory.
* **Output Space:** `O(1)` — Returns a single integer scalar.

---

### Problem 2: Maximum Product Subarray (LeetCode 152) — Dual Min/Max Tracking

#### 🎙️ 45-Minute Interview Talk Track
> *"Unlike addition, multiplication flips signs when negative numbers are involved: a very small negative product multiplied by another negative number becomes a large positive product. Therefore, tracking only the maximum product ending at each position is insufficient; we must also track the minimum product ending at each position. When the current number is negative, multiplying flips the maximum and minimum, so we swap `max_ending` and `min_ending` before multiplying. At each step, `max_ending` is `max(x, max_ending * x)` and `min_ending` is `min(x, min_ending * x)`. We record the global maximum product throughout the pass in `O(N)` time and `O(1)` space."*

#### C# Primary Implementation (.NET 8/9 — Production-Grade)
```csharp
using System;

public static class MaxProductSolver
{
    /// <summary>
    /// Finds maximum contiguous product subarray by tracking both minimum and maximum products.
    /// Time Complexity: O(N) | Auxiliary Space: O(1) | Output Space: O(1)
    /// </summary>
    public static int MaxProduct(int[] nums)
    {
        ArgumentNullException.ThrowIfNull(nums);
        if (nums.Length == 0) return 0;

        int maxEnding = nums[0];
        int minEnding = nums[0];
        int globalMax = nums[0];

        for (int i = 1; i < nums.Length; i++)
        {
            int x = nums[i];

            // Multiplying by a negative number swaps maximum and minimum
            if (x < 0)
            {
                (maxEnding, minEnding) = (minEnding, maxEnding);
            }

            maxEnding = Math.Max(x, maxEnding * x);
            minEnding = Math.Min(x, minEnding * x);

            globalMax = Math.Max(globalMax, maxEnding);
        }

        return globalMax;
    }
}
```

#### Python Secondary Implementation (3.11+ — Clean & Idiomatic)
```python
def max_product(nums: list[int]) -> int:
    """Calculates maximum product subarray using sign-flip min/max tracking.

    Time Complexity: O(N) | Auxiliary Space: O(1) | Output Space: O(1)
    """
    if not nums:
        return 0

    max_ending = min_ending = global_max = nums[0]

    for x in nums[1:]:
        if x < 0:
            max_ending, min_ending = min_ending, max_ending

        max_ending = max(x, max_ending * x)
        min_ending = min(x, min_ending * x)

        global_max = max(global_max, max_ending)

    return global_max
```

#### 📊 Explicit Complexity Deconstruction
* **Time Complexity:** `O(N)` — Single pass traversing `N` items with `O(1)` scalar comparisons per element.
* **Auxiliary Space:** `O(1)` — Only three scalar variables maintained.
* **Output Space:** `O(1)` — Returns a single integer scalar.

---

### Problem 3: Maximum Sum Circular Subarray (LeetCode 918) — Complement Reduction

#### 🎙️ 45-Minute Interview Talk Track
> *"In a circular array, the maximum contiguous subarray either does not wrap around (in which case it is solved by standard linear Kadane), or it wraps around the end and begins at the start. For the wrap-around case, notice that the elements not included form a contiguous minimum subarray in the middle. Therefore, the maximum wrap-around sum is simply `total_sum - minimum_subarray_sum`. In a single pass, we compute both the linear maximum subarray and the linear minimum subarray using dual Kadane trackers, while accumulating the total sum. There is one critical edge case: if every element is negative, `total_sum == minimum_subarray_sum`, which would result in an invalid empty subarray of sum 0. In that case, we simply return the linear maximum."*

#### C# Primary Implementation (.NET 8/9 — Production-Grade)
```csharp
using System;

public static class MaxSubarrayCircularSolver
{
    /// <summary>
    /// Computes maximum circular subarray sum in a single pass without mutating the array.
    /// Time Complexity: O(N) | Auxiliary Space: O(1) | Output Space: O(1)
    /// </summary>
    public static int MaxSubarraySumCircular(int[] nums)
    {
        ArgumentNullException.ThrowIfNull(nums);
        if (nums.Length == 0) return 0;

        int totalSum = 0;
        int maxEnding = 0, maxSoFar = nums[0];
        int minEnding = 0, minSoFar = nums[0];

        foreach (int x in nums)
        {
            totalSum += x;

            // Standard Kadane for maximum subarray
            maxEnding = Math.Max(x, maxEnding + x);
            maxSoFar = Math.Max(maxSoFar, maxEnding);

            // Kadane variant for minimum subarray
            minEnding = Math.Min(x, minEnding + x);
            minSoFar = Math.Min(minSoFar, minEnding);
        }

        // Edge case: if all elements are negative, totalSum - minSoFar == 0 (empty set),
        // so we must return maxSoFar (the maximum single negative number).
        return maxSoFar < 0 ? maxSoFar : Math.Max(maxSoFar, totalSum - minSoFar);
    }
}
```

#### Python Secondary Implementation (3.11+ — Clean & Idiomatic)
```python
def max_subarray_sum_circular(nums: list[int]) -> int:
    """Finds maximum circular subarray sum via dual max/min Kadane complement reduction.

    Time Complexity: O(N) | Auxiliary Space: O(1) | Output Space: O(1)
    """
    total_sum = 0
    max_ending = 0
    max_so_far = nums[0]
    min_ending = 0
    min_so_far = nums[0]

    for x in nums:
        total_sum += x
        max_ending = max(x, max_ending + x)
        max_so_far = max(max_so_far, max_ending)
        min_ending = min(x, min_ending + x)
        min_so_far = min(min_so_far, min_ending)

    return (
        max_so_far if max_so_far < 0 else max(max_so_far, total_sum - min_so_far)
    )
```

#### 📊 Explicit Complexity Deconstruction
* **Time Complexity:** `O(N)` — Single linear pass tracking both min and max recurrences simultaneously.
* **Auxiliary Space:** `O(1)` — Only scalar accumulator variables used.
* **Output Space:** `O(1)` — Returns a single integer scalar.

---

### Problem 4: Maximum Subarray with Indices Reconstruction

#### 🎙️ 45-Minute Interview Talk Track
> *"When an interviewer asks for the actual start and end indices of the maximum subarray rather than just the sum, we maintain two tracking pointers: `current_start` and `best_start`, along with `best_end`. Whenever `nums[i] > current_sum + nums[i]`, we restart the subarray, resetting `current_start = i`. When extending the previous subarray, `current_start` remains unchanged. Whenever `current_sum > max_so_far`, we update `max_so_far` and capture `best_start = current_start` and `best_end = i`. This reconstructs the optimal contiguous window in `O(N)` time and `O(1)` space."*

#### C# Primary Implementation (.NET 8/9 — Zero Allocation)
```csharp
using System;

public static class MaxSubArrayReconstructionSolver
{
    /// <summary>
    /// Reconstructs maximum subarray sum along with inclusive [start, end] indices.
    /// Time Complexity: O(N) | Auxiliary Space: O(1) | Output Space: O(1)
    /// </summary>
    public static (int MaxSum, int StartIndex, int EndIndex) FindMaxSubArrayWithIndices(int[] nums)
    {
        ArgumentNullException.ThrowIfNull(nums);
        if (nums.Length == 0) return (0, -1, -1);

        int maxSoFar = nums[0];
        int currentSum = nums[0];
        int bestStart = 0;
        int bestEnd = 0;
        int currentStart = 0;

        for (int i = 1; i < nums.Length; i++)
        {
            if (nums[i] > currentSum + nums[i])
            {
                currentSum = nums[i];
                currentStart = i;
            }
            else
            {
                currentSum += nums[i];
            }

            if (currentSum > maxSoFar)
            {
                maxSoFar = currentSum;
                bestStart = currentStart;
                bestEnd = i;
            }
        }

        return (maxSoFar, bestStart, bestEnd);
    }
}
```

#### Python Secondary Implementation (3.11+ — Clean & Idiomatic)
```python
def find_max_sub_array_with_indices(nums: list[int]) -> tuple[int, int, int]:
    """Returns (max_sum, start_index, end_index) for optimal contiguous subarray.

    Time Complexity: O(N) | Auxiliary Space: O(1) | Output Space: O(1)
    """
    if not nums:
        return (0, -1, -1)

    max_so_far = current_sum = nums[0]
    best_start = best_end = current_start = 0

    for i in range(1, len(nums)):
        if nums[i] > current_sum + nums[i]:
            current_sum = nums[i]
            current_start = i
        else:
            current_sum += nums[i]

        if current_sum > max_so_far:
            max_so_far = current_sum
            best_start = current_start
            best_end = i

    return (max_so_far, best_start, best_end)
```

#### 📊 Explicit Complexity Deconstruction
* **Time Complexity:** `O(N)` — Single pass evaluating each index once.
* **Auxiliary Space:** `O(1)` — Only index scalar variables tracked.
* **Output Space:** `O(1)` — Returns a fixed 3-tuple.

---

## ⚖️ CHAPTER 5: PERFORMANCE, TRADE-OFFS & REAL SYSTEMS

### Trade-Off Comparison

| Approach | Time Complexity | Auxiliary Space | Handles Negatives? | Memory Access Pattern |
| :--- | :--- | :--- | :--- | :--- |
| **Brute Force (Nested Loops)** | `O(N^2)` | `O(1)` | Yes | Repeated redundant scans |
| **Prefix Sums + Min Prefix** | `O(N)` | `O(N)` | Yes | Contiguous array reads |
| **Kadane's Algorithm** | `O(N)` | `O(1)` | Yes | Streamable single-pass, optimal cache hits |
| **Divide and Conquer** | `O(N log N)` | `O(log N)` | Yes | Recursive stack frames |

> [!NOTE]
> **Production Reality (Why FAANG Tests This):**
> High-frequency algorithmic trading systems stream billions of price delta events daily. Calculating the maximum potential return of an intraday long position or detecting worst-case peak-to-trough drawdowns occurs directly in streaming network interface cards (NICs) using Kadane's recurrence in hardware registers with zero memory allocations.

### Defensive Engineering & Failure Modes

1. **Initializing Global Max to 0 Instead of `nums[0]`:** If the input array consists entirely of negative numbers (e.g., `[-5, -2, -8]`), initializing `max_so_far = 0` incorrectly returns 0 rather than the true maximum `-2`. Always initialize `max_so_far = nums[0]`.
2. **Product Subarray Overflow:** For large sequences of integers, product subarrays can rapidly exceed 32-bit and 64-bit limits. In production systems, check for arithmetic overflow or use logarithmic addition.
3. **Circular Empty Subarray Edge Case:** When all numbers are negative, `totalSum - minSoFar == 0`. Returning this would yield an empty subarray, violating the non-empty constraint. Guard with `if (maxSoFar < 0) return maxSoFar;`.

---

## 🎯 CHAPTER 6: FAANG INTERVIEW PATTERN SIGNALS & EDGE CASES

### 🎯 Pattern Recognition Signals
- ✅ **"Maximum sum of a contiguous subarray"** -> Standard Kadane's (`O(N)` time, `O(1)` space).
- ✅ **"Maximum product contiguous subarray"** -> Kadane with sign-flip min/max tracking.
- ✅ **"Circular array maximum sum"** -> Dual Kadane (`max_so_far` vs. `total - min_so_far`).
- ✅ **"Best time to buy and sell stock"** -> Kadane equivalent tracking `min_price_seen`.
- 🛑 **"Maximum sum of a subarray of fixed size K"** -> Do NOT use Kadane's. Use a Fixed-Size Sliding Window.

### 🧪 Concrete Edge-Case Checklist
1. **All Negative Numbers (`[-3, -1, -5]`):** Result must return the least negative number (`-1`), not 0.
2. **Single Element (`nums = [7]`):** Must return `7` without index out of bounds.
3. **Array with Zeroes (`[2, 0, -1, 3]`):** Zeroes must be processed without causing null division or premature termination.
4. **All Identical Numbers (`[4, 4, 4]`):** Returns `4 * N` smoothly.

---

## ⚔️ SUPPLEMENTARY OUTCOMES

### 🏋️ Practice Problems

| # | Problem | Source | Difficulty | Key Concept |
| :--- | :--- | :--- | :--- | :--- |
| 1 | Maximum Subarray | LeetCode 53 | 🟡 Medium | Core Kadane's Algorithm |
| 2 | Maximum Product Subarray | LeetCode 152 | 🟡 Medium | Dual min/max state tracking |
| 3 | Maximum Sum Circular Subarray | LeetCode 918 | 🟡 Medium | Kadane complement subtraction |
| 4 | Best Time to Buy and Sell Stock | LeetCode 121 | 🟢 Easy | One-pass minimum tracking |
| 5 | Maximum Absolute Sum of Any Subarray | LeetCode 1749 | 🟡 Medium | Dual min/max Kadane |
| 6 | Maximum Subarray Sum with One Deletion | LeetCode 1186 | 🟡 Medium | DP state machine variant |
| 7 | Continuous Subarray Sum | LeetCode 523 | 🟡 Medium | Prefix sum + modulo hash map |
| 8 | Subarray Sum Equals K | LeetCode 560 | 🟡 Medium | Prefix sum hash complement |

### 🎙️ Interview Questions (Verbal Drills)

1. **Q:** Why does Kadane's algorithm achieve `O(N)` time while checking all `O(N^2)` subarrays?
   - **Answer:** By grouping all `O(N^2)` subarrays by their ending index `i`, we observe optimal substructure: the best subarray ending at `i` is either `nums[i]` alone or `nums[i]` appended to the best subarray ending at `i - 1`. This reduces the search space at each index from `O(i)` candidate subarrays to a single `O(1)` comparison.
2. **Q:** How do you handle finding the maximum subarray in a 2D matrix?
   - **Answer:** We fix two row boundaries `r1` and `r2`, compress the columns between them into a 1D array by summing column elements, and run 1D Kadane's on the compressed array. Running this over all `O(R^2)` row pairs solves 2D Maximum Subarray in `O(R^2 * C)` time.
3. **Q:** Can Kadane's be used if the problem requires finding a subarray of length at least `K`?
   - **Answer:** Yes, by combining prefix sums with a sliding window tracking `min(prefix[0..i - k])`. At index `i`, the maximum subarray of length at least `K` ending at `i` is `prefix[i] - min(prefix[0..i - k])`.

### ❌ Common Misconceptions

- **Myth:** Kadane's algorithm only works for sums, not products.  
  *Reality:* Kadane's generalized DP formulation applies to products by tracking both maximum and minimum states to account for negative sign inversions.
- **Myth:** Kadane's requires an auxiliary array of size `N`.  
  *Reality:* Because each step depends only on the immediately preceding local maximum, Kadane's requires strictly `O(1)` auxiliary space.

### 🚀 Advanced Concepts

1. **2D Kadane (Matrix Maximum Subarray):** Compressing row intervals and applying 1D Kadane's across column projections in `O(R^2 * C)` time.
2. **Divide-and-Conquer Segment Tree (Subarray Queries):** Maintaining `(totalSum, maxPrefix, maxSuffix, maxSubarray)` in segment tree nodes to answer dynamic range maximum subarray queries in `O(log N)` time.

---

## 📌 CLOSING REFLECTION

Kadane's algorithm illustrates the essence of **space-optimized Dynamic Programming**. Complex combinatorial problems often contain linear substructures where only immediate historical state matters. By distinguishing local state from global optimal tracking, you achieve maximum theoretical efficiency: a single pass, constant memory, and zero allocations.

---
> 🧭 **Navigation:** [← Previous Day](Week_05_Day_04_Part_A_Partition_Cyclic_Sort_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_05_Day_05_Fast_Slow_Pointers_Instructional.md)

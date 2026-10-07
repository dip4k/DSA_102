# 📘 Week 05 Day 04 Part A: Partition & Cyclic Sort Patterns — Engineering Guide

> 🧭 **Navigation:** [← Previous Day](Week_05_Day_03_Merge_Operations_Interval_Patterns_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_05_Day_04_Part_B_Kadane_Algorithm_Instructional.md)
> 
> 💡 **Instructor Note:** *Not all sections or topics are mandatory. Focus on the Dutch National Flag 3-pointer invariant and Cyclic Sort swap mechanics according to your interview timeline.*

---

## 🎯 LEARNING OBJECTIVES

By the end of this chapter, you will be able to:

- 🎯 **Internalize** the partition invariant: maintaining designated array sub-regions through pointer boundaries allows in-place segregation with strictly `O(1)` auxiliary space.
- ⚙️ **Implement** Sort Colors (Dutch National Flag), Move Zeroes, Missing Number, and Find Duplicate in production C# (.NET 8/9) and idiomatic Python (3.11+).
- ⚖️ **Evaluate** trade-offs between comparative sorting (`O(N log N)`), hash sets (`O(N)` space), and cyclic sort (`O(N)` time, `O(1)` space).
- 🏭 **Connect** partition patterns to real systems (in-place OS memory compaction, Quicksort pivot schemes, columnar database run-length encoding).
- 🎙️ **Explain** pointer advancement rules and why Cyclic Sort performs at most `N` swaps across a 45-minute technical interview.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Engineering Problem

In performance-critical software—such as database storage engines, operating system memory allocators, and embedded controllers—allocating secondary buffer arrays to filter, reorganize, or sort records is unacceptable. When millions of records arrive in a contiguous block, allocating an auxiliary `O(N)` buffer incurs heap fragmentation, garbage collector churn, and cache eviction.

Consider segregating records by status flags: valid (`1`), pending (`2`), or corrupted (`0`). Or consider moving all inactive null pointers to the end of an array while retaining active pointer ordering. A naive approach allocates new arrays and filters elements, wasting memory.

By using **partitioning and cyclic sort**, we manipulate elements directly in-place:
1. **Three-Way Partitioning (Dutch National Flag):** Rearranges three distinct categories in a single pass with `O(1)` auxiliary space.
2. **Cyclic Sort:** Uses the values themselves as indices when values reside within `[1..N]` or `[0..N]`, placing each number into its rightful memory slot in linear time.

> [!NOTE]
> **Production Reality (Why FAANG Tests This):**
> Operating system kernel allocators (Linux Slab/Buddy allocators) and garbage collectors (Java ZGC, .NET GC compacting phase) reorganize contiguous pages in-place using two-pointer and three-pointer partitions to defragment memory without allocating secondary buffers. FAANG interviewers test this pattern to verify you can execute tight, bug-free in-place pointer swaps without leaning on auxiliary memory.

### The Solution: Region Invariants

Instead of viewing the array as a monolithic list, we partition it into distinct semantic zones bounded by pointers:
- Elements before `low` belong strictly to Region 0.
- Elements between `low` and `mid` belong strictly to Region 1.
- Elements between `mid` and `high` constitute unexplored territory.
- Elements past `high` belong strictly to Region 2.

By advancing pointers monotonically as elements are inspected or swapped, the unexplored region shrinks to zero in a single pass.

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### The Core Analogy

Imagine sorting physical colored balls (Red=0, White=1, Blue=2) on a single narrow rail. You place a left barrier (`low`) and a right barrier (`high`), and walk along with a selector (`mid`):
- If you see a Red ball (0), you swap it behind the left barrier and advance both barriers.
- If you see a White ball (1), it already belongs in the middle zone, so you step past it.
- If you see a Blue ball (2), you throw it behind the right barrier and pull the right barrier inward. You do not step forward yet, because the ball that just arrived from behind the right barrier has not been inspected.

### 🖼 Visualizing Dutch National Flag Regions

```
Index:    0      low-1   low     mid-1   mid       high   high+1     N-1
Array:  [ 0  0 ... 0  |  1  1 ... 1  |  ?  ? ... ?  |  2  2 ... 2  ]
          ────────────   ────────────   ────────────   ────────────
          Region 0       Region 1       Unexplored     Region 2
          (Finalized)    (Finalized)    (Frontier)     (Finalized)
```

### Invariants & Properties

1. **Four-Zone Invariant (Dutch Flag):**
   - `[0 .. low - 1]`: all elements equal 0
   - `[low .. mid - 1]`: all elements equal 1
   - `[mid .. high]`: unexamined elements
   - `[high + 1 .. N - 1]`: all elements equal 2
2. **Non-Advancement on High Swap Invariant:** When `nums[mid] == 2`, swapping `nums[mid]` with `nums[high]` decrements `high`, but `mid` **must NOT advance**. The element swapped from `high` was previously unexamined.
3. **Cyclic Sort Swap Budget:** In an array of length `N`, each swap places at least one element into its permanent destination (`nums[nums[i] - 1] == nums[i]`). Since there are at most `N` elements, cyclic sort executes at most `N - 1` swaps total, guaranteeing `O(N)` overall runtime.

---

## 🔧 CHAPTER 3: CORE PATTERN MECHANICS & ASCII TRACES

### Cyclic Sort Swap Mechanics

Given array `[3, 5, 2, 1, 4]` where values are in range `[1..5]`. Target index for value `X` is `X - 1`:

```
Index:   0    1    2    3    4
Array:  [3,   5,   2,   1,   4]

Step 0: i = 0, val = 3 -> Target index = 3 - 1 = 2
        nums[0] != nums[2] (3 != 2) -> SWAP nums[0] <-> nums[2]
        Array:  [2,   5,   3,   1,   4]   (Value 3 is now permanently at index 2!)

Step 1: i = 0 (re-evaluate!), val = 2 -> Target index = 2 - 1 = 1
        nums[0] != nums[1] (2 != 5) -> SWAP nums[0] <-> nums[1]
        Array:  [5,   2,   3,   1,   4]   (Value 2 is now permanently at index 1!)

Step 2: i = 0 (re-evaluate!), val = 5 -> Target index = 5 - 1 = 4
        nums[0] != nums[4] (5 != 4) -> SWAP nums[0] <-> nums[4]
        Array:  [4,   2,   3,   1,   5]   (Value 5 is now permanently at index 4!)

Step 3: i = 0 (re-evaluate!), val = 4 -> Target index = 4 - 1 = 3
        nums[0] != nums[3] (4 != 1) -> SWAP nums[0] <-> nums[3]
        Array:  [1,   2,   3,   4,   5]   (Value 4 is now permanently at index 3!)

Step 4: i = 0, val = 1 -> Target index = 0. nums[0] == 1 -> Correct! Increment i to 1.
        Indices 1, 2, 3, 4 are already in their correct spots -> i increments to N.
Final:  [1, 2, 3, 4, 5] sorted in O(N) time with O(1) auxiliary space!
```

---

## 💻 CHAPTER 4: PRODUCTION-GRADE IMPLEMENTATIONS (C# & PYTHON)

### Problem 1: Sort Colors (LeetCode 75) — Dutch National Flag 3-Way Partition

#### 🎙️ 45-Minute Interview Talk Track
> *"To sort an array consisting strictly of 0s, 1s, and 2s in-place with `O(1)` space and a single pass, we use Dijkstra's Dutch National Flag three-way partition. We maintain three pointers: `low`, `mid`, and `high`. The invariant guarantees that elements in `[0..low-1]` are 0, elements in `[low..mid-1]` are 1, and elements in `[high+1..N-1]` are 2. The slice `[mid..high]` represents unexamined elements. When `nums[mid] == 0`, we swap `nums[low]` and `nums[mid]` and increment both pointers. When `nums[mid] == 1`, it belongs in the middle region, so we simply increment `mid`. When `nums[mid] == 2`, we swap `nums[mid]` with `nums[high]` and decrement `high`—critically without incrementing `mid`, because the swapped element from `high` has not yet been processed. The loop terminates when `mid > high` in `O(N)` time."*

#### C# Primary Implementation (.NET 8/9 — Zero Allocation)
```csharp
using System;

public static class SortColorsSolver
{
    /// <summary>
    /// Sorts an array containing 0s, 1s, and 2s in-place in a single pass.
    /// Time Complexity: O(N) | Auxiliary Space: O(1) | Output Space: O(1) in-place
    /// </summary>
    public static void SortColors(int[] nums)
    {
        ArgumentNullException.ThrowIfNull(nums);
        int low = 0;
        int mid = 0;
        int high = nums.Length - 1;

        while (mid <= high)
        {
            if (nums[mid] == 0)
            {
                (nums[low], nums[mid]) = (nums[mid], nums[low]);
                low++;
                mid++;
            }
            else if (nums[mid] == 1)
            {
                mid++;
            }
            else // nums[mid] == 2
            {
                (nums[mid], nums[high]) = (nums[high], nums[mid]);
                high--;
            }
        }
    }
}
```

#### Python Secondary Implementation (3.11+ — Clean & Idiomatic)
```python
def sort_colors(nums: list[int]) -> None:
    """Sorts array containing 0s, 1s, and 2s in-place using Dutch National Flag.

    Time Complexity: O(N) | Auxiliary Space: O(1) | Output Space: O(1) in-place
    """
    low, mid, high = 0, 0, len(nums) - 1

    while mid <= high:
        if nums[mid] == 0:
            nums[low], nums[mid] = nums[mid], nums[low]
            low += 1
            mid += 1
        elif nums[mid] == 1:
            mid += 1
        else:
            nums[mid], nums[high] = nums[high], nums[mid]
            high -= 1
```

#### 📊 Explicit Complexity Deconstruction
* **Time Complexity:** `O(N)` — In each iteration, either `mid` increments or `high` decrements. Since `mid` starts at 0 and `high` starts at `N - 1`, the loop executes at most `N` times.
* **Auxiliary Space:** `O(1)` — Only three integer scalar pointers are maintained.
* **Output Space:** `O(1)` in-place — Modifies the input array directly without allocations.

---

### Problem 2: Move Zeroes (LeetCode 283) — Two-Pointer Read/Write Partition

#### 🎙️ 45-Minute Interview Talk Track
> *"To move all zeroes to the end of the array while maintaining the relative order of non-zero elements in-place, we use a two-pointer partition with a slow write pointer and a fast read pointer. The `write` pointer marks the boundary of compacted non-zero elements. We iterate `read` from 0 to `N - 1`. Whenever `nums[read] != 0`, we swap `nums[write]` with `nums[read]` and advance `write`. If all initial elements are non-zero, `write` and `read` match and swap in-place. The moment a zero appears, `write` halts while `read` advances to the next non-zero number, cleanly shifting zeroes backward in a single pass with `O(1)` auxiliary space."*

#### C# Primary Implementation (.NET 8/9 — Zero Allocation)
```csharp
using System;

public static class MoveZeroesSolver
{
    /// <summary>
    /// Moves zeroes to the array end in-place while preserving non-zero element order.
    /// Time Complexity: O(N) | Auxiliary Space: O(1) | Output Space: O(1) in-place
    /// </summary>
    public static void MoveZeroes(int[] nums)
    {
        ArgumentNullException.ThrowIfNull(nums);
        int write = 0;

        for (int read = 0; read < nums.Length; read++)
        {
            if (nums[read] != 0)
            {
                if (read != write)
                {
                    (nums[write], nums[read]) = (nums[read], nums[write]);
                }
                write++;
            }
        }
    }
}
```

#### Python Secondary Implementation (3.11+ — Clean & Idiomatic)
```python
def move_zeroes(nums: list[int]) -> None:
    """Moves all zeroes to end of nums in-place while maintaining order.

    Time Complexity: O(N) | Auxiliary Space: O(1) | Output Space: O(1) in-place
    """
    write = 0
    for read in range(len(nums)):
        if nums[read] != 0:
            if read != write:
                nums[write], nums[read] = nums[read], nums[write]
            write += 1
```

#### 📊 Explicit Complexity Deconstruction
* **Time Complexity:** `O(N)` — Single pass where `read` visits all `N` elements exactly once.
* **Auxiliary Space:** `O(1)` — Only two scalar index pointers are tracked.
* **Output Space:** `O(1)` in-place — No new array memory allocated.

---

### Problem 3: Missing Number (LeetCode 268) — Cyclic Sort In-Place Indexing

#### 🎙️ 45-Minute Interview Talk Track
> *"Given an array of `N` distinct numbers taken from the range `[0..N]`, one number is missing. While Gauss's sum formula solves this with arithmetic, cyclic sort provides a robust structural approach that generalizes to duplicate and multiple-missing problems. Each number `X` should reside at index `X`. While iterating through index `i`, if `nums[i] < N` and `nums[i] != nums[nums[i]]`, we swap `nums[i]` into its target index without advancing `i`. Otherwise, if `nums[i] == N` or already in place, we advance `i`. After the array is cyclic-sorted, a second linear pass checks which index `j` does not match `nums[j] == j`. That index is our missing number. If all match, `N` is missing."*

#### C# Primary Implementation (.NET 8/9 — Production-Grade)
```csharp
using System;

public static class MissingNumberSolver
{
    /// <summary>
    /// Identifies the missing number in [0..N] using cyclic sort indexing.
    /// Time Complexity: O(N) | Auxiliary Space: O(1) | Output Space: O(1)
    /// </summary>
    public static int MissingNumber(int[] nums)
    {
        ArgumentNullException.ThrowIfNull(nums);
        int i = 0;
        int n = nums.Length;

        while (i < n)
        {
            int targetIndex = nums[i];

            // If value is within [0..n-1] and not yet at its target index, swap it
            if (targetIndex < n && nums[i] != nums[targetIndex])
            {
                (nums[i], nums[targetIndex]) = (nums[targetIndex], nums[i]);
            }
            else
            {
                i++;
            }
        }

        // Find index mismatch
        for (int j = 0; j < n; j++)
        {
            if (nums[j] != j) return j;
        }

        return n;
    }
}
```

#### Python Secondary Implementation (3.11+ — Clean & Idiomatic)
```python
def missing_number(nums: list[int]) -> int:
    """Finds missing number in range [0..n] using cyclic sort.

    Time Complexity: O(N) | Auxiliary Space: O(1) | Output Space: O(1)
    """
    i, n = 0, len(nums)

    while i < n:
        target = nums[i]
        if target < n and nums[i] != nums[target]:
            nums[i], nums[target] = nums[target], nums[i]
        else:
            i += 1

    for j in range(n):
        if nums[j] != j:
            return j

    return n
```

#### 📊 Explicit Complexity Deconstruction
* **Time Complexity:** `O(N)` — The cyclic sort loop executes at most `N` swaps because each swap places an element into its correct slot. The subsequent scan takes `O(N)`. Overall time is `O(N)`.
* **Auxiliary Space:** `O(1)` — Only loop counter integers.
* **Output Space:** `O(1)` — Returns a single integer scalar.

---

### Problem 4: Find the Duplicate Number (LeetCode 287) — Implicit Cycle Detection

#### 🎙️ 45-Minute Interview Talk Track
> *"Given an array of `N + 1` integers where each integer is between `1` and `N`, the Pigeonhole Principle guarantees at least one duplicate. If modifying the array were allowed, cyclic sort could solve this by detecting collision upon swap. However, the problem forbids array mutation. We model the array as an implicit directed graph where index `i` points to node `nums[i]`. Because each value is a valid index and a duplicate value exists, two distinct indices must point to the same next node, creating a cycle. We apply Floyd's Tortoise and Hare algorithm: `slow` advances 1 step (`nums[slow]`), `fast` advances 2 steps (`nums[nums[fast]]`). Once they meet, we reset `slow` to `nums[0]` and advance both at speed 1. They collide at the cycle entry, which corresponds to the duplicate number."*

#### C# Primary Implementation (.NET 8/9 — Immutable In-Place)
```csharp
using System;

public static class FindDuplicateSolver
{
    /// <summary>
    /// Finds the duplicate number in an unmodifiable array using Floyd's cycle detection.
    /// Time Complexity: O(N) | Auxiliary Space: O(1) | Output Space: O(1)
    /// </summary>
    public static int FindDuplicate(int[] nums)
    {
        ArgumentNullException.ThrowIfNull(nums);

        // Phase 1: Detect cycle meeting point
        int slow = nums[0];
        int fast = nums[0];

        do
        {
            slow = nums[slow];
            fast = nums[nums[fast]];
        } while (slow != fast);

        // Phase 2: Find cycle entrance (the duplicate value)
        slow = nums[0];
        while (slow != fast)
        {
            slow = nums[slow];
            fast = nums[fast];
        }

        return slow;
    }
}
```

#### Python Secondary Implementation (3.11+ — Clean & Idiomatic)
```python
def find_duplicate(nums: list[int]) -> int:
    """Finds duplicate number without modifying array via Floyd's cycle detection.

    Time Complexity: O(N) | Auxiliary Space: O(1) | Output Space: O(1)
    """
    slow = fast = nums[0]

    # Phase 1: Locate meeting point in cycle
    while True:
        slow = nums[slow]
        fast = nums[nums[fast]]
        if slow == fast:
            break

    # Phase 2: Locate cycle start
    slow = nums[0]
    while slow != fast:
        slow = nums[slow]
        fast = nums[fast]

    return slow
```

#### 📊 Explicit Complexity Deconstruction
* **Time Complexity:** `O(N)` — The fast pointer traverses the cycle in at most `2N` operations; the cycle-finding convergence takes at most `N` steps.
* **Auxiliary Space:** `O(1)` — Only two scalar pointer variables, leaving the input array completely immutable.
* **Output Space:** `O(1)` — Returns a single integer scalar.

---

## ⚖️ CHAPTER 5: PERFORMANCE, TRADE-OFFS & REAL SYSTEMS

### Trade-Off Comparison

| Algorithm | Time Complexity | Auxiliary Space | Mutates Input? | Canonical Scenario |
| :--- | :--- | :--- | :--- | :--- |
| **Dutch National Flag** | `O(N)` | `O(1)` | Yes (In-place) | 3-way segregation, pivot partitioning |
| **Two-Pointer Read/Write** | `O(N)` | `O(1)` | Yes (In-place) | Filtering, compacting, zero relocation |
| **Cyclic Sort** | `O(N)` | `O(1)` | Yes (In-place) | Numbers in bounded range `[1..N]` |
| **Floyd Cycle Detection** | `O(N)` | `O(1)` | **No (Read-only)** | Duplicate finding when mutation forbidden |

> [!NOTE]
> **Production Reality (Why FAANG Tests This):**
> High-performance Quicksort implementations (such as the Dual-Pivot Quicksort used in Java's `Arrays.sort()` and .NET's `Array.Sort()`) use three-way partitioning to avoid `O(N^2)` degradation on arrays with duplicate keys. Partitioning duplicate keys into a middle zone skips thousands of redundant recursive calls.

### Defensive Engineering & Failure Modes

1. **Advancing `mid` on High Swap:** In Dutch Flag, incrementing `mid` when swapping with `high` is a critical bug. The element arriving from `high` has never been evaluated and could be 0, 1, or 2.
2. **Infinite Loops in Cyclic Sort:** If checking `nums[i] != i + 1` instead of `nums[i] != nums[correctIndex]`, duplicate elements will cause the algorithm to swap identical values infinitely. Always check `nums[i] != nums[targetIndex]`.
3. **Loop Termination Boundary (`mid <= high` vs. `mid < high`):** Dutch Flag must continue while `mid <= high`. If it stops at `mid < high`, the element at index `high` remains unpartitioned.

---

## 🎯 CHAPTER 6: FAANG INTERVIEW PATTERN SIGNALS & EDGE CASES

### 🎯 Pattern Recognition Signals
- ✅ **"Sort array containing only 0, 1, 2"** -> Dutch National Flag (`low`, `mid`, `high`).
- ✅ **"Move elements meeting property to end in-place"** -> Two-pointer Read/Write partition.
- ✅ **"Find missing / duplicate numbers in array from 1 to N"** -> Cyclic Sort (`nums[i] <-> nums[target]`).
- ✅ **"Find duplicate in 1..N array without modifying array"** -> Floyd's Tortoise & Hare cycle detection.
- 🛑 **"General array sorting with arbitrary numbers"** -> Do NOT use Cyclic Sort. Use Comparison Sort (`O(N log N)`).

### 🧪 Concrete Edge-Case Checklist
1. **Array with No Duplicates or Zeroes:** Verify pointer updates do not fail when no swaps are needed.
2. **Array Already Completely Partitioned (`[0, 0, 1, 1, 2, 2]`):** Confirm algorithm runs in linear time without redundant writes.
3. **Array with All Identical Elements (`[2, 2, 2, 2]`):** Ensure `high` pointer decrements without out-of-bounds index errors.
4. **Single-Element Input (`N = 1`):** Guard clause must handle gracefully without infinite loops.

---

## ⚔️ SUPPLEMENTARY OUTCOMES

### 🏋️ Practice Problems

| # | Problem | Source | Difficulty | Key Concept |
| :--- | :--- | :--- | :--- | :--- |
| 1 | Sort Colors | LeetCode 75 | 🟡 Medium | Dutch National Flag 3-way partition |
| 2 | Move Zeroes | LeetCode 283 | 🟢 Easy | Two-pointer read/write partition |
| 3 | Missing Number | LeetCode 268 | 🟢 Easy | Cyclic sort / Gauss sum |
| 4 | Find All Numbers Disappeared | LeetCode 448 | 🟢 Easy | Cyclic sort array range `[1..N]` |
| 5 | Find the Duplicate Number | LeetCode 287 | 🟡 Medium | Implicit cycle detection / Pigeonhole |
| 6 | First Missing Positive | LeetCode 41 | 🔴 Hard | Cyclic sort on unbounded positive range |
| 7 | Partition Array According to Pivot | LeetCode 2161 | 🟡 Medium | 3-way stable partition |
| 8 | Set Mismatch | LeetCode 645 | 🟢 Easy | Cyclic sort identifying duplicate & missing |

### 🎙️ Interview Questions (Verbal Drills)

1. **Q:** Why is Cyclic Sort guaranteed to take `O(N)` time even though the while loop doesn't always increment `i`?
   - **Answer:** We use aggregate amortized analysis. Each swap places at least one number into its final, correct position. Once an element is at its correct index, it is never swapped again. Since there are `N` slots, at most `N` swaps can occur. When no swap occurs, `i` increments. Thus, total operations across the entire array cannot exceed `2N`.
2. **Q:** In Dutch National Flag, why do we increment `mid` when swapping with `low`, but not when swapping with `high`?
   - **Answer:** Because `mid` moves from left to right, we have already inspected every element between `low` and `mid - 1` (they are all 1s). Swapping `nums[mid]` with `nums[low]` brings a known 1 into index `mid`, so `mid` can safely advance. In contrast, the element arriving from `high` came from unexplored territory and could be 0, 1, or 2, so `mid` must re-evaluate it.
3. **Q:** Why can't we use Cyclic Sort for LeetCode 287 if the problem statement specifies the array is read-only?
   - **Answer:** Cyclic sort requires in-place element mutation to place numbers into matching indices. When the memory is read-only, we must interpret values as immutable pointer addresses and apply Floyd's cycle detection instead.

### ❌ Common Misconceptions

- **Myth:** Partitioning is the same as sorting.  
  *Reality:* Partitioning only segregates elements into broad categorical groups; the internal ordering of elements within each group is undefined.
- **Myth:** Cyclic sort can sort any array of integers.  
  *Reality:* Cyclic sort strictly requires that elements map deterministically to valid index ranges (such as `[1..N]` or `[0..N]`). Arbitrary floating point numbers or negative numbers cannot be mapped directly to array indices.

### 🚀 Advanced Concepts

1. **Dual-Pivot Quicksort:** Yaroslavskiy's partitioning algorithm which divides an array into three regions using two pivots, achieving fewer memory writes than standard Hoare/Lomuto partitioning.
2. **First Missing Positive (LeetCode 41):** Extending cyclic sort to unbounded integer inputs by ignoring negative numbers and values greater than `N`, achieving `O(N)` time and `O(1)` auxiliary space on Hard-tier constraints.

---

## 📌 CLOSING REFLECTION

Partitioning and cyclic sort prove that **in-place constraints breed algorithmic elegance**. By treating array slots as designated pigeonholes and array indices as target destinations, you achieve the theoretical maximum efficiency: linear time and zero auxiliary memory. Master region invariants and swap counting, and you will tackle array transformation problems with complete mastery.

---
> 🧭 **Navigation:** [← Previous Day](Week_05_Day_03_Merge_Operations_Interval_Patterns_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_05_Day_04_Part_B_Kadane_Algorithm_Instructional.md)

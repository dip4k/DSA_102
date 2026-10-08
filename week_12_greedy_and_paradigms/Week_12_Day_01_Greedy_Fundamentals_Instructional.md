# 📘 WEEK 12 DAY 1: GREEDY FUNDAMENTALS — ENGINEERING GUIDE

> 🧭 **Navigation:** [← Week Overview](README.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_12_Day_02_Activity_Selection_And_Interval_Problems_Instructional.md)
> 
> 💡 **Instructor Note:** *Focus on the structural intuition: understand why a greedy choice commits irrevocably without exploring subproblems, how to construct exchange arguments, and how greedy priority queues schedule tasks in production systems.*

---

## 🎯 LEARNING OBJECTIVES

*By the end of this chapter, you will be able to:*

- 🎯 **Internalize** the greedy choice property versus dynamic programming without academic jargon.
- ⚙️ **Implement** robust greedy algorithms with modern C# (.NET 8/9 `PriorityQueue`) and Python (3.11+ `heapq`).
- ⚖️ **Evaluate** when greedy yields mathematically optimal outcomes versus when it fails into suboptimality.
- 🧠 **Master** the 3-step Exchange Argument proof technique to defend greedy choices in senior-level interviews.
- 🏭 **Connect** greedy priority patterns directly to real-world architectures like the Linux CFS scheduler and CPU task managers.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Engineering Challenge

Imagine you are building a real-time event-dispatching engine for a high-throughput streaming backend. Requests stream in with strict execution windows. At each instant, your worker node must select the next unit of work to execute.

You could reach for **Dynamic Programming (DP)**:
- Break the timeline into subproblems.
- Solve all overlapping subproblems recursively or via memoization tables (`dp[i]`).
- Compare all possibilities before committing to an answer.
- **Cost:** `O(N^2)` or higher time complexity, substantial memory allocation (`O(N)` or `O(N * W)` tables), and cache invalidation overhead.

Or you could ask a transformative question:
**What if you could examine only the local state right now, select the best candidate according to a fixed priority rule, commit irrevocably, and never look back?**

When this strategy succeeds, it is called a **Greedy Algorithm**. It collapses complex state-space searches into an ultra-fast `O(N log N)` sort-and-scan or `O(log N)` priority queue dispatch.

```
Dynamic Programming Search Space:
          [Root Problem]
         /      |       \
     [Sub 1]  [Sub 2]  [Sub 3]     <-- Must evaluate all branches
     /  |  \  /  |  \  /  |  \
    All states computed & compared

Greedy Irrevocable Path:
          [Root Problem]
                | (Locally best choice: single branch)
                v
          [Subproblem A]
                | (Locally best choice: single branch)
                v
          [Subproblem B]           <-- Zero backtracking, single path
```

### The Catch: When Greed Fails

Greedy algorithms are deceptive because they look simple, read cleanly, and run fast. However, **greedy algorithms are not universally optimal**.
- In **Fractional Knapsack**, greedy by density works perfectly.
- In **0/1 Knapsack**, greedy by density fails catastrophically because item indivisibility leaves unusable gaps in capacity.
- In **Shortest Paths with Non-Negative Weights** (Dijkstra), greedy works. With negative weights, greedy gets trapped.

The senior engineering skill is not merely writing a greedy loop; it is **proving why local greed guarantees global optimality** and detecting when you must pivot to Dynamic Programming.

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### Greedy Choice Property vs Dynamic Programming (No Jargon)

To decide between Greedy and Dynamic Programming, ask one physical question:
**Does making the best local choice right now ever force you to regret it later?**

| Dimension | Greedy Algorithm | Dynamic Programming (DP) |
| :--- | :--- | :--- |
| **Commitment** | Irrevocable: picks immediately, never backtracks | Tentative: checks all alternatives before deciding |
| **Subproblem Ordering** | Top-down: choice made *before* solving subproblems | Bottom-up or memoized: solves subproblems *before* combining |
| **State Memory** | Minimal: `O(1)` state variables or heap tracking | High: table holding intermediate subproblem answers |
| **Time Profile** | Fast: typically `O(N log N)` (sorting) or `O(N)` | Slower: typically `O(N^2)`, `O(N * K)`, or `O(2^N)` |
| **Requirement** | Greedy Choice Property + Optimal Substructure | Overlapping Subproblems + Optimal Substructure |

```
Physical Intuition:
- DP is like booking a travel itinerary: you check hotel + flight combinations
  together because a cheap flight with an expensive hotel might be worse overall.
- Greedy is like walking down a mountain at a fork in the trail: you simply take
  whichever step drops your elevation lowest right now, trusting the topology
  guarantees it leads to the true valley floor.
```

### The Two Pillars of Greedy Optimality

Every correct greedy algorithm rests on two structural properties:

1. **Greedy Choice Property:**
   A globally optimal solution can be assembled by making locally optimal (greedy) choices. You never need to inspect future subproblems to make the current choice.
2. **Optimal Substructure:**
   An optimal solution to the entire problem contains within it optimal solutions to the remaining subproblems. Once you make choice `G_1`, solving the remaining input optimally completes the global optimum.

---

## 🔬 CHAPTER 3: SIMPLIFIED PROOF TECHNIQUES

### The 3-Step Exchange Argument

In interviews, saying *"this choice feels optimal"* will fail the loop. You must defend greedy choices using the **Exchange Argument**. 

Here is the straightforward 3-step blueprint:

```
+-------------------------------------------------------------------------+
|                  THE 3-STEP EXCHANGE ARGUMENT BLUEPRINT                 |
+-------------------------------------------------------------------------+
| Step 1: Posit an Arbitrary Optimal Solution                             |
|   Let OPT be an optimal solution. If OPT is identical to GREEDY, done.   |
|                                                                         |
| Step 2: Identify the First Disagreement                                 |
|   Find the first decision where OPT chose element X instead of the       |
|   greedy choice G.                                                      |
|                                                                         |
| Step 3: Swap X with G (The Exchange)                                    |
|   Replace X with G in OPT to produce a modified solution OPT'.          |
|   Demonstrate:                                                          |
|     (a) Feasibility: OPT' does not violate any problem constraints.     |
|     (b) Value: Quality(OPT') >= Quality(OPT).                            |
|   Conclusion: Since OPT was optimal, OPT' is also optimal. By induction,|
|   OPT can be transformed into GREEDY without losing optimality.         |
+-------------------------------------------------------------------------+
```

```
Visualizing the Swap:

OPT:    [ G1 ] -> [ G2 ] -> [  X  ] -> [  Y  ] -> [  Z  ]
                             ^
                             | (First divergence from greedy choice G3)
                             v
GREEDY: [ G1 ] -> [ G2 ] -> [ G3  ] -> [ ... ]

Exchange X with G3:
OPT':   [ G1 ] -> [ G2 ] -> [ G3  ] -> [  Y  ] -> [  Z  ]
        \________________________/
        Agrees with Greedy up to step 3, with Quality(OPT') >= Quality(OPT)!
```

---

## ⚙️ CHAPTER 4: CORE GREEDY PATTERNS & IMPLEMENTATIONS

### Pattern 1: Generic Sort + Single Scan Template

Most array-based greedy problems follow this deterministic shape:
1. Sort input by a greedy criterion (e.g., finish time, value density, deadlines).
2. Initialize solution container and state trackers.
3. Linear scan: evaluate feasibility, commit, and update state.

#### C# Implementation (.NET 8/9)

```csharp
namespace DsaMastery.Greedy;

using System;
using System.Collections.Generic;
using System.Linq;

public static class GenericGreedyTemplate
{
    /// <summary>
    /// Generic Sort-and-Scan Greedy Selector.
    /// Demonstrates the canonical O(N log N) greedy execution workflow.
    /// </summary>
    public static IReadOnlyList<TItem> SelectGreedy<TItem, TState>(
        IEnumerable<TItem> items,
        Comparison<TItem> priorityOrder,
        TState initialState,
        Func<TItem, TState, (bool IsFeasible, TState NextState)> evaluator)
    {
        ArgumentNullException.ThrowIfNull(items);
        ArgumentNullException.ThrowIfNull(priorityOrder);
        ArgumentNullException.ThrowIfNull(evaluator);

        var list = items.ToList();
        list.Sort(priorityOrder); // Step 1: Sort by greedy criterion

        var chosen = new List<TItem>();
        TState currentState = initialState;

        // Step 2 & 3: Single pass scan
        foreach (var item in list)
        {
            var (isFeasible, nextState) = evaluator(item, currentState);
            if (isFeasible)
            {
                chosen.Add(item);       // Irrevocable greedy commitment
                currentState = nextState; // Transition state
            }
        }

        return chosen;
    }
}
```

#### Python Implementation (Python 3.11+)

```python
from typing import TypeVar, Callable, List, Tuple

TItem = TypeVar('TItem')
TState = TypeVar('TState')

def select_greedy(
    items: List[TItem],
    key_func: Callable[[TItem], any],
    initial_state: TState,
    evaluator: Callable[[TItem, TState], Tuple[bool, TState]],
    reverse: bool = False
) -> List[TItem]:
    """
    Generic Sort-and-Scan Greedy Selector in idiomatic Python 3.11+.
    Sorts by key_func, iterates linearly, and commits irrevocably when feasible.
    """
    sorted_items = sorted(items, key=key_func, reverse=reverse)
    chosen: List[TItem] = []
    current_state = initial_state

    for item in sorted_items:
        is_feasible, next_state = evaluator(item, current_state)
        if is_feasible:
            chosen.append(item)
            current_state = next_state

    return chosen
```

---

### Pattern 2: Priority-Queue Greedy Dispatch — Task Scheduler (LeetCode 621)

**Problem Statement:**
Given an array of CPU `tasks` represented by characters and a cooldown period `n`, determine the minimum total CPU units required to complete all tasks. Between identical tasks, there must be at least `n` units of time (which can be filled with other tasks or idle cycles).

**Greedy Insight:**
Tasks with the **highest remaining frequencies** are the bottleneck. If you hold back high-frequency tasks, you will run out of variety later and be forced to emit consecutive idle cycles. Therefore:
1. Always greedily execute the task with the highest remaining frequency.
2. In each block of `n + 1` cycles, pick the top distinct tasks available.

```
ASCII Timeline Slot Diagram:
Tasks: [A, A, A, B, B, B], Cooldown n = 2
Max frequency task = 'A' (count = 3).
Chunk layout dictated by max frequency:

Frame 1:  [ A ] [ B ] [ idle ]
Frame 2:  [ A ] [ B ] [ idle ]
Frame 3:  [ A ] [ B ] (Final chunk only needs tasks with max frequency)

Total Time: (max_freq - 1) * (n + 1) + (count of max_freq tasks)
           = (3 - 1) * (2 + 1) + 2
           = 2 * 3 + 2 = 8 cycles.
```

#### Production C# (.NET 8/9 with `PriorityQueue`)

```csharp
namespace DsaMastery.Greedy;

using System;
using System.Collections.Generic;

public static class TaskScheduler
{
    /// <summary>
    /// Approach 1: Greedy Max-Heap simulation using PriorityQueue.
    /// Time Complexity: O(N * log(distinct_tasks)) = O(N) since alphabet size <= 26.
    /// Space Complexity: O(1) auxiliary space (size 26 arrays).
    /// </summary>
    public static int LeastIntervalHeap(char[] tasks, int n)
    {
        ArgumentNullException.ThrowIfNull(tasks);
        if (tasks.Length == 0) return 0;
        if (n == 0) return tasks.Length;

        Span<int> freq = stackalloc int[26];
        foreach (char t in tasks)
        {
            freq[t - 'A']++;
        }

        // Max-heap: highest frequency has priority
        var maxHeap = new PriorityQueue<char, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
        for (int i = 0; i < 26; i++)
        {
            if (freq[i] > 0)
            {
                char taskChar = (char)('A' + i);
                maxHeap.Enqueue(taskChar, freq[i]);
            }
        }

        int totalCycles = 0;
        var waitingList = new List<(char Task, int RemainingFreq)>(n + 1);

        while (maxHeap.Count > 0)
        {
            waitingList.Clear();
            int cycleSlots = n + 1;
            int tasksProcessed = 0;

            // Greedily consume up to (n + 1) tasks from the top of the heap
            while (cycleSlots > 0 && maxHeap.Count > 0)
            {
                maxHeap.TryPeek(out char task, out int priority);
                maxHeap.Dequeue();

                int remaining = priority - 1;
                if (remaining > 0)
                {
                    waitingList.Add((task, remaining));
                }

                tasksProcessed++;
                cycleSlots--;
            }

            // Re-enqueue tasks that still have remaining occurrences
            foreach (var item in waitingList)
            {
                maxHeap.Enqueue(item.Task, item.RemainingFreq);
            }

            // If heap is empty, only add actual executed tasks; otherwise add full frame (n + 1)
            totalCycles += (maxHeap.Count == 0) ? tasksProcessed : (n + 1);
        }

        return totalCycles;
    }

    /// <summary>
    /// Approach 2: Greedy Mathematical Slot Layout.
    /// Time Complexity: O(N) single pass.
    /// Space Complexity: O(1) auxiliary memory.
    /// </summary>
    public static int LeastIntervalMath(char[] tasks, int n)
    {
        ArgumentNullException.ThrowIfNull(tasks);
        if (tasks.Length == 0) return 0;
        if (n == 0) return tasks.Length;

        Span<int> freq = stackalloc int[26];
        int maxFreq = 0;

        foreach (char t in tasks)
        {
            int count = ++freq[t - 'A'];
            if (count > maxFreq) maxFreq = count;
        }

        int maxFreqTaskCount = 0;
        for (int i = 0; i < 26; i++)
        {
            if (freq[i] == maxFreq)
            {
                maxFreqTaskCount++;
            }
        }

        // Calculate slots dictated by the most frequent task
        int emptyChunkCount = maxFreq - 1;
        int chunkCapacity = n - (maxFreqTaskCount - 1);
        int idleSlots = emptyChunkCount * Math.Max(0, chunkCapacity);
        int availableTasks = tasks.Length - (maxFreq * maxFreqTaskCount);
        int unpaddedIdles = Math.Max(0, idleSlots - availableTasks);

        return tasks.Length + unpaddedIdles;
    }
}
```

#### Production Python (3.11+ with `heapq`)

```python
from collections import Counter, deque
import heapq
from typing import List

def least_interval_heap(tasks: List[str], n: int) -> int:
    """
    Greedy max-heap task scheduler simulation.
    Uses negative values in heapq to simulate a max-heap.
    Time Complexity: O(N) where N = len(tasks)
    Space Complexity: O(1) auxiliary space (at most 26 unique characters)
    """
    if not tasks:
        return 0
    if n == 0:
        return len(tasks)

    counts = Counter(tasks)
    # Python heapq is a min-heap; negate frequencies for max-heap behavior
    max_heap = [-cnt for cnt in counts.values()]
    heapq.heapify(max_heap)

    total_time = 0
    # Queue stores tuples of (negated_remaining_count, ready_time)
    cooldown_queue: deque[tuple[int, int]] = deque()

    while max_heap or cooldown_queue:
        total_time += 1

        if max_heap:
            # Greedily dispatch the task with highest remaining count
            neg_count = heapq.heappop(max_heap) + 1  # -count + 1 moves closer to 0
            if neg_count < 0:
                cooldown_queue.append((neg_count, total_time + n))

        # Check if any cooling task is ready to re-enter the heap
        if cooldown_queue and cooldown_queue[0][1] == total_time:
            ready_neg_count, _ = cooldown_queue.popleft()
            heapq.heappush(max_heap, ready_neg_count)

    return total_time

def least_interval_math(tasks: List[str], n: int) -> int:
    """
    Greedy mathematical slot layout formula.
    Time Complexity: O(N)
    Space Complexity: O(1) auxiliary memory
    """
    if not tasks:
        return 0
    if n == 0:
        return len(tasks)

    counts = Counter(tasks)
    max_freq = max(counts.values())
    max_freq_tasks = sum(1 for cnt in counts.values() if cnt == max_freq)

    # Calculate frame geometry
    frame_chunks = max_freq - 1
    empty_slots_per_chunk = n - (max_freq_tasks - 1)
    total_idle_slots = frame_chunks * max(0, empty_slots_per_chunk)
    remaining_tasks = len(tasks) - (max_freq * max_freq_tasks)
    idles_needed = max(0, total_idle_slots - remaining_tasks)

    return len(tasks) + idles_needed
```

---

## ⚖️ CHAPTER 5: PERFORMANCE, TRADE-OFFS & COMPLEXITY DECONSTRUCTION

### Explicit Complexity Breakdown

| Algorithm Approach | Time Complexity | Auxiliary Space | Cache Friendliness | Best Use Case |
| :--- | :--- | :--- | :--- | :--- |
| **Sort & Scan Greedy** | `O(N log N)` | `O(1)` or `O(N)` | High (sequential array access) | Offline static inputs |
| **Heap-Based Greedy** | `O(N log K)` | `O(K)` | Moderate (tree node jumping) | Online streams, dynamic arrivals |
| **Mathematical Slot Greedy** | `O(N)` | `O(1)` (alphabet size 26) | Optimal (single array pass) | Bounded alphabet scheduling |
| **Dynamic Programming (Alternative)** | `O(N^2)` to `O(N * W)` | `O(N * W)` | Poor (large table lookups) | Problems with item inter-dependencies |

#### Deconstruction Notes:
1. **Bottleneck Isolation:** In sort-based greedy algorithms, the comparator sort dominates runtime (`O(N log N)`). The greedy choice itself is almost always a single linear pass `O(N)`.
2. **Space Efficiency:** Greedy solutions regularly achieve `O(1)` auxiliary space because they discard previously evaluated candidates without retaining backtracking states.

---

## 🎙️ CHAPTER 6: 45-MINUTE INTERVIEW VERBAL SCRIPT

This verbatim script demonstrates how to communicate a greedy strategy to a Staff/Principal interviewer:

```
[00:00 - 05:00] Clarifying Questions & Constraints
"Before jumping into an implementation, let me confirm the constraints:
 1. Are tasks incoming as a streaming sequence or an offline static batch?
 2. What is the alphabet size? (If standard uppercase A-Z, our frequency map is O(1) space).
 3. Is the cooldown 'n' measured between two identical tasks? (e.g., A -> B -> A requires n=1).
 The objective is to minimize total CPU execution units including mandatory idles."

[05:00 - 12:00] Exploring Paradigms: Why DP is Overkill and Greedy is Optimal
"A naive approach might attempt backtracking or DP to find the optimal task permutation.
 However, notice the structural bottleneck: the task with the maximum frequency dictates
 the lower bound of execution time.
 If task A appears 4 times and n=2, we are physically forced to space out the 4 instances
 of A across at least 3 cooldown gaps: [A] [ _ ] [ _ ] [A] [ _ ] [ _ ] [A] [ _ ] [ _ ] [A].
 
 This gives us the Greedy Choice Property: we should always prioritize executing the
 task with the highest remaining frequency. Doing so packs the mandatory cooldown gaps
 with actual work instead of idle cycles. We don't need DP because we never regret
 dispatching the most frequent available task."

[12:00 - 20:00] Solution Architecture & Visual Walkthrough
"I can solve this in two ways:
 1. Simulation using a Max-Heap and Cooldown Queue: At each tick, pop the highest frequency
    task, decrement its count, and enqueue it in a waiting buffer until tick + n.
 2. Closed-Form Mathematical Greedy Calculation:
    Let max_freq be M. We create (M - 1) frames, each containing (n + 1) slots.
    We count how many tasks share this maximum frequency.
    We then populate remaining slots with other tasks. If available tasks exceed the idle slots,
    the answer is simply the total number of tasks!
 I will implement the mathematical approach for optimal O(N) runtime and O(1) space,
 followed by the heap simulation if we want to trace real execution schedules."

[20:00 - 35:00] Clean Coding (Walk through the C#/Python implementation methodically)
"Notice I use a fixed 26-element array for frequencies. In C# we use stackalloc Span<int>
 to achieve zero heap allocations. In Python we use Counter. We compute max_freq in a single pass..."

[35:00 - 42:00] Complexity Verification
"Time Complexity: Exactly O(N) to tally frequencies, where N is tasks.length. Finding the max
 frequency is O(26) = O(1). Total time is O(N).
 Space Complexity: O(26) = O(1) auxiliary space. This is cache-friendly and runs in under 2ms."

[42:00 - 45:00] Edge Cases & Defenses
"Edge cases covered:
 - n = 0: No cooldown, returns tasks.Length directly.
 - All tasks identical: Max idle cycles generated.
 - Distinct tasks exceed cooldown: Idles become negative, clamp to 0 via Math.Max, returns total tasks."
```


---

## 🏛️ CHAPTER 4: CANONICAL FAANG GREEDY PATTERNS — RUNNING DEFICIT & RANGE EXPANSION

> [!IMPORTANT]
> **Why FAANG Loves These Problems:**
> **Gas Station (LC 134)** and **Jump Game II (LC 45)** separate candidates who memorize disjointed loops from senior engineers who can derive **monotonic skip proofs**: proving why an entire range of suboptimal starting points or intermediate states can be skipped in `O(1)` without missing the global optimum.

---

### 🧠 Pattern 1: The Running Deficit Invariant — Gas Station (LeetCode 134)

#### The Problem & Intuitive Hook
You have `n` gas stations along a circular route. You have `gas[i]` at station `i` and it costs `cost[i]` to travel to station `i + 1`. Find the starting gas station index from which you can travel around the circuit once clockwise.

#### The Mathematical Skip Invariant
1. **Total Solvability Invariant:** If `sum(gas) < sum(cost)`, the circuit is impossible to complete from *any* station. Return `-1`.
2. **Local Discard-Safety Proof:** Suppose you start at station `A` and run out of gas at station `B` (where `B >= A`).
   - For any intermediate station `k` (where `A <= k <= B`), the gas accumulated from `A` to `k` was non-negative.
   - Starting at `k` with an empty tank means you have *less* gas than reaching `k` starting from `A`!
   - Therefore, no station in the entire range `[A..B]` can possibly reach past `B`. We safely skip all of them and reset our candidate start station directly to `B + 1`!

```text
Gas:   [ 1,  2,  3,  4,  5 ]
Cost:  [ 3,  4,  5,  1,  2 ]
Diff:  [-2, -2, -2, +3, +3 ]

Trace:
i=0: tank = -2 < 0 -> Fails! Reset start = 1, tank = 0
i=1: tank = -2 < 0 -> Fails! Reset start = 2, tank = 0
i=2: tank = -2 < 0 -> Fails! Reset start = 3, tank = 0
i=3: tank = +3 >= 0 -> Valid. Keep start = 3.
i=4: tank = 3 + 3 = 6 >= 0 -> Valid.
Total gas (15) >= Total cost (15) -> Start index 3 is guaranteed optimal!
```

#### Dual-Language Implementation (LC 134)

##### C# (.NET 8/9 Zero-Allocation Implementation)

```csharp
namespace DsaMastery.Greedy;

public static class GasStationSolver
{
    /// <summary>
    /// Finds the starting gas station index in a circular circuit.
    /// Time Complexity: O(N) single linear scan.
    /// Auxiliary Space: O(1).
    /// </summary>
    public static int CanCompleteCircuit(int[] gas, int[] cost)
    {
        int totalSurplus = 0;
        int currentTank = 0;
        int startStation = 0;

        for (int i = 0; i < gas.Length; i++)
        {
            int gain = gas[i] - cost[i];
            totalSurplus += gain;
            currentTank += gain;

            // If current tank drops below 0, stations [startStation..i] are all invalid
            if (currentTank < 0)
            {
                startStation = i + 1;
                currentTank = 0;
            }
        }

        // If total gas across the entire circuit is deficient, no solution exists
        return totalSurplus >= 0 ? startStation : -1;
    }
}
```

##### Python 3.11+ Idiomatic Implementation

```python
class Solution:
    def canCompleteCircuit(self, gas: list[int], cost: list[int]) -> int:
        """
        Determines the starting station index for a full circular traversal.
        Time Complexity: O(N)
        Auxiliary Space: O(1)
        """
        total_surplus = 0
        current_tank = 0
        start_station = 0

        for i in range(len(gas)):
            diff = gas[i] - cost[i]
            total_surplus += diff
            current_tank += diff

            # Monotonic discard: reset start boundary past failed segment
            if current_tank < 0:
                start_station = i + 1
                current_tank = 0

        return start_station if total_surplus >= 0 else -1
```

---

### 🧠 Pattern 2: Greedy Range Expansion — Jump Game II (LeetCode 45)

#### The Problem & Intuitive Hook
Given an array `nums` where `nums[i]` represents your maximum jump length from index `i`, return the minimum number of jumps to reach index `n - 1`.

#### The BFS-Style Window Invariant
Instead of testing combinations or running `O(N^2)` dynamic programming, view each jump as defining a **range window `[currentEnd, maxReach]`**:
* Within the current jump window, we greedily determine the maximum forward reach `maxReach = max(maxReach, i + nums[i])`.
* When our index pointer reaches `currentEnd`, we must spend 1 jump. We update `currentEnd = maxReach`.
* This implicitly explores BFS levels in `O(N)` time and `O(1)` space!

```text
nums = [ 2, 3, 1, 1, 4 ]

Window 0: [Index 0]          -> Reach = 0 + 2 = 2. Jump 1! Next window boundary = 2.
Window 1: [Index 1, 2]       -> At 1, reach = 1 + 3 = 4. Reached end index 4!
Total Jumps = 2.
```

#### Dual-Language Implementation (LC 45)

##### C# (.NET 8/9 Modern Implementation)

```csharp
namespace DsaMastery.Greedy;

public static class JumpGameIISolver
{
    /// <summary>
    /// Computes minimum jumps to reach the last index using greedy window expansion.
    /// Time Complexity: O(N) single pass.
    /// Auxiliary Space: O(1).
    /// </summary>
    public static int Jump(int[] nums)
    {
        if (nums == null || nums.Length <= 1)
        {
            return 0;
        }

        int jumps = 0;
        int currentWindowEnd = 0;
        int maxReachable = 0;
        int target = nums.Length - 1;

        for (int i = 0; i < target; i++)
        {
            maxReachable = Math.Max(maxReachable, i + nums[i]);

            // Reached boundary of current jump: must commit to the next jump
            if (i == currentWindowEnd)
            {
                jumps++;
                currentWindowEnd = maxReachable;

                if (currentWindowEnd >= target)
                {
                    break;
                }
            }
        }

        return jumps;
    }
}
```

##### Python 3.11+ Idiomatic Implementation

```python
class Solution:
    def jump(self, nums: list[int]) -> int:
        """
        Calculates the minimum number of jumps using greedy window horizons.
        Time Complexity: O(N)
        Auxiliary Space: O(1)
        """
        if len(nums) <= 1:
            return 0

        jumps = 0
        current_window_end = 0
        max_reachable = 0
        target = len(nums) - 1

        for i in range(target):
            max_reachable = max(max_reachable, i + nums[i])

            # Exhausted current jump tier: advance horizon
            if i == current_window_end:
                jumps += 1
                current_window_end = max_reachable
                if current_window_end >= target:
                    break

        return jumps
```

---

### 🎙️ 45-Minute Senior Interview Verbal Script

```text
+-----------------------+------------------------------------------------------------------------------------------+
| INTERVIEW STEP        | SPOKEN SCRIPT (WHAT YOU SAY ALOUD TO THE INTERVIEWER)                                    |
+-----------------------+------------------------------------------------------------------------------------------+
| 1. Discard-Proof Pitch| "In Gas Station, if a start candidate A runs out of fuel at station B, no station k      |
|                       | between A and B can succeed either, because starting at k with zero gas is strictly     |
|                       | worse than arriving at k with non-negative residual gas from A.                         |
|                       | Thus, we discard [A..B] in O(1) and reset our candidate start index to B + 1."           |
+-----------------------+------------------------------------------------------------------------------------------+
| 2. Total Net Invariant| "Because the circuit is closed, if the overall net gas across all stations is non-negative|
|                       | (sum(gas) >= sum(cost)), a valid starting index is mathematically guaranteed to exist."  |
+-----------------------+------------------------------------------------------------------------------------------+
```

---

## ⚔️ SUPPLEMENTARY OUTCOMES

### 🏋️ Practice Problems (10 Problems)

| # | Problem | Source | Difficulty | Greedy Rule |
| :--- | :--- | :--- | :--- | :--- |
| 1 | Non-overlapping Intervals | LeetCode 435 | Medium | Earliest finish time |
| 2 | Meeting Rooms II | LeetCode 253 | Medium | Sweep-line / Min-heap of end times |
| 3 | Jump Game | LeetCode 55 | Medium | Furthest reachable index |
| 4 | Jump Game II | LeetCode 45 | Medium | Maximum reach within current jump window |
| 5 | Gas Station | LeetCode 134 | Medium | Reset start candidate on negative running balance |
| 6 | Task Scheduler | LeetCode 621 | Medium | Bottleneck frequency slot allocation |
| 7 | Partition Labels | LeetCode 763 | Medium | Extend partition boundary to furthest last occurrence |
| 8 | Minimum Number of Arrows | LeetCode 452 | Medium | Shoot at earliest balloon end coordinate |
| 9 | Remove K Digits | LeetCode 402 | Medium | Monotonic stack: greedily drop peaks |
| 10 | Reorganize String | LeetCode 767 | Medium | Max-heap: place most frequent character alternate |

### 🎙️ Interview Questions (8 Questions)

1. **Q:** What is the precise difference between the Greedy Choice Property and Optimal Substructure?
   - *Answer:* Optimal Substructure means optimal solutions to subproblems combine into an optimal whole (shared with DP). Greedy Choice Property means local optimization can be made *without* solving subproblems first.
2. **Q:** Can an exchange argument be used to prove that a greedy algorithm is suboptimal?
   - *Answer:* No. The exchange argument is a proof of optimality. To prove an algorithm is suboptimal, provide a single concrete counterexample.
3. **Q:** Why does greedy fail for 0/1 Knapsack but succeed for Fractional Knapsack?
   - *Answer:* Item indivisibility creates capacity fragmentation. A high-density item might leave empty space that cannot fit other high-value items.
4. **Q:** How do you break ties in greedy algorithms without jeopardizing correctness?
   - *Answer:* In valid greedy algorithms, any tie-breaking rule preserves optimality as long as all candidates share the same optimal greedy metric value.
5. **Q:** How does Dijkstra's algorithm exhibit greedy properties?
   - *Answer:* It always locks in the shortest unvisited distance node via a priority queue, guaranteeing that no shorter path can reach it if all edge weights are non-negative.
6. **Q:** What is the time complexity bottleneck in most greedy algorithms?
   - *Answer:* The sorting step (`O(N log N)`) or heap operations (`O(N log K)`). The decision loop is typically linear (`O(N)`).
7. **Q:** When would you favor a simulation heap approach over a closed-form greedy formula?
   - *Answer:* When the system requires streaming dynamic task arrivals, custom dependencies, or non-uniform execution durations.
8. **Q:** How does the Linux Completely Fair Scheduler (CFS) use greedy logic?
   - *Answer:* It continuously dispatches the process with the minimum virtual runtime (`vruntime`) from a red-black tree.

---

**End of Week 12 Day 01 Instructional File**

---
> 🧭 **Navigation:** [← Week Overview](README.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_12_Day_02_Activity_Selection_And_Interval_Problems_Instructional.md)

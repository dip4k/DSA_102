# Phase 14: Greedy

> **Focus:** Local Optimal Decisions, Max-Reach Envelopes, Running Deficit Invariants in Circular Topologies, Last-Occurrence Substring Partitioning, and Layered Horizon Jumping.  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 14 (Problems #75–#78)

---

## 75. Jump Game (LeetCode #55)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#greedy` `#max-reach` `#boundary-scanning` |
| **LeetCode Link** | [Jump Game](https://leetcode.com/problems/jump-game/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given an integer array `nums`. You are initially positioned at the array's first index (`nums[0]`), and each element in the array represents your maximum jump length at that position. Return `true` if you can reach the last index, or `false` otherwise.
- **Key Constraints:**
  - $1 \le nums.Length \le 10^4$.
  - $0 \le nums[i] \le 10^5$.
- **Senior Edge Cases to Defend:**
  - Single element array ($nums.Length == 1 \implies \text{true}$ immediately without jumping).
  - Trapping zeros (e.g. $[3, 2, 1, 0, 4]$ where index 3 holds 0, preventing progress).
  - Leading zero in multi-element array ($[0, 2, 3] \implies \text{false}$).
  - Huge jump capacities ($nums[0] = 10^5$ immediately covers the array).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Maintain a dynamic accessible horizon `maxReach`. As long as cursor $i \le maxReach$, the position is accessible, and the horizon expands to $\max(maxReach, i + nums[i])$. If cursor $i > maxReach$, we have stepped into an unreachable void, so return `false`.
- **Sample 1:**
  - **Input:** `nums = [2, 3, 1, 1, 4]` $\implies$ `true`
- **Sample 2:**
  - **Input:** `nums = [3, 2, 1, 0, 4]` $\implies$ `false`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)
- **3.1 The Intuitive Spark & Conceptual Metaphor:**
  - *The Expanding Flashlight Beam in the Dark:* You are stepping through a pitch-black cave across a series of stepping stones. Standing at stone $i$, your flashlight illuminates stones ahead up to $i + nums[i]$. As you walk forward, you keep updating the farthest stone illuminated so far: $maxReach = \max(maxReach, i + nums[i])$. But there is one strict rule: you can only step on a stone if it is ALREADY illuminated ($i \le maxReach$). If you ever arrive at a stone $i > maxReach$, you are stepping into a bottomless abyss $\implies$ game over, return `false`. If the illuminated beam ever touches or surpasses the final stone ($maxReach \ge N - 1$), you are guaranteed to reach the exit!
- **3.2 The Naive Bottleneck & Redundant Computation:**
  - Recursive backtracking tries every jump length from 1 to $nums[i]$, leading to $O(2^N)$ overlapping paths.
  - 1D Dynamic Programming checks if any previous reachable index can jump to $i$, requiring $O(N^2)$ nested loops.
  - The Greedy insight observes that we do not need to know *which* specific sequence of jumps got us to index $i$; as long as $i \le maxReach$, index $i$ is guaranteed reachable by some valid sequence. This reduces $O(N^2)$ to a single $O(N)$ pass with $O(1)$ memory.
- **3.3 The Breakthrough Insight & Mathematical Invariant:**
  - **Accessible Horizon Invariant:**
    $$\forall j \le maxReach: \text{Index } j \text{ is reachable from index } 0$$
    If cursor $i \le maxReach$, $i$ is reachable. Standing at $i$, we can jump to any index in $[i, i + nums[i]]$.
    Hence, the updated reachable boundary is:
    $$maxReach \leftarrow \max(maxReach, i + nums[i])$$
  - **Backward Goal Shift Invariant:**
    Alternatively, working backwards: set $goal = N - 1$. Scan backwards from $N - 2$ down to 0. If $i + nums[i] \ge goal$, then index $i$ can reach the current goal, so we shift $goal = i$. A valid path exists if and only if $goal == 0$ at the end.
- **3.4 Cursor Semantics & Invariant Partition Architecture:**
  ```text
  Forward Horizon Partition:
  +-------------------------------------+-------------------------------------+
  |   Accessible Territory              |   Unreachable Void                  |
  |   Indices 0 ... maxReach            |   Indices maxReach + 1 ... N - 1    |
  +-------------------------------------+-------------------------------------+
       ^
       Cursor i advances here.
       If i > maxReach  ===> FATAL: Stepped into the Void! Return false.
       If maxReach >= N-1 ===> VICTORY: Exit reached! Return true.
  ```
- **3.5 State Transition Triggers & Decision Gates:**
  - Void Gate: `if (i > maxReach) return false;`
  - Horizon Expansion: `maxReach = Math.Max(maxReach, i + nums[i]);`
  - Early Exit Gate: `if (maxReach >= nums.Length - 1) return true;`
- **3.6 Concrete Step-by-Step State Trace:**
  - *Positive Trace:* `nums = [2, 3, 1, 1, 4]`

  | Index `i` | `nums[i]` | Jump Reach ($i + nums[i]$) | `i <= maxReach`? | Updated `maxReach` | Victory Check ($maxReach \ge 4$) |
  | :--- | :--- | :--- | :--- | :--- | :--- |
  | 0 | 2 | $0 + 2 = 2$ | $0 \le 0$ (OK) | $\max(0, 2) = 2$ | $2 \ge 4$ (False) |
  | 1 | 3 | $1 + 3 = 4$ | $1 \le 2$ (OK) | $\max(2, 4) = 4$ | $4 \ge 4$ (**True! Return true**) |

  - *Negative Trace:* `nums = [3, 2, 1, 0, 4]`

  | Index `i` | `nums[i]` | Jump Reach ($i + nums[i]$) | `i <= maxReach`? | Updated `maxReach` | Status |
  | :--- | :--- | :--- | :--- | :--- | :--- |
  | 0 | 3 | $0 + 3 = 3$ | $0 \le 0$ (OK) | 3 | Continue |
  | 1 | 2 | $1 + 2 = 3$ | $1 \le 3$ (OK) | 3 | Continue |
  | 2 | 1 | $2 + 1 = 3$ | $2 \le 3$ (OK) | 3 | Continue |
  | 3 | 0 | $3 + 0 = 3$ | $3 \le 3$ (OK) | 3 | Continue |
  | 4 | 4 | - | $4 \le 3$ (**FAIL**) | - | **Void Breach**: Return `false` |

### 4. Approach & Complexity Deconstruction
- **4.1 Anchor Points & Approach Selection Criteria:**
  - **Approach 1 (Forward Max-Reach Expansion):** Preferred in production. Allows immediate early exit as soon as `maxReach >= n - 1` without processing the rest of the array.
  - **Approach 2 (Backward Goal Shift):** Mathematically elegant inductive proof. Scans backwards from target to origin.
- **4.2 Step-by-Step Natural Progression Flow:**
  - *Step 1:* Initialize `maxReach = 0`.
  - *Step 2:* Loop `i` from 0 to $n - 1$.
  - *Step 3:* If `i > maxReach`, return `false`. Expand `maxReach`. If `maxReach >= n - 1`, return `true`.
  - *Step 4:* Return `true` if loop finishes.
- **4.3 Alternative Approaches Analysis:**
  - 1D Dynamic Programming: `bool[] dp = new bool[n]`. `dp[0] = true`. For each $i$, if $dp[i]$, mark all reachable $i + j$ as true. $O(N^2)$ time, $O(N)$ space—sub-optimal.
- **4.4 Multi-Dimensional Complexity & Trade-Off Matrix:**

| Approach | Time (Best) | Time (Avg/Worst) | Auxiliary Space | Early Termination |
| :--- | :--- | :--- | :--- | :--- |
| **Forward Max-Reach** | $O(1)$ | $O(N)$ | $O(1)$ | Yes (Instant exit on $maxReach \ge N-1$) |
| **Backward Goal Shift**| $O(N)$ | $O(N)$ | $O(1)$ | No (Full backward scan) |
| **1D Dynamic Programming**| $O(N)$ | $O(N^2)$ | $O(N)$ | Moderate |

### 5. Production C# Implementations

```csharp
// ============================================================================
// ARCHITECTURE ANCHOR: Jump Game
// Primary: Forward Max-Reach Greedy Horizon (O(N) Time, O(1) Space, Early Exit)
// Secondary: Backward Target Goal Pull (O(N) Time, O(1) Space)
// Invariant: If cursor i <= maxReach, position i is reachable from origin
// ============================================================================

public class Solution
{
    /// <summary>
    /// Evaluates reachability to the last index using forward max-reach horizon expansion.
    /// Exits early the instant the last index falls within reach.
    /// </summary>
    public bool CanJump(int[] nums)
    {
        if (nums == null || nums.Length == 0) return false;
        if (nums.Length == 1) return true;

        int maxReach = 0;
        int target = nums.Length - 1;

        for (int i = 0; i < nums.Length; i++)
        {
            // Void Breach Gate: Stepped beyond the maximum reachable horizon
            if (i > maxReach)
            {
                return false;
            }

            // Invariant Expansion: Update the farthest reachable stepping stone
            maxReach = Math.Max(maxReach, i + nums[i]);

            // Early Victory Gate: Target is already reachable; skip remaining computations
            if (maxReach >= target)
            {
                return true;
            }
        }

        return true;
    }
}

public class SolutionBackward
{
    /// <summary>
    /// Evaluates reachability by pulling the target goal backward from N-1 toward 0.
    /// </summary>
    public bool CanJump(int[] nums)
    {
        if (nums == null || nums.Length == 0) return false;

        int goal = nums.Length - 1;

        // Invariant: If index i can reach the current goal, i becomes the new goal
        for (int i = nums.Length - 2; i >= 0; i--)
        {
            if (i + nums[i] >= goal)
            {
                goal = i;
            }
        }

        // Complete reachability implies origin index 0 is capable of initiating the chain
        return goal == 0;
    }
}
```

---

## 76. Gas Station (LeetCode #134)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#greedy` `#circular-array` `#running-deficit` `#prefix-inversion` |
| **LeetCode Link** | [Gas Station](https://leetcode.com/problems/gas-station/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** There are $n$ gas stations along a circular route, where the amount of gas at the $i$-th station is `gas[i]`. It costs `cost[i]` of gas to travel from station $i$ to station $i + 1$. You begin the journey with an empty tank at one of the gas stations. Return the starting gas station's index if you can travel around the circuit once in the clockwise direction; otherwise return `-1`. If a solution exists, it is guaranteed to be unique.
- **Key Constraints:**
  - $n == gas.Length == cost.Length \in [1, 10^5]$.
  - $0 \le gas[i], cost[i] \le 10^4$.
- **Senior Edge Cases to Defend:**
  - $\sum gas < \sum cost$ (Total gas across all stations is strictly less than total cost; mathematically impossible, return `-1`).
  - Single station circuit ($n = 1$: can complete if $gas[0] \ge cost[0]$, else `-1`).
  - Massive deficit at station 0, followed by huge surpluses later in the circuit.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Global Feasibility + Local Deficit Reset: If $\sum gas \ge \sum cost$, a solution is mathematically guaranteed to exist. If the tank drops below 0 when traveling from candidate $start$ to $i$, no intermediate station in $[start, i]$ could possibly succeed either; jump candidate start directly to $i + 1$.
- **Sample 1:**
  - **Input:** `gas = [1, 2, 3, 4, 5]`, `cost = [3, 4, 5, 1, 2]`
  - **Output:** `3`
- **Sample 2:**
  - **Input:** `gas = [2, 3, 4]`, `cost = [3, 4, 3]`
  - **Output:** `-1` (Total gas 9 < Total cost 10)

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)
- **3.1 The Intuitive Spark & Conceptual Metaphor:**
  - *The Car with the Fuel Deficit:* Imagine driving around a circular racetrack. At each station, your net fuel gain is $diff[i] = gas[i] - cost[i]$.
    1. **The Global Balance:** If the entire circuit runs a net deficit ($\sum diff < 0$), no car can ever complete a lap, regardless of where it starts.
    2. **The Candidate Pruning Leap:** Suppose you start at station $A$ with tank 0 and run completely dry between station $B$ and $B+1$ ($currentTank < 0$). Could any intermediate station $C$ between $A$ and $B$ have been the valid starting point? **NO!** Why? Because when driving from $A$ to $C$, your tank was $\ge 0$ (otherwise you would have stalled before reaching $C$). Thus, arriving at $C$ from $A$ gave you *bonus* gas! If starting at $A$ and arriving at $C$ with bonus gas still caused you to fail at $B$, starting at $C$ with *zero* gas will fail even faster!
    - Therefore, the entire range $[A, B]$ is disqualified in one fell swoop! Jump candidate start to $B + 1$!
- **3.2 The Naive Bottleneck & Redundant Computation:**
  - Simulating a full circuit starting from each station $i \in [0, n - 1]$ takes $O(N^2)$ time.
  - The deficit reset pruning eliminates re-testing disqualified candidate stations, reducing exploration to a single $O(N)$ pass.
- **3.3 The Breakthrough Insight & Mathematical Invariant:**
  - **Formal Candidate Disqualification Proof:**
    Let candidate start be $S$. Suppose $\sum_{k=S}^j diff[k] \ge 0$ for all $j \in [S, i - 1]$, but drops below zero at $j = i$:
    $$\sum_{k=S}^i diff[k] < 0$$
    For any intermediate station $C \in [S + 1, i]$:
    $$\sum_{k=C}^i diff[k] = \sum_{k=S}^i diff[k] - \sum_{k=S}^{C - 1} diff[k]$$
    Since $S$ successfully reached $C$, $\sum_{k=S}^{C - 1} diff[k] \ge 0$. Subtracting a non-negative value from a negative value yields a strictly negative result:
    $$\sum_{k=C}^i diff[k] < 0$$
    Hence, starting at $C$ with tank 0 is guaranteed to run dry at or before station $i$. It is provably safe to discard all stations in $[S, i]$ and set candidate $start = i + 1$.
  - **Global Solvability Theorem:** If $\sum_{i=0}^{n-1} diff[i] \ge 0$, then the last surviving candidate station $start$ is guaranteed to complete the entire circular lap!
- **3.4 Cursor Semantics & Invariant Partition Architecture:**
  ```text
  Circular Array Sweep:
  +--------------------------------+--------------------+-------------------------------+
  |   Disproved Candidates         |   Candidate Start  |   Unexplored Stations         |
  |   Indices 0 ... i              |   Index i + 1      |   Indices i + 2 ... n - 1     |
  +--------------------------------+--------------------+-------------------------------+
                                   ^
  Reset currentTank = 0, candidateStart = i + 1
  ```
- **3.5 State Transition Triggers & Decision Gates:**
  - Accumulation: `totalSurplus += diff; currentTank += diff;`
  - Deficit Reset Gate:
    ```csharp
    if (currentTank < 0)
    {
        startingStation = i + 1;
        currentTank = 0;
    }
    ```
  - Global Solvability Gate: `return totalSurplus >= 0 ? startingStation : -1;`
- **3.6 Concrete Step-by-Step State Trace:**
  - Input: `gas = [1, 2, 3, 4, 5]`, `cost = [3, 4, 5, 1, 2]`
  - Net diffs: `[-2, -2, -2, +3, +3]`

  | Station `i` | `gas[i]` | `cost[i]` | Net `diff` | `totalSurplus` | `currentTank` Before | `currentTank` After | Reset Triggered? | Candidate `startingStation` |
  | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
  | 0 | 1 | 3 | -2 | -2 | 0 | -2 | **Yes** (< 0) | $0 + 1 = 1$ |
  | 1 | 2 | 4 | -2 | -4 | 0 | -2 | **Yes** (< 0) | $1 + 1 = 2$ |
  | 2 | 3 | 5 | -2 | -6 | 0 | -2 | **Yes** (< 0) | $2 + 1 = 3$ |
  | 3 | 4 | 1 | +3 | -3 | 0 | +3 | No ($\ge 0$) | 3 |
  | 4 | 5 | 2 | +3 | 0 | +3 | +6 | No ($\ge 0$) | 3 |

  *Termination Check:* `totalSurplus == 0 >= 0` $\implies$ Return `startingStation = 3`.

### 4. Approach & Complexity Deconstruction
- **4.1 Anchor Points & Approach Selection Criteria:**
  - **Approach 1 (Single-Pass Greedy Deficit Jump):** Optimal standard approach. Tracks running tank and total surplus in a single pass. $O(N)$ time, $O(1)$ auxiliary space.
  - **Approach 2 (Minimum Valley Prefix Sum):** Identifies the global minimum of the cumulative prefix sum curve. The station immediately following the deepest deficit valley is mathematically guaranteed to be the optimal starting station.
- **4.2 Step-by-Step Natural Progression Flow:**
  - *Step 1: Setup & Initialization:* Initialize `totalSurplus = 0`, `currentTank = 0`, `startingStation = 0`.
  - *Step 2: Exploration Loop:* For each station $i$, compute `diff = gas[i] - cost[i]`. Add to both accumulators.
  - *Step 3: Invariant Maintenance:* If `currentTank < 0`, reset `startingStation = i + 1` and `currentTank = 0`.
  - *Step 4: Resolution & Return:* If `totalSurplus >= 0`, return `startingStation`; else return `-1`.
- **4.3 Alternative Approaches Analysis:**
  - Brute Force Circuit Simulation: Nested loops simulating up to $N$ steps for each start index. Takes $O(N^2)$ time.
- **4.4 Multi-Dimensional Complexity & Trade-Off Matrix:**

| Approach | Time Complexity | Auxiliary Space | Pass Count | Mathematical Foundation |
| :--- | :--- | :--- | :--- | :--- |
| **Greedy Deficit Jump** | $O(N)$ | $O(1)$ | 1 Pass | Disqualification Induction |
| **Min Valley Prefix Sum** | $O(N)$ | $O(1)$ | 1 Pass | Cumulative Inflection Point |
| **Brute Force Simulation** | $O(N^2)$ | $O(1)$ | $N$ Passes | Direct Verification |

### 5. Production C# Implementations

```csharp
// ============================================================================
// ARCHITECTURE ANCHOR: Gas Station
// Primary: Single-Pass Greedy Deficit Reset (O(N) Time, O(1) Space)
// Secondary: Minimum Valley Prefix Sum Invariant (O(N) Time, O(1) Space)
// Invariant: If path from start to i fails, no station in [start, i] can succeed
// ============================================================================

public class Solution
{
    /// <summary>
    /// Determines the starting gas station in a single linear pass using greedy deficit resets.
    /// </summary>
    public int CanCompleteCircuit(int[] gas, int[] cost)
    {
        ArgumentNullException.ThrowIfNull(gas);
        ArgumentNullException.ThrowIfNull(cost);

        int totalSurplus = 0;
        int currentTank = 0;
        int startingStation = 0;

        for (int i = 0; i < gas.Length; i++)
        {
            int netGain = gas[i] - cost[i];
            totalSurplus += netGain;
            currentTank += netGain;

            // Invariant Gate: Running deficit detected
            // If currentTank drops below zero, starting at any station in [startingStation, i] is doomed
            if (currentTank < 0)
            {
                // Greedily advance candidate start to the next unexplored station
                startingStation = i + 1;
                // Reset active tank to zero for the new candidate journey
                currentTank = 0;
            }
        }

        // Global Conservation Gate: If total fuel >= total cost, startingStation is guaranteed valid
        return totalSurplus >= 0 ? startingStation : -1;
    }
}

public class SolutionValley
{
    /// <summary>
    /// Determines the starting station by finding the global minimum valley of the cumulative prefix fuel curve.
    /// The station immediately following the deepest valley eliminates all negative tank dips.
    /// </summary>
    public int CanCompleteCircuit(int[] gas, int[] cost)
    {
        int totalSurplus = 0;
        int minSurplus = int.MaxValue;
        int minIndex = -1;

        for (int i = 0; i < gas.Length; i++)
        {
            totalSurplus += gas[i] - cost[i];

            // Track deepest cumulative deficit point along the circuit
            if (totalSurplus < minSurplus)
            {
                minSurplus = totalSurplus;
                minIndex = i;
            }
        }

        // If circuit is globally feasible, start at station right after the deepest deficit
        return totalSurplus >= 0 ? (minIndex + 1) % gas.Length : -1;
    }
}
```

---

## 77. Partition Labels (LeetCode #763)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#greedy` `#two-pointers` `#last-occurrence-map` `#interval-merge` |
| **LeetCode Link** | [Partition Labels](https://leetcode.com/problems/partition-labels/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given a string `s`. We want to partition the string into as many parts as possible so that each letter appears in at most one part. Return a list of integers representing the size of these parts.
- **Key Constraints:**
  - $1 \le s.Length \le 500$.
  - `s` consists of lowercase English letters (`'a'` through `'z'`).
- **Senior Edge Cases to Defend:**
  - All characters identical ($s = \text{"aaaaa"} \implies [5]$).
  - All characters unique ($s = \text{"abcdef"} \implies [1, 1, 1, 1, 1, 1]$).
  - Interlocking characters extending across the entire string.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Precompute the last occurrence index of each character. Greedily stretch the active partition boundary $end = \max(end, last[s[i]])$. The instant cursor $i == end$, every character seen so far is strictly confined inside $[start, end]$; seal the partition and reset.
- **Sample 1:**
  - **Input:** `s = "ababcbacadefegdehijhklij"`
  - **Output:** `[9, 7, 8]` (partitions: `"ababcbaca"`, `"defegde"`, `"hijhklij"`)
- **Sample 2:**
  - **Input:** `s = "eccbbbbdec"`
  - **Output:** `[10]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)
- **3.1 The Intuitive Spark & Conceptual Metaphor:**
  - *The Elastic Leash / Captive Character Horizon:* Imagine every character in the string holds an elastic leash anchored to its final appearance in the string: $last[c]$. As you walk from left to right:
    - You grab the leash of character $s[i]$.
    - Your active boundary $end$ must stretch to the farthest leash you are holding: $end = \max(end, last[s[i]])$.
    - You must continue walking until you reach $end$.
    - The magical moment $i == end$: You have walked past the anchor of *every single leash* you picked up since $start$! Not a single character inside $[start, end]$ exists beyond this line. Snip the partition, record its length ($end - start + 1$), and start walking the next segment.
- **3.2 The Naive Bottleneck & Redundant Computation:**
  - Repeatedly scanning forward to find the remaining occurrences of characters takes $O(N^2)$ time.
  - A pre-computation pass records the last occurrence of all 26 lowercase English letters in $O(N)$ time and $O(1)$ fixed auxiliary memory (26 integers).
- **3.3 The Breakthrough Insight & Mathematical Invariant:**
  - **Partition Sealing Invariant:**
    $$i = end \iff \forall c \in s[start \dots i]: \text{LastOccurrence}(c) \le end$$
  - Because we seal the partition at the *earliest* index $i$ where $i == end$, the resulting partition is as short as possible, which mathematically maximizes the total number of partitions!
- **3.4 Cursor Semantics & Invariant Partition Architecture:**
  ```text
  String Scanning Partition:
  +---------------------------+-----------------------------------------------+------------------------+
  |   Sealed Partitions       |   Active Chunk: start ... i ... end           |   Unexplored Suffix    |
  |   Length committed        |   end = max(last[s[start]], ..., last[s[i]])  |   Not yet scanned      |
  +---------------------------+-----------------------------------------------+------------------------+
                              ^                                               ^
                       start cursor                                    end boundary
  When i == end: Commit chunk length (end - start + 1), set start = i + 1
  ```
- **3.5 State Transition Triggers & Decision Gates:**
  - Leash Expansion: `end = Math.Max(end, lastOccurrence[s[i] - 'a']);`
  - Sealing Gate:
    ```csharp
    if (i == end)
    {
        result.Add(end - start + 1);
        start = i + 1;
    }
    ```
- **3.6 Concrete Step-by-Step State Trace:**
  - Input: `s = "ababcbacadefegdehijhklij"`
  - Last occurrences: `a: 8, b: 5, c: 7, d: 14, e: 15, f: 11, g: 13, h: 19, i: 22, j: 23, k: 20, l: 21`

  | Step `i` | Char `s[i]` | `last[s[i]]` | Active `end` Before | Updated `end` | `i == end`? | Partition Committed |
  | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
  | 0 | 'a' | 8 | 0 | $\max(0, 8) = 8$ | $0 == 8$ (No) | - |
  | 1 | 'b' | 5 | 8 | $\max(8, 5) = 8$ | $1 == 8$ (No) | - |
  | ... | ... | ... | 8 | 8 | ... | - |
  | 8 | 'a' | 8 | 8 | 8 | **$8 == 8$ (YES)** | Add $8 - 0 + 1 = \mathbf{9}$; `start = 9` |
  | 9 | 'd' | 14 | 8 | $\max(8, 14) = 14$ | $9 == 14$ (No) | - |
  | 10 | 'e' | 15 | 14 | $\max(14, 15) = 15$| $10 == 15$ (No) | - |
  | ... | ... | ... | 15 | 15 | ... | - |
  | 15 | 'e' | 15 | 15 | 15 | **$15 == 15$ (YES)**| Add $15 - 9 + 1 = \mathbf{7}$; `start = 16` |

### 4. Approach & Complexity Deconstruction
- **4.1 Anchor Points & Approach Selection Criteria:**
  - **Approach 1 (Last Occurrence Array + Two Pointers):** Optimal C# implementation. Uses `stackalloc int[26]` for zero GC heap allocations and optimal L1 CPU cache performance.
  - **Approach 2 (Interval Merging Formulation):** Computes first and last occurrence of each character as an interval $[first_c, last_c]$, then applies Merge Intervals. Conceptually unifying, but requires sorting up to 26 intervals.
- **4.2 Step-by-Step Natural Progression Flow:**
  - *Step 1: First Pass:* Record last occurrence of each character in a 26-element array.
  - *Step 2: Second Pass:* Initialize `start = 0`, `end = 0`.
  - *Step 3: Horizon Expansion:* For each index $i$, update `end = Math.Max(end, last[s[i] - 'a'])`.
  - *Step 4: Sealing:* If $i == end$, commit length, set `start = i + 1`.
- **4.3 Alternative Approaches Analysis:**
  - Interval Merging: Map each letter to its span $[first, last]$. Sort the resulting 26 intervals and merge them. Produces identical partition sizes.
- **4.4 Multi-Dimensional Complexity & Trade-Off Matrix:**

| Approach | Time Complexity | Auxiliary Space | Heap Allocations |
| :--- | :--- | :--- | :--- |
| **Last Occurrence + Two Pointers** | $O(N)$ | $O(1)$ (26 ints via stackalloc) | Zero (excluding result list) |
| **Interval Merging** | $O(N + \Sigma \log \Sigma)$ | $O(\Sigma)$ where $\Sigma = 26$ | Allocates interval array |

### 5. Production C# Implementations

```csharp
// ============================================================================
// ARCHITECTURE ANCHOR: Partition Labels
// Primary: Last-Occurrence Map + Greedy Two-Pointer Sweep (O(N) Time, O(1) Space)
// Invariant: Partition is sealed at index i iff i == max(lastOccurrence of all seen chars)
// Memory: Uses stackalloc Span<int> to eliminate GC heap allocation overhead
// ============================================================================

public class Solution
{
    /// <summary>
    /// Partitions string into maximum valid parts using greedy horizon expansion.
    /// Employs stackalloc for high-throughput zero-GC execution.
    /// </summary>
    public IList<int> PartitionLabels(string s)
    {
        var result = new List<int>();
        if (string.IsNullOrEmpty(s)) return result;

        // Pass 1: Record the absolute last occurrence index of each of the 26 characters
        // stackalloc allocates 104 bytes on the execution thread stack, bypassing the GC entirely
        Span<int> lastOccurrence = stackalloc int[26];
        for (int i = 0; i < s.Length; i++)
        {
            lastOccurrence[s[i] - 'a'] = i;
        }

        // Pass 2: Greedily expand the current partition horizon to cover all occurrences
        int start = 0;
        int end = 0;

        for (int i = 0; i < s.Length; i++)
        {
            // Expand boundary to encompass the last appearance of current character
            end = Math.Max(end, lastOccurrence[s[i] - 'a']);

            // Sealing Invariant Gate: Cursor has caught up with the farthest character leash
            if (i == end)
            {
                // Commit partition length
                result.Add(end - start + 1);
                // Start a fresh partition immediately at the next character
                start = i + 1;
            }
        }

        return result;
    }
}
```

---

## 78. Jump Game II (LeetCode #45)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#greedy` `#bfs-layers` `#jump-horizons` `#minimum-jumps` |
| **LeetCode Link** | [Jump Game II](https://leetcode.com/problems/jump-game-ii/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given a 0-indexed array of integers `nums` of length $n$, you are initially positioned at `nums[0]`. Each element `nums[i]` represents the maximum length of a forward jump from index $i$. Return the minimum number of jumps to reach `nums[n - 1]`. (It is guaranteed that you can reach the last index).
- **Key Constraints:**
  - $1 \le nums.Length \le 10^4$.
  - $0 \le nums[i] \le 1000$.
  - A solution is guaranteed to exist.
- **Senior Edge Cases to Defend:**
  - $nums.Length == 1 \implies 0$ jumps needed (already at destination).
  - First jump from index 0 covers the entire array $\implies 1$ jump.
  - Intermediate zeros that must be bypassed by earlier jumps.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Implicit Layered BFS Horizons without a Queue: Each jump level $k$ spans an accessible interval $[currStart, currEnd]$. The next jump level $k+1$ spans $[currEnd + 1, farthestReachable]$. Advance jump count only when cursor $i$ reaches `currEnd`.
- **Sample 1:**
  - **Input:** `nums = [2, 3, 1, 1, 4]`
  - **Output:** `2` (jump 1 step from 0 to 1, then 3 steps from 1 to 4)
- **Sample 2:**
  - **Input:** `nums = [2, 3, 0, 1, 4]`
  - **Output:** `2`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)
- **3.1 The Intuitive Spark & Conceptual Metaphor:**
  - *Layered BFS Wavefronts without a Queue:* Minimum steps to reach a target in an unweighted graph is the textbook definition of Breadth-First Search. In a graph, you would use a FIFO queue. But notice the structure here: from any range of positions reachable in $J$ jumps, all newly reachable positions form a contiguous forward interval!
    - Level 0 (0 jumps): $[0, 0]$
    - Level 1 (1 jump): $[1, nums[0]]$
    - Level 2 (2 jumps): $[Level_1.end + 1, \max_{i \in Level_1} (i + nums[i])]$
  - Because each BFS generation is simply an interval on the 1D line, we can track the generation boundary with just two integers: `currEnd` (the end of the current jump horizon) and `farthest` (the farthest point discovered for the next jump horizon)!
- **3.2 The Naive Bottleneck & Redundant Computation:**
  - 1D Dynamic Programming: `dp[i]` represents min jumps to reach $i$. For each $i$, update all reachable forward neighbors $\implies O(N^2)$ time.
  - An explicit BFS queue pushes overlapping intervals, requiring visited sets and $O(N)$ queue memory.
  - Greedy horizon tracking runs in optimal $O(N)$ time with $O(1)$ auxiliary memory.
- **3.3 The Breakthrough Insight & Mathematical Invariant:**
  - **BFS Generation Boundary Invariant:**
    - `currEnd`: Farthest index reachable with current count of `jumps`.
    - `farthest`: Farthest index reachable with `jumps + 1`.
    - As long as cursor $i \le currEnd$, we are still exploring the current BFS level; update `farthest = max(farthest, i + nums[i])`.
    - The instant $i == currEnd$, we have completely exhausted the current jump generation. We are forced to expend another jump:
      $$jumps \leftarrow jumps + 1, \quad currEnd \leftarrow farthest$$
    - Loop only needs to run up to $n - 2$ because once we reach $n - 1$, no further jump is required!
- **3.4 Cursor Semantics & Invariant Partition Architecture:**
  ```text
  BFS Horizon Architecture:
  [ Jump 0: 0 ] -> [ Jump 1: 1 ... nums[0] ] -> [ Jump 2: currEnd + 1 ... farthest ]
                    ^                         ^
                    cursor i scans inside     When i == currEnd: jumps++, currEnd = farthest
  ```
- **3.5 State Transition Triggers & Decision Gates:**
  - Exploration Gate: `farthest = Math.Max(farthest, i + nums[i]);`
  - Generation Exhaustion Gate:
    ```csharp
    if (i == currEnd)
    {
        jumps++;
        currEnd = farthest;
        if (currEnd >= nums.Length - 1) break; // Early exit
    }
    ```
- **3.6 Concrete Step-by-Step State Trace:**
  - Input: `nums = [2, 3, 1, 1, 4]` ($n = 5$, loop runs up to $i = 3$)

  | Step `i` | `nums[i]` | Reach ($i + nums[i]$) | `farthest` | `currEnd` | `i == currEnd`? | `jumps` |
  | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
  | Init | - | - | 0 | 0 | - | 0 |
  | 0 | 2 | $0 + 2 = 2$ | $\max(0, 2) = 2$ | 0 | **$0 == 0$ (YES)** | `jumps = 1`, `currEnd = 2` |
  | 1 | 3 | $1 + 3 = 4$ | $\max(2, 4) = 4$ | 2 | $1 == 2$ (No) | 1 |
  | 2 | 1 | $2 + 1 = 3$ | $\max(4, 3) = 4$ | 2 | **$2 == 2$ (YES)** | `jumps = 2`, `currEnd = 4` $\implies$ Early exit ($4 \ge 4$) |

  *Result:* `jumps = 2`.

### 4. Approach & Complexity Deconstruction
- **4.1 Anchor Points & Approach Selection Criteria:**
  - **Approach 1 (Greedy Horizon BFS):** The gold standard for Jump Game II. Linear $O(N)$ single pass, $O(1)$ space.
  - **Approach 2 (1D Dynamic Programming):** Classical DP tabulation. $O(N^2)$ time, $O(N)$ space.
- **4.2 Step-by-Step Natural Progression Flow:**
  - *Step 1: Setup & Boundaries:* If $n \le 1$, return 0. Initialize `jumps = 0`, `currEnd = 0`, `farthest = 0`.
  - *Step 2: Exploration Loop:* Iterate $i$ from 0 to $n - 2$.
  - *Step 3: Invariant Maintenance:* Update `farthest`. If $i == currEnd$, increment `jumps` and update `currEnd = farthest`. Early break if $currEnd \ge n - 1$.
  - *Step 4: Resolution & Return:* Return `jumps`.
- **4.3 Alternative Approaches Analysis:**
  - DP: `dp[j] = Math.Min(dp[j], dp[i] + 1)` for all $j \in [i+1, i+nums[i]]$. Simple to reason about, but does extensive redundant work.
- **4.4 Multi-Dimensional Complexity & Trade-Off Matrix:**

| Approach | Time Complexity | Auxiliary Space | Redundant Checks |
| :--- | :--- | :--- | :--- |
| **Greedy Horizon BFS** | $O(N)$ | $O(1)$ | Zero (Each index visited once) |
| **1D Dynamic Programming** | $O(N^2)$ | $O(N)$ | Heavy (Updates same indices repeatedly) |
| **Explicit Queue BFS** | $O(N)$ with visited set | $O(N)$ queue | Minor queue overhead |

### 5. Production C# Implementations

```csharp
// ============================================================================
// ARCHITECTURE ANCHOR: Jump Game II
// Primary: Greedy Layered Horizon BFS (O(N) Time, O(1) Space)
// Secondary: 1D Dynamic Programming Tabulation (O(N^2) Time, O(N) Space)
// Invariant: currEnd marks the edge of jump level k; when i reaches currEnd, jump must increment
// ============================================================================

public class Solution
{
    /// <summary>
    /// Computes minimum jumps using implicit BFS horizon expansion in linear O(N) time and O(1) space.
    /// </summary>
    public int Jump(int[] nums)
    {
        // Boundary Gate: 0 or 1 elements require 0 jumps
        if (nums == null || nums.Length <= 1)
        {
            return 0;
        }

        int jumps = 0;
        int currEnd = 0;
        int farthest = 0;

        // Loop runs strictly up to nums.Length - 2:
        // Arriving at the final index does not require spending another jump!
        for (int i = 0; i < nums.Length - 1; i++)
        {
            // Expand the reach of the upcoming jump generation
            farthest = Math.Max(farthest, i + nums[i]);

            // Generation Exhaustion Gate: Reached the boundary of the current jump horizon
            if (i == currEnd)
            {
                jumps++;
                currEnd = farthest;

                // Early Exit Gate: If the destination is already encompassed, terminate immediately
                if (currEnd >= nums.Length - 1)
                {
                    break;
                }
            }
        }

        return jumps;
    }
}

public class SolutionDp
{
    /// <summary>
    /// Computes minimum jumps using 1D Dynamic Programming tabulation.
    /// Quadratic time O(N^2); provided as an instructional alternative.
    /// </summary>
    public int Jump(int[] nums)
    {
        int n = nums.Length;
        int[] dp = new int[n];
        Array.Fill(dp, int.MaxValue);
        dp[0] = 0;

        for (int i = 0; i < n; i++)
        {
            if (dp[i] == int.MaxValue) continue;

            int maxJump = nums[i];
            for (int step = 1; step <= maxJump && i + step < n; step++)
            {
                dp[i + step] = Math.Min(dp[i + step], dp[i] + 1);
            }
        }

        return dp[n - 1];
    }
}
```

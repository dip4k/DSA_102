# 📘 Week 19, Day 3: Mock Round 3: Dynamic Programming & Greedy Decisions

> 🧭 **Navigation:** [← Previous Day](Week_19_Day_02_Mock_Trees_Graphs_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_19_Day_04_Mock_Mixed_Complex_Rounds_Instructional.md)
> 
> 💡 **Instructor Note:** *This round tests optimal substructure intuition, state space compression from 2D to 1D, and the mathematical rigor needed to defend greedy choice properties against full DP formulations.*

---

## 🎯 Learning Objectives

*   **Interview Simulation:** Experience an authentic 45-minute live senior coding interview on multi-dimensional dynamic programming and greedy exchange arguments.
*   **Dialogue Navigation:** Defend the recurrence derivation aloud, transition from `O(M * N)` space to `O(min(M, N))` cache-resident buffers, and prove greedy optimality.
*   **3-Tiered Hint Recovery:** Master systematic hint extraction for overlapping subproblems and state-reduction mechanics.
*   **Senior Rubric Mastery:** Evaluate solutions against top-tier industry criteria: Recurrence Formulation, Boundary Base Cases, Space Compression, and Defensive Production Code.

## ⚖️ FAANG Senior / Lead Interview Calibration

In Tier-1 DP rounds, the difference between a mid-level hire and a senior hire is the ability to compress space into CPU L1 cache and rigorously justify greedy choice properties:

| Paradigm Variant | Time Complexity | Auxiliary Space | Hardware Residency | Common Failure Mode | Senior Evaluation Bar |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Naive Exponential Recursion** | `O(3^(M + N))` | `O(M + N)` stack | Cache thrashing | Stack overflow; timeout | Reject immediately. |
| **Top-Down Memoization** | `O(M * N)` | `O(M * N)` hash map | High cache miss rate | Hash collision / recursion depth | Developing (L4). |
| **Full 2D DP Table** | `O(M * N)` | `O(M * N)` (~100 MB) | Exceeds L3 cache | Unnecessary RAM consumption | Mid-level bar (L4). |
| **1D Rolling Array Compression** | `O(M * N)` | `O(min(M, N))` (~20 KB)| **100% L1 Cache Resident** | Diagonal state corruption | **Senior Bar (L5): Flawless in-place scalar swaps.** |
| **Greedy Interval Horizon (Jump Game)**| `O(N)` | `O(1)` (3 registers) | CPU Register allocated | Greedy choice unproven | **Senior Bar (L5): Rigorous exchange argument proof.** |

### The Interviewer's Hidden Rubric: What We Listen For
1. **Recurrence Articulation:** Does the candidate explain the inductive subproblem transitions verbally before writing loops?
2. **Space Compression Instinct:** Does the candidate proactively point out that `dp[i][j]` only depends on row `i-1`, compressing 2D space down to 1D?
3. **Greedy vs DP Defense:** Can the candidate prove why a greedy local optimum guarantees a global optimum (subsumption / exchange argument)?

---

## 📖 Chapter 1: Live Interview Simulation & Dialogue Transcript

### Problem Statement
> **Interviewer:** *"Given two strings `word1` and `word2`, find the minimum number of operations required to convert `word1` to `word2`. You may insert a character, delete a character, or replace a character."*

```
DP Grid Dependency Layout:
             word2[j-1]
           +---------+---------+
           | dp[i-1] | dp[i-1] |
           | [j-1]   |  [j]    |  <- Delete: dp[i-1][j] + 1
word1[i-1] +---------+---------+
           | dp[i]   | dp[i]   |  <- Replace: dp[i-1][j-1] + 1 (if mismatch)
           | [j-1]   |  [j]    |  <- Insert:  dp[i][j-1] + 1
           +---------+---------+
```

---

### Authentic Interviewer Dialogue Transcript

**Candidate:** "Thank you. Let me establish our operational parameters:
1. What character encoding are we dealing with? Standard ASCII / lowercase English letters, or arbitrary Unicode?
2. What are the maximum lengths of `word1` and `word2`?
3. What is the expected behavior if either string is empty?"

**Interviewer:** "Strings contain lowercase English letters. Lengths `M` and `N` can reach up to `5,000`. If one string is empty, the edit distance is simply the length of the other string."

**Candidate:** "Great. Let's examine the optimal substructure:
Let `dp[i][j]` represent the minimum edit distance to convert the prefix `word1[0 ... i-1]` to `word2[0 ... j-1]`.
- **Base Cases:**
  `dp[0][j] = j` (converting empty string to `word2` requires `j` insertions).
  `dp[i][0] = i` (converting `word1` to empty string requires `i` deletions).
- **Inductive Transitions:**
  If `word1[i-1] == word2[j-1]`:
  No additional operation is needed: `dp[i][j] = dp[i-1][j-1]`.
  If they differ:
  We take the minimum of three valid operations plus 1:
  1. **Insert:** `dp[i][j-1] + 1`
  2. **Delete:** `dp[i-1][j] + 1`
  3. **Replace:** `dp[i-1][j-1] + 1`
  `dp[i][j] = 1 + min(dp[i][j-1], min(dp[i-1][j], dp[i-1][j-1]))`.
Computing this in a 2D matrix takes `O(M * N)` time and `O(M * N)` space."

**Interviewer:** "For `M = N = 5,000`, a 2D integer array is `5,000 * 5,000 * 4` bytes = **100 Megabytes**. That causes significant cache misses and memory pressure. Can you optimize memory?"

**Candidate:** "Yes. Notice the row dependency in our recurrence:
Computing `dp[i][j]` requires only:
1. The current row's previous value: `dp[i][j-1]`.
2. The previous row's value directly above: `dp[i-1][j]`.
3. The previous row's diagonal value: `dp[i-1][j-1]`.
We never look back two rows!
Therefore, we only need a single 1D array of size `N + 1` plus a temporary scalar variable to hold the diagonal element from the previous row.
Furthermore, we can ensure the array length is `min(M, N)` by swapping the strings if `M < N`.
This drops memory from `100 MB` down to `min(M, N) * 4 bytes` = **20 Kilobytes**!
20 KB fits completely inside modern **L1 CPU data cache (32-48 KB)**, yielding massive cache locality and speedup."

**Interviewer:** "Brilliant memory-architecture deduction. Please write the implementation."

---

## 🧭 Chapter 2: 3-Tiered Hint Progression

```
+-----------------------------------------------------------------------------+
| TIER 1: GENTLE NUDGE (Contextual Awareness)                                 |
| "When comparing prefixes, what three physical actions can transform the     |
| last character? How do those three actions relate to earlier subproblems?"  |
+-----------------------------------------------------------------------------+
                                       |
                                       v
+-----------------------------------------------------------------------------+
| TIER 2: STRUCTURAL ANCHOR (Algorithmic Primitive)                           |
| "Define dp[i][j] as the edit distance between word1[:i] and word2[:j].      |
| If characters match, take diagonal. If they mismatch, take 1 + min of       |
| insert, delete, and replace. How far back in the matrix do you look?"       |
+-----------------------------------------------------------------------------+
                                       |
                                       v
+-----------------------------------------------------------------------------+
| TIER 3: TACTICAL CODE HINT (Implementation Mechanics)                        |
| "Allocate a 1D array dp of length len(word2) + 1. Store the diagonal in a   |
| scalar variable prev_diag before overwriting dp[j]. If word1[i-1] ==        |
| word2[j-1], new_val = prev_diag; else 1 + min(dp[j], dp[j-1], prev_diag)."  |
+-----------------------------------------------------------------------------+
```

---

## 📊 Chapter 3: Senior Evaluation Rubric

| Dimension | Unsatisfactory (Level 1) | Developing (Level 2) | Senior Bar (Level 3) | Staff/Principal Bar (Level 4) |
| :--- | :--- | :--- | :--- | :--- |
| **Problem Formulation** | Misses overlapping subproblems; writes uncontrolled exponential recursion. | Formulates 2D table after hints; struggles with base cases (0-length strings). | Immediately identifies 2D DP formulation; writes exact mathematical transitions aloud. | Explains DAG topological order; analyzes Levenshtein vs Wagner-Fischer vs Damerau edit distance. |
| **Boundary Questioning** | Does not consider empty strings, identical strings, or length asymmetry. | Checks bounds only when prompted by interviewer. | Tests empty word1, empty word2, and identical words before writing code. | Discusses memory alignment, Unicode grapheme clusters, and zero-allocation spans. |
| **Space/Time Trade-offs** | Stuck on `O(M * N)` space; cannot compress dimensions. | Compresses to 2 full rows `O(2 * N)` but cannot reduce to single 1D buffer. | Compresses to `O(min(M, N))` single 1D buffer; proves L1 cache residency benefits. | Analyzes memory bandwidth bottlenecks, SIMD vectorization potential, and diagonal waves. |
| **Code Cleanliness** | Diagonal variable corrupted during in-place updates; out-of-bounds index. | Working code but messy variable names and redundant nested conditions. | Idiomatic, clean variables (`prevDiag`, `temp`), explicit guard clauses. | Production-grade code with null validation, stack memory allocation, zero GC overhead. |

---

## 💻 Chapter 4: Idiomatic Dual-Language Code

### Problem 1: Edit Distance (Levenshtein Distance) Space-Optimized

#### C# Primary Implementation (.NET 8/9)

```csharp
using System;

namespace MockInterviews.DynamicProgramming;

public static class EditDistanceSolver
{
    /// <summary>
    /// Computes minimum edit distance in O(M * N) time and O(min(M, N)) space.
    /// Employs single 1D rolling buffer with scalar diagonal preservation.
    /// </summary>
    public static int MinDistance(ReadOnlySpan<char> word1, ReadOnlySpan<char> word2)
    {
        // Optimization: Ensure word2 is the shorter string to minimize array footprint
        if (word1.Length < word2.Length)
        {
            return MinDistance(word2, word1);
        }

        int m = word1.Length;
        int n = word2.Length;

        // Base case: if shorter string is empty, answer is length of longer string
        if (n == 0) return m;

        // Allocate rolling buffer for the shorter string
        Span<int> dp = stackalloc int[n + 1];

        // Initialize base case: dp[j] = j for transforming "" to word2[0..j-1]
        for (int j = 0; j <= n; j++)
        {
            dp[j] = j;
        }

        for (int i = 1; i <= m; i++)
        {
            int prevDiag = dp[0]; // Represents dp[i-1][j-1]
            dp[0] = i;            // Represents dp[i][0] (deleting i characters)

            for (int j = 1; j <= n; j++)
            {
                int temp = dp[j]; // Save dp[i-1][j] before overwriting

                if (word1[i - 1] == word2[j - 1])
                {
                    dp[j] = prevDiag;
                }
                else
                {
                    int insert = dp[j - 1]; // dp[i][j-1]
                    int delete = dp[j];     // dp[i-1][j]
                    int replace = prevDiag; // dp[i-1][j-1]

                    dp[j] = 1 + Math.Min(replace, Math.Min(insert, delete));
                }

                prevDiag = temp; // Advance diagonal for next column
            }
        }

        return dp[n];
    }
}
```

#### Python Secondary Implementation (Python 3.11+)

```python
def min_distance(word1: str, word2: str) -> int:
    """Computes minimum edit distance in O(M * N) time and O(min(M, N)) auxiliary space."""
    if len(word1) < len(word2):
        word1, word2 = word2, word1

    m, n = len(word1), len(word2)
    if n == 0:
        return m

    # 1D buffer of size n + 1
    dp = list(range(n + 1))

    for i in range(1, m + 1):
        prev_diag = dp[0]
        dp[0] = i

        for j in range(1, n + 1):
            temp = dp[j]

            if word1[i - 1] == word2[j - 1]:
                dp[j] = prev_diag
            else:
                dp[j] = 1 + min(
                    prev_diag,  # replace
                    dp[j - 1],  # insert
                    dp[j],      # delete
                )

            prev_diag = temp

    return dp[n]
```

---

### Problem 2: Jump Game II (Greedy BFS vs Quadratic DP)

#### C# Primary Implementation (.NET 8/9)

```csharp
using System;

namespace MockInterviews.DynamicProgramming;

public static class JumpGame
{
    /// <summary>
    /// Computes minimum jumps to reach the last index in O(N) time and O(1) space.
    /// Demonstrates Greedy Choice Property: expanding current window reach provably dominates sub-optimal choices.
    /// </summary>
    public static int Jump(ReadOnlySpan<int> nums)
    {
        if (nums.Length <= 1) return 0;

        int jumps = 0;
        int currentWindowEnd = 0;
        int furthestReach = 0;

        // Loop up to nums.Length - 1 because arriving at the last element completes the task
        for (int i = 0; i < nums.Length - 1; i++)
        {
            furthestReach = Math.Max(furthestReach, i + nums[i]);

            // When reaching the end of the current jump horizon, take another jump
            if (i == currentWindowEnd)
            {
                jumps++;
                currentWindowEnd = furthestReach;

                if (currentWindowEnd >= nums.Length - 1)
                {
                    break;
                }
            }
        }

        return jumps;
    }
}
```

#### Python Secondary Implementation (Python 3.11+)

```python
from typing import Sequence

def min_jumps(nums: Sequence[int]) -> int:
    """Greedy BFS interval expansion in O(N) time and O(1) space."""
    if len(nums) <= 1:
        return 0

    jumps = 0
    cur_end = 0
    furthest = 0

    for i in range(len(nums) - 1):
        furthest = max(furthest, i + nums[i])

        if i == cur_end:
            jumps += 1
            cur_end = furthest
            if cur_end >= len(nums) - 1:
                break

    return jumps
```

---

## 🔬 Chapter 5: Explicit Complexity Deconstruction

| Metric | Full 2D DP Table | Space-Optimized 1D DP | Greedy Jump Game II |
| :--- | :--- | :--- | :--- |
| **Time Complexity** | `O(M * N)` | `O(M * N)` | `O(N)` |
| **Auxiliary Memory** | `O(M * N)` (100 MB) | `O(min(M, N))` (~20 KB) | `O(1)` (3 scalar registers) |
| **L1 Cache Residency**| Cache thrashing (misses) | **100% Resident in L1 Cache** | Register-allocated |
| **Correctness Proof**| Induction on prefixes | Induction on prefixes | Greedy Exchange Argument |

---

## 🎙️ Chapter 6: 45-Minute Verbal Script & Step-by-Step Timeline

```
[00:00 - 05:00] Clarification & Baseline Recurrence
- Validate string alphabet, lengths up to 5,000, empty string outcomes.
- State recurrence: dp[i][j] = dp[i-1][j-1] if match, else 1 + min(insert, delete, replace).

[05:00 - 15:00] Memory Optimization & Hardware Justification
- Point out that standard 2D table requires 100 MB of heap memory.
- Prove that row i depends only on row i-1 and diagonal dp[i-1][j-1].
- Propose 1D rolling array with single diagonal variable: drops footprint to 20 KB.
- Emphasize: "20 KB fits inside 32 KB L1 CPU data cache, eliminating DRAM bus stalls."

[15:00 - 30:00] Live Defensive Implementation
- Swap strings so word2 is the shorter string.
- Allocate stack memory (stackalloc in C# / list in Python).
- Carefully preserve prevDiag before overwriting dp[j].

[30:00 - 40:00] Edge Cases & Verification
- Test empty string against non-empty string ("" vs "abc" -> 3).
- Test identical strings ("abc" vs "abc" -> 0).
- Test single character mismatch ("a" vs "b" -> 1).

[40:00 - 45:00] Follow-up Defense: Greedy vs DP
- Explain when DP is mandatory (Edit Distance: operations have equal weight and overlap)
  versus when Greedy works (Jump Game: any further reach strictly subsumes earlier reach).
```

---

## 🔍 Chapter 7: Edge-Case Verification

| Test Case | Scenario | Expected Outcome | Invariant Handling |
| :--- | :--- | :--- | :--- |
| **Both Strings Empty** | `w1 = "", w2 = ""` | `0` | Base condition `n == 0` returns `m = 0`. |
| **One String Empty** | `w1 = "horse", w2 = ""` | `5` | Loop returns length of non-empty string. |
| **Identical Strings** | `w1 = "algorithm", w2 = "algorithm"` | `0` | Match branch `dp[j] = prevDiag` executes on every step. |
| **Single Character Replacement**| `w1 = "a", w2 = "b"` | `1` | Base case `dp[0]=1, dp[1]=0`; replaces to `1 + 0 = 1`. |
| **Jump Game Single Element** | `nums = [0]` | `0` | Guard clause `nums.Length <= 1` returns `0` immediately. |

---

> 🧭 **Navigation:** [← Previous Day](Week_19_Day_02_Mock_Trees_Graphs_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_19_Day_04_Mock_Mixed_Complex_Rounds_Instructional.md)

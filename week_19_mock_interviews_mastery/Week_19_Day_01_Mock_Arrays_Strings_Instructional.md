# 📘 Week 19, Day 1: Mock Round 1: Arrays, Strings & Two-Pointers

> 🧭 **Navigation:** [← Week Overview](README.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_19_Day_02_Mock_Trees_Graphs_Instructional.md)
> 
> 💡 **Instructor Note:** *This is an authentic senior interview simulation. Practice speaking aloud using the transcripts, track your hint dependency level, and grade your performance against the senior rubric.*

---

## 🎯 Learning Objectives

*   **Interview Simulation:** Experience an authentic 45-minute live senior coding interview on core array and two-pointer paradigms.
*   **Dialogue Navigation:** Learn how to articulate brute-force trade-offs, identify structural bottlenecks, and steer the conversation toward optimal `O(1)` auxiliary memory.
*   **3-Tiered Hint Recovery:** Practice extracting hints systematically (Gentle Nudge -> Structural Anchor -> Tactical Code Hint) without losing evaluation points.
*   **Senior Rubric Mastery:** Evaluate solutions against top-tier industry criteria: Problem Formulation, Boundary Questioning, Space/Time Trade-offs, and Clean Code.

## ⚖️ FAANG Senior / Lead Interview Calibration

In Tier-1 coding rounds (Google, Meta, Amazon, Apple, Netflix), arrays, strings, and two-pointer problems test candidates on pointer discipline and allocation hygiene:

| Strategy | Time Complexity | Auxiliary Space | Garbage Collection Impact | Common Failure Mode | Senior Evaluation Bar |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Nested Brute Force** | `O(N^2)` | `O(1)` | None | Immediate TLE on `N = 10^5` | Reject. Cannot pass basic algorithmic filter. |
| **Prefix / Suffix Precomputation** | `O(N)` | `O(N)` | Heap allocation for two arrays | Memory bloat on massive streams | Developing / Mid-level bar (L4). |
| **Opposing Two-Pointers** | `O(N)` | `O(1)` | Zero heap allocations | Pointer termination off-by-one | **Senior Bar (L5): Inductive proof of shorter wall.** |
| **Direct-Address Sliding Window** | `O(N)` | `O(Sigma)` (stackalloc) | Zero heap allocations | Multi-byte UTF-8 character corruption | **Senior/Staff Bar (L5/L6): Cache-resident lookup table.** |

### The Interviewer's Hidden Rubric: What We Listen For
1. **Physical Intuition:** Does the candidate jump straight into guessing data structures, or do they derive the physical invariant (e.g., "water level is bounded by the shorter of the two extreme walls")?
2. **Allocation Aversion:** Does the candidate instinctively reach for `stackalloc` / in-place variables, or do they allocate unnecessary heap structures?
3. **Monotonic Progress:** Can the candidate prove that pointers strictly converge and never oscillate or deadlock?

---

## 📖 Chapter 1: Live Interview Simulation & Dialogue Transcript

### Problem Statement
> **Interviewer:** *"Given an elevation map represented by an array of non-negative integers `height` where each bar has width 1, compute how much water it can trap after raining."*

```
Visual Elevation Profile:
     #
 #   # #   #
_#_#_###_#_#_
 0 1 2 3 4 5  (Indices)
```

---

### Authentic Interviewer Dialogue Transcript

**Candidate:** "Thanks for the problem. Before jumping into code, I'd like to clarify a few operational constraints:
1. What is the maximum length of `height`? Are we looking at `10^5` elements?
2. Can bar heights be negative?
3. What is the expected behavior if `height.Length < 3`?"

**Interviewer:** "Good questions. `N` is up to `10^5`. Elevations are non-negative integers up to `10^4`. If `height.Length < 3`, it cannot form a container to hold water, so return 0."

**Candidate:** "Understood. Let's analyze the physical invariant governing trapped water:
At any index `i`, water can only pool if there are taller bars to both its left and its right. Specifically, the water level above bar `i` is determined by:
`water[i] = max(0, min(max_left[i], max_right[i]) - height[i])`.
If I compute `max_left` and `max_right` naively for every bar, scanning left and right takes `O(N)` per element, yielding an `O(N^2)` brute-force solution with `O(1)` memory. For `N = 10^5`, `10^10` operations will time out."

**Interviewer:** "Agreed. How would you optimize the repeated scans?"

**Candidate:** "We could precompute two prefix/suffix arrays:
- `left_max[i]` storing the maximum height from `0` to `i`.
- `right_max[i]` storing the maximum height from `i` to `N - 1`.
Then in a single pass over `i`, we calculate `min(left_max[i], right_max[i]) - height[i]`.
This achieves `O(N)` time complexity, but requires `2 * N` integers of auxiliary heap memory: `O(N)` space."

**Interviewer:** "That is a solid improvement. Can we achieve the same `O(N)` runtime while dropping auxiliary space to `O(1)`?"

**Candidate:** "Yes. Notice that we don't need the exact values of both `left_max` and `right_max`; we only need to know **which one is strictly smaller**!
If we place two opposing pointers `left = 0` and `right = N - 1`, and maintain running variables `left_max` and `right_max`:
- If `height[left] < height[right]`: we know for certain that the water level at `left` is strictly bounded by `left_max`, because `height[right]` guarantees there is a barrier to the right at least as tall as `height[left]`. Thus, we can resolve bar `left` immediately and advance `left++`.
- Conversely, if `height[right] <= height[left]`, bar `right` is strictly bounded by `right_max`, so we resolve `right` and decrement `right--`.
This eliminates both auxiliary arrays, solving the problem in `O(N)` time and `O(1)` space."

**Interviewer:** "Excellent mathematical deduction. Please implement the solution."

---

## 🧭 Chapter 2: 3-Tiered Hint Progression

When practicing or assessing candidates, never give away the complete algorithm immediately. Use this 3-tiered hint progression:

```
+-----------------------------------------------------------------------------+
| TIER 1: GENTLE NUDGE (Contextual Awareness)                                 |
| "What physically determines how much water can pool on top of any single    |
| bar? Does water overflow if one of the surrounding walls is lower?"          |
+-----------------------------------------------------------------------------+
                                       |
                                       v
+-----------------------------------------------------------------------------+
| TIER 2: STRUCTURAL ANCHOR (Algorithmic Primitive)                           |
| "Notice that water height at index i is min(max_left, max_right) - height[i].|
| Do you actually need the exact value of the taller wall, or only the fact   |
| that a taller wall exists somewhere on the opposite side?"                  |
+-----------------------------------------------------------------------------+
                                       |
                                       v
+-----------------------------------------------------------------------------+
| TIER 3: TACTICAL CODE HINT (Implementation Mechanics)                        |
| "Maintain left = 0, right = N - 1, and running left_max and right_max.      |
| While left < right, compare height[left] to height[right]. If height[left]  |
| is smaller, update left_max and accumulate left_max - height[left], then    |
| advance left++. Otherwise, do the same for right."                          |
+-----------------------------------------------------------------------------+
```

---

## 📊 Chapter 3: Senior Evaluation Rubric

| Dimension | Unsatisfactory (Level 1) | Developing (Level 2) | Senior Bar (Level 3) | Staff/Principal Bar (Level 4) |
| :--- | :--- | :--- | :--- | :--- |
| **Problem Formulation** | Jumps immediately to random code; misses water pooling condition. | Identifies `min(L, R) - H` after hints; proposes brute force only. | Formulates prefix/suffix concept instantly; independently derives two-pointer invariant. | Formulates physical invariant proactively; discusses 2D generalization or streaming inputs. |
| **Boundary Questioning** | Does not ask about input length, negative elevations, or null arrays. | Asks generic questions ("What are the constraints?") without specifics. | Asks targeted questions on `N < 3`, plateaus, memory limits, and data types. | Quantifies memory cache bounds and overflow limits (`10^5 * 10^4 = 10^9` fits 32-bit int). |
| **Space/Time Trade-offs** | Implements `O(N^2)` and cannot optimize. | Implements `O(N)` time with `O(N)` space; struggles to achieve `O(1)` space. | Defends `O(N)` time and `O(1)` space trade-off with clear convergence proof. | Analyzes CPU branch predictor behavior and L1 cache locality for sequential sweeps. |
| **Code Cleanliness** | Off-by-one errors; unhandled nulls; spaghetti conditional branches. | Working code but redundant checks, messy variable names, multiple returns. | Production-grade code: guard clauses, clear variable naming, zero unnecessary branching. | Flawless idiomatic code (.NET modern idioms, Pythonic typing), zero allocation, contract safety. |

---

## 💻 Chapter 4: Idiomatic Dual-Language Code

### Problem 1: Trapping Rain Water (LeetCode 42)

#### C# Primary Implementation (.NET 8/9)

```csharp
using System;

namespace MockInterviews.ArraysStrings;

public static class RainWaterTrapping
{
    /// <summary>
    /// Calculates trapped rainwater in O(N) time and O(1) auxiliary memory.
    /// Invariant: Water above bar i is governed strictly by min(maxLeft, maxRight) - height[i].
    /// Pointers left and right converge inward; the strictly smaller barrier resolves immediately.
    /// </summary>
    public static int Trap(ReadOnlySpan<int> height)
    {
        // Guard clause: minimum 3 bars required to form a basin
        if (height.Length < 3) return 0;

        int left = 0;
        int right = height.Length - 1;
        int leftMax = 0;
        int rightMax = 0;
        int totalTrapped = 0;

        while (left < right)
        {
            if (height[left] < height[right])
            {
                if (height[left] >= leftMax)
                {
                    leftMax = height[left];
                }
                else
                {
                    totalTrapped += leftMax - height[left];
                }
                left++;
            }
            else
            {
                if (height[right] >= rightMax)
                {
                    rightMax = height[right];
                }
                else
                {
                    totalTrapped += rightMax - height[right];
                }
                right--;
            }
        }

        return totalTrapped;
    }
}
```

#### Python Secondary Implementation (Python 3.11+)

```python
from typing import Sequence

def trap_rain_water(height: Sequence[int]) -> int:
    """Calculates trapped rainwater in O(N) time and O(1) space using opposing pointers."""
    if len(height) < 3:
        return 0

    left, right = 0, len(height) - 1
    left_max, right_max = 0, 0
    total_water = 0

    while left < right:
        if height[left] < height[right]:
            if height[left] >= left_max:
                left_max = height[left]
            else:
                total_water += left_max - height[left]
            left += 1
        else:
            if height[right] >= right_max:
                right_max = height[right]
            else:
                total_water += right_max - height[right]
            right -= 1

    return total_water
```

---

### Problem 2: Longest Substring Without Repeating Characters (LeetCode 3)

#### C# Primary Implementation (.NET 8/9)

```csharp
using System;

namespace MockInterviews.ArraysStrings;

public static class SubstringProblems
{
    /// <summary>
    /// Finds length of longest substring without repeating characters in O(N) time and O(1) space.
    /// Uses direct-address ASCII table for zero-allocation sliding window.
    /// </summary>
    public static int LengthOfLongestSubstring(ReadOnlySpan<char> s)
    {
        if (s.IsEmpty) return 0;

        // Direct-address table for ASCII characters tracking 1-based last seen positions
        Span<int> lastSeen = stackalloc int[128];
        int maxLength = 0;
        int windowStart = 0;

        for (int windowEnd = 0; windowEnd < s.Length; windowEnd++)
        {
            char c = s[windowEnd];
            if (c < 128)
            {
                // If character was seen inside the current window, contract window
                windowStart = Math.Max(windowStart, lastSeen[c]);
                lastSeen[c] = windowEnd + 1;
            }

            maxLength = Math.Max(maxLength, windowEnd - windowStart + 1);
        }

        return maxLength;
    }
}
```

#### Python Secondary Implementation (Python 3.11+)

```python
def length_of_longest_substring(s: str) -> int:
    """Sliding window tracking last seen character index in O(N) time and O(min(N, Sigma)) space."""
    last_seen: dict[str, int] = {}
    max_len = 0
    start = 0

    for end, ch in enumerate(s):
        if ch in last_seen and last_seen[ch] >= start:
            start = last_seen[ch] + 1

        last_seen[ch] = end
        max_len = max(max_len, end - start + 1)

    return max_len
```

---

## 🔬 Chapter 5: Explicit Complexity Deconstruction

| Metric | Trapping Rain Water | Longest Substring | Engineering Grounding |
| :--- | :--- | :--- | :--- |
| **Time Complexity** | `O(N)` | `O(N)` | Each pointer moves strictly inward/forward. No element is scanned more than once. |
| **Auxiliary Heap Space** | `O(1)` | `O(1)` (ASCII stackalloc) | Zero heap allocations during hot-path execution; GC is never triggered. |
| **Worst-Case Operations** | `N` iterations | `N` iterations | For `N = 100,000`, finishes in `< 1ms`. |
| **Branch Predictability** | High | High | Contiguous scans allow CPU branch target buffers to achieve `> 95%` branch prediction accuracy. |

---

## 🎙️ Chapter 6: 45-Minute Verbal Script & Step-by-Step Timeline

```
[00:00 - 05:00] Clarification & Contract Formulation
- Restate problem concisely.
- Ask clarifying questions: array length constraints, negative numbers, empty input behavior.
- Define function signature with defensive contracts (ReadOnlySpan in C#, Sequence in Python).

[05:00 - 15:00] Solution Exploration & Invariant Proof
- Mention brute-force O(N^2) baseline: computing min(maxLeft, maxRight) per bar.
- Identify the redundant computation: scanning left and right repeatedly.
- Introduce prefix/suffix arrays: O(N) time, O(N) space.
- Derive the two-pointer optimization aloud:
  "Because water height is constrained by the shorter wall, we only need to advance the pointer
   with the smaller height. This drops space from O(N) to O(1)."

[15:00 - 30:00] Live Defensive Implementation
- Write clean guard clause: if (height.Length < 3) return 0.
- Initialize left = 0, right = N - 1, leftMax = 0, rightMax = 0, total = 0.
- Write while (left < right) loop with clean monotonic advancement.

[30:00 - 40:00] Edge Cases & Walkthrough
- Step through dry run with standard example [0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1].
- Step through edge cases: [1, 2, 3] (strictly ascending -> 0 water), [3, 2, 1] (descending -> 0 water),
  and [2, 0, 2] (traps 2 units).

[40:00 - 45:00] Wrap-up & Production Scaling
"In a real distributed system handling streaming elevation data or time-series volatility,
we could maintain running max bounds across partitioned streaming windows."
```

---

## 🔍 Chapter 7: Edge-Case Verification

| Test Case | Scenario | Expected Outcome | Invariant Handling |
| :--- | :--- | :--- | :--- |
| **Empty or Small Array** | `height = [2, 1]` | `0` | Guard clause returns `0` before loop executes. |
| **Monotonically Increasing**| `height = [1, 2, 3, 4, 5]` | `0` | `leftMax` updates continuously; no lower basin exists. |
| **Monotonically Decreasing**| `height = [5, 4, 3, 2, 1]` | `0` | `rightMax` updates continuously; no lower basin exists. |
| **Flat Plateau** | `height = [3, 3, 3, 3]` | `0` | Bars equal boundary height; `leftMax - height[left] = 0`. |
| **Single Deep Valley** | `height = [4, 0, 4]` | `4` | Traps `min(4, 4) - 0 = 4` units accurately. |
| **Maximum Volume Overflow** | `N = 10^5`, all heights `10^4` | `0` | Trapped sum fits inside standard 32-bit signed integer (`max sum < 10^9`). |

---

> 🧭 **Navigation:** [← Week Overview](README.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_19_Day_02_Mock_Trees_Graphs_Instructional.md)

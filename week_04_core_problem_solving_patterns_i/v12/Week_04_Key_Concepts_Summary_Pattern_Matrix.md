# 📊 Week 04: Key Concepts Summary & Pattern Comparison Matrix

**Week:** 4 | **Purpose:** Quick reference for all patterns  
**Format:** Comparison tables, decision trees, visual references  
**Use:** Revision, pattern recognition, quick lookup

---

## 🧩 THE FIVE PATTERNS AT A GLANCE

### Pattern 1: TWO-POINTER

**Core Idea:** Synchronize two pointers; move based on contract/invariant

```
Same-Direction:          Opposite-Direction:
left → → → → right       left ← ← ← ← right
 ↓                            ↓
Points move right together    Points move toward each other
Both advance when needed      Each advance based on value/condition
```

| Aspect | Details |
|--------|---------|
| **When to Use** | Merging, deduplication, pairs, optimization on sorted arrays |
| **Time** | O(n) — each element visited ≤ 2 times |
| **Space** | O(1) — only pointers, no extra data structures |
| **Key Insight** | Invariant maintenance: guarantee what stays true at each step |
| **Red Flags** | "Two arrays", "merge", "remove in-place", "pair with sum X" |
| **Interview Difficulty** | 🟢 Easy to 🟡 Medium |

---

### Pattern 2: SLIDING WINDOW (FIXED SIZE)

**Core Idea:** Maintain a window of fixed size k; slide it across array; update incrementally

```
Array: [1, 3, -1, -3, 5, 3, 6, 7]
       [1, 3, -1]              ← window of size 3
          [3, -1, -3]
             [-1, -3, 5]
                ... slides to the right
```

| Aspect | Details |
|--------|---------|
| **When to Use** | Running averages, max/min in subarrays, bounded windows |
| **Time** | O(n) amortized — incremental update per slide |
| **Space** | O(k) — deque for max/min, counter for sums |
| **Key Insight** | Incremental updates (add right, remove left) beat recalculation |
| **Data Structures** | Deque (monotonic for max/min), array (sum), heap (priority) |
| **Red Flags** | "Sliding window", "k-length subarray", "running average" |
| **Interview Difficulty** | 🟡 Medium |

---

### Pattern 3: SLIDING WINDOW (VARIABLE SIZE)

**Core Idea:** Expand window to include elements; shrink when constraint violated; find optimal

```
"At most 2 distinct" constraint:
[a b c a b c b b]
[a b c]  ← 3 distinct, violates
[b c]    ← shrink left
[b c a]  ← ok again
```

| Aspect | Details |
|--------|---------|
| **When to Use** | At-most-K distinct, minimum window with condition, constraint-based |
| **Time** | O(n) amortized — each element enters and exits once |
| **Space** | O(k) — frequency map (k = constraint limit) |
| **Key Insight** | Despite varying window, amortized O(1) per iteration |
| **Decision Logic** | Expand right → check constraint → shrink left if violated |
| **Red Flags** | "At most K", "maximum length", "minimum window", "constraint" |
| **Interview Difficulty** | 🟡 Medium to 🔴 Hard |

---

### Pattern 4: DIVIDE & CONQUER

**Core Idea:** Recursively split problem, solve subproblems, combine results

```
          [8, 3, 5, 4, 2]
              /        \
         [8, 3]      [5, 4, 2]
         /    \      /    \
      [8]  [3]    [5]  [4,2]
            \      /      \
           [3, 8]         [2, 4, 5]
               \          /
            [2, 3, 4, 5, 8]
```

| Aspect | Details |
|--------|---------|
| **When to Use** | Sorting, inversions, optimal substructure problems |
| **Time** | Depends on recurrence: T(n) = a·T(n/b) + O(n^d) |
| **Space** | O(log n) to O(n) depending on algorithm |
| **Key Insight** | Optimal substructure: combine subproblem solutions |
| **Master Theorem** | If a > b^d: O(n^log_b(a)), if a = b^d: O(n^d log n), if a < b^d: O(n^d) |
| **Red Flags** | "Divide problem", "optimal substructure", "merge", "sort" |
| **Interview Difficulty** | 🟡 Medium to 🔴 Hard |

---

### Pattern 5: BINARY SEARCH (ANSWER SPACE)

**Core Idea:** Search on answer space (not array); binary search for optimal feasible answer

```
Answer Range: [1, 100]
                   ↓
              Can achieve 50? YES → search higher
                      ↓
              Can achieve 75? NO → search lower
                      ↓
          Binary search converges on boundary
```

| Aspect | Details |
|--------|---------|
| **When to Use** | Minimize/maximize with constraints, feasibility-based optimization |
| **Time** | O(log(answer_range) × feasibility_check_cost) |
| **Space** | O(1) for pointers, O(m) for feasibility state |
| **Key Insight** | Monotonicity: if X works, all "better" values work too |
| **Feasibility Check** | Custom function deciding if a candidate answer is valid |
| **Red Flags** | "Find minimum such that", "maximize subject to", "feasibility" |
| **Interview Difficulty** | 🟡 Medium to 🔴 Hard |

---

## 📊 COMPREHENSIVE COMPARISON MATRIX

| Criterion | Two-Pointer | Fixed Window | Variable Window | Divide & Conquer | Binary Search |
|-----------|------------|--------------|-----------------|-----------------|---------------|
| **Time Complexity** | O(n) | O(n) amort. | O(n) amort. | O(n log n) [merge] | O(log n × check) |
| **Space Complexity** | O(1) | O(k) | O(k) | O(log n) [recursion] | O(1) or O(m) |
| **When Applicable** | Sorted/pairs | Fixed k | Dynamic constraint | Sorting/substructure | Optimization |
| **Key Data Structure** | None | Deque/array | Hash map | Recursion | Custom logic |
| **Invariant Type** | Pointer contract | Window bounds | Constraint/state | Subproblem correctness | Monotonic feasibility |
| **Typical Problems** | 15-20% of interviews | 15-20% of interviews | 20-25% of interviews | 15-20% of interviews | 15-20% of interviews |
| **Difficulty to Learn** | 🟢 Easy | 🟡 Medium | 🟡-🔴 Hard | 🟡 Medium | 🟡-🔴 Hard |
| **Difficulty to Apply** | 🟢 Easy | 🟡 Medium | 🟡-🔴 Hard | 🟡-🔴 Hard | 🟡-🔴 Hard |
| **Off-By-One Errors** | Common | Rare | Common | Rare | Common |
| **Debugging Tips** | Trace pointers | Check window bounds | Verify state machine | Check recurrence | Verify monotonicity |

---

## 🎯 DECISION TREE: WHICH PATTERN TO USE?

```
START: Given a problem

    ↓ Does it involve searching a sorted array?
    YES → Binary search basics (Week 2), not Week 4 patterns
    NO  → Continue

    ↓ Does it involve two items/pointers/regions?
    YES → Might be Two-Pointer
          ├─ Same-direction? (both moving right)
          │  └─ Merging, deduplication → Two-Pointer
          └─ Opposite-direction? (moving toward each other)
             └─ Pairs, optimization → Two-Pointer
    NO  → Continue

    ↓ Does it involve a window of elements?
    YES → Sliding Window
          ├─ Is window size fixed/predetermined?
          │  └─ Yes → Fixed-Size Sliding Window (Day 2)
          └─ Is window size dynamic/constraint-based?
             └─ Yes → Variable-Size Sliding Window (Day 3)
    NO  → Continue

    ↓ Can the problem be split into independent subproblems?
    YES → Divide & Conquer
          └─ Does it have optimal substructure?
             └─ Yes → Divide & Conquer (Day 4)
             └─ No (overlapping subproblems) → Dynamic Programming (Week 14)
    NO  → Continue

    ↓ Is the goal to optimize (minimize/maximize) something?
    YES → Binary Search on Answer Space
          └─ Can you check if a candidate answer is feasible?
             └─ Yes → Binary Search (Day 5)
             └─ No → Might need different approach
    NO  → Might not be a Week 4 pattern

    ↓ Default: Unknown pattern
    RECOMMENDATION: Solve with simpler approach; review patterns afterward
```

---

## 🔀 PATTERN COMBINATIONS

**Common combinations in hard problems:**

| Combination | Example | Approach |
|------------|---------|----------|
| **Two-Pointer + Sorting** | Remove duplicates in sorted array | Sort first, then two-pointer |
| **Sliding Window + Hash Map** | Longest substring without repeating | Window + frequency map |
| **Divide & Conquer + Recursion** | Merge sort, counting inversions | Inherent in pattern |
| **Binary Search + Greedy** | Machine scheduling | Binary search + greedy feasibility check |
| **Variable Window + Dynamic Programming** | Complex constraint satisfaction | Window for outer loop, DP for feasibility |

---

## 🚨 ANTI-PATTERNS: WHEN THESE PATTERNS FAIL

| Pattern | Fails When | Example | Alternative |
|---------|-----------|---------|-------------|
| **Two-Pointer** | Array is unsorted OR multiple pointers needed | Unordered pairs with sum X | Hash map |
| **Fixed Window** | Window size is dynamic OR data is streaming | Variable-size requirement | Variable window |
| **Variable Window** | Constraint not easily checkable | Complex multi-constraint | Brute force, DP |
| **Divide & Conquer** | Subproblems overlap significantly | Same subproblem solved many times | Dynamic programming |
| **Binary Search** | Feasibility not monotonic | Neither YES nor NO region is contiguous | Iteration or DP |

---

## 📈 COMPLEXITY QUICK REFERENCE

```
Time Complexity Comparison for n=1,000,000:

Naive O(n²):         10^12 operations   → 1,000+ seconds
Naive O(n log n):    2×10^7 operations  → 0.02 seconds
Two-Pointer O(n):    10^6 operations    → 0.001 seconds
Divide & Conquer O(n log n): 2×10^7    → 0.02 seconds
Binary Search O(log n): 20 operations   → <0.001 seconds (just search)
  + check O(n): 20 × 10^6              → 0.02 seconds
```

**Key Takeaway:** Week 4 patterns enable O(n) or O(n log n) solutions where naive approaches require O(n²).

---

## 🏆 PATTERN MASTERY CHECKLIST

**For Each Pattern, Ask Yourself:**

### Two-Pointer
- ☐ Can I identify same-direction vs. opposite-direction scenarios?
- ☐ Do I understand the invariant being maintained?
- ☐ Can I trace through examples without looking at solutions?
- ☐ Do I know when to advance each pointer?

### Sliding Window (Fixed)
- ☐ Can I identify when a window size is meaningful?
- ☐ Do I understand incremental update vs. recalculation?
- ☐ Can I implement monotonic deque correctly?
- ☐ Do I understand amortized analysis?

### Sliding Window (Variable)
- ☐ Can I identify when constraints require dynamic windows?
- ☐ Do I understand when to expand vs. shrink?
- ☐ Can I maintain frequency maps correctly?
- ☐ Do I verify monotonicity of feasibility?

### Divide & Conquer
- ☐ Can I identify optimal substructure in a problem?
- ☐ Do I understand recurrence relations?
- ☐ Can I apply the Master Theorem correctly?
- ☐ Do I know merge sort deeply (not just mechanics)?

### Binary Search (Answer)
- ☐ Can I identify optimization problems suitable for binary search?
- ☐ Do I understand monotonic feasibility requirements?
- ☐ Can I write correct feasibility checkers?
- ☐ Do I set correct bounds for binary search?

---

## 📚 QUICK PROBLEM REFERENCE

**Which pattern for these common problems?**

| Problem | Pattern | Complexity |
|---------|---------|-----------|
| Merge sorted arrays | Two-Pointer | O(n) |
| Remove duplicates | Two-Pointer | O(n) |
| Container with most water | Two-Pointer (opposite) | O(n) |
| Moving average | Fixed Window | O(n) |
| Max in sliding window | Fixed Window + Deque | O(n) |
| Longest substring no repeating | Variable Window | O(n) |
| At most K distinct characters | Variable Window | O(n) |
| Minimum window substring | Variable Window | O(n) |
| Merge sort | Divide & Conquer | O(n log n) |
| Counting inversions | Divide & Conquer | O(n log n) |
| Machine scheduling | Binary Search | O(log n × n) |
| Aggressive cows | Binary Search | O(log n × n) |

---

**Use this as a reference sheet throughout Week 04 and beyond.**


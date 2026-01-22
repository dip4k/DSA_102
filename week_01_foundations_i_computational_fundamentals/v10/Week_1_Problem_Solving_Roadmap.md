# 🧭 WEEK 1 — PROBLEM-SOLVING ROADMAP

**Purpose:** Strategic progression from simple to complex

---

## 🎯 Overall Strategy

Week 1 problems focus on:
1. **Understanding complexity** (not solving complex problems).
2. **Tracing execution** manually.
3. **Building recursion intuition**.

**Not Yet:** Advanced algorithms, data structures, optimizations.

---

## 🪜 Progression: Simple → Complex

### 🟢 Stage 1: Direct Applications (Days 1-3)
**Goal:** Apply definitions directly.

**Problem Types:**
- Calculate Big-O of given code
- Identify space complexity (stack vs heap)
- Trace simple loops/recursion

**Example Problems:**
- "What is the time complexity of this nested loop?"
- "Does this function use O(1) or O(n) auxiliary space?"

---

### 🟡 Stage 2: Recursion Basics (Day 4)
**Goal:** Write and trace basic recursive functions.

**Problem Types:**
- Factorial, Fibonacci, Sum of array
- Reverse string, Power(x, n)
- Binary tree basics (just traversal)

**Example Problems:**
- LeetCode 509 (Fibonacci)
- LeetCode 344 (Reverse String)
- LeetCode 21 (Merge Two Sorted Lists)

---

### 🟠 Stage 3: Memoization (Day 5)
**Goal:** Optimize exponential recursion.

**Problem Types:**
- Climbing Stairs (Count ways)
- Longest Common Subsequence
- Grid Traveler

**Example Problems:**
- LeetCode 70 (Climbing Stairs)
- LeetCode 198 (House Robber)
- LeetCode 1143 (LCS)

---

## ⚠ Common Problem-Solving Pitfalls

### Pitfall 1: Jumping to Code Too Fast
- **Fix:** Write logic in plain English first. Draw diagram.

### Pitfall 2: Not Identifying Base Cases
- **Fix:** Ask: "What is the simplest input?" That's your base case.

### Pitfall 3: Forgetting to Check the Cache (Memoization)
- **Fix:** Always structure as: Check cache → Compute → Store → Return.

### Pitfall 4: Confusing Tree Recursion with Depth
- **Fix:** Depth = Space complexity. Branches = Time complexity.

---

## 📌 Pattern Templates

### Template 1: Basic Recursion
```
Function(input):
    if (base case): return simple answer
    do some work
    result = Function(smaller input)
    combine result with work done
    return result
```

### Template 2: Memoization
```
Function(input, cache):
    if (base case): return simple answer
    if (input in cache): return cache[input]
    result = compute using Function(smaller inputs)
    cache[input] = result
    return result
```

---

*End of Week 1 Roadmap*
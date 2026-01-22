# 📅 WEEK 5 DAILY PROGRESS CHECKLIST

**Week 5: Day-by-Day Learning Tracker**

---

## 🎯 How to Use This Checklist

Check off items as you complete them. This tracker ensures you build understanding systematically, not just memorize solutions.

**Daily Commitment:** 2.5-3 hours per day

---

## 📋 DAY 1: HASH MAP & HASH SET PATTERNS

### ✅ Morning Session (45 min)
- [ ] Read Section 1-2 of `Week_05_Day_1_Hash_Map_And_Hash_Set_Patterns_Instructional.md`
- [ ] Understand the analogy: "Hash as a complement tracker"
- [ ] Draw a diagram showing how hash resolves "Two Sum"
- [ ] Identify the two core operations: Insertion and Lookup

### ✅ Core Mechanics (40 min)
- [ ] Trace Example 1 (Two Sum) with `[2, 7, 11, 15]`, target = 9
- [ ] Trace Example 2 (Anagrams) with `"listen"` vs `"silent"`
- [ ] Trace one edge case (empty array, single element, duplicates)
- [ ] Answer: What happens if two values are identical?

### ✅ Practice Problems (40 min)
- [ ] Problem 1: Two Sum (Easy) — Solve & explain your approach
- [ ] Problem 2: Valid Anagram (Easy) — Trace on paper first
- [ ] Problem 3: Group Anagrams (Medium) — Recognize the pattern

### ✅ Reflection (10 min)
- [ ] Write 2-3 sentences: "Hash maps solve [problem] by [mechanism]"
- [ ] List 3 new problems you think hash maps can solve
- [ ] Question: Can you use hash for "three sum"?

---

## 📋 DAY 2: MONOTONIC STACK PATTERNS

### ✅ Morning Session (45 min)
- [ ] Read Section 1-2 of `Week_05_Day_2_Monotonic_Stack_Patterns_Instructional.md`
- [ ] Understand the analogy: "Movie Theater" or "Taller Person"
- [ ] Draw a decreasing stack and explain "Why pop when new is larger?"
- [ ] Identify the difference between increasing and decreasing stacks

### ✅ Core Mechanics (50 min)
- [ ] Trace Example 1 (Next Greater Element) with `[2, 1, 5, 6, 2, 3]`
- [ ] **KEY:** Create your own trace table (not looking at the answer table)
- [ ] Trace Example 2 (Daily Temperatures) with `[73, 74, 75, 71, 69]`
- [ ] Trace one edge case (all increasing, all decreasing, single element)

### ✅ Practice Problems (45 min)
- [ ] Problem 1: Next Greater Element I (Easy) — Just trace, no coding
- [ ] Problem 2: Daily Temperatures (Medium) — Trace your solution first
- [ ] Problem 3 (Optional): Largest Rectangle in Histogram (Hard) — Understand logic

### ✅ Reflection (10 min)
- [ ] Write down the invariant: "Stack always contains..."
- [ ] Explain: Why does this guarantee O(N) and not O(N²)?
- [ ] Challenge: Can you do "Next Smaller Element" with the same idea?

### ✅ Integration with Day 1
- [ ] Question: How would you combine Monotonic Stack + Hash? (e.g., frequency of next greater)

---

## 📋 DAY 3: MERGE OPERATIONS & INTERVAL PATTERNS

### ✅ Morning Session (45 min)
- [ ] Read Section 1-2 of `Week_05_Day_3_Merge_Operations_And_Interval_Patterns_Instructional.md`
- [ ] Understand the analogy: "Painting a Road" or "Zipper"
- [ ] Draw the "6 Types of Overlap" diagram on paper
- [ ] Identify: Why does sorting by start time matter?

### ✅ Core Mechanics (50 min)
- [ ] Trace Example 1 (Merge Intervals) with `[[1,3], [2,6], [8,10], [15,18]]`
- [ ] Create your own trace table (state progression)
- [ ] Trace Example 2 (Insert Interval) with new `[5, 7]`
- [ ] Trace one edge case (touching intervals, contained, identical)

### ✅ Practice Problems (40 min)
- [ ] Problem 1: Merge Sorted Array (Easy) — Two-pointer merge, no sorting
- [ ] Problem 2: Merge Intervals (Medium) — Sort then sweep
- [ ] Problem 3: Insert Interval (Medium) — No re-sorting needed

### ✅ Reflection (10 min)
- [ ] Compare: "Merge Sorted Arrays" vs. "Merge Intervals". What's different?
- [ ] Question: Could you solve "Merge Intervals" WITHOUT sorting?
- [ ] Integration: How does this relate to Day 1 (Hash) or Day 2 (Stack)?

---

## 📋 DAY 4A: PARTITION & CYCLIC SORT

### ✅ Morning Session (50 min)
- [ ] Read Section 1-2 of `Week_05_Day_4_Part_A_Partition_And_Cyclic_Sort_Instructional.md`
- [ ] Understand the analogy: "Traffic Cop" (DNF) or "Correct Seat" (Cyclic)
- [ ] Draw the three regions for Dutch National Flag
- [ ] Identify: Why do we NOT increment `mid` when swapping with `high`?

### ✅ Core Mechanics (55 min)
- [ ] Trace Example 1 (Dutch National Flag) with `[2, 0, 2, 1, 1, 0]`
- [ ] Create your own trace table (show all three pointers: Low, Mid, High)
- [ ] Trace Example 2 (Cyclic Sort / Find Duplicate) with `[3, 1, 3, 4, 2]`
- [ ] Trace one edge case (all same color, already sorted, reverse sorted)

### ✅ Practice Problems (40 min)
- [ ] Problem 1: Sort Colors (Medium) — Dutch National Flag
- [ ] Problem 2: Missing Number (Easy) — Cyclic Sort concept
- [ ] Problem 3: Find Duplicate (Medium) — Array as linked list

### ✅ Reflection (10 min)
- [ ] Write the invariant: "Region [Low, Mid-1] always contains..."
- [ ] Explain: Why is Cyclic Sort O(N) and not O(N log N)?
- [ ] Question: Can Cyclic Sort handle negative numbers?

### ✅ Integration
- [ ] Challenge: How would you use Partition + Hash to solve a more complex problem?

---

## 📋 DAY 4B: KADANE'S ALGORITHM

### ✅ Morning Session (45 min)
- [ ] Read Section 1-2 of `Week_05_Day_4_Part_B_Kadanes_Algorithm_Instructional.md`
- [ ] Understand the analogy: "Heavy Backpack" (drop it when negative)
- [ ] Draw a bar graph showing running sum and reset points
- [ ] Identify: When exactly do we reset?

### ✅ Core Mechanics (50 min)
- [ ] Trace Example 1 (Max Subarray) with `[-2, 1, -3, 4, -1, 2, 1, -5, 4]`
- [ ] Create your own trace table (current_sum and max_sum columns)
- [ ] Trace Example 2 (Max Product Subarray) with `[2, 3, -2, 4]`
- [ ] Trace one edge case (all negative, zeros, single element)

### ✅ Practice Problems (40 min)
- [ ] Problem 1: Maximum Subarray (Easy) — Classic Kadane
- [ ] Problem 2: Best Time to Buy/Sell Stock (Easy) — Variation of Kadane
- [ ] Problem 3: Maximum Product Subarray (Medium) — Track min & max

### ✅ Reflection (10 min)
- [ ] Write the insight: "Why is resetting negative prefixes greedy YET optimal?"
- [ ] Explain: Can Kadane handle zeros? Duplicates?
- [ ] Question: What if the problem asks for the subarray INDICES, not sum?

### ✅ Integration
- [ ] Challenge: Combine Kadane + Partition or Kadane + Hash for a hybrid problem

---

## 📋 DAY 5: FAST & SLOW POINTERS

### ✅ Morning Session (45 min)
- [ ] Read Section 1-2 of `Week_05_Day_5_Fast_And_Slow_Pointers_Instructional.md`
- [ ] Understand the analogy: "Tortoise & Hare" on a racetrack
- [ ] Draw the meeting scenario (linear list vs. cyclic list)
- [ ] Identify: Why do different speeds guarantee collision?

### ✅ Core Mechanics (55 min)
- [ ] Trace Example 1 (Detect Cycle) with `1 -> 2 -> 3 -> 4 -> 5 -> 3`
- [ ] Create your own trace table (slow, fast, iteration)
- [ ] Trace Example 2 (Find Cycle Start) — Phase 1 + Phase 2
- [ ] Trace Example 3 (Midpoint) with `[1, 2, 3, 4, 5]`

### ✅ Practice Problems (40 min)
- [ ] Problem 1: Linked List Cycle (Easy) — Just detect, no locating
- [ ] Problem 2: Middle of Linked List (Easy) — Midpoint using fast/slow
- [ ] Problem 3: Linked List Cycle II (Medium) — Find cycle start

### ✅ Reflection (10 min)
- [ ] Write the insight: "Why does a second pass find the cycle START?"
- [ ] Explain: Is the 2:1 speed ratio mandatory, or can we use 3:1?
- [ ] Question: Can you extend this to detecting cycles in arrays?

### ✅ Integration
- [ ] Challenge: Solve "Find Duplicate in Array" using array-as-linked-list
- [ ] Connect: How is this related to Cyclic Sort (Day 4A)?

---

## 📋 WEEK INTEGRATION: Days 6-7 (Thursday-Friday)

### ✅ Cross-Pattern Recognition (1.5 hours)
- [ ] Read 10 random problems from the Question Bank (no solutions)
- [ ] For EACH problem, identify: "This is a [Pattern] problem because..."
- [ ] Write pattern signature (keywords, constraints, data structure hints)

### ✅ Tabletop Problems (1 hour)
- [ ] Solve 2-3 problems combining 2+ patterns from the week
  - Example: Longest Substring with At Most K Distinct Characters (Hash + Sliding Window)
  - Example: Merge K Sorted Lists (Merge + Heap)
- [ ] Trace each solution on paper BEFORE coding

### ✅ Deep Dive Session (45 min)
- [ ] Pick the 2 hardest patterns (likely Monotonic Stack + Floyd's)
- [ ] Watch 1 external video explanation per pattern
- [ ] Teach it aloud (to rubber duck or recording)

### ✅ Reflection & Synthesis (30 min)
- [ ] Write 5-10 sentences answering:
  - "What was the hardest pattern and WHY?"
  - "Which cognitive lens (Section 🧩) helped most?"
  - "What surprised me about this week?"

---

## 📊 WEEKLY MASTERY CHECKLIST

By end of Week 5, verify:

### Conceptual Understanding
- [ ] I can explain the core analogy for each pattern (without looking)
- [ ] I understand the key invariant that makes each pattern work
- [ ] I can identify when to use each pattern (red flags, keywords)

### Mechanical Mastery
- [ ] I can trace all 6 patterns on paper without errors
- [ ] I can handle edge cases (empty, single, all same, reverse order)
- [ ] I can explain why each pattern is O(N) or O(N log N)

### Problem Solving
- [ ] I solved 15+ problems (3 per pattern minimum)
- [ ] My success rate is 70%+ on first attempts
- [ ] I can solve variations (circular, k-times, constrained space)

### Integration
- [ ] I solved 2+ mixed-pattern problems
- [ ] I recognized patterns in novel problems quickly (<2 min)
- [ ] I can explain trade-offs (time vs. space, simplicity vs. optimality)

### Retention
- [ ] I created a cheat sheet with all 6 patterns
- [ ] I can teach one pattern to someone else
- [ ] I can recall core insights without external reference

---

## 🎯 SELF-RATING (Day 7 Evening)

Rate yourself honestly (🟢 = Strong, 🟡 = OK, 🔴 = Weak):

| Skill | Day 1 | Day 2 | Day 3 | Day 4A | Day 4B | Day 5 |
|:---|:---:|:---:|:---:|:---:|:---:|:---:|
| Understanding Core | 🟢 | 🟢 | 🟢 | 🟢 | 🟢 | 🟢 |
| Tracing Examples | 🟢 | 🟢 | 🟢 | 🟢 | 🟢 | 🟢 |
| Solving Problems | 🟢 | 🟡 | 🟢 | 🟡 | 🟢 | 🟡 |
| Explaining Logic | 🟢 | 🟡 | 🟢 | 🟡 | 🟢 | 🟡 |
| Recognizing Pattern | 🟢 | 🟡 | 🟢 | 🟡 | 🟡 | 🟡 |

**If mostly 🟢:** Move to Week 6. ✅  
**If mix of 🟡 & 🟢:** Do 5 more problems on weakest areas.  
**If mostly 🔴:** Slow down, re-read, watch videos, redo traces.  

---

## 📝 NOTES & REFLECTIONS

**Day 1 Reflection:**
```
[Your reflection here]
```

**Day 2 Reflection:**
```
[Your reflection here]
```

**Day 3 Reflection:**
```
[Your reflection here]
```

**Day 4 Reflection:**
```
[Your reflection here]
```

**Day 5 Reflection:**
```
[Your reflection here]
```

**Week 5 Final Reflection:**
```
[Your reflection here]
```

---

**End of Week 5 Daily Checklist**  
*Consistency and reflection over perfection. Master the patterns, and the problems solve themselves.*
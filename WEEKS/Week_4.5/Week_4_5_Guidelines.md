# 📖 WEEK 4.5 — GUIDELINES & LEARNING STRATEGY

**Week:** 4.5 (TIER 1 - Critical Problem-Solving Patterns)  
**Duration:** 5 days | ~35,000-40,000 words  
**Difficulty:** 🟡 Medium (Critical for 70-80% of Interviews)  
**Total Files:** 5 instructional + 5 support files

---

## 🎯 WEEK OVERVIEW

Week 4.5 is the **most critical week** in this curriculum. Mastering these 5 patterns solves 70-80% of real coding interview problems. This week bridges foundation concepts (Week 1-4) with advanced patterns (Week 5+).

**Coverage Timeline:**
- **Day 1 (2 hrs):** Hash Map / Hash Set Patterns
- **Day 2 (2 hrs):** Monotonic Stack
- **Day 3 (2 hrs):** Merge Operations & Intervals
- **Day 4 (3 hrs):** Partition & Cyclic Sort + Kadane's Algorithm (split into Part A & Part B)
- **Day 5 (2 hrs):** Fast & Slow Pointers

---

## 🔑 CORE CONCEPTS ACROSS THE WEEK

| 📌 Day | 🎯 Pattern | 💡 Key Idea | ⏱️ Complexity | 💼 Interview % |
|--------|-----------|------------|-------------|---------------|
| **1** | HashMap/HashSet | O(1) lookup for frequency/existence | O(n) T, O(n) S | 65% |
| **2** | Monotonic Stack | Maintain order for next/prev queries | O(n) T, O(n) S | 55% |
| **3** | Merge Intervals | Overlap detection & calendar problems | O(n log n) T, O(1) S | 50% |
| **4A** | Partition/Cyclic | In-place segregation for O(1) space | O(n) T, O(1) S | 60% |
| **4B** | Kadane's Algo | Maximum subarray via DP reset logic | O(n) T, O(1) S | 60% |
| **5** | Fast/Slow Ptr | Cycle detection in O(1) space | O(n) T, O(1) S | 60% |

---

## 📚 LEARNING OBJECTIVES FOR THE WEEK

By the end of this week, you will:

1. ✅ **Recognize patterns** from problem statements (e.g., "two sum" → HashMap)
2. ✅ **Implement each pattern** from scratch without hints
3. ✅ **Solve variants** of each pattern (e.g., max/min, duplicates, constraints)
4. ✅ **Optimize from brute force** (O(n²)) to pattern-based (O(n) or O(n log n))
5. ✅ **Explain trade-offs:** Time vs Space, Simplicity vs Optimization
6. ✅ **Predict real-world usage** of each pattern

---

## 🏗️ LEARNING APPROACH FOR THIS WEEK

### Phase 1: Concept (Day 1 Morning)
- Read each day's **Section 1 (The Why)** and **Section 2 (The What)**.
- Understand the real-world motivation and core operations.
- Expected time: ~30 minutes per day.

### Phase 2: Implementation (Day 1 Afternoon + Day 2-3)
- Read **Section 3 (The How)** and **Section 4 (Visualization)**.
- Trace through examples manually.
- Implement 2-3 problems per pattern.
- Expected time: ~1.5 hours per pattern.

### Phase 3: Optimization & Analysis (Day 4)
- Read **Section 5 (Critical Analysis)**.
- Compare with brute force and alternative approaches.
- Understand complexity and trade-offs.
- Expected time: ~45 minutes per pattern.

### Phase 4: Real-World Context (Day 5)
- Read **Section 6 (Real Systems)**.
- Understand where these patterns show up in production.
- Answer **Section 10 (Knowledge Check)** questions.
- Expected time: ~30 minutes per pattern.

### Phase 5: Mastery (Day 5 Afternoon)
- Solve advanced problems (Supplementary Problems).
- Answer interview questions without solutions.
- Consolidate with Retention Hook.
- Expected time: ~2 hours.

---

## 💡 TIPS & STRATEGIES FOR SUCCESS

### ✅ DO's

1. **Pattern First, Code Second**
   - Understand the *why* before coding.
   - Trace examples by hand before implementing.

2. **Solve Problems in Order**
   - Start with Easy (🟢), move to Medium (🟡), then Hard (🔴).
   - Each increases difficulty incrementally.

3. **Compare Approaches**
   - Always implement brute force first (understand baseline).
   - Then optimize using the pattern (see the improvement).

4. **Explain Out Loud**
   - Verbally explain each pattern to someone (or yourself).
   - This builds interview communication skills.

### ❌ DON'Ts

1. **Don't Skip the "Why"**
   - Memorizing code without understanding context leads to failure.
   - Problems vary; patterns are the key.

2. **Don't Rush to Solutions**
   - First: read problem, identify pattern, implement.
   - Only then: check solutions if stuck.

3. **Don't Memorize Code**
   - Focus on logic and trade-offs.
   - You must be able to code from scratch in interviews.

4. **Don't Skip Edge Cases**
   - Empty input, single element, all same, all different.
   - Edge cases distinguish good from great solutions.

---

## 🔗 HOW TOPICS CONNECT ACROSS THE WEEK

### Day 1 → Day 2
- **Day 1 (HashMap)** finds elements/frequencies.
- **Day 2 (Monotonic Stack)** finds next/prev elements *in order*.
- Together: Frequency-based "Next Greater" problems.

### Day 2 → Day 3
- **Day 2 (Stack)** processes elements one-by-one.
- **Day 3 (Merge)** processes sorted sequences together.
- Together: Interval scheduling with stacks (advanced).

### Day 3 → Day 4
- **Day 3 (Merge)** combines sequences efficiently.
- **Day 4A (Partition)** segregates sequences efficiently.
- Together: Rearrange, reorder, segregate problems.

### Day 4A ↔ Day 4B
- **Day 4A (Partition)** uses in-place swaps.
- **Day 4B (Kadane)** uses running sum (no swaps).
- Both: O(1) space, but different mechanisms.

### Day 4B → Day 5
- **Day 4B (Kadane)** tracks running state (sum).
- **Day 5 (Fast/Slow)** tracks running state (two pointers).
- Both: State machines, different contexts.

---

## 📊 PRACTICE STRATEGY & TIME MANAGEMENT

### Daily Schedule (Recommended)

**Day 1 (HashMap):**
- 9:00-10:00 AM: Read Sections 1-2, understand concepts.
- 10:00-11:30 AM: Work through Visualization examples.
- 11:30 AM-1:00 PM: Solve 3-4 Easy problems.
- 2:00-3:00 PM: Solve 1-2 Medium problems.

**Days 2-5:** Follow similar schedule, adjusting difficulty as you progress.

### Problem Progression Per Pattern

For each pattern:
1. Solve the **canonical problem** (e.g., Two Sum for HashMap).
2. Solve **2-3 variants** (e.g., Two Sum II, 3Sum, 4Sum).
3. Solve **1-2 advanced** (e.g., Two Sum variants with constraints).

### Target Problem Count

- **Easy problems:** 2-3 per pattern (10-15 total for week).
- **Medium problems:** 2-3 per pattern (10-15 total for week).
- **Hard problems:** 0-1 per pattern (encouraged but not mandatory).
- **Total:** 20-30 problems for the week.

---

## 🧩 WEEKLY CHECKLIST

Use this to track progress:

### By End of Day 1
- [ ] Understand HashMap O(1) lookup and insertion
- [ ] Know when to use HashMap vs HashSet
- [ ] Solve "Two Sum" problem
- [ ] Solve "Valid Anagram" or similar

### By End of Day 2
- [ ] Understand Monotonic Stack concept
- [ ] Know next greater/smaller patterns
- [ ] Solve "Next Greater Element"
- [ ] Solve "Trapping Rain Water" (medium)

### By End of Day 3
- [ ] Understand interval merging logic
- [ ] Know overlap detection
- [ ] Solve "Merge Intervals"
- [ ] Solve "Insert Interval" (medium)

### By End of Day 4
- [ ] Understand Dutch National Flag (3-way partition)
- [ ] Understand Cyclic Sort pattern
- [ ] Solve "Sort Colors"
- [ ] Solve "First Missing Positive" (hard)
- [ ] Understand Kadane's Algorithm
- [ ] Solve "Maximum Subarray"
- [ ] Solve "Maximum Product Subarray" (medium)

### By End of Day 5
- [ ] Understand Floyd's Cycle Detection
- [ ] Know how to find cycle start
- [ ] Solve "Linked List Cycle"
- [ ] Solve "Find Duplicate Number" (medium)
- [ ] Answer all "Knowledge Check" questions
- [ ] Consolidate week with Retention Hooks

---

## 🎯 HOW TO HANDLE DIFFICULT MOMENTS

### If You Get Stuck on a Pattern
1. **Re-read the Core Analogy** (Section 2) — refresh intuition.
2. **Re-trace the Visualization Examples** — manually step through.
3. **Read the Real Systems section** — ground in real applications.
4. **Try a simpler problem first** — don't jump to hard variants.

### If You Can't Implement It
1. **Write pseudocode first** — no syntax, just logic.
2. **Compare with the detailed mechanics** (Section 3).
3. **Implement in a different language** if needed (just for clarity).
4. **Look at ONE example solution** if you've tried 15+ minutes.

### If You Pass Easy But Fail Medium
1. **Identify the added constraint** — what changed?
2. **Which part of the pattern is affected?** — review that section.
3. **Look for edge cases** — they often hide in constraints.

---

## 💼 INTERVIEW PREPARATION FOR THIS WEEK

### What Interviewers Test
Interviewers will:
- Give a problem that *sounds* new but uses a familiar pattern.
- Check if you can identify the pattern quickly.
- Test edge case handling and trade-off understanding.

### Red Flags to Avoid
- ❌ "I think I need to brute force..." (O(n²) when O(n) exists)
- ❌ "I'll use a hash map to store everything..." (O(n) space when O(1) is possible)
- ❌ "Let me sort the array..." (O(n log n) when O(n) is possible)

### Green Signals to Demonstrate
- ✅ "I recognize this as a [Pattern] problem."
- ✅ "Here's the brute force approach... but we can optimize with [Pattern]."
- ✅ "This trades O(n) space for O(n) time efficiency."

---

## 📈 SUCCESS METRICS

By the end of this week, you should be able to:

1. **Speed:** Solve Easy variants in < 10 minutes.
2. **Accuracy:** Solve Medium variants with < 1 bug.
3. **Communication:** Explain your approach before coding.
4. **Optimization:** Recognize brute force and optimize to O(n).

---

## 🔗 CONNECTIONS TO FUTURE WEEKS

- **Week 5 (Trees):** Heap problems use HashMap + Heap, extending Day 1.
- **Week 6 (Graphs):** BFS/DFS use HashMap for visited tracking, extending Day 1.
- **Week 11 (DP):** Kadane's (Day 4B) is foundational to DP patterns.

---

**End of Guidelines. Move to the next support file: Summary_Key_Concepts.md**
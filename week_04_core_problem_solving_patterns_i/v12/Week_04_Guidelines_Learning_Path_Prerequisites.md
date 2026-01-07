# 📘 Week 04: Guidelines, Learning Path & Prerequisites

**Week:** 4 | **Title:** Core Problem-Solving Patterns I  
**Duration:** 5 days | **Total Content:** ~80,500 words  
**Difficulty:** 🟡 Intermediate (builds on Weeks 1-3)  
**Status:** Complete and Production-Ready

---

## 🎯 WEEK OVERVIEW

Week 04 is the gateway to algorithmic mastery. You've learned the fundamentals (recursion, arrays, sorting, hashing). Now you'll acquire the **patterns** that turn these primitives into elegant, efficient solutions.

This week introduces **five core problem-solving patterns** used across industry, competitive programming, and systems design. Each pattern is a reusable template:
- Recognize the pattern in a problem
- Apply the template
- Solve in O(n log n) or O(n) instead of O(n²)

These patterns—two-pointers, sliding windows, divide & conquer, binary search on answers—are the building blocks for all advanced algorithms that follow.

---

## 📚 PREREQUISITES

**Essential (Must Review if Unsure):**
- ✅ **Week 1:** Recursion, call stack, base cases
- ✅ **Week 2:** Binary search on sorted arrays, binary search mechanics
- ✅ **Week 3:** Hash maps (hash tables, collision handling), sorting fundamentals
- ✅ **Weeks 1-3:** Array indexing, pointer arithmetic, in-place operations

**Helpful (Nice to Know):**
- Big-O notation and complexity analysis (Week 1-3 covered this)
- Time/space trade-offs (Weeks 1-3 covered this)
- Basic algorithm analysis (Week 1 covered this)

**If You're Weak On:**
- **Recursion:** Review Week 1 Day 3 before starting Week 4 Day 4 (Divide & Conquer)
- **Hash maps:** Review Week 3 Day 3 before starting Week 4 Day 3 (Variable Window)
- **Binary search:** Review Week 2 Day 4 before starting Week 4 Day 5 (Binary Search Pattern)

---

## 🗓️ WEEKLY LEARNING PATH

### DAY 1: Two-Pointer Patterns (4 hours)
**Topics:** Same-direction, opposite-direction, invariants  
**Prerequisite:** Arrays (Week 2), pointer arithmetic  
**Reading Time:** 1-1.5 hours  
**Implementation Time:** 2-2.5 hours  
**Practice:** 8 LeetCode problems (Easy to Medium)

**Milestone:** Understand synchronized pointer movement and invariant preservation

### DAY 2: Sliding Window (Fixed Size) (4 hours)
**Topics:** Fixed-length windows, monotonic deque, amortized analysis  
**Prerequisite:** Day 1 (two-pointer foundation)  
**Reading Time:** 1-1.5 hours  
**Implementation Time:** 2-2.5 hours  
**Practice:** 8 LeetCode problems (Medium)

**Milestone:** Optimize O(n*k) → O(n) through incremental updates

### DAY 3: Sliding Window (Variable Size) (4 hours)
**Topics:** Dynamic constraints, expand/shrink, frequency maps  
**Prerequisite:** Days 1-2, hash maps (Week 3)  
**Reading Time:** 1-1.5 hours  
**Implementation Time:** 2-2.5 hours  
**Practice:** 8 LeetCode problems (Medium to Hard)

**Milestone:** Solve constraint-based substring/subarray problems

### DAY 4: Divide & Conquer (4 hours)
**Topics:** Recursive decomposition, Master Theorem, recurrence relations  
**Prerequisite:** Recursion (Week 1), arrays (Week 2)  
**Reading Time:** 1-1.5 hours  
**Implementation Time:** 2-2.5 hours  
**Practice:** 8 LeetCode problems (Medium to Hard)

**Milestone:** Understand optimal substructure and O(n log n) sorting

### DAY 5: Binary Search as Pattern (4 hours)
**Topics:** Answer space search, monotonic feasibility, optimization  
**Prerequisite:** Binary search basics (Week 2), recursion (Week 1)  
**Reading Time:** 1-1.5 hours  
**Implementation Time:** 2-2.5 hours  
**Practice:** 8 LeetCode problems (Medium to Hard)

**Milestone:** Recognize and solve optimization via feasibility checking

---

## 💡 CORE CONCEPTS AT A GLANCE

| Pattern | What | When | Complexity | Key Insight |
|---------|------|------|-----------|------------|
| **Two-Pointer** | Sync'd pointers, invariants | Merging, pairs, optimization | O(n) time, O(1) space | Maintain guarantees as pointers advance |
| **Sliding Window (Fixed)** | k-size window slide | Running metrics, bounded subarrays | O(n*k)→O(n) | Incremental updates beat recalculation |
| **Sliding Window (Variable)** | Dynamic constraint window | At-most-K, substring patterns | O(n) amortized | Each element enters/exits once |
| **Divide & Conquer** | Recursive decomposition | Sorting, inversions, optimal substructure | O(n log n) or better | Solve subproblems independently, combine |
| **Binary Search (Answer)** | Search answer space, not array | Optimization with monotonic feasibility | O(log(range) × check) | Once achievable, better answers always work |

---

## 🎯 LEARNING OUTCOMES BY DAY

**After Day 1:** Can identify and implement two-pointer patterns; understand invariants as core principle; apply to merging, pairs, optimization

**After Day 2:** Can optimize sliding window problems from O(n*k) to O(n); implement monotonic deque; understand amortization

**After Day 3:** Can solve constraint-based substring/subarray problems; maintain dynamic windows; recognize at-most-K patterns

**After Day 4:** Can decompose problems recursively; analyze complexity via recurrence relations; apply Master Theorem

**After Day 5:** Can recognize optimization problems; identify monotonic feasibility; implement binary search on answer space

---

## 🏆 SUCCESS METRICS

**By End of Week 04, You Should Be Able To:**

✅ **Pattern Recognition:**
- Look at a problem and identify which pattern applies (or combination)
- Understand when each pattern beats naive approaches
- Recognize red flags in problem statements

✅ **Implementation:**
- Write efficient code using each pattern without memorization
- Trace through complex examples and verify correctness
- Optimize O(n²) solutions into O(n log n) or O(n)

✅ **Analysis:**
- Explain why each pattern works (invariants, amortization, Master Theorem)
- Analyze trade-offs (time, space, implementation complexity)
- Evaluate when patterns apply vs. when they fail

✅ **Application:**
- Solve 40+ practice problems from LeetCode
- Explain patterns in interviews with confidence
- Connect patterns to real production systems

---

## 📖 STUDY TIPS

**For Each Day:**

1. **Read the instructional file (1-1.5 hours)**
   - Focus on Chapters 1-2 (motivation and mental model)
   - Work through the visualizations carefully
   - Understand invariants/properties before mechanics

2. **Study the mechanics (0.5-1 hour)**
   - Read Chapter 3 with traced examples
   - Manually trace through 2-3 examples on paper
   - Understand decision logic step-by-step

3. **Connect to reality (0.5 hour)**
   - Read Chapter 4 real-world stories
   - Understand why the pattern matters at scale
   - See how professionals use it

4. **Practice (2-2.5 hours)**
   - Start with Easy problems (get the mechanics right)
   - Move to Medium (apply pattern correctly)
   - Tackle Hard variants (extend and optimize)
   - Don't look at solutions until you're stuck (spend 20+ mins)

5. **Reflect (0.5 hour)**
   - Review Socratic questions (think deeply, don't look for answers)
   - Note misconceptions you had
   - Understand when you'd use this pattern next time

---

## ⚠️ COMMON PITFALLS

**Week-Wide Issues:**

- **Pitfall 1:** Learning the mechanics without understanding the pattern
  - **Fix:** Study Chapter 2 (Mental Model) carefully; understand *why* before *how*

- **Pitfall 2:** Solving problems but not recognizing patterns
  - **Fix:** After each problem, ask: "What pattern is this? When else does this apply?"

- **Pitfall 3:** Memorizing solutions instead of understanding patterns
  - **Fix:** Cover the solution; try solving from scratch; understand why your approach differs

- **Pitfall 4:** Focusing on Big-O and ignoring practical performance
  - **Fix:** Read Chapter 4 (Performance & Real Systems); understand constants matter

- **Pitfall 5:** Trying to solve problems before understanding the pattern
  - **Fix:** Always read the instructional file *first*; study traced examples; *then* practice

**Day-Specific Issues:**

- **Days 1-3:** Off-by-one errors in pointer movement
  - Fix: Trace very carefully; use explicit boundary checks

- **Day 2:** Confusing fixed-size windows with variable-size
  - Fix: Fixed means you always see k elements; variable means size changes

- **Day 3:** Not recognizing when to expand vs. shrink
  - Fix: Study state machine formalism; decide based on constraint

- **Day 4:** Confusing divide & conquer with dynamic programming
  - Fix: D&C solves subproblems independently; DP tracks shared states

- **Day 5:** Assuming every optimization problem suits binary search
  - Fix: Verify monotonicity carefully; ensure feasibility is well-defined

---

## 🎓 INTERVIEW PREPARATION

**Week 04 is heavily weighted in technical interviews.** Expect:

- 2-3 problems per interview from these patterns
- Pattern recognition questions: "What approach would you use?"
- Complexity analysis: "Why is this O(n log n)?"
- Trade-offs: "Why sliding window vs. hash map?"
- Real systems: "How would this work at scale?"

**Preparation Strategy:**
1. **Days 1-5:** Study instructional files; practice problems
2. **Days 6-7:** Review all 5 patterns; solve mixed problem sets
3. **Day 8:** Revisit hard problems; do interview-style mock sessions

---

## 🚀 AFTER WEEK 04

**Week 05:** Pattern Integration & Advanced Variants
- Combine multiple patterns (when should you use two-pointer + sliding window?)
- Optimize further (when hash maps are suboptimal)

**Weeks 6-13:** Graph, Tree, and DP Algorithms
- These patterns apply to graphs and trees
- DP uses similar decomposition but with memoization

**Week 14:** Dynamic Programming
- Alternative optimization approach (subproblems with overlap)
- Complements binary search on answers

---

## 📞 GETTING HELP

**If you're stuck on:**

- **Mechanics:** Re-read Chapter 3 (Mechanics & Implementation); trace examples on paper
- **Correctness:** Ask: "What invariant am I breaking?" (Chapter 2)
- **Complexity:** Study recurrence relations and Master Theorem (Day 4)
- **When to use:** Review Chapter 5 (Integration & Mastery) decision frameworks
- **Practice problems:** Review the instructional file *first*; then attempt problems

**Red Flags (Stop & Review):**
- You're solving problems but can't name the pattern
- You can solve problems but don't understand *why* your approach works
- You're confusing multiple patterns (review comparison tables)
- You're getting wrong answers on simple examples (trace very carefully)

---

## 📋 WEEKLY CHECKLIST

**Preparation (Before Week Starts):**
- ☐ Review Week 1 recursion if you feel weak on it
- ☐ Review Week 2 binary search basics
- ☐ Review Week 3 hash maps and sorting

**Daily (Each of 5 Days):**
- ☐ Read instructional file (Chapters 1-2, then 3-5)
- ☐ Manually trace 2-3 examples on paper
- ☐ Read real-world stories (Chapter 4)
- ☐ Solve 8 practice problems (Easy → Medium → Hard)
- ☐ Answer Socratic reflection questions

**End of Week (Day 6-7):**
- ☐ Review all 5 patterns using comparison table
- ☐ Solve 10-15 mixed problems across all patterns
- ☐ Do mock interview (30 mins, solve 1 hard problem)
- ☐ Identify your weakest pattern; spend 1-2 hours reviewing it

---

## 🎯 WEEK 04 AT A GLANCE

| Day | Pattern | Time | Key Concept | Practice |
|-----|---------|------|-------------|----------|
| 1 | Two-Pointer | 4h | Invariants | 8 problems |
| 2 | Sliding (Fixed) | 4h | Amortization | 8 problems |
| 3 | Sliding (Variable) | 4h | Dynamic Constraints | 8 problems |
| 4 | Divide & Conquer | 4h | Recurrence | 8 problems |
| 5 | Binary Search | 4h | Answer Space | 8 problems |
| **Total** | **5 Patterns** | **20h** | **All Core** | **40 problems** |

---

**Status:** Ready to begin. Start with Day 1 instructional file.


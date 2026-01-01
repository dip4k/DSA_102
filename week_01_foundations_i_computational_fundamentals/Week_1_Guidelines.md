# 🎯 WEEK 1 — GUIDELINES & LEARNING STRATEGY

**Week Theme:** Computational Foundations & Mental Models  
**Status:** Foundation Building  
**Prerequisites:** None (Entry point to curriculum)

---

## 📚 Week Overview

Week 1 establishes the **computational thinking framework** that underlies all data structures and algorithms. This week is not about memorizing code—it's about building mental models of how computers execute programs, consume resources, and make trade-offs.

**Core Philosophy:**
- Understanding beats memorization
- Mechanical reasoning over pattern matching
- Space and time are physical constraints, not abstract concepts

**Connection to Previous Week:** N/A (First week)  
**Connection to Next Week:** Week 2 builds Arrays and Two Pointers patterns on top of these foundations.

---

## 🎓 Weekly Learning Objectives

By the end of Week 1, you will be able to:

1. **Explain the RAM Model** and how memory addressing works at the hardware level.
2. **Analyze time complexity** using Big-O notation and identify dominant terms.
3. **Calculate space complexity** including hidden costs (stack frames, object overhead).
4. **Trace recursive execution** manually using call stacks and recursion trees.
5. **Apply memoization** to transform exponential algorithms into linear ones.
6. **Reason about trade-offs** between time, space, simplicity, and correctness.

---

## 📋 Key Concepts Overview

| Day | 🎯 Core Topic | 💡 One-Line Essence |
|-----|--------------|---------------------|
| 1 | RAM Model & Pointers | Memory is just a giant array of bytes with addresses. |
| 2 | Asymptotic Analysis | Focus on growth rate, not constants. |
| 3 | Space Complexity | Stack and Heap are different; both cost real memory. |
| 4 | Recursion I | Functions can call themselves; the stack remembers where you came from. |
| 5 | Recursion II | Cache results to avoid repeating work (Memoization = DP). |

---

## 🧭 Learning Approach & Methodology

### 🔄 Daily Study Flow (2-3 hours per day)

**Phase 1: Read & Absorb (45-60 min)**
- Read the instructional file section by section.
- Focus on **Section 1 (Why)** and **Section 2 (What)** first.
- Draw the analogies and diagrams yourself on paper.

**Phase 2: Trace & Simulate (30-45 min)**
- Pick 2-3 examples from **Section 4 (Visualization)**.
- Trace them manually step-by-step.
- Use a whiteboard or paper—no IDE yet.

**Phase 3: Practice (45-60 min)**
- Attempt 2-3 problems from the Practice Problems list.
- Start with 🟢 Easy problems.
- **No code initially**—write logic in pseudocode first.

**Phase 4: Reflect (15 min)**
- Answer 1-2 Knowledge Check questions from **Section 10**.
- Write down what clicked and what confused you.

---

## 🧱 Common Mistakes & Pitfalls

### ❌ Pitfall 1: Confusing "Input Space" with "Auxiliary Space"
- **Mistake:** "My array is O(n), so space complexity is O(n)."
- **Reality:** Input doesn't count unless you copy it. Auxiliary space is *extra* space.
- **Fix:** Always clarify which definition the interviewer wants.

### ❌ Pitfall 2: Forgetting Stack Space in Recursion
- **Mistake:** "This recursive function uses no extra memory."
- **Reality:** Every call frame uses stack space. O(n) depth = O(n) space.
- **Fix:** Count maximum stack depth, not total calls.

### ❌ Pitfall 3: Over-optimizing Constants
- **Mistake:** "I changed `n*2` to `n<<1` (bit shift). It's faster!"
- **Reality:** Compilers already do this. Focus on Big-O first.
- **Fix:** Optimize algorithms (O(n²) → O(n log n)) before micro-optimizations.

### ❌ Pitfall 4: Blindly Memoizing Everything
- **Mistake:** "I'll cache all function results!"
- **Reality:** Memoization has overhead. Only helps with repeated calls.
- **Fix:** Identify overlapping subproblems first.

### ❌ Pitfall 5: Tracing Too Deep
- **Mistake:** Trying to trace Fibonacci(20) by hand.
- **Reality:** You'll get lost and confused.
- **Fix:** Trace small inputs (n=3, n=4) to understand the pattern, then trust the induction.

### ❌ Pitfall 6: Skipping the "Why"
- **Mistake:** Jumping straight to code without understanding motivation.
- **Reality:** You'll forget everything in 2 weeks.
- **Fix:** Always read Section 1 (The Why) carefully.

### ❌ Pitfall 7: Not Drawing Diagrams
- **Mistake:** Reading text passively.
- **Reality:** Diagrams reveal structure that text hides.
- **Fix:** Draw every major concept yourself.

---

## 🕒 Time & Practice Strategy

### ⏰ Suggested Daily Schedule

**Total Time:** 2-3 hours/day × 5 days = 10-15 hours/week

| Activity | Time | Purpose |
|----------|------|---------|
| Read Instructional File | 45-60 min | Build mental model |
| Trace Examples | 30-45 min | Internalize mechanics |
| Practice Problems | 45-60 min | Apply concepts |
| Review & Reflect | 15 min | Solidify learning |

### 🔁 Integration with Previous Weeks

Since this is Week 1, no previous review needed. However:
- **Daily Mini-Review (5 min):** Before starting a new day, review the previous day's "One-Liner Essence" from Section 11.
- **Weekend Review (30 min):** After Day 5, re-read all 5 "Retention Hook" sections.

---

## ✅ Weekly Checklist

Use this to track your progress:

### 📖 Conceptual Understanding
- [ ] I can explain the RAM Model to someone without CS background.
- [ ] I can identify the Big-O of a loop nest by counting nested levels.
- [ ] I understand the difference between Stack and Heap memory.
- [ ] I can trace a recursive function for inputs n=3, n=4 without confusion.
- [ ] I can explain what memoization is and when to use it.

### 🧪 Practical Skills
- [ ] I have traced at least 10 examples manually (2 per day).
- [ ] I have attempted at least 10 practice problems (2 per day).
- [ ] I have drawn diagrams for at least 5 concepts.

### 🎙 Interview Readiness
- [ ] I can answer "What is Big-O?" in 30 seconds.
- [ ] I can answer "What is space complexity?" and give an example.
- [ ] I can explain recursion using an analogy.

### 📝 Reflection
- [ ] I have written down 3 key insights from this week.
- [ ] I have identified 2 concepts I need to revisit.

---

## 🎯 Week 1 Success Criteria

You have successfully completed Week 1 if:
1. ✅ You can trace any simple recursive function without getting lost.
2. ✅ You can calculate time and space complexity for basic algorithms.
3. ✅ You understand *why* memoization works (not just *how*).
4. ✅ You feel comfortable explaining these concepts to someone else.

**If not:** Re-read the sections you struggled with. Focus on **Section 2 (Mental Model)** and **Section 4 (Visualization)**.

---

*End of Week 1 Guidelines*

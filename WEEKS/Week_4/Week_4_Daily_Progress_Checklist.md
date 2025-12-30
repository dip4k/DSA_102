# ✅ Week 4 — Daily Progress Checklist (Pattern Mastery Tracker) (REGENERATED)

**🗓️ Week:** 4  
**📌 Theme:** Problem-Solving Patterns (Two Pointers, Sliding Window, D&C, Binary Search)  
**🎯 Goal:** Check off each milestone as you master the patterns  
**⏱️ Daily Time:** 2-3 hours per day (study + practice)

---

## 📅 DAY 1: TWO POINTERS

### 🎓 Learning Objectives
- [ ] Understand three variants: Left/Right, Fast/Slow, Same Direction
- [ ] Master when to use Two Pointers vs Hash Map
- [ ] Analyze space/time trade-offs (O(1) space advantage)
- [ ] Apply to sorted arrays AND linked lists

### 📚 Study Checklist
- [ ] Read Week 4 Day 1 instructional file (Section 1-3: Why/What/How)
- [ ] Watch visualization examples (Two Sum II trace)
- [ ] Review edge cases (empty array, single element, duplicates)
- [ ] Understand Floyd's Fast/Slow algorithm proof

### 💻 Practice Problems (Check when solved)
- [ ] **Two Sum II (#167)** — Left/Right pointers | Easy | 15 min
- [ ] **Valid Palindrome (#125)** — Directional movement | Easy | 10 min
- [ ] **Linked List Cycle (#141)** — Fast/Slow | Easy | 15 min
- [ ] **Container with Most Water (#11)** — Greedy logic | Medium | 20 min
- [ ] **Bonus:** 3Sum (#15) | Medium | 30 min

### 🎯 End-of-Day Self-Test
- [ ] Can you explain why Two Pointers is O(n) and not O(n²)?
- [ ] Can you decide: Two Pointers or Hash Map for unsorted array? (Hash Map!)
- [ ] Can you explain why we move the shorter line in Container problem?

---

## 📅 DAY 2: FIXED SLIDING WINDOW

### 🎓 Learning Objectives
- [ ] Understand fixed window size k (known upfront)
- [ ] Master incremental update: `sum = sum - left + right`
- [ ] Optimize from O(n*k) to O(n)
- [ ] Apply to moving averages, max/min in windows

### 📚 Study Checklist
- [ ] Read Week 4 Day 2 instructional file (All 11 sections)
- [ ] Trace through Moving Average example manually
- [ ] Review edge cases (k > n, k = 1, empty array)
- [ ] Understand why fixed window is simpler than variable

### 💻 Practice Problems
- [ ] **Max Average Subarray (#643)** — Basic moving average | Easy | 15 min
- [ ] **Grumpy Bookstore (#1052)** — Window optimization | Medium | 25 min
- [ ] **Max Consecutive Ones III (#1004)** — Constraint tracking | Medium | 25 min
- [ ] **Bonus:** Sliding Window Maximum (#239, Deque) | Hard | 40 min

### 🎯 End-of-Day Self-Test
- [ ] Can you write the update formula for fixed window sums?
- [ ] Can you explain the O(n) optimization over O(n*k)?
- [ ] Can you handle edge case: k > array length?

---

## 📅 DAY 3: VARIABLE SLIDING WINDOW

### 🎓 Learning Objectives
- [ ] Understand variable window size (constraint-driven)
- [ ] Master expand-right, contract-left logic
- [ ] Prove amortized O(n) complexity
- [ ] Apply to substring problems (no repeating, k distinct, contains all)

### 📚 Study Checklist
- [ ] Read Week 4 Day 3 instructional file (Focus on Section 1, 5, 9)
- [ ] Understand amortized analysis (each element visited ≤ 2 times)
- [ ] Review constraint functions (frequency maps, counters)
- [ ] Study Longest Substring example trace

### 💻 Practice Problems
- [ ] **Longest Substring w/o Repeating (#3)** — Classic! | Medium | 25 min
- [ ] **At Most K Distinct (#340)** — Constraint tracking | Medium | 30 min
- [ ] **Permutation in String (#567)** — Anagram matching | Medium | 30 min
- [ ] **Bonus:** Minimum Window Substring (#76) | Hard | 45 min

### 🎯 End-of-Day Self-Test
- [ ] Can you explain why variable window is O(n) despite nested pointers?
- [ ] Can you write the constraint function for "at most k distinct"?
- [ ] Can you differentiate: fixed vs variable window?

---

## 📅 DAY 4: DIVIDE & CONQUER

### 🎓 Learning Objectives
- [ ] Understand Divide → Conquer → Combine meta-strategy
- [ ] Apply Master Theorem for complexity analysis
- [ ] Master binary tree decomposition (O(log n) depth)
- [ ] Recognize when D&C applies (independent subproblems)

### 📚 Study Checklist
- [ ] Read Week 4 Day 4 instructional file (Section 1, 2, 5)
- [ ] Study Merge K Lists binary tree visualization
- [ ] Review Master Theorem examples
- [ ] Understand parallelization opportunities

### 💻 Practice Problems
- [ ] **Merge K Sorted Lists (#23)** — Binary merge | Medium | 30 min
- [ ] **Median of Two Sorted Arrays (#4)** — Binary partition | Hard | 45 min
- [ ] **Bonus:** Kth Smallest in Matrix (#378) | Medium | 35 min

### 🎯 End-of-Day Self-Test
- [ ] Can you explain why D&C is O(N log K) vs O(N*K) sequential?
- [ ] Can you apply Master Theorem to T(n) = 2*T(n/2) + O(n)?
- [ ] Can you identify when subproblems are independent?

---

## 📅 DAY 5: BINARY SEARCH ON ANSWER

### 🎓 Learning Objectives
- [ ] Transcend binary search as "array search" → "answer space search"
- [ ] Master monotonic property recognition
- [ ] Build feasibility functions
- [ ] Apply to optimization problems (minimize max, maximize min)

### 📚 Study Checklist
- [ ] Read Week 4 Day 5 instructional file (Section 1, 9, 11)
- [ ] Study Capacity to Ship example trace
- [ ] Review monotonicity proofs
- [ ] Understand feasibility function patterns

### 💻 Practice Problems
- [ ] **Capacity to Ship (#1011)** — Minimize max | Medium | 30 min
- [ ] **Koko Eating Bananas (#875)** — Minimize speed | Medium | 25 min
- [ ] **Split Array Largest Sum (#410)** — Minimize max | Hard | 40 min

### 🎯 End-of-Day Self-Test
- [ ] Can you recognize "minimize the maximum" pattern?
- [ ] Can you verify monotonic property for a problem?
- [ ] Can you write a feasibility function for "ship in D days"?

---

## 📅 DAY 6-7: REVIEW & MIXED PRACTICE

### 🎓 Consolidation Goals
- [ ] Review all 5 patterns (read summaries)
- [ ] Solve 1 problem from each pattern (random selection)
- [ ] Attempt 2-3 Hard problems (any pattern)
- [ ] Simulate mock interview (1 Medium problem, 30 min limit)

### 📚 Review Checklist
- [ ] Re-read Week 4 Summary & Key Concepts
- [ ] Review Interview Q&A (conceptual questions)
- [ ] Revisit any problems marked "struggled" earlier in week
- [ ] Check all edge cases for each pattern

### 💻 Mixed Practice (Random Selection)
- [ ] **Pattern Mix 1:** Two Sum II + Longest Substring
- [ ] **Pattern Mix 2:** Container + Fixed Window problem
- [ ] **Pattern Mix 3:** Merge K + Binary Search Answer
- [ ] **Challenge:** Trapping Rain Water (#42) | Hard | 40 min

### 🎯 Week 4 Final Self-Assessment
- [ ] I can recognize which pattern applies from problem description
- [ ] I can explain trade-offs (Two Pointers vs Hash Map, etc.)
- [ ] I can solve 70%+ Easy, 50%+ Medium on first attempt
- [ ] I understand amortized analysis for variable window
- [ ] I can apply Master Theorem to D&C problems
- [ ] I can verify monotonicity for Binary Search on Answer

---

## 📊 WEEKLY PROGRESS TRACKER

### Pattern Mastery (Self-Rate 1-5)
- **Two Pointers:** ☐ 1 ☐ 2 ☐ 3 ☐ 4 ☐ 5
- **Fixed Sliding Window:** ☐ 1 ☐ 2 ☐ 3 ☐ 4 ☐ 5
- **Variable Sliding Window:** ☐ 1 ☐ 2 ☐ 3 ☐ 4 ☐ 5
- **Divide & Conquer:** ☐ 1 ☐ 2 ☐ 3 ☐ 4 ☐ 5
- **Binary Search on Answer:** ☐ 1 ☐ 2 ☐ 3 ☐ 4 ☐ 5

### Problem Completion
- **Easy Problems:** ___ / 15 (Target: 12+)
- **Medium Problems:** ___ / 18 (Target: 10+)
- **Hard Problems:** ___ / 6 (Target: 2+, understand all)

### Time Efficiency
- **Easy avg time:** ___ min (Target: ≤ 15 min)
- **Medium avg time:** ___ min (Target: ≤ 30 min)
- **Stuck rate:** ___ % (Target: < 30% for Medium)

---

## 🎯 SUCCESS INDICATORS

✅ **Pattern Recognition:** Can identify pattern from keywords in <30 seconds  
✅ **Explanation:** Can explain "why this pattern" to interviewer  
✅ **Edge Cases:** Automatically check empty, single element, duplicates  
✅ **Complexity:** Can derive time/space complexity without hints  
✅ **Trade-offs:** Can compare multiple approaches (Two Pointers vs Hash Map)

---

## 🔄 SPACED REPETITION SCHEDULE

### Week 5 (3 days later):
- [ ] Re-solve 2-3 problems you struggled with in Week 4
- [ ] Review pattern summaries (5 min each)

### Week 6 (7 days later):
- [ ] Re-solve 5 random Week 4 problems (no looking at solutions)
- [ ] Simulate mock interview with Week 4 problem

### Week 8 (14 days later):
- [ ] Final review: Solve 1 Hard problem from each pattern
- [ ] Self-assess: Can I teach these patterns to someone else?

---

**Generated:** December 30, 2025  
**Version:** 9.0 (Regenerated with actionable daily checklists)  
**Status:** ✅ COMPLETE
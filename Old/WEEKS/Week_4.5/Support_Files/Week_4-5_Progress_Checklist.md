# ✅ Week 4.5 — Daily Progress Checklist (Tier 1 Pattern Mastery) (COMPLETE)

**🗓️ Week:** 4.5  
**📌 Theme:** Critical Problem-Solving Patterns (80%+ interview coverage)  
**🎯 Goal:** Master patterns that solve majority of interview problems  
**⏱️ Daily Time:** 3-4 hours (study + practice)

---

## 📅 DAY 1: HASH MAP / HASH SET

### 🎓 Learning Objectives
- [ ] Understand O(1) average lookup vs O(n) array search
- [ ] Master when to use Hash Map vs Hash Set vs Array
- [ ] Solve Two Sum in O(n) (vs O(n²) nested loops)
- [ ] Apply to anagram detection, duplicate detection
- [ ] Understand hash collisions, load factor (0.75)

### 📚 Study Checklist
- [ ] Read Week 4.5 Day 1 instructional file (All 11 sections)
- [ ] Understand internal structure (buckets, chaining)
- [ ] Review edge cases (empty, duplicates, null keys)
- [ ] Study why Hash Map is O(1) average, O(n) worst case

### 💻 Practice Problems (Check when solved)
- [ ] **Two Sum (#1)** — THE classic | Easy | 15 min (**MUST MASTER**)
- [ ] **Valid Anagram (#242)** — Frequency count | Easy | 10 min
- [ ] **Contains Duplicate (#217)** — Hash Set | Easy | 10 min
- [ ] **Group Anagrams (#49)** — Hash Map grouping | Medium | 25 min
- [ ] **Longest Substring w/o Repeat (#3)** — Hash Set + window | Medium | 30 min
- [ ] **Bonus:** Subarray Sum K (#560) | Medium | 30 min

### 🎯 End-of-Day Self-Test
- [ ] Can you explain Two Sum O(n) solution without looking?
- [ ] Can you decide: Hash Map or Two Pointers for sorted array? (Two Pointers!)
- [ ] What's load factor? Why 0.75? (Balance space vs collisions)
- [ ] Can arrays be Hash Map keys? (No, mutable)

---

## 📅 DAY 2: MONOTONIC STACK

### 🎓 Learning Objectives
- [ ] Understand monotonic increasing vs decreasing stack
- [ ] Master "next greater element" pattern
- [ ] Apply to trapping rain water, histogram problems
- [ ] Recognize O(n) amortized (each element push/pop once)

### 📚 Study Checklist
- [ ] Read Week 4.5 Day 2 instructional file
- [ ] Trace Next Greater Element manually (draw stack)
- [ ] Understand why Monotonic Stack is O(n), not O(n²)
- [ ] Review when to use increasing vs decreasing stack

### 💻 Practice Problems
- [ ] **Next Greater Element I (#496)** — Basic pattern | Easy | 20 min
- [ ] **Daily Temperatures (#739)** — Days until warmer | Medium | 25 min
- [ ] **Next Greater Element II (#503)** — Circular array | Medium | 30 min
- [ ] **Trapping Rain Water (#42)** — HARD classic | 45 min
- [ ] **Bonus:** Largest Rectangle (#84) | Hard | 45 min

### 🎯 End-of-Day Self-Test
- [ ] Can you explain why each element pushed/popped only once?
- [ ] When do you use decreasing stack? (Next greater)
- [ ] Trapping Water: Stack vs Two Pointers? (Two Pointers better space)

---

## 📅 DAY 3: MERGE OPERATIONS & INTERVALS

### 🎓 Learning Objectives
- [ ] Master merging 2 sorted arrays/lists in O(n+m)
- [ ] Understand K-way merge with min-heap: O(N log K)
- [ ] Apply to interval problems (overlapping, insert)
- [ ] Recognize when to sort intervals first

### 📚 Study Checklist
- [ ] Read Week 4.5 Day 3 instructional file
- [ ] Understand heap approach for K lists (why O(N log K)?)
- [ ] Study interval merge pattern (sort + greedy)
- [ ] Review edge cases (empty lists, single interval)

### 💻 Practice Problems
- [ ] **Merge Sorted Array (#88)** — In-place | Easy | 15 min
- [ ] **Merge Two Sorted Lists (#21)** — Two pointers | Easy | 15 min
- [ ] **Merge K Sorted Lists (#23)** — Min-heap | Medium | 35 min
- [ ] **Merge Intervals (#56)** — Overlapping | Medium | 30 min
- [ ] **Insert Interval (#57)** — Three phases | Medium | 30 min
- [ ] **Bonus:** Interval Intersections (#986) | Medium | 30 min

### 🎯 End-of-Day Self-Test
- [ ] Why heap for K lists? (O(N log K) vs O(N*K) sequential)
- [ ] Merge Intervals: Why sort first? (Consecutive can overlap after sort)
- [ ] Insert Interval: What are the 3 phases? (Before, overlap, after)

---

## 📅 DAY 4 (PART A): PARTITION & CYCLIC SORT

### 🎓 Learning Objectives
- [ ] Master Dutch National Flag (3-way partition)
- [ ] Understand Cyclic Sort for missing/duplicate in [1..n]
- [ ] Apply in-place rearrangement with O(1) space
- [ ] Recognize when numbers in [1..n] or [0..n] → Cyclic Sort

### 📚 Study Checklist
- [ ] Read Week 4.5 Day 4 (Partition section)
- [ ] Trace Sort Colors with 3 pointers (low, mid, high)
- [ ] Understand Cyclic Sort invariant: arr[i] = i+1
- [ ] Review when pattern applies (range [1..n] or [0..n])

### 💻 Practice Problems
- [ ] **Move Zeroes (#283)** — Two pointers | Easy | 15 min
- [ ] **Sort Colors (#75)** — Dutch Flag | Medium | 25 min
- [ ] **Find All Missing (#448)** — Cyclic Sort | Medium | 30 min
- [ ] **Find Duplicate (#287)** — Cyclic OR Fast/Slow | Medium | 30 min
- [ ] **Bonus:** First Missing Positive (#41) | Hard | 40 min

### 🎯 End-of-Day Self-Test
- [ ] Sort Colors: Why 3 pointers? (low=0s boundary, mid=current, high=2s boundary)
- [ ] Cyclic Sort: When does it apply? (Numbers in [1..n] range)
- [ ] What's time complexity? (O(n) — each element swapped at most once)

---

## 📅 DAY 4 (PART B): KADANE'S ALGORITHM

### 🎓 Learning Objectives
- [ ] Master Kadane's for maximum subarray sum
- [ ] Understand DP recurrence: max_ending[i] = max(arr[i], max_ending[i-1] + arr[i])
- [ ] Apply to maximum product subarray (track min and max)
- [ ] Handle edge case: all negative numbers

### 📚 Study Checklist
- [ ] Read Week 4.5 Day 4 (Kadane's section)
- [ ] Trace Maximum Subarray manually
- [ ] Understand why O(n) single pass
- [ ] Review product variant (why track min AND max?)

### 💻 Practice Problems
- [ ] **Maximum Subarray (#53)** — Classic Kadane's | Medium | 20 min
- [ ] **Maximum Product Subarray (#152)** — Track min/max | Medium | 30 min
- [ ] **Bonus:** Max Sum Circular (#918) | Medium | 35 min

### 🎯 End-of-Day Self-Test
- [ ] Kadane's recurrence formula? (max(arr[i], max_ending + arr[i]))
- [ ] Why track both min and max for product? (Negative can flip)
- [ ] All negative: return max element or 0? (Clarify with interviewer)

---

## 📅 DAY 5: FAST & SLOW POINTERS

### 🎓 Learning Objectives
- [ ] Master Fast & Slow for linked list problems
- [ ] Understand cycle detection (Floyd's algorithm)
- [ ] Find cycle start with extended Floyd's
- [ ] Apply to middle, palindrome, Nth from end
- [ ] Recognize O(1) space advantage over Hash Set

### 📚 Study Checklist
- [ ] Read Week 4.5 Day 5 instructional file
- [ ] Understand relative speed = 1 (why they meet in cycle)
- [ ] Study Floyd's extended (find cycle start)
- [ ] Review gap technique (Nth from end)

### 💻 Practice Problems
- [ ] **Linked List Cycle (#141)** — Basic detection | Easy | 15 min
- [ ] **Middle of List (#876)** — Fast reaches end | Easy | 10 min
- [ ] **Linked List Cycle II (#142)** — Cycle start | Medium | 30 min
- [ ] **Happy Number (#202)** — Cycle in sequence | Medium | 20 min
- [ ] **Remove Nth from End (#19)** — Gap technique | Medium | 20 min
- [ ] **Palindrome List (#234)** — Middle + reverse | Medium | 30 min
- [ ] **Bonus:** Reorder List (#143) | Medium | 35 min

### 🎯 End-of-Day Self-Test
- [ ] Why does fast catch slow in cycle? (Relative speed = 1)
- [ ] Fast & Slow vs Hash Set? (O(1) space vs O(n))
- [ ] Find middle: What happens when fast reaches end? (Slow at middle)
- [ ] Cycle start: Why reset one pointer to head? (Distance equality)

---

## 📅 DAY 6-7: REVIEW & MIXED PRACTICE

### 🎓 Consolidation Goals
- [ ] Review all 5 pattern summaries
- [ ] Solve 1-2 problems from each pattern (random)
- [ ] Attempt 2-3 Hard problems
- [ ] Mock interview (1 Medium in 30 min, discuss approach)

### 📚 Review Checklist
- [ ] Re-read Week 4.5 Summary & Key Concepts
- [ ] Review Interview Q&A (conceptual questions)
- [ ] Revisit problems marked "struggled"
- [ ] Check all edge cases per pattern

### 💻 Mixed Practice
- [ ] **Pattern Mix 1:** Two Sum + Merge K Lists
- [ ] **Pattern Mix 2:** Kadane's + Fast & Slow
- [ ] **Challenge:** Trapping Water (#42) OR Largest Rectangle (#84)

### 🎯 Week 4.5 Final Self-Assessment
- [ ] Can I recognize pattern from problem keywords?
- [ ] Can I explain trade-offs (Hash Map vs Two Pointers)?
- [ ] Can I solve 80%+ Easy, 60%+ Medium on first attempt?
- [ ] Do I understand amortized O(n) for Monotonic Stack?
- [ ] Can I implement Fast & Slow without looking?

---

## 📊 WEEKLY PROGRESS TRACKER

### Pattern Mastery (Self-Rate 1-5)
- **Hash Map / Hash Set:** ☐ 1 ☐ 2 ☐ 3 ☐ 4 ☐ 5
- **Monotonic Stack:** ☐ 1 ☐ 2 ☐ 3 ☐ 4 ☐ 5
- **Merge Operations & Intervals:** ☐ 1 ☐ 2 ☐ 3 ☐ 4 ☐ 5
- **Partition & Cyclic Sort:** ☐ 1 ☐ 2 ☐ 3 ☐ 4 ☐ 5
- **Kadane's Algorithm:** ☐ 1 ☐ 2 ☐ 3 ☐ 4 ☐ 5
- **Fast & Slow Pointers:** ☐ 1 ☐ 2 ☐ 3 ☐ 4 ☐ 5

### Problem Completion
- **Easy Problems:** ___ / 18 (Target: 15+)
- **Medium Problems:** ___ / 22 (Target: 14+)
- **Hard Problems:** ___ / 6 (Target: 2+, understand all)

### Time Efficiency
- **Easy avg time:** ___ min (Target: ≤ 15 min)
- **Medium avg time:** ___ min (Target: ≤ 30 min)
- **Stuck rate:** ___ % (Target: < 25% for Medium)

---

## 🎯 SUCCESS INDICATORS

✅ **Two Sum Cold:** Can solve in <15 min without hints (THIS IS THE BAR)  
✅ **Pattern Recognition:** Identify pattern in <30 sec from keywords  
✅ **Trade-offs:** Can compare multiple approaches (Hash Map vs Two Pointers)  
✅ **Edge Cases:** Automatically check empty, negatives, boundaries  
✅ **Complexity:** Derive time/space without hints  
✅ **Explanation:** Can teach pattern to someone else

---

## 🔄 SPACED REPETITION SCHEDULE

### Week 5 (3 days later):
- [ ] Re-solve Two Sum (#1) — timed, <10 min
- [ ] Re-solve Merge K Lists (#23) — timed, <35 min
- [ ] Re-solve Maximum Subarray (#53) — timed, <20 min

### Week 6 (7 days later):
- [ ] Re-solve 5 random Medium from Week 4.5 (no solutions)
- [ ] Mock interview: 2 Medium in 60 min

### Week 8 (14 days later):
- [ ] Final review: 1 Hard per pattern
- [ ] Self-assess: Can I recognize ALL patterns instantly?

---

## 🚀 READY FOR INTERVIEWS?

**Checklist Before Moving to Week 5:**
- [ ] Two Sum in <15 min (COLD, no hints)
- [ ] Can explain Hash Map O(1) lookup (with caveats)
- [ ] Monotonic Stack: Understand amortized O(n)
- [ ] Merge K Lists: Why heap O(N log K)?
- [ ] Kadane's: Derive DP recurrence
- [ ] Fast & Slow: Explain cycle detection math

**If ALL checked:** You're ready for Week 5 (Trees & BST) ✅

---

**Generated:** December 30, 2025  
**Version:** 9.0 (Week 4.5)  
**Status:** ✅ COMPLETE
# ✅ Week 04 Daily Progress Checklist: Action Plan for Mastery

**Status:** Daily Accountability Tracker  
**Audience:** Self-directed learners  
**Purpose:** Track daily progress, ensure depth not just breadth

---

## 📅 MONDAY: Day 1 Core — Two-Pointer Patterns

### Morning Session (2 hours)

**Read Instructional Content (45 min)**
- [ ] Read Week 04 Day 1 Instructional file: Two-Pointer Patterns
- [ ] Note: Same-direction merge, opposite-direction water, invariant maintenance
- [ ] Mark 3 key insights:
  1. ___________________________________
  2. ___________________________________
  3. ___________________________________

**Visualize & Trace (30 min)**
- [ ] Draw two sorted arrays being merged (pointers at each start)
- [ ] Trace merge for: arr1=[1,3,5], arr2=[2,4,6]
  - [ ] Show pointer positions at each step
  - [ ] Mark what invariant is maintained
- [ ] Draw water container problem: heights=[1,8,6,2,5,4,8,3,7]
  - [ ] Show pointer positions and area calculations

**Comprehension Check (15 min)**
- [ ] Explain: Why is merge O(n+m) not O(n log n)?
- [ ] Write down: When can you move larger/smaller pointer in water problem?
- [ ] List: 3 real-world systems using merge logic

### Afternoon Session (2.5 hours)

**Code Practice Problems (2 hours)**
- [ ] LeetCode 88: Merge Sorted Array (🟢 Easy)
  - [ ] First attempt time: ____ min
  - [ ] Invariant maintained? ☐ Yes ☐ Partial
  - [ ] Tested edge: empty arrays ☑, one array empty ☑

- [ ] LeetCode 11: Container with Most Water (🟡 Medium)
  - [ ] Implementation time: ____ min
  - [ ] Why smaller pointer moves: _______________
  - [ ] Time complexity: O(n)? ☐ Yes ☐ Why?

- [ ] LeetCode 26: Remove Duplicates (🟢 Easy)
  - [ ] Write pointer interpretation: _______________
  - [ ] Tested: all same, no duplicates, mixed ☑

**Reflection & Integration (30 min)**
- [ ] When would you use two-pointer vs hash? (edge cases, constraints)
  - Reason 1: _______________
  - Reason 2: _______________
- [ ] Connect to Week 03: How does this build on sorting/hashing?
- [ ] What's next pattern after mastering today?

### Evening Checkpoint
- [ ] Can code merge sorted arrays without reference? ☐ Yes ☐ No
- [ ] Understand why water is O(n)? ☐ Yes ☐ Partial ☐ No
- [ ] Ready for Day 2? ☐ Yes ☐ Review needed

---

## 📅 TUESDAY: Day 2 Core — Sliding Window Fixed-Size

### Morning Session (2 hours)

**Read Instructional Content (45 min)**
- [ ] Read Week 04 Day 2 Instructional file: Sliding Window Fixed-Size
- [ ] Key concept: Move window in O(1) per slide (remove left, add right)
- [ ] Mark 3 insights:
  1. ___________________________________
  2. ___________________________________
  3. ___________________________________

**Visualize & Trace (30 min)**
- [ ] Draw window moving on [1,2,3,4,5,6,7] with k=3
  - [ ] Show window at each position
  - [ ] Track running sum at each step
- [ ] Draw max in window with deque for heights [1,3,1,2,0,5]
  - [ ] Show deque content at each step
  - [ ] Mark which indices are in deque

**Comprehension Check (15 min)**
- [ ] Why is O(n) and not O(nk)?
- [ ] What does deque maintain in max-window problem?
- [ ] When do you remove from front of deque?

### Afternoon Session (2.5 hours)

**Code Practice Problems (2 hours)**
- [ ] LeetCode 643: Maximum Average Subarray (🟢 Easy)
  - [ ] Implementation time: ____ min
  - [ ] Sum tracking: _______________

- [ ] LeetCode 239: Sliding Window Maximum (🟡 Medium)
  - [ ] Time attempt 1: ____ min
  - [ ] Deque content at each step clear? ☐ Yes

- [ ] LeetCode 346: Moving Average (🟢 Easy)
  - [ ] Queue-based vs deque-based: _______________

**Reflection & Integration (30 min)**
- [ ] Compare Day 1 (two-pointer) to Day 2 (sliding window fixed):
  - Same-direction vs moving both left and right?
  - Invariant is what?
- [ ] When would you use deque vs queue?

### Evening Checkpoint
- [ ] Can code running sum from scratch? ☐ Yes ☐ No
- [ ] Understand max-in-window with deque? ☐ Yes ☐ Partial
- [ ] Ready for Day 3? ☐ Yes ☐ Review needed

---

## 📅 WEDNESDAY: Day 3 Core — Sliding Window Variable-Size

### Morning Session (2 hours)

**Read Instructional Content (45 min)**
- [ ] Read Week 04 Day 3: Sliding Window Variable-Size
- [ ] Key concept: Expand/shrink to maintain constraint
- [ ] Mark insights:
  1. ___________________________________
  2. ___________________________________
  3. ___________________________________

**Visualize & Trace (30 min)**
- [ ] Draw expanding/shrinking on "pwwkew" (longest without repeat)
  - [ ] Show window at key points
  - [ ] Mark when index map is updated
- [ ] Trace "eceba" with k=2 (at most 2 distinct)
  - [ ] Show frequency map at each step
  - [ ] Show when we shrink

**Comprehension Check (15 min)**
- [ ] Why O(n) even though window expands/shrinks?
- [ ] How do you detect constraint violation?
- [ ] When exactly do you shrink?

### Afternoon Session (2.5 hours)

**Code Practice Problems (2 hours)**
- [ ] LeetCode 3: Longest Substring Without Repeating (🟡 Medium)
  - [ ] Time attempt 1: ____ min
  - [ ] Index map vs frequency map: _______________
  - [ ] Tested: all unique, all same, mixed ☑

- [ ] LeetCode 340: At Most K Distinct (🟡 Medium)
  - [ ] Frequency tracking: clear? ☐ Yes
  - [ ] Shrinking logic: _______________

- [ ] LeetCode 76: Minimum Window Substring (🔴 Hard)
  - [ ] Time attempt 1: ____ min
  - [ ] If stuck: reference solution, understand, code again
  - [ ] Key insight: _______________

**Reflection & Integration (30 min)**
- [ ] Days 1-3 connection: Two-pointer thinking is foundation?
- [ ] Could you use hash-based approach instead of index tracking?

### Evening Checkpoint
- [ ] Can code longest without repeating from scratch? ☐ Yes ☐ No
- [ ] Understand at-most K distinct logic? ☐ Yes ☐ Partial
- [ ] Ready for Day 4? ☐ Yes ☐ Review needed

---

## 📅 THURSDAY: Day 4 Core — Divide-Conquer Pattern

### Morning Session (2 hours)

**Read Instructional Content (45 min)**
- [ ] Read Week 04 Day 4: Divide-Conquer Pattern
- [ ] Key concepts: Split, conquer, combine (three parts essential)
- [ ] Mark insights:
  1. ___________________________________
  2. ___________________________________

**Visualize & Trace (45 min)**
- [ ] Draw merge sort tree for [3,1,4,1,5]
  - [ ] Show split at each level
  - [ ] Show merge at each level
  - [ ] Mark recurrence T(n) = 2T(n/2) + O(n)
- [ ] Trace majority element on [3,2,3]
  - [ ] Show how recursion finds majorities

**Comprehension Check (20 min)**
- [ ] Why O(n log n) for merge sort?
- [ ] What's the recurrence relation?
- [ ] Space complexity and why?

### Afternoon Session (2.5 hours)

**Code Practice Problems (2 hours)**
- [ ] LeetCode 912: Sort Array (Merge Sort) (🟡 Medium)
  - [ ] Implementation time: ____ min
  - [ ] Recurrence: T(n) = ___________
  - [ ] Space: ________

- [ ] LeetCode 169: Majority Element (🟡 Medium)
  - [ ] Divide-conquer approach: clear? ☐ Yes
  - [ ] Voting approach comparison: _______________

- [ ] LeetCode 50: Pow(x, n) (🟡 Medium) — Similar divide-conquer thinking
  - [ ] Recursive decomposition: _______________

**Reflection & Integration (30 min)**
- [ ] How does divide-conquer connect to binary search?
- [ ] When would you use divide-conquer vs iteration?

### Evening Checkpoint
- [ ] Can write merge sort recurrence? ☐ Yes ☐ No
- [ ] Understand combine step in merge? ☐ Yes ☐ Partial
- [ ] Ready for Day 5? ☐ Yes ☐ Review needed

---

## 📅 FRIDAY: Day 5 Core — Binary Search Pattern

### Morning Session (2 hours)

**Read Instructional Content (45 min)**
- [ ] Read Week 04 Day 5: Binary Search as Pattern
- [ ] Key concept: Maintain invariant (target in [low, high)), narrow geometrically
- [ ] Mark insights:
  1. ___________________________________
  2. ___________________________________

**Visualize & Trace (30 min)**
- [ ] Draw binary search on [1,3,5,7,9,11], target=7
  - [ ] Show low, high, mid at each iteration
  - [ ] Mark invariant at each step
- [ ] Trace "minimize max load" feasibility
  - [ ] Show how binary search on capacity works

**Comprehension Check (15 min)**
- [ ] Why O(log n) not O(n)?
- [ ] What makes binary search work on answer space?
- [ ] What's the invariant?

### Afternoon Session (2.5 hours)

**Code Practice Problems (2 hours)**
- [ ] LeetCode 704: Binary Search (🟢 Easy)
  - [ ] Time: ____ min
  - [ ] Invariant maintained? ☐ Yes
  - [ ] Off-by-one errors? ☐ None ☐ Fixed

- [ ] LeetCode 34: Find First/Last Position (🟡 Medium)
  - [ ] First vs last variant: different termination?
  - [ ] Time: ____ min

- [ ] LeetCode 1011: Capacity to Ship (🟡 Medium) — Binary search on answer
  - [ ] Feasibility check logic: _______________
  - [ ] Time: ____ min

**Reflection & Integration (30 min)**
- [ ] Days 1-5: How do all patterns connect?
- [ ] When would you combine patterns?

### Evening Checkpoint
- [ ] Can code binary search from scratch? ☐ Yes ☐ No
- [ ] Understand answer-space binary search? ☐ Yes ☐ Partial
- [ ] Ready for integration? ☐ Yes ☐ Review needed

---

## 📅 SATURDAY: Integration & Synthesis

### Morning Session (2 hours)

**Review All 4 Patterns (90 min)**
- [ ] Two-pointer: 1 min explanation from scratch
- [ ] Sliding window (fixed & variable): 1 min explanation
- [ ] Divide-conquer: 1 min explanation with recurrence
- [ ] Binary search: 1 min explanation including answer space
- [ ] Total time for all 4: _______ min (should be < 10 min)

**Pattern Decision Matrix (30 min)**
- [ ] Given 8 problems, classify each to pattern:
  - [ ] Problem 1: Pattern ____________ (confidence: %)
  - [ ] Problem 2: Pattern ____________ (confidence: %)
  - [ ] ... continue for 8 total
  - [ ] Accuracy: ____ / 8 correct

### Afternoon Session (2 hours)

**Integration Problems (120 min)**
- [ ] Pick 2 problems that mix patterns
  - [ ] Problem A: ____________
    - [ ] Patterns needed: _______ + ________
    - [ ] Time: ____ min (first attempt)
    - [ ] After discussion: ✓
  
  - [ ] Problem B: ____________
    - [ ] Patterns needed: _______ + ________
    - [ ] Time: ____ min (first attempt)
    - [ ] After discussion: ✓

### Evening Session (1 hour)

**Weekly Reflection**
- [ ] Most confident pattern: ______________
- [ ] Pattern needing more practice: ______________
- [ ] Pattern combination that clicked: ____ + ____
- [ ] Real system I'd apply Week 04 to: ______________
- [ ] One insight I'll remember forever: _________________

---

## 🎯 MASTERY VERIFICATION

**By end of Week 04, validate:**

- [ ] **Two-Pointer:** Code merge, remove duplicates without reference (2/2)
- [ ] **Sliding Fixed:** Code running sum, max-in-window without reference (2/2)
- [ ] **Sliding Variable:** Code longest-substring, at-most-K without reference (2/2)
- [ ] **Divide-Conquer:** Code merge sort, majority element without reference (2/2)
- [ ] **Binary Search:** Code classic, answer-space without reference (2/2)

**Total: 4 patterns × 2-3 problems each = 10-12 problems this week**

**Final Assessment:**
- [ ] Can identify pattern instantly (<5 sec) for any problem
- [ ] Can code any pattern without hesitation
- [ ] Can combine patterns for complex problems
- [ ] Understand time/space tradeoffs
- [ ] Ready for interviews? ☐ YES

---

## 📝 Notes Section

**Breakthroughs this week:**
- _____________________________
- _____________________________
- _____________________________

**Areas to review before interview:**
- _____________________________
- _____________________________

**Favorite pattern explanation to teach others:**
- _____________________________

---

**Status:** Week 04 Daily Checklist Complete  
**Total Time Investment:** 15-18 hours (core)  
**Pattern Mastery Level:** ______ / 100  
**Ready for Week 05?** ☐ YES (Move forward) ☐ REVIEW (One more day)
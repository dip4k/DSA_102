# ✅ Week 04: Daily Progress Checklist & Milestone Tracking

**Purpose:** Track learning progress; identify gaps; ensure mastery  
**Use:** Check off daily; review at week end  
**Duration:** 5 days + 2 review days

---

## PRE-WEEK PREPARATION (Before Day 1)

**Prerequisites Review (Do these if unsure):**
- ☐ Review Week 1 Day 3: Recursion and call stack
- ☐ Review Week 2 Day 4: Binary search on sorted arrays
- ☐ Review Week 3 Day 3: Hash maps and collision handling
- ☐ Ensure you can do: Array indexing, pointer arithmetic, Big-O analysis

**Setup:**
- ☐ Have a notebook or document for tracing examples
- ☐ Set up LeetCode account (or preferred problem platform)
- ☐ Download all 5 instructional files
- ☐ Have a quiet space for 4-5 hours of focused study

**Mental Preparation:**
- ☐ Understand this week is foundational (patterns reused everywhere)
- ☐ Expect to spend 20 hours (4-5 hours per day) on Week 04
- ☐ Commit to not looking at solutions until you're stuck 20+ minutes
- ☐ Plan to revisit hard problems if you don't understand them first time

---

## DAY 1: TWO-POINTER PATTERNS

### Reading Phase (1-1.5 hours)

**Instructional File: Week_04_Day_01_Two_Pointer_Patterns_Instructional.md**

- ☐ Read Chapter 1: Context & Motivation (understand the problems)
- ☐ Read Chapter 2: Building Mental Model (understand the insight)
- ☐ Visualize the diagrams (same-direction vs. opposite-direction)
- ☐ Understand the two key patterns:
  - ☐ Same-direction pointers (both moving right)
  - ☐ Opposite-direction pointers (moving toward each other)
- ☐ Understand invariants (what's guaranteed at each step?)

**Key Concepts Acquired:**
- ☐ Synchronized pointer movement
- ☐ Invariant maintenance as a principle
- ☐ Why O(1) space is possible (no hash maps needed)

### Mechanics Phase (0.5-1 hour)

- ☐ Read Chapter 3: Mechanics & Implementation
- ☐ Trace through "Merge two sorted arrays" example step-by-step
  - ☐ Write out the trace table yourself
  - ☐ Understand decision logic (which pointer advances?)
- ☐ Trace through "Container with most water" example
  - ☐ Understand why shrinking from smaller side is optimal
- ☐ Read Chapter 4: Real-world systems
  - ☐ PostgreSQL merge joins (understand the scale)
  - ☐ Why two-pointer beats hash maps in production

### Integration Phase (0.5 hour)

- ☐ Read Chapter 5: Integration & Mastery
- ☐ Review 5 cognitive lenses (understand deeper)
- ☐ Read misconceptions section
- ☐ Review interview questions (don't answer yet, just understand difficulty)

### Practice Phase (2-2.5 hours)

**LeetCode Problems (8 total):**
1. ☐ **Easy:** Merge Sorted Array (LeetCode 88)
   - Time: 15-20 mins
   - Difficulty: Should be quick
   - Goal: Understand basic two-pointer merging

2. ☐ **Easy:** Remove Duplicates from Sorted Array (LeetCode 26)
   - Time: 15-20 mins
   - Difficulty: Should be quick
   - Goal: Understand slow/fast pointer pattern

3. ☐ **Easy:** Valid Palindrome (LeetCode 125) — optional, use if time
   - Time: 15-20 mins
   - Difficulty: Easy
   - Goal: Opposite-direction pointers

4. ☐ **Medium:** Container With Most Water (LeetCode 11)
   - Time: 30-40 mins
   - Difficulty: Medium (requires optimization insight)
   - Goal: Understand why shrink smaller side

5. ☐ **Medium:** Two Sum II (LeetCode 167)
   - Time: 20-30 mins
   - Difficulty: Medium
   - Goal: Opposite-direction optimization

6-8: ☐ Additional Medium problems (your choice from LeetCode 2-pointer tag)

**Practice Guidelines:**
- ☐ Don't look at solution until 20+ mins of thinking
- ☐ After solving, review solution to understand alternative approaches
- ☐ For each problem: Can you explain the invariant?
- ☐ For each problem: Can you identify the pattern (same vs. opposite)?

### Milestone Check (End of Day 1)

**You should now be able to:**
- ☐ Recognize two-pointer patterns in problem statements
- ☐ Identify same-direction vs. opposite-direction scenarios
- ☐ Implement two-pointer algorithms from scratch
- ☐ Explain why two-pointers is O(1) space
- ☐ Trace through examples and verify correctness
- ☐ Solve 4/5 medium problems without solution hints

**If you can't do these, spend 30 mins reviewing Chapter 2 and retrying problems.**

---

## DAY 2: SLIDING WINDOW (FIXED SIZE)

### Reading Phase (1-1.5 hours)

**Instructional File: Week_04_Day_02_Sliding_Window_Fixed_Size_Instructional.md**

- ☐ Read Chapter 1-2 (understand motivation and mental model)
- ☐ Visualize fixed-size windows (k-length windows sliding)
- ☐ Understand the key transition: fixed k means window moves in lockstep
- ☐ Understand monotonic deque concept (maintains ordering)
- ☐ Understand amortization (why O(1) per slide despite deque operations)

**Key Concepts Acquired:**
- ☐ Fixed-size window movement
- ☐ Incremental update vs. recalculation
- ☐ Monotonic deque for max/min problems
- ☐ Amortized analysis intuition

### Mechanics Phase (1 hour)

- ☐ Read Chapter 3: Mechanics & Implementation
- ☐ Trace through "Running sum of k-length subarrays"
  - ☐ Write initial window
  - ☐ Write slide-by-one updates
  - ☐ Verify O(n) complexity
- ☐ Trace through "Maximum in sliding window" with deque
  - ☐ Understand deque initialization
  - ☐ Understand when to pop front (outside window)
  - ☐ Understand when to pop back (smaller elements)
  - ☐ Verify monotonic decreasing property

### Integration Phase (0.5 hour)

- ☐ Read Chapter 4-5 (real systems and integration)
- ☐ Understand TradingView use case (200x speedup)
- ☐ Read misconceptions (don't confuse with variable window)

### Practice Phase (2-2.5 hours)

**LeetCode Problems (8 total):**
1. ☐ **Easy:** Implement Circular Queue (basic window concept)
   - Time: 20 mins

2. ☐ **Medium:** Moving Average (LeetCode 346 or similar)
   - Time: 20-30 mins
   - Difficulty: Medium
   - Goal: Incremental sum updates

3. ☐ **Medium:** Maximum in Sliding Window (LeetCode 239)
   - Time: 40-50 mins
   - Difficulty: Medium-Hard (deque is tricky)
   - Goal: Deque mechanics, monotonic property
   - ☐ Optional: First solve with heap O(n log k), then optimize to deque O(n)

4. ☐ **Medium:** First Unique Character in Stream
   - Time: 30 mins
   - Goal: Window + frequency tracking

5. ☐ Additional problems of your choice (similar difficulty)

**Practice Guidelines:**
- ☐ For maximum in window: implement carefully; trace first 5-6 iterations
- ☐ Understand why deque solution beats heap in practice
- ☐ For each problem: identify what incremental update saves computation

### Milestone Check (End of Day 2)

**You should now be able to:**
- ☐ Recognize fixed-size window problems
- ☐ Implement basic fixed-window algorithms (sums, averages)
- ☐ Implement monotonic deque for max/min
- ☐ Understand amortization (each element in/out once = O(2n) total)
- ☐ Choose between deque, heap, and other data structures
- ☐ Solve 3-4 medium problems without major hints

**If struggling with deque, spend 30 mins on Chapter 3 trace examples.**

---

## DAY 3: SLIDING WINDOW (VARIABLE SIZE)

### Reading Phase (1-1.5 hours)

**Instructional File: Week_04_Day_03_Sliding_Window_Variable_Size_Instructional.md**

- ☐ Read Chapters 1-2 (understand constraint satisfaction)
- ☐ Visualize expanding and shrinking (major difference from fixed window)
- ☐ Understand state machine: VALID (expand right) vs. INVALID (shrink left)
- ☐ Understand "at most K" pattern (the most common use case)
- ☐ Understand why still O(n): each element enters/exits once

**Key Concepts Acquired:**
- ☐ Dynamic constraint satisfaction
- ☐ Expand/shrink decision logic
- ☐ Frequency maps for constraint checking
- ☐ Amortized analysis (again, reinforced)

### Mechanics Phase (1-1.5 hours)

- ☐ Read Chapter 3: Mechanics & Implementation
- ☐ Trace "At most K distinct characters" carefully
  - ☐ Go through step-by-step
  - ☐ Understand when to shrink (distinct count > K)
  - ☐ Verify amortization
- ☐ Trace "Minimum window substring" (complex variant)
  - ☐ Understand two-counter approach (have vs. need)
  - ☐ Understand contraction phase

### Integration Phase (0.5 hour)

- ☐ Read Chapter 4-5
- ☐ Understand browser LRU cache use case
- ☐ Understand TCP congestion control (AIMD)
- ☐ Review misconceptions (off-by-one, constraint complexity)

### Practice Phase (2-2.5 hours)

**LeetCode Problems (8 total):**
1. ☐ **Medium:** Longest Substring Without Repeating (LeetCode 3)
   - Time: 30-40 mins
   - Difficulty: Medium
   - Goal: Basic variable window pattern

2. ☐ **Medium:** Longest Substring with At Most K Distinct (LeetCode 340)
   - Time: 30-40 mins
   - Difficulty: Medium
   - Goal: Classic "at most K" pattern

3. ☐ **Hard:** Minimum Window Substring (LeetCode 76)
   - Time: 50-60 mins
   - Difficulty: Hard (complex constraint)
   - Goal: Advanced variable window with conditions

4. ☐ **Medium:** Permutation in String (LeetCode 567)
   - Time: 30-40 mins
   - Goal: Window with frequency comparison

5. ☐ Additional Medium-Hard problems

**Practice Guidelines:**
- ☐ For each problem: Write down the constraint in plain English
- ☐ Identify when to expand vs. shrink
- ☐ Verify amortization (trace showing each element enters/exits)
- ☐ Minimum window substring is hard; okay if you need solution hints

### Milestone Check (End of Day 3)

**You should now be able to:**
- ☐ Recognize variable-window problems (especially "at most K")
- ☐ Design expand/shrink state machine logic
- ☐ Maintain frequency maps for constraint checking
- ☐ Explain why variable windows are still O(n) amortized
- ☐ Solve 3-4 medium problems
- ☐ Attempt hard variants (may need hints)

**If stuck on state machine, review Chapter 2 formalism.**

---

## DAY 4: DIVIDE & CONQUER

### Reading Phase (1-1.5 hours)

**Instructional File: Week_04_Day_04_Divide_and_Conquer_Pattern_Instructional.md**

- ☐ Read Chapters 1-2 (understand recursion and decomposition)
- ☐ Visualize recursion tree (understand problem breakdown)
- ☐ Understand "optimal substructure" (key requirement)
- ☐ Understand recurrence relations T(n) = a·T(n/b) + f(n)
- ☐ Understand Master Theorem (formula for complexity)

**Key Concepts Acquired:**
- ☐ Recursive problem decomposition
- ☐ Optimal substructure concept
- ☐ Recurrence relations
- ☐ Master Theorem application
- ☐ Merge operation (O(n) for merging sorted arrays)

### Mechanics Phase (1-1.5 hours)

- ☐ Read Chapter 3: Mechanics & Implementation
- ☐ Trace merge sort completely (important!)
  - ☐ Understand divide (split array in half)
  - ☐ Understand conquer (recursive calls)
  - ☐ Understand combine (merge two sorted halves)
  - ☐ Trace levels and why it's O(n log n)
- ☐ Trace counting inversions
  - ☐ Understand how inversions counted during merge

### Integration Phase (0.5 hour)

- ☐ Read Chapter 4-5
- ☐ Understand MapReduce at scale
- ☐ Understand database external sorting
- ☐ Review Master Theorem (know the formula)

### Practice Phase (2-2.5 hours)

**LeetCode Problems (8 total):**
1. ☐ **Easy:** Merge Two Sorted Lists (LeetCode 21)
   - Time: 20 mins
   - Goal: Understand merge operation

2. ☐ **Medium:** Merge k Sorted Lists (LeetCode 23)
   - Time: 40-50 mins
   - Difficulty: Medium-Hard
   - Goal: Extend merging to multiple lists

3. ☐ **Medium:** Sort an Array (implement merge sort, LeetCode 912)
   - Time: 50-60 mins
   - Difficulty: Medium
   - Goal: Full merge sort implementation
   - ☐ Trace through code carefully
   - ☐ Verify base case, split, merge

4. ☐ **Medium:** Majority Element (LeetCode 169)
   - Time: 30-40 mins
   - Difficulty: Medium
   - Goal: Divide & conquer pattern (alternative to hash map/voting)

5. ☐ Additional problems (inversion counting, etc.)

**Practice Guidelines:**
- ☐ For merge sort: implement and test on [8, 3, 5, 4, 2]
- ☐ Verify complexity by counting merge levels
- ☐ Don't memorize; understand the structure
- ☐ Could implement quicksort as alternative

### Milestone Check (End of Day 4)

**You should now be able to:**
- ☐ Understand recursion and call stack (from Week 1, reinforced)
- ☐ Recognize divide & conquer problems (sorting, inversions, optimal substructure)
- ☐ Implement merge sort from scratch
- ☐ Trace recurrence relations and apply Master Theorem
- ☐ Understand why D&C beats naive for certain problems
- ☐ Solve 3-4 medium problems

**If struggling with recursion, revisit Week 1 Day 3.**

---

## DAY 5: BINARY SEARCH ON ANSWERS

### Reading Phase (1-1.5 hours)

**Instructional File: Week_04_Day_05_Binary_Search_as_Pattern_Instructional.md**

- ☐ Read Chapters 1-2 (understand optimization via feasibility)
- ☐ Understand key difference: searching answer space, not array
- ☐ Visualize monotonic feasibility boundary
- ☐ Understand Low/High represent candidate answers, not array indices
- ☐ Understand feasibility function (custom logic for "is X achievable?")

**Key Concepts Acquired:**
- ☐ Answer space vs. input array search
- ☐ Monotonic feasibility property (critical requirement)
- ☐ Custom feasibility checkers
- ☐ Minimization vs. maximization problems
- ☐ O(log(range) × cost) complexity

### Mechanics Phase (1-1.5 hours)

- ☐ Read Chapter 3: Mechanics & Implementation
- ☐ Trace "Machine Scheduling" example carefully
  - ☐ Understand answer space: [max_job_time, sum_all_jobs]
  - ☐ Understand feasibility: can we complete all jobs in time T with m machines?
  - ☐ Understand greedy assignment for feasibility check
- ☐ Trace "Aggressive Cows" example
  - ☐ Understand maximization (opposite direction from scheduling)
  - ☐ Understand distance-based feasibility

### Integration Phase (0.5 hour)

- ☐ Read Chapter 4-5
- ☐ Understand Kubernetes scheduling use case
- ☐ Understand ride-sharing wait time prediction
- ☐ Review when binary search on answers applies

### Practice Phase (2-2.5 hours)

**LeetCode Problems (8 total):**
1. ☐ **Medium:** Koko Eating Bananas (LeetCode 875)
   - Time: 30-40 mins
   - Difficulty: Medium
   - Goal: Minimize eating speed

2. ☐ **Medium:** Capacity To Ship Packages (LeetCode 1011)
   - Time: 30-40 mins
   - Difficulty: Medium
   - Goal: Minimize capacity

3. ☐ **Hard:** Aggressive Cows (LeetCode variants)
   - Time: 40-50 mins
   - Difficulty: Hard
   - Goal: Maximize minimum distance

4. ☐ **Hard:** Book Allocation or Painter's Partition
   - Time: 50-60 mins
   - Difficulty: Hard
   - Goal: Complex feasibility check

5. ☐ Additional problems (minimizing/maximizing with constraints)

**Practice Guidelines:**
- ☐ For each problem: Identify answer space bounds
- ☐ Verify monotonicity (crucial!)
- ☐ Implement feasibility checker separately; test it first
- ☐ Then apply binary search wrapper
- ☐ Common mistake: wrong bounds; always verify bounds include the answer

### Milestone Check (End of Day 5)

**You should now be able to:**
- ☐ Recognize optimization problems suitable for binary search
- ☐ Identify monotonic feasibility boundaries
- ☐ Set correct answer space bounds
- ☐ Implement custom feasibility checkers
- ☐ Apply binary search on answer space
- ☐ Solve 2-3 medium problems
- ☐ Understand (but maybe not solve) hard variants

**If struggling with monotonicity verification, spend extra time on this—it's critical.**

---

## WEEK-END REVIEW (Days 6-7)

### Saturday: Pattern Integration (3-4 hours)

- ☐ Review comparison matrix (Week 04 Key Concepts Summary)
  - ☐ Compare all 5 patterns side-by-side
  - ☐ Understand trade-offs
- ☐ Solve mixed problems (identify pattern first)
  - ☐ 3-4 problems from different patterns
  - ☐ Goal: recognize pattern before diving into solution
- ☐ Review misconceptions (all 20 of them)
  - ☐ Did you make any of these mistakes?
  - ☐ How will you avoid them next time?

**Sample Mixed Problems:**
1. Merge sorted arrays (Two-Pointer)
2. Longest substring without repeating (Variable Window)
3. Merge sort (Divide & Conquer)
4. Moving average (Fixed Window)
5. Machine scheduling (Binary Search)

### Sunday: Deep Dives (3-4 hours)

- ☐ Review your weakest pattern
  - ☐ Re-read the instructional chapter
  - ☐ Solve 2-3 problems you found hard
  - ☐ Understand the "aha" moment
- ☐ Attempt 2-3 hard problems (may need hints)
  - ☐ Minimum window substring (hard variable window)
  - ☐ Counting inversions (hard D&C)
  - ☐ Book allocation (hard binary search)
- ☐ Mock interview (30 mins, untimed)
  - ☐ Solve 1 hard problem from any pattern
  - ☐ Explain your approach before coding
  - ☐ Trace through example

---

## OVERALL PROGRESS CHECKLIST

### By End of Week 04, You Should:

**Pattern Recognition:**
- ☐ Identify two-pointer patterns (same vs. opposite direction)
- ☐ Identify fixed-size window patterns (running metrics, bounded subarrays)
- ☐ Identify variable-window patterns (at-most-K, constraint-based)
- ☐ Identify divide & conquer patterns (sorting, inversions, optimal substructure)
- ☐ Identify binary search on answer patterns (minimize/maximize with feasibility)

**Implementation:**
- ☐ Implement two-pointer algorithms without memorization
- ☐ Implement fixed-size sliding windows with deque optimization
- ☐ Implement variable-size sliding windows with frequency maps
- ☐ Implement divide & conquer recursion correctly
- ☐ Implement binary search on answer space with feasibility checkers

**Analysis:**
- ☐ Analyze complexity for each pattern
- ☐ Understand amortization (Days 1-3)
- ☐ Understand recurrence relations and Master Theorem (Day 4)
- ☐ Understand monotonicity requirements (Day 5)
- ☐ Evaluate trade-offs (time, space, implementation complexity)

**Production Knowledge:**
- ☐ Explain real-world uses (PostgreSQL, Netflix, Google, Kubernetes, etc.)
- ☐ Understand scalability implications
- ☐ Connect patterns to large-scale systems

**Interview Readiness:**
- ☐ Answer 30+ interview questions with confidence
- ☐ Solve 40+ practice problems (varied difficulty)
- ☐ Recognize patterns in unfamiliar problems
- ☐ Explain approaches clearly before coding

---

## TROUBLESHOOTING GUIDE

**"I'm failing easy problems"**
- ☐ You might not understand the pattern fundamentals
- ☐ Action: Stop; re-read Chapter 2 of the instructional file
- ☐ Don't move forward until you pass easy problems consistently

**"I understand the pattern but can't code it"**
- ☐ Implementation details are tricky (off-by-one, index management)
- ☐ Action: Trace examples on paper first; write pseudocode before actual code
- ☐ Common: window boundaries, pointer positions, deque operations

**"I solve the problem but it times out"**
- ☐ You might be solving in O(n²) instead of O(n)
- ☐ Action: Check if you're leveraging the pattern correctly
- ☐ Did you optimize incremental updates? Use deque instead of array? Use binary search instead of iteration?

**"I don't understand amortization"**
- ☐ It's a tricky concept; requires different thinking
- ☐ Action: Think "total operations across all iterations" not "operations per iteration"
- ☐ Example: Each element enters/exits window once = 2n total operations = O(n) amortized

**"Binary search on answers is confusing"**
- ☐ It's a conceptual shift from array searching
- ☐ Action: Verify monotonicity first (before implementing)
- ☐ Practice on simpler problems (Koko eating bananas) before hard ones

**"I'm running out of time"**
- ☐ 20 hours might not be enough; okay to extend to 25-30 hours
- ☐ Better to master fewer patterns than rush through all 5
- ☐ Focus on Days 1-3 (sliding windows are 50% of interview problems)

---

## SUCCESS INDICATORS

**If you check all of these, you're ready for Week 05:**

- ☐ **Pattern Recognition:** Given any problem, you identify the pattern in < 2 mins
- ☐ **Implementation:** You code solutions without looking at references
- ☐ **Speed:** You solve medium problems in 20-30 mins (including trace time)
- ☐ **Correctness:** 90%+ of your solutions pass on first or second attempt
- ☐ **Explanation:** You can explain your approach before coding
- ☐ **Trade-offs:** You discuss pros/cons of your approach vs. alternatives
- ☐ **Interview Confidence:** You feel ready to discuss these patterns in an interview

---

**Week 04 is the foundation. Invest time here; it pays off massively in Weeks 5-15.**

**You've got this! 💪**


# 🎯 LEETCODE PRACTICE: WEEKS 1-3 COMPREHENSIVE PROBLEM LIST

**Scope:** Weeks 1-3 Foundations & Computational Thinking
**Problem Count:** 90+ problems
**Difficulty Range:** Easy → Medium → Hard  
**Categories:** 10 major patterns across 15 days
**Updated:** January 2026

---

## 📚 TABLE OF CONTENTS

1. **Week 1: Computational Fundamentals, Peak Finding & Asymptotics**
2. **Week 2: Linear Data Structures & Binary Search**
3. **Week 3: Sorting, Heaps & Hashing**
4. **Gatcha & Gotchas - Common Pitfalls**
5. **Pattern Recognition Guide**
6. **Tips & Tricks for Problem Solving**
7. **Interview Preparation Notes**

---

## ⚡ HOW TO USE THIS LIST

✅ **These are FOUNDATIONAL problems** (Build strong base before Weeks 4-6)
✅ **Solve in progression order** (Problems ordered by increasing difficulty)
✅ **Don't skip categories** (Each builds core understanding)
✅ **Focus on approach** (Understand WHY algorithms work)
✅ **Implement from scratch** (Don't just read solutions)

**Time Estimate:** 90+ problems × 15-30 min average = 22-45 hours of practice

---

---

## 🟦 WEEK 1: COMPUTATIONAL FUNDAMENTALS, PEAK FINDING & ASYMPTOTICS

### Overview
**Goal:** Understand how programs execute, analyze complexity, and think algorithmically
**Key Insight:** Complexity analysis separates fast from slow; smart thinking reduces complexity

---

## 📌 WEEK 1 | DAY 1-2: ASYMPTOTIC ANALYSIS & COMPLEXITY (Big-O, Big-Ω, Big-Θ)

### Category: Complexity Analysis & Algorithm Fundamentals

**Pattern Signals:** 
- Comparing algorithms for same problem
- Understanding scaling behavior
- Predicting performance as n grows

---

### 🟢 Easy (Start Here)

1. **Sum of Array Elements** [LeetCode - Basic]
   - Pattern: Identify O(n) complexity
   - Note: Linear scan through all elements
   - Exercise: Analyze time and space complexity

2. **Find Maximum Element** [LeetCode 2553]
   - Pattern: O(n) linear search
   - Note: Requires examining each element
   - Exercise: How much better than sorting?

3. **Binary Search** [LeetCode 704]
   - Pattern: O(log n) vs O(n)
   - Note: Compare with linear search on same problem
   - Exercise: Calculate speedup for n=10^6

4. **Count Elements** [LeetCode - Basic]
   - Pattern: O(n) counting
   - Note: Simple iteration pattern
   - Exercise: What if counting in chunks?

5. **Valid Perfect Square** [LeetCode 367]
   - Pattern: O(sqrt(n)) vs O(log n) vs O(n)
   - Note: Multiple algorithmic approaches exist
   - Exercise: Implement all three, compare

---

### 🟡 Medium

6. **Check If N and Its Double Exist** [LeetCode 1346]
   - Pattern: O(n) with set vs O(n²) naive
   - Note: Data structure choice affects complexity
   - Exercise: Hash set optimization

7. **Majority Element** [LeetCode 169]
   - Pattern: O(n) vs O(n log n) sorting
   - Note: Multiple approaches with different complexities
   - Exercise: Implement all approaches, analyze

8. **First Unique Character in a String** [LeetCode 387]
   - Pattern: O(n) with hash map
   - Note: Two-pass pattern
   - Exercise: Can you do it in single pass? Trade-offs?

9. **Valid Anagram** [LeetCode 242]
   - Pattern: O(n) with counting vs O(n log n) sorting
   - Note: Space vs time trade-off
   - Exercise: Compare approaches

10. **Two Sum** [LeetCode 1]
    - Pattern: O(n) with hash set vs O(n²) naive
    - Note: Hash table saves us from nested loops
    - Exercise: Implement both, measure improvement

11. **Contains Duplicate** [LeetCode 217]
    - Pattern: O(n) with set vs O(n²) nested loop
    - Note: Early termination on duplicate
    - Exercise: Best/worst case analysis

12. **Intersection of Two Arrays** [LeetCode 349]
    - Pattern: O(n + m) with sets vs O(nm) naive
    - Note: Set operations enable efficiency
    - Exercise: Complexity analysis with different sizes

---

### 🔴 Hard

13. **Find All Numbers Disappeared in Array** [LeetCode 448]
    - Pattern: O(n) space vs O(1) in-place solution
    - Note: In-place marking technique
    - Exercise: Understand in-place complexity benefits

14. **Median of Two Sorted Arrays** [LeetCode 4]
    - Pattern: O(log(min(m,n))) binary search
    - Note: Very different from O(n+m) merge
    - Exercise: Why is binary search possible here?

---

---

## 📌 WEEK 1 | DAY 3: SPACE COMPLEXITY & MEMORY USAGE

### Category: Space Analysis & Memory Efficiency

**Pattern Signals:**
- "O(1) extra space required"
- "Stack overflow on deep recursion"
- "Memory optimization needed"

---

### 🟢 Easy

15. **Reverse String** [LeetCode 344]
    - Pattern: O(1) space (in-place)
    - Note: Two-pointer avoids extra array
    - Exercise: Compare with O(n) space reversal

16. **Move Zeroes** [LeetCode 283]
    - Pattern: O(1) space in-place modification
    - Note: Maintain relative order
    - Exercise: Why swap vs create new array?

17. **Rotate Array** [LeetCode 189]
    - Pattern: O(1) space rotation (reversal trick)
    - Note: Clever space optimization
    - Exercise: Implement naive O(n) and optimal O(1)

18. **Palindrome Number** [LeetCode 9]
    - Pattern: O(1) space reversal vs O(n) string conversion
    - Note: Math vs string trade-off
    - Exercise: Which approach is more elegant?

---

### 🟡 Medium

19. **Remove Duplicates from Sorted Array** [LeetCode 26]
    - Pattern: O(1) space with two pointers
    - Note: Modify in-place, return count
    - Exercise: Why modify input when we could return new array?

20. **Remove Element** [LeetCode 27]
    - Pattern: O(1) space with two pointers
    - Note: Similar to remove duplicates
    - Exercise: Generalize to remove any value

21. **First Missing Positive** [LeetCode 41]
    - Pattern: O(n) space required (theoretical limit)
    - Note: In-place marking technique
    - Exercise: Why can't we do truly O(1)?

22. **Maximum Product Subarray** [LeetCode 152]
    - Pattern: O(1) space (two variables)
    - Note: Track max and min products
    - Exercise: Why both max and min needed?

23. **Longest Substring Without Repeating Characters** [LeetCode 3]
    - Pattern: O(k) space where k = alphabet size
    - Note: Hash map for character positions
    - Exercise: Can you do without hash map?

---

### 🔴 Hard

24. **Merge k Sorted Lists** [LeetCode 23]
    - Pattern: O(n) space for result
    - Note: Heap vs priority queue complexity
    - Exercise: Calculate space usage exactly

25. **Trapping Rain Water** [LeetCode 42]
    - Pattern: O(n) space vs O(1) two-pointer
    - Note: Trade-off between clarity and space
    - Exercise: Implement both approaches

---

---

## 📌 WEEK 1 | DAY 4-5: RECURSION & PATTERNS

### Category: Recursion, Memoization & Problem Decomposition

**Pattern Signals:**
- "Recursive structure visible"
- "Overlapping subproblems"
- "Call stack depth matters"

---

### 🟢 Easy

26. **Factorial** [LeetCode - Basic]
    - Pattern: Simple recursive base/recursive case
    - Note: n! = n × (n-1)!
    - Exercise: Trace call stack on n=5

27. **Power of Two** [LeetCode 231]
    - Pattern: Recursive vs bit manipulation vs iterative
    - Note: Multiple approaches exist
    - Exercise: Which is most elegant?

28. **Fibonacci Number** [LeetCode 509]
    - Pattern: O(2^n) naive vs O(n) memoization
    - Note: Classic overlapping subproblems case
    - Exercise: Implement naive, memoized, iterative

29. **Sum of Array** [LeetCode - Basic]
    - Pattern: Recursive summation
    - Note: Base case = empty array (0)
    - Exercise: Trace recursion tree

30. **Palindrome Linked List** [LeetCode 234]
    - Pattern: Recursive traversal + reversal
    - Note: Recursion naturally traverses lists
    - Exercise: Compare with iterative approach

31. **Symmetric Tree** [LeetCode 101]
    - Pattern: Recursive tree comparison
    - Note: Left and right subtrees mirror each other
    - Exercise: Call stack depth = tree height

32. **Merge Sorted Lists** [LeetCode 21]
    - Pattern: Recursive merging
    - Note: Merge two lists recursively
    - Exercise: When is recursion better than iteration?

---

### 🟡 Medium

33. **Reverse Linked List** [LeetCode 206]
    - Pattern: Recursive reversal
    - Note: Reverse rest, point back
    - Exercise: Stack depth = list length

34. **Flatten Nested List** [LeetCode 341]
    - Pattern: Recursive flattening
    - Note: Handle nested structures
    - Exercise: Depth-first traversal

35. **Generate Parentheses** [LeetCode 22]
    - Pattern: Recursive generation with constraints
    - Note: Track open/close counts
    - Exercise: Call tree structure

36. **Letter Combinations of Phone Number** [LeetCode 17]
    - Pattern: Recursive generation
    - Note: Cartesian product via recursion
    - Exercise: Number of calls in tree

37. **Coin Change** [LeetCode 322]
    - Pattern: Recursive with memoization
    - Note: DP but understand via recursion first
    - Exercise: Compare recursion vs iteration

38. **Word Break** [LeetCode 139]
    - Pattern: Recursive with memoization
    - Note: Substring checking
    - Exercise: Overlapping subproblems analysis

39. **Combination Sum** [LeetCode 39]
    - Pattern: Recursive backtracking
    - Note: Allow repeated elements
    - Exercise: Decision tree structure

40. **Permutations** [LeetCode 46]
    - Pattern: Recursive generation
    - Note: All orderings of elements
    - Exercise: n! permutations generated

---

### 🔴 Hard

41. **Expression Add Operators** [LeetCode 282]
    - Pattern: Recursive expression building
    - Note: Track computed value and last term
    - Exercise: Complex recursion state

42. **Recover Binary Search Tree** [LeetCode 99]
    - Pattern: Recursive tree traversal + state tracking
    - Note: In-order traversal properties
    - Exercise: State management in recursion

---

---

## 📌 WEEK 1 | DAY 6 (OPTIONAL): PEAK FINDING & ALGORITHMIC THINKING

### Category: Problem Decomposition & Strategy

**Pattern Signals:**
- "Find element with property X"
- "Exploit structure for efficiency"
- "Better than brute force exists"

---

### 🟢 Easy

43. **Find Peak Element** [LeetCode 162]
    - Pattern: Binary search for peak
    - Note: O(log n) vs O(n) linear scan
    - Exercise: Why is binary search possible?

44. **Search in Rotated Sorted Array** [LeetCode 33]
    - Pattern: Modified binary search
    - Note: Exploit partial sortedness
    - Exercise: Identify which half is sorted

45. **Find First and Last Position of Element** [LeetCode 34]
    - Pattern: Two binary searches
    - Note: Find boundaries
    - Exercise: How to find left/right edges

---

### 🟡 Medium

46. **Search in Rotated Sorted Array II** [LeetCode 81]
    - Pattern: Binary search with duplicates
    - Note: Duplicates complicate finding sorted half
    - Exercise: When does binary search fail?

47. **Find Minimum in Rotated Sorted Array** [LeetCode 153]
    - Pattern: Binary search for extreme value
    - Note: Minimum is "pivot" point
    - Exercise: Compare with peak finding

48. **Median of Two Sorted Arrays** [LeetCode 4]
    - Pattern: Binary search optimization
    - Note: O(log(min(m,n))) approach
    - Exercise: Understand partition-based approach

---

### 🔴 Hard

49. **Find in Mountain Array** [LeetCode 1095]
    - Pattern: Three-step peak finding
    - Note: Find peak, then search each side
    - Exercise: Binary search composition

50. **Find K Closest Elements** [LeetCode 658]
    - Pattern: Binary search with sliding window
    - Note: Find start position, take k elements
    - Exercise: Why binary search for start?

---

---

## 🟩 WEEK 2: LINEAR DATA STRUCTURES & BINARY SEARCH

---

## 📌 WEEK 2 | DAY 1-2: ARRAYS & DYNAMIC ARRAYS

### Category: Array Operations & Memory Management

**Pattern Signals:**
- "Contiguous memory allocation"
- "Resizing and amortization"
- "O(1) random access"

---

### 🟢 Easy

51. **Two Sum** [LeetCode 1]
    - Pattern: Array + hash map
    - Note: Exploit array indexing
    - Exercise: Why hash map needed?

52. **Best Time to Buy and Sell Stock** [LeetCode 121]
    - Pattern: Single pass, track minimum
    - Note: O(n) scan
    - Exercise: Why single pass works?

53. **Contains Duplicate** [LeetCode 217]
    - Pattern: Set for deduplication
    - Note: O(n) time, O(n) space
    - Exercise: Trade-offs

54. **Missing Number** [LeetCode 268]
    - Pattern: Sum formula vs XOR vs sorting
    - Note: Multiple O(n) approaches
    - Exercise: Compare space usage

55. **Majority Element** [LeetCode 169]
    - Pattern: O(n) with hash map or Boyer-Moore
    - Note: Element appearing > n/2 times
    - Exercise: Implement multiple approaches

---

### 🟡 Medium

56. **3Sum** [LeetCode 15]
    - Pattern: Sort + two pointers + deduplication
    - Note: Find all triplets summing to 0
    - Exercise: How to handle duplicates?

57. **Container With Most Water** [LeetCode 11]
    - Pattern: Two pointers from ends
    - Note: Area = min(height) × distance
    - Exercise: Why move shorter wall?

58. **Product of Array Except Self** [LeetCode 238]
    - Pattern: Prefix/suffix product technique
    - Note: O(n) time, O(1) space (excluding output)
    - Exercise: Two-pass left-to-right then right-to-left

59. **Rotate Array** [LeetCode 189]
    - Pattern: Three reversals for O(1) space
    - Note: In-place array rotation
    - Exercise: Reversal technique clarity

60. **Set Matrix Zeroes** [LeetCode 73]
    - Pattern: Mark in-place with O(1) space
    - Note: Don't use extra array
    - Exercise: Use first row/col as markers

---

### 🔴 Hard

61. **Trapping Rain Water** [LeetCode 42]
    - Pattern: Two pointers or precomputation
    - Note: O(n) with O(1) space variant
    - Exercise: Understand water calculation

62. **Largest Rectangle in Histogram** [LeetCode 84]
    - Pattern: Stack-based approach
    - Note: O(n) with monotonic stack
    - Exercise: Span calculation

63. **Median of Two Sorted Arrays** [LeetCode 4]
    - Pattern: Binary search or merge
    - Note: O(log(min(m,n))) optimal
    - Exercise: Partition-based thinking

---

---

## 📌 WEEK 2 | DAY 3: LINKED LISTS

### Category: Pointer Manipulation & Linked List Operations

**Pattern Signals:**
- "Linked list traversal"
- "Node insertion/deletion"
- "Pointer management"

---

### 🟢 Easy

64. **Reverse Linked List** [LeetCode 206]
    - Pattern: Iterative pointer reversal
    - Note: Three pointers (prev, curr, next)
    - Exercise: Trace pointer changes

65. **Palindrome Linked List** [LeetCode 234]
    - Pattern: Find middle, reverse, compare
    - Note: Fast/slow pointers find middle
    - Exercise: Multi-step approach

66. **Merge Two Sorted Lists** [LeetCode 21]
    - Pattern: Two pointer merge
    - Note: Compare and advance
    - Exercise: Handle empty lists

67. **Remove Duplicates from Sorted List** [LeetCode 83]
    - Pattern: Single traversal deduplication
    - Note: Modify pointers to skip
    - Exercise: Handle consecutive duplicates

68. **Linked List Cycle** [LeetCode 141]
    - Pattern: Fast/slow pointer cycle detection
    - Note: Floyd's algorithm
    - Exercise: Why does it work?

---

### 🟡 Medium

69. **Remove Nth Node From End of List** [LeetCode 19]
    - Pattern: Fast pointer gap technique
    - Note: Dummy node for head removal
    - Exercise: One-pass with dummy

70. **Reorder List** [LeetCode 143]
    - Pattern: Three steps (find middle, reverse, merge)
    - Note: L1→L2→...Ln becomes L1→Ln→L2→Ln-1
    - Exercise: Combine three techniques

71. **LRU Cache** [LeetCode 146]
    - Pattern: Doubly linked list + hash map
    - Note: O(1) operations
    - Exercise: Order management

72. **Linked List Cycle II** [LeetCode 142]
    - Pattern: Cycle detection and start finding
    - Note: Math-based pointer positioning
    - Exercise: Why does math work?

73. **Intersection of Two Linked Lists** [LeetCode 160]
    - Pattern: Two pointer length equalization
    - Note: Move to other list when reaching end
    - Exercise: Why does this find intersection?

---

### 🔴 Hard

74. **Reverse Nodes in k-Group** [LeetCode 25]
    - Pattern: Recursive or iterative grouping
    - Note: Reverse k nodes at a time
    - Exercise: Pointer management complexity

75. **Copy List with Random Pointer** [LeetCode 138]
    - Pattern: Hash map + deep copy
    - Note: Handle random pointers
    - Exercise: Two-pass approach

76. **Flatten Multilevel Doubly Linked List** [LeetCode 430]
    - Pattern: Recursive depth-first traversal
    - Note: Flatten nested levels
    - Exercise: Pointer reconnection

---

---

## 📌 WEEK 2 | DAY 4: STACKS, QUEUES & DEQUES

### Category: LIFO/FIFO Data Structures

**Pattern Signals:**
- "Last-in-first-out behavior"
- "First-in-first-out behavior"
- "Push/pop operations"

---

### 🟢 Easy

77. **Valid Parentheses** [LeetCode 20]
    - Pattern: Stack for bracket matching
    - Note: Push open, pop on close
    - Exercise: Handle multiple bracket types

78. **Implement Queue Using Stacks** [LeetCode 232]
    - Pattern: Two stacks for FIFO
    - Note: Simulate queue with stacks
    - Exercise: Amortized complexity

79. **Implement Stack Using Queues** [LeetCode 225]
    - Pattern: Two queues or single queue with rotation
    - Note: Simulate stack with queues
    - Exercise: Both approaches

80. **Min Stack** [LeetCode 155]
    - Pattern: Auxiliary stack for O(1) min
    - Note: Track minimum at each level
    - Exercise: Why parallel stack?

81. **Backspace String Compare** [LeetCode 844]
    - Pattern: Stack for backspace simulation
    - Note: Build final strings with stack
    - Exercise: Or reverse two-pointer approach

---

### 🟡 Medium

82. **Evaluate Reverse Polish Notation** [LeetCode 150]
    - Pattern: Stack for postfix evaluation
    - Note: Numbers pushed, operators applied
    - Exercise: Different operator implementations

83. **Daily Temperatures** [LeetCode 739]
    - Pattern: Monotonic decreasing stack
    - Note: Find next greater temperature
    - Exercise: Stack invariant maintenance

84. **Next Greater Element I** [LeetCode 496]
    - Pattern: Monotonic stack for lookups
    - Note: Find next greater element
    - Exercise: Hash map for indexing

85. **Largest Rectangle in Histogram** [LeetCode 84]
    - Pattern: Monotonic stack with spans
    - Note: O(n) bar-wise approach
    - Exercise: Rectangle area calculation

86. **Trapping Rain Water** [LeetCode 42]
    - Pattern: Stack-based approach
    - Note: Alternative to two-pointer
    - Exercise: Compare with two-pointer

---

### 🔴 Hard

87. **Remove K Digits** [LeetCode 402]
    - Pattern: Monotonic stack construction
    - Note: Greedy digit selection
    - Exercise: Lexicographically smallest

88. **Maximal Rectangle** [LeetCode 85]
    - Pattern: Stack from histogram approach
    - Note: Per-row histogram + largest rectangle
    - Exercise: DP row computation

---

---

## 📌 WEEK 2 | DAY 5: BINARY SEARCH & INVARIANTS

### Category: Binary Search Variants & Correctness

**Pattern Signals:**
- "Sorted array searching"
- "Find boundary/extreme"
- "Logarithmic speedup"

---

### 🟢 Easy

89. **Binary Search** [LeetCode 704]
    - Pattern: Classic binary search
    - Note: mid = left + (right - left) / 2
    - Exercise: Loop invariant proof

90. **Search Insert Position** [LeetCode 35]
    - Pattern: Binary search with insertion
    - Note: Find position even if not present
    - Exercise: Boundary handling

91. **Valid Perfect Square** [LeetCode 367]
    - Pattern: Binary search on value space
    - Note: Find square root via binary search
    - Exercise: Why better than O(sqrt(n))?

92. **First Bad Version** [LeetCode 278]
    - Pattern: Binary search on answer space
    - Note: Monotonic true/false property
    - Exercise: Feasibility function

---

### 🟡 Medium

93. **Find Peak Element** [LeetCode 162]
    - Pattern: Binary search without full sorted array
    - Note: Use local properties
    - Exercise: Why binary search works

94. **Search in Rotated Sorted Array** [LeetCode 33]
    - Pattern: Binary search on partially sorted
    - Note: Identify which half is sorted
    - Exercise: Case analysis for rotation

95. **Find Minimum in Rotated Sorted Array** [LeetCode 153]
    - Pattern: Binary search for extreme value
    - Note: Minimum is the "pivot"
    - Exercise: Distinguish from peak

96. **Koko Eating Bananas** [LeetCode 875]
    - Pattern: Binary search on answer
    - Note: Find minimum eating speed
    - Exercise: Feasibility check with time calculation

97. **Magnetic Force Between Two Balls** [LeetCode 1552]
    - Pattern: Binary search on distance
    - Note: Maximize minimum distance
    - Exercise: Placement feasibility check

---

### 🔴 Hard

98. **Median of Two Sorted Arrays** [LeetCode 4]
    - Pattern: Binary search on partition
    - Note: O(log(min(m,n))) approach
    - Exercise: Partition correctness proof

99. **Find the Smallest Divisor Given a Threshold** [LeetCode 1283]
    - Pattern: Binary search on divisor
    - Note: Find smallest divisor for sum constraint
    - Exercise: Sum calculation with rounding

---

---

## 🟧 WEEK 3: SORTING, HEAPS & HASHING

---

## 📌 WEEK 3 | DAY 1-2: SORTING ALGORITHMS

### Category: Sorting & Comparison-Based Algorithms

**Pattern Signals:**
- "Sort the array"
- "Order matters"
- "O(n²) or O(n log n) acceptable?"

---

### 🟢 Easy

100. **Majority Element** [LeetCode 169]
     - Pattern: Sorting approach to find majority
     - Note: Element appearing > n/2 times
     - Exercise: Compare with hash map approach

101. **Valid Anagram** [LeetCode 242]
     - Pattern: Sort both strings and compare
     - Note: O(n log n) approach
     - Exercise: Compare with frequency counting

102. **Missing Number** [LeetCode 268]
     - Pattern: Sorting approach
     - Note: Check for gap in sorted array
     - Exercise: Compare with XOR and sum approaches

103. **Maximum Frequency Stack** [LeetCode - Basic]
     - Pattern: Sorting frequencies
     - Note: Use heap or sort
     - Exercise: Implementation options

104. **Sort Matrix Diagonally** [LeetCode 1329]
     - Pattern: Sort diagonals
     - Note: Group and sort by diagonal
     - Exercise: Indexing by i-j

---

### 🟡 Medium

105. **Sort Array by Parity** [LeetCode 905]
     - Pattern: In-place partitioning
     - Note: Even and odd separation
     - Exercise: Compare with dedicated sort

106. **Merge Intervals** [LeetCode 56]
     - Pattern: Sort by start, then merge
     - Note: Overlap detection
     - Exercise: Why sort first?

107. **Meeting Rooms** [LeetCode 252]
     - Pattern: Sort intervals and check overlaps
     - Note: Can attend all meetings?
     - Exercise: Adjacent interval checking

108. **K Closest Points to Origin** [LeetCode 973]
     - Pattern: Sort by distance
     - Note: Find k closest points
     - Exercise: Distance calculation and sorting

109. **Top K Frequent Elements** [LeetCode 347]
     - Pattern: Sort by frequency (heap faster)
     - Note: Find k most frequent elements
     - Exercise: Compare sorting vs heap

110. **Sort Array by Increasing Frequency** [LeetCode 1636]
     - Pattern: Sort by frequency then value
     - Note: Multi-key sorting
     - Exercise: Custom comparator

---

### 🔴 Hard

111. **Largest Number** [LeetCode 179]
     - Pattern: Custom sorting of numbers
     - Note: Arrange to form largest number
     - Exercise: Custom comparator (ab vs ba)

112. **The Skyline Problem** [LeetCode 218]
     - Pattern: Sort events and maintain structures
     - Note: Sweep line algorithm
     - Exercise: Event processing

---

---

## 📌 WEEK 3 | DAY 1-2: MERGE SORT & QUICKSORT

### Category: Divide & Conquer Sorting

**Pattern Signals:**
- "O(n log n) guaranteed"
- "Merge sort" for stability
- "Quicksort" for average case

---

### 🟢 Easy

113. **Sort an Array** [LeetCode 912]
     - Pattern: Implement merge sort or quicksort
     - Note: Guarantee O(n log n)
     - Exercise: Implement both algorithms

114. **Merge Sorted Array** [LeetCode 88]
     - Pattern: Two-pointer merge
     - Note: Merge two sorted arrays
     - Exercise: In-place backwards merging

115. **Merge Sorted Lists** [LeetCode 21]
     - Pattern: Merge two sorted lists
     - Note: Linked list merging
     - Exercise: Compare with array merging

---

### 🟡 Medium

116. **Sort List** [LeetCode 148]
     - Pattern: Merge sort on linked list
     - Note: O(n log n) without O(n) space (sort in place)
     - Exercise: Why merge sort for linked lists?

117. **Kth Largest Element in an Array** [LeetCode 215]
     - Pattern: Quicksort-based selection
     - Note: O(n) average via quickselect
     - Exercise: Compare with heap and sorting

118. **Closest Pair of Points** [LeetCode - Custom]
     - Pattern: Divide & conquer for closest pair
     - Note: O(n log n) approach
     - Exercise: Midpoint handling

---

### 🔴 Hard

119. **The Skyline Problem** [LeetCode 218]
     - Pattern: Event-based sweep with sorting
     - Note: Complex merging and maintenance
     - Exercise: Event ordering strategy

120. **Reverse Pairs** [LeetCode 493]
     - Pattern: Merge sort with counting
     - Note: Count inversions during sort
     - Exercise: Count at merge step

---

---

## 📌 WEEK 3 | DAY 3: HEAPS & HEAP OPERATIONS

### Category: Heap Data Structure & Priority Queue

**Pattern Signals:**
- "Find k largest/smallest"
- "Online/streaming maximum/minimum"
- "Priority-based processing"

---

### 🟢 Easy

121. **Last Stone Weight** [LeetCode 1046]
     - Pattern: Max heap simulation
     - Note: Repeatedly pop two largest
     - Exercise: Implement with heap

122. **Kth Largest Element in a Stream** [LeetCode 703]
     - Pattern: Min heap of size k
     - Note: Maintain k largest elements
     - Exercise: O(log k) operations

123. **Min Heap Implementation** [LeetCode - Basic]
     - Pattern: Basic heap operations
     - Note: Insert, delete, heapify
     - Exercise: Parent/child indexing

124. **Max Heap Conversion** [LeetCode - Basic]
     - Pattern: Convert array to heap
     - Note: Build heap in O(n)
     - Exercise: Heapify-down vs heapify-up

---

### 🟡 Medium

125. **Kth Largest Element in an Array** [LeetCode 215]
     - Pattern: Min heap of size k
     - Note: Find kth largest
     - Exercise: Compare heap vs quickselect

126. **Top K Frequent Elements** [LeetCode 347]
     - Pattern: Max heap by frequency
     - Note: Find k most frequent
     - Exercise: Heap vs bucket sort

127. **Reorganize String** [LeetCode 767]
     - Pattern: Max heap for character interleaving
     - Note: Prevent adjacent characters
     - Exercise: Greedy heap selection

128. **IPO** [LeetCode 502]
     - Pattern: Min/max heap for project selection
     - Note: Select projects within budget
     - Exercise: Two heaps usage

129. **Find Median from Data Stream** [LeetCode 295]
     - Pattern: Two heaps (max and min)
     - Note: Balanced heap approach
     - Exercise: Heap balance maintenance

---

### 🔴 Hard

130. **Sliding Window Median** [LeetCode 480]
     - Pattern: Two heaps with removal
     - Note: Maintain median in sliding window
     - Exercise: Element removal from heap

131. **Merge k Sorted Lists** [LeetCode 23]
     - Pattern: Min heap for k-way merge
     - Note: Extract minimum efficiently
     - Exercise: Heap size and operations

132. **Smallest Range Covering Elements from K Lists** [LeetCode 632]
     - Pattern: Min heap for range minimum
     - Note: Track current range while merging
     - Exercise: Range update logic

---

---

## 📌 WEEK 3 | DAY 4-5: HASHING & HASH TABLES

### Category: Hash Map/Set & Collision Handling

**Pattern Signals:**
- "Frequency counting"
- "Deduplication"
- "Constant-time lookup"

---

### 🟢 Easy

133. **Two Sum** [LeetCode 1]
     - Pattern: Hash map for complement lookup
     - Note: Store seen values
     - Exercise: Single pass efficiency

134. **Valid Anagram** [LeetCode 242]
     - Pattern: Frequency map comparison
     - Note: Character counts must match
     - Exercise: Early termination if sizes differ

135. **Contains Duplicate** [LeetCode 217]
     - Pattern: Hash set for uniqueness
     - Note: Early return on duplicate
     - Exercise: Set membership test

136. **Happy Number** [LeetCode 202]
     - Pattern: Hash set for cycle detection
     - Note: Detect cycles in transformation
     - Exercise: When to stop checking

137. **Isomorphic Strings** [LeetCode 205]
     - Pattern: Bidirectional hash map
     - Note: Consistent character mapping
     - Exercise: Check both directions

138. **Valid Palindrome** [LeetCode 125]
     - Pattern: Hash map for character positions
     - Note: Can build palindrome with transpositions
     - Exercise: Position tracking

---

### 🟡 Medium

139. **Group Anagrams** [LeetCode 49]
     - Pattern: Hash map with sorted string key
     - Note: Group words by signature
     - Exercise: Key generation strategy

140. **Majority Element II** [LeetCode 229]
     - Pattern: Hash map with threshold
     - Note: Find elements appearing > n/3 times
     - Exercise: Why at most 2 elements?

141. **Intersection of Two Arrays** [LeetCode 349]
     - Pattern: Hash sets for intersection
     - Note: Find common elements
     - Exercise: Handle duplicates correctly

142. **Happy Number** [LeetCode 202]
     - Pattern: Cycle detection with hashing
     - Note: Transform until 1 or cycle
     - Exercise: Different approaches

143. **Word Pattern** [LeetCode 290]
     - Pattern: Bidirectional mapping
     - Note: Consistent word-to-character mapping
     - Exercise: Both directions must match

144. **Intersection of Two Arrays II** [LeetCode 350]
     - Pattern: Frequency map for count preservation
     - Note: Include duplicates correctly
     - Exercise: Count frequency of smaller array

145. **LRU Cache** [LeetCode 146]
     - Pattern: Hash map + doubly linked list
     - Note: O(1) cache operations
     - Exercise: Eviction policy

146. **First Unique Character in a String** [LeetCode 387]
     - Pattern: Two-pass frequency mapping
     - Note: Count then search
     - Exercise: Why two passes?

---

### 🔴 Hard

147. **LFU Cache** [LeetCode 460]
     - Pattern: Multiple hash maps + priority
     - Note: Evict least frequently used
     - Exercise: Complex state management

148. **All O`one Data Structure** [LeetCode 432]
     - Pattern: Multiple hash maps + doubly linked list
     - Note: O(1) increment/decrement/getmax
     - Exercise: Frequency grouping

149. **Design HashMap** [LeetCode 706]
     - Pattern: Implement hash map with separate chaining
     - Note: Handle collisions
     - Exercise: Collision resolution

150. **Design HashSet** [LeetCode 705]
     - Pattern: Implement hash set
     - Note: Similar to hash map without values
     - Exercise: Chaining vs open addressing

---

---

## ⚠️ GATCHA & COMMON PITFALLS

### Array Gotchas

🚨 **Mistake 1: Index Out of Bounds**
- Symptom: IndexError or segmentation fault
- Cause: Off-by-one in loop or array access
- Fix: Verify loop bounds: `i < n` not `i <= n`
- Example: `for i in range(n)` goes 0 to n-1 ✓

🚨 **Mistake 2: Not Handling Empty Array**
- Symptom: Wrong answer or crash on empty input
- Cause: Assumed non-empty array
- Fix: Check length before accessing elements
- Example: `if len(arr) == 0: return ...`

🚨 **Mistake 3: Modifying While Iterating**
- Symptom: Skipped elements or wrong result
- Cause: Changed array length mid-iteration
- Fix: Collect indices to delete, process after
- Example: Build result list, don't modify input

---

### Recursion Gotchas

🚨 **Mistake 1: Missing Base Case**
- Symptom: Stack overflow, infinite recursion
- Cause: Recursion doesn't terminate
- Fix: Clear base case that stops recursion
- Example: `if n == 0: return 0` for recursion

🚨 **Mistake 2: Stack Depth Limit**
- Symptom: RecursionError on deep calls
- Cause: Python default ~1000 recursion limit
- Fix: Increase limit or use iteration
- Example: `sys.setrecursionlimit(10000)`

🚨 **Mistake 3: Wrong Problem Reduction**
- Symptom: Infinite loop or wrong answer
- Cause: Recursive case doesn't reduce problem size
- Fix: Verify each recursion gets closer to base case
- Example: `fib(n-1)` not `fib(n)` in fibonacci

---

### Linked List Gotchas

🚨 **Mistake 1: Null Pointer Dereference**
- Symptom: NullPointerException or segfault
- Cause: Accessed `.next` without checking null
- Fix: Always check `node != null` before `.next`
- Example: `while node and node.next:` in Python

🚨 **Mistake 2: Lost Reference**
- Symptom: Broken linked list, can't traverse
- Cause: Modified pointers incorrectly
- Fix: Save next pointer before changing it
- Example: `next_node = curr.next` before `curr.next = prev`

🚨 **Mistake 3: Infinite Loop**
- Symptom: Program hangs
- Cause: Circular reference created
- Fix: Carefully manage pointer modifications
- Example: Don't create cycle accidentally

---

### Stack/Queue Gotchas

🚨 **Mistake 1: Empty Stack/Queue Access**
- Symptom: IndexError or ValueError
- Cause: Popped from empty structure
- Fix: Check empty before popping
- Example: `if stack: val = stack.pop()`

🚨 **Mistake 2: Type Confusion**
- Symptom: Wrong value retrieved
- Cause: Pushed/popped wrong types
- Fix: Consistent type usage
- Example: Always push same type (number or string)

🚨 **Mistake 3: Order Confusion**
- Symptom: LIFO vs FIFO mixed up
- Cause: Used wrong data structure
- Fix: Choose: Stack (LIFO) or Queue (FIFO)
- Example: Stack = last-in-first-out

---

### Binary Search Gotchas

🚨 **Mistake 1: Integer Overflow**
- Symptom: mid calculation wrong (rare in Python)
- Cause: Old formula: `mid = (left + right) // 2`
- Fix: Use `mid = left + (right - left) // 2`
- Example: Large numbers don't overflow with formula

🚨 **Mistake 2: Loop Termination**
- Symptom: Infinite loop or missing answer
- Cause: Wrong loop condition or update
- Fix: Use `while left <= right` or `while left < right`
- Example: `left = mid + 1` not `mid` to avoid infinite loop

🚨 **Mistake 3: Sorted Array Assumption**
- Symptom: Binary search fails
- Cause: Applied binary search to unsorted array
- Fix: Verify array is sorted or sorted in search region
- Example: Rotated arrays need special handling

---

### Heap Gotchas

🚨 **Mistake 1: Heap Property Violation**
- Symptom: Wrong min/max retrieved
- Cause: Heap property broken
- Fix: Maintain parent ≤ children (min heap)
- Example: Heapify after modification

🚨 **Mistake 2: Index Calculation**
- Symptom: Wrong parent/child accessed
- Cause: Index formula wrong
- Fix: Parent = (i-1)//2, Children = 2i+1, 2i+2
- Example: Verify formula on small examples

🚨 **Mistake 3: Heap Size Management**
- Symptom: Heap operations fail
- Cause: Size not updated
- Fix: Always update heap size
- Example: `size += 1` on insert, `size -= 1` on delete

---

### Hash Map/Set Gotchas

🚨 **Mistake 1: Mutable Keys**
- Symptom: KeyError or lost entries
- Cause: Used list or dict as key
- Fix: Use immutable keys (string, tuple, number)
- Example: `key = tuple(sorted(word))` not `key = list(...)`

🚨 **Mistake 2: Key Existence Check**
- Symptom: KeyError when accessing
- Cause: Didn't check if key exists
- Fix: Use `.get(key, default)` or `if key in dict`
- Example: `freq.get(char, 0) + 1` instead of `freq[char] + 1`

🚨 **Mistake 3: Set vs Dict Confusion**
- Symptom: Wrong data structure chosen
- Cause: Mixed up set and dict usage
- Fix: Set = unique values, Dict = key-value pairs
- Example: `seen = set()` for uniqueness, `freq = {}` for counts

---

### Sorting Gotchas

🚨 **Mistake 1: Not Actually Sorted**
- Symptom: Binary search fails after sort
- Cause: Sort didn't execute or failed
- Fix: Verify sort completed
- Example: `arr.sort()` modifies in-place, `sorted(arr)` returns new

🚨 **Mistake 2: Stability Matters**
- Symptom: Different order than expected
- Cause: Used unstable sort when stable needed
- Fix: Use stable sort (Python's sort is stable)
- Example: Merge sort is stable, quicksort often isn't

🚨 **Mistake 3: Custom Comparator Wrong**
- Symptom: Wrong sorting order
- Cause: Comparator logic incorrect
- Fix: Test comparator on examples
- Example: `lambda x: -x` for descending, not `abs(x)`

---

---

## 🎯 PATTERN RECOGNITION GUIDE

### Week 1 Pattern Signals

**Complexity Analysis:**
- ✅ "Compare algorithms" → Complexity analysis
- ✅ "How does it scale?" → Big-O notation
- ✅ "Which is faster?" → Time complexity comparison

**Recursion:**
- ✅ "Tree structure" → Recursive decomposition
- ✅ "Can solve smaller version?" → Recursion candidate
- ✅ "Overlapping subproblems?" → Memoization needed

**Peak Finding:**
- ✅ "Find element with property" → Binary search applicable
- ✅ "Better than linear?" → Exploit structure

---

### Week 2 Pattern Signals

**Arrays:**
- ✅ "Find pair/triplet" → Hash map or two pointers
- ✅ "In-place modification" → Two pointer technique
- ✅ "Contiguous elements" → Array traversal

**Linked Lists:**
- ✅ "Cycle detection" → Fast/slow pointers
- ✅ "Find middle" → Fast/slow pointers
- ✅ "Reverse list" → Three pointers or recursion

**Stacks/Queues:**
- ✅ "Bracket matching" → Stack structure
- ✅ "Processing order" → LIFO (stack) or FIFO (queue)

**Binary Search:**
- ✅ "Sorted array" → Binary search applicable
- ✅ "Find boundary" → Binary search
- ✅ "Logarithmic speedup" → Binary search opportunity

---

### Week 3 Pattern Signals

**Sorting:**
- ✅ "Sort the array first" → Enables other algorithms
- ✅ "O(n log n) required" → Merge sort or quicksort
- ✅ "In-place O(n²)" → Bubble/insertion sort

**Heaps:**
- ✅ "Find k largest/smallest" → Heap of size k
- ✅ "Streaming/online" → Maintain heap
- ✅ "Priority-based" → Heap extraction

**Hashing:**
- ✅ "Frequency counting" → Hash map
- ✅ "Deduplication" → Hash set
- ✅ "Fast lookup" → Hash table

---

---

## 💡 TIPS & TRICKS FOR PROBLEM SOLVING

### General Tips

#### 1. **Understand Before Implementing**
- Read problem 2-3 times
- Write down what you need to find
- Note constraints and examples

#### 2. **Draw and Trace**
- Visual representation clarifies
- Trace algorithm on example
- Verify on edge cases before coding

#### 3. **Handle Edge Cases Early**
- Empty input (n=0)
- Single element (n=1)
- All same elements
- Negative numbers

#### 4. **Start Simple, Optimize Later**
- Get working solution first
- Then optimize time/space
- Easier to debug simple code

#### 5. **Test Your Code**
- Run on provided examples
- Create own test cases
- Check edge cases

---

### Recursion Tricks

#### Trick 1: **Trace Call Stack**
```
Understand call order:
Draw tree of function calls
Base case = leaf nodes
```

#### Trick 2: **Verify Base Case**
```
Always starts with clear base case
Test base case separately
```

#### Trick 3: **Prove by Induction**
```
Base: Show true for n=1
Induction: Assume true for n-1, prove for n
```

---

### Linked List Tricks

#### Trick 1: **Dummy Node**
```
Create fake head node
Simplifies head removal
Pointer manipulation cleaner
```

#### Trick 2: **Save Next Pointer**
```
Before modifying pointer:
next_node = curr.next
Then safe to change curr.next
```

#### Trick 3: **Fast/Slow for Finding Middle**
```
Fast moves 2 steps, slow moves 1
Meet at middle when fast reaches end
```

---

### Stack/Queue Tricks

#### Trick 1: **Know LIFO vs FIFO**
```
Stack = LIFO (Last-In-First-Out)
Queue = FIFO (First-In-First-Out)
Choose based on problem
```

#### Trick 2: **Two Stacks Pattern**
```
Reverse with two stacks
Simulate queue with stacks
```

---

### Binary Search Tricks

#### Trick 1: **Mid Calculation**
```
Use: mid = left + (right - left) // 2
Avoids overflow
```

#### Trick 2: **Boundary Checks**
```
left = 0, right = n-1 (inclusive)
left = 0, right = n (exclusive right)
Be consistent!
```

#### Trick 3: **Loop Invariant**
```
Maintain: answer in [left, right]
Each iteration narrows range
```

---

### Heap Tricks

#### Trick 1: **Min Heap for K Largest**
```
Keep heap size = k
Extract minimum when size > k
Top of heap = k-th largest
```

#### Trick 2: **Maintain Balance**
```
For two heaps (median finding):
max_heap = left half
min_heap = right half
Balance sizes
```

---

### Hashing Tricks

#### Trick 1: **Key Design**
```
Choose key that represents subset/group
Example: sorted string for anagrams
```

#### Trick 2: **Frequency Array**
```
If alphabet limited:
Use array[26] for letters
Faster than hash map
```

#### Trick 3: **Bidirectional Mapping**
```
For 1-to-1 relationships:
Keep two maps
Check both directions
```

---

---

## 📋 PROGRESS TRACKING

### How to Track Your Progress

```
WEEK 1:
  Days 1-2: Complexity Analysis
    [ ] Easy: Problems 1-5 (5/5)
    [ ] Medium: Problems 6-12 (7/7)
    [ ] Hard: Problems 13-14 (2/2)
    ☐ COMPLETE: ___/14 problems
  
  Day 3: Space Complexity
    [ ] Easy: Problems 15-18 (4/4)
    [ ] Medium: Problems 19-23 (5/5)
    [ ] Hard: Problems 24-25 (2/2)
    ☐ COMPLETE: ___/11 problems
  
  Days 4-5: Recursion
    [ ] Easy: Problems 26-32 (7/7)
    [ ] Medium: Problems 33-40 (8/8)
    [ ] Hard: Problems 41-42 (2/2)
    ☐ COMPLETE: ___/17 problems
  
  Day 6 (Optional): Peak Finding
    [ ] Easy: Problems 43-45 (3/3)
    [ ] Medium: Problems 46-48 (3/3)
    [ ] Hard: Problems 49-50 (2/2)
    ☐ COMPLETE: ___/8 problems

WEEK 1 TOTAL: ___/50 problems

WEEK 2:
  [Similar tracking for Days 1-5...]

WEEK 2 TOTAL: ___/25 problems

WEEK 3:
  [Similar tracking for Days 1-5...]

WEEK 3 TOTAL: ___/25+ problems

═══════════════════════════════════
GRAND TOTAL: ___/150+ PROBLEMS
═══════════════════════════════════
```

---

---

## 🎓 FINAL ADVICE

### Mastery Path

1. **Understand Fundamentals** (Weeks 1-3)
   - Memory and complexity
   - Basic data structures
   - Simple algorithms

2. **Build Intuition** (Daily practice)
   - Solve problems consistently
   - Understand patterns
   - Connect concepts

3. **Recognize Patterns** (Ongoing)
   - Each problem type → pattern
   - Multiple solutions → tradeoffs
   - When to use which approach

4. **Optimize Skills** (Practice)
   - Code faster
   - Fewer bugs
   - Better solutions

5. **Master Interview** (Final prep)
   - Explain clearly
   - Code cleanly
   - Handle edge cases

---

### Common Mistakes to Avoid

1. **Rushing to Code**
   - Think first, code later
   - Draw examples
   - Plan approach

2. **Ignoring Edge Cases**
   - Empty input
   - Single element
   - Duplicates
   - Negatives

3. **Not Analyzing Complexity**
   - Know your algorithm's time/space
   - Can you optimize?
   - Why is it optimal?

4. **Memorizing Instead of Understanding**
   - Learn patterns, not solutions
   - Solve similar problems
   - Explain to someone else

---

### When You Get Stuck

1. **Don't immediately look at solution**
   - Think for 15-20 minutes
   - Draw more examples
   - Try different approaches

2. **Look at hints, not solution**
   - Hints guide without giving away
   - Better for learning
   - Forces you to complete journey

3. **Revisit after learning more**
   - Come back to hard problems
   - Knowledge compounds
   - Earlier hard = later easy

---

### Before Moving to Weeks 4-6

✅ **Solve all 150+ problems at least once**
✅ **Re-solve 30 hardest problems**
✅ **Explain solutions to someone else**
✅ **Implement from scratch (no peeking)**
✅ **Understand why each algorithm works**

---

**End of LeetCode Practice List for Weeks 1-3**

*150+ Problems for foundations*
*Comprehensive gotcha section*
*Tips and tricks throughout*
*Clear progression from fundamentals to advanced*
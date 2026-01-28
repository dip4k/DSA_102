# 🎯 LEETCODE PRACTICE: WEEKS 4-6 COMPREHENSIVE PROBLEM LIST

**Scope:** Weeks 4-6 Pattern Mastery
**Problem Count:** 80+ problems
**Difficulty Range:** Easy → Medium → Hard  
**Categories:** 8 major patterns across 15 days
**Updated:** January 2026

---

## 📚 TABLE OF CONTENTS

1. **Week 4: Two Pointers, Sliding Window, Divide & Conquer, Binary Search**
2. **Week 5: Hash Maps, Monotonic Stack, Intervals, Partition, Kadane, Fast/Slow Pointers**
3. **Week 6: String Patterns (Palindrome, Substring, Parentheses, Transformations)**
4. **Gatcha & Gotchas - Common Pitfalls**
5. **Pattern Recognition Guide**
6. **Tips & Tricks for Problem Solving**
7. **When to Choose Which Pattern**

---

## ⚡ HOW TO USE THIS LIST

✅ **Solve in progression order** (problems are ordered by increasing difficulty)
✅ **Don't skip categories** (each builds on previous concepts)
✅ **Focus on approach** (understand HOW, not just what)
✅ **Reference patterns** (note which pattern applies)
✅ **Track your progress** (mark problems as you solve)

**Time Estimate:** 80 problems × 20-40 min average = 27-53 hours of practice

---

---

## 🟦 WEEK 4: TWO POINTERS, SLIDING WINDOW, DIVIDE & CONQUER, BINARY SEARCH

### Overview
**Goal:** Master foundational patterns for efficient array manipulation
**Key Insight:** Move multiple pointers strategically to reduce complexity from O(n²) to O(n)

---

## 📌 WEEK 4 | DAY 1: TWO-POINTER PATTERNS

### Category: Basic Two-Pointer

**Pattern Signals:** 
- Sorted array + find pair/triplet
- In-place array modification
- Partitioning or rearrangement

---

### 🟢 Easy (Start Here)

1. **Valid Palindrome** [LeetCode 125]
   - Pattern: Two pointers from ends
   - Note: Handle non-alphanumeric characters
   - Hint: Move inward, skip non-letters

2. **Two Sum II - Input Array Is Sorted** [LeetCode 167]
   - Pattern: Two pointers in sorted array
   - Note: Array is sorted (important!)
   - Hint: Start left and right

3. **Move Zeroes** [LeetCode 283]
   - Pattern: In-place rearrangement
   - Note: Maintain relative order of non-zero elements
   - Hint: Two pointers, fast and slow approach

4. **Reverse String** [LeetCode 344]
   - Pattern: Two pointers from ends
   - Note: Do it in-place with O(1) space
   - Hint: Classic swap pattern

5. **Remove Duplicates from Sorted Array** [LeetCode 26]
   - Pattern: Two pointers in sorted array
   - Note: Return count of unique elements
   - Hint: Slow pointer marks placement

---

### 🟡 Medium

6. **Container With Most Water** [LeetCode 11]
   - Pattern: Two pointers with greedy movement
   - Note: Area = min(height[i], height[j]) × (j - i)
   - Hint: Why move the shorter pointer?

7. **3Sum** [LeetCode 15]
   - Pattern: Two pointers inside loop
   - Note: Find all triplets that sum to 0, handle duplicates
   - Hint: Sort first, then use two pointers on remaining

8. **Remove Duplicates from Sorted Array II** [LeetCode 80]
   - Pattern: Two pointers with condition (at most 2 duplicates)
   - Note: More complex than Day 1 variant
   - Hint: Count occurrences as you go

9. **Squares of a Sorted Array** [LeetCode 977]
   - Pattern: Two pointers (largest values at ends)
   - Note: Negative numbers exist
   - Hint: Largest square comes from either end

10. **Sort Array By Parity** [LeetCode 905]
    - Pattern: Two pointers partitioning
    - Note: Partition into even and odd
    - Hint: Similar to partition in quicksort

11. **Partition Equal Subset Sum** (using two pointers concept) [LeetCode 416]
    - Pattern: Not purely two-pointer but related concept
    - Note: DP problem but test your understanding

12. **Rotate Array** [LeetCode 189]
    - Pattern: Three-step reversal
    - Note: Rotate right by k positions
    - Hint: Reverse whole, then parts

13. **2Sum Less Than Target** [LeetCode 1099]
    - Pattern: Two pointers with count
    - Note: Count pairs where a + b < target
    - Hint: Sorted array, greedy movement

---

### 🔴 Hard

14. **Median of Two Sorted Arrays** [LeetCode 4]
    - Pattern: Two pointers / Binary search (two approaches)
    - Note: Very classic, multiple solutions exist
    - Hint: Think about what "median" means for merged array

15. **Trapping Rain Water** [LeetCode 42]
    - Pattern: Two pointers or stack approach
    - Note: Calculate trapped water between walls
    - Hint: Water at position = min(max_left, max_right) - height[i]

16. **3Sum Closest** [LeetCode 16]
    - Pattern: Two pointers inside loop
    - Note: Find triplet closest to target
    - Hint: Track closest sum as you go

17. **Remove Nth Node From End of List** [LeetCode 19]
    - Pattern: Fast/slow pointers on linked list
    - Note: O(1) pass with dummy node trick
    - Hint: Move fast pointer n steps first

---

---

## 📌 WEEK 4 | DAY 2: SLIDING WINDOW (FIXED SIZE)

### Category: Fixed-Length Windows

**Pattern Signals:**
- "Maximum/minimum in every window of size k"
- Fixed window that slides
- Compute aggregate (sum, product, frequency)

---

### 🟢 Easy

18. **Maximum Average Subarray I** [LeetCode 643]
    - Pattern: Fixed window sum
    - Note: Find max average of k consecutive elements
    - Hint: Compute first window, then slide

19. **Grumpy Bookstore Owner** [LeetCode 1052]
    - Pattern: Fixed window, maximizing effect
    - Note: Calculate effect of making customers happy in window
    - Hint: Try all windows of size k

20. **Number of Submatrices That Sum to Target** (partial) [LeetCode 1074]
    - Pattern: 2D fixed window (harder variant)
    - Note: Requires nested loops
    - Skip if 2D is too complex; return after basics

21. **Find All Anagrams in a String** [LeetCode 438]
    - Pattern: Fixed window with frequency comparison
    - Note: Find all starting indices where anagram exists
    - Hint: Compare frequency maps at each window

22. **Permutation in String** [LeetCode 567]
    - Pattern: Fixed window with frequency check
    - Note: Is s1 a permutation of any substring in s2?
    - Hint: Same as anagrams problem concept

---

### 🟡 Medium

23. **Sliding Window Maximum** [LeetCode 239]
    - Pattern: Monotonic deque
    - Note: Find max in every k-length window
    - Hint: Use deque to maintain decreasing order of indices

24. **Sliding Window Median** [LeetCode 480]
    - Pattern: Fixed window with complex aggregate
    - Note: Find median in every k-length window
    - Hint: Multiset or heap approach

25. **Best Time to Buy and Sell Stock IV** [LeetCode 188]
    - Pattern: Fixed window with state tracking
    - Note: Dynamic programming variant
    - Skip if struggling; return to after DP week

26. **Repeated DNA Sequences** [LeetCode 187]
    - Pattern: Fixed window (10-character) + hashing
    - Note: Find all 10-letter sequences appearing twice+
    - Hint: Rolling hash or simple set-based approach

27. **Maximum Points You Can Obtain from Cards** [LeetCode 1423]
    - Pattern: Fixed window (complement)
    - Note: Take k cards from ends, maximize sum
    - Hint: Window in middle = unused cards

---

### 🔴 Hard

28. **Minimum Window Substring with Frequency** (extension) [LeetCode 76 variant]
    - Pattern: Variable size but treating fixed concept
    - Note: Harder frequency requirements
    - Return to after variable window

---

---

## 📌 WEEK 4 | DAY 3: SLIDING WINDOW (VARIABLE SIZE)

### Category: Dynamic Windows with Constraints

**Pattern Signals:**
- "Find longest/shortest substring with property X"
- "At most k distinct characters"
- "Sum less than target"

---

### 🟢 Easy

29. **Longest Substring Without Repeating Characters** [LeetCode 3]
    - Pattern: Variable window, character deduplication
    - Note: No repeating characters allowed
    - Hint: Hash map to track positions

30. **Minimum Size Subarray Sum** [LeetCode 209]
    - Pattern: Variable window with sum constraint
    - Note: Find shortest subarray with sum ≥ target
    - Hint: Expand right, shrink left when valid

31. **Frequency of the Most Frequent Element** [LeetCode 1838]
    - Pattern: Variable window with frequency constraint
    - Note: Make all elements same with cost
    - Hint: Window represents elements to equalize

32. **Contains Duplicate II** [LeetCode 219]
    - Pattern: Fixed/variable window size k
    - Note: Check if duplicate exists within k indices
    - Hint: Maintain sliding window of size k

33. **Maximum Erasure Value** [LeetCode 1695]
    - Pattern: Variable window, no duplicates
    - Note: Find max sum subarray with unique elements
    - Hint: Similar to longest substring without repeating

---

### 🟡 Medium

34. **Longest Substring with At Most K Distinct Characters** [LeetCode 340]
    - Pattern: Variable window with character limit
    - Note: Maintain at most k distinct chars
    - Hint: Expand right, shrink left when distinct > k

35. **Minimum Window Substring** [LeetCode 76]
    - Pattern: Variable window with frequency matching
    - Note: Find smallest window containing all chars of pattern
    - Hint: Track how many chars have sufficient frequency

36. **Permutation in String** [LeetCode 567]
    - Pattern: Variable vs fixed window (both work)
    - Note: Alternative approach using variable window
    - Hint: Expand/shrink based on character count

37. **Substring with Concatenation of All Words** [LeetCode 30]
    - Pattern: Fixed window size (word length × word count)
    - Note: Find all starting positions
    - Hint: Sliding window of words, not characters

38. **Subarrays with K Different Integers** [LeetCode 992]
    - Pattern: "At most k" minus "at most k-1" trick
    - Note: Find count of subarrays with exactly k distinct
    - Hint: Reduce to simpler problem

39. **Fruit Into Baskets** [LeetCode 904]
    - Pattern: Variable window, at most 2 types
    - Note: Pick fruit from max 2 consecutive trees
    - Hint: Longest subarray with at most 2 distinct values

40. **Longest Repeating Character Replacement** [LeetCode 424]
    - Pattern: Variable window with replacement budget
    - Note: Can replace at most k characters
    - Hint: Window valid if (window_length - max_char_freq) ≤ k

---

### 🔴 Hard

41. **Minimum Unique Array Sum Removal Cost** (advanced) [LeetCode - Custom]
    - Pattern: Variable window optimization
    - Note: Complex cost calculation
    - Skip for now

---

---

## 📌 WEEK 4 | DAY 4: DIVIDE & CONQUER

### Category: Recursive Problem Splitting

**Pattern Signals:**
- "Divide into subproblems, solve independently, merge results"
- Recursive structure visible
- Linear → Logarithmic improvement

---

### 🟢 Easy

42. **Maximum Subarray** [LeetCode 53]
    - Pattern: Divide & conquer (Kadane's algorithm alternative)
    - Note: Also solvable with Kadane's
    - Hint: Divide at midpoint, best subarray in left/right/crossing

43. **Reverse Linked List** [LeetCode 206]
    - Pattern: Divide & conquer on linked list
    - Note: Recursive approach
    - Hint: Reverse rest, point back, return head of rest

44. **Merge Sorted Array** [LeetCode 88]
    - Pattern: Merge operation from merge sort
    - Note: Merge two sorted arrays in-place
    - Hint: Work backwards to avoid overwriting

---

### 🟡 Medium

45. **Merge Sort Algorithm** [LeetCode - Implement] or similar [LeetCode 148]
    - Pattern: Classic divide & conquer
    - Note: Sort linked list in O(n log n) with O(log n) space
    - Hint: Find middle, split, merge

46. **Convert Sorted List to Binary Search Tree** [LeetCode 109]
    - Pattern: Divide array/list at midpoint
    - Note: Create balanced BST
    - Hint: Middle element becomes root

47. **Majority Element** [LeetCode 169]
    - Pattern: Divide & conquer (also other approaches)
    - Note: Find element appearing > n/2 times
    - Hint: Divide into halves, merge counts

48. **Sort an Array** [LeetCode 912]
    - Pattern: Implement efficient sorting
    - Note: Merge sort or quicksort (divide & conquer)
    - Hint: Build from divide & conquer principles

49. **Maximum Frequency Stack** [LeetCode 895]
    - Pattern: Not purely D&C but demonstrates decomposition
    - Note: Push/pop respecting max frequency order
    - Skip if struggling

50. **Invert Binary Tree** [LeetCode 226]
    - Pattern: Recursive decomposition
    - Note: Swap left and right subtrees
    - Hint: Solve left and right, combine

---

### 🔴 Hard

51. **Median of Two Sorted Arrays** [LeetCode 4]
    - Pattern: Divide & conquer or binary search
    - Note: O(log(min(m,n))) approach
    - Hint: Partition and find kth element

52. **Closest Pair of Points** (2D divide & conquer) [Custom/LeetCode variant]
    - Pattern: 2D divide & conquer
    - Note: Find two closest points
    - Skip for now

---

---

## 📌 WEEK 4 | DAY 5: BINARY SEARCH AS PATTERN (ANSWER SPACE)

### Category: Binary Search on Answer

**Pattern Signals:**
- "Find minimum X such that condition(X) is true"
- Monotonic property (false→true transition)
- Feasibility checking function exists

---

### 🟢 Easy

53. **Binary Search** [LeetCode 704]
    - Pattern: Standard binary search
    - Note: Find target in sorted array
    - Hint: mid = left + (right - left) / 2

54. **Search Insert Position** [LeetCode 35]
    - Pattern: Binary search variant
    - Note: Find position to insert to maintain sorted order
    - Hint: Return position even if not found

55. **First Bad Version** [LeetCode 278]
    - Pattern: Binary search on answer space
    - Note: Find first bad version
    - Hint: Check isBadVersion() monotonically

---

### 🟡 Medium

56. **Capacity To Ship Packages Within D Days** [LeetCode 1011]
    - Pattern: Binary search on answer (capacity)
    - Note: Find minimum capacity to ship all packages in D days
    - Hint: Check feasibility for given capacity

57. **Divide Chocolate** [LeetCode 1231]
    - Pattern: Binary search on answer (sweetness)
    - Note: Divide chocolate to maximize minimum sweetness
    - Hint: Check if can divide into k pieces with min sweetness

58. **Koko Eating Bananas** [LeetCode 875]
    - Pattern: Binary search on answer (eating speed)
    - Note: Find minimum speed to eat all bananas in h hours
    - Hint: Slower → more time needed

59. **Magnetic Force Between Two Balls** [LeetCode 1552]
    - Pattern: Binary search on answer (distance)
    - Note: Place m balls to maximize minimum distance
    - Hint: Check if can place with given distance

60. **Minimum Number of Days to Make m Bouquets** [LeetCode 1482]
    - Pattern: Binary search on days
    - Note: Find minimum days to make m bouquets
    - Hint: More days → more flowers bloom

61. **Time Based Key-Value Store** [LeetCode 981]
    - Pattern: Binary search on time
    - Note: Get value for key at closest time
    - Hint: Use bisect_right or custom binary search

62. **Find the Duplicate Number** [LeetCode 287]
    - Pattern: Binary search on answer (duplicate number)
    - Note: Find duplicate in array of n+1 integers
    - Hint: Check how many numbers ≤ mid

---

### 🔴 Hard

63. **Minimum Refinement of Chocolate** (advanced variant)
    - Pattern: Binary search with complex feasibility
    - Note: Multiple constraints
    - Skip for now

64. **Wildcard Matching with Binary Search** (advanced)
    - Pattern: Binary search + pattern matching
    - Note: Combination of patterns
    - Skip for now

---

---

## 🟩 WEEK 5: HASH MAPS, MONOTONIC STACK, INTERVALS, PARTITION, KADANE, FAST/SLOW

---

## 📌 WEEK 5 | DAY 1: HASH MAP / HASH SET PATTERNS

### Category: Frequency Tracking & Lookup

**Pattern Signals:**
- "Find duplicate/frequency"
- "Group by property"
- "Constant time lookup/insertion"

---

### 🟢 Easy

65. **Two Sum** [LeetCode 1]
    - Pattern: Hash map for complement lookup
    - Note: Find two numbers that sum to target
    - Hint: Store seen numbers and their indices

66. **Valid Anagram** [LeetCode 242]
    - Pattern: Character frequency comparison
    - Note: Are two strings anagrams?
    - Hint: Count characters in both

67. **Happy Number** [LeetCode 202]
    - Pattern: Hash set for cycle detection
    - Note: Repeatedly sum digits until 1 or cycle
    - Hint: Track seen sums to detect cycle

68. **Isomorphic Strings** [LeetCode 205]
    - Pattern: Hash map for character mapping
    - Note: Consistent character replacement?
    - Hint: Track both directions of mapping

69. **Contains Duplicate** [LeetCode 217]
    - Pattern: Hash set for uniqueness
    - Note: Are there duplicates?
    - Hint: Add to set, return false if already exists

70. **Valid Parentheses** [LeetCode 20]
    - Pattern: Not hash but related - stack/map
    - Note: Check matching brackets
    - Hint: Use map for matching pairs + stack

---

### 🟡 Medium

71. **Group Anagrams** [LeetCode 49]
    - Pattern: Hash map grouping by signature
    - Note: Group words that are anagrams
    - Hint: Use sorted string or character count as key

72. **Majority Element II** [LeetCode 229]
    - Pattern: Hash map frequency + threshold
    - Note: Find elements appearing > ⌊n/3⌋ times
    - Hint: Only up to 3 candidates possible

73. **LRU Cache** [LeetCode 146]
    - Pattern: Hash map + doubly linked list
    - Note: Implement cache with O(1) ops
    - Hint: Timestamp or order tracking

74. **Word Pattern** [LeetCode 290]
    - Pattern: Bidirectional hash map
    - Note: Consistent word-to-character mapping?
    - Hint: Check both directions

75. **Intersection of Two Arrays II** [LeetCode 350]
    - Pattern: Hash map for frequency
    - Note: Find common elements with correct frequency
    - Hint: Count in smaller array, look up in larger

76. **Top K Frequent Elements** [LeetCode 347]
    - Pattern: Hash map + heap/bucket sort
    - Note: Find k most frequent elements
    - Hint: Multiple approaches (heap, bucket, quickselect)

---

### 🔴 Hard

77. **LFU Cache** [LeetCode 460]
    - Pattern: Hash map + priority management
    - Note: Evict least frequently used
    - Hint: Track frequency and use timestamps for ties

---

---

## 📌 WEEK 5 | DAY 2: MONOTONIC STACK

### Category: Stack with Ordering Property

**Pattern Signals:**
- "Next greater/smaller element"
- "Maintain decreasing/increasing order"
- "Efficient scans avoiding O(n²)"

---

### 🟢 Easy

78. **Next Greater Element I** [LeetCode 496]
    - Pattern: Monotonic stack basics
    - Note: Find next greater element
    - Hint: Iterate backwards, maintain decreasing stack

---

### 🟡 Medium

79. **Daily Temperatures** [LeetCode 739]
    - Pattern: Monotonic stack for next greater
    - Note: Days until warmer temperature
    - Hint: Stack stores indices, not temperatures

80. **Next Greater Element II** [LeetCode 503]
    - Pattern: Monotonic stack with wrap-around
    - Note: Circular array, find next greater
    - Hint: Iterate array twice conceptually

81. **Largest Rectangle in Histogram** [LeetCode 84]
    - Pattern: Monotonic stack for range
    - Note: Find largest rectangular area
    - Hint: Stack maintains increasing heights and spans

82. **Trapping Rain Water** [LeetCode 42]
    - Pattern: Stack approach alternative to two pointers
    - Note: Calculate trapped water
    - Hint: Stack stores increasing indices

83. **Remove K Digits** [LeetCode 402]
    - Pattern: Monotonic stack construction
    - Note: Remove k digits, minimize result number
    - Hint: Stack maintains increasing digits

84. **Removing Stars From a String** [LeetCode 2390]
    - Pattern: Stack for removal pattern
    - Note: Remove character and preceding star
    - Hint: Simulate the process with stack

85. **Validate Stack Sequences** [LeetCode 946]
    - Pattern: Stack simulation
    - Note: Can pushed sequence result in popped sequence?
    - Hint: Simulate pushing and popping

---

### 🔴 Hard

86. **Maximum Frequency Stack** [LeetCode 895]
    - Pattern: Advanced stack pattern
    - Note: Pop max frequency elements
    - Skip unless confident

87. **Trapping Rain Water II** [LeetCode 407]
    - Pattern: 2D extension
    - Note: 3D terrain, water trapping
    - Skip for later

---

---

## 📌 WEEK 5 | DAY 3: MERGE OPERATIONS & INTERVAL PATTERNS

### Category: Interval Combination & Overlap

**Pattern Signals:**
- "Merge overlapping intervals"
- "Insert interval into list"
- "Meeting rooms/scheduling"

---

### 🟢 Easy

88. **Merge Two Sorted Lists** [LeetCode 21]
    - Pattern: Two pointer merge
    - Note: Merge two sorted linked lists
    - Hint: Compare and advance pointers

---

### 🟡 Medium

89. **Merge Intervals** [LeetCode 56]
    - Pattern: Sort then merge
    - Note: Merge all overlapping intervals
    - Hint: Sort by start, then check overlaps

90. **Insert Interval** [LeetCode 57]
    - Pattern: Interval insertion without full sort
    - Note: Insert new interval and merge
    - Hint: Process before, overlapping, after

91. **Meeting Rooms** [LeetCode 252]
    - Pattern: Interval overlap detection
    - Note: Can attend all meetings?
    - Hint: Sort and check adjacent overlaps

92. **Interval List Intersections** [LeetCode 986]
    - Pattern: Two pointer on interval lists
    - Note: Find intersection of two interval lists
    - Hint: Advance lower end

93. **Reorder Data in Log Files** [LeetCode 937]
    - Pattern: Custom sorting
    - Note: Sort logs with custom rules
    - Hint: Separate identifier from body

---

### 🔴 Hard

94. **Meeting Rooms II** [LeetCode 253]
    - Pattern: Interval overlap counting
    - Note: Minimum rooms needed
    - Hint: Events and counts approach

95. **Video Stitching** [LeetCode 1024]
    - Pattern: Greedy interval covering
    - Note: Stitch clips to cover [0, time]
    - Hint: Greedy selection of rightmost reachable

---

---

## 📌 WEEK 5 | DAY 4A: PARTITION & CYCLIC SORT

### Category: In-Place Rearrangement

**Pattern Signals:**
- "In-place rearrangement"
- "Colors/flags/0-1-2 pattern"
- "Cyclic sort"

---

### 🟢 Easy

96. **Sort Colors** [LeetCode 75]
    - Pattern: Partition into three sections
    - Note: 0s, 1s, 2s in-place
    - Hint: Three pointers or single pass

97. **Valid Parentheses** (prerequisite) [LeetCode 20]
    - Pattern: Not partition but sequential logic
    - Skip if already done

---

### 🟡 Medium

98. **Missing Number** [LeetCode 268]
    - Pattern: Cyclic sort concept
    - Note: Find missing in [0, n]
    - Hint: XOR approach or placement approach

99. **Find the Duplicate Number** [LeetCode 287]
    - Pattern: Cyclic sort / linked list cycle
    - Note: Find duplicate (not missing)
    - Hint: Fast/slow pointers work here too

100. **First Missing Positive** [LeetCode 41]
     - Pattern: Cyclic sort in-place
     - Note: Smallest positive integer not in array
     - Hint: Place number i at index i-1

101. **Find All Duplicates in Array** [LeetCode 442]
     - Pattern: Cyclic sort / marking
     - Note: Find all duplicates without extra space
     - Hint: Mark visited by negating value at index

---

---

## 📌 WEEK 5 | DAY 4B: KADANE'S ALGORITHM

### Category: Maximum Subarray Sum

**Pattern Signals:**
- "Maximum subarray sum"
- "Track running maximum"
- "Extend or restart logic"

---

### 🟢 Easy

102. **Maximum Subarray** [LeetCode 53]
     - Pattern: Kadane's algorithm
     - Note: Find maximum sum contiguous subarray
     - Hint: current_max = max(nums[i], current_max + nums[i])

103. **Maximum Product Subarray** [LeetCode 152]
     - Pattern: Kadane's with tracks for max/min
     - Note: Handle negative numbers flipping products
     - Hint: Track both max and min products

---

### 🟡 Medium

104. **Maximum Subarray Sum with One Deletion** [LeetCode 1186]
     - Pattern: Kadane's extended
     - Note: Optional one deletion allowed
     - Hint: Track max without deletion and with deletion

105. **Maximum Sum of 3 Non-Overlapping Subarrays** [LeetCode 689]
     - Pattern: DP + Kadane's
     - Note: Find 3 non-overlapping arrays with max total
     - Hint: Precompute best before and after each position

---

---

## 📌 WEEK 5 | DAY 5: FAST/SLOW POINTERS

### Category: Linked List Manipulation

**Pattern Signals:**
- "Linked list cycle detection"
- "Find middle of linked list"
- "Linked list operations"

---

### 🟢 Easy

106. **Linked List Cycle** [LeetCode 141]
     - Pattern: Fast/slow cycle detection
     - Note: Detect cycle in linked list
     - Hint: Floyd's algorithm

107. **Middle of the Linked List** [LeetCode 876]
     - Pattern: Fast/slow for finding middle
     - Note: Find middle node
     - Hint: Fast moves 2, slow moves 1

108. **Palindrome Linked List** [LeetCode 234]
     - Pattern: Find middle, reverse, compare
     - Note: Check if linked list is palindrome
     - Hint: Combine fast/slow with reversal

---

### 🟡 Medium

109. **Remove Nth Node From End of List** [LeetCode 19]
     - Pattern: Fast pointer gap
     - Note: Remove nth node from end
     - Hint: Gap = n positions

110. **Reorder List** [LeetCode 143]
     - Pattern: Find middle, reverse, merge
     - Note: Reorder L1→L2→...Ln to L1→Ln→L2→Ln-1
     - Hint: Three steps with helpers

111. **Intersection of Two Linked Lists** [LeetCode 160]
     - Pattern: Two pointers same length
     - Note: Find intersection node
     - Hint: Move to head of other list when reaching end

---

### 🔴 Hard

112. **Copy List with Random Pointer** [LeetCode 138]
     - Pattern: Hash map + linked list
     - Note: Deep copy with random pointers
     - Hint: Two passes or interweaving

---

---

## 🟧 WEEK 6: STRING PATTERNS

---

## 📌 WEEK 6 | DAY 1: PALINDROME PATTERNS

### Category: Palindrome Recognition & Construction

**Pattern Signals:**
- "Palindrome substring"
- "Expand around center"
- "Manacher's algorithm"

---

### 🟢 Easy

113. **Valid Palindrome** [LeetCode 125]
     - Pattern: Two pointers ignoring non-alphanumeric
     - Note: Check palindrome ignoring spaces/punctuation
     - Hint: Skip non-alphanumeric characters

114. **Palindrome Number** [LeetCode 9]
     - Pattern: Reverse and compare (or two pointers)
     - Note: Check if number is palindrome
     - Hint: Reverse or extract digits

115. **Longest Palindrome** [LeetCode 409]
     - Pattern: Character frequency
     - Note: Build longest palindrome from letters
     - Hint: Use pairs, possibly one odd in middle

---

### 🟡 Medium

116. **Longest Palindromic Substring** [LeetCode 5]
     - Pattern: Expand around center
     - Note: Find longest palindromic substring
     - Hint: Center can be char or between chars

117. **Palindrome Partitions** [LeetCode 131]
     - Pattern: Backtracking + palindrome check
     - Note: Partition string into palindromes
     - Hint: Try all partitions recursively

118. **Valid Palindrome II** [LeetCode 680]
     - Pattern: Two pointers with one deletion
     - Note: Palindrome after deleting at most one character
     - Hint: Check substrings when mismatch found

119. **Longest Palindromic Subsequence** [LeetCode 516]
     - Pattern: DP (not pure two-pointer)
     - Note: Longest palindromic subsequence (not substring)
     - Hint: LCS of string and reverse

---

### 🔴 Hard

120. **Shortest Palindrome** [LeetCode 214]
     - Pattern: KMP algorithm or rolling hash
     - Note: Add minimum chars to front to make palindrome
     - Hint: Find longest palindrome from start

121. **Palindrome Pairs** [LeetCode 336]
     - Pattern: Hash map + palindrome check
     - Note: Find pairs of words that form palindrome
     - Hint: For each word, check complement in hash

---

---

## 📌 WEEK 6 | DAY 2: SUBSTRING & SLIDING WINDOW ON STRINGS

### Category: String Substring Patterns

**Pattern Signals:**
- "Longest/shortest substring with property"
- "Anagrams/permutations"
- "Character constraints"

---

### 🟢 Easy

122. **Reverse String** [LeetCode 344]
     - Pattern: Two pointers
     - Note: Reverse string in-place
     - Hint: Swap from ends

123. **Valid Anagram** [LeetCode 242]
     - Pattern: Character frequency
     - Note: Check if anagram
     - Hint: Sort or count

124. **First Unique Character in a String** [LeetCode 387]
     - Pattern: Frequency + single pass
     - Note: Find first non-repeating character
     - Hint: Frequency map + linear scan

---

### 🟡 Medium

125. **Longest Substring Without Repeating Characters** [LeetCode 3]
     - Pattern: Sliding window (variable size)
     - Note: Longest without repeating chars
     - Hint: Hash map for char positions

126. **Minimum Window Substring** [LeetCode 76]
     - Pattern: Variable window with frequency
     - Note: Smallest window with all target chars
     - Hint: Frequency maps and counting

127. **Permutation in String** [LeetCode 567]
     - Pattern: Fixed window frequency match
     - Note: Is s1 permutation of any substring in s2?
     - Hint: Compare frequency maps

128. **Find All Anagrams in a String** [LeetCode 438]
     - Pattern: Sliding window + frequency
     - Note: Find all starting indices of anagrams
     - Hint: Efficient frequency comparison

129. **Substring with Concatenation of All Words** [LeetCode 30]
     - Pattern: Sliding window of words
     - Note: Find indices where all words concatenated
     - Hint: Window size = word length × word count

130. **Longest Substring with At Most K Distinct Characters** [LeetCode 340]
     - Pattern: Variable window with char limit
     - Note: At most k distinct characters
     - Hint: Expand/shrink based on distinct count

---

### 🔴 Hard

131. **Longest Substring with At Most Two Distinct Characters** [LeetCode 159]
     - Pattern: Variable window with constraint
     - Note: At most 2 distinct characters
     - Hint: Simpler version of k distinct

---

---

## 📌 WEEK 6 | DAY 3: PARENTHESES & BRACKET MATCHING

### Category: Stack-Based Pattern Matching

**Pattern Signals:**
- "Valid parentheses/brackets"
- "Matching pairs"
- "Stack structure"

---

### 🟢 Easy

132. **Valid Parentheses** [LeetCode 20]
     - Pattern: Stack for matching
     - Note: Check valid bracket sequence
     - Hint: Push open, pop and match close

133. **Valid Parentheses String** [LeetCode 678]
     - Pattern: Stack with wildcards
     - Note: * can be any bracket or empty
     - Hint: Track min/max possible open brackets

---

### 🟡 Medium

134. **Minimum Add to Make Parentheses Valid** [LeetCode 921]
     - Pattern: Counter or stack
     - Note: Add parentheses to make valid
     - Hint: Count unmatched open and close

135. **Score of Parentheses** [LeetCode 856]
     - Pattern: Stack based scoring
     - Note: Compute score where () = 1, AB = A+B, (A) = 2A
     - Hint: Push/pop and accumulate

136. **Remove Invalid Parentheses** [LeetCode 301]
     - Pattern: Backtracking + BFS
     - Note: Find all valid removals
     - Hint: BFS level-wise validation

137. **Minimum Removals to Make Valid Parentheses** [LeetCode 1541]
     - Pattern: Greedy matching
     - Note: Remove minimum for validity
     - Hint: Count unmatched pairs

---

### 🔴 Hard

138. **Regular Expression Matching** [LeetCode 10]
     - Pattern: DP pattern matching
     - Note: Support . and * wildcards
     - Hint: DP table for matching

139. **Wildcard Matching** [LeetCode 44]
     - Pattern: DP with * wildcard
     - Note: Support ? and * in pattern
     - Hint: Similar to regex but simpler

---

---

## 📌 WEEK 6 | DAY 4: STRING TRANSFORMATIONS & BUILDING

### Category: String Manipulation & Construction

**Pattern Signals:**
- "Transform/edit string"
- "Build new string"
- "Character/word manipulation"

---

### 🟢 Easy

140. **Reverse String** [LeetCode 344]
     - Pattern: Basic string reversal
     - Note: In-place reversal
     - Hint: Two pointers

141. **Reverse String II** [LeetCode 541]
     - Pattern: Reverse k characters
     - Note: Reverse every 2k characters starting from first
     - Hint: Iterate and reverse chunks

142. **Ransom Note** [LeetCode 383]
     - Pattern: Character frequency check
     - Note: Can magazine form ransom note?
     - Hint: Count characters in both

143. **Isomorphic Strings** [LeetCode 205]
     - Pattern: Consistent mapping
     - Note: One-to-one character mapping
     - Hint: Track both directions

---

### 🟡 Medium

144. **Edit Distance** [LeetCode 72]
     - Pattern: DP on strings
     - Note: Minimum edits (insert, delete, replace)
     - Hint: DP table for transformations

145. **Best Time to Buy and Sell Stock with Cooldown** [LeetCode 309]
     - Pattern: State machine / DP
     - Note: Buy/sell with cooldown period
     - Hint: Track states: hold, sold, cooldown

146. **Multiply Strings** [LeetCode 43]
     - Pattern: Digit-by-digit multiplication
     - Note: Multiply two number strings
     - Hint: Array for intermediate results

147. **Add Strings** [LeetCode 415]
     - Pattern: Digit addition with carry
     - Note: Add two number strings
     - Hint: Process from right to left

148. **Reverse Words in a String** [LeetCode 151]
     - Pattern: String manipulation
     - Note: Reverse word order
     - Hint: Split, reverse, rejoin or in-place

149. **Implement strStr()** [LeetCode 28]
     - Pattern: String matching
     - Note: Find substring index
     - Hint: KMP or rolling hash (advanced)

150. **Group Shifted Strings** [LeetCode 249]
     - Pattern: Hash map with custom key
     - Note: Group strings by shift pattern
     - Hint: Encode shift pattern as key

---

### 🔴 Hard

151. **Word Ladder** [LeetCode 127]
     - Pattern: BFS on word transformations
     - Note: Shortest transformation path
     - Hint: Graph of similar words

152. **Word Ladder II** [LeetCode 126]
     - Pattern: BFS + backtracking
     - Note: Find all shortest transformation paths
     - Hint: More complex than Word Ladder

---

---

## 📌 WEEK 6 | DAY 5 (OPTIONAL): STRING MATCHING & ROLLING HASH

### Category: Advanced String Algorithms

**Pattern Signals:**
- "Pattern matching in text"
- "Multiple pattern searches"
- "Rolling hash optimization"

---

### 🟡 Medium

153. **Repeated DNA Sequences** [LeetCode 187]
     - Pattern: Rolling hash for fixed windows
     - Note: Find all 10-letter sequences appearing 2+ times
     - Hint: Rolling hash or polynomial hash

154. **Shortest Palindrome** [LeetCode 214]
     - Pattern: KMP algorithm
     - Note: Add minimum chars to front for palindrome
     - Hint: Find longest palindrome from start

---

---

## ⚠️ GATCHA & COMMON PITFALLS

### Two-Pointer Gotchas

🚨 **Mistake 1: Wrong Movement Logic**
- Symptom: Skipping solutions or wrong answer
- Cause: Didn't think about when to move which pointer
- Fix: Draw example, trace movement carefully
- Example: Container with most water - move SHORTER pointer, not left one

🚨 **Mistake 2: Off-by-One Errors**
- Symptom: Boundary conditions fail
- Cause: Confusion between index and count
- Fix: Test edge cases (n=1, n=2, empty array)
- Example: Remove Nth Node - dummy node avoids this

🚨 **Mistake 3: Not Handling Duplicates**
- Symptom: Wrong count or missing elements
- Cause: Skipping processing of duplicates
- Fix: Explicitly check and handle duplicates
- Example: 3Sum - skip duplicate elements with while loops

---

### Sliding Window Gotchas

🚨 **Mistake 1: Wrong Window Size Calculation**
- Symptom: Off-by-one errors in window count
- Cause: Confusion between inclusive/exclusive indices
- Fix: Verify: window_size = right - left + 1
- Example: Length from indices [3,7] = 7-3+1 = 5 ✓

🚨 **Mistake 2: Forgetting to Shrink**
- Symptom: Time limit exceeded or wrong answer
- Cause: Left pointer never moves for variable window
- Fix: Always shrink when constraint violated
- Example: At most k distinct → shrink until distinct ≤ k

🚨 **Mistake 3: Frequency Map Synchronization**
- Symptom: Logic error in condition checking
- Cause: Frequency map out of sync with actual window
- Fix: Update frequency when adding/removing elements
- Example: Minimum window substring - remove from map when shrinking

🚨 **Mistake 4: Comparing Full Maps**
- Symptom: Time limit or slow performance
- Cause: Comparing full frequency maps each step
- Fix: Track "how many chars have sufficient count"
- Example: Instead of checking map equality, count matches

---

### Divide & Conquer Gotchas

🚨 **Mistake 1: Not Base Case**
- Symptom: Stack overflow, infinite recursion
- Cause: Missing or wrong base case
- Fix: Define base case clearly before recursion
- Example: Merge sort - base case is single element

🚨 **Mistake 2: Not Combining Results**
- Symptom: Returns wrong answer despite correct subproblems
- Cause: Subproblems solved but not merged correctly
- Fix: Make sure merge/combine logic is correct
- Example: Merge sorted lists - compare and pick smaller

---

### Binary Search Gotchas

🚨 **Mistake 1: Integer Overflow**
- Symptom: mid = right + left causes overflow (rare in Python)
- Cause: Old way: mid = (left + right) / 2
- Fix: mid = left + (right - left) / 2
- Example: Large array indices can overflow

🚨 **Mistake 2: Boundary Condition Logic**
- Symptom: Infinite loop or missing answer
- Cause: Wrong loop condition (while left <= right vs <)
- Fix: Understand what <= means vs <
- Example: left = mid + 1 (not mid) to avoid infinite loop

🚨 **Mistake 3: Feasibility Check Wrong**
- Symptom: Binary search converges to wrong answer
- Cause: Feasibility function is incorrect
- Fix: Test feasibility function thoroughly
- Example: "Can ship in D days with capacity C?" - calculate actual days

---

### Hash Map Gotchas

🚨 **Mistake 1: Mutable Keys**
- Symptom: KeyError or lost entries (in some languages)
- Cause: Using mutable objects as keys (lists, dicts)
- Fix: Use immutable keys (strings, tuples)
- Example: Key = tuple(sorted(word)) not list

🚨 **Mistake 2: Checking Existence Wrong**
- Symptom: KeyError or None confusion
- Cause: Not checking if key exists before accessing
- Fix: Use .get(key, default) or check in dict
- Example: `freq.get(char, 0) + 1` instead of freq[char] + 1

🚨 **Mistake 3: Not Clearing/Resetting**
- Symptom: State from previous iteration affects current
- Cause: Hash map not reset between iterations
- Fix: Create new map or clear explicitly
- Example: Start fresh freq map for each substring

---

### Monotonic Stack Gotchas

🚨 **Mistake 1: Storing Value vs Index**
- Symptom: Can't track position or time
- Cause: Stored values instead of indices
- Fix: Store indices (especially for distance tracking)
- Example: Next greater element - store index, not value

🚨 **Mistake 2: Wrong Stack Invariant**
- Symptom: Doesn't find correct answer
- Cause: Stack order wrong (decreasing vs increasing)
- Fix: Draw example, verify invariant before/after
- Example: Daily temperatures - decreasing temperatures in stack

🚨 **Mistake 3: Popping Too Much**
- Symptom: Missing valid answers
- Cause: Pop when shouldn't (wrong condition)
- Fix: Clear condition for when to pop
- Example: Only pop while current > stack.top(), not ≥

---

### Interval Gotchas

🚨 **Mistake 1: Overlap Definition**
- Symptom: Wrong interval merging
- Cause: Incorrect overlap condition (> vs >=)
- Fix: [1,2] and [2,3] overlap iff check properly
- Example: Overlap iff start2 <= end1 (not <)

🚨 **Mistake 2: Sorted Order Assumption**
- Symptom: Wrong result because not sorted
- Cause: Assumed input is sorted when it's not
- Fix: Always sort intervals by start
- Example: Merge intervals requires sorting first

🚨 **Mistake 3: In-Place Modification**
- Symptom: Modifying while iterating
- Cause: Not careful with indices during merge
- Fix: Use separate result list, don't modify input
- Example: Don't merge intervals by modifying input list

---

### Partition Gotchas

🚨 **Mistake 1: Not Swapping Correctly**
- Symptom: Final arrangement is wrong
- Cause: Swap logic broken (swapped wrong elements)
- Fix: Verify swap before/after values
- Example: Swap with temp variable to be safe

🚨 **Mistake 2: Index Out of Bounds**
- Symptom: IndexError or wrong boundary
- Cause: Pointer went past valid range
- Fix: Check boundary conditions carefully
- Example: mid pointer should stop before end+1

---

### Kadane's Gotchas

🚨 **Mistake 1: Not Tracking Result**
- Symptom: Returns wrong maximum
- Cause: Updated current_max but forgot to update max
- Fix: Update max after each iteration
- Example: max_sum = max(max_sum, current_sum)

🚨 **Mistake 2: Empty Subarray**
- Symptom: Returns 0 when shouldn't
- Cause: Allowed empty subarray
- Fix: Subarray must have at least one element
- Example: At least one element requirement

🚨 **Mistake 3: Forgetting About Negatives**
- Symptom: Max product issues
- Cause: Product can flip sign with negatives
- Fix: Track both max and min products
- Example: max_prod = max(nums[i], max_prod * nums[i], min_prod * nums[i])

---

### Fast/Slow Pointer Gotchas

🚨 **Mistake 1: Null Pointer Check**
- Symptom: NullPointerException or RuntimeError
- Cause: Didn't check if next/next.next is null
- Fix: Always check linked list pointers
- Example: while fast && fast.next before accessing fast.next.next

🚨 **Mistake 2: Modifying While Iterating**
- Symptom: Broken links or wrong result
- Cause: Modified pointers while traversing
- Fix: Be careful with pointer manipulations
- Example: Save next pointer before changing links

---

### String/Palindrome Gotchas

🚨 **Mistake 1: Case Sensitivity**
- Symptom: "A" and "a" treated as different
- Cause: Didn't convert to same case
- Fix: Convert to lowercase before comparing
- Example: s.lower() or s.upper()

🚨 **Mistake 2: Non-Alphanumeric Characters**
- Symptom: Wrong palindrome check with spaces/punctuation
- Cause: Didn't skip non-alphanumeric
- Fix: Only compare alphanumeric characters
- Example: Skip spaces, punctuation, symbols

🚨 **Mistake 3: Expand Around Center Logic**
- Symptom: Missing palindromes or counting duplicates
- Cause: Wrong expansion (didn't handle even/odd)
- Fix: Check both odd (single char) and even (pair) centers
- Example: For each char, expand odd and even separately

🚨 **Mistake 4: Substring vs Subsequence**
- Symptom: Wrong answer type
- Cause: Confused substring (contiguous) with subsequence
- Fix: Substring = contiguous, subsequence = any selection
- Example: Palindrome substring requires contiguous characters

---

---

## 🎯 PATTERN RECOGNITION GUIDE

### How to Identify Which Pattern to Use

#### 🔍 Problem Reading Process

1. **Read problem carefully**
   - Identify constraints (n size, time limit)
   - Note any special conditions
   - Look for clues about structure

2. **Look for pattern signals**
   - Key phrases indicate algorithms
   - Structure gives hints
   - Constraints narrow options

3. **Draw examples**
   - Visual representation helps
   - Trace algorithm on example
   - Verify before coding

4. **Code confidently**
   - Pattern identified → implementation clear
   - Edge cases from pattern
   - Testing straightforward

---

### Two-Pointer Signals

**When to Use:**
- ✅ "Find two numbers that sum to target"
- ✅ "Partition array into X and Y"
- ✅ "Rearrange in-place"
- ✅ "Valid palindrome (ignoring chars)"
- ✅ "Remove/move elements in array"

**When NOT to Use:**
- ❌ "Find all pairs" (might need different approach)
- ❌ "Needs sorting but can't sort" (invalid constraint)
- ❌ Problem requires element frequencies

**Examples That Match:**
- Container with most water → Two pointers from ends
- 3Sum → Two pointers inside loop
- Remove duplicates → Two pointer position tracking
- Valid palindrome → Two pointers converging

---

### Sliding Window Signals

**When to Use (Fixed):**
- ✅ "Maximum/minimum in every k-window"
- ✅ "Fixed window size"
- ✅ "Aggregate (sum, product, max) in window"
- ✅ "All k-windows in O(n) time"

**When to Use (Variable):**
- ✅ "Longest substring with property X"
- ✅ "Shortest substring with property X"
- ✅ "At most k distinct characters"
- ✅ "Window that satisfies constraint"

**When NOT to Use:**
- ❌ Problem doesn't have "window" structure
- ❌ Expansion/shrinking doesn't make sense
- ❌ Constraint is not monotonic

**Examples That Match:**
- Max in every window → Fixed window, monotonic deque
- Longest substring without repeating → Variable window, hash map
- Minimum window substring → Variable window, frequency counting

---

### Divide & Conquer Signals

**When to Use:**
- ✅ Problem can split into independent subproblems
- ✅ Solutions combine easily
- ✅ Recursive structure visible
- ✅ Reducing problem size matters

**When NOT to Use:**
- ❌ Subproblems overlap significantly (use DP)
- ❌ Can't effectively combine solutions
- ❌ Base case unclear

**Examples That Match:**
- Merge sort → Divide array, merge sorted pieces
- Maximum subarray → Divide at midpoint, merge results
- Tree inversion → Recursively invert left and right

---

### Binary Search Signals

**When to Use (Direct):**
- ✅ "Find element in sorted array"
- ✅ Target array is sorted or partially sorted

**When to Use (Answer Space):**
- ✅ "Find minimum X such that condition(X) is true"
- ✅ Feasibility function exists (true/false)
- ✅ Answer space has monotonic property

**When NOT to Use:**
- ❌ Array not sorted (unless rotating, etc.)
- ❌ Feasibility function undefined
- ❌ Simple linear search is cleaner

**Examples That Match:**
- Find target in sorted array → Standard binary search
- Capacity to ship in D days → Binary search on capacity (answer)
- Find first bad version → Binary search on version number

---

### Hash Map Signals

**When to Use:**
- ✅ "Count frequency"
- ✅ "Find duplicates"
- ✅ "Group by property"
- ✅ "Lookup/complement search"

**When NOT to Use:**
- ❌ Don't need O(1) lookup
- ❌ Array operations are cleaner

**Examples That Match:**
- Two sum → Hash map for complement
- Group anagrams → Hash map with signature key
- Top K frequent → Hash map + sorting/heap

---

### Monotonic Stack Signals

**When to Use:**
- ✅ "Next greater/smaller element"
- ✅ "Find spans where property holds"
- ✅ "Efficient O(n) scan avoiding O(n²)"

**When NOT to Use:**
- ❌ Need element values directly (not indices)
- ❌ Stack overhead not justified

**Examples That Match:**
- Daily temperatures → Monotonic decreasing stack
- Largest rectangle in histogram → Monotonic increasing stack

---

### Interval Signals

**When to Use:**
- ✅ "Merge overlapping intervals"
- ✅ "Check meeting room conflicts"
- ✅ "Insert into interval list"

**When NOT to Use:**
- ❌ Intervals don't overlap/conflict

**Examples That Match:**
- Merge intervals → Sort by start, merge overlapping
- Meeting rooms → Check consecutive overlaps
- Insert interval → Process before/during/after

---

### Partition/Cyclic Sort Signals

**When to Use:**
- ✅ "In-place rearrangement"
- ✅ "O(1) space constraint"
- ✅ "Reorder with specific property"

**When NOT to Use:**
- ❌ Extra space allowed (use simpler approach)

**Examples That Match:**
- Sort colors → In-place partition into 3 sections
- Find missing number → Cyclic sort to positions

---

### Kadane's Signals

**When to Use:**
- ✅ "Maximum subarray sum"
- ✅ "Maximum product subarray"
- ✅ "Contiguous elements"

**When NOT to Use:**
- ❌ Non-contiguous subsequence
- ❌ Need specific combination (use DP)

**Examples That Match:**
- Maximum subarray → Kadane's O(n)
- Maximum product subarray → Kadane's with min tracking

---

### Fast/Slow Pointer Signals

**When to Use:**
- ✅ "Linked list cycle detection"
- ✅ "Find middle of linked list"
- ✅ "Linked list manipulation"

**When NOT to Use:**
- ❌ Array problem (two pointers easier)

**Examples That Match:**
- Linked list cycle → Floyd's algorithm
- Middle of list → Fast moves 2, slow moves 1
- Palindrome linked list → Find middle, reverse, compare

---

### String Pattern Signals

**Palindrome:**
- ✅ "Check/find palindromes"
- ✅ "Expand around center"

**Substring with Sliding Window:**
- ✅ "Longest/shortest substring"
- ✅ "Character constraints"

**Parentheses/Brackets:**
- ✅ "Valid bracket sequence"
- ✅ "Stack-based matching"

**String Transformations:**
- ✅ "Edit distance"
- ✅ "String building"

**String Matching:**
- ✅ "Pattern in text"
- ✅ "Rolling hash"

---

---

## 💡 TIPS & TRICKS FOR PROBLEM SOLVING

### General Tips

#### 1. **Read Problem Multiple Times**
- First read: Understand what's being asked
- Second read: Note constraints and edge cases
- Third read: Plan approach before coding

#### 2. **Draw Examples**
- Visual representation clarifies confusion
- Trace algorithm on example before coding
- Examples test edge cases early

#### 3. **State Your Assumptions**
- Clarify ambiguities before coding
- Document assumptions about input
- Prevents implementing wrong solution

#### 4. **Code Incrementally**
- Write one part, test
- Don't try to code entire solution at once
- Easier to debug incrementally

#### 5. **Handle Edge Cases**
- Empty input (n=0)
- Minimum input (n=1)
- Maximum input (n=10^6)
- Negative numbers
- Duplicate elements
- All same elements

---

### Two-Pointer Tricks

#### Trick 1: **Dummy Node for Linked Lists**
```
Create dummy node pointing to head
Avoids special case for modifying head
```

#### Trick 2: **Move Shorter Pointer**
```
In container with most water:
Move shorter wall (more room for improvement)
```

#### Trick 3: **Sort for Two-Pointer**
```
Many two-pointer problems need sorted input
Sorting enables two-pointer approach
```

---

### Sliding Window Tricks

#### Trick 1: **Track Distinct Count**
```
Instead of comparing maps each step:
Track count of distinct chars with required frequency
More efficient than full map comparison
```

#### Trick 2: **Use Hash Map for Positions**
```
Store last position of each element
Helps calculate distance/span easily
```

#### Trick 3: **Monotonic Deque for Extremes**
```
For max/min in window:
Use monotonic deque instead of heap
O(n) instead of O(n log n)
```

---

### Divide & Conquer Tricks

#### Trick 1: **Identify Subproblem Structure**
```
Not all recursive problems are divide & conquer
Must have clear split and merge strategy
```

#### Trick 2: **Prove Correctness by Induction**
```
Base case: One element
Inductive step: Assume subproblems solved, prove whole solved
```

---

### Binary Search Tricks

#### Trick 1: **Binary Search on Answer Space**
```
Transform problem to: "Find minimum X where condition(X)"
Then binary search X
```

#### Trick 2: **Use Invariant**
```
Maintain invariant: answer in [left, right]
Loop while left < right
Answer is left at end
```

#### Trick 3: **Watch Mid Calculation**
```
Use: mid = left + (right - left) / 2
Not: mid = (left + right) / 2 (overflow risk)
```

---

### Hash Map Tricks

#### Trick 1: **Use Default Carefully**
```
.get(key, default) for safe access
Prevents KeyError
```

#### Trick 2: **Value Can Be Complex**
```
Map value to list/count/object for more info
Example: Map char to list of indices
```

#### Trick 3: **Two-Pass Technique**
```
First pass: Build frequency/mapping
Second pass: Use mapping for solution
```

---

### Monotonic Stack Tricks

#### Trick 1: **Store Indices, Not Values**
```
Indices give position info
Values needed for comparison only
```

#### Trick 2: **Know the Invariant**
```
Always know: what order is maintained?
Decreasing? Increasing? Why?
```

#### Trick 3: **Pop When Useful**
```
Pop = process that element
Know what question you're answering when you pop
```

---

### Interval Tricks

#### Trick 1: **Always Sort First**
```
Sort intervals by start position
Then process in order
```

#### Trick 2: **Track Last Interval**
```
Keep track of last merged interval
Extend or start new based on overlap
```

---

### Partition Tricks

#### Trick 1: **Three Pointers for 3-Way Partition**
```
Colors/flags → 0, 1, 2 pointers
Divide array into sections
```

#### Trick 2: **Place Elements at Index i**
```
For cyclic sort:
Number i should be at index i
Use swaps until correct placement
```

---

### Kadane's Tricks

#### Trick 1: **Track Both Max and Min**
```
For max product subarray:
Negative × Negative = Positive
Track min_product too
```

#### Trick 2: **Decision Point**
```
current_max = max(nums[i], current_max + nums[i])
Start new or extend? Decision made each step
```

---

### Fast/Slow Pointer Tricks

#### Trick 1: **Set Gap Intentionally**
```
Fast ahead by n positions
Creates gap between fast and slow
Useful for removing nth element
```

#### Trick 2: **Meet in Middle**
```
Fast moves 2, slow moves 1
Meet at middle (useful for finding middle)
```

#### Trick 3: **Cycle Detection**
```
If cycle exists, fast will lap slow
Where they meet not necessarily cycle start
```

---

### String Tricks

#### Trick 1: **Expand Around Center**
```
For palindromes:
Center can be single char or between chars
Two cases: odd and even length palindromes
```

#### Trick 2: **Skip Non-Alphanumeric**
```
When ignoring chars:
Check isalnum() or custom condition
Skip in two-pointer approach
```

#### Trick 3: **Frequency Map for Anagrams**
```
Two strings are anagrams if same char frequencies
Compare frequency maps for equality
```

---

### Algorithm Selection Tricks

#### Trick 1: **If in Doubt, Draw**
```
Picture problem clearly
Often reveals pattern
Example: Max water container → obvious with diagram
```

#### Trick 2: **Think Backwards**
```
Sometimes approaching from end is easier
Example: Reverse string, reverse array
```

#### Trick 3: **Use Standardized Templates**
```
Two-pointer has pattern
Sliding window has pattern
Each algorithm has standard structure
Memorize and apply
```

#### Trick 4: **Optimize After First Solution**
```
Get first solution working
Then optimize for time/space
Better than trying to optimize while coding
```

---

---

## 🔄 WHEN TO CHOOSE WHICH PATTERN

### Decision Matrix

```
├─ Sorted Array?
│  ├─ YES → Binary search
│  └─ NO → Check problem type
│
├─ Need to Find Pair/Triplet?
│  ├─ Two Numbers → Hash map or Two-pointer (if sorted)
│  ├─ Three Numbers → Sort + Two-pointer inside loop
│  └─ Multiple → Different approach
│
├─ Window-Based Problem?
│  ├─ Fixed Window Size → Sliding window (fixed)
│  ├─ Variable Window → Sliding window (variable)
│  └─ No Window → Different approach
│
├─ Linked List Operation?
│  ├─ Cycle Detection → Fast/Slow pointers
│  ├─ Find Middle → Fast/Slow pointers
│  ├─ Reverse → Two pointers or recursion
│  └─ Remove → Dummy node technique
│
├─ Minimize/Maximize on Array?
│  ├─ Subarray Sum → Kadane's algorithm
│  ├─ Frequency → Hash map
│  ├─ Partitioning → Two pointers
│  └─ Extremes → Monotonic stack
│
├─ String Problem?
│  ├─ Palindrome → Expand around center
│  ├─ Substring Property → Sliding window
│  ├─ Bracket Matching → Stack
│  ├─ Anagrams → Hash map
│  └─ Edit Distance → Dynamic programming
│
└─ Constraint: O(1) Extra Space?
   ├─ Yes → In-place algorithms
   │        Partition, cyclic sort, two pointers
   └─ No → More flexibility
            Use maps, stacks, etc.
```

---

### Quick Decision Table

| Problem Type | Signal | Algorithm | Time | Space |
|---|---|---|---|---|
| Find pair sum target | Sorted array | Two pointer | O(n) | O(1) |
| Find pair sum target | Unsorted array | Hash map | O(n) | O(n) |
| Max/min in window | Fixed size | Sliding + deque | O(n) | O(k) |
| Longest substring | Property X | Sliding window | O(n) | O(k) |
| Max subarray sum | Any array | Kadane's | O(n) | O(1) |
| Cycle detection | Linked list | Fast/slow | O(n) | O(1) |
| Check palindrome | String | Two pointer | O(n) | O(1) |
| Merge intervals | Multiple | Sort + merge | O(n log n) | O(n) |
| Next greater | Array | Monotonic stack | O(n) | O(n) |
| Partition array | In-place | Two/three pointer | O(n) | O(1) |

---

### How to Approach Unknown Problem

1. **Understand Constraints** (Usually gives hint)
   - n ≤ 100 → Simple O(n²) okay
   - n ≤ 10^5 → O(n log n) or O(n) needed
   - n ≤ 10^6 → O(n) or O(n log n) required

2. **Identify Structure**
   - Array? String? Linked List? Tree? Graph?
   - Input properties? Sorted? Distinct? Range?

3. **Think of Patterns**
   - Does it match any known pattern?
   - Two pointers? Sliding window? DP? Graph?

4. **Start Simple**
   - O(n²) brute force often first step
   - Optimize from there

5. **Test Edge Cases**
   - Empty input
   - Single element
   - All same
   - Negative values
   - Min/max values

---

---

## 📋 PROGRESS TRACKING

### How to Track Your Progress

Use this checklist as you solve problems:

```
WEEK 4:
  Day 1: Two Pointers
    [ ] Easy: Problems 1-5 (5/5)
    [ ] Medium: Problems 6-13 (8/8)
    [ ] Hard: Problems 14-17 (4/4)
    ☐ COMPLETE: ___/17 problems
  
  Day 2: Sliding Window (Fixed)
    [ ] Easy: Problems 18-22 (5/5)
    [ ] Medium: Problems 23-27 (5/5)
    [ ] Hard: Problems 28 (1/1)
    ☐ COMPLETE: ___/11 problems
  
  Day 3: Sliding Window (Variable)
    [ ] Easy: Problems 29-33 (5/5)
    [ ] Medium: Problems 34-40 (7/7)
    [ ] Hard: Problems 41 (1/1)
    ☐ COMPLETE: ___/13 problems
  
  Day 4: Divide & Conquer
    [ ] Easy: Problems 42-44 (3/3)
    [ ] Medium: Problems 45-50 (6/6)
    [ ] Hard: Problems 51-52 (2/2)
    ☐ COMPLETE: ___/11 problems
  
  Day 5: Binary Search
    [ ] Easy: Problems 53-55 (3/3)
    [ ] Medium: Problems 56-62 (7/7)
    [ ] Hard: Problems 63-64 (2/2)
    ☐ COMPLETE: ___/12 problems

WEEK 4 TOTAL: ___/64 problems

WEEK 5:
  Day 1: Hash Maps
    [ ] Easy: Problems 65-70 (6/6)
    [ ] Medium: Problems 71-76 (6/6)
    [ ] Hard: Problems 77 (1/1)
    ☐ COMPLETE: ___/13 problems
  
  [Continue for Days 2-5...]

WEEK 5 TOTAL: ___/33 problems

WEEK 6:
  [Similar tracking for Week 6...]

WEEK 6 TOTAL: ___/35+ problems

═══════════════════════════════════
GRAND TOTAL: ___/130+ PROBLEMS
═══════════════════════════════════
```

---

---

## 🎓 FINAL ADVICE

### What Matters Most

1. **Understanding Over Memorization**
   - Don't memorize solutions
   - Understand the pattern
   - Apply to new problems

2. **Consistency Over Speed**
   - Solve 2-3 problems daily
   - Better than 20 in one day
   - Reinforces patterns

3. **Quality Over Quantity**
   - Understand each problem deeply
   - Solve edge cases
   - Optimize solutions
   - Better than rushing through

4. **Mistakes as Learning**
   - Each mistake is a lesson
   - Understand why you failed
   - Prevent future mistakes

### When You Get Stuck

1. **Don't immediately look at solution**
   - Think for 15-20 minutes
   - Draw examples
   - Try different approaches

2. **Look at hints, not solution**
   - Hints guide without giving away
   - Helps you arrive at answer
   - Better for learning

3. **Come back to hard problems**
   - Don't spend > 1 hour on one
   - Mark as hard, come back
   - Revisit after learning more

### Before Interview

- Solve all 80+ problems at least once
- Re-solve 20 hardest problems
- Explain solution out loud
- Time yourself (20-30 min per problem)
- Review common mistakes

---

**End of LeetCode Practice List for Weeks 4-6**

*80+ Problems organized by pattern*
*Comprehensive gatcha section*
*Tips and tricks throughout*
*Clear progression from Easy to Hard*
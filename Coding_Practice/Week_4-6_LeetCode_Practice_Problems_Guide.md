# 🎯 LEETCODE PRACTICE PROBLEMS: WEEKS 4-6 COMPREHENSIVE GUIDE

**📊 Problem Set Scope:** Mixed Problems covering Phase B Core Patterns & String Manipulation
**🎓 Difficulty Range:** Easy → Medium → Hard
**📈 Total Problems:** 85+ curated problems
**⏰ Estimated Time:** 40-50 hours of focused practice
**🏆 Mastery Focus:** Problem-solving patterns, not just brute force solutions

---

## 📚 TABLE OF CONTENTS

1. **How to Use This Guide**
2. **Pattern Quick Reference**
3. **Week 4 Problems: Two-Pointers, Sliding Window, Divide & Conquer**
4. **Week 5 Problems: Critical Patterns (Hash Maps, Monotonic Stack, Fast-Slow)**
5. **Week 6 Problems: String Patterns (Palindromes, Brackets, Substrings)**
6. **🚨 Common Gotchas & What to Avoid**
7. **🎯 When to Choose Which Pattern**
8. **💡 Tips & Tricks for Problem-Solving Instincts**
9. **🔍 Approach Framework for Each Problem**

---

## 🎓 HOW TO USE THIS GUIDE

### 📋 Problem Structure

Each problem includes:
- **Problem Name & LeetCode ID**
- **Difficulty Rating** (Easy 🟢, Medium 🟡, Hard 🔴)
- **Key Topics** (which patterns apply)
- **Why This Problem** (what you learn)
- **Hints** (when needed, very minimal)

### 🚀 Recommended Workflow

For **each problem**:

```
1. Read problem carefully (5 mins)
   └─ Identify key constraints & requirements
   
2. Identify pattern (2 mins)
   └─ Two-pointers? Sliding window? Hash map?
   
3. Plan approach (3-5 mins)
   └─ Write pseudocode, trace example
   
4. Code solution (10-20 mins)
   └─ Implement in your language
   
5. Test & optimize (5-10 mins)
   └─ Edge cases, time/space complexity
   
Total per problem: 25-40 mins (Easy), 40-60 mins (Medium), 60-90 mins (Hard)
```

### 📊 Difficulty Progression

- **🟢 Easy (20 problems):** Foundation, warm-up
- **🟡 Medium (50 problems):** Core patterns, interview-style
- **🔴 Hard (15 problems):** Optimization, advanced techniques

**Recommended approach:**
- Day 1-2: Easy problems (pattern foundation)
- Day 3-4: Medium problems (pattern mastery)
- Day 5+: Hard problems (optimization & combinations)

---

## 🎯 PATTERN QUICK REFERENCE

### Two-Pointer Pattern ➡️⬅️

**When to use:**
- Array manipulation in-place
- Convergent search (start & end)
- Opposite-end operations

**Key indicators:**
- "Remove in-place" or "modify array"
- Sorted array, find pair/triplet
- Container problems

**Time: O(n) | Space: O(1)**

### Sliding Window 🪟

**When to use:**
- Substring/subarray problems
- Fixed or variable window size
- Optimize nested loops to single pass

**Key indicators:**
- "Longest", "shortest" substring
- "Contiguous subarray"
- Constraint-based window

**Time: O(n) | Space: O(alphabet size)**

### Hash Map/Set 🗺️

**When to use:**
- Frequency counting
- Anagrams, duplicates
- Membership queries

**Key indicators:**
- "Find pairs", "group by", "count"
- Character/number frequency
- Complement patterns

**Time: O(n) | Space: O(n)**

### Monotonic Stack 📚

**When to use:**
- "Next greater/smaller" element
- Range problems (max/min in range)
- Histogram/trapping water

**Key indicators:**
- "Next" or "previous" element
- Maintain order while processing
- Efficient range queries

**Time: O(n) | Space: O(n)**

### Fast-Slow Pointers 🐢🐇

**When to use:**
- Linked list cycle detection
- Finding middle
- Palindrome checking

**Key indicators:**
- Linked list problems
- "Detect cycle", "find middle"
- In-place manipulation

**Time: O(n) | Space: O(1)**

### Divide & Conquer ⚔️

**When to use:**
- Problem naturally splits into subproblems
- Need to combine results
- Recursive structure

**Key indicators:**
- "Merge" operations
- Sorted arrays/lists
- Balanced splits needed

**Time: O(n log n) | Space: O(n) or O(log n)**

---

## 📌 WEEK 4: TWO-POINTERS, SLIDING WINDOW, DIVIDE & CONQUER

### 🏷️ Category 1: TWO-POINTER PATTERNS (Same Direction & Opposite Direction)

#### EASY (🟢) - Foundation

**1. Remove Duplicates from Sorted Array | LC 26**
- Topics: Two-pointer (same direction)
- Why: Fundamental in-place modification
- Goal: Write unique elements to front

**2. Remove Element | LC 27**
- Topics: Two-pointer (same direction), in-place
- Why: Master moving non-matching elements
- Goal: Remove all occurrences of value

**3. Move Zeroes | LC 283**
- Topics: Two-pointer, in-place manipulation
- Why: Moving specific elements pattern
- Goal: All zeros to end, preserve order

**4. Two Sum II - Input Array Is Sorted | LC 167**
- Topics: Two-pointer (opposite direction)
- Why: Sorted array optimized search
- Goal: Find two numbers summing to target

**5. Valid Palindrome | LC 125**
- Topics: Two-pointer (opposite direction), string
- Why: Ignore non-alphanumeric, case-insensitive
- Goal: Check palindrome property

#### MEDIUM (🟡) - Core Patterns

**6. Container With Most Water | LC 11**
- Topics: Two-pointer (opposite), greedy
- Why: Why move from longer line never helps
- Goal: Maximize area between two lines
- 💡 Hint: Area = width × min(height). Move inward from shorter line.

**7. 3Sum | LC 15**
- Topics: Two-pointer, sorting, three-way search
- Why: Extend 2sum to k-sum problem
- Goal: Find all triplets summing to 0
- 💡 Hint: Sort first, fix one element, use 2sum on rest

**8. 3Sum Closest | LC 16**
- Topics: Two-pointer, similar to 3Sum
- Why: Optimization variant of 3Sum
- Goal: Find triplet sum closest to target

**9. Trapping Rain Water | LC 42**
- Topics: Two-pointer, monotonic approach
- Why: Classic two-pointer optimization
- Goal: Calculate water trapped between elevations
- 💡 Hint: For each position, track max heights to left and right

**10. Sort an Array by Parity | LC 905**
- Topics: Two-pointer, partitioning
- Why: In-place partition pattern
- Goal: Even numbers before odd numbers

**11. Sort Colors (Dutch National Flag) | LC 75**
- Topics: Two/Three-pointer, partitioning
- Why: Three-way partitioning in one pass
- Goal: Arrange 0s, 1s, 2s in order

**12. Partition List | LC 86**
- Topics: Two-pointer, linked list
- Why: Two-pointer on linked list
- Goal: Partition list around pivot

---

### 🏷️ Category 2: SLIDING WINDOW - FIXED SIZE (🪟)

#### EASY (🟢) - Foundation

**13. Max Consecutive Ones | LC 485**
- Topics: Sliding window (fixed), counter
- Why: Basic sliding window warmup
- Goal: Find longest sequence of 1s

**14. Squares of Sorted Array | LC 977**
- Topics: Two-pointer or sliding window
- Why: Handle negative numbers in sorted array
- Goal: Return sorted squares

**15. Majority Element | LC 169**
- Topics: Hash map or multiple approaches
- Why: Frequency counting foundation
- Goal: Find element appearing n/2 times

#### MEDIUM (🟡) - Core Patterns

**16. Sliding Window Maximum | LC 239**
- Topics: Monotonic deque, sliding window
- Why: Optimize sliding window max to O(n)
- Goal: Max element in each k-window
- 💡 Hint: Use decreasing deque of indices. Pop front if outside window, back if smaller.

**17. Permutation in String | LC 567**
- Topics: Sliding window, frequency matching
- Why: Pattern matching with fixed window
- Goal: Check if pattern is permutation of substring

**18. Find All Anagrams of Substring | LC 438**
- Topics: Sliding window, anagram matching
- Why: Multiple solutions with same pattern
- Goal: Find starting indices of anagrams

**19. Longest Repeating Character Replacement | LC 424**
- Topics: Sliding window, character counting
- Why: Optimize with frequency tracking
- Goal: Longest after k replacements

**20. Fruit Into Baskets | LC 904**
- Topics: Sliding window, at-most-k pattern
- Why: Variable window with constraint
- Goal: Maximum fruits with at most 2 baskets

---

### 🏷️ Category 3: SLIDING WINDOW - VARIABLE SIZE (🪟)

#### MEDIUM (🟡) - Core Patterns

**21. Longest Substring Without Repeating Characters | LC 3**
- Topics: Sliding window (variable), character map
- Why: Foundational variable window problem
- Goal: Longest substring with unique characters
- 💡 Hint: Expand right, shrink left when duplicate found.

**22. Longest Substring with At Most K Distinct Characters | LC 340**
- Topics: Sliding window (variable), at-most-k pattern
- Why: Constraint-based window management
- Goal: Longest with ≤k distinct characters

**23. Minimum Window Substring | LC 76**
- Topics: Sliding window (variable), character matching
- Why: Complex window validity checking
- Goal: Smallest window containing all pattern chars
- 💡 Hint: Expand to complete, shrink while valid, track best.

**24. Substring with Concatenation of All Words | LC 30**
- Topics: Sliding window, word frequency
- Why: Fixed-size words in substring
- Goal: Starting indices where all words appear exactly once

**25. Longest Repeating Character Replacement | LC 424** (revisit)
- Topics: Sliding window with frequency optimization
- Why: Optimize window validity check

---

### 🏷️ Category 4: DIVIDE & CONQUER (⚔️)

#### MEDIUM (🟡) - Core Patterns

**26. Merge Two Sorted Lists | LC 21** (Linked List version)
- Topics: Merge, two-pointer
- Why: Foundation for larger merge problems
- Goal: Merge two sorted linked lists

**27. Merge K Sorted Lists | LC 23**
- Topics: Merge, divide conquer, heap
- Why: Optimize naive pairwise merging
- Goal: Merge k sorted lists
- 💡 Hint: Use heap approach O(nk log k) or D&C merging pairs.

**28. Merge Sorted Array | LC 88**
- Topics: Merge, two-pointer
- Why: In-place merge optimization
- Goal: Merge two sorted arrays in-place

**29. Majority Element | LC 169** (revisit with divide conquer)
- Topics: Divide conquer or voting
- Why: Multiple approaches teach trade-offs
- Goal: Find element appearing > n/2 times

**30. Missing Number | LC 268**
- Topics: Array math or XOR or sorting
- Why: Multiple approaches comparison
- Goal: Find missing number in 0 to n

#### HARD (🔴) - Advanced

**31. Reverse Pairs | LC 493**
- Topics: Divide conquer, merge sort variant
- Why: Count inversions using merge sort
- Goal: Count pairs (i,j) where i < j and nums[i] > 2*nums[j]
- 💡 Hint: Modified merge sort counts while merging.

**32. Closest Pair of Points | External**
- Topics: Divide conquer, 2D geometry
- Why: Non-trivial divide conquer application
- Goal: Find closest pair distance

---

## 📌 WEEK 5: CRITICAL PATTERNS (Hash Maps, Monotonic Stack, Fast-Slow Pointers)

### 🏷️ Category 5: HASH MAP & HASH SET PATTERNS (🗺️)

#### EASY (🟢) - Foundation

**33. Valid Anagram | LC 242**
- Topics: Hash map, character frequency
- Why: Anagram fundamentals
- Goal: Check if two strings are anagrams

**34. Contains Duplicate | LC 217**
- Topics: Hash set, membership
- Why: Basic deduplication
- Goal: Check if array has duplicates

**35. Contains Duplicate II | LC 219**
- Topics: Hash map, sliding window
- Why: Constraint-based membership
- Goal: Check if duplicate within k indices

**36. Isomorphic Strings | LC 205**
- Topics: Hash map, character mapping
- Why: One-to-one mapping pattern
- Goal: Check if strings follow same pattern

#### MEDIUM (🟡) - Core Patterns

**37. Two Sum | LC 1**
- Topics: Hash map, complement pattern
- Why: Foundational hash map problem
- Goal: Find two numbers summing to target

**38. Group Anagrams | LC 49**
- Topics: Hash map, normalization
- Why: Grouping pattern with normalization
- Goal: Group strings by anagram
- 💡 Hint: Sort characters as key or count frequency as key.

**39. Valid Sudoku | LC 36**
- Topics: Hash set, constraint validation
- Why: Multiple constraints simultaneously
- Goal: Check sudoku validity

**40. Happy Number | LC 202**
- Topics: Hash set, cycle detection
- Why: Cycle detection in number sequences
- Goal: Determine if number converges to 1

**41. Majority Element II | LC 229**
- Topics: Hash map, frequency, voting
- Why: Elements appearing > n/3 times
- Goal: Find all elements appearing > n/3 times

**42. Intersection of Two Arrays | LC 349**
- Topics: Hash set, intersection
- Why: Multiple approaches (hash, sort, two-pointer)
- Goal: Find common elements

**43. Intersection of Two Arrays II | LC 350**
- Topics: Hash map, frequency-aware intersection
- Why: Handle duplicates in intersection
- Goal: Find intersection with duplicates

**44. Ransom Note | LC 383**
- Topics: Hash map, character frequency
- Why: Availability checking pattern
- Goal: Check if can build ransom note from magazine

**45. First Unique Character in String | LC 387**
- Topics: Hash map, two-pass approach
- Why: Two-pass for frequency and position
- Goal: Find first non-repeating character

**46. Majority Element | LC 169** (hash map approach)
- Topics: Hash map, frequency
- Why: Multiple approaches to same problem
- Goal: Element appearing > n/2 times

**47. Top K Frequent Elements | LC 347**
- Topics: Hash map, heap, bucket sort
- Why: Frequency sorting optimization
- Goal: Find k most frequent elements
- 💡 Hint: Frequency can't exceed n. Use bucket sort O(n) not heap O(n log n).

**48. LRU Cache | LC 146**
- Topics: Hash map + doubly-linked list, design
- Why: Combining two data structures
- Goal: Implement LRU cache
- 💡 Hint: Hash map for O(1) access, doubly-linked list for O(1) reorder.

---

### 🏷️ Category 6: MONOTONIC STACK (📚)

#### MEDIUM (🟡) - Core Patterns

**49. Next Greater Element I | LC 496**
- Topics: Monotonic stack, hash map
- Why: Stack-based nearest greater
- Goal: For each element, find next greater
- 💡 Hint: Use decreasing stack. Pop smaller elements, update their answer.

**50. Daily Temperatures | LC 739**
- Topics: Monotonic stack, indices
- Why: Find next warmer day
- Goal: Days until warmer temperature
- 💡 Hint: Store indices in stack, calculate distance when popping.

**51. Next Greater Element II | LC 503**
- Topics: Monotonic stack, circular array
- Why: Handle circular array variant
- Goal: Circular version of next greater

**52. Stock Span Problem | LC 901**
- Topics: Monotonic stack, accumulation
- Why: Efficient span calculation
- Goal: Number of consecutive days with price ≤ current

**53. Largest Rectangle in Histogram | LC 84**
- Topics: Monotonic stack, rectangle area
- Why: Find max rectangle under histogram
- Goal: Maximize rectangle area
- 💡 Hint: For each bar, find left and right boundaries. Use monotonic increasing stack.

**54. Trapping Rain Water II | LC 407** (Hard variant)
- Topics: Monotonic stack or priority queue
- Why: 2D version of trapping water
- Goal: Water volume in 2D elevation map

#### HARD (🔴) - Advanced

**55. Remove K Digits to Get Smallest Number | LC 402**
- Topics: Monotonic stack, greedy
- Why: Greedy + stack for optimization
- Goal: Remove k digits to get smallest number
- 💡 Hint: Use decreasing stack. Pop if current smaller and removals remaining.

**56. Largest Rectangle in Histogram | LC 84** (revisit if needed)
- Topics: Monotonic stack optimization
- Why: O(n) solution is elegant
- Goal: Maximize rectangle area

**57. Maximal Rectangle | LC 85**
- Topics: Monotonic stack applied to 2D
- Why: Reduce 2D to histogram problem
- Goal: Largest rectangle in binary matrix

---

### 🏷️ Category 7: FAST-SLOW POINTERS (🐢🐇)

#### EASY (🟢) - Foundation

**58. Linked List Cycle | LC 141**
- Topics: Fast-slow, cycle detection
- Why: Tortoise and hare algorithm
- Goal: Detect if cycle exists

**59. Linked List Cycle II | LC 142**
- Topics: Fast-slow, cycle start
- Why: Find where cycle starts
- Goal: Find start node of cycle

**60. Middle of Linked List | LC 876**
- Topics: Fast-slow, finding middle
- Why: Simple fast-slow application
- Goal: Find middle of linked list

#### MEDIUM (🟡) - Core Patterns

**61. Palindrome Linked List | LC 234**
- Topics: Fast-slow + reverse, palindrome
- Why: In-place palindrome check on linked list
- Goal: Check if linked list is palindrome

**62. Remove Nth Node From End of List | LC 19**
- Topics: Two-pointer with gap
- Why: Lead pointer creates gap
- Goal: Remove nth node from end

**63. Reorder List | LC 143**
- Topics: Fast-slow, reverse, merge
- Why: Find middle, reverse second, merge
- Goal: Reorder to L1→Ln→L2→Ln-1...

**64. Happy Number | LC 202** (fast-slow approach)
- Topics: Fast-slow, cycle detection in sequences
- Why: Detect cycles in number sequences
- Goal: Determine if number reaches 1

---

## 📌 WEEK 6: STRING PATTERNS (Palindromes, Brackets, Substrings)

### 🏷️ Category 8: PALINDROME PATTERNS (🎭)

#### EASY (🟢) - Foundation

**65. Valid Palindrome | LC 125** (revisit from week 4)
- Topics: Two-pointer, palindrome
- Why: Check palindrome with constraints
- Goal: Valid palindrome ignoring non-alphanumeric

**66. Valid Palindrome II | LC 680**
- Topics: Two-pointer, skip one character
- Why: Allow removing one character
- Goal: Check if palindrome after removing ≤1 char

#### MEDIUM (🟡) - Core Patterns

**67. Longest Palindromic Substring | LC 5**
- Topics: Palindrome, expand around center
- Why: O(n²) expand around center vs O(n) Manacher
- Goal: Find longest palindromic substring
- 💡 Hint: Expand around each center (odd and even). Track longest.

**68. Palindrome Partitioning | LC 131**
- Topics: Backtracking, palindrome DP
- Why: Partition string into palindromic parts
- Goal: All partitions into palindromes
- 💡 Hint: Backtracking with memoized palindrome checks.

**69. Palindrome Partitioning II | LC 132**
- Topics: DP, palindrome
- Why: Minimum cuts needed
- Goal: Minimum cuts for all palindromic parts

**70. Shortest Palindrome by Adding Characters | LC 214**
- Topics: KMP, palindrome
- Why: Add minimum prefix to make palindrome
- Goal: Shortest palindrome by prepending

#### HARD (🔴) - Advanced

**71. Manacher's Algorithm (Optional) | External**
- Topics: Advanced palindrome
- Why: Linear time palindrome finding
- Goal: Find longest palindrome in O(n)

---

### 🏷️ Category 9: BRACKET & PARENTHESES MATCHING (🎯)

#### EASY (🟢) - Foundation

**72. Valid Parentheses | LC 20**
- Topics: Stack, bracket matching
- Why: Foundational stack problem
- Goal: Check if parentheses valid

#### MEDIUM (🟡) - Core Patterns

**73. Generate Parentheses | LC 22**
- Topics: Backtracking, Catalan numbers
- Why: All valid combinations
- Goal: Generate all valid n parentheses
- 💡 Hint: Backtrack with open/close counts. Only add ) if less than open.

**74. Longest Valid Parentheses | LC 32**
- Topics: Stack or DP
- Why: Longest valid substring
- Goal: Length of longest valid parentheses
- 💡 Hint: Stack approach: store indices, push -1 initially.

**75. Minimum Remove to Make Valid Parentheses | LC 1541**
- Topics: Stack or two-pass
- Why: Remove minimum to make valid
- Goal: Remove minimum parentheses

#### HARD (🔴) - Advanced

**76. Score of Parentheses | LC 856**
- Topics: Stack, recursive scoring
- Why: Calculate score of valid parentheses
- Goal: Score = 1 per pair, nested multiply

---

### 🏷️ Category 10: SUBSTRING & ADVANCED PATTERNS

#### MEDIUM (🟡) - Core Patterns

**77. Longest Substring Without Repeating Characters | LC 3** (revisit)
- Topics: Sliding window, character map
- Why: Variable sliding window foundation
- Goal: Longest with unique characters

**78. Repeated DNA Sequences | LC 187**
- Topics: Sliding window, rolling hash
- Why: Hash for efficiency vs brute force
- Goal: All 10-length repeated sequences

**79. Word Break | LC 139**
- Topics: DP, BFS, word dictionary
- Why: Segmentation problem
- Goal: Can segment string using dictionary

**80. Word Ladder | LC 127**
- Topics: BFS, graph, word ladder
- Why: Shortest transformation sequence
- Goal: Minimum steps to transform word

#### HARD (🔴) - Advanced

**81. Word Break II | LC 140**
- Topics: DP, backtracking, memoization
- Why: All possible segmentations
- Goal: All ways to segment string

**82. Substring with Concatenation of All Words | LC 30** (revisit)
- Topics: Sliding window, word frequency
- Why: Fixed-size words in window
- Goal: All starting indices of concatenation

**83. Word Ladder II | LC 126**
- Topics: BFS, backtracking, graph
- Why: All shortest paths
- Goal: All shortest transformation sequences

---

### 🏷️ Category 11: BONUS - PATTERN COMBINATIONS

#### MEDIUM (🟡) - Pattern Mixing

**84. Multiply Strings | LC 43**
- Topics: String, array simulation
- Why: Simulate multiplication
- Goal: Multiply two number strings

**85. Integer to English Words | LC 273**
- Topics: String, recursion
- Why: Convert number to words
- Goal: 1 → "One", 1234567 → "One Million..."

---

## 🚨 COMMON GOTCHAS & WHAT TO AVOID

### ⚠️ SLIDING WINDOW MISTAKES

**Gotcha 1: Forgetting to shrink window**
```
❌ WRONG: Only expand right, never shrink left
✅ RIGHT: Shrink left when constraint violated
```

**Gotcha 2: Clearing data structures incorrectly**
```
❌ WRONG: Using clear() loses character counts
✅ RIGHT: Decrement counts, remove when 0
```

**Gotcha 3: Off-by-one in window size**
```
❌ WRONG: Window size = right - left (missing +1)
✅ RIGHT: Window size = right - left + 1
```

**Gotcha 4: Not handling edge cases**
```
❌ WRONG: Assume window always has elements
✅ RIGHT: Check left ≤ right, handle empty cases
```

---

### ⚠️ TWO-POINTER MISTAKES

**Gotcha 5: Wrong pointer movement direction**
```
❌ WRONG: Both pointers move in same direction for convergent
✅ RIGHT: Start from opposite ends, move toward middle
```

**Gotcha 6: Losing elements during in-place modification**
```
❌ WRONG: Overwrite before using value
✅ RIGHT: Write to first pointer, read from second pointer
```

**Gotcha 7: Off-by-one in index comparisons**
```
❌ WRONG: while (i < j) might miss middle element
✅ RIGHT: while (i ≤ j) includes middle
```

**Gotcha 8: Forgetting to initialize pointers**
```
❌ WRONG: int i, j; (undefined values)
✅ RIGHT: int i = 0, j = n-1; or int i = 0, j = 0;
```

---

### ⚠️ HASH MAP MISTAKES

**Gotcha 9: Collision handling in custom hash**
```
❌ WRONG: Assume perfect hash, no collisions
✅ RIGHT: Handle collision with chaining or probing
```

**Gotcha 10: Modifying map while iterating**
```
❌ WRONG: map.remove() during iteration
✅ RIGHT: Collect items to remove, remove after iteration
```

**Gotcha 11: Case sensitivity in strings**
```
❌ WRONG: Treat 'A' and 'a' as same without lowercasing
✅ RIGHT: Normalize: toLowerCase() or pre-specify case rules
```

**Gotcha 12: Missing edge case - empty input**
```
❌ WRONG: Assume non-empty array
✅ RIGHT: Check size before accessing first/last
```

---

### ⚠️ MONOTONIC STACK MISTAKES

**Gotcha 13: Comparing values vs indices**
```
❌ WRONG: Store values in stack, lose position info
✅ RIGHT: Store indices, access values when needed
```

**Gotcha 14: Not popping all smaller elements**
```
❌ WRONG: Pop once, stop
✅ RIGHT: Pop while condition true (all smaller elements)
```

**Gotcha 15: Empty stack access**
```
❌ WRONG: stack.top() without checking empty()
✅ RIGHT: if (!stack.empty()) stack.top()
```

**Gotcha 16: Off-by-one in distance calculation**
```
❌ WRONG: distance = right - left (should be right - left + 1 or current - prev_index)
✅ RIGHT: Calculate carefully based on what "distance" means
```

---

### ⚠️ FAST-SLOW POINTER MISTAKES

**Gotcha 17: Not detecting end of list**
```
❌ WRONG: Access node.next without null check
✅ RIGHT: Check if (node != null && node.next != null)
```

**Gotcha 18: Infinite loop in cycle detection**
```
❌ WRONG: Assuming fast always catches slow
✅ RIGHT: Verify fast catches slow in cycle (mathematically proven)
```

**Gotcha 19: Modifying list while traversing**
```
❌ WRONG: Remove node while iterating
✅ RIGHT: Track previous node, modify carefully
```

**Gotcha 20: Getting middle for even-length list**
```
❌ WRONG: Assume mid is exact center
✅ RIGHT: Choose convention: upper-mid or lower-mid
```

---

### ⚠️ STRING PATTERN MISTAKES

**Gotcha 21: Unicode vs ASCII assumptions**
```
❌ WRONG: Assume all strings are ASCII
✅ RIGHT: Handle Unicode, special characters appropriately
```

**Gotcha 22: Modifying string directly (immutable)**
```
❌ WRONG: string[i] = 'x' (strings are immutable in many languages)
✅ RIGHT: Convert to char array, modify, convert back
```

**Gotcha 23: Palindrome check without normalization**
```
❌ WRONG: Check case-sensitive, include non-alphanumeric
✅ RIGHT: Normalize: lowercase, ignore non-alphanumeric
```

**Gotcha 24: Substring vs subsequence confusion**
```
❌ WRONG: Treat substring (contiguous) as subsequence (not contiguous)
✅ RIGHT: Clarify problem requirement
```

---

### ⚠️ DIVIDE & CONQUER MISTAKES

**Gotcha 25: Not combining results correctly**
```
❌ WRONG: Merge but lose sorted order
✅ RIGHT: Two-pointer merge ensures order
```

**Gotcha 26: Unbalanced splits**
```
❌ WRONG: Always split at position 1 (degenerates to linear)
✅ RIGHT: Split at middle for O(n log n)
```

**Gotcha 27: Not handling odd-length arrays**
```
❌ WRONG: Only test even-length
✅ RIGHT: Test both even and odd lengths
```

---

## 🎯 WHEN TO CHOOSE WHICH PATTERN

### Decision Tree Flowchart

```
START: Read problem carefully
│
├─ Is it a SUBSTRING/SUBARRAY problem?
│  ├─ YES → Is window size FIXED?
│  │  ├─ YES → Sliding Window (Fixed) → LC 239, LC 567
│  │  └─ NO → Sliding Window (Variable) → LC 3, LC 76
│  └─ NO → Continue
│
├─ Is it asking for a PAIR/TRIPLET or PARTITION?
│  ├─ YES → Is array SORTED?
│  │  ├─ YES → Two-Pointers → LC 167, LC 11
│  │  └─ NO → Sort + Two-Pointers OR Hash Map → LC 1, LC 15
│  └─ NO → Continue
│
├─ Do we need FREQUENCY counting or ANAGRAM matching?
│  ├─ YES → Hash Map/Set → LC 49, LC 242
│  └─ NO → Continue
│
├─ Is it asking for NEXT/PREVIOUS GREATER/SMALLER element?
│  ├─ YES → Monotonic Stack → LC 496, LC 739
│  └─ NO → Continue
│
├─ Is it a LINKED LIST problem?
│  ├─ Cycle detection? → Fast-Slow → LC 141, LC 142
│  ├─ Finding middle? → Fast-Slow → LC 876
│  ├─ Palindrome check? → Fast-Slow + Reverse → LC 234
│  └─ Otherwise → Standard LL operations → LC 21, LC 19
│
├─ Does problem have NATURAL RECURSIVE STRUCTURE?
│  ├─ Merge two sorted? → Divide & Conquer → LC 21, LC 88
│  ├─ Count inversions? → Merge Sort variant → LC 493
│  └─ Otherwise → Recursion/DP may not be needed
│
└─ PALINDROME/BRACKET problems?
   ├─ Palindrome check? → Expand around center → LC 5, LC 125
   ├─ Bracket matching? → Stack → LC 20, LC 22
   └─ Bracket generation? → Backtracking → LC 22

REMEMBER:
- Multiple patterns may apply to same problem
- Choose based on time/space constraints
- Sometimes brute force is faster for small inputs
```

### Quick Pattern Matcher

| Problem Type | Pattern | Complexity | Example |
| :--- | :--- | :--- | :--- |
| Longest substring without repeat | Sliding Window | O(n) | LC 3 |
| Kth largest element | Heap/Sort | O(n log n) | LC 215 |
| Two sum | Hash Map | O(n) | LC 1 |
| Next greater element | Monotonic Stack | O(n) | LC 496 |
| Container with most water | Two-Pointer | O(n) | LC 11 |
| Merge k lists | Divide & Conquer | O(nk log k) | LC 23 |
| Linked list cycle | Fast-Slow | O(n) | LC 141 |
| Largest rectangle | Monotonic Stack | O(n) | LC 84 |
| Longest palindrome | Expand Center | O(n²) | LC 5 |
| Valid parentheses | Stack | O(n) | LC 20 |

---

## 💡 TIPS & TRICKS FOR PROBLEM-SOLVING INSTINCTS

### 🧠 General Instincts to Develop

#### Instinct 1: Recognize Patterns in Problem Statement

**Watch for these keywords:**

```
SLIDING WINDOW Keywords:
  • "substring", "subarray"
  • "longest", "shortest", "minimum", "maximum"
  • "contiguous", "consecutive"
  • "at most", "at least", "exactly k"
  
TWO-POINTER Keywords:
  • "sorted array"
  • "two sum", "pair", "triplet"
  • "palindrome"
  • "in-place", "remove", "partition"
  • "opposite ends", "converge"
  
HASH MAP Keywords:
  • "frequency", "count", "occurrence"
  • "anagram", "duplicates", "unique"
  • "group by", "map to"
  • "find pair with property"
  
MONOTONIC STACK Keywords:
  • "next greater/smaller"
  • "previous greater/smaller"
  • "range maximum/minimum"
  • "largest rectangle"
  
FAST-SLOW POINTER Keywords:
  • "linked list"
  • "cycle", "detect"
  • "middle", "kth node"
  • "in-place", "O(1) space"

DIVIDE & CONQUER Keywords:
  • "merge", "split"
  • "divide into subproblems"
  • "combine results"
  • "inversions", "closest pair"
```

---

#### Instinct 2: Ask Key Questions Before Coding

Before implementing, ask:

```
1. What is the CONSTRAINT?
   • Is array sorted?
   • Size limits? (small n → brute force OK)
   • Space constraints? (O(1) → in-place)
   
2. What does the PROBLEM ask for?
   • Just check validity? → Early termination possible
   • Find ALL? → Need to explore all
   • Find ONE? → Can stop early
   
3. Can I REDUCE nested loops?
   • Two nested O(n²)? → Try hash map or two-pointer
   • Substring problem? → Try sliding window instead of O(n²)
   
4. Do I need PREPROCESSING?
   • Would sorting help? → Check if can modify input
   • Precompute something? → Build frequency map, sorted array
   
5. What EDGE CASES exist?
   • Empty input?
   • Single element?
   • All same elements?
   • Negative numbers, zeros?
```

---

#### Instinct 3: Complexity Analysis Before Coding

**Ask yourself:**

```
Can this problem be solved in O(n)?
  → Likely sliding window, hash map, or single-pass

Can this problem be solved in O(n log n)?
  → Likely sorting + two-pointer, divide & conquer, heap

Is O(n²) acceptable?
  → Check constraints: n ≤ 1000? Then O(n²) OK
  → n ≤ 10⁶? Must be O(n) or O(n log n)

Space requirements?
  → O(1) space → In-place, two-pointer, fast-slow
  → O(n) space → Hash map, arrays allowed
```

---

#### Instinct 4: Test on Paper Before Coding

**For each problem:**

1. **Trace example by hand**
   - Use provided example
   - Trace step-by-step
   - Verify algorithm logic

2. **Identify edge cases FIRST**
   - Empty array/string
   - Single element
   - All duplicates
   - Negative numbers
   - Large numbers

3. **Verify invariants**
   - What stays true throughout?
   - Example: "Elements 0 to slow are unique" (LC 26)

4. **Check termination condition**
   - When does algorithm stop?
   - Guaranteed to finish? (Avoid infinite loops)

---

#### Instinct 5: Pattern Chaining (Multiple Patterns)

Some problems need **multiple patterns combined:**

```
Example: LC 234 (Palindrome Linked List)
  Step 1: Fast-Slow Pointers → Find middle
  Step 2: Reverse list → Reverse second half
  Step 3: Two-Pointer comparison → Check palindrome
  
Example: LC 76 (Minimum Window Substring)
  Step 1: Hash Map → Track character frequencies
  Step 2: Sliding Window → Expand/shrink window
  Step 3: Optimization → Efficient validity checking

Example: LC 23 (Merge K Sorted Lists)
  Option 1: Divide & Conquer → Merge pairs recursively
  Option 2: Heap → Track k list heads, always extract min
```

**When you see pattern chaining needed:**
- Multiple steps to solve
- Each step uses different pattern
- Combine them in correct order

---

### 🎯 Pattern-Specific Tips

#### SLIDING WINDOW Tips

**Tip 1: Expand-Shrink Template**
```
Initialize: left = 0
For each right:
  Add arr[right] to window
  While (window violates constraint):
    Remove arr[left] from window
    left++
  Update best result
```

**Tip 2: Frequency Optimization**
Instead of checking full hash match each iteration:
```
Track "count of characters with correct frequency"
When count == distinct_chars_needed, window valid
Faster than full map comparison
```

**Tip 3: Pre-allocate Fixed Window**
For fixed-size k-windows, build first window completely:
```
Build window[0..k-1]
Then for i from k to n-1:
  Remove element i-k
  Add element i
  Process
```

---

#### TWO-POINTER Tips

**Tip 4: Invariant First**
Define invariant before coding:
```
Example: "All elements [0..i] are unique"
Then rest of code follows from invariant
```

**Tip 5: Direction Matters**
```
Same direction (both left → right):
  - Use for in-place modifications
  - Example: Remove duplicates
  
Opposite direction (left ↔ right):
  - Use for pair finding
  - Greedy: move from longer line (container water)
```

**Tip 6: Boundary Conditions**
```
When handling boundaries:
  - Check i ≤ j (includes middle)
  - Check i < j (excludes one middle)
  - Know why your choice is correct
```

---

#### HASH MAP Tips

**Tip 7: Frequency Map Pattern**
```
1. Build frequency map: O(n)
2. Query or compare: O(alphabet_size)
Not O(n) comparison repeated
```

**Tip 8: Character Count Optimization**
```
Instead of: if (needed.equals(window)) → O(k) per check
Use: if (matched == needed.size()) → O(1) per check

Maintain: "count of chars with correct frequency"
Increment when char becomes valid
Decrement when char becomes invalid
```

**Tip 9: Handle Case Sensitivity**
```
For anagram/palindrome problems:
  • Lowercase all: str.toLowerCase()
  • Or specify: "a-z only" vs including "A-Z"
```

---

#### MONOTONIC STACK Tips

**Tip 10: Store Indices, Not Values**
```
❌ stack.push(arr[i])  // Can't know position of popped element
✅ stack.push(i)       // Index let's us calculate distances
```

**Tip 11: Pop ALL Smaller Elements**
```
while (!stack.empty() && arr[stack.top()] < arr[i]):
  int smaller = stack.pop()
  // Process 'smaller'
```

**Tip 12: Handle Remaining Elements**
```
// After main loop, stack has unmatched elements
while (!stack.empty()):
  int idx = stack.pop()
  // Handle remaining
```

---

#### FAST-SLOW POINTER Tips

**Tip 13: Initialize Correctly**
```
For cycle detection:
  slow = head; fast = head;  // Both start at head
  
For finding middle:
  slow = head; fast = head;  // Both start at head
```

**Tip 14: Movement Pattern**
```
Standard:
  slow = slow.next;
  fast = fast.next.next;
  
Be careful:
  Check if fast.next exists before accessing fast.next.next
```

**Tip 15: After Finding Cycle Start**
```
// First, detect cycle
slow, fast meet at point P in cycle

// Then find cycle start
ptr1 = head; ptr2 = P;
while (ptr1 != ptr2):
  ptr1 = ptr1.next;
  ptr2 = ptr2.next;
// ptr1 == ptr2 == cycle start
```

---

#### DIVIDE & CONQUER Tips

**Tip 16: Merge Sorted Lists Carefully**
```
while (i < len1 && j < len2):
  if (arr1[i] <= arr2[j]):
    result[k++] = arr1[i++];
  else:
    result[k++] = arr2[j++];
// Copy remaining
Copy rest of arr1 if any
Copy rest of arr2 if any
```

**Tip 17: Count Inversions While Merging**
```
During merge, if arr1[i] > arr2[j]:
  All remaining in arr1 form inversion with arr2[j]
  inversions += (len1 - i)
```

---

#### PALINDROME Tips

**Tip 18: Expand Around Each Center**
```
For odd-length: center is single character
For even-length: center is between two characters

Expand both directions simultaneously:
while (left ≥ 0 && right < n && s[left] == s[right]):
  left--;
  right++;
```

**Tip 19: Edge Case - Empty String**
```
Empty string "" is technically a palindrome
Check problem: usually handled correctly automatically
```

---

#### BRACKET Tips

**Tip 20: Stack Template for Brackets**
```
for each char:
  if (opening bracket):
    stack.push(char);
  else:  // closing bracket
    if (stack.empty()) return false;
    if (stack.top() matches char):
      stack.pop();
    else:
      return false;
return stack.empty();
```

---

### 🔥 Advanced Tricks

#### Trick 1: Two-Pass Approach
```
Problem needs information from both directions?
  Pass 1: Left to right (build left data)
  Pass 2: Right to left (build right data)
  Combine results

Example: Trapping rain water
  Left pass: max_height to left of each position
  Right pass: max_height to right of each position
  At each position: trapped = min(left_max, right_max) - height
```

#### Trick 2: Complement Pattern
```
Instead of finding element with property P:
  Find complement: element WITHOUT property P
  Then check if (target - complement) is valid

Example: Two sum
  Instead of: find two that sum to target
  Use: for each num, check if (target - num) exists in set
```

#### Trick 3: Normalization for Comparison
```
To compare complex objects, normalize to canonical form:
  Anagrams → sort characters as key
  Parentheses → balance count
  Fractions → reduced form (gcd)
  
Then equality becomes simple
```

#### Trick 4: Early Termination
```
If problem just asks "does it exist?":
  Return immediately when found
  Don't wait for full result

Example: Contains duplicate
  For each num, check if in set
  Return true immediately when found
```

#### Trick 5: Space-Time Tradeoff
```
DP solutions:
  More space → faster computation
  Less space → slower but memory efficient
  
Choose based on:
  Time limit: prefer time (more space)
  Space limit: prefer space (more time)
```

---

### 🎓 Building True Problem-Solving Instincts

#### How to Develop Intuition

1. **Do 5-10 problems of SAME pattern consecutively**
   - Brain recognizes pattern naturally
   - Variations become obvious
   - Solution structure becomes muscle memory

2. **Explain problem to someone else**
   - If can't explain, don't understand fully
   - Forces clarity

3. **Solve without looking at similar problems**
   - First attempt teaches your brain the pattern
   - Solution ideas are more valuable than code

4. **Review top solutions after solving**
   - Learn alternative approaches
   - See optimizations
   - Understand trade-offs

5. **Time yourself**
   - Fast recognition → Instinct developing
   - Slow recognition → Need more practice
   - Goal: < 5 minutes to identify pattern and approach

---

## 🔍 APPROACH FRAMEWORK FOR EACH PROBLEM

### 5-Step Approach for ANY Problem

#### Step 1: UNDERSTAND (5 minutes)
```
Read problem 2-3 times
Ask:
  ✓ What are inputs and outputs?
  ✓ What are constraints?
  ✓ What defines "correct answer"?
  ✓ Any special cases mentioned?
```

#### Step 2: IDENTIFY (3 minutes)
```
Pattern recognition:
  ✓ Which patterns might apply?
  ✓ What's the data structure?
  ✓ What's the constraint?
  ✓ What should the algorithm do at high level?
```

#### Step 3: PLAN (5 minutes)
```
Pseudocode/approach:
  ✓ Write high-level steps
  ✓ Identify sub-problems
  ✓ Check time/space complexity estimate
  ✓ Verify on example by hand
```

#### Step 4: CODE (15-30 minutes)
```
Implementation:
  ✓ Implement cleanly (not golfing)
  ✓ Variable names: clear and descriptive
  ✓ Comments for non-obvious logic
  ✓ Handle edge cases as discovered
```

#### Step 5: TEST & OPTIMIZE (10 minutes)
```
Verification:
  ✓ Run on provided examples
  ✓ Test edge cases
  ✓ Check complexity again
  ✓ Any optimizations possible?
```

---

## 📌 FINAL CHECKLIST BEFORE SUBMITTING

- [ ] Does code compile without warnings?
- [ ] All edge cases handled?
- [ ] Off-by-one errors checked?
- [ ] Null pointer checks (linked lists)?
- [ ] Array bounds checked?
- [ ] Time complexity acceptable?
- [ ] Space complexity acceptable?
- [ ] Tested on provided examples?
- [ ] Variable names clear?
- [ ] Comments for complex logic?
- [ ] No hardcoded magic numbers?

---

## 🏆 MASTERY PROGRESSION

### Week 4 Mastery Checklist

After Week 4, you should:
- ✅ Solve any two-pointer problem intuitively
- ✅ Recognize sliding window problems instantly
- ✅ Know when to use divide & conquer
- ✅ Solve in < 30 minutes per medium problem

### Week 5 Mastery Checklist

After Week 5, you should:
- ✅ Use hash maps/sets efficiently
- ✅ Design monotonic stack solutions
- ✅ Apply fast-slow pointer techniques
- ✅ Chain multiple patterns when needed

### Week 6 Mastery Checklist

After Week 6, you should:
- ✅ Solve all palindrome variations
- ✅ Handle bracket matching elegantly
- ✅ Recognize substring patterns
- ✅ Combine multiple patterns seamlessly

---

**End of LeetCode Practice Problems Guide: Weeks 4-6**

*85+ curated problems with pattern-based progression*
*Comprehensive gotchas, tips, and approach frameworks*
*Time to mastery: 40-50 hours of focused practice*
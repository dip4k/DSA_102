# 📗 PHASE_B_CORE_PATTERNS_STRING_MANIPULATION_v13_FULL.md

**Version:** 13.0 (Comprehensive Phase B - Weeks 4-6)  
**Status:** ✅ PRODUCTION READY - COMPLETE DETAILED BREAKDOWNS  
**Date:** January 24, 2026

---

# 🟩 PHASE B: CORE PATTERNS & STRING MANIPULATION (Weeks 4-6)

## Phase Motivation
Now that fundamentals are solid, learn reusable problem-solving patterns that apply across hundreds of interview problems. These patterns transform "stuck" into "I recognize this pattern." You'll learn to decompose complex array and string problems into recognizable templates.

## Phase Outcomes
- [ ] Identify and apply two-pointer patterns (same direction, opposite direction)
- [ ] Recognize and solve sliding window problems (fixed and variable size)
- [ ] Use divide & conquer systematically across different domains
- [ ] Apply binary search beyond sorted arrays to answer spaces
- [ ] Understand and apply string manipulation patterns
- [ ] Recognize when to use hashing, monotonic stacks, interval merging, partitioning, Kadane's algorithm
- [ ] Master fast/slow pointer techniques for cycle detection and list manipulation

---

## 📌 WEEK 4: TWO POINTERS, SLIDING WINDOWS, DIVIDE & CONQUER, BINARY SEARCH AS PATTERN

### Weekly Goal
Learn foundational array and sequence patterns that transform problems from confusing to solvable. Master two fundamental techniques: two-pointer manipulation and sliding window exploration.

### Weekly Topics
- Two-pointer techniques (same direction, opposite direction, convergent, divergent)
- Sliding window (fixed and variable size)
- Divide and conquer pattern and applications
- Binary search as a general-purpose optimization pattern

---

### 📅 DAY 1: TWO-POINTER PATTERNS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Same-Direction Pointers (Two Fast Moving in Sequence)**
  - Both pointers starting at beginning, moving in same direction
  - Example: removing duplicates in-place
  - Example: merging two sorted arrays
  - Pattern: fast pointer finds, slow pointer places
  - Invariant: everything before slow pointer is final result
  - Time: O(n), Space: O(1)

- **Opposite-Direction Pointers (Convergent Approach)**
  - One pointer at start, one at end, converging toward middle
  - Example: container with most water
    - Height array, find max area between two lines
    - Greedy choice: move inward from shorter line
    - Why it works: moving from longer line never improves
  - Example: two-sum in sorted array
    - Converge pointers based on sum too large/small
  - Time: O(n), Space: O(1)

- **Invariants & Correctness**
  - Maintain clear invariant as pointers move
  - Example: "elements before slow are sorted and final"
  - Example: "answer is between current pointers or won't appear"
  - Critical for proving algorithm correctness

- **Divergent Approach (Start Middle, Move Outward)**
  - Start at center, expand outward
  - Example: palindrome checking via center
  - Example: trapping rain water (combined with other techniques)

- **Practical Considerations**
  - When to use two pointers vs hash map vs other approaches
  - Space-time tradeoffs
  - Edge cases: empty array, single element, all same

- **Real-World Applications**
  - In-place array manipulation (avoid extra space)
  - Merge operations (essential in distributed systems)
  - Two-sum variants (financial transactions, pair finding)

**Key Insights:**
- Two-pointer elegant for in-place array operations
- Invariants are your proof of correctness
- Different pointer movements for different problems

**Deliverables:**
- [ ] Implement remove duplicates using same-direction pointers
- [ ] Implement container with most water using opposite-direction pointers
- [ ] Implement two-sum in sorted array
- [ ] Prove invariant for each algorithm
- [ ] Identify when two-pointer approach is better than alternatives

---

### 📅 DAY 2: SLIDING WINDOW (FIXED SIZE)

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Fixed-Size Sliding Window Mechanics**
  - Window of size k slides over array
  - At each step: add new element, remove old element
  - Maintain aggregate (sum, min, max, frequency count)
  - O(n) time for O(n) elements with O(k) window

- **Running Sum / Running Average**
  - Sum of k-length subarrays
  - Example: find subarray with sum closest to target
  - Approach: compute all k-sums, find best
  - Complexity: O(n) instead of O(nk) naive

- **Maximum / Minimum in Sliding Window**
  - Naive: O(n) for each window → O(nk) total
  - Better: monotonic deque in O(n) total
    - Maintain deque of indices in decreasing order of values
    - Add: remove from back while new element larger
    - Remove: pop from front if outside window
    - Front of deque is max at each step

- **Frequency-Based Windows**
  - Fixed window size, track character frequencies
  - Example: "permutation in string"
    - Check if pattern is permutation of any substring
    - Compare frequency maps at each window
  - Example: "all anagrams of substring"
    - Find all anagrams in text
    - Slide window and compare frequency maps

- **Monotonic Deque Details**
  - For maximum: keep in decreasing order
  - For minimum: keep in increasing order
  - Efficient: each element added/removed once from deque
  - Complex but powerful for window max/min

- **Space Considerations**
  - Monotonic deque: O(k) space (stores k elements max)
  - Frequency maps: O(alphabet size) usually small

- **Edge Cases**
  - Window larger than array
  - All elements same
  - Very small arrays

**Key Insights:**
- Fixed window enables O(n) processing time
- Monotonic deque elegant for min/max tracking
- Frequency comparison powerful for pattern matching

**Deliverables:**
- [ ] Implement fixed-window sum calculation
- [ ] Implement maximum sliding window using monotonic deque
- [ ] Solve "permutation in string" problem
- [ ] Find all anagrams in text using sliding window
- [ ] Compare naive vs optimized approaches

---

### 📅 DAY 3: SLIDING WINDOW (VARIABLE SIZE)

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Variable-Size Window Concept**
  - Expand window to include more elements
  - Shrink window when constraint violated
  - Goal: find optimal window (min/max/first)
  - Time: O(n) if shrink happens O(n) total times

- **Constraint-Based Windows**
  - Example: longest substring without repeating characters
    - Expand right: add character
    - If duplicate found: shrink from left until no duplicate
    - Track max length seen
  - Example: longest substring with "at most k" distinct characters
    - Expand right: add character
    - If more than k distinct: shrink left
    - Update frequency as you go

- **Two-Pointer Sliding Window**
  - Left pointer: shrink side
  - Right pointer: expand side
  - Invariant: [left, right] is valid window
  - Move right to expand, move left to shrink

- **Frequency Map Tracking**
  - Map character → count in current window
  - On expanding: increment count
  - On shrinking: decrement count (remove if zero)
  - Check constraint: how many distinct? Sum of counts?

- **"At Most k" Pattern**
  - Max elements with at most k distinct characters
  - Window shrinks when constraint violated
  - Shrink continues until constraint satisfied
  - Expand to try including more

- **"At Least k" vs "Exactly k" Patterns**
  - Exactly k distinct: compute at-most-k minus at-most-(k-1)
  - General: frame as counting valid windows

- **Minimum Window Substring Pattern**
  - Find smallest window containing all characters from pattern
  - Expand right until all characters included
  - Shrink left while all characters still present
  - Track best window found

- **Decision Logic**
  - Clear condition for validity
  - Right pointer always expands (until end)
  - Left pointer shrinks when needed
  - Record best seen at each step

**Key Insights:**
- Variable window elegant for optimization within constraints
- Two pointers move independently at different rates
- Frequency tracking enables efficient constraint checking

**Deliverables:**
- [ ] Implement longest substring without repeating characters
- [ ] Implement longest substring with at most k distinct characters
- [ ] Solve minimum window substring problem
- [ ] Implement exactly k distinct characters (using at-most trick)
- [ ] Measure performance vs alternative approaches

---

### 📅 DAY 4: DIVIDE & CONQUER PATTERN

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Divide & Conquer Template**
  - Divide: split problem into subproblems
  - Conquer: recursively solve subproblems
  - Combine: merge solutions into final answer
  - Complexity: typically O(n log n) for balanced splits

- **General Recurrence: T(n) = a×T(n/b) + O(n^d)**
  - a subproblems of size n/b
  - O(n^d) work per level
  - Master theorem gives solution: O(n^d) or O(n^d × log n) or O(n^log_b a)

- **Merge Sort as Divide & Conquer**
  - Divide: split into two halves
  - Conquer: recursively sort halves
  - Combine: merge two sorted halves in O(n)
  - Recurrence: T(n) = 2T(n/2) + O(n) = O(n log n)

- **Counting Inversions**
  - Problem: how many pairs (i, j) where i < j but arr[i] > arr[j]
  - Naive: O(n²) nested loops
  - Merge sort approach:
    - Count inversions in left half
    - Count inversions in right half
    - Count inversions across halves (pairs from left > pairs from right)
    - During merge, track cross-half inversions
  - Time: O(n log n)

- **Majority Element**
  - Find element appearing > n/2 times
  - Divide & conquer: recursively find majority in left/right
  - Combine: check if left or right majority is overall majority
  - Time: O(n log n) (naive) or O(n) with preprocessing

- **Closest Pair of Points**
  - 2D problem: find nearest pair
  - Naive: O(n²) all pairs
  - Divide & conquer:
    - Sort by x-coordinate
    - Recursively find closest in left half
    - Recursively find closest in right half
    - Check pairs spanning the dividing line
  - Complexity: O(n log n)

- **When Divide & Conquer Works**
  - Problem has optimal substructure
  - Can combine solutions efficiently
  - Balanced splits lead to logarithmic depth

- **When It Doesn't Work Well**
  - Unbalanced splits (quadratic time)
  - High combining cost
  - Better addressed by iteration or DP

**Key Insights:**
- Divide & conquer powerful for sorting, searching, optimization
- Recurrence analysis predicts complexity
- Practical implementations often hybrid (use insertion sort for small subarrays)

**Deliverables:**
- [ ] Implement merge sort
- [ ] Count inversions using divide & conquer
- [ ] Implement majority element finder
- [ ] Analyze recurrence for custom divide & conquer problem
- [ ] Compare vs DP or greedy approaches

---

### 📅 DAY 5: BINARY SEARCH AS A PATTERN (ANSWER SPACE)

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Binary Search Beyond Sorted Arrays**
  - Key insight: binary search works on any monotone condition
  - Reframe problem: "Find X such that condition(X) is true"
  - Condition must be monotone: false below threshold, true above (or vice versa)

- **Monotone Condition Concept**
  - Condition must split decision space into two regions
  - Example: "Can we schedule all jobs with capacity C?"
    - Below C: impossible
    - Above C: possible
    - Threshold: find minimum C that works

- **Capacity Planning Pattern**
  - Problem: given tasks with sizes, minimize maximum load
  - Search space: [max_task_size, sum_all_tasks]
  - Condition: "Can we distribute tasks with capacity C?"
  - Check: pack tasks into bins greedily, see if all fit in C
  - Binary search on C

- **Distance Maximization Pattern**
  - Problem: "Aggressive cows" - place N cows in stalls to maximize minimum distance
  - Search space: [1, (max_position - min_position)]
  - Condition: "Can we place N cows with minimum distance D?"
  - Check: greedily place cows, see if we get N or more
  - Binary search on D

- **Food Delivery Pattern**
  - Problem: minimize time to deliver all packages
  - Search space: [1, total_time_alone]
  - Condition: "Can person 1 deliver K packages in time X?"
  - Check: greedily assign packages
  - Binary search on time X

- **Feasibility Check Implementation**
  - Must be deterministic: true/false answer
  - Should be efficient: faster than linear search
  - Example: check if N items fit in capacity C in O(n) greedy packing

- **Combining Multiple Feasibility Checks**
  - Sometimes need to check multiple conditions
  - Example: chess knight tours - check if path exists
  - Combine with BFS/DFS feasibility

- **Floating-Point Binary Search**
  - For real-valued targets (e.g., find sqrt)
  - Use epsilon for convergence
  - Fixed iterations typically needed (20-30 iterations for 10^-6 precision)

- **Practical Considerations**
  - Precision requirements
  - Integer vs floating-point arithmetic
  - Avoid integer overflow (use mid = low + (high - low) / 2)

**Key Insights:**
- Binary search pattern extends to optimization problems
- Key is framing problem as finding threshold on monotone condition
- Feasibility check is heart of algorithm

**Deliverables:**
- [ ] Implement capacity planning using binary search
- [ ] Implement aggressive cows problem
- [ ] Solve food delivery optimization
- [ ] Implement floating-point binary search for sqrt
- [ ] Design feasibility check for custom problem

---

## 📌 WEEK 5: HASH, MONOTONIC STACK, INTERVALS, PARTITION, KADANE, FAST/SLOW

### Weekly Goal
Master high-frequency patterns that cover large fraction of interview problems. Each pattern solves family of problems with similar structure.

### Weekly Topics
- Hash map / hash set patterns for complement and frequency counting
- Monotonic stack for next/previous element and range problems
- Interval merging and scheduling patterns
- Partition and cyclic sort for in-place rearrangement
- Kadane's algorithm for maximum subarray and variants
- Fast/slow pointers for cycle detection and list manipulation

---

### 📅 DAY 1: HASH MAP / HASH SET PATTERNS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Two-Sum & Complement Patterns**
  - Problem: find two numbers summing to target
  - Naive: O(n²) nested loops
  - Hash approach: for each num, check if (target - num) in set
    - Build hash set on first pass
    - Check on second pass
  - Time: O(n), Space: O(n)

- **Variations of Two-Sum**
  - Two-Sum II (sorted array): use two pointers instead
  - 3Sum: sort, fix one element, use two-sum for rest
  - K-Sum: recursive reduction to 2-sum

- **Frequency Counting Patterns**
  - Anagrams: two strings are anagrams iff same character frequencies
    - Map each string to sorted characters or frequency tuple
    - Check equality
  - Top K Frequent Elements:
    - Count frequencies
    - Use heap/bucket sort to find top K
  - Majority Element:
    - Count frequencies
    - Return element with count > n/2

- **Valid Anagram**
  - Build frequency map for both strings
  - Compare maps (or sort and compare)
  - Time: O(n), Space: O(alphabet size)

- **Anagrams in List**
  - Group anagrams together
  - Normalize each string (e.g., sort characters)
  - Use map: normalized → list of original strings
  - Time: O(n × k log k) where n = words, k = word length

- **Membership & Deduplication**
  - Remove duplicates: convert to set, size gives count
  - Contains duplicate check: iterate and add to set
  - Unique elements in array: return set size

- **Intersection of Multiple Sets**
  - Two arrays: iterate one, check if element in other's set
  - Multiple arrays: intersect iteratively

- **Word Ladder Pattern**
  - Find neighbors by changing one character
  - Use set to track visited words
  - BFS to find shortest path

**Key Insights:**
- Hash maps enable O(1) lookups for membership
- Frequency counting solves many interview problems
- Normalization (sorting, etc.) enables grouping

**Deliverables:**
- [ ] Implement two-sum with hash map
- [ ] Implement 3-sum using two-sum recursion
- [ ] Group anagrams together
- [ ] Find top K frequent elements using bucket sort
- [ ] Find intersection of two sorted arrays

---

### 📅 DAY 2: MONOTONIC STACK

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Monotonic Stack Concept**
  - Stack maintaining elements in increasing or decreasing order
  - When adding new element: pop larger/smaller elements (depending on type)
  - Top of stack always closest qualifying element
  - Enables O(n) processing where naive would be O(n²)

- **Next Greater Element**
  - Problem: for each element, find next element larger than it
  - Naive: O(n²) nested loops
  - Monotonic decreasing stack:
    - Iterate right to left
    - For each element: pop smaller elements from stack (found next greater for them)
    - Top of stack is next greater for current
  - Time: O(n) each element added/removed once

- **Next Smaller Element**
  - Similar to next greater but with increasing stack
  - Iterate and maintain increasing order

- **Previous Greater / Previous Smaller**
  - Iterate left to right
  - Stack maintains elements processed so far
  - Top of stack is previous qualifier

- **Stock Span Problem**
  - For each day, how many consecutive previous days had price ≤ current?
  - Monotonic decreasing stack approach:
    - Store (price, span) pairs
    - For new day: accumulate spans of elements we pop
  - Time: O(n)

- **Largest Rectangle in Histogram**
  - Given heights, find largest rectangle
  - Key insight: for each bar, find left and right boundaries where bar is tallest
  - Monotonic stack:
    - Maintain indices in increasing order of heights
    - For each bar: pop taller bars (calculate rectangles)
    - Time: O(n)

- **Trapping Rain Water**
  - Water trapped = min(max_left, max_right) - current_height
  - For each position: need max to left and right
  - Monotonic stack approach (or precompute max left/right in O(n))

- **Remove K Digits to Get Smallest Number**
  - Greedy with monotonic stack:
    - Maintain increasing stack
    - For each digit: pop if can remove and current smaller
    - Limit removals to K
  - Time: O(n)

- **Implementation Details**
  - Stack stores indices, not values (enables left/right boundary calculations)
  - Careful about off-by-one errors
  - Handle remaining elements after main loop

**Key Insights:**
- Monotonic stack powerful for range queries
- Next/previous element problems natural fit
- Stack ensures each element processed once

**Deliverables:**
- [ ] Implement next greater element
- [ ] Solve stock span problem
- [ ] Implement largest rectangle in histogram
- [ ] Solve trapping rain water with monotonic stack
- [ ] Remove K digits to get smallest number

---

### 📅 DAY 3: MERGE OPERATIONS & INTERVAL PATTERNS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Merging Sorted Arrays**
  - Two sorted arrays: merge in O(m+n)
  - Two pointers at start of each array
  - Compare and take smaller, advance pointer
  - Combine into new array or merge in-place

- **Merge Sorted Lists**
  - Linked lists: similar approach
  - Create new list with pointers to both lists
  - Traverse and link nodes in order

- **Merging K Sorted Lists**
  - Naive: pairwise merging O(n² k) time
  - Heap approach: maintain min-heap of heads
    - Extract min, add to result
    - Add next element from same list to heap
    - Time: O(nk log k) where n = total elements
  - Divide & conquer: merge K lists recursively

- **Merge Intervals**
  - Given overlapping intervals, merge overlaps
  - Sort by start time: O(n log n)
  - Iterate and merge if overlaps: O(n)
  - Overlap condition: new_start ≤ current_end

- **Insert Interval**
  - Add new interval to list of non-overlapping intervals
  - Collect non-overlapping intervals before new
  - Find overlapping intervals and merge with new
  - Collect non-overlapping intervals after merged
  - Time: O(n)

- **Interval Scheduling Variation**
  - Different from greedy activity selection
  - Here we merge instead of selecting
  - Useful for calendar, room scheduling

- **Meeting Rooms Problems**
  - Problem 1: can one person attend all meetings?
    - Check if meetings overlap
  - Problem 2: minimum rooms needed?
    - Sweep line algorithm: sort start/end times
    - Track concurrent meetings at each time
    - Max concurrent = min rooms

- **Real-World Applications**
  - Calendar consolidation
  - Conference room scheduling
  - Resource allocation
  - Network time slot management

**Key Insights:**
- Sorting by start time is key to interval problems
- Merging logic similar across variations
- Sweep line elegant for concurrent range problems

**Deliverables:**
- [ ] Implement merge two sorted arrays
- [ ] Implement merge K sorted lists with heap
- [ ] Merge overlapping intervals
- [ ] Insert new interval into list
- [ ] Find minimum meeting rooms needed

---

### 📅 DAY 4 (PART A): PARTITION & CYCLIC SORT

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Dutch National Flag Problem**
  - Partition array into three regions: 0s, 1s, 2s
  - In-place partitioning in O(n)
  - Three pointers: left (0s boundary), mid (current), right (2s boundary)
  - Invariant: [0, left) = 0s, [left, mid) = 1s, [mid, right] = unknown, (right, n) = 2s

- **Partitioning Around Pivot**
  - Quicksort partition: elements < pivot on left, ≥ pivot on right
  - Two-pointer approach: converge from ends
  - Randomized pivot prevents worst-case

- **Move Zeroes to End**
  - In-place moving zeros to end
  - Slow pointer: next position for non-zero
  - Fast pointer: scan for non-zeros
  - Swap when found
  - Time: O(n), Space: O(1)

- **Cyclic Sort Pattern**
  - Used for arrays containing 1..n or 0..n-1
  - If element i belongs at position i:
    - Swap to correct position
    - Repeat until element at position i is correct
  - Time: O(n), Space: O(1)

- **Finding Missing Number Using Cyclic Sort**
  - Array [0, n] with one missing
  - Cyclic sort: place each number at index equal to its value
  - Then scan: first index where element ≠ index is the missing number
  - Time: O(n), Space: O(1)

- **Finding Duplicate in Array [1..n]**
  - One number repeated, find it
  - Cyclic sort would detect: when trying to place num, position already has num
  - Return when duplicate detected
  - Time: O(n), Space: O(1)

- **Finding All Duplicates**
  - Similar approach: mark positions as visited using signs
  - Or use cyclic sort and collect duplicates

- **First Missing Positive**
  - Array with positive and negative numbers
  - Cyclic sort on positive numbers in [1, n] range
  - Scan for first missing

**Key Insights:**
- Cyclic sort elegant for permutation-based problems
- In-place rearrangement saves space
- Invariants critical for correctness

**Deliverables:**
- [ ] Implement Dutch National Flag partitioning
- [ ] Implement move zeroes to end
- [ ] Implement cyclic sort
- [ ] Find missing number using cyclic sort
- [ ] Find all duplicates in array

---

### 📅 DAY 4 (PART B): KADANE'S ALGORITHM

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Maximum Subarray Problem**
  - Find contiguous subarray with largest sum
  - Naive: O(n³) all subarrays
  - Prefix sum: O(n²) with optimization
  - Kadane's: O(n)

- **Kadane's Algorithm Insight**
  - At each position: decide continue current subarray or start new
  - max_ending_here = max(arr[i], max_ending_here + arr[i])
  - max_global = max(max_global, max_ending_here)
  - Time: O(n), Space: O(1)

- **Example Walkthrough**
  - Array: [-2, 1, -3, 4, -1, 2, 1, -5, 4]
  - Trace through: see how subarrays evolve
  - Result: [4, -1, 2, 1] = sum 6

- **Maximum Product Subarray**
  - Similar to max sum but with multiplication
  - Complication: negative × negative = positive
  - Track both max and min ending at each position
  - max_here = max(arr[i], arr[i] × max_prev, arr[i] × min_prev)
  - min_here = min(arr[i], arr[i] × max_prev, arr[i] × min_prev)

- **Circular Array Variant**
  - Array is circular: last element connects to first
  - Two cases:
    - Normal: max subarray doesn't wrap
    - Wrapping: max subarray wraps around
  - Wrap case: total_sum - min_subarray
  - Return max of both cases

- **Maximum Subarray with K Constraint**
  - Subarray of exactly size K: O(n) with sliding window
  - Subarray of at most K: DP or greedy approaches

- **Applications**
  - Stock trading: buy/sell on one transaction
  - Energy consumption: find peak consumption period
  - Revenue analysis: find highest revenue period

- **DP Formulation**
  - dp[i] = max sum of subarray ending at i
  - dp[i] = max(arr[i], dp[i-1] + arr[i])
  - Answer = max(dp[i]) for all i

- **Handling Edge Cases**
  - All negative numbers: max is least negative
  - Empty array: define behavior (empty sum = 0 or undefined)
  - Single element: return that element

**Key Insights:**
- Kadane's algorithm elegant DP solution
- Greedy choice: continue or restart subarray
- Track both maximum and minimum for product variant

**Deliverables:**
- [ ] Implement Kadane's algorithm for max sum subarray
- [ ] Implement for maximum product subarray
- [ ] Solve circular subarray variant
- [ ] Find indices of max subarray (not just sum)
- [ ] Extend to at most K distinct elements

---

### 📅 DAY 5: FAST/SLOW POINTERS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Floyd's Cycle Detection Algorithm**
  - Problem: detect if linked list has cycle
  - Fast pointer (tortoise and hare): moves 2 steps
  - Slow pointer: moves 1 step
  - If cycle exists, they meet eventually
  - If no cycle, fast reaches end
  - Time: O(n), Space: O(1)

- **Finding Cycle Start**
  - After detecting cycle, reset one pointer to head
  - Move both one step at a time
  - They meet at cycle start
  - Mathematical proof: distances work out to meet at start

- **Cycle Length Calculation**
  - When pointers meet in cycle, count steps
  - Or: distance from head to cycle start + cycle length

- **Finding Middle of Linked List**
  - Slow pointer moves 1 step, fast moves 2 steps
  - When fast reaches end, slow is at middle
  - Handle even/odd length lists
  - Time: O(n), Space: O(1)

- **Splitting Linked List at Middle**
  - Find middle using fast/slow
  - Split into two lists at middle
  - Useful for merge sort on linked lists

- **Happy Number Problem**
  - Number is happy if repeatedly summing digit squares eventually reaches 1
  - Unhappy if cycles infinitely
  - Use fast/slow to detect cycle
  - If cycle is [1], it's happy; otherwise unhappy

- **Palindrome Linked List**
  - Find middle using fast/slow
  - Reverse second half
  - Compare first and second halves
  - Time: O(n), Space: O(1)

- **Remove Nth Node from End**
  - Use two pointers with gap
  - Lead pointer moves N steps ahead
  - Then both move until lead reaches end
  - Trailer is now before node to remove
  - Handle edge cases: removing head, single node

- **Reorder Linked List (L1→Ln→L2→Ln-1→...)**
  - Find middle
  - Reverse second half
  - Merge two halves interleaving
  - Time: O(n), Space: O(1)

- **Detecting Repeated Sequence**
  - Use fast/slow to detect any cyclic pattern
  - Apply to number sequences, strings

**Key Insights:**
- Fast/slow pointers enable cycle detection with O(1) space
- Works for any sequence, not just linked lists
- Middle finding useful preprocessing for many problems

**Deliverables:**
- [ ] Implement cycle detection in linked list
- [ ] Find cycle start position
- [ ] Find middle of linked list
- [ ] Detect happy number using fast/slow
- [ ] Solve palindrome linked list problem
- [ ] Solve reorder linked list problem

---

## 📌 WEEK 6: STRING PATTERNS

### Weekly Goal
Apply pattern techniques to string problems. Strings are arrays of characters; adapt earlier patterns and learn string-specific techniques like palindrome detection and bracket matching.

### Weekly Topics
- Palindrome patterns and detection
- Substring problems using sliding window
- Parentheses and bracket matching
- String transformations and building
- String matching and rolling hash applications

---

### 📅 DAY 1: PALINDROME PATTERNS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Simple Palindrome Check**
  - Two pointers from ends converging
  - Compare characters
  - Time: O(n), Space: O(1)

- **Longest Palindromic Substring**
  - Naive: O(n³) check all substrings
  - Expand around center: O(n²)
    - For each center (odd and even length)
    - Expand while characters match
    - Track longest found
  - Manacher's algorithm: O(n) (advanced)

- **Valid Palindrome (with Non-Alphanumeric)**
  - Ignore non-alphanumeric characters
  - Two pointers skip non-letters/digits
  - Compare letters (case-insensitive)
  - Time: O(n)

- **Valid Palindrome II (Remove at Most One)**
  - Two pointers from ends
  - When mismatch: try removing left or right
  - Check if either results in palindrome
  - Recursive or iterative

- **Palindrome Partitioning**
  - Partition string into palindromic substrings
  - Backtracking: at each position, try all palindromic prefixes
  - Memoization: cache (start, end) → is_palindrome
  - Time: O(n × 2^n) in worst case

- **Shortest Palindrome by Adding Characters**
  - Find longest palindrome from start
  - Add reverse of suffix to start
  - Example: "abcd" → find "a" is palindrome from start
    - Add "dcb" to start: "dcbabcd"
  - Use KMP for efficiency

- **Count Palindromic Substrings**
  - Expand around center for each center
  - Count palindromes found at each center
  - Time: O(n²)

- **Memoization for Palindrome Checks**
  - Cache (i, j) → is_substring_palindrome
  - Fill table diagonally
  - Use in palindrome partitioning for efficiency

**Key Insights:**
- Palindrome detection symmetric property
- Center expansion elegant for O(n²)
- Backtracking powerful for partitioning

**Deliverables:**
- [ ] Implement longest palindromic substring with center expansion
- [ ] Implement valid palindrome II (remove at most one)
- [ ] Solve palindrome partitioning with backtracking
- [ ] Count all palindromic substrings
- [ ] Find shortest palindrome by adding characters

---

### 📅 DAY 2: SUBSTRING & SLIDING WINDOW ON STRINGS

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Longest Substring Without Repeating Characters**
  - Variable sliding window with character map
  - Expand right: add character
  - If character repeats: shrink left until no repeat
  - Time: O(n), Space: O(alphabet size)

- **Longest Substring with At Most K Distinct Characters**
  - Similar approach: maintain at most K distinct
  - When K+1 distinct: shrink until K again
  - Variation: exactly K (use at-most-k minus at-most-(k-1) trick)

- **Permutation in String (Pattern Matching)**
  - Fixed window size = pattern length
  - Check if frequency map matches pattern frequency
  - Slide window, update frequency
  - Time: O(n)

- **Find All Anagrams of Substring**
  - Similar: fixed window size
  - For each window: check if anagram of pattern
  - Collect starting indices
  - Time: O(n)

- **Minimum Window Substring**
  - Find smallest window containing all pattern characters
  - Variable window: expand right until complete
  - Shrink left while complete
  - Track best window found
  - Time: O(n)

- **Substring with Concatenation of All Words**
  - Fixed window size = sum of word lengths
  - Check if window contains exactly all words
  - Use frequency map for words
  - Time: O(n × m²) where m = number of words

- **Repeated DNA Sequences (Rolling Hash)**
  - Find 10-character sequences appearing more than once
  - Rolling hash for efficiency
  - Hash to set, check for duplicates
  - Time: O(n)

- **Substring Problems Pattern**
  - Clear window condition (valid vs invalid)
  - Expand right to grow window
  - Shrink left to maintain validity
  - Process best candidate at each step

**Key Insights:**
- Sliding window transforms O(n²) to O(n)
- Frequency maps enable constraint checking
- Rolling hash for pattern detection

**Deliverables:**
- [ ] Implement longest substring without repeating characters
- [ ] Solve minimum window substring problem
- [ ] Find all anagrams of pattern in string
- [ ] Solve permutation in string problem
- [ ] Find repeated DNA sequences using rolling hash

---

### 📅 DAY 3: PARENTHESES & BRACKET MATCHING

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Valid Parentheses via Stack**
  - Push opening brackets: (, [, {
  - On closing bracket: pop and check if matching
  - If mismatch: invalid
  - If stack not empty at end: invalid
  - Time: O(n), Space: O(n)

- **Generate All Valid Parentheses**
  - N pairs of parentheses: generate all valid combinations
  - Backtracking: track open count and close count
  - Only add:
    - Opening: if less than n used
    - Closing: if less than opening used
  - Time: O(Catalan number) = O(4^n / sqrt(n))

- **Longest Valid Parentheses**
  - Find longest valid substring
  - Stack approach: store indices
    - Push -1 initially
    - On '(': push index
    - On ')': pop index
      - If empty: push current index
      - Else: length = current - top of stack
  - Time: O(n)

- **Minimum Remove to Make Valid Parentheses**
  - Remove minimum characters to make valid
  - Two-pass:
    - First pass: mark invalid ones
    - Second pass: build result without marked
  - Or use stack to track invalid indices

- **Balanced Parentheses with Limits**
  - Maximum depth K
  - Track current depth
  - Reject if exceed K
  - Validate as building

- **Score of Parentheses**
  - Each pair contributes: inner 2×score
  - Nested pairs multiply contributions
  - Use stack to track scores
  - Time: O(n)

- **Check if String is K-Palindrome**
  - Remove K character-pairs (any chars)
  - Check if remaining is palindrome
  - Backtracking or DP

**Key Insights:**
- Stack perfect for matching problems
- Push/pop mechanics handle nesting
- Backtracking generates all valid combinations

**Deliverables:**
- [ ] Implement valid parentheses check
- [ ] Generate all valid parentheses combinations for n pairs
- [ ] Solve longest valid parentheses problem
- [ ] Solve minimum remove to make valid
- [ ] Calculate score of parentheses string

---

### 📅 DAY 4: STRING TRANSFORMATIONS & BUILDING

**Duration:** 90 minutes

**Topics & Subtopics:**

- **String to Integer (atoi)**
  - Parse string to integer
  - Handle whitespace: trim leading
  - Handle sign: +/-
  - Handle non-digit characters: stop
  - Handle overflow: check bounds
  - Time: O(n)

- **Integer to String**
  - Reverse digits and build
  - Handle negative numbers
  - Handle zero
  - Time: O(log n) for n bits

- **Integer ↔ Roman Numerals**
  - Mapping: {1000: M, 900: CM, 500: D, ...}
  - Integer to Roman: greedily use largest symbols
  - Roman to Integer: sum values (special cases for subtraction)
  - Time: O(1) for fixed range

- **Zigzag Conversion**
  - Read string in zigzag pattern, output row by row
  - Either simulate zigzag or use index formula
  - Formula: character at row depends on k (number of rows) and position

- **Reverse String / Reverse Words**
  - Reverse entire string: O(1) space using two pointers
  - Reverse words: reverse entire, then reverse each word

- **String Compression (RLE)**
  - Replace runs with character + count
  - Example: "aabbbccc" → "a2b3c3"
  - Track when character changes
  - Time: O(n)

- **Decode String**
  - Pattern: "3[a2[c]]" → "accaccacc"
  - Use stack to handle nesting
  - Push: characters and counts
  - On ']': pop and decode
  - Time: O(output length)

- **String Builder / Performance**
  - String immutability in many languages
  - Use StringBuilder instead of concatenation
  - Concatenation in loop: O(n²) → use builder: O(n)

- **Word Ladder (BFS with String)**
  - Find transformation sequence using string adjacency
  - Adjacency: differ by exactly one character
  - BFS to find shortest path
  - Time: O(n × w²) where w = word length

**Key Insights:**
- Character-by-character processing for transformations
- Stack useful for nested structures
- StringBuilder avoids quadratic concatenation

**Deliverables:**
- [ ] Implement string to integer with overflow handling
- [ ] Convert integer to/from Roman numerals
- [ ] Implement zigzag conversion both directions
- [ ] String compression with RLE
- [ ] Decode string with nested brackets

---

### 📅 DAY 5 (OPTIONAL): STRING MATCHING & ROLLING HASH IN PRACTICE

**Duration:** 90 minutes

**Topics & Subtopics:**

- **Revisiting Karp-Rabin Algorithm**
  - Rolling hash for pattern matching
  - Compute hash of pattern and first window
  - Slide window, update hash in O(1)
  - On match: verify full string (avoid false positives)
  - Time: O(n + m) average, O((n-m)×m) worst case

- **Rolling Hash Implementation**
  - Hash = (s[0] × b^(m-1) + s[1] × b^(m-2) + ... + s[m-1]) mod p
  - Update: (hash - s[i]×b^(m-1)) × b + s[i+m]) mod p
  - Choose: b = 256 (alphabet size), p = large prime

- **Collision Handling**
  - On hash match: verify actual string (Karp-Rabin feature)
  - Reduces false positives
  - Handles hash collisions transparently

- **Repeated Substring Pattern**
  - Find if string is repetition of smaller string
  - Rolling hash: efficient checking
  - Or use string matching: pattern appears in (s+s) without boundaries

- **Find Repeated DNA Sequences**
  - 10-character sequences in DNA string
  - Rolling hash to find all 10-mers
  - Store in set, find duplicates
  - Alphabet = 4 (A, C, G, T)

- **Comparing KMP vs Boyer-Moore vs Karp-Rabin**
  - KMP: O(n+m), no extra space
  - Boyer-Moore: O(n/m) average, slow on short patterns
  - Karp-Rabin: O(n+m) average, simple hash computation
  - Choice depends on application and alphabet size

- **Applications in Systems**
  - Log searching: find pattern in logs
  - Plagiarism detection: compare document snippets
  - DNA sequence searching
  - Network traffic inspection

- **Performance Considerations**
  - Hash computation overhead for short patterns
  - Probability of collisions affects verification cost
  - Real systems use cryptographic hashes for security

**Key Insights:**
- Rolling hash enables O(1) window updates
- Verification step handles hash collisions
- Practical choice among matching algorithms

**Deliverables:**
- [ ] Implement Karp-Rabin algorithm
- [ ] Find all 10-mers in DNA sequence
- [ ] Detect repeated substring pattern
- [ ] Compare performance vs KMP on various inputs
- [ ] Implement with multiple patterns (Aho-Corasick preview)

---

# END OF PHASE B

**Total Content:**
- 3 weeks (Weeks 4-6)
- 15 study days (5 days per week)
- 60+ detailed topics and subtopics
- 150+ problem patterns covered
- 90-120 minutes per day
- 45-60 hours of material

**Phase B Mastery Outcomes:**
- ✅ Pattern recognition across array/string problems
- ✅ Two-pointer, sliding window, divide & conquer fluency
- ✅ Monotonic stack and hash-based solutions
- ✅ Interval merging, partitioning, Kadane's algorithm
- ✅ String manipulation and matching techniques
- ✅ Interview-ready problem-solving approach

**Next:** Phase C - Trees, Graphs & DP (Weeks 7-11)

---

**Version:** 13.0
**Status:** ✅ COMPLETE - PRODUCTION READY
**Date:** January 24, 2026
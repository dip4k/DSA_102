# 📊 WEEK 05 MASTER PRACTICE GUIDE — All Patterns

**Week:** 5 | **Tier:** Tier 1 Critical Patterns  
**Total Problems:** 48 (8 per day, difficulty progression)  
**Format:** LeetCode mapped with solutions strategy, not solutions

---

## 📋 PRACTICE PROGRESSION FRAMEWORK

**Difficulty Curve:** Easy (1-2) → Medium (3-6) → Hard (7-8) per day

**Spacing Strategy:** Complete 2-3 per day for 20 days, or 4-6 daily intensive

**Mastery Checkpoints:**
- After day 1 (8): Should solve all, no reference
- After day 2 (16): Should combine hash+stack patterns
- After day 3 (24): Should handle intervals independently
- After day 4A (32): Should apply partitioning concepts
- After day 4B (40): Should recognize DP subproblems
- After day 5 (48): Should solve 65% of all interview problems

---

## 🎯 DAY 1: HASH PATTERNS (8 Problems)

### Problem 1: Two Sum (Easy) — LeetCode 1
**Pattern:** Complement lookup | **Space:** O(n) hash | **Time:** O(n)
**Strategy:** Store complements as you scan, check map for target - value
**Follow-ups:** Sorted array (two-pointer), three-sum, k-sum, all pairs

### Problem 2: Valid Anagram (Easy) — LeetCode 242
**Pattern:** Frequency counting | **Space:** O(1) or O(k) | **Time:** O(n)
**Strategy:** Count char frequencies, compare or build canonical form
**Follow-ups:** Group anagrams, find anagrams in dict, anagram pairs

### Problem 3: Top K Frequent Elements (Medium) — LeetCode 347
**Pattern:** Frequency + bucket sort | **Space:** O(n) | **Time:** O(n)
**Strategy:** Count frequencies, bucket by count (1..n), collect k largest
**Follow-ups:** Top K with heap, top K pairs, k most frequent visitors

### Problem 4: Majority Element (Easy) — LeetCode 169
**Pattern:** Frequency threshold | **Space:** O(1) or O(n) | **Time:** O(n)
**Strategy:** Count frequencies, return element appearing > n/2 times
**Follow-ups:** Majority II (>n/3), majority III (>n/k), find all if > n/2

### Problem 5: Contains Duplicate (Easy) — LeetCode 217
**Pattern:** Membership set | **Space:** O(n) | **Time:** O(n)
**Strategy:** Track seen elements in set, return true if duplicate found
**Follow-ups:** Contains duplicate II (distance), III (value diff)

### Problem 6: Group Anagrams (Medium) — LeetCode 49
**Pattern:** Canonical grouping | **Space:** O(n) | **Time:** O(n×k log k)
**Strategy:** Canonical form as key (sorted chars), group by key
**Follow-ups:** Find all anagrams of word, group by pattern

### Problem 7: Isomorphic Strings (Easy) — LeetCode 205
**Pattern:** Two-way mapping | **Space:** O(1) alphabet | **Time:** O(n)
**Strategy:** Build char→char mapping both directions, check consistency
**Follow-ups:** Pattern matching, word pattern

### Problem 8: Intersection of Two Arrays (Easy) — LeetCode 349
**Pattern:** Membership intersection | **Space:** O(n) | **Time:** O(n+m)
**Strategy:** Store one array in set, find common elements with other
**Follow-ups:** Intersection II, LRU cache (uses hash)

---

## 📈 DAY 2: MONOTONIC STACK (8 Problems)

### Problem 1: Next Greater Element (Easy) — LeetCode 496
**Pattern:** Stack invariant | **Space:** O(n) | **Time:** O(n)
**Strategy:** Decreasing stack, pop when current > top, save answer
**Follow-ups:** Previous greater, circular array, all subarrays

### Problem 2: Daily Temperatures (Medium) — LeetCode 739
**Pattern:** Next greater distance | **Space:** O(n) | **Time:** O(n)
**Strategy:** Decreasing stack, calculate index difference
**Follow-ups:** Stock span variant, temperature increase count

### Problem 3: Stock Span (Medium) — LeetCode 901
**Pattern:** Span accumulation | **Space:** O(n) | **Time:** O(n)
**Strategy:** Stack tracks (price, span), pop smaller, span += popped span
**Follow-ups:** Span II, price oscillation patterns

### Problem 4: Trapping Rain Water (Hard) — LeetCode 42
**Pattern:** Stack area calculation | **Space:** O(n) | **Time:** O(n)
**Strategy:** Stack stores indices, trap = (min_wall - curr) × width
**Follow-ups:** 2D rain water (volume), rain water II (3D)

### Problem 5: Largest Rectangle in Histogram (Hard) — LeetCode 84
**Pattern:** Increasing stack, width | **Space:** O(n) | **Time:** O(n)
**Strategy:** Stack tracks indices, pop when height decreases, calc area
**Follow-ups:** Largest rectangle in matrix (reduce to histogram)

### Problem 6: Remove K Digits (Medium) — LeetCode 402
**Pattern:** Monotonic increasing | **Space:** O(n) | **Time:** O(n)
**Strategy:** Decreasing stack, remove k elements, return smallest
**Follow-ups:** Remove K digits II, smallest suffix

### Problem 7: Lexicographically Smallest Subsequence (Medium) — LeetCode 316
**Pattern:** Monotonic + char count | **Space:** O(n) | **Time:** O(n)
**Strategy:** Track remaining chars, pop if current < top and can repeat
**Follow-ups:** Duplicate remover, smallest rotation

### Problem 8: Online Stock Span (Medium) — LeetCode 901
**Pattern:** Streaming stack | **Space:** O(n) | **Time:** O(n)
**Strategy:** Query-based span calculation, stack of (price, span)
**Follow-ups:** Real-time stock analysis, sliding window span

---

## 🔄 DAY 3: INTERVAL PATTERNS (8 Problems)

### Problem 1: Merge Intervals (Medium) — LeetCode 56
**Pattern:** Sort + linear merge | **Space:** O(1) | **Time:** O(n log n)
**Strategy:** Sort by start, merge overlapping by tracking max end
**Follow-ups:** Insert interval, merge K sorted intervals

### Problem 2: Insert Interval (Medium) — LeetCode 57
**Pattern:** Three-phase partition | **Space:** O(1) | **Time:** O(n)
**Strategy:** Before (no overlap) → merge (overlap) → after (no overlap)
**Follow-ups:** Insert with K intervals, interval deletion

### Problem 3: Meeting Rooms (Easy) — LeetCode 252
**Pattern:** Sort + scan for gaps | **Space:** O(1) | **Time:** O(n log n)
**Strategy:** Sort by start, check no overlaps between consecutive
**Follow-ups:** Meeting rooms II (min conference rooms needed)

### Problem 4: Meeting Rooms II (Medium) — LeetCode 253
**Pattern:** Overlap counting via sort | **Space:** O(n) | **Time:** O(n log n)
**Strategy:** Start/end events, sweep, max concurrent = max rooms needed
**Follow-ups:** Meeting rooms III (with time windows), room booking

### Problem 5: Interval List Intersections (Medium) — LeetCode 986
**Pattern:** Two-pointer merge | **Space:** O(1) | **Time:** O(n+m)
**Strategy:** Two pointers, merge intersections, advance pointer with smaller end
**Follow-ups:** List unions, interval differences

### Problem 6: Non-overlapping Intervals (Medium) — LeetCode 435
**Pattern:** Greedy by end time | **Space:** O(1) | **Time:** O(n log n)
**Strategy:** Sort by end, greedily pick earliest ending, remove overlaps
**Follow-ups:** Maximum non-overlapping intervals, weighted intervals

### Problem 7: Course Schedule (Medium) — LeetCode 207
**Pattern:** Cycle in intervals | **Space:** O(n) | **Time:** O(n+e)
**Strategy:** Interval as prerequisite graph, topological sort = no cycle
**Follow-ups:** Course schedule II (order), alien dictionary

### Problem 8: The Skyline Problem (Hard) — LeetCode 218
**Pattern:** Complex interval merging | **Space:** O(n) | **Time:** O(n log n)
**Strategy:** Events (start/end) with heights, sweep line algorithm
**Follow-ups:** Skyline II (contours), building overlaps

---

## 🎨 DAY 4A: PARTITION & CYCLIC SORT (8 Problems)

### Problem 1: Sort Colors (Medium) — LeetCode 75
**Pattern:** Dutch National Flag | **Space:** O(1) | **Time:** O(n)
**Strategy:** 3-pointer (left, mid, right), segregate 0/1/2 in-place
**Follow-ups:** Sort 4 colors, sort objects by color+size

### Problem 2: Move Zeroes (Easy) — LeetCode 283
**Pattern:** Two-pointer partition | **Space:** O(1) | **Time:** O(n)
**Strategy:** Track lastNonZero, shift non-zeros, fill zeros at end
**Follow-ups:** Move element X to end, move k-smallest to front

### Problem 3: First Missing Positive (Hard) — LeetCode 41
**Pattern:** Cyclic sort 1..n | **Space:** O(1) | **Time:** O(n)
**Strategy:** Cyclic sort, find first position without correct value
**Follow-ups:** Kth missing, all missing in 1..n range

### Problem 4: Find the Duplicate (Medium) — LeetCode 287
**Pattern:** Cyclic + Floyd's | **Space:** O(1) | **Time:** O(n)
**Strategy:** Treat array as linked list, Floyd's cycle detection
**Follow-ups:** Find all duplicates, duplicate in K range

### Problem 5: Missing Number (Easy) — LeetCode 268
**Pattern:** Cyclic sort 0..n | **Space:** O(1) | **Time:** O(n)
**Strategy:** Position-based: value i should be at index i
**Follow-ups:** Missing numbers (multiple), find K missing

### Problem 6: Array Partition I (Easy) — LeetCode 561
**Pattern:** Partition + pairing | **Space:** O(1) | **Time:** O(n log n)
**Strategy:** Sort, pair consecutive elements, sum minimums
**Follow-ups:** Optimal partition, weighted partition

### Problem 7: Wiggle Sort (Medium) — LeetCode 280
**Pattern:** Partition + zigzag | **Space:** O(1) | **Time:** O(n)
**Strategy:** Rearrange so nums[0] < nums[1] > nums[2] < ...
**Follow-ups:** Wiggle sort II (with constraints), zigzag sequence

### Problem 8: Largest Number (Medium) — LeetCode 179
**Pattern:** Custom sort partition | **Space:** O(n) | **Time:** O(n log n)
**Strategy:** Sort by concatenation (ab vs ba), form largest number
**Follow-ups:** Smallest number, number arranging with constraints

---

## 📈 DAY 4B: KADANE'S ALGORITHM (8 Problems)

### Problem 1: Maximum Subarray (Medium) — LeetCode 53
**Pattern:** Core Kadane's | **Space:** O(1) | **Time:** O(n)
**Strategy:** Track local + global max, decide extend or restart
**Follow-ups:** Maximum subarray with constraint, index reconstruction

### Problem 2: Maximum Product Subarray (Medium) — LeetCode 152
**Pattern:** Min/max tracking | **Space:** O(1) | **Time:** O(n)
**Strategy:** Track both min/max (negative flips signs), decide transitions
**Follow-ups:** Maximum product with k operations, circular products

### Problem 3: Maximum Subarray Sum with One Deletion (Medium) — LeetCode 1186
**Pattern:** Kadane with skip | **Space:** O(1) | **Time:** O(n)
**Strategy:** DP: track max ending here with/without deletion
**Follow-ups:** Up to k deletions, deletions anywhere

### Problem 4: Best Time to Buy and Sell Stock (Easy) — LeetCode 121
**Pattern:** Kadane variant (min tracking) | **Space:** O(1) | **Time:** O(n)
**Strategy:** Track min price, max profit = current - min
**Follow-ups:** II (multiple), III (at most 2), IV (k transactions)

### Problem 5: Circular Array Loop (Medium) — LeetCode 457
**Pattern:** Cycle in sequence | **Space:** O(1) | **Time:** O(n²) worst
**Strategy:** Follow indices, detect cycles, check all positive/negative
**Follow-ups:** Cyclic array maximum, rotation finding

### Problem 6: Maximum Subarray Sum with Constraints (Hard) — LeetCode 1687
**Pattern:** DP with window | **Space:** O(n) | **Time:** O(n)
**Strategy:** DP state: min gap between elements, maximize sum
**Follow-ups:** Subarray with bounded length, element constraints

### Problem 7: Jump Game (Medium) — LeetCode 55
**Pattern:** Reach tracking (Kadane-like) | **Space:** O(1) | **Time:** O(n)
**Strategy:** Track max reachable index, check if can reach end
**Follow-ups:** Jump game II (min jumps), jump game III (with obstacles)

### Problem 8: Best Time to Buy and Sell with Cooldown (Medium) — LeetCode 309
**Pattern:** DP state machine | **Space:** O(1) | **Time:** O(n)
**Strategy:** States: hold, sell, cooldown; transitions between states
**Follow-ups:** Transaction fee, at most k transactions

---

## 🔁 DAY 5: FAST/SLOW POINTERS (8 Problems)

### Problem 1: Linked List Cycle (Easy) — LeetCode 141
**Pattern:** Floyd's detection | **Space:** O(1) | **Time:** O(n)
**Strategy:** Slow/fast pointers at 1/2 speed, meet = cycle
**Follow-ups:** Cycle II (find start), cycle length

### Problem 2: Linked List Cycle II (Medium) — LeetCode 142
**Pattern:** Cycle start finding | **Space:** O(1) | **Time:** O(n)
**Strategy:** Floyd's then pointer reset, meet at cycle start
**Follow-ups:** Cycle with properties, multi-cycle graph

### Problem 3: Happy Number (Easy) — LeetCode 202
**Pattern:** Cycle in transformation | **Space:** O(1) | **Time:** O(log n)
**Strategy:** Digit sum transformation, Floyd's on sequence
**Follow-ups:** Sad numbers, number path analysis

### Problem 4: Palindrome Linked List (Medium) — LeetCode 234
**Pattern:** Middle finding + reverse | **Space:** O(1) | **Time:** O(n)
**Strategy:** Find middle (fast/slow), reverse second half, compare
**Follow-ups:** Verify palindrome, construct palindrome

### Problem 5: Middle of Linked List (Easy) — LeetCode 876
**Pattern:** Midpoint via fast/slow | **Space:** O(1) | **Time:** O(n)
**Strategy:** Fast moves 2, slow moves 1, slow at middle when fast ends
**Follow-ups:** Split into k parts, middle element in array

### Problem 6: Remove Nth Node (Medium) — LeetCode 19
**Pattern:** Two-pointer spacing | **Space:** O(1) | **Time:** O(n)
**Strategy:** Gap of n between pointers, advance to find removal point
**Follow-ups:** Remove k from end, remove duplicates

### Problem 7: Intersection of Two Linked Lists (Easy) — LeetCode 160
**Pattern:** Sync two pointers | **Space:** O(1) | **Time:** O(n+m)
**Strategy:** Two pointers traverse both, swap at end, meet at intersection
**Follow-ups:** Intersection count, multiple intersections

### Problem 8: Reorder List (Medium) — LeetCode 143
**Pattern:** Middle + reverse + merge | **Space:** O(1) | **Time:** O(n)
**Strategy:** Find middle, reverse second half, merge/reorder
**Follow-ups:** Reorder to pattern, list rearrangement

---

## 🎯 MIXED PATTERN PROBLEMS (Optional Bonus)

### Problem 1: Repeated DNA Sequence (Medium) — LeetCode 187
**Patterns:** Hash (window) + string | **Time:** O(n)

### Problem 2: Sort Array by Parity (Easy) — LeetCode 905
**Patterns:** Partition + two-pointer | **Time:** O(n)

### Problem 3: Time-Based Key-Value Store (Medium) — LeetCode 981
**Patterns:** Hash + binary search | **Time:** O(1) store, O(log n) get

---

## 📈 PRACTICE STRATEGY

**Week 1 (Days 1-5):** One pattern per day, 8 problems each (40 total)
**Week 2 (Mixing):** Mixed patterns, 2-3 per session
**Week 3 (Mastery):** Hard problems, variants, follow-ups

**Success Criteria:**
- ✅ Solve all 8 per day without referencing solution
- ✅ Identify pattern within 2 minutes of reading
- ✅ Code from scratch in < 15 minutes
- ✅ Explain trade-offs and variants clearly

---

**Status:** Production-Ready for Practice | **Time:** 15-20 hours total  
**Generated:** January 08, 2026 | **System:** v12


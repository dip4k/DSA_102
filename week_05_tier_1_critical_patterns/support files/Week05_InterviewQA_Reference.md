# 🎙️ Week 05 Interview Q&A Reference: 40+ Questions by Pattern

**Status:** Interview Coaching Resource  
**Audience:** Students preparing for technical interviews  
**Format:** Questions only (no answers) + follow-ups to force deeper thinking  
**How to Use:** Pick 3-5 questions daily, attempt before looking up answers, discuss with partner

---

## 🎯 DAY 1: HASH MAP & HASH SET — 8 Questions

**Q1 (🟢 Easy):** What is a hash collision? Give an example of two keys that might hash to the same index.
- **Follow-up:** How do hash tables handle collisions (separate chaining vs open addressing)?
- **Follow-up:** What happens to collision rate as load factor increases?

**Q2 (🟢 Easy):** Implement two-sum: given array and target, find two numbers that sum to target.
- **Follow-up:** What's the time complexity with hash vs without?
- **Follow-up:** What if we need all unique pairs?

**Q3 (🟡 Medium):** Check if two strings are anagrams using frequency counting.
- **Follow-up:** Why is hash better than sorting for this?
- **Follow-up:** What about follow-up problems like grouping anagrams?

**Q4 (🟡 Medium):** Find top K frequent elements. Why is heap faster than sorting?
- **Follow-up:** What if K = n (return all)?
- **Follow-up:** Can you do it without extra space?

**Q5 (🔴 Hard):** Implement LRU cache using hash + doubly-linked list. Why doubly-linked?
- **Follow-up:** What about LFU cache (least frequently used)?
- **Follow-up:** Analyze space and time for each operation.

**Q6 (🔴 Hard):** Find longest consecutive sequence in unsorted array (e.g., [100,4,200,1,3,2] → 4).
- **Follow-up:** Why sorting first is O(n log n) but hash is O(n)?
- **Follow-up:** Why do we need to skip non-start elements?

**Q7 (🟡 Medium):** Given list of strings, find strings that can be typed using only letters from another string (char mapping).
- **Follow-up:** Is this a frequency problem or membership problem?
- **Follow-up:** How do you handle repeated characters?

**Q8 (🟡 Medium):** Is this string an isomorphic mapping? ("egg" → "add", each char maps uniquely.
- **Follow-up:** Does the reverse mapping also need to be unique?
- **Follow-up:** How do you detect when mapping violates uniqueness?

---

## 🎯 DAY 2: MONOTONIC STACK — 8 Questions

**Q1 (🟢 Easy):** What does "monotonic stack" mean? Show an example of increasing vs decreasing.
- **Follow-up:** Why is it "mono"? What invariant is maintained?
- **Follow-up:** When do we pop from the stack?

**Q2 (🟡 Medium):** Implement next greater element: for each element, find next element to right that is greater.
- **Follow-up:** Show stack state evolution on [2,1,2,4,3]?
- **Follow-up:** What's the time complexity and why?

**Q3 (🟡 Medium):** Calculate stock span: consecutive days price ≤ today's price.
- **Follow-up:** Why is monotonic stack better than naive O(n²)?
- **Follow-up:** What do we store in the stack (value, index, or both)?

**Q4 (🔴 Hard):** Trapping rain water: given elevation map, how much water is trapped?
- **Follow-up:** How does monotonic decreasing stack solve this?
- **Follow-up:** What about when we encounter increasing height?

**Q5 (🟡 Medium):** Largest rectangle in histogram: find largest rectangle area.
- **Follow-up:** Why increasing monotonic stack here instead of decreasing?
- **Follow-up:** When we pop, what rectangle are we calculating?

**Q6 (🟡 Medium):** Online stock span analyzer: given prices stream, output span for each price.
- **Follow-up:** Why can't you calculate span until all future prices known?
- **Follow-up:** How does monotonic stack process incrementally?

**Q7 (🟡 Medium):** Remove k digits from number to make it smallest possible.
- **Follow-up:** Is this a monotonic stack problem? Why or why not?
- **Follow-up:** How do you handle tied digits?

**Q8 (🔴 Hard):** Remove duplicate letters while keeping relative order, each letter appears at most once.
- **Follow-up:** Why is monotonic stack efficient here?
- **Follow-up:** When is it safe to remove a character?

---

## 🎯 DAY 3: MERGE OPERATIONS & INTERVALS — 8 Questions

**Q1 (🟢 Easy):** Define interval overlap. When do intervals [a,b] and [c,d] overlap?
- **Follow-up:** Does [1,2] overlap with [2,3]? Edge cases matter!
- **Follow-up:** How would you sort intervals by start time with ties?

**Q2 (🟡 Medium):** Merge intervals: given list of intervals, merge all overlapping ones.
- **Follow-up:** Why sort by start time first?
- **Follow-up:** After sorting, why is single pass merge O(n)?

**Q3 (🟡 Medium):** Merge sorted arrays without extra space: merge array1 into array2 (with enough room).
- **Follow-up:** Why merge from end backward instead of forward?
- **Follow-up:** What's the complexity if we merged forward?

**Q4 (🟡 Medium):** Insert interval: given sorted intervals and new interval, insert and merge efficiently.
- **Follow-up:** Why don't we sort after insertion?
- **Follow-up:** How many times can intervals merge with the inserted one?

**Q5 (🔴 Hard):** Merge K sorted lists efficiently.
- **Follow-up:** Naive pairwise merging: what's complexity?
- **Follow-up:** Heap approach: which k elements do you keep in heap?

**Q6 (🟡 Medium):** Meeting rooms: given intervals, can one person attend all meetings (no overlaps)?
- **Follow-up:** Is this really an interval problem or just sorting?
- **Follow-up:** What about "min number of rooms needed"?

**Q7 (🟡 Medium):** Interval list intersections: given two lists of intervals, find all intersections.
- **Follow-up:** Sort both lists, then two-pointer merge?
- **Follow-up:** How do you define intersection area?

**Q8 (🔴 Hard):** Skyline problem: given list of buildings [left, right, height], draw skyline.
- **Follow-up:** Why is this an interval + priority queue problem?
- **Follow-up:** When does skyline height change?

---

## 🎯 DAY 4A: PARTITION & CYCLIC SORT — 8 Questions

**Q1 (🟢 Easy):** What is Dutch National Flag sorting? Sort array of 0s, 1s, 2s in one pass.
- **Follow-up:** How many pointers do we need and what do they track?
- **Follow-up:** When do we move each pointer?

**Q2 (🟡 Medium):** Implement cyclic sort for 1..n array. Each element should be at index = element-1.
- **Follow-up:** Why is this O(n) not O(n log n)?
- **Follow-up:** How does cyclic sort differ from swapping to correct position?

**Q3 (🟡 Medium):** Given array 1..n with one missing number, find it (O(n) time, O(1) space).
- **Follow-up:** Can you do this with cyclic sort? XOR trick?
- **Follow-up:** What if TWO numbers are missing?

**Q4 (🟡 Medium):** Given array 1..n with one duplicate, find it (no extra space).
- **Follow-up:** Why can't you use hash table if no extra space allowed?
- **Follow-up:** Can you use fast-slow pointers instead?

**Q5 (🟡 Medium):** Move all zeros to end while maintaining relative order of non-zeros.
- **Follow-up:** Is this partition or something else?
- **Follow-up:** How many passes do you need?

**Q6 (🔴 Hard):** Sort array containing only 0s, 1s, 2s (Dutch National Flag) in-place.
- **Follow-up:** What's the state of three pointers [low, mid, high)?
- **Follow-up:** When can we stop the loop?

**Q7 (🟡 Medium):** Find all missing numbers in 1..n array (return as list).
- **Follow-up:** If cyclic sort is O(n), is building output list also O(n)?
- **Follow-up:** Why can't you just check positions against values?

**Q8 (🔴 Hard):** Find duplicate in 1..n array (not just one, but all duplicates).
- **Follow-up:** Same O(1) space constraint?
- **Follow-up:** How does cyclic sort reveal duplicates?

---

## 🎯 DAY 4B: KADANE'S ALGORITHM — 8 Questions

**Q1 (🟢 Easy):** Maximum subarray sum. What is Kadane's algorithm in one sentence?
- **Follow-up:** Why do we track both local_max and global_max?
- **Follow-up:** What if all numbers negative?

**Q2 (🟡 Medium):** Find maximum subarray product (not sum). Why is this harder?
- **Follow-up:** Why do you need to track both min and max?
- **Follow-up:** Why can negative × negative = positive?

**Q3 (🟡 Medium):** Maximum sum of subarray with at least K elements.
- **Follow-up:** Can you modify Kadane's for this constraint?
- **Follow-up:** What about "at most K elements"?

**Q4 (🟡 Medium):** Maximum circular subarray sum. Why is circular harder?
- **Follow-up:** Formula: max(max_subarray, total - min_subarray). Why?
- **Follow-up:** What's the edge case (all negative)?

**Q5 (🔴 Hard):** Maximum sum of non-adjacent elements (can't use consecutive).
- **Follow-up:** Is this DP or Kadane variant?
- **Follow-up:** State: include or exclude current element?

**Q6 (🟡 Medium):** Maximize profit from one transaction (buy, then sell).
- **Follow-up:** Is this Kadane's or min-tracking?
- **Follow-up:** Track min price seen so far, max profit = current - min?

**Q7 (🟡 Medium):** Maximum product of subarray sum (with bonus multiplier).
- **Follow-up:** When does bonus multiplier apply?
- **Follow-up:** How many times can you use bonus?

**Q8 (🔴 Hard):** Dynamic programming: max subarray ending at position i.
- **Follow-up:** Why is this DP formulation of Kadane's?
- **Follow-up:** Recurrence: f(i) = max(arr[i], f(i-1) + arr[i])?

---

## 🎯 DAY 5: FAST-SLOW POINTERS — 8 Questions

**Q1 (🟢 Easy):** Explain Floyd's cycle detection in one sentence.
- **Follow-up:** Why does meeting guarantee cycle?
- **Follow-up:** Can they meet at different positions than actual cycle?

**Q2 (🟡 Medium):** Detect cycle in linked list. Implement and explain.
- **Follow-up:** Why O(n) time and O(1) space?
- **Follow-up:** What if list is not circular but ends at null?

**Q3 (🟡 Medium):** Find start of cycle in linked list (after detecting cycle).
- **Follow-up:** Pointer reset trick: why does it work?
- **Follow-up:** What's the math behind "cycle start = head + meeting point"?

**Q4 (🟡 Medium):** Find middle node of linked list using fast-slow pointers.
- **Follow-up:** What if list has even length? Odd?
- **Follow-up:** Why is this useful?

**Q5 (🟡 Medium):** Detect if linked list is palindrome using fast-slow.
- **Follow-up:** Find middle, reverse second half, compare?
- **Follow-up:** Restore list after checking?

**Q6 (🟡 Medium):** Happy number: repeatedly sum squares of digits, detect cycle.
- **Follow-up:** Is this a traditional cycle detection or modified?
- **Follow-up:** What's the "linked list" here (chain of transformations)?

**Q7 (🔴 Hard):** Intersection of two linked lists (different lengths, might be null).
- **Follow-up:** Is this cycle detection or pointer arithmetic?
- **Follow-up:** Why do two-pointer trick work?

**Q8 (🔴 Hard):** Remove Nth node from end of linked list (one pass).
- **Follow-up:** Why use fast-slow pointers?
- **Follow-up:** What about removing head node?

---

## 🧠 Cross-Pattern Questions: Integration

**Q1 (🟡 Medium):** Problem: "Find smallest pair with sum > K." Hash or two-pointer?
- **Follow-up:** Both work? Compare efficiency.
- **Follow-up:** What if array unsorted?

**Q2 (🟡 Medium):** "Remove k digits to make smallest number." Which patterns?
- **Follow-up:** Monotonic stack? Greedy? Both?
- **Follow-up:** Why is greedy tricky here?

**Q3 (🔴 Hard):** "Longest substring with k distinct characters." Hash or sliding window?
- **Follow-up:** Sliding window + hash?
- **Follow-up:** Is this same as Day 1 problems?

**Q4 (🔴 Hard):** Design system: "Find top K trending topics from stream." Patterns?
- **Follow-up:** Hash + heap? Monotonic?
- **Follow-up:** Add/remove topics efficiently?

---

**Status:** Week 05 Interview Q&A Reference Complete  
**Total Questions:** 40+ (8 per day × 5 + 4 cross-pattern)  
**Interview Prep Time:** 2-3 hours daily during interview week
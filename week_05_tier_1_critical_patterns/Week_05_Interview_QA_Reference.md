# 🎙 WEEK 5 INTERVIEW QUESTION BANK & QA REFERENCE

**Week 5: Consolidated Interview Questions (No Solutions Provided)**

---

## 📋 How to Use This Document

This is a **question-only** bank. No solutions provided.

**Recommended Usage:**
1. **Mock Interview Mode:** Set a timer, answer aloud without coding first.
2. **Whiteboarding Practice:** Draw on paper/board, then implement.
3. **Peer Teaching:** Explain your approach to a peer or recording.
4. **Self-Assessment:** Rate yourself on each question: 🟢 Confident | 🟡 Sketchy | 🔴 Stuck

---

## 🔴 HASH MAP & HASH SET (Day 1) — 9 Questions

### Q1: Two Sum
**Difficulty:** 🟢 Easy  
**Source:** LeetCode #1  
**Problem:** Given an array of integers and a target, return indices of the two numbers that sum to the target. Assume each input has exactly one solution.

**Follow-up 1:** What if you can't use the same element twice?  
**Follow-up 2:** What if the array is sorted?  
**Follow-up 3:** What if we need all unique pairs that sum to target?  

---

### Q2: Valid Anagram
**Difficulty:** 🟢 Easy  
**Source:** LeetCode #242  
**Problem:** Given two strings, determine if one is an anagram of the other.

**Follow-up 1:** Can you do it in O(1) space?  
**Follow-up 2:** What if the strings contain Unicode characters?  

---

### Q3: Group Anagrams
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #49  
**Problem:** Given an array of strings, group anagrams together.

**Follow-up 1:** What if you needed to return groups in lexicographic order?  
**Follow-up 2:** What if the array has millions of words?  

---

### Q4: Majority Element
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #169  
**Problem:** Find the element that appears more than `n/2` times in an array.

**Follow-up 1:** Can you do it in O(1) space? (Hint: Boyer-Moore)  
**Follow-up 2:** What if we need elements appearing more than `n/3` times?  

---

### Q5: Top K Frequent Elements
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #347  
**Problem:** Given an array and an integer k, return the k most frequent elements.

**Follow-up 1:** Can you do it in less than O(n log n) time?  
**Follow-up 2:** What if k equals the number of unique elements?  

---

### Q6: LRU Cache
**Difficulty:** 🔴 Hard  
**Source:** LeetCode #146  
**Problem:** Design and implement an LRU (Least Recently Used) Cache with `get(key)` and `put(key, value)` operations.

**Follow-up 1:** How would you extend it to an LFU Cache?  
**Follow-up 2:** What if the cache is distributed across multiple machines?  

---

### Q7: Intersection of Two Arrays II
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #350  
**Problem:** Given two arrays, return an array of their intersection. Each element in the result must appear as many times as it shows in both arrays.

**Follow-up 1:** What if the arrays are sorted?  
**Follow-up 2:** What if one array is very large and doesn't fit in memory?  

---

### Q8: Contain Duplicate II
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #219  
**Problem:** Given an array and an integer k, determine if there are duplicate elements with index difference at most k.

**Follow-up 1:** What if the constraint is: exactly k apart?  
**Follow-up 2:** What if the constraint changes dynamically?  

---

### Q9: Longest Substring Without Repeating Characters
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #3  
**Problem:** Find the length of the longest substring without repeating characters.

**Follow-up 1:** Return the substring itself, not just the length.  
**Follow-up 2:** What if the string contains Unicode characters?  

---

## 🟠 MONOTONIC STACK (Day 2) — 8 Questions

### Q10: Next Greater Element I
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #496  
**Problem:** Given an array, for each element find the next greater element to its right.

**Follow-up 1:** What if there's no next greater element?  
**Follow-up 2:** Can you do it for a circular array?  

---

### Q11: Daily Temperatures
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #739  
**Problem:** Given daily temperatures, for each day output how many days until a warmer temperature.

**Follow-up 1:** What if the temperature is in Celsius and you need to convert?  
**Follow-up 2:** What if you need the actual date, not just day difference?  

---

### Q12: Largest Rectangle in Histogram
**Difficulty:** 🔴 Hard  
**Source:** LeetCode #84  
**Problem:** Given heights of bars, find the largest rectangular area that can be formed.

**Follow-up 1:** What if you can skip bars (non-contiguous)?  
**Follow-up 2:** What's the solution for a 2D grid (maximal rectangle)?  

---

### Q13: Trapping Rain Water
**Difficulty:** 🔴 Hard  
**Source:** LeetCode #42  
**Problem:** Given elevation map, calculate how much rainwater can be trapped.

**Follow-up 1:** How would this work in 2D (a land map)?  
**Follow-up 2:** What if water can flow diagonally?  

---

### Q14: Online Stock Span
**Difficulty:** 🔴 Hard  
**Source:** LeetCode #901  
**Problem:** Given a stream of daily stock prices, return the stock span (consecutive days with price ≤ today).

**Follow-up 1:** Can you return all spans at the end instead of online?  
**Follow-up 2:** What if you need the span for min/max instead of ≤?  

---

### Q15: Remove K Digits
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #402  
**Problem:** Given a string of digits and an integer k, remove k digits to make the smallest number.

**Follow-up 1:** What if you want the largest number?  
**Follow-up 2:** What if digits are in a linked list?  

---

### Q16: Sum of Subarray Minimums
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #907  
**Problem:** For each subarray, find its minimum. Return the sum of all minimums.

**Follow-up 1:** What if we sum the maximum of each subarray?  
**Follow-up 2:** What if we sum the median of each subarray?  

---

### Q17: The Celebrity Problem
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #277  
**Problem:** In a group, a celebrity is known by everyone but knows no one. Find the celebrity.

**Follow-up 1:** What if there's no celebrity?  
**Follow-up 2:** Can you do it in O(n) time with O(1) space?  

---

## 🟡 MERGE OPERATIONS & INTERVALS (Day 3) — 8 Questions

### Q18: Merge Intervals
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #56  
**Problem:** Given intervals, merge all overlapping intervals.

**Follow-up 1:** What if intervals are 2D (rectangles)?  
**Follow-up 2:** How would you incrementally insert intervals?  

---

### Q19: Insert Interval
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #57  
**Problem:** Given non-overlapping intervals and a new interval, insert and merge as needed.

**Follow-up 1:** What if the original intervals are not sorted?  
**Follow-up 2:** What if you're inserting multiple intervals at once?  

---

### Q20: Meeting Rooms
**Difficulty:** 🟢 Easy  
**Source:** LeetCode #252  
**Problem:** Given meeting times, determine if one person can attend all meetings.

**Follow-up 1:** Return the minimum rooms needed.  
**Follow-up 2:** Return the time slots where rooms are free.  

---

### Q21: Meeting Rooms II
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #253  
**Problem:** Given meeting times, return minimum conference rooms needed.

**Follow-up 1:** Return the optimal room assignment for each meeting.  
**Follow-up 2:** What if meetings can overlap by exactly 1 minute?  

---

### Q22: Interval List Intersections
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #986  
**Problem:** Given two interval lists, return their intersection.

**Follow-up 1:** Extend to k interval lists.  
**Follow-up 2:** Return the union instead.  

---

### Q23: Non-overlapping Intervals
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #435  
**Problem:** Given intervals, return the minimum number of intervals to remove to make the rest non-overlapping.

**Follow-up 1:** Return the indices of intervals to remove.  
**Follow-up 2:** Minimize removed length instead of count.  

---

### Q24: Merge Sorted Array
**Difficulty:** 🟢 Easy  
**Source:** LeetCode #88  
**Problem:** Given two sorted arrays, merge the second into the first (in-place).

**Follow-up 1:** What if you can't modify the first array?  
**Follow-up 2:** Merge k sorted arrays.  

---

### Q25: Partition Equal Subset Sum
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #416  
**Problem:** Given an array, can it be partitioned into two subsets with equal sum?

**Follow-up 1:** Partition into k subsets with equal sum.  
**Follow-up 2:** Partition minimizing the difference.  

---

## 🟢 PARTITION & CYCLIC SORT (Day 4A) — 8 Questions

### Q26: Sort Colors
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #75  
**Problem:** Sort array of 0s, 1s, 2s in-place (one pass preferred).

**Follow-up 1:** Can you do it in one pass?  
**Follow-up 2:** Extend to k colors.  

---

### Q27: Move Zeroes
**Difficulty:** 🟢 Easy  
**Source:** LeetCode #283  
**Problem:** Move all zeros to the end while maintaining relative order of non-zero elements.

**Follow-up 1:** Move all elements equal to a value (not just 0).  
**Follow-up 2:** Minimize the number of write operations.  

---

### Q28: Missing Number
**Difficulty:** 🟢 Easy  
**Source:** LeetCode #268  
**Problem:** Given array with n numbers from range 1 to n+1, find the missing number.

**Follow-up 1:** Can you do it without extra space?  
**Follow-up 2:** Find all k missing numbers.  

---

### Q29: Find the Duplicate Number
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #287  
**Problem:** Array contains n+1 numbers from 1 to n. One number appears twice (others unique). Find it.

**Follow-up 1:** Do it in O(1) space.  
**Follow-up 2:** Find all duplicates.  

---

### Q30: First Missing Positive
**Difficulty:** 🔴 Hard  
**Source:** LeetCode #41  
**Problem:** Find the smallest missing positive integer.

**Follow-up 1:** Return the k-th smallest missing positive.  
**Follow-up 2:** Do it in O(1) space and O(n) time.  

---

### Q31: Find All Numbers Disappeared in an Array
**Difficulty:** 🟢 Easy  
**Source:** LeetCode #448  
**Problem:** Given array of n numbers from 1 to n, find all numbers that don't appear.

**Follow-up 1:** Do it in O(1) space.  
**Follow-up 2:** Return indices instead of numbers.  

---

### Q32: Rearrange Array Elements by Sign
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #2149  
**Problem:** Rearrange array so positive and negative numbers alternate.

**Follow-up 1:** Maintain relative order of positives/negatives.  
**Follow-up 2:** What if there are more positives than negatives?  

---

### Q33: Set Mismatch
**Difficulty:** 🟢 Easy  
**Source:** LeetCode #645  
**Problem:** Array should have 1 to n, but one number is missing and one appears twice. Find both.

**Follow-up 1:** Do it in O(1) space.  
**Follow-up 2:** Return them in a specific order.  

---

## 🔵 KADANE'S ALGORITHM (Day 4B) — 8 Questions

### Q34: Maximum Subarray
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #53  
**Problem:** Find the contiguous subarray with the largest sum.

**Follow-up 1:** Return the subarray itself, not just the sum.  
**Follow-up 2:** Find k non-overlapping subarrays with max sum.  

---

### Q35: Best Time to Buy and Sell Stock
**Difficulty:** 🟢 Easy  
**Source:** LeetCode #121  
**Problem:** Given prices, find max profit from one buy-sell transaction.

**Follow-up 1:** Allow multiple transactions.  
**Follow-up 2:** Allow at most k transactions.  

---

### Q36: Maximum Product Subarray
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #152  
**Problem:** Find the contiguous subarray with the largest product.

**Follow-up 1:** Find subarray with max sum of products (different constraint).  
**Follow-up 2:** Allow rearranging elements within the subarray.  

---

### Q37: Maximum Circular Subarray Sum
**Difficulty:** 🔴 Hard  
**Source:** LeetCode #918  
**Problem:** Find max sum of subarray in a circular array (wrap-around allowed).

**Follow-up 1:** Find the actual subarray, not just the sum.  
**Follow-up 2:** What if the array has negative and zero elements?  

---

### Q38: Maximum Sum of K Consecutive Elements
**Difficulty:** 🟢 Easy  
**Source:** Custom  
**Problem:** Find the maximum sum of exactly k consecutive elements.

**Follow-up 1:** Find max sum of at most k elements.  
**Follow-up 2:** Find k non-overlapping subarrays with max total.  

---

### Q39: Longest Turbulent Subarray
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #978  
**Problem:** Find longest subarray where elements alternate between increasing and decreasing.

**Follow-up 1:** Return the subarray itself.  
**Follow-up 2:** Find the longest "zigzag" pattern (more general).  

---

### Q40: Maximum Absolute Sum of Any Subarray
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #1749  
**Problem:** Find the subarray with maximum absolute sum (max(sum) or min(sum)).

**Follow-up 1:** Return both the max and min subarray sums.  
**Follow-up 2:** Find the subarray with max |sum|.  

---

### Q41: K-Concatenation Maximum Sum
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #1191  
**Problem:** Concatenate array k times. Find max subarray sum.

**Follow-up 1:** What if k is very large (10^9)?  
**Follow-up 2:** Minimize the sum instead.  

---

## 🟣 FAST & SLOW POINTERS (Day 5) — 8 Questions

### Q42: Linked List Cycle
**Difficulty:** 🟢 Easy  
**Source:** LeetCode #141  
**Problem:** Detect if a linked list has a cycle.

**Follow-up 1:** Return the cycle length.  
**Follow-up 2:** Do it without modifying the list.  

---

### Q43: Linked List Cycle II
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #142  
**Problem:** Find the start node of the cycle in a linked list.

**Follow-up 1:** Find all nodes in the cycle.  
**Follow-up 2:** Remove the cycle.  

---

### Q44: Middle of the Linked List
**Difficulty:** 🟢 Easy  
**Source:** LeetCode #876  
**Problem:** Find the middle node of a linked list.

**Follow-up 1:** Return the node before the middle for even-length lists.  
**Follow-up 2:** Do it in a single pass without storing length.  

---

### Q45: Remove Nth Node From End of List
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #19  
**Problem:** Remove the n-th node from the end of the list.

**Follow-up 1:** Remove every n-th node.  
**Follow-up 2:** Do it in a single pass.  

---

### Q46: Happy Number
**Difficulty:** 🟢 Easy  
**Source:** LeetCode #202  
**Problem:** Determine if a number is happy (repeated digit-square sums reach 1).

**Follow-up 1:** Find all happy numbers up to n.  
**Follow-up 2:** Find the cycle if the number is not happy.  

---

### Q47: Find the Duplicate Number
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #287  
**Problem:** Array of n+1 numbers from 1 to n. One number repeats. Find it in O(1) space.

**Follow-up 1:** Find all duplicates.  
**Follow-up 2:** Find the duplicate assuming multiple numbers can repeat.  

---

### Q48: Palindrome Linked List
**Difficulty:** 🟡 Medium  
**Source:** LeetCode #234  
**Problem:** Check if a linked list is a palindrome.

**Follow-up 1:** Do it in O(1) space.  
**Follow-up 2:** Return the indices of non-matching nodes.  

---

### Q49: Intersection of Two Linked Lists
**Difficulty:** 🟢 Easy  
**Source:** LeetCode #160  
**Problem:** Find the intersection node of two linked lists.

**Follow-up 1:** Do it in O(1) space.  
**Follow-up 2:** Return all intersection nodes if they form a linked structure.  

---

## 🎯 MIXED PATTERN QUESTIONS (Combining Week 5 Patterns) — 5 Questions

### Q50: Longest Substring with At Most K Distinct Characters
**Difficulty:** 🟡 Medium  
**Problem:** Find the longest substring with at most k distinct characters.
**Patterns:** Hash (frequency) + Sliding Window

---

### Q51: Smallest Range Covering Elements from K Lists
**Difficulty:** 🔴 Hard  
**Problem:** Given k sorted lists, find the smallest range containing at least one element from each.
**Patterns:** Hash + Pointers + Intervals

---

### Q52: Median of Two Sorted Arrays
**Difficulty:** 🔴 Hard  
**Problem:** Find the median of two sorted arrays.
**Patterns:** Merge (Arrays) + Binary Search

---

### Q53: Reconstruct Schedule from Course Prerequisite
**Difficulty:** 🔴 Hard  
**Problem:** Given prerequisites, reconstruct valid course schedule or detect cycle.
**Patterns:** Cycle Detection + Graph

---

### Q54: Sliding Window Maximum
**Difficulty:** 🔴 Hard  
**Problem:** Given array and window size k, find max in each sliding window.
**Patterns:** Monotonic Deque (Stack variant)

---

## 📊 Self-Assessment Guide

Rate yourself on each question:

- 🟢 **Confident:** Solved it in <15 min, explained all follow-ups
- 🟡 **Sketchy:** Solved it but struggled, need to review
- 🔴 **Stuck:** Couldn't solve or need major hints

**Target for Week 5:** 40+ 🟢, 10+ 🟡, <4 🔴

---

**End of Week 5 Interview Question Bank**  
*Use this bank for mock interviews, peer teaching, and self-assessment.*
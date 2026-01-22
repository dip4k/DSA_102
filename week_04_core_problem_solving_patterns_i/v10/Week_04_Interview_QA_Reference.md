# 📁 Week_04_Interview_QA_Reference.md

## 🎙 Week 4 – Interview Question Bank: Core Patterns I

### 🌟 Introduction
This week's patterns (Two Pointers, Sliding Window, Binary Search on Answer) are the "bread and butter" of algorithmic interviews. Interviewers love them because they test your ability to **optimize** brute-force solutions into efficient linear or logarithmic solutions. This bank focuses on recognizing the signals for these patterns.

---

## ❓ Consolidated Questions (No Answers)

### 🧩 Day 1: Two Pointers

**Q1:** You are given a sorted array of integers. How do you efficiently find two numbers that sum up to a specific target?
- 🔀 **Follow-up:** What if the array is **not** sorted? How does the time/space complexity trade-off change?
- 🔀 **Follow-up:** Can you modify your approach to return *all* unique pairs that sum to the target?

**Q2:** Explain the logic behind the "Container With Most Water" problem. Why is it safe to move the pointer pointing to the shorter line?
- 🔀 **Follow-up:** Does this greedy logic hold if we are calculating area differently (e.g., maximizing sum of heights)?

**Q3:** How would you remove duplicates from a sorted array in-place with O(1) extra space?
- 🔀 **Follow-up:** What if you are allowed to keep at most *two* occurrences of each element?

**Q4:** Given three sorted arrays, how do you find one element from each array such that the distance between the maximum and minimum of the three elements is minimized?

**Q5:** You have an array of integers including 0s. How do you move all 0s to the end while maintaining the relative order of non-zero elements?

**Q6:** Write the logic to merge two sorted arrays into a single sorted array.
- 🔀 **Follow-up:** How do you do this if `nums1` has enough buffer space at the end to hold `nums2`? (Merge Sorted Array LC#88)

---

### 🪟 Day 2: Sliding Window (Fixed Size)

**Q7:** Given an array and an integer K, how do you find the maximum sum of any contiguous subarray of size K?
- 🔀 **Follow-up:** How does your approach avoid O(N*K) complexity?

**Q8:** How do you find the maximum element in every window of size K?
- 🔀 **Follow-up:** What data structure allows O(1) or amortized O(1) access to the maximum? (Hint: Deque)

**Q9:** Given a string, find the number of substrings of size K that contain no repeated characters.

**Q10:** How do you calculate the moving average of the last K elements from a stream of numbers?

**Q11:** Determine if any permutation of a pattern string `P` exists as a substring in text `T`.
- 🔀 **Follow-up:** How do you update your character counts efficiently as the window slides?

---

### 📏 Day 3: Sliding Window (Variable Size)

**Q12:** Find the length of the longest substring without repeating characters.
- 🔀 **Follow-up:** What invariants determine when you expand vs. when you shrink the window?

**Q13:** Given an array of positive integers and a target S, find the minimal length of a contiguous subarray of which the sum is greater than or equal to S.
- 🔀 **Follow-up:** Why does this approach fail if the array contains negative numbers?

**Q14:** Find the longest substring with at most K distinct characters.
- 🔀 **Follow-up:** What data structure works best to track the count of distinct characters in the current window?

**Q15:** Given a binary array, find the maximum number of consecutive 1s if you can flip at most K 0s.

**Q16:** Find the minimum window substring in `S` that contains all characters of string `T`.
- 🔀 **Follow-up:** How do you efficiently check if the current window satisfies the condition?

---

### 🌲 Day 4: Divide and Conquer

**Q17:** Explain how Merge Sort uses the Divide and Conquer strategy. What is the time complexity of the "Combine" step?
- 🔀 **Follow-up:** Is Merge Sort stable? Why or why not?

**Q18:** How would you count the number of inversions in an array? (An inversion is a pair `(i, j)` such that `i < j` but `arr[i] > arr[j]`).
- 🔀 **Follow-up:** How is this related to Merge Sort?

**Q19:** Given an array where the majority element appears more than `n/2` times, how can you find it using Divide and Conquer?
- 🔀 **Follow-up:** Is there a more efficient linear time approach? (Boyer-Moore).

**Q20:** Implement logic to calculate `x` raised to the power `n` (`pow(x, n)`) in O(log n) time.

**Q21:** Given a 2D matrix where each row and column is sorted, how would you find the K-th smallest element?

**Q22:** Find the "Peak Element" in an array (an element strictly greater than its neighbors) using a Divide and Conquer approach.

---

### 🔎 Day 5: Binary Search on Answer

**Q23:** You have `N` packages and `D` days to ship them. How do you find the minimum ship capacity to handle all packages within `D` days?
- 🔀 **Follow-up:** What is the range [low, high] for your binary search?

**Q24:** Koko loves eating bananas. Given piles of bananas and `H` hours, finding the minimum eating speed `K`. What is the monotonicity here?

**Q25:** How do you find the square root of a number `x` to a precision of `10^-6` without using built-in functions?

**Q26:** Given an array of stall positions and `C` cows, place the cows such that the minimum distance between any two cows is maximized.
- 🔀 **Follow-up:** How do you write the `check(distance)` function?

**Q27:** Find the smallest divisor such that the sum of division results is less than or equal to a threshold.

**Q28:** Explain why Binary Search on Answer is applicable even if the input array is not sorted (e.g., the packages problem).
- 🔀 **Follow-up:** What *is* sorted in this context?

---

## 🧭 Usage Suggestions

1. **Mock Interviews:** Pair up with a peer. One person picks a question, the other solves it on a whiteboard/paper without code first.
2. **Flashcards:** Put the problem statement on the front and the "Pattern Name" + "Time Complexity" on the back.
3. **Deep Dives:** For any question where you stumble on the follow-up, re-read the instructional file for that day.
4. **Implementation:** Pick 1 question from each section to implement fully in your IDE.

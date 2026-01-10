# 🎙️ Week 04 Interview Q&A Reference: 35+ Questions by Pattern

**Status:** Interview Coaching Resource  
**Audience:** Students preparing for technical interviews  
**Format:** Questions only (no answers) + follow-ups to force deeper thinking  
**How to Use:** Pick 3-5 questions daily, attempt before looking up answers, discuss with partner

---

## 🎯 DAY 1: TWO-POINTER — 8 Questions

**Q1 (🟢 Easy):** Merge two sorted arrays without extra space. What's the trick?
- **Follow-up:** What if first array has enough space at end?
- **Follow-up:** Can you do this with indices instead of copying?

**Q2 (🟢 Easy):** Remove duplicates from sorted array in-place. Maintain what invariant?
- **Follow-up:** What if you need to keep two duplicates?
- **Follow-up:** Does order have to be preserved?

**Q3 (🟡 Medium):** Container with most water. Why can you move the smaller height pointer?
- **Follow-up:** Can moving the larger pointer ever improve area?
- **Follow-up:** Prove this is O(n) correct.

**Q4 (🟡 Medium):** Two Sum II (sorted array). Two-pointer or hash?
- **Follow-up:** Which is faster and why?
- **Follow-up:** What if array unsorted?

**Q5 (🔴 Hard):** Trapping Rain Water II (2D grid). Extension of 1D problem?
- **Follow-up:** How does 2D change the approach?
- **Follow-up:** What data structure helps here?

**Q6 (🟡 Medium):** Valid Palindrome (alphanumeric only). Two-pointer approach?
- **Follow-up:** How do you handle case insensitivity?
- **Follow-up:** What about "almost palindrome" (remove one char)?

**Q7 (🟡 Medium):** Rotate Array by K positions. O(1) space?
- **Follow-up:** Can you reverse-based approach explain why it works?
- **Follow-up:** Is this two-pointer or something else?

**Q8 (🔴 Hard):** Smallest Subarray Sum ≥ Target. Two-pointer or sliding window?
- **Follow-up:** Why does two-pointer fail with negative numbers?
- **Follow-up:** Is this more sliding window?

---

## 🎯 DAY 2: SLIDING WINDOW — FIXED SIZE — 7 Questions

**Q1 (🟢 Easy):** Maximum average subarray of fixed size K.
- **Follow-up:** Why is this O(n) and not O(nk)?
- **Follow-up:** Can you maintain the sum in O(1)?

**Q2 (🟡 Medium):** Maximum in sliding window of size K using deque.
- **Follow-up:** Why deque and not just tracking max?
- **Follow-up:** What happens when max leaves window?

**Q3 (🟡 Medium):** Sliding window median. Fixed K size, find median.
- **Follow-up:** Is deque enough or need heaps?
- **Follow-up:** What about finding median of entire array vs window?

**Q4 (🟡 Medium):** Best time to buy/sell stock (one transaction). Sliding window?
- **Follow-up:** Is this really sliding window or min-tracking?
- **Follow-up:** What if you can do multiple transactions?

**Q5 (🟡 Medium):** Number of subarrays with exactly K distinct elements.
- **Follow-up:** Is this fixed-size or variable-size window?
- **Follow-up:** How do you count "exactly" vs "at most"?

**Q6 (🟡 Medium):** Greedily maximize candy distribution with K resources.
- **Follow-up:** Is sliding window right approach here?
- **Follow-up:** What if resources vary?

**Q7 (🔴 Hard):** Minimum window with product ≤ K. Fixed or variable?
- **Follow-up:** Can you maintain product in O(1)?
- **Follow-up:** What about overflow with products?

---

## 🎯 DAY 3: SLIDING WINDOW — VARIABLE SIZE — 8 Questions

**Q1 (🟢 Easy):** Longest substring without repeating characters.
- **Follow-up:** How do you detect repeating efficiently?
- **Follow-up:** Use frequency map or character index?

**Q2 (🟡 Medium):** Longest substring with at most K distinct characters.
- **Follow-up:** What changes if "exactly K"?
- **Follow-up:** How do you count distinct?

**Q3 (🟡 Medium):** Permutation in string. Fixed or variable?
- **Follow-up:** Is this window of fixed size or variable?
- **Follow-up:** How do you check permutation match?

**Q4 (🟡 Medium):** Minimum window substring. Expand/shrink logic?
- **Follow-up:** Why shrink while valid, not just shrink when invalid?
- **Follow-up:** How many characters needed in window?

**Q5 (🟡 Medium):** Substring with concatenation of all words.
- **Follow-up:** Is this sliding window or hash?
- **Follow-up:** What if word lengths vary?

**Q6 (🔴 Hard):** Max consecutive ones after flipping at most K zeros.
- **Follow-up:** Is this sliding window with constraint?
- **Follow-up:** What's the invariant?

**Q7 (🔴 Hard):** Subarrays with K different integers.
- **Follow-up:** How do you count "exactly K"?
- **Follow-up:** Similar to "at most K"?

**Q8 (🔴 Hard):** Longest substring where no character repeats more than once.
- **Follow-up:** Variable window or fixed?
- **Follow-up:** How do you track frequency?

---

## 🎯 DAY 4: DIVIDE-CONQUER — 7 Questions

**Q1 (🟢 Easy):** What does divide-conquer mean? Give structure.
- **Follow-up:** What three parts are essential?
- **Follow-up:** Why is it recursive?

**Q2 (🟡 Medium):** Merge sort implementation. Write recurrence.
- **Follow-up:** Why O(n log n) and not O(n²)?
- **Follow-up:** Space complexity and why?

**Q3 (🟡 Medium):** Majority element (>n/2). Divide-conquer or voting?
- **Follow-up:** Both work? Compare efficiency.
- **Follow-up:** What if threshold is >n/3?

**Q4 (🟡 Medium):** Count inversions (merge sort based).
- **Follow-up:** What's an inversion?
- **Follow-up:** How does merge count cross-inversions?

**Q5 (🔴 Hard):** Maximum subarray sum (Kadane or divide-conquer).
- **Follow-up:** Both O(n)? Which is better and why?
- **Follow-up:** Can you divide-conquer this?

**Q6 (🔴 Hard):** Largest rectangle in histogram (divide-conquer).
- **Follow-up:** Is this really divide-conquer or stack-based?
- **Follow-up:** Complexity of each approach?

**Q7 (🔴 Hard):** Kth largest element (quickselect uses divide-conquer idea).
- **Follow-up:** Average O(n) but worst O(n²)?
- **Follow-up:** Why randomized pivot helps?

---

## 🎯 DAY 5: BINARY SEARCH — 8 Questions

**Q1 (🟢 Easy):** Binary search basics. What invariant is maintained?
- **Follow-up:** Why is mid = low + (high - low) / 2?
- **Follow-up:** First vs last occurrence variant?

**Q2 (🟡 Medium):** Binary search on answer space. Minimize maximum load.
- **Follow-up:** Why binary search works here (monotone)?
- **Follow-up:** How do you check feasibility?

**Q3 (🟡 Medium):** Peak in rotated sorted array.
- **Follow-up:** Why can binary search work here?
- **Follow-up:** What happens if no rotation?

**Q4 (🟡 Medium):** Capacity to ship packages within D days.
- **Follow-up:** Binary search on capacity or days?
- **Follow-up:** Feasibility check: how do you compute?

**Q5 (🟡 Medium):** Minimize max distance between gas stations (K stations).
- **Follow-up:** Binary search on max distance?
- **Follow-up:** Feasibility: can you place K stations under distance D?

**Q6 (🔴 Hard):** Allocate books to M students minimize max pages.
- **Follow-up:** Is binary search on max pages?
- **Follow-up:** Greedy feasibility check?

**Q7 (🔴 Hard):** Kth smallest element in BST efficiently.
- **Follow-up:** In-order traversal or binary search?
- **Follow-up:** Can you prune branches?

**Q8 (🔴 Hard):** Find in rotated sorted array with duplicates.
- **Follow-up:** Why is this harder than without duplicates?
- **Follow-up:** Can you still guarantee O(log n)?

---

## 🧠 Cross-Pattern Questions: Integration

**Q1 (🟡 Medium):** Longest substring with K distinct. Sliding window + data structure?
- **Follow-up:** Which data structure is best?
- **Follow-up:** Why not binary search here?

**Q2 (🟡 Medium):** Merge K sorted arrays. Two-pointer merge + which combine strategy?
- **Follow-up:** Pairwise merge vs heap?
- **Follow-up:** Divide-conquer on K arrays?

**Q3 (🔴 Hard):** Minimum rectangle to include all points. Divide-conquer + two-pointer?
- **Follow-up:** Both patterns needed?
- **Follow-up:** Why divide-conquer helps?

**Q4 (🔴 Hard):** Search in 2D matrix (row/col sorted). Binary search twice?
- **Follow-up:** Binary search rows then columns?
- **Follow-up:** Single binary search on flattened?

---

**Status:** Week 04 Interview Q&A Reference Complete  
**Total Questions:** 35+ (7-8 per day × 5)  
**Interview Prep Time:** 2-3 hours daily during interview week
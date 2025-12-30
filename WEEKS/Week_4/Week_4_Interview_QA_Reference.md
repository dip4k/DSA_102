# 🎙️ Week 4 — Interview Q&A Reference (Pattern-Based) (REGENERATED)

**🗓️ Week:** 4  
**📌 Topic:** Two Pointers, Sliding Window, Divide & Conquer, Binary Search on Answer  
**🎯 Usage:** Review before interviews. Focus on "Why this pattern?" and "What are the trade-offs?"

---

## 🧠 CONCEPTUAL & THEORETICAL QUESTIONS

### **1. Two Pointers vs Hash Map for Two Sum**

**Q: "For Two Sum, when do you choose Two Pointers over Hash Map?"**

**A:**
- **Hash Map:** Works on **unsorted** arrays. O(n) time, O(n) space. Standard choice for general case.
- **Two Pointers:** Requires **sorted** array. O(n) time, O(1) space. Better space efficiency.
- **Interview Insight:** If problem explicitly says "sorted array", interviewer expects Two Pointers to show understanding of space/time trade-offs.
- **Trade-off Summary:** Hash Map is more flexible (any array). Two Pointers is more space-efficient (sorted only).

---

### **2. Fixed vs Variable Sliding Window**

**Q: "How do you distinguish between fixed and variable sliding window problems?"**

**A:**
- **Fixed Window:** Window size k is **given** and constant.
  - Example: "Find max in every window of size 3."
  - Logic: Simple add/remove (O(n) single pass).
- **Variable Window:** Window size **varies** based on constraint.
  - Example: "Find longest substring with at most 2 distinct characters."
  - Logic: Expand right, contract left when constraint violated (O(n) amortized).
- **Key Difference:** Fixed = size known. Variable = size discovered.

---

### **3. Why Divide & Conquer for Merge K Lists?**

**Q: "Why is Divide & Conquer better than merging lists one-by-one?"**

**A:**
- **One-by-one (Sequential):**
  - Merge L1 + L2: O(n)
  - Merge result + L3: O(n+m) ≈ O(n)
  - ... Total: O(N*K) where N = total elements, K = number of lists
- **Divide & Conquer (Binary Tree):**
  - Depth: O(log K) (binary tree height)
  - Work per level: O(N) (merging all elements at that level)
  - Total: O(N log K)
- **Speedup:** For K=1000: Sequential = 1000 passes. D&C = 10 passes. **100x faster.**
- **Why:** Each element compared log K times (tree depth) vs K times (sequential).

---

### **4. Binary Search on Answer: Pattern Recognition**

**Q: "How do you recognize that Binary Search on Answer is needed?"**

**A:**
Look for these **red flag keywords:**
1. **"Minimize the maximum"** (e.g., minimize max load)
2. **"Maximize the minimum"** (e.g., maximize min speed)
3. **"Find minimum X such that..."** (e.g., min capacity to ship in D days)

**Pattern:**
- Answer space is **monotonic**: If answer X works, X+1 also works (or vice versa).
- **Feasibility function exists**: Can check "Does X work?" efficiently (O(n) or better).

**Example:** "Ship packages in D days. Find min capacity."
- Capacity 1: NO. Capacity 100: YES. Capacity 150: YES.
- Monotonic! Binary search on [1, sum] to find minimum YES.

---

## 💻 PROBLEM-SPECIFIC Q&A

### **5. Two Sum II Variants**

**Q: "What's the difference between Two Sum (LeetCode #1) and Two Sum II (LeetCode #167)?"**

**A:**
- **Two Sum (Unsorted):** Use Hash Map. O(n) time, O(n) space.
- **Two Sum II (Sorted):** Use Two Pointers. O(n) time, O(1) space.
- **Interview Strategy:** If interviewer says "sorted", they're testing if you recognize Two Pointers opportunity. Mention Hash Map first (shows flexibility), then optimize to Two Pointers (shows expertise).

---

### **6. Container with Most Water (Greedy Logic)**

**Q: "Why does moving the shorter line work in Container with Most Water?"**

**A:**
- **Area Formula:** `width * min(left_height, right_height)`
- **Greedy Insight:**
  - If we move the **taller** line inward: Width ↓, Height still capped by shorter. **Area ↓ (can't improve).**
  - If we move the **shorter** line inward: Width ↓, BUT might find taller line. **Area might ↑ (chance to improve).**
- **Conclusion:** Always move the shorter line (greedy choice is optimal).

---

### **7. Longest Substring Without Repeating: Why O(n)?**

**Q: "Why is Variable Sliding Window O(n) and not O(n²) despite nested pointers?"**

**A:**
- **Amortized Analysis:** Each element is visited at most **twice**:
  - Once by right pointer (expanding)
  - Once by left pointer (contracting)
- **Total Movements:** Right moves n times. Left moves n times. Total = 2n = O(n).
- **Key:** Left pointer **never moves backward**. It only moves right (monotonic).

---

### **8. Minimum Window Substring (Complex Constraint)**

**Q: "How do you maintain the constraint in Minimum Window Substring?"**

**A:**
- **Setup:** Two frequency maps: `target_freq` (what we need) and `window_freq` (what we have).
- **Expand:** Add char to window. Update window_freq. Track "satisfied count" (how many target chars met).
- **Contract:** While all target chars satisfied, remove left char (try to minimize window).
- **Optimization:** Use a counter `required` to track unsatisfied chars (O(1) check instead of comparing all frequencies).

---

### **9. Binary Search Boundary Conditions**

**Q: "What's the difference between 'Find first occurrence' and 'Find minimum feasible answer'?"**

**A:**
- **First Occurrence (Array):** Binary search on sorted array. Find leftmost index where element appears.
- **Minimum Feasible (Answer):** Binary search on answer space. Find smallest value where feasibility = TRUE.
- **Key Difference:** Array search = find existing element. Answer search = find optimal value in a range.

---

## 🏛️ BEHAVIORAL / SYSTEM DESIGN

### **10. Handling Edge Cases**

**Q: "What are critical edge cases for Sliding Window problems?"**

**A:**
1. **Window larger than array:** k > n. No valid windows. Return empty or error.
2. **All elements identical:** Logic unchanged, but results may be trivial.
3. **Empty array:** n = 0. Return empty results.
4. **Window size = 1:** Each element is its own window (trivial case).
5. **Constraint never satisfied:** Variable window may never expand. Handle gracefully.

---

### **11. Two Pointers for Merge Operations**

**Q: "How do you use Two Pointers to merge two sorted arrays?"**

**A:**
- **Setup:** Pointer i in array1, pointer j in array2.
- **Logic:**
  - Compare arr1[i] vs arr2[j].
  - Add smaller to result. Advance that pointer.
  - Repeat until one array exhausted.
  - Copy remaining elements from other array.
- **Complexity:** O(n+m). Each element visited once.
- **Use Case:** Merge Sort, database merge joins.

---

### **12. Divide & Conquer Parallelization**

**Q: "Why is Divide & Conquer good for parallel computing?"**

**A:**
- **Independence:** Subproblems are independent (no shared state).
- **Parallelization:** Left and right subtrees can run on different processors simultaneously.
- **Speedup:** With unlimited processors, time = O(log n) (critical path = tree height).
- **Real Systems:** MapReduce, Apache Spark, distributed databases use this.

---

### **13. Fast/Slow Pointers Beyond Cycle Detection**

**Q: "What are other uses of Fast/Slow pointers besides cycle detection?"**

**A:**
1. **Find Middle of Linked List:** Fast moves 2x. When fast reaches end, slow is at middle.
2. **Palindrome Check (Linked List):** Fast/Slow to find middle, reverse second half, compare.
3. **Remove Nth from End:** Fast moves N steps ahead, then both move until fast reaches end.
4. **Detect Cycle Start:** After detecting cycle, reset one pointer to head, move both at same speed. They meet at cycle start.

---

## 🎯 ADVANCED / SYSTEM-LEVEL QUESTIONS

### **14. Load Balancing with Binary Search**

**Q: "How would you use Binary Search on Answer for load balancing?"**

**A:**
- **Problem:** Distribute N tasks among K workers. Minimize max load per worker.
- **Answer Space:** [max_task_size, sum_of_all_tasks]
- **Feasibility Function:** Given max_load, can we distribute tasks among K workers? (Greedy packing: add tasks to worker until exceeding max_load)
- **Binary Search:** Find minimum max_load where feasibility = TRUE.
- **Complexity:** O(N * log(sum)) vs O(N * sum) brute force.

---

### **15. External Merge Sort (Divide & Conquer)**

**Q: "How would you sort 1 TB of data that doesn't fit in RAM?"**

**A:**
- **External Merge Sort (Divide & Conquer):**
  1. **Divide:** Split data into chunks that fit in RAM (e.g., 1 GB chunks).
  2. **Conquer:** Sort each chunk in RAM (Quick Sort). Write sorted runs to disk.
  3. **Combine:** K-way merge of sorted runs (use min-heap of size K).
- **Complexity:** O(N log N) with I/O overhead.
- **Real System:** Database systems (PostgreSQL, MySQL) use this for large table sorts.

---

**Generated:** December 30, 2025  
**Version:** 9.0 (Regenerated)  
**Status:** ✅ COMPLETE
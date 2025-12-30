# 🎙️ Week 4.5 — Interview Q&A Reference (Tier 1 Patterns) (COMPLETE)

**🗓️ Week:** 4.5  
**📌 Topics:** Hash Map, Monotonic Stack, Merge, Partition, Kadane's, Fast & Slow  
**🎯 Usage:** Review before interviews. Focus on pattern recognition and trade-offs.

---

## 🧠 HASH MAP / HASH SET

### **Q1: Two Sum — Hash Map vs Nested Loops?**

**Q:** "Why use Hash Map for Two Sum instead of nested loops?"

**A:**
- **Nested Loops:** Check all pairs. O(n²) time.
  - For n=1000: 1,000,000 comparisons.
- **Hash Map:** Single pass, check complement. O(n) time, O(n) space.
  - For n=1000: 1,000 operations.
- **Speedup:** 1000x for n=1000.
- **Trade-off:** Space (O(n)) for time (O(n²) → O(n)).

**Interview Insight:** If candidate uses nested loops, interviewer knows they don't understand Hash Maps (red flag).

---

### **Q2: Hash Map vs Two Pointers for Two Sum II (Sorted)?**

**Q:** "If array is sorted, which is better?"

**A:**
- **Hash Map:** O(n) time, O(n) space. Works but wastes space.
- **Two Pointers:** O(n) time, O(1) space. Better choice.
- **Decision:** Always prefer Two Pointers for sorted arrays (space advantage).

**Interview Signal:** Recognizing this trade-off shows senior-level thinking.

---

### **Q3: Valid Anagram — Sort vs Hash Map?**

**Q:** "How do you check if two strings are anagrams?"

**A:**
**Approach 1: Sort**
- Sort both strings, compare. O(n log n).

**Approach 2: Hash Map (Better)**
- Count character frequencies for each string. O(n).
- Compare frequency maps. O(1) (alphabet size bounded).

**Why Hash Map Wins:** O(n) vs O(n log n). For n=10,000: ~10K ops vs ~130K ops.

---

### **Q4: When Does Hash Map Degrade to O(n)?**

**Q:** "Hash Map is O(1) average. When does it become O(n)?"

**A:**
**Worst Case:** All keys hash to same bucket (pathological).
- **Cause:** Bad hash function OR adversarial input.
- **Result:** Lookup becomes O(chain_length) = O(n).

**Mitigation:**
- Good hash functions (MurmurHash, SipHash).
- Randomized hashing (Python 3.3+).

---

## 🧠 MONOTONIC STACK

### **Q5: Next Greater Element — Why Monotonic Stack?**

**Q:** "How does Monotonic Stack solve Next Greater in O(n)?"

**A:**
**Naive:** For each element, scan right until find greater. O(n²).

**Monotonic Stack (Decreasing):**
- Iterate right-to-left.
- Stack maintains decreasing order.
- For each element:
  - Pop stack while stack.top ≤ current (they're not "next greater" for anyone).
  - If stack not empty: stack.top is next greater.
  - Push current.
- **Time:** Each element pushed/popped once. O(n).

**Key Insight:** Stack "remembers" potential next greater candidates in decreasing order.

---

### **Q6: Trapping Rain Water — Monotonic Stack vs Two Pointers?**

**Q:** "Which approach for Trapping Rain Water?"

**A:**
**Approach 1: Monotonic Stack**
- Track left wall (stack).
- When find taller wall, calculate water trapped between.
- O(n) time, O(n) space.

**Approach 2: Two Pointers (Better)**
- Track max height from left and right.
- Move pointers inward, calculate water at each position.
- O(n) time, O(1) space.

**Winner:** Two Pointers (better space). But Monotonic Stack is more intuitive for some.

---

## 🧠 MERGE OPERATIONS & INTERVALS

### **Q7: Merge K Sorted Lists — Why Heap?**

**Q:** "Why use heap for merging K lists instead of sequential?"

**A:**
**Sequential (Merge 1-by-1):**
- Merge L1 + L2 → result (size N/K each).
- Merge result + L3 → result (size 2N/K + N/K).
- ...
- **Total:** O(N*K) where N = total elements.

**Heap (K-way Merge):**
- Min-heap of size K (one node from each list).
- Extract min, add its next node.
- **Total:** O(N log K) (N elements, each log K heap operation).

**For K=1000, N=1M:** Sequential = 10⁹ ops. Heap = 10⁷ ops. **100x faster!**

---

### **Q8: Merge Intervals — Why Sort First?**

**Q:** "Merge overlapping intervals. Why sort?"

**A:**
**Without Sorting:** Compare all pairs to check overlap. O(n²).

**With Sorting:**
- Sort intervals by start time. O(n log n).
- Iterate, merge consecutive overlapping intervals. O(n).
- **Total:** O(n log n) (dominated by sort).

**Key Insight:** After sorting, only consecutive intervals can overlap (greedy merge works).

---

## 🧠 PARTITION & CYCLIC SORT

### **Q9: Dutch National Flag — Three Pointers?**

**Q:** "Sort colors [2,0,2,1,1,0] in-place. How?"

**A:**
**Dutch National Flag Pattern:**
- Three pointers: `low` (boundary of 0s), `mid` (current), `high` (boundary of 2s).
- Invariant:
  - [0, low) = all 0s
  - [low, mid) = all 1s
  - [mid, high] = unprocessed
  - (high, end] = all 2s
- **Logic:**
  - If arr[mid] == 0: swap with low, move both.
  - If arr[mid] == 1: move mid.
  - If arr[mid] == 2: swap with high, move high left (don't move mid yet, need to process swapped element).
- **Time:** O(n) single pass. **Space:** O(1).

**Interview Trick:** Don't move `mid` after swapping with `high` (common mistake).

---

### **Q10: Cyclic Sort — Find Missing Number?**

**Q:** "Array [3,0,1] contains 0 to n. Find missing (2 in this case)."

**A:**
**Cyclic Sort Pattern:**
- Numbers in [0, n] → Place each number at its index (arr[i] = i).
- **Logic:**
  - For each i, if arr[i] != i AND arr[i] != n: swap arr[i] with arr[arr[i]].
  - After sorting: arr[i] != i is the missing number.
- **Time:** O(n). Each element swapped at most once.

**Key:** Works only when numbers are in range [0, n] or [1, n].

---

## 🧠 KADANE'S ALGORITHM

### **Q11: Maximum Subarray — Why Kadane's?**

**Q:** "Find maximum sum of contiguous subarray. Why Kadane's?"

**A:**
**Brute Force:** Check all O(n²) subarrays. Each sum O(1) with prefix. Total O(n²).

**Kadane's Algorithm (DP):**
- **State:** max_ending_here = maximum sum of subarray ending at index i.
- **Recurrence:** `max_ending_here = max(arr[i], max_ending_here + arr[i])`
  - Either start fresh at i OR extend previous subarray.
- **Track:** global max across all positions.
- **Time:** O(n) single pass.

**Example:** `[-2,1,-3,4,-1,2,1,-5,4]`
- max_ending = [-2, 1, -2, 4, 3, 5, 6, 1, 5]
- global_max = 6 (subarray [4,-1,2,1])

---

### **Q12: Kadane's — Handle All Negatives?**

**Q:** "What if all numbers are negative?"

**A:**
**Edge Case:** All negative. Standard Kadane's might return 0 (empty subarray).

**Fix:**
- Initialize `global_max = arr[0]` (not 0).
- OR: If all negative, return max element (single element subarray).

**Interview Tip:** Clarify with interviewer: "Can subarray be empty? If all negative, return 0 or max element?"

---

## 🧠 FAST & SLOW POINTERS

### **Q13: Linked List Cycle — Why Fast & Slow?**

**Q:** "Detect cycle in linked list. Why not Hash Set?"

**A:**
**Hash Set:** Store visited nodes. O(n) time, O(n) space.

**Fast & Slow Pointers (Floyd's):**
- Slow moves 1 step, fast moves 2.
- If cycle exists, they meet inside cycle.
- **Time:** O(n). **Space:** O(1).

**Why It Works:** In a cycle, relative speed = 1. Fast catches slow within cycle length iterations.

**Advantage:** O(1) space (huge win for memory-constrained systems).

---

### **Q14: Find Cycle Start — Floyd's Extended?**

**Q:** "After detecting cycle, how to find where cycle starts?"

**A:**
**Floyd's Extended Algorithm:**
1. **Detect cycle:** Fast & slow meet at some node M inside cycle.
2. **Find start:** Reset one pointer to head. Move both at same speed (1 step). They meet at cycle start.

**Why It Works (Math):**
- Let L = distance from head to cycle start.
- Let C = cycle length.
- When they first meet: slow traveled L + k*C + x, fast traveled 2*(L + k*C + x).
- Distance from head to start = distance from meeting point to start (modulo cycle).

**Interview Tip:** This is advanced. If asked, explain concept (don't need rigorous proof unless interviewer insists).

---

### **Q15: Fast & Slow Beyond Linked Lists?**

**Q:** "Can Fast & Slow be used for arrays or other structures?"

**A:**
**Yes, but less common:**
- **Happy Number:** Treat sequence as "linked list" (each number points to its digit square sum). Detect cycle.
- **Find Duplicate (Cyclic Array):** If array elements are indices, treat as linked list.

**General Rule:** Fast & Slow works when structure has "next" pointer or can be modeled as directed graph.

---

## 🎯 ADVANCED / SYSTEM-LEVEL QUESTIONS

### **Q16: LRU Cache — Hash Map + Doubly Linked List?**

**Q:** "How does LRU Cache achieve O(1) get and put?"

**A:**
**Data Structures:**
- **Hash Map:** key → node (O(1) lookup).
- **Doubly Linked List:** Tracks LRU order (head = most recent, tail = least recent).

**Operations:**
- **get(key):** Hash Map lookup (O(1)). Move node to head (O(1) with doubly linked).
- **put(key, value):** Hash Map insert (O(1)). Add node at head. If capacity exceeded, remove tail (O(1)).

**Real Use:** Redis, Memcached, browser caches.

---

### **Q17: Consistent Hashing for Distributed Caching?**

**Q:** "How to distribute cache across K servers?"

**A:**
**Naive Hash:** `server = hash(key) % K`. Problem: Adding/removing server rehashes all keys.

**Consistent Hashing:**
- Hash keys and servers onto a ring [0, 2^32).
- Key → assigned to next server clockwise on ring.
- **Advantage:** Adding/removing server affects only ~1/K keys (minimal rehashing).

**Real Use:** Amazon DynamoDB, Cassandra, Memcached.

---

**Generated:** December 30, 2025  
**Version:** 9.0 (Week 4.5)  
**Status:** ✅ COMPLETE
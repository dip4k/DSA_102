# ✅ Week 03 Daily Progress Checklist: Action Plan & Execution

**Audience:** Self-directed learners  
**Purpose:** Track daily progress, ensure depth not just breadth

---

## 📅 MONDAY: Day 1 Core — Elementary Sorts (Bubble, Selection, Insertion)

### Morning Session (2.5 hours)

**Read Instructional Content (50 min)**
- [ ] Read Week 03 Day 1 Instructional file: Elementary Sorts
- [ ] Focus areas: bubble, selection, insertion mechanisms
- [ ] Write down 3 key insights:
  1. ___________________________________
  2. ___________________________________
  3. ___________________________________

**Visualize & Understand (60 min)**
- [ ] Draw bubble sort on [5,2,8,1,9]
  - [ ] Show array state after each pass
  - [ ] Mark invariant: "i largest elements in final position"
  - [ ] Count comparisons and swaps
- [ ] Draw selection sort on [5,2,8,1,9]
  - [ ] Show minimum finding each step
  - [ ] Mark invariant: "[0,i) sorted and contains i smallest"
- [ ] Draw insertion sort on [5,2,8,1,9]
  - [ ] Show sorted prefix growing
  - [ ] Mark insertion positions

**Comprehension Check (20 min)**
- [ ] Why is bubble sort O(n) best but O(n²) worst?
- [ ] Why is selection sort ALWAYS O(n²)?
- [ ] When would you use insertion sort? Answer: _____________

### Afternoon Session (3 hours)

**Code Implementation (2.5 hours)**
- [ ] Implement Bubble Sort
  - [ ] First attempt time: ____ min
  - [ ] Correct? ☐ Yes ☐ Fix needed
  - [ ] Add optimization (early termination)
  
- [ ] Implement Selection Sort
  - [ ] First attempt time: ____ min
  - [ ] Trace on [3,1,4,1,5], verify correctness
  
- [ ] Implement Insertion Sort
  - [ ] First attempt time: ____ min
  - [ ] Compare: shifting vs swapping version
  - [ ] Which is faster? _______________

**Problem Practice (30 min)**
- [ ] Identify: Which sort for nearly-sorted array? Answer: ___________
- [ ] Design: Worst-case input for each algorithm
- [ ] Trace: Manual execution on paper

### Evening Checkpoint
- [ ] Can code bubble sort from scratch? ☐ Yes ☐ No
- [ ] Can code selection sort from scratch? ☐ Yes ☐ No
- [ ] Can code insertion sort from scratch? ☐ Yes ☐ No
- [ ] Understand stability concept? ☐ Yes ☐ Partial ☐ No
- [ ] Ready for Day 2? ☐ Yes ☐ Review needed

---

## 📅 TUESDAY: Day 2 Core — Advanced Sorts (Merge Sort, Quick Sort)

### Morning Session (2.5 hours)

**Read Instructional Content (50 min)**
- [ ] Read Week 03 Day 2 Instructional: Merge Sort and Quick Sort
- [ ] Note: Divide-conquer, recurrence relations, pivot strategies
- [ ] Key insights:
  1. ___________________________________
  2. ___________________________________

**Visualize Merge Sort (40 min)**
- [ ] Draw recursion tree for [3,1,4,1,5,9]
  - [ ] Show splits at each level
  - [ ] Show merging at each level
  - [ ] Count levels: log₂(6) ≈ 2.5
- [ ] Trace merge function: [1,3,4] + [1,5,9]
  - [ ] Show two-pointer scan
  - [ ] Verify O(n+m) merging

**Visualize Quick Sort (30 min)**
- [ ] Draw partitioning [3,1,4,1,5,9] with pivot=4
  - [ ] Show elements < 4 on left
  - [ ] Show elements > 4 on right
  - [ ] Recursion on each side

### Afternoon Session (3 hours)

**Code Implementation (2 hours)**
- [ ] Implement Merge Sort
  - [ ] First attempt time: ____ min
  - [ ] Verify merge function works correctly
  - [ ] Test on duplicates: [1,1,2,2]
  
- [ ] Implement Quick Sort
  - [ ] Partition function: clear? ☐ Yes
  - [ ] First pivot strategy time: ____ min
  - [ ] Add random pivot strategy
  
- [ ] Trace Quick Sort worst-case
  - [ ] Input: [1,2,3,4,5] (sorted)
  - [ ] Show why O(n²)

**Analysis (1 hour)**
- [ ] Compare Merge vs Quick on different inputs:
  - [ ] Already sorted: Which faster?
  - [ ] Random: Which faster?
  - [ ] Duplicates: Which faster?
- [ ] Space: Merge uses O(n), Quick uses O(log n). Impact?

### Evening Checkpoint
- [ ] Can code merge sort from scratch? ☐ Yes ☐ No
- [ ] Can code merge function correctly? ☐ Yes ☐ No
- [ ] Can code quick sort? ☐ Yes ☐ No
- [ ] Understand O(n log n) recurrence? ☐ Yes ☐ Partial
- [ ] Ready for Day 3? ☐ Yes ☐ Review Day 2

---

## 📅 WEDNESDAY: Day 3 Core — Heaps & Heap Sort

### Morning Session (2.5 hours)

**Read Instructional Content (50 min)**
- [ ] Read Week 03 Day 3: Heaps and Heap Sort
- [ ] Key topics: array representation, bubble operations, build-heap
- [ ] Key insights:
  1. ___________________________________
  2. ___________________________________

**Visualize Heap Structure (50 min)**
- [ ] Draw min-heap tree from array [5,10,20,40,50,30,25]
  - [ ] Verify parent ≤ children
  - [ ] Mark root (minimum)
  - [ ] Verify it's NOT sorted
  
- [ ] Index mapping: parent(i) = (i-1)/2, left(i) = 2i+1, right(i) = 2i+2
  - [ ] Test on tree: index 2 → parent index 0 ✓
  - [ ] Test on tree: index 0 → left index 1 ✓

**Visualize Operations (30 min)**
- [ ] Insert 3 into [5,10,20,40,50]
  - [ ] Add at end: [5,10,20,40,50,3]
  - [ ] Bubble up: 3 < 50 → swap
  - [ ] Continue until heap property restored
  
- [ ] Extract min from [5,10,20,40,50]
  - [ ] Remove 5, move 50 to root
  - [ ] Bubble down 50 until heap property

### Afternoon Session (3 hours)

**Code Implementation (2 hours)**
- [ ] Implement Binary Heap
  - [ ] Insert operation: time _____ min, correct? ☐
  - [ ] Extract-min operation: time _____ min, correct? ☐
  - [ ] Bubble-up function: recursive or iterative? ___________
  - [ ] Bubble-down function: compare with both children? ☐
  
- [ ] Implement Build Heap (two approaches)
  - [ ] Naive O(n log n): insert n times
  - [ ] Optimal O(n): bottom-up sift-down
  - [ ] Verify both produce valid heap
  
- [ ] Implement Heap Sort
  - [ ] Build heap: _____ time
  - [ ] Extract n times: _____ time
  - [ ] Total: _____ time

**Analysis (1 hour)**
- [ ] Trace heap sort on [9,3,2,6,5]
  - [ ] Build heap result?
  - [ ] After extraction 1, 2, 3, ...?
- [ ] Compare to merge/quick sort on same input

### Evening Checkpoint
- [ ] Can implement heap insert? ☐ Yes ☐ No
- [ ] Can implement heap extract? ☐ Yes ☐ No
- [ ] Can build heap in O(n)? ☐ Yes ☐ No
- [ ] Understand why heap used in Dijkstra? ☐ Yes ☐ Partial
- [ ] Ready for Day 4? ☐ Yes ☐ Review Day 3

---

## 📅 THURSDAY: Day 4 Core — Hash Tables I (Separate Chaining)

### Morning Session (2.5 hours)

**Read Instructional Content (50 min)**
- [ ] Read Week 03 Day 4: Hash Tables I
- [ ] Topics: hash functions, separate chaining, load factor, resizing
- [ ] Key insights:
  1. ___________________________________
  2. ___________________________________

**Visualize Hash Table (60 min)**
- [ ] Design hash function for integers
  - [ ] h(x) = x mod m (m = 7 prime)
  - [ ] Hash keys [15, 22, 11, 8, 4] into [0..6]
  - [ ] Which collide? ___________
  
- [ ] Draw separate chaining with collisions
  - [ ] Bucket 0: [15] → [22] → null
  - [ ] Bucket 1: [8] → null
  - [ ] Bucket 4: [11] → [4] → null

**Understand Load Factor (20 min)**
- [ ] α = n / m (items / buckets)
- [ ] With keys [15, 22, 11, 8, 4] and m=7: α = 5/7 ≈ 0.71
- [ ] Expected chain length: α ≈ 0.71
- [ ] Expected search: O(1 + 0.71) ✓

### Afternoon Session (3 hours)

**Code Implementation (2.5 hours)**
- [ ] Implement Hash Function
  - [ ] For integers: h(x) = x % prime
  - [ ] For strings: rolling hash or sum (simple first)
  - [ ] Test distribution: count collisions?
  
- [ ] Implement Hash Table (Separate Chaining)
  - [ ] insert(key, value): time _____ min, correct? ☐
  - [ ] search(key): time _____ min, correct? ☐
  - [ ] delete(key): time _____ min, correct? ☐
  
- [ ] Implement Resize Logic
  - [ ] When to resize? (α threshold)
  - [ ] How to resize? (double m, rehash all)
  - [ ] Test with 10+ insertions

**Analysis (30 min)**
- [ ] Calculate load factor as you insert elements
- [ ] When does it trigger resize?
- [ ] Time to rehash all elements? O(n)

### Evening Checkpoint
- [ ] Can code hash function? ☐ Yes ☐ No
- [ ] Can code insert/search/delete? ☐ Yes ☐ No
- [ ] Understand load factor and resize? ☐ Yes ☐ Partial
- [ ] Ready for Day 5? ☐ Yes ☐ Review

---

## 📅 FRIDAY: Day 5 Core — Hash Tables II & Rolling Hash

### Morning Session (2.5 hours)

**Read Instructional Content (50 min)**
- [ ] Read Week 03 Day 5: Hash Tables II and Rabin-Karp
- [ ] Topics: open addressing, rolling hash, Rabin-Karp, security
- [ ] Key insights:
  1. ___________________________________
  2. ___________________________________

**Visualize Open Addressing (60 min)**
- [ ] Linear probing on [15, 22, 11, 8, 4] with m=7
  - [ ] If 15 % 7 = 1: insert at index 1
  - [ ] If 22 % 7 = 1 (collision): try 2, 3, ...
  - [ ] Show probe sequence for each
  
- [ ] Rolling Hash Formula
  - [ ] H(S) = S[0]·B^(m-1) + S[1]·B^(m-2) + ... + S[m-1]
  - [ ] Example: H("CAT") = C·B² + A·B + T (mod p)
  - [ ] For "ATO": (H("CAT") - C·B²)·B + O

**Visualize Rabin-Karp (20 min)**
- [ ] Find pattern "AB" in text "ABXAB"
  - [ ] Hash "AB"
  - [ ] Hash "AB" (position 0), "BX", "XA", "AB" (position 3)
  - [ ] Match at positions 0 and 3

### Afternoon Session (3 hours)

**Code Implementation (1.5 hours)**
- [ ] Implement Rolling Hash
  - [ ] Hash formula: clear? ☐ Yes
  - [ ] Rolling update: time _____ min, correct? ☐
  - [ ] Precompute B^(m-1): why? ___________
  
- [ ] Implement Rabin-Karp
  - [ ] Find pattern P in text T
  - [ ] Hash and roll: time _____ min, correct? ☐
  - [ ] Verify on mismatch? ☐ Yes (important!)

**Open Addressing (1.5 hours)**
- [ ] Implement Linear Probing
  - [ ] Insert, search, delete with probing
  - [ ] Handle tombstones for deleted elements
  - [ ] Test on small table

### Evening Checkpoint
- [ ] Can code rolling hash? ☐ Yes ☐ No
- [ ] Can code Rabin-Karp? ☐ Yes ☐ No
- [ ] Understand rolling update O(1)? ☐ Yes ☐ Partial
- [ ] Understand verification step importance? ☐ Yes ☐ No

---

## 📅 SATURDAY: Integration & Synthesis

### Morning Session (2 hours)

**Review All Three Components (90 min)**
- [ ] Sorting comparison (1 min explanation each):
  - [ ] Bubble sort: _______________
  - [ ] Selection sort: _______________
  - [ ] Insertion sort: _______________
  - [ ] Merge sort: _______________
  - [ ] Quick sort: _______________
  - [ ] Heap sort: _______________
  - [ ] Total time: _________ min (should be < 10 min)

- [ ] Heap concepts (1 min):
  - [ ] Structure and property: _______________
  - [ ] Insert/extract complexity: _______________
  - [ ] Build-heap optimal: _______________

- [ ] Hash table concepts (1 min):
  - [ ] Hash function purpose: _______________
  - [ ] Collision resolution: _______________
  - [ ] Load factor management: _______________

**Algorithm Selection Quiz (30 min)**
- [ ] Given 8 scenarios, choose algorithm and justify:
  - [ ] Sort 100 million ints with O(1) space: _______________
  - [ ] Find K most frequent items: _______________
  - [ ] Build priority queue for Dijkstra: _______________
  - [ ] String pattern matching: _______________
  - [ ] (continue for 8 total)
  - [ ] Accuracy: ____ / 8 correct

### Afternoon Session (2 hours)

**Integration Problems (2 hours)**
- [ ] Problem 1: Sort + Hash
  - [ ] LeetCode Kth Largest Element
  - [ ] Can use heap or quick sort selection
  - [ ] Time: _____ min, correct? ☐
  
- [ ] Problem 2: Hash + Analysis
  - [ ] Design LRU cache
  - [ ] Need hash table + doubly linked list
  - [ ] Time: _____ min, correct? ☐

- [ ] Problem 3: Multi-Algorithm
  - [ ] Merge K sorted lists (heap)
  - [ ] Time: _____ min, correct? ☐

### Evening Session (1 hour)

**Final Reflection**
- [ ] Most confident topic: ______________
- [ ] Topic needing more practice: ______________
- [ ] Real insight from this week: _________________
- [ ] Real-world algorithm I'll use: ______________

---

## 🎯 MASTERY VERIFICATION

**By end of Week 03, validate:**

**Sorting Implementations:**
- [ ] Bubble sort from scratch, no reference
- [ ] Selection sort from scratch, no reference
- [ ] Insertion sort from scratch, no reference
- [ ] Merge sort from scratch, with merge function
- [ ] Quick sort from scratch, with partition
- [ ] Heap sort from scratch, with build-heap

**Heap Operations:**
- [ ] Insert: code and trace
- [ ] Extract: code and trace
- [ ] Build-heap O(n): code and explain why
- [ ] Heap sort: full trace from array to sorted

**Hash Tables:**
- [ ] Hash function: design and test
- [ ] Separate chaining: insert, search, delete
- [ ] Load factor: understand and implement resize
- [ ] Rolling hash: implement and test

**Total: 6 sorts + 4 heap ops + 4 hash ops = 14 algorithms**

**Final Assessment:**
- [ ] Can explain trade-offs between all sorting algorithms
- [ ] Can code any algorithm without reference
- [ ] Can trace manually and predict performance
- [ ] Can choose algorithm for constraints
- [ ] Can implement 2-3 integration problems
- [ ] Ready for interviews? ☐ YES ☐ REVIEW needed

---

## 📝 Notes Section

**Breakthroughs this week:**
- Insight 1: _____________________________
- Insight 2: _____________________________
- Insight 3: _____________________________

**Areas to review before interview:**
- Topic 1: _____________________________
- Topic 2: _____________________________

**Favorite algorithm explanation:**
- _____________________________

---

**Total Time Investment:** 22+ hours (core)  
**Algorithm Mastery Level:** ______ / 100  
**Ready for Week 04?** ☐ YES (Move forward) ☐ REVIEW (One more day)


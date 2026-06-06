# 🎙️ Week 03 Interview Q&A Reference: 40+ Questions by Topic

**Audience:** Students preparing for technical interviews  
**Format:** Questions only (no answers) + follow-ups to force deeper thinking  
**How to Use:** Pick 4-5 questions daily, attempt before looking up answers, discuss with partner

---

## 🎯 DAY 1: ELEMENTARY SORTS — 8 Questions

**Q1 (🟢 Easy):** What's the difference between stable and unstable sorts?
- **Follow-up:** Which elementary sorts are stable?
- **Follow-up:** When does stability matter in real code?

**Q2 (🟢 Easy):** Implement bubble sort. What does the invariant guarantee after i passes?
- **Follow-up:** Can you optimize it for nearly-sorted arrays?
- **Follow-up:** Comparisons vs swaps count on average input?

**Q3 (🟡 Medium):** Selection sort vs insertion sort. Which is faster and why?
- **Follow-up:** When would you use selection over insertion?
- **Follow-up:** Can you modify one to behave like the other?

**Q4 (🟡 Medium):** Compare bubble, selection, insertion on nearly-sorted array. Which wins?
- **Follow-up:** How does "nearly-sorted" define performance difference?
- **Follow-up:** Can you design input worst-case for each?

**Q5 (🟡 Medium):** Why is selection sort always O(n²)?
- **Follow-up:** Even if array is already sorted?
- **Follow-up:** Compare to bubble sort on already sorted?

**Q6 (🔴 Hard):** Implement insertion sort. What's the trade-off between shifting vs swapping?
- **Follow-up:** Which is faster in practice?
- **Follow-up:** When is it used in production code?

**Q7 (🔴 Hard):** Adaptive sorting. Modify insertion sort to detect already-sorted sections.
- **Follow-up:** What's the resulting complexity on nearly-sorted input?
- **Follow-up:** Is this used in practice?

**Q8 (🔴 Hard):** Given constraints "minimize writes, allow reads," choose sort and justify.
- **Follow-up:** Why selection over insertion?
- **Follow-up:** Real-world scenario?

---

## 🎯 DAY 2: ADVANCED SORTING (MERGE & QUICK) — 9 Questions

**Q1 (🟡 Medium):** Write merge sort. Prove O(n log n) via recurrence.
- **Follow-up:** Draw recursion tree showing work at each level
- **Follow-up:** Why is merge O(n+m)?

**Q2 (🟡 Medium):** Implement merge function for two sorted arrays. O(1) space possible?
- **Follow-up:** What if in-place merge required?
- **Follow-up:** Stability implications?

**Q3 (🟡 Medium):** Merge sort vs quick sort. Which is stable?
- **Follow-up:** When is stability required?
- **Follow-up:** Trade-offs for your use case?

**Q4 (🟡 Medium):** Quick sort pivot strategies. First element: best, worst, or average?
- **Follow-up:** Random pivot: always O(n log n)?
- **Follow-up:** Median-of-three improvement?

**Q5 (🔴 Hard):** Implement quick sort. Design worst-case input (unsorted pivot).
- **Follow-up:** How does randomized pivot help?
- **Follow-up:** Introsort switches to heap sort why?

**Q6 (🔴 Hard):** Compare merge sort and quick sort on different data:
- Worst-case time complexity
- Space complexity
- Cache behavior
- Constants and hidden factors

**Q7 (🔴 Hard):** 3-way partitioning in quick sort. When does it help?
- **Follow-up:** Many duplicates scenario?
- **Follow-up:** Complexity with duplicates?

**Q8 (🔴 Hard):** Introsort algorithm. Why switch from quick to heap?
- **Follow-up:** When does depth exceed 2·log(n)?
- **Follow-up:** Why insertion sort for small subarrays?

**Q9 (🔴 Hard):** Timsort algorithm used in Python. Why?
- **Follow-up:** How does it exploit nearly-sorted runs?
- **Follow-up:** O(n) on already-sorted?

---

## 🎯 DAY 3: HEAPS & PRIORITY QUEUES — 10 Questions

**Q1 (🟢 Easy):** Heap property: what does min-heap guarantee?
- **Follow-up:** Is min-heap a sorted array?
- **Follow-up:** Where is minimum element?

**Q2 (🟢 Easy):** Array indices for heap. Parent, left, right formulas (0-indexed)?
- **Follow-up:** Off-by-one errors common?
- **Follow-up:** Why these specific formulas?

**Q3 (🟡 Medium):** Build min-heap from array. O(n log n) vs O(n) approach?
- **Follow-up:** Why is bottom-up O(n)?
- **Follow-up:** Prove geometric series sum?

**Q4 (🟡 Medium):** Implement bubble-up (sift-up). When does it stop?
- **Follow-up:** Bubble-down (sift-down) implementation?
- **Follow-up:** Comparisons at each step?

**Q5 (🟡 Medium):** Heap sort. Two phases: build and extract. Total complexity?
- **Follow-up:** Space complexity and in-place variant?
- **Follow-up:** Stability: yes or no?

**Q6 (🟡 Medium):** Priority queue applications. Three examples?
- **Follow-up:** Dijkstra's algorithm: how does heap help?
- **Follow-up:** Event simulation: explain?

**Q7 (🔴 Hard):** Heap-based median finding. How to maintain running median?
- **Follow-up:** Use two heaps (max-heap and min-heap)?
- **Follow-up:** Complexity of insert and find-median?

**Q8 (🔴 Hard):** Merge K sorted arrays using heap. Approach and complexity?
- **Follow-up:** Naive approach (merge pairwise)?
- **Follow-up:** Heap-based: how many elements in heap?

**Q9 (🔴 Hard):** Given stream of numbers, find K largest. Optimal approach?
- **Follow-up:** Min-heap of size K or max-heap?
- **Follow-up:** Why K-size heap better than all elements?

**Q10 (🔴 Hard):** Heap sort vs quick sort in practice. Why rarely use heap sort?
- **Follow-up:** Cache locality implications?
- **Follow-up:** Constants and overhead?

---

## 🎯 DAY 4: HASH TABLES I (SEPARATE CHAINING) — 9 Questions

**Q1 (🟢 Easy):** Hash function purpose. What's it mapping?
- **Follow-up:** Properties of good hash function?
- **Follow-up:** Example hash functions?

**Q2 (🟡 Medium):** Separate chaining collision resolution. Mechanism?
- **Follow-up:** Insert, search, delete complexity?
- **Follow-up:** Load factor α definition?

**Q3 (🟡 Medium):** Load factor and performance. α = n/m. When to resize?
- **Follow-up:** Typical threshold value?
- **Follow-up:** Cost of resizing?

**Q4 (🟡 Medium):** Hash function h(x) = x mod m. Prime vs non-prime m?
- **Follow-up:** Bad input if m = 10?
- **Follow-up:** Why prime m helps?

**Q5 (🟡 Medium):** Implement hash table with separate chaining. Key methods?
- **Follow-up:** How do you handle collisions?
- **Follow-up:** Rehashing when resize?

**Q6 (🔴 Hard):** Hash function design for strings. Sum of characters OK?
- **Follow-up:** Why anagrams collide?
- **Follow-up:** Better hash for strings?

**Q7 (🔴 Hard):** Resize operation. Rehash all elements. O(n)?
- **Follow-up:** Amortized complexity of insert?
- **Follow-up:** Doubling m vs linear growth?

**Q8 (🔴 Hard):** Adversarial input. Craft keys that all hash to same bucket.
- **Follow-up:** Performance impact?
- **Follow-up:** How to prevent (hash flooding attack)?

**Q9 (🔴 Hard):** Load factor α = 2. What's expected chain length?
- **Follow-up:** Expected search time?
- **Follow-up:** Resize threshold implications?

---

## 🎯 DAY 5: HASH TABLES II & ADVANCED HASHING — 10 Questions

**Q1 (🟡 Medium):** Open addressing vs separate chaining. Trade-offs?
- **Follow-up:** When use open addressing?
- **Follow-up:** Space efficiency implications?

**Q2 (🟡 Medium):** Linear probing collision resolution. Primary clustering?
- **Follow-up:** Why is clustering bad?
- **Follow-up:** Expected probe sequence length?

**Q3 (🟡 Medium):** Quadratic probing. Improvement over linear?
- **Follow-up:** Secondary clustering still exist?
- **Follow-up:** Explore all m slots?

**Q4 (🟡 Medium):** Double hashing. Two hash functions h₁ and h₂?
- **Follow-up:** Why h₂(key) must be coprime with m?
- **Follow-up:** Better distribution than linear/quadratic?

**Q5 (🟡 Medium):** Rolling hash formula. H(S) = S[0]·B^(m-1) + S[1]·B^(m-2) + ...?
- **Follow-up:** Why called "rolling"?
- **Follow-up:** Update in O(1)?

**Q6 (🔴 Hard):** Rabin-Karp pattern matching. Find all occurrences of P in T.
- **Follow-up:** Complexity vs naive approach?
- **Follow-up:** Hash collision implications?

**Q7 (🔴 Hard):** Rolling hash update. H(T[i+1..i+m]) from H(T[i..i+m-1])?
- **Follow-up:** Precomputation needed?
- **Follow-up:** O(1) update formula?

**Q8 (🔴 Hard):** Hash flooding security. Randomized seed defense?
- **Follow-up:** Universal hashing concept?
- **Follow-up:** When might DOS attack matter?

**Q9 (🔴 Hard):** Multi-pattern matching with Aho-Corasick. String matching for multiple patterns?
- **Follow-up:** Complexity vs Rabin-Karp for each pattern?
- **Follow-up:** Real-world use case?

**Q10 (🔴 Hard):** Cuckoo hashing. Alternative to chaining/probing?
- **Follow-up:** How does "eviction" work?
- **Follow-up:** Worst-case behavior?

---

## 🧠 Cross-Topic Integration Questions: 6 Questions

**Q1 (🟡 Medium):** Choose algorithm: sort 10 million integers with limited memory.
- **Follow-up:** Merge sort (needs O(n) extra) vs heap sort (O(1))?
- **Follow-up:** Quick sort stability requirements?

**Q2 (🟡 Medium):** Find K most frequent words in document (100MB).
- **Follow-up:** Sorting vs hash table?
- **Follow-up:** Heap for top K?

**Q3 (🟡 Medium):** Check if array has duplicates. Hash table vs sort?
- **Follow-up:** Trade-offs in time and space?
- **Follow-up:** Which approach for streaming data?

**Q4 (🔴 Hard):** Merge K sorted arrays. Heap vs simple approach?
- **Follow-up:** Complexity analysis?
- **Follow-up:** Space tradeoff?

**Q5 (🔴 Hard):** Range sum queries on static array. Sort + preprocessing?
- **Follow-up:** Segment tree vs sorted + binary search?
- **Follow-up:** When does sorting help?

**Q6 (🔴 Hard):** Design LRU cache. Hash table + doubly linked list. Why both?
- **Follow-up:** O(1) lookup via hash table?
- **Follow-up:** O(1) eviction via linked list ordering?

---

**Total Questions:** 40+ (8-10 per major topic)  
**Interview Prep Time:** 3-4 hours daily during interview week


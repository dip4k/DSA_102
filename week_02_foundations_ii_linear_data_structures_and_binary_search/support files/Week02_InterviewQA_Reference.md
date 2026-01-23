# 🎙️ Week 02 Interview Q&A Reference: 45+ Questions by Topic

**Status:** Interview Coaching Resource  
**Format:** Questions only + follow-ups  
**How to Use:** 4-5 questions daily, attempt before looking up answers

---

## 🎯 DAY 1-2: ARRAYS (STATIC & DYNAMIC) — 10 Questions

**Q1 (🟢 Easy):** Array index-to-address formula? Row-major vs column-major?
- **Follow-up:** Why does row-major matter in practice?
- **Follow-up:** Cache line implications?

**Q2 (🟡 Medium):** Amortized analysis of dynamic array doubling. O(1) per insert?
- **Follow-up:** Prove geometric series sum = 2n
- **Follow-up:** What if triple capacity instead of double?

**Q3 (🟡 Medium):** Implement dynamic array with doubling. Code the grow logic.
- **Follow-up:** Handle pointer invalidation how?
- **Follow-up:** Reserve capacity optimization?

**Q4 (🟡 Medium):** Array vs linked list. When use each?
- **Follow-up:** Frequency of operations matters?
- **Follow-up:** Cache locality example?

**Q5 (🔴 Hard):** Insert in middle of array. O(n) time. Alternatives?
- **Follow-up:** Can you do better with linked list?
- **Follow-up:** Trade-offs?

**Q6 (🟡 Medium):** 2D array layout. Memory calculation?
- **Follow-up:** Row-major address formula?
- **Follow-up:** Column-major address formula?

**Q7 (🔴 Hard):** Design resizable array supporting O(1) append and remove-middle.
- **Follow-up:** What additional structure helps?
- **Follow-up:** Trade-offs in space?

**Q8 (🟡 Medium):** Shrink-to-fit. Why useful? When risky?
- **Follow-up:** Cost of shrinking?
- **Follow-up:** Memory-critical systems?

**Q9 (🔴 Hard):** Compare shrinking strategies: eager (every time) vs lazy (only if α < 0.25).
- **Follow-up:** Thrashing prevention?
- **Follow-up:** Performance implications?

**Q10 (🔴 Hard):** Allocator fragmentation. How does doubling help/hurt?
- **Follow-up:** Free pool management?
- **Follow-up:** Real-world implications?

---

## 🎯 DAY 3: LINKED LISTS — 9 Questions

**Q1 (🟢 Easy):** Node structure. Singly vs doubly.
- **Follow-up:** When use each?
- **Follow-up:** Pointer overhead calculation?

**Q2 (🟡 Medium):** Reverse linked list. In-place algorithm?
- **Follow-up:** Iterative or recursive?
- **Follow-up:** Time and space complexity?

**Q3 (🟡 Medium):** Find middle of linked list. No length known.
- **Follow-up:** Slow and fast pointers?
- **Follow-up:** Odd vs even length?

**Q4 (🟡 Medium):** Detect cycle. Floyd's algorithm?
- **Follow-up:** Why two pointers at different speeds?
- **Follow-up:** Find cycle start?

**Q5 (🔴 Hard):** Merge two sorted linked lists. In-place possible?
- **Follow-up:** Time and space?
- **Follow-up:** Comparison to array merge?

**Q6 (🟡 Medium):** Insert in sorted linked list. Where insert?
- **Follow-up:** O(n) search time?
- **Follow-up:** Why not array?

**Q7 (🔴 Hard):** Linked list palindrome check.
- **Follow-up:** Find middle, reverse second half, compare?
- **Follow-up:** Space: O(1) vs O(n)?

**Q8 (🔴 Hard):** Reorder list: L0→L1→...→Ln to L0→Ln→L1→Ln-1→...
- **Follow-up:** Approach: find middle, reverse, merge?
- **Follow-up:** Time and space?

**Q9 (🔴 Hard):** Delete node given only node pointer (not head).
- **Follow-up:** Can you delete last node?
- **Follow-up:** Trick: copy next value, delete next?

---

## 🎯 DAY 4: STACKS & QUEUES — 8 Questions

**Q1 (🟢 Easy):** Stack vs queue. LIFO vs FIFO.
- **Follow-up:** Real-world examples?
- **Follow-up:** Implementation differences?

**Q2 (🟡 Medium):** Implement stack with array. Push, pop, full?
- **Follow-up:** Fixed size vs dynamic?
- **Follow-up:** Two-stack in one array?

**Q3 (🟡 Medium):** Implement queue with circular buffer.
- **Follow-up:** Enqueue, dequeue, wraparound?
- **Follow-up:** Distinguish full vs empty (both have front==back)?

**Q4 (🟡 Medium):** Min stack. O(1) get-min.
- **Follow-up:** Use auxiliary stack?
- **Follow-up:** Space and time?

**Q5 (🔴 Hard):** Stack using two queues. Pop from stack?
- **Follow-up:** Which queue is stack?
- **Follow-up:** Reverse one queue into other?

**Q6 (🟡 Medium):** Valid parentheses with stack.
- **Follow-up:** Algorithm?
- **Follow-up:** Time and space?

**Q7 (🔴 Hard):** Largest rectangle in histogram using stack.
- **Follow-up:** Monotonic stack insight?
- **Follow-up:** Time complexity?

**Q8 (🔴 Hard):** Implement deque with circular array.
- **Follow-up:** Add-front, add-back, remove-front, remove-back?
- **Follow-up:** Wraparound logic?

---

## 🎯 DAY 5: BINARY SEARCH — 12 Questions

**Q1 (🟢 Easy):** Binary search on sorted array. O(log n)?
- **Follow-up:** Invariant [low, high)?
- **Follow-up:** Mid calculation avoid overflow?

**Q2 (🟡 Medium):** Binary search first occurrence (leftmost).
- **Follow-up:** Modification to classic algorithm?
- **Follow-up:** When high = mid vs mid+1?

**Q3 (🟡 Medium):** Binary search last occurrence (rightmost).
- **Follow-up:** Modify where?
- **Follow-up:** Compare to first occurrence?

**Q4 (🟡 Medium):** Lower bound binary search. First ≥ target?
- **Follow-up:** Usage: sorted insert position?
- **Follow-up:** Boundary cases?

**Q5 (🟡 Medium):** Upper bound. First > target?
- **Follow-up:** Difference from lower bound?
- **Follow-up:** Range query [first, last]?

**Q6 (🔴 Hard):** Binary search in rotated sorted array. Peak?
- **Follow-up:** Detect rotation?
- **Follow-up:** Recurse on unsorted half?

**Q7 (🔴 Hard):** Capacity to ship packages. Binary search answer space?
- **Follow-up:** Feasibility check?
- **Follow-up:** Complexity analysis?

**Q8 (🔴 Hard):** Minimize maximum load across servers. Distribute tasks?
- **Follow-up:** Binary search capacity?
- **Follow-up:** Greedy feasibility check?

**Q9 (🔴 Hard):** Aggressive cows. Maximize minimum distance between k cows in m stalls?
- **Follow-up:** Binary search distance?
- **Follow-up:** Can-place(distance) function?

**Q10 (🔴 Hard):** Median of two sorted arrays. O(log(n+m))?
- **Follow-up:** Binary search on length?
- **Follow-up:** Partition logic?

**Q11 (🔴 Hard):** Find single element in array (all others appear twice)?
- **Follow-up:** Binary search on answer space?
- **Follow-up:** Properties to check?

**Q12 (🔴 Hard):** Peak element in array (not sorted). O(log n)?
- **Follow-up:** Monotone property?
- **Follow-up:** Compare neighbors for direction?

---

## 🧠 Cross-Topic Integration — 6 Questions

**Q1 (🟡 Medium):** LRU cache with O(1) get/put. Use data structure combo?
- **Follow-up:** Hash map + doubly linked list?
- **Follow-up:** Design details?

**Q2 (🟡 Medium):** Implement stack with singly linked list vs array. Trade-offs?
- **Follow-up:** Space overhead?
- **Follow-up:** Cache behavior?

**Q3 (🔴 Hard):** Merge k sorted linked lists efficiently.
- **Follow-up:** Naive pairwise vs priority queue?
- **Follow-up:** Complexity?

**Q4 (🔴 Hard):** Find k-th largest in stream (unbounded).
- **Follow-up:** Min-heap of size k?
- **Follow-up:** Dynamic vs fixed k?

**Q5 (🔴 Hard):** Sliding window maximum using deque.
- **Follow-up:** Monotonic deque?
- **Follow-up:** Complexity?

**Q6 (🔴 Hard):** Find closest value in BST-like structure using binary search.
- **Follow-up:** Applicability beyond BST?
- **Follow-up:** Answer space?

---

**Status:** Week 02 Interview Q&A Complete  
**Total:** 45 questions with 2-3 follow-ups each
# 🎯 WEEK 05 INTERVIEW QUESTIONS & FOLLOW-UPS

**Week:** 5 | **Tier:** Tier 1 Critical Patterns  
**Total Questions:** 36 (6 per day + follow-ups)  
**Format:** Questions without answers (for self-reflection)

---

## 📋 INTERVIEW QUESTION FRAMEWORK

**Depth Levels:**
- L1: Pattern understanding (conceptual)
- L2: Implementation details (tactical)
- L3: Trade-offs & variants (strategic)
- L4: Extensions & system design (architectural)

**Question Types:**
- Pattern recognition (identify from description)
- Implementation (code from scratch)
- Trade-off analysis (when/why/how)
- Variant solving (modify algorithm)
- System design (real-world application)

---

## 🎯 DAY 1: HASH PATTERNS (6 Questions)

### Q1: Hash Table Design (L1-L2)
**Question:** Explain how hash tables achieve O(1) average lookup. What happens during collisions? How do modern hash tables handle resizing?

**Follow-ups:**
- What's the time complexity of insertion if we need to resize?
- How would you design a hash function for strings?
- Why is load factor important?

**Topics to Cover:** Collision resolution (chaining vs. open addressing), resizing strategy, hash function properties

---

### Q2: Two-Sum Optimization (L2-L3)
**Question:** Given sorted array, solve two-sum in O(n) time and O(1) space. Compare to hash map approach. When is each better?

**Follow-ups:**
- Can you extend to three-sum? K-sum?
- How would you find all unique pairs (not just one)?
- What if array has duplicates? Negative numbers?

**Topics to Cover:** Trade-offs (space vs. simplicity), sorted structure exploitation, duplication handling

---

### Q3: Anagram Grouping (L2-L3)
**Question:** Group anagrams efficiently. Compare sorting approach vs. canonical form. What's faster in practice?

**Follow-ups:**
- Can you do this with a single pass?
- How would you handle Unicode characters?
- What if input is a stream (infinite)?

**Topics to Cover:** Canonical representations, sorting vs. hashing, stream processing

---

### Q4: Top-K with Constraints (L3-L4)
**Question:** Find top-K frequent elements. Why is bucket sort O(n) instead of O(n log k) with heap?

**Follow-ups:**
- What if k is huge (close to n)?
- How would you find top-K in a stream?
- Can you do it with O(1) space for k?

**Topics to Cover:** Bucket sort reasoning, streaming algorithms, space-time trade-offs

---

### Q5: Frequency Counter Application (L2-L3)
**Question:** Check if two strings are anagrams. Compare hash vs. array counting. Which is faster?

**Follow-ups:**
- What if strings have Unicode (size of alphabet unknown)?
- How would you find all anagrams of a word in a dictionary?
- Can you solve without extra space?

**Topics to Cover:** Alphabet size, array optimization, in-place solutions

---

### Q6: Hash Collision Analysis (L1-L3)
**Question:** What causes hash collisions? How do you measure collision rates? When does hash table degrade?

**Follow-ups:**
- What's the worst-case input for your hash function?
- How would you optimize for cache locality?
- Why not use a perfect hash?

**Topics to Cover:** Collision probability, adversarial inputs, hardware optimization

---

## 📈 DAY 2: MONOTONIC STACK (6 Questions)

### Q1: Stack Invariant Understanding (L1-L2)
**Question:** What does "monotonic invariant" mean? In a decreasing stack, what property must hold after each pop?

**Follow-ups:**
- Why do we maintain this invariant?
- Can you maintain increasing stack instead?
- What happens if we pop randomly?

**Topics to Cover:** Invariant definition, correctness guarantees, alternative approaches

---

### Q2: Amortized Analysis (L2-L3)
**Question:** Why is monotonic stack O(n) despite potential O(n) pops? Explain amortized analysis.

**Follow-ups:**
- Is it guaranteed O(n) in worst case?
- How would you prove amortized O(n)?
- Are there inputs that actually trigger many pops?

**Topics to Cover:** Amortized analysis technique, potential method, accounting method

---

### Q3: Next Greater vs. Previous Smaller (L2-L3)
**Question:** How do next greater and previous smaller differ algorithmically? Can you solve one and adapt to other?

**Follow-ups:**
- How would you find both in single pass?
- What about circular array variant?
- How does direction (left vs. right) matter?

**Topics to Cover:** Directional variants, bidirectional scanning, state reuse

---

### Q4: Trapping Rain Water (L3-L4)
**Question:** Why is stack-based approach O(n) whereas 2D array is O(n²)? What insight makes it work?

**Follow-ups:**
- Can you solve it with prefix/suffix max arrays?
- What's the memory overhead difference?
- How would you handle 2D rain water?

**Topics to Cover:** Problem structure insight, space optimization, 2D generalization

---

### Q5: Histogram Rectangle (L3-L4)
**Question:** Why does increasing stack find largest rectangle? How do index/width calculations work?

**Follow-ups:**
- What if all heights are same?
- How would you find rectangle with specific height constraint?
- Can you adapt to find height-constrained maximum area?

**Topics to Cover:** Width calculation logic, boundary conditions, variant handling

---

### Q6: Application to Streaming Data (L2-L3)
**Question:** How would you apply monotonic stack to streaming data (stock prices arriving online)?

**Follow-ups:**
- How does streaming differ from offline?
- Can you batch updates for efficiency?
- What's the latency vs. throughput trade-off?

**Topics to Cover:** Streaming algorithms, online vs. offline, real-time constraints

---

## 🔄 DAY 3: INTERVAL PATTERNS (6 Questions)

### Q1: Merge Invariant (L1-L2)
**Question:** In interval merging, why do we track "current_end = max(current_end, interval.end)"?

**Follow-ups:**
- What if we used min instead of max?
- Can you sort by end instead of start?
- What goes wrong if we don't update current_end?

**Topics to Cover:** Invariant correctness, sorting direction, merge logic

---

### Q2: Sort Direction Matters (L2-L3)
**Question:** Compare sorting by start vs. end time for interval scheduling. When is each better?

**Follow-ups:**
- What if intervals have weights?
- How would you solve weighted job scheduling?
- Why is end-time greedy optimal?

**Topics to Cover:** Greedy optimality, sorting impact, weighted variants

---

### Q3: Insert Interval Optimization (L2-L3)
**Question:** Why is insert interval O(n) while merge intervals is O(n log n)? What's the difference?

**Follow-ups:**
- Can you solve merge intervals in O(n) with pre-sorted input?
- How does three-phase strategy work?
- What if you need to insert k intervals?

**Topics to Cover:** Pre-sorted data exploitation, three-phase pattern, batch operations

---

### Q4: Overlapping Detection (L2-L3)
**Question:** When do two intervals overlap? How does overlap detection differ for open vs. closed intervals?

**Follow-ups:**
- How do you handle inclusive [a,b] vs. exclusive (a,b) intervals?
- What about mixed [a,b) half-open intervals?
- How does rounding affect interval detection?

**Topics to Cover:** Boundary conditions, interval types, floating-point precision

---

### Q5: Meeting Rooms Problem (L2-L3)
**Question:** How does finding minimum conference rooms differ from merging intervals? Why can't you just merge?

**Follow-ups:**
- Can you solve it with sweep line?
- What if rooms have different capabilities?
- How would you balance room assignment?

**Topics to Cover:** Sweep line algorithm, event processing, resource balancing

---

### Q6: Real-World Complexity (L3-L4)
**Question:** In Google Calendar, why can't you just sort and merge? What makes it complex in practice?

**Follow-ups:**
- How do timezones complicate scheduling?
- What about recurring events?
- How would you find optimal free slots?

**Topics to Cover:** Real-world constraints, calendar complexities, optimization under constraints

---

## 🎨 DAY 4A: PARTITION & CYCLIC SORT (6 Questions)

### Q1: Dutch National Flag Correctness (L1-L2)
**Question:** In Dutch National Flag, why don't we increment mid after swapping with right? Prove correctness.

**Follow-ups:**
- What goes wrong if we increment mid?
- Can you use different speeds (2 increments for right)?
- What if we swap left and right simultaneously?

**Topics to Cover:** Correctness proof, invariant maintenance, failure modes

---

### Q2: In-Place vs. Extra Space (L2-L3)
**Question:** Compare partitioning in-place vs. using extra arrays. When is O(1) space critical?

**Follow-ups:**
- What's the constant overhead of in-place?
- How much faster is extra-space approach?
- When would you use each?

**Topics to Cover:** Space-time trade-off, practical benchmarks, embedded systems

---

### Q3: Cyclic Sort Correctness (L2-L3)
**Question:** Why does cyclic sort work only for 1..n arrays? Can you extend to other ranges?

**Follow-ups:**
- What if array contains 0..n instead?
- How would you handle 1..n with duplicates?
- Can you solve for 5..20 range?

**Topics to Cover:** Preconditions for cyclic sort, generalization, handling duplicates

---

### Q4: Find Missing Optimal (L3-L4)
**Question:** Compare cyclic sort vs. math (n×(n+1)/2 - sum) for finding missing. When is each better?

**Follow-ups:**
- What about finding duplicates?
- How does overflow complicate math approach?
- What if multiple numbers are missing?

**Topics to Cover:** Algorithm selection, overflow handling, multiple missing

---

### Q5: Quicksort Partitioning (L2-L3)
**Question:** How does quicksort partitioning relate to Dutch National Flag? Why use 3-way partition?

**Follow-ups:**
- How much faster is 3-way for duplicate-heavy arrays?
- What's the worst-case difference?
- How does this affect sorting of objects with keys?

**Topics to Cover:** Quicksort variants, duplicate impact, real-world sorting

---

### Q6: Partition for Problem-Solving (L3-L4)
**Question:** When you see "segregate" or "rearrange", how do you recognize partitioning is needed?

**Follow-ups:**
- What makes a problem require partitioning?
- Can you convert non-partition problems to partitioning?
- How does order within partitions affect solution?

**Topics to Cover:** Pattern recognition, problem transformation, ordering constraints

---

## 📈 DAY 4B: KADANE'S ALGORITHM (6 Questions)

### Q1: DP Recurrence Definition (L1-L2)
**Question:** Define dp[i] for maximum subarray problem. Why "ending at i" and not "containing i"?

**Follow-ups:**
- What if you defined dp[i] as "containing i"?
- Can you solve both formulations?
- Which is more efficient?

**Topics to Cover:** DP state definition, impact on complexity, optimal substructure

---

### Q2: Extend vs. Restart Decision (L2-L3)
**Question:** In Kadane's, why do we always choose max(arr[i], arr[i] + prev)? Can this be suboptimal?

**Follow-ups:**
- When would restarting be better than extending?
- How do you prove this greedy choice is safe?
- What's the intuition?

**Topics to Cover:** Greedy correctness, optimal substructure, intuition building

---

### Q3: Max Product Complexity (L2-L3)
**Question:** Why is max product harder than max sum? Why do we need both min and max tracking?

**Follow-ups:**
- What happens if you only track max?
- Can you construct an example where min matters?
- How do you update min and max simultaneously?

**Topics to Cover:** Negative number handling, sign flipping, state complexity

---

### Q4: Circular Array Reduction (L3-L4)
**Question:** How does circular max reduce to total - min_subarray? Why does this work?

**Follow-ups:**
- What's the edge case with all-negative array?
- How do you handle that edge case?
- Can you extend this to k-circular arrays?

**Topics to Cover:** Problem reduction, edge cases, circular generalizations

---

### Q5: Index Reconstruction (L2-L3)
**Question:** How do you track indices in Kadane's? What extra state do you need?

**Follow-ups:**
- Does index tracking change complexity?
- How do you know where max starts and ends?
- Can you reconstruct without extra state?

**Topics to Cover:** Index tracking, state extension, reconstruction techniques

---

### Q6: 2D Kadane's (L3-L4)
**Question:** How would you extend Kadane's to 2D matrices? What's the algorithm and complexity?

**Follow-ups:**
- Can you reduce 2D to 1D?
- What's the time and space complexity?
- How would you find the rectangle itself?

**Topics to Cover:** Dimensionality expansion, column compression, 2D DP

---

## 🔁 DAY 5: FAST/SLOW POINTERS (6 Questions)

### Q1: Floyd's Cycle Detection (L1-L2)
**Question:** Prove that fast and slow pointers must meet in a cycle. What's the mathematical guarantee?

**Follow-ups:**
- What if fast moves 3 steps instead of 2?
- Can they miss each other in some cases?
- What's the maximum steps to meeting?

**Topics to Cover:** Cycle detection proof, speed ratios, meeting guarantee

---

### Q2: Cycle Start Finding (L2-L3)
**Question:** After finding cycle meeting point, why does resetting one pointer to head find cycle start?

**Follow-ups:**
- Can you prove this algebraically?
- Why do equal-speed pointers work after reset?
- What if you reset both pointers?

**Topics to Cover:** Cycle start theorem, distance relationships, proof techniques

---

### Q3: Space Optimization (L2-L3)
**Question:** Compare Floyd's O(1) space to hash set O(n) space. When is O(1) critical? What's the hidden cost?

**Follow-ups:**
- Is hash set approach faster in practice?
- When would you sacrifice space for speed?
- How do constants compare?

**Topics to Cover:** Space-time trade-offs, practical benchmarks, embedded systems

---

### Q4: Transformation Sequences (L2-L3)
**Question:** How do you detect cycles in transformation sequences (like happy numbers)? Does Floyd's apply?

**Follow-ups:**
- Can you optimize happy number checking?
- What other transformations have cycles?
- How do you prove cycle existence?

**Topics to Cover:** Transformation cycle detection, sequence analysis, cycle guarantees

---

### Q5: Palindrome Detection (L2-L3)
**Question:** To check palindrome in linked list, you find middle then reverse. Why reverse? Can you check without modifying?

**Follow-ups:**
- What's the complexity of reversal?
- Can you check with auxiliary space?
- How do you restore original list?

**Topics to Cover:** In-place modifications, list traversal, restoration techniques

---

### Q6: Multi-List Operations (L3-L4)
**Question:** How would you find intersection of two linked lists? Why does pointer swapping technique work?

**Follow-ups:**
- Can lists have different lengths?
- What if they don't intersect?
- How does technique handle cycles?

**Topics to Cover:** Two-pointer synchronization, list traversal, cycle handling

---

## 🏆 META-QUESTIONS (Across All Patterns)

### Pattern Recognition (L3-L4)
1. Given random problem, identify which 1-2 patterns apply
2. Explain why those patterns fit
3. Propose algorithm using those patterns
4. Discuss trade-offs vs. other approaches

### Trade-off Analysis (L3-L4)
1. When would you use hash over hash set?
2. When is stack approach better than DP?
3. How does interval sorting direction affect solution?
4. When is partition optimal vs. sorting?

### Real-World Reasoning (L3-L4)
1. How would you explain each pattern to non-technical colleague?
2. What's a real system using this pattern?
3. What constraints complicate the problem in production?
4. How would you monitor/debug at scale?

---

**Status:** Interview-Ready Questions | **Time:** 3-5 hours of discussion  
**Generated:** January 08, 2026 | **System:** v12


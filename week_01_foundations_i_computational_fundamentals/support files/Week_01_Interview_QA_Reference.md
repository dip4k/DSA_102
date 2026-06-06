# 🎙️ Week 01 Interview Q&A Reference: 50+ Questions by Topic

**Format:** Questions only + follow-ups to force deeper thinking  
**How to Use:** 5-6 questions daily, attempt before looking up answers

---

## 🎯 DAY 1: MEMORY MODEL — 9 Questions

**Q1 (🟢 Easy):** What are the four regions of process address space?
- **Follow-up:** Where are local variables stored?
- **Follow-up:** Where are malloc'd objects stored?

**Q2 (🟡 Medium):** Pointer is just an address. Explain dereferencing.
- **Follow-up:** What happens dereferencing null pointer?
- **Follow-up:** What is dangling pointer?

**Q3 (🟡 Medium):** Explain virtual memory and page tables.
- **Follow-up:** What is TLB?
- **Follow-up:** Why is page fault expensive?

**Q4 (🟡 Medium):** Contiguous memory vs scattered memory. Which is faster for iteration?
- **Follow-up:** Why? Cache behavior?
- **Follow-up:** Real-world implication?

**Q5 (🔴 Hard):** Memory hierarchy from registers to disk. Latencies?
- **Follow-up:** Why cache lines? What's a cache line?
- **Follow-up:** Spatial vs temporal locality?

**Q6 (🟡 Medium):** False sharing. What is it? When is it bad?
- **Follow-up:** Two threads, different variables, same cache line?
- **Follow-up:** How to prevent?

**Q7 (🔴 Hard):** Explain alignment and padding in structs.
- **Follow-up:** Why do compilers align data?
- **Follow-up:** Can you optimize struct layout?

**Q8 (🟡 Medium):** Stack frame: What lives in it?
- **Follow-up:** When frame pushed and popped?
- **Follow-up:** Stack overflow when?

**Q9 (🔴 Hard):** Virtual addressing translation. How does OS map virtual → physical?
- **Follow-up:** Cost of page fault?
- **Follow-up:** Why TLB cache?

---

## 🎯 DAY 2: BIG-O NOTATION — 10 Questions

**Q1 (🟢 Easy):** Big-O, Big-Ω, Big-Θ. Difference?
- **Follow-up:** Upper, lower, tight bounds?
- **Follow-up:** When is Big-Θ used?

**Q2 (🟡 Medium):** List complexity classes ordered by growth.
- **Follow-up:** For n=10^6, which are practical?
- **Follow-up:** Break-even points?

**Q3 (🟡 Medium):** Binary search is O(log n). Prove via recurrence.
- **Follow-up:** T(n) = T(n/2) + O(1) → O(log n)?
- **Follow-up:** Draw recurrence tree?

**Q4 (🟡 Medium):** Merge sort is O(n log n). Prove.
- **Follow-up:** T(n) = 2T(n/2) + O(n)?
- **Follow-up:** Recurrence tree?

**Q5 (🔴 Hard):** Naive Fibonacci O(2^n). Why exponential?
- **Follow-up:** Visualize call tree?
- **Follow-up:** How many times is fib(k) computed?

**Q6 (🟡 Medium):** Analyze algorithm: linear scan vs binary search.
- **Follow-up:** When O(n) faster despite larger Big-O?
- **Follow-up:** Constants matter?

**Q7 (🔴 Hard):** Compare O(n²) and O(n log n) for practical inputs.
- **Follow-up:** n=1000: which faster?
- **Follow-up:** n=10^6: which faster?

**Q8 (🟡 Medium):** Recurrence tree method. How to analyze arbitrary recursion?
- **Follow-up:** Example: T(n) = T(n/3) + T(2n/3) + O(n)?
- **Follow-up:** General form?

**Q9 (🔴 Hard):** Is O(n) better than O(n log n)? Or does it depend?
- **Follow-up:** Constants hidden in Big-O?
- **Follow-up:** Practical performance?

**Q10 (🔴 Hard):** Can an O(n²) algorithm beat O(n log n) algorithm?
- **Follow-up:** Small constants, cache behavior?
- **Follow-up:** When does Big-O fail to predict?

---

## 🎯 DAY 3: SPACE COMPLEXITY — 8 Questions

**Q1 (🟡 Medium):** Space complexity: types of space?
- **Follow-up:** Auxiliary space?
- **Follow-up:** Stack vs heap?

**Q2 (🟡 Medium):** Stack depth. Recursive function depth?
- **Follow-up:** Stack overflow limits?
- **Follow-up:** Deep recursion solutions?

**Q3 (🟡 Medium):** Activation record (stack frame). What's in it?
- **Follow-up:** Who pushes and pops?
- **Follow-up:** Cost of function call?

**Q4 (🔴 Hard):** Analyze space: "int arr[1000]" on stack vs malloc?
- **Follow-up:** Stack size limits?
- **Follow-up:** Practical implications?

**Q5 (🟡 Medium):** Time-space trade-off. Cache vs memory?
- **Follow-up:** Memoization example?
- **Follow-up:** When worth it?

**Q6 (🟡 Medium):** Object overhead in heap. Why vector<int> > 4n bytes?
- **Follow-up:** Metadata?
- **Follow-up:** Allocator overhead?

**Q7 (🔴 Hard):** Memory fragmentation. What is it? How to avoid?
- **Follow-up:** Allocator strategies?
- **Follow-up:** Real-world impact?

**Q8 (🔴 Hard):** Pointer size, object header size, cache line size on your system?
- **Follow-up:** How to measure?
- **Follow-up:** Implications?

---

## 🎯 DAY 4: RECURSION I — 10 Questions

**Q1 (🟢 Easy):** Call stack mechanics. Function call pushes what?
- **Follow-up:** Return pops what?
- **Follow-up:** Order of events?

**Q2 (🟡 Medium):** Base case. Why required?
- **Follow-up:** Missing base case consequence?
- **Follow-up:** Can recursion work without base case? (No)

**Q3 (🟡 Medium):** Simple recursion: factorial(5). Trace by hand.
- **Follow-up:** Draw stack frames?
- **Follow-up:** Identify base case and recursive case?

**Q4 (🟡 Medium):** Recursion tree. Visualize branching.
- **Follow-up:** Linear recursion tree shape?
- **Follow-up:** Tree recursion shape?

**Q5 (🔴 Hard):** Identify exponential blowup. Naive Fibonacci why O(2^n)?
- **Follow-up:** Many repeated subproblems?
- **Follow-up:** Draw call tree for fib(5)?

**Q6 (🟡 Medium):** Tail recursion. What is it? How to convert to loop?
- **Follow-up:** Example: factorial tail?
- **Follow-up:** Compiler optimization?

**Q7 (🔴 Hard):** Recursion depth limit. Practical limits?
- **Follow-up:** Stack size on your system?
- **Follow-up:** n=10^6 recursive calls overflow?

**Q8 (🟡 Medium):** Convert recursion to iteration. Using explicit stack?
- **Follow-up:** Pre-order traversal recursive vs iterative?
- **Follow-up:** Which clearer?

**Q9 (🔴 Hard):** Mutual recursion: A calls B calls A. Trace example.
- **Follow-up:** Even/odd recursive definition?
- **Follow-up:** Complexity analysis?

**Q10 (🔴 Hard):** Analyze stack usage. Recursive function space complexity?
- **Follow-up:** O(n) recursion depth → O(n) space?
- **Follow-up:** Including heap allocations?

---

## 🎯 DAY 5: RECURSION II & MEMOIZATION — 10 Questions

**Q1 (🟡 Medium):** Overlapping subproblems. What does it mean?
- **Follow-up:** Naive Fibonacci example?
- **Follow-up:** How to detect?

**Q2 (🟡 Medium):** Memoization. How does it work?
- **Follow-up:** Cache design: key, value?
- **Follow-up:** When to lookup, when to compute?

**Q3 (🟡 Medium):** Convert Fibonacci from O(2^n) to O(n) via memoization.
- **Follow-up:** Code it?
- **Follow-up:** Space cost?

**Q4 (🔴 Hard):** When is memoization useful?
- **Follow-up:** Tree traversal: overlapping subproblems?
- **Follow-up:** Not all recursion benefits?

**Q5 (🟡 Medium):** Recursion patterns: linear, tree, divide-conquer. Examples?
- **Follow-up:** Time complexity each?
- **Follow-up:** When use which?

**Q6 (🔴 Hard):** Divide-and-conquer recursion. Structure?
- **Follow-up:** Merge sort example?
- **Follow-up:** Recurrence and complexity?

**Q7 (🔴 Hard):** Given overlapping recursion, design memoization cache.
- **Follow-up:** HashMap with (n) key?
- **Follow-up:** Space-time trade-off?

**Q8 (🟡 Medium):** Stack depth vs memory for memoization. Trade-off?
- **Follow-up:** Deep recursion with memo?
- **Follow-up:** Convert to iteration?

**Q9 (🔴 Hard):** Analyze complex recursion: T(n) = 3T(n/4) + O(n²).
- **Follow-up:** Recurrence tree method?
- **Follow-up:** Total complexity?

**Q10 (🔴 Hard):** Recognize when memoization is optimal vs suboptimal.
- **Follow-up:** Pseudo-polynomial solutions?
- **Follow-up:** Space complexity?

---

## 🎯 DAY 6: PEAK FINDING — 8 Questions

**Q1 (🟡 Medium):** 1D peak definition. Example?
- **Follow-up:** Brute force solution?
- **Follow-up:** Time complexity O(n)?

**Q2 (🟡 Medium):** 1D peak divide-and-conquer. Algorithm?
- **Follow-up:** Why recurse on larger neighbor?
- **Follow-up:** Proof of correctness intuition?

**Q3 (🟡 Medium):** 1D peak complexity. Why O(log n)?
- **Follow-up:** Recurrence T(n) = T(n/2) + O(1)?
- **Follow-up:** Compare to O(n) scan?

**Q4 (🔴 Hard):** Code 1D peak finding in O(log n).
- **Follow-up:** Handle edge cases: array size 1, 2?
- **Follow-up:** All elements equal?

**Q5 (🟡 Medium):** 2D peak definition. Example?
- **Follow-up:** Extend to matrix?
- **Follow-up:** Brute force?

**Q6 (🟡 Medium):** 2D peak algorithm. Mid-column approach?
- **Follow-up:** Find column-local max?
- **Follow-up:** Check left and right neighbors?

**Q7 (🔴 Hard):** Code 2D peak finding.
- **Follow-up:** Complexity: O(n log m) or O(m log n)?
- **Follow-up:** Space O(1) or O(n)?

**Q8 (🔴 Hard):** Compare 1D and 2D peak approaches. Pattern?
- **Follow-up:** Divide-and-conquer structure?
- **Follow-up:** Exploit monotonicity?

---

## 🧠 Cross-Topic Integration — 5 Questions

**Q1 (🔴 Hard):** Given problem similar to peak finding. Recognize divide-conquer?
- **Follow-up:** Can you reformulate as peak problem?
- **Follow-up:** What's the structure?

**Q2 (🔴 Hard):** Memory + Recursion: Deep recursion on large objects?
- **Follow-up:** Stack overflow risk?
- **Follow-up:** Solution: heap allocation?

**Q3 (🔴 Hard):** Complexity + Memoization: Recursive problem with exponential complexity.
- **Follow-up:** Overlapping subproblems?
- **Follow-up:** Memoization feasible?

**Q4 (🔴 Hard):** System design: Algorithm performance depends on memory layout?
- **Follow-up:** Cache behavior matters?
- **Follow-up:** Virtual memory concern?

**Q5 (🔴 Hard):** Synthesize: Given algorithm, analyze memory, complexity, recursion depth, and memoization.
- **Follow-up:** Multiple considerations?
- **Follow-up:** Trade-offs?

---

**Total Questions:** 50 (8-10 per topic)  
**Interview Prep Time:** 4-5 hours for all questions

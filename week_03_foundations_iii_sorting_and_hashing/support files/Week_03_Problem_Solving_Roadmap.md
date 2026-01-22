# 📋 Week_03_Problem_Solving_Roadmap — Sorting, Heaps & Hashing

**Version:** 12.0 FINAL  
**Week:** 3 – Foundations III: Sorting, Heaps & Hashing  
**Purpose:** Transform Week 3 sorting, heaps, and hashing knowledge into problem-solving fluency  
**Target Audience:** Learners progressing from Week 2 linear structures to Week 3 advanced data structures  
**File Type:** Support File (Problem-Solving Playbook)  
**Tone:** Training coach—strategic, practical, actionable  
**Word Count Target:** 3,500-4,500 words

---

## 🗺️ WEEK 3 PROBLEM-SOLVING OVERVIEW

Welcome to Week 3—the week you shift from understanding *individual* data structures to mastering the algorithms that *power* systems at scale. This week is about three foundational primitives:

1. **Sorting Algorithms** — How to organize data efficiently
2. **Heaps** — How to maintain order with tree structures
3. **Hash Tables & Hashing** — How to achieve O(1) lookups in practice

These aren't just interview topics. Every database, every search engine, every recommendation system relies on these primitives working flawlessly. By the end of this week, you'll understand not just *how* they work, but *why* they matter in real systems.

### The Learning Arc This Week

**Day 1: Elementary Sorts**  
Start with the simplest algorithms (bubble, selection, insertion). Understand why they're O(n²) and when they matter despite their simplicity. Build mental models for how sorting works.

**Day 2: Advanced Sorts**  
Learn divide-and-conquer with merge sort and quicksort. Understand the leap from O(n²) to O(n log n) and why this matters at scale. See how real systems choose between them.

**Day 3: Heaps**  
Discover the elegant tree structure that maintains order implicitly through a single invariant. Learn to build, maintain, and extract from heaps. Connect to priority queues in real systems.

**Day 4: Hash Tables I**  
Understand how hash functions and collision resolution via separate chaining give us O(1) average-case operations. See where hash tables appear in Python, Java, C#.

**Day 5: Hash Tables II & Karp-Rabin**  
Dive into open addressing strategies, universal hashing (guarding against adversarial inputs), and the elegant rolling hash algorithm that powers substring search and plagiarism detection.

---

## 📊 DAY-BY-DAY CONCEPT ROADMAP

### Day 1: Elementary Sorts — The Foundation

| Concept | What You'll Learn | Key Mental Model | Time Allocation |
|---------|------------------|------------------|-----------------|
| **Bubble Sort** | Iterative adjacent-element swaps | "The largest element bubbles to the end each pass" | 25 min study + 20 min trace |
| **Selection Sort** | Finding minimum and placing it | "Select the smallest, place it, repeat" | 20 min study + 20 min trace |
| **Insertion Sort** | Growing a sorted prefix | "Insert each element into its correct position in sorted prefix" | 25 min study + 20 min trace |
| **Stability & In-Place** | Why these properties matter | "Stability preserves relative order; in-place uses O(1) extra memory" | 20 min study |
| **Trade-offs & Use Cases** | When O(n²) is acceptable | "Small n, nearly sorted data, or as hybrid algorithm component" | 30 min analysis |

**Daily Goal:** You can trace through all three algorithms by hand without looking at code. You can explain why selection sort is stable sometimes and why insertion sort shines on nearly sorted data.

---

### Day 2: Merge Sort & Quick Sort — The Leap to O(n log n)

| Concept | What You'll Learn | Key Mental Model | Time Allocation |
|---------|------------------|------------------|-----------------|
| **Divide & Conquer Pattern** | Splitting problems recursively | "Divide into halves → solve recursively → combine results" | 20 min study |
| **Merge Sort Analysis** | Guaranteed O(n log n) via recurrence | "T(n) = 2T(n/2) + O(n) solves to O(n log n)" | 30 min study + 25 min trace |
| **Quick Sort Analysis** | O(n log n) expected, O(n²) worst-case | "Pivot partitioning is O(n), done O(log n) times on average" | 30 min study + 25 min trace |
| **Pivot Strategies** | How to choose good pivots | "Random pivot avoids adversarial input; median-of-three is practical" | 20 min study |
| **Real Implementations** | Introsort, Timsort hybrid strategy | "Modern sorts switch to heapsort if quicksort degrades; insertion for small subarrays" | 25 min analysis |

**Daily Goal:** Understand the recurrence relation for merge sort. Trace quicksort partition by hand. Explain why pivot choice matters for average vs. worst case.

---

### Day 3: Heaps, Heapify & Heap Sort

| Concept | What You'll Learn | Key Mental Model | Time Allocation |
|---------|------------------|------------------|-----------------|
| **Binary Heap Model** | Array-based complete tree | "Index i's children are at 2i+1 and 2i+2; heap property: parent ≥ children (max-heap)" | 25 min study + 20 min visualize |
| **Bubble Up (Percolate Up)** | Restore heap after insertion | "New element compares with parent; swap if greater; repeat up tree" | 20 min study + 20 min trace |
| **Heapify Down (Sift Down)** | Restore heap after root removal | "Compare with children; swap with larger; repeat down tree" | 20 min study + 20 min trace |
| **Build-Heap (Linear Time)** | Construct heap in O(n) | "Apply heapify-down to all non-leaf nodes, bottom-up" | 25 min study + 25 min trace |
| **Heap Sort** | Sort via repeated extract-max | "Build heap, extract max n times; O(n log n), in-place but not stable" | 20 min study + 20 min trace |
| **Priority Queues** | Use-case abstraction | "Insert, extract-max/min, update-priority all in O(log n); powers Dijkstra, task schedulers" | 25 min analysis |

**Daily Goal:** Hand-build a min-heap and max-heap from unsorted data. Trace insertion and extraction by hand. Understand why heapify-down on half the elements builds a heap in O(n).

---

### Day 4: Hash Tables I — Separate Chaining

| Concept | What You'll Learn | Key Mental Model | Time Allocation |
|---------|------------------|------------------|-----------------|
| **Hash Functions** | Mapping keys to bucket indices | "Deterministic function: key → index in [0, table_size-1]; aim for uniform distribution" | 25 min study |
| **Separate Chaining** | Collision resolution via linked lists | "Each bucket contains a chain; collisions extend the chain; O(1+load_factor) average lookup" | 25 min study + 20 min visualize |
| **Load Factor** | Density of table (n / table_size) | "Low load factor → fewer collisions; typically maintain α ≈ 0.75; resize at α > 0.9" | 20 min study |
| **Resizing Strategy** | Doubling bucket count | "When load factor exceeds threshold, double size and rehash all entries; O(n) amortized per insert" | 20 min study + 20 min trace |
| **Worst-Case Behavior** | When hash tables fail | "All keys hash to same bucket → O(n) operations; adversarial inputs exploiting hash weakness" | 20 min analysis |
| **String Hashing** | Hashing variable-length data | "Polynomial rolling hash: H = c₀ + c₁*b + c₂*b² + ... (mod prime)" | 25 min study |

**Daily Goal:** Hand-calculate hash indices for keys. Trace insert/lookup/delete with separate chaining. Understand when resizing triggers and why doubling is better than adding fixed count.

---

### Day 5: Hash Tables II — Open Addressing & Rolling Hash (Karp-Rabin)

| Concept | What You'll Learn | Key Mental Model | Time Allocation |
|---------|------------------|------------------|-----------------|
| **Linear Probing** | Find next empty slot sequentially | "If h(key) is occupied, try h(key)+1, h(key)+2, ... (wrap around) → suffers primary clustering" | 20 min study + 20 min trace |
| **Quadratic Probing** | Reduce clustering via quadratic gaps | "Try h(key), h(key)+1, h(key)+4, h(key)+9, ... to spread probe sequence" | 20 min study + 20 min trace |
| **Double Hashing** | Two independent hash functions | "Use h₁(key) as initial index, h₂(key) as step size → fewer clusters than quadratic probing" | 20 min study |
| **Load Factor Limits** | Critical thresholds for open addressing | "α < 0.5 for linear probing; α < 0.75 for quadratic/double hashing; resize when exceeded" | 15 min study |
| **Hash Flooding & Security** | Adversarial collision attacks | "Attacker crafts inputs to hash to same bucket → denial of service; mitigate with randomized seeds" | 20 min analysis |
| **Universal Hashing** | Collision probability guarantee | "Family of hash functions: for any two distinct keys, Pr[collision] ≤ 1/m; randomized selection" | 25 min study |
| **Rolling Hash (Karp-Rabin)** | O(1) window update for substring search | "H = c₀*b^(k-1) + c₁*b^(k-2) + ... + c_(k-1); remove c₀*b^(k-1), add new char, O(1) per slide" | 30 min study + 25 min trace |
| **Karp-Rabin Applications** | Substring search, plagiarism, DNA matching | "Find all occurrences in O(n+m) average; verify on hash match to avoid false positives" | 20 min analysis |

**Daily Goal:** Trace open addressing with linear, quadratic, and double hashing. Understand rolling hash update arithmetic. Apply rolling hash to find pattern occurrences in text.

---

## 🎯 THREE-STAGE PROBLEM-SOLVING PROGRESSION

### Stage 1: Foundational Understanding (Easy)

**Goal:** Implement and trace basic algorithms by hand.

#### Sorting (Complete all 3)
1. **Bubble Sort** — Trace 5 elements by hand; identify best/worst cases (nearly sorted vs. reverse sorted)
2. **Selection Sort** — Trace 5 elements; explain why it's always O(n²) despite input
3. **Insertion Sort** — Trace 5 elements; explain why it's faster on nearly sorted data

#### Heaps (Complete all 3)
4. **Build Min-Heap** — Given array [7, 3, 9, 1, 5], build heap by hand; verify heap property
5. **Insert into Heap** — Start with existing heap, insert 2; trace bubble-up; verify O(log n)
6. **Extract Min** — Remove min from heap; trace heapify-down; verify correctness

#### Hashing (Complete all 3)
7. **Hash Function** — Given keys "apple", "banana", "cherry" and table size 5, compute h(key) = (sum of char codes) % 5; identify collisions
8. **Separate Chaining Lookup** — Given hash table with chains, perform insert/lookup/delete by hand
9. **Rolling Hash** — Compute rolling hash for pattern "cat" in text "catalog"; trace sliding window update

**Success Criteria:** You can complete each without reference material in under 10 minutes. You can explain *why* each step occurs.

---

### Stage 2: Pattern Recognition & Variation (Medium)

**Goal:** Recognize when to apply which algorithm; handle subtle variations.

#### Sorting Variations
1. **Merge Sort vs. Quick Sort** — Given constraint "stable sort needed" vs. "in-place preferred", choose appropriate algorithm and justify
2. **Hybrid Sorting** — Explain introsort: when does it switch from quicksort → heapsort? Why?
3. **Nearly Sorted Data** — Why does insertion sort beat quicksort on nearly sorted arrays? Can you quantify the improvement?

#### Heap Variants
4. **Min-Heap vs. Max-Heap** — Convert max-heap to min-heap; discuss when each is used (Dijkstra uses min-heap; scheduler might use max-heap for priority)
5. **d-ary Heaps** — Discuss trade-offs of d-ary heap vs. binary (fewer levels but more child comparisons); when is d=4 better than d=2?
6. **Priority Queue Implementation** — Implement priority queue API (insert, extract-max, update-key) using heap; discuss update-key complexity (O(n) with percolate or O(log n) with lazy deletion)

#### Hash Table Variations
7. **Collision Resolution Strategies** — Given load factor α=0.8, compare separate chaining vs. linear probing vs. double hashing; discuss memory, cache, and operation-cost tradeoffs
8. **Resizing Schedules** — When do we resize (threshold α)? Why double instead of adding fixed size? Amortized cost analysis
9. **Adversarial Hash Input** — Design input that causes hash flooding in simple modulo-based hash; explain universal hashing defense

#### Karp-Rabin Deep Dive
10. **Rolling Hash Verification** — Implement pattern search with rolling hash; verify on collision to avoid false positives; discuss probability of collision given prime modulus
11. **Multiple Pattern Search** — Extend rolling hash to search for multiple patterns simultaneously; discuss tradeoffs vs. Aho-Corasick

**Success Criteria:** You can solve each in 15-25 minutes. You can explain *why* one variation is better than another in specific contexts.

---

### Stage 3: Integration & Real-World Application (Hard)

**Goal:** Combine patterns; apply to production scenarios.

1. **Sorting in Databases** — Given a database with 1GB of data on disk and 256MB of RAM, design an external merge sort. How many passes? How do you minimize disk I/O?

2. **Heap in Task Scheduler** — Design a task scheduler using a heap: tasks have priority and deadline. Implement insert, extract-max-priority, update-priority (deadline change). Discuss real-world use in OS schedulers.

3. **Hash Table Load Balancing** — Design a load-balanced hash table for distributed systems. Each key should hash to a node in a cluster. When a node fails, minimal rebalancing. (Introduction to consistent hashing.)

4. **Karp-Rabin for Plagiarism** — Given two documents, use rolling hash to find matching substrings of length k (indicating plagiarism). Optimize for large documents. Discuss false-positive probability and hash size choice.

5. **Cache-Efficient Sorting** — Explain why merge sort has better cache locality than quicksort. Given CPU cache sizes (L1, L2, L3), predict which sort is faster for array size 100MB. How would you optimize?

6. **Cryptographic Hashing** — Discuss hash function properties needed for security (avalanche effect, preimage resistance). Compare cryptographic hash (SHA-256) with performance-optimized hash (MurmurHash). When is each appropriate?

**Success Criteria:** You can design and justify each solution in a technical interview. You can trade off competing concerns (memory, speed, simplicity). You can connect to production systems.

---

## 🔑 WEEK 3 CORE DECISION FRAMEWORK

### When to Use Which Sorting Algorithm?

| Input Characteristics | Best Choice | Why | Worst Choice |
|---|---|---|---|
| Small n (< 50 elements) | Insertion Sort | Low overhead, cache-friendly despite O(n²) | Merge sort (overkill, more allocations) |
| Nearly Sorted | Insertion Sort | Degenerates to O(n) on nearly sorted data | Quicksort (doesn't exploit existing order) |
| Need Stability | Merge Sort | Guaranteed stable; O(n log n) | Quicksort (unstable) |
| In-Place Preferred | Quicksort or Heapsort | O(1) or O(log n) space | Merge sort (requires O(n) extra space) |
| Worst-Case Guarantee | Merge Sort or Heapsort | Always O(n log n) | Quicksort (can degrade to O(n²)) |
| General Purpose | Quicksort or Hybrid (Introsort) | Fast in practice; can be made stable via modification | Bubble sort (educational only) |
| Large External Data | External Merge Sort | Minimizes disk I/O via multi-way merge | In-memory sort (exceeds RAM) |

**Key Decision:** Ask three questions: (1) Must it be stable? (2) In-place required? (3) Worst-case guarantee needed? Answers determine the choice.

---

### When to Use Which Hash Collision Resolution?

| Scenario | Best Choice | Why | Tradeoff |
|---|---|---|---|
| Low to Moderate Load (α < 0.7) | Separate Chaining | Simple, cache-inefficient chains are short | Pointer overhead |
| High Load (α > 0.8) | Open Addressing (Quadratic or Double Hash) | More cache-friendly; fills available space | Complex to implement; delete is awkward |
| Unknown Load Pattern | Separate Chaining | Degrades gracefully even at high load | Slightly lower average speed than probing |
| Adversarial Input Risk | Universal Hashing | Randomized selection prevents hash flooding | Slightly slower hash computation |
| Cryptographic Needs | Universal Hashing + Cryptographic Hash | Collision resistance for security | Much slower than performance hashing |

**Key Decision:** Separate chaining is the default for most use cases. Open addressing is an optimization when memory and cache matter more than simplicity.

---

## 🧠 MENTAL MODEL RETENTION HOOKS

**Sorting:** "Three O(n²) sorts (bubble, selection, insertion) are rarely used, except insertion sort on nearly sorted data. Merge sort guarantees O(n log n) but uses O(n) space. Quicksort is O(n log n) on average and O(1) in-place, but O(n²) in worst case. Modern sorts are hybrid: switch to insertion or heapsort to handle degenerate cases."

**Heaps:** "A binary heap is an array-based complete tree where every parent is greater (or less) than its children. Insert and extract are O(log n). Build is O(n). The heap property is the only invariant; it enables both sorting and priority queues."

**Hashing:** "Hash tables achieve O(1) average lookup via a hash function + collision resolution. Separate chaining uses linked lists per bucket; open addressing probes for empty slots. Both degrade under heavy load (high α) or poor hash functions. Universal hashing provides probabilistic guarantees against adversarial input."

**Rolling Hash:** "Polynomial hash enables O(1) window updates by factoring out the departing character and adding the arriving character. Used in substring search (Karp-Rabin), plagiarism detection, and DNA matching. Always verify on hash match to guard against collisions."

---

## 🚨 COMMON PITFALLS & HOW TO AVOID THEM

### Sorting Pitfalls

| Pitfall | Why It Happens | Prevention |
|---------|---|---|
| Choosing merge sort for small data | Recursion overhead dominates | Use insertion sort for n < 50; implement hybrid sorting |
| Implementing quicksort without handling pivot degeneracy | Fixed pivot (e.g., first element) fails on sorted input | Use random pivot, median-of-three, or switch to heapsort |
| Not recognizing nearly-sorted input opportunity | Tempted to use generic O(n log n) sort | Profile first; if nearly sorted, use insertion sort |
| Stability misconception | Assume all O(n log n) sorts are stable | Only merge sort among common sorts is stable; quicksort/heapsort are not |
| External sort overlooked | Force large dataset into RAM | Implement multi-pass merge sort for disk-resident data |

### Heap Pitfalls

| Pitfall | Why It Happens | Prevention |
|---------|---|---|
| Off-by-one errors in parent/child indices | Confusion between 0-based and 1-based indexing | Always use: parent = (i-1)/2, left = 2i+1, right = 2i+2 (0-based) |
| Forgetting to heapify after swap | Assume single swap restores heap property | Always bubble-up or heapify-down after insert/extract |
| Using array size vs. heap size | Treat entire array as heap without tracking size | Maintain separate heap size; never assume array.length |
| Not recognizing O(n) build opportunity | Build heap via n insertions (O(n log n)) instead of one pass (O(n)) | Use heapify-down on half the elements, bottom-up |

### Hashing Pitfalls

| Pitfall | Why It Happens | Prevention |
|---------|---|---|
| Poor hash function (e.g., just modulo) | Simple functions often collide on natural data patterns | Use polynomial rolling hash or multiply-and-modulo methods |
| Ignoring load factor | Only resize when table becomes full (load factor ~1.0) | Resize proactively when α > 0.75 (separate chaining) or α > 0.5 (open addressing) |
| Forgetting to rehash on resize | Resize bucket array without recomputing hash indices | Iterate all entries and re-insert with new hash function after resize |
| Open addressing with unsound delete | Deleting and leaving gap breaks probe chains | Mark as "tombstone" (deleted but present) instead of truly removing |
| Underestimating hash collisions | Assume good hash function eliminates collisions | Always implement collision resolution; test with adversarial input |

---

## 📋 WEEK 3 DAILY ACTIVITY GUIDE

### Day 1: Elementary Sorts
- **Morning (30 min):** Read mental models for bubble, selection, insertion sort. Visualize one pass of bubble sort.
- **Midday (40 min):** Trace all three sorts on [5, 2, 8, 1, 9] by hand. Note how many comparisons and swaps each makes.
- **Afternoon (30 min):** Implement all three sorts in pseudocode (no code, just English logic).
- **Evening (20 min):** Compare: on what input does insertion sort beat selection sort? On what input does bubble sort excel?

### Day 2: Merge Sort & Quick Sort
- **Morning (30 min):** Draw recurrence tree for merge sort. Convince yourself T(n) = O(n log n).
- **Midday (45 min):** Trace merge sort partition and merge steps on [5, 2, 8, 1, 9]. Verify sorted output.
- **Afternoon (45 min):** Trace quicksort with random pivot on same array. Compare number of comparisons to merge sort.
- **Evening (25 min):** Design input that causes quicksort worst-case (all pivots split 1 vs. n-1). Understand why this is rare.

### Day 3: Heaps
- **Morning (25 min):** Study binary heap array indexing. Verify parent/child formula for 5 elements.
- **Midday (40 min):** Build max-heap from [5, 2, 8, 1, 9] using bottom-up heapify. Trace each heapify-down.
- **Afternoon (40 min):** Insert 15, then 0, then extract max twice. Verify heap property after each operation.
- **Evening (20 min):** Manually sort [5, 2, 8, 1, 9] using heap sort. Compare heap sort vs. quick sort on this input.

### Day 4: Hash Tables (Separate Chaining)
- **Morning (25 min):** Design simple hash function. Hash 5 strings into table of size 3; identify collisions.
- **Midday (35 min):** Insert 10 items into hash table with separate chaining (table size 5). Trace each insert and collision resolution.
- **Afternoon (35 min):** Perform lookup/delete on several items. Trace chain traversal.
- **Evening (20 min):** Calculate load factor; determine when to resize. Simulate resize and rehashing.

### Day 5: Open Addressing & Rolling Hash
- **Morning (25 min):** Trace linear probing on same 10 items from Day 4. Observe primary clustering.
- **Midday (30 min):** Trace quadratic probing and double hashing; observe reduced clustering.
- **Afternoon (40 min):** Compute rolling hash for pattern "AB" in text "ABRACADABRA". Trace sliding window updates.
- **Evening (20 min):** Verify false positive: two different substrings hash to same value (collision). Discuss probability and mitigation.

---

## ✅ WEEK 3 COMPLETION CHECKLIST

### Pattern Mastery — Do You Understand These Deeply?

- [ ] **Sorting:** Can you trace insertion sort, merge sort, and quicksort by hand on 5 elements without reference?
- [ ] **Sorting Trade-offs:** Can you justify why insertion sort beats merge sort on nearly sorted data?
- [ ] **Heap Property:** Can you explain why binary heap's parent-child indexing works and derive it?
- [ ] **Heapify:** Can you trace bubble-up and heapify-down without memorizing steps?
- [ ] **Hash Functions:** Can you design a simple hash function and identify its weaknesses?
- [ ] **Collision Resolution:** Can you trace separate chaining, linear probing, and double hashing?
- [ ] **Load Factor:** Can you explain why resizing is critical and when to trigger it?
- [ ] **Rolling Hash:** Can you compute rolling hash updates in O(1) without recomputing from scratch?

### Problem Solving — Can You Apply These?

- [ ] **Stage 1 Complete:** Solved all foundational tracing exercises (3 sorts, 3 heaps, 3 hashing)
- [ ] **Stage 2 Complete:** Solved 8+ variation problems and justified algorithm choices
- [ ] **Stage 3 Attempted:** Tackled at least 2 real-world application problems
- [ ] **Code Implementation:** Wrote clean, commented implementations of merge sort, heapsort, hash table

### Interview Readiness — Are You Interview-Ready?

- [ ] **Quick Recall:** Can you sketch O(n log n) sort algorithm in under 5 minutes?
- [ ] **Explain Trade-offs:** Can you discuss when to use quicksort vs. merge sort vs. heapsort in an interview?
- [ ] **Production Awareness:** Can you discuss why Python uses Timsort and how it differs from quicksort?
- [ ] **Hash Table Internals:** Can you explain load factor, resizing strategy, and rolling hash in detail?
- [ ] **Real Examples:** Can you name 3 systems that use heaps and explain why?

### Conceptual Depth — Do You "Get It"?

- [ ] **Why Sorting Matters:** Understand that sorting enables binary search, merging, and many algorithms
- [ ] **Why Heaps Matter:** Understand that heaps are the data structure behind priority queues
- [ ] **Why Hashing Matters:** Understand that hash tables are the foundation of caching, databases, and language sets/dicts
- [ ] **Connection to MIT 6.006:** Understand how these fit into the broader algorithmic toolkit

---

## 🎯 WEEK 3 SUCCESS CRITERIA

### Green Light (Mastery)
- ✅ Can trace all algorithms by hand on 5-element inputs (< 10 min each)
- ✅ Can justify algorithm choice given problem constraints
- ✅ Can implement in pseudocode or code (depending on course level)
- ✅ Can explain real-world applications (Linux scheduler, Python dict, Dijkstra's algorithm)
- ✅ Attempted all Stage 2 problems; solved most

### Yellow Light (In Progress)
- 🟡 Can trace algorithms but with occasional errors
- 🟡 Unsure about when to use which algorithm
- 🟡 Can implement basic sorting but struggle with heap operations
- 🟡 Attempted some Stage 2 problems

### Red Light (Needs Review)
- ❌ Cannot trace algorithms by hand
- ❌ Don't understand heap indexing or heapify
- ❌ Hash table collision resolution unclear
- ❌ Rolling hash seems magical, not mechanically clear

**If Red Light:** Spend another day on whichever area is weakest. Trace by hand until mechanical understanding clicks.

---

## 📚 INTEGRATION WITH PRIOR & FUTURE WEEKS

### How Week 3 Builds on Week 2

- **Week 2 Linear Structures** → **Week 3 Sorting:** Sorting operates on sequences; list reversal, linked list merging use the same pointer mechanics
- **Week 2 Binary Search** → **Week 3 Sorting:** Binary search requires sorted data; understand sorting prerequisites
- **Week 2 Stacks/Queues** → **Week 3 Heaps:** Heaps are trees maintained via array; heapify uses stack-like recursive logic

### How Week 3 Enables Week 4+

- **Week 4 Graphs:** Dijkstra's algorithm relies on min-heap for efficiency (O(E log V) vs. O(E²))
- **Week 5 Dynamic Programming:** DP often requires sorting (e.g., interval DP sorts intervals first)
- **Week 6 Trees:** Balanced trees (AVL, red-black) use similar rebalancing logic to heapify-down
- **Week 8 Databases:** B-trees and indices use both sorting and hashing for query optimization

---

## 🧠 WEEK 3 REFLECTION PROMPTS

Reflect on these after finishing the week:

1. **Deep Dive on Quicksort:** Why does random pivot prevent adversarial worst-case input? Can you design an input that breaks fixed-pivot quicksort?

2. **Heap Efficiency:** Why is build-heap O(n) when we could insert n elements in O(n log n)? Derive the math.

3. **Hash Table Paradox:** Why can a hash table be O(1) average when we know collisions happen? What assumption makes this true?

4. **Rolling Hash Magic:** Why does removing c₀*b^(k-1) and adding new_char give us the hash of the next window? Derive the algebra.

5. **Real-World Choice:** In your favorite programming language, what sorting algorithm does it use? Why? What data structure does the built-in set/dict use? Why?

---

## 🚀 WEEK 3 RESOURCE GUIDE

### Recommended Resources

| Resource | Type | Why It's Worth It | Time |
|----------|------|------|------|
| **"Algorithm Design Manual" — Skiena (Ch. 4-5)** | Book | Practical perspective on sorting; discusses real-world performance | 2-3 hours |
| **"Introduction to Algorithms" — CLRS (Ch. 6-11)** | Book | Rigorous treatment; heaps, sorting, hashing with proofs | 4-6 hours |
| **VisuAlgo.net (Sorting & Hash Tables)** | Interactive | Animated trace of all sorting algorithms; see comparisons | 1-2 hours |
| **MIT OpenCourseWare 6.006 Lecture Videos** | Video | Prof. Erik Demaine's lectures on sorting, hashing, heaps | 3-4 hours |
| **LeetCode Problems (Sorting, Heap, Hash)** | Practice | Real interview problems using these concepts | 3-5 hours |

---

## ⏰ WEEKLY TIME BUDGET

| Activity | Time |
|----------|------|
| Study (reading, visualizing, understanding) | 4-5 hours |
| Tracing by hand (exercises, examples) | 3-4 hours |
| Coding/Implementation | 2-3 hours |
| Practice problems (Stage 1-2) | 4-5 hours |
| Integration & reflection | 1-2 hours |
| **Total** | **14-19 hours** |

**Recommendation:** Spread across 5-7 days; 2-3 hours per day is sustainable.

---

## 🎓 FINAL NOTE: WHY THIS WEEK MATTERS

Week 3 is the inflection point. You're no longer just understanding data structures; you're understanding how the *world* runs on them. Every database query uses sorting or hashing. Every recommendation system uses heaps. Every algorithm interview tests your grasp of these primitives.

By the end of this week, you won't just know sorting algorithms—you'll understand the engineering trade-offs that shaped them. You won't just implement a heap—you'll see how it powers schedulers in production systems. This is where the theoretical starts becoming intuition.

Push through the tracing exercises. They're not busy work. They're how your brain internalizes these patterns until they become second nature.

---

*End of Week 03 Problem Solving Roadmap — Version 12.0 FINAL*

**Status:** ✅ COMPLETE & PRODUCTION READY


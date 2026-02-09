# DSA Learning Syllabus (From 6 Books)

This syllabus organizes theory and learning content from:

- **DPV** – Algorithms by Dasgupta, Papadimitriou, Vazirani
- **KT** – Algorithm Design by Kleinberg & Tardos
- **CLRS** – Introduction to Algorithms (4th edition)
- **Skiena** – The Algorithm Design Manual
- **CTCI** – Cracking the Coding Interview (6th edition)
- **EPI** – Elements of Programming Interviews

The structure is **topic-based**, not book-based. For each topic you get:

- **Primary reading** – main chapters/sections
- **Supplement / interview focus** – extra, more applied material
- **Key concepts to master** – a checklist of what to understand

You can combine this with your practice file `dsa-questions.md` for problem solving.

---

## Phase 0 – Foundations & Complexity

### Primary Reading
- **CLRS**: Ch. 1–3 (Introduction, Getting Started, Growth of Functions)
- **CLRS**: Ch. 4 (Divide-and-Conquer & Recurrences)
- **DPV**: Intro + early sections on algorithms and efficiency
- **KT**: Ch. 1–2 (Introduction, Basics of algorithm analysis)

### Supplement / Interview Focus
- **CTCI**: Intro + Big-O and complexity overview section
- **EPI**: Preface/introduction sections on complexity and problem-solving
- **Skiena**: Ch. 1 (Introduction to Algorithm Design; mindset and modeling)

### Key Concepts to Master
- What an algorithm is; correctness vs. efficiency
- Asymptotic notation: \(O(\cdot), \Omega(\cdot), \Theta(\cdot)\)
- Common complexity classes: logarithmic, linear, \(n \log n\), quadratic, exponential
- Basic recurrences and solving them (substitution, recursion tree, Master Theorem)
- Proving correctness using loop invariants
- Worst-case, average-case, and amortized analysis (at a high level)

---

## Phase 1 – Arrays, Strings, and Hashing

### Primary Reading
- **CTCI**: Ch. 1 – Arrays and Strings
- **EPI**: Arrays chapter; Strings chapter
- **CLRS**: 
  - Ch. 10 (Elements of data structures – arrays overview)
  - Ch. 11 (Hash Tables)

### Supplement / Interview Focus
- **Skiena**: Sections on basic data structures (arrays, hash tables) in Part I
- **DPV / KT**: Use array and hash-based examples from early chapters as warm-up

### Key Concepts to Master
- Array indexing, memory layout, and iteration patterns
- Common array operations: insertion, deletion, rotation, merging
- Two-pointer patterns on arrays and strings (start/end, slow/fast)
- Frequency counting and grouping using hash maps/sets
- Anagram checks, substring problems, sliding window templates
- Hash table concepts: load factor, collisions, chaining vs. open addressing
- Trade-offs: arrays vs. linked lists vs. dynamic arrays

---

## Phase 2 – Linked Lists, Stacks, and Queues

### Primary Reading
- **CTCI**: 
  - Ch. 2 – Linked Lists
  - Ch. 3 – Stacks and Queues
- **EPI**: Linked Lists chapter; Stacks & Queues chapter
- **CLRS**: Ch. 10 (Linked lists, stacks, queues implementation)

### Supplement / Interview Focus
- **Skiena**: Part I data-structure sections for list-like and stack/queue structures

### Key Concepts to Master
- Singly vs. doubly linked lists; circular lists
- Operations: insert, delete, reverse, merge, detect cycle
- Fast/slow pointer patterns (middle element, cycle detection, palindrome)
- Implementation of stacks and queues using arrays and linked lists
- Usage patterns: parentheses matching, undo/redo, BFS queue
- When lists are better than arrays, and vice versa

---

## Phase 3 – Trees, BSTs, and Balanced Trees

### Primary Reading
- **CTCI**: Ch. 4 – Trees and Graphs (tree portion)
- **EPI**: Binary Trees chapter; Binary Search Trees chapter
- **CLRS**:
  - Ch. 12 – Binary Search Trees
  - Ch. 13 – Red-Black Trees (for balanced-tree intuition)

### Supplement / Interview Focus
- **Skiena**: Catalog entries for trees and search trees in Part II

### Key Concepts to Master
- Tree terminology: nodes, edges, depth, height, leaves, paths
- Binary tree traversals: pre-order, in-order, post-order, level-order
- Recursion patterns on trees: subtree results, passing state down/up
- Binary Search Trees (BST): invariants, search/insert/delete, min/max
- Height-balanced trees (AVL/Red-Black) – conceptual understanding
- Common interview tasks: validate BST, lowest common ancestor, diameter, view problems

---

## Phase 4 – Graphs and Graph Traversal

### Primary Reading
- **DPV**: Graph Algorithms chapter (BFS, DFS, connectivity)
- **KT**: Early graph chapter (graph traversals, connectivity, topological order)
- **CLRS**: Ch. 22 – Elementary Graph Algorithms (BFS, DFS, connected components)

### Supplement / Interview Focus
- **CTCI**: Ch. 4 – Trees and Graphs (graph problems)
- **EPI**: Graphs chapter (if present in your edition)
- **Skiena**: Graph-related catalog entries (connectivity, path-finding, etc.)

### Key Concepts to Master
- Graph representations: adjacency list vs. adjacency matrix
- Directed vs. undirected graphs; weighted vs. unweighted
- BFS: shortest path in unweighted graphs, level-order traversal
- DFS: recursion vs. explicit stack, exploring components, cycle detection
- Topological sorting and DAGs
- Modeling real problems as graphs: grids, prerequisite graphs, connectivity

---

## Phase 5 – Sorting, Searching, and Divide & Conquer

### Primary Reading
- **CLRS**:
  - Ch. 2 – Insertion Sort, Merge Sort
  - Ch. 6 – Heapsort
  - Ch. 7 – Quicksort
  - Ch. 8 – Sorting in Linear Time (Counting, Radix)
  - Ch. 9 – Medians and Order Statistics
- **KT**: Ch. on Divide-and-Conquer (binary search, median algorithms)
- **DPV**: Divide-and-Conquer chapter

### Supplement / Interview Focus
- **CTCI**: Ch. 10 – Sorting and Searching
- **EPI**: Searching and Sorting chapters

### Key Concepts to Master
- Comparison-based sorting lower bounds; \(n \log n\) barrier
- Implementing and analyzing merge sort and quicksort
- Partitioning logic and randomized quicksort
- Binary search and its variants (first/last occurrence, rotated arrays, etc.)
- Selection algorithms: quickselect and median-of-medians idea
- Stability of sorting and when it matters

---

## Phase 6 – Greedy Algorithms

### Primary Reading
- **DPV**: Greedy Algorithms chapter (scheduling, interval covering, etc.)
- **KT**: Greedy Algorithms chapter (interval scheduling, MSTs, Huffman coding)
- **CLRS**: Ch. 16 – Greedy Algorithms (activity selection, Huffman, etc.)

### Supplement / Interview Focus
- **Skiena**: Greedy algorithm patterns in the problem catalog

### Key Concepts to Master
- Greedy-choice property and optimal substructure
- Proving correctness of greedy algorithms (exchange arguments)
- Classic problems: activity selection, interval scheduling, Kruskal/Prim for MST
- Counterexamples where greedy fails (e.g., 0/1 knapsack)
- Real-world modeling: gas station, jump game, interval partitioning

---

## Phase 7 – Dynamic Programming

### Primary Reading
- **DPV**: Dynamic Programming chapter (Fibonacci, knapsack, edit distance, etc.)
- **KT**: Dynamic Programming chapter (segmented least squares, weighted intervals)
- **CLRS**: DP chapter (rod cutting, matrix chain multiplication, LCS)

### Supplement / Interview Focus
- **CTCI**: Recursion and Dynamic Programming chapter
- **EPI**: DP chapter (coin change, LIS, knapsack-style problems)
- **Skiena**: DP-related problems in the catalog

### Key Concepts to Master
- Recognizing overlapping subproblems and optimal substructure
- Designing DP states: indices, capacities, masks, and other dimensions
- Top-down (memoization) vs. bottom-up tabulation
- 1D DP (climbing stairs, house robber) vs. 2D DP (grid paths, edit distance)
- Sequence DP: LIS, LCS, subsequences and substrings
- Combinatorial counting with DP
- Space optimization techniques (rolling arrays, in-place DP)

---

## Phase 8 – Backtracking, Recursion, and Combinatorial Search

### Primary Reading
- **CTCI**: Recursion and Dynamic Programming chapter (backtracking examples)
- **EPI**: Recursion/backtracking chapter (subsets, permutations, n-queens, Sudoku)
- **CLRS**: Backtracking examples (e.g., n-queens, subset sum) if present in your edition

### Supplement / Interview Focus
- **Skiena**: Search/backtracking catalog entries

### Key Concepts to Master
- Recursive problem decomposition and base/recursive cases
- Generating permutations, combinations, and subsets
- N-queens, Sudoku, and other constraint satisfaction problems
- Pruning and bounding to reduce search space
- Using recursion with data structures (trees, grids, graphs)
- Difference between brute-force search, backtracking, and DP

---

## Phase 9 – Heaps, Priority Queues, and Advanced Data Structures

### Primary Reading
- **CLRS**: 
  - Ch. 6 – Heaps and Heapsort
  - Ch. 14 – Augmenting Data Structures (for advanced trees)
- **EPI**: Heaps chapter; Advanced data-structures chapter

### Supplement / Interview Focus
- **Skiena**: Problem catalog entries regarding heaps, priority queues, union-find
- **CTCI**: Problems involving heaps, maps, and sets (scattered across chapters)

### Key Concepts to Master
- Binary heap structure and invariants
- Priority queue operations and typical use-cases
- Applications: k-way merge, median maintenance, Dijkstra implementation
- Disjoint-set (Union-Find): union by rank, path compression (from CLRS graph chapters)
- Augmented trees (order statistic trees, interval trees) – at least conceptually

---

## Phase 10 – Graph Shortest Paths, MST, and Flow (Optional Deep Dive)

### Primary Reading
- **CLRS**:
  - Ch. 23 – Minimum Spanning Trees (Kruskal, Prim)
  - Ch. 24 – Single-Source Shortest Paths (Dijkstra, Bellman–Ford)
  - Ch. 25 – All-Pairs Shortest Paths (Floyd–Warshall)
  - Ch. 26 – Max-Flow (Ford–Fulkerson, Edmonds–Karp)
- **DPV**: Shortest paths and flow chapters
- **KT**: Graph algorithms chapters for MSTs, shortest paths, and flows

### Supplement / Interview Focus
- **Skiena**: Network-flow and shortest-path problems in the catalog

### Key Concepts to Master
- MST: cut property, cycle property, correctness of Kruskal/Prim
- Dijkstra vs. Bellman–Ford: constraints, complexity, negative edges
- All-pairs shortest paths and dynamic programming interpretation
- Max-flow / min-cut theorem and basic flow algorithms
- Modeling: bipartite matching, assignment problems via flow

---

## Phase 11 – Interview-Oriented Mixed Practice

### Primary Reading
- **CTCI**: Revisit all algorithmic chapters:
  - Ch. 1 – Arrays and Strings
  - Ch. 2 – Linked Lists
  - Ch. 3 – Stacks and Queues
  - Ch. 4 – Trees and Graphs
  - Ch. 5 – Bit Manipulation
  - Ch. 8 – Recursion and DP
  - Ch. 10 – Sorting and Searching
- **EPI**: Work through representative problems from each major chapter
- **Skiena**: Use the problem catalog as a reference for similar problems and patterns

### Key Concepts to Master
- Mapping any new problem to a known pattern (two pointers, sliding window, DP, etc.)
- Estimating time and space quickly and justifying your design
- Communicating solution ideas, edge cases, and trade-offs out loud
- Refactoring initial brute-force ideas into optimal or near-optimal versions

---

## Suggested Usage

- Treat each **Phase** as 3–7 focused study sessions depending on your depth and available time.
- For each session:
  1. Read the **Primary Reading** sections for that topic.
  2. Skim the **Supplement / Interview Focus** for applied patterns.
  3. Write brief notes summarizing the **Key Concepts to Master**.
  4. Then switch to coding practice (e.g., from your `dsa-questions.md` file).
- Revisit CTCI and EPI periodically as mock-interview style checkpoints.

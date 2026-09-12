# Phase 25 — Scale, Design & Advanced Signatures

> **Curriculum Phase:** 25  
> **Topic Domain:** Scaled Stream Counters, Time-Indexed Key-Value Stores, Weighted Interval DP, 2-Coloring Graphs, Matrix DAG Memoization, Combinatorial Scheduling, and Origin-Destination Transit Aggregators  
> **Problems Covered:** #154 – #164 (11 Problems)  
> **Target Mastery:** Bounded circular ring buffers, MVCC floor binary search, graph division transitivity, monotonic elevation DAGs, weighted interval non-overlapping DP, suffix inversion permutations, multi-calendar sweep-line gaps, 2-coloring bipartite invariants, permutation recurrence combinatorics, and dual-table transit analytics.

---


# Phase 25: Scale, Design & Advanced Signatures

> **Focus:** Scaled Stream Counters, Ring Buffers, Time-Indexed Binary Search, Multi-Version Key-Value Stores, Weighted Graph Transitive Division, Dynamic Topological DAG Memoization, and High-Throughput Invariants.  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 25 (Part 1: Problems #154–#158)  
> **Target Level:** Senior / Staff / Lead Software Engineer (Google, Meta, Netflix, Uber Signature Patterns)  
> **Language & Runtime:** C# 12 / .NET 8 & 9 (Idiomatic, Concurrency-aware, Zero-allocation where applicable)

---

---

## 154. Binary Tree Right Side View (LeetCode #199)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#binary-tree` `#bfs-level-order` `#dfs-reverse-preorder` `#view-projection` `#tree-traversal` |
| **LeetCode Link** | [Binary Tree Right Side View](https://leetcode.com/problems/binary-tree-right-side-view/) |

### 1. Problem Detail & Constraints

- **Formal Statement:** Given the `root` of a binary tree, imagine yourself standing on the **right side** of it. Return an ordered list of the values of the nodes you can see ordered from top to bottom.
- **Assumptions & Contracts:**
  - An empty tree (`root == null`) returns an empty collection `[]` immediately without exception.
  - At each discrete depth level $d \in [0, H - 1]$ (where $H$ is the height of the tree), exactly **one** node is visible from the right: the rightmost node existing at depth $d$.
  - A node residing on the left subtree can be visible if and only if no node exists at that same depth in the right subtree (i.e., the right subtree terminates earlier than the left subtree).
- **Key Constraints:**
  - The number of nodes in the tree is in the range $[0, 100]$.
  - Node values satisfy $-100 \le node.val \le 100$.
  - Generalizes to arbitrary scales: $N \le 10^5$, height $H \le 10^5$ (skewed trees).
- **Senior Edge Cases to Defend:**
  - **Null Tree (`root == null`):** Guard clause must return an empty list without dereferencing `root`.
  - **Single Node:** `[1]` returns `[1]`.
  - **Degenerate Left-Skewed Tree (`1 -> 2 -> 3 -> null`):** No right children exist anywhere. Every left child is completely unobstructed from the right view and must be captured: `[1, 2, 3]`.
  - **Asymmetric Jagged Tree (Right Subtree Shorter Than Left Subtree):**
    ```text
          1
        /   \
       2     3
        \
         4
    ```
    Depth 0 sees `1`, depth 1 sees `3` (occludes `2`), depth 2 sees `4` (no node exists at depth 2 in right subtree). Result: `[1, 3, 4]`.
  - **Complete Binary Tree vs Full Tree:** Symmetric levels must strictly pick the right child.

---

### 2. Summary & Sample Input / Output

- **Conceptual Essence:** 1D vertical orthogonal projection of a 2D hierarchical graph. For every discrete vertical coordinate (depth $y$), find $\operatorname{argmax}_{x} \text{node}(x, y)$. This can be solved via **Level-Order BFS Snapshots** (extracting the last element of each queue snapshot) or **Reverse Pre-Order DFS** (`Root -> Right -> Left`, recording a node if and only if its depth matches the current output list count).
- **Sample 1:**
  - **Input:** `root = [1, 2, 3, null, 5, null, 4]`
  - **Output:** `[1, 3, 4]`
  - **Diagram:**
    ```text
           1            <--- Visible: 1
         /   \
        2     3         <--- Visible: 3 (occludes 2)
         \     \
          5     4       <--- Visible: 4 (occludes 5)
    ```
- **Sample 2 (Asymmetric Jagged):**
  - **Input:** `root = [1, 2, 3, 4]`
  - **Output:** `[1, 3, 4]`
  - **Diagram:**
    ```text
           1            <--- Visible: 1
         /   \
        2     3         <--- Visible: 3
       /
      4                 <--- Visible: 4 (unobstructed left leaf)
    ```
- **Sample 3 (Empty Tree):**
  - **Input:** `root = []`
  - **Output:** `[]`

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine shining a powerful horizontal spotlight from the far right horizon toward a mountain range with trees planted along its ridges. The light casts shadows westward. Any tree that has an eastern neighbor standing at the exact same elevation is completely engulfed in shadow and invisible. Only the easternmost tree at each distinct elevation line catches the light. To catalog what a surveyor sees from the eastern horizon, you simply walk down from the mountain peak elevation-by-elevation, writing down the first tree you see at each contour.

#### 3.2 The Naive Bottleneck & Redundant Computation
A naive approach computes the $(x, y)$ coordinates of every node in the tree using Cartesian mapping, stores all coordinates in a hash map `Dictionary<int, List<int>>` grouped by depth $y$, sorts every list by horizontal offset $x$, and picks the maximum.
- **Wasted Work:** Allocating auxiliary coordinate maps, tracking horizontal displacements, and sorting nodes at every depth.
- **Algorithmic Inefficiency:** $O(N \log N)$ or $O(N)$ with heavy heap allocations for coordinates that are never needed. Tree traversal order already encodes depth and lateral hierarchy naturally.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Invariant 1 (BFS Level Snapshot):**
  When processing level $d$ in a FIFO queue of size $K$:
  $$\text{queue} = [u_{d, 0}, u_{d, 1}, \dots, u_{d, K-1}]$$
  If children are enqueued in canonical `left`-then-`right` order, then node $u_{d, K-1}$ is deterministically the rightmost node at depth $d$. Its value is appended to the result, while all prior $K - 1$ nodes are consumed purely to discover their children.
- **Invariant 2 (DFS Depth-Ledger Occlusion):**
  If we traverse the tree recursively in **Reverse Pre-Order** (`Root` $\to$ `Right` $\to$ `Left`):
  At any point in time, `result.Count` represents the total number of depth levels visited so far.
  Because the right branch is explored strictly before the left branch, the *very first* node to reach depth $d$ is mathematically guaranteed to be the rightmost node at depth $d$.
  Therefore, the decision gate is purely:
  $$\text{if } \text{depth} == \text{result.Count} \implies \text{result.Add}(node.val)$$
  Any subsequent node arriving at depth $d$ (from leftward branches) observes $\text{depth} < \text{result.Count}$ and is immediately ignored (occluded).

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Level-Order BFS Snapshot Invariant:
-------------------------------------------------------------------------------
Queue State at Level d:
[ u_{d, 0} (Leftmost)  -->  u_{d, 1}  -->  ...  -->  u_{d, K-1} (Rightmost) ]
 └───────────────────────┬──────────────────────┘    └──────────┬──────────┘
             Internal Level Nodes (Shadowed)              Visible Node
                    (Children enqueued)                 (Pushed to Result)

Occlusion Plane (Camera at Right Infinity):
              1                │ Camera
            /   \              │
           2     3   <=========│ Level 1: Sees 3 (2 is occluded)
            \                  │
             4       <=========│ Level 2: Sees 4 (unobstructed)
```

#### 3.5 State Transition Triggers & Decision Gates

```text
BFS Traversal:
1. Initialize queue with root. While queue is not empty:
2. Snapshot level size: K = queue.Count.
3. For i = 0 to K - 1:
   a. Dequeue current node.
   b. If i == K - 1: Append node.val to result.
   c. If node.left != null: Enqueue node.left.
   d. If node.right != null: Enqueue node.right.

DFS Traversal:
1. Recurse(node, depth):
   a. If node == null: Return.
   b. If depth == result.Count: result.Add(node.val) [Occlusion Gate].
   c. Recurse(node.right, depth + 1) [Priority: Right First].
   d. Recurse(node.left, depth + 1)  [Fallback: Left Second].
```

#### 3.6 Concrete Step-by-Step State Trace

Given `root = [1, 2, 3, null, 5, null, 4]`:

| Step / Level | Queue Contents at Start of Level | Level Size ($K$) | Dequeued Nodes ($i = 0 \dots K-1$) | Visible Node ($i = K-1$) | Result List State | Next Level Queue Enqueued |
| :---: | :--- | :---: | :--- | :---: | :--- | :--- |
| **Level 0** | `[1]` | 1 | $i=0: \text{Node}(1)$ | **1** | `[1]` | `[2, 3]` |
| **Level 1** | `[2, 3]` | 2 | $i=0: \text{Node}(2)$<br>$i=1: \text{Node}(3)$ | **3** | `[1, 3]` | From 2: `[5]`<br>From 3: `[4]` $\implies [5, 4]$ |
| **Level 2** | `[5, 4]` | 2 | $i=0: \text{Node}(5)$<br>$i=1: \text{Node}(4)$ | **4** | `[1, 3, 4]` | None (leaves) |

Final Output: `[1, 3, 4]`. Terminated in 3 level iterations visiting each of the 5 nodes exactly once.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: BFS Level-Order Snapshot (Industry Standard):**
  - *When to Use:* Default choice in production when tree balance is unknown or stack depth limits exist.
  - *Auxiliary Memory:* Proportional to the maximum tree width $W \le \lceil N / 2 \rceil$.
  - *Cache Locality:* Moderate. Relies on `Queue<TreeNode>` referencing nodes in heap memory.
- **Approach 2: DFS Reverse Pre-Order (`Root -> Right -> Left`):**
  - *When to Use:* High-performance environments with well-balanced trees or when zero collection allocations (other than recursion stack) are mandated.
  - *Auxiliary Memory:* Proportional to tree height $H$. For balanced trees, $H = O(\log N)$, which consumes drastically less memory than BFS ($O(\log N)$ vs $O(N)$). For heavily skewed trees, recursion stack reaches $O(N)$.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Guard Clause:** Verify `root != null`. Return empty array or empty `List<int>`.
2. **Queue Allocation:** Instantiate `Queue<TreeNode>`. Pre-allocate result list with initial capacity (e.g., 16 or estimated height) to avoid dynamic array resizing.
3. **Loop Invariant:** Outer `while (queue.Count > 0)` drives level transitions.
4. **Snapshot Lock:** `int levelSize = queue.Count`. Never evaluate `queue.Count` inside the inner loop condition, as enqueuing child nodes mutates the count dynamically.
5. **Ingestion & Propagation:** Extract elements $0 \dots levelSize - 1$. The terminal element ($i == levelSize - 1$) joins the result.

#### 4.3 Alternative Approaches Analysis
- **DFS with Depth Hash Map:** Traverse in standard pre-order (`Root -> Left -> Right`), storing `map[depth] = node.val`. Since left is visited first and right is visited later, right overwrites left. Flaw: Unnecessary $O(H)$ hash map allocations and hash bucket overhead. The reverse pre-order `depth == result.Count` eliminates the hash map entirely.
- **Morris Traversal Variant:** Can achieve $O(1)$ auxiliary space by threading tree nodes, but requires complex pointer mutation and restoration, making it error-prone and unsuitable for read-only concurrent trees.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. BFS Level-Order** | $O(N)$ | $O(N)$ | $O(N)$ | $O(W) = O(N)$ | $O(H)$ | Moderate (Queue references) | Read-Only | Excellent (Level-by-level) |
| **2. DFS Reverse Pre-Order**| $O(N)$ | $O(N)$ | $O(N)$ | $O(H)$ stack | $O(H)$ | High (Call stack frames) | Read-Only | Poor (Requires tree root) |
| **3. Coordinate Hash Map** | $O(N)$ | $O(N)$ | $O(N)$ | $O(N) + O(H)$ map | $O(H)$ | Low (Map buckets) | Read-Only | Moderate |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Problem #154 - Binary Tree Right Side View
 * ============================================================================
 * Core Pattern      : BFS Level-Order Queue Snapshot / DFS Reverse Pre-Order
 * Time Complexity   : O(N) visiting each node exactly once
 * Space Complexity  : BFS: O(W) where W is max level width (up to N/2)
 *                     DFS: O(H) where H is tree height (log N avg, N worst)
 * Selection Rule    : Default to BFS (Approach 1) for wide/skewed tree safety.
 *                     Transition to DFS (Approach 2) when minimal memory on balanced
 *                     trees is required without queue allocation.
 * Defensive Traps   : 1. Do NOT assume only right children are visible (left children
 *                        must be visible if right subtree terminates early).
 *                     2. Snapshot queue.Count into a local variable before loop.
 * ============================================================================
 */

namespace SeniorDSA.ScaleAndDesign;

/// <summary>
/// Definition for a binary tree node.
/// </summary>
public sealed class TreeNode
{
    public int val;
    public TreeNode? left;
    public TreeNode? right;

    public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
    {
        this.val = val;
        this.left = left;
        this.right = right;
    }
}

public class SolutionBinaryTreeRightSideView
{
    /// <summary>
    /// Approach 1: Optimal BFS Level-Order Snapshot.
    /// Traverses the tree level-by-level, recording the terminal node of each level snapshot.
    /// </summary>
    /// <param name="root">Root of the binary tree.</param>
    /// <returns>List of visible node values from top to bottom.</returns>
    public IList<int> RightSideView(TreeNode? root)
    {
        // Guard Clause: Empty tree returns empty list immediately
        if (root == null)
        {
            return Array.Empty<int>();
        }

        var visibleNodes = new List<int>();
        var levelQueue = new Queue<TreeNode>();
        levelQueue.Enqueue(root);

        // Exploration Invariant: Each iteration processes exactly one complete horizontal level
        while (levelQueue.Count > 0)
        {
            // Critical Snapshot: Freeze level width before enqueuing child nodes
            int levelSize = levelQueue.Count;

            for (int i = 0; i < levelSize; i++)
            {
                TreeNode currentNode = levelQueue.Dequeue();

                // Decision Gate: The final element in the level queue is the rightmost visible node
                if (i == levelSize - 1)
                {
                    visibleNodes.Add(currentNode.val);
                }

                // Invariant: Enqueue left child before right child to maintain left-to-right ordering
                if (currentNode.left != null)
                {
                    levelQueue.Enqueue(currentNode.left);
                }

                if (currentNode.right != null)
                {
                    levelQueue.Enqueue(currentNode.right);
                }
            }
        }

        return visibleNodes;
    }

    /// <summary>
    /// Approach 2: Optimal DFS Reverse Pre-Order (Root -> Right -> Left).
    /// Uses the occlusion ledger invariant: the first node to reach a depth is visible.
    /// </summary>
    /// <param name="root">Root of the binary tree.</param>
    /// <returns>List of visible node values from top to bottom.</returns>
    public IList<int> RightSideViewDfs(TreeNode? root)
    {
        if (root == null)
        {
            return Array.Empty<int>();
        }

        var visibleNodes = new List<int>();
        ExploreRightFirst(root, currentDepth: 0, visibleNodes);
        return visibleNodes;
    }

    private static void ExploreRightFirst(TreeNode? node, int currentDepth, List<int> visibleNodes)
    {
        if (node == null)
        {
            return;
        }

        // Occlusion Invariant: If currentDepth equals visibleNodes.Count, this node is the
        // first node encountered at this depth level. Because of Root -> Right -> Left traversal,
        // it is strictly the rightmost node at this depth.
        if (currentDepth == visibleNodes.Count)
        {
            visibleNodes.Add(node.val);
        }

        // Prioritize right subtree exploration over left subtree
        ExploreRightFirst(node.right, currentDepth + 1, visibleNodes);
        ExploreRightFirst(node.left, currentDepth + 1, visibleNodes);
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps

1. **The "Right-Child-Only" Fallacy:**
   - *Trap:* Writing `curr = curr.right` down the tree, completely ignoring left subtrees.
   - *Failure:* On jagged trees where the left subtree extends deeper than the right subtree, all lower left nodes are dropped, returning truncated views.
   - *Defense:* Both subtrees must be traversed; occlusion is determined by depth equivalence, never by branch existence alone.
2. **Dynamic Queue Mutation in Loop Condition:**
   - *Trap:* Writing `for (int i = 0; i < queue.Count; i++)`.
   - *Failure:* `queue.Count` shifts dynamically as children are enqueued, corrupting level boundaries and mixing levels into a single pass.
   - *Defense:* Capture `int levelSize = queue.Count;` as an immutable scalar before entering the loop.
3. **Stack Overflow on Pathological Trees in DFS:**
   - *Trap:* Relying on recursive DFS in environments with $N \ge 10^5$ on skewed trees.
   - *Failure:* Default .NET thread stack (1 MB) overflows around ~15,000 recursive frames.
   - *Defense:* For massive unbalanced trees, default to BFS or simulate DFS with an explicit heap-allocated `Stack<(TreeNode Node, int Depth)>`.
4. **Queue Garbage Collection Pressure:**
   - *Trap:* Re-instantiating queues across multiple tree queries in streaming engines.
   - *Defense:* Pre-size the queue or reuse collections across frames in high-frequency graphics pipelines.

---
---

## 155. Design Hit Counter (LeetCode #362)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#system-design` `#circular-buffer` `#ring-buffer` `#queue` `#sliding-window` `#concurrency` |
| **LeetCode Link** | [Design Hit Counter](https://leetcode.com/problems/design-hit-counter/) |

### 1. Problem Detail & Constraints

- **Formal Statement:** Design a hit counter which counts the number of hits received in the past 5 minutes (i.e., the past 300 seconds). Implement the `HitCounter` class:
  - `HitCounter()`: Initializes the hit counter system.
  - `void Hit(int timestamp)`: Records a hit that happened at `timestamp` (in seconds). Several hits may arrive at the exact same timestamp.
  - `int GetHits(int timestamp)`: Returns the number of hits received in the past 5 minutes from `timestamp` (i.e., the time window $[timestamp - 299, timestamp]$).
- **Assumptions & Contracts:**
  - All calls are made in chronological order (i.e., `timestamp` is monotonically non-decreasing: $t_1 \le t_2 \le \dots \le t_k$).
  - Several hits may arrive concurrently or at the exact same second.
  - The time window is strictly 300 seconds inclusive: any hit at $t_{\text{hit}} \le timestamp - 300$ has expired and must not be counted.
- **Key Constraints:**
  - $1 \le timestamp \le 2 \times 10^9$.
  - Window size $W = 300$ seconds.
  - Up to $3 \times 10^4$ calls in standard LeetCode, but in Senior System Design interviews: scale to **millions of hits per second** under concurrent multi-threaded writes!
- **Senior Edge Cases to Defend:**
  - **Burst Traffic at Identical Second:** $100,000$ hits arriving at $timestamp = 10$. Storing individual timestamps naively causes memory bloat.
  - **Dormant Gaps / Long Silence:** Hit arrives at $t = 1$, then silence until `GetHits(10000)`. All past hits are long expired; the counter must return `0` without iterating over thousands of empty seconds.
  - **Boundary Inclusion vs Exclusion:**
    - Hit at $t = 1$, query at $t = 300$: $300 - 1 = 299 < 300 \implies$ **Included**.
    - Hit at $t = 1$, query at $t = 301$: $301 - 1 = 300 \ge 300 \implies$ **Expired**.
  - **Modulo Collision / Stale Bucket Retention:** In a fixed circular buffer of size 300, timestamp $t = 1$ and $t = 301$ both map to index $1 \pmod{300}$. If no hits occur at index 1 between $t = 2$ and $t = 300$, the bucket at index 1 still holds the old data from $t = 1$. The system must verify the bucket's timestamp before summing.

---

### 2. Summary & Sample Input / Output

- **Conceptual Essence:** Temporal sliding window rate aggregator. While a `Queue<int>` works for low-traffic sequential problems, production-grade systems demand a **Fixed Circular Array Ring Buffer** of size 300. By decoupling hit count from timestamp storage via two parallel 300-slot arrays (`times[300]` and `hits[300]`), memory consumption is bounded to strictly $O(1)$ space, and `Hit` executes in $O(1)$ time regardless of throughput.
- **Sample Execution Trace:**
  - `HitCounter counter = new HitCounter();`
  - `counter.Hit(1);`
  - `counter.Hit(2);`
  - `counter.Hit(3);`
  - `counter.GetHits(4);   // returns 3 (hits at 1, 2, 3)`
  - `counter.Hit(300);`
  - `counter.GetHits(300); // returns 4 (hits at 1, 2, 3, 300)`
  - `counter.GetHits(301); // returns 3 (hit at 1 expired because 301 - 300 = 1; window is [2, 301])`

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a clock face with 300 second ticks (a roulette wheel of 5 minutes). Every incoming hit places a marble in the bucket pointed to by the second hand (`timestamp % 300`). If the second hand completes a full revolution and lands on that bucket 5 minutes later, it dumps out the old stale marbles from 5 minutes ago and starts fresh with 1 marble. When asked for total hits, the operator takes 300 quick glances at all buckets on the wheel, adding up only the buckets whose label matches the current 5-minute revolution. The wheel never grows, never shrinks, and never leaks memory.

#### 3.2 The Naive Bottleneck & Redundant Computation
- **Naive Approach 1 (Queue of Timestamps):** Store every single hit as an integer in `Queue<int>`.
  - Under heavy traffic ($10^6$ hits/sec), the queue accumulates $3 \times 10^8$ integers in 300 seconds $\approx 1.2 \text{ GB}$ of RAM.
  - `GetHits` must dequeue millions of elements one-by-one, triggering CPU spikes and GC stalls.
- **Naive Approach 2 (Dynamic Array + Binary Search):** Store all timestamps in a dynamic array. To query, binary search for $timestamp - 299$.
  - Array grows monotonically without bounds, leading to an eventual Out-Of-Memory (OOM) crash in production.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Fixed Window Discrete Bucketing:**
  The temporal window is strictly constant ($W = 300$ seconds). Any discrete time $T$ maps to bucket index:
  $$\text{idx} = T \pmod{300}$$
- **The Bucket Epoch Invariant:**
  Each slot $i \in [0, 299]$ maintains two scalar values:
  1. `times[i]`: The exact epoch timestamp of the last hit registered in this bucket.
  2. `hits[i]`: The aggregated hit volume accumulated during second `times[i]`.
- **State Transition Logic on `Hit(t)`:**
  $$\text{idx} = t \pmod{300}$$
  $$\text{if } times[\text{idx}] == t \implies hits[\text{idx}] \leftarrow hits[\text{idx}] + 1$$
  $$\text{if } times[\text{idx}] \ne t \implies times[\text{idx}] \leftarrow t, \quad hits[\text{idx}] \leftarrow 1$$
  *(Overwrites expired data from 300+ seconds ago in $O(1)$!)*
- **Summation Invariant on `GetHits(t)`:**
  A bucket $i$ is valid if and only if:
  $$t - times[i] < 300$$
  $$\text{TotalHits} = \sum_{i=0}^{299} \Big( hits[i] \text{ where } (t - times[i] < 300) \Big)$$
  Summing 300 integer registers takes $< 100$ nanoseconds on modern CPUs with SIMD auto-vectorization.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Fixed 300-Slot Circular Ring Buffer:
-------------------------------------------------------------------------------
Index:        0       1       2      ...     298     299
times[]:   [ 300 ] [   1 ] [   2 ]   ...   [ 298 ] [ 299 ]
hits[]:    [   5 ] [   1 ] [   3 ]   ...   [   0 ] [  12 ]
               │       │
               │       └─ Stale check at t = 301:
               │          (301 - 1 = 300 >= 300) -> EXPIRED! Ignore.
               └───────── Valid check at t = 301:
                          (301 - 300 = 1 < 300)  -> VALID! Add 5.

Window Validity Condition:
[ t - 299 ..................................................... t ]
  Valid Window (Length = 300 sec)
```

#### 3.5 State Transition Triggers & Decision Gates

```text
Operation: Hit(timestamp)
1. idx = timestamp % 300.
2. If times[idx] == timestamp:
   -> Atomic increment: hits[idx]++.
3. Else:
   -> Overwrite: times[idx] = timestamp, hits[idx] = 1.

Operation: GetHits(timestamp)
1. total = 0.
2. Loop i from 0 to 299:
   -> If (timestamp - times[i]) < 300:
        total += hits[i].
3. Return total.
```

#### 3.6 Concrete Step-by-Step State Trace

Trace with circular buffer of size 5 (Window $W = 5$ seconds):
Operations: `Hit(1)`, `Hit(2)`, `Hit(3)`, `GetHits(4)`, `Hit(6)`, `GetHits(6)`

| Step | Call | Index (`t % 5`) | `times` Array Before | `hits` Array Before | Action Taken | `times` Array After | `hits` Array After | Output |
| :---: | :--- | :---: | :--- | :--- | :--- | :--- | :--- | :---: |
| 1 | `Hit(1)` | 1 | `[0, 0, 0, 0, 0]` | `[0, 0, 0, 0, 0]` | `times[1]!=1` $\to$ reset | `[0, 1, 0, 0, 0]` | `[0, 1, 0, 0, 0]` | — |
| 2 | `Hit(2)` | 2 | `[0, 1, 0, 0, 0]` | `[0, 1, 0, 0, 0]` | `times[2]!=2` $\to$ reset | `[0, 1, 2, 0, 0]` | `[0, 1, 1, 0, 0]` | — |
| 3 | `Hit(3)` | 3 | `[0, 1, 2, 0, 0]` | `[0, 1, 1, 0, 0]` | `times[3]!=3` $\to$ reset | `[0, 1, 2, 3, 0]` | `[0, 1, 1, 1, 0]` | — |
| 4 | `GetHits(4)` | — | `[0, 1, 2, 3, 0]` | `[0, 1, 1, 1, 0]` | Sum valid $4 - t < 5$: slots 1, 2, 3 | Unchanged | Unchanged | **3** |
| 5 | `Hit(6)` | 1 | `[0, 1, 2, 3, 0]` | `[0, 1, 1, 1, 0]` | Slot 1 overwrite ($6 \ne 1$) | `[0, 6, 2, 3, 0]` | `[0, 1, 1, 1, 0]` | — |
| 6 | `GetHits(6)` | — | `[0, 6, 2, 3, 0]` | `[0, 1, 1, 1, 0]` | Sum valid $6 - t < 5$: slot 1 ($6-6=0$), slot 2 ($6-2=4$), slot 3 ($6-3=3$). Slot 0 ($6-0=6 \ge 5$). Total = $1 + 1 + 1$ | Unchanged | Unchanged | **3** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Fixed Circular Array Ring Buffer (Staff Signature):**
  - *When to Use:* High-throughput systems, metrics aggregation, network telemetry, rate-limiting counters.
  - *Memory Guarantee:* Bounded strictly to $300 \times 4 \text{ bytes} \times 2 = 2.4 \text{ KB}$. Guaranteed zero GC allocations.
- **Approach 2: Deque / Queue of Pairs `(timestamp, count)`:**
  - *When to Use:* Sparse traffic where events happen hours apart and window size $W$ is dynamically configured.
  - *Drawback:* Degrades under massive high-frequency write storms.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Allocate Memory:** Two fixed arrays `int[300]` for timestamps and hits.
2. **Hit Pipeline:** Map `timestamp` to slot via `% 300`. Compare stored timestamp. If match, increment count; otherwise overwrite timestamp and reset count to 1.
3. **GetHits Pipeline:** Iterate all 300 buckets. Sum `hits[i]` for all buckets where `timestamp - times[i] < 300`.
4. **Concurrency Extension:** Wrap operations in a reader-writer lock (`ReaderWriterLockSlim`) or apply striped atomic `Interlocked` operations for lock-free parallel execution.

#### 4.3 Alternative Approaches Analysis
- **Unbounded Queue:** `Queue<int>`. Dequeue elements while `timestamp - queue.Peek() >= 300`. Memory is $O(N)$ where $N$ is total hits in 5 minutes. Destroys server heap under traffic spikes.
- **Bucket Aggregation with ConcurrentDictionary:** High overhead per entry due to dictionary node wrapping and hash collisions. Fixed array indexed by modulo arithmetic is $100\times$ faster.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (`Hit`) | Time (`GetHits`) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Circular Ring Buffer** | $O(1)$ | $O(300) = O(1)$ | $O(300) = O(1)$ | $O(1)$ | Optimal (L1 cache line array) | High (Direct mutation) | Optimal (Bounded $O(1)$ RAM) |
| **2. Queue of Pairs** | $O(1)$ amortized | $O(K)$ expired | $O(U) \le O(300)$ | $O(1)$ | Moderate (Linked nodes) | Non-mutating | High |
| **3. Naive Queue<int>** | $O(1)$ | $O(N)$ expired | $O(N)$ (Unbounded) | $O(1)$ | Poor (Large heap allocations)| High | Dangerous (OOM risk) |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Problem #155 - Design Hit Counter
 * ============================================================================
 * Core Pattern      : Fixed Circular Bucket Ring Buffer (Modulo Time Partitioning)
 * Time Complexity   : Hit() -> O(1); GetHits() -> O(W) where W = 300 => strictly O(1)
 * Space Complexity  : O(W) auxiliary space where W = 300 integers => strictly O(1)
 * Selection Rule    : Always use fixed circular array over dynamic queue in production
 *                     systems to eliminate unbounded memory growth and GC pressure.
 * Defensive Traps   : 1. Modulo collision: Must store exact timestamp in each bucket
 *                        to detect and overwrite stale data from previous 300s epochs.
 *                     2. Window boundary: Condition is (current - time < 300), NOT <= 300.
 * ============================================================================
 */

namespace SeniorDSA.ScaleAndDesign;

/// <summary>
/// Approach 1: High-Performance Bounded Memory Hit Counter.
/// Employs dual circular arrays of size 300 to achieve strict O(1) space and time.
/// </summary>
public sealed class HitCounter
{
    private const int WindowSize = 300;
    private readonly int[] _timestamps;
    private readonly int[] _hits;

    /// <summary>
    /// Initializes the circular ring buffer for 300 seconds window tracking.
    /// </summary>
    public HitCounter()
    {
        _timestamps = new int[WindowSize];
        _hits = new int[WindowSize];
    }

    /// <summary>
    /// Records a hit at the given timestamp.
    /// Operates in O(1) time with zero memory allocations.
    /// </summary>
    /// <param name="timestamp">Chronological timestamp in seconds.</param>
    public void Hit(int timestamp)
    {
        int bucketIndex = timestamp % WindowSize;

        // Invariant: Check if the bucket belongs to the current second epoch
        if (_timestamps[bucketIndex] == timestamp)
        {
            // Same second: Accumulate hit volume
            _hits[bucketIndex]++;
        }
        else
        {
            // Epoch rollover: Stale data from >= 300 seconds ago is overwritten atomically
            _timestamps[bucketIndex] = timestamp;
            _hits[bucketIndex] = 1;
        }
    }

    /// <summary>
    /// Computes total hits received within the trailing 300-second window [timestamp - 299, timestamp].
    /// Operates in strictly bounded O(300) = O(1) time.
    /// </summary>
    /// <param name="timestamp">Current evaluation timestamp in seconds.</param>
    /// <returns>Total number of valid hits.</returns>
    public int GetHits(int timestamp)
    {
        int totalHits = 0;

        // Scan all 300 buckets in contiguous L1 cache memory
        for (int i = 0; i < WindowSize; i++)
        {
            // Defensive Boundary Check: (timestamp - _timestamps[i]) must be strictly less than 300
            if (timestamp - _timestamps[i] < WindowSize)
            {
                totalHits += _hits[i];
            }
        }

        return totalHits;
    }
}

/// <summary>
/// Approach 2: Thread-Safe Concurrent Circular Ring Buffer.
/// Follow-up signature for multi-threaded distributed web microservices.
/// Uses ReaderWriterLockSlim to permit massive concurrent reads with exclusive writes.
/// </summary>
public sealed class ConcurrentHitCounter : IDisposable
{
    private const int WindowSize = 300;
    private readonly int[] _timestamps;
    private readonly int[] _hits;
    private readonly ReaderWriterLockSlim _rwLock;
    private bool _disposed;

    public ConcurrentHitCounter()
    {
        _timestamps = new int[WindowSize];
        _hits = new int[WindowSize];
        _rwLock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);
    }

    public void Hit(int timestamp)
    {
        int bucketIndex = timestamp % WindowSize;

        _rwLock.EnterWriteLock();
        try
        {
            if (_timestamps[bucketIndex] == timestamp)
            {
                _hits[bucketIndex]++;
            }
            else
            {
                _timestamps[bucketIndex] = timestamp;
                _hits[bucketIndex] = 1;
            }
        }
        finally
        {
            _rwLock.ExitWriteLock();
        }
    }

    public int GetHits(int timestamp)
    {
        int totalHits = 0;

        _rwLock.EnterReadLock();
        try
        {
            for (int i = 0; i < WindowSize; i++)
            {
                if (timestamp - _timestamps[i] < WindowSize)
                {
                    totalHits += _hits[i];
                }
            }
        }
        finally
        {
            _rwLock.ExitReadLock();
        }

        return totalHits;
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            _rwLock.Dispose();
            _disposed = true;
        }
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps

1. **Modulo Collision Without Stale Epoch Check:**
   - *Trap:* Only using a single `hits[300]` array without recording `times[300]`.
   - *Failure:* If a hit occurred at $t = 5$, and the next query is at $t = 605$, index $5$ still holds the count from 10 minutes ago, producing wildly inflated phantom metrics.
   - *Defense:* Always pair the count with the absolute epoch `_timestamps[idx]` to validate freshness.
2. **Window Boundary Off-by-One (`< 300` vs `<= 300`):**
   - *Trap:* Writing `if (timestamp - _timestamps[i] <= 300)`.
   - *Failure:* At $t = 301$, a hit recorded at $t = 1$ gives $301 - 1 = 300$. If `<= 300` is used, the hit is counted, spanning 301 seconds rather than 300 seconds.
   - *Defense:* Strictly enforce `< 300` ($timestamp - \text{hitTime} < 300$).
3. **Queue Memory Exhaustion Under DDoS:**
   - *Trap:* Using `Queue<int>` in production microservices.
   - *Failure:* A DDoS attack with $500,000$ hits/sec allocates 150 million queue nodes in 5 minutes, crashing the server with `OutOfMemoryException`.
   - *Defense:* Mandate fixed circular buffers where memory is structurally constant and immune to traffic volume.
4. **Lock Contention on Coarse-Grained Synchronization:**
   - *Trap:* Putting a naive `lock(this)` on both `Hit` and `GetHits`.
   - *Failure:* Reader threads block each other; throughput collapses under load.
   - *Defense:* Use `ReaderWriterLockSlim` or striped lock partitioning across independent bucket shards.

---
---

## 156. Time Based Key-Value Store (LeetCode #981)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#binary-search` `#hash-map` `#system-design` `#timestamp-indexing` `#floor-entry` |
| **LeetCode Link** | [Time Based Key-Value Store](https://leetcode.com/problems/time-based-key-value-store/) |

### 1. Problem Detail & Constraints

- **Formal Statement:** Design a time-based key-value data structure that can store multiple values for the same key at different timestamps and retrieve the key's value at a certain timestamp. Implement the `TimeMap` class:
  - `TimeMap()`: Initializes the object.
  - `void Set(string key, string value, int timestamp)`: Stores the `key` with the `value` at the given `timestamp`.
  - `string Get(string key, int timestamp)`: Returns a value such that `set` was called previously with `timestamp_prev <= timestamp`. If multiple such values exist, return the value associated with the largest `timestamp_prev`. If no values exist matching the condition, return `""`.
- **Assumptions & Contracts:**
  - For each distinct `key`, timestamps passed to `Set` are strictly increasing: $timestamp_1 < timestamp_2 < \dots$.
  - `Get` must return the floor value (most recent prior value) relative to the queried timestamp.
  - If no timestamp exists that is $\le timestamp$, return the empty string `""`.
  - If the key has never been set, return `""` immediately.
- **Key Constraints:**
  - $1 \le key.Length, value.Length \le 100$
  - $key$ and $value$ consist of lowercase English letters and digits.
  - $1 \le timestamp \le 10^7$
  - At most $2 \times 10^5$ total calls will be made to `Set` and `Get`.
- **Senior Edge Cases to Defend:**
  - **Key Non-Existent:** Querying a key never stored must return `""` in $O(1)$ without allocating or throwing `KeyNotFoundException`.
  - **Query Earlier than Minimum Stored Timestamp:** Key exists with timestamps $[10, 20, 30]$; query `Get(key, 5)` must return `""` because no entry has timestamp $\le 5$.
  - **Exact Match on Timestamp:** Querying timestamp matching a set operation exactly must return that exact value.
  - **Query Intermediate Timestamp:** Entries at $[10, 20, 30]$; query `Get(key, 25)` must return the value at $20$.
  - **Query Timestamp Larger than All Entries:** Query `Get(key, 100)` must return the latest value at $30$.

---

### 2. Summary & Sample Input / Output

- **Conceptual Essence:** Multi-Version Concurrency Control (MVCC) snapshot store. Combine an associative map `Dictionary<string, List<(int Timestamp, string Value)>>` with **Right-Biased Floor Binary Search**. Because `Set` calls arrive in strictly increasing timestamp order, the per-key list is monotonically sorted by construction, enabling $O(\log K)$ retrieval with $O(1)$ append.
- **Sample Execution Trace:**
  - `TimeMap timeMap = new TimeMap();`
  - `timeMap.Set("foo", "bar", 1);  // store "foo" : "bar" at t = 1`
  - `timeMap.Get("foo", 1);         // returns "bar"`
  - `timeMap.Get("foo", 3);         // returns "bar" (earliest floor is t = 1)`
  - `timeMap.Set("foo", "bar2", 4); // store "foo" : "bar2" at t = 4`
  - `timeMap.Get("foo", 4);         // returns "bar2"`
  - `timeMap.Get("foo", 5);         // returns "bar2"`
  - `timeMap.Get("foo", 0);         // returns "" (no entry <= 0)`

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Think of Git commit history on a specific branch. Each `Set` operation is a new commit with an incrementing commit ID (timestamp). When a client calls `Get(key, timestamp)`, they are asking: *"What was the state of this file at 3:00 PM yesterday?"* You do not need to rewrite git history; you simply scan the commit log and find the newest commit created at or before 3:00 PM. Since commits are chronologically ordered, you binary search the commit history in logarithmic time.

#### 3.2 The Naive Bottleneck & Redundant Computation
- **Linear Reverse Scan ($O(K)$ per read):** Storing values in a list and iterating from end to beginning to find the first entry $\le timestamp$. Under $10^5$ writes to the same key, every `Get` call scans tens of thousands of items, degrading system throughput to $O(Q \times K)$.
- **Dense Timeline Map:** Storing every discrete timestamp in a hash map `Dictionary<int, string>` by copying values forward for all seconds in between. This consumes astronomical memory ($10^7$ integers per key) and is completely unscalable.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Temporal Monotonicity Invariant:**
  For any key $K$, the chronological list of entries:
  $$L = [ (t_0, v_0), (t_1, v_1), \dots, (t_{m-1}, v_{m-1}) ]$$
  satisfies $t_0 < t_1 < t_2 < \dots < t_{m-1}$ strictly.
- **The Floor Predicate Partition:**
  We seek the maximum index $p \in [0, m - 1]$ satisfying:
  $$P(idx) \equiv (L[idx].Timestamp \le \text{target})$$
  The boolean predicate array is monotonic:
  $$[\text{True}, \text{True}, \dots, \text{True}, \text{False}, \text{False}, \dots]$$
  Binary search finds the boundary in strictly $\lfloor \log_2 m \rfloor + 1$ iterations.
- **Right-Biased Binary Search Invariant:**
  - Let $mid = left + (right - left) / 2$.
  - If $L[mid].Timestamp \le target$:
    - $mid$ is a viable candidate. Record `candidateIndex = mid`.
    - Because we want the *largest* timestamp $\le target$, search rightward: $left \leftarrow mid + 1$.
  - Else ($L[mid].Timestamp > target$):
    - $mid$ is too new; discard right half: $right \leftarrow mid - 1$.
  - Upon convergence, if `candidateIndex == -1`, no valid historical record exists $\implies$ return `""`.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Binary Search Search Space Partition:
-------------------------------------------------------------------------------
Index:         0         1         2         3         4         5
Timestamp:   [ 10 ]    [ 20 ]    [ 30 ]    [ 40 ]    [ 50 ]    [ 60 ]
Target: 35

Predicate:   (t <= 35) (t <= 35) (t <= 35) (t <= 35) (t <= 35) (t <= 35)
Evaluation:    True      True      True     False     False     False
                                     ▲
                                     │
                             Target Floor Entry
                          (candidateIndex = 2 -> 30)

Binary Search Cursor Movement:
Left: 0, Right: 5  -> Mid: 2 (t=30 <= 35) -> candidate=2, Left = 3
Left: 3, Right: 5  -> Mid: 4 (t=50 > 35)  -> Right = 3
Left: 3, Right: 3  -> Mid: 3 (t=40 > 35)  -> Right = 2
Left > Right -> Terminate! Return list[2].Value
```

#### 3.5 State Transition Triggers & Decision Gates

```text
Set(key, value, timestamp):
1. Query _store.TryGetValue(key, out list).
2. If list does not exist:
   -> Allocate list = new List<(int, string)>(), insert into _store.
3. list.Add((timestamp, value)). [Guaranteed O(1) amortized append].

Get(key, timestamp):
1. Query _store.TryGetValue(key, out list).
2. If list == null or list.Count == 0: Return "".
3. If list[0].Timestamp > timestamp: Return "". [Early Exit Gate]
4. Binary Search:
   left = 0, right = list.Count - 1, candidate = -1.
   While left <= right:
     mid = left + (right - left) / 2.
     If list[mid].Timestamp <= timestamp:
        candidate = mid; left = mid + 1.
     Else:
        right = mid - 1.
5. Return candidate == -1 ? "" : list[candidate].Value.
```

#### 3.6 Concrete Step-by-Step State Trace

Given key `"stock"` with entries: `[(10, "A"), (20, "B"), (30, "C"), (40, "D")]`
Query: `Get("stock", 25)`

| Step | `left` | `right` | `mid` | `list[mid].Timestamp` | Comparison vs Target 25 | Action & Cursor Mutation | `candidate` |
| :---: | :---: | :---: | :---: | :---: | :---: | :--- | :---: |
| **Initial** | 0 | 3 | — | — | — | — | -1 |
| **Iter 1** | 0 | 3 | 1 | 20 | $20 \le 25 \implies \text{True}$ | `candidate` $\leftarrow 1$, `left` $\leftarrow 2$ | 1 |
| **Iter 2** | 2 | 3 | 2 | 30 | $30 \le 25 \implies \text{False}$| `right` $\leftarrow 1$ | 1 |
| **Exit** | 2 | 1 | — | — | `left > right` ($2 > 1$) | Loop terminates. Return `list[1].Value` | **1 ("B")** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: `Dictionary<string, List<(int, string)>>` + Binary Search (Optimal):**
  - *When to Use:* Timestamps arrive in non-decreasing order (standard contract).
  - *Memory & Cache:* Contiguous dynamic arrays provide unbeatable L1 CPU cache locality and zero tree pointer overhead.
- **Approach 2: `Dictionary<string, SortedDictionary<int, string>>` (Red-Black Tree):**
  - *When to Use:* Timestamps may arrive **out-of-order** in distributed environments with unsynchronized clock drift.
  - *Trade-Off:* Every write costs $O(\log K)$ rebalancing and allocates individual red-black tree nodes on the heap, increasing GC pressure.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Top-Level Associative Mapping:** `Dictionary<string, List<TimeEntry>>` for $O(1)$ key lookup.
2. **Sequential Append:** Leverage contract guarantee of increasing timestamps to append in $O(1)$.
3. **Floor Binary Search:** Implement robust boundary search using `left + (right - left) / 2` to prevent 32-bit signed integer overflow.
4. **Boundary Guard:** Verify `list[0].Timestamp <= timestamp` before launching search to short-circuit queries preceding all data.

#### 4.3 Alternative Approaches Analysis
- **`SortedList<int, string>`:** Maintains contiguous sorted array with binary search, but inserting out-of-order takes $O(K)$ array copy operations.
- **Linear Backward Scan:** Extremely slow for old keys with long histories.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Dictionary + List + Binary Search | Approach 2: Dictionary + SortedDictionary (Tree) | Approach 3: Linear Backward Scan |
| :--- | :--- | :--- | :--- |
| **Time (`Set`)** | $O(1)$ amortized | $O(\log K)$ | $O(1)$ |
| **Time (`Get`)** | $O(\log K)$ | $O(\log K)$ | $O(K)$ |
| **Auxiliary Space** | $O(N)$ contiguous entries | $O(N)$ node heap allocations | $O(N)$ |
| **Output Space** | $O(1)$ string reference | $O(1)$ string reference | $O(1)$ |
| **Cache Locality** | Optimal (Flat contiguous array) | Low (Scattered tree node pointers) | Optimal (Flat array scan) |
| **In-Place Mutability** | Append-only | Node balancing mutations | Append-only |
| **Streaming Suitability** | Optimal for chronological streams | Optimal for out-of-order streams | Degrades rapidly |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Problem #156 - Time Based Key-Value Store
 * ============================================================================
 * Core Pattern      : Hash Map + Monotonic List + Right-Biased Floor Binary Search
 * Time Complexity   : Set() -> O(1) amortized append
 *                     Get() -> O(log K) where K is number of entries for that key
 * Space Complexity  : O(N) total entries across all keys
 * Selection Rule    : Preferred over Red-Black tree due to contiguous memory layout
 *                     and zero-allocation queries under strictly increasing writes.
 * Defensive Traps   : 1. Prevent int overflow: mid = left + (right - left) / 2.
 *                     2. Handle target smaller than list[0] gracefully without exceptions.
 *                     3. Return string.Empty, never null.
 * ============================================================================
 */

namespace SeniorDSA.ScaleAndDesign;

public sealed class TimeMap
{
    /// <summary>
    /// Readonly record struct representing an immutable time-stamped entry.
    /// Struct eliminates heap allocation overhead per entry.
    /// </summary>
    private readonly record struct Entry(int Timestamp, string Value);

    private readonly Dictionary<string, List<Entry>> _store;

    /// <summary>
    /// Initializes the internal time-based store with default capacity.
    /// </summary>
    public TimeMap()
    {
        _store = new Dictionary<string, List<Entry>>(StringComparer.Ordinal);
    }

    /// <summary>
    /// Stores the key and value at the specified timestamp.
    /// Contract guarantees timestamps for a given key arrive in strictly increasing order.
    /// </summary>
    public void Set(string key, string value, int timestamp)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);
        ArgumentNullException.ThrowIfNull(value);

        if (!_store.TryGetValue(key, out var entries))
        {
            entries = new List<Entry>();
            _store[key] = entries;
        }

        // Invariant: Append-only operation preserves monotonic timestamp ordering
        entries.Add(new Entry(timestamp, value));
    }

    /// <summary>
    /// Retrieves the value associated with the largest timestamp_prev <= timestamp.
    /// Returns string.Empty if no such timestamp exists or key is not registered.
    /// </summary>
    public string Get(string key, int timestamp)
    {
        ArgumentException.ThrowIfNullOrEmpty(key);

        if (!_store.TryGetValue(key, out var entries) || entries.Count == 0)
        {
            return string.Empty;
        }

        // Fast Guard: Query timestamp precedes all recorded entries
        if (entries[0].Timestamp > timestamp)
        {
            return string.Empty;
        }

        // Fast Guard: Query timestamp is at or beyond the latest recorded entry
        if (entries[^1].Timestamp <= timestamp)
        {
            return entries[^1].Value;
        }

        // Right-Biased Floor Binary Search: find largest index where entries[idx].Timestamp <= timestamp
        int left = 0;
        int right = entries.Count - 1;
        int candidateIndex = -1;

        while (left <= right)
        {
            int mid = left + ((right - left) >> 1);

            if (entries[mid].Timestamp <= timestamp)
            {
                // Invariant: mid is valid; attempt to find a more recent valid entry to the right
                candidateIndex = mid;
                left = mid + 1;
            }
            else
            {
                // Invariant: mid is too new; discard right search space
                right = mid - 1;
            }
        }

        return candidateIndex != -1 ? entries[candidateIndex].Value : string.Empty;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps

1. **The Off-by-One Floor Binary Search Trap:**
   - *Trap:* Standard binary search returning `-1` on non-exact matches instead of recording `candidateIndex = mid`.
   - *Failure:* If `Set(t=10)` and `Set(t=20)` exist, calling `Get(t=15)` returns empty string rather than the valid floor value at $t=10$.
   - *Defense:* Implement right-biased search: whenever `Timestamp <= target`, record candidate and advance `left = mid + 1`.
2. **Memory Leaks via High Key Cardinality:**
   - *Trap:* Continuously creating new keys in microservices without eviction or TTL.
   - *Failure:* Memory grows unbounded until process memory is exhausted.
   - *Defense:* In real-world systems, combine `TimeMap` with an LRU key-eviction ledger or bounded capacity limits.
3. **Integer Overflow in Midpoint Calculation:**
   - *Trap:* Writing `(left + right) / 2`.
   - *Failure:* When `left + right > 2,147,483,647`, signed integer overflows to negative, throwing `IndexOutOfRangeException`.
   - *Defense:* Always use `left + (right - left) / 2` or `left + ((right - left) >> 1)`.
4. **Out-of-Order Distributed Writes Failure:**
   - *Trap:* Assuming network packets always arrive in timestamp order across distributed nodes.
   - *Failure:* If client clock skew causes $t=90$ to arrive after $t=100$, appending breaks the sorted invariant, invalidating future binary searches.
   - *Defense:* If distributed out-of-order writes are possible, use bisect insertion (`List.BinarySearch` to find insertion index) or `SortedDictionary<int, string>`.

---
---

## 157. Evaluate Division (LeetCode #399)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#graph` `#dfs-bfs` `#union-find` `#weighted-graph` `#path-multiplication` `#currency-arbitrage` |
| **LeetCode Link** | [Evaluate Division](https://leetcode.com/problems/evaluate-division/) |

### 1. Problem Detail & Constraints

- **Formal Statement:** You are given an array of variable pairs `equations` and an array of real numbers `values`, where `equations[i] = [Ai, Bi]` and `values[i]` represent the equation $Ai / Bi = values[i]$. Each $Ai$ or $Bi$ is a string representing a single variable. You are also given some `queries`, where `queries[j] = [Cj, Dj]` represents the $j$-th query where you must find the answer for $Cj / Dj = ?$. Return the answers to all queries. If a single answer cannot be determined, return `-1.0`.
- **Assumptions & Contracts:**
  - Input equations are mathematically valid and do not contain division by zero ($values[i] > 0.0$).
  - No contradictory equations exist (e.g., $a/b = 2.0$ and $a/b = 3.0$ will never co-exist).
  - The transitive property holds: if $a/b = 2$ and $b/c = 3$, then $a/c = 2 \times 3 = 6$.
  - Reciprocal property holds: if $a/b = 2$, then $b/a = 1 / 2 = 0.5$.
  - Reflexive identity holds: $a/a = 1.0$ **if and only if** $a$ was observed in at least one input equation. If variable $x$ was never mentioned in `equations`, $x/x = -1.0$.
- **Key Constraints:**
  - $1 \le equations.Length \le 20$
  - $values.Length == equations.Length$
  - $0.0 < values[i] \le 20.0$
  - $1 \le queries.Length \le 100$
  - `equations[i].Length == 2`, `queries[j].Length == 2`
  - Variable strings have length $1 \le len \le 5$ and consist of lowercase English letters.
- **Senior Edge Cases to Defend:**
  - **Unregistered Query Variables:** Query contains a variable that never appeared in `equations` (e.g., $x / a$ where $x$ is unknown). Must return `-1.0` immediately.
  - **Reflexive Query with Unknown Variable:** Query $x / x$ where $x$ never appeared. Must return `-1.0`, **not** $1.0$.
  - **Disconnected Graph Components:** $a/b = 2.0$ and $c/d = 3.0$. Query $a / c$ has no connecting path. Must return `-1.0`.
  - **Immediate Self-Division with Known Variable:** $a / a$ where $a$ exists in equations. Must return `1.0`.
  - **Long Transitive Chains:** $a/b=2, b/c=3, c/d=4, d/e=5 \implies a/e = 120.0$. Must accumulate products without floating-point precision degradation.

---

### 2. Summary & Sample Input / Output

- **Conceptual Essence:** Directed Weighted Graph Traversal & Transitive Path Product. Model each variable as a graph node. An equation $A / B = k$ establishes two directed edges:
  1. Forward edge: $A \xrightarrow{\times k} B$ (meaning $A = k \cdot B \implies A / B = k$).
  2. Reciprocal edge: $B \xrightarrow{\times (1/k)} A$ (meaning $B / A = 1 / k$).
  Evaluating $C / D$ is equivalent to finding any simple directed path from $C$ to $D$ and multiplying the edge weights along the path:
  $$\frac{C}{D} = \frac{C}{X_1} \times \frac{X_1}{X_2} \times \dots \times \frac{X_k}{D}$$
- **Sample 1:**
  - **Input:** `equations = [["a","b"],["b","c"]]`, `values = [2.0, 3.0]`, `queries = [["a","c"],["b","a"],["a","e"],["a","a"],["x","x"]]`
  - **Output:** `[6.0, 0.5, -1.0, 1.0, -1.0]`
  - **Explanation:**
    - $a/c = (a/b) \times (b/c) = 2.0 \times 3.0 = 6.0$
    - $b/a = 1 / (a/b) = 1 / 2.0 = 0.5$
    - $a/e = -1.0$ (variable $e$ does not exist)
    - $a/a = 1.0$ ($a$ exists in graph)
    - $x/x = -1.0$ ($x$ does not exist in graph)

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine an international currency exchange desk. You hold US Dollars ($USD$) and want Japanese Yen ($JPY$). The desk doesn't have a direct conversion rate for $USD \to JPY$, but it has:
$$USD \xrightarrow{\times 0.92} EUR \quad \text{and} \quad EUR \xrightarrow{\times 160.0} JPY$$
By chaining the conversions along the path, your total multiplier is $0.92 \times 160.0 = 147.2$. If there is no sequence of trades connecting two currencies, no trade can occur (return `-1.0`). If someone asks for the exchange rate of Monopoly money to Monopoly money, the teller rejects it because Monopoly money isn't on the books!

#### 3.2 The Naive Bottleneck & Redundant Computation
- Running an unweighted All-Pairs Shortest Path (Floyd-Warshall) across all possible variable names when only a handful of queries are requested.
- Forgetting a `visited` set during DFS/BFS, causing the search cursor to bounce infinitely between reciprocal edges $A \leftrightarrow B$ until a stack overflow occurs.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Consistency Guarantee:** The problem guarantees equations are mathematically consistent. Therefore, **any** simple path from $C$ to $D$ will yield the exact same cumulative product. We do not need Dijkstra's algorithm; a standard Depth-First Search (DFS) or Breadth-First Search (BFS) with a visited ledger is guaranteed to find the exact correct answer.
- **Weighted Disjoint Set Union (DSU) Invariant (Staff Alternative):**
  Instead of path search per query, we can maintain a Disjoint Set where each node stores:
  1. `parent[x]`: The representative root of the connected component.
  2. `weight[x]`: The ratio $\frac{x}{\text{parent}[x]}$.
  During `Find(x)`, path compression updates the weights multiplicatively:
  $$\text{weight}[x] \leftarrow \text{weight}[x] \times \text{weight}[\text{oldParent}]$$
  When querying $C / D$:
  - If $C$ and $D$ do not share the same root: Return `-1.0` (disconnected).
  - If they share the same root $R$:
    $$\frac{C}{R} = \text{weight}[C], \quad \frac{D}{R} = \text{weight}[D] \implies \frac{C}{D} = \frac{C/R}{D/R} = \frac{\text{weight}[C]}{\text{weight}[D]}$$
  Query time drops to $O(\alpha(V)) \approx O(1)$!

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Graph Adjacency Representation:
-------------------------------------------------------------------------------
Equations: a / b = 2.0,  b / c = 3.0

       2.0                   3.0
  [ a ] ───> [ b ]        [ b ] ───> [ c ]
  [ a ] <─── [ b ]        [ b ] <─── [ c ]
       0.5                   0.333...

Query: a / c
Path:  [ a ] ─────────> [ b ] ─────────> [ c ]
Weight:         2.0              3.0
Product: 2.0 * 3.0 = 6.0

Weighted Union-Find Tree Representation:
           [ Root: c ]
          ↗ (weight: 3.0) 
       [ b ]              weight[b] = b / c = 3.0
      ↗ (weight: 2.0)
   [ a ]                  After Path Compression:
                          weight[a] = a / c = (a/b) * (b/c) = 6.0
                          parent[a] = c
Query a / b: weight[a] / weight[b] = 6.0 / 3.0 = 2.0
```

#### 3.5 State Transition Triggers & Decision Gates

```text
Per Query: (src, dst)
1. Verification Gate:
   If src not in Graph OR dst not in Graph: Return -1.0.
2. Identity Gate:
   If src == dst: Return 1.0.
3. Search Initialization:
   visited = new HashSet<string>().
4. DFS(curr, target, runningProduct):
   a. If curr == target: Return runningProduct.
   b. visited.Add(curr).
   c. For each (neighbor, edgeWeight) in graph[curr]:
      If neighbor not in visited:
         result = DFS(neighbor, target, runningProduct * edgeWeight).
         If result != -1.0: Return result.
   d. Return -1.0.
```

#### 3.6 Concrete Step-by-Step State Trace

Given graph from $a/b = 2.0, b/c = 3.0$. Query: $a / c$

| Call Depth | Current Node (`curr`) | Target Node (`target`) | Running Product | Visited Set | Neighbors Inspected | Action & Decision | Return Value |
| :---: | :---: | :---: | :---: | :--- | :--- | :--- | :---: |
| **Depth 1** | `"a"` | `"c"` | 1.0 | `{"a"}` | `("b", 2.0)` | Unvisited $\to$ Recurse Depth 2 | **6.0** |
| **Depth 2** | `"b"` | `"c"` | $1.0 \times 2.0 = 2.0$ | `{"a", "b"}` | `("a", 0.5)`<br>`("c", 3.0)` | `"a"` already visited $\to$ skip.<br>`"c"` unvisited $\to$ Recurse Depth 3 | **6.0** |
| **Depth 3** | `"c"` | `"c"` | $2.0 \times 3.0 = 6.0$ | `{"a", "b", "c"}` | None | **Hit Target!** `curr == target` | **6.0** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Graph Adjacency List + DFS (Interview Gold Standard):**
  - *When to Use:* Small to moderate query volume ($Q \le 100$), fast implementation speed, easy to reason about cycle prevention.
  - *Complexity:* Graph construction is $O(E)$; each query takes $O(V + E)$ in the worst case.
- **Approach 2: Weighted Disjoint Set Union (Staff / Scale Signature):**
  - *When to Use:* Massive query volumes ($Q \ge 10^5$) where queries far outnumber equations.
  - *Complexity:* Construction $O(E \cdot \alpha(V))$; queries execute in $O(\alpha(V)) \approx O(1)$ time!

#### 4.2 Step-by-Step Natural Progression Flow
1. **Graph Scaffolding:** Build `Dictionary<string, List<(string Neighbor, double Weight)>>`.
2. **Bidirectional Ingestion:** For each equation $(u, v, w)$, insert forward $(v, w)$ and reverse $(u, 1.0 / w)$.
3. **Query Engine:** Loop through queries. Validate existence of both nodes in the dictionary keys.
4. **Traversal & Cycle Shield:** Use `HashSet<string>` to track visited nodes during DFS to prevent cycling back through reciprocal edges.
5. **Output Matrix:** Collect doubles into `double[]` output array.

#### 4.3 Alternative Approaches Analysis
- **Floyd-Warshall ($O(V^3)$):** Computes all-pairs products. Inefficient when $V$ is large or queries are few.
- **BFS (Breadth-First Search):** Equivalent asymptotic time to DFS. Finds shortest path in edge count, but because equations are mathematically consistent, edge count doesn't alter the product. DFS uses less memory overhead on sparse graphs.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Build) | Time (Per Query) | Auxiliary Space | Output Space | Cache Locality | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Graph + DFS** | $O(E)$ | $O(V + E)$ | $O(V + E)$ graph | $O(Q)$ doubles | Moderate (Hash map buckets) | Excellent |
| **2. Weighted Union-Find** | $O(E \alpha(V))$ | $O(\alpha(V)) \approx O(1)$ | $O(V)$ DSU maps | $O(Q)$ doubles | High (Flat indexable arrays) | Optimal for online queries |
| **3. Floyd-Warshall** | $O(V^3)$ | $O(1)$ | $O(V^2)$ dense matrix | $O(Q)$ doubles | Optimal (2D array) | Unusable for dynamic graphs |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Problem #157 - Evaluate Division
 * ============================================================================
 * Core Pattern      : Directed Weighted Graph + DFS Path Multiplication
 * Time Complexity   : Construction: O(E) where E = equations.Count
 *                     Query: O(Q * (V + E)) where Q = queries.Count
 * Space Complexity  : O(V + E) auxiliary space for graph representation
 * Selection Rule    : Default to DFS (Approach 1) for interview clarity and correctness.
 *                     Transition to Weighted DSU (Approach 2) when query volume Q >> E.
 * Defensive Traps   : 1. x / x where x was NEVER in equations MUST return -1.0, not 1.0.
 *                     2. Always register reciprocal edge with 1.0 / weight.
 *                     3. Guard against infinite cycles via HashSet visited tracking.
 * ============================================================================
 */

namespace SeniorDSA.ScaleAndDesign;

public class SolutionEvaluateDivision
{
    /// <summary>
    /// Approach 1: Graph Adjacency List with Recursive DFS.
    /// Builds a directed weighted graph and explores simple paths to evaluate transitive quotients.
    /// </summary>
    public double[] CalcEquation(
        IList<IList<string>> equations, 
        double[] values, 
        IList<IList<string>> queries)
    {
        // Guard Clauses
        if (equations == null || values == null || queries == null || equations.Count != values.Length)
        {
            throw new ArgumentException("Invalid equation or query parameters.");
        }

        // Build Adjacency Graph: node -> list of (neighbor, weight)
        var graph = new Dictionary<string, List<(string Neighbor, double Weight)>>(StringComparer.Ordinal);

        for (int i = 0; i < equations.Count; i++)
        {
            string u = equations[i][0];
            string v = equations[i][1];
            double weight = values[i];

            if (!graph.TryGetValue(u, out var uNeighbors))
            {
                uNeighbors = new List<(string, double)>();
                graph[u] = uNeighbors;
            }

            if (!graph.TryGetValue(v, out var vNeighbors))
            {
                vNeighbors = new List<(string, double)>();
                graph[v] = vNeighbors;
            }

            // Invariant: Division u / v = w implies v / u = 1.0 / w
            uNeighbors.Add((v, weight));
            vNeighbors.Add((u, 1.0 / weight));
        }

        double[] results = new double[queries.Count];

        for (int i = 0; i < queries.Count; i++)
        {
            string dividend = queries[i][0];
            string divisor = queries[i][1];

            // Decision Gate 1: If either variable was never observed, calculation is impossible
            if (!graph.ContainsKey(dividend) || !graph.ContainsKey(divisor))
            {
                results[i] = -1.0;
                continue;
            }

            // Decision Gate 2: Reflexive identity for known variables
            if (string.Equals(dividend, divisor, StringComparison.Ordinal))
            {
                results[i] = 1.0;
                continue;
            }

            // Decision Gate 3: Explore graph for transitive path product
            var visited = new HashSet<string>(StringComparer.Ordinal);
            results[i] = DfsMultiply(dividend, divisor, 1.0, visited, graph);
        }

        return results;
    }

    private static double DfsMultiply(
        string current, 
        string target, 
        double runningProduct, 
        HashSet<string> visited, 
        Dictionary<string, List<(string Neighbor, double Weight)>> graph)
    {
        // Terminal Base Case: Target node reached
        if (string.Equals(current, target, StringComparison.Ordinal))
        {
            return runningProduct;
        }

        visited.Add(current);

        foreach (var (neighbor, edgeWeight) in graph[current])
        {
            if (!visited.Contains(neighbor))
            {
                double pathResult = DfsMultiply(neighbor, target, runningProduct * edgeWeight, visited, graph);
                
                // If a valid path was found downstream, propagate it immediately
                if (pathResult != -1.0)
                {
                    return pathResult;
                }
            }
        }

        // Dead end: No path from current to target
        return -1.0;
    }
}

/// <summary>
/// Approach 2: Weighted Disjoint Set Union (Union-Find with Multiplicative Weights).
/// Staff-level signature optimizing query response time to O(alpha(V)) ~ O(1).
/// </summary>
public sealed class SolutionEvaluateDivisionUnionFind
{
    private sealed class WeightedDSU
    {
        private readonly Dictionary<string, string> _parent = new(StringComparer.Ordinal);
        private readonly Dictionary<string, double> _ratioToParent = new(StringComparer.Ordinal);

        public void Register(string x)
        {
            if (!_parent.ContainsKey(x))
            {
                _parent[x] = x;
                _ratioToParent[x] = 1.0;
            }
        }

        public bool Contains(string x) => _parent.ContainsKey(x);

        /// <summary>
        /// Finds the component root while performing multiplicative path compression.
        /// Invariant: ratioToParent[x] maintains the exact ratio (x / root).
        /// </summary>
        public string Find(string x)
        {
            if (_parent[x] != x)
            {
                string originalParent = _parent[x];
                string root = Find(originalParent);

                // Multiplicative Path Compression: (x / root) = (x / parent) * (parent / root)
                _ratioToParent[x] *= _ratioToParent[originalParent];
                _parent[x] = root;
            }

            return _parent[x];
        }

        /// <summary>
        /// Merges sets containing x and y such that x / y = value.
        /// </summary>
        public void Union(string x, string y, double value)
        {
            Register(x);
            Register(y);

            string rootX = Find(x);
            string rootY = Find(y);

            if (!string.Equals(rootX, rootY, StringComparison.Ordinal))
            {
                // Connect rootX -> rootY
                // We know: x = rootX * ratio[x]  => rootX = x / ratio[x]
                //          y = rootY * ratio[y]  => rootY = y / ratio[y]
                // Given: x / y = value
                // Ratio rootX / rootY = (x / ratio[x]) / (y / ratio[y]) = (x / y) * (ratio[y] / ratio[x])
                //                     = value * ratio[y] / ratio[x]
                _parent[rootX] = rootY;
                _ratioToParent[rootX] = value * _ratioToParent[y] / _ratioToParent[x];
            }
        }

        public double Query(string x, string y)
        {
            if (!Contains(x) || !Contains(y))
            {
                return -1.0;
            }

            string rootX = Find(x);
            string rootY = Find(y);

            if (!string.Equals(rootX, rootY, StringComparison.Ordinal))
            {
                return -1.0; // Different connected components
            }

            // Both share root R: (x / R) / (y / R) = x / y
            return _ratioToParent[x] / _ratioToParent[y];
        }
    }

    public double[] CalcEquation(
        IList<IList<string>> equations, 
        double[] values, 
        IList<IList<string>> queries)
    {
        var dsu = new WeightedDSU();

        for (int i = 0; i < equations.Count; i++)
        {
            dsu.Union(equations[i][0], equations[i][1], values[i]);
        }

        double[] results = new double[queries.Count];
        for (int i = 0; i < queries.Count; i++)
        {
            results[i] = dsu.Query(queries[i][0], queries[i][1]);
        }

        return results;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps

1. **The Unseen Variable Reflexive Trap ($x / x$):**
   - *Trap:* Writing `if (dividend == divisor) return 1.0;` before checking if `graph.ContainsKey(dividend)`.
   - *Failure:* If query asks for `["unknown", "unknown"]`, returning `1.0` directly violates the contract. Must return `-1.0`.
   - *Defense:* Always verify existence in the graph ledger *prior* to returning the reflexive `1.0`.
2. **Missing `visited` Set in DFS (Infinite Loop):**
   - *Trap:* Forgetting to maintain `HashSet<string> visited`.
   - *Failure:* Since edges are bidirectional ($a \to b$ and $b \to a$), DFS bounces infinitely between $a$ and $b$, throwing a fatal `StackOverflowException`.
   - *Defense:* Mark each node in `visited` immediately upon entry; only recurse on unvisited neighbors.
3. **Floating Point Underflow / Division by Zero:**
   - *Trap:* Storing `0.0` or unchecked reciprocal `1.0 / weight`.
   - *Failure:* While constraints guarantee $values[i] > 0$, defensive enterprise code must validate `weight > 1e-9` before computing reciprocal to avoid `double.PositiveInfinity` or `NaN`.
4. **Weighted DSU Multiplicative Inversion Error:**
   - *Trap:* Incorrectly calculating `_ratioToParent[rootX]` during `Union(x, y, val)`.
   - *Failure:* Inverting the fraction leads to reciprocal answers ($0.5$ instead of $2.0$).
   - *Defense:* Derive the algebra explicitly: $rootX / rootY = (x / y) \times \frac{y / rootY}{x / rootX} = \text{val} \times \frac{\text{ratio}[y]}{\text{ratio}[x]}$.

---
---

## 158. Longest Increasing Path in a Matrix (LeetCode #329)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#graph` `#matrix-dfs` `#memoization` `#topological-sort` `#dag` `#dynamic-programming` |
| **LeetCode Link** | [Longest Increasing Path in a Matrix](https://leetcode.com/problems/longest-increasing-path-in-a-matrix/) |

### 1. Problem Detail & Constraints

- **Formal Statement:** Given an $m \times n$ integers matrix `matrix`, return the length of the **longest increasing path** in `matrix`. From each cell, you can either move in four directions: up, down, left, or right. You **may not** move diagonally or move outside the boundary (i.e., wrap-around is not allowed).
- **Assumptions & Contracts:**
  - A valid path must be **strictly increasing**: $matrix[r_{k+1}][c_{k+1}] > matrix[r_k][c_k]$.
  - The path length is measured as the **number of cells** visited (a single cell has path length $1$).
  - Cells cannot be revisited along the same path because strict monotonicity mathematically precludes cycles.
- **Key Constraints:**
  - $m == matrix.Length$
  - $n == matrix[i].Length$
  - $1 \le m, n \le 200$
  - $0 \le matrix[i][j] \le 2^{31} - 1$
  - Total cells $M \times N \le 40,000$.
- **Senior Edge Cases to Defend:**
  - **Single Element Matrix ($1 \times 1$):** `[[7]]` returns `1`.
  - **All Elements Identical:** `[[5, 5], [5, 5]]`. No strictly increasing moves exist from any cell. Maximum path length is `1`.
  - **Monotonic Snake / Spiral:** A matrix where cells increase sequentially from $1$ to $M \times N$ along a snake path. Must return $M \times N = 40,000$ without encountering stack overflow.
  - **Isolated Peaks and Valleys:** Cells surrounded on all 4 sides by smaller or equal elements (local maxima) must evaluate to base length $1$.

---

### 2. Summary & Sample Input / Output

- **Conceptual Essence:** Directed Acyclic Graph (DAG) Longest Path via Dynamic Programming with Memoization. The strict inequality ($matrix[next] > matrix[curr]$) guarantees that no directed cycles can ever exist. Every matrix cell represents a DAG vertex with at most 4 outgoing directed edges. The problem reduces to finding the diameter (longest path) in a DAG, solved in optimal $O(M \times N)$ time via **Top-Down DFS + 2D Memoization** or **Bottom-Up Kahn's Topological Sort**.
- **Sample 1:**
  - **Input:**
    ```text
    matrix = [
      [9, 9, 4],
      [6, 6, 8],
      [2, 1, 1]
    ]
    ```
  - **Output:** `4`
  - **Explanation:** The longest increasing path is `[1, 2, 6, 9]`. (Moving: $(2, 1) \to (2, 0) \to (1, 0) \to (0, 0)$).
- **Sample 2:**
  - **Input:**
    ```text
    matrix = [
      [3, 4, 5],
      [3, 2, 6],
      [2, 2, 1]
    ]
    ```
  - **Output:** `4`
  - **Explanation:** Longest path is `[3, 4, 5, 6]`. Moving diagonally is forbidden.

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a mountain hiker walking along elevation contours. You are on a strict training regime: every step you take must be strictly uphill. Because you can only gain elevation, you can never possibly return to any spot you have previously visited. It is physically impossible to walk in a loop! Therefore, from any starting coordinates $(r, c)$, the maximum number of uphill steps you can take is fixed and deterministic. Once you calculate the uphill potential from a peak or ridge, you write it on your topographic map (`memo[r, c]`). If you ever approach that spot from below in another trail, you immediately read the cached number instead of re-climbing the mountain.

#### 3.2 The Naive Bottleneck & Redundant Computation
- **Unmemoized Brute Force DFS:** Exploring all increasing paths from every cell without caching.
  - At each cell, there are up to 4 branching choices.
  - Worst-case time complexity: $O(4^{M \times N})$.
  - For a modest $200 \times 200$ grid ($40,000$ cells), $4^{40,000}$ exceeds the number of atoms in the universe, causing immediate Time Limit Exceeded (TLE).
- **Redundant Visited Tracking:** Allocating an auxiliary `bool[m, n] visited` matrix. Because the path is strictly increasing, you can never step back into an already-visited smaller cell. Tracking visited state is completely redundant and wastes cache lines.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **The Implicit DAG Invariant:**
  Define directed graph $G = (V, E)$ where:
  $$V = \{ (r, c) \mid 0 \le r < m, 0 \le c < n \}$$
  $$E = \{ ((r, c), (nr, nc)) \mid (nr, nc) \in \text{Neighbors}(r, c) \land matrix[nr][nc] > matrix[r][c] \}$$
  Since edges only point from strictly smaller to strictly larger values:
  $$(u, v) \in E \implies \text{val}(u) < \text{val}(v)$$
  No cycle can exist: a path $v_1 \to v_2 \to \dots \to v_k \to v_1$ would imply $\text{val}(v_1) < \text{val}(v_1)$, a contradiction.
- **Bellman's Principle of Optimal Substructure:**
  Let $L(r, c)$ be the length of the longest increasing path starting at $(r, c)$.
  $$L(r, c) = 1 + \max \Big( \{ L(nr, nc) \mid (nr, nc) \in \text{Neighbors}(r, c), matrix[nr][nc] > matrix[r][c] \} \cup \{ 0 \} \Big)$$
- **Memoization Guarantee:**
  Each state $(r, c)$ is computed exactly once and cached in `memo[r, c]`.
  Each directed edge is evaluated at most once.
  Total Time: $O(V + E) = O(M \cdot N + 4 \cdot M \cdot N) = O(M \cdot N)$.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Implicit Directed Acyclic Graph (DAG) Overlay:
-------------------------------------------------------------------------------
Matrix:                       Directed Graph (Edges point to STRICTLY GREATER):
[ 9 ]  [ 9 ]  [ 4 ]           (9) <─── (6) <─── (2) <─── (1)
  ▲      ▲      ▲                           ▲
  │      │      │                           │
[ 6 ]  [ 6 ]  [ 8 ]                        (1)
  ▲             ▲
  │             │             Longest Path: 1 -> 2 -> 6 -> 9 (Length = 4)
[ 2 ]  [ 1 ]──>[ 1 ]

Memoization Table State Transition:
memo[r, c] = 0  => Unvisited state (Subproblem uncomputed)
memo[r, c] > 0  => Settled state (Maximum path length from this cell is immutable)
```

#### 3.5 State Transition Triggers & Decision Gates

```text
Function ComputeLIP(r, c):
1. Memoization Hit Gate:
   If memo[r, c] > 0: Return memo[r, c].
2. Base Potential:
   maxPath = 1 (every cell is a valid path of length 1).
3. 4-Directional Exploration:
   For each (dr, dc) in [(-1, 0), (1, 0), (0, -1), (0, 1)]:
     nr = r + dr, nc = c + dc.
     If nr, nc within bounds AND matrix[nr][nc] > matrix[r][c]:
        subLength = 1 + ComputeLIP(nr, nc).
        maxPath = Max(maxPath, subLength).
4. Settle State:
   memo[r, c] = maxPath.
   Return maxPath.
```

#### 3.6 Concrete Step-by-Step State Trace

Given matrix $3 \times 3$:
```text
[9, 9, 4]
[6, 6, 8]
[2, 1, 1]
```
Tracing evaluation from cell $(0, 0) = 9$:
1. $Dfs(0, 0)$ [val 9]: All neighbors are $\le 9$ ($9 \not> 9, 6 \not> 9$). No valid moves. `memo[0, 0] = 1`.
2. $Dfs(1, 0)$ [val 6]: Valid neighbor $(0, 0)$ has val 9. $1 + memo[0, 0] = 1 + 1 = 2$. `memo[1, 0] = 2`.
3. $Dfs(2, 0)$ [val 2]: Valid neighbor $(1, 0)$ has val 6. $1 + memo[1, 0] = 1 + 2 = 3$. `memo[2, 0] = 3`.
4. $Dfs(2, 1)$ [val 1]: Valid neighbor $(2, 0)$ has val 2. $1 + memo[2, 0] = 1 + 3 = 4$. `memo[2, 1] = 4`.

Final `memo` matrix populated during full grid iteration:
```text
memo = [
  [1, 1, 2],
  [2, 2, 1],
  [3, 4, 2]
]
```
Global Maximum: $\max_{(r, c)} memo[r, c] = 4$.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Top-Down DFS with 2D Memoization Table (Gold Standard):**
  - *When to Use:* Default choice in technical interviews. Minimal boilerplate, highly intuitive, zero graph building overhead.
  - *Memory:* $O(M \times N)$ memo table + call stack.
- **Approach 2: Bottom-Up Kahn's Topological Sort (Staff Alternative):**
  - *When to Use:* Environments where recursion depth is strictly constrained (e.g., embedded systems, deep call stack prevention) or when parallel wavefront execution is desired.
  - *Mechanism:* Calculate out-degrees of all cells (count of larger neighbors). Cells with out-degree 0 are local peaks (leaves). Peel leaves level-by-level using a queue. Number of queue peeling layers is the longest path length!

#### 4.2 Step-by-Step Natural Progression Flow
1. **Dimension Verification:** Check for null or empty rows/columns.
2. **Memo Matrix Initialization:** Allocate `int[m, n] memo`. In C#, integer arrays default to `0`, which perfectly encodes the "uncomputed" state since all valid paths have length $\ge 1$.
3. **Global Scan:** Iterate $r \in [0, m - 1]$ and $c \in [0, n - 1]$. Call $Dfs(r, c)$ and track `Math.Max(globalMax, Dfs(r, c))`.
4. **Boundary & Strict Monotonicity Guards:** Within DFS, check bounds $0 \le nr < m$ and $0 \le nc < n$ and condition $matrix[nr][nc] > matrix[r][c]$.

#### 4.3 Alternative Approaches Analysis
- **Dynamic Programming by Value Sorting:** Sort all $M \times N$ cells by value in $O(M N \log(M N))$ time. Iterate cells in descending order of value, relaxing neighbor paths. Flaw: The sorting step adds an unnecessary $O(M N \log(M N))$ time penalty; DFS + memoization solves it in pure linear $O(M N)$ time.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Memoized DFS** | $O(M N)$ | $O(M N)$ | $O(M N)$ | $O(M N)$ memo + stack | $O(1)$ | High (Direct 2D array indexing) | Poor (Requires static matrix) |
| **2. Kahn's Topological Sort** | $O(M N)$ | $O(M N)$ | $O(M N)$ | $O(M N)$ degrees + queue | $O(1)$ | Moderate (Queue traversal) | Poor |
| **3. Coordinate Value Sort**| $O(M N \log(M N))$ | $O(M N \log(M N))$ | $O(M N \log(M N))$ | $O(M N)$ sorted list | $O(1)$ | Low (Indirect coordinate access)| Poor |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Problem #158 - Longest Increasing Path in a Matrix
 * ============================================================================
 * Core Pattern      : Implicit DAG Top-Down DFS with 2D Tabular Memoization
 * Time Complexity   : O(M * N) visiting each state and edge at most once
 * Space Complexity  : O(M * N) auxiliary space for memo matrix + recursion stack
 * Selection Rule    : Preferred over unmemoized DFS (O(4^(M*N)) TLE) and value sorting
 *                     (O(MN log MN)). Zero visited array needed due to strict DAG property.
 * Defensive Traps   : 1. Strict inequality: Must be matrix[nr][nc] > matrix[r][c], NOT >=.
 *                     2. Base path length is 1, not 0 (a single cell is a path of length 1).
 *                     3. Do not allocate visited matrix (strict monotonicity prevents cycles).
 * ============================================================================
 */

namespace SeniorDSA.ScaleAndDesign;

public class SolutionLongestIncreasingPath
{
    // Cardinal Direction Vectors: Up, Down, Left, Right
    private static readonly int[] RowDelta = { -1, 1, 0, 0 };
    private static readonly int[] ColDelta = { 0, 0, -1, 1 };

    /// <summary>
    /// Computes the length of the longest strictly increasing path in the given matrix.
    /// Operates in optimal O(M * N) time and O(M * N) space.
    /// </summary>
    /// <param name="matrix">2D rectangular grid of integers.</param>
    /// <returns>Maximum path length.</returns>
    public int LongestIncreasingPath(int[][] matrix)
    {
        // Guard Clauses
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0)
        {
            return 0;
        }

        int rows = matrix.Length;
        int cols = matrix[0].Length;

        // Memoization Table: memo[r, c] stores the longest increasing path starting at (r, c)
        // Default initialized to 0, representing unvisited states.
        int[,] memo = new int[rows, cols];
        int globalMaxPath = 0;

        // Traversal Invariant: Scan every cell as a prospective path head
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                int currentPathLength = ComputeLongestPathFrom(r, c, matrix, memo, rows, cols);
                if (currentPathLength > globalMaxPath)
                {
                    globalMaxPath = currentPathLength;
                }
            }
        }

        return globalMaxPath;
    }

    /// <summary>
    /// Recursive DFS with memoization. Returns the longest strictly increasing path starting at (r, c).
    /// </summary>
    private static int ComputeLongestPathFrom(
        int r, 
        int c, 
        int[][] matrix, 
        int[,] memo, 
        int rows, 
        int cols)
    {
        // Decision Gate 1: Memoization Hit (State already computed)
        if (memo[r, c] != 0)
        {
            return memo[r, c];
        }

        // Base Potential: A singleton cell with no valid increasing neighbors has length 1
        int maxSubPath = 1;
        int currentVal = matrix[r][c];

        // Decision Gate 2: Explore 4 cardinal adjacent neighbors
        for (int d = 0; d < 4; d++)
        {
            int nr = r + RowDelta[d];
            int nc = c + ColDelta[d];

            // Boundary & Strict Monotonicity Guard
            // Invariant: Only step into cells with strictly greater elevation
            if (nr >= 0 && nr < rows && nc >= 0 && nc < cols && matrix[nr][nc] > currentVal)
            {
                int candidatePath = 1 + ComputeLongestPathFrom(nr, nc, matrix, memo, rows, cols);
                if (candidatePath > maxSubPath)
                {
                    maxSubPath = candidatePath;
                }
            }
        }

        // State Settlement: Cache optimal result before returning
        memo[r, c] = maxSubPath;
        return maxSubPath;
    }
}

/// <summary>
/// Approach 2: Bottom-Up Kahn's Topological Sort (Out-Degree Wavefront Peeling).
/// Eliminates recursion call stack entirely for environments with restricted stack limits.
/// </summary>
public sealed class SolutionLongestIncreasingPathTopological
{
    private static readonly int[] RowDelta = { -1, 1, 0, 0 };
    private static readonly int[] ColDelta = { 0, 0, -1, 1 };

    public int LongestIncreasingPath(int[][] matrix)
    {
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0)
        {
            return 0;
        }

        int rows = matrix.Length;
        int cols = matrix[0].Length;

        // Out-degree matrix: number of adjacent cells with strictly greater values
        int[,] outDegree = new int[rows, cols];

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                int currentVal = matrix[r][c];
                for (int d = 0; d < 4; d++)
                {
                    int nr = r + RowDelta[d];
                    int nc = c + ColDelta[d];
                    if (nr >= 0 && nr < rows && nc >= 0 && nc < cols && matrix[nr][nc] > currentVal)
                    {
                        outDegree[r, c]++;
                    }
                }
            }
        }

        // Enqueue all leaves (cells with out-degree 0: local maximums)
        var queue = new Queue<(int Row, int Col)>();
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                if (outDegree[r, c] == 0)
                {
                    queue.Enqueue((r, c));
                }
            }
        }

        int pathLength = 0;

        // Peel DAG levels backward from peaks to valleys
        while (queue.Count > 0)
        {
            pathLength++;
            int levelSize = queue.Count;

            for (int i = 0; i < levelSize; i++)
            {
                var (r, c) = queue.Dequeue();
                int currentVal = matrix[r][c];

                // Check cells that could step into (r, c): strictly smaller neighbors
                for (int d = 0; d < 4; d++)
                {
                    int nr = r + RowDelta[d];
                    int nc = c + ColDelta[d];

                    if (nr >= 0 && nr < rows && nc >= 0 && nc < cols && matrix[nr][nc] < currentVal)
                    {
                        outDegree[nr, nc]--;
                        if (outDegree[nr, nc] == 0)
                        {
                            queue.Enqueue((nr, nc));
                        }
                    }
                }
            }
        }

        return pathLength;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps

1. **The Redundant `visited` Matrix Bug:**
   - *Trap:* Adding a `bool[,] visited` array to prevent cycles.
   - *Failure:* If cell $A$ visits cell $B$, marking $B$ as visited prevents another path from traversing $B$, prematurely aborting optimal longer paths. Unmarking $B$ upon return degrades runtime back to exponential $O(4^{MN})$.
   - *Defense:* Recognize that strict monotonicity ($matrix[next] > matrix[curr]$) guarantees an acyclic DAG. A `visited` matrix is neither needed nor correct; `memo` alone handles caching.
2. **Weak Inequality Trap ($\ge$ vs $>$):**
   - *Trap:* Checking `matrix[nr][nc] >= matrix[r][c]`.
   - *Failure:* If equal adjacent values exist (e.g. `[2, 2]`), allowing $\ge$ introduces cycles between identical elements ($2 \to 2 \to 2 \dots$), causing infinite loops and stack overflow.
   - *Defense:* The problem statement strictly demands *increasing* paths: condition must be strictly `>`.
3. **Stack Overflow on Massive Snake Grids in DFS:**
   - *Trap:* Relying on recursive DFS on a $200 \times 200$ grid configured as a monotonic snake.
   - *Failure:* Recursion depth reaches $40,000$ frames. In C#, the default thread stack size is 1 MB, which typically crashes around ~15,000 frames.
   - *Defense:* For mission-critical large matrices, implement Kahn's Topological Sort (Approach 2) which uses heap-allocated queue memory, completely immune to stack overflow.
4. **Base Case Off-By-One:**
   - *Trap:* Initializing `maxSubPath = 0`.
   - *Failure:* A single isolated cell with no valid neighbors returns `0` instead of `1`.
   - *Defense:* Initialize `maxSubPath = 1` because every individual cell forms a valid increasing path of length 1.

---

## 159. Maximum Profit in Job Scheduling (LeetCode #1235)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#dynamic-programming` `#binary-search` `#interval-scheduling` `#memoization` `#sorting` |
| **LeetCode Link** | [Maximum Profit in Job Scheduling](https://leetcode.com/problems/maximum-profit-in-job-scheduling/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** We have $n$ jobs, where every job is scheduled to run from `startTime[i]` to `endTime[i]`, yielding a profit of `profit[i]`. Given the arrays `startTime`, `endTime`, and `profit`, return the maximum profit achievable from a subset of non-overlapping jobs. If a chosen job ends at timestamp $X$, another chosen job may start at timestamp $X$ (boundary touching is valid and non-conflicting).
- **Assumptions & Contracts:**
  - Input arrays `startTime`, `endTime`, and `profit` all share identical length $n$.
  - A job is defined as a half-open interval $[start, end)$ with profit $p$. Two jobs $A$ and $B$ are non-overlapping if and only if $A.end \le B.start$ or $B.end \le A.start$.
  - The return value is the global maximum profit obtainable from any valid non-overlapping subset of jobs.
- **Key Constraints:**
  - $1 \le n == 	ext{startTime.Length} == 	ext{endTime.Length} == 	ext{profit.Length} \le 5 	imes 10^4$
  - $1 \le 	ext{startTime}[i] < 	ext{endTime}[i] \le 10^9$
  - $1 \le 	ext{profit}[i] \le 10^4$
- **Senior Edge Cases to Defend:**
  - **Boundary Touching:** Job A ends at time $t$ and Job B starts at time $t$. Both must be permitted to execute consecutively; the query must locate jobs satisfying $endTime \le startTime$, not $endTime < startTime$.
  - **Identical Start or End Times:** Multiple jobs sharing the exact same start or end times with differing durations and profits.
  - **Sparsely Dispersed Timestamps ($10^9$):** Absolute time scale reaches $10^9$; allocating a dense time-indexed DP array causes immediate Out-Of-Memory ($O(10^9)$). DP state must be indexed over sorted job entities ($O(N)$), not time ticks.
  - **Strictly Dominated Long Jobs:** A long job spans $[1, 100]$ for profit $5$, while five short jobs span $[1, 2], [2, 3], \dots$ each with profit $10$. Algorithm must reject greedy duration-to-profit heuristics and evaluate exact optimal substructure.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** This is the Weighted Interval Scheduling problem. Unweighted interval scheduling yields to greedy earliest-deadline-first ($O(N \log N)$), but introducing non-uniform profits makes greedy choices sub-optimal. Dynamic programming with binary search over sorted end-times solves this: at each job $i$, evaluate the binary choice of either skipping job $i$ (profit $= dp[i-1]$) or taking job $i$ (profit $= profit[i] + dp[	ext{latest compatible job } j]$).
- **Sample 1:**
  - **Input:** `startTime = [1,2,3,3]`, `endTime = [3,4,5,6]`, `profit = [50,10,40,70]`
  - **Output:** `120`
  - **Explanation:** Choose the 1st job $[1, 3)$ with profit $50$ and the 4th job $[3, 6)$ with profit $70$. Total profit $= 50 + 70 = 120$.
- **Sample 2:**
  - **Input:** `startTime = [1,2,3,4,6]`, `endTime = [3,5,10,6,9]`, `profit = [20,20,100,70,60]`
  - **Output:** `150`
  - **Explanation:** Choose jobs $[1, 3)$ (profit $20$), $[4, 6)$ (profit $70$), and $[6, 9)$ (profit $60$). Total profit $= 20 + 70 + 60 = 150$.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a commercial freight charter aircraft available for rent. Job offers arrive from global shippers specifying takeoff time, landing time, and cash offer. If you greedily accept the job with the highest payout per hour, you might block off a pivotal landing window that could have accommodated three lucrative short hops. If you sort all flights strictly by landing time (chronological completion), at every flight's arrival you face a clean decision: *"If I accept this flight, my plane landed at time $X$. What was the richest historical schedule I could have flown that landed before this flight took off?"* Because all candidate predecessor schedules are already settled and sorted by landing time, a quick logarithmic binary lookup gives you the answer instantly.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force backtracking tree branches on every job: include or exclude. With $N = 5 	imes 10^4$, the recursion tree evaluates $2^{50000}$ subsets, causing catastrophic exponential time $O(2^N)$. A naive DP table without binary search inspects all previous $i - 1$ jobs linearly to find the maximum profit compatible predecessor, degrading to $O(N^2) = 2.5 	imes 10^9$ operations, which hits TLE (Time Limit Exceeded) within 2 seconds. Sorting by end time establishes monotonicity, allowing the predecessor search to collapse from $O(N)$ linear scan to $O(\log N)$ binary search.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Monotonic Prefix DP Invariant:**
  Sort all $N$ jobs monotonically by `endTime`: $E_1 \le E_2 \le \dots \le E_N$.
  Let $DP[i]$ be the maximum profit attainable using any valid subset of the first $i$ jobs ($1$-indexed).
  $DP[i]$ is monotonically non-decreasing: $DP[0] \le DP[1] \le \dots \le DP[N]$.
- **The Binary State Transition:**
  For job $i = 1 \dots N$ with parameters $(S_i, E_i, P_i)$:
  $$	ext{Exclude job } i \implies 	ext{Profit} = DP[i - 1]$$
  $$	ext{Include job } i \implies 	ext{Profit} = P_i + DP[p]$$
  where $p = \max \{ j \in [0, i - 1] \mid E_j \le S_i \}$.
  Therefore:
  $$DP[i] = \max(DP[i - 1], P_i + DP[p])$$
  Since $E_1 \dots E_{i-1}$ is sorted, index $p$ is located in $O(\log i)$ time via binary search (upper bound of $S_i$ over end times).

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Sorted Jobs by EndTime:
Index:          1           2          ...         p                  i
Job:       [S1---E1)   [S2---E2)              [Sp---Ep)          [Si---------Ei)
DP:          DP[1]       DP[2]                  DP[p]            DP[i] = max(...)
               |           |                      |                      ^
               +-----------+----------------------+                      |
                         Monotonic Prefix: E_1 <= E_2 <= ... <= E_p <= S_i
                         Binary Search finds latest 'p' where E_p <= S_i
```

- `i`: Active DP cursor advancing through sorted jobs from $1$ to $N$.
- `p`: The latest non-conflicting predecessor job found via binary search on subarray $E[1 \dots i - 1]$ against query key $S_i$.
- `dp[0..i-1]`: Fully settled immutable prefix subproblems.

#### 3.5 State Transition Triggers & Decision Gates
1. **Pre-Processing Gate:** Pack `(startTime, endTime, profit)` into a contiguous `Job[]` struct array and sort by `endTime` ascending.
2. **Binary Search Gate (Find $p$):** Given $S_i$, perform binary search on `jobs[0 .. i - 2]`:
   - If `jobs[mid].End <= currentJob.Start`: valid compatibility candidate; record `p = mid + 1` and search right (`low = mid + 1`).
   - If `jobs[mid].End > currentJob.Start`: overlapping collision; search left (`high = mid - 1`).
3. **Relaxation Gate:** Evaluate $DP[i] = \max(DP[i - 1], 	ext{currentJob.Profit} + DP[p])$.

#### 3.6 Concrete Step-by-Step State Trace
Input: `startTime = [1, 2, 3, 3]`, `endTime = [3, 4, 5, 6]`, `profit = [50, 10, 40, 70]`.
Sorted Jobs:
- Job 1: $[1, 3)$, $P = 50$
- Job 2: $[2, 4)$, $P = 10$
- Job 3: $[3, 5)$, $P = 40$
- Job 4: $[3, 6)$, $P = 70$

| Step $i$ | Current Job $(S, E, P)$ | Binary Search ($E \le S$) | Predecessor $p$ | Exclude Option ($DP[i-1]$) | Include Option ($P + DP[p]$) | DP State ($DP[i]$) | Active Max |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| $i = 0$ | Base Sentinel | N/A | N/A | N/A | N/A | $DP[0] = 0$ | $0$ |
| $i = 1$ | J1: $[1, 3), P=50$ | Query $E \le 1 \implies$ None | $p = 0$ | $DP[0] = 0$ | $50 + DP[0] = 50$ | $DP[1] = \max(0, 50) = 50$ | $50$ |
| $i = 2$ | J2: $[2, 4), P=10$ | Query $E \le 2 \implies$ None | $p = 0$ | $DP[1] = 50$ | $10 + DP[0] = 10$ | $DP[2] = \max(50, 10) = 50$ | $50$ |
| $i = 3$ | J3: $[3, 5), P=40$ | Query $E \le 3 \implies$ Job 1 | $p = 1$ | $DP[2] = 50$ | $40 + DP[1] = 90$ | $DP[3] = \max(50, 90) = 90$ | $90$ |
| $i = 4$ | J4: $[3, 6), P=70$ | Query $E \le 3 \implies$ Job 1 | $p = 1$ | $DP[3] = 90$ | $70 + DP[1] = 120$ | $DP[4] = \max(90, 120) = 120$ | **120** |

Termination: Final answer is $DP[4] = 120$.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Bottom-Up DP with Binary Search on End Times (Optimal & Recommended):**
  - Sorts jobs by `endTime`. Builds DP table left-to-right. Binary search finds latest compatible predecessor.
  - Predictable execution, zero recursion overhead, optimal memory locality.
- **Approach 2: Top-Down Memoized DFS with Binary Search on Start Times:**
  - Sorts jobs by `startTime`. Computes `dfs(i) = max(dfs(i + 1), profit[i] + dfs(nextJob))`.
  - Equally optimal $O(N \log N)$ asymptotic complexity, but incurs call stack allocation for $5 	imes 10^4$ frames.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Contract Validation:** Check non-null references and matching lengths $n$.
2. **Struct Packing:** Store elements in a contiguous `Job[]` value-type array (`struct Job { int Start, End, Profit; }`) to avoid reference pointer chasing.
3. **Sorting:** Order array by `End` ascending using `Array.Sort(jobs, (a, b) => a.End.CompareTo(b.End))`.
4. **Table Allocation:** Allocate `int[] dp = new int[n + 1]`.
5. **Linear Iteration with Binary Search:** For each job $i \in [1, n]$, locate latest non-overlapping job $p$ via custom binary search; assign $dp[i] = \max(dp[i - 1], 	ext{profit}_i + dp[p])$.
6. **Result Emission:** Return $dp[n]$.

#### 4.3 Alternative Approaches Analysis
- **Greedy Interval Scheduling:** Fails because higher-profit jobs can occupy longer intervals. No single greedy heuristic (earliest end time, max profit, max profit/duration) produces optimal answers.
- **Segment Tree / Fenwick Tree over Coordinate-Compressed Times:** Dynamically insert jobs and query range maximums. While $O(N \log N)$, it requires coordinate compression of timestamps and introduces substantial constant-factor overhead compared to simple prefix DP with binary search.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Bottom-Up DP + Binary Search (Selected)** | $O(N \log N)$ | $O(N \log N)$ | $O(N \log N)$ | $O(N)$ | $O(1)$ | High (contiguous struct array) | Non-destructive | Offline (requires sort) |
| **2. Top-Down Memoized DFS** | $O(N \log N)$ | $O(N \log N)$ | $O(N \log N)$ | $O(N)$ (recursion stack) | $O(1)$ | Moderate | Non-destructive | Offline |
| **3. Coordinate Compressed Fenwick Tree** | $O(N \log N)$ | $O(N \log N)$ | $O(N \log N)$ | $O(N)$ | $O(1)$ | Moderate | Non-destructive | Semi-Online |
| **4. Brute Force Backtracking** | $O(N)$ | $O(2^N)$ | $O(2^N)$ | $O(N)$ | $O(1)$ | Low | Non-destructive | Offline |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Maximum Profit in Job Scheduling (LeetCode #1235)
 * ============================================================================
 * Core Pattern      : DP with Binary Search (Weighted Interval Scheduling)
 * Time Complexity   : O(N log N) - Sorting requires O(N log N), N binary searches require O(N log N)
 * Space Complexity  : O(N) Auxiliary - Contiguous value-type struct array + DP state buffer
 * Selection Rule    : Sort by endTime ascending; monotonically increasing DP prefix enables
 *                     O(log N) predecessor retrieval via upper-bound binary search.
 * Defensive Traps   : Non-overlapping boundary condition: endTime == next.startTime is compatible;
 *                     binary search must use '<= targetStart' condition.
 * ============================================================================
 */

using System;

public class Solution
{
    /// <summary>
    /// Lightweight value-type representing an indivisible work interval.
    /// Struct memory layout guarantees cache locality during sorting and binary search.
    /// </summary>
    private readonly struct Job : IComparable<Job>
    {
        public readonly int Start;
        public readonly int End;
        public readonly int Profit;

        public Job(int start, int end, int profit)
        {
            Start = start;
            End = end;
            Profit = profit;
        }

        public int CompareTo(Job other) => End.CompareTo(other.End);
    }

    /// <summary>
    /// Calculates the maximum profit achievable from non-overlapping jobs.
    /// </summary>
    /// <param name="startTime">Array of job start timestamps.</param>
    /// <param name="endTime">Array of job end timestamps.</param>
    /// <param name="profit">Array of job profit values.</param>
    /// <returns>Global maximum profit attainable.</returns>
    public int JobScheduling(int[] startTime, int[] endTime, int[] profit)
    {
        // Guard Clauses
        if (startTime == null || endTime == null || profit == null)
        {
            throw new ArgumentNullException("Input arrays must not be null.");
        }

        int n = startTime.Length;
        if (n == 0 || endTime.Length != n || profit.Length != n)
        {
            throw new ArgumentException("Input arrays must be non-empty and have matching lengths.");
        }

        // 1. Pack inputs into a contiguous struct array
        var jobs = new Job[n];
        for (int i = 0; i < n; i++)
        {
            jobs[i] = new Job(startTime[i], endTime[i], profit[i]);
        }

        // 2. Sort jobs monotonically by End timestamp
        Array.Sort(jobs);

        // 3. Allocate DP table: dp[i] denotes maximum profit using subset of first i jobs (1-indexed)
        // dp[0] = 0 (sentinel base case)
        var dp = new int[n + 1];

        // 4. State transition loop
        for (int i = 1; i <= n; i++)
        {
            ref readonly var currentJob = ref jobs[i - 1];

            // Option A: Skip current job
            int profitExclude = dp[i - 1];

            // Option B: Take current job + max profit from latest non-overlapping predecessor
            int latestCompatibleIndex = FindLatestNonOverlapping(jobs, i - 1, currentJob.Start);
            int profitInclude = currentJob.Profit + dp[latestCompatibleIndex];

            dp[i] = Math.Max(profitExclude, profitInclude);
        }

        return dp[n];
    }

    /// <summary>
    /// Performs binary search over jobs[0 .. count - 1] to find the largest 1-based index
    /// of a job whose End timestamp is less than or equal to targetStart.
    /// </summary>
    /// <param name="jobs">Sorted array of jobs.</param>
    /// <param name="count">Number of elements to search within.</param>
    /// <param name="targetStart">Start timestamp of the active job.</param>
    /// <returns>1-based index of compatible job, or 0 if no compatible job exists.</returns>
    private static int FindLatestNonOverlapping(Job[] jobs, int count, int targetStart)
    {
        int low = 0;
        int high = count - 1;
        int result = -1;

        while (low <= high)
        {
            int mid = low + ((high - low) >> 1);
            if (jobs[mid].End <= targetStart)
            {
                result = mid;       // Valid candidate found; attempt to find a later one to the right
                low = mid + 1;
            }
            else
            {
                high = mid - 1;     // Conflict; must search earlier intervals
            }
        }

        return result + 1; // Convert 0-based array index to 1-based DP index (returns 0 if result == -1)
    }
}

/// <summary>
/// Companion Implementation: Top-Down Memoized DP with Binary Search on Start Times.
/// </summary>
public class SolutionTopDown
{
    private readonly struct Job : IComparable<Job>
    {
        public readonly int Start;
        public readonly int End;
        public readonly int Profit;

        public Job(int start, int end, int profit)
        {
            Start = start;
            End = end;
            Profit = profit;
        }

        public int CompareTo(Job other) => Start.CompareTo(other.Start);
    }

    public int JobScheduling(int[] startTime, int[] endTime, int[] profit)
    {
        int n = startTime.Length;
        var jobs = new Job[n];
        for (int i = 0; i < n; i++)
        {
            jobs[i] = new Job(startTime[i], endTime[i], profit[i]);
        }

        Array.Sort(jobs);

        var memo = new int[n];
        Array.Fill(memo, -1);

        return Dfs(0, jobs, memo);
    }

    private static int Dfs(int i, Job[] jobs, int[] memo)
    {
        if (i >= jobs.Length) return 0;
        if (memo[i] != -1) return memo[i];

        // Choice 1: Skip job i
        int skip = Dfs(i + 1, jobs, memo);

        // Choice 2: Take job i and find next job j whose Start >= jobs[i].End
        int nextIndex = FindNextJob(jobs, i + 1, jobs[i].End);
        int take = jobs[i].Profit + Dfs(nextIndex, jobs, memo);

        return memo[i] = Math.Max(skip, take);
    }

    private static int FindNextJob(Job[] jobs, int startIdx, int targetEnd)
    {
        int low = startIdx;
        int high = jobs.Length - 1;
        int result = jobs.Length;

        while (low <= high)
        {
            int mid = low + ((high - low) >> 1);
            if (jobs[mid].Start >= targetEnd)
            {
                result = mid;
                high = mid - 1;
            }
            else
            {
                low = mid + 1;
            }
        }

        return result;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **Boundary Inclusivity Bug (`<=` vs `<`):** A frequent bug in interval scheduling is treating touching boundaries as overlapping. The problem contract explicitly states: *"If you choose a job that ends at time $X$, you will be able to start another job that starts at time $X$."* The binary search must look for `jobs[mid].End <= currentJob.Start`. Using `<` instead of `<=` incorrectly excludes valid contiguous jobs, causing sub-optimal answers.
- **Heap Allocation Overhead via Object Classes:** Creating $50,000$ reference-type class objects (`new Job(...)`) causes $50,000$ individual heap allocations and GC generational promotion. Defining `Job` as a `readonly struct` allocates a single contiguous memory block on the managed heap (`new Job[n]`), drastically reducing memory overhead and maximizing L1/L2 data cache hit rates during sorting and binary search.
- **Recursion Stack Exhaustion in Top-Down:** In .NET, deep recursion of $5 	imes 10^4$ frames will consume approximately $48$ bytes per stack frame $pprox 2.4$ MB, which may exceed the default thread stack size ($1$ MB on Windows 32-bit or secondary thread defaults) and trigger a fatal `StackOverflowException`. Production implementations should strongly favor the iterative bottom-up table approach.
- **Large Timestamp Value Scale:** Timestamps range up to $10^9$. Any attempt to use timestamp values as array indices (e.g. `int[] dp = new int[maxTime]`) causes instant catastrophic failure (`OutOfMemoryException`). DP state must index over discrete job entities ($1 \dots N$), decoupling space complexity from the temporal span.


---

## 160. Next Permutation (LeetCode #31)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#two-pointers` `#in-place` `#array` `#lexicographical-order` `#permutation-logic` |
| **LeetCode Link** | [Next Permutation](https://leetcode.com/problems/next-permutation/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** A permutation of an array of integers is an arrangement of its members into a sequence or linear order. The next permutation of an array of integers is the lexicographically next greater permutation. If such arrangement is not possible (the array is sorted in descending order), the array must be rearranged as the lowest possible order (sorted in ascending order). The replacement must be done strictly in-place and use only constant extra memory.
- **Assumptions & Contracts:**
  - The array `nums` must be modified in-place; no new array may be returned.
  - Lexicographical ordering compares elements index-by-index from left to right.
  - If no lexicographically greater permutation exists, the cyclic wrap-around contract mandates returning the globally minimal permutation (fully reversed/ascending).
- **Key Constraints:**
  - $1 \le 	ext{nums.Length} \le 100$
  - $0 \le 	ext{nums}[i] \le 100$
- **Senior Edge Cases to Defend:**
  - **Strictly Decreasing Array (Maximal Permutation):** E.g., `[5, 4, 3, 2, 1]`. No element satisfies $nums[i] < nums[i + 1]$. Pivot search exhausts to index $-1$. Algorithm must gracefully skip the successor swap and reverse the entire array to `[1, 2, 3, 4, 5]`.
  - **Duplicate Values:** E.g., `[1, 5, 1]` or `[2, 3, 1, 3, 3]`. Inequalities must be strictly defended: pivot condition requires strict inequality $nums[i] < nums[i + 1]$; successor search requires strict inequality $nums[j] > nums[i]$. Using non-strict $\le$ or $\ge$ causes infinite loops or invalid swaps between equal elements.
  - **Array of Identical Values:** E.g., `[1, 1, 1]`. Pivot search reaches $-1$; reversing array produces identical `[1, 1, 1]` in $O(N)$ time with zero side-effects.
  - **Minimal Arrays ($N \le 2$):** Arrays of length 1 require zero mutations; length 2 executes at most one swap.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Narayana Pandita's classic 14th-century lexicographical generation algorithm. Identify the longest monotonically non-increasing (descending) suffix from right to left. The element immediately preceding this suffix is the *pivot*. To make the smallest possible increment, replace the pivot with the smallest element in the suffix that is strictly greater than the pivot. Finally, reverse the suffix into monotonically non-decreasing (ascending) order to make the remaining trailing digits minimal.
- **Sample 1:**
  - **Input:** `nums = [1, 2, 3]`
  - **Output:** `[1, 3, 2]`
  - **Explanation:** Suffix is `[3]`, pivot is `2`. Smallest suffix element $> 2$ is `3`. Swap `2` and `3` $	o$ `[1, 3, 2]`. Suffix reversed is `[2]`. Result: `[1, 3, 2]`.
- **Sample 2 (Maximal Suffix Wrap):**
  - **Input:** `nums = [3, 2, 1]`
  - **Output:** `[1, 2, 3]`
  - **Explanation:** Array is strictly descending; no pivot exists. Entire array reversed to minimal ascending order.
- **Sample 3 (With Duplicates):**
  - **Input:** `nums = [1, 1, 5]`
  - **Output:** `[1, 5, 1]`
  - **Explanation:** Pivot is at index 1 (`nums[1] = 1 < nums[2] = 5`). Swap gives `[1, 5, 1]`.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Think of an automotive mechanical odometer or a combination lock. When the rightmost dials read `999`, they are saturated—they cannot increase any further without rolling over. To get the next number, you look for the first dial from the right that isn't saturated (has a smaller digit than its right neighbor). You increment that dial by the smallest possible amount, and reset all dials to its right to zeros (their smallest possible state). In permutations, a descending suffix is "saturated" (no rearrangement can make it larger). Finding the first smaller element left of the suffix gives the exact dial to increment; reversing the suffix resets it to the smallest possible ascending configuration.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force solution generates all $N!$ permutations, sorts them lexicographically in $O(N! \cdot N \log(N!))$ time, finds the index of `nums`, and picks the subsequent array. For $N = 100$, $100! pprox 9.3 	imes 10^{157}$, which exceeds the number of atoms in the observable universe. The in-place suffix analysis decouples the problem into $O(N)$ localized pointer scans, inspecting at most $2N$ elements and achieving optimal $O(1)$ memory.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Suffix Satiation Invariant:**
  Any sequence $S = [s_1, s_2, \dots, s_k]$ sorted in non-increasing order ($s_1 \ge s_2 \ge \dots \ge s_k$) is already in its maximal lexicographical permutation. No reordering of $S$ can produce a larger value.
- **Pivot Invariant:**
  Scanning right-to-left, the first index $i$ where $nums[i] < nums[i + 1]$ defines the pivot. The entire suffix $nums[i + 1 \dots N - 1]$ is guaranteed to be non-increasing.
- **Minimal Increment Invariant:**
  To achieve the *immediate next* lexicographical permutation, we must increase $nums[i]$ by the minimal possible margin. We scan the suffix from right to left to find index $j > i$ such that $nums[j] > nums[i]$. Because the suffix is non-increasing, the first element from the right greater than $nums[i]$ is guaranteed to be the smallest element in the suffix strictly greater than $nums[i]$.
- **Inversion Invariant:**
  After swapping $nums[i]$ and $nums[j]$, the suffix $nums[i + 1 \dots N - 1]$ remains strictly non-increasing. Reversing this suffix transforms it into monotonically non-decreasing (ascending) order, which is the mathematically minimal configuration for those trailing elements.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Step 1: Scan backward to find Pivot 'i' (nums[i] < nums[i+1])
  nums:  [ 1 ,  5 ,  8 ,  4 ,  7 ,  6 ,  5 ,  3 ,  1 ]
                            ^  \____________________/
                         Pivot       Descending Suffix
                         (i=3)       nums[4..8] is saturated

Step 2: Scan backward in suffix to find Successor 'j' (nums[j] > nums[i])
  nums:  [ 1 ,  5 ,  8 ,  4 ,  7 ,  6 ,  5 ,  3 ,  1 ]
                            ^             ^
                          i=3           j=6 (smallest element > 4)

Step 3: Swap nums[i] and nums[j]
  nums:  [ 1 ,  5 ,  8 ,  5 ,  7 ,  6 ,  4 ,  3 ,  1 ]
                            ^  \____________________/
                         Updated   Still Descending Suffix!

Step 4: Reverse Suffix nums[i+1 .. N-1] in-place
  nums:  [ 1 ,  5 ,  8 ,  5 ,  1 ,  3 ,  4 ,  6 ,  7 ]
                         \__________________________/
                          Ascending Minimal Suffix!
```

- `i`: Pivot cursor, scanning from $N - 2$ down to $0$.
- `j`: Successor cursor, scanning from $N - 1$ down to $i + 1$.
- `left, right`: Two-pointer cursors for in-place suffix reversal.

#### 3.5 State Transition Triggers & Decision Gates
1. **Pivot Gate:** Locate largest index $i$ such that $nums[i] < nums[i + 1]$.
   - Condition: `while (i >= 0 && nums[i] >= nums[i + 1]) i--;`
2. **Branch Decision Gate:**
   - Case $i \ge 0$: Valid pivot found. Advance to Successor Gate.
   - Case $i < 0$: Saturated array (globally maximal). Skip swap and transition directly to Suffix Reversal Gate on range $[0, N - 1]$.
3. **Successor Gate:** Locate largest index $j > i$ such that $nums[j] > nums[i]$.
   - Condition: `while (nums[j] <= nums[i]) j--;`
   - Action: Swap `nums[i]` and `nums[j]`.
4. **Suffix Reversal Gate:** Reverse subarray $nums[i + 1 \dots N - 1]$ using two-pointer swap convergence.

#### 3.6 Concrete Step-by-Step State Trace
Input: `nums = [1, 3, 5, 4, 2]` ($N = 5$).

| Step | Operation | Cursors | Array State | Invariant Maintained |
| :---: | :--- | :---: | :--- | :--- |
| 1 | Locate Pivot | $i = 3 	o nums[3]=4 \ge nums[4]=2$ (continue)<br>$i = 2 	o nums[2]=5 \ge nums[3]=4$ (continue)<br>$i = 1 	o nums[1]=3 < nums[2]=5$ (STOP) | `[1, 3, 5, 4, 2]` | Suffix `[5, 4, 2]` is monotonically decreasing |
| 2 | Locate Successor | $j = 4 	o nums[4]=2 \le nums[1]=3$ (continue)<br>$j = 3 	o nums[3]=4 > nums[1]=3$ (STOP) | `[1, 3, 5, 4, 2]` | `nums[3]=4` is smallest element in suffix $> 3$ |
| 3 | Swap Pivot & Successor | Swap `nums[1]` and `nums[3]` | `[1, 4, 5, 3, 2]` | Prefix updated to `[1, 4]`; suffix `[5, 3, 2]` is still descending |
| 4 | Reverse Suffix | Reverse range $[i + 1, N - 1] = [2, 4]$<br>Swap `nums[2]` and `nums[4]` | `[1, 4, 2, 3, 5]` | Suffix converted to minimal ascending configuration |

Final Array: `[1, 4, 2, 3, 5]`. Next lexicographical permutation achieved.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Single-Pass In-Place Scan & Reverse (Optimal & Industry Standard):**
  - Time: $O(N)$ worst-case (at most $2N$ comparisons and $N/2$ swaps).
  - Space: $O(1)$ auxiliary (zero heap memory allocations).
  - Complies strictly with the in-place signature contract `void NextPermutation(int[] nums)`.
- **Approach 2: Binary Search for Successor:**
  - Because suffix is sorted descending, finding successor can use binary search in $O(\log N)$ instead of $O(N)$ scan.
  - However, overall complexity remains bounded by $O(N)$ due to the obligatory suffix reversal. Binary search adds branching overhead with zero asymptotic benefit for $N \le 100$.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Guard Clause:** If $nums == null$ or $nums.Length \le 1$, return immediately.
2. **Find Pivot:** Initialize pointer `i = nums.Length - 2`. Decrement `i` while $i \ge 0$ and $nums[i] \ge nums[i + 1]$.
3. **Find Successor & Swap:** If $i \ge 0$, initialize `j = nums.Length - 1`. Decrement `j` while $nums[j] \le nums[i]$. Swap $nums[i]$ and $nums[j]$.
4. **Reverse Suffix:** Set `left = i + 1`, `right = nums.Length - 1`. While `left < right`, swap $nums[left]$ and $nums[right]$, incrementing `left` and decrementing `right`.

#### 4.3 Alternative Approaches Analysis
- **Full Permutation Generation:** Enumerates all $N!$ permutations via backtracking. Completely unfeasible due to factorial combinatorial explosion.
- **Tree-Based / Priority Queue Multi-Set:** Inserting remaining elements into a balanced BST to find successor and in-order output. Requires $O(N \log N)$ time and $O(N)$ auxiliary space, violating the $O(1)$ space requirement.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. In-Place Narayana Algorithm (Selected)** | $O(1)$ | $O(N)$ | $O(N)$ | $O(1)$ | $O(1)$ | Optimal (sequential memory access) | Fully In-Place | Offline (requires random access) |
| **2. Binary Search Successor + Reverse** | $O(1)$ | $O(N)$ | $O(N)$ | $O(1)$ | $O(1)$ | High | Fully In-Place | Offline |
| **3. Suffix Sort with Array.Sort** | $O(N \log N)$ | $O(N \log N)$ | $O(N \log N)$ | $O(\log N)$ | $O(1)$ | Moderate | In-Place | Offline |
| **4. Brute Force Permutation Enumeration** | $O(N!)$ | $O(N!)$ | $O(N!)$ | $O(N!)$ | $O(N)$ | Terrible | Non-mutating | Unusable |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Next Permutation (LeetCode #31)
 * ============================================================================
 * Core Pattern      : In-Place Lexicographical Generation (Pivot-Successor-Reverse)
 * Time Complexity   : O(N) - At most two backward scans and one suffix reversal
 * Space Complexity  : O(1) Auxiliary - Strict in-place array mutation with zero heap allocations
 * Selection Rule    : Standard Narayana Pandita algorithm; guarantees minimal lexicographical increment.
 * Defensive Traps   : Use strict inequality nums[i] < nums[i + 1] to locate pivot;
 *                     use strict inequality nums[j] > nums[i] to locate successor.
 * ============================================================================
 */

using System;

public class Solution
{
    /// <summary>
    /// Rearranges numbers into the lexicographically next greater permutation of numbers.
    /// Operates strictly in-place in O(N) time and O(1) auxiliary space.
    /// </summary>
    /// <param name="nums">The integer array to mutate in-place.</param>
    public void NextPermutation(int[] nums)
    {
        // Guard Clause: Arrays with length <= 1 are already in their sole permutation
        if (nums == null || nums.Length <= 1)
        {
            return;
        }

        int n = nums.Length;

        // Step 1: Scan right-to-left to find the first decreasing element (the pivot)
        // Invariant: All elements to the right of 'pivot' form a monotonically non-increasing suffix.
        int pivot = n - 2;
        while (pivot >= 0 && nums[pivot] >= nums[pivot + 1])
        {
            pivot--;
        }

        // Step 2: If a valid pivot was found, find the smallest element in suffix strictly greater than nums[pivot]
        if (pivot >= 0)
        {
            int successor = n - 1;
            // Since suffix is non-increasing, the first element from the right > nums[pivot]
            // is guaranteed to be the smallest element strictly greater than nums[pivot].
            while (nums[successor] <= nums[pivot])
            {
                successor--;
            }

            // Swap pivot and successor
            Swap(nums, pivot, successor);
        }

        // Step 3: Reverse the suffix starting at pivot + 1
        // If pivot == -1 (entire array was descending), this reverses the full array to ascending order.
        ReverseSuffix(nums, pivot + 1, n - 1);
    }

    /// <summary>
    /// Swaps two elements in-place using a scalar temporary register.
    /// </summary>
    private static void Swap(int[] nums, int i, int j)
    {
        int temp = nums[i];
        nums[i] = nums[j];
        nums[j] = temp;
    }

    /// <summary>
    /// Reverses the subarray nums[start .. end] in-place using two pointers.
    /// </summary>
    private static void ReverseSuffix(int[] nums, int start, int end)
    {
        while (start < end)
        {
            Swap(nums, start, end);
            start++;
            end--;
        }
    }
}

/// <summary>
/// Span-Optimized High-Performance Variant (.NET 8/9).
/// Demonstrates zero-allocation slice mutations applicable to native memory buffers.
/// </summary>
public class SolutionSpanOptimized
{
    public void NextPermutation(Span<int> nums)
    {
        if (nums.Length <= 1) return;

        int n = nums.Length;
        int pivot = n - 2;

        while (pivot >= 0 && nums[pivot] >= nums[pivot + 1])
        {
            pivot--;
        }

        if (pivot >= 0)
        {
            int successor = n - 1;
            while (nums[successor] <= nums[pivot])
            {
                successor--;
            }

            (nums[pivot], nums[successor]) = (nums[successor], nums[pivot]);
        }

        nums.Slice(pivot + 1).Reverse();
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **The Non-Strict Comparison Bug (`>` vs `>=`):** When scanning for the pivot, using `nums[pivot] > nums[pivot + 1]` instead of `nums[pivot] >= nums[pivot + 1]` fails on arrays with duplicate adjacent elements such as `[1, 5, 1, 1]`. The suffix must be *weakly decreasing* (non-increasing). If you stop on equal elements, you identify a false pivot and generate an invalid permutation.
- **Successor Equality Trap:** When searching backward for the successor, using `nums[successor] < nums[pivot]` instead of `nums[successor] <= nums[pivot]` will select an element equal to the pivot. Swapping identical elements changes nothing, leaving the array in an invalid state after the suffix reverse. The condition must be strictly `nums[successor] <= nums[pivot]` to guarantee strict inequality upon swap.
- **Redundant Suffix Sorting with `Array.Sort`:** A common anti-pattern is replacing the two-pointer suffix reversal with `Array.Sort(nums, pivot + 1, n - pivot - 1)`. Because the suffix is mathematically proven to be monotonically decreasing, reversing it with two pointers takes exactly $\lfloor K/2 
floor$ swaps and $O(K)$ time. Invoking introspective sort incurs unnecessary $O(K \log K)$ comparisons, stack frames, and function call overhead.
- **Index Out-of-Bounds on Exhaustion:** When `nums` is strictly decreasing, `pivot` becomes `-1`. Defensive code must check `if (pivot >= 0)` before accessing `nums[pivot]` or attempting to locate a successor. If `pivot == -1`, the algorithm must smoothly transition to reversing the entire array from index `0` (`pivot + 1`) to `n - 1`.


---

## 161. Employee Free Time (LeetCode #759)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#intervals` `#heap-priority-queue` `#line-sweep` `#merge-intervals` |
| **LeetCode Link** | [Employee Free Time](https://leetcode.com/problems/employee-free-time/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** We are given a list `schedule` of employees, which represents the working time for each employee. Each employee has a list of non-overlapping `Interval`s, and these intervals are already sorted in ascending order. Return the list of finite intervals representing common, positive-length free time for all employees, also in sorted order.
- **Data Structure Definition:**
  ```csharp
  public class Interval {
      public int start;
      public int end;
      public Interval() {}
      public Interval(int _start, int _end) { start = _start; end = _end; }
  }
  ```
- **Assumptions & Contracts:**
  - "Common free time" is defined as any time window where *zero* employees are working.
  - Infinite outer intervals (e.g. $(-\infty, \min(start))$ and $(\max(end), +\infty)$) are explicitly excluded; only finite intermediate gaps are returned.
  - Free time intervals must have positive duration: if employee A works until time $3$ and employee B starts at time $3$, there is $0$ free duration, so $[3, 3]$ must not be emitted.
- **Key Constraints:**
  - $1 \le 	ext{schedule.Length} \le 50$ (number of employees $K$)
  - $0 \le 	ext{schedule}[i]	ext{.Length} \le 500$
  - Total intervals across all employees $N \le 2.5 	imes 10^4$
  - $0 \le 	ext{start} < 	ext{end} \le 10^8$
- **Senior Edge Cases to Defend:**
  - **Touching Boundaries:** Employee 1 works $[1, 3]$, Employee 2 works $[3, 5]$. Since $3 == 3$, no common free time exists; algorithm must require $curr.start > prevEnd$ (strict inequality).
  - **Completely Subsumed Intervals:** Employee 1 works $[1, 10]$, Employee 2 works $[2, 5]$. The active `prevEnd` must not regress: `prevEnd = Math.Max(prevEnd, curr.end)`.
  - **Zero Common Free Time:** Working hours collectively cover the entire continuous span without gaps $\implies$ returns empty list `[]`.
  - **Empty Employee Schedules:** An employee with zero scheduled intervals must not crash heap initialization.
  - **Single Employee:** If $K = 1$, the problem reduces to returning all internal gaps between their non-overlapping sorted intervals.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** The problem is equivalent to computing the set complement of the union of all busy intervals. By merging all busy intervals into contiguous non-overlapping maximal blocks, any gap between the end of one merged block and the start of the next constitutes a common free window. Because each employee's schedule is pre-sorted, we can stream the intervals via a $K$-Way Min-Heap Merge in $O(N \log K)$ time and $O(K)$ space, bypassing the need to sort all $N$ intervals upfront.
- **Sample 1:**
  - **Input:** `schedule = [[[1,2],[5,6]],[[1,3]],[[4,10]]]`
  - **Output:** `[[3,4]]`
  - **Explanation:** Merged busy intervals are $[1, 3]$ and $[4, 10]$. The finite gap between $3$ and $4$ is $[3, 4]$.
- **Sample 2:**
  - **Input:** `schedule = [[[1,3],[6,7]],[[2,4]],[[2,5],[9,12]]]`
  - **Output:** `[[5,6],[7,9]]`
  - **Explanation:** Merged busy intervals are $[1, 5]$, $[6, 7]$, and $[9, 12]$. The common free gaps are $[5, 6]$ and $[7, 9]$.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine looking at a corporate conference room booking schedule with 50 separate calendar feeds. If you print every employee's calendar, cut out the busy strips, and paste them onto a single master glass wall, some strips will overlap and thicken. Once all strips are pasted down, you look through the glass wall for sections of clear daylight. Every clear patch where no colored paper exists between the first strip and the last strip is a collective open window where everyone can meet. Because each employee's calendar is already chronologically sorted, you don't need to dump all 25,000 papers into a box and sort them; you can just look at the next upcoming meeting from each of the 50 employees using a small desk organizer (min-heap).

#### 3.2 The Naive Bottleneck & Redundant Computation
A naive approach constructs a timeline array of booleans `isBusy[10^8]`. With timestamps reaching $10^8$, this requires $100$ MB of memory and $10^8$ operations, making it extremely inefficient and prone to memory pressure. A standard interval merge dumps all $N$ intervals into an array and calls `Array.Sort`, which takes $O(N \log N)$ time and $O(N)$ space. Since each employee's list of intervals is *already sorted*, sorting from scratch discards valuable pre-existing order. A $K$-way merge heap leverages the pre-sorted invariant to reduce auxiliary space to $O(K)$ and time to $O(N \log K)$.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Union Complement Invariant:**
  Let $U = igcup_{i=1}^K 	ext{schedule}[i]$ be the union of all busy intervals.
  When $U$ is expressed as a minimal sequence of disjoint sorted closed intervals $[S_1, E_1], [S_2, E_2], \dots, [S_m, E_m]$ with $E_j < S_{j+1}$, the set of all finite common free intervals is uniquely defined as:
  $$	ext{Free} = \{ [E_j, S_{j+1}] \mid 1 \le j < m \land E_j < S_{j+1} \}$$
- **K-Way Stream Merge Invariant:**
  Maintain a min-heap containing at most one interval from each employee stream: `(interval, empIdx, intervalIdx)`.
  The heap root always presents the globally earliest unvisited interval across all employees.
- **Running Horizon Invariant:**
  Maintain `prevEnd` = maximum end time among all intervals processed so far.
  When extracting next interval `curr`:
  - If `curr.start > prevEnd`: A genuine common free interval exists: `[prevEnd, curr.start]`.
  - Update running horizon: `prevEnd = Math.Max(prevEnd, curr.end)`.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Employee 0: [1---3)       [6---8)
Employee 1:    [2-----5)              [9---11)
Employee 2:          [4-----7)

                       Min-Heap (size <= K)
                    ┌─────────────────────────┐
                    │ Extracts min start time │
                    └────────────┬────────────┘
                                 │
                                 v
Merged Horizon: [1-----------------------8)              [9---11)
                                         ^                ^
                                         |--- Free Gap ---|
                                            [ 8 ,  9 )
```

- `prevEnd`: Scalar cursor marking the rightmost boundary of the current contiguous busy cluster.
- `curr`: Next interval popped from Min-Heap.
- Decision: If `curr.start > prevEnd`, emit `new Interval(prevEnd, curr.start)`.

#### 3.5 State Transition Triggers & Decision Gates
1. **Initialization Gate:** For each employee $e \in [0, K - 1]$, if `schedule[e]` is non-empty, enqueue `(schedule[e][0], e, 0)` into the Min-Heap keyed by `Interval.start`.
2. **Horizon Seed:** Dequeue the first interval; set `prevEnd = first.end`; enqueue next interval from that employee if available.
3. **Stream Processing Loop (while Heap is not empty):**
   - Dequeue `(curr, e, idx)`.
   - **Gap Decision Gate:** If `curr.start > prevEnd`:
     - Common free time detected! Emit `new Interval(prevEnd, curr.start)`.
     - Advance horizon: `prevEnd = curr.end`.
   - **Overlap Decision Gate:** If `curr.start <= prevEnd`:
     - Intervals overlap or touch. Extend horizon: `prevEnd = Math.Max(prevEnd, curr.end)`.
   - **Replenishment Gate:** If `idx + 1 < schedule[e].Count`, enqueue `(schedule[e][idx + 1], e, idx + 1)`.

#### 3.6 Concrete Step-by-Step State Trace
Input:
- Emp 0: `[[1, 3], [6, 7]]`
- Emp 1: `[[2, 4]]`
- Emp 2: `[[2, 5], [9, 12]]`

| Step | Heap Pop `curr` (Emp, Idx) | `prevEnd` Before | Condition (`curr.start > prevEnd`) | Free Interval Emitted | `prevEnd` After | Heap Replenish |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| 1 | `[1, 3]` (E0, 0) | N/A (Init) | Seed Initial Horizon | None | $3$ | Enqueue E0[1]=`[6, 7]` |
| 2 | `[2, 4]` (E1, 0) | $3$ | $2 > 3 \implies$ False (Overlap) | None | $\max(3, 4) = 4$ | None (E1 exhausted) |
| 3 | `[2, 5]` (E2, 0) | $4$ | $2 > 4 \implies$ False (Overlap) | None | $\max(4, 5) = 5$ | Enqueue E2[1]=`[9, 12]` |
| 4 | `[6, 7]` (E0, 1) | $5$ | $6 > 5 \implies$ **True (Gap!)** | **`[5, 6]`** | $\max(5, 7) = 7$ | None (E0 exhausted) |
| 5 | `[9, 12]` (E2, 1) | $7$ | $9 > 7 \implies$ **True (Gap!)** | **`[7, 9]`** | $\max(7, 12) = 12$ | None (E2 exhausted) |

Final Result: `[[5, 6], [7, 9]]`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: K-Way Merge via PriorityQueue (Optimal Space & Streaming):**
  - Uses `PriorityQueue<(int EmpIdx, int IntIdx), int>` keyed by `start`.
  - Time: $O(N \log K)$ where $K \le 50$ is the number of employees, $N \le 2.5 	imes 10^4$ total intervals.
  - Auxiliary Space: $O(K)$ — heap size never exceeds $50$ entries. Ideal for memory-constrained distributed services.
- **Approach 2: Flatten & Full Sort:**
  - Collects all $N$ intervals into a flat `List<Interval>` and sorts in $O(N \log N)$ time.
  - Simpler implementation, but requires $O(N)$ auxiliary heap memory and ignores pre-sorted input properties.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Contract Validation:** Verify schedule is non-null and contains valid entries.
2. **Min-Heap Initialization:** Instantiate `PriorityQueue` with custom comparer or integer priority for `start`. Seed with index 0 of all non-empty employees.
3. **Horizon Tracking:** Seed `prevEnd` with the earliest interval's end.
4. **Iterative Extraction & Replenishment:** While heap has items, pop lowest start interval, compare with `prevEnd`, emit gap if strictly greater, update `prevEnd`, and push next item from the same employee list.
5. **Emission:** Return populated result list.

#### 4.3 Alternative Approaches Analysis
- **Line Sweep with Coordinate Events:** Insert `(start, +1)` and `(end, -1)` into a sorted event list. Maintain active employee counter `activeCount`. When `activeCount == 0`, enter free window. Takes $O(N \log N)$ time and $O(N)$ space; vulnerable to simultaneous start/end event ordering bugs.
- **Interval Tree / Segment Tree:** Overkill for offline batch processing; adds large pointer overhead without complexity advantage.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. K-Way Merge Heap (Selected)** | $O(N \log K)$ | $O(N \log K)$ | $O(N \log K)$ | $O(K)$ | $O(G)$ | Moderate (Heap in L1 cache) | Non-destructive | Excellent (Online streams) |
| **2. Flatten & Full Sort** | $O(N \log N)$ | $O(N \log N)$ | $O(N \log N)$ | $O(N)$ | $O(G)$ | High (linear array scan) | Non-destructive | Offline |
| **3. Event Line Sweep** | $O(N \log N)$ | $O(N \log N)$ | $O(N \log N)$ | $O(N)$ | $O(G)$ | Moderate | Non-destructive | Offline |
| **4. Dense Time Bitmap** | $O(T)$ | $O(T)$ | $O(T)$ | $O(T)$ ($10^8$ bits) | $O(G)$ | Low | Non-destructive | Unusable |

*(Note: $G$ is the count of free time gaps; $K \le 50$, $N \le 25,000$, $T \le 10^8$.)*

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Employee Free Time (LeetCode #759)
 * ============================================================================
 * Core Pattern      : K-Way Stream Merge via PriorityQueue (Interval Complement)
 * Time Complexity   : O(N log K) where N = total intervals, K = employee count
 * Space Complexity  : O(K) Auxiliary - Min-Heap contains at most K items at any time
 * Selection Rule    : Leverage pre-sorted employee intervals using K-Way merge; avoids O(N) sort.
 * Defensive Traps   : Strict inequality curr.start > prevEnd required to exclude zero-length gaps;
 *                     prevEnd must be updated via Math.Max to handle subsumed intervals.
 * ============================================================================
 */

using System;
using System.Collections.Generic;

public class Interval
{
    public int start;
    public int end;

    public Interval() { }

    public Interval(int _start, int _end)
    {
        start = _start;
        end = _end;
    }
}

public class Solution
{
    /// <summary>
    /// Lightweight state cursor tracking an employee's interval stream position.
    /// </summary>
    private readonly struct StreamCursor
    {
        public readonly int EmpIdx;
        public readonly int IntIdx;

        public StreamCursor(int empIdx, int intIdx)
        {
            EmpIdx = empIdx;
            IntIdx = intIdx;
        }
    }

    /// <summary>
    /// Finds all common finite free time intervals for all employees.
    /// Employs a K-way stream merge using PriorityQueue to achieve O(N log K) time and O(K) space.
    /// </summary>
    /// <param name="schedule">List of employees, each having a sorted list of non-overlapping intervals.</param>
    /// <returns>Sorted list of finite common free time intervals.</returns>
    public IList<Interval> EmployeeFreeTime(IList<IList<Interval>> schedule)
    {
        // Guard Clauses
        if (schedule == null || schedule.Count == 0)
        {
            return Array.Empty<Interval>();
        }

        int k = schedule.Count;
        var result = new List<Interval>();

        // PriorityQueue tracks StreamCursor, prioritized by Interval.start ascending
        var minHeap = new PriorityQueue<StreamCursor, int>();

        // Step 1: Seed the min-heap with the first interval of each non-empty employee schedule
        for (int empIdx = 0; empIdx < k; empIdx++)
        {
            if (schedule[empIdx] != null && schedule[empIdx].Count > 0)
            {
                minHeap.Enqueue(new StreamCursor(empIdx, 0), schedule[empIdx][0].start);
            }
        }

        if (minHeap.Count == 0)
        {
            return result;
        }

        // Step 2: Establish initial busy horizon from the earliest starting interval
        var initialCursor = minHeap.Peek();
        int prevEnd = schedule[initialCursor.EmpIdx][0].end;

        // Step 3: Stream intervals in chronological order
        while (minHeap.Count > 0)
        {
            var cursor = minHeap.Dequeue();
            var currInterval = schedule[cursor.EmpIdx][cursor.IntIdx];

            // Decision Gate: Check for gap between current start and the established busy horizon
            if (currInterval.start > prevEnd)
            {
                // Invariant: Zero employees are working in [prevEnd, currInterval.start]
                result.Add(new Interval(prevEnd, currInterval.start));
                prevEnd = currInterval.end;
            }
            else
            {
                // Overlapping or touching interval: extend busy horizon
                prevEnd = Math.Max(prevEnd, currInterval.end);
            }

            // Step 4: Advance the employee's stream cursor if additional intervals exist
            int nextIntIdx = cursor.IntIdx + 1;
            if (nextIntIdx < schedule[cursor.EmpIdx].Count)
            {
                minHeap.Enqueue(
                    new StreamCursor(cursor.EmpIdx, nextIntIdx),
                    schedule[cursor.EmpIdx][nextIntIdx].start
                );
            }
        }

        return result;
    }
}

/// <summary>
/// Companion Implementation: Flatten & Full Sort Approach.
/// Simpler mental model, optimal when K approaches N or heap allocations must be avoided.
/// </summary>
public class SolutionFlattenSort
{
    public IList<Interval> EmployeeFreeTime(IList<IList<Interval>> schedule)
    {
        if (schedule == null || schedule.Count == 0) return Array.Empty<Interval>();

        var allIntervals = new List<Interval>();
        foreach (var empSchedule in schedule)
        {
            if (empSchedule != null)
            {
                allIntervals.AddRange(empSchedule);
            }
        }

        if (allIntervals.Count == 0) return Array.Empty<Interval>();

        // Sort all intervals by start time ascending
        allIntervals.Sort((a, b) => a.start != b.start ? a.start.CompareTo(b.start) : a.end.CompareTo(b.end));

        var result = new List<Interval>();
        int prevEnd = allIntervals[0].end;

        for (int i = 1; i < allIntervals.Count; i++)
        {
            var curr = allIntervals[i];
            if (curr.start > prevEnd)
            {
                result.Add(new Interval(prevEnd, curr.start));
                prevEnd = curr.end;
            }
            else
            {
                prevEnd = Math.Max(prevEnd, curr.end);
            }
        }

        return result;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **Regressing `prevEnd` on Subsumed Intervals:** A catastrophic bug occurs when updating `prevEnd = curr.end` without `Math.Max`. If an employee has a long shift $[1, 10]$ followed by another employee's short shift $[2, 4]$, assigning `prevEnd = 4` regresses the busy horizon. A subsequent shift $[5, 7]$ would falsely detect a free window $[4, 5]$ even though Employee 1 is actively working until time $10$. Always use `prevEnd = Math.Max(prevEnd, curr.end)`.
- **Zero-Length Gap Ingestion (`>=` vs `>`):** When Employee A finishes at $3$ and Employee B starts at $3$, testing `curr.start >= prevEnd` would emit $[3, 3]$. The specification requires positive-length free time. Defend this invariant with strict inequality `curr.start > prevEnd`.
- **Memory Pressure in Flatten & Sort:** If each employee's schedule contains thousands of intervals, flattening them into a single list allocates an $O(N)$ contiguous collection on the large object heap (LOH) if $N$ is large, triggering expensive Gen 2 garbage collections. The $K$-way merge pattern keeps memory bounded strictly to $O(K)$, which for $K \le 50$ is a negligible footprint residing entirely in processor L1 cache.
- **Unbounded Outer Intervals Trap:** Do not attempt to append $(-\infty, 	ext{start})$ or $(	ext{end}, +\infty)$. The problem contract strictly defines output intervals as finite internal gaps between the first starting time and last ending time.


---

## 162. Is Graph Bipartite? (LeetCode #785)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#graph` `#bfs` `#dfs` `#graph-coloring` `#odd-length-cycle` |
| **LeetCode Link** | [Is Graph Bipartite?](https://leetcode.com/problems/is-graph-bipartite/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** There is an undirected graph with $n$ nodes, where each node is numbered between $0$ and $n - 1$. You are given a 2D array `graph`, where `graph[u]` is an array of adjacent nodes that node $u$ is connected to. The graph has no self-loops and no parallel edges. A graph is bipartite if the nodes can be partitioned into two independent sets $A$ and $B$ such that every edge in the graph connects a node in set $A$ and a node in set $B$. Return `true` if and only if the graph is bipartite.
- **Assumptions & Contracts:**
  - The graph is undirected: $v \in graph[u] \iff u \in graph[v]$.
  - The graph is not guaranteed to be connected; it may consist of multiple disjoint components, trees, or isolated nodes.
  - Return boolean `true` if 2-colorable without monochromatic edges, else `false`.
- **Key Constraints:**
  - $n == 	ext{graph.Length}$
  - $1 \le n \le 100$
  - $0 \le 	ext{graph}[u]	ext{.Length} < n$
  - $0 \le 	ext{graph}[u][i] \le n - 1$
  - `graph[u]` does not contain $u$ (no self-loops).
  - All values in `graph[u]` are distinct (no multi-edges).
- **Senior Edge Cases to Defend:**
  - **Disconnected Components (Forests):** E.g., vertices $\{0, 1\}$ form a bipartite component, while vertices $\{2, 3, 4\}$ form an odd triangle. Traversal must loop through all vertices $0 \dots n - 1$ as potential BFS seeds; searching only from vertex 0 creates a false positive.
  - **Isolated Vertices ($graph[u] = []$):** An isolated vertex has zero edges and trivially belongs to either set without conflict.
  - **Odd-Length Cycle ($C_3, C_5, \dots$):** E.g., triangle $0-1-2-0$. Impossible to 2-color; must immediately short-circuit to `false`.
  - **Even-Length Cycle ($C_4, C_6, \dots$):** E.g., square $0-1-2-3-0$. Perfectly 2-colorable; must return `true`.
  - **Complete Bipartite Graphs ($K_{m,n}$):** Densely connected across partitions with zero intra-partition edges.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** By Kőnig's 1936 theorem, a graph is bipartite if and only if it contains no odd-length cycles. This is algebraically equivalent to the 2-Colorability problem. We assign each visited node one of two colors (e.g. $+1$ or $-1$). When traversing an edge from node $u$ to neighbor $v$, if $v$ is uncolored, we paint it $-color[u]$. If $v$ is already colored and $color[v] == color[u]$, an odd cycle is detected and the graph cannot be bipartite.
- **Sample 1:**
  - **Input:** `graph = [[1,2,3],[0,2],[0,1,3],[0,2]]`
  - **Output:** `false`
  - **Explanation:** Nodes $0, 1, 2$ form a 3-cycle (triangle). If node 0 is Red, node 1 must be Blue, and node 2 must be Red (adjacent to 1). But node 2 is also adjacent to node 0 (both Red), violating bipartiteness.
- **Sample 2:**
  - **Input:** `graph = [[1,3],[0,2],[1,3],[0,2]]`
  - **Output:** `true`
  - **Explanation:** We can partition vertices into Set A = $\{0, 2\}$ (Red) and Set B = $\{1, 3\}$ (Blue). Every edge connects an element of Set A to an element of Set B.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine organizing an exhibition football scrimmage with two teams: Team Red and Team Blue. Whenever two players have a documented fierce rivalry (an edge in the graph), they must be assigned to opposing teams with different colored jerseys. You hand player 0 a Red jersey. All of player 0's rivals must receive Blue jerseys. In turn, all rivals of those Blue players must receive Red jerseys. As the wave spreads outward, if you ever find two players with a rivalry already wearing the same jersey color, the match is deadlocked—it is impossible to divide the players into two peaceful sides without rival teammates.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force approach generates all $2^n$ possible binary partition assignments of nodes into set $A$ and set $B$, and for each assignment validates all $E$ edges. For $N = 100$, $2^{100} pprox 1.26 	imes 10^{30}$ assignments, which is computationally intractable. Graph coloring via BFS or DFS constructs the partition deterministically in $O(V + E)$ time by propagating constraints along edges.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **The Parity Alternation Invariant:**
  In a bipartite graph, the shortest path between any two vertices in the same partition must have even length, and between vertices in different partitions must have odd length.
- **Two-Color Constraint Propagation:**
  Let $C: V 	o \{0, 1, -1\}$ denote the coloring state ($0 = 	ext{uncolored}$, $1 = 	ext{Color A}$, $-1 = 	ext{Color B}$).
  For every edge $(u, v) \in E$:
  - If $C(v) == 0 \implies C(v) \leftarrow -C(u)$.
  - If $C(v) == C(u) \implies 	ext{Monochromatic edge} \implies 	ext{Odd cycle} \implies 	ext{Graph is NOT bipartite}$.
- **Multi-Source Component Coverage:**
  A graph $G$ is bipartite if and only if every connected component $G_1, G_2, \dots, G_k$ of $G$ is bipartite. Iterating through all vertices $i \in [0, n - 1]$ ensures that every component is explored.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
BFS Wavefront Color Alternation:

Level 0 (Color +1):           [ Node 0 ]
                                /     Level 1 (Color -1):      [ Node 1 ]   [ Node 3 ]
                                \     /
Level 2 (Color +1):           [ Node 2 ]

Invariant: Edges cross ONLY between adjacent levels (Color +1 <-> Color -1).
Conflict: If an edge exists between two nodes on the same level (e.g. Node 1 <-> Node 3),
          both have Color -1 -> Monochromatic collision! Odd-length cycle proven!
```

- `i`: Active component scanning cursor traversing $0 \dots n - 1$.
- `queue`: FIFO BFS queue holding vertices whose neighbors are awaiting color propagation.
- `colors[]`: Trinary state vector where `colors[u] == 0` (unvisited), `1` (Team A), `-1` (Team B).

#### 3.5 State Transition Triggers & Decision Gates
1. **Component Sweep Gate:** Loop $i = 0 \dots n - 1$. If `colors[i] != 0`, skip (component already certified). If `colors[i] == 0`, set `colors[i] = 1`, enqueue $i$, and initiate BFS.
2. **BFS Expansion Gate:** Dequeue vertex $u$. Let expected neighbor color be $-colors[u]$.
3. **Neighbor Evaluation Gate (for each $v \in graph[u]$):**
   - **Case A (`colors[v] == 0`):** Paint $v \leftarrow -colors[u]$ and enqueue $v$.
   - **Case B (`colors[v] == colors[u]`):** Parity violation detected! Immediate terminal exit: return `false`.
   - **Case C (`colors[v] == -colors[u]`):** Consistent bipartite cross-edge. Continue traversal.
4. **Completion Gate:** If all components pass without violation, return `true`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `graph = [[1,3],[0,2],[1,3],[0,2]]` (Square $C_4$: $0-1-2-3-0$).

| Step | Action / Dequeue | Active Node | Neighbor $v$ | State of `colors[v]` | Decision | Colors Array | Queue State |
| :---: | :--- | :---: | :---: | :---: | :--- | :--- | :--- |
| 1 | Seed Component 0 | 0 | - | - | Set `colors[0] = 1` | `[1, 0, 0, 0]` | `[0]` |
| 2 | Dequeue 0 | 0 | 1 | `0` (Uncolored) | Paint `colors[1] = -1`, Enqueue 1 | `[1, -1, 0, 0]` | `[1]` |
| 3 | Continued from 0 | 0 | 3 | `0` (Uncolored) | Paint `colors[3] = -1`, Enqueue 3 | `[1, -1, 0, -1]` | `[1, 3]` |
| 4 | Dequeue 1 | 1 | 0 | `1` (Opposite) | Valid cross-edge | `[1, -1, 0, -1]` | `[3]` |
| 5 | Continued from 1 | 1 | 2 | `0` (Uncolored) | Paint `colors[2] = 1`, Enqueue 2 | `[1, -1, 1, -1]` | `[3, 2]` |
| 6 | Dequeue 3 | 3 | 0 | `1` (Opposite) | Valid cross-edge | `[1, -1, 1, -1]` | `[2]` |
| 7 | Continued from 3 | 3 | 2 | `1` (Opposite) | Valid cross-edge | `[1, -1, 1, -1]` | `[2]` |
| 8 | Dequeue 2 | 2 | 1, 3 | `-1` (Opposite) | Both valid cross-edges | `[1, -1, 1, -1]` | `[]` |

Loop finishes; all vertices colored consistently. Returns `true`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Breadth-First Search (BFS) 2-Coloring (Optimal & Recommended):**
  - Level-by-level propagation naturally corresponds to graph bipartition distance layers.
  - Iterative execution with a single queue eliminates recursion stack overflow risks.
- **Approach 2: Depth-First Search (DFS) 2-Coloring:**
  - Recursively explores paths, passing inverted color parameter.
  - Clean and concise, but for pathological graph shapes (e.g. long degenerate chains of $10^5$ vertices), recursion can exceed stack depth.
- **Approach 3: Disjoint Set Union (DSU / Union-Find) with $2V$ Nodes:**
  - Maintains sets for each node $x$ and its complement $x + V$.
  - Adds edges $(u, v + V)$ and $(v, u + V)$. If $Find(u) == Find(v)$, an odd cycle exists.
  - Useful in dynamic streaming graphs where edges are added online.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Contract Validation:** Check non-null array; if $n \le 1$, return `true` immediately.
2. **Color Array Allocation:** Allocate `int[] colors = new int[n]`. Initialize with $0$.
3. **Queue Allocation:** Pre-allocate `Queue<int>(n)` to prevent internal array growth churn.
4. **Component Loop:** Iterate $i = 0 \dots n - 1$. If $colors[i] == 0$, seed $colors[i] = 1$, push to queue, and process BFS.
5. **BFS Processing:** While queue non-empty, pop $u$, check every neighbor $v \in graph[u]$. If uncolored, color and enqueue; if same color, return `false`.
6. **Success Exit:** Return `true` if all components complete without contradiction.

#### 4.3 Alternative Approaches Analysis
- **Adjacency Matrix Powers ($A^k$):** In graph theory, $(A^k)_{ii}$ counts closed walks of length $k$. Bipartiteness requires all odd powers of $A$ to have zero trace. Computing matrix multiplications takes $O(V^{2.8})$ or $O(V^3)$ time—far inferior to $O(V + E)$ BFS.
- **Union-Find:** Operates in $O((V + E) lpha(V))$ time. Slightly slower than BFS due to tree pointer traversals, but excellent for incremental edge additions.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. BFS 2-Coloring (Selected)** | $O(1)$ | $O(V + E)$ | $O(V + E)$ | $O(V)$ | $O(1)$ | High (sequential queues) | Non-destructive | Offline |
| **2. DFS 2-Coloring** | $O(1)$ | $O(V + E)$ | $O(V + E)$ | $O(V)$ (stack) | $O(1)$ | Moderate | Non-destructive | Offline |
| **3. Disjoint Set Union (2V)** | $O(1)$ | $O((V + E)lpha(V))$ | $O((V + E)lpha(V))$ | $O(V)$ | $O(1)$ | High (flat arrays) | Non-destructive | Excellent (Online edges) |
| **4. Brute Force Partitioning** | $O(E)$ | $O(2^V \cdot E)$ | $O(2^V \cdot E)$ | $O(V)$ | $O(1)$ | Low | Non-destructive | Unusable |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Is Graph Bipartite? (LeetCode #785)
 * ============================================================================
 * Core Pattern      : 2-Coloring via Breadth-First Search (Odd-Cycle Detection)
 * Time Complexity   : O(V + E) - Every vertex and edge visited at most twice
 * Space Complexity  : O(V) Auxiliary - Color state array and BFS traversal queue
 * Selection Rule    : BFS with component loop guarantees all disconnected subgraphs are explored;
 *                     iterative execution protects against deep recursion stack overflow.
 * Defensive Traps   : Must loop through all vertices 0 to n - 1 to handle disconnected components/forests;
 *                     use trinary state (0: unvisited, 1: Color A, -1: Color B) to eliminate separate visited set.
 * ============================================================================
 */

using System;
using System.Collections.Generic;

public class Solution
{
    private const int Uncolored = 0;
    private const int ColorA = 1;
    private const int ColorB = -1;

    /// <summary>
    /// Determines whether an undirected graph can be partitioned into two independent sets (bipartite).
    /// Uses BFS graph coloring in O(V + E) time and O(V) auxiliary space.
    /// </summary>
    /// <param name="graph">Adjacency list representation where graph[u] contains neighbors of u.</param>
    /// <returns>True if the graph is bipartite; otherwise, false.</returns>
    public bool IsBipartite(int[][] graph)
    {
        // Guard Clause: Empty graph or singleton node is trivially bipartite
        if (graph == null || graph.Length <= 1)
        {
            return true;
        }

        int n = graph.Length;

        // Trinary color state: 0 = Uncolored, 1 = ColorA, -1 = ColorB
        var colors = new int[n];

        // Pre-allocate queue capacity to prevent dynamic buffer resizing
        var queue = new Queue<int>(capacity: n);

        // Component Loop: Defends against disconnected subgraphs / isolated vertices
        for (int i = 0; i < n; i++)
        {
            if (colors[i] != Uncolored)
            {
                continue;
            }

            // Seed BFS for the unvisited connected component
            colors[i] = ColorA;
            queue.Enqueue(i);

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();
                int currentColor = colors[u];
                int expectedNeighborColor = -currentColor; // Invert color: 1 -> -1, -1 -> 1

                foreach (int v in graph[u])
                {
                    if (colors[v] == Uncolored)
                    {
                        // Assign opposite color and enqueue for neighbor propagation
                        colors[v] = expectedNeighborColor;
                        queue.Enqueue(v);
                    }
                    else if (colors[v] == currentColor)
                    {
                        // Invariant Violation: Monochromatic edge detected -> Odd cycle exists!
                        return false;
                    }
                    // Else: colors[v] == expectedNeighborColor (valid cross-edge, proceed)
                }
            }
        }

        return true;
    }
}

/// <summary>
/// Companion Implementation: Depth-First Search (DFS) 2-Coloring.
/// </summary>
public class SolutionDfs
{
    private const int Uncolored = 0;

    public bool IsBipartite(int[][] graph)
    {
        if (graph == null || graph.Length <= 1) return true;

        int n = graph.Length;
        var colors = new int[n];

        for (int i = 0; i < n; i++)
        {
            if (colors[i] == Uncolored && !DfsColor(i, 1, graph, colors))
            {
                return false;
            }
        }

        return true;
    }

    private static bool DfsColor(int u, int color, int[][] graph, int[] colors)
    {
        colors[u] = color;

        foreach (int v in graph[u])
        {
            if (colors[v] == color)
            {
                return false; // Monochromatic edge
            }

            if (colors[v] == Uncolored && !DfsColor(v, -color, graph, colors))
            {
                return false;
            }
        }

        return true;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **The Disconnected Component Blindspot:** The single most common bug in bipartite verification is starting BFS/DFS only from node `0` (`queue.Enqueue(0)` without an outer loop). If the graph consists of an even component containing node `0` and an isolated triangle $\{3, 4, 5\}$, the single BFS will declare the graph bipartite, producing an erroneous `true`. Always wrap traversal in an outer component sweep `for (int i = 0; i < n; i++)`.
- **Boolean State Array Insufficiency:** Attempting to represent state using `bool[] visited` and `bool[] color` creates ambiguity: an unvisited node defaults to `false`, which is indistinguishable from color `false`. A trinary integer state `0` (unvisited), `1` (Color A), `-1` (Color B) represents visited status and color simultaneously in a single scalar value, eliminating dual-array synchronization bugs and memory overhead.
- **Deep Recursion Call-Stack Exhaustion:** In graphs with $10^5$ nodes structured as a long line graph ($0-1-2-\dots-N$), DFS recursion will create $10^5$ stack frames, crashing with `StackOverflowException`. BFS uses a heap-allocated queue, handling arbitrary graph diameters without exhausting the call stack.
- **Self-Loop and Parallel Edge Invariants:** If inputs come from an unvalidated API, verify that no node contains a self-loop (`v == u`). A self-loop $(u, u)$ creates a cycle of length 1 (odd cycle), instantly rendering the graph non-bipartite.


---

## 163. Count All Valid Pickup and Delivery Options (LeetCode #1359)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | ⚪ Reinforcement |
| **Pattern Tags** | `#math` `#combinatorics` `#dynamic-programming` `#modulo-arithmetic` |
| **LeetCode Link** | [Count All Valid Pickup and Delivery Options](https://leetcode.com/problems/count-all-valid-pickup-and-delivery-options/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given $n$ orders, each order consists of a pickup service $P_i$ and a delivery service $D_i$. Count all valid pickup and delivery sequences such that delivery $D_i$ always occurs strictly after pickup $P_i$ ($P_i < D_i$ in temporal index). Since the answer can be very large, return it modulo $10^9 + 7$.
- **Assumptions & Contracts:**
  - Every order $i \in [1, n]$ has exactly one pickup event $P_i$ and one delivery event $D_i$, yielding exactly $2n$ total events in each sequence.
  - A sequence is valid if and only if for every $i \in [1, n]$, the index of $P_i$ is strictly less than the index of $D_i$.
  - Result must be returned as a 32-bit signed integer in the range $[0, 10^9 + 6]$.
- **Key Constraints:**
  - $1 \le n \le 500$
- **Senior Edge Cases to Defend:**
  - **Base Case ($n = 1$):** Exactly one order: $[P_1, D_1]$. Returns $1$.
  - **Intermediate 64-Bit Arithmetic Overflow:** At $n = 500$, intermediate products `ans * i * (2*i - 1)` easily exceed `int.MaxValue` ($2.14 	imes 10^9$). Calculations must use `long` (64-bit integer) arithmetic before applying modulo $10^9 + 7$ at each iterative step.
  - **Upper Constraint ($n = 500$):** Without modulo, $(1000)! / 2^{500}$ has over 2,400 decimal digits. Continuous modular reduction prevents arbitrary-precision BigInteger allocation.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** This is a classic combinatorial slot-insertion problem. Suppose we have already formed a valid sequence of $n - 1$ orders containing $2(n - 1) = 2n - 2$ events. There are $(2n - 2) + 1 = 2n - 1$ possible slots (before first, between elements, or after last) to insert the new pair $(P_n, D_n)$. If we place both $P_n$ and $D_n$ in the same slot (with $P_n$ immediately preceding $D_n$), there are $2n - 1$ choices. If we place them in two distinct slots, there are $inom{2n - 1}{2}$ choices. In total, there are $(2n - 1) + inom{2n - 1}{2} = n(2n - 1)$ valid ways to insert the $n$-th order into any valid sequence of length $2n - 2$.
- **Sample 1:**
  - **Input:** `n = 1`
  - **Output:** `1`
  - **Explanation:** Only `(P1, D1)` is valid.
- **Sample 2:**
  - **Input:** `n = 2`
  - **Output:** `6`
  - **Explanation:** All valid sequences: `(P1, D1, P2, D2)`, `(P1, P2, D1, D2)`, `(P1, P2, D2, D1)`, `(P2, D2, P1, D1)`, `(P2, P1, D2, D1)`, `(P2, P1, D1, D2)`. Total $= 1 	imes (2 	imes (2 	imes 2 - 1)) = 1 	imes 6 = 6$.
- **Sample 3:**
  - **Input:** `n = 3`
  - **Output:** `90`
  - **Explanation:** $f(3) = f(2) 	imes 3 	imes (2 	imes 3 - 1) = 6 	imes 3 	imes 5 = 90$.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a delivery driver's daily route notebook. You have already scheduled $n - 1$ deliveries into a strict timeline consisting of $2n - 2$ milestone stops. A new customer calls in. You must insert their pickup $P_n$ and delivery $D_n$ into your schedule without altering the order of your existing stops. How many spaces can you write in? There are $2n - 1$ blank spaces (margins and gaps). You can write $P_n$ and $D_n$ right next to each other in any single space ($2n - 1$ options). Or you can pick any two distinct spaces and write $P_n$ in the earlier space and $D_n$ in the later space ($inom{2n - 1}{2}$ options). Because math guarantees $(2n - 1) + rac{(2n - 1)(2n - 2)}{2} = n(2n - 1)$, every existing schedule sprouts exactly $n(2n - 1)$ valid child schedules!

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force solution generates all $(2n)!$ permutations using recursive backtracking, filtering for sequences where index($P_i$) < index($D_i$). For $n = 500$, total permutations is $1000! pprox 4 	imes 10^{2567}$, which would run longer than the age of the universe. Recognizing the combinatorial recurrence collapses the entire problem into a single loop of $n$ iterations running in sub-millisecond $O(N)$ time.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Combinatorial Symmetry Formulation:**
  Consider all $(2n)!$ unconstrained permutations of the $2n$ events $\{P_1, D_1, \dots, P_n, D_n\}$.
  For any specific order $i$, by symmetry, $P_i$ appears before $D_i$ in exactly half ($1/2$) of the permutations.
  Because the relative order of each pair $(P_i, D_i)$ is independent of all other pairs, the fraction of permutations where *every* pair satisfies $P_i < D_i$ is precisely $(1/2)^n$.
  $$	ext{Total Valid Sequences} = rac{(2n)!}{2^n}$$
- **Factorial Product Decomposition:**
  Notice the algebraic factorization:
  $$rac{(2n)!}{2^n} = rac{1 	imes 2 	imes 3 	imes 4 	imes \dots 	imes (2n - 1) 	imes 2n}{2 	imes 2 	imes \dots 	imes 2}$$
  Pair each even factor $2k$ with a $2$ in the denominator: $rac{2k}{2} = k$.
  $$rac{(2n)!}{2^n} = \prod_{k=1}^n k 	imes (2k - 1)$$
- **Inductive Dynamic Programming Recurrence:**
  $$DP[1] = 1$$
  $$DP[k] = (DP[k - 1] 	imes k 	imes (2k - 1)) \pmod{10^9 + 7}$$
  This invariant proves that we never need to compute large factorials or modular inverse division; we can compute the answer purely through forward modular multiplication.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Insertion Space Architecture (Transition from n-1 to n):

Existing Events (length = 2n - 2):
     [ E_1 ]     [ E_2 ]     [ E_3 ] ... [ E_{2n-2} ]
  ^           ^           ^           ^            ^
Slot 1      Slot 2      Slot 3      Slot 4      Slot (2n-1)

Total Available Slots: S = 2n - 1

Placement Strategy:
1. P_n and D_n in same slot:               S choices = 2n - 1
2. P_n and D_n in different slots:  C(S, 2) choices = (2n - 1)(2n - 2) / 2
                                                     = (2n - 1)(n - 1)

Sum of Choices = (2n - 1) * [1 + (n - 1)] = n * (2n - 1)
```

- `i`: Order index progressing from $2$ to $n$.
- `ans`: Cumulative valid sequences modulo $10^9 + 7$.
- `multiplier`: $i 	imes (2i - 1)$, representing the branching factor for order $i$.

#### 3.5 State Transition Triggers & Decision Gates
1. **Base Case Initialization:** Set `long ans = 1`.
2. **Inductive Loop Gate:** Iterate order count $i$ from $2$ to $n$:
   - Calculate available insertion slots: $spaces = 2i - 1$.
   - Calculate combinatorial multiplier: $ways = i 	imes spaces$.
   - Apply modular multiplication: $ans = (ans 	imes ways) \pmod{10^9 + 7}$.
3. **Termination Gate:** Cast `ans` to `int` and return.

#### 3.6 Concrete Step-by-Step State Trace
Trace for $n = 1 \dots 5$ with Modulo $10^9 + 7$:

| Step $i$ | Sequence Length Before ($2i - 2$) | Slots ($2i - 1$) | Multiplier $i(2i - 1)$ | Calculation ($ans_{prev} 	imes 	ext{mult}$) | Cumulative `ans` Modulo $10^9+7$ |
| :---: | :---: | :---: | :---: | :---: | :---: |
| $i = 1$ | $0$ | $1$ | $1 	imes 1 = 1$ | Base Seed | **1** |
| $i = 2$ | $2$ | $3$ | $2 	imes 3 = 6$ | $1 	imes 6 = 6$ | **6** |
| $i = 3$ | $4$ | $5$ | $3 	imes 5 = 15$ | $6 	imes 15 = 90$ | **90** |
| $i = 4$ | $6$ | $7$ | $4 	imes 7 = 28$ | $90 	imes 28 = 2,520$ | **2,520** |
| $i = 5$ | $8$ | $9$ | $5 	imes 9 = 45$ | $2,520 	imes 45 = 113,400$ | **113,400** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Iterative Dynamic Programming / Multiplier Accumulation (Optimal):**
  - Time: $O(N)$ single loop with scalar multiplications.
  - Auxiliary Space: $O(1)$ scalar variables, zero heap allocations.
  - Most robust against overflow and completely eliminates division/modular inverse arithmetic.
- **Approach 2: Factorial with Modular Inverse:**
  - Computes $(2n)! \pmod M$ and multiplies by $(2^n)^{-1} \pmod M$ via Fermat's Little Theorem ($2^{n(M-2)} \pmod M$).
  - Requires $O(N)$ for factorial plus $O(\log M)$ for modular exponentiation. Unnecessary overhead compared to Approach 1.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Contract Validation:** Check that $n \ge 1$. If $n == 1$, return $1$.
2. **Accumulator Initialization:** Declare `long ans = 1` and define constant `MOD = 1_000_000_007`.
3. **Linear Induction:** Loop $i$ from $2$ to $n$:
   - Multiply `ans = (ans * (2L * i - 1) % MOD * i) % MOD`.
4. **Cast & Return:** Return `(int)ans`.

#### 4.3 Alternative Approaches Analysis
- **Permutation Backtracking:** $O((2n)!)$, completely non-viable for $n > 5$.
- **2D DP Memoization `dp[unpicked][undelivered]`:**
  - Let `dp[p][d]` be the number of valid sequences with $p$ pickups remaining and $d$ deliveries remaining.
  - State transitions:
    - Pick an order: $p 	imes dp[p - 1][d]$
    - Deliver an order: $(d - p) 	imes dp[p][d - 1]$ (can only deliver orders that have already been picked).
  - Time: $O(N^2)$, Space: $O(N^2)$. Valid and educational, but strictly inferior to $O(N)$ time and $O(1)$ space.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Iterative DP Multiplier (Selected)** | $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ | $O(1)$ | Optimal (pure registers) | Non-destructive | Online |
| **2. 2D DP State Table `dp[p][d]`** | $O(N^2)$ | $O(N^2)$ | $O(N^2)$ | $O(N^2)$ | $O(1)$ | Moderate | Non-destructive | Offline |
| **3. Factorial + Fermat Modular Inverse** | $O(N + \log M)$ | $O(N + \log M)$ | $O(N + \log M)$ | $O(1)$ | $O(1)$ | Optimal | Non-destructive | Offline |
| **4. Backtracking Generation** | $O((2N)!)$ | $O((2N)!)$ | $O((2N)!)$ | $O(N)$ | $O(1)$ | Low | Non-destructive | Unusable |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Count All Valid Pickup and Delivery Options (LeetCode #1359)
 * ============================================================================
 * Core Pattern      : Combinatorial Dynamic Programming with Modular Arithmetic
 * Time Complexity   : O(N) - Single pass loop from 2 to N performing scalar multiplication
 * Space Complexity  : O(1) Auxiliary - Constant space state registers
 * Selection Rule    : Slot-placement recurrence: dp[i] = dp[i - 1] * i * (2i - 1) mod (10^9 + 7).
 * Defensive Traps   : Intermediate 64-bit integer overflow: cast operands to long before multiplying;
 *                     apply modulo reduction at every step to keep values strictly within [0, MOD - 1].
 * ============================================================================
 */

using System;

public class Solution
{
    private const int Modulo = 1_000_000_007;

    /// <summary>
    /// Computes the count of all valid pickup and delivery permutations for n orders modulo 10^9 + 7.
    /// Operates in optimal O(N) time and O(1) auxiliary space.
    /// </summary>
    /// <param name="n">Number of distinct customer orders.</param>
    /// <returns>Total valid order permutations modulo 10^9 + 7.</returns>
    public int CountOrders(int n)
    {
        // Guard Clause: Contract requires positive order count
        if (n <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(n), "Order count must be strictly positive.");
        }

        // Base case: n = 1 has exactly 1 valid sequence [P1, D1]
        long totalWays = 1;

        // Inductive Accumulator:
        // Inserting the i-th order into existing 2*(i - 1) sequence elements:
        // Available insertion spaces = 2*i - 1.
        // Ways to insert Pi and Di = (2*i - 1) * i.
        for (int i = 2; i <= n; i++)
        {
            long spaces = 2L * i - 1;
            long placementOptions = spaces * i;

            // Defensive modular multiplication to prevent 64-bit overflow
            totalWays = (totalWays * (placementOptions % Modulo)) % Modulo;
        }

        return (int)totalWays;
    }
}

/// <summary>
/// Companion Implementation: 2D State Dynamic Programming dp[unpicked, undelivered].
/// Included for architectural completeness to demonstrate state space exploration.
/// </summary>
public class Solution2DDP
{
    private const int Modulo = 1_000_000_007;

    public int CountOrders(int n)
    {
        if (n <= 0) return 0;

        // memo[unpicked, undelivered]
        var memo = new int[n + 1, n + 1];
        return Compute(n, n, memo);
    }

    private static int Compute(int unpicked, int undelivered, int[,] memo)
    {
        if (unpicked == 0 && undelivered == 0) return 1;
        if (unpicked < 0 || undelivered < 0 || undelivered < unpicked) return 0;

        if (memo[unpicked, undelivered] != 0) return memo[unpicked, undelivered];

        long ways = 0;

        // Option A: Pick an unpicked order
        if (unpicked > 0)
        {
            ways += (long)unpicked * Compute(unpicked - 1, undelivered, memo);
        }

        // Option B: Deliver an order that has already been picked
        int waitingForDelivery = undelivered - unpicked;
        if (waitingForDelivery > 0)
        {
            ways += (long)waitingForDelivery * Compute(unpicked, undelivered - 1, memo);
        }

        return memo[unpicked, undelivered] = (int)(ways % Modulo);
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **32-Bit Integer Multiplication Overflow:** In C#, multiplying two 32-bit integers that exceed $2^{31} - 1$ silently overflows without throwing an exception (in unchecked contexts), wrapping into negative values. If `totalWays` and `placementOptions` are evaluated as 32-bit `int`, the statement `(totalWays * placementOptions) % Modulo` wraps and produces erroneous negative outputs. Declare the accumulator as `long` and use `2L * i - 1`.
- **Premature Factorial Division Trap:** A common mathematical misconception is attempting to calculate $rac{(2n)!}{2^n} \pmod M$ using regular integer division `Factorial(2*n) / Pow(2, n) % MOD`. In modular arithmetic, standard division does not distribute over modulo: $(A / B) \pmod M 
e (A \pmod M) / (B \pmod M)$. Division modulo $M$ requires computing the modular multiplicative inverse $B^{-1} \equiv B^{M - 2} \pmod M$ via Fermat's Little Theorem. The inductive slot multiplication $i(2i - 1)$ completely eliminates the need for division or inverses.
- **Unchecked BigInteger Heap Allocation:** Using `System.Numerics.BigInteger` to compute $(2n)!$ and divide by $2^n$ will pass correctness tests, but dynamically allocates hundreds of heap buffers during digit multiplication, incurring severe memory allocation and garbage collection penalties. The scalar $O(1)$ loop runs in under $1$ microsecond with zero allocations.
- **Boundary Discrepancy ($n=0$):** Ensure contracts defend against $n \le 0$ by throwing `ArgumentOutOfRangeException` rather than returning $1$ or negative values.


---

## 164. Design Underground System (LeetCode #1396)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#design` `#hash-map` `#statistics` `#running-average` `#system-design` |
| **LeetCode Link** | [Design Underground System](https://leetcode.com/problems/design-underground-system/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** An underground railway system is tracking customer travel times between different stations to calculate average transit times. Implement the `UndergroundSystem` class:
  - `void CheckIn(int id, string stationName, int t)`: A customer with card ID `id` checks in at `stationName` at time `t`. A customer can only be checked into one place at a time.
  - `void CheckOut(int id, string stationName, int t)`: A customer with card ID `id` checks out from `stationName` at time `t`.
  - `double GetAverageTime(string startStation, string endStation)`: Returns the average transit duration from `startStation` to `endStation`. The average is computed from all previous direct trips between `startStation` and `endStation`.
- **Assumptions & Contracts:**
  - Calls to `CheckIn` and `CheckOut` are consistent and valid: a customer checks out only after checking in, and cannot check in twice concurrently.
  - All timestamps $t$ are monotonically increasing per customer.
  - Answers within $10^{-5}$ of the actual value will be accepted.
  - `GetAverageTime` will only be called after at least one customer has completed the specified route.
- **Key Constraints:**
  - $1 \le id, t \le 10^6$
  - $1 \le 	ext{stationName.Length}, 	ext{startStation.Length}, 	ext{endStation.Length} \le 10$
  - All station names consist of uppercase and lowercase English letters and digits.
  - At most $2 	imes 10^4$ total calls to `CheckIn`, `CheckOut`, and `GetAverageTime`.
- **Senior Edge Cases to Defend:**
  - **Passenger State Cleanup (Memory Leak Defense):** Customers check in and out repeatedly across days. If check-in entries are not explicitly removed from the active dictionary upon checkout, memory grows unbounded ($O(	ext{Total Trips})$ instead of $O(	ext{Active Passengers})$).
  - **String Allocation & GC Churn in Route Keys:** Constructing keys via string interpolation `$"{startStation}->{endStation}"` allocates new string objects on every checkout and average query, driving Gen 0 garbage collection pauses. Production design uses a zero-allocation `readonly struct RouteKey : IEquatable<RouteKey>`.
  - **Numeric Precision & Overflow:** Total duration must be accumulated using a 64-bit `long` to defend against integer overflow in high-volume production deployments.
  - **Querying Non-Existent Routes:** In untrusted API layers, `GetAverageTime` must defend against routes with zero completed journeys by throwing a descriptive exception or returning a defined fallback.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Decouple ephemeral in-flight transit state from permanent route-level aggregate statistics. Maintain two hash maps:
  1. `_activeCheckIns`: Maps `customerId -> (startStation, checkInTime)` representing in-transit passengers.
  2. `_routeStats`: Maps `RouteKey(start, end) -> (totalDuration, tripCount)` representing completed journey metrics.
  On `CheckIn`: Ingest into `_activeCheckIns`.
  On `CheckOut`: Evict customer from `_activeCheckIns`, compute trip duration $t - t_{start}$, and atomically update the running total and count in `_routeStats`.
  On `GetAverageTime`: Query `_routeStats` and compute $rac{	ext{totalDuration}}{	ext{tripCount}}$ in $O(1)$ time.
- **Sample Execution Trace:**
  - `UndergroundSystem sys = new UndergroundSystem();`
  - `sys.CheckIn(45, "Leyton", 3);`
  - `sys.CheckIn(32, "Paradise", 8);`
  - `sys.CheckOut(45, "Waterloo", 15); // Duration = 15 - 3 = 12`
  - `sys.CheckOut(32, "Cambridge", 22); // Duration = 22 - 8 = 14`
  - `sys.GetAverageTime("Paradise", "Cambridge"); // returns 14.0`
  - `sys.CheckIn(10, "Leyton", 24);`
  - `sys.GetAverageTime("Leyton", "Waterloo"); // returns 12.0`
  - `sys.CheckOut(10, "Waterloo", 38); // Duration = 38 - 24 = 14`
  - `sys.GetAverageTime("Leyton", "Waterloo"); // (12 + 14) / 2 = 13.0`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine the electronic turnstiles of the London Underground or New York Subway. When you tap your contactless card at an entrance gate, a lightweight session token is created in an active commuter cache recording where and when you entered. As you navigate the train tunnels, nothing else needs to track your train. When you tap out at your destination station, the turnstile computer pulls your entry record, calculates the travel time, adds the elapsed minutes and trip counter to a static ledger for that specific route (`Leyton -> Waterloo`), and completely destroys your personal trip record to protect privacy and reclaim RAM.

#### 3.2 The Naive Bottleneck & Redundant Computation
A naive approach stores a complete historical list of every trip duration for each route: `Dictionary<string, List<int>>`. When `GetAverageTime` is called, it iterates through all $K$ stored durations, summing them and dividing by $K$. This causes $O(K)$ time per query, high memory fragmentation, and unbounded heap growth. Storing running cumulative aggregates $(	ext{totalDuration}, 	ext{tripCount})$ collapses `GetAverageTime` to strict $O(1)$ arithmetic with $O(1)$ memory per route.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Online Running Mean Invariant:**
  The arithmetic mean of $K$ samples is:
  $$\mu = rac{1}{K} \sum_{i=1}^K d_i = rac{	ext{TotalDuration}}{	ext{TripCount}}$$
  Because the mean depends only on the sufficient statistics $S = \sum d_i$ and $K$, we only ever need to store the pair $(S, K)$ for each route.
  $$	ext{Update: } S_{new} = S_{old} + \Delta t, \quad K_{new} = K_{old} + 1$$
- **Ephemeral Session Invariant:**
  An entry in `_activeCheckIns` exists if and only if the passenger is currently physically traveling between stations.
  $$|	ext{\_activeCheckIns}| \le 	ext{Max Concurrent Active Passengers}$$
  When a passenger checks out, removing their record immediately guarantees that auxiliary space is strictly bounded by active commuters rather than lifetime system traffic.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Underground System Data Flow Architecture:

1. CheckIn(id: 45, "Leyton", t: 3)
   ┌────────────────────────────────┐
   │ Active Check-In Map            │
   │ [Key: 45] -> ("Leyton", t=3)   │
   └────────────────────────────────┘

2. CheckOut(id: 45, "Waterloo", t: 15)
   Step A: Remove Key 45 -> duration = 15 - 3 = 12
   Step B: RouteKey = ("Leyton", "Waterloo")
   ┌───────────────────────────────────────────────────────────┐
   │ Route Aggregates Map                                      │
   │ [Key: ("Leyton", "Waterloo")] -> Total: 12, Count: 1      │
   └───────────────────────────────────────────────────────────┘

3. GetAverageTime("Leyton", "Waterloo")
   Direct O(1) Probe -> 12 / 1 = 12.0
```

- `_activeCheckIns`: Ephemeral hash table (`int id` $	o$ `(string Station, int Time)`).
- `_routeStats`: Permanent aggregate hash table (`RouteKey` $	o$ `(long TotalDuration, int TripCount)`).

#### 3.5 State Transition Triggers & Decision Gates
1. **CheckIn Gate:**
   - Ingest: `_activeCheckIns[id] = new CheckInRecord(stationName, t)`.
2. **CheckOut Gate:**
   - Probe & Evict: `_activeCheckIns.Remove(id, out var checkIn)`.
   - Calculate duration: $\Delta t = t - checkIn.Timestamp$.
   - Route Key Resolution: Resolve `route = new RouteKey(checkIn.StationName, stationName)`.
   - Aggregate Update: If route exists, update `TotalDuration += duration` and `TripCount++`; else create new aggregate.
3. **GetAverageTime Gate:**
   - Probe: Query `_routeStats.TryGetValue(route, out var stats)`.
   - Evaluate: If found and `TripCount > 0`, return `(double)stats.TotalDuration / stats.TripCount`.

#### 3.6 Concrete Step-by-Step State Trace
Input Operations:
1. `CheckIn(10, "A", 3)`
2. `CheckIn(20, "B", 5)`
3. `CheckOut(10, "B", 10)` ($\Delta t = 7$ on `A->B`)
4. `CheckIn(30, "A", 12)`
5. `CheckOut(30, "B", 25)` ($\Delta t = 13$ on `A->B`)
6. `GetAverageTime("A", "B")`

| Step | Operation | `_activeCheckIns` State | `_routeStats["A->B"]` (Total, Count) | Output Returned |
| :---: | :--- | :--- | :---: | :---: |
| 1 | `CheckIn(10, "A", 3)` | `{ 10: ("A", 3) }` | Empty | `void` |
| 2 | `CheckIn(20, "B", 5)` | `{ 10: ("A", 3), 20: ("B", 5) }` | Empty | `void` |
| 3 | `CheckOut(10, "B", 10)` | `{ 20: ("B", 5) }` (10 evicted!) | `(7, 1)` | `void` |
| 4 | `CheckIn(30, "A", 12)` | `{ 20: ("B", 5), 30: ("A", 12) }` | `(7, 1)` | `void` |
| 5 | `CheckOut(30, "B", 25)` | `{ 20: ("B", 5) }` (30 evicted!) | `(7 + 13 = 20, 2)` | `void` |
| 6 | `GetAverageTime("A", "B")` | `{ 20: ("B", 5) }` | `(20, 2)` | **`10.0`** ($20 / 2$) |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Dual Map with Zero-Allocation Value-Tuple/Struct Key (Optimal):**
  - Uses `Dictionary<int, CheckInRecord>` and `Dictionary<RouteKey, RouteAggregate>`.
  - `RouteKey` implemented as `readonly struct` implementing `IEquatable<RouteKey>` with custom `HashCode.Combine`.
  - Strict $O(1)$ amortized runtime across all three methods. Zero garbage collection heap allocations during lookups.
- **Approach 2: Dual Map with String Concatenation Key:**
  - Uses string keys: `string key = startStation + "->" + endStation`.
  - Correct and simple, but allocates a new string object on every checkout and average query. In heavy production streams ($100,000$ RPS), this induces GC thread pauses.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Type Definitions:** Define `CheckInRecord` (struct) and `RouteKey` (value struct with `Equals` and `GetHashCode`).
2. **Field Declarations:** Instantiate `_activeCheckIns` and `_routeStats`.
3. **CheckIn Method:** Validate inputs; store record in `_activeCheckIns`.
4. **CheckOut Method:** Atomically remove customer record; compute duration; increment route aggregate.
5. **GetAverageTime Method:** Retrieve aggregate for route; return `TotalDuration / (double)TripCount`.

#### 4.3 Alternative Approaches Analysis
- **Full History Log (`Dictionary<Route, List<int>>`):** Takes $O(1)$ for CheckIn/CheckOut, but $O(K)$ for `GetAverageTime` and $O(N)$ space. Severely sub-optimal.
- **ConcurrentDictionary for Multithreaded Execution:** Replaces standard dictionaries with `ConcurrentDictionary<int, CheckInRecord>` and lock-free thread-safe route accumulators. Essential for real-world high-concurrency microservices.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Dual Map + Struct Key (Selected)** | $O(1)$ | $O(1)$ | $O(1)$ | $O(P + R)$ | $O(1)$ | High (value structs) | Ephemeral purge | Optimal (Online streaming) |
| **2. Dual Map + String Key** | $O(1)$ | $O(1)$ | $O(1)$ | $O(P + R)$ + GC | $O(1)$ | Moderate | Ephemeral purge | Online (with GC overhead) |
| **3. Full Trip List Storage** | $O(1)$ | $O(K)$ | $O(K)$ | $O(N)$ (unbounded) | $O(1)$ | Low (fragmented lists) | Append-only | Poor |
| **4. Single Monolithic Event Log** | $O(1)$ | $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ | Low | Append-only | Unusable |

*(Note: $P$ = concurrent active passengers, $R$ = distinct station pair routes, $K$ = trips per route, $N$ = total historical events.)*

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Design Underground System (LeetCode #1396)
 * ============================================================================
 * Core Pattern      : Dual Associative Architecture (Transient Session Map + Route Aggregator)
 * Time Complexity   : O(1) Amortized - CheckIn, CheckOut, and GetAverageTime all execute in O(1)
 * Space Complexity  : O(P + R) Auxiliary - P active in-transit passengers and R distinct travel routes
 * Selection Rule    : Custom readonly struct RouteKey eliminates string concatenation GC allocations;
 *                     sufficient statistics (sum, count) provide O(1) running average.
 * Defensive Traps   : Use Dictionary.Remove to purge transient check-ins upon checkout (prevents memory leaks);
 *                     accumulate duration in 64-bit long to defend against numeric overflow.
 * ============================================================================
 */

using System;
using System.Collections.Generic;

public class UndergroundSystem
{
    /// <summary>
    /// Lightweight value-type representing an in-flight passenger journey.
    /// Struct memory layout avoids heap allocation upon check-in.
    /// </summary>
    private readonly struct CheckInRecord
    {
        public readonly string StationName;
        public readonly int Timestamp;

        public CheckInRecord(string stationName, int timestamp)
        {
            StationName = stationName;
            Timestamp = timestamp;
        }
    }

    /// <summary>
    /// Zero-allocation, high-performance composite key for directional station pairs.
    /// Implements IEquatable to prevent boxing and provide deterministic hash distribution.
    /// </summary>
    private readonly struct RouteKey : IEquatable<RouteKey>
    {
        public readonly string StartStation;
        public readonly string EndStation;

        public RouteKey(string startStation, string endStation)
        {
            StartStation = startStation;
            EndStation = endStation;
        }

        public bool Equals(RouteKey other) =>
            string.Equals(StartStation, other.StartStation, StringComparison.Ordinal) &&
            string.Equals(EndStation, other.EndStation, StringComparison.Ordinal);

        public override bool Equals(object obj) =>
            obj is RouteKey other && Equals(other);

        public override int GetHashCode() =>
            HashCode.Combine(
                StringComparer.Ordinal.GetHashCode(StartStation),
                StringComparer.Ordinal.GetHashCode(EndStation));
    }

    /// <summary>
    /// Running sufficient statistics for a directed route.
    /// Maintains cumulative travel duration and total completed trip count.
    /// </summary>
    private sealed class RouteAggregate
    {
        public long TotalDuration;
        public int TripCount;

        public double AverageTime => (double)TotalDuration / TripCount;
    }

    // Ephemeral commuter ledger: CustomerId -> CheckInRecord
    private readonly Dictionary<int, CheckInRecord> _activeCheckIns;

    // Permanent route aggregate ledger: RouteKey -> RouteAggregate
    private readonly Dictionary<RouteKey, RouteAggregate> _routeStats;

    public UndergroundSystem()
    {
        _activeCheckIns = new Dictionary<int, CheckInRecord>();
        _routeStats = new Dictionary<RouteKey, RouteAggregate>();
    }

    /// <summary>
    /// Registers a passenger entering a station at a given timestamp.
    /// </summary>
    /// <param name="id">Unique passenger card identifier.</param>
    /// <param name="stationName">Departure station name.</param>
    /// <param name="t">Departure timestamp.</param>
    public void CheckIn(int id, string stationName, int t)
    {
        if (string.IsNullOrEmpty(stationName))
        {
            throw new ArgumentException("Station name cannot be null or empty.", nameof(stationName));
        }

        // Overwrites or registers passenger in active transit
        _activeCheckIns[id] = new CheckInRecord(stationName, t);
    }

    /// <summary>
    /// Registers a passenger exiting a station, computes journey duration,
    /// updates route running metrics, and purges the passenger from active memory.
    /// </summary>
    /// <param name="id">Unique passenger card identifier.</param>
    /// <param name="stationName">Arrival station name.</param>
    /// <param name="t">Arrival timestamp.</param>
    public void CheckOut(int id, string stationName, int t)
    {
        // Defensive Verification & Ephemeral Cleanup:
        // Remove passenger from active ledger to defend against unbounded memory leaks
        if (!_activeCheckIns.Remove(id, out var checkIn))
        {
            throw new InvalidOperationException($"Passenger {id} has no active check-in record.");
        }

        long duration = t - checkIn.Timestamp;
        var route = new RouteKey(checkIn.StationName, stationName);

        if (!_routeStats.TryGetValue(route, out var aggregate))
        {
            aggregate = new RouteAggregate();
            _routeStats[route] = aggregate;
        }

        aggregate.TotalDuration += duration;
        aggregate.TripCount++;
    }

    /// <summary>
    /// Computes the arithmetic mean of all completed trips between two stations in O(1) time.
    /// </summary>
    /// <param name="startStation">Departure station name.</param>
    /// <param name="endStation">Arrival station name.</param>
    /// <returns>Average travel duration in seconds/units.</returns>
    public double GetAverageTime(string startStation, string endStation)
    {
        var route = new RouteKey(startStation, endStation);
        if (_routeStats.TryGetValue(route, out var aggregate) && aggregate.TripCount > 0)
        {
            return aggregate.AverageTime;
        }

        throw new InvalidOperationException($"No completed journeys recorded between {startStation} and {endStation}.");
    }
}

/// <summary>
/// Companion Enterprise Implementation: Thread-Safe Concurrent Underground System.
/// Designed for high-throughput multi-threaded transit microservices.
/// </summary>
public class ConcurrentUndergroundSystem
{
    private readonly struct RouteKey : IEquatable<RouteKey>
    {
        public readonly string Start;
        public readonly string End;

        public RouteKey(string start, string end)
        {
            Start = start;
            End = end;
        }

        public bool Equals(RouteKey other) =>
            string.Equals(Start, other.Start, StringComparison.Ordinal) &&
            string.Equals(End, other.End, StringComparison.Ordinal);

        public override bool Equals(object obj) => obj is RouteKey other && Equals(other);

        public override int GetHashCode() =>
            HashCode.Combine(
                StringComparer.Ordinal.GetHashCode(Start),
                StringComparer.Ordinal.GetHashCode(End));
    }

    private sealed class ThreadSafeAggregate
    {
        private long _totalDuration;
        private int _tripCount;

        public void AddTrip(long duration)
        {
            System.Threading.Interlocked.Add(ref _totalDuration, duration);
            System.Threading.Interlocked.Increment(ref _tripCount);
        }

        public double GetAverage()
        {
            long total = System.Threading.Interlocked.Read(ref _totalDuration);
            int count = System.Threading.Volatile.Read(ref _tripCount);
            return count == 0 ? 0.0 : (double)total / count;
        }
    }

    private readonly System.Collections.Concurrent.ConcurrentDictionary<int, (string Station, int Time)> _checkIns;
    private readonly System.Collections.Concurrent.ConcurrentDictionary<RouteKey, ThreadSafeAggregate> _stats;

    public ConcurrentUndergroundSystem()
    {
        _checkIns = new System.Collections.Concurrent.ConcurrentDictionary<int, (string, int)>();
        _stats = new System.Collections.Concurrent.ConcurrentDictionary<RouteKey, ThreadSafeAggregate>();
    }

    public void CheckIn(int id, string stationName, int t) =>
        _checkIns[id] = (stationName, t);

    public void CheckOut(int id, string stationName, int t)
    {
        if (_checkIns.TryRemove(id, out var record))
        {
            long duration = t - record.Time;
            var route = new RouteKey(record.Station, stationName);
            var agg = _stats.GetOrAdd(route, _ => new ThreadSafeAggregate());
            agg.AddTrip(duration);
        }
    }

    public double GetAverageTime(string startStation, string endStation)
    {
        var route = new RouteKey(startStation, endStation);
        return _stats.TryGetValue(route, out var agg) ? agg.GetAverage() : 0.0;
    }
}
```

---

### 6. Senior Pitfalls & Defensive Traps
- **Unbounded Memory Leak from Orphaned Sessions:** In naive implementations, developers use `_activeCheckIns[id]` to store trips and simply overwrite on subsequent check-ins without removing upon checkout. In a long-running 24/7 cloud service, every customer who ever used the subway remains in the dictionary forever, causing continuous memory bloat until the process crashes with `OutOfMemoryException`. Calling `_activeCheckIns.Remove(id, out var checkIn)` guarantees that memory usage is proportional only to *active concurrent passengers*.
- **String Concatenation GC Allocation Churn:** Keys created via `startStation + "_" + endStation` allocate a new `string` object on the heap on every checkout and every average query. Under a high load of $20,000$ operations, this produces tens of megabytes of short-lived heap allocations, triggering Gen 0/1 GC pauses. Encapsulating the pair into a `readonly struct RouteKey` with custom `GetHashCode()` and `Equals()` achieves zero heap allocations.
- **Integer Truncation in Running Average:** Performing integer division `aggregate.TotalDuration / aggregate.TripCount` truncates fractional seconds, failing precision tests. Always cast the numerator to `double`: `(double)TotalDuration / TripCount`.
- **Directional Asymmetry Violation:** Route `A -> B` is physically distinct from route `B -> A` (e.g. uphill vs downhill, different track congestion). The route key must strictly preserve directionality: `RouteKey("Leyton", "Waterloo")` must not match `RouteKey("Waterloo", "Leyton")`. The custom `RouteKey` struct guarantees direction-preserving equality and hash codes.

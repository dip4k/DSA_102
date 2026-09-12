# Phase 06: Linked Lists

> **Focus:** Pointer Rewiring Invariants, Sentinel Dummy Nodes, Fast & Slow Pointers (Floyd's Tortoise and Hare), In-Place Reversal, Interleaved Merging, K-Way Merge with Priority Queues, and Subsegment K-Group Reversals.  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 6 (Problems #29–#35)

---

## ListNode Definition

```csharp
public class ListNode
{
    public int val;
    public ListNode next;
    public ListNode(int val = 0, ListNode next = null)
    {
        this.val = val;
        this.next = next;
    }
}
```

---

## 29. Reverse Linked List (LeetCode #206)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#linked-list` `#pointer-reversal` `#iterative-recursive` |
| **LeetCode Link** | [Reverse Linked List](https://leetcode.com/problems/reverse-linked-list/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given the `head` of a singly linked list, reverse the list, and return the reversed list.
- **Key Constraints:**
  - The number of nodes in the list is in the range $[0, 5000]$.
  - `-5000 <= Node.val <= 5000`
  - Follow-up: Can you reverse it both iteratively and recursively?

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Invert the directed edge between every adjacent pair of nodes so that each node points to its predecessor rather than its successor.
- **Sample 1:**
  - **Input:** `head = [1, 2, 3, 4, 5]`
  - **Output:** `[5, 4, 3, 2, 1]`
- **Sample 2:**
  - **Input:** `head = [1, 2]`
  - **Output:** `[2, 1]`
- **Sample 3:**
  - **Input:** `head = []`
  - **Output:** `[]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a line of train cars coupled in one direction: each car has a latch hooking only onto the car ahead. You stand between two cars. To invert the train's travel direction, you must decouple the forward link and reconnect it backward. However, the moment you unhook the car in front of you, the rest of the train rolls away into the void unless you hold onto it with a third hand. Thus, reversing a singly linked list is fundamentally an exercise in **safely caching the forward universe** (`nextTemp`) before inverting the current local coupling (`curr.next = prev`).

#### 3.2 The Naive Bottleneck & Redundant Computation
A naive approach pushes all node references onto an auxiliary LIFO stack or extracts all values into a dynamic array (`O(N)` space), and then reconstructs a brand-new list or overwrites values. This incurs $O(N)$ auxiliary heap memory allocations, triggers garbage collection pressure, and does not alter the underlying structural node links. A brute-force pointer chase that seeks the tail node on every iteration to build a reversed chain degrades to quadratic time:
$$T(N) = \sum_{k=1}^N k = \frac{N(N+1)}{2} \implies O(N^2)$$
Eliminating this redundancy requires an in-place, single-pass $O(N)$ rewiring where each pointer is redirected exactly once.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
We maintain an invariant partition splitting the linked list into two disjoint sublists at every step:
1. **Reversed Prefix:** A valid reversed list ending at `null` with its new head pointed to by `prev`.
2. **Unreversed Suffix:** The remaining original list starting at `curr`.

The loop invariant states:
$$\forall \text{ node } u \in \text{Reversed Prefix}, \quad u.\text{next} = \text{predecessor}(u)$$
$$\forall \text{ node } v \in \text{Unreversed Suffix}, \quad v.\text{next} = \text{successor}(v)$$
By caching `curr.next` into `nextTemp` before overwriting `curr.next = prev`, we safely transfer node `curr` from the head of the unreversed suffix to become the new head of the reversed prefix without losing the unreversed chain.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
INITIAL STATE:
prev = null, curr = Node(1)

DURING ITERATION:
[ Reversed Prefix (Settled) ]       [ Active Node ]       [ Unreversed Suffix (Unexplored) ]
... <- (1) <- prev                  curr                  nextTemp -> (3) -> (4) -> null
└─────────┬─────────┘               └───┬───┘             └───────────────┬────────────────┘
  Fully inverted links              Rewiring target       Preserved forward link

TRANSITION STEP:
curr.next = prev  ===>  prev <- curr (prepended to settled prefix)
prev = curr       ===>  advances settled boundary to curr
curr = nextTemp   ===>  advances active cursor to unexplored head
```

- `prev`: Points to the head of the already reversed sublist (Settled territory). Initially `null`.
- `curr`: Points to the active candidate node undergoing edge inversion.
- `nextTemp`: Ephemeral lookahead pointer securing access to the unexplored remainder.

#### 3.5 State Transition Triggers & Decision Gates
At each step of `while (curr != null)`:
1. **Cache Gate:** `nextTemp = curr.next` (Mandatory: do not decouple `curr` before caching `nextTemp`).
2. **Rewire Gate:** `curr.next = prev` (Invert direction; `curr` now points backward).
3. **Advance Settled Frontier:** `prev = curr` (`curr` becomes the new head of the reversed prefix).
4. **Advance Exploration Cursor:** `curr = nextTemp` (`curr` steps to the next unreversed node).
5. **Termination Condition:** When `curr == null`, all nodes have crossed into the settled partition. Return `prev`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `head = [1 -> 2 -> 3 -> null]`

| Step | `prev` | `curr` | `nextTemp` | Rewiring Action (`curr.next`) | Resulting Reversed Chain |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Init** | `null` | `1` | — | None | `null` |
| **Iter 1** | `null` | `1` | `2` | `1.next = null` | `1 -> null` |
| **After 1** | `1` | `2` | `2` | State shift: `prev=1, curr=2` | `1 -> null` |
| **Iter 2** | `1` | `2` | `3` | `2.next = 1` | `2 -> 1 -> null` |
| **After 2** | `2` | `3` | `3` | State shift: `prev=2, curr=3` | `2 -> 1 -> null` |
| **Iter 3** | `2` | `3` | `null` | `3.next = 2` | `3 -> 2 -> 1 -> null` |
| **After 3** | `3` | `null`| `null`| State shift: `prev=3, curr=null` | `3 -> 2 -> 1 -> null` |
| **End** | `3` | `null`| — | Loop terminates (`curr == null`) | Return `prev` (`3`) |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Iterative Three-Pointer):** The industry standard for production systems. Operates in strict $O(1)$ auxiliary space, eliminates stack overflow vulnerabilities, and optimizes CPU instruction pipelining.
- **Approach 2 (Recursive Post-Order):** Elegant mathematical formulation utilizing the call stack as an implicit storage mechanism. Crucial for understanding post-order tree traversals and backtracking, but incurs $O(N)$ call stack memory, making it vulnerable to stack overflow on deep lists ($N > 5000$).

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Initialize `prev = null` and `curr = head`. Check for empty list or single-node list (where loop terminates immediately or executes once safely).
- **Step 2: Main Exploration Loop:** Loop as long as `curr != null`.
- **Step 3: Invariant Maintenance & Condition Gates:** Cache `curr.next`, rewire `curr.next = prev`, shift `prev = curr`, shift `curr = nextTemp`.
- **Step 4: Resolution & Return:** When `curr` falls off the end (`null`), `prev` points to the old tail, which is the new head. Return `prev`.

#### 4.3 Alternative Approaches Analysis
- **Recursive Post-Order Inversion:**
  - Base case: `if (head == null || head.next == null) return head;`
  - Inductive step: Recursively reverse the sublist starting at `head.next`:
    `ListNode newHead = ReverseList(head.next);`
  - Rewiring step: At this point, `head.next` is the tail of the newly reversed sublist. We wire `head.next.next = head` and break the old forward link with `head.next = null`.
  - Return `newHead` up the stack.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Iterative Three-Pointer (Optimal) | Approach 2: Recursive Post-Order |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | `O(N)` / `O(N)` / `O(N)` | `O(N)` / `O(N)` / `O(N)` |
| **Auxiliary Space** | `O(1)` strictly | `O(N)` stack frames |
| **Output Space** | `O(1)` (in-place rewiring) | `O(1)` (in-place rewiring) |
| **Cache Locality** | High spatial locality along traversal | Moderate (call stack thrashing) |
| **In-Place Mutability** | In-place destructive mutator | In-place destructive mutator |
| **Streaming Suitability** | High (single pass, immediate output pointer) | Low (must reach terminal node before rewiring) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #29 - Reverse Linked List
// Core Pattern: Three-Pointer In-Place Edge Inversion
// Primary Invariant: prev anchors reversed prefix; curr anchors unreversed suffix.
// Overflow Defense: Iterative avoids call-stack exhaustion on lists of N >= 10^5.
// ============================================================================
```

#### Implementation 1: Iterative Three-Pointer (Optimal `O(1)` Space)
```csharp
public class Solution
{
    public ListNode ReverseList(ListNode head)
    {
        // Sentinel/Boundary Check: Empty list or single node requires no rewiring
        if (head == null || head.next == null)
        {
            return head;
        }

        ListNode prev = null;       // Settled frontier: head of reversed prefix
        ListNode curr = head;       // Active frontier: node being redirected

        // Invariant: Nodes before 'curr' are fully reversed with head at 'prev'
        while (curr != null)
        {
            // Gate 1: Cache the forward pointer before decoupling
            ListNode nextTemp = curr.next;

            // Gate 2: Invert the local pointer to reference predecessor
            curr.next = prev;

            // Gate 3: Advance settled frontier to include curr
            prev = curr;

            // Gate 4: Step forward into unexplored territory
            curr = nextTemp;
        }

        // 'prev' now rests on the former tail, which is the new head of the reversed list
        return prev;
    }
}
```

#### Implementation 2: Recursive Post-Order (`O(N)` Call Stack)
```csharp
public class SolutionRecursive
{
    public ListNode ReverseList(ListNode head)
    {
        // Base Case: Empty list or reached the original tail node
        if (head == null || head.next == null)
        {
            return head; // This tail becomes the new overall head
        }

        // Recursive Step: Reverse the remaining sublist; newHead propagates up unchanged
        ListNode newHead = ReverseList(head.next);

        // Rewire Invariant: head.next is currently the tail of the reversed sublist.
        // Direct its next pointer back to head.
        head.next.next = head;

        // Decouple original forward link to eliminate circular reference
        head.next = null;

        return newHead;
    }
}
```

---

## 30. Merge Two Sorted Lists (LeetCode #21)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⚪ Reinforcement |
| **Pattern Tags** | `#linked-list` `#sentinel-node` `#two-pointers` |
| **LeetCode Link** | [Merge Two Sorted Lists](https://leetcode.com/problems/merge-two-sorted-lists/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given the heads of two sorted linked lists `list1` and `list2`. Merge the two lists into one sorted list by splicing together the nodes of the first two lists. Return the head of the merged linked list.
- **Key Constraints:**
  - The number of nodes in both lists is in the range $[0, 50]$.
  - `-100 <= Node.val <= 100`
  - Both `list1` and `list2` are sorted in non-decreasing order.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Zipper-merge two monotonically non-decreasing linked sequences into a single sorted chain by comparing heads and splicing pointers in-place.
- **Sample 1:**
  - **Input:** `list1 = [1, 2, 4]`, `list2 = [1, 3, 4]`
  - **Output:** `[1, 1, 2, 3, 4, 4]`
- **Sample 2:**
  - **Input:** `list1 = []`, `list2 = []`
  - **Output:** `[]`
- **Sample 3:**
  - **Input:** `list1 = []`, `list2 = [0]`
  - **Output:** `[0]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Picture two sorted conveyor belts delivering items to a single packing station. At each instant, the operator looks at the items at the front of both belts, picks the smaller one, and attaches it to the end of the finished line. A **sentinel dummy node** acts as a permanent immovable anchor peg driven into the ground: rather than writing complex conditional branches to figure out which belt provides the very first item (the true head), we anchor our building chain to the dummy node and let the cursor append uniformly.

#### 3.2 The Naive Bottleneck & Redundant Computation
A naive approach might extract all values from both lists into a dynamic array, call `Array.Sort` ($O((M+N) \log(M+N))$ time), and allocate an entirely new linked list ($O(M+N)$ space). This completely discards the pre-existing sorted invariant of both lists and creates unnecessary heap allocations. Furthermore, without a sentinel node, an implementation must branch on every iteration to check `if (mergedHead == null)`, adding redundant condition checks.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
Both lists are pre-sorted:
$$list1_0 \le list1_1 \le \dots \le list1_{M-1}, \quad list2_0 \le list2_1 \le \dots \le list2_{N-1}$$
By comparing only the active heads `p1.val` and `p2.val`, we greedily select $\min(p1.val, p2.val)$, maintaining the sorted invariant of the merged list. Crucially, when one list is exhausted, the remaining elements of the other list are already sorted and strictly greater than or equal to all merged elements; we can attach the entire remainder in a single $O(1)$ pointer assignment:
$$\text{tail.next} = (p1 \neq \text{null}) \ ? \ p1 : p2$$

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
SENTINEL ANCHOR & CURSOR ARCHITECTURE:

[ dummy (val: -1) ] -> [ 1 ] -> [ 1 ] -> [ tail ]
                         ▲                 ▲
                         └─ Settled Chain ─┘
                                           tail.next -> ?

ACTIVE STREAM 1: p1 -> [ 2 ] -> [ 4 ] -> null
ACTIVE STREAM 2: p2 -> [ 3 ] -> [ 4 ] -> null

DECISION:
p1.val (2) <= p2.val (3) ==> tail.next = p1; p1 = p1.next; tail = tail.next
```

- `dummy`: Invariant anchor node preceding the merged list. Eliminates null-head edge cases.
- `tail`: Scribe pointer tracking the last node of the settled merged chain.
- `p1`, `p2`: Traversal cursors scanning `list1` and `list2`.

#### 3.5 State Transition Triggers & Decision Gates
At each step of `while (p1 != null && p2 != null)`:
- **Decision Gate:** If `p1.val <= p2.val`:
  - `tail.next = p1` (splice `p1` to merged tail).
  - `p1 = p1.next` (advance stream 1).
- **Else:**
  - `tail.next = p2` (splice `p2` to merged tail).
  - `p2 = p2.next` (advance stream 2).
- **Tail Advance:** `tail = tail.next` (maintain tail invariant).
- **Residual Splice Gate:** Once the loop terminates, exactly one of `p1` or `p2` may be non-null. Execute `tail.next = p1 ?? p2`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `list1 = [1, 2, 4]`, `list2 = [1, 3, 4]`

| Step | `p1.val` | `p2.val` | Decision (`<=`) | Spliced Node | `tail` Position | Remaining Streams |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Init** | `1` | `1` | `p1 <= p2` | Node `1` (from L1) | `dummy -> 1(L1)` | L1: `[2, 4]`, L2: `[1, 3, 4]` |
| **Iter 1** | `2` | `1` | `p2 < p1` | Node `1` (from L2) | `... -> 1(L2)` | L1: `[2, 4]`, L2: `[3, 4]` |
| **Iter 2** | `2` | `3` | `p1 <= p2` | Node `2` (from L1) | `... -> 2(L1)` | L1: `[4]`, L2: `[3, 4]` |
| **Iter 3** | `4` | `3` | `p2 < p1` | Node `3` (from L2) | `... -> 3(L2)` | L1: `[4]`, L2: `[4]` |
| **Iter 4** | `4` | `4` | `p1 <= p2` | Node `4` (from L1) | `... -> 4(L1)` | L1: `null`, L2: `[4]` |
| **Residual**| `null`| `4` | Loop exited | Attach `p2` (`4`) | `... -> 4(L2)` | Done in $O(1)$ |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Iterative with Sentinel Dummy Node):** Optimal $O(1)$ space, zero heap allocation overhead, highly defensive against empty lists. Recommended for all production systems.
- **Approach 2 (Recursive Divide & Conquer):** Concise code representation matching mathematical induction. Incurs $O(M + N)$ call stack memory, creating risk of stack overflow when lists contain thousands of elements.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Instantiate a sentinel `dummy` node. Initialize `tail = dummy`.
- **Step 2: Main Exploration Loop:** While both `p1 != null` and `p2 != null`, compare values and splice the smaller node to `tail.next`. Advance both the chosen list cursor and `tail`.
- **Step 3: Residual Splicing:** Once either list is exhausted, assign `tail.next = p1 ?? p2`.
- **Step 4: Resolution & Return:** The merged list starts at `dummy.next`. Return `dummy.next`.

#### 4.3 Alternative Approaches Analysis
- **Recursive Merge Formulation:**
  - Base cases: If `list1 == null`, return `list2`. If `list2 == null`, return `list1`.
  - Relation:
    $$\text{Merge}(L_1, L_2) = \begin{cases} L_1 \to \text{Merge}(L_1.\text{next}, L_2) & \text{if } L_1.\text{val} \le L_2.\text{val} \\ L_2 \to \text{Merge}(L_1, L_2.\text{next}) & \text{otherwise} \end{cases}$$
  - Call stack depth equals $M + N$.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Iterative Sentinel (Optimal) | Approach 2: Recursive Merge |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | `O(min(M,N))` / `O(M+N)` / `O(M+N)` | `O(min(M,N))` / `O(M+N)` / `O(M+N)` |
| **Auxiliary Space** | `O(1)` strictly | `O(M + N)` call stack |
| **Output Space** | `O(1)` (in-place pointer splicing) | `O(1)` (in-place pointer splicing) |
| **Cache Locality** | High linear pointer traversal | Low (interleaved activation frames) |
| **In-Place Mutability** | Reuses existing nodes without reallocation | Reuses existing nodes |
| **Streaming Suitability** | High (can yield elements lazily) | Low (requires call stack evaluation) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #30 - Merge Two Sorted Lists
// Core Pattern: Two-Pointer Zipper Merge with Sentinel Dummy Anchor
// Primary Invariant: tail.next is always spliced to min(p1.val, p2.val)
// Residual Optimization: Splicing remaining sublist in O(1) time
// ============================================================================
```

#### Implementation 1: Iterative with Sentinel (Optimal `O(1)` Space)
```csharp
public class Solution
{
    public ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        // Sentinel dummy node simplifies head initialization and boundary branches
        ListNode dummy = new ListNode(-1);
        ListNode tail = dummy; // Scribe pointer for the merged chain

        ListNode p1 = list1;
        ListNode p2 = list2;

        // Invariant: Both streams have valid candidate nodes; choose the minimum
        while (p1 != null && p2 != null)
        {
            if (p1.val <= p2.val)
            {
                tail.next = p1; // Splice p1 into merged sequence
                p1 = p1.next;   // Advance stream 1
            }
            else
            {
                tail.next = p2; // Splice p2 into merged sequence
                p2 = p2.next;   // Advance stream 2
            }

            tail = tail.next;   // Advance scribe cursor
        }

        // Invariant: Exactly one stream may have residual sorted nodes.
        // Splice the remaining sublist in O(1) operations.
        tail.next = p1 ?? p2;

        return dummy.next; // Head of merged list resides immediately after dummy
    }
}
```

#### Implementation 2: Recursive Merge (`O(M + N)` Stack Space)
```csharp
public class SolutionRecursive
{
    public ListNode MergeTwoLists(ListNode list1, ListNode list2)
    {
        // Base Cases: If either list is null, return the other list directly
        if (list1 == null) return list2;
        if (list2 == null) return list1;

        // Inductive Step: Pick smaller node and recurse on its successor
        if (list1.val <= list2.val)
        {
            list1.next = MergeTwoLists(list1.next, list2);
            return list1;
        }
        else
        {
            list2.next = MergeTwoLists(list1, list2.next);
            return list2;
        }
    }
}
```

---

## 31. Linked List Cycle (LeetCode #141)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#linked-list` `#fast-slow-pointers` `#floyd-tortoise-hare` |
| **LeetCode Link** | [Linked List Cycle](https://leetcode.com/problems/linked-list-cycle/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given `head`, the head of a linked list, determine if the linked list has a cycle in it. Return `true` if there is a cycle, otherwise return `false`.
- **Strict Constraint:** Solve it using $O(1)$ (i.e. constant) memory.
- **Key Constraints:**
  - The number of the nodes in the list is in the range $[0, 10^4]$.
  - `-10^5 <= Node.val <= 10^5`
  - `pos` is `-1` or a valid index in the linked-list.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Detect if a closed loop exists within a singly linked chain without allocating memory or mutating the underlying node structures.
- **Sample 1:**
  - **Input:** `head = [3, 2, 0, -4]`, `pos = 1` (tail links to node index 1)
  - **Output:** `true`
- **Sample 2:**
  - **Input:** `head = [1, 2]`, `pos = 0`
  - **Output:** `true`
- **Sample 3:**
  - **Input:** `head = [1]`, `pos = -1`
  - **Output:** `false`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Envision two runners on a running track: a Tortoise moving at speed $1$ and a Hare moving at speed $2$. If the track is a straight open highway, the Hare will reach the end (`null`) and the run will terminate. However, if the track loops back into a closed circuit, both runners will eventually enter the loop. Because the Hare runs twice as fast as the Tortoise, the distance between them shrinks by exactly $1$ meter per second inside the loop. The Hare is mathematically guaranteed to lap and collide with the Tortoise without ever skipping past it.

#### 3.2 The Naive Bottleneck & Redundant Computation
The naive approach uses a hash set (`HashSet<ListNode>`) to store the reference of every visited node. On each step, it checks whether the current node exists in the set:
- **Time:** $O(N)$
- **Space:** $O(N)$ auxiliary memory allocations
This violates the strict $O(1)$ memory constraint, allocates heap memory for thousands of pointers, and triggers significant garbage collector overhead. Another flawed naive approach marks visited nodes by altering their values to a magic sentinel (e.g. `val = int.MinValue`), which corrupts input data and fails in multi-threaded read environments.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
Floyd's Cycle-Finding Algorithm (Tortoise and Hare) proves that a speed differential of $1$ guarantees collision inside a cycle:
1. Let the non-cycle linear segment have length $m$.
2. Let the cycle have length $C$.
3. When `slow` reaches the start of the cycle (after $m$ steps), `fast` has taken $2m$ steps and is already at some position $k = (2m - m) \pmod C = m \pmod C$ inside the cycle.
4. The distance from `fast` to `slow` along the direction of traversal is $d = (C - (m \pmod C)) \pmod C$.
5. On each subsequent iteration:
   - `slow` advances $1$ step.
   - `fast` advances $2$ steps.
   - The relative distance between `fast` and `slow` decreases by:
     $$\Delta d = 2 - 1 = 1 \text{ node per iteration}$$
6. Because $\Delta d = 1$ is an integer that divides any cycle length $C$, `fast` cannot leap over `slow`. Collision occurs within at most $C$ iterations after `slow` enters the cycle. Total iterations $\le m + C = N \implies O(N)$ time.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
STEM (Length m)                     CYCLE (Length C)
head -> (0) -> (1) -> ... -> [ Entry ] -> (e1) -> (e2)
                               ▲                   │
                               │                   ▼
                             (e4) <────────────── (e3)

INVARIANT AT TICK t:
distance(slow) = t
distance(fast) = 2t
Inside cycle: relative distance decreases strictly by 1 per tick:
d_{t+1} = (d_t - 1) mod C
```

- `slow`: Advances $1$ node per iteration.
- `fast`: Advances $2$ nodes per iteration. Lookahead guard: `fast != null && fast.next != null`.

#### 3.5 State Transition Triggers & Decision Gates
1. **Boundary Guard:** If `head == null || head.next == null`, return `false`.
2. **Loop Condition:** `while (fast != null && fast.next != null)`
   - If `fast` or `fast.next` hits `null`, the list is acyclic; loop terminates and returns `false`.
3. **Advance Gate:**
   - `slow = slow.next`
   - `fast = fast.next.next`
4. **Collision Gate:**
   - `if (slow == fast)` $\implies$ cycle detected, return `true`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `head = [3 -> 2 -> 0 -> -4 -> (back to 2)]` ($m=1, C=3$)

| Tick | `slow` Node (`val`) | `fast` Node (`val`) | Distance Apart in Cycle | Collision Check (`slow == fast`) |
| :--- | :--- | :--- | :--- | :--- |
| **0** | Node `3` | Node `3` | Outside cycle | Not checked at entry |
| **1** | Node `2` (cycle entry)| Node `0` | Fast is 1 ahead ($d=2$ behind) | `2 != 0` |
| **2** | Node `0` | Node `2` (cycled) | Fast is 2 ahead ($d=1$ behind) | `0 != 2` |
| **3** | Node `-4`| Node `-4` (cycled)| Fast caught slow ($d=0$) | `slow == fast` $\implies$ **Return `true`** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Floyd's Fast & Slow Pointers):** Optimal $O(1)$ space, zero data mutation, highly defensible in senior engineering interviews. Standard choice for embedded and systems-level programming.
- **Approach 2 (HashSet Visited Set):** Simple to reason about, but incurs $O(N)$ auxiliary memory and garbage collection pressure. Only appropriate when nodes can be uniquely identified and memory is unconstrained.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Guard against null head or single isolated node. Initialize `slow = head`, `fast = head`.
- **Step 2: Main Exploration Loop:** Loop while `fast != null && fast.next != null`.
- **Step 3: Invariant Maintenance & Condition Gates:** Advance `slow` by 1, `fast` by 2. Check if `slow == fast`.
- **Step 4: Resolution & Return:** If loop exits, fast hit `null` $\implies$ return `false`. If collision occurs, return `true`.

#### 4.3 Alternative Approaches Analysis
- **Node Mutation Flagging:** Overwrite `node.val` with a sentinel constant or point `node.next` to a designated sentinel dummy node.
  - *Major Flaw:* Destroys the data structure, makes it impossible to restore on read-only threads, and causes catastrophic side effects in concurrent systems.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Floyd's Tortoise & Hare (Optimal) | Approach 2: HashSet Visited Set |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | `O(1)` (acyclic short) / `O(N)` / `O(N)` | `O(1)` / `O(N)` / `O(N)` |
| **Auxiliary Space** | `O(1)` strictly | `O(N)` heap allocations |
| **Output Space** | `O(1)` boolean | `O(1)` boolean |
| **Cache Locality** | High (streaming pointer lookahead) | Low (hash table node pointer lookups) |
| **In-Place Mutability** | Non-destructive read-only | Non-destructive read-only |
| **Streaming Suitability** | High (infinite streams can be checked) | Low (infinite cycle causes OutOfMemoryException) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #31 - Linked List Cycle
// Core Pattern: Floyd's Tortoise and Hare (Fast & Slow Pointers)
// Primary Invariant: Relative speed differential of 1 node/step forces collision
// Memory Guarantee: Strictly O(1) auxiliary space; non-destructive read traversal
// ============================================================================
```

#### Implementation 1: Floyd's Fast & Slow Pointers (Optimal `O(1)` Space)
```csharp
public class Solution
{
    public bool HasCycle(ListNode head)
    {
        // Boundary Defense: An empty list or single isolated node cannot contain a cycle
        if (head == null || head.next == null)
        {
            return false;
        }

        ListNode slow = head; // Advances 1 node per iteration
        ListNode fast = head; // Advances 2 nodes per iteration

        // Invariant: Fast pointer safely scouts two steps ahead.
        // If fast encounters null, list is linear and acyclic.
        while (fast != null && fast.next != null)
        {
            slow = slow.next;       // Advance 1 step
            fast = fast.next.next;  // Advance 2 steps

            // Collision Gate: If fast laps slow, a cycle is mathematically proven
            if (slow == fast)
            {
                return true;
            }
        }

        // Fast pointer reached the terminal null boundary
        return false;
    }
}
```

#### Implementation 2: HashSet Visited Nodes (`O(N)` Auxiliary Space)
```csharp
public class SolutionHashSet
{
    public bool HasCycle(ListNode head)
    {
        if (head == null || head.next == null) return false;

        // Track node references in a hash table
        var visited = new HashSet<ListNode>();
        ListNode curr = head;

        while (curr != null)
        {
            // If Add returns false, the node pointer already exists in the set
            if (!visited.Add(curr))
            {
                return true;
            }
            curr = curr.next;
        }

        return false;
    }
}
```

---

## 32. Remove Nth Node From End of List (LeetCode #19)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#linked-list` `#fast-slow-gap` `#sentinel-node` |
| **LeetCode Link** | [Remove Nth Node From End of List](https://leetcode.com/problems/remove-nth-node-from-end-of-list/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given the `head` of a linked list, remove the $n$-th node from the end of the list and return its head in a single pass.
- **Key Constraints:**
  - The number of nodes in the list is $sz$.
  - $1 \le sz \le 30$
  - $0 \le \text{Node.val} \le 100$
  - $1 \le n \le sz$

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Maintain a fixed sliding window gap of $n + 1$ nodes between two pointers so that when the lead pointer hits the end (`null`), the trail pointer rests precisely on the predecessor of the target node.
- **Sample 1:**
  - **Input:** `head = [1, 2, 3, 4, 5]`, `n = 2`
  - **Output:** `[1, 2, 3, 5]`
- **Sample 2:**
  - **Input:** `head = [1]`, `n = 1`
  - **Output:** `[]`
- **Sample 3:**
  - **Input:** `head = [1, 2]`, `n = 1`
  - **Output:** `[1]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine holding a rigid measuring rod of length $n + 1$ nodes. You place the front of the rod at the start of the list and slide the entire rod forward until the front end drops off the edge of the list into `null`. Because the rod has a fixed length of $n + 1$, its back end must now be resting on the node immediately preceding the node that must be deleted. Splicing `slow.next = slow.next.next` unhooks the target node in a single move.

#### 3.2 The Naive Bottleneck & Redundant Computation
A two-pass approach first traverses the entire list of length $L$ to compute $L$ ($O(N)$ operations), and then performs a second traversal to step $L - n - 1$ times to reach the predecessor node. This requires two passes over memory, degrading cache efficiency and failing completely in a streaming environment where nodes cannot be rewound.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
To delete the $n$-th node from the end without a second pass, we must position a pointer at index $L - n - 1$ (0-indexed from a sentinel dummy node).
- Let a sentinel `dummy` node point to `head`. The total length from `dummy` to `null` is $L + 1$.
- Advance `fast` pointer by $n + 1$ steps ahead of `slow` (both starting at `dummy`).
- The invariant maintained during subsequent synchronized stepping is:
$$\text{index}(fast) - \text{index}(slow) = n + 1$$
- When `fast` reaches `null` (index $L + 1$):
$$\text{index}(slow) = (L + 1) - (n + 1) = L - n$$
Node $L - n$ is the exact node *before* the target node to be deleted!

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
SENTINEL GAP ARCHITECTURE:
n = 2, gap = n + 1 = 3 steps

dummy -> [ 1 ] -> [ 2 ] -> [ 3 ] -> [ 4 ] -> [ 5 ] -> null
  ▲                         ▲
slow                      fast (advanced n + 1 = 3 steps)

SYNCHRONIZED SLIDING UNTIL fast == null:
dummy -> [ 1 ] -> [ 2 ] -> [ 3 ] -> [ 4 ] -> [ 5 ] -> null
                             ▲                         ▲
                            slow                      fast

DELETION ACTION:
slow.next = slow.next.next  ===> Node 3 skips Node 4 and links to Node 5
```

- `dummy`: Sentinel prepended to `head`. Guarantees that deleting the first node (`head`) is structurally identical to deleting an internal node.
- `fast`: Scout pointer advanced $n + 1$ steps ahead.
- `slow`: Predecessor pointer that trails `fast` by exactly $n + 1$ steps.

#### 3.5 State Transition Triggers & Decision Gates
1. **Initialize Sentinel:** `ListNode dummy = new ListNode(0, head); fast = dummy; slow = dummy;`
2. **Scout Lead Creation:** Loop $n + 1$ times: `fast = fast.next`.
3. **Synchronized Traversal:** `while (fast != null) { fast = fast.next; slow = slow.next; }`
4. **Bypass Deletion:** `slow.next = slow.next.next`
5. **Return:** `dummy.next`

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `head = [1 -> 2 -> 3 -> 4 -> 5]`, `n = 2`

| Phase | `slow` Position | `fast` Position | Gap Distance | Invariant / Action |
| :--- | :--- | :--- | :--- | :--- |
| **Init** | `dummy` | `dummy` | 0 | Sentinel attached |
| **Advance Fast (1)** | `dummy` | Node `1` | 1 | Advancing scout |
| **Advance Fast (2)** | `dummy` | Node `2` | 2 | Advancing scout |
| **Advance Fast (3)** | `dummy` | Node `3` | 3 ($n + 1$) | Gap established |
| **Sync Step 1** | Node `1` | Node `4` | 3 | Both advance 1 step |
| **Sync Step 2** | Node `2` | Node `5` | 3 | Both advance 1 step |
| **Sync Step 3** | Node `3` | `null` | 3 | `fast == null` $\implies$ Loop terminates |
| **Deletion** | Node `3` | `null` | — | `3.next = 3.next.next (Node 5)` |

Result: `[1 -> 2 -> 3 -> 5 -> null]`

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (One-Pass Two-Pointer with Sentinel):** Optimal single pass, $O(1)$ auxiliary space, handles head removal seamlessly via sentinel. Preferred in all production environments and streaming pipelines.
- **Approach 2 (Two-Pass Length Counting):** Simpler to formulate conceptually, but requires two traversals and extra conditional handling for deleting the head.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Prepend sentinel `dummy` node to `head`. Set `fast = dummy`, `slow = dummy`.
- **Step 2: Establish Gap:** Advance `fast` $n + 1$ steps forward.
- **Step 3: Synchronized Advance:** Advance `fast` and `slow` together until `fast == null`.
- **Step 4: Deletion & Return:** Rewire `slow.next = slow.next.next`. Return `dummy.next`.

#### 4.3 Alternative Approaches Analysis
- **Two-Pass Traversal:**
  1. Pass 1: Walk to end to find length $L$.
  2. If $n == L$, return `head.next` (head deletion special case).
  3. Pass 2: Walk $L - n - 1$ steps to reach target's predecessor.
  4. Perform deletion.
  - *Trade-off:* Double memory reads, redundant pointer dereferences.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: One-Pass Two-Pointer (Optimal) | Approach 2: Two-Pass Length Counter |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | `O(N)` / `O(N)` / `O(N)` | `O(N)` / `O(N)` / `O(N)` (2 passes) |
| **Auxiliary Space** | `O(1)` | `O(1)` |
| **Output Space** | `O(1)` in-place | `O(1)` in-place |
| **Cache Locality** | High (single streaming read) | Moderate (re-traverses list from memory) |
| **In-Place Mutability** | In-place pointer modification | In-place pointer modification |
| **Streaming Suitability** | High (fixed lookahead buffer) | Impossible (cannot re-read stream) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #32 - Remove Nth Node From End of List
// Core Pattern: Fixed Window Gap (n + 1) with Sentinel Dummy Anchor
// Primary Invariant: distance(slow, fast) == n + 1 => slow rests on predecessor
// Edge Case Immunity: Deleting original head node requires zero special-casing
// ============================================================================
```

#### Implementation 1: One-Pass Fast/Slow Gap with Sentinel (Optimal `O(1)` Space)
```csharp
public class Solution
{
    public ListNode RemoveNthFromEnd(ListNode head, int n)
    {
        // Sentinel dummy node handles edge case where the head node itself is removed
        ListNode dummy = new ListNode(0, head);
        ListNode fast = dummy;
        ListNode slow = dummy;

        // Gate 1: Advance 'fast' pointer to establish a gap of (n + 1) nodes.
        // Because n <= list length, fast will not encounter null prematurely.
        for (int i = 0; i <= n; i++)
        {
            fast = fast.next;
        }

        // Gate 2: Advance both pointers synchronously until fast falls off the end
        // Invariant: Distance between slow and fast remains strictly (n + 1)
        while (fast != null)
        {
            fast = fast.next;
            slow = slow.next;
        }

        // Gate 3: 'slow' now rests strictly on the node PRECEDING the target node.
        // Bypass the target node to delete it from the chain.
        slow.next = slow.next.next;

        // Return new head anchored by dummy
        return dummy.next;
    }
}
```

#### Implementation 2: Two-Pass Length Calculation (`O(1)` Auxiliary Space)
```csharp
public class SolutionTwoPass
{
    public ListNode RemoveNthFromEnd(ListNode head, int n)
    {
        // Pass 1: Compute total length of the linked list
        int length = 0;
        ListNode curr = head;
        while (curr != null)
        {
            length++;
            curr = curr.next;
        }

        // Edge case: Target to delete is the head node
        if (n == length)
        {
            return head.next;
        }

        // Pass 2: Advance to node (length - n - 1), which is the predecessor
        curr = head;
        for (int i = 0; i < length - n - 1; i++)
        {
            curr = curr.next;
        }

        // Bypass target node
        curr.next = curr.next.next;

        return head;
    }
}
```

---

## 33. Reorder List (LeetCode #143)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#linked-list` `#fast-slow-split` `#reversal` `#interleave-merge` |
| **LeetCode Link** | [Reorder List](https://leetcode.com/problems/reorder-list/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given the head of a singly linked-list:
  $$L_0 \to L_1 \to \dots \to L_{n-1} \to L_n$$
  Reorder the list to be on the following form:
  $$L_0 \to L_n \to L_1 \to L_{n-1} \to L_2 \to L_{n-2} \to \dots$$
- **Strict Requirement:** You may not modify the values in the list's nodes. Only nodes themselves may be changed (pure in-place pointer rewiring).
- **Key Constraints:**
  - The number of nodes in the list is in the range $[1, 5 \times 10^4]$.
  - $1 \le \text{Node.val} \le 1000$

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Interleave the first half of a linked list with the reversed second half using a three-phase in-place pipeline.
- **Sample 1:**
  - **Input:** `head = [1, 2, 3, 4]`
  - **Output:** `[1, 4, 2, 3]`
- **Sample 2:**
  - **Input:** `head = [1, 2, 3, 4, 5]`
  - **Output:** `[1, 5, 2, 4, 3]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a long strip of paper printed with sequential numbers. You fold the paper in half, tear it at the crease, and flip the right half upside-down so the original end is now on top. Finally, you take cards alternating from the top of the left stack and the top of the right stack, weaving them together like teeth on a zipper. This composite problem is solved by chaining **three foundational linked list primitives**: (1) Halving via Tortoise & Hare, (2) In-place list reversal, and (3) Two-pointer interleaved weaving.

#### 3.2 The Naive Bottleneck & Redundant Computation
A naive approach stores all node references in a dynamic array or deque (`List<ListNode>`) requiring $O(N)$ auxiliary memory allocations. Weaving elements from both ends (`l++`, `r--`) achieves $O(N)$ time but wastes heap memory and violates strict in-place constraints. A brute-force pointer chase that searches for the tail node on every step degrades to $O(N^2)$ time:
$$T(N) = \sum_{k=1}^{N/2} (N - 2k) \implies O(N^2)$$

#### 3.3 The Breakthrough Insight & Mathematical Invariant
By partitioning the problem into 3 distinct $O(N)$ time, $O(1)$ space phases, we achieve optimal performance:
1. **Phase 1 (Split):** Fast and slow pointers find the exact midpoint. `slow.next` is decoupled (`slow.next = null`) to produce two clean disjoint sublists:
   - First half: $L_0 \to L_1 \to \dots \to L_{\lfloor(n-1)/2\rfloor}$
   - Second half: $L_{\lfloor(n-1)/2\rfloor + 1} \to \dots \to L_n$
2. **Phase 2 (Reverse Second Half):** Standard three-pointer reversal on the second half transforms it into:
   - $L_n \to L_{n-1} \to \dots$
3. **Phase 3 (Interleave Weave):** Simultaneously step through both lists, weaving one node from each:
   - Wire $L_0 \to L_n \to L_1 \to L_{n-1} \dots$

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
PHASE 1: SPLIT
Original: 1 -> 2 -> 3 -> 4 -> 5 -> null
          ▲         ▲         ▲
        head       slow      fast (fast.next.next == null)

Disconnect:
Half 1: 1 -> 2 -> 3 -> null
Half 2: 4 -> 5 -> null

PHASE 2: REVERSE SECOND HALF
Half 2 reversed: 5 -> 4 -> null (anchored by 'p2')

PHASE 3: INTERLEAVE WEAVE
p1 -> [ 1 ] -> [ 2 ] -> [ 3 ] -> null
p2 -> [ 5 ] -> [ 4 ] -> null

Weave action:
temp1 = p1.next (2)
temp2 = p2.next (4)
p1.next = p2 (1 -> 5)
p2.next = temp1 (5 -> 2)
p1 = temp1 (2), p2 = temp2 (4)
```

- `slow`, `fast`: Cursors for finding the midpoint in Phase 1.
- `prev`, `curr`: Cursors for reversing the second half in Phase 2.
- `p1`, `p2`: Weave cursors alternating nodes in Phase 3.
- `temp1`, `temp2`: Lookahead pointers caching next nodes during weaving.

#### 3.5 State Transition Triggers & Decision Gates
- **Phase 1 Guard:** `while (fast.next != null && fast.next.next != null)`
  - Guarantees `slow` lands on the end of the first half for both odd and even lengths.
- **Decouple Gate:** `ListNode secondHalf = slow.next; slow.next = null;`
- **Phase 2 Reversal:** Standard iterative reversal of `secondHalf`.
- **Phase 3 Weave Guard:** `while (p2 != null)` (since second half is always $\le$ first half in length).
  - `temp1 = p1.next; temp2 = p2.next;`
  - `p1.next = p2; p2.next = temp1;`
  - `p1 = temp1; p2 = temp2;`

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `head = [1 -> 2 -> 3 -> 4 -> 5]`

| Phase | Action / Pointers | Resulting State |
| :--- | :--- | :--- |
| **Phase 1: Split** | `slow` stops at Node `3`, `fast` at Node `5` | Half 1: `[1 -> 2 -> 3 -> null]`, Half 2: `[4 -> 5 -> null]` |
| **Phase 2: Reverse** | Reverse Half 2 starting at `4` | `p1 = 1 -> 2 -> 3 -> null`, `p2 = 5 -> 4 -> null` |
| **Phase 3: Weave 1** | Wire `1.next = 5`, `5.next = 2` | `1 -> 5 -> 2`, `p1 = 2`, `p2 = 4` |
| **Phase 3: Weave 2** | Wire `2.next = 4`, `4.next = 3` | `1 -> 5 -> 2 -> 4 -> 3`, `p1 = 3`, `p2 = null` |
| **Termination** | `p2 == null` $\implies$ Loop terminates | Fully reordered: `[1 -> 5 -> 2 -> 4 -> 3]` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Three-Phase In-Place Pipeline):** Strictly $O(1)$ auxiliary space and $O(N)$ linear time. The gold standard in technical interviews demonstrating mastery of linked list composability.
- **Approach 2 (Array / Deque Index Buffering):** Reads all nodes into an array and rewires pointers from boundaries inward. Incurs $O(N)$ heap memory; easy to write quickly under time pressure, but rejected if interviewer mandates $O(1)$ space.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** If `head == null || head.next == null || head.next.next == null`, return immediately (0, 1, or 2 nodes are already reordered).
- **Step 2: Find Midpoint & Split:** Run fast/slow pointers. Decouple `slow.next = null`.
- **Step 3: Invert Second Half:** Reverse second half sublist.
- **Step 4: Interleaved Splicing:** Weave `p1` and `p2` alternately until `p2 == null`.

#### 4.3 Alternative Approaches Analysis
- **Deque / Vector Buffer Approach:**
  - Copy all $N$ pointers into `ListNode[] nodes`.
  - Use opposing pointers `i = 0, j = N - 1`.
  - While `i < j`: `nodes[i].next = nodes[j]; i++; nodes[j].next = nodes[i]; j--;`
  - Finally, set `nodes[i].next = null`.
  - *Trade-off:* $O(N)$ memory allocations vs $O(1)$ in-place pipeline.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: In-Place Three-Phase (Optimal) | Approach 2: Array / Deque Buffer |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | `O(N)` / `O(N)` / `O(N)` | `O(N)` / `O(N)` / `O(N)` |
| **Auxiliary Space** | `O(1)` strictly | `O(N)` heap array |
| **Output Space** | `O(1)` in-place | `O(1)` in-place |
| **Cache Locality** | High (sequential passes) | High for array, poor for node pointer hops |
| **In-Place Mutability** | Pure in-place pointer mutator | Mutates pointers via buffer |
| **Streaming Suitability** | Low (must reach tail to reverse) | Low (requires full list buffer) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #33 - Reorder List
// Core Pattern: Tri-Primitive Pipeline (Find Mid -> Reverse Half -> Weave)
// Space Guarantee: O(1) auxiliary space strictly; zero array allocations
// Midpoint Invariant: fast.next != null && fast.next.next != null lands on exact cut
// ============================================================================
```

#### Implementation 1: In-Place Three-Phase Pipeline (Optimal `O(1)` Space)
```csharp
public class Solution
{
    public void ReorderList(ListNode head)
    {
        // Boundary Defense: Lists of length 0, 1, or 2 are already in reordered state
        if (head == null || head.next == null || head.next.next == null)
        {
            return;
        }

        // ====================================================================
        // PHASE 1: Locate Midpoint via Fast & Slow Pointers and Split
        // ====================================================================
        ListNode slow = head;
        ListNode fast = head;

        // Invariant: Fast moves 2 steps; slow moves 1 step.
        // Stopping when fast.next or fast.next.next is null ensures slow lands on the split tail.
        while (fast.next != null && fast.next.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
        }

        // Sever the list into two independent sublists
        ListNode secondHalf = slow.next;
        slow.next = null; // Terminate first half cleanly

        // ====================================================================
        // PHASE 2: In-Place Reversal of the Second Half Sublist
        // ====================================================================
        ListNode prev = null;
        ListNode curr = secondHalf;

        while (curr != null)
        {
            ListNode nextTemp = curr.next; // Cache forward link
            curr.next = prev;              // Invert pointer
            prev = curr;                   // Advance settled head
            curr = nextTemp;               // Advance unreversed cursor
        }

        // 'prev' is now the head of the reversed second half
        ListNode p1 = head; // First half cursor
        ListNode p2 = prev; // Second half cursor

        // ====================================================================
        // PHASE 3: Interleaved Weaving of Both Halves
        // ====================================================================
        // Invariant: Second half length is always <= first half length.
        while (p2 != null)
        {
            // Cache next forward pointers for both streams
            ListNode temp1 = p1.next;
            ListNode temp2 = p2.next;

            // Rewire: p1 -> p2 -> temp1
            p1.next = p2;
            p2.next = temp1;

            // Advance cursors to their cached forward positions
            p1 = temp1;
            p2 = temp2;
        }
    }
}
```

#### Implementation 2: Array Index Buffer (`O(N)` Memory)
```csharp
public class SolutionArrayBuffer
{
    public void ReorderList(ListNode head)
    {
        if (head == null || head.next == null || head.next.next == null) return;

        // Step 1: Collect node pointers into an indexed list
        var nodes = new List<ListNode>();
        ListNode curr = head;
        while (curr != null)
        {
            nodes.Add(curr);
            curr = curr.next;
        }

        // Step 2: Opposing two-pointer interleaved rewiring
        int left = 0;
        int right = nodes.Count - 1;

        while (left < right)
        {
            nodes[left].next = nodes[right];
            left++;

            if (left == right) break;

            nodes[right].next = nodes[left];
            right--;
        }

        // Nullify terminal tail node to prevent cycle
        nodes[left].next = null;
    }
}
```

---

## 34. Merge K Sorted Lists (LeetCode #23)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#min-heap` `#divide-and-conquer` `#k-way-merge` `#linked-list` |
| **LeetCode Link** | [Merge K Sorted Lists](https://leetcode.com/problems/merge-k-sorted-lists/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given an array of `k` linked-lists `lists`, each linked-list is sorted in ascending order. Merge all the linked-lists into one sorted linked-list and return it.
- **Key Constraints:**
  - $k == \text{lists.length}$
  - $0 \le k \le 10^4$
  - $0 \le \text{lists[i].length} \le 500$
  - $-10^4 \le \text{lists[i][j]} \le 10^4$
  - Each `lists[i]` is sorted in ascending order.
  - The sum of `lists[i].length` will not exceed $10^4$.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Perform a multi-way merge across $k$ sorted streams into a single sorted list in $O(N \log k)$ time, where $N$ is total nodes across all lists.
- **Sample 1:**
  - **Input:** `lists = [[1,4,5],[1,3,4],[2,6]]`
  - **Output:** `[1,1,2,3,4,4,5,6]`
- **Sample 2:**
  - **Input:** `lists = []`
  - **Output:** `[]`
- **Sample 3:**
  - **Input:** `lists = [[]]`
  - **Output:** `[]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine $k$ airport check-in lines, all feeding into a single boarding gate. At any given moment, the gate agent only needs to compare the passengers standing at the very front of each of the $k$ lines. To do this efficiently, the agent maintains a small scoreboard of size $k$ (a Min-Heap). The agent calls the passenger with the lowest ticket number, and immediately invites the next passenger from that exact same line to join the scoreboard. Alternatively, the lines can be paired up and merged two by two in rounds, like a tennis tournament bracket.

#### 3.2 The Naive Bottleneck & Redundant Computation
1. **Iterative Linear Merge (One-by-One):** Merging list 1 with list 2, then merging the result with list 3, and so on. If each list has $L$ nodes, list 1 is traversed $k-1$ times. Total comparisons:
   $$\sum_{i=1}^k i \cdot L = L \cdot \frac{k(k+1)}{2} \implies O(N \cdot k)$$
   When $k = 10^4$, $O(N \cdot k) \approx 10^8$ operations, causing Time Limit Exceeded (TLE).
2. **Collect and Sort:** Dumping all $N$ node values into an array and running `Array.Sort` ($O(N \log N)$ time and $O(N)$ space) completely ignores that all $k$ sublists are already sorted.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
The search for the global minimum among $k$ active candidates can be optimized to $O(\log k)$ using two distinct paradigms:
- **Paradigm A (Min-Heap / PriorityQueue):** Maintain a heap of size at most $k$. The heap top is always the absolute minimum across all active stream heads. Extracting the minimum and inserting its successor takes $O(\log k)$. Repeating for all $N$ nodes yields:
  $$T(N) = N \cdot O(\log k) \implies O(N \log k)$$
  *Crucial for streaming:* Only requires $O(k)$ memory; nodes can arrive on-the-fly over a network.
- **Paradigm B (Divide & Conquer Tournament Merge):** Merge pairs of lists iteratively. In round 1, merge $k$ lists into $k/2$ lists. In round 2, merge into $k/4$ lists. Number of rounds is $\lceil \log_2 k \rceil$. In each round, every node is touched exactly once $\implies O(N \log k)$ time and strictly $O(1)$ auxiliary space.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
MIN-HEAP PARADIGM:
PriorityQueue of capacity k:
Heap State: [ (1 from L1), (1 from L2), (2 from L3) ]
Dequeue min -> Node 1(L1). tail.next = Node 1(L1).
Enqueue successor -> Node 4(L1).
Heap State: [ (1 from L2), (2 from L3), (4 from L1) ]

DIVIDE & CONQUER TOURNAMENT BRACKET:
Round 0:  [L0]   [L1]   [L2]   [L3]   [L4]   [L5]
           └──────┬─┘    └──────┬─┘    └──────┬─┘
Round 1:      [M01]          [M23]          [M45]
                └──────────────┬─┘            │
Round 2:                     [M0123]        [M45]
                               └──────────────┬─┘
Round 3:                                  [M_ALL]  (Total rounds: ceil(log2 k))
```

- `tail`: Scribe cursor tracking the merged list.
- `minHeap`: Stores `(ListNode, int val)` tuple of size at most $k$.
- `interval`: Step interval for Divide & Conquer pairing ($1, 2, 4, 8, \dots$).

#### 3.5 State Transition Triggers & Decision Gates
- **Heap Approach:**
  - Enqueue all non-null `lists[i]`.
  - While `minHeap.Count > 0`:
    - `smallest = minHeap.Dequeue()`
    - `tail.next = smallest; tail = tail.next;`
    - If `smallest.next != null` $\implies$ `minHeap.Enqueue(smallest.next, smallest.next.val)`
- **Divide & Conquer Approach:**
  - `interval = 1`
  - While `interval < k`:
    - For `i = 0; i + interval < k; i += interval * 2`:
      - `lists[i] = MergeTwoLists(lists[i], lists[i + interval])`
    - `interval *= 2`

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `lists = [[1, 4, 5], [1, 3, 4], [2, 6]]` ($k=3$)

| Round / Step | Action | Active Heap / Merged Pairs | Merged Result So Far |
| :--- | :--- | :--- | :--- |
| **Heap Init** | Enqueue heads: `1(L1), 1(L2), 2(L3)` | Heap size: 3 | `dummy` |
| **Step 1** | Dequeue `1(L1)`, enqueue `4(L1)` | Heap: `[1(L2), 2(L3), 4(L1)]` | `dummy -> 1` |
| **Step 2** | Dequeue `1(L2)`, enqueue `3(L2)` | Heap: `[2(L3), 3(L2), 4(L1)]` | `... -> 1 -> 1` |
| **Step 3** | Dequeue `2(L3)`, enqueue `6(L3)` | Heap: `[3(L2), 4(L1), 6(L3)]` | `... -> 1 -> 1 -> 2` |
| **Step 4** | Dequeue `3(L2)`, enqueue `4(L2)` | Heap: `[4(L1), 4(L2), 6(L3)]` | `... -> 2 -> 3` |
| **Step 5** | Dequeue `4(L1)`, enqueue `5(L1)` | Heap: `[4(L2), 5(L1), 6(L3)]` | `... -> 3 -> 4` |
| **Step 6** | Dequeue `4(L2)`, no successor | Heap: `[5(L1), 6(L3)]` | `... -> 4 -> 4` |
| **Step 7** | Dequeue `5(L1)`, no successor | Heap: `[6(L3)]` | `... -> 4 -> 5` |
| **Step 8** | Dequeue `6(L3)`, no successor | Heap: `[]` (empty) | `... -> 5 -> 6` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Min-Heap / PriorityQueue):** The definitive choice for streaming, asynchronous, or distributed systems. Can process infinite streams online because memory consumption is bounded strictly by $O(k)$.
- **Approach 2 (Divide & Conquer Pairwise Merging):** The optimal batch processing algorithm when all $k$ list heads are pre-loaded in memory. Operates in $O(1)$ auxiliary space by reusing node pointers and the input array.

#### 4.2 Step-by-Step Natural Progression Flow
- **Heap Progression:**
  - Step 1: Validate input. Instantiate `PriorityQueue<ListNode, int>(capacity: k)`.
  - Step 2: Enqueue head of each non-empty list.
  - Step 3: Dequeue minimum node, append to merged `tail`, and enqueue successor if non-null.
  - Step 4: Return `dummy.next`.
- **Divide & Conquer Progression:**
  - Step 1: Validate input. Initialize `interval = 1`.
  - Step 2: Iteratively pair lists `i` and `i + interval`, merging via standard two-list merge.
  - Step 3: Double `interval` after each pass until `interval >= k`.
  - Step 4: Return `lists[0]`.

#### 4.3 Alternative Approaches Analysis
- **Sequential Pairwise Merging:** Merge $L_0$ with $L_1$, then with $L_2 \dots$
  - Time: $O(N \cdot k)$ — unacceptably slow.
- **Array Flattening & QuickSort:**
  - Time: $O(N \log N)$
  - Space: $O(N)$ allocations. Disregards pre-sorted properties of lists.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Min-Heap PriorityQueue | Approach 2: Divide & Conquer (Optimal Space) |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | `O(N log k)` | `O(N log k)` |
| **Auxiliary Space** | `O(k)` (heap capacity) | `O(1)` strictly (iterative) |
| **Output Space** | `O(1)` (in-place rewiring) | `O(1)` (in-place rewiring) |
| **Cache Locality** | Moderate (heap array dereferencing) | High (sequential paired scans) |
| **In-Place Mutability** | In-place node splicing | In-place node splicing |
| **Streaming Suitability** | High (ideal for online streaming) | Low (requires all lists upfront) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #34 - Merge K Sorted Lists
// Core Pattern: K-Way Min-Heap vs Iterative Divide-and-Conquer Tournament
// Complexity Anchor: O(N log k) Time across both implementations
// Space Trade-off: Heap requires O(k) memory; Divide & Conquer requires O(1)
// ============================================================================
```

#### Implementation 1: Min-Heap PriorityQueue (Optimal for Streaming)
```csharp
public class Solution
{
    public ListNode MergeKLists(ListNode[] lists)
    {
        // Boundary Defense: Null or empty input array requires no merge
        if (lists == null || lists.Length == 0)
        {
            return null;
        }

        // Min-Heap bounded to size k storing (Node, Priority: val)
        var minHeap = new PriorityQueue<ListNode, int>(capacity: lists.Length);

        // Gate 1: Seed the heap with the head of each non-empty linked list
        foreach (ListNode headNode in lists)
        {
            if (headNode != null)
            {
                minHeap.Enqueue(headNode, headNode.val);
            }
        }

        ListNode dummy = new ListNode(0); // Sentinel anchor for result
        ListNode tail = dummy;            // Scribe cursor

        // Invariant: At each tick, minHeap top is the global minimum across all active stream heads
        while (minHeap.Count > 0)
        {
            ListNode smallest = minHeap.Dequeue();
            tail.next = smallest; // Splice smallest node
            tail = tail.next;     // Advance scribe cursor

            // If the extracted node has a successor, feed it into the heap competition
            if (smallest.next != null)
            {
                minHeap.Enqueue(smallest.next, smallest.next.val);
            }
        }

        return dummy.next;
    }
}
```

#### Implementation 2: Divide and Conquer Pairwise Merging (`O(1)` Auxiliary Space)
```csharp
public class SolutionDivideAndConquer
{
    public ListNode MergeKLists(ListNode[] lists)
    {
        if (lists == null || lists.Length == 0) return null;

        int interval = 1;
        int k = lists.Length;

        // Invariant: In each tournament round, pair lists at (i, i + interval)
        // Halves the number of active lists in ceil(log2 k) rounds.
        while (interval < k)
        {
            for (int i = 0; i + interval < k; i += interval * 2)
            {
                lists[i] = MergeTwoLists(lists[i], lists[i + interval]);
            }
            interval *= 2; // Double stride for next tournament round
        }

        return lists[0];
    }

    private static ListNode MergeTwoLists(ListNode l1, ListNode l2)
    {
        ListNode dummy = new ListNode(0);
        ListNode tail = dummy;

        while (l1 != null && l2 != null)
        {
            if (l1.val <= l2.val)
            {
                tail.next = l1;
                l1 = l1.next;
            }
            else
            {
                tail.next = l2;
                l2 = l2.next;
            }
            tail = tail.next;
        }

        tail.next = l1 ?? l2; // O(1) residual splice
        return dummy.next;
    }
}
```

---

## 35. Reverse Nodes in K-Group (LeetCode #25)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#linked-list` `#k-group-reversal` `#subsegment-reversal` |
| **LeetCode Link** | [Reverse Nodes in K-Group](https://leetcode.com/problems/reverse-nodes-in-k-group/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given the `head` of a linked list, reverse the nodes of the list `k` at a time, and return the modified list. `k` is a positive integer and is less than or equal to the length of the linked list. If the number of nodes is not a multiple of `k` then left-out nodes, in the end, should remain as it is.
- **Strict Constraints:**
  - You may not alter the values in the list's nodes, only nodes themselves may be changed.
  - Solve it in $O(1)$ extra memory space.
- **Key Constraints:**
  - The number of nodes in the list is $n$.
  - $1 \le k \le n \le 5000$
  - $0 \le \text{Node.val} \le 1000$

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Divide the linked list into consecutive blocks of size $k$. Reverse each full block in-place while keeping terminal sub-blocks of length $< k$ completely untouched.
- **Sample 1:**
  - **Input:** `head = [1, 2, 3, 4, 5]`, `k = 2`
  - **Output:** `[2, 1, 4, 3, 5]`
- **Sample 2:**
  - **Input:** `head = [1, 2, 3, 4, 5]`, `k = 3`
  - **Output:** `[3, 2, 1, 4, 5]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Picture a train made of cargo containers. An inspection crane is programmed to reverse containers in batches of $k$. Before touching a single coupling, the crane sends a scout forward to count $k$ containers. If fewer than $k$ containers remain before the end of the track, the crane halts and leaves them untouched. If a full batch of $k$ exists, the crane uncouples the batch, flips its internal order, connects the previous train segment to the batch's new head, and wires the batch's new tail to the unreversed remainder.

#### 3.2 The Naive Bottleneck & Redundant Computation
A naive algorithm copies node values into an array, reverses subarrays of length $k$, and writes values back. This violates the strict problem requirement prohibiting value mutation and wastes $O(N)$ auxiliary space. An improper pointer reversal attempt that starts inverting links before verifying that $k$ nodes exist will find itself midway through an incomplete tail block, requiring an expensive, bug-prone second reversal to restore original order.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
The **Lookahead Probe Invariant**: Before initiating any pointer inversion on a candidate group, advance a scout pointer `k` steps forward:
- If `probe == null` before reaching $k$ steps, the remaining group has length $< k$. Immediately terminate and leave the sublist untouched.
- If $k$ valid nodes are verified, decouple the subsegment and apply the standard 3-pointer reversal algorithm for exactly $k$ iterations.

Crucial structural invariant:
- The node that was the **head** of the $k$-group before reversal becomes the **tail** of the $k$-group after reversal.
- Therefore, `head.next` must be connected to the result of reversing the subsequent $k$-groups.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
LOOKAHEAD & REVERSAL ARCHITECTURE (k = 3):

[ dummy ] -> [ 1 ] -> [ 2 ] -> [ 3 ] -> [ 4 ] -> [ 5 ] -> null
  ▲                               ▲
groupPrev                       probe (stepped k = 3 times; valid group confirmed)

REVERSE 3 NODES:
Group [1 -> 2 -> 3] becomes [3 -> 2 -> 1]

STITCHING:
groupPrev.next = 3 (new group head)
1.next = 4 (former head '1' is now tail, wired to next unexplored node '4')
groupPrev = 1 (advance anchor for next k-group)
```

- `dummy`: Sentinel anchor guarding the overall head.
- `groupPrev`: Pointer to the node immediately preceding the current $k$-group.
- `probe`: Scout pointer checking for $k$ available nodes.
- `curr`, `prev`: Pointers executing the internal $k$-node reversal.

#### 3.5 State Transition Triggers & Decision Gates
1. **Lookahead Gate:** Run `probe` from `curr` for $k$ steps. If `probe == null` at any step $i < k$, return `head` (or break loop).
2. **Reverse $k$ Nodes:** Execute $k$ iterations of:
   - `nextTemp = curr.next; curr.next = prev; prev = curr; curr = nextTemp;`
3. **Stitch Gate:**
   - In recursive form: `head.next = ReverseKGroup(curr, k); return prev;`
   - In iterative form: Wire `groupPrev.next = prev`, former head's `next = curr`, and shift `groupPrev = formerHead`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `head = [1 -> 2 -> 3 -> 4 -> 5]`, `k = 2`

| Iteration | Group Candidate | Lookahead Probe ($k=2$) | Reversal Result | Stitched Chain |
| :--- | :--- | :--- | :--- | :--- |
| **Group 1** | Nodes `1, 2` | Probe finds `1, 2` $\implies$ valid | `2 -> 1` | `dummy -> 2 -> 1` |
| **Group 2** | Nodes `3, 4` | Probe finds `3, 4` $\implies$ valid | `4 -> 3` | `... -> 1 -> 4 -> 3` |
| **Group 3** | Node `5` | Probe hits `null` at step 1 ($< 2$) | Untouched (`5`) | `... -> 3 -> 5 -> null` |
| **Return** | Complete | — | — | `[2 -> 1 -> 4 -> 3 -> 5]` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Iterative with Sentinel Dummy):** Strictly $O(1)$ auxiliary space. Fulfills the strict follow-up constraint of the problem with zero risk of stack overflow on large inputs.
- **Approach 2 (Recursive Subsegment Reversal):** Highly readable and elegant. Incurs $O(N / k)$ call stack memory. Acceptable in interviews when recursion is permitted, but must be paired with knowledge of the $O(1)$ iterative variant.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Sentinel:** Create `dummy` node pointing to `head`. Initialize `groupPrev = dummy`.
- **Step 2: Lookahead Probe:** Count $k$ nodes from `groupPrev.next`. If fewer than $k$ nodes exist, break.
- **Step 3: In-Place Reverse:** Reverse the $k$ nodes between `groupPrev.next` and `probe.next`.
- **Step 4: Subsegment Stitch:** Connect `groupPrev` to new group head, and new group tail to next group head.
- **Step 5: Advance Anchor:** Set `groupPrev` to the group's new tail. Repeat.

#### 4.3 Alternative Approaches Analysis
- **Recursive Formulation:**
  - Probe $k$ nodes. If $< k$, return `head`.
  - Reverse $k$ nodes.
  - `head.next = ReverseKGroup(curr, k)`
  - Return `prev`.
  - Space: $O(N / k)$ stack frames.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Iterative Sentinel (Optimal `O(1)`) | Approach 2: Recursive Formulation |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | `O(N)` / `O(N)` / `O(N)` | `O(N)` / `O(N)` / `O(N)` |
| **Auxiliary Space** | `O(1)` strictly | `O(N / k)` call stack |
| **Output Space** | `O(1)` in-place | `O(1)` in-place |
| **Cache Locality** | High (localized block traversals) | Moderate (stack frames) |
| **In-Place Mutability** | In-place pointer mutator | In-place pointer mutator |
| **Streaming Suitability** | High (buffer size $k$) | Moderate (buffers call stack) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #35 - Reverse Nodes in K-Group
// Core Pattern: Lookahead Scout Verification + In-Place Subsegment Inversion
// Primary Invariant: Only full groups of size k undergo pointer inversion
// Space Guarantee: Approach 1 operates in strictly O(1) auxiliary heap/stack memory
// ============================================================================
```

#### Implementation 1: Iterative Lookahead with Sentinel (Optimal `O(1)` Space)
```csharp
public class Solution
{
    public ListNode ReverseKGroup(ListNode head, int k)
    {
        // Boundary Defense: If list is empty or k is 1, no reordering is needed
        if (head == null || k <= 1)
        {
            return head;
        }

        ListNode dummy = new ListNode(0, head);
        ListNode groupPrev = dummy; // Anchor immediately preceding the active k-group

        while (true)
        {
            // Gate 1: Lookahead Probe - Verify that at least k nodes remain
            ListNode kth = groupPrev;
            for (int i = 0; i < k; i++)
            {
                kth = kth.next;
                if (kth == null)
                {
                    // Fewer than k nodes remain; leave trailing sublist intact
                    return dummy.next;
                }
            }

            ListNode groupNext = kth.next;      // Node immediately after the k-group
            ListNode prev = groupNext;          // Seed prev with groupNext to link tail automatically
            ListNode curr = groupPrev.next;     // Head of the unreversed k-group
            ListNode groupHead = groupPrev.next;// Will become the new tail after reversal

            // Gate 2: In-place reversal of exactly k nodes
            for (int i = 0; i < k; i++)
            {
                ListNode nextTemp = curr.next;
                curr.next = prev;
                prev = curr;
                curr = nextTemp;
            }

            // Gate 3: Stitching - wire preceding group anchor to the new group head
            groupPrev.next = prev;

            // Gate 4: Advance groupPrev to the tail of the newly reversed group
            groupPrev = groupHead;
        }
    }
}
```

#### Implementation 2: Recursive Lookahead Formulation (`O(N/k)` Stack Space)
```csharp
public class SolutionRecursive
{
    public ListNode ReverseKGroup(ListNode head, int k)
    {
        if (head == null || k <= 1) return head;

        // Step 1: Lookahead probe to ensure at least k nodes exist
        ListNode probe = head;
        for (int i = 0; i < k; i++)
        {
            if (probe == null)
            {
                return head; // Fewer than k nodes; return sublist unchanged
            }
            probe = probe.next;
        }

        // Step 2: Reverse exactly k nodes
        ListNode prev = null;
        ListNode curr = head;
        for (int i = 0; i < k; i++)
        {
            ListNode nextTemp = curr.next;
            curr.next = prev;
            prev = curr;
            curr = nextTemp;
        }

        // Invariant: 'head' is now the tail of this segment.
        // Wire its next pointer to the result of recursively reversing subsequent groups.
        head.next = ReverseKGroup(curr, k);

        // 'prev' is the new head of this reversed k-group
        return prev;
    }
}
```

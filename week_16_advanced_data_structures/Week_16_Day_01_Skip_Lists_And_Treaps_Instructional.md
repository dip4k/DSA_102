# 📘 Week 16, Day 1: Skip Lists & Treaps

> 🧭 **Navigation:** [← Week Overview](README.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_16_Day_02_Link_Cut_Trees_Instructional.md)
> 
> 💡 **Instructor Note:** *In senior technical interviews, you will almost never be asked to implement an AVL or Red-Black tree from scratch because the rebalancing rotations contain dozens of edge cases. However, Skip Lists and Treaps are high-yield interview targets: Skip Lists test your concurrent systems architecture knowledge (e.g., Redis Sorted Sets, LevelDB MemTables), while Treaps provide the cleanest 30-line balanced tree implementation via Split and Merge.*

---

## 🎯 Learning Objectives

*   **Probabilistic Balance Model:** Master how randomized priorities convert arbitrary insertion sequences into average-case balanced trees with expected `O(log N)` height.
*   **Skip List Express Lanes:** Internalize multi-level linked lists with geometric promotion (`p = 0.5` or `p = 0.25`), reducing pointer traversal from `O(N)` to `O(log N)`.
*   **Split & Merge Tree Algebra:** Replace fragile AVL/Red-Black rotation logic with the two fundamental Treap primitives: `Split` and `Merge`.
*   **Production Calibration (Redis & Storage Engines):** Clearly articulate why production systems like Redis `ZSET` and RocksDB choose Skip Lists over Red-Black trees.
*   **Dual-Language Mastery:** Implement memory-conscious, zero-leak Treap operations in modern C# (.NET 8/9) and idiomatic Python (3.11+).

---

## ⚖️ FAANG Senior / Lead Interview Calibration

In a 45-minute FAANG or Tier-1 quantitative interview (Google, Meta, Citadel, Stripe), interviewers calibrate expectations based on seniority:

| Dimension | Mid-Level (L4 / SDE II) | Senior / Lead (L5 / L6 / Staff) |
| :--- | :--- | :--- |
| **Skip Lists** | Explain `O(log N)` search; describe multi-level pointer navigation. | Contrast lock-free concurrent Skip Lists against mutex-heavy Red-Black trees; analyze Redis `ZSET` cache locality and memory overhead per node (`1 / (1 - p)` pointers). |
| **Treaps** | Implement basic BST search and discuss why priorities prevent degeneracy. | Code functional `Split` and `Merge` in under 20 minutes; explain Cartesian tree equivalence; extend to Implicit Treaps for range reversals (Rope data structures). |
| **When Code is Required** | Code Treap `Search` and `Insert`. | Code complete `Split` and `Merge` with zero pointer leaks, handle duplicates, and derive the expected tree height `E[height] = 2 * ln(N) + O(1)`. |
| **Conceptual Only** | Recognize that coin flips yield `O(log N)` average runtime. | Discuss failure probability bounds (Chernoff bound: probability that depth exceeds `c * log N` drops as `N^(-alpha)`). |

### The Production Trade-off: Why Redis Uses Skip Lists Instead of Red-Black Trees
1. **Range Queries are Contiguous:** In a Skip List, Level 0 is a standard doubly-linked list. Executing `ZRANGEBYSCORE min max` requires finding the lower bound in `O(log N)` and then walking forward along Level 0. In an RB-Tree, range traversals require repeated tree successor lookups (`O(log N)` each or stack-based in-order traversal).
2. **Lock-Free Concurrency:** Mutex contention in balanced trees is severe because root rebalancing locks the entire tree. Skip lists only lock local forward pointers during insertion, making concurrent lock-free skip lists (like Java's `ConcurrentSkipListMap`) vastly more scalable.
3. **Simplicity and Memory Tuning:** With promotion probability `p = 0.25`, the expected number of pointers per node is `1 / (1 - 0.25) = 1.33`. An RB-Tree node requires 3 pointers (left, right, parent) plus 1 color bit per node. The memory difference is negligible, but Skip List implementation is a fraction of the complexity.

---

## 📖 Chapter 1: Context & Physical Mental Models

### 1. The Skip List: Express Subway Analogy

A standard sorted linked list has `O(N)` lookup because you must inspect every station. A Skip List adds express tracks above the local track:

```text
[Level 2 Express]  (Head) -----------------------------> [25] -------------------------> [80] -> NIL
                     |                                     |                                |
[Level 1 Semi-Exp] (Head) ------------> [10] ----------> [25] ------------> [50] -------> [80] -> NIL
                     |                    |                |                 |              |
[Level 0 Local]    (Head) -> [3] ------> [10] -> [17] -> [25] -> [31] ----> [50] -> [62] > [80] -> NIL
```

*   **Search Path for Key 31:**
    1. Start at `Level 2`: Compare `Head` to `25`. `31 > 25`, jump to `25`. Compare to `80`. `31 < 80`, drop down to `Level 1` at node `25`.
    2. At `Level 1`: Next node is `50`. `31 < 50`, drop down to `Level 0` at node `25`.
    3. At `Level 0`: Next node is `31`. Target found! Total comparisons: 5 instead of 6 sequential scans.

### 2. The Treap: Tree + Heap Hybrid

A Treap (Tree + Heap) assigns every element two attributes: `(Key, Priority)`.
*   **Key:** Satisfies the **Binary Search Tree** invariant (left < root < right).
*   **Priority:** A uniformly distributed random number chosen independently upon insertion. Satisfies the **Max-Heap** invariant (`priority(node) >= max(priority(left), priority(right))`).

```text
               (Key: 50, Prio: 95)
                  /             \
      (Key: 25, Prio: 82)     (Key: 80, Prio: 71)
         /           \                    \
(Key: 10, Prio: 40) (Key: 31, Prio: 55) (Key: 90, Prio: 33)
```

**Why is it balanced?** If priorities are chosen uniformly at random, a Treap is mathematically identical to a randomized Binary Search Tree where keys are inserted in decreasing order of their priorities. Because a random permutation of `N` keys produces a BST with expected height `2 * ln(N) approx 1.386 * log2(N)`, the Treap is guaranteed to be balanced in expectation without complex tree balance factors.

---

## 🏛️ Chapter 2: The Governing Invariants & Algorithmic Mechanics

### 1. Mathematical Invariants

1. **BST Invariant:** For every node `u`:
   - For all `v` in `u.left`: `v.key < u.key`
   - For all `w` in `u.right`: `w.key > u.key`
2. **Heap Invariant:** For every node `u`:
   - If `u.left != null`: `u.priority >= u.left.priority`
   - If `u.right != null`: `u.priority >= u.right.priority`
3. **Cartesian Uniqueness Theorem:** If all keys are distinct and all priorities are distinct, there exists exactly **one unique Treap** topology representing that set of elements.

### 2. Treap Algebra: Split and Merge

Instead of writing rotations (`rotateLeft`, `rotateRight`), modern production Treaps are governed by two algebraic primitives:

#### Split Operation
Given a Treap `T` and a boundary key `K`, split `T` into two Treaps `L` and `R` such that:
*   All keys in `L` are `<= K`
*   All keys in `R` are `> K`

```text
Split(T, K):
  If T is null -> return (null, null)
  If T.key <= K:
      (L_sub, R_sub) = Split(T.right, K)
      T.right = L_sub
      return (T, R_sub)
  Else:
      (L_sub, R_sub) = Split(T.left, K)
      T.left = R_sub
      return (L_sub, T)
```

#### Merge Operation
Given two Treaps `L` and `R` where **every key in `L` is strictly less than every key in `R`**, merge them into a single valid Treap `T`.

```text
Merge(L, R):
  If L is null -> return R
  If R is null -> return L
  If L.priority > R.priority:
      L.right = Merge(L.right, R)
      return L
  Else:
      R.left = Merge(L, R.left)
      return R
```

#### Mutation via Split & Merge
*   **Insert(key):** Split root into `(L, R)` at `key`. Create `node = TreapNode(key, random())`. Root becomes `Merge(Merge(L, node), R)`.
*   **Delete(key):** Split root into `(L, Rest)` at `key - 1`. Split `Rest` into `(Mid, R)` at `key`. Root becomes `Merge(L, R)` (discarding `Mid`).

---

## 💻 Chapter 3: Idiomatic Dual-Language Code

### C# Primary Implementation (.NET 8/9)

```csharp
using System;
using System.Collections.Generic;

namespace AdvancedDataStructures;

/// <summary>
/// Represents a node in a randomized Treap (Cartesian Tree).
/// </summary>
public sealed class TreapNode
{
    public int Key { get; }
    public int Priority { get; }
    public TreapNode? Left { get; set; }
    public TreapNode? Right { get; set; }

    public TreapNode(int key, int priority)
    {
        Key = key;
        Priority = priority;
    }
}

/// <summary>
/// High-performance Treap supporting expected O(log N) operations via Split and Merge.
/// </summary>
public sealed class Treap
{
    private readonly Random _rand = new();
    public TreapNode? Root { get; private set; }

    /// <summary>
    /// Splits Treap rooted at t into:
    /// - left: all keys <= key
    /// - right: all keys > key
    /// </summary>
    public static (TreapNode? Left, TreapNode? Right) Split(TreapNode? t, int key)
    {
        if (t is null)
        {
            return (null, null);
        }

        if (t.Key <= key)
        {
            var (lSub, rSub) = Split(t.Right, key);
            t.Right = lSub;
            return (t, rSub);
        }
        else
        {
            var (lSub, rSub) = Split(t.Left, key);
            t.Left = rSub;
            return (lSub, t);
        }
    }

    /// <summary>
    /// Merges two Treaps l and r under the precondition that max_key(l) < min_key(r).
    /// Preserves Max-Heap priority invariant.
    /// </summary>
    public static TreapNode? Merge(TreapNode? l, TreapNode? r)
    {
        if (l is null) return r;
        if (r is null) return l;

        if (l.Priority >= r.Priority)
        {
            l.Right = Merge(l.Right, r);
            return l;
        }
        else
        {
            r.Left = Merge(l, r.Left);
            return r;
        }
    }

    /// <summary>
    /// Inserts a key with a cryptographically uniform random priority.
    /// Does not insert duplicate keys.
    /// </summary>
    public void Insert(int key)
    {
        if (Contains(key)) return;

        var (left, right) = Split(Root, key);
        var newNode = new TreapNode(key, _rand.Next());
        Root = Merge(Merge(left, newNode), right);
    }

    /// <summary>
    /// Deletes a key from the Treap if present.
    /// </summary>
    public bool Delete(int key)
    {
        if (!Contains(key)) return false;

        var (left, rest) = Split(Root, key - 1);
        var (target, right) = Split(rest, key);

        // Discard target node and merge left with right
        Root = Merge(left, right);
        return true;
    }

    /// <summary>
    /// Standard BST search in O(depth).
    /// </summary>
    public bool Contains(int key)
    {
        TreapNode? curr = Root;
        while (curr is not null)
        {
            if (curr.Key == key) return true;
            curr = key < curr.Key ? curr.Left : curr.Right;
        }
        return false;
    }

    /// <summary>
    /// In-order traversal yielding elements in sorted order.
    /// </summary>
    public IEnumerable<int> InOrder()
    {
        var stack = new Stack<TreapNode>();
        TreapNode? curr = Root;

        while (curr is not null || stack.Count > 0)
        {
            while (curr is not null)
            {
                stack.Push(curr);
                curr = curr.Left;
            }

            curr = stack.Pop();
            yield return curr.Key;
            curr = curr.Right;
        }
    }
}
```

### Python Secondary Implementation (Python 3.11+)

```python
import random
from typing import Optional, Tuple, Iterator

class TreapNode:
    __slots__ = ('key', 'priority', 'left', 'right')

    def __init__(self, key: int, priority: Optional[int] = None):
        self.key: int = key
        self.priority: int = priority if priority is not None else random.randint(1, 1 << 30)
        self.left: Optional['TreapNode'] = None
        self.right: Optional['TreapNode'] = None

class Treap:
    """Randomized balanced search tree maintaining BST on keys and Max-Heap on priorities."""

    def __init__(self):
        self.root: Optional[TreapNode] = None

    @staticmethod
    def split(t: Optional[TreapNode], key: int) -> Tuple[Optional[TreapNode], Optional[TreapNode]]:
        """
        Splits tree t into:
          - left: all nodes with key <= boundary
          - right: all nodes with key > boundary
        """
        if t is None:
            return None, None

        if t.key <= key:
            l_sub, r_sub = Treap.split(t.right, key)
            t.right = l_sub
            return t, r_sub
        else:
            l_sub, r_sub = Treap.split(t.left, key)
            t.left = r_sub
            return l_sub, t

    @staticmethod
    def merge(l: Optional[TreapNode], r: Optional[TreapNode]) -> Optional[TreapNode]:
        """
        Merges Treap l and Treap r assuming max(l.keys) < min(r.keys).
        Maintains Max-Heap invariant by priority comparison.
        """
        if l is None or r is None:
            return l or r

        if l.priority >= r.priority:
            l.right = Treap.merge(l.right, r)
            return l
        else:
            r.left = Treap.merge(l, r.left)
            return r

    def insert(self, key: int) -> None:
        """Inserts key into Treap if not already present."""
        if self.contains(key):
            return

        l, r = Treap.split(self.root, key)
        new_node = TreapNode(key)
        self.root = Treap.merge(Treap.merge(l, new_node), r)

    def delete(self, key: int) -> bool:
        """Removes key from Treap. Returns True if found and removed."""
        if not self.contains(key):
            return False

        l, rest = Treap.split(self.root, key - 1)
        _, r = Treap.split(rest, key)
        self.root = Treap.merge(l, r)
        return True

    def contains(self, key: int) -> bool:
        """Standard BST search in O(height) time."""
        curr = self.root
        while curr:
            if curr.key == key:
                return True
            curr = curr.left if key < curr.key else curr.right
        return False

    def in_order(self) -> Iterator[int]:
        """Generator yielding keys in ascending order."""
        stack = []
        curr = self.root
        while curr or stack:
            while curr:
                stack.append(curr)
                curr = curr.left
            curr = stack.pop()
            yield curr.key
            curr = curr.right
```

---

## 📊 Chapter 4: Explicit Complexity Deconstruction

| Operation | Best Case | Expected / Average | Worst Case | Auxiliary Space | Memory & Cache Reality |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Search** | `O(1)` (at root) | `O(log N)` | `O(N)` | `O(1)` iterative | Node pointers cause pointer chasing / cache misses. |
| **Split** | `O(1)` | `O(log N)` | `O(N)` | `O(log N)` stack | Recursive call stack bounded by tree height. |
| **Merge** | `O(1)` | `O(log N)` | `O(N)` | `O(log N)` stack | Touches only the right spine of `L` and left spine of `R`. |
| **Insert** | `O(1)` | `O(log N)` | `O(N)` | `O(log N)` stack | Split + 2 Merges; creates exactly 1 heap node. |
| **Delete** | `O(1)` | `O(log N)` | `O(N)` | `O(log N)` stack | 2 Splits + 1 Merge; unlinks 1 node for garbage collection. |
| **Skip List Search** | `O(1)` | `O(log N)` | `O(N)` | `O(1)` | Forward pointers per node; Level 0 linear scan has decent cache prefetching. |

### Mathematical Bound Justification
*   **Expected Depth of Key `i`:** Let keys be sorted `k_1 < k_2 < ... < k_N`. Key `k_j` is an ancestor of `k_i` if and only if `priority(k_j)` is the maximum priority among all keys in the range `[min(i, j), max(i, j)]`. Because priorities are independent and identically distributed uniform random variables, this happens with probability `1 / (|i - j| + 1)`.
*   Summing over all `j`:
    `E[depth(i)] = Sum_{j=1}^N (1 / (|i - j| + 1)) = H_i + H_{N-i+1} - 1 <= 2 * ln(N) + O(1)`.
*   Therefore, expected search, split, and merge time is strictly bounded by `O(log N)`.
*   **Worst-Case Probability:** A Treap degenerates to a line of length `N` only if priorities are generated in strictly monotonic order. The probability of this event is `1 / N!`, which for `N = 100` is less than `10^(-157)` (effectively zero in physical universe lifetimes).

---

## 🎙️ Chapter 5: 45-Minute Senior / Lead Verbal Interview Script

```text
[00:00 - 05:00] Clarify & Problem Modeling
Candidate: "We need a dynamic set that supports fast Search, Insert, and Delete. A standard BST gives O(log N) 
           average performance, but an adversarial sorted stream degrades it to O(N). AVL and Red-Black trees 
           guarantee worst-case O(log N) via deterministic rotations, but they require complex color flips and 
           double rotations. For this interview, would you like me to implement a Treap via Split and Merge, 
           or discuss the concurrent trade-offs of a Skip List?"
Interviewer: "Let's implement the Treap. Explain why it remains balanced."

[05:00 - 15:00] Invariants & Trade-offs
Candidate: "A Treap maintains two simultaneous invariants: BST on keys, and Max-Heap on uniformly distributed 
           random priorities. By the Cartesian uniqueness theorem, distinct keys and priorities yield a unique 
           tree topology—the exact same topology as if we inserted the keys in priority order into a regular BST.
           Since priorities are random, the expected tree height is 2 * ln(N), or approximately 1.39 * log2(N).
           Instead of writing left and right rotations, I will implement Split and Merge. 
           Split divides a Treap into keys <= K and keys > K in O(log N). 
           Merge takes two Treaps where all keys in L are strictly smaller than all keys in R, merging them in O(log N).
           Insert and Delete are simply combinations of Split and Merge."

[15:00 - 35:00] Coding Protocol & Invariant Preservation
Candidate: [Writes Split and Merge]
           "Notice how clean Split is: if root.Key <= K, the entire left subtree and root belong to L, so we 
           recursively split the right child. The boundary condition is base null returning (null, null).
           In Merge, we compare priorities to respect the Max-Heap property. If L.priority >= R.priority, L remains 
           the root, and its right child becomes Merge(L.right, R).
           Now Insert is trivial: Split at key, create node, Merge(Merge(left, node), right).
           Delete splits at key - 1 and key to isolate the target node, then merges the outer subtrees."

[35:00 - 45:00] Complexity Deconstruction & Production Systems
Candidate: "All operations run in expected O(log N) time with O(log N) recursion stack space.
           In production, if we were building Redis Sorted Sets or LevelDB MemTables, we would use a Skip List 
           instead of a Treap. Skip Lists allow lock-free concurrent updates by CAS-modifying forward pointers 
           without restructuring parent trees, and Level 0 provides O(1) contiguous pointer range scans."
```

---

## 🔍 Chapter 6: Granular Edge-Case & Invariant Verification Table

| Edge Case Scenario | Concrete Input | Expected Behavior | Invariant Verification & Handling |
| :--- | :--- | :--- | :--- |
| **Empty Treap Mutation** | `root = null; Delete(10)` | Returns `false`; `root` remains `null`. | Guard `if (t is null) return (null, null)` in `Split`. |
| **Duplicate Insertion** | `Insert(42); Insert(42)` | Second insert is a no-op; set size remains 1. | `Contains(key)` guard check prevents duplicate keys. |
| **Split Key Smaller Than All Elements** | Treap `{10, 20, 30}`, `Split(5)` | `L = null`, `R = {10, 20, 30}`. | Traversal goes purely left; `t.Left` becomes `null`. |
| **Split Key Larger Than All Elements** | Treap `{10, 20, 30}`, `Split(50)` | `L = {10, 20, 30}`, `R = null`. | Traversal goes purely right; `t.Right` becomes `null`. |
| **Merge with Empty Subtree** | `Merge(null, R)` or `Merge(L, null)` | Returns `R` or `L` directly in `O(1)`. | Base case returns `l ?? r` immediately without priority check. |
| **Single Node Deletion** | Treap `{42}`, `Delete(42)` | `root` becomes `null`. | `Split` isolates `mid = {42}`, outer `L` and `R` are `null`, `Merge(null, null)` returns `null`. |
| **Priority Collision** | Two nodes generated with equal priority | Handled gracefully via `>=` branch. | Stable comparison defaults to left tree being parent, preserving heap validity. |

---

> 🧭 **Navigation:** [← Week Overview](README.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_16_Day_02_Link_Cut_Trees_Instructional.md)

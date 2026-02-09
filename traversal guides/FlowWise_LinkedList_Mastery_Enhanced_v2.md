# 🔗 Flow-Wise Linked List Mastery Curriculum (v2, Level-wise)
**For:** Developers building interview-grade linked-list intuition  
**Format:** Why → What → How → Where → When, plus visuals, pitfalls, tips, and Python/C# snippets

---

# 🧭 One-page Level Mapping Index (quick lookup)
Use this index to instantly map a linked-list problem to the right **level**, the right **pattern**, and the **expected invariant**.

## ✅ Legend
- **Invariant** = statement that stays true after each step; if you can’t say it, you don’t own the pattern.
- **Dummy/Sentinel** = a node before head that eliminates head-edge cases.

## 🧩 Problem → Level → Invariant
| Level | Pattern / Skill | Common LeetCode problems (examples) | Expected invariant (one-liner) |
|---|---|---|---|
| L1 | Basic traversal | 1290 Convert Binary Number in a Linked List to Integer, 1721 Swapping Nodes in a Linked List | `curr` always points to the next unvisited node; processed prefix is final. |
| L1 | Null boundary control | 83 Remove Duplicates from Sorted List, 328 Odd Even Linked List | If you access `curr.next`, your guard guarantees it exists. |
| L1 | Lockstep iteration | 160 Intersection of Two Linked Lists, 234 Palindrome Linked List (compare phase) | Pointers move in sync; all nodes before them satisfy the condition. |
| L2 | Dummy/sentinel | 19 Remove Nth Node From End of List, 82 Remove Duplicates from Sorted List II | `prev` always points to node before the candidate; head deletion becomes normal. |
| L2 | Delete with `prev` | 203 Remove Linked List Elements, 237 Delete Node in a Linked List | Links remain connected; deleted nodes become unreachable. |
| L3 | Fast/slow pointers | 876 Middle of the Linked List, 141 Linked List Cycle, 142 Linked List Cycle II | `fast` moves 2x; if a cycle exists, pointers meet; else `fast` hits null. |
| L3 | Fixed-gap pointers | 19 Remove Nth Node From End of List, 61 Rotate List | Gap stays constant; when `fast` reaches end, `slow` is at target boundary. |
| L3 | Merge stitching | 21 Merge Two Sorted Lists, 23 Merge k Sorted Lists | `dummy.next..tail` is sorted and final; `a` and `b` are remaining tails. |
| L3 | Reverse (3-pointer) | 206 Reverse Linked List, 92 Reverse Linked List II (core reversal inside) | `prev` is reversed prefix; `curr` is next node to rewire. |
| L4 | Sublist reversal | 92 Reverse Linked List II, 25 Reverse Nodes in k-Group | `beforeRange` and `afterRange` remain connected correctly after reversal. |
| L4 | Stable partition (two chains) | 86 Partition List, 328 Odd Even Linked List | Low-chain and high-chain preserve order; tails always point to last node. |
| L4 | Dedup on sorted list | 83 Remove Duplicates from Sorted List, 82 Remove Duplicates from Sorted List II | `curr` points to last kept node; duplicates are skipped without losing remainder. |
| L5 | Composition: split+reverse+merge | 143 Reorder List, 234 Palindrome Linked List | Each stage preserves connectivity; final list terminates at null. |
| L5 | Linked-list merge sort | 148 Sort List, 147 Insertion Sort List | Each recursion returns sorted list; merge preserves sortedness. |
| L5 | Digit/carry pipelines | 2 Add Two Numbers, 445 Add Two Numbers II | `carry` is correct after each digit; result list built is valid so far. |
| L5 | Clone/copy pointer structures | 138 Copy List with Random Pointer, 430 Flatten a Multilevel Doubly Linked List | Each original maps to exactly one clone; pointers connect to clones only. |

---

# 🧠 Mastery mindset: complexity of state
Linked lists are hard because the **state is references**.
- Arrays: move by index; random access is cheap.
- Lists: move by following pointers; incorrect rewiring loses nodes.

Your goal at every level: **state the invariant → choose guard → save next → rewire safely**.

---

# 🧱 Baseline node definitions (for snippets)

## 🐍 Python
```python
class ListNode:
    def __init__(self, val=0, next=None):
        self.val = val
        self.next = next
```

## 🟦 C#
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

# 🟢 Level 1: The Physical Layer (Pointer Movement)
**Goal:** Muscle memory for safe traversal.

## Topics to know
- Forward traversal
- Null boundary control (`curr`, `curr.next` guards)
- Empty & single-node edge cases
- Lockstep traversal (two lists)
- Counting length and finding tail

## Things to master
- Never dereference `.next` unless your loop guard guarantees it
- Write and verify the traversal invariant in one sentence
- Build a habit of testing: empty, 1 node, 2 nodes, duplicates

---

## 1.1 ➡️ Basic traversal

**Why:** Almost every linked-list task begins as “walk through nodes and observe state.”

**What:** Start at `head`, follow `next` until null.

**How (step/flow):**
1) `curr = head`
2) While `curr` exists: process
3) `curr = curr.next`

**Where:** searching, counting, converting list→array, building sums.

**When:** default pattern for read-only operations.

**Visual**
```text
head → [A] → [B] → [C] → null
         ^
        curr moves →
```

**Python**
```python
curr = head
while curr:
    # use curr.val
    curr = curr.next
```

**Common pitfalls**
- ❌ `curr = curr.next.next` without checking `curr.next`
- ❌ infinite traversal if there is a cycle

**Tips**
- 💡 If cycle is possible, use fast/slow (Level 3).

---

## 1.2 🛑 Boundary control (null termination)

**Why:** Your “array bounds” are now null checks.

**What:** Choose a loop guard based on what you access.
- `while curr:` safe for `curr.val`
- `while curr and curr.next:` safe for `curr.next.val` and `curr.next.next` checks (with additional check)

**How (step/flow):**
1) Decide deepest pointer you will access
2) Choose guard that guarantees it
3) Keep it consistent

**Where:** dedup loops, pairwise operations, deletion of `curr.next`.

**When:** you access neighbor nodes.

**Pitfalls**
- ❌ `while curr.next:` when `curr` can be null

**Tip**
- 💡 “Guard matches access depth.” Say it before coding.

---

## 1.3 🧩 Edge cases (empty & single)

**Why:** Many pointer patterns assume at least 2 nodes.

**What:**
- Empty: `head == null`
- Single: `head != null` and `head.next == null`

**How:** Guard early if your algorithm needs neighbors.

**Pitfalls**
- ❌ doing `head.next` without checking head

**Tip**
- 💡 Always run a dry-run for (n=0) and (n=1).

---

## 1.4 🧵 Lockstep traversal (two lists)

**Why:** Comparing or aligning two lists requires synchronized motion.

**What:** Move two pointers together.

**How (step/flow):**
1) `p=headA`, `q=headB`
2) while p and q: process
3) p=p.next, q=q.next

**Where:** equality checks, aligned comparisons, intersection after alignment.

**When:** lists are aligned by position.

**Visual**
```text
p → [A1] → [A2] → [A3] → null
q → [B1] → [B2] → null
     ^       ^
    lockstep until q ends
```

**Pitfalls**
- ❌ assuming equal length without deciding behavior

---

## 1.5 📏 Length & tail

**Why:** Many problems need tail connections (rotate) or length (k-th from end).

**What:** One pass can produce both length and tail (acyclic assumption).

**How:** track `count`, and update `tail = curr` each step.

**Pitfall**
- ❌ this hangs on cyclic lists

---

# 🔵 Level 2: The Structural Layer (Local Rewiring)
**Goal:** Safe insertion/deletion and pointer hygiene.

## Topics to know
- Head insertion
- Delete with `prev`
- Save-next-before-rewire rule
- Dummy/sentinel node

## Things to master
- The “save next” habit
- Dummy node reflex when head may change
- Guarantee termination: ensure final tail points to null

---

## 2.1 ➕ Insertion at head

**Why:** Simplest rewiring; builds confidence.

**What:** new.next=head; head=new.

**How:** do in that exact order.

**Where:** building lists from streams, stack-like behavior.

**Pitfalls**
- ❌ overwriting head too early (losing old head)

---

## 2.2 ➖ Deletion with `prev`

**Why:** Singly list can’t go backward; you must keep the node before the one you delete.

**What:** `prev.next = curr.next` deletes `curr`.

**How (step/flow):**
1) prev points to node before curr
2) rewire prev.next
3) move curr safely

**Pitfalls**
- ❌ deleting head without special handling

---

## 2.3 🧷 Save `next` before rewiring (golden rule)

**Why:** If you change `curr.next` first, you can lose the remainder.

**What:** `nxt = curr.next` before any modification.

**How:** save → rewire → move.

**Pitfalls**
- ❌ rewire then try to move (remainder lost)

---

## 2.4 🧸 Dummy / sentinel node

**Why:** Eliminates “head special case” in deletions and some insertions.

**What:** `dummy.next = head`; operate starting from dummy.

**How (step/flow):**
1) create dummy
2) prev starts at dummy
3) return dummy.next

**Pitfalls**
- ❌ forgetting to return dummy.next

---

# 🟠 Level 3: The Multi-View Layer (Two Pointers + Stitching)
**Goal:** Two-pointer invariants and pointer stitching.

## Topics to know
- Fast/slow pointers (cycle, middle)
- Fixed-gap pointers (nth from end)
- Merge two sorted lists
- Reverse list (prev/curr/nxt)

## Things to master
- Correct while-guards (`fast and fast.next`)
- Dummy+tail builder for merges
- Reversal as a state machine

---

## 3.1 🐢🐇 Fast/slow pointers

**Why:** Detect cycles and find middle without extra memory.

**What:** slow moves 1; fast moves 2.

**How:** loop while fast and fast.next.

**Pitfalls**
- ❌ incorrect guard causes null dereference

---

## 3.2 🎯 Fixed-gap pointers (nth from end)

**Why:** One pass; no length needed.

**What:** Keep fast n steps ahead.

**How:** advance fast n, then move both until fast at end.

**Pitfalls**
- ❌ off-by-one in how far fast advances

---

## 3.3 🤝 Merge two sorted lists

**Why:** Foundational “stitching” primitive.

**What:** tail always points to last node of output.

**How:** attach smaller node; advance that list; advance tail.

**Invariant:** output prefix is sorted and final.

---

## 3.4 🔁 Reverse list (3-pointer)

**Why:** Most common transformation.

**What:** reverse pointers by maintaining prev/curr/nxt.

**How:** save nxt → curr.next=prev → advance.

---

# 🟣 Level 4: The Range Layer (Sublist Operations)
**Goal:** Operations on contiguous segments / stable grouping.

## Topics to know
- Reverse sublist [m..n]
- Reverse nodes in k-group
- Stable partition (two-chain build)
- Dedup patterns in sorted lists
- Two-list alignment tricks (intersection)

## Things to master
- Anchor pointers: beforeRange, rangeTail, afterRange
- Tail termination to avoid accidental cycles
- Stable grouping via two chains (preserve order)

---

# 🔴 Level 5: The Abstract Layer (Composition)
**Goal:** Combine primitives into full algorithms.

## Topics to know
- Palindrome list (split + reverse + compare)
- Reorder list (split + reverse + weave)
- Sort list (merge sort composition)
- Digit/carry pipelines (add numbers)
- Clone/copy pointer structures

## Things to master
- Stage invariants: each stage returns a well-formed list
- No lost nodes: connectivity preserved
- Final termination: last.next must be null

---

# 🧰 Traversal Mastery Add-ons (high leverage)

## A) 🧪 Micro-tracing method (paper debugging)
Use this notation in interviews:
- Draw nodes as boxes: [1]→[2]→[3]
- Write pointer names under boxes (prev, curr, fast, slow)
- After each operation, update pointers before going next

## B) 🧱 “Invariant library” (say these out loud)
- Reverse: `prev` is reversed prefix; `curr` is next to reverse.
- Merge: `dummy.next..tail` is sorted and final.
- Dedup: `curr` is last kept unique; duplicates are skipped.
- Partition: lowTail/highTail always point to list tails; both chains preserve order.

## C) ✅ Safety checklist (quick)
- Did I save nxt before rewiring?
- If head can change, did I use dummy?
- Does my last node end with next = null?
- Am I comparing node identity (same node) vs values?

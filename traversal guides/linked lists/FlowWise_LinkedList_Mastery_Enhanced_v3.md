# 🔗 Problem-Solving Curriculum 2.0: Linked List Mastery

## 🧭 The One-Page Linked List Blueprint

| Level | Mental Model | Pointer State (Invariant) | Drill Problems |
| :---: | :--- | :--- | :--- |
| **L1: Physical Layer** | Safe traversal & Null boundaries | `curr` always points to next unvisited node; processed prefix is final. | [LeetCode 1290: Convert Binary Number in a Linked List to Integer], [LeetCode 83: Remove Duplicates from Sorted List] |
| **L2: Structural Layer** | Dummy nodes & Deletion | `prev` points to the node *before* the target; target bypassed safely. | [LeetCode 203: Remove Linked List Elements], [LeetCode 24: Swap Nodes in Pairs] |
| **L3: Multi-View Layer** | Fast/Slow & Fixed-Gap | `fast` moves 2x; cycle = meet. `prev` is reversed prefix, `curr` is next. | [LeetCode 141: Linked List Cycle], [LeetCode 206: Reverse Linked List] |
| **L4: Range Layer** | Sublist Anchoring & Stitching | Anchor nodes connect to boundaries; sublist tails terminate at `null`. | [LeetCode 92: Reverse Linked List II], [LeetCode 86: Partition List] |
| **L5: Abstract Layer** | Multi-stage Composition | Independent stages return well-formed lists; no cycles introduced. | [LeetCode 234: Palindrome Linked List], [LeetCode 143: Reorder List] |

---

## 🧱 Baseline Node Definitions

Problem: Define the fundamental building block of a singly linked list. Each node must store data and a reference to the next node.

```python
# Python
class ListNode:
    # 1. Initialize node with a value and an optional next pointer
    def __init__(self, val=0, next=None):
        self.val = val
        self.next = next
```

```csharp
// C#
public class ListNode {
    // 1. Declare fields for the node's value and next pointer
    public int val;
    public ListNode next;
    // 2. Initialize node with a value and an optional next pointer
    public ListNode(int val = 0, ListNode next = null) {
        this.val = val;
        this.next = next;
    }
}
```

---

## 🟢 Level 1: The Physical Layer (Pointer Movement)

**Mental Model:** Linked list traversal is safe reference management. Null is your out-of-bounds marker.
**Invariant:** `curr` represents the node currently being processed. The prefix up to `curr` is fully processed and immutable.

### Basic Traversal & Null Boundary Control

**State Transitions:**
| Step | `curr` | Action / Invariant |
| :--- | :--- | :--- |
| 0 | `head` | Setup: `curr` starts at the beginning. |
| 1 | Node A | Process A. `curr` points to A. |
| 2 | Node B | Process B. `curr` advanced to `curr.next`. |
| 3 | `null` | Traversal complete. `curr` is null. |

**Code Snippet:**

Problem: Traverse a linked list from start to finish to process each node's value. Ensure the traversal stops safely at the null boundary.

```python
# Python
# 1. Initialize pointer to the head of the list
curr = head
# 2. Iterate safely until hitting the null boundary
while curr:
    # 3. Process the current node's value
    print(curr.val)
    # 4. Advance pointer to the next unvisited node
    curr = curr.next
```

```csharp
// C#
// 1. Initialize pointer to the head of the list
ListNode curr = head;
// 2. Iterate safely until hitting the null boundary
while (curr != null) {
    // 3. Process the current node's value
    Console.WriteLine(curr.val);
    // 4. Advance pointer to the next unvisited node
    curr = curr.next;
}
```

### ⚠️ Gotchas & Pitfalls
* **Null Pointer Exception on Empty Input:** Always check if `head` is null before trying to access `head.val` or `head.next` outside of a safe `while(curr)` loop.
* **Infinite Loops:** Forgetting to advance the pointer (`curr = curr.next`) inside the traversal loop will cause it to run forever.
* **Premature Termination:** Returning `curr` instead of `head` at the end; `curr` is usually `null` by the end of a full traversal.

**Drill Problems (L1):**
* **Easy:** [LeetCode 1290: Convert Binary Number in a Linked List to Integer], [LeetCode 83: Remove Duplicates from Sorted List]
* **Medium:** [LeetCode 1721: Swapping Nodes in a Linked List], [LeetCode 328: Odd Even Linked List]

---

## 🔵 Level 2: The Structural Layer (Local Rewiring)

**Mental Model:** Changing structure (insertion/deletion) requires a reference to the node *before* the target (`prev`). A `dummy` node eliminates special edge cases for deleting the head.
**Invariant:** `prev` always safely anchors the list before the modification point. Before modifying `curr.next`, `curr.next` must be saved.

### Dummy Sentinels & The "Save Next" Rule

**State Transitions (Deletion):**
| Step | `prev` | `curr` | Action / Invariant |
| :--- | :--- | :--- | :--- |
| 0 | `dummy` | `head` | Setup: Dummy points to head. `prev` is before `curr`. |
| 1 | `dummy` | Node A (Target) | `curr` is target. Rewire: `prev.next = curr.next`. |
| 2 | `dummy` | Node B | `curr` advances. `prev` remains anchored before new `curr`. |

**Code Snippet:**

Problem: Remove all nodes in a linked list that match a specific target value. Safely bypass the targets without losing the rest of the list.

```python
# Python
# 1. Create a dummy sentinel node to safely handle head deletions
dummy = ListNode(0, head)
# 2. Initialize prev to anchor before current, and curr to the head
prev, curr = dummy, head

# 3. Traverse the list up to the null boundary
while curr:
    if curr.val == target:
        # 4. Target found: bypass curr by linking prev to curr's next
        prev.next = curr.next
    else:
        # 5. Target not found: advance prev safely
        prev = curr
    # 6. Always advance curr to the next unvisited node
    curr = curr.next
```

```csharp
// C#
// 1. Create a dummy sentinel node to safely handle head deletions
ListNode dummy = new ListNode(0, head);
// 2. Initialize prev to anchor before current, and curr to the head
ListNode prev = dummy;
ListNode curr = head;

// 3. Traverse the list up to the null boundary
while (curr != null) {
    if (curr.val == target) {
        // 4. Target found: bypass curr by linking prev to curr's next
        prev.next = curr.next; 
    } else {
        // 5. Target not found: advance prev safely
        prev = curr;
    }
    // 6. Always advance curr to the next unvisited node
    curr = curr.next;
}
```

### ⚠️ Gotchas & Pitfalls
* **Losing the Head Node:** If the head itself needs deletion and you didn't use a dummy node, you might accidentally return the deleted head instead of the new head.
* **Skipping Consecutive Targets:** Advancing `prev` when a deletion occurs. If two adjacent nodes need to be deleted, advancing `prev` too early will skip the second target.
* **Memory Leaks:** In languages without automatic garbage collection, forgetting to free the memory of the bypassed node can cause memory leaks.

**Drill Problems (L2):**
* **Easy:** [LeetCode 203: Remove Linked List Elements]
* **Medium:** [LeetCode 82: Remove Duplicates from Sorted List II], [LeetCode 24: Swap Nodes in Pairs]

---

## 🟠 Level 3: The Multi-View Layer (Two Pointers & Stitching)

**Mental Model:** Advanced operations require maintaining multiple independent views (Fast/Slow, Reversal).
**Invariants:**
- *Fast/Slow:* `fast` travels exactly 2x `slow`. If there is a cycle, they will meet. If not, `fast` bounds checking applies.
- *Reversal:* `prev` holds the fully reversed prefix. `curr` is the next node to process.

### Reversing a Linked List

**State Transitions (Reversal):**
| Step | `prev` | `curr` | Action / Invariant |
| :--- | :--- | :--- | :--- |
| 0 | `null` | Node A | Setup: Prefix is empty (`null`). `curr` is start. |
| 1 | Node A | Node B | Save B. `A.next = null`. Prefix is A. `curr` becomes B. |
| 2 | Node B | Node C | Save C. `B.next = A`. Prefix is B->A. `curr` becomes C. |
| Final| Tail | `null` | `curr` hits null. `prev` points to new head of reversed list. |

**Code Snippet:**

Problem: Reverse a singly linked list in-place. Maintain a reversed prefix while traversing and rewiring each node's next pointer.

```python
# Python
# 1. prev represents the fully reversed prefix, initially null
prev, curr = None, head
# 2. Traverse until the next unvisited node is null
while curr:
    # 3. Save the next node before modifying curr's pointer
    nxt = curr.next
    # 4. Rewire curr to point backward to the reversed prefix
    curr.next = prev
    # 5. Advance prev (the reversed prefix grows)
    prev = curr
    # 6. Advance curr to process the next unvisited node
    curr = nxt
```

```csharp
// C#
// 1. prev represents the fully reversed prefix, initially null
ListNode prev = null;
ListNode curr = head;
// 2. Traverse until the next unvisited node is null
while (curr != null) {
    // 3. Save the next node before modifying curr's pointer
    ListNode nxt = curr.next;
    // 4. Rewire curr to point backward to the reversed prefix
    curr.next = prev;
    // 5. Advance prev (the reversed prefix grows)
    prev = curr;
    // 6. Advance curr to process the next unvisited node
    curr = nxt;
}
```

### ⚠️ Gotchas & Pitfalls
* **Cycle Check `NullReferenceException`:** When using fast/slow pointers, writing `while(fast != null)` but failing to check `fast.next != null` before doing `fast = fast.next.next`.
* **Cycle Off-By-One:** Depending on initialization (`fast = head` vs `fast = head.next`), they might meet at a different point in the cycle.
* **Lost "Next" Reference in Reversal:** Updating `curr.next` without first saving it into a temporary variable, severing the rest of the list.

**Drill Problems (L3):**
* **Easy:** [LeetCode 206: Reverse Linked List], [LeetCode 141: Linked List Cycle], [LeetCode 876: Middle of the Linked List]
* **Medium:** [LeetCode 19: Remove Nth Node From End of List]

---

## 🟣 Level 4: The Range Layer (Sublist Operations)

**Mental Model:** Operating on a segment (sublist) requires identifying and holding anchors (the node before the sublist and the node after the sublist).
**Invariant:** Anchor nodes maintain connection to the rest of the list. A processed sublist must be correctly terminated or spliced.

### Stable Partitioning & Anchoring

**State Transitions (Partitioning into Less and Greater):**
| Step | `less` tail | `greater` tail | Action / Invariant |
| :--- | :--- | :--- | :--- |
| 0 | `less_dummy` | `greater_dummy` | Setup chains. |
| 1 | Node < x | `greater_dummy` | Append to `less` chain. Advance `less`. |
| 2 | Node < x | Node >= x | Append to `greater` chain. Advance `greater`. |
| Final| Last < x | Last >= x | Stitch: `less.next = greater_dummy.next`. Terminate: `greater.next = null`. |

**Code Snippet:**

Problem: Partition a linked list around a value `x`, such that all nodes less than `x` come before nodes greater than or equal to `x`. Preserve the original relative order.

```python
# Python
# 1. Create dummy heads to anchor the 'less' and 'greater' sublists
less_dummy, greater_dummy = ListNode(0), ListNode(0)
# 2. Initialize tail pointers for both chains
less, greater = less_dummy, greater_dummy
curr = head

# 3. Traverse the original list
while curr:
    if curr.val < x:
        # 4. Append to the 'less' chain and advance its tail
        less.next = curr
        less = less.next
    else:
        # 5. Append to the 'greater' chain and advance its tail
        greater.next = curr
        greater = greater.next
    # 6. Advance curr to process the next node
    curr = curr.next

# 7. Stitch the less chain's tail to the head of the greater chain
less.next = greater_dummy.next
# 8. Terminate the final tail node to prevent cycles
greater.next = None
```

```csharp
// C#
// 1. Create dummy heads to anchor the 'less' and 'greater' sublists
ListNode lessDummy = new ListNode(0);
ListNode greaterDummy = new ListNode(0);
// 2. Initialize tail pointers for both chains
ListNode less = lessDummy, greater = greaterDummy;
ListNode curr = head;

// 3. Traverse the original list
while (curr != null) {
    if (curr.val < x) {
        // 4. Append to the 'less' chain and advance its tail
        less.next = curr;
        less = less.next;
    } else {
        // 5. Append to the 'greater' chain and advance its tail
        greater.next = curr;
        greater = greater.next;
    }
    // 6. Advance curr to process the next node
    curr = curr.next;
}
// 7. Stitch the less chain's tail to the head of the greater chain
less.next = greaterDummy.next;
// 8. Terminate the final tail node to prevent cycles
greater.next = null;
```

### ⚠️ Gotchas & Pitfalls
* **Accidental Cycles on Reconnection:** Forgetting to terminate the final tail node (e.g., `greater.next = null`), causing a cycle back into the stitched list.
* **Orphaned Sublists:** Losing the anchor to the segment *before* the reversed/modified sublist, making it impossible to stitch the modified segment back into the main list.
* **Boundary Edge Cases:** Dealing with `left = 1` in sublist reversal where there is no node *before* the reversed segment unless a dummy head is used.

**Drill Problems (L4):**
* **Medium:** [LeetCode 92: Reverse Linked List II], [LeetCode 86: Partition List]
* **Hard:** [LeetCode 25: Reverse Nodes in k-Group]

---

## 🔴 Level 5: The Abstract Layer (Composition)

**Mental Model:** Complex problems are combinations of L1-L4 components. Build modularly.
**Invariant:** Each composed stage (find mid, reverse half, interleave) receives a well-formed list and outputs a well-formed list with no loss of nodes or cycles.

### Multi-Stage Composition (E.g., Reorder List)

**State Transitions (Reorder List):**
| Stage | Operation | Output / Invariant |
| :--- | :--- | :--- |
| 1 | Fast/Slow (L3) | Find middle. Split into `L1` (head to mid) and `L2` (mid to end). |
| 2 | Reverse (L3) | `L2` becomes reversed. `prev` is new head of `L2`. |
| 3 | Lockstep (L1/L2) | Weave `L1` and `L2`. Temporary pointers save next nodes during rewiring. |

**Code Snippet (Weaving Phase):**

Problem: Interleave two well-formed linked lists (L1 and reversed L2) in lockstep. Safely weave their nodes without losing references to the remaining chains.

```python
# Python - Weaving L1 and L2
# 1. p1 anchors the first half, p2 anchors the reversed second half
p1, p2 = head, reversed_l2_head
# 2. Iterate while the second half still has nodes
while p2: 
    # 3. Save the next nodes for both halves
    nxt1, nxt2 = p1.next, p2.next
    
    # 4. Rewire: insert p2 between p1 and p1's original next node
    p1.next = p2
    p2.next = nxt1
    
    # 5. Advance both pointers to the saved next nodes
    p1, p2 = nxt1, nxt2
```

```csharp
// C# - Weaving L1 and L2
// 1. p1 anchors the first half, p2 anchors the reversed second half
ListNode p1 = head, p2 = reversedL2Head;
// 2. Iterate while the second half still has nodes
while (p2 != null) {
    // 3. Save the next nodes for both halves
    ListNode nxt1 = p1.next;
    ListNode nxt2 = p2.next;
    
    // 4. Rewire: insert p2 between p1 and p1's original next node
    p1.next = p2;
    p2.next = nxt1;
    
    // 5. Advance both pointers to the saved next nodes
    p1 = nxt1;
    p2 = nxt2;
}
```

### ⚠️ Gotchas & Pitfalls
* **Unterminated Halves:** When splitting a list (e.g., for merge sort or reordering), forgetting to set the `next` pointer of the first half's tail to `null`, causing the two halves to still be connected in a cycle or memory leak.
* **Mismatched Lengths in Weaving:** Not safely handling odd vs. even length lists during weaving; `p2` might run out of nodes before `p1` or vice-versa.
* **Over-Complexity:** Trying to do everything in one pass. It's often much safer and cleaner to do multiple independent passes (find mid, then reverse, then merge).

**Drill Problems (L5):**
* **Medium:** [LeetCode 143: Reorder List], [LeetCode 148: Sort List]
* **Easy/Medium:** [LeetCode 234: Palindrome Linked List]

---

## ✅ Safety Rules Checklist
- [ ] **Did I save `nxt` before modifying `curr.next`?**
- [ ] **Did I use a `dummy` node if the `head` might change or be deleted?**
- [ ] **Did I explicitly set my final node's `.next` to `null` to prevent cycles?**
- [ ] **Are my `while` boundaries safe?** (`while curr` vs `while curr and curr.next`)

# 🧪 Flow-Wise Linked List Mastery — Practice Set (v2)
**Requirement satisfied:** At least 2 LeetCode problems per **subtopic**.

**Format per item:**
- ⏱️ Timebox
- 🧾 Expected invariant

---

# 🟢 Level 1 — Physical Layer (Pointer Movement)

## 1.1 Basic traversal
- LeetCode 1290: Convert Binary Number in a Linked List to Integer
  - ⏱️ 15–25 min
  - 🧾 Invariant: running result equals value of processed prefix.
- LeetCode 1721: Swapping Nodes in a Linked List
  - ⏱️ 30–45 min
  - 🧾 Invariant: traversal count matches nodes visited; identified nodes are correct.

## 1.2 Boundary control (null guards)
- LeetCode 83: Remove Duplicates from Sorted List
  - ⏱️ 20–30 min
  - 🧾 Invariant: `curr` is last kept node; if `curr.next` duplicates, you skip it without moving curr.
- LeetCode 328: Odd Even Linked List
  - ⏱️ 30–45 min
  - 🧾 Invariant: oddTail and evenTail always point to correct chain tails; chains preserve relative order.

## 1.3 Edge cases (empty & single)
- LeetCode 206: Reverse Linked List
  - ⏱️ 25–35 min
  - 🧾 Invariant: prev is reversed prefix; curr is next to rewire.
- LeetCode 21: Merge Two Sorted Lists
  - ⏱️ 25–35 min
  - 🧾 Invariant: output prefix is sorted and final after each attach.

## 1.4 Lockstep traversal
- LeetCode 160: Intersection of Two Linked Lists
  - ⏱️ 35–55 min
  - 🧾 Invariant: after alignment, both pointers have equal remaining distance to tail.
- LeetCode 234: Palindrome Linked List (comparison phase)
  - ⏱️ 45–75 min
  - 🧾 Invariant: nodes compared so far match; pointers advance in sync.

## 1.5 Length & tail
- LeetCode 61: Rotate List
  - ⏱️ 45–60 min
  - 🧾 Invariant: tail and length are accurate; reconnection forms a temporary cycle that you break correctly.
- LeetCode 19: Remove Nth Node From End of List (length-based variant)
  - ⏱️ 30–45 min
  - 🧾 Invariant: computed index from start corresponds to nth from end.

---

# 🔵 Level 2 — Structural Layer (Local Rewiring)

## 2.1 Insertion at head
- LeetCode 707: Design Linked List (insert at head)
  - ⏱️ 45–75 min
  - 🧾 Invariant: head always points to first node; size tracks nodes.
- LeetCode 147: Insertion Sort List (inserting nodes into sorted prefix)
  - ⏱️ 60–90 min
  - 🧾 Invariant: sorted prefix remains sorted after each insertion.

## 2.2 Delete with prev tracking
- LeetCode 203: Remove Linked List Elements
  - ⏱️ 25–40 min
  - 🧾 Invariant: prev.next always points to next candidate; removed nodes become unreachable.
- LeetCode 83: Remove Duplicates from Sorted List
  - ⏱️ 20–30 min
  - 🧾 Invariant: curr points to last kept unique.

## 2.3 Save-next-before-rewire
- LeetCode 206: Reverse Linked List
  - ⏱️ 25–35 min
  - 🧾 Invariant: nxt saved before rewiring; remainder remains reachable.
- LeetCode 24: Swap Nodes in Pairs
  - ⏱️ 35–55 min
  - 🧾 Invariant: processed portion is correctly swapped; curr points to next unswapped pair.

## 2.4 Dummy/sentinel technique
- LeetCode 19: Remove Nth Node From End of List
  - ⏱️ 30–45 min
  - 🧾 Invariant: dummy makes deleting head identical to deleting middle.
- LeetCode 82: Remove Duplicates from Sorted List II
  - ⏱️ 45–70 min
  - 🧾 Invariant: prev always points to last confirmed unique node.

---

# 🟠 Level 3 — Multi-View Layer (Two Pointers + Stitching)

## 3.1 Fast/slow pointers
- LeetCode 876: Middle of the Linked List
  - ⏱️ 15–25 min
  - 🧾 Invariant: fast advances 2x; slow at middle when fast ends.
- LeetCode 141: Linked List Cycle
  - ⏱️ 20–30 min
  - 🧾 Invariant: if cycle exists, slow and fast meet.

## 3.2 Cycle start (advanced fast/slow)
- LeetCode 142: Linked List Cycle II
  - ⏱️ 45–70 min
  - 🧾 Invariant: pointer from head and meet point converge at entry.
- LeetCode 287: Find the Duplicate Number (tortoise/hare on array-as-linked-list)
  - ⏱️ 45–70 min
  - 🧾 Invariant: mapping forms a cycle; meeting logic holds.

## 3.3 Fixed-gap pointers
- LeetCode 19: Remove Nth Node From End of List
  - ⏱️ 30–45 min
  - 🧾 Invariant: gap remains n; slow ends before target.
- LeetCode 61: Rotate List (gap/connection reasoning)
  - ⏱️ 45–60 min
  - 🧾 Invariant: cut position is length-k; list reconnected and terminated.

## 3.4 Merge stitching
- LeetCode 21: Merge Two Sorted Lists
  - ⏱️ 25–35 min
  - 🧾 Invariant: dummy.next..tail is sorted and final.
- LeetCode 23: Merge k Sorted Lists
  - ⏱️ 60–90 min
  - 🧾 Invariant: each merge step preserves sortedness; heap always contains smallest current heads.

## 3.5 Reverse list
- LeetCode 206: Reverse Linked List
  - ⏱️ 25–35 min
  - 🧾 Invariant: prev is reversed prefix.
- LeetCode 92: Reverse Linked List II (core reversal)
  - ⏱️ 45–70 min
  - 🧾 Invariant: reversal stays within range; anchors reconnect correctly.

---

# 🟣 Level 4 — Range Layer (Sublist Operations)

## 4.1 Reverse sublist [m..n]
- LeetCode 92: Reverse Linked List II
  - ⏱️ 45–70 min
  - 🧾 Invariant: beforeRange and afterRange preserved; reversed segment well-formed.
- LeetCode 25: Reverse Nodes in k-Group
  - ⏱️ 60–95 min
  - 🧾 Invariant: each k-block reversed; remainder untouched and connected.

## 4.2 Stable partition (two chains)
- LeetCode 86: Partition List
  - ⏱️ 35–55 min
  - 🧾 Invariant: lowTail/highTail always point to tails; order preserved.
- LeetCode 328: Odd Even Linked List
  - ⏱️ 30–45 min
  - 🧾 Invariant: odd/even chains preserve order; final connect correct.

## 4.3 Dedup on sorted list
- LeetCode 83: Remove Duplicates from Sorted List
  - ⏱️ 20–30 min
  - 🧾 Invariant: `curr` last unique; duplicates skipped.
- LeetCode 82: Remove Duplicates from Sorted List II
  - ⏱️ 45–70 min
  - 🧾 Invariant: prev points to last confirmed unique; duplicate runs fully removed.

## 4.4 Two-list alignment / intersection
- LeetCode 160: Intersection of Two Linked Lists
  - ⏱️ 35–55 min
  - 🧾 Invariant: aligned pointers have equal distance to tail.
- LeetCode 1669: Merge In Between Linked Lists
  - ⏱️ 45–70 min
  - 🧾 Invariant: you cut exactly [a..b] in list1 and stitch list2 preserving connectivity.

---

# 🔴 Level 5 — Abstract Layer (Composition)

## 5.1 Palindrome composition
- LeetCode 234: Palindrome Linked List
  - ⏱️ 45–75 min
  - 🧾 Invariant: second half reversed; lockstep compare is consistent.
- LeetCode 2130: Maximum Twin Sum of a Linked List
  - ⏱️ 45–70 min
  - 🧾 Invariant: reversed half aligns twins; max tracked correctly.

## 5.2 Reorder / weave
- LeetCode 143: Reorder List
  - ⏱️ 60–90 min
  - 🧾 Invariant: split halves intact; weave alternates without losing remainder.
- LeetCode 24: Swap Nodes in Pairs (weaving-lite)
  - ⏱️ 35–55 min
  - 🧾 Invariant: processed part swapped; next pointer to continue is correct.

## 5.3 Sorting lists
- LeetCode 148: Sort List
  - ⏱️ 90–140 min
  - 🧾 Invariant: each recursion returns sorted list; merge preserves sortedness.
- LeetCode 147: Insertion Sort List
  - ⏱️ 60–90 min
  - 🧾 Invariant: sorted prefix stays sorted after insertion.

## 5.4 Digit + carry pipelines
- LeetCode 2: Add Two Numbers
  - ⏱️ 45–70 min
  - 🧾 Invariant: carry correct after each digit; result tail always last node.
- LeetCode 445: Add Two Numbers II
  - ⏱️ 60–95 min
  - 🧾 Invariant: stacks (or reversed lists) produce correct digit alignment.

## 5.5 Clone/copy pointer structures
- LeetCode 138: Copy List with Random Pointer
  - ⏱️ 75–120 min
  - 🧾 Invariant: each original maps to exactly one clone; pointers connect to clones.
- LeetCode 430: Flatten a Multilevel Doubly Linked List
  - ⏱️ 60–100 min
  - 🧾 Invariant: prev/next pointers remain consistent after flattening; tail correct.

---

## ✅ Mastery checklist (final)
- You can write reverse/merge/remove-nth without looking.
- You can state invariants for: reverse, merge, cycle detection, dummy deletion, partition.
- You always run: empty, 1 node, 2 nodes, duplicates, “remove head”, “remove tail”.

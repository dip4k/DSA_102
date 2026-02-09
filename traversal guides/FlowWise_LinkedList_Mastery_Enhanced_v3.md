# 🔗 Flow-Wise Linked List Mastery (v3)
**Goal:** Master traversal + pointer rewiring with invariants (Level 1 → Level 5).  
**Core idea:** Linked lists aren’t about clever math—they’re about safe reference management.

---

# 🧭 One-page Level Mapping Index (Linked Lists)
Use this to instantly map a linked-list problem to its **level**, **pattern**, and **invariant**.

| Level | Pattern / skill | Typical problems (LeetCode examples) | Expected invariant (one-liner) |
|---|---|---|---|
| L1 | Basic traversal | 1290 Convert Binary Number in a LL, 1721 Swapping Nodes in a LL | “curr always points to next unvisited node; processed prefix is final.” |
| L1 | Null boundary control | 83 Remove Duplicates from Sorted List, 328 Odd Even Linked List | “If you access curr.next, guard guarantees it exists.” |
| L2 | Dummy/sentinel + prev deletion | 203 Remove LL Elements, 19 Remove Nth From End, 82 Remove Duplicates II | “prev always points to node before candidate; head deletion becomes normal.” |
| L3 | Fast/slow pointers | 876 Middle, 141 Cycle, 142 Cycle II | “fast moves 2x; cycle => meet; no cycle => fast hits null.” [web:84] |
| L3 | Reverse (prev/curr/nxt) | 206 Reverse LL, 92 Reverse LL II (core) | “prev is reversed prefix; curr is next node to rewire.” |
| L3 | Merge stitching | 21 Merge Two Sorted Lists, 23 Merge k Sorted Lists | “dummy.next..tail is final sorted prefix.” |
| L4 | Sublist operations | 25 Reverse k-Group, 86 Partition List, 160 Intersection | “anchors remain connected; tails terminate at null.” |
| L5 | Composition | 234 Palindrome, 143 Reorder, 148 Sort List, 138 Copy Random Pointer | “Each stage returns a well-formed list; no nodes lost; no cycles introduced.” |

---

# 🧱 Baseline node definitions

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
- ➡️ forward traversal
- 🛑 null boundary control
- 🧩 empty & single handling
- 🧵 lockstep traversal
- 📏 length/tail scan

## Things to master
- ✅ never do curr.next unless you proved curr exists
- ✅ you can draw pointer positions quickly

---

## 1.1 ➡️ Basic traversal

**Why:** Every linked list algorithm starts with “walk nodes safely.”

**What:** curr=head; while curr: process; curr=curr.next.

**How (step/flow):**
1) curr=head
2) while curr != null:
3) process curr
4) curr=curr.next

**Where:** searching, counting, converting list to array.

**When:** read-only logic.

**Visual**
```text
head → [A] → [B] → [C] → null
         ^
        curr moves →
```

**Pitfalls**
- ❌ accessing curr.next.val without checking curr.next

**Tips**
- 💡 If cycle is possible, switch to Level 3 fast/slow.

**Practice (LeetCode)**
- 1290 Convert Binary Number in a Linked List to Integer
- 1721 Swapping Nodes in a Linked List

---

## 1.2 🛑 Boundary control (null guards)

**Why:** Null is your out-of-bounds.

**What:** Choose guard by access depth.

**How (step/flow):**
- Need only curr? use `while curr`
- Need curr.next? use `while curr and curr.next`

**Where:** dedup, pairwise operations.

**When:** you touch neighbors.

**Visual**
```text
Safe patterns:
while curr:
  use curr ✅

while curr and curr.next:
  use curr.next ✅
```

**Practice (LeetCode)**
- 83 Remove Duplicates from Sorted List
- 328 Odd Even Linked List

---

## 1.3 🧩 Empty & single

**Why:** Many pointer patterns assume at least 2 nodes.

**What:** empty: head==null, single: head.next==null.

**How:** guard early if you need neighbors.

**Visual**
```text
Empty:  head -> null
Single: head -> [X] -> null
```

**Practice (LeetCode)**
- 206 Reverse Linked List (handles empty/single)
- 21 Merge Two Sorted Lists (handles empty inputs)

---

## 1.4 🧵 Lockstep traversal

**Why:** Many tasks compare or align two lists.

**What:** move p and q together.

**How:** while p and q: compare; p=p.next; q=q.next.

**Where:** compare lists, intersection after alignment, compare halves.

**Practice (LeetCode)**
- 160 Intersection of Two Linked Lists
- 234 Palindrome Linked List (compare phase)

---

## 1.5 📏 Length + tail

**Why:** rotate and split operations need length and/or tail.

**What:** one pass to compute both.

**How:** count++, tail=curr, curr=curr.next.

**Pitfalls**
- ❌ hangs if cycle exists

**Practice (LeetCode)**
- 61 Rotate List
- 19 Remove Nth Node From End of List (length variant)

---

# 🔵 Level 2: The Structural Layer (Local Rewiring)
**Goal:** safe insertion/deletion with minimal state.

## Topics to know
- ➕ insertion at head
- ➖ deletion with prev
- 🧷 save-next-before-rewire rule
- 🧸 dummy/sentinel node

## Things to master
- ✅ your first move before rewiring is: save nxt
- ✅ dummy node reflex when head might change

---

## 2.1 ➕ Insert at head

**Why:** simplest pointer change.

**What:** new.next=head; head=new.

**How:** do it in that order.

**Practice (LeetCode)**
- 707 Design Linked List (insert head)
- 147 Insertion Sort List (inserting nodes into a sorted prefix)

---

## 2.2 ➖ Delete using prev (and why dummy helps)

**Why:** singly list can’t go backward.

**What:** prev.next = curr.next deletes curr.

**How (step/flow):**
1) prev points to node before curr
2) rewire prev.next
3) move curr using a saved next reference

**Visual**
```text
prev → [P] → [C] → [N]
Delete C:
prev.next = C.next
prev → [P] → [N]
```

**Practice (LeetCode)**
- 203 Remove Linked List Elements
- 237 Delete Node in a Linked List

---

## 2.3 🧷 Save next before rewiring

**Why:** after you change curr.next, you might lose the remainder.

**What:** nxt = curr.next comes first.

**How:** save → rewire → advance.

**Practice (LeetCode)**
- 206 Reverse Linked List
- 24 Swap Nodes in Pairs

---

## 2.4 🧸 Dummy / sentinel node

**Why:** head deletion becomes normal-case logic.

**What:** dummy.next=head; prev=dummy.

**Where:** remove nth from end, remove duplicates II.

**Practice (LeetCode)**
- 19 Remove Nth Node From End of List
- 82 Remove Duplicates from Sorted List II

---

# 🟠 Level 3: The Multi-View Layer (Two Pointers + Stitching)
**Goal:** fast/slow, fixed-gap, merge, reverse.

## Topics to know
- 🐢🐇 fast/slow pointers
- 🎯 fixed-gap pointers
- 🤝 merge stitching
- 🔁 reverse (prev/curr/nxt)

## Things to master
- ✅ correct guard: while fast and fast.next
- ✅ you can explain why cycle => meet (not just memorize)

### Reference note
cp-algorithms describes the tortoise-and-hare cycle detection steps: slow moves 1, fast moves 2, meet implies a cycle; fast reaching null implies no cycle. [web:84]

---

## 3.1 🐢🐇 Fast/slow pointers

**Why:** detect cycles and find middle without extra memory.

**What:** slow=1 step, fast=2 steps.

**How:** while fast and fast.next: slow=slow.next, fast=fast.next.next.

**Visual**
```text
slow: A → B → C → D
fast: A → C → E → ...
fast moves 2 nodes per loop
```

**Practice (LeetCode)**
- 876 Middle of the Linked List
- 141 Linked List Cycle

---

## 3.2 🔁 Find cycle start (advanced)

**Why:** after meeting, reset one pointer to head; move both 1 step; meet at entry. [web:84]

**Practice (LeetCode)**
- 142 Linked List Cycle II
- 287 Find the Duplicate Number (array-as-linked-list)

---

## 3.3 🎯 Fixed-gap pointers (nth from end)

**Why:** one pass without computing length.

**What:** keep fast n nodes ahead.

**How:** advance fast n; then move both until fast at last.

**Practice (LeetCode)**
- 19 Remove Nth Node From End of List
- 61 Rotate List (gap & reconnection reasoning)

---

## 3.4 🤝 Merge two sorted lists

**Why:** core pointer-stitching primitive.

**What:** tail always points to last node of output.

**How:** attach smaller head, advance that list, advance tail.

**Visual**
```text
A: 1 → 3 → 7
B: 2 → 4 → 5
out: dummy → 1 → 2 → 3 → 4 → 5 → 7
              ^
             tail
```

**Practice (LeetCode)**
- 21 Merge Two Sorted Lists
- 23 Merge k Sorted Lists

---

## 3.5 🔁 Reverse list (prev/curr/nxt)

**Why:** most common transformation.

**What:** prev is reversed prefix.

**How (step/flow):**
1) nxt=curr.next
2) curr.next=prev
3) prev=curr
4) curr=nxt

**Visual**
```text
prev   curr   nxt
null ← [A] → [B] → [C] → null
```

**Practice (LeetCode)**
- 206 Reverse Linked List
- 92 Reverse Linked List II (core reversal inside)

---

# 🟣 Level 4: The Range Layer (Sublist Operations)
**Goal:** manipulate segments safely and terminate tails.

## Topics to know
- 🔄 reverse sublist [m..n]
- 🔁 reverse k-group
- 🧺 stable partition (two chains)
- 🧽 dedup patterns
- 🔀 intersection/stitch

## Things to master
- ✅ anchor pointers: beforeRange, rangeTail, afterRange
- ✅ always null-terminate final tail

---

## 4.1 🔄 Reverse Linked List II (range)

**Why:** teaches anchors + local reversal.

**Visual anchors**
```text
before → [m] → ... → [n] → after
   |      \____reverse____/
 reconnect: before.next = newHead, oldHead.next = after
```

**Practice (LeetCode)**
- 92 Reverse Linked List II
- 25 Reverse Nodes in k-Group

---

## 4.2 🧺 Stable partition (two chains)

**Why:** stable grouping is easier by building two lists then connecting.

**Visual**
```text
lowDummy → ...lowTail
highDummy → ...highTail
Connect: lowTail.next = highDummy.next
Terminate: highTail.next = null
```

**Practice (LeetCode)**
- 86 Partition List
- 328 Odd Even Linked List

---

## 4.3 🧽 Dedup on sorted list

**Why:** duplicates form runs; you either skip extras (83) or remove all duplicates (82).

**Practice (LeetCode)**
- 83 Remove Duplicates from Sorted List
- 82 Remove Duplicates from Sorted List II

---

## 4.4 🔀 Intersection / stitching

**Why:** trains identity vs value thinking.

**Practice (LeetCode)**
- 160 Intersection of Two Linked Lists
- 1669 Merge In Between Linked Lists

---

# 🔴 Level 5: The Abstract Layer (Composition)
**Goal:** solve “multi-stage” problems reliably.

## Topics to know
- 🪞 palindrome (split + reverse + compare)
- 🧵 reorder (split + reverse + weave)
- 🧮 sort list (merge sort)
- ➕ digit/carry pipelines
- 🧬 clone pointer structures

## Things to master
- ✅ stage invariants: after each stage list is still well-formed
- ✅ you can prove “no nodes lost”

**Practice (LeetCode)**
- 234 Palindrome Linked List, 143 Reorder List
- 148 Sort List, 2 Add Two Numbers
- 138 Copy List with Random Pointer, 430 Flatten a Multilevel Doubly Linked List

---

# 🧰 Traversal mastery add-ons (linked lists)

## A) 🧪 Micro-tracing (interview superpower)
Draw nodes and write pointer names under nodes:
```text
[A]→[B]→[C]→null
 ^
 curr
```
After each line of code, update the drawing.

## B) 🧠 Invariant library
- Reverse: “prev is reversed prefix; curr is next to reverse.”
- Merge: “dummy.next..tail is sorted and final.”
- Dummy delete: “prev always points to node before candidate.”
- Cycle: “fast=2x slow; meet implies a cycle.” [web:84]

## C) ✅ Safety checklist
- Did I save nxt before rewiring?
- If head can change, did I use dummy?
- Did I terminate the final tail with null?
- Am I comparing node identity (same node) vs node value?

# 🎛️ Visual Playbook — Linked Lists (Diagrams Only)
**Purpose:** Clean ASCII diagrams to teach traversal + pointer rewiring.

---

## Legend
- `head`, `curr`, `prev`, `nxt`, `slow`, `fast`, `tail` are pointers
- `null` means end of list
- Boxes are nodes; arrows are `.next`

---

# 🟢 Level 1 — Physical Layer (Pointer Movement)

## 1) Basic traversal
```text
head → [A] → [B] → [C] → null
         ^
        curr
Move: curr = curr.next
```

## 2) Null boundary guards
```text
Safe for using curr:
while curr:
  use curr.val

Safe for using curr.next:
while curr and curr.next:
  use curr.next.val
```

## 3) Empty & single
```text
Empty:
head → null

Single:
head → [X] → null
```

## 4) Lockstep traversal (two lists)
```text
p → [A1] → [A2] → [A3] → null
q → [B1] → [B2] → null
     ^       ^
   lockstep
Invariant: all compared pairs so far are correct
```

## 5) Length + tail scan
```text
head → [1] → [2] → [3] → null
count++ as you move
Tail is last node seen before null
```

---

# 🔵 Level 2 — Structural Layer (Local Rewiring)

## 6) Dummy / sentinel node
```text
(dummy) → head → [A] → [B] → null
   ^
 prev starts here

Why: deleting head becomes same as deleting middle
```

## 7) Delete a node using prev/curr
```text
prev → [P] → [C] → [N] → ...
Delete C:
prev.next = C.next

Result:
prev → [P] → [N] → ...

Invariant: list remains connected; remainder reachable
```

## 8) Save-next-before-rewire rule
```text
curr → [C] → [N] → ...
Step1: nxt = curr.next   (save N)
Step2: rewire curr.next
Step3: curr = nxt        (continue)

Invariant: remainder never lost
```

---

# 🟠 Level 3 — Multi-View Layer (Two Pointers + Stitching)

## 9) Reverse list (prev/curr/nxt)
```text
Start:
prev=null
curr=head

null ← [A] → [B] → [C] → null
       ^
      curr

One iteration:
nxt = curr.next    (B)
curr.next = prev   (A.next=null)
prev = curr        (prev=A)
curr = nxt         (curr=B)

Invariant: prev is reversed prefix
```

## 10) Merge two sorted lists (tail builder)
```text
A: 1 → 3 → 7 → null
B: 2 → 4 → 5 → null

(dummy) → null
   ^
  tail

Attach smaller head each step:
(dummy) → 1 → 2 → 3 → 4 → 5 → 7
                          ^
                         tail

Invariant: dummy.next..tail is sorted and final
```

## 11) Fast/slow pointers (middle)
```text
A → B → C → D → E → null
^         ^
slow      fast

slow: 1 step
fast: 2 steps

When fast reaches end, slow is at middle
```

## 12) Cycle detection (tortoise/hare)
```text
A → B → C → D → E
        ↑       ↓
        H ← G ← F

slow moves 1
fast moves 2
If cycle exists, they meet inside the cycle
```

## 13) Fixed-gap pointers (nth from end)
```text
(dummy) → 1 → 2 → 3 → 4 → 5 → null
fast ahead by n nodes
slow starts at dummy

Move both until fast is last node
Then slow.next is the node to remove

Invariant: gap between fast and slow is constant
```

---

# 🟣 Level 4 — Range Layer (Sublist Operations)

## 14) Reverse sublist [m..n] (anchors)
```text
before → [m] → ... → [n] → after
   |      \____reverse____/

Reconnect:
before.next = newHead
oldHead.next = after

Invariant: before and after remain reachable; segment is reversed
```

## 15) Reverse k-group (chunk anchors)
```text
... → [a]→[b]→[c]→[d]→[e]→[f] → ...
      \___k=3___/

Reverse each full chunk, keep remainder
Invariant: processed chunks are final and connected
```

## 16) Stable partition (two chains)
```text
Original:
head → ... → null

Build:
lowDummy  → ... lowTail
highDummy → ... highTail

Connect:
lowTail.next = highDummy.next
highTail.next = null

Invariant: both chains preserve order; final list terminates
```

## 17) Intersection (alignment)
```text
A: a1 → a2 → c1 → c2 → null
B: b1 → c1 → c2 → null

Align by length difference:
Advance longer list so remaining lengths match

Then walk in lockstep until node identity matches
Invariant: remaining distance to tail is equal
```

---

# 🔴 Level 5 — Abstract Layer (Composition)

## 18) Palindrome (split + reverse + compare)
```text
1 → 2 → 3 → 2 → 1
Find middle (fast/slow)
Reverse second half
Compare halves in lockstep
(Optional) restore list

Invariant: each stage returns a well-formed list
```

## 19) Reorder list (split + reverse + weave)
```text
1 → 2 → 3 → 4 → 5
Split: 1→2→3 and 4→5
Reverse second: 5→4
Weave: 1→5→2→4→3

Invariant: weave never loses remainder; last.next=null
```

## 20) Merge sort list (split + merge)
```text
Split by middle:
head → ...
Left half + Right half

Sort each half (recursion)
Merge using merge-stitching primitive

Invariant: each recursion returns sorted list; merge preserves sortedness
```

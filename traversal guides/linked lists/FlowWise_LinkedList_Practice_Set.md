# 🧪 Flow-Wise Linked List Mastery — Practice Set (25 Problems)
**Format:** Problem → subtopic → timebox → expected invariant

---

# 🟢 Level 1 — Pointer Movement
1) Print list (iterate)
- Subtopic: basic traversal
- ⏱️ 5–10 min
- 🧾 Invariant: curr always points to next unvisited node.

2) Search value in list
- Subtopic: traversal + null guard
- ⏱️ 10–15 min
- 🧾 Invariant: all nodes before curr have been checked.

3) Length of list
- Subtopic: counting
- ⏱️ 10 min
- 🧾 Invariant: count equals number of visited nodes.

4) Find tail node
- Subtopic: boundary control
- ⏱️ 10 min
- 🧾 Invariant: tail is last node with next == null (acyclic assumption).

5) Compare two lists for equality
- Subtopic: lockstep
- ⏱️ 15–20 min
- 🧾 Invariant: nodes processed so far are equal pairwise.

---

# 🔵 Level 2 — Local Rewiring
6) Insert at head
- Subtopic: head insertion
- ⏱️ 10 min
- 🧾 Invariant: new node points to old head.

7) Delete first occurrence of value (with dummy)
- Subtopic: dummy/sentinel
- ⏱️ 20–30 min
- 🧾 Invariant: prev.next always points to current candidate node.

8) Remove all occurrences of value
- Subtopic: deletion + prev tracking
- ⏱️ 25–35 min
- 🧾 Invariant: list built so far contains no removed values.

9) Delete node given only node reference (classic)
- Subtopic: local overwrite trick
- ⏱️ 15–20 min
- 🧾 Invariant: current node takes value of next and skips next.

---

# 🟠 Level 3 — Two Pointers
10) Find middle of list
- Subtopic: fast/slow
- ⏱️ 15–20 min
- 🧾 Invariant: fast moves 2x speed of slow.

11) Detect cycle (Linked List Cycle)
- Subtopic: tortoise & hare
- ⏱️ 20–30 min
- 🧾 Invariant: if cycle exists, pointers meet; else fast reaches null.

12) Find cycle start (Linked List Cycle II)
- Subtopic: cycle meeting + reset trick
- ⏱️ 45–60 min
- 🧾 Invariant: moving pointers at same speed from head and meet point converges at entry.

13) Remove Nth from end
- Subtopic: fixed-gap pointers + dummy
- ⏱️ 30–45 min
- 🧾 Invariant: gap between fast and slow remains n.

14) Merge two sorted lists
- Subtopic: merge stitching
- ⏱️ 25–35 min
- 🧾 Invariant: dummy.next..tail is sorted and final.

15) Reverse linked list
- Subtopic: prev/curr/next
- ⏱️ 25–35 min
- 🧾 Invariant: prev is the reversed prefix; curr is next node to reverse.

---

# 🟣 Level 4 — Sublist Operations
16) Reverse between m and n
- Subtopic: sublist reversal
- ⏱️ 45–70 min
- 🧾 Invariant: beforeRange and rangeTail remain connected correctly.

17) Partition list around x (stable)
- Subtopic: two-chain build
- ⏱️ 35–50 min
- 🧾 Invariant: good and bad chains preserve original order.

18) Remove duplicates from sorted list
- Subtopic: skip duplicates
- ⏱️ 20–30 min
- 🧾 Invariant: curr points to last unique node kept.

19) Remove duplicates II (remove all duplicates)
- Subtopic: dummy + skip runs
- ⏱️ 45–70 min
- 🧾 Invariant: output list contains only values with frequency 1.

20) Intersection of two linked lists
- Subtopic: alignment
- ⏱️ 35–55 min
- 🧾 Invariant: after aligning, both pointers have equal remaining distance to tail.

---

# 🔴 Level 5 — Advanced Reasoning
21) Palindrome linked list
- Subtopic: find middle + reverse half
- ⏱️ 45–75 min
- 🧾 Invariant: second half reversed; lockstep compare checks equality.

22) Reorder list
- Subtopic: split + reverse + merge weave
- ⏱️ 60–90 min
- 🧾 Invariant: weave preserves nodes and terminates properly.

23) Sort list (merge sort)
- Subtopic: split by mid + merge
- ⏱️ 75–120 min
- 🧾 Invariant: each recursion returns sorted list; merge maintains sortedness.

24) Add two numbers (list digits)
- Subtopic: traversal + carry
- ⏱️ 45–70 min
- 🧾 Invariant: carry is correct after each digit addition.

25) Copy list with random pointer (if applicable)
- Subtopic: mapping / interleaving trick
- ⏱️ 90–120 min
- 🧾 Invariant: each original node maps to exactly one clone.

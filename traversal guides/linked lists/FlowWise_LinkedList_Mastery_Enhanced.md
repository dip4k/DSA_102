# 🔗 Flow-Wise Linked List Mastery Curriculum (Level-wise)
**For:** Developers building interview-grade linked-list intuition  
**Style:** Why → What → How → Where → When + visuals, pitfalls, tips, and Python/C# snippets

---

## 🧠 The core mindset (Complexity of State)
Linked lists aren’t hard because of logic—they’re hard because you’re managing **references**.
- Arrays: your “cursor” is an index (cheap random access).
- Linked lists: your “cursor” is a pointer, and moving is only possible by following `.next` (or `.prev`).

Your job is to keep the **invariants** correct while rewiring pointers.

---

## 🧱 Baseline node definitions (use in snippets)

### 🐍 Python
```python
class ListNode:
    def __init__(self, val=0, next=None):
        self.val = val
        self.next = next
```

### 🟦 C#
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
**Goal:** Muscle memory for moving through nodes safely.

## Topics
- Basic traversal (forward)
- Boundary control (null termination)
- Edge cases (empty & single)
- Lockstep traversal (two lists)
- Counting length + finding tail

---

## 1.1 ➡️ Basic traversal (forward)

**Why:** Almost every linked-list task begins as “walk through nodes and observe state.”

**What:** Start from `head`, repeatedly follow `next` until you hit `None/null`.

**How (step/flow):**
1) Set `curr = head`
2) While curr exists: process curr
3) Move: `curr = curr.next`

**Where:** printing, searching, counting, verifying sortedness, building arrays from lists.

**When:** default pattern whenever you don’t need rewiring.

**Visual**
```text
head → [A] → [B] → [C] → null
         ^
        curr moves →
```

**Python snippet**
```python
curr = head
while curr:
    # use curr.val
    curr = curr.next
```

**C# snippet**
```csharp
ListNode curr = head;
while (curr != null)
{
    // use curr.val
    curr = curr.next;
}
```

**Common pitfalls**
- ❌ Using `curr.next` before confirming `curr` is not null
- ❌ Infinite loop when the list has a cycle (you need Level 3 tools)

**Tips & tricks**
- 💡 If you might have a cycle, switch to fast/slow detection (Level 3). [cp-algorithms describes the tortoise/hare approach for cycle detection.]

---

## 1.2 🛑 Boundary control (null termination)

**Why:** In arrays, bounds are numeric; in linked lists, bounds are **null checks**.

**What:** Two common “safe loop guards”:
- `while curr:` (safe to read `curr.val`)
- `while curr and curr.next:` (safe to read `curr.next.val`)

**How (step/flow):**
1) Choose the guard based on what you access
2) Never access `curr.next` unless the guard guarantees it

**Where:** pairwise comparisons, deleting `curr.next`, fast/slow loops.

**When:** any time you touch a neighbor pointer.

**Visual**
```text
while curr:               while curr and curr.next:
  use curr ✅               use curr.next ✅
  curr = curr.next         curr = curr.next
```

**Pitfalls**
- ❌ `while curr.next:` when `curr` might be null (crash)

**Tip**
- 💡 Pick the guard that matches your deepest access.

---

## 1.3 🧩 Edge cases (empty & single)

### Empty list
**Why:** Many pointer algorithms assume at least 1 node.

**What:** `head == None/null`.

**How:** Guard early.

**Python**
```python
if not head:
    return head
```

**C#**
```csharp
if (head == null) return head;
```

### Single node
**Why:** Neighbor access breaks (no `head.next`).

**What:** `head != null` and `head.next == null`.

**How:** Guard if you access two nodes.

**Pitfalls**
- ❌ `head.next.next` without checking `head.next`

**Tip**
- 💡 Treat (empty, single) as mandatory tests in every linked list problem.

---

## 1.4 🧵 Lockstep traversal (two lists)

**Why:** Some tasks compare or merge aligned nodes.

**What:** Move two pointers together: `a = a.next`, `b = b.next`.

**How (step/flow):**
1) Initialize `p = head1`, `q = head2`
2) While both exist: process pair
3) Stop when either hits null

**Where:** equality check, lexicographic compare, building zipped output.

**When:** the lists are aligned by position.

**Visual**
```text
p → [A1] → [A2] → [A3] → null
q → [B1] → [B2] → null
     ^       ^
    lockstep until q ends
```

**Pitfalls**
- ❌ assuming equal lengths without checking

**Tip**
- 💡 Decide policy: stop at min length, or require equal length.

---

## 1.5 📏 Counting length + finding tail

**Why:** Many operations need length (kth from end, split) or tail (append, cycle link).

**What:** One pass count; tail is the last node where `node.next == null`.

**How (step/flow):**
1) `count=0`, `curr=head`
2) count++, move next
3) tail is last non-null node visited

**Where:** split in half, allocate arrays, validate structure.

**When:** you can afford O(n) extra scan.

**Pitfall**
- ❌ tail-finding loops can hang if list has a cycle

---

# 🔵 Level 2: The Structural Layer (Local Rewiring)
**Goal:** Learn safe pointer rewiring for insertion/deletion with minimal state.

## Topics
- Insertion at head
- Deletion with `prev` tracking
- The “save next before rewiring” rule
- Dummy/sentinel node technique

---

## 2.1 ➕ Insertion at head

**Why:** Head insertion is the simplest pointer rewrite and builds confidence.

**What:** New node points to old head, then becomes the head.

**How (step/flow):**
1) `new.next = head`
2) `head = new`

**Where:** stack-like usage, building list from stream.

**When:** you want O(1) insertion.

**Visual**
```text
Before: head → [A] → [B]
Insert X:
X.next = head
head = X
After:  head → [X] → [A] → [B]
```

**Python**
```python
new = ListNode(x)
new.next = head
head = new
```

**Pitfalls**
- ❌ losing reference to old head by overwriting head too early (solve by ordering)

---

## 2.2 ➖ Deletion needs `prev` (tracking the node before)

**Why:** In singly linked lists you can’t move backward, so deletions typically require `prev`.

**What:** To delete `curr`, set `prev.next = curr.next`.

**How (step/flow):**
1) Keep `prev` and `curr`
2) When `curr` is target, rewire `prev.next`
3) Move carefully after deletion

**Where:** remove value, remove duplicates, filter list.

**When:** deleting not-at-head nodes.

**Visual**
```text
prev → [P] → [C] → [N]
Delete C:
prev.next = C.next
Result:
prev → [P] → [N]
```

**Pitfalls**
- ❌ deleting head without special handling

---

## 2.3 🧷 The golden rule: save `next` before rewiring

**Why:** Once you change `curr.next`, you might lose the rest of the list.

**What:** Always store `next = curr.next` before modifying pointers.

**How (flow):**
1) `next = curr.next`
2) rewire `curr.next = something`
3) move `curr = next`

**Where:** reversing, partitioning, re-linking nodes.

**When:** any pointer-changing algorithm.

**Pitfalls**
- ❌ rewire first, then you can’t reach the remainder

**Tip**
- 💡 In interviews, literally say: “I will save next before rewiring.”

---

## 2.4 🧸 Dummy / sentinel node (edge-case eliminator)

**Why:** Removing or inserting at the head becomes just another “normal case” if you add a dummy node.

**What:** Create a node before head: `dummy.next = head`, return `dummy.next` at end.

**How (step/flow):**
1) Create `dummy = Node(0)` and set `dummy.next = head`
2) Start `prev = dummy`
3) Perform operations uniformly
4) Return `dummy.next`

**Where:** remove nth from end, remove value, merge operations.

**When:** head might be deleted/changed.

**Industry-proven note:** The one-pass “remove Nth from end” uses a sentinel/dummy to handle removing the head cleanly. [web:89][web:95]

**Visual**
```text
(dummy) → head → [A] → [B] → null
   ^
 prev starts here so deleting head is easy
```

**C# snippet**
```csharp
ListNode dummy = new ListNode(0, head);
ListNode prev = dummy;
// operate...
return dummy.next;
```

**Pitfalls**
- ❌ forgetting to return dummy.next

**Tips**
- 💡 If a problem says “may remove head”, dummy node is your first thought.

---

# 🟠 Level 3: The Multi-View Layer (Two Pointers)
**Goal:** Manage multiple pointers and invariants.

## Topics
- Fast/slow pointers (middle, cycle)
- Fixed-gap pointers (nth from end)
- Merge two sorted lists
- Reverse list (3-pointer state machine)

---

## 3.1 🐢🐇 Fast/Slow pointers (tortoise & hare)

**Why:** Lets you detect cycles and find midpoints without extra memory.

**What:** Two pointers:
- slow moves 1 step
- fast moves 2 steps

**How (step/flow):**
1) slow = head, fast = head
2) while fast and fast.next:
3) slow = slow.next
4) fast = fast.next.next

**Where:**
- Detect if a cycle exists
- Find middle node

**When:** you need “relative speed” behavior.

**Cycle detection fact:** If the list has a cycle, fast and slow will eventually meet; if fast reaches null, there is no cycle. [web:84][web:85]

**Visual (cycle case)**
```text
A → B → C → D → E
        ↑       ↓
        H ← G ← F
slow: 1 step
fast: 2 steps
Eventually they meet inside the loop
```

**Python (hasCycle)**
```python
def has_cycle(head):
    slow = fast = head
    while fast and fast.next:
        slow = slow.next
        fast = fast.next.next
        if slow is fast:
            return True
    return False
```

**Pitfalls**
- ❌ using `while fast.next and fast.next.next` without checking fast itself

**Tips**
- 💡 Memorize guard: `while fast and fast.next:`

---

## 3.2 🎯 Fixed-gap pointers (nth from end)

**Why:** One pass, no length computation.

**What:** Keep two pointers `fast` and `slow` with a constant gap of n nodes.

**How (step/flow):**
1) Add dummy before head
2) Move fast n steps
3) Move both until fast reaches end
4) slow is just before target; delete slow.next

**Where:** Remove Nth node from end, find kth from end.

**When:** “from the end” appears.

**Dummy node note:** Using a sentinel/dummy simplifies the edge case of deleting the head. [web:89][web:95]

**Visual**
```text
(dummy) → 1 → 2 → 3 → 4 → 5 → null
fast moved n steps ahead
slow follows so slow.next is the target when fast ends
```

**Python (remove nth from end)**
```python
def remove_nth_from_end(head, n):
    dummy = ListNode(0, head)
    fast = dummy
    slow = dummy

    for _ in range(n):
        fast = fast.next

    while fast.next:
        fast = fast.next
        slow = slow.next

    slow.next = slow.next.next
    return dummy.next
```

**Pitfalls**
- ❌ moving fast n+1 vs n steps inconsistently with your while condition

**Tip**
- 💡 Pick one variant and keep it consistent: “fast stops at last node” vs “fast becomes null”.

---

## 3.3 🤝 Merge two sorted lists

**Why:** Core interview primitive; also builds “pointer stitching” skill.

**What:** Choose smaller head repeatedly; advance that list.

**How (step/flow):**
1) dummy, tail = dummy
2) while a and b:
3) tail.next = smaller; advance smaller
4) tail = tail.next
5) attach remainder

**Where:** merge sort on lists, combining streams.

**When:** inputs are sorted.

**Visual**
```text
A: 1 → 3 → 7
B: 2 → 4 → 5
Result: 1 → 2 → 3 → 4 → 5 → 7
 tail always points to last node of result
```

**C#**
```csharp
ListNode MergeTwoLists(ListNode a, ListNode b)
{
    ListNode dummy = new ListNode(0);
    ListNode tail = dummy;

    while (a != null && b != null)
    {
        if (a.val <= b.val)
        {
            tail.next = a;
            a = a.next;
        }
        else
        {
            tail.next = b;
            b = b.next;
        }
        tail = tail.next;
    }

    tail.next = (a != null) ? a : b;
    return dummy.next;
}
```

**Pitfalls**
- ❌ forgetting `tail = tail.next`
- ❌ creating new nodes unnecessarily (unless required)

**Tips**
- 💡 The invariant: `dummy.next..tail` is always sorted and final.

---

## 3.4 🔁 Reverse a linked list (3-pointer state machine)

**Why:** The single most common linked-list transformation.

**What:** Rewire pointers so each node points to its previous.

**How (step/flow):**
1) prev = null, curr = head
2) save next = curr.next
3) curr.next = prev
4) prev = curr
5) curr = next
6) return prev (new head)

**This exact 3-pointer iterative method is a standard explanation for O(n) time, O(1) space reversal.** [web:90][web:93]

**Visual**
```text
prev   curr   next
null ← [A] → [B] → [C] → null
Step:
next = B
A.next = prev (null)
prev = A
curr = B
```

**Python**
```python
def reverse_list(head):
    prev = None
    curr = head
    while curr:
        nxt = curr.next
        curr.next = prev
        prev = curr
        curr = nxt
    return prev
```

**Pitfalls**
- ❌ forgetting to save nxt before rewiring
- ❌ returning head instead of prev

**Tips**
- 💡 If you can trace one iteration, you can reverse any list.

---

# 🟣 Level 4: The Range Layer (Sublist Operations)
**Goal:** Operate on sublists (ranges) and “windows” in list form.

## Topics
- Reverse sublist [m..n]
- Partition list by predicate
- Remove duplicates (sorted list)
- Detect intersection (two-list alignment)

---

## 4.1 🔄 Reverse a sublist [m..n]

**Why:** Realistic pointer surgery: isolate a region, reverse it, reconnect.

**What:** Reverse only a contiguous portion.

**How (step/flow):**
1) Use dummy to simplify head changes
2) Move `prev` to node before m
3) Reverse the next (n-m+1) nodes
4) Reconnect: prev.next to new subhead, old subhead to remainder

**Where:** “reverse between”, segment transformations.

**When:** the problem specifies a range.

**Pitfalls**
- ❌ losing the connection to the remainder after n

**Tip**
- 💡 Always store two anchors: `beforeRange` and `rangeTail`.

---

## 4.2 🧺 Partition list (stable predicate split)

**Why:** You often need to group nodes while preserving order.

**What:** Build two lists: `good` and `bad`, then connect.

**How (step/flow):**
1) Create two dummy heads: goodDummy, badDummy
2) Walk nodes, append to the appropriate tail
3) goodTail.next = badDummy.next
4) badTail.next = null (important!)

**Where:** partition by value, stable grouping.

**When:** order must be preserved.

**Pitfalls**
- ❌ forgetting to null-terminate bad tail (can create cycles)

**Tip**
- 💡 Stable partition is easier by building two chains, not by swapping.

---

## 4.3 🧽 Remove duplicates (sorted list)

**Why:** Similar to array compaction, but rewiring instead of overwriting.

**What:** Skip nodes with same value.

**How (step/flow):**
1) curr = head
2) while curr and curr.next:
3) if curr.val == curr.next.val: curr.next = curr.next.next
4) else curr = curr.next

**Where:** dedup in sorted data.

**When:** list is sorted.

**Pitfalls**
- ❌ moving curr after deletion (might skip more duplicates)

**Tip**
- 💡 If you delete `curr.next`, keep curr where it is.

---

## 4.4 🔀 Intersection of two lists (alignment trick)

**Why:** Tests pointer reasoning; common interview question.

**What:** Align by length difference, then walk together.

**How (step/flow):**
1) Compute lenA, lenB
2) Advance longer list by |lenA-lenB|
3) Walk both until they match or end

**Where:** find intersection node.

**When:** two lists may share tail nodes.

**Pitfalls**
- ❌ comparing node values instead of node references

---

# 🔴 Level 5: The Abstract Layer (Invariants & Proof Thinking)
**Goal:** Design solutions by stating invariants first.

## 5.1 ✅ The three invariants you should say out loud
- 🧷 **Connectivity invariant:** every node is reachable from the returned head unless intentionally removed.
- 🧭 **Acyclic invariant (when expected):** last node’s next is null; no accidental cycles.
- 🔒 **Region invariant (when partitioning):** each region’s tail is tracked and correctly terminated.

## 5.2 🧠 Interview-grade pointer safety checklist
- ✅ Did I save `next` before rewiring?
- ✅ If head can change, did I use dummy/sentinel?
- ✅ Am I using the correct while-guard for the deepest access?
- ✅ After I stitch lists, did I terminate the final tail with null?
- ✅ Am I comparing node identity (same node) vs node value?

---

# 🎯 What to learn next (if you want a practice pack)
If you want, I can generate a second file like your array practice pack:
- 20–30 linked list problems mapped to these subtopics
- timeboxes
- expected invariant per problem
- Python-first or C#-first versions

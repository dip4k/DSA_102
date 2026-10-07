# 📘 Week 02 Day 03: Linked Lists — ENGINEERING GUIDE

> 🧭 **Navigation:** [← Previous Day](Week_02_Day_02_Dynamic_Arrays_Amortized_Growth_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_02_Day_04_Stacks_Queues_Deques_Instructional.md)
> 
> 💡 **Instructor Note:** *Not all sections or topics are mandatory. Feel free to adapt your pace and prioritize high-yield patterns based on your personal interview goals.*

---

## 🎯 LEARNING OBJECTIVES

*By the end of this chapter, you will be able to:*

- 🎯 **Internalize** node-based non-contiguous heap layouts and how pointer indirection replaces index arithmetic.
- 🚂 **Conceptualize** pointer mechanics using physical metaphors: train cars with couplers and treasure hunts.
- ⚙️ **Master** the complete operations suite for both Singly and Doubly Linked Lists: `InsertHead`, `InsertTail`, `InsertAfter`, `DeleteHead`, `DeleteTail`, `DeleteByValue`, `DeleteNode`, and `Reverse`.
- 🐛 **Diagnose** and prevent the classic lost-reference bug with visual pointer detachment traces.
- 🛡️ **Deploy** Sentinel Dummy Nodes (`dummy.next = head`) to systematically eliminate null checks and boundary edge cases.
- 🔀 **Distinguish** structural variations: Singly vs. Doubly vs. Circular vs. Sentinel Linked Lists.
- 🏭 **Articulate** the mechanical trade-offs between array cache locality and linked list `O(1)` splicing in a 45-minute technical screen.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Problem: True O(1) Splicing vs. Array Shifting

In Day 1 and Day 2, we saw that arrays deliver lightning-fast `O(1)` index access and optimal cache prefetching. However, inserting or deleting an element at an arbitrary position in an array requires shifting all subsequent elements in `O(N)` time:

```text
Array Insert at index 1:
Index:   0    1    2    3    4
Array: [ 10 | 20 | 30 | 40 | 50 ]
              ▲
              Insert 99 -> Must shift [20, 30, 40, 50] right by 1 slot!
Cost: O(N) memory copies
```

If your application demands continuous insertions and deletions at known locations (such as operating system process task queues, kernel memory free lists, music playlist queues, or LRU cache eviction chains), an array's `O(N)` shifting penalty becomes a fatal bottleneck.

#### The Linked List Paradigm
Instead of packing elements into a contiguous slab, a Linked List allocates independent **Node** objects scattered anywhere across the heap. Each node stores its payload data and a memory address reference (**pointer**) to the adjacent node.

To insert or delete, we **never move data in RAM**. We merely rewire pointer addresses in strictly `O(1)` time.

> 💡 **Core Invariant:** *Every pointer mutation must secure an explicit reference to the downstream sublist before severing an upstream link. Dropping a pointer creates orphaned heap objects and unrecoverable data loss.*

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL & ANATOMY

### Beginner Physical Metaphors

#### 1. The Train Cars with Couplers (Physical Anatomy)
Imagine a long freight train:
- **Head (Locomotive):** The front engine that pulls the entire train.
- **Freight Car (Node):** Each car holds its cargo (`Value` or `Data`).
- **Mechanical Knuckle Coupler (`Next` Pointer):** At the rear of Car 1 is a steel coupler hooked into the front coupler of Car 2.
- **Doubly Linked Coupler (`Prev` & `Next`):** Each car has both a forward coupler connected to the car ahead (`Prev`) and a rear coupler connected to the car behind (`Next`).

```text
Singly Linked Train:
[ Locomotive / Head ]
         │
         ▼
 ┌───────────────┐        ┌───────────────┐        ┌───────────────┐
 │ Cargo: Coal   │ Coupler│ Cargo: Timber │ Coupler│ Cargo: Steel  │ Coupler
 │ Next: Car B   ├───────►│ Next: Car C   ├───────►│ Next: null    ├───────► null
 └───────────────┘        └───────────────┘        └───────────────┘
     Car A (0x10)             Car B (0x40)             Car C (0x88)
```

**How Splicing Works:**
To insert a new "Oil Tanker" car between Car A and Car B:
1. Back the Oil Tanker into place.
2. Hook the Oil Tanker's rear coupler to Car B.
3. Uncouple Car A from Car B and hook Car A's coupler to the Oil Tanker.
4. **Notice:** You did not lift, move, or roll Car B or Car C an inch down the track! The operation was strictly `O(1)`.

#### 2. The GPS Scavenger Hunt
Imagine a treasure hunt where you receive a sealed clue card:
1. Clue #1 gives you a riddle (`Value`) and the GPS coordinates of Clue #2 (`Next Address`).
2. You cannot teleport directly to Clue #5 because you do not know its GPS coordinates until you visit Clues 1, 2, 3, and 4 in order (`O(N)` traversal).
3. If you lose Clue #3 before reading Clue #4's coordinates, the remainder of the hunt is lost forever.

---

### 🖼 Node Structure & Physical Heap Anatomy

On a 64-bit architecture, each node object on the heap requires:
- **Singly Linked Node:** 16-byte object header (type metadata + sync block) + 4-byte payload + 4-byte padding + 8-byte `Next` pointer = **32 bytes total heap memory per 4-byte integer!**
- **Doubly Linked Node:** 16-byte object header + 4-byte payload + 4-byte padding + 8-byte `Prev` pointer + 8-byte `Next` pointer = **40 bytes total heap memory!**

```text
Physical Heap Node Layout (64-bit Architecture):

Singly Linked Node:                       Doubly Linked Node:
┌───────────────────────────┐             ┌───────────────────────────┐
│ Object Header  (16 bytes) │             │ Object Header  (16 bytes) │
├───────────────────────────┤             ├───────────────────────────┤
│ Data / Value   (4 bytes)  │             │ Data / Value   (4 bytes)  │
├───────────────────────────┤             ├───────────────────────────┤
│ Padding/Align  (4 bytes)  │             │ Padding/Align  (4 bytes)  │
├───────────────────────────┤             ├───────────────────────────┤
│ Next Pointer   (8 bytes)  │             │ Prev Pointer   (8 bytes)  │
└───────────────────────────┘             ├───────────────────────────┤
                                          │ Next Pointer   (8 bytes)  │
                                          └───────────────────────────┘
```

#### Non-Contiguous Heap Scatter:
Unlike an array where `arr[1]` sits immediately next to `arr[0]`, linked list nodes are scattered arbitrarily across virtual memory:

```text
Heap Address Space:
0x1040: [ Val: 10 | Next: 0x8820 ] ────────┐ (Hop across 30,000 bytes of RAM)
0x1050: (Unrelated Object)                 │
...                                        ▼
0x8820: [ Val: 20 | Next: 0x3100 ] ────────┐ (Hop across another region)
...                                        ▼
0x3100: [ Val: 30 | Next: null   ] ──► null
```
*Every hop forces the CPU to chase a pointer, triggering an L1/L2 cache miss.*

---

## 🛠️ CHAPTER 3: COMPLETE OPERATIONS SUITE & POINTER TRACES

### 1. `InsertHead(val)` — O(1) Time

Inserts a new node at the very beginning of the list.

```text
Step 0: Initial List: Head -> [ 10 | Next ] -> [ 20 | null ]
        New Node:     newNode = [ 5 | Next: null ]

Step 1: Point newNode.Next to current Head
        newNode = [ 5 | Next ] ──► [ 10 | Next ] ──► [ 20 | null ]
                                         ▲
                                        Head

Step 2: Update Head pointer to newNode
        Head ──► [ 5 | Next ] ──► [ 10 | Next ] ──► [ 20 | null ]
```
- **Code:** `newNode.Next = head; head = newNode;`

---

### 2. `InsertTail(val)` — O(1) with Tail Pointer / O(N) without

```text
Step 0: Initial List: Head -> [ 10 ] -> [ 20 ] -> null
                      Tail ───────► [ 20 ]
        New Node:     newNode = [ 30 | null ]

Step 1: Link current Tail.Next to newNode
        [ 20 | Next ] ──► [ 30 | null ]
              ▲                 ▲
            Tail             newNode

Step 2: Update Tail pointer to newNode
        Tail ───────────► [ 30 | null ]
```
- **Code:** `tail.Next = newNode; tail = newNode;`

---

### 3. `InsertAfter(node, val)` — O(1) Time

Inserts a new node immediately after a given target node.

```text
Target Node: A [ 10 ], Next Node: B [ 20 ]
New Node:    X [ 99 ]

Step 1: newNode.Next = target.Next
        X [ 99 | Next ] ──► B [ 20 ]

Step 2: target.Next = newNode
        A [ 10 | Next ] ──► X [ 99 | Next ] ──► B [ 20 ]
```
- **Order Invariant:** Step 1 MUST precede Step 2! If you do `target.Next = newNode` first, you lose the reference to node B!

---

### 4. `DeleteHead()` — O(1) Time

Removes the head node and returns its value.

```text
Step 0: Head ──► [ 10 | Next ] ──► [ 20 | Next ] ──► null

Step 1: Save target value (10)
Step 2: Advance Head: Head = Head.Next
        [ 10 | Next ]       Head ──► [ 20 | Next ] ──► null
          ▲ (Orphaned)
Step 3: Clear old node's Next pointer to assist GC
```

---

### 5. `DeleteTail()` — O(N) for SLL, O(1) for DLL with Tail

> [!WARNING]
> **The Singly Linked List Tail Trap:** Even if a Singly Linked List maintains a `Tail` pointer, deleting the tail is **`O(N)`**! Why? Because to update `Tail`, you must find the node *immediately before* the tail (`Tail - 1`), which requires walking from `Head` all the way to `N - 2`.  
> Conversely, in a Doubly Linked List, `Tail.Prev` is directly accessible, making `DeleteTail` strictly **`O(1)`**!

```text
Doubly Linked List DeleteTail:
Step 0: [ 10 ] <───> [ 20 ] <───> [ 30 ] (Tail)
Step 1: newTail = Tail.Prev ([ 20 ])
Step 2: newTail.Next = null
Step 3: Tail.Prev = null
Step 4: Tail = newTail
Result: [ 10 ] <───> [ 20 ] ──► null (strictly O(1)!)
```

---

### 6. `DeleteByValue(val)` & `DeleteNode(node)`

- **`DeleteByValue(val)` (SLL):** Traverse list with `prev` and `curr`. When `curr.val == val`, link `prev.Next = curr.Next`. Time: `O(N)`.
- **`DeleteNode(node)` (DLL):** Given direct reference to a node in a DLL:
  ```text
  node.Prev.Next = node.Next;
  node.Next.Prev = node.Prev;
  node.Next = null;
  node.Prev = null;
  ```
  Runs in **strictly `O(1)`** with zero traversal!

---

### 7. 🐛 The Classic Lost-Reference Bug: Visualized

What happens when a developer accidentally executes pointer rewiring out of order?

```text
Buggy Reversal Code:
curr.Next = prev;      // BUG: Overwrote curr.Next prematurely!
next = curr.Next;      // Reads prev (null) instead of downstream list!
```

```text
Initial State:
prev = null, curr = [10]
[ 10 | Next: 0x80 ] ──────► [ 20 | Next: 0x90 ] ──────► [ 30 | null ]

Buggy Step 1: curr.Next = prev (null)
null ◄── [ 10 | Next: null ]       [ 20 | Next: 0x90 ] ──────► [ 30 | null ]
             ▲                          ▲
           curr                    Lost in Heap Memory!
                                   (Zero references pointing to it)
                                   Dangling chain is lost forever!
```

#### The Golden Fix:
Always declare `next = curr.Next` on line 1 of the loop **before** touching `curr.Next`!

---

### 🛡️ Sentinel Dummy Nodes: Eliminating Boundary Edge Cases

Without sentinels, inserting or deleting nodes requires tedious, bug-prone null checks:
```csharp
// Without Sentinel (Messy):
if (head == null) { head = newNode; }
else if (head.Val == target) { head = head.Next; }
else { /* walk with prev and curr */ }
```

#### The Sentinel Head Pattern:
A dummy node whose `.Next` points to the real `head` sits permanently at the boundary:

```text
Singly Linked Sentinel:
┌──────────────┐
│ Dummy (0)    │ Next
│ (Sentinel)   ├──────────► [ Val: 10 ] ──► [ Val: 20 ] ──► null
└──────────────┘                ▲
                              Head
```
With the dummy node, the real `head` is never an exceptional boundary case—it is simply the `Next` of the sentinel node. At the conclusion of operations, the new head is cleanly recovered via `dummy.Next`.

#### Doubly Linked List Sentinels (Head & Tail):
By placing dummy nodes at both ends, `head` and `tail` never change addresses, completely eliminating null pointer dereferences:

```text
Doubly Linked Sentinel Frame:
┌──────────────┐         ┌──────────────┐         ┌──────────────┐
│  dummyHead   │◄───────►│ Node 10      │◄───────►│  dummyTail   │
│  (Sentinel)  │         │ (Real First) │         │  (Sentinel)  │
└──────────────┘         └──────────────┘         └──────────────┘
```

---

## 🔀 CHAPTER 4: VARIATIONS — SINGLY vs. DOUBLY vs. CIRCULAR vs. SENTINEL

```text
1. Singly Linked List:
Head ──► [ 10 | Next ] ──► [ 20 | Next ] ──► null

2. Doubly Linked List:
null ◄── [ Prev | 10 | Next ] ◄───► [ Prev | 20 | Next ] ──► null

3. Circular Singly Linked List:
Head ──► [ 10 | Next ] ──► [ 20 | Next ] ──┐
             ▲                             │
             └─────────────────────────────┘

4. Sentinel Doubly Linked List:
[ dummyHead ] ◄───► [ 10 ] ◄───► [ 20 ] ◄───► [ dummyTail ]
```

### Comprehensive Variation Comparison Matrix

| Dimension | Singly Linked List | Doubly Linked List | Circular Linked List | Sentinel Doubly List |
| :--- | :--- | :--- | :--- | :--- |
| **Pointers per Node**| 1 (`Next`) | 2 (`Prev`, `Next`) | 1 or 2 | 2 (`Prev`, `Next`) |
| **Memory Overhead** | 8 bytes pointer / node | 16 bytes pointers / node| 8 to 16 bytes / node | 16 bytes / node + 2 sentinels |
| **Traverse Direction**| Forward only | Bidirectional | Continuous loop | Bidirectional |
| **Delete Given Node**| `O(N)` (must find prev)| **`O(1)`** (has `node.Prev`) | `O(N)` (SLL) / `O(1)` (DLL)| **`O(1)`** (zero null checks) |
| **Delete Tail** | `O(N)` (even with tail)| **`O(1)`** | `O(N)` (SLL) / `O(1)` (DLL)| **`O(1)`** |
| **Null Pointer Bugs**| Frequent | Frequent | None (infinite loop risk) | **Zero (eliminated by design)** |
| **Best Production Use**| Simple forward streams | LRU Caches, OS Free lists | Round-robin schedulers | Production Deques, B-Tree leaves |

---

## ⚙️ CHAPTER 5: DUAL-LANGUAGE PRODUCTION IMPLEMENTATIONS

### 1. C# (.NET 8/9): Complete Singly & Doubly Linked Lists

```csharp
using System;
using System.Collections;
using System.Collections.Generic;

namespace LinearStructures.Day03;

// 1. Singly Linked List Node
public class SllNode<T>
{
    public T Value { get; set; }
    public SllNode<T>? Next { get; set; }

    public SllNode(T value, SllNode<T>? next = null)
    {
        Value = value;
        Next = next;
    }
}

// 2. Production Singly Linked List with Full Operations Suite
public class SinglyLinkedList<T> : IEnumerable<T>
{
    private SllNode<T>? _head;
    private SllNode<T>? _tail;
    private int _count;

    public int Count => _count;
    public bool IsEmpty => _count == 0;
    public SllNode<T>? Head => _head;
    public SllNode<T>? Tail => _tail;

    // O(1) Prepend
    public void InsertHead(T value)
    {
        SllNode<T> newNode = new(value, _head);
        _head = newNode;
        if (_tail == null) _tail = newNode;
        _count++;
    }

    // O(1) Append
    public void InsertTail(T value)
    {
        SllNode<T> newNode = new(value);
        if (_tail == null)
        {
            _head = newNode;
            _tail = newNode;
        }
        else
        {
            _tail.Next = newNode;
            _tail = newNode;
        }
        _count++;
    }

    // O(1) Insert After Known Node
    public void InsertAfter(SllNode<T> node, T value)
    {
        ArgumentNullException.ThrowIfNull(node);
        SllNode<T> newNode = new(value, node.Next);
        node.Next = newNode;
        if (node == _tail) _tail = newNode;
        _count++;
    }

    // O(1) Delete Head
    public T DeleteHead()
    {
        if (IsEmpty) throw new InvalidOperationException("List is empty.");
        T value = _head!.Value;
        _head = _head.Next;
        if (_head == null) _tail = null;
        _count--;
        return value;
    }

    // O(N) Delete Tail (must traverse to N - 2)
    public T DeleteTail()
    {
        if (IsEmpty) throw new InvalidOperationException("List is empty.");
        if (_head == _tail) return DeleteHead();

        SllNode<T> curr = _head!;
        while (curr.Next != _tail)
        {
            curr = curr.Next!;
        }

        T value = _tail!.Value;
        _tail = curr;
        _tail.Next = null;
        _count--;
        return value;
    }

    // O(N) Delete By Value using Sentinel Dummy Node
    public bool DeleteByValue(T value)
    {
        SllNode<T> dummy = new(default!, _head);
        SllNode<T> prev = dummy;
        SllNode<T>? curr = _head;
        EqualityComparer<T> comparer = EqualityComparer<T>.Default;

        while (curr != null)
        {
            if (comparer.Equals(curr.Value, value))
            {
                prev.Next = curr.Next;
                if (curr == _tail) _tail = prev == dummy ? null : prev;
                _head = dummy.Next;
                _count--;
                return true;
            }
            prev = curr;
            curr = curr.Next;
        }

        return false;
    }

    // O(N) In-Place Iterative Reversal
    public void Reverse()
    {
        SllNode<T>? prev = null;
        SllNode<T>? curr = _head;
        _tail = _head;

        while (curr != null)
        {
            SllNode<T>? next = curr.Next; // 1. Secure downstream pointer
            curr.Next = prev;             // 2. Invert link
            prev = curr;                  // 3. Advance prev
            curr = next;                  // 4. Advance curr
        }

        _head = prev;
    }

    public IEnumerator<T> GetEnumerator()
    {
        SllNode<T>? curr = _head;
        while (curr != null)
        {
            yield return curr.Value;
            curr = curr.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

// 3. Production Doubly Linked List with Sentinel Bounds (LRU Foundation)
public class DllNode<T>
{
    public T Value { get; set; }
    public DllNode<T>? Prev { get; set; }
    public DllNode<T>? Next { get; set; }

    public DllNode(T value)
    {
        Value = value;
    }
}

public class DoublyLinkedList<T> : IEnumerable<T>
{
    private readonly DllNode<T> _dummyHead;
    private readonly DllNode<T> _dummyTail;
    private int _count;

    public int Count => _count;
    public bool IsEmpty => _count == 0;

    public DoublyLinkedList()
    {
        _dummyHead = new DllNode<T>(default!);
        _dummyTail = new DllNode<T>(default!);
        _dummyHead.Next = _dummyTail;
        _dummyTail.Prev = _dummyHead;
        _count = 0;
    }

    // O(1) Prepend
    public DllNode<T> InsertHead(T value)
    {
        DllNode<T> newNode = new(value);
        InsertAfterNode(_dummyHead, newNode);
        return newNode;
    }

    // O(1) Append
    public DllNode<T> InsertTail(T value)
    {
        DllNode<T> newNode = new(value);
        InsertAfterNode(_dummyTail.Prev!, newNode);
        return newNode;
    }

    // O(1) Direct Node Deletion (Zero Traversal Required!)
    public void DeleteNode(DllNode<T> node)
    {
        ArgumentNullException.ThrowIfNull(node);
        node.Prev!.Next = node.Next;
        node.Next!.Prev = node.Prev;
        node.Next = null;
        node.Prev = null;
        _count--;
    }

    public T DeleteHead()
    {
        if (IsEmpty) throw new InvalidOperationException("List is empty.");
        DllNode<T> first = _dummyHead.Next!;
        T val = first.Value;
        DeleteNode(first);
        return val;
    }

    public T DeleteTail()
    {
        if (IsEmpty) throw new InvalidOperationException("List is empty.");
        DllNode<T> last = _dummyTail.Prev!;
        T val = last.Value;
        DeleteNode(last);
        return val;
    }

    private void InsertAfterNode(DllNode<T> target, DllNode<T> newNode)
    {
        newNode.Next = target.Next;
        newNode.Prev = target;
        target.Next!.Prev = newNode;
        target.Next = newNode;
        _count++;
    }

    public IEnumerator<T> GetEnumerator()
    {
        DllNode<T>? curr = _dummyHead.Next;
        while (curr != _dummyTail)
        {
            yield return curr!.Value;
            curr = curr.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
```

---

### 2. Python (3.11+): Complete Singly & Doubly Linked Lists

```python
from typing import Generic, TypeVar, Optional, Iterator

T = TypeVar('T')

class SllNode(Generic[T]):
    def __init__(self, value: T, next_node: Optional['SllNode[T]'] = None) -> None:
        self.value: T = value
        self.next: Optional['SllNode[T]'] = next_node


class SinglyLinkedList(Generic[T]):
    """Production Singly Linked List with full operations suite."""

    def __init__(self) -> None:
        self._head: Optional[SllNode[T]] = None
        self._tail: Optional[SllNode[T]] = None
        self._count: int = 0

    @property
    def count(self) -> int:
        return self._count

    @property
    def is_empty(self) -> bool:
        return self._count == 0

    def insert_head(self, value: T) -> None:
        """O(1) Prepend to head."""
        new_node = SllNode[T](value, self._head)
        self._head = new_node
        if self._tail is None:
            self._tail = new_node
        self._count += 1

    def insert_tail(self, value: T) -> None:
        """O(1) Append to tail."""
        new_node = SllNode[T](value)
        if self._tail is None:
            self._head = new_node
            self._tail = new_node
        else:
            self._tail.next = new_node
            self._tail = new_node
        self._count += 1

    def insert_after(self, node: SllNode[T], value: T) -> None:
        """O(1) Insert immediately downstream of target node."""
        new_node = SllNode[T](value, node.next)
        node.next = new_node
        if node is self._tail:
            self._tail = new_node
        self._count += 1

    def delete_head(self) -> T:
        """O(1) Delete head element."""
        if self.is_empty:
            raise IndexError("delete_head from empty list")
        assert self._head is not None
        val = self._head.value
        self._head = self._head.next
        if self._head is None:
            self._tail = None
        self._count -= 1
        return val

    def delete_tail(self) -> T:
        """O(N) Delete tail element."""
        if self.is_empty:
            raise IndexError("delete_tail from empty list")
        if self._head is self._tail:
            return self.delete_head()

        curr = self._head
        assert curr is not None
        while curr.next is not self._tail:
            curr = curr.next
            assert curr is not None

        assert self._tail is not None
        val = self._tail.value
        self._tail = curr
        self._tail.next = None
        self._count -= 1
        return val

    def reverse(self) -> None:
        """O(N) In-Place Reversal with 3 pointers."""
        prev: Optional[SllNode[T]] = None
        curr: Optional[SllNode[T]] = self._head
        self._tail = self._head

        while curr is not None:
            next_node = curr.next  # 1. Save downstream reference
            curr.next = prev       # 2. Invert pointer
            prev = curr            # 3. Step prev forward
            curr = next_node       # 4. Step curr forward

        self._head = prev

    def __iter__(self) -> Iterator[T]:
        curr = self._head
        while curr is not None:
            yield curr.value
            curr = curr.next

    def __repr__(self) -> str:
        items = [str(x) for x in self]
        return " -> ".join(items) if items else "Empty"


class DllNode(Generic[T]):
    def __init__(self, value: Optional[T] = None) -> None:
        self.value: Optional[T] = value
        self.prev: Optional['DllNode[T]'] = None
        self.next: Optional['DllNode[T]'] = None


class DoublyLinkedList(Generic[T]):
    """Production Doubly Linked List with Sentinel Dummy Nodes."""

    def __init__(self) -> None:
        self._dummy_head: DllNode[T] = DllNode[T]()
        self._dummy_tail: DllNode[T] = DllNode[T]()
        self._dummy_head.next = self._dummy_tail
        self._dummy_tail.prev = self._dummy_head
        self._count: int = 0

    @property
    def count(self) -> int:
        return self._count

    @property
    def is_empty(self) -> bool:
        return self._count == 0

    def insert_head(self, value: T) -> DllNode[T]:
        """O(1) Prepend to head."""
        node = DllNode[T](value)
        self._insert_after_node(self._dummy_head, node)
        return node

    def insert_tail(self, value: T) -> DllNode[T]:
        """O(1) Append to tail."""
        node = DllNode[T](value)
        assert self._dummy_tail.prev is not None
        self._insert_after_node(self._dummy_tail.prev, node)
        return node

    def delete_node(self, node: DllNode[T]) -> None:
        """O(1) Delete node in place with zero list scanning."""
        assert node.prev is not None and node.next is not None
        node.prev.next = node.next
        node.next.prev = node.prev
        node.next = None
        node.prev = None
        self._count -= 1

    def delete_head(self) -> T:
        """O(1) Delete head."""
        if self.is_empty:
            raise IndexError("delete_head from empty list")
        assert self._dummy_head.next is not None
        first = self._dummy_head.next
        val = first.value
        self.delete_node(first)
        return val  # type: ignore[return-value]

    def delete_tail(self) -> T:
        """O(1) Delete tail."""
        if self.is_empty:
            raise IndexError("delete_tail from empty list")
        assert self._dummy_tail.prev is not None
        last = self._dummy_tail.prev
        val = last.value
        self.delete_node(last)
        return val  # type: ignore[return-value]

    def _insert_after_node(self, target: DllNode[T], new_node: DllNode[T]) -> None:
        assert target.next is not None
        new_node.next = target.next
        new_node.prev = target
        target.next.prev = new_node
        target.next = new_node
        self._count += 1

    def __iter__(self) -> Iterator[T]:
        curr = self._dummy_head.next
        while curr is not self._dummy_tail:
            assert curr is not None
            yield curr.value  # type: ignore[misc]
            curr = curr.next

    def __repr__(self) -> str:
        items = [str(x) for x in self]
        return " <-> ".join(items) if items else "Empty"


if __name__ == "__main__":
    sll = SinglyLinkedList[int]()
    sll.insert_head(20)
    sll.insert_head(10)
    sll.insert_tail(30)
    print(f"SLL: {sll}")  # 10 -> 20 -> 30
    sll.reverse()
    print(f"Reversed SLL: {sll}")  # 30 -> 20 -> 10

    dll = DoublyLinkedList[int]()
    n1 = dll.insert_tail(100)
    n2 = dll.insert_tail(200)
    n3 = dll.insert_tail(300)
    print(f"DLL: {dll}")  # 100 <-> 200 <-> 300
    dll.delete_node(n2)
    print(f"After deleting 200: {dll}")  # 100 <-> 300
```

---

## 🔬 CHAPTER 6: DIFFERENCES & COMPLEXITY DECONSTRUCTION

### Comprehensive Big-O Operation Matrix

| Operation | Fixed Array | Dynamic Array | Singly Linked List | Doubly Linked List (Sentinel) |
| :--- | :--- | :--- | :--- | :--- |
| **Access Index `K` (`Get(k)`)** | `O(1)` | `O(1)` | `O(K)` | `O(K)` |
| **Search by Value** | `O(N)` | `O(N)` | `O(N)` | `O(N)` |
| **Insert Head / Prepend** | `O(N)` (shift) | `O(N)` (shift) | **`O(1)`** | **`O(1)`** |
| **Insert Tail / Append** | `O(1)` (until full)| **Amortized `O(1)`** | **`O(1)`** (with tail) | **`O(1)`** |
| **Insert After Known Node** | `O(N)` (shift) | `O(N)` (shift) | **`O(1)`** | **`O(1)`** |
| **Delete Head** | `O(N)` (shift) | `O(N)` (shift) | **`O(1)`** | **`O(1)`** |
| **Delete Tail** | `O(1)` | **Amortized `O(1)`** | `O(N)` (must find prev)| **`O(1)`** |
| **Delete Given Node Pointer**| `O(N)` | `O(N)` | `O(N)` (must find prev)| **`O(1)`** |
| **Per-Element Memory Overhead**| **0 bytes** | ~0 to 4 bytes headroom | **8 bytes** (next ptr + header)| **16 bytes** (prev+next ptrs + header) |
| **Cache Locality** | Maximum | High | Poor (cache miss per node)| Poor (cache miss per node) |

---

## 🎙️ CHAPTER 7: 45-MINUTE INTERVIEW VERBAL SCRIPTS

### Script 1: Defending the 3-Pointer In-Place Reversal State Machine
> *"When reversing a singly linked list, the common bug is severing the next reference prematurely, which orphans the rest of the list in memory. To prevent this, I maintain three pointers: `prev`, `curr`, and `next`. Before updating `curr.next = prev`, I store `curr.next` in a temporary `next` variable. Then I advance `prev` to `curr`, and `curr` to `next`. This process takes strictly `O(N)` time and `O(1)` auxiliary space because we only mutate existing pointers in place without allocating new node objects."*

### Script 2: Explaining Fast & Slow Runner Mechanics (Floyd's Cycle Algorithm)
> *"To detect cycles or find the midpoint in a single pass without extra memory, I use two pointers moving at different speeds. For midpoint finding, `fast` advances two steps while `slow` advances one. When `fast` reaches the tail, `slow` is guaranteed to be at `N/2` (the middle). For cycle detection, if a cycle exists, the relative speed gap between them decreases by 1 node per iteration, mathematically guaranteeing they will collide inside the loop in `O(N)` steps without requiring an `O(N)` HashSet."*

### Script 3: Justifying the Sentinel (Dummy Node) Pattern
> *"In linked list problems involving deletion or insertion, modifying the head node usually requires branching logic (e.g., `if (head == target) head = head.next`). By initializing a `dummy` sentinel node whose `next` pointer points directly to `head`, we treat the head node identically to any internal node. This guarantees that `slow.next = slow.next.next` works cleanly even when removing the original head, and we simply return `dummy.next` at the end."*

---

## ⚖️ CHAPTER 8: PRODUCTION TRADEOFFS & SYSTEMS CONTEXT

> [!NOTE]
> **Interview & Systems Context: LRU Cache (Doubly Linked List + Hash Map)**  
> Production in-memory caches (Redis, Guava, Memcached) implement Least Recently Used (LRU) eviction using a Doubly Linked List paired with a Hash Map. The Hash Map provides `O(1)` lookup to any node pointer, while the Doubly Linked List allows `O(1)` removal and splicing to the head of the eviction queue without shifting array buffers.

> [!NOTE]
> **Interview & Systems Context: Kernel Free Lists & Memory Allocators**  
> Operating system memory allocators (like `dlmalloc` and Linux slab allocators) track available chunks of free memory using circular doubly linked lists known as "free lists". Because free memory blocks are non-contiguous and constantly merged (coalescing) or split, linked structures enable `O(1)` insertion and deletion of memory descriptors without data relocation.

> [!NOTE]
> **Interview & Systems Context: Pointer Chasing vs. Array Cache Reality**  
> In theory, inserting into a linked list is `O(1)` while an array is `O(N)`. However, in production, traversing a linked list causes a CPU cache miss on almost every node dereference because nodes are scattered non-contiguously across the heap. On modern hardware, an `O(N)` contiguous array copy of 1,000 integers is often 5x to 10x faster than an `O(1)` linked list node insertion due to memory hierarchy latency.

---

## ⚔️ SUPPLEMENTARY OUTCOMES & REVISION

### 🏋️ Practice Problems

| Problem | Difficulty | Key Concept | Target Complexity |
| :--- | :--- | :--- | :--- |
| **Reverse Linked List** | 🟢 Easy | 3-Pointer Iterative Rewiring | `O(N)` Time, `O(1)` Space |
| **Middle of the Linked List** | 🟢 Easy | Fast & Slow Runner | `O(N)` Time, `O(1)` Space |
| **Linked List Cycle II** | 🟡 Medium | Floyd's Phase 1 & Phase 2 | `O(N)` Time, `O(1)` Space |
| **Remove Nth Node From End** | 🟡 Medium | Sentinel Dummy Head + Offset Runner | `O(N)` Time, `O(1)` Space |
| **Design LRU Cache Eviction Chain** | 🟠 Advanced | Doubly Linked List + Hash Map | `O(1)` Time All Operations |

### 🎙️ Quick Technical Screen Q&A

1. **Q:** *Why is `DeleteTail` O(N) in a Singly Linked List even with a tail pointer?*  
   **A:** Because updating the tail requires finding the node immediately preceding it (`node.next == tail`), which requires traversing all `N` nodes from the head.
2. **Q:** *What is the lost-reference bug and how do you prevent it?*  
   **A:** It occurs when `curr.next` is overwritten before saving its existing downstream pointer, permanently orphaning the rest of the list. It is prevented by always saving `next = curr.next` into a temporary variable first.
3. **Q:** *What is the primary advantage of Sentinel Dummy Nodes?*  
   **A:** They eliminate boundary special cases (such as deleting the head, inserting into an empty list, or updating pointers at the tail), removing conditional branching.
4. **Q:** *Why does an LRU Cache require a Doubly Linked List instead of a Singly Linked List?*  
   **A:** Because when a node is accessed, it must be removed from its current position and moved to the head in `O(1)` time. A Doubly Linked List allows in-place `O(1)` detachment using `node.prev.next = node.next` without scanning from the head.

---

> 🧭 **Navigation:** [← Previous Day](Week_02_Day_02_Dynamic_Arrays_Amortized_Growth_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_02_Day_04_Stacks_Queues_Deques_Instructional.md)

# Week 02 Extended Python Complete v13

Purpose: translate Week 02 linear data structures and binary search into Python implementation fluency.

## Week 02 Python focus

Python gives you high-level containers quickly, but Week 02 is about understanding what those containers cost and when not to use them blindly.

Topics covered:
- Python lists as dynamic arrays
- linked-list interview patterns with custom nodes
- stack and queue patterns in Python
- deque vs list trade-offs
- binary search with explicit invariants
- string/number representation details that still matter in Python

---

## Pattern recognition map

| Signal | Python structure | Why |
|---|---|---|
| append/pop at end | `list` | amortized O(1), dynamic array behavior |
| queue from front | `collections.deque` | avoids O(n) front-pop from list |
| linked list problem | custom `ListNode` | Python has no built-in interview-style singly linked list |
| sorted boundary search | while loop + invariants | clearer than memorizing `bisect` blindly |

---

## 1. Dynamic arrays in Python

Python `list` is the closest built-in example of a dynamic array.

```python
values = []
for x in range(10):
    values.append(x)
```

Conceptual truth:
- append is amortized O(1)
- occasional resize is expensive
- random indexing is O(1)
- insertion/deletion near the front is O(n)

Interview pitfall:
- `pop(0)` is not queue behavior you should rely on

---

## 2. Linked list node template

```python
class ListNode:
    def __init__(self, val=0, next=None):
        self.val = val
        self.next = next
```

Reverse list:

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

Invariant:
- `prev` is the reversed prefix
- `curr` is the next node to rewire

---

## 3. Stack, queue, deque in Python

Stack:

```python
stack = []
stack.append(10)
stack.append(20)
top = stack.pop()
```

Queue / deque:

```python
from collections import deque

q = deque()
q.append(10)
q.append(20)
front = q.popleft()
```

Rule:
- list for stack
- deque for queue or double-ended behavior

---

## 4. Binary search with explicit invariants

```python
def binary_search(nums, target):
    left, right = 0, len(nums) - 1
    while left <= right:
        mid = left + (right - left) // 2
        if nums[mid] == target:
            return mid
        if nums[mid] < target:
            left = mid + 1
        else:
            right = mid - 1
    return -1
```

Invariant:
- if the target exists, it remains inside `[left, right]`

Lower bound pattern:

```python
def lower_bound(nums, target):
    left, right = 0, len(nums)
    while left < right:
        mid = (left + right) // 2
        if nums[mid] < target:
            left = mid + 1
        else:
            right = mid
    return left
```

---

## 5. Strings and numbers in Python

Python hides memory management, but the conceptual lessons still apply:
- strings are immutable
- repeated concatenation in loops is costly
- use list accumulation plus `''.join(...)` when building big strings

```python
def build_words(parts):
    out = []
    for p in parts:
        out.append(p)
    return ''.join(out)
```

Parsing integer carefully:

```python
def parse_int(s):
    i = 0
    sign = 1
    while i < len(s) and s[i].isspace():
        i += 1
    if i < len(s) and s[i] in '+-':
        sign = -1 if s[i] == '-' else 1
        i += 1
    value = 0
    while i < len(s) and s[i].isdigit():
        value = value * 10 + (ord(s[i]) - ord('0'))
        i += 1
    return sign * value
```

---

## Python interview checklist for Week 02

- Mention `list` as dynamic-array backed.
- Use `deque` for FIFO queue operations.
- Write custom `ListNode` for linked-list questions.
- State binary-search invariant before code.
- For strings, mention immutability and `join` when building outputs.

---

## Recommended practice

Must:
- reverse linked list
- middle of linked list
- valid parentheses with stack
- binary search / lower bound / first occurrence
- queue with deque

Should:
- circular buffer reasoning
- remove duplicates from sorted array
- merge two sorted arrays

Optional:
- implement a minimal deque wrapper
- compare `bisect_left` with hand-written lower bound

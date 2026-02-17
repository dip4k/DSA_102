# ↔️ Deque Traversal — Guide + Visual Playbook (C# + Python)

> Goal: Learn how to traverse and process data using a **deque** (double-ended queue), where you can add and remove items from **both ends**.

---

## ✅ What you will master
- Deque model: ordered items with a **front** and a **back**.
- Core operations: add/remove/peek on both ends.
- Traversal Pattern A: **Consume from the front** (queue-style).
- Traversal Pattern B: **Consume from the back** (stack-style).
- Traversal Pattern C: **Two-ended traversal** (process from both ends).
- Traversal Pattern D: **Sliding window** and “recent history” buffers.
- Traversal Pattern E: **Monotonic deque** (window maximum/minimum).
- Traversal Pattern F: **0–1 Breadth-First Search** (deque for two-cost edges).

---

# 0) Deque in one picture

A deque has two ends, so it generalizes both a queue and a stack.

```
Front                                        Back
  |                                           |
  v                                           v
[ itemA ]  [ itemB ]  [ itemC ]  [ itemD ]
  ^                                           ^
Remove from front (popleft / RemoveFirst)     Remove from back (pop / RemoveLast)
Add to front (appendleft / AddFirst)         Add to back (append / AddLast)
```

A deque is a **double-ended queue**: you can insert or delete at both ends. [web:311]

---

# 1) Core operations and safety

## What
A deque supports:
- Add to front
- Add to back
- Remove from front
- Remove from back
- Peek front
- Peek back

## Why
Deque traversal is mostly loops that repeatedly remove items from one end (or both ends), so correct empty checks prevent runtime errors.

## How (step-by-step flow)
1) Before removing or peeking, check if the deque is empty.
2) Every traversal loop must make progress by removing at least one item.

## Edge cases ✅
- Removing from an empty deque: must be prevented.

## Common pitfalls
- Treating a deque like an array (random access) and losing the main benefit.

---

# 2) Python deque (recommended)

Python’s `collections.deque` is designed for efficient operations on both ends and provides `append`, `appendleft`, `pop`, and `popleft`. [web:35][web:32]

### Minimal Python setup
```python
from collections import deque

numbers = deque([1, 2, 3])
numbers.append(4)      # add to back
numbers.appendleft(0)  # add to front
numbers.pop()          # remove from back
numbers.popleft()      # remove from front
```

---

# 3) C# deque (common approach)

C# does not include a dedicated `Deque<T>` in the base collections; a common built-in substitute is `LinkedList<T>` because it supports adding/removing at both ends. [web:319][web:313][web:310]

## A small C# Deque wrapper (teaching version)
```csharp
using System;
using System.Collections.Generic;

sealed class Deque<T>
{
    private readonly LinkedList<T> _items = new LinkedList<T>();

    public int Count => _items.Count;

    public void AddToFront(T value) => _items.AddFirst(value);
    public void AddToBack(T value) => _items.AddLast(value);

    public T RemoveFromFront()
    {
        if (_items.Count == 0) throw new InvalidOperationException("Deque is empty.");
        T value = _items.First!.Value;
        _items.RemoveFirst();
        return value;
    }

    public T RemoveFromBack()
    {
        if (_items.Count == 0) throw new InvalidOperationException("Deque is empty.");
        T value = _items.Last!.Value;
        _items.RemoveLast();
        return value;
    }

    public T PeekFront()
    {
        if (_items.Count == 0) throw new InvalidOperationException("Deque is empty.");
        return _items.First!.Value;
    }

    public T PeekBack()
    {
        if (_items.Count == 0) throw new InvalidOperationException("Deque is empty.");
        return _items.Last!.Value;
    }
}
```
`LinkedList<T>.AddFirst` adds to the start, `RemoveFirst` removes from the start, and `RemoveLast` removes from the end (these methods throw if the list is empty). [web:319][web:313][web:310]

---

# 4) Traversal Pattern A: Consume from the front (queue-style)

## For what
Process items in arrival order.

## Visual
```
Front -> [A, B, C, D] <- Back
Consume: A then B then C then D
```

## Python
```python
from collections import deque

def consume_from_front(items):
    work_deque = deque(items or [])
    output = []
    while work_deque:
        output.append(work_deque.popleft())
    return output
```

## C#
```csharp
static List<T> ConsumeFromFront<T>(Deque<T> deque)
{
    var output = new List<T>();
    if (deque == null) return output;

    while (deque.Count > 0)
        output.Add(deque.RemoveFromFront());

    return output;
}
```

## Edge cases ✅
- Empty deque: output is empty.

## Gotchas
- If you re-add items while consuming, you must ensure termination.

---

# 5) Traversal Pattern B: Consume from the back (stack-style)

## For what
Reverse processing order, backtracking, undo-like behavior.

## Visual
```
Front -> [A, B, C, D] <- Back
Consume from back: D then C then B then A
```

## Python
```python
from collections import deque

def consume_from_back(items):
    work_deque = deque(items or [])
    output = []
    while work_deque:
        output.append(work_deque.pop())
    return output
```

## C#
```csharp
static List<T> ConsumeFromBack<T>(Deque<T> deque)
{
    var output = new List<T>();
    if (deque == null) return output;

    while (deque.Count > 0)
        output.Add(deque.RemoveFromBack());

    return output;
}
```

---

# 6) Traversal Pattern C: Two-ended traversal (meet-in-the-middle)

## For what
Problems where you compare or consume from both ends.

### Example: Palindrome check (letters only, simple)

#### Visual
```
frontIndex -> "r a c e c a r" <- backIndex
compare r==r, a==a, c==c, ...
```

#### Python (deque approach)
```python
from collections import deque

def is_palindrome_simple(text):
    if text is None:
        return False

    characters = deque([ch for ch in text if ch.isalnum()])

    while len(characters) > 1:
        if characters.popleft().lower() != characters.pop().lower():
            return False
    return True
```

#### C# (two-ended traversal without allocating a deque)
Note: for strings, two indices are typically faster than building a deque, but the *traversal idea* is the same.

```csharp
static bool IsPalindromeSimple(string text)
{
    if (text == null) return false;

    int leftIndex = 0;
    int rightIndex = text.Length - 1;

    while (leftIndex < rightIndex)
    {
        while (leftIndex < rightIndex && !char.IsLetterOrDigit(text[leftIndex])) leftIndex++;
        while (leftIndex < rightIndex && !char.IsLetterOrDigit(text[rightIndex])) rightIndex--;

        if (char.ToLowerInvariant(text[leftIndex]) != char.ToLowerInvariant(text[rightIndex]))
            return false;

        leftIndex++;
        rightIndex--;
    }

    return true;
}
```

## Edge cases ✅
- Empty or 1-character input: palindrome by definition (decide your contract).

## Pitfalls
- Forgetting to move both ends each iteration.

---

# 7) Traversal Pattern D: Sliding window with a deque

## For what
Maintain a window of the last `windowSize` items (history buffer).

## Visual
```
Window size = 3
Stream: 10, 20, 30, 40
Deque:
[10]
[10,20]
[10,20,30]
[20,30,40]  (after removing 10 from front)
```

## Python (moving average, teaching version)
```python
from collections import deque

def moving_average(values, window_size):
    if not values or window_size <= 0:
        return []

    window = deque()
    window_sum = 0
    result = []

    for value in values:
        window.append(value)
        window_sum += value

        if len(window) > window_size:
            window_sum -= window.popleft()

        if len(window) == window_size:
            result.append(window_sum / window_size)

    return result
```

## Edge cases ✅
- window_size larger than number of values: decide whether to emit partial results.

## Pitfalls
- Recomputing from scratch each step instead of updating incrementally.

---

# 8) Traversal Pattern E: Monotonic deque (sliding window maximum)

## For what
Compute maximum of every window in linear time.

## Visual invariant
The deque stores indices in decreasing value order:
- Front index is always the maximum for the current window.

```
values:  [1, 3, -1, -3, 5, 3, 6, 7]
indices:  0  1   2   3  4  5  6  7

Deque holds indices like: [1, 4, 6, ...]
Front is the max candidate.
```

## Python
```python
from collections import deque

def sliding_window_maximum(values, window_size):
    if not values or window_size <= 0:
        return []

    candidate_indices = deque()
    result = []

    for current_index, current_value in enumerate(values):
        window_start_index = current_index - window_size + 1

        while candidate_indices and candidate_indices[0] < window_start_index:
            candidate_indices.popleft()

        while candidate_indices and values[candidate_indices[-1]] <= current_value:
            candidate_indices.pop()

        candidate_indices.append(current_index)

        if current_index >= window_size - 1:
            result.append(values[candidate_indices[0]])

    return result
```

## Edge cases ✅
- Duplicates: using `<=` removes older equal values so the newest stays.

## Pitfalls
- Storing values instead of indices prevents you from expiring items correctly.

---

# 9) Traversal Pattern F: 0–1 Breadth-First Search (deque)

## For what
Shortest path in a graph where edge weights are only 0 or 1.

## Why
A deque supports:
- push to front for cost 0
- push to back for cost 1

This simulates a priority queue for the 0/1 cost case.

## Visual
```
If an edge cost is 0: new node goes to the front (process sooner)
If an edge cost is 1: new node goes to the back (process later)
```

## Python (template)
```python
from collections import deque

INF = 10**18

def zero_one_bfs(start_node, adjacency):
    # adjacency[node] = list of (neighbor, cost) where cost in {0,1}
    distance = {start_node: 0}
    work = deque([start_node])

    while work:
        current = work.popleft()
        current_distance = distance[current]

        for neighbor, cost in adjacency.get(current, []):
            new_distance = current_distance + cost
            if neighbor not in distance or new_distance < distance[neighbor]:
                distance[neighbor] = new_distance
                if cost == 0:
                    work.appendleft(neighbor)
                else:
                    work.append(neighbor)

    return distance
```

## Pitfalls
- Forgetting to update distances before enqueuing.
- Using a normal queue (front-only) loses the “0 goes first” priority.

---

# ✅ Visual playbook (copy-paste templates)

## Playbook A: Consume from front
```
while deque not empty:
  item = remove_front()
  process(item)
```

## Playbook B: Consume from back
```
while deque not empty:
  item = remove_back()
  process(item)
```

## Playbook C: Two-ended traversal
```
while deque has at least 2 items:
  left = remove_front()
  right = remove_back()
  compare/process(left, right)
```

## Playbook D: Sliding window buffer
```
add_to_back(new)
if size too big: remove_front()
use window state
```

## Playbook E: Monotonic deque
```
remove expired indices from front
remove dominated indices from back
add current index
front is answer
```

## Playbook F: 0–1 BFS
```
if cost 0: push to front
if cost 1: push to back
```

---

# 🧭 Mastery route (fastest path)

1) Practice consume-from-front and consume-from-back with small examples.
2) Practice two-ended traversal (palindrome / symmetric checks).
3) Practice sliding window buffer with incremental state (sum, count).
4) Learn monotonic deque for sliding window maximum.
5) Learn 0–1 BFS if you do graph problems.

If you want, I can generate a drill pack (with expected outputs) for each deque pattern in the same style as your merge-techniques drills.

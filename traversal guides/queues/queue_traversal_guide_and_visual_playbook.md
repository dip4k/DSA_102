# 🚶 Queue Traversal — Guide + Visual Playbook (C# + Python)

> Goal: Learn how to **traverse** and process items in **First In, First Out** order using a **queue**, and learn the main queue-based traversal patterns used in interviews and real systems.

---

## ✅ What you will master
- The queue model: **First In, First Out** (the first item you add is the first removed).
- Traversal Pattern A: **Dequeue-until-empty** traversal.
- Traversal Pattern B: **Breadth-first traversal** (level-order) using a queue.
- Traversal Pattern C: **Layered processing** (process items in “waves” / levels).
- Traversal Pattern D: **Sliding window** processing using a queue.
- Traversal Pattern E: **Monotonic queue** for fast window maximum/minimum.
- Traversal Pattern F: **Task scheduling** and “work queue” systems.

---

# 0) Queue in one picture

A queue is like a real-life line:

```
Front                                        Back
  |                                           |
  v                                           v
[ itemA ]  [ itemB ]  [ itemC ]  [ itemD ]
  ^
Dequeue removes from the front
Enqueue adds to the back

Enqueue(itemE) -> [ itemA, itemB, itemC, itemD, itemE ]
Dequeue()      -> removes itemA
```

**Two rules**:
1) Items enter at the back.
2) Items leave from the front.

---

# 1) Core operations and safety

## What
Queues are operated with:
- **Enqueue**: add to the back.
- **Dequeue**: remove from the front.
- **Peek**: look at the front without removing.

## Why
Queue traversal is mostly a loop that repeatedly dequeues. Safety comes from empty checks.

## How (step-by-step flow)
1) Before dequeue or peek: ensure the queue is not empty.
2) Ensure each loop iteration makes progress by removing one item.

## Edge cases ✅
- Dequeue on empty queue must be prevented (or handled).
- Enqueue and dequeue must be balanced in traversal loops; otherwise the loop may never finish.

## Gotchas / things to remember
- A queue is not for random access. If you need to remove from the middle, use a different structure.

---

# 2) Traversal Pattern A: Dequeue-until-empty

## For what
Process work items in the order they arrived.

## Why
This is the simplest and most reusable queue traversal.

## Where
- Job processing.
- Event handling.
- Simulating “time order” operations.

## How (step-by-step flow)
1) Enqueue initial items.
2) While the queue is not empty:
   - Dequeue one item.
   - Process it.
   - Optionally enqueue more items.

### Visual
```
Queue starts: [A, B, C]
Process: dequeue A -> queue [B, C]
Process: dequeue B -> queue [C]
Process: dequeue C -> queue []
Stop
```

### C# example: basic work queue traversal
```csharp
using System;
using System.Collections.Generic;

static void ProcessWorkItemsInOrder(IEnumerable<string> workItems)
{
    if (workItems == null) return;

    Queue<string> workQueue = new Queue<string>();
    foreach (string item in workItems)
        workQueue.Enqueue(item);

    while (workQueue.Count > 0)
    {
        string currentItem = workQueue.Dequeue();
        Console.WriteLine($"Processing: {currentItem}");
    }
}
```

### Python example (use collections.deque)
```python
from collections import deque

def process_work_items_in_order(work_items):
    if work_items is None:
        return

    work_queue = deque(work_items)

    while work_queue:
        current_item = work_queue.popleft()
        print("Processing:", current_item)
```

## Edge cases ✅
- Empty input list.
- Items that enqueue more items: the queue can grow; termination depends on your logic.

## Pitfalls
- Enqueuing without a terminating condition can create an infinite loop.

---

# 3) Traversal Pattern B: Breadth-first traversal (graph or tree)

## For what
Visit nodes level by level (closest first).

## Why
A queue naturally enforces level order: nodes discovered first are processed first.

## Where
- Shortest path in unweighted graphs.
- Tree level-order traversal.
- Flood fill and multi-source spreading.

## How (step-by-step flow)
1) Enqueue the start node.
2) Mark it as visited.
3) While queue not empty:
   - Dequeue node.
   - For each neighbor:
     - If not visited, mark visited and enqueue.

### Visual
```
Start at node 1
Queue: [1]
Dequeue 1, enqueue its neighbors: [2, 3]
Dequeue 2, enqueue its neighbors: [3, 4] (3 maybe already visited)
...
```

### C# example: breadth-first traversal (graph)
```csharp
using System.Collections.Generic;

static List<int> BreadthFirstTraversal(
    int startNode,
    Dictionary<int, List<int>> adjacencyList)
{
    List<int> visitOrder = new List<int>();
    if (adjacencyList == null) return visitOrder;

    HashSet<int> visited = new HashSet<int>();
    Queue<int> queue = new Queue<int>();

    visited.Add(startNode);
    queue.Enqueue(startNode);

    while (queue.Count > 0)
    {
        int currentNode = queue.Dequeue();
        visitOrder.Add(currentNode);

        if (!adjacencyList.TryGetValue(currentNode, out List<int> neighbors))
            continue;

        foreach (int neighbor in neighbors)
        {
            if (visited.Contains(neighbor)) continue;
            visited.Add(neighbor);
            queue.Enqueue(neighbor);
        }
    }

    return visitOrder;
}
```

### Python example
```python
from collections import deque

def breadth_first_traversal(start_node, adjacency_list):
    adjacency_list = adjacency_list or {}

    visit_order = []
    visited = set([start_node])
    queue = deque([start_node])

    while queue:
        current_node = queue.popleft()
        visit_order.append(current_node)

        for neighbor in adjacency_list.get(current_node, []):
            if neighbor in visited:
                continue
            visited.add(neighbor)
            queue.append(neighbor)

    return visit_order
```

## Edge cases ✅
- Graph has cycles: visited set is required.
- Graph disconnected: traversal covers only reachable nodes from the start.

## Pitfalls
- Marking visited too late (mark on enqueue, not on dequeue) can cause duplicates in the queue.

---

# 4) Traversal Pattern C: Layered (level-by-level) processing

## For what
Process items grouped by “distance” or “time step” (levels).

## Why
Sometimes you must know where one level ends and the next begins.

## Two common ways
### Method 1: Use current queue size as level size

#### How (step-by-step)
1) `levelItemCount = queue.Count` at the start of the level.
2) Dequeue exactly `levelItemCount` items.
3) Enqueue their children for the next level.

### Visual
```
Queue contains current level nodes
levelItemCount tells you how many to process before starting next level
```

### C# snippet: level-order traversal of a binary tree
```csharp
using System.Collections.Generic;

sealed class BinaryTreeNode
{
    public int Value;
    public BinaryTreeNode? Left;
    public BinaryTreeNode? Right;
    public BinaryTreeNode(int value) { Value = value; }
}

static List<List<int>> LevelOrder(BinaryTreeNode? root)
{
    List<List<int>> levels = new List<List<int>>();
    if (root == null) return levels;

    Queue<BinaryTreeNode> queue = new Queue<BinaryTreeNode>();
    queue.Enqueue(root);

    while (queue.Count > 0)
    {
        int levelItemCount = queue.Count;
        List<int> levelValues = new List<int>(levelItemCount);

        for (int index = 0; index < levelItemCount; index++)
        {
            BinaryTreeNode currentNode = queue.Dequeue();
            levelValues.Add(currentNode.Value);

            if (currentNode.Left != null) queue.Enqueue(currentNode.Left);
            if (currentNode.Right != null) queue.Enqueue(currentNode.Right);
        }

        levels.Add(levelValues);
    }

    return levels;
}
```

### Python snippet
```python
from collections import deque

class BinaryTreeNode:
    def __init__(self, value, left=None, right=None):
        self.value = value
        self.left = left
        self.right = right


def level_order(root):
    if root is None:
        return []

    levels = []
    queue = deque([root])

    while queue:
        level_item_count = len(queue)
        level_values = []

        for _ in range(level_item_count):
            current_node = queue.popleft()
            level_values.append(current_node.value)

            if current_node.left is not None:
                queue.append(current_node.left)
            if current_node.right is not None:
                queue.append(current_node.right)

        levels.append(level_values)

    return levels
```

## Edge cases ✅
- Empty tree.
- Single node.

## Pitfalls
- Forgetting to snapshot the level size before the for-loop.

---

# 5) Traversal Pattern D: Sliding window with a queue

## For what
Process a moving window of the last `windowSize` items.

## Why
A queue naturally represents “the last N items in order.”

## How (step-by-step flow)
1) For each new item:
   - Enqueue it.
   - If queue size exceeds window size, dequeue one.
   - Compute whatever you need from the window.

### Visual
```
Window size = 3
Stream:  10, 20, 30, 40
Queue:
[10]
[10,20]
[10,20,30]
[20,30,40] (after removing 10)
```

### C# example: moving average (teaching version)
```csharp
using System.Collections.Generic;

static List<double> MovingAverage(int[] values, int windowSize)
{
    var result = new List<double>();
    if (values == null || values.Length == 0 || windowSize <= 0) return result;

    Queue<int> window = new Queue<int>();
    long windowSum = 0;

    for (int index = 0; index < values.Length; index++)
    {
        int value = values[index];
        window.Enqueue(value);
        windowSum += value;

        if (window.Count > windowSize)
            windowSum -= window.Dequeue();

        if (window.Count == windowSize)
            result.Add(windowSum / (double)windowSize);
    }

    return result;
}
```

### Python example
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
- Window size > number of values: decide whether to output partial windows (here we do not).

## Pitfalls
- Recomputing statistics from scratch each time (use incremental updates like windowSum).

---

# 6) Traversal Pattern E: Monotonic queue (window maximum/minimum)

## For what
Find maximum (or minimum) of every sliding window in linear time.

## The key idea
Maintain a deque of **candidate indices** in decreasing order of values. Front always holds the best candidate for the current window.

### Visual
```
Values:  [1, 3, -1, -3, 5, 3, 6, 7]
Deque holds indices with values in decreasing order
Front index always points to max for current window
```

## How (step-by-step flow)
For each new index:
1) Remove indices from the front if they are out of the window.
2) Remove indices from the back while their values are <= current value (they can never be max again).
3) Add current index to the back.
4) The front is the maximum index.

### Python example (most concise)
```python
from collections import deque

def sliding_window_maximum(values, window_size):
    if not values or window_size <= 0:
        return []

    candidate_indices = deque()
    result = []

    for current_index, current_value in enumerate(values):
        window_start_index = current_index - window_size + 1

        # 1) remove expired indices
        while candidate_indices and candidate_indices[0] < window_start_index:
            candidate_indices.popleft()

        # 2) remove dominated indices
        while candidate_indices and values[candidate_indices[-1]] <= current_value:
            candidate_indices.pop()

        candidate_indices.append(current_index)

        # 3) record result once the first window is complete
        if current_index >= window_size - 1:
            result.append(values[candidate_indices[0]])

    return result
```

### Edge cases ✅
- Duplicates: using `<=` removes older equal values so the newest survives.

### Pitfalls
- Storing values instead of indices: you must know when an item leaves the window.

---

# 7) Traversal Pattern F: Work queues and scheduling

## For what
Process tasks in arrival order, possibly with retries.

## Why
Queues model fairness: older tasks get processed before newer tasks.

## Visual
```
Enqueue tasks as they arrive
Dequeue and process one by one
Optionally enqueue new tasks created by processing
```

## Pitfalls
- “Poison task” that always fails can block progress if re-enqueued immediately.
- You may need a retry limit or a separate delayed queue.

---

# ✅ Visual playbook (templates you can reuse)

## Playbook A: Dequeue-until-empty
```
while queue not empty:
  item = queue.dequeue()
  process(item)
```

## Playbook B: Breadth-first traversal (graph)
```
visited = set()
queue.enqueue(start)
visited.add(start)

while queue not empty:
  node = queue.dequeue()
  for neighbor in neighbors(node):
    if neighbor not visited:
      visited.add(neighbor)
      queue.enqueue(neighbor)
```

## Playbook C: Level-by-level processing
```
while queue not empty:
  levelItemCount = queue.size()
  repeat levelItemCount times:
    item = queue.dequeue()
    process(item)
    enqueue children
```

## Playbook D: Sliding window
```
enqueue new value
if queue size too big: dequeue
use window state
```

## Playbook E: Monotonic queue (window maximum)
```
remove expired indices from front
remove dominated indices from back
append current index
front is maximum
```

---

# 🧭 Mastery route (fastest learning path)

1) Implement Dequeue-until-empty using Queue (C#) and deque (Python).
2) Implement breadth-first traversal with visited set.
3) Add level-by-level processing using queue size.
4) Add sliding window with incremental state (sum/count).
5) Add monotonic queue for window maximum.

If you want, I can generate drills (with expected outputs) for each pattern in the same style as your merge-techniques drill packs.

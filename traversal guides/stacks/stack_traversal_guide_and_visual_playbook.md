# 📚 Stack Traversal — Guide + Visual Playbook (C# + Python)

> Goal: Learn how to **traverse** (process) sequences, nested structures, and “next/previous” relationships using a **stack**.

---

## ✅ What you will master
- The stack model: **Last In, First Out** (the most recently pushed item is the first popped).
- Traversal Pattern A: **Pop-until-empty** traversal.
- Traversal Pattern B: **Scan input while maintaining a stack** (monotonic stack family).
- Traversal Pattern C: **Simulate recursion** using an explicit stack (depth-first traversal).
- Traversal Pattern D: **Parsing with a stack** (balanced symbols, nesting).

---

# 0) Stack in one picture

A stack is like a vertical pile of plates:

```
Top
 ┌───────┐
 │  30   │  <- pop removes from here
 ├───────┤
 │  20   │
 ├───────┤
 │  10   │
 └───────┘
Bottom

push(40) puts 40 on top
peek()  looks at 30 (top) without removing
pop()   removes 30 (top)
```

**Two rules**:
1) You can only add/remove at the top.
2) The last item you add will be the first item you remove.

---

# 1) Core operations and safety

## What
You will use three core operations:
- **Push**: add an item on top.
- **Pop**: remove and return the top item.
- **Peek**: return the top item without removing it.

## Why
Traversal with stacks relies on repeatedly peeking/popping, so you must be disciplined about **empty-stack checks**.

## How (step-by-step flow)
1) Before `Pop` or `Peek`, check if the stack is empty.
2) Push only values that you will later understand how to pop (have a plan).

## Edge cases ✅
- Popping from an empty stack (underflow) must be handled.
- Pushing `null` (C# reference types) might be allowed, but often indicates a bug.

## Gotchas / things to remember
- Many stack bugs are “forgot to pop” or “popped too early.”
- Always define what each stack item represents (a value, an index, a node, a state).

---

# 2) Traversal Pattern A: Pop-until-empty

## For what
Process items in reverse of insertion order.

## Why
If you push items in a known order, popping lets you traverse them in the exact opposite order.

## Where
- Undo history.
- Backtracking steps.
- Reversing a sequence.

## How (step-by-step flow)
1) Push items.
2) While stack is not empty: pop one item and process it.

### Visual
```
Push order:   1, 2, 3, 4
Pop order:    4, 3, 2, 1
```

### C# example: reverse a string using a stack
```csharp
using System.Collections.Generic;
using System.Text;

static string ReverseStringWithStack(string text)
{
    if (text == null) return "";

    Stack<char> characterStack = new Stack<char>();
    for (int index = 0; index < text.Length; index++)
        characterStack.Push(text[index]);

    StringBuilder reversed = new StringBuilder(text.Length);
    while (characterStack.Count > 0)
        reversed.Append(characterStack.Pop());

    return reversed.ToString();
}
```

### Python example
```python
def reverse_string_with_stack(text):
    if text is None:
        return ""

    character_stack = []
    for character in text:
        character_stack.append(character)

    reversed_characters = []
    while character_stack:
        reversed_characters.append(character_stack.pop())

    return "".join(reversed_characters)
```

## Edge cases ✅
- Empty string.
- Very long string: prefer built-ins for production, but stack is good for learning.

## Pitfalls
- Forgetting to empty the stack completely.

---

# 3) Traversal Pattern B: Scan input while maintaining a stack (Monotonic stack)

## Big idea (visual)
You scan from left to right with an index, and the stack stores “candidates” that are not yet resolved.

```
Input:  [2, 1, 2, 4, 3]
Index:    0  1  2  3  4

You scan left -> right
Stack holds indices of elements waiting for their answer
```

## Why it matters
This pattern solves many problems that look like “next greater element”, “previous smaller element”, “span”, “largest rectangle in histogram”.

## Rule
You maintain the stack in a **monotonic** order:
- Monotonic decreasing stack: values on stack go down (top is smallest), good for “next greater”.
- Monotonic increasing stack: values on stack go up, good for “next smaller”.

## The invariant (what must always be true)
When you keep a monotonic decreasing stack of indices:
- The values at those indices are in decreasing order from bottom to top.
- When a new value is larger than the top, it “resolves” that top item.

---

## Example: Next greater element (index-by-index walkthrough)

### Problem
For each position, find the next element to the right that is greater; if none, use -1.

Input:
```
values = [2, 1, 2, 4, 3]
index    0  1  2  3  4
output  [-1,-1,-1,-1,-1]  (start with -1)
```

We store indices in a stack.

### Walkthrough table
| Scan index | Current value | Stack before (indices -> values) | Action | Stack after | Output changes |
|---:|---:|:---|:---|:---|:---|
| 0 | 2 | [] | push 0 | [0->2] | - |
| 1 | 1 | [0->2] | push 1 | [0->2, 1->1] | - |
| 2 | 2 | [0->2, 1->1] | 2 > 1 → pop 1, set output[1]=2; then push 2 | [0->2, 2->2] | output[1]=2 |
| 3 | 4 | [0->2, 2->2] | 4 > 2 → pop 2 set output[2]=4; 4 > 2 → pop 0 set output[0]=4; push 3 | [3->4] | output[2]=4, output[0]=4 |
| 4 | 3 | [3->4] | push 4 | [3->4, 4->3] | - |
| end | - | [3->4, 4->3] | unresolved → remain -1 | [] | output[3]=-1, output[4]=-1 |

Final output:
```
[4, 2, 4, -1, -1]
```

### C# code: next greater element
```csharp
using System.Collections.Generic;

static int[] NextGreaterToRight(int[] values)
{
    if (values == null) return Array.Empty<int>();

    int length = values.Length;
    int[] nextGreater = new int[length];
    for (int index = 0; index < length; index++) nextGreater[index] = -1;

    Stack<int> unresolvedIndices = new Stack<int>();

    for (int currentIndex = 0; currentIndex < length; currentIndex++)
    {
        int currentValue = values[currentIndex];

        while (unresolvedIndices.Count > 0 && currentValue > values[unresolvedIndices.Peek()])
        {
            int unresolvedIndex = unresolvedIndices.Pop();
            nextGreater[unresolvedIndex] = currentValue;
        }

        unresolvedIndices.Push(currentIndex);
    }

    return nextGreater;
}
```

### Python code
```python
def next_greater_to_right(values):
    if values is None:
        return []

    next_greater = [-1] * len(values)
    unresolved_indices = []  # stack of indices

    for current_index, current_value in enumerate(values):
        while unresolved_indices and current_value > values[unresolved_indices[-1]]:
            unresolved_index = unresolved_indices.pop()
            next_greater[unresolved_index] = current_value
        unresolved_indices.append(current_index)

    return next_greater
```

## Edge cases ✅
- Strict vs non-strict comparison:
  - If you want “next greater or equal”, use `>=`.
  - If you want “strictly greater”, use `>`.
- Duplicates: your comparison decides whether duplicates resolve or remain unresolved.

## Gotchas
- The stack almost always stores **indices**, not values, because you need to write answers back into an output array.

---

# 4) Traversal Pattern C: Simulate recursion using an explicit stack

## For what
Depth-first traversal without recursion (common in interviews and production when recursion depth is risky).

## Why
Recursion uses the call stack implicitly. Using your own stack makes the traversal explicit and controllable.

## Where
- Depth-first search on graphs.
- Tree traversals (preorder, inorder, postorder).
- Backtracking problems.

## How (step-by-step flow)
1) Push the initial state.
2) While stack is not empty:
   - Pop a state.
   - Process it.
   - Push its next states.

### Visual
```
Stack contains "what to do next"
Top item is processed next
```

### Example: iterative depth-first traversal of a graph
C#
```csharp
using System.Collections.Generic;

static List<int> DepthFirstTraversal(int startNode, Dictionary<int, List<int>> adjacencyList)
{
    var visitOrder = new List<int>();
    if (adjacencyList == null) return visitOrder;

    var visited = new HashSet<int>();
    var stack = new Stack<int>();

    stack.Push(startNode);

    while (stack.Count > 0)
    {
        int currentNode = stack.Pop();
        if (visited.Contains(currentNode)) continue;

        visited.Add(currentNode);
        visitOrder.Add(currentNode);

        if (!adjacencyList.TryGetValue(currentNode, out List<int> neighbors)) continue;

        // Push neighbors in reverse if you want a specific visit order
        for (int index = neighbors.Count - 1; index >= 0; index--)
            stack.Push(neighbors[index]);
    }

    return visitOrder;
}
```

Python
```python
def depth_first_traversal(start_node, adjacency_list):
    adjacency_list = adjacency_list or {}

    visit_order = []
    visited = set()
    stack = [start_node]

    while stack:
        current_node = stack.pop()
        if current_node in visited:
            continue

        visited.add(current_node)
        visit_order.append(current_node)

        neighbors = adjacency_list.get(current_node, [])
        for neighbor in reversed(neighbors):
            stack.append(neighbor)

    return visit_order
```

## Edge cases ✅
- Disconnected graph: traversal covers only the reachable part.
- Cycles: you must track visited nodes.

## Gotchas
- If you push neighbors in different orders, the traversal order changes (but still valid DFS).

---

# 5) Traversal Pattern D: Parsing with a stack (nesting and balance)

## For what
Check balanced parentheses/brackets, and generally manage nested structures.

## Why
Nesting is naturally Last In, First Out: the last opened bracket must be the first closed.

## Visual
```
Input:  ( [ { } ] )
Push openers: ( [ {
Pop when you see closers: } ] )
```

## How (step-by-step flow)
1) Scan characters from left to right.
2) If you see an opening bracket, push it.
3) If you see a closing bracket:
   - If stack empty: invalid.
   - Pop top and check if it matches.
4) At end: stack must be empty.

### C# code: balanced brackets
```csharp
using System.Collections.Generic;

static bool HasBalancedBrackets(string text)
{
    if (text == null) return true; // lenient choice

    var stack = new Stack<char>();

    bool IsOpening(char c) => c == '(' || c == '[' || c == '{';
    bool Matches(char opening, char closing) =>
        (opening == '(' && closing == ')') ||
        (opening == '[' && closing == ']') ||
        (opening == '{' && closing == '}');

    for (int index = 0; index < text.Length; index++)
    {
        char character = text[index];

        if (IsOpening(character))
        {
            stack.Push(character);
            continue;
        }

        if (character == ')' || character == ']' || character == '}')
        {
            if (stack.Count == 0) return false;

            char opening = stack.Pop();
            if (!Matches(opening, character)) return false;
        }
    }

    return stack.Count == 0;
}
```

### Python code
```python
def has_balanced_brackets(text):
    if text is None:
        return True

    stack = []

    opening_set = set(['(', '[', '{'])
    matching = {')': '(', ']': '[', '}': '{'}

    for character in text:
        if character in opening_set:
            stack.append(character)
        elif character in matching:
            if not stack:
                return False
            opening = stack.pop()
            if opening != matching[character]:
                return False

    return len(stack) == 0
```

## Edge cases ✅
- Closing bracket appears first.
- Extra opening bracket remains at end.
- Strings containing non-bracket characters.

## Pitfalls
- Not handling empty stack before pop.

---

# 6) Visual playbook (templates you can reuse)

## Playbook A: Pop-until-empty
```
while stack not empty:
  item = stack.pop()
  process(item)
```
Checklist:
- Do you ever push without later popping?
- Do you check empty before pop?

---

## Playbook B: Monotonic stack (next greater / next smaller)

### Next greater to the right (most common)
```
output = [-1] * length
unresolvedIndices = empty stack

for currentIndex from 0 to length-1:
  while unresolvedIndices not empty AND values[currentIndex] > values[unresolvedIndices.top]:
    unresolvedIndex = pop
    output[unresolvedIndex] = values[currentIndex]
  push currentIndex
```
Checklist:
- Are you storing indices (so you can write output)?
- Is your comparison strict (>) or non-strict (>=) as required?

---

## Playbook C: Simulate recursion (depth-first)
```
stack.push(initialState)
while stack not empty:
  state = stack.pop()
  if alreadyVisited(state): continue
  process(state)
  for nextState in nextStates(state):
    stack.push(nextState)
```
Checklist:
- Do you need a visited set to prevent cycles?
- Do you care about output order? (Push in reverse if necessary.)

---

## Playbook D: Balanced parsing
```
for each character:
  if opening: push
  if closing:
    if stack empty: invalid
    opening = pop
    if not matches(opening, closing): invalid
return stack empty
```
Checklist:
- Did you define matching pairs clearly?
- Did you decide what to do with non-bracket characters?

---

# 7) Mastery route (what to learn next)

1) Write 3 small stack traversals without looking:
   - reverse using pop-until-empty
   - balanced brackets
   - next greater element

2) Learn the “index stack” habit:
   - Most array problems store indices in the stack.

3) Add one advanced monotonic-stack problem:
   - largest rectangle in histogram (uses next smaller boundaries)

4) Add one recursion-simulation problem:
   - iterative tree traversal or iterative graph depth-first search

---

If you tell me which domain you want (arrays, strings, trees/graphs, or parsing), I can generate a drill set and traced walkthroughs that match your style.

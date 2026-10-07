# 📘 Week 13 Day 01: Backtracking Fundamentals — Engineering Guide

> 🧭 **Navigation:** [🏠 Week Overview](file:///d:/Interview_prep/DSA_102/week_13_backtracking_and_branch_bound/README.md) • [📘 Curriculum Syllabus](file:///d:/Interview_prep/DSA_102/COMPLETE_SYLLABUS.md) • [Next Day →](file:///d:/Interview_prep/DSA_102/week_13_backtracking_and_branch_bound/Week_13_Day_02_Backtracking_Problems_Instructional.md)
> 
> 💡 **Instructor Note:** *Focus on the invariant: any modification made before a recursive step must be reversed immediately after returning. Master the Choose / Explore / Unchoose state machine in both languages.*

---

## 🎯 Learning Objectives

- 🧠 **Internalize** backtracking as depth-first search on an implicit decision tree where nodes represent partial candidate states.
- ⚙️ **Implement** the universal **Choose / Explore / Unchoose** skeleton in modern C# (.NET 8/9) and Python (3.11+).
- 🌲 **Trace** state space explosions and understand how eager pruning eliminates combinatorial subtrees before exploration.
- 🔬 **Differentiate** between state mutation (in-place backtrack with `O(N)` memory) and immutable state passing (`O(N^2)` memory allocations).
- 🎙️ **Deliver** a structured 45-minute interview narrative deconstructing factorial and exponential time bounds.

---

## 📖 Chapter 1: Mental Model — DFS on Implicit Decision Trees

### The Problem: Combinatorial Explosion

In combinatorial search, evaluating every candidate combination naively guarantees catastrophic time complexity:
- Subsets of size `N`: `2^N` states. For `N = 30`, `2^30 ≈ 1.07 * 10^9` states.
- Permutations of size `N`: `N!` states. For `N = 12`, `12! ≈ 4.79 * 10^8` states.

A brute-force generator creates every leaf before testing validity. **Backtracking**, by contrast, evaluates constraints at every interior node of the decision tree. The moment a partial state violates a problem constraint, the entire subtree rooted at that decision is discarded (**pruned**).

```
BRUTE FORCE vs BACKTRACKING
========================================================================
Brute Force:   Generate complete leaf (depth N) -> Validate -> Keep / Drop
Backtracking:  Check constraint at depth k <= N -> If invalid: PRUNE SUBTREE
```

### The Physical Analogy: Maze Exploration with Chalk Marks

Imagine navigating a labyrinth with a piece of chalk:
1. **Choose**: At an intersection, mark the corridor with chalk and step forward.
2. **Explore**: Follow the corridor deeper into the maze recursively.
3. **Dead End (Constraint Violation / Exhaustion)**: You hit a wall or reach the goal.
4. **Unchoose**: Step back to the intersection, erase your chalk mark, and try the next corridor.

```
State Machine Transition:
 [State S] ──(Choose C)──> [State S + C] ──(Explore)──> [Subproblem]
     ▲                                                        │
     └────────────────(Unchoose C: Revert)────────────────────┘
```

---

## ⚙️ Chapter 2: The Universal "Choose / Explore / Unchoose" Template

Every backtracking problem adheres to a single canonical recursion contract. Both languages must maintain the invariant that the caller's state is preserved across recursive invocations.

### Universal Skeleton

```
function Backtrack(state, choices):
    if IsSolution(state):
        RecordSolution(state)
        return

    for choice in choices:
        if not IsValid(state, choice):
            continue                    // Eager pruning

        Choose(state, choice)           // Mutate state / mark used
        Backtrack(state, remaining)     // Explore recursive subtree
        Unchoose(state, choice)         // Revert state / unmark used
```

### Reference Implementations

Pulled from [`Week_13_Extended_CSharp_Complete.md`](file:///d:/Interview_prep/DSA_102/week_13_backtracking_and_branch_bound/Week_13_Extended_CSharp_Complete.md) and [`Week_13_Extended_Python_Complete.md`](file:///d:/Interview_prep/DSA_102/week_13_backtracking_and_branch_bound/Week_13_Extended_Python_Complete.md):

#### C# (.NET 8/9) Universal Generic Skeleton
```csharp
public static class BacktrackEngine
{
    public static void Backtrack<TState, TChoice>(
        TState state,
        Func<TState, bool> isComplete,
        Func<TState, IEnumerable<TChoice>> getValidChoices,
        Action<TState, TChoice> makeChoice,
        Action<TState, TChoice> undoChoice,
        Action<TState> recordSolution)
    {
        if (isComplete(state))
        {
            recordSolution(state);
            return;
        }

        foreach (var choice in getValidChoices(state))
        {
            makeChoice(state, choice);
            Backtrack(state, isComplete, getValidChoices, makeChoice, undoChoice, recordSolution);
            undoChoice(state, choice);
        }
    }
}
```

#### Python (3.11+) Universal Generic Skeleton
```python
from typing import Callable, Iterable, TypeVar

TState = TypeVar('TState')
TChoice = TypeVar('TChoice')

def backtrack(
    state: TState,
    is_complete: Callable[[TState], bool],
    get_valid_choices: Callable[[TState], Iterable[TChoice]],
    make_choice: Callable[[TState, TChoice], None],
    undo_choice: Callable[[TState, TChoice], None],
    record_solution: Callable[[TState], None],
) -> None:
    if is_complete(state):
        record_solution(state)
        return

    for choice in get_valid_choices(state):
        make_choice(state, choice)
        backtrack(state, is_complete, get_valid_choices, make_choice, undo_choice, record_solution)
        undo_choice(state, choice)
```

---

## 🌲 Chapter 3: Canonical Problem Implementations

### Problem 1: Subsets / Power Set (LeetCode 78)

**Decision at each index `i`:** Either include `nums[i]` or exclude `nums[i]`. Alternatively, use start-index iteration where every recursive step represents a prefix subset.

#### ASCII State Space Tree (`nums = [1, 2, 3]`)
```
                          [] (Root)
               /              |             \
            [1]              [2]            [3]
          /     \             |
       [1,2]   [1,3]        [2,3]
        /
     [1,2,3]

All nodes in this tree are valid subsets (Total = 2^3 = 8):
[], [1], [1, 2], [1, 2, 3], [1, 3], [2], [2, 3], [3]
```

#### Dual Implementation: Subsets

```csharp
// C# (.NET 8/9): Subsets via start index
public static IList<IList<int>> Subsets(int[] nums)
{
    var result = new List<IList<int>>();
    var path = new List<int>();

    void Dfs(int startIndex)
    {
        // Every prefix state is a valid subset; take snapshot
        result.Add([.. path]);

        for (int i = startIndex; i < nums.Length; i++)
        {
            path.Add(nums[i]);       // Choose
            Dfs(i + 1);              // Explore
            path.RemoveAt(path.Count - 1); // Unchoose
        }
    }

    Dfs(0);
    return result;
}
```

```python
# Python (3.11+): Subsets via start index
def subsets(nums: list[int]) -> list[list[int]]:
    result: list[list[int]] = []
    path: list[int] = []

    def dfs(start_index: int) -> None:
        result.append(path.copy())  # Snapshot current state
        for i in range(start_index, len(nums)):
            path.append(nums[i])    # Choose
            dfs(i + 1)              # Explore
            path.pop()              # Unchoose

    dfs(0)
    return result
```

---

### Problem 2: Permutations (LeetCode 46)

**Decision at each step:** Choose any element not yet used in `path`. Track elements with a boolean lookup array `used`.

#### ASCII Recursion Tree with Pruned Branches (`nums = [1, 2, 3]`)
```
                                    []
                 /                  |                  \
              [1]                  [2]                 [3]
           /       \            /       \           /       \
        [1,2]     [1,3]      [2,1]     [2,3]     [3,1]     [3,2]
       /    \     /   \      /   \     /   \     /   \     /   \
   [1,2,3]  [X] [1,3,2][X] [2,1,3][X][2,3,1][X] [3,1,2][X][3,2,1][X]
             ^          ^          ^         ^          ^         ^
    [X] PRUNED: Element already marked in used array (used[i] == true)
```

#### Dual Implementation: Permutations

```csharp
// C# (.NET 8/9): Permutations with used array
public static IList<IList<int>> Permute(int[] nums)
{
    var result = new List<IList<int>>();
    var path = new List<int>(nums.Length);
    var used = new bool[nums.Length];

    void Dfs()
    {
        if (path.Count == nums.Length)
        {
            result.Add([.. path]); // Snapshot path
            return;
        }

        for (int i = 0; i < nums.Length; i++)
        {
            if (used[i]) continue; // Pruning condition

            used[i] = true;        // Choose
            path.Add(nums[i]);

            Dfs();                 // Explore

            path.RemoveAt(path.Count - 1); // Unchoose
            used[i] = false;
        }
    }

    Dfs();
    return result;
}
```

```python
# Python (3.11+): Permutations with used array
def permute(nums: list[int]) -> list[list[int]]:
    result: list[list[int]] = []
    path: list[int] = []
    used = [False] * len(nums)

    def dfs() -> None:
        if len(path) == len(nums):
            result.append(path.copy())
            return

        for i, val in enumerate(nums):
            if used[i]:
                continue  # Pruning condition

            used[i] = True      # Choose
            path.append(val)
            dfs()               # Explore
            path.pop()          # Unchoose
            used[i] = False

    dfs()
    return result
```

---

## 📊 Chapter 4: Explicit Complexity Deconstruction

| Metric | Subsets (LeetCode 78) | Permutations (LeetCode 46) | Analytical Derivation |
| :--- | :--- | :--- | :--- |
| **Total Tree Nodes** | `2^(N + 1) - 1` | `Sum(N! / (N - k)!)` for `k=0..N` | Subsets tree has `2^N` leaves; Permutations tree has `N!` leaves |
| **Time Complexity** | `O(N * 2^N)` | `O(N * N!)` | `O(N)` work to clone each valid path into the result buffer |
| **Auxiliary Stack Space** | `O(N)` | `O(N)` | Maximum recursion depth `d = N` |
| **State Storage Space** | `O(N)` | `O(N)` | Single mutable `path` array + `used` boolean array |
| **Branching Factor (`b`)** | Decreasing: `N - startIndex` | Decreasing: `N - depth` | Number of unvisited candidates at recursion level |

### Memory & Allocation Reality Check
- ❌ **Common Flaw**: Passing new slices or new lists to recursive calls (`dfs(path + [nums[i]])`). This creates `O(N)` heap allocations per call, bloating memory to `O(N * 2^N)` and triggering aggressive Garbage Collection (GC) pressure.
- ✅ **Production Pattern**: Maintain a single pre-allocated mutable list (`List<int>` or Python `list`) and mutate/restore in `O(1)`. Only allocate when cloning solutions at terminal leaves.

---

## 🎙️ Chapter 5: 45-Minute Interview Verbal Script

Use this exact dialogue script to communicate clearly and decisively during technical interviews:

### Phase 1: Clarification & State Space (Minutes 00–05)
> *"We need to generate all valid configurations. Because this is an exhaustive search over discrete choices where order matters / does not matter, I model this as Depth-First Search over an implicit decision tree. At each level of the tree, our choices correspond to the remaining unused elements."*

### Phase 2: State Definition & Invariants (Minutes 05–15)
> *"I will maintain two state variables: a mutable list `path` representing the current candidate prefix, and a boolean array `used` to prevent re-using elements in `O(1)` time. The critical invariant is state symmetry: any modification made to `path` and `used` before entering the recursive branch must be restored immediately upon return."*

### Phase 3: Writing the Choose-Explore-Unchoose Block (Minutes 15–30)
> *"Let's write the recursive helper. The base case triggers when `path.Count == nums.Length`. Here, I must allocate a snapshot copy of `path` because the backing collection will continue mutating. Inside our loop from `0` to `N - 1`, we prune immediately if `used[i]` is true. Then we execute: Choose (`used[i] = true; path.Add(x)`), Explore (`Dfs()`), and Unchoose (`path.RemoveAt(last); used[i] = false`)."*

### Phase 4: Dry Run & Complexity Justification (Minutes 30–45)
> *"Tracing with `[1, 2]`: root calls index 0 (`path=[1]`), recurses to index 1 (`path=[1,2]`), records `[1,2]`, pops `2`, unwinds, pops `1`, and explores branch starting with `2`. The maximum call stack depth is `N`, giving `O(N)` auxiliary space. Time complexity is `O(N * N!)` since there are `N!` leaves, each taking `O(N)` time to copy into results."*

---

## ⚡ Quick Self-Check & Drill

1. **Why does `result.Add(path)` fail in backtracking?**
   *Answer:* It inserts a reference to the mutable list. When backtracking unwinds and clears `path`, all entries inside `result` become empty lists. Always snapshot with `[.. path]` or `path.copy()`.
2. **When should pruning happen: before or after the recursive call?**
   *Answer:* Before the call (`if (condition) continue;`). Checking after wastes stack frame allocation and function invocation overhead.
3. **What is the difference between combination search and permutation search?**
   *Answer:* Combinations maintain a `startIndex` parameter to only advance forward (preventing `[1,2]` and `[2,1]` duplication). Permutations use a `used` boolean array or swap mechanics to explore all orderings.

---

> 🧭 **Navigation:** [🏠 Week Overview](file:///d:/Interview_prep/DSA_102/week_13_backtracking_and_branch_bound/README.md) • [📘 Curriculum Syllabus](file:///d:/Interview_prep/DSA_102/COMPLETE_SYLLABUS.md) • [Next Day →](file:///d:/Interview_prep/DSA_102/week_13_backtracking_and_branch_bound/Week_13_Day_02_Backtracking_Problems_Instructional.md)

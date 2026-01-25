5dxcfv# 🗺️ WEEK_02_PROBLEM_SOLVING_ROADMAP_EXTENDED_CSHARP

**Version:** v1.0  
**Purpose:** Week-specific C# problem-solving playbook  
**Target:** Transform Week 2 linear-structure knowledge into C# coding fluency  
**Prerequisites:** Week 2 instructional files + standard support files complete  

---

## 🎯 WEEK 2 PROBLEM-SOLVING FRAMEWORK

**Week 2 Theme:** Foundations II — Linear Data Structures (arrays, dynamic arrays, linked lists, stacks, queues, binary search).[file:3]

**Decision Tree (Week 2 Patterns):**

| Problem Phrases/Signals                                         | 🎯 Primary Pattern                     | C# Collection / Type                     | Time/Space        |
|-----------------------------------------------------------------|----------------------------------------|------------------------------------------|-------------------|
| "reverse linked list", "middle node", "detect cycle"            | Linked list traversal / fast–slow      | `class ListNode`, `LinkedList<T>`        | O(n) / O(1)       |
| "insert/delete near head or tail many times"                    | Linked list mutation                   | `ListNode`, `LinkedList<T>`              | O(1) local ops    |
| "undo/redo", "nested brackets", "backtracking-like stack state" | Stack usage (LIFO)                     | `Stack<T>`                               | O(n) / O(n)       |
| "process in arrival order", "buffer", "first-come-first-served" | Queue usage (FIFO)                     | `Queue<T>` or `LinkedList<T>`            | O(n) / O(n)       |
| "sorted array", "log n", "first/last occurrence", "boundary"    | Binary search on array                 | `int[]`, `List<int>`                     | O(log n) / O(1)   |
| "sequential processing", "prefix sum", "aggregate over array"   | Linear scan / accumulation             | `T[]`, `List<T>`                         | O(n) / O(1) extra |

**Anti-Patterns (Week 2 specific):**

- ❌ Using `List<T>.RemoveAt(0)` as a queue replacement (O(n) per operation) → use `Queue<T>` or `LinkedList<T>` for O(1) dequeue.
- ❌ Applying binary search to unsorted data → sort first or choose a different pattern.
- ❌ Mutating a linked list inside a `foreach` over `LinkedList<T>` → traverse with explicit `LinkedListNode<T>` and `while` loop instead.

---

## 💻 C# PATTERN IMPLEMENTATIONS (Week 2)

### Pattern 1: Linked List Traversal and Reversal

**C# Mental Model:** A manual `IEnumerator<T>` over nodes; you advance a `current` reference instead of relying on `foreach`, and you explicitly manage node links like a tiny state machine.[file:3]

**When to Use:**

- ✅ You need to inspect each node in a singly or doubly linked list (`Reverse`, `FindMiddle`, `HasCycle`).
- ✅ You must change `Next` pointers (reversal, deletions, list partitioning).

**Core C# Skeleton:**
```
// Singly linked list node example
public class ListNode
{
    public int Value;
    public ListNode Next;
}

// Safe traversal - "tourist" pattern
public void Traverse(ListNode head)
{
    ListNode current = head;
    while (current != null)
    {
        // Process current.Value
        current = current.Next;
    }
}

// Iterative reversal - three-pointer pattern
public ListNode Reverse(ListNode head)
{
    ListNode prev = null;
    ListNode current = head;

    while (current != null)
    {
        ListNode next = current.Next; // 1. Save next
        current.Next = prev;          // 2. Reverse pointer
        prev = current;               // 3. Advance prev
        current = next;               // 4. Advance current
    }

    return prev; // New head
}
```

**C# Notes:**

- Always save `next` *before* overwriting `current.Next` to avoid losing access to the rest of the list.
- Guard pointer chains: never write `current.Next.Next` without checking both `current` and `current.Next` for null.

---

### Pattern 2: Fast–Slow Pointer (Runner Pattern)

**C# Mental Model:** Two runners on a circular track, one moving 1 step per lap (`slow`) and the other 2 steps (`fast`). If a cycle exists, they eventually meet.[file:3]

**When to Use:**

- ✅ Detecting a cycle and its entry point in a linked list.
- ✅ Finding the middle node of a list in a single traversal.

**Core C# Skeleton:**
```
// Detect cycle existence
public bool HasCycle(ListNode head)
{
    ListNode slow = head;
    ListNode fast = head;

    while (fast != null && fast.Next != null)
    {
        slow = slow.Next;           // 1 step
        fast = fast.Next.Next;      // 2 steps

        if (slow == fast)
            return true;
    }

    return false;
}

// Find middle node
public ListNode FindMiddle(ListNode head)
{
    if (head == null) return null;

    ListNode slow = head;
    ListNode fast = head;

    while (fast != null && fast.Next != null)
    {
        slow = slow.Next;           // move one
        fast = fast.Next.Next;      // move two
    }

    return slow; // middle (for even length, second middle)
}
```

**C# Notes:**

- Loop condition must be `while (fast != null && fast.Next != null)`; reversing the order can cause `NullReferenceException`.
- Comparing references (`slow == fast`) checks whether two pointers point to the same node, not just equal values.

---

### Pattern 3: Stack-Based State Tracking (LIFO)

**C# Mental Model:** Similar to the .NET call stack or expression parsers; you push when you encounter an opening construct and pop when you see its closing counterpart.[file:3]

**When to Use:**

- ✅ Validating parentheses/brackets and other nested structures.
- ✅ Simulating undo/redo operations or evaluating expressions.

**Core C# Skeleton:**
```
// Stack pattern: parentheses validation
public bool IsValidParentheses(string s)
{
    if (string.IsNullOrEmpty(s))
        return true;

    var stack = new Stack<char>();

    foreach (char c in s)
    {
        if (c == '(' || c == '{' || c == '[')
        {
            stack.Push(c);
        }
        else
        {
            if (stack.Count == 0)
                return false;

            char open = stack.Pop();
            bool match =
                (open == '(' && c == ')') ||
                (open == '{' && c == '}') ||
                (open == '[' && c == ']');

            if (!match)
                return false;
        }
    }

    return stack.Count == 0;
}
```

**C# Notes:**

- Always check `stack.Count > 0` before `Pop()` or `Peek()` to avoid `InvalidOperationException`.
- Use `Stack<T>` from `System.Collections.Generic` instead of manual arrays; intent is clearer and code is safer.

---

### Pattern 4: Queue-Based FIFO Processing

**C# Mental Model:** Like a web server request queue or message queue; you enqueue work items and process them strictly in first-in-first-out order.[file:3]

**When to Use:**

- ✅ Simulating buffers, scheduling, or breadth-first style traversals over data structures.
- ✅ Any scenario where order of arrival is the processing order.

**Core C# Skeleton:**
```
// Generic queue processing pattern
public void ProcessInOrder<T>(IEnumerable<T> items)
{
    var queue = new Queue<T>();

    foreach (var item in items)
        queue.Enqueue(item);

    while (queue.Count > 0)
    {
        T current = queue.Dequeue();

        // Process current

        // Optionally enqueue derived items:
        // if (condition) queue.Enqueue(newItem);
    }
}
```

**C# Notes:**

- Avoid `List<T>.RemoveAt(0)` for queue semantics; it is O(n) per operation and will degrade performance.
- For multi-producer/multi-consumer scenarios, `ConcurrentQueue<T>` is more appropriate, but that is beyond Week 2 basics.

---

### Pattern 5: Iterative Binary Search on Sorted Arrays

**C# Mental Model:** A more explicit version of `Array.BinarySearch`, where you manage the search interval `[low, high]` and maintain the invariant that the target, if present, is always inside that range.[file:3]

**When to Use:**

- ✅ The data is sorted and constraints clearly point to O(log n).
- ✅ Tasks like "find target index", "first/last occurrence", or "lower/upper bound".

**Core C# Skeleton:**
```
// Classic binary search on sorted array
public int BinarySearch(int[] nums, int target)
{
    if (nums == null || nums.Length == 0)
        return -1;

    int low = 0;
    int high = nums.Length - 1;

    while (low <= high)
    {
        int mid = low + (high - low) / 2; // safe midpoint

        if (nums[mid] == target)
            return mid;

        if (nums[mid] < target)
        {
            low = mid + 1;   // discard left half
        }
        else
        {
            high = mid - 1;  // discard right half
        }
    }

    return -1;
}
```

**C# Notes:**

- Avoid `int mid = (low + high) / 2;` when indices can be large, to prevent integer overflow.
- Before coding, write down the invariant: "target, if present, is in `[low, high]`" and check it after each update to `low` and `high`.

---

## 📊 PROGRESSIVE PROBLEM LADDER (Week 2)

### 🟢 Stage 1: Canonical Problems (Core Template Drills)

| # | LeetCode | Difficulty | Pattern                          | C# Focus                                                           |
|---|----------|------------|----------------------------------|--------------------------------------------------------------------|
| 1 | 206      | 🟢 Easy    | Linked list traversal + reverse | Three-pointer reversal; null-safe pointer handling                 |
| 2 | 141      | 🟢 Easy    | Fast–slow on linked list        | Proper `fast != null && fast.Next != null` condition               |
| 3 | 20       | 🟢 Easy    | Stack-based parentheses check   | `Stack<char>`, safe `Pop()`, clean mapping of pairs                |
| 4 | 704      | 🟢 Easy    | Binary search on array          | Correct while condition, midpoint computation, returning -1        |

---

### 🟡 Stage 2: Variations (Template Adaptation)

| # | LeetCode | Difficulty | Pattern + Twist                        | C# Focus                                                             |
|---|----------|------------|----------------------------------------|----------------------------------------------------------------------|
| 5 | 876      | 🟢 Easy    | Fast–slow for middle node             | Reusing runner skeleton; returning `ListNode` instead of a boolean   |
| 6 | 21       | 🟢 Easy    | Dummy head + pointer merge on lists   | Managing `next` pointers with a dummy node to simplify edge cases    |
| 7 | 232      | 🟢 Easy    | Queue using two stacks                | Encapsulating two `Stack<int>` in a class with clean API             |
| 8 | 278      | 🟡 Medium  | Binary search on a monotonic predicate| Abstracting predicate (`IsBadVersion`) and handling first-true index |

---

### 🟠 Stage 3: Integration (Combining Patterns)

| #  | LeetCode | Difficulty | Patterns Combined                        | C# Focus                                                                    |
|----|----------|------------|------------------------------------------|-----------------------------------------------------------------------------|
| 9  | 142      | 🟡 Medium  | Fast–slow + linked list pointer math     | Reusing meeting point to find cycle start; careful reference comparisons    |
| 10 | 234      | 🟡 Medium  | Fast–slow + half reversal / stack        | Reversing only second half or using `Stack<int>`; restoring list optionally |
| 11 | 88       | 🟢 Easy    | Two-pointer merge (Week 2 + later link)  | In-place merge with descending indices to avoid overwriting                 |

---

## ⚠ WEEK 2 PITFALLS & C# GOTCHAS

| Pattern                | Common Bug                                            | C# Symptom                | Quick Fix                                                             |
|------------------------|-------------------------------------------------------|---------------------------|------------------------------------------------------------------------|
| Linked list traversal  | Accessing `current.Next` when `current` is null      | `NullReferenceException`  | Always check `current != null` before reading `current.Next`          |
| Linked list reversal   | Losing reference to the rest of the list             | Truncated list            | Save `next` in a local variable before overwriting `current.Next`     |
| Fast–slow pointers     | Wrong loop condition (e.g., `fast.Next.Next` only)   | `NullReferenceException`  | Use `while (fast != null && fast.Next != null)` consistently          |
| Stack usage            | `Pop()` on an empty stack                            | `InvalidOperationException` | Guard with `if (stack.Count == 0) return false;` or similar        |
| Queue usage            | Using `List<T>.RemoveAt(0)` as queue                 | Performance degradation   | Replace with `Queue<T>` or `LinkedList<T>` front-removal              |
| Binary search          | Not updating `low`/`high` correctly                  | Infinite loop or wrong index | Confirm the search interval strictly shrinks each iteration       |
| Array loops            | Using `i <= array.Length - 1` plus off-by-one errors | `IndexOutOfRangeException` | Prefer `for (int i = 0; i < array.Length; i++)` consistently       |

**Week 2 Collection Guide:**

- ✅ `int[]`, `List<int>`: For contiguous data, index-based operations, and binary search.
- ✅ `class ListNode`, `LinkedList<T>`: For pointer-heavy operations, frequent inserts/deletes in the middle or at ends.
- ✅ `Stack<T>`: For nested structure validation, reverse-order consumption, and limited backtracking.
- ✅ `Queue<T>`: For first-in-first-out processing, buffering, and breadth-wise logic.

---

## ✅ WEEK 2 COMPLETION CHECKLIST

**Pattern Fluency:**

- [ ] Can write iterative linked list reversal from memory without referencing code.
- [ ] Can implement fast–slow pointer patterns (cycle detection, middle node) without confusion.
- [ ] Can code binary search correctly on the first attempt, including edge cases.

**Problem Solving:**

- [ ] Solved all Stage 1 canonical problems in ≤ 20 minutes each.
- [ ] Solved at least 3 of the Stage 2 variation problems without external help.
- [ ] Attempted at least 2 Stage 3 integration problems and identified the combined patterns.

**C# Implementation:**

- [ ] Used `Stack<T>` / `Queue<T>` instead of misusing `List<T>` for LIFO/FIFO behavior.
- [ ] Avoided `NullReferenceException` and `InvalidOperationException` by guarding pointer and collection operations.
- [ ] Wrote clear, idiomatic C# code with meaningful names and minimal unnecessary allocations.

**Ready:** [ ] Yes   [ ] Need more practice on: _____________________________

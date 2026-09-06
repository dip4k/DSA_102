# 🗺️ Week_02_Extended_CSharp_Problem_Solving_Implementation — COMPLETE v13

**Week:** 2 – Foundations II: Linear Data Structures & Binary Search  
**Purpose:** Master arrays, dynamic arrays, linked lists, stacks, queues, and binary search through pattern recognition, understanding, and practice  
**Target:** Transform Week 2 topics into interview-ready C# coding skills  
**Prerequisites:** Week 2 instructional files + standard support files complete

---

## 📚 WEEK 2 OVERVIEW

**Primary Goal:** Internalize arrays, dynamic arrays, linked lists, stacks, queues, both conceptually and in memory. Understand binary search as robust invariant-based pattern.

**Topics by Day:**
- **Day 1:** Arrays & Memory Layout (static arrays, contiguous memory, cache-friendly access)
- **Day 2:** Dynamic Arrays & Amortized Growth (`List<T>` resizing, doubling strategy)
- **Day 3:** Linked Lists (singly/doubly, pointer operations, O(1) insert/delete)
- **Day 4:** Stacks, Queues & Deques (LIFO/FIFO, circular buffers)
- **Day 5:** Binary Search & Invariants (maintain `[low, high]`, avoid overflow)

---

## SECTION 1️⃣: PATTERN RECOGNITION FRAMEWORK

### 🎯 Decision Tree — How to Identify & Choose Week 2 Patterns

Use this decision tree when you encounter a problem in Week 2:

| 🔍 Problem Phrases/Signals | 🎯 Pattern Name | ❓ Why This Pattern? | 💻 C# Collection | ⏱️ Time/Space |
|---|---|---|---|---|
| "Reverse list", "Iterate backwards", "Traverse linked list", "Modify pointers" | **Linked List Traversal & Manipulation** | Direct `.Next` pointer control; O(1) local ops at known node | `class ListNode`, `LinkedList<T>` | O(n) / O(1) |
| "Middle node", "Detect cycle", "Fast/slow", "Kth from end", "Partition" | **Fast-Slow Pointer (Runner)** | Two pointers at different speeds: meet if cycle, slow at middle when fast ends | `ListNode` manually | O(n) / O(1) |
| "Undo/redo", "Nested brackets", "Expression evaluation", "Backtracking", "DFS" | **Stack (LIFO)** | Last-in-first-out matches state rollback; call stack analogy | `Stack<T>` or `List<T>` | O(n) / O(n) |
| "Process in order", "Buffering", "FIFO", "BFS", "Scheduling", "Circular buffer" | **Queue (FIFO)** | First-in-first-out for temporal ordering; circular array O(1) all ops | `Queue<T>` or `LinkedList<T>` | O(n) / O(n) |
| "Both ends", "Deque", "Sliding window", "Monotonic queue", "Palindrome check" | **Deque (Double-Ended Queue)** | Access front AND back O(1); generalizes stack & queue | `Deque<T>` (custom) or `LinkedList<T>` | O(n) / O(n) |
| "Sorted array", "Log n", "First/last occurrence", "Boundary", "Answer space" | **Binary Search (Invariant-Based)** | Maintain `[low, high]` invariant; halve space each iteration | `int[]`, `List<int>` | O(log n) / O(1) |
| "Capacity needed", "Dynamic growth", "Append many", "Shrink-to-fit", "Amortized" | **Dynamic Array Management** | Doubling capacity; amortized O(1) append over many operations | `List<T>` | O(1) amortized / O(n) space |

**HOW TO READ THIS:**
- See "[Signal X]" in a problem? IMMEDIATELY think "[Pattern Y]"
- Ask yourself: "Why does this pattern solve it?" → Internalize the reasoning
- Check recommended collection → Learn why it's optimal for this case

---

## SECTION 2️⃣: ANTI-PATTERNS — WHEN NOT TO USE & WHAT FAILS

### ⚠️ Week 2 Common Mistakes & Correct Alternatives

Learn WHAT NOT TO DO and WHY:

| ❌ Wrong Approach | 💥 Why It Fails | ⚡ Symptom/Consequence | ✅ Correct Alternative |
|---|---|---|---|
| Using `List<T>.RemoveAt(0)` as queue | O(n) per operation: array shift required | "My queue is slow!" → O(n²) total instead of O(n) | Use `Queue<T>` for O(1) dequeue or `LinkedList<T>` |
| Applying binary search to UNSORTED array | Violates precondition; algorithm's invariant breaks | Silent wrong answer returned (hardest bug to find) | Sort first OR use linear scan if order matters |
| Mutating `LinkedList<T>` inside `foreach` loop | Iterator invalidation; unpredictable behavior | Skipped nodes, exceptions, undefined state | Use explicit `while` with `LinkedListNode<T>` reference |
| Reversing linked list without saving `next` | Lost reference to rest of list after `current.Next = prev` | Cannot traverse remaining nodes (appears to crash) | **ALWAYS:** `next = current.Next` BEFORE `current.Next = ...` |
| Using array index in linked list operations | Linked lists have O(n) index access, not O(1) | Single check becomes O(n); nested loop becomes O(n²) catastrophe | Keep explicit node references; advance pointers incrementally |
| Not guarding `head` null in linked list | Null reference exception on first operation | `NullReferenceException` at line 1 of function | Guard: `if (head == null) return default;` first |
| Choosing `Array` when size grows frequently | Reallocation on overflow, garbage of old array | Performance spikes under growth load; memory waste | Use `List<T>` for dynamic; reserve capacity if predictable |
| `mid = (low + high) / 2` in binary search | Integer overflow on large values | Wrong mid or crash on `int.MaxValue` arrays | Use: `mid = low + (high - low) / 2` |
| Off-by-one errors in binary search | Infinite loop or skipped boundary | Hangs or returns wrong boundary result | Test with `[1]`, `[1,2]`, boundaries explicitly |

**ANTI-PATTERN LESSON:**
Understand not just the pattern, but understand its boundaries.
When you see someone use [Mistake], explain why [Alternative] is better.

---

## SECTION 3️⃣: PATTERN IMPLEMENTATIONS (Production-Grade)

### Pattern 1: Linked List Traversal & Reversal

#### 🧠 Mental Model
A manual `IEnumerator<T>` over nodes; you advance a `current` reference by following `.Next` pointers instead of relying on `foreach` or indices. You explicitly manage node links like a tiny state machine. Three-pointer reversal: `prev`, `current`, `next` form a bubble that swaps direction as it moves.

#### ✅ When to Use This Pattern
- ✅ Reversing a singly linked list (pointer direction inversion).
- ✅ Detecting and removing cycles in linked lists.
- ✅ Rearranging linked list partitions (split around value).
- ✅ When you need O(1) insert/delete at a KNOWN node.

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
using System;

/// <summary>
/// Singly Linked List Node - Foundation
/// </summary>
public class ListNode
{
    public int Value;
    public ListNode Next;
    
    public ListNode(int value = 0, ListNode next = null)
    {
        Value = value;
        Next = next;
    }
}

/// <summary>
/// Traverse singly linked list - Safe visitor pattern
/// Time: O(n) | Space: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// Move a cursor (`current`) from head to null,
/// processing each node's value without modification.
/// </summary>
public void TraverseLinkedList(ListNode head)
{
    // STEP 1: Guard - handle empty list
    if (head == null) return;
    
    // STEP 2: Initialize - start at head
    ListNode current = head;
    
    // STEP 3: Core loop - follow .Next pointers
    while (current != null)
    {
        // Process current node value
        Console.WriteLine($"Node: {current.Value}");
        
        // Advance to next
        current = current.Next;
    }
    // Naturally terminates when current == null
}

/// <summary>
/// Reverse singly linked list - Three-pointer pattern
/// Time: O(n) | Space: O(1) extra (in-place)
/// 
/// 🧠 MENTAL MODEL:
/// Imagine three runners: [prev] [current] [next]
/// Each step: save next BEFORE breaking link,
/// reverse pointer (current.Next ← prev), advance all three.
/// Animation: null ← 1 → 2 → 3 becomes 1 ← 2 ← 3, return 3 as head.
/// </summary>
public ListNode ReverseLinkedList(ListNode head)
{
    // STEP 1: Guard - empty or single node, already "reversed"
    if (head == null || head.Next == null) return head;
    
    // STEP 2: Initialize three pointers
    ListNode prev = null;      // No predecessor initially
    ListNode current = head;   // Start at head
    ListNode next = null;      // Temporary storage
    
    // STEP 3: Core reversal loop
    while (current != null)
    {
        // Save next BEFORE link modification (CRITICAL!)
        // Without this, we lose the rest of the list.
        next = current.Next;
        
        // Reverse the pointer: current.Next now points backwards to prev
        current.Next = prev;
        
        // Advance: prev and current move one step forward
        prev = current;
        current = next;
        
        // Visual sequence:
        // Before iteration N:    prev → current → next → ...
        // After iteration N:     current ← prev,  current → next → ...
    }
    
    // After loop: prev points to what was the last node (new head)
    return prev;
}

/// <summary>
/// Find middle node of linked list (fast-slow integration)
/// Time: O(n) single pass | Space: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// Two runners on a track: slow moves 1 step/iter,
/// fast moves 2 steps/iter. When fast reaches end,
/// slow is at middle. Works because:
/// - If length is odd, slow stops at exact middle.
/// - If even, slow stops at first node of second half.
/// </summary>
public ListNode FindMiddle(ListNode head)
{
    // STEP 1: Guard - empty or single node
    if (head == null || head.Next == null) return head;
    
    // STEP 2: Initialize two pointers
    ListNode slow = head;   // Moves 1 step/iteration
    ListNode fast = head;   // Moves 2 steps/iteration
    
    // STEP 3: Move pointers until fast reaches end
    // Guard: check fast.Next before accessing fast.Next.Next
    while (fast != null && fast.Next != null)
    {
        slow = slow.Next;       // 1 step
        fast = fast.Next.Next;  // 2 steps
    }
    
    // When fast is null or fast.Next is null,
    // slow is at middle
    return slow;
}

/// <summary>
/// Delete node at specific position (1-indexed)
/// Time: O(n) | Space: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// Find the node BEFORE target, then skip over target.
/// This requires a previous pointer reference.
/// </summary>
public ListNode DeleteAtPosition(ListNode head, int position)
{
    // STEP 1: Guard - empty list
    if (head == null) return null;
    
    // STEP 2: Special case - deleting head
    if (position == 1) return head.Next;
    
    // STEP 3: Find node BEFORE target
    ListNode current = head;
    int count = 1;
    
    // Advance until we're at node BEFORE position
    while (current != null && current.Next != null && count < position - 1)
    {
        current = current.Next;
        count++;
    }
    
    // STEP 4: Guard - invalid position
    if (current == null || current.Next == null) return head;
    
    // STEP 5: Delete - skip the target node
    current.Next = current.Next.Next;
    
    return head;
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Always save `next = current.Next` BEFORE modifying `current.Next`. Forgetting causes list truncation.
- 🟡 **PERFORMANCE:** Linked list traversal O(n) but slower than arrays due to cache misses (pointer chasing).
- 🟢 **BEST PRACTICE:** Use explicit `while` loops with `ListNode` references. Avoid `foreach` on `LinkedList<T>` when fine-grained pointer control needed.

---

### Pattern 2: Fast-Slow Pointer (Runner Pattern)

#### 🧠 Mental Model
Two runners on a circular track: slow moves 1 step/lap, fast moves 2 steps/lap. If a cycle exists, they must eventually meet. If no cycle, fast reaches null first. This clever technique detects cycles with O(1) space (vs. hash set O(n)).

#### ✅ When to Use This Pattern
- ✅ Detecting cycles in linked lists (Floyd's cycle detection).
- ✅ Finding cycle entry point and cycle length.
- ✅ Finding middle node (use with `FindMiddle` above).
- ✅ Removing Nth node from end (offset gap).

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Detect cycle existence in singly linked list
/// Time: O(n) single pass | Space: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// If cycle exists, pointers MUST meet eventually.
/// Gap closes by 1 each iteration: eventually gap = 0.
/// If no cycle, fast reaches null first (exit via while condition).
/// </summary>
public bool HasCycle(ListNode head)
{
    // STEP 1: Guard - empty or single node (no cycle possible)
    if (head == null || head.Next == null) return false;
    
    // STEP 2: Initialize two pointers at head
    ListNode slow = head;   // Move 1 step
    ListNode fast = head;   // Move 2 steps
    
    // STEP 3: Run until pointers meet or fast reaches end
    while (fast != null && fast.Next != null)
    {
        slow = slow.Next;       // 1 step
        fast = fast.Next.Next;  // 2 steps
        
        // If they meet, cycle detected
        if (slow == fast) return true;
    }
    
    // fast reached end without meeting slow → no cycle
    return false;
}

/// <summary>
/// Find entry point of cycle in linked list
/// Time: O(n) | Space: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// Mathematical insight: distance from head to cycle entry
/// equals distance from meeting point to cycle entry.
/// Proof: When they meet, slow traveled distance D,
/// fast traveled 2D. Both cycled same # times.
/// </summary>
public ListNode FindCycleStart(ListNode head)
{
    // STEP 1: Guard - empty
    if (head == null) return null;
    
    // STEP 2: Detect cycle and find meeting point
    ListNode slow = head;
    ListNode fast = head;
    
    while (fast != null && fast.Next != null)
    {
        slow = slow.Next;
        fast = fast.Next.Next;
        
        if (slow == fast) break;  // Cycle detected
    }
    
    // STEP 3: Guard - no cycle
    if (fast == null || fast.Next == null) return null;
    
    // STEP 4: Find entry point
    // Reset one pointer to head, move both at 1x speed
    ListNode ptr1 = head;
    ListNode ptr2 = slow;  // Meeting point
    
    while (ptr1 != ptr2)
    {
        ptr1 = ptr1.Next;  // From head
        ptr2 = ptr2.Next;  // From meeting point
    }
    
    // They meet at cycle entry
    return ptr1;
}

/// <summary>
/// Remove Nth node from end of list
/// Time: O(n) single pass | Space: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// Two pointers with gap of N nodes.
/// When fast reaches end, slow is at node BEFORE target.
/// Dummy node handles edge case of deleting head.
/// </summary>
public ListNode RemoveNthFromEnd(ListNode head, int n)
{
    // STEP 1: Guard - empty
    if (head == null) return null;
    
    // STEP 2: Dummy node
    // Why? Simplifies "delete head" case.
    // Dummy → head → ... → null
    ListNode dummy = new(0);
    dummy.Next = head;
    
    // STEP 3: Initialize pointers with gap
    ListNode fast = dummy;
    ListNode slow = dummy;
    
    // Advance fast by n+1 positions to create gap of n
    for (int i = 0; i <= n; i++)
    {
        if (fast == null) return head;  // n too large
        fast = fast.Next;
    }
    
    // STEP 4: Move both until fast reaches null
    while (fast != null)
    {
        slow = slow.Next;
        fast = fast.Next;
    }
    
    // STEP 5: Delete - skip the Nth node
    slow.Next = slow.Next.Next;
    
    return dummy.Next;
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Always guard `fast.Next` before accessing `fast.Next.Next`. Null exception otherwise.
- 🟡 **PERFORMANCE:** O(n) single pass with O(1) space beats two-pass + length calculation.
- 🟢 **BEST PRACTICE:** Dummy node simplifies edge cases (delete head). Always use when list modification involved.

---

### Pattern 3: Stack (LIFO) — Expression Evaluation & Backtracking

#### 🧠 Mental Model
Last-In-First-Out (LIFO) semantics: push items on top, pop from top. Think of a plate stack: last plate added is first removed. Used for backtracking, expression evaluation, DFS, and maintaining state on return.

#### ✅ When to Use This Pattern
- ✅ Bracket matching and expression validation.
- ✅ Expression evaluation (infix→postfix conversion).
- ✅ Backtracking (DFS tree traversal, permutations).
- ✅ Monotonic stack (next greater/smaller element).
- ✅ Undo/redo systems (state rollback).

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Valid parentheses matching - Classic stack problem
/// Time: O(n) | Space: O(n) worst case (all open brackets)
/// 
/// 🧠 MENTAL MODEL:
/// Push open brackets. On close bracket, match with top.
/// If mismatch or stack empty on close, invalid.
/// If stack non-empty at end, invalid.
/// </summary>
public bool IsValidParentheses(string s)
{
    // STEP 1: Guard - empty or odd length (impossible to match)
    if (string.IsNullOrEmpty(s) || s.Length % 2 != 0) return false;
    
    // STEP 2: Initialize stack and bracket mapping
    Stack<char> stack = new();
    Dictionary<char, char> matches = new()
    {
        { ')', '(' },
        { ']', '[' },
        { '}', '{' }
    };
    
    // STEP 3: Process each character
    foreach (char c in s)
    {
        if (matches.ContainsKey(c))  // Closing bracket
        {
            // Stack must exist and have matching opening bracket
            if (stack.Count == 0 || stack.Pop() != matches[c])
                return false;
        }
        else  // Opening bracket
        {
            stack.Push(c);
        }
    }
    
    // STEP 4: Stack must be empty at end
    return stack.Count == 0;
}

/// <summary>
/// Monotonic stack - Daily temperatures (next warmer day)
/// Time: O(n) | Space: O(n)
/// 
/// 🧠 MENTAL MODEL:
/// Maintain stack of INDICES in decreasing temperature order.
/// For each day, pop all days cooler than current.
/// Stack stores "indices waiting for warmer day".
/// Each element pushed/popped once → O(n) total.
/// </summary>
public int[] DailyTemperatures(int[] temperatures)
{
    // STEP 1: Guard - empty
    if (temperatures == null || temperatures.Length == 0)
        return new int[0];
    
    // STEP 2: Initialize result and monotonic stack
    int n = temperatures.Length;
    int[] result = new int[n];
    Stack<int> stack = new();  // Stores INDICES, not temperatures
    
    // STEP 3: Process each day
    for (int i = 0; i < n; i++)
    {
        // Pop all days cooler than today
        // Why? They found their warmer day today.
        while (stack.Count > 0 && temperatures[stack.Peek()] < temperatures[i])
        {
            int coolDayIdx = stack.Pop();
            result[coolDayIdx] = i - coolDayIdx;  // Days until warmer
        }
        
        // Push current day (waiting for warmer day or never found)
        stack.Push(i);
    }
    
    // Remaining stack elements never found warmer day (result stays 0)
    return result;
}

/// <summary>
/// Evaluate Reverse Polish Notation (postfix expression)
/// Time: O(n) | Space: O(n)
/// 
/// 🧠 MENTAL MODEL:
/// Postfix: operands FIRST, operator SECOND.
/// Algorithm: push operands, pop two and apply on operator.
/// Example: ["3","4","+"] → push 3, push 4, operator +,
///          pop 4 and 3, compute 3+4, push 7.
/// </summary>
public int EvalRPN(string[] tokens)
{
    // STEP 1: Guard
    if (tokens == null || tokens.Length == 0) return 0;
    
    // STEP 2: Initialize stack
    Stack<int> stack = new();
    HashSet<string> operators = new() { "+", "-", "*", "/" };
    
    // STEP 3: Process each token
    foreach (string token in tokens)
    {
        if (operators.Contains(token))
        {
            // Guard: must have at least 2 operands
            if (stack.Count < 2)
                throw new InvalidOperationException("Invalid RPN");
            
            // Pop two operands (ORDER MATTERS for - and /)
            int b = stack.Pop();  // Second operand (top)
            int a = stack.Pop();  // First operand (below top)
            
            // Apply operator
            int result = token switch
            {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                "/" => a / b,  // Truncates toward zero
                _ => throw new InvalidOperationException("Unknown operator")
            };
            
            stack.Push(result);
        }
        else
        {
            // It's a number - push to stack
            stack.Push(int.Parse(token));
        }
    }
    
    // Final stack must have exactly one element (result)
    return stack.Count == 1 ? stack.Pop() : 0;
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Always check `stack.Count > 0` before `stack.Pop()`. Exception otherwise.
- 🟡 **PERFORMANCE:** Monotonic stack is O(n) because each element pushed/popped exactly once, NOT O(n²).
- 🟢 **BEST PRACTICE:** Use `Stack<T>` not `List<T>` for clarity (LIFO intent is explicit).

---

### Pattern 4: Queue (FIFO) — BFS & Buffering

#### 🧠 Mental Model
First-In-First-Out (FIFO) semantics: enqueue at back, dequeue from front. Think of a line at a store: first person in line is first to leave. Used for breadth-first search, level-order traversal, and temporal ordering (scheduling, buffering).

#### ✅ When to Use This Pattern
- ✅ Breadth-first search (BFS) on trees/graphs.
- ✅ Level-order (level-by-level) tree traversal.
- ✅ Circular buffering with FIFO constraint.
- ✅ Processes requiring "arrival order" semantics (scheduling).

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
using System.Collections.Generic;

/// <summary>
/// Binary tree node for BFS examples
/// </summary>
public class TreeNode
{
    public int Value;
    public TreeNode Left;
    public TreeNode Right;
    
    public TreeNode(int value = 0, TreeNode left = null, TreeNode right = null)
    {
        Value = value;
        Left = left;
        Right = right;
    }
}

/// <summary>
/// Level-order (BFS) traversal - Queue-based
/// Time: O(n) all nodes visited | Space: O(w) where w = max level width
/// 
/// 🧠 MENTAL MODEL:
/// Queue stores nodes to process.
/// Process ALL nodes at current level,
/// enqueue their children for next level.
/// Result: level-by-level (breadth-first) traversal.
/// </summary>
public List<List<int>> LevelOrder(TreeNode root)
{
    // STEP 1: Guard - empty tree
    List<List<int>> result = new();
    if (root == null) return result;
    
    // STEP 2: Initialize queue
    Queue<TreeNode> queue = new();
    queue.Enqueue(root);
    
    // STEP 3: Process level by level
    while (queue.Count > 0)
    {
        // Process ALL nodes at current level
        int levelSize = queue.Count;
        List<int> currentLevel = new();
        
        for (int i = 0; i < levelSize; i++)
        {
            TreeNode node = queue.Dequeue();
            currentLevel.Add(node.Value);
            
            // Enqueue children for NEXT level
            if (node.Left != null) queue.Enqueue(node.Left);
            if (node.Right != null) queue.Enqueue(node.Right);
        }
        
        result.Add(currentLevel);
    }
    
    return result;
}

/// <summary>
/// Circular queue implementation - Array-based, efficient
/// Time: All operations O(1) | Space: O(capacity)
/// 
/// 🧠 MENTAL MODEL:
/// Linear array with wraparound logic using modulo.
/// front and rear indices cycle around using `% capacity`.
/// Empty: front == rear. Full: (rear + 1) % capacity == front.
/// This avoids reallocation; perfect for fixed-size buffers.
/// </summary>
public class CircularQueue
{
    private int[] queue;
    private int front = 0;
    private int rear = 0;
    private int count = 0;
    private int capacity;
    
    public CircularQueue(int capacity)
    {
        this.capacity = capacity;
        queue = new int[capacity];
    }
    
    public void Enqueue(int value)
    {
        // STEP 1: Guard - full
        if (count == capacity)
            throw new InvalidOperationException("Queue is full");
        
        // STEP 2: Add at rear, wrap around using modulo
        queue[rear] = value;
        rear = (rear + 1) % capacity;
        count++;
    }
    
    public int Dequeue()
    {
        // STEP 1: Guard - empty
        if (count == 0)
            throw new InvalidOperationException("Queue is empty");
        
        // STEP 2: Remove from front, wrap around using modulo
        int value = queue[front];
        front = (front + 1) % capacity;
        count--;
        return value;
    }
    
    public bool IsEmpty() => count == 0;
    public bool IsFull() => count == capacity;
    public int Size() => count;
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Use `Queue<T>` not `List<T>.RemoveAt(0)` for queues. RemoveAt(0) is O(n) due to array shift.
- 🟡 **PERFORMANCE:** Circular queue O(1) all operations with fixed space; better than linked list for bounded buffers.
- 🟢 **BEST PRACTICE:** When doing BFS level-order, count queue size at start of loop to properly delineate levels.

---

### Pattern 5: Binary Search — Invariant-Based Robust Searching

#### 🧠 Mental Model
Maintain an invariant: if target exists, it lies within `[low, high]`. Each iteration, narrow search space by half by moving the boundary that violates the invariant. Terminate when `low > high` or found. The key insight: always move boundaries to maintain invariant; never break it.

#### ✅ When to Use This Pattern
- ✅ Searching in sorted arrays (exact match, first/last occurrence).
- ✅ Binary search on answer space (monotone feasibility condition).
- ✅ Finding boundaries (lower_bound, upper_bound).
- ✅ Peak finding, rotated array search, minimize/maximize problems.

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Standard binary search - Find exact match
/// Time: O(log n) | Space: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// Invariant: If target exists, it's in [low, high].
/// Each iteration: check mid.
/// If mid < target, search right (low = mid + 1).
/// If mid > target, search left (high = mid - 1).
/// If mid == target, found!
/// </summary>
public int BinarySearch(int[] nums, int target)
{
    // STEP 1: Guard - empty array
    if (nums == null || nums.Length == 0) return -1;
    
    // STEP 2: Initialize boundaries
    int low = 0;
    int high = nums.Length - 1;
    
    // STEP 3: Search loop - maintain invariant
    while (low <= high)
    {
        // CRITICAL: Avoid overflow with int.MaxValue
        // Use: low + (high - low) / 2, NOT (low + high) / 2
        int mid = low + (high - low) / 2;
        
        if (nums[mid] == target)
            return mid;  // Found!
        else if (nums[mid] < target)
            low = mid + 1;  // Target in right half
        else
            high = mid - 1;  // Target in left half
    }
    
    // STEP 4: Not found
    return -1;
}

/// <summary>
/// Find first occurrence of target in sorted array
/// Time: O(log n) | Space: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// When found, continue searching LEFT to find FIRST occurrence.
/// Shift high left even on match (collect smaller indices).
/// </summary>
public int FindFirst(int[] nums, int target)
{
    // STEP 1: Guard
    if (nums == null || nums.Length == 0) return -1;
    
    // STEP 2: Initialize boundaries
    int low = 0;
    int high = nums.Length - 1;
    int result = -1;
    
    // STEP 3: Search loop (collect leftmost match)
    while (low <= high)
    {
        int mid = low + (high - low) / 2;
        
        if (nums[mid] == target)
        {
            result = mid;
            high = mid - 1;  // Continue searching LEFT for earlier occurrence
        }
        else if (nums[mid] < target)
            low = mid + 1;
        else
            high = mid - 1;
    }
    
    return result;
}

/// <summary>
/// Find last occurrence of target in sorted array
/// Time: O(log n) | Space: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// When found, continue searching RIGHT to find LAST occurrence.
/// Shift low right even on match (collect larger indices).
/// </summary>
public int FindLast(int[] nums, int target)
{
    // STEP 1: Guard
    if (nums == null || nums.Length == 0) return -1;
    
    // STEP 2: Initialize boundaries
    int low = 0;
    int high = nums.Length - 1;
    int result = -1;
    
    // STEP 3: Search loop (collect rightmost match)
    while (low <= high)
    {
        int mid = low + (high - low) / 2;
        
        if (nums[mid] == target)
        {
            result = mid;
            low = mid + 1;  // Continue searching RIGHT for later occurrence
        }
        else if (nums[mid] < target)
            low = mid + 1;
        else
            high = mid - 1;
    }
    
    return result;
}

/// <summary>
/// Binary search on answer space - Capacity planning (Ship packages)
/// Time: O(log(sum) * n) | Space: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// NOT searching in array, but in ANSWER SPACE [min, max].
/// For each candidate answer, check if FEASIBLE (predicate).
/// Binary search answer space until minimum feasible found.
/// Example: Find minimum ship capacity to deliver all packages within days.
/// </summary>
public int ShipWithinDays(int[] weights, int days)
{
    // STEP 1: Guard
    if (weights == null || weights.Length == 0) return 0;
    
    // STEP 2: Bounds of answer space
    int low = weights.Max();  // At least carry heaviest package
    int high = weights.Sum();  // Carry all in one trip
    
    // STEP 3: Binary search for minimum feasible capacity
    while (low < high)
    {
        int mid = low + (high - low) / 2;
        
        // Check: Can we deliver with capacity = mid in time?
        if (CanShip(weights, mid, days))
        {
            high = mid;  // Try smaller capacity
        }
        else
        {
            low = mid + 1;  // Need larger capacity
        }
    }
    
    return low;
}

private bool CanShip(int[] weights, int capacity, int days)
{
    int daysNeeded = 1;
    int currentLoad = 0;
    
    foreach (int weight in weights)
    {
        // If adding this weight exceeds capacity, start new day
        if (currentLoad + weight > capacity)
        {
            daysNeeded++;
            currentLoad = weight;
            
            // Infeasible: can't meet deadline
            if (daysNeeded > days) return false;
        }
        else
        {
            currentLoad += weight;
        }
    }
    
    return true;  // Feasible: all shipped within deadline
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Use `mid = low + (high - low) / 2` ALWAYS. `(low + high) / 2` overflows on large arrays.
- 🟡 **PERFORMANCE:** O(log n) vs O(n) is massive speedup. 1M elements → ~20 operations.
- 🟢 **BEST PRACTICE:** Explicitly state invariant. Test edge cases: `[1]`, `[1,1]`, boundaries.

---

## SECTION 4️⃣: C# Collection Decision GUIDE

### When to Use Each Collection for Week 2 Patterns

| Use Case | Collection | Why? | When NOT to Use | Alternative |
|---|---|---|---|---|
| Fixed size, fastest random access, cache-friendly | `int[]` (array) | O(1) index, contiguous memory, CPU prefetching | Size grows dynamically | `List<T>` |
| Dynamic size, frequent append, random access | `List<T>` | Resizable array, O(1) amortized append | Frequent mid-inserts (expensive) | `LinkedList<T>` |
| Frequent O(1) insert/delete at head or tail | `LinkedList<T>` | O(1) ops at boundaries (no shift needed) | Random access needed (O(n) per index) | `List<T>` |
| LIFO semantics (undo/redo, expression eval) | `Stack<T>` | Push/pop clarity, LIFO guarantee | FIFO needed | `Queue<T>` |
| FIFO semantics (BFS, scheduling, buffers) | `Queue<T>` | Enqueue/dequeue clarity, FIFO guarantee | LIFO needed | `Stack<T>` |
| Manual linked list with pointer control | `class ListNode` | Direct `.Next` manipulation for reversal/cycles | Just traversing | `LinkedList<T>` |
| Fixed-size circular buffer (producer/consumer) | Custom `CircularQueue` | O(1) all ops, no reallocation, fixed memory | Dynamic queue | `Queue<T>` |

**KEY INSIGHT:**
Choosing the right collection is as important as choosing the right pattern.
Wrong collection = Correct algorithm running at wrong complexity.

---

## SECTION 5️⃣: PROGRESSIVE PROBLEM LADDER

### 🎯 Strategy: Solve Problems in Stages

**Stage 1 (Green):** Master core pattern skeleton  
**Stage 2 (Yellow):** Recognize pattern variations and edge cases  
**Stage 3 (Red):** Combine multiple patterns for complex problems

---

### 🟢 STAGE 1: CANONICAL PROBLEMS — Master Core Pattern

Solve these to cement the pattern. Can you code the skeleton without looking?

| # | LeetCode # | Difficulty | Pattern | C# Focus | Concept |
|---|---|---|---|---|---|
| 1 | #206 | 🟢 Easy | Linked List Reversal | Manual `.Next` manipulation | Pointer reversal 3-pointer pattern |
| 2 | #141 | 🟢 Easy | Fast-Slow Pointer | Cycle detection basics | Two-pointer with different speeds |
| 3 | #20 | 🟢 Easy | Stack (LIFO) | `Stack<T>` basic push/pop | Bracket matching |
| 4 | #225 | 🟢 Easy | Stack Implementation | Implement using `List<T>` | Understand push/pop internals |
| 5 | #232 | 🟢 Easy | Queue Implementation | Implement using `Stack<T>` | FIFO semantics |
| 6 | #704 | 🟢 Easy | Binary Search | Basic `[low, high]` invariant | Standard mid = low + (high-low)/2 |
| 7 | #278 | 🟢 Easy | Binary Search Variant | First bad version (monotone boundary) | Left boundary search, `mid = low+(high-low)/2` |

**STAGE 1 GOAL:** Pattern fluency. Can you implement each skeleton in < 5 minutes from memory?

---

### 🟡 STAGE 2: VARIATIONS — Recognize Pattern Boundaries

These problems twist the pattern. When does it work? When does it break?

| # | LeetCode # | Difficulty | Pattern + Twist | C# Focus | When Pattern Breaks |
|---|---|---|---|---|---|
| 1 | #92 | 🟡 Medium | Linked List Reversal (partial segment) | Reverse only [left, right] | Must track left/right boundaries, reconnect |
| 2 | #142 | 🟡 Medium | Fast-Slow Pointer (cycle entry) | Find cycle START point | Extra math needed after detecting cycle |
| 3 | #19 | 🟡 Medium | Two-Pointer (remove Nth from end) | Remove Nth node; handle delete head | Dummy node needed for edge case |
| 4 | #155 | 🟡 Medium | Stack with Extra State | Min stack tracks minimum | Stack stores pairs, not single values |
| 5 | #150 | 🟡 Medium | Stack Application (RPN) | Evaluate RPN postfix expression | Operator precedence no longer applies |
| 6 | #33 | 🟡 Medium | Binary Search Variant (rotated) | Search in rotated sorted array | Pivot location determines which half is sorted |
| 7 | #153 | 🟡 Medium | Binary Search Variant (find min) | Find minimum in rotated array | Edge case: [3,1,3] vs [3,1,1] differ |

**STAGE 2 GOAL:** Pattern boundaries. When do variations apply? When is core pattern insufficient?

---

### 🟠 STAGE 3: INTEGRATION — Combine Patterns

Hard problems rarely use just one pattern. These combine multiple:

| # | LeetCode # | Difficulty | Patterns Required | C# Focus | Pattern Combination Logic |
|---|---|---|---|---|---|
| 1 | #146 | 🔴 Hard | Hash + Linked List | LRU Cache | Hash for O(1) lookup + LL for order (MRU/LRU) |
| 2 | #23 | 🔴 Hard | Linked List + Heap | Merge K sorted lists | Heap to efficiently find minimum node |
| 3 | #25 | 🔴 Hard | Linked List + Group | Reverse nodes in K group | Reverse partial list + reconnect groups |

**STAGE 3 GOAL:** Real-world thinking. Professional problems combine multiple patterns.

---

## SECTION 6️⃣: WEEK 2 PITFALLS & C# GOTCHAS

### 🐛 Runtime Issues & Collection Pitfalls

Common bugs you'll hit and how to fix them:

| ❌ Pattern | 🐛 Bug | 💥 C# Symptom | 🔧 Quick Fix |
|---|---|---|---|
| Linked List Reversal | Not saving `next` before mutation | Lost references; rest of list unreachable | **ALWAYS:** `ListNode next = current.Next;` before `current.Next = ...` |
| Fast-Slow Pointer | Not guarding `fast.Next` | `NullReferenceException` on `fast.Next.Next` | Use: `while (fast != null && fast.Next != null)` |
| Stack | Pop from empty stack | `InvalidOperationException` | Always: `if (stack.Count > 0)` before `stack.Pop()` |
| Queue | Using `List<T>.RemoveAt(0)` for queue | O(n) per dequeue → O(n²) total | Use: `Queue<T>` for O(1) or `LinkedList<T>` |
| Binary Search | `(low + high) / 2` for mid | Integer overflow on large arrays | Use: `mid = low + (high - low) / 2` |
| Binary Search | Off-by-one with `mid+1`/`mid-1` | Infinite loop or miss boundary | Test: `[1]`, `[1,2]`, `[1,1,1]`, boundaries explicitly |
| Dynamic Array | Frequent `List<T>.Insert(0, x)` | O(n) per insert → O(n²) total | Use `LinkedList<T>` for frequent head inserts |
| Array Index | Accessing `arr[-1]` or `arr[length]` | `IndexOutOfRangeException` | Always bounds-check: `0 <= index < length` |

### 🎯 Week 2 Collection Gotchas

These mistakes are EASY to make:

- ❌ `List<T>.RemoveAt(0)` for queue operations → O(n) per dequeue instead of O(1) → Use `Queue<T>`
  - Concrete: Removing 1000 items from position 0 is O(1M) = 1,000,000 ops instead of 1,000

- ❌ Modifying `LinkedList<T>` while enumerating with `foreach` → Iterator invalidation → Use explicit `while` with `LinkedListNode<T>`
  - Code: `foreach (var node in linkedList) { linkedList.Remove(node); }` breaks iterator

- ❌ Not checking `null` on linked list head → `NullReferenceException` → Guard: `if (head == null) return default;` first
  - Effect: Empty linked list crashes on line 1 of function

- ❌ Binary search on UNSORTED array → Wrong answers silently returned → Sort first or linear search
  - Example: BS on `[3,1,4,1,5]` returns incorrect result silently

- ❌ Array index out of bounds in binary search → `IndexOutOfRangeException` → Always: `mid = low + (high-low)/2`
  - Overflow: `mid = (int.MaxValue + int.MaxValue) / 2` wraps; safe version doesn't

- ❌ `(low + high) / 2` overflow on large values → Integer wraparound → Use: `mid = low + (high - low) / 2`
  - Math: On int.MaxValue + 1, integer wraps to int.MinValue (disaster)

---

## SECTION 7️⃣: QUICK REFERENCE — INTERVIEW PREPARATION

### Mental Models for Fast Recall (30 seconds before interview!)

| Pattern | 1-Liner Mental Model | Code Symbol | When You See This... |
|---|---|---|---|
| **Linked List Reversal** | "Save next, reverse pointer, advance three pointers" | `next = curr.Next; curr.Next = prev; prev = curr; curr = next;` | "Reverse list", "Flip links", "Iterate backwards" |
| **Fast-Slow Pointer** | "Two runners, different speeds, meet if cycle exists" | `slow = slow.Next; fast = fast.Next.Next;` | "Detect cycle", "Find middle", "Kth from end" |
| **Stack (LIFO)** | "Last-in-first-out; push on top, pop from top" | `stack.Push(x); var x = stack.Pop();` | "Brackets", "Undo/redo", "Expression", "DFS state" |
| **Queue (FIFO)** | "First-in-first-out; enqueue back, dequeue front" | `queue.Enqueue(x); var x = queue.Dequeue();` | "BFS", "Level-order", "Buffering", "Scheduling" |
| **Binary Search** | "Maintain [low, high] invariant; halve each iteration" | `mid = low + (high - low) / 2;` | "Sorted array", "Log n", "Boundary", "Answer space" |
| **Dynamic Array** | "Doubling capacity when full; amortized O(1) append" | `list.Add(x); // O(1) amortized` | "Append many", "Dynamic sizing", "List<T>" |

---

## ✅ WEEK 2 COMPLETION CHECKLIST

### Pattern Fluency — Can You Recognize & Choose?

- [ ] Recognize Linked List Traversal by signals ("reverse", "iterate", "traverse")
- [ ] Recall Linked List Reversal 3-pointer skeleton without notes (< 2 min)
- [ ] Explain WHY reversal beats alternatives (in-place, O(1) space)
- [ ] Explain WHEN reversal fails (need original order preserved)

- [ ] Recognize Fast-Slow Pointer by signals ("cycle", "middle", "Kth from end")
- [ ] Recall Fast-Slow skeleton without notes
- [ ] Explain WHY fast-slow beats hash set (O(1) space vs O(n))
- [ ] Explain WHEN to apply (linked lists specifically)

- [ ] Recognize Stack (LIFO) by signals ("undo", "brackets", "backtrack", "DFS", "expression")
- [ ] Recall Stack push/pop semantics and LIFO guarantee
- [ ] Explain WHY stack beats queue (last-in requirement)
- [ ] Implement using `Stack<T>` correctly (not `List<T>`)

- [ ] Recognize Queue (FIFO) by signals ("BFS", "buffering", "arrival order", "FIFO", "scheduling")
- [ ] Recall Queue enqueue/dequeue semantics and FIFO guarantee
- [ ] Explain WHY queue beats stack (first-come requirement)
- [ ] Implement using `Queue<T>` correctly (NOT `List.RemoveAt(0)`)

- [ ] Recognize Binary Search by signals ("sorted", "log n", "boundary", "answer space", "first/last")
- [ ] Recall Binary Search skeleton without notes (mid = low + (high-low)/2)
- [ ] Explain WHY invariant-based BS is robust (prevents infinite loops)
- [ ] Explain WHEN to apply (only sorted arrays OR monotone answer space)

### Problem Solving — Can You Practice?

- [ ] Solved ALL Stage 1 canonical problems (7 problems) ✓
- [ ] Solved 80%+ Stage 2 variations (6+ of 7 problems)
- [ ] Solved 50%+ Stage 3 integration problems (got the ideas, even if not perfect)

### Production Code Quality — Can You Code?

- [ ] Used guard clauses on all inputs (null checks, boundary conditions)
- [ ] Added comments explaining mental models (WHY, not WHAT)
- [ ] Chose correct collection (Stack<T> not List<T>, Queue<T> not List.RemoveAt(0))
- [ ] Handled edge cases explicitly (empty lists, single elements, rotations)
- [ ] Your code passes code review (clean, readable, efficient, no hacks)

### Interview Ready — Can You Communicate?

- [ ] Can solve Stage 1 problem in < 5 minutes
- [ ] Can EXPLAIN your pattern choice to interviewer ("I see cycle detection, so fast-slow pointer because...")
- [ ] Can write PRODUCTION-GRADE code, not hacks (guards, comments, no shortcuts)
- [ ] Can discuss tradeoffs (time vs space, linked list vs array, when/why to use each)

---

### 🎯 Week 2 Mastery Status

- [ ] **YES - I've mastered Week 2. Ready for Week 3 (Sorting, Heaps, Hashing).**
- [ ] **NO - Need to practice more. Focus on Stage 2 variations or weak patterns.**

---

## 📚 REFERENCE MATERIALS

This file is self-contained. You have:
- **Decision framework** (SECTION 1) — Recognize which pattern to use
- **Anti-patterns knowledge** (SECTION 2) — What NOT to do and why
- **Production-grade code** (SECTION 3) — 5 complete patterns with guards and mental models
- **Collection guidance** (SECTION 4) — Which collection for which use case
- **Progressive practice** (SECTION 5) — Problems from easy to hard
- **Real gotchas** (SECTION 6) — Bugs you'll actually encounter
- **Quick interview reference** (SECTION 7) — 30-second recall before interview

**Everything you need to master Week 2 is here.**

---

## 🚀 HOW TO USE THIS FILE

### For Learning:
1. Start with **SECTION 1** → Understand the decision tree
2. Review **SECTION 2** → Learn what NOT to do
3. Study **SECTION 3** → Understand production implementations with mental models
4. Follow **SECTION 5** → Solve problems progressively (Stage 1 → 2 → 3)

### For Reference:
1. See a problem? → Check **SECTION 1** (decision tree)
2. Stuck or getting wrong answer? → Check **SECTION 6** (gotchas)
3. Need code template? → Check **SECTION 3** (implementations)
4. Before interview? → Check **SECTION 7** (quick recall)

### For Interview Prep:
1. Day before: Review **SECTION 7** (mental models)
2. Day of: Skim **SECTION 1** (decision tree)
3. During interview: Use mental models from **SECTION 7** to explain your choices

---

## 📊 COMPLEXITY REFERENCE

| Pattern | Time | Space | Notes |
|---------|------|-------|-------|
| Linked List Reversal | O(n) | O(1) extra | One pass, no extra data structures |
| Fast-Slow Pointer | O(n) | O(1) extra | No hash set needed |
| Stack Operations | O(1) each | O(n) worst | Depends on problem usage |
| Queue Operations | O(1) each | O(n) worst | Depends on problem usage |
| Binary Search | O(log n) | O(1) extra | Only works on sorted arrays |
| Dynamic Array Append | O(1) amortized | O(n) total | Occasional reallocation cost |

---

*End of Week 2 Extended C# Support — v13 Hybrid Format COMPLETE*

---

This file combines:
- ✅ **Pattern selection guidance** (v11 strength) — Know WHEN/WHY to choose
- ✅ **Production-grade code** (v12 strength) — Know HOW to implement
- ✅ **Anti-patterns & gotchas** (v11 strength) — Know what to AVOID  
- ✅ **Progressive learning** (v11 strength) — Practice from easy to hard
- ✅ **Interview readiness** (v13 integration) — Pass technical interviews
- ✅ **Complete coverage** (WEEK 2 TOPICS) — Arrays, linked lists, stacks, queues, binary search

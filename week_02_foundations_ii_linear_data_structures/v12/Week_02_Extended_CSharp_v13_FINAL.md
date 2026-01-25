# 🗺️ Week_02_Extended_CSharp_Problem_Solving_Implementation

**Version:** v1.0 HYBRID (Pattern Recognition + Production Implementation)  
**Purpose:** Master Week 2 patterns (Linear Data Structures & Binary Search) through recognition, understanding, and practice  
**Target:** Transform pattern knowledge into interview-ready C# coding skills  
**Prerequisites:** Week 2 instructional files + standard support files complete

---

## SECTION 1️⃣: PATTERN RECOGNITION FRAMEWORK

### 🎯 Decision Tree — How to Identify & Choose Week 2 Patterns

Use this decision tree when you encounter a problem in Week 2:

| 🔍 Problem Phrases/Signals | 🎯 Pattern Name | ❓ Why This Pattern? | 💻 C# Collection | ⏱️ Time/Space |
|---|---|---|---|---|
| "Reverse linked list", "iterate backwards" | **Linked List Reversal** | Manually manage pointer links; traverse backwards | `class ListNode` | O(n)/O(1) |
| "Middle node", "detect cycle", "cycle entry point" | **Fast-Slow Pointer** | Two pointers at different speeds; meet if cycle | `ListNode` | O(n)/O(1) |
| "Undo/redo", "nested brackets", "backtracking state" | **Stack (LIFO)** | Last-in-first-out; naturally handles recursion | `Stack<T>` | O(n)/O(n) |
| "Process in order", "buffer", "first-come-first-served" | **Queue (FIFO)** | First-in-first-out; BFS, scheduling | `Queue<T>` or `LinkedList<T>` | O(n)/O(n) |
| "Sorted array", "log n hint", "binary", "first/last occurrence" | **Binary Search** | Halving search space; invariant on [low, high] | `int[]`, `List<int>` | O(log n)/O(1) |
| "Capacity planning", "max load", "minimize maximum" | **Binary Search on Answer** | Search in answer space, not data | feasibility check function | O(log max·check)/O(1) |
| "Add to front/back frequently", "insert near edges" | **Deque (Double-Ended Queue)** | O(1) operations at both ends | `Deque<T>` or custom | O(1) each/O(n) |
| "Sequential processing", "prefix sum", "aggregate" | **Linear Scan / Array Traversal** | Simple iteration when pattern is straightforward | `T[]`, `List<T>` | O(n)/O(1) extra |

**HOW TO READ THIS:**
- See "[Signal X]" in a problem? IMMEDIATELY think "[Pattern Y]"
- Ask yourself: "Why does this pattern solve it?" → Internalize the reasoning
- Check what collection is recommended → Learn why that collection is best

---

## SECTION 2️⃣: ANTI-PATTERNS — WHEN NOT TO USE & WHAT FAILS

### ⚠️ Week 2 Common Mistakes & Correct Alternatives

Learn WHAT NOT TO DO and WHY:

| ❌ Wrong Approach | 💥 Why It Fails | ⚡ Symptom/Consequence | ✅ Correct Alternative |
|---|---|---|---|---|
| Using `List<T>.RemoveAt(0)` as queue replacement | O(n) per operation (shifts all elements) | Performance degrades quickly for queue-like patterns | Use `Queue<T>` for O(1) Dequeue, or `LinkedList<T>` |
| Applying binary search to unsorted array | Search assumes sorted order; violates precondition | Returns garbage result or misses target | Sort first with O(n log n), or use different pattern |
| Mutating LinkedList<T> inside `foreach` loop | Iterator invalidation; concurrent modification | `InvalidOperationException` or skipped elements | Use `while` loop with explicit `LinkedListNode<T>` |
| Forgetting to check `current != null` before `current.Next.Value` | Null reference exception | `NullReferenceException` crash | Always check pointers: `if (current?.Next != null)` |
| Using array indexing on linked list (mentally) | Linked list has no O(1) random access | Mentally treating as array leads to wrong approach | Recognize linked list pattern and use traversal |

**ANTI-PATTERN LESSON:**
Understand not just the pattern, but understand its boundaries.
When you see someone use [Mistake], explain why [Alternative] is better.

---

## SECTION 3️⃣: PATTERN IMPLEMENTATIONS (Production-Grade)

### Pattern 1: Linked List Reversal (Iterative)

#### 🧠 Mental Model
Think of three runners on a line: `prev` (before), `current` (now), `next` (after). As you move forward, reverse the link from `current` back to `prev`. Advance all three runners forward. When `current` becomes null, `prev` is the new head.

#### ✅ When to Use This Pattern
- ✅ Reverse a singly linked list (interview classic)
- ✅ Reverse a portion of a linked list
- ✅ Detect and reverse cycles in linked list problems

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Reverse a singly linked list iteratively.
/// Time Complexity: O(n) | Space Complexity: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// Three pointers: prev (reversed portion), current (processing), next (lookahead).
/// Reverse each link from current back to prev. Advance all three.
/// When current reaches null, prev is the new head.
/// </summary>
public ListNode ReverseList(ListNode head) {
    // STEP 1: Guard Clauses (Handle edge cases first)
    if (head == null || head.Next == null) return head;
    
    // STEP 2: Initialize State
    // prev: tracks the reversed portion (starts null)
    // current: tracks the node being processed
    // next: lookahead to avoid losing reference
    ListNode prev = null;
    ListNode current = head;
    
    // STEP 3: Core Logic Loop - Traverse and reverse
    while (current != null) {
        // Step A: Save next reference BEFORE we overwrite current.Next
        ListNode next = current.Next;
        
        // Step B: Reverse the link from current back to prev
        current.Next = prev;
        
        // Step C: Advance prev forward
        prev = current;
        
        // Step D: Advance current forward
        current = next;
        
        // Visualization:
        // Before: ... → [prev.val|next=current] → [current.val|next=X] → [X.val|...] → null
        // After:  ... ← [current.val|next=prev]   [prev.val|next=...]  [X.val|...] → null
    }
    
    // Return new head (prev, since current is now null)
    return prev;
}

// Helper class (if not already defined)
public class ListNode {
    public int Value;
    public ListNode Next;
    
    public ListNode(int value = 0, ListNode next = null) {
        Value = value;
        Next = next;
    }
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Save `next` BEFORE overwriting `current.Next` — otherwise you lose reference to the rest of the list
- 🟡 **PATTERN:** "three-pointer reversal" is the foundation for many linked list mutations
- 🟢 **BEST PRACTICE:** Always check `current != null` before accessing `current.Next` or `current.Value`

---

### Pattern 2: Fast-Slow Pointer (Cycle Detection)

#### 🧠 Mental Model
Imagine two runners on a circular track. One runs at 2x speed, one at 1x speed. If the track is truly circular (has a cycle), they will eventually meet. If no cycle, the fast runner exits the track (reaches null).

#### ✅ When to Use This Pattern
- ✅ Detect if a cycle exists in a linked list
- ✅ Find the entry point of a cycle
- ✅ Find the middle node of a linked list in one pass

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Detect cycle in a linked list using fast-slow pointers.
/// Time Complexity: O(n) | Space Complexity: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// Two pointers move at different speeds: slow (1 step), fast (2 steps).
/// If cycle exists, they meet. If no cycle, fast reaches null.
/// </summary>
public bool HasCycle(ListNode head) {
    // STEP 1: Guard Clauses
    if (head == null || head.Next == null) return false;
    
    // STEP 2: Initialize State
    // slow: moves 1 step at a time
    // fast: moves 2 steps at a time
    ListNode slow = head;
    ListNode fast = head;
    
    // STEP 3: Core Logic Loop - Advance pointers
    while (fast != null && fast.Next != null) {
        // Advance slow by 1
        slow = slow.Next;
        
        // Advance fast by 2
        fast = fast.Next.Next;
        
        // Check if they meet (cycle detected)
        if (slow == fast) return true;
    }
    
    // If fast reached null without meeting slow, no cycle
    return false;
}

/// <summary>
/// Find the middle node of a linked list using fast-slow pointers.
/// Time Complexity: O(n) | Space Complexity: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// When fast reaches end, slow is at middle (for odd n) or left-middle (for even n).
/// </summary>
public ListNode FindMiddle(ListNode head) {
    // STEP 1: Guard Clauses
    if (head == null) return null;
    
    // STEP 2: Initialize State
    ListNode slow = head;
    ListNode fast = head;
    
    // STEP 3: Core Logic Loop
    // Move slow 1 step, fast 2 steps, until fast reaches end
    while (fast != null && fast.Next != null) {
        slow = slow.Next;
        fast = fast.Next.Next;
    }
    
    // slow is now at the middle
    return slow;
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Always check `fast != null && fast.Next != null` before `fast.Next.Next` to avoid null reference
- 🟡 **PATTERN:** Finding middle in one pass is useful for split-then-merge strategies
- 🟢 **BEST PRACTICE:** This pattern is foundational for cycle detection and cycle entry point finding

---

### Pattern 3: Stack-Based Expression Evaluation

#### 🧠 Mental Model
A stack naturally handles nested/parenthesized expressions. When you see an operator, pop two operands, apply the operator, push result back. LIFO order ensures correct evaluation.

#### ✅ When to Use This Pattern
- ✅ Evaluate postfix expressions (RPN)
- ✅ Evaluate infix expressions (with operator precedence)
- ✅ Check balanced brackets/parentheses
- ✅ Undo/redo functionality

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Evaluate a postfix (Reverse Polish Notation) expression.
/// Time Complexity: O(n) | Space Complexity: O(n)
/// 
/// 🧠 MENTAL MODEL:
/// Postfix: operands come before operators (e.g., "3 4 +").
/// Push operands onto stack. When you see operator, pop two, apply, push result.
/// Final result is the single value left on stack.
/// </summary>
public int EvalRPN(string[] tokens) {
    // STEP 1: Guard Clauses
    if (tokens == null || tokens.Length == 0) return 0;
    
    // STEP 2: Initialize State
    // Stack holds integers during evaluation
    var stack = new Stack<int>();
    
    // STEP 3: Core Logic Loop - Process each token
    foreach (string token in tokens) {
        if (IsOperator(token)) {
            // Pop two operands (order matters for - and /)
            int b = stack.Pop();  // Second operand (top of stack)
            int a = stack.Pop();  // First operand
            
            // Apply operator and push result
            int result = ApplyOperator(token, a, b);
            stack.Push(result);
        } else {
            // Token is a number; parse and push
            int num = int.Parse(token);
            stack.Push(num);
        }
    }
    
    // Result is the final item on the stack
    return stack.Pop();
}

private bool IsOperator(string token) {
    return token == "+" || token == "-" || token == "*" || token == "/";
}

private int ApplyOperator(string op, int a, int b) {
    return op switch {
        "+" => a + b,
        "-" => a - b,
        "*" => a * b,
        "/" => a / b,  // Integer division in C#
        _ => throw new ArgumentException("Unknown operator")
    };
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Pop order matters for non-commutative operators (- and /). First pop is the second operand
- 🟡 **PERFORMANCE:** `Stack<T>` is optimal for this; avoid `List<T>.RemoveAt()` which is O(n)
- 🟢 **BEST PRACTICE:** Use `switch` expression for clean operator dispatch in C# 8+

---

### Pattern 4: Queue-Based BFS Traversal

#### 🧠 Mental Model
Queue maintains FIFO order. Push nodes to visit onto queue, then process them in the order they arrived. Naturally captures level-by-level traversal in a tree or graph.

#### ✅ When to Use This Pattern
- ✅ Level-order tree traversal
- ✅ Shortest path in unweighted graph
- ✅ BFS on implicit graphs
- ✅ Buffering/scheduling tasks

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Level-order (breadth-first) traversal of a tree.
/// Time Complexity: O(n) | Space Complexity: O(max width of tree)
/// 
/// 🧠 MENTAL MODEL:
/// Queue processes nodes level-by-level. Push children to back, process from front.
/// </summary>
public List<List<int>> LevelOrder(TreeNode root) {
    // STEP 1: Guard Clauses
    var result = new List<List<int>>();
    if (root == null) return result;
    
    // STEP 2: Initialize State
    // Queue holds nodes at current frontier
    var queue = new Queue<TreeNode>();
    queue.Enqueue(root);
    
    // STEP 3: Core Logic Loop - Process level by level
    while (queue.Count > 0) {
        // currentLevelSize = how many nodes at this level
        int currentLevelSize = queue.Count;
        var currentLevel = new List<int>();
        
        // Process all nodes at current level
        for (int i = 0; i < currentLevelSize; i++) {
            TreeNode node = queue.Dequeue();
            currentLevel.Add(node.Value);
            
            // Enqueue children for next level
            if (node.Left != null) queue.Enqueue(node.Left);
            if (node.Right != null) queue.Enqueue(node.Right);
        }
        
        result.Add(currentLevel);
    }
    
    return result;
}

public class TreeNode {
    public int Value;
    public TreeNode Left;
    public TreeNode Right;
    
    public TreeNode(int value = 0, TreeNode left = null, TreeNode right = null) {
        Value = value;
        Left = left;
        Right = right;
    }
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Capture `queue.Count` BEFORE the loop, not inside, to process only current level's nodes
- 🟡 **PERFORMANCE:** `Queue<T>` is O(1) for Enqueue/Dequeue; use it instead of `List<T>`
- 🟢 **BEST PRACTICE:** Keep a count of level size to distinguish between BFS levels

---

### Pattern 5: Binary Search on Sorted Array

#### 🧠 Mental Model
Invariant: target is always in the range `[low, high]` (inclusive). Each iteration, compute `mid` and compare with target. Discard half the remaining range. Repeat until found or range is empty.

#### ✅ When to Use This Pattern
- ✅ Find an element in a sorted array
- ✅ Find first/last occurrence of element
- ✅ Find lower_bound/upper_bound
- ✅ Any monotone property search

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Binary search for a target in a sorted array.
/// Time Complexity: O(log n) | Space Complexity: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// Invariant: If target exists, it's in [low, high].
/// Compare mid with target; discard half; repeat.
/// </summary>
public int BinarySearch(int[] arr, int target) {
    // STEP 1: Guard Clauses
    if (arr == null || arr.Length == 0) return -1;
    
    // STEP 2: Initialize State
    // [low, high] is the search range (inclusive)
    int low = 0;
    int high = arr.Length - 1;
    
    // STEP 3: Core Logic Loop - Narrow range until found
    while (low <= high) {
        // Compute mid using formula to avoid integer overflow
        int mid = low + (high - low) / 2;
        
        if (arr[mid] == target) {
            return mid;  // Found
        } else if (arr[mid] < target) {
            // Target is in right half
            low = mid + 1;
        } else {
            // Target is in left half
            high = mid - 1;
        }
    }
    
    // Not found
    return -1;
}

/// <summary>
/// Find the first (leftmost) occurrence of target in sorted array.
/// Time Complexity: O(log n) | Space Complexity: O(1)
/// </summary>
public int BinarySearchFirst(int[] arr, int target) {
    if (arr == null || arr.Length == 0) return -1;
    
    int low = 0;
    int high = arr.Length - 1;
    int result = -1;
    
    while (low <= high) {
        int mid = low + (high - low) / 2;
        
        if (arr[mid] == target) {
            result = mid;  // Found, but keep searching left
            high = mid - 1;
        } else if (arr[mid] < target) {
            low = mid + 1;
        } else {
            high = mid - 1;
        }
    }
    
    return result;
}

/// <summary>
/// Binary search on answer space (monotone condition).
/// Example: Find minimum capacity to deliver all packages.
/// Time Complexity: O(log max_capacity * check_time) | Space Complexity: O(1)
/// </summary>
public int MinCapacity(int[] packages, int days) {
    // Invariant: If we can deliver with capacity C, we can deliver with C+1.
    // Find minimum C where this is true.
    
    int low = packages.Max();  // At minimum, capacity = heaviest package
    int high = packages.Sum(); // At maximum, capacity = sum of all packages
    int result = high;
    
    while (low <= high) {
        int mid = low + (high - low) / 2;
        
        if (CanDeliver(packages, mid, days)) {
            // Feasible with mid capacity; try smaller
            result = mid;
            high = mid - 1;
        } else {
            // Not feasible; need larger capacity
            low = mid + 1;
        }
    }
    
    return result;
}

private bool CanDeliver(int[] packages, int capacity, int days) {
    // Check if we can deliver all packages in 'days' with given capacity
    int currentLoad = 0;
    int daysUsed = 1;
    
    foreach (int pkg in packages) {
        if (currentLoad + pkg > capacity) {
            daysUsed++;
            currentLoad = pkg;
        } else {
            currentLoad += pkg;
        }
    }
    
    return daysUsed <= days;
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Use `mid = low + (high - low) / 2` to avoid integer overflow (not `(low + high) / 2`)
- 🟡 **PATTERN:** Binary search on answer space is powerful; any monotone condition can be searched
- 🟢 **BEST PRACTICE:** Always verify the precondition (array is sorted) before binary search

---

## SECTION 4️⃣: C# COLLECTION DECISION GUIDE

### When to Use Each Collection for Week 2 Patterns

| Use Case | Collection | Why? | When NOT to Use | Alternative |
|---|---|---|---|---|
| Fixed-size sequential data, cache locality | `int[]` or `T[]` | O(1) access, contiguous memory, prefetching | When size changes frequently | `List<T>` |
| Dynamic array, frequent push/pop at end | `List<T>` | O(1) amortized append, resizing automatic | Frequent insert/remove at front | Use `LinkedList<T>` or `Queue<T>` |
| Frequent insert/delete at head or tail | `LinkedList<T>` | O(1) insert/delete at known node | When you need O(1) random access | `List<T>` or custom Deque |
| LIFO semantics, undo/redo, backtracking | `Stack<T>` | Semantic clarity; O(1) push/pop | When you need FIFO order | Use `Queue<T>` |
| FIFO semantics, BFS, scheduling, buffering | `Queue<T>` | Semantic clarity; O(1) enqueue/dequeue | When you need LIFO order | Use `Stack<T>` |
| Linked list node-based structure | `class ListNode { int Value; ListNode Next; }` | Full control over traversal and mutation | When linear list semantics suffice | `LinkedList<T>` (if you don't need manual control) |
| Custom deque (both ends) | Custom `Deque<T>` or `LinkedList<T>` | O(1) operations at both ends | When you only need one end fast | `Queue<T>` or `Stack<T>` |

**KEY INSIGHT:**
Choosing the right collection is as important as choosing the right pattern.
Wrong collection = Correct algorithm running slowly (or incorrectly).

---

## SECTION 5️⃣: PROGRESSIVE PROBLEM LADDER

### 🎯 Strategy: Solve Problems in Stages

**Stage 1 (Green):** Master core pattern skeleton  
**Stage 2 (Yellow):** Recognize pattern variations and edge cases  
**Stage 3 (Red):** Combine multiple patterns for complex problems

---

### 🟢 STAGE 1: CANONICAL PROBLEMS — Master Core Pattern

Solve these to cement the pattern. Can you code the skeleton without looking?

| # | LeetCode # | Difficulty | Pattern | C# Focus | Core Concept |
|---|---|---|---|---|---|
| 1 | #206 | 🟢 Easy | Linked List Reversal | Save `next` before mutating `current.Next` | Reverse entire list |
| 2 | #141 | 🟢 Easy | Fast-Slow Pointer | Check `fast != null && fast.Next != null` | Detect cycle exists |
| 3 | #20 | 🟢 Easy | Stack (LIFO) | `Push`/`Pop` for bracket matching | Balanced parentheses |
| 4 | #232 | 🟢 Easy | Queue (FIFO) | Implement queue using `Stack<T>` | FIFO semantics |
| 5 | #704 | 🟢 Easy | Binary Search | Use `mid = low + (high - low) / 2` | Basic binary search |
| 6 | #67 | 🟢 Easy | Binary Search (Answer) | Set up feasibility check function | Search answer space |

**STAGE 1 GOAL:** Pattern fluency. Can you implement [Pattern] skeleton in < 5 minutes?

---

### 🟡 STAGE 2: VARIATIONS — Recognize Pattern Boundaries

These problems twist the pattern. When does it work? When does it break?

| # | LeetCode # | Difficulty | Pattern + Twist | C# Focus | When Pattern Breaks |
|---|---|---|---|---|---|
| 1 | #92 | 🟡 Medium | Linked List Reversal + partial | Reverse portion of list; update pointers | Need to handle head change |
| 2 | #142 | 🟡 Medium | Fast-Slow Pointer + cycle entry | Find where cycle STARTS, not just detect | Need second pass to find entry |
| 3 | #155 | 🟡 Medium | Stack + min tracking | Stack with metadata for getMin() | Need extra state per element |
| 4 | #622 | 🟡 Medium | Queue (circular buffer) | Implement queue with fixed circular array | Wraparound logic at boundaries |
| 5 | #33 | 🟡 Medium | Binary Search + rotated array | Search in rotated sorted array | Need to identify which half is sorted |
| 6 | #1891 | 🟡 Medium | Binary Search on Answer | Cutting ribbons with max length | Feasibility check is complex |

**STAGE 2 GOAL:** Pattern boundaries. When do you need [Alternative Pattern]? When is [Pattern] insufficient?

---

### 🟠 STAGE 3: INTEGRATION — Combine Patterns for Real Problems

Hard problems rarely use just one pattern. These combine multiple patterns.

| # | LeetCode # | Difficulty | Patterns Required | C# Focus | Pattern Combination Logic |
|---|---|---|---|---|---|
| 1 | #23 | 🔴 Hard | Linked List Reversal + Min-Heap | Merge K sorted lists; use priority queue | Heap is better than manual min-finding |
| 2 | #146 | 🔴 Hard | Hash Map + Doubly Linked List | LRU Cache: hash for O(1) access + list for order | Combine for O(1) all operations |
| 3 | #2095 | 🔴 Hard | Queue + BFS + Simulation | Delete middle element of linked list + BFS | Queue naturally models level-by-level |
| 4 | #1157 | 🔴 Hard | Binary Search + Hash Map | Majority Voter; binary search on answer | Two techniques solve complementary parts |

**STAGE 3 GOAL:** Real-world thinking. Professional problems need multiple patterns working together.

---

## SECTION 6️⃣: WEEK 2 PITFALLS & C# GOTCHAS

### 🐛 Runtime Issues & Collection Pitfalls

Common bugs you'll hit and how to fix them:

| ❌ Pattern | 🐛 Bug | 💥 C# Symptom | 🔧 Quick Fix |
|---|---|---|---|---|
| Linked List Reversal | Lose reference to rest of list | `NullReferenceException` when accessing `current.Next` | Save `next = current.Next` BEFORE overwriting `current.Next` |
| Fast-Slow Pointer | Don't check `fast.Next` before `fast.Next.Next` | `NullReferenceException` | Always: `while (fast != null && fast.Next != null)` |
| Stack-based parsing | Forget to handle operator precedence | Wrong evaluation result (e.g., 2+3*4 = 20 instead of 14) | Use separate stack for operators or postfix notation |
| Queue FIFO mix-up | Confuse with Stack (LIFO) | Processes items in wrong order | Remember: Queue is FIFO (enqueue back, dequeue front) |
| Binary Search edge case | Mix up `low + 1` vs `high - 1` | Misses target or infinite loop | `low = mid + 1` (discard mid), `high = mid - 1` (discard mid) |
| Binary Search overflow | Use `(low + high) / 2` | Integer overflow on large arrays | Use `low + (high - low) / 2` instead |

### 🎯 Week 2 Collection Gotchas

These mistakes are EASY to make:

- ❌ Using `List<T>.RemoveAt(0)` as queue replacement → O(n) shifts array → Use `Queue<T>` instead for O(1)
  - Example: ✅ `var q = new Queue<int>(); q.Enqueue(5); int x = q.Dequeue();`
  - Example: ❌ `var list = new List<int> { 5 }; list.RemoveAt(0);` (O(n))

- ❌ Mutating `LinkedList<T>` while enumerating with `foreach` → Iterator invalidation → Use `while` loop with explicit `LinkedListNode<T>`
  - Example: ✅ `LinkedListNode<int> node = list.First; while (node != null) { node = node.Next; }`
  - Example: ❌ `foreach (int val in list) { list.Remove(val); }` (throws exception)

- ❌ Forgetting `current != null` check → `NullReferenceException` on `current.Value` or `current.Next` → Always guard pointer dereference
  - Example: ✅ `if (current != null && current.Next != null) { var x = current.Next.Value; }`
  - Example: ❌ `int x = current.Next.Value;` (crashes if `current` is null)

---

## SECTION 7️⃣: QUICK REFERENCE — INTERVIEW PREPARATION

### Mental Models for Fast Recall (30 seconds before interview!)

| Pattern | 1-Liner Mental Model | Code Symbol | When You See This... |
|---|---|---|---|
| **Linked List Reversal** | "Three pointers: prev, current, next. Reverse each link as you advance." | `current.Next = prev; prev = current;` | "reverse list", "iterate backwards" |
| **Fast-Slow Pointer** | "Two runners at different speeds; they meet if cycle exists." | `while (fast != null && fast.Next != null)` | "cycle", "middle node", "detection" |
| **Stack (LIFO)** | "Push to top, pop from top. Last-in, first-out." | `stack.Push(x); int x = stack.Pop();` | "undo/redo", "brackets", "backtrack" |
| **Queue (FIFO)** | "Enqueue at back, dequeue from front. First-in, first-out." | `queue.Enqueue(x); int x = queue.Dequeue();` | "buffer", "BFS", "order", "scheduling" |
| **Binary Search** | "Halving the range. Invariant: target in [low, high]. Check mid." | `if (arr[mid] < target) low = mid + 1;` | "sorted array", "log n", "binary" |
| **Binary Search on Answer** | "Search in answer space, not data. Check feasibility of each candidate." | `bool feasible = Check(candidate);` | "minimize max", "maximize min", "capacity" |

---

## ✅ WEEK 2 COMPLETION CHECKLIST

### Pattern Fluency — Can You Recognize & Choose?

- [ ] Recognize Linked List Reversal by signals ("reverse", "iterate backwards")
- [ ] Recall Linked List Reversal skeleton without notes (test yourself: 3 pointers, save next)
- [ ] Explain WHY Linked List Reversal beats creating new list (space efficiency)
- [ ] Explain WHEN Linked List Reversal fails (if you need immutability)

- [ ] Recognize Fast-Slow Pointer by signals ("cycle", "middle", "detection")
- [ ] Recall Fast-Slow Pointer skeleton (two pointers, speeds 1x and 2x)
- [ ] Explain WHY Fast-Slow beats visited set (space efficiency)
- [ ] Explain WHEN it's insufficient (finding entry point requires second pass)

- [ ] Recognize Stack pattern by signals ("undo", "brackets", "backtrack")
- [ ] Recall Stack implementation (push/pop, LIFO)
- [ ] Explain WHY Stack for nested structures (natural ordering)
- [ ] Explain WHEN to switch to Queue (when you need FIFO instead)

- [ ] Recognize Queue pattern by signals ("BFS", "buffer", "scheduling")
- [ ] Recall Queue implementation (enqueue/dequeue, FIFO)
- [ ] Explain WHY Queue for level-order (natural ordering)
- [ ] Explain WHEN to avoid `List<T>.RemoveAt(0)` (O(n) not O(1))

- [ ] Recognize Binary Search by signals ("sorted", "log n", "binary")
- [ ] Recall Binary Search skeleton (`mid = low + (high - low) / 2`)
- [ ] Explain WHY binary search beats linear (halving range)
- [ ] Explain WHEN precondition required (array must be sorted)

### Problem Solving — Can You Practice?

- [ ] Solved ALL Stage 1 canonical problems (6 problems)
- [ ] Solved 80%+ Stage 2 variations (4+ problems)
- [ ] Solved 50%+ Stage 3 integration problems (got ideas even if not perfect)

### Production Code Quality — Can You Code?

- [ ] Used guard clauses on all inputs (null checks, edge cases)
- [ ] Added mental model comments to your code
- [ ] Chose correct collection (no `List<T>.RemoveAt(0)`, no mistakes)
- [ ] Handled edge cases explicitly (empty lists, single elements, cycles)
- [ ] Your code would pass code review (clean, readable, efficient)

### Interview Ready — Can You Communicate?

- [ ] Can solve Stage 1 problem in < 5 minutes
- [ ] Can EXPLAIN your pattern choice to interviewer ("I chose Fast-Slow because...")
- [ ] Can write PRODUCTION-GRADE code, not hacks (guards, meaningful names)
- [ ] Can discuss tradeoffs (time vs space, simplicity vs performance)

---

### 🎯 Week 2 Mastery Status

- [ ] **YES - I've mastered Week 2. Ready for Week 3 (Sorting & Heaps).**
- [ ] **NO - Need to practice more. Focus on Stage 2/3 problems.**

---

## 📚 REFERENCE MATERIALS

This file is self-contained. You have:
- Decision framework for pattern selection (SECTION 1)
- Knowledge of anti-patterns (SECTION 2)
- Production-grade code implementations (SECTION 3)
- Collection guidance (SECTION 4)
- Progressive practice plan (SECTION 5)
- Real gotchas and fixes (SECTION 6)
- Quick interview reference (SECTION 7)

**Everything you need to master Week 2 is here.**

---

## 🚀 HOW TO USE THIS FILE

### For Learning:
1. Start with SECTION 1 → Understand the decision tree
2. Review SECTION 2 → Learn what NOT to do
3. Study SECTION 3 → Understand production implementations
4. Follow SECTION 5 → Solve problems progressively

### For Reference:
1. See a problem? → Check SECTION 1 (decision tree)
2. Stuck? → Check SECTION 6 (gotchas)
3. Need code? → Check SECTION 3 (implementations)
4. Before interview? → Check SECTION 7 (quick recall)

### For Interview Prep:
1. Day before: Review SECTION 7 (mental models)
2. Day of: Skim SECTION 1 (decision tree)
3. During interview: Use mental models to explain your choices

---

*End of Week 2 Extended C# Support — v13 Hybrid Format*

---

**Status:** ✅ Week 2 Complete


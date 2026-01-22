# 🗺️ Week_02_Extended_CSharp_Problem_Solving_Implementation

**Version:** v1.0 HYBRID (Pattern Recognition + Production Implementation)  
**Purpose:** Master Week 2 linear data structures and binary search through recognition, understanding, and practice  
**Target:** Transform Week 2 array, list, stack, queue, and binary search knowledge into interview-ready C# coding skills  
**Prerequisites:** Week 2 instructional files + standard support files complete

---

## SECTION 1️⃣: PATTERN RECOGNITION FRAMEWORK

### 🎯 Decision Tree — How to Identify & Choose Week 2 Patterns

Use this decision tree when you encounter a problem in Week 2:

| 🔍 Problem Phrases/Signals | 🎯 Pattern Name | ❓ Why This Pattern? | 💻 C# Collection | ⏱️ Time/Space |
|---|---|---|---|---|
| "Reverse list", "Iterate backwards", "Traverse linked list" | **Linked List Traversal & Manipulation** | Direct pointer control over node structure; O(1) local operations | `class ListNode`, `LinkedList<T>` | O(n) / O(1) |
| "Middle node", "Detect cycle", "Fast/slow movement" | **Fast-Slow Pointer (Runner)** | Two pointers at different speeds meet at cycle or midpoint | `ListNode` manually | O(n) / O(1) |
| "Undo/redo", "Nested brackets", "Expression evaluation", "Backtracking stack" | **Stack (LIFO)** | Last-in-first-out semantics match problem structure | `Stack<T>` or `List<T>` | O(n) / O(n) |
| "Process in arrival order", "Buffering", "FIFO queue", "BFS preparation" | **Queue (FIFO)** | First-in-first-out for processing; circular buffer advantage | `Queue<T>` or `LinkedList<T>` | O(n) / O(n) |
| "Sliding window", "Deque", "Remove front and back" | **Deque (Double-Ended Queue)** | Access both ends efficiently; monotonic queue patterns | `Deque<T>` (custom) or `LinkedList<T>` | O(n) / O(n) |
| "Sorted array", "Log n", "First/last occurrence", "Boundary search" | **Binary Search (Invariant-Based)** | Halving search space with maintained invariant [low, high] | `int[]`, `List<int>` | O(log n) / O(1) |
| "Capacity needed", "Dynamic sizing", "Insert many elements" | **Dynamic Array Management** | Understand reallocation costs; amortized analysis | `List<T>` | O(n) amortized / O(n) space |

**HOW TO READ THIS:**
- See "[Signal X]" in a problem? IMMEDIATELY think "[Pattern Y]"
- Ask yourself: "Why does this pattern solve it?" → Internalize the reasoning
- Check what collection is recommended → Learn why that collection is best for this use case

---

## SECTION 2️⃣: ANTI-PATTERNS — WHEN NOT TO USE & WHAT FAILS

### ⚠️ Week 2 Common Mistakes & Correct Alternatives

Learn WHAT NOT TO DO and WHY:

| ❌ Wrong Approach | 💥 Why It Fails | ⚡ Symptom/Consequence | ✅ Correct Alternative |
|---|---|---|---|
| Using `List<T>.RemoveAt(0)` as queue | O(n) per operation due to array shift | "My queue operations are slow!" Unexpected O(n²) behavior | Use `Queue<T>` for O(1) dequeue or `LinkedList<T>` |
| Applying binary search to UNSORTED array | Violates binary search precondition; incorrect results | Wrong answer silently returned (BUG hard to catch) | Sort first (if allowed) or use linear search |
| Mutating `LinkedList<T>` inside `foreach` loop | Iterator invalidation; unpredictable behavior | Skipped elements or exceptions | Use explicit `while` loop with `LinkedListNode<T>` reference |
| Reversing linked list without saving next pointer | Lost reference to remaining list after `current.Next = prev` | Cannot traverse rest of list (memory leak of nodes) | Save `next = current.Next` BEFORE modifying `current.Next` |
| Using array index when pointer-chasing in linked list | Linked lists don't have O(1) index access | O(n) per index check; catastrophic slowdown | Keep explicit node references; advance pointers incrementally |
| Not guarding `head` in linked list operations | Null reference exception on empty list | `NullReferenceException` at first line | Always check `if (head == null) return default;` first |
| Choosing `Array` when size grows frequently | Reallocation on each overflow causes performance spikes | Latency spike under high growth load | Use `List<T>` for dynamic sizing |

**ANTI-PATTERN LESSON:**
Understand not just the pattern, but understand its boundaries.
When you see someone use [Mistake], explain why [Alternative] is better.

---

## SECTION 3️⃣: PATTERN IMPLEMENTATIONS (Production-Grade)

### Pattern 1: Linked List Traversal & Reversal

#### 🧠 Mental Model
A manual `IEnumerator<T>` over nodes; you advance a `current` reference by following `.Next` pointers instead of relying on `foreach` or indices. You explicitly manage node links like a tiny state machine.

#### ✅ When to Use This Pattern
- ✅ You need to inspect each node in a singly or doubly linked list (reverse, find middle, detect cycle).
- ✅ You must change `.Next` pointers (reversal, deletions, list repartitioning).
- ✅ You need single-pass O(n) with no random access.

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Linked List Node - Singly linked
/// Time: O(1) per operation | Space: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// Each node holds a value and a reference to the next node.
/// Traversal means following the .Next chain from head to null.
/// </summary>
public class ListNode
{
    public int Value;
    public ListNode Next;
    
    public ListNode(int value) => Value = value;
}

/// <summary>
/// Traverse singly linked list - Safe visitor pattern
/// Time: O(n) | Space: O(1)
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
        Console.WriteLine(current.Value);
        
        // Advance to next
        current = current.Next;
    }
    // Naturally terminates when current == null
}

/// <summary>
/// Reverse singly linked list - Three-pointer pattern
/// Time: O(n) | Space: O(1) extra
/// 
/// 🧠 MENTAL MODEL:
/// Three pointers: [prev] [current] [next]
/// At each step: save next, reverse pointer, advance all three.
/// </summary>
public ListNode ReverseLinkedList(ListNode head)
{
    // STEP 1: Guard - empty or single node
    if (head == null || head.Next == null) return head;
    
    // STEP 2: Initialize three pointers
    ListNode prev = null;      // No node before head initially
    ListNode current = head;   // Start at head
    
    // STEP 3: Core reversal loop
    while (current != null)
    {
        // Save next BEFORE we break the current link
        // This is CRITICAL: without it, we lose the rest of the list
        ListNode next = current.Next;
        
        // Reverse the pointer: current.Next now points backwards
        current.Next = prev;
        
        // Advance: prev and current move one step forward
        prev = current;
        current = next;
        
        // Visual:
        // Before: null <- prev <- current -> next -> ...
        // After:  null <- current <- prev, current -> next -> ...
    }
    
    // prev now points to what was the last node (new head)
    return prev;
}

/// <summary>
/// Find middle node of linked list
/// Time: O(n) single pass | Space: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// Fast pointer moves 2 steps per iteration, slow moves 1.
/// When fast reaches end, slow is at middle.
/// </summary>
public ListNode FindMiddle(ListNode head)
{
    // STEP 1: Guard - empty or single node
    if (head == null || head.Next == null) return head;
    
    // STEP 2: Initialize two pointers
    ListNode slow = head;   // Moves 1x speed
    ListNode fast = head;   // Moves 2x speed
    
    // STEP 3: Move pointers until fast reaches end
    while (fast != null && fast.Next != null)
    {
        slow = slow.Next;       // 1 step
        fast = fast.Next.Next;  // 2 steps
    }
    
    // When fast hits end, slow is at middle
    return slow;
}

/// <summary>
/// Delete node at specific position (1-indexed)
/// Time: O(n) | Space: O(1)
/// </summary>
public ListNode DeleteAtPosition(ListNode head, int position)
{
    // STEP 1: Guard - empty list
    if (head == null) return null;
    
    // STEP 2: Special case - delete head
    if (position == 1) return head.Next;
    
    // STEP 3: Find node BEFORE target
    ListNode current = head;
    int count = 1;
    
    // Advance to node before target
    while (current != null && count < position - 1)
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
- 🔴 **CRITICAL:** Always save `next = current.Next` BEFORE modifying `current.Next`. Forgetting this loses the rest of the list.
- 🟡 **PERFORMANCE:** Linked list traversal is O(n) but slower than array iteration due to cache misses (pointer chasing).
- 🟢 **BEST PRACTICE:** Use explicit `while` loops with `ListNode` references, NOT `foreach` on `LinkedList<T>`, when you need fine-grained pointer control.

---

### Pattern 2: Fast-Slow Pointer (Runner Pattern)

#### 🧠 Mental Model
Two runners on a circular track: one moves 1 step per lap (`slow`), the other 2 steps (`fast`). If a cycle exists, they'll eventually meet at the same node. If the track ends, `fast` reaches null first.

#### ✅ When to Use This Pattern
- ✅ Detecting a cycle and finding its entry point in a linked list.
- ✅ Finding the middle node of a list in a single pass (no need to know length).
- ✅ Removing the Nth node from end (use two pointers with offset gap).

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Detect cycle in singly linked list
/// Time: O(n) | Space: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// If cycle exists, fast and slow pointers MUST meet.
/// Proof: On each iteration, gap closes by 1. Eventually gap = 0.
/// </summary>
public bool HasCycle(ListNode head)
{
    // STEP 1: Guard - empty or single node (no cycle possible)
    if (head == null || head.Next == null) return false;
    
    // STEP 2: Initialize two pointers
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
/// Once cycle detected, reset one pointer to head.
/// Move both at 1x speed; they meet at cycle entry.
/// Math: distance from head to cycle entry = 
///       distance from meeting point to cycle entry
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
    // Reset one pointer to head, move both at 1x
    ListNode ptr1 = head;
    ListNode ptr2 = slow;  // Meeting point
    
    while (ptr1 != ptr2)
    {
        ptr1 = ptr1.Next;
        ptr2 = ptr2.Next;
    }
    
    // They now meet at cycle entry
    return ptr1;
}

/// <summary>
/// Remove Nth node from end of list
/// Time: O(n) single pass | Space: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// Use two pointers with gap of N nodes.
/// When fast reaches end, slow is at node before target.
/// </summary>
public ListNode RemoveNthFromEnd(ListNode head, int n)
{
    // STEP 1: Guard - empty
    if (head == null) return null;
    
    // STEP 2: Create dummy node to handle "delete head" case
    // Dummy -> head -> ... -> null
    ListNode dummy = new ListNode(0);
    dummy.Next = head;
    
    // STEP 3: Initialize two pointers with gap of n
    ListNode fast = dummy;
    ListNode slow = dummy;
    
    // Advance fast by n+1 to create gap
    for (int i = 0; i <= n; i++)
    {
        if (fast == null) return head;  // n too large
        fast = fast.Next;
    }
    
    // STEP 4: Move both pointers until fast reaches end
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
- 🔴 **CRITICAL:** Always guard `fast.Next` before accessing `fast.Next.Next` to avoid null reference exceptions.
- 🟡 **PERFORMANCE:** Fast-slow pointer is O(n) single pass with O(1) space; better than two-pass + length calculation.
- 🟢 **BEST PRACTICE:** Use dummy node when deleting head is possible; simplifies edge case handling.

---

### Pattern 3: Stack (LIFO) — Expression Evaluation & Backtracking

#### 🧠 Mental Model
Last-In-First-Out (LIFO) semantics: push items on top, pop from top. Think of a plate stack: last plate added is first one removed. Used for backtracking, expression evaluation, and tracking state.

#### ✅ When to Use This Pattern
- ✅ Expression evaluation (infix/postfix conversion, bracket matching).
- ✅ Backtracking (DFS, undo/redo, permutations).
- ✅ Finding next greater/smaller element in array.
- ✅ Monotonic stack patterns (ensuring stack order is maintained).

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Valid parentheses matching - Classic stack problem
/// Time: O(n) | Space: O(n) worst case
/// 
/// 🧠 MENTAL MODEL:
/// Push open brackets, pop on matching close bracket.
/// If stack empty on close, or mismatch, invalid.
/// If stack non-empty at end, invalid.
/// </summary>
public bool IsValidParentheses(string s)
{
    // STEP 1: Guard - empty or odd length
    if (string.IsNullOrEmpty(s) || s.Length % 2 != 0) return false;
    
    // STEP 2: Initialize stack and mapping
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
            // Stack must have matching opening bracket
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
/// Daily temperatures - Find next warmer day using monotonic stack
/// Time: O(n) | Space: O(n)
/// 
/// 🧠 MENTAL MODEL:
/// Maintain stack of INDICES in decreasing temperature order.
/// For each day, pop all days cooler than current.
/// Stack stores "waiting for warmer day" indices.
/// </summary>
public int[] DailyTemperatures(int[] temperatures)
{
    // STEP 1: Guard - empty
    if (temperatures == null || temperatures.Length == 0) return new int[0];
    
    // STEP 2: Initialize result and monotonic stack
    int n = temperatures.Length;
    int[] result = new int[n];
    Stack<int> stack = new();  // Stores INDICES
    
    // STEP 3: Process each temperature from left to right
    for (int i = 0; i < n; i++)
    {
        // Pop all days cooler than today
        while (stack.Count > 0 && temperatures[stack.Peek()] < temperatures[i])
        {
            int prevIdx = stack.Pop();
            result[prevIdx] = i - prevIdx;  // Days until warmer
        }
        
        // Push current day index
        stack.Push(i);
    }
    
    // Remaining stack has no warmer day (result[i] stays 0)
    return result;
}

/// <summary>
/// Evaluate reverse Polish notation (postfix expression)
/// Time: O(n) | Space: O(n)
/// 
/// 🧠 MENTAL MODEL:
/// Postfix: operands first, then operator.
/// Stack: push operands, pop two and apply operator on seeing operator.
/// Example: "3 4 +" → push 3, push 4, see +, pop 4 and 3, push 7
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
            if (stack.Count < 2) throw new InvalidOperationException("Invalid RPN");
            
            // Pop two operands (order matters for - and /)
            int b = stack.Pop();  // Second operand
            int a = stack.Pop();  // First operand
            
            int result = token switch
            {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                "/" => a / b,  // Truncates towards zero in C#
                _ => 0
            };
            
            stack.Push(result);
        }
        else
        {
            // It's a number
            stack.Push(int.Parse(token));
        }
    }
    
    // Final stack must have exactly one element
    return stack.Count == 1 ? stack.Pop() : 0;
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** When using stack, always check `stack.Count > 0` before `stack.Pop()` to avoid exception.
- 🟡 **PERFORMANCE:** Monotonic stack is O(n) because each element pushed/popped once, not O(n²).
- 🟢 **BEST PRACTICE:** Use `Stack<T>` not `List<T>` for clarity of intent (LIFO semantics).

---

### Pattern 4: Queue (FIFO) — BFS & Buffering

#### 🧠 Mental Model
First-In-First-Out (FIFO) semantics: enqueue at back, dequeue from front. Think of a line at a store: first person in line is first to leave. Used for breadth-first search and buffering.

#### ✅ When to Use This Pattern
- ✅ Breadth-first search (BFS) on trees/graphs.
- ✅ Level-order traversal of trees.
- ✅ Circular buffering / sliding window with FIFO constraint.
- ✅ Processes requiring "arrival order" semantics.

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Binary tree node for BFS examples
/// </summary>
public class TreeNode
{
    public int Value;
    public TreeNode Left;
    public TreeNode Right;
    
    public TreeNode(int value) => Value = value;
}

/// <summary>
/// Level-order (BFS) traversal of binary tree
/// Time: O(n) | Space: O(w) where w = max width
/// 
/// 🧠 MENTAL MODEL:
/// Queue stores nodes to process.
/// Process each level, enqueue children for next level.
/// Level-by-level traversal.
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
        // Process all nodes at current level
        int levelSize = queue.Count;
        List<int> currentLevel = new();
        
        for (int i = 0; i < levelSize; i++)
        {
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

/// <summary>
/// Circular queue implementation (array-based, efficient)
/// Time: All ops O(1) | Space: O(capacity)
/// 
/// 🧠 MENTAL MODEL:
/// Linear array with wraparound logic.
/// front and rear indices wrap around using modulo.
/// Empty: front == rear. Full: (rear + 1) % capacity == front.
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
        
        // STEP 2: Add at rear, wrap around
        queue[rear] = value;
        rear = (rear + 1) % capacity;
        count++;
    }
    
    public int Dequeue()
    {
        // STEP 1: Guard - empty
        if (count == 0)
            throw new InvalidOperationException("Queue is empty");
        
        // STEP 2: Remove from front, wrap around
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
- 🔴 **CRITICAL:** Use `Queue<T>` not `List<T>.RemoveAt(0)` for queue operations. `RemoveAt(0)` is O(n) due to array shift.
- 🟡 **PERFORMANCE:** Circular queue is O(1) all operations with fixed space; better than linked list for bounded buffering.
- 🟢 **BEST PRACTICE:** For BFS, count queue size at start of level loop to properly delineate levels.

---

### Pattern 5: Binary Search — Invariant-Based Robust Searching

#### 🧠 Mental Model
Maintain an invariant: the target (if exists) lies within `[low, high]`. Each iteration, narrow the search space by half by moving the boundary that violates the invariant. Terminate when `low > high` or found.

#### ✅ When to Use This Pattern
- ✅ Searching in sorted arrays (exact match, first/last occurrence).
- ✅ Binary search on answer space (monotone feasibility condition).
- ✅ Finding boundaries (lower_bound, upper_bound).
- ✅ Peak finding, rotated array search.

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Standard binary search - Find exact match
/// Time: O(log n) | Space: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// Invariant: If target exists, it's in [low, high].
/// If mid matches, found. If mid < target, search right half.
/// If mid > target, search left half.
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
        // Avoid overflow: low + (high - low) / 2
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
/// When found, continue searching LEFT to find first occurrence.
/// Shift high left even on match.
/// </summary>
public int FindFirst(int[] nums, int target)
{
    // STEP 1: Guard
    if (nums == null || nums.Length == 0) return -1;
    
    // STEP 2: Initialize boundaries
    int low = 0;
    int high = nums.Length - 1;
    int result = -1;
    
    // STEP 3: Search loop
    while (low <= high)
    {
        int mid = low + (high - low) / 2;
        
        if (nums[mid] == target)
        {
            result = mid;
            high = mid - 1;  // Continue left to find first
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
/// When found, continue searching RIGHT to find last occurrence.
/// Shift low right even on match.
/// </summary>
public int FindLast(int[] nums, int target)
{
    // STEP 1: Guard
    if (nums == null || nums.Length == 0) return -1;
    
    // STEP 2: Initialize boundaries
    int low = 0;
    int high = nums.Length - 1;
    int result = -1;
    
    // STEP 3: Search loop
    while (low <= high)
    {
        int mid = low + (high - low) / 2;
        
        if (nums[mid] == target)
        {
            result = mid;
            low = mid + 1;  // Continue right to find last
        }
        else if (nums[mid] < target)
            low = mid + 1;
        else
            high = mid - 1;
    }
    
    return result;
}

/// <summary>
/// Binary search on answer space - Find minimum capacity for ship
/// Time: O(log(max)) | Space: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// Not searching in array, but searching in answer space [min, max].
/// For each candidate answer, check if feasible (predicate).
/// </summary>
public int ShipWithinDays(int[] weights, int days)
{
    // STEP 1: Guard
    if (weights == null || weights.Length == 0) return 0;
    
    // STEP 2: Bounds of answer space
    int low = weights.Max();  // At least carry heaviest item
    int high = weights.Sum();  // Carry all in one day
    
    // STEP 3: Binary search for minimum capacity
    while (low < high)
    {
        int mid = low + (high - low) / 2;
        
        // Check: can we ship with capacity = mid in time?
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
        if (currentLoad + weight > capacity)
        {
            daysNeeded++;
            currentLoad = weight;
            
            if (daysNeeded > days) return false;
        }
        else
        {
            currentLoad += weight;
        }
    }
    
    return true;
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Always use `mid = low + (high - low) / 2` to avoid integer overflow. NOT `(low + high) / 2`.
- 🟡 **PERFORMANCE:** Binary search is O(log n) vs linear O(n); massive speedup on large sorted arrays.
- 🟢 **BEST PRACTICE:** Clearly state the invariant when binary searching; helps debug off-by-one errors.

---

## SECTION 4️⃣: C# Collection Decision Guide

### When to Use Each Collection for Week 2 Patterns

| Use Case | Collection | Why? | When NOT to Use | Alternative |
|---|---|---|---|---|
| Fixed-size, fastest random access | `int[]` (array) | Cache-friendly, O(1) index | Size grows dynamically | `List<T>` |
| Dynamic size, random access needed | `List<T>` | Resizable array, O(1) amortized append | Frequent mid-inserts | `LinkedList<T>` |
| Frequent insert/delete at head/tail | `LinkedList<T>` | O(1) operations at boundaries | Random access needed | `List<T>` |
| LIFO semantics (undo/redo, stack) | `Stack<T>` | Push/pop clarity, LIFO | Need FIFO semantics | `Queue<T>` |
| FIFO semantics (BFS, buffering) | `Queue<T>` | Enqueue/dequeue clarity, FIFO | Need LIFO semantics | `Stack<T>` |
| Manual linked list with pointer control | `class ListNode` | Direct `.Next` manipulation | Just traversing | `LinkedList<T>` or array |

**KEY INSIGHT:**
Choosing the right collection is as important as choosing the right pattern.
Wrong collection = Correct algorithm running slowly.

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
| 1 | #206 | 🟢 Easy | Linked List Reversal | Manual `.Next` manipulation | Pointer reversal pattern |
| 2 | #141 | 🟢 Easy | Fast-Slow Pointer | Cycle detection basics | Two-pointer technique |
| 3 | #20 | 🟢 Easy | Stack (LIFO) | `Stack<T>` basic usage | Bracket matching |
| 4 | #225 | 🟢 Easy | Stack Implementation | Implement using `List<T>` | Understand internals |
| 5 | #232 | 🟢 Easy | Queue Implementation | Implement using `Stack<T>` | FIFO semantics |
| 6 | #704 | 🟢 Easy | Binary Search | Basic `[low, high]` invariant | Standard search pattern |
| 7 | #278 | 🟢 Easy | Binary Search Variant | First bad version (left boundary) | Monotone predicate |

**STAGE 1 GOAL:** Pattern fluency. Can you implement each pattern skeleton in < 5 minutes?

---

### 🟡 STAGE 2: VARIATIONS — Recognize Pattern Boundaries

These problems twist the pattern. When does it work? When does it break?

| # | LeetCode # | Difficulty | Pattern + Twist | C# Focus | When Pattern Breaks |
|---|---|---|---|---|---|
| 1 | #92 | 🟡 Medium | Linked List Reversal (partial) | Reverse only [left, right] segment | Must track left/right boundaries |
| 2 | #142 | 🟡 Medium | Fast-Slow Pointer | Find cycle START point | Extra math needed after detecting cycle |
| 3 | #19 | 🟡 Medium | Fast-Slow / Two-Pointer | Remove Nth from end | Dummy node needed for edge case |
| 4 | #155 | 🟡 Medium | Stack with Extra Data | Min stack tracks minimum | Stack stores pairs, not single values |
| 5 | #150 | 🟡 Medium | Stack Application | Evaluate RPN (postfix) | Operator precedence no longer applies |
| 6 | #33 | 🟡 Medium | Binary Search Variant | Search in rotated sorted array | Pivot location determines which side is sorted |
| 7 | #153 | 🟡 Medium | Binary Search Variant | Find minimum in rotated array | Edge case: [3, 1, 3] vs [3, 1, 1] |

**STAGE 2 GOAL:** Pattern boundaries. When do you need variations? When is the core pattern insufficient?

---

### 🟠 STAGE 3: INTEGRATION — Combine Patterns for Real Problems

Hard problems rarely use just one pattern. These combine multiple.

| # | LeetCode # | Difficulty | Patterns Required | C# Focus | Pattern Combination Logic |
|---|---|---|---|---|---|
| 1 | #LRU | 🔴 Hard | Hash + Linked List | LRU Cache implementation | Hash for O(1) lookup + LL for order |
| 2 | #347 | 🔴 Hard | Hash + Heap (not Week 2 yet) | Top K frequent elements | Hash counts + priority queue selection |
| 3 | #23 | 🔴 Hard | Linked List + Min Heap | Merge K sorted lists | Heap to efficiently find minimum |

**STAGE 3 GOAL:** Real-world thinking. Professional problems need multiple patterns working together.

---

## SECTION 6️⃣: WEEK 2 PITFALLS & C# GOTCHAS

### 🐛 Runtime Issues & Collection Pitfalls

Common bugs you'll hit and how to fix them:

| ❌ Pattern | 🐛 Bug | 💥 C# Symptom | 🔧 Quick Fix |
|---|---|---|---|
| Linked List Traversal | Not saving `next` before mutation | Lost references to nodes after reversal | Always: `ListNode next = current.Next;` before `current.Next = ...` |
| Fast-Slow Pointer | Not guarding `fast.Next` | `NullReferenceException` on `fast.Next.Next` | Use: `while (fast != null && fast.Next != null)` |
| Stack | Pop from empty stack | `InvalidOperationException` | Always: `if (stack.Count > 0)` before `stack.Pop()` |
| Queue | Using `List<T>.RemoveAt(0)` | O(n) instead of O(1) dequeue | Use: `Queue<T>` or `LinkedList<T>` for O(1) |
| Binary Search | `(low + high) / 2` for mid | Integer overflow on large arrays | Use: `mid = low + (high - low) / 2` |
| Binary Search | Off-by-one with `mid + 1` / `mid - 1` | Infinite loop or miss boundary | Test with single-element arrays and boundaries |

### 🎯 Week 2 Collection Gotchas

These mistakes are EASY to make:

- ❌ `List<T>.RemoveAt(0)` for queue operations → O(n) per operation (O(n²) total) → Use `Queue<T>` for O(1)
  - Example: Removing 1000 items from position 0 takes O(n²) = 1M operations instead of O(n) = 1000
  
- ❌ Modifying `LinkedList<T>` while enumerating with `foreach` → Iterator invalidation → Use explicit `while` loop with `LinkedListNode<T>`
  - Example: `foreach (var node in linkedList) { linkedList.Remove(node); }` breaks iterator
  
- ❌ Not checking `null` on linked list head → `NullReferenceException` → Guard: `if (head == null) return default;`
  - Example: Empty linked list; first line crashes if no guard

- ❌ Binary search on unsorted array → Wrong answers silently returned → Sort first or use linear search
  - Example: Binary search on `[3, 1, 4, 1, 5]` returns incorrect result

- ❌ Array index out of bounds in binary search → `IndexOutOfRangeException` → Always use `mid = low + (high - low) / 2`
  - Example: `mid = (int.MaxValue + int.MaxValue) / 2` overflows; `mid = int.MaxValue + (0 - int.MaxValue) / 2` wraps safely

---

## SECTION 7️⃣: QUICK REFERENCE — INTERVIEW PREPARATION

### Mental Models for Fast Recall (30 seconds before interview!)

| Pattern | 1-Liner Mental Model | Code Symbol | When You See This... |
|---|---|---|---|
| **Linked List Reversal** | "Save next, reverse pointer, advance three" | `next = curr.Next; curr.Next = prev; prev = curr; curr = next;` | "Reverse list", "Iterate backwards" |
| **Fast-Slow Pointer** | "Two runners, different speeds, meet if cycle" | `slow = slow.Next; fast = fast.Next.Next;` | "Detect cycle", "Find middle" |
| **Stack (LIFO)** | "Last-in-first-out; push/pop from top" | `stack.Push(x); var x = stack.Pop();` | "Brackets", "Undo/redo", "Expression" |
| **Queue (FIFO)** | "First-in-first-out; enqueue/dequeue" | `queue.Enqueue(x); var x = queue.Dequeue();` | "BFS", "Buffering", "Arrival order" |
| **Binary Search** | "Maintain [low, high] invariant; halve each iteration" | `mid = low + (high - low) / 2;` | "Sorted array", "Log n", "Boundary" |

---

## ✅ WEEK 2 COMPLETION CHECKLIST

### Pattern Fluency — Can You Recognize & Choose?

- [ ] Recognize Linked List Traversal by signals ("reverse", "iterate")
- [ ] Recall Linked List Reversal skeleton without notes (< 2 min)
- [ ] Explain WHY reversal beats alternatives (in-place, O(1) space)
- [ ] Explain WHEN reversal fails (need original order preserved)

- [ ] Recognize Fast-Slow Pointer by signals ("cycle", "middle")
- [ ] Recall Fast-Slow skeleton without notes
- [ ] Explain WHY fast-slow beats other cycle detection (O(1) space)
- [ ] Explain WHEN to apply (linked lists specifically)

- [ ] Recognize Stack (LIFO) by signals ("undo", "brackets", "backtrack")
- [ ] Recall Stack push/pop semantics
- [ ] Explain WHY stack beats queue (last-in requirement)
- [ ] Implement using `Stack<T>` correctly

- [ ] Recognize Queue (FIFO) by signals ("BFS", "buffering", "arrival order")
- [ ] Recall Queue enqueue/dequeue semantics
- [ ] Explain WHY queue beats stack (first-come requirement)
- [ ] Implement using `Queue<T>` correctly (NOT `List.RemoveAt(0)`)

- [ ] Recognize Binary Search by signals ("sorted", "log n", "boundary")
- [ ] Recall Binary Search skeleton without notes
- [ ] Explain WHY invariant-based BS is robust (prevents infinite loops)
- [ ] Explain WHEN to apply (only sorted arrays)

### Problem Solving — Can You Practice?

- [ ] Solved ALL Stage 1 canonical problems (7 problems)
- [ ] Solved 80%+ Stage 2 variations (6+ of 7 problems)
- [ ] Solved 50%+ Stage 3 integration problems (got the ideas, even if not perfect)

### Production Code Quality — Can You Code?

- [ ] Used guard clauses on all inputs (null checks, boundary conditions)
- [ ] Added comments explaining mental models (not just WHAT, but WHY)
- [ ] Chose correct collection (Stack<T> not List<T>, Queue<T> not List.RemoveAt(0))
- [ ] Handled edge cases explicitly (empty lists, single elements, rotations)
- [ ] Your code would pass code review (clean, readable, efficient, no hacks)

### Interview Ready — Can You Communicate?

- [ ] Can solve Stage 1 problem in < 5 minutes
- [ ] Can EXPLAIN your pattern choice to interviewer ("I see cycle detection, so I use fast-slow pointer because...")
- [ ] Can write PRODUCTION-GRADE code, not hacks (guards, comments, no shortcuts)
- [ ] Can discuss tradeoffs (time vs space, linked list vs array, when to use each)

---

### 🎯 Week 2 Mastery Status

- [ ] **YES - I've mastered Week 2. Ready for Week 3 (Sorting, Heaps, Hashing).**
- [ ] **NO - Need to practice more. Focus on Stage 2 variations or weak patterns.**

---

## 📚 REFERENCE MATERIALS

This file is self-contained. You have:
- **Decision framework** for pattern selection (SECTION 1) — When to use each pattern
- **Anti-patterns knowledge** (SECTION 2) — What NOT to do and why
- **Production-grade code** (SECTION 3) — How to implement correctly with guards
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
3. Study **SECTION 3** → Understand production implementations
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
| Linked List Reversal | O(n) | O(1) extra | One pass, constant extra |
| Fast-Slow Pointer | O(n) | O(1) extra | No extra space |
| Stack Operations | O(1) each | O(n) worst | Depends on usage |
| Queue Operations | O(1) each | O(n) worst | Depends on usage |
| Binary Search | O(log n) | O(1) extra | Only sorted arrays |
| Dynamic Array Append | O(1) amortized | O(n) total | Occasional reallocation |

---

*End of Week 2 Extended C# Support — v13 Hybrid Format*

---

**Status:** ✅ WEEK 2 COMPLETE & PRODUCTION READY

This file combines:
- ✅ **Pattern selection guidance** (v11 strength) → Know WHEN/WHY to choose
- ✅ **Production-grade code** (v12 strength) → Know HOW to implement
- ✅ **Anti-patterns & gotchas** (v11 strength) → Know what to AVOID
- ✅ **Progressive learning** (v11 strength) → Practice from easy to hard
- ✅ **Interview readiness** (v13 integration) → Pass technical interviews

Use this as a template for Week 3+ files.


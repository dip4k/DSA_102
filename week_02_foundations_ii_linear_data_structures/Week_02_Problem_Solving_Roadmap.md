# 🧭 WEEK 2 — PROBLEM-SOLVING ROADMAP

**Purpose:** Strategic progression from simple to complex

---

## 🎯 Overall Strategy

Week 2 transitions you from "user of collections" to "implementer of collections." The core theme is **Linear Data Manipulation** and **Divide & Conquer Search**.

### 🧠 The Framework for Week 2 Problems
1.  **Identify the Access Pattern:**
    *   Need O(1) random access? → **Array**.
    *   Need O(1) insert/delete at ends/middle? → **Linked List**.
    *   Need LIFO (Undo/Recursion)? → **Stack**.
    *   Need FIFO (Buffer/Order)? → **Queue**.
    *   Need to find in Sorted Data? → **Binary Search**.

2.  **Visualize the Memory:**
    *   Draw the array indices or list pointers.
    *   **NEVER** write code before drawing the `before` and `after` state of pointers.

3.  **Check Invariants:**
    *   *List:* Did I handle `null` head? Did I break the chain?
    *   *Search:* Is `low <= high` or `low < high`? Does `mid` move?

---

## 🪜 Progression: Simple → Complex

### 🟢 Stage 1: Mechanics (Days 1-3)
**Goal:** Manipulate data structures without crashing (NullReference, IndexOutOfBound).

**Problem Types:**
1.  **Array Traversal:** Iterating, prefix sums.
2.  **Basic List Ops:** Insert, delete, reverse (iterative).
3.  **Two Pointers (Simple):** Merging, finding middle.

**Example Problems:**
*   [LC 206] **Reverse Linked List** (The "Hello World" of pointers).
*   [LC 1929] **Concatenation of Array** (Index math).
*   [LC 876] **Middle of Linked List** (Fast/Slow introduction).

**C# Analogy:**
*   *Reverse List:* Like flipping a stack of cards one by one into a new pile.
*   *Middle Node:* Two people running on a track; one runs 2x speed. When fast guy hits finish, slow guy is at halfway mark.

---

### 🟡 Stage 2: Logic Patterns (Day 4-5)
**Goal:** Use structures as *tools* to solve logical constraints.

**Problem Types:**
1.  **Stack Matching:** Valid Parentheses, adjacent duplicates.
2.  **Queue Simulations:** Josephus problem, moving average.
3.  **Standard Binary Search:** Exact match, first bad version.
4.  **Cycle Detection:** Floyd's Tortoise & Hare.

**Example Problems:**
*   [LC 20] **Valid Parentheses** (Classic Stack).
*   [LC 141] **Linked List Cycle** (Pointer physics).
*   [LC 704] **Binary Search** (Invariant practice).
*   [LC 232] **Implement Queue using Stacks** (Structure conversion).

**C# Analogy:**
*   *Stack Matching:* Like parsing XML/HTML tags. Open tag pushes, Close tag pops.
*   *Cycle Detect:* Like `foreach` loop detection. If you see the same object reference again, you're stuck.

---

### 🟠 Stage 3: Advanced Applications
**Goal:** Combine concepts, handle tricky edge cases, and optimizations.

**Problem Types:**
1.  **Monotonic Stack:** Next Greater Element, Daily Temperatures.
2.  **Binary Search Variants:** Rotated array, Search on Answer (Koko Bananas).
3.  **List + Map:** LRU Cache (Doubly Linked List + Dictionary).

**Example Problems:**
*   [LC 739] **Daily Temperatures** (Processing future data efficiently).
*   [LC 33] **Search in Rotated Sorted Array** (Modified invariants).
*   [LC 142] **Linked List Cycle II** (Finding the *entry* point).

**C# Analogy:**
*   *Monotonic Stack:* Like looking for a "better offer." You discard all previous lower offers because the new one is better and more recent.
*   *Search on Answer:* Like finding the minimum CPU cores needed. Try 8 cores (too fast), try 4 (too slow), try 6...

---

## ⚠ Common Problem-Solving Pitfalls

### ❌ Pitfall 1: The "Blind" Pointer
*   **Symptom:** `NullReferenceException` doing `curr.next.next`.
*   **Fix:** Always check `if (curr != null && curr.next != null)`.
*   **Mental Check:** "Do I have permission to touch the next node?"

### ❌ Pitfall 2: The "Infinite" While Loop
*   **Symptom:** Binary search runs forever.
*   **Fix:** Ensure `mid` moves. Use `low = mid + 1` or `high = mid - 1`. If using `low = mid`, logic is wrong for 2-element arrays.
*   **Mental Check:** "Does my range shrink by at least 1 every iteration?"

### ❌ Pitfall 3: Modifying Collection during Iteration
*   **Symptom:** `InvalidOperationException` or skipping elements.
*   **Fix:** Iterate backwards `for (int i = n-1; i >= 0; i--)` OR use a second Stack/Queue to store items to remove.

### ❌ Pitfall 4: Stack vs Queue Confusion
*   **Symptom:** Solving "Level Order Traversal" with a Stack (DFS) instead of Queue (BFS).
*   **Fix:**
    *   Depth/Recursive logic = **Stack**.
    *   Breadth/Layer/Fairness logic = **Queue**.

---

## 📌 Pattern Templates

### 1. Linked List Traversal (Safe)
*Use for:* Printing, Finding, simple updates.
```csharp
// "The Tourist" - Visits every node safely
Node curr = head;
while (curr != null) {
    // Process curr.Value
    curr = curr.Next;
}
```

### 2. Fast & Slow Pointers (Runner)
*Use for:* Cycle detection, Finding Middle, Kth from end.
```csharp
// "The Race" - One runs 2x speed
Node slow = head;
Node fast = head;
while (fast != null && fast.Next != null) {
    slow = slow.Next;       // 1 step
    fast = fast.Next.Next;  // 2 steps
    
    // Check collision or update logic
}
```

### 3. Binary Search (Standard)
*Use for:* Finding target in sorted array.
```csharp
// "The Divider" - Cuts range in half
int low = 0, high = n - 1;
while (low <= high) {
    // C# safe mid: avoids overflow
    int mid = low + (high - low) / 2;
    
    if (arr[mid] == target) return mid;
    if (arr[mid] < target) 
        low = mid + 1; // Discard left half
    else 
        high = mid - 1; // Discard right half
}
return -1;
```

### 4. Monotonic Stack
*Use for:* Next Greater Element, Histogram areas.
```csharp
// "The Filter" - Keeps items in specific order
Stack<int> stack = new Stack<int>(); // Stores Indices usually
for (int i = 0; i < n; i++) {
    // While current is "better" than top, pop top
    while (stack.Count > 0 && arr[i] > arr[stack.Peek()]) {
        int index = stack.Pop();
        // Result for 'index' is arr[i]
    }
    stack.Push(i);
}
```

---

*End of Week 2 Roadmap*

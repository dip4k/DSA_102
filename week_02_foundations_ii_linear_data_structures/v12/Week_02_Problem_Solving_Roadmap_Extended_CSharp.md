# 🗺️ Week 02 Problem-Solving Roadmap Extended C# — Production-Grade Implementations

**Version:** v1.0  
**Purpose:** Week 02–specific C# problem-solving playbook for Linear Structures & Search (Arrays, Lists, Stacks, Queues, Binary Search)  
**Target:** Transform mental models into C# coding fluency with real-world patterns  
**Prerequisites:** Week 02 instructional files + Week 01 extended C# complete  

---

## 🎯 WEEK 02 PROBLEM-SOLVING FRAMEWORK

**Decision Tree (Week 02 Patterns):**

| Problem Signals/Phrases | 🎯 Primary Pattern | C# Collection | Time/Space | Day |
|------------------------|-------------------|---------------|-----------|-----|
| "Iterate through array efficiently" / "Optimize loop" | Sequential Access Pattern | Array, for loop | O(n) time / O(1) space | Day 1 |
| "Insert many elements" / "Dynamic collection" | Dynamic Array Growth | List<T> (internal resizing) | O(1) amortized / O(n) space | Day 2 |
| "Insert/delete in middle" / "Linked structure" | Linked List Operations | LinkedList<T> | O(n) search / O(1) insert | Day 3 |
| "LIFO behavior" / "Undo/redo stack" | Stack Semantics | Stack<T> | O(1) push/pop / O(n) space | Day 4 |
| "FIFO behavior" / "Task queue scheduling" | Queue Semantics | Queue<T> (circular) | O(1) enqueue/dequeue / O(n) space | Day 4 |
| "Find in sorted array" / "Optimize lookup" | Binary Search | Array with invariants | O(log n) / O(1) space | Day 5 |
| "Feasibility check predicate" / "Optimize configuration" | Binary Search on Answer | Custom bounds check | O(log n × check_cost) / varies | Day 5 |

**Anti-Patterns:**
- ❌ Accessing linked list elements by index (O(n) each time) → Use iterator instead
- ❌ Using linear search on large sorted arrays → Use binary search
- ❌ Incrementing array capacity by 1 → Use geometric growth (2x or 1.5x)
- ❌ Memoizing without overlapping subproblems → No benefit, wastes memory

---

## 💻 C# PATTERN IMPLEMENTATIONS (Week 02)

### Pattern 1: SEQUENTIAL ARRAY ACCESS (Days 1–2)

**C# Mental Model:** Arrays are contiguous memory; sequential iteration exploits prefetching and cache locality for 10-100x speedup over random access.

**When to Use:**
- ✅ Processing all elements once
- ✅ Building aggregates (sum, min, max, count)
- ✅ Filtering or transforming data
- ✅ Matrix operations where iteration order matters

**Core C# Skeleton (Sequential Fast Path):**

```csharp
// FAST: Sequential access (cache-friendly)
public static long SumArrayFast(int[] arr) {
    long sum = 0;
    for (int i = 0; i < arr.Length; i++) {
        sum += arr[i];  // Prefetch works, cache hits
    }
    return sum;
}
// Typical: ~1-2 ns per element

// SLOW: Random access (cache-unfriendly)
public static long SumArrayRandom(int[] arr, int[] indices) {
    long sum = 0;
    for (int i = 0; i < indices.Length; i++) {
        sum += arr[indices[i]];  // Cache miss on every access
    }
    return sum;
}
// Typical: ~20-50 ns per element (10-25x slower!)

// Key insight: Same data, same Big-O (O(n)), vastly different performance
```

**C# Notes:**
- Foreach loops on arrays are optimized by JIT (same as for loop)
- LINQ `.Select()` and `.Aggregate()` maintain sequential access pattern
- Avoid `arr[randomIndices[i]]` patterns when possible

**Implementation Variations:**

```csharp
// Variant 1A: Matrix row-major iteration (fast)
public static long SumMatrixRowMajor(int[,] matrix) {
    long sum = 0;
    for (int row = 0; row < matrix.GetLength(0); row++) {
        for (int col = 0; col < matrix.GetLength(1); col++) {
            sum += matrix[row, col];  // Sequential memory access
        }
    }
    return sum;
}

// Variant 1B: Matrix column-major iteration (slow - wrong order)
public static long SumMatrixColumnMajor(int[,] matrix) {
    long sum = 0;
    for (int col = 0; col < matrix.GetLength(1); col++) {
        for (int row = 0; row < matrix.GetLength(0); row++) {
            sum += matrix[row, col];  // Jumps around memory
        }
    }
    return sum;
}
// 3-10x slower despite same Big-O

// Variant 1C: Tiled matrix iteration (optimize for cache)
public static long SumMatrixTiled(int[,] matrix, int tileSize = 64) {
    long sum = 0;
    int rows = matrix.GetLength(0);
    int cols = matrix.GetLength(1);
    
    // Process in tiles (fit in L1 cache)
    for (int rowTile = 0; rowTile < rows; rowTile += tileSize) {
        for (int colTile = 0; colTile < cols; colTile += tileSize) {
            // Within tile: sequential
            for (int r = rowTile; r < Math.Min(rowTile + tileSize, rows); r++) {
                for (int c = colTile; c < Math.Min(colTile + tileSize, cols); c++) {
                    sum += matrix[r, c];
                }
            }
        }
    }
    return sum;
}
// Minimal performance benefit on modern CPUs (auto-prefetch), but principle important

// Variant 1D: Stride iteration (less friendly than sequential, better than random)
public static long SumArrayStride(int[] arr, int stride) {
    long sum = 0;
    for (int i = 0; i < arr.Length; i += stride) {
        sum += arr[i];
    }
    return sum;
}
// Stride 1: ~1 ns/element
// Stride 2-8: ~1-2 ns/element (still fits in cache)
// Stride 16+: ~5-10 ns/element (cache effectiveness degrades)
// Random: ~20-50 ns/element
```

**C# Pitfalls:**
- ⚠️ Accessing `arr[randomIndex]` in inner loop → Cache disasters
- ⚠️ Calling `arr.Length` in loop condition (no issue in C#, but caution in other languages)
- ⚠️ Assuming LINQ `.AsParallel()` helps without benchmarking (overhead can exceed benefit)

---

### Pattern 2: DYNAMIC ARRAY GROWTH (Day 2)

**C# Mental Model:** List<T> uses exponential growth (2x by default in .NET). Understanding the doubling strategy explains O(1) amortized insertion.

**When to Use:**
- ✅ Unknown size ahead of time
- ✅ Frequent append operations (push to end)
- ✅ Need occasional random access (unlike linked list)
- ✅ Need O(1) amortized insert, not constant-worst-case

**Core C# Skeleton:**

```csharp
public class DynamicArrayTrace<T> {
    private T[] data;
    private int length = 0;
    
    public DynamicArrayTrace() {
        data = new T[1];  // Start with capacity 1
    }
    
    public void Push(T value) {
        // Check if resize needed
        if (length == data.Length) {
            Resize();
        }
        data[length++] = value;
    }
    
    private void Resize() {
        // Double capacity (key strategy)
        int newCapacity = data.Length * 2;
        T[] newData = new T[newCapacity];
        
        // Copy existing elements: O(length) operation
        Array.Copy(data, newData, length);
        data = newData;
    }
}

// Trace for 10 pushes:
// Push 1: capacity 1 → full → Resize to 2, cost 1 copy
// Push 2: capacity 2 → full → Resize to 4, cost 2 copies
// Push 3-4: capacity 4 → no resize
// Push 5: capacity 4 → full → Resize to 8, cost 4 copies
// Push 6-8: capacity 8 → no resize
// Push 9: capacity 8 → full → Resize to 16, cost 8 copies
// Push 10: capacity 16 → no resize
//
// Total copy cost: 1 + 2 + 4 + 8 = 15 copies for 10 pushes
// Amortized: 15 / 10 = 1.5 copies per push ≈ O(1)
```

**C# Notes:**
- `List<T>` in .NET uses internal doubling (verified in reference source)
- `List<T>.Capacity` property shows current backing array size
- `List<T>.EnsureCapacity(n)` allows pre-allocation to avoid resizes

**Implementation Variations:**

```csharp
// Variant 2A: Measure actual resize events
public class DynamicArrayWithMetrics<T> {
    private T[] data;
    private int length = 0;
    public int ResizeCount { get; private set; } = 0;
    public int TotalCopyOps { get; private set; } = 0;
    
    public DynamicArrayWithMetrics() {
        data = new T[1];
    }
    
    public void Push(T value) {
        if (length == data.Length) {
            ResizeCount++;
            TotalCopyOps += length;  // Track copy cost
            
            T[] newData = new T[data.Length * 2];
            Array.Copy(data, newData, length);
            data = newData;
        }
        data[length++] = value;
    }
    
    public double AmortizedCostPerPush => (double)TotalCopyOps / length;
}

// Variant 2B: Shrinking with anti-thrashing
public class DynamicArrayWithShrink<T> {
    private T[] data;
    private int length = 0;
    
    public DynamicArrayWithShrink() {
        data = new T[4];  // Start bigger to reduce early resizes
    }
    
    public void Pop() {
        if (length == 0) throw new InvalidOperationException();
        length--;
        
        // Shrink if 1/4 full (not 1/2, to prevent thrashing)
        // Thrashing: repeatedly grow/shrink if always at boundary
        if (length > 0 && length == data.Length / 4) {
            T[] newData = new T[data.Length / 2];
            Array.Copy(data, newData, length);
            data = newData;
        }
    }
}

// Variant 2C: Reserve capacity to avoid resizes in hot path
public class DynamicArrayWithReserve<T> {
    private T[] data;
    private int length = 0;
    
    public void ReserveCapacity(int minCapacity) {
        if (data.Length < minCapacity) {
            // Round up to next power of 2
            int newCapacity = 1;
            while (newCapacity < minCapacity) newCapacity *= 2;
            
            T[] newData = new T[newCapacity];
            Array.Copy(data, newData, length);
            data = newData;
        }
    }
    
    public void Push(T value) {
        // No resize check; assumes ReserveCapacity called first
        data[length++] = value;
    }
}

// Variant 2D: Compare 1.5x vs 2x growth
public class CompareGrowthStrategies {
    // 2x growth: faster (fewer resizes), more waste (~50%)
    // 1.5x growth: slower (more resizes), less waste (~33%)
    
    public static void Benchmark() {
        const int n = 1_000_000;
        
        // 2x growth
        var sw = System.Diagnostics.Stopwatch.StartNew();
        var list2x = new List<int>(1);  // Start with capacity 1
        for (int i = 0; i < n; i++) {
            // Simulate manual 2x growth
            if (list2x.Count == list2x.Capacity) {
                list2x.Capacity *= 2;
            }
            list2x.Add(i);
        }
        sw.Stop();
        Console.WriteLine($"2x growth: {sw.ElapsedMilliseconds}ms");
        
        // In practice: .NET List<T> uses 2x, which is optimal
    }
}
```

---

### Pattern 3: LINKED LIST TRAVERSAL (Day 3)

**C# Mental Model:** LinkedList<T> provides O(1) insert/delete at known nodes, but O(n) search. Use iterators to avoid repeated searches.

**When to Use:**
- ✅ Frequent insertion/deletion in middle (if you have node reference)
- ✅ LRU cache implementation (combined with Dictionary)
- ✅ Undo/redo stacks (doubly linked)
- ❌ NOT for frequent random access (use arrays instead)

**Core C# Skeleton:**

```csharp
// Proper linked list usage: iterator pattern
public static void ProcessLinkedListEfficiently(LinkedList<int> list) {
    // GOOD: Single traversal, O(n)
    foreach (var node in list) {
        Console.WriteLine(node);
    }
    
    // BAD: Multiple traversals, O(n²) if done repeatedly
    for (int i = 0; i < list.Count; i++) {
        // Accessing list[i] iterates from start each time!
    }
    
    // GOOD: If modifying, iterate with LinkedListNode<T>
    LinkedListNode<int> current = list.First;
    while (current != null) {
        int value = current.Value;
        
        // Insert after current: O(1)
        if (value % 2 == 0) {
            list.AddAfter(current, value * 2);
        }
        
        current = current.Next;
    }
}

// LRU Cache: Hybrid structure (LinkedList + Dictionary)
public class LRUCache<K, V> {
    private LinkedList<(K, V)> recencyOrder = new();
    private Dictionary<K, LinkedListNode<(K, V)>> lookup = new();
    private int capacity;
    
    public LRUCache(int capacity) {
        this.capacity = capacity;
    }
    
    public V Get(K key) {
        if (!lookup.TryGetValue(key, out var node)) {
            throw new KeyNotFoundException();
        }
        
        // Move to front (most recently used)
        recencyOrder.Remove(node);
        recencyOrder.AddFirst(node);
        
        return node.Value.Item2;
    }
    
    public void Put(K key, V value) {
        if (lookup.TryGetValue(key, out var node)) {
            // Update existing
            recencyOrder.Remove(node);
        }
        
        // Add to front
        var newNode = new LinkedListNode<(K, V)>((key, value));
        recencyOrder.AddFirst(newNode);
        lookup[key] = newNode;
        
        // Evict if over capacity
        if (recencyOrder.Count > capacity) {
            var lru = recencyOrder.Last;
            recencyOrder.RemoveLast();
            lookup.Remove(lru.Value.Item1);
        }
    }
}
```

**C# Notes:**
- `LinkedList<T>` is doubly-linked (both next and prev)
- `.AddAfter()`, `.AddBefore()` are O(1) if you have the node
- `.Find()` is O(n); avoid in loops
- Iterating with foreach is the only O(n) way (not O(n²))

**Implementation Variations:**

```csharp
// Variant 3A: Custom doubly-linked node for manual control
public class DoublyLinkedNode<T> {
    public T Value { get; set; }
    public DoublyLinkedNode<T> Prev { get; set; }
    public DoublyLinkedNode<T> Next { get; set; }
}

// Variant 3B: Reverse linked list in-place
public static LinkedListNode<int> ReverseLinkedList(LinkedListNode<int> head) {
    LinkedListNode<int> prev = null;
    LinkedListNode<int> current = head;
    
    while (current != null) {
        // Save next before we change current.Next
        var next = current.Next;
        
        // Reverse the link
        current.Next = prev;
        
        // Move forward
        prev = current;
        current = next;
    }
    
    return prev;  // New head
}

// Variant 3C: Cycle detection (Floyd's algorithm)
public static bool HasCycle<T>(LinkedListNode<T> head) {
    LinkedListNode<T> slow = head, fast = head;
    
    while (fast != null && fast.Next != null) {
        slow = slow.Next;           // Move 1 step
        fast = fast.Next.Next;      // Move 2 steps
        
        if (slow == fast) {
            return true;  // Cycle detected
        }
    }
    
    return false;  // No cycle
}

// Variant 3D: Compare List<T> vs LinkedList<T> performance
public static void CompareDataStructures() {
    const int n = 100_000;
    
    // List<T>: O(1) append, O(n) insert in middle
    var sw = System.Diagnostics.Stopwatch.StartNew();
    var list = new List<int>();
    for (int i = 0; i < n; i++) {
        list.Add(i);  // O(1) amortized
    }
    sw.Stop();
    Console.WriteLine($"List<T>.Add: {sw.ElapsedMilliseconds}ms");
    
    // LinkedList<T>: O(1) insert if you have node, O(n) to find node
    sw = System.Diagnostics.Stopwatch.StartNew();
    var linked = new LinkedList<int>();
    LinkedListNode<int> node = null;
    for (int i = 0; i < n; i++) {
        node = linked.AddLast(i);  // O(1) AddLast
    }
    sw.Stop();
    Console.WriteLine($"LinkedList<T>.AddLast: {sw.ElapsedMilliseconds}ms");
    
    // Verdict: List<T> is usually faster due to cache locality
}
```

---

### Pattern 4: STACK & QUEUE SEMANTICS (Day 4)

**C# Mental Model:** Restrict access patterns (LIFO/FIFO) for clarity and O(1) operations.

**When to Use:**
- ✅ Stack: Undo/redo, expression evaluation, backtracking
- ✅ Queue: BFS, task scheduling, message passing
- ✅ Deque: Sliding window, monotonic operations

**Core C# Skeleton:**

```csharp
// Stack: LIFO (Last In, First Out)
public static void DemoStack() {
    Stack<int> stack = new();
    
    stack.Push(10);
    stack.Push(20);
    stack.Push(30);
    
    int top = stack.Pop();  // Returns 30 (most recent)
}

// Queue: FIFO (First In, First Out)
public static void DemoQueue() {
    Queue<int> queue = new();
    
    queue.Enqueue(10);
    queue.Enqueue(20);
    queue.Enqueue(30);
    
    int first = queue.Dequeue();  // Returns 10 (least recent)
}

// Balanced parentheses checker (Stack)
public static bool IsBalanced(string s) {
    Stack<char> stack = new();
    var pairs = new Dictionary<char, char> { 
        { ')', '(' }, { '}', '{' }, { ']', '[' } 
    };
    
    foreach (char c in s) {
        if (pairs.ContainsValue(c)) {
            stack.Push(c);  // Opening bracket
        } else if (pairs.ContainsKey(c)) {
            if (stack.Count == 0 || stack.Pop() != pairs[c]) {
                return false;  // Mismatch
            }
        }
    }
    
    return stack.Count == 0;  // All matched
}

// Postfix expression evaluator (Stack)
public static int EvaluatePostfix(string[] tokens) {
    Stack<int> stack = new();
    var ops = new HashSet<string> { "+", "-", "*", "/" };
    
    foreach (string token in tokens) {
        if (ops.Contains(token)) {
            int b = stack.Pop();
            int a = stack.Pop();
            int result = token switch {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                "/" => a / b,
                _ => 0
            };
            stack.Push(result);
        } else if (int.TryParse(token, out int num)) {
            stack.Push(num);
        }
    }
    
    return stack.Pop();
}

// BFS using Queue
public static void BFS<T>(Graph<T> graph, T start) where T : notnull {
    Queue<T> queue = new();
    var visited = new HashSet<T>();
    
    queue.Enqueue(start);
    visited.Add(start);
    
    while (queue.Count > 0) {
        T node = queue.Dequeue();
        Console.WriteLine(node);
        
        foreach (T neighbor in graph.GetNeighbors(node)) {
            if (!visited.Contains(neighbor)) {
                visited.Add(neighbor);
                queue.Enqueue(neighbor);
            }
        }
    }
}
```

**C# Notes:**
- `Stack<T>` and `Queue<T>` are built-in; no need for manual implementation
- Both use arrays internally with O(1) amortized operations
- For circular queue: C#'s `Queue<T>` handles it (no visible indexing)

---

### Pattern 5: BINARY SEARCH & INVARIANTS (Day 5)

**C# Mental Model:** Invariant-based thinking: maintain shrinking search space where target must exist, halve it each iteration.

**When to Use:**
- ✅ Find value in sorted array (O(log n) vs O(n) linear)
- ✅ Lower/upper bounds (first element >= x)
- ✅ Binary search on answer space (minimize/maximize feasible)
- ✅ Rotated sorted array, peak finding

**Core C# Skeleton:**

```csharp
public static class BinarySearchPatterns {
    // Classic exact match
    public static int Search(int[] arr, int target) {
        int low = 0, high = arr.Length - 1;
        
        while (low <= high) {
            int mid = low + (high - low) / 2;
            if (arr[mid] == target) return mid;
            
            if (arr[mid] < target) {
                low = mid + 1;
            } else {
                high = mid - 1;
            }
        }
        
        return -1;
    }
    
    // First occurrence (leftmost)
    public static int FindFirst(int[] arr, int target) {
        int low = 0, high = arr.Length - 1;
        int result = -1;
        
        while (low <= high) {
            int mid = low + (high - low) / 2;
            if (arr[mid] == target) {
                result = mid;
                high = mid - 1;  // Keep searching left
            } else if (arr[mid] < target) {
                low = mid + 1;
            } else {
                high = mid - 1;
            }
        }
        
        return result;
    }
    
    // Lower bound: first element >= target
    public static int LowerBound(int[] arr, int target) {
        int low = 0, high = arr.Length;
        
        while (low < high) {
            int mid = low + (high - low) / 2;
            if (arr[mid] < target) {
                low = mid + 1;
            } else {
                high = mid;
            }
        }
        
        return low;
    }
    
    // Binary search on answer: minimize max load
    public static int MinimizeMaxLoad(int[] tasks, int k) {
        int low = tasks.Max();
        int high = tasks.Sum();
        
        while (low < high) {
            int mid = low + (high - low) / 2;
            
            if (CanDistribute(tasks, k, mid)) {
                high = mid;  // Try smaller
            } else {
                low = mid + 1;  // Need larger
            }
        }
        
        return low;
    }
    
    private static bool CanDistribute(int[] tasks, int k, int maxLoad) {
        int workers = 1;
        int currentLoad = 0;
        
        foreach (int task in tasks) {
            if (currentLoad + task <= maxLoad) {
                currentLoad += task;
            } else {
                workers++;
                currentLoad = task;
                if (workers > k) return false;
            }
        }
        
        return true;
    }
}
```

**C# Notes:**
- C# has `Array.BinarySearch()` built-in (returns negative index if not found)
- `SortedSet<T>` uses binary search internally for O(log n) operations
- For custom binary search on answer space, implement feasibility check carefully

---

## 📊 PROGRESSIVE PROBLEM LADDER (Week 02)

### 🟢 Stage 1: Array & Sequential Access Foundations

| # | Concept | Difficulty | Pattern | C# Focus |
|---|---------|-----------|---------|----------|
| 1 | Sum array sequentially | 🟢 | Sequential iteration | for loop, not foreach index |
| 2 | Find max/min in array | 🟢 | Aggregation | Compare, not nested access |
| 3 | Count elements matching condition | 🟢 | Filtering predicate | Conditional increment |
| 4 | Sum 2D matrix (row-major order) | 🟡 | 2D layout awareness | Nested for, correct order |
| 5 | Transpose matrix cache-friendly | 🟡 | Memory layout | Tiling or appropriate order |
| 6 | Measure cache effects (benchmark) | 🟠 | Empirical understanding | Stopwatch, stride variation |

---

### 🟡 Stage 2: Dynamic Arrays & Amortized Analysis

| # | Concept | Difficulty | Pattern | C# Focus |
|---|---------|-----------|---------|----------|
| 1 | Implement List<T> with doubling | 🟡 | Growth strategy | Resize logic, capacity tracking |
| 2 | Trace cost for n pushes | 🟡 | Amortized analysis | Count operations, sum series |
| 3 | Compare 1.5x vs 2x growth | 🟠 | Trade-off analysis | Benchmark, measure metrics |
| 4 | Implement Pop with shrinking | 🟠 | Prevent thrashing | Shrink at 1/4, not 1/2 |

---

### 🟡 Stage 3: Linked Lists & Pointers

| # | Concept | Difficulty | Pattern | C# Focus |
|---|---------|-----------|---------|----------|
| 1 | Implement singly linked list | 🟢 | Node structure, pointer updates | Careful null checks |
| 2 | Reverse linked list | 🟡 | Pointer manipulation | Three-pointer technique |
| 3 | Detect cycle (Floyd's) | 🟡 | Fast/slow pointers | Convergence logic |
| 4 | Implement LRU cache | 🟠 | Hybrid (LinkedList + Dict) | Node tracking, eviction |

---

### 🟢 Stage 4: Stacks & Queues

| # | Concept | Difficulty | Pattern | C# Focus |
|---|---------|-----------|---------|----------|
| 1 | Implement stack (array-based) | 🟢 | LIFO semantics | Push/pop, empty check |
| 2 | Balanced parentheses check | 🟢 | Stack for validation | Bracket matching |
| 3 | Postfix expression evaluation | 🟡 | Stack for operator precedence | Token parsing |
| 4 | Implement circular queue | 🟡 | Wrap-around modulo logic | Index wrap, capacity |
| 5 | BFS using queue | 🟡 | FIFO for graph traversal | Neighbor iteration |

---

### 🔵 Stage 5: Binary Search & Optimization

| # | Concept | Difficulty | Pattern | C# Focus |
|---|---------|-----------|---------|----------|
| 1 | Classic binary search | 🟢 | Invariant maintenance | Loop condition, mid calc |
| 2 | Find first/last occurrence | 🟡 | Boundary variants | Different high = mid logic |
| 3 | Lower/upper bounds | 🟡 | STL-style bounds | while < vs <= distinction |
| 4 | Minimize max load (answer space) | 🟠 | Binary search on feasibility | Custom predicate |
| 5 | Aggressive cows (maximize min distance) | 🟠 | Answer space pattern | Distance feasibility check |

---

## ⚠ WEEK 02 C# PITFALLS & GOTCHAS

| Pattern | Common Bug | C# Symptom | Quick Fix |
|---------|-----------|-----------|-----------|
| Sequential Access | Accessing arr[randomIndex] in inner loop | Cache thrashing, 10-50x slowdown | Reorder loops or use iterator |
| Dynamic Array | Not accounting for amortized cost in tight loops | Performance worse than expected | Use pre-allocation (Capacity property) |
| Linked List | Calling `.Find()` repeatedly in loop | O(n²) total time | Get node reference once, iterate |
| Linked List | Forgetting to update next/prev pointers | Broken links, traversal fails | Use AddAfter/AddBefore, not manual |
| Stack | Confusing Push with Peek (both O(1)) | Wrong value consumed | Push adds, Pop removes; Peek doesn't remove |
| Queue | Using List<T> with RemoveAt(0) | O(n) per dequeue (terrible) | Use Queue<T> (circular buffer) |
| Binary Search | Off-by-one in loop condition | Infinite loop or wrong answer | high = mid - 1 for exact, high = mid for bounds |
| Binary Search | Calling CanDistribute in each binary search iteration | Forgetting cost of feasibility check | Binary search is O(log n × cost_of_check), not just O(log n) |

---

## ✅ WEEK 02 COMPLETION CHECKLIST

**Pattern Fluency:**
- [ ] Recognize sequential vs random access patterns and their performance implications
- [ ] Understand amortized analysis (why doubling gives O(1) per push)
- [ ] Implement linked list operations (insert, delete, reversal)
- [ ] Distinguish stack (LIFO) and queue (FIFO) semantics
- [ ] Apply binary search and variants (exact, bounds, answer space)

**Problem Solving:**
- [ ] Solved Stage 1 array problems (all 6)
- [ ] Solved 80%+ Stage 2 dynamic array (3+ of 4)
- [ ] Solved 80%+ Stage 3 linked list (3+ of 4)
- [ ] Solved 80%+ Stage 4 stack/queue (4+ of 5)
- [ ] Solved 80%+ Stage 5 binary search (4+ of 5)

**C# Implementation:**
- [ ] No off-by-one errors in loops and array accesses
- [ ] Proper null checking in linked list operations
- [ ] Correct use of Stack<T>, Queue<T>, LinkedList<T>
- [ ] Binary search with invariant maintenance
- [ ] Understanding cache-friendly iteration patterns

**Performance Awareness:**
- [ ] Can explain why row-major iteration is 3-10x faster than column-major
- [ ] Can estimate amortized cost of dynamic array operations
- [ ] Understand pointer-chasing cost in linked lists
- [ ] Know when to use each data structure (arrays vs lists)
- [ ] Can measure performance differences empirically

**Week 02 Integration Ready:** [ ] Yes (All above checks passed)

---

**Status:** ✅ Week 02 Extended C# Roadmap Complete  
**Next:** Proceed to Week 03 or continue Week 02 problem drilling


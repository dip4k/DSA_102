# 🗺️ Week 01 Problem-Solving Roadmap Extended C# — Production-Grade Implementations

**Version:** v1.0  
**Purpose:** Week 01–specific C# problem-solving playbook for Foundations (RAM, Asymptotics, Space, Recursion)  
**Target:** Transform mental models into C# coding fluency  
**Prerequisites:** Week 01 instructional files + standard support files complete  

---

## 🎯 WEEK 01 PROBLEM-SOLVING FRAMEWORK

**Decision Tree (Week 01 Patterns):**

| Problem Signals/Phrases | 🎯 Primary Pattern | C# Collection | Time/Space | Day |
|------------------------|-------------------|---------------|-----------|-----|
| "Analyze complexity" / "Compare growth" | Asymptotic Analysis | N/A (conceptual) | O(1) analysis | Day 2 |
| "Measure memory usage" / "Stack vs Heap" | Memory Layout Understanding | Arrays, Lists | O(n) space estimation | Day 3 |
| "Implement factorial/sum recursively" | Linear Recursion | Recursion + Call Stack | O(n) time / O(n) space | Day 4 |
| "Why is Fibonacci slow?" / "Compute Fib(40)" | Tree Recursion with Overlap | Dictionary<int, long> (memo) | O(2^n) → O(n) | Day 5 |
| "Find max/min in array recursively" | Recursive Aggregation | Recursion + Compare | O(n) time / O(n) space | Day 4 |

**Anti-Patterns:**
- ❌ Using recursion for very deep problems (> 10,000 levels) → Use iteration with explicit stack instead
- ❌ Memoizing divide-and-conquer algorithms → No overlapping subproblems; wastes memory
- ❌ Ignoring base case in recursion → Infinite recursion, stack overflow guaranteed
- ❌ Assuming recursion is always slower than iteration → For shallow recursion, difference is negligible

---

## 💻 C# PATTERN IMPLEMENTATIONS (Week 01)

### Pattern 1: LINEAR RECURSION (Days 4–5)

**C# Mental Model:** Like calling yourself to process the next item, then combining results. Each call makes exactly one recursive call. No branching. Safe and simple.

**When to Use:**
- ✅ Processing arrays element-by-element
- ✅ Computing single-path sequences (factorial, sum, reverse)
- ✅ Walking linked lists from head to tail
- ✅ String operations that process character-by-character

**Core C# Skeleton:**

```csharp
// Linear Recursion - Sum an array from index i onward
public static int SumArray(int[] arr, int i) {
    // Guard: Base case (stopping condition is critical)
    if (i >= arr.Length) return 0;
    
    // Recursive case: current element + recursion on rest
    // This is linear because we make ONLY ONE recursive call per invocation
    return arr[i] + SumArray(arr, i + 1);
}

// Trace for SumArray([10, 20, 30], 0):
// SumArray(0) → 10 + SumArray(1)
//                    → 20 + SumArray(2)
//                         → 30 + SumArray(3)
//                              → 0 [base case]
// Stack depth at max: 4 frames (indices 0, 1, 2, 3)
// Total calls: 4 (linear in array size)
```

**C# Notes:**
- Use `i < arr.Length` (not `<=`) to avoid off-by-one errors when checking base case
- Always define base case *before* recursive case to make termination explicit

**Implementation Variations:**

```csharp
// Variant 1A: Sum with accumulator (tail-recursive, TCO-eligible in functional languages)
public static int SumAccumulator(int[] arr, int i, int acc) {
    if (i >= arr.Length) return acc;
    return SumAccumulator(arr, i + 1, acc + arr[i]);  // Tail call (last operation)
}

// Variant 1B: Reverse a string recursively
public static string ReverseString(string s, int index) {
    // Base case: single character or empty
    if (index < 0) return "";
    // Recursive: take current char, append rest of reversed string
    return ReverseString(s, index - 1) + s[index];
}

// Variant 1C: Find max in array
public static int FindMax(int[] arr, int i) {
    if (i == arr.Length - 1) return arr[i];  // Base: last element is max of itself
    int maxRest = FindMax(arr, i + 1);       // Get max of rest
    return Math.Max(arr[i], maxRest);        // Compare current with rest
}

// Variant 1D: Check if array is sorted
public static bool IsSorted(int[] arr, int i) {
    // Base: reached end (entire array is sorted)
    if (i == arr.Length - 1) return true;
    
    // Recursive: current element <= next AND rest is sorted
    // Short-circuit: if current > next, immediately return false
    return arr[i] <= arr[i + 1] && IsSorted(arr, i + 1);
}
```

**C# Pitfalls:**
- ⚠️ Off-by-one in base case: `if (i > arr.Length)` skips last element
- ⚠️ Missing base case: Infinite recursion → `StackOverflowException`
- ⚠️ Modifying array during recursion: Side effects are dangerous; prefer immutability

---

### Pattern 2: TREE RECURSION WITH OVERLAPPING SUBPROBLEMS (Day 5)

**C# Mental Model:** Like a function that calls itself multiple times (usually 2+) and combines results. If the same subproblems are computed repeatedly, it's exponential. Memoization caches results.

**When to Use:**
- ✅ Fibonacci sequences
- ✅ Climbing stairs (1 or 2 steps at a time)
- ✅ Coin change (minimum coins)
- ✅ Subset generation (all combinations)
- ✅ Any problem with recurrence like `f(n) = f(n-1) + f(n-2)`

**Core C# Skeleton (Without Memoization — Slow):**

```csharp
// Tree Recursion - Naive Fibonacci (exponentially slow!)
public static long FibNaive(int n) {
    // Base cases
    if (n == 0) return 0;
    if (n == 1) return 1;
    
    // Recursive case: TWO recursive calls
    // This branches exponentially: Fib(n-1) + Fib(n-2)
    // For n=40, this makes ~2^40 ≈ 1 trillion calls
    return FibNaive(n - 1) + FibNaive(n - 2);
}
// ❌ DO NOT USE for n > 40 without memoization
```

**Core C# Skeleton (With Memoization — Fast):**

```csharp
// Tree Recursion with Memoization - Fibonacci (polynomial time)
public static long FibMemoized(int n) {
    // Memo: Dictionary caches results to avoid recomputation
    var memo = new Dictionary<int, long>();
    return FibHelper(n, memo);
}

private static long FibHelper(int n, Dictionary<int, long> memo) {
    // Base cases
    if (n == 0) return 0;
    if (n == 1) return 1;
    
    // Check memo first: O(1) lookup
    if (memo.ContainsKey(n)) {
        return memo[n];  // Cache hit: instant return
    }
    
    // Cache miss: compute recursively
    long result = FibHelper(n - 1, memo) + FibHelper(n - 2, memo);
    
    // Store in memo for next time
    memo[n] = result;
    return result;
}
// ✅ For n=40: only 41 distinct subproblems, runs instantly
```

**C# Notes:**
- `Dictionary<int, long>`: Key is subproblem (n), Value is cached result
- Check memo *before* recursing to avoid redundant computation
- Initialize memo once at top level, pass it down to all recursive calls

**Implementation Variations:**

```csharp
// Variant 2A: Array-based memo (faster for dense integer keys)
public static long FibArray(int n) {
    var memo = new long[n + 1];
    Array.Fill(memo, -1);  // -1 means "not computed"
    return FibArrayHelper(n, memo);
}

private static long FibArrayHelper(int n, long[] memo) {
    if (n == 0) return 0;
    if (n == 1) return 1;
    if (memo[n] != -1) return memo[n];  // Cache hit
    
    long result = FibArrayHelper(n - 1, memo) + FibArrayHelper(n - 2, memo);
    memo[n] = result;
    return result;
}

// Variant 2B: Climbing Stairs (1 or 2 steps at a time, how many ways?)
public static int ClimbStairs(int n) {
    var memo = new Dictionary<int, int>();
    return ClimbHelper(n, memo);
}

private static int ClimbHelper(int n, Dictionary<int, int> memo) {
    if (n == 0) return 1;  // One way: don't move
    if (n == 1) return 1;  // One way: take 1 step
    
    if (memo.ContainsKey(n)) return memo[n];
    
    // From step (n-1), take 1 more step. Or from (n-2), take 2 more steps.
    int result = ClimbHelper(n - 1, memo) + ClimbHelper(n - 2, memo);
    memo[n] = result;
    return result;
}
// Trace for n=5: 8 ways (same as Fibonacci due to recurrence structure)

// Variant 2C: Minimum Coin Change (find min coins to make amount)
public static int MinCoinChange(int[] coins, int amount) {
    var memo = new Dictionary<int, int>();
    return MinCoinHelper(coins, amount, memo);
}

private static int MinCoinHelper(int[] coins, int amount, Dictionary<int, int> memo) {
    // Base cases
    if (amount == 0) return 0;  // Need 0 coins for amount 0
    if (amount < 0) return -1;  // Invalid
    
    if (memo.ContainsKey(amount)) return memo[amount];
    
    int minCoins = int.MaxValue;
    foreach (int coin in coins) {
        int remaining = MinCoinHelper(coins, amount - coin, memo);
        if (remaining >= 0) {  // Only if valid
            minCoins = Math.Min(minCoins, remaining + 1);
        }
    }
    
    memo[amount] = (minCoins == int.MaxValue) ? -1 : minCoins;
    return memo[amount];
}

// Variant 2D: Count subsets with sum equal to target
public static int CountSubsetsWithSum(int[] arr, int target) {
    var memo = new Dictionary<string, int>();
    return CountHelper(arr, 0, target, memo);
}

private static int CountHelper(int[] arr, int i, int remaining, Dictionary<string, int> memo) {
    // Composite key: (index, remaining sum)
    string key = $"{i},{remaining}";
    
    if (memo.ContainsKey(key)) return memo[key];
    
    // Base cases
    if (remaining == 0) return 1;  // Found a subset
    if (i >= arr.Length) return 0; // No more elements
    if (remaining < 0) return 0;   // Over-target
    
    // Recursive: include arr[i] or exclude arr[i]
    int result = CountHelper(arr, i + 1, remaining - arr[i], memo)  // Include
                + CountHelper(arr, i + 1, remaining, memo);          // Exclude
    
    memo[key] = result;
    return result;
}
```

**C# Pitfalls:**
- ⚠️ Memoizing without overlapping subproblems (e.g., divide-and-conquer): Wastes memory, slows code
- ⚠️ Dictionary key collisions: If your key is poorly chosen, cache behaves incorrectly
- ⚠️ Thread safety: Multiple threads accessing same memo → race conditions (use `ConcurrentDictionary`)

---

### Pattern 3: UNDERSTANDING RECURSION DEPTH & STACK (Day 4)

**C# Mental Model:** The call stack grows with each recursive call. Depth = max number of concurrent frames. Deep recursion (> 10,000) risks `StackOverflowException`.

**When to Use (and How Deep):**
- ✅ n < 1,000: Safe on most systems
- ⚠️ 1,000 < n < 10,000: Risky; depends on frame size and system
- ❌ n > 10,000: Likely to crash; use iteration instead

**Core C# Skeleton (Measuring Depth):**

```csharp
// Trace recursion depth to predict stack overflow risk
public static int MeasureDepth(int n, int currentDepth = 0) {
    // Track current depth and max depth seen
    int maxDepth = currentDepth;
    
    if (n <= 0) {
        Console.WriteLine($"Base case at depth {currentDepth}");
        return currentDepth;
    }
    
    // Recurse and track depth
    int deeperDepth = MeasureDepth(n - 1, currentDepth + 1);
    return Math.Max(maxDepth, deeperDepth);
}

// Usage:
int depth = MeasureDepth(5);
Console.WriteLine($"Max recursion depth: {depth}");  // Output: 5
// For n=5: depth is 5 (linear recursion)
// For Fibonacci: depth is n (even though branches are exponential)
```

**C# Notes:**
- Recursion depth for linear recursion = n
- Recursion depth for tree recursion like Fibonacci = n (deepest path is linear)
- Total function calls ≠ depth (Fibonacci makes 2^n calls but depth is n)

**Implementation Variations (Depth Analysis):**

```csharp
// Variant 3A: Iterative sum (no recursion, no stack risk)
public static int SumIterative(int[] arr) {
    int total = 0;
    foreach (int x in arr) {
        total += x;
    }
    return total;
}
// Depth: 0 (no recursion)

// Variant 3B: Convert deep recursion to iteration with explicit stack
public static int SumIterativeWithStack(int[] arr) {
    var stack = new Stack<(int[] array, int index)>();
    stack.Push((arr, 0));
    
    int total = 0;
    while (stack.Count > 0) {
        var (array, i) = stack.Pop();
        if (i >= array.Length) continue;
        
        total += array[i];
    }
    return total;
}

// Variant 3C: Tail-call recursion (can be optimized by compiler)
public static long FactorialTail(int n, long acc = 1) {
    // Tail call: the recursive call is the LAST operation
    if (n == 0) return acc;
    return FactorialTail(n - 1, n * acc);  // Tail call (eligible for TCO)
}
// Note: C# compiler does NOT do TCO, but the structure is tail-recursive

// Variant 3D: Detect if recursion is tail-recursive
public static int BadTailRecursion(int n) {
    if (n == 0) return 1;
    int sub = BadTailRecursion(n - 1);
    return n * sub;  // NOT tail-recursive: multiply after recursion
}

public static int GoodTailRecursion(int n, int acc = 1) {
    if (n == 0) return acc;
    return GoodTailRecursion(n - 1, acc * n);  // Tail-recursive: pass acc
}
```

**C# Pitfalls:**
- ⚠️ C# runtime does NOT optimize tail calls (unlike Scheme, Scala with @tailrec)
- ⚠️ Assuming `ref` or `out` parameters reduce stack usage: They don't; frames still allocated
- ⚠️ Recursive calls in loops: Each iteration adds a frame; can cause stack overflow

---

### Pattern 4: ASYMPTOTIC ANALYSIS IN PRACTICE (Day 2)

**C# Mental Model:** Big-O describes how algorithm scales as input size grows. O(1), O(log n), O(n), O(n log n), O(n²), O(2^n). Understanding this guides design choices.

**When to Use:**
- ✅ Choosing between algorithms (quick sort vs bubble sort)
- ✅ Predicting performance at scale (will n=1,000,000 work?)
- ✅ Optimizing slow code (which operation is the bottleneck?)

**Core C# Skeleton (Empirical Complexity Analysis):**

```csharp
// Measure actual runtime and infer complexity
public static void AnalyzeComplexity() {
    for (int n = 100; n <= 1_000_000; n *= 2) {
        var sw = System.Diagnostics.Stopwatch.StartNew();
        
        // Run algorithm multiple times for accurate measurement
        for (int trial = 0; trial < 100; trial++) {
            int[] arr = new int[n];
            for (int i = 0; i < n; i++) arr[i] = i;
            
            // Example: Linear search O(n)
            LinearSearch(arr, -1);
        }
        
        sw.Stop();
        double ratioToPrevious = n > 100 ? 2.0 : 0;  // n doubled, time should roughly double
        Console.WriteLine($"n={n:N0}: {sw.ElapsedMilliseconds}ms (ratio ≈ {ratioToPrevious})");
    }
}

private static int LinearSearch(int[] arr, int target) {
    for (int i = 0; i < arr.Length; i++) {
        if (arr[i] == target) return i;
    }
    return -1;  // Not found
}
```

**C# Notes:**
- Use `System.Diagnostics.Stopwatch` for precise timing
- Run multiple trials to average out noise
- For rough estimates: O(n) doubles time when n doubles; O(n²) quadruples; O(log n) adds constant

**Implementation Variations (Common Complexities):**

```csharp
// O(1) - Constant time (direct array access)
public static int Get(int[] arr, int index) => arr[index];

// O(log n) - Logarithmic (binary search)
public static int BinarySearch(int[] arr, int target) {
    int low = 0, high = arr.Length - 1;
    while (low <= high) {
        int mid = (low + high) / 2;
        if (arr[mid] == target) return mid;
        if (arr[mid] < target) low = mid + 1;
        else high = mid - 1;
    }
    return -1;
}

// O(n) - Linear (single pass)
public static int LinearSearch(int[] arr, int target) {
    foreach (int x in arr) if (x == target) return x;
    return -1;
}

// O(n log n) - Linearithmic (merge sort, quick sort)
public static void QuickSort(int[] arr, int low, int high) {
    if (low < high) {
        int pi = Partition(arr, low, high);
        QuickSort(arr, low, pi - 1);      // O(log n) depth
        QuickSort(arr, pi + 1, high);     // O(n) work per level
    }
}

private static int Partition(int[] arr, int low, int high) {
    int pivot = arr[high];
    int i = low - 1;
    for (int j = low; j < high; j++) {
        if (arr[j] < pivot) {
            i++;
            (arr[i], arr[j]) = (arr[j], arr[i]);
        }
    }
    (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
    return i + 1;
}

// O(n²) - Quadratic (bubble sort, naive nested loops)
public static void BubbleSort(int[] arr) {
    for (int i = 0; i < arr.Length; i++) {
        for (int j = 0; j < arr.Length - 1 - i; j++) {
            if (arr[j] > arr[j + 1]) {
                (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
            }
        }
    }
}

// O(2^n) - Exponential (naive Fibonacci, subsets)
public static long FibExponential(int n) {
    if (n <= 1) return n;
    return FibExponential(n - 1) + FibExponential(n - 2);  // 2^n calls
}
```

**C# Pitfalls:**
- ⚠️ Confusing "worst case" with "average case": Quicksort is O(n²) worst, O(n log n) average
- ⚠️ Hidden constants: O(100n) can be faster than O(n log n) for moderate n
- ⚠️ Measuring on small inputs: For n < 1000, constants dominate; Big-O matters at scale

---

## 📊 PROGRESSIVE PROBLEM LADDER (Week 01)

### 🟢 Stage 1: Linear Recursion Foundations

| # | Concept | Difficulty | Pattern | C# Focus |
|---|---------|-----------|---------|----------|
| 1 | Sum array recursively | 🟢 | Linear Recursion | Guard clause (base case) |
| 2 | Factorial | 🟢 | Linear Recursion | Simple return + multiply |
| 3 | Reverse string recursively | 🟢 | Linear Recursion | String concatenation |
| 4 | Check palindrome (recursive) | 🟢 | Linear Recursion | Two-pointer (conceptual) |
| 5 | Find max/min in array (recursive) | 🟢 | Linear Recursion + Compare | Math.Max/Min helper |
| 6 | Count elements matching condition | 🟢 | Linear Recursion + Count | Conditional increment |
| 7 | All are positive (recursive check) | 🟢 | Linear Recursion + Boolean | Short-circuit with && |
| 8 | Print array in order (recursive) | 🟢 | Linear Recursion + Side Effect | Console.WriteLine |

**Canonical Problems (LeetCode-style):**
- Sum of array: `SumArray([10, 20, 30])` → `60`
- Factorial: `Factorial(5)` → `120`
- Reverse: `ReverseString("hello")` → `"olleh"`

---

### 🟡 Stage 2: Overlapping Subproblems & Memoization

| # | Concept | Difficulty | Pattern+Twist | C# Focus |
|---|---------|-----------|---|----------|
| 1 | Fibonacci (naive vs memoized) | 🟡 | Tree Recursion + Memo | Dictionary<int, long> |
| 2 | Climbing stairs (memoized) | 🟡 | Tree Recursion + Memo | Composite recurrence |
| 3 | Min coin change | 🟡 | Tree Recursion + Optimization | int.MaxValue sentinel |
| 4 | Count subsets with sum | 🟡 | Tree Recursion + Memo | Composite key (i, sum) |
| 5 | Longest common subsequence (simple) | 🟡 | Tree Recursion + Memo | String character iteration |
| 6 | Count all permutations (memoized) | 🟡 | Tree Recursion + Memo | Visited set tracking |
| 7 | Word break (recursive + memo) | 🟡 | Tree Recursion + Memo | String substring checks |
| 8 | Knights tour (with memoization) | 🟡 | Tree Recursion + Memo | 2D memo for positions |

**Key Insight:** Memoization transforms exponential time into polynomial. Always check memo first.

---

### 🟠 Stage 3: Integration & Depth Analysis

| # | Concept | Difficulty | Integration | C# Focus |
|---|---------|-----------|---|----------|
| 1 | Recursion depth impact (measure) | 🟠 | Depth Analysis | Track frame count |
| 2 | Convert recursion to iteration | 🟠 | Recursion + Iteration | Explicit Stack<T> |
| 3 | Asymptotic analysis on mixed recursion | 🟠 | Asymptotics + Recursion | Reasoning about T(n) |
| 4 | Tail recursion optimization (theory) | 🟠 | Tail-Recursion Patterns | Accumulator parameter |
| 5 | Compare all four: recursion, memo, iteration, DP | 🟠 | Multi-approach | Space/time trade-offs |
| 6 | Stack overflow detection & prevention | 🟠 | Depth Constraints | Guard depth checks |
| 7 | Mutual recursion (IsEven/IsOdd) | 🟠 | Indirect Recursion | Back-and-forth calls |
| 8 | Hybrid: recursive with iterative base | 🟠 | Mixed Approaches | Adaptive strategies |

**Critical Integration:** Understand *why* Fibonacci is exponential (tree with overlap) vs *why* QuickSort is n log n (divide-and-conquer unique subproblems).

---

## ⚠ WEEK 01 PITFALLS & C# GOTCHAS

| Pattern | Common Bug | C# Symptom | Quick Fix |
|---------|-----------|-----------|-----------|
| Linear Recursion | Off-by-one in base case | Wrong result or infinite loop | Use `i < arr.Length`, not `<=` |
| Linear Recursion | Missing base case | `StackOverflowException` | Add `if (i >= arr.Length) return;` at start |
| Tree Recursion | No memoization on overlapping subproblems | Program hangs (exponential) | Wrap in memo Dictionary<int, T> |
| Tree Recursion | Memoizing divide-and-conquer | Wastes memory, no speedup | Only memoize if subproblems repeat |
| Recursion Depth | Assuming C# does TCO | Tail-recursive still uses stack | Manually convert to iteration if deep |
| Asymptotics | Confusing O(n²) worst with O(n log n) average | Wrong algorithm choice | Profile real-world data |
| Space Analysis | Forgetting call stack cost | Mis-estimating space complexity | Add O(depth) to auxiliary space |
| Memoization | Using mutable objects as keys | Dictionary collisions / wrong cache hits | Use immutable keys or value types |
| Memoization | Thread-unsafe Dictionary | Race conditions in multithreaded | Use `ConcurrentDictionary<K, V>` |
| Recursion | Modifying input array during recursion | Side effects, hard to debug | Pass indices, don't mutate |

**Week 01 Collection Guide:**
- ✅ `int[]` / `List<int>`: Fixed/dynamic arrays for storing data
- ✅ `Dictionary<TKey, TValue>`: Memoization cache (key = subproblem, value = result)
- ✅ `Stack<T>`: Explicit stack for converting recursion to iteration
- ✅ `HashSet<T>`: Visited set for traversal (e.g., detecting cycles, finding unique elements)

---

## ✅ WEEK 01 COMPLETION CHECKLIST

**Pattern Fluency:**
- [ ] Recall linear recursion skeleton (base case + recursive case)
- [ ] Recall tree recursion skeleton (multiple calls + combine results)
- [ ] Write memoization wrapper (Dictionary check + store)
- [ ] Convert linear recursion to iteration (explicit loop)
- [ ] Identify overlapping subproblems in recurrence

**Problem Solving:**
- [ ] Solved Stage 1 linear recursion (all 8 problems)
- [ ] Solved 80%+ Stage 2 memoization (6+ of 8)
- [ ] Attempted Stage 3 integration (at least 4 of 8)

**C# Implementation:**
- [ ] Used correct base cases (no off-by-one)
- [ ] Handled edge cases (empty arrays, n=0, n=1)
- [ ] Memoized overlapping subproblems correctly
- [ ] No `StackOverflowException` on reasonable inputs (n < 1000)
- [ ] Measured recursion depth to ensure stack safety

**Asymptotic Reasoning:**
- [ ] Can classify function as O(n), O(n²), O(2^n), etc.
- [ ] Can explain why Fibonacci is exponential
- [ ] Can estimate runtime scaling (double input → double time?)
- [ ] Can identify redundant work and memoization targets

**Week 01 Integration Ready:** [ ] Yes (All above checks passed)

---

## 🧪 EXAMPLE IMPLEMENTATIONS (Quick Reference)

**Example 1: Factorial (Linear Recursion)**
```csharp
public static long Factorial(int n) {
    if (n < 0) return -1;  // Invalid
    if (n == 0) return 1;  // Base case
    return n * Factorial(n - 1);  // Recursive: multiply, then recurse
}
// Trace: Factorial(5) = 5 * Factorial(4) = 5 * 4 * 3 * 2 * 1 * 1 = 120
// Depth: 6 frames (n=5, 4, 3, 2, 1, 0)
// Calls: 6 (linear)
```

**Example 2: Fibonacci (Tree Recursion + Memoization)**
```csharp
public static long FibMemo(int n) {
    var memo = new Dictionary<int, long>();
    return FibHelper(n, memo);
}

private static long FibHelper(int n, Dictionary<int, long> memo) {
    if (n <= 1) return n;
    if (memo.ContainsKey(n)) return memo[n];  // Cache hit
    
    long result = FibHelper(n - 1, memo) + FibHelper(n - 2, memo);
    memo[n] = result;
    return result;
}
// Without memo: 2^n calls (exponential, hangs at n=40)
// With memo: n calls (polynomial, instant)
```

**Example 3: Convert Recursion to Iteration**
```csharp
// Recursive
public static int SumRecursive(int[] arr, int i) {
    if (i >= arr.Length) return 0;
    return arr[i] + SumRecursive(arr, i + 1);
}

// Iterative (no stack overflow risk)
public static int SumIterative(int[] arr) {
    int total = 0;
    for (int i = 0; i < arr.Length; i++) {
        total += arr[i];
    }
    return total;
}
// Same result, but iterative is safer for large arrays
```

---

## 🎓 WEEK 01 MASTERY GOALS

By week's end, you should be able to:

1. **Write recursive functions confidently** with clear base cases and proper progress toward termination
2. **Recognize overlapping subproblems** and apply memoization to transform exponential algorithms into polynomial
3. **Analyze recursion depth** and predict stack overflow risk
4. **Convert recursion to iteration** using explicit stacks when depth is a concern
5. **Reason about Big-O complexity** and understand why some algorithms are exponential while others are linear/logarithmic
6. **Choose the right C# collections** (Dictionary for memoization, Stack for iteration, etc.)

---

**Status:** ✅ Week 01 Extended C# Roadmap Complete  
**Next:** Proceed to Week 01 Daily Progress Checklist or move to Week 02 content


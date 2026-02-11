# 🧱 Stacks + Queues Mastery — Checklist Syllabus + 25 Drills (No Solutions) with Runnable Driver

This pack focuses on mastering **stack (LIFO)** and **queue (FIFO)** behaviors and the high-frequency algorithm patterns built on top of them. [web:136][web:143]  
It includes a checklist syllabus, and a runnable **C# console driver** with **inputs + expected outputs** but **no solutions** (all drill functions are TODOs).

---

## ✅ Checklist syllabus (what to cover)

### A) Foundations (data structure correctness)
- Stack operations: push, pop, peek, isEmpty, size, underflow behavior. [web:136]
- Queue operations: enqueue, dequeue, front/peek, isEmpty, size; circular buffer queue idea. [web:140][web:146]
- Amortized O(1) resizing for array-backed implementations (know conceptually).
- Trace discipline: what is the **frontier** and what invariant does it maintain?

### B) Stack patterns
- Parentheses / bracket validation.
- Expression evaluation: postfix (RPN), infix parsing (shunting-yard), basic calculator.
- “Undo” / backtracking stack for state.
- Monotonic stack: next greater element, daily temperatures, largest rectangle.

### C) Queue / BFS patterns
- BFS frontier: visited, distance, parent for path reconstruction.
- Multi-source BFS (rotting oranges style).
- Topological sort (Kahn’s algorithm) using a queue.

### D) Deque patterns
- Monotonic deque: sliding window maximum (LC239). [web:144]
- Monotonic deque + prefix sums (advanced): shortest subarray with sum ≥ K.

### E) “Conversion” patterns
- Queue using two stacks; stack using two queues.
- MinStack (stack + auxiliary mins).

---

# ✅ How to run (C#)

1) Create a new console app:
```bash
dotnet new console -n StackQueueDrills
cd StackQueueDrills
```

2) Replace `Program.cs` with the code below.

3) Run all drills:
```bash
dotnet run
```

4) Run one drill (example):
```bash
dotnet run D11
```

---

# 📌 Program.cs (driver + expected outputs + TODO drills)

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public sealed class Program
{
    public static void Main(string[] args)
    {
        try
        {
            var drillId = args.Length > 0 ? args[0].Trim().ToUpperInvariant() : "ALL";
            new DrillRunner().Run(drillId);
            Console.WriteLine("✅ Done");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("❌ " + ex.Message);
            Environment.ExitCode = 1;
        }
    }
}

// ---------------------------
// Assertions (minimal)
// ---------------------------

public static class AssertEx
{
    public static void True(bool cond, string msg)
    {
        if (!cond) throw new Exception(msg);
    }

    public static void Equal<T>(T expected, T actual, string msg)
        where T : IEquatable<T>
    {
        if (!expected.Equals(actual))
            throw new Exception(msg + $"\nExpected: {expected}\nActual:   {actual}");
    }

    public static void SeqEqual<T>(IList<T> expected, IList<T> actual, string msg)
    {
        if (expected.Count != actual.Count)
            throw new Exception(msg + $"\nExpected length: {expected.Count}\nActual length:   {actual.Count}\nExpected: [{string.Join(",", expected)}]\nActual:   [{string.Join(",", actual)}]");

        for (int i = 0; i < expected.Count; i++)
        {
            if (!EqualityComparer<T>.Default.Equals(expected[i], actual[i]))
                throw new Exception(msg + $"\nMismatch at i={i} expected={expected[i]} actual={actual[i]}\nExpected: [{string.Join(",", expected)}]\nActual:   [{string.Join(",", actual)}]");
        }
    }

    public static void SeqSeqEqual<T>(IList<IList<T>> expected, IList<IList<T>> actual, string msg)
    {
        if (expected.Count != actual.Count)
            throw new Exception(msg + $"\nExpected outer length: {expected.Count}\nActual outer length:   {actual.Count}");

        for (int i = 0; i < expected.Count; i++)
            SeqEqual(expected[i].ToList(), actual[i].ToList(), msg + $"\n(Level {i})");
    }
}

// ---------------------------
// Drill runner
// ---------------------------

public sealed class DrillRunner
{
    public void Run(string drillId)
    {
        var drills = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
        {
            ["D1"]  = Test_D1_ArrayStack,
            ["D2"]  = Test_D2_CircularQueue,
            ["D3"]  = Test_D3_QueueUsingTwoStacks,
            ["D4"]  = Test_D4_StackUsingTwoQueues,
            ["D5"]  = Test_D5_MinStack,

            ["D6"]  = Test_D6_ValidParentheses,
            ["D7"]  = Test_D7_RemoveAdjacentDuplicates,
            ["D8"]  = Test_D8_EvalRpn,
            ["D9"]  = Test_D9_SimplifyPath,
            ["D10"] = Test_D10_NextGreater,
            ["D11"] = Test_D11_DailyTemperatures,
            ["D12"] = Test_D12_LargestRectangle,
            ["D13"] = Test_D13_DecodeString,
            ["D14"] = Test_D14_InfixToPostfix,
            ["D15"] = Test_D15_BasicCalculator,

            ["D16"] = Test_D16_BfsShortestPath,
            ["D17"] = Test_D17_ConnectedComponents,
            ["D18"] = Test_D18_TopologicalSort,
            ["D19"] = Test_D19_SlidingWindowMax,
            ["D20"] = Test_D20_FirstNegativeInWindow,
            ["D21"] = Test_D21_RottingOranges,
            ["D22"] = Test_D22_ShortestPathBinaryMatrix,
            ["D23"] = Test_D23_RecentCounter,
            ["D24"] = Test_D24_BfsLevels,
            ["D25"] = Test_D25_ShortestSubarrayAtLeastK,
        };

        if (drillId == "ALL")
        {
            foreach (var kv in drills.OrderBy(k => k.Key))
            {
                kv.Value();
                Console.WriteLine($"✅ {kv.Key} passed");
            }
            return;
        }

        if (!drills.TryGetValue(drillId, out var runOne))
            throw new Exception($"Unknown drill id: {drillId}. Try ALL or D1..D25");

        runOne();
        Console.WriteLine($"✅ {drillId} passed");
    }

    // ---------------------------
    // D1: ArrayStack<T>
    // ---------------------------

    private static void Test_D1_ArrayStack()
    {
        var st = new Drills.ArrayStack<int>();
        st.Push(10);
        st.Push(20);
        st.Push(30);

        AssertEx.Equal(30, st.Peek(), "D1 peek wrong");
        AssertEx.Equal(30, st.Pop(), "D1 pop1 wrong");
        AssertEx.Equal(20, st.Pop(), "D1 pop2 wrong");
        st.Push(40);
        AssertEx.Equal(40, st.Pop(), "D1 pop3 wrong");
        AssertEx.Equal(10, st.Pop(), "D1 pop4 wrong");
        AssertEx.True(st.IsEmpty(), "D1 should be empty");

        bool threw = false;
        try { st.Pop(); } catch (InvalidOperationException) { threw = true; }
        AssertEx.True(threw, "D1 pop on empty should throw InvalidOperationException");
    }

    // ---------------------------
    // D2: CircularQueue<T>
    // ---------------------------

    private static void Test_D2_CircularQueue()
    {
        var q = new Drills.CircularQueue<int>(capacity: 3);
        q.Enqueue(1);
        q.Enqueue(2);
        q.Enqueue(3);

        bool threw = false;
        try { q.Enqueue(4); } catch (InvalidOperationException) { threw = true; }
        AssertEx.True(threw, "D2 enqueue on full should throw InvalidOperationException");

        AssertEx.Equal(1, q.Dequeue(), "D2 dequeue1 wrong");
        q.Enqueue(4); // wrap-around should work
        AssertEx.Equal(2, q.Dequeue(), "D2 dequeue2 wrong");
        AssertEx.Equal(3, q.Dequeue(), "D2 dequeue3 wrong");
        AssertEx.Equal(4, q.Dequeue(), "D2 dequeue4 wrong");
        AssertEx.True(q.IsEmpty(), "D2 should be empty");
    }

    // ---------------------------
    // D3: Queue using two stacks
    // ---------------------------

    private static void Test_D3_QueueUsingTwoStacks()
    {
        var q = new Drills.QueueUsingTwoStacks<int>();
        q.Enqueue(7);
        q.Enqueue(8);
        AssertEx.Equal(7, q.Dequeue(), "D3 dequeue wrong");
        q.Enqueue(9);
        AssertEx.Equal(8, q.Dequeue(), "D3 dequeue wrong");
        AssertEx.Equal(9, q.Dequeue(), "D3 dequeue wrong");
        AssertEx.True(q.IsEmpty(), "D3 should be empty");
    }

    // ---------------------------
    // D4: Stack using two queues
    // ---------------------------

    private static void Test_D4_StackUsingTwoQueues()
    {
        var st = new Drills.StackUsingTwoQueues<int>();
        st.Push(1);
        st.Push(2);
        st.Push(3);
        AssertEx.Equal(3, st.Pop(), "D4 pop1 wrong");
        AssertEx.Equal(2, st.Peek(), "D4 peek wrong");
        AssertEx.Equal(2, st.Pop(), "D4 pop2 wrong");
        AssertEx.Equal(1, st.Pop(), "D4 pop3 wrong");
        AssertEx.True(st.IsEmpty(), "D4 should be empty");
    }

    // ---------------------------
    // D5: MinStack
    // ---------------------------

    private static void Test_D5_MinStack()
    {
        var ms = new Drills.MinStack();
        ms.Push(5);
        ms.Push(3);
        ms.Push(7);
        AssertEx.Equal(3, ms.GetMin(), "D5 min1 wrong");
        AssertEx.Equal(7, ms.Pop(), "D5 pop wrong");
        AssertEx.Equal(3, ms.GetMin(), "D5 min2 wrong");
        AssertEx.Equal(3, ms.Pop(), "D5 pop wrong");
        AssertEx.Equal(5, ms.GetMin(), "D5 min3 wrong");
    }

    // ---------------------------
    // D6: Valid parentheses
    // ---------------------------

    private static void Test_D6_ValidParentheses()
    {
        AssertEx.True(Drills.IsValidParentheses("()[]{}"), "D6 expected true");
        AssertEx.True(!Drills.IsValidParentheses("(]"), "D6 expected false");
        AssertEx.True(!Drills.IsValidParentheses("([)]"), "D6 expected false");
        AssertEx.True(Drills.IsValidParentheses("{[()()]}"), "D6 expected true");
    }

    // ---------------------------
    // D7: Remove adjacent duplicates (stack simulation)
    // ---------------------------

    private static void Test_D7_RemoveAdjacentDuplicates()
    {
        AssertEx.Equal("ca", Drills.RemoveAdjacentDuplicates("abbaca"), "D7 wrong");
        AssertEx.Equal("", Drills.RemoveAdjacentDuplicates("azxxzyy"), "D7 wrong");
        AssertEx.Equal("abcd", Drills.RemoveAdjacentDuplicates("abcd"), "D7 wrong");
    }

    // ---------------------------
    // D8: Evaluate RPN
    // ---------------------------

    private static void Test_D8_EvalRpn()
    {
        AssertEx.Equal(9, Drills.EvalRpn(new []{"2","1","+","3","*"}), "D8 wrong");
        AssertEx.Equal(6, Drills.EvalRpn(new []{"4","13","5","/","+"}), "D8 wrong");
        AssertEx.Equal(22, Drills.EvalRpn(new []{"10","6","9","3","+","-11","*","/","*","17","+","5","+"}), "D8 wrong");
    }

    // ---------------------------
    // D9: Simplify Unix path
    // ---------------------------

    private static void Test_D9_SimplifyPath()
    {
        AssertEx.Equal("/home", Drills.SimplifyPath("/home/"), "D9 wrong");
        AssertEx.Equal("/", Drills.SimplifyPath("/../"), "D9 wrong");
        AssertEx.Equal("/home/foo", Drills.SimplifyPath("/home//foo/"), "D9 wrong");
        AssertEx.Equal("/c", Drills.SimplifyPath("/a/./b/../../c/"), "D9 wrong");
    }

    // ---------------------------
    // D10: Next greater element (to the right)
    // ---------------------------

    private static void Test_D10_NextGreater()
    {
        var got = Drills.NextGreaterRight(new []{2, 1, 2, 4, 3});
        AssertEx.SeqEqual(new List<int>{4,2,4,-1,-1}, got, "D10 wrong");

        got = Drills.NextGreaterRight(new []{5,4,3,2,1});
        AssertEx.SeqEqual(new List<int>{-1,-1,-1,-1,-1}, got, "D10 wrong");
    }

    // ---------------------------
    // D11: Daily temperatures
    // ---------------------------

    private static void Test_D11_DailyTemperatures()
    {
        var got = Drills.DailyTemperatures(new []{73,74,75,71,69,72,76,73});
        AssertEx.SeqEqual(new List<int>{1,1,4,2,1,1,0,0}, got, "D11 wrong");
    }

    // ---------------------------
    // D12: Largest rectangle in histogram
    // ---------------------------

    private static void Test_D12_LargestRectangle()
    {
        AssertEx.Equal(10, Drills.LargestRectangleArea(new []{2,1,5,6,2,3}), "D12 wrong");
        AssertEx.Equal(4, Drills.LargestRectangleArea(new []{2,4}), "D12 wrong");
    }

    // ---------------------------
    // D13: Decode string
    // ---------------------------

    private static void Test_D13_DecodeString()
    {
        AssertEx.Equal("aaabcbc", Drills.DecodeString("3[a]2[bc]"), "D13 wrong");
        AssertEx.Equal("accaccacc", Drills.DecodeString("3[a2[c]]"), "D13 wrong");
        AssertEx.Equal("abcabccdcdcdef", Drills.DecodeString("2[abc]3[cd]ef"), "D13 wrong");
    }

    // ---------------------------
    // D14: Infix to postfix (tokens)
    // Operators: +, -, *, / ; parentheses; integers.
    // ---------------------------

    private static void Test_D14_InfixToPostfix()
    {
        var got = Drills.InfixToPostfixTokens(new []{"3","+","4","*","2","/","(","1","-","5",")"});
        // Expected: 3 4 2 * 1 5 - / +
        AssertEx.SeqEqual(new List<string>{"3","4","2","*","1","5","-","/","+"}, got, "D14 wrong");
    }

    // ---------------------------
    // D15: Basic calculator (+,-, parentheses only)
    // ---------------------------

    private static void Test_D15_BasicCalculator()
    {
        AssertEx.Equal(2, Drills.Calculate("1 + 1"), "D15 wrong");
        AssertEx.Equal(3, Drills.Calculate(" 2-1 + 2 "), "D15 wrong");
        AssertEx.Equal(23, Drills.Calculate("(1+(4+5+2)-3)+(6+8)"), "D15 wrong");
    }

    // ---------------------------
    // D16: BFS shortest path (unweighted) + path reconstruction
    // ---------------------------

    private static void Test_D16_BfsShortestPath()
    {
        int n = 6;
        var edges = new (int u,int v)[] { (0,1),(0,2),(1,3),(2,3),(3,4),(4,5) };
        var (dist, path) = Drills.BfsShortestPath(n, edges, src: 0, dst: 5);
        AssertEx.Equal(4, dist, "D16 distance wrong");
        AssertEx.SeqEqual(new List<int>{0,1,3,4,5}, path, "D16 path wrong (tie-break: smallest parent first)" );
    }

    // ---------------------------
    // D17: Connected components (undirected)
    // ---------------------------

    private static void Test_D17_ConnectedComponents()
    {
        int n = 7;
        var edges = new (int u,int v)[] { (0,1),(1,2),(3,4),(5,6) };
        AssertEx.Equal(3, Drills.CountConnectedComponents(n, edges), "D17 wrong");
    }

    // ---------------------------
    // D18: Topological sort (Kahn)
    // ---------------------------

    private static void Test_D18_TopologicalSort()
    {
        // DAG edges: 5->2, 5->0, 4->0, 4->1, 2->3, 3->1
        int n = 6;
        var edges = new (int u,int v)[] { (5,2),(5,0),(4,0),(4,1),(2,3),(3,1) };
        var got = Drills.TopoSortKahn(n, edges);
        // Expect lexicographically smallest valid order if multiple exist.
        AssertEx.SeqEqual(new List<int>{4,5,0,2,3,1}, got, "D18 topo wrong" );
    }

    // ---------------------------
    // D19: Sliding window maximum (monotonic deque)
    // ---------------------------

    private static void Test_D19_SlidingWindowMax()
    {
        var got = Drills.SlidingWindowMax(new []{1,3,-1,-3,5,3,6,7}, 3);
        AssertEx.SeqEqual(new List<int>{3,3,5,5,6,7}, got, "D19 wrong");
    }

    // ---------------------------
    // D20: First negative in every window
    // ---------------------------

    private static void Test_D20_FirstNegativeInWindow()
    {
        var got = Drills.FirstNegativeInWindow(new []{12,-1,-7,8,-15,30,16,28}, 3);
        AssertEx.SeqEqual(new List<int>{-1,-1,-7,-15,-15,0}, got, "D20 wrong (use 0 if none)" );
    }

    // ---------------------------
    // D21: Rotting oranges (multi-source BFS)
    // ---------------------------

    private static void Test_D21_RottingOranges()
    {
        int[][] grid = {
            new []{2,1,1},
            new []{1,1,0},
            new []{0,1,1},
        };
        AssertEx.Equal(4, Drills.OrangesRotting(grid), "D21 wrong");
    }

    // ---------------------------
    // D22: Shortest path in binary matrix (8 directions)
    // 0 = open, 1 = blocked
    // ---------------------------

    private static void Test_D22_ShortestPathBinaryMatrix()
    {
        int[][] grid = {
            new []{0,1,0},
            new []{0,0,0},
            new []{1,0,0},
        };
        // One shortest path length is 3: (0,0)->(1,1)->(2,2)
        AssertEx.Equal(3, Drills.ShortestPathBinaryMatrix(grid), "D22 wrong");
    }

    // ---------------------------
    // D23: RecentCounter (queue)
    // ping(t) returns count of pings in [t-3000, t]
    // ---------------------------

    private static void Test_D23_RecentCounter()
    {
        var rc = new Drills.RecentCounter();
        AssertEx.Equal(1, rc.Ping(1), "D23 wrong");
        AssertEx.Equal(2, rc.Ping(100), "D23 wrong");
        AssertEx.Equal(3, rc.Ping(3001), "D23 wrong");
        AssertEx.Equal(3, rc.Ping(3002), "D23 wrong");
    }

    // ---------------------------
    // D24: BFS levels (layer grouping)
    // ---------------------------

    private static void Test_D24_BfsLevels()
    {
        int n = 6;
        var edges = new (int u,int v)[] { (0,1),(0,2),(1,3),(2,3),(3,4),(4,5) };
        var levels = Drills.BfsLevels(n, edges, src: 0);
        var expected = new List<IList<int>>
        {
            new List<int>{0},
            new List<int>{1,2},
            new List<int>{3},
            new List<int>{4},
            new List<int>{5},
        };
        AssertEx.SeqSeqEqual(expected, levels, "D24 wrong" );
    }

    // ---------------------------
    // D25: Shortest subarray with sum >= K (monotonic deque)
    // ---------------------------

    private static void Test_D25_ShortestSubarrayAtLeastK()
    {
        AssertEx.Equal(3, Drills.ShortestSubarrayAtLeastK(new []{2,-1,2}, 3), "D25 wrong");
        AssertEx.Equal(1, Drills.ShortestSubarrayAtLeastK(new []{1}, 1), "D25 wrong");
        AssertEx.Equal(-1, Drills.ShortestSubarrayAtLeastK(new []{1,2}, 4), "D25 wrong");
    }
}

// ---------------------------
// TODO drills (NO SOLUTIONS)
// ---------------------------

public static class Drills
{
    // ========== D1: ArrayStack<T> ==========
    public sealed class ArrayStack<T>
    {
        // Requirements:
        // - Push, Pop, Peek, Count, IsEmpty
        // - Pop/Peek on empty => InvalidOperationException

        public int Count => throw new NotImplementedException("TODO D1 Count");
        public bool IsEmpty() => throw new NotImplementedException("TODO D1 IsEmpty");
        public void Push(T x) => throw new NotImplementedException("TODO D1 Push");
        public T Pop() => throw new NotImplementedException("TODO D1 Pop");
        public T Peek() => throw new NotImplementedException("TODO D1 Peek");
    }

    // ========== D2: CircularQueue<T> ==========
    public sealed class CircularQueue<T>
    {
        // Requirements:
        // - Fixed capacity
        // - Enqueue on full => InvalidOperationException
        // - Dequeue/Peek on empty => InvalidOperationException
        public CircularQueue(int capacity) { throw new NotImplementedException("TODO D2 ctor"); }

        public int Count => throw new NotImplementedException("TODO D2 Count");
        public bool IsEmpty() => throw new NotImplementedException("TODO D2 IsEmpty");
        public void Enqueue(T x) => throw new NotImplementedException("TODO D2 Enqueue");
        public T Dequeue() => throw new NotImplementedException("TODO D2 Dequeue");
        public T Peek() => throw new NotImplementedException("TODO D2 Peek");
    }

    // ========== D3: QueueUsingTwoStacks<T> ==========
    public sealed class QueueUsingTwoStacks<T>
    {
        public int Count => throw new NotImplementedException("TODO D3 Count");
        public bool IsEmpty() => throw new NotImplementedException("TODO D3 IsEmpty");
        public void Enqueue(T x) => throw new NotImplementedException("TODO D3 Enqueue");
        public T Dequeue() => throw new NotImplementedException("TODO D3 Dequeue");
        public T Peek() => throw new NotImplementedException("TODO D3 Peek");
    }

    // ========== D4: StackUsingTwoQueues<T> ==========
    public sealed class StackUsingTwoQueues<T>
    {
        public int Count => throw new NotImplementedException("TODO D4 Count");
        public bool IsEmpty() => throw new NotImplementedException("TODO D4 IsEmpty");
        public void Push(T x) => throw new NotImplementedException("TODO D4 Push");
        public T Pop() => throw new NotImplementedException("TODO D4 Pop");
        public T Peek() => throw new NotImplementedException("TODO D4 Peek");
    }

    // ========== D5: MinStack ==========
    public sealed class MinStack
    {
        public int Count => throw new NotImplementedException("TODO D5 Count");
        public void Push(int x) => throw new NotImplementedException("TODO D5 Push");
        public int Pop() => throw new NotImplementedException("TODO D5 Pop");
        public int Peek() => throw new NotImplementedException("TODO D5 Peek");
        public int GetMin() => throw new NotImplementedException("TODO D5 GetMin");
    }

    // ========== D6..D15: Stack algorithms ==========
    public static bool IsValidParentheses(string s) => throw new NotImplementedException("TODO D6");
    public static string RemoveAdjacentDuplicates(string s) => throw new NotImplementedException("TODO D7");
    public static int EvalRpn(string[] tokens) => throw new NotImplementedException("TODO D8");
    public static string SimplifyPath(string path) => throw new NotImplementedException("TODO D9");
    public static List<int> NextGreaterRight(int[] nums) => throw new NotImplementedException("TODO D10");
    public static List<int> DailyTemperatures(int[] temps) => throw new NotImplementedException("TODO D11");
    public static int LargestRectangleArea(int[] heights) => throw new NotImplementedException("TODO D12");
    public static string DecodeString(string s) => throw new NotImplementedException("TODO D13");
    public static List<string> InfixToPostfixTokens(string[] tokens) => throw new NotImplementedException("TODO D14");
    public static int Calculate(string s) => throw new NotImplementedException("TODO D15");

    // ========== D16..D18: Queue/BFS algorithms ==========
    public static (int dist, List<int> path) BfsShortestPath(int n, (int u,int v)[] edges, int src, int dst)
        => throw new NotImplementedException("TODO D16");

    public static int CountConnectedComponents(int n, (int u,int v)[] edges)
        => throw new NotImplementedException("TODO D17");

    public static List<int> TopoSortKahn(int n, (int u,int v)[] edges)
        => throw new NotImplementedException("TODO D18");

    // ========== D19..D20: Deque patterns ==========
    public static List<int> SlidingWindowMax(int[] nums, int k) => throw new NotImplementedException("TODO D19");
    public static List<int> FirstNegativeInWindow(int[] nums, int k) => throw new NotImplementedException("TODO D20");

    // ========== D21..D22: Grid BFS ==========
    public static int OrangesRotting(int[][] grid) => throw new NotImplementedException("TODO D21");
    public static int ShortestPathBinaryMatrix(int[][] grid) => throw new NotImplementedException("TODO D22");

    // ========== D23: RecentCounter ==========
    public sealed class RecentCounter
    {
        public RecentCounter() { throw new NotImplementedException("TODO D23 ctor"); }
        public int Ping(int t) => throw new NotImplementedException("TODO D23 Ping");
    }

    // ========== D24: BFS levels ==========
    public static List<IList<int>> BfsLevels(int n, (int u,int v)[] edges, int src)
        => throw new NotImplementedException("TODO D24");

    // ========== D25: Prefix sums + monotonic deque ==========
    public static int ShortestSubarrayAtLeastK(int[] nums, int k)
        => throw new NotImplementedException("TODO D25");
}
```

---

## ✅ Recommended LeetCode set (stacks + queues)

### Stack basics + parsing
- 20 Valid Parentheses
- 150 Evaluate Reverse Polish Notation
- 71 Simplify Path
- 394 Decode String
- 224 Basic Calculator

### Monotonic stack
- 496 Next Greater Element I
- 739 Daily Temperatures
- 84 Largest Rectangle in Histogram

### Queue / BFS
- 933 Number of Recent Calls
- 994 Rotting Oranges
- 1091 Shortest Path in Binary Matrix

### Monotonic deque
- 239 Sliding Window Maximum

### Prefix + deque (advanced)
- 862 Shortest Subarray with Sum at Least K

---

## Notes (to keep it “no solutions”)
- All algorithmic behavior is enforced via tests and expected outputs.
- If you want, I can generate a **second** file that provides only templates (no expected outputs) for timed interview simulation.

# 🧱 Stacks + Queues Mastery — Level Guide (with Patterns)

This guide organizes stack/queue mastery into **levels**, so you learn by increasing control of (1) frontier management and (2) invariants.

---

## Level 0 — Mechanics (data structure correctness)
**Goal:** Implement correct LIFO/FIFO containers and never lose track of invariants.
- Stack: push/pop/peek, underflow behavior, size/empty checks.
- Queue: enqueue/dequeue/peek, circular buffer (bounded queue) behavior.
- Debug habit: at any point you can write down the exact content order (top→bottom for stack, front→back for queue).

---

## Level 1 — Frontier thinking (stack/queue as “what’s next”)
**Goal:** Use stacks/queues as traversal frontiers.
- Stack frontier = DFS-style exploration; queue frontier = BFS-style exploration.
- Learn “visit timing” in iterative form using stack frames (ENTER/EXIT) and BFS wave boundaries (`levelSize`).

---

## Level 2 — Constraint maintenance (monotonic structures)
**Goal:** Use stack/deque to maintain an invariant efficiently.
- Monotonic stack (indices): next greater/smaller, previous greater/smaller.
- Histogram rectangle: boundaries via previous/next smaller.
- Contribution technique: compute each element’s range where it’s min/max.

---

## Level 3 — Parsing & evaluation (operator stacks)
**Goal:** Convert infix to postfix and evaluate reliably.
- Shunting-yard: operator stack + output queue/list, precedence and associativity. [web:166]
- Full pipeline: tokenize → shunting-yard → evaluate postfix.

---

## Level 4 — Queue variants that become graph algorithms
**Goal:** Use queue/deque as shortest-path engines.
- Multi-source BFS: start frontier has multiple sources.
- 0–1 BFS: deque frontier with push_front for 0-weight edges and push_back for 1-weight edges. [web:174][web:163]

---

## Level 5 — Determinism & tie-break policies
**Goal:** Same algorithm, deterministic output.
- Sort adjacency lists / stable parent rules.
- Lexicographically smallest topological order (priority frontier vs plain queue).

---

# What to master before graphs
- You can identify when the frontier must be a stack vs queue vs deque vs heap.
- You can state the invariant for monotonic stack/deque in one sentence.
- You can parse/evaluate expressions without “magic ifs” (pure rules).


# ➕ Advanced Add‑On: 10 Drills (No Solutions) with Runnable Driver

These drills extend your existing 25-drill driver. This is a separate runnable pack with **inputs + expected outputs** and **TODO stubs**.

## ✅ How to run
```bash
dotnet new console -n StackQueueAdvanced10
cd StackQueueAdvanced10
```
Replace `Program.cs` with the code below and run:
```bash
dotnet run
```

---

# 📌 Program.cs (Advanced 10)

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

public static class AssertEx
{
    public static void Equal<T>(T expected, T actual, string msg) where T : IEquatable<T>
    {
        if (!expected.Equals(actual)) throw new Exception(msg + $"\nExpected: {expected}\nActual:   {actual}");
    }

    public static void True(bool cond, string msg)
    {
        if (!cond) throw new Exception(msg);
    }

    public static void SeqEqual<T>(IList<T> expected, IList<T> actual, string msg)
    {
        if (expected.Count != actual.Count)
            throw new Exception(msg + $"\nExpected: [{string.Join(",", expected)}]\nActual:   [{string.Join(",", actual)}]");

        for (int i = 0; i < expected.Count; i++)
            if (!EqualityComparer<T>.Default.Equals(expected[i], actual[i]))
                throw new Exception(msg + $"\nMismatch i={i} expected={expected[i]} actual={actual[i]}\nExpected: [{string.Join(",", expected)}]\nActual:   [{string.Join(",", actual)}]");
    }
}

public sealed class DrillRunner
{
    public void Run(string drillId)
    {
        var drills = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
        {
            ["A1"] = Test_A1_NextSmallerRight,
            ["A2"] = Test_A2_PrevSmallerLeft,
            ["A3"] = Test_A3_NextGreaterCircular,
            ["A4"] = Test_A4_SumSubarrayMins,
            ["A5"] = Test_A5_SlidingWindowMin,
            ["A6"] = Test_A6_ZeroOneBfs,
            ["A7"] = Test_A7_ShuntingYardPlusEval,
            ["A8"] = Test_A8_Bipartite,
            ["A9"] = Test_A9_CycleUndirected,
            ["A10"] = Test_A10_CycleDirected,
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
            throw new Exception($"Unknown drill id: {drillId}. Try ALL or A1..A10");

        runOne();
        Console.WriteLine($"✅ {drillId} passed");
    }

    // ---------------- A1: Next Smaller to the Right ----------------
    private static void Test_A1_NextSmallerRight()
    {
        var got = Drills.NextSmallerRight(new []{4, 5, 2, 10, 8});
        AssertEx.SeqEqual(new List<int>{2,2,-1,8,-1}, got, "A1 wrong");

        got = Drills.NextSmallerRight(new []{1, 1, 1});
        AssertEx.SeqEqual(new List<int>{-1,-1,-1}, got, "A1 duplicates wrong (define strict-smaller)" );
    }

    // ---------------- A2: Previous Smaller to the Left ----------------
    private static void Test_A2_PrevSmallerLeft()
    {
        var got = Drills.PrevSmallerLeft(new []{4, 5, 2, 10, 8});
        AssertEx.SeqEqual(new List<int>{-1,4,-1,2,2}, got, "A2 wrong");

        got = Drills.PrevSmallerLeft(new []{3, 2, 1});
        AssertEx.SeqEqual(new List<int>{-1,-1,-1}, got, "A2 decreasing wrong" );
    }

    // ---------------- A3: Next Greater in Circular Array ----------------
    private static void Test_A3_NextGreaterCircular()
    {
        var got = Drills.NextGreaterCircular(new []{1, 2, 1});
        AssertEx.SeqEqual(new List<int>{2,-1,2}, got, "A3 wrong");

        got = Drills.NextGreaterCircular(new []{5, 4, 3, 2, 1});
        AssertEx.SeqEqual(new List<int>{-1,5,5,5,5}, got, "A3 wrong" );
    }

    // ---------------- A4: Sum of Subarray Minimums (mod 1e9+7) ----------------
    private static void Test_A4_SumSubarrayMins()
    {
        AssertEx.Equal(17, Drills.SumSubarrayMins(new []{3,1,2,4}), "A4 wrong");
        AssertEx.Equal(444, Drills.SumSubarrayMins(new []{11,81,94,43,3}), "A4 wrong");
    }

    // ---------------- A5: Sliding Window Minimum (monotonic deque) ----------------
    private static void Test_A5_SlidingWindowMin()
    {
        var got = Drills.SlidingWindowMin(new []{1,3,-1,-3,5,3,6,7}, 3);
        AssertEx.SeqEqual(new List<int>{-1,-3,-3,-3,3,3}, got, "A5 wrong");
    }

    // ---------------- A6: 0-1 BFS shortest distances ----------------
    private static void Test_A6_ZeroOneBfs()
    {
        // Directed graph with 0/1 weights
        // 0->1 (0)
        // 0->2 (1)
        // 1->2 (0)
        // 1->3 (1)
        // 2->3 (0)
        int n = 4;
        var edges = new (int u,int v,int w)[] { (0,1,0),(0,2,1),(1,2,0),(1,3,1),(2,3,0) };
        var dist = Drills.ZeroOneBfs(n, edges, src: 0);
        AssertEx.SeqEqual(new List<int>{0,0,0,0}, dist, "A6 distances wrong" );

        // Another graph to ensure non-zero results:
        // 0->1 (1), 0->2 (1), 1->3 (1), 2->3 (0)
        n = 4;
        edges = new (int u,int v,int w)[] { (0,1,1),(0,2,1),(1,3,1),(2,3,0) };
        dist = Drills.ZeroOneBfs(n, edges, src: 0);
        AssertEx.SeqEqual(new List<int>{0,1,1,1}, dist, "A6 distances wrong" );
    }

    // ---------------- A7: Shunting-yard + postfix eval integrated ----------------
    private static void Test_A7_ShuntingYardPlusEval()
    {
        // Operators: + - * /, parentheses; integer tokens (may include negative via unary '-')
        AssertEx.Equal(1, Drills.EvalInfix("3 + 4 * 2 / ( 1 - 5 )"), "A7 wrong");
        AssertEx.Equal(14, Drills.EvalInfix("2 * (3 + 4)"), "A7 wrong");
        AssertEx.Equal(-12, Drills.EvalInfix("-(3+4) * 2 + 2"), "A7 unary minus wrong" );
    }

    // ---------------- A8: Bipartite check (queue) ----------------
    private static void Test_A8_Bipartite()
    {
        int n = 4;
        var edges = new (int u,int v)[] { (0,1),(1,2),(2,3) }; // path => bipartite
        AssertEx.True(Drills.IsBipartite(n, edges), "A8 expected true");

        n = 3;
        edges = new (int u,int v)[] { (0,1),(1,2),(2,0) }; // triangle => not bipartite
        AssertEx.True(!Drills.IsBipartite(n, edges), "A8 expected false" );
    }

    // ---------------- A9: Cycle detection (undirected) ----------------
    private static void Test_A9_CycleUndirected()
    {
        int n = 4;
        var edges = new (int u,int v)[] { (0,1),(1,2),(2,0),(2,3) };
        AssertEx.True(Drills.HasCycleUndirected(n, edges), "A9 expected true");

        n = 4;
        edges = new (int u,int v)[] { (0,1),(1,2),(2,3) };
        AssertEx.True(!Drills.HasCycleUndirected(n, edges), "A9 expected false");
    }

    // ---------------- A10: Cycle detection (directed) ----------------
    private static void Test_A10_CycleDirected()
    {
        int n = 4;
        var edges = new (int u,int v)[] { (0,1),(1,2),(2,0),(2,3) };
        AssertEx.True(Drills.HasCycleDirected(n, edges), "A10 expected true");

        n = 4;
        edges = new (int u,int v)[] { (0,1),(1,2),(2,3) };
        AssertEx.True(!Drills.HasCycleDirected(n, edges), "A10 expected false");
    }
}

// ---------------------------
// TODO drills (NO SOLUTIONS)
// ---------------------------

public static class Drills
{
    // A1: Next smaller element to the right (strictly smaller; output value or -1)
    public static List<int> NextSmallerRight(int[] a)
        => throw new NotImplementedException("TODO A1");

    // A2: Previous smaller element to the left (strictly smaller; output value or -1)
    public static List<int> PrevSmallerLeft(int[] a)
        => throw new NotImplementedException("TODO A2");

    // A3: Next greater element in a circular array (value or -1)
    public static List<int> NextGreaterCircular(int[] a)
        => throw new NotImplementedException("TODO A3");

    // A4: Sum of subarray minimums mod 1e9+7
    public static int SumSubarrayMins(int[] a)
        => throw new NotImplementedException("TODO A4");

    // A5: Sliding window minimum (monotonic deque)
    public static List<int> SlidingWindowMin(int[] nums, int k)
        => throw new NotImplementedException("TODO A5");

    // A6: 0-1 BFS distances from src (directed edges with weights 0/1)
    public static List<int> ZeroOneBfs(int n, (int u,int v,int w)[] edges, int src)
        => throw new NotImplementedException("TODO A6");

    // A7: Infix evaluation: tokenize + shunting-yard + postfix evaluation. [web:166]
    public static int EvalInfix(string expr)
        => throw new NotImplementedException("TODO A7");

    // A8: Bipartite check (undirected)
    public static bool IsBipartite(int n, (int u,int v)[] edges)
        => throw new NotImplementedException("TODO A8");

    // A9: Cycle detection (undirected)
    public static bool HasCycleUndirected(int n, (int u,int v)[] edges)
        => throw new NotImplementedException("TODO A9");

    // A10: Cycle detection (directed)
    public static bool HasCycleDirected(int n, (int u,int v)[] edges)
        => throw new NotImplementedException("TODO A10");
}
```

---

## Notes
- A6 (0–1 BFS) is the key bridge from BFS → Dijkstra thinking; the deque push-front/back rule is the core. [web:174][web:163]
- A7 is your “parsing mastery” capstone using shunting-yard (operator stack + output queue/list). [web:166]
- A4 uses the monotonic stack “contribution” technique (prev/next smaller bounds). [web:172]

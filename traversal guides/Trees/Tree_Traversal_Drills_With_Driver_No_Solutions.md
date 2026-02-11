# 🌳 Tree Traversal Drills (No Solutions) — With Driver Program + Expected Outputs

**Purpose:** Practice tree traversal patterns by implementing TODO functions inside a runnable **C# Console** driver that includes **inputs + expected outputs**.

**Rules:**
- This file includes **tests and expected outputs**, but **no solutions** (the drill functions are TODOs).
- You should implement *one drill at a time*, run the driver, and keep going until all tests pass.

---

## ✅ How to run (C#)

1) Create a new console app:
```bash
dotnet new console -n TreeTraversalDrills
cd TreeTraversalDrills
```

2) Replace `Program.cs` with the code below.

3) Run:
```bash
dotnet run
```

---

## 🧠 Training protocol (efficient)
- Before coding each drill, write a one-line contract: **Frontier + Visit timing + State**.
- Do a 60-second dry-run on the small sample tree printed in the test.
- When stuck, use the debug outputs suggested inside each drill comment.

---

# 📌 Program.cs (driver + drills)

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public sealed class Program
{
    public static void Main(string[] args)
    {
        // Usage:
        // - dotnet run            => run all tests
        // - dotnet run D1         => run a single drill (e.g., D1)
        // - dotnet run D12        => run a single drill

        try
        {
            var drillId = args.Length > 0 ? args[0].Trim().ToUpperInvariant() : "ALL";
            var runner = new DrillRunner();
            runner.Run(drillId);
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
// Data structures
// ---------------------------

public sealed class BNode
{
    public int Val;
    public BNode? Left;
    public BNode? Right;

    public BNode(int val, BNode? left = null, BNode? right = null)
    {
        Val = val;
        Left = left;
        Right = right;
    }
}

public sealed class NNode
{
    public int Val;
    public List<NNode> Children = new();
    public NNode(int val) { Val = val; }
}

public sealed class PNode
{
    public int Val;
    public PNode? Left;
    public PNode? Right;
    public PNode? Parent;
    public PNode(int val) { Val = val; }
}

public sealed class TrieNode
{
    public bool IsTerminal;
    public SortedDictionary<char, TrieNode> Next = new();
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
            throw new Exception(msg + $"
Expected: {expected}
Actual:   {actual}");
    }

    public static void SeqEqual<T>(IList<T> expected, IList<T> actual, string msg)
    {
        if (expected.Count != actual.Count)
            throw new Exception(msg + $"
Expected length: {expected.Count}
Actual length:   {actual.Count}
Expected: [{string.Join(",", expected)}]
Actual:   [{string.Join(",", actual)}]");

        for (int i = 0; i < expected.Count; i++)
        {
            if (!EqualityComparer<T>.Default.Equals(expected[i], actual[i]))
                throw new Exception(msg + $"
Mismatch at i={i} expected={expected[i]} actual={actual[i]}
Expected: [{string.Join(",", expected)}]
Actual:   [{string.Join(",", actual)}]");
        }
    }

    public static void SeqSeqEqual<T>(IList<IList<T>> expected, IList<IList<T>> actual, string msg)
    {
        if (expected.Count != actual.Count)
            throw new Exception(msg + $"
Expected outer length: {expected.Count}
Actual outer length:   {actual.Count}");

        for (int i = 0; i < expected.Count; i++)
            SeqEqual(expected[i].ToList(), actual[i].ToList(), msg + $"
(Level {i})");
    }
}

// ---------------------------
// Helpers: building sample trees
// ---------------------------

public static class TreeBuild
{
    // Build binary tree from level order array with nulls (LeetCode-style).
    // Example: [1,2,3,null,4] means 1's left=2, right=3; 2's right=4.
    public static BNode? FromLevelOrder(params int?[] a)
    {
        if (a == null) throw new ArgumentNullException(nameof(a));
        if (a.Length == 0) return null;
        if (a[0] == null) return null;

        var root = new BNode(a[0]!.Value);
        var q = new Queue<BNode>();
        q.Enqueue(root);
        int i = 1;

        while (q.Count > 0 && i < a.Length)
        {
            var node = q.Dequeue();

            if (i < a.Length && a[i] != null)
            {
                node.Left = new BNode(a[i]!.Value);
                q.Enqueue(node.Left);
            }
            i++;

            if (i < a.Length && a[i] != null)
            {
                node.Right = new BNode(a[i]!.Value);
                q.Enqueue(node.Right);
            }
            i++;
        }

        return root;
    }

    public static NNode SampleNary()
    {
        //      1
        //   /  |          //  2   3   4
        //     /         //    5   6
        var n1 = new NNode(1);
        var n2 = new NNode(2);
        var n3 = new NNode(3);
        var n4 = new NNode(4);
        var n5 = new NNode(5);
        var n6 = new NNode(6);
        n3.Children.Add(n5);
        n3.Children.Add(n6);
        n1.Children.Add(n2);
        n1.Children.Add(n3);
        n1.Children.Add(n4);
        return n1;
    }

    public static (PNode root, PNode n2, PNode n3, PNode n4, PNode n5) SampleParentPointerBst()
    {
        // BST:
        //      4
        //     /         //    2   5
        //             //      3
        var n4 = new PNode(4);
        var n2 = new PNode(2);
        var n5 = new PNode(5);
        var n3 = new PNode(3);

        n4.Left = n2; n2.Parent = n4;
        n4.Right = n5; n5.Parent = n4;
        n2.Right = n3; n3.Parent = n2;

        return (n4, n2, n3, n4, n5);
    }

    public static TrieNode SampleTrie()
    {
        // Words: to, tea, ted, ten, in, inn
        var root = new TrieNode();
        Add(root, "to");
        Add(root, "tea");
        Add(root, "ted");
        Add(root, "ten");
        Add(root, "in");
        Add(root, "inn");
        return root;

        static void Add(TrieNode r, string w)
        {
            var cur = r;
            foreach (var ch in w)
            {
                if (!cur.Next.TryGetValue(ch, out var nx))
                {
                    nx = new TrieNode();
                    cur.Next[ch] = nx;
                }
                cur = nx;
            }
            cur.IsTerminal = true;
        }
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
            ["D1"]  = Test_D1_PreorderRecursive,
            ["D2"]  = Test_D2_InorderRecursive,
            ["D3"]  = Test_D3_PostorderRecursive,
            ["D4"]  = Test_D4_LevelOrder,
            ["D5"]  = Test_D5_Zigzag,
            ["D6"]  = Test_D6_LeftRightView,
            ["D7"]  = Test_D7_VerticalStable,
            ["D8"]  = Test_D8_VerticalSortedTieBreak,
            ["D9"]  = Test_D9_Boundary,
            ["D10"] = Test_D10_Diagonal,
            ["D11"] = Test_D11_PostorderIter_Phases,
            ["D12"] = Test_D12_PostorderIter_TwoStacks,
            ["D13"] = Test_D13_PostorderIter_LastVisited,
            ["D14"] = Test_D14_NaryPreorderIter,
            ["D15"] = Test_D15_AdjacencyDfsBfs,
            ["D16"] = Test_D16_EulerTinTout,
            ["D17"] = Test_D17_ParentPointerSuccessor,
            ["D18"] = Test_D18_SerializePreNulls,
            ["D19"] = Test_D19_DeserializePreNulls,
            ["D20"] = Test_D20_TrieAllWords,
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
            throw new Exception($"Unknown drill id: {drillId}. Try ALL or D1..D20");

        runOne();
        Console.WriteLine($"✅ {drillId} passed");
    }

    // -------------------------------------------------
    // Shared sample trees used across many tests
    // -------------------------------------------------

    private static BNode? SampleTreeA()
    {
        //        1
        //      /           //     2     3
        //    / \             //   4   5     6
        return TreeBuild.FromLevelOrder(1, 2, 3, 4, 5, null, 6);
    }

    private static BNode? SampleTreeCollision()
    {
        // Perfect tree 1..7 to create a vertical collision at (col=0,row=2): nodes 5 and 6
        //        1
        //      /           //     2     3
        //    / \   /         //   4  5  6  7
        return TreeBuild.FromLevelOrder(1,2,3,4,5,6,7);
    }

    // -------------------------------------------------
    // Drill tests (expected outputs)
    // -------------------------------------------------

    private static void Test_D1_PreorderRecursive()
    {
        // Drill D1:
        // Implement PreorderRecursive (DFS preorder).
        // Debug log suggestion: print (node.Val, depth) on ENTER.
        var root = SampleTreeA();
        var got = Drills.PreorderRecursive(root);
        var expected = new List<int>{ 1,2,4,5,3,6 };
        AssertEx.SeqEqual(expected, got, "D1 preorder wrong");
    }

    private static void Test_D2_InorderRecursive()
    {
        var root = SampleTreeA();
        var got = Drills.InorderRecursive(root);
        var expected = new List<int>{ 4,2,5,1,3,6 };
        AssertEx.SeqEqual(expected, got, "D2 inorder wrong");
    }

    private static void Test_D3_PostorderRecursive()
    {
        var root = SampleTreeA();
        var got = Drills.PostorderRecursive(root);
        var expected = new List<int>{ 4,5,2,6,3,1 };
        AssertEx.SeqEqual(expected, got, "D3 postorder wrong");
    }

    private static void Test_D4_LevelOrder()
    {
        var root = SampleTreeA();
        var got = Drills.LevelOrder(root);
        var expected = new List<IList<int>>
        {
            new List<int>{1},
            new List<int>{2,3},
            new List<int>{4,5,6},
        };
        AssertEx.SeqSeqEqual(expected, got, "D4 level order wrong");
    }

    private static void Test_D5_Zigzag()
    {
        var root = SampleTreeA();
        var got = Drills.ZigzagLevelOrder(root);
        var expected = new List<IList<int>>
        {
            new List<int>{1},
            new List<int>{3,2},
            new List<int>{4,5,6},
        };
        AssertEx.SeqSeqEqual(expected, got, "D5 zigzag wrong");
    }

    private static void Test_D6_LeftRightView()
    {
        var root = SampleTreeA();
        var left = Drills.LeftView(root);
        var right = Drills.RightView(root);
        AssertEx.SeqEqual(new List<int>{1,2,4}, left, "D6 left view wrong");
        AssertEx.SeqEqual(new List<int>{1,3,6}, right, "D6 right view wrong");
    }

    private static void Test_D7_VerticalStable()
    {
        // Stable vertical order: group by column using BFS order within the column.
        var root = SampleTreeA();
        var got = Drills.VerticalOrderStable(root);
        // Columns: -2:[4], -1:[2], 0:[1,5], +1:[3], +2:[6]
        var expected = new List<IList<int>>
        {
            new List<int>{4},
            new List<int>{2},
            new List<int>{1,5},
            new List<int>{3},
            new List<int>{6},
        };
        AssertEx.SeqSeqEqual(expected, got, "D7 vertical stable wrong");
    }

    private static void Test_D8_VerticalSortedTieBreak()
    {
        // Sorted vertical traversal with tie-break: (col asc, row asc, val asc)
        // Collision: nodes 5 and 6 share (col=0,row=2), so order must be 5 then 6.
        var root = SampleTreeCollision();
        var got = Drills.VerticalTraversalSorted(root);
        var expected = new List<IList<int>>
        {
            new List<int>{4},
            new List<int>{2},
            new List<int>{1,5,6},
            new List<int>{3},
            new List<int>{7},
        };
        AssertEx.SeqSeqEqual(expected, got, "D8 vertical sorted wrong");
    }

    private static void Test_D9_Boundary()
    {
        var root = SampleTreeA();
        var got = Drills.BoundaryAntiClockwise(root);
        var expected = new List<int>{ 1,2,4,5,6,3 };
        AssertEx.SeqEqual(expected, got, "D9 boundary wrong");
    }

    private static void Test_D10_Diagonal()
    {
        // Using the common diagonal rule: right child stays on same diagonal, left child goes to next diagonal.
        var root = SampleTreeA();
        var got = Drills.DiagonalTraversal(root);
        // Diag0: 1,3,6 ; Diag1: 2,5 ; Diag2: 4
        var expected = new List<IList<int>>
        {
            new List<int>{1,3,6},
            new List<int>{2,5},
            new List<int>{4},
        };
        AssertEx.SeqSeqEqual(expected, got, "D10 diagonal wrong");
    }

    private static void Test_D11_PostorderIter_Phases()
    {
        var root = SampleTreeA();
        var got = Drills.PostorderIterativePhases(root);
        var expected = new List<int>{ 4,5,2,6,3,1 };
        AssertEx.SeqEqual(expected, got, "D11 postorder phases wrong");
    }

    private static void Test_D12_PostorderIter_TwoStacks()
    {
        var root = SampleTreeA();
        var got = Drills.PostorderIterativeTwoStacks(root);
        var expected = new List<int>{ 4,5,2,6,3,1 };
        AssertEx.SeqEqual(expected, got, "D12 postorder two-stacks wrong");
    }

    private static void Test_D13_PostorderIter_LastVisited()
    {
        var root = SampleTreeA();
        var got = Drills.PostorderIterativeLastVisited(root);
        var expected = new List<int>{ 4,5,2,6,3,1 };
        AssertEx.SeqEqual(expected, got, "D13 postorder lastVisited wrong");
    }

    private static void Test_D14_NaryPreorderIter()
    {
        var root = TreeBuild.SampleNary();
        var got = Drills.NaryPreorderIterative(root);
        var expected = new List<int>{ 1,2,3,5,6,4 };
        AssertEx.SeqEqual(expected, got, "D14 N-ary preorder iterative wrong");
    }

    private static void Test_D15_AdjacencyDfsBfs()
    {
        // Graph-form tree (rooted at 1): edges 0-1,1-2,1-3,3-4,3-5
        int n = 6;
        var edges = new (int u,int v)[] { (0,1),(1,2),(1,3),(3,4),(3,5) };
        var adj = Drills.BuildAdj(n, edges);
        Drills.SortAdj(adj);

        var dfs = Drills.PreorderAdj(1, adj);
        var bfsLevels = Drills.LevelsAdj(1, adj);

        // Deterministic traversal with sorted neighbors:
        // DFS preorder from 1: 1,0,2,3,4,5
        AssertEx.SeqEqual(new List<int>{1,0,2,3,4,5}, dfs, "D15 adjacency DFS wrong");

        // BFS levels from 1: [ [1], [0,2,3], [4,5] ]
        var expectedLevels = new List<IList<int>>
        {
            new List<int>{1},
            new List<int>{0,2,3},
            new List<int>{4,5},
        };
        AssertEx.SeqSeqEqual(expectedLevels, bfsLevels, "D15 adjacency BFS levels wrong");
    }

    private static void Test_D16_EulerTinTout()
    {
        // Same adjacency tree as D15; DFS order depends on sorted neighbors and skipping parent.
        int n = 6;
        var edges = new (int u,int v)[] { (0,1),(1,2),(1,3),(3,4),(3,5) };
        var adj = Drills.BuildAdj(n, edges);
        Drills.SortAdj(adj);

        var (tin, tout, order) = Drills.EulerTimes(1, adj);

        // Expected (precomputed with neighbor sort):
        // DFS order: [1, 0, 2, 3, 4, 5]
        AssertEx.SeqEqual(new List<int>{ 1,0,2,3,4,5 }, order, "D16 euler order wrong");
        AssertEx.SeqEqual(new List<int>{ 1,0,3,5,6,8 }, tin, "D16 tin wrong");
        AssertEx.SeqEqual(new List<int>{ 2,11,4,10,7,9 }, tout, "D16 tout wrong");
    }

    private static void Test_D17_ParentPointerSuccessor()
    {
        var (root, n2, n3, n4, n5) = TreeBuild.SampleParentPointerBst();

        // Inorder of this BST is: 2,3,4,5
        // successor(2)=3, successor(3)=4, successor(4)=5, successor(5)=null
        AssertEx.Equal(3, Drills.InorderSuccessor(n2)!.Val, "D17 succ(2) wrong");
        AssertEx.Equal(4, Drills.InorderSuccessor(n3)!.Val, "D17 succ(3) wrong");
        AssertEx.Equal(5, Drills.InorderSuccessor(n4)!.Val, "D17 succ(4) wrong");
        AssertEx.True(Drills.InorderSuccessor(n5) == null, "D17 succ(5) should be null");
    }

    private static void Test_D18_SerializePreNulls()
    {
        // For SampleTreeA, expected preorder with nulls (#):
        // 1,2,4,#,#,5,#,#,3,#,6,#,#
        var root = SampleTreeA();
        var got = Drills.SerializePreorderWithNulls(root);
        var expected = new List<string>{"1","2","4","#","#","5","#","#","3","#","6","#","#"};
        AssertEx.SeqEqual(expected, got, "D18 serialize preorder nulls wrong");
    }

    private static void Test_D19_DeserializePreNulls()
    {
        var tokens = new List<string>{"1","2","4","#","#","5","#","#","3","#","6","#","#"};
        var root = Drills.DeserializePreorderWithNulls(tokens);

        // After deserialization, traversals should match TreeA.
        AssertEx.SeqEqual(new List<int>{ 1,2,4,5,3,6 }, Drills.PreorderRecursive(root), "D19 preorder mismatch");
        AssertEx.SeqEqual(new List<int>{ 4,2,5,1,3,6 }, Drills.InorderRecursive(root), "D19 inorder mismatch");
        AssertEx.SeqEqual(new List<int>{ 4,5,2,6,3,1 }, Drills.PostorderRecursive(root), "D19 postorder mismatch");
    }

    private static void Test_D20_TrieAllWords()
    {
        var trie = TreeBuild.SampleTrie();
        var got = Drills.TrieAllWords(trie);
        var expected = new List<string>{"in","inn","tea","ted","ten","to"};
        AssertEx.SeqEqual(expected, got, "D20 trie words wrong (lexicographic)" );
    }
}

// ---------------------------
// TODO drills (implement these)
// ---------------------------

public static class Drills
{
    // D1
    public static List<int> PreorderRecursive(BNode? root)
        => throw new NotImplementedException("TODO D1");

    // D2
    public static List<int> InorderRecursive(BNode? root)
        => throw new NotImplementedException("TODO D2");

    // D3
    public static List<int> PostorderRecursive(BNode? root)
        => throw new NotImplementedException("TODO D3");

    // D4
    public static List<IList<int>> LevelOrder(BNode? root)
        => throw new NotImplementedException("TODO D4");

    // D5
    public static List<IList<int>> ZigzagLevelOrder(BNode? root)
        => throw new NotImplementedException("TODO D5");

    // D6
    public static List<int> LeftView(BNode? root)
        => throw new NotImplementedException("TODO D6-left");

    public static List<int> RightView(BNode? root)
        => throw new NotImplementedException("TODO D6-right");

    // D7
    public static List<IList<int>> VerticalOrderStable(BNode? root)
        => throw new NotImplementedException("TODO D7");

    // D8
    public static List<IList<int>> VerticalTraversalSorted(BNode? root)
        => throw new NotImplementedException("TODO D8");

    // D9
    public static List<int> BoundaryAntiClockwise(BNode? root)
        => throw new NotImplementedException("TODO D9");

    // D10
    public static List<IList<int>> DiagonalTraversal(BNode? root)
        => throw new NotImplementedException("TODO D10");

    // D11
    public static List<int> PostorderIterativePhases(BNode? root)
        => throw new NotImplementedException("TODO D11");

    // D12
    public static List<int> PostorderIterativeTwoStacks(BNode? root)
        => throw new NotImplementedException("TODO D12");

    // D13
    public static List<int> PostorderIterativeLastVisited(BNode? root)
        => throw new NotImplementedException("TODO D13");

    // D14
    public static List<int> NaryPreorderIterative(NNode? root)
        => throw new NotImplementedException("TODO D14");

    // D15
    public static List<int>[] BuildAdj(int n, (int u, int v)[] edges)
        => throw new NotImplementedException("TODO D15-buildAdj");

    public static void SortAdj(List<int>[] adj)
        => throw new NotImplementedException("TODO D15-sortAdj");

    public static List<int> PreorderAdj(int root, List<int>[] adj)
        => throw new NotImplementedException("TODO D15-preorderAdj");

    public static List<IList<int>> LevelsAdj(int root, List<int>[] adj)
        => throw new NotImplementedException("TODO D15-levelsAdj");

    // D16
    public static (List<int> tin, List<int> tout, List<int> order) EulerTimes(int root, List<int>[] adj)
        => throw new NotImplementedException("TODO D16");

    // D17
    public static PNode? InorderSuccessor(PNode? x)
        => throw new NotImplementedException("TODO D17");

    // D18
    public static List<string> SerializePreorderWithNulls(BNode? root)
        => throw new NotImplementedException("TODO D18");

    // D19
    public static BNode? DeserializePreorderWithNulls(IList<string> tokens)
        => throw new NotImplementedException("TODO D19");

    // D20
    public static List<string> TrieAllWords(TrieNode root)
        => throw new NotImplementedException("TODO D20");
}
```

---

# ✅ LeetCode problem set (tree traversal-focused)

Use this list after you complete D1–D20.

## DFS orders + basics
- 144 Binary Tree Preorder Traversal
- 94 Binary Tree Inorder Traversal
- 145 Binary Tree Postorder Traversal
- 104 Maximum Depth of Binary Tree
- 111 Minimum Depth of Binary Tree
- 100 Same Tree
- 101 Symmetric Tree

## Iterative DFS + BST inorder applications
- 94 Inorder Traversal (iterative)
- 230 Kth Smallest Element in a BST
- 98 Validate Binary Search Tree

## BFS level-order family
- 102 Binary Tree Level Order Traversal
- 103 Binary Tree Zigzag Level Order Traversal
- 199 Binary Tree Right Side View
- 515 Find Largest Value in Each Tree Row

## Structural pointers / iterators
- 116 Populating Next Right Pointers in Each Node
- 173 Binary Search Tree Iterator

## Coordinate / ordering variants
- 987 Vertical Order Traversal of a Binary Tree (strict tie-break)

## Tree DP + path-state
- 543 Diameter of Binary Tree
- 124 Binary Tree Maximum Path Sum
- 113 Path Sum II
- 437 Path Sum III

## Serialization
- 297 Serialize and Deserialize Binary Tree

---

## Notes
- D7 vs D8 are intentionally different: stable BFS-in-column vs sorted tie-break ordering.
- D15–D16 are your bridge to graphs: adjacency list + parent/visited discipline.

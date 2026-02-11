# 🧪 Tree Traversal Homework Pack (Binary + N-ary)

> You’ll implement each traversal, then run the C# and Python harnesses to verify.

---

## ✅ How to use this homework

### Why
Short drills force correct traversal control: ordering, frontier choice, and state.

### What
You get:
- A list of drills (2–6 minutes each).
- A **C# Console app** harness with assert helpers.
- A **Python unittest** harness.

### How (step/flow)
1) Implement one function at a time.
2) Run tests.
3) If failing, print the traversal event log (node + phase + depth).

### Common pitfalls
- Fixing tests by changing expected order without checking the traversal contract.

### Tips and tricks
- For iterative traversals, write down the push/enqueue order before you code.

---

# Part 1 — Drill list (short, focused)

## Binary drills
1) Preorder (recursive): return list.
2) Inorder (recursive): return list.
3) Postorder (recursive): return list.
4) Preorder (iterative stack).
5) Inorder (iterative stack).
6) Postorder (iterative one-stack using lastVisited).
7) Level-order BFS (grouped by levels).
8) Zigzag level-order.
9) Right view (BFS level representative).
10) Euler tour: return (enterTimes, exitTimes) dictionaries.

## N-ary drills (enhancement)
11) N-ary preorder (recursive).
12) N-ary preorder (iterative stack, push children right-to-left).
13) N-ary postorder (recursive).
14) N-ary level-order BFS (grouped).

---

# Part 2 — C# test harness (Console app style)

## Instructions
- Create a new console app: `dotnet new console`.
- Replace `Program.cs` with the code below.
- Run: `dotnet run`.

> Contract: `null` root returns empty collections (no exceptions).

```csharp
using System;
using System.Collections.Generic;

public class Program
{
    // -----------------------------
    // Data structures
    // -----------------------------

    public class BNode
    {
        public string Val;
        public BNode? Left;
        public BNode? Right;
        public BNode(string val, BNode? left=null, BNode? right=null)
        {
            Val = val; Left = left; Right = right;
        }
    }

    public class NNode
    {
        public string Val;
        public List<NNode> Children = new();
        public NNode(string val) { Val = val; }
    }

    static BNode BuildBinarySample()
    {
        //        A
        //      /   \
        //     B     C
        //    / \     \
        //   D   E     F
        return new BNode("A",
            new BNode("B", new BNode("D"), new BNode("E")),
            new BNode("C", null, new BNode("F")));
    }

    static NNode BuildNArySample()
    {
        //        A
        //     /  |  \
        //    B   C   D
        //   / \      |
        //  E   F     G
        var A = new NNode("A");
        var B = new NNode("B");
        var C = new NNode("C");
        var D = new NNode("D");
        var E = new NNode("E");
        var F = new NNode("F");
        var G = new NNode("G");
        A.Children.AddRange(new[] { B, C, D });
        B.Children.AddRange(new[] { E, F });
        D.Children.Add(G);
        return A;
    }

    // -----------------------------
    // TODO: Implement these
    // -----------------------------

    static List<string> PreorderRec(BNode? root) => throw new NotImplementedException();
    static List<string> InorderRec(BNode? root) => throw new NotImplementedException();
    static List<string> PostorderRec(BNode? root) => throw new NotImplementedException();

    static List<string> PreorderIter(BNode? root) => throw new NotImplementedException();
    static List<string> InorderIter(BNode? root) => throw new NotImplementedException();
    static List<string> PostorderIterOneStack(BNode? root) => throw new NotImplementedException();

    static List<List<string>> LevelOrder(BNode? root) => throw new NotImplementedException();
    static List<List<string>> ZigzagLevelOrder(BNode? root) => throw new NotImplementedException();
    static List<string> RightView(BNode? root) => throw new NotImplementedException();

    static (Dictionary<string,int> enter, Dictionary<string,int> exit) EulerTourTimes(BNode? root) => throw new NotImplementedException();

    static List<string> NAryPreorderRec(NNode? root) => throw new NotImplementedException();
    static List<string> NAryPreorderIter(NNode? root) => throw new NotImplementedException();
    static List<string> NAryPostorderRec(NNode? root) => throw new NotImplementedException();
    static List<List<string>> NAryLevelOrder(NNode? root) => throw new NotImplementedException();

    // -----------------------------
    // Minimal assert helpers
    // -----------------------------

    static void AssertEqual<T>(T actual, T expected, string name)
    {
        if (!EqualityComparer<T>.Default.Equals(actual, expected))
            throw new Exception($"FAIL {name}: expected {expected}, got {actual}");
    }

    static void AssertSeqEqual<T>(IList<T> actual, IList<T> expected, string name)
    {
        if (actual.Count != expected.Count)
            throw new Exception($"FAIL {name}: length expected {expected.Count}, got {actual.Count}");
        for (int i = 0; i < actual.Count; i++)
        {
            if (!EqualityComparer<T>.Default.Equals(actual[i], expected[i]))
                throw new Exception($"FAIL {name}[{i}]: expected {expected[i]}, got {actual[i]}");
        }
    }

    static void Assert2DSeqEqual<T>(IList<IList<T>> actual, IList<IList<T>> expected, string name)
    {
        if (actual.Count != expected.Count)
            throw new Exception($"FAIL {name}: outer length expected {expected.Count}, got {actual.Count}");
        for (int i = 0; i < actual.Count; i++)
            AssertSeqEqual((IList<T>)actual[i], (IList<T>)expected[i], $"{name}[{i}]");
    }

    // -----------------------------
    // Tests
    // -----------------------------

    public static void Main()
    {
        var br = BuildBinarySample();
        var nr = BuildNArySample();

        // Binary expected
        var pre = new List<string>{"A","B","D","E","C","F"};
        var ino = new List<string>{"D","B","E","A","C","F"};
        var post = new List<string>{"D","E","B","F","C","A"};
        var levels = new List<List<string>>{
            new(){"A"},
            new(){"B","C"},
            new(){"D","E","F"}
        };
        var zigzag = new List<List<string>>{
            new(){"A"},
            new(){"C","B"},
            new(){"D","E","F"}
        };
        var rightView = new List<string>{"A","C","F"};

        // N-ary expected
        var npre = new List<string>{"A","B","E","F","C","D","G"};
        var npost = new List<string>{"E","F","B","C","G","D","A"};
        var nlevels = new List<List<string>>{
            new(){"A"},
            new(){"B","C","D"},
            new(){"E","F","G"}
        };

        // Binary tests
        AssertSeqEqual(PreorderRec(br), pre, "PreorderRec");
        AssertSeqEqual(InorderRec(br), ino, "InorderRec");
        AssertSeqEqual(PostorderRec(br), post, "PostorderRec");

        AssertSeqEqual(PreorderIter(br), pre, "PreorderIter");
        AssertSeqEqual(InorderIter(br), ino, "InorderIter");
        AssertSeqEqual(PostorderIterOneStack(br), post, "PostorderIterOneStack");

        Assert2DSeqEqual(LevelOrder(br), levels, "LevelOrder");
        Assert2DSeqEqual(ZigzagLevelOrder(br), zigzag, "ZigzagLevelOrder");
        AssertSeqEqual(RightView(br), rightView, "RightView");

        var (enter, exit) = EulerTourTimes(br);
        // Sanity checks (not exact times): A should enter before everyone and exit last.
        AssertEqual(enter["A"], 0, "Euler enter[A] should be 0");
        if (exit["A"] <= exit["B"] || exit["A"] <= exit["C"]) throw new Exception("FAIL Euler exit[A] should be last");

        // N-ary tests
        AssertSeqEqual(NAryPreorderRec(nr), npre, "NAryPreorderRec");
        AssertSeqEqual(NAryPreorderIter(nr), npre, "NAryPreorderIter");
        AssertSeqEqual(NAryPostorderRec(nr), npost, "NAryPostorderRec");
        Assert2DSeqEqual(NAryLevelOrder(nr), nlevels, "NAryLevelOrder");

        Console.WriteLine("All tests passed ✅ (once you implement the TODOs)");
    }
}
```

---

# Part 3 — Python test harness (unittest)

## Instructions
- Save as `tree_traversal_tests.py`.
- Run: `python -m unittest tree_traversal_tests.py`.

```python
import unittest
from dataclasses import dataclass, field
from typing import Any, Optional, List, Dict, Tuple
from collections import deque


@dataclass
class BNode:
    val: Any
    left: Optional["BNode"] = None
    right: Optional["BNode"] = None


@dataclass
class NNode:
    val: Any
    children: List["NNode"] = field(default_factory=list)


def build_binary_sample() -> BNode:
    #        A
    #      /   \
    #     B     C
    #    / \     \
    #   D   E     F
    return BNode("A",
                 left=BNode("B", BNode("D"), BNode("E")),
                 right=BNode("C", None, BNode("F")))


def build_nary_sample() -> NNode:
    #        A
    #     /  |  \
    #    B   C   D
    #   / \      |
    #  E   F     G
    A = NNode("A")
    B = NNode("B")
    C = NNode("C")
    D = NNode("D")
    E = NNode("E")
    F = NNode("F")
    G = NNode("G")
    A.children = [B, C, D]
    B.children = [E, F]
    D.children = [G]
    return A


# -----------------------------
# TODO: Implement these
# -----------------------------

def preorder_rec(root: Optional[BNode]) -> List[Any]:
    raise NotImplementedError


def inorder_rec(root: Optional[BNode]) -> List[Any]:
    raise NotImplementedError


def postorder_rec(root: Optional[BNode]) -> List[Any]:
    raise NotImplementedError


def preorder_iter(root: Optional[BNode]) -> List[Any]:
    raise NotImplementedError


def inorder_iter(root: Optional[BNode]) -> List[Any]:
    raise NotImplementedError


def postorder_iter_one_stack(root: Optional[BNode]) -> List[Any]:
    raise NotImplementedError


def level_order(root: Optional[BNode]) -> List[List[Any]]:
    raise NotImplementedError


def zigzag_level_order(root: Optional[BNode]) -> List[List[Any]]:
    raise NotImplementedError


def right_view(root: Optional[BNode]) -> List[Any]:
    raise NotImplementedError


def euler_tour_times(root: Optional[BNode]) -> Tuple[Dict[Any,int], Dict[Any,int]]:
    raise NotImplementedError


def nary_preorder_rec(root: Optional[NNode]) -> List[Any]:
    raise NotImplementedError


def nary_preorder_iter(root: Optional[NNode]) -> List[Any]:
    raise NotImplementedError


def nary_postorder_rec(root: Optional[NNode]) -> List[Any]:
    raise NotImplementedError


def nary_level_order(root: Optional[NNode]) -> List[List[Any]]:
    raise NotImplementedError


class TestTraversals(unittest.TestCase):
    def setUp(self):
        self.br = build_binary_sample()
        self.nr = build_nary_sample()

    def test_binary_dfs_recursive(self):
        self.assertEqual(preorder_rec(self.br), ["A","B","D","E","C","F"])
        self.assertEqual(inorder_rec(self.br), ["D","B","E","A","C","F"])
        self.assertEqual(postorder_rec(self.br), ["D","E","B","F","C","A"])

    def test_binary_dfs_iterative(self):
        self.assertEqual(preorder_iter(self.br), ["A","B","D","E","C","F"])
        self.assertEqual(inorder_iter(self.br), ["D","B","E","A","C","F"])
        self.assertEqual(postorder_iter_one_stack(self.br), ["D","E","B","F","C","A"])

    def test_binary_bfs(self):
        self.assertEqual(level_order(self.br), [["A"],["B","C"],["D","E","F"]])
        self.assertEqual(zigzag_level_order(self.br), [["A"],["C","B"],["D","E","F"]])
        self.assertEqual(right_view(self.br), ["A","C","F"])

    def test_euler(self):
        enter, exit = euler_tour_times(self.br)
        self.assertEqual(enter["A"], 0)
        self.assertGreater(exit["A"], exit["B"])
        self.assertGreater(exit["A"], exit["C"])

    def test_nary(self):
        self.assertEqual(nary_preorder_rec(self.nr), ["A","B","E","F","C","D","G"])
        self.assertEqual(nary_preorder_iter(self.nr), ["A","B","E","F","C","D","G"])
        self.assertEqual(nary_postorder_rec(self.nr), ["E","F","B","C","G","D","A"])
        self.assertEqual(nary_level_order(self.nr), [["A"],["B","C","D"],["E","F","G"]])


if __name__ == "__main__":
    unittest.main()
```

---

# 🐛 Debugging hints (use during drills)

- If preorder is reversed: your stack push order is wrong.
- If BFS level grouping is wrong: you’re not using waveSize correctly.
- If postorder iterative is wrong: you’re visiting a node before its right subtree is fully processed.

---

# 📚 References (concept refresh)
- Binary DFS/BFS order definitions: https://www.geeksforgeeks.org/dsa/tree-traversals-inorder-preorder-and-postorder/ [web:1]
- N-ary preorder + iterative child push ordering: https://www.geeksforgeeks.org/dsa/preorder-traversal-of-a-n-ary-tree/ [web:49]
- N-ary BFS level-order: https://www.geeksforgeeks.org/dsa/level-order-traversal-of-n-ary-tree/ [web:58]
- General traversal concepts: https://en.wikipedia.org/wiki/Tree_traversal [web:4]

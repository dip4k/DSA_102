# 🧺 Queues-only Drills (No Solutions) — Python + C#

## 🐍 Option A: Python driver
Create `queues_drills.py` and run:
```bash
python queues_drills.py
python queues_drills.py Q1
```

```python
import sys
from collections import deque


def assert_eq(exp, act, msg=""):
    if exp != act:
        raise AssertionError(f"{msg}\nExpected: {exp}\nActual:   {act}")


def run(drill_id="ALL"):
    tests = {
        # FIFO / expiration
        "Q0": t_q0_recent_counter,

        # Deque / window
        "Q1": t_q1_sliding_window_max,
        "Q2": t_q2_sliding_window_min,
        "Q3": t_q3_first_negative_window,

        # Prefix deque
        "Q4": t_q4_shortest_subarray_ge_k,

        # Implementation drill
        "I1": t_i1_queue_using_stacks,
    }

    if drill_id == "ALL":
        for k in sorted(tests.keys()):
            tests[k]()
            print(f"✅ {k} passed")
        return

    if drill_id not in tests:
        raise ValueError("Unknown id. Use ALL or Q0..Q4 or I1")

    tests[drill_id]()
    print(f"✅ {drill_id} passed")


# ------------------- Tests -------------------

def t_q0_recent_counter():
    rc = RecentCounter()
    assert_eq(1, rc.ping(1), "Q0")
    assert_eq(2, rc.ping(100), "Q0")
    assert_eq(3, rc.ping(3001), "Q0")
    assert_eq(3, rc.ping(3002), "Q0")


def t_q1_sliding_window_max():
    nums = [1,3,-1,-3,5,3,6,7]
    assert_eq([3,3,5,5,6,7], sliding_window_max(nums, 3), "Q1")


def t_q2_sliding_window_min():
    nums = [1,3,-1,-3,5,3,6,7]
    assert_eq([-1,-3,-3,-3,3,3], sliding_window_min(nums, 3), "Q2")


def t_q3_first_negative_window():
    nums = [12,-1,-7,8,-15,30,16,28]
    assert_eq([-1,-1,-7,-15,-15,0], first_negative_in_window(nums, 3), "Q3")


def t_q4_shortest_subarray_ge_k():
    assert_eq(3, shortest_subarray_sum_ge_k([2,-1,2], 3), "Q4")
    assert_eq(1, shortest_subarray_sum_ge_k([1], 1), "Q4")


def t_i1_queue_using_stacks():
    q = QueueUsingStacks()
    q.push(1)
    q.push(2)
    assert_eq(1, q.pop(), "I1 pop")
    q.push(3)
    assert_eq(2, q.peek(), "I1 peek")
    assert_eq(2, q.pop(), "I1 pop2")
    assert_eq(False, q.empty(), "I1 empty")


# ------------------- TODOs (NO SOLUTIONS) -------------------

# Q0

class RecentCounter:
    def __init__(self):
        raise NotImplementedError("TODO Q0")

    def ping(self, t: int) -> int:
        raise NotImplementedError("TODO Q0")


# Q1

def sliding_window_max(nums, k):
    raise NotImplementedError("TODO Q1")


# Q2

def sliding_window_min(nums, k):
    raise NotImplementedError("TODO Q2")


# Q3

def first_negative_in_window(nums, k):
    """Return first negative in each window, or 0 if none."""
    raise NotImplementedError("TODO Q3")


# Q4

def shortest_subarray_sum_ge_k(nums, k):
    raise NotImplementedError("TODO Q4")


# I1

class QueueUsingStacks:
    def __init__(self):
        raise NotImplementedError("TODO I1")

    def push(self, x: int) -> None:
        raise NotImplementedError("TODO I1")

    def pop(self) -> int:
        raise NotImplementedError("TODO I1")

    def peek(self) -> int:
        raise NotImplementedError("TODO I1")

    def empty(self) -> bool:
        raise NotImplementedError("TODO I1")


if __name__ == "__main__":
    drill_id = "ALL" if len(sys.argv) == 1 else sys.argv[1].strip().upper()
    run(drill_id)
```

---

## 🟦 Option B: C# driver
Create a project:
```bash
dotnet new console -n QueuesOnlyDrills
cd QueuesOnlyDrills
```
Replace `Program.cs` with:

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public sealed class Program
{
    public static void Main(string[] args)
    {
        var id = args.Length == 0 ? "ALL" : args[0].Trim().ToUpperInvariant();
        new Runner().Run(id);
        Console.WriteLine("✅ Done");
    }
}

public static class AssertEx
{
    public static void Equal<T>(T expected, T actual, string msg) where T : IEquatable<T>
    {
        if (!expected.Equals(actual))
            throw new Exception(msg + $"\nExpected: {expected}\nActual:   {actual}");
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

public sealed class Runner
{
    public void Run(string id)
    {
        var tests = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
        {
            ["Q0"] = T_Q0,
            ["Q1"] = T_Q1,
            ["Q2"] = T_Q2,
            ["Q3"] = T_Q3,
            ["Q4"] = T_Q4,
            ["I1"] = T_I1,
        };

        if (id == "ALL")
        {
            foreach (var kv in tests.OrderBy(k => k.Key))
            {
                kv.Value();
                Console.WriteLine($"✅ {kv.Key} passed");
            }
            return;
        }

        if (!tests.TryGetValue(id, out var one))
            throw new Exception("Unknown id. Use ALL or Q0..Q4 or I1");

        one();
        Console.WriteLine($"✅ {id} passed");
    }

    private static void T_Q0()
    {
        var rc = new Drills.RecentCounter();
        AssertEx.Equal(1, rc.Ping(1), "Q0");
        AssertEx.Equal(2, rc.Ping(100), "Q0");
        AssertEx.Equal(3, rc.Ping(3001), "Q0");
        AssertEx.Equal(3, rc.Ping(3002), "Q0");
    }

    private static void T_Q1()
    {
        var nums = new List<int>{1,3,-1,-3,5,3,6,7};
        AssertEx.SeqEqual(new List<int>{3,3,5,5,6,7}, Drills.SlidingWindowMax(nums, 3), "Q1");
    }

    private static void T_Q2()
    {
        var nums = new List<int>{1,3,-1,-3,5,3,6,7};
        AssertEx.SeqEqual(new List<int>{-1,-3,-3,-3,3,3}, Drills.SlidingWindowMin(nums, 3), "Q2");
    }

    private static void T_Q3()
    {
        var nums = new List<int>{12,-1,-7,8,-15,30,16,28};
        AssertEx.SeqEqual(new List<int>{-1,-1,-7,-15,-15,0}, Drills.FirstNegativeInWindow(nums, 3), "Q3");
    }

    private static void T_Q4()
    {
        AssertEx.Equal(3, Drills.ShortestSubarraySumGeK(new List<int>{2,-1,2}, 3), "Q4");
        AssertEx.Equal(1, Drills.ShortestSubarraySumGeK(new List<int>{1}, 1), "Q4");
    }

    private static void T_I1()
    {
        var q = new Drills.QueueUsingStacks();
        q.Push(1);
        q.Push(2);
        AssertEx.Equal(1, q.Pop(), "I1 pop");
        q.Push(3);
        AssertEx.Equal(2, q.Peek(), "I1 peek");
        AssertEx.Equal(2, q.Pop(), "I1 pop2");
        AssertEx.Equal(false, q.Empty(), "I1 empty");
    }
}

public static class Drills
{
    // ------------------- TODOs (NO SOLUTIONS) -------------------

    // Q0
    public sealed class RecentCounter
    {
        public RecentCounter() => throw new NotImplementedException("TODO Q0");
        public int Ping(int t) => throw new NotImplementedException("TODO Q0");
    }

    // Q1
    public static List<int> SlidingWindowMax(List<int> nums, int k)
        => throw new NotImplementedException("TODO Q1");

    // Q2
    public static List<int> SlidingWindowMin(List<int> nums, int k)
        => throw new NotImplementedException("TODO Q2");

    // Q3
    public static List<int> FirstNegativeInWindow(List<int> nums, int k)
        => throw new NotImplementedException("TODO Q3");

    // Q4
    public static int ShortestSubarraySumGeK(List<int> nums, int k)
        => throw new NotImplementedException("TODO Q4");

    // I1
    public sealed class QueueUsingStacks
    {
        public QueueUsingStacks() => throw new NotImplementedException("TODO I1");
        public void Push(int x) => throw new NotImplementedException("TODO I1");
        public int Pop() => throw new NotImplementedException("TODO I1");
        public int Peek() => throw new NotImplementedException("TODO I1");
        public bool Empty() => throw new NotImplementedException("TODO I1");
    }
}
```

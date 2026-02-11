# 🧪 Stacks + Queues Drills (No Solutions) — C# + Python Drivers

- Each drill is a **TODO** (no solutions).
- Tests contain **inputs + expected outputs**.
- Implement in **one language first**, get all green, then port.

---

## ✅ Option A: Python driver

Create `stacks_queues_drills.py` and run:
```bash
python stacks_queues_drills.py
python stacks_queues_drills.py S7
```

```python
import sys
from collections import deque


def assert_eq(exp, act, msg=""):
    if exp != act:
        raise AssertionError(f"{msg}\nExpected: {exp}\nActual:   {act}")


def run(drill_id="ALL"):
    tests = {
        # Stack basics
        "S1": t_s1_valid_parentheses,
        "S2": t_s2_min_stack,
        "S3": t_s3_eval_rpn,
        "S4": t_s4_decode_string,
        "S5": t_s5_remove_k_digits,

        # Monotonic stack
        "S6": t_s6_next_greater,
        "S7": t_s7_daily_temperatures,
        "S8": t_s8_next_greater_circular,
        "S9": t_s9_prev_smaller,
        "S10": t_s10_sum_subarray_mins,
        "S11": t_s11_largest_rectangle,

        # Deque / window
        "Q1": t_q1_sliding_window_max,
        "Q2": t_q2_sliding_window_min,
        "Q3": t_q3_first_negative_window,
        "Q4": t_q4_shortest_subarray_ge_k,

        # Parsing
        "P1": t_p1_infix_to_postfix,
        "P2": t_p2_eval_infix,

        # Implementations
        "I1": t_i1_queue_using_stacks,
        "I2": t_i2_stack_using_queues,
    }

    if drill_id == "ALL":
        for k in sorted(tests.keys()):
            tests[k]()
            print(f"✅ {k} passed")
        return

    if drill_id not in tests:
        raise ValueError("Unknown id. Use ALL or S1.. or Q.. or P.. or I..")

    tests[drill_id]()
    print(f"✅ {drill_id} passed")


# ------------------- Tests -------------------

def t_s1_valid_parentheses():
    assert_eq(True, valid_parentheses("()[]{}"), "S1")
    assert_eq(False, valid_parentheses("(]"), "S1")
    assert_eq(False, valid_parentheses("([)]"), "S1")


def t_s2_min_stack():
    ms = MinStack()
    ms.push(3)
    ms.push(1)
    ms.push(2)
    assert_eq(1, ms.get_min(), "S2 min")
    ms.pop()
    assert_eq(1, ms.get_min(), "S2 min2")
    ms.pop()
    assert_eq(3, ms.top(), "S2 top")
    assert_eq(3, ms.get_min(), "S2 min3")


def t_s3_eval_rpn():
    assert_eq(9, eval_rpn(["2","1","+","3","*" ]), "S3")
    assert_eq(6, eval_rpn(["4","13","5","/","+" ]), "S3")


def t_s4_decode_string():
    assert_eq("accaccacc", decode_string("3[a2[c]]"), "S4")
    assert_eq("aaabcbc", decode_string("3[a]2[bc]"), "S4")


def t_s5_remove_k_digits():
    assert_eq("1219", remove_k_digits("1432219", 3), "S5")
    assert_eq("0", remove_k_digits("10200", 1), "S5")


def t_s6_next_greater():
    nums = [2,1,2,4,3]
    assert_eq([3,2,4,-1,-1], next_greater_right(nums), "S6")


def t_s7_daily_temperatures():
    t = [73,74,75,71,69,72,76,73]
    assert_eq([1,1,4,2,1,1,0,0], daily_temperatures(t), "S7")


def t_s8_next_greater_circular():
    nums = [1,2,1]
    assert_eq([2,-1,2], next_greater_circular(nums), "S8")


def t_s9_prev_smaller():
    nums = [4,5,2,10,8]
    assert_eq([-1,4,-1,2,2], prev_smaller_left(nums), "S9")


def t_s10_sum_subarray_mins():
    assert_eq(17, sum_subarray_mins([3,1,2,4], mod=None), "S10")


def t_s11_largest_rectangle():
    assert_eq(10, largest_rectangle_histogram([2,1,5,6,2,3]), "S11")


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


def t_p1_infix_to_postfix():
    expr = "3 + 4 * 2 / ( 1 - 5 )"
    assert_eq(["3","4","2","*","1","5","-","/","+"], infix_to_postfix(expr), "P1")


def t_p2_eval_infix():
    assert_eq(1, eval_infix("(2+3)*4/5"), "P2")
    assert_eq(7, eval_infix("1+2*3"), "P2")


def t_i1_queue_using_stacks():
    q = QueueUsingStacks()
    q.push(1)
    q.push(2)
    assert_eq(1, q.pop(), "I1 pop")
    q.push(3)
    assert_eq(2, q.peek(), "I1 peek")
    assert_eq(2, q.pop(), "I1 pop2")
    assert_eq(False, q.empty(), "I1 empty")


def t_i2_stack_using_queues():
    st = StackUsingQueues()
    st.push(1)
    st.push(2)
    assert_eq(2, st.top(), "I2 top")
    assert_eq(2, st.pop(), "I2 pop")
    assert_eq(False, st.empty(), "I2 empty")


# ------------------- TODOs (NO SOLUTIONS) -------------------

# S1

def valid_parentheses(s: str) -> bool:
    raise NotImplementedError("TODO S1")


# S2

class MinStack:
    def __init__(self):
        raise NotImplementedError("TODO S2")

    def push(self, x: int) -> None:
        raise NotImplementedError("TODO S2")

    def pop(self) -> None:
        raise NotImplementedError("TODO S2")

    def top(self) -> int:
        raise NotImplementedError("TODO S2")

    def get_min(self) -> int:
        raise NotImplementedError("TODO S2")


# S3

def eval_rpn(tokens):
    raise NotImplementedError("TODO S3")


# S4

def decode_string(s: str) -> str:
    raise NotImplementedError("TODO S4")


# S5

def remove_k_digits(num: str, k: int) -> str:
    raise NotImplementedError("TODO S5")


# S6

def next_greater_right(nums):
    """Return array of next greater VALUES to the right; -1 if none."""
    raise NotImplementedError("TODO S6")


# S7

def daily_temperatures(t):
    raise NotImplementedError("TODO S7")


# S8

def next_greater_circular(nums):
    raise NotImplementedError("TODO S8")


# S9

def prev_smaller_left(nums):
    """Return previous smaller VALUES to the left; -1 if none."""
    raise NotImplementedError("TODO S9")


# S10

def sum_subarray_mins(arr, mod=None):
    """Return sum of subarray minimums. If mod is None, return exact int."""
    raise NotImplementedError("TODO S10")


# S11

def largest_rectangle_histogram(heights):
    raise NotImplementedError("TODO S11")


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


# P1

def infix_to_postfix(expr: str):
    """Tokens are space-separated in tests. Return postfix token list."""
    raise NotImplementedError("TODO P1")


# P2

def eval_infix(expr: str) -> int:
    """Supports + - * / and parentheses. Integer division truncates toward zero."""
    raise NotImplementedError("TODO P2")


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


# I2

class StackUsingQueues:
    def __init__(self):
        raise NotImplementedError("TODO I2")

    def push(self, x: int) -> None:
        raise NotImplementedError("TODO I2")

    def pop(self) -> int:
        raise NotImplementedError("TODO I2")

    def top(self) -> int:
        raise NotImplementedError("TODO I2")

    def empty(self) -> bool:
        raise NotImplementedError("TODO I2")


if __name__ == "__main__":
    drill_id = "ALL" if len(sys.argv) == 1 else sys.argv[1].strip().upper()
    run(drill_id)
```

---

## ✅ Option B: C# driver

Create a project:
```bash
dotnet new console -n StacksQueuesDrills
cd StacksQueuesDrills
```
Replace `Program.cs` with the code below.

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
            ["S1"] = T_S1,
            ["S2"] = T_S2,
            ["S3"] = T_S3,
            ["S4"] = T_S4,
            ["S5"] = T_S5,
            ["S6"] = T_S6,
            ["S7"] = T_S7,
            ["S8"] = T_S8,
            ["S9"] = T_S9,
            ["S10"] = T_S10,
            ["S11"] = T_S11,
            ["Q1"] = T_Q1,
            ["Q2"] = T_Q2,
            ["Q3"] = T_Q3,
            ["Q4"] = T_Q4,
            ["P1"] = T_P1,
            ["P2"] = T_P2,
            ["I1"] = T_I1,
            ["I2"] = T_I2,
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
            throw new Exception("Unknown id. Use ALL or S1.. or Q.. or P.. or I..");

        one();
        Console.WriteLine($"✅ {id} passed");
    }

    private static void T_S1()
    {
        AssertEx.Equal(true, Drills.ValidParentheses("()[]{}"), "S1");
        AssertEx.Equal(false, Drills.ValidParentheses("(]"), "S1");
        AssertEx.Equal(false, Drills.ValidParentheses("([)]"), "S1");
    }

    private static void T_S2()
    {
        var ms = new Drills.MinStack();
        ms.Push(3);
        ms.Push(1);
        ms.Push(2);
        AssertEx.Equal(1, ms.GetMin(), "S2 min");
        ms.Pop();
        AssertEx.Equal(1, ms.GetMin(), "S2 min2");
        ms.Pop();
        AssertEx.Equal(3, ms.Top(), "S2 top");
        AssertEx.Equal(3, ms.GetMin(), "S2 min3");
    }

    private static void T_S3()
    {
        AssertEx.Equal(9, Drills.EvalRpn(new []{"2","1","+","3","*"}), "S3");
        AssertEx.Equal(6, Drills.EvalRpn(new []{"4","13","5","/","+"}), "S3");
    }

    private static void T_S4()
    {
        AssertEx.Equal("accaccacc", Drills.DecodeString("3[a2[c]]"), "S4");
        AssertEx.Equal("aaabcbc", Drills.DecodeString("3[a]2[bc]"), "S4");
    }

    private static void T_S5()
    {
        AssertEx.Equal("1219", Drills.RemoveKDigits("1432219", 3), "S5");
        AssertEx.Equal("0", Drills.RemoveKDigits("10200", 1), "S5");
    }

    private static void T_S6()
    {
        var nums = new List<int>{2,1,2,4,3};
        AssertEx.SeqEqual(new List<int>{3,2,4,-1,-1}, Drills.NextGreaterRight(nums), "S6");
    }

    private static void T_S7()
    {
        var t = new List<int>{73,74,75,71,69,72,76,73};
        AssertEx.SeqEqual(new List<int>{1,1,4,2,1,1,0,0}, Drills.DailyTemperatures(t), "S7");
    }

    private static void T_S8()
    {
        var nums = new List<int>{1,2,1};
        AssertEx.SeqEqual(new List<int>{2,-1,2}, Drills.NextGreaterCircular(nums), "S8");
    }

    private static void T_S9()
    {
        var nums = new List<int>{4,5,2,10,8};
        AssertEx.SeqEqual(new List<int>{-1,4,-1,2,2}, Drills.PrevSmallerLeft(nums), "S9");
    }

    private static void T_S10()
    {
        AssertEx.Equal(17L, Drills.SumSubarrayMins(new List<int>{3,1,2,4}, mod: null), "S10");
    }

    private static void T_S11()
    {
        AssertEx.Equal(10, Drills.LargestRectangleHistogram(new List<int>{2,1,5,6,2,3}), "S11");
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

    private static void T_P1()
    {
        var expr = "3 + 4 * 2 / ( 1 - 5 )";
        AssertEx.SeqEqual(
            new List<string>{"3","4","2","*","1","5","-","/","+"},
            Drills.InfixToPostfix(expr),
            "P1");
    }

    private static void T_P2()
    {
        AssertEx.Equal(1, Drills.EvalInfix("(2+3)*4/5"), "P2");
        AssertEx.Equal(7, Drills.EvalInfix("1+2*3"), "P2");
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

    private static void T_I2()
    {
        var st = new Drills.StackUsingQueues();
        st.Push(1);
        st.Push(2);
        AssertEx.Equal(2, st.Top(), "I2 top");
        AssertEx.Equal(2, st.Pop(), "I2 pop");
        AssertEx.Equal(false, st.Empty(), "I2 empty");
    }
}

public static class Drills
{
    // ------------------- TODOs (NO SOLUTIONS) -------------------

    // S1
    public static bool ValidParentheses(string s)
        => throw new NotImplementedException("TODO S1");

    // S2
    public sealed class MinStack
    {
        public MinStack() => throw new NotImplementedException("TODO S2");
        public void Push(int x) => throw new NotImplementedException("TODO S2");
        public void Pop() => throw new NotImplementedException("TODO S2");
        public int Top() => throw new NotImplementedException("TODO S2");
        public int GetMin() => throw new NotImplementedException("TODO S2");
    }

    // S3
    public static int EvalRpn(string[] tokens)
        => throw new NotImplementedException("TODO S3");

    // S4
    public static string DecodeString(string s)
        => throw new NotImplementedException("TODO S4");

    // S5
    public static string RemoveKDigits(string num, int k)
        => throw new NotImplementedException("TODO S5");

    // S6
    public static List<int> NextGreaterRight(List<int> nums)
        => throw new NotImplementedException("TODO S6");

    // S7
    public static List<int> DailyTemperatures(List<int> t)
        => throw new NotImplementedException("TODO S7");

    // S8
    public static List<int> NextGreaterCircular(List<int> nums)
        => throw new NotImplementedException("TODO S8");

    // S9
    public static List<int> PrevSmallerLeft(List<int> nums)
        => throw new NotImplementedException("TODO S9");

    // S10
    public static long SumSubarrayMins(List<int> arr, int? mod)
        => throw new NotImplementedException("TODO S10");

    // S11
    public static int LargestRectangleHistogram(List<int> heights)
        => throw new NotImplementedException("TODO S11");

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

    // P1
    public static List<string> InfixToPostfix(string expr)
        => throw new NotImplementedException("TODO P1");

    // P2
    public static int EvalInfix(string expr)
        => throw new NotImplementedException("TODO P2");

    // I1
    public sealed class QueueUsingStacks
    {
        public QueueUsingStacks() => throw new NotImplementedException("TODO I1");
        public void Push(int x) => throw new NotImplementedException("TODO I1");
        public int Pop() => throw new NotImplementedException("TODO I1");
        public int Peek() => throw new NotImplementedException("TODO I1");
        public bool Empty() => throw new NotImplementedException("TODO I1");
    }

    // I2
    public sealed class StackUsingQueues
    {
        public StackUsingQueues() => throw new NotImplementedException("TODO I2");
        public void Push(int x) => throw new NotImplementedException("TODO I2");
        public int Pop() => throw new NotImplementedException("TODO I2");
        public int Top() => throw new NotImplementedException("TODO I2");
        public bool Empty() => throw new NotImplementedException("TODO I2");
    }
}
```

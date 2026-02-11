# Flow‑Wise Stack Traversal Mastery (Enhanced v2)

**Goal:** Professional-grade intuition for stack-driven problem solving (Level 1 → Level 6).

**Style contract:** Why / What / How / Where / When + step-flow + Python/C# snippets + common pitfalls + tips & tricks.

---

## One-page Level Mapping Index (Stacks)
Use this to instantly map a stack problem to the right level, pattern, and invariant.

Legend:
- **Invariant** = what must stay true after each push/pop.
- **Index stack** = stack stores indices (preferred for “nearest to left/right” patterns).
- **Frame stack** = stack stores (node/state) to simulate recursion.

| Level | Pattern / skill | Typical problems (examples) | Expected invariant (one-liner) |
|---|---|---|---|
| L1 | Matching / nesting discipline | Valid Parentheses, Remove Adjacent Duplicates | “Stack equals unresolved opens (or unresolved work) in correct order.” |
| L1 | Parallel metadata stack | Min Stack | “Metadata stack stays same depth; top summarizes main stack.” |
| L2 | Stack as explicit traversal frontier | Iterative DFS (tree/graph) | “Every pushed item is a discovered-but-not-processed state.” |
| L2 | Monotonic stack (NGE/NSE/PGE/PSE) | Daily Temperatures, Stock Span | “Stack is monotonic by value; top is nearest candidate.” |
| L3 | Span finalization (pop finalizes boundaries) | Histogram, Subarray mins/ranges | “When an index pops, its left/right boundary is fixed.” |
| L4 | Greedy canonical prefix (pop improves answer) | Remove K Digits, Remove Duplicate Letters | “After processing i, stack is the best feasible prefix.” |
| L5 | Parsing stacks (operands/operators) | RPN eval, Basic Calculator | “Stacks preserve operand order + operator precedence.” |
| L6 | Structure completion via pops | Trapping Rain Water, 132 Pattern | “A pop event corresponds to a completed structure.” |

---

## The ladder: complexity of state
- **Level 1:** LIFO discipline and “what remains unresolved”.
- **Level 2:** Stack as a frontier (simulate recursion) + monotonic candidate set.
- **Level 3:** Popping finalizes spans; compute contributions from boundaries.
- **Level 4:** Greedy: stack represents the optimal prefix under constraints.
- **Level 5:** Two-stack parsing: precedence/associativity correctness.
- **Level 6:** Pops correspond to higher-level structures (basins/patterns).

---

## Baseline stack snippets

### Python
```python
st = []

st.append(x)   # push
x = st.pop()   # pop
x = st[-1]     # peek
empty = (len(st) == 0)
```

### C#
```csharp
var st = new Stack<int>();

st.Push(x);
int x2 = st.Pop();
int top = st.Peek();
bool empty = st.Count == 0;
```

---

# Level 1 — Physical Layer (LIFO basics)

## 1.1 Matching / nesting (Valid Parentheses)

**Why:** Nested constraints require “last opened closes first.”

**What:** Push opens; on close, pop and validate type.

**How (step-flow):**
1) For each char c.
2) If c is an opening bracket → push.
3) If c is a closing bracket → pop and check it matches.
4) End: stack must be empty.

**Where:** parentheses matching, basic parsing, matching delimiters.

**When:** whenever resolution must happen in reverse order.

**Invariant:** Stack contains exactly the unmatched openings encountered so far.

**Python (template)**
```python
def is_valid(s: str) -> bool:
    pair = {')': '(', ']': '[', '}': '{'}
    st = []
    for c in s:
        if c in '([{':
            st.append(c)
        else:
            if not st or st[-1] != pair.get(c, ''):
                return False
            st.pop()
    return not st
```

**C# (template)**
```csharp
public static bool IsValid(string s)
{
    var pair = new Dictionary<char,char> { [')']='(', [']']='[', ['}']='{' };
    var st = new Stack<char>();

    foreach (char c in s)
    {
        if (c == '(' || c == '[' || c == '{')
        {
            st.Push(c);
        }
        else
        {
            if (st.Count == 0) return false;
            char open = st.Pop();
            if (!pair.TryGetValue(c, out var need) || open != need) return false;
        }
    }
    return st.Count == 0;
}
```

**Common pitfalls**
- Popping without checking empty.
- Forgetting the final “stack must be empty”.
- Treating all bracket types as identical.

**Tips & tricks**
- Say out loud: “stack = unresolved opens”.
- Any close bracket is a request to resolve the most recent open.


## 1.2 Parallel metadata stack (Min Stack)

**Why:** Support `getMin()` in O(1) while preserving pop.

**What:** Maintain `mins` such that `mins.top` equals the min of all elements in `st`.

**How (step-flow):**
1) push(x): push x to main; push min(x, mins.top) to mins.
2) pop(): pop both.
3) getMin(): return mins.top.

**Invariant:** `mins.Count == st.Count`, and `mins.Peek()` is the min of `st`.

**Python (template)**
```python
class MinStack:
    def __init__(self):
        self.st = []
        self.mn = []

    def push(self, x: int) -> None:
        self.st.append(x)
        self.mn.append(x if not self.mn else min(x, self.mn[-1]))

    def pop(self) -> None:
        self.st.pop()
        self.mn.pop()

    def top(self) -> int:
        return self.st[-1]

    def get_min(self) -> int:
        return self.mn[-1]
```

**C# (template)**
```csharp
public sealed class MinStack
{
    private readonly Stack<int> st = new();
    private readonly Stack<int> mn = new();

    public void Push(int x)
    {
        st.Push(x);
        mn.Push(mn.Count == 0 ? x : Math.Min(x, mn.Peek()));
    }

    public void Pop()
    {
        st.Pop();
        mn.Pop();
    }

    public int Top() => st.Peek();
    public int GetMin() => mn.Peek();
}
```

**Common pitfalls**
- Only pushing to `mn` when x is a new minimum (breaks symmetry on pop).

**Tips & tricks**
- Any time you maintain a “summary stack”, keep it the same depth.

---

# Level 2 — Frontier Layer (explicit stack + monotonic basics)

## 2.1 Stack as an explicit traversal frontier (iterative DFS)

**Why:** Avoid recursion depth limits and make control explicit.

**What:** Stack holds “discovered but not fully processed” nodes/states.

**How (step-flow):**
1) Push start; mark visited immediately.
2) While stack not empty: pop u; process u; push undiscovered neighbors.

**Invariant:** With visited-on-push, each node enters the stack at most once.

**Python (template)**
```python
def dfs_iter(start, adj):
    visited = set([start])
    st = [start]
    while st:
        u = st.pop()
        for v in adj[u]:
            if v not in visited:
                visited.add(v)
                st.append(v)
    return visited
```

**C# (template)**
```csharp
public static HashSet<int> DfsIter(int start, List<int>[] adj)
{
    var visited = new HashSet<int> { start };
    var st = new Stack<int>();
    st.Push(start);

    while (st.Count > 0)
    {
        int u = st.Pop();
        foreach (int v in adj[u])
        {
            if (visited.Add(v))
                st.Push(v);
        }
    }
    return visited;
}
```

**Common pitfalls**
- Marking visited on pop (duplicates get pushed many times).
- Forgetting disconnected components (outer loop needed if you want full coverage).

**Tips & tricks**
- If you can phrase it as “frontier of pending work”, your stack usage will stay correct.


## 2.2 Monotonic stack: next greater to the right (NGE)

**Why:** “Nearest greater/smaller to left/right” can be solved in O(n).

**What:** Keep a stack of indices; maintain a monotonic property on `a[index]`.

**How (left→right, NGE):**
1) For each i, while `a[i] > a[st.top]`, pop j and set ans[j] = i.
2) Push i.

**Invariant:** Values at indices in stack are strictly decreasing.

**Python (template)**
```python
def next_greater_right(a):
    n = len(a)
    ans = [-1]*n
    st = []  # indices
    for i, x in enumerate(a):
        while st and x > a[st[-1]]:
            j = st.pop()
            ans[j] = i
        st.append(i)
    return ans
```

**C# (template)**
```csharp
public static int[] NextGreaterRightIndex(int[] a)
{
    int n = a.Length;
    var ans = Enumerable.Repeat(-1, n).ToArray();
    var st = new Stack<int>();

    for (int i = 0; i < n; i++)
    {
        while (st.Count > 0 && a[i] > a[st.Peek()])
        {
            int j = st.Pop();
            ans[j] = i;
        }
        st.Push(i);
    }
    return ans;
}
```

**Common pitfalls**
- Storing values instead of indices (you lose distances and positions).
- Not defining tie handling (use `>` vs `>=` intentionally).

**Tips & tricks**
- Monotonic stacks almost always store indices.

---

# Level 3 — Span Layer (pop finalizes boundaries)

## 3.1 Largest Rectangle in Histogram

**Why:** Each bar is the minimum height over some maximal contiguous span.

**What:** Maintain an increasing stack of indices; pop when a lower height arrives.

**How (step-flow):**
1) Scan i from 0..n, with sentinel height 0 at the end.
2) While stack not empty and `h[i] < h[top]`, pop k and compute area.
3) Left boundary = new stack top; right boundary = i-1.

**Invariant:** Stack holds indices with non-decreasing heights.

**Python (template)**
```python
def largest_rectangle(heights):
    h = heights + [0]
    st = []
    best = 0
    for i in range(len(h)):
        while st and h[i] < h[st[-1]]:
            k = st.pop()
            left = st[-1] if st else -1
            width = i - left - 1
            best = max(best, h[k] * width)
        st.append(i)
    return best
```

**C# (template)**
```csharp
public static int LargestRectangle(int[] heights)
{
    int n = heights.Length;
    var st = new Stack<int>();
    int best = 0;

    for (int i = 0; i <= n; i++)
    {
        int cur = (i == n) ? 0 : heights[i];
        while (st.Count > 0 && cur < heights[st.Peek()])
        {
            int k = st.Pop();
            int left = st.Count == 0 ? -1 : st.Peek();
            int width = i - left - 1;
            best = Math.Max(best, heights[k] * width);
        }
        st.Push(i);
    }
    return best;
}
```

**Common pitfalls**
- Forgetting the sentinel flush.
- Off-by-one in width; remember width = `right-left-1`.

**Tips & tricks**
- When an index pops, treat it as: “I now know my right boundary.”


## 3.2 Contribution: Sum of Subarray Minimums

**Why:** Each element contributes to many subarrays as the minimum.

**What:** Find how far it extends left and right while staying the minimum.

**How (step-flow):**
1) Compute previous strictly less (PLE) boundary.
2) Compute next less-or-equal (NLE) boundary.
3) Contribution: `a[i] * (i-PLE) * (NLE-i)`.

**Invariant:** Duplicate handling is consistent so each subarray minimum is counted once.

**Python (skeleton)**
```python
def sum_subarray_mins(a, MOD=10**9+7):
    n = len(a)
    ple = [-1]*n
    nle = [n]*n

    st = []
    for i in range(n):
        while st and a[st[-1]] > a[i]:
            st.pop()
        ple[i] = st[-1] if st else -1
        st.append(i)

    st = []
    for i in range(n-1, -1, -1):
        while st and a[st[-1]] >= a[i]:
            st.pop()
        nle[i] = st[-1] if st else n
        st.append(i)

    ans = 0
    for i in range(n):
        ans = (ans + a[i] * (i-ple[i]) * (nle[i]-i)) % MOD
    return ans
```

**C# (note)**
- Same approach as Python; implement PLE with `>` and NLE with `>=` consistently.

**Common pitfalls**
- Using the same comparison on both sides (double counts duplicates).

**Tips & tricks**
- Memorize one safe convention: left uses `>` (strictly greater pops), right uses `>=`.

---

# Level 4 — Greedy canonical prefix (stack = best prefix)

## 4.1 Remove K Digits

**Why:** To minimize the number, you want to remove “bad peaks” early.

**What:** Maintain an increasing stack of digits.

**How (step-flow):**
1) For each digit d: while k>0 and top>d, pop and k--.
2) Push d.
3) If k remains, pop from end.
4) Strip leading zeros.

**Invariant:** After processing i, stack is the smallest possible prefix among all choices with remaining k.

**Python (template)**
```python
def remove_k_digits(num: str, k: int) -> str:
    st = []
    for ch in num:
        while k and st and st[-1] > ch:
            st.pop(); k -= 1
        st.append(ch)
    while k and st:
        st.pop(); k -= 1
    res = ''.join(st).lstrip('0')
    return res if res else '0'
```

**C# (template)**
```csharp
public static string RemoveKDigits(string num, int k)
{
    var st = new List<char>();
    foreach (char ch in num)
    {
        while (k > 0 && st.Count > 0 && st[^1] > ch)
        {
            st.RemoveAt(st.Count - 1);
            k--;
        }
        st.Add(ch);
    }
    while (k > 0 && st.Count > 0)
    {
        st.RemoveAt(st.Count - 1);
        k--;
    }
    int i = 0;
    while (i < st.Count && st[i] == '0') i++;
    var res = new string(st.Skip(i).ToArray());
    return res.Length == 0 ? "0" : res;
}
```

**Common pitfalls**
- Not stripping leading zeros.
- Forgetting to remove remaining k from the end.

**Tips & tricks**
- Think: “pop while it improves the prefix and I still have removals.”

---

# Level 5 — Parsing stacks

## 5.1 Evaluate Reverse Polish Notation (RPN)

**Why:** Postfix evaluation is a pure stack machine.

**What:** Stack holds intermediate values.

**How (step-flow):**
1) If token is number → push.
2) Else operator → pop b, pop a, push a op b.

**Invariant:** Stack equals results of fully-evaluated subexpressions in the scanned prefix.

**Python (template)**
```python
def eval_rpn(tokens):
    st = []
    for t in tokens:
        if t in {"+","-","*","/"}:
            b = st.pop(); a = st.pop()
            if t == "+": st.append(a+b)
            elif t == "-": st.append(a-b)
            elif t == "*": st.append(a*b)
            else:
                st.append(int(a/b))  # truncate toward zero
        else:
            st.append(int(t))
    return st[-1]
```

**C# (template)**
```csharp
public static int EvalRpn(string[] tokens)
{
    var st = new Stack<int>();
    foreach (string t in tokens)
    {
        if (t is "+" or "-" or "*" or "/")
        {
            int b = st.Pop();
            int a = st.Pop();
            st.Push(t switch
            {
                "+" => a + b,
                "-" => a - b,
                "*" => a * b,
                "/" => a / b,
                _ => 0
            });
        }
        else st.Push(int.Parse(t));
    }
    return st.Peek();
}
```

**Common pitfalls**
- Swapping operand order (must pop b then a).
- Division semantics mismatch (define “truncate toward zero”).

**Tips & tricks**
- Say: “operator consumes last two values; order matters.”


## 5.2 Infix evaluation (two stacks)

**Why:** Precedence and parentheses require controlled operator application.

**What:** `vals` stack for numbers, `ops` stack for operators.

**How (high level):**
- Push numbers.
- On operator, apply while top has higher/equal precedence.
- '(' pushes; ')' applies until '('.

**Invariant:** `ops` contains operators waiting for their right operand; applying preserves precedence.

**Tip:** If you already have `infix → postfix`, you can reuse RPN eval and simplify correctness.

---

# Level 6 — Pops correspond to completed structures

## 6.1 Trapping Rain Water (stack)

**Why:** A rising bar can close a basin.

**What:** Stack of indices with decreasing heights.

**How (high level):**
- When current height > height[top], pop bottom and compute water using left wall (new top) and right wall (current).

**Invariant:** Stack represents potential left walls of future basins.

**Common pitfalls**
- Not checking stack emptiness after popping the bottom (no left wall).

**Tips & tricks**
- Treat each pop as “I found the basin bottom; now compute with two walls.”

---

## Debug checklist (stacks)
- What does the stack store (values, indices, frames)?
- Is every pop justified by resolving a constraint (match/span/greedy improvement)?
- Did you define duplicate handling (< vs <=) for monotonic cases?
- For parsing: did you define precedence, associativity, and division rounding?

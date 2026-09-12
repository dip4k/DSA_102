# Phase 07: Stack & Monotonic Stack

> **Focus:** Monotonic Invariants, Bracket Matching, Constant-Time Minimums, Reverse Polish Parsing, Next Greater Elements, and Histogram Area Maximization.  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 7 (Problems #36–#40)

---

## 36. Valid Parentheses (LeetCode #20)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#stack` `#bracket-matching` `#hash-map` |
| **LeetCode Link** | [Valid Parentheses](https://leetcode.com/problems/valid-parentheses/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given a string `s` containing just the characters `'('`, `')'`, `'{'`, `'}'`, `'['` and `']'`, determine if the input string is valid. An input string is valid if:
  1. Open brackets must be closed by the same type of brackets.
  2. Open brackets must be closed in the correct order.
  3. Every close bracket has a corresponding open bracket of the same type.
- **Key Constraints:**
  - `1 <= s.Length <= 10^4`
  - `s` consists of parentheses only `'()[]{}'`.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Validate balanced, properly nested bracket scoping using a Last-In-First-Out (LIFO) stack.
- **Sample 1:**
  - **Input:** `s = "()"`
  - **Output:** `true`
- **Sample 2:**
  - **Input:** `s = "()[]{}"`
  - **Output:** `true`
- **Sample 3:**
  - **Input:** `s = "(]"`
  - **Output:** `false`
- **Sample 4:**
  - **Input:** `s = "([])"`
  - **Output:** `true`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Think of nested parentheses as a stack of execution call frames or a set of Russian nesting dolls (Matryoshka). The function or block opened most recently must terminate and close before any enclosing outer function can close. If an inner function tries to return with an outer frame's signature (a mismatched closer) or an outer frame tries to close before the inner doll is packed away, structural syntax is violated. A LIFO stack precisely models this inner-to-outer resolution order.

#### 3.2 The Naive Bottleneck & Redundant Computation
A naive approach repeatedly searches for adjacent matching pairs `()`, `[]`, `{}` in the string and deletes them using string replacement:
```csharp
while (s.Contains("()") || s.Contains("[]") || s.Contains("{}"))
{
    s = s.Replace("()", "").Replace("[]", "").Replace("{}", "");
}
return s.Length == 0;
```
This naive reduction scans and copies the string up to $N/2$ times. Each scan and reallocation takes $O(N)$ time, causing quadratic complexity:
$$T(N) = O(N^2)$$
For $N = 10^4$, $N^2 = 10^8$ operations, causing excessive GC allocations and potential timeouts.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**The Expected-Closer Invariant:**
Instead of pushing the opening bracket onto the stack and later using a hash map or switch statement to look up if the closing bracket matches, we push the **expected closing bracket** immediately upon encountering an opener:
- When seeing `'('`, push `')'`
- When seeing `'{'`, push `'}'`
- When seeing `'['`, push `']'`

When a closing bracket arrives:
1. The stack must not be empty (otherwise there is an orphaned closer without an opener).
2. The popped character must match the incoming character exactly:
   $$\text{stack.Pop()} == c$$
This collapses the lookup gate into a single $O(1)$ equality check with zero branching.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
STRING INPUT: " ( [ { } ] ) "
                 ▲
                 Current Token: '{'

STACK STATE (Stores expected closing brackets):
Top -> [ '}' ]  <-- Expected closer for '{'
       [ ']' ]  <-- Expected closer for '['
       [ ')' ]  <-- Expected closer for '('
Base ---------------------------------------

TRANSITION ON NEXT CHAR:
Incoming char: '}'
Gate Check: stack.Count > 0 && stack.Pop() == '}'  ==> Match confirmed!
New Stack Top -> [ ']' ]
```

- `s[i]`: Scanning cursor stepping linearly through the string.
- `expectedClosers`: LIFO stack storing the mirror closing bracket expected for each open scope.
- **Parity Gate:** If `s.Length % 2 != 0`, return `false` immediately because pairs cannot form.

#### 3.5 State Transition Triggers & Decision Gates
1. **Odd Parity Check:** If `s.Length % 2 != 0`, return `false`.
2. **Opening Gate:**
   - If `c == '('` $\implies$ `Push(')')`
   - Else if `c == '{'` $\implies$ `Push('}')`
   - Else if `c == '['` $\implies$ `Push(']')`
3. **Closing Gate:**
   - If `stack.Count == 0 || stack.Pop() != c` $\implies$ return `false`.
4. **Final Scoping Check:**
   - At end of string, return `stack.Count == 0`. (If unclosed openers remain, count $> 0$).

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `s = "([{}])"`

| Step | Token `c` | Category | Action | Stack State (Top to Bottom) | Invariant Status |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **0** | — | — | Pre-allocate capacity $N/2$ | `[ ]` | Parity $6 \% 2 == 0$ |
| **1** | `'('` | Opener | Push expected `')'` | `[ ')' ]` | Valid prefix |
| **2** | `'['` | Opener | Push expected `']'` | `[ ']', ')' ]` | Valid prefix |
| **3** | `'{'` | Opener | Push expected `'}'` | `[ '}', ']', ')' ]` | Valid prefix |
| **4** | `'}'` | Closer | Pop and compare with `'}'` | `[ ']', ')' ]` | Matched `'}' == '}'` |
| **5** | `']'` | Closer | Pop and compare with `']'` | `[ ')' ]` | Matched `']' == ']'` |
| **6** | `')'` | Closer | Pop and compare with `')'` | `[ ]` | Matched `')' == ')'` |
| **End**| — | — | Check `stack.Count == 0` | `[ ]` | **Return `true`** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Expected Closers Push):** The optimal production implementation. Eliminates dictionary lookups, reduces branching, and allows pre-sizing the stack capacity to $N/2$ elements.
- **Approach 2 (Standard Stack with Dictionary Lookup):** Idiomatic and flexible when bracket rules are dynamic or extensible via configuration files.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Guard with `s.Length % 2 != 0`. Initialize `Stack<char>` with `capacity: s.Length / 2`.
- **Step 2: Main Exploration Loop:** Iterate each character in `s`.
- **Step 3: Invariant Maintenance & Condition Gates:** Push expected closers for openers; pop and verify equality for closers.
- **Step 4: Resolution & Return:** Return `stack.Count == 0`.

#### 4.3 Alternative Approaches Analysis
- **Dictionary/Hash Table Mapping:**
  Store pairs in `Dictionary<char, char> { {')', '('}, {']', '['}, {'}', '{'} }`.
  When closer arrives, verify `stack.Pop() == map[c]`.
  *Trade-off:* Adds hash lookup overhead and dictionary heap allocation compared to direct expected closer push.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Expected Closers Push (Optimal) | Approach 2: Dictionary Matching |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | `O(1)` (odd length) / `O(N)` / `O(N)` | `O(1)` / `O(N)` / `O(N)` |
| **Auxiliary Space** | `O(N)` (at most $N/2$ stack entries) | `O(N)` stack + $O(1)$ dictionary |
| **Output Space** | `O(1)` boolean | `O(1)` boolean |
| **Cache Locality** | High (contiguous stack buffer) | High |
| **In-Place Mutability** | Non-destructive read-only | Non-destructive read-only |
| **Streaming Suitability** | High (validates characters online) | High |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #36 - Valid Parentheses
// Core Pattern: LIFO Stack with Direct Expected-Closer Inversion
// Optimization: Pushing matching closers reduces comparison to direct O(1) equality
// Early Exit Defense: Odd string lengths are rejected in O(1) time
// ============================================================================
```

#### Implementation 1: Expected Closers Push (Optimal `O(N)` Time, `O(N)` Space)
```csharp
public class Solution
{
    public bool IsValid(string s)
    {
        // Boundary Defense: Odd-length strings can never form balanced pairs
        if (string.IsNullOrEmpty(s) || s.Length % 2 != 0)
        {
            return false;
        }

        // Pre-allocate stack capacity to avoid dynamic array resizing overhead
        var expectedClosers = new Stack<char>(capacity: s.Length / 2);

        foreach (char c in s)
        {
            // Gate 1: When an opener arrives, push the expected closer onto the stack
            if (c == '(')
            {
                expectedClosers.Push(')');
            }
            else if (c == '{')
            {
                expectedClosers.Push('}');
            }
            else if (c == '[')
            {
                expectedClosers.Push(']');
            }
            else
            {
                // Gate 2: Closing bracket arrived.
                // If stack is empty (orphan closer) or top does not match, sequence is invalid
                if (expectedClosers.Count == 0 || expectedClosers.Pop() != c)
                {
                    return false;
                }
            }
        }

        // Final Invariant: All opened scopes must be fully closed
        return expectedClosers.Count == 0;
    }
}
```

#### Implementation 2: Dictionary Matching Approach
```csharp
public class SolutionDictionary
{
    private static readonly Dictionary<char, char> BracketPairs = new()
    {
        { ')', '(' },
        { '}', '{' },
        { ']', '[' }
    };

    public bool IsValid(string s)
    {
        if (string.IsNullOrEmpty(s) || s.Length % 2 != 0) return false;

        var stack = new Stack<char>();

        foreach (char c in s)
        {
            if (BracketPairs.TryGetValue(c, out char matchingOpen))
            {
                // Closer encountered: verify matching open bracket on stack
                if (stack.Count == 0 || stack.Pop() != matchingOpen)
                {
                    return false;
                }
            }
            else
            {
                // Opener encountered: push directly
                stack.Push(c);
            }
        }

        return stack.Count == 0;
    }
}
```

---

## 37. Min Stack (LeetCode #155)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#stack` `#auxiliary-stack` `#constant-time-min` |
| **LeetCode Link** | [Min Stack](https://leetcode.com/problems/min-stack/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Design a stack that supports `push`, `pop`, `top`, and retrieving the minimum element in constant time `O(1)`.
  - `MinStack()` initializes the stack object.
  - `void Push(int val)` pushes the element `val` onto the stack.
  - `void Pop()` removes the element on the top of the stack.
  - `int Top()` gets the top element of the stack.
  - `int GetMin()` retrieves the minimum element in the stack.
- **Strict Requirement:** You must implement a solution with `O(1)` time complexity for each function.
- **Key Constraints:**
  - `-2^31 <= val <= 2^31 - 1`
  - Methods `pop`, `top` and `getMin` operations will always be called on non-empty stacks.
  - At most $3 \times 10^4$ calls will be made to `push`, `pop`, `top`, and `getMin`.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Track the historical prefix minimum alongside each stack frame so that popping naturally restores the previous minimum in $O(1)$ time.
- **Sample 1:**
  - `MinStack minStack = new MinStack();`
  - `minStack.Push(-2);`
  - `minStack.Push(0);`
  - `minStack.Push(-3);`
  - `minStack.GetMin(); // return -3`
  - `minStack.Pop();`
  - `minStack.Top();    // return 0`
  - `minStack.GetMin(); // return -2`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine climbing a mountain trail and carrying a diary. At every step you take, you write down your current elevation and the lowest point you have encountered on the trail up to that moment. If you take a step backward (pop), you simply cross out your last journal entry. The elevation of the lowest valley you saw prior to that step is right there, preserved on the previous line. Because a stack enforces strict LIFO ordering, the minimum of the stack at depth $k$ is an **immutable property of that prefix**!

#### 3.2 The Naive Bottleneck & Redundant Computation
A standard stack stores values alone. Calling `GetMin()` requires a linear search through all $N$ elements in the stack:
$$T(\text{GetMin}) = O(N)$$
Across $Q$ operations, total runtime balloons to $O(Q \cdot N) \approx 3 \times 10^4 \times 3 \times 10^4 \approx 9 \times 10^8$ operations, causing TLE. Trying to maintain a sorted data structure (like a `PriorityQueue` or Red-Black Tree) degrades `push` and `pop` to $O(\log N)$ time and does not support efficient arbitrary-element removal in $O(1)$.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Prefix Minimum Monotonicity:**
Let the stack contents from bottom to top be $[v_0, v_1, \dots, v_k]$.
Define the prefix minimum $m_k$ at depth $k$:
$$m_k = \begin{cases} v_0 & \text{if } k = 0 \\ \min(v_k, m_{k-1}) & \text{if } k > 0 \end{cases}$$
Because elements are removed in exact reverse order of insertion, when $v_k$ is popped, the minimum of the remaining stack is guaranteed to be $m_{k-1}$.
By storing each entry as a tuple `(Value: val, Min: currentMin)`, `GetMin()` is a simple $O(1)$ stack peek.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
TUPLE STACK ARCHITECTURE:

Depth 2: [ Value: -3 | Min: -3 ]  <-- Top of Stack (Current Min: -3)
Depth 1: [ Value:  0 | Min: -2 ]
Depth 0: [ Value: -2 | Min: -2 ]  <-- Bottom of Stack
----------------------------------
POP OPERATION:
Removes Depth 2. New Top is Depth 1.
New GetMin() immediately reads Depth 1's Min (-2) in O(1)!
```

- Each stack node stores:
  - `Value`: The actual integer element pushed.
  - `Min`: The running minimum of all elements from stack bottom up to this node.

#### 3.5 State Transition Triggers & Decision Gates
- **`Push(val)`:**
  - If `Count == 0`: `Push((val, val))`
  - Else: `int currentMin = Math.Min(val, Peek().Min); Push((val, currentMin))`
- **`Pop()`:** `stack.Pop()`
- **`Top()`:** `return stack.Peek().Value`
- **`GetMin()`:** `return stack.Peek().Min`

#### 3.6 Concrete Step-by-Step State Trace

| Operation | Input `val` | Stack State `(Value, Min)` [Bottom to Top] | Returned Value | Invariant Check |
| :--- | :--- | :--- | :--- | :--- |
| `Push(-2)` | `-2` | `[(-2, -2)]` | — | `Min = -2` |
| `Push(0)` | `0` | `[(-2, -2), (0, -2)]` | — | `Min = min(0, -2) = -2` |
| `Push(-3)` | `-3` | `[(-2, -2), (0, -2), (-3, -3)]` | — | `Min = min(-3, -2) = -3` |
| `GetMin()` | — | `[(-2, -2), (0, -2), (-3, -3)]` | `-3` | `Peek().Min == -3` |
| `Pop()` | — | `[(-2, -2), (0, -2)]` | — | Restores `Min = -2` |
| `Top()` | — | `[(-2, -2), (0, -2)]` | `0` | `Peek().Value == 0` |
| `GetMin()` | — | `[(-2, -2), (0, -2)]` | `-2` | `Peek().Min == -2` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Value-Min ValueTuple Stack):** Stores `(int Value, int Min)` in a single unified stack. Excellent cache locality, no secondary collection synchronization bugs, zero pointer dereferences.
- **Approach 2 (Two Synchronized Stacks):** Uses `Stack<int> _values` and `Stack<int> _mins`. Can be optimized to store duplicate minimums conditionally, saving space when minimums change infrequently.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Allocate underlying `Stack<(int Value, int Min)>`.
- **Step 2: Push Gate:** Compute running minimum against current stack top and push the tuple.
- **Step 3: Pop & Peek:** Expose standard LIFO operations with constant time lookups.

#### 4.3 Alternative Approaches Analysis
- **Difference Encoding ($O(1)$ space without tuples):**
  Store `diff = val - min` on stack and update `min`.
  *Drawback:* Requires 64-bit integer casting to prevent 32-bit arithmetic overflow on `val = int.MinValue`. Adds arithmetic instruction overhead on every call.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: ValueTuple Stack (Optimal) | Approach 2: Two Stacks |
| :--- | :--- | :--- |
| **Time Complexity (Push / Pop / Top / GetMin)** | `O(1)` / `O(1)` / `O(1)` / `O(1)` | `O(1)` / `O(1)` / `O(1)` / `O(1)` |
| **Auxiliary Space** | `O(N)` (8 bytes per node for tuple) | `O(N)` (split across two stacks) |
| **Memory Overhead** | Contiguous struct array buffer | Two separate heap stack buffers |
| **Cache Locality** | Maximum (value and min stored adjacently) | Moderate (two separate memory buffers) |
| **Thread Safety** | Requires external lock | Requires synchronized dual locks |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #37 - Min Stack
// Core Pattern: Historical Prefix-Minimum Monotonicity via ValueTuple
// Invariant: Each stack frame encapsulates the absolute minimum of its prefix
// Time Complexity: Strictly O(1) for Push, Pop, Top, and GetMin
// ============================================================================
```

#### Implementation 1: ValueTuple Stack (Optimal Contiguous Memory)
```csharp
public class MinStack
{
    // Single stack storing value paired with the historical minimum at that depth
    private readonly Stack<(int Value, int Min)> _stack;

    public MinStack()
    {
        _stack = new Stack<(int Value, int Min)>();
    }

    public void Push(int val)
    {
        // Gate 1: If stack is empty, val is the base minimum
        if (_stack.Count == 0)
        {
            _stack.Push((val, val));
        }
        else
        {
            // Gate 2: Compute running minimum between new val and previous prefix min
            int currentMin = Math.Min(val, _stack.Peek().Min);
            _stack.Push((val, currentMin));
        }
    }

    // Invariant: Popping automatically discards the local minimum and restores previous prefix min
    public void Pop()
    {
        _stack.Pop();
    }

    public int Top()
    {
        return _stack.Peek().Value;
    }

    public int GetMin()
    {
        return _stack.Peek().Min;
    }
}
```

#### Implementation 2: Two Stacks (Values and Sparse Minimums)
```csharp
public class MinStackTwoStacks
{
    private readonly Stack<int> _values = new();
    private readonly Stack<int> _mins = new();

    public void Push(int val)
    {
        _values.Push(val);

        // Push to min stack if val is less than or equal to current minimum
        if (_mins.Count == 0 || val <= _mins.Peek())
        {
            _mins.Push(val);
        }
    }

    public void Pop()
    {
        int popped = _values.Pop();
        // If the removed element is the current minimum, pop from min stack as well
        if (popped == _mins.Peek())
        {
            _mins.Pop();
        }
    }

    public int Top() => _values.Peek();
    public int GetMin() => _mins.Peek();
}
```

---

## 38. Evaluate Reverse Polish Notation (LeetCode #150)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#stack` `#postfix-evaluation` `#expression-parsing` |
| **LeetCode Link** | [Evaluate Reverse Polish Notation](https://leetcode.com/problems/evaluate-reverse-polish-notation/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given an array of strings `tokens` that represents an arithmetic expression in Reverse Polish Notation (RPN). Evaluate the expression. Return an integer that represents the value of the expression.
- **Arithmetic Rules:**
  - Valid operators are `'+'`, `'-'`, `'*'`, and `'/'`.
  - Each operand may be an integer or another expression.
  - Division between two integers always truncates toward zero.
  - No division by zero exists in the test cases.
- **Key Constraints:**
  - `1 <= tokens.Length <= 10^4`
  - `tokens[i]` is either an operator or an integer in range `[-200, 200]`.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Evaluate a postfix expression using an operand evaluation stack where binary operators consume the two most recently resolved values.
- **Sample 1:**
  - **Input:** `tokens = ["2","1","+","3","*"]`
  - **Output:** `9` (Explanation: `((2 + 1) * 3) = 9`)
- **Sample 2:**
  - **Input:** `tokens = ["4","13","5","/","+"]`
  - **Output:** `6` (Explanation: `(4 + (13 / 5)) = 6`)
- **Sample 3:**
  - **Input:** `tokens = ["10","6","9","3","+","-11","*","/","*","17","+","5","+"]`
  - **Output:** `22`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Picture a compiler's bytecode execution engine (such as the .NET CLR or JVM) or a classic Hewlett-Packard RPN scientific calculator. Numbers are pushed onto an execution evaluation stack as they appear. An operator acts as an instruction to pop the required arguments, run the ALU operation, and push the result back onto the stack. Postfix notation completely removes ambiguity: there are no parentheses, and precedence is explicitly enforced by the ordering of tokens.

#### 3.2 The Naive Bottleneck & Redundant Computation
Attempting to convert RPN back into standard infix notation (with parentheses) to evaluate it via an expression tree or string parsing engine introduces massive overhead:
- Tree building: $O(N)$ node allocations
- Parsing: Recursive evaluation or backtracking
RPN was designed specifically to be evaluated in a **single forward linear pass** in $O(N)$ time with zero backtracking.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**The Non-Commutative Operand Order Invariant:**
Addition and multiplication are commutative ($a + b = b + a, a \times b = b \times a$).
However, subtraction and division are strictly non-commutative:
$$a - b \neq b - a, \quad a / b \neq b / a$$
In postfix expression `[ "a", "b", "-" ]`, operand `a` was pushed first, followed by operand `b`.
When popping from the LIFO stack:
1. `right = stack.Pop()` $\implies$ First popped element is the **right-hand operand** ($b$).
2. `left = stack.Pop()` $\implies$ Second popped element is the **left-hand operand** ($a$).
3. Result $= left - right$ (or $left / right$).
Reversing this order is the single most common bug in evaluation stack implementations.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
EVALUATION PIPELINE:
Token: "-"
Stack before operation:
Top -> [ 5 ]   <-- right = stack.Pop()
       [ 13 ]  <-- left  = stack.Pop()
--------------------------------------
EXECUTION:
result = left - right = 13 - 5 = 8
stack.Push(8)

Stack after operation:
Top -> [ 8 ]
```

- `stack`: Accumulates intermediate operands and sub-expression evaluations.
- `token`: Read-only forward stream cursor.

#### 3.5 State Transition Triggers & Decision Gates
For each `token` in `tokens`:
1. **Operator Gate (`+`, `-`, `*`, `/`):**
   - Pop `right` operand.
   - Pop `left` operand.
   - Compute `left <op> right` and push result back onto `stack`.
2. **Operand Gate (Number):**
   - Parse integer via `int.Parse(token)` and push onto `stack`.
3. **Termination:**
   - Expression validity guarantees that exactly one integer remains on the stack. Return `stack.Pop()`.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `tokens = ["4", "13", "5", "/", "+"]`

| Step | Token | Type | Operands Popped | Calculation | Stack Contents (Bottom to Top) |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **1** | `"4"` | Number | — | — | `[ 4 ]` |
| **2** | `"13"` | Number | — | — | `[ 4, 13 ]` |
| **3** | `"5"` | Number | — | — | `[ 4, 13, 5 ]` |
| **4** | `"/"` | Operator | `right=5, left=13` | $13 / 5 = 2$ | `[ 4, 2 ]` |
| **5** | `"+"` | Operator | `right=2, left=4` | $4 + 2 = 6$ | `[ 6 ]` |
| **End**| — | — | Pop final result | — | Return `6` |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Stack with Pattern Matching):** Clear, highly readable, idiomatic C# with modern `switch` expression.
- **Approach 2 (Fixed Array Pointer Stack):** Simulates a stack using a raw `int[]` array and an integer index pointer `top`. Eliminates all heap allocations and BCL collection overhead. Ideal for high-throughput execution engines.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Instantiate stack (or pre-allocate array of size `tokens.Length`).
- **Step 2: Token Iteration:** Process each token sequentially.
- **Step 3: Operand vs Operator Branching:** Evaluate binary operations respecting operand order.
- **Step 4: Return:** Return single remaining element.

#### 4.3 Alternative Approaches Analysis
- **Simulated Array Stack:** Since each binary operation consumes two elements and produces one, the stack depth never exceeds `tokens.Length`. A raw array `int[tokens.Length]` with pointer `int top = 0` provides maximum possible execution speed.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: BCL Stack with Pattern Matching | Approach 2: Array Buffer Simulated Stack |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | `O(N)` / `O(N)` / `O(N)` | `O(N)` / `O(N)` / `O(N)` |
| **Auxiliary Space** | `O(N)` | `O(N)` pre-allocated array |
| **GC Pressure** | Minimal (struct pushes) | Zero (single stackalloc / array allocation) |
| **Cache Locality** | High | Maximum contiguous CPU cache locality |
| **Defensive Boundaries** | Checked BCL stack operations | Direct array index bounds |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #38 - Evaluate Reverse Polish Notation
// Core Pattern: Bytecode Evaluation Stack with Non-Commutative Operand Invariant
// Critical Order Invariant: first popped = right operand; second popped = left operand
// Truncation: Integer division in C# naturally truncates towards zero as mandated
// ============================================================================
```

#### Implementation 1: BCL Stack with Switch Expression (Optimal `O(N)`)
```csharp
public class Solution
{
    public int EvalRPN(string[] tokens)
    {
        // Boundary Defense: Guaranteed valid expression has at least 1 token
        if (tokens == null || tokens.Length == 0)
        {
            return 0;
        }

        var stack = new Stack<int>(capacity: tokens.Length);

        foreach (string token in tokens)
        {
            // Gate 1: Check if the token is a binary operator
            if (token is "+" or "-" or "*" or "/")
            {
                // CRITICAL INVARIANT: First popped is right operand; second is left operand.
                int right = stack.Pop();
                int left = stack.Pop();

                // Compute binary operation (division natively truncates toward zero in C#)
                int result = token switch
                {
                    "+" => left + right,
                    "-" => left - right,
                    "*" => left * right,
                    "/" => left / right,
                    _ => throw new InvalidOperationException($"Unsupported operator: {token}")
                };

                // Push evaluated sub-expression back onto evaluation stack
                stack.Push(result);
            }
            else
            {
                // Gate 2: Token is an integer operand
                stack.Push(int.Parse(token));
            }
        }

        // Final result is the single remaining value on the stack
        return stack.Pop();
    }
}
```

#### Implementation 2: Array Buffer Simulated Stack (Zero Heap Allocations)
```csharp
public class SolutionArrayStack
{
    public int EvalRPN(string[] tokens)
    {
        // Array acts as a raw LIFO stack; top tracks active index
        int[] stack = new int[tokens.Length];
        int top = 0;

        foreach (string token in tokens)
        {
            if (token is "+" or "-" or "*" or "/")
            {
                int right = stack[--top];
                int left = stack[--top];

                stack[top++] = token switch
                {
                    "+" => left + right,
                    "-" => left - right,
                    "*" => left * right,
                    "/" => left / right,
                    _ => 0
                };
            }
            else
            {
                stack[top++] = int.Parse(token);
            }
        }

        return stack[0];
    }
}
```

---

## 39. Daily Temperatures (LeetCode #739)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#monotonic-stack` `#next-greater-element` `#index-tracking` |
| **LeetCode Link** | [Daily Temperatures](https://leetcode.com/problems/daily-temperatures/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array of integers `temperatures` represents the daily temperatures, return an array `answer` such that `answer[i]` is the number of days you have to wait after the $i$-th day to get a warmer temperature. If there is no future day for which this is possible, keep `answer[i] == 0` instead.
- **Key Constraints:**
  - `1 <= temperatures.Length <= 10^5`
  - `30 <= temperatures[i] <= 100`

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Find the Next Greater Element (NGE) for every array position in amortized $O(N)$ time using a monotonic decreasing stack of unresolved indices.
- **Sample 1:**
  - **Input:** `temperatures = [73, 74, 75, 71, 69, 72, 76, 73]`
  - **Output:** `[1, 1, 4, 2, 1, 1, 0, 0]`
- **Sample 2:**
  - **Input:** `temperatures = [30, 40, 50, 60]`
  - **Output:** `[1, 1, 1, 0]`
- **Sample 3:**
  - **Input:** `temperatures = [30, 60, 90]`
  - **Output:** `[1, 1, 0]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Picture standing on a ridge looking across a sequence of mountain peaks. Every day you look forward waiting for a higher peak that catches the morning sunlight. When a taller peak appears, it immediately resolves the view for every shorter peak you were previously standing on that was waiting for a warmer day. A **Monotonic Decreasing Stack** acts as a "waiting room" for all prior days that have not yet seen a warmer day. The moment a day arrives that is warmer than the top of the waiting room, that top day has found its answer and leaves the room.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force solution uses nested loops: for each day $i$, scan days $j = i + 1 \dots N - 1$ until finding $T[j] > T[i]$.
In the worst-case scenario of strictly descending temperatures (e.g. `[100, 99, 98, ..., 31, 30]`), no future day is warmer. For each day $i$, the inner loop scans all the way to the end of the array:
$$T(N) = \sum_{i=0}^{N-1} (N - 1 - i) = \frac{N(N-1)}{2} \implies O(N^2)$$
For $N = 10^5$, $N^2 = 10^{10}$ operations, exceeding standard time limits by two orders of magnitude ($> 10$ seconds).

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**The Monotonic Decreasing Stack Invariant:**
Store the **indices** of days whose next warmer day has not yet been discovered.
The temperatures at the indices currently in the stack are strictly monotonically non-increasing (decreasing) from bottom to top:
$$temperatures[stack[0]] \ge temperatures[stack[1]] \ge \dots \ge temperatures[stack[top]]$$

When day $i$ with temperature $T[i]$ arrives:
- If $T[i] > temperatures[stack.Peek()]$:
  - Day $i$ is the **earliest and nearest warmer day** for the index at the stack top!
  - Pop `prevIndex = stack.Pop()`.
  - Calculate its waiting span:
    $$answer[prevIndex] = i - prevIndex$$
  - Continue popping and resolving earlier days as long as $T[i] > temperatures[stack.Peek()]$.
- Once all colder days are evicted and resolved, push index $i$ onto the stack.

**Amortized Complexity Proof:**
Every index $i \in [0, N-1]$ is pushed onto the stack exactly once. Every index is popped from the stack at most once. Therefore, the inner `while` loop executes at most $N$ times across the *entire* lifecycle of the algorithm.
Total operations $\le 2N \implies O(N)$ linear time.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
MONOTONIC DECREASING STACK ARCHITECTURE:

Active Day i: Index 6, Temp = 76

WAITING ROOM (Monotonic Stack of Unresolved Indices):
Top -> [ idx: 5, Temp: 72 ]  <-- 76 > 72 ==> POP! answer[5] = 6 - 5 = 1
       [ idx: 4, Temp: 69 ]  <-- 76 > 69 ==> POP! answer[4] = 6 - 4 = 2
       [ idx: 3, Temp: 71 ]  <-- 76 > 71 ==> POP! answer[3] = 6 - 3 = 3
       [ idx: 2, Temp: 75 ]  <-- 76 > 75 ==> POP! answer[2] = 6 - 2 = 4
Base ---------------------------------------

After resolving all colder days: Push idx 6 (Temp: 76).
```

- `i`: Current day scanner advancing from $0$ to $N - 1$.
- `stack`: Contains indices of days waiting for a warmer day.
- `answer[k]`: Final recorded distance for day $k$. Initialized to $0$ by default.

#### 3.5 State Transition Triggers & Decision Gates
At each day $i$:
1. **Eviction / Resolution Gate:**
   - While `stack.Count > 0 && temperatures[i] > temperatures[stack.Peek()]`:
     - `int prev = stack.Pop()`
     - `answer[prev] = i - prev`
2. **Registration Gate:**
   - `stack.Push(i)`
3. **Termination:**
   - Any indices remaining in the stack have no future warmer day; their `answer` values remain $0$.

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `temperatures = [73, 74, 75, 71, 69, 72, 76, 73]`

| Day $i$ | Temp $T[i]$ | Stack Before | Action / Popped Indices | Resolved `answer` Entry | Stack After |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **0** | 73 | `[]` | Push 0 | — | `[0(73)]` |
| **1** | 74 | `[0(73)]` | 74 > 73 $\implies$ Pop 0 | `answer[0] = 1 - 0 = 1` | `[1(74)]` |
| **2** | 75 | `[1(74)]` | 75 > 74 $\implies$ Pop 1 | `answer[1] = 2 - 1 = 1` | `[2(75)]` |
| **3** | 71 | `[2(75)]` | 71 <= 75 $\implies$ Push 3 | — | `[2(75), 3(71)]` |
| **4** | 69 | `[2, 3]` | 69 <= 71 $\implies$ Push 4 | — | `[2(75), 3(71), 4(69)]` |
| **5** | 72 | `[2, 3, 4]` | 72 > 69 $\implies$ Pop 4; 72 > 71 $\implies$ Pop 3 | `answer[4]=1, answer[3]=2` | `[2(75), 5(72)]` |
| **6** | 76 | `[2, 5]` | 76 > 72 $\implies$ Pop 5; 76 > 75 $\implies$ Pop 2 | `answer[5]=1, answer[2]=4` | `[6(76)]` |
| **7** | 73 | `[6(76)]` | 73 <= 76 $\implies$ Push 7 | — | `[6(76), 7(73)]` |

Final `answer`: `[1, 1, 4, 2, 1, 1, 0, 0]`

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Left-to-Right Online Processing):** The standard intuitive model. Processes days as an incoming event stream, immediately resolving waiting tasks as soon as the warmer event occurs.
- **Approach 2 (Right-to-Left Monotonic Stack):** Traverses from future to past. When examining day $i$, already possesses the full future horizon on the stack, resolving day $i$'s answer before pushing it.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Instantiate `int[] answer = new int[n]`. Initialize `Stack<int>`.
- **Step 2: Linear Exploration Loop:** Iterate index $i$ from $0$ to $n - 1$.
- **Step 3: Monotonic Eviction Gate:** While current temperature exceeds stack top, pop and compute span.
- **Step 4: Push & Repeat:** Push $i$. Return `answer`.

#### 4.3 Alternative Approaches Analysis
- **Right-to-Left Traversal:**
  - Loop $i = n - 1$ down to $0$.
  - Pop all elements where $temperatures[stack.Peek()] \le temperatures[i]$ (since they can never serve as next warmer day for any day to the left of $i$).
  - `answer[i] = stack.Count == 0 ? 0 : stack.Peek() - i`.
  - Push $i$.
  - *Trade-off:* Symmetrical $O(N)$ complexity, but requires full offline array access.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Left-to-Right Online Stack | Approach 2: Right-to-Left Offline Stack |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | `O(N)` / `O(N)` / `O(N)` | `O(N)` / `O(N)` / `O(N)` |
| **Auxiliary Space** | `O(N)` (stack holds unresolved indices) | `O(N)` (stack holds candidate horizons) |
| **Output Space** | `O(N)` result array | `O(N)` result array |
| **Amortized Ops per Element** | $\le 2$ operations per index | $\le 2$ operations per index |
| **Streaming Suitability** | High (handles continuous data feeds) | Low (requires knowing array end) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #39 - Daily Temperatures
// Core Pattern: Monotonic Decreasing Stack (Index Tracking)
// Amortized Invariant: Every index is pushed once and popped at most once => O(N)
// Resolution Trigger: T[i] > T[stack.Peek()] immediately resolves stack top
// ============================================================================
```

#### Implementation 1: Monotonic Stack (Left-to-Right Online Processing)
```csharp
public class Solution
{
    public int[] DailyTemperatures(int[] temperatures)
    {
        // Boundary Defense: Empty or single-day array has no future warmer days
        if (temperatures == null || temperatures.Length == 0)
        {
            return Array.Empty<int>();
        }

        int n = temperatures.Length;
        int[] answer = new int[n]; // Default initialized to 0
        var monotonicStack = new Stack<int>(); // Stores indices with unresolved temperatures

        for (int i = 0; i < n; i++)
        {
            int currentTemp = temperatures[i];

            // Gate 1: Monotonic Eviction Invariant
            // While current temperature is warmer than the temperature at the stack top,
            // day 'i' is the exact first warmer day for the day at stack.Peek().
            while (monotonicStack.Count > 0 && currentTemp > temperatures[monotonicStack.Peek()])
            {
                int prevDayIndex = monotonicStack.Pop();
                answer[prevDayIndex] = i - prevDayIndex; // Record waiting day span
            }

            // Gate 2: Push current day's index into the waiting room
            monotonicStack.Push(i);
        }

        // Indices remaining in the stack have no warmer future day; answer remains 0
        return answer;
    }
}
```

#### Implementation 2: Right-to-Left Traversal Approach
```csharp
public class SolutionRightToLeft
{
    public int[] DailyTemperatures(int[] temperatures)
    {
        if (temperatures == null || temperatures.Length == 0) return Array.Empty<int>();

        int n = temperatures.Length;
        int[] answer = new int[n];
        var stack = new Stack<int>();

        // Traverse backwards from future to past
        for (int i = n - 1; i >= 0; i--)
        {
            // Evict all future days that are colder or equal (they are shadowed by day i)
            while (stack.Count > 0 && temperatures[stack.Peek()] <= temperatures[i])
            {
                stack.Pop();
            }

            // Invariant: Nearest warmer day is sitting right at the top of the stack
            answer[i] = stack.Count == 0 ? 0 : stack.Peek() - i;

            stack.Push(i);
        }

        return answer;
    }
}
```

---

## 40. Largest Rectangle in Histogram (LeetCode #84)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#monotonic-stack` `#previous-next-smaller` `#boundary-expansion` |
| **LeetCode Link** | [Largest Rectangle in Histogram](https://leetcode.com/problems/largest-rectangle-in-histogram/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array of integers `heights` representing the histogram's bar height where the width of each bar is `1`, return the area of the largest rectangle in the histogram.
- **Key Constraints:**
  - `1 <= heights.Length <= 10^5`
  - `0 <= heights[i] <= 10^4`

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** For every bar of height $H$, compute the widest horizontal span $[L+1, R-1]$ it can extend across without encountering a strictly shorter bar. Resolve all boundaries simultaneously in $O(N)$ time using a monotonic increasing stack.
- **Sample 1:**
  - **Input:** `heights = [2, 1, 5, 6, 2, 3]`
  - **Output:** `10` (Explanation: Bars at index 2 and 3 have heights 5 and 6; rectangle of height 5 and width 2 has area 10)
- **Sample 2:**
  - **Input:** `heights = [2, 4]`
  - **Output:** `4`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a row of buildings of different heights. If you are standing on top of building $i$ of height $H$, you want to stretch a horizontal steel beam as far left and as far right as possible. Your beam extends unimpeded until it collides with a building strictly shorter than $H$.
- The **Left Limiting Wall** is the nearest strictly shorter bar to the left.
- The **Right Limiting Wall** is the nearest strictly shorter bar to the right.
Finding the largest rectangle in the histogram is equivalent to finding the left and right limiting walls for **every single bar**, multiplying height by width, and taking the global maximum.

#### 3.2 The Naive Bottleneck & Redundant Computation
1. **All Subarrays:** Inspecting all pairs $(i, j)$ and finding the minimum height in range takes $O(N^3)$ or $O(N^2)$ time.
2. **Independent Boundary Scans:** For each bar $i$, run two `while` loops extending left and right. In the worst case of monotonically ascending bars (e.g. `[1, 2, 3, 4, ..., N]`), the left scan walks back to index 0 on every step:
   $$T(N) = \sum_{i=1}^N i = \frac{N(N+1)}{2} \implies O(N^2)$$
   For $N = 10^5$, $N^2 = 10^{10}$ operations, resulting in immediate TLE.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**Dual-Boundary Resolution via Monotonic Increasing Stack:**
Maintain a stack of bar indices whose heights are strictly monotonically increasing:
$$heights[stack[0]] < heights[stack[1]] < \dots < heights[stack[top]]$$

When bar $i$ arrives with $heights[i] < heights[stack.Peek()]$:
1. The bar at the top of the stack, `poppedIdx = stack.Pop()`, has met its **Right Limiting Wall**: it is index $i$!
2. The new stack top, `stack.Peek()`, is the nearest strictly smaller bar on the left, which is its **Left Limiting Wall**!
3. The span width is calculated in $O(1)$ directly:
   $$\text{width} = (stack.Count == 0) \ ? \ i \ : \ (i - stack.Peek() - 1)$$
4. The area bounded by `heights[poppedIdx]` is:
   $$\text{area} = heights[poppedIdx] \times \text{width}$$

**The Virtual Sentinel Zero Invariant:**
If we append a virtual bar of height `0` at index $N$, it will be strictly smaller than any valid histogram bar ($heights[j] \ge 0$). This forces the stack to flush and resolve every remaining bar, completely eliminating any post-loop cleanup code.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
DUAL-BOUNDARY GEOMETRY:
Index:          0   1   2   3   4   5   6 (sentinel)
Heights:      [ 2,  1,  5,  6,  2,  3,  0 ]
                        ▲   ▲   ▲
                      left mid  i (Right Limiting Wall)

STACK STATE when i = 4 (Height 2):
Top -> idx 3 (Height 6)
       idx 2 (Height 5)
       idx 1 (Height 1)

ACTION 1: Pop idx 3 (H = 6).
Right Wall: i = 4. Left Wall: stack top = 2.
Width = i - stack.Peek() - 1 = 4 - 2 - 1 = 1. Area = 6 * 1 = 6.

ACTION 2: Pop idx 2 (H = 5).
Right Wall: i = 4. Left Wall: stack top = 1.
Width = i - stack.Peek() - 1 = 4 - 1 - 1 = 2. Area = 5 * 2 = 10. (Max Area!)
```

- `i`: Active cursor scanning from $0$ to $N$ (where index $N$ has virtual height 0).
- `stack`: Monotonic increasing stack of bar indices.
- `poppedIdx`: Bar whose maximum expansion rectangle is currently being evaluated.

#### 3.5 State Transition Triggers & Decision Gates
For $i = 0$ to $N$:
1. `currentHeight = (i == n) ? 0 : heights[i]`
2. **Eviction / Calculation Gate:**
   - While `stack.Count > 0 && currentHeight < heights[stack.Peek()]`:
     - `int h = heights[stack.Pop()]`
     - `int w = stack.Count == 0 ? i : (i - stack.Peek() - 1)`
     - `maxArea = Math.Max(maxArea, h * w)`
3. **Registration Gate:**
   - `stack.Push(i)`
4. **Return:** `maxArea`

#### 3.6 Concrete Step-by-Step State Trace
Trace input: `heights = [2, 1, 5, 6, 2, 3]` ($N=6$, with virtual sentinel at $i=6$)

| $i$ | $H[i]$ | Stack Action | Popped Bar ($H$) | Left Wall | Width ($w$) | Area ($H \times w$) | `maxArea` |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **0** | 2 | Push 0 | — | — | — | — | 0 |
| **1** | 1 | 1 < 2 $\implies$ Pop 0 | 0 ($H=2$) | None (empty) | $1$ | $2 \times 1 = 2$ | 2 |
| | | Push 1 | — | — | — | — | 2 |
| **2** | 5 | Push 2 | — | — | — | — | 2 |
| **3** | 6 | Push 3 | — | — | — | — | 2 |
| **4** | 2 | 2 < 6 $\implies$ Pop 3 | 3 ($H=6$) | idx 2 | $4 - 2 - 1 = 1$ | $6 \times 1 = 6$ | 6 |
| | | 2 < 5 $\implies$ Pop 2 | 2 ($H=5$) | idx 1 | $4 - 1 - 1 = 2$ | $5 \times 2 = 10$ | **10** |
| | | Push 4 | — | — | — | — | 10 |
| **5** | 3 | Push 5 | — | — | — | — | 10 |
| **6** | 0 | 0 < 3 $\implies$ Pop 5 | 5 ($H=3$) | idx 4 | $6 - 4 - 1 = 1$ | $3 \times 1 = 3$ | 10 |
| | | 0 < 2 $\implies$ Pop 4 | 4 ($H=2$) | idx 1 | $6 - 1 - 1 = 4$ | $2 \times 4 = 8$ | 10 |
| | | 0 < 1 $\implies$ Pop 1 | 1 ($H=1$) | None (empty) | $6$ | $1 \times 6 = 6$ | 10 |
| | | Push 6 | — | — | — | — | 10 |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Single-Pass Monotonic Stack with Sentinel 0):** The definitive optimal algorithm. 25 lines of code, strictly $O(N)$ single pass, resolves both boundaries concurrently with zero code duplication.
- **Approach 2 (Precomputed Left and Right Smaller Arrays):** Uses two independent passes to populate `leftSmaller[]` and `rightSmaller[]` arrays. Helpful for pedagogical clarity, but uses $3 \times O(N)$ passes and $3 \times O(N)$ auxiliary memory.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Check if array is null or empty. Initialize `stack` and `maxArea = 0`.
- **Step 2: Traverse with Virtual Sentinel:** Loop $i$ from $0$ to $N$. If $i == N$, set height to $0$.
- **Step 3: Invariant Maintenance & Area Resolution:** While stack top is taller than current bar, pop it, determine width from new top to $i$, compute area, update `maxArea`.
- **Step 4: Push & Return:** Push $i$. When loop terminates, return `maxArea`.

#### 4.3 Alternative Approaches Analysis
- **Divide and Conquer (Segment Tree):**
  Find the index of the minimum bar in the current range $[L, R]$, compute `height[min] * (R - L + 1)`, and recurse on left and right subranges.
  *Complexity:* $O(N \log N)$ average, but degrades to $O(N^2)$ in skewed histograms unless an expensive Range Minimum Query (RMQ) Segment Tree is constructed ($O(N)$ build, $O(\log N)$ query).

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Single-Pass Sentinel Stack | Approach 2: Explicit Boundary Arrays |
| :--- | :--- | :--- |
| **Time Complexity (Best / Avg / Worst)** | `O(N)` / `O(N)` / `O(N)` | `O(N)` / `O(N)` / `O(N)` |
| **Auxiliary Space** | `O(N)` (stack only) | `3 * O(N)` (stack + 2 boundary arrays) |
| **Output Space** | `O(1)` integer | `O(1)` integer |
| **Pass Count** | Exactly 1 pass | 3 passes |
| **Code Footprint** | Extremely compact | Verbose (three separate loops) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Problem #40 - Largest Rectangle in Histogram
// Core Pattern: Monotonic Increasing Stack with Dual-Boundary Expansion
// Key Sentinel Invariant: Virtual height 0 at index N flushes all residual bars
// Width Invariant: width = stack.Count == 0 ? i : (i - stack.Peek() - 1)
// ============================================================================
```

#### Implementation 1: Monotonic Stack with Virtual Sentinel (Optimal Single-Pass `O(N)`)
```csharp
public class Solution
{
    public int LargestRectangleArea(int[] heights)
    {
        // Boundary Defense: Empty histogram has area 0
        if (heights == null || heights.Length == 0)
        {
            return 0;
        }

        int n = heights.Length;
        var stack = new Stack<int>(); // Stores bar indices in strictly increasing height order
        int maxArea = 0;

        // Iterate up to n; index n acts as a virtual sentinel bar of height 0
        for (int i = 0; i <= n; i++)
        {
            // Gate 1: When i reaches n, virtual height 0 forces eviction of all remaining bars
            int currentHeight = (i == n) ? 0 : heights[i];

            // Gate 2: Monotonic Increasing Invariant Maintenance
            // If current bar is shorter than the bar at stack top, the bar at stack top
            // cannot expand rightwards past index 'i'. Index 'i' is its Right Limiting Wall!
            while (stack.Count > 0 && currentHeight < heights[stack.Peek()])
            {
                int poppedIndex = stack.Pop();
                int height = heights[poppedIndex];

                // Gate 3: Dual-Boundary Width Calculation
                // - Right Limiting Wall is current index 'i'.
                // - Left Limiting Wall is the new stack top (the nearest smaller bar to the left).
                // - If stack is empty, popped bar was the smallest seen so far and spans from 0 to i - 1.
                int width = (stack.Count == 0) ? i : (i - stack.Peek() - 1);

                maxArea = Math.Max(maxArea, height * width);
            }

            // Gate 4: Push current index to maintain monotonic increasing order
            stack.Push(i);
        }

        return maxArea;
    }
}
```

#### Implementation 2: Explicit Boundary Arrays Approach (`O(N)` Time, `O(N)` Memory)
```csharp
public class SolutionExplicitArrays
{
    public int LargestRectangleArea(int[] heights)
    {
        if (heights == null || heights.Length == 0) return 0;

        int n = heights.Length;
        int[] leftSmaller = new int[n];
        int[] rightSmaller = new int[n];
        var stack = new Stack<int>();

        // Pass 1: Find nearest smaller bar to the left
        for (int i = 0; i < n; i++)
        {
            while (stack.Count > 0 && heights[stack.Peek()] >= heights[i])
            {
                stack.Pop();
            }
            leftSmaller[i] = (stack.Count == 0) ? -1 : stack.Peek();
            stack.Push(i);
        }

        stack.Clear();

        // Pass 2: Find nearest smaller bar to the right
        for (int i = n - 1; i >= 0; i--)
        {
            while (stack.Count > 0 && heights[stack.Peek()] >= heights[i])
            {
                stack.Pop();
            }
            rightSmaller[i] = (stack.Count == 0) ? n : stack.Peek();
            stack.Push(i);
        }

        // Pass 3: Calculate maximum area across all bars
        int maxArea = 0;
        for (int i = 0; i < n; i++)
        {
            int width = rightSmaller[i] - leftSmaller[i] - 1;
            maxArea = Math.Max(maxArea, heights[i] * width);
        }

        return maxArea;
    }
}
```

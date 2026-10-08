# 📘 Week 06 Day 3: Parentheses & Bracket Matching — Engineering Guide

> 🧭 **Navigation:** [← Previous Day](Week_06_Day_02_Substring_Sliding_Window_Patterns_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_06_Day_04_String_Transformations_Building_Instructional.md)
> 
> 💡 **Instructor Note:** *Parentheses problems model hierarchical syntax and balanced tree grammars. The core mechanism is Last-In-First-Out (LIFO) stack discipline: each closing token must immediately cancel the most recent unmatched opening token of the identical category.*

---

## 🎯 LEARNING OBJECTIVES

By the end of this chapter, you will be able to:

- **Internalize** why single counters succeed for single-bracket grammars but fail catastrophically for multiple bracket types, necessitating a LIFO stack.
- **Implement** `O(N)` validation with early termination on odd string lengths (`N % 2 != 0`).
- **Master** the sentinel index-stack technique (`stack.Push(-1)`) to compute Longest Valid Parentheses in a single linear pass.
- **Generate** all well-formed parentheses combinations using Catalan-bound backtracking (`O(4^N / sqrt(N))`).
- **Deliver** a structured 45-minute technical interview script explaining stack state invariants.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Engineering Challenge

Hierarchical balance verification is fundamental to compilers, serialization formats, and developer tools:
1. **Compilers & AST Generation:** Language parsers (Clang, Roslyn, GCC) reject syntax trees when bracket scopes (`{}`, `()`, `[]`) are ill-formed.
2. **Serialization Validation:** Streaming JSON and XML decoders must reject malformed nesting payloads before allocating in-memory document object models.
3. **IDE Highlighting:** Code editors track bracket pairs across large files in sub-millisecond time.

A single bracket mismatch invalidates entire programs. While a simple integer counter can track balanced parentheses of a single type (e.g., only `(` and `)`), it fails on mixed bracket types like `"([)]"` where counts match but nesting sequence violates scoping rules.

> [!NOTE]
> **Interview & Systems Context:** In coding interviews, immediately note that an odd-length string (`s.Length % 2 != 0`) cannot possibly be balanced, allowing `O(1)` immediate rejection. For single-bracket types, mention the space optimization from `O(N)` stack to `O(1)` integer counter, but clarify why a stack is strictly mandatory whenever multiple bracket varieties exist.

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### The LIFO Nesting Invariant

A bracketed sequence is valid if and only if:
1. Every closing bracket matches the most recently opened, unclosed bracket of the exact same type.
2. At every prefix of the string, the number of closing brackets of each type never exceeds its corresponding open brackets.
3. At the end of the string, all opened brackets have been matched and closed (stack is empty).

```
Valid Matching ("({[]})"):
  Action:      Push '('    Push '{'    Push '['    Pop '['     Pop '{'     Pop '('
  Incoming:       (           {           [           ]           }           )
  Stack Top:    [ ( ]       [ { ]       [ [ ]       [ { ]       [ ( ]        [ ] (Empty -> Valid)
                            [ ( ]       [ { ]       [ ( ]
                                        [ ( ]

Invalid Interleaved Matching ("([)]"):
  Action:      Push '('    Push '['    Encounter ')'
  Incoming:       (           [             )
  Stack Top:    [ ( ]       [ [ ]      Top is '[' but expected '('!
                            [ ( ]      -> MISMATCH! Immediate Failure
```

### Visualizing Longest Valid Parentheses (Sentinel Index Stack)

To compute the longest valid parentheses substring, store **indices** rather than characters. Initialize the stack with `-1` to serve as a base boundary for length calculations:

```
String:      )     (     (     )     )
Index:       0     1     2     3     4

Initial:  Stack = [-1]

i = 0, char ')':
  Pop -1. Stack is empty!
  Push current index 0 as new base boundary.
  Stack = [ 0 ]

i = 1, char '(':
  Push index 1.
  Stack = [ 0, 1 ]

i = 2, char '(':
  Push index 2.
  Stack = [ 0, 1, 2 ]

i = 3, char ')':
  Pop index 2. Stack top is now 1.
  Valid length = i - stack.Peek() = 3 - 1 = 2 (substring: "()")
  Max = 2
  Stack = [ 0, 1 ]

i = 4, char ')':
  Pop index 1. Stack top is now 0.
  Valid length = i - stack.Peek() = 4 - 0 = 4 (substring: "(())")
  Max = 4
  Stack = [ 0 ]

Result: Longest valid substring length = 4.
```

### Taxonomy of Parentheses Patterns

| Pattern | Problem Objective | Data Structure | Time Complexity | Auxiliary Space |
| :--- | :--- | :--- | :--- | :--- |
| **Valid Parentheses** | Boolean validation of multiple bracket types | `Stack<char>` | `O(N)` | `O(N)` |
| **Longest Valid Parentheses** | Max length of valid balanced substring | `Stack<int>` (indices) | `O(N)` | `O(N)` |
| **Longest Valid Parentheses (O(1) Space)**| Max length of valid balanced substring | Left-Right two-pass counters | `O(N)` | `O(1)` |
| **Generate Parentheses** | All valid combinations of `N` pairs | Backtracking recursion | `O(4^N / sqrt(N))` | `O(N)` |
| **Minimum Remove to Make Valid** | Remove minimal invalid parentheses | `Stack<int>` + boolean mask | `O(N)` | `O(N)` |

---

## ⚙️ CHAPTER 3: MECHANICS & IMPLEMENTATION

### Operation 1: Valid Parentheses (LeetCode 20)

1. Early guard: If `s.Length % 2 != 0`, return `false`.
2. Iterate through characters:
   - If opening bracket (`(`, `{`, `[`), push onto stack.
   - If closing bracket, verify `stack.Count > 0` and `stack.Pop() == matchingOpen`. If not, return `false`.
3. At termination, return `stack.Count == 0`.

### Operation 2: Longest Valid Parentheses (LeetCode 32)

1. Push `-1` to stack.
2. For each index `i`:
   - If `s[i] == '('`: push `i`.
   - If `s[i] == ')'`: pop top index.
     - If stack becomes empty: push `i` as new base boundary.
     - If stack is not empty: `maxLen = max(maxLen, i - stack.Peek())`.

### Operation 3: Generate Parentheses (LeetCode 22)

Maintain state `(openCount, closeCount, currentString)`:
- If `openCount < n`: can safely append `'('`.
- If `closeCount < openCount`: can safely append `')'`.
- Base case: `currentString.Length == 2 * n`.

---

### 💻 Production-Grade Implementations

#### C# (.NET 8/9 — Zero Allocation & Modern Switch)

```csharp
using System;
using System.Collections.Generic;
using System.Text;

public static class ParenthesesSolutions
{
    /// <summary>
    /// Validates balanced brackets across multiple types: (), {}, [].
    /// Time Complexity: O(N) | Auxiliary Space: O(N)
    /// </summary>
    public static bool IsValid(ReadOnlySpan<char> s)
    {
        // Odd length strings cannot be paired
        if (s.Length % 2 != 0) return false;

        Stack<char> stack = new();

        foreach (char c in s)
        {
            switch (c)
            {
                case '(' or '{' or '[':
                    stack.Push(c);
                    break;
                case ')':
                    if (stack.Count == 0 || stack.Pop() != '(') return false;
                    break;
                case '}':
                    if (stack.Count == 0 || stack.Pop() != '{') return false;
                    break;
                case ']':
                    if (stack.Count == 0 || stack.Pop() != '[') return false;
                    break;
                default:
                    // Ignore non-bracket characters if permitted, or fail
                    return false;
            }
        }

        return stack.Count == 0;
    }

    /// <summary>
    /// Computes length of the longest valid parentheses substring.
    /// Time Complexity: O(N) | Auxiliary Space: O(N)
    /// </summary>
    public static int LongestValidParentheses(ReadOnlySpan<char> s)
    {
        if (s.Length < 2) return 0;

        Stack<int> indexStack = new();
        indexStack.Push(-1); // Sentinel base boundary
        int maxLength = 0;

        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] == '(')
            {
                indexStack.Push(i);
            }
            else // s[i] == ')'
            {
                indexStack.Pop();

                if (indexStack.Count == 0)
                {
                    // No matching open bracket; current index becomes base boundary
                    indexStack.Push(i);
                }
                else
                {
                    // Calculate distance to previous unmatched barrier
                    int currentLength = i - indexStack.Peek();
                    maxLength = Math.Max(maxLength, currentLength);
                }
            }
        }

        return maxLength;
    }

    /// <summary>
    /// Generates all combinations of well-formed parentheses for n pairs.
    /// Time Complexity: O(4^N / sqrt(N)) (Catalan) | Auxiliary Space: O(N)
    /// </summary>
    public static IList<string> GenerateParenthesis(int n)
    {
        List<string> result = new();
        StringBuilder sb = new(2 * n);
        Backtrack(result, sb, 0, 0, n);
        return result;
    }

    private static void Backtrack(List<string> result, StringBuilder sb, int open, int close, int max)
    {
        if (sb.Length == max * 2)
        {
            result.Add(sb.ToString());
            return;
        }

        if (open < max)
        {
            sb.Append('(');
            Backtrack(result, sb, open + 1, close, max);
            sb.Length--; // Backtrack
        }

        if (close < open)
        {
            sb.Append(')');
            Backtrack(result, sb, open, close + 1, max);
            sb.Length--; // Backtrack
        }
    }
}
```

#### Python (3.11+ — Idiomatic & Clean)

```python
class ParenthesesSolutions:
    @staticmethod
    def is_valid(s: str) -> bool:
        """
        Validates balanced brackets using a mapping dictionary and stack.
        Time: O(N) | Auxiliary Space: O(N)
        """
        if len(s) % 2 != 0:
            return False

        matching = {")": "(", "}": "{", "]": "["}
        stack: list[str] = []

        for char in s:
            if char in matching:
                if not stack or stack.pop() != matching[char]:
                    return False
            else:
                stack.append(char)

        return len(stack) == 0

    @staticmethod
    def longest_valid_parentheses(s: str) -> int:
        """
        Calculates length of the longest valid parentheses substring using sentinel index stack.
        Time: O(N) | Auxiliary Space: O(N)
        """
        if len(s) < 2:
            return 0

        stack: list[int] = [-1]  # Sentinel base
        max_length = 0

        for i, char in enumerate(s):
            if char == "(":
                stack.append(i)
            else:
                stack.pop()
                if not stack:
                    stack.append(i)  # New base boundary
                else:
                    max_length = max(max_length, i - stack[-1])

        return max_length

    @staticmethod
    def generate_parenthesis(n: int) -> list[str]:
        """
        Generates all combinations of well-formed parentheses for n pairs.
        Time: O(4^N / sqrt(N)) (Catalan) | Auxiliary Space: O(N)
        """
        result: list[str] = []

        def backtrack(
            current: list[str], open_count: int, close_count: int
        ) -> None:
            if len(current) == 2 * n:
                result.append("".join(current))
                return

            if open_count < n:
                current.append("(")
                backtrack(current, open_count + 1, close_count)
                current.pop()

            if close_count < open_count:
                current.append(")")
                backtrack(current, open_count, close_count + 1)
                current.pop()

        backtrack([], 0, 0)
        return result
```

---

## ⚖️ CHAPTER 4: COMPLEXITY DECONSTRUCTION

### Explicit Complexity Breakdown

| Algorithm / Operation | Time (Best Case) | Time (Worst Case) | Auxiliary Space | Output Space | Algorithmic Invariant |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Valid Parentheses** | `O(1)` (`N % 2 != 0` or early mismatch) | `O(N)` (balanced input) | `O(N)` (`N/2` open brackets) | `O(1)` | Stack top always holds unmatched opening token |
| **Longest Valid (Stack)** | `O(N)` | `O(N)` | `O(N)` (unmatched indices) | `O(1)` | `i - stack.Peek()` measures contiguous matched span |
| **Longest Valid (Two-Pass)** | `O(N)` | `O(N)` | `O(1)` (left/right counters) | `O(1)` | Left-to-right + right-to-left counter resets |
| **Generate Parentheses** | `O(4^N / sqrt(N))` | `O(4^N / sqrt(N))` | `O(N)` (recursion depth) | `O(C_n * 2N)` | `C_n` is the n-th Catalan number: `(2n)! / ((n+1)! * n!)` |

> [!TIP]
> **Understanding the Catalan Growth Rate:**
> For `N = 3`, `C_3 = 5` combinations. For `N = 8`, `C_8 = 1,430`. The number of well-formed parentheses grows as `O(4^N / (N * sqrt(N)))`. Because every generated string is strictly valid due to our pruning condition (`close < open`), the backtracking algorithm visits zero dead branches, making it asymptotically optimal.

---

## 🎙️ CHAPTER 5: 45-MINUTE INTERVIEW VERBAL SCRIPT

### Phase 1: Clarification & Guard Clauses (0–5 Mins)
- **Candidate:** "Let's confirm the problem constraints: Does the string contain only bracket characters `()[]{}` or could arbitrary letters and whitespaces appear? If other characters are present, should they be ignored or treated as invalid?"
- **Interviewer:** "Only bracket characters `()[]{}`."
- **Candidate:** "Great. An immediate observation is that if `s.Length` is odd, it's mathematically impossible for all brackets to be paired. We can return `false` in `O(1)` time before allocating our stack."

### Phase 2: Why Counters Fail & Data Structure Selection (5–12 Mins)
- **Candidate:** "If we only had parentheses `()`, we could use a single integer balance counter, incrementing on `(` and decrementing on `)`. If the counter drops below 0, it's invalid. That would take `O(1)` space.
However, with multiple bracket types (`()`, `{}`, `[]`), counters fail because they cannot track interleaving order. For instance, in `"([)]"`, the individual counts for each bracket are balanced, but the nesting violates syntactic rules because `[` was opened last and must be closed first. Therefore, a LIFO stack is strictly required to enforce that closing brackets match the most recent unmatched opening bracket."

### Phase 3: Live Implementation & Sentinel Mechanics (12–32 Mins)
- **Candidate (for Longest Valid):** "To find the longest valid parentheses substring in `O(N)` time, I will store indices on the stack. I initialize the stack with a sentinel value `-1`.
When we see an open bracket, we push its index. When we see a close bracket, we pop the top index.
If the stack remains non-empty, the distance from our current index `i` to the new top of the stack `stack.Peek()` represents the length of the valid substring ending at `i`.
If the stack becomes empty, it means this closing bracket had no matching open bracket; we push `i` as the new boundary anchor."

### Phase 4: Edge Cases & Verification (32–40 Mins)
- **Candidate:** "Let's dry-run edge cases:
  1. Empty string `""`: Handled correctly (returns true for validity, 0 for longest).
  2. Single closing bracket `")"`: Pops `-1`, stack empty, pushes `0`. Max remains 0.
  3. Single opening bracket `"("`: Pushes 0. Loop ends. Stack not empty, validity false, max remains 0.
  4. Disconnected pairs `"()()"`: Correctly bridges lengths across indices to yield 4."

---

## 🛠️ CHAPTER 6: COMMON PITFALLS & DECISION FRAMEWORK

### Common Pitfalls
1. **Empty Stack Exception:** Attempting `stack.Pop()` on an empty stack when encountering an extra closing bracket. Always check `stack.Count > 0` first.
2. **Forgetting Final Stack Check:** Checking all closing brackets match, but forgetting to verify `stack.Count == 0` at the end (failing on `"((("`).
3. **Inefficient String Concatenation in Backtracking:** Doing `current + "("` creates a new string at each recursive frame; use a mutable buffer (`StringBuilder` in C# or `list` in Python) with push/pop backtrack discipline.

### Decision Framework

```
                          [ Parentheses Problem ]
                                     |
               +---------------------+---------------------+
               |                                           |
       [ Validation / Matching ]                  [ Generation / Length ]
               |                                           |
      How many bracket types?                     Problem Objective?
               |                                           |
        +------+------+                             +------+------+
        |             |                             |             |
      Single        Multiple                  Find Longest     Generate All
      Counter       Stack<char>               Index Stack      Backtracking
      (O(1) space)  (O(N) space)              with -1 base     with pruning
```


---

## 🏛️ CHAPTER 7: MULTI-STACK NESTED PARSING & OPERATOR PRECEDENCE

> [!IMPORTANT]
> **Senior Interview Reality:**
> Simple bracket validation (`LC 20`) tests mid-level syntax mechanics. Senior and Staff interviews at Meta, Google, and Amazon evaluate **Nested State Persistence and Evaluation Stacks** via **Decode String (LC 394)** and **Basic Calculator II (LC 227)**.
> These problems require managing multi-level context: remembering unfinished outer strings while resolving nested inner expressions, and respecting operator precedence without full context-free grammar parsers.

### 🧠 Pattern 1: Nested Scope Stacking — Decode String (LeetCode 394)

#### The Mental Model
When parsing nested expressions like `3[a2[c]]`, each `[` opens a new execution scope, and each `]` closes it. Because inner scopes must resolve *before* the outer scope can be multiplied, this follows strict **LIFO (Last-In, First-Out)** order.

```text
Parsing String: "3[a2[c]]"
Pointer trace:
1. Read '3', '[': Push count 3, push outer string "" onto stacks. Reset current = "".
2. Read 'a': current = "a".
3. Read '2', '[': Push count 2, push outer string "a" onto stacks. Reset current = "".
4. Read 'c': current = "c".
5. Read ']': Pop count 2, pop previous "a".
   New current = "a" + "c" * 2 = "acc".
6. Read ']': Pop count 3, pop previous "".
   New current = "" + "acc" * 3 = "accaccacc".
```

#### Dual-Language Implementation (LC 394)

##### C# (.NET 8/9 Modern Implementation)

```csharp
using System;
using System.Collections.Generic;
using System.Text;

public static class StringDecoder
{
    /// <summary>
    /// Decodes nested bracket strings using dual stacks for multipliers and string prefixes.
    /// Time Complexity: O(MaxK * N) where MaxK is repeat factor and N is length.
    /// Auxiliary Space: O(N) for recursion/stack memory.
    /// </summary>
    public static string DecodeString(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }

        Stack<int> countStack = new();
        Stack<StringBuilder> stringStack = new();
        StringBuilder currentStr = new();
        int currentCount = 0;

        foreach (char c in s)
        {
            if (char.IsDigit(c))
            {
                // Multi-digit multiplier accumulator
                currentCount = currentCount * 10 + (c - '0');
            }
            else if (c == '[')
            {
                // Push outer context into stacks and reset local scope
                countStack.Push(currentCount);
                stringStack.Push(currentStr);
                currentStr = new StringBuilder();
                currentCount = 0;
            }
            else if (c == ']')
            {
                // Pop scope and repeat current string
                int repeatTimes = countStack.Pop();
                StringBuilder decodedPrefix = stringStack.Pop();

                for (int i = 0; i < repeatTimes; i++)
                {
                    decodedPrefix.Append(currentStr);
                }

                currentStr = decodedPrefix;
            }
            else
            {
                // Regular character: append to current working buffer
                currentStr.Append(c);
            }
        }

        return currentStr.ToString();
    }
}
```

##### Python 3.11+ Idiomatic Implementation

```python
class Solution:
    def decodeString(self, s: str) -> str:
        """
        Decodes nested encoded strings using a single tuple stack: (previous_string, repeat_count).
        Time Complexity: O(Output Length)
        Auxiliary Space: O(N)
        """
        stack: list[tuple[str, int]] = []
        current_str = []
        current_num = 0

        for char in s:
            if char.isdigit():
                current_num = current_num * 10 + int(char)
            elif char == '[':
                # Freeze current state and push into stack
                stack.append(("".join(current_str), current_num))
                current_str = []
                current_num = 0
            elif char == ']':
                prev_str, repeat_times = stack.pop()
                current_str = [prev_str + ("".join(current_str) * repeat_times)]
            else:
                current_str.append(char)

        return "".join(current_str)
```

---

### 🧠 Pattern 2: Operator Precedence Stack — Basic Calculator II (LeetCode 227)

#### The Mental Model
Evaluating arithmetic strings containing `+`, `-`, `*`, `/` with proper operator precedence without recursion:
* `*` and `/` have **high precedence**: resolve them immediately by popping the top number from the stack, evaluating, and pushing the result back.
* `+` and `-` have **low precedence**: defer them by pushing signed numbers onto the stack (`+X` pushes `X`, `-X` pushes `-X`).
* At the end, sum all elements in the stack.

```text
Expression: "3 + 2 * 2"
Pointer trace:
- Initial sign = '+'
- Read '3': sign was '+', push +3. Stack: [3]. Next sign = '+'.
- Read '2': sign was '+', push +2. Stack: [3, 2]. Next sign = '*'.
- Read '2': sign was '*', pop 2, compute 2 * 2 = 4, push +4. Stack: [3, 4].
- End of string: sum stack elements = 3 + 4 = 7.
```

#### Dual-Language Implementation (LC 227)

##### C# (.NET 8/9 Zero-Recursion Implementation)

```csharp
using System;
using System.Collections.Generic;

public static class BasicCalculator
{
    /// <summary>
    /// Evaluates arithmetic string with +, -, *, / respecting precedence.
    /// Time Complexity: O(N)
    /// Auxiliary Space: O(N) for expression stack
    /// </summary>
    public static int Calculate(string s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return 0;
        }

        Stack<int> stack = new();
        int currentNumber = 0;
        char operation = '+';
        int length = s.Length;

        for (int i = 0; i < length; i++)
        {
            char c = s[i];

            if (char.IsDigit(c))
            {
                currentNumber = currentNumber * 10 + (c - '0');
            }

            // If character is an operator, or we reached the last character
            if ((!char.IsDigit(c) && c != ' ') || i == length - 1)
            {
                if (operation == '+')
                {
                    stack.Push(currentNumber);
                }
                else if (operation == '-')
                {
                    stack.Push(-currentNumber);
                }
                else if (operation == '*')
                {
                    stack.Push(stack.Pop() * currentNumber);
                }
                else if (operation == '/')
                {
                    stack.Push(stack.Pop() / currentNumber); // Truncates toward zero in C#
                }

                operation = c;
                currentNumber = 0;
            }
        }

        int result = 0;
        while (stack.Count > 0)
        {
            result += stack.Pop();
        }

        return result;
    }
}
```

##### Python 3.11+ Idiomatic Implementation

```python
class Solution:
    def calculate(self, s: str) -> int:
        """
        Evaluates +, -, *, / expressions using an operator precedence stack.
        Time Complexity: O(N)
        Auxiliary Space: O(N)
        """
        stack: list[int] = []
        current_num = 0
        operator = '+'
        s = s.strip()

        for i, char in enumerate(s):
            if char.isdigit():
                current_num = current_num * 10 + int(char)

            if (not char.isdigit() and char != ' ') or i == len(s) - 1:
                if operator == '+':
                    stack.append(current_num)
                elif operator == '-':
                    stack.append(-current_num)
                elif operator == '*':
                    stack.append(stack.pop() * current_num)
                elif operator == '/':
                    # In Python, int division with negative numbers truncates away from 0:
                    # -3 // 2 = -2. Use int(prev / current_num) to truncate towards zero.
                    stack.append(int(stack.pop() / current_num))

                operator = char
                current_num = 0

        return sum(stack)
```

---

### 🎙️ 45-Minute Senior Interview Verbal Script

```text
+-----------------------+------------------------------------------------------------------------------------------+
| INTERVIEW STEP        | SPOKEN SCRIPT (WHAT YOU SAY ALOUD TO THE INTERVIEWER)                                    |
+-----------------------+------------------------------------------------------------------------------------------+
| 1. Spot Bottleneck    | "In Basic Calculator, we cannot evaluate operators left-to-right naively because '*'     |
|                       | and '/' have higher precedence than '+' and '-'.                                         |
+-----------------------+------------------------------------------------------------------------------------------+
| 2. State Invariant    | "We maintain an evaluation stack where all high-precedence operations (*, /) are         |
|                       | collapsed immediately, and low-precedence operations (+, -) are deferred as signed      |
|                       | numbers. At the end of the scan, the total sum of the stack is the final expression value."|
+-----------------------+------------------------------------------------------------------------------------------+
| 3. Language Trap Alert| "In Python, integer division -3 // 2 rounds to -2 instead of -1. We must use             |
|                       | int(a / b) to ensure truncation toward zero matches language-neutral arithmetic specs."  |
+-----------------------+------------------------------------------------------------------------------------------+
```

---

## 🏋️ PRACTICE LADDER

| Problem | LeetCode # | Difficulty | Key Pattern | Stack Stored Element |
| :--- | :--- | :--- | :--- | :--- |
| **Valid Parentheses** | #20 | 🟢 Easy | Stack Matching | Characters (`char`) |
| **Longest Valid Parentheses** | #32 | 🔴 Hard | Sentinel Index Stack | Indices (`int`) |
| **Generate Parentheses** | #22 | 🟡 Medium | Catalan Backtracking | Recursive Call Stack |
| **Minimum Remove to Make Valid Parentheses** | #1249 | 🟡 Medium | Two-Pass Index Stack | Invalid Indices |
| **Score of Parentheses** | #856 | 🟡 Medium | Stack Depth Accumulation | Numerical Sub-scores |
| **Minimum Add to Make Parentheses Valid** | #921 | 🟡 Medium | Balance Counters | Balance state |

---

> 🧭 **Navigation:** [← Previous Day](Week_06_Day_02_Substring_Sliding_Window_Patterns_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_06_Day_04_String_Transformations_Building_Instructional.md)

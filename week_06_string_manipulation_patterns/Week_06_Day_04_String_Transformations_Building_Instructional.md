# 📘 Week 06 Day 4: String Transformations & Building — Engineering Guide

> 🧭 **Navigation:** [← Previous Day](Week_06_Day_03_Parentheses_Bracket_Matching_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_06_Day_05_Advanced_String_Matching_Rabin_Karp_Rolling_Hash_Instructional.md)
> 
> 💡 **Instructor Note:** *In modern managed languages (C#, Python, Java), strings are immutable heap objects. Understanding the memory reality of string allocation, string builders, and numerical parsing state machines prevents quadratic runtime and edge-case overflow bugs.*

---

## 🎯 LEARNING OBJECTIVES

By the end of this chapter, you will be able to:

- **Internalize** the mechanical impact of string immutability and why repeated concatenation in loops creates `O(N^2)` garbage collection pressure.
- **Implement** bulletproof state machine parsers (`my_atoi`) with proactive 32-bit integer overflow detection *before* arithmetic execution.
- **Master** dual representation transformations: greedy value-mapping (`IntToRoman`) and lookahead reduction (`RomanToInt`).
- **Execute** in-place two-pointer string mutations (Run-Length Encoding / Array Compression) in `O(1)` auxiliary space.
- **Deliver** a structured 45-minute technical interview script defending buffer capacity allocation and boundary protection.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Engineering Challenge

String transformation and parsing sits at the boundary between raw network protocols and typed runtime systems:
1. **API Ingestion & Microservices:** Gateways convert raw untrusted HTTP query parameters and headers into typed integers and domain symbols.
2. **High-Throughput Serialization:** Encoders (JSON serializers, Protobuf text bridges, log formatters) emit millions of structured strings per second.
3. **Data Compression & Archival:** Lightweight run-length encoders compress repetitive payloads prior to disk or network transmission.

A common developer mistake is string concatenation inside loops: `s += c`. Because strings are immutable, each concatenation allocates a new buffer of length `i` and copies all existing characters. Summing `1 + 2 + ... + N` creates `O(N^2)` memory churn and catastrophic GC pauses.

> [!NOTE]
> **Interview & Systems Context:** In systems programming and technical interviews, never use 64-bit integer (`long` / `int64`) casting as a lazy crutch to solve 32-bit overflow in `atoi`. Interviewers at top-tier firms explicitly test whether you can detect 32-bit boundary violations mathematically before the multiplication step using `int.MaxValue / 10`.

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### Memory Reality: String Immutability vs. Mutable Builders

```
Naive Concatenation (s += c):
  Step 1: Allocate "a"          (Length 1)
  Step 2: Allocate "ab"         (Length 2, copies "a")
  Step 3: Allocate "abc"        (Length 3, copies "ab")
  Step 4: Allocate "abcd"       (Length 4, copies "abc")
  Total Allocations: N strings | Total Copies: N*(N+1)/2 = O(N^2) bytes copied!

StringBuilder / Dynamic Array (Pre-allocated Buffer):
  Internal Buffer: [ a | b | c | d | _ | _ | _ | _ ]
  Capacity: 8, Length: 4
  Appends occur in O(1) amortized time by writing directly to contiguous heap memory.
```

### State Machine Architecture: String to Integer (`my_atoi`)

```
   [ State 0: Leading Whitespace ]
            | (Encounter non-space)
            v
   [ State 1: Optional Sign (+/-) ]
            | (Encounter digit)
            v
   [ State 2: Digit Accumulation ] <----+
            |                           | (More digits)
            | Check: result > MAX/10    +
            | Accumulate: result * 10 + d
            v (Encounter non-digit / EOS)
   [ State 3: Terminal Return ]
            -> return sign * result
```

### Roman Numeral Representations: Greedy vs. Lookahead

```
Greedy Value Partition (IntToRoman):
  Values:  [ 1000, 900, 500, 400, 100,  90,  50,  40,  10,   9,   5,   4,   1 ]
  Symbols: [  "M", "CM","D", "CD","C", "XC","L", "XL","X", "IX", "V", "IV", "I" ]
  Rule: Iterate descending. Subtract largest matching value repeatedly.

Lookahead Scan (RomanToInt):
  Symbol:   M     C     M     X     C     I     V
  Values: 1000   100  1000   10   100     1     5
  Rule: If currentValue < nextValue, subtract it (e.g., C < M -> -100).
        Otherwise, add it (e.g., M -> +1000).
```

### In-Place Array Compression: Read and Write Heads

```
Input: ['a', 'a', 'a', 'b', 'b', 'c']
Pointers: Write (W), Read (R), Anchor (A)

Step 1: Count run of 'a': length 3.
        Chars[W++] = 'a'; Chars[W++] = '3'
        Array: [ 'a', '3', | 'a', 'b', 'b', 'c' ]
                           W
Step 2: Count run of 'b': length 2.
        Chars[W++] = 'b'; Chars[W++] = '2'
        Array: [ 'a', '3', 'b', '2', | 'b', 'c' ]
                                     W
Step 3: Count run of 'c': length 1.
        Chars[W++] = 'c' (no count for length 1)
        Array: [ 'a', '3', 'b', '2', 'c', | 'c' ]
                                          W (Return W = 5)
```

---

## ⚙️ CHAPTER 3: MECHANICS & IMPLEMENTATION

### Operation 1: String to Integer (LeetCode 8)

1. Skip leading whitespace: Advance index while `s[i] == ' '`.
2. Check sign: If `s[i] == '-'` set `sign = -1`, if `'+'` set `sign = 1`. Advance index.
3. Process digits:
   - Calculate digit: `d = s[i] - '0'`.
   - Prevent 32-bit overflow *before* multiplying:
     `if (result > INT_MAX / 10 || (result == INT_MAX / 10 && d > 7))`
     Return `sign == 1 ? INT_MAX : INT_MIN`.
   - Update: `result = result * 10 + d`.
4. Return `sign * result`.

### Operation 2: In-Place String Compression (LeetCode 443)

1. Maintain `write = 0`, `read = 0`.
2. For each run:
   - Identify character `c = chars[read]` and find length of continuous run.
   - Write character: `chars[write++] = c`.
   - If `count > 1`, convert count to string and write each digit to `chars[write++]`.
3. Return `write` as the new compressed array length.

---

### 💻 Production-Grade Implementations

#### C# (.NET 8/9 — Zero Allocation & Memory Efficiency)

```csharp
using System;
using System.Text;

public static class StringTransformationSolutions
{
    /// <summary>
    /// Converts a string to a 32-bit signed integer with robust overflow handling.
    /// Time Complexity: O(N) | Auxiliary Space: O(1)
    /// </summary>
    public static int MyAtoi(ReadOnlySpan<char> s)
    {
        if (s.IsEmpty) return 0;

        int i = 0;
        // Step 1: Skip leading whitespace
        while (i < s.Length && s[i] == ' ')
        {
            i++;
        }

        if (i >= s.Length) return 0;

        // Step 2: Handle sign
        int sign = 1;
        if (s[i] == '+' || s[i] == '-')
        {
            sign = (s[i] == '-') ? -1 : 1;
            i++;
        }

        // Step 3: Accumulate digits with overflow guard
        int result = 0;
        const int maxThreshold = int.MaxValue / 10;

        while (i < s.Length && char.IsDigit(s[i]))
        {
            int digit = s[i] - '0';

            // Guard overflow BEFORE calculation
            // int.MaxValue is 2147483647 (ends in 7), int.MinValue is -2147483648 (ends in 8)
            if (result > maxThreshold || (result == maxThreshold && digit > 7))
            {
                return sign == 1 ? int.MaxValue : int.MinValue;
            }

            result = result * 10 + digit;
            i++;
        }

        return sign * result;
    }

    /// <summary>
    /// Converts an integer to a Roman numeral using greedy value mapping.
    /// Time Complexity: O(1) | Auxiliary Space: O(1)
    /// </summary>
    public static string IntToRoman(int num)
    {
        if (num <= 0 || num > 3999) return string.Empty;

        ReadOnlySpan<int> values = [1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1];
        string[] symbols = ["M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I"];

        StringBuilder sb = new(16);

        for (int i = 0; i < values.Length; i++)
        {
            while (num >= values[i])
            {
                sb.Append(symbols[i]);
                num -= values[i];
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// In-place run-length string compression (LeetCode 443).
    /// Time Complexity: O(N) | Auxiliary Space: O(1)
    /// </summary>
    public static int Compress(char[] chars)
    {
        if (chars == null || chars.Length == 0) return 0;

        int write = 0;
        int read = 0;

        while (read < chars.Length)
        {
            char current = chars[read];
            int count = 0;

            // Count contiguous run
            while (read < chars.Length && chars[read] == current)
            {
                read++;
                count++;
            }

            // Write character
            chars[write++] = current;

            // Write count digits if greater than 1
            if (count > 1)
            {
                foreach (char digit in count.ToString())
                {
                    chars[write++] = digit;
                }
            }
        }

        return write;
    }
}
```

#### Python (3.11+ — Idiomatic & Pythonic)

```python
class StringTransformationSolutions:
    @staticmethod
    def my_atoi(s: str) -> int:
        """
        Parses string to a 32-bit signed integer with clamping.
        Time: O(N) | Auxiliary Space: O(1)
        """
        s = s.lstrip()
        if not s:
            return 0

        sign = 1
        idx = 0

        if s[0] in ("-", "+"):
            sign = -1 if s[0] == "-" else 1
            idx = 1

        result = 0
        int_max = 2**31 - 1
        int_min = -(2**31)

        while idx < len(s) and s[idx].isdigit():
            digit = ord(s[idx]) - ord("0")

            # Check overflow before accumulating
            if result > int_max // 10 or (
                result == int_max // 10 and digit > 7
            ):
                return int_max if sign == 1 else int_min

            result = result * 10 + digit
            idx += 1

        total = sign * result
        return max(int_min, min(int_max, total))

    @staticmethod
    def int_to_roman(num: int) -> str:
        """
        Converts integer to Roman numeral using greedy value matching.
        Time: O(1) | Auxiliary Space: O(1)
        """
        mapping: list[tuple[int, str]] = [
            (1000, "M"),
            (900, "CM"),
            (500, "D"),
            (400, "CD"),
            (100, "C"),
            (90, "XC"),
            (50, "L"),
            (40, "XL"),
            (10, "X"),
            (9, "IX"),
            (5, "V"),
            (4, "IV"),
            (1, "I"),
        ]

        out: list[str] = []
        for val, sym in mapping:
            while num >= val:
                out.append(sym)
                num -= val

        return "".join(out)

    @staticmethod
    def compress(chars: list[str]) -> int:
        """
        In-place run-length encoding on a character list.
        Time: O(N) | Auxiliary Space: O(1)
        """
        write = 0
        read = 0

        while read < len(chars):
            current_char = chars[read]
            count = 0

            while read < len(chars) and chars[read] == current_char:
                read += 1
                count += 1

            chars[write] = current_char
            write += 1

            if count > 1:
                for digit in str(count):
                    chars[write] = digit
                    write += 1

        return write
```

---

## ⚖️ CHAPTER 4: COMPLEXITY DECONSTRUCTION

### Explicit Complexity Breakdown

| Algorithm / Operation | Time (Best Case) | Time (Worst Case) | Auxiliary Space | Output Space | Key Optimization |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **String to Integer (atoi)** | `O(1)` (non-digit prefix) | `O(N)` (valid digits) | `O(1)` | `O(1)` | Clamping prior to `result * 10` |
| **Integer to Roman** | `O(1)` (fixed <= 13 steps)| `O(1)` | `O(1)` | `O(1)` (max 15 chars) | Pre-sorted greedy denomination array |
| **Roman to Integer** | `O(N)` | `O(N)` (max 15 chars) | `O(1)` | `O(1)` | Lookahead subtractive reduction |
| **In-Place Compression** | `O(N)` | `O(N)` | `O(1)` | `O(1)` (in-place) | Dual-pointer read/write heads |
| **Loop `+=` Concatenation** | `O(N)` | `O(N^2)` | `O(N^2)` | `O(N)` | Antipattern: reallocates on every character |

---

## 🎙️ CHAPTER 5: 45-MINUTE INTERVIEW VERBAL SCRIPT

### Phase 1: Clarification & Boundary Invariants (0–5 Mins)
- **Candidate:** "Let's confirm the transformation parameters: For `atoi`, what should happen on overflow? Should it wrap around or clamp to `[INT_MIN, INT_MAX]`? Also, how should leading/trailing garbage be handled?"
- **Interviewer:** "Clamp to 32-bit signed integer limits. Ignore leading whitespace, ignore trailing characters after the valid digit sequence."
- **Candidate:** "Understood. That defines a classic 4-state lexical analyzer: skip whitespace, parse sign, parse digits with proactive overflow guarding, and terminate upon first non-digit."

### Phase 2: Why String Builders & Guarding Arithmetic (5–12 Mins)
- **Candidate:** "In C# and Python, strings are immutable. Any problem involving building or transforming strings dynamically should use a mutable buffer (`StringBuilder` or list) to guarantee `O(N)` linear runtime instead of `O(N^2)`.
For numeric accumulation, a critical mistake is computing `result * 10 + digit` using a 64-bit integer and casting at the end. That hides the math and fails in languages without 64-bit types. Instead, I will detect overflow *before* multiplication using `result > int.MaxValue / 10` or `result == int.MaxValue / 10 && digit > 7`."

### Phase 3: Live Implementation Walkthrough (12–32 Mins)
- **Candidate:** "Let's code `MyAtoi`. Notice how clean the loop is. We initialize `result = 0` and `sign = 1`. If we detect that the next multiplication would exceed `2147483647`, we immediately return the clamped boundary. This requires zero memory allocation."

### Phase 4: Edge Cases & Verification (32–40 Mins)
- **Candidate:** "Let's trace tricky edge cases:
  1. Whitespace only `"   "`: Correctly advances to end and returns 0.
  2. Just a sign `"+"` or `"-"`: Advances sign, loop never runs, returns 0.
  3. Overflow payload `"2147483648"`: Reaches threshold check at digit 8, clamps to `INT_MAX`.
  4. Negative overflow `"-2147483649"`: Clamps to `INT_MIN`.
  5. Words before numbers `"words and 987"`: First char non-space non-digit, returns 0."

---

## 🛠️ CHAPTER 6: COMMON PITFALLS & DECISION FRAMEWORK

### Common Pitfalls
1. **Multiplication Overflow:** Calculating `result = result * 10 + digit` *before* checking bounds. In 32-bit arithmetic, this wraps silently to negative values.
2. **Character Digit Arithmetic:** Forgetting `- '0'` and accidentally using the ASCII code of the digit (e.g., `'0'` is ASCII 48).
3. **Repeated String Concatenation:** Using `string += str` inside a loop instead of `StringBuilder.Append()`.

### Decision Framework

```
                     [ String Transformation Task ]
                                   |
         +-------------------------+-------------------------+
         |                                                   |
   [ Numeric Parsing ]                              [ Text Formatting ]
         |                                                   |
   State Machine                                   Memory Discipline?
   - Skip whitespace                                         |
   - Extract sign                                   +--------+--------+
   - Pre-check MAX/10                               |                 |
   - Accumulate digits                        In-Place Buffer     Dynamic Builder
                                              Two-Pointers        StringBuilder
                                              (Compress)          (IntToRoman)
```

---

## 🏋️ PRACTICE LADDER

| Problem | LeetCode # | Difficulty | Key Pattern | Primary Focus |
| :--- | :--- | :--- | :--- | :--- |
| **String to Integer (atoi)** | #8 | 🟡 Medium | State Machine | 32-bit arithmetic boundary check |
| **Integer to Roman** | #12 | 🟡 Medium | Greedy Mapping | Descending value partition |
| **Roman to Integer** | #13 | 🟢 Easy | Lookahead Scan | Subtractive symbol reduction |
| **String Compression** | #443 | 🟡 Medium | In-Place Two Pointers | Zero-allocation run length write |
| **Zigzag Conversion** | #6 | 🟡 Medium | Buffer Simulation | Multi-row `StringBuilder` array |
| **Reverse Words in a String** | #151 | 🟡 Medium | In-Place Two Pointers | Word reversal + space compaction |

---

> 🧭 **Navigation:** [← Previous Day](Week_06_Day_03_Parentheses_Bracket_Matching_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_06_Day_05_Advanced_String_Matching_Rabin_Karp_Rolling_Hash_Instructional.md)

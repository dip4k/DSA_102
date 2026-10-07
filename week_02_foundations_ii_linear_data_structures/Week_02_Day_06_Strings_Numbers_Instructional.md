# 📘 Week 02 Day 06: Strings & Numbers — Representation & Conversions — ENGINEERING GUIDE

> 🧭 **Navigation:** [← Previous Day](Week_02_Day_05_Binary_Search_Invariants_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Week Playbook →](WEEK_02_FULL_PLAYBOOK.md)
> 
> 💡 **Instructor Note:** *Not all sections or topics are mandatory. Feel free to adapt your pace and prioritize high-yield patterns based on your personal interview goals.*

---

## 🎯 LEARNING OBJECTIVES

*By the end of this chapter, you will be able to:*

- 🎯 **Internalize** physical memory representations of strings (contiguous immutable buffers, object headers) and numbers (Two's complement binary bits).
- ⚙️ **Implement** robust `atoi` (string to integer) and `itoa` (integer to string) parsing engines with strict overflow detection and clamping.
- ⚖️ **Prove** why string concatenation inside loops causes quadratic `O(N^2)` heap allocation churn and how `StringBuilder` resolves it in `O(N)`.
- 🛡️ **Master** character arithmetic (`ch - 'a'`) to build `O(1)` space hash buckets for frequency tables.
- 🏭 **Articulate** production system impacts (VSCode piece tables, database collation indexes, integer wrap vulnerabilities) in a 45-minute technical screen.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Problem: When Abstractions Hide Costly Hardware Realities

In high-level languages, strings and numbers appear as simple primitive values. However, treating them as abstract mathematical constructs leads to severe production defects:

1. **Quadratic String Allocations:**
   ```csharp
   string result = "";
   for (int i = 0; i < 100_000; i++) {
       result += "a"; // Creates 100,000 heap objects and copies ~5 GB of memory!
   }
   ```
   Because strings are immutable, each `+=` allocates a new string object and copies all previous characters, degrading an apparently simple loop into an `O(N^2)` throughput collapse that triggers massive Garbage Collection pauses.

2. **Silent Integer Overflow:**
   In 32-bit signed arithmetic, `2,147,483,647 + 1 = -2,147,483,648`. Without defensive boundary checks, numerical overflows cause silent financial ledger inversions, buffer calculation bugs, and catastrophic security vulnerabilities.

> 💡 **Core Invariant:** *Physical representations dictate performance and safety. Strings are immutable contiguous arrays with metadata headers; signed integers are fixed-width bit patterns governed by Two's complement arithmetic.*

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### The Sealed Museum Display Analogy

Think of a string as a sealed glass display case containing a fixed row of antique coins (characters):
- You can inspect coin #3 instantly through the glass in `O(1)` time.
- You can never insert a coin into the existing case. To add one more coin, you must construct an entirely new, larger glass case, physically transfer every existing coin one by one, place the new coin at the end, and discard the old case.

Doing this in a loop means constantly manufacturing and discarding glass cases. A `StringBuilder`, by contrast, is an open adjustable tray with pre-allocated extra slots that expands geometrically.

---

### 🖼 Visualizing String Memory Layout in the Managed Heap

```text
Stack Frame               Managed Heap (64-bit CLR / JVM Layout)
+----------------+        +-------------------------------------------------------+
| strRef (8B)   |───────>| MethodTable Pointer (8 bytes)                         |
+----------------+        | SyncBlock Index      (8 bytes)                         |
                          | Array Length: 5      (4 bytes)                         |
                          | Cached Hash/Padding  (4 bytes)                         |
                          +-------------------------------------------------------+
                          | Contiguous Char Buffer (UTF-16 in C#, 2 bytes/char):  |
                          | ['H', 'e', 'l', 'l', 'o', '\0'] (12 bytes)            |
                          +-------------------------------------------------------+
                          Total Memory Footprint: 8 + 8 + 4 + 4 + 12 = 36 bytes
```

---

### 🖼 Visualizing Quadratic String Concatenation vs. StringBuilder

```text
Repeated Concatenation: s += "X" (N iterations)
Iteration 1: [ A ][ X ]             -> Allocates 2 chars, copies 1
Iteration 2: [ A ][ X ][ X ]        -> Allocates 3 chars, copies 2
Iteration 3: [ A ][ X ][ X ][ X ]   -> Allocates 4 chars, copies 3
...
Total Copies = 1 + 2 + 3 + ... + N = O(N^2) allocations + massive GC churn!

StringBuilder (Dynamic Backing Array with Doubling):
Allocates Capacity 16: [ A ][ X ][ X ][ X ][ _ ][ _ ] ... [ _ ]
Appends occur in O(1) direct memory writes; resizes occur only O(log N) times.
Total Copies = O(N) linear time!
```

---

### 🖼 Two's Complement Integer Representation & Overflow Boundary

32-bit signed integers represent numbers using Two's Complement. The highest bit (Bit 31) serves as the sign bit (`0 = positive`, `1 = negative`):

```text
Bit 31                                                            Bit 0
  │                                                                 │
  ▼                                                                 ▼
[ 0 ][ 1 ][ 1 ][ 1 ] ... [ 1 ][ 1 ][ 1 ][ 1 ] = +2,147,483,647 (int.MaxValue)
  │
  └── Adding 1 triggers carry wrap:
  ▼
[ 1 ][ 0 ][ 0 ][ 0 ] ... [ 0 ][ 0 ][ 0 ][ 0 ] = -2,147,483,648 (int.MinValue)
```

#### The Asymmetry Trap:
Notice that `|int.MinValue| = 2,147,483,648`, which is **1 greater than `int.MaxValue`**.
Negating `int.MinValue` (`-int.MinValue`) in 32-bit signed integer space cannot produce `+2,147,483,648`; it overflows and returns `-2,147,483,648`!

---

## ⚙️ CHAPTER 3: DUAL-LANGUAGE PRODUCTION IMPLEMENTATIONS

### 1. C# (.NET 8/9): Production `MyAtoi` and `MyItoa` with Defensive Clamping

```csharp
using System;
using System.Text;

namespace LinearStructures.Day06;

public static class StringNumberConversions
{
    // 1. Production atoi: String to 32-bit Signed Integer (LeetCode 8)
    public static int MyAtoi(string? s)
    {
        if (string.IsNullOrEmpty(s)) return 0;

        ReadOnlySpan<char> span = s.AsSpan().TrimStart();
        if (span.IsEmpty) return 0;

        int sign = 1;
        int index = 0;

        // Parse optional sign
        if (span[0] == '-')
        {
            sign = -1;
            index++;
        }
        else if (span[0] == '+')
        {
            index++;
        }

        int result = 0;
        const int maxThreshold = int.MaxValue / 10;
        const int maxLastDigit = int.MaxValue % 10; // 7

        while (index < span.Length && char.IsAsciiDigit(span[index]))
        {
            int digit = span[index] - '0';

            // Check overflow before multiplying by 10
            if (result > maxThreshold || (result == maxThreshold && digit > maxLastDigit))
            {
                return sign == 1 ? int.MaxValue : int.MinValue;
            }

            result = (result * 10) + digit;
            index++;
        }

        return result * sign;
    }

    // 2. Production itoa: 32-bit Integer to String (handles int.MinValue cleanly)
    public static string MyItoa(int value)
    {
        if (value == 0) return "0";
        if (value == int.MinValue) return "-2147483648"; // Asymmetry guard

        bool isNegative = value < 0;
        int num = Math.Abs(value);

        StringBuilder sb = new();
        while (num > 0)
        {
            int digit = num % 10;
            sb.Append((char)('0' + digit));
            num /= 10;
        }

        if (isNegative)
        {
            sb.Append('-');
        }

        // Reverse characters in place
        for (int i = 0, j = sb.Length - 1; i < j; i++, j--)
        {
            (sb[i], sb[j]) = (sb[j], sb[i]);
        }

        return sb.ToString();
    }

    // 3. O(1) Space Character Frequency Map
    public static int[] ComputeFrequency(string s)
    {
        int[] freq = new int[26];
        foreach (char c in s)
        {
            if (c is >= 'a' and <= 'z')
            {
                freq[c - 'a']++;
            }
        }
        return freq;
    }
}
```

---

### 2. Python (3.11+): Idiomatic String & Number Conversions

```python
class StringNumberConversions:
    INT_MAX: int = 2**31 - 1
    INT_MIN: int = -2**31

    @classmethod
    def my_atoi(cls, s: str) -> int:
        """Parses string into 32-bit signed integer with strict boundary clamping."""
        s = s.lstrip()
        if not s:
            return 0

        sign: int = 1
        index: int = 0

        if s[0] == '-':
            sign = -1
            index += 1
        elif s[0] == '+':
            index += 1

        result: int = 0
        max_div_10: int = cls.INT_MAX // 10
        max_rem: int = cls.INT_MAX % 10  # 7

        while index < len(s) and s[index].isdigit():
            digit: int = ord(s[index]) - ord('0')

            # Defensive overflow detection prior to multiplication
            if result > max_div_10 or (result == max_div_10 and digit > max_rem):
                return cls.INT_MAX if sign == 1 else cls.INT_MIN

            result = (result * 10) + digit
            index += 1

        return sign * result

    @classmethod
    def my_itoa(cls, value: int) -> str:
        """Converts integer to string without relying on built-in str()."""
        if value == 0:
            return "0"
        if value == cls.INT_MIN:
            return "-2147483648"

        is_negative: bool = value < 0
        num: int = abs(value)
        chars: list[str] = []

        while num > 0:
            digit: int = num % 10
            chars.append(chr(ord('0') + digit))
            num //= 10

        if is_negative:
            chars.append('-')

        chars.reverse()
        return "".join(chars)

    @staticmethod
    def compute_frequency(s: str) -> list[int]:
        """Calculates lowercase ASCII frequencies using an O(1) space array."""
        freq: list[int] = [0] * 26
        for ch in s:
            if 'a' <= ch <= 'z':
                freq[ord(ch) - ord('a')] += 1
        return freq

if __name__ == "__main__":
    print(f"Atoi '  -42': {StringNumberConversions.my_atoi('  -42')}")
    print(f"Atoi Overflow '2147483648': {StringNumberConversions.my_atoi('2147483648')}")
    print(f"Itoa -12345: {StringNumberConversions.my_itoa(-12345)}")
    print(f"Freq 'leetcode': {StringNumberConversions.compute_frequency('leetcode')}")
```

---

## 🔬 COMPLEXITY DECONSTRUCTION

| Operation | Time Complexity | Auxiliary Space | Output Space | Mechanical Justification |
| :--- | :--- | :--- | :--- | :--- |
| **`MyAtoi`** | `O(N)` | `O(1)` | `O(1)` | Scans string of length `N` once; accumulator lives in CPU registers. |
| **`MyItoa`** | `O(D)` | `O(D)` | `O(D)` | `D` is number of digits (`<= 10` for 32-bit integers). Fixed small buffer reversed in place. |
| **`+=` String Concat in Loop** | `O(N^2)` | `O(N^2)` | `O(N)` | Each step allocates a new heap buffer of size `K`, copying all previous characters (`1 + 2 + ... + N`). |
| **`StringBuilder` in Loop** | `O(N)` | `O(N)` | `O(N)` | Uses geometric doubling amortized `O(1)` append; single final allocation on `.ToString()`. |
| **`ComputeFrequency`** | `O(N)` | `O(1)` | `O(1)` | Fixed 26-element array regardless of input string length `N`. |

---

## 🎙️ 45-MINUTE INTERVIEW VERBAL SCRIPTS

### Script 1: Defending StringBuilder over Repeated String Concatenation
> *"In C#, Java, and Python, strings are immutable heap objects. If we perform repeated concatenation inside a loop like `s += ch`, every iteration allocates a brand-new string on the managed heap and copies all existing characters. For a loop of `N` iterations, total characters copied is `sum(1..N) = O(N^2)`, which not only bottlenecks CPU cycles but floods the Garbage Collector with short-lived objects. To achieve optimal `O(N)` runtime, I use `StringBuilder`, which maintains an internal dynamic character array that doubles geometrically, amortizing append costs to `O(1)`."*

### Script 2: Defending Integer Overflow Detection in `atoi`
> *"A frequent trap in string-to-integer parsing is multiplying by 10 before checking for overflow, which can cause the integer to silently wrap into negative numbers. To prevent this, I inspect `result` against `int.MaxValue / 10` before executing `result * 10 + digit`. If `result` is greater than this threshold, or if it equals the threshold and the incoming digit exceeds 7, any further arithmetic will overflow 32-bit limits. I immediately clamp and return `int.MaxValue` or `int.MinValue` according to the sign."*

### Script 3: Handling the Two's Complement Negation Asymmetry in `itoa`
> *"In Two's complement representation, the range of a 32-bit signed integer is `[-2^31, 2^31 - 1]`, or `[-2,147,483,648, 2,147,483,647]`. Notice that the magnitude of `int.MinValue` is 1 greater than `int.MaxValue`. If you attempt to make the number positive using `Math.Abs(int.MinValue)` or `-value`, it overflows back to negative. In my `itoa` implementation, I handle `int.MinValue` as an explicit guard condition or cast to 64-bit integer before computing digits."*

---

## ⚖️ CHAPTER 4: PRODUCTION TRADEOFFS & SYSTEMS CONTEXT

> [!NOTE]
> **Interview & Systems Context: Piece Tables & Ropes in Production Text Editors (VS Code)**  
> Text editors like VS Code and Monaco do not store file contents as a monolithic string or character array; inserting a single keystroke in a 50MB file would require shifting 50MB of memory (`O(N)`). Instead, they employ Piece Tables or Ropes (trees of immutable string slices). Inserts and deletes modify pointer references in `O(log N)` or `O(1)` time without copying the underlying character buffers.

> [!NOTE]
> **Interview & Systems Context: Database Collation & Index Prefix Compression**  
> In relational databases (PostgreSQL, MySQL), comparing long strings for indexing is computationally expensive. High-performance storage engines store a fixed-width 4-byte or 8-byte prefix hash alongside the string record. The engine compares the numeric prefix first in a single CPU instruction, only performing full string dereferencing and comparison when the prefix hashes match.

> [!NOTE]
> **Interview & Systems Context: The Ariane 5 Rocket Failure (Integer Overflow)**  
> In 1996, the Ariane 5 rocket was destroyed 37 seconds after launch due to an unhandled numerical overflow. Software attempting to convert a 64-bit floating-point velocity measurement into a 16-bit signed integer exceeded `32,767`, causing an unhandled hardware exception that caused the primary guidance computers to crash. Production systems must enforce explicit boundary clamping on all numerical conversions.

---

## ⚔️ SUPPLEMENTARY OUTCOMES & REVISION

### 🏋️ Practice Problems

| Problem | Difficulty | Key Concept | Target Complexity |
| :--- | :--- | :--- | :--- |
| **String to Integer (atoi)** | 🟡 Medium | Overflow Clamping & Sign Parsing | `O(N)` Time, `O(1)` Space |
| **Integer to String (itoa)** | 🟡 Medium | Digit Extraction & Asymmetry Guard | `O(D)` Time, `O(D)` Space |
| **Valid Anagram** | 🟢 Easy | 26-Bucket ASCII Frequency Map | `O(N)` Time, `O(1)` Space |
| **Reverse String In-Place** | 🟢 Easy | Two-Pointer Swap | `O(N)` Time, `O(1)` Space |
| **Longest Common Prefix** | 🟢 Easy | Vertical Scanning Across Strings | `O(S)` Time, `O(1)` Space |

### 🎙️ Quick Technical Screen Q&A

1. **Q:** *Why is character arithmetic `c - 'a'` useful?*  
   **A:** It maps lowercase characters `'a'` through `'z'` to contiguous array indices `0` through `25`, allowing `O(1)` lookups in a fixed 26-element array without the overhead of a `HashMap`.
2. **Q:** *What happens when you negate `int.MinValue` in C# or Java?*  
   **A:** It overflows and remains `int.MinValue` (-2,147,483,648) because `+2,147,483,648` exceeds the maximum positive 32-bit signed integer.
3. **Q:** *Why does string immutability benefit multithreaded systems?*  
   **A:** Because immutable objects can be safely shared across multiple concurrent threads without synchronization locks or risk of data races.

---

> 🧭 **Navigation:** [← Previous Day](Week_02_Day_05_Binary_Search_Invariants_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Week Playbook →](WEEK_02_FULL_PLAYBOOK.md)

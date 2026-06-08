# 📘 Week 14 Full Playbook: Matrices, Bitmasks & Number Theory Mastery

Welcome to the comprehensive, integrated study workbook for Week 14. This playbook aligns your mental models, mechanical walkthroughs, and optimized implementations across five cohesive study days. 

---

## 🗂️ Table of Contents
1.  [Day 1: Matrix Operations, Transformations & Higher-Dimensional Searches](#-day-1-matrix-operations-transformations--higher-dimensional-searches)
2.  [Day 2: Bitwise Registers, Tricks, Submask Loops & Gray Codes](#-day-2-bitwise-registers-tricks-submask-loops--gray-codes)
3.  [Day 3: Number Theory Basics, GCD, LCM, Primes & Modular Exponentiation](#-day-3-number-theory-basics-gcd-lcm-primes--modular-exponentiation)
4.  [Day 4: Advanced Preprocessed Strings, KMP Matching, Tries & Radix Sorting](#-day-4-advanced-preprocessed-strings-kmp-matching-tries--radix-sorting)
5.  [Day 5: Advanced Number Theory, Euler's Totient & Chinese Remainder Theorem](#-day-5-advanced-number-theory-eulers-totient--chinese-remainder-theorem)
6.  [🔁 Week 14 Multi-Domain Integration & Complex Synthesis](#-week-14-multi-domain-integration--complex-synthesis)

---

## 🧮 Day 1: Matrix Operations, Transformations & Higher-Dimensional Searches

### 1. Conceptual Grounding
Matrix operations test index tracking and boundary control. Because contiguous row layouts (Row-Major format) match hardware cache lines, indexing patterns affect speed:
*   **Row-major sequential loop**: Highly cache-friendly.
*   **Vertical column traversal**: Triggers repetitive cache line misses.

Decomposing grid rotations into composed steps prevents out-of-bounds errors and complex projection tracking.
*   **Clockwise 90°** == Transpose diagonally arrow Reverse rows horizontally.

### 2. State Mechanics & Dry-Runs

#### 🚶 Spiral Tracking Trace
Track four pointers: `top` (T), `bottom` (B), `left` (L), and `right` (R):
```text
  L                   R
T [ 1  ->  2  ->  3 ]   1. Fill top row (left to right), increment T
  [ 4      5      6 ]   2. Fill right col (top to bottom), decrement R
  [ 7  <-  8  <-  9 ]   3. Fill bottom row (right to left), decrement B
B                       4. Fill left col (bottom to top), increment L
```

### 3. Implementations (C# & Python)

```csharp
// C# - Rotation 90° Clockwise In-Place
public static void RotateClockwise90(int[][] matrix) {
    int n = matrix.Length;
    for (int i = 0; i < n; i++) {
        for (int j = i + 1; j < n; j++) {
            (matrix[i][j], matrix[j][i]) = (matrix[j][i], matrix[i][j]);
        }
    }
    for (int i = 0; i < n; i++) {
        Array.Reverse(matrix[i]);
    }
}
```

```python
# Python - Spiral Order Walk
def spiral_order(matrix: list[list[int]]) -> list[int]:
    if not matrix: return []
    top, bottom = 0, len(matrix) - 1
    left, right = 0, len(matrix[0]) - 1
    result = []
    while top <= bottom and left <= right:
        for col in range(left, right + 1):
            result.append(matrix[top][col])
        top += 1
        for row in range(top, bottom + 1):
            result.append(matrix[row][right])
        right -= 1
        if top <= bottom:
            for col in range(right, left - 1, -1):
                result.append(matrix[bottom][col])
            bottom -= 1
        if left <= right:
            for row in range(bottom, top - 1, -1):
                result.append(matrix[row][left])
            left += 1
    return result
```

---

## 🔌 Day 2: Bitwise Registers, Tricks, Submask Loops & Gray Codes

### 1. Conceptual Grounding
We can lock state flags inside single registers to avoid heap allocations and lower garbage collection overhead.
*   **AND (`&`)**: Intersects bits.
*   **OR (`|`)**: Enables bits.
*   **XOR (`^`)**: Detects parities / differences.

### 2. State Mechanics & Dry-Runs

#### Bit Trick Arithmetic Trace
```text
  Value (X)       =  12  ==>  0 0 0 0 1 1 0 0
  Add One (-X)    =  -12 ==>  1 1 1 1 1 1 0 0   (Two's Complement carry)
  -------------------------------------------
  AND (X & -X)    =   4  ==>  0 0 0 0 0 1 0 0   (Isolates the lowest set bit)
```

### 3. Implementations (C# & Python)

```csharp
// C# - Submask Decrement Enumerator
public static List<int> EnumerateSubmasks(int mask) {
    var submasks = new List<int>();
    int sub = mask;
    while (sub > 0) {
        submasks.Add(sub);
        sub = (sub - 1) & mask;
    }
    submasks.Add(0);
    return submasks;
}
```

```python
# Python - Brian Kernighan popcounting
def popcount(n: int) -> int:
    count = 0
    while n > 0:
        n &= n - 1 # Clears the lowest set bit
        count += 1
    return count
```

---

## 📐 Day 3: Number Theory Basics, GCD, LCM, Primes & Modular Exponentiation

### 1. Conceptual Grounding
To prevent register overflows on large exponents, solve equations on wrapped modular spaces.
*   **Euclidean algorithm**: Finding greatest common divisors using modulo remainder steps:
    gcd(a, b) = gcd(b, a \bmod b)

### 2. State Mechanics & Dry-Runs

#### ModPow Binary Exponentiation Trace
```text
  Calculate 3^13 mod 7 (13 in binary = 1101)
  Step 1: Exp = 13 (odd)  => Res = (1 * 3) % 7 = 3  | Base = (3 * 3) % 7 = 2  | Exp >> 1 = 6
  Step 2: Exp = 6 (even)  => Res = 3                | Base = (2 * 2) % 7 = 4  | Exp >> 1 = 3
  Step 3: Exp = 3 (odd)   => Res = (3 * 4) % 7 = 5  | Base = (4 * 4) % 7 = 2  | Exp >> 1 = 1
  Step 4: Exp = 1 (odd)   => Res = (5 * 2) % 7 = 3  | Base = (2 * 2) % 7 = 4  | Exp >> 1 = 0
  Result: 3
```

### 3. Implementations (C# & Python)

```csharp
// C# - Logarithmic Exponentiation and Modular Inverse
public static class ModularMath {
    public static long ModPow(long b, long e, long m) {
        long res = 1;
        b %= m;
        while (e > 0) {
            if ((e & 1) == 1) res = (res * b) % m;
            b = (b * b) % m;
            e >>= 1;
        }
        return res;
    }

    public static long GetModInverse(long val, long prime) {
        return ModPow(val, prime - 2, prime); // Fermat's Little Theorem
    }
}
```

```python
# Python - Sieve of Eratosthenes
def sieve(limit: int) -> list[bool]:
    is_prime = [True] * (limit + 1)
    is_prime[0] = is_prime[1] = False
    for p in range(2, int(limit**0.5) + 1):
        if is_prime[p]:
            for m in range(p * p, limit + 1, p):
                is_prime[m] = False
    return is_prime
```

---

## 🧵 Day 4: Advanced Preprocessed Strings, KMP Matching, Tries & Radix Sorting

### 1. Conceptual Grounding
We compile string patterns into fallback tables or prefix trees to avoid backward pointer searches on mismatches.
*   **KMP Algorithm**: Scans the text stream linearly by stepping fallback states based on the prefix mapping of the pattern (LPS Table).
*   **Radix sorting**: Beats comparison bounds (O(N log N)) by sorting digit columns sequentially.

### 2. State Mechanics & Dry-Runs

#### LPS Table Tracings
```text
  Pattern: "A B A B C"
  LPS Table construction:
  lps[0] = 0
  i = 1, len = 0: 'B' != 'A' => lps[1] = 0, i = 2
  i = 2, len = 0: 'A' == 'A' => len = 1, lps[2] = 1, i = 3
  i = 3, len = 1: 'B' == 'B' => len = 2, lps[3] = 2, i = 4
  i = 4, len = 2: 'C' != 'B' => len = lps[1] = 0
  i = 4, len = 0: 'C' != 'A' => lps[4] = 0, i = 5
  Result Table: [0, 0, 1, 2, 0]
```

### 3. Implementations (C# & Python)

```csharp
// C# - KMP Matcher
public static class KmpScanner {
    public static List<int> Match(string txt, string pat) {
        var matchIndexes = new List<int>();
        int[] lps = BuildLps(pat);
        int t = 0, p = 0;
        while (t < txt.Length) {
            if (txt[t] == pat[p]) {
                t++; p++;
                if (p == pat.Length) {
                    matchIndexes.Add(t - p);
                    p = lps[p - 1];
                }
            } else {
                if (p > 0) p = lps[p - 1];
                else t++;
            }
        }
        return matchIndexes;
    }

    private static int[] BuildLps(string pat) {
        int[] lps = new int[pat.Length];
        int len = 0, i = 1;
        while (i < pat.Length) {
            if (pat[i] == pat[len]) {
                len++; lps[i] = len; i++;
            } else {
                if (len > 0) len = lps[len - 1];
                else { lps[i] = 0; i++; }
            }
        }
        return lps;
    }
}
```

```python
# Python - Trie Prefix insert & search
class TrieNode:
    def __init__(self):
        self.children = {}
        self.is_word = False

class Trie:
    def __init__(self):
        self.root = TrieNode()

    def insert(self, word: str) -> None:
        current = self.root
        for ch in word:
            if ch not in current.children:
                current.children[ch] = TrieNode()
            current = current.children[ch]
        current.is_word = True

    def starts_with(self, prefix: str) -> bool:
        current = self.root
        for ch in prefix:
            if ch not in current.children:
                return False
            current = current.children[ch]
        return True
```

---

## 🪐 Day 5: Advanced Number Theory, Euler's Totient & Chinese Remainder Theorem

### 1. Conceptual Grounding
*   **Euler's Totient** phi(N): Counts the number of positive integers up to N that are coprime to N.
*   **Chinese Remainder Theorem**: Solves systems of equations modulo coprime bases by dividing calculations into smaller channels.

### 2. State Mechanics & Dry-Runs

#### CRT Equation Reconstructions
```text
  Solve:
    x ≡ 2 (mod 3)
    x ≡ 3 (mod 5)
  moduli = {3, 5} (coprime)
  totalM = 15 | M1 = 5, M2 = 3
  y1 (Inv of 5 mod 3) => 5 * y1 ≡ 1 => 2 * y1 ≡ 1 => y1 = 2
  y2 (Inv of 3 mod 5) => 3 * y2 ≡ 1 => y2 = 2
  x = (2 * 5 * 2 + 3 * 3 * 2) % 15 = (20 + 18) % 15 = 38 % 15 = 8
```

### 3. Implementations (C# & Python)

```csharp
// C# - Euler's Totient Calculation
public static class TotientCalc {
    public static long GetTotient(long n) {
        long result = n;
        long temp = n;
        for (long p = 2; p * p <= temp; p++) {
            if (temp % p == 0) {
                result -= result / p;
                while (temp % p == 0) {
                    temp /= p;
                }
            }
        }
        if (temp > 1) {
            result -= result / temp;
        }
        return result;
    }
}
```

```python
# Python - Chinese Remainder Theorem Solver
def ext_gcd(a: int, b: int) -> tuple[int, int, int]:
    if b == 0: return a, 1, 0
    g, x1, y1 = ext_gcd(b, a % b)
    return g, y1, x1 - (a // b) * y1

def solve_crt(a: list[int], m: list[int]) -> int:
    total_m = 1
    for base in m:
         total_m *= base
    result = 0
    for val, base in zip(a, m):
        m_i = total_m // base
        _, x, _ = ext_gcd(m_i, base)
        inv = (x % base + base) % base
        result = (result + val * m_i * inv) % total_m
    return result
```

---

## 🔁 Week 14 Multi-Domain Integration & Complex Synthesis

To master Week 14's topics, combine them to solve complex problems:
1.  **Bitmask Matrix States**: For small grids (N <= 25), store state information as single integer bitmasks. This avoids large allocations inside recursive search loops.
2.  **Sieve Precomputation**: Precompute primes once at startup using a sieve to speed up subsequent primality checks to O(1) time.
3.  **Compressed Path Tries**: Reduce the memory footprint of large-scale character dictionaries by merging adjacent nodes with single children into compressed paths.

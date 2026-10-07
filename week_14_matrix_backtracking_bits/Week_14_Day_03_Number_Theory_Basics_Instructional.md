# 📅 Week 14, Day 3: Number Theory Basics, GCD, LCM, Primes & Modular Exponentiation




> 🧭 **Navigation:** [← Previous Day](Week_14_Day_02_Bitwise_Operations_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_14_Day_04_Advanced_Strings_Instructional.md)
> 
> 💡 **Instructor Note:** *Not all sections or topics are mandatory. Feel free to adapt your pace and skim or skip sections based on your current focus and interview timeline.*

---

Welcome to Day 3. Today, we build practical number theory tools for coding interviews: GCD/LCM, prime generation, modular arithmetic, fast power, and modular inverse.

---

## 🎯 Learning Objectives
*   Master the Euclidean algorithm for Greatest Common Divisor (GCD) and Least Common Multiple (LCM).
*   Check primality with trial division and generate primes up to N using the Sieve of Eratosthenes.
*   Understand modular arithmetic laws and implement logarithmic modular exponentiation.
*   Find modular multiplicative inverses using Fermat's Little Theorem (prime modulus) and know when to use Extended Euclid.

---

## 📘 Chapter 1: Context and Motivation

### 1. The Core Engineering Challenge
In algorithmic problems, values can become enormous quickly. Computing powers or combinations like N! can overflow fixed-width integers.
But modular arithmetic changes the rules for division:
`(A / B) % M != (A % M) / (B % M)`

So we need number theory to compute safely and correctly.

### 2. Naive Pitfalls
The naive way to compute modular powers like `b^e % m` is to multiply `b` by itself `e` times in a loop:
```csharp
long res = 1;
for (int i = 0; i < e; i++) { res = (res * b) % m; }
```
This takes O(e) operations. If e approx. 10^9, this is too slow.

### 3. Real-world Anchor: Encryption & Hashing
Secure systems (HTTPS, SSH, signatures) rely on modular exponentiation and prime-based arithmetic.
Hashing systems also use modulo arithmetic to keep values bounded and distribute keys.

---

## 📘 Chapter 2: Mental Model

### 1. Modular Congruence Wrapped Space
Think of modular arithmetic as a circular clock face. Numbers wrap around a fixed modular range, preserving arithmetic relationships (sums and products) across boundaries:

```text
                        11   0   1
                      10           2
                     9       o       3   <-- 12-hour Clock Analogy
                      8             4
                        7    6    5
```

Every integer `n > 1` has a unique prime factorization up to ordering, including exponents:
`n = p_1^(a_1) * p_2^(a_2) * ... * p_k^(a_k)`
This is the foundation for many number-theory shortcuts.

#### 📐 Core Modulo Mechanics
*   **Addition**: `(A + B) % M = ((A % M) + (B % M)) % M`
*   **Multiplication**: `(A * B) % M = ((A % M) * (B % M)) % M`
*   *Caveat*: Division is not direct. Use inverse only if denominator and modulus are coprime.

---

## 📘 Chapter 3: Mechanics

### 1. Euclidean GCD and LCM
The Euclidean Greatest Common Divisor (GCD) algorithm replaces the larger number with its modular remainder recursively until it reaches 0:
gcd(a, b) = gcd(b, a % b)

Invariant: the set of common divisors of (a, b) is identical to that of (b, a % b), so the GCD stays unchanged each step.

```csharp
public static long Gcd(long a, long b) {
    while (b > 0) {
        long temp = b;
        b = a % b;
        a = temp;
    }
    return Math.Abs(a);
}

public static long Lcm(long a, long b) {
    if (a == 0 || b == 0) return 0;
    // Divide first to prevent intermediate multiplication overflows
    return Math.Abs(a / Gcd(a, b) * b);
}
```

### 2. Prime Checking (Trial Division)
For a single number, trial divide up to sqrt(n). If no divisor exists, the number is prime.

```csharp
public static bool IsPrime(long n) {
    if (n < 2) return false;
    if (n % 2 == 0) return n == 2;
    for (long d = 3; d * d <= n; d += 2) {
        if (n % d == 0) return false;
    }
    return true;
}
```

---

### 3. Sieve of Eratosthenes (Prime Generation)
To generate all primes up to N, create a boolean array representing raw primality. Starting from p=2, if p is prime, mark all of its multiples starting from p^2 as composite:

```csharp
public static bool[] Sieve(int limit) {
    bool[] isPrime = new bool[limit + 1];
    Array.Fill(isPrime, true);
    if (limit >= 0) isPrime[0] = false;
    if (limit >= 1) isPrime[1] = false;

    for (int p = 2; p * p <= limit; p++) {
        if (isPrime[p]) {
            // Mark multiples of p starting from p^2
            for (int m = p * p; m <= limit; m += p) {
                isPrime[m] = false;
            }
        }
    }
    return isPrime;
}
```

---

### 4. Logarithmic Modular Exponentiation
Fast modular exponentiation computes remainders in O(log e) time using binary decomposition. It squares the base and halves the exponent in each step:

```csharp
public static long ModPow(long b, long e, long m) {
    if (m <= 0) throw new ArgumentException("Modulus must be positive.");
    if (m == 1) return 0;
    long res = 1 % m;
    b = ((b % m) + m) % m;
    while (e > 0) {
        if ((e & 1) == 1) {
            res = (res * b) % m;
        }
        b = (b * b) % m;
        e >>= 1;
    }
    return res;
}
```

---

### 5. Modular Multiplicative Inverse
The modular inverse of `A` modulo `P` is an integer `x` such that:
`(A * x) % P == 1`  (often written `A * x = 1 (mod P)`)

*   **Fermat's Little Theorem**: If `P` is prime and `gcd(A, P) == 1`, then:
    `A^(P-1) = 1 (mod P)`
*   Multiplying both sides by `A^(-1)`:
    `A^(-1) = A^(P-2) (mod P)`

```csharp
public static long ModularInversePrime(long val, long primeMod) {
    if (primeMod <= 1) {
        throw new ArgumentException("Modulus must be > 1.");
    }
    long normalized = ((val % primeMod) + primeMod) % primeMod;
    if (normalized == 0) {
        throw new ArgumentException("No inverse exists because value is 0 modulo primeMod.");
    }
    // Valid when primeMod is prime.
    return ModPow(normalized, primeMod - 2, primeMod);
}
```

For composite modulus, use the Extended Euclidean Algorithm requiring `gcd(A, M) == 1`.

---

### 6. Python Clarity-First Implementations

```python
def gcd(a: int, b: int) -> int:
    """Computes Greatest Common Divisor in O(log(min(a, b))) time."""
    while b > 0:
        a, b = b, a % b
    return abs(a)


def lcm(a: int, b: int) -> int:
    """Computes Least Common Multiple, dividing first to prevent overflow."""
    if a == 0 or b == 0:
        return 0
    return abs((a // gcd(a, b)) * b)


def is_prime(n: int) -> bool:
    """Trial division primality test in O(sqrt(n)) time."""
    if n < 2:
        return False
    if n % 2 == 0:
        return n == 2
    d = 3
    while d * d <= n:
        if n % d == 0:
            return False
        d += 2
    return True


def sieve(limit: int) -> list[bool]:
    """Generates primality table up to limit in O(N log log N) time."""
    if limit < 0:
        return []
    is_p = [True] * (limit + 1)
    if limit >= 0:
        is_p[0] = False
    if limit >= 1:
        is_p[1] = False

    p = 2
    while p * p <= limit:
        if is_p[p]:
            for m in range(p * p, limit + 1, p):
                is_p[m] = False
        p += 1
    return is_p


def mod_pow(base: int, exp: int, mod: int) -> int:
    """Computes (base^exp) % mod in O(log exp) time."""
    if mod <= 0:
        raise ValueError("Modulus must be positive.")
    if mod == 1:
        return 0
    res = 1 % mod
    base = base % mod
    while exp > 0:
        if exp & 1:
            res = (res * base) % mod
        base = (base * base) % mod
        exp >>= 1
    return res


def modular_inverse_prime(val: int, prime_mod: int) -> int:
    """Computes modular multiplicative inverse modulo a prime using Fermat's Little Theorem."""
    if prime_mod <= 1:
        raise ValueError("Modulus must be > 1.")
    normalized = val % prime_mod
    if normalized == 0:
        raise ValueError("No inverse exists for 0 mod prime_mod.")
    return mod_pow(normalized, prime_mod - 2, prime_mod)
```

---

## 📘 Chapter 4: Performance, Invariants & Systems Architecture

### 1. Exact Governing Invariants

*   **Euclidean GCD Invariant**: For any integers `a` and `b > 0`, `gcd(a, b) = gcd(b, a % b)`. Any common divisor of `a` and `b` divides `a - q * b = a % b`. The remainder strictly decreases by at least a factor of 2 every two steps (`a % b < a / 2`), guaranteeing logarithmic termination in at most `5 * log10(min(a, b))` iterations (Lamé's Theorem).
*   **Primality Sifting Invariant**: If an integer `n` is composite, it can be factored into `n = a * b` where at least one factor satisfies `min(a, b) <= sqrt(n)`. Therefore, testing trial divisors up to `floor(sqrt(n))` without finding a factor strictly proves primality.
*   **Sieve of Eratosthenes Marking Invariant**: When a prime `p` is processed, every composite multiple `k * p` for `k < p` has already been crossed out by a prime factor smaller than `p`. Starting multiple elimination at `p * p` guarantees zero redundant marking of composite numbers with smaller prime factors, establishing `O(N log log N)` runtime.
*   **Binary Modular Exponentiation Invariant**: At each loop step, the invariant:
    `(res * base^exp) % mod == constant`
    is maintained. Halving `exp` and squaring `base` reduces the exponent to zero in `floor(log2(exp)) + 1` cycles without intermediate 64-bit integer overflow.
*   **Fermat's Modular Inverse Invariant**: If `P` is prime and `gcd(A, P) == 1`, Fermat's Little Theorem establishes that `A^(P-1) = 1 (mod P)`. Multiplying both sides by `A^(-1)` yields `A^(-1) = A^(P-2) (mod P)`. This holds strictly for prime moduli.

---

### 2. Explicit Complexity Deconstruction

| Algorithm | Time (Best / Avg / Worst) | Auxiliary Space | Output Space | Mathematical Derivation |
| :--- | :--- | :--- | :--- | :--- |
| **Euclidean GCD** | `O(1)` / `O(log(min(a, b)))` / `O(log(min(a, b)))` | `O(1)` | `O(1)` | Worst case: consecutive Fibonacci numbers where remainder ratio converges to `1 / phi`. |
| **Least Common Multiple** | `O(log(min(a, b)))` | `O(1)` | `O(1)` | Governed by single GCD computation: `a / gcd(a, b) * b`. |
| **Trial Division Primality** | `O(1)` / `O(sqrt(n))` / `O(sqrt(n))` | `O(1)` | `O(1)` | Best case: even numbers (`O(1)`). Worst case: prime numbers requiring `sqrt(n) / 2` division checks. |
| **Sieve of Eratosthenes** | `O(N log log N)` | `O(N)` | `O(N)` | Sum over primes: `sum_{p <= N} (N / p) = N * sum (1 / p) = N log log N` (Mertens' Second Theorem). |
| **Modular Exponentiation** | `O(1)` / `O(log exp)` / `O(log exp)` | `O(1)` | `O(1)` | Bit-length of exponent: exactly `floor(log2(exp)) + 1` iterations. |
| **Fermat Modular Inverse** | `O(log mod)` | `O(1)` | `O(1)` | Single `ModPow(val, mod - 2, mod)` call. |

---

### 3. Senior Interview Context: Hardware, Memory & Concurrency

*   **64-Bit Integer Multiplication Overflow**:
    In C# and C++, standard modular arithmetic uses `(a * b) % mod`. If `mod = 10^9 + 7`, the intermediate product `a * b` can reach `approx. 10^18`, which fits inside a signed 64-bit `long` (max `9.22 * 10^18`). However, if `mod > 3 * 10^9`, `a * b` overflows `long.MaxValue`, producing silent sign-bit wrap and incorrect results. In such environments, use **Russian Peasant Safe Multiplication** (`O(log B)`) or 128-bit integer primitives (`Int128` in .NET 7+, `__int128` in GCC).
*   **Modulo Semantics Across Languages**:
    - In C#, Java, and C++, `%` is the **remainder** operator: `-7 % 3 = -1`.
    - In Python, `%` is the **modulo** operator (floored division): `-7 % 3 = 2`.
    - Production rule: Always normalize modular values using `((val % mod) + mod) % mod` to ensure strict non-negative results in `[0, mod - 1]`.
*   **Prime Sieve Memory Footprint**:
    A naive `bool[]` array allocates 1 byte per boolean flag. For `limit = 10^8`, this consumes 100 MB of heap memory, exceeding L3 cache capacity and causing DRAM thrashing. In high-performance systems, pack primality flags into a `BitArray` or an array of `ulong[]` (64 flags per 8-byte word), shrinking memory usage to 12.5 MB.

---

## 🎙️ Senior 45-Minute Verbal Talk Track

```text
+-------------------------------------------------------------------------------+
| PHASE 1: Constraints & Modulus Validation (Minutes 00 - 05)                   |
| - Verify magnitude of base, exponent, and modulus (e.g., mod = 10^9 + 7).     |
| - Check if modulus is strictly prime (enabling Fermat) or composite.          |
+-------------------------------------------------------------------------------+
                                      │
                                      ▼
+-------------------------------------------------------------------------------+
| PHASE 2: Naive Linear Approach & Bottleneck (Minutes 05 - 10)                 |
| - Highlight naive O(exp) multiplication loop: 10^9 operations = timeout.      |
| - Explain integer overflow hazard when chaining multiplications.              |
+-------------------------------------------------------------------------------+
                                      │
                                      ▼
+-------------------------------------------------------------------------------+
| PHASE 3: Mathematical Invariants          (Minutes 10 - 20)                   |
| - Derive binary exponentiation invariant: (res * base^exp) % mod == constant. |
| - State Fermat's Little Theorem: A^(P-1) = 1 (mod P) => A^(P-2) = A^(-1).     |
| - Prove Sieve of Eratosthenes starts composite marking at p * p.              |
+-------------------------------------------------------------------------------+
                                      │
                                      ▼
+-------------------------------------------------------------------------------+
| PHASE 4: Clean Production Implementation  (Minutes 20 - 35)                   |
| - Write clean, overflow-safe C# (.NET 8/9) and Python (3.11+) routines.       |
| - Guard against edge cases: mod <= 1, exp = 0, val % mod = 0.                 |
| - Enforce non-negative normalization: ((val % mod) + mod) % mod.              |
+-------------------------------------------------------------------------------+
                                      │
                                      ▼
+-------------------------------------------------------------------------------+
| PHASE 5: Rigorous Verification & Extensions (Minutes 35 - 45)                 |
| - Walk through complexity: O(log exp) time, O(1) auxiliary space.             |
| - Verify edge cases: exp = 0 (returns 1 % mod), base = 0, primeMod = 2.       |
| - Address extensions: Extended Euclidean Algorithm for composite moduli.      |
+-------------------------------------------------------------------------------+
```

### Verbal Script Excerpts

*   **Opening (Minutes 00-05)**: *"Before writing the modular inverse routine, let me verify the modulus characteristics. Is `mod` guaranteed to be a prime number (such as `10^9 + 7`), and is `val` guaranteed to be non-zero modulo `mod`? If `mod` is prime, Fermat's Little Theorem gives an optimal `O(log mod)` solution. If `mod` can be composite, we must use the Extended Euclidean Algorithm to solve `a * x + m * y = gcd(a, m)`."*
*   **Invariant Articulation (Minutes 10-20)**: *"For fast modular exponentiation, we represent the exponent in binary. If the lowest bit of `exp` is 1, we fold `base` into our running result: `res = (res * base) % mod`. We then square `base = (base * base) % mod` and shift `exp >>= 1`. The invariant `(res * base^exp) % mod == constant` holds throughout, guaranteeing exact results in `O(log exp)` time with `O(1)` space."*
*   **Sieve Composite Marking Defense (Minutes 35-45)**: *"Notice line 124 in the Sieve: we start marking composites at `m = p * p` rather than `2 * p`. This is because any smaller composite `k * p` (where `k < p`) has a prime factor smaller than `p`, meaning it was already marked when that smaller prime was processed. This eliminates redundant work and establishes the `O(N log log N)` bound."*

---

## 📘 Chapter 5: Integration and Mastery

### 1. Pattern Selection Rules
*   *Use the Euclidean Algorithm when*: You need to simplify fraction ratios, verify coprime relationships, or solve linear Diophantine equations.
*   *Use Trial Division when*: You need primality for a single query or small set of inputs (`N <= 10^{12}`).
*   *Use the Sieve of Eratosthenes when*: You need dense primality queries or factor precomputations for all integers up to `10^7`.
*   *Use Fermat's Modular Inverse when*: You need division under a known prime modulus, such as calculating combinations `C(N, K) % (10^9 + 7)`.
*   *Use Extended Euclidean Algorithm when*: The modulus `M` is composite and you must determine if an inverse exists (`gcd(A, M) == 1`).

### 2. Beginner to Advanced Progression
1. **Beginner**: Implement iterative `Gcd`, `Lcm`, and single-number trial division primality.
2. **Intermediate**: Implement Sieve of Eratosthenes and logarithmic modular exponentiation with negative input handling.
3. **Advanced**: Precompute factorials and inverse factorials in `O(N)` time to answer `C(N, K) % P` combinations in `O(1)` per query.

---

## 🛠️ Supplementary Material

### Practice Problems
1.  **Count Primes** ([LeetCode 204](https://leetcode.com/problems/count-primes/)): Count prime numbers less than a non-negative integer `n`.
2.  **Pow(x, n)** ([LeetCode 50](https://leetcode.com/problems/powx-n/)): Implement binary exponentiation with negative powers.
3.  **Greatest Common Divisor of Strings** ([LeetCode 1071](https://leetcode.com/problems/greatest-common-divisor-of-strings/)): Generalize numeric GCD to string periodicities.
4.  **Super Pow** ([LeetCode 372](https://leetcode.com/problems/super-pow/)): Calculate `a^b % 1337` where `b` is an array of large decimal digits using Euler's Totient.
5.  **Reconstruct Combinations Modulo Prime**: Compute `nCr % (10^9 + 7)` using precomputed factorial modular inverses.

### Misconceptions and Corrections
*   *Incorrect Idea*: Attempting to calculate modular inverses modulo composite numbers using Fermat's Little Theorem.
    *   *Correction*: Fermat's Little Theorem strictly requires a prime modulus. For composite moduli, you must use the **Extended Euclidean Algorithm**; if `gcd(A, M) != 1`, no inverse exists.
*   *Incorrect Idea*: Assuming `(a * b) % m` never overflows in 64-bit integers.
    *   *Correction*: If `m > 3 * 10^9`, the product `a * b` exceeds `long.MaxValue` (`approx. 9.22 * 10^18`), leading to silent signed overflow. Use Russian Peasant safe multiplication or 128-bit integer registers.
*   *Incorrect Idea*: Assuming `Lcm(a, b) = (a * b) / Gcd(a, b)` is safe.
    *   *Correction*: Multiplying `a * b` first can cause 32-bit or 64-bit integer overflow. Always divide first: `(a / Gcd(a, b)) * b`.

---

> 🧭 **Navigation:** [← Previous Day](Week_14_Day_02_Bitwise_Operations_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_14_Day_04_Advanced_Strings_Instructional.md)

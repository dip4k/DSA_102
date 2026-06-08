# 📅 Week 14, Day 3: Number Theory Basics, GCD, LCM, Primes & Modular Exponentiation

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
To control growth, problems ask for answers modulo a value (often a large prime like 10^9 + 7).
But modular arithmetic changes the rules for division:
\frac{A}{B} +/-od M \neq \frac{A \bmod M}{B \bmod M}
So we need number theory to compute safely and correctly.

### 2. Naive Pitfalls
The naive way to compute modular powers like b^e \bmod m is to multiply b by itself e times in a loop:
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

Every integer n > 1 has a unique prime factorization up to ordering, including exponents:
n = p_1^{a_1} p_2^{a_2} *s p_k^{a_k}
This is the foundation for many number-theory shortcuts.

#### 📐 Core Modulo Mechanics
*   **Addition**: (A + B) \bmod M = ((A \bmod M) + (B \bmod M)) \bmod M
*   **Multiplication**: (A x B) \bmod M = ((A \bmod M) x (B \bmod M)) \bmod M
*   *Caveat*: Division is not direct. Use inverse only if denominator and modulus are coprime.

---

## 📘 Chapter 3: Mechanics

### 1. Euclidean GCD and LCM
The Euclidean Greatest Common Divisor (GCD) algorithm replaces the larger number with its modular remainder recursively until it reaches 0:
gcd(a, b) = gcd(b, a \bmod b)

Invariant: the set of common divisors of (a, b) is identical to that of (b, a \bmod b), so the GCD stays unchanged each step.

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
The modular inverse of A modulo P is an integer x such that:
A x x == 1 +/-od P
*   **Fermat's Little Theorem**: If P is prime and gcd(A, P) = 1, then A^{P-1} == 1 +/-od P.
*   Therefore:
    A^{-1} == A^{P-2} +/-od P

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

For composite modulus, use Extended Euclidean Algorithm and require gcd(A, M) = 1.

---

## 📘 Chapter 4: Performance and Systems

*   **Computational Complexities Table**:
    | Algorithm | Time Complexity | Space Complexity | Real-world Constraints |
    | :--- | :--- | :--- | :--- |
    | **Euclidean GCD** | O(log(min(a, b))) | O(1) | Fibonacci inputs represent worst-case steps |
    | **Prime Check (Trial Division)** | O(sqrt(n)) | O(1) | Good for single queries |
    | **Sieve of Eratosthenes** | O(limit log log limit) | O(limit) | Memory footprint scales with limits |
    | **Fast modular Exponentiation** | O(log e) | O(1) | Keeps intermediate multiplications safe |
    | **Fermat Modular Inverse** | O(log mod) | O(1) | Modulus must be prime and value non-zero mod mod |

*   **Preventing Intermediate Overflow**: 64-bit `long` is enough for common interview moduli like 10^9+7. If modulus approaches 10^{18}, `(a * b)` may still overflow `long`; use safe multiplication or `BigInteger`.

### Edge Cases Checklist
*   `Gcd(0, 0)` is undefined mathematically; implementation usually returns `0` by convention.
*   `Lcm(a, b)` is `0` if either input is `0`.
*   Sieve needs explicit handling for `limit < 2`.
*   Modular inverse exists only if gcd(a, m) = 1.

---

## 📘 Chapter 5: Integration and Mastery

### 1. Pattern Selection Rules
*   *Use the Euclidean Algorithm when*: You need to simplify fraction ratios, check coprime relations, or compute periodicities.
*   *Use Trial Division when*: You need primality for one or few numbers.
*   *Use the Sieve of Eratosthenes when*: You need to handle dense primality checks on numbers smaller than 10^7 repeatedly.
*   *Use Modular Multiplicative Inverses when*: You need division under modulo arithmetic, such as C(N, K) \bmod P.

### 2. Beginner to Advanced Progression
1. Beginner: implement `Gcd`, `Lcm`, and single-number prime check.
2. Intermediate: implement sieve and fast modular exponentiation with tests.
3. Advanced: combine factorial precompute + modular inverse for nCr modulo prime.

---

## 🛠️ Supplementary Material

### Practice Problems
1.  **Count Primes**: Find the number of primes smaller than N.
2.  **Modular Combinations**: Calculate combinations C(N, K) \bmod P.
3.  **Euclidean GCD recursion**: Implement recursive Greatest Common Divisor checks.
4.  **Greatest Common Divisor of Strings**: Generalize numeric GCD to string repeating substrings.
5.  **Modular Inverse (Composite Modulus)**: Use Extended Euclid to compute inverse where possible.

### Misconceptions and Corrections
*   *Incorrect Idea*: Attempting to calculate modular inverses modulo composite numbers using Fermat's Little Theorem.
    *   *Correction*: Fermat's Little Theorem only applies to prime moduli. For composite moduli, you must use the **Extended Euclidean Algorithm** to find the inverse.
*   *Incorrect Idea*: Assuming `(a * b) % m` is always safe with `long`.
    *   *Correction*: It is safe for common small moduli, but for very large moduli you must use overflow-safe multiplication.

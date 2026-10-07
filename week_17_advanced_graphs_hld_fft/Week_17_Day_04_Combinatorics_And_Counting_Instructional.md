# 📘 Week 17, Day 4: Combinatorics & Principle of Inclusion-Exclusion

> 🧭 **Navigation:** [← Previous Day](Week_17_Day_03_Game_Theory_Nimbers_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_17_Day_05_Catalan_Numbers_And_Advanced_Counting_Instructional.md)
> 
> 💡 **Instructor Note:** *Combinatorics in senior technical interviews (Meta, Google, Bloomberg, Citadel) evaluates whether a candidate can replace naive exponential backtracking (O(2^N) or O(N!)) with exact closed-form algebraic formulations. A production-ready Modular Combinatorics engine precomputes factorials and modular inverse factorials in O(N) time, answering any nCr query in O(1).*

---

## 🎯 Learning Objectives

*   **Modular Arithmetic Foundations:** Master division under prime modulus `MOD = 10^9 + 7` using Fermat's Little Theorem: `a^(-1) = a^(MOD - 2) mod MOD`.
*   **O(N) Precomputation with Backward Inversion:** Precompute all factorials and inverse factorials in linear time by calculating `invFact[N]` once and cascading downward.
*   **Stars and Bars Partitioning:** Solve indistinguishable item allocation problems into distinct bins: `(n + k - 1 choose k - 1)`.
*   **Derangements Formulation:** Count fixed-point-free permutations using both linear recurrences and the Principle of Inclusion-Exclusion (PIE).
*   **Principle of Inclusion-Exclusion (PIE):** Systematically compute the size of set unions via bitmask parity aggregation.
*   **Dual-Language Fluency:** Deliver clean, zero-allocation modular combinatorics engines in modern C# (.NET 8/9) and idiomatic Python (3.11+).

---

## ⚖️ FAANG Senior / Lead Interview Calibration

Counting problems appear frequently in Big Tech rounds. Senior candidates navigate the complexity spectrum:

| Problem Formulation | Naive Approach | Optimal Formulation | Time Complexity | Interview Requirement |
| :--- | :--- | :--- | :--- | :--- |
| **Calculate `nCr mod P` for `Q` queries** | Recompute on the fly (`O(Q * N)`) | Precompute `fact` and `invFact` | `O(N + Q)` | **Full code expected in 10 minutes.** |
| **Distribute items into bins** | Backtracking recursion `O(k^n)` | Stars and Bars formula | `O(1)` query | Mathematical recognition expected instantly. |
| **Permutations with no fixed points** | Permutation generation `O(N!)` | Derangements recurrence | `O(N)` | Derivation via PIE expected on whiteboard. |
| **Count arrays satisfying constraints** | Exponential DP `O(2^N)` | Principle of Inclusion-Exclusion | `O(2^K)` (where `K` = constraints) | Senior / Lead standard for constraint counting. |

### The "Fermat's Little Theorem" Trap
Many candidates know that `inv(x) = pow(x, MOD - 2, MOD)`. However, computing the inverse for every factorial individually takes `O(N log MOD)`.
A Senior candidate demonstrates **backward prefix cascading**:
```text
Compute fact[i] forward in O(N).
Compute invFact[N] = pow(fact[N], MOD - 2, MOD) in O(log MOD).
For i = N - 1 down to 0:
    invFact[i] = (invFact[i + 1] * (i + 1)) mod MOD
```
This reduces modular inversions from `N` exponentiations down to **exactly 1 exponentiation**, achieving true **`O(N)` linear precomputation**.

---

## 📖 Chapter 1: Context & Physical Mental Models

### 1. Stars and Bars (Balls in Bins)

Suppose you have `n = 7` identical items (stars) to distribute among `k = 3` distinct bins.
Represent the partition by placing `k - 1 = 2` divider bars among the 7 stars:

```text
Bin 1: 3 items      Bin 2: 2 items      Bin 3: 2 items

     *   *   *    |    *   *    |    *   *
    (Star Star Star)   (Star Star)  (Star Star)
```

*   Total positions available: `n + k - 1` (7 stars + 2 bars = 9 positions).
*   Any choice of 2 bar positions defines a unique distribution.
*   **Formula (Non-negative integers `x_i >= 0`):**
    `Total Ways = (n + k - 1 choose k - 1)`.
*   **Formula (Positive integers `x_i >= 1`):**
    Pre-assign 1 item to each bin, leaving `n - k` items to distribute:
    `Total Ways = (n - 1 choose k - 1)`.

### 2. The Principle of Inclusion-Exclusion (PIE)

To find the size of the union of multiple sets, we cannot simply add their sizes because elements in overlaps are overcounted:

```text
|A U B U C| = |A| + |B| + |C|                  <-- Level 1: Add single sets (+)
              - (|A cap B| + |A cap C| + |B cap C|)  <-- Level 2: Subtract pairwise overlaps (-)
              + |A cap B cap C|                <-- Level 3: Add triple overlap (+)
```

General Formula:
`|Union_{i=1}^m A_i| = Sum_{k=1}^m (-1)^{k-1} * Sum_{1 <= i_1 < ... < i_k <= m} |A_{i_1} cap ... cap A_{i_k}|`.

---

## 🏛️ Chapter 2: The Governing Invariants & Algorithmic Mechanics

### 1. Mathematical Invariants

1. **Modular Inverse Correctness:**
   By Fermat's Little Theorem, if `MOD` is prime and `gcd(a, MOD) == 1`:
   `a^(MOD - 1) = 1 (mod MOD)` -> `a * a^(MOD - 2) = 1 (mod MOD)`.
   Therefore, `a^(-1) = a^(MOD - 2) mod MOD`.
2. **Combination Formula:**
   `nCr(n, r) = n! / (r! * (n - r)!) = fact[n] * invFact[r] * invFact[n - r] mod MOD`.
3. **Derangement Recurrence:**
   Let `D(n)` be the number of derangements of `n` items:
   - `D(0) = 1`, `D(1) = 0`.
   - `D(n) = (n - 1) * (D(n - 1) + D(n - 2))` for `n >= 2`.
   - Closed form via PIE: `D(n) = n! * Sum_{i=0}^n ((-1)^i / i!)`.

---

## 💻 Chapter 3: Idiomatic Dual-Language Code

### C# Primary Implementation (.NET 8/9)

```csharp
using System;

namespace AdvancedDataStructures;

/// <summary>
/// Production modular combinatorics engine.
/// Precomputes factorials and inverse factorials in O(N) to answer nCr and nPr queries in O(1).
/// </summary>
public sealed class ModularCombinatorics
{
    public const int Mod = 1_000_000_007;
    private readonly long[] _fact;
    private readonly long[] _invFact;
    private readonly int _maxN;

    public ModularCombinatorics(int maxN)
    {
        _maxN = maxN;
        _fact = new long[maxN + 1];
        _invFact = new long[maxN + 1];

        _fact[0] = 1;
        _invFact[0] = 1;

        // Step 1: Forward precomputation of factorials in O(N)
        for (int i = 1; i <= maxN; i++)
        {
            _fact[i] = (_fact[i - 1] * i) % Mod;
        }

        // Step 2: Single modular exponentiation for invFact[maxN] in O(log Mod)
        _invFact[maxN] = ModPow(_fact[maxN], Mod - 2, Mod);

        // Step 3: Backward cascading precomputation of inverse factorials in O(N)
        for (int i = maxN - 1; i >= 1; i--)
        {
            _invFact[i] = (_invFact[i + 1] * (i + 1)) % Mod;
        }
    }

    public static long ModPow(long baseVal, long exp, long mod)
    {
        long result = 1;
        baseVal %= mod;
        while (exp > 0)
        {
            if ((exp & 1) == 1) result = (result * baseVal) % mod;
            baseVal = (baseVal * baseVal) % mod;
            exp >>= 1;
        }
        return result;
    }

    /// <summary>
    /// Computes nCr modulo 10^9 + 7 in O(1) time.
    /// </summary>
    public long NCr(int n, int r)
    {
        if (r < 0 || r > n || n > _maxN) return 0;
        return _fact[n] * _invFact[r] % Mod * _invFact[n - r] % Mod;
    }

    /// <summary>
    /// Computes nPr modulo 10^9 + 7 in O(1) time.
    /// </summary>
    public long NPr(int n, int r)
    {
        if (r < 0 || r > n || n > _maxN) return 0;
        return _fact[n] * _invFact[n - r] % Mod;
    }

    /// <summary>
    /// Stars and Bars: Number of ways to distribute n identical items into k distinct bins.
    /// Non-negative solutions: x1 + x2 + ... + xk = n (xi >= 0).
    /// </summary>
    public long StarsAndBars(int n, int k)
    {
        if (n < 0 || k <= 0) return 0;
        return NCr(n + k - 1, k - 1);
    }

    /// <summary>
    /// Computes the number of derangements of n elements in O(N) time.
    /// </summary>
    public static long Derangements(int n)
    {
        if (n == 0) return 1;
        if (n == 1) return 0;

        long prev2 = 1; // D(0)
        long prev1 = 0; // D(1)
        long curr = 0;

        for (int i = 2; i <= n; i++)
        {
            curr = ((i - 1) * (prev1 + prev2)) % Mod;
            prev2 = prev1;
            prev1 = curr;
        }

        return curr;
    }
}
```

### Python Secondary Implementation (Python 3.11+)

```python
from typing import List

class ModularCombinatorics:
    """Precomputes factorials and modular inverse factorials for O(1) nCr queries."""
    MOD: int = 1_000_000_007

    def __init__(self, max_n: int):
        self.max_n = max_n
        self.fact: List[int] = [1] * (max_n + 1)
        self.inv_fact: List[int] = [1] * (max_n + 1)

        # Forward factorial precomputation
        for i in range(1, max_n + 1):
            self.fact[i] = (self.fact[i - 1] * i) % self.MOD

        # Single modular exponentiation at max_n
        self.inv_fact[max_n] = pow(self.fact[max_n], self.MOD - 2, self.MOD)

        # Backward cascade for inverse factorials
        for i in range(max_n - 1, 0, -1):
            self.inv_fact[i] = (self.inv_fact[i + 1] * (i + 1)) % self.MOD

    def ncr(self, n: int, r: int) -> int:
        """Returns nCr modulo 10^9 + 7 in O(1) time."""
        if r < 0 or r > n or n > self.max_n:
            return 0
        return self.fact[n] * self.inv_fact[r] % self.MOD * self.inv_fact[n - r] % self.MOD

    def npr(self, n: int, r: int) -> int:
        """Returns nPr modulo 10^9 + 7 in O(1) time."""
        if r < 0 or r > n or n > self.max_n:
            return 0
        return self.fact[n] * self.inv_fact[n - r] % self.MOD

    def stars_and_bars(self, n: int, k: int) -> int:
        """Distributes n identical items into k distinct non-negative bins."""
        if n < 0 or k <= 0:
            return 0
        return self.ncr(n + k - 1, k - 1)

def derangements(n: int, mod: int = 1_000_000_007) -> int:
    """Computes number of derangements of n elements in O(N) time and O(1) space."""
    if n == 0:
        return 1
    if n == 1:
        return 0

    prev2, prev1 = 1, 0
    for i in range(2, n + 1):
        curr = ((i - 1) * (prev1 + prev2)) % mod
        prev2, prev1 = prev1, curr
    return prev1
```

---

## 📊 Chapter 4: Explicit Complexity Deconstruction

| Operation | Time Complexity | Auxiliary Space | Bottleneck & Invariant |
| :--- | :--- | :--- | :--- |
| **Factorial Array Precompute** | `O(N)` | `O(N)` | Simple forward multiplications. |
| **`invFact[N]` Calculation** | `O(log MOD)` | `O(1)` | Binary exponentiation `pow(x, MOD - 2)`. |
| **Inverse Cascade Precompute** | `O(N)` | `O(N)` | Backward multiplications: `invFact[i] = invFact[i+1] * (i+1)`. |
| **Single `nCr` Query** | `O(1)` | `O(1)` | Three array lookups and two modular multiplications. |
| **Derangements `D(N)`** | `O(N)` | `O(1)` | Two-variable rolling DP state machine. |
| **Bitmask PIE over `M` sets** | `O(2^M)` | `O(1)` | Exhaustive subset traversal using sign `(-1)^|S|`. |

### Mathematical Proof: Why Backward Cascade Works
*   By definition: `invFact[i] = 1 / (i!)`.
*   Notice: `1 / (i!) = (i + 1) / ((i + 1)!) = (i + 1) * invFact[i + 1]`.
*   Taking this modulo `MOD` preserves exact field inverses because `gcd(i + 1, MOD) == 1`.
*   Thus, starting at `invFact[N]` and multiplying by `(i + 1)` moving down to 1 correctly produces every inverse factorial without performing expensive `O(log MOD)` inversions for each element.

---

## 🎙️ Chapter 5: 45-Minute Senior / Lead Verbal Interview Script

```text
[00:00 - 05:00] Clarify & Problem Modeling
Candidate: "We are asked to answer Q queries computing combinations nCr modulo 10^9 + 7 where N <= 10^6 and Q <= 10^5.
           If we compute each combination naively using modular inverses on the fly, each query takes O(N log MOD), 
           leading to O(Q * N log MOD)—which will severely timeout.
           Instead, I will build an O(N) precomputation engine: factorials and modular inverse factorials. 
           Once precomputed, every subsequent combination query executes in O(1) time."

[05:00 - 15:00] Invariants & Trade-offs
Candidate: "Since MOD = 10^9 + 7 is prime, Fermat's Little Theorem states a^(MOD - 1) = 1 (mod MOD), 
           so the modular inverse is a^(MOD - 2) mod MOD.
           A common pitfall is calling modular inverse on every element, taking O(N log MOD). 
           I will optimize this to true O(N):
           1. Compute fact[0...N] in O(N).
           2. Invert ONLY the last element: invFact[N] = pow(fact[N], MOD - 2) in O(log MOD).
           3. Cascade backward: invFact[i] = invFact[i + 1] * (i + 1) mod MOD in O(N).
           Now nCr(n, r) is simply fact[n] * invFact[r] * invFact[n - r] mod MOD in O(1)."

[15:00 - 35:00] Implementation Protocol
Candidate: "I'll implement the `ModularCombinatorics` class:
           - Verify bounds: `if (r < 0 || r > n) return 0;`.
           - In `ModPow`: use standard binary exponentiation.
           - Implement `StarsAndBars(n, k)`: mapping to `NCr(n + k - 1, k - 1)` for distributing n identical 
             items among k bins.
           - Implement `Derangements(n)`: using the O(1) memory two-pointer recurrence D(n) = (n - 1)*(D(n-1) + D(n-2))."

[35:00 - 45:00] Complexity Deconstruction & Edge Cases
Candidate: "Precomputation takes O(N + log MOD) time and O(N) space. Each query is O(1).
           Edge cases:
           - r = 0 or r = n: yields 1.
           - r > n: guard clause returns 0 immediately.
           - 64-bit integer overflow: intermediate products in C# and Python must be typed to prevent 
             32-bit register overflow prior to modulo."
```

---

## 🔍 Chapter 6: Granular Edge-Case & Invariant Verification Table

| Edge Case Scenario | Concrete Input | Expected Behavior | Invariant Verification & Handling |
| :--- | :--- | :--- | :--- |
| **`r = 0` (Choose 0 elements)** | `nCr(10, 0)` | Returns `1`. | `fact[10] * invFact[0] * invFact[10] = 1`. |
| **`r = n` (Choose all elements)**| `nCr(10, 10)` | Returns `1`. | `fact[10] * invFact[10] * invFact[0] = 1`. |
| **`r > n` (Invalid combination)** | `nCr(5, 8)` | Returns `0`. | Guard clause `if (r > n) return 0;` prevents out-of-bounds array access. |
| **`r < 0` (Negative index)** | `nCr(5, -2)` | Returns `0`. | Guard clause `if (r < 0) return 0;`. |
| **Stars & Bars with `n = 0`** | `StarsAndBars(0, 5)` | Returns `1`. | `NCr(0 + 5 - 1, 4) = NCr(4, 4) = 1` (1 way: all bins empty). |
| **Single Item Derangement** | `Derangements(1)` | Returns `0`. | A single item cannot be placed anywhere other than position 1. |
| **Zero Item Derangement** | `Derangements(0)` | Returns `1`. | Vacuously true: the empty permutation has no fixed points. |

---

> 🧭 **Navigation:** [← Previous Day](Week_17_Day_03_Game_Theory_Nimbers_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_17_Day_05_Catalan_Numbers_And_Advanced_Counting_Instructional.md)

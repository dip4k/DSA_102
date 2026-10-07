# 📘 Week 18, Day 4: Probabilistic Data Structures in Distributed Systems

> 🧭 **Navigation:** [← Previous Day](Week_18_Day_03_Heavy_Light_Decomposition_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_18_Day_05_Algorithmic_Systems_Design_Instructional.md)
> 
> 💡 **Instructor Note:** *Probabilistic data structures trade 100% precision for exponential space savings. Master the mathematical derivations of Bloom Filter false positives, Count-Min Sketch error bounds, and HyperLogLog cardinality estimation.*

---

## 🎯 Learning Objectives

*   **Core Mental Model:** Understand how hashing with controlled collisions enables constant-memory membership testing, frequency tracking, and cardinality estimation at massive scale.
*   **Exact Mathematical Bounds:**
    *   **Bloom Filter:** Derive false positive rate `p = (1 - e^(-kn/m))^k` and optimal hash count `k = (m/n) * ln(2)`.
    *   **Count-Min Sketch:** Derive width `w = ceil(e / epsilon)` and depth `d = ceil(ln(1 / delta))` with guarantee `a_hat <= a + epsilon * ||a||_1`.
    *   **HyperLogLog:** Derive standard error `SE = 1.04 / sqrt(m)` using harmonic mean estimation across `m = 2^b` registers.
*   **Production Systems Anchors:** Ground these structures in production engines: Apache Cassandra/RocksDB SSTable bloom filters, Redis `PFADD`/`PFCOUNT`, and Cloudflare/TinyLFU edge admission caches.
*   **Algorithmic Protocol:** Implement complete, robust engines in modern C# (.NET 8/9) and Python (3.11+).

## ⚖️ FAANG Senior / Lead Interview Calibration

In Tier-1 distributed systems design (L5/L6/L7), choosing the exact probabilistic primitive differentiates a senior engineer who builds scalable systems from a junior developer who runs out of RAM:

| Probabilistic Primitive | Core Question Answered | False Outcomes | Memory Footprint (10^8 items) | Key Architectural Anchor | Interview Calibration Bar |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Exact Hash Set** | Membership | Zero Error (100% exact) | ~3.2 GB RAM | In-memory Redis Set | Baseline; rejected when scale hits millions of keys. |
| **Standard Bloom Filter** | Membership | False Positives (`p`), Zero False Negatives | ~120 MB RAM (`~10` bits/item) | **Apache Cassandra / RocksDB SSTables** | **Mandatory L5/L6 anchor: derive `p = (1 - e^(-kn/m))^k`.** |
| **Cuckoo Filter** | Membership with Deletions | False Positives (`p`), Zero False Negatives | ~96 MB RAM | High-churn edge caches | Mention when deletes are required (Bloom cannot delete). |
| **Count-Min Sketch** | Frequency / Heavy Hitters | Overestimates only (`a_hat >= a`), Zero Underestimation | ~50 KB - 2 MB RAM | **Cloudflare TinyLFU / CDN Edge Caches** | **L6 Systems Bar: derive `w = ceil(e/eps)`, `d = ceil(ln(1/delta))`.** |
| **HyperLogLog (HLL)** | Cardinality (Count Distinct) | Relative Error `SE = 1.04 / sqrt(m)` | **12 KB total** | **Redis `PFADD` / Google BigQuery DAU** | **L6 Systems Bar: explain harmonic mean & register bit hacks.** |

### The Recognition Pattern: "Which Probabilistic DS Do I Select?"
1. **"Does key `K` exist on disk?"** -> **Bloom Filter**. (Guarantees zero false negatives: if filter says NO, never touch disk SSTable).
2. **"How often was URL `U` requested in the last hour?"** -> **Count-Min Sketch**. (Prevents caching one-hit wonders; fixed memory independent of key volume).
3. **"How many unique IP addresses visited our platform today?"** -> **HyperLogLog**. (12 KB fixed memory handles 10 billion distinct visitors with ~1% error).

---

## 📖 Chapter 1: Context & Motivation: The High-Volume Data Challenge

### 1. The Engineering Challenge: Memory Wall at 100 Million Elements

Consider high-throughput distributed systems:
1. **Database Key Lookup (Cassandra / RocksDB):** Storing 1 billion 64-bit keys in a hash table requires at least 32 GB of RAM. When querying a non-existent key, checking disk SSTables incurs expensive NVMe random I/O.
2. **Frequency Monitoring (Akamai / Cloudflare CDN):** Over 70% of cached web requests are "one-hit wonders" never requested again. Caching every object evicts valuable hot content. Tracking exact counts with a hash map crashes under billions of distinct URLs.
3. **Unique Daily Active Users (Redis / BigQuery):** Counting unique user IDs from a stream of 500 million daily events requires gigabytes of set storage per metric dimension.

**Probabilistic Data Structures** solve these problems using fractional memory:

| Structure | Query Question | Memory | Guarantee | Production System |
| :--- | :--- | :--- | :--- | :--- |
| **Bloom Filter** | *Is item `x` in the set?* | ~10 bits / item | Zero False Negatives, tunable false positive `p` | Cassandra SSTables, Google Chrome Safe Browsing |
| **Count-Min Sketch** | *How many times did `x` appear?* | Fixed matrix `w * d` | Overestimation bounded by `epsilon * N`, never underestimates | Cloudflare TinyLFU admission, Network packet heavy-hitters |
| **HyperLogLog** | *How many distinct items exist?* | 12 KB total | Relative standard error `~1%` | Redis `PFCOUNT`, Google BigQuery `APPROX_COUNT_DISTINCT` |

---

### 2. Physical Layouts & Bit Encodings

#### Bloom Filter Bit Array
```
Item "user_942" --+--> Hash 1 mod 16 = 3  --+
                  +--> Hash 2 mod 16 = 7  --+---> Set bits [3, 7, 12] to 1
                  +--> Hash 3 mod 16 = 12 -+

Bit Array (m = 16 bits):
Index:  0   1   2   3   4   5   6   7   8   9  10  11  12  13  14  15
Bits:  [0] [0] [0] [1] [0] [0] [0] [1] [0] [0] [0] [0] [1] [0] [0] [0]
                    ^               ^                   ^
                    h1              h2                  h3
Query: If ANY probed bit is 0 -> Item is GUARANTEED not in set (0% False Negative).
       If ALL probed bits are 1 -> Item is PROBABLY in set (p False Positive).
```

#### Count-Min Sketch 2D Matrix
```
Depth d = 4 rows (4 independent hash functions)
Width w = 8 columns
         col 0   col 1   col 2   col 3   col 4   col 5   col 6   col 7
Row 0:  [  12  ] [   0  ] [  45  ] [   8  ] [  91  ] [   3  ] [   0  ] [  14  ]  <- h0(x)
Row 1:  [   5  ] [  78  ] [   2  ] [  31  ] [   0  ] [  18  ] [  42  ] [   7  ]  <- h1(x)
Row 2:  [  88  ] [  19  ] [   4  ] [   0  ] [  15  ] [  64  ] [   1  ] [  22  ]  <- h2(x)
Row 3:  [   0  ] [  33  ] [  11  ] [  92  ] [   6  ] [  12  ] [   5  ] [  49  ]  <- h3(x)

Point Query: Frequency(x) = min(Row 0[h0(x)], Row 1[h1(x)], Row 2[h2(x)], Row 3[h3(x)])
Minimum operation suppresses collision noise across rows!
```

#### HyperLogLog Register Array
```
Hash(x) = 64-bit integer
+------------------------+------------------------------------------------------+
| Register Index: b bits | Remaining Bits: Look for position of first '1' bit   |
| (Determines bucket j)  | Example: 00000101... -> 5 leading zeros -> Rank = 6  |
+------------------------+------------------------------------------------------+

Registers M[0 ... m-1] where m = 2^b (e.g., b = 14 -> 16,384 registers):
Register: [ 0 ] [ 1 ] [ 2 ] ... [ j ] ... [ 16383 ]
Value:    [ 4 ] [ 2 ] [ 7 ] ... [ 6 ] ... [   3   ]  (Stores max rank seen)
Memory: 16,384 registers * 6 bits = 12,288 bytes = 12 KB total!
```

---

## 🏛️ Chapter 2: Exact Mathematical Bounds & Formulations

### 1. Bloom Filter Mathematical Invariants
- **False Positive Probability Derivation:**
  Given bit array of size `m`, `n` inserted elements, and `k` independent uniform hash functions:
  Probability a specific bit is NOT set by a hash function: `1 - 1/m`.
  Probability a bit is NOT set after inserting `n` items with `k` hashes: `(1 - 1/m)^(k * n) approx e^(-kn/m)`.
  Probability that a bit IS set: `1 - e^(-kn/m)`.
  Probability that all `k` selected bits for a query are set (False Positive `p`):
  ```
  p = (1 - e^(-kn/m))^k
  ```
- **Optimal Number of Hash Functions `k`:**
  To minimize `p` for given `m` and `n`, taking the natural log and differentiating yields:
  ```
  k = (m / n) * ln(2) approx 0.693 * (m / n)
  ```
- **Required Bit Array Size `m`:**
  Substituting optimal `k` back into `p = (1/2)^k`:
  ```
  m = - (n * ln(p)) / (ln(2)^2) approx 1.44 * n * log2(1 / p)
  ```
  *Rule of thumb:* For `p = 0.01` (1% false positive), `m / n approx 9.6` bits per element, and `k = 7`.
- **Kirsch-Mitzenmacher Double Hashing Theorem:**
  Generating `k` hashes via two independent 64-bit hashes `h1(x)` and `h2(x)`:
  ```
  g_i(x) = (h1(x) + i * h2(x)) mod m
  ```
  Simulates `k` independent hashes with asymptotic equivalence to truly independent hash functions.

### 2. Count-Min Sketch Error Bounds
- Given user-specified error factor `epsilon` and failure probability `delta`:
  - **Width:** `w = ceil(e / epsilon)` where `e approx 2.71828`
  - **Depth:** `d = ceil(ln(1 / delta))`
- **Estimation Guarantee (Markov's Inequality):**
  For stream length `N = ||a||_1 = sum(frequencies)`:
  ```
  a(x) <= a_hat(x) <= a(x) + epsilon * N
  ```
  with probability at least `1 - delta`.
- Counters are monotonic (never decrease). Hence collisions only cause overestimation (`a_hat >= a`). Taking the minimum across `d` hash rows yields the tightest upper bound.

### 3. HyperLogLog Cardinality Bounds
- Given `m = 2^b` registers, let `M[j]` be the maximum number of leading zeros plus 1 observed in bucket `j`.
- **Raw Estimate using Normalized Harmonic Mean:**
  ```
  E = alpha_m * m^2 / sum_{j=0}^{m-1} (2^(-M[j]))
  ```
  where `alpha_m` is the bias-correction constant:
  - For `m = 16`, `alpha_16 = 0.673`
  - For `m = 32`, `alpha_32 = 0.697`
  - For `m = 64`, `alpha_64 = 0.709`
  - For `m >= 128`, `alpha_m = 0.7213 / (1 + 1.079 / m)`
- **Standard Error:**
  ```
  Standard Error (SE) = 1.04 / sqrt(m)
  ```
  For `b = 14` (`m = 16,384` registers), `SE = 1.04 / 128 approx 0.81%`.

---

## 💻 Chapter 3: Idiomatic Dual-Language Implementations

### C# Primary Implementation (.NET 8/9)

```csharp
using System;
using System.Collections;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;

namespace AdvancedAlgorithms;

// =============================================================================
// 1. Production Bloom Filter with Kirsch-Mitzenmacher Double Hashing
// =============================================================================
public sealed class BloomFilter
{
    private readonly BitArray _bits;
    private readonly int _m;
    private readonly int _k;

    public BloomFilter(int expectedElements, double falsePositiveRate = 0.01)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(expectedElements);
        if (falsePositiveRate <= 0.0 || falsePositiveRate >= 1.0)
            throw new ArgumentOutOfRangeException(nameof(falsePositiveRate), "Rate must be in (0, 1).");

        // m = - (n * ln(p)) / (ln(2)^2)
        _m = (int)Math.Ceiling(-expectedElements * Math.Log(falsePositiveRate) / (Math.Log(2) * Math.Log(2)));
        // k = (m / n) * ln(2)
        _k = Math.Max(1, (int)Math.Round(((double)_m / expectedElements) * Math.Log(2)));
        _bits = new BitArray(_m);
    }

    private static (ulong h1, ulong h2) Hash128(string key)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(key);
        byte[] hash = SHA256.HashData(bytes);
        ulong h1 = BitConverter.ToUInt64(hash, 0);
        ulong h2 = BitConverter.ToUInt64(hash, 8);
        return (h1, h2 == 0 ? 1UL : h2);
    }

    public void Add(string item)
    {
        ArgumentNullException.ThrowIfNull(item);
        var (h1, h2) = Hash128(item);

        for (int i = 0; i < _k; i++)
        {
            int idx = (int)((h1 + (ulong)i * h2) % (ulong)_m);
            _bits[idx] = true;
        }
    }

    public bool MightContain(string item)
    {
        ArgumentNullException.ThrowIfNull(item);
        var (h1, h2) = Hash128(item);

        for (int i = 0; i < _k; i++)
        {
            int idx = (int)((h1 + (ulong)i * h2) % (ulong)_m);
            if (!_bits[idx]) return false; // 100% Guaranteed NOT in set
        }

        return true; // Probably in set
    }
}

// =============================================================================
// 2. Count-Min Sketch for High-Throughput Frequency Estimation
// =============================================================================
public sealed class CountMinSketch
{
    private readonly int _width;
    private readonly int _depth;
    private readonly long[][] _table;
    private readonly ulong[] _seeds;

    public CountMinSketch(double epsilon = 0.001, double delta = 0.01)
    {
        // width w = ceil(e / epsilon), depth d = ceil(ln(1 / delta))
        _width = (int)Math.Ceiling(Math.E / epsilon);
        _depth = (int)Math.Ceiling(Math.Log(1.0 / delta));
        _table = new long[_depth][];
        for (int i = 0; i < _depth; i++) _table[i] = new long[_width];

        _seeds = new ulong[_depth];
        var rng = new Random(42);
        for (int i = 0; i < _depth; i++) _seeds[i] = ((ulong)rng.Next() << 32) | (uint)rng.Next();
    }

    private int HashRow(string key, int row)
    {
        ulong hash = _seeds[row];
        foreach (char c in key)
        {
            hash = (hash ^ c) * 1099511628211UL; // 64-bit FNV-1a
        }
        return (int)(hash % (ulong)_width);
    }

    public void Add(string key, long count = 1)
    {
        for (int r = 0; r < _depth; r++)
        {
            int c = HashRow(key, r);
            _table[r][c] += count;
        }
    }

    public long EstimateFrequency(string key)
    {
        long minCount = long.MaxValue;
        for (int r = 0; r < _depth; r++)
        {
            int c = HashRow(key, r);
            minCount = Math.Min(minCount, _table[r][c]);
        }
        return minCount;
    }
}

// =============================================================================
// 3. HyperLogLog Cardinality Estimator
// =============================================================================
public sealed class HyperLogLog
{
    private readonly int _b; // Precision bits
    private readonly int _m; // Number of registers 2^b
    private readonly byte[] _registers;
    private readonly double _alphaM;

    public HyperLogLog(int b = 14) // Default b = 14 -> 16,384 registers, ~0.81% error
    {
        if (b is < 4 or > 16) throw new ArgumentOutOfRangeException(nameof(b), "b must be between 4 and 16.");
        _b = b;
        _m = 1 << b;
        _registers = new byte[_m];

        _alphaM = _m switch
        {
            16 => 0.673,
            32 => 0.697,
            64 => 0.709,
            _ => 0.7213 / (1.0 + 1.079 / _m)
        };
    }

    public void Add(string item)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(item);
        ulong hash = BitConverter.ToUInt64(SHA256.HashData(bytes), 0);

        // First b bits determine bucket index
        int bucket = (int)(hash >> (64 - _b));

        // Remaining 64 - b bits: count leading zeros + 1
        ulong remainingBits = (hash << _b) | (1UL << (_b - 1)); // sentinel
        int leadingZeros = BitOperations.LeadingZeroCount(remainingBits);
        byte rank = (byte)(leadingZeros + 1);

        if (rank > _registers[bucket])
        {
            _registers[bucket] = rank;
        }
    }

    public long EstimateCardinality()
    {
        double harmonicSum = 0.0;
        int zeroRegisters = 0;

        for (int j = 0; j < _m; j++)
        {
            byte val = _registers[j];
            if (val == 0) zeroRegisters++;
            harmonicSum += Math.Pow(2.0, -val);
        }

        double estimate = _alphaM * _m * _m / harmonicSum;

        // Small range correction (Linear Counting) when estimate <= 2.5 * m
        if (estimate <= 2.5 * _m && zeroRegisters > 0)
        {
            estimate = _m * Math.Log((double)_m / zeroRegisters);
        }

        return (long)Math.Round(estimate);
    }
}
```

---

### Python Secondary Implementation (Python 3.11+)

```python
import math
import hashlib
from typing import List

class BloomFilter:
    """Production Bloom Filter with Kirsch-Mitzenmacher double hashing."""
    def __init__(self, expected_elements: int, false_positive_rate: float = 0.01):
        self.n = expected_elements
        self.p = false_positive_rate
        # m = - (n * ln(p)) / (ln(2)^2)
        self.m = int(math.ceil(-self.n * math.log(self.p) / (math.log(2) ** 2)))
        # k = (m / n) * ln(2)
        self.k = max(1, int(round((self.m / self.n) * math.log(2))))
        self.bit_array = bytearray((self.m + 7) // 8)

    def _get_hashes(self, item: str) -> tuple[int, int]:
        digest = hashlib.sha256(item.encode('utf-8')).digest()
        h1 = int.from_bytes(digest[:8], byteorder='little')
        h2 = int.from_bytes(digest[8:16], byteorder='little')
        return h1, h2 if h2 != 0 else 1

    def add(self, item: str) -> None:
        h1, h2 = self._get_hashes(item)
        for i in range(self.k):
            idx = (h1 + i * h2) % self.m
            self.bit_array[idx // 8] |= (1 << (idx % 8))

    def might_contain(self, item: str) -> bool:
        h1, h2 = self._get_hashes(item)
        for i in range(self.k):
            idx = (h1 + i * h2) % self.m
            if not (self.bit_array[idx // 8] & (1 << (idx % 8))):
                return False
        return True


class CountMinSketch:
    """Frequency estimation using 2D Count-Min matrix."""
    def __init__(self, epsilon: float = 0.001, delta: float = 0.01):
        self.width = int(math.ceil(math.e / epsilon))
        self.depth = int(math.ceil(math.log(1.0 / delta)))
        self.table = [[0] * self.width for _ in range(self.depth)]
        self.seeds = [42 + i * 1337 for i in range(self.depth)]

    def _hash(self, key: str, row: int) -> int:
        h = self.seeds[row]
        for ch in key:
            h = ((h ^ ord(ch)) * 1099511628211) & 0xFFFFFFFFFFFFFFFF
        return h % self.width

    def add(self, key: str, count: int = 1) -> None:
        for r in range(self.depth):
            col = self._hash(key, r)
            self.table[r][col] += count

    def estimate_frequency(self, key: str) -> int:
        return min(self.table[r][self._hash(key, r)] for r in range(self.depth))


class HyperLogLog:
    """HyperLogLog cardinality estimator with linear counting correction."""
    def __init__(self, b: int = 14):
        self.b = b
        self.m = 1 << b
        self.registers = bytearray(self.m)
        if self.m == 16:
            self.alpha = 0.673
        elif self.m == 32:
            self.alpha = 0.697
        elif self.m == 64:
            self.alpha = 0.709
        else:
            self.alpha = 0.7213 / (1.0 + 1.079 / self.m)

    def add(self, item: str) -> None:
        digest = hashlib.sha256(item.encode('utf-8')).digest()
        h = int.from_bytes(digest[:8], byteorder='big')

        # First b bits for register index
        bucket = h >> (64 - self.b)

        # Remaining bits: find leading zeros + 1
        rem = (h << self.b) & 0xFFFFFFFFFFFFFFFF
        leading_zeros = (64 - self.b) if rem == 0 else (64 - self.b - rem.bit_length())
        rank = min(255, leading_zeros + 1)

        if rank > self.registers[bucket]:
            self.registers[bucket] = rank

    def estimate(self) -> int:
        harmonic_sum = sum(2.0 ** (-val) for val in self.registers)
        raw_estimate = self.alpha * (self.m ** 2) / harmonic_sum

        zero_registers = self.registers.count(0)
        if raw_estimate <= 2.5 * self.m and zero_registers > 0:
            raw_estimate = self.m * math.log(self.m / zero_registers)

        return int(round(raw_estimate))
```

---

## 🔬 Chapter 4: Explicit Complexity Deconstruction

| Metric | Bloom Filter | Count-Min Sketch | HyperLogLog |
| :--- | :--- | :--- | :--- |
| **Insert Time** | `O(k)` bit operations | `O(d)` matrix operations | `O(1)` register update |
| **Query Time** | `O(k)` bit tests | `O(d)` row reads | `O(m)` or `O(1)` cached estimate |
| **Auxiliary Memory** | `~1.44 * n * log2(1/p)` bits | `(e / eps) * ln(1/delta) * 8` bytes | `1.04^2 / (SE^2) * 6` bits (~12 KB) |
| **Theoretical Error** | `p = (1 - e^(-kn/m))^k` | `a_hat <= a + eps * N` | `SE = 1.04 / sqrt(m)` |
| **Hardware Locality** | `k` random bit accesses | `d` row buffer accesses | 1 register write per insert |

---

## 🎙️ Chapter 5: 45-Minute Verbal Script & Interview Playbook

```
[00:00 - 05:00] Clarification & Architectural Constraints
"We need to verify key existence across 1 billion items under strict memory constraints.
Storing 1B 64-bit IDs in a hash set requires 16-32 GB RAM.
If we can tolerate a 1% false positive rate while maintaining a strict 0% false negative guarantee,
a Bloom Filter is the optimal probabilistic primitive."

[05:00 - 15:00] Mathematical Derivation & Trade-Offs
"Let m be bit size, n be element count, and k be hash functions.
- The probability that a bit remains 0 after n insertions is (1 - 1/m)^(k*n) approx e^(-kn/m).
- False positive probability is p = (1 - e^(-kn/m))^k.
- Solving for minimal p yields optimal k = (m/n) * ln(2).
- For p = 0.01 (1%), m = 9.6 bits per item, and k = 7.
- For 1 billion items: 9.6 billion bits = 1.2 GB of RAM! A 96% memory reduction over a hash set.
I will also employ the Kirsch-Mitzenmacher optimization: g_i(x) = (h1(x) + i * h2(x)) mod m,
avoiding the cost of computing 7 independent cryptographic hashes."

[15:00 - 32:00] Live Implementation & Guard Clauses
- Build the class with explicit constructor assertions on falsePositiveRate in (0, 1).
- Implement double hashing using 64-bit halves from SHA-256 or FNV-1a.
- Provide Add and MightContain with exact bitwise masking.

[32:00 - 40:00] System Failure Modes & Edge Cases
- Saturation: What happens if n exceeds expected capacity? False positive rate degrades towards 100%.
- Deletions: Standard Bloom filters do not support deletions (clearing a bit might erase other keys).
  Explain Counting Bloom Filters or Cuckoo Filters if deletions are required.

[40:00 - 45:00] Real-World Distributed Systems Anchor
- Cassandra / RocksDB: Bloom filters sit in memory for each SSTable. If MightContain returns false,
  the database skips the disk seek entirely, preserving NVMe read IOPS.
- Redis HyperLogLog (PFADD/PFCOUNT): 12 KB per key tracks millions of unique users with ~0.81% error.
- TinyLFU in Cache Systems (Caffeine, Cloudflare): Count-Min Sketch estimates frequency to reject one-hit wonders."
```

---

## 🔍 Chapter 6: Edge-Case Verification

| Test Case | Scenario | Expected Outcome | Invariant Handling |
| :--- | :--- | :--- | :--- |
| **Item Not Inserted** | Probe key never added | Returns `false` (with `1 - p` probability) | Any bit `0` terminates immediately; returns `false`. |
| **Item Inserted** | Probe key previously added | **100% Guaranteed** `true` | All `k` bits were set; zero false negatives guaranteed. |
| **Over-capacity Saturation** | Inserted `10 * n` items | `true` for almost all queries | Bit array fills with 1s; gracefully degrades without crashing. |
| **Zero Elements Inserted** | Query empty filter | `false` | Bit array is initialized to all `0`s; returns `false`. |
| **Hash Collision in CMS** | Two keys hash to same bucket | Frequency over-estimated by at most `epsilon * N` | Suppressed by taking `min` across independent rows. |

---

> 🧭 **Navigation:** [← Previous Day](Week_18_Day_03_Heavy_Light_Decomposition_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_18_Day_05_Algorithmic_Systems_Design_Instructional.md)

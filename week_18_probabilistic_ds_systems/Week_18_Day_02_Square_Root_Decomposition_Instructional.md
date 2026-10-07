# 📘 Week 18, Day 2: Square Root Decomposition & Mo's Algorithm

> 🧭 **Navigation:** [← Previous Day](Week_18_Day_01_Meet_In_The_Middle_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_18_Day_03_Heavy_Light_Decomposition_Instructional.md)
> 
> 💡 **Instructor Note:** *Square Root Decomposition provides sub-linear range query capabilities with simpler data invariants than Segment Trees. Focus on the mathematical derivation of optimal block size B = N / sqrt(Q) and Mo's algorithm offline query reordering.*

---

## 🎯 Learning Objectives

*   **Core Mental Model:** Understand how partitioning contiguous arrays into `sqrt(N)` blocks transforms arbitrary range queries into `O(sqrt(N))` operations while maintaining `O(1)` point updates.
*   **Mathematical Precision:** Derive the exact pointer movement bounds of Mo's Algorithm: `T(N, Q) = O((N + Q) * sqrt(N))` using optimal block size `B = N / sqrt(Q)` and alternating sort parity.
*   **Production Invariant:** Connect block decomposition to production storage engines: Apache Parquet row-group statistics, ClickHouse granule chunking (8,192 rows), and cache-line spatial locality.
*   **Algorithmic Protocol:** Implement zero-allocation range aggregation and Mo's offline distinct-elements query engine in modern C# (.NET 8/9) and Python (3.11+).

## ⚖️ FAANG Senior / Lead Interview Calibration

When designing range query engines, senior engineers navigate the trade-off space between tree structures and block chunking:

| Range Query Engine | Update Time | Query Time | Offline vs Online | Non-Mergeable Statistics (Mode, Distinct) | Interview Coding Expectation |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Prefix Sum Array** | `O(N)` | `O(1)` | Online | Invertible operations only (sum, XOR) | Expected in 5 mins; static arrays only. |
| **Fenwick Tree (BIT)** | `O(log N)` | `O(log N)` | Online | Invertible operations; prefix-dominated | Expected in 15 mins for point update / range sum. |
| **Segment Tree** | `O(log N)` | `O(log N)` | Online | Associative monoids only (`min`, `max`, `sum`) | Expected in 20-25 mins; high pointer/array overhead. |
| **SQRT Decomposition** | `O(1)` | `O(sqrt(N))` | Online | Extremely flexible; `O(1)` fast updates | Expected in 15 mins when update throughput dominates queries. |
| **Mo's Algorithm** | N/A (Offline) | `O((N + Q) * sqrt(N))` total | Offline | **Any state machine with `O(1)` add/remove** | **Tier-1 L5/L6 bar: full offline engine in 25 mins.** |

### The Recognition Pattern: "When to choose SQRT / Mo's over Trees?"
1. **Black-box non-associative queries:** When range queries ask for the most frequent element (mode), distinct count, or inversion count where subranges cannot be combined in `O(1)` time.
2. **Asymmetric workload (Update-heavy):** In write-heavy telemetry where updates must be `O(1)` and queries are infrequent or can tolerate `O(sqrt(N))` latency.
3. **Offline query batches:** All `Q` queries are available upfront, allowing spatial reordering to minimize sliding window pointer hops.

---

## 📖 Chapter 1: Context & Motivation

### 1. The Engineering Challenge: Trees vs. Blocks

Segment Trees and Fenwick Trees achieve `O(log N)` range queries, but they require strict algebraic properties: the operation must be associative and preferably invertible for Fenwick trees.
What happens when you need to answer offline queries like:
- **Range Mode:** Find the most frequent element in `[L, R]`.
- **Range Distinct Elements:** Count distinct items in `[L, R]`.
- **Range Inversions:** Count pairs `(i, j)` where `i < j` and `A[i] > A[j]`.

These functions cannot be merged in `O(1)` time inside a segment tree node without heavy persistent data structures.

**Square Root Decomposition (and Mo's Algorithm)** re-orders queries offline into blocks of size `B`. By expanding and contracting two sliding pointers `[cur_L, cur_R]`, we transition from query to query in amortized `O(sqrt(N))` steps per query without any complex tree rebalancing.

---

### 2. Physical Layout & Pointer Mechanics

#### Contiguous Block Layout
```
Array of N = 16 elements divided into blocks of B = sqrt(16) = 4:

Block 0: [0, 1, 2, 3]    Block 1: [4, 5, 6, 7]    Block 2: [8, 9, 10, 11]   Block 3: [12, 13, 14, 15]
+---+---+---+---+        +---+---+---+---+        +---+---+---+---+         +---+---+---+---+
| 3 | 1 | 4 | 1 |        | 5 | 9 | 2 | 6 |        | 5 | 3 | 5 | 8 |         | 9 | 7 | 9 | 3 |
+---+---+---+---+        +---+---+---+---+        +---+---+---+---+         +---+---+---+---+
      B_0 = 9                  B_1 = 22                 B_2 = 21                  B_3 = 28

Query Range [L = 2, R = 13]:
  [2, 3]              [4, 5, 6, 7]        [8, 9, 10, 11]            [12, 13]
  Left Partial Block  Full Block B_1      Full Block B_2            Right Partial Block
  (Elements checked   (Direct sum read:   (Direct sum read:         (Elements checked
   individually)       B_1 = 22)           B_2 = 21)                 individually)
```

#### Mo's Algorithm Pointer Sweeping
```
Query 1: [L1 = 2, R1 = 12] -> cur_L = 2, cur_R = 12
Query 2: [L2 = 3, R2 = 14] -> cur_L advances 1 step (cur_L++), cur_R advances 2 steps (cur_R++)

              Left Block k                        Right Pointer Sweep
   +---------------------------------+  ========================================>
   |  L1          L2                 |                                R1      R2
   +---------------------------------+  ========================================>
   <---- Max B moves within block --->  <---- Right pointer only sweeps forward! ->
```

---

## 🏛️ Chapter 2: Mathematical Formulation & Governing Invariants

### 1. Derivation of Optimal Block Size in Mo's Algorithm
Let `N` be the array size, `Q` be the number of queries, and `B` be the block size.
Queries are sorted by:
`Block(L) = floor(L / B)` ascending, then by `R` ascending.

1. **Left Pointer Movement (`cur_L`):**
   - For consecutive queries within the same block, `L` values differ by at most `B`.
   - Across `Q` queries, total `cur_L` movement is at most:
     `Movement(L) = Q * B`
2. **Right Pointer Movement (`cur_R`):**
   - Within a single block of `L`, queries have monotonically non-decreasing `R` values.
   - Therefore, `cur_R` moves from `0` to `N` at most once per block: `N` steps.
   - There are `ceil(N / B)` blocks, so across all blocks:
     `Movement(R) = (N / B) * N = N^2 / B`
3. **Total Pointer Movement:**
   `T(B) = Q * B + N^2 / B`
4. **Minimizing Total Work:**
   To find the critical point, differentiate with respect to `B` and set to zero:
   `d(T) / dB = Q - N^2 / B^2 = 0`
   `B^2 = N^2 / Q  =>  B = N / sqrt(Q)`

When `Q = N`, the optimal block size is `B = sqrt(N)`.
Total operations:
`T(N) = N * sqrt(N) + N^2 / sqrt(N) = 2 * N * sqrt(N) = O((N + Q) * sqrt(N))`

### 2. Alternating Sort Parity Optimization (50% Speedup)
Standard Mo's sorts `R` ascending for every block. When transitioning from block `k` to block `k+1`, `cur_R` can be at `N-1` and must rewind all the way to the start of the next block, wasting `O(N)` steps.

**Optimization:**
- If `Block(L)` is **even**: Sort `R` ascending (`R1 < R2`).
- If `Block(L)` is **odd**: Sort `R` descending (`R1 > R2`).
This forms a serpentine (snake-like) traversal, eliminating pointer rewinding and halving real-world runtime.

---

## 💻 Chapter 3: Idiomatic Dual-Language Implementations

### C# Primary Implementation (.NET 8/9)

```csharp
using System;
using System.Buffers;

namespace AdvancedAlgorithms;

/// <summary>
/// High-performance Square Root Decomposition and Mo's Algorithm suite.
/// </summary>
public static class SqrtEngines
{
    // =========================================================================
    // 1. Classic Sqrt Decomposition for Dynamic Range Sum Queries (O(1) Update, O(sqrt N) Query)
    // =========================================================================
    public sealed class SqrtRangeSum
    {
        private readonly long[] _nums;
        private readonly long[] _blockSums;
        private readonly int _blockSize;
        private readonly int _n;

        public SqrtRangeSum(ReadOnlySpan<long> initialData)
        {
            _n = initialData.Length;
            _blockSize = (int)Math.Max(1, Math.Sqrt(_n));
            int blockCount = (_n + _blockSize - 1) / _blockSize;

            _nums = new long[_n];
            _blockSums = new long[blockCount];

            for (int i = 0; i < _n; i++)
            {
                _nums[i] = initialData[i];
                _blockSums[i / _blockSize] += initialData[i];
            }
        }

        public void Update(int index, long newValue)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, _n);

            long delta = newValue - _nums[index];
            _nums[index] = newValue;
            _blockSums[index / _blockSize] += delta;
        }

        public long Query(int left, int right)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(left);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(right, _n);
            if (left > right) return 0;

            long sum = 0;
            int startBlock = left / _blockSize;
            int endBlock = right / _blockSize;

            if (startBlock == endBlock)
            {
                // Query lies entirely within a single block
                for (int i = left; i <= right; i++) sum += _nums[i];
                return sum;
            }

            // 1. Left partial block
            int leftBlockEnd = (startBlock + 1) * _blockSize - 1;
            for (int i = left; i <= leftBlockEnd; i++) sum += _nums[i];

            // 2. Intermediate full blocks (O(1) per block)
            for (int b = startBlock + 1; b < endBlock; b++) sum += _blockSums[b];

            // 3. Right partial block
            int rightBlockStart = endBlock * _blockSize;
            for (int i = rightBlockStart; i <= right; i++) sum += _nums[i];

            return sum;
        }
    }

    // =========================================================================
    // 2. Mo's Algorithm for Offline Range Distinct Elements (O((N + Q) * sqrt N))
    // =========================================================================
    public readonly record struct Query(int L, int R, int Index, int BlockIndex);

    public static int[] SolveRangeDistinctElements(int[] nums, (int L, int R)[] rawQueries)
    {
        ArgumentNullException.ThrowIfNull(nums);
        ArgumentNullException.ThrowIfNull(rawQueries);

        int n = nums.Length;
        int q = rawQueries.Length;
        if (q == 0) return [];

        int blockSize = (int)Math.Max(1, n / Math.Sqrt(q));
        Query[] queries = new Query[q];

        for (int i = 0; i < q; i++)
        {
            queries[i] = new Query(
                rawQueries[i].L,
                rawQueries[i].R,
                i,
                rawQueries[i].L / blockSize
            );
        }

        // Alternating block sort optimization (Hilbert-like serpentine order)
        Array.Sort(queries, (a, b) =>
        {
            if (a.BlockIndex != b.BlockIndex) return a.BlockIndex.CompareTo(b.BlockIndex);
            // If block index is odd, sort R descending; if even, sort R ascending
            return (a.BlockIndex & 1) == 1 ? b.R.CompareTo(a.R) : a.R.CompareTo(b.R);
        });

        // Find max element for frequency array allocation (or coordinate compress)
        int maxVal = 0;
        foreach (int val in nums) if (val > maxVal) maxVal = val;

        int[] freq = new int[maxVal + 1];
        int distinctCount = 0;
        int[] answers = new int[q];

        int curL = 0;
        int curR = -1;

        void Add(int val)
        {
            if (freq[val] == 0) distinctCount++;
            freq[val]++;
        }

        void Remove(int val)
        {
            freq[val]--;
            if (freq[val] == 0) distinctCount--;
        }

        foreach (var query in queries)
        {
            // Expand or shrink curR to query.R
            while (curR < query.R) Add(nums[++curR]);
            while (curR > query.R) Remove(nums[curR--]);

            // Expand or shrink curL to query.L
            while (curL < query.L) Remove(nums[curL++]);
            while (curL > query.L) Add(nums[--curL]);

            answers[query.Index] = distinctCount;
        }

        return answers;
    }
}
```

---

### Python Secondary Implementation (Python 3.11+)

```python
import math
from typing import Sequence, Tuple, List

class SqrtRangeSum:
    """Classic Square Root Decomposition for dynamic prefix/range sums."""
    def __init__(self, nums: Sequence[int]):
        self.n = len(nums)
        self.nums = list(nums)
        self.block_size = max(1, int(math.isqrt(self.n)))
        num_blocks = (self.n + self.block_size - 1) // self.block_size
        self.block_sums = [0] * num_blocks

        for i, val in enumerate(self.nums):
            self.block_sums[i // self.block_size] += val

    def update(self, index: int, new_val: int) -> None:
        delta = new_val - self.nums[index]
        self.nums[index] = new_val
        self.block_sums[index // self.block_size] += delta

    def query(self, left: int, right: int) -> int:
        if left > right:
            return 0
        b_left = left // self.block_size
        b_right = right // self.block_size

        if b_left == b_right:
            return sum(self.nums[left : right + 1])

        total = 0
        # 1. Left partial block
        left_end = (b_left + 1) * self.block_size
        total += sum(self.nums[left : left_end])

        # 2. Intermediate full blocks
        for b in range(b_left + 1, b_right):
            total += self.block_sums[b]

        # 3. Right partial block
        right_start = b_right * self.block_size
        total += sum(self.nums[right_start : right + 1])

        return total


def solve_mos_distinct_elements(nums: List[int], raw_queries: List[Tuple[int, int]]) -> List[int]:
    """Mo's Algorithm for offline Distinct Elements range queries in O((N + Q) * sqrt(N))."""
    n = len(nums)
    q = len(raw_queries)
    if q == 0:
        return []

    block_size = max(1, int(n / math.sqrt(q)))

    # Structure queries: (L, R, original_idx, block_idx)
    queries = [
        (l, r, idx, l // block_size)
        for idx, (l, r) in enumerate(raw_queries)
    ]

    # Serpentine alternating sort: odd blocks reverse R order
    queries.sort(key=lambda item: (item[3], -item[1] if (item[3] & 1) else item[1]))

    max_val = max(nums, default=0)
    freq = [0] * (max_val + 1)
    distinct_count = 0
    ans = [0] * q

    cur_l = 0
    cur_r = -1

    def add(idx: int) -> None:
        nonlocal distinct_count
        val = nums[idx]
        if freq[val] == 0:
            distinct_count += 1
        freq[val] += 1

    def remove(idx: int) -> None:
        nonlocal distinct_count
        val = nums[idx]
        freq[val] -= 1
        if freq[val] == 0:
            distinct_count -= 1

    for l, r, original_idx, _ in queries:
        while cur_r < r:
            cur_r += 1
            add(cur_r)
        while cur_r > r:
            remove(cur_r)
            cur_r -= 1
        while cur_l < l:
            remove(cur_l)
            cur_l += 1
        while cur_l > l:
            cur_l -= 1
            add(cur_l)

        ans[original_idx] = distinct_count

    return ans
```

---

## 🔬 Chapter 4: Explicit Complexity Deconstruction

| Metric | Classic Sqrt Decomp | Mo's Algorithm | Production Relevance |
| :--- | :--- | :--- | :--- |
| **Preprocessing Time** | `O(N)` | `O(Q * log Q)` | Sorting queries offline in Mo's dominates setup. |
| **Point Update Time** | `O(1)` | N/A (Offline only) | Sqrt Decomp gives instant `O(1)` buffer writes. |
| **Range Query Time** | `O(sqrt N)` | Amortized `O(sqrt N)` | Sub-linear queries without deep tree balancing. |
| **Total Query Movement** | `O(Q * sqrt N)` | `O((N + Q) * sqrt N)` | Provably optimal when `B = N / sqrt(Q)`. |
| **Auxiliary Memory** | `O(sqrt N)` block array | `O(N + Q)` frequency buffer | Fits effortlessly in L2/L3 cache without heap fragmentation. |
| **Cache Behavior** | Contiguous partial blocks | Dense pointer sweeps | CPU prefetcher streams consecutive indices during pointer sweeps. |

---

## 🎙️ Chapter 5: 45-Minute Verbal Script & Interview Playbook

```
[00:00 - 05:00] Problem Qualification & Strategy Selection
"The problem requires answering Q offline range queries counting distinct elements in range [L, R].
Because 'distinct count' does not permit O(1) interval merging, a standard Segment Tree requires
heavy persistent structures with high constant factor overhead.
Since all queries are provided upfront, I will apply Mo's Algorithm with Square Root Decomposition."

[05:00 - 15:00] Mathematical Bound Derivation
"Let block size be B. We partition query L coordinates into blocks of size B.
- Within each block, the left pointer moves at most B steps per query: Q * B moves.
- The right pointer sweeps monotonically from left to right across the array once per block: (N / B) * N moves.
Total moves = Q * B + N^2 / B.
To minimize total moves, d/dB (Q * B + N^2 / B) = 0 => B = N / sqrt(Q).
When N = Q = 100,000, setting B = sqrt(100,000) approx 316 yields total pointer moves of
2 * 100,000 * 316 approx 6.3 * 10^7, which executes in ~120ms.
I will also implement alternating sort parity: odd blocks sort R descending to halve pointer rewinds."

[15:00 - 32:00] Live Defensive Implementation
- Define query data structure with original index and block index.
- Implement the custom serpentine sorting comparator.
- Maintain `cur_L = 0` and `cur_R = -1` with `add` and `remove` helper functions.
- Run pointer sweeps carefully ordered to avoid out-of-bounds array indices.

[32:00 - 40:00] Edge Cases & Memory Verification
- Single element range `[i, i]`, overlapping ranges, identical ranges, and empty query list.
- Verify coordinate compression if values exceed `10^6` to bound the frequency table.

[40:00 - 45:00] Production Systems Translation
"In analytical columnar databases like ClickHouse and Apache Parquet, this exact block decomposition
is used for physical data skipping: data is chunked into 8,192 row granules with min/max/distinct statistics
stored in block headers to avoid scanning millions of unneeded disk blocks."
```

---

## 🔍 Chapter 6: Edge-Case Verification

| Test Case | Scenario | Expected Outcome | Invariant Handling |
| :--- | :--- | :--- | :--- |
| **Single Element Query** | `L = 5, R = 5` | Distinct count = 1 | `cur_L` and `cur_R` contract to index 5; `add` called once. |
| **Full Range Query** | `L = 0, R = N - 1` | Correct global distinct count | Pointers expand to encompass entire array bounds without overflow. |
| **Identical Consecutive Queries** | `Q1 = [2, 7], Q2 = [2, 7]` | Identical answer | Zero pointer movement occurs between queries; answered in `O(1)`. |
| **Disjoint Ranges** | `Q1 = [0, 1], Q2 = [N-2, N-1]` | Correct distinct counts | Pointer moves monotonically within sorted blocks; state resets correctly. |
| **Q >> N (Massive Queries)** | `N = 10^3, Q = 10^5` | `B = N / sqrt(Q) = 3` | Dynamically calculated block size prevents right pointer oscillation. |

---

> 🧭 **Navigation:** [← Previous Day](Week_18_Day_01_Meet_In_The_Middle_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_18_Day_03_Heavy_Light_Decomposition_Instructional.md)

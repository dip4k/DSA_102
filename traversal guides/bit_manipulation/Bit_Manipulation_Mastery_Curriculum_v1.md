# 🧬 Bit Manipulation — Traversal Mastery Curriculum v1

---

## Summary Table

| Level | Mental Model | State / Pointer | Drill Problems |
|-------|-------------|-----------------|----------------|
| **L1** Single-Bit Ops | XOR cancellation, lowest-bit isolation | Single integer accumulator or bit-test | LC 136, LC 231, LC 191 |
| **L2** Bit Masking & Counting | Mask as subset selector, Brian Kernighan counting | Bitmask `mask ∈ [0, 2ⁿ)`, popcount state | LC 338, LC 461, LC 78 |
| **L3** Bitwise DP & Advanced | Bit-by-bit construction, prefix AND/XOR window | DP on bitmask / prefix-set / bit-position | LC 201, LC 421, LC 1863 |

---

## Prerequisite Bit Facts (Cheat Sheet)

| Operation | Expression | What It Does |
|-----------|-----------|--------------|
| Check bit `k` | `(n >> k) & 1` | Returns bit k (0 or 1) |
| Set bit `k` | `n \| (1 << k)` | Forces bit k to 1 |
| Clear bit `k` | `n & ~(1 << k)` | Forces bit k to 0 |
| Toggle bit `k` | `n ^ (1 << k)` | Flips bit k |
| Lowest set bit | `n & (-n)` | Isolates rightmost 1-bit |
| Clear lowest set bit | `n & (n - 1)` | Turns off rightmost 1-bit |
| All 1s mask (k bits) | `(1 << k) - 1` | `k` ones: `0…0111…1` |
| Two's complement | `-n == ~n + 1` | Negation identity |

---

# Level 1 — Single-Bit Operations

## Mental Model

> **XOR Cancellation**: `a ^ a = 0` and `a ^ 0 = a`.  
> XOR-ing a collection collapses all pairs, leaving the unpaired element.

> **Power-of-Two Isolation**: A power of two has exactly one set bit.  
> `n & (n - 1) == 0` iff `n` is a power of two (for `n > 0`).

### State Definition

| Symbol | Meaning |
|--------|---------|
| `acc` | XOR accumulator — after processing `nums[0..i]`, `acc` holds the XOR of all elements seen |
| `n` | The integer under test for bit-property queries |

### Invariant

- **XOR scan**: After iteration `i`, `acc = nums[0] ^ nums[1] ^ … ^ nums[i]`.  
  At the end, every element appearing twice cancels out; the unique element remains.
- **Power-of-two**: `n > 0` **and** `n & (n - 1) == 0` ⟺ exactly one bit set.

---

### Visual Trace — Single Number (LC 136)

`nums = [4, 1, 2, 1, 2]`

| Step | Element | `acc` (binary) | `acc` (decimal) | Why |
|------|---------|---------------|-----------------|-----|
| 0 | 4 | `100` | 4 | `0 ^ 4` |
| 1 | 1 | `101` | 5 | `4 ^ 1` |
| 2 | 2 | `111` | 7 | `5 ^ 2` |
| 3 | 1 | `110` | 6 | `7 ^ 1` — the 1-pair cancels |
| 4 | 2 | `100` | 4 | `6 ^ 2` — the 2-pair cancels |

**Result**: `acc = 4` ✅

---

### Code — Single Number

> **Problem (LC 136):** Given a non-empty array where every element appears twice except one, find the single element.

```python
def singleNumber(nums: list[int]) -> int:
    # 1. Initialize accumulator to identity element of XOR (0)
    acc = 0
    # 2. XOR every element into the accumulator
    for x in nums:
        acc ^= x              # pairs cancel via a ^ a = 0; unique survives via a ^ 0 = a
    # 3. After full scan, only the unpaired element remains
    return acc
```

```csharp
public int SingleNumber(int[] nums)
{
    // 1. Initialize accumulator to identity element of XOR (0)
    int acc = 0;
    // 2. XOR every element — paired values cancel out (a ^ a = 0)
    foreach (int x in nums)
        acc ^= x;
    // 3. Return the sole survivor (the unique, unpaired element)
    return acc;
}
```

---

### Code — Power of Two

> **Problem (LC 231):** Given an integer `n`, return `true` if it is a power of two.

```python
def isPowerOfTwo(n: int) -> bool:
    # 1. Guard: zero and negatives are never powers of two
    # 2. Bit trick: a power of two has exactly one set bit,
    #    so n & (n - 1) clears that single bit, yielding 0
    return n > 0 and (n & (n - 1)) == 0
```

```csharp
public bool IsPowerOfTwo(int n)
{
    // 1. Guard: n must be positive (0 and negatives fail)
    // 2. Bit trick: exactly one set bit ⟹ n & (n-1) == 0
    return n > 0 && (n & (n - 1)) == 0;
}
```

---

### Code — Number of 1 Bits (Hamming Weight)

> **Problem (LC 191):** Return the number of `1` bits in the binary representation of an unsigned integer.

```python
def hammingWeight(n: int) -> int:
    # 1. Initialize a counter for set bits
    count = 0
    # 2. Loop until all bits are cleared
    while n:
        # 3. Clear the lowest set bit using Brian Kernighan's trick:
        #    n & (n - 1) turns off the rightmost 1-bit
        n &= n - 1
        # 4. Each clearing means we found one more set bit
        count += 1
    # 5. Return total number of 1-bits
    return count
```

```csharp
public int HammingWeight(uint n)
{
    // 1. Initialize a counter for set bits
    int count = 0;
    // 2. Loop until all bits are cleared
    while (n != 0)
    {
        // 3. Brian Kernighan's trick: clear the lowest set bit
        n &= n - 1;
        // 4. Increment — each iteration removes exactly one 1-bit
        count++;
    }
    // 5. Return total number of 1-bits
    return count;
}
```

---

### ⚠️ Gotchas & Pitfalls

| # | Pitfall | Detail |
|---|---------|--------|
| 1 | **`n = 0` is not a power of two** | `0 & (0-1) = 0` passes the bit test — always guard with `n > 0`. |
| 2 | **Signed vs unsigned shift** | In C#/Java, `>>` is arithmetic (sign-extends). Use `>>>` (Java) or cast to `uint` (C#) for unsigned right shift. Python integers are arbitrary-precision, so this doesn't apply. |
| 3 | **Negative numbers in power-of-two** | `-16` in two's complement has more than one set bit when viewed unsigned; the `n > 0` guard catches this. |

---

### 🏋️ Drill Problems — Level 1

| # | Problem | Difficulty | Key Trick |
|---|---------|-----------|-----------|
| 1 | [LeetCode 136: Single Number](https://leetcode.com/problems/single-number/) | Easy | XOR accumulator |
| 2 | [LeetCode 231: Power of Two](https://leetcode.com/problems/power-of-two/) | Easy | `n & (n-1) == 0` |
| 3 | [LeetCode 191: Number of 1 Bits](https://leetcode.com/problems/number-of-1-bits/) | Easy | Brian Kernighan |
| 4 | [LeetCode 190: Reverse Bits](https://leetcode.com/problems/reverse-bits/) | Easy | Bit-by-bit rebuild |
| 5 | [LeetCode 268: Missing Number](https://leetcode.com/problems/missing-number/) | Easy | XOR `[0..n]` with array |
| 6 | [LeetCode 389: Find the Difference](https://leetcode.com/problems/find-the-difference/) | Easy | XOR all chars |

---

# Level 2 — Bit Masking & Counting

## Mental Model

> **Bitmask as Subset Selector**: An integer `mask` with `n` bits represents a subset of `n` elements.  
> Bit `i` is set ⟹ element `i` is **included** in the subset.

> **DP on Popcount**: `countBits[i]` can be derived from a smaller already-solved state:  
> `countBits[i] = countBits[i >> 1] + (i & 1)`  — shift right drops the last bit; add it back.

### State Definition

| Symbol | Meaning |
|--------|---------|
| `mask` | Integer in `[0, 2ⁿ)` — each bit decides include/exclude for element at that index |
| `dp[i]` | Number of 1-bits in binary representation of `i` |

### Invariant / Recurrence

- **Subset enumeration**: For `mask` from `0` to `2ⁿ - 1`, build subset by checking each bit.
- **Counting bits DP**: `dp[0] = 0`; for `i ≥ 1`: `dp[i] = dp[i >> 1] + (i & 1)`.

---

### Visual Trace — Subsets via Bitmask (LC 78)

`nums = [a, b, c]` → `n = 3`, masks `000` to `111`.

| `mask` (bin) | `mask` (dec) | Bit 2 (`c`) | Bit 1 (`b`) | Bit 0 (`a`) | Subset |
|-------------|-------------|-------------|-------------|-------------|--------|
| `000` | 0 | ✗ | ✗ | ✗ | `[]` |
| `001` | 1 | ✗ | ✗ | ✓ | `[a]` |
| `010` | 2 | ✗ | ✓ | ✗ | `[b]` |
| `011` | 3 | ✗ | ✓ | ✓ | `[a,b]` |
| `100` | 4 | ✓ | ✗ | ✗ | `[c]` |
| `101` | 5 | ✓ | ✗ | ✓ | `[a,c]` |
| `110` | 6 | ✓ | ✓ | ✗ | `[b,c]` |
| `111` | 7 | ✓ | ✓ | ✓ | `[a,b,c]` |

**Total subsets**: `2³ = 8` ✅

---

### Visual Trace — Counting Bits DP (LC 338)

| `i` | `i` (bin) | `i >> 1` | `dp[i>>1]` | `i & 1` | `dp[i]` |
|-----|----------|----------|-----------|---------|---------| 
| 0 | `0000` | — | — | — | **0** |
| 1 | `0001` | 0 | 0 | 1 | **1** |
| 2 | `0010` | 1 | 1 | 0 | **1** |
| 3 | `0011` | 1 | 1 | 1 | **2** |
| 4 | `0100` | 2 | 1 | 0 | **1** |
| 5 | `0101` | 2 | 1 | 1 | **2** |

---

### Code — Counting Bits

> **Problem (LC 338):** Given an integer `n`, return an array `ans` of length `n + 1` where `ans[i]` is the number of 1-bits in `i`.

```python
def countBits(n: int) -> list[int]:
    # 1. Base case: dp[0] = 0 (zero has no set bits)
    dp = [0] * (n + 1)
    # 2. Build up using DP recurrence for each integer 1..n
    for i in range(1, n + 1):
        # 3. Recurrence: i >> 1 drops the LSB (already solved),
        #    (i & 1) adds back the parity of the dropped bit
        dp[i] = dp[i >> 1] + (i & 1)
    # 4. Return the full popcount array
    return dp
```

```csharp
public int[] CountBits(int n)
{
    // 1. Base case: dp[0] = 0 (zero has no set bits)
    int[] dp = new int[n + 1];
    // 2. Fill dp[1..n] using the recurrence
    for (int i = 1; i <= n; i++)
        // 3. dp[i] = dp[i >> 1] (bits of i without its LSB) + parity of LSB
        dp[i] = dp[i >> 1] + (i & 1);
    // 4. Return the complete popcount array
    return dp;
}
```

---

### Code — Hamming Distance

> **Problem (LC 461):** Given two integers `x` and `y`, return the Hamming distance — the number of positions at which their corresponding bits differ.

```python
def hammingDistance(x: int, y: int) -> int:
    # 1. XOR isolates the differing bits: each 1 in xor marks a mismatch
    xor = x ^ y
    # 2. Count the set bits in xor using Brian Kernighan's technique
    count = 0
    while xor:
        # 3. Clear the lowest set bit — one fewer differing position
        xor &= xor - 1
        # 4. Tally each cleared bit
        count += 1
    # 5. Total count equals the Hamming distance
    return count
```

```csharp
public int HammingDistance(int x, int y)
{
    // 1. XOR isolates the differing bits: each 1 marks a mismatch
    int xor = x ^ y;
    // 2. Count set bits via Brian Kernighan's technique
    int count = 0;
    while (xor != 0)
    {
        // 3. Clear the lowest set bit
        xor &= xor - 1;
        // 4. Each clearing represents one bit position of difference
        count++;
    }
    // 5. Return the total Hamming distance
    return count;
}
```

---

### Code — Subsets via Bitmask

> **Problem (LC 78):** Given an integer array `nums` of unique elements, return all possible subsets (the power set).

```python
def subsets(nums: list[int]) -> list[list[int]]:
    n = len(nums)
    result = []
    # 1. Enumerate every bitmask from 0 (empty set) to 2^n - 1 (full set)
    for mask in range(1 << n):
        subset = []
        # 2. Check each bit position i in the mask
        for i in range(n):
            # 3. If bit i is set, include nums[i] in this subset
            if mask & (1 << i):
                subset.append(nums[i])
        # 4. Collect the subset corresponding to this mask
        result.append(subset)
    # 5. Return all 2^n subsets
    return result
```

```csharp
public IList<IList<int>> Subsets(int[] nums)
{
    int n = nums.Length;
    var result = new List<IList<int>>();
    // 1. Enumerate every bitmask from 0 (empty set) to 2^n - 1 (full set)
    for (int mask = 0; mask < (1 << n); mask++)
    {
        var subset = new List<int>();
        // 2. Check each bit position i in the current mask
        for (int i = 0; i < n; i++)
        {
            // 3. If bit i is set, include nums[i] in this subset
            if ((mask & (1 << i)) != 0)
                subset.Add(nums[i]);
        }
        // 4. Add the constructed subset to the result
        result.Add(subset);
    }
    // 5. Return all 2^n subsets
    return result;
}
```

---

### ⚠️ Gotchas & Pitfalls

| # | Pitfall | Detail |
|---|---------|--------|
| 1 | **`1 << n` overflow** | In C#/Java, `1 << 31` overflows a 32-bit int. Use `1L << n` for `n ≥ 31`. Python has arbitrary precision, so no issue. |
| 2 | **Off-by-one in mask range** | The range is `[0, 2ⁿ)`, i.e., `range(1 << n)`. Writing `range(1, 1 << n)` skips the empty subset. |
| 3 | **Counting bits DP base case** | `dp[0] = 0` must be set explicitly. The recurrence `dp[i] = dp[i >> 1] + (i & 1)` relies on `dp[0]` being correct. |

---

### 🏋️ Drill Problems — Level 2

| # | Problem | Difficulty | Key Trick |
|---|---------|-----------|-----------|
| 1 | [LeetCode 338: Counting Bits](https://leetcode.com/problems/counting-bits/) | Easy | DP: `dp[i] = dp[i>>1] + (i&1)` |
| 2 | [LeetCode 461: Hamming Distance](https://leetcode.com/problems/hamming-distance/) | Easy | XOR + Brian Kernighan count |
| 3 | [LeetCode 78: Subsets](https://leetcode.com/problems/subsets/) | Medium | Bitmask enumeration `[0, 2ⁿ)` |
| 4 | [LeetCode 476: Number Complement](https://leetcode.com/problems/number-complement/) | Easy | XOR with all-1s mask of same width |
| 5 | [LeetCode 1009: Complement of Base 10 Integer](https://leetcode.com/problems/complement-of-base-10-integer/) | Easy | Same as LC 476 |
| 6 | [LeetCode 90: Subsets II](https://leetcode.com/problems/subsets-ii/) | Medium | Bitmask + duplicate skip |

---

# Level 3 — Bitwise DP & Advanced

## Mental Model

> **Bit-by-Bit Construction (AND of Range):** AND-ing a contiguous range `[m, n]` preserves only the common prefix bits. Every bit position where `m` and `n` differ will eventually see both 0 and 1 in the range, AND-ing to 0.

> **Greedy Bit-Building (Maximum XOR):** Build the answer bit-by-bit from the most significant bit down. At each bit position, *try* to set that bit to 1 by checking if a valid pair exists using prefix sets.

### State Definition

| Symbol | Meaning |
|--------|---------|
| `m, n` | Range endpoints; shift right together until they match (common prefix) |
| `prefix_set` | Set of number prefixes seen so far (for XOR trie-like approach) |
| `candidate` | The best XOR answer we're trying to achieve at the current bit level |

---

### Visual Trace — Bitwise AND of Range (LC 201)

`m = 5 (101)`, `n = 7 (111)`

| Step | `m` (bin) | `n` (bin) | Equal? | Action |
|------|----------|----------|--------|--------|
| 0 | `101` | `111` | No | Shift right, `shifts = 1` |
| 1 | `10` | `11` | No | Shift right, `shifts = 2` |
| 2 | `1` | `1` | ✓ | Stop — common prefix found |

**Result**: `1 << 2 = 4` → `100` ✅  
Verify: `5 & 6 & 7 = 101 & 110 & 111 = 100 = 4`

---

### Visual Trace — Maximum XOR (LC 421)

`nums = [3, 10, 5, 25, 2, 8]` — Find max XOR of any two elements.

Process bit-by-bit from MSB (bit 4 down to 0):

| Bit `k` | `candidate` (try) | Prefixes (top k+1 bits) | Can achieve? | `max_xor` |
|---------|--------------------|------------------------|-------------|-----------|
| 4 | `10000` (16) | `{00, 01, 11, 00, 00, 01}` → `{00, 01, 11}` | `00 ^ 11 = 11` ≠ `10` — No | `00000` |
| 3 | `01000` (8) | `{000, 010, 001, 110, 000, 010}` → `{000, 001, 010, 110}` | `010 ^ 110 = 100` ≠ `010`; but check all — not found via `01` | need careful check |
| … | *(full trie or prefix-set check)* | | | **`28`** |

> The prefix-set approach checks: for each `p` in set, does `p ^ candidate` exist in set?  
> Final answer: `5 ^ 25 = 00101 ^ 11001 = 11100 = 28`.

---

### Code — Bitwise AND of Number Range

> **Problem (LC 201):** Given two integers `left` and `right` (`left <= right`), return the bitwise AND of all numbers in `[left, right]`.  
> **Key insight:** Only the common binary prefix of `left` and `right` survives the AND; all lower bits get zeroed out because somewhere in the range those bits flip.

```python
def rangeBitwiseAnd(left: int, right: int) -> int:
    # 1. Track how many bits we strip from the right
    shifts = 0
    # 2. Shift both endpoints right until they share a common prefix
    while left != right:
        left >>= 1               # 3. Discard the diverging LSB from left
        right >>= 1              # 4. Discard the diverging LSB from right
        shifts += 1              # 5. Remember how many bits we dropped
    # 6. Shift the common prefix back to its original position
    return left << shifts
```

```csharp
public int RangeBitwiseAnd(int left, int right)
{
    // 1. Track the number of right-shifts performed
    int shifts = 0;
    // 2. Shift both endpoints until they converge to the common prefix
    while (left != right)
    {
        left >>= 1;              // 3. Discard diverging LSB from left
        right >>= 1;             // 4. Discard diverging LSB from right
        shifts++;                // 5. Count dropped bits
    }
    // 6. Restore the common prefix to its original bit position
    return left << shifts;
}
```

**Time**: O(log n) — at most 32 iterations. **Space**: O(1).

---

### Code — Maximum XOR of Two Numbers in an Array

> **Problem (LC 421):** Given an integer array `nums`, return the maximum XOR of any two elements `nums[i] XOR nums[j]`.  
> **Key insight:** Build the answer greedily from the highest bit. At each bit position, assume we can set that bit to 1 in the answer. Check feasibility: if any two prefixes XOR to the candidate, it's achievable.

```python
def findMaximumXOR(nums: list[int]) -> int:
    max_xor = 0
    # 1. Process each bit from MSB (bit 31) down to LSB (bit 0)
    for k in range(31, -1, -1):
        # 2. Collect prefixes: top bits of each number down to bit k
        prefixes = set()
        for num in nums:
            prefixes.add(num >> k)

        # 3. Greedily try to set bit k in the answer
        candidate = max_xor | 1           # tentatively add bit k

        # 4. Check feasibility: does any pair of prefixes XOR to candidate?
        for p in prefixes:
            if (p ^ candidate) in prefixes:
                # 5. Confirmed — a valid pair exists; lock in this bit
                max_xor = candidate
                break

        # 6. Shift max_xor left to make room for the next lower bit
        max_xor <<= 1

    # 7. Undo the final extra left-shift from the last iteration
    return max_xor >> 1
```

> **Note:** The above is the conceptual greedy version. A cleaner standard implementation:

```python
def findMaximumXOR(nums: list[int]) -> int:
    max_xor = 0
    # 1. Process each bit from MSB (bit 31) down to LSB (bit 0)
    for k in range(31, -1, -1):
        # 2. Shift left first to make room for the next bit decision
        max_xor <<= 1
        # 3. Tentatively set the current bit to 1
        candidate = max_xor | 1
        # 4. Collect prefixes: extract top bits down to position k
        prefixes = {num >> k for num in nums}
        # 5. Check if any pair of prefixes can achieve the candidate XOR
        for p in prefixes:
            if (p ^ candidate) in prefixes:
                # 6. Confirmed — keep this bit set in max_xor
                max_xor = candidate
                break
    # 7. Return the greedily constructed maximum XOR
    return max_xor
```

```csharp
public int FindMaximumXOR(int[] nums)
{
    int maxXor = 0;
    // 1. Process each bit from MSB (bit 31) down to LSB (bit 0)
    for (int k = 31; k >= 0; k--)
    {
        // 2. Shift left to make room for the next bit decision
        maxXor <<= 1;
        // 3. Tentatively set the current bit to 1
        int candidate = maxXor | 1;
        // 4. Collect prefixes: extract top bits down to position k
        var prefixes = new HashSet<int>();
        foreach (int num in nums)
            prefixes.Add(num >> k);

        // 5. Check if any pair of prefixes can achieve the candidate XOR
        foreach (int p in prefixes)
        {
            if (prefixes.Contains(p ^ candidate))
            {
                // 6. Confirmed — lock this bit into maxXor
                maxXor = candidate;
                break;
            }
        }
    }
    // 7. Return the greedily constructed maximum XOR
    return maxXor;
}
```

**Time**: O(32·n) = O(n). **Space**: O(n) for the prefix set.

---

### Code — Sum of All Subset XOR Totals

> **Problem (LC 1863):** Return the sum of XOR totals for every subset of `nums`.  
> **Key insight:** Each bit position `k` contributes to exactly `2^(n-1)` subsets (half of all subsets). The OR of all elements tells us which bit positions are ever set. Multiply contribution by `2^(n-1)`.

```python
def subsetXORSum(nums: list[int]) -> int:
    # 1. OR all elements together — any bit set in at least one number
    #    will contribute to exactly half (2^(n-1)) of all subsets
    all_or = 0
    for num in nums:
        all_or |= num
    # 2. Multiply the combined bit contributions by 2^(n-1)
    return all_or * (1 << (len(nums) - 1))
```

```csharp
public int SubsetXORSum(int[] nums)
{
    // 1. OR all elements — collect every bit that appears in any number
    int allOr = 0;
    foreach (int num in nums)
        allOr |= num;
    // 2. Each set bit contributes to exactly 2^(n-1) subsets
    return allOr * (1 << (nums.Length - 1));
}
```

**Time**: O(n). **Space**: O(1).

---

### ⚠️ Gotchas & Pitfalls

| # | Pitfall | Detail |
|---|---------|--------|
| 1 | **Range AND — brute force TLE** | Iterating from `left` to `right` is O(right - left) which can be ~2³¹. The shift-based approach is O(32). |
| 2 | **Max XOR — forgetting the shift** | The `max_xor <<= 1` must happen *before* testing the candidate, not after. Getting the shift order wrong produces wrong bit positions. |
| 3 | **Negative numbers in XOR problems** | In Python, `>>` on negative numbers sign-extends. Most LC problems use non-negative inputs, but if negatives appear, mask with `& 0xFFFFFFFF` to simulate 32-bit unsigned. In C#, cast to `uint` or use `>>>`. |

---

### 🏋️ Drill Problems — Level 3

| # | Problem | Difficulty | Key Trick |
|---|---------|-----------|-----------|
| 1 | [LeetCode 201: Bitwise AND of Numbers Range](https://leetcode.com/problems/bitwise-and-of-numbers-range/) | Medium | Common prefix via right-shift |
| 2 | [LeetCode 421: Maximum XOR of Two Numbers](https://leetcode.com/problems/maximum-xor-of-two-numbers-in-an-array/) | Medium | Greedy bit-build + prefix set |
| 3 | [LeetCode 1863: Sum of All Subset XOR Totals](https://leetcode.com/problems/sum-of-all-subset-xor-totals/) | Easy | OR-all × 2^(n-1) |
| 4 | [LeetCode 1829: Maximum XOR for Each Query](https://leetcode.com/problems/maximum-xor-for-each-query/) | Medium | Prefix XOR + complement mask |
| 5 | [LeetCode 2275: Largest Combination With Bitwise AND > 0](https://leetcode.com/problems/largest-combination-with-bitwise-and-greater-than-zero/) | Medium | Count set bits per position |
| 6 | [LeetCode 137: Single Number II](https://leetcode.com/problems/single-number-ii/) | Medium | Bit counting mod 3 / state machine |

---

## Quick-Reference: Complexity Cheat Sheet

| Technique | Time | Space | When to Use |
|-----------|------|-------|-------------|
| XOR accumulator | O(n) | O(1) | Find unique / cancel pairs |
| Brian Kernighan count | O(set bits) | O(1) | Count 1-bits |
| Bitmask subset enum | O(2ⁿ · n) | O(2ⁿ) | Enumerate all subsets, n ≤ 20 |
| Counting bits DP | O(n) | O(n) | Precompute popcount array |
| Prefix-shift AND | O(32) | O(1) | AND over contiguous range |
| Greedy bit-build XOR | O(32·n) | O(n) | Max XOR pair |
| Bit-count per position | O(32·n) | O(1) | Per-bit contribution across array |

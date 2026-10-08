# 08. Prefix Sums & Difference Arrays Mastery Guide

## 1. Summary Table

| Level | Topic | Mental Model | Pointer/State | Drill Problems |
| --- | --- | --- | --- | --- |
| L1 | 1D Prefix Sum | Precompute running totals to query subarray sums in `O(1)`. | `P[i]`: Sum of first `i` elements. | [LeetCode 303: Range Sum Query - Immutable] (Easy) |
| L2 | Prefix Sum + Hashing | Store prefix sums in a hash map to find subarrays with specific properties (sum, parity). | `sum_freq`: Map of `prefix_sum` to its frequency or first seen index. | [LeetCode 560: Subarray Sum Equals K] (Medium) |
| L3 | 2D Prefix Sum | Precompute area totals for 2D subgrid sum queries in `O(1)`. | `P[i][j]`: Sum of grid from `(0,0)` to `(i-1,j-1)`. | [LeetCode 304: Range Sum Query 2D - Immutable] (Medium) |
| L4 | Difference Arrays | Track relative changes between adjacent elements for `O(1)` range updates. | `D[i] = A[i] - A[i-1]`. Update: `D[L] += V`, `D[R+1] -= V`. | [LeetCode 1094: Car Pooling] (Medium), [LeetCode 1109: Corporate Flight Bookings] (Medium) |
| L5 | Prefix XOR & Bitmask Parity | Running XOR precomputation and parity bitmasks for `O(1)` subarray XOR and even-count constraints. | `xor_prefix[i]`: XOR of first `i` elements. Range XOR: `xor_prefix[R+1] ^ xor_prefix[L]`. | [LeetCode 1310: XOR Queries of a Subarray] (Medium), [LeetCode 1371: Find Longest Substring Containing Vowels in Even Counts] (Medium) |

---

## Level 1: 1D Prefix Sum

**Mental Model & Invariants**
- **State Meaning:** `P[i]` represents the sum of elements from index `0` to `i-1`. (1-indexed prefix array is cleaner).
- **Region Processed:** Building phase processes `0` to `n`. Query phase processes specific `[L, R]` queries.
- **Invariant:** Sum of subarray `A[L...R]` is ALWAYS `P[R+1] - P[L]`.

**Visual State Transitions**
Building `P` for `A = [2, -1, 3]`:

| Step | Element `A[i]` | Prefix Sum `P[i+1]` | Invariant/Recurrence |
| --- | --- | --- | --- |
| 0 | - | `P[0] = 0` | Base case |
| 1 | `A[0] = 2` | `P[1] = 0 + 2 = 2` | `P[1] = P[0] + A[0]` |
| 2 | `A[1] = -1`| `P[2] = 2 + (-1) = 1` | `P[2] = P[1] + A[1]` |
| 3 | `A[2] = 3` | `P[3] = 1 + 3 = 4` | `P[3] = P[2] + A[2]` |

**Code Snippet**

Problem: Given an integer array, preprocess it to answer multiple range-sum queries `sumRange(left, right)` in O(1) time each.

```python
class PrefixSum1D:
    def __init__(self, nums: list[int]):
        # 1. Allocate P with size n+1, using 1-indexing so P[0]=0 acts as
        #    the base case (empty prefix), avoiding edge-case checks for L=0.
        self.P = [0] * (len(nums) + 1)
        for i in range(len(nums)):
            # 2. Build running total: P[i+1] = P[i] + nums[i].
            #    After this loop, P[k] = sum of nums[0..k-1].
            self.P[i + 1] = self.P[i] + nums[i]

    def query(self, left: int, right: int) -> int:
        # 3. Apply the invariant: sum(nums[left..right]) = P[right+1] - P[left].
        #    Subtracting P[left] removes the prefix we don't want.
        return self.P[right + 1] - self.P[left]
```

```csharp
public class PrefixSum1D {
    private int[] P;

    public PrefixSum1D(int[] nums) {
        // 1. Allocate P with size n+1 (1-indexed). P[0] = 0 is the base case.
        P = new int[nums.Length + 1];
        for (int i = 0; i < nums.Length; i++) {
            // 2. Build running total: P[i+1] accumulates the sum of nums[0..i].
            P[i + 1] = P[i] + nums[i];
        }
    }

    public int Query(int left, int right) {
        // 3. Apply the invariant: sum(nums[left..right]) = P[right+1] - P[left].
        return P[right + 1] - P[left];
    }
}
```

### ⚠️ Gotchas & Pitfalls
- **0-indexing vs 1-indexing:** Using a 0-indexed prefix array forces an ugly `if left == 0` check. Always use `P` of size `N+1`.
- **Overflow:** Prefix sums can quickly exceed 32-bit integer limits. In C#/Java, use `long` if `nums[i]` can be large.

**Drill Problems**
- [LeetCode 303: Range Sum Query - Immutable] (Easy)

---

## Level 2: Prefix Sum + Hashing

**Mental Model & Invariants**
- **State Meaning:** `curr_sum` is the running prefix sum up to the current index. A hash map stores how many times we've seen each `curr_sum`.
- **Region Processed:** We iterate left to right once.
- **Invariant:** If we want a subarray sum equal to `K` ending at current index, we must have seen a previous prefix sum equal to `curr_sum - K`.

**Visual State Transitions**
Finding subarrays summing to `K = 3` for `A = [1, 2, 3]`:

| Step | `A[i]` | `curr_sum` | Target `curr_sum - K` | Hash Map `counts` (after step) | Action / Result |
| --- | --- | --- | --- | --- | --- |
| Init | - | 0 | - | `{0: 1}` | Initialize dummy prefix 0 |
| 0 | 1 | 1 | `1 - 3 = -2` | `{0:1, 1:1}` | `-2` not in map. |
| 1 | 2 | 3 | `3 - 3 = 0` | `{0:1, 1:1, 3:1}` | Found `0`. Count += 1. |
| 2 | 3 | 6 | `6 - 3 = 3` | `{0:1, 1:1, 3:1, 6:1}` | Found `3`. Count += 1. |

**Code Snippet**

Problem: Given an integer array and a target `k`, count the total number of contiguous subarrays whose elements sum to `k`.

```python
def subarraySum(nums: list[int], k: int) -> int:
    # 1. Base case: an empty prefix has sum 0, seen once.
    #    This lets us count subarrays starting at index 0.
    counts = {0: 1}
    curr_sum = 0
    ans = 0
    
    for num in nums:
        # 2. Extend the running prefix sum by the current element.
        curr_sum += num
        # 3. Check the invariant: if (curr_sum - k) was a previous prefix sum,
        #    then the subarray between that earlier index and here sums to k.
        if (curr_sum - k) in counts:
            ans += counts[curr_sum - k]
        # 4. Record the current prefix sum in the map for future look-ups.
        counts[curr_sum] = counts.get(curr_sum, 0) + 1
        
    return ans
```

```csharp
public int SubarraySum(int[] nums, int k) {
    // 1. Base case: empty prefix sum 0 occurs once, so subarrays
    //    starting at index 0 can be detected.
    Dictionary<int, int> counts = new Dictionary<int, int>();
    counts[0] = 1;
    int currSum = 0, ans = 0;
    
    foreach (int num in nums) {
        // 2. Extend the running prefix sum with the current element.
        currSum += num;
        // 3. Check the invariant: if (currSum - k) exists in the map,
        //    those many subarrays ending here sum to exactly k.
        if (counts.TryGetValue(currSum - k, out int count)) {
            ans += count;
        }
        // 4. Record the current prefix sum frequency for future queries.
        counts[currSum] = counts.GetValueOrDefault(currSum, 0) + 1;
    }
    
    return ans;
}
```

### ⚠️ Gotchas & Pitfalls
- **Missing Base Case:** Forgetting to initialize the hash map with `{0: 1}` will cause you to miss valid subarrays that start at index 0.
- **Negative Numbers:** Sliding window cannot be used if elements can be negative. Prefix sum + hashing handles negatives naturally.

**Drill Problems**
- [LeetCode 560: Subarray Sum Equals K] (Medium)
- [LeetCode 974: Subarray Sums Divisible by K] (Medium)

---

## Level 3: 2D Prefix Sum

**Mental Model & Invariants**
- **State Meaning:** `P[r][c]` represents the sum of the rectangular grid from `(0,0)` to `(r-1,c-1)`.
- **Region Processed:** Grid is processed row by row, column by column.
- **Invariant:** The sum of a subgrid from `(r1, c1)` to `(r2, c2)` is: `P[r2+1][c2+1] - P[r1][c2+1] - P[r2+1][c1] + P[r1][c1]`. (Inclusion-Exclusion Principle).

**Visual State Transitions**
Building `P` uses inclusion-exclusion: `P[r][c] = A[r-1][c-1] + P[r-1][c] + P[r][c-1] - P[r-1][c-1]`.

| Region | Component | Sign |
| --- | --- | --- |
| Bottom-Right | `P[r2+1][c2+1]` | `+` (Full Area) |
| Top-Right | `P[r1][c2+1]` | `-` (Subtract top strip) |
| Bottom-Left | `P[r2+1][c1]` | `-` (Subtract left strip) |
| Top-Left | `P[r1][c1]` | `+` (Add back double-subtracted overlap) |

**Code Snippet**

Problem: Given a 2D matrix, preprocess it to answer multiple sub-rectangle sum queries `sumRegion(r1, c1, r2, c2)` in O(1) time each.

```python
class NumMatrix:
    def __init__(self, matrix: list[list[int]]):
        R, C = len(matrix), len(matrix[0])
        # 1. Allocate (R+1) x (C+1) grid with zero-padding on row 0 and col 0.
        #    This eliminates bounds checking during the inclusion-exclusion build.
        self.P = [[0] * (C + 1) for _ in range(R + 1)]
        
        for r in range(R):
            for c in range(C):
                # 2. Build via inclusion-exclusion:
                #    P[r+1][c+1] = current cell
                #                + area above (P[r][c+1])
                #                + area to the left (P[r+1][c])
                #                - double-counted overlap (P[r][c])
                self.P[r+1][c+1] = (matrix[r][c] + 
                                    self.P[r][c+1] + 
                                    self.P[r+1][c] - 
                                    self.P[r][c])

    def sumRegion(self, r1: int, c1: int, r2: int, c2: int) -> int:
        # 3. Query via inclusion-exclusion:
        #    Full rectangle - top strip - left strip + overlap added back.
        return (self.P[r2+1][c2+1] 
              - self.P[r1][c2+1] 
              - self.P[r2+1][c1] 
              + self.P[r1][c1])
```

```csharp
public class NumMatrix {
    private int[,] P;

    public NumMatrix(int[][] matrix) {
        int R = matrix.Length, C = matrix[0].Length;
        // 1. Allocate (R+1) x (C+1) grid; row 0 and col 0 are zero-padding.
        P = new int[R + 1, C + 1];
        
        for (int r = 0; r < R; r++) {
            for (int c = 0; c < C; c++) {
                // 2. Build via inclusion-exclusion:
                //    current cell + area above + area left - double-counted overlap.
                P[r+1, c+1] = matrix[r][c] + P[r, c+1] + P[r+1, c] - P[r, c];
            }
        }
    }

    public int SumRegion(int r1, int c1, int r2, int c2) {
        // 3. Query via inclusion-exclusion:
        //    full area - top strip - left strip + overlap added back.
        return P[r2+1, c2+1] - P[r1, c2+1] - P[r2+1, c1] + P[r1, c1];
    }
}
```

### ⚠️ Gotchas & Pitfalls
- **Off-By-One Errors:** Very common if not using `(R+1) x (C+1)` padding.
- **Sign Flipping:** Forgetting to add back the top-left overlap `P[r1][c1]` during the query phase.

**Drill Problems**
- [LeetCode 304: Range Sum Query 2D - Immutable] (Medium)

---

## Level 4: Difference Arrays

**Mental Model & Invariants**
- **State Meaning:** `D[i]` represents the difference between element `i` and element `i-1`. `D[i] = A[i] - A[i-1]`.
- **Region Processed:** Updates happen at boundaries `L` and `R+1`. We sweep left to right at the end to reconstruct the array.
- **Invariant:** A range addition `+V` to `[L, R]` changes only `D[L]` by `+V` and `D[R+1]` by `-V`. The prefix sum of `D` reconstructs the final modified array `A`.

**Visual State Transitions**
Apply `+2` to range `[1, 3]` on array of 5 zeros.

| Step | Action | Diff Array `D` | Reconstructed `A` (Prefix Sum of `D`) |
| --- | --- | --- | --- |
| 0 | Init | `[0, 0, 0, 0, 0, 0]` | `[0, 0, 0, 0, 0]` |
| 1 | `D[1] += 2` | `[0, 2, 0, 0, 0, 0]` | (Not calculated yet) |
| 2 | `D[3+1] -= 2`| `[0, 2, 0, 0, -2, 0]` | (Not calculated yet) |
| 3 | Sweep | `[0, 2, 0, 0, -2, 0]` | `[0, 2, 2, 2, 0]` |

**Code Snippet**

Problem: Given an array initially filled with zeros and a list of range-update operations `[L, R, V]`, return the final array after applying all additions.

```python
def getModifiedArray(length: int, updates: list[list[int]]) -> list[int]:
    # 1. Allocate difference array with size length+1.
    #    The extra slot at index `length` safely absorbs D[R+1] when R is the last index.
    D = [0] * (length + 1)
    
    for L, R, V in updates:
        # 2. Mark the start of the range: the effect of +V begins at index L.
        D[L] += V
        # 3. Mark one past the end: the effect of +V stops after index R.
        D[R + 1] -= V
        
    # 4. Reconstruct the final array by taking the prefix sum of D.
    #    The running total `curr` accumulates all overlapping range updates.
    res = []
    curr = 0
    for i in range(length):
        curr += D[i]
        res.append(curr)
        
    return res
```

```csharp
public int[] GetModifiedArray(int length, int[][] updates) {
    // 1. Allocate difference array with size length+1.
    //    Extra slot prevents out-of-bounds when R is the last valid index.
    int[] D = new int[length + 1];
    
    foreach (var update in updates) {
        int L = update[0], R = update[1], V = update[2];
        // 2. Mark range start: +V effect begins at L.
        D[L] += V;
        // 3. Mark range end+1: +V effect is cancelled after R.
        D[R + 1] -= V;
    }
    
    // 4. Sweep left-to-right: prefix sum of D reconstructs the final array.
    int[] res = new int[length];
    int curr = 0;
    for (int i = 0; i < length; i++) {
        curr += D[i];
        res[i] = curr;
    }
    
    return res;
}
```

### ⚠️ Gotchas & Pitfalls
- **Out of Bounds on Right Boundary:** If `R` is the last index (`N-1`), updating `D[R+1]` accesses index `N`. Always allocate the difference array with size `N+1`.
- **Applying Sweeps Iteratively:** difference arrays are for offline updates. You must apply ALL updates before computing the prefix sum. You cannot query point values midway efficiently.

**Drill Problems**
- [LeetCode 1094: Car Pooling] (Medium)
- [LeetCode 1109: Corporate Flight Bookings] (Medium)

---

## Level 5: Prefix XOR & Bitmask Parity

### Mental Model & Invariants
- **State Meaning:** `P_xor[i]` represents the cumulative XOR sum of elements from index `0` to `i-1`. With 1-based indexing, `P_xor[0] = 0`.
- **Region Processed:** Subarray `nums[L..R]`.
- **Invariant:** Range XOR identity:
  ```text
  XOR(nums[L..R]) = P_xor[R + 1] ^ P_xor[L]
  ```
  *Why it works:* XOR is its own inverse (`x ^ x = 0` and `x ^ 0 = x`). XORing `P_xor[R+1]` with `P_xor[L]` cancels out all elements from `0` to `L-1`, leaving exactly the XOR product of `nums[L..R]`.
- **Bitmask Parity Mapping:** When tracking whether character frequencies in a substring are even or odd, assign each tracked element a bit position (e.g. vowels `a=1, e=2, i=4, o=8, u=16`). XORing bitmasks toggles parity (`0 -> 1 -> 0`). Substring `s[L..R]` has all tracked characters with even counts if and only if `mask[R] ^ mask[L-1] == 0`, meaning `mask[R] == mask[L-1]`.

### Visual State Transition
Example: `arr = [1, 3, 4, 8]`, queries = `[[0, 1], [1, 2]]`

| Index `i` | Element `arr[i]` | Binary | Prefix XOR `P_xor[i+1]` | Binary |
| --- | --- | --- | --- | --- |
| Base | - | - | `P_xor[0] = 0` | `0000` |
| 0 | 1 | `0001` | `P_xor[1] = 0 ^ 1 = 1` | `0001` |
| 1 | 3 | `0011` | `P_xor[2] = 1 ^ 3 = 2` | `0010` |
| 2 | 4 | `0100` | `P_xor[3] = 2 ^ 4 = 6` | `0110` |
| 3 | 8 | `1000` | `P_xor[4] = 6 ^ 8 = 14` | `1110` |

* Query `[0, 1]`: `P_xor[2] ^ P_xor[0] = 2 ^ 0 = 2` (check: `1 ^ 3 = 2`).
* Query `[1, 2]`: `P_xor[3] ^ P_xor[1] = 6 ^ 1 = 7` (check: `3 ^ 4 = 7`).

### Code Snippets

#### 1. Range XOR Queries (LeetCode 1310 - Medium)

```python
def xorQueries(arr: list[int], queries: list[list[int]]) -> list[int]:
    # 1. Allocate 1-indexed prefix XOR array with base case P[0] = 0
    prefix_xor = [0] * (len(arr) + 1)
    for i in range(len(arr)):
        # 2. Invariant: prefix_xor[k] = arr[0] ^ arr[1] ^ ... ^ arr[k-1]
        prefix_xor[i + 1] = prefix_xor[i] ^ arr[i]
        
    res = []
    # 3. Answer each range query in O(1) time
    for left, right in queries:
        # 4. XOR identity: XOR(left..right) = prefix_xor[right+1] ^ prefix_xor[left]
        res.append(prefix_xor[right + 1] ^ prefix_xor[left])
        
    return res
```

```csharp
public int[] XorQueries(int[] arr, int[][] queries) {
    // 1. Allocate 1-indexed prefix XOR array with base case P[0] = 0
    int[] prefixXor = new int[arr.Length + 1];
    for (int i = 0; i < arr.Length; i++) {
        // 2. Invariant: prefixXor[k] = arr[0] ^ ... ^ arr[k-1]
        prefixXor[i + 1] = prefixXor[i] ^ arr[i];
    }
    
    int[] res = new int[queries.Length];
    // 3. Process each query in O(1) time
    for (int i = 0; i < queries.Length; i++) {
        int left = queries[i][0];
        int right = queries[i][1];
        // 4. Invariant: XOR(left..right) = prefixXor[right+1] ^ prefixXor[left]
        res[i] = prefixXor[right + 1] ^ prefixXor[left];
    }
    
    return res;
}
```

#### 2. Longest Substring Containing Vowels in Even Counts (LeetCode 1371 - Medium)

```python
def findTheLongestSubstring(s: str) -> int:
    # 1. Bitmask map: assign each vowel a distinct power of 2
    vowel_mask = {'a': 1, 'e': 2, 'i': 4, 'o': 8, 'u': 16}
    
    # 2. Track first seen index of each mask state (2^5 = 32 possible masks)
    #    Base state 0 (all vowels even) is seen before string starts at index -1
    first_seen = {0: -1}
    curr_mask = 0
    max_len = 0
    
    for i, ch in enumerate(s):
        # 3. Toggle bit if character is a vowel (XOR flips 0 <-> 1)
        if ch in vowel_mask:
            curr_mask ^= vowel_mask[ch]
            
        # 4. Invariant: if curr_mask was seen before at index j,
        #    the substring s[j+1..i] has even counts for all vowels
        if curr_mask in first_seen:
            max_len = max(max_len, i - first_seen[curr_mask])
        else:
            first_seen[curr_mask] = i
            
    return max_len
```

```csharp
public int FindTheLongestSubstring(string s) {
    // 1. Array of size 32 (2^5) to store first occurrence of each parity mask
    int[] firstSeen = new int[32];
    Array.Fill(firstSeen, -2);
    // 2. Base state mask 0 is seen before index 0 (at index -1)
    firstSeen[0] = -1;
    
    int currMask = 0;
    int maxLen = 0;
    
    for (int i = 0; i < s.Length; i++) {
        // 3. Toggle bit for corresponding vowel
        switch (s[i]) {
            case 'a': currMask ^= 1;  break;
            case 'e': currMask ^= 2;  break;
            case 'i': currMask ^= 4;  break;
            case 'o': currMask ^= 8;  break;
            case 'u': currMask ^= 16; break;
        }
        
        // 4. Invariant: if currMask was seen before at firstSeen[currMask],
        //    all vowels between that index and i have even frequency
        if (firstSeen[currMask] != -2) {
            maxLen = Math.Max(maxLen, i - firstSeen[currMask]);
        } else {
            firstSeen[currMask] = i;
        }
    }
    
    return maxLen;
}
```

### ⚠️ Gotchas & Pitfalls
- **Self-Inverse XOR Trap:** Unlike addition where `Sum(L..R) = P[R+1] - P[L]`, in XOR you do NOT subtract. You apply `^`: `P_xor[R+1] ^ P_xor[L]`.
- **Pre-allocating Bitmask Arrays:** In parity bitmask problems (like LC 1371), the number of states is fixed (`2^K` where `K` is tracked characters). Use a static array of size `32` initialized to `-2` instead of dynamic hash maps for zero allocation and maximum cache locality.
- **Base Case Initialization:** Always set `first_seen[0] = -1` before scanning. Without this, valid substrings starting from index 0 will be completely missed!


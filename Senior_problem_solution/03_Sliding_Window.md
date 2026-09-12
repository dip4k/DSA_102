# Phase 03: Sliding Window

> **Focus:** Fixed-Size Windows, Dynamic Variable-Size Windows, Frequency Counters, Fast Index Boundary Jumps, Rolling Validity Indicators, and Sparse String Pre-filtering.  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 3 (Problems #14–#18)

---

## 14. Maximum Average Subarray I (LeetCode #643)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⚪ Reinforcement |
| **Pattern Tags** | `#sliding-window` `#fixed-size` `#array` `#running-accumulator` |
| **LeetCode Link** | [Maximum Average Subarray I](https://leetcode.com/problems/maximum-average-subarray-i/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an integer array `nums` consisting of $n$ elements, and an integer $k$, find a contiguous subarray whose length is equal to $k$ that has the maximum average value, and return this value. Any answer with a calculation error less than $10^{-5}$ will be accepted.
- **Key Constraints:**
  - `n == nums.Length`
  - `1 <= k <= n <= 10^5`
  - `-10^4 <= nums[i] <= 10^4`
- **Senior Edge Cases to Defend:**
  - $k = n$: The entire array is the only window; no loop iterations beyond initial sum.
  - All negative numbers (e.g., `nums = [-5, -3, -10, -2]`, $k = 2$): Initializing `maxSum` to `0` or `double.MinValue` without accounting for negative sums causes fatal logic corruption. `maxSum` must be initialized strictly to the sum of the very first window.
  - Potential integer overflow: While $10^5 \times 10^4 = 10^9$ fits within a signed 32-bit integer, standardizing accumulators to `long` prevents intermediate overflow in high-throughput enterprise pipelines.
  - Floating-point division latency: Dividing by $k$ inside the hot sliding loop introduces $O(N)$ IEEE 754 floating-point divisions. Division must occur exactly **once** at method exit.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Track the cumulative sum of a fixed window of width $k$ by adding the entering element and subtracting the departing element in $O(1)$ per step.
- **Sample 1:**
  - **Input:** `nums = [1, 12, -5, -6, 50, 3]`, `k = 4`
  - **Window Evaluations:**
    - Window `[0..3]`: $1 + 12 + (-5) + (-6) = 2$
    - Window `[1..4]`: $2 + 50 - 1 = 51$
    - Window `[2..5]`: $51 + 3 - 12 = 42$
  - **Output:** `12.75000` (from $\max(2, 51, 42) / 4 = 51 / 4$)

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a rigid physical stencil or viewport of fixed width $k$ laid over a conveyor belt of numbers. As the conveyor belt moves one notch forward, exactly one number exits through the left slit of the stencil, and exactly one number enters through the right slit. The $k - 2$ numbers in the center remain completely unchanged. To know the new total weight inside the stencil, you do not weigh all $k$ numbers from scratch—you simply subtract the weight of the departing item and add the weight of the entering item.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force scan enumerates every valid start index $i \in [0, n - k]$ and sums the slice `nums[i .. i + k - 1]`:
$$\text{Sum}(i) = \sum_{j=i}^{i + k - 1} \text{nums}[j]$$
- Between consecutive windows starting at $i$ and $i + 1$, $k - 1$ elements overlap completely.
- Total additions performed: $(n - k + 1) \times k \approx O(N \cdot k)$. When $k = N / 2$, this degenerates to $O(N^2)$ operations ($5 \times 10^9$ ops for $N = 10^5$, exceeding typical 1-second CPU quotas).

#### 3.3 The Breakthrough Insight & Mathematical Invariant
Consecutive window sums satisfy a telescoping differential relation:
$$\text{Sum}_{i} = \text{Sum}_{i - 1} + \text{nums}[i] - \text{nums}[i - k]$$
Because the window size $k$ is strictly constant, maximizing the average $\frac{\text{Sum}_k}{k}$ is mathematically equivalent to maximizing the integer sum $\text{Sum}_k$. By maintaining a rolling scalar `windowSum`, every transition requires exactly **1 addition and 1 subtraction**, reducing work per element from $O(k)$ to $O(1)$.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
The array is partitioned at index $i \ge k$ into four distinct semantic regions:
```text
[ 0 ... i - k - 1 ]   [ i - k ]   [ (i - k + 1) ... i - 1 ]   [   i   ]   [ i + 1 ... N - 1 ]
└────────┬────────┘   └───┬───┘   └───────────┬───────────┘   └───┬───┘   └────────┬────────┘
   Evicted History    Departing       Retained Active         Entering        Unexamined
    (OutOfScope)       Element        Window Sub-Core         Element           Stream
```
- Cursor `i`: Leading edge explorer ingesting the next element into the window.
- Expression `i - k`: Trailing edge locator identifying the exact element to evict.
- Scalar `windowSum`: Maintains $\sum_{j = i - k + 1}^{i} \text{nums}[j]$ as an invariant before each boundary commit.

#### 3.5 State Transition Triggers & Decision Gates
1. **Bootstrap Phase ($0 \le i < k$):** Accumulate `windowSum += nums[i]`. Set `maxSum = windowSum`.
2. **Sliding Phase ($k \le i < n$):**
   - **Ingress & Egress Gate:** `windowSum = windowSum + nums[i] - nums[i - k]`.
   - **Extremum Gate:** If `windowSum > maxSum`, commit `maxSum = windowSum`.
3. **Termination Gate:** Loop completes when $i = n$. Return `(double)maxSum / k`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `nums = [1, 12, -5, -6, 50, 3]`, $k = 4$.

| Step / Index $i$ | Action | Element Entering | Element Leaving | `windowSum` Calc | `windowSum` | `maxSum` | Invariant Satisfied |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **0..3** | Init | `1, 12, -5, -6` | — | $1 + 12 - 5 - 6$ | `2` | `2` | Initial $k$-window valid |
| **4** | Slide | `50` (idx 4) | `1` (idx 0) | $2 + 50 - 1$ | `51` | `51` | $51 > 2 \implies$ Update |
| **5** | Slide | `3` (idx 5) | `12` (idx 1) | $51 + 3 - 12$ | `42` | `51` | $42 \le 51 \implies$ Retain |

Final Result: $51 / 4.0 = 12.75000$.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Rolling Differential Accumulator - Optimal):** Standard industrial choice. $O(N)$ time, $O(1)$ space. High cache locality; zero heap allocations.
- **Approach 2 (Prefix Sum Array):** Requires precomputing $P[i] = \sum_{j=0}^{i-1} nums[j]$ with $O(N)$ extra memory. Only justified if multiple range queries of arbitrary sizes $k_1, k_2$ are executed against a static array. For a single pass with fixed $k$, Approach 1 dominates.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Boundaries:** Guard against null or `nums.Length < k`.
- **Step 2: Bootstrap First Window:** Sum elements `0` to `k - 1` into a `long` accumulator. Initialize `maxSum = windowSum`.
- **Step 3: Sliding Exploration:** Loop $i$ from $k$ to $n - 1$. Apply the differential step `windowSum += nums[i] - nums[i - k]`. Update `maxSum = Math.Max(maxSum, windowSum)`.
- **Step 4: Single Precision Division:** Cast `maxSum` to `double` and divide by `k`.

#### 4.3 Alternative Approaches Analysis
- **Prefix Sum Alternative:** Construct array `prefix` of size $N + 1$. Query each window in $O(1)$ via `prefix[i + k] - prefix[i]`. Cost: $O(N)$ space allocation and an extra cache-thrashing memory traversal.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Dimension | Approach 1: Rolling Window (Optimal) | Approach 2: Prefix Sum Array |
| :--- | :--- | :--- |
| **Time (Best / Avg / Worst)** | `O(N)` / `O(N)` / `O(N)` | `O(N)` / `O(N)` / `O(N)` |
| **Auxiliary Space** | `O(1)` (stack scalars only) | `O(N)` (prefix buffer) |
| **Output Space** | `O(1)` | `O(1)` |
| **Cache Locality** | Optimal ($L1$ streaming sequential read) | Sub-optimal (2 array streams) |
| **In-Place Mutability** | Pure read-only | Pure read-only |
| **Streaming Suitability** | Yes (processes infinite continuous streams) | No (requires complete data upfront) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Maximum Average Subarray I (#643)
// Core Pattern: Fixed-Size Sliding Window (Differential Egress/Ingress)
// Target Complexity: O(N) Time, O(1) Auxiliary Space
// Key Invariant: windowSum[i] = windowSum[i-1] + nums[i] - nums[i-k]
// ============================================================================

public class Solution
{
    public double FindMaxAverage(int[] nums, int k)
    {
        // --------------------------------------------------------------------
        // Defensive Guard: Validate input integrity and boundary conditions
        // --------------------------------------------------------------------
        if (nums == null || nums.Length < k || k <= 0)
        {
            throw new ArgumentException("Array must contain at least k elements, and k must be positive.");
        }

        // --------------------------------------------------------------------
        // Step 1: Bootstrap the first fixed window [0 .. k - 1]
        // Use 64-bit integer to defend against 32-bit overflow under extreme inputs
        // --------------------------------------------------------------------
        long windowSum = 0;
        for (int i = 0; i < k; i++)
        {
            windowSum += nums[i];
        }

        // Anchor: Correctly handles arrays where all window sums are negative
        long maxSum = windowSum;

        // --------------------------------------------------------------------
        // Step 2: Slide the window from index k to n - 1
        // Maintain invariant: Exactly k elements exist in window at all times
        // --------------------------------------------------------------------
        for (int i = k; i < nums.Length; i++)
        {
            // O(1) Differential Update:
            // Add entering element at leading edge: nums[i]
            // Evict departing element at trailing edge: nums[i - k]
            windowSum += nums[i] - nums[i - k];

            // Branchless / direct comparison for maximum tracking
            if (windowSum > maxSum)
            {
                maxSum = windowSum;
            }
        }

        // --------------------------------------------------------------------
        // Step 3: Perform floating-point division exactly once at method exit
        // Prevents IEEE 754 precision drift and avoids O(N) hardware division ops
        // --------------------------------------------------------------------
        return (double)maxSum / k;
    }
}
```

---

## 15. Longest Substring Without Repeating Characters (LeetCode #3)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#sliding-window` `#variable-size` `#hash-table` `#last-seen-map` |
| **LeetCode Link** | [Longest Substring Without Repeating Characters](https://leetcode.com/problems/longest-substring-without-repeating-characters/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given a string `s`, find the length of the longest substring without duplicate characters.
- **Key Constraints:**
  - `0 <= s.Length <= 5 * 10^4`
  - `s` consists of English letters, digits, symbols, and spaces.
- **Senior Edge Cases to Defend:**
  - `s = ""` (empty string): Must immediately return `0`.
  - `s = " "` (single space) or `s = "a"`: Must return `1`.
  - All identical characters (e.g., `"bbbbb"`): Must return `1`.
  - All distinct characters (e.g., `"abcdef"`): Window expands to entire length $N$.
  - Characters outside standard lower ASCII: If non-ASCII or full Unicode characters are expected, a flat array must be carefully bounded or replaced by a hash map. For ASCII (0–127), stack allocation (`Span<int>`) provides zero-GC memory safety.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Maintain a dynamic window $[left, right]$ containing only unique characters. Jump $left$ directly past the previous occurrence of duplicate character $s[right]$.
- **Sample 1:**
  - **Input:** `s = "abcabcbb"` $\implies$ `3` (`"abc"`)
- **Sample 2:**
  - **Input:** `s = "pwwkew"` $\implies$ `3` (`"wke"`, note that `"pwke"` is a subsequence, not a contiguous substring)

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Picture an accordion viewport moving across the string. The front edge (`right`) constantly explores forward, stretching the viewport. Inside the viewport, every character must be unique. When `right` encounters a character that already exists within the active viewport, the viewport snags. Instead of shrinking the back edge (`left`) forward one character at a time like an inchworm, the back edge instantaneously snaps forward past the previous collision site.

#### 3.2 The Naive Bottleneck & Redundant Computation
The naive approach considers all $\frac{N(N+1)}{2}$ substrings. For each substring $s[i..j]$, it iterates through characters checking for duplicates using a nested loop or hash set:
- Complexity: $O(N^3)$ with nested checks, or $O(N^2)$ using an incremental set.
- Redundant Computation: When a duplicate character is found at index $j$ matching index $k$ ($i \le k < j$), any substring starting at $i, i+1, \dots, k$ and ending at $j$ is guaranteed to contain that duplicate. Testing those prefixes is entirely wasted work.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
Let $lastSeen[c]$ store the most recent 0-based index where character $c$ was observed. When cursor `right` visits character $c = s[right]$:
- If $lastSeen[c] \ge left$, the character $c$ has already appeared inside our active window $[left, right - 1]$.
- To restore uniqueness, $left$ MUST be moved to at least $lastSeen[c] + 1$.
- Any starting boundary $< lastSeen[c] + 1$ would still include the duplicate at $lastSeen[c]$.
- Mathematical Invariant:
  $$left_{new} = \max(left_{old}, lastSeen[c] + 1)$$
  The $\max$ operator ensures $left$ **never moves backward**, even if $lastSeen[c]$ references an old occurrence that was already evicted outside the current window!

#### 3.4 Cursor Semantics & Invariant Partition Architecture
```text
[ 0 ......... left - 1 ]   [ left ..................... right ]   [ right + 1 ......... N - 1 ]
└──────────┬───────────┘   └─────────────────┬────────────────┘   └─────────────┬─────────────┘
      Evicted Domain               Active Collision-Free               Unexplored Future
   (Contains duplicates              Window (All Unique)                    Stream
    of earlier chars)
```
- `right`: Fast explorer cursor advancing unconditionally from $0$ to $N - 1$.
- `left`: Slow boundary cursor maintaining the invariant that $s[left .. right]$ contains no duplicate characters.
- `lastSeen[128]`: Direct-mapped cache of character positions initialized to `-1`.

#### 3.5 State Transition Triggers & Decision Gates
1. **Encounter Gate:** Read $c = s[right]$.
2. **Jump Decision Gate:**
   - If $lastSeen[c] \ge left$: Collision detected! Update $left = lastSeen[c] + 1$.
   - If $lastSeen[c] < left$ or $-1$: No collision within active window; $left$ remains unchanged.
3. **Commit Gate:**
   - Cache current position: $lastSeen[c] = right$.
   - Update global maximum: $maxLength = \max(maxLength, right - left + 1)$.

#### 3.6 Concrete Step-by-Step State Trace
Input: `s = "abcabcbb"`

| $right$ | Char $s[right]$ | $lastSeen[c]$ | Collision in Window? | $left$ Action | Window Range | Window String | Length | $maxLength$ |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| **0** | `'a'` | `-1` | No | Retain $left=0$ | `[0..0]` | `"a"` | 1 | 1 |
| **1** | `'b'` | `-1` | No | Retain $left=0$ | `[0..1]` | `"ab"` | 2 | 2 |
| **2** | `'c'` | `-1` | No | Retain $left=0$ | `[0..2]` | `"abc"` | 3 | **3** |
| **3** | `'a'` | `0` | Yes ($0 \ge 0$) | Jump $left = 0 + 1 = 1$ | `[1..3]` | `"bca"` | 3 | 3 |
| **4** | `'b'` | `1` | Yes ($1 \ge 1$) | Jump $left = 1 + 1 = 2$ | `[2..4]` | `"cab"` | 3 | 3 |
| **5** | `'c'` | `2` | Yes ($2 \ge 2$) | Jump $left = 2 + 1 = 3$ | `[3..5]` | `"abc"` | 3 | 3 |
| **6** | `'b'` | `4` | Yes ($4 \ge 3$) | Jump $left = 4 + 1 = 5$ | `[5..6]` | `"cb"` | 2 | 3 |
| **7** | `'b'` | `6` | Yes ($6 \ge 5$) | Jump $left = 6 + 1 = 7$ | `[7..7]` | `"b"` | 1 | 3 |

Final Answer: `3`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Fast-Jump Last-Seen Map - Optimal):** The gold standard for production. Uses `Span<int>` allocated on the stack (`stackalloc int[128]`). Performs strictly $N$ iterations (one step per character). Zero GC pressure.
- **Approach 2 (Sliding Window with HashSet):** Uses `while (set.Contains(s[right])) { set.Remove(s[left++]); }`. Performs up to $2N$ pointer steps ($N$ for `right`, $N$ for `left`). Suffers from heap allocations and hash collision overhead.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup & Memory Allocation:** Handle empty string. Allocate 128 integers on stack via `stackalloc int[128]`; initialize with `-1`.
- **Step 2: Initialize Pointers:** `left = 0`, `maxLength = 0`.
- **Step 3: Exploration Loop:** Advance `right` from $0$ to $N - 1$.
  - Check if character was seen at or after `left`.
  - Update `left` if collision occurred.
  - Record `lastSeen[c] = right`.
  - Compute window width $right - left + 1$ and update `maxLength`.
- **Step 4: Return:** Return `maxLength`.

#### 4.3 Alternative Approaches Analysis
- **HashSet Step-by-Step:** Very easy to explain initially in an interview, but less optimal because `left` contracts incrementally: if a duplicate character is at the end of a 1,000-character window, the HashSet approach must perform 999 unnecessary removals and set lookups before reaching the duplicate.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Dimension | Approach 1: Fast-Jump Last-Seen Map | Approach 2: Sliding Window with HashSet |
| :--- | :--- | :--- |
| **Time (Best / Avg / Worst)** | `O(N)` / `O(N)` / `O(N)` (strictly $N$ loop steps) | `O(N)` / `O(N)` / `O(N)` (amortized $2N$ steps) |
| **Auxiliary Space** | `O(1)` (128 integers = 512 bytes on stack) | `O(min(N, M))` on heap ($M$ = alphabet size) |
| **GC Pressure** | Zero allocations | High allocation / deallocation per unique char |
| **Cache Locality** | Consecutive stack memory (fits in L1 cache) | Pointer indirection (hash bucket node traversal) |
| **In-Place Mutability** | Read-only | Read-only |
| **Streaming Suitability** | Yes (processes stream via index tracking) | Yes |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Longest Substring Without Repeating Characters (#3)
// Core Pattern: Variable-Size Sliding Window with Fast Index Boundary Jump
// Target Complexity: O(N) Strict Time, O(1) Auxiliary Stack Space
// Key Invariant: left = Math.Max(left, lastSeen[c] + 1)
// ============================================================================

public class Solution
{
    public int LengthOfLongestSubstring(string s)
    {
        // --------------------------------------------------------------------
        // Defensive Guard: Handle null and trivial empty string scenarios
        // --------------------------------------------------------------------
        if (string.IsNullOrEmpty(s))
        {
            return 0;
        }

        // --------------------------------------------------------------------
        // Allocate direct-mapped ASCII lookup table on the stack.
        // Size 128 covers standard printable ASCII. stackalloc guarantees ZERO GC heap pressure.
        // Initialize all slots to -1 indicating the character has not been observed.
        // --------------------------------------------------------------------
        Span<int> lastSeen = stackalloc int[128];
        lastSeen.Fill(-1);

        int left = 0;
        int maxLength = 0;

        // --------------------------------------------------------------------
        // Main Exploration Loop: Cursor 'right' advances strictly once per char
        // --------------------------------------------------------------------
        for (int right = 0; right < s.Length; right++)
        {
            char c = s[right];

            // Safety guard: support characters up to standard ASCII range
            if (c < 128)
            {
                int prevIndex = lastSeen[c];

                // Invariant Decision Gate:
                // If the character was previously seen at an index >= left, it is inside
                // the active window. We instantly jump 'left' past that collision point.
                // Stale occurrences (prevIndex < left) are naturally ignored.
                if (prevIndex >= left)
                {
                    left = prevIndex + 1;
                }

                // Update the most recent position of character c
                lastSeen[c] = right;
            }

            // Invariant: s[left .. right] is guaranteed to contain only unique characters
            int currentLength = right - left + 1;
            if (currentLength > maxLength)
            {
                maxLength = currentLength;
            }
        }

        return maxLength;
    }
}

// ----------------------------------------------------------------------------
// Approach 2: Sliding Window with HashSet (Standard 2N Step Contract)
// Provided for interview comparison and trade-off deconstruction
// ----------------------------------------------------------------------------
public class SolutionHashSet
{
    public int LengthOfLongestSubstring(string s)
    {
        if (string.IsNullOrEmpty(s)) return 0;

        var set = new HashSet<char>();
        int left = 0;
        int maxLength = 0;

        for (int right = 0; right < s.Length; right++)
        {
            // Contract left step-by-step until the duplicate character s[right] is evicted
            while (set.Contains(s[right]))
            {
                set.Remove(s[left]);
                left++;
            }

            set.Add(s[right]);
            maxLength = Math.Max(maxLength, right - left + 1);
        }

        return maxLength;
    }
}
```

---

## 16. Longest Repeating Character Replacement (LeetCode #424)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#sliding-window` `#variable-size` `#frequency-map` `#monotonic-expansion` |
| **LeetCode Link** | [Longest Repeating Character Replacement](https://leetcode.com/problems/longest-repeating-character-replacement/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given a string `s` and an integer `k`. You can choose any character of the string and change it to any other uppercase English character. You can perform this operation at most `k` times. Return the length of the longest substring containing the same letter you can get after performing the above operations.
- **Key Constraints:**
  - `1 <= s.Length <= 10^5`
  - `s` consists of only uppercase English letters (`'A'` through `'Z'`).
  - `0 <= k <= s.Length`
- **Senior Edge Cases to Defend:**
  - $k \ge s.Length$: We can replace every character; the answer is trivially $s.Length$.
  - $k = 0$: No replacements allowed; problem reduces to finding the longest contiguous run of identical characters.
  - All characters identical initially (e.g., `"AAAA"`, $k = 2$): Window expands across whole string without contraction.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Maintain a dynamic window $[left, right]$ where the number of non-dominant characters is $\le k$:
  $$\text{Replacements Needed} = (\text{Window Length}) - \text{Max Frequency} = (right - left + 1) - \text{maxFreq} \le k$$
- **Sample 1:**
  - **Input:** `s = "AABABBA"`, `k = 1`
  - **Output:** `4` (Substrings `"AABA"` or `"ABBA"` can both be transformed into 4 identical characters with 1 replacement).

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Think of an elastic band with a credit limit of $k$ wildcard tokens. As the band stretches to encompass more letters, one letter is chosen as the "king" (the dominant character with highest frequency in the window). Every other letter in the band must be converted to the king using wildcard tokens. If the number of peasants (non-king letters) exceeds our credit limit $k$, the band becomes invalid and must slide forward to shed debt.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute force approach inspects every pair $(i, j)$ with $0 \le i \le j < N$:
- Frequency count of 26 letters: $O(26)$.
- Find dominant letter frequency $M$: $O(26)$.
- Check if $(j - i + 1) - M \le k$.
- Total Time: $O(26 \cdot N^2)$. For $N = 10^5$, this requires $\approx 2.6 \times 10^{11}$ operations, resulting in a severe Time Limit Exceeded (TLE).

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**The Non-Decreasing Window & Historical Maximum Frequency Invariant:**
Do we need to recalculate `maxFreq` when the window shrinks? **No.**
This is one of the most elegant mathematical invariants in sliding window theory:
1. We only care about finding a window size *strictly greater* than the maximum window size observed so far.
2. Suppose at some point we attained a valid window of size $W$ with dominant frequency $M$.
3. If the window becomes invalid because $(W' - M') > k$, we shift `left++`. Even if the evicted element was part of the dominant character group (meaning the true local mode drops to $M' - 1$), **we do not decrement `maxFreq`**.
4. Why? Because a smaller local frequency $M' < M$ can NEVER produce a new global maximum window length! A new record window can only ever be established if some character's frequency strictly surpasses our historical peak `maxFreq`.
5. Therefore, leaving `maxFreq` at its historical peak is 100% sound. It acts as an optimistic monotonic filter.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
```text
[ 0 ... left - 1 ]   [ left .................................... right ]   [ right + 1 ... N - 1 ]
└────────┬───────┘   └────────────────────────┬────────────────────────┘   └──────────┬──────────┘
  Evicted History                Active Candidate Window                     Unexplored Future
                      Length L, Dominant Count M, Replacements = L - M <= k
```
- `right`: Extends window, increments frequency counter `counts[s[right] - 'A']`, and updates `maxFreq = max(maxFreq, counts[s[right] - 'A'])`.
- `left`: Evicts elements when replacement budget is exhausted.
- `counts[26]`: Frequency of each uppercase English letter in the active window.

#### 3.5 State Transition Triggers & Decision Gates
1. **Expansion Step:**
   - Ingest character $s[right]$: `counts[s[right] - 'A']++`.
   - Update historical peak: `maxFreq = Math.Max(maxFreq, counts[s[right] - 'A'])`.
2. **Validity Gate:**
   - Window size: $L = right - left + 1$.
   - Deficit: $\text{replacements} = L - \text{maxFreq}$.
   - If $\text{replacements} > k$: Window is invalid.
     - Decrement outgoing frequency: `counts[s[left] - 'A']--`.
     - Advance `left++`.
3. **Extremum Gate:**
   - `maxWindow = Math.Max(maxWindow, right - left + 1)`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `s = "AABABBA"`, `k = 1`.

| $right$ | Char | `counts` State | `maxFreq` | Window Length | Replacements $(L - M)$ | Valid? $(\le 1)$ | `left` Action | Active Window | $maxWindow$ |
| :---: | :---: | :--- | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| **0** | `'A'` | A:1 | 1 | 1 | $1 - 1 = 0$ | Yes | None ($left=0$) | `"A"` | 1 |
| **1** | `'A'` | A:2 | 2 | 2 | $2 - 2 = 0$ | Yes | None ($left=0$) | `"AA"` | 2 |
| **2** | `'B'` | A:2, B:1 | 2 | 3 | $3 - 2 = 1$ | Yes | None ($left=0$) | `"AAB"` | 3 |
| **3** | `'A'` | A:3, B:1 | 3 | 4 | $4 - 3 = 1$ | Yes | None ($left=0$) | `"AABA"` | **4** |
| **4** | `'B'` | A:3, B:2 | 3 | 5 | $5 - 3 = 2$ | **No** | Evict $s[0]$ ('A'), $left=1$ | `"ABAB"` | 4 |
| **5** | `'B'` | A:2, B:3 | 3 | 5 | $5 - 3 = 2$ | **No** | Evict $s[1]$ ('A'), $left=2$ | `"BABB"` | 4 |
| **6** | `'A'` | A:2, B:3 | 3 | 5 | $5 - 3 = 2$ | **No** | Evict $s[2]$ ('B'), $left=3$ | `"ABBA"` | 4 |

Final Answer: `4`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Dynamic Contraction via While Loop):** Standard intuitive implementation. When condition $(L - \text{maxFreq}) > k$ is violated, contracts `left` with a `while` loop until valid. Completely transparent and simple to reason about during an interview.
- **Approach 2 (Monotonic Non-Shrinking Window via If Branch):** Replaces `while` with `if`. Once the window reaches size $K$, it *never shrinks*; it only shifts or expands. At the end, the answer is simply $right - left$. Highly praised in senior architecture discussions for strictly $N$ branch executions.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Setup:** Allocate 26 integers on stack via `stackalloc int[26]`.
- **Step 2: Exploration:** Iterate `right` through $0 \dots N - 1$. Ingest character into frequency array.
- **Step 3: Track Maximum Frequency:** Update `maxFreq = Math.Max(maxFreq, counts[charIndex])`.
- **Step 4: Invariant Guard:** If $(right - left + 1) - maxFreq > k$, decrement `counts[s[left] - 'A']` and advance `left++`.
- **Step 5: Return:** Final maximum window length.

#### 4.3 Alternative Approaches Analysis
- **Binary Search on Window Length:** Can we binary search on the answer $L \in [1, N]$? Yes. For a fixed length $L$, slide a fixed window of size $L$ in $O(N)$ to verify if $(L - \max(freq)) \le k$. Overall time: $O(N \log N)$. Approach 1 ($O(N)$) strictly supersedes this, but mentioning the binary search demonstrates breadth of pattern recognition.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Dimension | Approach 1: Dynamic Shrinking Window | Approach 2: Monotonic Non-Shrinking Window |
| :--- | :--- | :--- |
| **Time Complexity** | `O(N)` amortized ($2N$ pointer steps) | `O(N)` strict ($N$ loop steps) |
| **Auxiliary Space** | `O(1)` (26 integers on stack) | `O(1)` (26 integers on stack) |
| **Branch Predictability** | Variable contraction in while loop | Completely uniform single-branch progression |
| **Code Readability** | Intuitive and standard | Subtle invariant requires deep proof |
| **Streaming Suitability** | Yes | Yes |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Longest Repeating Character Replacement (#424)
// Core Pattern: Variable Sliding Window with Monotonic Frequency Tracking
// Target Complexity: O(N) Time, O(1) Auxiliary Stack Space
// Key Invariant: (right - left + 1) - maxFreq <= k
// ============================================================================

public class Solution
{
    public int CharacterReplacement(string s, int k)
    {
        // --------------------------------------------------------------------
        // Defensive Guard: Handle empty string and trivial single char
        // --------------------------------------------------------------------
        if (string.IsNullOrEmpty(s)) return 0;
        if (s.Length <= k) return s.Length;

        // --------------------------------------------------------------------
        // Stack-allocated 26-integer table for uppercase English letters ('A' - 'Z')
        // Zero GC heap allocations, strictly cache-resident.
        // --------------------------------------------------------------------
        Span<int> charCounts = stackalloc int[26];

        int left = 0;
        int maxFreq = 0;
        int maxWindow = 0;

        // --------------------------------------------------------------------
        // Exploration Loop: Advance leading edge 'right'
        // --------------------------------------------------------------------
        for (int right = 0; right < s.Length; right++)
        {
            int charIndex = s[right] - 'A';
            charCounts[charIndex]++;

            // Update the historical maximum frequency achieved by any single character
            // inside the window. This value is monotonically non-decreasing.
            if (charCounts[charIndex] > maxFreq)
            {
                maxFreq = charCounts[charIndex];
            }

            // Invariant Check:
            // Current window length: (right - left + 1)
            // Number of characters that MUST be replaced: length - maxFreq
            // If replacements needed exceed our budget k, shrink window from the left.
            while ((right - left + 1) - maxFreq > k)
            {
                charCounts[s[left] - 'A']--;
                left++;
                // Note: We deliberately do NOT recompute maxFreq here.
                // A stale maxFreq will never overestimate a valid window of larger size!
            }

            // Window [left .. right] is now strictly valid
            int currentLength = right - left + 1;
            if (currentLength > maxWindow)
            {
                maxWindow = currentLength;
            }
        }

        return maxWindow;
    }
}

// ----------------------------------------------------------------------------
// Approach 2: Monotonic Non-Shrinking Window (Optimal O(N) Strict N Steps)
// Window expands when valid, slides without shrinking when invalid.
// ----------------------------------------------------------------------------
public class SolutionNonShrinking
{
    public int CharacterReplacement(string s, int k)
    {
        if (string.IsNullOrEmpty(s)) return 0;

        Span<int> counts = stackalloc int[26];
        int left = 0;
        int maxFreq = 0;

        for (int right = 0; right < s.Length; right++)
        {
            maxFreq = Math.Max(maxFreq, ++counts[s[right] - 'A']);

            // If invalid, slide the whole window rightward by 1 step without shrinking
            if ((right - left + 1) - maxFreq > k)
            {
                counts[s[left] - 'A']--;
                left++;
            }
        }

        // At loop termination, the window size (s.Length - left) is guaranteed to equal maxWindow
        return s.Length - left;
    }
}
```

---

## 17. Permutation in String (LeetCode #567)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#sliding-window` `#fixed-size` `#frequency-matching` `#scalar-matches` |
| **LeetCode Link** | [Permutation in String](https://leetcode.com/problems/permutation-in-string/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given two strings `s1` and `s2`, return `true` if `s2` contains a permutation of `s1`, or `false` otherwise. In other words, return `true` if one of `s1`'s permutations is the substring of `s2`.
- **Key Constraints:**
  - `1 <= s1.Length, s2.Length <= 10^4`
  - `s1` and `s2` consist of lowercase English letters.
- **Senior Edge Cases to Defend:**
  - `s1.Length > s2.Length`: A larger string can never be a substring of a shorter string; return `false` immediately.
  - `s1.Length == 1`: Simple character search; sliding window of size 1.
  - All identical characters in `s1` (e.g. `s1 = "aaa"`, `s2 = "aaaa"`): Must match exact frequency counts, not merely unique presence.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** A fixed-size sliding window of width $m = s1.Length$ over $s2$. The multiset of character counts in the window must match $s1$ exactly.
- **Sample 1:**
  - **Input:** `s1 = "ab"`, `s2 = "eidbaooo"` $\implies$ `true` (`s2` contains `"ba"` which is a permutation of `s1`).
- **Sample 2:**
  - **Input:** `s1 = "ab"`, `s2 = "eidboaoo"` $\implies$ `false`.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a mechanical sorting tray with 26 distinct slots (one for each letter 'a' through 'z'). `s1` defines an exact target quota for each slot. We place a sliding frame of fixed width $m = |s1|$ onto $s2$. Instead of checking all 26 slots from scratch every time the frame moves, we maintain a dashboard tally of `matches`—the count of slots that currently hold their exact target quota. When the frame shifts, only two slots are disturbed: the slot of the departing letter and the slot of the entering letter. If the dashboard tally reaches 26, the chemistry is perfect.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute force approach extracts all $(N - m + 1)$ substrings of length $m$ from $s2$, sorts each substring, and compares it to sorted $s1$:
- Sorting cost: $O((N - m) \cdot m \log m)$.
- Even with frequency arrays, comparing two 26-element arrays on every slide takes $26 \times (N - m)$ operations. While technically $O(N)$, performing 26 array lookups per iteration introduces noticeable overhead.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**The Scalar `matches` Invariant (Strict $O(1)$ Transition):**
Instead of scanning 26 integers on every slide, we track a scalar integer `matches` representing how many of the 26 lowercase letters have identical counts in `s1Map` and `s2Map`:
1. Initially, if character `'c'` has identical frequency in both maps (even if that frequency is 0), `matches` increments by 1.
2. During sliding, an entering character `inChar` is incremented in `s2Map`:
   - If `s2Map[inChar] == s1Map[inChar]`: It just reached perfection! Increment `matches++`.
   - If `s2Map[inChar] == s1Map[inChar] + 1`: It just exceeded perfection! Decrement `matches--`.
3. A departing character `outChar` is decremented in `s2Map`:
   - If `s2Map[outChar] == s1Map[outChar]`: It just returned to perfection from an excess! Increment `matches++`.
   - If `s2Map[outChar] == s1Map[outChar] - 1`: It just dropped below perfection! Decrement `matches--`.
4. If `matches == 26`, all 26 character frequencies match identically $\implies$ permutation confirmed in $O(1)$ time!

#### 3.4 Cursor Semantics & Invariant Partition Architecture
```text
[ 0 ... i - m - 1 ]   [ i - m ]   [ (i - m + 1) ... i - 1 ]   [   i   ]   [ i + 1 ... N - 1 ]
└────────┬────────┘   └───┬───┘   └───────────┬───────────┘   └───┬───┘   └────────┬────────┘
   Evicted History    Departing       Retained Stride         Entering        Unexamined
                       Element         (State Preserved)       Element          Stream
```
- `i`: Current entering index in $s2$.
- `i - m`: Departing index in $s2$.
- `matches`: Scalar value $\in [0, 26]$. Condition `matches == 26` is the universal success trigger.

#### 3.5 State Transition Triggers & Decision Gates
1. **Initial Bootstrap ($0 \le i < m$):**
   - Populate `s1Map` and `s2Map` for the first window.
   - Count initial matching bins: for each $c \in [0, 25]$, if `s1Map[c] == s2Map[c]`, `matches++`.
   - If `matches == 26`, return `true`.
2. **Sliding Loop ($m \le i < N$):**
   - Ingress $s2[i]$ $\implies$ update `s2Map` and adjust `matches`.
   - Egress $s2[i - m]$ $\implies$ update `s2Map` and adjust `matches`.
   - Decision Gate: If `matches == 26`, return `true` immediately.
3. **Termination:** Return `false`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `s1 = "ab"`, `s2 = "eidbaooo"`. $m = 2$.
Target `s1Map`: `a:1, b:1`, all other 24 chars: `0`.

| Window / Step $i$ | Ingress Char | Egress Char | Frequency Deltas | `matches` Update | Total `matches` | Permutation Found? |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| **Initial (0..1)** `"ei"` | `e, i` | — | `e:1, i:1` | 'e' drops, 'i' drops | 24 | No ($24 \ne 26$) |
| **$i = 2$** `"id"` | `d` (idx 2) | `e` (idx 0) | `d:1`, `e:0` | 'e' restored (+1), 'd' broken (-1) | 24 | No |
| **$i = 3$** `"db"` | `b` (idx 3) | `i` (idx 1) | `b:1`, `i:0` | 'i' restored (+1), 'b' matched (+1) | 26 - 1 = 25 | No |
| **$i = 4$** `"ba"` | `a` (idx 4) | `d` (idx 2) | `a:1`, `d:0` | 'd' restored (+1), 'a' matched (+1) | **26** | **YES! Return true** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Optimal Scalar Match Counter):** Employs the `matches` invariant to achieve strict $O(1)$ operations per slide. Zero overhead; strictly 1 array write and 2 scalar checks per step.
- **Approach 2 (26-Bin Array Equality Check):** At each step, runs `s1Map.SequenceEqual(s2Map)`. Slightly simpler code, but executes $26$ operations per step.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Edge Validation:** If `s1.Length > s2.Length`, return `false`.
- **Step 2: Initialize Maps:** Allocate two 26-integer spans on the stack.
- **Step 3: Seed First Window:** Count character frequencies for $s1$ and the first $m$ characters of $s2$.
- **Step 4: Seed Matches Counter:** Iterate $0 \dots 25$ and count bins where `s1Map[i] == s2Map[i]`. If `matches == 26`, return `true`.
- **Step 5: Sliding Invariant Loop:** Iterate $i = m \dots s2.Length - 1$. Apply differential updates to `matches` for `inChar` and `outChar`. Check `matches == 26`.

#### 4.3 Alternative Approaches Analysis
- **Prime Product Hashing (Gödel Numbering):** Map each character to a prime number; compute the product modulo a large integer. Permutations yield identical products. However, handling integer overflows and hash collisions makes it fragile and non-idiomatic compared to the deterministic scalar match counter.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Dimension | Approach 1: Scalar Match Counter (Optimal) | Approach 2: 26-Bin Sequence Equality |
| :--- | :--- | :--- |
| **Time Complexity** | `O(|s1| + |s2|)` (strictly $O(1)$ per slide) | `O(|s1| + 26 \times |s2|)` |
| **Auxiliary Space** | `O(1)` (52 integers on stack) | `O(1)` (52 integers on stack) |
| **Operation Count / Slide** | $\le 4$ integer comparisons | $26$ integer comparisons |
| **Cache Locality** | Pure stack L1 cache | Pure stack L1 cache |
| **Streaming Suitability** | Yes | Yes |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Permutation in String (#567)
// Core Pattern: Fixed Sliding Window with O(1) Scalar Match Counter
// Target Complexity: O(|s1| + |s2|) Time, O(1) Auxiliary Stack Space
// Key Invariant: matches == 26 <=> Current window is a valid permutation
// ============================================================================

public class Solution
{
    public bool CheckInclusion(string s1, string s2)
    {
        // --------------------------------------------------------------------
        // Defensive Guard: A longer pattern can never be contained in a shorter string
        // --------------------------------------------------------------------
        if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2) || s1.Length > s2.Length)
        {
            return false;
        }

        // Stack-allocate frequency maps for lowercase English characters ('a' - 'z')
        Span<int> s1Map = stackalloc int[26];
        Span<int> s2Map = stackalloc int[26];
        int k = s1.Length;

        // --------------------------------------------------------------------
        // Step 1: Bootstrap the initial window of size k = s1.Length
        // --------------------------------------------------------------------
        for (int i = 0; i < k; i++)
        {
            s1Map[s1[i] - 'a']++;
            s2Map[s2[i] - 'a']++;
        }

        // --------------------------------------------------------------------
        // Step 2: Initialize the scalar 'matches' counter
        // Represents how many of the 26 character classes have identical counts
        // --------------------------------------------------------------------
        int matches = 0;
        for (int i = 0; i < 26; i++)
        {
            if (s1Map[i] == s2Map[i])
            {
                matches++;
            }
        }

        // If the initial window matches perfectly, terminate immediately
        if (matches == 26) return true;

        // --------------------------------------------------------------------
        // Step 3: Slide window across s2 from index k to s2.Length - 1
        // Maintain 'matches' in strict O(1) time without scanning all 26 bins
        // --------------------------------------------------------------------
        for (int i = k; i < s2.Length; i++)
        {
            int inChar = s2[i] - 'a';
            int outChar = s2[i - k] - 'a';

            // --- Process Ingress Character (nums[i]) ---
            s2Map[inChar]++;
            if (s2Map[inChar] == s1Map[inChar])
            {
                // Count became equal to target
                matches++;
            }
            else if (s2Map[inChar] == s1Map[inChar] + 1)
            {
                // Count just exceeded target; match broken
                matches--;
            }

            // --- Process Egress Character (nums[i - k]) ---
            s2Map[outChar]--;
            if (s2Map[outChar] == s1Map[outChar])
            {
                // Count dropped back down to exact target
                matches++;
            }
            else if (s2Map[outChar] == s1Map[outChar] - 1)
            {
                // Count dropped below target; match broken
                matches--;
            }

            // Invariant Gate: If all 26 character counts align, permutation is found
            if (matches == 26)
            {
                return true;
            }
        }

        return false;
    }
}
```

---

## 18. Minimum Window Substring (LeetCode #76)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#sliding-window` `#variable-size` `#frequency-map` `#match-counter` `#sparse-filtering` |
| **LeetCode Link** | [Minimum Window Substring](https://leetcode.com/problems/minimum-window-substring/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given two strings `s` and `t` of lengths `m` and `n` respectively, return the minimum window substring of `s` such that every character in `t` (including duplicates) is included in the window. If there is no such substring, return the empty string `""`.
- **Key Constraints:**
  - `m == s.Length, n == t.Length`
  - `1 <= m, n <= 10^5`
  - `s` and `t` consist of uppercase and lowercase English letters.
- **Senior Edge Cases to Defend:**
  - `m < n`: Impossible to contain all characters of `t`; return `""` immediately.
  - `s == t`: Returns `s`.
  - Duplicate target characters (e.g. `t = "AABC"`): The window must contain at least two `'A'`s, one `'B'`, and one `'C'`.
  - No matching window exists: Must return `""` (avoid off-by-one errors returning full string).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Two-pointer variable-size sliding window. Expand `right` to satisfy target multiset requirements; contract `left` to discard non-essential slack and discover minimal enclosing bounds.
- **Sample 1:**
  - **Input:** `s = "ADOBECODEBANC"`, `t = "ABC"` $\implies$ `"BANC"`
  - **Sample 2:**
  - **Input:** `s = "a"`, `t = "aa"` $\implies$ `""`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Picture a cargo net (`[left, right]`) sweeping along a terrain to collect a specific list of expedition gear (`t`). 
- **Expansion Phase (Hunting):** The front runner (`right`) sweeps forward aggressively, tossing all found items into the cart until every item on the manifest has reached its required quota.
- **Contraction Phase (Pruning Slack):** The rear guard (`left`) immediately steps up to throw out surplus items—items not on the manifest, or items we have accumulated in excess. The rear guard contracts as far right as possible until discarding one more item would breach the manifest requirements.
- At that precise inflection point, the net is at its local minimal valid tension! We record its length and coordinates, step the rear guard forward by 1 (creating a deficit), and command the front runner to resume hunting.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute force approach considers all $O(M^2)$ substrings of $s$. For each substring, it builds a frequency map and verifies whether it covers target multiset $t$ in $O(M + N)$ time:
- Total Time: $O(M^3)$. With $M = 10^5$, $10^{15}$ operations would take days to complete.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
**The Distinct Character Match Counter (`formed == required`):**
Instead of comparing full frequency arrays on every contraction:
1. `required`: The number of *unique* characters in $t$.
2. `formed`: The number of unique characters whose count in the active window currently meets or exceeds the required count in $t$.
3. When `windowCounts[c] == targetCounts[c]`, `formed++`.
4. The window is valid if and only if `formed == required`.
5. Once valid, any extension of $right$ can only make the window longer (worse). Therefore, we are mathematically guaranteed that the current `right` cannot produce any smaller valid window starting at `left`. We can safely contract `left` until validity is broken.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
```text
[ 0 ......... left - 1 ]   [ left ..................... right ]   [ right + 1 ......... M - 1 ]
└──────────┬───────────┘   └─────────────────┬────────────────┘   └─────────────┬─────────────┘
      Evicted Domain              Active Valid / Contracting            Unexplored Domain
   (Surplus or Deficit              Window (formed == required)              (Pending Scan)
     Discarded Items)
```
- `right`: Scans forward to satisfy character deficits.
- `left`: Contracts to prune excess characters and minimize window span.
- `bestStart`, `minLen`: Tracks the absolute minimum window slice found across the entire traversal.

#### 3.5 State Transition Triggers & Decision Gates
1. **Ingress Gate ($right$ advances):**
   - Ingest character $c = s[right]$ into `windowMap`.
   - If $targetMap[c] > 0$ and $windowMap[c] == targetMap[c]$: `formed++`.
2. **Contraction Loop Gate (`formed == required`):**
   - Check if current window $(right - left + 1) < minLen$: Update `minLen = right - left + 1`, `bestStart = left`.
   - Evict $s[left]$ from `windowMap`.
   - If $targetMap[s[left]] > 0$ and $windowMap[s[left]] < targetMap[s[left]]$: `formed--`.
   - Increment `left++`.
3. **Termination Gate:** When $right = M$, return `minLen == int.MaxValue ? "" : s.Substring(bestStart, minLen)`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `s = "ADOBECODEBANC"`, `t = "ABC"`. Target: `A:1, B:1, C:1`. `required = 3`.

| $right$ | Char $s[right]$ | `formed` | Valid? (`== 3`) | Contraction Actions ($left$) | Candidate Window | Min Window So Far |
| :---: | :---: | :---: | :---: | :--- | :---: | :---: |
| **0..4** | `A,D,O,B,E` | 2 (A, B) | No | None ($left=0$) | — | — |
| **5** | `'C'` | **3** (A, B, C) | **Yes** | $left=0$ (A) cannot be dropped; drops break `formed` | `[0..5]` (`"ADOBEC"`, len 6) | `"ADOBEC"` (len 6) |
| **6..8** | `O,D,E` | 2 | No | None ($left=1$) | — | `"ADOBEC"` |
| **9** | `'B'` | 2 | No | None ($left=1$) | — | `"ADOBEC"` |
| **10** | `'A'` | **3** | **Yes** | Drop D,O,B,E,C; $left$ reaches 6 | `[5..10]` (`"CODEBA"`, len 6) | `"ADOBEC"` |
| **11** | `'N'` | 2 | No | None | — | `"ADOBEC"` |
| **12** | `'C'` | **3** | **Yes** | Drop O,D,E; $left$ reaches 9 ('B'). $left=9$: `"BANC"` (len 4) | `[9..12]` (`"BANC"`, len 4) | **`"BANC"` (len 4)** |

Final Minimum Window: `"BANC"`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1 (Direct Stack-Allocated ASCII Array - Optimal):** Uses `Span<int>` on the stack for direct character frequency indexing. Highly efficient, zero heap allocations, $O(M + N)$ time.
- **Approach 2 (Filtered List Optimization for Sparse Strings):** When $|s| \gg |t|$ and only a tiny fraction of characters in $s$ match $t$ (e.g. $s$ has length $10^6$ but only 100 characters belong to $t$), pre-filtering $s$ into a list of `(index, char)` pairs reduces the sliding window domain from $10^6$ to 100 elements.

#### 4.2 Step-by-Step Natural Progression Flow
- **Step 1: Input Validation:** Return `""` if $s$ is shorter than $t$.
- **Step 2: Build Target Frequency Table:** Populate target character counts and compute `required` (distinct characters in $t$).
- **Step 3: Sliding Expansion:** Loop `right` through $s$. Update window count and increment `formed` when a character's requirement is precisely met.
- **Step 4: Contraction & Optimization:** While `formed == required`, update `minLen` and `bestStart`, then decrement counts and advance `left`.
- **Step 5: Substring Generation:** Return `s.Substring(bestStart, minLen)` or `""`.

#### 4.3 Alternative Approaches Analysis
- **Binary Search on Window Size:** Not applicable here because validity is non-monotonic with respect to fixed length (a valid window of size $L$ does not imply every window of size $L+1$ is valid without matching the exact multiset).

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Dimension | Approach 1: Direct ASCII Sliding Window | Approach 2: Filtered String Optimization |
| :--- | :--- | :--- |
| **Time Complexity** | `O(|S| + |T|)` | `O(|S| + |T|)` (significantly faster if $|S_{filtered}| \ll |S|$) |
| **Auxiliary Space** | `O(1)` (128 integers on stack) | `O(|S_{filtered}|)` heap memory |
| **GC Pressure** | Zero heap memory | Allocates tuple list |
| **Cache Locality** | Sequential string memory read | Array indirection through filtered list |
| **Streaming Suitability** | Yes | No (requires pre-filtering pass) |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Minimum Window Substring (#76)
// Core Pattern: Variable Sliding Window with Multiset Match Counter
// Target Complexity: O(|S| + |T|) Time, O(1) Auxiliary Stack Space
// Key Invariant: formed == required => Window contains all target characters
// ============================================================================

public class Solution
{
    public string MinWindow(string s, string t)
    {
        // --------------------------------------------------------------------
        // Defensive Guard: Validate string boundaries
        // --------------------------------------------------------------------
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t) || s.Length < t.Length)
        {
            return string.Empty;
        }

        // Stack-allocated ASCII lookup arrays (0-127 covers all upper/lower ASCII)
        Span<int> targetMap = stackalloc int[128];
        int requiredMatches = 0;

        // --------------------------------------------------------------------
        // Step 1: Build target frequency table
        // 'requiredMatches' tracks the number of DISTINCT characters in t to satisfy
        // --------------------------------------------------------------------
        foreach (char c in t)
        {
            if (targetMap[c] == 0)
            {
                requiredMatches++;
            }
            targetMap[c]++;
        }

        Span<int> windowMap = stackalloc int[128];
        int formedMatches = 0;

        int left = 0;
        int minLen = int.MaxValue;
        int bestStart = 0;

        // --------------------------------------------------------------------
        // Step 2: Expand the window using cursor 'right'
        // --------------------------------------------------------------------
        for (int right = 0; right < s.Length; right++)
        {
            char rightChar = s[right];
            windowMap[rightChar]++;

            // If current character matches target quota exactly, increment formed counter
            if (targetMap[rightChar] > 0 && windowMap[rightChar] == targetMap[rightChar])
            {
                formedMatches++;
            }

            // ----------------------------------------------------------------
            // Step 3: Contract 'left' boundary while the window remains fully valid
            // Invariant: formedMatches == requiredMatches guarantees all characters
            // of t are present in [left .. right] with required frequencies.
            // ----------------------------------------------------------------
            while (formedMatches == requiredMatches)
            {
                int currentLen = right - left + 1;

                // Record new minimal window coordinates
                if (currentLen < minLen)
                {
                    minLen = currentLen;
                    bestStart = left;
                }

                // Evict character at trailing edge
                char leftChar = s[left];
                windowMap[leftChar]--;

                // If eviction causes window count to fall below required quota, break validity
                if (targetMap[leftChar] > 0 && windowMap[leftChar] < targetMap[leftChar])
                {
                    formedMatches--;
                }

                left++;
            }
        }

        // --------------------------------------------------------------------
        // Step 4: Extract and return the minimal window substring
        // --------------------------------------------------------------------
        return minLen == int.MaxValue ? string.Empty : s.Substring(bestStart, minLen);
    }
}

// ----------------------------------------------------------------------------
// Approach 2: Filtered List Sliding Window
// Recommended when |s| >>> |t| and matching characters are extremely sparse.
// ----------------------------------------------------------------------------
public class SolutionFilteredString
{
    public string MinWindow(string s, string t)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t) || s.Length < t.Length)
        {
            return string.Empty;
        }

        var targetMap = new Dictionary<char, int>();
        foreach (char c in t)
        {
            targetMap[c] = targetMap.GetValueOrDefault(c, 0) + 1;
        }

        // Pre-filter: extract only matching characters and their original indices
        var filtered = new List<(int Index, char CharVal)>();
        for (int i = 0; i < s.Length; i++)
        {
            if (targetMap.ContainsKey(s[i]))
            {
                filtered.Add((i, s[i]));
            }
        }

        int required = targetMap.Count;
        var windowCounts = new Dictionary<char, int>();
        int formed = 0;
        int l = 0;
        int minLen = int.MaxValue;
        int bestStart = 0;

        for (int r = 0; r < filtered.Count; r++)
        {
            char c = filtered[r].CharVal;
            windowCounts[c] = windowCounts.GetValueOrDefault(c, 0) + 1;

            if (windowCounts[c] == targetMap[c])
            {
                formed++;
            }

            while (formed == required && l <= r)
            {
                int startIdx = filtered[l].Index;
                int endIdx = filtered[r].Index;
                int len = endIdx - startIdx + 1;

                if (len < minLen)
                {
                    minLen = len;
                    bestStart = startIdx;
                }

                char leftChar = filtered[l].CharVal;
                windowCounts[leftChar]--;
                if (windowCounts[leftChar] < targetMap[leftChar])
                {
                    formed--;
                }

                l++;
            }
        }

        return minLen == int.MaxValue ? string.Empty : s.Substring(bestStart, minLen);
    }
}
```

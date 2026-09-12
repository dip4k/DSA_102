# Phase 02: Two Pointers

> **Focus:** Sorted Arrays, Opposing Pointers, Eliminating Sub-Optimal Hyperplanes, Boundary Shrinking, In-Place Invariants, and K-Sum Deduplication.  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 2 (Problems #8–#13)

---


## 8. Valid Palindrome (LeetCode #125)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟢 Easy |
| **Priority** | ⚪ Reinforcement |
| **Pattern Tags** | `#two-pointers` `#opposing-pointers` `#string` `#in-place` `#zero-allocation` |
| **LeetCode Link** | [Valid Palindrome](https://leetcode.com/problems/valid-palindrome/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** A phrase is a palindrome if, after converting all uppercase letters into lowercase letters and removing all non-alphanumeric characters, it reads the same forward and backward. Given a string `s`, return `true` if it is a palindrome, or `false` otherwise.
- **Assumptions & Contracts:**
  - Alphanumeric characters include lowercase letters, uppercase letters, and ASCII digits (`0-9`).
  - An empty string or a string consisting entirely of non-alphanumeric characters is trivially a palindrome (`true`).
- **Key Constraints:**
  - `1 <= s.Length <= 2 * 10^5`
  - `s` consists only of printable ASCII characters.
- **Senior Edge Cases to Defend:**
  - Punctuation/Space only string: `s = "., "` $\implies$ `true`. Pointers must converge and cross without out-of-bounds exceptions.
  - Single alphanumeric character: `s = "a."` $\implies$ `true`.
  - Mismatch at extremities: `s = "ab0ba"` vs `s = "ab0ca"`.
  - Case folding: `'A'` must match `'a'`.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Verify symmetric reflection across the string center while dynamically filtering non-alphanumerics in-place without memory allocation.
- **Sample 1:**
  - **Input:** `s = "A man, a plan, a canal: Panama"`
  - **Output:** `true`
  - **Explanation:** Filtered lowercase string is `"amanaplanacanalpanama"`, which reads identically forward and backward.
- **Sample 2:**
  - **Input:** `s = "race a car"`
  - **Output:** `false`
  - **Explanation:** Filtered string is `"raceacar"`, which is not a palindrome.

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine two quality-control inspectors standing at opposite ends of a long conveyor belt holding manufactured parts interspersed with packing peanuts and dust (spaces and punctuation). The two inspectors walk inward toward each other.
Whenever an inspector encounters packing peanuts or dust, they simply kick it aside and take another step forward without bothering the other inspector.
Only when *both* inspectors are standing on an actual manufactured part (letter or digit) do they halt, compare their parts via radio, and verify that they are identical (case-insensitively).
If the parts match, both take one step inward. If the parts mismatch, they immediately stop the line (`return false`). If the inspectors meet or cross in the middle with zero mismatches, the assembly is certified symmetrical (`return true`).

#### 3.2 The Naive Bottleneck & Redundant Computation
- **Naive Filtering & Reversal:**
  ```csharp
  string filtered = Regex.Replace(s, "[^a-zA-Z0-9]", "").ToLower();
  string reversed = new string(filtered.Reverse().ToArray());
  return filtered == reversed;
  ```
- **Why Naive Wastes CPU Cycles & Memory:**
  1. *Excess Heap Memory:* Allocates multiple auxiliary objects (regex match buffers, filtered string, char array, reversed string) totaling several megabytes for $N = 2 \times 10^5$.
  2. *Garbage Collection Churn:* Triggers Gen-0 / Gen-1 GC collections under high throughput.
  3. *Unnecessary Traversals:* The naive approach processes the entire string 3 to 4 times, even if the very first and last characters mismatch (e.g., `"a... (200k chars) ...z"`), where an optimal algorithm terminates in $O(1)$ time on step 1!

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **In-Place Read-Only Convergence:** By maintaining two pointers `left = 0` and `right = s.Length - 1` directly over the input string, we eliminate all intermediate string allocations, achieving strictly $O(1)$ auxiliary space.
- **Symmetric Reflection Invariant:** At any state $(left, right)$, the alphanumeric sub-sequence of the traversed prefix $s[0 \dots left - 1]$ is guaranteed to be an exact case-insensitive reverse of the traversed suffix $s[right + 1 \dots N - 1]$.
- **Short-Circuit Safety:** The first mismatch encountered between valid alphanumeric characters invalidates the palindrome property across the entire phrase. Exiting immediately is 100% safe.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
- `left`: Pointer advancing $0 \to$ center, scanning for next valid alphanumeric character.
- `right`: Pointer advancing $N - 1 \to$ center, scanning for next valid alphanumeric character.

```text
[ 0 ......... left - 1 ]   [ left ......... right ]   [ right + 1 ......... N - 1 ]
└──────────┬───────────┘   └──────────┬───────────┘   └─────────────┬─────────────┘
    Validated Symmetric         Active Candidate          Validated Symmetric
      Alphanumerics               Search Space               Alphanumerics
```

- **Invariant:**
  1. $0 \le left \le right + 1 \le N$.
  2. Filtered characters in prefix $[0 \dots left - 1]$ match suffix $[right + 1 \dots N - 1]$ in reverse order.

#### 3.5 State Transition Triggers & Decision Gates
1. **Decision Gate 1 (Left Skip):** If `!char.IsLetterOrDigit(s[left])`, increment `left++`. (Guarded by `left < right`).
2. **Decision Gate 2 (Right Skip):** If `!char.IsLetterOrDigit(s[right])`, decrement `right--`. (Guarded by `left < right`).
3. **Decision Gate 3 (Alphanumeric Match):**
   - Both `s[left]` and `s[right]` are alphanumeric.
   - If `char.ToLowerInvariant(s[left]) != char.ToLowerInvariant(s[right])`: return `false` immediately.
   - Else: advance both `left++` and `right--`.
4. **Resolution Gate:** If `left >= right`, return `true`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `s = "A m, a: m a"` ($N = 11$).

| Step | `left` | `s[left]` | `right` | `s[right]` | Action & State Transition | Invariant Check |
| :---: | :---: | :---: | :---: | :---: | :--- | :--- |
| 1 | 0 | `'A'` | 10 | `'a'` | Both alphanumeric. `'a' == 'a'`. | Match! `left++ (1), right-- (9)` |
| 2 | 1 | `' '` | 9 | `'m'` | `s[left]` is space $\implies$ skip | `left++ (2)` |
| 3 | 2 | `'m'` | 9 | `'m'` | Both alphanumeric. `'m' == 'm'`. | Match! `left++ (3), right-- (8)` |
| 4 | 3 | `','` | 8 | `' '` | `s[left]` is comma $\implies$ skip | `left++ (4)` |
| 5 | 4 | `' '` | 8 | `' '` | `s[left]` is space $\implies$ skip | `left++ (5)` |
| 6 | 5 | `'a'` | 8 | `' '` | `s[right]` is space $\implies$ skip | `right-- (7)` |
| 7 | 5 | `'a'` | 7 | `':'` | `s[right]` is colon $\implies$ skip | `right-- (6)` |
| 8 | 5 | `'a'` | 6 | `'a'` | Both alphanumeric. `'a' == 'a'`. | Match! `left++ (6), right-- (5)` |
| 9 | 6 | - | 5 | - | Loop termination: `left > right` | **Return `true`** |

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: In-Place Opposing Pointers (Optimal & Standard)**
  - *When to Use:* Always. Strictly $O(N)$ runtime, strictly $O(1)$ auxiliary space, zero heap allocation.
  - *Cache Locality:* Sequential memory traversal from boundaries inward guarantees high L1 cache hit rate.
- **Approach 2: Filtered StringBuilder (Pedagogical Alternative)**
  - *When to Use:* When input string needs to be permanently cleaned and stored for downstream processing, or when explaining baseline string manipulation.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Step 1: Setup:** Handle null or empty string guards. Initialize `left = 0`, `right = s.Length - 1`.
2. **Step 2: Main Convergence Loop:** Run `while (left < right)`.
3. **Step 3: Skip Non-Alphanumerics:** Inner while loops advance `left` and decrement `right` while maintaining `left < right`.
4. **Step 4: Character Comparison:** Compare case-folded characters. Return `false` on discrepancy; advance both on equality.
5. **Step 5: Resolution:** Return `true` when pointers cross.

#### 4.3 Alternative Approaches Analysis
- **Regex Cleaning:** Using `Regex.Replace` incurs significant regex compilation and DFA state machine overhead. For $200,000$ characters, regex takes up to 50x longer than simple pointer inspection.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. In-Place Pointers** | $O(1)$ | $O(N)$ | $O(N)$ | $O(1)$ | $O(1)$ | High | Non-mutating | Moderate (bidirectional) |
| **2. StringBuilder** | $O(N)$ | $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ | Moderate | Allocates new string | Poor |
| **3. Regex Replace** | $O(N)$ | $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ | Low | High GC pressure | Poor |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Valid Palindrome
 * ============================================================================
 * Core Pattern      : In-Place Opposing Two Pointers (Zero Heap Allocation)
 * Time Complexity   : O(N) single pass across length N, O(1) best case
 * Space Complexity  : O(1) auxiliary space (zero heap memory allocations)
 * Selection Rule    : Default to Approach 1 to eliminate Garbage Collection pressure.
 * Defensive Traps   : Every inner skip loop MUST verify (left < right) boundary condition
 *                     to avoid IndexOutOfRangeException on strings with trailing punctuation.
 * ============================================================================
 */

public class Solution
{
    /// <summary>
    /// Verifies whether the input string is a valid palindrome, considering only
    /// alphanumeric characters and ignoring case sensitivity.
    /// Operates strictly in O(1) auxiliary space with zero heap allocations.
    /// </summary>
    public bool IsPalindrome(string s)
    {
        // Guard Clause: Null or empty strings are considered valid palindromes by contract
        if (string.IsNullOrEmpty(s))
        {
            return true;
        }

        // Initialize opposing caliper pointers at the string extremities
        int left = 0;
        int right = s.Length - 1;

        // Convergence Loop: Walk pointers inward toward the center
        while (left < right)
        {
            // Skip non-alphanumeric characters from the left boundary
            // Invariant Defense: left < right ensures left pointer never overflows boundary
            while (left < right && !char.IsLetterOrDigit(s[left]))
            {
                left++;
            }

            // Skip non-alphanumeric characters from the right boundary
            // Invariant Defense: left < right ensures right pointer never underflows boundary
            while (left < right && !char.IsLetterOrDigit(s[right]))
            {
                right--;
            }

            // Invariant: s[left] and s[right] are guaranteed to be alphanumeric characters
            // Case-Insensitive Comparison: Normalize using Invariant culture
            if (char.ToLowerInvariant(s[left]) != char.ToLowerInvariant(s[right]))
            {
                // Terminal Short-Circuit: Asymmetric character detected; cannot be a palindrome
                return false;
            }

            // Advance both cursors inward past the validated symmetric pair
            left++;
            right--;
        }

        // Invariant: Pointers crossed without any character mismatch detected
        return true;
    }
}

/// <summary>
/// Approach 2: Filtered StringBuilder Approach.
/// Demonstrates the O(N) auxiliary space alternative using managed string buffers.
/// </summary>
public class SolutionStringBuilder
{
    public bool IsPalindrome(string s)
    {
        if (string.IsNullOrEmpty(s)) return true;

        // Allocate buffer to accumulate filtered alphanumeric characters
        var sb = new System.Text.StringBuilder(capacity: s.Length);
        foreach (char c in s)
        {
            if (char.IsLetterOrDigit(c))
            {
                sb.Append(char.ToLowerInvariant(c));
            }
        }

        // Compare filtered string inward
        int l = 0;
        int r = sb.Length - 1;
        while (l < r)
        {
            if (sb[l++] != sb[r--])
            {
                return false;
            }
        }

        return true;
    }
}
```

---


## 9. Two Sum II — Input Array Is Sorted (LeetCode #167)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#two-pointers` `#opposing-pointers` `#sorted-array` `#hyperplane-elimination` `#binary-search` |
| **LeetCode Link** | [Two Sum II](https://leetcode.com/problems/two-sum-ii-input-array-is-sorted/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given a 1-indexed array of integers `numbers` that is already sorted in non-decreasing order, find two numbers such that they add up to a specific `target` number. Return the indices of the two numbers incremented by one as `[index1, index2]`.
- **Strict Requirement:** Your solution must use only $O(1)$ extra space.
- **Assumptions & Contracts:**
  - Exactly one valid solution exists.
  - You may not use the same element twice ($index1 < index2$).
- **Key Constraints:**
  - `2 <= numbers.Length <= 3 * 10^4`
  - `-1000 <= numbers[i] <= 1000`
  - `numbers` is sorted in non-decreasing order.
  - `-1000 <= target <= 1000`
- **Senior Edge Cases to Defend:**
  - Extreme values / Negatives: E.g., `numbers = [-1000, -500, 0, 1000]`, `target = 0`.
  - Duplicate elements forming target: E.g., `numbers = [0, 0, 3, 4]`, `target = 0`.
  - 1-based indexing in return value.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Exploit array sorted monotonicity to eliminate an entire row or column of impossible pairs at every step in $O(1)$ extra space.
- **Sample 1:**
  - **Input:** `numbers = [2, 7, 11, 15]`, `target = 9`
  - **Output:** `[1, 2]`
  - **Explanation:** `numbers[0] + numbers[1] = 2 + 7 = 9`. 1-based indices: `[1, 2]`.
- **Sample 2:**
  - **Input:** `numbers = [2, 3, 4]`, `target = 6`
  - **Output:** `[1, 3]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a two-dimensional multiplication or addition grid where rows represent `numbers[i]` and columns represent `numbers[j]`. Because `numbers` is sorted in non-decreasing order, this grid is a *Saddle Matrix*:
- Values increase as you move right across any row.
- Values increase as you move down across any column.

If you place your pointer at the top-right corner of this matrix (`i = 0, j = N - 1`), you stand at a strategic crossroads:
- If your current sum is **greater** than target, moving left is your *only* option to decrease the sum. Everything below you in column $j$ is even larger! You can permanently throw away the entire column $j$.
- If your current sum is **less** than target, moving down is your *only* option to increase the sum. Everything to the left of you in row $i$ is even smaller! You can permanently throw away the entire row $i$.

In physical terms, it is an **Opposing Vice Grip**:
- `sum < target` $\implies$ Turn the left crank tighter to increase the number (`left++`).
- `sum > target` $\implies$ Loosen the right crank to decrease the number (`right--`).

#### 3.2 The Naive Bottleneck & Redundant Computation
- **Brute Force ($O(N^2)$):** Tests all $\frac{N(N - 1)}{2}$ pairs, completely ignoring the fact that the array is already sorted.
- **Binary Search ($O(N \log N)$):** For each element $i$, binary searches for $(target - numbers[i])$ in the subarray $[i + 1 \dots N - 1]$. While $O(1)$ space, it performs $N$ separate logarithmic searches ($N \log N \approx 30,000 \times 15 \approx 4.5 \times 10^5$ operations), whereas two pointers solve it in at most $N$ total steps.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Hyperplane Elimination Invariant:**
  - Suppose `sum = numbers[left] + numbers[right] > target`.
  - Because `numbers` is sorted, for any index $k \in [left, right]$:
    $$numbers[k] \ge numbers[left]$$
    $$numbers[k] + numbers[right] \ge numbers[left] + numbers[right] > target$$
  - **Deduction:** The element `numbers[right]` cannot pair with `numbers[left]`, nor can it pair with *any* other remaining element in the range $[left, right]$! Discarding `right` ($right \leftarrow right - 1$) permanently eliminates $right - left$ impossible pairs in a single $O(1)$ operation without any risk of missing the target.
  - Symmetrically, if `sum < target`, `numbers[left]` cannot pair with any element in $[left, right]$. Advancing $left \leftarrow left + 1$ eliminates $right - left$ impossible pairs.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
- `left`: Points to the smallest candidate in the active search space. Initialized to $0$.
- `right`: Points to the largest candidate in the active search space. Initialized to $N - 1$.

```text
[ 0 ... left - 1 ]   [ left ..................... right ]   [ right + 1 ... N - 1 ]
└───────┬────────┘   └─────────────────┬────────────────┘   └──────────┬──────────┘
  Values too small             Active Candidate Span              Values too large
(Permanently eliminated)       (Solution guaranteed inside)     (Permanently eliminated)
```

- **Invariant:**
  1. The unique solution pair $(p, q)$ satisfies $left \le p < q \le right$.
  2. No element outside $[left, right]$ can participate in a valid pair.

#### 3.5 State Transition Triggers & Decision Gates
1. **Compute Sum:** `sum = numbers[left] + numbers[right]`.
2. **Decision Gate 1 (Terminal Hit):**
   - If `sum == target`: Return `new int[] { left + 1, right + 1 }`.
3. **Decision Gate 2 (Undershoot):**
   - If `sum < target`: Discard row `left`: `left++`.
4. **Decision Gate 3 (Overshoot):**
   - If `sum > target`: Discard column `right`: `right--`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `numbers = [2, 7, 11, 15]`, `target = 9`.

| Step | `left` (Val) | `right` (Val) | `sum` | Comparison with Target (9) | Decision & Action | Discarded Search Space |
| :---: | :---: | :---: | :---: | :---: | :--- | :--- |
| 1 | 0 (`2`) | 3 (`15`) | 17 | $17 > 9$ (Overshoot) | `right--` | Discard `numbers[3] = 15` |
| 2 | 0 (`2`) | 2 (`11`) | 13 | $13 > 9$ (Overshoot) | `right--` | Discard `numbers[2] = 11` |
| 3 | 0 (`2`) | 1 (`7`) | 9 | $9 == 9$ (**Match!**) | **Return `[0 + 1, 1 + 1] = [1, 2]`** | - |

Terminates in 3 iterations.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Opposing Two Pointers (Optimal)**
  - *When to Use:* Always for sorted arrays when $O(1)$ memory is required. Strictly linear $O(N)$ time, zero heap memory allocations.
- **Approach 2: Binary Search per Element**
  - *When to Use:* When the array is massive and stored across distributed pages on disk (external memory), where jumping via binary search reduces disk page reads if the target complement is very close or very far.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Step 1: Setup:** Validate input length $\ge 2$. Initialize `left = 0`, `right = numbers.Length - 1`.
2. **Step 2: Exploration Loop:** Run `while (left < right)`.
3. **Step 3: Sum & Gate Check:** Calculate sum. If equal, format 1-based return. If less, `left++`. If greater, `right--`.
4. **Step 4: Exception Handling:** Throw `InvalidOperationException` if array is exhausted without a solution.

#### 4.3 Alternative Approaches Analysis
- **Hash Map ($O(N)$ Space):** While LeetCode #1's hash map solution works, it uses $O(N)$ auxiliary memory and completely ignores the sorted nature of the array, violating the problem's strict $O(1)$ extra space constraint.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Opposing Two Pointers** | $O(1)$ | $O(N)$ | $O(N)$ | $O(1)$ | $O(1)$ | High (sequential convergence) | Non-mutating | Moderate |
| **2. Binary Search** | $O(\log N)$ | $O(N \log N)$ | $O(N \log N)$ | $O(1)$ | $O(1)$ | Moderate (pointer jumps) | Non-mutating | Poor |
| **3. Hash Map** | $O(1)$ | $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ | Moderate | Non-mutating | Excellent |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Two Sum II — Input Array Is Sorted
 * ============================================================================
 * Core Pattern      : Opposing Two Pointers (Monotonic Saddle Elimination)
 * Time Complexity   : O(N) strictly linear single pass
 * Space Complexity  : O(1) auxiliary space
 * Selection Rule    : Mandatory over Hash Map when input is pre-sorted to satisfy O(1) space constraint.
 * Defensive Traps   : Return 1-based indices (index + 1).
 *                     Defend against 32-bit signed integer overflow when summing extremes.
 * ============================================================================
 */

public class Solution
{
    /// <summary>
    /// Finds two numbers in a sorted array that sum to target, returning 1-indexed results.
    /// Operates in O(N) time and strictly O(1) auxiliary space.
    /// </summary>
    public int[] TwoSum(int[] numbers, int target)
    {
        // Guard Clause: Contract requires at least two elements
        if (numbers == null || numbers.Length < 2)
        {
            throw new ArgumentException("Array must contain at least two elements.", nameof(numbers));
        }

        // Initialize opposing boundaries
        int left = 0;
        int right = numbers.Length - 1;

        while (left < right)
        {
            // Defensive Integer Promotion: Cast to long prevents overflow if numbers contain Int32 extremes
            long sum = (long)numbers[left] + numbers[right];

            if (sum == target)
            {
                // Terminal Exit: Problem contract strictly specifies 1-indexed output
                return new int[] { left + 1, right + 1 };
            }
            else if (sum < target)
            {
                // Invariant: sum is strictly less than target.
                // Because the array is sorted, numbers[left] + numbers[k] <= sum < target for all k <= right.
                // Thus, numbers[left] cannot pair with ANY element in the current active span.
                // Discard row 'left' safely:
                left++;
            }
            else
            {
                // Invariant: sum is strictly greater than target.
                // For all k >= left, numbers[k] + numbers[right] >= sum > target.
                // Thus, numbers[right] cannot pair with ANY element in the current active span.
                // Discard column 'right' safely:
                right--;
            }
        }

        // Defensive Exception: Guaranteed exactly one solution exists per contract
        throw new InvalidOperationException("No valid two-sum pair found matching target.");
    }
}

/// <summary>
/// Approach 2: Binary Search per Element.
/// Used when memory is constrained and input structure favors binary tree search.
/// </summary>
public class SolutionBinarySearch
{
    public int[] TwoSum(int[] numbers, int target)
    {
        if (numbers == null || numbers.Length < 2)
        {
            throw new ArgumentException("Array must contain at least two elements.", nameof(numbers));
        }

        for (int i = 0; i < numbers.Length; i++)
        {
            int complement = target - numbers[i];
            
            // Search space strictly to the right of index i to prevent self-match
            int low = i + 1;
            int high = numbers.Length - 1;

            while (low <= high)
            {
                // Defensive Midpoint: Prevents integer overflow bugs in binary search
                int mid = low + (high - low) / 2;

                if (numbers[mid] == complement)
                {
                    return new int[] { i + 1, mid + 1 };
                }
                else if (numbers[mid] < complement)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }
        }

        throw new InvalidOperationException("No valid two-sum pair found matching target.");
    }
}
```

---


## 10. Container With Most Water (LeetCode #11)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#two-pointers` `#greedy-elimination` `#boundary-shrink` `#area-maximization` |
| **LeetCode Link** | [Container With Most Water](https://leetcode.com/problems/container-with-most-water/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You are given an integer array `height` of length $n$. There are $n$ vertical lines drawn such that the two endpoints of the $i$-th line are $(i, 0)$ and $(i, height[i])$. Find two lines that together with the x-axis form a container, such that the container contains the most water. Return the maximum amount of water a container can store.
- **Assumptions & Contracts:**
  - You may not slant the container (water level is horizontal).
  - Width is determined by the horizontal distance between vertical lines: $width = right - left$.
  - Height is constrained by the shorter of the two boundary lines: $h = \min(height[left], height[right])$.
- **Key Constraints:**
  - `n == height.Length`
  - `2 <= n <= 10^5`
  - `0 <= height[i] <= 10^4`
- **Senior Edge Cases to Defend:**
  - Uniform heights: `height = [5, 5, 5, 5]` $\implies$ maximum width pair ($0$ and $N - 1$) is optimal.
  - Symmetrical pyramid: `height = [1, 2, 4, 3]` vs `height = [1, 8, 6, 2, 5, 4, 8, 3, 7]`.
  - Zero-height lines: `height = [0, 2]`. Area with 0 height is 0.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Maximize $\text{Area}(L, R) = \min(height[L], height[R]) \times (R - L)$ across all $0 \le L < R < N$ in $O(N)$ time by greedily eliminating the limiting boundary.
- **Sample 1:**
  - **Input:** `height = [1, 8, 6, 2, 5, 4, 8, 3, 7]`
  - **Output:** `49`
  - **Explanation:** Optimal lines are at index 1 ($height = 8$) and index 8 ($height = 7$). Distance = $8 - 1 = 7$. Water volume = $\min(8, 7) \times 7 = 7 \times 7 = 49$.
- **Sample 2:**
  - **Input:** `height = [1, 1]`
  - **Output:** `1`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine constructing a swimming pool between two retaining walls. The water capacity is governed by two factors: the width between the walls and the height of the **shorter** wall. Water immediately spills over the shorter wall if you try to fill it higher.
We start by placing our walls at the absolute widest possible distance: the two extreme ends of the property ($L = 0$ and $R = N - 1$).
As we move inward, the pool width *strictly shrinks* ($R - L$ decreases by 1 on every step).
Now consider: If wall $L$ has height 3 and wall $R$ has height 10, what happens if we move the taller wall $R$ inward?
- The width shrinks.
- The water height is STILL trapped by wall $L$ (height 3)!
- Therefore, moving the taller wall can *only* decrease or match the area; it can NEVER increase it. It is a guaranteed losing move.
The **only** move with any positive expected value is to tear down the shorter wall ($L$) and hunt inward for a taller pillar that might compensate for the reduced width!

#### 3.2 The Naive Bottleneck & Redundant Computation
- **Quadratic All-Pairs Comparison ($O(N^2)$):**
  ```csharp
  int maxArea = 0;
  for (int i = 0; i < n; i++)
      for (int j = i + 1; j < n; j++)
          maxArea = Math.Max(maxArea, Math.Min(height[i], height[j]) * (j - i));
  ```
- **Redundant Scans:** For $N = 10^5$, $\frac{N(N - 1)}{2} \approx 5 \times 10^9$ evaluations, which causes TLE. The naive approach fails to recognize that when `height[L] < height[R]`, evaluating $L$ with any intermediate wall $R' < R$ is mathematically guaranteed to produce a smaller area than $(L, R)$. The CPU wastes billions of cycles computing doomed candidate areas.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Formal Elimination Proof:**
  - Let current boundaries be $L$ and $R$ with $height[L] < height[R]$.
  - The current area is $\text{Area}(L, R) = height[L] \times (R - L)$.
  - Now consider any candidate boundary $R'$ strictly between $L$ and $R$ ($L < R' < R$).
  - The area formed by $(L, R')$ is:
    $$\text{Area}(L, R') = \min(height[L], height[R']) \times (R' - L) \le height[L] \times (R' - L)$$
  - Since $R' < R$, it follows that $(R' - L) < (R - L)$.
  - Thus:
    $$\text{Area}(L, R') < height[L] \times (R - L) = \text{Area}(L, R)$$
  - **Conclusion:** Boundary $L$ can **never** form a container with *any* other remaining right boundary $R'$ that exceeds the current area $\text{Area}(L, R)$.
  - Discarding $L$ ($L \leftarrow L + 1$) eliminates $R - L - 1$ sub-optimal candidate pairs in a single $O(1)$ decision step!

#### 3.4 Cursor Semantics & Invariant Partition Architecture
- `left`: Pointer initialized to 0.
- `right`: Pointer initialized to $N - 1$.
- `maxArea`: Scalar tracking maximum water volume discovered across all tested configurations.

```text
[ 0 ... left - 1 ]   [ left ..................... right ]   [ right + 1 ... N - 1 ]
└───────┬────────┘   └─────────────────┬────────────────┘   └──────────┬──────────┘
  Discarded shorter           Active Frontier Boundary              Discarded shorter
  pillars (Provably               (Global maximum is                 pillars (Provably
     suboptimal)                  guaranteed inside)                    suboptimal)
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Compute Current Area:**
   `width = right - left;`
   `currentArea = Math.Min(height[left], height[right]) * width;`
2. **State Mutation:** `maxArea = Math.Max(maxArea, currentArea);`
3. **Decision Gate 1 (Left Shorter):**
   - If `height[left] < height[right]`: `left++`.
4. **Decision Gate 2 (Right Shorter or Equal):**
   - If `height[right] <= height[left]`: `right--`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `height = [1, 8, 6, 2, 5, 4, 8, 3, 7]` ($N = 9$).

| Step | `left` ($h_L$) | `right` ($h_R$) | Width ($R - L$) | Limiting Height | Current Area | `maxArea` | Action & Invariant Gate |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :--- |
| 1 | 0 (`1`) | 8 (`7`) | 8 | $\min(1, 7) = 1$ | $1 \times 8 = 8$ | 8 | $h_L < h_R \implies$ Discard $L$ (`left++`) |
| 2 | 1 (`8`) | 8 (`7`) | 7 | $\min(8, 7) = 7$ | $7 \times 7 = 49$ | **49** | $h_R < h_L \implies$ Discard $R$ (`right--`) |
| 3 | 1 (`8`) | 7 (`3`) | 6 | $\min(8, 3) = 3$ | $3 \times 6 = 18$ | 49 | $h_R < h_L \implies$ Discard $R$ (`right--`) |
| 4 | 1 (`8`) | 6 (`8`) | 5 | $\min(8, 8) = 8$ | $8 \times 5 = 40$ | 49 | Equal heights $\implies$ `right--` |
| 5 | 1 (`8`) | 5 (`4`) | 4 | $\min(8, 4) = 4$ | $4 \times 4 = 16$ | 49 | $h_R < h_L \implies$ Discard $R$ (`right--`) |
| 6 | 1 (`8`) | 4 (`5`) | 3 | $\min(8, 5) = 5$ | $5 \times 3 = 15$ | 49 | $h_R < h_L \implies$ Discard $R$ (`right--`) |
| 7 | 1 (`8`) | 3 (`2`) | 2 | $\min(8, 2) = 2$ | $2 \times 2 = 4$ | 49 | $h_R < h_L \implies$ Discard $R$ (`right--`) |
| 8 | 1 (`8`) | 2 (`6`) | 1 | $\min(8, 6) = 6$ | $6 \times 1 = 6$ | 49 | $h_R < h_L \implies$ Discard $R$ (`right--`) |
| 9 | 1 | 1 | 0 | - | - | 49 | Pointers meet; terminate. |

Final Maximum Area: `49`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Two Pointers Greedy Elimination (Optimal)**
  - *When to Use:* Always. Strictly $O(N)$ single pass, strictly $O(1)$ auxiliary space.
- **Approach 2: Two Pointers with Fast-Forward Skipping**
  - *When to Use:* Highly competitive programming optimization. When moving a pointer inward, if the next pillar is even shorter than the pillar just discarded, its area is guaranteed to be smaller; we can fast-forward past all shorter intermediate pillars with a `while` loop.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Step 1: Setup:** Check `height.Length < 2`. Initialize `left = 0`, `right = n - 1`, `maxArea = 0`.
2. **Step 2: Main Convergence Loop:** While `left < right`.
3. **Step 3: Area Calculation & State Update:** Compute area and update `maxArea`.
4. **Step 4: Boundary Shift:** Increment `left` if `height[left] < height[right]`, else decrement `right`.
5. **Step 5: Return:** Return `maxArea`.

#### 4.3 Alternative Approaches Analysis
- **Monotonic Stack:** Monotonic stacks are appropriate for *Trapping Rain Water* (LeetCode #42) or *Largest Rectangle in Histogram* (LeetCode #84), where interior contours hold water or form bars. For Container With Most Water, water is bounded only by the two endpoint lines, making Two Pointers strictly superior.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Two Pointers Greedy** | $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ | $O(1)$ | High | Non-mutating | Poor (needs two ends) |
| **2. Fast-Forward Skipping**| $O(N)$ | $O(N)$ (fewer ops)| $O(N)$ | $O(1)$ | $O(1)$ | High | Non-mutating | Poor |
| **3. Brute Force** | $O(N^2)$ | $O(N^2)$ | $O(N^2)$ | $O(1)$ | $O(1)$ | High | Non-mutating | Poor |

---

### 5. Production C# Implementation

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Container With Most Water
 * ============================================================================
 * Core Pattern      : Two Pointers Greedy Boundary Elimination
 * Time Complexity   : O(N) strictly linear single pass
 * Space Complexity  : O(1) auxiliary space
 * Selection Rule    : Default to Two Pointers. Discard the shorter boundary at each step.
 * Defensive Traps   : Do not move the taller boundary—moving the taller boundary reduces width
 *                     while water height remains bottlenecked by the shorter boundary.
 * ============================================================================
 */

public class Solution
{
    /// <summary>
    /// Computes the maximum water volume trapped between two vertical lines.
    /// Operates in O(N) time and O(1) auxiliary space using greedy boundary elimination.
    /// </summary>
    public int MaxArea(int[] height)
    {
        // Guard Clause: Need at least 2 vertical lines to form a container
        if (height == null || height.Length < 2)
        {
            return 0;
        }

        int left = 0;
        int right = height.Length - 1;
        int maxArea = 0;

        while (left < right)
        {
            int hLeft = height[left];
            int hRight = height[right];
            int width = right - left;

            // Compute current container capacity
            int currentArea = Math.Min(hLeft, hRight) * width;
            if (currentArea > maxArea)
            {
                maxArea = currentArea;
            }

            // Invariant & Decision Gate:
            // The water ceiling is strictly bottlenecked by the shorter pillar.
            // Any container using this shorter pillar with an inward boundary will have
            // a strictly smaller width and a height <= shorter pillar.
            // Therefore, the shorter pillar can NEVER achieve a larger area. Discard it!
            if (hLeft < hRight)
            {
                // Discard left pillar and advance inward
                left++;
            }
            else
            {
                // Discard right pillar and advance inward (handles equal heights symmetrically)
                right--;
            }
        }

        return maxArea;
    }
}

/// <summary>
/// Approach 2: Two Pointers with Fast-Forward Skipping.
/// Skips intermediate pillars that are shorter than the boundary just eliminated.
/// </summary>
public class SolutionFastForward
{
    public int MaxArea(int[] height)
    {
        if (height == null || height.Length < 2) return 0;

        int left = 0;
        int right = height.Length - 1;
        int maxArea = 0;

        while (left < right)
        {
            int hLeft = height[left];
            int hRight = height[right];
            int width = right - left;

            int currentArea = Math.Min(hLeft, hRight) * width;
            if (currentArea > maxArea)
            {
                maxArea = currentArea;
            }

            if (hLeft < hRight)
            {
                // Fast-forward past any intermediate pillars that cannot beat hLeft
                while (left < right && height[left] <= hLeft)
                {
                    left++;
                }
            }
            else
            {
                // Fast-forward past any intermediate pillars that cannot beat hRight
                while (left < right && height[right] <= hRight)
                {
                    right--;
                }
            }
        }

        return maxArea;
    }
}
```

---


## 11. 3Sum (LeetCode #15)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#two-pointers` `#sorting` `#k-sum` `#deduplication` `#pruning` |
| **LeetCode Link** | [3Sum](https://leetcode.com/problems/3sum/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an integer array `nums`, return all the unique triplets `[nums[i], nums[j], nums[k]]` such that $i \neq j, i \neq k, j \neq k$, and $nums[i] + nums[j] + nums[k] == 0$.
- **Strict Contract:** The solution set must **not** contain duplicate triplets.
- **Key Constraints:**
  - `3 <= nums.Length <= 3000`
  - `-10^5 <= nums[i] <= 10^5`
- **Senior Edge Cases to Defend:**
  - All zeros: `nums = [0, 0, 0, 0]` $\implies$ `[[0, 0, 0]]`. Must produce exactly one triplet without duplicate sets.
  - Heavy duplicate clusters: E.g., `nums = [-2, 0, 0, 2, 2]` $\implies$ `[[-2, 0, 2]]`.
  - No valid triplets: `nums = [1, 2, 3]` $\implies$ `[]`.
  - Smallest element positive: `nums[0] > 0` $\implies$ early termination.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Fix an anchor element $i$ and execute Two Sum II on the sorted remaining subarray $[i + 1 \dots N - 1]$, suppressing duplicate triplets in-place without heap HashSet allocations.
- **Sample 1:**
  - **Input:** `nums = [-1, 0, 1, 2, -1, -4]`
  - **Output:** `[[-1, -1, 2], [-1, 0, 1]]`
  - **Explanation:** Sorted array is `[-4, -1, -1, 0, 1, 2]`. Distinct zero-sum triplets are identified without repeats.
- **Sample 2:**
  - **Input:** `nums = [0, 1, 1]`
  - **Output:** `[]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine balancing a three-legged stool where the total balance equation is $a + b + c = 0$.
If all three legs are free to move at random, the degrees of freedom make coordinating them chaotic ($O(N^3)$).
To bring order to the system, you bolt one leg into the floor: fix $a = nums[i]$.
Now, the required balance point for the remaining two legs is deterministic:
$$b + c = -nums[i]$$
This collapses the problem into the classic **Two Sum II (Opposing Calipers)** problem!
By sorting the array upfront, we gain two superpowers:
1. We can use opposing two pointers (`left` and `right`) to find pairs in linear time.
2. Identical values cluster together consecutively, allowing us to enforce zero duplicate triplets simply by skipping adjacent duplicate values.

#### 3.2 The Naive Bottleneck & Redundant Computation
- **Brute Force ($O(N^3)$):** Testing all triplets takes $\frac{N(N - 1)(N - 2)}{6} \approx 4.5 \times 10^9$ operations for $N = 3000$, which times out severely.
- **HashSet Deduplication Overhead:** Naive solutions sort each valid triplet and insert into `HashSet<List<int>>` or `HashSet<(int, int, int)>`.
  - *Why this is rejected by senior interviewers:* It incurs severe memory allocation, object boxing, and hash collision overhead. Handling deduplication *algorithmically* via pointer mechanics is the hallmark of senior-level engineering.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Sorting as the Foundation:** Sorting in $O(N \log N)$ provides the monotonic structure necessary for $O(N)$ two-pointer scanning per anchor.
- **Three Core Pruning & Deduplication Invariants:**
  1. *Early Positive Pruning:* If `nums[i] > 0`, since the array is sorted, $nums[i] \le nums[left] \le nums[right]$. Hence $nums[i] + nums[left] + nums[right] \ge 3 \times nums[i] > 0$. No zero-sum triplet can ever be formed. We can `break` immediately!
  2. *Outer Anchor Deduplication:* If $i > 0$ and $nums[i] == nums[i - 1]$, skip $i$ (`continue`). All possible unique triplets starting with this value were already exhausted when $nums[i - 1]$ was the anchor.
  3. *Inner Caliper Deduplication:* When `sum == 0`, append the triplet, advance both `left++` and `right--`, and fast-forward past duplicate adjacent values:
     `while (left < right && nums[left] == nums[left - 1]) left++;`
     `while (left < right && nums[right] == nums[right + 1]) right--;`

#### 3.4 Cursor Semantics & Invariant Partition Architecture
- `i`: Outer anchor cursor iterating $0 \dots N - 3$.
- `left`: Inner lower-bound cursor starting at $i + 1$.
- `right`: Inner upper-bound cursor starting at $N - 1$.

```text
[ 0 ... i - 1 ]   [ i ]   [ i + 1 ... left - 1 ]   [ left ... right ]   [ right + 1 ... N - 1 ]
└───────┬───────┘   └─┬─┘   └─────────┬──────────┘   └───────┬────────┘   └──────────┬──────────┘
    Exhausted       Fixed        Values too small         Active Pair          Values too large
     anchors       anchor          for anchor           Search Frontier          for anchor
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Outer Gate 1 (Early Break):** If `nums[i] > 0`, `break`.
2. **Outer Gate 2 (Skip Duplicate Anchor):** If `i > 0 && nums[i] == nums[i - 1]`, `continue`.
3. **Inner While Loop (`left < right`):**
   `sum = nums[i] + nums[left] + nums[right]`
   - **Gate A (`sum == 0`):** Add `[nums[i], nums[left], nums[right]]` to result.
     `left++; right--;`
     Skip duplicate left: `while (left < right && nums[left] == nums[left - 1]) left++;`
     Skip duplicate right: `while (left < right && nums[right] == nums[right + 1]) right--;`
   - **Gate B (`sum < 0`):** Sum too small $\implies left++$.
   - **Gate C (`sum > 0`):** Sum too large $\implies right--$.

#### 3.6 Concrete Step-by-Step State Trace
Input: `nums = [-1, 0, 1, 2, -1, -4]`
Sorted: `nums = [-4, -1, -1, 0, 1, 2]` ($N = 6$).

- **Anchor $i = 0$ (`nums[0] = -4`):** Target pair sum = $4$.
  - `left = 1 (-1), right = 5 (2)`: sum = $-3 < 0 \implies left++$
  - `left = 2 (-1), right = 5 (2)`: sum = $-3 < 0 \implies left++$
  - `left = 3 (0), right = 5 (2)`: sum = $-2 < 0 \implies left++$
  - `left = 4 (1), right = 5 (2)`: sum = $-1 < 0 \implies left++$
  - `left = 5, right = 5`: loop ends. No triplets.
- **Anchor $i = 1$ (`nums[1] = -1`):** Target pair sum = $1$.
  - `left = 2 (-1), right = 5 (2)`: sum = $-1 + (-1) + 2 = 0$. **Found `[-1, -1, 2]`!**
    `left++ (3), right-- (4)`. No duplicate skips needed.
  - `left = 3 (0), right = 4 (1)`: sum = $-1 + 0 + 1 = 0$. **Found `[-1, 0, 1]`!**
    `left++ (4), right-- (3)`. Loop ends.
- **Anchor $i = 2$ (`nums[2] = -1`):**
  - `nums[2] == nums[1]` $\implies$ **Duplicate anchor! Skipped instantly.**
- **Anchor $i = 3$ (`nums[3] = 0`):**
  - `left = 4 (1), right = 5 (2)`: sum = $0 + 1 + 2 = 3 > 0 \implies right--$. Loop ends.

Output: `[[-1, -1, 2], [-1, 0, 1]]`. Zero duplicate entries, zero HashSet memory.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Sort + Two Pointers (Optimal)**
  - *When to Use:* Always. $O(N^2)$ time, $O(1)$ auxiliary space (sorting takes $O(\log N)$ stack space). Handles deduplication natively in-place.
- **Approach 2: Sort + HashSet Lookup for Complement**
  - *When to Use:* Useful when building a generalized $K$-sum solver with memoized lookup maps, but strictly inferior for 3Sum due to hash allocation and boxing costs.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Step 1: Guard & Sort:** Check `nums.Length < 3`. Sort `nums` in ascending order.
2. **Step 2: Outer Anchor Loop:** Iterate $i = 0 \dots N - 3$. Apply positive break and duplicate skipping.
3. **Step 3: Inner Opposing Pointers:** Set `left = i + 1`, `right = N - 1`.
4. **Step 4: Convergence & In-Place Deduplication:** Probe sum, adjust pointers, skip consecutive identical elements.

#### 4.3 Alternative Approaches Analysis
- **No-Sort HashSet Approach:** Useful only if input cannot be mutated and copying the array is forbidden. Uses outer loop $i$, inner loop $j$, and a `HashSet<int>` to find complement $-(nums[i] + nums[j])$. However, preventing duplicate triplets without sorting requires hashing sorted 3-tuples, which causes high memory and GC pressure.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Sort + Two Pointers** | $O(N \log N)$ | $O(N^2)$ | $O(N^2)$ | $O(1)$ (or $O(\log N)$ stack)| $O(K)$ triplets | High | Mutates input array | Poor |
| **2. Sort + HashSet** | $O(N \log N)$ | $O(N^2)$ | $O(N^2)$ | $O(N)$ hash table | $O(K)$ triplets | Moderate | Mutates input array | Poor |
| **3. Brute Force** | $O(N^3)$ | $O(N^3)$ | $O(N^3)$ | $O(K)$ hash set | $O(K)$ triplets | High | Non-mutating | Poor |

---

### 5. Production C# Implementation

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: 3Sum
 * ============================================================================
 * Core Pattern      : Fixed Anchor + Opposing Two Pointers (In-Place Deduplication)
 * Time Complexity   : O(N^2) dominated by N inner two-pointer scans
 * Space Complexity  : O(1) auxiliary space beyond introsort stack frames
 * Selection Rule    : Default to Sort + Two Pointers for optimal time and zero memory overhead.
 * Defensive Traps   : Skip duplicates at BOTH outer anchor (i > 0 && nums[i] == nums[i-1])
 *                     AND inner boundaries after a match to prevent identical triplets.
 *                     Break outer loop early if nums[i] > 0 (positive sum impossible).
 * ============================================================================
 */

public class Solution
{
    /// <summary>
    /// Finds all unique triplets in the array that sum to zero.
    /// Operates in O(N^2) time and O(1) auxiliary space without HashSet allocations.
    /// </summary>
    public IList<IList<int>> ThreeSum(int[] nums)
    {
        var result = new List<IList<int>>();

        // Guard Clause: Need at least 3 elements to form a triplet
        if (nums == null || nums.Length < 3)
        {
            return result;
        }

        // Sort in O(N log N) to enable monotonic two-pointer convergence & deduplication
        Array.Sort(nums);
        int n = nums.Length;

        // Outer Anchor Loop: Fix the first element of the triplet
        for (int i = 0; i < n - 2; i++)
        {
            // Pruning Invariant 1:
            // Since the array is sorted, if the smallest element of the triplet is positive,
            // the sum of any three numbers from this point forward must be strictly > 0.
            if (nums[i] > 0)
            {
                break;
            }

            // Deduplication Invariant 1:
            // Skip duplicate outer anchors to avoid evaluating identical triplet sets
            if (i > 0 && nums[i] == nums[i - 1])
            {
                continue;
            }

            // Inner Two-Pointer Calipers
            int left = i + 1;
            int right = n - 1;

            while (left < right)
            {
                int sum = nums[i] + nums[left] + nums[right];

                if (sum == 0)
                {
                    // Valid triplet identified
                    result.Add(new List<int> { nums[i], nums[left], nums[right] });

                    // Advance pointers past current matched values
                    left++;
                    right--;

                    // Deduplication Invariant 2: Skip identical adjacent values from left
                    while (left < right && nums[left] == nums[left - 1])
                    {
                        left++;
                    }

                    // Deduplication Invariant 3: Skip identical adjacent values from right
                    while (left < right && nums[right] == nums[right + 1])
                    {
                        right--;
                    }
                }
                else if (sum < 0)
                {
                    // Sum too small; advance left pointer to larger value
                    left++;
                }
                else
                {
                    // Sum too large; advance right pointer to smaller value
                    right--;
                }
            }
        }

        return result;
    }
}
```

---


## 12. 4Sum (LeetCode #18)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#k-sum` `#two-pointers` `#recursion-pruning` `#deduplication` `#overflow-defense` |
| **LeetCode Link** | [4Sum](https://leetcode.com/problems/4sum/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array `nums` of $n$ integers, return an array of all unique quadruplets `[nums[a], nums[b], nums[c], nums[d]]` such that:
  - $0 \le a, b, c, d < n$
  - $a, b, c,$ and $d$ are distinct.
  - $nums[a] + nums[b] + nums[c] + nums[d] == target$
- **Strict Contract:** No duplicate quadruplets in output.
- **Key Constraints:**
  - `1 <= nums.Length <= 200`
  - `-10^9 <= nums[i] <= 10^9`
  - `-10^9 <= target <= 10^9`
- **Senior Edge Cases to Defend:**
  - 32-bit Integer Overflow: Summing four integers up to $10^9$ can reach $4 \times 10^9$, exceeding standard 32-bit signed integer capacity (`int.MaxValue` $\approx 2.14 \times 10^9$). Must use 64-bit integer arithmetic (`long`).
  - Target requiring 4 identical elements: E.g., `nums = [2, 2, 2, 2, 2]`, `target = 8` $\implies$ exactly one quadruplet `[[2, 2, 2, 2]]`.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Generalize Two Sum II and 3Sum to $K = 4$ dimensions using nested anchors, two-level boundary pruning, 64-bit overflow prevention, and in-place deduplication.
- **Sample 1:**
  - **Input:** `nums = [1, 0, -1, 0, -2, 2]`, `target = 0`
  - **Output:** `[[-2, -1, 1, 2], [-2, 0, 0, 2], [-1, 0, 0, 1]]`
- **Sample 2:**
  - **Input:** `nums = [2, 2, 2, 2, 2]`, `target = 8`
  - **Output:** `[[2, 2, 2, 2]]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a set of nested Russian Matryoshka dolls. Solving 4Sum directly is intimidating. So you open the outer doll: fix the first anchor element $i$.
Now inside is a 3Sum problem! You open the second doll: fix the second anchor element $j$.
Now inside is a 2Sum problem! You solve the 2Sum core using our trusted opposing calipers (`left`, `right`).
However, walking all nested loops blindly is wasteful. Imagine installing an **early-warning radar** at each doll level:
- You calculate the *minimum theoretical sum* possible from this point onward (the current anchor plus the smallest remaining elements). If even the minimum possible sum already overshoots your target, you smash the current search branch immediately (`break`)!
- You calculate the *maximum theoretical sum* possible from this point onward (the current anchor plus the largest remaining elements). If even the maximum possible sum cannot reach your target, the current anchor is hopelessly weak; you advance to the next candidate immediately (`continue`)!

#### 3.2 The Naive Bottleneck & Redundant Computation
- **Quadratic / Quartic Loops ($O(N^4)$):** A 4-level nested loop without pruning executes $\approx \frac{N^4}{24}$ iterations. For $N = 200$, this is $\approx 6.6 \times 10^7$ loops.
- Redundancy occurs when the algorithm explores iterations where numbers are far too large or far too small to ever reach `target`.
- **The Integer Overflow Trap:**
  ```csharp
  int sum = nums[i] + nums[j] + nums[left] + nums[right]; // BUG: Overflows to negative!
  ```
  In C#, 32-bit signed overflow wraps around into negative territory without throwing an exception by default. A quadruplet that sums to $+3 \times 10^9$ wraps to negative, corrupting the comparison with `target`.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **64-bit Arithmetic Guarantee:** All sum computations are cast to `long` before addition:
  $$\text{sum} = (long)nums[i] + nums[j] + nums[left] + nums[right]$$
- **Multi-Level Boundary Pruning Invariants:**
  1. *Level 1 Minimum Cutoff:* At anchor $i$, the smallest sum possible is $(long)nums[i] + nums[i + 1] + nums[i + 2] + nums[i + 3]$. If this $> target$, then because the array is sorted, no subsequent combination can be smaller. `break` immediately!
  2. *Level 1 Maximum Cutoff:* At anchor $i$, the largest sum possible is $(long)nums[i] + nums[N - 1] + nums[N - 2] + nums[N - 3]$. If this $< target$, $nums[i]$ is too small even with the three largest numbers in the array. `continue` to $i + 1$!
  3. Symmetrical Min/Max cutoffs applied at anchor $j$.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
- Cursor hierarchy:
  - $i$: First anchor ($0 \dots N - 4$)
  - $j$: Second anchor ($i + 1 \dots N - 3$)
  - $left$: Lower caliper pointer ($j + 1$)
  - $right$: Upper caliper pointer ($N - 1$)

```text
[ 0 ... i-1 ]  [ i ]  [ i+1 ... j-1 ]  [ j ]  [ j+1 ... left-1 ]  [ left ... right ]  [ right+1 ... N-1 ]
  Exhausted   Anchor    Exhausted     Anchor      Too small for        Active Pair         Too large for
  Anchor 1      1       Anchor 2        2           Anchors             Caliper               Anchors
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Anchor $i$ Deduplication:** If $i > 0 && nums[i] == nums[i - 1]$, `continue`.
2. **Anchor $i$ Pruning:** Min-sum break, Max-sum continue.
3. **Anchor $j$ Deduplication:** If $j > i + 1 && nums[j] == nums[j - 1]$, `continue`.
4. **Anchor $j$ Pruning:** Min-sum break, Max-sum continue.
5. **Inner Caliper While Loop (`left < right`):**
   - If `sum == target`: Add quadruplet, advance both, skip duplicates.
   - If `sum < target`: `left++`.
   - If `sum > target`: `right--`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `nums = [1, 0, -1, 0, -2, 2]`, `target = 0`
Sorted: `nums = [-2, -1, 0, 0, 1, 2]` ($N = 6$).

- Anchor $i = 0$ (`nums[0] = -2`):
  - Min sum: $-2 + (-1) + 0 + 0 = -3 \le 0$. Max sum: $-2 + 0 + 1 + 2 = 1 \ge 0$. Bounds valid.
  - Anchor $j = 1$ (`nums[1] = -1`):
    - Target for pair: $0 - (-2 + -1) = 3$.
    - `left = 2 (0), right = 5 (2)`: sum = $-2 + -1 + 0 + 2 = -1 < 0 \implies left++$
    - `left = 3 (0), right = 5 (2)`: sum = $-1 < 0 \implies left++$
    - `left = 4 (1), right = 5 (2)`: sum = $-2 + -1 + 1 + 2 = 0$. **Found `[-2, -1, 1, 2]`!**
  - Anchor $j = 2$ (`nums[2] = 0`):
    - Target for pair: $0 - (-2 + 0) = 2$.
    - `left = 3 (0), right = 5 (2)`: sum = $-2 + 0 + 0 + 2 = 0$. **Found `[-2, 0, 0, 2]`!**
- Anchor $i = 1$ (`nums[1] = -1`):
  - Anchor $j = 2$ (`nums[2] = 0`):
    - `left = 3 (0), right = 4 (1)`: sum = $-1 + 0 + 0 + 1 = 0$. **Found `[-1, 0, 0, 1]`!**

Result: 3 unique quadruplets found with extensive pruning.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Iterative Two Pointers with Pruning (Optimal for $K = 4$)**
  - *When to Use:* Standard in production for fixed 4Sum. Strictly $O(N^3)$ worst-case, with practical runtime cut by up to 80% via branch pruning. $O(1)$ auxiliary space.
- **Approach 2: Generalized Recursive $K$-Sum**
  - *When to Use:* When the problem asks for arbitrary $K$-Sum ($K = 3, 4, 5, \dots$). Uses recursion to reduce $K$-Sum to $(K-1)$-Sum until base case $K = 2$.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Step 1: Guard & Sort:** Check length $< 4$. Sort array.
2. **Step 2: Loop $i$ with Pruning:** Check min-sum and max-sum, deduplicate.
3. **Step 3: Loop $j$ with Pruning:** Check min-sum and max-sum, deduplicate.
4. **Step 4: Two Pointers:** Converge `left` and `right`, compute 64-bit sum, record quadruplets, skip adjacent duplicates.

#### 4.3 Alternative Approaches Analysis
- **Hash Map of Pair Sums ($O(N^2)$ Time & $O(N^2)$ Space):** Precompute sums of all pairs in `Dictionary<int, List<(int, int)>>`, then find pairs that add to target. Flaw: Handling duplicate indices and duplicate quadruplets becomes a combinatorial nightmare requiring heavy post-processing deduplication.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Cache Locality | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Iterative Pruned** | $O(N \log N)$ (pruned)| $O(N^3)$ | $O(N^3)$ | $O(1)$ | $O(K)$ | High | Mutates input array | Poor |
| **2. Recursive K-Sum** | $O(N^{K-1})$ | $O(N^{K-1})$ | $O(N^{K-1})$ | $O(K)$ stack frames | $O(K)$ | High | Mutates input array | Poor |
| **3. Pair Hash Map** | $O(N^2)$ | $O(N^2)$ | $O(N^2)$ | $O(N^2)$ pairs | $O(K)$ | Moderate | Non-mutating | Poor |

---

### 5. Production C# Implementation

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: 4Sum
 * ============================================================================
 * Core Pattern      : Nested Anchors + Two Pointers with Multi-Level Branch Pruning
 * Time Complexity   : O(N^3) with significant real-world acceleration from pruning
 * Space Complexity  : O(1) auxiliary space beyond introsort recursion stack
 * Selection Rule    : Default to Approach 1 for K=4; transition to Approach 2 for general K-Sum.
 * Defensive Traps   : ALWAYS cast intermediate sums to (long) to prevent 32-bit signed integer overflow.
 *                     Prune aggressively using theoretical min-sum and max-sum bounds.
 * ============================================================================
 */

public class Solution
{
    /// <summary>
    /// Finds all unique quadruplets summing to target using two-level pruning and two pointers.
    /// Defends strictly against 32-bit arithmetic overflow using 64-bit integer casts.
    /// </summary>
    public IList<IList<int>> FourSum(int[] nums, int target)
    {
        var result = new List<IList<int>>();

        // Guard Clause: Need at least 4 elements to form a quadruplet
        if (nums == null || nums.Length < 4)
        {
            return result;
        }

        // Sort to establish monotonicity for pruning and two-pointer traversal
        Array.Sort(nums);
        int n = nums.Length;

        // Level 1 Anchor: First Element
        for (int i = 0; i < n - 3; i++)
        {
            // Level 1 Deduplication: Skip identical first anchor values
            if (i > 0 && nums[i] == nums[i - 1])
            {
                continue;
            }

            // Pruning Invariant 1 (Level 1 Minimum Bound):
            // The smallest possible sum with anchor nums[i] uses the 3 immediate next smallest elements.
            // If this minimum sum exceeds target, no valid quadruplet can be formed from this point.
            long minSum1 = (long)nums[i] + nums[i + 1] + nums[i + 2] + nums[i + 3];
            if (minSum1 > target)
            {
                break; // Break outer loop completely
            }

            // Pruning Invariant 2 (Level 1 Maximum Bound):
            // The largest possible sum with anchor nums[i] uses the 3 largest elements at the end of the array.
            // If this maximum sum is less than target, nums[i] is too small; advance to next anchor.
            long maxSum1 = (long)nums[i] + nums[n - 1] + nums[n - 2] + nums[n - 3];
            if (maxSum1 < target)
            {
                continue; // Continue to next candidate for i
            }

            // Level 2 Anchor: Second Element
            for (int j = i + 1; j < n - 2; j++)
            {
                // Level 2 Deduplication: Skip identical second anchor values within current branch
                if (j > i + 1 && nums[j] == nums[j - 1])
                {
                    continue;
                }

                // Pruning Invariant 3 (Level 2 Minimum Bound):
                long minSum2 = (long)nums[i] + nums[j] + nums[j + 1] + nums[j + 2];
                if (minSum2 > target)
                {
                    break; // Break inner loop j
                }

                // Pruning Invariant 4 (Level 2 Maximum Bound):
                long maxSum2 = (long)nums[i] + nums[j] + nums[n - 1] + nums[n - 2];
                if (maxSum2 < target)
                {
                    continue; // Continue to next candidate for j
                }

                // Inner Core: Opposing Calipers for remaining two elements
                int left = j + 1;
                int right = n - 1;

                while (left < right)
                {
                    // Defensive Overflow Cast: Summing 4 32-bit values requires 64-bit long
                    long sum = (long)nums[i] + nums[j] + nums[left] + nums[right];

                    if (sum == target)
                    {
                        result.Add(new List<int> { nums[i], nums[j], nums[left], nums[right] });

                        left++;
                        right--;

                        // Deduplication: Skip duplicate left elements
                        while (left < right && nums[left] == nums[left - 1])
                        {
                            left++;
                        }

                        // Deduplication: Skip duplicate right elements
                        while (left < right && nums[right] == nums[right + 1])
                        {
                            right--;
                        }
                    }
                    else if (sum < target)
                    {
                        left++; // Increase sum monotonically
                    }
                    else
                    {
                        right--; // Decrease sum monotonically
                    }
                }
            }
        }

        return result;
    }
}
```

---


## 13. Trapping Rain Water (LeetCode #42)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#two-pointers` `#prefix-suffix-max` `#monotonic-stack` `#boundary-invariant` `#water-trapping` |
| **LeetCode Link** | [Trapping Rain Water](https://leetcode.com/problems/trapping-rain-water/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given $n$ non-negative integers representing an elevation map where the width of each bar is 1, compute how much water it can trap after raining.
- **Assumptions & Contracts:**
  - Water cannot leak from the left boundary ($< 0$) or the right boundary ($> n - 1$).
  - Water is trapped vertically in columns above each pillar.
- **Key Constraints:**
  - `n == height.Length`
  - `1 <= n <= 2 * 10^4`
  - `0 <= height[i] <= 10^5`
- **Senior Edge Cases to Defend:**
  - Monotonically increasing or decreasing elevation: `height = [1, 2, 3, 4]` or `[4, 3, 2, 1]` $\implies$ `0` water.
  - Fewer than 3 bars: Cannot form a basin $\implies$ `0` water.
  - Large flat plateaus: `height = [3, 0, 0, 0, 3]` $\implies$ $3 \times 3 = 9$ units of water.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Water trapped over column $i$ is strictly governed by:
  $$\text{Water}[i] = \max(0, \min(\text{leftMax}_i, \text{rightMax}_i) - height[i])$$
  Achieve $O(N)$ runtime and $O(1)$ auxiliary space using the two-pointer boundary invariant.
- **Sample 1:**
  - **Input:** `height = [0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1]`
  - **Output:** `6`
- **Sample 2:**
  - **Input:** `height = [4, 2, 0, 3, 2, 5]`
  - **Output:** `9`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine a canyon formed by stone pillars after an endless monsoon. Water collects between the pillars.
How high can water pool directly over pillar $i$?
Water will inevitably spill over the lower of the two enclosing rim walls!
$$\text{Water Level} = \min(\text{highest peak to the left of } i, \text{highest peak to the right of } i)$$
If you use two pointers `left` and `right` approaching each other from the far sides of the canyon, and keep track of the tallest peak seen so far from the left (`leftMax`) and from the right (`rightMax`), you notice a profound mathematical truth:
**Whichever wall is shorter is the absolute ceiling for that side!**
Suppose `leftMax <= rightMax`. We don't care if there is an Everest hidden somewhere between `left` and `right`. Any hidden peak will only make the right side even taller—it will **never** lower the left ceiling!
Therefore, the water level at column `left` is *unquestionably* governed by `leftMax`. We can calculate its trapped water immediately and advance `left++`.

#### 3.2 The Naive Bottleneck & Redundant Computation
- **Brute Force ($O(N^2)$):** For every index $i$, scan left ($0 \dots i$) to find $\max$, then scan right ($i \dots N - 1$) to find $\max$. Total scans: $O(N^2)$.
- **Dynamic Programming ($O(N)$ Time & $O(N)$ Space):** Precompute prefix maximum array `leftMax[N]` and suffix maximum array `rightMax[N]`. While this runs in linear time, it allocates two separate integer arrays on the managed heap, creating garbage collection overhead.
- Two pointers eliminates both arrays entirely, maintaining the maximums on CPU registers in $O(1)$ space.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **The Two-Pointer Boundary Invariant:**
  - Maintain running trackers:
    - `leftMax`: $\max(height[0 \dots left])$
    - `rightMax`: $\max(height[right \dots N - 1])$
  - **Case 1: $height[left] \le height[right]$:**
    - Since $leftMax$ is the max up to $left$, we know $leftMax = \max(leftMax, height[left])$.
    - Because $height[left] \le height[right] \le rightMax$, we have $leftMax \le rightMax$.
    - Could the *true* global maximum to the right of $left$ be smaller than $leftMax$? **No**, because $height[right]$ is already $\ge leftMax$, guaranteeing the true right maximum is at least $rightMax \ge leftMax$.
    - Therefore, $\min(\text{true\_leftMax}, \text{true\_rightMax}) = leftMax$ with 100% mathematical certainty!
    - Water trapped at `left` is:
      $$\text{water} = leftMax - height[left]$$
    - Discard column `left` and advance `left++`.
  - **Case 2: $height[right] < height[left]$:**
    - Symmetrically, $\min(\text{true\_leftMax}, \text{true\_rightMax}) = rightMax$.
    - Water trapped at `right` is:
      $$\text{water} = rightMax - height[right]$$
    - Discard column `right` and decrement `right--`.

#### 3.4 Cursor Semantics & Invariant Partition Architecture
- `left`: Sweeps inward from index 0.
- `right`: Sweeps inward from index $N - 1$.
- `leftMax`: Running maximum height seen in prefix $[0 \dots left]$.
- `rightMax`: Running maximum height seen in suffix $[right \dots N - 1]$.

```text
[ 0 ... left - 1 ]   [ left ]   [ left + 1 ... right - 1 ]   [ right ]   [ right + 1 ... N - 1 ]
└───────┬────────┘   └───┬──┘   └───────────┬────────────┘   └───┬───┘   └──────────┬──────────┘
  Settled columns      Active        Unexplored canyon         Active       Settled columns
  (Water calculated)   Column                                  Column       (Water calculated)
```

#### 3.5 State Transition Triggers & Decision Gates
1. **Decision Gate 1 (`height[left] <= height[right]`):**
   - If `height[left] >= leftMax`: Update `leftMax = height[left]` (new peak, no water trapped).
   - Else: `totalWater += leftMax - height[left]` (basin trapped under leftMax).
   - `left++`.
2. **Decision Gate 2 (`height[right] < height[left]`):**
   - If `height[right] >= rightMax`: Update `rightMax = height[right]` (new peak, no water trapped).
   - Else: `totalWater += rightMax - height[right]` (basin trapped under rightMax).
   - `right--`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `height = [0, 1, 0, 2, 1, 0, 1, 3, 2, 1, 2, 1]` ($N = 12$).

| Step | `L` ($h_L$) | `R` ($h_R$) | Invariant Evaluation | `leftMax` | `rightMax` | Water Trapped at Column | `totalWater` | Pointer Move |
| :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
| 1 | 0 (`0`) | 11 (`1`) | $h_L \le h_R \implies$ Process L | 0 | 0 | $0 - 0 = 0$ | 0 | `L++ (1)` |
| 2 | 1 (`1`) | 11 (`1`) | $h_L \le h_R \implies$ Process L | 1 | 0 | $1 - 1 = 0$ (Peak) | 0 | `L++ (2)` |
| 3 | 2 (`0`) | 11 (`1`) | $h_L \le h_R \implies$ Process L | 1 | 0 | $1 - 0 = 1$ | **1** | `L++ (3)` |
| 4 | 3 (`2`) | 11 (`1`) | $h_R < h_L \implies$ Process R | 1 | 1 | $1 - 1 = 0$ (Peak) | 1 | `R-- (10)` |
| 5 | 3 (`2`) | 10 (`2`) | $h_L \le h_R \implies$ Process L | 2 | 1 | $2 - 2 = 0$ (Peak) | 1 | `L++ (4)` |
| 6 | 4 (`1`) | 10 (`2`) | $h_L \le h_R \implies$ Process L | 2 | 1 | $2 - 1 = 1$ | **2** | `L++ (5)` |
| 7 | 5 (`0`) | 10 (`2`) | $h_L \le h_R \implies$ Process L | 2 | 1 | $2 - 0 = 2$ | **4** | `L++ (6)` |
| 8 | 6 (`1`) | 10 (`2`) | $h_L \le h_R \implies$ Process L | 2 | 1 | $2 - 1 = 1$ | **5** | `L++ (7)` |
| 9 | 7 (`3`) | 10 (`2`) | $h_R < h_L \implies$ Process R | 2 | 2 | $2 - 2 = 0$ (Peak) | 5 | `R-- (9)` |
| 10| 7 (`3`) | 9 (`1`) | $h_R < h_L \implies$ Process R | 2 | 2 | $2 - 1 = 1$ | **6** | `R-- (8)` |
| 11| 7 (`3`) | 8 (`2`) | $h_R < h_L \implies$ Process R | 2 | 2 | $2 - 2 = 0$ | 6 | `R-- (7)` |
| 12| 7 | 7 | Pointers meet | - | - | - | **6** | Terminate |

Total Trapped Water: `6`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **Approach 1: Two Pointers (Optimal Space & Industry Standard)**
  - *When to Use:* Always. Strictly $O(N)$ single pass, $O(1)$ auxiliary space, zero allocations. Accumulates water *vertically* column-by-column.
- **Approach 2: Dynamic Programming Prefix/Suffix Arrays**
  - *When to Use:* Best for explaining the foundational intuition of $\min(leftMax, rightMax)$ before introducing the space optimization.
- **Approach 3: Monotonic Stack**
  - *When to Use:* When water must be accumulated *horizontally* slice-by-slice, or when solving related geometric problems like Largest Rectangle in Histogram (LeetCode #84).

#### 4.2 Step-by-Step Natural Progression Flow
1. **Step 1: Setup:** Check `height.Length < 3`. Initialize `left = 0`, `right = n - 1`, `leftMax = 0`, `rightMax = 0`, `totalWater = 0`.
2. **Step 2: Exploration Loop:** While `left < right`.
3. **Step 3: Invariant Execution:** If `height[left] <= height[right]`, update `leftMax` and add delta, increment `left`. Else update `rightMax` and add delta, decrement `right`.
4. **Step 4: Return:** Return `totalWater`.

#### 4.3 Alternative Approaches Analysis
- **Monotonic Decreasing Stack:** Stores indices of decreasing heights. When a taller bar is found, it pops the bottom of the basin, uses the previous stack element as left boundary, and computes trapped water horizontally as:
  $$\text{sliceWater} = (\min(height[left], height[i]) - height[bottom]) \times (i - left - 1)$$
  Time is $O(N)$ and Space is $O(N)$ stack memory.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Auxiliary Space | Output Space | Accumulation Mode | Cache Locality | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **1. Two Pointers** | $O(N)$ | $O(N)$ | $O(N)$ | $O(1)$ | $O(1)$ | Vertical columns | High | Moderate |
| **2. Dynamic Programming**| $O(N)$ | $O(N)$ | $O(N)$ | $O(N)$ arrays | $O(1)$ | Vertical columns | High | Poor (3 passes) |
| **3. Monotonic Stack** | $O(N)$ | $O(N)$ | $O(N)$ | $O(N)$ stack | $O(1)$ | Horizontal slices | Moderate | High (online stream) |

---

### 5. Production C# Implementations

```csharp
/*
 * ============================================================================
 * HEADER ANCHOR BLOCK: Trapping Rain Water
 * ============================================================================
 * Core Pattern      : Two Pointers Boundary Limiting Invariant
 * Time Complexity   : O(N) strictly linear single pass
 * Space Complexity  : O(1) auxiliary space (zero heap memory allocation)
 * Selection Rule    : Default to Two Pointers for optimal O(1) memory.
 *                     Use Monotonic Stack if water must be calculated horizontally as elements stream.
 * Defensive Traps   : Verify height.Length >= 3 (at least 3 bars required to form any basin).
 * ============================================================================
 */

public class Solution
{
    /// <summary>
    /// Computes total trapped water in O(N) time and O(1) space using the Two Pointer Boundary Invariant.
    /// Accumulates water volume column-by-column vertically.
    /// </summary>
    public int Trap(int[] height)
    {
        // Guard Clause: A basin requires at least 3 vertical bars (left wall, floor, right wall)
        if (height == null || height.Length < 3)
        {
            return 0;
        }

        // Initialize opposing caliper pointers
        int left = 0;
        int right = height.Length - 1;

        // Running maximum height registers
        int leftMax = 0;
        int rightMax = 0;
        int totalWater = 0;

        while (left < right)
        {
            // Boundary Invariant Decision Gate:
            // Whichever wall is shorter acts as the definitive limiting ceiling for that side.
            if (height[left] <= height[right])
            {
                // Invariant: leftMax is the absolute global limiting height for column 'left',
                // because height[right] >= height[left], guaranteeing rightMax >= leftMax.
                if (height[left] >= leftMax)
                {
                    // New peak encountered: cannot trap water above a peak
                    leftMax = height[left];
                }
                else
                {
                    // Water trapped is the delta between ceiling (leftMax) and floor (height[left])
                    totalWater += leftMax - height[left];
                }

                // Advance left caliper
                left++;
            }
            else
            {
                // Symmetrical Invariant: rightMax is the definitive limiting height for column 'right'
                if (height[right] >= rightMax)
                {
                    // New peak encountered on the right
                    rightMax = height[right];
                }
                else
                {
                    // Water trapped is the delta between ceiling (rightMax) and floor (height[right])
                    totalWater += rightMax - height[right];
                }

                // Advance right caliper
                right--;
            }
        }

        return totalWater;
    }
}

/// <summary>
/// Approach 2: Dynamic Programming Prefix/Suffix Arrays.
/// Precalculates maximum left and right boundaries using O(N) auxiliary space.
/// </summary>
public class SolutionDP
{
    public int Trap(int[] height)
    {
        if (height == null || height.Length < 3) return 0;

        int n = height.Length;
        int[] leftMax = new int[n];
        int[] rightMax = new int[n];

        // Pass 1: Compute cumulative maximum from left to right
        leftMax[0] = height[0];
        for (int i = 1; i < n; i++)
        {
            leftMax[i] = Math.Max(leftMax[i - 1], height[i]);
        }

        // Pass 2: Compute cumulative maximum from right to left
        rightMax[n - 1] = height[n - 1];
        for (int i = n - 2; i >= 0; i--)
        {
            rightMax[i] = Math.Max(rightMax[i + 1], height[i]);
        }

        // Pass 3: Calculate trapped water column by column
        int totalWater = 0;
        for (int i = 0; i < n; i++)
        {
            totalWater += Math.Min(leftMax[i], rightMax[i]) - height[i];
        }

        return totalWater;
    }
}

/// <summary>
/// Approach 3: Monotonic Decreasing Stack.
/// Accumulates trapped water horizontally slice-by-slice.
/// </summary>
public class SolutionMonotonicStack
{
    public int Trap(int[] height)
    {
        if (height == null || height.Length < 3) return 0;

        // Monotonic stack storing indices of bars with decreasing heights
        var stack = new Stack<int>();
        int totalWater = 0;

        for (int i = 0; i < height.Length; i++)
        {
            // When current bar is taller than the stack top, a valley floor has been enclosed
            while (stack.Count > 0 && height[i] > height[stack.Peek()])
            {
                int bottomIndex = stack.Pop();

                // If stack becomes empty, there is no left boundary to enclose water
                if (stack.Count == 0)
                {
                    break;
                }

                int leftIndex = stack.Peek();

                // Height of horizontal water slice bounded by left wall and current wall
                int boundedHeight = Math.Min(height[leftIndex], height[i]) - height[bottomIndex];

                // Width of horizontal water slice
                int width = i - leftIndex - 1;

                totalWater += boundedHeight * width;
            }

            stack.Push(i);
        }

        return totalWater;
    }
}
```

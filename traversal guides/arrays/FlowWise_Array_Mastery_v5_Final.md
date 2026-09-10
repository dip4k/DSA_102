# Array Mastery 2.0: The Complete Problem-Solving Curriculum

This curriculum shifts focus from syntax memorization to state management. For each pattern, you must define the invariant, understand the pointer states, and execute the transitions safely. 

## Summary Table

| Level | Mental Model | Pointer State | Drill Problems |
|-------|--------------|---------------|----------------|
| **0-3: Boundaries & Index Arithmetic** | The index is a mathematical construct representing position, distance, and boundaries. | Single pointer, bounded by `0` and `N-1`. | [LeetCode 189: Rotate Array], [LeetCode 344: Reverse String], [LeetCode 566: Reshape the Matrix] |
| **4: Multi-Pointer** | Pointers represent a specific relationship (converging, same direction) spanning an active region. | `L` and `R` pointers, independent or moving towards each other. | [LeetCode 167: Two Sum II - Input Array Is Sorted], [LeetCode 11: Container With Most Water], [LeetCode 88: Merge Sorted Array] |
| **5: Read/Write Pointers** | Separate reading (discovery) from writing (compaction). Blind scan vs conditional commit. | `Read` pointer scans unconditionally, `Write` points to the next available valid slot. | [LeetCode 27: Remove Element], [LeetCode 283: Move Zeroes], [LeetCode 26: Remove Duplicates from Sorted Array] |
| **6: Range & Sliding Window** | Treat `[L, R]` as a unified state (the window). Maintain window validity by shrinking/expanding. | Expand `R` unconditionally, shrink `L` conditionally to restore invariant. | [LeetCode 643: Maximum Average Subarray I], [LeetCode 3: Longest Substring Without Repeating Characters], [LeetCode 76: Minimum Window Substring] |
| **7: Prefix & Suffix State** | Precompute historical state traveling left-to-right or right-to-left to reduce O(N) lookup to O(1). | Implicit pointers building state arrays `Prefix[i]` and `Suffix[i]`. | [LeetCode 1480: Running Sum of 1d Array], [LeetCode 238: Product of Array Except Self], [LeetCode 42: Trapping Rain Water] |
| **8: Monotonic Deferral** | Do not immediately resolve an element. Defer its resolution using a stack/deque until future data arrives. | Implicit pointer iterating, Stack/Deque stores unresolved indices. | [LeetCode 739: Daily Temperatures], [LeetCode 239: Sliding Window Maximum], [LeetCode 84: Largest Rectangle in Histogram] |

---

## Phase 1: Foundation & Index Fluency

### Level 0-3: Boundaries & Index Arithmetic

Master the language of indices before writing loops. Understand exactly what an index represents in relation to the array's boundaries.

**Mental Models & Invariants:**
- **What does the index mean?** The index represents an absolute position or mathematical offset from a boundary.
- **What region is processed?** The whole array, or a specific mathematical mapping of elements.
- **What is the invariant?** Index operations must always stay within the valid bounds `[0, N-1]`.

**Index Mathematical Relationships:**

| Concept | Formula / Pattern | Mental Model |
|---------|-------------------|--------------|
| **Distance (Inclusive)** | `R - L + 1` | Number of elements including both bounds. |
| **Middle Element** | `L + (R - L) / 2` | Prevents integer overflow in large arrays. |
| **Circular Next** | `(i + k) % n` | Wrap around to the beginning seamlessly. |
| **Mirrored Index** | `n - 1 - i` | Symmetric opposite (e.g., for reversing). |
| **2D to 1D Mapping** | `row * cols + col`| Flatten a matrix into a single array. |

### ⚠️ Gotchas & Pitfalls
- **Empty input crashes**: Attempting to access `nums[0]` or `nums[N-1]` when `N=0` causes an index out of bounds error.
- **Integer overflow**: Using `(L + R) / 2` can overflow in some languages if L and R are large. Use `L + (R - L) / 2`.
- **Off-by-one errors**: Forgetting that the last valid index is `N-1`, or mixing up `<` and `<=` in loop bounds.

**Code Snippets:**

Problem: Reverse an array of characters in-place using mirrored index mapping.
```python
# Python: Reversing an array using mirrored index
def reverse_string(s: list[str]) -> None:
    # 1. Get the boundary size for the index math
    n = len(s)
    # 2. Iterate up to the middle element to avoid double-swapping
    for i in range(n // 2):
        # 3. Calculate the mirrored index from the right boundary
        mirror = n - 1 - i
        # 4. Swap the current element with its mirror counterpart
        s[i], s[mirror] = s[mirror], s[i]
```

Problem: Rotate an array to the right by k steps utilizing circular index arithmetic.
```csharp
// C#: Circular next calculation
public void Rotate(int[] nums, int k) {
    // 1. Get the array length to set boundaries
    int n = nums.Length;
    // 2. Normalize k to prevent unnecessary rotations larger than n
    k %= n;
    // 3. Create a temporary array to store the shifted values safely
    int[] res = new int[n];
    for (int i = 0; i < n; i++) {
        // 4. Calculate the new position using modulo for seamless wrap-around
        res[(i + k) % n] = nums[i];
    }
    // 5. Copy the fully resolved rotated array back to the original reference
    for (int i = 0; i < n; i++) nums[i] = res[i];
}
```

**Drill Problems:**
- **Easy**: [LeetCode 344: Reverse String] - Use mirrored indices (L and n-1-L).
- **Easy**: [LeetCode 566: Reshape the Matrix] - Map 2D coordinates to 1D, then back to new 2D.
- **Medium**: [LeetCode 189: Rotate Array] - Use circular index math to place items.

---

## Phase 2: Pointer Relationships & State

### Level 4: Multi-Pointer

Two pointers represent a specific relationship between positions. They can converge, move in the same direction, or act independently across multiple arrays.

**Mental Models & Invariants:**
- **What does the index mean?** The pointers `L` and `R` define the boundaries of the unexplored or active region.
- **What region is processed?** The shrinking window `[L, R]`, representing remaining candidates.
- **What is the invariant?** Elements outside `[L, R]` have been safely processed, categorized, or discarded.

**Converging Pointers State Transition (Two Sum Sorted, Target=9 on `[2, 7, 11, 15]`)**

| Step | L / R Positions | Sum vs Target | Action / Invariant |
|------|-----------------|---------------|--------------------|
| **Initial** | `L=0` (val:2), `R=3` (val:15) | 2+15=17 (> 9) | Sum too large. Decrement `R`. `[0, R]` unknown. |
| **Step 1** | `L=0` (val:2), `R=2` (val:11) | 2+11=13 (> 9) | Sum too large. Decrement `R`. |
| **Step 2** | `L=0` (val:2), `R=1` (val:7) | 2+7=9 (== 9) | Target found. Return `[L, R]`. |

### ⚠️ Gotchas & Pitfalls
- **Stagnant pointers**: Forgetting to increment/decrement `L` or `R` during a condition match, leading to an infinite loop.
- **Pointer crossover**: Using `while L <= R` when it should be `while L < R`, causing duplicate processing of the middle element.
- **Premature increment**: Updating pointer positions before safely extracting the current values or calculating the distance.

**Code Snippets:**

Problem: Find two numbers in a sorted array that add up to a specific target by converging from both ends.
```python
# Python: Two Sum II
def twoSum(numbers: list[int], target: int) -> list[int]:
    # 1. Initialize left and right pointers at the boundaries
    l, r = 0, len(numbers) - 1
    # 2. Loop while the pointers form a valid unseen region
    while l < r:
        # 3. Calculate current sum from pointers
        curr = numbers[l] + numbers[r]
        if curr == target:
            # 4. Target found, return 1-based indices
            return [l + 1, r + 1]
        elif curr > target:
            # 5. Sum too large, shrink the upper boundary
            r -= 1
        else:
            # 6. Sum too small, shrink the lower boundary
            l += 1
    return []
```

Problem: Find two lines that together with the x-axis form a container holding the maximum amount of water.
```csharp
// C#: Container With Most Water
public int MaxArea(int[] height) {
    // 1. Initialize converging pointers at both ends of the array
    int l = 0, r = height.Length - 1;
    int maxArea = 0;
    // 2. Continue until the pointers cross
    while (l < r) {
        // 3. The height of the water is limited by the shorter line
        int h = Math.Min(height[l], height[r]);
        // 4. Update max area with the current window size and height
        maxArea = Math.Max(maxArea, h * (r - l));
        
        // 5. Move the pointer pointing to the shorter line to seek a taller one
        if (height[l] < height[r]) l++;
        else r--;
    }
    return maxArea;
}
```

**Drill Problems:**
- **Easy**: [LeetCode 88: Merge Sorted Array] - Independent: Compare ends, write from right to left.
- **Medium**: [LeetCode 167: Two Sum II - Input Array Is Sorted] - Converging: Narrow search space based on sum.
- **Medium**: [LeetCode 11: Container With Most Water] - Converging: Move pointer pointing to shorter line.

---

### Level 5: Read/Write Pointers

Separate reading (discovery) from writing (compaction). The read pointer blindly scans. The write pointer only commits when a condition is met.

**Mental Models & Invariants:**
- **What does the index mean?** `Read` discovers elements; `Write` tracks the boundary of the compacted valid prefix.
- **What region is processed?** The prefix `[0, Write-1]` contains only safely processed valid elements.
- **What is the invariant?** `Write <= Read` always holds. All elements before `Write` guarantee validity.

**Read/Write State Flow (Move Zeroes on `[0, 1, 0, 3]`)**

| Step | Read Ptr (Val) | Write Ptr | Array State | Rule Applied |
|------|----------------|-----------|-------------|--------------|
| **Initial** | `R=0` (0) | `W=0` | `[0, 1, 0, 3]` | Value is 0. Read advances, Write waits. |
| **Step 1** | `R=1` (1) | `W=0` | `[1, 1, 0, 3]` | Valid! `nums[W]=nums[R]`. `W` increments. |
| **Step 2** | `R=2` (0) | `W=1` | `[1, 1, 0, 3]` | Value is 0. Read advances, Write waits. |
| **Step 3** | `R=3` (3) | `W=1` | `[1, 3, 0, 3]` | Valid! `nums[W]=nums[R]`. `W` increments. |

### ⚠️ Gotchas & Pitfalls
- **Overwriting unread data**: If `Write` somehow advances faster than `Read`, you might overwrite elements before reading them.
- **Forgetting to return the correct length**: When the compaction is done, you often need to return `Write` as the new length.
- **Out of bounds on the read pointer**: Trying to read `Read + 1` or `Read - 1` without checking if it's within bounds.

**Code Snippets:**

Problem: Move all zeroes in an array to the end while maintaining the relative order of non-zero elements.
```python
# Python: Move Zeroes
def moveZeroes(nums: list[int]) -> None:
    # 1. Initialize write pointer to track the boundary of valid elements
    write = 0
    # 2. Use read pointer to blindly scan the entire array
    for read in range(len(nums)):
        # 3. Condition met: current element is non-zero
        if nums[read] != 0:
            # 4. Swap to compact the valid element and advance write pointer
            nums[write], nums[read] = nums[read], nums[write]
            write += 1
```

Problem: Remove all occurrences of a specific value from an array in-place and return the new length.
```csharp
// C#: Remove Element
public int RemoveElement(int[] nums, int val) {
    // 1. Write pointer indicates the next available safe slot
    int write = 0;
    // 2. Read pointer evaluates every element unconditionally
    for (int read = 0; read < nums.Length; read++) {
        // 3. If element differs from the target value, it is valid
        if (nums[read] != val) {
            // 4. Commit the valid value to the write slot and increment it
            nums[write++] = nums[read];
        }
    }
    // 5. Write pointer naturally reflects the length of the compacted array
    return write;
}
```

**Drill Problems:**
- **Easy**: [LeetCode 27: Remove Element] - `W` tracks length of valid prefix.
- **Easy**: [LeetCode 283: Move Zeroes] - `W` tracks non-zero compact prefix.
- **Easy**: [LeetCode 26: Remove Duplicates from Sorted Array] - Compare `R` with `R-1`; write only unique.

---

### Level 6: Range & Sliding Window

Treat `[L, R]` as a unified state. Expand `R` to add to the window. Shrink `L` when the window violates a constraint. The goal is maintaining validity.

**Mental Models & Invariants:**
- **What does the index mean?** `[L, R]` represents a contiguous active segment (the window).
- **What region is processed?** The dynamic window between `L` and `R`.
- **What is the invariant?** The window `[L, R]` satisfies the problem constraint. If violated, shrink `L` until the invariant is restored.

**Variable Window Transition (Longest Subarray Sum <= 5 on `[2, 1, 3, 2]`)**

| Step | Window `[L, R]` | Values | Current Sum | Action / Invariant Check |
|------|-----------------|--------|-------------|--------------------------|
| **Initial** | `[0, 0]` | 2 | 2 (<= 5) | Valid. Record length 1. `R++` |
| **Step 1** | `[0, 1]` | 2, 1 | 3 (<= 5) | Valid. Record length 2. `R++` |
| **Step 2** | `[0, 2]` | 2, 1, 3 | 6 (> 5) | Invalid. Sum > 5. Shrink `L++` (remove 2) |
| **Step 3** | `[1, 2]` | 1, 3 | 4 (<= 5) | Valid again. `R++` |

### ⚠️ Gotchas & Pitfalls
- **Shrinking past `R`**: The `L` pointer shrinking past `R`, violating `L <= R`. You must guard the `L` increment or handle negative window sizes.
- **Missing the last window**: Forgetting to update the global max/min result after the loop finishes if the longest sequence is at the end.
- **State mismatch**: Forgetting to remove the element at `nums[L]` from the window state (sum, hashmap) before incrementing `L`.

**Code Snippets:**

Problem: Find the length of the longest contiguous substring without repeating characters using a sliding window.
```python
# Python: Longest Substring w/o Repeats
def lengthOfLongestSubstring(s: str) -> int:
    # 1. Maintain a set to track the state of characters in the current window
    char_set = set()
    # 2. Initialize the left boundary of the window
    l = 0
    max_len = 0
    # 3. Expand the window unconditionally by moving the right boundary
    for r in range(len(s)):
        # 4. Invariant violated: duplicate character found. Shrink left boundary until valid.
        while s[r] in char_set:
            char_set.remove(s[l])
            l += 1
        # 5. Invariant restored: safely add new character to the window
        char_set.add(s[r])
        # 6. Record the valid window size
        max_len = max(max_len, r - l + 1)
    return max_len
```

Problem: Find a contiguous subarray of fixed length k that has the maximum average value.
```csharp
// C#: Maximum Average Subarray I (Fixed Window)
public double FindMaxAverage(int[] nums, int k) {
    // 1. Setup the initial window state for the first k elements
    int sum = 0;
    for (int i = 0; i < k; i++) sum += nums[i];
    
    int maxSum = sum;
    // 2. Slide the fixed-size window across the rest of the array
    for (int i = k; i < nums.Length; i++) {
        // 3. Update the window state: add new right element, subtract old left element
        sum += nums[i] - nums[i - k];
        // 4. Track the maximum valid state seen so far
        maxSum = Math.Max(maxSum, sum);
    }
    // 5. Compute the final average using the maximum sum
    return (double)maxSum / k;
}
```

**Drill Problems:**
- **Easy**: [LeetCode 643: Maximum Average Subarray I] - Fixed window size K.
- **Medium**: [LeetCode 3: Longest Substring Without Repeating Characters] - Variable window. Shrink `L` until char is unique.
- **Hard**: [LeetCode 76: Minimum Window Substring] - Variable window. Maintain frequency map validity.

---

## Phase 3: Precomputation & Optimization

### Level 7: Prefix & Suffix State

Turn O(N) historical lookups into O(1) lookups by precomputing state traveling left-to-right (Prefix) or right-to-left (Suffix).

**Mental Models & Invariants:**
- **What does the index mean?** The index `i` cleanly separates the array into a `[0, i-1]` history (Prefix) and an `[i+1, N-1]` future (Suffix).
- **What region is processed?** Two passes: left-to-right to build prefix state, right-to-left to build suffix state.
- **What is the invariant?** `Prefix[i]` holds aggregated state *up to* index `i`. Future dependencies are stripped away.

**Prefix/Suffix Product Flow (Array: `[1, 2, 3, 4]`)**

| Index | Value | Prefix[i] (Left) | Suffix[i] (Right) | Result (Prefix * Suffix) |
|-------|-------|------------------|-------------------|--------------------------|
| **0** | 1 | 1 (default) | 24 (2\*3\*4) | 24 |
| **1** | 2 | 1 (1) | 12 (3\*4) | 12 |
| **2** | 3 | 2 (1\*2) | 4 (4) | 8 |
| **3** | 4 | 6 (1\*2\*3) | 1 (default) | 6 |

### ⚠️ Gotchas & Pitfalls
- **Modification during read**: Updating the original array in-place while still needing its original values for future prefix/suffix calculations.
- **Boundary default values**: Failing to initialize the default state (e.g., 0 for sum, 1 for product) for indices outside the array bounds.
- **Array size mismatch**: Allocating an array of size `N` but trying to access `N` instead of `N-1`, leading to out of bounds when checking the "rightmost" suffix.

**Code Snippets:**

Problem: Return an array where each element is the product of all other elements, without using division.
```python
# Python: Product of Array Except Self
def productExceptSelf(nums: list[int]) -> list[int]:
    n = len(nums)
    res = [1] * n
    
    # 1. Initialize running prefix product state
    prefix = 1
    # 2. Traverse left-to-right building up prefix dependencies
    for i in range(n):
        res[i] = prefix
        prefix *= nums[i]
    
    # 3. Initialize running suffix product state
    suffix = 1
    # 4. Traverse right-to-left, multiplying suffix with the precomputed prefix
    for i in range(n - 1, -1, -1):
        res[i] *= suffix
        suffix *= nums[i]
        
    return res
```

Problem: Compute a running sum of an array where each element contains the sum of all elements up to that index.
```csharp
// C#: Running Sum of 1D Array
public int[] RunningSum(int[] nums) {
    // 1. Allocate state array for the prefix sum
    int[] prefix = new int[nums.Length];
    // 2. Base case: first element has no prior history
    prefix[0] = nums[0];
    // 3. Build state iteratively using only the immediately preceding state and current value
    for (int i = 1; i < nums.Length; i++) {
        prefix[i] = prefix[i - 1] + nums[i];
    }
    return prefix;
}
```

**Drill Problems:**
- **Easy**: [LeetCode 1480: Running Sum of 1d Array] - Basic prefix sum.
- **Medium**: [LeetCode 238: Product of Array Except Self] - Multiply `Prefix[i-1] * Suffix[i+1]`.
- **Hard**: [LeetCode 42: Trapping Rain Water] - `Min(MaxLeft[i], MaxRight[i]) - Height[i]`.

---

### Level 8: Monotonic Deferral

Do not immediately resolve an element. Store its index in a stack/deque until future data arrives that answers its question (e.g., finding the Next Greater Element).

**Mental Models & Invariants:**
- **What does the index mean?** It points to an unresolved question waiting for future data (e.g., waiting for a warmer day).
- **What region is processed?** Left-to-right iteration while maintaining a monotonic structure.
- **What is the invariant?** The stack/deque strictly preserves elements in monotonic order. Incoming data that breaks this order causes older data to be resolved and popped.

### ⚠️ Gotchas & Pitfalls
- **Stack containing values instead of indices**: Storing values in the stack makes it impossible to calculate distances (e.g., `i - stack.pop()`). Always store indices.
- **Unresolved elements remaining**: Forgetting that after the loop finishes, there may still be unresolved elements left in the stack.
- **Monotonicity direction flip**: Accidentally using a monotonically increasing stack when a monotonically decreasing one is needed for the problem.

**Code Snippets:**

Problem: For each day, find the number of days you have to wait until a warmer temperature.
```python
# Python: Daily Temperatures
def dailyTemperatures(temperatures: list[int]) -> list[int]:
    res = [0] * len(temperatures)
    # 1. Initialize stack to hold indices of unresolved days
    stack = [] 
    # 2. Iterate through each temperature to process incoming data
    for i, t in enumerate(temperatures):
        # 3. If current temperature breaks the decreasing monotonicity, resolve pending indices
        while stack and temperatures[stack[-1]] < t:
            prev_i = stack.pop()
            # 4. Calculate the distance between the resolved day and the current day
            res[prev_i] = i - prev_i
        # 5. Defer the current day's index onto the stack
        stack.append(i)
    return res
```

Problem: For each day, find the number of days you have to wait until a warmer temperature.
```csharp
// C#: Daily Temperatures
public int[] DailyTemperatures(int[] temperatures) {
    int[] res = new int[temperatures.Length];
    // 1. Maintain a stack of indices for elements awaiting a larger value
    Stack<int> stack = new Stack<int>();
    for (int i = 0; i < temperatures.Length; i++) {
        // 2. While incoming data resolves the top of the stack, pop and process
        while (stack.Count > 0 && temperatures[stack.Peek()] < temperatures[i]) {
            int prev_i = stack.Pop();
            // 3. Compute distance (days waited) for the resolved index
            res[prev_i] = i - prev_i;
        }
        // 4. Push the current unresolved index onto the monotonic stack
        stack.Push(i);
    }
    return res;
}
```

**Drill Problems:**
- **Medium**: [LeetCode 739: Daily Temperatures] - Stack maintains unresolved colder days.
- **Hard**: [LeetCode 239: Sliding Window Maximum] - Deque maintains strictly decreasing useful elements.
- **Hard**: [LeetCode 84: Largest Rectangle in Histogram] - Stack resolves when a shorter bar is found.
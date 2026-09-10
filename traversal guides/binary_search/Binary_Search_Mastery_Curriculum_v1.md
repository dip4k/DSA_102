# Binary Search Traversal Mastery Curriculum

## Summary Table

| Level | Mental Model | Pointer/State | Drill Problems |
|---|---|---|---|
| Level 1: Standard BS | Target exists in a sorted array. Eliminate half based on midpoint. | `left`, `right`, `mid` | [LeetCode 704: Binary Search] (Easy) |
| Level 2: Lower/Upper Bounds | Find the first (lower) or first element greater (upper) in sorted array. | `left`, `right`, `mid`, `ans` | [LeetCode 35: Search Insert Position] (Easy), [LeetCode 34: Find First and Last Position of Element in Sorted Array] (Medium) |
| Level 3: Rotated Arrays | Array sorted but shifted. One half is always perfectly sorted. | `left`, `right`, `mid` | [LeetCode 33: Search in Rotated Sorted Array] (Medium), [LeetCode 153: Find Minimum in Rotated Sorted Array] (Medium) |
| Level 4: Matrix Search | 2D matrix mapped to 1D, or eliminating rows/columns. | `left`, `right` / `row`, `col` | [LeetCode 74: Search a 2D Matrix] (Medium), [LeetCode 240: Search a 2D Matrix II] (Medium) |
| Level 5: Binary Search on Answer | Monotonic search space based on a boolean predicate condition `isValid(x)`. | `low`, `high`, `mid` | [LeetCode 875: Koko Eating Bananas] (Medium), [LeetCode 410: Split Array Largest Sum] (Hard) |

---

## Level 1: Standard Binary Search

* **What does the state/index mean?**: `left` and `right` bound the search space where the target could exist. `mid` is the element being tested.
* **What region is processed?**: The continuous subarray `[left, right]`.
* **What is the invariant/recurrence relation?**: `target` $\in$ `[left, right]`. If `nums[mid] < target`, target $\in$ `[mid + 1, right]`. If `nums[mid] > target`, target $\in$ `[left, mid - 1]`.

### Visual State Transitions
*Searching for `target = 9` in `nums = [-1,0,3,5,9,12]`*

| Step | Action | State (`left`, `right`, `mid`) | Invariant |
|---|---|---|---|
| 1 | Test `mid=2` (val 3) | `L=0, R=5, mid=2` | `target` is in `[0, 5]` |
| 2 | `3 < 9` -> `left = mid + 1` | `L=3, R=5, mid=4` | `target` is in `[3, 5]` |
| 3 | Test `mid=4` (val 9) | `L=3, R=5, mid=4` | Match found at `index 4` |

### Code Snippets

Problem: Find the exact index of a target value within a sorted array, returning -1 if not found.

```python
def search(nums, target):
    # 1. Initialize pointers to the start and end of the array
    left, right = 0, len(nums) - 1
    
    # 2. Continue while the search space is valid (inclusive bounds)
    while left <= right:
        # 3. Calculate midpoint, avoiding integer overflow
        mid = left + (right - left) // 2
        
        if nums[mid] == target:
            # 4. Target found, return index
            return mid
        elif nums[mid] < target:
            # 5. Target must be in the right half, update left pointer
            left = mid + 1
        else:
            # 6. Target must be in the left half, update right pointer
            right = mid - 1
            
    # 7. Target not found
    return -1
```

```csharp
public int Search(int[] nums, int target) {
    // 1. Initialize pointers to the start and end of the array
    int left = 0, right = nums.Length - 1;
    
    // 2. Continue while the search space is valid
    while (left <= right) {
        // 3. Calculate midpoint safely
        int mid = left + (right - left) / 2;
        
        if (nums[mid] == target) 
            return mid; // 4. Target found
            
        if (nums[mid] < target) 
            left = mid + 1; // 5. Search right half
        else 
            right = mid - 1; // 6. Search left half
    }
    
    return -1; // 7. Not found
}
```

### ⚠️ Gotchas & Pitfalls
* **Integer Overflow**: Calculating `mid = (left + right) / 2` can overflow in languages like C++/C#/Java. Always use `left + (right - left) / 2`.
* **Loop Condition**: Using `while left < right` instead of `left <= right` skips checking the last element when `left == right`.
* **Off-by-One**: Failing to do `left = mid + 1` or `right = mid - 1` leads to infinite loops.

### Drill Problems
* [LeetCode 704: Binary Search] (Easy)

---

## Level 2: Lower/Upper Bounds

* **What does the state/index mean?**: Searching for the boundary where a condition becomes true (e.g., `>= target` for lower bound).
* **What region is processed?**: Array `[left, right]`, reducing space until `left == right` (or tracking best answer).
* **What is the invariant/recurrence relation?**: The boundary lies within `[left, right]`. If condition is met at `mid`, `ans = mid, right = mid - 1`. Otherwise `left = mid + 1`.

### Visual State Transitions
*Finding lower bound of `target = 4` in `nums = [1, 2, 4, 4, 5]`*

| Step | Action | State (`left`, `right`, `mid`) | Invariant |
|---|---|---|---|
| 1 | Test `mid=2` (val 4) | `L=0, R=4, mid=2` | First `4` is in `[0, 4]` |
| 2 | `4 >= 4` -> `ans = 2, R = 1` | `L=0, R=1, mid=0` | First `4` is in `[0, 1]` or is at `2` |
| 3 | Test `mid=0` (val 1) | `L=0, R=1, mid=0` | `1 < 4` |
| 4 | `1 < 4` -> `L = 1` | `L=1, R=1, mid=1` | First `4` is in `[1, 1]` or is at `2` |
| 5 | Test `mid=1` (val 2) | `L=1, R=1, mid=1` | `2 < 4` |
| 6 | `2 < 4` -> `L = 2` | `L=2, R=1` (Loop Ends) | Best `ans = 2` |

### Code Snippets

Problem: Find the first index where the element is greater than or equal to a target value (lower bound).

```python
# Lower Bound: First index where nums[i] >= target
def lower_bound(nums, target):
    # 1. Initialize pointers for the full array range
    left, right = 0, len(nums) - 1
    # 2. Initialize answer to out-of-bounds (if target is larger than all elements)
    ans = len(nums)
    
    # 3. Loop to exhaust search space
    while left <= right:
        mid = left + (right - left) // 2
        
        # 4. Check if condition is met
        if nums[mid] >= target:
            # 5. Record potential answer and shrink right bound to find an even earlier index
            ans = mid
            right = mid - 1 # Look left
        else:
            # 6. Condition not met, boundary must be to the right
            left = mid + 1 # Look right
            
    return ans
```

```csharp
public int LowerBound(int[] nums, int target) {
    // 1. Initialize boundaries
    int left = 0, right = nums.Length - 1;
    // 2. Default answer if all elements are smaller than target
    int ans = nums.Length;
    
    // 3. Search while valid space exists
    while (left <= right) {
        int mid = left + (right - left) / 2;
        
        if (nums[mid] >= target) {
            // 4. Condition met: record and try to find a better (smaller) index
            ans = mid;
            right = mid - 1; // Look left
        } else {
            // 5. Condition failed: valid boundary must be further right
            left = mid + 1; // Look right
        }
    }
    return ans;
}
```

### ⚠️ Gotchas & Pitfalls
* **Excluding Potential Answer**: If using `right = mid - 1` without recording `ans = mid`, you lose the boundary element.
* **Infinite Loops (Upper Bound):** Using `mid = left + (right - left) / 2` with `left = mid` can infinite loop when `left + 1 == right`. Either bias mid right `mid = left + (right - left + 1) / 2` or use `ans` tracking pattern.
* **Out of Bounds**: Target greater than all elements returns length of array; handle this edge case explicitly if accessing the array.

### Drill Problems
* [LeetCode 35: Search Insert Position] (Easy)
* [LeetCode 34: Find First and Last Position of Element in Sorted Array] (Medium)

---

## Level 3: Rotated Arrays

* **What does the state/index mean?**: A cyclically shifted sorted array. `left`, `right`, `mid` track the current segment.
* **What region is processed?**: `[left, right]`. At least one half of this region `[left, mid]` or `[mid, right]` is strictly sorted.
* **What is the invariant/recurrence relation?**: Identify the sorted half. Check if `target` falls within the range of the sorted half. If yes, search that half. Else, search the unsorted half.

### Visual State Transitions
*Searching for `target = 0` in `nums = [4,5,6,7,0,1,2]`*

| Step | Action | State (`left`, `right`, `mid`) | Invariant |
|---|---|---|---|
| 1 | Test `mid=3` (val 7) | `L=0, R=6, mid=3` | `[0,3]` is sorted `(4 <= 7)`. |
| 2 | `0` not in `[4, 7]` -> `L = mid + 1` | `L=4, R=6, mid=5` | Target must be in unsorted half `[4, 6]`. |
| 3 | Test `mid=5` (val 1) | `L=4, R=6, mid=5` | `[4,5]` is sorted `(0 <= 1)`. |
| 4 | `0` in `[0, 1]` -> `R = mid - 1` | `L=4, R=4, mid=4` | Target in sorted half `[4, 5]`. |
| 5 | Test `mid=4` (val 0) | `L=4, R=4, mid=4` | Match found at `index 4`. |

### Code Snippets

Problem: Find a target value in an array that was originally sorted but has been cyclically rotated.

```python
def search_rotated(nums, target):
    left, right = 0, len(nums) - 1
    while left <= right:
        mid = left + (right - left) // 2
        if nums[mid] == target:
            return mid
        
        # 1. Check if the left half is perfectly sorted
        if nums[left] <= nums[mid]:
            # 2. Determine if the target falls strictly within the sorted left half
            if nums[left] <= target < nums[mid]:
                right = mid - 1 # Target is here, discard right half
            else:
                left = mid + 1  # Target not here, discard left half
                
        # 3. Otherwise, the right half must be perfectly sorted
        else:
            # 4. Determine if target falls strictly within the sorted right half
            if nums[mid] < target <= nums[right]:
                left = mid + 1 # Target is here, discard left half
            else:
                right = mid - 1 # Target not here, discard right half
                
    return -1
```

```csharp
public int SearchRotated(int[] nums, int target) {
    int left = 0, right = nums.Length - 1;
    while (left <= right) {
        int mid = left + (right - left) / 2;
        if (nums[mid] == target) return mid;
        
        // 1. Identify if left segment is sorted
        if (nums[left] <= nums[mid]) {
            // 2. Check if target lies within the sorted left segment
            if (nums[left] <= target && target < nums[mid]) 
                right = mid - 1; // Search left
            else 
                left = mid + 1; // Search right
        } 
        // 3. Right segment must be sorted
        else {
            // 4. Check if target lies within the sorted right segment
            if (nums[mid] < target && target <= nums[right]) 
                left = mid + 1; // Search right
            else 
                right = mid - 1; // Search left
        }
    }
    return -1;
}
```

### ⚠️ Gotchas & Pitfalls
* **Strict Sorting Check**: Comparing `nums[left] <= nums[mid]` needs the `<=` to handle sub-arrays of size 2 where `left == mid`.
* **Target Inclusion**: When checking if target is in the sorted half, bounds must be strictly checked: `nums[left] <= target < nums[mid]`.
* **Duplicates**: If the array has duplicates, `nums[left] == nums[mid] == nums[right]` ruins the sorted-half check. Must increment `left` and decrement `right` to break ties.

### Drill Problems
* [LeetCode 33: Search in Rotated Sorted Array] (Medium)
* [LeetCode 153: Find Minimum in Rotated Sorted Array] (Medium)

---

## Level 4: Matrix Search

* **What does the state/index mean?**: A 2D array mapped to a conceptual 1D array (if perfectly sorted), or a coordinate `(row, col)` shrinking search space.
* **What region is processed?**: 1D mapping: `[0, m*n - 1]`. 2D Shrinking: Top-right or Bottom-left corners.
* **What is the invariant/recurrence relation?**: 1D: `row = mid // n`, `col = mid % n`. 2D Shrinking: Current value `< target` -> move to larger values. Current value `> target` -> move to smaller values.

### Visual State Transitions
*Searching `target = 3` in perfectly sorted 2D `3x4` Matrix (1D Mapping)*

| Step | Action | State (`left`, `right`, `mid`) | Invariant |
|---|---|---|---|
| 1 | `L=0, R=11`, `mid=5` | `row=5//4=1, col=5%4=1` | Value at `(1,1)` vs `3` |
| 2 | `val=11 > 3` -> `R = 4` | `L=0, R=4, mid=2` | Target is in first 5 elements |
| 3 | `row=2//4=0, col=2%4=2` | Value at `(0,2)` vs `3` | `val=3 == 3` (Found) |

### Code Snippets

Problem: Search for a target value in a strictly sorted 2D matrix by conceptually flattening it into a 1D array.

```python
# Fully Sorted 2D Matrix (1D Mapping)
def searchMatrix(matrix, target):
    if not matrix or not matrix[0]: return False
    
    m, n = len(matrix), len(matrix[0])
    # 1. Define bounds as if the 2D matrix were a single 1D array
    left, right = 0, m * n - 1
    
    while left <= right:
        mid = left + (right - left) // 2
        
        # 2. Map the 1D midpoint back to 2D matrix coordinates
        row, col = divmod(mid, n)
        
        if matrix[row][col] == target:
            return True
        elif matrix[row][col] < target:
            # 3. Target is greater, search the conceptually right half
            left = mid + 1
        else:
            # 4. Target is smaller, search the conceptually left half
            right = mid - 1
            
    return False
```

```csharp
public bool SearchMatrix(int[][] matrix, int target) {
    if (matrix == null || matrix.Length == 0) return false;
    int m = matrix.Length, n = matrix[0].Length;
    
    // 1. Map entire matrix bounds to a 1D range
    int left = 0, right = m * n - 1;
    
    while (left <= right) {
        int mid = left + (right - left) / 2;
        
        // 2. Decode the 1D mid index into 2D (row, column) coordinates
        int row = mid / n, col = mid % n;
        
        if (matrix[row][col] == target) return true;
        
        // 3. Adjust 1D bounds based on the mapped 2D value
        if (matrix[row][col] < target) 
            left = mid + 1;
        else 
            right = mid - 1;
    }
    return false;
}
```

### ⚠️ Gotchas & Pitfalls
* **Dimensions Confusion**: `row = mid // n` (cols), NOT `// m`. Division by number of columns.
* **Empty Matrix**: Always handle `[]` or `[[]]` edge cases before applying logic.
* **Matrix Type**: Standard BS only works if the *entire* matrix is strictly sorted. If only rows/cols are sorted independently (like LC 240), use the step-wise coordinate approach starting from top-right.

### Drill Problems
* [LeetCode 74: Search a 2D Matrix] (Medium)
* [LeetCode 240: Search a 2D Matrix II] (Medium)

---

## Level 5: Binary Search on Answer

* **What does the state/index mean?**: `low` and `high` define the range of *possible answers*, not indices.
* **What region is processed?**: The problem's domain (e.g., minimum capacity `[max(weights), sum(weights)]`).
* **What is the invariant/recurrence relation?**: A monotonic boolean function `isValid(x)` dictates direction. If `isValid(mid)` is true, try to find a better (smaller/larger) valid answer. If false, answer must be in the other half.

### Visual State Transitions
*Koko eating bananas: find min `k` to finish within `H=8` hours. `piles=[3,6,7,11]`*
*Search space: `[1, max(piles)]` -> `[1, 11]`*

| Step | Action | State (`low`, `high`, `mid`) | Invariant (`isValid` check) |
|---|---|---|---|
| 1 | Test `mid=6` | `L=1, H=11, mid=6` | `isValid(6) -> (1+1+2+2) = 6 <= 8` (True) |
| 2 | `isValid(6)` is True -> `ans=6, H=5` | `L=1, H=5, mid=3` | Target `k <= 6`. Check smaller speeds. |
| 3 | Test `mid=3` | `L=1, H=5, mid=3` | `isValid(3) -> (1+2+3+4) = 10 <= 8` (False)|
| 4 | `isValid(3)` is False -> `L=4` | `L=4, H=5, mid=4` | Target `k > 3`. |
| 5 | Test `mid=4` | `L=4, H=5, mid=4` | `isValid(4) -> (1+2+2+3) = 8 <= 8` (True) |
| 6 | `isValid(4)` is True -> `ans=4, H=3` | `L=4, H=3` (End) | Best valid `ans=4` |

### Code Snippets

Problem: Find the minimum eating speed `k` such that all bananas are eaten within `h` hours by binary searching the range of possible speeds.

```python
import math

def minEatingSpeed(piles, h):
    # 1. Define the monotonic predicate: can we eat all bananas at speed k within h hours?
    def is_valid(k):
        return sum(math.ceil(p / k) for p in piles) <= h
        
    # 2. Set search space: min speed is 1, max speed is the largest pile
    left, right = 1, max(piles)
    ans = right
    
    # 3. Binary search over the answer range
    while left <= right:
        mid = left + (right - left) // 2
        
        # 4. If current speed is valid, record it and try to find a slower (smaller) valid speed
        if is_valid(mid):
            ans = mid
            right = mid - 1 
        else:
            # 5. If invalid, the speed is too slow, so search the faster (larger) half
            left = mid + 1  
            
    return ans
```

```csharp
public int MinEatingSpeed(int[] piles, int h) {
    // 1. Monotonic condition: checks if speed k is sufficient
    bool IsValid(int k) {
        int hours = 0;
        foreach (int p in piles) {
            hours += (p + k - 1) / k; // Ceiling division
        }
        return hours <= h;
    }

    // 2. Define search space bounds (1 to max pile size)
    int left = 1, right = 0;
    foreach (int p in piles) right = Math.Max(right, p);
    int ans = right;

    // 3. Search for the optimal eating speed
    while (left <= right) {
        int mid = left + (right - left) / 2;
        
        if (IsValid(mid)) {
            // 4. Valid speed found: save it and try even slower speeds
            ans = mid;
            right = mid - 1; // Try smaller
        } else {
            // 5. Invalid speed: must search faster speeds
            left = mid + 1; // Try larger
        }
    }
    return ans;
}
```

### ⚠️ Gotchas & Pitfalls
* **Search Space Bounds**: Defining `low` and `high` incorrectly. E.g., `low` can't be `0` for division. Sum/Max of array elements are common bounds.
* **Monotonicity**: The predicate function `isValid` MUST transition purely from `False -> True` or `True -> False`. If it fluctuates, binary search cannot be applied.
* **Efficiency of Predicate**: The `isValid(x)` function typically runs in $O(N)$ time. The total time complexity becomes $O(N \log(\text{high} - \text{low}))$. Avoid sorting or nested loops inside `isValid`.

### Drill Problems
* [LeetCode 875: Koko Eating Bananas] (Medium)
* [LeetCode 410: Split Array Largest Sum] (Hard)

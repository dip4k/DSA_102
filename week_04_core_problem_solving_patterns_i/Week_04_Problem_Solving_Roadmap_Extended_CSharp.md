# 🗺️ WEEK 4 PROBLEM-SOLVING ROADMAP EXTENDED C#

**Version:** v1.0  
**Purpose:** Week 4 C# problem-solving playbook for foundational array/sequence patterns  
**Target:** Transform pattern knowledge into C# coding fluency  
**Prerequisites:** Week 4 instructional files + standard support files complete  

---

## 🎯 WEEK 4 PROBLEM-SOLVING FRAMEWORK

**Decision Tree (Week 4 Patterns):**

| Problem Phrases/Signals | 🎯 Primary Pattern | C# Collection | Time/Space |
|-------------------------|-------------------|---------------|-----------|
| "Merge two sorted arrays/lists" | Two Pointers (Same Direction) | int[] or List<T> | O(N+M)/O(1) |
| "Remove duplicates in sorted array" | Two Pointers (Read/Write) | int[] (in-place) | O(N)/O(1) |
| "Two sum in sorted array" | Two Pointers (Opposite Direction) | int[] | O(N)/O(1) |
| "Container with most water" | Two Pointers (Opposite Direction) | int[] | O(N)/O(1) |
| "Maximum sum of K consecutive elements" | Sliding Window (Fixed) | int[] | O(N)/O(1) |
| "Avg/sum of all subarrays of size K" | Sliding Window (Fixed) | int[] | O(N)/O(1) |
| "Longest substring with K distinct chars" | Sliding Window (Variable) | Dictionary<char, int> | O(N)/O(K) |
| "Subarray sum equals K" | Sliding Window (Variable) | Dictionary<int, int> | O(N)/O(N) |
| "At most K distinct elements" | Sliding Window (Variable) | Dictionary<T, int> | O(N)/O(K) |
| "Counting inversions in array" | Divide & Conquer (Merge Sort) | int[] | O(N log N)/O(N) |
| "Majority element" | Divide & Conquer | int[] | O(N log N)/O(log N) |
| "Find peak element" | Binary Search (Answer Space) | int[] | O(log N)/O(1) |
| "Minimum in rotated sorted array" | Binary Search (Answer Space) | int[] | O(log N)/O(1) |
| "Search in rotated sorted array" | Binary Search (Answer Space) | int[] | O(log N)/O(1) |
| "Aggressive cows / minimum distance" | Binary Search (Feasibility) | int[] | O(N log MAX)/O(1) |

**Anti-Patterns:**
- ❌ "Use nested loops for sorted arrays" → Use Two Pointers for O(N)
- ❌ "Recalculate sum for each window" → Use Sliding Window accumulator
- ❌ "Sort for every search query" → Use Binary Search on sorted space
- ❌ "Check every partition" → Use Divide & Conquer for O(N log N)
- ❌ "Linear search in sorted array" → Binary Search gives O(log N)

---

## 💻 C# PATTERN IMPLEMENTATIONS (WEEK 4)

### Pattern 1: TWO POINTERS — Same Direction (Merge & Remove Duplicates)

**C# Mental Model:** Like `.NET IEnumerator<T>` with two cursors. Think "read pointer ahead, write pointer behind".

**When to Use:**
- ✅ Merging two sorted sequences
- ✅ Removing duplicates in-place
- ✅ Partitioning (slow-fast pointers)
- ✅ Floyd's cycle detection (fast moves 2x)

**Core C# Skeleton — Merge Sorted Arrays:**
```csharp
// Merge Two Sorted Arrays - O(N+M) time, O(1) space if modifying nums1
public void Merge(int[] nums1, int m, int[] nums2, int n) {
    // Validation
    if (nums2 == null || n == 0) return;
    
    // Two pointers starting from ends (to avoid overwriting nums1)
    int p1 = m - 1;      // Last element in nums1
    int p2 = n - 1;      // Last element in nums2
    int p = m + n - 1;   // Last position in merged result
    
    // Core pattern: compare and place larger element
    while (p1 >= 0 && p2 >= 0) {
        if (nums1[p1] > nums2[p2]) {
            nums1[p] = nums1[p1];
            p1--;
        } else {
            nums1[p] = nums2[p2];
            p2--;
        }
        p--;
    }
    
    // Copy remaining nums2 (nums1 already in place)
    while (p2 >= 0) {
        nums1[p] = nums2[p2];
        p2--;
        p--;
    }
}
```

**C# Notes:**
- Fill from END to avoid overwriting when merging in-place.
- No need to copy remaining `nums1` elements (already in correct position).

**Variant — Remove Duplicates from Sorted Array:**
```csharp
// Remove Duplicates - O(N) time, O(1) space (in-place)
public int RemoveDuplicates(int[] nums) {
    if (nums == null || nums.Length == 0) return 0;
    
    int writeIdx = 1; // Start writing from index 1
    
    for (int readIdx = 1; readIdx < nums.Length; readIdx++) {
        // Only write if different from previous
        if (nums[readIdx] != nums[readIdx - 1]) {
            nums[writeIdx] = nums[readIdx];
            writeIdx++;
        }
    }
    
    return writeIdx; // New length
}
```

**Variant — Remove Element:**
```csharp
// Remove Element (in-place) - O(N) time, O(1) space
public int RemoveElement(int[] nums, int val) {
    int writeIdx = 0;
    
    for (int readIdx = 0; readIdx < nums.Length; readIdx++) {
        if (nums[readIdx] != val) {
            nums[writeIdx] = nums[readIdx];
            writeIdx++;
        }
    }
    
    return writeIdx;
}
```

---

### Pattern 2: TWO POINTERS — Opposite Direction (Two Sum, Container)

**C# Mental Model:** Like converging `.NET LINQ` queries from both ends. Think "squeeze from outside to find interior solution".

**When to Use:**
- ✅ Two sum in sorted array
- ✅ Container with most water
- ✅ Trapping rain water (with additional logic)
- ✅ Valid palindrome checks

**Core C# Skeleton — Two Sum (Sorted):**
```csharp
// Two Sum II (Sorted Array) - O(N) time, O(1) space
public int[] TwoSum(int[] numbers, int target) {
    // Validation
    if (numbers == null || numbers.Length < 2) return new int[] { };
    
    // Two pointers from opposite ends
    int left = 0;
    int right = numbers.Length - 1;
    
    while (left < right) {
        int sum = numbers[left] + numbers[right];
        
        if (sum == target) {
            return new int[] { left + 1, right + 1 }; // 1-indexed
        } else if (sum < target) {
            left++;  // Need larger sum
        } else {
            right--; // Need smaller sum
        }
    }
    
    return new int[] { }; // No solution
}
```

**C# Notes:**
- Array MUST be sorted for this pattern to work.
- Converging pointers guarantee O(N) single pass.

**Variant — Container With Most Water:**
```csharp
// Container With Most Water - O(N) time, O(1) space
public int MaxArea(int[] height) {
    int left = 0, right = height.Length - 1;
    int maxArea = 0;
    
    while (left < right) {
        // Width * min(height)
        int width = right - left;
        int currentHeight = Math.Min(height[left], height[right]);
        int area = width * currentHeight;
        
        maxArea = Math.Max(maxArea, area);
        
        // Move shorter line inward (only way to improve)
        if (height[left] < height[right]) {
            left++;
        } else {
            right--;
        }
    }
    
    return maxArea;
}
```

**Variant — Valid Palindrome:**
```csharp
// Valid Palindrome - O(N) time, O(1) space
public bool IsPalindrome(string s) {
    if (string.IsNullOrEmpty(s)) return true;
    
    int left = 0, right = s.Length - 1;
    
    while (left < right) {
        // Skip non-alphanumeric
        while (left < right && !char.IsLetterOrDigit(s[left])) {
            left++;
        }
        while (left < right && !char.IsLetterOrDigit(s[right])) {
            right--;
        }
        
        // Compare (case-insensitive)
        if (char.ToLower(s[left]) != char.ToLower(s[right])) {
            return false;
        }
        
        left++;
        right--;
    }
    
    return true;
}
```

**Variant — Three Sum:**
```csharp
// Three Sum - O(N²) time, O(1) space (ignoring output)
public IList<IList<int>> ThreeSum(int[] nums) {
    var result = new List<IList<int>>();
    if (nums == null || nums.Length < 3) return result;
    
    Array.Sort(nums); // O(N log N)
    
    for (int i = 0; i < nums.Length - 2; i++) {
        // Skip duplicates for first element
        if (i > 0 && nums[i] == nums[i - 1]) continue;
        
        // Two sum on remaining array
        int left = i + 1, right = nums.Length - 1;
        int target = -nums[i];
        
        while (left < right) {
            int sum = nums[left] + nums[right];
            
            if (sum == target) {
                result.Add(new List<int> { nums[i], nums[left], nums[right] });
                
                // Skip duplicates
                while (left < right && nums[left] == nums[left + 1]) left++;
                while (left < right && nums[right] == nums[right - 1]) right--;
                
                left++;
                right--;
            } else if (sum < target) {
                left++;
            } else {
                right--;
            }
        }
    }
    
    return result;
}
```

---

### Pattern 3: SLIDING WINDOW — Fixed Size

**C# Mental Model:** Like `.NET Enumerable.Window()` extension. Think "rolling accumulator over fixed-width window".

**When to Use:**
- ✅ Maximum/minimum/average of K consecutive elements
- ✅ Fixed-size subarray problems
- ✅ String pattern matching with fixed length

**Core C# Skeleton — Maximum Sum of K Consecutive:**
```csharp
// Max Sum of K Consecutive - O(N) time, O(1) space
public int MaxSumSubarray(int[] nums, int k) {
    // Validation
    if (nums == null || nums.Length < k) return 0;
    
    // Build first window
    int windowSum = 0;
    for (int i = 0; i < k; i++) {
        windowSum += nums[i];
    }
    
    int maxSum = windowSum;
    
    // Slide window: add new, remove old
    for (int i = k; i < nums.Length; i++) {
        windowSum += nums[i];           // Add new element
        windowSum -= nums[i - k];       // Remove leftmost of previous window
        maxSum = Math.Max(maxSum, windowSum);
    }
    
    return maxSum;
}
```

**C# Notes:**
- Build first window separately, then slide.
- Each slide: +1 element, -1 element (constant work).

**Variant — Average of Subarrays of Size K:**
```csharp
// Average of Subarrays - O(N) time, O(N) space for output
public double[] FindAverages(int[] arr, int k) {
    if (arr == null || arr.Length < k) return new double[] { };
    
    double[] result = new double[arr.Length - k + 1];
    double windowSum = 0;
    
    // First window
    for (int i = 0; i < k; i++) {
        windowSum += arr[i];
    }
    result[0] = windowSum / k;
    
    // Slide
    for (int i = k; i < arr.Length; i++) {
        windowSum += arr[i] - arr[i - k];
        result[i - k + 1] = windowSum / k;
    }
    
    return result;
}
```

**Variant — Contains Duplicate Within K:**
```csharp
// Contains Duplicate II - O(N) time, O(K) space
public bool ContainsNearbyDuplicate(int[] nums, int k) {
    var window = new HashSet<int>();
    
    for (int i = 0; i < nums.Length; i++) {
        // Check current
        if (window.Contains(nums[i])) {
            return true;
        }
        
        // Add to window
        window.Add(nums[i]);
        
        // Remove if window exceeds size k
        if (window.Count > k) {
            window.Remove(nums[i - k]);
        }
    }
    
    return false;
}
```

---

### Pattern 4: SLIDING WINDOW — Variable Size

**C# Mental Model:** Like `.NET Enumerable.TakeWhile()` with dynamic criteria. Think "expand until violated, shrink until valid".

**When to Use:**
- ✅ Longest substring with K distinct characters
- ✅ Subarray sum equals target
- ✅ Minimum window substring
- ✅ "At most K distinct" problems

**Core C# Skeleton — Longest Substring K Distinct:**
```csharp
// Longest Substring with K Distinct - O(N) time, O(K) space
public int LengthOfLongestSubstringKDistinct(string s, int k) {
    if (string.IsNullOrEmpty(s) || k == 0) return 0;
    
    var charCount = new Dictionary<char, int>();
    int maxLength = 0;
    int left = 0;
    
    for (int right = 0; right < s.Length; right++) {
        // Expand window: add s[right]
        char rightChar = s[right];
        if (charCount.ContainsKey(rightChar)) {
            charCount[rightChar]++;
        } else {
            charCount[rightChar] = 1;
        }
        
        // Shrink window while > K distinct
        while (charCount.Count > k) {
            char leftChar = s[left];
            charCount[leftChar]--;
            
            if (charCount[leftChar] == 0) {
                charCount.Remove(leftChar);
            }
            
            left++;
        }
        
        // Update max with valid window
        maxLength = Math.Max(maxLength, right - left + 1);
    }
    
    return maxLength;
}
```

**C# Notes:**
- Dictionary tracks frequency of each character in window.
- `while` loop shrinks window when constraint violated.

**Variant — Subarray Sum Equals K:**
```csharp
// Subarray Sum Equals K - O(N) time, O(N) space
public int SubarraySum(int[] nums, int k) {
    var prefixSumCount = new Dictionary<int, int>();
    prefixSumCount[0] = 1; // Empty prefix
    
    int count = 0;
    int currentSum = 0;
    
    foreach (int num in nums) {
        currentSum += num;
        
        // Check if (currentSum - k) exists
        int complement = currentSum - k;
        if (prefixSumCount.ContainsKey(complement)) {
            count += prefixSumCount[complement];
        }
        
        // Add current prefix sum
        if (prefixSumCount.ContainsKey(currentSum)) {
            prefixSumCount[currentSum]++;
        } else {
            prefixSumCount[currentSum] = 1;
        }
    }
    
    return count;
}
```

**Variant — Minimum Window Substring:**
```csharp
// Minimum Window Substring - O(N) time, O(M) space
public string MinWindow(string s, string t) {
    if (string.IsNullOrEmpty(s) || string.IsNullOrEmpty(t)) return "";
    
    // Frequency of chars in t
    var targetCount = new Dictionary<char, int>();
    foreach (char c in t) {
        if (targetCount.ContainsKey(c)) {
            targetCount[c]++;
        } else {
            targetCount[c] = 1;
        }
    }
    
    int required = targetCount.Count; // Unique chars needed
    int formed = 0;                   // Unique chars satisfied
    var windowCount = new Dictionary<char, int>();
    
    int left = 0, right = 0;
    int minLen = int.MaxValue;
    int minLeft = 0;
    
    while (right < s.Length) {
        // Expand
        char c = s[right];
        if (windowCount.ContainsKey(c)) {
            windowCount[c]++;
        } else {
            windowCount[c] = 1;
        }
        
        // Check if this char satisfies target
        if (targetCount.ContainsKey(c) && windowCount[c] == targetCount[c]) {
            formed++;
        }
        
        // Shrink while valid
        while (left <= right && formed == required) {
            // Update result
            if (right - left + 1 < minLen) {
                minLen = right - left + 1;
                minLeft = left;
            }
            
            // Remove left char
            char leftChar = s[left];
            windowCount[leftChar]--;
            if (targetCount.ContainsKey(leftChar) && windowCount[leftChar] < targetCount[leftChar]) {
                formed--;
            }
            
            left++;
        }
        
        right++;
    }
    
    return minLen == int.MaxValue ? "" : s.Substring(minLeft, minLen);
}
```

**Variant — Fruits Into Baskets (At Most 2 Types):**
```csharp
// Fruits Into Baskets - O(N) time, O(1) space (max 2 types)
public int TotalFruit(int[] fruits) {
    var basket = new Dictionary<int, int>();
    int maxFruits = 0;
    int left = 0;
    
    for (int right = 0; right < fruits.Length; right++) {
        // Add fruit
        if (basket.ContainsKey(fruits[right])) {
            basket[fruits[right]]++;
        } else {
            basket[fruits[right]] = 1;
        }
        
        // Shrink if > 2 types
        while (basket.Count > 2) {
            basket[fruits[left]]--;
            if (basket[fruits[left]] == 0) {
                basket.Remove(fruits[left]);
            }
            left++;
        }
        
        maxFruits = Math.Max(maxFruits, right - left + 1);
    }
    
    return maxFruits;
}
```

---

### Pattern 5: DIVIDE AND CONQUER

**C# Mental Model:** Like `.NET Parallel.Invoke()` on subproblems. Think "split, solve, combine".

**When to Use:**
- ✅ Merge sort / quick sort
- ✅ Counting inversions
- ✅ Majority element (divide & conquer variant)
- ✅ Maximum subarray (alternative to Kadane)

**Core C# Skeleton — Merge Sort:**
```csharp
// Merge Sort - O(N log N) time, O(N) space
public void MergeSort(int[] nums) {
    if (nums == null || nums.Length <= 1) return;
    
    int[] temp = new int[nums.Length];
    MergeSortHelper(nums, temp, 0, nums.Length - 1);
}

private void MergeSortHelper(int[] nums, int[] temp, int left, int right) {
    if (left >= right) return; // Base case
    
    // Divide
    int mid = left + (right - left) / 2;
    
    // Conquer
    MergeSortHelper(nums, temp, left, mid);
    MergeSortHelper(nums, temp, mid + 1, right);
    
    // Combine
    Merge(nums, temp, left, mid, right);
}

private void Merge(int[] nums, int[] temp, int left, int mid, int right) {
    // Copy to temp
    for (int i = left; i <= right; i++) {
        temp[i] = nums[i];
    }
    
    int i1 = left, i2 = mid + 1, current = left;
    
    // Merge back
    while (i1 <= mid && i2 <= right) {
        if (temp[i1] <= temp[i2]) {
            nums[current] = temp[i1];
            i1++;
        } else {
            nums[current] = temp[i2];
            i2++;
        }
        current++;
    }
    
    // Copy remaining left half (right already in place)
    while (i1 <= mid) {
        nums[current] = temp[i1];
        i1++;
        current++;
    }
}
```

**C# Notes:**
- Allocate `temp` array once at top level, reuse in recursion.
- Avoid repeated `new` allocations in recursive calls.

**Variant — Count Inversions:**
```csharp
// Count Inversions - O(N log N) time, O(N) space
public int CountInversions(int[] nums) {
    if (nums == null || nums.Length <= 1) return 0;
    
    int[] temp = new int[nums.Length];
    return CountInversionsHelper(nums, temp, 0, nums.Length - 1);
}

private int CountInversionsHelper(int[] nums, int[] temp, int left, int right) {
    if (left >= right) return 0;
    
    int mid = left + (right - left) / 2;
    
    int count = 0;
    count += CountInversionsHelper(nums, temp, left, mid);
    count += CountInversionsHelper(nums, temp, mid + 1, right);
    count += MergeAndCount(nums, temp, left, mid, right);
    
    return count;
}

private int MergeAndCount(int[] nums, int[] temp, int left, int mid, int right) {
    for (int i = left; i <= right; i++) {
        temp[i] = nums[i];
    }
    
    int i1 = left, i2 = mid + 1, current = left;
    int inversions = 0;
    
    while (i1 <= mid && i2 <= right) {
        if (temp[i1] <= temp[i2]) {
            nums[current] = temp[i1];
            i1++;
        } else {
            nums[current] = temp[i2];
            i2++;
            // All remaining in left half are > temp[i2]
            inversions += (mid - i1 + 1);
        }
        current++;
    }
    
    while (i1 <= mid) {
        nums[current] = temp[i1];
        i1++;
        current++;
    }
    
    return inversions;
}
```

**Variant — Majority Element (D&C):**
```csharp
// Majority Element (Divide & Conquer) - O(N log N) time, O(log N) space
public int MajorityElement(int[] nums) {
    return MajorityHelper(nums, 0, nums.Length - 1);
}

private int MajorityHelper(int[] nums, int left, int right) {
    // Base case
    if (left == right) return nums[left];
    
    int mid = left + (right - left) / 2;
    
    int leftMajority = MajorityHelper(nums, left, mid);
    int rightMajority = MajorityHelper(nums, mid + 1, right);
    
    // If same, that's the majority
    if (leftMajority == rightMajority) {
        return leftMajority;
    }
    
    // Count occurrences in current range
    int leftCount = CountInRange(nums, leftMajority, left, right);
    int rightCount = CountInRange(nums, rightMajority, left, right);
    
    return leftCount > rightCount ? leftMajority : rightMajority;
}

private int CountInRange(int[] nums, int target, int left, int right) {
    int count = 0;
    for (int i = left; i <= right; i++) {
        if (nums[i] == target) count++;
    }
    return count;
}
```

---

### Pattern 6: BINARY SEARCH AS A PATTERN

**C# Mental Model:** Like `.NET BinarySearch()` on abstract answer spaces. Think "guess-and-check with halving".

**When to Use:**
- ✅ Search in sorted array (classic)
- ✅ Find peak element
- ✅ Search in rotated sorted array
- ✅ Minimize maximum / maximize minimum
- ✅ Feasibility-based binary search

**Core C# Skeleton — Binary Search (Classic):**
```csharp
// Binary Search - O(log N) time, O(1) space
public int BinarySearch(int[] nums, int target) {
    if (nums == null || nums.Length == 0) return -1;
    
    int left = 0, right = nums.Length - 1;
    
    while (left <= right) {
        int mid = left + (right - left) / 2; // Avoid overflow
        
        if (nums[mid] == target) {
            return mid;
        } else if (nums[mid] < target) {
            left = mid + 1; // Search right half
        } else {
            right = mid - 1; // Search left half
        }
    }
    
    return -1; // Not found
}
```

**C# Notes:**
- Use `left + (right - left) / 2` to avoid integer overflow.
- Loop condition: `left <= right` (not just `<`).

**Variant — First Occurrence (Lower Bound):**
```csharp
// First Occurrence - O(log N) time, O(1) space
public int FirstOccurrence(int[] nums, int target) {
    int left = 0, right = nums.Length - 1;
    int result = -1;
    
    while (left <= right) {
        int mid = left + (right - left) / 2;
        
        if (nums[mid] == target) {
            result = mid;       // Record and continue left
            right = mid - 1;
        } else if (nums[mid] < target) {
            left = mid + 1;
        } else {
            right = mid - 1;
        }
    }
    
    return result;
}
```

**Variant — Search in Rotated Sorted Array:**
```csharp
// Search in Rotated Sorted Array - O(log N) time, O(1) space
public int SearchRotated(int[] nums, int target) {
    int left = 0, right = nums.Length - 1;
    
    while (left <= right) {
        int mid = left + (right - left) / 2;
        
        if (nums[mid] == target) return mid;
        
        // Determine which half is sorted
        if (nums[left] <= nums[mid]) {
            // Left half is sorted
            if (nums[left] <= target && target < nums[mid]) {
                right = mid - 1; // Target in left
            } else {
                left = mid + 1;  // Target in right
            }
        } else {
            // Right half is sorted
            if (nums[mid] < target && target <= nums[right]) {
                left = mid + 1;  // Target in right
            } else {
                right = mid - 1; // Target in left
            }
        }
    }
    
    return -1;
}
```

**Variant — Find Peak Element:**
```csharp
// Find Peak Element - O(log N) time, O(1) space
public int FindPeakElement(int[] nums) {
    int left = 0, right = nums.Length - 1;
    
    while (left < right) {
        int mid = left + (right - left) / 2;
        
        if (nums[mid] < nums[mid + 1]) {
            // Peak is in right half
            left = mid + 1;
        } else {
            // Peak is in left half (including mid)
            right = mid;
        }
    }
    
    return left; // left == right
}
```

**Variant — Minimum in Rotated Sorted Array:**
```csharp
// Find Minimum in Rotated Sorted Array - O(log N) time, O(1) space
public int FindMin(int[] nums) {
    int left = 0, right = nums.Length - 1;
    
    while (left < right) {
        int mid = left + (right - left) / 2;
        
        if (nums[mid] > nums[right]) {
            // Min is in right half
            left = mid + 1;
        } else {
            // Min is in left half (including mid)
            right = mid;
        }
    }
    
    return nums[left];
}
```

**Variant — Capacity to Ship Packages (Feasibility BS):**
```csharp
// Capacity to Ship Packages - O(N log SUM) time, O(1) space
public int ShipWithinDays(int[] weights, int days) {
    int left = weights.Max();      // Min capacity (largest single package)
    int right = weights.Sum();     // Max capacity (ship all at once)
    
    while (left < right) {
        int mid = left + (right - left) / 2;
        
        if (CanShip(weights, days, mid)) {
            right = mid; // Try smaller capacity
        } else {
            left = mid + 1; // Need larger capacity
        }
    }
    
    return left;
}

private bool CanShip(int[] weights, int days, int capacity) {
    int daysNeeded = 1;
    int currentLoad = 0;
    
    foreach (int weight in weights) {
        if (currentLoad + weight > capacity) {
            daysNeeded++;
            currentLoad = weight;
        } else {
            currentLoad += weight;
        }
    }
    
    return daysNeeded <= days;
}
```

---

## 📊 PROGRESSIVE PROBLEM LADDER (WEEK 4)

### 🟢 Stage 1: Canonical Problems (Core Pattern Application)

| # | LeetCode | Difficulty | Pattern | C# Focus |
|---|----------|------------|---------|----------|
| 1 | #88 | 🟢 Easy | Two Pointers (Same) | Array manipulation from end |
| 2 | #26 | 🟢 Easy | Two Pointers (Read/Write) | In-place modification |
| 3 | #167 | 🟡 Medium | Two Pointers (Opposite) | Converging pointers logic |
| 4 | #643 | 🟢 Easy | Sliding Window (Fixed) | Rolling sum calculation |
| 5 | #340 | 🔴 Hard | Sliding Window (Variable) | Dictionary frequency tracking |
| 6 | #493 | 🔴 Hard | Divide & Conquer | Merge sort modification |
| 7 | #704 | 🟢 Easy | Binary Search | Classic implementation |
| 8 | #33 | 🟡 Medium | Binary Search (Rotated) | Conditional search logic |

### 🟡 Stage 2: Variations with Constraints

| # | LeetCode | Difficulty | Pattern+Twist | C# Focus |
|---|----------|------------|---------------|----------|
| 1 | #27 | 🟢 Easy | Two Pointers + Remove | In-place element removal |
| 2 | #11 | 🟡 Medium | Two Pointers + Area | Min/Max calculations |
| 3 | #15 | 🟡 Medium | Two Pointers + Three Sum | Nested loops + deduplication |
| 4 | #209 | 🟡 Medium | Sliding Window + Min Length | Variable window with condition |
| 5 | #424 | 🟡 Medium | Sliding Window + Replacement | Window validity tracking |
| 6 | #560 | 🟡 Medium | Prefix Sum + Hash | Dictionary for complement lookup |
| 7 | #169 | 🟢 Easy | Divide & Conquer + Majority | Merge with counting |
| 8 | #34 | 🟡 Medium | Binary Search + First/Last | Double binary search |
| 9 | #153 | 🟡 Medium | Binary Search + Rotated Min | Condition-based halving |
| 10 | #162 | 🟡 Medium | Binary Search + Peak | Gradient-based search |

### 🟠 Stage 3: Integration & Advanced

| # | LeetCode | Difficulty | Patterns | C# Focus |
|---|----------|------------|----------|----------|
| 1 | #76 | 🔴 Hard | Sliding Window + Hash | Complex window validation |
| 2 | #992 | 🔴 Hard | Sliding Window + Atmost K | Counting subarrays logic |
| 3 | #1011 | 🟡 Medium | Binary Search + Feasibility | Custom CanShip check |
| 4 | #410 | 🔴 Hard | Binary Search + Split Array | Minimize maximum sum |
| 5 | #875 | 🟡 Medium | Binary Search + Eating Speed | Feasibility with ceiling division |
| 6 | #4 | 🔴 Hard | Binary Search + Two Arrays | Median of two sorted |

---

## ⚠ WEEK 4 PITFALLS & C# GOTCHAS

| Pattern | Common Bug | C# Symptom | Quick Fix |
|---------|------------|------------|-----------|
| **Two Pointers (Merge)** | Starting from beginning (overwriting) | Data loss | Fill from END in in-place merge |
| **Two Pointers (Opposite)** | Off-by-one with `left <= right` | Infinite loop | Use `left < right` for squeeze |
| **Two Pointers (Three Sum)** | Not skipping duplicates | Duplicate triplets | Add `while` to skip same values |
| **Sliding Window (Fixed)** | Recalculating sum every iteration | O(N*K) instead of O(N) | Use rolling sum (add/remove) |
| **Sliding Window (Variable)** | Not shrinking properly | Incorrect max length | `while` loop to shrink when invalid |
| **Sliding Window (Hash)** | Not removing from Dictionary | Memory leak / wrong count | Check and `Remove()` when count == 0 |
| **Divide & Conquer** | Creating new arrays in recursion | O(N²) space | Reuse single temp array |
| **Binary Search** | Using `(left + right) / 2` | Integer overflow | Use `left + (right - left) / 2` |
| **Binary Search** | Using `<` instead of `<=` | Misses single element | Use `left <= right` for exact match |
| **Binary Search (First/Last)** | Not updating `result` | Returns wrong index | Store `mid` when found, continue |
| **Binary Search (Rotated)** | Wrong sorted half check | Incorrect search direction | Check `nums[left] <= nums[mid]` |
| **Binary Search (Feasibility)** | Wrong boundary update | Infinite loop | Update `left = mid + 1` or `right = mid` |
| **Array Indexing** | Accessing `nums[mid + 1]` | `IndexOutOfRangeException` | Check `mid < nums.Length - 1` |
| **Dictionary** | Accessing without checking | `KeyNotFoundException` | Use `ContainsKey()` or `TryGetValue()` |

**Week 4 Collection Guide:**
- ✅ `int[]`: Fixed-size arrays for in-place algorithms
- ✅ `List<T>`: When building dynamic result lists
- ✅ `Dictionary<T, int>`: Frequency tracking in sliding window
- ✅ `HashSet<T>`: Membership testing in fixed window
- ✅ `Math.Max() / Math.Min()`: Tracking extremes
- ✅ `Array.Sort()`: Sorting for two-pointer variants
- ✅ `.Sum() / .Max() / .Min()`: LINQ for quick calculations (not in hot loops)

---

## ✅ WEEK 4 COMPLETION CHECKLIST

**Pattern Fluency:**
- [ ] Recall Two Pointers skeleton (same + opposite direction)
- [ ] Recall Sliding Window skeleton (fixed + variable size)
- [ ] Recall Divide & Conquer skeleton (merge sort template)
- [ ] Recall Binary Search skeleton (classic + variations)

**Problem Solving:**
- [ ] Solved 4-5 Stage 1 canonical problems
- [ ] Solved 5-7 Stage 2 variation problems
- [ ] 70%+ Stage 2 attempt rate (even if not perfect)

**C# Implementation:**
- [ ] Used correct indexing (avoid overflow, out-of-range)
- [ ] Handled edge cases (null, empty, single element)
- [ ] Avoided `IndexOutOfRangeException`, `KeyNotFoundException`, `OverflowException`
- [ ] Implemented in-place algorithms without extra `new` allocations
- [ ] Used rolling sums/accumulators correctly

**Code Quality:**
- [ ] All skeletons compile without syntax errors
- [ ] All functions handle edge cases (validation at start)
- [ ] All return types match problem specification
- [ ] Used meaningful variable names (left/right, not i/j)

**Ready for Week 5:** [ ] Yes / [ ] No (revisit patterns if No)

---

**End of Week 4 Extended C# Roadmap**  
*Master these foundational patterns and you'll solve 50%+ of array/sequence interview problems with confidence.*
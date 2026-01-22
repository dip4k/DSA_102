# 🛠️ Week 04 Extended Support – Core Problem-Solving Patterns I (C#)

> **Scope:** Two Pointers, Sliding Window (Fixed & Variable), Divide and Conquer, Binary Search as a Pattern  
> **Goal:** Move from mental models → clean, reusable **C# templates** for each pattern variant.

***

## 🧭 0. How to Use This File

1. **Start from the instructional file** for each day (mental model + invariants).  
2. **Use the diagrams** here to visualize pointer/recursion/binary-search movement.  
3. **Copy a template**, adapt only:
   - Condition checks  
   - Update logic  
   - Feasibility functions (for Binary Search on Answer)  
4. **Unit-test each template** with:
   - Typical case  
   - Edge case (empty arrays, single-element arrays, extremes)  
   - Stress case (large N, large values)

```text
MENTAL MODEL  →  VISUAL TRACE  →  GENERIC TEMPLATE  →  PROBLEM-SPECIFIC ADAPTATION
```

***

## 1️⃣ Day 1 – Two Pointers (All Variants)

### 1.1 Overall Visual: Pointer Motion

```text
Opposite Direction (Converging)
index:  0   1   2   3   4   5   6
        L →             ← R

Same Direction (Reader / Writer)
index:  0   1   2   3   4   5   6
        W   R →
Window-like advance with write lagging behind read
```

***

### 1.2 Two Pointers – Opposite Direction (Sorted Pair Search)

**Use for:**
- Two Sum in sorted array  
- Container With Most Water  
- Pair comparisons from both ends

#### Flow Diagram

```mermaid
flowchart LR
    A[Start: sorted array] --> B[Set left=0, right=n-1]
    B --> C[Compute metric using arr[left], arr[right]]
    C --> D{metric < target?}
    D -->|Yes| E[Move left++]
    D -->|No| F{metric > target?}
    F -->|Yes| G[Move right--]
    F -->|No| H[Found / record answer]
    E --> B
    G --> B
```

#### Template: Opposite Direction (Generic)

```csharp
public (int leftIndex, int rightIndex)? TwoPointerOppositeTemplate(
    int[] nums,
    Func<int, int, int> metricSelector,
    Func<int, int> compareToTarget)
{
    int left = 0;
    int right = nums.Length - 1;

    while (left < right)
    {
        int metric = metricSelector(nums[left], nums[right]);
        int cmp = compareToTarget(metric); 
        // cmp < 0 → too small; cmp > 0 → too large; cmp == 0 → good

        if (cmp == 0)
        {
            return (left, right);
        }
        else if (cmp < 0)
        {
            left++;   // need larger metric
        }
        else
        {
            right--;  // need smaller metric
        }
    }

    return null; // no pair found
}
```

**Specialization: Two Sum (sorted)**

```csharp
public int[] TwoSumSorted(int[] numbers, int target)
{
    int left = 0;
    int right = numbers.Length - 1;

    while (left < right)
    {
        long sum = (long)numbers[left] + numbers[right];
        if (sum == target)
            return new[] { left + 1, right + 1 }; // 1-based
        if (sum < target)
            left++;
        else
            right--;
    }
    return Array.Empty<int>();
}
```

**Specialization: Container With Most Water**

```csharp
public int MaxArea(int[] height)
{
    int left = 0;
    int right = height.Length - 1;
    int best = 0;

    while (left < right)
    {
        int h = Math.Min(height[left], height[right]);
        int width = right - left;
        int area = h * width;
        if (area > best) best = area;

        // Greedy: move the shorter side
        if (height[left] < height[right])
            left++;
        else
            right--;
    }

    return best;
}
```

***

### 1.3 Two Pointers – Same Direction (Reader/Writer)

**Use for:**
- Remove duplicates in-place  
- Move zeros to end  
- Partition array into regions

#### Flow Diagram

```text
Initial:
index:  0   1   2   3   4   5
        W
        R

Step:
if keep(nums[R]):
    nums[W] = nums[R]
    W++
R++

Invariant: [0..W-1] holds the "clean" / processed region
```

#### Template: Same Direction (Filter In-place)

```csharp
public int FilterInPlace<T>(T[] items, Func<T, bool> keep)
{
    int write = 0;

    for (int read = 0; read < items.Length; read++)
    {
        if (keep(items[read]))
        {
            items[write] = items[read];
            write++;
        }
    }

    // Length of filtered prefix
    return write;
}
```

**Specialization: Remove Duplicates from Sorted Array**

```csharp
public int RemoveDuplicates(int[] nums)
{
    if (nums.Length == 0) return 0;

    int write = 1;
    for (int read = 1; read < nums.Length; read++)
    {
        if (nums[read] != nums[write - 1])
        {
            nums[write] = nums[read];
            write++;
        }
    }
    return write;
}
```

**Specialization: Move Zeros to End (Stable)**

```csharp
public void MoveZeroes(int[] nums)
{
    int write = 0;

    for (int read = 0; read < nums.Length; read++)
    {
        if (nums[read] != 0)
        {
            nums[write] = nums[read];
            write++;
        }
    }

    // Fill remainder with zeros
    while (write < nums.Length)
    {
        nums[write] = 0;
        write++;
    }
}
```

***

## 2️⃣ Day 2 – Sliding Window (Fixed Size)

### 2.1 Visual: Fixed Window Movement

```text
Array:  [ 2, 1, 5, 1, 3, 2 ]
Window K=3

Step 1: [2, 1, 5]  -> sum = 8
Step 2:    [1, 5, 1]  -> sum = 8 - 2 + 1 = 7
Step 3:       [5, 1, 3]  -> sum = 7 - 1 + 3 = 9
Step 4:          [1, 3, 2]  -> sum = 9 - 5 + 2 = 6
```

### 2.2 Template: Fixed Window Generic

```csharp
public TWindowResult FixedWindowTemplate<TWindowResult>(
    int[] nums,
    int k,
    Func<long, TWindowResult, TWindowResult> updateAggregator,
    Func<long, TWindowResult> initialAggregator)
{
    if (k <= 0 || k > nums.Length)
        throw new ArgumentException("Invalid window size");

    long windowSum = 0;
    for (int i = 0; i < k; i++)
    {
        windowSum += nums[i];
    }

    TWindowResult result = initialAggregator(windowSum);

    for (int i = k; i < nums.Length; i++)
    {
        windowSum += nums[i];
        windowSum -= nums[i - k];
        result = updateAggregator(windowSum, result);
    }

    return result;
}
```

**Specialization: Maximum Average Subarray of Size K**

```csharp
public double FindMaxAverage(int[] nums, int k)
{
    long windowSum = 0;
    for (int i = 0; i < k; i++)
        windowSum += nums[i];

    long maxSum = windowSum;

    for (int i = k; i < nums.Length; i++)
    {
        windowSum += nums[i];
        windowSum -= nums[i - k];
        if (windowSum > maxSum)
            maxSum = windowSum;
    }

    return (double)maxSum / k;
}
```

**Specialization: Max in Each Window (with Deque)**

```csharp
public int[] MaxSlidingWindow(int[] nums, int k)
{
    if (nums.Length == 0 || k <= 0) return Array.Empty<int>();

    var deque = new LinkedList<int>(); // store indices
    var result = new List<int>();

    for (int i = 0; i < nums.Length; i++)
    {
        // Remove indices out of window (left side)
        while (deque.Count > 0 && deque.First.Value <= i - k)
        {
            deque.RemoveFirst();
        }

        // Remove smaller elements from right
        while (deque.Count > 0 && nums[deque.Last.Value] <= nums[i])
        {
            deque.RemoveLast();
        }

        deque.AddLast(i);

        // Window ready
        if (i >= k - 1)
        {
            result.Add(nums[deque.First.Value]);
        }
    }

    return result.ToArray();
}
```

***

## 3️⃣ Day 3 – Sliding Window (Variable Size)

### 3.1 Visual: Expand–Shrink Pattern

```text
Goal: Smallest subarray with sum >= S

L,R both start at 0

1) Expand R until sum >= S
2) Shrink L while sum >= S
3) Track best length
4) Continue moving R

Timeline:
[ L........R )  -> expand
[  L......R )   -> shrink
```

### 3.2 Template: Variable Window (Min-Length Satisfying Condition)

```csharp
public int MinWindowLengthTemplate(
    int[] nums,
    Func<long, bool> isValid)
{
    int n = nums.Length;
    int left = 0;
    long current = 0;
    int best = int.MaxValue;

    for (int right = 0; right < n; right++)
    {
        current += nums[right];

        while (left <= right && isValid(current))
        {
            int length = right - left + 1;
            if (length < best) best = length;

            current -= nums[left];
            left++;
        }
    }

    return best == int.MaxValue ? 0 : best;
}
```

**Specialization: Minimum Size Subarray Sum (positive integers)**

```csharp
public int MinSubArrayLen(int target, int[] nums)
{
    int left = 0;
    long sum = 0;
    int best = int.MaxValue;

    for (int right = 0; right < nums.Length; right++)
    {
        sum += nums[right];

        while (sum >= target)
        {
            best = Math.Min(best, right - left + 1);
            sum -= nums[left];
            left++;
        }
    }

    return best == int.MaxValue ? 0 : best;
}
```

***

### 3.3 Template: Variable Window (Max-Length Under Constraint)

**Use for:**
- Longest substring without repeating characters  
- Longest substring with at most K distinct characters  
- Longest subarray with at most K zeros flipped to ones

#### Generic Max-Length Template

```csharp
public int MaxWindowLengthTemplate(
    string s,
    Func<Dictionary<char, int>, bool> isValid)
{
    var freq = new Dictionary<char, int>();
    int left = 0;
    int best = 0;

    for (int right = 0; right < s.Length; right++)
    {
        char c = s[right];
        if (!freq.ContainsKey(c)) freq[c] = 0;
        freq[c]++;

        while (!isValid(freq))
        {
            char d = s[left];
            freq[d]--;
            if (freq[d] == 0) freq.Remove(d);
            left++;
        }

        int length = right - left + 1;
        if (length > best) best = length;
    }

    return best;
}
```

**Specialization: Longest Substring with At Most K Distinct Characters**

```csharp
public int LengthOfLongestSubstringKDistinct(string s, int k)
{
    if (k == 0 || string.IsNullOrEmpty(s)) return 0;

    var freq = new Dictionary<char, int>();
    int left = 0;
    int best = 0;

    for (int right = 0; right < s.Length; right++)
    {
        char c = s[right];
        if (!freq.ContainsKey(c)) freq[c] = 0;
        freq[c]++;

        while (freq.Count > k)
        {
            char d = s[left];
            freq[d]--;
            if (freq[d] == 0) freq.Remove(d);
            left++;
        }

        best = Math.Max(best, right - left + 1);
    }

    return best;
}
```

**Specialization: Longest Substring Without Repeating Characters (optimized)**

```csharp
public int LengthOfLongestSubstring(string s)
{
    var lastSeen = new int[128];
    Array.Fill(lastSeen, -1);

    int left = 0;
    int best = 0;

    for (int right = 0; right < s.Length; right++)
    {
        char c = s[right];
        if (lastSeen[c] >= left)
        {
            left = lastSeen[c] + 1;
        }
        lastSeen[c] = right;
        best = Math.Max(best, right - left + 1);
    }

    return best;
}
```

***

## 4️⃣ Day 4 – Divide and Conquer Pattern

### 4.1 Visual: Merge Sort Recursion Tree

```text
               [0..7]
              /      \
          [0..3]    [4..7]
          /   \      /   \
       [0..1][2..3][4..5][6..7]
       / \    / \   / \   / \
     [0][1][2][3] [4][5] [6][7]  (base cases)
```

### 4.2 Template: Divide and Conquer Skeleton

```csharp
public TResult DivideAndConquerTemplate<TResult, TInput>(
    TInput input,
    Func<TInput, bool> isBaseCase,
    Func<TInput, TResult> solveBaseCase,
    Func<TInput, (TInput left, TInput right)> split,
    Func<TResult, TResult, TResult> combine)
{
    if (isBaseCase(input))
        return solveBaseCase(input);

    var (leftInput, rightInput) = split(input);
    TResult leftResult = DivideAndConquerTemplate(
        leftInput, isBaseCase, solveBaseCase, split, combine);
    TResult rightResult = DivideAndConquerTemplate(
        rightInput, isBaseCase, solveBaseCase, split, combine);

    return combine(leftResult, rightResult);
}
```

### 4.3 Template: Merge Sort (Full C# Implementation)

```csharp
public class MergeSort
{
    public void Sort(int[] arr)
    {
        if (arr == null || arr.Length <= 1) return;
        int[] temp = new int[arr.Length];
        Sort(arr, temp, 0, arr.Length - 1);
    }

    private void Sort(int[] arr, int[] temp, int low, int high)
    {
        if (low >= high) return; // base case

        int mid = low + (high - low) / 2;

        Sort(arr, temp, low, mid);
        Sort(arr, temp, mid + 1, high);

        // Optimization: skip merge if already sorted
        if (arr[mid] <= arr[mid + 1]) return;

        Merge(arr, temp, low, mid, high);
    }

    private void Merge(int[] arr, int[] temp, int low, int mid, int high)
    {
        for (int i = low; i <= high; i++)
            temp[i] = arr[i];

        int left = low;
        int right = mid + 1;
        int current = low;

        while (left <= mid && right <= high)
        {
            if (temp[left] <= temp[right])
                arr[current++] = temp[left++];
            else
                arr[current++] = temp[right++];
        }

        while (left <= mid)
            arr[current++] = temp[left++];
        // Right half already in place
    }
}
```

***

## 5️⃣ Day 5 – Binary Search as a Pattern (All Variants)

### 5.1 Visual: Binary Search on Answer Space

```text
Answer space: [low ............ high]

Iteration 1: mid1 = (low+high)/2
   check(mid1) = False  -> need bigger  -> low = mid1 + 1

Iteration 2: mid2 = (low+high)/2
   check(mid2) = True   -> record ans   -> high = mid2 - 1
...
Stop when low > high
```

```mermaid
flowchart TD
    A[Define low, high] --> B[While low <= high]
    B --> C[Compute mid]
    C --> D[Run check(mid)]
    D -->|Feasible (True)| E[Update ans = mid, move high = mid - 1 (Minimize)]
    D -->|Not feasible| F[Move low = mid + 1]
    E --> B
    F --> B
    B -->|Exit| G[Return ans]
```

***

### 5.2 Template: Integer Binary Search on Answer – Minimize (Find First True)

```csharp
public int BinarySearchMinimize(
    int low,
    int high,
    Func<int, bool> isFeasible)
{
    int ans = high;

    while (low <= high)
    {
        int mid = low + (high - low) / 2;

        if (isFeasible(mid))
        {
            ans = mid;       // candidate
            high = mid - 1;  // search smaller
        }
        else
        {
            low = mid + 1;   // need larger
        }
    }

    return ans;
}
```

**Specialization: Capacity To Ship Packages Within D Days**

```csharp
public int ShipWithinDays(int[] weights, int days)
{
    int low = weights.Max();      // cannot be less than max item
    int high = weights.Sum();     // at most all in one day

    return BinarySearchMinimize(low, high, capacity => CanShip(weights, days, capacity));
}

private bool CanShip(int[] weights, int days, int capacity)
{
    int usedDays = 1;
    int current = 0;

    foreach (int w in weights)
    {
        if (current + w > capacity)
        {
            usedDays++;
            current = 0;
        }
        current += w;
        if (usedDays > days) return false;
    }

    return true;
}
```

***

### 5.3 Template: Integer Binary Search on Answer – Maximize (Find Last True)

```csharp
public int BinarySearchMaximize(
    int low,
    int high,
    Func<int, bool> isFeasible)
{
    int ans = low;

    while (low <= high)
    {
        int mid = low + (high - low) / 2;

        if (isFeasible(mid))
        {
            ans = mid;      // candidate
            low = mid + 1;  // search bigger
        }
        else
        {
            high = mid - 1; // need smaller
        }
    }

    return ans;
}
```

**Specialization: Aggressive Cows / Maximize Minimum Distance**

```csharp
public int MaximizeMinDistance(int[] positions, int cows)
{
    Array.Sort(positions);
    int low = 1;
    int high = positions[^1] - positions[0];

    return BinarySearchMaximize(low, high, d => CanPlace(positions, cows, d));
}

private bool CanPlace(int[] pos, int cows, int distance)
{
    int count = 1;
    int last = pos[0];

    for (int i = 1; i < pos.Length; i++)
    {
        if (pos[i] - last >= distance)
        {
            count++;
            last = pos[i];
            if (count >= cows) return true;
        }
    }

    return false;
}
```

***

### 5.4 Template: Real-Valued Binary Search (Precision-Based)

```csharp
public double BinarySearchReal(
    double low,
    double high,
    Func<double, bool> isFeasible,
    double epsilon = 1e-6)
{
    for (int iter = 0; iter < 100; iter++)
    {
        double mid = low + (high - low) / 2.0;

        if (isFeasible(mid))
        {
            high = mid;  // Minimize
        }
        else
        {
            low = mid;
        }

        if (high - low < epsilon)
            break;
    }

    return (low + high) / 2.0;
}
```

**Example Usage: Finding Square Root**

```csharp
public double SqrtBinary(double x, double epsilon = 1e-6)
{
    if (x < 0) throw new ArgumentException("Negative input");

    double low = 0;
    double high = Math.Max(1.0, x);

    return BinarySearchReal(
        low, high,
        mid => mid * mid >= x,
        epsilon);
}
```

***

## ✅ Summary: Pattern–Template Map

```text
Two Pointers (Opposite)   →  TwoSumSorted, MaxArea
Two Pointers (Same)       →  RemoveDuplicates, MoveZeroes
Sliding Window (Fixed)    →  FixedWindowTemplate, MaxAverage, MaxSlidingWindow
Sliding Window (Variable) →  MinWindowLengthTemplate, MaxWindowLengthTemplate, K-distinct variants
Divide & Conquer          →  Generic Template + MergeSort
Binary Search (Minimize)  →  BinarySearchMinimize + ShipWithinDays, Koko
Binary Search (Maximize)  →  BinarySearchMaximize + Aggressive Cows
Binary Search (Real)      →  BinarySearchReal + SqrtBinary
```

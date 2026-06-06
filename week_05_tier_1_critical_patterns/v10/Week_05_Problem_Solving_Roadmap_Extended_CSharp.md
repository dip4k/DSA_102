# 🗺️ WEEK 5 PROBLEM-SOLVING ROADMAP EXTENDED C#

**Purpose:** Week 5 C# problem-solving playbook for critical interview patterns  
**Target:** Transform pattern knowledge into C# coding fluency  
**Prerequisites:** Week 5 instructional files + standard support files complete  

---

## 🎯 WEEK 5 PROBLEM-SOLVING FRAMEWORK

**Decision Tree (Week 5 Patterns):**

| Problem Phrases/Signals | 🎯 Primary Pattern | C# Collection | Time/Space |
|-------------------------|-------------------|---------------|-----------|
| "Find two numbers that sum to..." | Hash Map | Dictionary<T,T> | O(N)/O(N) |
| "Anagrams", "group by frequency" | Hash Map + Frequency | Dictionary<char, int> | O(N)/O(N) |
| "Most/least frequent elements" | Hash Map + Heap | Dictionary + PriorityQueue | O(N log k) / O(k) |
| "Next greater/smaller element" | Monotonic Stack | Stack<int> | O(N)/O(N) |
| "Daily temperatures", "stock span" | Monotonic Stack | Stack<int> | O(N)/O(N) |
| "Largest rectangle in histogram" | Monotonic Stack (Advanced) | Stack<int> | O(N)/O(N) |
| "Merge overlapping intervals" | Sort + Sweep | List<Interval> | O(N log N)/O(N) |
| "Insert interval without re-sort" | Two-Pass Merge | List<Interval> | O(N)/O(N) |
| "Merge K sorted lists" | Two Pointers + Heap | PriorityQueue | O(NK log K)/O(K) |
| "Sort colors (0, 1, 2)" | Dutch National Flag | int[] (in-place) | O(N)/O(1) |
| "Move zeroes to end" | Two-Way Partition | int[] (in-place) | O(N)/O(1) |
| "Find missing number 1..N" | Cyclic Sort | int[] (in-place) | O(N)/O(1) |
| "Find duplicate in 1..N" | Cyclic Sort (Array as List) | int[] (in-place) | O(N)/O(1) |
| "Maximum subarray sum" | Kadane's DP | (no extra) | O(N)/O(1) |
| "Maximum product subarray" | Kadane (Min+Max tracking) | (no extra) | O(N)/O(1) |
| "Circular max subarray" | Kadane (Two-Pass) | (no extra) | O(N)/O(1) |
| "Linked list cycle detection" | Floyd's Cycle | (no extra) | O(N)/O(1) |
| "Find cycle start" | Floyd's Two-Phase | (no extra) | O(N)/O(1) |
| "Middle of linked list" | Fast & Slow | (no extra) | O(N)/O(1) |

**Anti-Patterns:**
- ❌ "Always use Sorting first" → Use Hash if unsorted, or Monotonic Stack
- ❌ "Always hash for frequency" → Use Partition if range-constrained (Cyclic Sort)
- ❌ "Use HashSet for cycle detection" → Use Floyd's for O(1) space
- ❌ "Brute force for max subarray" → Kadane's gives O(N) instantly
- ❌ "Use Hash for complement tracking only" → Hash also enables LRU Cache, frequency analysis

---

## 💻 C# PATTERN IMPLEMENTATIONS (WEEK 5)

### Pattern 1: HASH MAP & HASH SET — Complement Tracking & Frequency Counting

**C# Mental Model:** Like `.NET Dictionary<T, TValue>` for O(1) key-value lookups. Think "shopping list with quantities".

**When to Use:**
- ✅ Find two/three/k numbers with a target sum
- ✅ Count frequencies (anagrams, most common elements)
- ✅ Detect duplicates (membership set)
- ✅ Cache previous results (LRU foundation)

**Core C# Skeleton — Two Sum:**
```csharp
// Two Sum - O(N) time, O(N) space
public int[] TwoSum(int[] nums, int target) {
    // Validation
    if (nums == null || nums.Length < 2) return new int[] { };
    
    // Initialize: Dictionary stores value -> index
    var seen = new Dictionary<int, int>();
    
    // Core pattern: linear scan with complement lookup
    for (int i = 0; i < nums.Length; i++) {
        int complement = target - nums[i];
        
        // Check if complement exists in map
        if (seen.ContainsKey(complement)) {
            return new int[] { seen[complement], i };
        }
        
        // Store current value if not already seen (first occurrence)
        if (!seen.ContainsKey(nums[i])) {
            seen[nums[i]] = i;
        }
    }
    
    return new int[] { };
}
```

**C# Notes:**
- `Dictionary<K,V>`: O(1) average lookup. Use `ContainsKey()` before accessing to avoid KeyNotFoundException.
- `HashSet<T>`: O(1) membership test. Use when only checking existence, not storing values.

**Variant — Valid Anagram (Frequency Counting):**
```csharp
// Valid Anagram - O(N) time, O(1) space (max 26 characters)
public bool IsAnagram(string s, string t) {
    if (s.Length != t.Length) return false;
    
    var freqS = new Dictionary<char, int>();
    var freqT = new Dictionary<char, int>();
    
    // Build frequency maps
    foreach (char c in s) {
        if (freqS.ContainsKey(c)) freqS[c]++;
        else freqS[c] = 1;
    }
    
    foreach (char c in t) {
        if (freqT.ContainsKey(c)) freqT[c]++;
        else freqT[c] = 1;
    }
    
    // Compare: same keys and counts
    if (freqS.Count != freqT.Count) return false;
    
    foreach (var kvp in freqS) {
        if (!freqT.ContainsKey(kvp.Key) || freqT[kvp.Key] != kvp.Value) {
            return false;
        }
    }
    
    return true;
}
```

**Variant — Group Anagrams:**
```csharp
// Group Anagrams - O(N * K log K) where K = word length
public IList<IList<string>> GroupAnagrams(string[] strs) {
    var groups = new Dictionary<string, List<string>>();
    
    foreach (string word in strs) {
        // Sort characters to create canonical form (anagram key)
        char[] charArray = word.ToCharArray();
        Array.Sort(charArray);
        string sorted = new string(charArray);
        
        // Add to group
        if (!groups.ContainsKey(sorted)) {
            groups[sorted] = new List<string>();
        }
        groups[sorted].Add(word);
    }
    
    return new List<IList<string>>(groups.Values);
}
```

**Variant — Top K Frequent Elements:**
```csharp
// Top K Frequent - O(N log K) using Min-Heap
public int[] TopKFrequent(int[] nums, int k) {
    // Step 1: Count frequencies
    var freq = new Dictionary<int, int>();
    foreach (int num in nums) {
        if (freq.ContainsKey(num)) freq[num]++;
        else freq[num] = 1;
    }
    
    // Step 2: Min-Heap of size K (PriorityQueue in .NET 6+)
    var minHeap = new PriorityQueue<int, int>();
    
    foreach (var kvp in freq) {
        minHeap.Enqueue(kvp.Key, kvp.Value);
        
        // Keep only K largest
        if (minHeap.Count > k) {
            minHeap.Dequeue();
        }
    }
    
    // Step 3: Extract K elements
    int[] result = new int[k];
    for (int i = k - 1; i >= 0; i--) {
        result[i] = minHeap.Dequeue();
    }
    
    return result;
}
```

**Variant — Majority Element (Boyer-Moore):**
```csharp
// Majority Element - O(N) time, O(1) space (voting algorithm)
public int MajorityElement(int[] nums) {
    int candidate = 0;
    int count = 0;
    
    // Find candidate
    foreach (int num in nums) {
        if (count == 0) {
            candidate = num;
        }
        count += (num == candidate) ? 1 : -1;
    }
    
    // Candidate is majority (guaranteed in problem)
    return candidate;
}
```

---

### Pattern 2: MONOTONIC STACK — Next/Previous Element & Range Queries

**C# Mental Model:** Like a `.NET Stack<T>` where you maintain sorted order and pop non-useful elements. Think "removing short people when taller person arrives".

**When to Use:**
- ✅ Find next greater/smaller element
- ✅ Histogram area, trapping water
- ✅ Stock span problems
- ✅ Evaluate expressions with precedence

**Core C# Skeleton — Next Greater Element:**
```csharp
// Next Greater Element I - O(N) time, O(N) space
public int[] NextGreaterElement(int[] nums1, int[] nums2) {
    // Decreasing monotonic stack for nums2
    var stack = new Stack<int>();
    var nextGreater = new Dictionary<int, int>();
    
    // Process nums2 from right to left
    for (int i = nums2.Length - 1; i >= 0; i--) {
        // Pop smaller elements (they're blocked by nums2[i])
        while (stack.Count > 0 && stack.Peek() < nums2[i]) {
            stack.Pop();
        }
        
        // Current top is the next greater (or none)
        if (stack.Count > 0) {
            nextGreater[nums2[i]] = stack.Peek();
        } else {
            nextGreater[nums2[i]] = -1;
        }
        
        // Push current for future comparisons
        stack.Push(nums2[i]);
    }
    
    // Map results from nums1
    int[] result = new int[nums1.Length];
    for (int i = 0; i < nums1.Length; i++) {
        result[i] = nextGreater[nums1[i]];
    }
    
    return result;
}
```

**C# Notes:**
- `Stack<T>.Peek()`: Returns top without removing. `Pop()`: Returns and removes.
- Always check `Count > 0` before `Peek()` to avoid InvalidOperationException.

**Variant — Daily Temperatures (Return Days):**
```csharp
// Daily Temperatures - O(N) time, O(N) space
public int[] DailyTemperatures(int[] temperatures) {
    int n = temperatures.Length;
    int[] result = new int[n];
    var stack = new Stack<int>(); // Stores indices
    
    for (int i = 0; i < n; i++) {
        // Pop indices where this day is warmer
        while (stack.Count > 0 && temperatures[i] > temperatures[stack.Peek()]) {
            int prevIdx = stack.Pop();
            result[prevIdx] = i - prevIdx; // Days to wait
        }
        
        stack.Push(i);
    }
    
    // Remaining indices have no warmer day (result[i] = 0 by default)
    return result;
}
```

**Variant — Largest Rectangle in Histogram:**
```csharp
// Largest Rectangle in Histogram - O(N) time, O(N) space
public int LargestRectangleArea(int[] heights) {
    int maxArea = 0;
    var stack = new Stack<int>(); // Stores indices of heights
    
    for (int i = 0; i < heights.Length; i++) {
        // Pop taller bars (they're blocked by current bar)
        while (stack.Count > 0 && heights[i] < heights[stack.Peek()]) {
            int h = heights[stack.Pop()];
            int width = stack.Count == 0 ? i : i - stack.Peek() - 1;
            maxArea = Math.Max(maxArea, h * width);
        }
        
        stack.Push(i);
    }
    
    // Process remaining bars (height extends to end)
    while (stack.Count > 0) {
        int h = heights[stack.Pop()];
        int width = stack.Count == 0 ? heights.Length : heights.Length - stack.Peek() - 1;
        maxArea = Math.Max(maxArea, h * width);
    }
    
    return maxArea;
}
```

**Variant — Trapping Rain Water:**
```csharp
// Trapping Rain Water - O(N) time, O(N) space (Stack approach)
public int Trap(int[] height) {
    int trapped = 0;
    var stack = new Stack<int>(); // Indices of increasing heights
    
    for (int i = 0; i < height.Length; i++) {
        // Pop when current is taller (water trapped)
        while (stack.Count > 0 && height[i] > height[stack.Peek()]) {
            int bottomIdx = stack.Pop();
            
            if (stack.Count == 0) break; // No left wall
            
            int leftIdx = stack.Peek();
            int boundedHeight = Math.Min(height[i], height[leftIdx]) - height[bottomIdx];
            int width = i - leftIdx - 1;
            
            trapped += boundedHeight * width;
        }
        
        stack.Push(i);
    }
    
    return trapped;
}
```

**Variant — Stock Span (Consecutive Days ≤ Price):**
```csharp
// Online Stock Span - O(N) time, O(N) space
public class StockSpanner {
    private Stack<(int price, int span)> stack; // (price, cumulative span)
    
    public StockSpanner() {
        stack = new Stack<(int, int)>();
    }
    
    public int Next(int price) {
        int span = 1;
        
        // Pop prices <= current (they're dominated)
        while (stack.Count > 0 && stack.Peek().price <= price) {
            span += stack.Pop().span;
        }
        
        stack.Push((price, span));
        return span;
    }
}
```

---

### Pattern 3: MERGE OPERATIONS & INTERVAL PATTERNS — Sort & Sweep

**C# Mental Model:** Like sorting a classroom by height, then scanning linearly to find "groups". Think "timeline consolidation".

**When to Use:**
- ✅ Merge overlapping intervals
- ✅ Insert interval into sorted list
- ✅ Merge K sorted lists
- ✅ Meeting rooms / scheduler problems

**Core C# Skeleton — Merge Intervals:**
```csharp
// Merge Intervals - O(N log N) time, O(N) space
public int[][] Merge(int[][] intervals) {
    // Validation
    if (intervals == null || intervals.Length == 0) return new int[][] { };
    
    // Step 1: Sort by start time
    Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
    
    // Step 2: Sweep & merge
    var merged = new List<int[]>();
    int[] current = intervals[0];
    
    for (int i = 1; i < intervals.Length; i++) {
        // Overlapping: extend current
        if (intervals[i][0] <= current[1]) {
            current[1] = Math.Max(current[1], intervals[i][1]);
        } else {
            // Gap: save current, start new
            merged.Add(current);
            current = intervals[i];
        }
    }
    
    merged.Add(current); // Don't forget last
    
    return merged.ToArray();
}
```

**C# Notes:**
- `Array.Sort()` with custom Comparator. Lambda syntax: `(a, b) => a.CompareTo(b)`.
- `List<T>.ToArray()`: Converts list to array for return.

**Variant — Insert Interval (No Re-sort):**
```csharp
// Insert Interval - O(N) time, O(N) space (list already sorted)
public int[][] Insert(int[][] intervals, int[] newInterval) {
    var result = new List<int[]>();
    
    // Add all intervals ending before new starts
    int i = 0;
    while (i < intervals.Length && intervals[i][1] < newInterval[0]) {
        result.Add(intervals[i]);
        i++;
    }
    
    // Merge all overlapping with new
    int[] merged = newInterval;
    while (i < intervals.Length && intervals[i][0] <= merged[1]) {
        merged[0] = Math.Min(merged[0], intervals[i][0]);
        merged[1] = Math.Max(merged[1], intervals[i][1]);
        i++;
    }
    result.Add(merged);
    
    // Add remaining intervals starting after new ends
    while (i < intervals.Length) {
        result.Add(intervals[i]);
        i++;
    }
    
    return result.ToArray();
}
```

**Variant — Merge Sorted Array (Two Pointers):**
```csharp
// Merge Sorted Array - O(N+M) time, O(1) space (if modifying nums1)
public void Merge(int[] nums1, int m, int[] nums2, int n) {
    // Fill from the end to avoid overwriting nums1 data
    int p1 = m - 1;
    int p2 = n - 1;
    int p = m + n - 1;
    
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
    
    // Only nums2 leftover (nums1 already in place)
    while (p2 >= 0) {
        nums1[p] = nums2[p2];
        p2--;
        p--;
    }
}
```

**Variant — Meeting Rooms II (Max Concurrent):**
```csharp
// Meeting Rooms II - O(N log N) time, O(N) space (Sweep Line)
public int MinMeetingRooms(int[][] intervals) {
    // Create events: start (+1 room), end (-1 room)
    var events = new List<(int time, int delta)>();
    
    foreach (var interval in intervals) {
        events.Add((interval[0], 1));   // Start
        events.Add((interval[1], -1));  // End
    }
    
    // Sort: time ascending, ends BEFORE starts (if same time)
    events.Sort((a, b) => {
        if (a.time != b.time) return a.time.CompareTo(b.time);
        return a.delta.CompareTo(b.delta); // -1 (end) before 1 (start)
    });
    
    int maxRooms = 0;
    int currentRooms = 0;
    
    foreach (var (time, delta) in events) {
        currentRooms += delta;
        maxRooms = Math.Max(maxRooms, currentRooms);
    }
    
    return maxRooms;
}
```

---

### Pattern 4A: PARTITION & CYCLIC SORT — In-Place Segregation

**C# Mental Model:** Like `.NET Enumerable.OrderBy()` but in-place with pointers. Think "arranging cards without extra space".

**When to Use:**
- ✅ Sort 0s, 1s, 2s (Dutch National Flag)
- ✅ Move zeroes to end
- ✅ Partition for QuickSort
- ✅ Cyclic Sort for range 1..N

**Core C# Skeleton — Sort Colors (DNF):**
```csharp
// Sort Colors - O(N) time, O(1) space (in-place)
public void SortColors(int[] nums) {
    int low = 0, mid = 0, high = nums.Length - 1;
    
    while (mid <= high) {
        if (nums[mid] == 0) {
            // Swap with low, advance both
            Swap(nums, low, mid);
            low++;
            mid++;
        } else if (nums[mid] == 1) {
            // Already in middle, skip
            mid++;
        } else { // nums[mid] == 2
            // Swap with high, retreat high only
            Swap(nums, mid, high);
            high--;
            // Don't increment mid: need to check swapped value
        }
    }
}

private void Swap(int[] nums, int i, int j) {
    int temp = nums[i];
    nums[i] = nums[j];
    nums[j] = temp;
}
```

**C# Notes:**
- Three pointers: `low` (end of 0s), `mid` (scanner), `high` (start of 2s).
- **Critical:** Don't move `mid` when swapping with `high`—the swapped value needs inspection.

**Variant — Move Zeroes:**
```csharp
// Move Zeroes - O(N) time, O(1) space
public void MoveZeroes(int[] nums) {
    int writeIdx = 0;
    
    // Move all non-zero to front
    for (int i = 0; i < nums.Length; i++) {
        if (nums[i] != 0) {
            nums[writeIdx] = nums[i];
            writeIdx++;
        }
    }
    
    // Fill remainder with zeros
    while (writeIdx < nums.Length) {
        nums[writeIdx] = 0;
        writeIdx++;
    }
}
```

**Variant — Cyclic Sort (Missing Number):**
```csharp
// Missing Number - O(N) time, O(1) space
// Array contains 0..N (one missing), place each at index = value
public int MissingNumber(int[] nums) {
    int n = nums.Length;
    
    // Cyclic sort: place nums[i] at index nums[i] (if in range)
    for (int i = 0; i < n; i++) {
        // Keep swapping until nums[i] is in correct position or out of range
        while (nums[i] != i && nums[i] < n) {
            Swap(nums, i, nums[i]);
        }
    }
    
    // Find first index where nums[i] != i
    for (int i = 0; i < n; i++) {
        if (nums[i] != i) {
            return i;
        }
    }
    
    return n; // Missing is n itself
}
```

**Variant — Find the Duplicate (Array as Linked List):**
```csharp
// Find Duplicate - O(N) time, O(1) space
// Treat array values as linked list pointers: index -> value -> next index
public int FindDuplicate(int[] nums) {
    // Values are in range 1..N, map to indices 0..N-1
    for (int i = 0; i < nums.Length; i++) {
        int index = Math.Abs(nums[i]); // Use value as index
        
        // Mark visited by negating
        if (nums[index] < 0) {
            return index; // Already visited = duplicate
        }
        nums[index] = -nums[index];
    }
    
    return -1; // No duplicate found
}
```

**Variant — First Missing Positive:**
```csharp
// First Missing Positive - O(N) time, O(1) space
public int FirstMissingPositive(int[] nums) {
    int n = nums.Length;
    
    // Cyclic sort: place value i at index i-1
    for (int i = 0; i < n; i++) {
        while (nums[i] > 0 && nums[i] <= n && nums[nums[i] - 1] != nums[i]) {
            // Swap nums[i] to correct position
            int correctIdx = nums[i] - 1;
            Swap(nums, i, correctIdx);
        }
    }
    
    // Find first position where nums[i] != i+1
    for (int i = 0; i < n; i++) {
        if (nums[i] != i + 1) {
            return i + 1;
        }
    }
    
    return n + 1; // All 1..N present
}
```

---

### Pattern 4B: KADANE'S ALGORITHM — Greedy DP for Subarrays

**C# Mental Model:** Like `.NET Enumerable.Aggregate()` but tracking running max. Think "dropping negative baggage".

**When to Use:**
- ✅ Maximum subarray sum
- ✅ Maximum product subarray
- ✅ Best time to buy/sell stock
- ✅ Circular array constraints

**Core C# Skeleton — Maximum Subarray:**
```csharp
// Maximum Subarray - O(N) time, O(1) space
public int MaxSubArray(int[] nums) {
    int currentSum = 0;
    int maxSum = int.MinValue;
    
    for (int i = 0; i < nums.Length; i++) {
        // Decide: start new or extend
        currentSum = Math.Max(nums[i], currentSum + nums[i]);
        
        // Track global max
        maxSum = Math.Max(maxSum, currentSum);
    }
    
    return maxSum;
}
```

**C# Notes:**
- Initialize `maxSum` to `int.MinValue` to handle all-negative arrays.
- `Math.Max()` for two-way comparison; use `Math.Min()` for min tracking.

**Variant — Maximum Product Subarray (Min+Max tracking):**
```csharp
// Maximum Product Subarray - O(N) time, O(1) space
public int MaxProduct(int[] nums) {
    int maxProd = nums[0];
    int minProd = nums[0]; // Track min because negative * negative = positive
    int result = maxProd;
    
    for (int i = 1; i < nums.Length; i++) {
        // Negative number flips max <-> min
        if (nums[i] < 0) {
            (maxProd, minProd) = (minProd, maxProd); // Swap using tuple
        }
        
        // Update max/min
        maxProd = Math.Max(nums[i], maxProd * nums[i]);
        minProd = Math.Min(nums[i], minProd * nums[i]);
        
        // Track global max
        result = Math.Max(result, maxProd);
    }
    
    return result;
}
```

**Variant — Maximum Circular Subarray Sum:**
```csharp
// Maximum Circular Subarray - O(N) time, O(1) space
// Case 1: Max non-circular (standard Kadane)
// Case 2: Max circular = Total - Min (wrap-around is complement)
public int MaxSubarraySumCircular(int[] nums) {
    int maxKadane = KadaneMax(nums);
    
    // Negate to find min subarray
    for (int i = 0; i < nums.Length; i++) {
        nums[i] = -nums[i];
    }
    int minKadane = KadaneMax(nums);
    
    // Restore
    for (int i = 0; i < nums.Length; i++) {
        nums[i] = -nums[i];
    }
    
    int total = 0;
    foreach (int num in nums) {
        total += num;
    }
    
    // If all negatives, minKadane == total (no wrap possible)
    if (total == minKadane) {
        return maxKadane;
    }
    
    return Math.Max(maxKadane, total - minKadane);
}

private int KadaneMax(int[] nums) {
    int current = 0;
    int max = int.MinValue;
    foreach (int num in nums) {
        current = Math.Max(num, current + num);
        max = Math.Max(max, current);
    }
    return max;
}
```

**Variant — Best Time to Buy/Sell Stock (Single Transaction):**
```csharp
// Best Time to Buy/Sell - O(N) time, O(1) space
// Reframe: max profit = max(price[j] - price[i]) where j > i
// This is max subarray on deltas
public int MaxProfit(int[] prices) {
    int maxProfit = 0;
    int minPrice = prices[0];
    
    for (int i = 1; i < prices.Length; i++) {
        // Current profit if selling today
        int profit = prices[i] - minPrice;
        maxProfit = Math.Max(maxProfit, profit);
        
        // Update min price seen so far
        minPrice = Math.Min(minPrice, prices[i]);
    }
    
    return maxProfit;
}
```

---

### Pattern 5: FAST & SLOW POINTERS — Cycle Detection & Geometry

**C# Mental Model:** Like two `.NET Enumerable` iterators at different speeds. Think "tortoise and hare on a racetrack".

**When to Use:**
- ✅ Detect cycle in linked list
- ✅ Find cycle start
- ✅ Find middle of linked list
- ✅ Duplicate in array (array as linked list)
- ✅ Happy number (cycle in sequence)

**Core C# Skeleton — Cycle Detection (Floyd's):**
```csharp
// Linked List Cycle - O(N) time, O(1) space
public bool HasCycle(ListNode head) {
    ListNode slow = head, fast = head;
    
    while (fast != null && fast.next != null) {
        slow = slow.next;           // Move 1 step
        fast = fast.next.next;      // Move 2 steps
        
        if (slow == fast) {
            return true; // They met = cycle exists
        }
    }
    
    return false; // Fast reached end = no cycle
}

public class ListNode {
    public int val;
    public ListNode next;
}
```

**C# Notes:**
- Always check `fast.next` before `fast.next.next` to avoid NullReferenceException.
- Use `==` to compare references (node equality), not values.

**Variant — Find Cycle Start (Two-Phase):**
```csharp
// Linked List Cycle II - O(N) time, O(1) space
public ListNode DetectCycleStart(ListNode head) {
    ListNode slow = head, fast = head;
    
    // Phase 1: Detect cycle
    while (fast != null && fast.next != null) {
        slow = slow.next;
        fast = fast.next.next;
        
        if (slow == fast) {
            break; // Cycle found
        }
    }
    
    // No cycle
    if (fast == null || fast.next == null) {
        return null;
    }
    
    // Phase 2: Find start (reset slow to head, move both by 1)
    slow = head;
    while (slow != fast) {
        slow = slow.next;
        fast = fast.next; // Now move fast by 1 only
    }
    
    return slow; // They meet at cycle start
}
```

**Variant — Middle of Linked List:**
```csharp
// Middle of Linked List - O(N) time, O(1) space
public ListNode MiddleNode(ListNode head) {
    ListNode slow = head, fast = head;
    
    while (fast != null && fast.next != null) {
        slow = slow.next;       // 1 step
        fast = fast.next.next;  // 2 steps
    }
    
    return slow; // Slow is at middle (or before for even length)
}
```

**Variant — Happy Number (Cycle in Sequence):**
```csharp
// Happy Number - O(N) time, O(1) space
public bool IsHappy(int n) {
    int slow = n, fast = n;
    
    while (true) {
        slow = SumOfSquares(slow);      // 1 step
        fast = SumOfSquares(fast);
        fast = SumOfSquares(fast);      // 2 steps
        
        if (fast == 1) return true;     // Happy
        if (slow == fast) return false; // Unhappy cycle
    }
}

private int SumOfSquares(int n) {
    int sum = 0;
    while (n > 0) {
        int digit = n % 10;
        sum += digit * digit;
        n /= 10;
    }
    return sum;
}
```

**Variant — Find Duplicate in Array (Array as Linked List):**
```csharp
// Find Duplicate Number - O(N) time, O(1) space
// Treat array as linked list: index -> value -> next index
public int FindDuplicate(int[] nums) {
    int slow = nums[0], fast = nums[0];
    
    // Phase 1: Detect cycle (values form a linked list)
    do {
        slow = nums[slow];          // 1 step
        fast = nums[nums[fast]];    // 2 steps
    } while (slow != fast);
    
    // Phase 2: Find cycle start (the duplicate)
    slow = nums[0];
    while (slow != fast) {
        slow = nums[slow];
        fast = nums[fast];
    }
    
    return slow;
}
```

---

## 📊 PROGRESSIVE PROBLEM LADDER (WEEK 5)

### 🟢 Stage 1: Canonical Problems (Core Pattern Application)

| # | LeetCode | Difficulty | Pattern | C# Focus |
|---|----------|------------|---------|----------|
| 1 | #1 | 🟢 Easy | Hash Map | Dictionary key lookup |
| 2 | #242 | 🟢 Easy | Hash Map | Frequency counting |
| 3 | #496 | 🟡 Medium | Monotonic Stack | Stack.Peek() / Pop() |
| 4 | #739 | 🟡 Medium | Monotonic Stack | Index manipulation |
| 5 | #56 | 🟡 Medium | Merge Intervals | Array.Sort() with Comparator |
| 6 | #75 | 🟡 Medium | Partition (DNF) | Three-pointer in-place |
| 7 | #283 | 🟢 Easy | Partition | Write pointer tracking |
| 8 | #53 | 🟡 Medium | Kadane's | Math.Max() comparison |
| 9 | #141 | 🟢 Easy | Fast & Slow | ListNode reference equality |
| 10 | #876 | 🟢 Easy | Fast & Slow | Pointer increment patterns |

### 🟡 Stage 2: Variations with Constraints

| # | LeetCode | Difficulty | Pattern+Twist | C# Focus |
|---|----------|------------|---------------|----------|
| 1 | #49 | 🟡 Medium | Hash Map + Sorting | Array.Sort() + Dictionary grouping |
| 2 | #347 | 🟡 Medium | Hash Map + Heap | PriorityQueue in .NET 6+ |
| 3 | #84 | 🔴 Hard | Monotonic Stack | Area calculation with indexing |
| 4 | #42 | 🔴 Hard | Monotonic Stack | Two-pointer alternative (water trap) |
| 5 | #57 | 🟡 Medium | Intervals No Sort | Three-pass logic (before, merge, after) |
| 6 | #253 | 🟡 Medium | Intervals + Sweep | Event-based room counting |
| 7 | #268 | 🟢 Easy | Cyclic Sort | Index = Value mapping |
| 8 | #152 | 🟡 Medium | Kadane (Product) | Min+Max tracking with tuple swap |
| 9 | #142 | 🟡 Medium | Fast & Slow Phase 2 | Two-phase pointer meeting |
| 10 | #287 | 🟡 Medium | Fast & Slow + Array as List | Value as pointer interpretation |

### 🟠 Stage 3: Integration & Advanced

| # | LeetCode | Difficulty | Patterns | C# Focus |
|---|----------|------------|----------|----------|
| 1 | #3 | 🟡 Medium | Hash Map + Sliding Window | Dictionary + two pointers |
| 2 | #918 | 🔴 Hard | Kadane (Circular) | Two-pass Kadane comparison |
| 3 | #435 | 🟡 Medium | Intervals + Greedy | Sorting by end vs. start |
| 4 | #202 | 🟢 Easy | Fast & Slow + Number | Cycle in mathematical sequence |
| 5 | #234 | 🟡 Medium | Fast & Slow + Reverse | Palindrome with middle finding |
| 6 | #146 | 🔴 Hard | Hash Map + LinkedList | LRU Cache design |
| 7 | #239 | 🔴 Hard | Monotonic Deque | Sliding window max (variant) |
| 8 | #632 | 🔴 Hard | Merge + Heap | K-way merge with PriorityQueue |

---

## ⚠ WEEK 5 PITFALLS & C# GOTCHAS

| Pattern | Common Bug | C# Symptom | Quick Fix |
|---------|------------|------------|-----------|
| **Hash Map** | Accessing non-existent key | `KeyNotFoundException` | Use `ContainsKey()` or `TryGetValue()` |
| **Hash Map** | Modifying while iterating | `InvalidOperationException` | Create list of keys, then modify |
| **Monotonic Stack** | Not checking empty before `Peek()` | `InvalidOperationException` | Always `stack.Count > 0` |
| **Monotonic Stack** | Incrementing `mid` when swapping with `high` | Elements out of order | Only increment for swap with `low` |
| **Intervals** | Forgetting last merged interval | Off-by-one in output | Add `current` after loop |
| **Intervals** | Wrong sort order (by end instead of start) | Incorrect merge | Sort by `[0]` (start), not `[1]` (end) |
| **Partition (DNF)** | Incrementing `mid` after high swap | Infinite loop / wrong order | Leave `mid` as-is; retry comparison |
| **Cyclic Sort** | Not checking `nums[i] != nums[target]` | Infinite loop | Swap only if positions differ |
| **Kadane** | Not handling all-negative arrays | Wrong min value | Initialize `maxSum = int.MinValue` |
| **Kadane (Product)** | Forgetting min tracking | Missed large negatives | Track both `maxProd` and `minProd` |
| **Fast & Slow** | Not checking `fast.next` | `NullReferenceException` | Always check both `fast` and `fast.next` |
| **Fast & Slow** | Using `!=` instead of `==` | Infinite loop | Use `slow == fast` for cycle |
| **Array as LinkedList** | Using actual array indices | Wrong value mapping | Use `value` as index directly |
| **Dictionary** | Assuming all keys exist | `KeyNotFoundException` | Use `GetValueOrDefault()` or check first |
| **List Conversion** | Forgetting `ToArray()` | Type mismatch | Call `.ToArray()` for return |

**Week 5 Collection Guide:**
- ✅ `Dictionary<K, V>`: Frequency counting, complement lookup, memoization
- ✅ `HashSet<T>`: Membership testing, deduplication
- ✅ `Stack<T>`: Monotonic stack (only `Push`, `Pop`, `Peek`)
- ✅ `List<T>`: Dynamic arrays, building results before conversion
- ✅ `PriorityQueue<E, P>` (.NET 6+): Top K problems, heap operations
- ✅ `Array.Sort()` with Comparator: Custom sorting for intervals/events
- ✅ `Math.Max() / Math.Min()`: Tracking extremes (Kadane, comparisons)
- ✅ Tuple swapping `(a, b) = (b, a)`: Clean value exchange (e.g., max/min flip)

---

## ✅ WEEK 5 COMPLETION CHECKLIST

**Pattern Fluency:**
- [ ] Recall Hash Map skeleton (complement lookup)
- [ ] Recall Monotonic Stack skeleton (decreasing/increasing logic)
- [ ] Recall Merge Intervals skeleton (sort + sweep)
- [ ] Recall DNF skeleton (three pointers)
- [ ] Recall Cyclic Sort skeleton (place value at index)
- [ ] Recall Kadane skeleton (current vs. global max)
- [ ] Recall Fast & Slow skeleton (cycle detection)

**Problem Solving:**
- [ ] Solved 4-5 Stage 1 canonical problems
- [ ] Solved 4-5 Stage 2 variation problems
- [ ] 70%+ Stage 2 attempt rate (even if not perfect)

**C# Implementation:**
- [ ] Used correct collections for each pattern
- [ ] Handled edge cases (null, empty, single element)
- [ ] Avoided `KeyNotFoundException`, `NullReferenceException`, `InvalidOperationException`
- [ ] Used `Array.Sort()` with custom Comparators correctly
- [ ] Implemented in-place algorithms without `new` allocations (where required)

**Code Quality:**
- [ ] All skeletons compile without syntax errors
- [ ] All functions handle edge cases (validation at start)
- [ ] All return types match problem specification

**Ready for Week 6:** [ ] Yes / [ ] No (revisit patterns if No)

---

**End of Week 5 Extended C# Roadmap**  
*Transform pattern theory into muscle memory through deliberate C# coding practice.*

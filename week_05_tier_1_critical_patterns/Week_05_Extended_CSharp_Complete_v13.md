# 🗺️ Week_05_Extended_CSharp_Problem_Solving_Implementation — COMPLETE v13

**Version:** v1.0 HYBRID (Pattern Recognition + Production Implementation)  
**Week:** 5 – Tier 1 Critical Patterns: Hash, Monotonic Stack, Intervals, Partition & Kadane, Fast/Slow  
**Purpose:** Master high-frequency patterns covering large fraction of interview problem space  
**Target:** Transform Week 5 critical patterns into interview-ready C# coding skills  
**Prerequisites:** Week 5 instructional files + standard support files complete

---

## 📚 WEEK 5 OVERVIEW

**Primary Goal:** Master a set of high-frequency patterns (hash, monotonic stack, interval, cyclic sort, Kadane, fast/slow) that cover a large fraction of interview problem space.

**Why This Week Comes Here:** Builds directly on Week 4 patterns and prepares for tree/graph/DP complexity.

**Topics by Day:**
- **Day 1:** Hash Map / Hash Set Patterns (two-sum, frequency counting, membership)
- **Day 2:** Monotonic Stack (next greater/smaller, stock span, trapping rain, histogram)
- **Day 3:** Merge Operations & Interval Patterns (merge intervals, insert interval, scheduling)
- **Day 4:** Partition, Cyclic Sort & Kadane's Algorithm (Dutch flag, cyclic sort, max subarray)
- **Day 5:** Fast/Slow Pointers (cycle detection, cycle start, midpoint, happy number)

---

## SECTION 1️⃣: PATTERN RECOGNITION FRAMEWORK

### 🎯 Decision Tree — How to Identify & Choose Week 5 Patterns

Use this decision tree when you encounter a problem in Week 5:

| 🔍 Problem Phrases/Signals | 🎯 Pattern Name | ❓ Why This Pattern? | 💻 C# Collection | ⏱️ Time/Space |
|---|---|---|---|---|
| "Two numbers sum to target", "Complement", "Anagram", "Duplicate detection" | **Hash Map/Set Patterns** | O(1) lookup for complements; frequency counting | `Dictionary<K,V>`, `HashSet<T>` | O(n) / O(n) |
| "Next greater element", "Next smaller", "Stock span", "Daily temperatures" | **Monotonic Stack** | O(n) one-pass; stack stores indices in mono order | `Stack<int>` for indices | O(n) / O(n) |
| "Merge intervals", "Insert interval", "Scheduling", "Resource allocation" | **Interval Patterns** | Sort by start; greedy merge overlapping | `List<Interval>` | O(n log n) / O(n) |
| "Merge K sorted", "K-way merge", "Priority queue", "Median in stream" | **Heap-Based Merge** | O(N log k) vs O(N*k²) naive; heap tracks k minimums | `PriorityQueue<T>` | O(N log k) / O(k) |
| "0/1/2 sorting", "Dutch flag", "Partition array", "Missing number 1..n" | **Partition & Cyclic Sort** | Three-way partition O(n); cyclic sort finds position | `int[]` in-place | O(n) / O(1) |
| "Maximum subarray", "Max product subarray", "Circular variant" | **Kadane's Algorithm** | DP: track max_ending_here and max_so_far | `int` variables | O(n) / O(1) |
| "Detect cycle", "Find cycle start", "Happy number", "Midpoint finder" | **Fast/Slow Pointers** | Two pointers at 1x/2x speed; meet if cycle exists | Implicit pointers | O(n) / O(1) |

**HOW TO READ THIS:**
- See "[Signal X]" in a problem? IMMEDIATELY think "[Pattern Y]"
- Ask yourself: "Why does this pattern solve it?" → Internalize the reasoning
- Check recommended collection and complexity

---

## SECTION 2️⃣: ANTI-PATTERNS — WHEN NOT TO USE & WHAT FAILS

### ⚠️ Week 5 Common Mistakes & Correct Alternatives

Learn WHAT NOT TO DO and WHY:

| ❌ Wrong Approach | 💥 Why It Fails | ⚡ Symptom/Consequence | ✅ Correct Alternative |
|---|---|---|---|
| Hash lookup without checking key existence | `KeyNotFoundException` on missing key | Crash on first miss or unchecked logic | Always: `ContainsKey()` or `TryGetValue()` |
| Monotonic stack without proper index tracking | Comparing values instead of indices | Wrong "previous greater" answers | Store indices only; compare `nums[index]` |
| Merging intervals without sorting first | Overlaps missed; wrong merge | Wrong interval list or TLE | Sort by start position first (O(n log n)) |
| Kadane without resetting max_ending_here | Carries forward negative maximums | Wrong max subarray sum | Reset to current when `< 0` |
| Partition without three-pointer invariant | Unsorted array remains unsorted | Wrong 0/1/2 positions | Maintain invariant: [0..low]=0, [mid..high]=2 |
| Cyclic sort on non-1..n arrays | Out of bounds or infinite loop | `IndexOutOfRangeException` or hang | Only for missing/duplicates in 1..n range |
| Fast/slow pointers without checking null | `NullReferenceException` on .Next | Crash in cycle detection | Guard: `fast != null && fast.Next != null` |
| Interval merge with open/closed endpoints | Off-by-one boundary issues | Overlaps missed or incorrectly merged | Consistent: [start, end) or [start, end] |

**ANTI-PATTERN LESSON:**
Each pattern has assumptions. Violating them gives wrong answers.

---

## SECTION 3️⃣: PATTERN IMPLEMENTATIONS (Production-Grade)

### Pattern 1: Hash Map/Set Patterns

#### 🧠 Mental Model
Use dictionaries/sets for O(1) lookup. For two-sum: store first element, lookup complement (target - current). For frequency: count occurrences, find max/duplicates. For membership: fast existence checks.

#### ✅ When to Use This Pattern
- ✅ Two-sum/three-sum (lookup complements).
- ✅ Anagram detection (compare frequency maps).
- ✅ Top-K frequent elements (count occurrences).
- ✅ Duplicate detection (set membership).

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Hash Map/Set Patterns - Two Sum, Frequency, Membership
/// Time: O(n) | Space: O(n)
/// 
/// 🧠 MENTAL MODEL:
/// Two-Sum: store first, lookup complement (target - num).
/// Frequency: count occurrences for anagrams/duplicates.
/// Membership: set for O(1) existence checks.
/// </summary>
public class HashPatternsSolution
{
    /// <summary>
    /// Two Sum - Hash Map for O(1) Complement Lookup
    /// Time: O(n) | Space: O(n)
    /// </summary>
    public static int[] TwoSum(int[] nums, int target)
    {
        // STEP 1: Guard
        if (nums == null || nums.Length < 2) return new int[0];
        
        // STEP 2: Map to store value → index
        Dictionary<int, int> seen = new();
        
        // STEP 3: Scan array
        for (int i = 0; i < nums.Length; i++)
        {
            int complement = target - nums[i];
            
            // Check if complement exists
            if (seen.ContainsKey(complement))
            {
                return new int[] { seen[complement], i };
            }
            
            // Store current number for future lookups
            if (!seen.ContainsKey(nums[i]))
            {
                seen[nums[i]] = i;
            }
        }
        
        return new int[0];
    }
    
    /// <summary>
    /// Valid Anagram - Frequency Map Comparison
    /// Time: O(n) | Space: O(1) for lowercase English (26 chars)
    /// </summary>
    public static bool IsAnagram(string s, string t)
    {
        // STEP 1: Guard - different lengths can't be anagrams
        if (s.Length != t.Length) return false;
        
        // STEP 2: Frequency map for s
        Dictionary<char, int> freq = new();
        foreach (char c in s)
        {
            if (!freq.ContainsKey(c))
                freq[c] = 0;
            freq[c]++;
        }
        
        // STEP 3: Decrement frequencies for t
        foreach (char c in t)
        {
            if (!freq.ContainsKey(c))
                return false;  // Character in t not in s
            
            freq[c]--;
            if (freq[c] < 0)
                return false;  // More occurrences in t than s
        }
        
        // All frequencies should be 0
        return true;
    }
    
    /// <summary>
    /// Top K Frequent Elements - Frequency + Heap/Dictionary Ordering
    /// Time: O(n + k log n) | Space: O(n)
    /// </summary>
    public static int[] TopKFrequent(int[] nums, int k)
    {
        // STEP 1: Guard
        if (nums == null || nums.Length == 0 || k <= 0) return new int[0];
        
        // STEP 2: Count frequencies
        Dictionary<int, int> freq = new();
        foreach (int num in nums)
        {
            if (!freq.ContainsKey(num))
                freq[num] = 0;
            freq[num]++;
        }
        
        // STEP 3: Use min-heap of size k to track k most frequent
        // Min-heap so we can pop smallest frequency when heap exceeds k
        PriorityQueue<int, int> minHeap = new();  // (value, frequency)
        
        foreach (var entry in freq)
        {
            minHeap.Enqueue(entry.Key, entry.Value);
            
            // Keep only k elements; pop minimum frequency
            if (minHeap.Count > k)
            {
                minHeap.Dequeue();
            }
        }
        
        // STEP 4: Extract k elements from heap
        int[] result = new int[k];
        int idx = k - 1;
        while (minHeap.Count > 0)
        {
            result[idx--] = minHeap.Dequeue();
        }
        
        return result;
    }
    
    /// <summary>
    /// Group Anagrams - Hash Map with Sorted String as Key
    /// Time: O(n * k log k) where k = avg string length | Space: O(n*k)
    /// </summary>
    public static IList<IList<string>> GroupAnagrams(string[] strs)
    {
        // STEP 1: Guard
        if (strs == null || strs.Length == 0) return new List<IList<string>>();
        
        // STEP 2: Map sorted string → list of anagrams
        Dictionary<string, List<string>> groups = new();
        
        // STEP 3: For each string, use sorted form as key
        foreach (string str in strs)
        {
            // Sort characters to get canonical form
            char[] chars = str.ToCharArray();
            Array.Sort(chars);
            string sorted = new string(chars);
            
            // Add to group
            if (!groups.ContainsKey(sorted))
            {
                groups[sorted] = new List<string>();
            }
            groups[sorted].Add(str);
        }
        
        // Convert to result
        IList<IList<string>> result = new List<IList<string>>();
        foreach (var group in groups.Values)
        {
            result.Add(group);
        }
        
        return result;
    }
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Always check `ContainsKey()` before accessing. Use `TryGetValue()` for safety.
- 🟡 **PERFORMANCE:** Frequency counting O(n); heap operations O(k log n); overall O(n log k) for top-K.
- 🟢 **BEST PRACTICE:** Use `Dictionary<K,V>` for key-value; `HashSet<T>` for membership only.

---

### Pattern 2: Monotonic Stack

#### 🧠 Mental Model
Stack maintains indices in increasing/decreasing order of values. For next greater: pop stack when current > top, store result, push current. Stack shrinks and grows; each element pushed/popped once → O(n).

#### ✅ When to Use This Pattern
- ✅ Next greater/smaller element (single pass O(n)).
- ✅ Stock span (consecutive days with price ≤ today).
- ✅ Trapping rain water (calculate boundaries).
- ✅ Largest rectangle in histogram (find boundary indices).

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Monotonic Stack Pattern
/// Time: O(n) single pass | Space: O(n)
/// 
/// 🧠 MENTAL MODEL:
/// Stack maintains indices in mono order (increasing values).
/// Pop when current breaks monotonicity; store result.
/// Each element pushed/popped once → O(n) total.
/// </summary>
public class MonotonicStackSolution
{
    /// <summary>
    /// Next Greater Element I - Monotonic Decreasing Stack
    /// Time: O(n) | Space: O(n)
    /// </summary>
    public static int[] NextGreaterElement(int[] nums1, int[] nums2)
    {
        // STEP 1: Guard
        if (nums1 == null || nums1.Length == 0) return new int[0];
        
        // STEP 2: Map each number to its next greater
        Dictionary<int, int> nextGreater = new();
        Stack<int> stack = new();  // Stores indices in decreasing order of values
        
        // STEP 3: Process nums2 (the larger array with all elements)
        foreach (int num in nums2)
        {
            // Pop all smaller elements; current is their next greater
            while (stack.Count > 0 && nums2[stack.Peek()] < num)
            {
                int idx = stack.Pop();
                nextGreater[nums2[idx]] = num;
            }
            
            // Push current (waiting for greater element)
            stack.Push(Array.IndexOf(nums2, num));
        }
        
        // STEP 4: Build result for nums1
        int[] result = new int[nums1.Length];
        for (int i = 0; i < nums1.Length; i++)
        {
            result[i] = nextGreater.ContainsKey(nums1[i]) ? nextGreater[nums1[i]] : -1;
        }
        
        return result;
    }
    
    /// <summary>
    /// Largest Rectangle in Histogram - Monotonic Stack
    /// Time: O(n) | Space: O(n)
    /// 
    /// 🧠 MENTAL MODEL:
    /// Stack maintains indices in increasing height order.
    /// When height drops, pop and calculate rectangle widths.
    /// Rectangle height = popped element; width = from next-lower to current.
    /// </summary>
    public static int LargestRectangleArea(int[] heights)
    {
        // STEP 1: Guard
        if (heights == null || heights.Length == 0) return 0;
        
        // STEP 2: Initialize stack and result
        Stack<int> stack = new();  // Stores indices in increasing height order
        int maxArea = 0;
        
        // STEP 3: Process each bar
        for (int i = 0; i < heights.Length; i++)
        {
            // Pop bars taller than current (they can't extend to right)
            while (stack.Count > 0 && heights[stack.Peek()] > heights[i])
            {
                int h_idx = stack.Pop();
                int h = heights[h_idx];
                
                // Width: from element after next-lower-bar to current position
                int w = stack.Count > 0 ? i - stack.Peek() - 1 : i;
                
                maxArea = Math.Max(maxArea, h * w);
            }
            
            // Push current bar (waiting to see how far right it can extend)
            stack.Push(i);
        }
        
        // Process remaining bars in stack
        while (stack.Count > 0)
        {
            int h_idx = stack.Pop();
            int h = heights[h_idx];
            int w = stack.Count > 0 ? heights.Length - stack.Peek() - 1 : heights.Length;
            
            maxArea = Math.Max(maxArea, h * w);
        }
        
        return maxArea;
    }
    
    /// <summary>
    /// Trapping Rain Water - Monotonic Stack or Two-Pointer Pre-computation
    /// Time: O(n) | Space: O(n) or O(1)
    /// 
    /// 🧠 MENTAL MODEL:
    /// Water trapped at position i = min(maxLeft[i], maxRight[i]) - height[i]
    /// Use monotonic stack or pre-compute max left/right arrays.
    /// </summary>
    public static int Trap(int[] height)
    {
        // STEP 1: Guard
        if (height == null || height.Length < 3) return 0;
        
        // STEP 2: Pre-compute max height to left and right of each position
        int n = height.Length;
        int[] maxLeft = new int[n];
        int[] maxRight = new int[n];
        
        maxLeft[0] = height[0];
        for (int i = 1; i < n; i++)
        {
            maxLeft[i] = Math.Max(maxLeft[i - 1], height[i]);
        }
        
        maxRight[n - 1] = height[n - 1];
        for (int i = n - 2; i >= 0; i--)
        {
            maxRight[i] = Math.Max(maxRight[i + 1], height[i]);
        }
        
        // STEP 3: Calculate water at each position
        int water = 0;
        for (int i = 0; i < n; i++)
        {
            int waterLevel = Math.Min(maxLeft[i], maxRight[i]);
            water += waterLevel - height[i];
        }
        
        return water;
    }
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Stack stores INDICES, not values. Compare via `heights[stack.Peek()]` not direct value.
- 🟡 **PERFORMANCE:** Each element pushed/popped once → O(n) total despite nested loops.
- 🟢 **BEST PRACTICE:** Monotonic stack for "next greater/smaller" and rectangle problems. Prefer simplicity over premature optimization.

---

### Pattern 3: Interval Patterns

#### 🧠 Mental Model
Sort intervals by start time. Merge overlapping: if current.start ≤ last.end, overlap → merge. Otherwise add to result. O(n log n) due to sort.

#### ✅ When to Use This Pattern
- ✅ Merge intervals (scheduling, overlapping events).
- ✅ Insert interval (add new event, merge with overlapping).
- ✅ Meeting rooms (can attend all meetings).
- ✅ Calendar scheduling.

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Interval Definition
/// </summary>
public class Interval
{
    public int start;
    public int end;
    public Interval(int start = 0, int end = 0)
    {
        this.start = start;
        this.end = end;
    }
}

/// <summary>
/// Interval Patterns - Merge, Insert, Meeting Rooms
/// Time: O(n log n) | Space: O(n)
/// 
/// 🧠 MENTAL MODEL:
/// Sort by start time. Merge overlapping greedily.
/// Current overlaps if start ≤ prev.end → merge.
/// Complexity dominated by sort.
/// </summary>
public class IntervalSolution
{
    /// <summary>
    /// Merge Intervals
    /// Time: O(n log n) | Space: O(1) if not counting output
    /// </summary>
    public static int[][] Merge(int[][] intervals)
    {
        // STEP 1: Guard
        if (intervals == null || intervals.Length == 0) return new int[0][];
        
        // STEP 2: Sort by start time
        Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
        
        // STEP 3: Merge overlapping intervals
        List<int[]> result = new List<int[]>();
        int[] current = intervals[0];
        
        for (int i = 1; i < intervals.Length; i++)
        {
            // Overlapping: current.start ≤ current.end < next.start
            if (intervals[i][0] <= current[1])
            {
                // Merge: extend end to max
                current[1] = Math.Max(current[1], intervals[i][1]);
            }
            else
            {
                // No overlap: add merged interval to result
                result.Add(current);
                current = intervals[i];
            }
        }
        
        // Add final interval
        result.Add(current);
        
        return result.ToArray();
    }
    
    /// <summary>
    /// Insert Interval - Insert new interval and merge with overlapping
    /// Time: O(n) | Space: O(n)
    /// </summary>
    public static int[][] Insert(int[][] intervals, int[] newInterval)
    {
        // STEP 1: Guard
        if (intervals == null) return new int[][] { newInterval };
        
        List<int[]> result = new List<int[]>();
        int newStart = newInterval[0];
        int newEnd = newInterval[1];
        
        // STEP 2: Add all intervals before new interval (no overlap)
        int i = 0;
        while (i < intervals.Length && intervals[i][1] < newStart)
        {
            result.Add(intervals[i]);
            i++;
        }
        
        // STEP 3: Merge all overlapping intervals
        while (i < intervals.Length && intervals[i][0] <= newEnd)
        {
            newStart = Math.Min(newStart, intervals[i][0]);
            newEnd = Math.Max(newEnd, intervals[i][1]);
            i++;
        }
        result.Add(new int[] { newStart, newEnd });
        
        // STEP 4: Add remaining intervals (after new interval)
        while (i < intervals.Length)
        {
            result.Add(intervals[i]);
            i++;
        }
        
        return result.ToArray();
    }
    
    /// <summary>
    /// Meeting Rooms II - Minimum Meeting Rooms Needed
    /// Time: O(n log n) | Space: O(n)
    /// 
    /// 🧠 MENTAL MODEL:
    /// Sort start/end times. Use two-pointer sweep line.
    /// Track concurrent meetings at any time.
    /// </summary>
    public static int MinMeetingRooms(int[][] intervals)
    {
        // STEP 1: Guard
        if (intervals == null || intervals.Length == 0) return 0;
        
        // STEP 2: Separate and sort start/end times
        int[] starts = new int[intervals.Length];
        int[] ends = new int[intervals.Length];
        
        for (int i = 0; i < intervals.Length; i++)
        {
            starts[i] = intervals[i][0];
            ends[i] = intervals[i][1];
        }
        
        Array.Sort(starts);
        Array.Sort(ends);
        
        // STEP 3: Two-pointer sweep line
        int rooms = 0;
        int maxRooms = 0;
        int i_start = 0;
        int i_end = 0;
        
        while (i_start < starts.Length)
        {
            if (starts[i_start] < ends[i_end])
            {
                // Meeting starts, need room
                rooms++;
                i_start++;
            }
            else
            {
                // Meeting ends, free up room
                rooms--;
                i_end++;
            }
            
            maxRooms = Math.Max(maxRooms, rooms);
        }
        
        return maxRooms;
    }
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Sort by start time first. End time secondary for consistency.
- 🟡 **PERFORMANCE:** O(n log n) dominated by sort; merge is O(n).
- 🟢 **BEST PRACTICE:** Two-pointer technique for sweep-line problems beats binary search for interval queries.

---

### Pattern 4: Kadane's Algorithm (Maximum Subarray)

#### 🧠 Mental Model
Track `max_ending_here` (best sum ending at current position) and `max_so_far` (overall best). For each element: max_ending_here = max(current, max_ending_here + current). Update max_so_far. Reset when negative.

#### ✅ When to Use This Pattern
- ✅ Maximum subarray sum (Kadane classic).
- ✅ Maximum product subarray (track min too for negatives).
- ✅ Circular maximum (subtract minimum subarray from total).
- ✅ Financial analysis (max gain over holding period).

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Kadane's Algorithm Pattern - Maximum Subarray Problems
/// Time: O(n) | Space: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// max_ending_here = max(current, max_ending_here + current)
/// At each position, decide: start new subarray or extend existing.
/// Track max_so_far separately.
/// </summary>
public class KadaneSolution
{
    /// <summary>
    /// Maximum Subarray - Classic Kadane's Algorithm
    /// Time: O(n) | Space: O(1)
    /// </summary>
    public static int MaxSubArray(int[] nums)
    {
        // STEP 1: Guard
        if (nums == null || nums.Length == 0) return 0;
        
        // STEP 2: Initialize state
        int max_ending_here = nums[0];
        int max_so_far = nums[0];
        
        // STEP 3: Kadane's DP
        for (int i = 1; i < nums.Length; i++)
        {
            // Decision: extend previous or start new
            max_ending_here = Math.Max(nums[i], max_ending_here + nums[i]);
            
            // Update global maximum
            max_so_far = Math.Max(max_so_far, max_ending_here);
        }
        
        return max_so_far;
    }
    
    /// <summary>
    /// Maximum Product Subarray - Track Min Too (for negatives)
    /// Time: O(n) | Space: O(1)
    /// 
    /// 🧠 MENTAL MODEL:
    /// Max product could come from:
    /// 1. Current number alone
    /// 2. Product of previous max × current
    /// 3. Product of previous min × current (if current negative, min becomes max)
    /// Track both max and min; swap when multiplying by negative.
    /// </summary>
    public static int MaxProduct(int[] nums)
    {
        // STEP 1: Guard
        if (nums == null || nums.Length == 0) return 0;
        
        // STEP 2: Initialize
        int max_prod = nums[0];
        int min_prod = nums[0];
        int result = nums[0];
        
        // STEP 3: DP with min/max tracking
        for (int i = 1; i < nums.Length; i++)
        {
            // Current element might flip min/max (negative multiplies min to max)
            int curr_max = Math.Max(nums[i], 
                Math.Max(max_prod * nums[i], min_prod * nums[i]));
            int curr_min = Math.Min(nums[i], 
                Math.Min(max_prod * nums[i], min_prod * nums[i]));
            
            max_prod = curr_max;
            min_prod = curr_min;
            
            result = Math.Max(result, max_prod);
        }
        
        return result;
    }
    
    /// <summary>
    /// Maximum Subarray Sum in Circular Array
    /// Time: O(n) | Space: O(1)
    /// 
    /// 🧠 MENTAL MODEL:
    /// Circular max = max(linear Kadane, total - minimum_subarray)
    /// If all negative, return linear max (no wrap).
    /// Otherwise, circular wrap (total - min_subarray) might be better.
    /// </summary>
    public static int MaxSubarraySumCircular(int[] nums)
    {
        // STEP 1: Guard
        if (nums == null || nums.Length == 0) return 0;
        
        // STEP 2: Kadane's for linear max
        int max_kadane = KadaneMax(nums);
        
        // STEP 3: Calculate total and find minimum subarray
        int total = 0;
        for (int i = 0; i < nums.Length; i++)
        {
            total += nums[i];
            nums[i] = -nums[i];  // Negate to find min subarray
        }
        
        // STEP 4: Kadane's on negated array (finds min subarray)
        int min_kadane = KadaneMax(nums);
        
        // Restore array
        for (int i = 0; i < nums.Length; i++)
        {
            nums[i] = -nums[i];
        }
        
        // STEP 5: Return max of linear or circular
        int circular_max = total + min_kadane;  // total - min_subarray
        
        return circular_max == 0 ? max_kadane : Math.Max(max_kadane, circular_max);
    }
    
    private static int KadaneMax(int[] nums)
    {
        int max_ending_here = nums[0];
        int max_so_far = nums[0];
        
        for (int i = 1; i < nums.Length; i++)
        {
            max_ending_here = Math.Max(nums[i], max_ending_here + nums[i]);
            max_so_far = Math.Max(max_so_far, max_ending_here);
        }
        
        return max_so_far;
    }
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** max_ending_here = Math.Max(current, prev_sum + current). Not just max_so_far + current.
- 🟡 **PERFORMANCE:** O(n) single pass; state is O(1).
- 🟢 **BEST PRACTICE:** Track both max and min for product subarray (negatives flip signs).

---

### Pattern 5: Fast/Slow Pointers (Cycle Detection)

#### 🧠 Mental Model
Two pointers on linked list: slow moves 1 step, fast moves 2 steps. If cycle exists, they meet. Use pointer reset trick to find cycle start: when they meet, reset one to head, move both 1 step until they meet again (at cycle start).

#### ✅ When to Use This Pattern
- ✅ Detect cycle in linked list (Floyd's algorithm).
- ✅ Find cycle entry point.
- ✅ Happy number (cycle detection in sequence).
- ✅ Linked list midpoint.

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// Fast/Slow Pointer Pattern - Cycle Detection & Midpoint
/// Time: O(n) single pass | Space: O(1)
/// 
/// 🧠 MENTAL MODEL:
/// Two runners: slow 1x, fast 2x. Meet if cycle.
/// Gap closes by 1 each iteration → must meet if cycle.
/// Reset trick: distance from head to cycle start = distance from meet to cycle start.
/// </summary>
public class FastSlowSolution
{
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }
    
    /// <summary>
    /// Detect Cycle in Linked List - Floyd's Algorithm
    /// Time: O(n) | Space: O(1)
    /// </summary>
    public static bool HasCycle(ListNode head)
    {
        // STEP 1: Guard
        if (head == null || head.next == null) return false;
        
        // STEP 2: Initialize pointers
        ListNode slow = head;
        ListNode fast = head;
        
        // STEP 3: Move pointers; if meet, cycle exists
        while (fast != null && fast.next != null)
        {
            slow = slow.next;         // Move 1 step
            fast = fast.next.next;    // Move 2 steps
            
            if (slow == fast) return true;
        }
        
        return false;
    }
    
    /// <summary>
    /// Find Cycle Start Point - Pointer Reset Trick
    /// Time: O(n) | Space: O(1)
    /// </summary>
    public static ListNode DetectCycleStart(ListNode head)
    {
        // STEP 1: Guard
        if (head == null || head.next == null) return null;
        
        // STEP 2: Detect cycle and get meeting point
        ListNode slow = head;
        ListNode fast = head;
        
        while (fast != null && fast.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
            
            if (slow == fast) break;  // Cycle detected
        }
        
        // STEP 3: Guard - no cycle
        if (fast == null || fast.next == null) return null;
        
        // STEP 4: Reset trick - find cycle start
        // Distance from head to cycle start = distance from meet point to cycle start
        ListNode ptr1 = head;
        ListNode ptr2 = slow;  // Meeting point
        
        while (ptr1 != ptr2)
        {
            ptr1 = ptr1.next;
            ptr2 = ptr2.next;
        }
        
        return ptr1;
    }
    
    /// <summary>
    /// Find Middle Node of Linked List
    /// Time: O(n) single pass | Space: O(1)
    /// </summary>
    public static ListNode FindMiddle(ListNode head)
    {
        // STEP 1: Guard
        if (head == null || head.next == null) return head;
        
        // STEP 2: Initialize pointers
        ListNode slow = head;
        ListNode fast = head;
        
        // STEP 3: Move to middle
        // When fast reaches end, slow is at middle
        while (fast != null && fast.next != null)
        {
            slow = slow.next;
            fast = fast.next.next;
        }
        
        return slow;
    }
    
    /// <summary>
    /// Happy Number - Cycle Detection in Sequence
    /// Time: O(log n) | Space: O(1) with cycle detection
    /// 
    /// 🧠 MENTAL MODEL:
    /// Repeatedly sum squares of digits.
    /// If reaches 1: happy. If cycle: not happy.
    /// Use fast/slow to detect cycle.
    /// </summary>
    public static bool IsHappy(int n)
    {
        // STEP 1: Helper - sum of squares of digits
        int GetNext(int num)
        {
            int sum = 0;
            while (num > 0)
            {
                int digit = num % 10;
                sum += digit * digit;
                num /= 10;
            }
            return sum;
        }
        
        // STEP 2: Fast/slow to detect cycle
        int slow = n;
        int fast = n;
        
        while (fast != 1 && GetNext(fast) != 1)
        {
            slow = GetNext(slow);
            fast = GetNext(GetNext(fast));
            
            if (slow == fast)
                return false;  // Cycle detected - not happy
        }
        
        return true;
    }
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** Guard `fast.Next` before `fast.Next.Next`. Null check prevents crash.
- 🟡 **PERFORMANCE:** O(n) single pass; pointer reset doesn't revisit (O(n) total, not O(n²)).
- 🟢 **BEST PRACTICE:** Use fast/slow for O(1) space cycle detection. Avoid HashSet approach (O(n) space).

---

## SECTION 4️⃣: C# Collection Decision GUIDE

### When to Use Each Collection for Week 5 Patterns

| Use Case | Collection | Why? | When NOT to Use | Alternative |
|---|---|---|---|---|
| Two-sum complement lookup | `Dictionary<int, int>` | O(1) lookup for value → index | Not storing index (use HashSet) | `HashSet<int>` if membership only |
| Frequency counting | `Dictionary<K, int>` | O(1) increment; count occurrences | Small fixed range (use array) | `int[26]` for lowercase letters |
| Monotonic stack | `Stack<int>` (indices) | LIFO order; O(1) push/pop | Storing values directly (store indices) | Store indices; compare via array |
| Interval operations | `List<int[]>` sorted | O(1) access; sort once O(n log n) | Unsorted (sort first) | Sort then iterate |
| Kadane tracking | `int` variables | State = (max_ending, max_so_far) | Need history (use array) | `int[]` if tracking all subarrays |
| Cycle detection | Implicit pointers | O(1) space; no extra allocation | Need to track visited (fast/slow beats it) | HashSet if must know all visited |

**KEY INSIGHT:**
Week 5 patterns are mostly pointer/algorithm-centric. Collections are secondary.

---

## SECTION 5️⃣: PROGRESSIVE PROBLEM LADDER

### 🎯 Strategy: Solve Problems in Stages

---

### 🟢 STAGE 1: CANONICAL PROBLEMS — Master Core Pattern

| # | LeetCode # | Difficulty | Pattern | C# Focus | Concept |
|---|---|---|---|---|---|
| 1 | #1 | 🟢 Easy | Two Sum | Dictionary complement lookup | O(1) space beats sort |
| 2 | #242 | 🟢 Easy | Valid Anagram | Frequency map comparison | Character count matching |
| 3 | #496 | 🟢 Easy | Next Greater Element | Monotonic stack | Single pass O(n) |
| 4 | #56 | 🟢 Easy | Merge Intervals | Sort + greedy merge | Overlapping scheduling |
| 5 | #53 | 🟢 Easy | Maximum Subarray | Kadane's Algorithm | DP state tracking |
| 6 | #141 | 🟢 Easy | Linked List Cycle | Fast/slow pointers | O(1) space detection |
| 7 | #202 | 🟢 Easy | Happy Number | Cycle in sequence | Fast/slow on integers |

**STAGE 1 GOAL:** Pattern fluency. Can you implement each skeleton in < 5 minutes from memory?

---

### 🟡 STAGE 2: VARIATIONS — Recognize Pattern Boundaries

| # | LeetCode # | Difficulty | Pattern + Twist | C# Focus | When Pattern Breaks |
|---|---|---|---|---|---|
| 1 | #347 | 🟡 Medium | Top K Frequent | Heap-based with freq map | Tie-breaking with second criterion |
| 2 | #49 | 🟡 Medium | Group Anagrams | Sorted-string as key | Anagram equivalence class |
| 3 | #84 | 🟡 Medium | Largest Rectangle | Monotonic decreasing stack | Width calculation subtlety |
| 4 | #42 | 🟡 Medium | Trapping Rain Water | Monotonic stack alt | Pre-compute vs stack |
| 5 | #435 | 🟡 Medium | Non-overlapping Intervals | Interval merging greedy | Greedy on endpoint matters |
| 6 | #152 | 🟡 Medium | Maximum Product Subarray | Track min too | Negatives flip min→max |
| 7 | #142 | 🟡 Medium | Linked List Cycle II | Pointer reset trick | Distance = head to start |

**STAGE 2 GOAL:** Pattern boundaries. When do variations apply?

---

### 🟠 STAGE 3: INTEGRATION — Combine Patterns

| # | LeetCode # | Difficulty | Patterns Required | C# Focus | Pattern Combination Logic |
|---|---|---|---|---|---|
| 1 | #23 | 🔴 Hard | Merge K Sorted Lists + Heap | Priority queue for min | K-way merge with O(N log k) |
| 2 | #295 | 🔴 Hard | Median Finder + Two Heaps | Max + Min heap balance | Median stream with insertions |
| 3 | #1425 | 🔴 Hard | Constrained Subsequence + Deque | Deque + DP | Monotonic deque for DP optimization |

**STAGE 3 GOAL:** Real-world thinking. Professional problems combine multiple patterns.

---

## SECTION 6️⃣: WEEK 5 PITFALLS & C# GOTCHAS

### 🐛 Runtime Issues & Collection Pitfalls

| ❌ Pattern | 🐛 Bug | 💥 C# Symptom | 🔧 Quick Fix |
|---|---|---|---|
| Hash Lookup | Accessing without checking key | `KeyNotFoundException` | Always: `ContainsKey()` or `TryGetValue()` |
| Monotonic Stack | Storing values instead of indices | Comparing wrong values | Store indices only; compare `array[index]` |
| Interval Merge | Not sorting intervals first | Overlaps missed; wrong output | Sort by start: `Array.Sort(intervals, (a,b) => a[0].CompareTo(b[0]))` |
| Kadane | Resetting max_ending_here to 0 | Wrong maximum subarray | Reset to current element: `max(nums[i], prev + nums[i])` |
| Cycle Detection | Null check missing | `NullReferenceException` | Guard: `fast != null && fast.Next != null` |
| Fast/Slow Pointers | Using `ptr1 = fast.next.next` | Skip elements; wrong midpoint | Use: `slow = slow.Next; fast = fast.Next.Next;` |

### 🎯 Week 5 Collection Gotchas

- ❌ Dictionary without null check → `KeyNotFoundException` → Use `ContainsKey()` first
- ❌ Monotonic stack on values → Wrong "greater" answers → Store indices; compare via array
- ❌ Interval sort without comparator → Wrong start order → Use `Array.Sort(intervals, (a,b) => a[0].CompareTo(b[0]))`
- ❌ Kadane with `max_ending = 0` reset → Misses negative subarrays → Use `max(current, sum + current)`
- ❌ `fast.Next.Next` without guard → `NullReferenceException` → Check both `!= null` before chaining

---

## SECTION 7️⃣: QUICK REFERENCE — INTERVIEW PREPARATION

### Mental Models for Fast Recall (30 seconds before interview!)

| Pattern | 1-Liner Mental Model | Code Symbol | When You See This... |
|---|---|---|---|
| **Hash Map/Set** | "O(1) lookup for complements; frequency counting" | `dict[complement] = i; freq[char]++` | "Two sum", "Anagram", "Top-K", "Duplicates" |
| **Monotonic Stack** | "Stack in mono order; pop when breaks; O(n) total" | `while (stack.Count > 0 && array[stack.Peek()] < current)` | "Next greater", "Histogram", "Stock span" |
| **Interval Merge** | "Sort by start; greedy merge overlapping" | `if (current.start ≤ prev.end) merge; else add new` | "Merge intervals", "Scheduling", "Calendar" |
| **Kadane** | "Track max_ending_here; decide extend or restart" | `max_ending = Math.Max(current, sum + current)` | "Max subarray", "Max product", "Circular" |
| **Fast/Slow Pointer** | "Two speeds; meet if cycle; reset for start" | `slow = slow.Next; fast = fast.Next.Next;` | "Cycle", "Midpoint", "Happy number" |

---

## ✅ WEEK 5 COMPLETION CHECKLIST

### Pattern Fluency — Can You Recognize & Choose?

- [ ] Recognize Hash Map signals ("two sum", "complement", "frequency", "anagram")
- [ ] Recall hash solutions without notes (< 2 min)
- [ ] Explain WHY O(1) lookup beats sort (O(n) vs O(n log n))
- [ ] Explain WHEN to use frequency vs membership

- [ ] Recognize Monotonic Stack signals ("next greater", "histogram", "span")
- [ ] Recall stack pattern without notes (indices, comparisons)
- [ ] Explain WHY each element O(1) amortized (pushed/popped once)
- [ ] Explain WHEN to use decreasing vs increasing stack

- [ ] Recognize Interval signals ("merge", "scheduling", "overlapping")
- [ ] Recall interval merge pattern without notes (sort, greedy)
- [ ] Explain WHY sort by start matters
- [ ] Explain WHEN to check <= vs < for overlapping

- [ ] Recognize Kadane signals ("max subarray", "product", "circular")
- [ ] Recall Kadane decision logic without notes
- [ ] Explain WHY max(current, sum + current) vs reset
- [ ] Explain WHEN to track min (for products)

- [ ] Recognize Fast/Slow signals ("cycle", "midpoint", "happy", "start point")
- [ ] Recall fast/slow skeleton without notes
- [ ] Explain WHY they must meet if cycle (gap closes by 1)
- [ ] Explain WHEN pointer reset finds cycle start

### Problem Solving — Can You Practice?

- [ ] Solved ALL Stage 1 canonical problems (7 problems) ✓
- [ ] Solved 80%+ Stage 2 variations (6+ of 7 problems)
- [ ] Solved 50%+ Stage 3 integration problems

### Production Code Quality — Can You Code?

- [ ] Used guard clauses (null checks, empty collections)
- [ ] Added mental model comments
- [ ] Chose correct pattern (hash vs stack vs interval vs Kadane vs pointers)
- [ ] Handled edge cases explicitly
- [ ] Code passes code review

### Interview Ready — Can You Communicate?

- [ ] Can solve Stage 1 problem in < 5 minutes
- [ ] Can EXPLAIN your pattern choice to interviewer
- [ ] Can write PRODUCTION-GRADE code
- [ ] Can discuss tradeoffs (time/space, patterns, variants)

---

### 🎯 Week 5 Mastery Status

- [ ] **YES - I've mastered Week 5. Ready for Week 6+.**
- [ ] **NO - Need to practice more. Focus on weak patterns.**

---

## 📚 REFERENCE MATERIALS

This file is self-contained. You have:
- **Decision framework** (SECTION 1) — Recognize which pattern
- **Anti-patterns knowledge** (SECTION 2) — What NOT to do
- **Production-grade code** (SECTION 3) — 5 complete pattern implementations
- **Collection guidance** (SECTION 4) — Which collection when
- **Progressive practice** (SECTION 5) — Easy to hard problems
- **Real gotchas** (SECTION 6) — Bugs you'll encounter
- **Quick interview reference** (SECTION 7) — 30-second recall

---

## 🚀 HOW TO USE THIS FILE

### For Learning:
1. Start with **SECTION 1** → Understand the decision tree
2. Review **SECTION 2** → Learn what NOT to do
3. Study **SECTION 3** → Understand production implementations
4. Follow **SECTION 5** → Solve problems progressively

### For Reference:
1. See a problem? → Check **SECTION 1** (decision tree)
2. Stuck? → Check **SECTION 6** (gotchas)
3. Need code? → Check **SECTION 3** (implementations)
4. Before interview? → Check **SECTION 7** (quick recall)

---

## 📊 COMPLEXITY REFERENCE

| Pattern | Time | Space | Notes |
|---------|------|-------|-------|
| Hash Map/Set | O(n) | O(n) | Expected; depends on hash function |
| Monotonic Stack | O(n) | O(n) | Each element pushed/popped once |
| Interval Merge | O(n log n) | O(n) | Dominated by sort |
| Kadane | O(n) | O(1) | State variables only |
| Fast/Slow Pointers | O(n) | O(1) | Single pass, no extra space |
| Cycle Detection | O(n) | O(1) | Floyd's algorithm; no HashSet |

---

*End of Week 5 Extended C# Support — v13 Hybrid Format COMPLETE*

---

**Status:** ✅ WEEK 5 PRODUCTION READY & COMPREHENSIVE

This file combines:
- ✅ **Pattern selection guidance** (v11 strength) — Know WHEN/WHY to choose
- ✅ **Production-grade code** (v12 strength) — Know HOW to implement
- ✅ **Anti-patterns & gotchas** (v11 strength) — Know what to AVOID  
- ✅ **Progressive learning** (v11 strength) — Practice from easy to hard
- ✅ **Interview readiness** (v13 integration) — Pass technical interviews
- ✅ **Complete coverage** (WEEK 5 TOPICS) — All Tier 1 critical patterns


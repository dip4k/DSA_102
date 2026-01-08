# 🚀 WEEK 05 PROBLEM-SOLVING ROADMAP: EXTENDED C# SUPPORT

**Week:** 5 | **Tier:** Tier 1 Critical Patterns  
**Goal:** Transform pattern knowledge into production-grade C# coding fluency  
**Focus:** Problem-solving mechanics, collection selection, common pitfalls

---

## 📋 PATTERN RECOGNITION & DECISION TREE

### Problem Phrase → Pattern Mapping

| Problem Signals | Primary Pattern | C# Collection | Time | Space |
|---|---|---|---|---|
| "Find pair summing to X", "Two elements equal target" | **Hash Complement** | `HashSet<T>`, `Dictionary<K,V>` | O(n) | O(n) |
| "Count frequencies", "Group by property", "Anagrams" | **Hash Frequency** | `Dictionary<K,int>` | O(n) | O(n) |
| "Next greater/smaller element", "Nearest X" | **Monotonic Stack** | `Stack<T>` | O(n) | O(n) |
| "Trapping water", "Largest rectangle" | **Stack + Area** | `Stack<T>` | O(n) | O(n) |
| "Merge intervals", "Scheduling", "Non-overlapping" | **Intervals** | `List<int[]>`, sort | O(n log n) | O(1) |
| "Segregate 0/1/2", "Partition array" | **Dutch Flag** | Array only | O(n) | O(1) |
| "Move X to end", "Rearrange in-place" | **Two-Pointer** | Array only | O(n) | O(1) |
| "Cyclic sort", "1..n position" | **Cyclic Sort** | Array only | O(n) | O(1) |
| "Max subarray sum", "Best window" | **Kadane's DP** | Scalar tracking | O(n) | O(1) |
| "Cycle in linked list", "Midpoint" | **Fast/Slow** | Pointers | O(n) | O(1) |

### Anti-Patterns to Avoid

| Week 5 Mistake | Why It's Wrong | Use This Instead |
|---|---|---|
| Using `List<T>` for O(1) lookup | O(n) complexity, defeats purpose | `HashSet<T>` or `Dictionary<K,V>` |
| Sorting without understanding direction | Wrong merge result or missed optimization | Check: Sort by start or end? |
| Forgetting to reset pointer after swap | Leaves elements in wrong region | Remember: mid doesn't increment after right swap |
| Tracking only local max in Kadane's | Miss true maximum in middle | Always update global inside loop |
| Not checking `fast.next` before dereferencing | NullReferenceException | Always: `while (fast != null && fast.next != null)` |
| Using extra space for partitioning | O(n) space defeats in-place goal | Partition with 2-3 pointers only |

---

## 🎯 WEEK 05 PROBLEM-SOLVING FRAMEWORK

### DAY 1: HASH PATTERNS

**C# Mental Model:** Hash as "instant lookup by property"  
**Common Analogy:** Dictionary lookup (name → phone number)

**When to Use:**
- Finding complements in array (two-sum)
- Counting frequencies (word frequency, top-k)
- Membership testing ("is X in set?")

**Core C# Skeleton Pattern 1: Two-Sum Complement**

```csharp
public int[] TwoSum(int[] nums, int target)
{
    // MENTAL MODEL: Store complements as we scan
    
    // 1. Guard Clauses: Check input validity
    if (nums == null || nums.Length < 2)
        return new int[0];
    
    // 2. State Initialization: Seen values with indices
    var seen = new Dictionary<int, int>();
    // Why Dictionary? Need both value AND index for result
    
    // 3. Core Logic: Single pass, O(1) lookup
    for (int i = 0; i < nums.Length; i++)
    {
        // Step 1: Calculate complement needed
        int complement = target - nums[i];
        
        // Step 2: Check if complement was already seen
        if (seen.ContainsKey(complement))
        {
            // Found it! Return indices immediately
            return new int[] { seen[complement], i };
        }
        
        // Step 3: Store current value for future lookups
        if (!seen.ContainsKey(nums[i]))
            seen[nums[i]] = i;
    }
    
    // No pair found
    return new int[0];
}
```

**C# Notes:**
- Use `Dictionary<int, int>` to store value→index mapping
- `ContainsKey` is O(1) on average; crucial for performance
- Early return when pair found beats searching full array

---

**Core C# Skeleton Pattern 2: Frequency Counting**

```csharp
public IList<string> GroupAnagrams(string[] strs)
{
    // MENTAL MODEL: Canonical form (sorted chars) as key
    
    // 1. Guard Clauses
    if (strs == null || strs.Length == 0)
        return new List<string>();
    
    // 2. State Initialization: Groups by canonical key
    var anagramGroups = new Dictionary<string, List<string>>();
    // Why Dictionary? Fast O(1) lookup by sorted string
    
    // 3. Core Logic
    foreach (var word in strs)
    {
        // Step 1: Get canonical form (sorted characters)
        char[] chars = word.ToCharArray();
        Array.Sort(chars);
        string canonical = new string(chars);
        
        // Step 2: Add word to group with this canonical key
        if (!anagramGroups.ContainsKey(canonical))
            anagramGroups[canonical] = new List<string>();
        
        anagramGroups[canonical].Add(word);
    }
    
    // Return all groups as IList
    return new List<List<string>>(anagramGroups.Values);
}
```

**C# Notes:**
- `string.ToCharArray()` → `Array.Sort()` → reconstruct pattern
- Dictionary key lookup is O(1); crucial for grouping efficiency
- `anagramGroups.Values` gives all groups in one collection

---

### DAY 2: MONOTONIC STACK PATTERNS

**C# Mental Model:** Stack maintains order invariant throughout  
**Common Analogy:** Tower of building blocks (always increasing/decreasing)

**When to Use:**
- Finding next greater/smaller element
- Trapping rain water (heights problem)
- Largest rectangle in histogram

**Core C# Skeleton Pattern 1: Next Greater Element**

```csharp
public int[] NextGreaterElement(int[] nums)
{
    // MENTAL MODEL: Decreasing stack; pop when current > top
    
    // 1. Guard Clauses
    if (nums == null || nums.Length == 0)
        return new int[0];
    
    // 2. State Initialization
    var stack = new Stack<int>();
    var result = new int[nums.Length];
    Array.Fill(result, -1);  // Default: no greater element
    
    // Why Stack<int>? Not List - need O(1) pop/push
    
    // 3. Core Logic: Traverse right to left
    for (int i = nums.Length - 1; i >= 0; i--)
    {
        // Step 1: Pop elements that are smaller than current
        while (stack.Count > 0 && stack.Peek() <= nums[i])
        {
            stack.Pop();
            // Maintaining decreasing invariant
        }
        
        // Step 2: If stack non-empty, top is next greater
        if (stack.Count > 0)
            result[i] = stack.Peek();
        
        // Step 3: Push current for future comparisons
        stack.Push(nums[i]);
        
        // Invariant: Stack is always decreasing
        // i.e., bottom < middle < top (conceptually)
    }
    
    return result;
}
```

**C# Notes:**
- `Stack<T>` not `List<T>`: O(1) push/pop vs O(n)
- `Array.Fill()` for bulk initialization (faster than loop)
- Traverse right-to-left to build answer array

---

**Core C# Skeleton Pattern 2: Trapping Rain Water**

```csharp
public int Trap(int[] height)
{
    // MENTAL MODEL: Stack tracks indices; pop to calculate area
    
    // 1. Guard Clauses
    if (height == null || height.Length < 3)
        return 0;
    
    // 2. State Initialization
    var stack = new Stack<int>();  // Indices, not heights
    int waterTrapped = 0;
    
    // 3. Core Logic
    for (int i = 0; i < height.Length; i++)
    {
        // Step 1: Pop and calculate while current > stack top
        while (stack.Count > 0 && height[i] > height[stack.Peek()])
        {
            int topIndex = stack.Pop();
            
            // No water trapped if wall to left is gone
            if (stack.Count == 0)
                break;
            
            // Step 2: Calculate water area
            // Width: distance between current and stack top
            int width = i - stack.Peek() - 1;
            
            // Height: min of left and right walls minus floor
            int waterHeight = Math.Min(height[i], height[stack.Peek()]) 
                              - height[topIndex];
            
            waterTrapped += width * waterHeight;
        }
        
        // Step 3: Push current index
        stack.Push(i);
    }
    
    return waterTrapped;
}
```

**C# Notes:**
- Store **indices** in stack, not values (need for width calculation)
- `Math.Min()` for bounding height
- Calculate area when popping (trapped between walls)

---

### DAY 3: INTERVAL PATTERNS

**C# Mental Model:** Sort first, then single scan merge  
**Common Analogy:** Timeline merging (consecutive events collapse into one)

**When to Use:**
- Merge overlapping intervals
- Meeting room scheduling
- Non-overlapping interval selection

**Core C# Skeleton Pattern 1: Merge Intervals**

```csharp
public int[][] Merge(int[][] intervals)
{
    // MENTAL MODEL: Sort by start, then track max end
    
    // 1. Guard Clauses
    if (intervals == null || intervals.Length == 0)
        return new int[0][];
    
    // 2. State Initialization
    Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
    // Why sort? Guarantees no past interval will overlap future ones
    
    var merged = new List<int[]>();
    int currentStart = intervals[0][0];
    int currentEnd = intervals[0][1];
    
    // 3. Core Logic: Single pass
    for (int i = 1; i < intervals.Length; i++)
    {
        int nextStart = intervals[i][0];
        int nextEnd = intervals[i][1];
        
        // Step 1: Check overlap
        if (nextStart <= currentEnd)
        {
            // Step 2: Overlapping - extend current end
            currentEnd = Math.Max(currentEnd, nextEnd);
            // Why Max? Handle nested intervals correctly
        }
        else
        {
            // Step 3: No overlap - save previous, start new
            merged.Add(new int[] { currentStart, currentEnd });
            currentStart = nextStart;
            currentEnd = nextEnd;
        }
    }
    
    // Don't forget final interval!
    merged.Add(new int[] { currentStart, currentEnd });
    
    return merged.ToArray();
}
```

**C# Notes:**
- `Array.Sort()` with custom comparator for sorting logic
- `Math.Max()` handles nested intervals
- **Critical:** Don't forget to add final interval after loop

---

### DAY 4A: PARTITION & CYCLIC SORT

**C# Mental Model:** Three regions, three pointers (left/mid/right)  
**Common Analogy:** Sorting laundry into three bins (socks/shirts/pants)

**When to Use:**
- Segregating 0/1/2 (Dutch National Flag)
- Moving element X to end
- Cyclic sort for 1..n arrays

**Core C# Skeleton Pattern 1: Dutch National Flag**

```csharp
public void SortColors(int[] nums)
{
    // MENTAL MODEL: Three regions; swap to correct position
    
    // 1. Guard Clauses
    if (nums == null || nums.Length == 0)
        return;
    
    // 2. State Initialization
    int left = 0;      // End of 0s region
    int mid = 0;       // Current element
    int right = nums.Length - 1;  // Start of 2s region
    
    // 3. Core Logic
    while (mid <= right)
    {
        if (nums[mid] == 0)
        {
            // Step 1: 0 belongs in left region
            Swap(nums, left, mid);
            left++;
            mid++;
            // Both move because 0 is now in correct region
        }
        else if (nums[mid] == 1)
        {
            // Step 2: 1 already in correct region
            mid++;
            // Only mid moves; 1 stays
        }
        else // nums[mid] == 2
        {
            // Step 3: 2 belongs in right region
            Swap(nums, mid, right);
            right--;
            // Only right moves; mid stays to check swapped element
        }
    }
    
    // Invariant maintained:
    // [0..left) = all 0s
    // [left..mid) = all 1s
    // [mid..right+1) = all 2s
}

private void Swap(int[] nums, int i, int j)
{
    int temp = nums[i];
    nums[i] = nums[j];
    nums[j] = temp;
}
```

**C# Notes:**
- **Critical:** Don't increment `mid` after swapping with `right` - need to check swapped element
- Three pointers maintain three invariant regions
- Single pass O(n)

---

**Core C# Skeleton Pattern 2: Cyclic Sort**

```csharp
public void CyclicSort(int[] nums)
{
    // MENTAL MODEL: Each element belongs at nums[i-1]
    
    // 1. Guard Clauses
    if (nums == null || nums.Length == 0)
        return;
    
    // 2. State Initialization (implicit in loop)
    
    // 3. Core Logic
    int i = 0;
    while (i < nums.Length)
    {
        // Step 1: Calculate correct position
        int correctPos = nums[i] - 1;  // Value k goes to index k-1
        
        // Step 2: Swap if not in correct position
        if (i != correctPos)
        {
            Swap(nums, i, correctPos);
            // Don't increment i; check swapped element
        }
        else
        {
            // Step 3: In correct position; move to next
            i++;
        }
    }
}

private void Swap(int[] nums, int i, int j)
{
    int temp = nums[i];
    nums[i] = nums[j];
    nums[j] = temp;
}
```

**C# Notes:**
- **Critical:** Don't increment `i` after swap - re-check swapped element
- Works only for 1..n arrays without duplicates
- O(n) time, O(1) space

---

### DAY 4B: KADANE'S ALGORITHM

**C# Mental Model:** Track best ending here + best overall  
**Common Analogy:** Running total - keep sum if positive, restart if negative

**When to Use:**
- Maximum subarray sum
- Maximum product subarray
- Best trading window

**Core C# Skeleton Pattern 1: Maximum Subarray**

```csharp
public int MaxSubArray(int[] nums)
{
    // MENTAL MODEL: Local max ending here; extend or restart
    
    // 1. Guard Clauses
    if (nums == null || nums.Length == 0)
        return 0;
    
    // 2. State Initialization
    int maxEndingHere = nums[0];  // Best sum ending at current
    int maxSoFar = nums[0];       // Best sum seen anywhere
    
    // Why track both?
    // maxEndingHere = local decision point
    // maxSoFar = global answer
    
    // 3. Core Logic
    for (int i = 1; i < nums.Length; i++)
    {
        // Step 1: Decide extend or restart
        maxEndingHere = Math.Max(nums[i], nums[i] + maxEndingHere);
        // If previous sum was negative, restart fresh at nums[i]
        
        // Step 2: Update global maximum
        maxSoFar = Math.Max(maxSoFar, maxEndingHere);
        
        // Visualization of decision:
        // If maxEndingHere < 0: next iteration starts fresh
        // If maxEndingHere > 0: next iteration extends
    }
    
    return maxSoFar;
}
```

**C# Notes:**
- **Critical:** Update `maxSoFar` **inside** loop
- `Math.Max(a, b)` makes extend-or-restart decision
- O(1) space - only two variables

---

**Core C# Skeleton Pattern 2: Maximum Product Subarray**

```csharp
public int MaxProduct(int[] nums)
{
    // MENTAL MODEL: Track both max and min (negative flips signs)
    
    // 1. Guard Clauses
    if (nums == null || nums.Length == 0)
        return 0;
    
    // 2. State Initialization
    int maxEndingHere = nums[0];
    int minEndingHere = nums[0];  // Why min? Negative × min = positive
    int globalMax = nums[0];
    
    // 3. Core Logic
    for (int i = 1; i < nums.Length; i++)
    {
        int current = nums[i];
        
        // Step 1: Compute new max/min before updating
        int newMax = Math.Max(Math.Max(current, current * maxEndingHere), 
                              current * minEndingHere);
        int newMin = Math.Min(Math.Min(current, current * maxEndingHere), 
                              current * minEndingHere);
        
        // Step 2: Update tracking variables
        maxEndingHere = newMax;
        minEndingHere = newMin;
        
        // Step 3: Update global max
        globalMax = Math.Max(globalMax, maxEndingHere);
        
        // Why min? If current negative and minEndingHere large negative,
        // current × minEndingHere = positive (larger than maxEndingHere)
    }
    
    return globalMax;
}
```

**C# Notes:**
- Must compute both `newMax` and `newMin` before updating
- Negative × large negative = large positive
- O(1) space but more complex state

---

### DAY 5: FAST/SLOW POINTERS

**C# Mental Model:** Two speeds → Inevitable meeting in cycle  
**Common Analogy:** Race on circular track - faster catches slower

**When to Use:**
- Detect cycles in linked lists
- Find cycle start node
- Find list middle (for merge sort)

**Core C# Skeleton Pattern 1: Cycle Detection**

```csharp
public bool HasCycle(ListNode head)
{
    // MENTAL MODEL: Fast overtakes slow in cycle
    
    // 1. Guard Clauses
    if (head == null || head.Next == null)
        return false;
    
    // 2. State Initialization
    ListNode slow = head;  // Moves 1 step
    ListNode fast = head;  // Moves 2 steps
    
    // 3. Core Logic
    while (fast != null && fast.Next != null)
    {
        // Step 1: Move pointers
        slow = slow.Next;           // 1 step
        fast = fast.Next.Next;      // 2 steps
        
        // Step 2: Check if they meet
        if (slow == fast)
        {
            return true;  // Cycle detected!
        }
        
        // Invariant: If cycle exists, gap shrinks by 1 each iteration
        // Must meet within cycle length L steps
    }
    
    // Step 3: Fast reached end - no cycle
    return false;
}
```

**C# Notes:**
- **Critical:** Check `fast != null && fast.Next != null` before dereferencing
- `slow == fast` tests reference equality (same node object)
- No extra space needed - just two pointers

---

**Core C# Skeleton Pattern 2: Find Cycle Start**

```csharp
public ListNode DetectCycleStart(ListNode head)
{
    // MENTAL MODEL: Distance from head to cycle start = 
    //               distance from meeting to cycle start
    
    // 1. Guard Clauses
    if (head == null || head.Next == null)
        return null;
    
    // 2. State Initialization
    ListNode slow = head, fast = head;
    
    // Step 1: Find meeting point
    while (fast != null && fast.Next != null)
    {
        slow = slow.Next;
        fast = fast.Next.Next;
        
        if (slow == fast)
            break;  // Found meeting point
    }
    
    // Step 2: Check if cycle exists
    if (fast == null || fast.Next == null)
        return null;  // No cycle
    
    // Step 3: Reset one pointer, move both at same speed
    slow = head;
    while (slow != fast)
    {
        slow = slow.Next;
        fast = fast.Next;  // Both now move 1 step
    }
    
    // They meet at cycle start!
    return slow;
}
```

**C# Notes:**
- After finding cycle, reset `slow` to head
- Move both at equal speed (1 step each)
- They meet exactly at cycle start node
- Mathematical property: distances are equal

---

## 📊 PROGRESSIVE PROBLEM LADDER

### Stage 1: Canonical Problems (Foundation)

| LeetCode | Difficulty | Pattern | C# Focus |
|---|---|---|---|
| 1 | Easy | Hash Complement | Dictionary with early return |
| 242 | Easy | Hash Frequency | String sorting for canonicalization |
| 347 | Medium | Hash + Bucket | Bucket sort O(n) vs heap O(n log k) |
| 75 | Medium | Dutch Flag | Three-pointer with correct invariant |
| 283 | Easy | Move Zeroes | Two-pointer, fill after loop |
| 53 | Medium | Kadane | Track both local and global |
| 141 | Easy | Cycle Detection | Null checks critical |

**Stage 1 Success:** Solve all 7 within 15 min each, no reference

---

### Stage 2: Variations (Deepen Understanding)

| LeetCode | Difficulty | Pattern Twist | C# Focus |
|---|---|---|---|
| 49 | Medium | Grouping by canonical | Dictionary<string, List<string>> |
| 84 | Hard | Monotonic Stack | Stack of indices not values |
| 56 | Medium | Interval Merge | Don't forget final interval |
| 268 | Easy | Cyclic Sort | Find missing via position mapping |
| 152 | Medium | Max Product | Min tracking for negative flip |
| 142 | Medium | Cycle Start | Pointer reset theorem |

**Stage 2 Success:** Mix and match patterns, 20 min each

---

### Stage 3: Integration (Full Mastery)

| Problem Type | Difficulty | Patterns | C# Focus |
|---|---|---|---|
| Two approaches to same problem | Hard | Hash vs Stack vs DP | Justify choice |
| Constraint variation | Medium | Same pattern, new constraint | Modify state |
| Multi-pattern composite | Hard | 2-3 patterns combined | Recognize when each applies |

**Stage 3 Success:** Design novel problems using patterns

---

## 🔴 PITFALLS & C# GOTCHAS

### Pattern-Specific Common Bugs

| Pattern | Common Bug | C# Symptom | Quick Fix |
|---|---|---|---|
| **Hash** | Forgot null check | `NullReferenceException` | Add guard: `if (input == null)` |
| **Hash** | `List<T>` instead of `HashSet<T>` | O(n) lookup instead of O(1) | Use `HashSet<int>` or `Dictionary<K,V>` |
| **Stack** | Wrong stack type | Semantic error (values vs indices) | Stack<int> for indices in histogram |
| **Stack** | Off-by-one in area calculation | Wrong water trapped | Check width and height calculations |
| **Intervals** | Sorted by wrong field | Wrong merges | Sort by start: `(a,b)=>a[0]-b[0]` |
| **Partition** | Increment mid after right swap | Wrong final array | `mid` stays, only `right` changes |
| **Cyclic Sort** | Increment after swap | Element not in correct place | `i` stays, only checks after swap |
| **Kadane** | Update maxSoFar outside loop | Miss true maximum | Move update inside for loop |
| **Kadane Product** | Update max/min same time | Use old values in calc | Compute new values first, then assign |
| **Pointers** | No null checks | `NullReferenceException` | Always: `fast != null && fast.Next != null` |

---

### C# Collection Selection Quick Guide

| Collection | When | Why | Example |
|---|---|---|---|
| `HashSet<T>` | Need O(1) membership | Average O(1) lookup | Seen values, deduplication |
| `Dictionary<K,V>` | Need O(1) key→value | Flexible pairing | Value→index mapping |
| `Stack<T>` | Need LIFO, O(1) push/pop | Efficient stack ops | Monotonic stack, DFS |
| `List<T>` | Need O(1) indexing | Random access | Dynamic results array |
| `Array` | Size fixed, O(1) index | Efficiency + simplicity | Partition (in-place), DP tracking |
| `PriorityQueue<T>` | Need sorted access | O(log n) per op | Merge K lists, top-k |

---

## ✅ WEEK 05 COMPLETION CHECKLIST

### Pattern Fluency
- [ ] Recall Hash complement skeleton in < 2 min
- [ ] Recall Monotonic stack skeleton in < 2 min
- [ ] Recall Partition (Dutch flag) skeleton in < 2 min
- [ ] Recall Kadane's skeleton in < 2 min
- [ ] Recall Fast/Slow cycle detection in < 2 min

### Problem Solving
- [ ] Solved Stage 1 canonical: 7/7 within time
- [ ] Solved Stage 2 variations: 6/6 within time
- [ ] Created Stage 3 integration problems: 3/3

### C# Implementation
- [ ] Used correct Collections (Hash, Stack, Array)
- [ ] Handled all edge cases (null, empty, single element)
- [ ] Wrote production-grade code (no "spaghetti")
- [ ] Justified collection choices in code comments

### Ready for Interview?
- [ ] Can solve 65% of Week 05 patterns
- [ ] Can explain collection choice (why not List?)
- [ ] Can identify pattern from problem phrase
- [ ] Can trace through complex examples

**If all ✅ → Ready for Week 06**

---

**Status:** Production-Ready Problem-Solving Roadmap | **Format:** Extended C# Support  
**Generated:** January 08, 2026 | **System:** v12 Extended C# Template


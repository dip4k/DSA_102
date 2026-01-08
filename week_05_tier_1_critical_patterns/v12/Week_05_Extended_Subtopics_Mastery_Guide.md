# 🎓 WEEK 05 EXTENDED SUBTOPICS & MASTERY GUIDE

**Week:** 5 | **Tier:** Tier 1 Critical Patterns  
**Purpose:** Master additional subtopics NOT in syllabus but REQUIRED for true understanding  
**Format:** Day-by-day deep-dive with practical implementations

---

## 🎯 OVERVIEW: Why These Subtopics Matter

The official syllabus covers **core patterns**. This guide covers **mastery depth** required for:
- Production systems (not just interviews)
- Understanding trade-offs deeply
- Building intuition for novel problems
- Real-world constraint handling

Each day includes **8-12 additional subtopics** organized by:
1. **Foundation** (why this exists)
2. **Mechanics** (how it works)
3. **Trade-offs** (when to use)
4. **Implementation** (C# code patterns)
5. **Real-world** (where it appears)

---

## 📅 DAY 1: HASH PATTERNS — EXTENDED MASTERY

### Subtopic 1.1: Hash Collision Resolution Deep Dive

**Foundation:** Why collisions matter even with good hash functions

**Two Main Strategies:**

**Chaining (Separate Chaining):**
```csharp
// Dictionary internally uses chaining in .NET
// Multiple key-value pairs stored in linked list at each bucket
Dictionary<string, int> dict = new Dictionary<string, int>();

// O(1) average for insert/lookup
// O(1) amortized; O(n) worst case with poor hash function
// Space: O(n + m) where m = number of buckets
```

**Open Addressing:**
```csharp
// Linear probing, quadratic probing, double hashing
// When collision: find next empty slot
// Pros: Better cache locality
// Cons: Clustering, complex deletion

// Example: Linear probing pseudocode
int index = hash(key) % tableSize;
while (table[index] != null && table[index].key != key)
{
    index = (index + 1) % tableSize;  // Linear probe
}
```

**Trade-offs:**
| Approach | Pros | Cons | When Use |
|---|---|---|---|
| Chaining | Simple deletion, lower load factor | Pointer overhead | General-purpose (like .NET Dictionary) |
| Linear Probing | Cache-friendly | Clustering, performance degrades | Small datasets, fixed size |
| Quadratic Probing | Reduces clustering | More complex | Medium datasets |
| Double Hashing | Best clustering reduction | Compute overhead | Large datasets, open addressing |

**Mastery Insight:** .NET Dictionary uses chaining with dynamic sizing. Understanding this explains why it maintains performance even at high load factors.

---

### Subtopic 1.2: Load Factor Management & Resizing

**Foundation:** When and how hash tables grow

```csharp
// Load factor = number of entries / bucket count
// High load factor → more collisions → slower lookups

// .NET Dictionary resizes when:
// - Load factor exceeds ~0.75-0.90 (implementation detail)
// - Resize to next prime number
// - O(n) operation to rehash all entries

public class HashTableWithResizing
{
    private static float MAX_LOAD_FACTOR = 0.75f;
    private Entry[] table;
    private int count;
    
    private void CheckResize()
    {
        float loadFactor = (float)count / table.Length;
        if (loadFactor > MAX_LOAD_FACTOR)
        {
            // Resize to ~2x next prime
            Resize();  // O(n) rehashing
        }
    }
    
    private void Resize()
    {
        int newSize = GetNextPrimeSize(table.Length * 2);
        Entry[] oldTable = table;
        table = new Entry[newSize];
        count = 0;
        
        // Rehash all entries
        foreach (var entry in oldTable)
        {
            if (entry != null)
                Add(entry.Key, entry.Value);
        }
    }
}
```

**Mastery Insight:** Choosing MAX_LOAD_FACTOR is trade-off:
- Low (0.5): Fast lookup, more memory
- High (0.9): Memory efficient, slower lookup

**Real-world:** Redis sets initial hash table size with predictable load factor management.

---

### Subtopic 1.3: Custom Hash Function Design

**Foundation:** Good hash functions are critical

```csharp
// Criteria for good hash function:
// 1. Deterministic (same input → same output always)
// 2. Uniformly distributed (minimize collisions)
// 3. Avalanche effect (tiny change → huge hash change)
// 4. Efficient (O(1) computation)
// 5. Collision-resistant (hard to find collisions intentionally)

public class CustomHashFunctionExamples
{
    // Bad hash function (clusters many strings together)
    public int BadHash(string s)
    {
        return s[0] % 256;  // Only depends on first character!
    }
    
    // Better: Polynomial rolling hash
    public int PolynomialHash(string s, int prime = 31)
    {
        long hash = 0;
        long primePower = 1;
        
        for (int i = s.Length - 1; i >= 0; i--)
        {
            hash += (s[i] - 'a' + 1) * primePower;
            primePower *= prime;
            // Modulo to prevent overflow
            hash %= 1000000007;
        }
        
        return (int)hash;
    }
    
    // FNV-1a hash (good general purpose)
    public long FNV1aHash(string s)
    {
        long hash = 14695981039346656037;  // FNV offset basis
        foreach (char c in s)
        {
            hash ^= c;
            hash *= 1099511628211;  // FNV prime
        }
        return hash;
    }
}
```

**Mastery Insight:** .NET uses combination of object.GetHashCode() and internal mixing to get uniform distribution.

---

### Subtopic 1.4: String Hashing & Polynomial Rolling Hash

**Foundation:** Strings need special handling

```csharp
// Polynomial rolling hash for pattern matching
public class RollingHashMatcher
{
    private const int PRIME = 31;
    private const long MOD = 1000000007;
    
    public int FindPattern(string text, string pattern)
    {
        int n = text.Length;
        int m = pattern.Length;
        
        // Precompute pattern hash
        long patternHash = ComputeHash(pattern);
        long textHash = ComputeHash(text.Substring(0, m));
        
        // Precompute prime^(m-1) % MOD
        long h = 1;
        for (int i = 0; i < m - 1; i++)
            h = (h * PRIME) % MOD;
        
        // Slide window through text
        for (int i = 0; i <= n - m; i++)
        {
            // If hashes match, verify actual strings (avoid false positives)
            if (textHash == patternHash && 
                text.Substring(i, m) == pattern)
                return i;
            
            // Roll hash: remove first char, add new char
            if (i < n - m)
            {
                textHash = (PRIME * (textHash - text[i] * h) + text[i + m]) % MOD;
                if (textHash < 0)
                    textHash += MOD;
            }
        }
        
        return -1;
    }
    
    private long ComputeHash(string s)
    {
        long hash = 0;
        foreach (char c in s)
        {
            hash = (hash * PRIME + c) % MOD;
        }
        return hash;
    }
}
```

**Mastery Insight:** Rolling hash enables O(n+m) pattern matching (Rabin-Karp algorithm) vs. naive O(n×m).

---

### Subtopic 1.5: Three-Sum & K-Sum Generalization

**Foundation:** Scaling two-sum to k elements

```csharp
public class KSumSolver
{
    // Two-Sum: Hash-based O(n) solution
    public int[] TwoSum(int[] nums, int target)
    {
        var seen = new HashSet<int>();
        foreach (int num in nums)
        {
            if (seen.Contains(target - num))
                return new int[] { target - num, num };
            seen.Add(num);
        }
        return null;
    }
    
    // Three-Sum: Reduce to two-sum
    public IList<IList<int>> ThreeSum(int[] nums, int target = 0)
    {
        Array.Sort(nums);  // O(n log n)
        var results = new List<IList<int>>();
        
        // Fix first element, find two-sum for remainder
        for (int i = 0; i < nums.Length - 2; i++)
        {
            if (i > 0 && nums[i] == nums[i - 1])
                continue;  // Skip duplicates
            
            // Two-pointer approach (faster than hash for sorted array)
            int left = i + 1, right = nums.Length - 1;
            int remainder = target - nums[i];
            
            while (left < right)
            {
                int sum = nums[left] + nums[right];
                if (sum == remainder)
                {
                    results.Add(new List<int> { nums[i], nums[left], nums[right] });
                    left++;
                    right--;
                    
                    // Skip duplicates
                    while (left < right && nums[left] == nums[left - 1]) left++;
                    while (left < right && nums[right] == nums[right + 1]) right--;
                }
                else if (sum < remainder)
                    left++;
                else
                    right--;
            }
        }
        
        return results;
    }
    
    // K-Sum: General recursion
    public IList<IList<int>> KSum(int[] nums, int target, int k)
    {
        Array.Sort(nums);
        return KSumHelper(nums, target, k, 0);
    }
    
    private IList<IList<int>> KSumHelper(int[] nums, int target, int k, int start)
    {
        var results = new List<IList<int>>();
        
        if (k == 2)
        {
            // Base case: two-sum
            int left = start, right = nums.Length - 1;
            while (left < right)
            {
                int sum = nums[left] + nums[right];
                if (sum == target)
                {
                    results.Add(new List<int> { nums[left], nums[right] });
                    left++;
                    right--;
                    while (left < right && nums[left] == nums[left - 1]) left++;
                }
                else if (sum < target)
                    left++;
                else
                    right--;
            }
        }
        else
        {
            // Recursive: reduce to k-1 sum
            for (int i = start; i < nums.Length - k + 1; i++)
            {
                if (i > start && nums[i] == nums[i - 1])
                    continue;
                
                var subResults = KSumHelper(nums, target - nums[i], k - 1, i + 1);
                foreach (var subResult in subResults)
                {
                    subResult.Insert(0, nums[i]);
                    results.Add(subResult);
                }
            }
        }
        
        return results;
    }
}
```

**Time Complexity Analysis:**
- Two-Sum: O(n)
- Three-Sum: O(n²) (sort + nested loop)
- K-Sum: O(n^(k-1) log n) (rough estimate)

**Mastery Insight:** Three-sum forces trade-off between hash (O(n) space, O(n) time) and two-pointer sorted (O(n²) time, O(1) space).

---

### Subtopic 1.6: Two-Pointer Sorted vs. Hash Approach Trade-offs

**Foundation:** Different tools for similar problems

```csharp
public class TwoSumComparison
{
    // Approach 1: Hash Table (Unsorted Array)
    public bool TwoSumHashApproach(int[] nums, int target)
    {
        // Time: O(n), Space: O(n)
        var seen = new HashSet<int>();
        foreach (int num in nums)
        {
            if (seen.Contains(target - num))
                return true;
            seen.Add(num);
        }
        return false;
    }
    
    // Approach 2: Two-Pointer (Sorted Array)
    public bool TwoSumTwoPointerApproach(int[] nums, int target)
    {
        // Time: O(n log n) for sorting + O(n) for pointers = O(n log n)
        // Space: O(1) extra space (ignoring sort space)
        
        Array.Sort(nums);  // O(n log n)
        int left = 0, right = nums.Length - 1;
        
        while (left < right)
        {
            int sum = nums[left] + nums[right];
            if (sum == target)
                return true;
            else if (sum < target)
                left++;
            else
                right--;
        }
        return false;
    }
    
    // Approach 3: SkipList (Balanced structure)
    // Time: O(n log n), Space: O(n), Better cache than hash
}
```

**Decision Tree:**
```
Problem: Find if two elements sum to target

Is array already sorted?
├─ YES → Use two-pointer (O(n), O(1) space)
└─ NO → 
    Need multiple queries on same array?
    ├─ YES → Hash (O(n) per query), sort once for two-pointer
    └─ NO →
        Can modify array?
        ├─ YES → Sort + two-pointer (O(n log n))
        └─ NO → Hash (O(n) space, O(n) time)
```

---

### Subtopic 1.7: Dictionary vs. HashSet Performance

**Foundation:** Understanding when each is better

```csharp
public class DictionaryVsHashSet
{
    // Benchmark scenario: Store and lookup user IDs
    
    public void PerformanceComparison()
    {
        int n = 1_000_000;
        var ids = new string[n];
        for (int i = 0; i < n; i++)
            ids[i] = $"user_{i}";
        
        // HashSet: Just membership
        var hashSet = new HashSet<string>(ids);
        var lookupHashSet = hashSet.Contains("user_500000");  // Very fast
        
        // Dictionary: Key→Value mapping
        var dict = new Dictionary<string, int>();
        for (int i = 0; i < ids.Length; i++)
            dict[ids[i]] = i;  // Store index
        var index = dict["user_500000"];  // Also fast, but extra storage
    }
}
```

**Comparison Table:**
| Aspect | HashSet<T> | Dictionary<K,V> |
|--------|-----------|-----------------|
| Memory (per element) | ~32 bytes | ~32 + size(value) |
| Contains check | O(1) avg | O(1) avg |
| Iteration | O(n) | O(n) |
| Add duplicates | Ignored | Overwrites |
| Enumerate values | N/A | dict.Values |
| Remove efficiency | O(1) avg | O(1) avg |

**Mastery Insight:** Use HashSet when only testing membership; Dictionary when need value-association.

---

### Subtopic 1.8: Memory Layout & Cache Considerations

**Foundation:** Why hash table implementation matters

```csharp
// .NET Dictionary internal structure (conceptual):
public class DictionaryInternals
{
    private Entry[] entries;  // Array of entries (contiguous memory)
    private int[] buckets;    // Array of bucket indices
    
    private struct Entry
    {
        public int hashCode;
        public int next;      // Next entry in chain (-1 if end)
        public TKey key;
        public TValue value;
    }
    
    // Benefits of this design:
    // 1. Contiguous entry array → better cache locality
    // 2. All entries stored together, only following references when needed
    // 3. Better than separate chain nodes (would scatter memory)
}
```

**Real-world Impact:**
- Hash tables with chaining: ~2-3x slower in cache misses
- .NET Dictionary: Optimized for cache (entries array)
- Result: .NET Dictionary faster than naive linked-list chaining

**Mastery Insight:** Performance isn't just about Big-O; memory layout and cache matter enormously.

---

### Subtopic 1.9: Multi-Key Lookup Patterns

**Foundation:** Handling composite keys

```csharp
public class MultiKeyPatterns
{
    // Pattern 1: Nested dictionaries
    public class NestedDictionaryExample
    {
        // userId → (courseId → score)
        Dictionary<string, Dictionary<string, int>> scores = 
            new Dictionary<string, Dictionary<string, int>>();
        
        public void AddScore(string userId, string courseId, int score)
        {
            if (!scores.ContainsKey(userId))
                scores[userId] = new Dictionary<string, int>();
            scores[userId][courseId] = score;
        }
        
        public int GetScore(string userId, string courseId)
        {
            return scores[userId][courseId];
        }
    }
    
    // Pattern 2: Composite key (better performance)
    public class CompositeKeyExample
    {
        // Create composite key string
        private string MakeKey(string userId, string courseId)
            => $"{userId}|{courseId}";
        
        Dictionary<string, int> scores = new Dictionary<string, int>();
        
        public void AddScore(string userId, string courseId, int score)
        {
            scores[MakeKey(userId, courseId)] = score;
        }
        
        public int GetScore(string userId, string courseId)
        {
            return scores[MakeKey(userId, courseId)];
        }
    }
    
    // Pattern 3: Tuple as composite key (C# 7.1+)
    public class TupleKeyExample
    {
        Dictionary<(string, string), int> scores = 
            new Dictionary<(string, string), int>();
        
        public void AddScore(string userId, string courseId, int score)
        {
            scores[(userId, courseId)] = score;
        }
    }
}
```

**Performance:** Tuple > String concatenation > Nested Dictionary

**Mastery Insight:** Tuple keys are idiomatic C# and have best performance.

---

### Subtopic 1.10: Handling Hash Collisions in Interviews

**Foundation:** Preparing for "what if hash function is bad?"

```csharp
// Interview scenario: Guarantee hash function quality
public class RobustHashUsage
{
    public class Example
    {
        // Assume bad hash function provided
        private Func<string, int> badHashFunc;
        
        // Solution 1: Add hash function verification
        private Dictionary<string, int> SafeLookup(List<string> items)
        {
            var dict = new Dictionary<string, int>();
            
            // Instead of relying on default hash:
            // Create composite key with position verification
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                // Verify no collision
                if (dict.ContainsKey(item))
                    throw new InvalidOperationException("Duplicate!");
                dict[item] = i;
            }
            
            return dict;
        }
        
        // Solution 2: Double-check with different hash
        private bool VerifyPair(int[] nums, int target)
        {
            var seen = new HashSet<int>();
            
            foreach (int num in nums)
            {
                int needed = target - num;
                if (seen.Contains(needed))
                {
                    // Double-verify before returning
                    if (num + needed == target)  // Redundant but safe
                        return true;
                }
                seen.Add(num);
            }
            
            return false;
        }
    }
}
```

**Interview Tip:** Don't worry about bad hash functions—rely on language implementation.

---

## 📈 DAY 2: MONOTONIC STACK — EXTENDED MASTERY

### Subtopic 2.1: Stock Span Problem

**Foundation:** Application of monotonic stack pattern

```csharp
public class StockSpanProblem
{
    // Problem: For each price, find span = number of consecutive days 
    // price >= current price (looking backward)
    
    // Example: prices = [100, 80, 60, 70, 60, 75, 85]
    // Output: [1, 1, 1, 2, 1, 4, 6]
    // Explanation:
    // Day 0: 100 (span = 1, only itself)
    // Day 1: 80 (span = 1, only itself since 100 > 80)
    // Day 2: 60 (span = 1)
    // Day 3: 70 (span = 2, includes day 2 with 60)
    // Day 4: 60 (span = 1)
    // Day 5: 75 (span = 4, days 2-5 all have price <= 75)
    // Day 6: 85 (span = 6, days 1-6 all have price < 85)
    
    public class Solution
    {
        private class PriceDay
        {
            public int Price;
            public int Index;
        }
        
        public int[] CalculateSpans(int[] prices)
        {
            int n = prices.Length;
            int[] spans = new int[n];
            var stack = new Stack<PriceDay>();
            
            for (int i = 0; i < n; i++)
            {
                // Pop all smaller prices (they're not in span anymore)
                while (stack.Count > 0 && stack.Peek().Price <= prices[i])
                    stack.Pop();
                
                // Span = distance to previous greater or start
                spans[i] = stack.Count == 0 ? i + 1 : i - stack.Peek().Index;
                
                // Push current
                stack.Push(new PriceDay { Price = prices[i], Index = i });
            }
            
            return spans;
        }
    }
}
```

**Time:** O(n), Space: O(n)  
**Key Insight:** Each element pushed/popped once = O(n) total

---

### Subtopic 2.2: Daily Temperature Variant

**Foundation:** Different application of same pattern

```csharp
public class DailyTemperatures
{
    // Find next warmer day for each day
    public int[] DailyTemperaturesNearest(int[] temperatures)
    {
        int n = temperatures.Length;
        int[] result = new int[n];
        var stack = new Stack<int>();  // Store indices
        
        for (int i = n - 1; i >= 0; i--)
        {
            // Pop cooler days (they can't be answer)
            while (stack.Count > 0 && 
                   temperatures[stack.Peek()] <= temperatures[i])
                stack.Pop();
            
            // Next warmer day index
            result[i] = stack.Count > 0 ? stack.Peek() - i : 0;
            
            stack.Push(i);
        }
        
        return result;
    }
}
```

---

### Subtopic 2.3: Sum of Subarray Minimums

**Foundation:** Complex monotonic stack application

```csharp
public class SumOfSubarrayMinimums
{
    // Find sum of minimums for all subarrays
    // Example: [3,1,2] → subarrays: [3]=3, [1]=1, [2]=2, [3,1]=1, [1,2]=1, [3,1,2]=1
    // Sum = 3+1+2+1+1+1 = 9
    
    public int SumSubarrayMins(int[] arr)
    {
        int n = arr.Length;
        int[] leftCount = new int[n];   // Elements to left >= arr[i]
        int[] rightCount = new int[n];  // Elements to right > arr[i]
        
        // For each element, count how many subarrays it's the minimum
        
        // Left: find previous smaller element
        var leftStack = new Stack<int>();
        for (int i = 0; i < n; i++)
        {
            while (leftStack.Count > 0 && arr[leftStack.Peek()] > arr[i])
                leftStack.Pop();
            
            leftCount[i] = leftStack.Count == 0 ? i + 1 : i - leftStack.Peek();
            leftStack.Push(i);
        }
        
        // Right: find next smaller or equal element
        var rightStack = new Stack<int>();
        for (int i = n - 1; i >= 0; i--)
        {
            while (rightStack.Count > 0 && arr[rightStack.Peek()] >= arr[i])
                rightStack.Pop();
            
            rightCount[i] = rightStack.Count == 0 ? n - i : rightStack.Peek() - i;
            rightStack.Push(i);
        }
        
        long result = 0;
        const long MOD = 1000000007;
        
        for (int i = 0; i < n; i++)
        {
            long contribution = (long)arr[i] * leftCount[i] * rightCount[i];
            result = (result + contribution) % MOD;
        }
        
        return (int)result;
    }
}
```

**Insight:** Element is minimum in `left × right` subarrays

---

### Subtopic 2.4: Removing K Digits to Get Smallest Number

**Foundation:** Monotonic stack for digit ordering

```csharp
public class RemoveKDigits
{
    // Remove k digits from number string to get smallest result
    // Example: "1432219", k=3 → "1219"
    
    public string RemoveKDigits(string num, int k)
    {
        int toRemove = k;
        var stack = new Stack<char>();
        
        // Greedy: for each digit, remove larger digits before it
        foreach (char digit in num)
        {
            while (toRemove > 0 && stack.Count > 0 && 
                   stack.Peek() > digit)
            {
                stack.Pop();
                toRemove--;
            }
            
            stack.Push(digit);
        }
        
        // If still need to remove (monotonic increasing case)
        while (toRemove > 0)
        {
            stack.Pop();
            toRemove--;
        }
        
        // Build result
        var result = new StringBuilder();
        while (stack.Count > 0)
            result.Insert(0, stack.Pop());
        
        // Remove leading zeros
        int start = 0;
        while (start < result.Length - 1 && result[start] == '0')
            start++;
        
        return result.ToString().Substring(start);
    }
}
```

**Insight:** Monotonic stack maintains decreasing order for minimal result

---

### Subtopic 2.5: Amortized Analysis Proof

**Foundation:** Why O(n) amortized despite looking O(n²)?

```csharp
public class AmortizedAnalysisExplained
{
    // Claim: Monotonic stack is O(n) amortized
    
    // Proof by accounting method:
    // - Each element pushed exactly once: O(n) operations
    // - Each element popped exactly once: O(n) operations
    // - Total push + pop = O(2n) = O(n)
    
    // Why it looks O(n²):
    // for (int i = 0; i < n; i++)
    // {
    //     while (stack.Pop()) { ... }  // Looks like nested loop!
    // }
    
    // But each element pops once total, not once per outer iteration
    
    public void AmortizedCostExample()
    {
        int[] arr = { 1, 3, 2, 4, 5 };
        var stack = new Stack<int>();
        
        // Iteration 0: push 1 (1 push)
        // Iteration 1: push 3 (1 push)
        // Iteration 2: pop 3, push 2 (1 pop + 1 push)
        // Iteration 3: pop 2, pop 1, push 4 (2 pops + 1 push)
        // Iteration 4: push 5 (1 push)
        // Total: 5 pushes + 3 pops = 8 operations = O(n)
        
        // Not O(n²) despite while loop!
    }
}
```

**Key Insight:** Total pops across entire algorithm ≤ total pushes = n

---

### Subtopic 2.6: Monotonic Deque vs. Stack

**Foundation:** When deque is better

```csharp
public class MonotonicDequeVsStack
{
    // Problem: Max in all sliding windows
    // Example: arr = [1,3,-1,-3,5,3,6,7], k=3
    // Windows: [1,3,-1]=3, [3,-1,-3]=3, [-1,-3,5]=5, [-3,5,3]=5, [5,3,6]=6, [3,6,7]=7
    
    public int[] MaxSlidingWindow(int[] nums, int k)
    {
        // Monotonic DEQUE solution (better than stack!)
        var deque = new LinkedList<int>();  // Store indices
        var result = new List<int>();
        
        for (int i = 0; i < nums.Length; i++)
        {
            // Remove indices outside window
            while (deque.Count > 0 && deque.First.Value < i - k + 1)
                deque.RemoveFirst();
            
            // Remove elements smaller than current (from back)
            // This is why we need DEQUE, not stack!
            while (deque.Count > 0 && nums[deque.Last.Value] < nums[i])
                deque.RemoveLast();
            
            // Add current
            deque.AddLast(i);
            
            // First element is max
            if (i >= k - 1)
                result.Add(nums[deque.First.Value]);
        }
        
        return result.ToArray();
    }
}
```

**Why Deque?**
- Stack: Can only remove from top (can't remove outside window from middle)
- Deque: Can remove from both ends (window boundary + smaller elements)

---

### Subtopic 2.7: Building Visibility Problem

**Foundation:** 2D variant of monotonic stack

```csharp
public class BuildingVisibilityProblem
{
    // Problem: How many buildings to the left/right can each building see?
    // Use monotonic stack to solve efficiently
    
    public int[] GetBuildingVisibility(int[] buildings)
    {
        int n = buildings.Length;
        int[] result = new int[n];
        var stack = new Stack<int>();
        
        for (int i = 0; i < n; i++)
        {
            int visibleCount = 0;
            
            // Pop buildings that current building blocks
            while (stack.Count > 0 && buildings[stack.Peek()] < buildings[i])
            {
                visibleCount++;  // Can see this building
                stack.Pop();
            }
            
            // If same height, can see one
            if (stack.Count > 0 && buildings[stack.Peek()] == buildings[i])
                visibleCount++;
            
            result[i] = visibleCount;
            stack.Push(i);
        }
        
        return result;
    }
}
```

---

## 🔄 DAY 3: INTERVALS — EXTENDED MASTERY

### Subtopic 3.1: Interval Scheduling Optimization

**Foundation:** Greedy algorithm proof

```csharp
public class IntervalSchedulingOptimization
{
    // Problem: Select maximum number of non-overlapping intervals
    // Greedy: Always pick interval ending earliest!
    
    public class Interval
    {
        public int Start, End;
    }
    
    public int MaxNonOverlappingIntervals(Interval[] intervals)
    {
        // Sort by end time (greedy choice point)
        Array.Sort(intervals, (a, b) => a.End.CompareTo(b.End));
        
        int count = 0;
        int lastEnd = int.MinValue;
        
        foreach (var interval in intervals)
        {
            if (interval.Start >= lastEnd)  // No overlap
            {
                count++;
                lastEnd = interval.End;
            }
        }
        
        return count;
    }
    
    // Proof that greedy works:
    // 1. Sort by end time
    // 2. Pick first interval (ends earliest)
    // 3. This leaves most room for future intervals
    // 4. Any other choice would be suboptimal
}
```

**Why end-time greedy works:** Picking earliest ending interval leaves maximum space for remaining intervals.

---

### Subtopic 3.2: Weighted Interval Scheduling (DP Approach)

**Foundation:** When greedy fails, use DP

```csharp
public class WeightedIntervalScheduling
{
    // Problem: Select non-overlapping intervals with maximum weight sum
    // Greedy doesn't work—need DP!
    
    // Example: intervals = [(1,4,3), (2,3,2), (4,6,4), (6,8,2)]
    // Greedy by end: pick (2,3,2)+(4,6,4) = 6
    // Optimal: pick (1,4,3)+(4,6,4) = 7
    
    public class Interval
    {
        public int Start, End, Weight;
    }
    
    public int MaxWeightedIntervals(Interval[] intervals)
    {
        Array.Sort(intervals, (a, b) => a.End.CompareTo(b.End));
        int n = intervals.Length;
        int[] dp = new int[n];
        
        dp[0] = intervals[0].Weight;
        
        for (int i = 1; i < n; i++)
        {
            // Option 1: Include current interval
            int include = intervals[i].Weight;
            
            // Find latest interval that doesn't overlap
            int j = FindLatestNonOverlapping(intervals, i);
            if (j >= 0)
                include += dp[j];
            
            // Option 2: Skip current
            int exclude = dp[i - 1];
            
            dp[i] = Math.Max(include, exclude);
        }
        
        return dp[n - 1];
    }
    
    private int FindLatestNonOverlapping(Interval[] intervals, int current)
    {
        // Binary search for latest non-overlapping
        int left = 0, right = current - 1;
        int result = -1;
        
        while (left <= right)
        {
            int mid = (left + right) / 2;
            if (intervals[mid].End <= intervals[current].Start)
            {
                result = mid;
                left = mid + 1;
            }
            else
                right = mid - 1;
        }
        
        return result;
    }
}
```

**Key Insight:** Weighted requires DP with binary search; time: O(n log n)

---

### Subtopic 3.3: Sweep Line Algorithm

**Foundation:** Event-based interval processing

```csharp
public class SweepLineAlgorithm
{
    // Problem: Find all times when meetings overlap (active meetings)
    
    // Approach: Create events, sweep through time
    
    public class Event
    {
        public int Time;
        public bool IsStart;  // true for start, false for end
    }
    
    public int[] MeetingOverlapTimes(List<(int start, int end)> meetings)
    {
        var events = new List<Event>();
        
        // Create start and end events
        foreach (var (start, end) in meetings)
        {
            events.Add(new Event { Time = start, IsStart = true });
            events.Add(new Event { Time = end, IsStart = false });
        }
        
        // Sort by time (start before end if same time)
        events.Sort((a, b) =>
        {
            if (a.Time != b.Time)
                return a.Time.CompareTo(b.Time);
            return a.IsStart ? -1 : 1;  // Start events before end
        });
        
        var result = new List<int>();
        int activeCount = 0;
        
        // Sweep through events
        foreach (var evt in events)
        {
            if (evt.IsStart)
            {
                activeCount++;
                if (activeCount == 2)
                    result.Add(evt.Time);  // Overlap starts
            }
            else
            {
                if (activeCount == 2)
                    result.Add(evt.Time);  // Overlap ends
                activeCount--;
            }
        }
        
        return result.ToArray();
    }
}
```

**Time:** O(n log n) for sorting, O(n) for sweep

---

### Subtopic 3.4: Room Allocation Algorithm

**Foundation:** Apply interval concepts to resource allocation

```csharp
public class RoomAllocationAlgorithm
{
    // Problem: Minimum rooms needed for all meetings
    
    public int MinMeetingRooms(List<(int start, int end)> meetings)
    {
        var events = new List<(int time, int type)>();
        
        foreach (var (start, end) in meetings)
        {
            events.Add((start, 1));   // Start event
            events.Add((end, -1));    // End event
        }
        
        // Sort: end events before start events if same time
        events.Sort((a, b) =>
        {
            if (a.time != b.time)
                return a.time.CompareTo(b.time);
            return a.type.CompareTo(b.type);  // End (-1) before Start (1)
        });
        
        int maxRooms = 0;
        int currentRooms = 0;
        
        foreach (var (time, type) in events)
        {
            currentRooms += type;
            maxRooms = Math.Max(maxRooms, currentRooms);
        }
        
        return maxRooms;
    }
}
```

**Insight:** Process end events before start events when same time (free room first)

---

### Subtopic 3.5: Meeting Conflict Detection

**Foundation:** Efficient conflict detection using intervals

```csharp
public class MeetingConflictDetection
{
    // Problem: Check if all meetings fit without conflict
    
    public bool CanAttendAllMeetings(List<(int start, int end)> meetings)
    {
        // Sort by start time
        var sorted = meetings.OrderBy(m => m.start).ToList();
        
        for (int i = 1; i < sorted.Count; i++)
        {
            if (sorted[i].start < sorted[i - 1].end)
                return false;  // Overlap found
        }
        
        return true;
    }
    
    // Also return which meetings conflict
    public List<((int, int), (int, int))> FindConflictingPairs(
        List<(int start, int end)> meetings)
    {
        var result = new List<((int, int), (int, int))>();
        
        for (int i = 0; i < meetings.Count; i++)
        {
            for (int j = i + 1; j < meetings.Count; j++)
            {
                if (!(meetings[i].end <= meetings[j].start || 
                      meetings[j].end <= meetings[i].start))
                {
                    result.Add((meetings[i], meetings[j]));
                }
            }
        }
        
        return result;
    }
}
```

---

## 🎨 DAY 4A: PARTITION & CYCLIC SORT — EXTENDED MASTERY

### Subtopic 4A.1: Quicksort Partitioning Basis

**Foundation:** Understanding partition's role in quicksort

```csharp
public class QuicksortPartitioning
{
    // Quicksort = recursive partitioning
    
    public void QuickSort(int[] arr, int low, int high)
    {
        if (low < high)
        {
            // Partition and get pivot index
            int pi = Partition(arr, low, high);
            
            // Recursively sort left and right
            QuickSort(arr, low, pi - 1);
            QuickSort(arr, pi + 1, high);
        }
    }
    
    private int Partition(int[] arr, int low, int high)
    {
        // Choose pivot (last element for simplicity)
        int pivot = arr[high];
        
        // i = end of smaller elements
        int i = low - 1;
        
        for (int j = low; j < high; j++)
        {
            if (arr[j] < pivot)
            {
                i++;
                Swap(arr, i, j);
            }
        }
        
        Swap(arr, i + 1, high);
        return i + 1;
    }
    
    private void Swap(int[] arr, int i, int j)
    {
        int temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }
}
```

**Time Complexity:**
- Average: O(n log n)
- Worst: O(n²) (when pivot always smallest/largest)

---

### Subtopic 4A.2: 3-Way Partition for Duplicates

**Foundation:** Optimize quicksort with many duplicates

```csharp
public class ThreeWayPartition
{
    // Problem: Quicksort slow when many duplicates
    // Solution: 3-way partition (Dutch National Flag style)
    
    // Regions: < pivot | == pivot | > pivot
    
    public void QuickSort3Way(int[] arr, int low, int high)
    {
        if (low < high)
        {
            int[] bounds = Partition3Way(arr, low, high);
            QuickSort3Way(arr, low, bounds[0] - 1);
            QuickSort3Way(arr, bounds[1] + 1, high);
        }
    }
    
    private int[] Partition3Way(int[] arr, int low, int high)
    {
        int pivot = arr[high];
        int lt = low;      // End of < region
        int gt = high - 1; // Start of > region
        int i = low;
        
        while (i <= gt)
        {
            if (arr[i] < pivot)
                Swap(arr, i++, lt++);
            else if (arr[i] > pivot)
                Swap(arr, i, gt--);
            else
                i++;  // Equal—stay in middle
        }
        
        Swap(arr, gt + 1, high);  // Place pivot
        return new int[] { lt, gt + 1 };
    }
    
    private void Swap(int[] arr, int i, int j)
    {
        int temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }
}
```

**Benefit:** With many duplicates, 3-way partition avoids unnecessary recursive calls on equal elements.

---

### Subtopic 4A.3: Partition for K-th Smallest

**Foundation:** Use partition to find k-th element

```csharp
public class KthSmallestViaPartition
{
    // Problem: Find k-th smallest element
    // Option 1: Sort—O(n log n)
    // Option 2: Partition—O(n) average
    
    public int FindKthSmallest(int[] arr, int k)
    {
        return QuickSelect(arr, 0, arr.Length - 1, k - 1);
    }
    
    private int QuickSelect(int[] arr, int low, int high, int kIndex)
    {
        if (low == high)
            return arr[low];
        
        int pivotIndex = Partition(arr, low, high);
        
        if (kIndex == pivotIndex)
            return arr[kIndex];
        else if (kIndex < pivotIndex)
            return QuickSelect(arr, low, pivotIndex - 1, kIndex);
        else
            return QuickSelect(arr, pivotIndex + 1, high, kIndex);
    }
    
    private int Partition(int[] arr, int low, int high)
    {
        // Random pivot improves average case
        int randomIndex = low + new Random().Next(high - low + 1);
        Swap(arr, randomIndex, high);
        
        int pivot = arr[high];
        int i = low - 1;
        
        for (int j = low; j < high; j++)
        {
            if (arr[j] < pivot)
                Swap(arr, ++i, j);
        }
        
        Swap(arr, ++i, high);
        return i;
    }
    
    private void Swap(int[] arr, int i, int j)
    {
        int temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }
}
```

**Time:** O(n) average, O(n²) worst case (but rare with random pivot)

---

### Subtopic 4A.4: Multiple Missing/Duplicate Detection

**Foundation:** Extend cyclic sort to complex scenarios

```csharp
public class MissingDuplicateDetection
{
    // Problem: Find all missing numbers and all duplicates in 1..n array
    
    public (List<int> missing, List<int> duplicates) FindMissingDuplicates(int[] arr)
    {
        int n = arr.Length;
        
        // Step 1: Cyclic sort to place each number at correct position
        for (int i = 0; i < n; i++)
        {
            while (arr[i] != i + 1)
            {
                int correctPos = arr[i] - 1;
                
                if (arr[i] <= 0 || arr[i] > n)
                    break;  // Out of range
                
                if (arr[correctPos] == arr[i])
                    break;  // Duplicate found
                
                Swap(arr, i, correctPos);
            }
        }
        
        // Step 2: Find missing and duplicates
        var missing = new List<int>();
        var duplicates = new List<int>();
        
        for (int i = 0; i < n; i++)
        {
            if (arr[i] != i + 1)
            {
                if (arr[i] == arr[arr[i] - 1])
                    duplicates.Add(arr[i]);
                else
                    missing.Add(i + 1);
            }
        }
        
        return (missing, duplicates);
    }
    
    private void Swap(int[] arr, int i, int j)
    {
        int temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }
}
```

---

### Subtopic 4A.5: Quicksort vs. Heapsort vs. Mergesort

**Foundation:** Understanding when to use which

```csharp
public class SortingAlgorithmComparison
{
    // Time Complexities:
    // Quicksort:  O(n log n) avg, O(n²) worst
    // Heapsort:   O(n log n) guaranteed
    // Mergesort:  O(n log n) guaranteed
    // Insertion:  O(n²)
    // Shell sort: O(n^1.3) approximately
    
    // Space Complexities:
    // Quicksort:  O(log n) recursion stack
    // Heapsort:   O(1) extra
    // Mergesort:  O(n) extra
    // Insertion:  O(1) extra
    
    public class SortingDecisionTree
    {
        // Use when? (Decision tree)
        
        // Small array (< 50 elements)?
        // └─ YES: Insertion sort (cache friendly)
        
        // Need guaranteed O(n log n)?
        // ├─ YES (hard deadline): Heapsort
        // └─ NO: Quicksort (faster in practice)
        
        // Need stable sort?
        // ├─ YES: Mergesort
        // └─ NO: Quicksort or Heapsort
        
        // External sort (disk I/O)?
        // └─ Mergesort (sequential access)
        
        // Nearly sorted data?
        // └─ Insertion sort (only O(n) passes needed)
    }
}
```

---

## 📈 DAY 4B: KADANE'S ALGORITHM — EXTENDED MASTERY

### Subtopic 4B.1: Minimum Subarray Problem

**Foundation:** Mirror of maximum subarray

```csharp
public class MinimumSubarray
{
    // Problem: Find minimum sum subarray
    // Similar to Kadane's but tracking minima
    
    public int MinSubArray(int[] nums)
    {
        int minEndingHere = nums[0];
        int minSoFar = nums[0];
        
        for (int i = 1; i < nums.Length; i++)
        {
            // Decide extend or restart
            minEndingHere = Math.Min(nums[i], nums[i] + minEndingHere);
            minSoFar = Math.Min(minSoFar, minEndingHere);
        }
        
        return minSoFar;
    }
}
```

---

### Subtopic 4B.2: Maximum Sum Subarray with Constraint (Length)

**Foundation:** Add constraint to Kadane's

```csharp
public class MaxSubarrayWithLengthConstraint
{
    // Problem: Max subarray sum with at least k elements
    
    public int MaxSumWithMinLength(int[] nums, int minLength)
    {
        int n = nums.Length;
        if (n < minLength)
            return 0;
        
        // Compute max subarray sum ending at each position
        // with constraint that length >= minLength
        
        // dp[i] = max sum ending at i with at least minLength elements
        int[] dp = new int[n];
        int[] cumSum = new int[n];
        
        cumSum[0] = nums[0];
        for (int i = 1; i < n; i++)
            cumSum[i] = cumSum[i - 1] + nums[i];
        
        // Can only form subarray of minLength starting at index 0 to n-minLength
        for (int i = minLength - 1; i < n; i++)
        {
            if (i == minLength - 1)
                dp[i] = cumSum[i];  // First valid subarray
            else
                dp[i] = Math.Max(nums[i] + dp[i - 1], cumSum[i]);
        }
        
        int result = int.MinValue;
        for (int i = minLength - 1; i < n; i++)
            result = Math.Max(result, dp[i]);
        
        return result;
    }
}
```

---

### Subtopic 4B.3: Maximum Circular Subarray (Complete Proof)

**Foundation:** Handling circular constraint

```csharp
public class MaximumCircularSubarray
{
    // Problem: Find max subarray sum in circular array
    
    // Key insight: Either:
    // 1. Max subarray doesn't wrap (standard Kadane's)
    // 2. Max subarray wraps (= total - min subarray)
    
    public int MaxCircularSubarray(int[] nums)
    {
        int n = nums.Length;
        
        // Case 1: Standard max subarray (no wrap)
        int maxKadane = KadanesAlgorithm(nums);
        
        // Case 2: Max sum that wraps around
        // = total - minimum subarray sum
        
        // Negate array to find minimum
        for (int i = 0; i < n; i++)
            nums[i] = -nums[i];
        
        int maxNegated = KadanesAlgorithm(nums);
        int minSubarray = -maxNegated;
        
        // Restore array
        for (int i = 0; i < n; i++)
            nums[i] = -nums[i];
        
        int totalSum = nums.Sum();
        int maxCircular = totalSum - minSubarray;
        
        // Edge case: All negative (min is same as total)
        if (maxCircular == 0)
            return maxKadane;
        
        return Math.Max(maxKadane, maxCircular);
    }
    
    private int KadanesAlgorithm(int[] nums)
    {
        int maxHere = nums[0];
        int maxSoFar = nums[0];
        
        for (int i = 1; i < nums.Length; i++)
        {
            maxHere = Math.Max(nums[i], nums[i] + maxHere);
            maxSoFar = Math.Max(maxSoFar, maxHere);
        }
        
        return maxSoFar;
    }
}
```

**Critical Edge Case:** If min_subarray == total_sum (all negative), return max_kadane instead.

---

### Subtopic 4B.4: Maximum Product Subarray (Complete Strategy)

**Foundation:** Handling negatives systematically

```csharp
public class MaximumProductSubarray
{
    // Problem: Maximum product of subarray
    
    // Challenge: Negative × negative = positive
    // Strategy: Track both max and min
    
    public int MaxProduct(int[] nums)
    {
        if (nums.Length == 0)
            return 0;
        
        int maxEndingHere = nums[0];
        int minEndingHere = nums[0];  // Track negative too!
        int result = nums[0];
        
        for (int i = 1; i < nums.Length; i++)
        {
            int current = nums[i];
            
            // Compute new candidates
            int newMax = Math.Max(Math.Max(current, current * maxEndingHere), 
                                  current * minEndingHere);
            int newMin = Math.Min(Math.Min(current, current * maxEndingHere), 
                                  current * minEndingHere);
            
            maxEndingHere = newMax;
            minEndingHere = newMin;
            result = Math.Max(result, maxEndingHere);
        }
        
        return result;
    }
    
    public void TraceExample()
    {
        // Example: [2, 3, -2, 4]
        // i=0: maxHere=2, minHere=2, result=2
        // i=1: newMax=max(3, 3*2, 3*2)=6
        //      newMin=min(3, 3*2, 3*2)=3
        //      result=6
        // i=2: newMax=max(-2, -2*6, -2*3)=max(-2,-12,-6)=-2
        //      newMin=min(-2, -2*6, -2*3)=min(-2,-12,-6)=-12
        //      result=6
        // i=3: newMax=max(4, 4*(-2), 4*(-12))=max(4,-8,-48)=4
        //      newMin=min(4, 4*(-2), 4*(-12))=min(4,-8,-48)=-48
        //      result=6
    }
}
```

**Why track minEndingHere?** Because negative × large_negative = large_positive!

---

### Subtopic 4B.5: Stock Trading Applications

**Foundation:** Real-world Kadane applications

```csharp
public class StockTradingKadane
{
    // Problem 1: Buy once, sell once for max profit
    public int MaxProfit_SingleTransaction(int[] prices)
    {
        if (prices.Length < 2)
            return 0;
        
        int minPrice = prices[0];
        int maxProfit = 0;
        
        for (int i = 1; i < prices.Length; i++)
        {
            maxProfit = Math.Max(maxProfit, prices[i] - minPrice);
            minPrice = Math.Min(minPrice, prices[i]);
        }
        
        return maxProfit;
    }
    
    // Problem 2: Buy/sell multiple times
    public int MaxProfit_MultipleTransactions(int[] prices)
    {
        int maxProfit = 0;
        
        for (int i = 1; i < prices.Length; i++)
        {
            if (prices[i] > prices[i - 1])
                maxProfit += prices[i] - prices[i - 1];
        }
        
        return maxProfit;
    }
    
    // Problem 3: At most k transactions
    public int MaxProfit_KTransactions(int[] prices, int k)
    {
        if (prices.Length < 2 || k == 0)
            return 0;
        
        // Optimization: if k >= n/2, unlimited transactions
        if (k >= prices.Length / 2)
            return MaxProfit_MultipleTransactions(prices);
        
        // dp[i][j][0/1] = max profit using at most i transactions
        // ending at day j with 0 (hold) or 1 (own) stock
        
        int[][] buy = new int[k + 1][];  // buy[i][j] = max profit with i buys
        int[][] sell = new int[k + 1][]; // sell[i][j] = max profit with i sells
        
        for (int i = 0; i <= k; i++)
        {
            buy[i] = new int[prices.Length];
            sell[i] = new int[prices.Length];
            buy[i][0] = -prices[0];  // Buy on day 0
        }
        
        for (int i = 1; i <= k; i++)
        {
            for (int j = 1; j < prices.Length; j++)
            {
                buy[i][j] = Math.Max(buy[i][j - 1], 
                                    sell[i - 1][j - 1] - prices[j]);
                sell[i][j] = Math.Max(sell[i][j - 1], 
                                     buy[i][j - 1] + prices[j]);
            }
        }
        
        return sell[k][prices.Length - 1];
    }
}
```

---

### Subtopic 4B.6: Cooldown Period Handling

**Foundation:** Add constraint to stock trading

```csharp
public class StockTradingWithCooldown
{
    // Problem: Buy/sell but need 1-day cooldown after sell
    
    public int MaxProfit_WithCooldown(int[] prices)
    {
        if (prices.Length < 2)
            return 0;
        
        int hold = -prices[0];      // Holding stock
        int sold = 0;               // Just sold (in cooldown)
        int rest = 0;               // Resting (available to buy)
        
        for (int i = 1; i < prices.Length; i++)
        {
            int prevHold = hold;
            int prevSold = sold;
            int prevRest = rest;
            
            // Actions:
            // hold: buy today OR hold from yesterday
            // sold: sell today (from holding yesterday)
            // rest: stay in rest OR complete cooldown from sold
            
            hold = Math.Max(prevHold, prevRest - prices[i]);
            sold = prevHold + prices[i];
            rest = Math.Max(prevRest, prevSold);
        }
        
        return Math.Max(sold, rest);
    }
}
```

---

## 🔁 DAY 5: FAST/SLOW POINTERS — EXTENDED MASTERY

### Subtopic 5.1: Mathematical Proof of Floyd's Algorithm

**Foundation:** Why it works mathematically

```csharp
public class FloydsMathematicalProof
{
    /*
    THEOREM: If cycle exists, slow and fast pointers must meet
    
    PROOF:
    Let:
    - L = distance from head to cycle start
    - C = cycle length
    - At meeting point, slow traveled S steps, fast traveled F steps
    
    Since they move at constant rates:
    - F = 2S (fast is 2x slow)
    - S ≡ F (mod C) [both in cycle at meeting]
    
    From F = 2S:
    2S ≡ S (mod C)
    S ≡ 0 (mod C)
    
    This means S is multiple of C, so they meet!
    
    MEETING POINT PROPERTY:
    Distance from meeting point to cycle start = L
    (This is why we reset one pointer to head)
    */
    
    public class ProofExample
    {
        // Example: Head → 1 → 2 → 3 → 4 → 5 → 3 (cycle)
        // L = 3 (head to cycle start)
        // C = 3 (cycle length: 3→4→5→3)
        
        // Slow path: Head → 1 → 2 → 3 → 4 → 5 → 3 → 4 → 5 (meets fast at 3)
        // Fast path: Head → 2 → 4 → 5 → 3 → 5 → 4 → 3 (meets slow at 3)
        
        // Both meet at distance 3 from cycle start
        // Resetting slow to head and moving both 1 step:
        // Slow: Head → 1 → 2 → 3 (cycle start)
        // Fast: 3 → 4 → 5 → 3 (cycle start)
        // They meet at cycle start!
    }
}
```

---

### Subtopic 5.2: Cycle Length Detection

**Foundation:** Finding how long the cycle is

```csharp
public class CycleLengthDetection
{
    // Find length of cycle in linked list
    
    public int GetCycleLength(ListNode head)
    {
        ListNode slow = head, fast = head;
        
        // Find meeting point
        while (fast != null && fast.Next != null)
        {
            slow = slow.Next;
            fast = fast.Next.Next;
            
            if (slow == fast)
                break;
        }
        
        if (fast == null || fast.Next == null)
            return 0;  // No cycle
        
        // Count cycle length
        int length = 1;
        ListNode current = slow.Next;
        
        while (current != slow)
        {
            current = current.Next;
            length++;
        }
        
        return length;
    }
}
```

---

### Subtopic 5.3: Palindrome Detection in Linked List

**Foundation:** Combining fast/slow with reversal

```csharp
public class LinkedListPalindromeDetection
{
    // Approach: Find middle, reverse second half, compare
    
    public bool IsPalindrome(ListNode head)
    {
        if (head == null || head.Next == null)
            return true;
        
        // Step 1: Find middle using slow/fast
        ListNode slow = head, fast = head;
        ListNode prev = null;
        
        while (fast != null && fast.Next != null)
        {
            prev = slow;
            slow = slow.Next;
            fast = fast.Next.Next;
        }
        
        // Break list at middle
        if (prev != null)
            prev.Next = null;
        
        // Step 2: Reverse second half
        ListNode reversed = ReverseList(slow);
        
        // Step 3: Compare first and second halves
        ListNode p1 = head, p2 = reversed;
        bool isPalin = true;
        
        while (p1 != null && p2 != null)
        {
            if (p1.Val != p2.Val)
            {
                isPalin = false;
                break;
            }
            p1 = p1.Next;
            p2 = p2.Next;
        }
        
        // Optional: restore original structure
        // (not required for function correctness)
        
        return isPalin;
    }
    
    private ListNode ReverseList(ListNode head)
    {
        ListNode prev = null;
        while (head != null)
        {
            ListNode next = head.Next;
            head.Next = prev;
            prev = head;
            head = next;
        }
        return prev;
    }
}
```

---

### Subtopic 5.4: Remove Nth Node from End

**Foundation:** Using fast/slow for position calculation

```csharp
public class RemoveNthNodeFromEnd
{
    // Problem: Remove n-th node from end in one pass
    
    // Approach: Use gap of n between slow and fast
    
    public ListNode RemoveNthFromEnd(ListNode head, int n)
    {
        // Create dummy node to handle head removal
        var dummy = new ListNode(0);
        dummy.Next = head;
        
        ListNode fast = dummy, slow = dummy;
        
        // Create gap of n between pointers
        for (int i = 0; i < n; i++)
        {
            fast = fast.Next;
            if (fast == null)
                return dummy.Next;  // n > length
        }
        
        // Move both until fast reaches end
        while (fast.Next != null)
        {
            fast = fast.Next;
            slow = slow.Next;
        }
        
        // Remove node
        slow.Next = slow.Next.Next;
        
        return dummy.Next;
    }
}
```

**Key:** Dummy node simplifies head removal case.

---

### Subtopic 5.5: Rotate List

**Foundation:** Fast/slow for rotation

```csharp
public class RotateList
{
    // Problem: Rotate list right by k positions
    // Example: 1→2→3→4→5, k=2 → 4→5→1→2→3
    
    public ListNode RotateRight(ListNode head, int k)
    {
        if (head == null || head.Next == null)
            return head;
        
        // Find length
        int length = 0;
        ListNode temp = head;
        while (temp != null)
        {
            length++;
            temp = temp.Next;
        }
        
        k = k % length;  // Handle k > length
        if (k == 0)
            return head;
        
        // Find new tail (length - k - 1 from head)
        ListNode slow = head, fast = head;
        
        for (int i = 0; i < k; i++)
            fast = fast.Next;
        
        while (fast.Next != null)
        {
            slow = slow.Next;
            fast = fast.Next;
        }
        
        // Rotate
        ListNode newHead = slow.Next;
        slow.Next = null;
        fast.Next = head;
        
        return newHead;
    }
}
```

---

### Subtopic 5.6: Happy Number & Cycle Detection in Sequences

**Foundation:** Floyd's applied to number sequences

```csharp
public class HappyNumberDetection
{
    // Problem: Determine if number is "happy"
    // Happy = eventually reach 1 by repeatedly summing squares of digits
    // Unhappy = stuck in cycle
    
    public bool IsHappy(int n)
    {
        // Use Floyd's cycle detection on sequence
        int slow = n;
        int fast = n;
        
        do
        {
            slow = SumOfSquares(slow);         // 1 step
            fast = SumOfSquares(SumOfSquares(fast));  // 2 steps
            
            if (fast == 1)
                return true;  // Happy
        } while (slow != fast);  // Cycle detected
        
        return false;  // Unhappy (cycle found)
    }
    
    private int SumOfSquares(int n)
    {
        int sum = 0;
        while (n > 0)
        {
            int digit = n % 10;
            sum += digit * digit;
            n /= 10;
        }
        return sum;
    }
    
    public void TraceExample()
    {
        // Example: n = 7
        // 7 → 49 → 97 → 130 → 10 → 1 (Happy!)
        
        // Example: n = 2
        // 2 → 4 → 16 → 37 → 58 → 89 → 145 → 42 → 20 → 4 (Cycle!)
    }
}
```

---

### Subtopic 5.7: Swap Nodes in Pairs

**Foundation:** Pointer manipulation patterns

```csharp
public class SwapNodesInPairs
{
    // Problem: Swap adjacent pairs
    // 1→2→3→4 → 2→1→4→3
    
    public ListNode SwapPairs(ListNode head)
    {
        var dummy = new ListNode(0);
        dummy.Next = head;
        
        ListNode prev = dummy;
        
        while (prev.Next != null && prev.Next.Next != null)
        {
            ListNode first = prev.Next;
            ListNode second = prev.Next.Next;
            
            // Swap
            prev.Next = second;
            first.Next = second.Next;
            second.Next = first;
            
            // Move to next pair
            prev = first;
        }
        
        return dummy.Next;
    }
}
```

---

### Subtopic 5.8: Reverse List in Groups of K

**Foundation:** Partition-based list operations

```csharp
public class ReverseListInGroupsOfK
{
    // Problem: Reverse every k nodes
    // 1→2→3→4→5, k=2 → 2→1→4→3→5
    
    public ListNode ReverseKGroup(ListNode head, int k)
    {
        var dummy = new ListNode(0);
        dummy.Next = head;
        ListNode prevGroup = dummy;
        
        while (true)
        {
            // Check if k nodes exist
            ListNode kthNode = GetKthNode(prevGroup, k);
            if (kthNode == null)
                break;
            
            ListNode nextGroupStart = kthNode.Next;
            
            // Reverse current group
            ReverseGroup(prevGroup, nextGroupStart);
            
            prevGroup = GetKthNode(prevGroup, k);
        }
        
        return dummy.Next;
    }
    
    private ListNode GetKthNode(ListNode node, int k)
    {
        while (node != null && k-- > 0)
            node = node.Next;
        return node;
    }
    
    private void ReverseGroup(ListNode prevGroupEnd, ListNode nextGroupStart)
    {
        ListNode current = prevGroupEnd.Next;
        ListNode prev = nextGroupStart;
        
        while (current != nextGroupStart)
        {
            ListNode next = current.Next;
            current.Next = prev;
            prev = current;
            current = next;
        }
        
        ListNode temp = prevGroupEnd.Next;
        prevGroupEnd.Next = prev;
    }
}
```

---

## ✅ SUMMARY: MASTERY INTEGRATION

**This extended guide adds 38+ subtopics across 5 days:**

- Day 1 (Hash): 10 subtopics (collision, load factor, custom hashing, rolling hash, k-sum, 2-sum vs., memory, multi-key, robustness)
- Day 2 (Stack): 7 subtopics (stock span, daily temp, sum subarrays, removing digits, amortized analysis, deque, visibility)
- Day 3 (Intervals): 5 subtopics (scheduling optimization, weighted DP, sweep line, room allocation, conflict detection)
- Day 4A (Partition): 5 subtopics (quicksort, 3-way partition, k-th smallest, missing/duplicate, sort comparison)
- Day 4B (Kadane): 6 subtopics (minimum, constrained, circular, product, stock trading, cooldown)
- Day 5 (Pointers): 8 subtopics (Floyd's proof, cycle length, palindrome, remove nth, rotate, happy numbers, swap pairs, reverse groups)

**Integration Strategy:**
- Each day's extended topics build on syllabus
- Provide production-ready C# code
- Connect to real-world systems
- Prepare for advanced interview questions

---

**Status:** Extended Mastery Guide Complete  
**Format:** Production-Ready Code + Deep Understanding  
**Generated:** January 08, 2026 | **System:** v12


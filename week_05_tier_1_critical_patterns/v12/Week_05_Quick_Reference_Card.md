# 🎯 WEEK 05 QUICK REFERENCE CARD — Patterns at a Glance

**Week:** 5 | **Tier:** Tier 1 Critical Patterns  
**Format:** One-page quick lookup (print-friendly)

---

## 📊 PATTERN COMPARISON MATRIX

| Pattern | Time | Space | Use Case | Key Insight | Interview % |
|---------|------|-------|----------|-------------|------------|
| **Hash** | O(n) avg | O(n) | Complement, frequency | O(1) lookup | 25-30% |
| **Stack** | O(n) | O(n) | Next greater, histograms | Monotonic invariant | 15-20% |
| **Intervals** | O(n log n) | O(1) | Merge, scheduling | Sort then scan | 10-15% |
| **Partition** | O(n) | O(1) | Segregate, sort | In-place regions | 5-10% |
| **Kadane** | O(n) | O(1) | Max subarray, DP | Track local/global | 5-8% |
| **Pointers** | O(n) | O(1) | Cycles, middle | Two speeds collide | 8-12% |

---

## 🔍 PATTERN IDENTIFICATION CHEAT SHEET

**When You See → Think:**

- "Find pair summing to X" → **Hash complement**
- "Count frequencies" → **Hash frequency**
- "Next greater/smaller" → **Monotonic stack**
- "Trapping water/area" → **Monotonic stack**
- "Merge intervals" → **Sort + scan**
- "Scheduling" → **Greedy intervals**
- "Segregate 0/1/2" → **Partition (Dutch flag)**
- "Max subarray" → **Kadane's DP**
- "Cycle in list" → **Fast/slow pointers**
- "Linked list middle" → **Fast/slow pointers**

---

## 💻 IMPLEMENTATION TEMPLATES

### Hash Complement (Two-Sum Pattern)
```csharp
var seen = new HashSet<int>();
foreach (int num in nums)
{
    if (seen.Contains(target - num)) return true;
    seen.Add(num);
}
```

### Monotonic Stack (Next Greater)
```csharp
var stack = new Stack<int>();
for (int i = n-1; i >= 0; i--)
{
    while (stack.Count > 0 && stack.Peek() <= nums[i])
        stack.Pop();
    result[i] = stack.Count > 0 ? stack.Peek() : -1;
    stack.Push(nums[i]);
}
```

### Interval Merge (Sort + Scan)
```csharp
Array.Sort(intervals, (a, b) => a[0].CompareTo(b[0]));
int end = intervals[0][1];
foreach (var interval in intervals.Skip(1))
{
    if (interval[0] <= end)
        end = Math.Max(end, interval[1]);
    else { /* save and restart */ }
}
```

### Dutch National Flag (Partition)
```csharp
int left = 0, mid = 0, right = n-1;
while (mid <= right)
{
    if (arr[mid] == 0) Swap(left++, mid++);
    else if (arr[mid] == 1) mid++;
    else Swap(mid, right--);
}
```

### Kadane's Algorithm
```csharp
int maxHere = nums[0], maxSoFar = nums[0];
for (int i = 1; i < n; i++)
{
    maxHere = Math.Max(nums[i], nums[i] + maxHere);
    maxSoFar = Math.Max(maxSoFar, maxHere);
}
```

### Floyd's Cycle Detection
```csharp
ListNode slow = head, fast = head;
while (fast != null && fast.Next != null)
{
    slow = slow.Next;
    fast = fast.Next.Next;
    if (slow == fast) return true; // cycle
}
```

---

## 🎯 COMPLEXITY QUICK LOOKUP

| Problem | Algorithm | Time | Space |
|---------|-----------|------|-------|
| Two-Sum | Hash map | O(n) | O(n) |
| Top-K Freq | Bucket sort | O(n) | O(n) |
| Next Greater | Stack | O(n) | O(n) |
| Trapping Water | Stack | O(n) | O(n) |
| Merge Intervals | Sort+merge | O(n log n) | O(1) |
| Merge K Lists | Heap | O(N log k) | O(k) |
| Sort Colors | Partition | O(n) | O(1) |
| Max Subarray | DP | O(n) | O(1) |
| Cycle Detect | Floyd | O(n) | O(1) |

---

## 🏆 TRADE-OFF DECISION TREE

```
Problem → Segregate elements?
├─ YES → In-place required?
│   ├─ YES → Partition (O(1) space)
│   └─ NO → Sorting (simpler code)
└─ NO → Find optimal subarray?
    ├─ YES → Can end anywhere? 
    │   ├─ YES → Kadane's (max subarray)
    │   └─ NO → DP (constrained)
    └─ NO → Track something?
        ├─ YES → Hash (frequency, pairs)
        └─ NO → Stack? (invariant)
            ├─ YES → Monotonic stack
            └─ NO → Pointers? (cycles, middle)
                ├─ YES → Fast/slow
                └─ NO → Check problem type
```

---

## 📝 MISTAKE PREVENTION CHECKLIST

### Hash Patterns
- [ ] Considered load factor implications?
- [ ] Handled collisions correctly?
- [ ] Edge case: empty input, single element?

### Monotonic Stack
- [ ] Maintained invariant (increasing/decreasing)?
- [ ] Updated before popping?
- [ ] Handled end-of-array correctly?

### Intervals
- [ ] Sorted by correct dimension?
- [ ] Checked overlapping condition correctly?
- [ ] Updated end value before saving?

### Partition
- [ ] Didn't increment mid after right swap?
- [ ] Checked termination condition?
- [ ] All elements processed?

### Kadane's
- [ ] Updated global max inside loop?
- [ ] Initialized both local and global?
- [ ] Handled all-negative array?

### Fast/Slow Pointers
- [ ] Checked null before dereferencing fast.next?
- [ ] Reset pointer correctly for cycle start?
- [ ] Handled empty/single-element list?

---

## 🧪 TEST CASE GENERATION

For each pattern, test these:

### Edge Cases
- Empty input
- Single element
- All same elements
- All negative/all positive
- Duplicates (if applicable)

### Boundary Cases
- Minimum valid input (size 2 for 2-sum)
- Maximum expected input
- Powers of 2 (often hide bugs)

### Special Cases
- Pattern-specific (cycles for pointers, overlap for intervals)
- Worst-case (many duplicates, all overlapping)

---

## 💡 OPTIMIZATION TECHNIQUES

| Technique | Applies To | Improvement |
|-----------|-----------|-------------|
| Early termination | All | Reduce iterations |
| Two-pointer | Hash, intervals | O(1) space |
| Bucket sort | Frequency | O(n) vs O(n log n) |
| Amortized analysis | Stack | Proves O(n) not O(n²) |
| Greedy choice | Intervals, DP | O(n log n) vs brute force |
| Stream processing | Any | Handle infinite data |

---

## 🎓 INTERVIEW PHRASES TO USE

- "Trade-off between space and time..."
- "Invariant maintained is..."
- "Worst case complexity is..."
- "Edge case to consider..."
- "Optimal substructure means..."
- "Amortized analysis shows..."
- "I'd first optimize for correctness, then..."

---

## ❌ COMMON MISTAKES TO AVOID

1. **Hash:** Forgot to check collision
2. **Stack:** Incremented counter before popping
3. **Intervals:** Wrong sort order (end vs. start)
4. **Partition:** Incremented mid after right swap
5. **Kadane:** Didn't track global max inside loop
6. **Pointers:** Didn't check fast.next before dereferencing

---

## 🚀 AFTER-WEEK PROGRESSION

**What to Study Next:**
- **Week 6:** Strings (pattern matching, manipulation)
- **Week 7:** Arrays (advanced, 2D problems)
- **Week 8:** Linked lists (advanced operations)
- **Week 9:** Trees (traversal, BST, structures)
- **Week 10+:** Graphs, advanced DP

**How Week 05 Prepares:**
- Hash → Use in graph algorithms
- Stack → Use in DFS, expression evaluation
- Intervals → Use in scheduling optimization
- Partition → Quicksort basis
- Kadane → Foundation for advanced DP
- Pointers → Linked list manipulations

---

## 📚 RESOURCE QUICK LINKS

- **LeetCode:** Tag problems by pattern (filter: hash, stack, etc.)
- **Discussion:** Read comments on LeetCode solutions
- **Visualization:** Use algorithm visualizer tools
- **Practice:** Code each operation 5+ times until muscle memory

---

**Print This Card → Keep With You → Reference During Problems**

**Status:** Quick Reference Ready  
**Generated:** January 08, 2026 | **System:** v12


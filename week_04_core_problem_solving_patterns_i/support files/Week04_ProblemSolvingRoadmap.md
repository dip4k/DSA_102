# 🗓️ Week 04 Problem-Solving Roadmap: Progression & Decision Matrix

**Status:** Training Coach Resource  
**Audience:** Students working through practice problems  
**Purpose:** Guide problem selection, difficulty progression, and pattern recognition

---

## 🎯 Overall Problem-Solving Strategy for Week 04

### Stage 1: Pattern Recognition (Foundation)
**Goal:** Solve canonical problem for each of 4 patterns  
**Time:** Days 1-2  
**Outcome:** You can identify pattern, solve on first attempt with slight hesitation

**Pattern Recognition Checklist:**
- [ ] Merge sorted arrays (two-pointer same-direction)
- [ ] Sliding window with running sum (fixed-size)
- [ ] Longest substring without repeating (variable-size)
- [ ] Merge sort or majority element (divide-conquer)
- [ ] Binary search on sorted array (classic)

### Stage 2: Variation & Constraint (Mastery)
**Goal:** Solve 2-3 variations with new constraints per pattern  
**Time:** Days 3-4  
**Outcome:** You modify algorithm for new constraints without recoding logic

**Variations to Explore:**
- Two-pointer: merge, remove, water, palindrome, complement
- Sliding fixed: running sum, max/min, best average
- Sliding variable: substring, at-most K, minimum window
- Divide-conquer: merge sort, majority, inversions
- Binary search: sorted array, answer space, rotated array

### Stage 3: Integration & System (Mastery+)
**Goal:** Solve problems combining patterns or adding system design  
**Time:** Day 5  
**Outcome:** You see multiple patterns in one problem, decide which to combine

**Integration Examples:**
- Two-pointer + binary search (merge + search)
- Sliding window + divide-conquer (window within divide)
- Divide-conquer + two-pointer (merge sorted windows)
- Binary search + feasibility (answer space optimization)

---

## 📊 Difficulty Progression by Pattern

### Two-Pointer

| Level | Problem | Topic | Key Skill |
|-------|---------|-------|-----------|
| 🟢 Easy | Merge Sorted Array | Same-direction merge | Merging with invariant |
| 🟢 Easy | Remove Duplicates | In-place modification | Write pointer tracking |
| 🟡 Medium | Container Most Water | Opposite-direction | Area calculation, pointer logic |
| 🟡 Medium | Two Sum II | Sorted complement | Why O(n) works here |
| 🔴 Hard | Trapping Rain Water | Elevation mapping | Water level calculation |

**Progression Logic:** Easy teaches same-direction (simpler), medium teaches opposite-direction (harder), hard combines with computation.

---

### Sliding Window — Fixed Size

| Level | Problem | Topic | Key Skill |
|-------|---------|-------|-----------|
| 🟢 Easy | Max Avg Subarray | Running sum | Incremental O(n) |
| 🟡 Medium | Max in Window | Deque optimization | Maintaining structure |
| 🟡 Medium | Best Time Buy Stock | Min tracking | Similar to running computation |
| 🟡 Medium | Sliding Window Median | Deque/heap | Maintaining statistics |

**Progression Logic:** Easy teaches basic sliding, medium teaches optimization with deques/heaps.

---

### Sliding Window — Variable Size

| Level | Problem | Topic | Key Skill |
|-------|---------|-------|-----------|
| 🟢 Easy | Longest Without Repeat | Character map | Index-based tracking |
| 🟡 Medium | At Most K Distinct | Frequency map | Constraint satisfaction |
| 🟡 Medium | Permutation in String | Fixed window variant | Frequency matching |
| 🔴 Hard | Minimum Window Substring | Expand/shrink | Complex shrinking logic |

**Progression Logic:** Easy teaches character tracking, medium teaches constraint handling, hard teaches complex state.

---

### Divide-Conquer

| Level | Problem | Topic | Key Skill |
|-------|---------|-------|-----------|
| 🟡 Medium | Merge Sort | Split/merge | Writing recurrence |
| 🟡 Medium | Majority Element | Find >n/2 | Combining results |
| 🔴 Hard | Count Inversions | Merge-based counting | Cross-inversion logic |
| 🔴 Hard | Max Subarray Sum | Split to find crossing max | Combining into answer |

**Progression Logic:** Medium teaches basic split/combine, hard teaches combining with computation.

---

### Binary Search

| Level | Problem | Topic | Key Skill |
|-------|---------|-------|-----------|
| 🟢 Easy | Binary Search | Classic | Maintaining [low, high) |
| 🟡 Medium | Minimize Maximum Load | Answer space | Feasibility checking |
| 🟡 Medium | Peak in Rotated | Sorted property detection | Where to recurse |
| 🔴 Hard | Allocate Books | Greedy + binary | Complex feasibility |

**Progression Logic:** Easy teaches classic, medium teaches answer space, hard teaches combination with greedy.

---

## 🎯 Common Problem-Solving Pitfalls & Fixes

### Pitfall 1: Wrong Invariant in Two-Pointer

**Problem:** Moving pointers without clear invariant
```
while left < right:
  left += 1  # Why? What does left point to now?
  right -= 1
```

**Fix:** State invariant explicitly
```
# Invariant: [0, left) processed, [right, n) processed
while left < right:
  # Process arr[left], arr[right]
  left += 1
  right -= 1
```

---

### Pitfall 2: Sliding Window Without Constraint Tracking

**Problem:** Adding to window but not tracking constraint state

**Fix:** Explicit constraint counter
```
distinct_count = 0
for right in range(n):
  if char_freq[arr[right]] == 0:
    distinct_count += 1
  char_freq[arr[right]] += 1
  
  while distinct_count > k:
    char_freq[arr[left]] -= 1
    if char_freq[arr[left]] == 0:
      distinct_count -= 1
    left += 1
```

---

### Pitfall 3: Divide-Conquer Base Case Missing

**Problem:** Recursion never terminates or handles size-1 arrays wrong

**Fix:** Explicit base case
```
if left == right:
  return arr[left]  # Single element

if left > right:
  return 0  # Empty (for sum-like operations)

# Recursive case follows
```

---

### Pitfall 4: Binary Search Off-by-One

**Problem:** Using wrong mid calculation or termination

**Fix:** Safe mid calculation
```
mid = low + (high - low) // 2  # Avoids overflow

while low < high:  # [low, high) invariant
  if feasible(mid):
    high = mid  # mid might be answer
  else:
    low = mid + 1
```

---

### Pitfall 5: Not Recognizing Pattern Combination

**Problem:** Solving each problem from scratch instead of combining

**Fix:** Ask pattern questions:
- "Is this sorted?" → two-pointer or binary search
- "Is this contiguous?" → sliding window
- "Can I split it?" → divide-conquer
- "Is it monotone?" → binary search on answer

---

## 🔄 Pattern Decision Matrix: How to Choose

Given a problem, which pattern(s) fit?

```
Does the problem ask about:

├─ "Merge or combine two sorted"?
│  └─ Use Two-Pointer same-direction
│
├─ "Max/min in contiguous subarray"?
│  └─ Use Sliding Window (fixed or variable)
│
├─ "Find complement or opposite direction"?
│  └─ Use Two-Pointer opposite-direction
│
├─ "Recursively solve subproblems"?
│  └─ Use Divide-Conquer
│
├─ "Optimize within monotone constraint"?
│  └─ Use Binary Search on answer space
│
└─ "Unsure"?
   └─ Check: sorted? Contiguous? Complement? Recursive? Monotone?
      └─ These hints suggest pattern family
```

---

## 📝 Per-Pattern Study Checklist

### Two-Pointer
- [ ] Explain what "maintaining invariant" means
- [ ] Code same-direction merge from scratch
- [ ] Code opposite-direction water from scratch
- [ ] Understand why water is O(n) not O(n²)
- [ ] Trace manually on paper
- [ ] Know when invariant fails

### Sliding Window (Fixed)
- [ ] Understand why O(n) not O(nk)
- [ ] Implement running sum/avg
- [ ] Implement max in window with deque
- [ ] Know what deque operations preserve
- [ ] Trace on paper showing window moves

### Sliding Window (Variable)
- [ ] Write constraint-checking function
- [ ] Implement expand/shrink correctly
- [ ] Understand when to shrink
- [ ] Know state to track (frequency map)
- [ ] Trace showing window grows/shrinks

### Divide-Conquer
- [ ] Write recurrence relation
- [ ] Identify base case
- [ ] Implement split/combine
- [ ] Calculate complexity from recurrence
- [ ] Understand why it's better than brute force

### Binary Search
- [ ] Implement classic binary search
- [ ] Understand [low, high) invariant
- [ ] Write feasibility check for answer space
- [ ] Test on edge cases (not found, at boundaries)
- [ ] Identify when problem is monotone

---

## 🏁 Weekly Milestone Targets

**After Day 1:** Solve 3 two-pointer problems without reference  
**After Day 2:** Solve 2-3 sliding window fixed without reference  
**After Day 3:** Solve 2-3 sliding window variable without reference  
**After Day 4:** Solve 2 divide-conquer problems (understand recurrence)  
**After Day 5:** Solve 2 binary search problems (identify monotone)  
**Integration Day:** Solve 1-2 problems mixing patterns  

---

**Status:** Week 04 Problem-Solving Roadmap Complete  
**Next:** Daily Progress Checklist  
**Practice Time:** 12-15 hours for 20-25 problems
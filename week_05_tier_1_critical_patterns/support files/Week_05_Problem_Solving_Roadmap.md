# 🗓️ Week 05 Problem-Solving Roadmap: Progression & Decision Matrix

**Audience:** Students working through practice problems  
**Purpose:** Guide problem selection, difficulty progression, and pattern recognition

---

## 🎯 Overall Problem-Solving Strategy for Week 05

### Stage 1: Pattern Recognition (Foundation)
**Goal:** Solve canonical problems for each of 6 patterns  
**Time:** Days 1-2  
**Outcome:** You can identify pattern, solve on first attempt with slight hesitation

**Pattern Recognition Checklist:**
- [ ] Two-sum (hash complement)
- [ ] Next greater element (monotonic stack)
- [ ] Merge intervals (greedy interval)
- [ ] Dutch National Flag (partition)
- [ ] Maximum subarray (Kadane's)
- [ ] Cycle detection (fast-slow)

### Stage 2: Variation & Constraint (Mastery)
**Goal:** Solve 2-3 variations with new constraints per pattern  
**Time:** Days 3-4  
**Outcome:** You modify algorithm for new constraints without recoding logic

**Variations to Explore:**
- Hash: anagrams, frequency, membership, two-sum variations
- Stack: next element, stock span, rain water, histogram
- Intervals: merge, insert, k-lists, calendar scheduling
- Partition: Dutch flag, cyclic sort, find missing/duplicate
- Kadane: max sum, max product, circular, with constraints
- Fast-slow: cycle start, midpoint, palindrome, happy number

### Stage 3: Integration & System (Mastery+)
**Goal:** Solve problems combining patterns or adding system design  
**Time:** Day 5  
**Outcome:** You see multiple patterns in one problem, decide which to combine

**Integration Examples:**
- Hash + partition for duplicates
- Stack + intervals for bracket nesting
- Kadane + constraints for budgeted problems
- Fast-slow + hash for cycle value detection

---

## 📊 Difficulty Progression by Pattern

### Hash Map & Hash Set

| Level | Problem | Topic | Key Skill |
|-------|---------|-------|-----------|
| 🟢 Easy | Two Sum | Complement lookup | Fast O(1) retrieval |
| 🟢 Easy | Valid Anagram | Frequency counting | Map key-value pairs |
| 🟡 Medium | Group Anagrams | Frequency-based grouping | Dict of lists |
| 🟡 Medium | Top K Frequent | Heap + frequency | Combining patterns |
| 🔴 Hard | LRU Cache | Hash + linked list | Design with constraints |
| 🔴 Hard | Longest Consecutive | Hash + smart iteration | Avoid redundant work |

**Progression Logic:** Easy problems teach basics (lookup), medium adds grouping, hard combines with other structures.

---

### Monotonic Stack

| Level | Problem | Topic | Key Skill |
|-------|---------|-------|-----------|
| 🟢 Easy | Next Greater Element | Stack order | Popping on violation |
| 🟡 Medium | Stock Span | Span calculation | Combining with stack |
| 🟡 Medium | Trapping Rain Water | Height mapping | Water level calculation |
| 🔴 Hard | Largest Rectangle | Histogram scanning | Area calculation logic |
| 🔴 Hard | Remove K Digits | Greedy + stack | When to remove |

**Progression Logic:** Easy teaches stack logic, medium teaches state tracking, hard combines with area/value calculations.

---

### Merge Operations & Intervals

| Level | Problem | Topic | Key Skill |
|-------|---------|-------|-----------|
| 🟢 Easy | Merge Sorted Array | Two-pointer merge | Backtracking from end |
| 🟡 Medium | Merge Intervals | Sort + single pass | Greedy merging |
| 🟡 Medium | Insert Interval | Smart insertion | Merging on the fly |
| 🟡 Medium | Meeting Rooms | Overlap detection | Just sorting |
| 🔴 Hard | Merge K Lists | Heap-based merge | Streaming merge |
| 🔴 Hard | Skyline Problem | Events + priority | Complex interval logic |

**Progression Logic:** Easy teaches two-pointer, medium teaches sorting + greedy, hard teaches heap/event processing.

---

### Partition & Cyclic Sort

| Level | Problem | Topic | Key Skill |
|-------|---------|-------|-----------|
| 🟢 Easy | Move Zeroes | Partition variant | Two-pointer rearrange |
| 🟡 Medium | Dutch National Flag | 0-1-2 sorting | Three pointers |
| 🟡 Medium | Find Missing Number | Cyclic sort insight | Position-based finding |
| 🟡 Medium | Find Duplicate | Array as index | Value-as-index trick |
| 🔴 Hard | First Missing Positive | Cyclic sort variant | 1..n not 0..n-1 |

**Progression Logic:** Easy teaches partition basics, medium teaches cyclic sort, hard teaches position mapping variants.

---

### Kadane's Algorithm

| Level | Problem | Topic | Key Skill |
|-------|---------|-------|-----------|
| 🟢 Easy | Max Subarray Sum | Basic Kadane | Local vs global max |
| 🟡 Medium | Max Product Subarray | Track min too | Handling negatives |
| 🟡 Medium | Circular Max | Total - min approach | Circular variant |
| 🟡 Medium | Max Subarray with K Elements | Constrained | Adding constraint |
| 🔴 Hard | Best Time to Buy/Sell | Price pairs | Single transaction |

**Progression Logic:** Easy teaches basic DP, medium teaches variants, hard teaches real-world applications.

---

### Fast-Slow Pointers

| Level | Problem | Topic | Key Skill |
|-------|---------|-------|-----------|
| 🟢 Easy | Linked List Cycle | Floyd's algorithm | Meeting point finding |
| 🟡 Medium | Cycle Start Detection | Pointer reset | Math of cycle start |
| 🟡 Medium | Middle of Linked List | Midpoint finding | Even/odd handling |
| 🟡 Medium | Palindrome List | Reverse + compare | Using midpoint |
| 🔴 Hard | Happy Number | Cycle in sequences | Non-linked-list cycle |

**Progression Logic:** Easy teaches cycle detection, medium teaches finding cycle properties, hard generalizes beyond linked lists.

---

## 🎯 Common Problem-Solving Pitfalls & Fixes

### Pitfall 1: Not Checking Hash Key Existence

**Problem:** KeyNotFoundException or wrong logic
```
if map[key] > 0:  # Crashes if key not present!
```

**Fix:** Always check existence first
```
if key in map and map[key] > 0:
```

---

### Pitfall 2: Forgetting Monotonic Stack Invariant

**Problem:** Stack has random elements, logic fails
```
if arr[i] > stack[-1]:  # Don't just compare, need to POP
  # Still wrong
```

**Fix:** Pop all violating elements FIRST
```
while stack and arr[i] > stack[-1]:
  pop_and_process()
stack.push(i)  # Then push new element
```

---

### Pitfall 3: Sorting Intervals Wrong

**Problem:** Sorted by end time, merging fails
```
intervals.sort(key=lambda x: x[1])  # WRONG! Sort by start
```

**Fix:** Sort by start time, then merge from left to right
```
intervals.sort(key=lambda x: x[0])
```

---

### Pitfall 4: Partition with Duplicate Checks

**Problem:** Trying to swap but already at right place
```
while arr[i] != i+1:  # Infinite loop if arr[i] == arr[arr[i]-1]
  swap(i, arr[i]-1)
```

**Fix:** Check if target position already has right value
```
if arr[i] != i+1 and arr[arr[i]-1] != arr[i]:
  swap(i, arr[i]-1)
else:
  i += 1  # Skip if already correct or duplicate
```

---

### Pitfall 5: Kadane's Not Handling All Negative

**Problem:** Returns 0 instead of max negative
```
max_so_far = 0
max_ending_here = 0
for num in arr:
  max_ending_here = max(num, max_ending_here + num)
  max_so_far = max(max_so_far, max_ending_here)
# Returns 0 for [-5,-2,-8]
```

**Fix:** Initialize with first element
```
max_so_far = arr[0]
max_ending_here = arr[0]
for i in range(1, len(arr)):
  ...
```

---

### Pitfall 6: Fast-Slow with No Null Check

**Problem:** Null pointer exception when fast reaches end
```
while fast and fast.next:  # What about fast.next.next?
  fast = fast.next
  slow = slow.next
```

**Fix:** Check all dereferences
```
while fast and fast.next and fast.next.next:
  fast = fast.next.next
  slow = slow.next
```

---

## 🔄 Pattern Decision Matrix: How to Choose

Given a problem, which pattern(s) fit?

```
Does the problem ask about:

├─ "Find pairs/elements with property"?
│  └─ Use Hash (complement lookup, frequency)
│
├─ "Next/Previous element greater/smaller"?
│  └─ Use Monotonic Stack
│
├─ "Merge overlapping ranges"?
│  └─ Use Intervals (sort + greedy)
│
├─ "Find missing/duplicate in 1..n"?
│  └─ Use Partition / Cyclic Sort
│
├─ "Maximum/minimum subarray sum"?
│  └─ Use Kadane's Algorithm
│
├─ "Linked list cycle/midpoint"?
│  └─ Use Fast-Slow Pointers
│
├─ "Combining two or more above"?
│  └─ Use integration (multiple patterns)
│
└─ "Unsure"?
   └─ Check: sorted? Space constraint? Streaming? Multiple solutions?
      └─ These hints suggest pattern family
```

---

## 📝 Per-Pattern Study Checklist

### Hash Map & Hash Set
- [ ] Understand hash function, collision, load factor
- [ ] Code two-sum with no reference
- [ ] Explain why O(1) average not guaranteed
- [ ] Solve frequency counting problem
- [ ] Recognize when hash beats sorting
- [ ] Combine hash with other structures (heap, list)

### Monotonic Stack
- [ ] Draw stack state evolution for example
- [ ] Explain why we pop on violation
- [ ] Code next greater element from scratch
- [ ] Solve stock span and rain water
- [ ] Understand pop → process → push sequence
- [ ] Combine stack with height/value tracking

### Merge Operations & Intervals
- [ ] Sort intervals by start time confidently
- [ ] Explain greedy merging correctness
- [ ] Code merge sorted arrays backward
- [ ] Solve merge intervals and insert interval
- [ ] Recognize interval scheduling in real systems
- [ ] Combine sorting with greedy logic

### Partition & Cyclic Sort
- [ ] Explain three pointers in Dutch flag
- [ ] Code cyclic sort from scratch
- [ ] Find missing/duplicate using position-as-index
- [ ] Recognize O(1) space benefit
- [ ] Handle duplicates in cyclic sort
- [ ] Understand termination condition

### Kadane's Algorithm
- [ ] Write Kadane's without reference (handle all-negative!)
- [ ] Distinguish local max vs global max
- [ ] Solve max product (track min too)
- [ ] Handle circular variant
- [ ] Add constraints (K elements, budget)
- [ ] Recognize DP thinking

### Fast-Slow Pointers
- [ ] Explain why meeting guarantees cycle
- [ ] Implement cycle detection from scratch
- [ ] Find cycle start (pointer reset trick)
- [ ] Find midpoint and handle even/odd
- [ ] Solve palindrome detection
- [ ] Generalize to non-linked-list cycles

---

## 🏁 Weekly Milestone Targets

**After Day 1:** Solve 3 hash problems without reference  
**After Day 2:** Solve 3 monotonic stack problems  
**After Day 3:** Solve 2 interval merging problems + compare sorting times  
**After Day 4:** Solve partition and Kadane problems, both from scratch  
**After Day 5:** Solve fast-slow and cycle problems, explain time/space  
**Integration Day:** Solve 2-3 problems mixing patterns  

---

**Next:** Daily Progress Checklist  
**Practice Time:** 12-15 hours for 25-30 problems

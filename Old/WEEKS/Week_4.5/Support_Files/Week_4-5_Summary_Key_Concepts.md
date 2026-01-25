# 📝 Week 4.5 — Summary & Key Concepts: Tier 1 Critical Patterns (COMPLETE)

**🗓️ Week:** 4.5  
**📌 Theme:** Critical Problem-Solving Patterns (Foundational + High-ROI)  
**🎯 Goal:** Master 5 patterns solving 70-80% of interview problems  
**🌟 Key Insight:** These patterns are TIER 1 — every candidate must know them cold

---

## 🧠 THE 5 TIER 1 PATTERNS (QUICK REFERENCE)

### 1️⃣ **Hash Map / Hash Set** (Day 1)
**When:** Need O(1) lookup by value (not index)  
**Core Operations:**
- Hash Map: key → value storage
- Hash Set: membership testing, duplicates

**Complexity:** O(1) average lookup/insertion, O(n) space  
**Red Flags:** "Two Sum", "Anagram", "Duplicate", "Frequency", "Group by", "First unique"

**Key Problems:**
- Two Sum (LeetCode #1) — THE classic
- Valid Anagram (#242)
- Group Anagrams (#49)

---

### 2️⃣ **Monotonic Stack** (Day 2)
**When:** Need next greater/smaller element OR maintain order invariant  
**Pattern:** Stack maintains increasing or decreasing order

**Complexity:** O(n) time (each element push/pop once), O(n) space  
**Red Flags:** "Next greater", "Next smaller", "Trapping water", "Largest rectangle", "Daily temperatures"

**Key Problems:**
- Next Greater Element (#496)
- Trapping Rain Water (#42) — HARD classic
- Largest Rectangle in Histogram (#84)

---

### 3️⃣ **Merge Operations & Intervals** (Day 3)
**When:** Combine sorted collections OR handle overlapping intervals  
**Variants:**
- Merge 2 sorted arrays: O(n+m)
- Merge K sorted lists: O(N log K) with min-heap
- Merge Intervals: O(n log n)

**Complexity:** Varies (see variants)  
**Red Flags:** "Merge sorted", "Combine", "Overlapping intervals", "Schedule", "Insert interval"

**Key Problems:**
- Merge K Sorted Lists (#23)
- Merge Intervals (#56)
- Insert Interval (#57)

---

### 4️⃣ **Partition & Cyclic Sort / Kadane's** (Day 4)
**Part A: Partition & Cyclic Sort**
- **When:** In-place rearrangement OR find missing/duplicate in [1..n]
- **Patterns:** Dutch National Flag, Cyclic Sort
- **Complexity:** O(n) time, O(1) space
- **Red Flags:** "Move zeros", "Sort colors", "Find missing", "Find duplicate"

**Part B: Kadane's Algorithm**
- **When:** Maximum subarray sum (consecutive elements)
- **Pattern:** DP — max ending at i = max(arr[i], max_ending_at_i-1 + arr[i])
- **Complexity:** O(n) time, O(1) space
- **Red Flags:** "Maximum subarray", "Maximum product subarray"

**Key Problems:**
- Sort Colors (#75) — Dutch National Flag
- Find All Missing Numbers (#448) — Cyclic Sort
- Maximum Subarray (#53) — Kadane's

---

### 5️⃣ **Fast & Slow Pointers** (Day 5)
**When:** Linked list problems (cycle, middle, palindrome)  
**Pattern:** Slow moves 1 step, fast moves 2 steps

**Complexity:** O(n) time, O(1) space  
**Red Flags:** "Cycle detection", "Find middle", "Palindrome (linked list)", "Happy number", "Nth from end"

**Key Problems:**
- Linked List Cycle (#141)
- Linked List Cycle II (#142) — Find cycle start
- Happy Number (#202)

---

## 📊 PATTERN SELECTION MATRIX

```
┌─────────────────────────────────────────────────────────────┐
│ PROBLEM TYPE                    → PATTERN TO USE            │
├─────────────────────────────────────────────────────────────┤
│ "Two Sum", "Frequency count"    → Hash Map                  │
│ "Anagram", "Duplicate"          → Hash Map / Hash Set       │
│                                                              │
│ "Next greater/smaller"          → Monotonic Stack           │
│ "Trapping water", "Rectangle"   → Monotonic Stack           │
│                                                              │
│ "Merge sorted arrays/lists"     → Merge Operations          │
│ "Overlapping intervals"         → Interval Merge            │
│                                                              │
│ "Move zeros", "Sort colors"     → Partition (Dutch Flag)    │
│ "Find missing in [1..n]"        → Cyclic Sort               │
│ "Maximum subarray sum"          → Kadane's Algorithm        │
│                                                              │
│ "Linked list cycle"             → Fast & Slow Pointers      │
│ "Find middle of list"           → Fast & Slow Pointers      │
└─────────────────────────────────────────────────────────────┘
```

---

## 🎯 COMPLEXITY QUICK REFERENCE

| Pattern | Time | Space | Key Optimization |
|---|---|---|---|
| **Hash Map** | O(1) avg | O(n) | Trade space for time |
| **Monotonic Stack** | O(n) | O(n) | Each element push/pop once |
| **Merge 2 Arrays** | O(n+m) | O(n+m) | Two pointers |
| **Merge K Lists (Heap)** | O(N log K) | O(K) | Min-heap for K-way merge |
| **Dutch National Flag** | O(n) | O(1) | Three pointers (low, mid, high) |
| **Cyclic Sort** | O(n) | O(1) | Index = value - 1 |
| **Kadane's** | O(n) | O(1) | DP, track max ending here |
| **Fast & Slow** | O(n) | O(1) | Relative speed = 1 |

---

## ⚠️ CRITICAL DISTINCTIONS

### **Hash Map vs Two Pointers (Two Sum)**
- **Unsorted array:** Hash Map (O(n) time, O(n) space).
- **Sorted array:** Two Pointers (O(n) time, O(1) space).
- **Trade-off:** Hash Map flexible (any array), Two Pointers space-efficient (sorted only).

### **Monotonic Stack vs Regular Stack**
- **Regular Stack:** LIFO, general purpose.
- **Monotonic Stack:** Maintains increasing/decreasing order (pops elements violating order).
- **Use Case:** Next greater element, histogram problems.

### **Merge vs Partition**
- **Merge:** Combine sorted collections (creates new collection or modifies existing).
- **Partition:** Rearrange in-place around pivot (no new memory).

### **Kadane's vs Brute Force Subarray**
- **Brute Force:** Check all O(n²) subarrays. Each sum O(n) → O(n³) or O(n²) with prefix.
- **Kadane's:** DP tracks max ending at each position. O(n) single pass.

### **Fast & Slow vs Two Pointers**
- **Fast & Slow:** Same direction, different speeds (linked list focus).
- **Two Pointers:** Opposite directions or same direction (array focus).

---

## 🧠 MENTAL MODELS FOR RETENTION

### **Hash Map = Library Catalog**
Array: Walk every shelf (O(n)).  
Hash Map: Look up title in catalog, go directly to shelf (O(1)).

### **Monotonic Stack = Conveyor Belt Filter**
Items on belt. Remove items that don't fit order (increasing/decreasing).  
What remains: monotonic sequence.

### **Merge = Zipper**
Two sorted lists = two zipper tracks.  
Merge = interleave teeth in sorted order.

### **Partition = Sorting Hat (Harry Potter)**
Dutch Flag: Sort students into 3 houses (0s, 1s, 2s).  
Three pointers move students to correct sections.

### **Kadane's = Stock Trader**
Track max profit so far.  
At each day: continue holding OR sell and start fresh (max of both).

### **Fast & Slow = Tortoise & Hare**
Tortoise walks. Hare runs.  
In a cycle, hare eventually laps tortoise (they meet).

---

## 📈 INTERVIEW FREQUENCY RANKING (Week 4.5)

1. 🔴 **80%+:** Hash Map / Hash Set (foundational, appears everywhere)
2. 🟡 **60%:** Fast & Slow Pointers (linked lists very common)
3. 🟡 **50-60%:** Merge Operations (K-way merge, intervals)
4. 🟡 **50%:** Kadane's Algorithm (subarray problems)
5. 🟡 **40-50%:** Monotonic Stack (niche but powerful when applicable)
6. 🟡 **30-40%:** Partition / Cyclic Sort (less common but O(1) space advantage)

---

## 🔗 PATTERN COMBINATIONS

Many interview problems combine multiple patterns:

**Example 1: Longest Substring Without Repeating (#3)**
- **Patterns:** Hash Set (track seen chars) + Sliding Window (variable)
- **Complexity:** O(n) time, O(k) space

**Example 2: Subarray Sum Equals K (#560)**
- **Patterns:** Hash Map (prefix sum → count) + Prefix Sum
- **Complexity:** O(n) time, O(n) space

**Example 3: Trapping Rain Water (#42)**
- **Patterns:** Monotonic Stack OR Two Pointers (track max heights)
- **Complexity:** O(n) time, O(1) space (two pointers) or O(n) stack

**Example 4: Merge Intervals (#56)**
- **Patterns:** Sort + Merge
- **Complexity:** O(n log n) time (dominated by sort)

---

## 🎁 BONUS: PATTERN RECOGNITION FLOWCHART

```
START: Read Problem
    ↓
Q: Need O(1) lookup by value?
    YES → Q: Frequency counting / Duplicate?
        YES → Hash Map / Hash Set ✓
    NO → Continue
    ↓
Q: Linked list problem?
    YES → Q: Cycle or middle?
        YES → Fast & Slow Pointers ✓
    NO → Continue
    ↓
Q: Maintain order OR next greater?
    YES → Monotonic Stack ✓
    NO → Continue
    ↓
Q: Merge sorted collections?
    YES → Merge Operations ✓
    NO → Continue
    ↓
Q: Overlapping intervals?
    YES → Interval Merge ✓
    NO → Continue
    ↓
Q: In-place partition / missing number?
    YES → Partition / Cyclic Sort ✓
    NO → Continue
    ↓
Q: Maximum consecutive sum?
    YES → Kadane's Algorithm ✓
    NO → Explore other patterns
```

---

## 🛠️ PRACTICE STRATEGY (Week 4.5)

### **Study Plan:**
- **Day 1:** Hash Map/Set (3-4 hours practice)
- **Day 2:** Monotonic Stack (2-3 hours)
- **Day 3:** Merge Operations & Intervals (3 hours)
- **Day 4:** Partition/Cyclic Sort + Kadane's (3-4 hours)
- **Day 5:** Fast & Slow Pointers (2-3 hours)
- **Day 6-7:** Mixed review + Hard problems

### **LeetCode Progression:**
1. **Easy problems (5-6 per pattern):** Build muscle memory.
2. **Medium problems (3-4 per pattern):** Test understanding.
3. **Hard problems (1-2 per pattern):** See advanced applications.

---

## 📚 KEY TAKEAWAYS

1. ✅ **Hash Map is foundational:** 80%+ of problems use it directly or indirectly.
2. ✅ **Space-time trade-offs:** Hash Map trades O(n) space for O(1) lookup.
3. ✅ **Monotonic Stack:** Powerful for "next greater/smaller" family.
4. ✅ **Merge operations:** K-way merge uses heap (O(N log K)).
5. ✅ **Kadane's is DP:** Recognize maximum subarray as DP pattern.
6. ✅ **Fast & Slow:** Cycle detection without extra space (elegant!).

---

**Generated:** December 30, 2025  
**Version:** 9.0 (Week 4.5 NEW)  
**Status:** ✅ COMPLETE
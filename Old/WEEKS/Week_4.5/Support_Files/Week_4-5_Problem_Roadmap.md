# 🗺️ Week 4.5 — Problem Solving Roadmap (Tier 1 Patterns) (COMPLETE)

**🗓️ Week:** 4.5  
**📌 Focus:** Hash Map, Monotonic Stack, Merge, Partition, Kadane's, Fast & Slow  
**🎯 Goal:** Master 5 critical patterns through 40+ LeetCode problems  
**⏱️ Estimated Time:** 18-25 hours total practice

---

## 📊 PATTERN-BASED PROBLEM MATRIX

### **Pattern 1: Hash Map / Hash Set** ⭐ Priority: CRITICAL (80%+)

| Difficulty | Problem | LeetCode # | Focus | Time | Key Concept |
|---|---|---|---|---|---|
| 🟢 Easy | **Two Sum** | 1 | Complement lookup | 15 min | **THE classic** |
| 🟢 Easy | **Valid Anagram** | 242 | Frequency count | 10 min | Char frequency |
| 🟢 Easy | **Contains Duplicate** | 217 | Hash Set | 10 min | Membership test |
| 🟢 Easy | **First Unique Character** | 387 | Frequency map | 15 min | Two pass |
| 🟢 Easy | **Isomorphic Strings** | 205 | Bidirectional map | 15 min | Two hash maps |
| 🟢 Easy | **Happy Number** | 202 | Cycle detection | 15 min | Hash Set for cycles |
| 🟡 Medium | **Group Anagrams** | 49 | Hash Map grouping | 25 min | Sorted key |
| 🟡 Medium | **Longest Substring w/o Repeat** | 3 | Hash Set + window | 30 min | Sliding window |
| 🟡 Medium | **Subarray Sum Equals K** | 560 | Prefix sum + map | 30 min | Running sum |
| 🟡 Medium | **4Sum II** | 454 | Pair sum map | 30 min | O(n²) with map |

**Total:** 10 problems | **Est. Time:** 3.5-4 hours

---

### **Pattern 2: Monotonic Stack** ⭐ Priority: MEDIUM-HIGH (40-50%)

| Difficulty | Problem | LeetCode # | Stack Type | Time | Key Concept |
|---|---|---|---|---|---|
| 🟢 Easy | **Next Greater Element I** | 496 | Decreasing | 20 min | Basic pattern |
| 🟡 Medium | **Daily Temperatures** | 739 | Decreasing | 25 min | Days until warmer |
| 🟡 Medium | **Next Greater Element II** | 503 | Decreasing (circular) | 30 min | Circular array |
| 🔴 Hard | **Trapping Rain Water** | 42 | Decreasing | 45 min | **Classic!** |
| 🔴 Hard | **Largest Rectangle Histogram** | 84 | Increasing | 45 min | **Classic!** |

**Total:** 5 problems | **Est. Time:** 2.5-3 hours

---

### **Pattern 3: Merge Operations & Intervals** ⭐ Priority: MEDIUM-HIGH (50-60%)

| Difficulty | Problem | LeetCode # | Merge Type | Time | Key Concept |
|---|---|---|---|---|---|
| 🟢 Easy | **Merge Sorted Array** | 88 | Two arrays | 15 min | In-place merge |
| 🟢 Easy | **Merge Two Sorted Lists** | 21 | Two lists | 15 min | Two pointers |
| 🟡 Medium | **Merge K Sorted Lists** | 23 | K lists (heap) | 35 min | Min-heap |
| 🟡 Medium | **Merge Intervals** | 56 | Overlapping | 30 min | Sort + merge |
| 🟡 Medium | **Insert Interval** | 57 | Insert + merge | 30 min | Three phases |
| 🟡 Medium | **Interval List Intersections** | 986 | Two lists | 30 min | Two pointers |
| 🔴 Hard | **Employee Free Time** | 759 | Multiple lists | 40 min | Flatten + merge |

**Total:** 7 problems | **Est. Time:** 3-3.5 hours

---

### **Pattern 4A: Partition & Cyclic Sort** ⭐ Priority: MEDIUM (30-40%)

| Difficulty | Problem | LeetCode # | Pattern | Time | Key Concept |
|---|---|---|---|---|---|
| 🟢 Easy | **Move Zeroes** | 283 | Partition | 15 min | Two pointers |
| 🟡 Medium | **Sort Colors** | 75 | Dutch Flag | 25 min | Three pointers |
| 🟡 Medium | **Find All Missing Numbers** | 448 | Cyclic Sort | 30 min | Index = value-1 |
| 🟡 Medium | **Find Duplicate Number** | 287 | Cyclic / Fast-Slow | 30 min | Two approaches |
| 🔴 Hard | **First Missing Positive** | 41 | Cyclic Sort | 40 min | In-place |

**Total:** 5 problems | **Est. Time:** 2-2.5 hours

---

### **Pattern 4B: Kadane's Algorithm** ⭐ Priority: HIGH (50%)

| Difficulty | Problem | LeetCode # | Variant | Time | Key Concept |
|---|---|---|---|---|---|
| 🟡 Medium | **Maximum Subarray** | 53 | Basic Kadane's | 20 min | **Classic!** |
| 🟡 Medium | **Maximum Product Subarray** | 152 | Track min/max | 30 min | Handle negatives |
| 🟡 Medium | **Maximum Sum Circular Subarray** | 918 | Circular | 35 min | Min subarray |
| 🔴 Hard | **Best Time to Buy/Sell III** | 123 | Two transactions | 45 min | Extended Kadane's |

**Total:** 4 problems | **Est. Time:** 2-2.5 hours

---

### **Pattern 5: Fast & Slow Pointers** ⭐ Priority: HIGH (60%)

| Difficulty | Problem | LeetCode # | Focus | Time | Key Concept |
|---|---|---|---|---|---|
| 🟢 Easy | **Linked List Cycle** | 141 | Cycle detection | 15 min | **Foundation** |
| 🟢 Easy | **Middle of Linked List** | 876 | Find middle | 10 min | Fast reaches end |
| 🟡 Medium | **Linked List Cycle II** | 142 | Cycle start | 30 min | Floyd's extended |
| 🟡 Medium | **Happy Number** | 202 | Cycle in sequence | 20 min | Hash Set OR Fast/Slow |
| 🟡 Medium | **Remove Nth from End** | 19 | Gap technique | 20 min | Fast ahead N steps |
| 🟡 Medium | **Palindrome Linked List** | 234 | Find middle + reverse | 30 min | Combine techniques |
| 🟡 Medium | **Reorder List** | 143 | Middle + reverse | 35 min | Multi-step |

**Total:** 7 problems | **Est. Time:** 2.5-3 hours

---

## 📅 SUGGESTED 7-DAY SCHEDULE

### **Day 1: Hash Map / Hash Set Mastery** (4-5 hours)
**Morning (Fundamentals):**
- Two Sum (#1) → 15 min (**MUST master**)
- Valid Anagram (#242) → 10 min
- Contains Duplicate (#217) → 10 min
- First Unique Character (#387) → 15 min

**Afternoon (Medium):**
- Group Anagrams (#49) → 25 min
- Longest Substring w/o Repeat (#3) → 30 min

**Evening (Challenge):**
- Subarray Sum Equals K (#560) → 30 min
- 4Sum II (#454) → 30 min

---

### **Day 2: Monotonic Stack** (3 hours)
**Morning:**
- Next Greater Element I (#496) → 20 min
- Daily Temperatures (#739) → 25 min

**Afternoon:**
- Next Greater Element II (#503) → 30 min

**Challenge (Hard):**
- Trapping Rain Water (#42) → 45 min
- OR: Largest Rectangle (#84) → 45 min

---

### **Day 3: Merge Operations & Intervals** (3.5 hours)
**Morning (Easy):**
- Merge Sorted Array (#88) → 15 min
- Merge Two Sorted Lists (#21) → 15 min

**Afternoon (K-way Merge):**
- Merge K Sorted Lists (#23) → 35 min
- Merge Intervals (#56) → 30 min

**Evening:**
- Insert Interval (#57) → 30 min
- Interval List Intersections (#986) → 30 min

---

### **Day 4: Partition, Cyclic Sort, Kadane's** (4-5 hours)
**Morning (Partition):**
- Move Zeroes (#283) → 15 min
- Sort Colors (#75) → 25 min

**Afternoon (Cyclic Sort):**
- Find All Missing (#448) → 30 min
- Find Duplicate (#287) → 30 min

**Kadane's:**
- Maximum Subarray (#53) → 20 min (**Classic!**)
- Maximum Product Subarray (#152) → 30 min

**Challenge:**
- First Missing Positive (#41) → 40 min

---

### **Day 5: Fast & Slow Pointers** (3 hours)
**Morning:**
- Linked List Cycle (#141) → 15 min
- Middle of List (#876) → 10 min

**Afternoon:**
- Linked List Cycle II (#142) → 30 min
- Happy Number (#202) → 20 min

**Evening:**
- Remove Nth from End (#19) → 20 min
- Palindrome Linked List (#234) → 30 min

---

### **Day 6: Mixed Review & Hard** (3-4 hours)
**Morning:** Review all patterns (1 problem each)
**Afternoon:** Attempt 2-3 Hard problems
- Trapping Rain Water (#42)
- Largest Rectangle (#84)
- Employee Free Time (#759)

---

### **Day 7: Mock Interview & Weak Spots** (3 hours)
**Morning:** Revisit problems marked "struggled"
**Afternoon:** Mock interview (1-2 Medium in 45 min)
**Evening:** Review pattern summaries

---

## 🎯 SUCCESS METRICS

### **Per Difficulty:**
- ✅ **Easy:** Solve in 10-15 min (no hints)
- ✅ **Medium:** Solve in 20-35 min (1 hint allowed first time)
- ✅ **Hard:** Understand optimal solution (implement in 40-50 min)

### **Overall Goals:**
- ✅ Solve 80%+ Easy on first attempt
- ✅ Solve 60%+ Medium within time
- ✅ Understand 100% Hard approaches

---

## 📈 PROGRESSION PATH

```
Week 4.5 Tier 1 Patterns
    ↓
Hash Map (Foundation)
    ↓
Monotonic Stack (Specialized)
    ↓
Merge Operations (Divide & Conquer extension)
    ↓
Partition / Cyclic Sort (In-place)
    ↓
Kadane's (DP foundation)
    ↓
Fast & Slow (Linked List mastery)
    ↓
Combine Patterns (Week 5+)
```

---

## 💡 PRO TIPS

1. **Two Sum is THE gateway:** Master it cold. Interviewers expect instant solution.
2. **Monotonic Stack:** Draw stack state at each step (visualize helps immensely).
3. **Merge K Lists:** Understand why heap is O(N log K), not O(N*K).
4. **Cyclic Sort:** Only works when numbers in [1..n] or [0..n] range (clarify!).
5. **Fast & Slow:** Practice on paper first (understand relative speed = 1).

---

## 🔄 SPACED REPETITION

**Week 5 (3 days later):**
- Re-solve Two Sum, Merge K Lists, Kadane's

**Week 6 (7 days later):**
- Re-solve 5 random Medium problems from Week 4.5

**Week 8 (14 days later):**
- Final review: 1 Hard per pattern

---

**Generated:** December 30, 2025  
**Version:** 9.0 (Week 4.5)  
**Status:** ✅ COMPLETE
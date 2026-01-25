# 📝 Week 4 — Summary & Key Concepts: Problem-Solving Patterns (REGENERATED)

**🗓️ Week:** 4  
**📌 Theme:** Pattern Recognition & Algorithmic Problem-Solving  
**🎯 Goal:** Master 5 fundamental patterns that solve 80%+ of interview problems  
**🌟 Key Insight:** Patterns > Memorization. Recognize the structure, not the specific problem.

---

## 🧠 THE 5 CORE PATTERNS (QUICK REFERENCE)

### 1️⃣ **Two Pointers** (Left/Right, Fast/Slow)
**When:** Sorted arrays OR directional property (palindrome, reversal)  
**Variants:**
- **Left/Right:** Converge from ends (Two Sum II, Container with Water)
- **Fast/Slow:** Different speeds (Cycle Detection, Find Middle)
- **Same Direction:** One scans, one tracks valid position (Remove Duplicates)

**Complexity:** O(n) time, O(1) space  
**Red Flags:** "Two elements", "Pair", "Cycle", "Palindrome", "Merge sorted"

---

### 2️⃣ **Sliding Window (Fixed)**
**When:** Window size k known, compute aggregate over consecutive elements  
**Update Formula:** `New Sum = Old Sum - arr[left] + arr[right]`  
**Optimization:** O(n*k) → O(n)

**Complexity:** O(n) time, O(1) space  
**Red Flags:** "Maximum/minimum over subarray of size k", "Moving average", "k consecutive"

---

### 3️⃣ **Sliding Window (Variable)**
**When:** Constraint-based, window size unknown (find optimal)  
**Logic:** Expand right (add elements), contract left (remove to maintain constraint)  
**Key:** Both pointers visit each element at most once → Amortized O(n)

**Complexity:** O(n) time, O(k) space (hash map for frequency)  
**Red Flags:** "Longest substring", "Minimum substring", "At most k distinct", "Contains all"

---

### 4️⃣ **Divide & Conquer**
**When:** Problem can be split into independent subproblems  
**Pattern:** Divide → Solve Recursively → Combine  
**Master Theorem:** T(n) = a*T(n/b) + f(n)

**Complexity:** O(n log n) for balanced divisions  
**Red Flags:** "Merge K lists", "Median of two arrays", "Parallel processing", "MapReduce"

---

### 5️⃣ **Binary Search on Answer**
**When:** Answer space is monotonic (once feasible, always feasible for larger values)  
**Pattern:** Binary search on answer range, check feasibility each time  
**Feasibility Function:** Must be O(n) or better

**Complexity:** O(n * log(max_answer))  
**Red Flags:** "Minimize the maximum", "Maximize the minimum", "Find minimum X such that..."

---

## 📊 PATTERN SELECTION MATRIX

```
┌─────────────────────────────────────────────────────────┐
│ PROBLEM TYPE              → PATTERN TO USE              │
├─────────────────────────────────────────────────────────┤
│ "Two elements sum to X"   → Two Pointers (if sorted)   │
│                              OR Hash Map (if unsorted)  │
│                                                         │
│ "Max/min in window of k"  → Fixed Sliding Window       │
│                                                         │
│ "Longest substring with"  → Variable Sliding Window    │
│ "Minimum window that"                                   │
│                                                         │
│ "Merge K sorted"          → Divide & Conquer           │
│ "Median of two arrays"                                  │
│                                                         │
│ "Minimize max capacity"   → Binary Search on Answer    │
│ "Find minimum X where"                                  │
└─────────────────────────────────────────────────────────┘
```

---

## 🎯 COMPLEXITY COMPARISON TABLE

| Problem | Brute Force | Optimized Pattern | Speedup |
|---|---|---|---|
| **Two Sum II (Sorted)** | O(n log n) Binary search per element | O(n) Two Pointers | 3-5x |
| **Moving Average (k=100)** | O(n*k) Recalculate each | O(n) Fixed Window | 100x |
| **Longest Substring** | O(n³) All substrings | O(n) Variable Window | Massive |
| **Merge K Lists (K=1000)** | O(N*K) Sequential | O(N log K) D&C | 100x |
| **Ship Capacity** | O(n*sum) Try all | O(n log sum) Binary | 1000x+ |

---

## ⚠️ CRITICAL DISTINCTIONS

### **Two Pointers vs Sliding Window**
- **Two Pointers:** Target element *pairs* (sum, distance). Pointers converge.
- **Sliding Window:** Track *subarray content* (frequency, sum). Window slides.
- **Overlap:** Both use two indices, but logic and purpose differ.

### **Fixed vs Variable Window**
- **Fixed:** Size k known. Simple update (add right, remove left).
- **Variable:** Size varies by constraint. Independent pointer movement.

### **Binary Search (Array) vs Binary Search (Answer)**
- **Array:** Find element in sorted array.
- **Answer:** Find optimal value in answer space. Requires feasibility function.

---

## 🧠 MENTAL MODELS FOR RETENTION

### **Two Pointers = Scissors**
Two blades closing toward the center. When they meet, they "cut" (find solution).

### **Sliding Window = Camera on Rail**
Camera frame slides left-to-right. Update what's visible (add right, remove left).

### **Variable Window = Elastic Balloon**
Expand (blow air) when possible. Contract (release air) when constraint violated.

### **Divide & Conquer = Binary Tree of Work**
Split problem recursively. Each level does O(n) work. Height O(log n).

### **Binary Search on Answer = Light Switch**
Below threshold: OFF (infeasible). Above threshold: ON (feasible). Find the switch point.

---

## 📈 INTERVIEW FREQUENCY RANKING

1. 🔴 **70%:** Binary Search on Answer (Tier 1 skill, Hard problems)
2. 🔴 **70%:** Variable Sliding Window (Substring problems, very common)
3. 🟡 **60-70%:** Two Pointers (Sorted arrays, Linked lists)
4. 🟡 **50-60%:** Fixed Sliding Window (Moving stats, time series)
5. 🟡 **25-30%:** Divide & Conquer (High-level strategy, system design)

---

## 🔗 PATTERN DEPENDENCIES

```
Two Pointers (Week 4 Day 1)
    ↓ (Foundation for understanding pointer movement)
Fixed Sliding Window (Week 4 Day 2)
    ↓ (Extension to independent movement)
Variable Sliding Window (Week 4 Day 3)
    ↓ (Combine with recursion)
Divide & Conquer (Week 4 Day 4)
    ↓ (Advanced optimization thinking)
Binary Search on Answer (Week 4 Day 5)
```

---

## 🎁 BONUS: PATTERN RECOGNITION FLOWCHART

```
START: Read Problem
    ↓
Q: Involves PAIRS of elements?
    YES → Q: Array sorted?
        YES → Two Pointers ✓
        NO → Hash Map ✓
    NO → Continue
    ↓
Q: Window over consecutive elements?
    YES → Q: Window size known?
        YES → Fixed Sliding Window ✓
        NO → Variable Sliding Window ✓
    NO → Continue
    ↓
Q: Merge/combine multiple collections?
    YES → Divide & Conquer ✓
    NO → Continue
    ↓
Q: Minimize max OR Maximize min?
    YES → Binary Search on Answer ✓
    NO → Explore other patterns
```

---

## 🛠️ PRACTICE STRATEGY

### **Week 4 Study Plan:**
- **Day 1-2:** Master Two Pointers + Fixed Window (fundamentals)
- **Day 3:** Variable Window (constraint thinking)
- **Day 4:** Divide & Conquer (recursive strategy)
- **Day 5:** Binary Search on Answer (advanced optimization)
- **Day 6-7:** Review + solve mixed problems

### **LeetCode Progression:**
1. Solve 3-4 Easy problems per pattern (build confidence)
2. Attempt 2-3 Medium problems (test understanding)
3. Read 1-2 Hard solutions (see advanced applications)

---

## 📚 KEY TAKEAWAYS

1. ✅ **Patterns are tools.** Recognize which tool (pattern) fits the problem structure.
2. ✅ **Complexity matters.** Know when O(n) vs O(n log n) vs O(n²) is acceptable.
3. ✅ **Space-time trade-offs.** Two Pointers (O(1) space) vs Hash Map (O(n) space).
4. ✅ **Edge cases are critical.** Empty inputs, single elements, duplicates, boundaries.
5. ✅ **Practice recognition.** Speed comes from instantly recognizing "This is a Two Pointer problem."

---

**Generated:** December 30, 2025  
**Version:** 9.0 (Regenerated - Comprehensive Reference)  
**Status:** ✅ COMPLETE
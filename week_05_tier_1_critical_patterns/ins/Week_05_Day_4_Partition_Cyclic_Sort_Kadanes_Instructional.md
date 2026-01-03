# 🎯 WEEK 5 DAY 4: PARTITION, CYCLIC SORT, & KADANE'S — COMPLETE GUIDE

**Category:** Core Problem-Solving Patterns / In-Place Array Manipulation  
**Difficulty:** 🟡 Medium  
**Prerequisites:** Arrays (Week 2 Day 1), Swapping Logic  
**Interview Frequency:** 65% (High — Kadane's and Dutch National Flag are classics)  
**Real-World Impact:** Memory Management, Sort Implementation, Signal Processing, Data Deduplication

---

## 🎓 LEARNING OBJECTIVES

By the end of this topic, you will be able to:

- ✅ **Master** the 3-Way Partitioning (Dutch National Flag) logic to categorize data in one pass.
- ✅ **Apply** Cyclic Sort to place numbers in their correct indices in O(N) time with O(1) space.
- ✅ **Understand** Kadane’s Algorithm as a dynamic programming optimization for maximum subarray sum.
- ✅ **Visualize** how "Current", "Max So Far", and "Min So Far" variables interact in stream processing.
- ✅ **Identify** missing or duplicate numbers in a range `1` to `N` using index-mapping.

| 🎯 Objective | 📍 Primary Section |
|:---|:---|
| Core Philosophy & Mental Model | Section 2: The What |
| Mechanical Implementation Steps | Section 3: The How |
| Worked Examples with Traces | Section 4: Visualization |
| Complexity & Performance Analysis | Section 5: Critical Analysis |
| Real-World Production Systems | Section 6: Real Systems |

---

## 🤔 SECTION 1: THE WHY — Engineering Motivation

### 🎯 Real-World Problems This Solves

#### Problem 1: Memory & Garbage Collection (Partitioning)

**🌍 Where:** Java Garbage Collector (G1GC), OS Page Replacement  
**💼 Concrete Challenge:**  
Memory objects are categorized as "Young Generation" (New), "Old Generation" (Long-lived), or "Dead" (Garbage). The GC needs to group these objects efficiently to reclaim space.  

**Pattern:** Dutch National Flag (3-Way Partition).
- Partition memory blocks into [Live Objects | Indeterminate | Dead Objects].
- Doing this in-place avoids allocating new memory during a memory-critical cleanup phase.

**Impact:** Efficient memory reclamation with minimal overhead.

---

#### Problem 2: Signal Processing & Stock Analysis (Kadane's)

**🌍 Where:** High-Frequency Trading, Audio Noise Reduction  
**💼 Concrete Challenge:**  
Given a stream of stock price changes `[+1, -2, +5, +3, -4, +2]`, find the continuous period with the highest profit. Or, in audio, find the loudest continuous segment to normalize volume.

**Pattern:** Kadane's Algorithm.
- Brute force checks all subarrays (O(N²)).
- Kadane's scans once (O(N)), resetting the "current sum" whenever it drops below zero (because a negative prefix never helps a future sum).

**Impact:** Real-time analytics on millions of data points/sec.

---

#### Problem 3: Data Integrity & Error Correction (Cyclic Sort)

**🌍 Where:** Network Packet Reassembly, Database Sequence Gaps  
**💼 Concrete Challenge:**  
Packets arrive out of order: `[3, 1, 4, 2]`. You know they should be `1, 2, 3, 4`. You need to detect if any packet is missing or duplicated without using extra memory (buffer is full).

**Pattern:** Cyclic Sort.
- Since packet `3` belongs at index `2` (0-based), swap it there.
- Repeat until everything is ordered.
- Scan to find gaps.

**Impact:** Fault detection in constrained embedded systems.

---

### ⚖ Design Problem & Trade-offs

**Core Design Problem:**  
How do we organize or analyze an array **in-place** (O(1) space) when we have specific knowledge about the content (e.g., range 1..N or 3 categories)?

#### Approach Comparison Table

| Approach | Strategy | Time Complexity | Space Complexity | Best For |
|----------|----------|-----------------|------------------|----------|
| **Sorting (Standard)** | Merge/Quick Sort | O(N log N) | O(log N) | Generic data |
| **Hash Set** | Store seen elements | O(N) | O(N) | Finding dups/missing |
| **Cyclic Sort** | Place `val` at `index = val - 1` | **O(N)** | **O(1)** | **Range 1 to N** |
| **DNF Partition** | 3 Pointers (Low, Mid, High) | **O(N)** | **O(1)** | **3 Categories** |
| **Kadane's** | Drop negative prefixes | **O(N)** | **O(1)** | **Max Subarray Sum** |

**Trade-off:**  
These patterns are **highly specialized**. Cyclic Sort *only* works if data is in a specific integer range. Kadane's *only* works for sum/product maximization. But when they apply, they are mathematically optimal (O(N) Time, O(1) Space).

---

### 💼 Interview Relevance

**Common Interview Archetypes:**

| 🔴 Red Flag Keywords | Problem Type | Example |
|---------------------|--------------|---------|
| "Sort 0s, 1s, and 2s" | Dutch National Flag | Sort Colors |
| "Numbers from 1 to N" | Cyclic Sort | Missing Number, Find Duplicate |
| "Max contiguous sum" | Kadane's Algorithm | Maximum Subarray |
| "Rearrange positive/negative" | Partitioning | Rearrange Array Elements |
| "First missing positive" | Cyclic Sort (Advanced) | First Missing Positive |

**What Interviewers Test:**

```mermaid
graph TD
    A[Interviewer Goals] --> B[In-Place Manipulation]
    A --> C[Edge Case Handling]
    A --> D[Pointer Management]
    
    B --> B1["Can you sort/partition without<br/>creating a new array?"]
    C --> C1["All negative numbers (Kadane)"]
    C --> C2["Duplicate numbers (Cyclic Sort)"]
    D --> D1["Handling 'High' pointer decrement<br/>without moving 'Mid' (DNF Bug)"]
    
    style A fill:#ff9999
    style B fill:#99ccff
    style C fill:#99ff99
    style D fill:#ffff99
```

---

## 📌 SECTION 2: THE WHAT — Mental Model & Core Concepts

### 🧠 Core Analogy 1: The "Traffic Cop" (Dutch National Flag)

Imagine a chaotic line of cars: **Red**, **White**, and **Blue**.
You want: All **Reds** on the left, **Whites** in the middle, **Blues** on the right.

**The Strategy:**
- You stand in the middle (`Mid` pointer).
- If you see a **Red** car: Swap it to the Left (`Low` region). Increment both `Low` and `Mid`.
- If you see a **White** car: It's already in the middle. Just move to the next (`Mid++`).
- If you see a **Blue** car: Swap it to the Right (`High` region). Decrement `High`. **Do not move Mid yet** (the swapped car needs inspection).

---

### 🧠 Core Analogy 2: The "Wrong Seat" (Cyclic Sort)

Imagine students numbered 1 to N entering a classroom with seats 0 to N-1.
- Student #3 is sitting in Seat #0.
- Teacher says: "Student #3, go to Seat #2 (index 2)!"
- Student #3 swaps with whoever is in Seat #2.
- Now a new student is in Seat #0. Repeat until Seat #0 has Student #1 (or a correct student).
- Move to Seat #1.

**Key Insight:** If every student sits in their assigned seat, the array is sorted.

---

### 🧠 Core Analogy 3: The "Heavy Backpack" (Kadane's)

You are hiking and collecting gold coins (positive numbers) but sometimes you hit debt traps (negative numbers).
- Goal: Maximize wealth at some point.
- Rule: If your backpack's total value becomes **negative** (debt > gold), dump the backpack completely (Reset to 0). A debt-ridden backpack will only drag down future gains. Start fresh.

---

### 📋 Core Concepts & Variations (List All)

#### 1. Dutch National Flag (3-Way Partition)
- **Goal:** Sort array of 3 distinct values (0, 1, 2) in O(N).
- **Pointers:** `Low` (end of 0s), `High` (start of 2s), `Mid` (current scanner).
- **Invariant:** `[0...Low-1]` are 0s. `[High+1...End]` are 2s.

#### 2. Cyclic Sort (Range 1 to N)
- **Goal:** Sort/Find Missing in range `1` to `N`.
- **Logic:** Swap `arr[i]` with `arr[arr[i]-1]`.
- **Complexity:** O(N) (Each number is swapped at most once).

#### 3. Kadane's Algorithm (Max Subarray)
- **Goal:** Largest sum of contiguous subarray.
- **Logic:** `CurrentSum = max(num, CurrentSum + num)`.
- **Edge Case:** All negative numbers (result should be the max single element, usually handled naturally or by specific check).

#### 4. Partitioning (2-Way)
- **Goal:** Move all valid elements to left. (Move Zeroes, Remove Elements).
- **Logic:** `Read` and `Write` pointers.

---

## ⚙ SECTION 3: THE HOW — Mechanical Walkthrough

### 🔧 Operation 1: Dutch National Flag (Sort Colors)

**Problem:** Sort `[2, 0, 2, 1, 1, 0]` (0=Red, 1=White, 2=Blue).

**Visual State Machine:**

```mermaid
graph LR
    subgraph "Regions"
    L[Low Pointer] -->|Stores 0s| M[Mid Pointer]
    M -->|Scans Unknown| H[High Pointer]
    H -->|Stores 2s| E[End]
    end

    subgraph "Rules"
    R1{Is arr[Mid] == 0?} -- Yes --> S1[Swap Mid & Low<br/>Low++, Mid++]
    R2{Is arr[Mid] == 1?} -- Yes --> S2[Mid++]
    R3{Is arr[Mid] == 2?} -- Yes --> S3[Swap Mid & High<br/>High--]
    end
```

**Mechanical Steps:**
1. `Low=0`, `Mid=0`, `High=N-1`.
2. Loop while `Mid <= High`.
3. Check `arr[Mid]`:
   - If `0`: Swap `arr[Low], arr[Mid]`. `Low++`, `Mid++`.
   - If `1`: `Mid++`.
   - If `2`: Swap `arr[Mid], arr[High]`. `High--`. (Notice `Mid` doesn't move!).

---

### 🔧 Operation 2: Cyclic Sort (Find Missing)

**Problem:** `[3, 0, 1]`. Range 0 to N. Find missing.

**Mechanical Steps:**
1. Iterate `i` from 0.
2. While `arr[i] < N` AND `arr[i] != arr[arr[i]]` (Not in correct spot):
   - Swap `arr[i]` with `arr[arr[i]]`.
3. After loop, scan `i`. First index where `arr[i] != i` is the answer.

---

### 🔧 Operation 3: Kadane's Algorithm

**Problem:** `[-2, 1, -3, 4, -1, 2, 1, -5, 4]`.

**Logic Flow:**

```mermaid
flowchart TD
    Start([Init CurrentSum = 0, MaxSum = -Inf]) --> Loop{For each num}
    Loop --> Update[CurrentSum += num]
    Update --> CheckMax{CurrentSum > MaxSum?}
    CheckMax -- Yes --> SetMax[MaxSum = CurrentSum]
    CheckMax -- No --> CheckNeg{CurrentSum < 0?}
    SetMax --> CheckNeg
    CheckNeg -- Yes --> Reset[CurrentSum = 0]
    CheckNeg -- No --> Loop
    Reset --> Loop
    Loop -- Done --> End([Return MaxSum])
```

---

## 🎨 SECTION 4: VISUALIZATION — Simulation & Examples

### 🧊 Trace: Dutch National Flag

**Input:** `[2, 0, 1]`  
**Init:** `L=0, M=0, H=2`

| Iteration | Array State | Mid Val | Action | Pointers Update |
|:---:|:---:|:---:|:---:|:---|
| 1 | `[2, 0, 1]` | 2 | Swap Mid/High | `High` becomes 1. Array: `[1, 0, 2]` |
| 2 | `[1, 0, 2]` | 1 | Ignore | `Mid` becomes 1. |
| 3 | `[1, 0, 2]` | 0 | Swap Mid/Low | `Low` becomes 1, `Mid` becomes 2. Array: `[0, 1, 2]` |
| 4 | `[0, 1, 2]` | - | `M > H` | **STOP** |

**Result:** `[0, 1, 2]`

---

### 🧊 Trace: Kadane's Algorithm

**Input:** `[-2, 1, -3, 4, -1, 2, 1]`

| Step | Num | CurrentSum (Before) | Action (Add) | New CurrentSum | MaxSoFar |
|:---:|:---:|:---:|:---:|:---:|:---:|
| 1 | -2 | 0 | Add -2 | -2 → **Reset to 0** | -2 (Init) |
| 2 | 1 | 0 | Add 1 | 1 | 1 |
| 3 | -3 | 1 | Add -3 | -2 → **Reset to 0** | 1 |
| 4 | 4 | 0 | Add 4 | 4 | 4 |
| 5 | -1 | 4 | Add -1 | 3 | 4 |
| 6 | 2 | 3 | Add 2 | 5 | 5 |
| 7 | 1 | 5 | Add 1 | 6 | **6** |

**Result:** 6

---

## 📊 SECTION 5: CRITICAL ANALYSIS — Performance & Robustness

### 📈 Complexity Table

| Algorithm | Time Complexity | Space Complexity | Why? |
|-----------|-----------------|------------------|------|
| **Dutch National Flag** | O(N) | O(1) | Single pass (Mid pointer traverses once). |
| **Cyclic Sort** | O(N) | O(1) | Each element swapped at most once into place. |
| **Kadane's Algo** | O(N) | O(1) | Single pass, constant variables. |
| **Sort Colors (Std Sort)** | O(N log N) | O(1) | Standard sorting is slower. |
| **Brute Force Max Subarray**| O(N²) | O(1) | Calculating all subarray sums. |

### 🧠 Trade-offs
- **Destructive Mutation:** Cyclic Sort and DNF **scramble** the original array order. If you need to preserve original indices (e.g., "Find indices of two numbers..."), you cannot use these without copying.
- **Range Constraints:** Cyclic Sort strictly requires values to be within `[0, N]` or `[1, N]`. It fails if numbers are `100, 200, 300`.

### ⚠ Edge Cases
1. **All Negative Numbers (Kadane):**
   - Standard Kadane resets to 0. If answer must be at least one number (e.g., -1), logic needs `CurrentSum = max(num, CurrentSum + num)` instead of resetting to 0 explicitly.
2. **Duplicates (Cyclic Sort):**
   - If `arr[i] == arr[target_index]`, we found a duplicate. Do not swap (infinite loop), just move on or record duplicate.
3. **Empty Array:** Handle gracefully.

---

## 🏭 SECTION 6: REAL SYSTEMS — Integration in Production

### 🏭 System 1: Garbage Collection (Mark-Compact)
**Use Case:** Defragmenting Heap.
**Logic:** Uses a variation of **Partitioning**. Live objects are moved to one end of the memory space, and the pointer is bumped to free the rest.

### 🏭 System 2: Database Storage (Deduplication)
**Use Case:** Compressing data columns.
**Logic:** **Cyclic Sort** concepts help in Run-Length Encoding or finding gaps in sequence IDs (e.g., finding a missing invoice ID in a sequence).

### 🏭 System 3: Audio Engineering (Peak Detection)
**Use Case:** Normalizing audio tracks.
**Logic:** **Kadane’s Algorithm** logic scans audio samples to find the loudest continuous section (Max Subarray) to set the gain correctly without clipping.

### 🏭 System 4: Computer Vision (Thresholding)
**Use Case:** Separating background from foreground.
**Logic:** **Partitioning** pixels based on intensity (Binary Partition). Pixels < 128 go to Black bucket, > 128 go to White bucket.

### 🏭 System 5: Operating Systems (Page Replacement)
**Use Case:** LRU approximation (Clock Algorithm).
**Logic:** Uses a **Cyclic** circular buffer approach to give "second chances" to pages before eviction.

---

## 🔗 SECTION 7: CONCEPT CROSSOVERS

### 📚 Builds On (Prerequisites)
- **Arrays:** Direct index access is crucial.
- **Swapping:** The fundamental atomic operation for DNF and Cyclic Sort.
- **Greedy:** Kadane's is a greedy approach (take positive, drop negative).

### 🚀 Used By (Future Topics)
- **Quick Sort:** The partition step in Quick Sort is essentially a 2-way Dutch National Flag (Pivot Partition).
- **Introselect (Quick Select):** Uses partitioning to find K-th smallest element.
- **Dynamic Programming:** Kadane's is the 1D base case for "Max Sum Rectangle in 2D Matrix".

---

## 🧩 5 COGNITIVE LENSES

### 🧠 1. The "Traffic Light" Lens (DNF)
Red (Stop/Left), Yellow (Yield/Mid), Green (Go/Right). You are the traffic cop directing cars to their lanes. The key is you don't need to move the Yellow cars; you just move the Red and Green ones around them.

### 🔄 2. The "Correct Seat" Lens (Cyclic Sort)
Every number has a "home" index.
- Number `5` belongs at index `4` (0-based).
- If `5` is not at `4`, kick out whoever is at `4` and put `5` there.
- Repeat until everyone is home.

### 🎒 3. The "Baggage" Lens (Kadane's)
Your `CurrentSum` is your baggage.
- If your baggage has value (Positive), keep carrying it.
- If your baggage becomes debt (Negative), drop it immediately. A fresh start (0) is better than debt (-5).

### 📉 4. The "In-Place" Lens
These algorithms are obsessed with `O(1)` space. They ask: "Can I use the input array itself as my scratchpad?"
- Cyclic Sort uses the *value* to encode the *address*.

### 🔍 5. The "Pivot" Lens
In DNF, `1` is the pivot.
In Partitioning, `0` is often the pivot.
We classify the world relative to this pivot: "Less than", "Equal to", "Greater than".

---

## ⚔ SUPPLEMENTARY OUTCOMES

### ⚔ Practice Problems (10 Problems)

1.  **Sort Colors** (Medium) - *Classic DNF.*
2.  **Maximum Subarray** (Medium) - *Classic Kadane.*
3.  **Missing Number** (Easy) - *Cyclic Sort or Math.*
4.  **Find the Duplicate Number** (Medium) - *Cyclic Sort logic (or Floyd's).*
5.  **Find All Numbers Disappeared in an Array** (Easy) - *Cyclic Sort / Index Marking.*
6.  **First Missing Positive** (Hard) - *Advanced Cyclic Sort (Ignore > N and <= 0).*
7.  **Maximum Product Subarray** (Medium) - *Kadane's with Min/Max tracking.*
8.  **Wiggle Sort II** (Medium) - *Partitioning / Sorting.*
9.  **Rearrange Array Elements by Sign** (Medium) - *Partitioning logic.*
10. **Longest Turbulant Subarray** (Medium) - *Kadane variation.*

### 🎙 Interview Questions (6 Questions)

1.  **Q:** Why does Kadane's algorithm reset `CurrentSum` when it drops below 0?
    -   **A:** Because a negative prefix will always reduce the sum of any subsequent subarray. It is mathematically better to start from 0 than from a negative number.

2.  **Q:** In Dutch National Flag, why don't we increment `Mid` when we swap with `High`?
    -   **A:** The element swapped from `High` is unknown (could be 0, 1, or 2). We must inspect it at `Mid` in the next iteration.

3.  **Q:** Can Cyclic Sort handle negative numbers?
    -   **A:** No, it relies on the values mapping to valid indices `0` to `N-1`.

4.  **Q:** How do you modify Kadane's to handle the "Max Circular Subarray" problem?
    -   **A:** Calculate Max Subarray (linear) AND (Total Sum - Min Subarray). Return the max of the two.

5.  **Q:** What is the invariant for the `Low`, `Mid`, `High` pointers in DNF?
    -   **A:** `0` to `Low-1` are Reds. `High+1` to `End` are Blues. `Low` to `Mid-1` are Whites. `Mid` to `High` are Unknown.

6.  **Q:** Solve "First Missing Positive" in O(N) time and O(1) space.
    -   **A:** Use Cyclic Sort to place `1` at `0`, `2` at `1`, etc. Ignore numbers `<=0` or `>N`. First index `i` where `arr[i] != i+1` is the answer.

### ❌ Common Misconceptions

1.  **"Kadane's handles non-contiguous subarrays"**
    -   *Correction:* No, Kadane's is strictly for *contiguous* subarrays. For non-contiguous (subsequence), just sum all positive numbers.
2.  **"Cyclic Sort is O(N²)"**
    -   *Correction:* It looks like nested loops, but each swap places one number correctly forever. Total swaps <= N. Complexity is O(N).
3.  **"Mid always moves"**
    -   *Correction:* In DNF, `Mid` moves ONLY when swapping with `Low` or encountering `1`. It stays put when swapping with `High`.

### 🚀 Advanced Concepts
-   **Introselect:** A hybrid of QuickSelect (Partitioning) and Median of Medians to guarantee O(N) selection.
-   **K-Kadane:** Finding the K-th largest subarray sum (requires more complex structures).

### 🔗 External Resources
1.  **Wikipedia:** "Dutch National Flag Problem" (Edsger Dijkstra).
2.  **VisualAlgo:** "Sorting" (Watch Quick Sort partition step).
3.  **YouTube:** "Kadane's Algorithm Explained" (Back to Back SWE).

---

## 🎯 RETENTION HOOK

### 🪝 The "One-Liner" Essence
> **"Categorize data in-place by swapping to boundaries; maximize sums by dropping debt immediately."**

### 🧠 Memory Aid: "D.C.K."
- **D**utch Flag (3-way partition)
- **C**yclic Sort (Index mapping)
- **K**adane (Drop negative prefix)

**Final Thought:** These are the "Physics" of arrays. They don't use fancy data structures; they exploit the raw mathematical properties of indices and sums. Mastery here shows deep algorithmic intuition.

---

**End of Instructional File**  
*Output generated strictly following Template v10 and System Config v10.*
# 🎯 WEEK 5 DAY 4A: PARTITION & CYCLIC SORT — COMPLETE GUIDE

**Category:** Critical Patterns / Array Sorting In-Place  
**Difficulty:** 🟡 Medium  
**Prerequisites:** Week 2 (Arrays), Week 4 (Two Pointers)  
**Interview Frequency:** 60% (High — Frequently used in "missing number" and "sort colors" style problems)  
**Real-World Impact:** Memory-constrained systems, garbage collection algorithms, database in-place compaction

---

## 🎓 LEARNING OBJECTIVES

By the end of this topic, you will be able to:

- ✅ Master the **Dutch National Flag (DNF)** partitioning algorithm for 3-way sorting  
- ✅ Understand **Cyclic Sort** to place elements in correct indices in O(N) time  
- ✅ Solve "Find Missing/Duplicate Number" problems in O(1) extra space  
- ✅ Apply partition logic to "Move Zeroes" and other segregation problems  
- ✅ Distinguish when to use Cyclic Sort (range 1 to N) vs Standard Sort

---

## 🤔 SECTION 1: THE WHY — Engineering Motivation

### 🎯 Real-World Problems This Solves

#### Problem 1: Memory Management (Garbage Collection)

- **Domain:** Runtime Environments (Java JVM, Go Runtime, .NET)  
- **Challenge:** Efficiently segregate "live" objects from "dead" objects during garbage collection cycles to reclaim memory.  
- **Impact:** Reduces pause times (Stop-The-World) in applications.  
- **Example System:** The "Mark-Sweep-Compact" GC algorithms often use partition logic similar to Dutch National Flag to group live objects together in memory, reducing fragmentation.

#### Problem 2: Database Storage Optimization (Compaction)

- **Domain:** Database Engines (PostgreSQL, MySQL)  
- **Challenge:** Remove "deleted" records (tombstones) from data pages without allocating new pages.  
- **Impact:** Keeps disk usage low and query scans fast.  
- **Example System:** LSM-Tree based databases (like RocksDB) perform compaction steps that merge and partition data to remove obsolete versions of records.

#### Problem 3: Data Cleaning & Deduplication

- **Domain:** Data Pipelines (ETL)  
- **Challenge:** Identify missing sequences or duplicate entries in huge streams of ID-based data (e.g., transaction IDs 1 to 1,000,000).  
- **Impact:** Ensures data integrity for financial reporting.  
- **Example System:** High-performance data ingestion services use cyclic-sort style logic to detect gaps in sequence numbers without expensive sorting.

### ⚖ Design Problem & Trade-offs

**What design problem does this solve?**

How do we sort or segregate data **in-place** with **O(1) extra space** and **O(N) time**, specifically when the data has known constraints (like a limited range of values)?

**Main goals:**

- **Space Efficiency:** Zero external allocations (no new arrays).  
- **Time Efficiency:** Linear pass O(N), avoiding O(N log N) sorting.  
- **Cache Friendliness:** Operations happen locally on the array.

**What we give up:**

- **Stability:** Partitioning is typically unstable (relative order of identical elements changes).  
- **Generality:** Cyclic sort only works on specific integer ranges (e.g., 1 to N). It cannot sort generic floating-point numbers or arbitrary strings.

### 💼 Interview Relevance

**Common question archetypes:**

- "Sort an array of 0s, 1s, and 2s." (Dutch National Flag)  
- "Move all zeroes to the end of the array."  
- "Find the missing number in an array containing 1 to N."  
- "Find the duplicate number in an array of 1 to N."  
- "Find the first missing positive integer." (Hard variant)

**What interviewers test:**

- **In-place manipulation:** Can you modify the array without making a copy?  
- **Pointer management:** Can you handle multiple pointers (low, mid, high) without off-by-one errors?  
- **Index mapping:** Can you map a value to its correct index (value minus 1)?  
- **Edge cases:** Empty arrays, arrays with all same elements, already sorted arrays.

---

## 📌 SECTION 2: THE WHAT — Mental Model & Core Concepts

### 🧠 Core Analogy 1: The Dutch National Flag (Partitioning)

Imagine you are organizing a chaotic line of people wearing **Red**, **White**, and **Blue** shirts. You want them lined up in that exact order (Red first, then White, then Blue).

**Strategy:** You stand in the middle. You have three zones:
1.  **Red Zone (Left):** Confirmed Red shirts.
2.  **Blue Zone (Right):** Confirmed Blue shirts.
3.  **Unknown Zone (Middle):** People you haven't sorted yet.

You look at the person at the front of the Unknown Zone:
-   If **Red**: Swap them to the Red Zone (left) and expand the Red Zone.
-   If **Blue**: Swap them to the Blue Zone (right) and shrink the Unknown Zone.
-   If **White**: They are already in the middle (correct relative to Red), so just move to the next person.

### 🧠 Core Analogy 2: Cyclic Sort (The "Wrong Seat" Game)

Imagine a classroom with numbered seats 1 to N. Students also have jersey numbers 1 to N. Currently, everyone is sitting randomly.

**Goal:** Get student #5 into seat #5, student #3 into seat #3, etc.

**Strategy:**
Go to seat #1. Ask the student there: "What is your number?"
-   If they are Student #1: Great, move to seat #2.
-   If they are Student #5: Say "You belong in seat #5." Swap them with whoever is currently in seat #5.
-   **Crucial Step:** Don't move to seat #2 yet! Check who you just received from seat #5. They might also be in the wrong seat. Keep swapping until seat #1 has Student #1 (or a number that doesn't belong anywhere, like a duplicate or out-of-bounds).

### 🔑 Core Invariants

**Invariant 1: Partitioning Boundaries**
In Dutch National Flag, we maintain three pointers:
-   `low`: The boundary of the 0s (everything `< low` is 0).
-   `high`: The boundary of the 2s (everything `> high` is 2).
-   `mid`: The current element being examined.
-   **Invariant:** `[0...low-1]` are 0s, `[low...mid-1]` are 1s, `[mid...high]` are unknown, `[high+1...N-1]` are 2s.

**Invariant 2: Cyclic Placement**
In Cyclic Sort, for every index `i`:
-   Ideally, `nums[i] == i + 1` (if 1-based) or `nums[i] == i` (if 0-based).
-   If `nums[i]` is not at its correct position `correct_index`, swap it to `correct_index`.

### 📋 Core Concepts & Variations

#### 1. Dutch National Flag (3-Way Partition)
-   **What it is:** Sort an array of three distinct values (e.g., 0, 1, 2) in one pass.
-   **Pointers:** `low`, `mid`, `high`.
-   **Action:** Swap `mid` with `low` (if 0), swap `mid` with `high` (if 2), or just increment `mid` (if 1).

#### 2. Move Zeroes (2-Way Partition)
-   **What it is:** Move all specific elements (like 0) to end while maintaining relative order of non-zeroes.
-   **Pointers:** `read`, `write`.
-   **Action:** If `read` sees non-zero, write to `write` and increment `write`.

#### 3. Standard Cyclic Sort
-   **What it is:** Sort array of 1 to N in O(N) time.
-   **Logic:** Iterate `i`. While `nums[i]` is not at correct index, swap `nums[i]` with `nums[nums[i] - 1]`.

#### 4. Find Missing Number
-   **What it is:** Given 0 to N with one missing, find it.
-   **Logic:** Run Cyclic Sort. Then scan: first index `i` where `nums[i] != i` is the missing number.

#### 5. Find Duplicate Number
-   **What it is:** Given 1 to N with one duplicate.
-   **Logic:** During Cyclic Sort swap, if `nums[i]` is same as target, duplicate found. Or Fast & Slow pointers (Floyd's) if read-only.

#### 6. First Missing Positive
-   **What it is:** Find smallest positive integer missing from unsorted array (hard variant).
-   **Logic:** Ignore negatives and numbers > N. Cyclic sort valid positives 1..N to indices 0..N-1. First index mismatch is answer.

### 📊 Concept Summary Table

| Pattern | Input Constraint | Time Complexity | Space Complexity | Key Mechanism |
| :--- | :--- | :--- | :--- | :--- |
| **Dutch National Flag** | 3 distinct values (0,1,2) | O(N) | O(1) | 3 Pointers (Low, Mid, High) |
| **Move Zeroes** | Any array | O(N) | O(1) | 2 Pointers (Read/Write) |
| **Cyclic Sort** | Numbers in range 1 to N | O(N) | O(1) | Swap to Correct Index |
| **Find Missing** | Range 0 to N | O(N) | O(1) | Cyclic Sort + Linear Scan |
| **First Missing Positive**| Unsorted Integers | O(N) | O(1) | Ignore invalid ranges, cycle sort rest |

---

## ⚙ SECTION 3: THE HOW — Mechanical Walkthrough

### 🔧 Operation 1: Dutch National Flag Algorithm

**Input:** `[2, 0, 2, 1, 1, 0]`  
**Goal:** Sort to `[0, 0, 1, 1, 2, 2]`

**Algorithm:**
1.  Initialize `low = 0`, `mid = 0`, `high = N - 1`.
2.  While `mid <= high`:
    -   If `nums[mid] == 0`: Swap `nums[mid]` and `nums[low]`. Increment `low`, Increment `mid`.
    -   If `nums[mid] == 1`: Correct place. Increment `mid`.
    -   If `nums[mid] == 2`: Swap `nums[mid]` and `nums[high]`. Decrement `high`. **Do NOT increment mid** (we need to check what we just swapped from the right).

**Trace:**
`[2, 0, 2, 1, 1, 0]`, L=0, M=0, H=5
-   M is 2: Swap with H(0). Array: `[0, 0, 2, 1, 1, 2]`. H becomes 4. M stays 0.
-   M is 0: Swap with L(0). No change visually. L=1, M=1.
-   M is 0: Swap with L(0). Array: `[0, 0, 2, 1, 1, 2]`. L=2, M=2.
-   M is 2: Swap with H(1). Array: `[0, 0, 1, 1, 2, 2]`. H=3. M stays 2.
-   M is 1: Correct. M=3.
-   M is 1: Correct. M=4.
-   Loop ends (M > H).

### 🔧 Operation 2: Cyclic Sort (Standard)

**Input:** `[3, 1, 5, 4, 2]` (Range 1 to 5)  
**Goal:** Sort to `[1, 2, 3, 4, 5]`

**Algorithm:**
1.  Iterate `i` from 0 to N-1.
2.  While `nums[i]` is not at correct index (i.e., `nums[i] != i + 1`):
    -   Target index `d = nums[i] - 1`.
    -   If `nums[i] == nums[d]`: Break (duplicate handling) or Skip.
    -   Swap `nums[i]` with `nums[d]`.
    -   Repeat check for new `nums[i]`.
3.  Move to next `i`.

**Trace:**
`i = 0`, Val = 3. Correct spot is index 2.
-   Swap `nums[0]` (3) with `nums[2]` (5). Array: `[5, 1, 3, 4, 2]`.
-   `i = 0`, Val = 5. Correct spot is index 4.
-   Swap `nums[0]` (5) with `nums[4]` (2). Array: `[2, 1, 3, 4, 5]`.
-   `i = 0`, Val = 2. Correct spot is index 1.
-   Swap `nums[0]` (2) with `nums[1]` (1). Array: `[1, 2, 3, 4, 5]`.
-   `i = 0`, Val = 1. Correct spot is index 0. Match!
-   Move to `i = 1`. Val = 2. Correct.
-   Move to `i = 2`. Val = 3. Correct.
-   ... Done.

### 💾 Memory Behavior

**In-Place Nature:**
Both algorithms modify the original array directly.
-   **Stack:** Minimal (only variables `i`, `low`, `mid`, `high`, `temp`).
-   **Heap:** No new allocations.

**Cache Locality:**
-   **DNF:** Very good. `low` and `mid` move forward. `high` moves backward. Swaps are local.
-   **Cyclic Sort:** Moderate. Swapping `nums[i]` with `nums[target]` causes random access jumps if the target is far away (random reads/writes). Less cache-friendly than linear scans but still O(N).

### 🛡 Edge Cases

| Edge Case | Behavior | Handling |
| :--- | :--- | :--- |
| **Empty Array** | No ops | Return immediately. |
| **All Duplicates** | Loops continue | Ensure termination condition (e.g., `nums[i] == nums[target]`) prevents infinite swapping. |
| **Already Sorted** | O(N) scan | Algorithm validates but performs 0 swaps. |
| **Values Out of Range** | Cyclic Sort fails | For First Missing Positive, ignore numbers `<= 0` or `> N`. |

---

## 🎨 SECTION 4: VISUALIZATION — Simulation & Examples

### 🧊 Example 1: Move Zeroes (Partitioning Variant)

**Input:** `[0, 1, 0, 3, 12]`

**Pointers:**
`write`: Position where the next non-zero should go. Starts at 0.
`read`: Current element scanner. Starts at 0.

**Trace:**
1.  `read=0`, Val=0. Ignore.
2.  `read=1`, Val=1. Non-zero!
    -   Swap `nums[write]` with `nums[read]`. Array: `[1, 0, 0, 3, 12]`.
    -   Increment `write` to 1.
3.  `read=2`, Val=0. Ignore.
4.  `read=3`, Val=3. Non-zero!
    -   Swap `nums[write]` with `nums[read]`. Array: `[1, 3, 0, 0, 12]`.
    -   Increment `write` to 2.
5.  `read=4`, Val=12. Non-zero!
    -   Swap `nums[write]` with `nums[read]`. Array: `[1, 3, 12, 0, 0]`.
    -   Increment `write` to 3.

**Result:** `[1, 3, 12, 0, 0]`

### 📈 Example 2: First Missing Positive (Cyclic Sort Variant)

**Input:** `[3, 4, -1, 1]`  
**Range of Interest:** 1 to 4 (Length is 4). Ignore negatives.

**Cyclic Logic:**
-   `i=0`, Val=3. Target index 2. Swap 3 with -1. `[-1, 4, 3, 1]`.
-   `i=0`, Val=-1. Ignore (out of range). Move to `i=1`.
-   `i=1`, Val=4. Target index 3. Swap 4 with 1. `[-1, 1, 3, 4]`.
-   `i=1`, Val=1. Target index 0. Swap 1 with -1. `[1, -1, 3, 4]`.
-   `i=1`, Val=-1. Ignore. Move to `i=2`.
-   `i=2`, Val=3. Correct (index 2+1=3).
-   `i=3`, Val=4. Correct (index 3+1=4).

**Final Scan:**
-   Index 0: Val 1 (Correct).
-   Index 1: Val -1 (Expected 2). **MISMATCH**.

**Answer:** 2 is the first missing positive.

---

## 📊 SECTION 5: CRITICAL ANALYSIS — Performance & Robustness

### 📈 Complexity Table

| Algorithm | Best Case Time | Worst Case Time | Space | Stability |
| :--- | :--- | :--- | :--- | :--- |
| **DNF Partition** | O(N) | O(N) | O(1) | Unstable |
| **Cyclic Sort** | O(N) | O(N) | O(1) | Unstable |
| **Move Zeroes** | O(N) | O(N) | O(1) | Stable (usually) |
| **Find Missing** | O(N) | O(N) | O(1) | N/A |

### 🤔 Why Big-O Might Mislead Here

**Swap Costs:**
Although DNF and Cyclic Sort are both O(N), "N" represents operations.
-   **DNF:** Each element is swapped at most once into its final zone.
-   **Cyclic Sort:** In the worst case (a purely rotated array), you might perform N-1 swaps. However, every swap places at least one number in its *permanent correct position*. Therefore, the total number of swaps across the entire algorithm is limited to N. The inner `while` loop runs N times *in total* across all iterations, not N times *per* iteration.

**Real-world speed:**
DNF is extremely fast because it is a single pass with predictable branches. Cyclic sort involves random memory access (jumping to `nums[nums[i]-1]`), which can cause cache misses for large N.

### ⚠ Edge Cases & Failure Modes

**Failure Mode 1: Infinite Loops in Cyclic Sort**
-   **Scenario:** Duplicate numbers exist in the input when the problem assumes distinct numbers.
-   **Risk:** `nums[i]` tries to swap with `nums[target]`, but `nums[target]` is the same value. The algorithm keeps swapping identical values forever.
-   **Fix:** Always check `if nums[i] != nums[target]` before swapping.

**Failure Mode 2: Integer Overflow in Index Calculation**
-   **Scenario:** Values are very large, close to MAX_INT.
-   **Risk:** Calculating `nums[i] - 1` is usually fine, but if input contains `MIN_INT`, `abs()` or offset logic might crash.
-   **Fix:** Validate range `1 <= nums[i] <= N` strictly before computing index.

---

## 🏭 SECTION 6: REAL SYSTEMS — Integration in Production

### 🏭 Real System 1: Elasticsearch / Lucene (Segment Merging)

**Domain:** Search Engine Indexing
**Context:** When merging index segments, Lucene needs to delete documents marked as "deleted" (tombstones).
**Application:** It effectively uses a variation of the **Move Zeroes** (partitioning) logic. It iterates through the document IDs, sliding valid documents to fill gaps left by deleted ones, creating a dense, compacted segment. This is done in-place (or streaming to new file) to minimize IO.

### 🏭 Real System 2: Java G1 Garbage Collector

**Domain:** Memory Management
**Context:** The G1 collector divides heap into regions. During a "mixed collection," it evacuates live objects from one region to another.
**Application:** Within a region, to reduce fragmentation, it uses **compaction** (similar to partitioning). Live objects are moved to one end of the memory block (Partitioning 0s), and the rest is freed. This ensures contiguous free memory for fast allocation (pointer bumping).

### 🏭 Real System 3: Redis (List Pack / IntSet)

**Domain:** In-memory Data Store
**Context:** Redis optimizes storage for small lists or sets of integers.
**Application:** When inserting into a sorted integer set (`IntSet`), if the value fits inside the existing array, it may shift elements to make space. While not exactly cyclic sort, the **in-place shifting** logic mirrors the pointer manipulation skills learned in partitioning.

---

## 🔗 SECTION 7: CONCEPT CROSSOVERS — Connections & Comparisons

### 📚 What It Builds On (Prerequisites)

**1. Two Pointers (Week 4):**
-   DNF is essentially a "Three Pointer" approach. It extends the read/write logic of standard two-pointer problems.

**2. Direct Addressing / Hashing (Week 5 Day 1):**
-   Cyclic sort is "In-place Hashing". Instead of a separate boolean array or HashSet to track seen numbers, we use the index of the array itself as the hash key (`index = value - 1`).

### 🚀 What Builds On It (Successors)

**1. QuickSort (Week 3 / Algorithm Paradigms):**
-   QuickSort's partitioning step IS the DNF logic (or 2-way variant). Mastering DNF makes implementing QuickSort trivial.

**2. Floyd's Cycle Detection (Week 5 Day 5):**
-   The "Find Duplicate Number" problem can be solved with Cyclic Sort (modifies array) OR Floyd's Tortoise and Hare (read-only). These are often taught together as alternative approaches.

**3. Manacher's Algorithm (Advanced Strings):**
-   While distinct, the idea of using known boundaries (low/high) to optimize processing is a shared mental model.

### 🔄 Comparison with Alternatives

| Approach | Time | Space | Best For |
| :--- | :--- | :--- | :--- |
| **Cyclic Sort** | O(N) | O(1) | Range 1..N, Finding Missing/Dupes, In-place allowed. |
| **HashSet** | O(N) | O(N) | Generic numbers (not 1..N), Read-only required. |
| **Sorting (Merge/Quick)** | O(N log N) | O(log N) | Generic numbers, Output must be sorted. |
| **Boolean Array** | O(N) | O(N) | Simple check, if extra space is cheap. |

---

## 📐 SECTION 8: MATHEMATICAL & THEORETICAL PERSPECTIVE

### 📋 Formal Definition

**Partitioning:** Given a predicate P, rearrange array A such that all elements satisfying P come before all elements not satisfying P.
**DNF:** Given predicates P1 (val=0), P2 (val=1), P3 (val=2), rearrange such that `P1(A[0..i])` is true, `P2(A[j..k])` is true, `P3(A[l..N])` is true.

### 📐 Key Theorem

**Theorem: Minimum Swaps for Partitioning**
The Dutch National Flag algorithm performs at most N swaps.
-   Every swap of a '0' advances the `low` pointer.
-   Every swap of a '2' decrements the `high` pointer.
-   Since `low` and `high` meet, the total swaps cannot exceed N.

**Theorem: Cycle Decomposition**
Any permutation can be decomposed into disjoint cycles.
Cyclic Sort works by resolving these cycles one by one. If a cycle has length K, it requires K-1 swaps to place all elements in that cycle correctly. The sum of (K-1) for all cycles is bounded by N.

---

## 💡 SECTION 9: ALGORITHMIC DESIGN INTUITION

### 🎯 Decision Framework

**When to use Dutch National Flag?**
-   Is the input composed of a **small, fixed number of categories** (e.g., Low/Med/High, 0/1/2, RGB)?
-   Do we need **O(1) space**?
-   Is **stability** not required?
-   *Decision:* Yes -> DNF Partition.

**When to use Cyclic Sort?**
-   Does the problem involve an array of numbers in a **fixed range** (1 to N, or 0 to N)?
-   Does the problem ask for **missing**, **duplicate**, or **misplaced** numbers?
-   Is **modifying the array** allowed?
-   *Decision:* Yes -> Cyclic Sort.

**When NOT to use them?**
-   If the range of numbers is large (e.g., values 1 to 1,000,000 in an array of size 100) -> Cyclic sort fails (index out of bounds).
-   If input is immutable -> Must use HashSet or Copy.

### 🔍 Interview Pattern Recognition

**Clue 1:** "You are given an array containing `n` objects colored red, white, or blue..."
-   **Pattern:** DNF Partition (Sort Colors).

**Clue 2:** "Given an unsorted array of integers 1 to n..."
-   **Pattern:** Cyclic Sort.

**Clue 3:** "Find the smallest missing positive integer..."
-   **Pattern:** Cyclic Sort (ignoring negatives).

**Clue 4:** "Reorder elements in-place..."
-   **Pattern:** Partitioning pointers.

---

## ❓ SECTION 10: KNOWLEDGE CHECK — Socratic Reasoning

1.  **In the Dutch National Flag algorithm, why do we NOT increment `mid` when we swap with `high`?**
    *Hint: Consider what value might be sitting at `nums[high]` before the swap. Have we inspected it yet?*

2.  **In Cyclic Sort, what is the worst-case number of swaps for an array of size N?**
    *Hint: Think about an array that is shifted by 1 position (e.g., `[2, 3, 4, 5, 1]`).*

3.  **Why can't we use Cyclic Sort to sort an array like `[100, 20, 5]`?**
    *Hint: How do the values map to indices?*

4.  **How would you modify the Partition logic to sort an array of simply 0s and 1s?**
    *Hint: Do you need `mid` and `high`, or just `read` and `write`?*

5.  **If an array contains duplicates (e.g., `[1, 4, 4, 3]`), how does Cyclic Sort detect them?**
    *Hint: What happens when you try to swap 4 to index 3, but index 3 already has a 4?*

---

## 🎯 SECTION 11: RETENTION HOOK — Memory Anchors

### 💎 One-Liner Essence

"**Partitioning is about organizing chaos into zones; Cyclic Sort is about checking tickets and sending people to their assigned seats.**"

### 🧠 Mnemonic Device

**"L-M-H" for DNF:**
-   **L**ow: Holds 0s.
-   **M**id: Scans the unknown.
-   **H**igh: Holds 2s.
-   **Rule:** Mid swaps with Low (step both), Mid swaps with High (step High only), Mid steps alone (for 1s).

### 🖼 Visual Cue

**The Traffic Light:**
-   **Red (0):** Stop/Left.
-   **Yellow (1):** Yield/Middle.
-   **Green (2):** Go/Right.
-   Algorithm organizes cars into Red lane, Yellow lane, Green lane.

### 💼 Real Interview Story

**Context:** Candidate asked to "Find the first missing positive" in an unsorted array.
**Mistake:** They tried to use a HashSet.
**Interviewer:** "Can you do this with O(1) space?"
**Pivot:** Candidate realized the numbers themselves could act as indices. They switched to **Cyclic Sort**, placing 5 at index 4, 3 at index 2, etc. Then scanned for the first gap.
**Outcome:** Hire. Strong signal on "In-place data mapping."

---

## ⚔ SUPPLEMENTARY OUTCOMES

### ⚔ Practice Problems (10)

1.  **Sort Colors** (LeetCode 75) - *The classic DNF problem.*
2.  **Move Zeroes** (LeetCode 283) - *2-way partition.*
3.  **Missing Number** (LeetCode 268) - *Cyclic sort introduction.*
4.  **Find the Duplicate Number** (LeetCode 287) - *Cyclic sort or Floyds.*
5.  **First Missing Positive** (LeetCode 41) - *Advanced cyclic sort (ignoring ranges).*
6.  **Find All Numbers Disappeared in an Array** (LeetCode 448) - *Cyclic sort variant.*
7.  **Find All Duplicates in an Array** (LeetCode 442) - *Cyclic sort variant.*
8.  **Partition Array according to Given Pivot** (LeetCode 2161) - *Stable partition.*
9.  **Set Mismatch** (LeetCode 645) - *Find duplicate and missing simultaneously.*
10. **Couples Holding Hands** (LeetCode 765) - *Advanced cyclic swapping concept.*

### 🎙 Interview Questions (6+)

**Q1: Can you explain why Dutch National Flag is one-pass?**
**Q2: How does Cyclic Sort achieve O(N) if there is a nested while loop?**
**Q3: Modify DNF to sort 4 colors. How many pointers do you need?**
**Q4: In "First Missing Positive", how do you handle negative numbers?**
**Q5: Compare the cache performance of Cyclic Sort vs QuickSort.**
**Q6: If the input array is immutable (read-only), how does your approach to "Find Duplicate" change?**

### ⚠ Common Misconceptions (5)

1.  **"Cyclic Sort works on any array."** -> No, only when values map densely to indices (1 to N).
2.  **"DNF requires 2 passes."** -> No, it is designed to be 1 pass. Counting sort is 2 passes.
3.  **"Swapping is always expensive."** -> In-memory swaps are cheap; avoiding O(N) auxiliary space is often more valuable.
4.  **"You increment `mid` after swapping with `high`."** -> Fatal bug. The new value at `mid` (from `high`) hasn't been checked yet!
5.  **"Move zeroes requires sorting."** -> Overkill. Partitioning is O(N).

### 🚀 Advanced Concepts (5)

1.  **Introselect Algorithm:** Uses partitioning to find Kth smallest element (QuickSelect).
2.  **Wiggle Sort:** Using partition-like logic to create `nums[0] < nums[1] > nums[2] < ...`
3.  **Stable Partitioning:** Doing the partition while keeping original relative order (requires O(N) buffer or rotation logic).
4.  **In-place Matrix Rotation:** Similar "cycle" logic used to rotate image layers.
5.  **Permutation Cycles:** The mathematical foundation of why cyclic sort terminates.

### 🔗 External Resources (5)

1.  **Wikipedia: Dutch National Flag Problem** - *Historical context by Dijkstra.*
2.  **LeetCode Discuss: "Cyclic Sort Pattern"** - *Compilation of related problems.*
3.  **VisualAlgo: Sorting** - *Visualize partition steps.*
4.  **GeeksForGeeks: Sort an array of 0s, 1s and 2s** - *Standard reference.*
5.  **YouTube: "Cycle Sort Algorithm" by Abdul Bari** - *Deep dive into stability and swaps.*

---

**Generated:** January 03, 2026
**Version:** Template v10.0 Mental-Model-First
**File:** Week_05_Day_4A_Partition_Cyclic_Sort_Instructional.md
**Status:** ✅ Ready for Review
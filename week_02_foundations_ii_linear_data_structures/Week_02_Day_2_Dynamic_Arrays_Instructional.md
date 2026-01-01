# 🎯 WEEK 2 DAY 2: DYNAMIC ARRAYS — COMPLETE GUIDE

**Category:** Data Structures / Foundations  
**Difficulty:** 🟢 Foundation  
**Prerequisites:** Week 2 Day 1 (Static Arrays)  
**Interview Frequency:** ~90% (Often implied when using Python Lists, Java ArrayList, C++ Vectors)  
**Real-World Impact:** The default "list" in almost every modern language. Powers everything from JSON parsers to UI lists.

---

## 🎓 LEARNING OBJECTIVES

By the end of this topic, you will be able to:

- ✅ Explain how Dynamic Arrays grow automatically (Resizing strategy).
- ✅ Analyze **Amortized Complexity** and why append is O(1) *on average* despite occasional O(n) copying.
- ✅ Implement a basic Dynamic Array from scratch (conceptually).
- ✅ Understand the difference between **Capacity** (allocated space) and **Size** (used space).
- ✅ Recognize the trade-off between **wasted memory** (over-allocation) and **resizing cost** (CPU time).

---

## 🤔 SECTION 1: THE WHY — Engineering Motivation

**Purpose:** Motivate dynamic arrays with concrete engineering problems and trade-offs.

### 🎯 Real-World Problems This Solves

#### **Problem 1: The "User Input" Unknown**
- 🌍 **Where:** Web Forms, Command Line Tools
- 💼 **Why it matters:** You ask a user to "Enter numbers". You don't know if they will enter 5 numbers or 5,000.
- 🔧 **Static Array Failure:** If you allocate `int[100]` and they enter 101, program crashes. If you allocate `int[1000000]`, you waste RAM.
- 🔧 **Solution:** Dynamic Array starts small (size 4) and grows automatically as needed.

#### **Problem 2: Reading Files of Unknown Size**
- 🌍 **Where:** Text Editors, Log Parsers
- 💼 **Why it matters:** Loading a file into memory line-by-line. You don't know the file size upfront.
- 🔧 **Solution:** `List<string>` in C# / `ArrayList` in Java handles the growth. You just call `.Add()`.

#### **Problem 3: Building JSON Objects**
- 🌍 **Where:** API Responses
- 💼 **Why it matters:** A JSON array `[...]` can contain any number of elements. The parser needs a flexible container.
- 🔧 **Solution:** Dynamic arrays allow appending elements one by one as they are parsed.

### ⚖ Design Problem & Trade-offs

**Core Design Problem:** How do we give the *illusion* of infinite size while using fixed-size hardware memory blocks?

**The Challenge:**
- **Hardware Reality:** RAM only gives fixed-size contiguous blocks. You cannot "stretch" a block if the neighbor bytes are occupied.
- **Performance:** Copying elements to a new, larger block is slow (O(n)).

**Main Goals:**
- **Flexibility:** Grow as needed.
- **Speed:** Maintain O(1) access and O(1) *average* insert.
- **Convenience:** Abstraction hides the ugly memory management.

**What We Give Up:**
- **Predictability:** Most appends are fast, but *some* are very slow (latency spikes during resize).
- **Memory Efficiency:** We almost always allocate more than we use (Capacity > Size).

### 💼 Interview Relevance

- **The "Implement ArrayList" Question:** Very common low-level design question.
- **Complexity Analysis:** Asking "What is the worst-case insert?" (O(n)) vs "Average insert?" (O(1)).
- **Language Internals:** "How does Python's `list` work?" (It's a dynamic array, not a linked list!).

---

## 📌 SECTION 2: THE WHAT — Mental Model & Core Concepts

**Purpose:** Build a mental picture: analogy, shape, invariants, and key variations.

### 🧠 Core Analogy

> **"The Dynamic Array is like a Hermit Crab finding shells."**
>
> 1. **Small Shell:** The crab (your data) starts in a small shell (Capacity = 4).
> 2. **Growth:** The crab grows. Eventually, the shell is full (Size = 4).
> 3. **The Swap:** To grow more, the crab must find a **new, bigger shell** (Capacity = 8).
> 4. **The Move:** It painstakingly moves *all* its body parts (copying elements) to the new shell.
> 5. **Discard:** The old shell is abandoned (garbage collected/freed).
> 6. **Room to Grow:** Now it has empty space again.

### 🖼 Visual Representation

**Resizing Process**

```text
Step 1: Initial (Cap: 4, Size: 4)
[A, B, C, D]  (Full!)

Step 2: Add 'E' -> Trigger Resize!
Create New Array (Cap: 8)
[_, _, _, _, _, _, _, _]

Step 3: Copy Old Elements
[A, B, C, D, _, _, _, _]

Step 4: Add New Element 'E'
[A, B, C, D, E, _, _, _] (Size: 5, Cap: 8)

Step 5: Discard Old Array
[A, B, C, D] -> 🗑 Garbage
```

### 🔑 Core Invariants

1. **Size <= Capacity:** The number of valid elements (`Size`) can never exceed the allocated space (`Capacity`).
2. **Contiguity:** Underlying storage is *always* a standard contiguous static array.
3. **Geometric Growth:** When full, capacity typically multiplies by a factor (usually 1.5x or 2x), not by a fixed constant (+10). This is critical for O(1) amortized performance.

### 📋 Core Concepts & Variations (List All)

#### 1. **Capacity vs Size**
- **Size (Count):** Number of elements user "sees".
- **Capacity:** Number of slots actually reserved in RAM.

#### 2. **Growth Factor (Doubling Strategy)**
- **Why double?** Why not `Capacity + 10`?
- **Doubling:** Ensures we resize less and less frequently. Total copy cost is proportional to N (Amortized O(1)).
- **Fixed Increment:** Resizes constantly. Total copy cost becomes O(N^2).

#### 3. **Shrinking (Optional)**
- **Concept:** If `Size < Capacity / 4`, reduce capacity by half.
- **Why:** To save memory.
- **Hysteresis:** Don't shrink at exactly 50% usage to prevent "thrashing" (resize-shrink-resize cycle).

#### 4. **Language Implementations**
- **C++:** `std::vector` (Growth: 2x)
- **Java:** `ArrayList` (Growth: 1.5x)
- **Python:** `list` (Growth: ~1.125x + constant)
- **C#:** `List<T>` (Growth: 2x)

#### 📊 Concept Summary Table

| # | 🧩 Concept | ✏️ Description | ⏱ Key Characteristic | 💾 Space Overhead |
|---|-----------|----------------|----------------------|-------------------|
| 1 | **Capacity** | Total slots allocated | - | O(Capacity) |
| 2 | **Size** | Slots currently used | - | - |
| 3 | **Resize** | Alloc new → Copy → Swap | O(N) (One time) | Peaks at 3x N during copy |
| 4 | **Append** | Add to end | O(1) Amortized | None (usually) |

---

## ⚙ SECTION 3: THE HOW — Mechanical Walkthrough

**Purpose:** Show how dynamic growth works step-by-step.

### 🧱 State / Data Structure

```csharp
class DynamicArray<T> {
    T[] arr;      // Underlying static array
    int size;     // User-facing count
    int capacity; // arr.Length
}
```

### 🔧 Operation 1: Append (Add to End)

**Logic:**
```text
Operation: Append(value)
Step 1: Check if size == capacity.
    If NO:
        arr[size] = value
        size++
    If YES (Full):
        1. NewCapacity = capacity * 2
        2. NewArr = new T[NewCapacity]
        3. Copy arr[0..size-1] to NewArr
        4. arr = NewArr
        5. capacity = NewCapacity
        6. arr[size] = value
        7. size++
```
- **Time:** Usually O(1). Occasionally O(N).
- **Amortized Time:** O(1).

### 🔧 Operation 2: Get / Set (Random Access)

**Logic:**
```text
Operation: Get(index)
Step 1: Check bounds (0 <= index < size)
Step 2: Return arr[index]
```
- **Time:** O(1). Same as static array.

### 🔧 Operation 3: Insert (Middle)

**Logic:**
```text
Operation: Insert(index, value)
Step 1: Resize if full (same as Append logic).
Step 2: Shift elements [index..size-1] to right.
Step 3: arr[index] = value.
Step 4: size++.
```
- **Time:** O(N) due to shifting (and potential resize).

### 🔧 Operation 4: RemoveAt (Delete)

**Logic:**
```text
Operation: RemoveAt(index)
Step 1: Check bounds.
Step 2: Shift elements [index+1..size-1] to left.
Step 3: size--.
Step 4: (Optional) Shrink if size is very small compared to capacity.
```
- **Time:** O(N) due to shifting.

### 💾 Memory Behavior

- **Over-allocation:** `List` often consumes 2x more memory than strictly needed.
- **GC Pressure:** Resizing creates a large "garbage" array (the old one) that the Garbage Collector must clean up. Frequent resizing of large lists causes GC spikes.
- **Locality:** Excellent. Despite resizing, data is always contiguous.

### 🛡 Edge Cases

1. **Initial Capacity 0:** If capacity starts at 0, doubling (0 * 2) stays 0.
   - *Fix:* Logic must handle 0 -> 1 or start with default (e.g., 4).
2. **Integer Overflow:** If capacity is `2 Billion`, `capacity * 2` overflows `int`.
   - *Fix:* Check `MaxArrayLength` limit.

---

## 🎨 SECTION 4: VISUALIZATION — Simulation & Examples

**Purpose:** Visualize the cost amortization.

### 🧊 Example 1: The "Doubling" Cost

**Scenario:** Start Cap=1. Append 5 elements.

| Step | Operation | Size | Capacity | Cost (Writes) | Note |
|------|-----------|------|----------|---------------|------|
| 1 | Add 'A' | 1 | 1 | 1 | Fill |
| 2 | Add 'B' | 2 | 2 | 1 (Copy A) + 1 (Write B) = 2 | **Resize 1->2** |
| 3 | Add 'C' | 3 | 4 | 2 (Copy A,B) + 1 (Write C) = 3 | **Resize 2->4** |
| 4 | Add 'D' | 4 | 4 | 1 | Fill |
| 5 | Add 'E' | 5 | 8 | 4 (Copy A..D) + 1 (Write E) = 5 | **Resize 4->8** |

**Total Cost for 5 inserts:** 1 + 2 + 3 + 1 + 5 = 12 writes.
**Average Cost:** 12 / 5 = 2.4 writes per insert.
**Insight:** Even with copying, the average is constant! It never explodes.

### 📈 Example 2: The "Plus 1" Mistake (Bad Strategy)

**Scenario:** Start Cap=1. Grow by `+1` each time.

| Step | Operation | Size | Capacity | Cost |
|------|-----------|------|----------|------|
| 1 | Add 'A' | 1 | 1 | 1 |
| 2 | Add 'B' | 2 | 2 | 1 (Copy) + 1 = 2 |
| 3 | Add 'C' | 3 | 3 | 2 (Copy) + 1 = 3 |
| 4 | Add 'D' | 4 | 4 | 3 (Copy) + 1 = 4 |
| ... | ... | ... | ... | ... |
| N | Add | N | N | N |

**Total Cost:** 1 + 2 + 3 + ... + N = O(N^2).
**Average Cost:** O(N).
**Insight:** Growing by a constant amount is a **performance disaster**.

### 🔥 Example 3: Shrinking Hysteresis

**Scenario:** Shrink at 50%.
1. Full at 100. Resize to 200.
2. Delete 1. Size 99. Usage < 50%. Shrink to 100.
3. Add 1. Size 100. Full. Resize to 200.
4. Delete 1...
*Result:* Infinite resizing loop (Thrashing).
**Fix:** Shrink only when usage drops below **25%**.

---

## 📊 SECTION 5: CRITICAL ANALYSIS — Performance & Robustness

**Purpose:** Summarize performance beyond Big-O.

### 📈 Complexity Table

| 📌 Operation | ⏱ Time (Avg) | ⏱ Time (Worst) | 💾 Space | 📝 Notes |
|--------------|--------------|----------------|---------|----------|
| **Access** | O(1) | O(1) | O(1) | Same as static array. |
| **Append** | **O(1)** | **O(N)** | O(1) | Amortized O(1). Worst case is resize. |
| **Insert (Mid)** | O(N) | O(N) | O(1) | Shifting dominates. |
| **Remove (Mid)** | O(N) | O(N) | O(1) | Shifting dominates. |
| **Remove (End)** | O(1) | O(N) | O(1) | Worst case if shrinking triggers. |

### 🤔 Why Big-O Might Mislead Here

- **Latency Spikes:** For real-time systems (games, HFT), "Average O(1)" isn't good enough. That one O(N) resize frame causes a stutter.
  - *Fix:* Pre-allocate capacity (`new List(10000)`).
- **Memory Wasted:** A `List<long>` with 10M items might have capacity 16M. That's 6M * 8 bytes = 48MB wasted RAM.

### ⚠ Edge Cases & Failure Modes

- **Out of Memory (OOM):** Resizing a 2GB list requires a 4GB contiguous block (2GB old + 4GB new = 6GB peak usage during copy). This often fails even if you have 4GB free RAM (fragmentation).
- **Concurrent Modification:** Adding to a list while iterating over it (in another thread or same loop) causes `ConcurrentModificationException` or undefined behavior. Arrays are not thread-safe.

---

## 🏭 SECTION 6: REAL SYSTEMS — Integration in Production

**Purpose:** Make dynamic arrays feel real and relevant.

### 🏭 Real System 1: Python `list`
- 🎯 **Problem:** Python needs a flexible array.
- 🔧 **Implementation:** Dynamic array of pointers. Growth factor is roughly 1.125 (N + N/8 + constant).
- 📊 **Impact:** Less aggressive memory usage than 2x, but slightly more frequent resizes.

### 🏭 Real System 2: Java `ArrayList`
- 🎯 **Problem:** The standard resizable collection.
- 🔧 **Implementation:** Growth factor 1.5x (`oldCapacity + (oldCapacity >> 1)`).
- 📊 **Impact:** 1.5x is preferred over 2x because it allows memory reuse (mathematically, new blocks can fit in freed old blocks).

### 🏭 Real System 3: C++ `std::vector`
- 🎯 **Problem:** Zero-overhead dynamic array.
- 🔧 **Implementation:** Growth factor usually 2x. Guaranteed contiguous.
- 📊 **Impact:** The default container for C++ performance.

### 🏭 Real System 4: Text Editors (Piece Table / Gap Buffer)
- 🎯 **Problem:** Typing characters inserts in the middle.
- 🔧 **Implementation:** Simple dynamic arrays (shifting O(N)) are too slow for large files. Editors use "Gap Buffers" (dynamic array with a hole in the middle) to make local typing O(1).

### 🏭 Real System 5: Browser DOM (Child Nodes)
- 🎯 **Problem:** Managing list of child elements.
- 🔧 **Implementation:** Often C++ vectors (Chrome/Blink) or dynamic arrays.
- 📊 **Impact:** Adding `<div>` to a container is usually fast, but adding 10,000 one-by-one triggers many resizes. Browser advises using `DocumentFragment` to batch inserts.

---

## 🔗 SECTION 7: CONCEPT CROSSOVERS — Connections & Comparisons

### 📚 What It Builds On (Prerequisites)
- **Static Arrays:** The underlying storage.
- **Amortized Analysis:** The math trick that explains why it's efficient.

### 🚀 What Builds On It (Successors)
- **Hash Tables (Open Addressing):** Use dynamic arrays for the bucket table.
- **Heaps:** Use dynamic arrays to store the tree.
- **Stacks:** Often implemented using a Dynamic Array.

### 🔄 Comparison with Alternatives

| 📌 Structure | ⏱ Access | ⏱ Append | 💾 Locality | ✅ Best For |
|-------------|---------|----------|-------------|-------------|
| **Dynamic Array** | O(1) | O(1) Amort. | ✅ Excellent | General purpose lists. |
| **LinkedList** | O(N) | O(1) | ❌ Poor | Frequent insertions in *middle*. |
| **Static Array** | O(1) | N/A | ✅ Perfect | Known size, embedded systems. |

---

## 📐 SECTION 8: MATHEMATICAL & THEORETICAL PERSPECTIVE

**Purpose:** Provide formalism for Amortized Analysis.

### 📋 Formal Definition
**Amortized Analysis (Aggregate Method):**
If a sequence of `N` operations takes `T(N)` total time, the amortized time per operation is `T(N) / N`.

### 📐 Key Theorem: The Geometric Series Cost
When doubling capacity, the cost of copying elements is:
$Cost = 1 + 2 + 4 + 8 + ... + N$
This is a geometric series summing to $\approx 2N$.
Total writes for $N$ appends = $N$ (initial writes) + $2N$ (copies) = $3N$.
Average per append = $3N / N = 3$ writes = **O(1)**.

*Contrast:* If adding fixed capacity $K$:
$Cost = K + 2K + 3K + ... + (N/K)K \approx O(N^2)$.
Average = **O(N)**.

---

## 💡 SECTION 9: ALGORITHMIC DESIGN INTUITION

**Purpose:** Teach "when and how to use" dynamic arrays.

### 🎯 Decision Framework

| Scenario | 🛠 Strategy |
|----------|------------|
| **Unknown number of items** | ✅ Use Dynamic Array (`List<T>`). |
| **Low Latency Critical** | ❌ Avoid Dynamic Array OR ✅ `List(capacity)` pre-allocate. |
| **Lots of middle inserts** | ❌ Use Linked List or Block List. |
| **Memory constrained** | ❌ Use Static Array (avoid 2x overhead). |

### 🔍 Interview Pattern Recognition

- 🔴 **Red Flag:** "Implement a class that supports `add` and `get`..."
  - *Pattern:* They want you to build a Dynamic Array. Don't use the built-in List!
- 🔵 **Blue Flag:** "Return a list of results..."
  - *Pattern:* Just use `List<T>`. Don't overthink it.
- 🔵 **Blue Flag:** "Optimize this O(N^2) string builder..."
  - *Pattern:* String concatenation is repeated copying (like bad resizing). Use `StringBuilder` (which is a dynamic array of chars).

---

## ❓ SECTION 10: KNOWLEDGE CHECK — Socratic Reasoning

1. **Why is the growth factor usually 1.5 or 2? Why not 10?**
2. **If I know I need exactly 10,000 items, should I use `new List()` or `new List(10000)`?** What is the performance difference?
3. **Can a Dynamic Array ever be slower than a Linked List?** In what specific operation?
4. **Explain "Amortized O(1)" to a non-technical manager.** Use an analogy (maybe paying rent vs daily hotel?).
5. **If you remove elements from the end, does the capacity automatically shrink?** Why might a language choose NOT to shrink?

---

## 🎯 SECTION 11: RETENTION HOOK — Memory Anchors

### 💎 One-Liner Essence
> **"Dynamic Arrays are just static arrays that learned to trade memory for convenience."**

### 🧠 Mnemonic Device
**"GROW"**
- **G**eometric growth (x2)
- **R**esize is rare
- **O**ver-allocation (Capacity > Size)
- **W**rites are amortized O(1)

### 🖼 Visual Cue
**The Balloon:**
- Starts small.
- You blow air (add items).
- It stretches.
- If it pops (full), you need a bigger balloon (resize).

### 💼 Real Interview Story
**Context:** Candidate asked to "Reverse words in a string."
**Approach:** Used simple string concatenation `res += word + " "`.
**Interviewer:** "What's the complexity?"
**Candidate:** "O(N)?"
**Interviewer:** "No. Strings are immutable. `+=` creates a new string. Like a bad dynamic array."
**Correction:** Candidate switched to `StringBuilder` (Dynamic Array). Complexity became true O(N).
**Outcome:** Passed. Demonstrated understanding of underlying memory costs.

---

## 🧩 5 COGNITIVE LENSES

### 🖥 Computational Lens
- **Prefetching:** Dynamic arrays maintain contiguity, keeping the CPU prefetcher happy. Linked lists break this. This is why `List<T>` is often faster than `LinkedList<T>` even for inserts, simply due to cache hits.

### 🧠 Psychological Lens
- **The "Free Lunch" Fallacy:** Developers use `.Add()` thinking it's always fast.
- **Correction:** Remember the "Tax Day" (Resize). It happens rarely, but it's expensive.

### 🔄 Design Trade-off Lens
- **Capacity vs Size:** We trade **Space** (unused capacity) for **Time** (fewer resizes). This is the classic Time-Space trade-off.

### 🤖 AI/ML Analogy Lens
- **Batch Size:** In training, we process data in batches (arrays) rather than one by one. Why? Because GPU operations on arrays are vectorised and efficient.

### 📚 Historical Context Lens
- **LISP (1958):** Used Linked Lists. Arrays were second-class.
- **Modern Era:** CPUs got faster, but memory latency didn't keep up. Arrays (Dynamic or Static) won because Cache Locality became the #1 performance factor.

---

## ⚔ SUPPLEMENTARY OUTCOMES

### ⚔ Practice Problems (8)

1. **⚔ Build Array from Permutation** (Source: LeetCode 1920 - 🟢)
   - 🎯 Concepts: Basic indexing, O(1) space trick.
   - 📌 Constraints: O(1) space (hard version).
2. **⚔ Concatenation of Array** (Source: LeetCode 1929 - 🟢)
   - 🎯 Concepts: Resizing logic, modulo arithmetic.
   - 📌 Constraints: Return double length array.
3. **⚔ Design Dynamic Array** (Source: NeetCode / Custom - 🟡)
   - 🎯 Concepts: Implement class with `resize()`, `push`, `pop`.
   - 📌 Constraints: Handle resizing manually.
4. **⚔ Pascal's Triangle** (Source: LeetCode 118 - 🟢)
   - 🎯 Concepts: List of Lists (Dynamic 2D).
   - 📌 Constraints: Each row grows.
5. **⚔ Flatten Nested List Iterator** (Source: LeetCode 341 - 🟡)
   - 🎯 Concepts: Recursion + List building.
   - 📌 Constraints: Handle arbitrary nesting.
6. **⚔ Insert Interval** (Source: LeetCode 57 - 🟡)
   - 🎯 Concepts: Building a new result list.
   - 📌 Constraints: O(N) time.
7. **⚔ Spiral Matrix** (Source: LeetCode 54 - 🟡)
   - 🎯 Concepts: Traversal adding to list.
   - 📌 Constraints: Boundary checks.
8. **⚔ Summary Ranges** (Source: LeetCode 228 - 🟢)
   - 🎯 Concepts: Sequential processing, list building.
   - 📌 Constraints: String formatting.

### 🎙 Interview Questions (6)

1. **Q: How does `ArrayList` grow in Java?**
   - 🔀 *Follow-up:* Why 1.5x and not 2x? (Memory fragmentation theory).
2. **Q: What is the amortized complexity of `push_back`? Proof?**
   - 🔀 *Follow-up:* When is it O(N)?
3. **Q: Design a `List` that shrinks when half empty.**
   - 🔀 *Follow-up:* Why is shrinking at 50% bad? (Hysteresis/Thrashing).
4. **Q: Difference between `Array` and `ArrayList` in C#?**
   - 🔀 *Follow-up:* Which one is thread-safe? (Neither).
5. **Q: Why does `StringBuilder` exist? Why not just use `String`?**
   - 🔀 *Follow-up:* Relate `StringBuilder` to Dynamic Arrays.
6. **Q: Implement a `remove_even_numbers` function.**
   - 🔀 *Follow-up:* Do it in-place O(1) space (Two pointer pattern).

### ⚠ Common Misconceptions (3)

1. **❌ Misconception:** "List is a Linked List."
   - ✅ **Reality:** In Python, C#, Java (ArrayList), it's a Dynamic Array.
   - 🧠 **Memory Aid:** "List = Array++. LinkedList = Nodes."
2. **❌ Misconception:** "Amortized O(1) means every op is O(1)."
   - ✅ **Reality:** It means "Total time / Operations = Constant". Individual ops can be slow.
   - 🧠 **Memory Aid:** "Average speed, not constant speed."
3. **❌ Misconception:** "Capacity == Size."
   - ✅ **Reality:** Capacity is hidden reserve. Size is what you use.
   - 🧠 **Memory Aid:** "Parking lot (Capacity) vs Parked cars (Size)."

### 📈 Advanced Concepts (3)

1. **Gap Buffers:**
   - 🎓 Prerequisite: Dynamic Arrays.
   - 🔗 Relation: An array with the "empty space" moved to the cursor position.
   - 💼 Use case: Text editors (efficient typing).
2. **Hashed Array Tree (HAT):**
   - 🎓 Prerequisite: Hashing.
   - 🔗 Relation: A "dynamic array" that uses a directory of fixed-size blocks to avoid copying.
   - 💼 Use case: Reducing resize latency.
3. **Tiered Vectors:**
   - 🎓 Prerequisite: Tree structures.
   - 🔗 Relation: O(sqrt(N)) insert anywhere.
   - 💼 Use case: Specialized databases.

### 🔗 External Resources (3)

1. **"Amortized Analysis - MIT OpenCourseWare"**
   - 🎥 Video
   - 🎯 Why useful: Mathematical proof of O(1) growth.
   - 🔗 Link: YouTube
2. **"Source Code of java.util.ArrayList"**
   - 📝 Code
   - 🎯 Why useful: See the actual `grow()` function implementation.
   - 🔗 Link: OpenJDK
3. **"Python List Implementation (CPython)"**
   - 📝 Article
   - 🎯 Why useful: Explains the growth pattern (0, 4, 8, 16, 25...).
   - 🔗 Link: Python Dev Guide

---
*End of Week 2 Day 2 Instructional File*

# 🎙️ Week 3 — Interview Q&A Reference (C# Focus)

**🗓️ Week:** 3  
**📌 Topic:** Sorting, Heaps, Hashing  
**🎯 Usage:** Review these before interviews. Focus on "Why" and "Trade-offs".

---

## 🧠 CONCEPTUAL & THEORETICAL QUESTIONS

### 1. **Quick Sort vs Merge Sort**
**Q: "Why is Quick Sort preferred for arrays, but Merge Sort for Linked Lists?"**

**A:**
- **Arrays:** Quick Sort has better cache locality (in-place swaps). Merge Sort needs O(n) extra RAM and copying data hurts performance.
- **Linked Lists:** Merge Sort works efficiently because we can manipulate pointers (O(1) merge) without extra space. Quick Sort relies on random access for partitioning, which lists don't have.

---

### 2. **Hash Table Worst Case**
**Q: "How can a Hash Table degrade to O(n)? How do we fix it?"**

**A:**
- **Cause:** Poor hash function puts all keys in one bucket (Collisions). Or Denial of Service attack (Hash Flooding).
- **Fix:**
  - Use a randomized hash seed (prevent deterministic attacks).
  - Use Balanced Trees (Red-Black Tree) instead of Linked Lists for buckets (Java 8 approach). Worst case becomes O(log n).

---

### 3. **Stable Sorting**
**Q: "What is a Stable Sort? Why does it matter?"**

**A:**
- **Definition:** Equal elements retain their relative order.
- **Example:** Sorting `[("Bob", 20), ("Alice", 20)]` by Age.
  - Stable: `("Bob", 20)` stays before `("Alice", 20)`.
  - Unstable: Might swap them.
- **Importance:** Essential when sorting by multiple columns (e.g., in Excel or SQL).

---

## 💻 C# SPECIFIC QUESTIONS

### 4. **Dictionary Internals**
**Q: "How does `Dictionary<K, V>` resolve collisions in C#?"**

**A:**
- It uses **Chaining**.
- **Optimization:** It stores entries in a single contiguous array `Entry[]` and buckets are just indices pointing into this array. This improves cache locality compared to standard "Node" based chaining.

### 5. **`List.Sort()` Algorithm**
**Q: "What algorithm does `List<T>.Sort()` use?"**

**A:**
- **Introsort (Introspective Sort).**
- It starts with **Quick Sort**.
- If recursion goes too deep (> 2 * log N), it switches to **Heap Sort** to prevent O(n²) worst case.
- For small partitions (< 16 items), it uses **Insertion Sort**.

### 6. **`GetHashCode` and `Equals`**
**Q: "What is the contract between `GetHashCode` and `Equals`?"**

**A:**
- If `A.Equals(B)` is true, then `A.GetHashCode()` **MUST** equal `B.GetHashCode()`.
- **Violation:** If they differ, the Dictionary will look in the wrong bucket and fail to find the key, even though it "equals" the search key.

---

## 🧪 CODING & ALGORITHMIC SCENARIOS

### 7. **Top K Elements**
**Q: "Find the top 10 largest numbers in a 1 billion number stream."**

**A:**
- **Naive:** Sort all. O(N log N). Too slow.
- **Heap:** Use a **Min-Heap** of size 10.
  - If new number > Heap.Peek(), pop min and push new.
  - Result: Heap contains the 10 largest.
  - **Complexity:** O(N log K). Since K=10, effectively O(N).

### 8. **Merge K Sorted Lists**
**Q: "Merge K sorted arrays of size N."**

**A:**
- **Naive:** Concatenate and Sort. O(NK log NK).
- **Heap:** Min-Heap of size K (one from each array).
  - Extract min, insert next element from that array.
  - **Complexity:** O(NK log K). Much faster.

---

## 🏛️ BEHAVIORAL / SYSTEM DESIGN

### 9. **Distributed Sorting**
**Q: "How would you sort 1 PB of data?"**

**A:**
- Cannot fit in RAM.
- **External Merge Sort:**
  1. Break into 100MB chunks.
  2. Sort each chunk in RAM (Quick Sort) and write to disk.
  3. Merge sorted chunks (K-Way Merge) using a Min-Heap.
  4. Write final stream.

---

**Generated:** December 30, 2025
**Context:** C# Focused Interview Prep

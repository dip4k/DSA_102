# 📚 Week 3 — Study Guidelines: Sorting & Hashing

**🗓️ Week:** 3  
**📌 Focus:** Ordering Data (Sorting) & Fast Lookup (Hashing)  
**⏱️ Time Commitment:** 5-7 hours (45-60 mins per day + practice)  
**🎯 Learning Philosophy:** "Speed through understanding structure, not memorizing code."

---

## 🎯 WEEK 3 OBJECTIVES

By the end of this week, you will:
1. ✅ Understand **why O(n log n) is the comparison sort limit** and how we achieve it.
2. ✅ Master **Hash Tables** (the most interviewed data structure).
3. ✅ Recognize when to use **Elementary Sorts** (small n, nearly sorted).
4. ✅ Apply **Heaps** to "Top K" problems (priority-based access).
5. ✅ Analyze real-world systems: How Python's `dict`, C# `Dictionary<K,V>`, and Timsort work.

---

## 📅 DAILY STRUCTURE

### **Day 1: Elementary Sorts (Foundations)**
- **Goal:** Understand O(n²) is not always "slow" for small arrays.
- **Key Insight:** Insertion Sort beats Merge Sort for n < 50 due to lower constants and cache locality.
- **Real System:** Timsort (Python/Java) uses Insertion Sort for runs < 64 elements.
- **Time:** 45 minutes reading + 30 minutes implementing Insertion Sort (no-code trace first, then C# if needed).

### **Day 2: Merge Sort & Quick Sort (Divide & Conquer)**
- **Goal:** Master the two pillars of O(n log n) sorting.
- **Key Insight:** Merge Sort guarantees O(n log n) but uses O(n) space. Quick Sort is faster (cache) but O(n²) worst case.
- **Real System:** C# `List.Sort()` uses **Introsort** (Quick Sort + Heap Sort fallback).
- **Time:** 60 minutes + 30 minutes tracing partitioning logic.

### **Day 3: Heap Sort & Priority Queues**
- **Goal:** Learn the **Heap** structure (Array-based Binary Tree).
- **Key Insight:** Building a Heap is O(n), not O(n log n). Heaps power priority queues.
- **Real System:** OS Task Schedulers, Dijkstra's algorithm.
- **Time:** 60 minutes + 30 minutes solving "Top K" problems.

### **Day 4: Hash Tables I (Chaining)**
- **Goal:** Understand how **O(1) lookup** works via hashing.
- **Key Insight:** Collisions are inevitable (Birthday Paradox). Chaining (Linked Lists) handles them robustly.
- **Real System:** C# `Dictionary<K,V>` uses chaining with cache-optimized entry arrays.
- **Time:** 60 minutes + 30 minutes solving "Two Sum" variants.

### **Day 5: Hash Tables II (Open Addressing)**
- **Goal:** Learn cache-friendly hashing (no pointers).
- **Key Insight:** Open Addressing is faster (cache) but requires Tombstones for deletion.
- **Real System:** Python `dict` uses Open Addressing with Robin Hood hashing.
- **Time:** 60 minutes + 30 minutes understanding Tombstone logic.

---

## 🛠️ HOW TO USE THESE FILES

### **Step 1: Read the "WHY" Section First**
- Don't jump to code. Understand the *business problem* being solved.
- Example: "Why do we need heaps?" → To efficiently find the Top K items without sorting everything.

### **Step 2: Visualize with ASCII Diagrams**
- Trace through the step-by-step examples (e.g., Partitioning in Quick Sort).
- Draw on paper or whiteboard. Physical tracing builds intuition.

### **Step 3: Implement (C# if needed)**
- After understanding the logic, implement the algorithm.
- Focus on **edge cases** (empty array, single element, duplicates).

### **Step 4: Solve LeetCode Problems**
- Use the Problem Roadmap to pick 2-3 problems per day.
- Start with Easy, then Medium.

### **Step 5: Review "Real Systems" Section**
- Understand *why* production code (Timsort, Introsort) is hybrid.
- This knowledge separates "I know algorithms" from "I understand systems."

---

## ⚠️ COMMON PITFALLS TO AVOID

1. **Memorizing Code Without Understanding:**
   - ❌ "I memorized Quick Sort."
   - ✅ "I understand why we need randomized pivot to avoid O(n²)."

2. **Ignoring Edge Cases:**
   - Empty arrays, single elements, all duplicates.
   - These break naive implementations.

3. **Assuming O(1) is Always Fast:**
   - Hash Tables can degrade to O(n) with bad hash functions (Hash Flooding attack).

4. **Thinking Elementary Sorts are Useless:**
   - For n < 50, Insertion Sort is *faster* than Merge Sort.
   - This is why Timsort and Introsort use it.

---

## 📊 PROGRESS TRACKING

Use `Week_3_Daily_Progress_Checklist.md` to track:
- [ ] Instructional file read.
- [ ] Concept checks passed (Can you explain out loud?).
- [ ] LeetCode problems solved.
- [ ] Reflection: "What did I learn today?"

---

## 🎯 SUCCESS CRITERIA

By the end of Week 3, you should be able to:
- ✅ Explain Quick Sort vs Merge Sort trade-offs to a peer.
- ✅ Implement a Hash Map from scratch (Chaining).
- ✅ Solve "Top K Frequent Elements" using a Heap.
- ✅ Understand why Python's `dict` is faster than Java's `HashMap` (Open Addressing vs Chaining).

---

## 🔗 PREREQUISITES

Before starting Week 3, ensure you've completed:
- **Week 1:** Big-O Notation, Recursion.
- **Week 2:** Arrays, Linked Lists.

If these are shaky, review them first. Sorting builds heavily on array manipulation.

---

## 📚 SUPPLEMENTARY RESOURCES

- **Book:** *The Algorithm Design Manual* (Skiena) - Chapter 4 (Sorting).
- **Video:** MIT 6.006 Lecture 3 (Insertion Sort, Merge Sort).
- **Paper:** "Timsort" by Tim Peters (Python Enhancement Proposal).

---

**Generated:** December 30, 2025  
**Version:** 9.0 (C# / No-Code First / V9 Config)  
**Status:** ✅ COMPLETE
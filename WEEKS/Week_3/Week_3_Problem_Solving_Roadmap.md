# 🗺️ Week 3 — Problem Solving Roadmap (C# Edition)

**🗓️ Week:** 3  
**📌 Focus:** Sorting, Heaps, Hash Tables  
**🎯 Goal:** Master the "Top K" pattern and Hash Map tricks.

---

## traffic_light: PROBLEM CATEGORIES & PATTERNS

### **1. Hash Maps (Advanced)**
*Core Pattern: Frequency Counting, Caching, Two-Pass.*

| Difficulty | Problem Name | Key Concept | C# Tip |
|---|---|---|---|
| 🟢 Easy | **Two Sum** | Complement Lookup | `if (map.ContainsKey(target - num))` |
| 🟡 Medium | **Group Anagrams** | Sorted String as Key | `string key = new string(s.OrderBy(c => c).ToArray())` |
| 🟡 Medium | **Longest Consecutive Seq** | HashSet Lookup | Check `!set.Contains(num-1)` to start counting. |
| 🟡 Medium | **Subarray Sum Equals K** | Prefix Sum + Map | Store `(PrefixSum, Frequency)`. |

---

### **2. Heaps / Priority Queue**
*Core Pattern: Top K, Median, Merging.*

| Difficulty | Problem Name | Key Concept | C# Tip |
|---|---|---|---|
| 🟡 Medium | **Kth Largest Element** | Min-Heap (Size K) | `PriorityQueue<int, int>` (.NET 6). |
| 🟡 Medium | **Top K Frequent Elements** | Bucket Sort or Heap | Heap is O(N log K). Buckets O(N). |
| 🔴 Hard | **Merge k Sorted Lists** | Min-Heap | Add heads to heap. Pop min, add next. |
| 🔴 Hard | **Median of Data Stream** | Two Heaps | Balance sizes between MinHeap and MaxHeap. |

---

### **3. Sorting Intervals**
*Core Pattern: Sort by Start Time.*

| Difficulty | Problem Name | Key Concept | C# Tip |
|---|---|---|---|
| 🟡 Medium | **Merge Intervals** | Sort + Iterate | `intervals.Sort((a,b) => a[0].CompareTo(b[0]))`. |
| 🟡 Medium | **Non-overlapping Intervals** | Sort by End Time | Greedy approach. |
| 🟢 Easy | **Meeting Rooms** | Sort Start Time | Check overlap `prev.End > curr.Start`. |

---

## 🛠️ C# PROBLEM SOLVING TIPS

### **1. PriorityQueue usage (.NET 6+)**
Before .NET 6, we didn't have a built-in Heap. Now we do!
```csharp
// Min Heap (Default)
var pq = new PriorityQueue<string, int>();
pq.Enqueue("Task1", 1); // Element, Priority
pq.Enqueue("Task2", 10);
var item = pq.Dequeue(); // Returns "Task1"

// Max Heap (Trick)
// Use negative priority, or Custom Comparer
var maxPQ = new PriorityQueue<int, int>(Comparer<int>.Create((x, y) => y - x));
```

### **2. Dictionary Tricks**
- **TryAdd:** `dict.TryAdd(key, val)` returns false if exists. Faster than `ContainsKey` + `Add`.
- **TryGetValue:** `dict.TryGetValue(key, out var val)` prevents double lookup exception.
- **Default Value:** `dict.GetValueOrDefault(key, 0)` simplifies counters.

### **3. Sorting Custom Objects**
```csharp
// Inline Lambda Sort
people.Sort((p1, p2) => p1.Age.CompareTo(p2.Age));

// LINQ (Clean but slower, returns new list)
var sorted = people.OrderBy(p => p.Age).ToList();
```

---

## 📅 SUGGESTED SCHEDULE

- **Mon:** Elementary Sorts + Hash Maps (Two Sum, Group Anagrams).
- **Tue:** Merge/Quick Sort (Sort an Array, Kth Largest - QuickSelect).
- **Wed:** Heaps (Top K Frequent, Last Stone Weight).
- **Thu:** Hash Maps II (Subarray Sum Equals K).
- **Fri:** Intervals (Merge Intervals).
- **Sat/Sun:** Hard Problems (Merge k Lists, Median Stream).

---

**Generated:** December 30, 2025
**Context:** C# LeetCode Patterns

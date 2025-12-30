# 🗺️ Week 2 — Problem Solving Roadmap (C# Edition)

**🗓️ Week:** 2  
**📌 Focus:** Mastering Arrays, Lists, Stacks, Queues, Binary Search  
**🎯 Goal:** Solve 15+ problems to build pattern recognition.

---

## 🚦 PROBLEM CATEGORIES & PATTERNS

### **1. Arrays & Hashing**
*Core Pattern: Using Arrays/HashMaps for counting or lookup.*

| Difficulty | Problem Name | Key Concept | C# Tip |
|---|---|---|---|
| 🟢 Easy | **Contains Duplicate** | `HashSet<T>` | `new HashSet<int>(nums).Count < nums.Length` |
| 🟢 Easy | **Valid Anagram** | Frequency Array | Use `int[26]` for lowercase English letters. |
| 🟢 Easy | **Two Sum** | Dictionary Lookup | Use `Dictionary<int, int>` to store `{val: index}`. |
| 🟡 Medium | **Group Anagrams** | Sorting as Key | `String.Concat(str.OrderBy(c => c))` as Dict Key. |
| 🟡 Medium | **Top K Frequent Elements** | Bucket Sort / Heap | `PriorityQueue<T, int>` (C# 10+) or Bucket Sort. |

---

### **2. Two Pointers**
*Core Pattern: Manipulating indices from both ends or distinct positions.*

| Difficulty | Problem Name | Key Concept | C# Tip |
|---|---|---|---|
| 🟢 Easy | **Valid Palindrome** | Left/Right Pointers | `Char.IsLetterOrDigit(c)` helps clean strings. |
| 🟡 Medium | **Two Sum II (Sorted)** | Shrinking Window | If `sum > target`, `right--`; else `left++`. |
| 🟡 Medium | **3Sum** | Fixed Pointer + Two Sum | Skip duplicates! `while (l < r && nums[l] == nums[l+1]) l++;` |
| 🟡 Medium | **Container With Most Water** | Greedy Two Pointer | Move the shorter line inward to try for a taller line. |

---

### **3. Stack**
*Core Pattern: Matching, Monotonic Logic, Backtracking history.*

| Difficulty | Problem Name | Key Concept | C# Tip |
|---|---|---|---|
| 🟢 Easy | **Valid Parentheses** | Matching | Use `Stack<char>`. Check `Count == 0` at end. |
| 🟡 Medium | **Min Stack** | Aux Stack / Pair | Store `(val, min)` tuples or use 2 stacks. |
| 🟡 Medium | **Evaluate RPN** | Postfix Eval | `int.Parse()` strings. Process operators. |
| 🟡 Medium | **Daily Temperatures** | Monotonic Stack | Store indices. Pop when current > stack top. |

---

### **4. Binary Search**
*Core Pattern: Reducing search space by half.*

| Difficulty | Problem Name | Key Concept | C# Tip |
|---|---|---|---|
| 🟢 Easy | **Binary Search** | Standard Template | Loop `while (lo <= hi)`. |
| 🟡 Medium | **Search 2D Matrix** | Treat 2D as 1D | `row = mid / cols`, `col = mid % cols`. |
| 🟡 Medium | **Koko Eating Bananas** | Search on Answer | Search range `[1, max(piles)]`. Predicate function. |
| 🟡 Medium | **Min in Rotated Sorted Array** | Modified BS | Compare `mid` with `right` to determine sorted half. |

---

### **5. Linked List**
*Core Pattern: Pointer manipulation (NEXT, PREV).*

| Difficulty | Problem Name | Key Concept | C# Tip |
|---|---|---|---|
| 🟢 Easy | **Reverse Linked List** | Iterative/Recursive | Use tuple swap syntax: `(curr.next, prev, curr) = (prev, curr, curr.next)` (C# 7+). |
| 🟢 Easy | **Merge Two Sorted Lists** | Dummy Node | `ListNode dummy = new ListNode();` simplifies head logic. |
| 🟢 Easy | **Linked List Cycle** | Fast/Slow Pointers | `while (fast != null && fast.next != null)`. |
| 🟡 Medium | **Reorder List** | Split + Reverse + Merge | Find middle, reverse second half, interleave. |

---

## 🛠️ C# PROBLEM SOLVING TIPS

### **1. Choosing the Right Collection**
- Need unique items? → `HashSet<T>`
- Need Key-Value pairs? → `Dictionary<K, V>`
- Need Sort + Unique? → `SortedSet<T>` (Red-Black Tree)
- Need Sort + Duplicates? → `List<T>` + `Sort()` OR `PriorityQueue<T, P>`

### **2. Useful C# Snippets**
```csharp
// Sorting an Array/List
Array.Sort(arr);
list.Sort();

// Binary Search (Built-in)
int index = Array.BinarySearch(arr, value);
// If index < 0, insertion point is ~index

// LINQ One-Liners (Good for easy problems, avoid in performance-critical loops)
int max = nums.Max();
int sum = nums.Sum();
var distinct = nums.Distinct().ToArray();
```

### **3. Handling Edge Cases**
- **Null/Empty:** Always check `if (arr == null || arr.Length == 0)`.
- **Single Element:** Binary search logic needs to handle `low == high`.
- **Constraints:** Check input size.
  - $N \le 10^5$ → Aim for $O(N)$ or $O(N \log N)$.
  - $N \le 20$ → $O(2^N)$ might be okay.

---

## 📅 SUGGESTED SCHEDULE

- **Mon:** Arrays & Hashing (Contains Duplicate, Valid Anagram, Two Sum).
- **Tue:** Two Pointers (Valid Palindrome, Two Sum II).
- **Wed:** Stack (Valid Parentheses, Min Stack).
- **Thu:** Binary Search (BS, Search 2D Matrix).
- **Fri:** Linked List (Reverse, Merge Sorted).
- **Sat/Sun:** Mediums & Review (Group Anagrams, Top K, Daily Temps).

---

**Generated:** December 30, 2025
**Context:** C# LeetCode Patterns

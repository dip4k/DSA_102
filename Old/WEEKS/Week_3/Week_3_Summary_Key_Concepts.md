# 📝 Week 3 — Summary & Key Concepts: Sorting & Hashing

**🗓️ Week:** 3  
**📌 Theme:** Ordering and Mapping Data  
**🎯 Goal:** Master O(n log n) sorting and O(1) hashing mechanics.

---

## 🧠 CORE CONCEPTS CHEAT SHEET

### 1. Sorting Algorithms
| Algo | Time (Avg) | Time (Worst) | Space | Stable? | Key Mechanism |
|---|---|---|---|---|---|
| **Bubble** | O(n²) | O(n²) | O(1) | ✅ | Swaps neighbors. |
| **Insertion** | O(n²) | O(n²) | O(1) | ✅ | Inserts into sorted subarray. |
| **Selection** | O(n²) | O(n²) | O(1) | ❌ | Picks min element. |
| **Merge** | O(n log n) | O(n log n) | O(n) | ✅ | Split, Recurse, Merge. |
| **Quick** | O(n log n) | O(n²) | O(log n) | ❌ | Pivot, Partition, Recurse. |
| **Heap** | O(n log n) | O(n log n) | O(1) | ❌ | Build Heap, Extract Max. |

### 2. Hash Tables
- **Chaining:**
  - Buckets hold Linked Lists.
  - Safe against high load factors (> 0.7).
  - Used in Java `HashMap` (pre-8).
- **Open Addressing:**
  - Everything in the array (Linear Probing).
  - Cache-friendly, no pointer overhead.
  - Fails if load factor $\ge$ 1.0.
  - Used in Python `dict`, Rust `HashMap`.

### 3. Heaps (Priority Queues)
- **Structure:** Complete Binary Tree stored in Array.
- **Indices:** Parent $i$, Children $2i+1, 2i+2$.
- **Heap Property:** Parent $\le$ Child (MinHeap).
- **Build:** O(n).
- **Pop/Push:** O(log n).

---

## 🛠️ C# IMPLEMENTATION DETAILS

### **Collections**
- **`List<T>.Sort()`**: Uses **Introsort** (Hybrid Quick + Heap + Insertion).
- **`Dictionary<K, V>`**: Uses **Chaining** with an array of structs (cache-friendly chaining).
- **`PriorityQueue<T, P>`** (.NET 6+): Uses **4-ary Min-Heap**.
- **`SortedDictionary<K, V>`**: Uses **Red-Black Tree** (O(log n)).

### **Interfaces**
- `IComparable<T>`: Implements `CompareTo` (Natural ordering).
- `IComparer<T>`: Implements `Compare` (Custom ordering, e.g., passing a lambda to Sort).
- `GetHashCode()`: Critical for Dictionary keys. Must be consistent with `Equals()`.

---

## 🧠 CRITICAL MENTAL MODELS

### 1. Stability
- **Stable Sort:** Keeps original relative order of equal elements.
- **Why?** Sorting by "Last Name" then "First Name". If "First Name" sort isn't stable, the "Last Name" order is destroyed.

### 2. Comparison vs Non-Comparison
- **Comparison Sort limit:** O(n log n). You cannot beat this by comparing keys.
- **Non-Comparison (Radix/Count):** O(n) possible for integers/strings.

### 3. Hashing Trade-off
- **Space-Time Trade-off:** Use more RAM (larger table) to get fewer collisions (faster access).
- **Load Factor:** The knob that controls this trade-off.

---

## ⚠️ COMMON PITFALLS

1. **Mutable Keys in Dictionary:**
   - If you modify an object *after* putting it in a Dictionary, its HashCode changes. You can never find it again.
   - **Fix:** Keys should be immutable (Strings, Records, Structs).

2. **Quicksort Worst Case:**
   - Sorting an already sorted array with "first element pivot" takes O(n²).
   - **Fix:** Use Random Pivot or Median-of-3.

3. **Heap Sort Cache:**
   - Heap Sort jumps around memory ($i \rightarrow 2i$). Poor cache locality compared to Quick Sort.

---

## 🔗 CONNECTIONS TO FUTURE WEEKS

- **Trees (Week 5):** BSTs are the "sorted" alternative to Hash Maps.
- **Graphs (Week 6):** Dijkstra's algorithm uses Priority Queues.
- **System Design:** Distributed Caching (Consistent Hashing) relies on these principles.

---

**Generated:** December 30, 2025
**Context:** C# / No-Code Focus

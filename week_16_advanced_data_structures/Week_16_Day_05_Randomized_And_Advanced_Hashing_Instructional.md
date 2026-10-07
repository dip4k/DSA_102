# 📘 Week 16, Day 5: Advanced Hashing: Robin Hood & Cuckoo Hashing

> 🧭 **Navigation:** [← Previous Day](Week_16_Day_04_Cache_Oblivious_Algorithms_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Week Playbook →](WEEK_16_FULL_PLAYBOOK.md)
> 
> 💡 **Instructor Note:** *In senior systems and distributed database interviews (Snowflake, Databricks, Meta, Google), standard separate chaining (Java `HashMap`) is considered naive due to pointer-chasing cache misses. Interviewers evaluate your understanding of open-addressing variants: Robin Hood Hashing (low PSL variance), Cuckoo Hashing (deterministic `O(1)` worst-case lookups), and SIMD-accelerated Swiss Tables.*

---

## 🎯 Learning Objectives

*   **Clustering Breakdown:** Diagnose primary clustering in linear probing and understand how variance in Probe Sequence Length (PSL) causes tail latency spikes.
*   **The Robin Hood Invariant:** Master the "take from the rich to give to the poor" displacement rule, equalizing search distances across all keys.
*   **Early Termination Guarantees:** Understand why Robin Hood search can terminate on a non-empty slot as soon as `resident.DIB < current_dib`.
*   **Cuckoo Hashing Mechanics:** Evaluate two-table cuckoo hashing with deterministic `O(1)` worst-case search vs insertion cycle rehash risks.
*   **Production Hashing Landscape:** Compare Robin Hood Hashing against Google Abseil / Rust `hashbrown` Swiss Tables (SIMD group probing).
*   **Dual-Language Fluency:** Implement high-performance Robin Hood hash tables with backward-shift deletion and early search termination in modern C# (.NET 8/9) and idiomatic Python (3.11+).

---

## ⚖️ FAANG Senior / Lead Interview Calibration

In Senior (L5) and Lead/Staff (L6) technical rounds, candidates must articulate trade-offs across modern hash table architectures:

| Architecture | Average Lookup | Worst-Case Lookup | Memory & Cache Locality | Deletion Strategy | Production Adoptions |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Separate Chaining** | `O(1)` | `O(N)` (or `O(log N)` via RB-Tree) | Poor (Pointer hopping per node) | Unlink linked-list node | Java `HashMap`, C++ `std::unordered_map`. |
| **Linear Probing** | `O(1)` | `O(N)` (Primary clustering) | Excellent (Sequential prefetch) | Tombstones or backward shift | Python dictionaries (pre-3.6). |
| **Robin Hood Hashing** | `O(1)` | `O(log log N)` w.h.p. | Excellent (Cache-line sequential) | Backward shift (zero tombstones) | Rust `std::collections` (historical), embedded DBs. |
| **Cuckoo Hashing** | `O(1)` | `O(1)` (Guaranteed 2 probes!) | Moderate (2 random cache lines) | Direct deletion in `O(1)` | High-frequency trading, hardware routers. |
| **Swiss Tables (SIMD)** | `O(1)` | `O(N)` | Optimal (16-byte SSE2 group probe) | Tombstone marks | Google Abseil `flat_hash_map`, Rust `hashbrown`. |

### Senior Expectations
1. **The Tail Latency Argument:** An L4 candidate thinks average-case `O(1)` is sufficient. An L5/L6 engineer emphasizes P99 / P99.9 latency: *"In linear probing at 80% load factor, average search is ~2.5 probes, but the maximum probe length can spike to 50+ slots due to primary clustering. Robin Hood hashing minimizes the variance of PSL, keeping P99 latency within 4-6 probes even at 90% load factor."*
2. **When Full Code is Required:** Candidates are expected to write open-addressing Robin Hood insertion and early-exit search in 25 minutes.

---

## 📖 Chapter 1: Context & Physical Mental Models

### 1. Distance From Initial Bucket (DIB) / Probe Sequence Length (PSL)

Every entry in a Robin Hood hash table tracks its **DIB (Distance from Initial Bucket)**:
`DIB = (current_slot - hash(key) + capacity) % capacity`.

```text
Hash Slot:     [ 0 ]       [ 1 ]       [ 2 ]       [ 3 ]       [ 4 ]
Resident:     Key: A      Key: B      Empty       Empty       Empty
DIB:            0           1           -           -           -
```

Now consider inserting `Key C`, whose initial hash slot is `0` (`DIB = 0`):
*   Check slot `0`: Occupied by `A` (`DIB = 0`). Since `C.DIB == A.DIB`, advance `C` to slot 1 (`C.DIB = 1`).
*   Check slot `1`: Occupied by `B` (`DIB = 1`). Since `C.DIB == B.DIB`, advance `C` to slot 2 (`C.DIB = 2`).
*   Check slot `2`: Empty! Place `Key C` here with `DIB = 2`.

### 2. The "Rich-to-Poor" Displacement Rule

What happens when an incoming key has traveled farther than the current resident?
*   **"Rich" Element:** Low DIB (near its home slot).
*   **"Poor" Element:** High DIB (far from its home slot).
*   **The Invariant:** Whenever `incoming.DIB > resident.DIB`, the incoming key displaces the resident! The resident is evicted and continues probing with `evicted.DIB + 1`.

```text
Slot 5: Currently occupied by Key X with DIB = 1 (Rich)
Incoming: Key Y with DIB = 3 (Poor)

Action: Y kicks out X!
        Slot 5 now stores Key Y (DIB = 3).
        Key X becomes the new incoming element with DIB = 2, probing slot 6.
```

---

## 🏛️ Chapter 2: The Governing Invariants & Algorithmic Mechanics

### 1. Mathematical Invariants

1. **Rich-to-Poor Swap Invariant:** For any collision at bucket `i`:
   `if (incoming.DIB > table[i].DIB)` -> Swap `incoming` and `table[i]`.
2. **Early Termination on Search Miss Invariant:**
   When searching for key `K` with current probe distance `d`:
   If we inspect bucket `i` and find that `table[i].IsOccupied` but `table[i].DIB < d`, **key `K` is mathematically guaranteed not to exist in the table**.
   *Proof:* If `K` existed, it would have arrived at bucket `i` with DIB equal to `d`. Since `d > table[i].DIB`, `K` would have displaced `table[i]` during insertion. Because it didn't, `K` was never inserted. We can terminate immediately without probing the rest of the cluster!
3. **PSL Variance Bound:**
   While standard linear probing has maximum probe length `O(log N)` with high probability, Robin Hood hashing reduces maximum probe length to `O(log log N)` with high probability at any constant load factor `alpha < 1`.

---

## 💻 Chapter 3: Idiomatic Dual-Language Code

### C# Primary Implementation (.NET 8/9)

```csharp
using System;
using System.Collections.Generic;

namespace AdvancedDataStructures;

/// <summary>
/// High-performance Robin Hood Hash Table using open addressing with DIB displacement.
/// Equalizes probe sequence lengths and enables early search termination on misses.
/// </summary>
public sealed class RobinHoodHashTable<TKey, TValue> where TKey : notnull
{
    private struct Entry
    {
        public TKey Key;
        public TValue Value;
        public int DIB;
        public bool IsOccupied;
    }

    private Entry[] _table;
    private int _capacity;
    private int _count;
    private const double MaxLoadFactor = 0.80;

    public RobinHoodHashTable(int initialCapacity = 16)
    {
        _capacity = Math.Max(16, initialCapacity);
        _table = new Entry[_capacity];
        _count = 0;
    }

    private int GetHomeIndex(TKey key) => (key.GetHashCode() & 0x7FFFFFFF) % _capacity;

    /// <summary>
    /// Inserts or updates key-value pair.
    /// Displaces richer entries (lower DIB) with poorer entries (higher DIB).
    /// </summary>
    public void Put(TKey key, TValue value)
    {
        if (_count >= _capacity * MaxLoadFactor)
        {
            Resize(_capacity * 2);
        }

        int idx = GetHomeIndex(key);
        var incoming = new Entry
        {
            Key = key,
            Value = value,
            DIB = 0,
            IsOccupied = true
        };

        while (true)
        {
            if (!_table[idx].IsOccupied)
            {
                _table[idx] = incoming;
                _count++;
                return;
            }

            // Existing key update
            if (EqualityComparer<TKey>.Default.Equals(_table[idx].Key, incoming.Key))
            {
                _table[idx].Value = incoming.Value;
                return;
            }

            // Robin Hood rule: swap if incoming has traveled farther from home
            if (incoming.DIB > _table[idx].DIB)
            {
                (incoming, _table[idx]) = (_table[idx], incoming);
            }

            incoming.DIB++;
            idx = (idx + 1) % _capacity;
        }
    }

    /// <summary>
    /// Retrieves value for key, terminating early if resident DIB is less than search DIB.
    /// </summary>
    public bool TryGet(TKey key, out TValue? value)
    {
        int idx = GetHomeIndex(key);
        int currentDIB = 0;

        while (_table[idx].IsOccupied)
        {
            if (EqualityComparer<TKey>.Default.Equals(_table[idx].Key, key))
            {
                value = _table[idx].Value;
                return true;
            }

            // Early termination invariant: key cannot exist past this point
            if (_table[idx].DIB < currentDIB)
            {
                break;
            }

            currentDIB++;
            idx = (idx + 1) % _capacity;
        }

        value = default;
        return false;
    }

    private void Resize(int newCapacity)
    {
        var oldTable = _table;
        _capacity = newCapacity;
        _table = new Entry[_capacity];
        _count = 0;

        foreach (var entry in oldTable)
        {
            if (entry.IsOccupied)
            {
                Put(entry.Key, entry.Value);
            }
        }
    }
}
```

### Python Secondary Implementation (Python 3.11+)

```python
from typing import Any, Optional, Tuple

class RobinHoodHashTable:
    """Robin Hood Hash Table minimizing probe sequence length variance."""

    class Entry:
        __slots__ = ('key', 'val', 'dib', 'occupied')

        def __init__(self, key: Any, val: Any, dib: int = 0):
            self.key = key
            self.val = val
            self.dib = dib
            self.occupied = True

    def __init__(self, initial_capacity: int = 16):
        self.capacity: int = max(16, initial_capacity)
        self.table: list[Optional[RobinHoodHashTable.Entry]] = [None] * self.capacity
        self.count: int = 0
        self.max_load_factor: float = 0.80

    def _hash(self, key: Any) -> int:
        return (hash(key) & 0x7FFFFFFF) % self.capacity

    def put(self, key: Any, val: Any) -> None:
        """Inserts or updates key. Swaps entries when incoming DIB exceeds resident DIB."""
        if self.count >= self.capacity * self.max_load_factor:
            self._resize(self.capacity * 2)

        idx = self._hash(key)
        incoming = self.Entry(key, val, 0)

        while True:
            curr = self.table[idx]
            if curr is None or not curr.occupied:
                self.table[idx] = incoming
                self.count += 1
                return

            if curr.key == incoming.key:
                curr.val = incoming.val
                return

            # Rich-to-poor invariant swap
            if incoming.dib > curr.dib:
                incoming, self.table[idx] = self.table[idx], incoming

            incoming.dib += 1
            idx = (idx + 1) % self.capacity

    def get(self, key: Any) -> Optional[Any]:
        """Looks up key. Terminates early if resident DIB is strictly less than search DIB."""
        idx = self._hash(key)
        current_dib = 0

        while True:
            curr = self.table[idx]
            if curr is None or not curr.occupied:
                return None

            if curr.key == key:
                return curr.val

            # Early termination invariant
            if curr.dib < current_dib:
                return None

            current_dib += 1
            idx = (idx + 1) % self.capacity

    def _resize(self, new_capacity: int) -> None:
        old_table = self.table
        self.capacity = new_capacity
        self.table = [None] * self.capacity
        self.count = 0

        for entry in old_table:
            if entry and entry.occupied:
                self.put(entry.key, entry.val)
```

---

## 📊 Chapter 4: Explicit Complexity Deconstruction

| Metric | Separate Chaining | Linear Probing | Robin Hood Hashing | Cuckoo Hashing |
| :--- | :--- | :--- | :--- | :--- |
| **Successful Search (Avg)**| `O(1)` (1.5 probes) | `O(1)` (2.5 probes at 80% load) | `O(1)` (2.5 probes at 80% load) | `O(1)` (Guaranteed <= 2 probes) |
| **Unsuccessful Search (Avg)**| `O(1)` (1.0 probe) | `O(1)` (13 probes at 80% load) | `O(1)` (~3 probes via early exit) | `O(1)` (Guaranteed 2 probes) |
| **Worst-Case Search** | `O(N)` | `O(N)` (Primary clustering) | `O(log log N)` w.h.p. | `O(1)` (Strictly 2 probes) |
| **P99 Lookup Latency** | High variance | Severe spikes | Ultra-flat variance | Deterministic |
| **Insertion Time (Avg)** | `O(1)` | `O(1)` | `O(1)` | `O(1)` amortized |
| **Cache Line Misses** | 1 per link jump | 1 (contiguous scan) | 1 (contiguous scan) | 2 (two distinct table lookups) |

### Invariant Justification: Why Unsuccessful Search is Faster in Robin Hood
In standard linear probing, an unsuccessful search cannot stop until it encounters an empty slot. If a cluster is 40 slots wide, the search scans all 40 slots.
In Robin Hood Hashing, keys are sorted along their probe path in increasing order of home bucket. If `current_search_DIB > resident.DIB`, the target key cannot possibly appear further along the probe chain. The search aborts immediately, converting worst-case cluster scans into average `~2-4` comparisons.

---

## 🎙️ Chapter 5: 45-Minute Senior / Lead Verbal Interview Script

```text
[00:00 - 05:00] Clarify & Problem Modeling
Candidate: "We are asked to design a high-throughput, low-latency in-memory key-value store. 
           Standard separate chaining allocates a node per key, leading to pointer chasing and CPU cache misses. 
           Open addressing with linear probing stores elements in a flat array for contiguous cache prefetching, 
           but suffers from primary clustering—causing high P99 tail latencies at high load factors. 
           I propose Robin Hood Hashing, which equalizes probe lengths by taking from the rich to give to the poor, 
           enabling early search termination on cache misses."

[05:00 - 15:00] Invariants & Trade-offs
Candidate: "We maintain one core invariant: every entry stores its Distance from Initial Bucket (DIB). 
           When inserting, if an incoming element's DIB exceeds the resident element's DIB, the incoming element 
           steals the slot, and the evicted resident continues probing. 
           This bounds the variance of DIB across the entire table.
           Crucially, for lookups: if we search for key K and encounter an occupied slot whose DIB is strictly 
           less than our current probe distance, we can terminate with a miss immediately, because K would 
           have displaced that element during insertion."

[15:00 - 35:00] Implementation Protocol
Candidate: "I'll implement the open-addressing table:
           - Struct `Entry` with `Key, Value, DIB, IsOccupied`.
           - In `Put`: loop with modulo wrap-around. If slot is empty, insert and exit. If key matches, update. 
             If `incoming.DIB > resident.DIB`, swap them and increment DIB.
           - In `TryGet`: increment probe distance, check key match, and apply the early termination invariant 
             `if (table[idx].DIB < currentDIB) return false;`."

[35:00 - 45:00] Complexity Deconstruction & Production Systems
Candidate: "Lookup and insert amortize to O(1) with O(N) contiguous storage.
           In production systems like Rust's `hashbrown` and Google's `absl::flat_hash_map`, the industry has 
           evolved toward Swiss Tables: using a 1-byte control metadata array and SSE2/AVX SIMD instructions 
           to probe 16 buckets in parallel in a single CPU cycle. Robin Hood hashing remains popular in embedded 
           systems where SIMD intrinsics are unavailable."
```

---

## 🔍 Chapter 6: Granular Edge-Case & Invariant Verification Table

| Edge Case Scenario | Concrete Input | Expected Behavior | Invariant Verification & Handling |
| :--- | :--- | :--- | :--- |
| **Search Miss on Early Exit**| Search for key with `DIB = 3`, encounter slot with `DIB = 1` | Returns `false` immediately without scanning remaining cluster. | Early exit guard: `resident.DIB < currentDIB` proves key was never inserted. |
| **Capacity Resizing at 80% Load**| Insert 13th element in table of size 16 | Triggers table doubling to size 32; re-hashes all elements. | Resizing preserves load factor threshold, preventing infinite probe loops. |
| **Existing Key Update** | `Put("foo", 1); Put("foo", 2)` | Value updated in place; count and DIB unchanged. | Key equality check `EqualityComparer.Default.Equals` handles update before DIB swap. |
| **Array Bounds Wraparound** | Collision at `capacity - 1` | Probing wraps cleanly to slot `0`. | Modulo indexing `idx = (idx + 1) % _capacity` maintains circular buffer ring. |
| **Cascading Eviction Chain** | Inserting element triggers 5 successive swaps | All 5 elements shifted right; final displaced item placed in empty slot. | Loop maintains `incoming` state machine until an empty slot is reached. |

---

> 🧭 **Navigation:** [← Previous Day](Week_16_Day_04_Cache_Oblivious_Algorithms_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Week Playbook →](WEEK_16_FULL_PLAYBOOK.md)

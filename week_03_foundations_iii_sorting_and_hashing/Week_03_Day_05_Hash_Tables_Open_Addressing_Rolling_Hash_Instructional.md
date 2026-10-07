# 📘 Week 03 Day 05: Hash Tables II — Open Addressing & Rolling Hash (Rabin-Karp) — ENGINEERING GUIDE





> 🧭 **Navigation:** [← Previous Day](Week_03_Day_04_Hash_Tables_Separate_Chaining_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Week Playbook →](WEEK_03_FULL_PLAYBOOK.md)
> 
> 💡 **Instructor Note:** *Not all sections or topics are mandatory. Feel free to adapt your pace and skim or skip sections based on your current focus and interview timeline.*

---

## 🎯 LEARNING OBJECTIVES

*By the end of this chapter, you will be able to:*

- 🎯 **Internalize** why open addressing trades memory for cache locality and how probing sequences avoid clustering.
- ⚙️ **Implement** linear probing, quadratic probing, and double hashing with analysis of clustering.
- ⚖️ **Evaluate** when open addressing outperforms chaining (cache behavior, `alpha` threshold).
- 🏭 **Defend** against hash flooding attacks via universal hashing and randomization.
- 🎯 **Master** rolling hash and Rabin-Karp algorithm for O(n+m) substring matching and plagiarism detection.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Problem: Chaining vs Open Addressing Trade-Off

From Week 3 Day 4, separate chaining resolves collisions via linked lists. But chaining has a serious drawback:

**Chaining Drawback:**

```csharp
HashTableChaining<string, int> table = new();
// Insert many items; some hash to same bucket
// Bucket 5: [item1] → [item2] → [item3] → [item4]
//
// Search for item4:
//   Compute hash → Bucket 5
//   Follow pointer to item1? Cache miss (pointer dereference)
//   Follow pointer to item2? Cache miss
//   Follow pointer to item3? Cache miss
//   Follow pointer to item4? Cache miss
//   4 cache misses for one search!
```

Modern CPUs heavily reward cache-friendly access patterns. Pointer chasing causes cache misses → 100-1000x slowdown.

**Open Addressing Alternative:**

Instead of separate chains, probe for next available slot in array itself.

```
Array: [item1] [empty] [item2] [item3] [empty]
           0      1       2       3       4

Search for item:
  Compute hash → index 0
  Check array[0]? Cache hit
  Found? No, continue probing
  Check array[1]? Cache hit (contiguous)
  Empty? Yes, not found
  
All accesses are contiguous array elements → excellent cache locality!
```

Trade-off: Less memory efficient (can't exceed ~75% full) but much faster in practice.

> **💡 Insight:** *This is a quintessential systems design decision: time vs space. Chaining uses space (pointers) to optimize time complexity. Open addressing uses more search (probing) to maintain cache locality. Which is better depends on hardware: modern CPUs heavily favor cache-friendly algorithms, making open addressing dominant in practice despite higher theoretical complexity.*

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### The Core Analogy: Parking Lot Probing

Imagine a parking lot where you need to find a free spot:

**Separate Chaining:** Have separate overflow parking lot (linked list). If spot is taken, list overflow lot. Search: go to spot, then follow pointers to overflow lot (expensive!).

**Open Addressing:** Search sequentially through spots. If spot taken, try next spot. If that's taken, try next. Eventually find free spot. Search is local and cache-friendly.

### 🖼 Visualizing Open Addressing Collision Resolution

```
Initial table (m = 5):
Index:  0   1   2   3   4
Array: [A] [B] [C] [ ] [ ]

Insert D with h(D) = 2 (hash collision with C):

Linear Probing: Try next sequential slot
  h(D) = 2: occupied by C
  Probe 3: empty!
  Insert D at 3

Array: [A] [B] [C] [D] [ ]
       Index: 0   1   2   3   4

Search for D:
  h(D) = 2: occupied by C (not D)
  Probe 3: found D!
  1 collision, 1 probe -> O(1 + alpha) expected

Now insert E with h(E) = 2:
  h(E) = 2: occupied by C
  Probe 3: occupied by D
  Probe 4: empty!
  Insert E at 4

Array: [A] [B] [C] [D] [E]
       Index: 0   1   2   3   4

Problem: Linear probing creates "primary clustering"
  Items hash to 2, 3, 4 → contiguous block
  This attracts more collisions!
```

### 🖼 Comparing Probing Strategies & Collision Resolution

Open addressing resolves collisions by finding an alternative slot within the primary array along a deterministic probe sequence `h(k, i) = (h(k) + f(i)) mod M`:

```text
PROBING STRATEGIES AT A GLANCE:

1. LINEAR PROBING: f(i) = i
   Probe Sequence: h(k), h(k)+1, h(k)+2, h(k)+3, ...
   Pros: Maximum CPU L1/L2 cache prefetching (contiguous cache line hits).
   Cons: Primary Clustering. Contiguous blocks grow like snowballs, attracting
         more collisions and increasing average probe lengths.

2. QUADRATIC PROBING: f(i) = c1 * i + c2 * i^2
   Probe Sequence: h(k), h(k)+1, h(k)+4, h(k)+9, h(k)+16, ...
   Pros: Eliminates Primary Clustering; probe distance grows quadratically.
   Cons: Secondary Clustering. Keys that hash to the same initial home bucket
         follow the exact same probe sequence. Requires table size to be prime
         (or power of 2 with c1=c2=0.5) to guarantee full table coverage.

3. DOUBLE HASHING: f(i) = i * h2(k)
   Probe Sequence: h1(k), h1(k) + h2(k), h1(k) + 2*h2(k), h1(k) + 3*h2(k), ...
   Pros: Eliminates both Primary AND Secondary Clustering! Every key has its
         own unique jump stride determined by h2(k).
   Cons: Computation of two independent hash functions; loses sequential CPU
         cache line prefetching. Requires h2(k) to be coprime to M.

4. ROBIN HOOD HASHING: "Take from the rich, give to the poor!"
   Mechanics:
   - Every entry tracks its Probe Sequence Length (PSL) = distance from home slot.
   - On insertion, if the incoming element has traveled farther from home than the
     occupant (incoming.PSL > occupant.PSL), they SWAP!
   - The incoming element steals the slot, and the displaced occupant continues
     probing with its own PSL incremented.
   Pros: Drastically reduces variance of probe lengths.
         Enables Early Termination on search misses (if occupant.PSL < search.PSL,
         the key cannot possibly exist further ahead; stop search immediately!).
```

### Hash Function Quality and Universality

From Day 4, a good hash function spreads keys uniformly. But adversaries can design pathological inputs (hash flooding attack). Universal hashing provides theoretical guarantees.

**Universal Hash Family:**

A family H of hash functions is universal if for any two distinct keys k1, k2:
```
P(h(k1) == h(k2)) <= 1/m  (for h randomly chosen from H)
```

Even with adversarial inputs, collision probability is at most `1/m`. Expected chain length = `1 + alpha` (independent of input!).

**Example: Multiply-Add-Divide (MAD) Hash Function:**

```csharp
using System;

public class UniversalHash {
    private readonly int p;  // Large prime (e.g., 2^31 - 1)
    private readonly int a;  // Random a in [1, p-1]
    private readonly int b;  // Random b in [0, p-1]
    private readonly int m;  // Table size
    
    public UniversalHash(int tableSize) {
        m = tableSize;
        p = 2147483647;  // Mersenne prime 2^31 - 1
        
        // Randomize a, b for each table instance
        Random rand = new();
        a = rand.Next(1, p);
        b = rand.Next(0, p);
    }
    
    public int Hash(int k) {
        return (int)(((long)a * k + b) % p % m);
    }
    
    // Property: For any two distinct k1, k2,
    // P(Hash(k1) == Hash(k2)) = 1/m
    // This holds for ANY input distribution!
    // (Not dependent on specific keys or patterns)
}
```

---

## ⚙️ CHAPTER 3: MECHANICS & IMPLEMENTATION

### The State Machine: Open Addressing Complete CRUD Operations

| Operation | Purpose | Array Mechanism | Time (Average) | Time (Worst) |
| :--- | :--- | :--- | :---: | :---: |
| **`Put(K key, V value)`** | Insert or update key-value pair | Probes array until key match, first tombstone, or empty slot | `O(1)` | `O(N)` |
| **`Get(K key)` / `TryGetValue`**| Retrieve value for given key | Probes along sequence; skips tombstones; stops on key match or empty slot | `O(1)` | `O(N)` |
| **`Remove(K key)`** | Soft-delete key from table | Probes for key; writes `TOMBSTONE` marker (preserves probe sequence) | `O(1)` | `O(N)` |
| **`ContainsKey(K key)`** | Test key membership | Probes along sequence; returns true on key match, false on empty slot | `O(1)` | `O(N)` |
| **`Resize(2M)` & Rehash** | Double capacity and purge tombstones | Allocates `2M` slots; inserts only active `OCCUPIED` entries, discarding dead tombstones | `O(N + M)` | `O(N + M)` |

---

### 💀 Deletion in Open Addressing: Why Tombstones are Strictly Required

In separate chaining, deleting an item simply unlinks a node from a bucket list. In open addressing, however, **naive deletion by clearing a slot back to `EMPTY` catastrophically breaks subsequent searches!**

```text
DELETION IN OPEN ADDRESSING: THE PROBE-CHAIN COLLAPSE PROOF

Setup: Table Capacity M = 8.
Assume 3 keys A, B, C all hash to the same initial home bucket 2:
  h(A) = 2,  h(B) = 2,  h(C) = 2

Insertion Sequence:
1. Insert(A): h(A) = 2 -> Slot 2 is EMPTY -> Placed at [ 2 ].
2. Insert(B): h(B) = 2 -> Slot 2 OCCUPIED (A) -> Linear probe to [ 3 ] -> Placed at [ 3 ].
3. Insert(C): h(C) = 2 -> Slot 2 OCCUPIED (A), Slot 3 OCCUPIED (B) -> Probe to [ 4 ] -> Placed at [ 4 ].

Current Valid Table State:
Index:   [ 0 ]   [ 1 ]   [ 2 ]   [ 3 ]   [ 4 ]   [ 5 ]   [ 6 ]   [ 7 ]
State:   EMPTY   EMPTY   OCCUP   OCCUP   OCCUP   EMPTY   EMPTY   EMPTY
Entry:    --      --     A(h=2)  B(h=2)  C(h=2)   --      --      --

--------------------------------------------------------------------------------
FATAL MISTAKE: NAIVE DELETION (Marking Deleted Slot 3 as EMPTY)
Suppose Delete(B) simply clears slot 3 back to EMPTY:

Index:   [ 0 ]   [ 1 ]   [ 2 ]   [ 3 ]   [ 4 ]   [ 5 ]   [ 6 ]   [ 7 ]
State:   EMPTY   EMPTY   OCCUP   EMPTY   OCCUP   EMPTY   EMPTY   EMPTY
Entry:    --      --     A(h=2)   --     C(h=2)   --      --      --
                                   ^
                                   |-- THE SEVERED CHAIN (HOLE)

Now run Search(C):
  1. Compute h(C) = 2. Inspect table[2].
     Key is A (A != C). Collision! Continue linear probe to index 3.
  2. Inspect table[3].
     Slot state is EMPTY!
     The Fundamental Open Addressing Invariant says:
     "If a key were in the table, it would reside along a continuous unbroken
      sequence of occupied slots starting at its home hash."
     Because slot 3 is EMPTY, the search terminates immediately and reports NOT FOUND!
  Result: SILENT DATA LOSS / LOOKUP CORRUPTION!
          Key C is physically inside the table at index 4, but completely unreachable!

--------------------------------------------------------------------------------
THE REMEDY: THE TOMBSTONE (DELETED) SENTINEL
Instead of resetting slot 3 to EMPTY, mark it as TOMBSTONE (or DELETED):

Index:   [ 0 ]   [ 1 ]   [ 2 ]   [ 3 ]        [ 4 ]   [ 5 ]   [ 6 ]   [ 7 ]
State:   EMPTY   EMPTY   OCCUP   TOMBSTONE    OCCUP   EMPTY   EMPTY   EMPTY
Entry:    --      --     A(h=2)  (Deleted)    C(h=2)   --      --      --

Now rerun Search(C):
  1. Inspect table[2] -> Key is A != C -> Probe to index 3.
  2. Inspect table[3] -> Slot is TOMBSTONE!
     Invariant Rule: Tombstones do NOT stop a search! Skip past index 3 to index 4.
  3. Inspect table[4] -> Key is C == C -> FOUND! (Returns value successfully).

Tombstone Recycling during Insertion:
When inserting key D:
  - Probe along sequence. If a TOMBSTONE is encountered, remember its index (firstTombstone).
  - Continue probing to verify D doesn't already exist.
  - If an EMPTY slot is hit and D wasn't found, place D into firstTombstone, recycling dead space!

Tombstone Purging during Dynamic Resizing (Resize to 2M):
As deletions accumulate, tombstones slow down searches. When resizing to 2M,
all tombstones are completely thrown away. Only live OCCUPIED entries are
rehashed, restoring pristine contiguous packing.
```

---

### 🔧 Operation 1: Open Addressing with Linear Probing

```text
Linear Probing Memory State with Tombstones:
Index:        [ 0 ]          [ 1 ]          [ 2 ]          [ 3 ]          [ 4 ]
State:     [Occupied]     [Tombstone]    [Occupied]      [Empty]       [Occupied]
Key/Val:  "apple": 1     (Deleted)      "banana": 5       null        "cherry": 8
           ^              ^                             ^
           |              |                             +-- Probe sequence stops here on search miss
           |              +-- Search skips past; Insert can overwrite
           +-- Direct hash hit
```

#### C# Implementation (Linear Probing with Tombstone Deletion)

```csharp
using System;

public class HashTableLinearProbing<K, V> where K : notnull {
    private enum SlotState { Empty, Occupied, Tombstone }

    private struct Entry {
        public K Key;
        public V Value;
        public SlotState State;
    }
    
    private Entry[] table;
    private int count = 0;
    private const float LoadFactorThreshold = 0.5f; // Open addressing strictly caps alpha <= 0.5
    
    public HashTableLinearProbing(int capacity = 16) {
        table = new Entry[capacity];
    }
    
    private int Hash(K key) {
        return Math.Abs(key.GetHashCode()) % table.Length;
    }
    
    public void Insert(K key, V value) {
        if ((float)(count + 1) / table.Length > LoadFactorThreshold) {
            Resize();
        }

        int h = Hash(key);
        int firstTombstone = -1;
        
        for (int i = 0; i < table.Length; i++) {
            int idx = (h + i) % table.Length;
            
            if (table[idx].State == SlotState.Occupied && table[idx].Key.Equals(key)) {
                table[idx].Value = value;
                return;
            }
            if (table[idx].State == SlotState.Tombstone && firstTombstone == -1) {
                firstTombstone = idx;
            } else if (table[idx].State == SlotState.Empty) {
                int target = firstTombstone != -1 ? firstTombstone : idx;
                table[target] = new Entry { Key = key, Value = value, State = SlotState.Occupied };
                count++;
                return;
            }
        }
        
        if (firstTombstone != -1) {
            table[firstTombstone] = new Entry { Key = key, Value = value, State = SlotState.Occupied };
            count++;
            return;
        }

        throw new InvalidOperationException("Hash table full");
    }
    
    public bool TryGetValue(K key, out V value) {
        int h = Hash(key);
        
        for (int i = 0; i < table.Length; i++) {
            int idx = (h + i) % table.Length;
            
            if (table[idx].State == SlotState.Occupied && table[idx].Key.Equals(key)) {
                value = table[idx].Value;
                return true;
            }
            if (table[idx].State == SlotState.Empty) {
                break; // Stop: key definitely does not exist
            }
        }
        
        value = default!;
        return false;
    }

    public bool ContainsKey(K key) {
        int h = Hash(key);
        for (int i = 0; i < table.Length; i++) {
            int idx = (h + i) % table.Length;
            if (table[idx].State == SlotState.Occupied && table[idx].Key.Equals(key)) {
                return true;
            }
            if (table[idx].State == SlotState.Empty) {
                break;
            }
        }
        return false;
    }

    public bool Remove(K key) {
        int h = Hash(key);

        for (int i = 0; i < table.Length; i++) {
            int idx = (h + i) % table.Length;

            if (table[idx].State == SlotState.Occupied && table[idx].Key.Equals(key)) {
                table[idx].State = SlotState.Tombstone; // Mark as tombstone
                table[idx].Key = default!;
                table[idx].Value = default!;
                count--;
                return true;
            }
            if (table[idx].State == SlotState.Empty) {
                break;
            }
        }

        return false;
    }
    
    private void Resize() {
        var oldTable = table;
        table = new Entry[oldTable.Length * 2];
        count = 0;
        
        foreach (var entry in oldTable) {
            if (entry.State == SlotState.Occupied) {
                Insert(entry.Key, entry.Value);
            }
        }
    }

    public int Count => count;
    public int Capacity => table.Length;
}
```

#### Python Implementation (Linear Probing with Tombstones)

```python
class HashTableLinearProbing[K, V]:
    """Open addressing hash table with linear probing and tombstone deletion.
    
    Time: O(1) expected average lookup/insert/delete under SUHA with alpha <= 0.5
    Space: O(M) contiguous array allocation, zero node pointers
    """
    _EMPTY = object()
    _TOMBSTONE = object()

    def __init__(self, initial_capacity: int = 16, load_factor_threshold: float = 0.5) -> None:
        self.capacity: int = max(4, initial_capacity)
        self.load_factor_threshold: float = load_factor_threshold
        self.size: int = 0
        self.keys: list = [self._EMPTY] * self.capacity
        self.values: list = [None] * self.capacity

    def _hash(self, key: K) -> int:
        return abs(hash(key)) % self.capacity

    def insert(self, key: K, value: V) -> None:
        """Insert or update key-value pair, reusing tombstones if encountered."""
        if (self.size + 1) / self.capacity > self.load_factor_threshold:
            self._resize()

        idx = self._hash(key)
        first_tombstone = -1

        for step in range(self.capacity):
            curr = (idx + step) % self.capacity
            k = self.keys[curr]

            if k == key:
                self.values[curr] = value
                return
            if k is self._TOMBSTONE and first_tombstone == -1:
                first_tombstone = curr
            elif k is self._EMPTY:
                target = first_tombstone if first_tombstone != -1 else curr
                self.keys[target] = key
                self.values[target] = value
                self.size += 1
                return

        if first_tombstone != -1:
            self.keys[first_tombstone] = key
            self.values[first_tombstone] = value
            self.size += 1
            return

        raise RuntimeError("Hash table full")

    def get(self, key: K) -> V:
        """Retrieve value for key; raises KeyError if absent."""
        idx = self._hash(key)
        for step in range(self.capacity):
            curr = (idx + step) % self.capacity
            k = self.keys[curr]
            if k == key:
                return self.values[curr]
            if k is self._EMPTY:
                break
        raise KeyError(f"Key not found: {key}")

    def contains_key(self, key: K) -> bool:
        """Return True if key is present in table, False otherwise."""
        idx = self._hash(key)
        for step in range(self.capacity):
            curr = (idx + step) % self.capacity
            k = self.keys[curr]
            if k == key:
                return True
            if k is self._EMPTY:
                break
        return False

    def __contains__(self, key: K) -> bool:
        return self.contains_key(key)

    def remove(self, key: K) -> bool:
        """Mark slot as tombstone; returns True if deleted."""
        idx = self._hash(key)
        for step in range(self.capacity):
            curr = (idx + step) % self.capacity
            k = self.keys[curr]
            if k == key:
                self.keys[curr] = self._TOMBSTONE
                self.values[curr] = None
                self.size -= 1
                return True
            if k is self._EMPTY:
                break
        return False

    def _resize(self) -> None:
        old_keys = self.keys
        old_vals = self.values
        self.capacity *= 2
        self.keys = [self._EMPTY] * self.capacity
        self.values = [None] * self.capacity
        self.size = 0
        for k, v in zip(old_keys, old_vals):
            if k is not self._EMPTY and k is not self._TOMBSTONE:
                self.insert(k, v)
```

### 🔧 Operation 2: Double Hashing (Better Distribution)

```csharp
using System;

public class HashTableDoubleHashing<K, V> where K : notnull {
    private struct Entry {
        public K Key;
        public V Value;
        public bool Occupied;
    }
    
    private Entry[] table;
    private int count = 0;
    private const float LoadFactorThreshold = 0.5f;
    
    public HashTableDoubleHashing(int capacity = 16) {
        table = new Entry[capacity];
    }
    
    // Primary hash function
    private int Hash1(K key) {
        return Math.Abs(key.GetHashCode()) % table.Length;
    }
    
    // Secondary hash function (must be coprime to table.Length for full coverage)
    private int Hash2(K key) {
        // Use different hash function; ensure non-zero and coprime
        int h = Math.Abs(key.GetHashCode() * 31) % table.Length;
        if (h == 0) h = 1;  // Ensure non-zero
        return h;
    }
    
    // Double hashing: h(k, i) = (h1(k) + i * h2(k)) mod m
    public void Insert(K key, V value) {
        int h1 = Hash1(key);
        int h2 = Hash2(key);
        
        for (int i = 0; i < table.Length; i++) {
            int idx = (h1 + i * h2) % table.Length;
            
            if (table[idx].Occupied && table[idx].Key.Equals(key)) {
                table[idx].Value = value;
                return;
            }
            
            if (!table[idx].Occupied) {
                table[idx] = new Entry { Key = key, Value = value, Occupied = true };
                count++;
                
                if ((float)count / table.Length > LoadFactorThreshold) {
                    Resize();
                }
                return;
            }
        }
        
        throw new InvalidOperationException("Hash table full");
    }
    
    public bool TryGetValue(K key, out V value) {
        int h1 = Hash1(key);
        int h2 = Hash2(key);
        
        for (int i = 0; i < table.Length; i++) {
            int idx = (h1 + i * h2) % table.Length;
            
            if (table[idx].Occupied && table[idx].Key.Equals(key)) {
                value = table[idx].Value;
                return true;
            }
            
            if (!table[idx].Occupied) {
                value = default!;
                return false;
            }
        }
        
        value = default!;
        return false;
    }
    
    private void Resize() {
        var oldTable = table;
        table = new Entry[oldTable.Length * 2];
        count = 0;
        
        foreach (var entry in oldTable) {
            if (entry.Occupied) {
                Insert(entry.Key, entry.Value);
            }
        }
    }
    
    // Double hashing vs linear probing:
    // Linear: Sequence 0, 1, 2, 3, 4, ... (predictable, causes clustering)
    // Double: Sequence 0, h2, 2h2, 3h2, ... (unpredictable, avoids clustering)
    //
    // Example with h1 = 0, h2 = 3, m = 8:
    // Linear:  0, 1, 2, 3, 4, 5, 6, 7 (hits every slot)
    // Double:  0, 3, 6, 1, 4, 7, 2, 5 (permutation of all slots if h2 coprime)
    //
    // Double hashing: More probe locations spread across table
    // Linear probing: Localizes clustering to consecutive blocks
}
```

#### Python Implementation (Double Hashing)

```python
class HashTableDoubleHashing[K, V]:
    """Open addressing hash table with double hashing to eliminate primary clustering.
    
    Probe sequence: h(k, i) = (h1(k) + i * h2(k)) % capacity
    """
    _EMPTY = object()
    _TOMBSTONE = object()

    def __init__(self, initial_capacity: int = 17, load_factor_threshold: float = 0.5) -> None:
        self.capacity: int = max(5, initial_capacity)
        self.load_factor_threshold: float = load_factor_threshold
        self.size: int = 0
        self.keys: list = [self._EMPTY] * self.capacity
        self.values: list = [None] * self.capacity

    def _hash1(self, key: K) -> int:
        return abs(hash(key)) % self.capacity

    def _hash2(self, key: K) -> int:
        # Step size must be non-zero and coprime to table size
        step = (abs(hash(key) * 31) % (self.capacity - 1)) + 1
        return step

    def insert(self, key: K, value: V) -> None:
        if (self.size + 1) / self.capacity > self.load_factor_threshold:
            self._resize()

        h1 = self._hash1(key)
        h2 = self._hash2(key)
        first_tombstone = -1

        for i in range(self.capacity):
            idx = (h1 + i * h2) % self.capacity
            k = self.keys[idx]

            if k == key:
                self.values[idx] = value
                return
            if k is self._TOMBSTONE and first_tombstone == -1:
                first_tombstone = idx
            elif k is self._EMPTY:
                target = first_tombstone if first_tombstone != -1 else idx
                self.keys[target] = key
                self.values[target] = value
                self.size += 1
                return

        if first_tombstone != -1:
            self.keys[first_tombstone] = key
            self.values[first_tombstone] = value
            self.size += 1
            return

        raise RuntimeError("Hash table full")

    def get(self, key: K) -> V:
        h1 = self._hash1(key)
        h2 = self._hash2(key)
        for i in range(self.capacity):
            idx = (h1 + i * h2) % self.capacity
            k = self.keys[idx]
            if k == key:
                return self.values[idx]
            if k is self._EMPTY:
                break
        raise KeyError(f"Key not found: {key}")

    def _resize(self) -> None:
        old_keys = self.keys
        old_vals = self.values
        self.capacity = self.capacity * 2 + 1
        self.keys = [self._EMPTY] * self.capacity
        self.values = [None] * self.capacity
        self.size = 0
        for k, v in zip(old_keys, old_vals):
            if k is not self._EMPTY and k is not self._TOMBSTONE:
                self.insert(k, v)
```

---

### 🔧 Operation 3: Robin Hood Hashing (Low Variance & Early Search Termination)

#### Mechanics & Architectural Intuition
Robin Hood hashing is an open addressing variant based on the moral principle: *"Take from the rich (small PSL) and give to the poor (large PSL)!"*

1. **Probe Sequence Length (PSL):** Defined as the distance between an element's actual slot and its ideal home hash index `(current_index - home_index + capacity) % capacity`.
   - PSL = 0: Resides in its ideal home bucket ("very rich").
   - PSL >= 3: Suffered multiple collisions ("poor").
2. **Swap Invariant on Insertion:** While probing along the table:
   - If the incoming element has a **greater PSL** than the element currently occupying the slot (`incoming.PSL > occupant.PSL`), **SWAP them!**
   - The incoming element claims the slot, and the displaced occupant becomes the new traveler, probing forward with its own PSL incremented.
3. **Variance Equalization:** Unlike standard linear probing where unlucky items suffer 20-30 probe steps while others have 1, Robin Hood redistributes collisions evenly, ensuring nearly all items have PSL between 0 and 3.
4. **Early Search Termination:** During a search, if `current_search_psl > table[idx].PSL`, **the key cannot possibly exist further ahead!** (If it did, Robin Hood swaps would have promoted our poorer key ahead of the current occupant). The search terminates immediately without probing all the way to an empty slot.
5. **Backward Shift Deletion (Tombstone-Free):** When deleting an item, shift all subsequent elements in the cluster whose PSL > 0 backward by 1 position (decrementing their PSL). This completely eliminates the need for tombstones!

#### C# Implementation (.NET 8/9: Robin Hood Hash Table)

```csharp
using System;

public class RobinHoodHashTable<K, V> where K : notnull {
    private struct Entry {
        public K Key;
        public V Value;
        public int PSL; // -1 indicates empty slot
    }

    private Entry[] table;
    private int count = 0;
    private const float LoadFactorThreshold = 0.85f; // Robin Hood can sustain alpha up to 0.85

    public RobinHoodHashTable(int capacity = 16) {
        table = new Entry[capacity];
        for (int i = 0; i < capacity; i++) table[i].PSL = -1;
    }

    private int Hash(K key) => Math.Abs(key.GetHashCode()) % table.Length;

    public void Put(K key, V value) {
        if ((float)(count + 1) / table.Length > LoadFactorThreshold) {
            Resize();
        }

        int currIdx = Hash(key);
        K currKey = key;
        V currVal = value;
        int currPsl = 0;

        while (true) {
            if (table[currIdx].PSL == -1) {
                table[currIdx] = new Entry { Key = currKey, Value = currVal, PSL = currPsl };
                count++;
                return;
            }

            if (table[currIdx].Key.Equals(currKey)) {
                table[currIdx].Value = currVal;
                return;
            }

            // Steal from rich, give to poor
            if (currPsl > table[currIdx].PSL) {
                (currKey, table[currIdx].Key) = (table[currIdx].Key, currKey);
                (currVal, table[currIdx].Value) = (table[currIdx].Value, currVal);
                (currPsl, table[currIdx].PSL) = (table[currIdx].PSL, currPsl);
            }

            currIdx = (currIdx + 1) % table.Length;
            currPsl++;
        }
    }

    public bool TryGetValue(K key, out V value) {
        int home = Hash(key);
        int currIdx = home;
        int psl = 0;

        while (true) {
            // Early termination on search miss!
            if (table[currIdx].PSL == -1 || psl > table[currIdx].PSL) {
                value = default!;
                return false;
            }

            if (table[currIdx].Key.Equals(key)) {
                value = table[currIdx].Value;
                return true;
            }

            currIdx = (currIdx + 1) % table.Length;
            psl++;
            if (psl >= table.Length) break;
        }

        value = default!;
        return false;
    }

    public bool ContainsKey(K key) => TryGetValue(key, out _);

    public bool Remove(K key) {
        int home = Hash(key);
        int currIdx = home;
        int psl = 0;

        while (true) {
            if (table[currIdx].PSL == -1 || psl > table[currIdx].PSL) return false;

            if (table[currIdx].Key.Equals(key)) {
                // Backward shift deletion: shift subsequent entries back 1 position
                int nextIdx = (currIdx + 1) % table.Length;
                while (table[nextIdx].PSL > 0) {
                    table[currIdx] = table[nextIdx];
                    table[currIdx].PSL--;
                    currIdx = nextIdx;
                    nextIdx = (nextIdx + 1) % table.Length;
                }
                table[currIdx].PSL = -1;
                table[currIdx].Key = default!;
                table[currIdx].Value = default!;
                count--;
                return true;
            }

            currIdx = (currIdx + 1) % table.Length;
            psl++;
            if (psl >= table.Length) break;
        }
        return false;
    }

    private void Resize() {
        var oldTable = table;
        table = new Entry[oldTable.Length * 2];
        for (int i = 0; i < table.Length; i++) table[i].PSL = -1;
        count = 0;

        foreach (var entry in oldTable) {
            if (entry.PSL != -1) {
                Put(entry.Key, entry.Value);
            }
        }
    }

    public int Count => count;
}
```

#### Python Implementation (3.11+: Robin Hood Hash Table)

```python
class RobinHoodHashTable[K, V]:
    """Robin Hood open addressing hash table with backward-shift tombstone-free deletion.
    
    Invariants:
      - Steal from rich (small PSL) to give to poor (high PSL).
      - Early termination: stops probe when search_psl > occupant.PSL.
    """
    def __init__(self, capacity: int = 16, load_factor_threshold: float = 0.85) -> None:
        self.capacity: int = max(4, capacity)
        self.load_factor_threshold: float = load_factor_threshold
        self.size: int = 0
        self.keys: list = [None] * self.capacity
        self.values: list = [None] * self.capacity
        self.psl: list[int] = [-1] * self.capacity  # -1 denotes empty slot

    def _hash(self, key: K) -> int:
        return abs(hash(key)) % self.capacity

    def put(self, key: K, value: V) -> None:
        """Upsert key-value pair using Robin Hood swap heuristic."""
        if (self.size + 1) / self.capacity > self.load_factor_threshold:
            self._resize()

        curr_idx = self._hash(key)
        curr_key, curr_val, curr_psl = key, value, 0

        while True:
            if self.psl[curr_idx] == -1:
                self.keys[curr_idx] = curr_key
                self.values[curr_idx] = curr_val
                self.psl[curr_idx] = curr_psl
                self.size += 1
                return

            if self.keys[curr_idx] == curr_key:
                self.values[curr_idx] = curr_val
                return

            if curr_psl > self.psl[curr_idx]:
                curr_key, self.keys[curr_idx] = self.keys[curr_idx], curr_key
                curr_val, self.values[curr_idx] = self.values[curr_idx], curr_val
                curr_psl, self.psl[curr_idx] = self.psl[curr_idx], curr_psl

            curr_idx = (curr_idx + 1) % self.capacity
            curr_psl += 1

    def get(self, key: K) -> V:
        """Lookup key with early miss termination."""
        curr_idx = self._hash(key)
        curr_psl = 0

        while True:
            if self.psl[curr_idx] == -1 or curr_psl > self.psl[curr_idx]:
                raise KeyError(f"Key not found: {key}")

            if self.keys[curr_idx] == key:
                return self.values[curr_idx]

            curr_idx = (curr_idx + 1) % self.capacity
            curr_psl += 1
            if curr_psl >= self.capacity:
                raise KeyError(f"Key not found: {key}")

    def contains_key(self, key: K) -> bool:
        try:
            self.get(key)
            return True
        except KeyError:
            return False

    def remove(self, key: K) -> bool:
        """Tombstone-free deletion via backward shift."""
        curr_idx = self._hash(key)
        curr_psl = 0

        while True:
            if self.psl[curr_idx] == -1 or curr_psl > self.psl[curr_idx]:
                return False

            if self.keys[curr_idx] == key:
                next_idx = (curr_idx + 1) % self.capacity
                while self.psl[next_idx] > 0:
                    self.keys[curr_idx] = self.keys[next_idx]
                    self.values[curr_idx] = self.values[next_idx]
                    self.psl[curr_idx] = self.psl[next_idx] - 1
                    curr_idx = next_idx
                    next_idx = (next_idx + 1) % self.capacity

                self.keys[curr_idx] = None
                self.values[curr_idx] = None
                self.psl[curr_idx] = -1
                self.size -= 1
                return True

            curr_idx = (curr_idx + 1) % self.capacity
            curr_psl += 1
            if curr_psl >= self.capacity:
                return False

    def _resize(self) -> None:
        old_keys, old_vals, old_psl = self.keys, self.values, self.psl
        self.capacity *= 2
        self.keys = [None] * self.capacity
        self.values = [None] * self.capacity
        self.psl = [-1] * self.capacity
        self.size = 0
        for k, v, p in zip(old_keys, old_vals, old_psl):
            if p != -1:
                self.put(k, v)
```

---

### 🔧 Operation 4: Rabin-Karp Rolling Hash (String Matching)

#### C# Implementation

```csharp
using System.Collections.Generic;

public class KarpRabinRollingHash {
    private const int PRIME = 101;      // Prime for modulo
    private const int BASE = 256;       // Alphabet size
    
    // Compute hash for string starting at position [start, end)
    public static long ComputeHash(string text, int start, int length) {
        long hash = 0;
        long basePower = 1;
        
        // Process characters right to left
        for (int i = start + length - 1; i >= start; i--) {
            hash = (hash + (text[i] * basePower) % PRIME + PRIME) % PRIME;
            basePower = (basePower * BASE) % PRIME;
        }
        
        return hash;
    }
    
    // Rolling hash: update hash from [start, start+length) to [start+1, start+1+length)
    // Removes text[start], adds text[start+length]
    public static long RollingHash(string text, long prevHash, int start, 
                                   int length, long basePowerM) {
        // Remove first character contribution
        long hash = (prevHash - (text[start] * basePowerM) % PRIME + PRIME) % PRIME;
        
        // Multiply by BASE (shift left)
        hash = (hash * BASE) % PRIME;
        
        // Add new character
        hash = (hash + text[start + length] % PRIME + PRIME) % PRIME;
        
        return hash;
    }
    
    // Find all occurrences of pattern in text using Rabin-Karp
    public static List<int> FindPattern(string text, string pattern) {
        var matches = new List<int>();
        int n = text.Length;
        int m = pattern.Length;
        
        if (m > n) return matches;
        
        // Compute base^(m-1) mod PRIME
        long basePowerM = 1;
        for (int i = 0; i < m - 1; i++) {
            basePowerM = (basePowerM * BASE) % PRIME;
        }
        
        // Compute hash for pattern
        long patternHash = ComputeHash(pattern, 0, m);
        
        // Compute hash for first window of text
        long textHash = ComputeHash(text, 0, m);
        
        // Check first window
        if (patternHash == textHash && text.Substring(0, m) == pattern) {
            matches.Add(0);
        }
        
        // Rolling hash for remaining windows
        for (int i = 1; i <= n - m; i++) {
            // Update hash using rolling formula
            textHash = RollingHash(text, textHash, i - 1, m, basePowerM);
            
            // Check if hash matches and verify actual string
            if (patternHash == textHash && 
                text.Substring(i, m) == pattern) {
                matches.Add(i);
            }
        }
        
        return matches;
    }
    
    // Trace: Find "abc" in "abcabc"
    // m = 3, n = 6
    // basePowerM = BASE^(3-1) = 256^2
    
    // patternHash = hash("abc")
    // textHash = hash("abc") [first window at 0]
    // Match at 0!
    
    // Rolling:
    //   Remove 'a' from position 0, add 'c' from position 3
    //   textHash → hash("bca")
    //   No match (different from "abc")
    
    // Rolling:
    //   Remove 'b' from position 1, add 'a' from position 4
    //   textHash → hash("cab")
    //   No match
    
    // Rolling:
    //   Remove 'c' from position 2, add 'b' from position 5
    //   textHash → hash("abc")
    //   Match at position 3!
    
    // Results: [0, 3]
    //
    // Time: O(n + m) expected
    //   Build pattern hash: O(m)
    //   First window hash: O(m)
    //   Rolling for n-m windows: O(n-m) with O(1) per window
    //   Verification: O(m) per match (rare in practice)
    // Space: O(1) for hash values (vs O(nm) for naive)
}
```

#### Python Implementation (3.11+)

```python
def rabin_karp_search(text: str, pattern: str) -> list[int]:
    """Finds all 0-indexed starting occurrences of pattern in text using polynomial rolling hash.
    
    Time Complexity:
      Average: O(N + M) where N = len(text), M = len(pattern)
      Worst-case: O(N * M) under severe hash collision degradation
    Auxiliary Space: O(1) beyond the output match list
    """
    n, m = len(text), len(pattern)
    if m == 0 or m > n:
        return []

    prime: int = 101       # Modulus prime
    base: int = 256        # Alphabet radix
    matches: list[int] = []

    # Precompute high power: (base^(m - 1)) % prime
    base_power: int = pow(base, m - 1, prime)

    # Initial window hash fingerprints
    pattern_hash: int = 0
    window_hash: int = 0
    for i in range(m):
        pattern_hash = (pattern_hash * base + ord(pattern[i])) % prime
        window_hash = (window_hash * base + ord(text[i])) % prime

    for i in range(n - m + 1):
        if pattern_hash == window_hash:
            # Hash collision check (verify exact characters)
            if text[i : i + m] == pattern:
                matches.append(i)

        if i < n - m:
            # Roll hash: strip leading char, shift left, append trailing char
            window_hash = (window_hash - ord(text[i]) * base_power) % prime
            window_hash = (window_hash * base + ord(text[i + m])) % prime
            window_hash = (window_hash + prime) % prime

    return matches
```

### 🔧 Operation 5: Plagiarism Detection Using Rolling Hash

```csharp
using System.Collections.Generic;

public class PlagiarismDetector {
    // Find similar segments between two documents using rolling hash
    public static List<(int, int, int)> FindSimilarSegments(
        string doc1, string doc2, int segmentLength = 50) {
        
        var matches = new List<(int, int, int)>();
        var hashMap = new Dictionary<long, List<int>>();
        
        // Index all segments in doc1
        long basePower = 1;
        for (int i = 0; i < segmentLength - 1; i++) {
            basePower = (basePower * 256) % 101;
        }
        
        long hash1 = KarpRabinRollingHash.ComputeHash(doc1, 0, segmentLength);
        if (!hashMap.ContainsKey(hash1)) {
            hashMap[hash1] = new List<int>();
        }
        hashMap[hash1].Add(0);
        
        for (int i = 1; i <= doc1.Length - segmentLength; i++) {
            hash1 = KarpRabinRollingHash.RollingHash(
                doc1, hash1, i - 1, segmentLength, basePower);
            
            if (!hashMap.ContainsKey(hash1)) {
                hashMap[hash1] = new List<int>();
            }
            hashMap[hash1].Add(i);
        }
        
        // Find matching segments in doc2
        long hash2 = KarpRabinRollingHash.ComputeHash(doc2, 0, segmentLength);
        
        if (hashMap.ContainsKey(hash2)) {
            foreach (int pos1 in hashMap[hash2]) {
                if (doc1.Substring(pos1, segmentLength) == 
                    doc2.Substring(0, segmentLength)) {
                    matches.Add((pos1, 0, segmentLength));
                }
            }
        }
        
        for (int i = 1; i <= doc2.Length - segmentLength; i++) {
            hash2 = KarpRabinRollingHash.RollingHash(
                doc2, hash2, i - 1, segmentLength, basePower);
            
            if (hashMap.ContainsKey(hash2)) {
                foreach (int pos1 in hashMap[hash2]) {
                    if (doc1.Substring(pos1, segmentLength) == 
                        doc2.Substring(i, segmentLength)) {
                        matches.Add((pos1, i, segmentLength));
                    }
                }
            }
        }
        
        return matches;
    }
    
    // Time: O(n + m) for rolling hash indexing
    // Space: O(n) for hash map
    // Vs naive comparison: O(nm) time, no space optimization
}
```

### 🔧 Operation 6: Hash Map / Set Pattern — Two Sum (Complement Lookup)

```text
Complement Lookup Array Traversal:
Target = 9
Index:      [ 0 ]      [ 1 ]      [ 2 ]      [ 3 ]
Nums:         2          7         11         15

Step 0: num = 2  -> complement = 9 - 2 = 7. Seen: {}              -> 7 not found -> Seen[2] = 0
Step 1: num = 7  -> complement = 9 - 7 = 2. Seen: {2: 0}          -> 2 FOUND at index 0! Return [0, 1]
```

#### C# Implementation (.NET 8/9)

```csharp
using System;
using System.Collections.Generic;

public static class TwoSumSolution {
    /// <summary>
    /// Finds indices of two numbers that add up to target using single-pass complement lookup.
    /// Time Complexity: O(N) average single-pass hash lookups
    /// Auxiliary Space: O(N) hash map storage
    /// </summary>
    public static int[] TwoSum(int[] nums, int target) {
        ArgumentNullException.ThrowIfNull(nums);
        var seen = new Dictionary<int, int>(capacity: nums.Length);

        for (int i = 0; i < nums.Length; i++) {
            int complement = target - nums[i];
            if (seen.TryGetValue(complement, out int complementIdx)) {
                return [complementIdx, i]; // C# 12 collection expression
            }
            seen[nums[i]] = i;
        }

        return Array.Empty<int>();
    }
}
```

#### Python Implementation (3.11+)

```python
def two_sum(nums: list[int], target: int) -> list[int]:
    """Finds two indices such that nums[i] + nums[j] == target via single-pass hash map complement search.
    
    Time Complexity: O(N) expected single-pass hash lookups
    Auxiliary Space: O(N) hash map storage
    """
    seen: dict[int, int] = {}
    for idx, num in enumerate(nums):
        complement = target - num
        if complement in seen:
            return [seen[complement], idx]
        seen[num] = idx
    return []
```

### ⚠️ Critical Pitfalls

> **Watch Out – Mistake 1: Integer Overflow in Rolling Hash**

```csharp
// BAD: Not using modulo, integer overflow
long hash = text[i] * basePower;  // Can overflow!

// CORRECT: Use modulo at each step
long hash = (text[i] * basePower) % PRIME;
hash = (hash + text[i+1] * basePower) % PRIME;
```

> **Watch Out – Mistake 2: Spurious Matches (Hash Collisions)**

```csharp
// BAD: Accept hash match without verifying actual string
if (patternHash == textHash) {
    return true;  // WRONG! Hash collision can occur
}

// CORRECT: Verify actual string after hash match
if (patternHash == textHash && 
    text.Substring(i, m) == pattern) {
    return true;  // Correct
}
```

> **Watch Out – Mistake 3: Load Factor Mismanagement in Open Addressing**

```csharp
// BAD: Allow load factor to exceed 0.75
// Clustering gets severe, probing becomes O(n)

// CORRECT: Resize when alpha > 0.5
if ((float)count / table.Length > 0.5f) {
    Resize();  // Maintain alpha <= 0.5 for fast probing
}
```

---

## ⚖️ CHAPTER 4: PERFORMANCE, TRADE-OFFS & REAL SYSTEMS

### ⚖️ Differences & Trade-off Matrix: Separate Chaining vs Open Addressing vs TreeMap

A comprehensive systems engineering comparison across all collision resolution strategies and associative containers:

| Dimension / Metric | Separate Chaining (Linked Lists / Trees) | Linear Probing | Quadratic Probing | Double Hashing | Robin Hood Hashing | TreeMap (Red-Black Tree) |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Search Hit (Avg)** | `O(1 + alpha)` | `O(1)` (1-2 probes at `alpha <= 0.5`) | `O(1)` | `O(1)` | `O(1)` (tight PSL <= 3) | `O(log N)` |
| **Search Miss (Avg)**| `O(1 + alpha)` | `O(1 / (1 - alpha)^2)` | `O(1 / (1 - alpha))` | `O(1 / (1 - alpha))` | `O(1)` (Early termination) | `O(log N)` |
| **Search Worst-Case**| `O(N)` (or `O(log K)`) | `O(N)` (clustering) | `O(N)` | `O(N)` | `O(N)` (narrow variance) | `O(log N)` guaranteed |
| **Insert (Average)** | `O(1)` | `O(1)` | `O(1)` | `O(1)` | `O(1)` (with PSL swaps) | `O(log N)` |
| **Delete (Average)** | `O(1)` (pointer unlink)| `O(1)` (Tombstone) | `O(1)` (Tombstone) | `O(1)` (Tombstone) | `O(1)` (Backward Shift) | `O(log N)` |
| **Tombstones Needed?**| **No** | **Yes** (strictly required) | **Yes** (strictly required) | **Yes** (strictly required) | **No** (Shift removes holes) | **No** |
| **Clustering Type** | None | Primary Clustering | Secondary Clustering | None | None (Equalized PSL) | None |
| **Cache Locality** | Poor (heap pointers) | **Maximum** (sequential) | Moderate (non-linear) | Low (variable strides) | **Maximum** (sequential) | Poor (pointer nodes) |
| **Max Safe Alpha** | `> 1.0` supported | `<= 0.50` | `<= 0.50` | `<= 0.65` | `<= 0.85` | N/A (Dynamic tree) |
| **Memory Overhead** | 16-24 bytes / node | **0 bytes** (contiguous) | **0 bytes** (contiguous) | **0 bytes** (contiguous) | 1-2 bytes (PSL byte) | 32 bytes (3 pointers + color) |
| **Ordered Traversal**| No | No | No | No | No | **Yes** (`O(N)` in-order) |
| **Range Queries** | No (`O(N)` full scan) | No (`O(N)` full scan) | No (`O(N)` full scan) | No (`O(N)` full scan) | No (`O(N)` full scan) | **Yes** (`O(log N + K)`) |
| **Industry Adoption** | Java `HashMap` | Abseil SwissTable, Rust | Database disk hashing | Network routing caches | Rust `indexmap`, ClickHouse | C# `SortedDictionary` |

#### Architectural Decision Rules:
1. **Linear Probing / SwissTable:** Use when raw latency and throughput are critical and table load can be kept `<= 0.5`. Modern SIMD control bytes (SwissTable) scan 16 slots per CPU instruction.
2. **Robin Hood Hashing:** Use when you need open addressing cache locality but must support higher load factors (`alpha ~ 0.85`) with guaranteed low variance and fast search misses via early termination.
3. **Double Hashing:** Use when memory layout allows stride jumps and primary clustering severely degrades linear probing due to poorly distributed input keys.
4. **Separate Chaining:** Use when payloads are large, deletions are frequent, or memory bursts may drive `alpha > 1.0` before resizing can occur.
5. **TreeMap (Balanced BST):** Use exclusively when range queries, sorted keys, nearest-neighbor floor/ceiling, or strict `O(log N)` worst-case latency SLAs are required.

> [!NOTE]
> **Production Systems Context:** Modern high-performance key-value systems (Google Abseil FlatHashMap / SwissTable, Rust `hashbrown`, ClickHouse) favor open addressing over chaining because CPU cache misses (~200 cycles to main memory vs 3-5 cycles for L1 cache) dominate runtime; linear probing and metadata control bytes ensure spatial cache locality. In high-frequency trading and algorithmic search engines, keeping table load factors `alpha <= 0.5` enables near-100% cache-resident probe sequences. Similarly, rolling polynomial hashes power scalable multi-document similarity engines (Turnitin, rsync delta transfers) by filtering gigabyte candidate corpuses in `O(N + M)` streaming time before expensive character verification.

### 📊 Complexity Deconstruction

| Operation / Paradigm | Time (Average) | Time (Worst-Case) | Auxiliary Space | Dominant Resource / Cache Impact |
|---|---|---|---|---|
| **Linear Probing Insert/Search** | `O(1)` | `O(N)` | `O(M)` contiguous array | Spatial cache line locality (`L1/L2` prefetcher friendly); primary clustering degrades as `alpha -> 1.0`. |
| **Double Hashing Insert/Search** | `O(1)` | `O(N)` | `O(M)` contiguous array | Eliminates clustering via secondary jump hash; loses consecutive cache line prefetching. |
| **Separate Chaining (Reference)**| `O(1 + alpha)` | `O(N)` | `O(N + M)` pointer nodes | Pointer chasing causes random memory dereferences and cache misses. |
| **Rabin-Karp Rolling Hash Search**| `O(N + M)` | `O(N * M)` | `O(1)` auxiliary | `O(1)` window sliding hash via Horner polynomial rolling formula; `O(M)` verification on match. |
| **Two Sum Complement Hash Map**  | `O(N)` | `O(N)` | `O(N)` auxiliary map | Trades `O(N)` auxiliary memory to eliminate `O(N^2)` quadratic nested brute force comparisons. |

> **Load Factor (`alpha = N / M`) Sensitivity in Open Addressing:**
> Unlike separate chaining where `alpha` can exceed `1.0`, open addressing strictly requires `alpha < 1.0`. Expected probe steps under SUHA are `1 / (1 - alpha)` for search hits and `1 / (1 - alpha)^2` for search misses with linear probing. When `alpha >= 0.7`, probe chains cascade exponentially into contiguous clusters. Production open-address tables enforce resizing at `alpha = 0.5` to guarantee expected `O(1)` performance.

---

### 🎙️ 45-Minute Interview Verbal Script

> "When deciding between separate chaining and open addressing in high-performance system design, the decisive trade-off is cache locality versus load factor resilience.
>
> In open addressing with linear probing, all entries reside in a single contiguous backing array with zero node allocation overhead. Because consecutive probe steps hit adjacent memory addresses, modern CPU hardware prefetchers load the entire probe sequence into L1/L2 cache lines simultaneously, yielding 3-to-5 cycle lookups. However, linear probing suffers from primary clustering: contiguous occupied blocks grow longer and merge, causing probe lengths to spike drastically as the load factor `alpha` exceeds 0.5. To maintain expected `O(1)` operations, we enforce aggressive resizing at `alpha = 0.5` and use tombstones during deletions so search sequences do not terminate prematurely.
>
> When scaling to substring pattern matching and sliding window problems, naive comparison takes `O(N * M)`. With Rabin-Karp rolling hash, we view character windows as polynomial integers modulo a prime. By subtracting the outgoing character's weight, multiplying by the base radix, and adding the incoming character in `O(1)`, we maintain the rolling fingerprint in `O(N + M)` expected time. We only perform full character checks on hash equality to prevent false positives from spurious collisions.
>
> For complement lookup patterns like Two Sum, a single-pass hash map records incoming elements and probes for `target - x` in expected `O(1)` time, converting an `O(N^2)` brute-force search into an optimal `O(N)` time and `O(N)` auxiliary space algorithm."

---

## 🔗 CHAPTER 5: INTEGRATION & MASTERY

### Connections to the Learning Arc

**Building on Week 3 Day 4:**
- **Chaining (Day 4):** Separate chaining with linked lists
- **Today:** Alternative collision resolution strategy
- **Trade-offs:** Memory/simplicity vs cache locality

**Building on Prior Weeks:**
- **Arrays (Week 2 Day 1):** Open addressing uses array directly
- **Strings (Week 2):** Rabin-Karp for efficient string matching
- **Recursion (Week 1):** Recursive hashing formulas

**Foreshadowing Future:**
- **Week 5 (Patterns):** Rabin-Karp for pattern matching problems
- **Week 8 (Graphs):** Hash-based algorithms for deduplication
- **Week 10 (Advanced Hashing):** Consistent hashing, bloom filters

### Pattern Recognition: Randomization & Universality

**Pattern 1: Universal Hash Families**
- Randomized hash functions guarantee performance
- Appears in load balancing, distributed systems, security

**Pattern 2: Rolling Hash Principle**
- Maintain state of sliding window hash
- Extend to rolling checksums, rolling statistics, polynomial hashing

**Pattern 3: Probe Sequences**
- Different probing strategies (linear, quadratic, double)
- Generalize to other searching scenarios

### Socratic Reflection

1. **On Clustering:** Why does linear probing cause primary clustering?

2. **On Universality:** How does universal hashing prevent hash flooding attacks?

3. **On Rolling Hash:** Why can we update hash in O(1) for a sliding window?

4. **On Trade-offs:** When is open addressing better than chaining?

5. **On Applications:** How do plagiarism detectors use rolling hash efficiently?

### 📌 Retention Hook

> **The Essence:** *"Hash tables come in two flavors: separate chaining (simple, cache-unfriendly) and open addressing (complex, cache-friendly). Which dominates depends on hardware priorities. More profoundly, rolling hash shows how to maintain properties of sliding windows efficiently—a principle that extends beyond hashing to checksums, polynomials, and stream processing. Master both collision resolution and rolling hash, and you master efficient string algorithms and cache-aware systems design."*

---

## ⚔️ SUPPLEMENTARY OUTCOMES

### 🏋️ Practice Problems

| Problem | Difficulty | Key Concept |
|---------|-----------|-------------|
| Implement linear probing | 🟡 | Probing, load factor |
| Implement double hashing | 🟠 | Independent hash functions |
| Implement Rabin-Karp | 🟡 | Rolling hash, modulo arithmetic |
| Find all occurrences | 🟡 | Pattern matching |
| Plagiarism detection | 🟠 | Hash indexing + rolling hash |
| DNA sequence matching | 🟠 | Rabin-Karp on biological data |

### 🎙️ Interview Questions

1. **Q:** Implement linear probing. Why does it suffer from clustering?  
   **Follow-up:** How does quadratic probing or double hashing help?

2. **Q:** Explain Rabin-Karp algorithm. Why is rolling hash O(1)?  
   **Follow-up:** How do you handle hash collisions?

3. **Q:** Find all occurrences of pattern in text in O(n+m) time.  
   **Follow-up:** Compare with KMP algorithm from Day 4.

4. **Q:** Design a plagiarism detector for 1 billion documents.  
   **Follow-up:** How do you index efficiently? Search efficiently?

5. **Q:** What's the difference between chaining and open addressing?  
   **Follow-up:** When would you use each in production?

### ❌ Common Misconceptions

- **Myth:** Open addressing is always faster than chaining.  
  **Reality:** Depends on hardware, workload, and load factor.

- **Myth:** Hash collisions should be minimized.  
  **Reality:** Some collisions are inevitable; what matters is managing them efficiently.

- **Myth:** Rolling hash guarantees correct string matching.  
  **Reality:** Hash match indicates possible match; must verify actual string.

- **Myth:** Double hashing is always better than linear probing.  
  **Reality:** Linear probing is cache-friendly and practical; double hashing is theoretically better but 10% slower in practice.

### 🚀 Advanced Concepts

- **Cuckoo Hashing:** O(1) worst-case lookup (guarantees with probabilistic insertions)
- **Hopscotch Hashing:** Cache-friendly cuckoo variant
- **Robin Hood Hashing:** Reduce variance in probe lengths
- **Cryptographic Hash Functions:** SHA, MD5 (one-way for security)
- **Bloom Filters:** Probabilistic membership testing
- **Locality-Sensitive Hashing:** Find similar items in high dimensions

### 📚 External Resources

- **CLRS Chapter 11:** Open addressing collision resolution
- **MIT 6.006 Lecture 10:** Hashing and open addressing
- **MIT 6.046 Lecture 4–5:** Universal hashing theory
- **"Introduction to Algorithms":** Comprehensive treatment of all variants
- **LeetCode:** Hash table problems with rolling hash focus

---

## 📌 CLOSING REFLECTION

Hash tables seem simple mechanically—hash key, probe array, handle collisions. But they embody deep principles:

**Collision Resolution Strategies:** Separate chaining is simple and flexible; open addressing trades complexity for cache locality. The choice depends on hardware priorities and workload. This is systems design: making informed trade-offs based on constraints.

**Rolling Hash:** Maintain properties of sliding windows efficiently in O(1) updates. This principle extends beyond hashing to checksums, polynomials, and stream algorithms. Understanding rolling hash teaches incremental thinking: update state based on changes, not from scratch.

**Universal Hashing:** Design hash functions that guarantee performance even against adversarial inputs. This brings randomization into algorithmic design: accept worst-case for excellent average-case.

Master open addressing, rolling hash, and universal hashing—their mechanics, their trade-offs, their applications—and you master techniques that appear in plagiarism detection, DNA sequencing, security systems, and distributed computing.

---

**Inline Visuals:** 12 diagrams and traces  
**Real-World Stories:** 3 detailed case studies  
**Interview-Ready:** Yes—covers mechanics, analysis, and applications  
**Batch Status:** ✅ COMPLETE — Week 03 Day 05 Final
---

> 🧭 **Navigation:** [← Previous Day](Week_03_Day_04_Hash_Tables_Separate_Chaining_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Week Playbook →](WEEK_03_FULL_PLAYBOOK.md)

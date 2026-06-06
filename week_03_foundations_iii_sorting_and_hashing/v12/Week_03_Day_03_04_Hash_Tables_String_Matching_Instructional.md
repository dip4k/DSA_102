# 📘 Week 03 Day 03: Hash Tables & Hashing — ENGINEERING GUIDE

**Metadata:**
- **Week:** 3 | **Day:** 3
- **Category:** Foundations / Advanced Data Structures
- **Difficulty:** 🟡 Intermediate–Advanced (builds on Week 2 arrays, Week 3 Days 1–2)
- **Real-World Impact:** Hash tables are the workhorse of modern systems. Every language, database, and compiler uses hashing. Hash tables provide O(1) average lookup, beating binary search's O(log n). Understanding hash functions, collision resolution, and load factors is critical for systems design, caching, and distributed systems.
- **Prerequisites:** Week 2 (arrays), Week 3 Days 1–2 (sorting and heaps motivation)
- **MIT Alignment:** Hash tables and hashing from MIT 6.006 Lecture 7–8

---

## 🎯 LEARNING OBJECTIVES

*By the end of this chapter, you will be able to:*

- 🎯 **Internalize** how hash functions map keys to buckets and why collisions matter.
- ⚙️ **Implement** hash tables with chaining and open addressing collision resolution.
- ⚖️ **Evaluate** hash function quality and collision resolution strategies.
- 🏭 **Connect** hashing to caching, distributed systems, and bloom filters.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Problem: O(1) Lookup vs O(log n) Binary Search

From Week 2, you know binary search gives O(log n) lookup on sorted data. Can you do better?

**Sorted Array + Binary Search:**
```csharp
Array.BinarySearch(arr, target);  // O(log n)
// For 1 million elements: ~20 comparisons
```

**Hash Table:**
```csharp
Dictionary<K, V> dict = new();
dict[key];  // O(1) average
// For 1 million elements: ~1-2 lookups
```

Hash tables beat binary search by computing index directly instead of searching. The trade-off: requires good hash function and manages collisions.

> **💡 Insight:** *Hash tables achieve O(1) average by exploiting randomness: a good hash function spreads keys randomly across buckets. This is probabilistic—individual operations might be slow, but average is fast. This principle (randomization for average-case efficiency) appears everywhere: randomized quicksort, load balancing, distributed systems.*

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### The Core Analogy: Filing Cabinet with Hash

Imagine a filing cabinet with n drawers (buckets). To file document with key k:
1. Compute hash(k) mod n → drawer number
2. Go to that drawer
3. If occupied, resolve collision (store in list at drawer, or probe next drawer)
4. File document

Lookup is similar: hash(k) → drawer → find document.

If hash function spreads keys evenly, each drawer has ~1 document → O(1) lookup.

### 🖼 Visualizing Hash Table with Chaining

```
Hash Table (size 4):
Bucket 0: [key=8, value=v8] → [key=12, value=v12]  (collisions: 8, 12)
Bucket 1: [key=5, value=v5]
Bucket 2: (empty)
Bucket 3: [key=11, value=v11]

Hash function: h(k) = k mod 4

Insert (5, v5): h(5) = 5 mod 4 = 1 → Bucket 1
Insert (8, v8): h(8) = 8 mod 4 = 0 → Bucket 0
Insert (12, v12): h(12) = 12 mod 4 = 0 → Bucket 0 (collision!)
Insert (11, v11): h(11) = 11 mod 4 = 3 → Bucket 3

Lookup 12: h(12) = 0 → Bucket 0 → Scan list → Find [key=12]
```

### Hash Function Quality

A good hash function:
1. **Deterministic:** Same input always produces same hash
2. **Fast:** Computation in O(1) time
3. **Uniform:** Distributes keys evenly across buckets (minimize collisions)
4. **Avalanche Effect:** Small change in input → large change in output (avoid patterns)

**Bad hash function example:**
```csharp
// BAD: h(k) = k mod 10
// Keys: 1, 11, 21, 31 all hash to same bucket (1)
// No distribution!
```

**Good hash function example:**
```csharp
// GOOD: h(k) = (k * 2654435761) >> 32  (MurmurHash variant)
// Spreads keys well even with patterns
```

### 🖼 Visualizing Load Factor & Resizing

```
Load factor α = n / m (n = keys, m = buckets)

α = 0.25: [empty, full, empty, empty, full, ...]
          Average chain length: 0.25 (fast)

α = 0.75: [full, full, full, full, full, ...]
          Average chain length: 0.75 (slow)

α > 1.0: Average chain length > 1 (too many collisions)
         Time to resize and rehash

Resizing strategy:
When α > threshold (e.g., 0.75):
1. Create new table with double size
2. Rehash all keys: h_new(k) = h(k) mod (2m)
3. Cost: O(n) for all rehashes, O(1) amortized per insertion
```

---

## ⚙️ CHAPTER 3: MECHANICS & IMPLEMENTATION

### 🔧 Operation 1: Hash Table with Chaining

```csharp
public class HashTableChaining<K, V> where K : notnull {
    private List<(K, V)>[] buckets;
    private int count = 0;
    private const float LoadFactorThreshold = 0.75f;
    
    public HashTableChaining(int capacity = 16) {
        buckets = new List<(K, V)>[capacity];
        for (int i = 0; i < capacity; i++) {
            buckets[i] = new List<(K, V)>();
        }
    }
    
    private int Hash(K key) {
        return Math.Abs(key.GetHashCode()) % buckets.Length;
    }
    
    public void Insert(K key, V value) {
        int bucketIndex = Hash(key);
        var bucket = buckets[bucketIndex];
        
        // Remove old value if exists
        for (int i = 0; i < bucket.Count; i++) {
            if (bucket[i].Item1.Equals(key)) {
                bucket[i] = (key, value);
                return;
            }
        }
        
        // Add new value
        bucket.Add((key, value));
        count++;
        
        // Check load factor and resize if needed
        if ((float)count / buckets.Length > LoadFactorThreshold) {
            Resize();
        }
    }
    
    public bool TryGetValue(K key, out V value) {
        int bucketIndex = Hash(key);
        var bucket = buckets[bucketIndex];
        
        foreach (var (k, v) in bucket) {
            if (k.Equals(key)) {
                value = v;
                return true;
            }
        }
        
        value = default!;
        return false;
    }
    
    private void Resize() {
        var oldBuckets = buckets;
        buckets = new List<(K, V)>[oldBuckets.Length * 2];
        for (int i = 0; i < buckets.Length; i++) {
            buckets[i] = new List<(K, V)>();
        }
        
        count = 0;
        foreach (var bucket in oldBuckets) {
            foreach (var (k, v) in bucket) {
                Insert(k, v);
            }
        }
    }
}
```

### 🔧 Operation 2: Hash Table with Open Addressing

```csharp
public class HashTableOpenAddressing<K, V> where K : notnull {
    private (K key, V value, bool occupied)[] table;
    private int count = 0;
    private const float LoadFactorThreshold = 0.5f;  // Lower threshold for open addressing
    
    public HashTableOpenAddressing(int capacity = 16) {
        table = new (K, V, bool)[capacity];
    }
    
    private int Hash(K key) {
        return Math.Abs(key.GetHashCode()) % table.Length;
    }
    
    public void Insert(K key, V value) {
        int h = Hash(key);
        
        // Linear probing: find next available slot
        for (int i = 0; i < table.Length; i++) {
            int index = (h + i) % table.Length;
            
            if (!table[index].occupied || table[index].key.Equals(key)) {
                table[index] = (key, value, true);
                if (!table[index].occupied) count++;
                
                if ((float)count / table.Length > LoadFactorThreshold) {
                    Resize();
                }
                return;
            }
        }
        
        throw new InvalidOperationException("Table full");
    }
    
    public bool TryGetValue(K key, out V value) {
        int h = Hash(key);
        
        for (int i = 0; i < table.Length; i++) {
            int index = (h + i) % table.Length;
            
            if (!table[index].occupied) {
                value = default!;
                return false;  // Key not found
            }
            
            if (table[index].key.Equals(key)) {
                value = table[index].value;
                return true;
            }
        }
        
        value = default!;
        return false;
    }
    
    private void Resize() {
        var oldTable = table;
        table = new (K, V, bool)[oldTable.Length * 2];
        count = 0;
        
        foreach (var (k, v, occupied) in oldTable) {
            if (occupied) {
                Insert(k, v);
            }
        }
    }
}
```

---

## ⚖️ CHAPTER 4: PERFORMANCE, TRADE-OFFS & REAL SYSTEMS

### Collision Resolution Comparison

| Strategy | Pros | Cons | Use Case |
|----------|------|------|----------|
| **Chaining** | Simple, flexible load factor | Cache-unfriendly (linked lists) | Hash maps in Java, Python |
| **Linear Probing** | Cache-friendly, simple | Clustering, poor worst-case | Hash tables in C++ (Google) |
| **Quadratic Probing** | Reduces clustering | More complex, poor cache | Academic, less practical |
| **Double Hashing** | Avoids clustering well | Complex, two hash functions | Performance-critical systems |

### Real Systems: Where Hashing Appears

> **🏭 Real-World Systems Story 1: Database Indexing & Query Processing**

SQL databases use hash tables:
- Hash index: Direct lookup by key, O(1) average
- Hash join: Join two tables by hashing on join key
- Aggregation (GROUP BY): Hash key frequencies

Example: "SELECT COUNT(*) FROM users GROUP BY country"
→ Hash users by country, count keys → O(n) instead of O(n log n) sorting

> **🏭 Real-World Systems Story 2: Caching (CDN, CPU, Memory)**

Caches use hashing to map objects to cache lines:
- Hash object key → cache line
- L1/L2 CPU caches: Hardware hashing for fast lookup
- CDN caches: Hash URL → cache server in distributed network

> **🏭 Real-World Systems Story 3: Bloom Filters (Space-Efficient Sets)**

Bloom filters use multiple hash functions to test set membership:
- Advantages: O(1) lookup, O(1) space per element (vs O(element_size))
- Disadvantage: False positives (may say "yes" when actually "no")
- Use: Spellcheck, duplicate detection, IP blacklists

---

## 🔗 CHAPTER 5: INTEGRATION & MASTERY

### Connections to the Learning Arc

**Building on Week 2:**
- **Arrays:** Hash tables are arrays with hash indexing
- **Linked Lists:** Chaining uses linked lists for collision resolution

**Building on Week 3 Days 1–2:**
- **Sorting vs Hashing:** Both organize data; sorting enables O(log n) binary search; hashing enables O(1) average lookup
- **Space-Time Trade-off:** Hashing trades space (load factor, probing) for time (O(1) lookup)

### Pattern Recognition: Randomization for Efficiency

**Pattern 1: Hash Function as Randomizer**
- Hash function acts as randomizer: spreads arbitrary keys uniformly
- Enables average-case O(1) despite worst-case O(n) (pathological keys)

**Pattern 2: Dynamic Resizing**
- Similar to dynamic arrays: when load factor exceeds threshold, double size
- O(n) rehash cost amortized to O(1) per insertion

---

## ⚔️ SUPPLEMENTARY OUTCOMES

### 🏋️ Practice Problems

| Problem | Difficulty | Key Concept |
|---------|----------|-------------|
| Implement hash table with chaining | 🟢 | Basic hash operations |
| Implement hash table with open addressing | 🟡 | Collision resolution |
| Two sum problem (hash table) | 🟢 | Hash for O(n) lookup |
| Group anagrams (hash function on sorted) | 🟡 | Hash key design |
| LRU cache (hash + doubly linked) | 🟠 | Hybrid structure |
| Bloom filter implementation | 🟠 | Multiple hash functions |

### 🎙️ Interview Questions

1. **Q:** Implement a hash table with chaining. Why resize when load factor exceeds 0.75?

2. **Q:** Compare chaining vs open addressing. When is each better?

3. **Q:** Design a hash function for strings. How do you avoid collisions?

4. **Q:** Solve "Two Sum" using a hash table. Why is it better than sorting?

5. **Q:** Implement a Bloom filter with k hash functions.

### ❌ Common Misconceptions

- **Myth:** Hash tables are always O(1).  
  **Reality:** O(1) is average case. Worst case is O(n) (all keys hash to same bucket).

- **Myth:** Collision are rare in practice.  
  **Reality:** Birthday paradox: with n keys and m buckets, collisions expected when n ≈ √m.

- **Myth:** Any hash function works.  
  **Reality:** Bad hash function (poor distribution) causes clustering and O(n) operations.

### 🚀 Advanced Concepts

- **Consistent Hashing:** For distributed systems; handles node failures
- **Universal Hashing:** Theoretical guarantees on collision probability
- **Cuckoo Hashing:** O(1) worst-case lookup (vs average case)
- **Perfect Hashing:** Zero collisions (for static sets)

### 📚 External Resources

- **CLRS Chapter 11:** Hash tables, collision resolution
- **MIT 6.006 Lecture 7–8:** Hashing and load balancing
- **LeetCode Hash Table Problems:** Easy to hard progression

---


---

# 📘 Week 03 Day 04: String Matching & Patterns — ENGINEERING GUIDE

**Metadata:**
- **Week:** 3 | **Day:** 4
- **Category:** Algorithms / String Algorithms
- **Difficulty:** 🟡 Intermediate (builds on sorting, hashing)
- **Real-World Impact:** String matching is fundamental: text search, DNA sequencing, plagiarism detection, autocomplete. Naive approaches are O(nm); clever algorithms (KMP, Boyer-Moore) are O(n+m). Understanding these teaches pattern matching which extends to regex, parsing, and data compression.
- **Prerequisites:** Week 2 (arrays), Week 3 Days 1–3 (sorting, heaps, hashing)
- **MIT Alignment:** String matching from MIT 6.006 Lecture 11–12

---

## 🎯 LEARNING OBJECTIVES

*By the end of this chapter, you will be able to:*

- 🎯 **Internalize** why naive string matching is slow and how to optimize.
- ⚙️ **Implement** KMP algorithm using failure function.
- ⚖️ **Evaluate** trade-offs between naive, KMP, and hashing-based matching.
- 🏭 **Connect** pattern matching to DNA sequencing and plagiarism detection.

---

## 📖 CHAPTER 1: CONTEXT & MOTIVATION

### The Problem: Find Pattern in Text

Given text T and pattern P, find all occurrences of P in T.

**Naive Algorithm:**
```csharp
int NaiveSearch(string text, string pattern) {
    int count = 0;
    for (int i = 0; i <= text.Length - pattern.Length; i++) {
        bool match = true;
        for (int j = 0; j < pattern.Length; j++) {
            if (text[i + j] != pattern[j]) {
                match = false;
                break;
            }
        }
        if (match) count++;
    }
    return count;
}
// Time: O((n - m + 1) × m) = O(nm)
// For n=1M, m=1K: 1 billion comparisons
```

**Smart Algorithm (KMP):**
Exploit pattern structure to avoid redundant comparisons.
Time: O(n + m) instead of O(nm).

---

## 🧠 CHAPTER 2: BUILDING THE MENTAL MODEL

### The Core Insight: Failure Function

When a mismatch occurs at position j in pattern, where should we resume?

```
Text:    a b a c a b a b c
Pattern: a b a b

Position 0: a b a b ... (match first 3, mismatch at 'a' vs 'b')
           a b a b (what if we shift?)

Naive: Shift by 1 (O(n²) in worst case)
Smart: Use "failure function" to skip directly to next promising position
```

The failure function encodes: "If pattern fails at position j, jump to position k (determined by pattern structure alone)"

### 🖼 Visualizing KMP Failure Function

```
Pattern: a b a b
Failure:
Index 0: a → 0 (no proper prefix-suffix)
Index 1: ab → 0 (no proper prefix-suffix)
Index 2: aba → 1 (prefix "a" = suffix "a")
Index 3: abab → 2 (prefix "ab" = suffix "ab")

Failure function: [0, 0, 1, 2]

Meaning:
- If mismatch at index 0, no recovery possible
- If mismatch at index 1, no recovery possible
- If mismatch at index 2, jump to index 1
- If mismatch at index 3, jump to index 2
```

---

## ⚙️ CHAPTER 3: MECHANICS & IMPLEMENTATION

### 🔧 Operation 1: Build Failure Function

```csharp
public static int[] BuildFailureFunction(string pattern) {
    int m = pattern.Length;
    int[] failure = new int[m];
    failure[0] = 0;
    
    int j = 0;
    for (int i = 1; i < m; i++) {
        // Find longest proper prefix-suffix at pattern[0..i-1]
        while (j > 0 && pattern[i] != pattern[j]) {
            j = failure[j - 1];  // Use failure function recursively
        }
        
        if (pattern[i] == pattern[j]) {
            j++;
        }
        
        failure[i] = j;
    }
    
    return failure;
}

// Trace for "abab":
// i=1: pattern[1]='b' != pattern[0]='a', failure[1]=0
// i=2: pattern[2]='a' == pattern[0]='a', j=1, failure[2]=1
// i=3: pattern[3]='b' == pattern[1]='b', j=2, failure[3]=2
// Result: [0, 0, 1, 2]
```

### 🔧 Operation 2: KMP Search

```csharp
public static int KMPSearch(string text, string pattern) {
    int n = text.Length, m = pattern.Length;
    int[] failure = BuildFailureFunction(pattern);
    int count = 0;
    
    int j = 0;
    for (int i = 0; i < n; i++) {
        while (j > 0 && text[i] != pattern[j]) {
            j = failure[j - 1];  // Jump using failure function
        }
        
        if (text[i] == pattern[j]) {
            j++;
        }
        
        if (j == m) {
            count++;  // Found a match
            j = failure[j - 1];  // Find next occurrence
        }
    }
    
    return count;
}

// Time: O(n + m)
// Proof: i increments n times, j increments at most m times total
```

---

## ⚖️ CHAPTER 4: PERFORMANCE & TRADE-OFFS

| Algorithm | Time | Space | Notes |
|-----------|------|-------|-------|
| **Naive** | O(nm) | O(1) | Simple, but slow for large m |
| **KMP** | O(n+m) | O(m) | Optimal for single pattern |
| **Boyer-Moore** | O(n/m) best, O(nm) worst | O(m+σ) | Faster in practice (larger jumps) |
| **Aho-Corasick** | O(n+m+z) | O(m×σ) | Multiple patterns simultaneously |
| **Rabin-Karp** | O(n+m) avg, O(nm) worst | O(1) | Hash-based, good for multiple patterns |

**When to use each:**
- **Naive:** Small patterns, simple strings (like built-in string.Contains)
- **KMP:** Theoretical interest, known pattern
- **Boyer-Moore:** Practical for large patterns, text search
- **Aho-Corasick:** Multiple pattern matching (virus scanning, filter lists)
- **Rabin-Karp:** Multiple patterns, rolling hash for efficiency

---

### Real Systems: Where String Matching Appears

> **🏭 Real-World Systems Story 1: DNA Sequencing**

Bioinformatics searches for patterns in DNA:
- DNA has ~3 billion base pairs (ACGT)
- Search for disease-causing mutations (patterns)
- KMP/Boyer-Moore enables efficient search in this massive dataset

> **🏭 Real-World Systems Story 2: Plagiarism Detection**

Plagiarism detectors use rolling hashes (Rabin-Karp):
- Hash every substring of size k in both documents
- Compare hashes: O(n + m) with Rabin-Karp
- Find similar passages efficiently

---

## ⚔️ SUPPLEMENTARY OUTCOMES

### 🏋️ Practice Problems

| Problem | Difficulty | Key Concept |
|---------|----------|-------------|
| Implement KMP algorithm | 🟡 | Failure function, linear time |
| Find all occurrences of pattern | 🟡 | KMP search |
| Repeat pattern detection | 🟠 | Pattern analysis |
| Shortest palindrome (KMP variant) | 🟠 | KMP with reversed pattern |

### 🎙️ Interview Questions

1. **Q:** Explain KMP algorithm. Why does the failure function work?

2. **Q:** Find all occurrences of pattern in text in O(n+m) time.

3. **Q:** How does Rabin-Karp use rolling hash for string matching?

### 🚀 Advanced Concepts

- **Boyer-Moore:** Right-to-left matching, skip tables
- **Aho-Corasick:** Trie-based multi-pattern matching
- **Suffix Array:** Preprocess text for pattern queries
- **Suffix Tree:** Generalize suffix array for other string problems

---


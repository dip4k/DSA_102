# 📘 Week 03 Support: LeetCode Problems & Interview Mastery

**Week:** 3 | **Category:** Practice & Interview Preparation  
**Purpose:** Master Week 03 concepts through curated LeetCode problems  
**Audience:** Competitive programmers, interview candidates

---

## 🎯 SORTING PROBLEMS

### Easy (Warmup)

| Problem | Difficulty | Key Insight | Approach |
|---------|-----------|-------------|----------|
| **Merge Sorted Array** (LeetCode 88) | Easy | Two pointers from end | Merge backwards to avoid shifting |
| **Valid Anagrams** (LeetCode 242) | Easy | Sort both strings | Compare sorted versions |
| **Contains Duplicate** (LeetCode 217) | Easy | Hash set | O(n) check, O(n) space |
| **Majority Element** (LeetCode 169) | Easy | Sort or hash | Sort + find middle element |

**Solve Order:** 88 → 242 → 217 → 169 (Reinforces merge, then introduces variants)

### Medium (Core)

| Problem | Difficulty | Key Insight | Approach |
|---------|-----------|-------------|----------|
| **Sort Colors** (LeetCode 75) | Medium | Partition not sort | Three-way partition (0, 1, 2) |
| **Kth Largest Element** (LeetCode 215) | Medium | Partial sorting | Min-heap of size k |
| **Top K Frequent Elements** (LeetCode 347) | Medium | Heap + hash | Hash count, heap for top k |
| **Merge k Sorted Lists** (LeetCode 23) | Medium | Min-heap merge | K-way merge via heap |
| **Largest Number** (LeetCode 179) | Medium | Custom comparator | Sort by concatenation order |

**Solve Order:** 75 → 215 → 347 → 23 → 179 (Partition → heaps → sorting tricks)

### Hard (Mastery)

| Problem | Difficulty | Key Insight | Approach |
|---------|-----------|-------------|----------|
| **Median of Two Sorted Arrays** (LeetCode 4) | Hard | Binary search on split | Find partition minimizing difference |
| **Sort Matrix** (LeetCode 378) | Hard | Binary search + count | Count elements ≤ x, binary search on answer |
| **Minimum Cost to Connect Ropes** (LeetCode 1167) | Hard | Greedy + heap | Always connect two smallest (Huffman) |

---

## 🎯 HEAP PROBLEMS

### Easy (Warmup)

| Problem | Difficulty | Key Insight | Approach |
|---------|-----------|-------------|----------|
| **Last Stone Weight** (LeetCode 1046) | Easy | Max heap | Always process two heaviest |
| **Kth Largest Element in Array** (LeetCode 215) | Easy | Min-heap | Keep k largest in heap |
| **Find Kth Largest** (Variation) | Easy | Heap basics | Extract max k times |

### Medium (Core)

| Problem | Difficulty | Key Insight | Approach |
|---------|-----------|-------------|----------|
| **Top K Frequent Words** (LeetCode 692) | Medium | Heap + hash + custom sort | Frequency + lexicographic ordering |
| **Reorganize String** (LeetCode 767) | Medium | Greedy + max heap | Always pick highest frequency char |
| **IPO** (LeetCode 502) | Medium | Greedy + heap | Process projects in profit order |

### Hard (Mastery)

| Problem | Difficulty | Key Insight | Approach |
|---------|-----------|-------------|----------|
| **Sliding Window Median** (LeetCode 480) | Hard | Heap management | Two heaps (max + min) with rebalancing |
| **Smallest Range in K Lists** (LeetCode 632) | Hard | Heap + pointers | Track min of k lists, move smallest pointer |

---

## 🎯 HASH TABLE PROBLEMS

### Easy (Warmup)

| Problem | Difficulty | Key Insight | Approach |
|---------|-----------|-------------|----------|
| **Two Sum** (LeetCode 1) | Easy | Hash for O(n) | Store seen values, check complement |
| **Contains Duplicate** (LeetCode 217) | Easy | Hash set | Linear scan with set |
| **Valid Anagram** (LeetCode 242) | Easy | Hash count | Count character frequencies |
| **Majority Element** (LeetCode 169) | Easy | Hash count | Count, find > n/2 |

**Solve Order:** 1 → 217 → 242 → 169 (Complement → duplicate → frequency)

### Medium (Core)

| Problem | Difficulty | Key Insight | Approach |
|---------|-----------|-------------|----------|
| **Group Anagrams** (LeetCode 49) | Medium | Hash key design | Sort string as key, group by key |
| **Word Pattern** (LeetCode 290) | Medium | Bijection | Map pattern char ↔ word, verify |
| **LRU Cache** (LeetCode 146) | Medium | Hash + doubly linked list | O(1) get/put with ordering |
| **Intersection of Arrays** (LeetCode 349) | Medium | Set operations | Hash set intersection |
| **Happy Number** (LeetCode 202) | Medium | Cycle detection | Track seen sums via hash set |

### Hard (Mastery)

| Problem | Difficulty | Key Insight | Approach |
|---------|-----------|-------------|----------|
| **LFU Cache** (LeetCode 460) | Hard | Hash + frequency tracking | Hash + counters + doubly linked lists |
| **Design Search Autocomplete** (LeetCode 1268) | Hard | Trie + hash | Or: hash with prefix keys |

---

## 🎯 STRING MATCHING / ROLLING HASH PROBLEMS

### Easy (Warmup)

| Problem | Difficulty | Key Insight | Approach |
|---------|-----------|-------------|----------|
| **Implement strStr** (LeetCode 28) | Easy | Naive or rolling hash | Naive: O(nm), Rabin-Karp: O(n+m) |
| **Repeated Substring Pattern** (LeetCode 459) | Easy | String property | Check if s == (s+s)[1:-1] or use rolling hash |

### Medium (Core)

| Problem | Difficulty | Key Insight | Approach |
|---------|-----------|-------------|----------|
| **Longest Substring Without Repeat** (LeetCode 3) | Medium | Sliding window + hash | Track char positions, slide left |
| **Minimum Window Substring** (LeetCode 76) | Medium | Sliding window + hash | Track frequency of needed chars |

### Hard (Mastery)

| Problem | Difficulty | Key Insight | Approach |
|---------|-----------|-------------|----------|
| **Palindrome Index** (HackerRank variant) | Hard | Rolling hash for efficiency | Check if palindrome after removing one char |

---

## 📈 PROGRESSION STRATEGY

### Week 03 Learning Path (2 weeks to mastery)

**Week 1: Sorting + Heaps**
```
Day 1: Merge Sorted Array (88) + Valid Anagrams (242)
Day 2: Sort Colors (75) + Kth Largest (215)
Day 3: Top K Frequent (347) + Merge k Lists (23)
Day 4: Largest Number (179) + Median Two Arrays (4)
Day 5: Last Stone (1046) + Reorganize String (767)
```

**Week 2: Hash Tables + Integration**
```
Day 1: Two Sum (1) + Contains Duplicate (217)
Day 2: Group Anagrams (49) + Word Pattern (290)
Day 3: LRU Cache (146) + Intersection (349)
Day 4: Implement strStr (28) + Longest Substring (3)
Day 5: Minimum Window (76) + LFU Cache (460)
```

---

## 🎯 COMMON MISTAKES & HOW TO AVOID THEM

### Mistake 1: Off-By-One Errors

**Problem:** Sorting boundary conditions

```csharp
// BAD: Sorts up to n-2
for (int i = 0; i < arr.Length - 1; i++) {
    if (arr[i] > arr[i+1]) {
        Swap(arr, i, i+1);
    }
}
// Last element never compared!

// CORRECT: Use <= for inclusive
for (int i = 0; i <= arr.Length - 1; i++) { ... }
// Or better: i < arr.Length
```

### Mistake 2: Wrong Comparison for Stability

```csharp
// BAD: Uses > instead of >=
if (arr[i] > arr[j]) { take_from_left; }
// Loses stability for equal elements

// CORRECT: Use <= for stable merge
if (arr[i] <= arr[j]) { take_from_left; }
```

### Mistake 3: Hash Collisions Not Verified

```csharp
// BAD: Accepts hash match without string verification
if (patternHash == textHash) return true;

// CORRECT: Verify actual string
if (patternHash == textHash && 
    text.Substring(i, m) == pattern) return true;
```

### Mistake 4: Load Factor Mismanagement

```csharp
// BAD: Never resize, load factor grows unbounded
public void Insert(K key, V value) {
    // ... insert logic, never resize
    // After many insertions, α > 2.0 → O(n) search!
}

// CORRECT: Resize when α exceeds threshold
if ((float)count / buckets.Length > LoadFactorThreshold) {
    Resize();  // Double buckets, rehash everything
}
```

### Mistake 5: Integer Overflow in Rolling Hash

```csharp
// BAD: Overflow possible
long hash = text[i] * basePower;

// CORRECT: Modulo at each step
long hash = (text[i] * basePower) % PRIME;
```

### Mistake 6: Forgetting Edge Cases

```csharp
// BAD: No handling for edge cases
public List<int> FindPattern(string text, string pattern) {
    // ... main logic ...
    // What if pattern.Length > text.Length?
    // What if pattern is empty?
}

// CORRECT: Check boundaries first
if (pattern.Length > text.Length || pattern.Length == 0) 
    return new List<int>();

// Then proceed with main logic
```

---

## 📊 INTERVIEW CHEAT SHEET

### Questions You'll Get Asked

1. **"Implement a hash table from scratch."**
   - → Separate chaining OR open addressing
   - → Handle resizing, load factor
   - → Consider collision resolution

2. **"Compare merge sort vs quick sort."**
   - → Merge: O(n log n) guaranteed, stable, O(n) space
   - → Quick: O(n log n) average, in-place, risky O(n²)
   - → Quick is usually faster due to constants

3. **"Explain heap sort. Why isn't it used more?"**
   - → O(n log n) guaranteed, in-place
   - → Poor cache behavior (random jumps)
   - → Constants are higher than quick sort
   - → Used mainly for theoretical interest

4. **"Design a system to detect plagiarism in 1 billion documents."**
   - → Rolling hash (Rabin-Karp)
   - → Index all documents in O(total_size)
   - → Query new document in O(doc_size)
   - → Much faster than naive O(doc_size × billion)

5. **"How do you choose which sorting algorithm?"**
   - → Is data already sorted? → Insertion sort
   - → Do you need stability? → Merge sort
   - → Do you need O(n log n) guarantee? → Merge or Heap
   - → Is speed priority? → Quick sort
   - → Are keys integers? → Counting or Radix

---

**Document Status:** ✅ COMPLETE  
**LeetCode Problems:** 30+ with strategies  
**Interview Questions:** 5+ detailed guides


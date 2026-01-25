# 🎙 Week 03 Interview Q&A Reference — Sorting & Hashing

**Purpose:** A master list of conceptual and mechanical questions to test your depth.  
**Usage:** Cover the answer column and try to explain the **mechanics** and **trade-offs** out loud.

---

## 🟦 Day 1: Elementary Sorts (Bubble, Selection, Insertion)

### ❓ Q1: Why is Insertion Sort often preferred over Bubble or Selection Sort for small arrays?
- **Answer:** Insertion Sort is **adaptive** and **stable**. It performs very well ($O(n)$) on nearly sorted data and has extremely low overhead (simple logic, cache-friendly for small $n$).
- 🔀 **Follow-up:** Why do hybrid algorithms like Timsort use Insertion Sort for small blocks (e.g., size < 32)?  
  *(Answer: For small $n$, the constant factors of $O(n^2)$ insertion sort are smaller than the recursion/allocation overhead of Merge/Quick sort.)*

### ❓ Q2: Can Selection Sort ever be stable?
- **Answer:** Standard Selection Sort is **not stable** because swapping an element from the unsorted part to its correct position can jump over equal elements. It can be implemented stably using linked lists or shifting instead of swapping, but that ruins its $O(n)$ write property.
- 🔀 **Follow-up:** When is minimizing writes (Selection Sort's main feature) actually useful?  
  *(Answer: On EEPROM/Flash memory where write cycles are limited or expensive.)*

---

## 🟦 Day 2: Merge Sort & Quick Sort

### ❓ Q3: Merge Sort guarantees $O(n \log n)$, but Quick Sort is often faster in practice. Why?
- **Answer:** Quick Sort is **in-place** (cache-friendly, no auxiliary array) and has smaller constant factors in its inner loop. Merge Sort requires $O(n)$ extra space and involves copying data back and forth, which strains memory bandwidth.
- 🔀 **Follow-up:** When would you absolutely prefer Merge Sort over Quick Sort?  
  *(Answer: Linked lists (no random access needed), external sorting (disk I/O), or when stability is required.)*

### ❓ Q4: What triggers the $O(n^2)$ worst-case in Quick Sort? How do we prevent it?
- **Answer:** It happens when the **pivot** consistently splits the array into size $1$ and size $n-1$ (e.g., sorted array with first/last element pivot).
- 🔀 **Follow-up:** How does "Median-of-Three" or "Randomized Pivot" solve this?  
  *(Answer: They make the probability of picking a bad pivot astronomically low, restoring expected $O(n \log n)$.)*

### ❓ Q5: Explain the space complexity of Quick Sort. Is it $O(1)$?
- **Answer:** No, it is $O(\log n)$ due to the **recursion stack**. In the worst case (unbalanced partitions), it can degrade to $O(n)$ stack depth.
- 🔀 **Follow-up:** How does **tail call optimization** or sorting the smaller partition first help?  
  *(Answer: By recurring on the smaller partition first and looping for the larger, we guarantee $O(\log n)$ stack depth.)*

---

## 🟦 Day 3: Heap Sort & Priority Queues

### ❓ Q6: Why is Heap Sort not the default general-purpose sorting algorithm (like Quick Sort)?
- **Answer:** Heap Sort is **not stable** and has poor **cache locality**. The parent-child jumps (index $i$ to $2i$) scatter memory accesses across the array, causing more cache misses than Quick Sort's linear scanning.
- 🔀 **Follow-up:** What is the specific advantage of Heap Sort then?  
  *(Answer: Guaranteed $O(n \log n)$ time with $O(1)$ auxiliary space. Useful in embedded systems where memory is tight and worst-case guarantees are needed.)*

### ❓ Q7: How does building a heap take $O(n)$ time, not $O(n \log n)$?
- **Answer:** Using `heapify` (sift-down) from the bottom up: the vast majority of nodes are leaves (0 swaps) or near leaves (few swaps). The series sums to linear time. Sifting up (insertion style) would indeed be $O(n \log n)$.

---

## 🟦 Day 4: Hash Tables I (Separate Chaining)

### ❓ Q8: How does the load factor ($\alpha$) affect a hash table using separate chaining?
- **Answer:** $\alpha = n/m$. Average lookup is $O(1 + \alpha)$. If $\alpha$ grows too large (e.g., > 5-10), buckets become long lists, degrading performance to $O(n)$.
- 🔀 **Follow-up:** What happens during a resize/rehash operation?  
  *(Answer: Create a new array (usually $2x$ size), and re-insert every element. This is expensive ($O(n)$) but amortized.)*

### ❓ Q9: Why must a Hash Table key be immutable (or at least have a constant hash)?
- **Answer:** If the key changes after insertion, its hash value changes. The table won't find it in the original bucket, effectively "losing" the data and causing memory leaks or logic errors.

---

## 🟦 Day 5: Hashing II (Open Addressing)

### ❓ Q10: Why do we need "tombstones" in Open Addressing deletions?
- **Answer:** If we simply delete a key (make the slot "Empty"), probing sequences for keys inserted *after* it (that collided and moved past) will break. The search will stop at the empty slot and return "Not Found" incorrectly. A tombstone says "keep looking."
- 🔀 **Follow-up:** What is the downside of tombstones?  
  *(Answer: They count as "filled" for load factor but hold no data, cluttering the table. Requires periodic cleanup/rehashing.)*

### ❓ Q11: Explain "Primary Clustering" in Linear Probing.
- **Answer:** Occupied slots tend to build continuous runs (clusters). Any new key hashing into a cluster extends it. Larger clusters grow faster, degrading performance from $O(1)$ to $O(n)$.
- 🔀 **Follow-up:** How does Double Hashing fix this?  
  *(Answer: It uses a second hash function for the step size, scattering keys that collide at the same primary index.)*

### ❓ Q12: Why would a real-time system prefer Cuckoo Hashing over standard Linear Probing?
- **Answer:** Cuckoo Hashing guarantees **$O(1)$ worst-case lookup** (checking only 2 locations). Linear probing has $O(n)$ worst-case lookup if a cluster is long, which causes unpredictable latency spikes.

---

## 🧠 Behavioral & System Design Questions

### ❓ Q13: Design a "Top K" leaderboard for a video game.
- **Pattern:** Use a **Hash Map** (ID $\to$ Score) for fast updates and a **Min-Heap** of size $K$ (or Max-Heap) to track the leaders.
- **Trade-off:** If $K$ is small, Heap is great. If $K$ is huge, maybe QuickSelect or approximate counting.

### ❓ Q14: Which sorting algorithm does Python's `sort()` or Java's `Arrays.sort()` use?
- **Answer:** Python uses **Timsort** (hybrid Merge + Insertion). Java primitives use **Dual-Pivot Quicksort**; Objects use Timsort (stability required).
- **Why:** Timsort takes advantage of real-world data often having pre-sorted runs.

### ❓ Q15: How would you store a dictionary of 100 million static words for a spell checker?
- **Answer:** Since the data is **static**, use **Perfect Hashing** (no collisions, $O(1)$ lookup) or a **Bloom Filter** (space efficient) + Disk lookup. Standard hashing wastes space on pointers/empty slots.
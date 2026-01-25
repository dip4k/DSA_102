# ⚠️ Week 4.5 — Common Pitfalls & Misconceptions (Tier 1 Patterns) (COMPLETE)

**🗓️ Week:** 4.5  
**📌 Focus:** Hash Map, Monotonic Stack, Merge, Partition, Kadane's, Fast & Slow  
**🎯 Goal:** Avoid critical mistakes that fail 70%+ of candidates

---

## 🔴 HASH MAP / HASH SET PITFALLS

### ❌ Pitfall 1: Using Nested Loops for Two Sum

**Mistake:**
```
Problem: Two Sum [2,7,11,15], target 9
Candidate: "I'll check all pairs with nested loops."
```

**Why It Fails:**
- O(n²) time. For n=10,000: 100 million comparisons.
- Shows lack of Hash Map understanding (automatic red flag in interviews).

**✅ Correct:**
- Hash Map: O(n) time, O(n) space. For each num, check if `target - num` exists in map.

**Interview Reality:** 90% of interviewers will stop here if you suggest nested loops (shows you don't know patterns).

---

### ❌ Pitfall 2: Hash Map for Sorted Array (Wasting Space)

**Mistake:**
```
Problem: Two Sum II (sorted array)
Candidate: "I'll use Hash Map for O(n) time."
```

**Why It's Suboptimal:**
- Hash Map works but O(n) space.
- Two Pointers is O(1) space (better for sorted).

**✅ Correct:**
- **Sorted:** Two Pointers (O(n) time, O(1) space).
- **Unsorted:** Hash Map (O(n) time, O(n) space).

**Interview Signal:** Recognizing this trade-off = senior-level thinking.

---

### ❌ Pitfall 3: Using Mutable Objects as Hash Map Keys

**Mistake:**
```
Python:
key = [1, 2, 3]  # List (mutable)
map[key] = value  # ERROR: unhashable type
```

**Why It Fails:**
- Mutable objects can change, breaking hash invariant.
- If key changes after insertion, hash changes → can't find value.

**✅ Correct:**
- Use immutable keys: strings, integers, tuples (in Python).
- Arrays/lists can't be keys (convert to tuple if needed).

---

### ❌ Pitfall 4: Forgetting Load Factor & Rehashing

**Mistake:**
"Hash Map is always O(1), no exceptions."

**Reality:**
- **Average:** O(1).
- **Worst Case:** O(n) if all keys hash to same bucket.
- **Amortized:** O(1) per insertion despite occasional O(n) rehashing.

**Load Factor:**
- When `elements / buckets > 0.75`, rehash (double buckets).
- Amortized cost: O(1) per insertion.

---

## 🔴 MONOTONIC STACK PITFALLS

### ❌ Pitfall 5: Confusing Increasing vs Decreasing Stack

**Mistake:**
Problem: "Next Greater Element"  
Candidate uses **increasing** stack (wrong direction).

**Reality:**
- **Next Greater:** Use **decreasing** stack (pop smaller elements).
- **Next Smaller:** Use **increasing** stack (pop larger elements).

**Mnemonic:**
- Next **Greater** → Maintain **decreasing** order (opposite).
- Next **Smaller** → Maintain **increasing** order (opposite).

---

### ❌ Pitfall 6: Thinking Monotonic Stack is O(n²)

**Mistake:**
"Nested loop (iterate + stack operations) → O(n²)."

**Reality:**
- **Amortized O(n):** Each element pushed **once**, popped **once**.
- Total operations: 2n = O(n).

**Proof:**
- For n elements: n pushes + n pops (max) = 2n operations.

---

### ❌ Pitfall 7: Not Handling Circular Arrays

**Mistake:**
Problem: "Next Greater Element II (circular)"  
Candidate: Single pass, doesn't wrap around.

**✅ Correct:**
- Iterate **twice** through array (simulate circular).
- Use modulo: `arr[i % n]`.
- Stack maintains decreasing order across both passes.

---

## 🔴 MERGE OPERATIONS & INTERVALS PITFALLS

### ❌ Pitfall 8: Sequential Merge for K Lists

**Mistake:**
```
Merge K lists one-by-one:
result = merge(L1, L2)
result = merge(result, L3)
...
Total: O(N*K) where N = total elements
```

**Why It's Slow:**
- For K=1000, N=1M: 10⁹ operations.

**✅ Correct (Heap):**
- Min-heap of size K.
- Extract min, add its next node.
- **Total:** O(N log K) (10⁷ ops for K=1000, N=1M).
- **Speedup:** 100x!

---

### ❌ Pitfall 9: Not Sorting Intervals First

**Mistake:**
Problem: "Merge Intervals"  
Candidate: Compare all pairs to check overlap. O(n²).

**✅ Correct:**
- **Sort intervals by start time:** O(n log n).
- **Greedy merge:** Only consecutive can overlap after sorting. O(n).
- **Total:** O(n log n).

**Key Insight:** Sorting enables greedy approach.

---

### ❌ Pitfall 10: Forgetting Edge Cases in Merge

**Edge Cases:**
1. **Empty lists:** One or both lists empty.
2. **Different lengths:** One list exhausts before other.
3. **Duplicates:** How to handle?
4. **Single element:** List with 1 element.

**Common Mistake:** Not copying remaining elements after one list exhausts.

---

## 🔴 PARTITION & CYCLIC SORT PITFALLS

### ❌ Pitfall 11: Moving Mid After Swap with High (Dutch Flag)

**Mistake (Sort Colors):**
```
If arr[mid] == 2:
    swap(arr[mid], arr[high])
    mid += 1  # WRONG! Don't move mid yet.
    high -= 1
```

**Why It Fails:**
- After swapping with `high`, `arr[mid]` contains unprocessed element.
- Must check it in next iteration.

**✅ Correct:**
```
If arr[mid] == 2:
    swap(arr[mid], arr[high])
    high -= 1  # DON'T move mid
```

---

### ❌ Pitfall 12: Cyclic Sort on Non-Range Arrays

**Mistake:**
Applying Cyclic Sort to array **not** in range [1..n] or [0..n].

**Example:**
```
Array: [5, 2, 9, 1]  (contains 9, which is > n=4)
Cyclic Sort assumes arr[i] can be placed at index arr[i]-1.
```

**✅ Verification:**
- **Before using Cyclic Sort:** Confirm numbers are in [1..n] or [0..n].
- Otherwise, use different approach (Hash Set, sorting, etc.).

---

### ❌ Pitfall 13: Infinite Loop in Cyclic Sort

**Mistake:**
```
while arr[i] != i+1:
    swap(arr[i], arr[arr[i]-1])
    # If arr[i] == arr[arr[i]-1], infinite loop!
```

**Why It Happens:**
- Duplicate elements cause swapping between same values.

**✅ Correct:**
```
while arr[i] != i+1 AND arr[i] != arr[arr[i]-1]:
    swap(arr[i], arr[arr[i]-1])
```

---

## 🔴 KADANE'S ALGORITHM PITFALLS

### ❌ Pitfall 14: Initializing Max to 0 (All Negatives)

**Mistake:**
```
max_sum = 0  # WRONG if all numbers are negative
max_ending = 0

For each num:
    max_ending = max(0, max_ending + num)
    max_sum = max(max_sum, max_ending)
```

**Why It Fails:**
- If all negative, returns 0 (empty subarray), not max element.

**✅ Correct:**
```
max_sum = arr[0]  # Initialize to first element
max_ending = arr[0]

For i from 1 to n-1:
    max_ending = max(arr[i], max_ending + arr[i])
    max_sum = max(max_sum, max_ending)
```

---

### ❌ Pitfall 15: Forgetting Negatives in Max Product

**Mistake (Max Product Subarray):**
Only tracking `max_product`, not `min_product`.

**Why It Fails:**
- Negative number can flip min to max.
- Example: [-2, 3, -4]. At -4: min was -6, becomes max 24.

**✅ Correct:**
- Track **both** `max_product` and `min_product`.
- At each step:
  - `new_max = max(num, max_prod*num, min_prod*num)`
  - `new_min = min(num, max_prod*num, min_prod*num)`

---

### ❌ Pitfall 16: Confusing Kadane's with DP Memoization

**Mistake:**
"Kadane's is not DP because it doesn't use memoization."

**Reality:**
- Kadane's **IS DP:** State = `max_ending_here[i]`.
- **Space-optimized:** Only track previous state (O(1) space vs O(n) array).
- **Recurrence:** `max_ending[i] = max(arr[i], max_ending[i-1] + arr[i])`

---

## 🔴 FAST & SLOW POINTERS PITFALLS

### ❌ Pitfall 17: Not Checking fast.next (Crash)

**Mistake:**
```
while fast != null:
    slow = slow.next
    fast = fast.next.next  # CRASH if fast.next is null!
```

**Why It Fails:**
- Odd-length list: `fast.next` might be null before `fast`.

**✅ Correct:**
```
while fast != null AND fast.next != null:
    slow = slow.next
    fast = fast.next.next
```

---

### ❌ Pitfall 18: Thinking Fast & Slow is O(n²)

**Mistake:**
"Fast pointer moves 2x, so visits 2n nodes. Slow visits n. Total 3n → O(n²)?"

**Reality:**
- **O(n) time:** Each node visited at most twice (by fast and slow).
- Not nested loops (they move simultaneously).

---

### ❌ Pitfall 19: Forgetting to Reset Pointer for Cycle Start

**Mistake (Find Cycle Start):**
After detecting cycle at meeting point, **not resetting** one pointer to head.

**✅ Correct (Floyd's Extended):**
1. Detect cycle: slow and fast meet at M.
2. **Reset slow to head.**
3. Move both at **same speed** (1 step each).
4. They meet at cycle start.

---

### ❌ Pitfall 20: Using Fast & Slow on Array (Rare)

**Mistake:**
Trying to apply Fast & Slow to standard array iteration.

**Reality:**
- Fast & Slow designed for **linked structures** (linked list, sequences with "next" pointer).
- For arrays, usually Two Pointers or Sliding Window is more natural.

**Exception:** Problems where array elements act as indices (cyclic arrays, "jump game" variants).

---

## 🎯 GENERAL PITFALLS (ALL PATTERNS)

### ❌ Pitfall 21: Not Testing Edge Cases

**Common Edge Cases (Test ALL):**
- Empty input (n = 0)
- Single element (n = 1)
- All elements identical
- All negative numbers (Kadane's)
- Duplicates (if allowed)
- Maximum constraints (n = 10⁵, values = 10⁹)

**Interview Tip:** Verbally mention edge cases even if not coding them (shows thoroughness).

---

### ❌ Pitfall 22: Off-by-One Errors

**Common Mistakes:**
- `while i < n` vs `while i <= n`
- Array index: `arr[i]` when i could be n (out of bounds)
- Range: `[0, n)` vs `[0, n]`

**Best Practice:** Trace through with small example (n=3) before submitting.

---

### ❌ Pitfall 23: Integer Overflow

**Mistake:**
```
Kadane's: max_sum = max_sum + arr[i]
If max_sum is near INT_MAX, overflow possible.
```

**✅ Mitigation:**
- Use `long` (64-bit) for sums if needed.
- Or: Check overflow before operation.

---

### ❌ Pitfall 24: Pattern Misidentification

**Mistake:**
- Applying Monotonic Stack to a Two Pointers problem.
- Using Hash Map when Cyclic Sort is more efficient.

**✅ Decision Framework:**
- **Pairs:** Two Pointers (sorted) or Hash Map (unsorted).
- **Next Greater:** Monotonic Stack.
- **Missing in [1..n]:** Cyclic Sort.
- **Max subarray:** Kadane's.
- **Cycle (linked list):** Fast & Slow.

---

### ❌ Pitfall 25: Not Explaining Trade-offs

**Mistake:**
Jumping to solution without discussing alternatives.

**✅ Interview Best Practice:**
1. State brute force (O(n²) or worse).
2. Mention why it's slow.
3. Propose optimized approach (Hash Map, etc.).
4. Discuss trade-offs (space, time).
5. Confirm with interviewer before coding.

**Example:**
"Brute force is O(n²) nested loops. I can optimize to O(n) with Hash Map, trading O(n) space for speed. Is that acceptable?"

---

## 📝 PRE-SUBMISSION CHECKLIST

✅ **Complexity:** Time and space both analyzed?  
✅ **Edge Cases:** Tested empty, single, negatives, max size?  
✅ **Off-by-One:** Checked all boundaries?  
✅ **Null/Overflow:** Handled null pointers, integer overflow?  
✅ **Pattern Fit:** Is this the right pattern?  
✅ **Dry Run:** Traced through 2-3 examples?  
✅ **Trade-offs Discussed:** Mentioned alternatives?

---

## 🎯 PATTERN-SPECIFIC RED FLAGS

### Hash Map Red Flags:
- ❌ Nested loops for Two Sum
- ❌ Not discussing space trade-off
- ❌ Using mutable keys

### Monotonic Stack Red Flags:
- ❌ Wrong stack order (increasing vs decreasing)
- ❌ Thinking it's O(n²)

### Merge Red Flags:
- ❌ Sequential merge for K lists
- ❌ Not sorting intervals first

### Cyclic Sort Red Flags:
- ❌ Applying to non-range arrays
- ❌ Infinite loop on duplicates

### Kadane's Red Flags:
- ❌ Initializing max to 0 (all negatives fail)
- ❌ Not tracking min for product variant

### Fast & Slow Red Flags:
- ❌ Not checking `fast.next` (crash)
- ❌ Not resetting for cycle start

---

## 🚀 FINAL ADVICE

**The #1 Pitfall:** Not recognizing the pattern.

**Solution:**
1. Memorize **red flag keywords** for each pattern.
2. Practice **pattern recognition** (< 30 sec per problem).
3. Build **mental model** for each pattern (analogies help).

**Remember:** Interviews test **pattern recognition + trade-off discussion**, not just coding speed.

---

**Generated:** December 30, 2025  
**Version:** 9.0 (Week 4.5)  
**Status:** ✅ COMPLETE
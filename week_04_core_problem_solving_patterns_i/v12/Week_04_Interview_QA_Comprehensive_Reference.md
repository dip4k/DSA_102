# 🎙️ Week 04: Interview Q&A Reference (All Patterns)

**Format:** Questions organized by pattern; answers provided for learning  
**Use:** Interview prep, self-testing, deep concept review  
**Difficulty:** 🟡 Medium to 🔴 Hard

---

## TWO-POINTER PATTERN (6+ Questions)

### Q1: Merge Two Sorted Arrays (Easy)
**Interview Signal:** "Merge", "two arrays", "in-place"

**Problem:** Given two sorted arrays, merge them into one sorted array.

**Answer Framework:**
- **Insight:** Use two pointers, one for each array; advance based on comparison
- **Complexity:** O(n+m) time, O(1) space (or O(n+m) if output array counts)
- **Implementation:** Start from beginning; compare elements; advance smaller pointer
- **Edge Cases:** Empty arrays, arrays of different sizes, all elements equal

**Follow-up:** What if you need to merge in-place in the first array?
- **Answer:** Merge backward (from end) to avoid overwriting data

---

### Q2: Remove Duplicates In-Place (Easy)
**Interview Signal:** "Remove duplicates", "in-place", "sorted array"

**Problem:** Remove duplicates from a sorted array; modify in-place; return count of unique elements.

**Answer Framework:**
- **Insight:** Use two pointers; one marks position for unique elements, one scans
- **Complexity:** O(n) time, O(1) space
- **Implementation:** `slow` tracks position to insert next unique; `fast` scans ahead
- **Key:** Only advance `slow` when `arr[fast] != arr[fast-1]`

**Follow-up:** What if array is unsorted?
- **Answer:** Sort first O(n log n), then apply algorithm. Or use hash set O(n) space.

---

### Q3: Container With Most Water (Medium)
**Interview Signal:** "Two pointers", "maximize area", "two indices"

**Problem:** Given array of heights, find two indices where the container holds most water.

**Answer Framework:**
- **Insight:** Start with widest container; shrink from side with smaller height
- **Invariant:** If left and right container has area A, and we shrink the smaller side, we can't do worse (width decreases, but height might increase)
- **Complexity:** O(n) time, O(1) space
- **Proof:** Any container formed by shrinking this container will have smaller width; height can't compensate for both sides' width loss

**Follow-up:** Why do we shrink the smaller side?
- **Answer:** Shrinking the larger side guarantees area decreases (width goes down, and height can't increase beyond the smaller side's height).

---

### Q4: Two Sum in Sorted Array (Easy-Medium)
**Interview Signal:** "Two sum", "sorted", "find indices"

**Problem:** Find two numbers that add to target; return indices.

**Answer Framework:**
- **Insight:** Use opposite-direction pointers; move based on sum vs. target
- **Complexity:** O(n) time, O(1) space
- **Implementation:** `left` starts at 0, `right` at n-1; if sum < target, move left; if sum > target, move right
- **Works because:** Array is sorted; increasing left increases sum; increasing right decreases sum

**Follow-up:** What if array is unsorted?
- **Answer:** Hash map: O(n) time, O(n) space. Or sort then use two-pointer: O(n log n) time.

---

### Q5: Explain Two-Pointer Invariant (Hard)
**Interview Signal:** "Explain why", "invariant", "correctness"

**Problem:** Explain the concept of invariant in two-pointer pattern.

**Answer Framework:**
- **Definition:** A property that remains true throughout the algorithm
- **Example (merging):** "All elements before `left` in left array are processed"
- **Example (removal):** "All elements in [slow, fast) are elements to keep"
- **Why it matters:** Proves algorithm correctness (maintain invariant → correct result)
- **How to design:** Identify what must be true after each step; ensure operations preserve it

**Follow-up:** Design a two-pointer algorithm for a new problem; explain its invariant.

---

### Q6: When to Use Two-Pointer (Medium)
**Interview Signal:** "Design approach", "what technique", "complexity requirement"

**Problem:** When should you choose two-pointer over hash maps or sorting?

**Answer Framework:**
- **Two-Pointer Strengths:** O(1) space, O(n) time, works on sorted arrays
- **Two-Pointer Weakness:** Requires sorted input (usually)
- **Hash Map Comparison:** More flexible (works unsorted) but O(n) space
- **When to Choose:** If space is critical AND input is sorted → two-pointer; otherwise, hash map often cleaner
- **Production:** Two-pointer for merge operations (database, filesystems); hash map for general cases

---

## SLIDING WINDOW (FIXED SIZE) (6+ Questions)

### Q7: Moving Average (Easy)
**Interview Signal:** "Moving average", "k-length", "running metric"

**Problem:** Calculate the average of all subarrays of size k.

**Answer Framework:**
- **Naive:** O(n*k) — recalculate sum for each window
- **Optimized:** O(n) — maintain running sum; subtract element leaving, add element entering
- **Complexity:** O(n) time, O(1) space
- **Implementation:** Use a simple counter; add right, subtract left; divide by k

**Follow-up:** What if you need all moving averages stored?
- **Answer:** O(n) time (same), O(n) space for output array.

---

### Q8: Maximum in Sliding Window Using Deque (Hard)
**Interview Signal:** "Maximum in window", "deque", "monotonic"

**Problem:** Find maximum in every k-length subarray.

**Answer Framework:**
- **Naive:** O(n*k) — scan each window
- **Deque Approach:** O(n) — maintain monotonic decreasing deque
- **Key Insight:** Deque stores indices in decreasing order of values; remove front if outside window
- **Implementation:** 
  1. Remove front if index < window start
  2. While new element > back of deque, remove back
  3. Add new element to back
  4. Front is the maximum

**Correctness:** Deque is monotonic decreasing; we only care about max (front), not all elements.

**Follow-up:** Can you solve with heap?
- **Answer:** Yes, O(n log n) — insert all, remove if outside window. Worse than deque.

---

### Q9: Data Structure Trade-Offs (Medium)
**Interview Signal:** "Data structure", "trade-off", "when to use"

**Problem:** For sliding window max/min, compare deque vs. heap vs. balanced BST.

**Answer Framework:**

| Data Structure | Time | Space | Best For |
|---|---|---|---|
| Deque | O(n) | O(k) | Sliding window (optimal) |
| Heap | O(n log k) | O(k) | Priority (useful elsewhere) |
| BST | O(n log k) | O(k) | Generality (less optimized) |
| Array (na) | O(n*k) | O(1) | Teaching (not practical) |

- **Deque wins** because each element enters and exits once
- **Heap requires cleanup** (lazy deletion) making it O(n log n) in worst case
- **Choice:** Deque for sliding window; heap for other problems

**Follow-up:** Why can't we use a simple priority queue?
- **Answer:** Need to remove arbitrary elements (when they leave window), not just pop min. Deque allows this without heap restructuring.

---

### Q10: Amortized Analysis (Hard)
**Interview Signal:** "Why O(n)?", "amortized", "analysis"

**Problem:** Explain why deque-based sliding window is O(n) despite variable operations.

**Answer Framework:**
- **Key Insight:** Each element is added to deque once and removed once
- **Total operations:** 2n (add + remove for each element)
- **Operations per element:** 2 = O(1) amortized
- **Compare to naive:** Each window checks k elements = n*k operations

**Amortized Proof:** Potential function; banker's method; or aggregate analysis.

**Follow-up:** Give a scenario where a single iteration takes O(k) time.
- **Answer:** When removing many elements from deque (each smaller than new element).

---

## SLIDING WINDOW (VARIABLE SIZE) (6+ Questions)

### Q11: Longest Substring Without Repeating (Medium)
**Interview Signal:** "Longest substring", "no repeating", "window"

**Problem:** Find longest substring with no repeating characters.

**Answer Framework:**
- **Approach:** Use set or hash map to track characters in current window
- **Expand right** until you see a repeating character
- **Shrink left** until the character exits the window
- **Complexity:** O(n) time, O(k) space (k = character set size)
- **Why O(n):** Each character enters and exits window once

**Implementation Detail:** Use hash map to track last seen index; when repeat found, move left pointer to just after previous occurrence.

**Follow-up:** Handle Unicode characters.
- **Answer:** Use hash map instead of array. Space becomes O(k) where k = distinct characters.

---

### Q12: At Most K Distinct Characters (Medium)
**Interview Signal:** "At most K", "distinct", "dynamic window"

**Problem:** Find longest substring with at most K distinct characters.

**Answer Framework:**
- **Key:** "At most K" is a constraint triggering expand/shrink
- **Expand right** while distinct count ≤ K
- **Shrink left** when distinct count > K (until it goes back to ≤ K)
- **State Machine:** Two states: VALID (distinct ≤ K) and INVALID (distinct > K)
- **Complexity:** O(n) amortized; O(k) space

**Pattern Generalization:** Works for "at most K" of anything (occurrences, sum, etc.)

**Follow-up:** Modify for "exactly K distinct".
- **Answer:** Trick: answer = (≤ K) - (≤ K-1). Use two windows.

---

### Q13: Minimum Window Substring (Hard)
**Interview Signal:** "Minimum window", "contains", "all characters"

**Problem:** Find minimum window containing all characters from target string.

**Answer Framework:**
- **Two Maps:** Track target character counts and window character counts
- **Expand right** until window contains all target characters
- **Shrink left** to minimize length while maintaining containment
- **Complexity:** O(n) time, O(k) space (k = character set)
- **Correctness:** Once valid window found, shrink left doesn't lose solution (valid windows are nested)

**Key Difference from "at most K":** Expansion must include all characters; shrinking must maintain this.

**Follow-up:** Return the substring, not just length.
- **Answer:** Track left and right indices when valid window found; store best.

---

### Q14: State Machine Decision Logic (Hard)
**Interview Signal:** "When to expand", "when to shrink", "logic"

**Problem:** Explain the decision logic: when to expand right vs. shrink left.

**Answer Framework:**
1. **Check constraint:** Does current window [left, right) satisfy the constraint?
2. **If YES (valid state):** Try to expand right to potentially get a better answer
3. **If NO (invalid state):** Shrink left until valid again
4. **Algorithm:**
   ```
   while right < n:
       add arr[right] to window
       while (constraint_violated):
           remove arr[left] from window
           left += 1
       update answer with current window
       right += 1
   ```

**Why this works:** Each element enters and exits at most once, giving O(n) amortization.

**Follow-up:** What if you can't guarantee monotonicity?
- **Answer:** Algorithm breaks; might get wrong answer or time out. Always verify monotonicity before applying pattern.

---

## DIVIDE & CONQUER (6+ Questions)

### Q15: Explain Merge Sort Complexity (Medium)
**Interview Signal:** "Why O(n log n)?", "recurrence", "analysis"

**Problem:** Why is merge sort O(n log n)?

**Answer Framework:**
- **Recurrence:** T(n) = 2·T(n/2) + O(n)
  - Split: O(1)
  - Sort two halves: 2·T(n/2)
  - Merge: O(n)
- **Recursion tree:** log(n) levels, each level does n work = O(n log n)
- **Proof by substitution or Master Theorem:** a=2, b=2, d=1 → a=b^d → O(n log n)

**Key insight:** Merge of two sorted arrays is O(n), not O(n²).

**Follow-up:** Compare to quicksort.
- **Answer:** Quicksort is O(n log n) average, O(n²) worst. Merge sort is always O(n log n) but needs O(n) extra space.

---

### Q16: Counting Inversions (Hard)
**Interview Signal:** "Inversions", "divide and conquer", "pairs"

**Problem:** Count inversions in array (pairs where i < j but arr[i] > arr[j]).

**Answer Framework:**
- **Naive:** O(n²) — check all pairs
- **Divide & Conquer:** O(n log n) — count during merge
- **Key Insight:** When merging sorted arrays, taking element from right array counts inversions with all remaining left elements (because left is sorted)
- **Implementation:** Modify merge sort to count inversions during merge phase

**Example:**
```
Merge [1, 5] with [2, 4]:
- Take 2 (from right): inversions with [5] = 1
- Take 4 (from right): inversions with [5] = 1
- Total: 2 inversions
```

**Follow-up:** Why doesn't naive all-pairs approach work at scale?
- **Answer:** 1 million elements = 10^12 comparisons. Binary search divide & conquer does ~20 million.

---

### Q17: Master Theorem Application (Hard)
**Interview Signal:** "Master Theorem", "recurrence", "complexity"

**Problem:** Solve T(n) = 3·T(n/2) + O(n).

**Answer Framework:**
- **Format:** T(n) = a·T(n/b) + O(n^d)
- **Identify:** a=3, b=2, d=1
- **Compare:** log_b(a) = log_2(3) ≈ 1.585
- **Decision:** a > b^d (3 > 2¹=2) → T(n) = O(n^log_2(3)) ≈ O(n^1.585)
- **Conclusion:** Better than O(n²), worse than O(n log n)

**When to use Master Theorem:** Most divide & conquer problems fitting the form.

**Follow-up:** What if a < b^d?
- **Answer:** T(n) = O(n^d). Example: T(n) = T(n/2) + O(n) → O(n).

---

### Q18: Optimal Substructure (Medium)
**Interview Signal:** "Optimal substructure", "how to verify", "correctness"

**Problem:** What does "optimal substructure" mean, and how do you verify it?

**Answer Framework:**
- **Definition:** Optimal solution to problem contains optimal solutions to subproblems
- **Verification:** Prove that combining optimal subproblem solutions gives optimal parent solution
- **Example (sorting):** Sorting array requires sorting halves; if halves are sorted, merging gives sorted array. ✓
- **Counterexample (longest path in graph with negative edges):** Longest path in left + longest path in right ≠ longest path overall. ✗

**When it applies:** Sorting, searching, many optimization problems. Doesn't apply to problems with overlapping subproblems (use DP instead).

**Follow-up:** How does optimal substructure differ from overlapping subproblems?
- **Answer:** Optimal substructure is necessary for divide & conquer; overlapping means subproblems repeat (use DP memoization).

---

## BINARY SEARCH ON ANSWERS (6+ Questions)

### Q19: Binary Search vs. Answer Space (Hard)
**Interview Signal:** "Optimization", "binary search", "not on array"

**Problem:** Explain the difference between binary search on arrays vs. answer space.

**Answer Framework:**
- **Array Search:** Find element in pre-existing sorted array (Week 2)
- **Answer Space Search:** Find optimal answer by searching a virtual space
  - Low/High are not array bounds; they're candidate answer ranges
  - Feasibility function replaces comparisons
  - Each iteration checks "can we achieve answer X?"
- **Key difference:** Answer space might not be an array; it's defined by your problem

**Example:**
- **Array:** Binary search [1, 2, 3, 4, 5] for element 3
- **Answer Space:** Binary search [1, 1000] for "minimum time to complete n jobs"

**Follow-up:** What properties of answer space enable binary search?
- **Answer:** Monotonicity: feasibility boundary is well-defined. If X is feasible, all Y ≥ X are feasible (or vice versa).

---

### Q20: Machine Scheduling (Hard)
**Interview Signal:** "Minimize makespan", "machines", "jobs"

**Problem:** Given n jobs with times, distribute to m machines to minimize max time.

**Answer Framework:**
- **Answer Space:** Possible makespans [max_job, sum_all_jobs]
- **Feasibility Function:** Can we complete all jobs in time T with m machines?
  - Greedy: Assign each job to machine with least current load
  - Check: max load ≤ T?
- **Binary Search:** Find minimum T where feasibility is true
- **Complexity:** O(log(sum) × n log m) — log(sum) binary search iterations, each checks n jobs, assigns to m machines using min-heap

**Why greedy feasibility works:** Load balancing greedy is optimal for feasibility checking (not necessarily optimal allocation, but optimal for checking a given deadline).

**Follow-up:** How would you construct the actual allocation?
- **Answer:** When feasibility check passes, record which job goes to which machine during the final run.

---

### Q21: Aggressive Cows (Hard)
**Interview Signal:** "Maximize minimum distance", "places", "binary search"

**Problem:** Place k cows in n stalls to maximize minimum distance between any two.

**Answer Framework:**
- **Answer Space:** Distance [1, max_position]
- **Feasibility Function:** Can we place k cows with minimum distance D?
  - Greedy: Place first cow at position 0; each subsequent at furthest valid position (≥ prev + D)
  - Check: Did we place k cows?
- **Binary Search:** Find maximum D where feasibility is true
- **Complexity:** O(log(max_pos) × n) — log(max) iterations, each does n checks

**Key Insight:** If we can place k cows with distance D, we can definitely place with distance D-1. Monotonicity holds.

**Follow-up:** What if positions are unsorted?
- **Answer:** Sort first: O(n log n). Then binary search: O(log(max) × n). Total: O(n log n).

---

### Q22: Designing Feasibility Checkers (Hard)
**Interview Signal:** "Custom feasibility", "design checker", "correctness"

**Problem:** How do you design a correct feasibility checker for binary search on answers?

**Answer Framework:**
1. **Understand the problem:** What are we optimizing? (minimize/maximize?)
2. **Define answer space:** What are the bounds? (min and max possible answers?)
3. **Design feasibility:** Given a candidate answer C, can we verify if it's achievable?
   - Usually involves a greedy or simulation approach
   - Must be efficient (typically O(n) or O(n log n))
4. **Verify monotonicity:** If C is feasible, are all "better" values feasible?
   - For minimization: if achievable with time T, achievable with time T+1? (usually yes)
   - For maximization: if achievable with distance D, achievable with D-1? (usually yes)
   - **Critical:** Verify this carefully; if false, binary search fails

**Checklist before implementing:**
- ☐ Answer bounds clearly defined?
- ☐ Feasibility function correctly identifies achievability?
- ☐ Feasibility is monotonic (proven)?
- ☐ Feasibility check is efficient enough?

**Follow-up:** What happens if monotonicity fails?
- **Answer:** Binary search gives wrong answer or loops. Always verify monotonicity in design phase.

---

## COMPREHENSIVE COMPARISON QUESTIONS

### Q23: Pattern Recognition (Medium)
**Interview Signal:** "Which pattern?", "recognize", "approach"

**Problem:** Given 5 problems, identify which pattern applies.

**Answer Framework:** Use decision tree from Week 04 summary. Key questions:
1. Involves two pointers/regions? → Two-Pointer
2. Involves a window? → Sliding Window (fixed or variable?)
3. Can split into subproblems? → Divide & Conquer
4. Optimizing with constraint? → Binary Search on Answer

---

### Q24: Trade-Offs Discussion (Medium)
**Interview Signal:** "Trade-off", "when to use", "pros/cons"

**Problem:** Compare two approaches; discuss trade-offs.

**Answer Framework:**
- **Time complexity:** Which is faster?
- **Space complexity:** Which uses less memory?
- **Implementation complexity:** Which is easier to code correctly?
- **Production suitability:** Which scales better? Handles edge cases?
- **Context matters:** Interview wants O(n) space-optimized? Or simpler O(n) hash map?

---

## HOW TO USE THIS REFERENCE

**For Interview Prep:**
1. Read question
2. Think 5-10 mins without reading answer
3. Compare your answer to framework
4. Note gaps and misconceptions
5. Repeat with different questions

**For Learning:**
1. Read answer framework carefully
2. Understand *why*, not just *how*
3. Mentally trace through examples
4. Explain aloud to solidify understanding

**For Revision:**
1. Read questions (without answers) as quick check
2. Answer from memory
3. Verify against framework
4. Identify weak areas; review instructional file

---

**Remember:** Patterns are reusable. Master one problem deeply; similar problems become obvious.


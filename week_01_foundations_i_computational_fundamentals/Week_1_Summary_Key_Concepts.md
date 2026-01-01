# 🗺 WEEK 1 — CONCEPT MAP & SUMMARY

**Week Theme:** Computational Foundations  
**Status:** Foundation Complete

---

## 📚 Key Concepts (Per Day)

### 📅 Day 1: RAM Model & Pointers
- **Memory is an Array:** Physical RAM is a 1D array of bytes with addresses (0x00000000 to 0xFFFFFFFF).
- **Pointers Store Addresses:** A pointer is just an integer that refers to a memory location.
- **Dereferencing Costs:** Following a pointer chain is slower than direct access due to cache misses.

### 📅 Day 2: Asymptotic Analysis (Big-O)
- **Growth Rates Matter:** O(n) vs O(n²) determines scalability, not constants.
- **Dominant Terms Win:** O(3n² + 5n + 10) simplifies to O(n²).
- **Best/Average/Worst:** Always clarify which case you're analyzing.

### 📅 Day 3: Space Complexity & Memory Usage
- **Stack vs Heap:** Stack is fast, small, automatic. Heap is flexible, large, managed.
- **Auxiliary Space:** Only count extra space, not input.
- **Hidden Costs:** Recursion depth, string immutability, object overhead.

### 📅 Day 4: Recursion I (Call Stack & Basic Patterns)
- **Base Case is Mandatory:** Without it, you get infinite recursion.
- **Trust the Induction:** Don't trace deep; assume the recursive call works.
- **Pre-order vs Post-order:** Work before vs after the recursive call.

### 📅 Day 5: Recursion II (Memoization)
- **Overlapping Subproblems:** Same inputs computed multiple times.
- **Memoization = Cache:** Store results in a Dictionary/Array.
- **Exponential → Linear:** O(2^n) becomes O(n) with memoization.

---

## 🔗 Concept Relationships

```
RAM Model (Day 1)
    ↓ (Physical foundation)
Space Complexity (Day 3)
    ↓ (Stack is a region of RAM)
Recursion I (Day 4)
    ↓ (Uses Stack heavily)
Recursion II (Day 5)
    ↓ (Trades Stack for Heap via Memoization)

Big-O (Day 2)
    ↓ (Measures both Time and Space)
Space Complexity (Day 3)
    ↓ (Applies to all algorithms)
Recursion I & II (Days 4-5)
```

---

## 📊 Comparison & Relationship Table

| Concept | Time Focus | Space Focus | Key Trade-off |
|---------|-----------|-------------|---------------|
| **RAM Model** | Access time (O(1)) | Physical layout | Contiguous vs Scattered |
| **Big-O** | Algorithm growth | - | Speed vs Simplicity |
| **Space Complexity** | - | Stack vs Heap | Memory vs CPU |
| **Recursion** | Often exponential (naive) | Stack depth O(n) | Code clarity vs Safety |
| **Memoization** | Linearizes time | Adds heap O(n) | Time vs Space |

---

## 💡 7 Key Insights from Week 1

1. **Memory is Physical:** RAM, caches, stack—all are hardware realities, not abstract concepts.
2. **Recursion Uses Stack:** Every frame costs memory. Deep = Dangerous.
3. **Memoization is Magical:** Turns intractable problems into trivial ones.
4. **Big-O Hides Details:** Same O(n) can differ 10x in practice (cache effects).
5. **Base Cases Save Lives:** One missing `if` causes crashes.
6. **Trust Induction:** Don't trace; prove the pattern holds.
7. **Trade-offs Everywhere:** Time vs Space. Readability vs Performance. Theory vs Practice.

---

## ⚠ 5 Common Misconceptions Fixed

1. **"Recursion is always slow."**  
   → Reality: With memoization, it's often optimal.

2. **"Big-O = exact runtime."**  
   → Reality: It's a growth rate, not a timer.

3. **"Space complexity = variables I create."**  
   → Reality: Stack frames, object headers, alignment padding all count.

4. **"Memoization works on any function."**  
   → Reality: Only pure functions (no side effects).

5. **"If it compiles, the space is fine."**  
   → Reality: Compilation doesn't check stack depth limits.

---

*End of Week 1 Summary*
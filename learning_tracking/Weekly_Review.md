# Weekly Review

Create one section per week.

## Week XX Review Template

### 1) Outcomes
- Problems attempted:
- Problems solved independently:
- Main patterns covered:

### 2) What Improved
- 

### 3) What Is Still Weak
- 

### 4) Top Mistakes
1. 
2. 
3. 

### 5) Next Week Focus (Top 3)
1. 
2. 
3. 

### 6) Confidence Snapshot
- Arrays/Strings:
- Trees:
- Graphs:
- DP:
- Greedy:

---

## Week 14 Review: Matrices, Bitmasks & Number Theory

### 1) Outcomes
- Problems attempted: 8
- Problems solved independently: 6
- Main patterns covered: In-place matrix transformations (transpose, rotate), spiral traversal walks, bitwise operations (popcount, submask loops), Sieve of Eratosthenes, and KMP pattern preprocessing.

### 2) What Improved
- Handled indexing boundaries for matrix spiral reads cleanly.
- Reduced memory overhead in grid visited checks to zero by packing cell flags into register-size integers.
- Built a KMP state table to match patterns without pointer backtracking.

### 3) What Is Still Weak
- Chinese Remainder Theorem modulus mapping reconstruction requires careful sign checks.
- Radix sorting algorithms (MSD/LSD partition logic) take time to configure correctly under pressure.

### 4) Top Mistakes
1. Checked power of two status using `(n & (n - 1)) == 0` without validating positivity, which caused incorrect true returns when `n == 0`.
2. Performed standard division `A / B % M` inside a modular multiplier instead of multiplying by the multiplicative inverse of `B` modulo `M`.

### 5) Next Week Focus (Top 3)
1. Master Advanced Segment Trees and Range Query structures.
2. Study the Max-Flow Min-Cut theorem and network flow architectures.
3. Maintain zero index errors on variable sliding windows.

### 6) Confidence Snapshot
- Arrays/Strings: 90%
- Trees: 85%
- Graphs: 80%
- DP: 80%
- Greedy: 85%
- Matrices & Bitmasks: 90%
- Number Theory: 80%

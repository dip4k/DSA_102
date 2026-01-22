# 📖 Week 06 Summary & Key Concepts: String Patterns Reference

**Status:** Graduate-Level Study Notes  
**Audience:** Students completing Week 06 instructional content  
**Purpose:** Comprehensive reference for review, retention, and interview prep

---

## 🎯 Week 06 Narrative Summary

**Week 06: String Patterns (Tier 1.5)** teaches you to recognize and solve the five most common string problems with elegance and efficiency. You'll move from analyzing string structure (what is the string?) to manipulating it (what can I do with it?) to optimizing at scale (how do I handle billions of characters?).

### The Five Core Patterns

**Day 1: Palindrome Patterns** — Exploit symmetry to detect and find palindromes. Expand-around-center runs in O(n²) worst-case but is elegant and intuitive. Mental model: two pointers moving inward while maintaining mirror property.

**Day 2: Substring Sliding Windows** — Find substrings matching constraints using variable-size windows. The window grows to include more characters, shrinks when constraints violated. Time O(n) amortized. Mental model: "shrink when bad, expand when good."

**Day 3: Bracket Matching** — Validate nesting and find longest valid substrings using stacks. Stack maintains unmatched opens; each close matches against top. LIFO discipline is key. Mental model: "LIFO matching for nested structures."

**Day 4: String Transformations** — Convert between formats (integer to Roman, compression, parsing) efficiently. StringBuilder avoids O(n²) concatenation trap. Mental model: "Builders for efficiency, defensive parsing for correctness."

**Day 5: Advanced Pattern Matching (Optional)** — Scale pattern matching to billions using rolling hash. Karp-Rabin computes hashes incrementally in O(1), reducing matching to O(n+m) expected time. Mental model: "Rolling polynomial for scale."

---

## 🧠 Concept Map: Relationships Between Patterns

```
String Problem Space

├─ STRUCTURE ANALYSIS
│  ├─ Is it a palindrome?
│  │  └─ Expand-around-center (Day 1)
│  └─ Is it nested/balanced?
│     └─ Stack validation (Day 3)
│
├─ SUBSTRING SEARCH
│  ├─ Find longest with property P?
│  │  └─ Sliding window (Day 2)
│  └─ Find pattern M in text T?
│     └─ Karp-Rabin rolling hash (Day 5, optional)
│
├─ TRANSFORMATION
│  ├─ Parse input (string → integer)?
│  │  └─ Defensive character-by-character (Day 4)
│  ├─ Convert format (integer → Roman)?
│  │  └─ Greedy mapping (Day 4)
│  └─ Compress data?
│     └─ Run-length encoding (Day 4)
│
└─ OPTIMIZATION
   ├─ Single pattern, slow?
   │  └─ Use Karp-Rabin (Day 5)
   └─ Multiple patterns, multiple texts?
      └─ Pre-hash, then lookup (Day 5)
```

---

## 📋 Per-Day Concept Summary

### Day 1: Palindrome Patterns — Symmetry & Expansion

**Definition:** A palindrome reads the same forwards and backwards (e.g., "racecar").

**Key Property:** Mirror symmetry around a center point (which can be a single character or between two characters).

**Expand-Around-Center Technique:**
- For each possible center (n characters + n-1 gaps = 2n-1 centers)
- Expand outward while characters match
- Track longest found

**Time/Space:** O(n²) time worst-case, O(1) extra space (plus output).

**When to Use:** Finding longest palindromic substring; palindrome checking; DNA pattern analysis.

**Common Mistakes:**
- Forgetting both odd-length (center on character) and even-length (center between characters) cases
- Not tracking the best palindrome found so far

---

### Day 2: Substring Sliding Windows — Constraints & Movement

**Definition:** Find a substring satisfying constraint C (e.g., "longest substring without repeating characters").

**Core Logic:**
1. Initialize window to empty or first position
2. Expand right to include more characters
3. While constraint violated, shrink from left
4. Repeat until right pointer reaches end

**Time/Space:** O(n) amortized time, O(k) space for frequency map (k = alphabet size).

**Key Invariant:** At each step, window[left..right] is the best window seen so far.

**Pattern Variations:**
- **Fixed-size window:** Simpler; slide by one position (Day 2, simpler problems)
- **Variable-size window:** Harder; expand/shrink based on constraint (Day 2, complex problems)
- **At-most-K constraint:** Track frequency of each character; when > K distinct, shrink

**When to Use:** Longest substring without repeating; character replacement; permutation substrings; minimum window.

**Common Mistakes:**
- Moving only right pointer (not shrinking when needed)
- Not updating constraint tracking (frequency map) correctly

---

### Day 3: Bracket Matching — Nesting & Stack Discipline

**Definition:** Validate that brackets are properly matched and nested (e.g., "{[()]}").

**Core Data Structure:** Stack holds unmatched opening brackets.

**Algorithm:**
1. For each character:
   - If opening bracket, push to stack
   - If closing bracket, check if stack top matches; pop if yes, error if no
2. At end, stack must be empty

**Time/Space:** O(n) time, O(n) worst-case space (stack can hold all opens).

**Key Invariant:** Stack contains only unmatched opening brackets, in order of appearance.

**Variants:**
- **Valid parentheses:** Check if string is valid (boolean output)
- **Longest valid parentheses:** Find longest contiguous valid substring (use stack indices)
- **Generate all valid:** Use backtracking to generate all valid combinations of n pairs

**When to Use:** Compiler syntax checking; JSON/XML validation; expression parsing; expression evaluation.

**Common Mistakes:**
- Not checking stack non-empty before popping
- Not handling multiple bracket types correctly
- Forgetting that order matters: {[}] is invalid despite balanced counts

---

### Day 4: String Transformations — Format Conversion & Efficiency

**Definition:** Convert string from one format to another (parse integer from string, encode to compressed format, etc.).

**Categories:**
1. **Parsing:** String → Structured data (atoi, validation)
2. **Encoding:** Data → Different representation (Roman numerals, compression)
3. **Building:** Structured data → String (formatting, serialization)

**Critical Efficiency Insight:** String concatenation in a loop is O(n²) in most languages due to immutability.

**Solution:** Use StringBuilder (accumulate in mutable buffer, convert to immutable string at end).

**Time/Space:** O(n) with StringBuilder (vs O(n²) naive), O(n) output space.

**Common Patterns:**
- **Greedy mapping:** Integer to Roman (map ranges in descending order)
- **Character-by-character parsing:** atoi (handle sign, overflow, non-numeric)
- **Index formula:** Zigzag conversion (derive formula for position mapping)

**When to Use:** Any string building; user input parsing; data serialization; encoding/compression.

**Common Mistakes:**
- Using naive concatenation (string + string in loop)
- Not handling overflow in integer parsing
- Not handling sign (+/-) in parsing
- Forgetting edge cases (empty input, single character, boundary values)

---

### Day 5: Advanced Pattern Matching — Rolling Hash & Karp-Rabin (Optional)

**Definition:** Find pattern P in text T efficiently using polynomial hashing.

**Core Insight:** Represent each substring as polynomial evaluated at prime base p. As window slides, update polynomial in O(1) by: remove old, shift (multiply by p), add new.

**Algorithm (Karp-Rabin):**
1. Precompute hash of pattern and p^(m-1) mod MOD
2. Hash first window of text
3. For each position, roll hash to next window
4. On hash match, verify actual substring (to handle collisions)

**Time/Space:** O(n+m) expected time, O(1) space; verification is O(m) on collision (rare with large primes).

**Key Probability:** With modulus = 10^9 + 7 and n = 10^6 comparisons, expected collisions ≈ 0.001 (negligible).

**When to Use:** Multiple patterns in same text; plagiarism detection; DNA matching; streaming pattern search.

**Comparison with Alternatives:**
- **Naive:** O(nm), slow for large patterns
- **Karp-Rabin:** O(n+m) avg, simple code, multi-pattern friendly
- **KMP:** O(n+m) worst-case, complex code, guaranteed linear
- **Boyer-Moore:** O(n/m) best-case, fastest for long patterns in practice

**Common Mistakes:**
- Integer overflow (must apply modulo after each operation)
- Negative modulo (use (a - b + MOD) % MOD for subtraction)
- Not verifying on hash match (false positives without verification)
- Choosing non-prime modulus (worse collision properties)

---

## 📊 Comparison Tables: Patterns at a Glance

### Algorithm Comparison: Time & Space

| Algorithm | Time Best | Time Avg | Time Worst | Space | When |
| :--- | :--- | :--- | :--- | :--- | :--- |
| Naive substring match | O(n) | O(nm) | O(nm) | O(1) | Tiny strings |
| Expand-around-center | O(1) | O(n²) | O(n²) | O(1) | Palindromes |
| Sliding window | O(n) | O(n) | O(n) | O(k) | Substrings |
| Stack (brackets) | O(n) | O(n) | O(n) | O(n) | Validation |
| StringBuilder | O(n) | O(n) | O(n) | O(n) | Building |
| **Karp-Rabin** | **O(n+m)** | **O(n+m)** | O(nm) collisions | **O(1)** | **Multiple patterns** |

### String Problem Decision Matrix

| Problem Type | Pattern | Time | Space | Difficulty |
| :--- | :--- | :--- | :--- | :--- |
| Longest palindromic substring | Expand-around-center | O(n²) | O(1) | 🟡 Medium |
| Longest substring without repeating | Sliding window | O(n) | O(k) | 🟡 Medium |
| Valid parentheses | Stack | O(n) | O(n) | 🟢 Easy |
| String to integer | Character parsing | O(n) | O(1) | 🟡 Medium |
| Integer to Roman | Greedy mapping | O(log n) | O(1) | 🟡 Medium |
| Repeated DNA sequences | Karp-Rabin rolling hash | O(n) | O(1) | 🟡 Medium |

---

## 🎓 Seven Key Insights

### Insight 1: Symmetry Beats Brute Force
Expand-around-center finds longest palindromes without comparing all pairs. Exploit structure whenever possible; brute force is a last resort.

### Insight 2: Invariants Are Guardrails
Every algorithm maintains an invariant (e.g., "stack contains only unmatched opens"). Understanding invariants makes you immune to off-by-one errors.

### Insight 3: Immutability Has a Price
Languages with immutable strings (Java, Python, C#) make naive concatenation O(n²). Builders are mandatory for any string accumulation.

### Insight 4: LIFO Enforces Discipline
Stacks work for brackets because of strict nesting. Use stacks when order and LIFO matching matter; use queues otherwise.

### Insight 5: Two-Pointer Patterns Are Universal
Same-direction (fast/slow), opposite-direction (palindrome), and sliding windows are templates you'll use in trees, graphs, and array problems.

### Insight 6: Defensive Parsing Prevents Disasters
Parsing user input without checking boundaries (overflow, non-numeric) is a common bug. Guard clauses first, then logic.

### Insight 7: Hash Scaling Is Elegant
Rolling hash reduces naive O(nm) to O(n+m) by clever math (polynomial evaluation). It shows how deep insights beat brute force.

---

## ❌ Seven Common Misconceptions (Corrected)

### Misconception 1: Palindromes Are Only Odd-Length
**False Reality:** Both odd-length ("aba") and even-length ("abba") palindromes exist. Expand-around-center must check both center types (on character and between characters).

### Misconception 2: Sliding Window Always Shrinks Immediately
**False Reality:** Different problems require different shrinking strategies. "Longest substring" requires shrinking only when constraint violated; "shortest with property X" requires shrinking when valid.

### Misconception 3: Stacks Are Only for Recursion
**False Reality:** Stacks model any LIFO structure (brackets, undo/redo, call stacks, DFS). Recognizing when to use stacks is key.

### Misconception 4: String Builders Are Optional
**False Reality:** In languages with immutable strings, builders are mandatory. Naive concatenation in a loop is unacceptable in production.

### Misconception 5: Overflow Won't Happen in Practice
**False Reality:** Edge cases happen. Boundary input (2^31 - 1 for 32-bit int, empty strings, single characters) appear in real systems. Always check.

### Misconception 6: Hash Collisions Ruin Karp-Rabin
**False Reality:** With large prime moduli, collisions are rare (< 0.001%). Verification on collision is O(m), but expected cost is negligible.

### Misconception 7: Pattern Matching Is Always Hard
**False Reality:** Karp-Rabin is surprisingly simple—just rolling polynomial arithmetic. KMP is harder but guarantees worst-case O(n+m). Choose based on problem.

---

## 🔗 Week 06 Connection to Other Weeks

**From Week 05 (Tier 1 Patterns):**
- Monotonic stack concept (Day 5) → Stack usage for brackets (Day 3)
- Two-pointer basics (Day 1-4) → extended to substrings
- Hash tables (Day 5) → foundation for rolling hash (Day 5, optional)

**To Week 07 (Trees):**
- Two-pointer thinking → tree traversal (left/right child)
- Stack usage → DFS recursion
- Invariant maintenance → BST properties

**To Week 10 (Dynamic Programming):**
- Edit distance uses substring windows (Day 2)
- Palindrome partitioning uses Day 1 concepts
- Many string DP problems combine multiple patterns

**To Week 15 (Advanced Strings):**
- KMP, Z-algorithm, Aho-Corasick extend pattern matching (Day 5)
- Suffix arrays/trees use similar thinking to hashing

---

## ✅ Mastery Signals: Week 06 Checklist

**Pattern Recognition:**
- [ ] See "longest X with property Y" → immediately think sliding window
- [ ] See "valid/balanced" → immediately think stack
- [ ] See "find palindrome" → immediately think expand-around-center
- [ ] See "multiple patterns" → immediately think rolling hash

**Implementation Confidence:**
- [ ] Write sliding window code without second-guessing pointer logic
- [ ] Implement stack-based bracket matching on first try
- [ ] Code string building without forgetting StringBuilder
- [ ] Trace through rolling hash without computing full polynomials

**Edge Case Mastery:**
- [ ] List 5 edge cases for each pattern before coding
- [ ] Implement guard clauses before core logic
- [ ] Handle empty input, single character, and boundary values
- [ ] Check for overflow and negative modulo in rolling hash

**Optimization Instinct:**
- [ ] Recognize O(n²) naive solutions immediately
- [ ] Apply optimization (builder, clever algorithm) proactively
- [ ] Explain space-time trade-offs for each pattern
- [ ] Know when to trade complexity for readability

---

## 📚 External Reference: When to Dive Deeper

**For Palindromes:**
- Manacher's Algorithm (O(n) guaranteed for longest palindromes)
- Palindrome Partitioning (DP + recursion)

**For Substrings:**
- Minimum Window Substring (classic hard problem)
- Longest Repeating Character Replacement

**For Brackets:**
- Expression Evaluation (stacks + parsing)
- N-Queens and Constraint Satisfaction (advanced)

**For Transformations:**
- Text Justification (complex layout)
- Valid Number (all edge cases)

**For Matching (Optional):**
- KMP Algorithm (guaranteed O(n+m) worst-case)
- Aho-Corasick (multiple patterns efficiently)

---

## 🎯 Interview Readiness: Week 06 Checklist

You're ready for interviews when:

1. **Pattern Recognition < 10 seconds:** See a problem, recognize the pattern instantly
2. **Implementation without bugs:** Code the solution fluently, no syntax errors
3. **Trace on paper:** Manually trace through example without running code
4. **Explain trade-offs:** Compare time, space, and code complexity confidently
5. **Handle edge cases:** Speak about boundary conditions before implementation
6. **Real-world impact:** Connect pattern to actual system (compiler, editor, etc.)

---

**Status:** Week 06 Summary Complete  
**Next:** Week 06 Interview Q&A Reference  
**Review Time:** 1-2 hours
# 📋 Week 06 Guidelines: String Patterns Mastery

**Week Overview:** String Manipulation Patterns (Tier 1.5)  
**Total Content:** 5 comprehensive instructional files + 5 support guides  
**Time Allocation:** 12-18 hours core learning + practice

---

## 🎯 Week Learning Arc

This week transforms you from pattern analyzer to string manipulator. You'll master the five core string operations that power everything from text editors to plagiarism detectors:

**Days 1-2:** Understand string structure (palindromes, substrings)  
**Day 3:** Learn constraint satisfaction (brackets, nesting)  
**Day 4:** Master efficient transformation (building, encoding, conversion)  
**Day 5:** Explore advanced optimization (rolling hash, pattern matching at scale)

By week's end, you'll recognize string problems instantly and implement solutions with both correctness and efficiency.

---

## 📚 Day-by-Day Concept Overview

### Day 1: Palindrome Patterns
**Core Mental Model:** Symmetry detection using expand-around-center

**Key Techniques:**
- Two-pointer symmetry checking (O(n) time, O(1) space)
- Expand-around-center for longest palindromic substring (O(n²) time worst-case)
- Invariant: maintaining mirror property as pointers move inward

**Why It Matters:** Palindrome patterns teach you how to think about string symmetry, which extends to compression detection, DNA matching, and data validation.

**Connection:** This is your foundation for understanding substring structure. Day 2 builds on this to handle arbitrary windows.

---

### Day 2: Substring Sliding Windows
**Core Mental Model:** Variable-size window with constraint management

**Key Techniques:**
- Two-pointer window expansion and contraction (O(n) amortized)
- Frequency maps for character counting
- Decision logic: when to expand vs shrink

**Why It Matters:** Substring problems account for 15-20% of interview questions. Mastering the sliding window pattern gives you a template for "find the longest/shortest substring with property X."

**Connection:** Day 1 taught you fixed-size symmetry; Day 2 generalizes to arbitrary windows. Day 3 uses similar logic for validation.

---

### Day 3: Parentheses & Bracket Matching
**Core Mental Model:** Stack discipline for nested structures

**Key Techniques:**
- LIFO stack for matching (O(n) time, O(n) space worst-case)
- State machine for validation
- Index tracking for longest valid substring

**Why It Matters:** Bracket matching is the canonical stack application. It teaches you when to use LIFO (instead of FIFO queues). Every compiler, JSON parser, and code editor uses this.

**Connection:** Days 1-2 analyzed substrings; Day 3 adds nesting constraints. Day 4 uses similar logic to transform formats.

---

### Day 4: String Transformations & Building
**Core Mental Model:** Format conversion with efficiency optimization

**Key Techniques:**
- String builders to avoid O(n²) concatenation (O(n) amortized)
- Greedy algorithms for mappings (Roman numerals)
- Character-by-character parsing with overflow detection

**Why It Matters:** Transformation skills power logging systems, serialization, data formatting. The StringBuilder pattern is critical: O(n²) naive → O(n) optimized.

**Connection:** Days 1-3 analyzed existing strings; Day 4 builds new strings. Day 5 makes pattern matching fast at scale.

---

### Day 5: Advanced String Matching (Optional)
**Core Mental Model:** Rolling polynomial hash for O(n+m) pattern matching

**Key Techniques:**
- Incremental hash updates in O(1) (remove-shift-add)
- Rabin-Karp algorithm with collision probability analysis
- Comparison with KMP, Boyer-Moore for different problem sizes

**Why It Matters:** Rolling hash powers plagiarism detection, DNA matching, and real-time streaming. It shows how cleverness in math (polynomial evaluation) beats brute force.

**Connection:** Days 1-4 solved string problems on known texts; Day 5 optimizes for multiple patterns and massive corpora.

---

## 🎓 Eight Core Learning Objectives

By end of Week 06, you will be able to:

1. **Analyze** any string problem and recognize its pattern (palindrome, window, bracket, transform, match)
2. **Implement** each pattern with both correctness and efficiency in mind
3. **Trace** your solution step-by-step to verify correctness
4. **Explain** the invariants that make each approach work
5. **Compare** trade-offs (time, space, code complexity) between approaches
6. **Design** robust solutions that handle edge cases and boundary conditions
7. **Optimize** from naive O(n²) solutions to O(n) or better using appropriate techniques
8. **Connect** string patterns to real systems (compilers, editors, search engines, forensics)

---

## 🧭 How to Study Effectively

### Strategy 1: Pattern Recognition First, Code Second

**Wrong approach:** See problem → write code immediately

**Right approach:** See problem → identify which of 5 patterns it matches → recall the technique → implement

Spend 30% of time on pattern recognition (reading problems without coding), 70% on implementation.

### Strategy 2: Trace Before You Code

For each algorithm:
1. Read the explanation
2. Trace through an example on paper (no computer)
3. Write pseudocode
4. Implement in your language
5. Test on edge cases

This builds mental models that stick.

### Strategy 3: Build Your Pattern Toolbox

Create a reference document with:
- Pattern name (e.g., "Expand-Around-Center")
- When to use (e.g., "longest palindromic substring")
- Time/space complexity
- Skeleton code (4-5 lines showing structure)

By week's end, you should have 5-10 patterns memorized to muscle memory.

### Strategy 4: Solve Problems in Progression

**Stage 1 (Canonical):** Solve the classic LeetCode problem for each pattern
**Stage 2 (Variations):** Solve 2-3 variations (different constraints, edge cases)
**Stage 3 (Integration):** Solve problems mixing patterns or adding system design

Don't skip stages. Rushing to Stage 3 before mastering Stage 1 creates false confidence.

---

## ⏱️ Recommended Time Allocation

| Activity | Hours | Focus |
| :--- | :--- | :--- |
| Read Day 1-2 instructional | 3 | Build mental models, trace examples |
| Practice Day 1-2 problems | 3 | 5-8 LeetCode problems each |
| Read Day 3 instructional | 2 | Stack discipline, state machines |
| Practice Day 3 problems | 2 | 5 LeetCode problems |
| Read Day 4 instructional | 2 | Transformation techniques, builders |
| Practice Day 4 problems | 2 | 5 LeetCode problems |
| Synthesis & review | 1-2 | Review all 4 days, connect patterns |
| Day 5 (optional advanced) | 2-3 | Read + 3-4 rolling hash problems |
| **Total Core (Days 1-4)** | **15-16 hours** | Interview prep complete |
| **Total with Day 5** | **17-19 hours** | Mastery level |

---

## 🚫 Five Common Pitfalls (and How to Avoid Them)

### Pitfall 1: Confusing Two-Pointer Directions

**Wrong:** Using same-direction pointers (slow and fast) for palindrome checking

**Reality:** Palindrome checking needs opposite-direction pointers (left and right converging)

**Fix:** Remember the mental model: opposite-direction pointers "meet in the middle" (symmetry), same-direction pointers "lap" (cycle detection or racing)

---

### Pitfall 2: Forgetting to Handle String Immutability

**Wrong:** Concatenating strings in a loop (`result = result + char`) in languages like Java/Python

**Reality:** Creates O(n²) time due to object allocation and copying

**Fix:** Always use StringBuilder (Java), list append then join (Python), or StringBuffer. This is non-negotiable for production code.

---

### Pitfall 3: Stack Overflow When Checking Brackets

**Wrong:** Forgetting to verify the stack is non-empty before popping

**Example:** When closing bracket appears before opening bracket

**Fix:** Always check `!stack.isEmpty()` before popping. Guard clauses are your friend.

---

### Pitfall 4: Boundary Off-by-One in Rolling Hash

**Wrong:** Computing base power incorrectly or forgetting to apply modulo after operations

**Reality:** Integer overflow or negative modulo values silently corrupt hashes

**Fix:** Apply modulo after EVERY arithmetic operation. Use `(a - b + MOD) % MOD` for subtraction.

---

### Pitfall 5: Not Verifying Hash Matches

**Wrong:** Assuming hash collision = string match

**Reality:** Hash collisions exist. False positives lead to wrong answers.

**Fix:** On hash match, always verify the actual strings with character-by-character comparison.

---

## 📊 Concept Map: How Week 06 Patterns Connect

```
All String Problems

├─ ANALYZE STRUCTURE
│  ├─ Palindrome? (Day 1)
│  └─ Symmetry-based solution
│
├─ FIND SUBSTRING WITH PROPERTY
│  ├─ Fixed window? (Day 2, simple)
│  ├─ Variable window? (Day 2, complex)
│  └─ Sliding window template
│
├─ VALIDATE NESTING
│  ├─ Brackets? (Day 3)
│  ├─ Stack for LIFO matching
│  └─ State machine for validation
│
├─ CONVERT FORMATS
│  ├─ Build new string? (Day 4)
│  ├─ Parse input safely? (Day 4)
│  ├─ StringBuilder for efficiency
│  └─ Greedy mapping for conversion
│
└─ SCALE TO BILLIONS
   ├─ Multiple patterns? (Day 5)
   ├─ Massive corpus? (Day 5)
   ├─ Rolling hash + Rabin-Karp
   └─ O(n+m) beats O(nm)
```

---

## 🎯 Seven Key Insights to Remember

1. **Symmetry is faster than search:** Palindromes teach you to leverage structure instead of brute force.

2. **Invariants are your guide:** Every algorithm maintains some property. Understanding invariants makes you bulletproof to edge cases.

3. **Immutability has a cost:** String concatenation in a loop is O(n²) in most languages. Builders are mandatory.

4. **Stacks enforce discipline:** LIFO matching works for brackets because of strict nesting. Use stacks when order matters.

5. **Two-pointer patterns are universal:** Same-direction, opposite-direction, sliding windows—these are templates you'll use everywhere.

6. **Transformations need defensive parsing:** Always check boundaries (overflow, empty input, non-numeric characters). One forgotten guard clause crashes production.

7. **Hashing scales pattern matching:** Rolling hash reduces O(nm) to O(n+m) by exploiting polynomial structure. Probability theory makes rare collisions acceptable.

---

## 🔄 How Week 06 Connects to Surrounding Weeks

**From Week 05 (Tier 1 Patterns):**
- Monotonic stack taught you to use stacks for structure → Day 3 uses stacks for brackets
- Hash patterns taught you lookup tables → Day 5 uses hashing for pattern matching
- Two-pointer taught you pointer movement → Days 1-2 extend this to substrings

**To Week 07 (Trees):**
- Tree traversals use similar two-pointer concepts (left/right child)
- Tree patterns use stack-like recursion (DFS)
- Balanced trees use similar "maintain invariant" thinking

**To Week 10 (Dynamic Programming):**
- Many string DP problems (edit distance, LCS) use substring windows from Day 2
- Palindrome partitioning (Day 1 extension) is a DP classic
- Character counting (Day 2) appears in DP state design

---

## ✅ Weekly Checklist

By end of week, you should be able to:

- [ ] Explain expand-around-center and why it finds longest palindromes
- [ ] Implement sliding window with variable window size
- [ ] Draw a stack trace for bracket matching
- [ ] Explain why naive string concatenation is O(n²) and fix it with StringBuilder
- [ ] Implement Rabin-Karp rolling hash (optional, but try it)
- [ ] Recognize any string problem and map it to one of 5 patterns
- [ ] Solve 3 problems from each day (15+ problems total)
- [ ] Trace through at least one problem per pattern without running code
- [ ] Explain trade-offs (space vs time) for each approach
- [ ] List real-world systems that use each pattern (compiler, JSON parser, firewall, etc.)

---

## 🎓 Mastery Signals

You've mastered Week 06 when:

1. **You can recognize patterns instantly.** Someone shows you a problem; you immediately see "this is expand-around-center" or "this needs a sliding window."

2. **You code confidently without second-guessing.** Implement a two-pointer solution without constantly re-reading the logic.

3. **You optimize proactively.** Instead of writing naive O(n²) code first, you write O(n) code directly.

4. **You handle edge cases first.** Before writing core logic, you list and guard against boundary conditions.

5. **You teach others.** You can explain why palindrome patterns work and when to use them without looking at notes.

6. **You trace manually.** You can trace code on paper and predict output without running it.

---

**Next:** Week 06 Summary & Key Concepts  
**Time to Completion:** 12-18 hours core learning


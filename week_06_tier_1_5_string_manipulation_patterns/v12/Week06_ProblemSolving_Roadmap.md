# 🗺️ Week 06 Problem-Solving Roadmap: Strategy & Progressive Ladder

**Status:** Training Coach Resource  
**Audience:** Students working through Week 06 practice problems  
**Purpose:** Guide systematic problem-solving progression and pattern application  
**Focus:** From canonical problems → variations → integration

---

## 🧭 Week 06 Problem-Solving Strategy

### The Three-Stage Progression

**Stage 1: Canonical (Foundation)**
- Solve the classic LeetCode problem for each pattern
- Goal: internalize the core technique
- Time per problem: 20-30 minutes
- Success criteria: understand why this pattern works, can trace manually

**Stage 2: Variations (Building Flexibility)**
- Solve 2-3 variations of each pattern with different constraints
- Goal: recognize when to apply the pattern despite problem variations
- Time per problem: 15-20 minutes  
- Success criteria: identify pattern faster, code with fewer bugs

**Stage 3: Integration (Mastery)**
- Solve problems mixing multiple patterns or adding system design
- Goal: recognize when to combine patterns, optimize for scale
- Time per problem: 30-45 minutes
- Success criteria: solve fluently, explain trade-offs, discuss real-world impact

---

## 📊 Pattern Templates & Mental Models

### Template 1: Expand-Around-Center (Palindrome)

**Pattern Purpose:** Find palindromic substrings by expanding from each center.

**Mental Model:** "Two pointers diverging outward while maintaining symmetry."

**Core Skeleton:**
```
For each center (odd and even):
  1. Expand left and right while chars match
  2. Track longest palindrome
Return longest
```

**When to Use:**
- Find longest palindromic substring
- Count palindromic substrings
- Detect if string is palindrome

**C# Notes:**
- Use `Math.Min()` to track longest span
- Check both `center` and `center+1` for odd/even cases
- Expand in a helper method to avoid code duplication

---

### Template 2: Sliding Window (Substring)

**Pattern Purpose:** Find substrings matching constraints using variable-size windows.

**Mental Model:** "Window expands when good, shrinks when bad."

**Core Skeleton:**
```
left = 0
for right = 0 to n-1:
  Expand: add s[right] to window
  While constraint violated:
    Shrink: remove s[left] from window
    left++
  Process current window if valid
Return best window
```

**When to Use:**
- Longest substring without repeating characters
- Minimum window substring
- At-most-K distinct characters
- Character replacement within K changes

**C# Notes:**
- Use `Dictionary<char, int>` for frequency tracking
- Shrink immediately when constraint violated
- Track left and right boundaries separately

---

### Template 3: Stack-Based Matching (Brackets)

**Pattern Purpose:** Validate nesting and find valid segments using LIFO structure.

**Mental Model:** "Stack holds unmatched opens; closes match against top."

**Core Skeleton:**
```
stack = new Stack()
for each char:
  if opening:
    Push char
  else (closing):
    if stack empty or top doesn't match:
      Return invalid
    Pop
Return stack empty
```

**When to Use:**
- Valid parentheses checking
- Longest valid parentheses substring
- Generate valid parentheses combinations
- Remove minimum characters to make valid

**C# Notes:**
- Always check `stack.Count > 0` before peeking/popping
- Use `Dictionary<char, char>` for closing → opening mapping
- For longest valid: use stack indices instead of characters

---

### Template 4: StringBuilder Building (Transformation)

**Pattern Purpose:** Build strings efficiently without O(n²) concatenation.

**Mental Model:** "Accumulate in mutable buffer; convert to immutable at end."

**Core Skeleton:**
```
builder = new StringBuilder()
for each unit to process:
  Append to builder
Return builder.ToString()
```

**When to Use:**
- Parse user input (atoi, validation)
- Convert between formats (integer to Roman)
- Build encoded representations
- Any string concatenation loop

**C# Notes:**
- Always use `StringBuilder` for loops with concatenation
- Reserve capacity if size is known: `new StringBuilder(capacity)`
- Call `ToString()` only once at the end

---

### Template 5: Rolling Hash (Pattern Matching)

**Pattern Purpose:** Match patterns using polynomial hashing for O(1) updates.

**Mental Model:** "Remove old, shift, add new in O(1) arithmetic."

**Core Skeleton:**
```
Precompute: patternHash, basePower
Compute: first window hash
For each position:
  Roll: remove left, multiply by base, add right
  If hash matches:
    Verify actual strings
    Record match if verified
```

**When to Use:**
- Multiple patterns in same text
- Plagiarism detection
- DNA sequence matching
- Pattern occurrence counting

**C# Notes:**
- Use `long` for hash values (prevent overflow)
- Apply `% modulus` after each operation
- Use `(hash - left*basePower + MOD) % MOD` for subtraction to handle negatives

---

## 🏋️ Progressive Problem Ladder

### STAGE 1: CANONICAL PROBLEMS (Foundation)

#### Day 1: Palindrome Patterns — Canonical

| Problem | Difficulty | Pattern | C# Focus | Time Target |
| :--- | :--- | :--- | :--- | :--- |
| LeetCode #9: Palindrome Number | 🟢 Easy | Two-pointer check | Reverse number or string | 10 min |
| LeetCode #125: Valid Palindrome | 🟢 Easy | Two-pointer, skip non-alphanumeric | Handle case + non-chars | 15 min |
| LeetCode #5: Longest Palindromic Substring | 🟡 Medium | Expand-around-center | Both odd/even centers | 25 min |

#### Day 2: Substring Sliding Windows — Canonical

| Problem | Difficulty | Pattern | C# Focus | Time Target |
| :--- | :--- | :--- | :--- | :--- |
| LeetCode #3: Longest Substring Without Repeating | 🟡 Medium | Variable window | Dict tracking | 20 min |
| LeetCode #438: Find All Anagrams | 🟡 Medium | Fixed window, frequency | Two-pointer with array | 25 min |
| LeetCode #76: Minimum Window Substring | 🔴 Hard | Variable window, two-pointer | Dict + frequency logic | 35 min |

#### Day 3: Bracket Matching — Canonical

| Problem | Difficulty | Pattern | C# Focus | Time Target |
| :--- | :--- | :--- | :--- | :--- |
| LeetCode #20: Valid Parentheses | 🟢 Easy | Stack validation | Three bracket types | 15 min |
| LeetCode #32: Longest Valid Parentheses | 🟡 Medium | Stack indices | Track matching pairs | 30 min |
| LeetCode #22: Generate Parentheses | 🟡 Medium | Backtracking + stack | Build valid combinations | 25 min |

#### Day 4: String Transformations — Canonical

| Problem | Difficulty | Pattern | C# Focus | Time Target |
| :--- | :--- | :--- | :--- | :--- |
| LeetCode #8: String to Integer (atoi) | 🟡 Medium | Parsing, overflow | Handle sign + overflow | 25 min |
| LeetCode #12: Integer to Roman | 🟡 Medium | Greedy mapping | Descending value order | 20 min |
| LeetCode #443: String Compression | 🟡 Medium | In-place, two-pointer | RLE encoding | 20 min |

#### Day 5: Advanced Pattern Matching — Canonical (Optional)

| Problem | Difficulty | Pattern | C# Focus | Time Target |
| :--- | :--- | :--- | :--- | :--- |
| LeetCode #28: Implement strstr() | 🟢 Easy | Pattern matching | Naive or Karp-Rabin | 15 min |
| LeetCode #187: Repeated DNA Sequences | 🟡 Medium | Rolling hash | Substring hashing | 25 min |

---

### STAGE 2: VARIATIONS (Flexibility)

#### Day 1 Variations: Palindrome Patterns

| Problem | Difficulty | Variation | C# Focus | Time Target |
| :--- | :--- | :--- | :--- | :--- |
| LeetCode #131: Palindrome Partitioning | 🟡 Medium | Partition into palindromes | Backtracking + check | 20 min |
| LeetCode #234: Palindrome Linked List | 🟡 Medium | Check in linked list | Fast/slow pointers | 20 min |
| LeetCode #647: Palindromic Substrings | 🟡 Medium | Count all palindromes | Expand center + count | 15 min |

#### Day 2 Variations: Substring Sliding Windows

| Problem | Difficulty | Variation | C# Focus | Time Target |
| :--- | :--- | :--- | :--- | :--- |
| LeetCode #159: Longest Substring with At Most 2 Distinct | 🟡 Medium | K=2 distinct chars | Dict + tracking | 20 min |
| LeetCode #567: Permutation in String | 🟡 Medium | Anagram of pattern | Frequency matching | 20 min |
| LeetCode #424: Longest Repeating Character Replacement | 🟡 Medium | With K changes allowed | Frequency + tolerance | 25 min |

#### Day 3 Variations: Bracket Matching

| Problem | Difficulty | Variation | C# Focus | Time Target |
| :--- | :--- | :--- | :--- | :--- |
| LeetCode #301: Remove Invalid Parentheses | 🔴 Hard | Minimum removals | BFS or backtracking | 30 min |
| LeetCode #1541: Minimum Insertions to Balance Parentheses | 🟡 Medium | Insertions to fix | Greedy + balance tracking | 25 min |

#### Day 4 Variations: String Transformations

| Problem | Difficulty | Variation | C# Focus | Time Target |
| :--- | :--- | :--- | :--- | :--- |
| LeetCode #68: Text Justification | 🔴 Hard | Complex layout | StringBuilder + spacing | 40 min |
| LeetCode #65: Valid Number | 🔴 Hard | All edge cases | State machine parsing | 30 min |

---

### STAGE 3: INTEGRATION (Mastery)

#### Mixed-Pattern Problems

| Problem | Difficulty | Patterns | C# Focus | Time Target |
| :--- | :--- | :--- | :--- | :--- |
| LeetCode #214: Shortest Palindrome | 🔴 Hard | Palindrome + string building | KMP or rolling hash | 35 min |
| LeetCode #394: Decode String | 🟡 Medium | Brackets + stack + building | Nested parsing | 30 min |
| LeetCode #30: Substring with Concatenation | 🔴 Hard | Window + multiple patterns | Hashing combinations | 35 min |

#### System Design Integration

| Scenario | Patterns | C# Considerations | Time Target |
| :--- | :--- | :--- | :--- |
| Autocomplete suggestion system | Palindrome + substring + transformation | Dict lookup, prefix trees | Research |
| Real-time code formatter | Brackets + transformation + validation | StringBuilder efficiency | Research |
| Plagiarism detector | Pattern matching + hashing + optimization | Multi-threading, corpus indexing | Research |

---

## 🧪 Common Problem-Solving Pitfalls (and Fixes)

### Pitfall 1: Choosing the Wrong Pattern
**Symptom:** You code for 30 minutes only to realize you picked wrong pattern.

**Quick Fix:** Before coding, answer: "Is this about (A) symmetry, (B) constraints, (C) nesting, (D) transformation, or (E) scale?"

**Prevention:** Spend 2 minutes identifying pattern before touching keyboard.

---

### Pitfall 2: Off-by-One Errors in Windows
**Symptom:** Code runs but gives wrong substrings (off by one character).

**Quick Fix:** Trace through with indices on paper BEFORE coding. Verify loop bounds: `left < n`, `right < n`, etc.

**Prevention:** Manually trace through "abc" before running code.

---

### Pitfall 3: Stack Underflow in Bracket Matching
**Symptom:** Exception when popping empty stack.

**Quick Fix:** Always check `stack.Count > 0` before popping. Guard clauses first.

**Prevention:** Add guard clause as first statement in closing-bracket handler.

---

### Pitfall 4: Integer Overflow in Parsing
**Symptom:** Overflow silently corrupts result (or throws unhandled exception).

**Quick Fix:** Check bounds before adding: `if (result > INT_MAX / 10) throw`.

**Prevention:** Implement overflow check BEFORE main parsing loop.

---

### Pitfall 5: Modulo Issues in Rolling Hash
**Symptom:** Negative numbers or incorrect hash values.

**Quick Fix:** Use `(value % MOD + MOD) % MOD` for all operations (ensures positive result).

**Prevention:** Copy formula from reference; don't improvise modulo arithmetic.

---

## 📈 Daily Practice Structure

### **Day 1 Session (2 hours)**
- [ ] Read expand-around-center concept
- [ ] Trace through 1 example manually
- [ ] Solve LeetCode #5 (longest palindrome)
- [ ] Code 2-3 additional palindrome problems
- [ ] Discuss approach with partner

### **Day 2 Session (2-2.5 hours)**
- [ ] Read sliding window concept
- [ ] Trace through variable window example
- [ ] Solve LeetCode #3 (longest without repeating)
- [ ] Code LeetCode #76 (minimum window)
- [ ] Attempt 1 variation problem

### **Day 3 Session (2 hours)**
- [ ] Review stack invariant for brackets
- [ ] Trace bracket validation manually
- [ ] Solve LeetCode #20 (valid parentheses)
- [ ] Code LeetCode #32 (longest valid)
- [ ] Discuss with partner

### **Day 4 Session (2-2.5 hours)**
- [ ] Review StringBuilder vs concatenation
- [ ] Implement atoi parsing carefully
- [ ] Solve LeetCode #8 and #12
- [ ] Trace overflow detection manually
- [ ] Code 1-2 transformation problems

### **Day 5 Session (1.5-2 hours, optional)**
- [ ] Read rolling hash concept
- [ ] Manually compute hash for small string
- [ ] Solve LeetCode #28 (strstr)
- [ ] Code LeetCode #187 (repeated DNA)
- [ ] Trace Karp-Rabin step-by-step

### **Weekend Integration (2-3 hours)**
- [ ] Review all 5 patterns
- [ ] Solve 1 mixed-pattern problem
- [ ] Discuss real-world applications
- [ ] Plan Week 07 preview

---

## ✅ Weekly Problem Count

**Minimum (Core Interview Prep):**
- Canonical only: 12-15 problems
- Stage 1 mastery only
- Time: 10-12 hours

**Recommended (Solid Preparation):**
- Canonical + 2-3 variations per day: 25-30 problems
- Stages 1-2 mastery
- Time: 18-22 hours

**Mastery (Deep Learning):**
- All stages including integration: 40+ problems
- Can tackle unseen problems with confidence
- Time: 25-30 hours

---

## 🎓 Progression Checklist

**Week End Readiness:**

- [ ] Solved all 12-15 canonical problems
- [ ] Can implement each pattern without reference
- [ ] Trace patterns manually on paper without running code
- [ ] Recognize patterns in variations instantly
- [ ] Explain why each pattern works
- [ ] Discuss space-time trade-offs
- [ ] Handle edge cases proactively
- [ ] Code fluently without syntax errors
- [ ] Estimate complexity without calculation
- [ ] Connect patterns to real systems

---

**Status:** Week 06 Problem-Solving Roadmap Complete  
**Next:** Week 06 Daily Progress Checklist  
**Review Time:** 1 hour (overview) + 20-30 hours (practice problems)
# Week 06 Practice Problems & Solutions Strategy

**Week:** 06 | **Total Problems:** 39 LeetCode + 8 Custom | **Difficulty Spread:** Easy (8), Medium (24), Hard (15)

---

## 🎯 PROBLEM MAPPING BY DAY

### Day 1: Palindrome Patterns (8 Problems)

| # | LeetCode | Title | Difficulty | Key Concept | Time | Space |
|---|----------|-------|-----------|-------------|------|-------|
| 1 | #125 | Valid Palindrome | 🟢 Easy | Two-pointer with filtering | O(n) | O(1) |
| 2 | #5 | Longest Palindromic Substring | 🟡 Medium | Expand-around-center | O(n²) | O(1) |
| 3 | #647 | Palindromic Substrings | 🟡 Medium | Count all palindromes | O(n²) | O(1) |
| 4 | #1332 | Remove Palindromic Subsequences | 🟡 Medium | Observation-based | O(n) | O(1) |
| 5 | #516 | Longest Palindromic Subsequence | 🔴 Hard | DP + LCS | O(n²) | O(n²) |
| 6 | #1216 | Valid Palindrome III | 🔴 Hard | DP variant | O(n²) | O(n²) |
| 7 | #131 | Palindrome Partitioning | 🔴 Hard | Backtracking + DP | O(n × 2^n) | O(n) |
| 8 | #336 | Palindrome Pairs | 🔴 Hard | Hash + two-pointer | O(n²k) | O(nk) |

**Problem Progression Strategy:**
- Start with #125 (warmup, character filtering)
- Move to #5 (core expand-around-center)
- Extend to #647 (counting variant)
- Jump to #131 (DP integration)

---

### Day 2: Substring Sliding Window (10 Problems)

| # | LeetCode | Title | Difficulty | Key Concept | Time | Space |
|---|----------|-------|-----------|-------------|------|-------|
| 1 | #3 | Longest Substring Without Repeating | 🟡 Medium | Variable window, char map | O(n) | O(k) |
| 2 | #340 | Longest Substring with K Distinct | 🟡 Medium | Window shrinking logic | O(n) | O(k) |
| 3 | #567 | Permutation in String | 🟡 Medium | Fixed-size window | O(n) | O(k) |
| 4 | #30 | Substring with Concatenation | 🔴 Hard | Multi-word window | O(nm) | O(n) |
| 5 | #76 | Minimum Window Substring | 🔴 Hard | Constraint satisfaction | O(n) | O(k) |
| 6 | #424 | Longest Repeating Char Replacement | 🟡 Medium | Replacement budget | O(n) | O(k) |
| 7 | #1004 | Max Consecutive Ones III | 🟡 Medium | Binary window | O(n) | O(1) |
| 8 | #209 | Minimum Size Subarray Sum | 🟡 Medium | Numeric constraint | O(n) | O(1) |
| 9 | #438 | Find All Anagrams | 🟡 Medium | Rolling frequency | O(n) | O(k) |
| 10 | #1695 | Max Erasure Value | 🟡 Medium | Max sum with constraint | O(n) | O(k) |

**Problem Progression Strategy:**
- Warm up: #3 (signature problem)
- Expand: #340 (variable shrinking)
- Multi-pattern: #567 (fixed anagrams)
- Advanced: #76 (minimum window, hardest)

---

### Day 3: Parentheses & Bracket Matching (8 Problems)

| # | LeetCode | Title | Difficulty | Key Concept | Time | Space |
|---|----------|-------|-----------|-------------|------|-------|
| 1 | #20 | Valid Parentheses | 🟢 Easy | Stack validation | O(n) | O(n) |
| 2 | #32 | Longest Valid Parentheses | 🔴 Hard | Stack indices or DP | O(n) | O(n) |
| 3 | #301 | Remove Invalid Parentheses | 🔴 Hard | BFS + state exploration | O(2^n) | O(2^n) |
| 4 | #22 | Generate Parentheses | 🟡 Medium | Backtracking | O(Catalan) | O(n) |
| 5 | #1541 | Minimum Additions for Valid | 🟡 Medium | Greedy counting | O(n) | O(1) |
| 6 | #1614 | Maximum Nesting Depth | 🟢 Easy | Counter/stack | O(n) | O(1) |
| 7 | #1003 | Check if Word is Valid | 🟢 Easy | Stack with replacement | O(n) | O(n) |
| 8 | #1249 | Minimum Remove Invalid Parens | 🟡 Medium | Two-pass or stack | O(n) | O(n) |

**Problem Progression Strategy:**
- Foundation: #20 (mandatory, < 5 min)
- Core: #32 (signature problem)
- Advanced: #301 (optimization)
- Creative: #22 (generation)

---

### Day 4: String Transformations (8 Problems)

| # | LeetCode | Title | Difficulty | Key Concept | Time | Space |
|---|----------|-------|-----------|-------------|------|-------|
| 1 | #8 | String to Integer (atoi) | 🟡 Medium | Parsing + overflow | O(n) | O(1) |
| 2 | #12 | Integer to Roman | 🟡 Medium | Greedy table | O(log n) | O(1) |
| 3 | #13 | Roman to Integer | 🟡 Medium | Reverse mapping | O(n) | O(1) |
| 4 | #6 | Zigzag Conversion | 🟡 Medium | Index formula | O(n) | O(n) |
| 5 | #443 | String Compression RLE | 🟡 Medium | In-place modification | O(n) | O(1) |
| 6 | #68 | Text Justification | 🔴 Hard | Layout logic | O(n) | O(n) |
| 7 | #271 | Encode and Decode Strings | 🟡 Medium | Delimiter handling | O(n) | O(n) |
| 8 | #468 | Valid IP Address | 🔴 Hard | Parsing + validation | O(n) | O(1) |

**Problem Progression Strategy:**
- Foundation: #8 (parsing, edge cases)
- Table-based: #12, #13 (mapping)
- Layout: #6, #68 (formula-based)
- Advanced: #468 (validation complexity)

---

### Day 5: Advanced String Matching (7 Problems)

| # | LeetCode | Title | Difficulty | Key Concept | Time | Space |
|---|----------|-------|-----------|-------------|------|-------|
| 1 | #187 | Repeated DNA Sequences | 🟡 Medium | Rolling hash | O(n) | O(n) |
| 2 | #438 | Find All Anagrams | 🟡 Medium | Sliding window freq | O(n) | O(k) |
| 3 | #686 | Repeated String Match | 🟡 Medium | Pattern repetition | O(n+m) | O(n) |
| 4 | #214 | Shortest Palindrome | 🔴 Hard | Karp-Rabin for palindrome | O(n) | O(n) |
| 5 | #30 | Substring with Concatenation | 🔴 Hard | Multi-pattern hash | O(nm) | O(n) |
| 6 | #482 | License Key Formatting | 🟢 Easy | Character transformation | O(n) | O(n) |
| 7 | #28 | Implement strstr() | 🟢 Easy | Basic pattern match | O(nm) or O(n+m) | O(1) |

**Problem Progression Strategy:**
- Warm up: #482, #28 (basic)
- Core: #187 (rolling hash signature)
- Multi-pattern: #438, #30 (extension)
- Advanced: #214 (Karp-Rabin for palindrome)

---

## 📈 PRACTICE PROGRESSION PATH (Recommended)

### Week 1: Foundation (Days 1-2)
**Goal:** Comfortable with basic patterns

**Daily Target:**
- Day 1: #125, #5, #647 (3 problems, 1.5 hours)
- Day 2: #3, #340, #567 (3 problems, 1.5 hours)

**Success Criteria:**
- Can solve without looking at hints
- Can explain approach clearly
- Can trace through example

### Week 2: Core Mastery (Days 3-4)
**Goal:** Fluent implementation and trade-off understanding

**Daily Target:**
- Day 3: #20, #32, #22 (3 problems, 2 hours)
- Day 4: #8, #12, #6 (3 problems, 1.5 hours)

**Success Criteria:**
- Can code without referencing notes
- Can handle edge cases automatically
- Can discuss alternatives

### Week 3: Advanced & Integration (Day 5 + Mixed)
**Goal:** System-level thinking and optimization

**Daily Target:**
- Day 5: #187, #438, #214 (3 problems, 2 hours)
- Integration: #76 (combines Days 2-3), #32 (combines Days 3-4) (2 hours)

**Success Criteria:**
- Can optimize from naive to efficient
- Can recognize patterns in new problems
- Can design systems using these techniques

---

## 🧪 CUSTOM PRACTICE PROBLEMS (Optional)

### Custom 1: Palindrome Finder
**Problem:** Given text with annotations, find all palindromes ignoring non-letters
**Example:** "A man, a plan, a canal: Panama"
**Solution:** Iterate with two-pointer, skip non-letters

### Custom 2: Sliding Window Edge Cases
**Problem:** Find longest substring with sum = k, handling negative numbers
**Example:** [-2, 1, -3, 4, 5], k=4
**Solution:** Use prefix sum + hash map

### Custom 3: Bracket Nesting Depth
**Problem:** Find maximum nesting depth of brackets (all types mixed)
**Example:** "a(b(c)d(e(f)g)h)i"
**Solution:** Stack-based, track maximum height

### Custom 4: String Transformation Chain
**Problem:** Transform integers to words, then to reverse, with validation
**Example:** 123 → "one-two-three" → "eerht-owt-eno"
**Solution:** Chain transformations with error handling

### Custom 5: Plagiarism Detector
**Problem:** Find longest common substring between two documents
**Example:** doc1="thequickbrownfox", doc2="thequickwhitefox"
**Solution:** Rolling hash or DP, with efficiency considerations

---

## ✅ PROBLEM SOLVING CHECKLIST

### For Each Problem
- [ ] Understand what the problem is asking
- [ ] Identify the pattern (palindrome, window, bracket, transform, matching)
- [ ] Write a brute-force solution first
- [ ] Optimize step-by-step
- [ ] Trace through examples (typical, edge case, large)
- [ ] Implement with proper error handling
- [ ] Test and debug systematically
- [ ] Analyze time and space complexity
- [ ] Discuss trade-offs and alternatives

### When Stuck
1. Reread the problem statement carefully
2. Sketch the example by hand
3. Identify the subproblem
4. Check if a simpler variant exists
5. Review similar problems from the same day
6. Read the problem's hint section
7. Look at solution explanation (read, don't copy)

---

## 📊 PROBLEM DIFFICULTY EXPECTATIONS

**Easy (8 problems):** 5-10 minutes each
- Should be solvable in interview without hints
- Tests basic understanding and implementation

**Medium (24 problems):** 15-30 minutes each
- Requires optimization or combination of techniques
- Tests problem-solving and complexity analysis

**Hard (15 problems):** 30-60 minutes each
- Combines multiple concepts or introduces new angles
- Tests systems thinking and edge case handling

---

**Version:** 1.0 | **Status:** ✅ Production Ready | **Generated:** January 10, 2026
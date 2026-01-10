# ✅ Week 06 Daily Progress Checklist: Daily Actions & Reflection

**Status:** Daily Planner & Self-Assessment Tool  
**Audience:** Students working through Week 06 day-by-day  
**Purpose:** Track daily progress, consolidate learning, maintain momentum  
**How to Use:** Check off each item, reflect on learnings, adjust pace as needed

---

## 📅 MONDAY: Day 1 — Palindrome Patterns

**🎯 Learning Goal:** Understand symmetry detection using expand-around-center.

### Morning (Concepts)
- [ ] Read Week 06 Day 1 instructional (Chapter 1-2)
- [ ] Understand expand-around-center analogy (two mirrors)
- [ ] Draw 3 palindromes showing centers (both odd & even)
- [ ] Note key invariant: "mirror property maintained as pointers move"

**Reflection:** What makes expand-around-center elegant? Why two pointer directions?

### Midday (Mechanics & Traces)
- [ ] Read Week 06 Day 1 instructional (Chapter 3)
- [ ] Manually trace LeetCode #5 on "babad"
- [ ] Trace on "ac" (no palindrome, edge case)
- [ ] Predict output before running code

**Reflection:** Did manual trace match expected output? What surprised you?

### Afternoon (Practice)
- [ ] Solve LeetCode #9 (Palindrome Number) — easy warm-up
- [ ] Solve LeetCode #125 (Valid Palindrome) — handle non-alphanumeric
- [ ] Code your own expand-around-center from memory (no reference)

**Reflection:** Which was harder: removing non-alphanumeric or handling even-length centers?

### Evening (Integration & Concepts)
- [ ] Read Week 06 Day 1 (Chapter 4-5: Real systems, cognitive lenses)
- [ ] List 3 real-world applications of palindrome detection
- [ ] Review palindrome mistakes from "Misconceptions" section
- [ ] Write one sentence: "The key insight about palindromes is..."

**Reflection:** How does this connect to DNA analysis or compression?

### Daily Reflection Prompts
1. On a scale 1-10, how confident are you with expand-around-center? ___
2. Did you struggle with odd vs even-length centers? If yes, spend 10 more minutes on this.
3. Can you explain to a friend why we check both 2n-1 centers?
4. What was your biggest "aha" moment today?

### Concepts to Master Today
- [ ] Palindrome definition and detection
- [ ] Expand-around-center algorithm
- [ ] Odd-length vs even-length centers
- [ ] Time complexity O(n²) and why
- [ ] Space complexity O(1) and why
- [ ] Real-world DNA matching application

---

## 📅 TUESDAY: Day 2 — Substring Sliding Windows

**🎯 Learning Goal:** Master variable-size windows for substring constraints.

### Morning (Concepts)
- [ ] Read Week 06 Day 2 instructional (Chapter 1-2)
- [ ] Understand "expand-shrink" rhythm
- [ ] Draw a window on "aabcabcbb" showing expand/shrink phases
- [ ] Note key invariant: "window[left..right] is best window so far"

**Reflection:** What's the difference between fixed-size and variable-size windows?

### Midday (Mechanics & Traces)
- [ ] Read Week 06 Day 2 instructional (Chapter 3)
- [ ] Manually trace LeetCode #3 on "abcabcbb" (longest without repeating)
- [ ] Trace on "au" and "dvdf" (edge cases)
- [ ] Draw frequency map updates step-by-step

**Reflection:** When did you expand vs shrink? Why?

### Afternoon (Practice)
- [ ] Solve LeetCode #3 (Longest Substring Without Repeating)
- [ ] Solve LeetCode #438 (Find All Anagrams) — fixed window variant
- [ ] Attempt LeetCode #76 (Minimum Window Substring) — harder variant

**Reflection:** Which problem was hardest and why? Revisit that one.

### Evening (Integration & Concepts)
- [ ] Read Week 06 Day 2 (Chapter 4-5: Autocomplete systems, cognitive lenses)
- [ ] List 3 sliding window applications
- [ ] Review common sliding window mistakes
- [ ] Write one sentence: "The key insight about sliding windows is..."

**Reflection:** How would you use this for autocomplete or plagiarism detection?

### Daily Reflection Prompts
1. On a scale 1-10, how confident are you with sliding windows? ___
2. Do you intuitively know when to expand vs shrink?
3. Can you implement without looking at notes?
4. What's one edge case you didn't consider initially?

### Concepts to Master Today
- [ ] Variable-size sliding window
- [ ] Expand-shrink decision logic
- [ ] Frequency map tracking
- [ ] Dictionary or HashSet usage
- [ ] Time complexity O(n) amortized
- [ ] "At-most-K" constraint pattern
- [ ] Autocomplete system application

---

## 📅 WEDNESDAY: Day 3 — Parentheses & Bracket Matching

**🎯 Learning Goal:** Learn stack discipline for nested structures.

### Morning (Concepts)
- [ ] Read Week 06 Day 3 instructional (Chapter 1-2)
- [ ] Understand LIFO matching principle
- [ ] Draw stack evolution for "(())" and "([)]"
- [ ] Note key invariant: "stack contains only unmatched opens"

**Reflection:** Why does LIFO (stack) work for brackets but FIFO (queue) doesn't?

### Midday (Mechanics & Traces)
- [ ] Read Week 06 Day 3 instructional (Chapter 3)
- [ ] Manually trace LeetCode #20 on "()", "([)]", "())"
- [ ] Show stack state at each step
- [ ] Predict where each fails

**Reflection:** Did you anticipate stack underflow or top-mismatch errors?

### Afternoon (Practice)
- [ ] Solve LeetCode #20 (Valid Parentheses) — warm-up
- [ ] Solve LeetCode #32 (Longest Valid Parentheses) — use stack indices
- [ ] Solve LeetCode #22 (Generate Parentheses) — backtracking variant

**Reflection:** Was index tracking in #32 confusing? Retrace that.

### Evening (Integration & Concepts)
- [ ] Read Week 06 Day 3 (Chapter 4-5: Compiler design, real systems)
- [ ] List 3 bracket matching applications (compiler, JSON, editor)
- [ ] Review bracket mistakes and guard clauses
- [ ] Write one sentence: "The key insight about brackets is..."

**Reflection:** How does this relate to expression evaluation or parsing?

### Daily Reflection Prompts
1. On a scale 1-10, how confident are you with stack-based bracket matching? ___
2. Did you forget to check `stack.Count > 0` before popping? If yes, add that guard clause to your mental checklist.
3. Can you explain why "{[}]" is invalid?
4. What's the hardest variant: checking, finding longest, or generating?

### Concepts to Master Today
- [ ] Stack-based validation
- [ ] LIFO discipline and why it works
- [ ] Three bracket types
- [ ] Guard clauses (empty stack check)
- [ ] Index tracking for longest valid
- [ ] Backtracking for generation
- [ ] Compiler syntax checking application

---

## 📅 THURSDAY: Day 4 — String Transformations & Building

**🎯 Learning Goal:** Master efficient string building and parsing.

### Morning (Concepts)
- [ ] Read Week 06 Day 4 instructional (Chapter 1-2)
- [ ] Understand StringBuilder vs concatenation
- [ ] Calculate: 1000 concatenations naive vs builder
- [ ] Understand atoi parsing with overflow detection

**Reflection:** Why does string immutability make concatenation expensive?

### Midday (Mechanics & Traces)
- [ ] Read Week 06 Day 4 instructional (Chapter 3)
- [ ] Manually parse "  -42  xyz" → -42 (atoi)
- [ ] Check overflow detection: would "9999999999" overflow?
- [ ] Trace Roman numeral: 1994 → "MCMXCIV"

**Reflection:** Did you catch the boundary cases (sign, overflow, non-numeric)?

### Afternoon (Practice)
- [ ] Solve LeetCode #8 (String to Integer) — careful parsing
- [ ] Solve LeetCode #12 (Integer to Roman) — greedy mapping
- [ ] Solve LeetCode #443 (String Compression) — RLE encoding

**Reflection:** Which required most careful boundary checking?

### Evening (Integration & Concepts)
- [ ] Read Week 06 Day 4 (Chapter 4-5: Logging systems, real-world impact)
- [ ] Calculate: naive O(n²) vs builder O(n) for 10 million log entries
- [ ] List 3 transformation applications
- [ ] Write one sentence: "The key insight about transformations is..."

**Reflection:** How would you refactor a logging system using StringBuilder?

### Daily Reflection Prompts
1. On a scale 1-10, how confident are you with transformations? ___
2. Can you spot O(n²) concatenation traps immediately?
3. Do you always check overflow before adding?
4. What's the trickiest edge case: empty input, single char, boundary value?

### Concepts to Master Today
- [ ] String builders for efficiency
- [ ] Parsing defensively (sign, overflow, edge cases)
- [ ] Greedy mapping for format conversion
- [ ] Boundary checking before operations
- [ ] O(n²) vs O(n) complexity difference
- [ ] Real-world logging and serialization
- [ ] Guard clauses for parsing

---

## 📅 FRIDAY: Day 5 (Optional Advanced) — Advanced Pattern Matching

**🎯 Learning Goal:** Understand rolling hash and Karp-Rabin algorithm.

### Morning (Concepts)
- [ ] Read Week 06 Day 5 instructional (Chapter 1-2)
- [ ] Understand polynomial rolling hash
- [ ] Draw "remove-shift-add" formula for rolling
- [ ] Understand collision probability with large primes

**Reflection:** Why does rolling update happen in O(1)?

### Midday (Mechanics & Traces)
- [ ] Read Week 06 Day 5 instructional (Chapter 3)
- [ ] Manually compute rolling hash for "ABCDE" finding "BC"
- [ ] Show hash at each window position
- [ ] Verify hash match leads to character verification

**Reflection:** Did you handle modulo arithmetic correctly (negative numbers)?

### Afternoon (Practice — Optional)
- [ ] Solve LeetCode #28 (Implement strstr()) — naive or Karp-Rabin
- [ ] Solve LeetCode #187 (Repeated DNA Sequences) — rolling hash
- [ ] Solve LeetCode #686 (Repeated String Match) — pattern search

**Reflection:** Is rolling hash faster than naive on large texts?

### Evening (Integration & Concepts)
- [ ] Read Week 06 Day 5 (Chapter 4-5: Turnitin, DNA matching, real systems)
- [ ] List 3 rolling hash applications
- [ ] Compare Karp-Rabin, KMP, Boyer-Moore
- [ ] Write one sentence: "The key insight about rolling hash is..."

**Reflection:** When would you choose Karp-Rabin over KMP?

### Daily Reflection Prompts
1. On a scale 1-10, how confident are you with rolling hash? ___ (Optional, so OK if lower)
2. Do you understand the modulo arithmetic pitfalls?
3. Can you explain why collision probability is negligible?
4. Would you use this in production or stick to simpler algorithms?

### Concepts to Master Today (Optional)
- [ ] Polynomial representation of substrings
- [ ] Rolling hash incremental update
- [ ] Modulo arithmetic (preventing overflow, handling negatives)
- [ ] Collision probability analysis
- [ ] Karp-Rabin algorithm
- [ ] Comparison with KMP and Boyer-Moore
- [ ] Plagiarism detection and DNA matching applications

---

## 📅 SATURDAY: Integration & Review

**🎯 Learning Goal:** Connect all 5 patterns; recognize pattern types quickly.

### Morning (Pattern Recognition Drill)
- [ ] Read problem without coding: identify which pattern it is
- [ ] Do this for 10 random LeetCode string problems
- [ ] Time yourself: can you recognize in < 1 minute?

**Pattern Recognition Checklist:**
- [ ] See "longest X with property Y" → sliding window
- [ ] See "palindrome" → expand-around-center
- [ ] See "valid/balanced" → stack
- [ ] See "parse" or "build" → StringBuilder + defensive parsing
- [ ] See "multiple patterns" → rolling hash (optional)

### Midday (Trace Without Code)
- [ ] Pick 1 problem from each day
- [ ] Trace through manually on paper
- [ ] Don't run code; predict output
- [ ] Verify with code after

**Tracing Checklist:**
- [ ] Can you trace expand-around-center without code?
- [ ] Can you trace sliding window without code?
- [ ] Can you trace stack operations without code?
- [ ] Can you trace parsing/building without code?
- [ ] Can you trace rolling hash without code?

### Afternoon (Connect Patterns)
- [ ] Solve 1-2 mixed-pattern problems
- [ ] Discuss: which patterns did you combine?
- [ ] Why did you choose each pattern?

### Evening (Reflection & Planning)

**Weekly Reflection:**
- [ ] Which pattern feels most natural? Why?
- [ ] Which pattern do you still struggle with?
- [ ] What's your biggest strength this week?
- [ ] What's one area to improve next week?

**Mastery Assessment (Self-Grade 1-10):**
- [ ] Pattern recognition: ___
- [ ] Expand-around-center implementation: ___
- [ ] Sliding window implementation: ___
- [ ] Stack-based bracket matching: ___
- [ ] String transformation & building: ___
- [ ] Rolling hash (if attempted): ___
- [ ] Edge case handling: ___
- [ ] Code fluency (no syntax errors): ___

**Week Completion Checklist:**
- [ ] Solved 12-15 canonical problems
- [ ] Can implement each pattern without reference
- [ ] Traced patterns manually without running code
- [ ] Recognized patterns in variations instantly
- [ ] Explained why each pattern works
- [ ] Discussed space-time trade-offs
- [ ] Handled edge cases proactively
- [ ] Coded fluently without syntax errors
- [ ] Estimated complexity without calculation
- [ ] Connected patterns to real systems

---

## 📅 SUNDAY: Optional Deep Dives

**Choose 1-2 based on interests:**

### Option A: System Design Deep Dive
- Design an autocomplete system (combine palindrome + sliding window + optimization)
- Design a plagiarism detector (rolling hash + performance)
- Design a code formatter (brackets + transformations)

### Option B: Advanced Problem Practice
- Solve 2-3 hard problems mixing multiple patterns
- Attempt problems you skipped during the week
- Solve integration problems from problem-solving roadmap

### Option C: Theory & Research
- Study Manacher's algorithm (O(n) palindromes)
- Study KMP algorithm (guaranteed O(n+m))
- Research Aho-Corasick (multi-pattern matching)

### Option D: Rest & Consolidation
- Review notes from the week
- Create a cheat sheet of patterns
- Plan next week's focus areas
- Catch up on any missed problems

---

## 🎓 Week 06 Completion Signals

By end of week, you should feel:

✅ **Confident** recognizing patterns instantly  
✅ **Fluent** implementing without second-guessing  
✅ **Thorough** handling edge cases proactively  
✅ **Connected** explaining real-world applications  
✅ **Prepared** for string questions in interviews  

---

## 📊 Weekly Stats to Track

| Metric | Target | Your Progress |
| :--- | :--- | :--- |
| Problems solved | 15-20 | ___ |
| Patterns mastered | 5/5 | ___ |
| Edge cases caught | 80%+ | ___ |
| Code fluency (no bugs) | 90%+ | ___ |
| Manual traces (accuracy) | 95%+ | ___ |
| Pattern recognition time | < 1 min | ___ |

---

**Status:** Week 06 Daily Progress Checklist Complete  
**Total Learning Time:** 18-24 hours (with daily review and practice)  
**Interview Readiness:** Ready after completion  
**Mastery Readiness:** Ready after solving Stage 1 + Stage 2 problems
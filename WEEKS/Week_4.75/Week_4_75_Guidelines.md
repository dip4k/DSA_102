# 📖 WEEK 4.75 — GUIDELINES & LEARNING STRATEGY

**Week:** 4.75 (TIER 1.5 - String Manipulation Patterns)  
**Duration:** 4 days | ~20,000 words  
**Difficulty:** 🟡 Medium (High ROI for Interviews)  
**Total Files:** 4 instructional + 4 support files

---

## 🎯 WEEK OVERVIEW

Week 4.75 focuses entirely on **String Manipulation**. Strings are the most common data type in interviews (along with Arrays). This week bridges the gap between general patterns (Week 4.5) and specialized structures (Week 8 Tries/Suffix Trees).

**Coverage Timeline:**
- **Day 1 (2 hrs):** Palindrome Patterns (Expand Around Center)
- **Day 2 (2 hrs):** Substring & Sliding Window (String Focus)
- **Day 3 (2 hrs):** Parentheses & Bracket Matching
- **Day 4 (2 hrs):** String Transformations & Building

---

## 🔑 CORE CONCEPTS ACROSS THE WEEK

| 📌 Day | 🎯 Pattern | 💡 Key Idea | ⏱️ Complexity | 💼 Interview % |
|--------|-----------|------------|-------------|---------------|
| **1** | Palindromes | Symmetry, Center Expansion | O(n²) or O(n) | 30% |
| **2** | String Window | Frequency Maps + Window | O(n) | 35% |
| **3** | Parentheses | Stack Matching / Balance Counter | O(n) | 20% |
| **4** | Transformations | Parsing, Encoding, Compression | O(n) | 20% |

---

## 📚 LEARNING OBJECTIVES FOR THE WEEK

By the end of this week, you will:

1. ✅ **Master symmetric thinking** (Palindromes).
2. ✅ **Apply Sliding Window** specifically to character frequency problems.
3. ✅ **Understand Stack patterns** for nested structures (Parentheses).
4. ✅ **Handle string parsing** efficiently (Atoi, Zigzag, Compression).
5. ✅ **Optimize string concatenation** (StringBuilder vs String).

---

## 🏗️ LEARNING APPROACH FOR THIS WEEK

### Phase 1: Visualization (Morning)
- Use the **Expand Around Center** visualization for palindromes.
- Draw sliding windows on paper for Day 2.
- Use stack diagrams for Day 3.

### Phase 2: Implementation (Midday)
- Implement **Valid Palindrome** (Day 1) and **Longest Substring Without Repeating Characters** (Day 2).
- These are the "Hello World" of string interviews.

### Phase 3: Edge Cases (Afternoon)
- Strings are notorious for edge cases:
  - Empty string `""`
  - Single character `"a"`
  - All same characters `"aaaa"`
  - Case sensitivity `"A" vs "a"`
  - Whitespace handling

---

## 💡 TIPS & STRATEGIES FOR SUCCESS

### ✅ DO's

1. **Use StringBuilder (Java/C#)**
   - String concatenation in loops is O(n²). StringBuilder is O(n).
   - Always mention this trade-off in interviews.

2. **Check for Constraints**
   - Is it ASCII (128 chars)? Extended ASCII (256)? Unicode?
   - This determines if you use an array `int[128]` or a `HashMap`.

3. **Master Two Pointers**
   - Palindromes use **Converging Pointers** (`L -> ... <- R`).
   - Windows use **Sliding Pointers** (`L ... R ->`).

### ❌ DON'Ts

1. **Don't use Recursion for Strings (usually)**
   - Recursion can cause stack overflow for long strings.
   - Prefer iterative Two Pointers or Loops.

2. **Don't Forget Case Sensitivity**
   - Ask: "Is 'A' equal to 'a'?"
   - Solution: `Character.toLowerCase()` or bit manipulation.

---

## 🔗 HOW TOPICS CONNECT

### Day 1 → Day 2
- **Day 1 (Palindromes)** looks for symmetry.
- **Day 2 (Windows)** looks for patterns/frequencies.
- Both use **Pointers** and **Indices**.

### Day 2 → Day 3
- **Day 2 (Windows)** counts characters.
- **Day 3 (Parentheses)** counts balance.
- Both involve **State Tracking** (Map vs Counter).

---

## 📊 PRACTICE STRATEGY

### Target Problem Count
- **Easy:** 3-4 per day (12-16 total)
- **Medium:** 2-3 per day (8-12 total)
- **Hard:** 0-1 per day (Optional)

### Recommended Practice Flow
1. **Valid Palindrome** (Day 1)
2. **Longest Palindromic Substring** (Day 1)
3. **Longest Substring Without Repeating Characters** (Day 2)
4. **Valid Parentheses** (Day 3)
5. **String to Integer (atoi)** (Day 4)

---

**End of Guidelines. Proceed to Day 1 Instructional File.**
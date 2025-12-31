# 🧭 WEEK 4.75 GUIDELINES — STRING MASTERY & PATTERNS

**Week Goal:** Master intermediate string algorithms (Palindromes, Sliding Window, Parentheses, Transformations) and understand their real-world applications in parsing, validation, and text processing.

---

## 📅 WEEKLY LEARNING OBJECTIVES

By the end of this week, you should be able to:

1.  **Identify & Apply Core String Patterns:**
    -   Instantly recognize when to use **Two Pointers** (inward vs outward).
    -   Apply **Sliding Window** for substring problems (shrinkable constraints).
    -   Use **Stacks** for hierarchical validation (parentheses).
    -   Implement **String Transformations** (atoi, reversal, zigzag) with edge case discipline.

2.  **Master State Management:**
    -   Track character frequencies using **Arrays vs HashMaps** (trade-offs).
    -   Manage **window boundaries** (L, R) correctly without off-by-one errors.
    -   Handle **overflow** and **encoding** nuances in transformations.

3.  **Debug & Optimize:**
    -   Detect O(N²) pitfalls (like string concatenation in loops).
    -   Debug common edge cases (empty strings, single characters, Unicode).
    -   Optimize space from O(N) to O(1) using in-place techniques where possible.

---

## 🧠 KEY CONCEPTS OVERVIEW

| Concept | Key Idea | Typical Use Case |
| :--- | :--- | :--- |
| **Palindromes** | Symmetry around a center or matching ends. | Input validation, motif discovery. |
| **Sliding Window** | Variable-size window (`L`, `R`) satisfying a condition. | Longest/shortest substring with constraints. |
| **Stack Matching** | LIFO (Last-In, First-Out) for nested structures. | Code validation, parsing, expression evaluation. |
| **Transformations** | Converting/Building strings safely & efficiently. | `atoi`, `reversal`, parsing logs/JSON. |

---

## 📚 LEARNING APPROACH & METHODOLOGY

### 1. Visual-First Understanding
-   **Don't just code; trace.** Use the **ASCII traces** provided in the instructional files to visualize how pointers move.
-   **Draw it out.** For every sliding window problem, draw the string and manually move `L` and `R` pointers on paper before writing a single line of code.

### 2. Pattern Recognition over Memorization
-   Instead of memorizing "how to solve Longest Palindromic Substring", understand **"Expansion from Center"**.
-   Ask: *Why* does expansion work better than DP here? (Space complexity).
-   Ask: *Why* is a Stack needed for parentheses but not for simple counters? (Types & nesting order).

### 3. Edge Case Discipline
-   String problems are notorious for edge cases.
-   **Always check:** Empty string, single char, all same chars, max length, no valid answer.
-   Use **Support File 3 (Edge Cases)** to stress-test your logic.

---

## 💡 TIPS & STRATEGIES FOR THE WEEK

-   **Stop Concatenating in Loops:** In Java/C#, `s += c` is O(N²). Use `StringBuilder`. This is an immediate red flag in interviews.
-   **Window Shrinking:** In sliding window problems, use a `while` loop to shrink `L`, not an `if`. The window might need to shrink multiple steps to become valid again.
-   **Arrays for Frequency:** If the input is ASCII, `int[128]` is faster and lighter than a `HashMap`. Mention this optimization.
-   **Overflow Checks:** When implementing `atoi`, check for overflow *before* the multiplication/addition that causes it.

---

## 🔗 HOW TOPICS CONNECT

-   **Two Pointers (Day 1)** are the foundation for **Sliding Window (Day 2)**.
-   **Sliding Window** logic (validity check) often uses **HashMaps** (from Week 4.5).
-   **Parentheses (Day 3)** introduce **Stack**, which is critical for **Parsing** (Day 4 Transformations).
-   **Transformations (Day 4)** apply the efficiency lessons (StringBuilder) to all previous days.

---

## ⏳ PRACTICE STRATEGY & TIME MANAGEMENT

-   **Daily Rhythm:**
    -   **Concept (30 min):** Read instructional file & trace examples.
    -   **Active Practice (60 min):** Solve 2-3 core problems.
    -   **Review (30 min):** Check edge cases & optimize.

-   **If you have limited time:**
    -   Focus heavily on **Day 2 (Sliding Window)** and **Day 3 (Parentheses)**. These are the highest ROI for interviews.

---

## ✅ WEEKLY CHECKLIST

-   [ ] **Day 1:** Palindromes (Validity, Longest Substring).
-   [ ] **Day 2:** Sliding Window (Longest No Repeats, Anagrams).
-   [ ] **Day 3:** Parentheses (Valid, Generate, Longest Valid).
-   [ ] **Day 4:** Transformations (atoi, Reversal, Zigzag).
-   [ ] **Review:** Scan Support File 1 (Quick Reference) & File 3 (Edge Cases).
-   [ ] **Mock:** Simulate one interview question from Support File 5.

**Status:** 🚀 Ready to start Week 4.75!
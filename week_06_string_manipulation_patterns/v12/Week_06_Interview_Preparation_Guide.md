# Week 06 Interview Preparation Guide

**Week:** 06 | **Interview Ready:** After Days 1-4 | **Advanced:** With Day 5

---

## 🎙️ TYPICAL INTERVIEW QUESTIONS BY DAY

### Day 1: Palindrome Patterns
**Likelihood in Interview:** ⭐⭐⭐⭐⭐ (Very Common)

**Question 1: Longest Palindromic Substring (LeetCode #5)**
- **Difficulty:** Medium
- **Follow-up 1:** What if the string is very long (10GB)? How would you optimize?
- **Follow-up 2:** How would you handle Unicode characters?
- **Solution Approach:** Expand-around-center with O(n²) time, O(1) space
- **What They're Testing:** String manipulation, boundary conditions, optimization thinking

**Question 2: Valid Palindrome with Filtering (LeetCode #125)**
- **Difficulty:** Easy
- **Follow-up 1:** What about case sensitivity and non-alphanumeric characters?
- **Follow-up 2:** Can you do it in-place without extra space?
- **Solution Approach:** Two-pointer from both ends, skip non-alphanumeric
- **What They're Testing:** Character handling, pointer discipline, edge cases

**Question 3: Palindrome Partitioning (LeetCode #131)**
- **Difficulty:** Hard
- **Follow-up 1:** What if you need the minimum cuts to form palindromes?
- **Follow-up 2:** How would you optimize with memoization?
- **Solution Approach:** Backtracking with DP for palindrome checking
- **What They're Testing:** Recursion, memoization, algorithm design

---

### Day 2: Substring Sliding Window
**Likelihood in Interview:** ⭐⭐⭐⭐⭐ (Very Common)

**Question 1: Longest Substring Without Repeating (LeetCode #3)**
- **Difficulty:** Medium
- **Follow-up 1:** What's the minimum window substring problem? (LeetCode #76)
- **Follow-up 2:** How would you extend this to "at most K distinct characters"?
- **Solution Approach:** Sliding window with character index map
- **What They're Testing:** Window management, hash maps, pointer discipline

**Question 2: Minimum Window Substring (LeetCode #76)**
- **Difficulty:** Hard
- **Follow-up 1:** What if you had multiple pattern strings to match?
- **Follow-up 2:** How would you optimize if patterns change frequently?
- **Solution Approach:** Expand right until constraint met, shrink left to find minimum
- **What They're Testing:** Constraint reasoning, window optimization, pattern thinking

**Question 3: Permutation in String (LeetCode #567)**
- **Difficulty:** Medium
- **Follow-up 1:** What if the pattern has repeating characters?
- **Follow-up 2:** Can you find all anagrams of the pattern in the text?
- **Solution Approach:** Fixed-size window with frequency comparison
- **What They're Testing:** Frequency tracking, fixed-window logic

---

### Day 3: Parentheses & Bracket Matching
**Likelihood in Interview:** ⭐⭐⭐⭐⭐ (Very Common)

**Question 1: Valid Parentheses (LeetCode #20)**
- **Difficulty:** Easy
- **Follow-up 1:** What about nested bracket types?
- **Follow-up 2:** How would you handle special cases like empty strings?
- **Solution Approach:** Stack-based validation
- **What They're Testing:** Stack usage, type matching, correctness

**Question 2: Longest Valid Parentheses (LeetCode #32)**
- **Difficulty:** Hard
- **Follow-up 1:** Can you solve it without DP or stack?
- **Follow-up 2:** How would you find all valid substrings (not just longest)?
- **Solution Approach:** Stack with index tracking OR DP
- **What They're Testing:** Multiple approaches, optimization, index management

**Question 3: Generate Parentheses (LeetCode #22)**
- **Difficulty:** Medium
- **Follow-up 1:** How would you generate in lexicographic order?
- **Follow-up 2:** What's the time complexity of the output?
- **Solution Approach:** Backtracking with valid count tracking
- **What They're Testing:** Recursion, constraint checking, enumeration

---

### Day 4: String Transformations
**Likelihood in Interview:** ⭐⭐⭐⭐ (Common)

**Question 1: String to Integer (LeetCode #8)**
- **Difficulty:** Medium
- **Follow-up 1:** How would you handle different number bases?
- **Follow-up 2:** What if the input is streaming (reading one character at a time)?
- **Solution Approach:** Character-by-character parsing with overflow checking
- **What They're Testing:** Edge case handling, parsing, boundary checking

**Question 2: Integer to Roman (LeetCode #12)**
- **Difficulty:** Medium
- **Follow-up 1:** How would you handle numbers larger than 3999?
- **Follow-up 2:** Can you solve Roman to Integer?
- **Solution Approach:** Greedy table-based mapping
- **What They're Testing:** Greedy correctness, table design, optimization

**Question 3: Text Justification (LeetCode #68)**
- **Difficulty:** Hard
- **Follow-up 1:** How would you handle different justification modes (left, right, center)?
- **Follow-up 2:** What about edge cases like single words or single character?
- **Solution Approach:** Greedy packing with space distribution logic
- **What They're Testing:** String building, algorithm design, edge cases

---

### Day 5: Advanced String Matching (Optional)
**Likelihood in Interview:** ⭐⭐⭐ (Less Common, More Advanced)

**Question 1: Implement Karp-Rabin (Design)**
- **Difficulty:** Hard
- **Follow-up 1:** How would you handle hash collisions?
- **Follow-up 2:** What modulus would you choose and why?
- **Solution Approach:** Rolling hash with polynomial evaluation
- **What They're Testing:** Algorithm design, hash function understanding, systems thinking

**Question 2: Plagiarism Detection System Design**
- **Difficulty:** Hard
- **Follow-up 1:** How would you scale to billions of documents?
- **Follow-up 2:** How would you handle document modifications (formatting, paraphrasing)?
- **Solution Approach:** Rolling hash fingerprinting, corpus indexing
- **What They're Testing:** Systems design, scalability, real-world problem solving

---

## 🎯 COMMON INTERVIEW PATTERNS

### Pattern 1: "Optimize from Naive"
**Interview Setup:** "Here's a brute force approach. Can you optimize it?"

**Example:** Given text and pattern, find all occurrences
- Naive: O(nm) checking each position
- Optimized: O(n+m) with rolling hash

**Strategy:**
1. Understand the naive approach fully
2. Identify redundant work
3. Use data structures (hash map, stack) to eliminate redundancy
4. Prove correctness of optimization

### Pattern 2: "Edge Cases and Robustness"
**Interview Setup:** "Here's a basic solution. What about edge cases?"

**Example:** String to Integer
- Basic: Assume valid input
- Robust: Handle overflow, signs, spaces, non-digits

**Strategy:**
1. List potential edge cases
2. Trace through your code with each case
3. Add explicit checks (don't assume)
4. Test mentally with boundary values

### Pattern 3: "Multiple Constraints"
**Interview Setup:** "Now add another requirement..."

**Example:** Longest substring without repeating characters
- V1: Without repeating characters → sliding window
- V2: With at most K distinct characters → add limit
- V3: With K character replacements allowed → modify constraint

**Strategy:**
1. Understand the base problem deeply
2. Identify how new constraint affects algorithm
3. Adapt systematically (don't rewrite from scratch)
4. Discuss time/space trade-offs

---

## 💡 INTERVIEW COMMUNICATION TIPS

### Tip 1: State Your Approach Before Coding
```
"I'll use a sliding window approach with two pointers. 
I'll expand the right pointer to bring new characters, 
and contract the left pointer when constraints break. 
This gives O(n) time and O(k) space."
```
Interviewers appreciate this overview before you code.

### Tip 2: Explain Trade-offs
```
"Stack-based validation is O(n) time and O(n) space, 
but we could optimize space to O(d) where d is nesting depth. 
The DP approach is more complex to implement but 
also O(n) time and space."
```
Shows you think deeply about alternatives.

### Tip 3: Clarify Ambiguities
```
"I'm assuming the input string contains only ASCII characters. 
Should I handle Unicode? Also, are leading/trailing spaces allowed?"
```
Saves time and shows professionalism.

### Tip 4: Walk Through Your Code
```
"Starting at position 0, I have 'a'. It's not in my map, 
so I add it and move right to position 1..."
```
Traces through an example while explaining logic.

### Tip 5: Discuss Robustness
```
"For edge cases: empty string returns empty, 
single character returns itself, 
and for very long strings, I'm using StringBuilder 
to avoid O(n²) concatenation."
```
Shows you think like a production engineer.

---

## 🧪 MOCK INTERVIEW SCENARIOS

### Scenario 1: 30-Minute Technical Screen
**Time Allocation:**
- 2 min: Problem clarification
- 3 min: Approach discussion
- 10 min: Implementation
- 5 min: Testing and edge cases
- 7 min: Complexity analysis and optimization
- 3 min: Follow-up discussion

**Problem Recommendation:** LeetCode #3 or #20 (core, can extend easily)

### Scenario 2: 45-Minute Deep Dive
**Time Allocation:**
- 3 min: Problem setup
- 5 min: Clarification and approach
- 15 min: Implementation with discussion
- 10 min: Testing, edge cases, robustness
- 7 min: Optimization discussion
- 5 min: Follow-up (extension or different variant)

**Problem Recommendation:** LeetCode #76 (complex, room for discussion)

### Scenario 3: 60-Minute System Design
**Time Allocation:**
- 5 min: Problem setup
- 10 min: Clarification and requirements
- 20 min: High-level design
- 15 min: Implementation strategy
- 5 min: Edge cases and robustness
- 5 min: Q&A

**Problem Recommendation:** Plagiarism detection or DNA matching system

---

## ❌ COMMON MISTAKES TO AVOID

### Mistake 1: Not Reading the Problem Carefully
- **Wrong:** Assume lowercase letters only, but problem has uppercase
- **Right:** Read problem statement 2-3 times, list assumptions

### Mistake 2: Jumping to Code Without Planning
- **Wrong:** Start coding without explaining approach
- **Right:** State algorithm, complexity, walk through example, THEN code

### Mistake 3: Ignoring Edge Cases
- **Wrong:** Code works for typical input but fails on empty/single-element
- **Right:** Identify edge cases early, test them explicitly

### Mistake 4: Not Optimizing After Initial Solution
- **Wrong:** Submit O(n²) when O(n) is possible
- **Right:** Always ask "Can I do better?" and optimize

### Mistake 5: Unclear Communication
- **Wrong:** Code silently, hand over solution
- **Right:** Explain as you code, ask clarifying questions

---

## 📚 RESOURCES FOR INTERVIEW PREP

### Practice Sites
- **LeetCode:** 39 problems curated in this week
- **LeetCode Discuss:** Read others' solutions after solving
- **InterviewBit:** Curated problem sets with editorial explanations

### Books
- **"Cracking the Coding Interview"** by Gayle McDowell: Classic interview prep
- **"Elements of Programming Interviews"** by Tjan et al.: Deep technical focus
- **"Introduction to Algorithms" (CLRS):** Theoretical foundation

### Study Groups
- Find a study partner to mock interview with
- Take turns asking and solving problems
- Discuss approaches before coding

### Video Resources
- **TechLead:** YouTube series on interview problem solving
- **NeetCode:** Video solutions with explanations
- **Back to Back SWE:** Detailed problem walkthroughs

---

## ✅ INTERVIEW READINESS CHECKLIST

**Before Interview:**
- [ ] Solved all Day 1-4 problems at least once
- [ ] Can solve LeetCode #3, #20 without looking
- [ ] Can explain time/space complexity clearly
- [ ] Have mock-interviewed with a friend
- [ ] Know 2-3 real-world applications per day

**During Interview:**
- [ ] Clarify the problem statement
- [ ] State approach before coding
- [ ] Walk through an example
- [ ] Code cleanly with variable names
- [ ] Test with edge cases
- [ ] Discuss complexity analysis
- [ ] Suggest optimizations

**After Interview:**
- [ ] Thank the interviewer
- [ ] Ask about next steps
- [ ] Follow up with thank you email
- [ ] Reflect on what went well/poorly
- [ ] Practice similar problems if struggled

---

**Version:** 1.0 | **Status:** ✅ Production Ready | **Generated:** January 10, 2026
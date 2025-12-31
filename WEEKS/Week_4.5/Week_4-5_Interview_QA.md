# 🎙️ WEEK 4.5 — INTERVIEW Q&A REFERENCE

**Interview Questions from All 5 Days — Questions Only (NO SOLUTIONS PROVIDED)**

---

## 📌 DAY 1: HASH MAP / HASH SET PATTERNS

**Q1:** Explain the two-pointer approach vs HashMap approach for the Two Sum problem. What are the trade-offs?

**Q2:** How would you implement a frequency counter for a list of words? What structure would you use and why?

**Q3:** In the "Group Anagrams" problem, why is sorting each word's characters a good hashing strategy?

**Q4:** What's the difference between using a HashMap with counts vs a HashSet for the "Valid Anagram" problem?

**Q5:** How would you design an LRU (Least Recently Used) Cache? What data structures would you combine?

**Q6:** In the "Majority Element" problem, when can you guarantee a majority element exists? Can you solve it without HashMap?

---

## 📌 DAY 2: MONOTONIC STACK

**Q1:** Explain monotonic stack using the "Next Greater Element" problem. Why does this pattern work?

**Q2:** In "Trapping Rain Water," why must we track both left and right boundaries?

**Q3:** What's the difference between finding the "Next Greater" vs "Previous Greater" element?

**Q4:** In the "Largest Rectangle in Histogram," why is a monotonic stack better than brute force?

**Q5:** In "Daily Temperatures," what does popping from the stack represent?

**Q6:** How would you modify the monotonic stack pattern for decreasing order vs increasing order?

---

## 📌 DAY 3: MERGE OPERATIONS & INTERVALS

**Q1:** Why is sorting intervals by start time the first step in the "Merge Intervals" algorithm?

**Q2:** In "Insert Interval," why can you skip intervals that don't overlap with the insertion?

**Q3:** What's the difference between "Merge Intervals" and "Meeting Rooms II"? (Hint: involves counting)

**Q4:** In overlapping intervals, what condition determines if two intervals overlap?

**Q5:** How would you handle inserting an interval that spans multiple existing intervals?

**Q6:** Can you merge intervals without sorting first? What would the complexity be?

---

## 📌 DAY 4A: PARTITION & CYCLIC SORT

**Q1:** Explain the Dutch National Flag algorithm. Why do we use three pointers (low, mid, high)?

**Q2:** In the "Sort Colors" problem, why do we NOT increment `mid` after swapping with `high`?

**Q3:** What's the core insight of Cyclic Sort? Why does it only work for range [1, n]?

**Q4:** In "Find the Duplicate Number," how does interpreting an array as a linked list create a cycle?

**Q5:** In "First Missing Positive," why must we handle values outside the range [1, n]?

**Q6:** Compare the space complexity: Partition (O(1)) vs Sorting (O(log n) or O(1) with heapsort). When is each preferable?

---

## 📌 DAY 4B: KADANE'S ALGORITHM

**Q1:** Explain Kadane's Algorithm from first principles. Why is the "reset" mechanism crucial?

**Q2:** What happens if all numbers in the array are negative? Does Kadane's still work?

**Q3:** In "Maximum Product Subarray," why must we track BOTH the max AND min product?

**Q4:** In "Maximum Sum Circular Subarray," why is the formula `Total - MinSubarray` used?

**Q5:** Can you apply Kadane's to 2D arrays? How would you extend the logic?

**Q6:** How does Kadane's relate to Dynamic Programming? What is the state, and what is the recurrence?

---

## 📌 DAY 5: FAST & SLOW POINTERS

**Q1:** Explain Floyd's Cycle Detection algorithm. Why does moving fast at 2x and slow at 1x guarantee a meeting in cycles?

**Q2:** After detecting a cycle with Floyd's, why does resetting slow to head (and moving both at 1x) find the cycle start?

**Q3:** Can you use fast/slow to find the middle of a linked list? Is it always exact?

**Q4:** In "Find the Duplicate Number," how does the array-as-linked-list interpretation create a cycle?

**Q5:** Can fast/slow work with faster speeds (3x, 4x) instead of 2x? What changes?

**Q6:** In "Happy Number," how is cycle detection applied to a problem that's not about linked lists?

---

## 🔄 CROSS-PATTERN INTERVIEW QUESTIONS

**Q1:** A problem asks to "find elements that appear more than n/3 times." Which pattern(s) would you use?

**Q2:** Given a stream of integers, find the median *online* (without storing all values). Which patterns apply?

**Q3:** A problem combines "frequencies" and "next greater element" — which two patterns interact?

**Q4:** How would you optimize "Merge K Sorted Lists"? Which pattern from this week helps?

**Q5:** A problem asks to "rearrange array such that arr[i] == i." Which pattern applies? Why?

**Q6:** You need to solve "two sum" with a **sorted** array instead of unsorted. Does HashMap still apply?

---

## 🎯 ADVANCED INTERVIEW SCENARIOS

**Scenario 1: Follow-Up on Two Sum**
- Original: "Find two numbers that sum to target."
- Follow-up: "Now find all unique pairs." How does the solution change?
- Follow-up 2: "Now find all triplets (3Sum)." Is this just a loop of Two Sum?

**Scenario 2: Follow-Up on Merge Intervals**
- Original: "Merge overlapping intervals."
- Follow-up: "Count how many rooms are needed at peak time." (Meeting Rooms II logic)
- Follow-up 2: "Find the longest interval." Does the merge approach help?

**Scenario 3: Follow-Up on Kadane's**
- Original: "Find maximum subarray sum."
- Follow-up: "Find the start and end indices." How do you track them without extra space?
- Follow-up 2: "Find at most K elements to remove to maximize subarray sum." Is it still Kadane's?

**Scenario 4: Follow-Up on Fast/Slow**
- Original: "Detect if a linked list has a cycle."
- Follow-up: "Find where the cycle starts." (You already know this!)
- Follow-up 2: "Find the length of the cycle." Can fast/slow help?

---

## 💡 BEHAVIORAL QUESTIONS (PATTERN-AGNOSTIC)

**Q1:** "Walk me through how you'd approach a new problem you've never seen before."

**Q2:** "Tell me about a time you optimized a solution. What was your process?"

**Q3:** "How do you decide between time optimization and space optimization?"

**Q4:** "Describe a problem where multiple patterns could apply. How did you choose?"

---

## 🏆 MOCK INTERVIEW SCENARIOS

### Scenario 1: "Two Numbers That Sum to Target"
- **Expectation:** Identify HashMap pattern immediately, solve in 5 minutes.
- **Why it's asked:** Tests hash table knowledge and quick pattern recognition.

### Scenario 2: "Trapping Rain Water"
- **Expectation:** Identify that it's a monotonic stack problem after thinking ~2 minutes.
- **Why it's asked:** Tests ability to map geometric intuition to algorithmic patterns.

### Scenario 3: "Merge Intervals"
- **Expectation:** Sort + iterate logic, handle edge cases (nested intervals, touching intervals).
- **Why it's asked:** Tests sorting, merging, and interval boundary logic.

### Scenario 4: "First Missing Positive"
- **Expectation:** Recognize Cyclic Sort pattern, implement O(n) time and O(1) space.
- **Why it's asked:** Tests advanced in-place manipulation and pattern recognition.

### Scenario 5: "Linked List Cycle II"
- **Expectation:** Identify Fast/Slow, find cycle start using the mathematical insight.
- **Why it's asked:** Tests understanding of pointer mechanics and mathematical proof.

---

**No solutions provided intentionally. Work through these questions deeply!**
# DSA Mastery 101 — Phase 1-2 Learning Accelerator

## Purpose
This supplementary file is designed to make **Phase 1 and Phase 2** more learnable, more adaptive, and easier to revise before moving into **Phase 3: Trees, Graphs, and Dynamic Programming**.

It does not replace the main phase files. It upgrades them with:
- pattern decision support
- active recall prompts
- mistake tracking
- adaptive re-practice logic
- spaced revision structure
- confidence and readiness checkpoints
- faster content-generation templates for future phases

---

## Why this file matters
By the end of Phase 2, the learner usually has enough exposure to solve many problems, but not always enough structure to:
- classify new problems quickly
- diagnose why a solution failed
- retain patterns after a few days
- distinguish similar-looking techniques under interview pressure
- convert knowledge into reliable execution

This file fixes that gap.

---

# 1. Pattern decision system

## 1.1 Fast classification tree
Use this before writing code.

```text
Q1. Is the input sorted or can sorting create structure?
- Yes -> test two pointers, binary search, interval merge, 3Sum-style anchored scan
- No  -> continue

Q2. Is the task about a contiguous subarray / substring?
- Yes -> test sliding window, prefix sum, hashing, Kadane
- No  -> continue

Q3. Do you need nearest greater / smaller / span / boundary?
- Yes -> test monotonic stack
- No  -> continue

Q4. Is there a monotone answer space: small values fail, large values work (or vice versa)?
- Yes -> binary search on answer
- No  -> continue

Q5. Do repeated lookups, counts, complements, or seen-state checks matter?
- Yes -> hash map / hash set / prefix frequency
- No  -> continue

Q6. Do values naturally belong to indices 1..n?
- Yes -> cyclic sort / index placement
- No  -> continue

Q7. Is there a cycle or repeated state transition?
- Yes -> fast/slow pointers or visited set
- No  -> continue

Q8. Does the problem split cleanly into left/right subproblems?
- Yes -> divide and conquer
```

## 1.2 Pattern recognition prompts
Before solving, answer these in one line:
- Is the structure **ordered**, **contiguous**, **frequency-based**, **state-based**, or **boundary-based**?
- Do I need **one answer**, **all answers**, **count of answers**, or **best answer**?
- Can I express a clean invariant?
- Is there a monotone predicate?
- What would brute force do repeatedly that I can avoid?

---

# 2. Pattern confusion matrix

## 2.1 Common confusion pairs

| Confused patterns | How to distinguish |
|---|---|
| Two pointers vs sliding window | Two pointers often works on sorted or symmetric structure; sliding window is usually about contiguous ranges with evolving validity. |
| Fixed window vs variable window | Fixed size means window length never changes; variable size means validity controls shrink/expand. |
| Sliding window vs prefix sum + hash map | If negatives exist or exact count of many subarrays is needed, prefix-sum hashing often beats window logic. |
| Hash map vs sorting | Hashing preserves O(n) lookup; sorting may be worth it when order enables cleaner pointer logic. |
| Monotonic stack vs heap | Stack gives nearest boundary/order relationships; heap gives repeated global min/max extraction. |
| Kadane vs sliding window | Kadane optimizes arbitrary-length contiguous sum without validity constraint; sliding window needs a maintainable rule. |
| Binary search on array vs binary search on answer | One searches positions in ordered data; the other searches a value range using feasibility. |
| Fast/slow pointer vs hash set visited | Both detect cycles, but Floyd saves space when a transition function exists. |
| Divide and conquer vs dynamic programming | D&C splits independent subproblems; DP appears when overlapping subproblems or reusable states dominate. |

## 2.2 Self-test drill
For each problem you practice, write one sentence:
- “This is **not** a sliding-window problem because …”
- “This is **not** a heap problem because …”
- “This pattern wins because …”

That single step prevents shallow template matching.

---

# 3. Active recall deck

## 3.1 Phase 1 recall prompts
- Why is dynamic-array append amortized O(1)?
- Why is linked-list insertion theoretically O(1) but often slower in practice than arrays?
- What invariant proves binary search correctness?
- When does recursion become a space problem?
- Why does heapify run in O(n), not O(n log n)?
- What makes a good hash function useful in practice?
- Why does rolling hash need substring verification after a hash match?

## 3.2 Phase 2 recall prompts
- When does sliding window fail completely?
- What is the exact validity condition in a minimum-window problem?
- Why does the shorter wall move in Container With Most Water?
- What monotonic order is the stack maintaining in Daily Temperatures?
- Why does `freq[0] = 1` matter in prefix-sum counting problems?
- How do you know a problem is binary search on answer instead of DP?
- Why does Kadane restart at the current element?
- Why do fast and slow pointers meet at the cycle entry after reset?

## 3.3 Output-first recall drills
Use these without code:
- Say the pattern name in under 5 seconds.
- State the invariant in one sentence.
- Give brute force, better, and optimal in order.
- State one bug most candidates make.
- State time and space complexity aloud.

---

# 4. Adaptive error log

## 4.1 Mistake taxonomy
Tag each failed attempt with one or more of these:

| Code | Mistake type | Meaning |
|---|---|---|
| P1 | Pattern misclassification | Chose the wrong technique family |
| P2 | Right pattern, wrong invariant | Pattern was correct but logic contract was weak |
| B1 | Boundary error | Off-by-one, empty input, last element, first element |
| B2 | Duplicate handling error | Missed repeated values or uniqueness conditions |
| D1 | Data-structure misuse | Used set/map/stack/heap incorrectly |
| C1 | Complexity miss | Solved correctly but too slowly |
| R1 | Reasoning gap | Could not justify why the approach works |
| I1 | Implementation bug | Syntax or pointer updates broke otherwise good logic |
| T1 | Time-pressure collapse | Understood solution but could not code fluently |

## 4.2 Reflection template
After every miss, complete this in under 2 minutes:

```text
Problem:
Pattern I chose:
Correct pattern:
First moment I got lost:
Mistake tags:
Invariant I should have used:
One edge case I missed:
One sentence I will remember next time:
Review again on:
```

## 4.3 Adaptive next-step rules
- If the mistake is **P1**, solve 2 more problems from the same category with just pattern recognition first.
- If the mistake is **P2** or **R1**, rewrite the invariant and dry-run the example before coding.
- If the mistake is **B1** or **B2**, do 3 edge-case-only reviews from the same category.
- If the mistake is **I1**, retype the solution from memory after understanding.
- If the mistake is **C1**, compare brute force vs optimal side by side.
- If the mistake is **T1**, do a timed 20-minute re-solve after one untimed correction.

---

# 5. Spaced revision system

## 5.1 Review cadence
Use this cycle for every `Must` problem:
- Review 1: same day summary
- Review 2: after 24 hours
- Review 3: after 3 days
- Review 4: after 7 days
- Review 5: after 14 days
- Review 6: after 30 days or before mock interviews

## 5.2 What to do in each review

| Review stage | Task |
|---|---|
| Same day | Re-explain the signal, invariant, and final complexity without looking |
| 24 hours | Re-solve from scratch with no code help |
| 3 days | Solve a variant of the same pattern |
| 7 days | Compare the optimal solution with one alternate approach |
| 14 days | Timed solve + verbal explanation |
| 30 days | Mixed practice from multiple categories |

## 5.3 Retention rule
Do not mark a problem as “mastered” just because it was solved once. Mark it mastered only if:
- you can classify it quickly,
- derive the main approach,
- code it cleanly,
- explain one alternate approach,
- and pass at least one delayed re-solve.

---

# 6. Dry-run system

## 6.1 Mandatory dry-run questions
Before coding any nontrivial problem, answer:
- What does each variable mean?
- What condition makes the structure valid?
- When do I move left, right, pop, or split?
- When do I update the answer?
- What final state proves the algorithm completed correctly?

## 6.2 Table template
Use this table for tricky problems.

| Step | Pointer / state | Action | Why valid now? |
|---|---|---|---|
| 1 | left=0, right=0 | initialize | empty or first state established |
| 2 | ... | expand / shrink / pop / count | invariant preserved |

## 6.3 Best places to force dry-runs
- Minimum Window Substring
- Largest Rectangle in Histogram
- First Missing Positive
- Find the Duplicate Number
- Longest Valid Parentheses
- Binary search on answer problems

---

# 7. Variant ladder system

## 7.1 Why ladders improve adaptability
Solving one canonical problem is not enough. Adaptability comes from seeing the same idea in multiple disguises.

## 7.2 Ladder format
For each core pattern, practice three levels:
- **Level A:** direct pattern recognition
- **Level B:** same pattern with one extra condition
- **Level C:** disguised or harder version

## 7.3 Example ladders

### Two pointers
- Level A: #167 Two Sum II
- Level B: #11 Container With Most Water
- Level C: #15 3Sum

### Sliding window
- Level A: #643 Maximum Average Subarray I
- Level B: #3 Longest Substring Without Repeating Characters
- Level C: #76 Minimum Window Substring

### Binary search on answer
- Level A: #875 Koko Eating Bananas
- Level B: #1011 Capacity To Ship Packages Within D Days
- Level C: #410 Split Array Largest Sum

### Monotonic stack
- Level A: #739 Daily Temperatures
- Level B: #496 Next Greater Element I
- Level C: #84 Largest Rectangle in Histogram

### Fast/slow pointers
- Level A: #141 Linked List Cycle
- Level B: #142 Linked List Cycle II
- Level C: #287 Find the Duplicate Number

---

# 8. Readiness tracker

## 8.1 Confidence table template

| Problem # | Problem | Category | First attempt | Current confidence (1-5) | Mistake tags | Last solved | Next review |
|---|---|---|---|---|---|---|---|
| 167 | Two Sum II | Two pointers | Solved / Hint / Failed | 1-5 | P1, B1, etc. | YYYY-MM-DD | YYYY-MM-DD |

## 8.2 Category mastery rubric

| Score | Meaning |
|---|---|
| 1 | Cannot identify pattern |
| 2 | Can identify pattern after hints |
| 3 | Can solve with some implementation struggle |
| 4 | Can solve and explain clearly |
| 5 | Can derive variants and teach the pattern |

## 8.3 Promotion rule before Phase 3
Move to Phase 3 only if:
- all `Must` problems from Phase 1 and 2 are at confidence 4 or above,
- at least half of `Should` problems are at confidence 3 or above,
- you can explain the decision tree for choosing patterns,
- and you can solve at least 3 mixed-category problems in one session without pattern confusion.

---

# 9. Category-specific invariants to memorize

## 9.1 High-value invariant bank

| Category | Invariant to say aloud |
|---|---|
| Binary search | If the answer exists, it stays inside the current search range. |
| Fixed window | Window size is always exactly `k`. |
| Variable window | Window is shrunk until the constraint becomes valid again. |
| Prefix sum + hash | Count prior states that make current_state - prior_state equal target. |
| Monotonic stack | Stack stores unresolved indices in monotone order. |
| Intervals | Processed intervals are already merged and disjoint. |
| Kadane | `current` is the best subarray sum ending here. |
| Fast/slow pointers | Relative speed reveals a cycle or its entry. |
| Cyclic sort style | Every valid number should sit at its natural index. |
| Palindrome expansion | Center expansion remains valid while mirrored characters match. |

---

# 10. Content-generation improvements for future phase files

## 10.1 Add these fields to every problem block
Future content should include these compact fields consistently:
- **Recognition test:** how to identify the pattern quickly
- **Invariant:** one sentence the learner should speak before coding
- **Wrong instinct:** the common incorrect approach
- **Dry-run checkpoint:** the exact moment where most bugs happen
- **Variant jump:** one follow-up problem from the same pattern family
- **Confidence box:** self-rate 1-5 after solving

## 10.2 Better learning artifacts to include
Add these artifacts in future markdown generation:
- pattern decision trees
- one-page category cheat sheets
- minimal template code blocks per pattern
- mixed-problem diagnostic quizzes
- flashcards at the end of each category
- spaced-review tables
- “when this pattern fails” notes
- interview narration bullets: clarify, propose, justify, analyze

## 10.3 Recommended template skeletons

### Sliding window template
```python
left = 0
for right in range(len(data)):
    # include data[right]
    while window_invalid:
        # remove data[left]
        left += 1
    # update answer
```

### Binary search on answer template
```python
left, right = low_bound, high_bound
while left < right:
    mid = left + (right - left) // 2
    if feasible(mid):
        right = mid
    else:
        left = mid + 1
return left
```

### Monotonic stack template
```python
stack = []
for i, x in enumerate(data):
    while stack and violates(stack[-1], i):
        # resolve stack top
        stack.pop()
    stack.append(i)
```

---

# 11. Efficient weekly workflow before Phase 3

## 11.1 Three-pass review plan

### Pass 1 — Pattern recognition only
Take the Phase 1 and 2 problem tables and, without coding, label each problem with:
- pattern
- invariant
- likely alternate approach
- likely pitfall

### Pass 2 — Must-problem re-solves
Re-solve all `Must` problems from scratch using only the title and problem brief.
Do not look at the code unless truly stuck.

### Pass 3 — Mixed adaptive drill
Pick 6 mixed problems:
- 1 two-pointer / window
- 1 binary-search-on-answer
- 1 hash pattern
- 1 monotonic stack / interval
- 1 cyclic sort / fast-slow
- 1 string-pattern problem

Then log every miss by mistake type.

## 11.2 90-minute session structure
- 10 min: recall and pattern classification
- 30 min: first solve attempt
- 15 min: dry-run and debugging
- 15 min: explain alternate approach and complexity
- 10 min: variant problem or edge-case-only drill
- 10 min: update tracker and review schedule

---

# 12. High-ROI additions specifically for your files

## 12.1 Best add-ons to backfill into Phase 1 and 2
- a decision tree page at the start
- category flashcards at the end
- confidence table after each category
- 24h / 3d / 7d / 14d revision prompts
- one mixed quiz after every week block
- one “spot the pattern” section with no solutions first
- mistake taxonomy and reflection box in every phase file

## 12.2 Best add-ons for markdown content generation
When generating future phases, keep this order:
1. syllabus coverage map
2. category decision rules
3. anchor problems with visuals
4. alternate methods and likely mistakes
5. practice table
6. flashcards
7. revision tracker
8. readiness checklist

That structure improves both consumption and reuse.

---

# 13. Final pre-Phase-3 checklist

Mark each item Yes / No:
- I can distinguish fixed vs variable sliding window.
- I can explain why prefix sum + hash works for exact-subarray-count problems.
- I can derive a first-true binary search template.
- I can identify when a monotonic stack is needed.
- I can do Dutch national flag without pointer confusion.
- I can explain Kadane without memorized magic.
- I understand why Floyd cycle detection works.
- I can solve at least one palindrome problem and one parentheses problem cleanly.
- I have reviewed my Must problems on a spaced schedule.
- I know my weakest 3 categories before entering trees, graphs, and DP.

If any of these remain “No,” spend one more revision block before Phase 3.

---

# 14. Recommended next move
After using this file once, do one **Phase 1-2 consolidation day** before starting Phase 3:
- 60 minutes: active recall + pattern classification
- 90 minutes: 3 mixed re-solves
- 30 minutes: update confidence tracker
- 30 minutes: revisit weakest category notes

That single consolidation session will make Phase 3 much easier.

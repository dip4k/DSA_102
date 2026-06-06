# 🤖 GENERIC AI SELF-CHECK & CORRECTION STEP
## Actionable Rule for Any Week X Day Y Instructional Content

**Purpose:** Verify content accuracy BEFORE outputting markdown file. Apply this step to any topic/week.

**Timing:** Run this AFTER drafting content, BEFORE creating the markdown file.

---

## ✅ QUICK SELF-CHECK PROTOCOL (3 MINUTES)

### Step 1: Verify Input Definitions
**Check:** Do all examples/traces reference only items defined in the problem?

- [ ] All variables/nodes/items mentioned exist in problem statement
- [ ] All values (numbers, weights, labels) match their definitions exactly
- [ ] No undefined references or missing items
- [ ] Consistent naming throughout (e.g., "node A" not "node a")

**Example (Generic):**
```
Problem: "Array [5, 2, 8, 1]"
Trace: "Element at index 1 is 2" ✓ CORRECT
Trace: "Element at index 1 is 8" ❌ WRONG (should be 2)
→ Fix before output
```

---

### Step 2: Verify Logic Flow
**Check:** Does each step follow logically from the previous?

- [ ] Each decision/comparison is mathematically or logically correct
- [ ] State after Step N = input state for Step N+1
- [ ] Loop conditions match algorithm description
- [ ] No unexplained jumps or missing steps

**Example (Generic):**
```
Algorithm: "If x > 5, add to result"
Step 1: x = 3
Check: Is 3 > 5? NO ✓ CORRECT (don't add)
Step 2: x = 7
Check: Is 7 > 5? YES ✓ CORRECT (add)
```

---

### Step 3: Verify Numerical Accuracy
**Check:** Do all numbers add up correctly?

- [ ] Totals/sums are cumulative (each step adds to previous)
- [ ] Counts match actual items (e.g., 3 edges ≠ listing 4 edges)
- [ ] Final result equals sum of components
- [ ] No arithmetic errors

**Example (Generic):**
```
Step 1: Add value 5 → Total = 5 ✓
Step 2: Add value 3 → Total = 5 + 3 = 8 ✓ (not 5 + 3 = 9)
Step 3: Add value 2 → Total = 8 + 2 = 10 ✓
Final: 10 = 5 + 3 + 2 ✓ MATCH
```

---

### Step 4: Verify State Consistency
**Check:** Do state variables track changes correctly?

- [ ] State at end of Step N is used as input for Step N+1
- [ ] All state changes are explained/visible
- [ ] No contradictions (e.g., "value is 5" then "value is 3" without explaining)
- [ ] Lists/collections show clear additions/removals

**Example (Generic):**
```
After Step 1: List = [A, B]
After Step 2: List = [A, B, C] (added C) ✓ CLEAR
After Step 3: List = [A, C] (removed B) ✓ EXPLAINED

NOT:
After Step 1: List = [A, B]
After Step 3: List = [A, C] ❌ MISSING STEP 2 EXPLANATION
```

---

### Step 5: Verify Termination & Completion
**Check:** Does the algorithm stop at the right point?

- [ ] Termination condition is clearly stated
- [ ] Final result is clearly identified
- [ ] No missing steps after termination
- [ ] Count/summary of final result is correct

**Example (Generic):**
```
Condition: "Stop when count reaches 3"
Step 1: count = 1 (continue)
Step 2: count = 2 (continue)
Step 3: count = 3 (STOP) ✓ CORRECT

NOT:
Step 4: count = 4 ❌ SHOULD HAVE STOPPED AT STEP 3
```

---

## 🔧 QUICK FIX TEMPLATES

### Template 1: Input Definition Error
```
WRONG: "Reference undefined item X"
FIX:   "Verify X exists in problem definition OR remove reference"

Example:
❌ Trace mentions "Edge D-E" but graph only has A, B, C
✅ Remove mention of D-E, or add D-E to graph definition
```

### Template 2: Logic/Step Error
```
WRONG: "Step doesn't follow from previous state"
FIX:   "Recompute step manually using correct inputs"

Example:
❌ Step 2: "x > 10? YES" but x = 5 from Step 1
✅ Step 2: "5 > 10? NO" (recomputed correctly)
```

### Template 3: Numerical/Arithmetic Error
```
WRONG: "Total/sum is incorrect"
FIX:   "Recount from scratch: sum all components manually"

Example:
❌ Step 1: 5, Step 2: +3 = 7 (should be 8)
✅ Step 1: 5, Step 2: 5 + 3 = 8
```

### Template 4: State Inconsistency Error
```
WRONG: "State changes without explanation"
FIX:   "Make all changes explicit and explain the transition"

Example:
❌ After Step 1: List = [A, B]
   After Step 2: List = [A] (why was B removed?)
✅ After Step 1: List = [A, B]
   After Step 2: Remove B → List = [A] ✓ EXPLAINED
```

### Template 5: Termination Error
```
WRONG: "Algorithm continues past stopping point OR stops too early"
FIX:   "Verify condition and stop exactly when condition met"

Example:
❌ Count = 3, condition "stop at count 3", but Step 4 exists
✅ Count = 3, condition "stop at count 3", STOP immediately
```

---

## 🚨 RED FLAG CHECKS (Stop if ANY of these appear)

| 🚩 Red Flag | What It Means | Action |
|---|---|---|
| **Input mismatch** | Example uses undefined items | Verify all inputs exist in problem statement |
| **Logic jump** | Step doesn't follow from previous | Re-trace manually, show each step |
| **Math error** | Totals/counts don't match | Recount from scratch |
| **State contradiction** | State changes unexpectedly | Track state explicitly, explain transitions |
| **Algorithm overshoots** | Continues past termination point | Check stopping condition |
| **Count mismatch** | "3 items" but 4 listed | Recount items |
| **Missing step** | Jumps from Step N to Step N+2 | Add missing Step N+1 |

**If ANY red flag appears → STOP and FIX before output**

---

## 📋 DEPLOYMENT CHECKLIST (Before Outputting File)

**Run through this in order:**

```
[ ] Step 1: Input Definitions — All items exist and values match?
[ ] Step 2: Logic Flow — Each step follows logically?
[ ] Step 3: Numerical Accuracy — All numbers correct and cumulative?
[ ] Step 4: State Consistency — State tracks changes clearly?
[ ] Step 5: Termination — Algorithm stops at right point?

[ ] RED FLAGS — None of the 7 red flags present?

If ALL checked ✓ → SAFE TO OUTPUT FILE
If ANY ❌ → USE FIX TEMPLATES, THEN RE-CHECK
```

---

## 🎯 HOW TO USE IN YOUR PROMPT

**Add this section to your existing prompt structure:**

```markdown
**AI Self-Check & Correction Step:**
Before generating the markdown file, apply the GENERIC AI SELF-CHECK & CORRECTION STEP:
1. Verify input definitions (all items/values exist in problem?)
2. Verify logic flow (each step follows from previous?)
3. Verify numerical accuracy (counts/totals correct & cumulative?)
4. Verify state consistency (state changes tracked explicitly?)
5. Verify termination (algorithm stops at correct point?)
6. Check red flags (any of 7 red flags present?)

If any issue found → Use quick fix templates to correct
When all checks pass ✓ → Generate markdown file
```

---

## 💡 EXAMPLES BY TOPIC

### Example 1: Array/List Problem (Week 1-3)
```
Problem: "Sort [3, 1, 4, 1, 5]"

Self-Check:
✓ Step 1: Input = [3, 1, 4, 1, 5] (all values exist in array)
✓ Step 2: Each comparison step follows logically
✓ Step 3: Count of swaps/steps is correct
✓ Step 4: State shows array progression clearly
✓ Step 5: Stops when sorted (no more swaps possible)
✓ Red Flags: None
→ SAFE TO OUTPUT
```

### Example 2: Tree Traversal Problem (Week 5-6)
```
Problem: "Traverse tree with root=A, children B,C"

Self-Check:
✓ Step 1: Nodes A, B, C exist in tree definition
✓ Step 2: Each visit step follows traversal rule
✓ Step 3: Visit count = 3 (matches node count)
✓ Step 4: Visited set shows progression [A] → [A,B] → [A,B,C]
✓ Step 5: Stops after all nodes visited
✓ Red Flags: None
→ SAFE TO OUTPUT
```

### Example 3: Graph Algorithm (Week 7-9)
```
Problem: "Find shortest path A→D in graph with edges..."

Self-Check:
✓ Step 1: All vertices/edges in trace exist in graph
✓ Step 2: Each relaxation step is logically valid
✓ Step 3: Distance totals are cumulative
✓ Step 4: Distance array updates shown clearly
✓ Step 5: Stops when all vertices processed
✓ Red Flags: None
→ SAFE TO OUTPUT
```

### Example 4: Dynamic Programming (Week 10-12)
```
Problem: "Compute DP table for given inputs"

Self-Check:
✓ Step 1: All array indices exist (no out-of-bounds)
✓ Step 2: Each DP recurrence follows definition
✓ Step 3: DP values accumulate correctly
✓ Step 4: State transitions shown (which cells fill which cells)
✓ Step 5: Stops when final cell computed
✓ Red Flags: None
→ SAFE TO OUTPUT
```

---

## 📝 TEMPLATE FOR PROMPT INTEGRATION

Use this exact format in your prompt:

```markdown
**Action:**
- [Your existing actions here]
- Apply GENERIC AI SELF-CHECK & CORRECTION STEP before output:
  1. Verify all references (items, values, indices) exist in problem
  2. Verify logic flow (each step follows logically from previous)
  3. Verify numbers (totals cumulative, counts match actual items)
  4. Verify state consistency (changes tracked, transitions explained)
  5. Verify termination (algorithm stops at correct condition)
  6. Check 7 red flags (input mismatch, logic jump, math error, etc.)
  7. If any issue found → Fix using templates before output
  8. When all checks pass → Generate markdown file

**File Naming:** [Your existing naming here]
```

---

## 🎓 WHAT MAKES THIS GENERIC

✅ **Works for ANY topic:** Arrays, Trees, Graphs, DP, Strings, Math, etc.
✅ **Works for ANY algorithm:** Sorting, Searching, Traversal, etc.
✅ **Works for ANY week:** Week 01 to Week 19
✅ **Simple language:** No technical jargon, easy for AI to follow
✅ **Actionable:** Each step has clear do/check list
✅ **Quick:** Can be done in 2-3 minutes per trace
✅ **Reusable:** Same framework applies to all content types

---

**End of Generic Framework**

Use this as a standalone rule that can be inserted into ANY prompt for ANY instructional content generation.


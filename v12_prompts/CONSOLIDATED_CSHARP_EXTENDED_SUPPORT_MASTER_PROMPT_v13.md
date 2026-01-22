# 💻 CONSOLIDATED_CSHARP_EXTENDED_SUPPORT_MASTER_PROMPT_v13

**Version:** 13.0 FINAL (Production Ready)  
**Date:** January 22, 2026  
**Purpose:** Master prompt for generating ANY week's extended C# support content  
**Status:** ✅ READY FOR PRODUCTION USE WITH AI

---

## 🎯 MASTER PHILOSOPHY

Generate Extended C# Support files that bridge TWO critical student needs:

```
1. PATTERN SELECTION (Learning)      2. PRODUCTION IMPLEMENTATION (Engineering)
├─ Recognize patterns                ├─ Write professional code
├─ Understand when/why to use        ├─ Guard clauses & error handling
├─ Know anti-patterns                ├─ Mental models & narratives
└─ Choose right collection           └─ Performance awareness
         ↓                                    ↓
      v11 STRENGTH              +        v12 STRENGTH
         ↓                                    ↓
   Result: v13 HYBRID = Both + Cohesion
```

---

## 📋 CONSOLIDATED PROMPT TEMPLATE (USE THIS WITH AI)

```
============================================================================
INSTRUCTION FOR AI: GENERATE WEEK [X] EXTENDED C# SUPPORT FILE
============================================================================

You are generating: Week_[X]_Extended_CSharp_Problem_Solving_Implementation.md

MANDATORY STRUCTURE (7 SECTIONS):

---

# 🗺️ Week_[X]_Extended_CSharp_Problem_Solving_Implementation

**Version:** v1.0 HYBRID (Pattern Recognition + Production Implementation)  
**Week:** [X] – [Week Title from Syllabus]  
**Purpose:** Master [Week X topics] through pattern recognition, understanding, and practice  
**Target:** Transform Week [X] knowledge into interview-ready C# coding skills  
**Prerequisites:** Week [X] instructional files + standard support files complete

---

## 📚 WEEK [X] OVERVIEW

**Primary Goal:** [From syllabus primary goal]

**Topics by Day:**
- **Day 1:** [Topic 1 from syllabus]
- **Day 2:** [Topic 2 from syllabus]
- **Day 3:** [Topic 3 from syllabus]
- **Day 4:** [Topic 4 from syllabus]
- **Day 5:** [Topic 5 from syllabus]

---

## SECTION 1️⃣: PATTERN RECOGNITION FRAMEWORK

### 🎯 Decision Tree — How to Identify & Choose Week [X] Patterns

Create a decision tree table with these columns:
| 🔍 Problem Phrases/Signals | 🎯 Pattern Name | ❓ Why This Pattern? | 💻 C# Collection | ⏱️ Time/Space |

REQUIREMENTS:
- Extract 3-5 PRIMARY patterns from Week [X] syllabus topics
- For each pattern, provide:
  * Problem signals/phrases (when you see "X", think "Y")
  * Pattern name (clear, memorable)
  * Reason WHY this pattern solves it (2-3 sentence explanation)
  * Best C# collection/type to use
  * Time and Space complexity

EXAMPLE ROW:
| "Find minimum in rotated", "Pivot location", "Log n" | **Binary Search (Rotated)** | Identify which half is sorted; narrow to unsorted half | `int[]` | O(log n) / O(1) |

---

## SECTION 2️⃣: ANTI-PATTERNS — WHEN NOT TO USE & WHAT FAILS

Create anti-pattern table with these columns:
| ❌ Wrong Approach | 💥 Why It Fails | ⚡ Symptom/Consequence | ✅ Correct Alternative |

REQUIREMENTS:
- Identify 4-6 COMMON MISTAKES students make with Week [X] patterns
- For each mistake:
  * What approach/code is wrong
  * Root cause why it fails (performance/correctness)
  * Runtime symptom or consequence (exception/bug/slowdown)
  * Correct alternative with brief explanation

EXAMPLE ROW:
| Using recursion without memoization | O(2^n) exponential explosion | Stack overflow or timeout on n>20 | Use DP with memoization for O(n) |

---

## SECTION 3️⃣: PATTERN IMPLEMENTATIONS (Production-Grade)

For EACH pattern (2-4 patterns per week):

### Pattern: [PATTERN_NAME]

#### 🧠 Mental Model
[1-2 sentence analogy or core invariant explaining the pattern]
Example: "Like merging two sorted arms of an X; pointers start at ends and move inward."

#### ✅ When to Use This Pattern
- ✅ [Scenario 1 — what type of problem needs this]
- ✅ [Scenario 2 — another use case]
- ✅ [Scenario 3 — edge case where best choice]

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// [Algorithm Name] - [1-line purpose]
/// Time Complexity: O(?) | Space Complexity: O(?)
/// 
/// 🧠 MENTAL MODEL:
/// [Core invariant or analogy explaining WHY this works in 2-3 sentences]
/// </summary>
public [ReturnType] [MethodName]([InputType] input) {
    
    // STEP 1: Guard Clauses (Handle edge cases FIRST)
    // Check nulls, empty cases, single elements
    if (input == null || input.Length == 0) return [default];
    if ([other edge case]) return [alternative];
    
    // STEP 2: Initialize State (Explain WHAT and WHY)
    // [Describe state variables and their invariants]
    var [stateVariable] = new [Collection];
    
    // STEP 3: Core Logic Loop
    // [Explain the core operation step by step]
    for (int i = 0; i < input.Length; i++) {
        // [Step A description]: [Action with why]
        [logic]
        
        // [Step B description]: [Action with why]
        [logic]
    }
    
    return result;
}
```

REQUIREMENTS FOR EACH IMPLEMENTATION:
- Mental model comment (1-2 sentences, clear analogy)
- Time/Space complexity documented
- Guard clauses on ALL inputs (null, empty, edge cases)
- Step-by-step comments explaining WHAT and WHY
- Variable names are MEANINGFUL (not single letters)
- Return explicitly stated
- 2-3 example implementations showing variations (if applicable)

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** [One critical C# gotcha for this pattern]
- 🟡 **PERFORMANCE:** [One performance consideration or optimization]
- 🟢 **BEST PRACTICE:** [One best practice for this pattern in C#]

---

## SECTION 4️⃣: C# COLLECTION DECISION GUIDE

Create table with columns:
| Use Case | Collection | Why? | When NOT to Use | Alternative |

REQUIREMENTS:
- Cover 4-6 collections relevant to Week [X]:
  * `Array` (fixed size)
  * `List<T>` (dynamic)
  * `LinkedList<T>` (pointer-based)
  * `Stack<T>` (LIFO)
  * `Queue<T>` (FIFO)
  * `HashSet<T>`, `Dictionary<K,V>`, custom classes (as needed)
  * Custom implementations (if pattern-specific)

- For EACH collection:
  * Use case (when you'd choose this)
  * Why it's best for this use case
  * When NOT to use it (anti-pattern)
  * Better alternative if not suitable

**KEY INSIGHT:** Add sentence: "Choosing the right collection is as important as choosing the right pattern. Wrong collection = Correct algorithm running slowly."

---

## SECTION 5️⃣: PROGRESSIVE PROBLEM LADDER

Organize problems by difficulty and progression:

### 🟢 STAGE 1: CANONICAL PROBLEMS — Master Core Pattern

Create table with columns:
| # | LeetCode # | Difficulty | Pattern | C# Focus | Concept |

REQUIREMENTS:
- 3-4 EASY problems per pattern
- Each tests core skeleton (no tricks)
- C# Focus: specific C# aspect (collections, guards, etc.)
- Concept: what's being tested

EXAMPLE:
| 1 | #206 | 🟢 Easy | Linked List | Manual `.Next` manipulation | Pointer reversal |

### 🟡 STAGE 2: VARIATIONS — Recognize Pattern Boundaries

Create table with columns:
| # | LeetCode # | Difficulty | Pattern + Twist | C# Focus | When Pattern Breaks |

REQUIREMENTS:
- 3-4 MEDIUM problems per pattern
- Each adds a constraint or variation
- "When Pattern Breaks" column: edge case that breaks naive approach
- Tests understanding of boundaries

EXAMPLE:
| 1 | #92 | 🟡 Medium | Linked List (partial) | Track [left, right] boundaries | Must handle segment disconnection |

### 🟠 STAGE 3: INTEGRATION — Combine Patterns

Create table with columns:
| # | LeetCode # | Difficulty | Patterns Required | C# Focus | Pattern Combination Logic |

REQUIREMENTS:
- 2-3 HARD problems
- Require combining 2+ patterns
- "Pattern Combination Logic": why combine? what does each solve?
- Tests real-world thinking

EXAMPLE:
| 1 | #146 | 🔴 Hard | Hash + Linked List | LRU Cache | Hash for O(1) lookup + LL for order |

---

## SECTION 6️⃣: WEEK [X] PITFALLS & C# GOTCHAS

### 🐛 Runtime Issues & Collection Pitfalls

Create table with columns:
| ❌ Pattern | 🐛 Bug | 💥 C# Symptom | 🔧 Quick Fix |

REQUIREMENTS:
- 4-6 real bugs students will encounter
- Pattern: which pattern causes this bug
- Bug: specific mistake
- Symptom: what error/behavior results
- Fix: one-line solution

EXAMPLE:
| Linked List | Not saving `next` before mutation | Lost reference | Always: `next = current.Next;` before `current.Next = ...` |

### 🎯 Week [X] Collection Gotchas

Bullet list of SPECIFIC mistakes:

- ❌ [Mistake description] → [Why wrong] → [Correct approach]
  - Example: `List<T>.RemoveAt(0)` for queue → O(n) per dequeue → Use `Queue<T>`

REQUIREMENTS:
- 3-4 collection-specific gotchas
- Concrete example with numbers if applicable
- Clear correct alternative

---

## SECTION 7️⃣: QUICK REFERENCE — INTERVIEW PREPARATION

Create table with columns:
| Pattern | 1-Liner Mental Model | Code Symbol | When You See This... |

REQUIREMENTS:
- ONE row per main pattern in week
- 1-liner: quick 10-second recall
- Code symbol: 1-2 lines of code structure
- When you see: problem phrase triggers this pattern

EXAMPLE:
| Fast-Slow Pointer | "Two runners different speeds, meet if cycle" | `slow = slow.Next; fast = fast.Next.Next;` | "Cycle", "Middle", "Kth from end" |

---

## ✅ WEEK [X] COMPLETION CHECKLIST

### Pattern Fluency — Can You Recognize & Choose?

For EACH pattern in the week:
- [ ] Recognize [Pattern Name] by its problem signals
- [ ] Recall [Pattern Name] skeleton without notes
- [ ] Explain WHY [Pattern Name] beats alternatives
- [ ] Explain WHEN [Pattern Name] fails

### Problem Solving — Can You Practice?

- [ ] Solved ALL Stage 1 canonical problems
- [ ] Solved 80%+ Stage 2 variations
- [ ] Solved 50%+ Stage 3 integration problems

### Production Code Quality — Can You Code?

- [ ] Used guard clauses on all inputs
- [ ] Added mental model comments
- [ ] Chose correct collections
- [ ] Handled edge cases explicitly
- [ ] Code passes code review (clean, readable, efficient)

### Interview Ready — Can You Communicate?

- [ ] Can solve Stage 1 problem in < 5 minutes
- [ ] Can EXPLAIN your pattern choice
- [ ] Can write PRODUCTION-GRADE code
- [ ] Can discuss tradeoffs

---

### 🎯 Week [X] Mastery Status

- [ ] **YES - I've mastered Week [X]. Ready for Week [X+1].**
- [ ] **NO - Need to practice more.**

---

## 📚 REFERENCE MATERIALS

This file is self-contained. You have:
- **Decision framework** → Recognize which pattern
- **Anti-patterns knowledge** → What NOT to do
- **Production-grade code** → How to implement
- **Collection guidance** → Which collection when
- **Progressive practice** → Easy to hard problems
- **Real gotchas** → Bugs you'll encounter
- **Quick interview reference** → 30-second recall

---

## 🚀 HOW TO USE THIS FILE

**For Learning:** SECTION 1 → 2 → 3 → 5 (progressive)
**For Reference:** See problem → Check SECTION 1 → Find pattern → SECTION 3 code
**For Interview:** Review SECTION 7 → Day of: skim SECTION 1

---

*End of Week [X] Extended C# Support — v13 Hybrid*

============================================================================
END OF WEEK [X] REQUIREMENTS
============================================================================

CRITICAL REQUIREMENTS FOR ALL WEEKS:

✅ SECTION 1: Decision tree has 3-5 CLEAR patterns from syllabus
✅ SECTION 2: Anti-patterns tied to Week [X] specifically (not generic)
✅ SECTION 3: 2-4 COMPLETE implementations with mental models, guards, step comments
✅ SECTION 4: Collections relevant to Week [X] patterns
✅ SECTION 5: 3+4+2 = 9 total LeetCode problems across 3 stages
✅ SECTION 6: Week-specific gotchas (not generic issues)
✅ SECTION 7: Quick reference for interview recall
✅ SECTION 8: Completion checklist for self-assessment

QUALITY GATES:

- [ ] Each code example has mental model comment
- [ ] Each code example has guard clauses
- [ ] Each code example has step-by-step comments
- [ ] Time/Space complexity documented for each algorithm
- [ ] Collection choices explained (not just used)
- [ ] Anti-patterns are SPECIFIC to week (not generic)
- [ ] Problem ladder has clear progression (easy→medium→hard)
- [ ] Student can learn patterns from SECTION 1 alone
- [ ] Student can implement from SECTION 3 alone
- [ ] File is self-contained (no external references needed)

GOLDEN RULES:

1. **Pattern FIRST, Code SECOND** — Decision tree before implementations
2. **Mental Models REQUIRED** — Every algorithm needs core invariant/analogy
3. **Guards EVERYWHERE** — Check for null, empty, edge cases
4. **Comments Explain WHY** — Not WHAT (code shows that)
5. **Production Ready** — No hacks, no shortcuts, no incomplete error handling
6. **Week Specific** — Not generic; use syllabus topics specifically
7. **Interview Focused** — Students should pass technical interviews after this

============================================================================
```

---

## 🎯 HOW TO USE THIS CONSOLIDATED PROMPT

### **Method 1: Direct Copy-Paste to AI**

```
Copy the entire "CONSOLIDATED PROMPT TEMPLATE" section above
Paste into your AI (ChatGPT, Claude, etc.)
Replace [X] with the week number
Replace [topic] with actual week topic from syllabus
Click Generate
```

### **Method 2: Fill-in-the-Blanks Workflow**

1. **Get Week [X] Syllabus** (from COMPLETE_SYLLABUS_v12_FINAL_Updated.md)
2. **Extract Information:**
   - Primary goal → Paste into template
   - Day 1-5 topics → Paste into template
   - Main patterns → Identify for SECTION 1
   - Common mistakes → List for SECTION 2
3. **Prompt AI** with template + extracted info
4. **AI Generates** complete Week [X] file

### **Method 3: Iterative Refinement**

```
ROUND 1: Generate full file with prompt
ROUND 2: Ask AI to expand SECTION 3 (more implementations)
ROUND 3: Ask AI to add more gotchas to SECTION 6
ROUND 4: Ask AI to verify Stage 1/2/3 problems are correct LeetCode #s
ROUND 5: Ask AI to check all code has guards + mental models
```

---

## 📊 EXPECTED OUTPUT STRUCTURE

When AI generates using this consolidated prompt, you'll get:

```
File: Week_[X]_Extended_CSharp_Problem_Solving_Implementation.md

Size: 15,000-25,000 words (for comprehensive week)

Contents:
├─ SECTION 1: 1 decision tree (3-5 patterns)
├─ SECTION 2: 1 anti-pattern table (4-6 issues)
├─ SECTION 3: 2-4 complete implementations (~100-200 lines per pattern)
├─ SECTION 4: 1 collection guide (4-6 collections)
├─ SECTION 5: 3 problem ladders (9 total problems listed)
├─ SECTION 6: 2 tables (4-6 runtime issues + 3-4 collection gotchas)
├─ SECTION 7: 1 quick reference table (5-6 patterns)
├─ SECTION 8: Completion checklist + summary
└─ References + Usage Guide
```

---

## ✅ VALIDATION CHECKLIST FOR GENERATED FILES

After AI generates a week file, validate:

**Pattern Framework:**
- [ ] Decision tree has 3-5 patterns extracted from syllabus?
- [ ] Each pattern has clear problem signals?
- [ ] "Why this pattern?" reasoning is clear?
- [ ] Collections are correctly recommended?

**Anti-Patterns:**
- [ ] 4-6 mistakes specific to this week?
- [ ] Each has root cause explanation?
- [ ] Symptoms are realistic?
- [ ] Alternatives are actionable?

**Implementations:**
- [ ] 2-4 complete, runnable code examples?
- [ ] Mental models included for each?
- [ ] Guard clauses on ALL inputs?
- [ ] Step comments explain WHY logic?
- [ ] Time/Space complexity documented?

**Collections:**
- [ ] Covers 4-6 relevant collections?
- [ ] When/why clear for each?
- [ ] When NOT to use documented?
- [ ] Alternatives provided?

**Problem Ladder:**
- [ ] Stage 1: 3-4 easy canonical problems?
- [ ] Stage 2: 3-4 medium variations?
- [ ] Stage 3: 2-3 hard integration problems?
- [ ] LeetCode numbers correct and real?

**Gotchas:**
- [ ] 4-6 runtime issues with fixes?
- [ ] 3-4 collection gotchas?
- [ ] Examples or code snippets included?

**Interview Ready:**
- [ ] Quick reference has 1-liners?
- [ ] Code symbols provided?
- [ ] Problem signals → Pattern mapping clear?

---

## 🔄 ITERATION WORKFLOW

**Week 1-3:** Use consolidated prompt + syllabus to generate  
**Review:** Check validation checklist  
**Refine:** Ask AI to fix gaps (more code, clearer explanations, etc.)  
**Approve:** File passes quality gates  
**Week 4-16:** Repeat process  

---

## 💡 PRO TIPS FOR BEST RESULTS

### **Tip 1: Provide Syllabus Context**
```
"Generate Week 3 extended C# support using this prompt.
Here's the Week 3 syllabus: [paste syllabus].
Include these 4 patterns: Merge Sort, Quick Sort, Heaps, Hash Tables."
```

### **Tip 2: Ask for Specific Code Style**
```
"Generate with mental models using 🧠 emoji.
Guard clauses check for null, empty, and boundaries.
Comments explain WHY, not WHAT."
```

### **Tip 3: Request Iteration**
```
"Round 1: Generate full structure.
Round 2: Expand SECTION 3 with 10+ code examples.
Round 3: Add 5+ more LeetCode problems to SECTION 5.
Round 4: Verify all code compiles and runs."
```

### **Tip 4: Verify LeetCode Numbers**
```
"Verify all LeetCode problem numbers in SECTION 5 are correct.
Stage 1 problems should be 🟢 Easy difficulty.
Stage 2 problems should be 🟡 Medium difficulty.
Stage 3 problems should be 🔴 Hard difficulty."
```

### **Tip 5: Check Code Quality**
```
"Review all code in SECTION 3:
1. Each has guard clauses?
2. Each has mental model comment?
3. Each has step comments?
4. Time/Space complexity documented?
5. Variable names are meaningful (no single letters)?"
```

---

## 📝 EXAMPLE USAGE (Week 03)

```
PROMPT FOR AI:

Use this consolidated prompt to generate Week 3 extended C# support:
[PASTE CONSOLIDATED TEMPLATE ABOVE]

Replace [X] with: 3
Replace "Week [X] Overview" with:

Primary Goal: Understand internal mechanics and trade-offs of sorting algorithms, 
heaps, and hash tables, including string hashing.

Topics by Day:
- Day 1: Elementary Sorts (Bubble, Selection, Insertion)
- Day 2: Merge Sort & Quick Sort
- Day 3: Heaps, Heapify & Heap Sort
- Day 4: Hash Tables I (Separate Chaining)
- Day 5: Hash Tables II (Open Addressing & Karp-Rabin)

Main Patterns to Cover:
1. Merge Sort (stable, O(n log n) guaranteed)
2. Quick Sort (fast average, O(n²) worst)
3. Heap Operations (insert, extract, heapify)
4. Hash Tables with Chaining
5. Binary Search on Heaps (find k-th largest)

Generate the complete Week 3 file with all 7 sections.
```

**AI Will Generate:** Complete Week_03_Extended_CSharp_Problem_Solving_Implementation.md

---

## 🚀 PRODUCTION DEPLOYMENT

### **For Weeks 2-16:**

1. **Extract syllabus info** from COMPLETE_SYLLABUS_v12_FINAL_Updated.md
2. **Use consolidated prompt** (this file)
3. **Run AI generation** with template + syllabus
4. **Validate** against checklist
5. **Approve** when all checkboxes pass
6. **Deploy** to course repository

### **Update Frequency:**

- Generate all Week 2-16 files once using this prompt
- Re-generate if:
  - Syllabus changes
  - Student feedback identifies gaps
  - New patterns emerge
  - Better examples found

---

## 📚 DOCUMENT HIERARCHY

```
SYSTEM_PROMPT_v13_EXTENDED_SUPPORT_CSHARP.md
├─ Philosophy & principles
├─ Coding standards
├─ Template structure
└─ Usage rules

CONSOLIDATED_CSHARP_EXTENDED_SUPPORT_MASTER_PROMPT_v13.md ← YOU ARE HERE
├─ Complete AI-ready prompt
├─ Fill-in-the-blanks sections
├─ Usage workflows
└─ Validation checklist

Week_02_Extended_CSharp_Complete_v13.md
├─ EXAMPLE of output using this prompt
├─ Reference for students
└─ Template for consistency

Week_03, Week_04, ... Week_16
└─ Generated using this consolidated prompt
```

---

## ✅ FINAL VERIFICATION

Before using this consolidated prompt, confirm:

- [ ] This file references all 7 required sections
- [ ] Template uses exact formatting (headers, emojis, tables)
- [ ] Instructions are clear for each section
- [ ] Examples provided for guidance
- [ ] Validation checklist is comprehensive
- [ ] Usage workflows are explained
- [ ] AI can understand and follow all requirements

---

**Status:** ✅ CONSOLIDATED MASTER PROMPT v13 PRODUCTION READY

**Usage:** Copy entire "CONSOLIDATED PROMPT TEMPLATE" section and paste into AI with week [X] and syllabus details.

**Result:** Production-ready Week [X] Extended C# Support file following v13 hybrid approach.

---


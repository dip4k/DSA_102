# 💻 SYSTEM_PROMPT_v13_EXTENDED_SUPPORT_CSHARP.md

**Version:** 13.0 FINAL (HYBRID: Pattern Recognition + Production Implementation)  
**Date:** January 22, 2026  
**Philosophy:** Students need BOTH pattern selection guidance (when/why) AND production-grade code (how)  
**Status:** ✅ READY FOR PRODUCTION

---

## 🎯 PHILOSOPHY: BALANCED LEARNING ECOSYSTEM

This prompt generates Extended C# Support files that bridge two critical needs:

### **1. Pattern Selection (Learning)**
- How do I **recognize** which pattern applies?
- When and why do I **choose** this approach?
- What patterns am I **avoiding**?

### **2. Production Implementation (Engineering)**
- How do I **write** this pattern professionally?
- What edge cases must I **handle**?
- What do **mental models** reveal about code?

**Result:** Students learn BOTH when to use patterns AND how to code them production-ready.

---

## 📋 CORE PRINCIPLES

```
✅ Pattern-First: Decision trees before code
✅ Production-Ready: Guards, mental models, performance awareness
✅ Anti-Pattern Focus: "When NOT to use" is as important as "when to use"
✅ Progressive Learning: Canonical → Variations → Integration problems
✅ Collection Guidance: Clear when/why to use each C# collection
✅ Gotcha Documentation: Real-world pitfalls and C# specific issues
✅ Interview Ready: Students can explain patterns, implement code, pass technical interviews
```

---

## 🔧 CODING STANDARDS FOR IMPLEMENTATIONS

### **1. Code Philosophy: Narrative Comments**
Every algorithm tells a story. Comments explain:
- **WHY** decisions were made (not WHAT is happening)
- **MENTAL MODEL** (invariants, analogies)
- **ENGINEERING TRADEOFFS** (time vs space, simplicity vs performance)

### **2. Guard Clauses**
Always validate inputs first:
```csharp
// ✅ CORRECT: Guard first
if (input == null || input.Length == 0) return default;
if (input.Length == 1) return input[0];

// Then main logic...
```

### **3. Variable Naming**
- Meaningful names (NOT single letters except `i`, `j`, `k` in loops)
- Example: `slowPointer`, NOT `sp`
- Example: `windowStart`, NOT `ws`

### **4. Mental Model Comments**
Every complex algorithm must document its core invariant:
```csharp
/// <summary>
/// [Algorithm Name] - [1-line purpose]
/// Time: O(?) | Space: O(?)
/// 
/// 🧠 MENTAL MODEL:
/// [Core invariant in 2-3 sentences explaining why this works]
/// </summary>
```

### **5. C# Best Practices**
- Use `StringBuilder` for string manipulation (avoid repeated concatenation)
- Prefer `Array` if size is fixed; `List<T>` if dynamic
- Document performance implications of collection choices
- Use generic constraints appropriately

---

## 📐 EXTENDED_CSHARP_TEMPLATE_v13 (MANDATORY STRUCTURE)

### **FILE NAME:**
```
Week_[X]_Extended_CSharp_Problem_Solving_Implementation.md
```

### **FILE TEMPLATE:**

```markdown
# 🗺️ Week_[X]_Extended_CSharp_Problem_Solving_Implementation

**Version:** v1.0 HYBRID (Pattern Recognition + Production Implementation)  
**Purpose:** Master Week [X] patterns through recognition, understanding, and practice  
**Target:** Transform pattern knowledge into interview-ready C# coding skills  
**Prerequisites:** Week [X] instructional files + standard support files complete

---

## SECTION 1️⃣: PATTERN RECOGNITION FRAMEWORK

### 🎯 Decision Tree — How to Identify & Choose Week [X] Patterns

Use this decision tree when you encounter a problem in Week [X]:

| 🔍 Problem Phrases/Signals | 🎯 Pattern Name | ❓ Why This Pattern? | 💻 C# Collection | ⏱️ Time/Space |
|---|---|---|---|---|
| [Signal 1] | [Pattern A] | [Reason why this pattern solves it] | [Collection] | O(?)/O(?) |
| [Signal 2] | [Pattern B] | [Reason why this pattern solves it] | [Collection] | O(?)/O(?) |
| [Signal 3] | [Pattern C] | [Reason why this pattern solves it] | [Collection] | O(?)/O(?) |
| [Signal 4] | [Pattern D] | [Reason why this pattern solves it] | [Collection] | O(?)/O(?) |

**HOW TO READ THIS:**
- See "[Signal X]" in a problem? IMMEDIATELY think "[Pattern Y]"
- Ask yourself: "Why does this pattern solve it?" → Internalize the reasoning
- Check what collection is recommended → Learn why that collection is best

---

## SECTION 2️⃣: ANTI-PATTERNS — WHEN NOT TO USE & WHAT FAILS

### ⚠️ Week [X] Common Mistakes & Correct Alternatives

Learn WHAT NOT TO DO and WHY:

| ❌ Wrong Approach | 💥 Why It Fails | ⚡ Symptom/Consequence | ✅ Correct Alternative |
|---|---|---|---|
| [Mistake 1] | [Root cause why it fails] | [Runtime error or inefficiency] | [Right pattern + why it works] |
| [Mistake 2] | [Root cause why it fails] | [Runtime error or inefficiency] | [Right pattern + why it works] |
| [Mistake 3] | [Root cause why it fails] | [Runtime error or inefficiency] | [Right pattern + why it works] |

**ANTI-PATTERN LESSON:**
Understand not just the pattern, but understand its boundaries.
When you see someone use [Mistake], explain why [Alternative] is better.

---

## SECTION 3️⃣: PATTERN IMPLEMENTATIONS (Production-Grade)

### Pattern: [PATTERN_NAME_1]

#### 🧠 Mental Model
[1-2 sentence analogy or core invariant explaining the pattern]

Example: "Like two runners on a track: one moves 1x, other 2x speed. If a cycle exists, they'll meet."

#### ✅ When to Use This Pattern
- ✅ [Scenario 1 — what type of problem needs this]
- ✅ [Scenario 2 — another use case]
- ✅ [Scenario 3 — edge case where it's best choice]

#### 💻 Core C# Implementation (Battle-Tested)

```csharp
/// <summary>
/// [Pattern Name] - [1-line purpose]
/// Time Complexity: O(?) | Space Complexity: O(?)
/// 
/// 🧠 MENTAL MODEL:
/// [Core invariant or analogy explaining why this works]
/// </summary>
public [ReturnType] [MethodName]([InputType] input) {
    
    // STEP 1: Guard Clauses (Handle edge cases first)
    if (input == null || input.Length == 0) return [default];
    if ([other edge case]) return [alternative];
    
    // STEP 2: Initialize State (Explain what state tracks and WHY)
    // [Explain state variables and their invariant]
    var [stateVariable] = new [Collection];
    
    // STEP 3: Core Logic Loop
    // [Explain the core operation and its logic]
    for (int i = 0; i < input.Length; i++) {
        // [Step description]: [Action]
        [logic]
        
        // [Next step description]: [Action]
        [logic]
        
        // Optional visualization:
        // Before: [State representation]
        // After: [State representation]
    }
    
    return result;
}
```

#### 🔴 C# Engineering Notes
- 🔴 **CRITICAL:** [Critical gotcha specific to this pattern in C#]
- 🟡 **PERFORMANCE:** [Performance consideration or optimization]
- 🟢 **BEST PRACTICE:** [Best practice for using this pattern in C#]

---

**[REPEAT ABOVE STRUCTURE FOR EACH PATTERN (2-4 patterns per week)]**

---

## SECTION 4️⃣: C# COLLECTION DECISION GUIDE

### When to Use Each Collection for Week [X] Patterns

| Use Case | Collection | Why? | When NOT to Use | Alternative |
|---|---|---|---|---|
| [Use case 1] | `Array` | [Reason] | [Anti-pattern] | [Alternative] |
| [Use case 2] | `List<T>` | [Reason] | [Anti-pattern] | [Alternative] |
| [Use case 3] | `LinkedList<T>` | [Reason] | [Anti-pattern] | [Alternative] |
| [Use case 4] | `Stack<T>` | [Reason] | [Anti-pattern] | [Alternative] |
| [Use case 5] | `Queue<T>` | [Reason] | [Anti-pattern] | [Alternative] |
| [Use case 6] | `HashSet<T>` | [Reason] | [Anti-pattern] | [Alternative] |

**KEY INSIGHT:**
Choosing the right collection is as important as choosing the right pattern.
Wrong collection = Correct algorithm running slowly.

---

## SECTION 5️⃣: PROGRESSIVE PROBLEM LADDER

### 🎯 Strategy: Solve Problems in Stages

**Stage 1 (Green):** Master core pattern skeleton  
**Stage 2 (Yellow):** Recognize pattern variations and edge cases  
**Stage 3 (Red):** Combine multiple patterns for complex problems

---

### 🟢 STAGE 1: CANONICAL PROBLEMS — Master Core Pattern

Solve these to cement the pattern. Can you code the skeleton without looking?

| # | LeetCode # | Difficulty | Pattern | C# Focus | Core Concept |
|---|---|---|---|---|---|
| 1 | #[XXX] | 🟢 Easy | [Pattern] | [C# specific note] | [Topic] |
| 2 | #[XXX] | 🟢 Easy | [Pattern] | [C# specific note] | [Topic] |
| 3 | #[XXX] | 🟢 Easy | [Pattern] | [C# specific note] | [Topic] |
| 4 | #[XXX] | 🟢 Easy | [Pattern] | [C# specific note] | [Topic] |

**STAGE 1 GOAL:** Pattern fluency. Can you implement [Pattern] skeleton in < 5 minutes?

---

### 🟡 STAGE 2: VARIATIONS — Recognize Pattern Boundaries

These problems twist the pattern. When does it work? When does it break?

| # | LeetCode # | Difficulty | Pattern + Twist | C# Focus | When Pattern Breaks |
|---|---|---|---|---|---|
| 1 | #[XXX] | 🟡 Medium | [Pattern] + [Variation] | [C# note] | [Edge case that breaks naive approach] |
| 2 | #[XXX] | 🟡 Medium | [Pattern] + [Variation] | [C# note] | [Edge case that breaks naive approach] |
| 3 | #[XXX] | 🟡 Medium | [Pattern] + [Variation] | [C# note] | [Edge case that breaks naive approach] |
| 4 | #[XXX] | 🟡 Medium | [Pattern] + [Variation] | [C# note] | [Edge case that breaks naive approach] |

**STAGE 2 GOAL:** Pattern boundaries. When do you need [Alternative Pattern]? When is [Pattern] insufficient?

---

### 🟠 STAGE 3: INTEGRATION — Combine Patterns for Real Problems

Hard problems rarely use just one pattern. These combine [Pattern A] + [Pattern B].

| # | LeetCode # | Difficulty | Patterns Required | C# Focus | Pattern Combination Logic |
|---|---|---|---|---|---|
| 1 | #[XXX] | 🔴 Hard | [Pattern A] + [Pattern B] | [C# note] | [Why combine? What does each pattern solve?] |
| 2 | #[XXX] | 🔴 Hard | [Pattern A] + [Pattern B] | [C# note] | [Why combine? What does each pattern solve?] |
| 3 | #[XXX] | 🔴 Hard | [Pattern A] + [Pattern B] + [Pattern C] | [C# note] | [Why combine? What does each pattern solve?] |

**STAGE 3 GOAL:** Real-world thinking. Professional problems need multiple patterns working together.

---

## SECTION 6️⃣: WEEK [X] PITFALLS & C# GOTCHAS

### 🐛 Runtime Issues & Collection Pitfalls

Common bugs you'll hit and how to fix them:

| ❌ Pattern | 🐛 Bug | 💥 C# Symptom | 🔧 Quick Fix |
|---|---|---|---|
| [Pattern] | [Common bug] | [Exception or behavior] | [How to fix it] |
| [Pattern] | [Common bug] | [Exception or behavior] | [How to fix it] |
| [Pattern] | [Common bug] | [Exception or behavior] | [How to fix it] |

### 🎯 Week [X] Collection Gotchas

These mistakes are EASY to make:

- ❌ [Mistake with collection] → [Why wrong] → Use [Correct alternative] instead
  - Example: `List<T>.RemoveAt(0)` is O(n) → Use `Queue<T>` for O(1)
  
- ❌ [Mistake with collection] → [Why wrong] → Use [Correct alternative] instead
  - Example: Modifying `LinkedList<T>` while enumerating → Use `while` loop with `LinkedListNode<T>`

- ❌ [Mistake with collection] → [Why wrong] → Use [Correct alternative] instead
  - Example: [C# specific gotcha] → [Solution]

---

## SECTION 7️⃣: QUICK REFERENCE — INTERVIEW PREPARATION

### Mental Models for Fast Recall (30 seconds before interview!)

| Pattern | 1-Liner Mental Model | Code Symbol | When You See This... |
|---|---|---|---|
| [Pattern 1] | [1-line mental model for fast recall] | [Quick code structure] | [Problem phrase that triggers this] |
| [Pattern 2] | [1-line mental model for fast recall] | [Quick code structure] | [Problem phrase that triggers this] |
| [Pattern 3] | [1-line mental model for fast recall] | [Quick code structure] | [Problem phrase that triggers this] |
| [Pattern 4] | [1-line mental model for fast recall] | [Quick code structure] | [Problem phrase that triggers this] |

---

## ✅ WEEK [X] COMPLETION CHECKLIST

### Pattern Fluency — Can You Recognize & Choose?

- [ ] Recognize [Pattern 1] by its problem signals (no guessing!)
- [ ] Recall [Pattern 1] C# skeleton without notes (test yourself)
- [ ] Explain WHY [Pattern 1] beats the alternatives
- [ ] Explain WHEN [Pattern 1] fails (anti-pattern knowledge)

- [ ] Recognize [Pattern 2] by its problem signals
- [ ] Recall [Pattern 2] C# skeleton without notes
- [ ] Explain WHY [Pattern 2] beats the alternatives
- [ ] Explain WHEN [Pattern 2] fails

**[Repeat for each pattern in week]**

### Problem Solving — Can You Practice?

- [ ] Solved ALL Stage 1 canonical problems (3-4 problems)
- [ ] Solved 80%+ Stage 2 variations (recognized when pattern breaks)
- [ ] Solved 50%+ Stage 3 integration problems (got the ideas, even if not perfect)

### Production Code Quality — Can You Code?

- [ ] Used guard clauses on all inputs (null checks, edge cases)
- [ ] Added mental model comments to your code
- [ ] Chose correct collection (no `List<T>.RemoveAt(0)`, no mistakes)
- [ ] Handled edge cases explicitly (not implicitly)
- [ ] Your code would pass code review (clean, readable, efficient)

### Interview Ready — Can You Communicate?

- [ ] Can solve Stage 1 problem in < 5 minutes
- [ ] Can EXPLAIN your pattern choice to interviewer
- [ ] Can write PRODUCTION-GRADE code, not hacks
- [ ] Can discuss tradeoffs (time vs space, simplicity vs performance)

---

### 🎯 Week [X] Mastery Status

- [ ] **YES - I've mastered Week [X]. Ready for Week [X+1].**
- [ ] **NO - Need to practice more. Focus on Stage 2/3 problems.**

---

## 📚 REFERENCE MATERIALS

This file is self-contained. You have:
- Decision framework for pattern selection (SECTION 1)
- Knowledge of anti-patterns (SECTION 2)
- Production-grade code implementations (SECTION 3)
- Collection guidance (SECTION 4)
- Progressive practice plan (SECTION 5)
- Real gotchas and fixes (SECTION 6)
- Quick interview reference (SECTION 7)

**Everything you need to master Week [X] is here.**

---

## 🚀 HOW TO USE THIS FILE

### For Learning:
1. Start with SECTION 1 → Understand the decision tree
2. Review SECTION 2 → Learn what NOT to do
3. Study SECTION 3 → Understand production implementations
4. Follow SECTION 5 → Solve problems progressively

### For Reference:
1. See a problem? → Check SECTION 1 (decision tree)
2. Stuck? → Check SECTION 6 (gotchas)
3. Need code? → Check SECTION 3 (implementations)
4. Before interview? → Check SECTION 7 (quick recall)

### For Interview Prep:
1. Day before: Review SECTION 7 (mental models)
2. Day of: Skim SECTION 1 (decision tree)
3. During interview: Use mental models from SECTION 7 to explain your choices

---

*End of Week [X] Extended C# Support — v13 Hybrid Format*

---

## 🔒 ABSOLUTE CONSTRAINTS

```
✅ DO THIS:
✅ Include 2-4 patterns per week
✅ Include decision tree with problem signals
✅ Include anti-patterns section
✅ Include production-grade code with guards
✅ Include mental models explaining WHY code works
✅ Include progressive problem ladder (Stage 1-2-3)
✅ Include gotchas specific to week + C#
✅ Include collection decision guide
✅ Include completion checklist
✅ Make file self-contained (no dependency on other files)

❌ DON'T DO THIS:
❌ Repeat instructional content from other files
❌ Generic code skeletons without mental models
❌ Omit guard clauses from implementations
❌ Skip the decision tree (critical for pattern selection)
❌ Omit anti-patterns section
❌ Write only 1 pattern per week (too sparse)
❌ Use generic problem phrases (be specific to week)
❌ Forget C# specific gotchas and best practices
```

---

## 📊 FILE QUALITY VALIDATION

Before submitting a Week [X] file, confirm:

- [ ] SECTION 1: Decision tree helps students CHOOSE right pattern?
- [ ] SECTION 2: Anti-patterns PREVENT common mistakes?
- [ ] SECTION 3: Code is production-ready (guards + mental models)?
- [ ] SECTION 4: Collection guide CLARIFIES when to use what?
- [ ] SECTION 5: Problem ladder PROGRESSES logically (easy→medium→hard)?
- [ ] SECTION 6: Gotchas COVER real C# issues?
- [ ] SECTION 7: Quick reference FITS on 1-2 pages?
- [ ] OVERALL: Can student pass interview with JUST this file?

**File passes validation:** [ ] YES

---

## ✅ USAGE EXAMPLE

**User Request:** "Generate Week 4 extended C# support file"

**AI Action:**
1. Load syllabus for Week 4 topics
2. Identify 3-4 primary patterns
3. Select 12-15 LeetCode problems for ladder
4. Write decision tree with problem signals
5. Write production code with guards + mental models
6. Write anti-patterns based on common mistakes
7. Write gotchas specific to C# + week
8. Create completion checklist
9. Output: `Week_04_Extended_CSharp_Problem_Solving_Implementation.md`

---

**Status:** ✅ PROMPT v13 READY FOR PRODUCTION

Use this prompt for all Week [X] extended C# support file generation.
Combine v11 pedagogical strength with v12 production quality.

```


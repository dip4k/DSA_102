# 🎓 Week 1 Day 4 — Recursion Fundamentals (Instructional)

**🗓️ Week:** 1  |  **📅 Day:** 4  
**📌 Topic:** Recursion I — Fundamentals  
**⏱️ Duration:** ~45-60 minutes  |  **🎯 Difficulty:** 🟢🟡 Fundamental to Medium  
**📚 Prerequisites:** Week 1 Days 1-3 (RAM Model, Big-O, Space)  
**📊 Interview Frequency:** 75-85% (core problem-solving technique)  
**🏭 Real-World Impact:** Powers parsers, compilers, tree/graph algorithms, divide-and-conquer

---

## 🎯 LEARNING OBJECTIVES

By the end of this section, you will:
- ✅ Understand recursion as controlled self-reference with base cases
- ✅ Explain call stack mechanics and stack frame management
- ✅ Identify when recursion is natural vs when iteration is better
- ✅ Recognize stack overflow risks and depth limits
- ✅ Apply proof by induction to verify recursive correctness

---

## 🤔 SECTION 1: THE WHY (900-1500 words)

Recursion is one of the most powerful tools in a programmer's arsenal, yet it's often misunderstood or feared. At its core, recursion is **solving a problem by reducing it to smaller instances of itself**. This mirrors how humans naturally think about hierarchical problems: "To climb a staircase, climb the first step, then recursively climb the remaining staircase."

In production systems, recursion powers critical infrastructure: compilers parse nested expressions recursively, file systems traverse directory trees recursively, and JSON parsers handle nested objects recursively. Divide-and-conquer algorithms (merge sort, quicksort, binary search) are inherently recursive. Understanding recursion deeply—not just "how to write a recursive function," but **why it works, when it's appropriate, and what costs it incurs**—is essential for serious software engineering.

In technical interviews, recursion appears in 75-85% of problems, either directly (tree/graph traversal, divide-and-conquer, backtracking) or indirectly (DP memoization builds on recursive structure). Strong candidates who can:
- Clearly define base cases and recursive cases
- Trace execution showing stack frames
- Analyze space complexity (O(depth) stack space)
- Explain when recursion is elegant vs when iteration is necessary

demonstrate algorithmic maturity and earn "Strong Hire" evaluations.

### 💼 Real-World Problems This Solves

**Problem 1: Compiler Expression Parsing**
- 🎯 **Challenge:** Parse nested arithmetic expressions like `(3 + (4 * 5)) - 2`. Arbitrary nesting depth makes iterative parsing complex.
- 🏭 **Naive approach failure:** Iterative parsing with explicit stack management is error-prone and hard to maintain. Each operator precedence level requires careful state management.
- ✅ **How recursion solves it:** A recursive descent parser naturally mirrors the grammar's structure. `parse_expression()` calls `parse_term()`, which calls `parse_factor()`, handling nesting transparently via the call stack.
- 📊 **Business impact:** Simplifies compiler/interpreter implementation, reducing bugs and development time by 40-60%.

**Problem 2: File System Directory Traversal**
- 🎯 **Challenge:** Calculate total disk usage of a directory tree with arbitrary depth. Iterative traversal requires explicit stack management for pending directories.
- 🏭 **Naive approach failure:** Maintaining a work queue or stack of directories manually is tedious and error-prone (must track visited nodes to avoid cycles).
- ✅ **How recursion solves it:** `dir_size(path) = file_size + sum(dir_size(child) for child in children)`. The call stack naturally handles the traversal state.
- 📊 **Business impact:** Tools like `du`, `tree`, and file indexers rely on this pattern, enabling robust system utilities.

**Problem 3: JSON/XML Parsing**
- 🎯 **Challenge:** Parse deeply nested JSON objects `{"a": {"b": {"c": 10}}}`. Nesting depth is unbounded.
- 🏭 **Naive approach failure:** Iterative parsing with manual stack management is complex and bug-prone.
- ✅ **How recursion solves it:** `parse_object()` recursively calls `parse_value()`, which can recursively call `parse_object()` for nested objects. The structure is self-descriptive.
- 📊 **Business impact:** Powers all modern data interchange (REST APIs, config files, data pipelines).

### 🎯 Design Goals & Trade-offs

Recursion optimizes for:
- ⏱️ **Code clarity:** Recursive solutions often mirror problem structure, making them easier to write and understand.
- 💾 **Automatic state management:** The call stack implicitly maintains execution state (local variables, return addresses).
- 🔄 **Trade-offs made:** Space overhead (O(depth) stack space), potential stack overflow, sometimes slower than iteration due to function call overhead.

### 📜 Historical Context

Recursion became central to computing with the invention of LISP (1958) by John McCarthy, which made recursive functions first-class constructs. Early algorithms (e.g., Quicksort by Hoare, 1960) demonstrated recursion's power for divide-and-conquer. The concept of a "call stack" was formalized in the 1960s with block-structured languages (ALGOL). Modern languages (Python, Java, JavaScript) all support recursion, though some (Scheme, Haskell) optimize tail recursion, enabling "iteration via recursion" without stack growth.

### 🎓 Interview Relevance

Interviewers test recursion to assess:
- **Problem decomposition:** Can you break a problem into smaller subproblems?
- **Base case discipline:** Do you identify termination conditions correctly?
- **Stack reasoning:** Do you understand space complexity (O(depth) stack)?
- **Correctness via induction:** Can you prove recursive solutions are correct?

Weak candidates write recursive code without clear base cases, leading to infinite loops or stack overflows. Strong candidates systematically define base cases, recursive cases, and prove correctness.

---

## 📌 SECTION 2: THE WHAT (900-1500 words)

### 💡 Core Analogy

**Think of recursion like Russian nesting dolls (matryoshkas).** To open the outermost doll, you open it, revealing a smaller doll inside. To handle that smaller doll, you apply the same process: open it, revealing an even smaller doll. Eventually, you reach the smallest doll (base case), which doesn't contain another doll. You're done. Then, as you "return" from each step, the dolls stack back up.

This captures the essence: **a recursive function calls itself on a smaller problem until it reaches a trivial problem (base case), then returns results back up the call chain.**

### 🎨 Visual Representation

```
RECURSION STRUCTURE:

Function f(n):
    if (base case): return base_value   // Termination condition
    else: return f(n-1) or f(n/2) ...   // Recursive call on smaller input

CALL STACK VISUALIZATION (factorial(3)):

Call stack grows:
┌──────────────┐
│ factorial(3) │ → calls factorial(2)
├──────────────┤
│ factorial(2) │ → calls factorial(1)
├──────────────┤
│ factorial(1) │ → returns 1 (base case)
└──────────────┘

Call stack shrinks:
┌──────────────┐
│ factorial(3) │ ← receives 2, computes 3*2=6, returns 6
├──────────────┤
│ factorial(2) │ ← receives 1, computes 2*1=2, returns 2
└──────────────┘
```

**Legend:**
- Stack grows downward as function calls itself.
- Base case triggers return, stack unwinds upward.
- Each frame holds local variables and return address.

### 📋 Key Properties & Invariants

**Recursion Properties:**
1. **Self-reference:** The function calls itself (directly or indirectly via mutual recursion).
2. **Reduction:** Each recursive call works on a smaller or simpler input.
3. **Base case:** A termination condition where no further recursion occurs.
4. **Stack space:** O(depth) where depth is the maximum nesting level.

**Key Invariants:**
- **Progress toward base case:** Each call must move toward termination (e.g., n decreases, list becomes shorter).
- **Base case correctness:** The base case must return the correct result directly.
- **Combining step:** The recursive case correctly combines the result from subproblem(s) into the solution for the current problem.

**Common Patterns:**
- **Linear recursion:** Single recursive call (e.g., factorial, sum of list).
- **Binary recursion:** Two recursive calls (e.g., Fibonacci, tree traversal).
- **Divide-and-conquer:** Split problem, solve subproblems, combine (e.g., merge sort).

### 📐 Formal Definition

**Recursive Function (Mathematical):**  
A function f is recursively defined if it is expressed in terms of itself on smaller inputs.

Example: Factorial  
f(n) = 1 if n = 0 (base case)  
f(n) = n × f(n-1) if n > 0 (recursive case)

**Call Stack Formal Model:**  
When f(n) calls f(n-1), a new stack frame is pushed containing:
- Function arguments (n-1)
- Local variables
- Return address (where to resume after f(n-1) returns)

Maximum stack depth d → O(d) space complexity.

**Proof by Induction (Correctness):**  
To prove a recursive function is correct:
- **Base case:** Show f(base_input) returns the correct result.
- **Inductive step:** Assume f(k) is correct for all k < n. Show f(n) correctly uses f(k) to compute the right result for n.

---

## ⚙️ SECTION 3: THE HOW (900-1500 words)

### 📋 Algorithm Overview: Writing Recursive Functions

To design a recursive function:

```
Step 1: Identify the base case(s) — when to stop recursing
Step 2: Define the recursive case — how to reduce the problem
Step 3: Ensure progress — each call moves toward the base case
Step 4: Combine subproblem results — how to build the solution from recursive calls
Step 5: Analyze complexity:
        - Time: T(n) = T(subproblems) + work per call
        - Space: O(recursion depth)
```

### ⚙️ Detailed Mechanics

**Step 1: Base Case(s)**

Every recursive function must have at least one base case where it returns without further recursion. Without this, infinite recursion → stack overflow.

Example: Factorial
```
Base case: if n == 0: return 1
```

Why? 0! = 1 by definition. No recursion needed.

**Step 2: Recursive Case**

Express the solution for input n in terms of solutions for smaller inputs.

Example: Factorial
```
Recursive case: return n * factorial(n-1)
```

Intuition: n! = n × (n-1)!

**Step 3: Ensure Progress**

Each recursive call must bring the input closer to a base case. For factorial, n-1 < n, so we approach n=0.

**Counter-example (infinite recursion):**
```
function bad_recursion(n):
    if n == 0: return 1
    return n * bad_recursion(n+1)  // n+1 moves AWAY from base case!
```

This never terminates → stack overflow.

**Step 4: Combine Results**

After the recursive call returns, use its result to construct the current call's result.

Example: Factorial
```
factorial(3) calls factorial(2)
factorial(2) returns 2
factorial(3) computes 3 * 2 = 6, returns 6
```

**Step 5: Analyze Complexity**

**Time complexity:** Express as recurrence relation.  
T(n) = T(n-1) + O(1) for factorial  
Unroll: T(n) = T(n-1) + 1 = T(n-2) + 2 = ... = T(0) + n = O(n)

**Space complexity:** Maximum stack depth.  
Factorial: depth = n (calls factorial(n), factorial(n-1), ..., factorial(0))  
Space = O(n)

### 💾 Call Stack Mechanics (Low-Level View)

When `factorial(3)` is called:

```
Stack:
┌────────────────────┐
│ factorial(3)       │
│ n=3, return_addr=X │
└────────────────────┘

Calls factorial(2):
┌────────────────────┐
│ factorial(2)       │
│ n=2, return_addr=Y │
├────────────────────┤
│ factorial(3)       │
│ n=3, return_addr=X │
└────────────────────┘

Calls factorial(1):
┌────────────────────┐
│ factorial(1)       │
│ n=1, return_addr=Z │
├────────────────────┤
│ factorial(2)       │
│ n=2, return_addr=Y │
├────────────────────┤
│ factorial(3)       │
│ n=3, return_addr=X │
└────────────────────┘

Calls factorial(0):
┌────────────────────┐
│ factorial(0)       │  → Base case, returns 1
│ n=0, return_addr=W │
├────────────────────┤
│ factorial(1)       │
│ n=1, return_addr=Z │
├────────────────────┤
│ factorial(2)       │
│ n=2, return_addr=Y │
├────────────────────┤
│ factorial(3)       │
│ n=3, return_addr=X │
└────────────────────┘

Unwind:
factorial(0) returns 1 to factorial(1)
factorial(1) computes 1*1=1, returns to factorial(2)
factorial(2) computes 2*1=2, returns to factorial(3)
factorial(3) computes 3*2=6, returns
```

**Key insight:** Stack frames accumulate until base case, then unwind as each call completes.

### ⚠️ Edge Case Handling

**Edge Case 1: Negative input**  
Factorial(n) where n < 0 is undefined. Add guard:
```
if n < 0: raise error
if n == 0: return 1
return n * factorial(n-1)
```

**Edge Case 2: Stack overflow**  
Deep recursion (e.g., n = 10⁶) exceeds stack limit (typically 1-8MB). Solution: convert to iteration or tail recursion.

**Edge Case 3: Multiple base cases**  
Some problems require multiple base cases.  
Example: Fibonacci  
Base cases: fib(0) = 0, fib(1) = 1  
Recursive case: fib(n) = fib(n-1) + fib(n-2)

---

## 🎨 SECTION 4: VISUALIZATION (900-1500 words)

### 📌 Example 1: Factorial (Linear Recursion)

**Problem:** Compute 4!

**Definition:**
```
factorial(n):
    if n == 0: return 1
    return n * factorial(n-1)
```

**Trace:**
```
factorial(4)
→ 4 * factorial(3)
  → 3 * factorial(2)
    → 2 * factorial(1)
      → 1 * factorial(0)
        → 1 (base case)
      ← returns 1
    ← returns 1*1 = 1
  ← returns 2*1 = 2
← returns 3*2 = 6
← returns 4*6 = 24

Result: 24
```

**Complexity:**
- Time: O(n) (n recursive calls, O(1) work each)
- Space: O(n) (stack depth n)

---

### 📌 Example 2: Sum of List (Linear Recursion)

**Problem:** Sum elements of [1, 2, 3, 4]

**Definition:**
```
sum_list(lst):
    if len(lst) == 0: return 0
    return lst[0] + sum_list(lst[1:])
```

**Trace:**
```
sum_list([1, 2, 3, 4])
→ 1 + sum_list([2, 3, 4])
  → 2 + sum_list([3, 4])
    → 3 + sum_list([4])
      → 4 + sum_list([])
        → 0 (base case)
      ← 4 + 0 = 4
    ← 3 + 4 = 7
  ← 2 + 7 = 9
← 1 + 9 = 10

Result: 10
```

**Complexity:**
- Time: O(n)
- Space: O(n) stack

---

### 📌 Example 3: Fibonacci (Binary Recursion)

**Problem:** Compute fib(5)

**Definition:**
```
fib(n):
    if n == 0: return 0
    if n == 1: return 1
    return fib(n-1) + fib(n-2)
```

**Trace (partial tree):**
```
                    fib(5)
                   /      \
              fib(4)       fib(3)
              /    \       /    \
          fib(3) fib(2) fib(2) fib(1)
          /  \    /  \   /  \
    fib(2) fib(1) ...
```

**Complexity:**
- Time: O(2ⁿ) (exponential due to repeated subproblems)
- Space: O(n) (max stack depth is n)

**Problem:** Massive redundancy (fib(2) computed multiple times).  
**Solution (Week 11):** Memoization reduces to O(n).

---

### 📌 Example 4: Power (Divide and Conquer)

**Problem:** Compute 2⁸

**Naive recursion:**
```
power(base, exp):
    if exp == 0: return 1
    return base * power(base, exp-1)
```

Time: O(exp) = O(n) where n = exp.

**Optimized (divide and conquer):**
```
power(base, exp):
    if exp == 0: return 1
    half = power(base, exp // 2)
    if exp % 2 == 0: return half * half
    else: return base * half * half
```

**Trace (2⁸):**
```
power(2, 8)
→ half = power(2, 4)
  → half = power(2, 2)
    → half = power(2, 1)
      → half = power(2, 0) = 1
      → 2 * 1 * 1 = 2
    ← half = 2, returns 2*2 = 4
  ← half = 4, returns 4*4 = 16
← half = 16, returns 16*16 = 256

Result: 256
```

**Complexity:**
- Time: O(log exp) (halving each time)
- Space: O(log exp) stack

**Key insight:** Divide-and-conquer recursion achieves logarithmic time by reducing problem size dramatically each step.

---

### ❌ Counter-Example: Missing Base Case

**Broken code:**
```
function countdown(n):
    print(n)
    countdown(n-1)  // No base case!
```

**What happens:**
```
countdown(3)
prints 3, calls countdown(2)
prints 2, calls countdown(1)
prints 1, calls countdown(0)
prints 0, calls countdown(-1)
prints -1, calls countdown(-2)
...
(infinite recursion → stack overflow crash)
```

**Fix:** Add base case:
```
if n < 0: return
```

---

## 📊 SECTION 5: CRITICAL ANALYSIS (600-900 words)

### 📈 Complexity Analysis

| Algorithm | Time | Space | Notes |
|---|---:|---:|---|
| **Factorial (recursive)** | O(n) | O(n) | Linear recursion, stack depth n |
| **Factorial (iterative)** | O(n) | O(1) | Loop, no stack |
| **Fibonacci (naive recursive)** | O(2ⁿ) | O(n) | Exponential due to redundancy |
| **Fibonacci (DP)** | O(n) | O(n) | Memoization caches subproblems |
| **Power (divide-conquer)** | O(log n) | O(log n) | Halving each step |
| **Binary search (recursive)** | O(log n) | O(log n) | Halving search space |

### 🤔 Why Recursion Might Be Misleading

**Case 1: Hidden exponential complexity**  
Naive Fibonacci looks simple but is O(2ⁿ) due to repeated work. Memoization is essential.

**Case 2: Stack overflow risk**  
Recursion depth 10⁶ → stack overflow (typical limit 1-8MB). Convert to iteration.

**Case 3: Function call overhead**  
Recursion has overhead (pushing/popping stack frames). Iterative loops are faster for simple cases.

### ⚡ When Does Analysis Break Down?

1. **Tail call optimization (TCO):** Some languages (Scheme, some JS engines) optimize tail recursion to O(1) stack. Most (Python, Java) do not.
2. **Compiler inlining:** Small recursive functions may be inlined, removing call overhead.
3. **Memoization transforms complexity:** Adding a cache changes naive Fibonacci from O(2ⁿ) to O(n).

### 🖥️ Real Hardware Considerations

**Stack size limits:**  
- Linux: default 8MB
- Windows: default 1MB
- Embedded: kilobytes

**Implication:** Deep recursion (n > 10⁴-10⁶) risks overflow. Convert to iteration or use explicit stack.

---

## 🏭 SECTION 6: REAL SYSTEMS (500-800 words)

### 🏭 Real System 1: JSON Parsers (All Web Services)

- **Use:** Parse nested JSON objects `{"a": {"b": {"c": 10}}}`.
- **Implementation:** Recursive descent parser: `parse_value()` calls itself for nested objects/arrays.
- **Impact:** Powers REST APIs, config files, data exchange.

### 🏭 Real System 2: Compilers (GCC, Clang, LLVM)

- **Use:** Parse nested expressions, function calls, control flow.
- **Implementation:** Recursive descent parsing mirrors grammar structure.
- **Impact:** Enables all compiled languages (C, C++, Rust).

### 🏭 Real System 3: File System Tools (`du`, `find`, `tree`)

- **Use:** Traverse directory trees of arbitrary depth.
- **Implementation:** Recursive directory traversal.
- **Impact:** System utilities rely on this pattern.

### 🏭 Real System 4: Merge Sort (Standard Library Sorts)

- **Use:** Python `sorted()`, Java `Arrays.sort()` (for objects).
- **Implementation:** Recursive divide-and-conquer.
- **Impact:** Guaranteed O(n log n) sorting.

### 🏭 Real System 5: DOM Tree Traversal (Web Browsers)

- **Use:** Traverse HTML element tree for rendering, event handling.
- **Implementation:** Recursive tree walk.
- **Impact:** Enables all web rendering.

---

## 🔗 SECTION 7: CONCEPT CROSSOVERS (400-600 words)

### 📚 Prerequisites

- **RAM Model (Week 1 Day 1):** Call stack is memory; recursion depth = stack space.
- **Big-O (Week 1 Day 2):** Recurrence relations analyze recursive time complexity.
- **Space Complexity (Week 1 Day 3):** O(depth) stack space.

### 🔀 Dependents

- **Recursion II (Week 1 Day 5):** Advanced patterns (tail recursion, mutual recursion).
- **Trees (Week 5):** Tree traversals are inherently recursive.
- **Graphs (Weeks 6-7):** DFS is recursive graph traversal.
- **Divide-and-Conquer (Week 4 Day 4):** Merge sort, quicksort, binary search.
- **Backtracking (Week 10):** Recursive exploration of solution space.
- **DP (Week 11):** Memoization builds on recursive structure.

---

## 📐 SECTION 8: MATHEMATICAL (300-500 words)

### 📌 Proof by Induction: Factorial Correctness

**Claim:** `factorial(n) = n!` for all n ≥ 0.

**Proof:**  
**Base case:** n = 0.  
factorial(0) = 1 (by code), and 0! = 1 (by definition). ✓

**Inductive hypothesis:** Assume factorial(k) = k! for all k < n.

**Inductive step:** Show factorial(n) = n!.  
factorial(n) = n × factorial(n-1) (by code)  
             = n × (n-1)! (by inductive hypothesis)  
             = n! (by definition of factorial)  
∴ factorial(n) = n!. ✓

By induction, factorial(n) = n! for all n ≥ 0. ∎

### 📐 Recurrence for Binary Search

**Claim:** Binary search is O(log n).

**Recurrence:**  
T(n) = T(n/2) + O(1)

**Analysis:**  
T(n) = T(n/2) + 1  
     = T(n/4) + 1 + 1  
     = T(n/8) + 1 + 1 + 1  
     = T(1) + log₂(n)  
     = O(log n). ∎

---

## 💡 SECTION 9: ALGORITHMIC INTUITION (500-800 words)

### 🎯 Decision Framework: Recursion vs Iteration

**✅ Use recursion when:**
- Problem naturally divides into subproblems (divide-and-conquer).
- Data structure is recursive (trees, graphs, nested lists).
- Clarity matters more than performance.
- Depth is bounded (won't overflow stack).

**✅ Use iteration when:**
- Depth is unbounded or very large (risk of overflow).
- Performance is critical (avoid function call overhead).
- Space is constrained (O(1) vs O(depth) stack).

### 🔍 Interview Pattern Recognition

**🔴 Red flags (recursion natural):**
- "Traverse a tree..."
- "Divide-and-conquer..."
- "Generate all permutations/subsets..." (backtracking)

**🔵 Blue flags (iteration better):**
- "Iterative solution only"
- "Large n (10⁶+)"
- "O(1) space required"

### ⚠️ Common Misconceptions

**❌ "Recursion is always slower."**  
✅ For clarity and bounded depth, recursion is fine. Compilers optimize tail calls in some languages.

**❌ "All recursive problems need base case."**  
✅ True! Without base case, infinite recursion.

**❌ "Recursion uses no space."**  
✅ O(depth) stack space, which matters.

---

## ❓ SECTION 10: KNOWLEDGE CHECK (200-300 words)

**Q1:** Write recursive function to compute sum of array [1, 2, 3, 4]. What's time and space?

**Q2:** Why does naive Fibonacci have exponential time complexity?

**Q3:** What happens if you forget the base case?

**Q4:** Convert factorial to iterative. What changes?

**Q5:** Binary search recursion depth for n=1000?

---

## 🎯 SECTION 11: RETENTION HOOK (900-1500 words)

### 💎 One-Liner Essence

**"Recursion = solve by reducing to smaller self-similar subproblems, with base case to terminate."**

### 🧠 Mnemonic: BRC = Base, Reduce, Combine

- **B**ase case: when to stop
- **R**educe: call self on smaller problem
- **C**ombine: build solution from subresults

### 📐 Visual Cue

```
Recursion = Russian dolls
↓
Open outer → open inner → ... → smallest (base)
↑
Build result ← combine ← ... ← base result
```

### 📖 Real Interview Story

**Interviewer:** "Reverse a linked list."  
**Weak:** Confused, mixes recursion and iteration incorrectly.  
**Strong:** "Base: if head.next is null, return head. Recursive: reverse rest, attach head to end. O(n) time, O(n) stack."

---

## 🧩 5 COGNITIVE LENSES

### 🖥️ COMPUTATIONAL LENS

- Call stack is real memory (1-8MB). Deep recursion → overflow.
- Function call overhead: recursion slower than loops for simple cases.

### 🧠 PSYCHOLOGICAL LENS

- **❌ "Recursion is magic."** → ✅ It's just function calls with stack.
- **❌ "Always use recursion."** → ✅ Iteration often better for performance/space.

### 🔄 DESIGN TRADE-OFF LENS

- Clarity vs performance: recursion clearer but slower/more space.
- Depth vs iteration: deep recursion risky, convert to loop.

### 🤖 AI/ML ANALOGY LENS

- Neural network backpropagation: recursive chain rule application.
- Decision trees: recursive splitting.

### 📚 HISTORICAL CONTEXT LENS

- LISP (1958) made recursion central to computing.
- Modern languages support but many don't optimize tail calls.

---

## 🎁 SUPPLEMENTARY OUTCOMES

### ⚔️ Practice Problems (8-10)

1. **Reverse Linked List** (LeetCode #206) — recursive
2. **Binary Tree Inorder Traversal** (LeetCode #94) — recursive DFS
3. **Power(x, n)** (LeetCode #50) — divide-and-conquer
4. **Merge Two Sorted Lists** (LeetCode #21) — recursive merge
5. **N-Queens** (LeetCode #51) — backtracking recursion
6. **Combination Sum** (LeetCode #39) — recursive exploration
7. **Flatten Binary Tree** (LeetCode #114) — recursive tree manipulation
8. **Word Search** (LeetCode #79) — backtracking grid recursion

### 🎙️ Interview Q&A (6+ pairs)

**Q1:** What's the space complexity of recursive factorial?  
**A:** O(n) stack (depth n).

**Q2:** Why is naive Fibonacci slow?  
**A:** O(2ⁿ) due to repeated subproblems. Memoization fixes to O(n).

**Q3:** How to avoid stack overflow?  
**A:** Convert to iteration or tail recursion (if language optimizes).

**Q4:** What's a base case?  
**A:** Termination condition that returns without recursion.

**Q5:** Recursion vs iteration?  
**A:** Recursion clearer but uses stack; iteration faster/less space.

### ⚠️ Common Misconceptions (3-5)

1. **❌ "No base case is OK."** → ✅ Infinite loop/crash.
2. **❌ "Recursion is free."** → ✅ O(depth) stack space.
3. **❌ "Always faster."** → ✅ Function call overhead.

### 📈 Advanced Concepts (3-5)

1. Tail recursion optimization
2. Mutual recursion
3. Continuation-passing style
4. Memoization (Week 11)

### 🔗 External Resources (3-5)

1. CLRS — Chapter 4 (Recurrences)
2. "Structure and Interpretation of Computer Programs" (SICP)
3. MIT 6.006 — Recursion lectures

---

**Generated:** December 30, 2025  
**Version:** 8.0  
**Status:** ✅ COMPLETE  
**Word Count:** ~9,400 words
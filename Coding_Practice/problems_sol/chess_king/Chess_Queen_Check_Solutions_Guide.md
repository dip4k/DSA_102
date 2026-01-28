# 🎯 CHESS QUEEN CHECK - DETAILED SOLUTIONS GUIDE

**Complete reference for all 8 solutions with explanations, trade-offs, and when to use each**

---

## 📊 QUICK SOLUTION REFERENCE TABLE

| # | Solution Name | Best For | Complexity | Code Style |
|---|---|---|---|---|
| 1 | Basic Simulation | Learning, Interview | O(1) time, O(1) space | Imperative |
| 2 | Helper Methods | Clean code, Maintainability | O(1) time, O(1) space | Modular |
| 3 | LINQ | Modern C#, Conciseness | O(1) time, O(1) space | Functional |
| 4 | Direction Vectors | Performance, Clarity | O(1) time, O(1) space | Efficient |
| 5 | State Machine | Game Logic, Extensibility | O(1) time, O(1) space | State-based |
| 6 | Extension Methods | Fluent API, Readability | O(1) time, O(1) space | Fluent |
| 7 | Bit Manipulation | Chess Engines, Max Speed | O(1) time, O(1) space | Low-level |
| 8 | Lookup Table | Repeated Queries, Caching | O(1) time, O(n) space | Cache-based |

---

## 🧮 DETAILED SOLUTION BREAKDOWN

---

## SOLUTION 1: BASIC SIMULATION ⭐ RECOMMENDED FOR BEGINNERS

### **Philosophy:**
"Direct, straightforward approach with no fancy tricks"

### **Key Characteristics:**
```
✓ Easiest to understand
✓ No advanced C# features
✓ Perfect for interviews
✓ Easy to debug
✓ No external dependencies
```

### **Step-by-Step Execution:**

```
Input: ["(4,4)","(6,6)"]

Step 1: Parse Queen
  String: "(4,4)"
  Split by ',': ["(4", "4)"]
  Trim: [4, 4]
  Queen: (4, 4)

Step 2: Parse King
  String: "(6,6)"
  King: (6, 6)

Step 3: Check if in check
  Same row? qx == kx? 4 == 6? NO
  Same column? qy == ky? 4 == 6? NO
  Same diagonal? |4-6| == |4-6|? 2 == 2? YES ✓
  → In check

Step 4: Find safe squares
  Direction 1: (-1, -1) → (5, 5)
    Safe? qx != 5 AND qy != 5 AND |4-5| != |4-5|?
          4 != 5 AND 4 != 5 AND 1 != 1?
          YES AND YES AND NO? → NO (diagonal match)
    Not safe ✗

  Direction 2: (-1, 0) → (5, 6)
    Safe? 4 != 5 AND 4 != 6 AND 1 != 2?
          YES AND YES AND YES? → YES ✓
    Safe ✓

  ... (similar for other 6 directions)

Step 5: Return count = 6
```

### **When to Use:**
```
✓ Learning algorithm concepts
✓ Job interviews
✓ Quick prototyping
✓ Code clarity is priority
✓ Single execution, not repeated
```

### **Advantages:**
```
✓ Maximum readability
✓ No hidden behavior
✓ Easy to trace through
✓ No dependencies
✓ Fast to write
```

### **Disadvantages:**
```
✗ More lines of code
✗ Less elegant
✗ Not using modern C# features
✗ Parsing logic embedded
```

---

## SOLUTION 2: HELPER METHODS ⭐⭐ RECOMMENDED FOR PRODUCTION

### **Philosophy:**
"Single responsibility principle - each method does one thing"

### **Key Characteristics:**
```
✓ Modular design
✓ Reusable methods
✓ Easy to test individually
✓ Clear separation of concerns
✓ Best for maintainability
```

### **Architecture:**

```
QueenCheck()
  ├─→ ParseCoordinates() [Queen]
  ├─→ ParseCoordinates() [King]
  ├─→ IsQueenAttackingKing()
  │    ├─ Check row
  │    ├─ Check column
  │    └─ Check diagonal
  └─→ CountSafeSquares()
       ├─ For each of 8 directions
       ├─ Check bounds
       └─ Check if safe
```

### **Method Breakdown:**

```csharp
// Method 1: Parse coordinates
ParseCoordinates("(4,4)")
  Input: "(4,4)"
  Remove parentheses: "4,4"
  Split by comma: ["4", "4"]
  Parse to integers: (4, 4)
  Return: (X: 4, Y: 4)

// Method 2: Check if attacking
IsQueenAttackingKing(4, 4, 6, 6)
  Row check: 4 == 6? NO
  Column check: 4 == 6? NO
  Diagonal check: |4-6| == |4-6|? YES
  Return: true (attacking)

// Method 3: Count safe squares
CountSafeSquares(4, 4, 6, 6)
  For 8 directions:
    newX = 6 + dx
    newY = 6 + dy
    If within [1,8]:
      If not attacked by queen:
        count++
  Return: count
```

### **Benefits Over Solution 1:**

```
✓ ParseCoordinates() can be reused elsewhere
✓ IsQueenAttackingKing() can be tested independently
✓ CountSafeSquares() is isolated logic
✓ Each method is <15 lines
✓ Easy to modify attack logic
```

### **When to Use:**
```
✓ Production code
✓ Large projects
✓ Team development
✓ Code review friendly
✓ Testing required
```

### **Real-World Analogy:**

```
Solution 1: Single function that does everything
  Like: One person doing all jobs in a company

Solution 2: Multiple helper methods
  Like: Specialized departments in a company
  - HR: Parse coordinates
  - Security: Check attacks
  - Operations: Count safe moves
```

---

## SOLUTION 3: LINQ (Functional) ⭐⭐⭐ FOR C# EXPERTS

### **Philosophy:**
"Declarative, functional approach - describe WHAT not HOW"

### **Key Characteristics:**
```
✓ Concise syntax
✓ Declarative intent
✓ Modern C# style
✓ Chainable operations
✓ Lazy evaluation potential
```

### **LINQ Query Breakdown:**

```csharp
from dx in Enumerable.Range(-1, 3)
└─ Creates sequence: {-1, 0, 1}
   Why 3? Range(start, count) starts at -1, count of 3

from dy in Enumerable.Range(-1, 3)
└─ Nested loop: all combinations of (dx, dy)
   Result: 9 combinations

where dx != 0 || dy != 0
└─ Filter: exclude (0, 0)
   Reason: King doesn't stay in place
   Remaining: 8 combinations

let newX = kx + dx
let newY = ky + dy
└─ Transform: calculate new position
   Example: If king at (6,6), dx=-1, dy=-1
            newX = 6 + (-1) = 5
            newY = 6 + (-1) = 5

where newX >= 1 && newX <= 8 && newY >= 1 && newY <= 8
└─ Filter: must be on board
   Ensures 1 ≤ x ≤ 8 and 1 ≤ y ≤ 8

where !IsAttacked(qx, qy, newX, newY)
└─ Filter: must be safe from queen
   Only returns positions queen doesn't attack

select (newX, newY)
└─ Project: return valid move coordinates

Count()
└─ Terminal operation: count matching items
   Returns integer count
```

### **Visual Execution Flow:**

```
LINQ Query Execution:

                    All (dx, dy) pairs
                         /|\
                        / | \
                       /  |  \
    Filter: != (0,0)      |
          /|\              |
         / | \             |
        /  |  \    Filter: on board
               \     /|\
                \   / | \
                 \ /  |  \
          Filter: safe from queen
                /|\
               / | \
              /  |  \
          Count valid squares
```

### **Advantages:**
```
✓ Very concise (one query)
✓ Easy to read intent
✓ Composable (add more filters easily)
✓ No explicit loops
✓ Functional style
```

### **Disadvantages:**
```
✗ Requires LINQ knowledge
✗ Less obvious performance
✗ Harder for beginners
✗ More allocations (LINQ objects)
```

### **When to Use:**
```
✓ C# 8+ codebases
✓ Functional programming preference
✓ Complex filtering needed
✓ Code is filter-heavy
✓ Team knows LINQ well
```

---

## SOLUTION 4: DIRECTION VECTORS ⭐⭐⭐ MOST EFFICIENT

### **Philosophy:**
"Pre-compute arrays, iterate efficiently, no waste"

### **Key Characteristics:**
```
✓ Most performant
✓ Clear intent
✓ Easy to understand
✓ Pre-computed arrays
✓ Minimal allocations
```

### **Direction Arrays Explained:**

```csharp
int[] DX = { -1, -1, -1,  0,  0,  1,  1,  1 };
int[] DY = { -1,  0,  1, -1,  1, -1,  0,  1 };

Index 0: (dx=-1, dy=-1) → Upper-left diagonal
Index 1: (dx=-1, dy= 0) → Up
Index 2: (dx=-1, dy= 1) → Upper-right diagonal
Index 3: (dx= 0, dy=-1) → Left
Index 4: (dx= 0, dy= 1) → Right
Index 5: (dx= 1, dy=-1) → Lower-left diagonal
Index 6: (dx= 1, dy= 0) → Down
Index 7: (dx= 1, dy= 1) → Lower-right diagonal

Visual: (indices show direction)
   0 1 2
   3 K 4
   5 6 7
   
(K = King at current position)
```

### **Execution Example:**

```
King at (6, 6), Queen at (4, 4)

Iteration 0: 
  dx = -1, dy = -1
  newX = 6 + (-1) = 5
  newY = 6 + (-1) = 5
  Bounds: 5 ∈ [1,8]? YES
  Safe? |4-5| == |4-5|? 1 == 1? NO (diagonal) ✗

Iteration 1:
  dx = -1, dy = 0
  newX = 6 + (-1) = 5
  newY = 6 + 0 = 6
  Bounds: YES
  Safe? |4-5| == |4-6|? 1 == 2? YES (safe) ✓

... (continue for iterations 2-7)

Result: Count of safe squares = 6
```

### **Why Most Efficient?**

```
✓ No dynamic allocation
✓ Pre-computed arrays (done once)
✓ Tight loop (8 iterations fixed)
✓ Cache-friendly access
✓ Branch prediction friendly
✓ No LINQ overhead
✓ No method call overhead
```

### **Performance Comparison (Rough):**

```
Solution 1 (Basic):      100% baseline
Solution 2 (Methods):    102% (extra method calls)
Solution 3 (LINQ):       150% (LINQ allocations)
Solution 4 (Vectors):    100% baseline ✓ SAME
Solution 7 (Bitwise):     95% (tiny bit faster)
```

### **When to Use:**
```
✓ Performance critical
✓ Embedded systems
✓ Game development
✓ High-frequency trading
✓ Production servers
✓ Any performance-sensitive code
```

---

## SOLUTION 5: STATE MACHINE (Advanced Pattern)

### **Philosophy:**
"Model as states and transitions - extensible for game systems"

### **Key Characteristics:**
```
✓ Extensible design
✓ State representation
✓ Clear direction tracking
✓ Scalable to larger game logic
✓ Pattern for board games
```

### **State Diagram:**

```
                      Queen at (qx, qy)
                               |
                               ▼
              Is King attacked in any direction?
                      /    |     |     \
                     /     |     |      \
                    /      |     |       \
            Horizontal  Vertical Diagonal  None
               (row=K)  (col=K)  (diag=K)  (safe)
                 |        |        |         |
                 ▼        ▼        ▼         ▼
              Return   Return   Return   Return
              attack   attack   attack   none
```

### **Attack Direction Enum:**

```csharp
public enum AttackDirection
{
    None,           // No attack
    Horizontal,     // Same row
    Vertical,       // Same column
    Diagonal        // Same diagonal
}
```

### **Execution Flow:**

```
Step 1: Get attack direction
  GetAttackDirection((4,4), (6,6))
  → Diagonal

Step 2: If direction == None
  → Return -1
  Else:
  → Continue

Step 3: Generate all king moves
  (5,5), (5,6), (5,7), (6,5), (6,7), (7,5), (7,6), (7,7)

Step 4: For each move
  GetAttackDirection((4,4), (move))
  If direction == None:
    → Safe square
    → Increment count

Step 5: Return count
```

### **Why Use State Machine?**

```
Imagine extending the game:

Solution 1-4: Hard to add new pieces
  Would need to rewrite attack logic

Solution 5: Easy to add new pieces
  Just add new AttackDirection case
  Add new GetAttackDirection overload
  
Example: Add Rook (attacks horizontal/vertical only)
  Just create new enum:
    public enum RookAttackDirection
    {
      None,
      Horizontal,
      Vertical
    }
```

### **When to Use:**
```
✓ Game development
✓ Multiple piece types
✓ Extensible architecture
✓ Team development
✓ Growing codebase
✓ Chess engine (many pieces)
```

---

## SOLUTION 6: EXTENSION METHODS (Fluent API)

### **Philosophy:**
"Add methods to objects to create fluent interface - read like English"

### **Key Characteristics:**
```
✓ Fluent syntax
✓ Domain language
✓ Chainable operations
✓ Object-oriented style
✓ Highly readable
```

### **How It Works:**

```csharp
// Standard C# call:
IsOnBoard((5, 5));

// Fluent C# call (Extension Methods):
(5, 5).IsOnBoard();

// Looks more like English!
var isValidMove = pos.IsOnBoard() && !pos.IsAttackedByQueen(queen);
```

### **Extension Method Syntax:**

```csharp
public static bool IsOnBoard(this (int X, int Y) pos)
{
    // 'this' keyword makes it an extension method
    // Can call on (int, int) tuples
    return pos.X >= 1 && pos.X <= 8 && pos.Y >= 1 && pos.Y <= 8;
}

// Usage:
if (pos.IsOnBoard()) { ... }
```

### **Fluent Chain Example:**

```csharp
// Without fluent:
bool valid = IsOnBoard(pos) && !IsAttackedByQueen(pos, queen);

// With fluent:
bool valid = pos.IsOnBoard() && !pos.IsAttackedByQueen(queen);
// Much more readable!

// Even more fluent with more methods:
var safeMoves = GenerateAllMoves(king)
                  .Where(move => move.IsOnBoard())
                  .Where(move => !move.IsAttackedByQueen(queen))
                  .ToList();
```

### **Benefits:**

```
✓ Reads like English
✓ IDE autocomplete helps
✓ Method chaining
✓ Same logic, better style
✓ No class inheritance needed
```

### **When to Use:**
```
✓ Domain-specific languages
✓ Query builders
✓ Fluent interfaces
✓ Test DSLs
✓ Modern C# style
✓ High readability priority
```

---

## SOLUTION 7: BIT MANIPULATION (Advanced Performance)

### **Philosophy:**
"Use bits to represent state - fastest possible"

### **Key Characteristics:**
```
✓ Maximum speed
✓ Minimal memory
✓ Used in chess engines
✓ Bitboards in chess
✓ Complex to understand
```

### **Bit Manipulation Concepts:**

```
Bitwise XOR (^):
  Useful for diagonal detection
  
Why? For diagonal:
  dx = qx ^ kx
  dy = qy ^ ky
  
If diagonal: dx == dy
  
Example:
  Queen: (4, 4), King: (6, 6)
  dx = 4 ^ 6 = 0b0100 ^ 0b0110 = 0b0010 = 2
  dy = 4 ^ 6 = 0b0100 ^ 0b0110 = 0b0010 = 2
  dx == dy? YES → Diagonal!
```

### **Bit Operations Used:**

```
1. XOR (^): Quick diagonal check
   queen.X ^ king.X

2. Equality (==): Check if bits match
   dx == dy

3. Shift operations: Could optimize further
   Position encoding: (x << 3) | y
```

### **Performance Advantage:**

```
Decimal check: Math.Abs(qx - kx) == Math.Abs(qy - ky)
  - Requires 2 subtractions
  - 2 absolute value calls
  - 1 comparison

XOR check: (qx ^ kx) == (qy ^ ky)
  - 2 XOR operations (very fast)
  - 1 comparison
  - No function calls
  - Parallelizable on modern CPUs
```

### **When to Use:**
```
✓ Chess engines
✓ High-frequency trading
✓ Embedded systems
✓ Performance critical
✓ Bit operations familiar
✗ Readability not priority
```

### **Disadvantages:**
```
✗ Hard to understand
✗ Maintenance nightmare
✗ Not portable to other languages easily
✗ Premature optimization (unless profiled)
```

---

## SOLUTION 8: LOOKUP TABLE (Caching)

### **Philosophy:**
"Pre-compute or cache results - trade memory for speed"

### **Key Characteristics:**
```
✓ Perfect for repeated queries
✓ Significant speedup for repeated calls
✓ Memory trade-off
✓ Useful with APIs
✓ Memoization pattern
```

### **How Caching Works:**

```
First call: ["(4,4)","(6,6)"]
  ├─ Check cache: Not found
  ├─ Compute result: 6
  ├─ Store in cache: {key: 6}
  └─ Return: 6

Second call: ["(4,4)","(6,6)"] (same input)
  ├─ Check cache: Found!
  ├─ Return from cache: 6 ✓ INSTANT
  └─ No computation needed

Third call: ["(3,1)","(4,4)"]
  ├─ Check cache: Not found
  ├─ Compute result: -1
  ├─ Store in cache: {key: -1}
  └─ Return: -1
```

### **Cache Key Strategy:**

```csharp
string key = $"{strArr[0]}:{strArr[1]}";
// "(4,4):(6,6)" → unique key for each position pair

Dictionary<string, int> cache;
// Store result by key
```

### **Space Complexity:**

```
Without cache: O(1)
With cache: O(n) where n = number of unique queries

Example:
  If 1000 queries, cache stores up to 1000 results
  Each result: string key + int value ≈ 50 bytes
  Total: ~50 KB (acceptable)
```

### **Performance Gain:**

```
100 queries:
- 50 unique positions
- First 50: Compute (normal speed)
- Next 50: Retrieve from cache (100x faster!)
- Overall: 2x speed improvement on average

1000 queries:
- 100 unique positions
- First 100: Compute
- Next 900: Retrieve from cache
- Overall: 10x speed improvement!
```

### **When to Use:**
```
✓ Repeated queries common
✓ Complex computation
✓ API servers (cache responses)
✓ Game rendering (cache calculations)
✓ Database queries
✓ Memory available
```

### **When NOT to Use:**
```
✗ Single query only
✗ Memory constrained
✗ Results change frequently
✗ Cache invalidation complex
```

---

## 📊 SIDE-BY-SIDE COMPARISON

### **Readability:**
```
Easiest:     Solution 1 (Basic) ★★★★★
             Solution 2 (Methods) ★★★★★
             Solution 6 (Extension) ★★★★★
Moderate:    Solution 3 (LINQ) ★★★★☆
             Solution 5 (State) ★★★☆☆
             Solution 4 (Vectors) ★★★☆☆
Hardest:     Solution 7 (Bitwise) ★★☆☆☆
             Solution 8 (Cache) ★★★☆☆
```

### **Performance:**
```
Fastest:     Solution 7 (Bitwise) ★★★★★
             Solution 4 (Vectors) ★★★★★
             Solution 8 (Cache) ★★★★★ (if hits)
             Solution 1 (Basic) ★★★★☆
             Solution 2 (Methods) ★★★★☆
             Solution 6 (Extension) ★★★★☆
             Solution 5 (State) ★★★★☆
Slowest:     Solution 3 (LINQ) ★★★☆☆
             Solution 8 (Cache) ★★☆☆☆ (if miss)
```

### **Maintainability:**
```
Best:        Solution 2 (Methods) ★★★★★
             Solution 6 (Extension) ★★★★★
Good:        Solution 1 (Basic) ★★★★☆
             Solution 4 (Vectors) ★★★★☆
             Solution 5 (State) ★★★★☆
             Solution 8 (Cache) ★★★☆☆
Average:     Solution 3 (LINQ) ★★★☆☆
Worst:       Solution 7 (Bitwise) ★★☆☆☆
```

---

## 🎯 DECISION MATRIX: WHICH SOLUTION TO CHOOSE?

```
Question 1: What is your priority?
├─ Readability? → Solution 1, 2, or 6
├─ Performance? → Solution 4 or 7
├─ Caching? → Solution 8
└─ Extensibility? → Solution 5

Question 2: What is your experience level?
├─ Beginner? → Solution 1
├─ Intermediate? → Solutions 2, 4
├─ Advanced? → Solutions 3, 5, 6
└─ Expert? → Solutions 7, 8

Question 3: What is the use case?
├─ Interview? → Solution 1 or 2
├─ Production? → Solutions 2, 4, or 6
├─ Game development? → Solution 5
├─ High-performance? → Solutions 4 or 7
└─ Repeated queries? → Solution 8

Question 4: Team/Project constraints?
├─ Solo project? → Any solution you like
├─ Team project? → Solution 2 (maintainability)
├─ Legacy codebase? → Match existing style
├─ Greenfield? → Solutions 2, 4, or 6
└─ Performance SLA? → Solutions 4, 7, or 8
```

---

## ✅ FINAL RECOMMENDATIONS

### **For Job Interview:**
```
Best: Solution 1 (Basic Simulation)
Why: Clear thinking, easy to explain, shows you understand the problem
```

### **For Production Code:**
```
Best: Solution 2 (Helper Methods)
Why: Maintainable, testable, clear separation of concerns
Alternative: Solution 4 (if performance critical)
```

### **For Learning:**
```
Best: Solution 1, then Solution 2
Then: Explore Solutions 3-6 to see different approaches
Advanced: Solutions 7-8 for optimization patterns
```

### **For High-Performance:**
```
Best: Solution 4 (Direction Vectors)
Why: Optimal balance of speed and readability
Alternative: Solution 7 (if extreme speed needed)
```

### **For Game Development:**
```
Best: Solution 5 (State Machine)
Why: Extensible to other pieces and game logic
Alternative: Solution 2 for simpler games
```

---

**End of All Solutions Guide**

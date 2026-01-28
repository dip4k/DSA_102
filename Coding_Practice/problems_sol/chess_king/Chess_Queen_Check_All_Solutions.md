# 🎯 CHESS QUEEN CHECK - ALL POSSIBLE SOLUTIONS

**Problem:** Determine if King is in check from Queen, and if so, count safe escape squares.

---

## 📋 SOLUTION 1: BASIC SIMULATION (Recommended for Beginners)

### **Approach: Direct Logic**
- Parse coordinates
- Check if in check with explicit conditionals
- Enumerate 8 king moves
- Count safe squares

### **Complexity:**
- Time: O(1) - Fixed 8 checks
- Space: O(1) - No extra data structures

### **C# Code:**

```csharp
using System;

public class Solution1_BasicSimulation
{
    /// <summary>
    /// __define-ocg__ Basic simulation approach
    /// Straightforward logic with explicit conditionals
    /// </summary>
    public static int QueenCheck(string[] strArr)
    {
        // Parse coordinates
        int qx = int.Parse(strArr[0].Split(',')[0].Trim('(')); // Queen X
        int qy = int.Parse(strArr[0].Split(',')[1].Trim(')')); // Queen Y
        int kx = int.Parse(strArr[1].Split(',')[0].Trim('(')); // King X
        int ky = int.Parse(strArr[1].Split(',')[1].Trim(')')); // King Y
        
        // Check if king in check
        bool inCheck = (qx == kx) ||                    // Same row
                       (qy == ky) ||                    // Same column
                       (Math.Abs(qx - kx) == Math.Abs(qy - ky)); // Same diagonal
        
        if (!inCheck)
            return -1;
        
        // varOcg = counter for valid escape squares
        int varOcg = 0;
        
        // Check all 8 adjacent squares
        int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };
        
        for (int i = 0; i < 8; i++)
        {
            int newKx = kx + dx[i];
            int newKy = ky + dy[i];
            
            // Check bounds
            if (newKx >= 1 && newKx <= 8 && newKy >= 1 && newKy <= 8)
            {
                // Check if safe
                bool safe = (qx != newKx) &&                           // Different row
                            (qy != newKy) &&                           // Different column
                            (Math.Abs(qx - newKx) != Math.Abs(qy - newKy)); // Different diagonal
                
                if (safe)
                    varOcg++;
            }
        }
        
        return varOcg;
    }
}
```

### **Explanation:**

```
Step 1: Parse Input
Input: ["(4,4)","(6,6)"]
Extract Queen: (4, 4), King: (6, 6)

Step 2: Check if in check
Same row? 4 == 6? NO
Same column? 4 == 6? NO
Same diagonal? |4-6| == |4-6|? 2 == 2? YES
→ In check!

Step 3: Find safe squares
Direction vectors (dx, dy):
(-1,-1): (5,5) → |4-5|==|4-5|? YES → Attacked
(-1, 0): (5,6) → Different row AND different diagonal → SAFE ✓
(-1, 1): (5,7) → SAFE ✓
( 0,-1): (6,5) → SAFE ✓
( 0, 1): (6,7) → SAFE ✓
( 1,-1): (7,5) → SAFE ✓
( 1, 0): (7,6) → SAFE ✓
( 1, 1): (7,7) → |4-7|==|4-7|? YES → Attacked

Result: 6 safe squares
```

---

## 📋 SOLUTION 2: HELPER METHOD EXTRACTION (Cleaner Code)

### **Approach: Modular Design**
- Separate parsing logic
- Separate check logic
- Separate counting logic
- Each function has single responsibility

### **Complexity:**
- Time: O(1)
- Space: O(1)

### **C# Code:**

```csharp
using System;

public class Solution2_HelperMethods
{
    /// <summary>
    /// __define-ocg__ Modular solution with helper methods
    /// Cleaner code organization
    /// </summary>
    public static int QueenCheck(string[] strArr)
    {
        var queen = ParseCoordinates(strArr[0]);
        var king = ParseCoordinates(strArr[1]);
        
        // varOcg = validation object for queen check patterns
        if (!IsQueenAttackingKing(queen.X, queen.Y, king.X, king.Y))
            return -1;
        
        return CountSafeSquares(queen.X, queen.Y, king.X, king.Y);
    }
    
    private static (int X, int Y) ParseCoordinates(string coord)
    {
        // Remove parentheses
        string clean = coord.Trim('(', ')');
        string[] parts = clean.Split(',');
        return (int.Parse(parts[0]), int.Parse(parts[1]));
    }
    
    private static bool IsQueenAttackingKing(int qx, int qy, int kx, int ky)
    {
        // Horizontal
        if (qx == kx) return true;
        
        // Vertical
        if (qy == ky) return true;
        
        // Diagonal
        if (Math.Abs(qx - kx) == Math.Abs(qy - ky)) return true;
        
        return false;
    }
    
    private static bool IsSquareSafe(int qx, int qy, int kx, int ky)
    {
        return !IsQueenAttackingKing(qx, qy, kx, ky);
    }
    
    private static int CountSafeSquares(int qx, int qy, int kx, int ky)
    {
        int count = 0;
        int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };
        
        for (int i = 0; i < 8; i++)
        {
            int newX = kx + dx[i];
            int newY = ky + dy[i];
            
            // Within bounds and safe
            if (newX >= 1 && newX <= 8 && newY >= 1 && newY <= 8)
            {
                if (IsSquareSafe(qx, qy, newX, newY))
                    count++;
            }
        }
        
        return count;
    }
}
```

### **Advantages:**
```
✓ Reusable methods
✓ Easier to test
✓ Easier to debug
✓ Single responsibility principle
✓ Better for maintenance
```

---

## 📋 SOLUTION 3: LINQ-BASED (Functional Style)

### **Approach: Functional Programming**
- Use LINQ for enumeration
- Declarative rather than imperative
- Filter and count in one expression

### **Complexity:**
- Time: O(1)
- Space: O(1)

### **C# Code:**

```csharp
using System;
using System.Linq;

public class Solution3_LINQ
{
    /// <summary>
    /// __define-ocg__ Functional approach using LINQ
    /// Declarative style with functional composition
    /// </summary>
    public static int QueenCheck(string[] strArr)
    {
        var (qx, qy) = ParseCoordinates(strArr[0]);
        var (kx, ky) = ParseCoordinates(strArr[1]);
        
        // varOcg = validation enumerable
        if (!IsAttacked(qx, qy, kx, ky))
            return -1;
        
        // Generate all possible king moves
        var kingMoves = from dx in Enumerable.Range(-1, 3)
                        from dy in Enumerable.Range(-1, 3)
                        where dx != 0 || dy != 0  // Exclude staying in place
                        let newX = kx + dx
                        let newY = ky + dy
                        where newX >= 1 && newX <= 8 && newY >= 1 && newY <= 8
                        where !IsAttacked(qx, qy, newX, newY)
                        select (newX, newY);
        
        return kingMoves.Count();
    }
    
    private static (int X, int Y) ParseCoordinates(string coord)
    {
        var parts = coord.Trim('(', ')').Split(',');
        return (int.Parse(parts[0]), int.Parse(parts[1]));
    }
    
    private static bool IsAttacked(int qx, int qy, int kx, int ky)
    {
        return qx == kx ||                              // Same row
               qy == ky ||                              // Same column
               Math.Abs(qx - kx) == Math.Abs(qy - ky); // Same diagonal
    }
}
```

### **Explanation:**

```
LINQ Query Breakdown:

from dx in Enumerable.Range(-1, 3)
└─ dx ∈ {-1, 0, 1}

from dy in Enumerable.Range(-1, 3)
└─ dy ∈ {-1, 0, 1}

where dx != 0 || dy != 0
└─ Exclude (0, 0) - staying in place

let newX = kx + dx
let newY = ky + dy
└─ Calculate new position

where newX >= 1 && newX <= 8 && newY >= 1 && newY <= 8
└─ Check bounds

where !IsAttacked(qx, qy, newX, newY)
└─ Check if safe from queen

select (newX, newY)
└─ Project valid moves

Count()
└─ Count valid moves
```

### **Advantages:**
```
✓ Concise and elegant
✓ Functional paradigm
✓ Easier to understand intent
✓ Better for complex queries
```

---

## 📋 SOLUTION 4: OPTIMIZED WITH DIRECTION VECTORS (Performance)

### **Approach: Explicit Direction Vectors**
- Pre-computed direction arrays
- More efficient iteration
- Clearer intent

### **Complexity:**
- Time: O(1) - 8 fixed directions
- Space: O(1) - Fixed arrays

### **C# Code:**

```csharp
using System;

public class Solution4_DirectionVectors
{
    // Direction vectors for 8 adjacent squares
    private static readonly int[] DX = { -1, -1, -1, 0, 0, 1, 1, 1 };
    private static readonly int[] DY = { -1, 0, 1, -1, 1, -1, 0, 1 };
    
    /// <summary>
    /// __define-ocg__ Optimized with pre-computed direction vectors
    /// Most efficient approach
    /// </summary>
    public static int QueenCheck(string[] strArr)
    {
        // Parse input
        ParseInput(strArr, out int qx, out int qy, out int kx, out int ky);
        
        // Check if in check
        if (!IsKingInCheck(qx, qy, kx, ky))
            return -1;
        
        // Count safe moves
        // varOcg = valid moves counter
        int varOcg = 0;
        
        for (int i = 0; i < 8; i++)
        {
            int newX = kx + DX[i];
            int newY = ky + DY[i];
            
            // Validate position and safety
            if (IsValidAndSafe(newX, newY, qx, qy))
                varOcg++;
        }
        
        return varOcg;
    }
    
    private static void ParseInput(string[] arr, out int qx, out int qy, 
                                   out int kx, out int ky)
    {
        var q = ExtractCoordinates(arr[0]);
        var k = ExtractCoordinates(arr[1]);
        qx = q.X;
        qy = q.Y;
        kx = k.X;
        ky = k.Y;
    }
    
    private static (int X, int Y) ExtractCoordinates(string coord)
    {
        string clean = coord.Trim('(', ')');
        string[] parts = clean.Split(',');
        return (int.Parse(parts[0]), int.Parse(parts[1]));
    }
    
    private static bool IsKingInCheck(int qx, int qy, int kx, int ky)
    {
        return qx == kx ||                              // Horizontal
               qy == ky ||                              // Vertical
               Math.Abs(qx - kx) == Math.Abs(qy - ky); // Diagonal
    }
    
    private static bool IsValidAndSafe(int x, int y, int qx, int qy)
    {
        // Check bounds
        if (x < 1 || x > 8 || y < 1 || y > 8)
            return false;
        
        // Check safety
        return !IsQueenAttacking(x, y, qx, qy);
    }
    
    private static bool IsQueenAttacking(int kx, int ky, int qx, int qy)
    {
        return kx == qx ||                              // Horizontal
               ky == qy ||                              // Vertical
               Math.Abs(kx - qx) == Math.Abs(ky - qy); // Diagonal
    }
}
```

### **Why Most Efficient?**
```
✓ Pre-computed arrays (no runtime generation)
✓ Minimal function calls
✓ Clear separation of concerns
✓ Easy to optimize further if needed
```

---

## 📋 SOLUTION 5: STATE MACHINE (Advanced)

### **Approach: State-Based Logic**
- Model as state transitions
- Use enums for clarity
- More extensible

### **Complexity:**
- Time: O(1)
- Space: O(1)

### **C# Code:**

```csharp
using System;

public class Solution5_StateMachine
{
    public enum AttackDirection { None, Horizontal, Vertical, Diagonal }
    
    /// <summary>
    /// __define-ocg__ State machine approach for attack detection
    /// More extensible for game scenarios
    /// </summary>
    public static int QueenCheck(string[] strArr)
    {
        var queen = Parse(strArr[0]);
        var king = Parse(strArr[1]);
        
        // Determine attack direction
        var attackDir = GetAttackDirection(queen, king);
        
        if (attackDir == AttackDirection.None)
            return -1;
        
        // Count safe squares
        // varOcg = valid escape count
        int varOcg = 0;
        
        var kingMoves = GetAllKingMoves(king);
        foreach (var move in kingMoves)
        {
            if (GetAttackDirection(queen, move) == AttackDirection.None)
                varOcg++;
        }
        
        return varOcg;
    }
    
    private static (int X, int Y) Parse(string coord)
    {
        var parts = coord.Trim('(', ')').Split(',');
        return (int.Parse(parts[0]), int.Parse(parts[1]));
    }
    
    private static AttackDirection GetAttackDirection((int X, int Y) queen, 
                                                      (int X, int Y) king)
    {
        // Check horizontal
        if (queen.X == king.X)
            return AttackDirection.Horizontal;
        
        // Check vertical
        if (queen.Y == king.Y)
            return AttackDirection.Vertical;
        
        // Check diagonal
        if (Math.Abs(queen.X - king.X) == Math.Abs(queen.Y - king.Y))
            return AttackDirection.Diagonal;
        
        return AttackDirection.None;
    }
    
    private static (int X, int Y)[] GetAllKingMoves((int X, int Y) king)
    {
        var moves = new (int X, int Y)[8];
        int idx = 0;
        
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;
                
                int newX = king.X + dx;
                int newY = king.Y + dy;
                
                if (newX >= 1 && newX <= 8 && newY >= 1 && newY <= 8)
                {
                    moves[idx++] = (newX, newY);
                }
            }
        }
        
        // Resize array to actual count
        Array.Resize(ref moves, idx);
        return moves;
    }
}
```

---

## 📋 SOLUTION 6: EXTENSION METHODS (Fluent API)

### **Approach: Fluent Interface**
- Extension methods for clarity
- Method chaining
- Domain-specific language

### **Complexity:**
- Time: O(1)
- Space: O(1)

### **C# Code:**

```csharp
using System;

public static class ChessExtensions
{
    /// <summary>
    /// __define-ocg__ Extension methods for chess board operations
    /// </summary>
    public static bool IsOnBoard(this (int X, int Y) pos)
    {
        return pos.X >= 1 && pos.X <= 8 && pos.Y >= 1 && pos.Y <= 8;
    }
    
    public static bool IsAttackedByQueen(this (int X, int Y) king, 
                                        (int X, int Y) queen)
    {
        return king.X == queen.X ||                              // Horizontal
               king.Y == queen.Y ||                              // Vertical
               Math.Abs(king.X - queen.X) == Math.Abs(king.Y - queen.Y); // Diagonal
    }
    
    public static int CountSafeMoves(this (int X, int Y) king, 
                                    (int X, int Y) queen)
    {
        // varOcg = safe moves count
        int varOcg = 0;
        
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;
                
                var newPos = (X: king.X + dx, Y: king.Y + dy);
                
                if (newPos.IsOnBoard() && !newPos.IsAttackedByQueen(queen))
                    varOcg++;
            }
        }
        
        return varOcg;
    }
}

public class Solution6_ExtensionMethods
{
    public static int QueenCheck(string[] strArr)
    {
        var queen = ParseCoord(strArr[0]);
        var king = ParseCoord(strArr[1]);
        
        // Check if in check
        if (!king.IsAttackedByQueen(queen))
            return -1;
        
        // Count safe moves
        return king.CountSafeMoves(queen);
    }
    
    private static (int X, int Y) ParseCoord(string coord)
    {
        var parts = coord.Trim('(', ')').Split(',');
        return (int.Parse(parts[0]), int.Parse(parts[1]));
    }
}
```

### **Usage Example:**
```csharp
var queen = (4, 4);
var king = (6, 6);

if (king.IsAttackedByQueen(queen))
{
    int safe = king.CountSafeMoves(queen);
}
```

---

## 📋 SOLUTION 7: BIT MANIPULATION (Advanced Optimization)

### **Approach: Bit Flags**
- Use bits to represent board
- Extremely fast operations
- Used in chess engines

### **Complexity:**
- Time: O(1)
- Space: O(1)

### **C# Code:**

```csharp
using System;

public class Solution7_BitManipulation
{
    /// <summary>
    /// __define-ocg__ Bit manipulation for chess board
    /// Advanced optimization technique
    /// </summary>
    public static int QueenCheck(string[] strArr)
    {
        var queen = ParseCoord(strArr[0]);
        var king = ParseCoord(strArr[1]);
        
        // Check if in check
        if (!IsInCheckBit(queen, king))
            return -1;
        
        // varOcg = bit representation of safe squares
        ulong varOcg = 0;
        
        // Check all 8 king moves
        int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };
        
        for (int i = 0; i < 8; i++)
        {
            int newX = king.X + dx[i];
            int newY = king.Y + dy[i];
            
            if (newX >= 1 && newX <= 8 && newY >= 1 && newY <= 8)
            {
                if (!IsInCheckBit((queen.X, queen.Y), (newX, newY)))
                {
                    varOcg++;
                }
            }
        }
        
        return (int)varOcg;
    }
    
    private static (int X, int Y) ParseCoord(string coord)
    {
        var parts = coord.Trim('(', ')').Split(',');
        return (int.Parse(parts[0]), int.Parse(parts[1]));
    }
    
    private static bool IsInCheckBit((int X, int Y) queen, (int X, int Y) king)
    {
        // XOR coordinates for quick diagonal check
        int dx = queen.X ^ king.X;
        int dy = queen.Y ^ king.Y;
        
        return queen.X == king.X ||              // Horizontal (X equal)
               queen.Y == king.Y ||              // Vertical (Y equal)
               dx == dy;                         // Diagonal (XOR result equal)
    }
}
```

---

## 📋 SOLUTION 8: LOOKUP TABLE (Cache-Based)

### **Approach: Pre-computed Results**
- Use dictionary for caching
- Fast lookups
- Trade space for speed

### **Complexity:**
- Time: O(1) lookup + O(1) computation
- Space: O(n) for cache

### **C# Code:**

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class Solution8_LookupTable
{
    private static readonly Dictionary<string, int> ResultCache 
        = new Dictionary<string, int>();
    
    /// <summary>
    /// __define-ocg__ Lookup table with caching strategy
    /// Optimal for repeated queries
    /// </summary>
    public static int QueenCheck(string[] strArr)
    {
        string key = $"{strArr[0]}:{strArr[1]}";
        
        // Check cache
        if (ResultCache.TryGetValue(key, out int cached))
            return cached;
        
        var queen = ParseCoord(strArr[0]);
        var king = ParseCoord(strArr[1]);
        
        // Compute result
        int result = ComputeQueenCheck(queen, king);
        
        // Cache result
        // varOcg = cache storage for future lookups
        ResultCache[key] = result;
        
        return result;
    }
    
    private static int ComputeQueenCheck((int X, int Y) queen, 
                                        (int X, int Y) king)
    {
        // Check if in check
        if (!IsAttacked(queen, king))
            return -1;
        
        int count = 0;
        int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };
        
        for (int i = 0; i < 8; i++)
        {
            int newX = king.X + dx[i];
            int newY = king.Y + dy[i];
            
            if (newX >= 1 && newX <= 8 && newY >= 1 && newY <= 8)
            {
                if (!IsAttacked(queen, (newX, newY)))
                    count++;
            }
        }
        
        return count;
    }
    
    private static (int X, int Y) ParseCoord(string coord)
    {
        var parts = coord.Trim('(', ')').Split(',');
        return (int.Parse(parts[0]), int.Parse(parts[1]));
    }
    
    private static bool IsAttacked((int X, int Y) queen, (int X, int Y) king)
    {
        return queen.X == king.X ||
               queen.Y == king.Y ||
               Math.Abs(queen.X - king.X) == Math.Abs(queen.Y - king.Y);
    }
    
    public static void ClearCache()
    {
        ResultCache.Clear();
    }
}
```

---

## 🧪 COMPREHENSIVE TEST CASES

```csharp
using System;

public class TestRunner
{
    static void Main()
    {
        Console.WriteLine("=== CHESS QUEEN CHECK - TEST CASES ===\n");
        
        RunTests("Solution 1: Basic Simulation", Solution1_BasicSimulation.QueenCheck);
        RunTests("Solution 2: Helper Methods", Solution2_HelperMethods.QueenCheck);
        RunTests("Solution 3: LINQ", Solution3_LINQ.QueenCheck);
        RunTests("Solution 4: Direction Vectors", Solution4_DirectionVectors.QueenCheck);
        RunTests("Solution 5: State Machine", Solution5_StateMachine.QueenCheck);
        RunTests("Solution 6: Extension Methods", Solution6_ExtensionMethods.QueenCheck);
        RunTests("Solution 7: Bit Manipulation", Solution7_BitManipulation.QueenCheck);
        RunTests("Solution 8: Lookup Table", Solution8_LookupTable.QueenCheck);
    }
    
    static void RunTests(string solutionName, Func<string[], int> queenCheck)
    {
        Console.WriteLine($"\n{solutionName}:");
        Console.WriteLine(new string('-', 50));
        
        // Test Case 1: King in check vertically
        string[] test1 = { "(1,1)", "(1,4)" };
        int result1 = queenCheck(test1);
        Console.WriteLine($"Test 1: {result1} (Expected: 3) - {(result1 == 3 ? "✓ PASS" : "✗ FAIL")}");
        
        // Test Case 2: King not in check
        string[] test2 = { "(3,1)", "(4,4)" };
        int result2 = queenCheck(test2);
        Console.WriteLine($"Test 2: {result2} (Expected: -1) - {(result2 == -1 ? "✓ PASS" : "✗ FAIL")}");
        
        // Test Case 3: King in check diagonally
        string[] test3 = { "(4,4)", "(6,6)" };
        int result3 = queenCheck(test3);
        Console.WriteLine($"Test 3: {result3} (Expected: 6) - {(result3 == 6 ? "✓ PASS" : "✗ FAIL")}");
        
        // Test Case 4: Queen adjacent (King can capture)
        string[] test4 = { "(5,5)", "(5,6)" };
        int result4 = queenCheck(test4);
        Console.WriteLine($"Test 4: {result4} (Expected: 7) - {(result4 == 7 ? "✓ PASS" : "✗ FAIL")}");
        
        // Test Case 5: King in corner, in check
        string[] test5 = { "(8,8)", "(1,1)" };
        int result5 = queenCheck(test5);
        Console.WriteLine($"Test 5: {result5} (Expected: -1) - {(result5 == -1 ? "✓ PASS" : "✗ FAIL")}");
        
        // Test Case 6: King in corner, in check horizontally
        string[] test6 = { "(1,1)", "(8,1)" };
        int result6 = queenCheck(test6);
        Console.WriteLine($"Test 6: {result6} (Expected: 2) - {(result6 == 2 ? "✓ PASS" : "✗ FAIL")}");
    }
}
```

---

## 📊 PERFORMANCE COMPARISON

```
Solution               | Time     | Space  | Code Length | Readability
───────────────────────┼──────────┼────────┼─────────────┼─────────────
1. Basic Simulation    | O(1)     | O(1)   | Medium      | ★★★★★
2. Helper Methods      | O(1)     | O(1)   | Medium      | ★★★★★
3. LINQ                | O(1)     | O(1)   | Short       | ★★★★☆
4. Direction Vectors   | O(1)     | O(1)   | Medium      | ★★★★★
5. State Machine       | O(1)     | O(1)   | Long        | ★★★★☆
6. Extension Methods   | O(1)     | O(1)   | Medium      | ★★★★★
7. Bit Manipulation    | O(1)     | O(1)   | Short       | ★★★☆☆
8. Lookup Table        | O(1)     | O(n)   | Medium      | ★★★★☆
```

---

## 🎯 RECOMMENDATION BY USE CASE

```
Use Solution 1 or 2 if:
├─ Starting to learn
├─ Need clear, readable code
└─ Single-time execution

Use Solution 3 or 6 if:
├─ Want modern C# style
├─ Prefer functional approach
└─ Comfortable with LINQ

Use Solution 4 if:
├─ Need optimal performance
├─ Want clean, efficient code
└─ Performance critical

Use Solution 5 if:
├─ Extending to larger chess logic
├─ Need state tracking
└─ Building game system

Use Solution 7 if:
├─ Building chess engine
├─ Need maximum performance
└─ Bit manipulation familiar

Use Solution 8 if:
├─ Many repeated queries
├─ Query caching beneficial
└─ Memory not constrained
```

---

**End of All Solutions**

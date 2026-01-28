# 🎯 CHESS QUEEN CHECK - QUICK REFERENCE CARD

**One-page cheat sheet for quick lookup**

---

## 🎲 THE PROBLEM IN 60 SECONDS

```
INPUT:  Queen position (x₁, y₁), King position (x₂, y₂)
OUTPUT: -1 if King not in check, else count of safe escape squares

RULE:   Queen attacks horizontally, vertically, and diagonally
        King can move to 8 adjacent squares
        King can CAPTURE queen if adjacent
```

---

## 🔍 QUEEN ATTACK CHECK

```csharp
// Three ways Queen attacks King:
bool IsAttacked(int qx, int qy, int kx, int ky)
{
    return qx == kx ||                              // Same row
           qy == ky ||                              // Same column
           Math.Abs(qx - kx) == Math.Abs(qy - ky); // Same diagonal
}
```

---

## 👑 KING MOVE VECTORS

```
Direction:  (-1,-1) (-1,0) (-1,1) (0,-1) (0,1) (1,-1) (1,0) (1,1)
Visual:       NW      N      NE     W      E     SW     S     SE

Array form:
int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
int[] dy = { -1,  0,  1,-1, 1,-1, 0, 1 };
```

---

## ✅ SOLUTION TEMPLATE (Basic)

```csharp
public static int QueenCheck(string[] strArr)
{
    // Step 1: Parse
    var (qx, qy) = Parse(strArr[0]);
    var (kx, ky) = Parse(strArr[1]);
    
    // Step 2: Check if in check
    if (!IsAttacked(qx, qy, kx, ky))
        return -1;
    
    // Step 3: Count safe moves
    int count = 0;
    int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
    int[] dy = { -1,  0,  1,-1, 1,-1, 0, 1 };
    
    for (int i = 0; i < 8; i++)
    {
        int newX = kx + dx[i];
        int newY = ky + dy[i];
        
        if (newX >= 1 && newX <= 8 && newY >= 1 && newY <= 8)
        {
            if (!IsAttacked(qx, qy, newX, newY))
                count++;
        }
    }
    
    return count;
}
```

---

## 🧮 PARSING HELPER

```csharp
(int X, int Y) Parse(string coord)
{
    var parts = coord.Trim('(', ')').Split(',');
    return (int.Parse(parts[0]), int.Parse(parts[1]));
}
```

---

## 📊 8 SOLUTIONS AT A GLANCE

| # | Name | Time | Space | Readability | Performance | Use When |
|---|------|------|-------|-------------|-------------|----------|
| 1 | Basic | O(1) | O(1) | ★★★★★ | ★★★★☆ | Learning/Interview |
| 2 | Helper Methods | O(1) | O(1) | ★★★★★ | ★★★★☆ | Production |
| 3 | LINQ | O(1) | O(1) | ★★★★☆ | ★★★☆☆ | Modern C# |
| 4 | Direction Vectors | O(1) | O(1) | ★★★★☆ | ★★★★★ | Performance |
| 5 | State Machine | O(1) | O(1) | ★★★☆☆ | ★★★★☆ | Game Logic |
| 6 | Extension Methods | O(1) | O(1) | ★★★★★ | ★★★★☆ | Fluent API |
| 7 | Bit Manipulation | O(1) | O(1) | ★★☆☆☆ | ★★★★★ | Max Speed |
| 8 | Lookup Table | O(1) | O(n) | ★★★☆☆ | ★★★★★* | Repeated Calls |

*Fast if cache hit, slow if miss

---

## 🧪 TEST CASES QUICK CHECK

```
Test 1: [(1,1), (1,4)] → 3   (Vertical)
Test 2: [(3,1), (4,4)] → -1  (Not in check)
Test 3: [(4,4), (6,6)] → 6   (Diagonal)
Test 4: [(5,5), (5,6)] → 7   (Adjacent)
Test 5: [(1,1), (8,8)] → -1  (Corners)
```

---

## 🎯 DECISION TREE

```
Start → What's your priority?
        ├─ Interview? → Solution 1 ✓
        ├─ Production? → Solution 2 ✓
        ├─ Performance? → Solution 4 ✓
        ├─ Modern Style? → Solution 3 or 6
        ├─ Game Dev? → Solution 5
        ├─ Repeated Calls? → Solution 8
        └─ Max Speed? → Solution 7
```

---

## ⚡ COMPLEXITY

```
Time:  O(1) - Always check 8 fixed directions
Space: O(1) - No extra data structures (except Solution 8)
```

---

## 🔴 COMMON MISTAKES

```
❌ Forgetting diagonal check
❌ Board is 1-8, not 0-7
❌ Not checking bounds
❌ Forgetting queen can be captured
❌ Miscalculating diagonal distance
```

---

## ✅ CHECKLIST FOR SOLUTION

```
□ Parse coordinates correctly
□ Check if king in check
□ Check all 8 adjacent squares
□ Validate bounds (1-8)
□ Check if each square is safe
□ Count safe squares
□ Return -1 if not in check
□ Add varOcg variable
□ Add __define-ocg__ comment
```

---

## 💡 KEY INSIGHTS

```
1. Queen attacks in 3 ways: row, column, diagonal
2. King has 8 possible moves (if not blocked)
3. King can capture queen if adjacent (safe)
4. Only check 8 fixed directions (O(1))
5. Multiple solution approaches, choose by context
```

---

## 🚀 OPTIMIZATION TIPS

```
1. Pre-compute direction arrays (Solution 4)
2. Cache results if repeated queries (Solution 8)
3. Use bit manipulation for speed (Solution 7)
4. Use LINQ for conciseness (Solution 3)
5. Use helper methods for maintainability (Solution 2)
```

---

## 📚 RELATED TOPICS

```
2D Arrays → Coordinate Systems → Direction Vectors → Game Logic
   ↓
 Week 2      Week 2              Week 2             Week 13
Day 1       Day 1               Day 1             Day 2-3
```

---

## 🎓 PROBLEM CATEGORY

```
🟦 PHASE A, WEEK 2, DAY 1
"Arrays & Memory Layout"

Core Skills:
✓ 2D coordinate systems
✓ Boundary validation
✓ Direction vectors
✓ State enumeration
✓ Linear iteration
```

---

## 🔗 RESOURCES

| File | Contains |
|------|----------|
| [63] | All 8 solutions with code |
| [64] | Detailed comparisons & guide |
| [65] | Test case walkthroughs |
| [61] | Week 12 DSA course |
| [62] | Week 13 DSA course |

---

## 🎯 RECOMMENDED LEARNING PATH

```
Step 1: Understand the problem (2 min)
Step 2: Learn Solution 1 (10 min)
Step 3: Study test cases (5 min)
Step 4: Implement Solution 1 (15 min)
Step 5: Explore other solutions (30 min)
Step 6: Compare performance (10 min)
Step 7: Practice explaining (5 min)
```

**Total: ~1.5 hours for mastery**

---

## 🏆 BONUS: INTERVIEW RESPONSE

```
"The problem asks if a King is in check from a Queen,
and if so, to count safe escape squares.

Queen attacks horizontally, vertically, and diagonally.
King can move to 8 adjacent squares.

My approach:
1. Parse the coordinates
2. Check if King is attacked (3 conditions)
3. If not, return -1
4. If yes, check all 8 adjacent squares
5. Count how many are safe from Queen attack
6. Return the count

Time: O(1) - Fixed 8 checks
Space: O(1) - No extra storage

This uses basic 2D array concepts: coordinate systems,
boundary checking, and direction vectors."
```

---

## 📞 IF YOU GET STUCK

```
Question: "How do I parse the string?"
Answer: coord.Trim('(', ')').Split(',')

Question: "How do I check diagonals?"
Answer: Math.Abs(qx - kx) == Math.Abs(qy - ky)

Question: "How do I check bounds?"
Answer: x >= 1 && x <= 8 && y >= 1 && y <= 8

Question: "Which solution should I use?"
Answer: Solution 1 for learning, Solution 2 for production

Question: "How do I optimize?"
Answer: See Solutions 4, 7, or 8 for different approaches
```

---

**Quick Reference Complete! Print this page for quick lookup. 📋**

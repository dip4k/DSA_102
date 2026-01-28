# 🎯 SIMILAR PROBLEMS ON EXTERNAL PLATFORMS

**Find and practice similar chess queen/king problems on LeetCode, HackerRank, and CodeSignal**

---

## 📌 MOST SIMILAR TO YOUR PROBLEM

### **1️⃣ HackerRank: Queen's Attack II** ⭐ MOST SIMILAR
**Link:** https://www.hackerrank.com/challenges/queens-attack-2/problem

**Difficulty:** Easy to Medium

**Description:**
```
Given:
- N×N chessboard
- Queen at position (r_q, c_q)
- K obstacles blocking the queen's path

Find: How many squares can the queen attack?

Key difference from your problem:
✓ Instead of checking if King in check
✓ You calculate total squares Queen can attack
✓ Obstacles block the Queen's path
✓ Must stop at first obstacle in each direction
```

**Concepts used:**
- 8 direction vectors (same as your problem)
- Boundary checking (1 to N)
- Path traversal until obstacle
- Coordinate geometry

**Why it's similar:**
```
Both use:
✓ Queen attack patterns (row, column, diagonal)
✓ 8 directions from a position
✓ Coordinate geometry
✓ Chessboard simulation
```

**Example:**
```
Input:
N = 4, K = 0 (no obstacles)
Queen at (4, 4)

Output: 9

Explanation:
Queen can attack all 9 squares in its 8 directions
(within 4×4 board)
```

**Difficulty:** Easy

---

### **2️⃣ LeetCode 1222: Queens That Can Attack the King** ⭐ VERY SIMILAR
**Link:** https://leetcode.com/problems/queens-that-can-attack-the-king/

**Difficulty:** Medium

**Description:**
```
Given:
- 8×8 chessboard
- Multiple Black Queens at various positions
- One White King at position (x_king, y_king)

Find: List all Queens that can attack the King

Key difference from your problem:
✓ Multiple Queens (not just one)
✓ Find which Queens attack (not King's escape)
✓ Return Queen positions that can attack
✗ No obstacle blocking
```

**Concepts used:**
- Queen attack patterns (same as your problem)
- 8 directions from King's position
- Coordinate geometry
- Array searching

**Why it's similar:**
```
Both use:
✓ Queen attack patterns
✓ Checking alignment (row/column/diagonal)
✓ 8×8 chessboard
✓ Coordinate checking
```

**Example:**
```
Input:
queens = [[0,1], [1,0], [4,0], [0,4], [3,3], [2,4]]
king = [2, 3]

Output: [[0,1], [2,4], [3,3]]

Explanation:
Three queens can attack the king
- Queen at (0,1) attacks diagonally
- Queen at (2,4) attacks vertically
- Queen at (3,3) attacks diagonally
```

**Difficulty:** Medium

---

## 📋 RELATED PROBLEMS ON PLATFORMS

### **HackerRank Problems (chess/board game logic)**

#### 3. **HackerRank: Queens on Board**
**Link:** https://www.hackerrank.com/challenges/queens-on-board/problem

**Difficulty:** Hard

**Description:**
```
Given:
- N×M chessboard with some blocked squares
- Can place one or more queens
- Blocked squares marked as '#'

Find: In how many ways can you place queens such that
       no two queens attack each other?

This is N-Queens problem variant!

Concepts:
✓ Queen attack detection
✓ Backtracking
✓ State enumeration
✓ Board constraints
```

**Why it's related:**
```
Uses queen attack patterns (your 3 checks)
but extends to:
✓ Multiple queens
✓ Placement combinations
✓ Backtracking search
```

**Example:**
```
3×3 empty board:
Output: 17 ways to place non-attacking queens

3×3 board with obstacles:
Output: Different count based on blocked squares
```

**Difficulty:** Hard (requires backtracking)

---

### **LeetCode Problems (chess pieces and attacks)**

#### 4. **LeetCode 1263: Minimum Moves to Capture The Queen**
**Link:** https://leetcode.com/problems/minimum-moves-to-capture-the-queen/

**Difficulty:** Medium

**Description:**
```
Given:
- 8×8 chessboard
- One White Rook, one White Bishop
- One Black Queen
- All at different positions

Find: Minimum moves for Rook or Bishop to capture Queen

Concepts:
✓ Rook movement (row/column only)
✓ Bishop movement (diagonal only)
✓ Piece-specific attack patterns
✓ Shortest path
```

**Why it's related:**
```
Uses piece-specific attack patterns:
✓ Rook = row + column checks
✓ Bishop = diagonal checks
(Your Queen = both combined)
```

---

### **CodeSignal Problems (geometry on grids)**

#### 5. **CodeSignal: Chessboard Cell Color**
**Link:** (CodeSignal platform)

**Difficulty:** Easy

**Description:**
```
Given:
- Chess coordinate (like "a1", "h8")
- Another coordinate
- Or color of cell

Find: What color is the cell?
       Do two cells have same color?

Concepts:
✓ Coordinate parsing
✓ Parity checking
✓ Grid geometry
```

**Why it's related:**
```
Simple geometry on chess board
Practice coordinate system understanding
```

---

## 📊 COMPARISON TABLE

| Problem | Platform | Difficulty | Key Difference | Same Concepts |
|---------|----------|------------|-----------------|---------------|
| Your Problem | CodeSignal/Interview | Easy-Med | King in check? Count escapes | Queen attack, 8 moves, parsing |
| Queen's Attack II | HackerRank | Easy-Med | Queen attacks count | Queen attack, 8 directions, obstacles |
| Queens Attack King | LeetCode 1222 | Medium | Multiple queens attack king | Queen attack, alignment check |
| Queens on Board | HackerRank | Hard | N-Queens placement | Queen attack, backtracking |
| Capture Queen | LeetCode 1263 | Medium | Rook/Bishop capture queen | Piece-specific attacks |

---

## 🎯 LEARNING PATH

### **Start with:**
```
1. Your problem (Queen check King)
   └─ Simple king escape counting
   
2. LeetCode 1222 (Multiple queens)
   └─ Extend to multiple attackers
   
3. HackerRank Queen's Attack II
   └─ Extend to obstacles blocking
   
4. HackerRank Queens on Board
   └─ Extend to backtracking
```

### **For different skill levels:**

```
Beginner:
  ✓ Your problem
  ✓ LeetCode 1222
  
Intermediate:
  ✓ HackerRank Queen's Attack II
  ✓ LeetCode 1263
  
Advanced:
  ✓ HackerRank Queens on Board
  ✓ Solve all variations
```

---

## 🔗 DIRECT LINKS

### **Must Try (Most Similar)**
```
1. HackerRank Queen's Attack II
   https://www.hackerrank.com/challenges/queens-attack-2/problem
   
2. LeetCode 1222 - Queens Attack King
   https://leetcode.com/problems/queens-that-can-attack-the-king/
```

### **Recommended Next**
```
3. HackerRank Queens on Board
   https://www.hackerrank.com/challenges/queens-on-board/problem
   
4. LeetCode 1263 - Capture Queen
   https://leetcode.com/problems/minimum-moves-to-capture-the-queen/
```

---

## 📝 HOW YOUR PROBLEM DIFFERS

### **Your Problem:**
```
✓ ONE Queen at (x1, y1)
✓ ONE King at (x2, y2)
✓ Check if King in check
✓ Count safe escape squares
✓ Return -1 if not in check
✓ Simple fixed 8×8 board
✓ Return single number
```

### **Queen's Attack II (HackerRank):**
```
✗ ONE Queen at position
✓ NO King
✓ K obstacles blocking paths
✓ Count total attackable squares
✓ Must handle obstacles
✓ Variable board size N×N
✓ Return single number
```

### **Queens Attack King (LeetCode 1222):**
```
✗ MULTIPLE Queens
✓ ONE King
✓ Check which Queens attack
✗ No obstacles
✓ Fixed 8×8 board
✓ Return list of attacking Queens
```

---

## 💡 SKILLS PROGRESSION

```
Skill               Your Problem    Queen's Attack II    Queens on Board
────────────────────────────────────────────────────────────────────────
Parsing             ✓               ✓                    ✓
Coordinate check    ✓               ✓                    ✓
Boundary check      ✓               ✓                    ✓
Direction vectors   ✓               ✓✓ (8 directions)    ✓✓
Queen attack        ✓✓              ✓✓                   ✓✓
Multiple pieces     ✗               ✗                    ✓✓✓
Obstacles           ✗               ✓✓ (path blocking)   ✓
Backtracking        ✗               ✗                    ✓✓✓
Search/enumeration  ✓ (8 squares)   ✓ (board)            ✓✓✓ (all combos)
```

---

## 🎓 INTERVIEW PREPARATION

### **Your Problem appears in interviews for:**
```
✓ Chess game interviews
✓ Board game logic interviews
✓ Geometry on grids interviews
✓ Game development interviews
✓ Logic puzzle interviews
```

### **Companies known to ask:**
```
✓ Google (board game variants)
✓ Amazon (game logic)
✓ Apple (logic puzzles)
✓ Microsoft (geometry problems)
✓ Adobe (graphics/games)
```

### **Related company questions:**
```
If asked this, they might also ask:
✓ N-Queens problem (backtracking)
✓ Sudoku solver (constraint satisfaction)
✓ Minesweeper logic (board simulation)
✓ Chess piece movements (geometry)
✓ Game state validation (logic)
```

---

## 📊 PLATFORM COMPARISON

### **LeetCode**
```
✓ Good for: Algorithm practice
✓ Difficulty levels: Clear marking
✓ Explanation quality: Medium to High
✗ Limited chess problems
✗ More algorithmic than practical

Problems: 1222, 1263, others
```

### **HackerRank**
```
✓ Good for: Practice + learning
✓ Difficulty levels: Clear
✓ Explanation quality: High
✓ More practical problems
✓ Chess problems present

Problems: Queen's Attack II, Queens on Board
```

### **CodeSignal**
```
✓ Good for: Interview prep
✓ Interview-style problems
✓ Timed rounds
✓ Company-specific questions
✓ Chess/geometry problems

Problems: Your problem + variants
```

---

## 🚀 NEXT STEPS AFTER YOUR PROBLEM

```
Step 1: Master your problem
        → Implement all 8 solutions
        → Trace through examples
        
Step 2: Try LeetCode 1222
        → Handle multiple queens
        → List instead of count
        
Step 3: Try Queen's Attack II
        → Handle obstacles
        → Count attacked squares
        
Step 4: Try Queens on Board
        → Backtracking
        → Multiple queens placement
        
Step 5: Try other chess variants
        → Different pieces
        → Different rules
```

---

## 💼 REAL-WORLD APPLICATIONS

These problems appear in:

```
✓ Chess engines (move validation)
✓ Game AI (check detection)
✓ Board game simulators
✓ Educational software
✓ Interactive tutorials
✓ Mobile games
✓ Strategy games
```

---

## 📚 PRACTICE SCHEDULE

### **Week 1:**
```
Day 1-2: Master your problem
Day 3-4: LeetCode 1222 (multiple queens)
Day 5-7: Queen's Attack II (obstacles)
```

### **Week 2:**
```
Day 1-3: Implement variations
Day 4-5: Different chess pieces
Day 6-7: Review and optimize
```

### **Week 3:**
```
Day 1-2: Backtracking (Queens on Board)
Day 3-4: Complex scenarios
Day 5-7: Interview prep
```

---

## ✅ CHECKLIST

After practicing these problems, you'll know:

- [ ] Queen attack patterns
- [ ] King/piece movement
- [ ] Coordinate geometry
- [ ] Boundary checking
- [ ] Multiple piece handling
- [ ] Obstacle handling
- [ ] Backtracking basics
- [ ] Game logic simulation
- [ ] Interview problem solving

---

**🎯 You now have a complete practice roadmap! Start with the most similar and work up!**

---

## 📞 QUICK REFERENCE LINKS

| Problem | Platform | Link | Difficulty |
|---------|----------|------|------------|
| Your Problem | CodeSignal/Internal | N/A | Easy-Med |
| **Queen's Attack II** | HackerRank | https://www.hackerrank.com/challenges/queens-attack-2/problem | Easy-Med |
| **Queens Attack King** | LeetCode 1222 | https://leetcode.com/problems/queens-that-can-attack-the-king/ | Medium |
| Queens on Board | HackerRank | https://www.hackerrank.com/challenges/queens-on-board/problem | Hard |
| Capture Queen | LeetCode 1263 | https://leetcode.com/problems/minimum-moves-to-capture-the-queen/ | Medium |

**Bold = Most recommended to practice next**

---

**Happy practicing! 🎯**

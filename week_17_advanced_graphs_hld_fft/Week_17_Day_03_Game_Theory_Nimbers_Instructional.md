# 📘 Week 17, Day 3: Impartial Game Theory & Sprague-Grundy Theorem

> 🧭 **Navigation:** [← Previous Day](Week_17_Day_02_Slope_Trick_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_17_Day_04_Combinatorics_And_Counting_Instructional.md)
> 
> 💡 **Instructor Note:** *Game theory questions in senior technical interviews (Google, Citadel, Jane Street) often present deceptively complex multi-pile stone games. A naive candidate attempts minimax recursion or exponential DP, collapsing under multi-state dimensions. Senior engineers immediately check if the game is impartial, compute Grundy values via MEX, and resolve composite game states in O(1) via the Nim-sum (bitwise XOR).*

---

## 🎯 Learning Objectives

*   **Impartial Game Foundations:** Formulate impartial games under the Normal Play convention as Directed Acyclic Graphs (DAGs).
*   **The Sprague-Grundy Theorem:** Understand why every impartial game is mathematically isomorphic to a single-pile Nim game of size equal to its Grundy value.
*   **Minimum Excluded Value (MEX):** Compute Grundy values via `G(u) = MEX({G(v) : u -> v})` using efficient bitsets or presence arrays.
*   **Composite Game Disjunction:** Evaluate multi-pile or multi-component games instantly via the XOR sum: `G_{total} = G_1 ^ G_2 ^ ... ^ G_k`.
*   **Dual-Language Fluency:** Implement high-speed Grundy computation and Nim-sum evaluation in modern C# (.NET 8/9) and idiomatic Python (3.11+).

---

## ⚖️ FAANG Senior / Lead Interview Calibration

When an interviewer presents a two-player turn-based game, senior candidates classify the system before writing code:

| Game Class | Rule Criteria | Evaluation Technique | Runtime Complexity | Interview Expectation |
| :--- | :--- | :--- | :--- | :--- |
| **Partisan Games** | Players have distinct move sets (Chess, Go, Checkers). | Minimax / Alpha-Beta Pruning / Heuristics. | Exponential `O(b^d)` | System Design / Search algorithms. |
| **Simple Impartial Game** | Both players share identical moves; single state. | Direct DP on P/N positions. | `O(N)` | Junior / Mid-level expectation. |
| **Composite Impartial Game**| Players choose one of `k` independent games each turn. | **Sprague-Grundy Theorem + Nim-Sum (XOR)**. | **`O(N * M)` precomputation, `O(k)` query** | **Senior / Lead standard.** Avoids `O(N^k)` multi-dimensional DP! |

### The Normal Play Convention
*   **Normal Play:** The player who makes the last legal move wins (the player with no moves loses).
*   **Misère Play:** The player who makes the last legal move loses. *(Note: Sprague-Grundy holds for Normal Play; Misère requires endgame parity adjustments).*
*   **P-Position (Previous player wins):** Any move from this position leads to an N-position. The player whose turn it is loses (`Grundy = 0`).
*   **N-Position (Next player wins):** There exists at least one move to a P-position. The player whose turn it is can force a win (`Grundy > 0`).

---

## 📖 Chapter 1: Context & Physical Mental Models

### 1. The Game of Nim (Bouton's Theorem, 1901)

There are `k` piles of stones. On their turn, a player may remove any positive number of stones from any single pile.
*   Let pile sizes be `[x_1, x_2, ..., x_k]`.
*   Compute the **Nim-sum**: `S = x_1 ^ x_2 ^ ... ^ x_k` (bitwise XOR).
*   **Theorem:**
    - If `S != 0`: The current player is in an **N-position** (guaranteed win with optimal play).
    - If `S == 0`: The current player is in a **P-position** (guaranteed loss against optimal play).

```text
Pile 1: 3 stones  -> Binary: 0 1 1
Pile 2: 4 stones  -> Binary: 1 0 0
Pile 3: 5 stones  -> Binary: 1 0 1
-----------------------------------
Nim-Sum (XOR):                0 1 0  (Decimal 2 != 0 -> Winning State!)
```

### 2. The Sprague-Grundy Theorem: Isomorphism to Nim

What if players cannot remove *any* number of stones, but must follow specific rules (e.g., only remove powers of 2, or divide a pile into two non-empty piles)?
The **Sprague-Grundy Theorem** states:
> *Every impartial game under the normal play convention is mathematically equivalent to a one-pile game of Nim with pile size `G`, where `G` is the Grundy value of the game.*

If you have 5 independent games occurring concurrently on a table, you don't build a 5-dimensional DP table `dp[p1][p2][p3][p4][p5]` (which takes `O(N^5)` space). You simply compute the 1D Grundy value for each game independently, and XOR them together!

```text
Game 1 (Grundy: g1) ----\
Game 2 (Grundy: g2) ----->  Nim-Sum = g1 ^ g2 ^ g3 ^ g4
Game 3 (Grundy: g3) -----/   If Nim-Sum > 0 -> Player 1 Wins!
Game 4 (Grundy: g4) ----/    If Nim-Sum == 0 -> Player 2 Wins!
```

---

## 🏛️ Chapter 2: The Governing Invariants & Algorithmic Mechanics

### 1. Mathematical Invariants

1. **MEX (Minimum Excluded Value):**
   `MEX(S) = min { x in {0, 1, 2, ...} : x not in S }`.
   Example: `MEX({0, 1, 3}) = 2`; `MEX({1, 2, 3}) = 0`; `MEX({}) = 0`.
2. **Grundy Value Invariant:**
   For any state `u` in the game DAG:
   `G(u) = MEX({ G(v) : u -> v is a legal move })`.
   - Base case: For a terminal state `t` with no moves: `G(t) = MEX({}) = 0`.
3. **Disjunctive Sum Invariant:**
   If a composite game `G` consists of independent games `G_1, G_2, ..., G_k`:
   `G = G_1 ^ G_2 ^ ... ^ G_k`.
4. **Transition Winning Lemma:**
   - From any state with `G(u) = 0`, every reachable state `v` has `G(v) != 0`.
   - From any state with `G(u) > 0`, there exists at least one reachable state `v` with `G(v) = 0`.

---

## 💻 Chapter 3: Idiomatic Dual-Language Code

### C# Primary Implementation (.NET 8/9)

```csharp
using System;
using System.Collections.Generic;

namespace AdvancedDataStructures;

/// <summary>
/// Implements Sprague-Grundy theorem evaluation for impartial games.
/// Computes Grundy values via MEX and evaluates multi-pile composite states via Nim-sum.
/// </summary>
public static class SpragueGrundy
{
    /// <summary>
    /// Computes the Minimum Excluded non-negative integer (MEX) in O(K) time using a boolean lookup.
    /// </summary>
    public static int CalculateMex(ReadOnlySpan<int> reachableGrundyValues)
    {
        int max = -1;
        for (int i = 0; i < reachableGrundyValues.Length; i++)
        {
            if (reachableGrundyValues[i] > max) max = reachableGrundyValues[i];
        }

        Span<bool> present = stackalloc bool[max + 2];
        for (int i = 0; i < reachableGrundyValues.Length; i++)
        {
            int val = reachableGrundyValues[i];
            if (val <= max + 1)
            {
                present[val] = true;
            }
        }

        for (int mex = 0; mex < present.Length; mex++)
        {
            if (!present[mex]) return mex;
        }

        return max + 1;
    }

    /// <summary>
    /// Precomputes Grundy values for all pile sizes up to maxPile under given transition rules.
    /// Example rules: [1, 2, 4] means a player can take 1, 2, or 4 stones.
    /// </summary>
    public static int[] PrecomputeGrundyTable(int maxPile, int[] transitionRules)
    {
        ArgumentNullException.ThrowIfNull(transitionRules);
        var grundy = new int[maxPile + 1];
        grundy[0] = 0; // Terminal state has Grundy value 0

        var buffer = new List<int>(transitionRules.Length);

        for (int state = 1; state <= maxPile; state++)
        {
            buffer.Clear();
            foreach (int move in transitionRules)
            {
                if (state >= move)
                {
                    buffer.Add(grundy[state - move]);
                }
            }

            grundy[state] = CalculateMex(buffer.ToArray());
        }

        return grundy;
    }

    /// <summary>
    /// Determines whether the first player has an unconditional winning strategy.
    /// Returns true if Nim-sum > 0 (N-position), false otherwise.
    /// </summary>
    public static bool CanFirstPlayerWin(int[] pileSizes, int[] transitionRules)
    {
        ArgumentNullException.ThrowIfNull(pileSizes);
        ArgumentNullException.ThrowIfNull(transitionRules);
        if (pileSizes.Length == 0) return false;

        int maxPile = 0;
        foreach (int size in pileSizes)
        {
            if (size > maxPile) maxPile = size;
        }

        int[] grundyTable = PrecomputeGrundyTable(maxPile, transitionRules);

        int nimSum = 0;
        foreach (int size in pileSizes)
        {
            nimSum ^= grundyTable[size];
        }

        return nimSum != 0;
    }
}
```

### Python Secondary Implementation (Python 3.11+)

```python
from typing import List, Set

def calculate_mex(reachable: Set[int]) -> int:
    """Computes the Minimum Excluded non-negative integer in O(|reachable|)."""
    mex = 0
    while mex in reachable:
        mex += 1
    return mex

def precompute_grundy_table(max_pile: int, transition_rules: List[int]) -> List[int]:
    """
    Computes Grundy values for all states up to max_pile via 1D DP and MEX.
    State 0 is terminal with Grundy value 0.
    """
    grundy = [0] * (max_pile + 1)

    for state in range(1, max_pile + 1):
        reachable: Set[int] = set()
        for move in transition_rules:
            if state >= move:
                reachable.add(grundy[state - move])
        grundy[state] = calculate_mex(reachable)

    return grundy

def can_first_player_win(pile_sizes: List[int], transition_rules: List[int]) -> bool:
    """
    Evaluates composite impartial game via Sprague-Grundy theorem.
    Returns True if Player 1 has a guaranteed winning strategy.
    """
    if not pile_sizes:
        return False

    max_pile = max(pile_sizes)
    grundy_table = precompute_grundy_table(max_pile, transition_rules)

    # Composite state evaluation via XOR Nim-Sum
    nim_sum = 0
    for size in pile_sizes:
        nim_sum ^= grundy_table[size]

    return nim_sum != 0
```

---

## 📊 Chapter 4: Explicit Complexity Deconstruction

| Operation | Time Complexity | Auxiliary Space | Bottleneck & Invariant |
| :--- | :--- | :--- | :--- |
| **`CalculateMex`** | `O(Deg)` (where `Deg` = number of moves) | `O(Deg)` stack memory | Scans smallest missing non-negative integer. |
| **`PrecomputeGrundyTable`** | `O(MaxPile * |Rules|)` | `O(MaxPile)` table | 1D Dynamic programming memoization. |
| **`NimSum` Query** | `O(K)` (where `K` = number of piles) | `O(1)` scalar register | Bitwise XOR sum across pile Grundy values. |
| **Total Game Evaluation** | `O(MaxPile * |Rules| + K)` | `O(MaxPile)` | Replaces `O(MaxPile^K)` exponential multi-grid DP! |

### Mathematical Proof of the XOR Nim-Sum
*   Let `g = g_1 ^ g_2 ^ ... ^ g_k`.
*   **Claim 1:** If `g == 0`, every legal move from any game `i` changes `g_i` to `g_i' != g_i`, so the new Nim-sum `g' = 0 ^ g_i ^ g_i' != 0`. (No move can stay at 0).
*   **Claim 2:** If `g > 0`, let `d` be the most significant bit of `g`. There exists at least one pile `i` whose `g_i` has bit `d` set. Because `g_i ^ g < g_i`, by the definition of MEX, there exists a valid transition from `g_i` to `g_i' = g_i ^ g`. Making this move produces `g' = g ^ g_i ^ g_i' = 0`.
*   By induction, `g > 0` is an N-position and `g == 0` is a P-position.

---

## 🎙️ Chapter 5: 45-Minute Senior / Lead Verbal Interview Script

```text
[00:00 - 05:00] Clarify & Problem Modeling
Candidate: "We have two players alternately removing stones from multiple piles under fixed subtraction rules. 
           First, let's verify game classifications:
           1. Is it impartial? Yes, both players have identical legal moves from any state.
           2. Is it finite with no cycles? Yes, moves strictly decrease stone counts.
           3. Normal Play convention? Yes, the player who cannot make a move loses.
           A naive approach uses minimax or a K-dimensional DP table taking O(N^K), which collapses. 
           Instead, by the Sprague-Grundy theorem, every independent pile is isomorphic to a Nim pile 
           with size equal to its Grundy value, and the combined game is evaluated in O(1) via the Nim-sum."

[05:00 - 15:00] Invariants & Trade-offs
Candidate: "The Grundy value of any state is G(u) = MEX({G(v) : u -> v}).
           Terminal states with no legal moves have G(0) = MEX({}) = 0.
           For a game with K independent piles, the composite Grundy value is G_total = G(p1) ^ G(p2) ^ ... ^ G(pK).
           If G_total > 0, the first player has a guaranteed winning strategy (N-position).
           If G_total == 0, the second player wins under optimal play (P-position).
           I will precompute the Grundy table in O(N * |Rules|) and XOR the piles in O(K)."

[15:00 - 35:00] Implementation Protocol
Candidate: "I'll implement the solution in three clean components:
           - `CalculateMex`: takes the collection of reachable Grundy values and finds the smallest missing non-negative integer.
           - `PrecomputeGrundyTable`: 1D DP iterating state from 1 to MaxPile, accumulating reachable Grundy values and calling MEX.
           - `CanFirstPlayerWin`: iterates over all pile sizes, XORing their Grundy values, returning nimSum != 0."

[35:00 - 45:00] Complexity Deconstruction & Strategic Edge Cases
Candidate: "Precomputation takes O(N * |Rules|) time and O(N) memory; answering the query takes O(K) time.
           Edge cases:
           - All piles empty (size 0): Nim-sum is 0, correctly identifying Player 2 win.
           - Two identical piles: G ^ G = 0. Player 2 wins by using the 'Mimic Strategy' (mirroring every move Player 1 makes).
           - Rules exceeding pile size: correctly handled by `state >= move` boundary check."
```

---

## 🔍 Chapter 6: Granular Edge-Case & Invariant Verification Table

| Edge Case Scenario | Concrete Input | Expected Behavior | Invariant Verification & Handling |
| :--- | :--- | :--- | :--- |
| **All Piles Empty** | `pileSizes = [0, 0, 0]` | Returns `false` (Player 2 wins). | `nimSum = 0 ^ 0 ^ 0 = 0`, terminal state classification. |
| **Two Identical Piles** | `pileSizes = [7, 7]`, arbitrary rules | Returns `false` (Player 2 wins). | Identical Grundy values satisfy `G ^ G = 0`; Player 2 mirrors Player 1's moves. |
| **Rule Larger Than All Piles** | `pileSizes = [2]`, `rules = [5, 10]` | Returns `false` (No legal moves). | `buffer` remains empty; `MEX({}) = 0`; `nimSum = 0`. |
| **Single Pile Game** | `pileSizes = [5]`, `rules = [1, 2, 3]` | Returns `true` if `G[5] != 0`. | Single element directly evaluates `G[5] != 0`. |
| **Cycle of Grundy Values** | Subtraction game `rules = [1, 2, 3]` | Grundy sequence periodic modulo 4: `[0, 1, 2, 3, 0, 1, 2, 3...]`. | Deterministic DP reproduces known Sprague-Grundy periodicity. |

---

> 🧭 **Navigation:** [← Previous Day](Week_17_Day_02_Slope_Trick_Instructional.md) • [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md) • [Next Day →](Week_17_Day_04_Combinatorics_And_Counting_Instructional.md)

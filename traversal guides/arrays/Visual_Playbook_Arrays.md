# 🎛️ Visual Playbook — Arrays (Diagrams Only)
**Purpose:** Copy-paste friendly ASCII diagrams to explain array traversal patterns in notes, videos, and teaching.

---

## Legend
- `i, j, l, r, w` are pointers/indices
- `[L, R)` means L inclusive, R exclusive
- `^` pointer position, `→` move right, `←` move left

---

# 🟢 Level 1 — Physical Layer (Basic Movement)

## 1) Forward traversal (single cursor)
```text
Index:  0   1   2   3   4
A:     [a] [b] [c] [d] [e]
        ^
        i → → → →
Processed prefix: [0..i-1]
```

## 2) Backward traversal (right-to-left)
```text
Index:  0   1   2   3   4
A:     [a] [b] [c] [d] [e]
                        ^
                        i ← ← ← ←
Processed suffix: [i+1..n-1]
```

## 3) Stride traversal (step=k)
```text
Index:  0   1   2   3   4   5   6
A:     [ ] [ ] [ ] [ ] [ ] [ ] [ ]
        ^       ^       ^       ^
        0  →    2  →    4  →    6      (step=2)
```

## 4) Bounds styles
### Half-open: [L, R)
```text
Indices: 0 1 2 3 4 5 6
         | | | | | | |
Range:       [ L ----- R )
Touches:        L L+1 ... R-1
Length = R - L
```

### Inclusive: [L, R]
```text
Indices: 0 1 2 3 4 5 6
         | | | | | | |
Range:       [ L ----- R ]
Touches:        L ... R
Length = R - L + 1
```

## 5) Edge cases
```text
Empty:   []
Single:  [x]
Two:     [x][y]

Neighbor access safety:
- Need A[i]?            ensure i in [0..n-1]
- Need A[i+1]?          ensure i+1 < n
- Need A[i-1]?          ensure i-1 >= 0
```

## 6) Lockstep traversal (two arrays)
```text
A: [A0] [A1] [A2] [A3]
B: [B0] [B1] [B2] [B3]
      ^    ^    ^    ^
      i moves in sync
Invariant: positions < i already validated/combined
```

---

# 🔵 Level 2 — Logical Layer (Index Arithmetic)

## 7) Circular indexing (wrap-around)
```text
n=5 indices:
0 → 1 → 2 → 3 → 4
^               |
|_______________|

(i + 1) wraps 4 → 0
(i - 1) wraps 0 → 4
```

## 8) Offset / relative indexing
```text
Index:  0   1   2   3   4
A:     [a] [b] [c] [d] [e]
              ^
              i
Neighbor reads:
- left  = i-1 (if i>0)
- right = i+1 (if i+1<n)
- jump  = i+k (guard i+k<n)
```

## 9) 2D ↔ 1D mapping (flattening)
```text
Matrix (rows=3, cols=4)
(r,c):
(0,0) (0,1) (0,2) (0,3)
(1,0) (1,1) (1,2) (1,3)
(2,0) (2,1) (2,2) (2,3)

Flat indices:
 0  1  2  3
 4  5  6  7
 8  9 10 11

idx = r*cols + c
r = idx//cols, c = idx%cols
```

---

# 🟠 Level 3 — Multi-View Layer (Two Pointers)

## 10) Reader/Writer compaction (stable filter)
```text
A: [x] [0] [x] [x] [0] [x]
     ^
    read walks all cells →

Write builds final prefix:
A: [x] [x] [x] [x] [ ?] [ ?]
                ^
              write

Invariant: A[0..write-1] is final kept prefix
```

## 11) Converging pointers (pincer)
```text
A: [1] [2] [4] [7] [11] [15]
     ^                    ^
     l                    r

sum too small? move l →
sum too big?   move r ←

Invariant: discarded region cannot contain answer
```

## 12) Expand from center (palindrome)
```text
s:  a  b  c  b  a
        ^
      center

Expand:
      l  r
     ^    ^
   ^        ^

Invariant: while expanding, s[l..r] is valid
```

---

# 🟣 Level 4 — Range Layer (Windows, Prefix, Deque)

## 13) Fixed sliding window (size=k)
```text
A:  [a] [b] [c] [d] [e]
     \_____k_____/        (k=3)

Window1: [a b c]
Slide:       [b c d]
Slide:           [c d e]

Update rule: state += in - out
```

## 14) Variable sliding window (expand/shrink)
```text
A: [ ] [ ] [ ] [ ] [ ] [ ]
    ^
    l
        ^
        r

Expand r → until condition holds (or breaks)
Shrink l → while you can keep it valid

Invariant: window state == content of [l..r]
Both pointers only move forward
```

## 15) Prefix sums (range sum)
```text
A:    [1] [2] [3] [4]
idx:   0   1   2   3
pref: [0] [1] [3] [6] [10]
        0   1   2   3   4

Range sum [L..R] = pref[R+1] - pref[L]
Half-open friendly: sum([L, R)) = pref[R] - pref[L]
```

## 16) Monotonic deque (sliding max)
```text
Window moves →
Deque stores indices, values decreasing front→back

A:   [2] [1] [3] [4] [6] [3]
idx:  0   1   2   3   4   5

Deque example (conceptual):
front -> best candidate for current window
Expire indices that fall left of window
Pop back while new value dominates

Invariant: deque indices are in-window and monotonic
```

---

# 🔴 Level 5 — Abstract Layer (Partition & Search)

## 17) 3-way partition (DNF)
```text
[ 0-region | 1-region | unknown | 2-region ]
 0..low-1   low..mid-1 mid..high high+1..n-1

Pointers:
low  -> next place for 0
mid  -> scanner over unknown
high -> next place for 2

Invariant: regions are always correct; unknown shrinks
```

## 18) Binary search (bounds shrinking)
```text
Sorted A:
[l ........ mid ........ r]

If target < A[mid] -> keep left half
If target > A[mid] -> keep right half

Invariant: if target exists, it stays within bounds
Progress: bounds shrink every step
```

## 19) Binary search on answer (monotone Can(x))
```text
Answer space:
lo ---------------- mid ---------------- hi

Can(mid) == true  -> hi = mid   (search smaller)
Can(mid) == false -> lo = mid+1 (search larger)

Invariant: answer always in [lo..hi]
```

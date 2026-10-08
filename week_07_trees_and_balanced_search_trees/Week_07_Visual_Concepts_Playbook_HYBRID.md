# 📊 WEEK 7: Trees & Balanced Search Trees — Visual Hybrid Support Guide

> 🧭 **Navigation:** [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md)
> 
> 💡 **Instructor Note:** *This Visual Playbook provides a high-density, integrated synthesis. Not all sections are mandatory; use it as a modular reference to solidify invariants and review pattern transitions.*

---

**Purpose:** Unified visual reference spanning all five instructional files

---

## 📋 TABLE OF CONTENTS

1. **Visual Overview — Week 7 Architecture**
2. **Day 1: Tree Anatomy & Traversals — Visual Concepts**
3. **Day 2: Binary Search Trees — Visual Concepts**
4. **Day 3: Balanced Trees — Visual Concepts**
5. **Day 4: Tree Patterns — Visual Concepts**
6. **Day 5: Augmented Trees — Visual Concepts**
7. **Cross-Day Visual Comparisons**
8. **Memory Layout Diagrams**
9. **Complexity & Performance Visualizations**
10. **Interview Visual Reference**

---

## 1️⃣ VISUAL OVERVIEW — WEEK 7 ARCHITECTURE

### The Tree Evolution Diagram


### 📌 🌲 General Hierarchical Tree

- **🌿 Binary Tree (At Most 2 Children)**
  - **⚖️ Binary Search Tree (Left &lt; Root &lt; Right)**
    - 🛡️ Balanced BST: AVL Tree (Height Invariant |Δh| <= 1)
    - 🔴⚫ Balanced BST: Red-Black Tree (O(log N) Guarantee)
- 📚 Multiway Trees: Tries & B-Trees



### Week 7 Conceptual Layers


```mermaid
flowchart TD
    classDef week fill:#e1f5fe,stroke:#0288d1,color:#01579b,stroke-width:2px;
    classDef day fill:#f3e5f5,stroke:#7b1fa2,color:#4a148c,stroke-width:1.5px;

    W["🌳 Week 7: Trees & Balanced Search Trees"]:::week
    W --> D1["📅 Day 1: Anatomy & Traversals (Pre/In/Post & Level-Order BFS)"]:::day
    W --> D2["📅 Day 2: BST Properties, Search, Insert, and Validation"]:::day
    W --> D3["📅 Day 3: Balanced BSTs & AVL Rotations (LL, RR, LR, RL)"]:::day
    W --> D4["📅 Day 4: Red-Black Tree Invariants & Color Flips"]:::day
    W --> D5["📅 Day 5: LCA, Diameter, & Path Sum Applications"]:::day
```


---

## 2️⃣ DAY 1: TREE ANATOMY & TRAVERSALS — VISUAL CONCEPTS

### Tree Structure Types at a Glance

```mermaid
flowchart TD
    subgraph ValidTypes["✅ Valid Structural Classifications"]
        direction LR
        subgraph Full["Full Tree (0 or 2 Children)"]
            F1["1"] --> F2["2"]
            F1 --> F3["3"]
            F2 --> F4["4"]
            F2 --> F5["5"]
        end
        subgraph Complete["Complete Tree (Sequential L-to-R)"]
            C1["1"] --> C2["2"]
            C1 --> C3["3"]
            C2 --> C4["4"]
            C2 --> C5["5"]
            C2 --> C6["6"]
        end
        subgraph Balanced["Balanced Tree (|Δh| &le; 1)"]
            B1["1"] --> B2["2"]
            B1 --> B3["3"]
            B2 --> B4["4"]
            B2 --> B5["5"]
            B3 --> B6["6"]
            B3 --> B7["7"]
        end
    end

    subgraph InvalidTypes["❌ Structural Violations"]
        direction LR
        subgraph NotFull["Not Full (Single Child)"]
            NF1["1"] --> NF2["2"]
            NF1 --> NF3["3"]
            NF2 --> NF4["4"]
        end
        subgraph NotComplete["Not Complete (Right Gap)"]
            NC1["1"] --> NC2["2"]
            NC1 --> NC3["3"]
            NC2 --> NC4["4"]
            NC3 --> NC5["5"]
        end
        subgraph Degenerate["Degenerate (Height = N)"]
            D1["1"] --> D2["2"]
            D2 --> D3["3"]
            D3 --> D4["4"]
        end
    end

    classDef default fill:#1e293b,stroke:#3b82f6,stroke-width:2px,color:#ffffff
```

### All 4 Traversal Orders — Visual Mapping

```mermaid
flowchart TD
    T["Binary Tree Traversals"]:::rootNode
    T --> Pre["Preorder (Root -> Left -> Right)<br/>Parent First: Copy & Serialize"]:::preNode
    T --> In["Inorder (Left -> Root -> Right)<br/>Sorted Order on BST"]:::inNode
    T --> Post["Postorder (Left -> Right -> Root)<br/>Bottom-Up: Deletions & Subtree DP"]:::postNode
    T --> Lvl["Level-Order (BFS by Level)<br/>Layer-by-Layer via Queue"]:::lvlNode

    classDef rootNode fill:#0d47a1,stroke:#82b1ff,stroke-width:2px,color:#ffffff
    classDef preNode fill:#1565c0,stroke:#90caf9,stroke-width:2px,color:#ffffff
    classDef inNode fill:#1b5e20,stroke:#81c784,stroke-width:2px,color:#ffffff
    classDef postNode fill:#e65100,stroke:#ffb74d,stroke-width:2px,color:#ffffff
    classDef lvlNode fill:#4a148c,stroke:#ce93d8,stroke-width:2px,color:#ffffff
```

### Recursion Call Stack Visualization (Preorder)

### 📌 preorder(A)

- print A
- **preorder(B)**
  - print B
  - **preorder(D)**
    - print D
  - **preorder(E)**
    - print E
- **preorder(C)**
  - print C


### Iterative Traversal Using Explicit Stack

```
ITERATIVE PREORDER (using stack):

Stack: [A]       -> Pop A, print A, push right C, push left B
Stack: [C, B]    -> Pop B, print B, push right E, push left D
Stack: [C, E, D] -> Pop D, print D
Stack: [C, E]    -> Pop E, print E
Stack: [C]       -> Pop C, print C
Stack: []        -> Done

Output: A, B, D, E, C  (matches recursive order)
```

### Tree Anatomy Measurements

```mermaid
flowchart TD
    A["A (Depth: 0, Height: 2)"]:::rootNode
    B["B (Depth: 1, Height: 1)"]:::internalNode
    C["C (Depth: 1, Height: 0)"]:::leafNode
    D["D (Depth: 2, Height: 0)"]:::leafNode
    E["E (Depth: 2, Height: 0)"]:::leafNode

    A --> B
    A --> C
    B --> D
    B --> E

    classDef rootNode fill:#0d47a1,stroke:#82b1ff,stroke-width:2px,color:#ffffff
    classDef internalNode fill:#37474f,stroke:#90a4ae,stroke-width:2px,color:#ffffff
    classDef leafNode fill:#1b5e20,stroke:#81c784,stroke-width:2px,color:#ffffff
```

- **Height Calculation (bottom-up):**
  - `height(D) = 0`, `height(E) = 0`, `height(C) = 0` (leaves)
  - `height(B) = 1 + max(0, 0) = 1`
  - `height(A) = 1 + max(1, 0) = 2`
- **Depth Calculation (top-down):**
  - `depth(A) = 0` (root)
  - `depth(B) = 1`, `depth(C) = 1`
  - `depth(D) = 2`, `depth(E) = 2`

---

## 3️⃣ DAY 2: BINARY SEARCH TREES — VISUAL CONCEPTS

### BST Invariant — The Core Property

```mermaid
flowchart TD
    subgraph ValidBST["✅ Valid BST (Left &lt; Root &lt; Right)"]
        V5["5"] --> V3["3"]
        V5 --> V7["7"]
        V3 --> V1["1"]
        V3 --> V4["4"]
        V7 --> V9["9"]
    end

    subgraph InvalidBST["❌ Violates Invariant (6 &lt; 5 in right subtree)"]
        I5["5"] --> I3["3"]
        I5 --> I8["8"]
        I3 --> I1["1"]
        I8 --> I6["6 (Illegal: 6 &lt; 5)"]:::errorNode
    end

    classDef default fill:#1e293b,stroke:#3b82f6,stroke-width:2px,color:#ffffff
    classDef errorNode fill:#b71c1c,stroke:#ff8a80,stroke-width:2px,color:#ffffff
```

### BST Search Visualization

Searching for target key `6`:

```mermaid
flowchart TD
    S5["1️⃣ Root 5 (6 > 5 -> Go Right)"]:::searchStep --> S7["2️⃣ Node 7 (6 &lt; 7 -> Go Left)"]:::searchStep
    S5 -.-> S3["Node 3"]
    S7 --> SNull["3️⃣ Left is null -> NOT FOUND ❌"]:::nullStep
    S7 -.-> S9["Node 9"]

    classDef searchStep fill:#0d47a1,stroke:#82b1ff,stroke-width:2px,color:#ffffff
    classDef nullStep fill:#b71c1c,stroke:#ff8a80,stroke-width:2px,color:#ffffff
    classDef default fill:#37474f,stroke:#90a4ae,stroke-width:1px,color:#ffffff
```

- **Search Path:** `5 -> 7 -> null`
- **Time Complexity:** `O(height)` (`O(log N)` average, `O(N)` worst case).

### BST Insertion Mechanics

Inserting key `6` into tree:

```mermaid
flowchart TD
    I5["5"] --> I3["3"]
    I5 --> I7["7"]
    I3 --> I1["1"]
    I3 --> I4["4"]
    I7 --> I6["6 (New Node Added)"]:::newNode
    I7 --> I9["9"]

    classDef default fill:#1e293b,stroke:#3b82f6,stroke-width:2px,color:#ffffff
    classDef newNode fill:#1b5e20,stroke:#81c784,stroke-width:2px,color:#ffffff
```

- **Insertion Steps:**
  1. Compare `6` with `5` (`6 > 5` -> go right to `7`).
  2. Compare `6` with `7` (`6 < 7` -> go left to `null`).
  3. Attach `6` as the left child of `7`.

### BST Deletion Cases

```mermaid
flowchart TD
    subgraph Case1["Case 1: Leaf Deletion (Delete 4)"]
        direction TB
        subgraph C1B["Before"]
            B5_1["5"] --> B3_1["3"]
            B5_1 --> B7_1["7"]
            B3_1 --> B1_1["1"]
            B3_1 --> B4_1["4 (Leaf)"]:::deletedNode
            B7_1 --> B9_1["9"]
        end
        subgraph C1A["After"]
            A5_1["5"] --> A3_1["3"]
            A5_1 --> A7_1["7"]
            A3_1 --> A1_1["1"]
            A7_1 --> A9_1["9"]
        end
        C1B ==>|"Sever pointer"| C1A
    end

    classDef deletedNode fill:#b71c1c,stroke:#ff8a80,stroke-width:2px,color:#ffffff
    classDef default fill:#1e293b,stroke:#3b82f6,stroke-width:2px,color:#ffffff
```

```mermaid
flowchart TD
    subgraph Case2["Case 2: Single Child Deletion (Delete 7)"]
        direction TB
        subgraph C2B["Before"]
            B5_2["5"] --> B3_2["3"]
            B5_2 --> B7_2["7 (To Delete)"]:::deletedNode
            B7_2 --> B9_2["9"]
            B9_2 --> B8_2["8"]
        end
        subgraph C2A["After"]
            A5_2["5"] --> A3_2["3"]
            A5_2 --> A9_2["9 (Bypasses 7)"]:::promotedNode
            A9_2 --> A8_2["8"]
        end
        C2B ==>|"Bypass 7"| C2A
    end

    classDef deletedNode fill:#b71c1c,stroke:#ff8a80,stroke-width:2px,color:#ffffff
    classDef promotedNode fill:#1b5e20,stroke:#81c784,stroke-width:2px,color:#ffffff
    classDef default fill:#1e293b,stroke:#3b82f6,stroke-width:2px,color:#ffffff
```

### Inorder Traversal = Sorted Order (Proof)

```mermaid
flowchart TD
    N5["5"] --> N3["3"]
    N5 --> N7["7"]
    N3 --> N1["1"]
    N3 --> N4["4"]
    N7 --> N6["6"]
    N7 --> N9["9"]

    classDef default fill:#1e293b,stroke:#3b82f6,stroke-width:2px,color:#ffffff
```

- **Inorder Visit Order:** `[1] -> [3] -> [4] -> [5] -> [6] -> [7] -> [9]`
- **Invariant Guarantee:** Left subtree (`< root`) -> Root -> Right subtree (`> root`) strictly preserves ascending numerical order.

### Degenerate Tree (Sorted Input)

```mermaid
flowchart TD
    subgraph Degenerate["❌ Adversarial Sorted Input: [1, 2, 3, 4, 5]"]
        direction TB
        D1["1"] --> D2["2"]
        D2 --> D3["3"]
        D3 --> D4["4"]
        D4 --> D5["5"]
    end

    subgraph Balanced["✅ Balanced Self-Structuring: [3, 2, 4, 1, 5]"]
        direction TB
        B3["3"] --> B2["2"]
        B3 --> B4["4"]
        B2 --> B1["1"]
        B4 --> B5["5"]
    end

    classDef default fill:#1e293b,stroke:#3b82f6,stroke-width:2px,color:#ffffff
```

- **Unbalanced Height:** `5` (Degrades to linked list, `O(N)` search).
- **Balanced Height:** `2` (`O(log N)` search).

---

## 4️⃣ DAY 3: BALANCED TREES — VISUAL CONCEPTS

### AVL Balance Factor Calculation

```mermaid
flowchart TD
    subgraph BalancedAVL["✅ Balanced AVL (|BF| &le; 1)"]
        B5["5 (BF: 0)"] --> B3["3 (BF: 0)"]
        B5 --> B7["7 (BF: 0)"]
        B3 --> B1["1 (BF: 0)"]
        B3 --> B4["4 (BF: 0)"]
    end

    subgraph UnbalancedAVL["❌ Unbalanced Tree (|BF| = 2)"]
        U5["5 (BF: +2)"] --> U3["3 (BF: +1)"]
        U3 --> U1["1 (BF: 0)"]
    end

    classDef default fill:#1e293b,stroke:#3b82f6,stroke-width:2px,color:#ffffff
```

- **Balance Factor Formula:** `BF(node) = height(left) - height(right)`.
- **AVL Invariant:** Every node must strictly satisfy `BF ∈ {-1, 0, +1}`.

### AVL Rotations — 4 Cases Visualized

```mermaid
flowchart TD
    subgraph LL["Case 1: LL Imbalance -> Single Right Rotate"]
        LL_B1["1 (BF: +2)"] --> LL_B2["2 (BF: +1)"]
        LL_B2 --> LL_B3["3 (BF: 0)"]
        LL_B1 -.->|"Rotate Right(1)"| LL_A2["2 (BF: 0)"]
        LL_A2 --> LL_A3["3"]
        LL_A2 --> LL_A1["1"]
    end

    subgraph RR["Case 2: RR Imbalance -> Single Left Rotate"]
        RR_B5["5 (BF: -2)"] --> RR_B4["4 (BF: -1)"]
        RR_B4 --> RR_B3["3 (BF: 0)"]
        RR_B5 -.->|"Rotate Left(5)"| RR_A4["4 (BF: 0)"]
        RR_A4 --> RR_A5["5"]
        RR_A4 --> RR_A3["3"]
    end

    subgraph LR["Case 3: LR Imbalance -> Double Rotate"]
        LR_B1["1 (BF: +2)"] --> LR_B3["3 (BF: -1)"]
        LR_B3 --> LR_B2["2 (BF: 0)"]
        LR_B1 -.->|"1. Left(3)<br/>2. Right(1)"| LR_A2["2 (BF: 0)"]
        LR_A2 --> LR_A3["3"]
        LR_A2 --> LR_A1["1"]
    end

    subgraph RL["Case 4: RL Imbalance -> Double Rotate"]
        RL_B5["5 (BF: -2)"] --> RL_B3["3 (BF: +1)"]
        RL_B3 --> RL_B4["4 (BF: 0)"]
        RL_B5 -.->|"1. Right(3)<br/>2. Left(5)"| RL_A4["4 (BF: 0)"]
        RL_A4 --> RL_A5["5"]
        RL_A4 --> RL_A3["3"]
    end

    classDef default fill:#1e293b,stroke:#3b82f6,stroke-width:2px,color:#ffffff
```

### Red-Black Tree Coloring Rules

```mermaid
flowchart TD
    subgraph ValidRB["✅ Valid Red-Black Tree"]
        V10["10 (Black)"]:::blackNode --> V5["5 (Red)"]:::redNode
        V10 --> V15["15 (Black)"]:::blackNode
        V5 --> V3["3 (Black)"]:::blackNode
        V5 --> V7["7 (Black)"]:::blackNode
        V15 --> V12["12 (Black)"]:::blackNode
        V15 --> V20["20 (Red)"]:::redNode
    end

    subgraph InvalidRB["❌ Invalid: Two Consecutive Red Nodes"]
        I10["10 (Black)"]:::blackNode --> I5["5 (Red)"]:::redNode
        I10 --> I15["15 (Black)"]:::blackNode
        I5 --> I3["3 (Red - Violation!)"]:::violationNode
        I5 --> I7["7 (Black)"]:::blackNode
    end

    classDef blackNode fill:#263238,stroke:#eceff1,stroke-width:2px,color:#ffffff
    classDef redNode fill:#b71c1c,stroke:#ff8a80,stroke-width:2px,color:#ffffff
    classDef violationNode fill:#d50000,stroke:#ff1744,stroke-width:3px,color:#ffffff
```

**The 5 Red-Black Invariants:**
1. Every node is colored either **Red** or **Black**.
2. The root node is always **Black**.
3. All leaf `null` pointers are considered **Black**.
4. No two consecutive **Red** nodes are permitted (a red node's children must both be black).
5. Every path from any node to its descendant `null` pointers contains an identical count of black nodes (**Black-Height Property**).

### Black-Height Property

```mermaid
flowchart TD
    R10["10 (Black)"]:::blackNode --> R5["5 (Black)"]:::blackNode
    R10 --> R15["15 (Black)"]:::blackNode
    R5 --> R3["3 (Red)"]:::redNode
    R5 --> R7["7 (Red)"]:::redNode
    R15 --> R12["12 (Red)"]:::redNode
    R15 --> R20["20 (Red)"]:::redNode

    classDef blackNode fill:#263238,stroke:#eceff1,stroke-width:2px,color:#ffffff
    classDef redNode fill:#b71c1c,stroke:#ff8a80,stroke-width:2px,color:#ffffff
```

- **Path Verification to Leaves:**
  - `10 -> 5 -> 3 -> null`: 2 black nodes (`10`, `5`)
  - `10 -> 5 -> 7 -> null`: 2 black nodes (`10`, `5`)
  - `10 -> 15 -> 12 -> null`: 2 black nodes (`10`, `15`)
  - `10 -> 15 -> 20 -> null`: 2 black nodes (`10`, `15`)
- **Conclusion:** Black-height invariant is universally satisfied (`bh = 2`). Strict `O(log N)` search depth is guaranteed!

### AVL vs Red-Black Trade-offs


| Property | AVL | Red-Black |
| :--- | :--- | :--- |
| Height | ~1.0×log₂(n) | ~1.5×log₂(n) |
| Balance | Strict ±1 | Loose (colors) |
| Rotations/ins | ~1 avg | ~1 avg (fewer) |
| Worst case | ~2 rotations | ~3 rotations |
| Lookup speed | Faster (tighter) | Slightly slower |
| Insert speed | Slower (rotate) | Faster |
| Production use | Rare (LLVM) | Common (Java) |


---

## 5️⃣ DAY 4: TREE PATTERNS — VISUAL CONCEPTS

### Path Sum Visualization

Finding all root-to-leaf paths that sum to target `7`:

```mermaid
flowchart TD
    N1["1 (Root)"]:::rootNode --> N2["2 (Remaining: 6)"]:::pathNode
    N1 --> N3["3 (Remaining: 6)"]
    N2 --> N4["4 (Leaf: Sum = 1+2+4 = 7 ✅)"]:::validPath
    N2 --> N5["5 (Leaf: Sum = 1+2+5 = 8 ❌)"]:::invalidPath
    N3 --> N6["6 (Leaf: Sum = 1+3+6 = 10 ❌)"]:::invalidPath

    classDef rootNode fill:#0d47a1,stroke:#82b1ff,stroke-width:2px,color:#ffffff
    classDef pathNode fill:#1565c0,stroke:#90caf9,stroke-width:2px,color:#ffffff
    classDef validPath fill:#1b5e20,stroke:#81c784,stroke-width:2px,color:#ffffff
    classDef invalidPath fill:#37474f,stroke:#90a4ae,stroke-width:1px,color:#ffffff
```

- **Candidate Path `1 -> 2 -> 4`:** Sum is `1 + 2 + 4 = 7` (Matches target, valid result).
- **Candidate Path `1 -> 2 -> 5`:** Sum is `8 != 7` (Pruned).
- **Candidate Path `1 -> 3`:** Not a leaf or sum does not match.

### Tree Diameter — Longest Path

Longest distance between any two leaf nodes:

```mermaid
flowchart TD
    D5["5 (h: 3, Diam through 5 = 2+2 = 4)"]:::rootNode
    D3["3 (h: 2)"]:::pathNode
    D7["7 (h: 2)"]:::pathNode
    D1["1 (h: 1)"]:::pathNode
    D10["10 (h: 1)"]:::pathNode
    D0["0 (Leaf)"]:::pathNode
    D8["8 (Leaf)"]
    D15["15 (Leaf)"]:::pathNode

    D5 --> D3
    D5 --> D7
    D3 --> D1
    D7 --> D10
    D1 --> D0
    D10 --> D8
    D10 --> D15

    classDef rootNode fill:#0d47a1,stroke:#82b1ff,stroke-width:2px,color:#ffffff
    classDef pathNode fill:#e65100,stroke:#ffb74d,stroke-width:2px,color:#ffffff
    classDef default fill:#1e293b,stroke:#3b82f6,stroke-width:2px,color:#ffffff
```

- **Longest Path:** `0 -> 1 -> 3 -> 5 -> 7 -> 10 -> 15`
- **Edges:** `6` edges (7 nodes).
- **Recurrence:** `diameter(node) = max(diam(left), diam(right), height(left) + height(right))`.

### Lowest Common Ancestor (LCA) — Visual Path

Find `LCA(1, 10)`:

```mermaid
flowchart TD
    L5["5 (LCA Meeting Point ✅)"]:::lcaNode
    L3["3 (Ancestor of 1)"]:::leftPath
    L7["7 (Ancestor of 10)"]:::rightPath
    L1["1 (Target Node p)"]:::leftPath
    L10["10 (Target Node q)"]:::rightPath

    L5 --> L3
    L5 --> L7
    L3 --> L1
    L7 --> L10

    classDef lcaNode fill:#1b5e20,stroke:#81c784,stroke-width:2px,color:#ffffff
    classDef leftPath fill:#0d47a1,stroke:#82b1ff,stroke-width:2px,color:#ffffff
    classDef rightPath fill:#e65100,stroke:#ffb74d,stroke-width:2px,color:#ffffff
```

- **Left DFS Path:** Returns node `1` up through `3` to `5`.
- **Right DFS Path:** Returns node `10` up through `7` to `5`.
- **Meeting Point:** Both left and right recursive calls return non-null at root `5` -> `5` is the Lowest Common Ancestor!

### Serialization — Preorder with Null Markers

```mermaid
flowchart TD
    subgraph TreeStructure["Original Tree"]
        direction TB
        T1["1"] --> T2["2"]
        T1 --> T3["3"]
        T2 --> TNull1["null"]:::nullNode
        T2 --> TNull2["null"]:::nullNode
        T3 --> TNull3["null"]:::nullNode
        T3 --> TNull4["null"]:::nullNode
    end

    subgraph Serialized["Preorder String"]
        S["'1, 2, null, null, 3, null, null'"]:::strNode
    end

    TreeStructure ==>|"Serialize"| Serialized
    Serialized ==>|"Deserialize via Queue"| TreeStructure

    classDef nullNode fill:#37474f,stroke:#78909c,stroke-width:1px,color:#ffffff
    classDef strNode fill:#4a148c,stroke:#ce93d8,stroke-width:2px,color:#ffffff
    classDef default fill:#1e293b,stroke:#3b82f6,stroke-width:2px,color:#ffffff
```

---

## 6️⃣ DAY 5: AUGMENTED TREES — VISUAL CONCEPTS

### Augmentation with Subtree Size

```mermaid
flowchart TD
    subgraph StandardBST["Standard BST"]
        S5["5"] --> S3["3"]
        S5 --> S8["8"]
        S3 --> S1["1"]
        S3 --> S4["4"]
        S8 --> S7["7"]
        S8 --> S10["10"]
    end

    subgraph AugmentedBST["Augmented BST (Subtree Size)"]
        A5["5 (size: 7)"] --> A3["3 (size: 3)"]
        A5 --> A8["8 (size: 3)"]
        A3 --> A1["1 (size: 1)"]
        A3 --> A4["4 (size: 1)"]
        A8 --> A7["7 (size: 1)"]
        A8 --> A10["10 (size: 1)"]
    end

    classDef default fill:#1e293b,stroke:#3b82f6,stroke-width:2px,color:#ffffff
```

- **Subtree Size Formula:** `size(node) = 1 + size(node.left) + size(node.right)`.
- **Capabilities Unlocked:**
  - `k`-th smallest element search in `O(log N)`.
  - Rank queries (`count(x <= target)`) in `O(log N)`.
  - Range counting (`count(L <= x <= R)`) in `O(log N)`.

### Order-Statistics Query — Finding kth Smallest

Find `3`-rd smallest element (`k = 3`) in augmented tree:

```mermaid
flowchart TD
    Q5["1️⃣ Root 5 (size: 7)<br/>leftSize(3) = 3 >= k(3) -> Go Left"]:::stepNode --> Q3["2️⃣ Node 3 (size: 3)<br/>leftSize(1) = 1 &lt; k(3) -> Go Right with k'=3-1-1=1"]:::stepNode
    Q3 --> Q4["3️⃣ Node 4 (size: 1)<br/>leftSize(0) + 1 == k'(1) -> Found! ✅"]:::foundNode
    Q3 -.-> Q1["Node 1 (size: 1)"]

    classDef stepNode fill:#0d47a1,stroke:#82b1ff,stroke-width:2px,color:#ffffff
    classDef foundNode fill:#1b5e20,stroke:#81c784,stroke-width:2px,color:#ffffff
    classDef default fill:#37474f,stroke:#90a4ae,stroke-width:1px,color:#ffffff
```

- **Result:** `3rd` smallest element is `4`. Time complexity: `O(log N)`.

### Rank Query — How Many Elements ≤ X

Find the rank of `6` (count of keys `<= 6`):

```mermaid
flowchart TD
    R5["1️⃣ Node 5: 6 > 5 -> Go Right<br/>Accumulate leftSize + 1 = 3 + 1 = 4"]:::accNode --> R8["2️⃣ Node 8: 6 &lt; 8 -> Go Left<br/>No accumulation"]:::stepNode
    R8 --> R7["3️⃣ Node 7: 6 &lt; 7 -> Go Left<br/>Left is null -> Terminate"]:::stepNode

    classDef accNode fill:#1b5e20,stroke:#81c784,stroke-width:2px,color:#ffffff
    classDef stepNode fill:#0d47a1,stroke:#82b1ff,stroke-width:2px,color:#ffffff
```

- **Total Elements `<= 6`:** `4` (`{1, 3, 4, 5}`).

### Range Count [L, R]

```
Count values in [4, 8]:

rank(8) = 6 (nodes ≤ 8: 1, 3, 4, 5, 7, 8)
rank(3) = 2 (nodes ≤ 3: 1, 3)

Count in [4, 8] = rank(8) - rank(3) = 6 - 2 = 4
Actually in range: 4, 5, 7, 8 ✓
```

### Augmentation Maintenance During Insertion

```mermaid
flowchart TD
    subgraph BeforeInsert["Before: Root size = 7"]
        direction TB
        B5["5 (size: 7)"] --> B3["3 (size: 3)"]
        B5 --> B8["8 (size: 3)"]
        B8 --> B7["7 (size: 1)"]
        B8 --> B10["10 (size: 1)"]
    end

    subgraph AfterInsert["After: Insert 6 (Ancestors Updated)"]
        direction TB
        A5["5 (size: 8 ⬆️)"]:::updateNode --> A3["3 (size: 3)"]
        A5 --> A8["8 (size: 4 ⬆️)"]:::updateNode
        A8 --> A7["7 (size: 2 ⬆️)"]:::updateNode
        A8 --> A10["10 (size: 1)"]
        A7 --> A6["6 (size: 1 🆕)"]:::newNode
    end

    BeforeInsert ==>|"Insert 6 and Increment Ancestors"| AfterInsert

    classDef updateNode fill:#1565c0,stroke:#90caf9,stroke-width:2px,color:#ffffff
    classDef newNode fill:#1b5e20,stroke:#81c784,stroke-width:2px,color:#ffffff
    classDef default fill:#1e293b,stroke:#3b82f6,stroke-width:2px,color:#ffffff
```

- **Update Mechanism:** Bottom-up backpropagation adds `+1` to every ancestor along the insertion path from newly created leaf to root in `O(log N)` time.

---

## 7️⃣ CROSS-DAY VISUAL COMPARISONS

### Comparison: All 5 Tree Variants

| Tree Variant | Structural Invariant | Search Time | Insertion Time | Optimal Use Case |
| :--- | :--- | :--- | :--- | :--- |
| **Generic Tree** | Acyclic connected graph | `O(N)` | `O(1)` | Hierarchies, DOM, ASTs |
| **BST** | Left < Node < Right | `O(height)` | `O(height)` | Basic dictionary lookups |
| **AVL Tree** | Strict `\|BF\| <= 1` | `O(log N)` | `O(log N)` | Read-heavy lookups |
| **Red-Black Tree** | 5 color rules, `bh` equality | `O(log N)` | `O(log N)` | Production standard collections |
| **Augmented BST** | Subtree metadata caches | `O(log N)` | `O(log N)` | Order statistics, rank, range |

### Comparison: Insertion Scenarios

```mermaid
flowchart TD
    subgraph RandomOrder["Random Input: [1, 5, 3, 8, 2] (Naturally Balanced)"]
        R5["5"] --> R3["3"]
        R5 --> R8["8"]
        R3 --> R1["1"]
        R1 --> R2["2"]
    end

    subgraph SortedOrder["Sorted Input: [1, 2, 3, 4, 5] (Balanced via Rotations)"]
        S3["3"] --> S2["2"]
        S3 --> S4["4"]
        S2 --> S1["1"]
        S4 --> S5["5"]
    end

    classDef default fill:#1e293b,stroke:#3b82f6,stroke-width:2px,color:#ffffff
```

---

## 8️⃣ MEMORY LAYOUT DIAGRAMS

### Pointer-Based Heap Tree vs Array-Based Segment Tree

| Feature | Pointer-Based Heap Tree | Array-Based Segment / Binary Heap |
| :--- | :--- | :--- |
| **Storage Allocation** | Scattered heap allocations (`malloc`/`new`) | Contiguous 1D buffer (`T[]`) |
| **Pointer Overhead** | 16–24 bytes per node (`left`, `right`, `parent`) | **0 bytes** (implicit index arithmetic) |
| **Child Navigation** | `node.left`, `node.right` | `2 * i`, `2 * i + 1` |
| **CPU Cache Locality** | Poor (pointer chasing causes L1/L2 cache misses) | Excellent (sequential cache lines preloaded) |

---

## 9️⃣ COMPLEXITY & PERFORMANCE VISUALIZATIONS

### Big-O Complexity Matrix

| Operation | Generic Binary Tree | Unbalanced BST | AVL Tree | Red-Black Tree | Augmented Order-Stat BST |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Search** | `O(N)` | `O(N)` worst / `O(log N)` avg | `O(log N)` | `O(log N)` | `O(log N)` |
| **Insert** | `O(N)` | `O(N)` worst / `O(log N)` avg | `O(log N)` | `O(log N)` | `O(log N)` |
| **Delete** | `O(N)` | `O(N)` worst / `O(log N)` avg | `O(log N)` | `O(log N)` | `O(log N)` |
| **K-th Smallest** | `O(N)` | `O(N)` | `O(N)` | `O(N)` | **`O(log N)`** |
| **Rank Query** | `O(N)` | `O(N)` | `O(N)` | `O(N)` | **`O(log N)`** |

---

## 🔟 INTERVIEW VISUAL REFERENCE

### Red Flags & Problem Patterns

| Interview Trigger Phrase | Algorithmic Pattern | Visual Mental Hook |
| :--- | :--- | :--- |
| *"Find k-th smallest element dynamically"* | Augmented BST with subtree sizes | Subtree size counting binary search down left/right |
| *"Find rank or percentile of key"* | Order Statistics Augmented Tree | Accumulate `leftSize + 1` when navigating right |
| *"Serialize and deserialize binary tree"* | Preorder DFS with null sentinels | Root first + `null` markers for reconstructing uniquely |
| *"Lowest common ancestor"* | 3-Way recursive postorder / binary lifting | Left and right recursive calls both return non-null |
| *"Longest path between any two nodes"* | Postorder DFS subtree diameter | Pass `(height, maxDiameter)` bottom-up |
| *"Find all root-to-leaf paths summing to K"* | DFS with backtracking | Push node to path list, recurse, backtrack by popping |

### Common Interview Bugs to Avoid

> [!WARNING]
> 1. **Omitting `null` markers in serialization:** A single traversal without `null` sentinels cannot uniquely reconstruct a general binary tree.
> 2. **Forgetting to update subtree sizes after rotations:** If you perform AVL/Red-Black rotations on an augmented tree, you must recalculate `size` on the old sub-root and new sub-root!
> 3. **Rank query off-by-one errors:** When branching right, always include `leftSize + 1` (accounting for the current node itself).
> 4. **Degenerating to `O(N)` on sorted inputs:** Mention self-balancing properties (Red-Black / AVL) when an interviewer asks about worst-case inputs.

### Core Algorithmic Mechanics

```mermaid
flowchart TD
    subgraph LeftRotate["Left Rotate around x"]
        direction TB
        LX["x"] --> LY["y"]
        LY --> LB["b"]
        LY --> LC["c"]
    end
    subgraph RightRotate["After Left Rotate"]
        direction TB
        RY["y"] --> RX["x"]
        RY --> RC["c"]
        RX --> RB["b"]
    end
    LeftRotate ==>|"O(1) pointer updates"| RightRotate

    classDef default fill:#1e293b,stroke:#3b82f6,stroke-width:2px,color:#ffffff
```

---

## 📚 VISUAL ENCODING GUIDE

### Modern Visual Standards

- **Node Fills:** High-contrast dark backgrounds (`fill:#1e293b`) with clear borders (`stroke:#3b82f6`) and white text (`color:#ffffff`).
- **Success/Valid States:** Forest green fills (`fill:#1b5e20`, `stroke:#81c784`).
- **Violations/Errors:** Crimson red fills (`fill:#b71c1c`, `stroke:#ff8a80`).
- **Focus/Steps:** Deep sapphire blue fills (`fill:#0d47a1`, `stroke:#82b1ff`).
- **Labels:** Explicitly double-quoted strings preventing syntax collisions.


### Color/Emphasis Scheme

```
Algorithm steps:
  STEP 1 → STEP 2 → ... ← progression
  
Comparison:
  Table with columns | Cell highlighting key differences
  
Warnings:
  ⚠️  Important / Easy to miss
  
Facts:
  ✓ Proven / Verified
  
Complexity:
  O(log n) ← asymptotic notation
```

---

## 📋 VISUAL QUICK REFERENCE TABLE

### All Diagrams by Topic

| Day | Topic | Diagram Type | Count | Key Visual |
| :--- | :--- | :--- | :--- | :--- |
| 1 | Traversals | Tree structures + paths | 4 | All 4 orders side-by-side |
| 1 | Stack traces | Call stack visualization | 2 | Recursive vs iterative |
| 2 | BST operations | Before/after diagrams | 4 | Insert, delete (3 cases) |
| 2 | Inorder = sorted | Visual proof | 1 | Left-parent-right mapping |
| 3 | Rotations | LL, LR, RR, RL cases | 4 | All 4 rotation types |
| 3 | Balance factors | Node annotations | 2 | AVL height differences |
| 3 | RB coloring | Color diagrams | 2 | Valid/invalid colorings |
| 4 | Path sum | Root-to-leaf paths | 2 | All paths enumerated |
| 4 | LCA | Ancestor path traces | 2 | Path meeting point |
| 4 | Serialization | Array encoding | 2 | Preorder with nulls |
| 5 | Augmentation | Size annotations | 3 | Subtree size calculation |
| 5 | Order-stats | Binary search trace | 2 | kth smallest navigation |
| 5 | Rank queries | Ancestor accumulation | 2 | How many ≤ x |
| **Total** | | | **38** | |

---

## 🎓 USING THIS VISUAL GUIDE

### For Self-Study
1. Read the instructional file (narrative text)
2. Consult this visual guide when concepts feel abstract
3. Redraw diagrams yourself to internalize structure
4. Try "visual debugging" when tracing algorithms

### For Interview Preparation
1. Mentally convert problem description to tree visualization
2. Sketch the tree as you talk through the solution
3. Use these visual patterns to recognize problem types
4. Practice explaining using diagrams, not just code

### For Teaching/Mentoring
1. Use diagrams to explain key concepts clearly
2. Have students redraw diagrams from memory
3. Use visual comparisons to highlight trade-offs
4. Point to specific visual patterns for red flags

---

**End of Week 7 Visual Hybrid Support Guide**

**Diagram Count: 38 ASCII diagrams + descriptions**  
**Coverage:** All 5 days, all major concepts, comparison tables, memory layouts, complexity analysis

---

> 🧭 **Navigation:** [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md)

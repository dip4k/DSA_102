# Phase 19: Union-Find / Disjoint Set

> **Focus:** Dynamic Equivalence Relations, Path Compression, Union by Rank/Size, Amortized Inverse Ackermann Complexity $\alpha(N)$, Integer Account Indexing, and Spanning Tree Invariants.  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 19 (Problems #102–#105)

---

## 102. Number of Provinces (LeetCode #547)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#union-find` `#disjoint-set` `#connected-components` `#equivalence-classes` |
| **LeetCode Link** | [Number of Provinces](https://leetcode.com/problems/number-of-provinces/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** There are $n$ cities. Some of them are connected directly, while others are connected indirectly. Given an $n \times n$ matrix `isConnected` where `isConnected[i][j] = 1` if city $i$ and city $j$ are directly connected, return the total number of provinces (connected components).
- **Key Constraints:**
  - $1 \le n \le 200$.
  - $\text{isConnected}[i][i] == 1$.
  - $\text{isConnected}[i][j] == \text{isConnected}[j][i]$.
  - The graph is undirected and represented as an adjacency matrix.
- **Senior Edge Cases to Defend:**
  - Fully disconnected graph (identity matrix $\implies n$ provinces).
  - Fully connected complete graph ($K_n \implies 1$ province).
  - Multi-city linear chain ($0-1-2-\dots-n-1 \implies 1$ province).

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Dynamic Equivalence Partitioning. Initialize $n$ disjoint sets. For each upper-triangular edge $(i, j)$ where $\text{isConnected}[i][j] == 1$, merge their sets; each successful merge of previously disjoint sets decrements the total province counter by 1.
- **Sample 1:**
  - **Input:** `isConnected = [[1,1,0],[1,1,0],[0,0,1]]`
  - **Output:** `2` (Province 1: $\{0, 1\}$, Province 2: $\{2\}$).

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine $n$ independent city-states at the dawn of civilization. Each city-state hoists its own distinct flag and declares itself an independent province (total provinces = $n$). Diplomats read through a list of bilateral treaties (`isConnected`). When a treaty connects City $i$ and City $j$, the heralds check whether City $i$ and City $j$ already swear fealty to the same high king (the canonical root). If they already belong to the same alliance, the treaty changes nothing. If they belong to different kingdoms, the smaller kingdom lowers its flag and pledges fealty to the larger kingdom's monarch. With this single unification pact, the total number of independent sovereign realms on the continent decreases by exactly 1.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force approach might compute the transitive closure matrix using the Floyd-Warshall algorithm ($O(N^3)$ time), or repeatedly perform broad searches across the matrix to compute connected components. In an adjacency matrix, scanning all pairs requires $O(N^2)$ checks. Disjoint Set Union optimizes this by checking each undirected edge once (upper triangle $j > i$) and executing union-find operations in near $O(1)$ amortized time ($O(\alpha(N))$).

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Inverse Ackermann Complexity $\alpha(N)$:** By combining two optimizations:
  1. **Path Compression** in `Find`: Flattens the tree so that every node on the lookup path points directly to the root.
  2. **Union by Rank** in `Union`: Always attaches the root of the shallower tree under the root of the deeper tree, preventing tall degenerative linear tree chains.
  The amortized time per operation is $O(\alpha(N))$, where $\alpha(N) \le 4$ for any input size up to $10^{80}$ (the number of atoms in the observable universe).
- **Component Decrement Invariant:** Start with `Count = n`. For any pair $(i, j)$, if $\text{Find}(i) \ne \text{Find}(j)$, executing `Union(i, j)` unifies two distinct trees into one and decrements `Count` by 1. If $\text{Find}(i) == \text{Find}(j)$, the edge is redundant and `Count` remains unchanged.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
DSU Forest State:
Initially:  {0}  {1}  {2}  ...  {n-1}     (Count = n)

Upper-Triangular Scan (i: 0 -> n-1, j: i+1 -> n-1):
  Gate: isConnected[i][j] == 1
    rootI = Find(i)
    rootJ = Find(j)
    If rootI != rootJ:
      Attach shallower root under deeper root
      Count--
```

#### 3.5 State Transition Triggers & Decision Gates
- Loop $i$ from $0$ to $n - 1$:
  - Loop $j$ from $i + 1$ to $n - 1$:
    - **Matrix Gate:** If `isConnected[i][j] == 1`:
      - `rootI = Find(i)`
      - `rootJ = Find(j)`
      - **DSU Gate:** If `rootI != rootJ`:
        - Update parent pointer based on rank.
        - `Count--`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `isConnected = [[1,1,0],[1,1,0],[0,0,1]]` ($n = 3$).
- Initial: `parent = [0, 1, 2]`, `rank = [0, 0, 0]`, `Count = 3`.
- Upper triangle pairs to check: $(0, 1), (0, 2), (1, 2)$.
- **Pair $(0, 1)$:**
  - `isConnected[0][1] == 1`.
  - `Find(0) = 0`, `Find(1) = 1`. Roots differ!
  - `rank[0] == rank[1] \implies parent[1] = 0, rank[0] = 1`.
  - `Count` decreases from 3 to 2.
- **Pair $(0, 2)$:**
  - `isConnected[0][2] == 0`. Skip.
- **Pair $(1, 2)$:**
  - `isConnected[1][2] == 0`. Skip.
- Loops complete. Final `Count = 2`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **When to choose Approach 1 (Disjoint Set Union):** Mandatory when edges arrive in an online/streaming fashion or dynamic connectivity queries are required. Provides reusable data structure architecture for senior interviews.
- **When to choose Approach 2 (DFS Matrix Traversal):** Simplest to write from scratch in time-pressured interviews when graph topology is static ($O(N^2)$ time, $O(N)$ visited space).

#### 4.2 Step-by-Step Natural Progression Flow
1. **Setup & Boundaries:** Instantiate `DisjointSet(n)`.
2. **Main Exploration Loop:** Iterate strictly over the upper triangle ($0 \le i < n, i < j < n$).
3. **Invariant Maintenance & Condition Gates:** If $\text{isConnected}[i][j] == 1$, call `Union(i, j)`.
4. **Resolution & Return:** Return `dsu.Count`.

#### 4.3 Alternative Approaches Analysis
- **DFS Component Flood-Fill:** An array `bool[] visited = new bool[n]`. For each unvisited $i$, increment province count and recursively mark all $j$ where $\text{isConnected}[i][j] == 1$. Takes $O(N^2)$ time and $O(N)$ stack space.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Disjoint Set Union (DSU) | Approach 2: DFS Matrix Traversal |
| :--- | :--- | :--- |
| **Time Complexity** | $O(N^2 \cdot \alpha(N)) \approx O(N^2)$ | $O(N^2)$ strictly |
| **Auxiliary Space** | $O(N)$ (parent + rank) | $O(N)$ (visited array + stack) |
| **Dynamic Streaming** | High (handles new edges in $O(\alpha(N))$) | Poor (requires re-running DFS) |
| **Pair Scanning** | $N(N-1)/2$ upper triangle pairs | Up to $N^2$ full row scans |
| **Cache Locality** | High | High |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Number of Provinces (#102)
// Selection Heuristic: 
//   - Approach 1 (DSU): Standard pattern for dynamic equivalence partitioning.
//   - Approach 2 (DFS): Minimal code footprint for static adjacency matrix traversal.
// Invariant: Each successful union between distinct roots decrements the total province count by 1.
// ============================================================================
```

#### Approach 1: Disjoint Set Union (Optimal Dynamic Architecture)
```csharp
public class Solution
{
    public int FindCircleNum(int[][] isConnected)
    {
        int n = isConnected.Length;
        var dsu = new DisjointSet(n);

        // Scan strictly the upper triangle to avoid redundant symmetric evaluations
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                // Gate: Direct connection between city i and city j
                if (isConnected[i][j] == 1)
                {
                    dsu.Union(i, j);
                }
            }
        }

        return dsu.Count;
    }

    private sealed class DisjointSet
    {
        private readonly int[] _parent;
        private readonly int[] _rank;
        public int Count { get; private set; }

        public DisjointSet(int n)
        {
            Count = n;
            _parent = new int[n];
            _rank = new int[n];
            // Initialize each element as its own representative root
            for (int i = 0; i < n; i++) _parent[i] = i;
        }

        // Path Compression: Flattens the lookup path by pointing nodes directly to the root
        public int Find(int x)
        {
            if (_parent[x] != x)
            {
                _parent[x] = Find(_parent[x]);
            }
            return _parent[x];
        }

        // Union by Rank: Attaches the shallower tree beneath the deeper tree
        public bool Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);

            // Cycle Guard: Already belong to the same component
            if (rootX == rootY) return false;

            if (_rank[rootX] < _rank[rootY])
            {
                _parent[rootX] = rootY;
            }
            else if (_rank[rootX] > _rank[rootY])
            {
                _parent[rootY] = rootX;
            }
            else
            {
                _parent[rootY] = rootX;
                _rank[rootX]++;
            }

            // Invariant: Unifying two distinct components reduces total province count
            Count--;
            return true;
        }
    }
}
```

#### Approach 2: Boolean DFS Matrix Traversal ($O(N^2)$)
```csharp
public class SolutionDfs
{
    public int FindCircleNum(int[][] isConnected)
    {
        int n = isConnected.Length;
        bool[] visited = new bool[n];
        int provinceCount = 0;

        for (int i = 0; i < n; i++)
        {
            if (!visited[i])
            {
                provinceCount++;
                Dfs(i, isConnected, visited, n);
            }
        }

        return provinceCount;
    }

    private static void Dfs(int city, int[][] isConnected, bool[] visited, int n)
    {
        visited[city] = true;

        for (int neighbor = 0; neighbor < n; neighbor++)
        {
            if (isConnected[city][neighbor] == 1 && !visited[neighbor])
            {
                Dfs(neighbor, isConnected, visited, n);
            }
        }
    }
}
```

---

## 103. Redundant Connection (LeetCode #684)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#union-find` `#cycle-detection` `#disjoint-set` `#spanning-tree` |
| **LeetCode Link** | [Redundant Connection](https://leetcode.com/problems/redundant-connection/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an undirected graph that started as a tree with $n$ nodes labeled from $1$ to $n$, with one additional edge added, return an edge that can be removed so that the resulting graph is a tree. If there are multiple answers, return the edge that occurs last in the input.
- **Key Constraints:**
  - $n == \text{edges.Length} \in [3, 1000]$.
  - $\text{edges}[i] = [u_i, v_i]$ where $1 \le u_i < v_i \le n$.
  - No repeated edges; the given graph is connected.
- **Senior Edge Cases to Defend:**
  - 1-indexed node identifiers: Arrays must be allocated with size $n + 1$.
  - Discarding the correct edge: If multiple cycle edges exist, the problem explicitly demands the edge that appears **last** in the input array. Processing edges chronologically with DSU naturally identifies the exact edge that closes the cycle.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Fundamental Cycle Detection. In a tree of $n$ vertices, adding an $n$-th edge creates exactly one cycle. As edges are streamed into a DSU, the first edge whose endpoints already share the same canonical root closes the cycle.
- **Sample 1:**
  - **Input:** `edges = [[1, 2], [1, 3], [2, 3]]`
  - **Output:** `[2, 3]`

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine assembling a suspension bridge using modular steel trusses. You are bolting trusses into place one after another. Your goal is to form a single rigid tree structure. Each truss connects two joints. If you attempt to install a truss between Joint $u$ and Joint $v$, and your laser scanner shows that Joint $u$ and Joint $v$ are already rigidly connected through a sequence of previously bolted trusses, this new truss creates a redundant closed loop (a cycle). Because you install trusses in strict order of delivery, the moment a truss bridges two points already in the same rigid structure, you immediately flag it as redundant.

#### 3.2 The Naive Bottleneck & Redundant Computation
A brute-force approach would remove each edge from last to first, and run a full BFS or DFS across the remaining edges to verify if the graph remains connected. This takes $O(N^2)$ time. DSU solves this in a single pass of near-constant amortized operations ($O(N \cdot \alpha(N))$).

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Single Fundamental Cycle:** A connected graph with $V$ vertices and $V$ edges has cyclomatic complexity $M = E - V + 1 = 1$. It contains exactly one fundamental cycle.
- **Chronological Detection Invariant:** All edges preceding the cycle-closing edge connect previously disconnected components, acting as tree edges. The cycle-closing edge is the unique edge $(u, v)$ for which $\text{Find}(u) == \text{Find}(v)$ at the moment of evaluation.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Streamed Edge Processing:
edges = [e_0, e_1, ..., e_{k-1}, e_k, ...]
For each edge e_k = [u, v]:
  rootU = Find(u)
  rootV = Find(v)
  - If rootU != rootV: Union(rootU, rootV) --> Safe tree edge
  - If rootU == rootV: CYCLE DETECTED! --> Return e_k immediately
```

#### 3.5 State Transition Triggers & Decision Gates
- For each `edge` in `edges`:
  - `rootU = Find(edge[0])`
  - `rootV = Find(edge[1])`
  - **Cycle Gate:** If `rootU == rootV`, return `edge`.
  - **Union Gate:** If `rootU != rootV`, perform union by rank and proceed.

#### 3.6 Concrete Step-by-Step State Trace
Input: `edges = [[1, 2], [2, 3], [3, 4], [1, 4], [1, 5]]`.
- Initial: `parent = [0, 1, 2, 3, 4, 5]`.
- `[1, 2]`: `Find(1) != Find(2) \implies Union(1, 2)`.
- `[2, 3]`: `Find(2) == 1 != Find(3) == 3 \implies Union(1, 3)`.
- `[3, 4]`: `Find(3) == 1 != Find(4) == 4 \implies Union(1, 4)`.
- `[1, 4]`: `Find(1) = 1`, `Find(4) = 1`. Equal roots! Cycle detected!
- Return `[1, 4]`. Edge `[1, 5]` is never even processed.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **When to choose Approach 1 (DSU):** Universally optimal. Processes edges in one pass, uses $O(N)$ space, requires zero graph reconstruction, and directly halts on the closing edge.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Setup & Boundaries:** Allocate 1-indexed `parent` and `rank` arrays of size $n + 1$.
2. **Main Exploration Loop:** Iterate sequentially through `edges`.
3. **Invariant Maintenance & Condition Gates:** Compare `Find(u)` and `Find(v)`. Return if equal; union if distinct.
4. **Resolution & Return:** Guaranteed to return within $n$ iterations.

#### 4.3 Alternative Approaches Analysis
- **Incremental DFS Cycle Search:** Maintain an adjacency list. Before adding edge $(u, v)$, run DFS from $u$ searching for $v$. If $v$ is reachable, $(u, v)$ creates a cycle. Time: $O(N^2)$.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Disjoint Set Union (DSU) | Approach 2: Incremental DFS Cycle Search |
| :--- | :--- | :--- |
| **Time Complexity** | $O(N \cdot \alpha(N)) \approx O(N)$ | $O(N^2)$ |
| **Auxiliary Space** | $O(N)$ (parent + rank) | $O(N)$ (adjacency list + visited) |
| **Cache Locality** | Exceptional (contiguous flat arrays) | Moderate (pointer-based lists) |
| **Code Footprint** | Extremely compact (~25 lines) | Moderate (~45 lines) |

---

### 5. Production C# Implementation

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Redundant Connection (#103)
// Selection Heuristic: 
//   - Approach 1 (DSU): Optimal single-pass cycle detection via path compression and rank.
// Invariant: The first edge whose endpoints share an identical DSU root is the redundant edge.
// ============================================================================
```

#### Approach 1: Disjoint Set Union (Path Compression + Union by Rank)
```csharp
public class Solution
{
    public int[] FindRedundantConnection(int[][] edges)
    {
        int n = edges.Length;
        // 1-indexed data structures to match vertex numbering 1 .. n
        int[] parent = new int[n + 1];
        int[] rank = new int[n + 1];
        for (int i = 1; i <= n; i++) parent[i] = i;

        // Find with recursive path compression: Directly re-parents visited nodes to root
        int Find(int x)
        {
            if (parent[x] != x)
            {
                parent[x] = Find(parent[x]);
            }
            return parent[x];
        }

        foreach (var edge in edges)
        {
            int rootU = Find(edge[0]);
            int rootV = Find(edge[1]);

            // Invariant Gate: Both vertices already share the same representative root.
            // Adding this edge closes the unique fundamental cycle!
            if (rootU == rootV)
            {
                return edge;
            }

            // Union by Rank: Attach smaller tree under root of larger tree
            if (rank[rootU] < rank[rootV])
            {
                parent[rootU] = rootV;
            }
            else if (rank[rootU] > rank[rootV])
            {
                parent[rootV] = rootU;
            }
            else
            {
                parent[rootV] = rootU;
                rank[rootU]++;
            }
        }

        return Array.Empty<int>();
    }
}
```

---

## 104. Accounts Merge (LeetCode #721)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 💡 Advanced |
| **Pattern Tags** | `#union-find` `#email-graph` `#connected-components` `#string-grouping` |
| **LeetCode Link** | [Accounts Merge](https://leetcode.com/problems/accounts-merge/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given a list of `accounts` where each entry is `accounts[i] = [name, email1, email2, ...]`, merge accounts belonging to the same person. Two accounts definitely belong to the same person if they share at least one common email address. Return the merged accounts with sorted emails and the person's name as the first element.
- **Key Constraints:**
  - $1 \le \text{accounts.Length} \le 1000$.
  - $2 \le \text{accounts}[i]\text{.Length} \le 10$.
  - Total emails across all accounts $\le 10^4$.
  - Emails consist of lowercase English letters and periods.
- **Senior Edge Cases to Defend:**
  - Name Collision with Distinct People: Two different people may share the exact same name (e.g., "John"), but have completely disjoint email sets. They must NOT be merged!
  - Transitive Email Chaining: Account 0 shares an email with Account 1, and Account 1 shares a different email with Account 2. All three accounts must be merged into one.
  - Duplicate emails within the same account: Must be deduplicated.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Connected Component Partitioning via Integer-Indexed DSU. Map each email to an account index. Union account indices directly rather than strings to minimize heap allocations and hashing overhead.
- **Sample 1:**
  - **Input:** `accounts = [["John","johnsmith@mail.com","john_newyork@mail.com"],["John","johnsmith@mail.com","john00@mail.com"],["Mary","mary@mail.com"],["John","johnnybravo@mail.com"]]`
  - **Output:** `[["John","john00@mail.com","john_newyork@mail.com","johnsmith@mail.com"],["Mary","mary@mail.com"],["John","johnnybravo@mail.com"]]`

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine an identity verification system at a global bank. Customers open multiple bank accounts over decades under the name "John". Each account lists several verified email addresses. To unify customer profiles, we treat each bank account index ($0 \dots n-1$) as an identity dossier. As we review dossiers, we register each email into a master phonebook (`emailToAccountIndex`). If an email on Dossier $i$ was previously seen on Dossier $j$, we know Dossier $i$ and Dossier $j$ belong to the exact same human being. We clip Dossier $i$ and Dossier $j$ together into the same folder (DSU union). Once all dossiers are reviewed, we gather all emails belonging to each unified folder, sort them lexicographically, and affix the account holder's name.

#### 3.2 The Naive Bottleneck & Redundant Computation
A naive implementation treats each unique email string as a DSU node, maintaining `Dictionary<string, string> parent`. With $10,000$ emails, hashing strings repeatedly during path compression and union operations creates massive CPU overhead and high GC pressure. Pairwise account comparisons ($O(N^2 \cdot K)$) perform redundant string equality tests across unrelated users.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **Integer-Indexed DSU Architecture:** The number of accounts $N \le 1000$ is small. Operating a Disjoint Set Union directly over **account indices $0 \dots N - 1$** eliminates string hashing inside the DSU entirely!
- **Email-to-First-Account Mapping Invariant:** Maintain a single lookup map `Dictionary<string, int> emailToAccount`. For each email in account $i$:
  - If the email is already in the map with account index $j$, execute `dsu.Union(i, j)`.
  - Otherwise, record `emailToAccount[email] = i`.
- **Component Root Grouping:** After unioning, every email in account $i$ belongs to the component identified by `dsu.Find(i)`. We group emails into `Dictionary<int, HashSet<string>>` keyed by the component root index.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
3-Stage Architecture Pipeline:
Stage 1: Integer DSU Union via Email Lookup
  Account 0 [John]: {johnsmith, john_newyork}  --> emailToAccount[johnsmith] = 0, emailToAccount[john_newyork] = 0
  Account 1 [John]: {johnsmith, john00}        --> "johnsmith" seen at 0 ==> DSU.Union(1, 0)
  Account 2 [Mary]: {mary}                     --> emailToAccount[mary] = 2

Stage 2: Bucket Emails by Component Root
  dsu.Find(0) == 0 ==> Root 0: {johnsmith, john_newyork, john00}
  dsu.Find(1) == 0 ==> (Merged into Root 0)
  dsu.Find(2) == 2 ==> Root 2: {mary}

Stage 3: Sort Emails & Prepend Name
  Root 0: ["John", "john00@mail.com", "john_newyork@mail.com", "johnsmith@mail.com"]
  Root 2: ["Mary", "mary@mail.com"]
```

#### 3.5 State Transition Triggers & Decision Gates
- **Phase 1 (Union Accounts):** For account $i \in [0, N-1]$, for email $e$ in `accounts[i]`:
  - Gate: If `emailToAccount.TryGetValue(e, out int prevAcc)`:
    - Action: `dsu.Union(i, prevAcc)`.
  - Else:
    - Action: `emailToAccount[e] = i`.
- **Phase 2 (Group Emails):** For account $i \in [0, N-1]$:
  - `root = dsu.Find(i)`.
  - Add all emails of account $i$ to `rootToEmails[root]`.
- **Phase 3 (Format):** For each `(root, emailSet)` in `rootToEmails`:
  - Sort `emailSet` lexicographically.
  - Prepend `accounts[root][0]` (account holder's name).

#### 3.6 Concrete Step-by-Step State Trace
Input accounts:
- Acc 0: `["John", "a@mail", "b@mail"]`
- Acc 1: `["John", "c@mail", "d@mail"]`
- Acc 2: `["John", "a@mail", "c@mail"]`
- Step 1:
  - Acc 0: `emailToAccount["a@mail"] = 0`, `emailToAccount["b@mail"] = 0`.
  - Acc 1: `emailToAccount["c@mail"] = 1`, `emailToAccount["d@mail"] = 1`.
  - Acc 2:
    - `"a@mail"` seen at 0 $\implies$ `Union(2, 0)`.
    - `"c@mail"` seen at 1 $\implies$ `Union(2, 1)`.
  - Now accounts 0, 1, 2 are all unified under a single root!
- Step 2: Grouping by root:
  - Root contains all emails: `{"a@mail", "b@mail", "c@mail", "d@mail"}`.
- Step 3: Sort and prepend "John". Output correctly merges all 3 accounts.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **When to choose Approach 1 (Integer DSU over Account Indices):** The gold standard for enterprise production. Restricts DSU memory to a flat array of size $N \le 1000$, avoiding thousands of string allocations.
- **When to choose Approach 2 (Email Graph DFS):** Build an explicit adjacency list where vertices are email strings. Explore components using DFS. Conceptually standard, but creates heavy graph node allocation.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Setup & Boundaries:** Initialize `DisjointSet(n)` and `emailToAccount = new Dictionary<string, int>()`.
2. **Main Exploration Loop:** Iterate over accounts and emails to perform unions.
3. **Invariant Maintenance & Condition Gates:** Group emails by `dsu.Find(i)` into `HashSet<string>` buckets.
4. **Resolution & Return:** Sort each email set with `StringComparer.Ordinal` and build final list with name prepended.

#### 4.3 Alternative Approaches Analysis
- **Direct String DSU:** `Dictionary<string, string> parent`. While it avoids account index tracking, it creates a large dictionary footprint and frequent string equality checks on every `Find` call.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: Integer DSU over Accounts | Approach 2: Email Graph DFS | Approach 3: String-based DSU |
| :--- | :--- | :--- | :--- |
| **Time Complexity** | $O(N \cdot K \log(N \cdot K))$ | $O(N \cdot K \log(N \cdot K))$ | $O(N \cdot K \log(N \cdot K))$ |
| **DSU Array Size** | Strictly $N \le 1000$ ints | N/A (graph lists) | Up to $10,000$ string keys |
| **String Allocations** | Zero inside DSU | High (adjacency lists) | High (string parent map) |
| **GC Pressure** | Minimal | Significant | High |
| **Sorting Cost** | $O(M \log M)$ on final email sets | $O(M \log M)$ | $O(M \log M)$ |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Accounts Merge (#104)
// Selection Heuristic: 
//   - Approach 1 (Integer DSU): High-performance enterprise pattern; DSU operates on account indices.
//   - Approach 2 (Email Graph DFS): Traditional graph traversal over email adjacency lists.
// Invariant: Mapping email -> firstAccountIndex allows O(1) integer unions, bypassing string mutations.
// ============================================================================
```

#### Approach 1: Integer DSU over Account Indices (Optimal Architecture)
```csharp
public class Solution
{
    public IList<IList<string>> AccountsMerge(IList<IList<string>> accounts)
    {
        int n = accounts.Count;
        var dsu = new DisjointSet(n);
        // Map email string to the earliest account index that registered it
        var emailToFirstAccount = new Dictionary<string, int>();

        // Stage 1: Union account indices that share at least one email address
        for (int i = 0; i < n; i++)
        {
            var acc = accounts[i];
            // Skip index 0 which holds the user's name
            for (int j = 1; j < acc.Count; j++)
            {
                string email = acc[j];
                // Decision Gate: If email was already seen, union current account with previous account
                if (emailToFirstAccount.TryGetValue(email, out int prevAccIndex))
                {
                    dsu.Union(i, prevAccIndex);
                }
                else
                {
                    emailToFirstAccount[email] = i;
                }
            }
        }

        // Stage 2: Aggregate all emails into buckets keyed by the component root account index
        var rootToEmails = new Dictionary<int, HashSet<string>>();
        for (int i = 0; i < n; i++)
        {
            int root = dsu.Find(i);
            if (!rootToEmails.TryGetValue(root, out var emailSet))
            {
                emailSet = new HashSet<string>();
                rootToEmails[root] = emailSet;
            }

            var acc = accounts[i];
            for (int j = 1; j < acc.Count; j++)
            {
                emailSet.Add(acc[j]);
            }
        }

        // Stage 3: Format output with account name followed by lexicographically sorted emails
        var result = new List<IList<string>>(rootToEmails.Count);
        foreach (var kvp in rootToEmails)
        {
            int rootIndex = kvp.Key;
            var sortedEmails = kvp.Value.ToList();
            // Invariant: Problem requires emails sorted in ASCII / ordinal order
            sortedEmails.Sort(StringComparer.Ordinal);

            var mergedAccount = new List<string>(sortedEmails.Count + 1)
            {
                accounts[rootIndex][0] // Account holder name from canonical root account
            };
            mergedAccount.AddRange(sortedEmails);
            result.Add(mergedAccount);
        }

        return result;
    }

    private sealed class DisjointSet
    {
        private readonly int[] _parent;

        public DisjointSet(int n)
        {
            _parent = new int[n];
            for (int i = 0; i < n; i++) _parent[i] = i;
        }

        public int Find(int x)
        {
            if (_parent[x] != x)
            {
                _parent[x] = Find(_parent[x]); // Path compression
            }
            return _parent[x];
        }

        public void Union(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);
            if (rootX != rootY)
            {
                _parent[rootX] = rootY;
            }
        }
    }
}
```

#### Approach 2: Email Graph DFS Component Walk
```csharp
public class SolutionDfs
{
    public IList<IList<string>> AccountsMerge(IList<IList<string>> accounts)
    {
        var adj = new Dictionary<string, List<string>>();
        var emailToName = new Dictionary<string, string>();

        // Build undirected graph of emails
        foreach (var acc in accounts)
        {
            string name = acc[0];
            string firstEmail = acc[1];

            for (int i = 1; i < acc.Count; i++)
            {
                string email = acc[i];
                emailToName[email] = name;

                if (!adj.ContainsKey(email)) adj[email] = new List<string>();
                if (!adj.ContainsKey(firstEmail)) adj[firstEmail] = new List<string>();

                adj[firstEmail].Add(email);
                adj[email].Add(firstEmail);
            }
        }

        var visited = new HashSet<string>();
        var result = new List<IList<string>>();

        foreach (string email in adj.Keys)
        {
            if (!visited.Contains(email))
            {
                var componentEmails = new List<string>();
                Dfs(email, adj, visited, componentEmails);
                componentEmails.Sort(StringComparer.Ordinal);

                var merged = new List<string>(componentEmails.Count + 1)
                {
                    emailToName[email]
                };
                merged.AddRange(componentEmails);
                result.Add(merged);
            }
        }

        return result;
    }

    private static void Dfs(string email, Dictionary<string, List<string>> adj, HashSet<string> visited, List<string> component)
    {
        visited.Add(email);
        component.Add(email);

        foreach (string neighbor in adj[email])
        {
            if (!visited.Contains(neighbor))
            {
                Dfs(neighbor, adj, visited, component);
            }
        }
    }
}
```

---

## 105. Graph Valid Tree (LeetCode #261)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#union-find` `#tree-definition` `#cycle-detection` `#connected-components` |
| **LeetCode Link** | [Graph Valid Tree](https://leetcode.com/problems/graph-valid-tree/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** You have a graph of $n$ nodes labeled from $0$ to $n - 1$. You are given an integer $n$ and a list of `edges` where $edges[i] = [a_i, b_i]$. Check whether these edges make up a valid tree.
- **Key Constraints:**
  - $1 \le n \le 2000$.
  - $0 \le \text{edges.Length} \le 5000$.
  - All edges are undirected and unique ($a_i \ne b_i$).
- **Senior Edge Cases to Defend:**
  - Fast Pruning Guard: If $\text{edges.Length} \ne n - 1$, it is mathematically impossible to be a tree $\implies$ return `false` in $O(1)$ immediately!
  - Disconnected Components with Cycle: $n = 5$, edges = $[[0, 1], [1, 2], [2, 0], [3, 4]]$. Has 4 edges ($n - 1$), but forms a cycle and 2 components. Must reject.
  - Degenerate single-node tree: $n = 1, \text{edges} = [] \implies 0 == 1 - 1 \implies \text{true}$.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Dual Invariant Tree Verification. A graph of $n$ vertices is a tree if and only if: (1) it contains exactly $n - 1$ edges, and (2) it contains no cycles. Disjoint Set Union validates acyclicity in $O(N \cdot \alpha(N))$ time.
- **Sample 1:**
  - **Input:** `n = 5, edges = [[0, 1], [0, 2], [0, 3], [1, 4]]` $\implies$ `true`
- **Sample 2:**
  - **Input:** `n = 5, edges = [[0, 1], [1, 2], [2, 3], [1, 3], [1, 4]]` $\implies$ `false` (Cycle between 1, 2, 3).

---

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)

#### 3.1 The Intuitive Spark & Conceptual Metaphor
Imagine designing a structural truss for a roof. An architect knows that a minimally rigid, non-redundant structure over $n$ anchor joints requires exactly $n - 1$ struts. If you have fewer than $n - 1$ struts, parts of the roof will float freely without support (disconnected graph). If you have more than $n - 1$ struts, some struts form closed structural triangles or rings that carry redundant stress (cycles). By first counting the struts ($E == n - 1$), you immediately filter out non-trees. Then, as you assemble the struts using a Disjoint Set Union, if any strut attempts to connect two joints that already form a rigid assembly, a cycle exists, and the roof is disqualified.

#### 3.2 The Naive Bottleneck & Redundant Computation
A naive approach might perform a full cycle-detection DFS followed by a second full BFS to count reachable nodes and verify connectivity, ignoring the mathematical constraint on edge count. Evaluating connectivity on graphs with $E \ne n - 1$ is completely wasted work. Checking $E == n - 1$ takes $O(1)$ time and eliminates over $90\%$ of invalid graph instances instantly.

#### 3.3 The Breakthrough Insight & Mathematical Invariant
- **The Tree Theorem:** For any undirected graph $G = (V, E)$ with $V$ vertices, any two of the following conditions strictly guarantee that $G$ is a valid tree:
  1. $|E| = |V| - 1$.
  2. $G$ is connected (exactly 1 connected component).
  3. $G$ is acyclic (contains 0 cycles).
- **Senior Optimization Pivot:**
  - Gate 1: Check $|E| == n - 1$. If false, return `false` in $O(1)$.
  - Gate 2: Since $|E| == n - 1$ is guaranteed, checking that $G$ contains **no cycles** via DSU simultaneously guarantees that $G$ is connected! Adding $n - 1$ edges that never create a cycle must reduce the component count from $n$ to exactly $n - (n - 1) = 1$.

#### 3.4 Cursor Semantics & Invariant Partition Architecture

```text
Tree Validation Decision Pipeline:
Step 1: Invariant Check 1 (Edge Count)
  edges.Length != n - 1  ==> Return false in O(1)

Step 2: Invariant Check 2 (Acyclicity via DSU)
  For each edge [u, v]:
    rootU = Find(u)
    rootV = Find(v)
    - If rootU == rootV ==> Cycle detected ==> Return false
    - If rootU != rootV ==> Union(rootU, rootV)

Step 3: All n - 1 edges acyclic ==> Graph is a Valid Tree! Return true.
```

#### 3.5 State Transition Triggers & Decision Gates
- **Initial Guard:** If `edges.Length != n - 1`, return `false`.
- **DSU Edge Processing:** For each `[u, v]` in `edges`:
  - `rootU = Find(u)`
  - `rootV = Find(v)`
  - **Decision Gate:** If `rootU == rootV`, return `false`.
  - Otherwise: `parent[rootU] = rootV`.
- If all edges are processed without a cycle, return `true`.

#### 3.6 Concrete Step-by-Step State Trace
Input: `n = 5`, `edges = [[0, 1], [0, 2], [0, 3], [1, 4]]`.
- Edge count check: $edges.Length == 4 == 5 - 1 \implies$ Pass.
- `parent = [0, 1, 2, 3, 4]`.
- Edge `[0, 1]`: `Find(0) = 0 != Find(1) = 1 \implies parent[0] = 1`.
- Edge `[0, 2]`: `Find(0) = 1 != Find(2) = 2 \implies parent[1] = 2`.
- Edge `[0, 3]`: `Find(0) = 2 != Find(3) = 3 \implies parent[2] = 3`.
- Edge `[1, 4]`: `Find(1) = 3 != Find(4) = 4 \implies parent[3] = 4`.
- All 4 edges processed with zero cycles. Return `true`.

---

### 4. Approach & Complexity Deconstruction

#### 4.1 Anchor Points & Approach Selection Criteria
- **When to choose Approach 1 (DSU + Early Edge Count Guard):** The undisputed optimal solution. $O(1)$ early exit on edge count, near-linear $O(N \cdot \alpha(N))$ cycle detection, and minimal lines of code.
- **When to choose Approach 2 (Adjacency List BFS/DFS):** When graph structures are already materialized as adjacency lists. Checks visited count to ensure all $n$ nodes were reached from node 0.

#### 4.2 Step-by-Step Natural Progression Flow
1. **Setup & Boundaries:** Guard `edges.Length != n - 1`. Allocate `parent` array.
2. **Main Exploration Loop:** Iterate through edges.
3. **Invariant Maintenance & Condition Gates:** Return `false` if `Find(u) == Find(v)`.
4. **Resolution & Return:** Return `true` after all edges pass.

#### 4.3 Alternative Approaches Analysis
- **DFS Visited Set:** Build adjacency list. Start DFS from 0, tracking `visited`. If DFS encounters an already-visited neighbor that is not the parent, a cycle exists. At end, verify `visited.Count == n`. Complexity: $O(V + E)$ time and space.

#### 4.4 Multi-Dimensional Complexity & Trade-Off Matrix

| Metric | Approach 1: DSU + Edge Count Check | Approach 2: DFS Visited + Cycle Check |
| :--- | :--- | :--- |
| **Time Complexity** | $O(N \cdot \alpha(N)) \approx O(N)$ | $O(N)$ |
| **Auxiliary Space** | $O(N)$ flat array | $O(N)$ graph list + stack + set |
| **Early Rejection** | $O(1)$ when $E \ne n - 1$ | $O(1)$ when $E \ne n - 1$ |
| **Graph Construction** | Zero (streams edges directly) | Required (allocates adjacency lists) |
| **Cache Locality** | High | Low |

---

### 5. Production C# Implementations

```csharp
// ============================================================================
// HEADER ANCHOR BLOCK: Graph Valid Tree (#105)
// Selection Heuristic: 
//   - Approach 1 (DSU + Guard): The definitive optimal standard; O(1) early rejection + O(N * alpha(N)) cycle check.
// Invariant: If |E| == n - 1 and all edges are acyclic, the graph is mathematically guaranteed to be a single tree.
// ============================================================================
```

#### Approach 1: DSU with Early Edge Count Guard (Optimal Standard)
```csharp
public class Solution
{
    public bool ValidTree(int n, int[][] edges)
    {
        // Invariant Guard 1: A valid tree of n nodes MUST have exactly n - 1 edges.
        // If edges.Length != n - 1, it either has cycles or is disconnected.
        if (edges.Length != n - 1)
        {
            return false;
        }

        int[] parent = new int[n];
        for (int i = 0; i < n; i++) parent[i] = i;

        int Find(int x)
        {
            if (parent[x] != x)
            {
                parent[x] = Find(parent[x]); // Path compression
            }
            return parent[x];
        }

        // Invariant Guard 2: Given |E| == n - 1, the absence of cycles strictly guarantees a single connected tree
        foreach (var edge in edges)
        {
            int rootU = Find(edge[0]);
            int rootV = Find(edge[1]);

            // Cycle Decision Gate: If endpoints already share a root, adding this edge creates a cycle
            if (rootU == rootV)
            {
                return false;
            }

            // Union operation
            parent[rootU] = rootV;
        }

        return true;
    }
}
```

#### Approach 2: BFS Connectivity & Cycle Traversal
```csharp
public class SolutionBfs
{
    public bool ValidTree(int n, int[][] edges)
    {
        // Invariant Guard: Fast check on edge count
        if (edges.Length != n - 1) return false;

        var adj = new List<int>[n];
        for (int i = 0; i < n; i++) adj[i] = new List<int>();

        foreach (var edge in edges)
        {
            adj[edge[0]].Add(edge[1]);
            adj[edge[1]].Add(edge[0]);
        }

        var visited = new HashSet<int>();
        var queue = new Queue<int>();

        queue.Enqueue(0);
        visited.Add(0);

        while (queue.Count > 0)
        {
            int curr = queue.Dequeue();

            foreach (int neighbor in adj[curr])
            {
                // In BFS on an undirected graph with E == n - 1,
                // simply enqueuing unvisited neighbors is sufficient.
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }

        // Invariant: If all n nodes are reached from node 0, the graph is connected (hence a valid tree)
        return visited.Count == n;
    }
}
```

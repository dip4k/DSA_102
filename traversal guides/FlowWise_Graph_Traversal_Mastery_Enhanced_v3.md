# 🕸️ Flow-Wise Graph Traversal Mastery (Enhanced v3)
**Goal:** Professional-grade intuition for graph traversal and search (Level 1 → Level 5).  
**Upgrade in v3:** Deep **BFS vs DFS** diagrams + an upgraded **Level 1: Physical Layer** modeled after your “array playbook” learning standard (Why/What/How/Where/When; step/flow; visuals; Python/C#; pitfalls; tips).  

---

# 🧭 One-page Level Mapping Index (Graphs)
Use this to instantly map a graph problem to the right **level**, **pattern**, and **expected invariant**.

## ✅ Legend
- 🧾 **Invariant** = what stays true after each step.
- 🧠 **Visited** = prevents cycles and repeated work.
- 🧺 **Queue** = BFS frontier (wave).
- 🎒 **Stack** = DFS frontier (depth-first).

| Level | Pattern / skill | Typical LeetCode problems (examples) | Expected invariant (one-liner) |
|---|---|---|---|
| L1 | Graph representation + visited discipline | 1971 Find if Path Exists, 841 Keys and Rooms | “Every node is marked visited at most once; visited set matches discovered region.” |
| L1 | DFS recursion skeleton | 200 Number of Islands, 695 Max Area of Island | “dfs(u) fully explores u’s connected region without revisiting nodes.” |
| L1 | BFS skeleton | 733 Flood Fill, 994 Rotting Oranges | “Queue contains exactly the frontier; every enqueued node is newly discovered.” |
| L2 | Iterative DFS (stack) | 133 Clone Graph, 547 Number of Provinces | “Stack holds pending nodes; visited prevents repeats; pop order doesn’t break reachability.” |
| L2 | Connected components | 547 Provinces | “Each new traversal start discovers exactly one component.” |
| L3 | Shortest path unweighted (BFS + parent) | 752 Open the Lock, 127 Word Ladder | “First discovery gives shortest distance; parent reconstructs one shortest path.” |
| L3 | Multi-source BFS | 542 01 Matrix, 994 Rotting Oranges | “Queue starts with all sources at dist=0; distances expand outward in layers.” |
| L3 | Bipartite check (coloring) | 785 Is Graph Bipartite, 886 Possible Bipartition | “Every edge connects opposite colors; conflict => not bipartite.” |
| L4 | Topological sort (DAG) | 207, 210 Course Schedule | “For edge u->v, u appears before v; if not possible => cycle.” |
| L4 | Directed cycle detection (colors) | 207 Course Schedule, 802 Safe States | “Back-edge to a ‘visiting’ node implies a cycle.” |
| L4 | 0-1 BFS (deque frontier) | 1368, 2290 | “Deque pops smallest current cost first (0-front, 1-back).” |
| L5 | State-space graphs (implicit nodes) | 127, 1091 | “State is node; neighbors generated on the fly; visited prevents exponential blow-up.” |

---

# 🟢 Level 1: The Physical Layer (Basic Movement)
**Goal:** Muscle memory for moving through graph memory safely (neighbors/edges) with minimal bugs.

> Your job at Level 1 is NOT “knowing 20 graph algorithms.”  
> Your job is to become dangerous with 4 primitives:
> 1) Iterate neighbors correctly, 2) control boundaries, 3) mark visited at the right time, 4) keep frontier data structures correct.

---

## 1A) 🧱 Mastering the loop (Forward, Backward, Strides) — *for graphs*
You don’t “index” graphs like arrays, but you still do controlled iteration over:
- Node IDs (0..n-1)
- Adjacency lists (neighbors of u)
- Edge lists (u,v,w)
- Grid neighbors (up/down/left/right)

### 1A.1 ➡️ Forward scan over nodes (0..n-1)
**Why:** Many tasks need a “start traversal from every unvisited node” outer loop (components, bipartite across components, topo initialization).  

**What:** A simple for-loop over all nodes that conditionally launches BFS/DFS.  

**How (step/flow):**
1) for u in 0..n-1  
2) if not visited[u]: start a traversal from u  
3) that traversal marks exactly one component  

**Where:** Counting components, “is graph connected?”, bipartite check in disconnected graph, topo indegree initialization.  

**When:** Whenever input does NOT guarantee connectivity (which is most real problems).

**Visual**
```text
Component A: 0 -- 1
Component B: 2 -- 3

for u=0 -> start traversal => visits {0,1}
for u=2 -> start traversal => visits {2,3}
```

**Python**
```python
components = 0
for u in range(n):
    if not visited[u]:
        components += 1
        bfs(u)  # or dfs(u)
```

**C#**
```csharp
int components = 0;
for (int u = 0; u < n; u++)
{
    if (!visited[u])
    {
        components++;
        Bfs(u); // or Dfs(u)
    }
}
```

**Common pitfalls**
- ❌ Only starting from node 0 and assuming “visited all” means connected.
- ❌ Forgetting to reset visited/dist between test cases.

**Tips & tricks**
- 💡 Say it out loud: “The outer loop handles disconnected graphs.”

---

### 1A.2 ⬅️ Backward scan (when you *write* or *rollback* metadata)
**Why:** Some graph problems maintain arrays like `order[]`, `topo[]`, or “finish order” where you may reverse a result or process from back to front.  

**What:** Backward iteration over a list/array produced by traversal (not usually the graph itself).  

**How (step/flow):**
1) Run DFS to build `finish_order` (append when node finishes)  
2) Reverse it (or traverse it backwards) to get topo-like ordering (DAG)  

**Where:** DFS postorder-based topo patterns, path reconstruction lists (reverse path), SCC algorithms (finish-time stack concept).  

**When:** When meaning is “built backwards” (parents from target to source; finishes from leaves upward).

**Visual**
```text
Parent chain produced by BFS:
T <- B <- A <- S

Reconstruct:
walk: T, B, A, S
reverse -> S, A, B, T
```

**Python**
```python
path = []
cur = target
while cur != -1:
    path.append(cur)
    cur = parent[cur]
path.reverse()
```

**C#**
```csharp
var path = new List<int>();
int cur = target;
while (cur != -1)
{
    path.Add(cur);
    cur = parent[cur];
}
path.Reverse();
```

**Common pitfalls**
- ❌ Forgetting to reverse and then “path” looks correct but is backwards.
- ❌ Reversing huge lists repeatedly inside loops (do it once).

**Tips & tricks**
- 💡 If you used `parent[]`, your first path will be “backwards.” That’s normal.

---

### 1A.3 🪜 Stride iteration (k-step neighbor patterns / layered processing)
**Why:** “Stride” in graphs often means one of these:
- Process BFS by *levels* (one wave at a time)
- Process a list of nodes in chunks (batching)
- Work with grid moves that are a fixed set (4 or 8 directions)

**What:** Controlled stepping through a frontier with a fixed “batch size” (BFS level size) or fixed move set.

**How (step/flow) — BFS Level Stride:**
1) `level_size = len(queue)`  
2) repeat `level_size` times: pop, expand neighbors  
3) after the loop: you completed exactly one BFS level

**Where:** “minimum steps,” “time to spread,” “all nodes at distance k.”  

**When:** When the question talks about *minutes/steps/levels*.

**Visual**
```text
Queue at start of a minute:
[ nodes of distance d ]

Process exactly these nodes, append newly discovered:
[ nodes of distance d+1 ]

That’s one stride.
```

**Python**
```python
from collections import deque

q = deque([start])
visited[start] = True
steps = 0

while q:
    level_size = len(q)
    for _ in range(level_size):
        u = q.popleft()
        for v in adj[u]:
            if not visited[v]:
                visited[v] = True
                q.append(v)
    steps += 1
```

**C#**
```csharp
var q = new Queue<int>();
q.Enqueue(start);
visited[start] = true;
int steps = 0;

while (q.Count > 0)
{
    int levelSize = q.Count;
    for (int i = 0; i < levelSize; i++)
    {
        int u = q.Dequeue();
        foreach (int v in adj[u])
        {
            if (!visited[v])
            {
                visited[v] = true;
                q.Enqueue(v);
            }
        }
    }
    steps++;
}
```

**Common pitfalls**
- ❌ Using `for i in range(len(q))` while q is changing (level size must be frozen).
- ❌ Incrementing `steps` at wrong moment (off-by-one minutes).

**Tips & tricks**
- 💡 Freeze `level_size` first. That single habit prevents many BFS bugs.

---

## 1B) 📏 Boundary control (Inclusive vs Exclusive) — *graph edition*
Graph boundaries aren’t only about indices; they’re about:
- node-id ranges
- grid bounds
- adjacency correctness
- directed/undirected edge insertion rules

### 1B.1 🧮 Node ID boundaries (0..n-1) and half-open thinking
**Why:** Most runtime errors are boundary errors: invalid node id, wrong `n`, wrong allocation sizes.  

**What:** Treat node iteration as half-open: [0, n).  

**How (step/flow):**
1) Allocate arrays with length n  
2) Loop `for u in range(n)`  
3) Only accept edges (u,v) where 0 <= u,v < n

**Where:** Building adjacency list, visited/dist arrays, indegree arrays.  

**When:** Always—before you even think about BFS/DFS correctness.

**Common pitfalls**
- ❌ Building `adj = [[]]*n` in Python (aliasing bug).
- ❌ Using 1-based node labels from input but allocating 0-based arrays without converting.

**Tip**
- 💡 If input is 1..n, normalize once: `u -= 1; v -= 1`.

---

### 1B.2 🧱 Grid boundaries (the “in bounds” gate)
**Why:** Grid BFS/DFS is graph traversal + boundary checking; most bugs are “fell off grid” or “revisited cell.”  

**What:** A single `in_bounds(r,c)` check that guards every neighbor.  

**How (step/flow):**
1) Generate candidate neighbor cell  
2) Check bounds  
3) Check visited/block  
4) Enqueue/recurse

**Where:** Shortest path in matrix, islands, flood fill.  

**When:** Whenever graph is implicit (grid/state graph).

**Visual**
```text
(r,c) -> try moves:
(r-1,c) (r+1,c) (r,c-1) (r,c+1)

Each move passes through:
Bounds gate -> Block gate -> Visited gate
```

**Python**
```python
def in_bounds(r, c):
    return 0 <= r < R and 0 <= c < C
```

**C#**
```csharp
static bool InBounds(int r, int c, int R, int C)
{
    return 0 <= r && r < R && 0 <= c && c < C;
}
```

**Common pitfalls**
- ❌ Checking visited AFTER enqueue (duplicates explode).
- ❌ Forgetting that `grid[r][c]` might be char/int and comparing against wrong type.

**Tips**
- 💡 Gate order: bounds -> blocked -> visited -> then enqueue.

---

## 1C) 🧩 Edge case handling (Empty, Single, Weird Graphs)
### 1C.1 Empty graph / no edges
**Why:** Many problems allow n>0 but edges=0, producing isolated vertices.  

**What:** Graph may have nodes but no connectivity.  

**How:** Components loop (Level 1A.1) should count n components if no edges.  

**Where:** Connectivity checks, provinces, “make network connected.”  

**When:** If edges are sparse or constraints permit 0 edges.

**Pitfalls**
- ❌ Assuming BFS from 0 reaches all nodes.

---

### 1C.2 Single node, self-loop, parallel edges
**Why:** Self-loops and duplicates can confuse cycle checks or cause repeated neighbor processing.  

**What:** `u -> u` exists; or multiple identical edges `u-v`.  

**How:** Visited discipline should still prevent infinite traversal. For cycle detection, be explicit: “Is a self-loop a cycle?” (often yes in directed graphs).  

**Where:** Course prerequisites, directed-cycle detection, redundant connections.  

**When:** When inputs are not guaranteed simple graphs.

**Tips**
- 💡 For traversal correctness, visited solves it.
- 💡 For cycle problems, define what counts as a cycle per problem statement.

---

## 1D) 🤝 Simultaneous iteration (Lockstep) — frontiers + metadata arrays
This is your “two pointers / lockstep” concept, but for graphs it becomes:
- Frontier structure (queue/stack) moving forward
- Metadata arrays (visited/dist/parent/color) updating in sync

### 1D.1 BFS: queue + dist + parent in lockstep
**Why:** Shortest paths need both distance and path reconstruction.  

**What:** When you discover v from u, you update:
- visited[v] = True
- dist[v] = dist[u] + 1
- parent[v] = u
- enqueue(v)

**How (step/flow):**
1) Initialize src: dist=0, parent=-1  
2) On neighbor discovery: set dist/parent once  
3) Never overwrite them (first discovery is best in unweighted BFS)

**Where:** Word ladder, shortest path in unweighted graphs, “minimum moves.”  

**When:** When the output wants steps OR the actual path.

**Visual**
```text
Queue wave:
[ u ]  -> discovers v

Metadata lockstep:
dist[v]   = dist[u] + 1
parent[v] = u
```

---

# 🆚 BFS vs DFS (Diagrams + Decision Rules)
This is the upgrade you asked for: “tree playbook style” but for graphs.

---

## 2.1 🧠 The one-line difference (memorize)
- 🌊 **BFS**: queue-driven *wave*; best for **shortest steps** in unweighted graphs.
- 🕳️ **DFS**: stack/recursion-driven *dive*; best for **explore/structure**.

---

## 2.2 🎭 Same graph, different behavior (full micro-trace)

### Graph (fixed neighbor order)
```text
0 -- 1 -- 3
 \ 
  2

Adj order:
0: [ppl-ai-file-upload.s3.amazonaws](https://ppl-ai-file-upload.s3.amazonaws.com/web/direct-files/attachments/83049665/295a245b-4f5a-4e5b-b129-fabe583e1919/SYSTEM_PROMPT_v12_EXTENDED_SUPPORT_CSHARP.md)
1: [ppl-ai-file-upload.s3.amazonaws](https://ppl-ai-file-upload.s3.amazonaws.com/web/direct-files/attachments/83049665/9eb047cc-c20e-43fe-8d07-88d5746bddb6/VISUAL_PLAYBOOK_GENERATION_PROMPT_v12_UPDATED.md)
2: 
3: [ppl-ai-file-upload.s3.amazonaws](https://ppl-ai-file-upload.s3.amazonaws.com/web/direct-files/attachments/83049665/295a245b-4f5a-4e5b-b129-fabe583e1919/SYSTEM_PROMPT_v12_EXTENDED_SUPPORT_CSHARP.md)
```

### 🌊 BFS micro-trace (Queue = frontier)
**Why:** BFS expands by layers, which corresponds to distance in unweighted graphs.  
**What:** FIFO queue.  
**How (step/flow):**
1) Enqueue start, mark visited immediately
2) Pop front, push undiscovered neighbors
3) Repeat until queue empty

**Visual**
```text
Start:
queue: 
visited: {0}
dist=0

Pop 0:
discover 1,2
queue: [ppl-ai-file-upload.s3.amazonaws](https://ppl-ai-file-upload.s3.amazonaws.com/web/direct-files/attachments/83049665/99a4b859-7120-4924-84dd-f374e18f9ee2/CONSOLIDATED_CSHARP_EXTENDED_SUPPORT_MASTER_PROMPT_v13.md)
visited: {0,1,2}
dist=1, dist=1 [ppl-ai-file-upload.s3.amazonaws](https://ppl-ai-file-upload.s3.amazonaws.com/web/direct-files/attachments/83049665/99a4b859-7120-4924-84dd-f374e18f9ee2/CONSOLIDATED_CSHARP_EXTENDED_SUPPORT_MASTER_PROMPT_v13.md)
parent=0, parent=0 [ppl-ai-file-upload.s3.amazonaws](https://ppl-ai-file-upload.s3.amazonaws.com/web/direct-files/attachments/83049665/295a245b-4f5a-4e5b-b129-fabe583e1919/SYSTEM_PROMPT_v12_EXTENDED_SUPPORT_CSHARP.md)

Pop 1:
discover 3
queue: [ppl-ai-file-upload.s3.amazonaws](https://ppl-ai-file-upload.s3.amazonaws.com/web/direct-files/attachments/83049665/9eb047cc-c20e-43fe-8d07-88d5746bddb6/VISUAL_PLAYBOOK_GENERATION_PROMPT_v12_UPDATED.md)
visited: {0,1,2,3}
dist=2 [ppl-ai-file-upload.s3.amazonaws](https://ppl-ai-file-upload.s3.amazonaws.com/web/direct-files/attachments/83049665/9eb047cc-c20e-43fe-8d07-88d5746bddb6/VISUAL_PLAYBOOK_GENERATION_PROMPT_v12_UPDATED.md)
parent=1 [ppl-ai-file-upload.s3.amazonaws](https://ppl-ai-file-upload.s3.amazonaws.com/web/direct-files/attachments/83049665/9eb047cc-c20e-43fe-8d07-88d5746bddb6/VISUAL_PLAYBOOK_GENERATION_PROMPT_v12_UPDATED.md)

Pop 2: no new
queue: [ppl-ai-file-upload.s3.amazonaws](https://ppl-ai-file-upload.s3.amazonaws.com/web/direct-files/attachments/83049665/9eb047cc-c20e-43fe-8d07-88d5746bddb6/VISUAL_PLAYBOOK_GENERATION_PROMPT_v12_UPDATED.md)

Pop 3: done
queue: []
```

**BFS property you should say out loud**
```text
When a node is first discovered, its dist is minimal in unweighted graphs.
```

---

### 🕳️ DFS micro-trace (Stack = depth path + pending branches)
**Why:** DFS gives a clean “explore region fully” structure.  
**What:** Recursion or explicit stack.  
**How (step/flow):**
1) Visit u
2) For each neighbor v: if unvisited, DFS(v)
3) Backtrack after finishing all neighbors

**Visual**
```text
dfs(0)
  dfs(1)
    dfs(3)
    backtrack to 1
  backtrack to 0
  dfs(2)
  backtrack to 0
```

**One valid DFS order (depends on neighbor order):**
```text
0, 1, 3, 2
```

---

## 2.3 🧺 Queue vs 🎒 Stack (frontier picture)
```text
BFS frontier (queue):      DFS frontier (stack):
front -> [a b c] -> back   bottom -> [a b c] -> top

BFS expands "breadth":     DFS expands "depth":
process a, then b, then c  process c first (last pushed)
```

**Decision rule**
- If you care about “nearest / minimum steps” → queue wave (BFS).
- If you care about “explore everything / detect structure” → stack dive (DFS).

---

## 2.4 🧯 Typical BFS pitfalls vs DFS pitfalls

### BFS pitfalls
**Why they happen:** queue order hides duplicates unless visited is done right.  
- ❌ Mark visited on DEQUEUE → duplicates get enqueued many times.
- ❌ Forgetting level-size freeze → wrong “minutes/steps” simulation.

**BFS tip**
- ✅ Mark visited at ENQUEUE time.

### DFS pitfalls
**Why they happen:** recursion can blow the call stack, and “color states” must be consistent.  
- ❌ Recursion depth overflow on large graphs
- ❌ Mark visited after recursion call (cycle disaster)
- ❌ In directed cycle detection: forgetting “visiting” (gray) state

**DFS tip**
- ✅ Use iterative DFS stack when depth can be huge.

---

## 2.5 🧠 When/Where (problem signals)

### Choose 🌊 BFS when you see
**Why:** distance layers match “fewest edges.”  
**Signals**
- “Minimum number of moves/steps”
- “Shortest path in an unweighted graph”
- “Spreads each minute” / “nearest source”
- “Distance K”

**Where (detailed examples)**
- Rotting Oranges: each BFS layer = 1 minute.
- 01 Matrix: multi-source BFS from all zeros gives nearest-zero distance.

---

### Choose 🕳️ DFS when you see
**Why:** DFS naturally solves “region/structure” questions.  
**Signals**
- “Count islands / components”
- “Is there a path?” (reachability)
- “Detect cycle (directed)” via recursion-stack / colors
- “Topological order” via finishing times (or use Kahn BFS)

**Where (detailed examples)**
- Number of Islands: DFS paints one island completely; outer loop counts.
- Course Schedule: DFS with colors detects cycle in prerequisites graph.

---

# ✅ NEW: Upgraded Level 1 Teaching (BFS + DFS) with your standard
This section replaces “quick templates” with a professional-grade teaching format.

---

## 1.1 🕳️ DFS (recursive) — explore a connected region

**Why:** DFS is the simplest mental model for “paint-fill” exploration and component discovery.  

**What:** Visit u, then recursively visit all unvisited neighbors.  

**How:**  
Step/flow:
1) Enter dfs(u)  
2) Mark visited[u] = true  
3) For each neighbor v:
   - If v not visited, dfs(v)  
4) Return (backtrack)

**Where:** Connected components, island counting, reachability, cycle detection with extra bookkeeping.  

**When:** When you need “fully explore a region,” and recursion depth is safe (or you’ll switch to iterative DFS).

**Visual**
```text
0--1--3
 \ 
  2

dfs(0):
visit 0
  visit 1
    visit 3
  back to 0
  visit 2
```

**Python**
```python
def dfs(u):
    visited[u] = True
    for v in adj[u]:
        if not visited[v]:
            dfs(v)
```

**C#**
```csharp
void Dfs(int u)
{
    visited[u] = true;
    foreach (int v in adj[u])
        if (!visited[v]) Dfs(v);
}
```

**Common pitfalls**
- ❌ Forgetting visited -> infinite recursion on cycles.
- ❌ Recursion depth overflow on large graphs.

**Tips & tricks**
- 💡 Invariant: “When dfs(u) returns, everything reachable from u is visited.”

**Where this shows up (more detailed)**
- “Number of Islands”: each land cell triggers DFS paint; recursion explores 4 neighbors; marking visited prevents revisiting.
- “Keys and Rooms”: DFS from room 0 collects all reachable rooms; visited prevents loops from key cycles.

---

## 1.2 🌊 BFS (queue) — explore in waves

**Why:** BFS naturally processes nodes by increasing distance in unweighted graphs, which maps to “minimum steps.”  

**What:** Use a queue; pop front; push undiscovered neighbors; mark visited at enqueue time.  

**How:**  
Step/flow:
1) q = [start]  
2) visited[start] = true  
3) While q not empty:
4) u = pop front  
5) For v in adj[u]:
   - If not visited[v]:
     - visited[v]=true
     - q.push_back(v)

**Where:** shortest unweighted path, minimum moves, time-to-spread problems, level order.  

**When:** When distance or “layers/minutes” matters.

**Visual (frontier growth)**
```text
Level 0: {S}
Level 1: neighbors of S
Level 2: neighbors of Level 1 not seen
Queue enforces this order.
```

**Python**
```python
from collections import deque

def bfs(start):
    q = deque([start])
    visited[start] = True
    while q:
        u = q.popleft()
        for v in adj[u]:
            if not visited[v]:
                visited[v] = True
                q.append(v)
```

**C#**
```csharp
void Bfs(int start)
{
    var q = new Queue<int>();
    q.Enqueue(start);
    visited[start] = true;

    while (q.Count > 0)
    {
        int u = q.Dequeue();
        foreach (int v in adj[u])
        {
            if (!visited[v])
            {
                visited[v] = true;
                q.Enqueue(v);
            }
        }
    }
}
```

**Common pitfalls**
- ❌ Marking visited on dequeue instead of enqueue (duplicate work).
- ❌ Trying to compute “minutes/levels” without freezing `levelSize`.

**Tips & tricks**
- 💡 Invariant: “Queue contains nodes discovered but not processed.”

**Where this shows up (more detailed)**
- “Rotting Oranges”: enqueue all rotten oranges initially (multi-source), each BFS layer equals one minute of spread.
- “Shortest Path in Binary Matrix”: BFS from start cell, each move adds 1 step; first time reaching target is minimal.

---

# 📎 Appendix — Original v1 content (verbatim)
(Kept exactly so nothing is lost; v3 adds/extends above.)

# 🕸️ Flow-Wise Graph Traversal Mastery (v1)
**Goal:** Professional-grade intuition for graph traversal and search (Level 1 → Level 5).  
**Standards (same as your previous guides):** Why / What / How / Where / When + step/flow + lots of visuals + Python/C# snippets + common pitfalls + tips/tricks + invariants + mapped practice.

---

# 🧭 One-page Level Mapping Index (Graphs)
Use this to instantly map a graph problem to the right **level**, **pattern**, and **expected invariant**.

## ✅ Legend
- 🧾 **Invariant** = what stays true after each step.
- 🧠 **Visited** = prevents cycles and repeated work.
- 🧺 **Queue** = BFS frontier (wave).
- 🎒 **Stack** = DFS frontier (depth-first).

| Level | Pattern / skill | Typical LeetCode problems (examples) | Expected invariant (one-liner) |
|---|---|---|---|
| L1 | Graph representation + visited discipline | 1971 Find if Path Exists, 841 Keys and Rooms | “Every node is marked visited at most once; visited set matches explored region.” |
| L1 | DFS recursion skeleton | 200 Number of Islands, 695 Max Area of Island | “dfs(u) fully explores u’s connected region without revisiting nodes.” |
| L1 | BFS skeleton | 733 Flood Fill, 994 Rotting Oranges | “Queue contains exactly the frontier; every enqueued node is newly discovered.” |
| L2 | Iterative DFS (stack) | 133 Clone Graph, 547 Number of Provinces | “Stack represents pending nodes; visited prevents repeats; pop order doesn’t break correctness.” |
| L2 | Connected components | 323 Number of Connected Components (if available), 547 Provinces | “Each new DFS/BFS starting node discovers exactly one component.” |
| L3 | Shortest path in unweighted graphs (BFS + parent) | 752 Open the Lock, 127 Word Ladder | “First time you pop/visit a node is the shortest distance from source.” |
| L3 | Multi-source BFS | 542 01 Matrix, 994 Rotting Oranges | “Queue starts with all sources at distance 0; distances expand outward in layers.” |
| L3 | Bipartite check (BFS coloring) | 785 Is Graph Bipartite, 886 Possible Bipartition | “Each edge connects opposite colors; conflict implies not bipartite.” |
| L4 | Topological sort (DAG) | 207 Course Schedule, 210 Course Schedule II | “Order respects edges u->v: u appears before v; cycle means impossible.” |
| L4 | Cycle detection (directed/undirected) | 684 Redundant Connection, 802 Find Eventual Safe States | “A back-edge (or color conflict) implies a cycle; parent edge ignored in undirected.” |
| L4 | 0-1 BFS / weighted frontier tricks | 1368 Min Cost Valid Path, 2290 Min Obstacle Removal | “Deque pops smallest current cost first; edges 0 go front, edges 1 go back.” |
| L5 | State-space graphs (implicit nodes) | 127 Word Ladder, 1091 Shortest Path in Binary Matrix | “A ‘state’ is a node; neighbors are generated; visited prevents exponential blow-up.” |
| L5 | Graph DP on DAG | 797 All Paths From Source to Target, 1203 Sort Items by Groups | “dp[u] is final once all outgoing dependencies computed (topo order).” |

---

# 🧠 The ladder: complexity of state
- **Level 1:** Just traverse safely: adjacency + visited + base templates.
- **Level 2:** Control stack/iteration + discover components.
- **Level 3:** Add distance layers + parents for shortest paths (unweighted).
- **Level 4:** Add structure constraints: DAG ordering, cycles, 0-1 frontier.
- **Level 5:** Turn problems into graphs (implicit/state graphs) + DP-on-graph thinking.

---

# 🧱 Graph representations (baseline)

## 0.1 Adjacency list (most common)
**Why:** Space-efficient for sparse graphs, easy traversal.

**Visual**
```text
Edges: 0-1, 0-2, 1-3
Adj:
0: [ppl-ai-file-upload.s3.amazonaws](https://ppl-ai-file-upload.s3.amazonaws.com/web/direct-files/attachments/83049665/99a4b859-7120-4924-84dd-f374e18f9ee2/CONSOLIDATED_CSHARP_EXTENDED_SUPPORT_MASTER_PROMPT_v13.md)
1: [ppl-ai-file-upload.s3.amazonaws](https://ppl-ai-file-upload.s3.amazonaws.com/web/direct-files/attachments/83049665/9eb047cc-c20e-43fe-8d07-88d5746bddb6/VISUAL_PLAYBOOK_GENERATION_PROMPT_v12_UPDATED.md)
2: 
3: [ppl-ai-file-upload.s3.amazonaws](https://ppl-ai-file-upload.s3.amazonaws.com/web/direct-files/attachments/83049665/295a245b-4f5a-4e5b-b129-fabe583e1919/SYSTEM_PROMPT_v12_EXTENDED_SUPPORT_CSHARP.md)
```

### 🐍 Python
```python
adj = [[] for _ in range(n)]
for u, v in edges:
    adj[u].append(v)
    adj[v].append(u)  # omit for directed graphs
```

### 🟦 C#
```csharp
var adj = new List<int>[n];
for (int i = 0; i < n; i++) adj[i] = new List<int>();
foreach (var (u, v) in edges)
{
    adj[u].Add(v);
    adj[v].Add(u); // omit for directed
}
```

## 0.2 Grid-as-graph (the hidden graph)
**Why:** Many “matrix BFS/DFS” problems are graphs where each cell is a node.

**Visual**
```text
Grid:
(0,0) (0,1) (0,2)
(1,0) (1,1) (1,2)

Neighbors of (r,c):
(r-1,c), (r+1,c), (r,c-1), (r,c+1)  (if in bounds)
```

---

# 🟢 Level 1: The Physical Layer (Basic Traversal)
**Goal:** Muscle memory for visited + exploring neighbors.

## Topics to know
- ✅ visited discipline (when to mark)
- 🌊 BFS queue skeleton
- 🕳️ DFS recursion skeleton
- 🧩 edge cases: disconnected graphs, self-loops, duplicates

## Things to master
- ✅ Mark visited when you enqueue/push (prevents duplicates)
- ✅ State your invariant: “visited = discovered region”
- ✅ Handle disconnected graphs by looping through all nodes

---

## 1.1 🕳️ DFS (recursive) — explore a connected region

**Why:** DFS is the simplest way to explore everything reachable from a start node.

**What:** Go deep along neighbors; mark visited; backtrack.

**How (step/flow):**
1) dfs(u): mark u visited
2) for each v in adj[u]: if not visited[v], dfs(v)

**Where:** connected components, island counting, reachability.

**When:** you need “explore region” and recursion depth is safe.

**Visual**
```text
Graph:
0--1--3
 \
  2

Start dfs(0):
Visit 0 -> then 1 -> then 3, backtrack, then 2

Visited grows like a paint fill.
```

**Python**
```python
def dfs(u):
    visited[u] = True
    for v in adj[u]:
        if not visited[v]:
            dfs(v)
```

**C#**
```csharp
void Dfs(int u)
{
    visited[u] = true;
    foreach (int v in adj[u])
        if (!visited[v]) Dfs(v);
}
```

**Common pitfalls**
- ❌ forgetting visited -> infinite recursion on cycles
- ❌ recursion depth overflow on large/deep graphs (switch to Level 2 stack)

**Tips & tricks**
- 💡 Invariant: “once visited[u] is true, u will never be processed again.”

**Practice (2+)**
- LeetCode 1971: Find if Path Exists in Graph
- LeetCode 200: Number of Islands (grid DFS)

---

## 1.2 🌊 BFS (queue) — explore in waves

**Why:** BFS naturally processes by distance (#edges) in unweighted graphs.

**What:** Use a queue; enqueue start; pop; enqueue undiscovered neighbors.

**How (step/flow):**
1) queue = [start]
2) visited[start] = true
3) while queue:
4) u = pop front
5) for v in adj[u]: if not visited[v]: visited[v]=true; push v

**Where:** shortest path in unweighted graphs, level expansion, multi-source spread.

**When:** distance matters or you need layer-by-layer growth.

**Visual (frontier growth)**
```text
Level 0: {S}
Level 1: neighbors of S
Level 2: neighbors of Level1 not seen yet

Queue is the pipe that enforces this order.
```

**Python**
```python
from collections import deque

def bfs(start):
    q = deque([start])
    visited[start] = True
    while q:
        u = q.popleft()
        for v in adj[u]:
            if not visited[v]:
                visited[v] = True
                q.append(v)
```

**C#**
```csharp
void Bfs(int start)
{
    var q = new Queue<int>();
    q.Enqueue(start);
    visited[start] = true;

    while (q.Count > 0)
    {
        int u = q.Dequeue();
        foreach (int v in adj[u])
        {
            if (!visited[v])
            {
                visited[v] = true;
                q.Enqueue(v);
            }
        }
    }
}
```

**Common pitfalls**
- ❌ marking visited when dequeuing instead of enqueuing (causes duplicates)
- ❌ forgetting to initialize visited per test case

**Tips**
- 💡 Invariant: “Queue contains nodes discovered but not processed.”

**Practice (2+)**
- LeetCode 733: Flood Fill
- LeetCode 841: Keys and Rooms

---

## 1.3 🧩 Disconnected graphs (loop over all nodes)

**Why:** Many graphs have multiple components; starting from one node won’t cover all.

**What:** For each node not visited, start a new DFS/BFS.

**How (step/flow):**
1) for u in 0..n-1:
2) if not visited[u]: components++; dfs(u)

**Where:** count components, check if graph is fully connected.

**When:** input doesn’t guarantee connected.

**Visual**
```text
Component A: 0--1
Component B: 2--3

Loop u=0 starts exploring A
Loop u=2 starts exploring B
```

**Practice (2+)**
- LeetCode 547: Number of Provinces
- LeetCode 1319: Number of Operations to Make Network Connected

---

# 🔵 Level 2: The Structural Layer (Iterative DFS + Components)
**Goal:** Replace recursion with stack + learn component mechanics.

## Topics to know
- 🎒 iterative DFS (stack)
- 🧱 parent tracking (avoid false cycles in undirected)
- 🧩 grid traversal hygiene (bounds + visited)

## Things to master
- ✅ You can convert recursive DFS to stack DFS
- ✅ You know exactly when to mark visited

---

## 2.1 🎒 Iterative DFS (stack)

**Why:** Avoid recursion depth limits and gain explicit control.

**What:** Stack holds nodes to process.

**How (step/flow):**
1) push start; mark visited
2) while stack not empty:
3) u = pop
4) for v in adj[u]: if not visited: mark + push

**Visual**
```text
Stack (top at right):

pop 0, push neighbors -> [ppl-ai-file-upload.s3.amazonaws](https://ppl-ai-file-upload.s3.amazonaws.com/web/direct-files/attachments/83049665/99a4b859-7120-4924-84dd-f374e18f9ee2/CONSOLIDATED_CSHARP_EXTENDED_SUPPORT_MASTER_PROMPT_v13.md)
pop 2 -> ...

DFS order depends on push order, but reachability is correct.
```

**Pitfalls**
- ❌ marking visited on pop -> duplicates

**Practice (2+)**
- LeetCode 133: Clone Graph
- LeetCode 695: Max Area of Island (iterative)

---

## 2.2 🧱 Connected components as a reusable pattern

**Why:** “Count groups” appears everywhere: islands, provinces, friend circles.

**What:** Outer loop over nodes + inner traversal.

**How:** each fresh traversal discovers exactly one component.

**Invariant:** “No node belongs to two components.”

**Practice (2+)**
- LeetCode 200: Number of Islands
- LeetCode 547: Number of Provinces

---

# 🟠 Level 3: The Frontier Layer (Shortest Paths in Unweighted Graphs)
**Goal:** BFS + distances + parents.

## Topics to know
- 📏 BFS distance array
- 🧭 parent array for path reconstruction
- 🌋 multi-source BFS
- 🎨 bipartite coloring

## Things to master
- ✅ First time discovered = shortest distance (unweighted)
- ✅ You can reconstruct path using parent pointers

---

## 3.1 📏 BFS shortest distance + parent (path reconstruction)

**Why:** BFS gives shortest number-of-edges distance in unweighted graphs.

**What:** Maintain dist[u] and parent[u].

**How (step/flow):**
1) dist = -1 for all
2) dist[src]=0; parent[src]=-1
3) BFS: when discovering v from u:
   dist[v]=dist[u]+1; parent[v]=u

**Visual**
```text
S -- A -- B -- T

When T is first discovered, dist[T] is minimal.
Parents form a breadcrumb chain:
T <- B <- A <- S
```

**Pitfalls**
- ❌ overwriting parent/dist after already visited

**Practice (2+)**
- LeetCode 752: Open the Lock
- LeetCode 127: Word Ladder

---

## 3.2 🌋 Multi-source BFS (many starting points)

**Why:** Spread/rot/nearest-source problems start from multiple sources simultaneously.

**What:** Initialize queue with all sources at distance 0.

**How (step/flow):**
1) enqueue all sources
2) set their dist=0
3) BFS expands outward

**Visual**
```text
Sources: * *
Wave expands from all sources together.
The first wave to reach a cell sets the minimal distance.
```

**Practice (2+)**
- LeetCode 542: 01 Matrix
- LeetCode 994: Rotting Oranges

---

## 3.3 🎨 Bipartite check (BFS/DFS coloring)

**Why:** Many constraint problems reduce to 2-coloring.

**What:** Color nodes 0/1 so neighbors differ.

**How (step/flow):**
1) for each component: if uncolored, assign 0 and BFS
2) for each edge u-v: if color[v] == color[u] -> conflict

**Visual**
```text
u(0) -- v(1) -- w(0)
All edges connect opposite colors
Conflict occurs if an edge connects same color
```

**Practice (2+)**
- LeetCode 785: Is Graph Bipartite?
- LeetCode 886: Possible Bipartition

---

# 🟣 Level 4: The Constraint Layer (DAG, Cycles, 0-1 BFS)
**Goal:** Traversal with global structure constraints.

## Topics to know
- 🧱 topological sort (Kahn BFS / DFS finish order)
- 🔁 cycle detection (directed/undirected)
- 🪙 0-1 BFS (deque)

## Things to master
- ✅ “indegree” meaning and updates
- ✅ directed cycle detection using colors (0/1/2)

---

## 4.1 🧱 Topological sort (Kahn’s algorithm)

**Why:** Scheduling prerequisites requires an order that respects directed edges.

**What:** Repeatedly take nodes with indegree 0.

**How (step/flow):**
1) compute indegree[]
2) enqueue all nodes with indegree 0
3) pop u, append to order
4) for v in adj[u]: indegree[v]-- ; if 0 enqueue
5) if order size < n => cycle

**Visual**
```text
u -> v means u must come before v

Queue holds all currently-available tasks (indegree 0)
Processing u unlocks its neighbors.
```

**Practice (2+)**
- LeetCode 207: Course Schedule
- LeetCode 210: Course Schedule II

---

## 4.2 🔁 Cycle detection (directed graph coloring)

**Why:** Cycles break prerequisite ordering and many dependency problems.

**What:** Use colors:
- 0 = unvisited
- 1 = visiting (in current recursion stack)
- 2 = done

**How (step/flow):**
1) dfs(u): color[u]=1
2) for v in adj[u]:
   if color[v]==1 => back-edge => cycle
   if color[v]==0 => dfs(v)
3) color[u]=2

**Visual**
```text
0 (white) -> 1 (gray) -> 2 (black)

Back-edge to gray means a cycle.
```

**Practice (2+)**
- LeetCode 207: Course Schedule (cycle detection)
- LeetCode 802: Find Eventual Safe States

---

## 4.3 🪙 0-1 BFS (deque frontier)

**Why:** When edges have weight 0 or 1, you can do shortest path faster than Dijkstra using a deque.

**What:** Use deque; push 0-weight edges to front, 1-weight edges to back.

**How (step/flow):**
1) dist = INF; dist[src]=0
2) pop left u
3) for each edge u->v with w in {0,1}:
   if dist[u]+w < dist[v]: update dist[v]
   if w==0: pushleft(v) else pushright(v)

**Visual**
```text
Deque:
front ... back
0-cost neighbors go to front (processed sooner)
1-cost neighbors go to back

Invariant: deque pops the smallest-current-cost node first (for 0/1 weights)
```

**Practice (2+)**
- LeetCode 1368: Minimum Cost to Make at Least One Valid Path in a Grid
- LeetCode 2290: Minimum Obstacle Removal to Reach Corner

---

# 🔴 Level 5: The Abstract Layer (Implicit Graphs + Proof Thinking)
**Goal:** Turn problems into graphs and traverse the state space correctly.

## Topics to know
- 🧠 Implicit nodes (states) + neighbor generation
- 🧷 visited key design (what uniquely identifies a state)
- 🧮 DAG DP with topo order

## Things to master
- ✅ Define the state precisely (node)
- ✅ Define transitions precisely (edges)
- ✅ Prove visited key prevents revisits without losing optimal answers

---

## 5.1 🧠 State-space BFS (implicit graph)

**Why:** Many “puzzle” problems don’t give a graph, but you can generate neighbors.

**What:** A state is a node; legal moves generate edges.

**How (step/flow):**
1) define state representation (string/tuple)
2) generate neighbors (moves)
3) BFS with visited to avoid exponential repeats

**Visual**
```text
State: "0000"
Neighbors: change one wheel +/-1... This is a graph even though it wasn’t given as adj list.
```

**Practice (2+)**
- LeetCode 752: Open the Lock
- LeetCode 1091: Shortest Path in Binary Matrix

---

## 5.2 🧮 DP on DAG (traversal order matters)

**Why:** If the graph is a DAG, you can compute dp in topo order.

**What:** dp[u] depends on dp of neighbors or prerequisites.

**How (step/flow):**
1) topological sort
2) process nodes in that order
3) relax dp transitions

**Visual**
```text
Topo order ensures when you process u,
all nodes that u depends on are already computed.
```

**Practice (2+)**
- LeetCode 797: All Paths From Source to Target
- LeetCode 2050: Parallel Courses III (topo + dp)

---

# 🧰 Traversal mastery add-ons (graphs)

## A) 🧪 Micro-tracing (fast debugging)
- Draw 5 nodes and edges.
- Write visited set.
- For BFS: write queue as [front ... back].
- For DFS: write stack (or recursion) as vertical list.

## B) 🧠 Invariant library (memorize)
- DFS: “dfs(u) explores all reachable nodes from u exactly once.”
- BFS: “queue holds frontier; first discovery gives shortest distance (unweighted).”
- Components: “starting a traversal from an unvisited node finds exactly one component.”
- Topo (Kahn): “queue holds indegree 0 nodes only.”
- Directed cycle (colors): “back-edge to gray implies cycle.”
- 0-1 BFS: “deque order respects current best distances for 0/1 weights.”

## C) ✅ Pitfall checklist
- Did I mark visited on enqueue/push?
- Am I reusing visited/dist arrays incorrectly across test cases?
- If undirected: am I ignoring the parent edge in cycle checks?
- For BFS: did I freeze levelSize before iterating a level?
- For state graphs: is my visited key correct and minimal?

---

# 🔗 Suggested deep references (optional)
- cp-algorithms: BFS, DFS, 0-1 BFS, Topo (excellent for invariants + correctness intuition)

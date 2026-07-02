# DSA Mastery 101 — Phase 3 Trees, Graphs & Dynamic Programming

## Scope
This file covers **Phase 3** of the attached syllabus: **Weeks 7-11**.

Covered syllabus areas:
- Week 7: binary trees, traversals, BSTs, balanced BST overview, tree patterns, augmented trees / order-statistics awareness
- Week 8: graph models and representations, BFS, DFS, topological sort, connectivity, bipartite graphs, SCC awareness
- Week 9: Dijkstra, Bellman-Ford, Floyd-Warshall, minimum spanning trees, Union-Find / DSU
- Week 10: DP as recursion + memoization, 1D DP, knapsack family, grid DP, edit distance, sequence DP
- Week 11: DP on trees, DP on DAGs, bitmask DP, state compression, mixed advanced DP patterns

Programming language used throughout: **Python**.

> Note: each problem includes an original concise problem brief, not copied platform wording, while preserving the exact algorithmic target.

---

## How to study this phase
1. Identify whether the problem is about **structure traversal**, **state propagation**, **path optimization**, or **state compression**.
2. Say the invariant or state meaning before coding.
3. Dry-run on a tiny example first, especially for graphs and DP.
4. Compare the chosen approach with one alternate solution.
5. Mark coding friction points; these matter more in Phase 3 than in earlier phases.

---

## Phase 3 category map

| Category | Week alignment | Core interview idea |
|---|---|---|
| Tree traversals and basics | Week 7 Day 1 | DFS/BFS over hierarchical structure |
| BST and validation | Week 7 Day 2 | Ordered tree reasoning |
| Balanced BST overview | Week 7 Day 3 | Why logarithmic balance matters |
| Tree patterns | Week 7 Day 4 | Path, ancestor, diameter, serialization |
| Graph traversal | Week 8 Days 1-4 | BFS/DFS, reachability, ordering |
| SCC awareness | Week 8 Day 5 | Directional graph condensation |
| Shortest paths | Week 9 Days 1-3 | Local relaxation vs global relaxation |
| MST and DSU | Week 9 Days 4-5 | Connect everything cheaply |
| DP fundamentals | Week 10 Day 1 | State + transition + memoization/tabulation |
| 1D DP / knapsack | Week 10 Day 2 | Build answers from smaller prefixes or capacities |
| Grid and edit distance DP | Week 10 Day 3 | Two-dimensional state transitions |
| Sequence DP | Week 10 Day 4 | Order-sensitive subsequence reasoning |
| DP on trees | Week 11 Day 1 | State propagated through children |
| DP on DAGs | Week 11 Day 2 | Topological order makes recurrence acyclic |
| Bitmask / subset DP | Week 11 Day 3 | Compress chosen-set state into bits |
| State compression / mixed DP | Week 11 Days 4-5 | Advanced optimization and pattern fusion |

---

## Coverage promise
Every Phase 3 topic from the syllabus is represented by one or more **core anchor problems**, plus supplementary drills in the practice table.

---

# Part A — Binary trees and traversals

## A1. LeetCode #102 — Binary Tree Level Order Traversal
**Theme:** breadth-first traversal by levels  
**Difficulty:** Medium

### Problem brief
Return the values of a binary tree level by level from top to bottom.

### Recognition test
- Output grouped by levels.
- Natural “wavefront” traversal.
- Queue is more natural than recursion.

### Visual cue
```text
        3
      /   \
     9    20
         /  \
        15   7

Level 0 -> [3]
Level 1 -> [9, 20]
Level 2 -> [15, 7]
```

### Walkthrough
1. Push the root into a queue.
2. For each iteration, process exactly the current queue size.
3. Those nodes belong to one level.
4. Push their children for the next round.

### Invariant
At the start of each outer loop, the queue contains exactly one tree level.

### Python solution
```python
from collections import deque

class Solution:
    def levelOrder(self, root):
        if not root:
            return []

        ans = []
        q = deque([root])

        while q:
            level = []
            for _ in range(len(q)):
                node = q.popleft()
                level.append(node.val)
                if node.left:
                    q.append(node.left)
                if node.right:
                    q.append(node.right)
            ans.append(level)

        return ans
```

### Alternate ways
- DFS with depth parameter can also group nodes by level.
- BFS is the clearest answer because level structure is explicit.

### Likely coding friction
- Iterating directly over the queue while mutating it.
- Forgetting to capture `len(q)` before processing the level.

### Complexity
- Time: `O(n)`
- Space: `O(w)` where `w` is maximum width

---

## A2. LeetCode #94 — Binary Tree Inorder Traversal
**Theme:** recursive or iterative inorder  
**Difficulty:** Easy

### Problem brief
Return the inorder traversal of a binary tree.

### Recognition test
- Classic traversal problem.
- For BSTs, inorder yields sorted values.

### Visual cue
```text
Inorder = Left -> Node -> Right
```

### Walkthrough
1. Go as far left as possible.
2. Process node.
3. Move to right subtree.
4. Iterative solution uses a stack to simulate recursion.

### Python solution
```python
class Solution:
    def inorderTraversal(self, root):
        ans = []
        stack = []
        curr = root

        while curr or stack:
            while curr:
                stack.append(curr)
                curr = curr.left
            curr = stack.pop()
            ans.append(curr.val)
            curr = curr.right

        return ans
```

### Alternate ways
- Recursive traversal is shorter.
- Iterative version is better interview preparation because it exposes stack mechanics.

### Likely coding friction
- Forgetting to move to `curr.right` after processing a node.
- Popping too early without exhausting the left chain.

### Complexity
- Time: `O(n)`
- Space: `O(h)`

---

## A3. LeetCode #1448 — Count Good Nodes in Binary Tree
**Theme:** DFS with path state  
**Difficulty:** Medium

### Problem brief
Count nodes that are at least as large as every earlier node on the root-to-node path.

### Recognition test
- Need information from the path, not whole tree.
- DFS naturally carries path state downward.

### Visual cue
```text
root-to-node path keeps current_max
node is good if node.val >= current_max
```

### Python solution
```python
class Solution:
    def goodNodes(self, root) -> int:
        def dfs(node, current_max):
            if not node:
                return 0
            good = 1 if node.val >= current_max else 0
            current_max = max(current_max, node.val)
            return good + dfs(node.left, current_max) + dfs(node.right, current_max)

        return dfs(root, root.val)
```

### Alternate ways
- Iterative DFS with explicit stack works too.
- Recursive DFS is cleaner because state is path-specific.

### Likely coding friction
- Updating `current_max` before evaluating whether the current node is good.
- Using global state carelessly across branches.

### Complexity
- Time: `O(n)`
- Space: `O(h)`

---

# Part B — BST and balanced-tree awareness

## B1. LeetCode #98 — Validate Binary Search Tree
**Theme:** global bounds, not local comparisons  
**Difficulty:** Medium

### Problem brief
Determine whether a binary tree satisfies the BST ordering property everywhere.

### Recognition test
- A node must respect all ancestors, not only its parent.
- Local left/right comparison alone is insufficient.

### Visual cue
```text
        5
      /   \
     1     7
          / \
         4   8

4 is in right subtree of 5, so it must be > 5, but it isn't.
```

### Walkthrough
1. Each node gets an allowed `(low, high)` range.
2. Left child must be `< node.val`.
3. Right child must be `> node.val`.
4. Propagate bounds downward recursively.

### Invariant
Every recursive call checks one subtree under the exact valid numeric range inherited from ancestors.

### Python solution
```python
class Solution:
    def isValidBST(self, root) -> bool:
        def dfs(node, low, high):
            if not node:
                return True
            if not (low < node.val < high):
                return False
            return dfs(node.left, low, node.val) and dfs(node.right, node.val, high)

        return dfs(root, float('-inf'), float('inf'))
```

### Alternate ways
- Inorder traversal with strictly increasing sequence also works.
- Bounds-based DFS is more direct for correctness explanation.

### Likely coding friction
- Using `<=` and `>=` incorrectly with duplicates.
- Only checking immediate children instead of inherited bounds.

### Complexity
- Time: `O(n)`
- Space: `O(h)`

---

## B2. LeetCode #235 — Lowest Common Ancestor of a Binary Search Tree
**Theme:** exploit BST ordering  
**Difficulty:** Medium

### Problem brief
Find the lowest node in a BST that is an ancestor of both target nodes.

### Recognition test
- BST ordering lets you avoid full traversal.
- Targets diverging at a node signals the answer.

### Visual cue
```text
if both p and q < node -> go left
if both p and q > node -> go right
else node is split point = LCA
```

### Python solution
```python
class Solution:
    def lowestCommonAncestor(self, root, p, q):
        curr = root
        while curr:
            if p.val < curr.val and q.val < curr.val:
                curr = curr.left
            elif p.val > curr.val and q.val > curr.val:
                curr = curr.right
            else:
                return curr
```

### Alternate ways
- General binary tree LCA works too but ignores BST structure.
- BST-guided traversal gives `O(h)` time.

### Likely coding friction
- Not normalizing the idea that `p` may be larger or smaller than `q`; the split logic already handles both.
- Overcomplicating with path storage.

### Complexity
- Time: `O(h)`
- Space: `O(1)` iterative

---

## B3. LeetCode #230 — Kth Smallest Element in a BST
**Theme:** inorder + order statistics intuition  
**Difficulty:** Medium

### Problem brief
Return the `k`th smallest value in a BST.

### Recognition test
- BST inorder traversal is sorted order.
- Order-statistics idea appears naturally here.

### Visual cue
```text
BST inorder = sorted order
count nodes as visited until count == k
```

### Python solution
```python
class Solution:
    def kthSmallest(self, root, k: int) -> int:
        stack = []
        curr = root

        while curr or stack:
            while curr:
                stack.append(curr)
                curr = curr.left
            curr = stack.pop()
            k -= 1
            if k == 0:
                return curr.val
            curr = curr.right
```

### Alternate ways
- Augmented BST with subtree sizes supports repeated queries efficiently.
- For one query, simple inorder is enough.

### Likely coding friction
- Off-by-one in decrementing `k`.
- Mixing preorder intuition with inorder requirement.

### Complexity
- Time: `O(h + k)` worst case `O(n)`
- Space: `O(h)`

---

# Part C — Tree patterns

## C1. LeetCode #543 — Diameter of Binary Tree
**Theme:** combine heights bottom-up  
**Difficulty:** Easy

### Problem brief
Return the length of the longest path between any two nodes in a binary tree.

### Recognition test
- Best answer may pass through a node but does not have to include root.
- Height calculation and global optimum interact.

### Visual cue
```text
for each node:
left_height + right_height = path through node
best = max(best, that value)
```

### Walkthrough
1. DFS returns subtree height.
2. At each node, compute path length through it using left and right heights.
3. Update global best.
4. Return `1 + max(left, right)` upward.

### Python solution
```python
class Solution:
    def diameterOfBinaryTree(self, root) -> int:
        best = 0

        def dfs(node):
            nonlocal best
            if not node:
                return 0
            left = dfs(node.left)
            right = dfs(node.right)
            best = max(best, left + right)
            return 1 + max(left, right)

        dfs(root)
        return best
```

### Alternate ways
- Naive recomputation of heights from every node leads to `O(n^2)`.
- Single-pass postorder DFS is the intended pattern.

### Likely coding friction
- Confusing diameter in edges versus nodes.
- Returning path length instead of height from DFS.

### Complexity
- Time: `O(n)`
- Space: `O(h)`

---

## C2. LeetCode #236 — Lowest Common Ancestor of a Binary Tree
**Theme:** postorder ancestor discovery  
**Difficulty:** Medium

### Problem brief
Find the lowest common ancestor of two given nodes in a general binary tree.

### Recognition test
- No BST ordering available.
- Need information from both subtrees.

### Visual cue
```text
if left subtree has one target and right subtree has the other,
current node is the LCA
```

### Python solution
```python
class Solution:
    def lowestCommonAncestor(self, root, p, q):
        if not root or root == p or root == q:
            return root

        left = self.lowestCommonAncestor(root.left, p, q)
        right = self.lowestCommonAncestor(root.right, p, q)

        if left and right:
            return root
        return left or right
```

### Alternate ways
- Parent pointers + hash set of ancestors.
- Path-to-node comparison.
- Recursive postorder is the cleanest standard answer.

### Likely coding friction
- Returning boolean instead of node reference.
- Forgetting that if current node is `p` or `q`, it should return itself.

### Complexity
- Time: `O(n)`
- Space: `O(h)`

---

## C3. LeetCode #124 — Binary Tree Maximum Path Sum
**Theme:** tree DP with one-branch return, two-branch update  
**Difficulty:** Hard

### Problem brief
Return the maximum sum of any path in the tree, where the path may start and end anywhere but must follow parent-child edges.

### Recognition test
- Like diameter, but weighted and negative values matter.
- Upward return and global update are different quantities.

### Visual cue
```text
return to parent: node + max(one useful child branch)
update answer: node + left_gain + right_gain
```

### Walkthrough
1. DFS returns the best one-sided gain from each node upward.
2. Negative gains are clipped to zero.
3. At each node, consider using both left and right gains to form a complete path.
4. Update global best.

### Python solution
```python
class Solution:
    def maxPathSum(self, root) -> int:
        best = float('-inf')

        def dfs(node):
            nonlocal best
            if not node:
                return 0
            left = max(dfs(node.left), 0)
            right = max(dfs(node.right), 0)
            best = max(best, node.val + left + right)
            return node.val + max(left, right)

        dfs(root)
        return best
```

### Alternate ways
- No simpler brute force is interview-practical.
- This is a canonical tree-DP pattern and worth memorizing conceptually.

### Likely coding friction
- Returning both branches upward, which is invalid because parent path cannot fork twice.
- Mishandling all-negative trees if initialized poorly.

### Complexity
- Time: `O(n)`
- Space: `O(h)`

---

## C4. LeetCode #297 — Serialize and Deserialize Binary Tree
**Theme:** structural encoding of trees  
**Difficulty:** Hard

### Problem brief
Convert a binary tree into a string form and rebuild the same tree later.

### Recognition test
- Need to preserve shape, not just values.
- Null markers are essential.

### Visual cue
```text
preorder with null markers:
1,2,#,#,3,4,#,#,5,#,#
```

### Python solution
```python
from collections import deque

class Codec:
    def serialize(self, root):
        vals = []

        def dfs(node):
            if not node:
                vals.append('#')
                return
            vals.append(str(node.val))
            dfs(node.left)
            dfs(node.right)

        dfs(root)
        return ','.join(vals)

    def deserialize(self, data):
        vals = deque(data.split(','))

        def dfs():
            val = vals.popleft()
            if val == '#':
                return None
            node = TreeNode(int(val))
            node.left = dfs()
            node.right = dfs()
            return node

        return dfs()
```

### Alternate ways
- Level-order serialization also works.
- Preorder + null markers is concise and deterministic.

### Likely coding friction
- Omitting null markers, which loses shape information.
- Using inconsistent serialization and deserialization orders.

### Complexity
- Time: `O(n)`
- Space: `O(n)`

---

# Part D — Graph traversal fundamentals

## D1. LeetCode #200 — Number of Islands
**Theme:** connected components in a grid  
**Difficulty:** Medium

### Problem brief
Count how many disconnected groups of land cells appear in a 2D grid.

### Recognition test
- Grid can be treated as an implicit graph.
- Each island is one connected component.

### Visual cue
```text
1 1 0 0
1 0 0 1
0 0 1 1

start DFS/BFS on an unseen land cell -> marks one full island
```

### Walkthrough
1. Scan the grid.
2. When an unvisited land cell appears, increment the count.
3. Flood-fill that entire component via DFS or BFS.
4. Continue scanning.

### Python solution
```python
class Solution:
    def numIslands(self, grid: list[list[str]]) -> int:
        rows, cols = len(grid), len(grid[0])
        count = 0

        def dfs(r, c):
            if r < 0 or r >= rows or c < 0 or c >= cols or grid[r][c] != '1':
                return
            grid[r][c] = '0'
            dfs(r + 1, c)
            dfs(r - 1, c)
            dfs(r, c + 1)
            dfs(r, c - 1)

        for r in range(rows):
            for c in range(cols):
                if grid[r][c] == '1':
                    count += 1
                    dfs(r, c)

        return count
```

### Alternate ways
- BFS queue flood fill.
- DSU also works but is less direct for one-off counting.

### Likely coding friction
- Forgetting to mark visited immediately.
- Diagonal neighbors are not allowed unless specified.

### Complexity
- Time: `O(mn)`
- Space: `O(mn)` worst-case recursion/queue

---

## D2. LeetCode #994 — Rotting Oranges
**Theme:** multi-source BFS  
**Difficulty:** Medium

### Problem brief
Each minute, rotten oranges infect adjacent fresh oranges. Return the minimum time needed to rot all fresh oranges.

### Recognition test
- Time evolves in discrete layers.
- Multiple starting sources spread simultaneously.

### Visual cue
```text
all rotten oranges enter queue first
minute 0 -> current rotten layer
minute 1 -> newly infected neighbors
```

### Python solution
```python
from collections import deque

class Solution:
    def orangesRotting(self, grid: list[list[int]]) -> int:
        rows, cols = len(grid), len(grid[0])
        q = deque()
        fresh = 0

        for r in range(rows):
            for c in range(cols):
                if grid[r][c] == 2:
                    q.append((r, c))
                elif grid[r][c] == 1:
                    fresh += 1

        minutes = 0
        dirs = [(1, 0), (-1, 0), (0, 1), (0, -1)]

        while q and fresh > 0:
            for _ in range(len(q)):
                r, c = q.popleft()
                for dr, dc in dirs:
                    nr, nc = r + dr, c + dc
                    if 0 <= nr < rows and 0 <= nc < cols and grid[nr][nc] == 1:
                        grid[nr][nc] = 2
                        fresh -= 1
                        q.append((nr, nc))
            minutes += 1

        return minutes if fresh == 0 else -1
```

### Alternate ways
- DFS time propagation is possible but less robust.
- Multi-source BFS is the correct mental model for minimum-step spread.

### Likely coding friction
- Incrementing time per orange instead of per level.
- Forgetting to enqueue all initial rotten sources first.

### Complexity
- Time: `O(mn)`
- Space: `O(mn)`

---

## D3. LeetCode #207 — Course Schedule
**Theme:** cycle detection in directed graph  
**Difficulty:** Medium

### Problem brief
Given prerequisite pairs, determine whether all courses can be completed.

### Recognition test
- “Can finish all tasks with dependencies?” usually means directed-cycle detection.
- DAG means possible; cycle means impossible.

### Visual cue
```text
0 <- 1 <- 2
^         |
|---------|
cycle -> impossible
```

### Python solution
```python
from collections import defaultdict

class Solution:
    def canFinish(self, numCourses: int, prerequisites: list[list[int]]) -> bool:
        graph = defaultdict(list)
        for a, b in prerequisites:
            graph[b].append(a)

        state = [0] * numCourses

        def dfs(node):
            if state[node] == 1:
                return False
            if state[node] == 2:
                return True
            state[node] = 1
            for nei in graph[node]:
                if not dfs(nei):
                    return False
            state[node] = 2
            return True

        return all(dfs(i) for i in range(numCourses))
```

### Alternate ways
- Kahn’s algorithm with indegrees is also excellent.
- DFS coloring is especially good for explicit cycle detection.

### Likely coding friction
- Using only a visited set and missing back-edge cycles.
- Not distinguishing current-path state from fully processed state.

### Complexity
- Time: `O(V + E)`
- Space: `O(V + E)`

---

## D4. LeetCode #210 — Course Schedule II
**Theme:** topological ordering  
**Difficulty:** Medium

### Problem brief
Return one valid order to finish all courses, or an empty array if impossible.

### Recognition test
- Need an ordering that respects directed prerequisites.
- Topological sort is the target pattern.

### Visual cue
```text
nodes with indegree 0 are ready now
remove them, update indegrees, repeat
```

### Python solution
```python
from collections import defaultdict, deque

class Solution:
    def findOrder(self, numCourses: int, prerequisites: list[list[int]]) -> list[int]:
        graph = defaultdict(list)
        indegree = [0] * numCourses

        for a, b in prerequisites:
            graph[b].append(a)
            indegree[a] += 1

        q = deque([i for i in range(numCourses) if indegree[i] == 0])
        order = []

        while q:
            node = q.popleft()
            order.append(node)
            for nei in graph[node]:
                indegree[nei] -= 1
                if indegree[nei] == 0:
                    q.append(nei)

        return order if len(order) == numCourses else []
```

### Alternate ways
- DFS postorder topological sort also works.
- Kahn’s algorithm makes cycle failure obvious.

### Likely coding friction
- Mixing edge direction in the prerequisite graph.
- Forgetting that nodes with indegree 0 can start immediately.

### Complexity
- Time: `O(V + E)`
- Space: `O(V + E)`

---

## D5. LeetCode #785 — Is Graph Bipartite?
**Theme:** 2-coloring graph by BFS/DFS  
**Difficulty:** Medium

### Problem brief
Determine whether an undirected graph can be colored using two colors so that adjacent nodes have different colors.

### Recognition test
- “Two groups with no internal conflict” often hints bipartite checking.
- Alternating levels or colors matter.

### Visual cue
```text
color start node 0
all neighbors -> color 1
their neighbors -> color 0
conflict means not bipartite
```

### Python solution
```python
from collections import deque

class Solution:
    def isBipartite(self, graph: list[list[int]]) -> bool:
        n = len(graph)
        color = [-1] * n

        for start in range(n):
            if color[start] != -1:
                continue
            q = deque([start])
            color[start] = 0
            while q:
                node = q.popleft()
                for nei in graph[node]:
                    if color[nei] == -1:
                        color[nei] = 1 - color[node]
                        q.append(nei)
                    elif color[nei] == color[node]:
                        return False
        return True
```

### Alternate ways
- DFS coloring is equivalent.
- BFS level thinking often makes the parity logic clearer.

### Likely coding friction
- Forgetting disconnected components.
- Assuming odd cycle detection without actually coloring.

### Complexity
- Time: `O(V + E)`
- Space: `O(V)`

---

## D6. LeetCode #1466 — Reorder Routes to Make All Paths Lead to the City Zero
**Theme:** directed edges + traversal with edge cost interpretation  
**Difficulty:** Medium

### Problem brief
Count the minimum number of directed roads that must be reversed so every city can reach city `0`.

### Recognition test
- Convert direction issue into traversal on an undirected view plus edge labels.
- DFS/BFS from node `0` accumulates reversal cost.

### Visual cue
```text
store edges both ways:
(u -> v, cost 1) means original direction points away from 0
(v -> u, cost 0) means already usable toward 0
```

### Python solution
```python
from collections import defaultdict

class Solution:
    def minReorder(self, n: int, connections: list[list[int]]) -> int:
        graph = defaultdict(list)
        for a, b in connections:
            graph[a].append((b, 1))
            graph[b].append((a, 0))

        seen = set()

        def dfs(node):
            seen.add(node)
            changes = 0
            for nei, cost in graph[node]:
                if nei not in seen:
                    changes += cost + dfs(nei)
            return changes

        return dfs(0)
```

### Alternate ways
- BFS works equally well.
- The trick is modeling edge directions, not the traversal type itself.

### Likely coding friction
- Traversing only original directed edges and getting stuck.
- Missing why the reverse helper edge has cost `0`.

### Complexity
- Time: `O(V + E)`
- Space: `O(V + E)`

---

# Part E — Strongly connected components awareness

## E1. LeetCode #802 — Find Eventual Safe States
**Theme:** reverse graph + outdegree trimming / SCC intuition  
**Difficulty:** Medium

### Problem brief
Return all nodes in a directed graph from which every possible path eventually ends at a terminal node.

### Recognition test
- Safety in directed graphs is related to not being trapped in cycles.
- Reverse-graph processing avoids explicit SCC coding here.

### Visual cue
```text
terminal nodes are safe
remove them backwards
nodes whose outgoing choices all become safe are also safe
```

### Python solution
```python
from collections import deque

class Solution:
    def eventualSafeNodes(self, graph: list[list[int]]) -> list[int]:
        n = len(graph)
        rev = [[] for _ in range(n)]
        outdeg = [0] * n

        for u in range(n):
            outdeg[u] = len(graph[u])
            for v in graph[u]:
                rev[v].append(u)

        q = deque([i for i in range(n) if outdeg[i] == 0])
        safe = []

        while q:
            node = q.popleft()
            safe.append(node)
            for prev in rev[node]:
                outdeg[prev] -= 1
                if outdeg[prev] == 0:
                    q.append(prev)

        return sorted(safe)
```

### Alternate ways
- DFS with color states also works.
- SCC-based reasoning explains why cycle-containing regions are unsafe.

### Likely coding friction
- Thinking forward instead of backward.
- Confusing terminal nodes with source nodes.

### Complexity
- Time: `O(V + E)`
- Space: `O(V + E)`

---

# Part F — Shortest paths

## F1. LeetCode #743 — Network Delay Time
**Theme:** Dijkstra on weighted graph  
**Difficulty:** Medium

### Problem brief
Given directed weighted travel times from one source, compute when the last reachable node receives the signal.

### Recognition test
- Non-negative weighted shortest paths from a single source.
- Priority queue is the strongest signal.

### Visual cue
```text
always expand the currently closest unfinished node
heap keeps best known frontier distances
```

### Walkthrough
1. Build adjacency list.
2. Use min-heap keyed by distance.
3. Pop the smallest unfinalized distance.
4. Relax outgoing edges.

### Python solution
```python
import heapq
from collections import defaultdict

class Solution:
    def networkDelayTime(self, times: list[list[int]], n: int, k: int) -> int:
        graph = defaultdict(list)
        for u, v, w in times:
            graph[u].append((v, w))

        dist = {}
        heap = [(0, k)]

        while heap:
            d, node = heapq.heappop(heap)
            if node in dist:
                continue
            dist[node] = d
            for nei, w in graph[node]:
                if nei not in dist:
                    heapq.heappush(heap, (d + w, nei))

        return max(dist.values()) if len(dist) == n else -1
```

### Alternate ways
- Bellman-Ford also works but is slower.
- Dijkstra is correct only because weights are non-negative.

### Likely coding friction
- Reprocessing finalized nodes without a visited/finalized check.
- Using BFS because the graph “looks like reachability” even though weights matter.

### Complexity
- Time: `O((V + E) log V)`
- Space: `O(V + E)`

---

## F2. LeetCode #787 — Cheapest Flights Within K Stops
**Theme:** bounded-edge shortest path / Bellman-Ford style  
**Difficulty:** Medium

### Problem brief
Find the minimum cost to travel from source to destination with at most `k` stops.

### Recognition test
- Path length is constrained by number of edges/stops.
- Standard Dijkstra is trickier because stop count matters.

### Visual cue
```text
perform relaxations layer by layer
iteration i allows paths using up to i edges
```

### Python solution
```python
class Solution:
    def findCheapestPrice(self, n: int, flights: list[list[int]], src: int, dst: int, k: int) -> int:
        INF = float('inf')
        dist = [INF] * n
        dist[src] = 0

        for _ in range(k + 1):
            nxt = dist[:]
            for u, v, w in flights:
                if dist[u] != INF and dist[u] + w < nxt[v]:
                    nxt[v] = dist[u] + w
            dist = nxt

        return -1 if dist[dst] == INF else dist[dst]
```

### Alternate ways
- State-augmented Dijkstra can work.
- Bellman-Ford style `k+1` relaxations is interview-friendly and directly matches the stop constraint.

### Likely coding friction
- Updating in-place instead of using a copy, which accidentally allows too many edges per round.
- Miscounting stops versus edges.

### Complexity
- Time: `O(kE)`
- Space: `O(V)`

---

## F3. LeetCode #1334 — Find the City With the Smallest Number of Neighbors at a Threshold Distance
**Theme:** all-pairs shortest paths / Floyd-Warshall flavor  
**Difficulty:** Medium

### Problem brief
Find the city that can reach the fewest other cities within a distance threshold, breaking ties by larger index.

### Recognition test
- Need shortest paths between many pairs.
- Small-to-medium dense graph suggests Floyd-Warshall is reasonable.

### Visual cue
```text
use each node k as an allowed intermediate
update dist[i][j] = min(dist[i][j], dist[i][k] + dist[k][j])
```

### Python solution
```python
class Solution:
    def findTheCity(self, n: int, edges: list[list[int]], distanceThreshold: int) -> int:
        INF = float('inf')
        dist = [[INF] * n for _ in range(n)]
        for i in range(n):
            dist[i][i] = 0
        for u, v, w in edges:
            dist[u][v] = min(dist[u][v], w)
            dist[v][u] = min(dist[v][u], w)

        for k in range(n):
            for i in range(n):
                for j in range(n):
                    if dist[i][k] + dist[k][j] < dist[i][j]:
                        dist[i][j] = dist[i][k] + dist[k][j]

        best_city = -1
        best_count = float('inf')
        for i in range(n):
            count = sum(1 for j in range(n) if i != j and dist[i][j] <= distanceThreshold)
            if count <= best_count:
                best_count = count
                best_city = i
        return best_city
```

### Alternate ways
- Run Dijkstra from every node for sparse graphs.
- Floyd-Warshall is easier when the graph size is small and all-pairs logic is central.

### Likely coding friction
- Forgetting to initialize diagonal zeros.
- Missing tie-breaking requirement.
- Using Floyd-Warshall on very large graphs without checking constraints.

### Complexity
- Time: `O(n^3)`
- Space: `O(n^2)`

---

# Part G — Minimum spanning trees and DSU

## G1. LeetCode #1584 — Min Cost to Connect All Points
**Theme:** MST on complete graph  
**Difficulty:** Medium

### Problem brief
Connect all given points with minimum total Manhattan-distance cost.

### Recognition test
- Need connect-all with minimum total cost.
- No source/destination target; this is MST territory, not shortest path.

### Visual cue
```text
MST does not care about one shortest route
it cares about cheapest full network
```

### Python solution
```python
import heapq

class Solution:
    def minCostConnectPoints(self, points: list[list[int]]) -> int:
        n = len(points)
        visited = [False] * n
        heap = [(0, 0)]
        ans = 0
        used = 0

        while used < n:
            cost, u = heapq.heappop(heap)
            if visited[u]:
                continue
            visited[u] = True
            ans += cost
            used += 1

            x1, y1 = points[u]
            for v in range(n):
                if not visited[v]:
                    x2, y2 = points[v]
                    dist = abs(x1 - x2) + abs(y1 - y2)
                    heapq.heappush(heap, (dist, v))

        return ans
```

### Alternate ways
- Kruskal works but explicit edge list is `O(n^2)` in size anyway.
- Prim is often more natural here.

### Likely coding friction
- Confusing MST with shortest path tree.
- Forgetting to skip already visited vertices when popping heap entries.

### Complexity
- Time: `O(n^2 log n)` in this straightforward form
- Space: `O(n^2)` heap worst case

---

## G2. LeetCode #684 — Redundant Connection
**Theme:** DSU detects first cycle edge  
**Difficulty:** Medium

### Problem brief
A tree gained one extra undirected edge. Return an edge whose addition creates a cycle.

### Recognition test
- Edges added one by one.
- Need to know when two nodes are already connected.
- Classic DSU fit.

### Visual cue
```text
if find(u) == find(v), u and v already share a component
adding edge creates cycle
```

### Python solution
```python
class DSU:
    def __init__(self, n):
        self.parent = list(range(n + 1))
        self.rank = [0] * (n + 1)

    def find(self, x):
        if self.parent[x] != x:
            self.parent[x] = self.find(self.parent[x])
        return self.parent[x]

    def union(self, a, b):
        pa, pb = self.find(a), self.find(b)
        if pa == pb:
            return False
        if self.rank[pa] < self.rank[pb]:
            pa, pb = pb, pa
        self.parent[pb] = pa
        if self.rank[pa] == self.rank[pb]:
            self.rank[pa] += 1
        return True


class Solution:
    def findRedundantConnection(self, edges: list[list[int]]) -> list[int]:
        dsu = DSU(len(edges))
        for u, v in edges:
            if not dsu.union(u, v):
                return [u, v]
```

### Alternate ways
- DFS reachability per edge insertion is slower.
- DSU is the intended structure here.

### Likely coding friction
- Forgetting path compression or union by rank is okay for many inputs, but the conceptual DSU pattern should include them.
- Using DSU indexing incorrectly when nodes are 1-based.

### Complexity
- Time: almost `O(E α(V))`
- Space: `O(V)`

---

## G3. LeetCode #1319 — Number of Operations to Make Network Connected
**Theme:** component counting with DSU  
**Difficulty:** Medium

### Problem brief
Determine the minimum number of cable moves needed to connect all computers, or return `-1` if impossible.

### Recognition test
- Need enough edges plus component counting.
- DSU merges components efficiently.

### Visual cue
```text
need at least n-1 edges overall
answer = number_of_components - 1
```

### Python solution
```python
class DSU:
    def __init__(self, n):
        self.parent = list(range(n))
        self.rank = [0] * n

    def find(self, x):
        if self.parent[x] != x:
            self.parent[x] = self.find(self.parent[x])
        return self.parent[x]

    def union(self, a, b):
        pa, pb = self.find(a), self.find(b)
        if pa == pb:
            return False
        if self.rank[pa] < self.rank[pb]:
            pa, pb = pb, pa
        self.parent[pb] = pa
        if self.rank[pa] == self.rank[pb]:
            self.rank[pa] += 1
        return True


class Solution:
    def makeConnected(self, n: int, connections: list[list[int]]) -> int:
        if len(connections) < n - 1:
            return -1
        dsu = DSU(n)
        components = n
        for u, v in connections:
            if dsu.union(u, v):
                components -= 1
        return components - 1
```

### Alternate ways
- DFS/BFS component counting also works.
- DSU becomes more reusable for dynamic connectivity patterns.

### Likely coding friction
- Forgetting the quick impossibility check `edges < n - 1`.
- Counting redundant edges instead of counting components.

### Complexity
- Time: almost `O(E α(V))`
- Space: `O(V)`

---

# Part H — DP fundamentals and 1D DP

## H1. LeetCode #70 — Climbing Stairs
**Theme:** DP from recurrence  
**Difficulty:** Easy

### Problem brief
Count how many distinct ways exist to reach the top when each move can take 1 or 2 steps.

### Recognition test
- Answer depends on a few smaller answers.
- Overlapping subproblems are obvious.

### Visual cue
```text
ways[n] = ways[n-1] + ways[n-2]
```

### Python solution
```python
class Solution:
    def climbStairs(self, n: int) -> int:
        if n <= 2:
            return n
        a, b = 1, 2
        for _ in range(3, n + 1):
            a, b = b, a + b
        return b
```

### Alternate ways
- Plain recursion is exponential.
- Memoization and tabulation both work.
- Rolling variables are the space-optimized final form.

### Likely coding friction
- Weak base cases.
- Not recognizing this as the simplest DP warm-up.

### Complexity
- Time: `O(n)`
- Space: `O(1)`

---

## H2. LeetCode #198 — House Robber
**Theme:** choose or skip current item  
**Difficulty:** Medium

### Problem brief
Maximize total money robbed from a line of houses when adjacent houses cannot both be robbed.

### Recognition test
- Local binary choice with dependency on previous states.
- “Take or skip” is the DP signal.

### Visual cue
```text
best[i] = max(skip current, take current + best[i-2])
```

### Python solution
```python
class Solution:
    def rob(self, nums: list[int]) -> int:
        rob_prev, skip_prev = 0, 0
        for x in nums:
            new_rob = skip_prev + x
            new_skip = max(skip_prev, rob_prev)
            rob_prev, skip_prev = new_rob, new_skip
        return max(rob_prev, skip_prev)
```

### Alternate ways
- Recursion + memoization.
- Standard DP array.
- Rolling states are elegant and efficient.

### Likely coding friction
- Using greedy local pick instead of state reasoning.
- Confusing “rob previous” with “best up to previous.”

### Complexity
- Time: `O(n)`
- Space: `O(1)`

---

## H3. LeetCode #322 — Coin Change
**Theme:** minimum count DP  
**Difficulty:** Medium

### Problem brief
Given coin denominations, return the minimum number of coins needed to form a target amount, or `-1` if impossible.

### Recognition test
- Optimization over smaller amounts.
- Transition tries all coin choices.

### Visual cue
```text
dp[a] = 1 + min(dp[a - coin]) over usable coins
```

### Python solution
```python
class Solution:
    def coinChange(self, coins: list[int], amount: int) -> int:
        INF = amount + 1
        dp = [INF] * (amount + 1)
        dp[0] = 0

        for a in range(1, amount + 1):
            for c in coins:
                if c <= a:
                    dp[a] = min(dp[a], dp[a - c] + 1)

        return -1 if dp[amount] == INF else dp[amount]
```

### Alternate ways
- BFS on amounts is possible.
- DP is the standard answer because state is one-dimensional and reusable.

### Likely coding friction
- Using greedy with arbitrary coin systems.
- Bad infinity initialization.

### Complexity
- Time: `O(amount * len(coins))`
- Space: `O(amount)`

---

## H4. LeetCode #416 — Partition Equal Subset Sum
**Theme:** 0/1 knapsack reachability  
**Difficulty:** Medium

### Problem brief
Determine whether the numbers can be split into two subsets with equal sum.

### Recognition test
- Equal partition becomes subset-sum target `total // 2`.
- Each number can be used at most once.

### Visual cue
```text
target = total_sum / 2
can I reach target using each item once?
```

### Python solution
```python
class Solution:
    def canPartition(self, nums: list[int]) -> bool:
        total = sum(nums)
        if total % 2:
            return False
        target = total // 2
        dp = [False] * (target + 1)
        dp[0] = True

        for x in nums:
            for s in range(target, x - 1, -1):
                dp[s] = dp[s] or dp[s - x]

        return dp[target]
```

### Alternate ways
- Set-based reachable sums is another readable approach.
- Reverse iteration is essential in 0/1 knapsack style to avoid reusing the same item.

### Likely coding friction
- Iterating forward and accidentally allowing repeated use.
- Forgetting odd total sum is immediately impossible.

### Complexity
- Time: `O(n * target)`
- Space: `O(target)`

---

# Part I — Grid DP and edit distance

## I1. LeetCode #62 — Unique Paths
**Theme:** count paths in a grid  
**Difficulty:** Medium

### Problem brief
Count the number of ways to move from the top-left to bottom-right of a grid using only right and down moves.

### Recognition test
- Grid, limited move directions, overlapping subproblems.
- Simple additive DP.

### Visual cue
```text
ways[r][c] = ways[r-1][c] + ways[r][c-1]
```

### Python solution
```python
class Solution:
    def uniquePaths(self, m: int, n: int) -> int:
        dp = [1] * n
        for _ in range(1, m):
            for c in range(1, n):
                dp[c] += dp[c - 1]
        return dp[-1]
```

### Alternate ways
- 2D DP table is more visual.
- Combinatorics also works, but DP is better for pattern learning.

### Likely coding friction
- Confusing row and column indexing.
- Not seeing why first row and first column are all ones.

### Complexity
- Time: `O(mn)`
- Space: `O(n)`

---

## I2. LeetCode #64 — Minimum Path Sum
**Theme:** weighted grid DP  
**Difficulty:** Medium

### Problem brief
Find the minimum cost path from the top-left to the bottom-right using only right and down moves.

### Recognition test
- Same grid structure as Unique Paths, but optimize sum instead of count.

### Visual cue
```text
dp[r][c] = grid[r][c] + min(top, left)
```

### Python solution
```python
class Solution:
    def minPathSum(self, grid: list[list[int]]) -> int:
        rows, cols = len(grid), len(grid[0])
        dp = [float('inf')] * cols
        dp[0] = 0

        for r in range(rows):
            new = [float('inf')] * cols
            for c in range(cols):
                if r == 0 and c == 0:
                    new[c] = grid[0][0]
                else:
                    top = dp[c] if r > 0 else float('inf')
                    left = new[c - 1] if c > 0 else float('inf')
                    new[c] = grid[r][c] + min(top, left)
            dp = new

        return dp[-1]
```

### Alternate ways
- In-place mutation of the grid is acceptable when allowed.
- Standard 2D DP is easier for first understanding.

### Likely coding friction
- Boundary initialization for first row/column.
- Mixing count transitions with min transitions.

### Complexity
- Time: `O(mn)`
- Space: `O(n)`

---

## I3. LeetCode #72 — Edit Distance
**Theme:** classic 2D transformation DP  
**Difficulty:** Medium

### Problem brief
Compute the minimum number of insertions, deletions, and replacements needed to convert one string into another.

### Recognition test
- Compare prefixes of two strings.
- Three operations create branching subproblems.

### Visual cue
```text
dp[i][j] = min edits to turn word1[:i] into word2[:j]
```

### Walkthrough
1. Base row and column handle empty-string conversions.
2. If characters match, carry diagonal value.
3. Otherwise take `1 + min(insert, delete, replace)`.

### Python solution
```python
class Solution:
    def minDistance(self, word1: str, word2: str) -> int:
        m, n = len(word1), len(word2)
        dp = [[0] * (n + 1) for _ in range(m + 1)]

        for i in range(m + 1):
            dp[i][0] = i
        for j in range(n + 1):
            dp[0][j] = j

        for i in range(1, m + 1):
            for j in range(1, n + 1):
                if word1[i - 1] == word2[j - 1]:
                    dp[i][j] = dp[i - 1][j - 1]
                else:
                    dp[i][j] = 1 + min(
                        dp[i - 1][j],
                        dp[i][j - 1],
                        dp[i - 1][j - 1]
                    )

        return dp[m][n]
```

### Alternate ways
- Memoized recursion is good for derivation.
- Tabulation is usually better in interviews because state flow is explicit.

### Likely coding friction
- Confusing which move corresponds to insert, delete, or replace.
- Off-by-one errors in prefix indexing.

### Complexity
- Time: `O(mn)`
- Space: `O(mn)`

---

# Part J — Sequence DP

## J1. LeetCode #300 — Longest Increasing Subsequence
**Theme:** sequence DP and optimization awareness  
**Difficulty:** Medium

### Problem brief
Return the length of the longest strictly increasing subsequence.

### Recognition test
- Subsequence, not substring.
- Order matters, contiguity does not.

### Visual cue
```text
dp[i] = best LIS ending at i
check all j < i with nums[j] < nums[i]
```

### Python solution
```python
class Solution:
    def lengthOfLIS(self, nums: list[int]) -> int:
        dp = [1] * len(nums)
        for i in range(len(nums)):
            for j in range(i):
                if nums[j] < nums[i]:
                    dp[i] = max(dp[i], dp[j] + 1)
        return max(dp)
```

### Alternate ways
- Patience sorting + binary search gives `O(n log n)`.
- The `O(n^2)` DP is better for learning sequence-state thinking first.

### Likely coding friction
- Treating subsequence like contiguous segment.
- Forgetting strictness of increasing relation.

### Complexity
- Time: `O(n^2)`
- Space: `O(n)`

---

## J2. LeetCode #1143 — Longest Common Subsequence
**Theme:** two-sequence prefix DP  
**Difficulty:** Medium

### Problem brief
Return the length of the longest subsequence present in both strings.

### Recognition test
- Two strings, subsequence, optimal overlap of prefixes.
- Classic 2D DP.

### Visual cue
```text
if chars match -> diagonal + 1
else -> max(top, left)
```

### Python solution
```python
class Solution:
    def longestCommonSubsequence(self, text1: str, text2: str) -> int:
        m, n = len(text1), len(text2)
        dp = [[0] * (n + 1) for _ in range(m + 1)]

        for i in range(1, m + 1):
            for j in range(1, n + 1):
                if text1[i - 1] == text2[j - 1]:
                    dp[i][j] = dp[i - 1][j - 1] + 1
                else:
                    dp[i][j] = max(dp[i - 1][j], dp[i][j - 1])

        return dp[m][n]
```

### Alternate ways
- Memoization is often easier to derive.
- Tabulation makes transitions and dependencies more visible.

### Likely coding friction
- Confusing LCS with longest common substring.
- Wrong indexing on character comparisons.

### Complexity
- Time: `O(mn)`
- Space: `O(mn)`

---

## J3. LeetCode #516 — Longest Palindromic Subsequence
**Theme:** interval-style sequence DP  
**Difficulty:** Medium

### Problem brief
Return the length of the longest subsequence of a string that is also a palindrome.

### Recognition test
- Subsequence, not substring.
- DP over string intervals is natural.

### Visual cue
```text
if s[i] == s[j] -> 2 + dp[i+1][j-1]
else -> max(dp[i+1][j], dp[i][j-1])
```

### Python solution
```python
class Solution:
    def longestPalindromeSubseq(self, s: str) -> int:
        n = len(s)
        dp = [[0] * n for _ in range(n)]

        for i in range(n - 1, -1, -1):
            dp[i][i] = 1
            for j in range(i + 1, n):
                if s[i] == s[j]:
                    dp[i][j] = 2 + dp[i + 1][j - 1]
                else:
                    dp[i][j] = max(dp[i + 1][j], dp[i][j - 1])

        return dp[0][n - 1]
```

### Alternate ways
- Convert to LCS with reversed string, though direct interval DP is more insightful here.

### Likely coding friction
- Traversal order matters because smaller intervals must be ready first.
- Confusing subsequence with center-expansion substring techniques.

### Complexity
- Time: `O(n^2)`
- Space: `O(n^2)`

---

# Part K — DP on trees and DAGs

## K1. LeetCode #337 — House Robber III
**Theme:** tree DP with include/exclude states  
**Difficulty:** Medium

### Problem brief
Maximize money robbed from a binary tree of houses when directly linked parent-child houses cannot both be robbed.

### Recognition test
- Tree structure plus local dependency between parent and child.
- Need two states per node.

### Visual cue
```text
for each node return:
[rob_this, skip_this]
```

### Walkthrough
1. DFS each node.
2. `rob_this = node.val + skip(left) + skip(right)`.
3. `skip_this = max(left states) + max(right states)`.
4. Final answer is max of root states.

### Python solution
```python
class Solution:
    def rob(self, root) -> int:
        def dfs(node):
            if not node:
                return (0, 0)
            left = dfs(node.left)
            right = dfs(node.right)
            rob_this = node.val + left[1] + right[1]
            skip_this = max(left) + max(right)
            return (rob_this, skip_this)

        return max(dfs(root))
```

### Alternate ways
- Memoization by node and parent-taken state works too.
- Returning paired states is cleaner and faster.

### Likely coding friction
- Trying greedy local choice.
- Forgetting the difference between taking a node and taking the best below it.

### Complexity
- Time: `O(n)`
- Space: `O(h)`

---

## K2. LeetCode #329 — Longest Increasing Path in a Matrix
**Theme:** DAG DP on implicit graph  
**Difficulty:** Hard

### Problem brief
Find the longest path in a matrix where each move goes to a strictly larger neighboring value.

### Recognition test
- Strictly increasing edges mean no cycles.
- This is DFS + memo or DAG DP on an implicit graph.

### Visual cue
```text
cell -> larger neighbors only
acyclic because values strictly increase
memoize best path starting at each cell
```

### Python solution
```python
class Solution:
    def longestIncreasingPath(self, matrix: list[list[int]]) -> int:
        rows, cols = len(matrix), len(matrix[0])
        memo = [[0] * cols for _ in range(rows)]
        dirs = [(1, 0), (-1, 0), (0, 1), (0, -1)]

        def dfs(r, c):
            if memo[r][c]:
                return memo[r][c]
            best = 1
            for dr, dc in dirs:
                nr, nc = r + dr, c + dc
                if 0 <= nr < rows and 0 <= nc < cols and matrix[nr][nc] > matrix[r][c]:
                    best = max(best, 1 + dfs(nr, nc))
            memo[r][c] = best
            return best

        return max(dfs(r, c) for r in range(rows) for c in range(cols))
```

### Alternate ways
- Topological DP by outdegree is possible.
- DFS + memo is more natural for interviews.

### Likely coding friction
- Missing memoization and getting exponential recomputation.
- Treating the matrix as cyclic when strict increase prevents cycles.

### Complexity
- Time: `O(mn)`
- Space: `O(mn)`

---

# Part L — Bitmask and subset DP

## L1. LeetCode #698 — Partition to K Equal Sum Subsets
**Theme:** subset state with memoization  
**Difficulty:** Medium

### Problem brief
Determine whether the array can be partitioned into `k` subsets having equal sum.

### Recognition test
- Small `n`, subset membership matters.
- Bitmask is a compact representation of used elements.

### Visual cue
```text
mask bit 1 = element already used
state = which items are used + current bucket fill
```

### Python solution
```python
from functools import lru_cache

class Solution:
    def canPartitionKSubsets(self, nums: list[int], k: int) -> bool:
        total = sum(nums)
        if total % k:
            return False
        target = total // k
        nums.sort(reverse=True)
        if nums[0] > target:
            return False

        @lru_cache(None)
        def dfs(mask, curr_sum, buckets_done):
            if buckets_done == k - 1:
                return True
            if curr_sum == target:
                return dfs(mask, 0, buckets_done + 1)

            for i in range(len(nums)):
                if mask & (1 << i):
                    continue
                if curr_sum + nums[i] > target:
                    continue
                if dfs(mask | (1 << i), curr_sum + nums[i], buckets_done):
                    return True
            return False

        return dfs(0, 0, 0)
```

### Alternate ways
- Backtracking without memoization can work on small inputs but repeats states heavily.
- Bitmask memo is the stronger general solution.

### Likely coding friction
- State explosion from poor memo design.
- Forgetting symmetry pruning or sorting, which helps a lot.

### Complexity
- Exponential in worst case, but memoization prunes repeated subset states significantly.

---

## L2. LeetCode #847 — Shortest Path Visiting All Nodes
**Theme:** BFS over `(node, visited_mask)` state  
**Difficulty:** Hard

### Problem brief
Find the shortest number of edges needed to visit every node in an undirected graph.

### Recognition test
- Need shortest path in state space, not original graph alone.
- State must track current node and visited set.

### Visual cue
```text
state = (node, mask)
BFS because every edge has equal cost
first state hitting full_mask is optimal
```

### Python solution
```python
from collections import deque

class Solution:
    def shortestPathLength(self, graph: list[list[int]]) -> int:
        n = len(graph)
        full = (1 << n) - 1
        q = deque((i, 1 << i) for i in range(n))
        seen = {(i, 1 << i) for i in range(n)}
        steps = 0

        while q:
            for _ in range(len(q)):
                node, mask = q.popleft()
                if mask == full:
                    return steps
                for nei in graph[node]:
                    nxt = mask | (1 << nei)
                    state = (nei, nxt)
                    if state not in seen:
                        seen.add(state)
                        q.append(state)
            steps += 1
```

### Alternate ways
- DP over subsets is possible.
- BFS is especially elegant because all original edges cost the same.

### Likely coding friction
- Trying plain BFS on nodes without tracking visited set state.
- Starting from only one node instead of all nodes simultaneously.

### Complexity
- Time: `O(n * 2^n + E * 2^n)`
- Space: `O(n * 2^n)`

---

# Supplementary category notes

## Balanced BSTs and augmented trees
Phase 3 includes balanced BSTs and order-statistics as syllabus topics, but most interviews test **understanding and use-cases** more often than full AVL or Red-Black implementation. The most practical anchor problems here are BST validation, predecessor/successor reasoning, kth-smallest queries, and “why balance matters” discussion.  

## SCC and Bellman-Ford / Floyd-Warshall awareness
These topics matter for algorithmic breadth even when exact LeetCode frequency is lower than BFS, DFS, Dijkstra, and DSU. The practice table below includes representative problems so the syllabus remains complete.

---

# Phase 3 practice table
Use this as the deliberate-practice sheet before Phase 4.

| # | Problem | Theme | Category | Difficulty | Tags / keywords | Priority |
|---|---|---|---|---|---|---|
| 102 | Binary Tree Level Order Traversal | BFS by level | Trees | Medium | queue, level-order, traversal | Must |
| 94 | Binary Tree Inorder Traversal | Iterative traversal | Trees | Easy | stack, inorder, DFS | Must |
| 1448 | Count Good Nodes in Binary Tree | Path-state DFS | Trees | Medium | DFS, current-max, root-path | Should |
| 104 | Maximum Depth of Binary Tree | Height of tree | Trees | Easy | recursion, depth, BFS/DFS | Should |
| 226 | Invert Binary Tree | Mirror recursion | Trees | Easy | swap-children, DFS | Supplementary |
| 98 | Validate Binary Search Tree | Global bounds | BST | Medium | bounds, inorder, validity | Must |
| 235 | Lowest Common Ancestor of a BST | Ordered split point | BST | Medium | BST, ancestor, comparisons | Should |
| 230 | Kth Smallest Element in a BST | Inorder order-statistics | BST | Medium | sorted-order, iterative stack | Must |
| 700 | Search in a Binary Search Tree | Ordered traversal | BST | Easy | BST, search, iterative | Supplementary |
| 701 | Insert into a BST | Ordered insertion | BST | Medium | BST, recursion, insert | Supplementary |
| 543 | Diameter of Binary Tree | Height + answer update | Tree patterns | Easy | postorder, diameter, height | Must |
| 236 | Lowest Common Ancestor of a Binary Tree | Postorder discovery | Tree patterns | Medium | recursion, ancestor, split | Must |
| 124 | Binary Tree Maximum Path Sum | Tree DP | Tree patterns | Hard | path-gain, postorder, global-best | Must |
| 297 | Serialize and Deserialize Binary Tree | Tree encoding | Tree patterns | Hard | preorder, null-markers, codec | Should |
| 110 | Balanced Binary Tree | Height-balanced check | Tree patterns | Easy | prune, height, balance | Should |
| 112 | Path Sum | Root-to-leaf target | Tree patterns | Easy | DFS, accumulation | Supplementary |
| 200 | Number of Islands | Grid components | Graph traversal | Medium | DFS, BFS, components | Must |
| 994 | Rotting Oranges | Multi-source BFS | Graph traversal | Medium | BFS, layers, shortest-time | Must |
| 133 | Clone Graph | Graph copy via traversal | Graph traversal | Medium | DFS/BFS, map, graph-copy | Should |
| 207 | Course Schedule | Directed cycle detection | Graph / topo | Medium | DFS-coloring, DAG, cycle | Must |
| 210 | Course Schedule II | Topological sort | Graph / topo | Medium | indegree, queue, order | Must |
| 785 | Is Graph Bipartite? | Two-coloring | Graph connectivity | Medium | BFS, DFS, parity | Should |
| 1971 | Find if Path Exists in Graph | Reachability | Graph connectivity | Easy | BFS, DFS, components | Supplementary |
| 802 | Find Eventual Safe States | Reverse graph pruning | SCC awareness | Medium | outdegree, reverse-graph, safe-states | Should |
| 743 | Network Delay Time | Dijkstra | Shortest paths | Medium | heap, weighted-graph, relax | Must |
| 787 | Cheapest Flights Within K Stops | Bounded relaxations | Shortest paths | Medium | Bellman-Ford, stops, layered-relax | Must |
| 1334 | Find the City With the Smallest Number of Neighbors at a Threshold Distance | All-pairs shortest path | Shortest paths | Medium | Floyd-Warshall, matrix, threshold | Should |
| 1631 | Path With Minimum Effort | Dijkstra / binary search alt | Shortest paths | Medium | minimax-path, heap | Should |
| 1584 | Min Cost to Connect All Points | MST | MST | Medium | Prim, complete-graph, min-cost | Must |
| 1135 | Connecting Cities With Minimum Cost | Kruskal MST | MST | Medium | sort-edges, DSU, MST | Should |
| 684 | Redundant Connection | DSU cycle detect | DSU | Medium | union-find, cycle, components | Must |
| 1319 | Number of Operations to Make Network Connected | DSU components | DSU | Medium | components, spare-edges, union-find | Should |
| 547 | Number of Provinces | Component counting | DSU / graph | Medium | DFS, DSU, adjacency-matrix | Should |
| 70 | Climbing Stairs | Fibonacci DP | DP fundamentals | Easy | recurrence, rolling-state | Should |
| 198 | House Robber | Take / skip DP | 1D DP | Medium | include-exclude, rolling | Must |
| 213 | House Robber II | Circular DP | 1D DP | Medium | circular-array, split-cases | Should |
| 322 | Coin Change | Min-coin DP | 1D DP | Medium | complete-knapsack, min-count | Must |
| 416 | Partition Equal Subset Sum | 0/1 knapsack boolean DP | Knapsack | Medium | subset-sum, reverse-iterate | Must |
| 494 | Target Sum | Sign assignment DP | Knapsack / counting | Medium | subset-transform, counts | Should |
| 62 | Unique Paths | Counting grid DP | Grid DP | Medium | right-down, combinatorics | Should |
| 64 | Minimum Path Sum | Weighted grid DP | Grid DP | Medium | min-cost, grid-transition | Must |
| 63 | Unique Paths II | Grid DP with obstacles | Grid DP | Medium | blocked-cells, transitions | Should |
| 72 | Edit Distance | String transformation DP | 2D DP | Medium | insert-delete-replace, prefixes | Must |
| 115 | Distinct Subsequences | Count subsequence matches | 2D DP | Hard | counting, prefixes, subsequence | Should+ |
| 300 | Longest Increasing Subsequence | Sequence DP | Sequence DP | Medium | subsequence, LIS, patience-sort-alt | Must |
| 1143 | Longest Common Subsequence | Two-sequence DP | Sequence DP | Medium | prefixes, match/skip | Must |
| 516 | Longest Palindromic Subsequence | Interval DP | Sequence DP | Medium | palindrome, interval, subsequence | Should |
| 1218 | Longest Arithmetic Subsequence of Given Difference | Map DP | Sequence DP | Medium | hashmap-dp, progression | Supplementary |
| 337 | House Robber III | Tree DP | DP on trees | Medium | include-exclude, tree-state | Must |
| 329 | Longest Increasing Path in a Matrix | DAG DP on grid | DP on DAGs | Hard | memoized-DFS, acyclic, matrix | Must |
| 931 | Minimum Falling Path Sum | Grid / DAG style DP | DP on DAGs | Medium | top-down or bottom-up, matrix | Should |
| 698 | Partition to K Equal Sum Subsets | Bitmask memo | Bitmask DP | Medium | subsets, memo, mask | Should |
| 847 | Shortest Path Visiting All Nodes | State-space BFS | Bitmask / state compression | Hard | mask, BFS, full-visit | Should+ |
| 691 | Stickers to Spell Word | State compression DP | Mixed advanced DP | Hard | memo, mask, target-progress | Supplementary |

---

# Suggested execution blocks

## Block 1 — Week 7 trees
- #102 Binary Tree Level Order Traversal
- #94 Binary Tree Inorder Traversal
- #98 Validate Binary Search Tree
- #230 Kth Smallest Element in a BST
- #543 Diameter of Binary Tree
- #236 Lowest Common Ancestor of a Binary Tree
- #124 Binary Tree Maximum Path Sum

## Block 2 — Week 8 graph foundations
- #200 Number of Islands
- #994 Rotting Oranges
- #207 Course Schedule
- #210 Course Schedule II
- #785 Is Graph Bipartite?
- #802 Find Eventual Safe States

## Block 3 — Week 9 weighted graphs, MST, DSU
- #743 Network Delay Time
- #787 Cheapest Flights Within K Stops
- #1334 Find the City With the Smallest Number of Neighbors at a Threshold Distance
- #1584 Min Cost to Connect All Points
- #684 Redundant Connection
- #1319 Number of Operations to Make Network Connected

## Block 4 — Week 10 DP fundamentals
- #70 Climbing Stairs
- #198 House Robber
- #322 Coin Change
- #416 Partition Equal Subset Sum
- #62 Unique Paths
- #64 Minimum Path Sum
- #72 Edit Distance

## Block 5 — Week 10-11 sequence and advanced DP
- #300 Longest Increasing Subsequence
- #1143 Longest Common Subsequence
- #516 Longest Palindromic Subsequence
- #337 House Robber III
- #329 Longest Increasing Path in a Matrix
- #698 Partition to K Equal Sum Subsets
- #847 Shortest Path Visiting All Nodes

---

# Common failure patterns in Phase 3
- Treating trees and graphs as the same traversal problem without respecting direction, weight, or state.
- Using BFS on weighted shortest-path problems that require Dijkstra or Bellman-Ford style relaxations.
- Confusing MST with shortest path.
- Writing DP before defining the state meaning.
- Mixing subsequence and substring problems.
- Forgetting base cases or traversal order in 2D DP.
- Returning the wrong quantity in tree DP: upward contribution versus global optimum.
- Skipping disconnected components in graph problems.

---

# Phase 3 readiness checklist
You are ready for Phase 4 only if you can do the following reliably:
- Explain when BFS beats DFS and when weights require Dijkstra or Bellman-Ford.
- Distinguish shortest path from minimum spanning tree.
- Write a clean DSU with path compression and union by rank/size.
- Define DP state, transition, base case, and iteration order before coding.
- Solve one tree DP and one bitmask/state-compression problem with correct state narration.
- Explain why topological order makes DAG DP possible.
- Recognize when sequence DP uses prefixes, intervals, or ending-at-index states.

---

# Next phase preview
Phase 4 moves into algorithmic paradigms: **greedy, backtracking, branch and bound, and amortized analysis**.

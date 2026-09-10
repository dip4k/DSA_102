# Problem-Solving Curriculum 2.0: Queue & Deque Traversal Mastery

**Goal:** Build a professional-grade intuition for queue/deque-driven problem solving (Level 1 → Level 6).

## 📊 Summary Table: One-Page Level Mapping Index

| Level | Pattern | Mental Model | Pointer / Queue State | Drill Problems |
|---|---|---|---|---|
| **L1** | **FIFO Expiration** | Sliding window over time. Expire stale data. | **Front**: Oldest valid item. **Back**: Newest arrival. | LC 933, LC 346 |
| **L2** | **Stride Processing** | Synchronous wave expansion. Process layer by layer. | **Queue**: Contains exactly one generation of nodes at a time. | LC 102, LC 994 |
| **L3** | **Monotonic Deque** | Moving window candidate set. Weaker entries are dominated. | **Front**: Optimal window answer. Elements are strictly monotonic. | LC 239, LC 1438 |
| **L4** | **DP Frontier** | Sliding window optimization for Dynamic Programming. | **Deque**: Indices sorted by optimal DP transition value. | LC 1696, LC 1425 |
| **L5** | **Prefix-Sum Deque** | Dominance over prefix sums to handle negative values. | **Deque**: Indices strictly increasing by prefix sum value. | LC 862 |
| **L6** | **PQ Frontier** | Best-first expansion. Cost-driven traversal. | **Root**: Cheapest unvisited node. Skip stale duplicate entries. | LC 743, LC 778 |

---

## 🟢 Level 1: FIFO Buffering & Expiration

**Mental Model:** A time-based sliding window. Events arrive in chronological order; older events expire as time progresses.  
**What does the index mean?** The value stored is the timestamp (event occurrence).  
**What region is processed?** The valid window strictly defined as `[current_time - T, current_time]`.  
**The Invariant:** Every element currently inside the queue belongs to the valid time window.

### Visual State Transitions (LC 933: `T = 3000`)
| Step | Current Time | Queue State | Action / Invariant |
|---|---|---|---|
| 1 | `t=1` | `[1]` | Enqueue `1`. Valid window `[-2999, 1]`. |
| 2 | `t=100` | `[1, 100]` | Enqueue `100`. Valid `[-2900, 100]`. |
| 3 | `t=3001` | `[1, 100, 3001]` | Enqueue `3001`. Valid `[1, 3001]`. |
| 4 | `t=3002` | `[100, 3001, 3002]` | `t-3000 = 2`. `1 < 2` so `popleft()`. Invariant restored. |

### Code Implementation

Problem: Count the number of recent requests within a 3000ms sliding time window. We process events chronologically and discard any that fall outside this valid timeframe.

**Python:**
```python
from collections import deque

class RecentCounter:
    def __init__(self):
        # 1. Initialize queue to hold chronologically arriving timestamps
        self.q = deque()

    def ping(self, t: int) -> int:
        # 2. Append new event timestamp (newest arrival at the back)
        self.q.append(t)
        
        # 3. Slide window: pop elements from the front that are older than t - 3000
        # Invariant: Every element left in the queue belongs to [t - 3000, t]
        while self.q and self.q[0] < t - 3000:
            self.q.popleft()
            
        # 4. The queue size represents the number of valid recent events
        return len(self.q)
```

**C#:**
```csharp
using System.Collections.Generic;

public class RecentCounter {
    // 1. Initialize queue to hold chronologically arriving timestamps
    private Queue<int> q = new Queue<int>();

    public int Ping(int t) {
        // 2. Append new event timestamp (newest arrival at the back)
        q.Enqueue(t);
        
        // 3. Slide window: pop elements from the front that are older than t - 3000
        // Invariant: Every element left in the queue belongs to [t - 3000, t]
        while (q.Count > 0 && q.Peek() < t - 3000) {
            q.Dequeue();
        }
        
        // 4. The queue size represents the number of valid recent events
        return q.Count;
    }
}
```

### ⚠️ Gotchas & Pitfalls
- **Off-by-one window boundaries:** Confusing `<` vs `<=` when expiring stale items can accidentally pop valid boundary events.
- **Memory leaks in sparse events:** If no new events arrive to trigger the `ping`, stale items indefinitely linger in memory unless handled by a background cleaner.

### 🛠️ Drill Problems
- **Easy:** [LeetCode 933: Number of Recent Calls], [LeetCode 346: Moving Average from Data Stream]

---

## 🟡 Level 2: Stride Processing (Level Snapshots)

**Mental Model:** Radial expansion. Process all elements at depth $d$ before processing any at depth $d+1$.  
**What does the index mean?** Nodes are grouped strictly by layer (or "stride").  
**What region is processed?** Exactly `level_size` items at the front of the queue, captured at the start of the level.  
**The Invariant:** The inner loop strictly consumes only the nodes present at the start of the current level. New additions form the *next* level.

### Visual State Transitions (LC 102)
| Step | Layer Depth | Queue State (Start) | Queue State (End) | Action / Invariant |
|---|---|---|---|---|
| 1 | Depth 0 | `[root]` | `[L, R]` | `size=1`. Pop 1, push its children. |
| 2 | Depth 1 | `[L, R]` | `[LL, LR, RL, RR]`| `size=2`. Pop 2, push their children. |

### Code Implementation

Problem: Group nodes of a binary tree by their depth (level). We must process all nodes in the current layer simultaneously before moving to their children.

**Python:**
```python
def levelOrder(root):
    if not root: return []
    # 1. Initialize queue with the root (Layer 0)
    q = deque([root])
    ans = []
    
    while q:
        # 2. Freeze the size of the current level to avoid processing next-level nodes
        level_size = len(q) 
        current_level = []
        
        # 3. Process exactly 'level_size' nodes (one full stride)
        for _ in range(level_size):
            node = q.popleft()
            current_level.append(node.val)
            
            # 4. Enqueue the next generation (children) for the upcoming level
            if node.left: q.append(node.left)
            if node.right: q.append(node.right)
            
        # 5. Record the processed layer
        ans.append(current_level)
    return ans
```

**C#:**
```csharp
public IList<IList<int>> LevelOrder(TreeNode root) {
    var ans = new List<IList<int>>();
    if (root == null) return ans;
    
    // 1. Initialize queue with the root (Layer 0)
    var q = new Queue<TreeNode>();
    q.Enqueue(root);
    
    while (q.Count > 0) {
        // 2. Freeze the size of the current level to avoid processing next-level nodes
        int levelSize = q.Count; 
        var currentLevel = new List<int>();
        
        // 3. Process exactly 'levelSize' nodes (one full stride)
        for (int i = 0; i < levelSize; i++) {
            var node = q.Dequeue();
            currentLevel.Add(node.val);
            
            // 4. Enqueue the next generation (children) for the upcoming level
            if (node.left != null) q.Enqueue(node.left);
            if (node.right != null) q.Enqueue(node.right);
        }
        
        // 5. Record the processed layer
        ans.Add(currentLevel);
    }
    return ans;
}
```

### ⚠️ Gotchas & Pitfalls
- **Forgetting to freeze `level_size`:** Using `for _ in range(len(q)):` dynamically evaluates `len(q)` as the queue grows, causing an infinite loop.
- **Null node enqueues:** Pushing `None` to the queue and not handling it can lead to exceptions when popping in the next level.
- **Premature answer updates:** Processing logic at the wrong scope (inside the `for` loop instead of after it) leads to miscalculating per-level aggregates.

### 🛠️ Drill Problems
- **Medium:** [LeetCode 102: Binary Tree Level Order Traversal], [LeetCode 994: Rotting Oranges]

---

## 🟠 Level 3: Monotonic Deque (Sliding Windows)

**Mental Model:** The deque is a **candidate set** for the optimal answer in a moving window. Old or weak candidates are eliminated (dominance).  
**What does the index mean?** Deque stores **indices** of the original array, enabling precise expiration checks (`index <= i - k`).  
**What region is processed?** The sliding window `[i - k + 1, i]`.  
**The Invariant:** Values corresponding to deque indices are **strictly monotonic**. `dq.front()` is *always* the optimal candidate for the current window.

### Visual State Transitions (LC 239: `nums=[9, 5, 3, 7], k=3`)
| Step | Position `i` | Incoming Val | Deque (Indices) | Action / Invariant |
|---|---|---|---|---|
| 1 | `i=0` | `9` | `[0]` | Push `0`. Window max is `nums[0] = 9`. |
| 2 | `i=1` | `5` | `[0, 1]` | Push `1`. `5 < 9`, keeps monotonic decreasing. |
| 3 | `i=2` | `3` | `[0, 1, 2]` | Push `2`. Max is `nums[0] = 9`. |
| 4 | `i=3` | `7` | `[1, 3]` | Expire `0`. Pop `2` (`3 < 7`). Push `3`. Max is `nums[1] = 5`. |

### Code Implementation

Problem: Find the maximum value in every contiguous sliding window of size k. We maintain a deque of potential maximum candidates, discarding elements that are too old or outclassed by newer elements.

**Python:**
```python
def maxSlidingWindow(nums, k):
    # 1. Initialize deque to store array *indices*, not values, for precise expiration checks
    dq = deque() 
    ans = []
    
    for i in range(len(nums)):
        # 2. Expire stale indices: remove front if it falls outside the window [i - k + 1, i]
        if dq and dq[0] < i - k + 1:
            dq.popleft()
            
        # 3. Dominate weaker candidates: pop from back if elements are smaller/equal to current
        # Invariant: Deque values remain strictly monotonic decreasing
        while dq and nums[dq[-1]] <= nums[i]:
            dq.pop()
            
        # 4. Push current index as a new candidate
        dq.append(i)
        
        # 5. Record optimal answer (at the front) once the window reaches size k
        if i >= k - 1:
            ans.append(nums[dq[0]])
            
    return ans
```

**C#:**
```csharp
public int[] MaxSlidingWindow(int[] nums, int k) {
    // 1. Initialize LinkedList (as deque) to store array *indices* for precise expiration checks
    var dq = new LinkedList<int>(); 
    var ans = new int[nums.Length - k + 1];
    
    for (int i = 0; i < nums.Length; i++) {
        // 2. Expire stale indices: remove front if it falls outside the window [i - k + 1, i]
        if (dq.Count > 0 && dq.First.Value < i - k + 1)
            dq.RemoveFirst();
            
        // 3. Dominate weaker candidates: pop from back if elements are smaller/equal to current
        // Invariant: Deque values remain strictly monotonic decreasing
        while (dq.Count > 0 && nums[dq.Last.Value] <= nums[i])
            dq.RemoveLast();
            
        // 4. Push current index as a new candidate
        dq.AddLast(i);
        
        // 5. Record optimal answer (at the front) once the window reaches size k
        if (i >= k - 1)
            ans[i - k + 1] = nums[dq.First.Value];
    }
    return ans;
}
```

### ⚠️ Gotchas & Pitfalls
- **Storing values instead of indices:** Deques must store indices to properly evaluate `i - k + 1` expiration; storing raw values loses positional context.
- **Wrong dominance operator:** Using `<` instead of `<=` when popping weaker elements can cause duplicate maximums to pile up unnecessarily.
- **Accessing empty deques:** Failing to check `if dq:` before accessing `dq[0]` or `dq[-1]` causes runtime exceptions when the window collapses.

### 🛠️ Drill Problems
- **Medium:** [LeetCode 1438: Longest Continuous Subarray With Absolute Diff Less Than or Equal to Limit]
- **Hard:** [LeetCode 239: Sliding Window Maximum]

---

## 🔴 Level 4: Deque as a DP Frontier

**Mental Model:** Transitioning state dynamically by picking the best option from the previous $K$ valid DP states.  
**What does the index mean?** Deque stores indices of previously computed DP states.  
**What region is processed?** The previous valid $K$ states: `[i - k, i - 1]`.  
**The Invariant:** Deque entries are sorted strictly monotonically decreasing by their **DP transition value**, reducing the lookback transition time from $O(K)$ to $O(1)$.

### Visual State Transitions (LC 1696: `nums=[10, -5, -2], k=2`)
| Step | Position `i` | `nums[i]` | Deque (Indices) | Action / Invariant |
|---|---|---|---|---|
| 1 | `0` | `10` | `[0]` | `DP[0] = 10`. Max past state is `DP[0]`. |
| 2 | `1` | `-5` | `[0, 1]` | `DP[1] = 10 - 5 = 5`. Push `1`. |
| 3 | `2` | `-2` | `[0, 2]` | `DP[2] = 10 - 2 = 8`. `DP[1]` is dominated by `DP[2]`. Pop `1`, push `2`. |

### Code Implementation

Problem: Maximize your score by jumping at most k steps forward. We optimize the DP transition by storing past states in a monotonic deque, enabling O(1) lookbacks.

**Python:**
```python
def maxResult(nums, k):
    # 1. Initialize DP array and seed the deque with the base case index
    dp = [0] * len(nums)
    dp[0] = nums[0]
    dq = deque([0])
    
    for i in range(1, len(nums)):
        # 2. Expire states outside the valid lookback jump range [i - k, i - 1]
        if dq[0] < i - k:
            dq.popleft()
            
        # 3. Best previous transition is strictly at the front of the deque
        dp[i] = nums[i] + dp[dq[0]]
        
        # 4. Maintain monotonic decreasing deque based on *DP transition values*
        # Weaker prior DP states are dominated by the newly computed DP state
        while dq and dp[dq[-1]] <= dp[i]:
            dq.pop()
            
        # 5. Push current index as a valid state for future jumps
        dq.append(i)
        
    return dp[-1]
```

**C#:**
```csharp
public int MaxResult(int[] nums, int k) {
    // 1. Initialize DP array and seed the deque with the base case index
    int[] dp = new int[nums.Length];
    dp[0] = nums[0];
    var dq = new LinkedList<int>();
    dq.AddLast(0);
    
    for (int i = 1; i < nums.Length; i++) {
        // 2. Expire states outside the valid lookback jump range [i - k, i - 1]
        if (dq.First.Value < i - k)
            dq.RemoveFirst();
            
        // 3. Best previous transition is strictly at the front of the deque
        dp[i] = nums[i] + dp[dq.First.Value];
        
        // 4. Maintain monotonic decreasing deque based on *DP transition values*
        // Weaker prior DP states are dominated by the newly computed DP state
        while (dq.Count > 0 && dp[dq.Last.Value] <= dp[i])
            dq.RemoveLast();
            
        // 5. Push current index as a valid state for future jumps
        dq.AddLast(i);
    }
    return dp[nums.Length - 1];
}
```

### ⚠️ Gotchas & Pitfalls
- **Initializing DP constraints:** Forgetting to seed the deque with the base case index `0` before starting the loop will skip evaluating the first transition correctly.
- **Stale pop boundary:** Using `dq[0] < i - k` incorrectly instead of the accurate lookback boundary for DP.
- **Negative transitions:** Mistakenly dominating indices using raw array values instead of the cumulative `DP[i]` value, leading to sub-optimal state retention.

### 🛠️ Drill Problems
- **Medium:** [LeetCode 1696: Jump Game VI]
- **Hard:** [LeetCode 1425: Constrained Subsequence Sum]

---

## 🟣 Level 5: Prefix-Sum Deque (Dominance Reasoning)

**Mental Model:** Searching subarrays with negative numbers using a prefix sum array. We maintain dominance by dropping suboptimal prefix sums.  
**What does the index mean?** Indices correspond to positions in the computed prefix sum array `P`.  
**What region is processed?** We expand the window checking for the constraint `P[i] - P[dq.front()] >= K`.  
**The Invariant:** Prefix sum values at deque indices are strictly increasing. A higher previous prefix sum is strictly worse for future subtractions, and therefore dominated.

### Visual State Transitions (LC 862: `nums=[2, -1, 2], k=3`)
| Step | `i` | Pref Sum `P[i]` | Deque State | Action / Invariant |
|---|---|---|---|---|
| 1 | `0` | `0` (base) | `[0]` | Start state. |
| 2 | `1` | `2` | `[0, 1]` | `P[1] > P[0]`. Monotonic increasing intact. |
| 3 | `2` | `1` | `[0, 2]` | `P[2] <= P[1]`. `P[1]` is dominated. Pop `1`. Push `2`. |
| 4 | `3` | `3` | `[2, 3]` | `P[3] - P[0] >= 3`. Ans=3. Pop `0`. Push `3`. |

### Code Implementation

Problem: Find the shortest subarray with a sum of at least K, which may contain negative numbers. We use a strictly increasing monotonic deque of prefix sum indices to guarantee dominance.

**Python:**
```python
def shortestSubarray(nums, k):
    # 1. Compute prefix sums (1-indexed to handle subarray starting from index 0)
    P = [0] * (len(nums) + 1)
    for i in range(len(nums)): 
        P[i+1] = P[i] + nums[i]
    
    dq = deque()
    ans = float('inf')
    
    for i in range(len(P)):
        # 2. Answer & Shrink: Evaluate valid subarrays meeting sum constraint K
        # If valid, pop front because any longer subarray using this start index will be suboptimal length
        while dq and P[i] - P[dq[0]] >= k:
            ans = min(ans, i - dq.popleft())
            
        # 3. Maintain Monotonicity: Higher prefix sums are strictly worse for future subtractions
        # Pop back to ensure prefix sum values are strictly increasing
        while dq and P[i] <= P[dq[-1]]:
            dq.pop()
            
        # 4. Push current prefix sum index
        dq.append(i)
        
    return ans if ans != float('inf') else -1
```

**C#:**
```csharp
public int ShortestSubarray(int[] nums, int k) {
    // 1. Compute prefix sums (1-indexed to handle subarray starting from index 0)
    long[] P = new long[nums.Length + 1];
    for (int i = 0; i < nums.Length; i++) 
        P[i+1] = P[i] + nums[i];
        
    var dq = new LinkedList<int>();
    int ans = int.MaxValue;
    
    for (int i = 0; i < P.Length; i++) {
        // 2. Answer & Shrink: Evaluate valid subarrays meeting sum constraint K
        // If valid, pop front because any longer subarray using this start index will be suboptimal length
        while (dq.Count > 0 && P[i] - P[dq.First.Value] >= k) {
            ans = Math.Min(ans, i - dq.First.Value);
            dq.RemoveFirst();
        }
        
        // 3. Maintain Monotonicity: Higher prefix sums are strictly worse for future subtractions
        // Pop back to ensure prefix sum values are strictly increasing
        while (dq.Count > 0 && P[i] <= P[dq.Last.Value])
            dq.RemoveLast();
            
        // 4. Push current prefix sum index
        dq.AddLast(i);
    }
    return ans == int.MaxValue ? -1 : ans;
}
```

### ⚠️ Gotchas & Pitfalls
- **Missing prefix sum base case:** Forgetting to prepend `0` to the prefix sum array will cause subarrays starting from index `0` to be entirely missed.
- **Non-monotonic processing:** Evaluating `P[i] - P[dq[0]] >= k` but failing to subsequently `popleft()` means you keep an expanded, suboptimal length instead of the shortest.
- **Negative elements context:** Standard sliding windows fail here because adding negative numbers decreases the sum; only a strictly monotonic prefix-sum deque guarantees correctness.

### 🛠️ Drill Problems
- **Hard:** [LeetCode 862: Shortest Subarray with Sum at Least K]

---

## ⚫ Level 6: Priority Queue Frontier (Best-First)

**Mental Model:** Graph exploration driven directly by edge cost rather than hop count.  
**What does the index mean?** Represents a tuple containing `(accumulated_cost, state)`.  
**What region is processed?** The frontier node globally guaranteeing the cheapest path from origin.  
**The Invariant:** The root of the Priority Queue is strictly the cheapest unvisited node globally. Stale/duplicated entries with higher costs are aggressively skipped.

### Visual State Transitions (LC 743: Dijkstra)
| Step | PQ State `(cost, node)` | Action / Invariant |
|---|---|---|
| 1 | `[(0, src)]` | Pop `(0, src)`. Mark visited. |
| 2 | `[(2, A), (5, B)]` | Push neighbors of `src`. Min cost is `2`. |
| 3 | `[(4, B), (5, B)]` | Pop `A`. Push neighbor `B` with cost `4`. |
| 4 | `[(5, B)]` | Pop `(4, B)`. (Next pop of `(5, B)` will be skipped as stale). |

### Code Implementation

Problem: Determine how long it takes for a signal to reach all nodes in a network with varying travel times. We use a Priority Queue to always expand the cheapest known frontier path first (Dijkstra's Algorithm).

**Python:**
```python
import heapq

def networkDelayTime(times, n, k):
    # 1. Build adjacency list for the directed graph (node -> (neighbor, weight))
    adj = {i: [] for i in range(1, n+1)}
    for u, v, w in times:
        adj[u].append((v, w))
        
    # 2. Initialize PQ with (accumulated_cost, starting_node)
    pq = [(0, k)]
    dist = {}
    
    while pq:
        # 3. Pop strictly the cheapest unvisited node globally
        d, node = heapq.heappop(pq)
        
        # 4. Skip stale duplicate entries that have a higher cost
        if node in dist: continue
        
        # 5. Mark node as visited with its final minimum cost
        dist[node] = d
        
        # 6. Expand frontier to neighbors
        for nei, weight in adj[node]:
            if nei not in dist:
                heapq.heappush(pq, (d + weight, nei))
                
    # 7. Return maximum delay if all nodes reached, else -1
    return max(dist.values()) if len(dist) == n else -1
```

**C#:**
```csharp
using System.Collections.Generic;
using System;

public int NetworkDelayTime(int[][] times, int n, int k) {
    // 1. Build adjacency list for the directed graph (node -> (neighbor, weight))
    var adj = new Dictionary<int, List<(int v, int w)>>();
    for (int i = 1; i <= n; i++) adj[i] = new List<(int, int)>();
    foreach (var t in times) adj[t[0]].Add((t[1], t[2]));
    
    // 2. Initialize PriorityQueue with starting node and cost 0
    var pq = new PriorityQueue<int, int>();
    pq.Enqueue(k, 0);
    var dist = new Dictionary<int, int>();
    
    while (pq.Count > 0) {
        // 3. Pop strictly the cheapest unvisited node globally
        pq.TryDequeue(out int node, out int d);
        
        // 4. Skip stale duplicate entries that have a higher cost
        if (dist.ContainsKey(node)) continue;
        
        // 5. Mark node as visited with its final minimum cost
        dist[node] = d;
        
        // 6. Expand frontier to neighbors
        foreach (var (nei, weight) in adj[node]) {
            if (!dist.ContainsKey(nei)) {
                pq.Enqueue(nei, d + weight);
            }
        }
    }
    
    // 7. Return maximum delay if all nodes reached, else -1
    return dist.Count == n ? System.Linq.Enumerable.Max(dist.Values) : -1;
}
```

### ⚠️ Gotchas & Pitfalls
- **Skipping the stale check:** Failing to check `if node in dist:` after popping from the PQ causes nodes to be fully re-processed, destroying the $O(E \log V)$ time complexity.
- **Queueing nodes, not costs:** A PQ sorts by the first tuple element. Pushing `(node, cost)` instead of `(cost, node)` leads to arbitrary traversal instead of cheapest-first.
- **Updating in-place vs Re-pushing:** Many languages lack an efficient `decrease-key` operation. The standard approach is blindly re-pushing duplicate nodes with lower costs and letting the "stale check" handle the old ones.

### 🛠️ Drill Problems
- **Medium:** [LeetCode 743: Network Delay Time]
- **Hard:** [LeetCode 778: Swim in Rising Water]

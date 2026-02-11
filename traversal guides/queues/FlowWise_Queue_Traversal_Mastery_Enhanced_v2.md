# Flow‑Wise Queue Traversal Mastery (Enhanced v2)

**Goal:** Professional-grade intuition for queue/deque-driven problem solving (Level 1 → Level 6).

**Style contract:** Why / What / How / Where / When + step-flow + Python/C# snippets + common pitfalls + tips & tricks.

---

## One-page Level Mapping Index (Queues)
Use this to map a queue problem to the right level, pattern, and invariant.

Legend:
- **Invariant** = what must stay true after each enqueue/dequeue.
- **Queue** = FIFO frontier.
- **Deque** = double-ended queue for candidate sets.
- **PQ** = priority queue (generalized frontier).

| Level | Pattern / skill | Typical problems (examples) | Expected invariant (one-liner) |
|---|---|---|---|
| L1 | FIFO buffering + expiration | RecentCounter, moving time window | “Queue is in arrival order; expired items removed from front.” |
| L2 | Level snapshots (stride processing) | Tree level order, BFS minutes | “Process exactly `levelSize` items; new items are next wave.” |
| L3 | Monotonic deque (window max/min) | Sliding Window Max | “Deque stores indices of candidates; front is window answer.” |
| L4 | Deque as DP frontier (max/min over last k) | Jump Game VI | “Deque keeps best DP candidates in monotonic score order.” |
| L5 | Prefix-sum deque (hard) | Shortest Subarray ≥ K | “Prefix indices in deque are increasing by prefix sum; dominated removed.” |
| L6 | PQ frontier (best-first) | Dijkstra / best-first search | “Pop returns smallest tentative key; stale entries skipped.” |

---

## The ladder: complexity of state
- **Level 1:** FIFO semantics + expiration rules.
- **Level 2:** Freeze `levelSize` to avoid mixing waves.
- **Level 3:** Deque maintains candidates for a sliding window answer.
- **Level 4:** Deque optimizes DP transitions over a window.
- **Level 5:** Deque filters prefix sums (dominance reasoning).
- **Level 6:** Priority queue generalizes “next frontier by best key”.

---

## Baseline queue/deque snippets

### Python (Queue/Deque)
```python
from collections import deque
q = deque()

q.append(x)      # enqueue
x = q.popleft()  # dequeue
front = q[0]
empty = not q

q.appendleft(x)  # deque push front
x = q.pop()      # deque pop back
```

### C# (Queue)
```csharp
var q = new Queue<int>();
q.Enqueue(x);
int x2 = q.Dequeue();
int front = q.Peek();
bool empty = q.Count == 0;
```

### C# (Deque using LinkedList)
```csharp
var dq = new LinkedList<int>();

dq.AddLast(i);
int front = dq.First!.Value;
int back = dq.Last!.Value;

dq.RemoveFirst();
dq.RemoveLast();
```

### C# (.NET PriorityQueue)
```csharp
var pq = new PriorityQueue<int,int>();
pq.Enqueue(node, priority);
int u = pq.Dequeue();
```

---

# Level 1 — Physical Layer (FIFO basics)

## 1.1 Time-window queue (RecentCounter pattern)

**Why:** You only care about events in the last T time units.

**What:** Keep timestamps in a FIFO queue; drop expired from the front.

**How (step-flow):**
1) enqueue(t)
2) while q.front < t - T: dequeue
3) answer = q.size

**Where:** rate limiting, telemetry windows, “last 3000ms” type problems.

**When:** whenever “only the recent window matters”.

**Invariant:** Queue timestamps are increasing; all entries are within the valid window.

**Python (template)**
```python
from collections import deque

class RecentCounter:
    def __init__(self):
        self.q = deque()

    def ping(self, t: int) -> int:
        self.q.append(t)
        while self.q[0] < t - 3000:
            self.q.popleft()
        return len(self.q)
```

**C# (template)**
```csharp
public sealed class RecentCounter
{
    private readonly Queue<int> q = new();

    public int Ping(int t)
    {
        q.Enqueue(t);
        while (q.Count > 0 && q.Peek() < t - 3000)
            q.Dequeue();
        return q.Count;
    }
}
```

**Common pitfalls**
- Forgetting to expire before returning the answer.

**Tips & tricks**
- FIFO is perfect when time is monotone (timestamps increasing).

---

# Level 2 — Stride processing (level snapshots)

## 2.1 BFS “minutes/levels” pattern (freeze levelSize)

**Why:** Problems talk about steps, minutes, rounds, layers.

**What:** Process the current frontier fully before counting a step.

**How (step-flow):**
1) while q not empty:
2) levelSize = q.size
3) repeat levelSize times: pop one, push its children/next states
4) increment step after finishing the loop

**Invariant:** Nodes processed in this loop belong to the same level; enqueued nodes belong to the next.

**Python (template)**
```python
from collections import deque

def bfs_levels(starts, neighbors):
    q = deque(starts)
    steps = 0
    while q:
        levelSize = len(q)
        for _ in range(levelSize):
            u = q.popleft()
            for v in neighbors(u):
                q.append(v)
        steps += 1
    return steps
```

**C# (template)**
```csharp
public static int ProcessLevels<T>(IEnumerable<T> starts, Func<T,IEnumerable<T>> neighbors)
{
    var q = new Queue<T>(starts);
    int steps = 0;

    while (q.Count > 0)
    {
        int levelSize = q.Count;
        for (int i = 0; i < levelSize; i++)
        {
            var u = q.Dequeue();
            foreach (var v in neighbors(u))
                q.Enqueue(v);
        }
        steps++;
    }
    return steps;
}
```

**Common pitfalls**
- Using `for i in range(len(q))` without freezing size (mixes levels).
- Incrementing steps at the wrong moment (off-by-one).

**Tips & tricks**
- Say: “I freeze the level size so I don’t mix waves.”

---

# Level 3 — Monotonic deque (sliding windows)

## 3.1 Sliding Window Maximum

**Why:** Recomputing max for each window is too slow.

**What:** Deque holds indices of candidates in decreasing value order.

**How (step-flow):**
1) Expire dq.front if it’s out of window.
2) While dq not empty and nums[i] >= nums[dq.back], pop back.
3) Push i.
4) Answer is nums[dq.front].

**Invariant:** Indices increase; values decrease; front is the maximum.

**Python (template)**
```python
from collections import deque

def sliding_window_max(nums, k):
    dq = deque()  # indices
    out = []
    for i, x in enumerate(nums):
        while dq and dq[0] <= i - k:
            dq.popleft()
        while dq and nums[dq[-1]] <= x:
            dq.pop()
        dq.append(i)
        if i >= k - 1:
            out.append(nums[dq[0]])
    return out
```

**C# (template)**
```csharp
public static List<int> SlidingWindowMax(int[] nums, int k)
{
    var dq = new LinkedList<int>(); // indices
    var outp = new List<int>();

    for (int i = 0; i < nums.Length; i++)
    {
        while (dq.Count > 0 && dq.First!.Value <= i - k)
            dq.RemoveFirst();

        while (dq.Count > 0 && nums[dq.Last!.Value] <= nums[i])
            dq.RemoveLast();

        dq.AddLast(i);

        if (i >= k - 1)
            outp.Add(nums[dq.First!.Value]);
    }

    return outp;
}
```

**Common pitfalls**
- Storing values instead of indices (can’t expire old items).
- Expiring after reading the answer (window becomes incorrect).

**Tips & tricks**
- Deque is not “the window”; it is the *candidate set* for the window.

---

# Level 4 — Deque as a DP frontier

## 4.1 Jump Game VI pattern (max dp over last k)

**Why:** dp[i] depends on the best dp[j] in a sliding range.

**What:** Deque keeps candidate indices with decreasing dp value.

**How (step-flow):**
1) Expire indices < i-k.
2) dp[i] = nums[i] + dp[dq.front].
3) Pop back while dp[i] >= dp[dq.back].
4) Push i.

**Invariant:** Deque is decreasing by dp value; front is best valid index.

**Common pitfalls**
- Expiring too late (dp uses invalid j).

**Tips & tricks**
- This is “sliding window max” applied to dp values.

---

# Level 5 — Prefix-sum deque (hard)

## 5.1 Shortest subarray with sum ≥ K

**Why:** Negative numbers break two pointers; need dominance reasoning.

**What:** Use prefix sums P; want minimal i-j with P[i]-P[j] ≥ K.

**How (step-flow):**
1) While dq not empty and P[i]-P[dq.front] ≥ K: update ans, pop front.
2) While dq not empty and P[i] <= P[dq.back]: pop back (dominated).
3) Push i.

**Invariant:** Prefix sums at dq indices are strictly increasing.

**Python (template)**
```python
from collections import deque

def shortest_subarray_ge_k(nums, K):
    n = len(nums)
    P = [0]*(n+1)
    for i in range(n):
        P[i+1] = P[i] + nums[i]

    dq = deque()
    ans = n + 1

    for i in range(n+1):
        while dq and P[i] - P[dq[0]] >= K:
            ans = min(ans, i - dq.popleft())
        while dq and P[i] <= P[dq[-1]]:
            dq.pop()
        dq.append(i)

    return ans if ans <= n else -1
```

**Common pitfalls**
- Forgetting the dominated-prefix removal step (performance/correctness break).

**Tips & tricks**
- Think: “I only keep prefixes that could be the best left boundary for some future i.”

---

# Level 6 — Priority queue frontier (generalized queue)

## 6.1 When FIFO is not enough

**Why:** If “next to process” must be smallest cost/priority, FIFO fails.

**What:** Priority queue returns smallest key first; treat it as an ordered frontier.

**How (step-flow):**
1) Push (state, key).
2) Pop smallest key.
3) Skip stale entries.
4) Relax/generate neighbors.

**Invariant:** The popped entry has the smallest tentative key among queued entries (stale entries ignored).

**Tips & tricks**
- “Queue” is a frontier idea; FIFO is just the simplest ordering.

---

## Debug checklist (queues)
- What does the queue store (events/nodes/indices/prefix indices)?
- Did I expire old items before using front as the answer?
- For levels: did I freeze `levelSize`?
- For deques: are indices increasing and values (or dp/prefix) monotone?
- For PQ: do I skip stale entries?

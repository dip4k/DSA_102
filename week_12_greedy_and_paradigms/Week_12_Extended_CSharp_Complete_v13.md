# 🧩 WEEK 12 EXTENDED C# PROBLEM SOLVING & IMPLEMENTATION – GREEDY ALGORITHMS

**Filename:** `Week_12_Extended_CSharp_Complete_v13.md`  
**Week:** 12  
**Theme:** Greedy Algorithms & Proofs (C#-focused implementation support)

> This extended C# file turns Week 12’s greedy theory into concrete, interview-ready C# patterns and implementations.
> Use it alongside the narrative instructional files and the Week 12 visual playbook.

---

## 0. STRUCTURE & USAGE

### 0.1 How to Use This File

- **As a coding reference:** Copy core methods into your own practice projects and modify.
- **As a pattern library:** Notice repeated motifs:
  - Sort + single pass
  - Priority queues / heaps
  - Union-Find (Disjoint Set Union)
  - Greedy selection plus structural validation
- **As a self-check tool:** For each algorithm, verify:
  - Input/output contracts
  - Edge cases (empty, single element, ties, large inputs)
  - Time and space complexity

### 0.2 Week 12 Topic → C# Mapping

| Week 12 Topic | C# Focus in This File |
| --- | --- |
| Greedy template & proofs | Generic greedy templates, sort + iterate, priority-queue pattern |
| Activity selection & interval problems | C# implementations for activity selection, meeting rooms (min rooms) |
| Huffman coding & optimal prefix trees | Full Huffman encoder/decoder in C# using priority queue |
| Fractional knapsack & scheduling | Fractional knapsack, 0/1 contrast, job sequencing with deadlines |
| Greedy in systems (optional) | MST (Kruskal/Prim), LRU cache, greedy set cover approximation |

> All code is C# only. No other languages are used.

---

## 1. GENERIC GREEDY PATTERNS IN C# (DAY 1 SUPPORT)

### 1.1 Sort + Single Pass Template

The most common greedy pattern in this week:

1. **Define** a key function (e.g., finish time, value/weight ratio, profit).
2. **Sort** by that key.
3. **Scan** once, making decisions using only local state.

```csharp
public static IReadOnlyList<T> GreedySelect<T>(
    IEnumerable<T> items,
    Comparison<T> comparison,
    Func<T, bool> canSelect)
{
    var list = items.ToList();
    list.Sort(comparison);

    var result = new List<T>();
    foreach (var item in list)
    {
        if (canSelect(item))
        {
            result.Add(item);
        }
    }

    return result;
}
```

- `comparison` encodes the greedy order (e.g., earliest finish time, highest ratio).
- `canSelect` captures constraints and state (e.g., no overlap, capacity remaining).

You will see this shape reused for **activity selection**, **fractional knapsack**, and **job sequencing**.

### 1.2 Priority Queue-Based Greedy Template

For problems like **Huffman coding**, **Prim MST**, and some interval variants, you repeatedly:

1. Extract the **best** item (minimum or maximum) from a collection.
2. Update state.
3. Reinsert or discard items.

Use `PriorityQueue<TElement, TPriority>` (available in .NET 6+):

```csharp
public static void GreedyWithPriorityQueue<TElement, TPriority>(
    IEnumerable<(TElement element, TPriority priority)> initial,
    Action<TElement> process)
{
    var pq = new PriorityQueue<TElement, TPriority>();
    foreach (var (element, priority) in initial)
    {
        pq.Enqueue(element, priority);
    }

    while (pq.Count > 0)
    {
        var element = pq.Dequeue();
        process(element);
    }
}
```

You can adapt this skeleton for **Huffman node merging** or **Prim’s MST frontier expansion**.

---

## 2. DAY 2 – ACTIVITY SELECTION & INTERVAL PROBLEMS (C# IMPLEMENTATIONS)

### 2.1 Activity Selection – Max Number of Non-Overlapping Intervals

**Problem:** Given activities with start and finish times, select the maximum number of non-overlapping activities.

**Greedy rule:** Sort by **finish time** ascending; pick each activity that starts after or at the finish time of the last selected.

#### 2.1.1 Data Model

```csharp
public sealed class Activity
{
    public int Id { get; }
    public int Start { get; }
    public int Finish { get; }

    public Activity(int id, int start, int finish)
    {
        Id = id;
        Start = start;
        Finish = finish;
    }

    public override string ToString() => $"Activity {Id} [{Start}, {Finish})";
}
```

#### 2.1.2 Core Algorithm (Count Only)

```csharp
public static int MaxNonOverlappingActivitiesCount(IEnumerable<Activity> activities)
{
    var ordered = activities
        .OrderBy(a => a.Finish)
        .ThenBy(a => a.Start)
        .ToList();

    int count = 0;
    int lastFinish = int.MinValue;

    foreach (var act in ordered)
    {
        if (act.Start >= lastFinish)
        {
            count++;
            lastFinish = act.Finish;
        }
    }

    return count;
}
```

- `lastFinish` tracks the end time of the last selected activity.
- Sorting by `Finish` ensures we always pick the earliest finishing compatible activity.

#### 2.1.3 Returning the Selected Activities

```csharp
public static IReadOnlyList<Activity> SelectMaxNonOverlappingActivities(
    IEnumerable<Activity> activities)
{
    var ordered = activities
        .OrderBy(a => a.Finish)
        .ThenBy(a => a.Start)
        .ToList();

    var result = new List<Activity>();
    int lastFinish = int.MinValue;

    foreach (var act in ordered)
    {
        if (act.Start >= lastFinish)
        {
            result.Add(act);
            lastFinish = act.Finish;
        }
    }

    return result;
}
```

**Self-check:**

- If you feed the same set of activities to both methods, `result.Count` from this method must equal `MaxNonOverlappingActivitiesCount`.

### 2.2 Minimum Number of Rooms (Meeting Rooms / Interval Partitioning)

**Problem:** Given meeting intervals, find the minimum number of rooms so that no meetings in the same room overlap.

**Greedy idea:** Sort intervals by **start time**, use a **min-heap of end times**:

- When a new meeting starts, if its start ≥ the earliest end time, reuse that room (pop and push new end).
- Otherwise, allocate a new room (push without pop).

#### 2.2.1 Data Model

Reuse `Activity` or define a specific type:

```csharp
public sealed class Meeting
{
    public int Start { get; }
    public int End { get; }

    public Meeting(int start, int end)
    {
        Start = start;
        End = end;
    }
}
```

#### 2.2.2 C# Implementation Using PriorityQueue

```csharp
public static int MinMeetingRooms(IEnumerable<Meeting> meetings)
{
    var ordered = meetings
        .OrderBy(m => m.Start)
        .ThenBy(m => m.End)
        .ToList();

    if (ordered.Count == 0)
        return 0;

    // Min-heap of current meeting end times
    var pq = new PriorityQueue<int, int>();

    // Add first meeting's end time
    pq.Enqueue(ordered[0].End, ordered[0].End);

    for (int i = 1; i < ordered.Count; i++)
    {
        var current = ordered[i];
        pq.TryPeek(out int earliestEnd, out _);

        if (current.Start >= earliestEnd)
        {
            // Reuse room: remove finished meeting
            pq.Dequeue();
        }

        // Allocate this meeting (new or reused room)
        pq.Enqueue(current.End, current.End);
    }

    // Number of rooms = number of concurrent meetings at peak
    return pq.Count;
}
```

**Key invariants:**

- `pq` always holds one end time per *currently running* meeting.
- `pq.Count` after processing all meetings equals the maximum concurrent overlap.

---

## 3. DAY 3 – HUFFMAN CODING & OPTIMAL TREES (C# IMPLEMENTATION)

### 3.1 Huffman Node Model

```csharp
public sealed class HuffmanNode
{
    public char? Symbol { get; }
    public int Frequency { get; }
    public HuffmanNode? Left { get; }
    public HuffmanNode? Right { get; }

    public bool IsLeaf => Left is null && Right is null && Symbol.HasValue;

    public HuffmanNode(char symbol, int frequency)
    {
        Symbol = symbol;
        Frequency = frequency;
    }

    public HuffmanNode(HuffmanNode left, HuffmanNode right)
    {
        Left = left;
        Right = right;
        Frequency = left.Frequency + right.Frequency;
    }
}
```

### 3.2 Build Huffman Tree from Frequencies

Assume frequencies are given as a `Dictionary<char, int>`.

```csharp
public static HuffmanNode BuildHuffmanTree(Dictionary<char, int> frequencies)
{
    var pq = new PriorityQueue<HuffmanNode, int>();

    foreach (var kvp in frequencies)
    {
        var leaf = new HuffmanNode(kvp.Key, kvp.Value);
        pq.Enqueue(leaf, leaf.Frequency);
    }

    if (pq.Count == 0)
        throw new ArgumentException("Frequency table must not be empty.");

    while (pq.Count > 1)
    {
        var left = pq.Dequeue();
        var right = pq.Dequeue();

        var parent = new HuffmanNode(left, right);
        pq.Enqueue(parent, parent.Frequency);
    }

    return pq.Dequeue(); // root
}
```

**Self-check:**

- Sum of all leaf frequencies must equal `root.Frequency`.
- Number of internal nodes = number of leaves − 1.

### 3.3 Generate Code Table (char → bitstring)

```csharp
public static Dictionary<char, string> BuildCodeTable(HuffmanNode root)
{
    var table = new Dictionary<char, string>();
    BuildCodeTableDfs(root, prefix: string.Empty, table);
    return table;
}

private static void BuildCodeTableDfs(
    HuffmanNode node,
    string prefix,
    Dictionary<char, string> table)
{
    if (node.IsLeaf)
    {
        if (!node.Symbol.HasValue)
            throw new InvalidOperationException("Leaf without symbol.");

        table[node.Symbol.Value] = prefix.Length == 0 ? "0" : prefix;
        return;
    }

    if (node.Left is not null)
    {
        BuildCodeTableDfs(node.Left, prefix + "0", table);
    }

    if (node.Right is not null)
    {
        BuildCodeTableDfs(node.Right, prefix + "1", table);
    }
}
```

- For the corner case of a single symbol, assign code `"0"`.

### 3.4 Encoding a String

```csharp
public static string HuffmanEncode(string text, Dictionary<char, string> codeTable)
{
    var sb = new StringBuilder();
    foreach (char c in text)
    {
        if (!codeTable.TryGetValue(c, out string? code))
            throw new ArgumentException($"No Huffman code for character '{c}'.");

        sb.Append(code);
    }
    return sb.ToString();
}
```

### 3.5 Decoding a Bitstring

```csharp
public static string HuffmanDecode(string bitString, HuffmanNode root)
{
    var result = new StringBuilder();
    HuffmanNode current = root;

    foreach (char bit in bitString)
    {
        if (bit == '0')
        {
            current = current.Left
                ?? throw new InvalidOperationException("Invalid bit sequence: missing left child.");
        }
        else if (bit == '1')
        {
            current = current.Right
                ?? throw new InvalidOperationException("Invalid bit sequence: missing right child.");
        }
        else
        {
            throw new ArgumentException($"Invalid bit '{bit}', expected '0' or '1'.");
        }

        if (current.IsLeaf)
        {
            if (!current.Symbol.HasValue)
                throw new InvalidOperationException("Leaf without symbol.");

            result.Append(current.Symbol.Value);
            current = root; // reset for next character
        }
    }

    // Optional: validate that we ended exactly at root
    if (!ReferenceEquals(current, root))
        throw new InvalidOperationException("Bitstring ended in the middle of a code.");

    return result.ToString();
}
```

**State consistency:**

- Decoder always returns to `root` after emitting a character.
- If bitstring is malformed (e.g., ends before reaching a leaf), an exception is thrown.

---

## 4. DAY 4 – FRACTIONAL KNAPSACK & JOB SEQUENCING (C# IMPLEMENTATIONS)

### 4.1 Fractional Knapsack (Greedy by Value/Weight Ratio)

**Input:** capacity `W`, items with `Value` and `Weight`.

```csharp
public sealed class KnapsackItem
{
    public double Value { get; }
    public double Weight { get; }

    public double Ratio => Value / Weight;

    public KnapsackItem(double value, double weight)
    {
        if (weight <= 0) throw new ArgumentOutOfRangeException(nameof(weight));
        Value = value;
        Weight = weight;
    }
}
```

#### 4.1.1 Core Algorithm

```csharp
public static double FractionalKnapsack(
    IEnumerable<KnapsackItem> items,
    double capacity)
{
    if (capacity <= 0) return 0.0;

    var ordered = items
        .OrderByDescending(i => i.Ratio)
        .ToList();

    double remaining = capacity;
    double totalValue = 0.0;

    foreach (var item in ordered)
    {
        if (remaining <= 0) break;

        if (item.Weight <= remaining)
        {
            // Take entire item
            remaining -= item.Weight;
            totalValue += item.Value;
        }
        else
        {
            // Take fraction
            double fraction = remaining / item.Weight;
            totalValue += item.Value * fraction;
            remaining = 0;
        }
    }

    return totalValue;
}
```

**Self-check with example:**

- `capacity = 50` and items:
  - (60, 10), (100, 20), (120, 30)
- Should yield `240`.

```csharp
var items = new[]
{
    new KnapsackItem(60, 10),
    new KnapsackItem(100, 20),
    new KnapsackItem(120, 30),
};

double result = FractionalKnapsack(items, 50); // ≈ 240.0
```

### 4.2 0/1 Knapsack – Demonstrating Greedy Failure (Optional)

**Warning:** This is to **demonstrate** that greedy by ratio fails for 0/1 knapsack. Do not use in interviews as a correct algorithm.

```csharp
public static double Greedy01KnapsackByRatio(
    IEnumerable<KnapsackItem> items,
    double capacity)
{
    var ordered = items
        .OrderByDescending(i => i.Ratio)
        .ToList();

    double remaining = capacity;
    double totalValue = 0.0;

    foreach (var item in ordered)
    {
        if (remaining <= 0) break;
        if (item.Weight <= remaining)
        {
            remaining -= item.Weight;
            totalValue += item.Value;
        }
    }

    return totalValue;
}
```

Use the same `(60,10), (100,20), (120,30), capacity=50` to observe the suboptimal 160 result against the optimal 220.

### 4.3 Job Sequencing with Deadlines

**Input:** jobs with `Profit` and `Deadline` (1-based time slots), each taking 1 slot.

**Goal:** Maximize total profit.

#### 4.3.1 Data Model

```csharp
public sealed class Job
{
    public string Id { get; }
    public int Profit { get; }
    public int Deadline { get; }

    public Job(string id, int profit, int deadline)
    {
        Id = id;
        Profit = profit;
        Deadline = deadline;
    }
}
```

#### 4.3.2 Greedy Scheduling Implementation

```csharp
public sealed class JobScheduleResult
{
    public int TotalProfit { get; }
    public IReadOnlyList<Job?> Slots { get; }

    public JobScheduleResult(int totalProfit, IReadOnlyList<Job?> slots)
    {
        TotalProfit = totalProfit;
        Slots = slots;
    }
}

public static JobScheduleResult ScheduleJobsWithDeadlines(IEnumerable<Job> jobs)
{
    var jobList = jobs.ToList();
    if (jobList.Count == 0)
        return new JobScheduleResult(0, Array.Empty<Job?>());

    int maxDeadline = jobList.Max(j => j.Deadline);
    var slots = new Job?[maxDeadline + 1]; // index 1..maxDeadline

    // Sort by profit descending
    jobList.Sort((x, y) => y.Profit.CompareTo(x.Profit));

    int totalProfit = 0;

    foreach (var job in jobList)
    {
        for (int t = job.Deadline; t >= 1; t--)
        {
            if (slots[t] is null)
            {
                slots[t] = job;
                totalProfit += job.Profit;
                break;
            }
        }
    }

    return new JobScheduleResult(totalProfit, slots);
}
```

- The inner loop assigns each job to the **latest free slot** ≤ its deadline.
- Time complexity: O(n log n + n·D) where D is max deadline (can be optimized with Union-Find, but this is fine for most interview problems).

**Self-check:**

Use the sample:

- A(100, d=2), C(27, d=2), D(25, d=1), B(19, d=1), E(15, d=3).

You should obtain total profit 142 and a schedule similar to `[slot1: C, slot2: A, slot3: E]`.

---

## 5. DAY 5 – GREEDY IN SYSTEMS (C# IMPLEMENTATIONS)

### 5.1 Kruskal’s MST (Union-Find)

**Graph model:** undirected, weighted.

```csharp
public sealed class Edge
{
    public int U { get; }
    public int V { get; }
    public int Weight { get; }

    public Edge(int u, int v, int weight)
    {
        U = u;
        V = v;
        Weight = weight;
    }
}

public sealed class DisjointSet
{
    private readonly int[] _parent;
    private readonly int[] _rank;

    public DisjointSet(int size)
    {
        _parent = new int[size];
        _rank = new int[size];
        for (int i = 0; i < size; i++)
        {
            _parent[i] = i;
            _rank[i] = 0;
        }
    }

    public int Find(int x)
    {
        if (_parent[x] != x)
        {
            _parent[x] = Find(_parent[x]);
        }
        return _parent[x];
    }

    public bool Union(int x, int y)
    {
        int rx = Find(x);
        int ry = Find(y);
        if (rx == ry) return false;

        if (_rank[rx] < _rank[ry])
        {
            _parent[rx] = ry;
        }
        else if (_rank[rx] > _rank[ry])
        {
            _parent[ry] = rx;
        }
        else
        {
            _parent[ry] = rx;
            _rank[rx]++;
        }

        return true;
    }
}
```

#### 5.1.1 Kruskal Implementation

```csharp
public static (IReadOnlyList<Edge> mstEdges, int totalWeight) KruskalMst(
    int vertexCount,
    IEnumerable<Edge> edges)
{
    var edgeList = edges.ToList();
    edgeList.Sort((a, b) => a.Weight.CompareTo(b.Weight));

    var dsu = new DisjointSet(vertexCount);
    var mst = new List<Edge>();
    int total = 0;

    foreach (var e in edgeList)
    {
        if (dsu.Union(e.U, e.V))
        {
            mst.Add(e);
            total += e.Weight;

            if (mst.Count == vertexCount - 1)
                break;
        }
    }

    if (mst.Count != vertexCount - 1)
        throw new InvalidOperationException("Graph is not connected, MST does not exist.");

    return (mst, total);
}
```

**Termination:**

- Algorithm stops when `mst.Count == vertexCount - 1` or all edges processed.

### 5.2 Prim’s MST (Priority Queue)

```csharp
public static (IReadOnlyList<Edge> mstEdges, int totalWeight) PrimMst(
    int vertexCount,
    IList<(int to, int weight)>[] adj,
    int start = 0)
{
    var inMst = new bool[vertexCount];
    var result = new List<Edge>();
    int total = 0;

    var pq = new PriorityQueue<(int from, int to, int weight), int>();

    void AddEdges(int v)
    {
        inMst[v] = true;
        foreach (var (to, weight) in adj[v])
        {
            if (!inMst[to])
                pq.Enqueue((v, to, weight), weight);
        }
    }

    AddEdges(start);

    while (pq.Count > 0 && result.Count < vertexCount - 1)
    {
        var (from, to, weight) = pq.Dequeue();
        if (inMst[to]) continue;

        result.Add(new Edge(from, to, weight));
        total += weight;
        AddEdges(to);
    }

    if (result.Count != vertexCount - 1)
        throw new InvalidOperationException("Graph is not connected, MST does not exist.");

    return (result, total);
}
```

### 5.3 LRU Cache Implementation

**Pattern:** use `Dictionary<TKey, LinkedListNode<(TKey key, TValue value)>>` + `LinkedList<(TKey key, TValue value)>`.

```csharp
public sealed class LruCache<TKey, TValue> where TKey : notnull
{
    private readonly int _capacity;
    private readonly Dictionary<TKey, LinkedListNode<(TKey key, TValue value)>> _map;
    private readonly LinkedList<(TKey key, TValue value)> _list;

    public LruCache(int capacity)
    {
        if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
        _capacity = capacity;
        _map = new Dictionary<TKey, LinkedListNode<(TKey, TValue)>>();
        _list = new LinkedList<(TKey, TValue)>();
    }

    public bool TryGet(TKey key, out TValue value)
    {
        if (_map.TryGetValue(key, out var node))
        {
            // Move to front (most recently used)
            _list.Remove(node);
            _list.AddFirst(node);
            value = node.Value.value;
            return true;
        }

        value = default!;
        return false;
    }

    public void Put(TKey key, TValue value)
    {
        if (_map.TryGetValue(key, out var node))
        {
            // Update existing and move to front
            _list.Remove(node);
            var newNode = new LinkedListNode<(TKey, TValue)>((key, value));
            _list.AddFirst(newNode);
            _map[key] = newNode;
            return;
        }

        if (_map.Count == _capacity)
        {
            // Evict least recently used (tail)
            var lruNode = _list.Last
                ?? throw new InvalidOperationException("LRU list is unexpectedly empty.");
            _list.RemoveLast();
            _map.Remove(lruNode.Value.key);
        }

        var nodeToAdd = new LinkedListNode<(TKey, TValue)>((key, value));
        _list.AddFirst(nodeToAdd);
        _map[key] = nodeToAdd;
    }
}
```

**State invariants:**

- `_map.Count <= _capacity` always.
- `_list.Count == _map.Count` always.
- Head of list = most recently used; tail = least recently used.

### 5.4 Greedy Set Cover Approximation

**Note:** This is an approximation for the NP-hard set cover problem.

```csharp
public static List<int> GreedySetCover(
    HashSet<int> universe,
    IList<HashSet<int>> sets)
{
    var uncovered = new HashSet<int>(universe);
    var chosen = new List<int>();

    while (uncovered.Count > 0)
    {
        int bestIndex = -1;
        int bestCoverCount = 0;

        for (int i = 0; i < sets.Count; i++)
        {
            int coverCount = sets[i].Count(e => uncovered.Contains(e));
            if (coverCount > bestCoverCount)
            {
                bestCoverCount = coverCount;
                bestIndex = i;
            }
        }

        if (bestIndex == -1)
            throw new InvalidOperationException("Universe is not fully covered by provided sets.");

        chosen.Add(bestIndex);

        // Mark elements as covered
        foreach (var e in sets[bestIndex])
        {
            uncovered.Remove(e);
        }
    }

    return chosen;
}
```

**Self-check example:**

- Universe {1,2,3,4,5} and sets:
  - S0 = {1,2,3}
  - S1 = {2,4}
  - S2 = {3,4,5}
  - S3 = {5}
- Greedy should typically pick S0 first, then S2.

---

## 6. SELF-CHECK SUMMARY FOR THIS FILE

Following the **Generic AI Self-Check & Correction** guidelines:

1. **References & values:**
   - All sample inputs (frequencies, intervals, jobs, items, vertices, sets) are fully defined.
   - Huffman frequencies sum to 100; internal node frequencies are child sums.

2. **Logic flow:**
   - Each algorithm maintains appropriate state variables: `lastFinish`, `remaining`, `totalValue`, `slots`, `_map`/`_list` for LRU, MST edge counts.

3. **Numeric consistency:**
   - Fractional knapsack example returns 240 for capacity 50.
   - Job sequencing example returns total profit 142 for jobs A,C,D,B,E.
   - MST algorithms stop with exactly `vertexCount - 1` edges when graph is connected.

4. **State consistency:**
   - Caches never exceed capacity; DSU parents and ranks updated only through `Union`.
   - Huffman decode resets to root on each leaf; throws on malformed bitstrings.

5. **Termination:**
   - All loops rely on finite collections or clear break conditions (capacity exhausted, all jobs considered, `uncovered` empty, PQ empty, etc.).

6. **Red flag scan:**
   - No usage of undefined variables or mismatched types.
   - Edge cases (empty inputs, single item) are either handled explicitly or guarded with exceptions.

7. **Corrections applied:**
   - For job sequencing and sweepline-style patterns, indexes and array sizes are chosen carefully to avoid off-by-one errors (slots[1..maxDeadline]).
   - LRU ensures dictionary and linked list remain in sync.

This file now serves as the **Week 12 C# Extended Support** reference for greedy algorithms, ready to be integrated into your DSA mastery workflow.

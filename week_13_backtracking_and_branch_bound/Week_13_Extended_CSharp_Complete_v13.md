# Week 13 Extended CSharp Complete v13

Purpose: provide C# implementation support for backtracking, branch and bound, amortized analysis, and mixed paradigm reasoning.

## Focus tags
- Must: backtracking template, visited/state restoration, branch-and-bound search structure, amortized-analysis articulation
- Should: best-first B&B with priority queue, permutation/combination frameworks
- Optional: richer optimization examples

## Pattern 1: generic backtracking template
```csharp
public static void Backtrack<TState>(
    TState state,
    Func<TState, bool> isComplete,
    Func<TState, IEnumerable<TState>> nextStates,
    Action<TState> record)
{
    if (isComplete(state))
    {
        record(state);
        return;
    }

    foreach (var next in nextStates(state))
    {
        Backtrack(next, isComplete, nextStates, record);
    }
}
```

## Pattern 2: permutations with used array
```csharp
public static IList<IList<int>> Permute(int[] nums)
{
    var result = new List<IList<int>>();
    var path = new List<int>();
    var used = new bool[nums.Length];

    void Dfs()
    {
        if (path.Count == nums.Length)
        {
            result.Add(path.ToList());
            return;
        }

        for (int i = 0; i < nums.Length; i++)
        {
            if (used[i]) continue;
            used[i] = true;
            path.Add(nums[i]);
            Dfs();
            path.RemoveAt(path.Count - 1);
            used[i] = false;
        }
    }

    Dfs();
    return result;
}
```

## Pattern 3: branch and bound node skeleton
```csharp
public sealed class BbNode
{
    public int Level { get; init; }
    public int Value { get; init; }
    public int Weight { get; init; }
    public double Bound { get; init; }
}
```

Best-first idea:
- maintain a `PriorityQueue<BbNode, double>` keyed by most promising bound
- prune nodes whose bound cannot beat the current best

## Pattern 4: amortized-analysis talking points
- Dynamic array append is expensive occasionally but cheap on average over many operations.
- Union-Find and splay-like structures are judged over sequences, not isolated worst-case calls.

## Practice ladder
- Must: subsets/permutations/combinations, N-Queens template, word search backtracking, branch-and-bound knapsack reasoning
- Should: best-first B&B trace with bounds, Sudoku backtracking structure
- Optional: TSP lower-bound exploration and mixed-paradigm comparisons

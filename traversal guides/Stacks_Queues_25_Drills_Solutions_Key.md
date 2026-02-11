# Solutions Key — 25 Stack + Queue Drills (C#)

This is a **drop-in replacement** for the `Drills` class in your driver file (`Stacks_Queues_Checklist_25_Drills_With_Driver_No_Solutions.md`).

## How to use
1) Open your existing `Program.cs`.
2) Replace the entire `public static class Drills { ... }` block with the code below.
3) Run: `dotnet run`.

---

```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public static class Drills
{
    // ========== D1: ArrayStack<T> ==========
    public sealed class ArrayStack<T>
    {
        private T[] _a = new T[4];
        private int _n;

        public int Count => _n;

        public bool IsEmpty() => _n == 0;

        public void Push(T x)
        {
            if (_n == _a.Length)
            {
                var b = new T[_a.Length * 2];
                Array.Copy(_a, b, _a.Length);
                _a = b;
            }
            _a[_n++] = x;
        }

        public T Pop()
        {
            if (_n == 0) throw new InvalidOperationException("Stack is empty");
            var x = _a[--_n];
            _a[_n] = default!;
            return x;
        }

        public T Peek()
        {
            if (_n == 0) throw new InvalidOperationException("Stack is empty");
            return _a[_n - 1];
        }
    }

    // ========== D2: CircularQueue<T> ==========
    public sealed class CircularQueue<T>
    {
        private readonly T[] _buf;
        private int _head;
        private int _tail;
        private int _count;

        public CircularQueue(int capacity)
        {
            if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity));
            _buf = new T[capacity];
        }

        public int Count => _count;

        public bool IsEmpty() => _count == 0;

        public void Enqueue(T x)
        {
            if (_count == _buf.Length) throw new InvalidOperationException("Queue is full");
            _buf[_tail] = x;
            _tail = (_tail + 1) % _buf.Length;
            _count++;
        }

        public T Dequeue()
        {
            if (_count == 0) throw new InvalidOperationException("Queue is empty");
            var x = _buf[_head];
            _buf[_head] = default!;
            _head = (_head + 1) % _buf.Length;
            _count--;
            return x;
        }

        public T Peek()
        {
            if (_count == 0) throw new InvalidOperationException("Queue is empty");
            return _buf[_head];
        }
    }

    // ========== D3: QueueUsingTwoStacks<T> ==========
    public sealed class QueueUsingTwoStacks<T>
    {
        private readonly Stack<T> _in = new();
        private readonly Stack<T> _out = new();

        public int Count => _in.Count + _out.Count;

        public bool IsEmpty() => Count == 0;

        public void Enqueue(T x) => _in.Push(x);

        public T Dequeue()
        {
            MoveIfNeeded();
            if (_out.Count == 0) throw new InvalidOperationException("Queue is empty");
            return _out.Pop();
        }

        public T Peek()
        {
            MoveIfNeeded();
            if (_out.Count == 0) throw new InvalidOperationException("Queue is empty");
            return _out.Peek();
        }

        private void MoveIfNeeded()
        {
            if (_out.Count > 0) return;
            while (_in.Count > 0) _out.Push(_in.Pop());
        }
    }

    // ========== D4: StackUsingTwoQueues<T> ==========
    public sealed class StackUsingTwoQueues<T>
    {
        private Queue<T> _q = new();

        public int Count => _q.Count;

        public bool IsEmpty() => _q.Count == 0;

        public void Push(T x)
        {
            var tmp = new Queue<T>();
            tmp.Enqueue(x);
            while (_q.Count > 0) tmp.Enqueue(_q.Dequeue());
            _q = tmp;
        }

        public T Pop()
        {
            if (_q.Count == 0) throw new InvalidOperationException("Stack is empty");
            return _q.Dequeue();
        }

        public T Peek()
        {
            if (_q.Count == 0) throw new InvalidOperationException("Stack is empty");
            return _q.Peek();
        }
    }

    // ========== D5: MinStack ==========
    public sealed class MinStack
    {
        private readonly Stack<(int val, int min)> _st = new();

        public int Count => _st.Count;

        public void Push(int x)
        {
            int mn = _st.Count == 0 ? x : Math.Min(x, _st.Peek().min);
            _st.Push((x, mn));
        }

        public int Pop()
        {
            if (_st.Count == 0) throw new InvalidOperationException("Stack is empty");
            return _st.Pop().val;
        }

        public int Peek()
        {
            if (_st.Count == 0) throw new InvalidOperationException("Stack is empty");
            return _st.Peek().val;
        }

        public int GetMin()
        {
            if (_st.Count == 0) throw new InvalidOperationException("Stack is empty");
            return _st.Peek().min;
        }
    }

    // ========== D6..D15: Stack algorithms ==========

    public static bool IsValidParentheses(string s)
    {
        var st = new Stack<char>();
        foreach (var ch in s)
        {
            if (ch == '(' || ch == '[' || ch == '{')
            {
                st.Push(ch);
            }
            else if (ch == ')' || ch == ']' || ch == '}')
            {
                if (st.Count == 0) return false;
                var open = st.Pop();
                if ((ch == ')' && open != '(') ||
                    (ch == ']' && open != '[') ||
                    (ch == '}' && open != '{'))
                    return false;
            }
            else
            {
                // If input ever includes non-bracket chars, treat as invalid.
                return false;
            }
        }
        return st.Count == 0;
    }

    public static string RemoveAdjacentDuplicates(string s)
    {
        var st = new Stack<char>();
        foreach (var ch in s)
        {
            if (st.Count > 0 && st.Peek() == ch) st.Pop();
            else st.Push(ch);
        }
        var arr = st.ToArray();
        Array.Reverse(arr);
        return new string(arr);
    }

    public static int EvalRpn(string[] tokens)
    {
        var st = new Stack<int>();
        foreach (var t in tokens)
        {
            if (t == "+" || t == "-" || t == "*" || t == "/")
            {
                int b = st.Pop();
                int a = st.Pop();
                int r = t switch
                {
                    "+" => a + b,
                    "-" => a - b,
                    "*" => a * b,
                    "/" => a / b,
                    _ => 0
                };
                st.Push(r);
            }
            else
            {
                st.Push(int.Parse(t));
            }
        }
        return st.Pop();
    }

    public static string SimplifyPath(string path)
    {
        var parts = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        var st = new Stack<string>();
        foreach (var p in parts)
        {
            if (p == ".") continue;
            if (p == "..")
            {
                if (st.Count > 0) st.Pop();
            }
            else
            {
                st.Push(p);
            }
        }
        var arr = st.ToArray();
        Array.Reverse(arr);
        return "/" + string.Join("/", arr);
    }

    public static List<int> NextGreaterRight(int[] nums)
    {
        int n = nums.Length;
        var res = new int[n];
        Array.Fill(res, -1);
        var st = new Stack<int>(); // indices; nums[st] is decreasing

        for (int i = 0; i < n; i++)
        {
            while (st.Count > 0 && nums[i] > nums[st.Peek()])
                res[st.Pop()] = nums[i];
            st.Push(i);
        }

        return res.ToList();
    }

    public static List<int> DailyTemperatures(int[] temps)
    {
        int n = temps.Length;
        var res = new int[n];
        var st = new Stack<int>(); // indices with decreasing temps

        for (int i = 0; i < n; i++)
        {
            while (st.Count > 0 && temps[i] > temps[st.Peek()])
            {
                int j = st.Pop();
                res[j] = i - j;
            }
            st.Push(i);
        }

        return res.ToList();
    }

    public static int LargestRectangleArea(int[] heights)
    {
        int n = heights.Length;
        var st = new Stack<int>();
        int best = 0;

        for (int i = 0; i <= n; i++)
        {
            int h = (i == n) ? 0 : heights[i];
            while (st.Count > 0 && h < heights[st.Peek()])
            {
                int height = heights[st.Pop()];
                int leftLess = st.Count == 0 ? -1 : st.Peek();
                int width = i - leftLess - 1;
                best = Math.Max(best, height * width);
            }
            st.Push(i);
        }

        return best;
    }

    public static string DecodeString(string s)
    {
        var countSt = new Stack<int>();
        var strSt = new Stack<string>();
        int num = 0;
        var cur = "";

        foreach (var ch in s)
        {
            if (char.IsDigit(ch))
            {
                num = num * 10 + (ch - '0');
            }
            else if (ch == '[')
            {
                countSt.Push(num);
                strSt.Push(cur);
                num = 0;
                cur = "";
            }
            else if (ch == ']')
            {
                int k = countSt.Pop();
                var prev = strSt.Pop();
                cur = prev + string.Concat(Enumerable.Repeat(cur, k));
            }
            else
            {
                cur += ch;
            }
        }

        return cur;
    }

    public static List<string> InfixToPostfixTokens(string[] tokens)
    {
        static bool IsOp(string t) => t is "+" or "-" or "*" or "/";
        static int Prec(string op) => op is "*" or "/" ? 2 : 1;

        var outp = new List<string>();
        var ops = new Stack<string>();

        foreach (var t in tokens)
        {
            if (int.TryParse(t, out _))
            {
                outp.Add(t);
            }
            else if (t == "(")
            {
                ops.Push(t);
            }
            else if (t == ")")
            {
                while (ops.Count > 0 && ops.Peek() != "(")
                    outp.Add(ops.Pop());
                if (ops.Count == 0 || ops.Peek() != "(") throw new ArgumentException("Mismatched parentheses");
                ops.Pop();
            }
            else if (IsOp(t))
            {
                while (ops.Count > 0 && IsOp(ops.Peek()) && Prec(ops.Peek()) >= Prec(t))
                    outp.Add(ops.Pop());
                ops.Push(t);
            }
            else
            {
                throw new ArgumentException("Unknown token: " + t);
            }
        }

        while (ops.Count > 0)
        {
            var op = ops.Pop();
            if (op == "(" || op == ")") throw new ArgumentException("Mismatched parentheses");
            outp.Add(op);
        }

        return outp;
    }

    // Basic calculator for +, -, parentheses.
    public static int Calculate(string s)
    {
        int res = 0;
        int sign = 1;
        int num = 0;
        var st = new Stack<int>(); // stores previous res and sign

        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];
            if (char.IsDigit(c))
            {
                num = num * 10 + (c - '0');
            }
            else if (c == '+' || c == '-')
            {
                res += sign * num;
                num = 0;
                sign = (c == '+') ? 1 : -1;
            }
            else if (c == '(')
            {
                st.Push(res);
                st.Push(sign);
                res = 0;
                sign = 1;
                num = 0;
            }
            else if (c == ')')
            {
                res += sign * num;
                num = 0;
                int prevSign = st.Pop();
                int prevRes = st.Pop();
                res = prevRes + prevSign * res;
            }
            else if (c == ' ')
            {
                continue;
            }
            else
            {
                throw new ArgumentException("Invalid character: " + c);
            }
        }

        res += sign * num;
        return res;
    }

    // ========== D16..D18: Queue/BFS algorithms ==========

    public static (int dist, List<int> path) BfsShortestPath(int n, (int u, int v)[] edges, int src, int dst)
    {
        var adj = BuildUndirectedAdj(n, edges, sortNeighbors: true);
        var q = new Queue<int>();
        var dist = Enumerable.Repeat(-1, n).ToArray();
        var parent = Enumerable.Repeat(-1, n).ToArray();

        dist[src] = 0;
        parent[src] = src;
        q.Enqueue(src);

        while (q.Count > 0)
        {
            int u = q.Dequeue();
            if (u == dst) break;
            foreach (var v in adj[u])
            {
                if (dist[v] != -1) continue;
                dist[v] = dist[u] + 1;
                parent[v] = u;
                q.Enqueue(v);
            }
        }

        if (dist[dst] == -1) return (-1, new List<int>());

        var path = new List<int>();
        int cur = dst;
        while (cur != src)
        {
            path.Add(cur);
            cur = parent[cur];
            if (cur == -1) break;
        }
        path.Add(src);
        path.Reverse();
        return (dist[dst], path);
    }

    public static int CountConnectedComponents(int n, (int u, int v)[] edges)
    {
        var adj = BuildUndirectedAdj(n, edges, sortNeighbors: false);
        var vis = new bool[n];
        int comps = 0;

        for (int i = 0; i < n; i++)
        {
            if (vis[i]) continue;
            comps++;
            var q = new Queue<int>();
            q.Enqueue(i);
            vis[i] = true;
            while (q.Count > 0)
            {
                int u = q.Dequeue();
                foreach (var v in adj[u])
                {
                    if (vis[v]) continue;
                    vis[v] = true;
                    q.Enqueue(v);
                }
            }
        }

        return comps;
    }

    public static List<int> TopoSortKahn(int n, (int u, int v)[] edges)
    {
        var adj = new List<int>[n];
        for (int i = 0; i < n; i++) adj[i] = new List<int>();
        var indeg = new int[n];

        foreach (var (u, v) in edges)
        {
            adj[u].Add(v);
            indeg[v]++;
        }

        for (int i = 0; i < n; i++) adj[i].Sort();

        // Lexicographically smallest: always pick smallest available indegree-0 node.
        var pq = new PriorityQueue<int, int>();
        for (int i = 0; i < n; i++)
            if (indeg[i] == 0) pq.Enqueue(i, i);

        var res = new List<int>(n);
        while (pq.Count > 0)
        {
            int u = pq.Dequeue();
            res.Add(u);
            foreach (var v in adj[u])
            {
                indeg[v]--;
                if (indeg[v] == 0) pq.Enqueue(v, v);
            }
        }

        if (res.Count != n) return new List<int>();
        return res;
    }

    // ========== D19..D20: Deque patterns ==========

    public static List<int> SlidingWindowMax(int[] nums, int k)
    {
        if (k <= 0) throw new ArgumentOutOfRangeException(nameof(k));
        var res = new List<int>();
        var dq = new LinkedList<int>(); // indices, nums[dq] decreasing

        for (int i = 0; i < nums.Length; i++)
        {
            // Remove out-of-window
            if (dq.Count > 0 && dq.First!.Value <= i - k)
                dq.RemoveFirst();

            // Maintain decreasing
            while (dq.Count > 0 && nums[dq.Last!.Value] <= nums[i])
                dq.RemoveLast();

            dq.AddLast(i);

            if (i >= k - 1)
                res.Add(nums[dq.First!.Value]);
        }

        return res;
    }

    public static List<int> FirstNegativeInWindow(int[] nums, int k)
    {
        if (k <= 0) throw new ArgumentOutOfRangeException(nameof(k));
        var res = new List<int>();
        var q = new Queue<int>(); // indices of negative elements

        for (int i = 0; i < nums.Length; i++)
        {
            if (nums[i] < 0) q.Enqueue(i);

            // Window start
            int start = i - k + 1;

            if (start >= 0)
            {
                while (q.Count > 0 && q.Peek() < start) q.Dequeue();
                res.Add(q.Count == 0 ? 0 : nums[q.Peek()]);
            }
        }

        return res;
    }

    // ========== D21..D22: Grid BFS ==========

    public static int OrangesRotting(int[][] grid)
    {
        int m = grid.Length;
        int n = grid[0].Length;
        var q = new Queue<(int r, int c)>();
        int fresh = 0;

        for (int r = 0; r < m; r++)
        {
            for (int c = 0; c < n; c++)
            {
                if (grid[r][c] == 2) q.Enqueue((r, c));
                else if (grid[r][c] == 1) fresh++;
            }
        }

        if (fresh == 0) return 0;

        int minutes = 0;
        int[] dr = { -1, 1, 0, 0 };
        int[] dc = { 0, 0, -1, 1 };

        while (q.Count > 0)
        {
            int sz = q.Count;
            bool rottedThisMinute = false;

            for (int i = 0; i < sz; i++)
            {
                var (r, c) = q.Dequeue();
                for (int k = 0; k < 4; k++)
                {
                    int nr = r + dr[k], nc = c + dc[k];
                    if (nr < 0 || nr >= m || nc < 0 || nc >= n) continue;
                    if (grid[nr][nc] != 1) continue;
                    grid[nr][nc] = 2;
                    fresh--;
                    rottedThisMinute = true;
                    q.Enqueue((nr, nc));
                }
            }

            if (rottedThisMinute) minutes++;
        }

        return fresh == 0 ? minutes : -1;
    }

    public static int ShortestPathBinaryMatrix(int[][] grid)
    {
        int n = grid.Length;
        if (n == 0) return -1;
        int m = grid[0].Length;
        if (grid[0][0] != 0 || grid[n - 1][m - 1] != 0) return -1;

        var q = new Queue<(int r, int c, int d)>();
        var vis = new bool[n, m];
        q.Enqueue((0, 0, 1));
        vis[0, 0] = true;

        int[] dr = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] dc = { -1, 0, 1, -1, 1, -1, 0, 1 };

        while (q.Count > 0)
        {
            var (r, c, d) = q.Dequeue();
            if (r == n - 1 && c == m - 1) return d;

            for (int k = 0; k < 8; k++)
            {
                int nr = r + dr[k], nc = c + dc[k];
                if (nr < 0 || nr >= n || nc < 0 || nc >= m) continue;
                if (vis[nr, nc]) continue;
                if (grid[nr][nc] != 0) continue;
                vis[nr, nc] = true;
                q.Enqueue((nr, nc, d + 1));
            }
        }

        return -1;
    }

    // ========== D23: RecentCounter ==========

    public sealed class RecentCounter
    {
        private readonly Queue<int> _q = new();

        public RecentCounter() { }

        public int Ping(int t)
        {
            _q.Enqueue(t);
            int lo = t - 3000;
            while (_q.Count > 0 && _q.Peek() < lo) _q.Dequeue();
            return _q.Count;
        }
    }

    // ========== D24: BFS levels ==========

    public static List<IList<int>> BfsLevels(int n, (int u, int v)[] edges, int src)
    {
        var adj = BuildUndirectedAdj(n, edges, sortNeighbors: true);
        var res = new List<IList<int>>();
        var dist = Enumerable.Repeat(-1, n).ToArray();
        var q = new Queue<int>();

        dist[src] = 0;
        q.Enqueue(src);

        while (q.Count > 0)
        {
            int sz = q.Count;
            var level = new List<int>(sz);

            for (int i = 0; i < sz; i++)
            {
                int u = q.Dequeue();
                level.Add(u);
                foreach (var v in adj[u])
                {
                    if (dist[v] != -1) continue;
                    dist[v] = dist[u] + 1;
                    q.Enqueue(v);
                }
            }

            res.Add(level);
        }

        return res;
    }

    // ========== D25: Prefix sums + monotonic deque ==========

    public static int ShortestSubarrayAtLeastK(int[] nums, int k)
    {
        int n = nums.Length;
        var pref = new long[n + 1];
        for (int i = 0; i < n; i++) pref[i + 1] = pref[i] + nums[i];

        int ans = int.MaxValue;
        var dq = new LinkedList<int>(); // indices with increasing pref

        for (int i = 0; i <= n; i++)
        {
            while (dq.Count > 0 && pref[i] - pref[dq.First!.Value] >= k)
            {
                ans = Math.Min(ans, i - dq.First!.Value);
                dq.RemoveFirst();
            }

            while (dq.Count > 0 && pref[i] <= pref[dq.Last!.Value])
                dq.RemoveLast();

            dq.AddLast(i);
        }

        return ans == int.MaxValue ? -1 : ans;
    }

    // ---------------------------
    // Helpers
    // ---------------------------

    private static List<int>[] BuildUndirectedAdj(int n, (int u, int v)[] edges, bool sortNeighbors)
    {
        var adj = new List<int>[n];
        for (int i = 0; i < n; i++) adj[i] = new List<int>();
        foreach (var (u, v) in edges)
        {
            adj[u].Add(v);
            adj[v].Add(u);
        }
        if (sortNeighbors)
        {
            for (int i = 0; i < n; i++) adj[i].Sort();
        }
        return adj;
    }
}

```

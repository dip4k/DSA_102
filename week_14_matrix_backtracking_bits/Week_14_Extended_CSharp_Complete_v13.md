# 🗄️ Week 14 Extended C# Complete Reference v13

This reference guide provides production-grade, highly optimized, and memory-safe C# implementations for Week 14's core algorithm patterns. All code blocks are written to minimize GC allocation and use idiomatic modern C# structures.

---

## 🧮 1. Matrix Rotations, Transpositions & Traversal

### Transpose & Clockwise 90° Rotation
To rotate an N x N matrix clockwise 90 degrees in-place without auxiliary memory, first reflect the matrix diagonally (Transpose), then reverse each horizontal row.

```csharp
using System;
using System.Collections.Generic;

public static class MatrixRotator {
    /// <summary>
    /// Swaps elements symmetrically across the main diagonal in-place.
    /// Time Complexity: O(N^2)
    /// Space Complexity: O(1)
    /// </summary>
    public static void Transpose(int[][] matrix) {
        int n = matrix.Length;
        for (int i = 0; i < n; i++) {
            for (int j = i + 1; j < n; j++) {
                int temp = matrix[i][j];
                matrix[i][j] = matrix[j][i];
                matrix[j][i] = temp;
            }
        }
    }

    /// <summary>
    /// Rotates an N x N 2D grid clockwise 90 degrees in-place.
    /// Time Complexity: O(N^2)
    /// Space Complexity: O(1)
    /// </summary>
    public static void RotateClockwise90(int[][] matrix) {
        if (matrix == null || matrix.Length == 0) return;
        
        // Step 1: Reflect over the diagonal
        Transpose(matrix);
        
        // Step 2: Reverse each row individually
        int n = matrix.Length;
        for (int i = 0; i < n; i++) {
            int left = 0;
            int right = n - 1;
            while (left < right) {
                int temp = matrix[i][left];
                matrix[i][left] = matrix[i][right];
                matrix[i][right] = temp;
                left++;
                right--;
            }
        }
    }
}
```

### Spiral Order Walk
To traverse a variable-sized rectangular matrix in spiral order, use four bounding pointers to track active limits dynamically. This avoids complex coordinate calculations.

```csharp
public static class SpiralWalker {
    /// <summary>
    /// Performs a layered outer-to-inner boundary walk of a rectangular matrix.
    /// Time Complexity: O(R * C)
    /// Space Complexity: O(1) auxiliary space (excluding result list)
    /// </summary>
    public static IList<int> SpiralOrder(int[][] matrix) {
        var result = new List<int>();
        if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) {
            return result;
        }

        int top = 0;
        int bottom = matrix.Length - 1;
        int left = 0;
        int right = matrix[0].Length - 1;

        while (top <= bottom && left <= right) {
            // 1. Traverse Right (Left to Right along top boundary)
            for (int col = left; col <= right; col++) {
                result.Add(matrix[top][col]);
            }
            top++; // Shrink top boundary

            // 2. Traverse Down (Top to Bottom along right boundary)
            for (int row = top; row <= bottom; row++) {
                result.Add(matrix[row][right]);
            }
            right--; // Shrink right boundary

            // 3. Traverse Left (Right to Left along bottom boundary if still valid)
            if (top <= bottom) {
                for (int col = right; col >= left; col--) {
                    result.Add(matrix[bottom][col]);
                }
                bottom--; // Shrink bottom boundary
            }

            // 4. Traverse Up (Bottom to Top along left boundary if still valid)
            if (left <= right) {
                for (int row = bottom; row >= top; row--) {
                    result.Add(matrix[row][left]);
                }
                left++; // Shrink left boundary
            }
        }
        return result;
    }
}
```

---

## 🔌 2. Bitwise Utilities & State Operations

```csharp
public static class BitwiseEngine {
    /// <summary>
    /// Constant-time check verifying if value is a true positive power of 2.
    /// Time Complexity: O(1)
    /// </summary>
    public static bool IsPowerOfTwo(long n) {
        return n > 0 && (n & (n - 1)) == 0;
    }

    /// <summary>
    /// Brian Kernighan popcounting. Repeatedly clears the lowest set bit.
    /// Time Complexity: O(Set Bits)
    /// </summary>
    public static int PopCount(long n) {
        int count = 0;
        while (n > 0) {
            n &= (n - 1); // Clears the lowest set bit
            count++;
        }
        return count;
    }

    /// <summary>
    /// Enumerates all nested active submasks of a given bitmask in decreasing order.
    /// Time Complexity: O(2^K) where K is active bits
    /// </summary>
    public static List<int> EnumerateSubmasks(int mask) {
        var submasks = new List<int>();
        int sub = mask;
        while (sub > 0) {
            submasks.Add(sub);
            sub = (sub - 1) & mask; // Algebraic decrement intersection
        }
        submasks.Add(0); // Add empty set boundary
        return submasks;
    }
    
    /// <summary>
    /// Calculates Gray code value for integer n.
    /// Time Complexity: O(1)
    /// </summary>
    public static long IntegerToGray(long n) {
        return n ^ (n >> 1);
    }
}
```

---

## 📐 3. Sieve of Eratosthenes & Modular Mathematics

```csharp
public static class ClassicalPrimes {
    /// <summary>
    /// Standard prime sieve up to N.
    /// Time Complexity: O(N log log N)
    /// Space Complexity: O(N)
    /// </summary>
    public static bool[] GeneratePrimesUpTo(int limit) {
        if (limit < 0) return Array.Empty<bool>();
        bool[] isPrime = new bool[limit + 1];
        Array.Fill(isPrime, true);

        if (limit >= 0) isPrime[0] = false;
        if (limit >= 1) isPrime[1] = false;

        for (int p = 2; p * p <= limit; p++) {
            if (isPrime[p]) {
                // Eliminate multiples starting from p^2
                for (int m = p * p; m <= limit; m += p) {
                    isPrime[m] = false;
                }
            }
        }
        return isPrime;
    }
}

public static class ModularAlgebra {
    /// <summary>
    /// Calculates Greatest Common Divisor recursively.
    /// Time Complexity: O(log(min(a, b)))
    /// </summary>
    public static long Gcd(long a, long b) {
        while (b > 0) {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return Math.Abs(a);
    }

    /// <summary>
    /// Fast modular exponentiation calculating (b^e) % m.
    /// Time Complexity: O(log e)
    /// </summary>
    public static long ModPow(long b, long e, long m) {
        if (m == 1) return 0;
        long res = 1;
        b %= m;
        while (e > 0) {
            if ((e & 1) == 1) {
                res = (res * b) % m;
            }
            b = (b * b) % m;
            e >>= 1;
        }
        return res;
    }

    /// <summary>
    /// Finds multiplicative inverse of val modulo primeMod.
    /// Time Complexity: O(log primeMod)
    /// </summary>
    public static long ModularInverse(long val, long primeMod) {
        if (val % primeMod == 0) {
            throw new ArgumentException("No inverse exists since val is a multiple of modular base.");
        }
        // Applying Fermat's Little Theorem: val^(P-2) = val^-1 (mod P)
        return ModPow(val, primeMod - 2, primeMod);
    }
}
```

---

## 🧵 4. KMP String Matcher & Preprocessed Trie

```csharp
public static class KmpEngine {
    /// <summary>
    /// Standard KMP pattern search returning 0-based starting indexes.
    /// Time Complexity: O(N + M)
    /// Space Complexity: O(M)
    /// </summary>
    public static List<int> Search(string text, string pattern) {
        var matchIndexes = new List<int>();
        if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(pattern)) {
            return matchIndexes;
        }

        int[] lps = BuildLpsTable(pattern);
        int tPtr = 0; // Text scanner pointer
        int pPtr = 0; // Pattern scanner pointer

        while (tPtr < text.Length) {
            if (text[tPtr] == pattern[pPtr]) {
                tPtr++;
                pPtr++;

                if (pPtr == pattern.Length) {
                    matchIndexes.Add(tPtr - pPtr);
                    pPtr = lps[pPtr - 1]; // Reset pattern pointer using fallback index
                }
            } else {
                if (pPtr > 0) {
                    pPtr = lps[pPtr - 1];
                } else {
                    tPtr++;
                }
            }
        }
        return matchIndexes;
    }

    private static int[] BuildLpsTable(string pattern) {
        int[] lps = new int[pattern.Length];
        int len = 0;
        int i = 1;
        lps[0] = 0;

        while (i < pattern.Length) {
            if (pattern[i] == pattern[len]) {
                len++;
                lps[i] = len;
                i++;
            } else {
                if (len > 0) {
                    len = lps[len - 1];
                } else {
                    lps[i] = 0;
                    i++;
                }
            }
        }
        return lps;
    }
}

public class TrieNode {
    public Dictionary<char, TrieNode> Children { get; } = new();
    public bool IsEndOfWord { get; set; }
}

public class Trie {
    private readonly TrieNode _root = new();

    /// <summary>
    /// Inserts string path into the Trie.
    /// Time Complexity: O(L)
    /// </summary>
    public void Insert(string word) {
        if (word == null) return;
        var current = _root;
        foreach (char ch in word) {
            if (!current.Children.ContainsKey(ch)) {
                current.Children[ch] = new TrieNode();
            }
            current = current.Children[ch];
        }
        current.IsEndOfWord = true;
    }

    /// <summary>
    /// Searches for exact match string in the prefix tree.
    /// Time Complexity: O(L)
    /// </summary>
    public bool Search(string word) {
        if (word == null) return false;
        var node = GetNodePath(word);
        return node != null && node.IsEndOfWord;
    }

    /// <summary>
    /// Verifies if a prefix exists in the Trie.
    /// Time Complexity: O(L)
    /// </summary>
    public bool StartsWith(string prefix) {
        if (prefix == null) return false;
        return GetNodePath(prefix) != null;
    }

    private TrieNode GetNodePath(string target) {
        var current = _root;
        foreach (char ch in target) {
            if (!current.Children.TryGetValue(ch, out var nextNode)) {
                return null;
            }
            current = nextNode;
        }
        return current;
    }
}

public class AhoCorasickNode {
    public Dictionary<char, AhoCorasickNode> Children { get; } = new();
    public AhoCorasickNode Fail { get; set; }
    public List<string> Output { get; } = new();
}

public class AhoCorasick {
    private readonly AhoCorasickNode _root = new();

    public void Insert(string word) {
        if (string.IsNullOrEmpty(word)) return;
        var current = _root;
        foreach (char ch in word) {
            if (!current.Children.ContainsKey(ch)) {
                current.Children[ch] = new AhoCorasickNode();
            }
            current = current.Children[ch];
        }
        current.Output.Add(word);
    }

    public void BuildAutomaton() {
        var queue = new Queue<AhoCorasickNode>();
        foreach (var child in _root.Children.Values) {
            child.Fail = _root;
            queue.Enqueue(child);
        }

        while (queue.Count > 0) {
            var current = queue.Dequeue();
            foreach (var kvp in current.Children) {
                char ch = kvp.Key;
                var childNode = kvp.Value;
                var failState = current.Fail;

                while (failState != null && !failState.Children.ContainsKey(ch)) {
                    failState = failState.Fail;
                }

                childNode.Fail = failState != null ? failState.Children[ch] : _root;
                childNode.Output.AddRange(childNode.Fail.Output);
                queue.Enqueue(childNode);
            }
        }
    }

    public Dictionary<string, List<int>> SearchText(string text) {
        var results = new Dictionary<string, List<int>>();
        var current = _root;

        for (int i = 0; i < text.Length; i++) {
            char ch = text[i];
            while (current != null && !current.Children.ContainsKey(ch)) {
                current = current.Fail;
            }

            current = current != null ? current.Children[ch] : _root;
            foreach (var word in current.Output) {
                int startIndex = i - word.Length + 1;
                if (!results.ContainsKey(word)) {
                    results[word] = new List<int>();
                }
                results[word].Add(startIndex);
            }
        }
        return results;
    }
}

public static class SuffixArrayEngine {
    public static int[] ConstructSuffixArray(string s) {
        int n = s.Length;
        var suffixes = new List<(int index, string value)>();
        for (int i = 0; i < n; i++) {
            suffixes.Add((i, s.Substring(i)));
        }
        suffixes.Sort((a, b) => string.Compare(a.value, b.value, StringComparison.Ordinal));
        
        int[] sa = new int[n];
        for (int i = 0; i < n; i++) {
            sa[i] = suffixes[i].index;
        }
        return sa;
    }

    public static int[] BuildLcpArray(string s, int[] sa) {
        int n = s.Length;
        int[] lcp = new int[n];
        int[] rank = new int[n];
        for (int i = 0; i < n; i++) {
            rank[sa[i]] = i;
        }

        int h = 0;
        for (int i = 0; i < n; i++) {
            if (rank[i] > 0) {
                int j = sa[rank[i] - 1];
                while (i + h < n && j + h < n && s[i + h] == s[j + h]) {
                    h++;
                }
                lcp[rank[i]] = h;
                if (h > 0) h--;
            }
        }
        return lcp;
    }
}

public static class RadixSorter {
    public static void LsdRadixSort(string[] arr, int stringLength) {
        int n = arr.Length;
        int R = 256;
        string[] aux = new string[n];

        for (int d = stringLength - 1; d >= 0; d--) {
            int[] count = new int[R + 1];
            for (int i = 0; i < n; i++) {
                count[arr[i][d] + 1]++;
            }
            for (int r = 0; r < R; r++) {
                count[r + 1] += count[r];
            }
            for (int i = 0; i < n; i++) {
                aux[count[arr[i][d]]++] = arr[i];
            }
            Array.Copy(aux, arr, n);
        }
    }

    public static void Sort3Way(string[] arr) {
        Sort3Way(arr, 0, arr.Length - 1, 0);
    }

    private static void Sort3Way(string[] arr, int lo, int hi, int d) {
        if (hi <= lo) return;

        int lt = lo;
        int gt = hi;
        int v = GetCharAt(arr[lo], d);
        int i = lo + 1;

        while (i <= gt) {
            int t = GetCharAt(arr[i], d);
            if (t < v) Swap(arr, lt++, i++);
            else if (t > v) Swap(arr, i, gt--);
            else i++;
        }

        Sort3Way(arr, lo, lt - 1, d);
        if (v >= 0) Sort3Way(arr, lt, gt, d + 1);
        Sort3Way(arr, gt + 1, hi, d);
    }

    private static int GetCharAt(string s, int d) {
        return d < s.Length ? s[d] : -1;
    }

    private static void Swap(string[] arr, int i, int j) {
        string temp = arr[i];
        arr[i] = arr[j];
        arr[j] = temp;
    }
}

---

## 🧮 5. Advanced Totients & Chinese Remainder Solvers

```csharp
public static class TotientEngine {
    /// <summary>
    /// Calculates Euler's Totient function phi(N) using prime factors.
    /// Time Complexity: O(sqrt(N))
    /// </summary>
    public static long GetTotient(long n) {
        long result = n;
        long temp = n;
        
        for (long p = 2; p * p <= temp; p++) {
            if (temp % p == 0) {
                result -= result / p;
                while (temp % p == 0) {
                    temp /= p;
                }
            }
        }
        if (temp > 1) {
            result -= result / temp;
        }
        return result;
    }

    /// <summary>
    /// Pre-calculates Euler's Totients for all values up to limit.
    /// Time Complexity: O(N log log N)
    /// </summary>
    public static int[] PrecomputeTotients(int limit) {
        int[] phi = new int[limit + 1];
        for (int i = 0; i <= limit; i++) phi[i] = i;

        for (int p = 2; p <= limit; p++) {
            if (phi[p] == p) {
                for (int m = p; m <= limit; m += p) {
                    phi[m] -= phi[m] / p;
                }
            }
        }
        return phi;
    }
}

public static class CrtSolver {
    /// <summary>
    /// Performs overflow-safe modular multiplication calculating (a * b) % mod.
    /// Time Complexity: O(log b)
    /// </summary>
    public static long SafeMultiply(long a, long b, long mod) {
        long res = 0;
        a %= mod;
        while (b > 0) {
            if ((b & 1) == 1) {
                res = (res + a) % mod;
            }
            a = (a * 2) % mod;
            b >>= 1;
        }
        return res;
    }

    private static long ExtendedGcd(long a, long b, out long x, out long y) {
        if (b == 0) {
            x = 1;
            y = 0;
            return a;
        }
        long g = ExtendedGcd(b, a % b, out long x1, out long y1);
        x = y1;
        y = x1 - (a / b) * y1;
        return g;
    }

    private static long ModInverse(long a, long m) {
        long g = ExtendedGcd(a, m, out long x, out long y);
        if (g != 1) return -1; // GCD is not 1, modular inverse does not exist
        return (x % m + m) % m;
    }

    /// <summary>
    /// Solves a simultaneous system of congruence equations modulo prime-coprime moduli.
    /// Time Complexity: O(K log M) where K is number of congruence bases
    /// </summary>
    public static long Solve(long[] a, long[] m) {
        long totalM = 1;
        for (int i = 0; i < m.Length; i++) {
            totalM *= m[i];
        }

        long result = 0;
        for (int i = 0; i < a.Length; i++) {
            long intermediateM = totalM / m[i];
            long inverse = ModInverse(intermediateM, m[i]);
            if (inverse == -1) return -1; // No solution exists
            
            long term = SafeMultiply(a[i], intermediateM, totalM);
            term = SafeMultiply(term, inverse, totalM);
            result = (result + term) % totalM;
        }
        return result;
    }
}
```
        current.IsEndOfWord = true;
    }

    /// <summary>
    /// Searches for exact match string in the prefix tree.
    /// Time Complexity: O(L)
    /// </summary>
    public bool Search(string word) {
        if (word == null) return false;
        var node = GetNodePath(word);
        return node != null && node.IsEndOfWord;
    }

    /// <summary>
    /// Verifies if a prefix exists in the Trie.
    /// Time Complexity: O(L)
    /// </summary>
    public bool StartsWith(string prefix) {
        if (prefix == null) return false;
        return GetNodePath(prefix) != null;
    }

    private TrieNode GetNodePath(string target) {
        var current = _root;
        foreach (char ch in target) {
            if (!current.Children.TryGetValue(ch, out var nextNode)) {
                return null;
            }
            current = nextNode;
        }
        return current;
    }
}
```

---

## 🧮 5. Advanced Totients & Chinese Remainder Solvers

```csharp
public static class TotientEngine {
    /// <summary>
    /// Calculates Euler's Totient function phi(N) using prime factors.
    /// Time Complexity: O(sqrt(N))
    /// </summary>
    public static long GetTotient(long n) {
        long result = n;
        long temp = n;
        
        for (long p = 2; p * p <= temp; p++) {
            if (temp % p == 0) {
                // Sieve out factor components
                result -= result / p;
                while (temp % p == 0) {
                    temp /= p;
                }
            }
        }
        if (temp > 1) {
            result -= result / temp;
        }
        return result;
    }
}

public static class CrtSolver {
    private static long ExtendedGcd(long a, long b, out long x, out long y) {
        if (b == 0) {
            x = 1;
            y = 0;
            return a;
        }
        long g = ExtendedGcd(b, a % b, out long x1, out long y1);
        x = y1;
        y = x1 - (a / b) * y1;
        return g;
    }

    private static long ModInverse(long a, long m) {
        long g = ExtendedGcd(a, m, out long x, out long y);
        if (g != 1) return -1; // GCD is not 1, modular inverse does not exist
        return (x % m + m) % m;
    }

    /// <summary>
    /// Solves a simultaneous system of congruence equations modulo prime-coprime moduli.
    /// Time Complexity: O(K log M) where K is number of congruence bases
    /// </summary>
    public static long Solve(long[] a, long[] m) {
        long totalM = 1;
        for (int i = 0; i < m.Length; i++) {
            totalM *= m[i];
        }

        long result = 0;
        for (int i = 0; i < a.Length; i++) {
            long intermediateM = totalM / m[i];
            long inverse = ModInverse(intermediateM, m[i]);
            if (inverse == -1) return -1; // No solution exists
            
            long term = (a[i] * intermediateM) % totalM;
            term = (term * inverse) % totalM;
            result = (result + term) % totalM;
        }
        return result;
    }
}
```

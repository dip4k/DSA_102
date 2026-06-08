# 📌 Week 14 Instructional Guide v13: Matrices, Bitmasks & Number Theory

Welcome to Week 14. This week represents an integration checkpoint where we bridge geometric transformations, machine-level bit manipulation, algebraic structures, and advanced string pattern matching. Mastering these domains elevates you from a developer who implements algorithms to an engineer who understands how they execute on physical hardware, scale in production systems, and secure communications.

---

## 🎯 Weekly Goal
Master specialized problem domains—matrices, bitwise operations, number theory, and advanced string preprocessing structures—by developing rigorous indexing invariants, using registers over heap allocation, exploiting modular algebra, and compiling matching state machines.

---

## 🧭 Pattern Triggers

### 🧮 Matrix Operations & Transformations
*   **Trigger**: Matrix search, 90-degree rotations, spiral walks, in-place traversals, or constraint-based grid pathing.
*   **Rationale**: Using sequential transform compositions (e.g., transpose + row reverse) or strict boundary pointers prevents index drift and bounds-checking failure under pressure.

### 🔌 Bitwise Manipulation & States
*   **Trigger**: Subsets tracking, power-of-two state checking, unique parity queries, or compact integer-based set representations.
*   **Rationale**: Packing boolean states into register-size words reduces allocation overhead to zero and unlocks single-cycle CPU operations.

### 📐 Number Theory & Cryptographic Math
*   **Trigger**: Divisibility, prime factorization, coprime counts, modular inverse queries, or solving multi-boundary congruences.
*   **Rationale**: Modular inverse arithmetic, logarithmic exponentiation, and prime sieves convert exponential searching into deterministic algebraic formulas.

### 🧵 Advanced String Preprocessing
*   **Trigger**: Substring search, multi-pattern streaming matching, suffix indexing, or linear-time string quicksort.
*   **Rationale**: Compiling patterns into prefix/suffix state tracking structures (LPS tabs, Tries, Automata) avoids backtracking the main text pointer.

---

## 📅 Day 1: Matrix Operations, Transpose, Spiral & Rotations

### 1. Context & Motivation
Matrix operations are the primary filter for testing coordinate-mapping discipline and index-boundary security. In real systems, multi-dimensional grids are mapped to sequential address physical lines. Programmers often struggle with coordinates because they fail to separate logical coordinate transforms from sequential index walks. Under pressure, index off-by-one errors and boundary drift are the main failure modes.

Real-world anchors:
*   **Graphics Pipelines**: 2D/3D matrix rotations and image transpositions are critical operations in low-level graphics engines (like Vulkan and WebGL).
*   **Row-major vs. Column-major Cache Performance**: Accessing sequential rows in contiguous memory is significantly faster than vertical columns because of CPU cache line prefetching.

---

### 2. Mental Model
Consider an N x N matrix as a collection of nested concentric rings (like boundaries of a castle).
Complicated 2D coordinate manipulations are often simpler 1D transforms composed sequentially. For example:
*   **Rotate 90° Clockwise** == Transpose across primary diagonal arrow Reverse each horizontal row.
*   **Rotate 90° Counter-Clockwise** == Transpose across primary diagonal arrow Reverse each vertical column (or reverse each row then transpose).

#### 📊 Matrix Transformation Taxonomy
| Operation | Algebraic Index Mapping | Sequential Composition Method | Space Complexity | Best-use Case |
| :--- | :--- | :--- | :--- | :--- |
| **Transpose** | M[i][j] <=ftrightarrow M[j][i] | In-place nested swaps over upper triangle (j > i) | O(1) | Reflection over diagonal |
| **Clockwise 90°** | M[i][j] arrow M[j][N-1-i] | Transpose arrow Reverse Rows | O(1) | Coordinate rotation |
| **Counter-Clockwise 90°** | M[i][j] arrow M[N-1-j][i] | Reverse Rows arrow Transpose | O(1) | Opposite rotation |
| **Spiral Walk** | Layer/Boundary step tracing | Step-by-step bounds shrinkage (T, B, L, R) | O(1) | Ordered data extraction |

---

### 3. Mechanics

#### A. In-Place Transpose (Square Matrix)
To transpose a matrix in-place, swap elements symmetrically across the main diagonal. Only iterate elements where j > i to avoid swapping elements back to their original positions.

```csharp
// In-place Transposition of an N x N Matrix
public static void Transpose(int[][] matrix) {
    int n = matrix.Length;
    for (int i = 0; i < n; i++) {
        for (int j = i + 1; j < n; j++) {
            // Swap symmetric coordinates across the diagonal
            int temp = matrix[i][j];
            matrix[i][j] = matrix[j][i];
            matrix[j][i] = temp;
        }
    }
}
```

#### B. Clockwise 90° Rotation
```csharp
public static void RotateClockwise90(int[][] matrix) {
    // Step 1: Reflect over the primary diagonal (Transpose)
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
```

#### C. Spiral Traversal (Variable Rectangular Grid)
Maintain four strict pointer boundaries: `top`, `bottom`, `left`, and `right`. Read values sequentially and shrink the boundaries inward:

```text
  L                 R
T +-----------------+
  | 1 -> 2 -> 3 -> 4|   Boundary top incremented after traversing right
  | 5    6    7    8|   
  | 9   10   11   12|   
B +-----------------+
```

```csharp
public static IList<int> SpiralOrder(int[][] matrix) {
    var result = new List<int>();
    if (matrix == null || matrix.Length == 0 || matrix[0].Length == 0) return result;
    
    int top = 0, bottom = matrix.Length - 1;
    int left = 0, right = matrix[0].Length - 1;
    
    while (top <= bottom && left <= right) {
        // 1. Traverse Left to Right across top boundary
        for (int i = left; i <= right; i++) {
            result.Add(matrix[top][i]);
        }
        top++; // Shrink top boundary
        
        // 2. Traverse Top to Bottom along right boundary
        for (int i = top; i <= bottom; i++) {
            result.Add(matrix[i][right]);
        }
        right--; // Shrink right boundary
        
        // 3. Traverse Right to Left (only if top boundary hasn't crossed bottom)
        if (top <= bottom) {
            for (int i = right; i >= left; i--) {
                result.Add(matrix[bottom][i]);
            }
            bottom--; // Shrink bottom boundary
        }
        
        // 4. Traverse Bottom to Top (only if left boundary hasn't crossed right)
        if (left <= right) {
            for (int i = bottom; i >= top; i--) {
                result.Add(matrix[i][left]);
            }
            left++; // Shrink left boundary
        }
    }
    return result;
}
```

#### D. Matrix Multiplication (DP Order & Strassen)
Matrix multiplication of A (P x Q) by B (Q x R) yields matrix C (P x R) in O(P * Q * R) operations.
*   **Strassen's Algorithm**: Divides matrices into sub-blocks recursively. Standard multiplication takes 8 multiplications of size N/2. Strassen uses algebraic identities to reduce this to 7 multiplications, lowering asymptotic time to O(N^{log2(7)}) approx. O(N^{2.81}).
*   **Matrix Chain Multiplication**: To find the optimal multiplication parenthesization for a chain A_1 x A_2 x \dots x A_k, define dp[i][j] as the minimum cost to multiply matrices from index i to j. The transition tries all partition points m:
    dp[i][j] = min(dp[i][m] + dp[m+1][j] + dims[i-1] * dims[m] * dims[j]) for all i <= m < j

#### E. Determinant & Inverse (Gaussian Elimination)
Evaluating n simultaneous equations is equivalent to performing operations on row values in a column matrix.
*   **Gaussian Elimination**: Systematically converts a matrix to Upper Triangular form by scaling rows and subtracting them.
*   **Time Complexity**: O(N^3) where N is the number of rows.
*   **Applications**: Finding the determinant of a matrix (the product of diagonal pivots of the upper triangular form) and computing the inverse of M via augmented rows [M | I].

---

### 4. Performance & Systems
*   **Time Complexities**:
    *   Transpose: O(N^2) — visited each element above diagonal once.
    *   Rotation: O(N^2) — transpose + row reverses require two scan passes.
    *   Spiral: O(R * C) — visited each element exactly once.
*   **Space Complexities**:
    *   In-place transformations utilize O(1) auxiliary space.
*   **Cache Locality Impact**: Because of CPU caching architectures, reading row values sequentially (`matrix[r][c]`, then `matrix[r][c+1]`) is significantly faster than looping vertically down columns (`matrix[r][c]`, then `matrix[r+1][c]`). When writing matrix traversals, keep the innermost loop iterating over contiguous columns where possible.

---

### 5. Integration & Mastery

#### 🚨 Wrong Approach Contrast
*   **The Fallacy**: Attempting in-place rotation of elements using coordinate projection maps in a single loop traversal.
    *   *Why it fails*: When you write M[j][N-1-i] = M[i][j] directly, you overwrite elements before they are copied, corrupting adjacent cells. Attempting to track multiple active cell temporary variables results in deep indexing bugs.
    *   *The Fix*: Implement the rotation as a composition of simpler operations (transpose followed by horizontal row reflection). The code is modular, self-documenting, and guarantees zero data corruption.

#### 🧗 Practice Ladder
1.  **Transpose Matrix (Square)**: Symmetrically swap diagonal elements.
2.  **Rotate Clockwise / Counter-clockwise**: Composite diagonal transposings with horizontal/vertical reversals.
3.  **Spiral Matrix II**: Populate an empty array sequentially using shrinking boundary pointers.
4.  **Diagonal Traverse**: Trace paths with index parity checks (`row + col == diagonal_index`).
5.  **Dungeon Game**: Implement a backward-moving matrix DP with custom state constraints.

---

## 📅 Day 2: Bitwise Operations, Tricks, Subset Loops & Gray Codes

### 1. Context & Motivation
Bitwise manipulation lets you write high-performance programs by mapping data layouts directly to CPU registers. High-frequency systems, database storage engines, and compression codecs replace high-level collections with primitive integer structures. Performing arithmetic on raw bits reduces heap allocations to zero and leverages single-cycle CPU operations.

Real-world anchors:
*   **Memory Constraints**: Storing a set of boolean flags using a `Dictionary` is highly inefficient and creates significant Garbage Collector pressure. Storing interests using bit fields in a single 64-bit integer takes zero allocations.
*   **Fast Hardware Instructions**: CPUs have built-in execution circuits like `POPCNT` (population count) and `CLZ` (count leading zeros) to manipulate masks in a single clock cycle.

---

### 2. Mental Model
Think of an integer as a fixed-capacity boolean array indices 0 to 31 or 63.
*   Register index i corresponds to the boolean flag stored at the i-th bit: `(n >> i) & 1`.
*   We can update this flag using bitwise operations:

```text
Index Position:   [ 7 | 6 | 5 | 4 | 3 | 2 | 1 | 0 ]
Active Bitmasks:  [ 0 | 1 | 0 | 0 | 1 | 1 | 0 | 1 ] = Value 77
                                        ^
                                   Isolating Bit at Index 3
```

#### 🔌 Core Bit Operator Logic
*   **AND (`&`)**: Intersects active bits. Useful for masking operations: `n & mask`.
*   **OR (`|`)**: Unifies active bits. Useful for enabling flags: `n |= mask`.
*   **XOR (`^`)**: Detects differences. If you flip a bit twice, it returns to its original state.
*   **NOT (`~`)**: Symmetrically complements all bits.
*   **Shifts (`<<`, `>>`)**: Moves components left or right. Each shift is equivalent to multiplying or dividing by 2.

---

### 3. Mechanics

#### A. Common Bit Flags Utilities
```csharp
public static class BitTricks {
    // 1. Is Power of 2?
    public static bool IsPowerOfTwo(int n) {
        return n > 0 && (n & (n - 1)) == 0;
    }

    // 2. Clear lowest set bit (rightmost 1-bit)
    public static int ClearLowestSetBit(int n) {
        return n & (n - 1);
    }

    // 3. Isolate lowest set bit
    public static int IsolateLowestSetBit(int n) {
        return n & -n;
    }

    // 4. Check if i-th bit is set
    public static bool CheckBit(int n, int i) {
        return ((n >> i) & 1) == 1;
    }

    // 5. Set i-th bit
    public static int SetBit(int n, int i) {
        return n | (1 << i);
    }

    // 6. Clear i-th bit
    public static int ClearBit(int n, int i) {
        return n & ~(1 << i);
    }

    // 7. Toggle i-th bit
    public static int ToggleBit(int n, int i) {
        return n ^ (1 << i);
    }
}
```

#### B. Brian Kernighan's Popcount
To count the set bits in an integer, we can examine each bit one by one recursively (which takes O({bits}) time). Alternatively, we can use the `n &= n - 1` trick to clear the lowest set bit in each step. This runs in O({set bits}) steps:

```csharp
public static int PopCount(int n) {
    int count = 0;
    while (n > 0) {
        n = n & (n - 1); // Clears the lowest set bit
        count++;
    }
    return count;
}
```

#### C. Exhaustive Subset Enumeration (Submask Loops)
Using a bitmask from 0 to 2^N - 1 lets you enumerate all possible subsets of an array of size N.
To find all subsets (submasks) of a specific active bitmask, use this optimized loop:

```csharp
// Iterate over all submasks of a given mask
public static void EnumerateSubmasks(int mask) {
    int submask = mask;
    while (submask > 0) {
        // Process submask
        Console.WriteLine($"Submask: {Convert.ToString(submask, 2)}");
        
        // Step to next valid submask
        submask = (submask - 1) & mask;
    }
}
```

#### D. Gray Code Generation
A Gray code is a binary representation system where consecutive numbers differ by exactly one bit.
To convert an integer N to its Gray code representation, use this formula:
Gray(N) = n ^ (n >> 1)

---

### 4. Performance & Systems
*   **Computational Cost**: Bit operations (`&`, `|`, `^`, `<<`) execute in a single clock cycle on modern processors, making them faster than high-level boolean evaluations.
*   **Precision and Sign Bit Trap**: In 32-bit signed integers, shifting a 1 into index 31 alters the sign of the value because of Two's Complement representation. Always use `uint` (unsigned) or `long` (64-bit) structures when manipulating the most significant bits.

---

### 5. Integration & Mastery

#### 🚨 Wrong Approach Contrast
*   **The Fallacy**: Using heavy arrays or generic lists to track state membership in complex search operations instead of single-word bitmasks.
    *   *Why it fails*: Creating dynamic lists inside recursive loops forces high frequency heap allocations, which stalls the runtime garbage collector and destroys performance.
    *   *The Fix*: Represent state entries as a single integer bitmask. All inclusion checks take O(1) time with absolute zero memory allocations.

#### 🧗 Practice Ladder
1.  **Single Number**: Find the unique element in an array where all other elements appear twice (O(N) time, O(1) space using XOR).
2.  **Number of 1 Bits**: Implement Brian Kernighan popcounting.
3.  **Reverse Bits**: Symmetrically swap the bits in a 32-bit integer.
4.  **Subsets**: Generate all subsets of a set using mask integer shifts.
5.  **Gray Code**: Generate a sequence of Gray codes from 0 to 2^N - 1 recursively or iteratively.

---

## 📅 Day 3: Number Theory Basics, GCD, Primes & Modular Arithmetic

### 1. Context & Motivation
Many computational problems require finding modular remainders of large exponents or fast prime factorization. Standard integer types overflow quickly, and naive division algorithms are too slow for large numbers. Number theory provides deterministic algebraic properties that simplify complex searching problems.

Real-world anchors:
*   **Modular Arithmetic in Cryptography**: RSA and Diffie-Hellman encryption rely on the difficulty of factoring the product of two large prime numbers, using modular inverses and fast exponentiation.
*   **Hashing Algorithms**: Hash functions divide numerical keys by a prime number modulo M to distribute keys uniformly across buckets and minimize collisions.

---

### 2. Mental Model
*   **Divisibility and Factors**: Think of prime numbers as the fundamental building blocks of integers (Fundamental Theorem of Arithmetic).
*   **Modular Congruence**: Modular arithmetic wraps values around a fixed range (like a clock face). On a 12-hour clock, 10 + 5 == 3 +/-od{12}.

```text
                        11   0   1
                      10           2
                     9       o       3   <-- 12-hour Clock Analogy
                      8             4
                        7    6    5
```

---

### 3. Mechanics

#### A. Euclidean Greatest Common Divisor (GCD) & LCM
The Euclidean algorithm is a fast recursive method to find the greatest common divisor of two numbers:
gcd(a, b) = {gcd}(b, a \bmod b)

```csharp
public static long Gcd(long a, long b) {
    while (b > 0) {
        long temp = b;
        b = a % b;
        a = temp;
    }
    return Math.Abs(a);
}

public static long Lcm(long a, long b) {
    if (a == 0 || b == 0) return 0;
    // Divide first to prevent overflow
    return Math.Abs(a / Gcd(a, b) * b);
}
```

#### B. Sieve of Eratosthenes (Prime Generation)
To find all prime numbers up to N, create a boolean array representing prime locations. Mark all multiples of p starting from p^2 as composite:

```csharp
public static bool[] Sieve(int n) {
    bool[] isPrime = new bool[n + 1];
    Array.Fill(isPrime, true);
    if (n >= 0) isPrime[0] = false;
    if (n >= 1) isPrime[1] = false;
    
    for (int p = 2; p * p <= n; p++) {
        if (isPrime[p]) {
            // Mark multiples of p starting from p^2
            for (int i = p * p; i <= n; i += p) {
                isPrime[i] = false;
            }
        }
    }
    return isPrime;
}
```

#### C. Modular Arithmetic Laws
To prevent integer overflow when calculating products and sums of large coordinates, use these identities:
*   (A + B) \bmod M = ((A \bmod M) + (B \bmod M)) \bmod M
*   (A x B) \bmod M = ((A \bmod M) x (B \bmod M)) \bmod M
*   *Caveat*: (A / B) \bmod M \neq ((A \bmod M) / (B \bmod M)) \bmod M. You must multiply the numerator by the modular multiplicative inverse of the denominator.

#### D. Logarithmic Modular Exponentiation
Using standard loops to compute b^e \bmod m takes O(e) time, which overflows quickly. Fast exponentiation computes values in O(log e) time using binary decomposition:

```csharp
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
```

#### E. Modular Multiplicative Inverse
The modular inverse of a modulo M is an integer x such that:
(a * x) % M == 1
*   **Fermat's Little Theorem**: If p is a prime number and gcd(a, p) = 1, then a^{p-1} == 1 +/-od p.
*   Multiplying both sides by a^{-1} yields:
    a^{-1} == a^{p-2} +/-od p
*   For non-prime moduli, use the **Extended Euclidean Algorithm** to find the inverse.

---

### 4. Performance & Systems
*   **Algorithmic Complexities**:
    *   Euclidean GCD: O(log(min(a, b))) steps.
    *   Sieve of Eratosthenes: O(N log log N) time, O(N) space.
    *   Modular Exponentiation: O(log e) multiplications.
*   **Handling BigInteger**: Under extremely large cryptographic keys (e.g. 2048-bit primes), use custom base structures or built-in BigInteger structures to prevent bit truncation.

---

### 5. Integration & Mastery

#### 🚨 Wrong Approach Contrast
*   **The Fallacy**: Calculating `(A / B) % M` by dividing `A` by `B` first, or evaluating standard fractional values.
    *   *Why it fails*: Large intermediate values like `A` will overflow the float/double capacity, resulting in loss of precision. Additionally, floating-point numbers do not preserve exact modular remainders.
    *   *The Fix*: Find the modular multiplicative inverse of `B` modulo `M` (using Fermat's Little Theorem or the Extended Euclidean Algorithm), then compute `(A * ModInverse(B, M)) % M`.

#### 🧗 Practice Ladder
1.  **Euclidean GCD**: Find the greatest common divisor recursively.
2.  **Sieve of Eratosthenes**: Generate primes up to N.
3.  **Modular Powering**: Implement fast modular exponentiation.
4.  **Count Primes**: Find the number of primes smaller than N.
5.  **Modular Inverse**: Solve combinations C(N, K) \bmod P using modular inverses for factorials.

---

## 📅 Day 4: Advanced String Preprocessing, KMP, Tries & Aho-Corasick

### 1. Context & Motivation
Standard string pattern matching algorithms, such as looking for a pattern P of length M inside a text T of length N, have a worst-case time complexity of O(N * M). This quadratic complexity is too slow for large-scale applications like text search engines, gene sequence analyses, and real-time network hazard monitoring.
Advanced string algorithms use preprocessing tables and prefix tree structures to find matches in O(N + M) time. They scan the text sequentially without backtracking.

Real-world anchors:
*   **DNA Pattern Matching**: Finding gene sequences inside a chromosome with billions of base pairs requires linear-time matching algorithms.
*   **IP Address Routing**: Internet routers match destination IP prefixes against routing tables in O(L) time using compressed prefix trees.

---

### 2. Mental Model
Instead of backtracking the search pointer in the main text on a mismatch, compile the search pattern into a state transition table. The table preserves matching prefixes and indicates the next best fallback state.

```text
Pattern:  "A  B  A  B  C"
Index:     0  1  2  3  4
LPS:      [0, 0, 1, 2, 0]  <-- LPS table stores length of longest prefix matching proper suffix
```

*   **Trie**: Merges shared prefixes of multiple words into a single search tree to save space and speed up matches.
*   **Aho-Corasick Automaton**: Integrates a prefix search Trie with KMP failure links. This lets you search for multiple patterns simultaneously in a single linear pass over the text stream.

---

### 3. Mechanics

#### A. Knuth-Morris-Pratt (KMP) & LPS Table
The **Longest Prefix Suffix (LPS)** table stores the length of the longest proper prefix of P[0 \dots i] that is also a suffix of P[0 \dots i].

```csharp
public static int[] BuildLps(string pattern) {
    int[] lps = new int[pattern.Length];
    int len = 0; // Length of the previous longest prefix suffix
    int i = 1;
    lps[0] = 0; // lps[0] is always 0
    
    while (i < pattern.Length) {
        if (pattern[i] == pattern[len]) {
            len++;
            lps[i] = len;
            i++;
        } else {
            if (len > 0) {
                // Fallback to previous longest prefix suffix
                len = lps[len - 1];
            } else {
                lps[i] = 0;
                i++;
            }
        }
    }
    return lps;
}

public static List<int> KmpSearch(string text, string pattern) {
    var matchIndices = new List<int>();
    if (string.IsNullOrEmpty(pattern)) return matchIndices;
    
    int[] lps = BuildLps(pattern);
    int tPtr = 0; // Pointer for text
    int pPtr = 0; // Pointer for pattern
    
    while (tPtr < text.Length) {
        if (text[tPtr] == pattern[pPtr]) {
            tPtr++;
            pPtr++;
            
            if (pPtr == pattern.Length) {
                // Pattern match found
                matchIndices.Add(tPtr - pPtr);
                pPtr = lps[pPtr - 1]; // Fallback to check for overlapping matches
            }
        } else {
            if (pPtr > 0) {
                pPtr = lps[pPtr - 1];
            } else {
                tPtr++;
            }
        }
    }
    return matchIndices;
}
```

#### B. Trie (Prefix Tree) Implementation
A Trie matches words in O(L) time, where L is the length of the word, independent of the number of words stored in the dictionary.

```csharp
public class TrieNode {
    public Dictionary<char, TrieNode> Children { get; } = new();
    public bool IsEndOfWord { get; set; }
}

public class Trie {
    private readonly TrieNode _root = new();

    public void Insert(string word) {
        var current = _root;
        foreach (char ch in word) {
            if (!current.Children.ContainsKey(ch)) {
                current.Children[ch] = new TrieNode();
            }
            current = current.Children[ch];
        }
        current.IsEndOfWord = true;
    }

    public bool Search(string word) {
        var current = _root;
        foreach (char ch in word) {
            if (!current.Children.ContainsKey(ch)) return false;
            current = current.Children[ch];
        }
        return current.IsEndOfWord;
    }

    public bool StartsWith(string prefix) {
        var current = _root;
        foreach (char ch in prefix) {
            if (!current.Children.ContainsKey(ch)) return false;
            current = current.Children[ch];
        }
        return true;
    }
}
```

#### C. Radix Sorting (Strings and Integers)
Radix Sort sorts elements of stable structures without comparing them directly. It beats comparison-sorting limits by exploiting the digit representations of keys.
*   **LSD (Least Significant Digit)**: Sorts elements starting from the rightmost digit, moving left. It requires a stable sorting algorithm like counting sort for each pass. Excellent for fixed-length keys.
*   **MSD (Most Significant Digit)**: Sorts elements starting from the leftmost digit, moving right. It recursively partitions subarrays, making it ideal for sorting variable-length strings alphabetically.
*   **3-way String Quicksort**: Combines MSD Radix Sort with quicksort partitioning. It partitions the array into three segments: elements with characters smaller than the pivot, equal to the pivot, and larger than the pivot. This is highly efficient and uses less memory.

#### D. Aho-Corasick Algorithm
The **Aho-Corasick** algorithm builds a state machine by adding failure links to a standard prefix search Trie. If a character mismatch occurs while scanning a node, the failure link redirects the pointer of the state machine to the longest valid suffix of the pattern traversed so far. This lets you search for a set of dictionary patterns simultaneously in O({text length}) time.

---

### 4. Performance & Systems
*   **Asymptotic Complexities**:
    *   KMP: O(N + M) time. Preprocessing takes O(M) time, and search takes O(N) time.
    *   Trie: Insertion and Search take O(L) time, where L is the word length.
    *   Aho-Corasick: O(N + sum(M_i) + Z) search time, where Z is the count of matches.
*   **Memory Footprint and Prefix Compression**: Standard Tries have a high memory overhead because each node stores pointers to its children. Real-world systems use **Compressed Tries (Radix Trees)** to merge adjacent nodes with single children. This compresses paths, reduces pointer sizes, and improves cache performance.

---

### 5. Integration & Mastery

#### 🧗 Practice Ladder
1.  **Implement Trie (Prefix Tree)**: Build a standard Trie supporting insertion, search, and prefix matching.
2.  **KMP Substring Matcher**: Practice building the LPS table and search algorithm.
3.  **Radix Sort Implementation**: Implement LSD and MSD counting sorting algorithms.
4.  **Shortest Palindrome**: Find the shortest palindrome by adding characters to the front of a string (using KMP's LPS table).
5.  **Multi-Pattern Matcher**: Implement the basic Aho-Corasick failure links algorithm.

---

## 📅 Day 5: Advanced Number Theory, Totient & Chinese Remainder Theorem

### 1. Context & Motivation
When designing large systems, we often need to solve problems where parameters can exceed standard integer capacities. For example, find high-power combinations across different prime modular limits, or solve simultaneous congruence relations. Advanced number theory provides tools to solve these problems.

Real-world anchors:
*   **Secure Protocols**: The RSA cryptosystem relies on Euler's Totient function to calculate private exponents that are coprime to the encryption keys.
*   **High-Precision Distributed Calculations**: The Chinese Remainder Theorem lets you partition a large calculation into smaller modulo operations across copier pipelines. This lets you run calculations in parallel without numerical overflow.

---

### 2. Mental Model
*   **Euler's Totient Function** phi(N): Counts how many positive integers up to N are coprime to N (share no common factors other than 1).
*   **Chinese Remainder Theorem (CRT)**: If you know the remainders of an unknown integer X modulo several pairwise coprime numbers, you can determine X uniquely modulo the product of those numbers.

---

### 3. Mechanics

#### A. Euler's Totient Function phi(N)
The mathematical formula to calculate phi(N) is based on the prime factorization of N:
phi(N) = N * product(1 - 1/p for all distinct prime factors p dividing N)
Where p represents the distinct prime factors of N.

```csharp
public static long GetTotient(long n) {
    long result = n;
    long temp = n;
    for (long p = 2; p * p <= temp; p++) {
        if (temp % p == 0) {
            // Divide first to prevent integer overflow
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
```

#### B. Chinese Remainder Theorem (CRT) Solver
Given a system of modular equations:
*   x == a_1 +/-od{m_1}
*   x == a_2 +/-od{m_2}
*   \dots
*   x == a_k +/-od{m_k}
Where all m_i are pairwise coprime, the Chinese Remainder Theorem guarantees a unique solution for x modulo M = m_1 x m_2 x \dots x m_k.

To find x:
1.  Compute the total product M = product m_i.
2.  For each equation, calculate M_i = M / m_i.
3.  Find y_i such that M_i * y_i == 1 +/-od{m_i} (modular inverse of M_i modulo m_i).
4.  Reconstruct the solution:
    x = sum(a_i * M_i * y_i) (mod M)

```csharp
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
        if (g != 1) return -1; // Inverse doesn't exist
        return (x % m + m) % m;
    }

    public static long Solve(long[] a, long[] m) {
        long totalM = 1;
        for (int i = 0; i < m.Length; i++) {
            totalM *= m[i];
        }
        
        long x = 0;
        for (int i = 0; i < a.Length; i++) {
            long intermediateM = totalM / m[i];
            long inverse = ModInverse(intermediateM, m[i]);
            if (inverse == -1) return -1; // Inconsistent system
            
            // Reconstruct the unique solution step-by-step
            long term = (a[i] * intermediateM) % totalM;
            term = (term * inverse) % totalM;
            x = (x + term) % totalM;
        }
        return x;
    }
}
```

---

### 4. Performance & Systems
*   **Euler's Totient**: Finding the value of phi(N) using trial factorization runs in O(sqrt(N)) time. Precomputing totients for all values up to N using a sieve-like method runs in O(N log log N) time.
*   **CRT Engine**: Solving k simultaneous modular equations runs in O(k log M) steps, where M is the product of all moduli.

---

### 5. Integration & Mastery

#### 🧗 Practice Ladder
1.  **Totient Evaluation**: Implement the O(sqrt(N)) totient function using prime division.
2.  **Extended Euclidean Algorithm**: Find modular inverses for non-prime moduli.
3.  **Modular Modular Equations**: Write a solver for two modular equations.
4.  **Chinese Remainder Solver**: Build a generic CRT solver for K modular equations.

---

## 🔁 Integration & Performance Checklist

When designing performance-critical systems, combine these techniques to optimize your code:
1.  **Bit-Packing Matrix States**: If your matrix is small (N <= 25), store state information (like visited coordinates) as bits in an integer mask. This avoids allocating heap memory inside your search loops.
2.  **Sieve Precomputation**: Instead of checking for primes repeatedly during execution, use the Sieve of Eratosthenes to precompute primes once at startup. This speeds up subsequent queries to O(1) time.
3.  **Modular Inverse Arrays**: When computing large combinatorics tables under modular limits, precalculate modular inverses for factorials once. This speeds up combinations queries to O(1) time.
4.  **Trie Path Compressions**: When implementing dictionary-heavy Tries, compress nodes with single children into a single string path. This reduces memory usage and improves cache localization.

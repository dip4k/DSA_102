# 🐍 Week 14 Extended Python Complete Reference v13

This reference guide provides highly optimized, readable, and idiomatic Python implementations for Week 14's core algorithm patterns. All code blocks leverage built-in Python optimization operations and conform to strict static-typing standards.

---

## 🧮 1. Matrix Rotations, Transpositions & Traversal

### Transpose & Clockwise 90° Rotation
To rotate an N x N matrix clockwise 90 degrees in-place, first transpose the matrix by swapping elements across the main diagonal, then reverse each vertical row.

```python
def transpose_in_place(matrix: list[list[int]]) -> None:
    """Swaps matrix elements symmetrically across the primary diagonal in-place.
    
    Time Complexity: O(N^2)
    Space Complexity: O(1)
    """
    n = len(matrix)
    for i in range(n):
        for j in range(i + 1, n):
            matrix[i][j], matrix[j][i] = matrix[j][i], matrix[i][j]


def rotate_clockwise_90(matrix: list[list[int]]) -> None:
    """Rotates an N x N 2D grid clockwise 90 degrees in-place.
    
    Time Complexity: O(N^2)
    Space Complexity: O(1)
    """
    if not matrix or not matrix[0]:
        return
        
    # Step 1: Transpose elements
    transpose_in_place(matrix)
    
    # Step 2: Reverse each row
    for row in matrix:
        row.reverse()
```

### Spiral Order Walk
To traverse an R x C matrix in spiral order, use four bounding pointers to track active boundaries dynamically. This avoids complex coordinate calculations.

```python
def spiral_order(matrix: list[list[int]]) -> list[int]:
    """Returns elements of an r x c matrix walked in spiral order.
    
    Time Complexity: O(R * C)
    Space Complexity: O(1) auxiliary space (excluding result list)
    """
    if not matrix or not matrix[0]:
        return []

    top, bottom = 0, len(matrix) - 1
    left, right = 0, len(matrix[0]) - 1
    result = []

    while top <= bottom and left <= right:
        # 1. Traverse Right (Left to Right along top boundary)
        for col in range(left, right + 1):
            result.append(matrix[top][col])
        top += 1

        # 2. Traverse Down (Top to Bottom along right boundary)
        for row in range(top, bottom + 1):
            result.append(matrix[row][right])
        right -= 1

        # 3. Traverse Left (Right to Left along bottom boundary if still valid)
        if top <= bottom:
            for col in range(right, left - 1, -1):
                result.append(matrix[bottom][col])
            bottom -= 1

        # 4. Traverse Up (Bottom to Top along left boundary if still valid)
        if left <= right:
            for row in range(bottom, top - 1, -1):
                result.append(matrix[row][left])
            left += 1

    return result
```

---

## 🔌 2. Bitwise Utilities & State Operations

```python
def is_power_of_two(n: int) -> bool:
    """Constant time evaluation validating power of two status.
    
    Time Complexity: O(1)
    """
    return n > 0 and (n & (n - 1)) == 0


def brian_kernighan_popcount(n: int) -> int:
    """Counts active bits by clearing the lowest set bit in each step.
    
    Time Complexity: O(Set Bits)
    """
    count = 0
    while n > 0:
        n &= n - 1  # Clears the lowest set bit
        count += 1
    return count


def submasks_enumeration(mask: int) -> list[int]:
    """Retrieves all nested active submasks of a given bitmask in decreasing order.
    
    Time Complexity: O(2^K) where K is active bits
    """
    submasks = []
    sub = mask
    while sub > 0:
        submasks.append(sub)
        sub = (sub - 1) & mask  # Algebraic decrement intersection
    submasks.append(0)  # Add empty set boundary
    return submasks


def integer_to_gray(n: int) -> int:
    """Calculates Gray code representation of value n.
    
    Time Complexity: O(1)
    """
    return n ^ (n >> 1)
```

---

## 📐 3. Sieve of Eratosthenes & Modular Mathematics

```python
def generate_primes_up_to(limit: int) -> list[bool]:
    """Generates boolean primality lookup array up to limit.
    
    Time Complexity: O(N log log N)
    Space Complexity: O(N)
    """
    if limit < 0:
        return []
    is_prime = [True] * (limit + 1)
    if limit >= 0:
        is_prime[0] = False
    if limit >= 1:
        is_prime[1] = False

    for p in range(2, int(limit**0.5) + 1):
        if is_prime[p]:
            # Eliminate multiples starting from p^2
            for m in range(p * p, limit + 1, p):
                is_prime[m] = False
    return is_prime


def compute_gcd(a: int, b: int) -> int:
    """Retrieves Greatest Common Divisor recursively using Euclidean algorithm.
    
    Time Complexity: O(log(min(a, b)))
    """
    while b > 0:
        a, b = b, a % b
    return abs(a)


def modulus_power(base: int, exp: int, mod: int) -> int:
    """Fast modular exponentiation calculating (base^exp) % mod.
    
    Time Complexity: O(log exp)
    """
    if mod == 1:
        return 0
    res = 1
    base %= mod
    while exp > 0:
        if exp & 1 == 1:
            res = (res * base) % mod
        base = (base * base) % mod
        exp >>= 1
    return res


def modular_inverse(val: int, prime_mod: int) -> int:
    """Finds modular multiplicative inverse of val modulo prime_mod.
    
    Time Complexity: O(log prime_mod)
    """
    if val % prime_mod == 0:
        raise ValueError("No inverse exists since val is a multiple of modular base.")
    # Applying Fermat's Little Theorem: val^(P-2) = val^-1 (mod P)
    return modulus_power(val, prime_mod - 2, prime_mod)
```

---

## 🧵 4. KMP String Matcher & Preprocessed Trie

```python
def kmp_pattern_positions(text: str, pattern: str) -> list[int]:
    """Standard KMP pattern search returning 0-based starting indexes.
    
    Time Complexity: O(N + M)
    Space Complexity: O(M)
    """
    if not text or not pattern:
        return []

    # LPS generation
    lps = [0] * len(pattern)
    length, i = 0, 1
    while i < len(pattern):
        if pattern[i] == pattern[length]:
            length += 1
            lps[i] = length
            i += 1
        elif length > 0:
            length = lps[length - 1]
        else:
            lps[i] = 0
            i += 1

    # Search implementation
    matched_indices = []
    t_ptr = p_ptr = 0
    while t_ptr < len(text):
        if text[t_ptr] == pattern[p_ptr]:
            t_ptr += 1
            p_ptr += 1
            if p_ptr == len(pattern):
                matched_indices.append(t_ptr - p_ptr)
                p_ptr = lps[p_ptr - 1]  # Reset using fallback index
        elif p_ptr > 0:
            p_ptr = lps[p_ptr - 1]
        else:
            t_ptr += 1

    return matched_indices


class TrieNode:
    def __init__(self):
        self.children: dict[str, TrieNode] = {}
        self.is_end_of_word: bool = False


class Trie:
    def __init__(self):
        self.root = TrieNode()

    def insert(self, word: str) -> None:
        """Inserts string path into the Trie.
        
        Time Complexity: O(L)
        """
        if not word:
            return
        current = self.root
        for ch in word:
            if ch not in current.children:
                current.children[ch] = TrieNode()
            current = current.children[ch]
        current.is_end_of_word = True

    def search(self, word: str) -> bool:
        """Searches for exact match string in the prefix tree.
        
        Time Complexity: O(L)
        """
        if not word:
            return False
        node = self._get_node_path(word)
        return node is not None and node.is_end_of_word

    def starts_with(self, prefix: str) -> bool:
        """Verifies if a prefix exists in the Trie.
        
        Time Complexity: O(L)
        """
        if not prefix:
            return False
        return self._get_node_path(prefix) is not None

    def _get_node_path(self, target: str) -> TrieNode | None:
        current = self.root
        for ch in target:
            if ch not in current.children:
                return None
            current = current.children[ch]
        return current


class AhoCorasickNode:
    def __init__(self):
        self.children: dict[str, AhoCorasickNode] = {}
        self.fail: AhoCorasickNode | None = None
        self.output: list[str] = []


class AhoCorasick:
    def __init__(self):
        self.root = AhoCorasickNode()

    def insert(self, word: str) -> None:
        """Inserts a pattern into the Trie."""
        if not word:
            return
        current = self.root
        for ch in word:
            if ch not in current.children:
                current.children[ch] = AhoCorasickNode()
            current = current.children[ch]
        current.output.append(word)

    def build_automaton(self) -> None:
        """Constructs failure links using Breadth-First Search (BFS) queue."""
        from collections import deque
        queue = deque()
        for child in self.root.children.values():
            child.fail = self.root
            queue.append(child)

        while queue:
            current = queue.popleft()
            for ch, child_node in current.children.items():
                fail_state = current.fail
                while fail_state is not None and ch not in fail_state.children:
                    fail_state = fail_state.fail

                child_node.fail = fail_state.children[ch] if fail_state else self.root
                child_node.output.extend(child_node.fail.output)
                queue.append(child_node)

    def search_text(self, text: str) -> dict[str, list[int]]:
        """Scans raw streaming text sequentially returning list of match indices."""
        results = {}
        current = self.root

        for idx, ch in enumerate(text):
            while current is not None and ch not in current.children:
                current = current.fail

            current = current.children[ch] if current else self.root
            for pattern in current.output:
                start_index = idx - len(pattern) + 1
                if pattern not in results:
                    results[pattern] = []
                results[pattern].append(start_index)

        return results


def construct_suffix_array(s: str) -> list[int]:
    """Generates the suffix array for a given string.
    
    Time Complexity: O(N log N)
    """
    n = len(s)
    suffixes = sorted([(i, s[i:]) for i in range(n)], key=lambda x: x[1])
    return [suffix[0] for suffix in suffixes]


def build_lcp_array(s: str, sa: list[int]) -> list[int]:
    """Builds the Longest Common Prefix (LCP) array using Kasai's algorithm.
    
    Time Complexity: O(N)
    """
    n = len(s)
    lcp = [0] * n
    rank = [0] * n
    for i in range(n):
        rank[sa[i]] = i

    h = 0
    for i in range(n):
        if rank[i] > 0:
            j = sa[rank[i] - 1]
            while i + h < n and j + h < n and s[i + h] == s[j + h]:
                h += 1
            lcp[rank[i]] = h
            if h > 0:
                h -= 1
    return lcp


def lsd_radix_sort(arr: list[str], string_length: int) -> None:
    """Performs stable Least Significant Digit (LSD) Radix Sort.
    
    Time Complexity: O(N * L)
    """
    n = len(arr)
    R = 256
    aux = [""] * n

    for d in range(string_length - 1, -1, -1):
        count = [0] * (R + 1)
        for i in range(n):
            count[ord(arr[i][d]) + 1] += 1
        for r in range(R):
            count[r + 1] += count[r]
        for i in range(n):
            aux[count[ord(arr[i][d])]] = arr[i]
            count[ord(arr[i][d])] += 1
        for i in range(n):
            arr[i] = aux[i]


def sort_3way(arr: list[str]) -> None:
    """Sorts strings using 3-way String Quicksort."""
    def _get_char_at(s: str, d: int) -> int:
        return ord(s[d]) if d < len(s) else -1

    def _sort(lo: int, hi: int, d: int) -> None:
        if hi <= lo:
            return

        lt, gt = lo, hi
        v = _get_char_at(arr[lo], d)
        i = lo + 1

        while i <= gt:
            t = _get_char_at(arr[i], d)
            if t < v:
                arr[lt], arr[i] = arr[i], arr[lt]
                lt += 1
                i += 1
            elif t > v:
                arr[i], arr[gt] = arr[gt], arr[i]
                gt -= 1
            else:
                i += 1

        _sort(lo, lt - 1, d)
        if v >= 0:
            _sort(lt, gt, d + 1)
        _sort(gt + 1, hi, d)

    _sort(0, len(arr) - 1, 0)

---

## 🧮 5. Advanced Totients & Chinese Remainder Solvers

```python
def get_totient(n: int) -> int:
    """Calculates Euler's Totient function phi(N) using prime factors.
    
    Time Complexity: O(sqrt(N))
    """
    result = n
    temp = n
    p = 2
    while p * p <= temp:
        if temp % p == 0:
            # Sieve out factor components
            result -= result // p
            while temp % p == 0:
                temp //= p
        p += 1
    if temp > 1:
        result -= result // temp
    return result


def precompute_totients(limit: int) -> list[int]:
    """Pre-calculates Euler's Totients for all values up to limit.
    
    Time Complexity: O(N log log N)
    """
    phi = list(range(limit + 1))
    for p in range(2, limit + 1):
        if phi[p] == p:
            for m in range(p, limit + 1, p):
                phi[m] -= phi[m] // p
    return phi


def safe_multiply(a: int, b: int, mod: int) -> int:
    """Performs modular multiplication (a * b) % mod preventing register overflow.
    
    Time Complexity: O(log B) | Space Complexity: O(1)
    """
    res = 0
    a %= mod
    while b > 0:
        if b & 1 == 1:
            res = (res + a) % mod
        a = (a * 2) % mod
        b >>= 1
    return res


def extended_gcd(a: int, b: int) -> tuple[int, int, int]:
    """Extended Euclidean algorithm return (gcd, x, y) where ax + by = gcd."""
    if b == 0:
        return a, 1, 0
    g, x1, y1 = extended_gcd(b, a % b)
    x = y1
    y = x1 - (a // b) * y1
    return g, x, y


def mod_inverse_generic(a: int, m: int) -> int:
    """Calculates modular inverse of a modulo m using Extended Euclidean."""
    g, x, _ = extended_gcd(a, m)
    if g != 1:
        return -1  # Inverse does not exist
    return (x % m + m) % m


def solve_crt(a: list[int], m: list[int]) -> int:
    """Solves a simultaneous system of congruence equations modulo coprime moduli.
    
    Time Complexity: O(K log M) where K is number of congruence bases
    """
    total_m = 1
    for base in m:
        total_m *= base

    result = 0
    for val, base in zip(a, m):
        intermediate_m = total_m // base
        inverse = mod_inverse_generic(intermediate_m, base)
        if inverse == -1:
            return -1  # No solution exists
            
        term = safe_multiply(val, intermediate_m, total_m)
        term = safe_multiply(term, inverse, total_m)
        result = (result + term) % total_m
        
    return result
```
        for ch in word:
            if ch not in current.children:
                current.children[ch] = TrieNode()
            current = current.children[ch]
        current.is_end_of_word = True

    def search(self, word: str) -> bool:
        """Searches for exact match string in the prefix tree.
        
        Time Complexity: O(L)
        """
        if not word:
            return False
        node = self._get_node_path(word)
        return node is not None and node.is_end_of_word

    def starts_with(self, prefix: str) -> bool:
        """Verifies if a prefix exists in the Trie.
        
        Time Complexity: O(L)
        """
        if not prefix:
            return False
        return self._get_node_path(prefix) is not None

    def _get_node_path(self, target: str) -> TrieNode | None:
        current = self.root
        for ch in target:
            if ch not in current.children:
                return None
            current = current.children[ch]
        return current
```

---

## 🧮 5. Advanced Totients & Chinese Remainder Solvers

```python
def get_totient(n: int) -> int:
    """Calculates Euler's Totient function phi(N) using prime factors.
    
    Time Complexity: O(sqrt(N))
    """
    result = n
    temp = n
    p = 2
    while p * p <= temp:
        if temp % p == 0:
            # Sieve out factor components
            result -= result // p
            while temp % p == 0:
                temp //= p
        p += 1
    if temp > 1:
        result -= result // temp
    return result


def extended_gcd(a: int, b: int) -> tuple[int, int, int]:
    """Extended Euclidean algorithm return (gcd, x, y) where ax + by = gcd."""
    if b == 0:
        return a, 1, 0
    g, x1, y1 = extended_gcd(b, a % b)
    x = y1
    y = x1 - (a // b) * y1
    return g, x, y


def mod_inverse_generic(a: int, m: int) -> int:
    """Calculates modular inverse of a modulo m using Extended Euclidean."""
    g, x, _ = extended_gcd(a, m)
    if g != 1:
        return -1  # Inverse does not exist
    return (x % m + m) % m


def solve_crt(a: list[int], m: list[int]) -> int:
    """Solves a simultaneous system of congruence equations modulo coprime moduli.
    
    Time Complexity: O(K log M) where K is number of congruence bases
    """
    total_m = 1
    for base in m:
        total_m *= base

    result = 0
    for val, base in zip(a, m):
        intermediate_m = total_m // base
        inverse = mod_inverse_generic(intermediate_m, base)
        if inverse == -1:
            return -1  # No solution exists
            
        term = (val * intermediateM) % totalM if 'intermediateM' in locals() else (val * intermediate_m) % total_m
        term = (term * inverse) % total_m
        result = (result + term) % total_m
        
    return result
```

```

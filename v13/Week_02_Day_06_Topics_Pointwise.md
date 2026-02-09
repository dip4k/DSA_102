# WEEK 2, DAY 6: STRINGS & NUMBERS: CONCEPTUAL UNDERSTANDING

## STRINGS: THE COMPLETE STORY

### What is a String? (Definition & Representation)
- Definition: sequence of characters, indexed, immutable in modern languages
- Storage model: contiguous character array in memory with metadata
- Character-by-character layout:
  - C#/.NET: object header (8B) + length (4B) + cached hash (4B) + UTF-16 array
  - Java: similar structure with cached hash optimization
  - C: null-terminated array, no explicit length field
  - Python: variable-width encoding and internal caching
- Memory overhead: 5-character string costs ~38 bytes total

### String Immutability: Why It Exists
- Thread-safety: multiple threads safely share references without synchronization
- Hash stability: string contents never change, enabling consistent hashing
- Interning: JVM and CLR intern strings for memory optimization
- What breaks if violated: concurrent access, hash table corruption, cache invalidation
- Performance implication: immutability forces allocation on concatenation

### String Operations Analysis
- Indexing (Access):
  - Formula: address = base + header_size + (index × char_size)
  - Complexity: O(1) constant time
  - Real performance: 1-2 CPU cycles if in L1/L2 cache
- Concatenation:
  - Process: allocate new memory, copy characters, create new object
  - Complexity: O(n) where n = total length of result
  - Real cost: 10-100 nanoseconds per character
  - The loop trap: s = s + "x" in loop is O(n²)
    - Iteration 1: allocate 2, copy 1 → cost 2
    - Iteration 2: allocate 3, copy 2 → cost 3
    - Total: 2+3+4+...+n = O(n²)
- Comparison (Equality):
  - Reference equality: O(1) if same object address
  - Value equality: O(n) character-by-character comparison
  - Optimization: cached hash code allows early rejection
- Substring & Slicing:
  - Legacy (copy-based): O(n) allocate and copy all characters
  - Modern (view-based): O(1) create view, O(n) when modified

### Character Encoding
- ASCII:
  - 7-bit encoding (0-127), single byte per character
  - Limited to English characters
  - Simple and efficient
- Unicode:
  - Scalable standard covering all languages and scripts
  - Codepoint: abstract number (U+0041 for 'A')
  - Multiple encodings for same codepoint:
    - UTF-8: variable length (1-4 bytes), dense for ASCII
    - UTF-16: typically 2 bytes per character (C# and Java)
    - UTF-32: always 4 bytes per character
- Implications:
  - Indexing by byte position vs character position differs
  - File corruption if encoding mismatch
  - Performance varies by encoding density

### StringBuilder Pattern
- Problem: concatenation in loops is O(n²)
- Solution: growable mutable buffer that reallocates infrequently
- How it works:
  - Maintains internal char array with capacity
  - Appends add to buffer without copying old data
  - Reallocation uses doubling strategy: 2× capacity when full
  - Final ToString() creates immutable string from buffer
- Complexity: O(1) amortized per append
- When to use: building strings in loops, concatenating many strings, formatting

### String Variations & Trade-offs
- Immutable String:
  - Mutability: no
  - Storage: heap object
  - Use case: general purpose, threading
  - Trade-off: slow concatenation
- StringBuilder:
  - Mutability: yes
  - Storage: growable buffer
  - Use case: building in loops
  - Trade-off: extra object, manual ToString()
- Char Array:
  - Mutability: yes
  - Storage: stack/heap
  - Use case: processing, sorting
  - Trade-off: manual length tracking
- Rope:
  - Mutability: immutable
  - Storage: tree of strings
  - Use case: large texts, editors
  - Trade-off: complex structure
- Interned String:
  - Mutability: no
  - Storage: string pool
  - Use case: memory optimization
  - Trade-off: limited benefit, pool overhead

---

## NUMBERS - REPRESENTATION & OVERFLOW

### Number Systems: Foundations
- Decimal (Base 10):
  - Familiar notation
  - 10 unique digits (0-9)
  - Example: 255 = 2×10² + 5×10¹ + 5×10⁰
- Binary (Base 2):
  - Computer native representation
  - 2 unique digits (0-1)
  - Example: 255 = 1111 1111₂ = 128+64+32+16+8+4+2+1
  - Conversion: repeatedly divide by 2, track remainders
- Hexadecimal (Base 16):
  - Compact notation for binary
  - 16 unique digits (0-9, A-F)
  - Example: 255 = FF₁₆ = 15×16 + 15
  - Advantage: each hex digit = 4 binary digits

### Unsigned Integers (8-bit example)
- Range: 0 to 255 (2⁸ - 1)
- Representation: each bit represents power of 2
  - 0 = 0000 0000
  - 127 = 0111 1111
  - 255 = 1111 1111
- Overflow behavior: addition wrapping
  - 255 + 1 = 256, exceeds 8-bit range → wraps to 0
  - Silent failure: no exception thrown

### Signed Integers (8-bit, Two's Complement)
- Range: -128 to +127 (not -127 to +127)
- Representation method:
  - Positive: standard binary (MSB = 0)
  - Negative: flip all bits, add 1 (MSB = 1)
- Examples:
  - 5 = 0000 0101
  - -5: flip → 1111 1010, add 1 → 1111 1011
  - Verify: 5 + (-5) = 0 with overflow ignored
- Why two's complement:
  - Addition/subtraction work naturally without special handling
  - Single zero representation (unlike sign-magnitude)
  - Efficient hardware implementation
- Special value: -128:
  - 1000 0000₂ represents -128 (range asymmetry)
  - No +128 equivalent
  - Adding 127 + 1 = -128 (correct overflow behavior)

### Integer Overflow & Underflow
- What happens: arithmetic result exceeds representable range
- Silently wraps: no exception, result is modulo 2^bits
- Real-world consequences:
  - Y2K bug: 2-digit year "00" → 1900 instead of 2000
  - Financial software: rounding errors compound
  - Security: integer overflow in buffer overflow exploits
- Detection strategy:
  - Before multiply: check a ≤ (INT_MAX - b) / c
  - Use checked arithmetic (C#): throw exception
  - Validate input bounds strictly

### Floating-Point Representation (IEEE 754, 32-bit)
- Components: Sign (1-bit) | Exponent (8-bit) | Mantissa (23-bit)
- Formula: Value = (-1)^S × 1.Mantissa × 2^(Exponent-127)
- Example 0.5:
  - Exponential form: 1.0 × 2^(-1) = 0.5
  - Sign = 0 (positive)
  - Exponent = 126 (126 - 127 = -1)
  - Mantissa = 0 (represents 1.0)
- Precision issues:
  - Not all decimal numbers representable exactly
  - 0.1 has repeating binary expansion, limited to 23 bits
  - 0.1 + 0.2 ≠ 0.3 (accumulates rounding error)
  - Solution: epsilon-based comparison |a - b| < 0.0001
- Special values:
  - +∞, -∞: overflow or division by zero
  - NaN: invalid operations (0/0, sqrt(-1))
  - Signed zero: +0 and -0 for directed rounding

---

## STRING-NUMBER CONVERSIONS

### String-to-Integer (atoi Algorithm)
- Input: "12345", "  -42abc", "0"
- Output: integer or error signal
- Algorithm steps:
  1. Skip leading whitespace
  2. Check for optional sign (+/-)
  3. Extract digits character by character: result = result × 10 + digit
  4. Stop at first non-digit
  5. Check overflow before each multiplication
  6. Apply sign to result
- Overflow detection:
  - Before multiplying by 10: check if result > (INT_MAX - digit) / 10
  - If true, multiplication would overflow
  - Clamp to INT_MAX or return error
- Edge cases:
  - All whitespace → return 0
  - No digits at all → return 0
  - Mixed valid/invalid: parse valid prefix, ignore invalid suffix
  - Sign without digits: behavior varies (0 or error)

### Integer-to-String (itoa Algorithm)
- Input: 12345, -456, 0
- Output: "12345", "-456", "0"
- Algorithm steps:
  1. Handle zero separately (return "0")
  2. Extract digits right-to-left using modulo:
     - digit = number % 10 (rightmost digit)
     - number = number / 10 (remove rightmost digit)
  3. Collect digits in order: [5, 4, 3, 2, 1]
  4. Reverse: [1, 2, 3, 4, 5]
  5. Convert each digit to char: 1 → '1', 5 → '5'
  6. Prepend negative sign if applicable
- Complexity: O(log₁₀ n) where n = number

### Base Conversions (Any-to-Any)
- Decimal to binary:
  - Repeatedly divide by 2, track remainders
  - Read remainders bottom-to-top
  - Example: 42 = 101010₂
- Decimal to hexadecimal:
  - Repeatedly divide by 16, map digits to A-F
  - Read bottom-to-top
  - Example: 255 = FF₁₆
- General pattern:
  - For base b, divide by b repeatedly
  - Map digit values to symbol set (0-9, A-Z)
  - Remainder at each step gives next digit

---

## PERFORMANCE ANALYSIS

### Beyond Big-O: Performance Reality
- String indexing:
  - Theory: O(1)
  - Real: 1-2 cycles (L1 cache hit)
  - Measurement: 2-5 nanoseconds
- String concatenation:
  - Theory: O(n)
  - Real: 10-100 ns per character
  - Includes: allocation, copy, GC setup
- atoi parsing:
  - Theory: O(n)
  - Real: 20-100 ns per digit
  - Predictable and linear
- String comparison:
  - Theory: O(n)
  - Real: early termination if strings differ
  - Cached hash helps rejection

### Memory & GC Implications
- Allocation cost: 10-50 ns per allocation
- GC pressure: concatenation creates temporaries
- GC pause: stop-the-world can reach milliseconds
- Cache effects: sequential access cache-friendly, random access defeats prefetching

### Real-World Systems
- Text Editor (VSCode):
  - Problem: edit large files efficiently
  - Solution: rope data structure (tree of strings)
  - Benefit: O(log n) insert/delete instead of O(n)
- Database (PostgreSQL):
  - Problem: string comparisons constant in indexes
  - Solution: hash caching, short string optimization
  - Benefit: fast comparison, reduced allocations
- Web Server (Node.js):
  - Problem: encode/decode constantly
  - Solution: Buffer class for raw bytes, explicit encoding
  - Benefit: zero-copy operations

### Failure Modes & Robustness
- Unbounded string input:
  - User sends 1GB string → memory exhaustion crash
  - Mitigation: validate input length early
- Integer overflow in array indexing:
  - Unvalidated arithmetic → wrong location writes
  - Mitigation: use long for calculations, bounds check
- Character encoding mismatch:
  - UTF-8 file read as Latin-1 → mojibake
  - Mitigation: always specify encoding explicitly
- Loop concatenation:
  - O(n²) behavior kills performance
  - Mitigation: use StringBuilder


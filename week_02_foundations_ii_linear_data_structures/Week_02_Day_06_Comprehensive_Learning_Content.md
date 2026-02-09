# 📚 WEEK 2 DAY 6: STRINGS & NUMBERS - COMPREHENSIVE LEARNING CONTENT

---

## 🎯 COURSE OVERVIEW

**Duration:** 120 minutes  
**Difficulty:** Intermediate 🟡  
**Topics:** Strings, Numbers, Conversions, Performance  
**Learning:** Memory representation, algorithms, real-world implications

---

# PART 1: STRINGS - THE COMPLETE STORY (50 minutes)

## TOPIC 1: WHAT IS A STRING? (Definition & Representation)

### Concept Overview
A string is a **sequence of characters stored contiguously in memory**, with metadata describing its properties. Unlike abstract strings in mathematics, computational strings are physical objects with size constraints, encoding rules, and performance characteristics.

### Definition
```
String = Sequence of characters + Metadata (length, type info, cached hash)
       = Contiguous array of character values in memory
       = Immutable (in most modern languages)
```

### Memory Representation

#### C#/.NET String Layout
```
String s = "Hello";

Memory Layout (Stack and Heap):
┌─────────────────────────────────────────────────────┐
│  STACK: s (reference variable)                      │
│  ┌───────────────────────────────────────────────┐  │
│  │ s → 0x7000 (address of string object)        │  │
│  └───────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────┘
            │
            │ points to
            ↓
┌──────────────────────────────────────────────────────┐
│ HEAP: String Object at 0x7000                        │
├──────────────────────────────────────────────────────┤
│ Object Header (16 bytes)                             │
│ ├─ Type Info (8 bytes)  → "System.String"           │
│ ├─ GC Flags (4 bytes)   → generation info           │
│ └─ Sync Block (4 bytes) → lock info                 │
├──────────────────────────────────────────────────────┤
│ Length (4 bytes)        → 5                          │
├──────────────────────────────────────────────────────┤
│ Hash Code (4 bytes)     → cached_hash_value         │
├──────────────────────────────────────────────────────┤
│ Character Array (UTF-16, 10 bytes)                   │
│ ├─ Index 0: 'H' → 0x0048                            │
│ ├─ Index 1: 'e' → 0x0065                            │
│ ├─ Index 2: 'l' → 0x006C                            │
│ ├─ Index 3: 'l' → 0x006C                            │
│ └─ Index 4: 'o' → 0x006F                            │
├──────────────────────────────────────────────────────┤
│ Alignment Padding (4 bytes)                          │
└──────────────────────────────────────────────────────┘

Total Size: 16 (header) + 4 (length) + 4 (hash) + 10 (chars) + 4 (padding)
          = 38 bytes for 5-character string
```

#### Comparison: Different Languages

| Language | Header | Length | Hash | Encoding | Notes |
|----------|--------|--------|------|----------|-------|
| **C#** | 16B | 4B | 4B | UTF-16 (2B/char) | Object-based |
| **Java** | 12B | in array | 4B | UTF-16 (2B/char) | Similar to C# |
| **C** | - | - | - | ASCII/UTF-8 | Null-terminated |
| **Python** | variable | variable | - | flexible | Variable width |

### Memory Overhead Example

```
String "ABC" in C#:

Without string: Just 3 characters = 6 bytes (3 × 2)

With string object:
├─ Header: 16 bytes
├─ Length field: 4 bytes
├─ Hash code: 4 bytes
├─ Character data: 6 bytes (3 × 2 UTF-16)
└─ Padding: 2 bytes
─────────────
TOTAL: 32 bytes

Overhead ratio: 32 / 6 = 5.3x larger!
Small strings have large relative overhead.
```

### Key Insight

> 💡 **Strings are not lightweight primitives—they are heap-allocated objects with significant memory overhead. This matters when creating millions of strings or storing many small strings.**

---

## TOPIC 2: STRING IMMUTABILITY: WHY IT EXISTS

### Definition
**Immutability** means once a string is created, its contents cannot be modified. Attempting to change a character results in a compile-time error.

```csharp
string s = "Hello";
s[0] = 'J';  // ❌ COMPILE ERROR: String is immutable

string s2 = s.ToUpper();  // ✅ OK: Returns NEW string
// s remains "Hello"
// s2 is new object "HELLO"
```

### Why Immutability Exists

#### 1. Thread-Safety
```
Scenario: Multiple threads reading same string

With Mutability (UNSAFE):
Thread 1: Reading s = "Hello"...
Thread 2: Modifying s = "Jello" at same time
Thread 1: Gets corrupted data, crashes

With Immutability (SAFE):
Thread 1: Reading s = "Hello"
Thread 2: Cannot modify, creates new string "Jello"
Thread 1: Always gets "Hello", safe
```

#### 2. Hash Stability
```
Hash Tables rely on consistent hashing:

With Mutability (UNSAFE):
s = "Alice"
hash = ComputeHash(s) = 12345
dictionary[s] = address_of_data

... (later)
s = "Bob"  // Modified!
hash_new = 67890  // Different!
dictionary[12345] ≠ "Bob"
Cannot find the data!

With Immutability (SAFE):
s = "Alice"
hash = ComputeHash(s) = 12345 (computed once, cached)
dictionary[s] = address_of_data
No change possible, always finds data
```

#### 3. String Interning
```
JVM/CLR optimization: Share identical strings

With Immutability:
s1 = "Hello"  → address 0x5000
s2 = "Hello"  → reuse 0x5000 (same object!)
s1 == s2  // true, same reference

With Mutability:
s1 = "Hello"  → address 0x5000
s2 = "Hello"  → reuse 0x5000
s2[0] = 'J'  // s1 is now "Jello"!
Corrupted both!
```

### What Breaks if Violated

```
Concurrent Modification:
├─ Data races between threads
├─ Hash table corruption
├─ Cache invalidation
└─ Silent data loss

Example: Financial System
Account name: "Alice"
String stored in multiple places
One thread changes to "Bob"
Other threads see inconsistent names
Transactions route to wrong account!
```

### Performance Implication

```
Benefit: Thread-safe, no synchronization needed
Cost: Immutability forces allocation on concatenation

s = s + "!"  // Creates new object, discards old
           // Memory pressure, GC work
```

---

## TOPIC 3: STRING OPERATIONS ANALYSIS

### Operation 1️⃣: String Indexing (Access)

#### How It Works

```
Accessing s[2] in "Hello":

Step 1: Get base address of string object
        base = 0x5000

Step 2: Skip metadata to reach character array
        header_size = 16 + 4 + 4 = 24 bytes
        char_array_start = 0x5000 + 24 = 0x5018

Step 3: Calculate offset for index
        char_size = 2 bytes (UTF-16)
        offset = index × char_size = 2 × 2 = 4 bytes

Step 4: Get final address
        address = 0x5018 + 4 = 0x501C

Step 5: Fetch 2 bytes from address 0x501C
        Result: 'l' (0x006C in UTF-16)
```

#### Performance

```
Complexity: O(1) constant time
Real performance:
├─ L1 cache hit: 1-2 CPU cycles
├─ L2 cache hit: ~10 cycles
├─ L3 cache hit: ~40 cycles
└─ Main memory: 100+ cycles

Rule of thumb: If accessing sequential characters (cache-friendly)
               → ~5 nanoseconds per character
               If random access (cache-unfriendly)
               → ~100 nanoseconds per character
```

#### Memory Access Flow

```
┌──────────────────────────────────┐
│ Access s[i]                      │
├──────────────────────────────────┤
│ 1. Calculate address              │
│    addr = base + header + i*size  │
├──────────────────────────────────┤
│ 2. Check bounds (optional)        │
│    if (i < length) proceed        │
├──────────────────────────────────┤
│ 3. Fetch from memory              │
│    value = *(addr)                │
├──────────────────────────────────┤
│ 4. Return to caller               │
│    return (char)value             │
└──────────────────────────────────┘
```

---

### Operation 2️⃣: String Concatenation

#### How It Works

```
s1 = "Hi"     (length 2)
s2 = "!!!"    (length 3)
s3 = s1 + s2

Step 1: Calculate total length
        total_length = 2 + 3 = 5

Step 2: Allocate new memory
        allocate(5 × 2 + header_size)
        allocate(10 + 24) = 34 bytes
        New address: 0x8000

Step 3: Initialize metadata
        Write header at 0x8000
        Write length (5) at 0x8018
        Write hash code at 0x801C

Step 4: Copy characters from s1
        Copy "Hi" (4 bytes) to 0x8020

Step 5: Copy characters from s2
        Copy "!!!" (6 bytes) to 0x8024

Step 6: Create reference
        s3 → 0x8000

Result:
s1 → 0x5000 (still exists)
s2 → 0x6000 (still exists)
s3 → 0x8000 (new object)
```

#### Complexity Analysis

```
Time Complexity: O(n) where n = total length

Real Performance:
├─ Allocation: 50-200 nanoseconds
├─ Copy: 10-20 nanoseconds per character
├─ GC setup: 10-50 nanoseconds
└─ Total for 1000-char result: ~30 microseconds
```

#### Memory Timeline

```
Before:  s1="Hi" @0x5000, s2="!!!" @0x6000
         Available: 0x7000+

After:   s1="Hi" @0x5000
         s2="!!!" @0x6000
         s3="Hi!!!" @0x8000  ← NEW allocation
         Old s1/s2 still in memory (may be GC'd)
```

---

### The Loop Trap: O(n²) Problem

#### The Problem

```csharp
string result = "";
for (int i = 0; i < 1000; i++)
{
    result += i.ToString() + ",";
}
```

#### Why It's O(n²)

```
Iteration 0: "" + "0," = allocate 2B, copy 0, copy 2 → cost 2
Iteration 1: "0," + "1," = allocate 4B, copy 2, copy 2 → cost 4
Iteration 2: "0,1," + "2," = allocate 6B, copy 4, copy 2 → cost 6
Iteration 3: "0,1,2," + "3," = allocate 8B, copy 6, copy 2 → cost 8
...
Iteration 999: previous + "999," = allocate 2000B, copy 1998, copy 4 → cost 2000

Total work: 2 + 4 + 6 + 8 + ... + 2000
          = 2 × (1 + 2 + 3 + ... + 1000)
          = 2 × (1000 × 1001 / 2)
          = 1,001,000 character operations
          = O(n²) where n = 1000
```

#### Visual: Scaling Problem

```
Input size → Total operations (actual measurements)

n=100:    ~5,000 ops      (n²=10,000)
n=1,000:  ~500,000 ops    (n²=1,000,000)
n=10,000: ~50,000,000 ops (n²=100,000,000) ← Gets very slow

Performance degradation:
10x input → 100x slower (quadratic relationship)
```

---

### Operation 3️⃣: String Comparison

#### Reference Equality

```
if (s1 == s2)  // Check if SAME object

s1 = "Hello"  @0x5000
s2 = "Hello"  @0x5000  (same reference, likely from interning)

s1 == s2:
Step 1: Compare memory addresses
        0x5000 == 0x5000?
Step 2: Return true immediately (O(1))

Cost: 1-2 CPU cycles
```

#### Value Equality

```
if (s1.Equals(s2))  // Check if SAME CONTENT

s1 = "Hello"  @0x5000
s2 = "Hello"  @0x6000  (different object, different address)

Process:
Step 1: Compare lengths
        if (s1.length != s2.length) return false  → O(1)
Step 2: Compare hashes (if cached)
        if (cached_hash1 != cached_hash2) return false → O(1)
Step 3: Character-by-character comparison
        for i in 0..length:
            if (s1[i] != s2[i]) return false
        return true

Complexity: O(n) worst case (all characters match)
            O(1) best case (length or hash differs)
```

#### Optimization: Hash Code Caching

```
Without caching:
s1 = "Alice"
hash_new = ComputeHash(s1)  // Recalculate every time
           → hash=12345, cost=100 ns

With caching:
s1 = "Alice"
hash = s1.GetHashCode()  // Return cached value
       → hash=12345, cost=2 ns (50x faster!)

C# caches hash code in string object:
First call: compute and cache
Subsequent calls: return cached value
```

---

### Operation 4️⃣: Substring & Slicing

#### Legacy Approach (Copy-Based)

```
s = "Hello World"
substring = s.Substring(0, 5)  // "Hello"

Process:
├─ Allocate new string of length 5
├─ Copy characters 0-4 to new string
└─ Create new object

Complexity: O(n) where n = substring length
Cost: High memory usage, allocation overhead
```

#### Modern Approach (View-Based)

```
Some languages use views/slices:
s = "Hello World"
substring = s[0:5]  // Creates view to original

Process:
├─ Create view object (just metadata)
├─ Points to same underlying data
└─ No character copy

Complexity: O(1) for creation
Trade-off: Data lifetime tied to original string
           Problematic if original gets GC'd
```

---

## TOPIC 4: CHARACTER ENCODING

### ASCII: The Original Standard

#### Definition & Range

```
ASCII (American Standard Code for Information Exchange)
├─ 7-bit encoding
├─ 128 unique characters (0-127)
└─ One character = 1 byte (1 bit wasted)

Character mappings:
'A' = 65 = 0100 0001
'Z' = 90 = 0101 1010
'a' = 97 = 0110 0001
'z' = 122 = 0111 1010
'0' = 48 = 0011 0000
'9' = 57 = 0011 1001
' ' = 32 = 0010 0000  (space)
'\n' = 10 = 0000 1010 (newline)
```

#### Limitations

```
ASCII covers only English and basic punctuation.
Cannot represent:
├─ Accented characters (é, ñ, ü)
├─ Currency symbols (€, ¥, ₹)
├─ Non-Latin scripts (中文, العربية, Ελληνικά)
└─ Emojis (😀, 🎉, ❤️)

Solution needed: Unicode
```

---

### Unicode: The Modern Solution

#### Core Concept

```
Unicode assigns a unique code point to every character:

Codepoint = Abstract number identifying a character
           U+0041 = 'A'
           U+00E9 = 'é'
           U+4E00 = '一' (Chinese)
           U+1F600 = '😀' (emoji)

Range: U+0000 to U+10FFFF (over 1 million characters)
```

#### Three Major Encodings

**1. UTF-8 (Variable-length, 1-4 bytes)**

```
Design: Backward compatible with ASCII

Encoding rules:
├─ ASCII (0-127): 1 byte
│  0xxxxxxx
│  Example: 'A' = 01000001
│
├─ Latin (128-2047): 2 bytes
│  110xxxxx 10xxxxxx
│  Example: 'é' = 11000011 10101001
│
├─ Most languages (2048-65535): 3 bytes
│  1110xxxx 10xxxxxx 10xxxxxx
│  Example: '中' = 11100100 10111000 10100000
│
└─ Rare characters: 4 bytes
   11110xxx 10xxxxxx 10xxxxxx 10xxxxxx
   Example: '😀' = 11110000 10011111 10011000 10000000

Advantages:
├─ Compact for English (1 byte per char)
├─ Backward compatible with ASCII
└─ Self-synchronizing (can detect encoding errors)

Disadvantages:
├─ Variable length (harder to index)
└─ More processing overhead

Usage: Web, Linux, UTF-8 is default everywhere
```

**2. UTF-16 (Typically 2 bytes, sometimes 4)**

```
Design: Balanced between efficiency and simplicity

Basic plane (U+0000 to U+FFFF): 2 bytes directly
Supplementary planes: 4 bytes using surrogate pairs

Examples:
'A' (U+0041) = 00 41 (big-endian)
             = 41 00 (little-endian)
'é' (U+00E9) = 00 E9
'中' (U+4E00) = 4E 00
'😀' (U+1F600) = D8 3D DE 00 (surrogate pair)

Advantages:
├─ Most common characters use 2 bytes (short)
├─ Simpler than UTF-8
└─ Direct indexing in basic plane

Disadvantages:
├─ Surrogate pairs complicate supplementary characters
├─ Not backward compatible with ASCII
└─ Wastes space for English text (2 bytes per char)

Usage: C#, Java, Windows internally
```

**3. UTF-32 (Fixed 4 bytes per character)**

```
All characters encoded in exactly 4 bytes:

'A' (U+0041) = 00 00 00 41
'é' (U+00E9) = 00 00 00 E9
'中' (U+4E00) = 00 00 4E 00
'😀' (U+1F600) = 00 01 F6 00

Advantages:
├─ Fixed width, easy indexing
├─ Simple implementation
└─ Every character same size

Disadvantages:
├─ Wastes space (4 bytes even for ASCII)
└─ Rarely used except in internal processing

Usage: Some text editors, rarely in storage
```

---

### Implications: Why Encoding Matters

#### Problem: Encoding Mismatch

```
File saved as UTF-8:
Bytes: C3 A9  (UTF-8 for 'é')

Read as Latin-1:
Interpretation: Ã © (two separate chars!)
Visual result: "mojibake" (incorrect characters)

Common disasters:
├─ Email headers displayed as ????
├─ Web pages with garbled text
├─ Database corruption
└─ International data loss
```

#### Performance: Different Densities

```
Text: "Hello, 中文" (English + Chinese)

UTF-8:
"Hello, " = 7 bytes (ASCII)
"中文" = 6 bytes (3 per character)
Total: 13 bytes for 9 characters

UTF-16:
"Hello, " = 14 bytes (7 × 2)
"中文" = 4 bytes (2 × 2)
Total: 18 bytes for 9 characters

For English-heavy: UTF-8 wins (7 bytes)
For Chinese-heavy: UTF-16 wins (4 bytes per char)
```

#### String Length Confusion

```
s = "café"

Length in characters: 4
Length in UTF-8 bytes: 5 (c=1 + a=1 + f=1 + é=2)
Length in UTF-16 bytes: 8 (4 × 2)

s.length in C#: 4 (character count)
s.Length in bytes: 8 (UTF-16, 2 per char)

When iterating, must be careful:
for (int i = 0; i < s.Length; i++)  // Safe (character iteration)
for (int i = 0; i < s.Length * 2; i++) // Wrong! (byte iteration)
```

---

## TOPIC 5: STRINGBUILDER PATTERN

### The Problem: Loop Concatenation

```csharp
// ❌ ANTIPATTERN
string result = "";
for (int i = 0; i < 1000; i++)
{
    result = result + i + ",";
}
// Allocates ~1000 times, copies millions of characters
// Real time: ~3-5 seconds
```

### The Solution: StringBuilder

```csharp
// ✅ PATTERN
StringBuilder sb = new StringBuilder();
for (int i = 0; i < 1000; i++)
{
    sb.Append(i).Append(",");
}
string result = sb.ToString();
// Allocates ~10 times, copies ~2000 characters
// Real time: ~1 millisecond
```

### How StringBuilder Works

#### Internal Structure

```
StringBuilder sb = new StringBuilder();

Internal state:
┌─────────────────────────────────────────┐
│ StringBuilder Object                    │
├─────────────────────────────────────────┤
│ char[] buffer          → [capacity 16]  │
│ int length             → 0              │
│ int capacity           → 16             │
│ char defaultChar       → '\0'           │
│ StringBuilderCache ... → optimization   │
└─────────────────────────────────────────┘
```

#### Append Operation (Mutable Buffer)

```
sb.Append("Hello")

Before:
buffer: [___,___,___,___,...,___]  (16 slots)
length: 0

After:
buffer: [H,e,l,l,o,___,___,___,...,___]
length: 5

Cost: O(1) - just copy to buffer, no allocation
```

#### Reallocation (Doubling Strategy)

```
sb.Append("World after 11 more chars")
Current: length=5, capacity=16, still fits

sb.Append("One more " × 5)  // Total 50 chars
Current: length=5, capacity=16 → FULL
Trigger reallocation:

Step 1: Check if full
        if (length >= capacity) reallocate

Step 2: Allocate new buffer
        new_capacity = capacity × 2 = 32
        new_buffer = new char[32]

Step 3: Copy existing data
        for i in 0..length:
            new_buffer[i] = buffer[i]

Step 4: Switch buffers
        buffer = new_buffer
        capacity = 32

Step 5: Continue appending
        buffer[5] = 'W'
        buffer[6] = 'o'
        ...

Complexity of this operation: O(n) but amortized O(1)
```

#### Final ToString() Conversion

```
sb.ToString()

Creates immutable string from buffer:
Step 1: Allocate new string of length 'length'
Step 2: Copy buffer[0..length] to string
Step 3: Return immutable string

Cost: O(length) one-time conversion
```

### Amortized Analysis

```
Appending n characters:

Without StringBuilder:
Total allocations: n
Total copies: 1 + 2 + 3 + ... + n = n(n+1)/2
Complexity: O(n²)
Time: ~5 seconds for 1000 appends

With StringBuilder (doubling):
Total allocations: log₂(n) ≈ 10
Total copies: n + (n/2) + (n/4) + ... = 2n
Complexity: O(n)
Time: ~1 millisecond for 1000 appends

Speedup: 5000x faster!
```

---

## TOPIC 6: STRING VARIATIONS & TRADE-OFFS

### Variation 1: Immutable String

```csharp
string s = "Hello";
// Cannot modify: s[0] = 'J' is compile error
// New string from operations: s = s.ToUpper()

Pros:
├─ Thread-safe (immutable)
├─ Can be cached/interned
├─ Simple semantics
└─ Hash table friendly

Cons:
├─ Expensive concatenation
├─ Memory overhead (header, hash)
└─ Garbage from temporary strings
```

### Variation 2: StringBuilder

```csharp
StringBuilder sb = new StringBuilder();
sb.Append("Hello");
sb[0] = 'J';  // Modifiable
string s = sb.ToString();  // Convert to immutable

Pros:
├─ Efficient building (O(1) amortized)
├─ Lower GC pressure
└─ Perfect for loops

Cons:
├─ Extra object overhead
├─ Manual ToString() required
└─ Mutable, not thread-safe
```

### Variation 3: Char Array

```csharp
char[] chars = "Hello".ToCharArray();
chars[0] = 'J';  // Modifiable
string s = new string(chars);  // Convert

Pros:
├─ Fine-grained control
├─ Can sort/rearrange
└─ Low overhead if already array

Cons:
├─ Manual length tracking
├─ No built-in methods
└─ Error-prone
```

### Variation 4: Rope (Advanced)

```
Tree structure for very large strings:

       ┌─────────────┐
       │Root (len=20)│
       └──────┬──────┘
              │
        ┌─────┴─────┐
        │            │
    ┌───┴──┐     ┌──┴──┐
    │Rope(10)    │Rope(10)
    │"Hello"     │"World"
    └────────     ──────┘

Operations:
├─ Insert: O(log n)
├─ Delete: O(log n)
└─ Access: O(log n)

Trade-off: Complex, but handles gigabyte files
Usage: Text editors (VSCode), Git
```

---

# PART 2: NUMBERS - REPRESENTATION & OVERFLOW (45 minutes)

## TOPIC 7: NUMBER SYSTEMS: FOUNDATIONS

### Decimal (Base 10)

#### How It Works

```
Decimal uses 10 unique digits: 0, 1, 2, 3, 4, 5, 6, 7, 8, 9

Position notation:
  d_n × 10^n + d_(n-1) × 10^(n-1) + ... + d_1 × 10¹ + d_0 × 10⁰

Example: 255
  2 × 10² + 5 × 10¹ + 5 × 10⁰
= 2 × 100 + 5 × 10 + 5 × 1
= 200 + 50 + 5
= 255
```

### Binary (Base 2)

#### How It Works

```
Binary uses 2 unique digits: 0, 1

Each digit represents a power of 2:

Position notation:
  b_n × 2^n + b_(n-1) × 2^(n-1) + ... + b_1 × 2¹ + b_0 × 2⁰

Example: 11111111₂
  1×2⁷ + 1×2⁶ + 1×2⁵ + 1×2⁴ + 1×2³ + 1×2² + 1×2¹ + 1×2⁰
= 128 + 64 + 32 + 16 + 8 + 4 + 2 + 1
= 255₁₀

Binary place values:
2⁷ = 128
2⁶ = 64
2⁵ = 32
2⁴ = 16
2³ = 8
2² = 4
2¹ = 2
2⁰ = 1
```

#### Decimal to Binary Conversion

```
Method: Repeatedly divide by 2, track remainders

Convert 42 to binary:

42 ÷ 2 = 21 remainder 0
21 ÷ 2 = 10 remainder 1
10 ÷ 2 = 5  remainder 0
5 ÷ 2 = 2   remainder 1
2 ÷ 2 = 1   remainder 0
1 ÷ 2 = 0   remainder 1

Read remainders bottom-to-top: 101010₂

Verify: 32 + 8 + 2 = 42 ✓
```

---

### Hexadecimal (Base 16)

#### How It Works

```
Hexadecimal uses 16 unique digits: 0-9, A-F

Digit values:
0-9 = 0-9
A = 10
B = 11
C = 12
D = 13
E = 14
F = 15

Position notation:
  h_n × 16^n + h_(n-1) × 16^(n-1) + ... + h_1 × 16¹ + h_0 × 16⁰

Example: FF₁₆
  F × 16¹ + F × 16⁰
= 15 × 16 + 15 × 1
= 240 + 15
= 255₁₀
```

#### Decimal to Hexadecimal Conversion

```
Method: Repeatedly divide by 16, map to A-F

Convert 255 to hexadecimal:

255 ÷ 16 = 15 remainder 15
15 ÷ 16 = 0  remainder 15

Read bottom-to-top, map 15→F: FF₁₆

Verify: 15×16 + 15 = 255 ✓
```

#### Binary to Hexadecimal (Direct Conversion)

```
Each hex digit = 4 binary digits

Binary:  11111111
Group:   1111 1111
Hex:       F    F
Result:    FF₁₆

This is why hex is used for addresses:
0x FFFF FFFF is much shorter than 1111111111111111111111111111111111₂
```

---

### Conversion Chart

```
Decimal | Binary    | Hex
--------|-----------|-----
0       | 0000      | 0
1       | 0001      | 1
2       | 0010      | 2
3       | 0011      | 3
4       | 0100      | 4
5       | 0101      | 5
6       | 0110      | 6
7       | 0111      | 7
8       | 1000      | 8
9       | 1001      | 9
10      | 1010      | A
11      | 1011      | B
12      | 1100      | C
13      | 1101      | D
14      | 1110      | E
15      | 1111      | F
16      | 10000     | 10
255     | 11111111  | FF
256     | 100000000 | 100
```

---

## TOPIC 8: UNSIGNED INTEGERS (8-bit example)

### Definition & Range

```
Unsigned integers: Can only represent non-negative values

8-bit unsigned range:
Minimum: 0
Maximum: 255 (2⁸ - 1)

Why maximum is 2⁸ - 1?
All 8 bits set to 1: 11111111₂ = 255₁₀
2⁸ = 256 possible values (0-255)
```

### Binary Representation

```
Value | Binary
------|----------
0     | 0000 0000
1     | 0000 0001
2     | 0000 0010
127   | 0111 1111
128   | 1000 0000
254   | 1111 1110
255   | 1111 1111
```

### Overflow Behavior

#### What Happens

```
Overflow: Result exceeds maximum representable value

Example: 255 + 1

  1111 1111 (255)
+       1
-----------
1 0000 0000 (256 in 9 bits)
  ^
  overflow bit
  (discarded, only 8 bits kept)

Result: 0000 0000 = 0
        (silently wraps to 0, no exception)
```

#### Why This Happens

```
Computer arithmetic is modular:
(a + b) mod 2^bits = result

For 8-bit unsigned:
(255 + 1) mod 256 = 256 mod 256 = 0

This is by design for performance
(no overflow checking overhead)
```

#### Real-World Impact

```
Example: Network packet sequence numbers (32-bit)

Max value: 2³² - 1 = 4,294,967,295
If you increment continuously:
... 4,294,967,294
    4,294,967,295 (max)
    0 (wraps around!)

This is expected and handled in network protocols
(sequence number wraps, both sides know)
```

---

## TOPIC 9: SIGNED INTEGERS (Two's Complement)

### The Problem: How to Represent Negative Numbers?

```
8-bit gives 256 possible values (00000000 to 11111111)
If unsigned: 0 to 255
If signed: How to split between positive and negative?

Sign-magnitude (naive):
├─ Use 1 bit for sign, 7 bits for magnitude
├─ -5 = 1 0000 101 (sign bit + magnitude 5)
├─ Problem: Two zeros (00000000 = +0, 10000000 = -0)
├─ Problem: Addition needs special logic

Two's complement (better):
├─ Use all 8 bits for value
├─ Clever encoding: positive + negative + zero
├─ Addition works naturally!
└─ This is the standard (used everywhere)
```

### Two's Complement Encoding

#### Process: How to Encode a Negative Number

```
To represent -5:

Step 1: Start with positive 5
        5 = 0000 0101

Step 2: Flip all bits (bitwise NOT)
        ~5 = 1111 1010

Step 3: Add 1
        1111 1010 + 1 = 1111 1011

Result: -5 = 1111 1011 in two's complement
```

#### Decoding: How to Read a Negative Number

```
Given bit pattern: 1111 1011

Step 1: Recognize negative (MSB = 1)
        1111 1011 is negative

Step 2: Flip all bits
        ~1111 1011 = 0000 0100

Step 3: Add 1
        0000 0100 + 1 = 0000 0101

Step 4: Interpret as decimal
        0000 0101 = 5

Result: Original was -5
```

#### Verification: Addition Check

```
5 + (-5) should equal 0:

  0000 0101 (5)
+ 1111 1011 (-5)
-----------
1 0000 0000

In 8-bit arithmetic, the 9th bit (carry) is discarded:
Result: 0000 0000 = 0 ✓

This is why two's complement works: addition is natural!
```

### 8-bit Signed Range

```
Range: -128 to +127 (not -127 to +127)

Representation:
Positive values (MSB=0):
0   = 0000 0000
1   = 0000 0001
...
127 = 0111 1111

Negative values (MSB=1):
-1  = 1111 1111
-2  = 1111 1110
...
-127 = 1000 0001
-128 = 1000 0000 ← SPECIAL (no positive counterpart!)

Why asymmetry?
8 bits = 256 values = 128 positive + 128 negative + 1 zero
Since 0 is in positive side:
Positive: 0 to 127 (128 values)
Negative: -128 to -1 (128 values)
Total: 256 values ✓
```

### Special Case: -128

```
-128 = 1000 0000 (special in 8-bit two's complement)

Why special?
1. No positive counterpart
   (127 is max positive)
2. Cannot negate:
   -(-128) = -128 (stays same!)
   This is a bug waiting to happen.

Example bug:
int x = -128;
int negated = -x;  // Still -128 (overflow!)
// Expected: 128, Got: -128
```

---

## TOPIC 10: INTEGER OVERFLOW & UNDERFLOW

### What Is Overflow?

```
Overflow: Arithmetic result exceeds representable range

Example (8-bit signed):
127 + 1 = 128

  0111 1111 (127)
+ 0000 0001
-----------
1 0000 0000

Interpretation in 8-bit:
1000 0000 = -128 (MSB is 1, so negative!)

Result: 127 + 1 = -128 (completely wrong!)
```

### Why It Happens

```
Computer arithmetic is modular arithmetic

For 8-bit signed (range -128 to 127):
(127 + 1) mod 256 = 128 mod 256 = 128
Interpret 128 as signed → -128

For 32-bit signed (range -2³¹ to 2³¹-1):
(2147483647 + 1) mod 2³² = 2147483648 mod 2³²
                           = -2147483648

Wraps around silently!
```

### Real-World Consequences

#### Disaster 1: Y2K Bug (1999-2000)

```
Problem: Programs stored year as 2-digit number

Code:
int year = ReadYearFromFile();  // "00"
if (year < 50) year += 2000;    // Assume 2000s
else year += 1900;              // Assume 1900s

When year="00":
year < 50? Yes
year += 2000
Result: 2000 ✓

But many systems used single digit (0-99)
Some treated 00 as 1900!
Airplanes, power plants, banks affected
Estimated cost: $600 billion worldwide
```

#### Disaster 2: Ariane 5 Rocket (1996)

```
Cause: Integer overflow in guidance system

Code:
int velocity = ReadVelocity();  // Expected 16-bit value
accelerometer_data = (int16_t) velocity;  // Cast to 16-bit

Actual velocity: 64000 (16-bit max = 32767)
After cast: overflow!
Interpreted as: -3535 (wrapped around)
Guidance system thought rocket was going backward
Attempted correction crashed rocket 40 seconds after launch
Cost: $370 million
```

#### Disaster 3: Financial Software Rounding

```
Compound interest calculation:

int balance = 1000000;  // $10,000.00 (cents)
int interest_rate = 1005;  // 0.5% per month
for (int i = 0; i < 120; i++) {
    balance = balance * interest_rate / 1000;
}

After many iterations:
balance overflows (exceeds INT_MAX)
Wraps to negative value
Customer gets credited thousands instead of paying interest!
```

---

### Overflow Detection

#### Strategy 1: Check Before Operation

```csharp
// Multiply safely
const int INT_MAX = 2147483647;
int a = 1000000;
int b = 3000;

// Check: a × b ≤ INT_MAX?
if (a <= INT_MAX / b)  // Safe
{
    int result = a * b;
}
else  // Would overflow
{
    HandleError("Multiplication would overflow");
}
```

#### Strategy 2: Use Checked Arithmetic

```csharp
// C# checked keyword
try
{
    int result = checked(int.MaxValue + 1);  // Throws!
}
catch (OverflowException ex)
{
    Console.WriteLine("Overflow detected!");
}
```

#### Strategy 3: Use Larger Type

```csharp
// Use long instead of int
long a = 2000000000;
long b = 2;
long result = a * b;  // 4000000000, no overflow
int final = (int) result;  // Now check if fits
if (final == result)
    Console.WriteLine("Fits in int");
else
    Console.WriteLine("Doesn't fit in int");
```

#### Strategy 4: Input Validation

```csharp
// Validate user input at boundaries
int[] array = new int[10];
int index = ReadUserInput();

if (index < 0 || index >= array.Length)
{
    throw new ArgumentException("Index out of range");
}
// Safe to use index
```

---

## TOPIC 11: FLOATING-POINT REPRESENTATION (IEEE 754)

### 32-bit Single Precision Format

```
Bit layout: [Sign (1)] [Exponent (8)] [Mantissa (23)]

Example: 0x3F800000
         0 01111111 00000000000000000000000
         └─────────────────────────────────┘
                IEEE 754 format

Formula: Value = (-1)^Sign × 1.Mantissa × 2^(Exponent-127)
```

### Example 1: Encoding 0.5

```
Target: 0.5

Step 1: Express as binary exponential
        0.5 = 1.0 × 2^(-1)

Step 2: Extract components
        Sign = 0 (positive)
        Mantissa = 0 (represents 1.0)
        Exponent = -1 + 127 = 126

Step 3: Convert to binary
        Sign = 0 (1 bit)
        Exponent = 126 = 01111110 (8 bits)
        Mantissa = 0 (23 bits of zeros)

Step 4: Assemble
        0 01111110 00000000000000000000000
        0x3F000000 in hexadecimal

Verify:
(-1)^0 × 1.0 × 2^(126-127) = 1.0 × 2^(-1) = 0.5 ✓
```

### Example 2: Encoding 5.5

```
Target: 5.5

Step 1: Express in binary exponential
        5.5 = 101.1₂ (binary)
            = 1.011₂ × 2^2

Step 2: Extract components
        Sign = 0 (positive)
        Mantissa = .011₂ (after decimal point)
        Exponent = 2 + 127 = 129

Step 3: Convert exponent to binary
        129 = 10000001 (8 bits)

Step 4: Assemble
        0 10000001 01100000000000000000000
        = 0x40B00000

Verify:
(-1)^0 × 1.011₂ × 2^2 = 101.1₂ = 5.5 ✓
```

---

### Precision Issues: Why 0.1 + 0.2 ≠ 0.3

```
Problem: Binary cannot exactly represent 0.1

0.1 in decimal is irrational in binary (repeating):
0.1₁₀ = 0.0001100110011...₂ (repeating)

Limited to 23 bits of mantissa:
0.1₁₀ ≈ 0.00011001100110011001101₂ (truncated)

Similarly for 0.2:
0.2₁₀ ≈ 0.00110011001100110011010₂ (truncated)

When added:
0.1 (truncated) + 0.2 (truncated) = 0.30000000000000004...

Float result: 0.30000001 (not exactly 0.3)
```

### Special Values

```
Infinity:
├─ Exponent: all 1s (11111111)
├─ Mantissa: all 0s
├─ Represents: too large to fit

NaN (Not a Number):
├─ Exponent: all 1s (11111111)
├─ Mantissa: non-zero (any pattern)
├─ Represents: invalid operations (0/0, sqrt(-1))

Signed Zero:
├─ +0.0: 0 00000000 00000000000000000000000
├─ -0.0: 1 00000000 00000000000000000000000
├─ Mathematically equal but bit patterns differ
├─ Used for directed rounding
```

---

### Comparison: Floating-Point Pitfalls

```csharp
// ❌ WRONG
float a = 0.1f;
float b = 0.2f;
if (a + b == 0.3f)
    Console.WriteLine("Equal");  // Never happens!

// ✅ CORRECT
if (Math.Abs(a + b - 0.3f) < 0.0001f)
    Console.WriteLine("Close enough");
```

---

# PART 3: STRING-NUMBER CONVERSIONS (25 minutes)

## TOPIC 12: STRING-TO-INTEGER (atoi Algorithm)

### Algorithm Overview

```
Input: String like "  -42abc"
Output: Integer -42 (or error)

Process:
1. Skip leading whitespace
2. Check optional sign (+/-)
3. Extract digits until non-digit
4. Check for overflow
5. Apply sign
6. Return result
```

### Step-by-Step Walkthrough

```
Input: "  -42abc"

Step 1: Initialize
        result = 0, sign = 1, i = 0

Step 2: Skip whitespace
        s[0] = ' ' → skip
        s[1] = ' ' → skip
        s[2] = '-' → exit loop, i = 2

Step 3: Check sign
        s[2] = '-' → sign = -1, i = 3

Step 4: Extract digits
        s[3] = '4' → digit, result = 0×10 + 4 = 4
                  → check overflow: 4 ≤ (INT_MAX-4)/10? Yes
                  → i = 4
        s[4] = '2' → digit, result = 4×10 + 2 = 42
                  → check overflow: 42 ≤ (INT_MAX-2)/10? Yes
                  → i = 5
        s[5] = 'a' → not digit, exit loop

Step 5: Apply sign
        result = 42 × (-1) = -42

Output: -42
```

### Overflow Detection

```
Before multiplying by 10:

Check: result > (INT_MAX - digit) / 10

Why this works:
(INT_MAX - digit) / 10 is the largest value where
result × 10 + digit won't overflow

Example:
INT_MAX = 2147483647
digit = 7
threshold = (2147483647 - 7) / 10 = 214748364

If result > 214748364:
result × 10 + 7 > INT_MAX (overflow!)

If result ≤ 214748364:
result × 10 + 7 ≤ INT_MAX (safe)
```

### Edge Cases

```
Input → Output

"0"           → 0
"  "          → 0 (all whitespace)
"+-42"        → 0 (invalid: two signs)
"2147483647"  → 2147483647 (max int)
"2147483648"  → 2147483647 (overflow, clamped)
""            → 0 (empty)
"-2147483648" → -2147483648 (min int)
"-2147483649" → -2147483648 (overflow, clamped)
"abc123"      → 0 (no digits)
```

---

## TOPIC 13: INTEGER-TO-STRING (itoa Algorithm)

### Algorithm Overview

```
Input: Integer like -12345
Output: String "-12345"

Process:
1. Handle zero specially
2. Extract digits right-to-left (using modulo)
3. Reverse collected digits
4. Convert each digit to character
5. Prepend sign if negative
6. Return string
```

### Step-by-Step Walkthrough

```
Input: -12345

Step 1: Check if zero
        -12345 ≠ 0, continue
        Mark as negative: is_negative = true
        Work with absolute: n = 12345

Step 2: Extract digits right-to-left
        12345 % 10 = 5 → collect [5], n = 12345/10 = 1234
        1234 % 10 = 4  → collect [5,4], n = 1234/10 = 123
        123 % 10 = 3   → collect [5,4,3], n = 123/10 = 12
        12 % 10 = 2    → collect [5,4,3,2], n = 12/10 = 1
        1 % 10 = 1     → collect [5,4,3,2,1], n = 1/10 = 0
        n = 0, exit loop

Step 3: Reverse
        [5,4,3,2,1] → [1,2,3,4,5]

Step 4: Convert to characters
        1 + '0' = '1'
        2 + '0' = '2'
        3 + '0' = '3'
        4 + '0' = '4'
        5 + '0' = '5'
        Result: ['1','2','3','4','5']

Step 5: Prepend sign
        is_negative? Yes
        Insert '-' at beginning
        Result: ['-','1','2','3','4','5']

Step 6: Create string
        Return "-12345"

Output: "-12345" ✓
```

### Complexity

```
Time: O(log₁₀ n)
  - Proportional to number of digits
  - 12345 has 5 digits, 5 iterations
  - 1000000 has 7 digits, 7 iterations

Space: O(log₁₀ n)
  - Array to collect digits
  - Then converted to string

Example:
n = 1,000,000,000
Digits = log₁₀(1 billion) ≈ 9 digits
Time ≈ 9 iterations
```

---

## TOPIC 14: BASE CONVERSIONS (Any-to-Any)

### General Algorithm

```
Convert decimal number 'n' to base 'b':

Method: Repeatedly divide, track remainders

While n > 0:
    digit = n mod b
    append digit to result
    n = n / b
Reverse result
```

### Decimal to Binary

```
Convert 42 to binary:

42 ÷ 2 = 21 R 0
21 ÷ 2 = 10 R 1
10 ÷ 2 = 5  R 0
5 ÷ 2 = 2   R 1
2 ÷ 2 = 1   R 0
1 ÷ 2 = 0   R 1

Remainders: [0, 1, 0, 1, 0, 1]
Reversed:   [1, 0, 1, 0, 1, 0]
Result:     101010₂

Verify: 32 + 8 + 2 = 42 ✓
```

### Decimal to Hexadecimal

```
Convert 255 to hexadecimal:

255 ÷ 16 = 15 R 15 (15 = F in hex)
15 ÷ 16 = 0  R 15 (15 = F in hex)

Remainders: [15, 15]
Map to hex: [F, F]
Reversed:   [F, F]
Result:     FF₁₆

Verify: 15×16 + 15 = 255 ✓
```

### Binary to Hexadecimal (Direct)

```
Binary to hex is direct (4 bits = 1 hex digit):

Binary:  11110101
Group:   1111 0101
             F    5
Result:  F5₁₆

Why this works:
1111₂ = 8 + 4 + 2 + 1 = 15 = F₁₆
0101₂ = 0 + 4 + 0 + 1 = 5 = 5₁₆

Example memory address in binary:
11111111111111111111111100000000₂
(32 bits)
Hex equivalent:
FFFFFC00₁₆
Much more readable!
```

### Hexadecimal to Decimal

```
Convert FF₁₆ to decimal:

F × 16¹ + F × 16⁰
= 15 × 16 + 15 × 1
= 240 + 15
= 255₁₀
```

---

# PART 4: PERFORMANCE ANALYSIS (20 minutes)

## TOPIC 15: BEYOND BIG-O: PERFORMANCE REALITY

### Theoretical vs Real Performance

```
Operation              | Theory  | Real Time (ns) | Factor
--------------------  |---------|----------------|--------
String indexing       | O(1)    | 2-5            | constant
String concat         | O(n)    | 10-100/char    | linear
atoi parsing          | O(n)    | 20-100/digit   | linear
String comparison     | O(n)    | early stop     | varies
Integer arithmetic    | O(1)    | 1-3            | constant
```

### Case Study 1: String Concatenation Cost

```
Building CSV line: name + "," + age + "," + email

With String concatenation:
s = "" + "John" → allocate, copy 4 → cost 4
s = "John" + "," → allocate, copy 5 → cost 5
s = "John," + "30" → allocate, copy 7 → cost 7
s = "John,30" + "," → allocate, copy 8 → cost 8
s = "John,30," + "john@example.com" → allocate, copy 25 → cost 25
Total: 49 operations for 25-char result (2x waste)

With StringBuilder:
Append each part to buffer
Total: ~25 operations for same result
Speedup: 2x

For 1000 lines:
String concat: 49 × 1000 = 49,000 ops → ~5 ms
StringBuilder: 25 × 1000 = 25,000 ops → ~2.5 ms
```

---

## TOPIC 16: MEMORY & GC IMPLICATIONS

### Garbage Collection Pressure

```
String concat in loop:
for (int i = 0; i < 10000; i++) {
    s = s + i;  // Creates 10,000 temporary strings
}

Memory timeline:
Iteration 0: alloc "0" (1 char)
Iteration 1: alloc "0" (1), alloc "01" (2), discard "0"
Iteration 2: alloc "01" (2), alloc "012" (3), discard "01"
...
Iteration 9999: alloc "0123...9999" (big), alloc concat, discard previous

Total allocations: ~10,000
GC pressure: Very high
GC pause: Can reach 100+ milliseconds

With StringBuilder:
Allocations: ~13 (doubling: 16→32→64→...→32768)
GC pressure: Minimal
GC pause: None or < 1 millisecond
```

---

## TOPIC 17: REAL-WORLD SYSTEMS

### System 1: Text Editor (VSCode)

```
Challenge: Edit 100MB files with millions of insertions

Naive approach (single string):
├─ Insert at position 50000
├─ Allocate 100MB+new_chars
├─ Copy everything after position
├─ Insert new content
├─ Cost: O(n) for EVERY insertion
└─ Unusable (100+ seconds for each operation)

Solution: Rope data structure (tree of strings)

       ┌─────────────────┐
       │Root (100MB)     │
       └────────┬────────┘
                │
        ┌───────┴────────┐
        │                │
    ┌───┴──┐          ┌──┴───┐
    │50MB  │          │50MB   │
    │......│          │......
    └──────┘          └───────┘
        │
        ├─────┬─────┬─────┐
        │     │     │     │
      1MB   1MB   1MB   1MB

Operations:
├─ Insert: O(log n) traverse to position
├─ Delete: O(log n) split and delete segment
└─ No full copy needed

Performance:
10,000 insertions: ~100 ms (interactive!)
```

### System 2: Database (PostgreSQL)

```
Challenge: String comparisons constant in indexes

Problem:
Comparing two 1KB strings character-by-character is slow
10,000 comparisons × 1000 characters = 10M operations

Solution: Hash-based shortcuts

├─ Store hash(string) in index
├─ Compare hashes first (O(1))
│   → If hashes differ, strings differ (fast rejection)
│   → If hashes match, do full comparison
├─ Probability of false positive: ~1 in 2³²
└─ Fast path: most comparisons done in 1-2 cycles

Performance gain:
✓ Different strings rejected immediately
✓ Same strings found with one full comparison
✓ Overall query time: 2-10x faster
```

### System 3: Web Server (Node.js)

```
Challenge: Constant encoding/decoding of text

Network I/O constantly converts:
Bytes ↔ String ↔ JSON ↔ String ↔ Bytes

Problem with naive approach:
Each conversion allocates new strings
Heavy GC pressure on high-traffic server

Solution: Buffer + Explicit encoding

├─ Store raw bytes as Buffer
├─ Only decode when needed
├─ Share buffers where possible
├─ Explicit encoding (UTF-8) prevents confusion
└─ Zero-copy operations in some cases

Example:
send_to_client(response) {
    // response is string
    // Convert to UTF-8 bytes once
    Buffer buf = Encoding.UTF8.GetBytes(response);
    // Send buffer
    stream.Write(buf);
}

Performance:
10,000 requests: 50ms vs 500ms (with naive encoding)
Throughput: 200k req/sec vs 20k req/sec
```

---

## TOPIC 18: FAILURE MODES & ROBUSTNESS

### Failure 1: Unbounded String Input

```
Vulnerability:
string userInput = Console.ReadLine();  // No limit!
string result = userInput + userInput + userInput;

Attack:
User enters 500MB of data
System tries to allocate 1.5GB
Out of memory exception
Server crashes

Mitigation:
const int MAX_INPUT = 10000;
if (userInput.Length > MAX_INPUT)
    throw new ArgumentException("Input too large");
```

### Failure 2: Integer Overflow in Array Indexing

```
Vulnerability:
byte[] buffer = new byte[capacity];
int index = int.MaxValue - 1;
index++;  // Overflows to -2147483648!
buffer[index] = 0;  // Writes to wrong location!

Mitigation:
if (index >= buffer.Length || index < 0)
    throw new IndexOutOfRangeException();
buffer[index] = 0;  // Safe
```

### Failure 3: Character Encoding Mismatch

```
Vulnerability:
byte[] data = File.ReadAllBytes("data.txt");  // UTF-8 file
string text = Encoding.Latin1.GetString(data);  // Wrong!

Result:
Accented characters turn into garbage
"café" becomes "cafÃ©" (mojibake)
Data loss or corruption

Mitigation:
// Always specify encoding explicitly!
string text = File.ReadAllText("data.txt", Encoding.UTF8);
```

### Failure 4: Loop Concatenation DoS

```
Vulnerability:
for (int i = 0; i < request.Count; i++)
{
    result = result + data[i];  // O(n²) hidden!
}

Attack:
10,000 items: ~50 seconds (looks like freeze)
User refreshes repeatedly
Server becomes unresponsive

Mitigation:
StringBuilder sb = new StringBuilder();
for (int i = 0; i < request.Count; i++)
{
    sb.Append(data[i]);  // O(1) amortized
}
string result = sb.ToString();
// 10,000 items: ~1 millisecond
```

---

# 📊 SUMMARY TABLES

## String Operations Complexity

| Operation | Complexity | Real Time | Notes |
|-----------|-----------|-----------|-------|
| Access s[i] | O(1) | 2-5 ns | Cache-friendly |
| Concatenate | O(n) | 10-100 ns/char | Allocates new |
| Compare | O(n) | early stop | Hash helps |
| IndexOf | O(n×m) | fast | Optimized string search |
| Substring | O(n) | varies | Copy vs view |
| StringBuilder.Append | O(1) amortized | 1-2 ns | No copy |

## Number Representation

| Type | Range | Bits | Special Values |
|------|-------|------|-----------------|
| Unsigned 8-bit | 0 to 255 | 8 | - |
| Signed 8-bit | -128 to 127 | 8 | -128 (no positive) |
| Unsigned 32-bit | 0 to 4B | 32 | - |
| Signed 32-bit | -2B to 2B | 32 | Overflow wrapping |
| Float 32-bit | ±1.4e-45 to ±3.4e38 | 32 | NaN, ±∞ |
| Double 64-bit | ±5e-324 to ±1.7e308 | 64 | Higher precision |

## Conversion Algorithms

| Conversion | Time | Space | Method |
|-----------|------|-------|--------|
| atoi | O(n) | O(1) | Digit extraction |
| itoa | O(log n) | O(log n) | Modulo division |
| Base conversion | O(log n) | O(log n) | Repeated division |
| Binary↔Hex | O(1) | O(1) | Direct bit mapping |

---

**END OF COMPREHENSIVE LEARNING CONTENT**

Next: Practice problems, code examples, and interactive exercises coming next session.

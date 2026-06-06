# Week 14 Extended Python Complete v13

Purpose: build Python fluency for matrices, bitwise operations, and number-theory style problem solving.

## Focus tags
- Must: matrix traversal, bit operations, subset masks, gcd/mod arithmetic basics
- Should: prime sieves, modular exponentiation, matrix transformations
- Optional: number-theory deeper tools and contest-style optimizations

## Pattern 1: matrix traversal
```python
def spiral_order(matrix):
    if not matrix or not matrix[0]:
        return []
    top, bottom = 0, len(matrix) - 1
    left, right = 0, len(matrix[0]) - 1
    out = []
    while top <= bottom and left <= right:
        for c in range(left, right + 1):
            out.append(matrix[top][c])
        top += 1
        for r in range(top, bottom + 1):
            out.append(matrix[r][right])
        right -= 1
        if top <= bottom:
            for c in range(right, left - 1, -1):
                out.append(matrix[bottom][c])
            bottom -= 1
        if left <= right:
            for r in range(bottom, top - 1, -1):
                out.append(matrix[r][left])
            left += 1
    return out
```

## Pattern 2: bit tricks
```python
def is_power_of_two(n):
    return n > 0 and (n & (n - 1)) == 0
```

## Pattern 3: subset iteration
```python
def all_submasks(mask):
    sub = mask
    out = []
    while True:
        out.append(sub)
        if sub == 0:
            break
        sub = (sub - 1) & mask
    return out
```

## Pattern 4: gcd / lcm
```python
from math import gcd

def lcm(a, b):
    return a // gcd(a, b) * b
```

## Pattern 5: fast modular exponentiation
```python
def mod_pow(base, exp, mod):
    result = 1
    base %= mod
    while exp > 0:
        if exp & 1:
            result = (result * base) % mod
        base = (base * base) % mod
        exp >>= 1
    return result
```

## Practice ladder
- Must: spiral matrix, rotate matrix, single number, counting bits, gcd/lcm basics
- Should: sieve, subset masks, modular power, matrix search variants
- Optional: CRT/totient follow-ups and contest-style number theory

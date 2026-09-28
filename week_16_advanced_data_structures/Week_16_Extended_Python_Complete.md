# Week 16 Extended Python Complete v13

> 🧭 **Navigation:** [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md)
> 
> 💡 **Instructor Note:** *This Python (3.11+) guide provides reference implementations, zero-allocation patterns, and test verification drills. Use it alongside daily study as a practical code blueprint.*

---

Purpose: build Python intuition for advanced data structures beyond standard interview-core choices.

## Focus tags
- Must: conceptual understanding of skip lists, treaps, persistence, dynamic-tree motivation
- Should: implementation sketches for randomized balancing
- Optional: cache-oblivious and specialized structures

## Pattern 1: treap node idea
```python
import random

class TreapNode:
    def __init__(self, key):
        self.key = key
        self.pri = random.random()
        self.left = None
        self.right = None
```

## Pattern 2: rotate right
```python
def rotate_right(root):
    new_root = root.left
    root.left = new_root.right
    new_root.right = root
    return new_root
```

## Pattern 3: persistent path-copying intuition
- copy only the nodes on the update path
- reuse untouched subtrees
- query old roots as historical versions

## Practice ladder
- Must: explain skip list levels, treap heap-on-priority invariant, persistence by path copying
- Should: hand-trace randomized insertions and rotations
- Optional: dynamic tree/query structures and cache-oblivious motivation

---

> 🧭 **Navigation:** [🏠 Week Overview](README.md) • [📘 Curriculum Syllabus](../COMPLETE_SYLLABUS.md)

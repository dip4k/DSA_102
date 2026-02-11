# Stack Visual Playbook — Core + Interview Variations (Level 1→6)

Last updated: 2026-02-10

**Style:** ASCII visuals + invariants + failure modes.

---

## Visual legend

```text
Top is on the right in stack drawings:
S: [bottom ... top]
```

---

## L1 Visuals — Push / Pop / Peek

```text
S: []

push(10)  → S: [10]
push(20)  → S: [10, 20]
peek()=20 → S unchanged
pop()=20  → S: [10]
```

Failure mode:

```text
WRONG mental model: remove from bottom
That’s a queue, not a stack.
```

---

## L2 Visuals — Call stack intuition

```text
factorial(3)

frame f(3)
 calls f(2)
   calls f(1)
     returns 1
   returns 2
 returns 6

Call stack grows and shrinks like a stack:
S: [f(3), f(2), f(1)]  then pops back.
```

---

## L2 Visuals — Parentheses matching

```text
Input:  ( [ ] )

Read '(' → push '('   S:['(']
Read '[' → push '['   S:['(', '[']
Read ']' → pop '['    S:['(']
Read ')' → pop '('    S:[]
Valid if stack ends empty.
```

Failure mode:

```text
WRONG: only count '(' and ')'
Counts can match but nesting can be wrong: '([)]'
CORRECT: enforce matching with stack top.
```

---

## L3 Visuals — Monotonic stack: Next Greater Element

Array: [2, 1, 2, 4, 3]

Idea: keep a decreasing stack of indices; current value resolves smaller ones.

```text
i=0 val=2  S:[0]
i=1 val=1  S:[0,1]      (2>1 ok)
i=2 val=2  pop 1 (1<2)  answer[1]=2; S:[0]; push 2 → S:[0,2]
i=3 val=4  pop 2 (2<4)  answer[2]=4; pop 0 (2<4) answer[0]=4; push 3 → S:[3]
i=4 val=3  push 4 → S:[3,4]
end: indices left have no greater → answer[3]=-1, answer[4]=-1
```

Failure modes:

```text
WRONG: store values not indices
You lose the ability to compute distances/spans.

WRONG: use <= instead of < (or vice versa) without thinking
Duplicates will break hidden tests.
```

---

## L4 Visuals — Min Stack (two-stack picture)

```text
Data stack: S : [3, 5, 2, 4]
Min  stack: M : [3, 3, 2, 2]

Invariant: M.top == min(S)
pop(): pop both; push(x): push to S, push min(x, M.top) to M
```

---

## L4 Visuals — Queue using stacks

```text
in  stack: [1, 2, 3]   (top=3)
out stack: []

pop/peek needs out:
Pour all from in to out (reverses order)

in  : []
out : [3, 2, 1]  (top=1 is queue front)

pop() returns 1; out becomes [3,2]
```

Key invariant:

```text
If out is non-empty: out.top is always the queue front.
Only pour when out is empty.
```

---

## L6 Visuals — Histogram boundaries

Largest rectangle in histogram uses nearest smaller boundaries.

```text
Heights: [2, 1, 5, 6, 2, 3]

Maintain increasing stack of indices.
When you see a smaller height, you pop bars whose “right boundary” you just found.

Each popped bar i gets:
left boundary  = new stack top after pop
right boundary = current index j
width = right - left - 1
area = height[i] * width
```

---

## References

- LIFO definition and stack basics: https://www.geeksforgeeks.org/dsa/introduction-to-stack-data-structure-and-algorithm-tutorials/
- Stack operations list (push/pop/top/isEmpty/size): https://www.geeksforgeeks.org/dsa/basic-operations-in-stack-data-structure-with-implementations/
- Monotonic stack for next greater element: https://www.geeksforgeeks.org/dsa/next-greater-element/
- Implement queue using stacks (problem framing): https://leetcode.ca/all/232.html

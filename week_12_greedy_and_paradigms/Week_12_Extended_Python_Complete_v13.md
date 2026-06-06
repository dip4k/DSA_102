# Week 12 Extended Python Complete v13

Purpose: build Python fluency for greedy algorithm implementation patterns and proof-aware coding habits.

## Focus tags
- Must: sort-and-scan greedy, activity selection, interval allocation, fractional knapsack
- Should: Huffman coding, priority-queue greedy, scheduling variants
- Optional: approximation-style greedy extensions

## Pattern 1: activity selection
```python
def activity_selection(intervals):
    intervals = sorted(intervals, key=lambda x: x[1])
    chosen = []
    last_end = float('-inf')
    for start, end in intervals:
        if start >= last_end:
            chosen.append((start, end))
            last_end = end
    return chosen
```

## Pattern 2: meeting rooms / sweep line
```python
def min_rooms(intervals):
    events = []
    for s, e in intervals:
        events.append((s, 1))
        events.append((e, -1))
    events.sort(key=lambda x: (x[0], x[1]))
    current = best = 0
    for _, delta in events:
        current += delta
        best = max(best, current)
    return best
```

## Pattern 3: fractional knapsack
```python
def fractional_knapsack(items, capacity):
    items = sorted(items, key=lambda x: x[0] / x[1], reverse=True)
    value = 0.0
    for profit, weight in items:
        if capacity == 0:
            break
        take = min(weight, capacity)
        value += profit * (take / weight)
        capacity -= take
    return value
```

## Pattern 4: Huffman merge skeleton
```python
import heapq

def huffman_cost(freqs):
    heap = freqs[:]
    heapq.heapify(heap)
    cost = 0
    while len(heap) > 1:
        a = heapq.heappop(heap)
        b = heapq.heappop(heap)
        merged = a + b
        cost += merged
        heapq.heappush(heap, merged)
    return cost
```

## Practice ladder
- Must: activity selection, interval removal, meeting rooms, fractional knapsack
- Should: Huffman coding, job sequencing with deadlines, MST greedy comparison
- Optional: set-cover style approximation intuition

## Proof prompts
- State the greedy rule explicitly.
- Name the proof style: exchange argument, stays-ahead, or induction.
- Give one counterexample when a tempting greedy rule fails.

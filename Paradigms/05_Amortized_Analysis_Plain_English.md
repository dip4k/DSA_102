# 🏦 Amortized Analysis: In Plain English

> *"Amortized analysis is like buying a 30-day unlimited subway pass. Day 1 feels expensive (\$60), but for the next 29 days you ride for \$0. On average, each ride costs you just \$2."*

---

## 💥 The War Story: The High-Frequency Trading P99 Latency Nightmare

At a cryptocurrency derivatives exchange, the order-matching engine was engineered in C# to achieve an SLA of **sub-50 microsecond execution**.

During the initial staging benchmarks, the engineering team celebrated:
- Average latency (P50): **8 microseconds**.
- 90th percentile latency (P90): **14 microseconds**.

Then the exchange went live. High-volume institutional algorithmic trading desks connected via direct fiber connections. 

Within two hours, the trading desks began bombarding customer support with furious complaints:
> *"Every few minutes, our market-maker cancel orders freeze for over 120 milliseconds! Our capital is exposed to adverse price movement during your freezes!"*

The engineers were bewildered. Average latency was great, but **P99 latency was spiking to 142,000 microseconds (142 milliseconds)**!

### The Profiler Culprit: Dynamic Array Resizing
The team attached a low-overhead memory profiler to the matching engine. The culprit was a single innocent-looking line of code inside the order book:
```csharp
activeOrderBook.Add(incomingOrder); // List<Order>
```

The list had been declared with default capacity (`new List<Order>()`).
1. As orders poured in, the list grew: `4 -> 8 -> 16 -> 32 -> 64 ... -> 65,536 -> 131,072`.
2. When the list hit 65,536 elements, the next `.Add()` hit capacity.
3. The .NET CLR was forced to:
   - Allocate a brand new contiguous block of memory for 131,072 orders.
   - Copy all 65,536 order structs from the old array into the new array.
   - Trigger garbage collection pressure to reclaim the discarded 65,536-element block.
4. During that copy operation, the execution thread froze for **142 milliseconds**.

### The Fix: Pre-Allocation & Amortization Awareness
The fix took 30 seconds to code:
```csharp
// Pre-allocate based on peak daily order book depth
activeOrderBook = new List<Order>(capacity: 250_000);
```
P99 latency immediately collapsed from **142,000 microseconds down to 18 microseconds**.

**The Golden Takeaway:** On paper, `List<T>.Add()` has an **amortized cost of `O(1)`**. But in the real world, amortized means *"frequent cheap operations absorb rare expensive operations"*. If that rare expensive operation happens on a critical financial or gaming thread, your users will feel that latency spike! Understanding how amortization works is essential for both algorithmic interviews and high-performance engineering.

---

## 🚇 1. The Everyday Hook: The Subway Pass

Suppose you commute to work by train every weekday:
- **Option A:** Pay \$2.50 cash every single morning.
- **Option B:** Buy an unlimited Monthly Pass on the 1st of the month for \$60.

On Day 1, with Option B, you swipe your card and spend **\$60**. If someone asked you: *"How much did today's commute cost?"*, you might say, *"Yikes, \$60!"*

But on Day 2, you pay \$0. On Day 3, \$0. On Day 20, \$0.  
Across the whole month (30 days), your **average cost per day** is:
```
Total Spent / Total Days = $60 / 30 = $2.00 per day!
```

In software engineering, this is called **Amortized Analysis**:
> **Amortized Cost** means: an occasional, expensive operation happens so rarely that its cost is completely absorbed by the massive number of cheap operations that came before or after it.

---

## 🎯 2. The 4 Essential Interview Variations of Amortized Analysis

In technical interviews, amortized analysis appears in **4 foundational patterns**:

```
                       ┌───────────────────────────────────────────┐
                       │     AMORTIZED INTERVIEW VARIATIONS        │
                       └───────────────────────────────────────────┘
                                             │
        ┌───────────────────┬────────────────┴───────────────────┬───────────────────┐
        ▼                   ▼                                   ▼                   ▼
 [ 1. Dynamic Array   [ 2. Two-Stack Queue:   [ 3. Monotonic Stack:   [ 4. Union-Find:
      Doubling ]           Lazy Transfer ]         While Loop O(N) ]       Path Compression ]
  • List<T> / vector   • Queue using 2 Stacks  • Next Greater Element  • Disjoint Set
  • Why 2x vs +K       • InStack & OutStack    • Daily Temperatures    • Inverse Ackermann
```

Let's dissect each variation.

---

## 📦 Variation 1: Dynamic Array Doubling (Why 2x vs. +K?)

A classic interview question asks:  
*"Why do dynamic arrays double their capacity (`* 2`), instead of expanding by a fixed size like `+ 100` slots?"*

### The Comparison:
- **Strategy A: Fixed Increment (+100 slots)**
  - If you insert 10,000 items, you must resize **100 separate times**.
  - Total copies = `100 + 200 + 300 + ... + 10,000 = O(N^2)`!
  - Average cost per insert = `O(N^2) / N = O(N)`!  
  - ❌ **Disastrous performance!**
- **Strategy B: Exponential Doubling (* 2)**
  - If you insert 16 items: resizes happen at `1, 2, 4, 8`.
  - Total copies = `1 + 2 + 4 + 8 = 15 copies < 2N`.
  - Average cost per insert = `< 2N / N = O(1)`!  
  - ✅ **Guaranteed O(1) amortized!**

---

## 🥞 Variation 2: Queue Using Two Stacks (Lazy Transfer)

### The FAANG Problem
Implement a First-In-First-Out (FIFO) queue using only two Last-In-First-Out (LIFO) stacks (`InStack` and `OutStack`).

### The Intuitive Hook: The Flip-Flop
- `Enqueue(x)`: Always push onto `InStack` (`O(1)`).
- `Dequeue()`:
  - If `OutStack` already has elements, simply pop from `OutStack` (`O(1)`).
  - If `OutStack` is empty: dump all elements from `InStack` into `OutStack`. This reverses their order so the oldest element is now on top!
- **Is `Dequeue()` slow?**
  - Worst single call: `O(N)` (copying all elements).
  - **Amortized across all calls: `O(1)`!**
  - Every single element is pushed to `InStack` once, moved to `OutStack` once, and popped from `OutStack` once. Exactly **3 operations per element across its entire lifetime**!

```
Enqueue 1, 2, 3:      InStack: [ 1, 2, 3 | Top      OutStack: [ Empty ]
Dequeue():            Dump InStack to OutStack:
                      InStack: [ Empty ]             OutStack: [ 3, 2, 1 | Top
                      Pop 1!                         OutStack: [ 3, 2 | Top
Next Dequeue():       Instant Pop 2 from OutStack! (O(1))
```

### C# (.NET 8/9) Code
```csharp
namespace Paradigms.Amortized;

public class MyQueue<T>
{
    private readonly Stack<T> _inStack = new();
    private readonly Stack<T> _outStack = new();

    public void Enqueue(T item)
    {
        _inStack.Push(item);
    }

    public T Dequeue()
    {
        ShiftStacksIfNeeded();
        if (_outStack.Count == 0) throw new InvalidOperationException("Queue is empty.");
        return _outStack.Pop();
    }

    public T Peek()
    {
        ShiftStacksIfNeeded();
        if (_outStack.Count == 0) throw new InvalidOperationException("Queue is empty.");
        return _outStack.Peek();
    }

    private void ShiftStacksIfNeeded()
    {
        if (_outStack.Count == 0)
        {
            while (_inStack.Count > 0)
            {
                _outStack.Push(_inStack.Pop());
            }
        }
    }
}
```

### Python (3.11+) Code
```python
from typing import Generic, TypeVar

T = TypeVar('T')

class MyQueue(Generic[T]):
    def __init__(self):
        self.in_stack = []
        self.out_stack = []

    def enqueue(self, item: T) -> None:
        self.in_stack.append(item)

    def dequeue(self) -> T:
        self._shift_if_needed()
        if not self.out_stack:
            raise IndexError("Queue is empty")
        return self.out_stack.pop()

    def peek(self) -> T:
        self._shift_if_needed()
        if not self.out_stack:
            raise IndexError("Queue is empty")
        return self.out_stack[-1]

    def _shift_if_needed(self) -> None:
        if not self.out_stack:
            while self.in_stack:
                self.out_stack.append(self.in_stack.pop())
```

---

## 📈 Variation 3: Monotonic Stack (Why a Nested Loop is Still O(N)!)

### The FAANG Problem: Next Greater Element
Given an array `nums`, find the next greater element for every index.

### The Code Pattern:
```python
for i in range(len(nums)):
    while stack and nums[stack[-1]] < nums[i]:
        stack.pop()
    stack.append(i)
```

### The Interview Trap:
Beginners look at the `for` loop containing a `while` loop and say:  
*"There is a loop inside a loop, so the time complexity is `O(N^2)`!"*  
❌ **WRONG! It is `O(N)`!**

### The Amortized Proof:
Ask yourself: **How many times can any single element be pushed and popped?**
- Each element is pushed onto the stack **exactly once**.
- Each element can be popped from the stack **at most once**.
- Across the entire execution of the program, there are at most `N` pushes and `N` pops.
- Total operations `= N + N = 2N = O(N)` total time!
- **Amortized cost per iteration: `O(1)`!**

---

## 🌲 Variation 4: Disjoint Set (Union-Find with Path Compression)

When searching for connected components in a graph using Union-Find:
- A naive tree structure can degrade into a long linked list of depth `N`, making `Find()` take `O(N)` time.
- With **Path Compression**, every node visited during a `Find()` operation points its parent pointer directly to the root:

```
Before Find(4):                 After Find(4) (Path Compressed):
      [ 1 ]                           [ 1 ]
        │                            ┌──┼──┐
      [ 2 ]                          ▼  ▼  ▼
        │                          [2] [3] [4]
      [ 3 ]
        │
      [ 4 ]
```

The first `Find(4)` takes `O(N)` time to traverse the chain. But **every future call for 2, 3, or 4 now takes `O(1)` time**!  
The amortized cost per operation across a sequence of `M` operations is `O(α(N))` (where `α` is the Inverse Ackermann function, which is `< 5` for any universe-sized number). For all practical engineering purposes, it is **amortized `O(1)`**!

---

## 📊 Summary Comparison: Amortized Variations

| Data Structure / Pattern | Worst-Case Single Operation | Amortized Cost | Why the Work is Absorbed |
| :--- | :--- | :--- | :--- |
| **Dynamic Array Append** | `O(N)` (when resizing) | `O(1)` | Doubling capacity spaces out resizes geometrically |
| **Two-Stack Queue Dequeue** | `O(N)` (when dumping) | `O(1)` | Each item pushed and popped at most twice lifetime |
| **Monotonic Stack Loop** | `O(N)` (popping all items) | `O(1)` | Each item pushed once, popped at most once |
| **Union-Find with Path Comp** | `O(N)` (first deep path) | `O(α(N)) ≈ O(1)` | Traversed paths are flattened permanently for future calls |

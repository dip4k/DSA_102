# Stack Mastery — Flow‑Wise Roadmap (Level 1→6) | Lenient

**Goal:** Build interview-ready stack intuition. Move beyond basic mechanics to instantly recognizing *when* and *how* to use a stack.

**Contract: LENIENT**
- `pop()` or `peek()` on an empty stack returns `None` (or equivalent) instead of crashing.
- Empty input $\rightarrow$ empty output.

---

## 🚀 Curriculum 2.0 Summary Table

| Level | Mental Model | Pointer / Stack State | Drill Problems |
| :--- | :--- | :--- | :--- |
| **L1: Physical Layer** | LIFO Container | Top is the most recently pushed element. | `[LeetCode 1441: Build an Array With Stack Operations]` (Easy) |
| **L2: Structural Layer** | Matching Pairs / Control Flow | Stack holds unresolved "opening" elements. | `[LeetCode 20: Valid Parentheses]` (Easy) |
| **L3: Stateful Layer** | Monotonic Sequences | Stack strictly increasing/decreasing. Holds indices. | `[LeetCode 739: Daily Temperatures]` (Medium) |
| **L4: Derived Layer** | State Snapshots / Composition | Secondary stack/tuple maintains snapshot properties. | `[LeetCode 155: Min Stack]` (Medium) |
| **L5: Abstract Layer** | Consume & Reduce | Stack holds parsed tokens. Reduce when rules are met. | `[LeetCode 394: Decode String]` (Medium) |
| **L6: Pro Layer** | Range Contributions | Stack bounds regions. Sentinels trigger final flushes. | `[LeetCode 84: Largest Rectangle in Histogram]` (Hard) |

---

## Level 1: Physical Layer (Operations)

**Mental Model:** Last-In-First-Out (LIFO) memory. 
- **What does the index mean?** The relative time of entry.
- **What region is processed?** Real-time queries or array iterations.
- **What is the invariant?** The last item pushed is always the first to be popped.

### Visual State Transitions: Pushing and Popping
| Step | Action | Stack State | Action / Invariant |
| :--- | :--- | :--- | :--- |
| 1 | `Push(10)` | `[10]` | 10 is at the top |
| 2 | `Push(20)` | `[10, 20]` | 20 is at the top |
| 3 | `Push(30)` | `[10, 20, 30]` | 30 is at the top |
| 4 | `Pop()` | `[10, 20]` | 30 removed, 20 is at the top again |

### 🛠️ Code Snippet: Array-Backed Stack

**Problem:** Implement a basic stack data structure with push, pop, and peek operations using an underlying array or list. The stack should follow the Last-In-First-Out (LIFO) principle and handle empty states gracefully (returning `None` or default values).

```python
# Python
class Stack:
    def __init__(self):
        # 1. Initialize an empty list to act as the underlying dynamic array
        self.items = []
        
    def push(self, val):
        # 2. Add the new element to the end (top) of the stack
        self.items.append(val)
        
    def pop(self):
        # 3. Check for empty stack to prevent crashes, returning None if empty
        # 4. Remove and return the last element (LIFO order)
        return self.items.pop() if self.items else None
        
    def peek(self):
        # 5. Return the last element without removing it, or None if empty
        return self.items[-1] if self.items else None
```

```csharp
// C#
public class Stack<T> {
    // 1. Initialize a dynamic list to store stack elements
    private List<T> items = new List<T>();
    
    // 2. Append the new value to the end of the list (acting as the top)
    public void Push(T val) => items.Add(val);
    
    public T Pop() {
        // 3. Prevent crashing by checking if the stack is empty first
        if (items.Count == 0) return default(T);
        
        // 4. Capture the top element (last item in the list)
        T val = items[^1];
        
        // 5. Remove it from the list to finalize the pop operation
        items.RemoveAt(items.Count - 1);
        return val;
    }
    
    // 6. View the top element if it exists, otherwise return default value
    public T Peek() => items.Count > 0 ? items[^1] : default(T);
}
```

### ⚠️ Gotchas & Pitfalls
- **Empty input crashes:** Calling `pop()` or `peek()` without verifying if the stack has elements, leading to `IndexError`.
- **Misunderstanding LIFO:** Reversing the logical order of elements. Elements come out backward relative to their insertion.
- **Hidden O(N) operations:** Using an underlying data structure (like inserting at index 0 of a list) that makes push/pop operations take $O(N)$ time instead of $O(1)$.

**🎯 Drill Problems:**
- **Easy:** `[LeetCode 1441: Build an Array With Stack Operations]`

---

## Level 2: Structural Layer (Matching & Control Flow)

**Mental Model:** Simulation of nested structures and deferred resolution.
- **What does the stack hold?** "Opening" brackets or incomplete states.
- **What region is processed?** A string or stream from left to right.
- **What is the invariant?** Every closing character must perfectly match the most recent unresolved opening character (the top of the stack).

### Visual State Transitions: `s = "{[()]}"`
| Step | Char | Stack State | Action / Invariant |
| :--- | :--- | :--- | :--- |
| 1 | `{` | `['{']` | Push unresolved opening bracket. |
| 2 | `[` | `['{', '[']` | Push unresolved opening bracket. |
| 3 | `(` | `['{', '[', '(']` | Push unresolved opening bracket. |
| 4 | `)` | `['{', '[']` | Match `)` with `(`. Pop. |
| 5 | `]` | `['{']` | Match `]` with `[`. Pop. |
| 6 | `}` | `[]` | Match `}` with `{`. Pop. Valid if empty! |

### ⚠️ Gotchas & Pitfalls
- **Dangling elements:** Forgetting to check if the stack is completely empty at the end of the string (e.g., input is just `"((("`).
- **Popping an empty stack:** Encountering a closing bracket when there is no opening bracket on the stack (e.g., input is `"]"`).
- **Mismatched types:** Popping the wrong opening bracket (e.g., matching `)` with `[`) because of missing type checks.

**🎯 Drill Problems:**
- **Easy:** `[LeetCode 20: Valid Parentheses]`, `[LeetCode 1047: Remove All Adjacent Duplicates In String]`

---

## Level 3: Stateful Layer (Monotonic Stacks)

**Mental Model:** Stateful solver for "Nearest Greater/Smaller".
- **What does the stack hold?** **Indices** of elements, NOT values. The index allows us to calculate distance and retrieve the value.
- **What region is processed?** Left to right over an array.
- **What is the invariant?** The stack remains strictly monotonic (e.g., decreasing). A new element breaking this invariant resolves (pops) previous elements, acting as their "Next Greater".

### Visual State Transitions: Next Greater Element for `[2, 1, 5, 3]`
| Step | Current | Stack (Indices) | Action / Invariant |
| :--- | :--- | :--- | :--- |
| 1 | `2` (idx 0) | `[0]` | Push unresolved index. |
| 2 | `1` (idx 1) | `[0, 1]` | `1 < 2`. Monotonic property holds. Push idx 1. |
| 3 | `5` (idx 2) | `[2]` | `5 > 1`, pop idx 1 (ans=5). `5 > 2`, pop idx 0 (ans=5). Push idx 2. |
| 4 | `3` (idx 3) | `[2, 3]` | `3 < 5`. Monotonic property holds. Push idx 3. |

### 🛠️ Code Snippet: Daily Temperatures

**Problem:** Given an array of daily temperatures, return an array where each element is the number of days you have to wait to get a warmer temperature. Use a monotonic stack to efficiently find the next greater element.

```python
# Python
def dailyTemperatures(temperatures: list[int]) -> list[int]:
    # 1. Initialize the result array with 0s (default if no warmer day is found)
    ans = [0] * len(temperatures)
    
    # 2. Initialize a stack to store the indices of unresolved days
    stack = []
    
    # 3. Iterate through each day's temperature
    for i, temp in enumerate(temperatures):
        # 4. While stack has unresolved days AND the current day is warmer
        # Invariant: stack values (temperatures) must remain strictly decreasing.
        while stack and temperatures[stack[-1]] < temp:
            # 5. Resolve the previous colder day by popping its index
            prev_idx = stack.pop()
            
            # 6. Calculate the distance in days and store it in the result array
            ans[prev_idx] = i - prev_idx
            
        # 7. Push the current day's index onto the stack to await a future warmer day
        stack.append(i)
        
    return ans
```

```csharp
// C#
public int[] DailyTemperatures(int[] temperatures) {
    // 1. Initialize the answer array with zeros
    int[] ans = new int[temperatures.Length];
    
    // 2. Initialize a stack to keep track of unresolved day indices
    Stack<int> stack = new Stack<int>(); 
    
    // 3. Process each temperature day by day
    for (int i = 0; i < temperatures.Length; i++) {
        // 4. Check if the current temperature breaks the decreasing invariant
        // Resolve previous days that were colder than today
        while (stack.Count > 0 && temperatures[stack.Peek()] < temperatures[i]) {
            // 5. Pop the index of the colder day
            int prevIdx = stack.Pop();
            
            // 6. The distance is the difference between the current index and the popped index
            ans[prevIdx] = i - prevIdx;
        }
        
        // 7. Push the current index onto the stack as an unresolved day
        stack.Push(i);
    }
    
    return ans;
}
```

### ⚠️ Gotchas & Pitfalls
- **Storing values instead of indices:** Saving actual array values to the stack makes it impossible to compute distances or safely mutate the output array based on index positions.
- **Strict vs. Non-strict bounds:** Mixing up `<` and `<=` in the while-loop condition, which causes subtle bugs when dealing with duplicate values.
- **Infinite loops:** Forgetting to pop the element from the stack inside the resolving while-loop.

**🎯 Drill Problems:**
- **Easy:** `[LeetCode 496: Next Greater Element I]`
- **Medium:** `[LeetCode 739: Daily Temperatures]`, `[LeetCode 901: Online Stock Span]`

---

## Level 4: Derived Layer (Compositions)

**Mental Model:** Storing complex state snapshots.
- **What does the stack hold?** Tuples of `(value, aggregate_state)` or a secondary stack tracks the state.
- **What region is processed?** Real-time queries over object lifecycle.
- **What is the invariant?** At any point, querying the aggregate state (like Min or Max) relies purely on the top of the stack, guaranteeing $O(1)$ time complexity.

### ⚠️ Gotchas & Pitfalls
- **Out of sync auxiliary stacks:** When using a second stack for tracking minimums, popping the main stack without checking if the secondary stack also needs a pop.
- **Memory bloat:** Creating tuples for every single operation which can double the memory footprint, rather than intelligently pushing only when the aggregate state changes.
- **Aggregate query on empty stack:** Calling `getMin()` or `getMax()` when there's no state inside the composition, leading to unexpected crashes or undefined behavior.

**🎯 Drill Problems:**
- **Easy:** `[LeetCode 232: Implement Queue using Stacks]`
- **Medium:** `[LeetCode 155: Min Stack]`

---

## Level 5: Abstract Layer (Consume & Reduce)

**Mental Model:** Parsing machine.
- **What does the stack hold?** Processed tokens (numbers, strings, operators).
- **What region is processed?** Left to right tokens.
- **What is the invariant?** Tokens are accumulated until a "reduce" trigger (e.g., closing bracket, operator) is hit, at which point the top tokens are evaluated, popped, and the result is pushed back.

### Visual State Transitions: RPN `["2", "1", "+", "3", "*"]`
| Step | Token | Stack State | Action / Invariant |
| :--- | :--- | :--- | :--- |
| 1 | `"2"` | `[2]` | Push number. |
| 2 | `"1"` | `[2, 1]` | Push number. |
| 3 | `"+"` | `[3]` | Reduce: `1 + 2 = 3`. Pop two, push result. |
| 4 | `"3"` | `[3, 3]` | Push number. |
| 5 | `"*"` | `[9]` | Reduce: `3 * 3 = 9`. Pop two, push result. |

### ⚠️ Gotchas & Pitfalls
- **Reverse operand order:** Popping two operands for subtraction or division and operating in the wrong order (e.g. `b = pop(), a = pop()`, then doing `b - a` instead of `a - b`).
- **Multi-digit/Nested Parsing:** Reading characters one-by-one and incorrectly handling numbers with multiple digits or nested bracket logic (e.g., `3[a2[c]]`).
- **Type casting issues:** Treating string numbers as strings during an addition trigger, causing concatenation rather than mathematical addition.

**🎯 Drill Problems:**
- **Medium:** `[LeetCode 71: Simplify Path]`, `[LeetCode 150: Evaluate Reverse Polish Notation]`, `[LeetCode 394: Decode String]`

---

## Level 6: Pro Layer (Ranges & Contributions)

**Mental Model:** Using boundaries to compute spatial limits.
- **What does the stack hold?** Indices forming a strictly monotonic sequence.
- **What region is processed?** Entire array, often padded with a **Sentinel** at the end.
- **What is the invariant?** Finding the Nearest Smaller to Left (NSL) and Right (NSR) simultaneously. A popped index `i` has its right boundary at the current index `curr`, and its left boundary at the new top of the stack.

### Visual State Transitions: Histogram Areas with Sentinel
For heights `[2, 1, 5]`, append `0` sentinel `[2, 1, 5, 0]`.

| Step | Current (Height) | Stack (Indices) | Action / Invariant |
| :--- | :--- | :--- | :--- |
| 1 | `2` (idx 0) | `[0]` | Push idx 0. |
| 2 | `1` (idx 1) | `[1]` | `1 < 2`. Pop idx 0, calc area `2*1 = 2`. Push idx 1. |
| 3 | `5` (idx 2) | `[1, 2]` | Push idx 2. |
| 4 | `0` (idx 3) | `[3]` | Sentinel forces pop. Pop 2: area `5*1=5`. Pop 1: area `1*3=3`. Push idx 3. |

### ⚠️ Gotchas & Pitfalls
- **Off-by-one boundary logic:** Miscalculating the width of the range. The standard formula is `width = current_index - stack[-1] - 1`.
- **Forgetting the sentinel:** Leaving unresolved elements in the stack at the end of the iteration, resulting in missed range evaluations.
- **Empty stack boundaries:** When computing the left boundary and the stack is empty (meaning the popped element was the minimum so far), not mapping the left boundary to `-1`.

**🎯 Drill Problems:**
- **Medium:** `[LeetCode 907: Sum of Subarray Minimums]`
- **Hard:** `[LeetCode 84: Largest Rectangle in Histogram]`

---

## 🚨 Core Rules to Remember

1. **Store Indices, not Values:** When solving substring/subarray problems (Next Greater, Histogram), you need the index to compute width/distance: `width = current_index - stack.peek() - 1`.
2. **Sentinel Padding:** Add dummy elements (e.g., `-1` or `0`) to the end of your array/string to avoid writing a second cleanup loop to empty the stack.
3. **Strict Inequalities:** Pay attention to `<` vs `<=`. If you need to handle duplicates differently, your strictly monotonic condition dictates which element gets resolved.

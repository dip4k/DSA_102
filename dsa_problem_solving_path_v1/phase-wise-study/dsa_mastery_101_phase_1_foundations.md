# DSA Mastery 101 — Phase 1 Foundations & Computational Thinking

## Scope
This document curates **Phase 1** of the attached syllabus: **Weeks 1-3** of the Foundations track.

Covered syllabus areas:
- Week 1: computational fundamentals, asymptotics, space complexity, recursion, memoization, peak finding
- Week 2: arrays, dynamic arrays, linked lists, stacks, queues, deques, binary search, strings and number representation
- Week 3: elementary sorting, merge sort, quicksort, heaps, hash tables, rolling hash

Programming language used for all implementations: **Python**.

---

## How to use this file
1. Study the category overview first.
2. Solve each problem **without** looking at the solution.
3. Then read the **signal**, **visual**, **approach**, and only then the code.
4. Speak the complexity aloud before moving on.
5. Do not skip the **companion drills**; they cover syllabus topics that are concept-heavy but do not map perfectly to a single LeetCode problem.

---

## Phase 1 category map

| Category | Syllabus alignment | Core items |
|---|---|---:|
| Computational thinking | Week 1 Days 1-6 | 5 problems + 3 drills |
| Arrays and dynamic arrays | Week 2 Days 1-2 | 4 problems + 2 drills |
| Linked structures | Week 2 Day 3 | 3 problems |
| Stacks, queues, deques | Week 2 Day 4 | 4 problems |
| Binary search | Week 2 Day 5 | 4 problems |
| Strings and numbers | Week 2 Day 6 | 2 problems |
| Sorting | Week 3 Days 1-2 | 3 problems + 1 drill |
| Heaps | Week 3 Day 3 | 2 problems |
| Hashing and rolling hash | Week 3 Days 4-5 | 5 problems |

---

## Study order
- Part A: conceptual foundation and recursion
- Part B: linear data structures
- Part C: search and representation
- Part D: sorting, heaps, hashing

The order below follows that exact progression.

---

# Part A — Computational thinking

## A1. Concept bridge: RAM model, asymptotics, and space
These syllabus topics are fundamental, but they are not always represented by one direct LeetCode problem. Use the following mapping to connect theory to practice.

| Syllabus topic | Best problem anchor | Why it fits |
|---|---|---|
| RAM model and locality | `Move Zeroes` | In-place writes, sequential scan, low-overhead memory behavior |
| Big-O and logarithmic growth | `Binary Search`, `Sqrt(x)` | Clear contrast between linear and logarithmic search |
| Space complexity | `Rotate Array` companion drill | Compare extra-array vs reversal in-place approach |
| Recursion and call stack | `Pow(x, n)`, `Fibonacci Number` | Clean recursion trees and memoization contrast |
| Peak finding | `Find Peak Element` | Canonical divide-and-conquer intuition |

### Companion drill 1 — Linear search vs binary search
```python
def linear_search(nums, target):
    for i, x in enumerate(nums):
        if x == target:
            return i
    return -1


def binary_search(nums, target):
    lo, hi = 0, len(nums) - 1
    while lo <= hi:
        mid = lo + (hi - lo) // 2
        if nums[mid] == target:
            return mid
        if nums[mid] < target:
            lo = mid + 1
        else:
            hi = mid - 1
    return -1
```

Why this drill matters:
- Same input/output goal.
- Very different growth rate.
- Perfect for explaining why asymptotic analysis matters.

### Companion drill 2 — Extra space vs in-place rotation
```python
def rotate_extra(nums, k):
    n = len(nums)
    k %= n
    temp = nums[-k:] + nums[:-k]
    nums[:] = temp


def rotate_in_place(nums, k):
    n = len(nums)
    k %= n

    def rev(l, r):
        while l < r:
            nums[l], nums[r] = nums[r], nums[l]
            l += 1
            r -= 1

    rev(0, n - 1)
    rev(0, k - 1)
    rev(k, n - 1)
```

Why this drill matters:
- Same asymptotic time can still have different auxiliary space.
- Great interview discussion for trade-offs.

### Companion drill 3 — Tiny recursive tree
```python
def fib_naive(n):
    if n <= 1:
        return n
    return fib_naive(n - 1) + fib_naive(n - 2)


def fib_memo(n, memo=None):
    if memo is None:
        memo = {}
    if n in memo:
        return memo[n]
    if n <= 1:
        return n
    memo[n] = fib_memo(n - 1, memo) + fib_memo(n - 2, memo)
    return memo[n]
```

Visual:
```text
fib(5)
├─ fib(4)
│  ├─ fib(3)
│  └─ fib(2)
└─ fib(3)   <- repeated work
```

---

## A2. Fibonacci Number
**LeetCode:** Fibonacci Number

### Why this problem belongs here
It is the cleanest first example of recursion, base cases, overlapping subproblems, and memoization.

### Pattern signal
- Problem reduces to smaller versions of itself.
- Same subproblems repeat.
- Naive recursion is easy to write but inefficient.

### Visual
```text
F(6)
= F(5) + F(4)
= (F(4) + F(3)) + (F(3) + F(2))
```

### Approach
1. Identify base cases: `F(0) = 0`, `F(1) = 1`.
2. Use bottom-up DP to avoid recursion overhead.
3. Keep only the last two values because each state depends on two previous states.

### Python solution
```python
class Solution:
    def fib(self, n: int) -> int:
        if n <= 1:
            return n
        a, b = 0, 1
        for _ in range(2, n + 1):
            a, b = b, a + b
        return b
```

### Complexity
- Time: `O(n)`
- Space: `O(1)`

### Common mistakes
- Forgetting the `n = 0` case.
- Writing naive recursion in interviews without discussing repeated work.

---

## A3. Pow(x, n)
**LeetCode:** Pow(x, n)

### Why this problem belongs here
This is a recursion and divide-and-conquer classic. It also teaches how reducing the problem by half changes complexity dramatically.

### Pattern signal
- The exponent can be halved.
- Repeated multiplication is too slow.
- Negative exponent must be handled carefully.

### Visual
```text
x^13
= x * (x^6)^2
= x * ((x^3)^2)^2
```

### Approach
1. If `n` is negative, compute `1 / x^-n`.
2. Recursively compute `half = x^(n//2)`.
3. Square `half`.
4. If `n` is odd, multiply by one more `x`.

### Python solution
```python
class Solution:
    def myPow(self, x: float, n: int) -> float:
        def fast_pow(base, exp):
            if exp == 0:
                return 1.0
            half = fast_pow(base, exp // 2)
            if exp % 2 == 0:
                return half * half
            return half * half * base

        if n < 0:
            return 1 / fast_pow(x, -n)
        return fast_pow(x, n)
```

### Complexity
- Time: `O(log n)`
- Space: `O(log n)` due to recursion

### Common mistakes
- Using `-2**31` style edge cases carelessly in other languages.
- Forgetting that odd exponents need one extra multiply.

---

## A4. Climbing Stairs
**LeetCode:** Climbing Stairs

### Why this problem belongs here
It is one of the best memoization transition problems: from recursion to DP.

### Pattern signal
- To reach step `i`, you come from `i-1` or `i-2`.
- State transition is small and local.

### Visual
```text
stairs[5]
= stairs[4] + stairs[3]
```

### Approach
1. Define `dp[i]` as ways to reach step `i`.
2. `dp[1] = 1`, `dp[2] = 2`.
3. Iterate upward using the recurrence.
4. Compress space to two variables.

### Python solution
```python
class Solution:
    def climbStairs(self, n: int) -> int:
        if n <= 2:
            return n
        a, b = 1, 2
        for _ in range(3, n + 1):
            a, b = b, a + b
        return b
```

### Complexity
- Time: `O(n)`
- Space: `O(1)`

### Common mistakes
- Presenting it as a trick instead of state transition reasoning.

---

## A5. Sqrt(x)
**LeetCode:** Sqrt(x)

### Why this problem belongs here
It is excellent for asymptotic thinking and binary search over an answer range.

### Pattern signal
- Answer is an integer in a bounded numeric range.
- Need largest value whose square does not exceed `x`.

### Visual
```text
Target x = 17
Range: [0, 17]
mid = 8 -> 64 too big
mid = 3 -> 9 okay
mid = 5 -> 25 too big
mid = 4 -> 16 okay => answer 4
```

### Approach
1. Search the answer range `[0, x]`.
2. If `mid * mid <= x`, keep it as candidate and move right.
3. Otherwise move left.
4. Return the last valid candidate.

### Python solution
```python
class Solution:
    def mySqrt(self, x: int) -> int:
        lo, hi = 0, x
        ans = 0
        while lo <= hi:
            mid = lo + (hi - lo) // 2
            if mid * mid <= x:
                ans = mid
                lo = mid + 1
            else:
                hi = mid - 1
        return ans
```

### Complexity
- Time: `O(log x)`
- Space: `O(1)`

---

## A6. Find Peak Element
**LeetCode:** Find Peak Element

### Why this problem belongs here
This is the direct Week 1 peak-finding anchor from the syllabus.

### Pattern signal
- Need any valid peak, not necessarily the global maximum.
- Local slope tells which side must contain a peak.

### Visual
```text
1  2  5  3  2
      ^
If nums[mid] > nums[mid+1], a peak exists on the left side including mid.
If nums[mid] < nums[mid+1], a peak exists on the right side.
```

### Approach
1. Compare `nums[mid]` with `nums[mid + 1]`.
2. Descending slope means a peak is on the left half including `mid`.
3. Ascending slope means a peak is on the right half.
4. Continue until one index remains.

### Python solution
```python
class Solution:
    def findPeakElement(self, nums: list[int]) -> int:
        lo, hi = 0, len(nums) - 1
        while lo < hi:
            mid = lo + (hi - lo) // 2
            if nums[mid] > nums[mid + 1]:
                hi = mid
            else:
                lo = mid + 1
        return lo
```

### Complexity
- Time: `O(log n)`
- Space: `O(1)`

### Common mistakes
- Trying to find the global maximum instead of any peak.
- Overcomplicating edge handling.

---

# Part B — Arrays and dynamic arrays

## B1. Arrays: what the syllabus wants you to notice
- Contiguous memory gives fast indexing.
- Sequential scans are cache-friendly.
- Middle insert/delete is expensive because elements shift.
- In Python, `list` behaves like a dynamic array, so append is usually cheap but occasional resizing is expensive.

### Companion drill — elementary dynamic array mental model
```python
class TinyDynamicArray:
    def __init__(self):
        self.capacity = 1
        self.size = 0
        self.data = [None] * self.capacity

    def append(self, value):
        if self.size == self.capacity:
            self._resize(self.capacity * 2)
        self.data[self.size] = value
        self.size += 1

    def _resize(self, new_capacity):
        new_data = [None] * new_capacity
        for i in range(self.size):
            new_data[i] = self.data[i]
        self.data = new_data
        self.capacity = new_capacity
```

This is not a LeetCode problem, but it directly covers Week 2 Day 2 amortized growth.

---

## B2. Remove Duplicates from Sorted Array
**LeetCode:** Remove Duplicates from Sorted Array

### Why this problem belongs here
It reinforces in-place array writing and two-index array compaction.

### Pattern signal
- Array is sorted.
- Need to keep one representative of each value.
- Output must reuse the original array.

### Visual
```text
write
  |
[1, 1, 2, 2, 3]
    read------->
```

### Approach
1. Keep `write` at the location where the next unique value should go.
2. Scan with `read` from left to right.
3. Whenever `nums[read] != nums[write - 1]`, write it and advance `write`.

### Python solution
```python
class Solution:
    def removeDuplicates(self, nums: list[int]) -> int:
        if not nums:
            return 0
        write = 1
        for read in range(1, len(nums)):
            if nums[read] != nums[write - 1]:
                nums[write] = nums[read]
                write += 1
        return write
```

### Complexity
- Time: `O(n)`
- Space: `O(1)`

---

## B3. Move Zeroes
**LeetCode:** Move Zeroes

### Why this problem belongs here
This is a clean in-place compaction problem and a great memory-layout discussion starter.

### Pattern signal
- Preserve non-zero order.
- Perform in-place updates.
- Compaction followed by fill.

### Visual
```text
Input : [0,1,0,3,12]
Pass 1: [1,3,12,_,_]
Pass 2: [1,3,12,0,0]
```

### Approach
1. Maintain `write` for the next non-zero location.
2. Copy each non-zero value forward.
3. Fill the rest with zeroes.

### Python solution
```python
class Solution:
    def moveZeroes(self, nums: list[int]) -> None:
        write = 0
        for x in nums:
            if x != 0:
                nums[write] = x
                write += 1
        while write < len(nums):
            nums[write] = 0
            write += 1
```

### Complexity
- Time: `O(n)`
- Space: `O(1)`

---

## B4. Merge Sorted Array
**LeetCode:** Merge Sorted Array

### Why this problem belongs here
This teaches reverse filling, array capacity usage, and how to avoid overwriting unread data.

### Pattern signal
- First array has extra space at the end.
- Both inputs are already sorted.
- Writing from the back avoids collisions.

### Visual
```text
nums1: [1,2,3,0,0,0]
nums2: [2,5,6]
fill from right -> [1,2,2,3,5,6]
```

### Approach
1. Point `i` to the last real element in `nums1`, `j` to the last element in `nums2`.
2. Point `k` to the final slot.
3. Place the larger of `nums1[i]` and `nums2[j]` at `nums1[k]`.
4. Continue until `nums2` is exhausted.

### Python solution
```python
class Solution:
    def merge(self, nums1: list[int], m: int, nums2: list[int], n: int) -> None:
        i, j, k = m - 1, n - 1, m + n - 1
        while j >= 0:
            if i >= 0 and nums1[i] > nums2[j]:
                nums1[k] = nums1[i]
                i -= 1
            else:
                nums1[k] = nums2[j]
                j -= 1
            k -= 1
```

### Complexity
- Time: `O(m + n)`
- Space: `O(1)`

---

## B5. Rotate Array
**LeetCode:** Rotate Array

### Why this problem belongs here
It is a strong in-place array manipulation problem and perfect for space complexity discussion.

### Pattern signal
- Need cyclic shift.
- Prefer `O(1)` extra space.
- Reversal trick converts rotation into local reversals.

### Visual
```text
[1,2,3,4,5,6,7], k=3
reverse all   -> [7,6,5,4,3,2,1]
reverse first -> [5,6,7,4,3,2,1]
reverse last  -> [5,6,7,1,2,3,4]
```

### Python solution
```python
class Solution:
    def rotate(self, nums: list[int], k: int) -> None:
        n = len(nums)
        k %= n

        def rev(l, r):
            while l < r:
                nums[l], nums[r] = nums[r], nums[l]
                l += 1
                r -= 1

        rev(0, n - 1)
        rev(0, k - 1)
        rev(k, n - 1)
```

### Complexity
- Time: `O(n)`
- Space: `O(1)`

---

# Part C — Linked structures

## C1. Reverse Linked List
**LeetCode:** Reverse Linked List

### Why this problem belongs here
This is the first must-master linked-list pointer rewrite problem.

### Pattern signal
- Need to redirect pointers, not values.
- Only one pass required.

### Visual
```text
prev <- 1 <- 2 <- 3    curr -> None
```

### Approach
1. Track `prev`, `curr`, and `next_node`.
2. Save next node before breaking the pointer.
3. Reverse the current pointer.
4. Advance all pointers.

### Python solution
```python
class Solution:
    def reverseList(self, head):
        prev = None
        curr = head
        while curr:
            next_node = curr.next
            curr.next = prev
            prev = curr
            curr = next_node
        return prev
```

### Complexity
- Time: `O(n)`
- Space: `O(1)`

---

## C2. Linked List Cycle
**LeetCode:** Linked List Cycle

### Why this problem belongs here
It is the canonical fast/slow pointer introduction on linked structures.

### Pattern signal
- Linked list may loop back.
- Need detection without extra memory.

### Visual
```text
slow: 1 step
fast: 2 steps
If a cycle exists, fast eventually laps slow.
```

### Approach
1. Use two pointers: slow moves one step, fast moves two.
2. If they ever meet, a cycle exists.
3. If fast reaches `None`, no cycle exists.

### Python solution
```python
class Solution:
    def hasCycle(self, head) -> bool:
        slow = fast = head
        while fast and fast.next:
            slow = slow.next
            fast = fast.next.next
            if slow == fast:
                return True
        return False
```

### Complexity
- Time: `O(n)`
- Space: `O(1)`

---

## C3. Merge Two Sorted Lists
**LeetCode:** Merge Two Sorted Lists

### Why this problem belongs here
It combines pointer handling with sorted-order reasoning.

### Pattern signal
- Two sorted linked lists.
- Need stable merge with pointer manipulation.

### Approach
1. Use a dummy node to simplify head handling.
2. Compare current nodes from both lists.
3. Attach the smaller one and advance.
4. Append the remainder at the end.

### Python solution
```python
class Solution:
    def mergeTwoLists(self, list1, list2):
        dummy = ListNode(0)
        tail = dummy

        while list1 and list2:
            if list1.val <= list2.val:
                tail.next = list1
                list1 = list1.next
            else:
                tail.next = list2
                list2 = list2.next
            tail = tail.next

        tail.next = list1 if list1 else list2
        return dummy.next
```

### Complexity
- Time: `O(m + n)`
- Space: `O(1)` auxiliary

---

# Part D — Stacks, queues, and deques

## D1. Valid Parentheses
**LeetCode:** Valid Parentheses

### Why this problem belongs here
It is the stack problem every student must internalize early.

### Pattern signal
- Need to match most recent unmatched opening bracket.
- LIFO behavior is natural.

### Visual
```text
({[]})
push ( { [
pop  ] } )
```

### Approach
1. Push opening brackets.
2. On a closing bracket, the stack top must be the matching opener.
3. If not, return `False`.
4. Valid string leaves the stack empty.

### Python solution
```python
class Solution:
    def isValid(self, s: str) -> bool:
        match = {')': '(', ']': '[', '}': '{'}
        stack = []
        for ch in s:
            if ch in '([{':
                stack.append(ch)
            else:
                if not stack or stack[-1] != match[ch]:
                    return False
                stack.pop()
        return not stack
```

### Complexity
- Time: `O(n)`
- Space: `O(n)`

---

## D2. Implement Queue using Stacks
**LeetCode:** Implement Queue using Stacks

### Why this problem belongs here
It teaches data-structure simulation and amortized analysis.

### Pattern signal
- Need FIFO behavior using LIFO containers.
- Transfer happens only when necessary.

### Visual
```text
in_stack  -> newest on top
out_stack -> oldest on top for dequeue
```

### Approach
1. Push all new items into `in_stack`.
2. For `pop` or `peek`, if `out_stack` is empty, pour everything from `in_stack` into `out_stack`.
3. This reverses order and exposes the oldest element.

### Python solution
```python
class MyQueue:
    def __init__(self):
        self.in_stack = []
        self.out_stack = []

    def push(self, x: int) -> None:
        self.in_stack.append(x)

    def _shift(self):
        if not self.out_stack:
            while self.in_stack:
                self.out_stack.append(self.in_stack.pop())

    def pop(self) -> int:
        self._shift()
        return self.out_stack.pop()

    def peek(self) -> int:
        self._shift()
        return self.out_stack[-1]

    def empty(self) -> bool:
        return not self.in_stack and not self.out_stack
```

### Complexity
- Amortized push/pop/peek: `O(1)`
- Space: `O(n)`

---

## D3. Design Circular Queue
**LeetCode:** Design Circular Queue

### Why this problem belongs here
This is the clearest queue implementation problem for array-based wraparound logic.

### Pattern signal
- Fixed-size queue.
- Head/tail movement must wrap around.

### Visual
```text
index: 0 1 2 3 4
       ^       ^
      head    tail
next index = (index + 1) % capacity
```

### Approach
1. Store a fixed-size array.
2. Track `head`, `tail`, and current `size`.
3. Use modulo arithmetic to wrap indices.
4. Distinguish full and empty states using size.

### Python solution
```python
class MyCircularQueue:
    def __init__(self, k: int):
        self.q = [0] * k
        self.k = k
        self.head = 0
        self.tail = -1
        self.size = 0

    def enQueue(self, value: int) -> bool:
        if self.isFull():
            return False
        self.tail = (self.tail + 1) % self.k
        self.q[self.tail] = value
        self.size += 1
        return True

    def deQueue(self) -> bool:
        if self.isEmpty():
            return False
        self.head = (self.head + 1) % self.k
        self.size -= 1
        return True

    def Front(self) -> int:
        return -1 if self.isEmpty() else self.q[self.head]

    def Rear(self) -> int:
        return -1 if self.isEmpty() else self.q[self.tail]

    def isEmpty(self) -> bool:
        return self.size == 0

    def isFull(self) -> bool:
        return self.size == self.k
```

### Complexity
- Each operation: `O(1)`
- Space: `O(k)`

---

## D4. Sliding Window Maximum
**LeetCode:** Sliding Window Maximum

### Why this problem belongs here
It is the best deque showcase, even though it also previews Phase 2 patterns.

### Pattern signal
- Need max for each window.
- Old indices expire from the left.
- Smaller values behind a larger value are useless.

### Visual
```text
deque stores indices in decreasing value order
nums = [1,3,-1,-3,5]
deque values: [3,-1] then [5]
```

### Approach
1. Store indices in a deque.
2. Remove expired indices from the front.
3. Remove smaller values from the back because they can never become window maximum again.
4. Front of deque is the maximum.

### Python solution
```python
from collections import deque

class Solution:
    def maxSlidingWindow(self, nums: list[int], k: int) -> list[int]:
        dq = deque()
        ans = []

        for i, x in enumerate(nums):
            while dq and dq[0] <= i - k:
                dq.popleft()
            while dq and nums[dq[-1]] <= x:
                dq.pop()
            dq.append(i)
            if i >= k - 1:
                ans.append(nums[dq[0]])
        return ans
```

### Complexity
- Time: `O(n)`
- Space: `O(k)`

---

# Part E — Binary search mastery

## E1. Binary Search
**LeetCode:** Binary Search

### Why this problem belongs here
This is the pure invariant-based baseline.

### Pattern signal
- Sorted array.
- Exact target lookup.

### Invariant
If the target exists, it is always inside the current `[lo, hi]` range.

### Python solution
```python
class Solution:
    def search(self, nums: list[int], target: int) -> int:
        lo, hi = 0, len(nums) - 1
        while lo <= hi:
            mid = lo + (hi - lo) // 2
            if nums[mid] == target:
                return mid
            if nums[mid] < target:
                lo = mid + 1
            else:
                hi = mid - 1
        return -1
```

### Complexity
- Time: `O(log n)`
- Space: `O(1)`

---

## E2. Search Insert Position
**LeetCode:** Search Insert Position

### Why this problem belongs here
This teaches lower-bound style thinking.

### Pattern signal
- Need either exact index or insertion slot.
- Search boundary is more important than equality alone.

### Approach
1. Search for the first index where value is greater than or equal to target.
2. That index is the insertion position.

### Python solution
```python
class Solution:
    def searchInsert(self, nums: list[int], target: int) -> int:
        lo, hi = 0, len(nums)
        while lo < hi:
            mid = lo + (hi - lo) // 2
            if nums[mid] < target:
                lo = mid + 1
            else:
                hi = mid
        return lo
```

### Complexity
- Time: `O(log n)`
- Space: `O(1)`

---

## E3. Find First and Last Position of Element in Sorted Array
**LeetCode:** Find First and Last Position of Element in Sorted Array

### Why this problem belongs here
It forces precision with lower-bound and upper-bound variants.

### Visual
```text
nums = [5,7,7,8,8,10], target = 8
first >= 8 -> 3
first > 8  -> 5
answer     -> [3, 4]
```

### Approach
1. Write a helper that returns first index where `nums[i] >= target`.
2. Compute `left = lower_bound(target)`.
3. Compute `right = lower_bound(target + 1) - 1`.
4. Validate that `left` really contains target.

### Python solution
```python
class Solution:
    def searchRange(self, nums: list[int], target: int) -> list[int]:
        def lower_bound(x):
            lo, hi = 0, len(nums)
            while lo < hi:
                mid = lo + (hi - lo) // 2
                if nums[mid] < x:
                    lo = mid + 1
                else:
                    hi = mid
            return lo

        left = lower_bound(target)
        if left == len(nums) or nums[left] != target:
            return [-1, -1]
        right = lower_bound(target + 1) - 1
        return [left, right]
```

### Complexity
- Time: `O(log n)`
- Space: `O(1)`

---

## E4. Koko Eating Bananas
**LeetCode:** Koko Eating Bananas

### Why this problem belongs here
This is the best early binary-search-on-answer problem.

### Pattern signal
- Need minimum feasible rate.
- Feasibility check is monotone.

### Visual
```text
speed too small -> impossible
speed large enough -> possible
Find first possible speed.
```

### Approach
1. Search eating speed from `1` to `max(piles)`.
2. For a guessed speed `k`, compute total hours needed.
3. If total hours fit, try smaller speed.
4. Else increase speed.

### Python solution
```python
class Solution:
    def minEatingSpeed(self, piles: list[int], h: int) -> int:
        def can_finish(speed):
            hours = 0
            for p in piles:
                hours += (p + speed - 1) // speed
            return hours <= h

        lo, hi = 1, max(piles)
        while lo < hi:
            mid = lo + (hi - lo) // 2
            if can_finish(mid):
                hi = mid
            else:
                lo = mid + 1
        return lo
```

### Complexity
- Time: `O(n log M)`, where `M = max(piles)`
- Space: `O(1)`

---

# Part F — Strings and numbers

## F1. String to Integer (atoi)
**LeetCode:** String to Integer (atoi)

### Why this problem belongs here
It directly maps to Week 2 Day 6 representation and conversion topics.

### Pattern signal
- Need manual parsing.
- Must handle whitespace, sign, digits, overflow, and stop conditions.

### Visual
```text
"   -42abc"
trim -> sign -> accumulate digits -> stop at 'a'
```

### Approach
1. Skip leading spaces.
2. Read optional sign.
3. Accumulate digits using `res = res * 10 + digit`.
4. Clamp to 32-bit integer range when overflow would happen.

### Python solution
```python
class Solution:
    def myAtoi(self, s: str) -> int:
        i, n = 0, len(s)
        while i < n and s[i] == ' ':
            i += 1

        sign = 1
        if i < n and s[i] in '+-':
            sign = -1 if s[i] == '-' else 1
            i += 1

        INT_MIN, INT_MAX = -(2 ** 31), 2 ** 31 - 1
        num = 0
        while i < n and s[i].isdigit():
            digit = ord(s[i]) - ord('0')
            if num > (INT_MAX - digit) // 10:
                return INT_MAX if sign == 1 else INT_MIN
            num = num * 10 + digit
            i += 1
        return sign * num
```

### Complexity
- Time: `O(n)`
- Space: `O(1)`

---

## F2. Add Binary
**LeetCode:** Add Binary

### Why this problem belongs here
It strengthens low-level number representation intuition.

### Pattern signal
- Right-to-left digit processing.
- Carry management.

### Visual
```text
  1011
+ 1101
------
11000
```

### Python solution
```python
class Solution:
    def addBinary(self, a: str, b: str) -> str:
        i, j = len(a) - 1, len(b) - 1
        carry = 0
        out = []

        while i >= 0 or j >= 0 or carry:
            total = carry
            if i >= 0:
                total += int(a[i])
                i -= 1
            if j >= 0:
                total += int(b[j])
                j -= 1
            out.append(str(total % 2))
            carry = total // 2

        return ''.join(reversed(out))
```

### Complexity
- Time: `O(n + m)`
- Space: `O(n + m)` for the output

---

# Part G — Sorting

## G1. Concept bridge: elementary sorts
Week 3 Day 1 explicitly includes bubble sort, selection sort, and insertion sort. These do not always appear as the best interview choices, but they are still essential for understanding invariants, adaptiveness, and stability.

### Companion drill — three elementary sorts
```python
def bubble_sort(nums):
    arr = nums[:]
    n = len(arr)
    for i in range(n):
        swapped = False
        for j in range(0, n - 1 - i):
            if arr[j] > arr[j + 1]:
                arr[j], arr[j + 1] = arr[j + 1], arr[j]
                swapped = True
        if not swapped:
            break
    return arr


def selection_sort(nums):
    arr = nums[:]
    n = len(arr)
    for i in range(n):
        mn = i
        for j in range(i + 1, n):
            if arr[j] < arr[mn]:
                mn = j
        arr[i], arr[mn] = arr[mn], arr[i]
    return arr


def insertion_sort(nums):
    arr = nums[:]
    for i in range(1, len(arr)):
        key = arr[i]
        j = i - 1
        while j >= 0 and arr[j] > key:
            arr[j + 1] = arr[j]
            j -= 1
        arr[j + 1] = key
    return arr
```

### What to say in an interview
- Bubble sort: mostly educational, rarely chosen in practice.
- Selection sort: minimizes swaps but stays quadratic.
- Insertion sort: excellent for tiny or nearly sorted arrays.

---

## G2. Sort an Array
**LeetCode:** Sort an Array

### Why this problem belongs here
This is the clean anchor for merge sort or quicksort implementation.

### Recommended interview answer
Use **merge sort** when you want predictability and easier correctness reasoning.

### Visual
```text
[5,2,3,1]
-> [5,2] [3,1]
-> [2,5] [1,3]
-> [1,2,3,5]
```

### Approach
1. Split the array into halves.
2. Recursively sort both halves.
3. Merge the sorted halves with two pointers.

### Python solution
```python
class Solution:
    def sortArray(self, nums: list[int]) -> list[int]:
        def merge_sort(arr):
            if len(arr) <= 1:
                return arr
            mid = len(arr) // 2
            left = merge_sort(arr[:mid])
            right = merge_sort(arr[mid:])

            i = j = 0
            merged = []
            while i < len(left) and j < len(right):
                if left[i] <= right[j]:
                    merged.append(left[i])
                    i += 1
                else:
                    merged.append(right[j])
                    j += 1
            merged.extend(left[i:])
            merged.extend(right[j:])
            return merged

        return merge_sort(nums)
```

### Complexity
- Time: `O(n log n)`
- Space: `O(n)`

---

## G3. Sort Colors
**LeetCode:** Sort Colors

### Why this problem belongs here
It teaches partitioning and in-place reordering. It also previews quicksort partition logic.

### Pattern signal
- Only three values exist.
- Need one-pass partition.

### Visual
```text
[0-region | 1-region | unknown | 2-region]
```

### Approach
1. Maintain three regions using `low`, `mid`, and `high`.
2. `0` goes to the left region.
3. `2` goes to the right region.
4. `1` stays in the middle.

### Python solution
```python
class Solution:
    def sortColors(self, nums: list[int]) -> None:
        low = mid = 0
        high = len(nums) - 1

        while mid <= high:
            if nums[mid] == 0:
                nums[low], nums[mid] = nums[mid], nums[low]
                low += 1
                mid += 1
            elif nums[mid] == 1:
                mid += 1
            else:
                nums[mid], nums[high] = nums[high], nums[mid]
                high -= 1
```

### Complexity
- Time: `O(n)`
- Space: `O(1)`

---

## G4. Merge Intervals of sorting intuition
This syllabus block is still Phase 1 adjacent: once sorting is comfortable, many array problems start with sort-first reasoning. Keep that thought for Phase 2, where interval-heavy patterns become central.

---

# Part H — Heaps and priority queues

## H1. Kth Largest Element in an Array
**LeetCode:** Kth Largest Element in an Array

### Why this problem belongs here
It is the simplest heap selection problem and demonstrates why full sorting is sometimes unnecessary.

### Pattern signal
- Need only the `k`th largest, not complete order.
- Maintain a small heap of the best candidates.

### Visual
```text
min-heap of size k
heap contains current k largest elements
heap top = kth largest
```

### Approach
1. Keep a min-heap of size `k`.
2. Push each number.
3. If the heap grows beyond `k`, pop the smallest.
4. Heap top is the answer.

### Python solution
```python
import heapq

class Solution:
    def findKthLargest(self, nums: list[int], k: int) -> int:
        heap = []
        for x in nums:
            heapq.heappush(heap, x)
            if len(heap) > k:
                heapq.heappop(heap)
        return heap[0]
```

### Complexity
- Time: `O(n log k)`
- Space: `O(k)`

---

## H2. Top K Frequent Elements
**LeetCode:** Top K Frequent Elements

### Why this problem belongs here
This combines hashing and heaps, which is exactly how many real interview problems are built.

### Pattern signal
- Need frequencies first.
- Then need top `k` extraction.

### Approach
1. Count frequencies with a hash map.
2. Push `(frequency, value)` into a min-heap.
3. Keep heap size at most `k`.
4. Extract the values.

### Python solution
```python
import heapq
from collections import Counter

class Solution:
    def topKFrequent(self, nums: list[int], k: int) -> list[int]:
        freq = Counter(nums)
        heap = []
        for num, count in freq.items():
            heapq.heappush(heap, (count, num))
            if len(heap) > k:
                heapq.heappop(heap)
        return [num for _, num in heap]
```

### Complexity
- Time: `O(n log k)`
- Space: `O(n)`

---

# Part I — Hash tables and rolling hash

## I1. Contains Duplicate
**LeetCode:** Contains Duplicate

### Why this problem belongs here
This is the cleanest first hash-set problem.

### Pattern signal
- Need repeated element detection.
- Membership query speed matters.

### Python solution
```python
class Solution:
    def containsDuplicate(self, nums: list[int]) -> bool:
        seen = set()
        for x in nums:
            if x in seen:
                return True
            seen.add(x)
        return False
```

### Complexity
- Time: `O(n)` average
- Space: `O(n)`

---

## I2. Valid Anagram
**LeetCode:** Valid Anagram

### Why this problem belongs here
It exercises frequency counting and string representation reasoning.

### Pattern signal
- Same multiset of characters.
- Order does not matter, counts do.

### Python solution
```python
from collections import Counter

class Solution:
    def isAnagram(self, s: str, t: str) -> bool:
        return Counter(s) == Counter(t)
```

### Complexity
- Time: `O(n + m)`
- Space: `O(1)` or `O(k)` depending on alphabet model

---

## I3. Group Anagrams
**LeetCode:** Group Anagrams

### Why this problem belongs here
This is the first truly important hash-map key design problem.

### Pattern signal
- Need canonical representation for equivalent strings.
- Group by that representation.

### Visual
```text
"eat", "tea", "ate" -> key = sorted letters or 26-count signature
```

### Approach
1. Build a 26-length count signature for each string.
2. Use the tuple of counts as the hash key.
3. Append the string into the corresponding bucket.

### Python solution
```python
from collections import defaultdict

class Solution:
    def groupAnagrams(self, strs: list[str]) -> list[list[str]]:
        groups = defaultdict(list)
        for s in strs:
            count = [0] * 26
            for ch in s:
                count[ord(ch) - ord('a')] += 1
            groups[tuple(count)].append(s)
        return list(groups.values())
```

### Complexity
- Time: `O(total characters)`
- Space: `O(total characters)`

---

## I4. Design HashMap
**LeetCode:** Design HashMap

### Why this problem belongs here
This directly matches Week 3 hash table implementation goals.

### Pattern signal
- Need insert, get, remove.
- Collision resolution must be handled.

### Visual
```text
bucket index = key % bucket_count
bucket -> list of (key, value) pairs
```

### Approach
1. Use an array of buckets.
2. Map key to bucket using modulo.
3. Store key-value pairs inside each bucket list.
4. On collision, linearly scan within the bucket.

### Python solution
```python
class MyHashMap:
    def __init__(self):
        self.size = 1000
        self.buckets = [[] for _ in range(self.size)]

    def _idx(self, key):
        return key % self.size

    def put(self, key: int, value: int) -> None:
        bucket = self.buckets[self._idx(key)]
        for pair in bucket:
            if pair[0] == key:
                pair[1] = value
                return
        bucket.append([key, value])

    def get(self, key: int) -> int:
        bucket = self.buckets[self._idx(key)]
        for k, v in bucket:
            if k == key:
                return v
        return -1

    def remove(self, key: int) -> None:
        bucket = self.buckets[self._idx(key)]
        for i, (k, _) in enumerate(bucket):
            if k == key:
                bucket.pop(i)
                return
```

### Complexity
- Average put/get/remove: `O(1)`
- Worst case: `O(n)` if collisions are pathological

### Interview note
Mention load factor and resizing even if the toy implementation omits rehashing.

---

## I5. Two Sum
**LeetCode:** Two Sum

### Why this problem belongs here
Even though it becomes a general pattern later, it is also a perfect hash-table fundamentals problem.

### Pattern signal
- Need complement lookup in constant expected time.

### Python solution
```python
class Solution:
    def twoSum(self, nums: list[int], target: int) -> list[int]:
        seen = {}
        for i, x in enumerate(nums):
            need = target - x
            if need in seen:
                return [seen[need], i]
            seen[x] = i
```

### Complexity
- Time: `O(n)` average
- Space: `O(n)`

---

## I6. Repeated DNA Sequences
**LeetCode:** Repeated DNA Sequences

### Why this problem belongs here
This is the best Phase 1 rolling-hash style extension problem.

### Pattern signal
- Fixed-size substring window.
- Need repeated sequence detection.
- Straight substring extraction works, but rolling techniques are the conceptual prize.

### Simple interview-safe approach
Use a hash set of 10-character substrings first. Mention rolling hash as the optimized conceptual extension.

### Python solution
```python
class Solution:
    def findRepeatedDnaSequences(self, s: str) -> list[str]:
        seen = set()
        repeated = set()
        for i in range(len(s) - 9):
            sub = s[i:i + 10]
            if sub in seen:
                repeated.add(sub)
            else:
                seen.add(sub)
        return list(repeated)
```

### Complexity
- Time: `O(n)` average for fixed-size windows
- Space: `O(n)`

### Rolling hash extension
For a deeper Week 3 Day 5 review, encode `A,C,G,T` into bits and slide a compact rolling integer window.

---

# Part J — Final Phase 1 ladder

## Must-solve set
1. Fibonacci Number
2. Pow(x, n)
3. Find Peak Element
4. Remove Duplicates from Sorted Array
5. Move Zeroes
6. Reverse Linked List
7. Valid Parentheses
8. Binary Search
9. Search Insert Position
10. Koko Eating Bananas
11. Sort an Array
12. Kth Largest Element in an Array
13. Contains Duplicate
14. Group Anagrams
15. Design HashMap

## Should-solve set
1. Climbing Stairs
2. Sqrt(x)
3. Merge Sorted Array
4. Rotate Array
5. Linked List Cycle
6. Merge Two Sorted Lists
7. Implement Queue using Stacks
8. Design Circular Queue
9. Sliding Window Maximum
10. Top K Frequent Elements
11. Valid Anagram
12. Two Sum
13. Repeated DNA Sequences
14. String to Integer (atoi)
15. Add Binary
16. Sort Colors

---

## Category-to-problem coverage check

| Syllabus topic | Covered by |
|---|---|
| RAM model and locality | Move Zeroes, Rotate Array, array companion drills |
| Asymptotic analysis | Binary Search, Sqrt(x), Pow(x, n) |
| Space complexity | Rotate Array, Reverse Linked List, merge vs in-place notes |
| Recursion | Fibonacci Number, Pow(x, n), Climbing Stairs |
| Memoization | Fibonacci drill, Climbing Stairs |
| Peak finding | Find Peak Element |
| Arrays | Remove Duplicates, Move Zeroes, Merge Sorted Array, Rotate Array |
| Dynamic arrays and amortization | TinyDynamicArray drill, Implement Queue using Stacks amortized discussion |
| Linked lists | Reverse Linked List, Linked List Cycle, Merge Two Sorted Lists |
| Stacks | Valid Parentheses |
| Queues | Implement Queue using Stacks, Design Circular Queue |
| Deques | Sliding Window Maximum |
| Binary search invariants | Binary Search, Search Insert Position, Search Range |
| Binary search on answer | Sqrt(x), Koko Eating Bananas |
| Strings and number representation | String to Integer, Add Binary, Valid Anagram |
| Elementary sorts | companion drill |
| Merge sort | Sort an Array |
| Quicksort and partition intuition | Sort Colors, Sort an Array discussion |
| Heaps and heapify intuition | Kth Largest, Top K Frequent |
| Hash tables | Contains Duplicate, Two Sum, Group Anagrams, Design HashMap |
| Rolling hash | Repeated DNA Sequences extension |

---

## Recommended execution rhythm
- Day 1: A2, A3, A5
- Day 2: A4, A6, B2
- Day 3: B3, B4, B5
- Day 4: C1, C2, C3
- Day 5: D1, D2, D3, E1
- Day 6: E2, E3, E4, F1
- Day 7: F2, G2, G3
- Day 8: H1, H2, I1
- Day 9: I2, I3, I4
- Day 10: I5, I6, revision drills

---

## Interview behaviors to practice in Phase 1
- State the invariant before coding.
- Name the data structure before naming the syntax.
- Explain why a naive approach is too slow or too memory-heavy.
- Mention one trade-off for every optimized solution.
- For arrays and linked lists, say out loud whether updates are **in-place**.
- For binary search, explicitly define what the search space means.
- For hashing, mention average-case versus worst-case behavior.

---

## Exit criteria for Phase 1
You are ready for Phase 2 only when you can do the following consistently:
- Implement binary search variants without off-by-one confusion.
- Explain the difference between recursion, memoization, and bottom-up DP.
- Reverse and merge linked lists without pointer mistakes.
- Use stack, queue, and deque deliberately rather than by guesswork.
- Explain when heap beats full sorting.
- Design a hash-based solution and justify the key choice.

---

## Next phase preview
Phase 2 will shift from basic structures to **problem-solving patterns**: two pointers, sliding windows, hash-map patterns, monotonic stack, intervals, partition, Kadane, fast/slow pointers, palindrome patterns, substring techniques, parentheses, string transformations, and rolling-hash string matching.

---

# Supplementary add-ons for Phase 1

## What was worth adding before Phase 2
The main Phase 1 file already covered the core syllabus, but the following supplementary material strengthens weak spots that often cause trouble later:
- **Problem taxonomy table** for deliberate practice instead of random solving.
- **Tags/keywords** so each problem is connected to a reusable pattern signal.
- **Supplementary anchors** for recursion, implementation, and hash design that improve transition into later phases.
- **Readiness checklist** so Phase 2 starts only after the foundations are actually stable.

---

## Supplementary problem additions
These are not strictly required to understand the basics, but they improve Phase 1 depth substantially.

### 1. Palindrome Linked List
**Why add it:** strengthens linked-list traversal plus reverse-half reasoning.

**Tags:** `linked-list`, `fast-slow-pointers`, `reverse`, `in-place`

```python
class Solution:
    def isPalindrome(self, head) -> bool:
        slow = fast = head
        while fast and fast.next:
            slow = slow.next
            fast = fast.next.next

        prev = None
        while slow:
            nxt = slow.next
            slow.next = prev
            prev = slow
            slow = nxt

        left, right = head, prev
        while right:
            if left.val != right.val:
                return False
            left = left.next
            right = right.next
        return True
```

### 2. Min Stack
**Why add it:** deepens stack implementation skill and invariant maintenance.

**Tags:** `stack`, `design`, `auxiliary-state`, `invariant`

```python
class MinStack:
    def __init__(self):
        self.stack = []
        self.min_stack = []

    def push(self, val: int) -> None:
        self.stack.append(val)
        if not self.min_stack or val <= self.min_stack[-1]:
            self.min_stack.append(val)

    def pop(self) -> None:
        if self.stack[-1] == self.min_stack[-1]:
            self.min_stack.pop()
        self.stack.pop()

    def top(self) -> int:
        return self.stack[-1]

    def getMin(self) -> int:
        return self.min_stack[-1]
```

### 3. First Bad Version
**Why add it:** one of the simplest clean invariant exercises for binary search correctness.

**Tags:** `binary-search`, `lower-bound`, `monotonic-predicate`

```python
class Solution:
    def firstBadVersion(self, n: int) -> int:
        lo, hi = 1, n
        while lo < hi:
            mid = lo + (hi - lo) // 2
            if isBadVersion(mid):
                hi = mid
            else:
                lo = mid + 1
        return lo
```

### 4. Implement Stack using Queues
**Why add it:** complements queue-using-stacks and reinforces simulation trade-offs.

**Tags:** `stack`, `queue`, `design`, `simulation`

```python
from collections import deque

class MyStack:
    def __init__(self):
        self.q = deque()

    def push(self, x: int) -> None:
        self.q.append(x)
        for _ in range(len(self.q) - 1):
            self.q.append(self.q.popleft())

    def pop(self) -> int:
        return self.q.popleft()

    def top(self) -> int:
        return self.q[0]

    def empty(self) -> bool:
        return not self.q
```

### 5. Rabin–Karp mini drill
This one is added as a **concept drill** because Week 3 explicitly includes rolling hash, but many learners never build the intuition unless they see the sliding update formula.

```python
def rabin_karp(text: str, pattern: str) -> list[int]:
    if len(pattern) > len(text):
        return []

    base = 257
    mod = 10**9 + 7
    m = len(pattern)
    power = pow(base, m - 1, mod)

    ph = 0
    wh = 0
    for i in range(m):
        ph = (ph * base + ord(pattern[i])) % mod
        wh = (wh * base + ord(text[i])) % mod

    ans = []
    for i in range(len(text) - m + 1):
        if ph == wh and text[i:i + m] == pattern:
            ans.append(i)
        if i + m < len(text):
            wh = (wh - ord(text[i]) * power) % mod
            wh = (wh * base + ord(text[i + m])) % mod
    return ans
```

Visual:
```text
window hash(i+1)
= remove left char -> multiply by base -> add new right char
```

---

## Phase 1 practice table
Use this table as the main drill sheet before moving to Phase 2.

| # | Problem | Category | Difficulty | Tags / keywords | Priority |
|---|---|---|---|---|---|
| 1 | Fibonacci Number | Recursion / DP basics | Easy | recursion, memoization, bottom-up, state-transition | Must |
| 2 | Pow(x, n) | Divide and conquer | Medium | recursion, exponentiation-by-squaring, logarithmic, math | Must |
| 3 | Climbing Stairs | Memoization / DP basics | Easy | recursion, dp, rolling-state, overlapping-subproblems | Should |
| 4 | Sqrt(x) | Binary search on value | Easy | binary-search, answer-space, monotonicity, bounds | Should |
| 5 | Find Peak Element | Divide and conquer / binary search | Medium | peak-finding, local-slope, invariant, logarithmic | Must |
| 6 | Remove Duplicates from Sorted Array | Arrays | Easy | in-place, two-pointers, compaction, sorted-array | Must |
| 7 | Move Zeroes | Arrays | Easy | in-place, stable-compaction, write-pointer, sequential-scan | Must |
| 8 | Merge Sorted Array | Arrays | Easy | reverse-fill, two-pointers, in-place, sorted-merge | Should |
| 9 | Rotate Array | Arrays | Medium | reversal, cyclic-shift, in-place, space-tradeoff | Should |
| 10 | Reverse Linked List | Linked list | Easy | pointers, iterative, in-place, reversal | Must |
| 11 | Linked List Cycle | Linked list | Easy | fast-slow-pointers, cycle-detection, two-speed | Should |
| 12 | Merge Two Sorted Lists | Linked list | Easy | dummy-node, merge, pointer-manipulation, stable | Should |
| 13 | Palindrome Linked List | Linked list | Easy | reverse-half, fast-slow-pointers, compare-halves | Supplementary |
| 14 | Valid Parentheses | Stack | Easy | stack, bracket-matching, parsing, LIFO | Must |
| 15 | Min Stack | Stack design | Medium | stack, design, auxiliary-min, invariant | Supplementary |
| 16 | Implement Queue using Stacks | Queue simulation | Easy | amortized, simulation, two-stacks, FIFO | Should |
| 17 | Implement Stack using Queues | Stack simulation | Easy | simulation, queue-rotation, design, LIFO | Supplementary |
| 18 | Design Circular Queue | Queue / circular buffer | Medium | circular-array, modulo, head-tail, fixed-capacity | Should |
| 19 | Sliding Window Maximum | Deque | Hard | monotonic-deque, window, indices, amortized | Should+ |
| 20 | Binary Search | Binary search | Easy | invariant, sorted-array, mid, bounds | Must |
| 21 | Search Insert Position | Binary search | Easy | lower-bound, insertion-point, invariant | Must |
| 22 | Find First and Last Position of Element in Sorted Array | Binary search | Medium | lower-bound, upper-bound, duplicates, boundaries | Must |
| 23 | First Bad Version | Binary search | Easy | monotonic-predicate, first-true, lower-bound | Supplementary |
| 24 | Koko Eating Bananas | Binary search on answer | Medium | answer-space, feasibility-check, monotonicity, greedy-check | Must |
| 25 | String to Integer (atoi) | Strings / numbers | Medium | parsing, overflow, sign, finite-state-thinking | Must |
| 26 | Add Binary | Strings / numbers | Easy | binary-addition, carry, representation, simulation | Should |
| 27 | Sort an Array | Sorting | Medium | merge-sort, divide-conquer, stable-sort, recursion | Must |
| 28 | Sort Colors | Sorting / partition | Medium | dutch-national-flag, partition, in-place, three-pointers | Should |
| 29 | Kth Largest Element in an Array | Heap | Medium | min-heap, selection, top-k, priority-queue | Must |
| 30 | Top K Frequent Elements | Heap + hashing | Medium | counter, min-heap, top-k, frequency | Should |
| 31 | Contains Duplicate | Hash set | Easy | hashing, membership, duplicate-detection | Must |
| 32 | Two Sum | Hash map | Easy | complement, hashmap, lookup-first, indices | Should |
| 33 | Valid Anagram | Hashing / strings | Easy | frequency-count, character-map, multiset | Should |
| 34 | Group Anagrams | Hash map key design | Medium | signature, tuple-count, grouping, frequency-vector | Must |
| 35 | Design HashMap | Hash table implementation | Easy | buckets, chaining, collision-resolution, modulo | Must |
| 36 | Repeated DNA Sequences | Rolling hash extension | Medium | fixed-window, hashing, substring, rolling-hash | Should |

---

## Practice blocks by category

### Block 1 — Week 1 core thinking
- Fibonacci Number
- Pow(x, n)
- Climbing Stairs
- Sqrt(x)
- Find Peak Element

### Block 2 — Week 2 linear data structures
- Remove Duplicates from Sorted Array
- Move Zeroes
- Merge Sorted Array
- Rotate Array
- Reverse Linked List
- Linked List Cycle
- Merge Two Sorted Lists
- Palindrome Linked List

### Block 3 — Week 2 operational structures
- Valid Parentheses
- Min Stack
- Implement Queue using Stacks
- Implement Stack using Queues
- Design Circular Queue
- Sliding Window Maximum

### Block 4 — Week 2 search and representation
- Binary Search
- Search Insert Position
- Find First and Last Position of Element in Sorted Array
- First Bad Version
- Koko Eating Bananas
- String to Integer (atoi)
- Add Binary

### Block 5 — Week 3 algorithms and hashing
- Sort an Array
- Sort Colors
- Kth Largest Element in an Array
- Top K Frequent Elements
- Contains Duplicate
- Two Sum
- Valid Anagram
- Group Anagrams
- Design HashMap
- Repeated DNA Sequences

---

## Suggested mastery milestones before Phase 2
- **Minimum ready:** solve all `Must` problems without reading code.
- **Strong ready:** solve all `Must` + `Should` problems and explain trade-offs.
- **Excellent ready:** solve the supplementary set too and write the key patterns from memory.

---

## Flash tags for revision
Use these tags as quick memory triggers during review.

| Theme | Tags |
|---|---|
| Recursion | base-case, reduction, recursion-tree, memoization |
| Arrays | in-place, write-pointer, reverse-fill, compaction |
| Linked list | dummy-node, prev-curr-next, slow-fast, half-reversal |
| Stack / queue | LIFO, FIFO, simulation, amortized, circular-buffer |
| Binary search | invariant, lower-bound, first-true, answer-space |
| Strings / numbers | parsing, carry, overflow, encoding, representation |
| Sorting | stable, in-place, partition, merge, divide-conquer |
| Heap | top-k, priority-queue, min-heap, bounded-heap |
| Hashing | collision, signature, frequency-map, chaining, rolling-hash |


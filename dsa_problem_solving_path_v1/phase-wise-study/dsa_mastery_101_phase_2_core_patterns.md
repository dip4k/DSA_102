# DSA Mastery 101 — Phase 2 Core Patterns & String Manipulation

## Scope
This file covers **Phase 2** of the attached syllabus: **Weeks 4-6**.

Covered syllabus areas:
- Week 4: two pointers, sliding window fixed size, sliding window variable size, divide and conquer, binary search as a pattern
- Week 5: hash map / hash set patterns, monotonic stack, interval patterns, partition and cyclic sort, Kadane's algorithm, fast/slow pointers
- Week 6: palindrome patterns, substring and sliding window on strings, parentheses and bracket matching, string transformations and building, rolling-hash-style string matching

Programming language used for all implementations: **Python**.

> Note: each problem includes an **original concise problem brief** rather than copied platform wording, while still preserving the exact problem idea, theme, and implementation target.

---

## How to use this phase file
1. Read the category overview first.
2. For each problem, identify the **signal** before looking at the code.
3. Use the **visual cue** and **walkthrough** to trace one example by hand.
4. Read the **alternate ways** section so you understand why the chosen solution is the best interview answer.
5. Pay attention to **likely coding friction**; those are the mistakes most candidates make under pressure.

---

## Phase 2 category map

| Category | Week / day alignment | Main pattern idea |
|---|---|---|
| Two pointers | Week 4 Day 1 | Move two indices to exploit ordering or structure |
| Sliding window fixed size | Week 4 Day 2 | Maintain a window of exact length efficiently |
| Sliding window variable size | Week 4 Day 3 | Expand and shrink while maintaining a constraint |
| Divide and conquer | Week 4 Day 4 | Split, solve subproblems, combine |
| Binary search as pattern | Week 4 Day 5 | Search on a monotone answer space |
| Hash map / set patterns | Week 5 Day 1 | Trade memory for fast lookup and counting |
| Monotonic stack | Week 5 Day 2 | Maintain useful order for nearest greater/smaller |
| Intervals | Week 5 Day 3 | Sort and merge overlapping segments |
| Partition / cyclic sort | Week 5 Day 4A | Reorder by bucket, color, or index placement |
| Kadane | Week 5 Day 4B | Best subarray ending here |
| Fast / slow pointers | Week 5 Day 5 | Relative-speed traversal reveals hidden structure |
| Palindrome patterns | Week 6 Day 1 | Mirror comparison around center or ends |
| String sliding windows | Week 6 Day 2 | Frequency-controlled windows on strings |
| Parentheses / bracket matching | Week 6 Day 3 | Stack or counter structure for validity |
| String transformations / building | Week 6 Day 4 | Reverse, compress, or rebuild efficiently |
| String matching / rolling hash | Week 6 Day 5 | Match substrings faster than naive retrying |

---

## Coverage promise
Every Phase 2 topic from the syllabus is represented either by a **core detailed problem**, a **supplementary problem**, or both.

---

# Part A — Two-pointer patterns

## A1. LeetCode #167 — Two Sum II: Input Array Is Sorted
**Theme:** opposite-direction pointers on a sorted array  
**Difficulty:** Medium

### Problem brief
Given a sorted array, return the 1-based indices of two numbers whose sum equals the target.

### Signal
- Array is sorted.
- Need a pair, not all pairs.
- Sum changes predictably when pointers move.

### Visual cue
```text
nums = [2, 7, 11, 15], target = 9
 l                 r
[2, 7, 11, 15]
 2 + 15 too big -> move r left
 2 + 11 too big -> move r left
 2 + 7  perfect -> done
```

### Walkthrough
1. Start one pointer at the left and one at the right.
2. If the sum is too small, move left pointer right.
3. If the sum is too large, move right pointer left.
4. Sorted order guarantees that no useful pair is skipped.

### Primary approach
Use opposite-direction pointers to shrink the search space greedily.

### Python solution
```python
class Solution:
    def twoSum(self, numbers: list[int], target: int) -> list[int]:
        left, right = 0, len(numbers) - 1
        while left < right:
            s = numbers[left] + numbers[right]
            if s == target:
                return [left + 1, right + 1]
            if s < target:
                left += 1
            else:
                right -= 1
        return []
```

### Alternate ways
- Hash map in `O(n)` time and `O(n)` space.
- Brute force in `O(n^2)`.
- The two-pointer version is better because the array is already sorted.

### Likely coding friction
- Forgetting the answer is **1-indexed**.
- Moving the wrong pointer after comparing the sum.
- Reusing Phase 1 hash map instinct even though sorted order gives a better solution.

### Complexity
- Time: `O(n)`
- Space: `O(1)`

---

## A2. LeetCode #11 — Container With Most Water
**Theme:** maximize area with opposite-direction pointers  
**Difficulty:** Medium

### Problem brief
Pick two vertical lines that, with the x-axis, hold the maximum water area.

### Signal
- Area depends on width and the shorter wall.
- Trying all pairs is too expensive.
- Only one of the two boundaries can help after each comparison.

### Visual cue
```text
height = [1,8,6,2,5,4,8,3,7]
 l                       r
width = 8
area = min(1,7) * 8 = 8
Move the shorter wall, not the taller one.
```

### Walkthrough
1. Start with the widest possible container.
2. Compute current area.
3. Move the shorter side inward, because keeping it cannot improve height and only shrinks width.
4. Track the maximum area seen.

### Primary approach
Greedy two-pointer shrinking based on the shorter height.

### Python solution
```python
class Solution:
    def maxArea(self, height: list[int]) -> int:
        left, right = 0, len(height) - 1
        best = 0
        while left < right:
            h = min(height[left], height[right])
            best = max(best, h * (right - left))
            if height[left] < height[right]:
                left += 1
            else:
                right -= 1
        return best
```

### Alternate ways
- Brute force every pair in `O(n^2)`.
- No practical improvement beats the standard two-pointer solution here.

### Likely coding friction
- Moving the taller wall instead of the shorter one.
- Forgetting why width always decreases.
- Confusing this with trapping-rain-water logic.

### Complexity
- Time: `O(n)`
- Space: `O(1)`

---

## A3. LeetCode #15 — 3Sum
**Theme:** sorting + anchored two pointers  
**Difficulty:** Medium

### Problem brief
Return all unique triplets whose values add up to zero.

### Signal
- Need triplets, not just one pair.
- Duplicates matter.
- Sorting enables structured searching.

### Visual cue
```text
sorted nums = [-4,-1,-1,0,1,2]
anchor = -1
find pair summing to +1 using left/right pointers
```

### Walkthrough
1. Sort the array.
2. Fix one number as the anchor.
3. Use two pointers on the remaining suffix to find the complement pair.
4. Skip duplicates for both the anchor and the moving pointers.

### Python solution
```python
class Solution:
    def threeSum(self, nums: list[int]) -> list[list[int]]:
        nums.sort()
        ans = []

        for i in range(len(nums)):
            if i > 0 and nums[i] == nums[i - 1]:
                continue
            left, right = i + 1, len(nums) - 1
            while left < right:
                total = nums[i] + nums[left] + nums[right]
                if total == 0:
                    ans.append([nums[i], nums[left], nums[right]])
                    left += 1
                    right -= 1
                    while left < right and nums[left] == nums[left - 1]:
                        left += 1
                    while left < right and nums[right] == nums[right + 1]:
                        right -= 1
                elif total < 0:
                    left += 1
                else:
                    right -= 1
        return ans
```

### Alternate ways
- Hash-set based pair search per anchor.
- Brute force in `O(n^3)`.
- Sorting + two pointers is the standard interview answer.

### Likely coding friction
- Duplicate handling is the main trap.
- Skipping duplicates at the wrong time can lose valid answers.
- Pointer movement after finding a valid triple must happen before duplicate skipping.

### Complexity
- Time: `O(n^2)`
- Space: `O(1)` extra, ignoring output

---

# Part B — Sliding window, fixed size

## B1. LeetCode #643 — Maximum Average Subarray I
**Theme:** fixed-size numeric window  
**Difficulty:** Easy

### Problem brief
Find the maximum average value across all contiguous subarrays of length `k`.

### Signal
- Window size is fixed.
- Recomputing each window sum from scratch wastes time.

### Visual cue
```text
window size = 4
[1 12 -5 -6] [50]
 sum old       -1 + 50 -> next sum
```

### Walkthrough
1. Compute the first window sum.
2. Slide right by removing the leftmost value and adding the incoming one.
3. Keep the maximum sum.
4. Divide by `k` at the end.

### Python solution
```python
class Solution:
    def findMaxAverage(self, nums: list[int], k: int) -> float:
        window_sum = sum(nums[:k])
        best = window_sum
        for i in range(k, len(nums)):
            window_sum += nums[i] - nums[i - k]
            best = max(best, window_sum)
        return best / k
```

### Alternate ways
- Prefix sums also work.
- Brute force gives `O(nk)`.
- Fixed-window rolling sum is simplest and fastest.

### Likely coding friction
- Off-by-one in removing `nums[i-k]`.
- Updating max before the first valid full window exists.

### Complexity
- Time: `O(n)`
- Space: `O(1)`

---

## B2. LeetCode #438 — Find All Anagrams in a String
**Theme:** fixed-size frequency window  
**Difficulty:** Medium

### Problem brief
Find all start indices where a substring of `s` is an anagram of `p`.

### Signal
- Window size must equal `len(p)`.
- Character counts matter, order does not.

### Visual cue
```text
s = cbaebabacd
p = abc
window length = 3
[cba] -> counts match -> record 0
```

### Walkthrough
1. Count characters in `p`.
2. Slide a fixed-size window across `s`.
3. Add the new char, remove the old char.
4. Record the index when window counts match target counts.

### Python solution
```python
from collections import Counter

class Solution:
    def findAnagrams(self, s: str, p: str) -> list[int]:
        if len(p) > len(s):
            return []

        need = Counter(p)
        window = Counter(s[:len(p)])
        ans = []

        if window == need:
            ans.append(0)

        k = len(p)
        for i in range(k, len(s)):
            window[s[i]] += 1
            left_char = s[i - k]
            window[left_char] -= 1
            if window[left_char] == 0:
                del window[left_char]
            if window == need:
                ans.append(i - k + 1)

        return ans
```

### Alternate ways
- Use two length-26 arrays for lowercase letters.
- Use a `matched` counter to avoid full comparison every time.
- Array counts are usually faster than `Counter` in interviews.

### Likely coding friction
- Forgetting to delete zero-count keys when comparing counters.
- Confusing this with variable-size window problems.
- Missing that the window size is fixed from the start.

### Complexity
- Time: `O(n)` with fixed alphabet assumptions
- Space: `O(1)` for lowercase letters, otherwise `O(k)`

---

# Part C — Sliding window, variable size

## C1. LeetCode #3 — Longest Substring Without Repeating Characters
**Theme:** grow/shrink window under uniqueness constraint  
**Difficulty:** Medium

### Problem brief
Find the length of the longest substring with no repeated characters.

### Signal
- Window validity changes as new characters enter.
- Left boundary must jump forward when a duplicate appears.

### Visual cue
```text
s = abcabcbb
window grows: [abc]
see 'a' again -> left jumps just past previous 'a'
```

### Walkthrough
1. Track the last seen index of each character.
2. Expand right one character at a time.
3. If the current character was already inside the active window, move left.
4. Keep the best window length.

### Python solution
```python
class Solution:
    def lengthOfLongestSubstring(self, s: str) -> int:
        last = {}
        left = 0
        best = 0
        for right, ch in enumerate(s):
            if ch in last and last[ch] >= left:
                left = last[ch] + 1
            last[ch] = right
            best = max(best, right - left + 1)
        return best
```

### Alternate ways
- Maintain a set and shrink one step at a time.
- Last-index jump is usually cleaner and faster.

### Likely coding friction
- Updating `left` backward accidentally.
- Using `if ch in last` without checking whether it is inside the current window.

### Complexity
- Time: `O(n)`
- Space: `O(min(n, alphabet))`

---

## C2. LeetCode #904 — Fruit Into Baskets
**Theme:** at-most-k-distinct variable window  
**Difficulty:** Medium

### Problem brief
Find the longest contiguous segment containing at most two distinct fruit types.

### Signal
- Window is valid while distinct count is `<= 2`.
- When it becomes invalid, shrink from the left.

### Visual cue
```text
[1,2,1,2,3]
valid until 3rd type enters
shrink left until only 2 types remain
```

### Walkthrough
1. Track counts in the current window.
2. Expand right pointer.
3. If more than two distinct values exist, shrink from the left until valid again.
4. Track best window size.

### Python solution
```python
from collections import defaultdict

class Solution:
    def totalFruit(self, fruits: list[int]) -> int:
        count = defaultdict(int)
        left = 0
        best = 0

        for right, fruit in enumerate(fruits):
            count[fruit] += 1
            while len(count) > 2:
                count[fruits[left]] -= 1
                if count[fruits[left]] == 0:
                    del count[fruits[left]]
                left += 1
            best = max(best, right - left + 1)
        return best
```

### Alternate ways
- Same framework generalizes to at-most-`k` distinct.
- This is a core stepping-stone before minimum-window style problems.

### Likely coding friction
- Forgetting to delete a fruit type when its count becomes zero.
- Tracking best length before restoring validity.

### Complexity
- Time: `O(n)`
- Space: `O(k)` distinct types, here effectively `O(1)`

---

## C3. LeetCode #76 — Minimum Window Substring
**Theme:** smallest valid variable window  
**Difficulty:** Hard

### Problem brief
Find the smallest substring of `s` that contains every character of `t` with sufficient counts.

### Signal
- Window becomes valid only after enough required characters are collected.
- After validity is reached, shrink aggressively.

### Visual cue
```text
need: A:1 B:1 C:1
window expands -> becomes valid
then left shrinks while still valid
best = smallest valid seen
```

### Walkthrough
1. Count required characters.
2. Expand right until the window satisfies all counts.
3. While valid, try shrinking left to minimize length.
4. Track the best valid window.

### Python solution
```python
from collections import Counter

class Solution:
    def minWindow(self, s: str, t: str) -> str:
        need = Counter(t)
        missing = len(t)
        left = start = end = 0

        for right, ch in enumerate(s, 1):
            if need[ch] > 0:
                missing -= 1
            need[ch] -= 1

            if missing == 0:
                while left < right and need[s[left]] < 0:
                    need[s[left]] += 1
                    left += 1
                if end == 0 or right - left < end - start:
                    start, end = left, right
                need[s[left]] += 1
                missing += 1
                left += 1

        return s[start:end]
```

### Alternate ways
- Use two maps plus a `formed == required` counter.
- The `missing` trick is compact and elegant but harder to derive under stress.

### Likely coding friction
- This problem breaks candidates on window validity logic.
- Many bugs come from mixing up `>=` counts and exact required counts.
- Another common issue is updating the answer before the window is fully valid.

### Complexity
- Time: `O(n)` average
- Space: `O(alphabet)`

---

# Part D — Divide and conquer

## D1. LeetCode #169 — Majority Element
**Theme:** divide and conquer voting intuition  
**Difficulty:** Easy

### Problem brief
Find the element that appears more than half the time in the array.

### Signal
- A majority survives pairwise cancellation.
- Can also be reasoned recursively by halves.

### Visual cue
```text
left half majority ?
right half majority ?
combine by counting which candidate dominates overall
```

### Walkthrough
1. Recursively find the majority candidate of the left half.
2. Recursively find the majority candidate of the right half.
3. If equal, return it.
4. Otherwise count both candidates in the current range and choose the stronger one.

### Python solution
```python
class Solution:
    def majorityElement(self, nums: list[int]) -> int:
        def solve(lo, hi):
            if lo == hi:
                return nums[lo]
            mid = (lo + hi) // 2
            left = solve(lo, mid)
            right = solve(mid + 1, hi)
            if left == right:
                return left
            left_count = sum(1 for i in range(lo, hi + 1) if nums[i] == left)
            right_count = sum(1 for i in range(lo, hi + 1) if nums[i] == right)
            return left if left_count > right_count else right

        return solve(0, len(nums) - 1)
```

### Alternate ways
- Boyer-Moore voting is the best practical answer: `O(n)` time, `O(1)` space.
- Hash map counting is simpler but uses extra memory.

### Likely coding friction
- Divide and conquer is conceptually valid but not always the interview-optimal answer.
- Good answer: show D&C, then mention Boyer-Moore as the production choice.

### Complexity
- Time: `O(n log n)`
- Space: `O(log n)` recursion stack

---

## D2. LeetCode #241 — Different Ways to Add Parentheses
**Theme:** recursive split-and-combine  
**Difficulty:** Medium

### Problem brief
Given an arithmetic expression with numbers and operators, return all possible results from different parenthesizations.

### Signal
- Every operator is a possible split point.
- Left and right subexpressions are independent subproblems.

### Visual cue
```text
2*3-4*5
split at '-'
left: 2*3
right: 4*5
combine all left results with all right results
```

### Walkthrough
1. Scan the expression for operators.
2. Split around one operator.
3. Recursively solve left and right parts.
4. Combine every left result with every right result using that operator.

### Python solution
```python
from functools import lru_cache

class Solution:
    def diffWaysToCompute(self, expression: str) -> list[int]:
        @lru_cache(None)
        def solve(expr: str) -> list[int]:
            ans = []
            for i, ch in enumerate(expr):
                if ch in '+-*':
                    left = solve(expr[:i])
                    right = solve(expr[i + 1:])
                    for a in left:
                        for b in right:
                            if ch == '+':
                                ans.append(a + b)
                            elif ch == '-':
                                ans.append(a - b)
                            else:
                                ans.append(a * b)
            if not ans:
                ans.append(int(expr))
            return ans

        return solve(expression)
```

### Alternate ways
- Pure recursion works but repeats subproblems.
- Memoization is strongly recommended.

### Likely coding friction
- Forgetting multi-digit numbers.
- Splitting only once instead of trying every operator.
- Returning a single value instead of all possible results.

### Complexity
- Exponential number of combinations in the worst case
- Memoization reduces repeated work significantly

---

# Part E — Binary search as a pattern

## E1. LeetCode #875 — Koko Eating Bananas
**Theme:** binary search on minimum feasible answer  
**Difficulty:** Medium

### Problem brief
Find the smallest eating speed that lets Koko finish all piles within `h` hours.

### Signal
- If speed `k` works, any larger speed also works.
- That monotonicity creates a searchable answer space.

### Visual cue
```text
speed: 1 2 3 4 5 6 ...
work?: F F F T T T ...
find first True
```

### Walkthrough
1. Search speeds between `1` and `max(piles)`.
2. For a guessed speed, compute total hours needed.
3. If feasible, search left for a smaller valid speed.
4. Otherwise search right.

### Python solution
```python
class Solution:
    def minEatingSpeed(self, piles: list[int], h: int) -> int:
        def can_finish(speed: int) -> bool:
            hours = 0
            for p in piles:
                hours += (p + speed - 1) // speed
            return hours <= h

        left, right = 1, max(piles)
        while left < right:
            mid = left + (right - left) // 2
            if can_finish(mid):
                right = mid
            else:
                left = mid + 1
        return left
```

### Alternate ways
- Linear scan over all speeds is too slow.
- This is the standard “first feasible answer” template.

### Likely coding friction
- Wrong upper bound.
- Using floor division incorrectly for ceiling hours.
- Returning `right` or `left` inconsistently because the invariant was never stated.

### Complexity
- Time: `O(n log M)` where `M = max(piles)`
- Space: `O(1)`

---

## E2. LeetCode #1011 — Capacity To Ship Packages Within D Days
**Theme:** binary search with greedy feasibility check  
**Difficulty:** Medium

### Problem brief
Find the smallest ship capacity that lets all packages be shipped in at most `days` days, in order.

### Signal
- Capacity too small fails.
- Larger capacities never make feasibility worse.

### Visual cue
```text
capacity too small -> need too many days
capacity large enough -> days fit
search first feasible capacity
```

### Walkthrough
1. Lower bound is the heaviest package.
2. Upper bound is the sum of all weights.
3. For a candidate capacity, greedily pack packages in order.
4. Count days used and binary search on feasibility.

### Python solution
```python
class Solution:
    def shipWithinDays(self, weights: list[int], days: int) -> int:
        def can_ship(cap: int) -> bool:
            used_days = 1
            current = 0
            for w in weights:
                if current + w > cap:
                    used_days += 1
                    current = 0
                current += w
            return used_days <= days

        left, right = max(weights), sum(weights)
        while left < right:
            mid = left + (right - left) // 2
            if can_ship(mid):
                right = mid
            else:
                left = mid + 1
        return left
```

### Alternate ways
- Dynamic programming exists but is heavier.
- Greedy check + binary search is the interview sweet spot.

### Likely coding friction
- Resetting day state incorrectly after overflow.
- Choosing a lower bound below `max(weights)`.
- Accidentally reordering packages, which is not allowed.

### Complexity
- Time: `O(n log S)` where `S = sum(weights)`
- Space: `O(1)`

---

## E3. LeetCode #410 — Split Array Largest Sum
**Theme:** minimize the maximum segment sum  
**Difficulty:** Hard

### Problem brief
Split the array into `k` non-empty parts so that the largest part sum is as small as possible.

### Signal
- Ask: “Can I split using max allowed sum `X`?”
- Feasibility is monotone.

### Visual cue
```text
allowed max sum = X
scan left to right
start new segment only when current segment would exceed X
```

### Python solution
```python
class Solution:
    def splitArray(self, nums: list[int], k: int) -> int:
        def can_split(limit: int) -> bool:
            pieces = 1
            current = 0
            for x in nums:
                if current + x > limit:
                    pieces += 1
                    current = 0
                current += x
            return pieces <= k

        left, right = max(nums), sum(nums)
        while left < right:
            mid = left + (right - left) // 2
            if can_split(mid):
                right = mid
            else:
                left = mid + 1
        return left
```

### Alternate ways
- Dynamic programming is classical but more complex.
- Binary search + greedy check is usually easier to explain and code.

### Likely coding friction
- Misreading “at most `k` pieces” versus “exactly `k` pieces” inside the feasibility check.
- Starting a new piece too early or too late.

### Complexity
- Time: `O(n log S)`
- Space: `O(1)`

---

# Part F — Hash map / hash set patterns

## F1. LeetCode #560 — Subarray Sum Equals K
**Theme:** prefix sum + frequency map  
**Difficulty:** Medium

### Problem brief
Count how many subarrays sum exactly to `k`.

### Signal
- Need count of subarrays, not just one answer.
- Prefix sums turn a range sum into a difference.

### Visual cue
```text
prefix[j] - prefix[i] = k
=> prefix[i] = prefix[j] - k
count previous prefix sums equal to current - k
```

### Walkthrough
1. Maintain running prefix sum.
2. If a previous prefix sum equals `current - k`, then the subarray between them sums to `k`.
3. Count how many times each prefix sum has appeared.
4. Update the map as you scan.

### Python solution
```python
from collections import defaultdict

class Solution:
    def subarraySum(self, nums: list[int], k: int) -> int:
        freq = defaultdict(int)
        freq[0] = 1
        prefix = 0
        ans = 0

        for x in nums:
            prefix += x
            ans += freq[prefix - k]
            freq[prefix] += 1

        return ans
```

### Alternate ways
- Brute force all start/end pairs in `O(n^2)`.
- Sliding window does **not** work when negatives are allowed.

### Likely coding friction
- Forgetting `freq[0] = 1`.
- Updating the frequency map before counting matches.
- Misapplying sliding window from the previous category.

### Complexity
- Time: `O(n)`
- Space: `O(n)`

---

## F2. LeetCode #128 — Longest Consecutive Sequence
**Theme:** hash-set expansion from sequence starts  
**Difficulty:** Medium

### Problem brief
Find the length of the longest run of consecutive integers in an unsorted array.

### Signal
- Order in the array is irrelevant.
- Constant-time membership checks are valuable.
- Only start counting from true sequence starts.

### Visual cue
```text
set = {100,4,200,1,3,2}
1 is a start because 0 not in set
count: 1,2,3,4 -> length 4
```

### Walkthrough
1. Put all numbers in a set.
2. For each number, start counting only if `num - 1` is absent.
3. Expand upward while consecutive values exist.
4. Track the best length.

### Python solution
```python
class Solution:
    def longestConsecutive(self, nums: list[int]) -> int:
        s = set(nums)
        best = 0
        for x in s:
            if x - 1 not in s:
                length = 1
                while x + length in s:
                    length += 1
                best = max(best, length)
        return best
```

### Alternate ways
- Sorting gives `O(n log n)`.
- Hash-set start detection gives true linear average time.

### Likely coding friction
- Starting a count from every number and accidentally making it quadratic.
- Forgetting to deduplicate by using a set.

### Complexity
- Time: `O(n)` average
- Space: `O(n)`

---

# Part G — Monotonic stack

## G1. LeetCode #739 — Daily Temperatures
**Theme:** next greater element to the right  
**Difficulty:** Medium

### Problem brief
For each day, return how many days must pass until a warmer temperature appears.

### Signal
- Need nearest larger element on the right.
- A decreasing stack of unresolved days fits perfectly.

### Visual cue
```text
temps: [73,74,75,71,69,72,76,73]
stack stores indices with decreasing temperatures
when 76 arrives, it resolves many earlier days
```

### Walkthrough
1. Store indices in a stack.
2. While current temperature is higher than the temperature at the stack top, resolve that index.
3. Push current index.
4. Unresolved indices stay zero.

### Python solution
```python
class Solution:
    def dailyTemperatures(self, temperatures: list[int]) -> list[int]:
        ans = [0] * len(temperatures)
        stack = []

        for i, t in enumerate(temperatures):
            while stack and temperatures[stack[-1]] < t:
                j = stack.pop()
                ans[j] = i - j
            stack.append(i)

        return ans
```

### Alternate ways
- Brute force scan right for each day in `O(n^2)`.
- Reverse traversal variants also exist.

### Likely coding friction
- Storing values instead of indices, then losing distance information.
- Using `<=` instead of `<` and changing equal-temperature behavior incorrectly.

### Complexity
- Time: `O(n)`
- Space: `O(n)`

---

## G2. LeetCode #84 — Largest Rectangle in Histogram
**Theme:** nearest smaller boundaries  
**Difficulty:** Hard

### Problem brief
Find the largest rectangle area that can be formed inside a histogram.

### Signal
- Each bar can be the limiting height of a maximal rectangle.
- Need nearest smaller element on both sides.

### Visual cue
```text
height = 5
extend left until smaller
extend right until smaller
area = height * width
```

### Walkthrough
1. Use a monotonic increasing stack of indices.
2. When a lower bar arrives, bars taller than it must finalize their best rectangle.
3. Width is determined by the new stack top and current index.
4. Add a trailing zero height to flush the stack.

### Python solution
```python
class Solution:
    def largestRectangleArea(self, heights: list[int]) -> int:
        stack = []
        best = 0
        heights.append(0)

        for i, h in enumerate(heights):
            while stack and heights[stack[-1]] > h:
                height = heights[stack.pop()]
                left = stack[-1] if stack else -1
                width = i - left - 1
                best = max(best, height * width)
            stack.append(i)

        heights.pop()
        return best
```

### Alternate ways
- Precompute previous smaller and next smaller arrays.
- Stack-on-the-fly is more compact.

### Likely coding friction
- Width calculation is the biggest source of bugs.
- Forgetting the sentinel `0` leaves bars unprocessed.
- Mixing “strictly smaller” and “smaller or equal” changes boundaries.

### Complexity
- Time: `O(n)`
- Space: `O(n)`

---

# Part H — Interval patterns

## H1. LeetCode #56 — Merge Intervals
**Theme:** sort then merge overlaps  
**Difficulty:** Medium

### Problem brief
Combine all overlapping intervals and return the resulting disjoint intervals.

### Signal
- Intervals interact by order of start time.
- Sorting simplifies overlap detection.

### Visual cue
```text
[1,3] [2,6] [8,10] [15,18]
  overlap -> merge to [1,6]
```

### Walkthrough
1. Sort intervals by start.
2. Start the answer with the first interval.
3. If the next interval overlaps, extend the current end.
4. Otherwise start a new merged block.

### Python solution
```python
class Solution:
    def merge(self, intervals: list[list[int]]) -> list[list[int]]:
        intervals.sort(key=lambda x: x[0])
        merged = []
        for start, end in intervals:
            if not merged or merged[-1][1] < start:
                merged.append([start, end])
            else:
                merged[-1][1] = max(merged[-1][1], end)
        return merged
```

### Alternate ways
- Sweeping line events is another perspective.
- For standard merge-overlap problems, sort-and-scan is the right default.

### Likely coding friction
- Using the wrong overlap test.
- Forgetting to sort first.
- Mutating interval references carelessly.

### Complexity
- Time: `O(n log n)`
- Space: `O(n)` for output

---

## H2. LeetCode #57 — Insert Interval
**Theme:** insert and merge in one pass  
**Difficulty:** Medium

### Problem brief
Insert one new interval into a sorted non-overlapping interval list and merge if needed.

### Signal
- Existing intervals are already sorted and disjoint.
- The new interval may overlap a middle block.

### Visual cue
```text
left safe | overlap block | right safe
append left
merge middle
append right
```

### Python solution
```python
class Solution:
    def insert(self, intervals: list[list[int]], newInterval: list[int]) -> list[list[int]]:
        ans = []
        i = 0
        n = len(intervals)

        while i < n and intervals[i][1] < newInterval[0]:
            ans.append(intervals[i])
            i += 1

        while i < n and intervals[i][0] <= newInterval[1]:
            newInterval[0] = min(newInterval[0], intervals[i][0])
            newInterval[1] = max(newInterval[1], intervals[i][1])
            i += 1

        ans.append(newInterval)

        while i < n:
            ans.append(intervals[i])
            i += 1

        return ans
```

### Alternate ways
- Append then call merge-intervals.
- One-pass insertion is more elegant when original list is already sorted.

### Likely coding friction
- Mixing the three phases: before overlap, during overlap, after overlap.
- Mutating `newInterval` incorrectly.

### Complexity
- Time: `O(n)`
- Space: `O(n)` output

---

# Part I — Partition and cyclic-sort style problems

## I1. LeetCode #75 — Sort Colors
**Theme:** Dutch national flag partition  
**Difficulty:** Medium

### Problem brief
Sort an array containing only `0`, `1`, and `2` in one pass and constant extra space.

### Signal
- Only three categories exist.
- Multi-region partitioning beats general sorting.

### Visual cue
```text
[0-zone | 1-zone | unknown | 2-zone]
 low      mid              high
```

### Walkthrough
1. `low` marks where next `0` should go.
2. `high` marks where next `2` should go.
3. `mid` scans the unknown zone.
4. Swap into proper region based on the current value.

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

### Alternate ways
- Counting sort in two passes.
- General sorting in `O(n log n)`.
- Partitioning is the intended pattern solution.

### Likely coding friction
- Incrementing `mid` after swapping with `high` is wrong because the new value at `mid` is unprocessed.

### Complexity
- Time: `O(n)`
- Space: `O(1)`

---

## I2. LeetCode #41 — First Missing Positive
**Theme:** index placement / cyclic-sort spirit  
**Difficulty:** Hard

### Problem brief
Find the smallest missing positive integer in an unsorted array using linear time and constant extra space.

### Signal
- Values from `1` to `n` have natural home positions.
- Place each value `x` at index `x - 1` when possible.

### Visual cue
```text
value 3 belongs at index 2
swap until every place holds either the right number or junk
then scan for first mismatch
```

### Walkthrough
1. While a value is in range and not in its correct position, swap it into place.
2. Repeat until no more useful swaps exist at the current index.
3. Scan from left to right.
4. First index `i` with value `!= i + 1` gives the answer `i + 1`.

### Python solution
```python
class Solution:
    def firstMissingPositive(self, nums: list[int]) -> int:
        n = len(nums)
        for i in range(n):
            while 1 <= nums[i] <= n and nums[nums[i] - 1] != nums[i]:
                j = nums[i] - 1
                nums[i], nums[j] = nums[j], nums[i]

        for i in range(n):
            if nums[i] != i + 1:
                return i + 1
        return n + 1
```

### Alternate ways
- Hash set in `O(n)` space.
- Sorting in `O(n log n)`.
- Index placement is the special in-place insight.

### Likely coding friction
- Infinite loops from careless swapping.
- Forgetting to guard values outside `[1, n]`.
- This is one of the harder “cyclic sort” family problems to code cleanly.

### Complexity
- Time: `O(n)`
- Space: `O(1)`

---

# Part J — Kadane and subarray optimization

## J1. LeetCode #53 — Maximum Subarray
**Theme:** Kadane's algorithm  
**Difficulty:** Medium

### Problem brief
Find the contiguous subarray with the largest sum.

### Signal
- Need best subarray ending at each position.
- Negative running sums hurt future growth.

### Visual cue
```text
best ending here = max(current value, extend previous run)
```

### Walkthrough
1. Maintain `current`, the best sum ending at the current index.
2. Either start fresh at `nums[i]` or extend the previous run.
3. Track the global best.

### Python solution
```python
class Solution:
    def maxSubArray(self, nums: list[int]) -> int:
        current = best = nums[0]
        for x in nums[1:]:
            current = max(x, current + x)
            best = max(best, current)
        return best
```

### Alternate ways
- Prefix-sum minimum tracking.
- Divide and conquer version also exists.
- Kadane is the cleanest interview answer.

### Likely coding friction
- Initializing from zero breaks all-negative arrays.
- Failing to explain *why* resetting a negative prefix is safe.

### Complexity
- Time: `O(n)`
- Space: `O(1)`

---

## J2. LeetCode #152 — Maximum Product Subarray
**Theme:** Kadane variant with sign flips  
**Difficulty:** Medium

### Problem brief
Find the contiguous subarray with the largest product.

### Signal
- Negative numbers can turn a small negative product into a huge positive one.
- Need both max and min product ending here.

### Visual cue
```text
negative x swaps roles:
max_here <-> min_here
```

### Python solution
```python
class Solution:
    def maxProduct(self, nums: list[int]) -> int:
        max_here = min_here = ans = nums[0]
        for x in nums[1:]:
            if x < 0:
                max_here, min_here = min_here, max_here
            max_here = max(x, max_here * x)
            min_here = min(x, min_here * x)
            ans = max(ans, max_here)
        return ans
```

### Alternate ways
- Brute force works but is slow.
- Prefix/suffix product tricks exist, but max/min DP is more robust.

### Likely coding friction
- Forgetting to swap when the current number is negative.
- Treating product like sum; this problem is trickier than standard Kadane.

### Complexity
- Time: `O(n)`
- Space: `O(1)`

---

# Part K — Fast / slow pointers

## K1. LeetCode #142 — Linked List Cycle II
**Theme:** cycle entry via meeting-point mathematics  
**Difficulty:** Medium

### Problem brief
If a linked list has a cycle, return the node where the cycle begins.

### Signal
- Classic Floyd cycle detection.
- Need the cycle start, not just detection.

### Visual cue
```text
phase 1: slow and fast meet inside cycle
phase 2: move one pointer to head
then both move 1 step; they meet at cycle start
```

### Walkthrough
1. Use slow and fast pointers to detect a meeting point.
2. Once they meet, place one pointer at head.
3. Move both one step at a time.
4. Their next meeting point is the cycle entry.

### Python solution
```python
class Solution:
    def detectCycle(self, head):
        slow = fast = head
        while fast and fast.next:
            slow = slow.next
            fast = fast.next.next
            if slow == fast:
                break
        else:
            return None

        slow = head
        while slow != fast:
            slow = slow.next
            fast = fast.next
        return slow
```

### Alternate ways
- Hash set of visited nodes.
- Floyd is better because it uses constant extra space.

### Likely coding friction
- The math behind phase 2 is often memorized but not understood.
- Good interview move: say “distance from head to cycle start equals distance from meet point to cycle start along the cycle.”

### Complexity
- Time: `O(n)`
- Space: `O(1)`

---

## K2. LeetCode #287 — Find the Duplicate Number
**Theme:** fast/slow pointers on implicit linked structure  
**Difficulty:** Medium

### Problem brief
Array values are in `[1, n]`; one value repeats. Find the duplicate without modifying the array and with constant extra space.

### Signal
- Value-to-index mapping creates an implicit linked list.
- Duplicate creates a cycle.

### Visual cue
```text
i -> nums[i] -> nums[nums[i]] -> ...
duplicate value means two arrows point into same node
```

### Walkthrough
1. Treat each index as a node and `nums[i]` as the next pointer.
2. Run Floyd cycle detection.
3. Meeting point occurs inside the cycle.
4. Reset one pointer to start and move both one step to find duplicate value.

### Python solution
```python
class Solution:
    def findDuplicate(self, nums: list[int]) -> int:
        slow = fast = nums[0]
        while True:
            slow = nums[slow]
            fast = nums[nums[fast]]
            if slow == fast:
                break

        slow = nums[0]
        while slow != fast:
            slow = nums[slow]
            fast = nums[fast]
        return slow
```

### Alternate ways
- Binary search on value range.
- In-place marking or cyclic sort is simpler but may violate constraints.
- Floyd is the elegant constraint-satisfying answer.

### Likely coding friction
- This problem feels magical until you see the implicit linked list model.
- Candidates often start with sorting, then realize the constraints forbid it.

### Complexity
- Time: `O(n)`
- Space: `O(1)`

---

## K3. LeetCode #202 — Happy Number
**Theme:** cycle detection on state transitions  
**Difficulty:** Easy

### Problem brief
Repeatedly replace a number by the sum of squares of its digits; determine whether the process reaches `1`.

### Signal
- Repeated state transition may loop forever.
- Fast/slow or hash-set detection both work.

### Python solution
```python
def next_num(n: int) -> int:
    total = 0
    while n:
        digit = n % 10
        total += digit * digit
        n //= 10
    return total


class Solution:
    def isHappy(self, n: int) -> bool:
        slow = fast = n
        while True:
            slow = next_num(slow)
            fast = next_num(next_num(fast))
            if fast == 1:
                return True
            if slow == fast:
                return False
```

### Alternate ways
- Hash set is more intuitive.
- Floyd is space-optimal and pattern-consistent.

### Likely coding friction
- Forgetting to define the digit-square transition cleanly.

### Complexity
- Time: effectively small and bounded in practice
- Space: `O(1)` with Floyd

---

# Part L — Palindrome patterns

## L1. LeetCode #125 — Valid Palindrome
**Theme:** filtered two-pointer palindrome check  
**Difficulty:** Easy

### Problem brief
Ignore non-alphanumeric characters and case, then decide whether the cleaned string is a palindrome.

### Signal
- Compare mirrored positions.
- Skip irrelevant characters without building a new string if desired.

### Visual cue
```text
"A man, a plan, a canal: Panama"
^                             ^
skip punctuation, compare normalized letters
```

### Python solution
```python
class Solution:
    def isPalindrome(self, s: str) -> bool:
        left, right = 0, len(s) - 1
        while left < right:
            while left < right and not s[left].isalnum():
                left += 1
            while left < right and not s[right].isalnum():
                right -= 1
            if s[left].lower() != s[right].lower():
                return False
            left += 1
            right -= 1
        return True
```

### Alternate ways
- Build filtered lowercase string then compare to reverse.
- Two pointers save extra space.

### Likely coding friction
- Forgetting to normalize case.
- Mishandling the skip loops at the ends.

### Complexity
- Time: `O(n)`
- Space: `O(1)`

---

## L2. LeetCode #5 — Longest Palindromic Substring
**Theme:** expand around centers  
**Difficulty:** Medium

### Problem brief
Return the longest contiguous substring that reads the same forward and backward.

### Signal
- Every palindrome has a center.
- Need to check both odd-length and even-length centers.

### Visual cue
```text
babad
 center at 'a' -> bab
 center between 'b' and 'a' -> no even palindrome there
```

### Walkthrough
1. For each index, expand around one-char center and two-char center.
2. While characters match, grow outward.
3. Track the best substring boundaries.

### Python solution
```python
class Solution:
    def longestPalindrome(self, s: str) -> str:
        def expand(l: int, r: int) -> tuple[int, int]:
            while l >= 0 and r < len(s) and s[l] == s[r]:
                l -= 1
                r += 1
            return l + 1, r - 1

        best_l = best_r = 0
        for i in range(len(s)):
            l1, r1 = expand(i, i)
            l2, r2 = expand(i, i + 1)
            if r1 - l1 > best_r - best_l:
                best_l, best_r = l1, r1
            if r2 - l2 > best_r - best_l:
                best_l, best_r = l2, r2
        return s[best_l:best_r + 1]
```

### Alternate ways
- Dynamic programming works.
- Manacher's algorithm is linear but much harder.
- Expand-around-center is usually the best interview balance.

### Likely coding friction
- Forgetting even-length centers.
- Returning indices off by one after expansion exits.

### Complexity
- Time: `O(n^2)`
- Space: `O(1)`

---

# Part M — String sliding window patterns

## M1. LeetCode #567 — Permutation in String
**Theme:** frequency-controlled fixed window on strings  
**Difficulty:** Medium

### Problem brief
Return whether some substring of `s2` is a permutation of `s1`.

### Signal
- Window size must equal `len(s1)`.
- Only counts matter.

### Visual cue
```text
s1 = ab
s2 = eidbaooo
window size 2 -> [db] [ba] -> match on [ba]
```

### Python solution
```python
class Solution:
    def checkInclusion(self, s1: str, s2: str) -> bool:
        if len(s1) > len(s2):
            return False

        need = [0] * 26
        window = [0] * 26
        for ch in s1:
            need[ord(ch) - ord('a')] += 1
        for ch in s2[:len(s1)]:
            window[ord(ch) - ord('a')] += 1

        if window == need:
            return True

        k = len(s1)
        for i in range(k, len(s2)):
            window[ord(s2[i]) - ord('a')] += 1
            window[ord(s2[i - k]) - ord('a')] -= 1
            if window == need:
                return True
        return False
```

### Alternate ways
- Counter maps work too.
- Array counts are cleaner when alphabet is fixed.

### Likely coding friction
- Off-by-one at window boundaries.
- Confusing “contains permutation” with “contains subsequence.”

### Complexity
- Time: `O(n)` with fixed alphabet
- Space: `O(1)`

---

## M2. LeetCode #424 — Longest Repeating Character Replacement
**Theme:** variable window with replacement budget  
**Difficulty:** Medium

### Problem brief
Find the longest substring that can be turned into all one repeated letter using at most `k` replacements.

### Signal
- Window is valid when `window_size - max_frequency <= k`.
- That expression measures how many letters must be changed.

### Visual cue
```text
AABABBA, k=1
window valid if all but the most frequent char can be replaced
```

### Python solution
```python
from collections import defaultdict

class Solution:
    def characterReplacement(self, s: str, k: int) -> int:
        count = defaultdict(int)
        left = 0
        max_freq = 0
        best = 0

        for right, ch in enumerate(s):
            count[ch] += 1
            max_freq = max(max_freq, count[ch])

            while (right - left + 1) - max_freq > k:
                count[s[left]] -= 1
                left += 1

            best = max(best, right - left + 1)

        return best
```

### Alternate ways
- Recompute max frequency inside the shrink loop, but that is slower.
- The “stale max frequency” optimization is standard and safe.

### Likely coding friction
- Not understanding why `max_freq` may stay stale and still work.
- Shrinking too aggressively.

### Complexity
- Time: `O(n)`
- Space: `O(alphabet)`

---

# Part N — Parentheses and bracket matching

## N1. LeetCode #1249 — Minimum Remove to Make Valid Parentheses
**Theme:** index tracking with stack  
**Difficulty:** Medium

### Problem brief
Remove the minimum number of parentheses so that the remaining string is valid.

### Signal
- Need to identify unmatched positions.
- Stack naturally tracks unmatched opening brackets.

### Visual cue
```text
a)b(c)d
unmatched ')' removed immediately
unmatched '(' removed after scan
```

### Python solution
```python
class Solution:
    def minRemoveToMakeValid(self, s: str) -> str:
        stack = []
        remove = set()

        for i, ch in enumerate(s):
            if ch == '(':
                stack.append(i)
            elif ch == ')':
                if stack:
                    stack.pop()
                else:
                    remove.add(i)

        remove.update(stack)
        return ''.join(ch for i, ch in enumerate(s) if i not in remove)
```

### Alternate ways
- Two-pass counter-based filtering also works.
- Stack is more direct when indices matter.

### Likely coding friction
- Removing characters while iterating.
- Forgetting leftover unmatched `'('` after the scan ends.

### Complexity
- Time: `O(n)`
- Space: `O(n)`

---

## N2. LeetCode #32 — Longest Valid Parentheses
**Theme:** longest valid bracket segment  
**Difficulty:** Hard

### Problem brief
Find the length of the longest contiguous valid parentheses substring.

### Signal
- Need lengths, not just validity.
- Stack of indices is more useful than stack of characters.

### Visual cue
```text
stack starts with sentinel -1
when a valid pair closes, length = i - stack[-1]
```

### Walkthrough
1. Push `-1` as a sentinel base.
2. For `'('`, push its index.
3. For `')'`, pop once.
4. If stack becomes empty, push current index as new invalid base; otherwise update best length.

### Python solution
```python
class Solution:
    def longestValidParentheses(self, s: str) -> int:
        stack = [-1]
        best = 0

        for i, ch in enumerate(s):
            if ch == '(':
                stack.append(i)
            else:
                stack.pop()
                if not stack:
                    stack.append(i)
                else:
                    best = max(best, i - stack[-1])
        return best
```

### Alternate ways
- DP solution exists.
- Two-pass left/right counter method also exists.
- Stack-with-sentinel is the most interview-friendly explanation.

### Likely coding friction
- Missing the sentinel `-1`.
- Using character stack instead of index stack.
- Forgetting to reset the base after an invalid closing bracket.

### Complexity
- Time: `O(n)`
- Space: `O(n)`

---

# Part O — String transformations and building

## O1. LeetCode #151 — Reverse Words in a String
**Theme:** tokenize, normalize spaces, rebuild  
**Difficulty:** Medium

### Problem brief
Reverse the order of words in a string while trimming and normalizing spaces.

### Signal
- Words, not characters, are the unit.
- Multiple spaces should collapse.

### Visual cue
```text
"  the sky   is blue  "
-> [the, sky, is, blue]
-> reverse words
-> "blue is sky the"
```

### Python solution
```python
class Solution:
    def reverseWords(self, s: str) -> str:
        return ' '.join(reversed(s.split()))
```

### Alternate ways
- Manual parse from right to left without extra token list.
- In Python, `split()` + `reversed()` is clean and accepted.

### Likely coding friction
- Forgetting to normalize multiple spaces.
- Reversing characters instead of words.

### Complexity
- Time: `O(n)`
- Space: `O(n)`

---

## O2. LeetCode #443 — String Compression
**Theme:** in-place write pointer string building  
**Difficulty:** Medium

### Problem brief
Compress repeated characters in-place using counts after the character.

### Signal
- Need to read runs and write compressed output.
- Two-pointer read/write structure fits naturally.

### Visual cue
```text
[a,a,a,b,b,c]
read run aaa -> write a3
read run bb  -> write b2
read run c   -> write c
```

### Python solution
```python
class Solution:
    def compress(self, chars: list[str]) -> int:
        write = 0
        read = 0

        while read < len(chars):
            start = read
            while read < len(chars) and chars[read] == chars[start]:
                read += 1

            chars[write] = chars[start]
            write += 1
            count = read - start
            if count > 1:
                for digit in str(count):
                    chars[write] = digit
                    write += 1

        return write
```

### Alternate ways
- Build a new output string/list, then copy back.
- In-place read/write is the pattern the problem is testing.

### Likely coding friction
- Writing multi-digit counts incorrectly.
- Forgetting to return the new length, not the array itself.

### Complexity
- Time: `O(n)`
- Space: `O(1)` extra

---

# Part P — String matching and rolling-hash style thinking

## P1. LeetCode #28 — Find the Index of the First Occurrence in a String
**Theme:** substring matching baseline  
**Difficulty:** Easy

### Problem brief
Return the first index where `needle` appears inside `haystack`, or `-1` if absent.

### Signal
- Baseline string search problem.
- Great place to discuss naive search, KMP, and rolling hash.

### Visual cue
```text
haystack: mississippi
needle:   issip
shift the pattern until full match occurs
```

### Python solution
```python
class Solution:
    def strStr(self, haystack: str, needle: str) -> int:
        if needle == '':
            return 0
        m, n = len(haystack), len(needle)
        for i in range(m - n + 1):
            if haystack[i:i + n] == needle:
                return i
        return -1
```

### Alternate ways
- KMP gives linear worst-case time.
- Rabin-Karp is useful when multiple comparisons or many patterns are involved.
- For interviews, start with naive, then mention KMP or rolling hash as the optimization path.

### Likely coding friction
- Forgetting the empty-needle case.
- Overengineering too early instead of discussing optimization progressively.

### Complexity
- Time: `O((m-n+1) * n)` in the naive worst case
- Space: `O(1)`

---

## P2. LeetCode #686 — Repeated String Match
**Theme:** string repetition + match check  
**Difficulty:** Medium

### Problem brief
Find the minimum number of times `a` must be repeated so that `b` becomes a substring.

### Signal
- Repetition count must be near `len(b)/len(a)`.
- Matching is still the central issue.

### Python solution
```python
class Solution:
    def repeatedStringMatch(self, a: str, b: str) -> int:
        repeated = a
        count = 1
        while len(repeated) < len(b):
            repeated += a
            count += 1
        if b in repeated:
            return count
        if b in repeated + a:
            return count + 1
        return -1
```

### Alternate ways
- KMP or Rabin-Karp can speed the substring check conceptually.
- For interview coding, bounded repetition plus substring check is fine.

### Likely coding friction
- Not checking one extra repetition.
- Appending too many times without reasoning about the bound.

### Complexity
- Time: depends on repeated substring search cost
- Space: `O(len(b))` or more for the built string

---

# Supplementary anchors by topic

## Topic-to-problem supplement list

| Topic | Core detailed anchors | Good extra practice |
|---|---|---|
| Two pointers | #167, #11, #15 | #125 Valid Palindrome, #42 Trapping Rain Water |
| Fixed sliding window | #643, #438 | #1456 Maximum Number of Vowels in a Substring of Given Length |
| Variable sliding window | #3, #904, #76 | #209 Minimum Size Subarray Sum |
| Divide and conquer | #169, #241 | #53 Maximum Subarray D&C view |
| Binary search as pattern | #875, #1011, #410 | #1552 Magnetic Force Between Two Balls |
| Hash patterns | #560, #128 | #1 Two Sum, #49 Group Anagrams |
| Monotonic stack | #739, #84 | #496 Next Greater Element I |
| Intervals | #56, #57 | #435 Non-overlapping Intervals |
| Partition / cyclic sort | #75, #41 | #442 Find All Duplicates in an Array |
| Kadane | #53, #152 | #918 Maximum Sum Circular Subarray |
| Fast / slow | #142, #287, #202 | #141 Linked List Cycle |
| Palindrome | #125, #5 | #647 Palindromic Substrings, #680 Valid Palindrome II |
| String windows | #567, #424 | #438 Find All Anagrams in a String |
| Parentheses | #1249, #32 | #20 Valid Parentheses, #22 Generate Parentheses |
| String building | #151, #443 | #415 Add Strings |
| String matching | #28, #686 | #187 Repeated DNA Sequences |

---

# Phase 2 practice table
Use this as the main deliberate-practice sheet before Phase 3.

| # | Problem | Theme | Category | Difficulty | Tags / keywords | Priority |
|---|---|---|---|---|---|---|
| 167 | Two Sum II | Opposite-direction pointers | Two pointers | Medium | sorted-array, pair-sum, left-right | Must |
| 11 | Container With Most Water | Shrink from shorter side | Two pointers | Medium | greedy, area, left-right | Must |
| 15 | 3Sum | Anchor + two pointers | Two pointers | Medium | sorting, duplicates, triplets | Must |
| 643 | Maximum Average Subarray I | Rolling fixed window sum | Fixed sliding window | Easy | window-sum, fixed-k | Should |
| 438 | Find All Anagrams in a String | Fixed-size frequency match | Fixed sliding window | Medium | counts, anagram, window | Must |
| 3 | Longest Substring Without Repeating Characters | Unique-character window | Variable sliding window | Medium | last-seen, hashmap, substring | Must |
| 904 | Fruit Into Baskets | At-most-2 distinct | Variable sliding window | Medium | frequency-map, shrink-expand | Should |
| 76 | Minimum Window Substring | Smallest valid window | Variable sliding window | Hard | need/have, frequency, shrink-while-valid | Must |
| 169 | Majority Element | Majority by recursion/voting | Divide and conquer | Easy | cancellation, candidate, recursion | Should |
| 241 | Different Ways to Add Parentheses | Split-and-combine expression | Divide and conquer | Medium | recursion, memoization, parsing | Should |
| 875 | Koko Eating Bananas | First feasible speed | Binary search pattern | Medium | answer-space, monotone-check | Must |
| 1011 | Capacity To Ship Packages Within D Days | Min feasible capacity | Binary search pattern | Medium | greedy-check, boundary-search | Must |
| 410 | Split Array Largest Sum | Minimize maximum segment sum | Binary search pattern | Hard | partition, monotone, answer-space | Should+ |
| 560 | Subarray Sum Equals K | Prefix sum counting | Hash map / set | Medium | prefix-sum, hashmap, counting | Must |
| 128 | Longest Consecutive Sequence | Sequence starts in set | Hash map / set | Medium | hash-set, consecutive, start-detection | Must |
| 739 | Daily Temperatures | Next greater to right | Monotonic stack | Medium | decreasing-stack, indices | Must |
| 84 | Largest Rectangle in Histogram | Previous/next smaller | Monotonic stack | Hard | stack, width, boundaries | Must |
| 56 | Merge Intervals | Sort then merge | Intervals | Medium | overlap, sorting, sweep | Must |
| 57 | Insert Interval | Insert and merge | Intervals | Medium | sorted-intervals, merge-block | Should |
| 75 | Sort Colors | Dutch national flag | Partition | Medium | three-way-partition, in-place | Must |
| 41 | First Missing Positive | Index placement | Cyclic sort style | Hard | placement, in-place, index-as-bucket | Must |
| 53 | Maximum Subarray | Best-ending-here | Kadane | Medium | dp, running-sum, subarray | Must |
| 152 | Maximum Product Subarray | Track max and min product | Kadane variant | Medium | sign-flip, product-dp | Should |
| 142 | Linked List Cycle II | Detect cycle start | Fast/slow | Medium | Floyd, cycle-entry | Must |
| 287 | Find the Duplicate Number | Implicit cycle in array | Fast/slow | Medium | value-as-next, Floyd | Must |
| 202 | Happy Number | State-cycle detection | Fast/slow | Easy | digit-transform, cycle | Supplementary |
| 125 | Valid Palindrome | Compare after cleanup | Palindrome | Easy | two-pointers, filtering | Should |
| 5 | Longest Palindromic Substring | Expand around center | Palindrome | Medium | center-expansion, odd-even | Must |
| 567 | Permutation in String | Permutation window | String windows | Medium | fixed-window, frequency-array | Must |
| 424 | Longest Repeating Character Replacement | Window with replacement budget | String windows | Medium | max-frequency, at-most-k | Should |
| 1249 | Minimum Remove to Make Valid Parentheses | Remove unmatched brackets | Parentheses | Medium | stack, indices, cleanup | Must |
| 32 | Longest Valid Parentheses | Longest valid bracket span | Parentheses | Hard | sentinel, index-stack | Should+ |
| 151 | Reverse Words in a String | Normalize and rebuild | String transformation | Medium | split, reverse, join | Should |
| 443 | String Compression | Read/write run compression | String transformation | Medium | run-length, two-pointers | Must |
| 28 | Find the Index of the First Occurrence in a String | Baseline substring search | String matching | Easy | naive-match, KMP, Rabin-Karp | Must |
| 686 | Repeated String Match | Repetition + match | String matching | Medium | substring, repetition bound | Supplementary |

---

# Suggested execution blocks

## Block 1 — Week 4 foundations
- #167 Two Sum II
- #11 Container With Most Water
- #15 3Sum
- #643 Maximum Average Subarray I
- #438 Find All Anagrams in a String

## Block 2 — Week 4 deeper patterns
- #3 Longest Substring Without Repeating Characters
- #904 Fruit Into Baskets
- #76 Minimum Window Substring
- #169 Majority Element
- #241 Different Ways to Add Parentheses

## Block 3 — Week 4 binary-search pattern
- #875 Koko Eating Bananas
- #1011 Capacity To Ship Packages Within D Days
- #410 Split Array Largest Sum

## Block 4 — Week 5 pattern expansion
- #560 Subarray Sum Equals K
- #128 Longest Consecutive Sequence
- #739 Daily Temperatures
- #84 Largest Rectangle in Histogram
- #56 Merge Intervals
- #57 Insert Interval

## Block 5 — Week 5 structure-heavy drills
- #75 Sort Colors
- #41 First Missing Positive
- #53 Maximum Subarray
- #152 Maximum Product Subarray
- #142 Linked List Cycle II
- #287 Find the Duplicate Number
- #202 Happy Number

## Block 6 — Week 6 strings and parsing
- #125 Valid Palindrome
- #5 Longest Palindromic Substring
- #567 Permutation in String
- #424 Longest Repeating Character Replacement
- #1249 Minimum Remove to Make Valid Parentheses
- #32 Longest Valid Parentheses
- #151 Reverse Words in a String
- #443 String Compression
- #28 Find the Index of the First Occurrence in a String
- #686 Repeated String Match

---

# Common failure patterns in Phase 2
- Solving by memory instead of by **signal recognition**.
- Mixing fixed-size and variable-size sliding window logic.
- Using hash maps where sorted structure already gives a simpler two-pointer solution.
- Forgetting to define the binary-search predicate before coding.
- Using monotonic stack without first saying what monotonic order is being maintained.
- Writing palindrome solutions that only handle odd-length centers.
- Treating parentheses problems as all the same, when some ask for validity, some for removal, and some for longest span.

---

# Phase 2 readiness checklist
You are ready for Phase 3 only if you can do the following reliably:
- Recognize whether a problem wants two pointers, sliding window, hash map, monotonic stack, or binary search on answer.
- State the **window invariant** before coding any sliding-window problem.
- Explain the feasibility function in answer-space binary search.
- Derive nearest greater/smaller logic with a monotonic stack.
- Merge intervals without hesitation after sorting.
- Explain why Kadane works instead of just memorizing the formula.
- Distinguish palindrome checking, palindrome generation, and longest-palindrome search.
- Discuss at least one alternate approach and one coding pitfall for every core pattern.

---

# Next phase preview
Phase 3 moves into the classic interview core: **trees, BSTs, graph traversal, shortest paths, MST/DSU, and dynamic programming**.

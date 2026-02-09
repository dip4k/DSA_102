# DSA Complete Detailed Syllabus

## Table of Contents
1. [Phase 0 - Foundations & Complexity](#phase-0--foundations--complexity)
2. [Phase 1 - Arrays, Strings, and Hashing](#phase-1--arrays-strings-and-hashing)
3. [Phase 2 - Linked Lists, Stacks, and Queues](#phase-2--linked-lists-stacks-and-queues)
4. [Phase 3 - Trees, BSTs, and Balanced Trees](#phase-3--trees-bsts-and-balanced-trees)
5. [Phase 4 - Graphs and Graph Traversal](#phase-4--graphs-and-graph-traversal)
6. [Phase 5 - Sorting, Searching, and Divide & Conquer](#phase-5--sorting-searching-and-divide--conquer)
7. [Phase 6 - Greedy Algorithms](#phase-6--greedy-algorithms)
8. [Phase 7 - Dynamic Programming](#phase-7--dynamic-programming)
9. [Phase 8 - Backtracking, Recursion, and Combinatorial Search](#phase-8--backtracking-recursion-and-combinatorial-search)
10. [Phase 9 - Heaps, Priority Queues, and Advanced Data Structures](#phase-9--heaps-priority-queues-and-advanced-data-structures)
11. [Phase 10 - Graph Shortest Paths, MST, and Flow](#phase-10--graph-shortest-paths-mst-and-flow)
12. [Phase 11 - Interview-Oriented Mixed Practice](#phase-11--interview-oriented-mixed-practice)

---

## Phase 0 – Foundations & Complexity

**Duration:** Week 1 (7 days)  
**Total Problems:** 7 (All Easy)  
**Time Commitment:** 6-8 hours

### Learning Objectives
- Understand time and space complexity (Big-O notation)
- Master basic binary search template
- Learn fundamental dynamic programming concepts
- Build confidence with LeetCode platform
- Establish problem-solving workflow

### Core Concepts

#### 1. Complexity Analysis
- **Big-O Notation**
  - O(1) - Constant time
  - O(log n) - Logarithmic time
  - O(n) - Linear time
  - O(n log n) - Linearithmic time
  - O(n²) - Quadratic time
  - O(2ⁿ) - Exponential time
- **Space Complexity**
  - In-place algorithms
  - Auxiliary space
  - Recursion stack space
- **Best, Average, Worst Cases**

#### 2. Binary Search Fundamentals
- **Search Space Halving**
  - Left and right pointers
  - Mid calculation: `mid = left + (right - left) // 2`
  - Loop invariants
- **Iterative vs Recursive**
  - Iterative template (preferred for interviews)
  - Recursive base cases
- **Boundary Conditions**
  - When to use `left <= right` vs `left < right`
  - Handling edge cases (empty array, single element)

#### 3. Basic Dynamic Programming
- **State Definition**
  - What does dp[i] represent?
  - Base cases
- **Recurrence Relations**
  - Finding the pattern
  - Fibonacci sequence as DP
- **Memoization vs Tabulation**
  - Top-down (memoization)
  - Bottom-up (tabulation)

#### 4. Bit Manipulation Basics
- **Binary Representation**
  - Understanding bits
  - Powers of 2
- **Common Bit Operations**
  - AND (&), OR (|), XOR (^)
  - Left shift (<<), Right shift (>>)
  - `n & (n-1)` removes rightmost set bit

#### 5. Prefix Sum Technique
- **Running Sum**
  - Cumulative sum array
  - O(1) range sum queries
- **Balance Point Pattern**
  - Left sum vs right sum
  - Mathematical relationship: `leftSum = totalSum - rightSum - current`

### Problem Breakdown

#### Problem 1: Binary Search (LC 704) 🔑 ANCHOR
**Pattern:** Binary Search  
**Difficulty:** Easy  
**Key Concepts:**
- Search space halving
- Loop invariants
- Iterative and recursive approaches

**What to Learn:**
- Template for binary search
- When to update left/right pointers
- How to avoid infinite loops
- Integer overflow prevention: `mid = left + (right - left) // 2`

**Common Mistakes:**
- Using `mid = (left + right) // 2` (overflow risk)
- Wrong loop condition
- Off-by-one errors

---

#### Problem 2: First Bad Version (LC 278) 🎯 PRACTICE
**Pattern:** Binary Search (Minimization)  
**Difficulty:** Easy  
**Key Concepts:**
- API constraints
- Finding first occurrence
- Minimization variant

**What to Learn:**
- Binary search for "first" element satisfying condition
- When to move left pointer: `left = mid + 1`
- When to move right pointer: `right = mid`

**Connection:** Apply binary search template from Problem 1

---

#### Problem 3: Climbing Stairs (LC 70) 🔑 ANCHOR
**Pattern:** 1D Dynamic Programming  
**Difficulty:** Easy  
**Key Concepts:**
- Fibonacci sequence
- State definition
- Recurrence relation

**What to Learn:**
- How to define DP state: `dp[i] = number of ways to reach step i`
- Derive recurrence: `dp[i] = dp[i-1] + dp[i-2]`
- Base cases: `dp[0] = 1, dp[1] = 1`
- Space optimization: only need last two values

**Mathematical Insight:**
- Problem reduces to Fibonacci numbers
- Can solve in O(n) time, O(1) space

---

#### Problem 4: Power of Two (LC 231) 🎯 PRACTICE
**Pattern:** Bit Manipulation  
**Difficulty:** Easy  
**Key Concepts:**
- Binary representation
- Bit tricks
- Powers of 2 properties

**What to Learn:**
- Power of 2 has exactly one set bit: `10000...`
- `n & (n-1)` removes rightmost set bit
- Power of 2: `n > 0 and (n & (n-1)) == 0`

**Alternative Approaches:**
- Loop division by 2
- Count set bits
- Mathematical: `n > 0 and 2^(log2(n)) == n`

---

#### Problem 5: Power of Three (LC 326) 🎯 PRACTICE
**Pattern:** Math  
**Difficulty:** Easy  
**Key Concepts:**
- Logarithm properties
- Mathematical verification
- Edge cases

**What to Learn:**
- `log₃(n)` should be integer if n is power of 3
- Handle floating-point precision
- Alternative: max power of 3 in 32-bit int divisible by n

**Mathematical Formula:**
- `log₃(n) = log(n) / log(3)`
- Check if result is integer

---

#### Problem 6: Counting Bits (LC 338) ⭐ HIGH VALUE
**Pattern:** Dynamic Programming + Bit Manipulation  
**Difficulty:** Easy  
**Key Concepts:**
- Hamming weight (number of 1s in binary)
- DP optimization using bit patterns
- Right shift operation

**What to Learn:**
- **Key Insight:** `count[i] = count[i >> 1] + (i & 1)`
- `i >> 1` is i divided by 2
- `i & 1` checks if last bit is 1
- Build solution using previously computed results

**Example:**
- `5 = 101₂`: count[5] = count[2] + 1 = 1 + 1 = 2
- `6 = 110₂`: count[6] = count[3] + 0 = 2 + 0 = 2

---

#### Problem 7: Find Pivot Index (LC 724) 🔑 ANCHOR
**Pattern:** Prefix Sum  
**Difficulty:** Easy  
**Key Concepts:**
- Running sum
- Balance point
- Mathematical relationship

**What to Learn:**
- Calculate total sum first
- Track left sum as you iterate
- Right sum = total - left - current
- Pivot: `leftSum == rightSum`

**Formula:**
```
leftSum = sum of elements before index
rightSum = totalSum - leftSum - nums[i]
if leftSum == rightSum: i is pivot
```

### Week 1 Study Plan

**Monday (2 hours)**
- Theory: Big-O notation (30 min)
- Problem 1: Binary Search (704) - 1 hour
- Problem 2: First Bad Version (278) - 30 min

**Tuesday (1.5 hours)**
- Theory: DP basics (30 min)
- Problem 3: Climbing Stairs (70) - 1 hour

**Wednesday (1.5 hours)**
- Theory: Bit manipulation (30 min)
- Problem 4: Power of Two (231) - 30 min
- Problem 5: Power of Three (326) - 30 min

**Thursday (1.5 hours)**
- Theory: Bit manipulation + DP (20 min)
- Problem 6: Counting Bits (338) - 70 min

**Friday (1 hour)**
- Theory: Prefix sum (20 min)
- Problem 7: Find Pivot Index (724) - 40 min

**Saturday (1.5 hours)**
- Review all 7 problems
- Practice explaining each pattern
- Create pattern notes

**Sunday (Rest or light review)**
- Optional: Re-solve Problems 1, 3, 7 (anchors)

### Success Metrics
- ✅ Solve all 7 problems
- ✅ Can explain binary search template from memory
- ✅ Understand basic DP state definition (dp[i] meaning)
- ✅ Comfortable with LeetCode submission process
- ✅ Can analyze time/space complexity of solutions

### Key Takeaways
1. **Binary Search**: Master the template - will be used in 20+ problems
2. **DP Foundation**: State definition is critical - spend time understanding
3. **Prefix Sum**: Simple but powerful - used in arrays and matrices
4. **Bit Manipulation**: Learn the tricks - makes many problems trivial

---

## Phase 1 – Arrays, Strings, and Hashing

**Duration:** Weeks 2-3 (14 days)  
**Total Problems:** 19 (13 Easy, 6 Medium)  
**Time Commitment:** 16-20 hours

### Learning Objectives
- Master two-pointer technique (same direction and opposite direction)
- Understand sliding window (fixed and variable size)
- Use hash maps/sets for O(1) lookups
- Learn in-place array manipulation
- Handle string manipulation problems

### Core Concepts

#### 1. Hash Map Pattern
- **Key-Value Storage**
  - O(1) average lookup, insert, delete
  - When to use: complement search, frequency counting, grouping
- **Common Use Cases**
  - Finding pairs/complements: Two Sum pattern
  - Frequency counting: anagram, character count
  - Grouping: group anagrams
- **Implementation Details**
  - Dictionary in Python
  - HashMap in Java
  - Collision handling (chaining, open addressing)

#### 2. Hash Set Pattern
- **Uniqueness Checking**
  - O(1) lookup for existence
  - When to use: duplicates, seen tracking
- **Common Use Cases**
  - Checking duplicates
  - Tracking visited elements
  - Sliding window with unique characters

#### 3. Two Pointers - Same Direction
- **Pattern Structure**
  ```python
  slow = 0
  for fast in range(len(arr)):
      # Process arr[fast]
      # Conditionally advance slow
      # Possible swap/assignment
  ```
- **When to Use**
  - In-place modifications
  - Removing duplicates
  - Partitioning arrays
- **Key Insight:** Slow pointer marks position for next valid element

#### 4. Two Pointers - Opposite Direction
- **Pattern Structure**
  ```python
  left, right = 0, len(arr) - 1
  while left < right:
      # Check condition
      # Move left or right based on condition
  ```
- **When to Use**
  - Palindrome checking
  - Container problems
  - Pair finding in sorted arrays
- **Key Insight:** Use sorted property or symmetry

#### 5. Sliding Window - Variable Size
- **Pattern Structure**
  ```python
  left = 0
  for right in range(len(arr)):
      # Expand: add arr[right] to window
      while window_invalid:
          # Contract: remove arr[left] from window
          left += 1
      # Update result with valid window
  ```
- **When to Use**
  - Longest/shortest subarray with condition
  - Substring problems
  - Variable size optimization
- **Key Insight:** Expand right, contract left when condition violated

#### 6. Sliding Window - Fixed Size
- **Pattern Structure**
  ```python
  for i in range(len(arr) - k + 1):
      window = arr[i:i+k]
      # Process fixed size window
  ```
- **When to Use**
  - Maximum/minimum in window of size k
  - Average of subarrays of size k

### Part A: Hash Maps & Basic Two-Pointers (Week 2)

#### Problem 1: Two Sum (LC 1) 🔑 ANCHOR
**Pattern:** Hash Map  
**Difficulty:** Easy  
**Key Concepts:**
- Complement search
- O(1) lookup
- Value to index mapping

**What to Learn:**
- **Core Pattern:** Store `value → index` in hash map
- Check if `target - current_value` exists in map
- Handle duplicates: check index before returning
- One-pass solution: check and add in same iteration

**Template:**
```python
def twoSum(nums, target):
    seen = {}
    for i, num in enumerate(nums):
        complement = target - num
        if complement in seen:
            return [seen[complement], i]
        seen[num] = i
```

**Time:** O(n), **Space:** O(n)

---

#### Problem 2: Contains Duplicate (LC 217) 🎯 PRACTICE
**Pattern:** Hash Set  
**Difficulty:** Easy  
**Key Concepts:**
- Uniqueness checking
- Early termination

**What to Learn:**
- Hash set for existence checks
- Add to set, check if already exists
- Return immediately when duplicate found

**Alternative Approaches:**
- Sorting: O(n log n) time, O(1) space
- Set length comparison: `len(set(nums)) != len(nums)`

---

#### Problem 3: Valid Anagram (LC 242) ⭐ HIGH VALUE
**Pattern:** Hash Map / Sorting  
**Difficulty:** Easy  
**Key Concepts:**
- Character frequency
- Two approaches comparison

**What to Learn:**
- **Approach 1:** Frequency map
  - Count characters in both strings
  - Compare frequency maps
  - Time: O(n), Space: O(1) [26 letters max]
- **Approach 2:** Sorting
  - Sort both strings
  - Compare sorted strings
  - Time: O(n log n), Space: O(1)

**Follow-up:** Unicode characters? Use hash map (approach 1)

---

#### Problem 4: Group Anagrams (LC 49) 🔑 ANCHOR
**Pattern:** Hash Map (Advanced)  
**Difficulty:** Medium  
**Key Concepts:**
- String as key
- Grouping by property

**What to Learn:**
- **Key Insight:** Anagrams have same sorted string
- Use sorted string as dictionary key
- Value: list of anagrams
- Build groups in one pass

**Template:**
```python
def groupAnagrams(strs):
    groups = {}
    for s in strs:
        key = ''.join(sorted(s))
        groups.setdefault(key, []).append(s)
    return list(groups.values())
```

**Alternative Key:** Character frequency tuple (faster than sorting)

---

#### Problem 5: Valid Palindrome (LC 125) 🔑 ANCHOR
**Pattern:** Two Pointers (Opposite Direction)  
**Difficulty:** Easy  
**Key Concepts:**
- Character filtering
- Case-insensitive comparison
- Pointer movement

**What to Learn:**
- **Template:** Left and right pointers moving toward center
- Skip non-alphanumeric characters
- Compare after converting to lowercase
- Stop when pointers meet or cross

**Template:**
```python
def isPalindrome(s):
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

---

#### Problem 6: Merge Sorted Array (LC 88) ⭐ HIGH VALUE
**Pattern:** Two Pointers (Same Direction)  
**Difficulty:** Easy  
**Key Concepts:**
- In-place merge
- Backward iteration
- Three pointers

**What to Learn:**
- **Key Insight:** Fill from back to avoid overwriting
- Three pointers: p1 (end of nums1), p2 (end of nums2), p (write position)
- Compare and place larger element at p
- Handle remaining elements from nums2

**Why Backward?** Forward would require shifting elements

---

#### Problem 7: Move Zeroes (LC 283) 🎯 PRACTICE
**Pattern:** Two Pointers (Same Direction)  
**Difficulty:** Easy  
**Key Concepts:**
- In-place swap
- Order preservation
- Partition pattern

**What to Learn:**
- **Slow pointer:** Position for next non-zero
- **Fast pointer:** Current element being examined
- Swap when fast finds non-zero
- All zeros end up at the end

**Template:**
```python
def moveZeroes(nums):
    slow = 0
    for fast in range(len(nums)):
        if nums[fast] != 0:
            nums[slow], nums[fast] = nums[fast], nums[slow]
            slow += 1
```

---

#### Problem 8: Best Time to Buy and Sell Stock (LC 121) 🔑 ANCHOR
**Pattern:** Greedy / Dynamic Programming  
**Difficulty:** Easy  
**Key Concepts:**
- Min tracking
- Max profit calculation
- One-pass solution

**What to Learn:**
- Track minimum price seen so far
- Calculate profit if selling today
- Update maximum profit
- **Key Insight:** Buy at lowest, sell at highest after that point

**Template:**
```python
def maxProfit(prices):
    min_price = float('inf')
    max_profit = 0
    for price in prices:
        min_price = min(min_price, price)
        profit = price - min_price
        max_profit = max(max_profit, profit)
    return max_profit
```

**Time:** O(n), **Space:** O(1)

---

#### Problem 9: Majority Element (LC 169) ⭐ HIGH VALUE
**Pattern:** Boyer-Moore Voting Algorithm  
**Difficulty:** Easy  
**Key Concepts:**
- Majority definition (> n/2)
- Cancellation principle
- Linear time, constant space

**What to Learn:**
- **Naive:** Hash map to count frequency - O(n) space
- **Optimal:** Boyer-Moore Voting
  - Maintain candidate and count
  - Increment count if same as candidate
  - Decrement count if different
  - Change candidate when count reaches 0
- **Why it works:** Majority element cancels all others

**Template:**
```python
def majorityElement(nums):
    candidate, count = None, 0
    for num in nums:
        if count == 0:
            candidate = num
        count += (1 if num == candidate else -1)
    return candidate
```

---

#### Problem 10: Single Number (LC 136) 🎯 PRACTICE
**Pattern:** Bit Manipulation (XOR)  
**Difficulty:** Easy  
**Key Concepts:**
- XOR properties
- Cancellation of duplicates

**What to Learn:**
- **XOR Properties:**
  - `a ^ a = 0`
  - `a ^ 0 = a`
  - Commutative and associative
- XOR all elements: duplicates cancel, single remains
- One line solution: `reduce(lambda x, y: x ^ y, nums)`

**Time:** O(n), **Space:** O(1)

### Week 2 Study Plan

**Monday:** Problems 1-2 (Two Sum, Contains Duplicate)  
**Tuesday:** Problems 3-4 (Valid Anagram, Group Anagrams)  
**Wednesday:** Problems 5-6 (Valid Palindrome, Merge Sorted Array)  
**Thursday:** Problems 7-8 (Move Zeroes, Best Time to Buy and Sell Stock)  
**Friday:** Problems 9-10 (Majority Element, Single Number)  
**Saturday:** Review all 10 problems, create pattern notes  
**Sunday:** Rest or practice weak areas

### Part B: Sliding Window & Advanced Two-Pointers (Week 3)

#### Problem 11: Longest Substring Without Repeating Characters (LC 3) 🔑 ANCHOR
**Pattern:** Sliding Window (Variable Size)  
**Difficulty:** Medium  
**Key Concepts:**
- Window expansion/contraction
- Character tracking with set
- Maximum length tracking

**What to Learn:**
- **Template:** Expand right, contract left when duplicate found
- Use set to track characters in current window
- When duplicate found, remove from left until no duplicate
- Track maximum window size

**Template:**
```python
def lengthOfLongestSubstring(s):
    seen = set()
    left = 0
    max_len = 0
    for right in range(len(s)):
        while s[right] in seen:
            seen.remove(s[left])
            left += 1
        seen.add(s[right])
        max_len = max(max_len, right - left + 1)
    return max_len
```

**Time:** O(n), **Space:** O(min(n, charset))

---

#### Problem 12: Longest Repeating Character Replacement (LC 424) 💡 ADVANCED
**Pattern:** Sliding Window (Variable Size)  
**Difficulty:** Medium  
**Key Concepts:**
- Character frequency in window
- k replacements
- Window validity condition

**What to Learn:**
- Track frequency of each character in window
- Track max frequency character
- **Window valid if:** `window_length - max_frequency ≤ k`
- Slide window when invalid

**Key Insight:** We can replace up to k characters, so window is valid if we need ≤ k replacements

---

#### Problem 13: Container With Most Water (LC 11) 🔑 ANCHOR
**Pattern:** Two Pointers (Opposite Direction)  
**Difficulty:** Medium  
**Key Concepts:**
- Area calculation
- Greedy movement
- Optimal strategy

**What to Learn:**
- **Formula:** `area = (right - left) * min(height[left], height[right])`
- **Greedy Strategy:** Move pointer with shorter height
- **Why?** Taller height might find better partner, shorter won't
- Track maximum area

**Template:**
```python
def maxArea(height):
    left, right = 0, len(height) - 1
    max_area = 0
    while left < right:
        width = right - left
        max_area = max(max_area, width * min(height[left], height[right]))
        if height[left] < height[right]:
            left += 1
        else:
            right -= 1
    return max_area
```

---

#### Problem 14: 3Sum (LC 15) 🔑 ANCHOR
**Pattern:** Two Pointers + Sorting  
**Difficulty:** Medium  
**Key Concepts:**
- Fixed + moving pointers
- Duplicate handling
- Three-pointer technique

**What to Learn:**
- Sort array first
- Fix first element, use two pointers on rest
- Skip duplicates carefully (all three positions)
- **Pattern:** Fix one, find two that sum to complement

**Template:**
```python
def threeSum(nums):
    nums.sort()
    result = []
    for i in range(len(nums) - 2):
        if i > 0 and nums[i] == nums[i-1]:
            continue  # Skip duplicate first element
        left, right = i + 1, len(nums) - 1
        while left < right:
            total = nums[i] + nums[left] + nums[right]
            if total < 0:
                left += 1
            elif total > 0:
                right -= 1
            else:
                result.append([nums[i], nums[left], nums[right]])
                # Skip duplicates for left and right
                while left < right and nums[left] == nums[left+1]:
                    left += 1
                while left < right and nums[right] == nums[right-1]:
                    right -= 1
                left += 1
                right -= 1
    return result
```

**Time:** O(n²), **Space:** O(1) excluding output

---

#### Problem 15: Product of Array Except Self (LC 238) ⭐ HIGH VALUE
**Pattern:** Prefix/Suffix Products  
**Difficulty:** Medium  
**Key Concepts:**
- Running products
- Space optimization
- Two-pass solution

**What to Learn:**
- Cannot use division (what if zero exists?)
- **Approach:** Calculate left products, then right products
- Pass 1: `result[i] = product of all elements to left of i`
- Pass 2: Multiply by product of all elements to right of i
- Use result array to store left products, multiply right products in-place

**Template:**
```python
def productExceptSelf(nums):
    n = len(nums)
    result = [1] * n
    
    # Left products
    left_product = 1
    for i in range(n):
        result[i] = left_product
        left_product *= nums[i]
    
    # Right products (multiply in place)
    right_product = 1
    for i in range(n-1, -1, -1):
        result[i] *= right_product
        right_product *= nums[i]
    
    return result
```

**Time:** O(n), **Space:** O(1) excluding output

---

#### Problem 16: Top K Frequent Elements (LC 347) ⭐ HIGH VALUE
**Pattern:** Hash Map + Heap  
**Difficulty:** Medium  
**Key Concepts:**
- Frequency counting
- Selection problem
- Multiple approaches

**What to Learn:**
- **Approach 1:** Frequency map + Min heap of size k - O(n log k)
- **Approach 2:** Frequency map + Bucket sort - O(n)
- **Bucket Sort Key Insight:** Use frequency as index

**Template (Heap):**
```python
import heapq
def topKFrequent(nums, k):
    freq = {}
    for num in nums:
        freq[num] = freq.get(num, 0) + 1
    return heapq.nlargest(k, freq.keys(), key=freq.get)
```

**Template (Bucket Sort):**
```python
def topKFrequent(nums, k):
    freq = {}
    for num in nums:
        freq[num] = freq.get(num, 0) + 1
    
    buckets = [[] for _ in range(len(nums) + 1)]
    for num, count in freq.items():
        buckets[count].append(num)
    
    result = []
    for i in range(len(buckets) - 1, -1, -1):
        result.extend(buckets[i])
        if len(result) >= k:
            return result[:k]
```

---

#### Problem 17: Subarray Sum Equals K (LC 560) 💡 ADVANCED
**Pattern:** Prefix Sum + Hash Map  
**Difficulty:** Medium  
**Key Concepts:**
- Cumulative sum
- Sum lookup
- Subarray calculation

**What to Learn:**
- **Key Insight:** If `prefixSum[j] - prefixSum[i] = k`, then `subarray[i+1:j]` sums to k
- Store prefix sums in hash map with frequencies
- For each position, check if `currentSum - k` exists in map
- Add currentSum to map

**Template:**
```python
def subarraySum(nums, k):
    count = 0
    prefix_sum = 0
    sum_freq = {0: 1}  # Base case: empty prefix
    
    for num in nums:
        prefix_sum += num
        if prefix_sum - k in sum_freq:
            count += sum_freq[prefix_sum - k]
        sum_freq[prefix_sum] = sum_freq.get(prefix_sum, 0) + 1
    
    return count
```

**Time:** O(n), **Space:** O(n)

---

#### Problem 18: Rotate Array (LC 189) 🎯 PRACTICE
**Pattern:** Array Manipulation  
**Difficulty:** Medium  
**Key Concepts:**
- Cyclic rotation
- Reversal technique
- In-place modification

**What to Learn:**
- **Three Reverses Trick:**
  1. Reverse entire array
  2. Reverse first k elements
  3. Reverse remaining elements
- Handles k > n: use `k = k % n`

**Example:** `[1,2,3,4,5,6,7], k=3`
1. Reverse all: `[7,6,5,4,3,2,1]`
2. Reverse first 3: `[5,6,7,4,3,2,1]`
3. Reverse last 4: `[5,6,7,1,2,3,4]`

---

#### Problem 19: Missing Number (LC 268) 🎯 PRACTICE
**Pattern:** Math / Bit Manipulation  
**Difficulty:** Easy  
**Key Concepts:**
- Sum formula
- XOR properties

**What to Learn:**
- **Approach 1:** Expected sum - actual sum
  - Expected: `n * (n + 1) // 2`
  - Actual: `sum(nums)`
  - Missing: `expected - actual`
- **Approach 2:** XOR all numbers and indices
  - XOR range [0, n] with array elements
  - Missing number remains

### Week 3 Study Plan

**Monday:** Problem 11 (Longest Substring) - Focus on sliding window template  
**Tuesday:** Problem 12 (Longest Repeating) + Problem 13 (Container With Water)  
**Wednesday:** Problem 14 (3Sum) - Master three-pointer technique  
**Thursday:** Problem 15 (Product Except Self) + Problem 16 (Top K Frequent)  
**Friday:** Problem 17 (Subarray Sum) + Problems 18-19  
**Saturday:** Review all 19 problems from Phase 1, create comprehensive notes  
**Sunday:** Spaced repetition: Re-solve anchor problems (1, 4, 5, 8, 11, 13, 14)

### Success Metrics for Phase 1
- ✅ Can identify when to use sliding window vs two-pointers
- ✅ Understand variable vs fixed size windows
- ✅ Master prefix sum technique
- ✅ Can implement hash map patterns from memory
- ✅ Solve Easy problems in <20 minutes
- ✅ Solve Medium problems in <35 minutes

### Key Patterns Mastered
1. **Hash Map for Complement Search** (Two Sum)
2. **Two Pointers - Opposite Direction** (Valid Palindrome, Container With Water, 3Sum)
3. **Two Pointers - Same Direction** (Merge Sorted Array, Move Zeroes)
4. **Sliding Window - Variable Size** (Longest Substring, Longest Repeating)
5. **Prefix Sum + Hash Map** (Subarray Sum Equals K)

---

## Phase 2 – Linked Lists, Stacks, and Queues

**Duration:** Week 4 (7 days)  
**Total Problems:** 21 (7 Easy, 12 Medium, 2 Hard)  
**Focus:** First 12 problems  
**Time Commitment:** 12-16 hours

### Learning Objectives
- Master pointer manipulation in linked lists
- Understand fast/slow pointer technique (Floyd's algorithm)
- Learn dummy node pattern for list operations
- Use stacks for LIFO problems
- Master monotonic stack pattern

### Core Concepts

#### 1. Linked List Fundamentals
- **Node Structure**
  ```python
  class ListNode:
      def __init__(self, val=0, next=None):
          self.val = val
          self.next = next
  ```
- **Pointer Manipulation**
  - Always track previous, current, next
  - Break links carefully
  - Update pointers in correct order
- **Common Patterns**
  - Dummy node for edge cases
  - Fast/slow pointers
  - Reversal technique

#### 2. Fast/Slow Pointer (Floyd's Algorithm)
- **Pattern:**
  ```python
  slow, fast = head, head
  while fast and fast.next:
      slow = slow.next
      fast = fast.next.next
  ```
- **Applications:**
  - Cycle detection
  - Finding middle
  - Finding kth from end
- **Why It Works:**
  - Fast moves 2x speed of slow
  - If cycle exists, they meet inside cycle
  - Middle: fast reaches end when slow at middle

#### 3. Dummy Node Pattern
- **When to Use:**
  - Result list might have new head
  - Simplifies edge cases (empty list, single node)
  - Merging operations
- **Pattern:**
  ```python
  dummy = ListNode(0)
  current = dummy
  # Build list
  return dummy.next
  ```

#### 4. Stack (LIFO - Last In First Out)
- **Operations:** O(1) push, pop, peek
- **When to Use:**
  - Matching pairs (parentheses)
  - Backtracking
  - Next greater/smaller element
  - Expression evaluation
- **Implementation:**
  - Python: list with append() and pop()
  - Java: Stack class or Deque

#### 5. Monotonic Stack
- **Increasing Monotonic Stack:**
  - Elements in increasing order from bottom to top
  - Use for "next smaller element"
- **Decreasing Monotonic Stack:**
  - Elements in decreasing order from bottom to top
  - Use for "next greater element"
- **Template:**
  ```python
  stack = []
  for i, num in enumerate(arr):
      while stack and arr[stack[-1]] < num:  # For next greater
          # Process arr[stack[-1]]
          stack.pop()
      stack.append(i)
  ```

### Problem Breakdown

#### Problem 1: Reverse Linked List (LC 206) 🔑 ANCHOR
**Pattern:** Linked List Reversal  
**Difficulty:** Easy  
**Key Concepts:**
- Pointer reversal
- Iterative vs recursive
- Three-pointer technique

**What to Learn:**
- **Iterative (Preferred):**
  - Three pointers: prev, curr, next
  - Store next before breaking link
  - Reverse link: `curr.next = prev`
  - Move all pointers forward
- **Recursive:**
  - Reverse from end
  - Return new head
  
**Template (Iterative):**
```python
def reverseList(head):
    prev = None
    curr = head
    while curr:
        next_node = curr.next
        curr.next = prev
        prev = curr
        curr = next_node
    return prev
```

**Template (Recursive):**
```python
def reverseList(head):
    if not head or not head.next:
        return head
    new_head = reverseList(head.next)
    head.next.next = head
    head.next = None
    return new_head
```

**Time:** O(n), **Space:** O(1) iterative, O(n) recursive (stack)

---

#### Problem 2: Merge Two Sorted Lists (LC 21) 🔑 ANCHOR
**Pattern:** Linked List Merging  
**Difficulty:** Easy  
**Key Concepts:**
- Dummy node pattern
- Merging logic
- Pointer advancement

**What to Learn:**
- Use dummy node to simplify edge cases
- Compare heads of both lists
- Advance pointer of smaller value
- Link remaining list at end

**Template:**
```python
def mergeTwoLists(l1, l2):
    dummy = ListNode(0)
    current = dummy
    
    while l1 and l2:
        if l1.val < l2.val:
            current.next = l1
            l1 = l1.next
        else:
            current.next = l2
            l2 = l2.next
        current = current.next
    
    current.next = l1 if l1 else l2
    return dummy.next
```

**Time:** O(m + n), **Space:** O(1)

---

#### Problem 3: Linked List Cycle (LC 141) 🔑 ANCHOR
**Pattern:** Fast/Slow Pointers (Floyd's Cycle Detection)  
**Difficulty:** Easy  
**Key Concepts:**
- Cycle detection
- Two-speed traversal

**What to Learn:**
- Fast moves 2 steps, slow moves 1 step
- If cycle exists, they eventually meet
- If fast reaches None, no cycle

**Template:**
```python
def hasCycle(head):
    if not head or not head.next:
        return False
    
    slow, fast = head, head
    while fast and fast.next:
        slow = slow.next
        fast = fast.next.next
        if slow == fast:
            return True
    return False
```

**Time:** O(n), **Space:** O(1)

**Why It Works:** If cycle exists with length k, fast gains 1 step on slow per iteration. They meet in at most k iterations.

---

#### Problem 4: Linked List Cycle II (LC 142) 💡 ADVANCED
**Pattern:** Fast/Slow Pointers (Cycle Entry)  
**Difficulty:** Medium  
**Key Concepts:**
- Finding cycle start point
- Mathematical proof
- Two-phase algorithm

**What to Learn:**
- **Phase 1:** Detect cycle (same as LC 141)
- **Phase 2:** Find entry point
  - Reset one pointer to head
  - Move both at same speed (1 step)
  - Meeting point is cycle start

**Mathematical Proof:**
- Let distance from head to cycle start = F
- Let distance from cycle start to meeting point = a
- Let cycle length = C
- At meeting: slow traveled F + a, fast traveled F + a + nC (n cycles)
- Since fast = 2 × slow: F + a + nC = 2(F + a)
- Solving: F = nC - a = (n-1)C + (C - a)
- C - a is distance from meeting point to cycle start
- So F = distance from head to cycle start = distance from meeting point to cycle start

**Template:**
```python
def detectCycle(head):
    if not head or not head.next:
        return None
    
    # Phase 1: Detect cycle
    slow, fast = head, head
    has_cycle = False
    while fast and fast.next:
        slow = slow.next
        fast = fast.next.next
        if slow == fast:
            has_cycle = True
            break
    
    if not has_cycle:
        return None
    
    # Phase 2: Find entry point
    slow = head
    while slow != fast:
        slow = slow.next
        fast = fast.next
    
    return slow
```

---

#### Problem 5: Middle of the Linked List (LC 876) 🎯 PRACTICE
**Pattern:** Fast/Slow Pointers  
**Difficulty:** Easy  
**Key Concepts:**
- Finding middle in one pass
- Even vs odd length handling

**What to Learn:**
- Fast moves 2 steps, slow moves 1 step
- When fast reaches end, slow at middle
- For even length, returns second middle

**Template:**
```python
def middleNode(head):
    slow, fast = head, head
    while fast and fast.next:
        slow = slow.next
        fast = fast.next.next
    return slow
```

---

#### Problem 6: Palindrome Linked List (LC 234) ⭐ HIGH VALUE
**Pattern:** Fast/Slow + Reverse + Compare  
**Difficulty:** Easy  
**Key Concepts:**
- Combining multiple patterns
- Space optimization

**What to Learn:**
- **Step 1:** Find middle (LC 876)
- **Step 2:** Reverse second half (LC 206)
- **Step 3:** Compare first half with reversed second half
- **Optional Step 4:** Restore list

**Template:**
```python
def isPalindrome(head):
    # Find middle
    slow, fast = head, head
    while fast and fast.next:
        slow = slow.next
        fast = fast.next.next
    
    # Reverse second half
    prev = None
    while slow:
        next_node = slow.next
        slow.next = prev
        prev = slow
        slow = next_node
    
    # Compare
    left, right = head, prev
    while right:  # Only need to check second half
        if left.val != right.val:
            return False
        left = left.next
        right = right.next
    
    return True
```

**Time:** O(n), **Space:** O(1)

---

#### Problem 7: Valid Parentheses (LC 20) 🔑 ANCHOR
**Pattern:** Stack (Matching Pairs)  
**Difficulty:** Easy  
**Key Concepts:**
- LIFO for matching
- Opening/closing pairs
- Stack-based validation

**What to Learn:**
- Push opening brackets onto stack
- For closing bracket, pop and check match
- At end, stack should be empty

**Template:**
```python
def isValid(s):
    stack = []
    mapping = {')': '(', '}': '{', ']': '['}
    
    for char in s:
        if char in mapping:  # Closing bracket
            if not stack or stack[-1] != mapping[char]:
                return False
            stack.pop()
        else:  # Opening bracket
            stack.append(char)
    
    return len(stack) == 0
```

**Time:** O(n), **Space:** O(n)

---

#### Problem 8: Remove Nth Node From End of List (LC 19) ⭐ HIGH VALUE
**Pattern:** Two Pointers (N-ahead)  
**Difficulty:** Medium  
**Key Concepts:**
- Gap between pointers
- One-pass solution
- Dummy node for edge cases

**What to Learn:**
- Move fast pointer n steps ahead
- Move both pointers together
- When fast reaches end, slow at (n+1)th from end
- Use dummy node to handle removing head

**Template:**
```python
def removeNthFromEnd(head, n):
    dummy = ListNode(0)
    dummy.next = head
    slow, fast = dummy, dummy
    
    # Move fast n+1 steps ahead
    for _ in range(n + 1):
        fast = fast.next
    
    # Move both until fast reaches end
    while fast:
        slow = slow.next
        fast = fast.next
    
    # Remove nth node
    slow.next = slow.next.next
    return dummy.next
```

**Time:** O(n), **Space:** O(1)

---

#### Problem 9: Reorder List (LC 143) 💡 ADVANCED
**Pattern:** Multiple Patterns Combined  
**Difficulty:** Medium  
**Key Concepts:**
- Find middle
- Reverse second half
- Merge alternately

**What to Learn:**
- **Step 1:** Find middle (LC 876)
- **Step 2:** Reverse second half (LC 206)
- **Step 3:** Merge first half with reversed second half alternately

**Template:**
```python
def reorderList(head):
    if not head or not head.next:
        return
    
    # Find middle
    slow, fast = head, head
    while fast.next and fast.next.next:
        slow = slow.next
        fast = fast.next.next
    
    # Reverse second half
    second = slow.next
    slow.next = None
    prev = None
    while second:
        next_node = second.next
        second.next = prev
        prev = second
        second = next_node
    second = prev
    
    # Merge alternately
    first = head
    while second:
        temp1, temp2 = first.next, second.next
        first.next = second
        second.next = temp1
        first, second = temp1, temp2
```

---

#### Problem 10: Add Two Numbers (LC 2) ⭐ HIGH VALUE
**Pattern:** Linked List (Digit Addition)  
**Difficulty:** Medium  
**Key Concepts:**
- Digit-by-digit addition
- Carry handling
- Different lengths

**What to Learn:**
- Add corresponding digits + carry
- Create new node with sum % 10
- Update carry = sum // 10
- Handle different lengths and final carry

**Template:**
```python
def addTwoNumbers(l1, l2):
    dummy = ListNode(0)
    current = dummy
    carry = 0
    
    while l1 or l2 or carry:
        val1 = l1.val if l1 else 0
        val2 = l2.val if l2 else 0
        
        total = val1 + val2 + carry
        carry = total // 10
        current.next = ListNode(total % 10)
        
        current = current.next
        if l1: l1 = l1.next
        if l2: l2 = l2.next
    
    return dummy.next
```

---

#### Problem 11: Daily Temperatures (LC 739) 🔑 ANCHOR
**Pattern:** Monotonic Stack (Decreasing)  
**Difficulty:** Medium  
**Key Concepts:**
- Next greater element
- Index tracking
- Stack of indices

**What to Learn:**
- **Decreasing monotonic stack** (stores indices, not values)
- For each temperature:
  - While current > stack top temperature: pop and calculate days
  - Push current index
- Result: days until warmer temperature

**Template:**
```python
def dailyTemperatures(temperatures):
    n = len(temperatures)
    result = [0] * n
    stack = []  # Stack of indices
    
    for i, temp in enumerate(temperatures):
        while stack and temperatures[stack[-1]] < temp:
            prev_index = stack.pop()
            result[prev_index] = i - prev_index
        stack.append(i)
    
    return result
```

**Time:** O(n), **Space:** O(n)

**Why Monotonic Stack?** Each element pushed once, popped once = O(n) total

---

#### Problem 12: Min Stack (LC 155) ⭐ HIGH VALUE
**Pattern:** Stack Design (Auxiliary Stack)  
**Difficulty:** Medium  
**Key Concepts:**
- O(1) minimum retrieval
- Two-stack approach
- Synchronized operations

**What to Learn:**
- **Main stack:** Normal push/pop operations
- **Min stack:** Tracks minimum at each level
  - Push: push min(current, current_min)
  - Pop: pop from both stacks
  - GetMin: peek min stack
- Both stacks have same size

**Template:**
```python
class MinStack:
    def __init__(self):
        self.stack = []
        self.min_stack = []
    
    def push(self, val):
        self.stack.append(val)
        if not self.min_stack:
            self.min_stack.append(val)
        else:
            self.min_stack.append(min(val, self.min_stack[-1]))
    
    def pop(self):
        self.stack.pop()
        self.min_stack.pop()
    
    def top(self):
        return self.stack[-1]
    
    def getMin(self):
        return self.min_stack[-1]
```

**All operations:** O(1) time, **Space:** O(n)

### Week 4 Study Plan

**Monday:** Problems 1-3 (Reverse, Merge, Cycle)  
**Tuesday:** Problems 4-5 (Cycle II, Middle) + review fast/slow pattern  
**Wednesday:** Problems 6-7 (Palindrome, Valid Parentheses)  
**Thursday:** Problems 8-9 (Remove Nth, Reorder List)  
**Friday:** Problems 10-12 (Add Two Numbers, Daily Temperatures, Min Stack)  
**Saturday:** Review all 12 problems, practice fast/slow pointer pattern  
**Sunday:** Spaced repetition: Re-solve anchor problems (1, 2, 3, 7, 11)

### Remaining 9 Problems (Advanced Practice)
- Swap Nodes in Pairs (LC 24) - Medium
- Reverse Nodes in k-Group (LC 25) - Hard
- Rotate List (LC 61) - Medium
- Partition List (LC 86) - Medium
- Sort List (LC 148) - Medium
- Intersection of Two Linked Lists (LC 160) - Easy
- Remove Linked List Elements (LC 203) - Easy
- Odd Even Linked List (LC 328) - Medium
- Flatten a Multilevel Doubly Linked List (LC 430) - Medium

**Practice these after mastering first 12 problems**

### Success Metrics for Phase 2
- ✅ Can implement fast/slow pointer pattern from memory
- ✅ Know when to use dummy node
- ✅ Understand monotonic stack concept
- ✅ Comfortable with pointer manipulation
- ✅ Can reverse linked list iteratively and recursively
- ✅ Master stack-based problems (parentheses, temperatures)

### Key Patterns Mastered
1. **Linked List Reversal** (In-place pointer manipulation)
2. **Fast/Slow Pointers** (Cycle detection, middle finding)
3. **Dummy Node** (Simplifying edge cases)
4. **Stack for Matching** (Valid Parentheses)
5. **Monotonic Stack** (Next greater/smaller element)

---

## Phase 3 – Trees, BSTs, and Balanced Trees

**Duration:** Weeks 5-6 (14 days)  
**Total Problems:** 23+ problems  
**Focus:** Core tree patterns and BST properties  
**Time Commitment:** 18-22 hours

### Learning Objectives
- Master recursive tree patterns (top-down and bottom-up)
- Understand tree traversals (DFS and BFS)
- Learn BST properties and how to exploit them
- Handle height-balanced trees
- Master path and ancestor problems

### Core Concepts

#### 1. Tree Recursion Patterns

**Top-Down Recursion (Preorder):**
- Process current node first
- Pass information down to children
- Use for: path sum, depth tracking, validation

```python
def topDown(node, param):
    if not node:
        return base_case
    
    # Process current node with param
    left_result = topDown(node.left, updated_param)
    right_result = topDown(node.right, updated_param)
    
    return combine(left_result, right_result)
```

**Bottom-Up Recursion (Postorder):**
- Process children first
- Return information up to parent
- Use for: height, diameter, subtree properties

```python
def bottomUp(node):
    if not node:
        return base_case
    
    left_result = bottomUp(node.left)
    right_result = bottomUp(node.right)
    
    # Process current node with children results
    return compute_result(node, left_result, right_result)
```

#### 2. Tree Traversals

**DFS Traversals:**
1. **Preorder (Root → Left → Right)**
   - Use for: tree serialization, prefix expression
   ```python
   def preorder(node):
       if not node: return
       process(node)
       preorder(node.left)
       preorder(node.right)
   ```

2. **Inorder (Left → Root → Right)**
   - Use for: BST → sorted array
   ```python
   def inorder(node):
       if not node: return
       inorder(node.left)
       process(node)
       inorder(node.right)
   ```

3. **Postorder (Left → Right → Root)**
   - Use for: tree deletion, postfix expression
   ```python
   def postorder(node):
       if not node: return
       postorder(node.left)
       postorder(node.right)
       process(node)
   ```

**BFS (Level Order):**
- Use queue for level-by-level traversal
```python
from collections import deque

def levelOrder(root):
    if not root: return []
    queue = deque([root])
    result = []
    
    while queue:
        level_size = len(queue)
        level = []
        for _ in range(level_size):
            node = queue.popleft()
            level.append(node.val)
            if node.left: queue.append(node.left)
            if node.right: queue.append(node.right)
        result.append(level)
    
    return result
```

#### 3. Binary Search Tree (BST) Properties
- **Left subtree:** All values < node.val
- **Right subtree:** All values > node.val
- **Inorder traversal:** Produces sorted sequence
- **Search/Insert/Delete:** O(h) where h = height

**BST Validation:**
```python
def isValidBST(node, min_val=float('-inf'), max_val=float('inf')):
    if not node:
        return True
    if not (min_val < node.val < max_val):
        return False
    return (isValidBST(node.left, min_val, node.val) and
            isValidBST(node.right, node.val, max_val))
```

#### 4. Height-Balanced Trees
- **Balanced:** |leftHeight - rightHeight| ≤ 1 for all nodes
- **Check balance:** Bottom-up recursion
- **Height:** -1 for null, 1 + max(leftHeight, rightHeight)

### Part A: Basic Tree Recursion (Week 5)

#### Problem 1: Maximum Depth of Binary Tree (LC 104) 🔑 ANCHOR
**Pattern:** DFS (Bottom-Up)  
**Difficulty:** Easy  
**Key Concepts:**
- Height calculation
- Base case handling
- Simplest recursion

**What to Learn:**
- **Base case:** null node has depth 0
- **Recursive case:** 1 + max(leftDepth, rightDepth)
- Template for bottom-up recursion

**Template:**
```python
def maxDepth(root):
    if not root:
        return 0
    return 1 + max(maxDepth(root.left), maxDepth(root.right))
```

**Time:** O(n), **Space:** O(h) recursion stack

---

#### Problem 2: Invert Binary Tree (LC 226) 🔑 ANCHOR
**Pattern:** DFS (Preorder)  
**Difficulty:** Easy  
**Key Concepts:**
- Subtree swapping
- In-place modification

**What to Learn:**
- Swap left and right at each node
- Recurse on both children
- Can do preorder or postorder

**Template:**
```python
def invertTree(root):
    if not root:
        return None
    
    # Swap children
    root.left, root.right = root.right, root.left
    
    # Recurse
    invertTree(root.left)
    invertTree(root.right)
    
    return root
```

---

#### Problem 3: Same Tree (LC 100) 🎯 PRACTICE
**Pattern:** DFS  
**Difficulty:** Easy  
**Key Concepts:**
- Structural equality
- Value equality
- Multiple base cases

**What to Learn:**
- Check both structure and values
- Base cases:
  - Both null → True
  - One null → False
  - Values different → False
- Recurse on both children

---

#### Problem 4: Symmetric Tree (LC 101) ⭐ HIGH VALUE
**Pattern:** DFS (Mirror Check)  
**Difficulty:** Easy  
**Key Concepts:**
- Mirror symmetry
- Dual recursion

**What to Learn:**
- Helper function: isMirror(left, right)
- Check:
  - left.val == right.val
  - left.left mirrors right.right
  - left.right mirrors right.left

**Template:**
```python
def isSymmetric(root):
    def isMirror(left, right):
        if not left and not right:
            return True
        if not left or not right:
            return False
        return (left.val == right.val and
                isMirror(left.left, right.right) and
                isMirror(left.right, right.left))
    
    return isMirror(root, root)
```

---

#### Problem 5: Diameter of Binary Tree (LC 543) 🔑 ANCHOR
**Pattern:** DFS (Bottom-Up with Global Variable)  
**Difficulty:** Easy  
**Key Concepts:**
- Path length
- Global maximum
- Height calculation

**What to Learn:**
- Diameter at node = leftHeight + rightHeight
- Track global maximum
- Return height up to parent

**Template:**
```python
def diameterOfBinaryTree(root):
    self.diameter = 0
    
    def height(node):
        if not node:
            return 0
        
        left = height(node.left)
        right = height(node.right)
        
        # Update diameter
        self.diameter = max(self.diameter, left + right)
        
        # Return height
        return 1 + max(left, right)
    
    height(root)
    return self.diameter
```

**Time:** O(n), **Space:** O(h)

---

#### Problem 6: Balanced Binary Tree (LC 110) 🎯 PRACTICE
**Pattern:** DFS (Bottom-Up)  
**Difficulty:** Easy  
**Key Concepts:**
- Height balance definition
- Efficient checking

**What to Learn:**
- Check balance while calculating height
- Return -1 if subtree unbalanced
- Check: |leftHeight - rightHeight| ≤ 1

**Template:**
```python
def isBalanced(root):
    def checkHeight(node):
        if not node:
            return 0
        
        left = checkHeight(node.left)
        if left == -1:
            return -1
        
        right = checkHeight(node.right)
        if right == -1:
            return -1
        
        if abs(left - right) > 1:
            return -1
        
        return 1 + max(left, right)
    
    return checkHeight(root) != -1
```

---

#### Problem 7: Path Sum (LC 112) 🎯 PRACTICE
**Pattern:** DFS (Top-Down)  
**Difficulty:** Easy  
**Key Concepts:**
- Target sum
- Root-to-leaf path
- Subtraction approach

**What to Learn:**
- Subtract current value from target
- Base case: leaf node with target == 0
- Recurse with updated target

---

#### Problem 8: Binary Tree Level Order Traversal (LC 102) 🔑 ANCHOR
**Pattern:** BFS  
**Difficulty:** Medium  
**Key Concepts:**
- Level separation
- Queue-based traversal
- Level size tracking

**What to Learn:**
- Use queue (deque in Python)
- Process level by level
- Track level size before processing

**Template:**
```python
from collections import deque

def levelOrder(root):
    if not root:
        return []
    
    result = []
    queue = deque([root])
    
    while queue:
        level_size = len(queue)
        level = []
        
        for _ in range(level_size):
            node = queue.popleft()
            level.append(node.val)
            
            if node.left:
                queue.append(node.left)
            if node.right:
                queue.append(node.right)
        
        result.append(level)
    
    return result
```

**Time:** O(n), **Space:** O(w) where w = max width

---

#### Problem 9: Binary Tree Right Side View (LC 199) ⭐ HIGH VALUE
**Pattern:** BFS or DFS  
**Difficulty:** Medium  
**Key Concepts:**
- Rightmost node per level
- Level tracking

**What to Learn:**
- **BFS Approach:** Take last element of each level
- **DFS Approach:** Right-first traversal, add when visiting level for first time

**Template (BFS):**
```python
def rightSideView(root):
    if not root:
        return []
    
    result = []
    queue = deque([root])
    
    while queue:
        level_size = len(queue)
        
        for i in range(level_size):
            node = queue.popleft()
            
            # Last node in level
            if i == level_size - 1:
                result.append(node.val)
            
            if node.left:
                queue.append(node.left)
            if node.right:
                queue.append(node.right)
    
    return result
```

### Week 5 Study Plan

**Monday:** Problems 1-2 (Max Depth, Invert Tree) - Master basic recursion  
**Tuesday:** Problems 3-4 (Same Tree, Symmetric Tree)  
**Wednesday:** Problems 5-6 (Diameter, Balanced)  
**Thursday:** Problem 7 (Path Sum) + Review recursion patterns  
**Friday:** Problems 8-9 (Level Order, Right Side View) - BFS practice  
**Saturday:** Review all 9 problems, compare DFS vs BFS  
**Sunday:** Create recursion pattern cheatsheet

### Part B: BST & Advanced Trees (Week 6)

#### Problem 10: Validate Binary Search Tree (LC 98) 🔑 ANCHOR
**Pattern:** BST (Range Validation)  
**Difficulty:** Medium  
**Key Concepts:**
- BST property
- Range checking
- Common mistake: only comparing with immediate children

**What to Learn:**
- Pass valid range (min, max) down the tree
- Each node must be in its valid range
- Left subtree: (-∞, node.val)
- Right subtree: (node.val, ∞)

**Template:**
```python
def isValidBST(root):
    def validate(node, min_val, max_val):
        if not node:
            return True
        
        if not (min_val < node.val < max_val):
            return False
        
        return (validate(node.left, min_val, node.val) and
                validate(node.right, node.val, max_val))
    
    return validate(root, float('-inf'), float('inf'))
```

**Alternative:** Inorder traversal should give sorted array

---

#### Problem 11: Lowest Common Ancestor of BST (LC 235) 🎯 PRACTICE
**Pattern:** BST (Property Exploitation)  
**Difficulty:** Medium  
**Key Concepts:**
- BST property
- Ancestor definition
- Split point

**What to Learn:**
- Use BST property to find split point
- If p and q on different sides of node → node is LCA
- If both smaller → go left
- If both larger → go right

**Template:**
```python
def lowestCommonAncestor(root, p, q):
    while root:
        if p.val < root.val and q.val < root.val:
            root = root.left
        elif p.val > root.val and q.val > root.val:
            root = root.right
        else:
            return root
```

**Time:** O(h), **Space:** O(1)

---

#### Problem 12: Lowest Common Ancestor of Binary Tree (LC 236) 🔑 ANCHOR
**Pattern:** DFS (Bottom-Up)  
**Difficulty:** Medium  
**Key Concepts:**
- Ancestor definition
- Path tracking
- Return value meaning

**What to Learn:**
- If node is p or q, return node
- If left and right both return non-null → current is LCA
- If only one side returns non-null → return that

**Template:**
```python
def lowestCommonAncestor(root, p, q):
    if not root or root == p or root == q:
        return root
    
    left = lowestCommonAncestor(root.left, p, q)
    right = lowestCommonAncestor(root.right, p, q)
    
    if left and right:
        return root
    
    return left if left else right
```

**Time:** O(n), **Space:** O(h)

---

#### Problem 13: Kth Smallest Element in BST (LC 230) ⭐ HIGH VALUE
**Pattern:** BST + Inorder Traversal  
**Difficulty:** Medium  
**Key Concepts:**
- Inorder gives sorted order
- Counting during traversal

**What to Learn:**
- Inorder traversal of BST gives sorted sequence
- Count elements during traversal
- Return when count == k

**Template:**
```python
def kthSmallest(root, k):
    self.count = 0
    self.result = None
    
    def inorder(node):
        if not node or self.result is not None:
            return
        
        inorder(node.left)
        
        self.count += 1
        if self.count == k:
            self.result = node.val
            return
        
        inorder(node.right)
    
    inorder(root)
    return self.result
```

**Follow-up:** If tree modified often, use augmented BST (store subtree size)

---

#### Problem 14: Convert Sorted Array to BST (LC 108) 🎯 PRACTICE
**Pattern:** BST + Divide & Conquer  
**Difficulty:** Easy  
**Key Concepts:**
- Middle element as root
- Recursion on halves
- Height-balanced construction

**What to Learn:**
- Pick middle element as root
- Left half → left subtree
- Right half → right subtree
- Guarantees balanced tree

**Template:**
```python
def sortedArrayToBST(nums):
    if not nums:
        return None
    
    mid = len(nums) // 2
    root = TreeNode(nums[mid])
    root.left = sortedArrayToBST(nums[:mid])
    root.right = sortedArrayToBST(nums[mid+1:])
    
    return root
```

---

#### Problem 15: Binary Tree Maximum Path Sum (LC 124) 💡 ADVANCED
**Pattern:** DFS (Bottom-Up with Global Max)  
**Difficulty:** Hard  
**Key Concepts:**
- Path definition
- Global maximum
- Contribution vs full path

**What to Learn:**
- Path can: not include parent, include as left/right child
- At each node: maxPath = node.val + max(0, left) + max(0, right)
- Return contribution to parent: node.val + max(0, left, right)
- Use max(0, ...) to handle negative paths

**Template:**
```python
def maxPathSum(root):
    self.max_sum = float('-inf')
    
    def maxGain(node):
        if not node:
            return 0
        
        left_gain = max(maxGain(node.left), 0)
        right_gain = max(maxGain(node.right), 0)
        
        # Path through current node
        path_sum = node.val + left_gain + right_gain
        self.max_sum = max(self.max_sum, path_sum)
        
        # Return contribution
        return node.val + max(left_gain, right_gain)
    
    maxGain(root)
    return self.max_sum
```

---

#### Problem 16: Serialize and Deserialize Binary Tree (LC 297) 💡 ADVANCED
**Pattern:** Tree + Design  
**Difficulty:** Hard  
**Key Concepts:**
- Encoding/decoding
- Preorder traversal
- Delimiter usage
- Null markers

**What to Learn:**
- Use preorder: root → left → right
- Mark null nodes explicitly
- Use delimiter (comma)
- Reconstruct using same order

**Template:**
```python
class Codec:
    def serialize(self, root):
        def dfs(node):
            if not node:
                vals.append('null')
                return
            vals.append(str(node.val))
            dfs(node.left)
            dfs(node.right)
        
        vals = []
        dfs(root)
        return ','.join(vals)
    
    def deserialize(self, data):
        def dfs():
            val = next(vals)
            if val == 'null':
                return None
            node = TreeNode(int(val))
            node.left = dfs()
            node.right = dfs()
            return node
        
        vals = iter(data.split(','))
        return dfs()
```

### Week 6 Study Plan

**Monday:** Problem 10 (Validate BST) - Master BST validation  
**Tuesday:** Problems 11-12 (LCA of BST, LCA of Binary Tree)  
**Wednesday:** Problems 13-14 (Kth Smallest, Sorted Array to BST)  
**Thursday:** Problem 15 (Maximum Path Sum) - Advanced bottom-up pattern  
**Friday:** Problem 16 (Serialize/Deserialize)  
**Saturday:** Review all Week 6 problems, compare BST vs Binary Tree techniques  
**Sunday:** Spaced repetition: Re-solve Week 5 anchor problems

### Remaining Tree Problems (Advanced Practice)
- Subtree of Another Tree (LC 572)
- Construct Binary Tree from Preorder and Inorder (LC 105)
- Binary Tree Zigzag Level Order (LC 103)
- Count Good Nodes in Binary Tree (LC 1448)
- All Nodes Distance K in Binary Tree (LC 863)
- Lowest Common Ancestor of Deepest Leaves (LC 1123)

### Success Metrics for Phase 3
- ✅ Can write basic tree recursion (top-down and bottom-up)
- ✅ Understand BFS level-order traversal
- ✅ Know when to track global vs return value
- ✅ Understand BST properties and how to use them
- ✅ Can implement LCA for both BST and binary tree
- ✅ Master inorder traversal for BST problems

### Key Patterns Mastered
1. **Bottom-Up Recursion** (Max Depth, Diameter, Maximum Path Sum)
2. **Top-Down Recursion** (Path Sum, Validate BST)
3. **BFS Level Order** (Level Order Traversal, Right Side View)
4. **BST Property Exploitation** (Validate BST, LCA in BST, Kth Smallest)
5. **Tree Construction** (Sorted Array to BST, Serialize/Deserialize)

---

## Phase 4 – Graphs and Graph Traversal

**Duration:** Weeks 7-8 (14 days)  
**Total Problems:** 16+ problems  
**Focus:** DFS/BFS on graphs, topological sort  
**Time Commitment:** 16-20 hours

### Learning Objectives
- Master graph representation (adjacency list, matrix, implicit grid)
- Understand DFS for exploration and connectivity
- Learn BFS for shortest path in unweighted graphs
- Handle grid traversal problems
- Master topological sort for DAGs
- Learn cycle detection techniques

### Core Concepts

#### 1. Graph Representation

**Adjacency List (Most Common):**
```python
# Directed graph
graph = {
    0: [1, 2],
    1: [3],
    2: [3],
    3: []
}

# From edge list
def buildGraph(n, edges):
    graph = [[] for _ in range(n)]
    for u, v in edges:
        graph[u].append(v)
        # For undirected: graph[v].append(u)
    return graph
```

**Adjacency Matrix:**
```python
# n x n matrix
graph = [[0] * n for _ in range(n)]
for u, v in edges:
    graph[u][v] = 1
    # For undirected: graph[v][u] = 1
```

**Implicit Grid (Common in Leetcode):**
```python
# 2D grid represents graph
# grid[i][j] = 1 (land/cell)
# Neighbors: 4-directional or 8-directional
```

#### 2. DFS on Graphs

**Template (Recursive):**
```python
def dfs(node, graph, visited):
    visited.add(node)
    # Process node
    for neighbor in graph[node]:
        if neighbor not in visited:
            dfs(neighbor, graph, visited)
```

**Template (Iterative with Stack):**
```python
def dfs_iterative(start, graph):
    visited = set()
    stack = [start]
    
    while stack:
        node = stack.pop()
        if node in visited:
            continue
        visited.add(node)
        # Process node
        for neighbor in graph[node]:
            if neighbor not in visited:
                stack.append(neighbor)
```

**When to Use DFS:**
- Finding connected components
- Detecting cycles
- Path finding (not necessarily shortest)
- Topological sort
- Exploring all possibilities

#### 3. BFS on Graphs

**Template:**
```python
from collections import deque

def bfs(start, graph):
    visited = set([start])
    queue = deque([start])
    
    while queue:
        node = queue.popleft()
        # Process node
        for neighbor in graph[node]:
            if neighbor not in visited:
                visited.add(neighbor)
                queue.append(neighbor)
```

**Multi-Source BFS:**
```python
def multi_source_bfs(starts, graph):
    visited = set(starts)
    queue = deque(starts)
    level = 0
    
    while queue:
        level_size = len(queue)
        for _ in range(level_size):
            node = queue.popleft()
            # Process node at this level
            for neighbor in graph[node]:
                if neighbor not in visited:
                    visited.add(neighbor)
                    queue.append(neighbor)
        level += 1
```

**When to Use BFS:**
- Shortest path in unweighted graph
- Level-by-level exploration
- Multi-source problems
- Minimum steps/distance

#### 4. Grid Traversal

**4-Directional Movement:**
```python
directions = [(0, 1), (1, 0), (0, -1), (-1, 0)]  # right, down, left, up

def dfs_grid(row, col, grid, visited):
    if (row < 0 or row >= len(grid) or 
        col < 0 or col >= len(grid[0]) or
        (row, col) in visited or 
        grid[row][col] == 0):  # Invalid cell
        return
    
    visited.add((row, col))
    
    for dr, dc in directions:
        dfs_grid(row + dr, col + dc, grid, visited)
```

**8-Directional Movement:**
```python
directions = [(0,1), (1,0), (0,-1), (-1,0), (1,1), (1,-1), (-1,1), (-1,-1)]
```

#### 5. Topological Sort

**Kahn's Algorithm (BFS-based):**
```python
def topologicalSort(n, edges):
    graph = [[] for _ in range(n)]
    in_degree = [0] * n
    
    # Build graph and count in-degrees
    for u, v in edges:
        graph[u].append(v)
        in_degree[v] += 1
    
    # Start with nodes having in-degree 0
    queue = deque([i for i in range(n) if in_degree[i] == 0])
    result = []
    
    while queue:
        node = queue.popleft()
        result.append(node)
        
        for neighbor in graph[node]:
            in_degree[neighbor] -= 1
            if in_degree[neighbor] == 0:
                queue.append(neighbor)
    
    # Check if all nodes processed (no cycle)
    return result if len(result) == n else []
```

**DFS-based (with cycle detection):**
```python
def topologicalSortDFS(n, graph):
    visited = set()
    rec_stack = set()
    result = []
    
    def dfs(node):
        if node in rec_stack:
            return False  # Cycle detected
        if node in visited:
            return True
        
        visited.add(node)
        rec_stack.add(node)
        
        for neighbor in graph[node]:
            if not dfs(neighbor):
                return False
        
        rec_stack.remove(node)
        result.append(node)
        return True
    
    for i in range(n):
        if i not in visited:
            if not dfs(i):
                return []  # Cycle exists
    
    return result[::-1]  # Reverse for topological order
```

### Part A: Grid DFS/BFS (Week 7)

#### Problem 1: Flood Fill (LC 733) 🔑 ANCHOR
**Pattern:** DFS / BFS on Grid  
**Difficulty:** Easy  
**Key Concepts:**
- Connected component coloring
- 4-directional traversal
- Visited tracking

**What to Learn:**
- Simplest graph problem
- Mark visited by changing color
- Base case: out of bounds, wrong color, already new color

**Template (DFS):**
```python
def floodFill(image, sr, sc, color):
    if image[sr][sc] == color:
        return image
    
    original_color = image[sr][sc]
    rows, cols = len(image), len(image[0])
    
    def dfs(r, c):
        if (r < 0 or r >= rows or c < 0 or c >= cols or
            image[r][c] != original_color):
            return
        
        image[r][c] = color
        
        # 4-directional
        dfs(r+1, c)
        dfs(r-1, c)
        dfs(r, c+1)
        dfs(r, c-1)
    
    dfs(sr, sc)
    return image
```

**Time:** O(m×n), **Space:** O(m×n) recursion stack

---

#### Problem 2: Number of Islands (LC 200) 🔑 ANCHOR
**Pattern:** DFS / BFS (Component Counting)  
**Difficulty:** Medium  
**Key Concepts:**
- Connected components
- Grid traversal
- Count separate islands

**What to Learn:**
- Iterate through grid
- When finding unvisited land, increment count and DFS entire island
- Mark visited cells

**Template:**
```python
def numIslands(grid):
    if not grid:
        return 0
    
    rows, cols = len(grid), len(grid[0])
    count = 0
    
    def dfs(r, c):
        if (r < 0 or r >= rows or c < 0 or c >= cols or
            grid[r][c] == '0'):
            return
        
        grid[r][c] = '0'  # Mark as visited
        
        dfs(r+1, c)
        dfs(r-1, c)
        dfs(r, c+1)
        dfs(r, c-1)
    
    for r in range(rows):
        for c in range(cols):
            if grid[r][c] == '1':
                count += 1
                dfs(r, c)
    
    return count
```

**Time:** O(m×n), **Space:** O(m×n)

---

#### Problem 3: Max Area of Island (LC 695) 🎯 PRACTICE
**Pattern:** DFS (Area Calculation)  
**Difficulty:** Medium  
**Key Concepts:**
- Variation of Number of Islands
- Return area from DFS

**What to Learn:**
- DFS returns area of current island
- Track maximum area
- Each cell contributes 1 to area

**Template:**
```python
def maxAreaOfIsland(grid):
    if not grid:
        return 0
    
    rows, cols = len(grid), len(grid[0])
    max_area = 0
    
    def dfs(r, c):
        if (r < 0 or r >= rows or c < 0 or c >= cols or
            grid[r][c] == 0):
            return 0
        
        grid[r][c] = 0  # Mark visited
        
        area = 1
        area += dfs(r+1, c)
        area += dfs(r-1, c)
        area += dfs(r, c+1)
        area += dfs(r, c-1)
        
        return area
    
    for r in range(rows):
        for c in range(cols):
            if grid[r][c] == 1:
                max_area = max(max_area, dfs(r, c))
    
    return max_area
```

---

#### Problem 4: Rotting Oranges (LC 994) 🔑 ANCHOR
**Pattern:** Multi-Source BFS  
**Difficulty:** Medium  
**Key Concepts:**
- Multi-source BFS
- Time tracking
- Level-by-level spread

**What to Learn:**
- Add all rotten oranges to queue initially
- Process level by level (each level = 1 minute)
- Track fresh oranges count
- Return -1 if any fresh orange remains

**Template:**
```python
from collections import deque

def orangesRotting(grid):
    rows, cols = len(grid), len(grid[0])
    queue = deque()
    fresh = 0
    
    # Find all rotten oranges and count fresh
    for r in range(rows):
        for c in range(cols):
            if grid[r][c] == 2:
                queue.append((r, c))
            elif grid[r][c] == 1:
                fresh += 1
    
    if fresh == 0:
        return 0
    
    minutes = 0
    directions = [(0,1), (1,0), (0,-1), (-1,0)]
    
    while queue:
        level_size = len(queue)
        
        for _ in range(level_size):
            r, c = queue.popleft()
            
            for dr, dc in directions:
                nr, nc = r + dr, c + dc
                if (0 <= nr < rows and 0 <= nc < cols and
                    grid[nr][nc] == 1):
                    grid[nr][nc] = 2
                    fresh -= 1
                    queue.append((nr, nc))
        
        minutes += 1
    
    return minutes - 1 if fresh == 0 else -1
```

**Time:** O(m×n), **Space:** O(m×n)

---

#### Problem 5: Surrounded Regions (LC 130) ⭐ HIGH VALUE
**Pattern:** DFS / BFS (Border Search)  
**Difficulty:** Medium  
**Key Concepts:**
- Reverse thinking
- Border exploration
- Marking safe regions

**What to Learn:**
- DFS from border 'O's - these cannot be captured
- Mark border-connected 'O's as safe (e.g., '#')
- Flip remaining 'O's to 'X'
- Restore safe 'O's

**Template:**
```python
def solve(board):
    if not board:
        return
    
    rows, cols = len(board), len(board[0])
    
    def dfs(r, c):
        if (r < 0 or r >= rows or c < 0 or c >= cols or
            board[r][c] != 'O'):
            return
        
        board[r][c] = '#'  # Mark as safe
        
        dfs(r+1, c)
        dfs(r-1, c)
        dfs(r, c+1)
        dfs(r, c-1)
    
    # Mark border-connected 'O's
    for r in range(rows):
        dfs(r, 0)
        dfs(r, cols-1)
    for c in range(cols):
        dfs(0, c)
        dfs(rows-1, c)
    
    # Flip remaining 'O's to 'X' and restore '#' to 'O'
    for r in range(rows):
        for c in range(cols):
            if board[r][c] == 'O':
                board[r][c] = 'X'
            elif board[r][c] == '#':
                board[r][c] = 'O'
```

---

#### Problem 6: Pacific Atlantic Water Flow (LC 417) 💡 ADVANCED
**Pattern:** Multi-Source DFS/BFS  
**Difficulty:** Medium  
**Key Concepts:**
- Multi-source exploration
- Intersection of sets
- Reverse flow thinking

**What to Learn:**
- Water flows from high to low
- Reverse: find cells reachable from Pacific, from Atlantic
- Answer: intersection of both sets
- Run DFS from all Pacific borders, then all Atlantic borders

**Template:**
```python
def pacificAtlantic(heights):
    if not heights:
        return []
    
    rows, cols = len(heights), len(heights[0])
    pacific = set()
    atlantic = set()
    
    def dfs(r, c, visited):
        visited.add((r, c))
        directions = [(0,1), (1,0), (0,-1), (-1,0)]
        
        for dr, dc in directions:
            nr, nc = r + dr, c + dc
            if (0 <= nr < rows and 0 <= nc < cols and
                (nr, nc) not in visited and
                heights[nr][nc] >= heights[r][c]):  # Water can flow from higher
                dfs(nr, nc, visited)
    
    # DFS from Pacific borders
    for r in range(rows):
        dfs(r, 0, pacific)
    for c in range(cols):
        dfs(0, c, pacific)
    
    # DFS from Atlantic borders
    for r in range(rows):
        dfs(r, cols-1, atlantic)
    for c in range(cols):
        dfs(rows-1, c, atlantic)
    
    # Intersection
    return list(pacific & atlantic)
```

### Week 7 Study Plan

**Monday:** Problems 1-2 (Flood Fill, Number of Islands) - Master grid DFS  
**Tuesday:** Problem 3 (Max Area of Island) + review DFS template  
**Wednesday:** Problem 4 (Rotting Oranges) - Master multi-source BFS  
**Thursday:** Problem 5 (Surrounded Regions) - Border search technique  
**Friday:** Problem 6 (Pacific Atlantic) - Multi-source DFS  
**Saturday:** Review all 6 problems, practice grid traversal  
**Sunday:** Compare DFS vs BFS approaches for grid problems

### Part B: Graph Structures & Topological Sort (Week 8)

#### Problem 7: Clone Graph (LC 133) ⭐ HIGH VALUE
**Pattern:** DFS / BFS (Deep Copy)  
**Difficulty:** Medium  
**Key Concepts:**
- Deep copy
- Node mapping
- Adjacency list traversal

**What to Learn:**
- Use hash map: old node → new node
- DFS/BFS to traverse original graph
- Create new nodes and link neighbors
- Check map before creating to avoid duplicates

**Template (DFS):**
```python
def cloneGraph(node):
    if not node:
        return None
    
    clones = {}  # old node -> new node
    
    def dfs(node):
        if node in clones:
            return clones[node]
        
        # Create clone
        clone = Node(node.val)
        clones[node] = clone
        
        # Clone neighbors
        for neighbor in node.neighbors:
            clone.neighbors.append(dfs(neighbor))
        
        return clone
    
    return dfs(node)
```

---

#### Problem 8: Number of Provinces (LC 547) 🎯 PRACTICE
**Pattern:** DFS / BFS (Adjacency Matrix)  
**Difficulty:** Medium  
**Key Concepts:**
- Adjacency matrix representation
- Connected components
- Component counting

**What to Learn:**
- Apply Number of Islands logic to adjacency matrix
- Iterate through nodes
- DFS from unvisited nodes
- Count number of DFS calls

**Template:**
```python
def findCircleNum(isConnected):
    n = len(isConnected)
    visited = set()
    count = 0
    
    def dfs(city):
        visited.add(city)
        for neighbor in range(n):
            if isConnected[city][neighbor] == 1 and neighbor not in visited:
                dfs(neighbor)
    
    for i in range(n):
        if i not in visited:
            count += 1
            dfs(i)
    
    return count
```

---

#### Problem 9: Course Schedule (LC 207) 🔑 ANCHOR
**Pattern:** Topological Sort (Cycle Detection)  
**Difficulty:** Medium  
**Key Concepts:**
- DAG (Directed Acyclic Graph)
- Cycle detection
- Kahn's algorithm

**What to Learn:**
- Course prerequisites form directed graph
- Can finish all courses if no cycle (DAG)
- Kahn's algorithm: BFS with in-degrees
- Track in-degrees, process nodes with in-degree 0

**Template:**
```python
from collections import deque

def canFinish(numCourses, prerequisites):
    graph = [[] for _ in range(numCourses)]
    in_degree = [0] * numCourses
    
    # Build graph
    for course, prereq in prerequisites:
        graph[prereq].append(course)
        in_degree[course] += 1
    
    # Start with courses having no prerequisites
    queue = deque([i for i in range(numCourses) if in_degree[i] == 0])
    count = 0
    
    while queue:
        course = queue.popleft()
        count += 1
        
        for next_course in graph[course]:
            in_degree[next_course] -= 1
            if in_degree[next_course] == 0:
                queue.append(next_course)
    
    return count == numCourses
```

**Time:** O(V + E), **Space:** O(V + E)

---

#### Problem 10: Course Schedule II (LC 210) ⭐ HIGH VALUE
**Pattern:** Topological Sort (Ordering)  
**Difficulty:** Medium  
**Key Concepts:**
- Topological ordering
- Kahn's algorithm
- Return actual order

**What to Learn:**
- Extension of Course Schedule (LC 207)
- Return the BFS traversal result (topological order)
- If count != numCourses, cycle exists → return []

**Template:**
```python
from collections import deque

def findOrder(numCourses, prerequisites):
    graph = [[] for _ in range(numCourses)]
    in_degree = [0] * numCourses
    
    for course, prereq in prerequisites:
        graph[prereq].append(course)
        in_degree[course] += 1
    
    queue = deque([i for i in range(numCourses) if in_degree[i] == 0])
    order = []
    
    while queue:
        course = queue.popleft()
        order.append(course)
        
        for next_course in graph[course]:
            in_degree[next_course] -= 1
            if in_degree[next_course] == 0:
                queue.append(next_course)
    
    return order if len(order) == numCourses else []
```

---

#### Problem 11: Graph Valid Tree (LC 261) 💡 ADVANCED
**Pattern:** DFS / Union-Find  
**Difficulty:** Medium  
**Key Concepts:**
- Tree conditions
- Cycle detection
- Connectivity check

**What to Learn:**
- Tree properties:
  1. Exactly n-1 edges
  2. No cycles
  3. Connected (all nodes reachable)
- Check: edges == n-1 AND DFS visits all nodes

**Template (DFS):**
```python
def validTree(n, edges):
    if len(edges) != n - 1:
        return False
    
    graph = [[] for _ in range(n)]
    for u, v in edges:
        graph[u].append(v)
        graph[v].append(u)
    
    visited = set()
    
    def dfs(node, parent):
        visited.add(node)
        for neighbor in graph[node]:
            if neighbor == parent:
                continue
            if neighbor in visited:
                return False  # Cycle
            if not dfs(neighbor, node):
                return False
        return True
    
    return dfs(0, -1) and len(visited) == n
```

### Week 8 Study Plan

**Monday:** Problem 7 (Clone Graph) - Master node mapping  
**Tuesday:** Problem 8 (Number of Provinces) - Adjacency matrix practice  
**Wednesday:** Problem 9 (Course Schedule) - Master topological sort  
**Thursday:** Problem 10 (Course Schedule II) - Topological ordering  
**Friday:** Problem 11 (Graph Valid Tree) - Tree validation  
**Saturday:** Review all Week 8 problems, practice topological sort  
**Sunday:** Spaced repetition: Re-solve Week 7 anchor problems

### Remaining Graph Problems (Advanced Practice)
- Word Ladder (LC 127) - BFS, shortest transformation
- Word Ladder II (LC 126) - BFS + backtracking
- Alien Dictionary (LC 269) - Topological sort
- Graph Valid Tree with Union-Find (LC 261)
- Accounts Merge (LC 721) - Union-Find

### Success Metrics for Phase 4
- ✅ Can implement topological sort (Kahn's algorithm)
- ✅ Understand cycle detection in directed graphs
- ✅ Know tree conditions for graphs
- ✅ Master grid DFS/BFS from memory
- ✅ Understand multi-source BFS pattern
- ✅ Can identify when DFS vs BFS is better

### Key Patterns Mastered
1. **Grid DFS/BFS** (Flood Fill, Number of Islands, Rotting Oranges)
2. **Multi-Source BFS** (Rotting Oranges, Pacific Atlantic)
3. **Border Search** (Surrounded Regions)
4. **Topological Sort** (Course Schedule, Course Schedule II)
5. **Cycle Detection** (Course Schedule, Graph Valid Tree)

---

## Phase 5 – Sorting, Searching, and Divide & Conquer

**Duration:** Week 9 (7 days)  
**Total Problems:** 18 (Focus on first 12)  
**Time Commitment:** 12-16 hours

### Learning Objectives
- Master binary search variants
- Learn interval merging and scheduling
- Understand binary search on answer space
- Handle 2D matrix search problems
- Master sorting-based problem solving

### Core Concepts

#### 1. Binary Search Variants

**Basic Binary Search (Finding Element):**
```python
def binarySearch(nums, target):
    left, right = 0, len(nums) - 1
    while left <= right:
        mid = left + (right - left) // 2
        if nums[mid] == target:
            return mid
        elif nums[mid] < target:
            left = mid + 1
        else:
            right = mid - 1
    return -1
```

**Find Leftmost (First Occurrence):**
```python
def findFirst(nums, target):
    left, right = 0, len(nums) - 1
    result = -1
    while left <= right:
        mid = left + (right - left) // 2
        if nums[mid] == target:
            result = mid
            right = mid - 1  # Continue searching left
        elif nums[mid] < target:
            left = mid + 1
        else:
            right = mid - 1
    return result
```

**Find Rightmost (Last Occurrence):**
```python
def findLast(nums, target):
    left, right = 0, len(nums) - 1
    result = -1
    while left <= right:
        mid = left + (right - left) // 2
        if nums[mid] == target:
            result = mid
            left = mid + 1  # Continue searching right
        elif nums[mid] < target:
            left = mid + 1
        else:
            right = mid - 1
    return result
```

#### 2. Binary Search on Answer Space

**Template:**
```python
def binarySearchAnswer(constraints):
    def feasible(answer):
        # Check if 'answer' satisfies constraints
        # Return True if feasible, False otherwise
        pass
    
    left, right = min_answer, max_answer
    result = left
    
    while left <= right:
        mid = left + (right - left) // 2
        if feasible(mid):
            result = mid
            # Depending on optimization:
            # Minimize: right = mid - 1
            # Maximize: left = mid + 1
        else:
            # Adjust search space
    
    return result
```

**When to Use:**
- Answer is in a range
- Can check feasibility of answer in O(n) or O(n log n)
- Want to minimize or maximize answer

**Common Problems:**
- Koko Eating Bananas
- Capacity to Ship Packages
- Split Array Largest Sum
- Minimum Speed to Arrive on Time

#### 3. Interval Problems

**Interval Representation:**
```python
intervals = [[start1, end1], [start2, end2], ...]
```

**Sorting Intervals:**
```python
intervals.sort(key=lambda x: x[0])  # Sort by start
intervals.sort(key=lambda x: x[1])  # Sort by end
```

**Overlap Detection:**
```python
def overlaps(interval1, interval2):
    return interval1[1] >= interval2[0]  # Assuming sorted by start
```

**Merging Intervals:**
```python
def merge(intervals):
    if not intervals:
        return []
    
    intervals.sort(key=lambda x: x[0])
    merged = [intervals[0]]
    
    for current in intervals[1:]:
        last = merged[-1]
        if current[0] <= last[1]:  # Overlap
            merged[-1] = [last[0], max(last[1], current[1])]
        else:
            merged.append(current)
    
    return merged
```

### Problem Breakdown

#### Problem 1: Merge Intervals (LC 56) 🔑 ANCHOR
**Pattern:** Intervals + Sorting  
**Difficulty:** Medium  
**Key Concepts:**
- Overlap detection
- Merging logic
- Sorting prerequisite

**What to Learn:**
- Sort intervals by start time
- If current.start ≤ last.end → overlap, merge
- Else → add new interval
- Merge: [min(starts), max(ends)]

**Template:**
```python
def merge(intervals):
    if not intervals:
        return []
    
    intervals.sort(key=lambda x: x[0])
    merged = [intervals[0]]
    
    for current in intervals[1:]:
        last = merged[-1]
        if current[0] <= last[1]:
            merged[-1][1] = max(last[1], current[1])
        else:
            merged.append(current)
    
    return merged
```

**Time:** O(n log n), **Space:** O(n)

---

#### Problem 2: Insert Interval (LC 57) ⭐ HIGH VALUE
**Pattern:** Intervals  
**Difficulty:** Medium  
**Key Concepts:**
- Three phases
- Insertion and merging

**What to Learn:**
- **Phase 1:** Add all intervals before overlap
- **Phase 2:** Merge all overlapping intervals with new interval
- **Phase 3:** Add all intervals after overlap

**Template:**
```python
def insert(intervals, newInterval):
    result = []
    i = 0
    n = len(intervals)
    
    # Phase 1: Before overlap
    while i < n and intervals[i][1] < newInterval[0]:
        result.append(intervals[i])
        i += 1
    
    # Phase 2: Merge overlapping
    while i < n and intervals[i][0] <= newInterval[1]:
        newInterval[0] = min(newInterval[0], intervals[i][0])
        newInterval[1] = max(newInterval[1], intervals[i][1])
        i += 1
    result.append(newInterval)
    
    # Phase 3: After overlap
    while i < n:
        result.append(intervals[i])
        i += 1
    
    return result
```

---

#### Problem 3: Non-overlapping Intervals (LC 435) 💡 ADVANCED
**Pattern:** Greedy + Intervals  
**Difficulty:** Medium  
**Key Concepts:**
- Interval scheduling
- Greedy choice
- Removal counting

**What to Learn:**
- Sort by end time
- Greedy: keep interval with earliest end time
- Remove overlapping intervals
- Count removals

**Template:**
```python
def eraseOverlapIntervals(intervals):
    if not intervals:
        return 0
    
    intervals.sort(key=lambda x: x[1])  # Sort by end
    count = 0
    end = intervals[0][1]
    
    for i in range(1, len(intervals)):
        if intervals[i][0] < end:  # Overlap
            count += 1
        else:
            end = intervals[i][1]
    
    return count
```

---

#### Problem 4: Meeting Rooms (LC 252) 🎯 PRACTICE
**Pattern:** Intervals + Sorting  
**Difficulty:** Easy  
**Key Concepts:**
- Overlap checking
- Simple validation

**What to Learn:**
- Sort by start time
- Check if any meeting starts before previous ends
- Return False if overlap found

**Template:**
```python
def canAttendMeetings(intervals):
    intervals.sort(key=lambda x: x[0])
    for i in range(1, len(intervals)):
        if intervals[i][0] < intervals[i-1][1]:
            return False
    return True
```

---

#### Problem 5: Search in Rotated Sorted Array (LC 33) 🔑 ANCHOR
**Pattern:** Binary Search (Modified)  
**Difficulty:** Medium  
**Key Concepts:**
- Pivot detection
- Half selection
- Modified binary search

**What to Learn:**
- One half is always sorted
- Determine which half is sorted
- Check if target in sorted half
- Adjust search space accordingly

**Template:**
```python
def search(nums, target):
    left, right = 0, len(nums) - 1
    
    while left <= right:
        mid = left + (right - left) // 2
        
        if nums[mid] == target:
            return mid
        
        # Left half is sorted
        if nums[left] <= nums[mid]:
            if nums[left] <= target < nums[mid]:
                right = mid - 1
            else:
                left = mid + 1
        # Right half is sorted
        else:
            if nums[mid] < target <= nums[right]:
                left = mid + 1
            else:
                right = mid - 1
    
    return -1
```

**Time:** O(log n), **Space:** O(1)

---

#### Problem 6: Find Minimum in Rotated Sorted Array (LC 153) 🎯 PRACTICE
**Pattern:** Binary Search  
**Difficulty:** Medium  
**Key Concepts:**
- Minimum finding
- Pivot detection

**What to Learn:**
- Compare mid with right
- If mid > right: min in right half
- Else: min in left half (including mid)

**Template:**
```python
def findMin(nums):
    left, right = 0, len(nums) - 1
    
    while left < right:
        mid = left + (right - left) // 2
        
        if nums[mid] > nums[right]:
            left = mid + 1
        else:
            right = mid
    
    return nums[left]
```

---

#### Problem 7: Find First and Last Position of Element (LC 34) 🔑 ANCHOR
**Pattern:** Binary Search (Boundary Search)  
**Difficulty:** Medium  
**Key Concepts:**
- Leftmost position
- Rightmost position
- Two binary searches

**What to Learn:**
- Run binary search twice
- First: find leftmost (first occurrence)
- Second: find rightmost (last occurrence)

**Template:**
```python
def searchRange(nums, target):
    def findFirst():
        left, right = 0, len(nums) - 1
        result = -1
        while left <= right:
            mid = left + (right - left) // 2
            if nums[mid] == target:
                result = mid
                right = mid - 1  # Continue left
            elif nums[mid] < target:
                left = mid + 1
            else:
                right = mid - 1
        return result
    
    def findLast():
        left, right = 0, len(nums) - 1
        result = -1
        while left <= right:
            mid = left + (right - left) // 2
            if nums[mid] == target:
                result = mid
                left = mid + 1  # Continue right
            elif nums[mid] < target:
                left = mid + 1
            else:
                right = mid - 1
        return result
    
    return [findFirst(), findLast()]
```

---

#### Problem 8: Koko Eating Bananas (LC 875) 🔑 ANCHOR
**Pattern:** Binary Search on Answer Space  
**Difficulty:** Medium  
**Key Concepts:**
- Feasibility check
- Answer space search
- Minimization

**What to Learn:**
- Binary search on eating speed k
- Check if possible to finish within h hours
- Minimize k

**Template:**
```python
def minEatingSpeed(piles, h):
    def canFinish(k):
        hours = 0
        for pile in piles:
            hours += (pile + k - 1) // k  # Ceiling division
        return hours <= h
    
    left, right = 1, max(piles)
    
    while left < right:
        mid = left + (right - left) // 2
        if canFinish(mid):
            right = mid  # Try smaller speed
        else:
            left = mid + 1
    
    return left
```

**Time:** O(n log m) where m = max(piles), **Space:** O(1)

---

#### Problem 9: Search a 2D Matrix (LC 74) 🎯 PRACTICE
**Pattern:** Binary Search (2D → 1D)  
**Difficulty:** Medium  
**Key Concepts:**
- Treating matrix as sorted array
- Index conversion

**What to Learn:**
- Treat m×n matrix as sorted array of length m×n
- Convert 1D index to 2D: row = mid // cols, col = mid % cols
- Standard binary search

**Template:**
```python
def searchMatrix(matrix, target):
    if not matrix or not matrix[0]:
        return False
    
    rows, cols = len(matrix), len(matrix[0])
    left, right = 0, rows * cols - 1
    
    while left <= right:
        mid = left + (right - left) // 2
        row, col = mid // cols, mid % cols
        value = matrix[row][col]
        
        if value == target:
            return True
        elif value < target:
            left = mid + 1
        else:
            right = mid - 1
    
    return False
```

---

#### Problem 10: Find Peak Element (LC 162) ⭐ HIGH VALUE
**Pattern:** Binary Search  
**Difficulty:** Medium  
**Key Concepts:**
- Peak definition
- Gradient following

**What to Learn:**
- Peak: element greater than neighbors
- If mid > mid+1: peak in left half (including mid)
- Else: peak in right half
- Guaranteed to find peak

**Template:**
```python
def findPeakElement(nums):
    left, right = 0, len(nums) - 1
    
    while left < right:
        mid = left + (right - left) // 2
        
        if nums[mid] > nums[mid + 1]:
            right = mid  # Peak in left half
        else:
            left = mid + 1  # Peak in right half
    
    return left
```

---

#### Problem 11: Sort Colors (LC 75) ⭐ HIGH VALUE
**Pattern:** Two Pointers (Three-way Partition)  
**Difficulty:** Medium  
**Key Concepts:**
- Dutch National Flag problem
- In-place sorting
- Three pointers

**What to Learn:**
- Three-way partition: 0s | 1s | 2s
- Low pointer: next position for 0
- High pointer: next position for 2
- Mid pointer: current element

**Template:**
```python
def sortColors(nums):
    low, mid, high = 0, 0, len(nums) - 1
    
    while mid <= high:
        if nums[mid] == 0:
            nums[low], nums[mid] = nums[mid], nums[low]
            low += 1
            mid += 1
        elif nums[mid] == 1:
            mid += 1
        else:  # nums[mid] == 2
            nums[mid], nums[high] = nums[high], nums[mid]
            high -= 1
```

**Time:** O(n), **Space:** O(1)

---

#### Problem 12: Capacity To Ship Packages Within D Days (LC 1011) 💡 ADVANCED
**Pattern:** Binary Search on Answer Space  
**Difficulty:** Medium  
**Key Concepts:**
- Capacity search
- Simulation
- Minimization

**What to Learn:**
- Similar to Koko Eating Bananas
- Binary search on ship capacity
- Simulate loading to check feasibility
- Minimize capacity

**Template:**
```python
def shipWithinDays(weights, days):
    def canShip(capacity):
        days_needed = 1
        current_load = 0
        
        for weight in weights:
            if current_load + weight > capacity:
                days_needed += 1
                current_load = weight
            else:
                current_load += weight
        
        return days_needed <= days
    
    left, right = max(weights), sum(weights)
    
    while left < right:
        mid = left + (right - left) // 2
        if canShip(mid):
            right = mid
        else:
            left = mid + 1
    
    return left
```

### Week 9 Study Plan

**Monday:** Problems 1-2 (Merge Intervals, Insert Interval)  
**Tuesday:** Problems 3-4 (Non-overlapping Intervals, Meeting Rooms)  
**Wednesday:** Problems 5-6 (Search Rotated Array, Find Minimum)  
**Thursday:** Problem 7 (Find First and Last Position) - Master boundary search  
**Friday:** Problems 8-9 (Koko Bananas, Search 2D Matrix)  
**Saturday:** Problems 10-12 (Find Peak, Sort Colors, Ship Packages)  
**Sunday:** Review all problems, practice binary search variants

### Remaining 6 Problems (Advanced Practice)
- Meeting Rooms II (LC 253) - Min heap for intervals
- Minimum Number of Arrows to Burst Balloons (LC 452)
- Search a 2D Matrix II (LC 240)
- Median of Two Sorted Arrays (LC 4) - Hard
- Split Array Largest Sum (LC 410) - Binary search on answer
- Find K Closest Elements (LC 658)

### Success Metrics for Phase 5
- ✅ Can implement binary search variants (leftmost, rightmost, answer space)
- ✅ Understand interval merging pattern
- ✅ Master binary search on answer space
- ✅ Can handle 2D matrix problems
- ✅ Recognize when to use binary search vs sorting

### Key Patterns Mastered
1. **Interval Merging** (Merge Intervals, Insert Interval)
2. **Binary Search Variants** (Leftmost, Rightmost, Rotated Array)
3. **Binary Search on Answer Space** (Koko Bananas, Ship Packages)
4. **Greedy Interval Scheduling** (Non-overlapping Intervals)
5. **Three-way Partition** (Sort Colors - Dutch National Flag)

---

Due to length constraints, I'll create the remaining phases (6-11) in the second file. This completes Phases 0-5 of the detailed syllabus.

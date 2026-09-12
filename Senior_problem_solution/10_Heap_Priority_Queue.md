# Phase 10: Heap / Priority Queue

> **Focus:** Partial Ordering, Min-Heap vs Max-Heap Selection, Quickselect Trade-offs, Top-K Streaming Invariants, and Dual Balanced Heaps for Dynamic Median.  
> **Source Curriculum:** [`Senior_dsa_question_list.md`](../Senior_dsa_question_list.md) — Phase 10 (Problems #56–#59)

---

## 56. Kth Largest Element in an Array (LeetCode #215)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#min-heap` `#quickselect` `#top-k` `#partial-sort` |
| **LeetCode Link** | [Kth Largest Element in an Array](https://leetcode.com/problems/kth-largest-element-in-an-array/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an integer array `nums` and an integer `k`, return the $k$-th largest element in the array. Note that it is the $k$-th largest element in sorted order, not the $k$-th distinct element. Can you solve it without full sorting?
- **Key Constraints:**
  - $1 \le k \le nums.Length \le 10^5$.
  - $-10^4 \le nums[i] \le 10^4$.
- **Senior Edge Cases to Defend:**
  - $k = 1$ (maximum element) or $k = nums.Length$ (minimum element).
  - Massive duplicates (e.g. array where all elements are identical; a naive quickselect partition can degrade to $O(N^2)$ without 3-way Dutch National Flag partitioning).
  - Production streaming inputs where the entire array cannot fit into memory at once.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Maintain a Min-Heap of size $k$ representing the $k$ largest elements encountered; the heap root is the minimum among the top $k$, which is precisely the $k$-th largest element. Alternatively, use Quickselect for average $O(N)$ in-place selection.
- **Sample 1:**
  - **Input:** `nums = [3, 2, 1, 5, 6, 4]`, `k = 2`
  - **Output:** `5`
- **Sample 2:**
  - **Input:** `nums = [3, 2, 3, 1, 2, 4, 5, 5, 6]`, `k = 4`
  - **Output:** `4`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)
- **3.1 The Intuitive Spark & Conceptual Metaphor:**
  - *The VIP Velvet Rope (Min-Heap of size k):* Imagine an exclusive club with exactly $k$ VIP seats. The bouncer only tracks the *poorest* person in the VIP room (the root of the min-heap). When a newcomer arrives, if the VIP room is not full, they enter. If full, the bouncer compares the newcomer with the poorest VIP: if the newcomer is wealthier, the poorest VIP is evicted and the newcomer enters. After checking the entire queue of $N$ candidates, the person sitting right at the door of the VIP room is precisely the $k$-th wealthiest person!
  - *The Targeted Searchlight (Quickselect):* Sorting the entire array ($O(N \log N)$) is overkill because we do not care about the relative order of elements above or below rank $k$. Quickselect picks a pivot, partitions the array into three zones `[ < pivot | == pivot | > pivot ]`, and points its searchlight *only* into the partition containing the target index $N - k$.
- **3.2 The Naive Bottleneck & Redundant Computation:**
  - Full comparison sort ($O(N \log N)$) establishes $N!$ possible permutations, doing immense redundant work ordering elements that are irrelevant to rank $k$.
  - A Min-Heap of size $k$ spends only $O(\log k)$ per element, giving $O(N \log k)$ total time, with bounded $O(k)$ memory.
  - Quickselect discards roughly half of the search space at each iteration, yielding an $O(N)$ geometric series: $N + N/2 + N/4 + \dots = 2N$.
- **3.3 The Breakthrough Insight & Mathematical Invariant:**
  - **Min-Heap Invariant:**
    $$\forall x \in \text{Heap}: x \ge \text{Heap.Peek()} \quad \land \quad |\text{Heap}| = k$$
    When the heap contains $k$ elements, every element ever evicted was $\le \text{Heap.Peek()}$. Hence, $\text{Heap.Peek()}$ is guaranteed to be the $k$-th largest element.
  - **Dutch National Flag (3-Way) Partition Invariant:**
    To guarantee linear average time even with thousands of identical elements, partition the array into:
    $$\text{Zone 1: } [0 \dots lt - 1] < \text{pivot} \quad | \quad \text{Zone 2: } [lt \dots gt] = \text{pivot} \quad | \quad \text{Zone 3: } [gt + 1 \dots N - 1] > \text{pivot}$$
    If target index $T \in [lt, gt]$, terminate immediately in $O(1)$!
- **3.4 Cursor Semantics & Invariant Partition Architecture:**
  ```text
  Min-Heap Streaming Architecture (Capacity k):
  [ Discarded Elements <= Root ]  |  Heap: [ (Root = k-th Largest) <= Larger Contenders ]
  
  Quickselect 3-Way Partition Architecture:
  +----------------------+----------------------+----------------------+
  |    < Pivot (lt)      |     == Pivot         |     > Pivot (gt)     |
  |  Index: 0 ... lt-1   |  Index: lt ... gt    |  Index: gt+1 ... N-1 |
  +----------------------+----------------------+----------------------+
                         ^                      ^
                  Target in here? -> Found!
  ```
- **3.5 State Transition Triggers & Decision Gates:**
  - **Heap Enqueue Gate:** Always enqueue arriving number.
  - **Heap Eviction Gate:** If `minHeap.Count > k`, execute `minHeap.Dequeue()`.
  - **Quickselect Target Gate:**
    - If $T \in [lt, gt] \implies$ Return `nums[T]`.
    - If $T < lt \implies$ Recurse left: `Quickselect(left, lt - 1)`.
    - If $T > gt \implies$ Recurse right: `Quickselect(gt + 1, right)`.
- **3.6 Concrete Step-by-Step State Trace:**
  - Input: `nums = [3, 2, 1, 5, 6, 4]`, `k = 2` (Target: $2$-nd largest $\implies$ index $6 - 2 = 4$ in sorted order).

  *Min-Heap Trace (Size bounded to 2):*

  | Step | Processed `num` | Heap Before Eviction | Action | Heap After Eviction | Root Value (`Peek`) |
  | :--- | :--- | :--- | :--- | :--- | :--- |
  | 1 | 3 | `[3]` | Count $\le 2$ | `[3]` | 3 |
  | 2 | 2 | `[2, 3]` | Count $\le 2$ | `[2, 3]` | 2 |
  | 3 | 1 | `[1, 3, 2]` | Count > 2 $\implies$ Dequeue 1 | `[2, 3]` | 2 |
  | 4 | 5 | `[2, 3, 5]` | Count > 2 $\implies$ Dequeue 2 | `[3, 5]` | 3 |
  | 5 | 6 | `[3, 5, 6]` | Count > 2 $\implies$ Dequeue 3 | `[5, 6]` | 5 |
  | 6 | 4 | `[4, 6, 5]` | Count > 2 $\implies$ Dequeue 4 | `[5, 6]` | **5** |

  *Result:* `minHeap.Peek() == 5`.

### 4. Approach & Complexity Deconstruction
- **4.1 Anchor Points & Approach Selection Criteria:**
  - **Approach 1 (Min-Heap of Size $k$):** The production standard for streaming data, read-only constraints, or multi-threaded pipelines. Memory is strictly $O(k)$ and the input array is never mutated.
  - **Approach 2 (In-Place 3-Way Quickselect):** Optimal for offline in-memory data where mutating `nums` is permitted. Provides average $O(N)$ execution with zero auxiliary heap allocations.
- **4.2 Step-by-Step Natural Progression Flow:**
  - *Step 1: Setup & Boundaries:* Validate $k \in [1, N]$. Compute target index $N - k$.
  - *Step 2: Main Exploration Loop:* Stream through elements (Heap) or recursively partition active sub-array (Quickselect).
  - *Step 3: Invariant Maintenance & Condition Gates:* Evict root when heap count exceeds $k$; or advance Dutch National Flag pointers `lt`, `i`, `gt`.
  - *Step 4: Resolution & Return:* Return `minHeap.Peek()` or `nums[targetIndex]`.
- **4.3 Alternative Approaches Analysis:**
  - Max-Heap of size $N$: Heapifying all $N$ elements takes $O(N)$ time, followed by $k$ deletions taking $O(k \log N)$. When $k \ll N$, this is competitive, but consumes $O(N)$ auxiliary memory.
- **4.4 Multi-Dimensional Complexity & Trade-Off Matrix:**

| Approach | Time (Best) | Time (Avg) | Time (Worst) | Aux Space | In-Place Mutability | Streaming Suitability |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **Min-Heap of Size $k$** | $O(N \log k)$ | $O(N \log k)$ | $O(N \log k)$ | $O(k)$ | Read-Only | Optimal ($O(k)$ memory) |
| **3-Way Quickselect** | $O(N)$ | $O(N)$ | $O(N^2)$ (adversarial) | $O(1)$ | Mutates Input | Poor (requires random access) |
| **Full Array Sort** | $O(N \log N)$ | $O(N \log N)$ | $O(N \log N)$ | $O(1)$ or $O(N)$ | Mutates / Clones | Unsuitable |

### 5. Production C# Implementations

```csharp
// ============================================================================
// ARCHITECTURE ANCHOR: Kth Largest Element in an Array
// Primary: Min-Heap of Capacity k (O(N log k) Time, O(k) Space, Streaming-Safe)
// Secondary: Randomized 3-Way Quickselect (O(N) Avg Time, O(1) Extra Space)
// Invariant: Min-Heap of size k holds the k largest elements; root is k-th largest
// ============================================================================

public class Solution
{
    /// <summary>
    /// Finds k-th largest element using an online Min-Heap of capacity k.
    /// Non-destructive, ideal for streaming or read-only input sources.
    /// </summary>
    public int FindKthLargest(int[] nums, int k)
    {
        ArgumentNullException.ThrowIfNull(nums);
        if (k <= 0 || k > nums.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(k), "k must be between 1 and nums.Length.");
        }

        // Min-heap prioritizes smaller values at the root for eviction
        var minHeap = new PriorityQueue<int, int>(capacity: k + 1);

        foreach (int num in nums)
        {
            // Priority equals value for natural ascending min-heap ordering
            minHeap.Enqueue(num, num);

            // Invariant Gate: Bounded capacity ensures only top-k candidates survive
            if (minHeap.Count > k)
            {
                minHeap.Dequeue();
            }
        }

        // The minimum of the k largest elements is the k-th largest overall
        return minHeap.Peek();
    }
}

public class SolutionQuickselect
{
    /// <summary>
    /// Finds k-th largest element in-place using randomized 3-way Quickselect.
    /// Defends against adversarial quadratic degradation on duplicated arrays.
    /// </summary>
    public int FindKthLargest(int[] nums, int k)
    {
        ArgumentNullException.ThrowIfNull(nums);
        if (k <= 0 || k > nums.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(k), "Invalid k.");
        }

        // The k-th largest element sits at index (nums.Length - k) in ascending order
        int targetIndex = nums.Length - k;
        Quickselect(nums, 0, nums.Length - 1, targetIndex);
        return nums[targetIndex];
    }

    private static void Quickselect(int[] nums, int left, int right, int targetIndex)
    {
        // Base Gate: Single element partition is already settled
        if (left >= right)
        {
            return;
        }

        // Random Pivot Selection: Defends against pre-sorted adversarial worst-case inputs
        int pivotIndex = Random.Shared.Next(left, right + 1);
        int pivotValue = nums[pivotIndex];

        // 3-Way Dutch National Flag Partitioning
        int lt = left;
        int gt = right;
        int i = left;

        while (i <= gt)
        {
            if (nums[i] < pivotValue)
            {
                (nums[lt], nums[i]) = (nums[i], nums[lt]);
                lt++;
                i++;
            }
            else if (nums[i] > pivotValue)
            {
                (nums[gt], nums[i]) = (nums[i], nums[gt]);
                gt--;
            }
            else
            {
                i++;
            }
        }

        // Decision Gate: Check which partition contains the target rank index
        if (targetIndex >= lt && targetIndex <= gt)
        {
            // Target lies within the pivot duplicate cluster: Exact match found!
            return;
        }
        else if (targetIndex < lt)
        {
            // Search strictly within the smaller-element left partition
            Quickselect(nums, left, lt - 1, targetIndex);
        }
        else
        {
            // Search strictly within the larger-element right partition
            Quickselect(nums, gt + 1, right, targetIndex);
        }
    }
}
```

---

## 57. Top K Frequent Elements (LeetCode #347)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#hash-map` `#bucket-sort` `#min-heap` `#top-k` |
| **LeetCode Link** | [Top K Frequent Elements](https://leetcode.com/problems/top-k-frequent-elements/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an integer array `nums` and an integer `k`, return the `k` most frequent elements in any order. The time complexity must be better than $O(N \log N)$.
- **Key Constraints:**
  - $1 \le nums.Length \le 10^5$.
  - $k \in [1, \text{number of unique elements}]$.
  - It is guaranteed that the answer is unique.
- **Senior Edge Cases to Defend:**
  - Uniform frequency: All elements appear exactly once ($k = \text{unique count}$).
  - Negative values as dictionary keys (handled naturally by 2's complement hash keys).
  - $k = 1$ when a single dominant element exists.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Count frequencies with a hash map. Then, invert the relationship using Bucket Sort where index represents frequency $[1, N]$ for guaranteed $O(N)$ linear time; or stream through a Min-Heap of size $k$ for $O(N \log k)$ online processing.
- **Sample 1:**
  - **Input:** `nums = [1, 1, 1, 2, 2, 3]`, `k = 2`
  - **Output:** `[1, 2]`
- **Sample 2:**
  - **Input:** `nums = [1]`, `k = 1`
  - **Output:** `[1]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)
- **3.1 The Intuitive Spark & Conceptual Metaphor:**
  - *Frequency Inversion / Pigeonhole Buckets:* The maximum frequency any number can possibly achieve in an array of length $N$ is $N$. Instead of sorting unique numbers by frequency (which takes $O(U \log U)$), create $N + 1$ distinct buckets where slot $f$ contains all numbers that occurred exactly $f$ times. Walk backward from bucket $N$ down to 1, harvesting numbers until $k$ items are gathered. This completely bypasses comparison sorting!
- **3.2 The Naive Bottleneck & Redundant Computation:**
  - Sorting unique elements by frequency using `Array.Sort` or LINQ `OrderByDescending` takes $O(U \log U)$ where $U$ is unique count. When $U \approx N$, this burns unnecessary logarithmic overhead on values already bounded by $N$.
- **3.3 The Breakthrough Insight & Mathematical Invariant:**
  - **Bucket Boundedness Invariant:**
    $$\forall x \in \text{nums}: 1 \le \text{Frequency}(x) \le N$$
  - Scanning buckets in descending order from $N$ down to 1 guarantees that every element pulled out has a frequency strictly greater than or equal to any element in subsequent buckets.
- **3.4 Cursor Semantics & Invariant Partition Architecture:**
  ```text
  Bucket Sort Inverted Array [0 ... N]:
  Freq Index:    0       1             2             3       ...     N
  Bucket Lists: [ - ] [ [3] ]     [ [2] ]       [ [1] ]    ...   [ - ]
                                  ^             ^
                          Harvest Direction <=== (Descending scan)
  ```
- **3.5 State Transition Triggers & Decision Gates:**
  - Frequency Counting Gate: `map[num] = map.GetValueOrDefault(num, 0) + 1;`
  - Bucket Placement Gate: `buckets[freq].Add(num);`
  - Harvest Termination Gate: `if (gathered == k) break;`
- **3.6 Concrete Step-by-Step State Trace:**
  - Input: `nums = [1, 1, 1, 2, 2, 3]`, `k = 2`

  | Phase | Structure State | Detail |
  | :--- | :--- | :--- |
  | Freq Map | `{ 1: 3, 2: 2, 3: 1 }` | Count completed in $O(N)$ pass |
  | Inverted Buckets | `bucket[1] = [3]`, `bucket[2] = [2]`, `bucket[3] = [1]` | Grouped by frequency |
  | Harvest $f = 6 \dots 4$ | Null / Empty | Skipped |
  | Harvest $f = 3$ | `bucket[3]` contains `1` | Gathered: `[1]`, count = 1 |
  | Harvest $f = 2$ | `bucket[2]` contains `2` | Gathered: `[1, 2]`, count = 2 $\implies$ **STOP ($k$ reached)** |

### 4. Approach & Complexity Deconstruction
- **4.1 Anchor Points & Approach Selection Criteria:**
  - **Approach 1 (Bucket Sort):** Optimal linear $O(N)$ algorithm. Best when memory permits an array of lists of size $N + 1$.
  - **Approach 2 (Min-Heap of Size $k$):** Best when input is streaming or memory footprint must be restricted to $O(U + k)$ without allocating $N$ bucket references.
- **4.2 Step-by-Step Natural Progression Flow:**
  - *Step 1:* Frequency map accumulation.
  - *Step 2:* Populate inverted buckets or push to size-$k$ min-heap.
  - *Step 3:* Harvest elements descending until $k$ elements are found.
  - *Step 4:* Return array.
- **4.3 Alternative Approaches Analysis:**
  - Quickselect on unique pairs `(value, frequency)` achieves average $O(U)$ time and $O(U)$ space, but requires more code than bucket sort.
- **4.4 Multi-Dimensional Complexity & Trade-Off Matrix:**

| Approach | Time (Best/Avg/Worst) | Aux Space | Cache Locality | Streaming Friendly |
| :--- | :--- | :--- | :--- | :--- |
| **Bucket Sort** | $O(N)$ | $O(N)$ | High | No (requires total frequencies) |
| **Min-Heap of Size $k$** | $O(N + U \log k)$ | $O(U + k)$ | Medium | Semi (streaming with map) |
| **Quickselect on Pairs** | $O(N + U)$ avg, $O(U^2)$ worst | $O(U)$ | High | No |

### 5. Production C# Implementations

```csharp
// ============================================================================
// ARCHITECTURE ANCHOR: Top K Frequent Elements
// Primary: Frequency Bucket Sort (Guaranteed O(N) Time, O(N) Space)
// Secondary: Min-Heap of Size k (O(N + U log k) Time, O(U + k) Space)
// Invariant: Bucket index equals frequency; scan descending to collect top-k
// ============================================================================

public class Solution
{
    /// <summary>
    /// Gathers top k frequent elements in guaranteed linear O(N) time using bucket sort.
    /// Completely avoids comparison-based sorting overhead.
    /// </summary>
    public int[] TopKFrequent(int[] nums, int k)
    {
        ArgumentNullException.ThrowIfNull(nums);
        if (k <= 0) return Array.Empty<int>();

        // Step 1: Accumulate occurrence frequencies
        var freqMap = new Dictionary<int, int>();
        foreach (int num in nums)
        {
            freqMap[num] = freqMap.GetValueOrDefault(num, 0) + 1;
        }

        // Step 2: Bucket array where index corresponds directly to frequency (1 .. nums.Length)
        var buckets = new List<int>[nums.Length + 1];
        foreach (var (val, freq) in freqMap)
        {
            buckets[freq] ??= new List<int>();
            buckets[freq].Add(val);
        }

        // Step 3: Descending harvest from maximum possible frequency down to 1
        int[] result = new int[k];
        int gathered = 0;

        for (int f = buckets.Length - 1; f >= 1 && gathered < k; f--)
        {
            if (buckets[f] == null)
            {
                continue;
            }

            foreach (int num in buckets[f])
            {
                result[gathered++] = num;
                if (gathered == k)
                {
                    break;
                }
            }
        }

        return result;
    }
}

public class SolutionHeap
{
    /// <summary>
    /// Gathers top k frequent elements using a bounded Min-Heap of size k.
    /// Memory efficient when k is significantly smaller than unique element count.
    /// </summary>
    public int[] TopKFrequent(int[] nums, int k)
    {
        var freqMap = new Dictionary<int, int>();
        foreach (int num in nums)
        {
            freqMap[num] = freqMap.GetValueOrDefault(num, 0) + 1;
        }

        // Min-heap ordered by frequency: evicts the least frequent candidate among top-k
        var minHeap = new PriorityQueue<int, int>(capacity: k + 1);

        foreach (var (val, freq) in freqMap)
        {
            minHeap.Enqueue(val, freq);

            if (minHeap.Count > k)
            {
                minHeap.Dequeue();
            }
        }

        int[] result = new int[k];
        for (int i = 0; i < k; i++)
        {
            result[i] = minHeap.Dequeue();
        }

        return result;
    }
}
```

---

## 58. K Closest Points to Origin (LeetCode #973)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🟡 Medium |
| **Priority** | 🔥 High |
| **Pattern Tags** | `#max-heap` `#quickselect` `#geometry` `#top-k` |
| **LeetCode Link** | [K Closest Points to Origin](https://leetcode.com/problems/k-closest-points-to-origin/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Given an array of `points` where $points[i] = [x_i, y_i]$ represents a point on the X-Y plane and an integer `k`, return the `k` closest points to the origin $(0, 0)$. Euclidean distance is $\sqrt{x^2 + y^2}$.
- **Key Constraints:**
  - $1 \le k \le points.Length \le 10^4$.
  - $-10^4 \le x_i, y_i \le 10^4$.
- **Senior Edge Cases to Defend:**
  - 32-bit Integer Overflow: For coordinates up to $10^4$, $x^2 \le 10^8$, and $x^2 + y^2 \le 2 \times 10^8$, which comfortably fits inside signed 32-bit `int` (`int.MaxValue` $\approx 2.14 \times 10^9$).
  - Distance ties: Any order is valid; tie-breaking stability is not required.
  - Floating point drift: Avoid `Math.Sqrt` entirely; compare squared Euclidean distances directly.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Track the $k$ smallest squared distances using a **Max-Heap** of size $k$ to continually evict the farthest candidate, or use in-place Quickselect for average $O(N)$ performance.
- **Sample 1:**
  - **Input:** `points = [[1, 3], [-2, 2]]`, `k = 1`
  - **Output:** `[[-2, 2]]`
- **Sample 2:**
  - **Input:** `points = [[3, 3], [5, -1], [-2, 4]]`, `k = 2`
  - **Output:** `[[3, 3], [-2, 4]]`

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)
- **3.1 The Intuitive Spark & Conceptual Metaphor:**
  - *Max-Heap Funnel Inversion:* To retain the $k$ *closest* points, you do NOT use a min-heap; you use a **Max-Heap**! Why? Because the *worst* of your current $k$ best candidates sits directly at the root. When a new point arrives, if it is closer than the root, you decapitate the root and insert the closer point. The Max-Heap acts as an adaptive filter ceiling.
- **3.2 The Naive Bottleneck & Redundant Computation:**
  - Calculating $\sqrt{x^2 + y^2}$ is computationally expensive. Because $\sqrt{z}$ is strictly monotonically increasing for $z \ge 0$:
    $$d_1 < d_2 \iff d_1^2 < d_2^2$$
  - Sorting all points takes $O(N \log N)$ time and pollutes cache with unnecessary swaps.
- **3.3 The Breakthrough Insight & Mathematical Invariant:**
  - **Monotonic Distance Preservation:** Avoid floating-point arithmetic by comparing $x^2 + y^2$.
  - **Max-Heap Eviction Invariant:** Any point evicted from the size-$k$ Max-Heap has distance $\ge$ all points remaining in the heap.
- **3.4 Cursor Semantics & Invariant Partition Architecture:**
  ```text
  Max-Heap Ceiling Filter (Size k):
  [ Evicted Points with dist >= Heap Ceiling ] | Heap: [ (Root = Farthest of Top-k) >= Closer Candidates ]
  ```
- **3.5 State Transition Triggers & Decision Gates:**
  - Enqueue with reversed priority: `maxHeap.Enqueue(pt, distSquared);` (using custom descending comparer).
  - Eviction Gate: `if (maxHeap.Count > k) maxHeap.Dequeue();`
- **3.6 Concrete Step-by-Step State Trace:**
  - Input: `points = [[1, 3], [-2, 2]]`, $k = 1$
  - Distances: $P_1(1, 3) \implies 1^2 + 3^2 = 10$; $P_2(-2, 2) \implies (-2)^2 + 2^2 = 8$.

  | Step | Point | Dist$^2$ | Heap Contents (Max at Root) | Action | Resulting Heap |
  | :--- | :--- | :--- | :--- | :--- | :--- |
  | 1 | `[1, 3]` | 10 | `[ [1, 3]: 10 ]` | Count $\le 1$ | `[ [1, 3]: 10 ]` |
  | 2 | `[-2, 2]` | 8 | `[ [1, 3]: 10, [-2, 2]: 8 ]` | Count > 1 $\implies$ Dequeue root `[1, 3]` | `[ [-2, 2]: 8 ]` |

  *Result:* `[-2, 2]` with distance 8.

### 4. Approach & Complexity Deconstruction
- **4.1 Anchor Points & Approach Selection Criteria:**
  - **Approach 1 (Max-Heap of Size $k$):** Best for online/streaming data where points arrive continuously and cannot be stored in an array.
  - **Approach 2 (In-Place Quickselect):** Optimal for batch offline datasets. $O(N)$ average time, $O(1)$ auxiliary space.
- **4.2 Step-by-Step Natural Progression Flow:**
  - *Step 1:* Initialize Max-Heap with custom descending integer comparer.
  - *Step 2:* Iterate over points, compute squared Euclidean distance.
  - *Step 3:* Enqueue; if count exceeds $k$, dequeue.
  - *Step 4:* Drain heap into output array.
- **4.3 Alternative Approaches Analysis:**
  - Full sort by distance: $O(N \log N)$ time, simple one-liner with `Array.Sort`, but wastes significant cycles on large $N$.
- **4.4 Multi-Dimensional Complexity & Trade-Off Matrix:**

| Approach | Time (Best/Avg) | Time (Worst) | Aux Space | In-Place Mutability | Streaming Friendly |
| :--- | :--- | :--- | :--- | :--- | :--- |
| **Max-Heap of Size $k$** | $O(N \log k)$ | $O(N \log k)$ | $O(k)$ | Read-Only | Optimal |
| **In-Place Quickselect** | $O(N)$ | $O(N^2)$ | $O(1)$ | Mutates Input | No |
| **Full Array Sort** | $O(N \log N)$ | $O(N \log N)$ | $O(1)$ or $O(N)$ | Mutates / Clones | No |

### 5. Production C# Implementations

```csharp
// ============================================================================
// ARCHITECTURE ANCHOR: K Closest Points to Origin
// Primary: Max-Heap of Size k (O(N log k) Time, O(k) Space, Streaming-Safe)
// Secondary: In-Place 3-Way Quickselect (O(N) Avg Time, O(1) Extra Space)
// Invariant: Max-Heap tracks k closest; root holds the largest distance among them
// ============================================================================

public class Solution
{
    /// <summary>
    /// Finds k closest points using a bounded Max-Heap of size k.
    /// Operates on squared Euclidean distance to eliminate floating-point square root costs.
    /// </summary>
    public int[][] KClosest(int[][] points, int k)
    {
        ArgumentNullException.ThrowIfNull(points);
        if (k <= 0 || k > points.Length) return Array.Empty<int[]>();

        // Custom reverse comparer creates a Max-Heap where largest distance is at the root
        var maxHeap = new PriorityQueue<int[], int>(
            Comparer<int>.Create((d1, d2) => d2.CompareTo(d1))
        );

        foreach (var pt in points)
        {
            // Squared Euclidean distance: Fits safely inside signed 32-bit int
            int distSquared = pt[0] * pt[0] + pt[1] * pt[1];
            maxHeap.Enqueue(pt, distSquared);

            // Invariant Gate: Bounded ceiling eviction
            if (maxHeap.Count > k)
            {
                maxHeap.Dequeue();
            }
        }

        int[][] result = new int[k][];
        for (int i = 0; i < k; i++)
        {
            result[i] = maxHeap.Dequeue();
        }

        return result;
    }
}

public class SolutionQuickselect
{
    /// <summary>
    /// Partitions array in-place so that points[0 .. k-1] contain the k closest points.
    /// </summary>
    public int[][] KClosest(int[][] points, int k)
    {
        ArgumentNullException.ThrowIfNull(points);
        Quickselect(points, 0, points.Length - 1, k);

        int[][] result = new int[k][];
        Array.Copy(points, result, k);
        return result;
    }

    private static void Quickselect(int[][] points, int left, int right, int k)
    {
        if (left >= right) return;

        int pivotIdx = Random.Shared.Next(left, right + 1);
        int pivotDist = Dist(points[pivotIdx]);

        int lt = left, gt = right, i = left;
        while (i <= gt)
        {
            int d = Dist(points[i]);
            if (d < pivotDist)
            {
                (points[lt], points[i]) = (points[i], points[lt]);
                lt++;
                i++;
            }
            else if (d > pivotDist)
            {
                (points[gt], points[i]) = (points[i], points[gt]);
                gt--;
            }
            else
            {
                i++;
            }
        }

        // Check if target k boundary is resolved
        if (k >= lt && k <= gt + 1)
        {
            return;
        }
        else if (k < lt)
        {
            Quickselect(points, left, lt - 1, k);
        }
        else
        {
            Quickselect(points, gt + 1, right, k);
        }
    }

    private static int Dist(int[] pt) => pt[0] * pt[0] + pt[1] * pt[1];
}
```

---

## 59. Find Median from Data Stream (LeetCode #295)

| Attribute | Specification |
| :--- | :--- |
| **Difficulty** | 🔴 Hard |
| **Priority** | ⭐ Core |
| **Pattern Tags** | `#two-heaps` `#min-max-heap-balance` `#streaming-median` `#dynamic-order-statistics` |
| **LeetCode Link** | [Find Median from Data Stream](https://leetcode.com/problems/find-median-from-data-stream/) |

### 1. Problem Detail & Constraints
- **Formal Statement:** Design a data structure that supports adding numbers from a continuous data stream and finding the running median of all elements received so far in $O(1)$ query time:
  - `void AddNum(int num)`: Adds an integer `num` to the data stream.
  - `double FindMedian()`: Returns the median of all elements so far.
- **Key Constraints:**
  - $-10^5 \le num \le 10^5$.
  - Up to $5 \times 10^4$ total calls to `AddNum` and `FindMedian`.
  - At least one element exists before `FindMedian` is called.
- **Senior Edge Cases to Defend:**
  - Even total count: Median is the arithmetic mean of the two middle elements, requiring `double` division `(a + b) / 2.0`.
  - Integer overflow: When adding two large numbers around $10^5$, sum does not overflow 32-bit `int`, but defensive casting to `(double)` avoids overflow in generic settings.
  - Streaming identical elements: Dual heaps must handle duplicates without balance skew.

### 2. Summary & Sample Input / Output
- **Conceptual Essence:** Split data symmetrically into two balanced halves: a **Max-Heap** holds the smaller half of numbers, and a **Min-Heap** holds the larger half. The median sits directly between their roots.
- **Sample 1:**
  - `AddNum(1); AddNum(2); FindMedian()` $\implies 1.5$
  - `AddNum(3); FindMedian()` $\implies 2.0$

### 3. Traversal Theory & Mental Model (State / Cursor Architecture)
- **3.1 The Intuitive Spark & Conceptual Metaphor:**
  - *The Balanced Hydraulic Seesaw:* Imagine two reservoirs meeting at a central observation point:
    - The left reservoir is capped with a **Max-Heap**; its highest peak is the largest number of the lower half.
    - The right reservoir is seated in a **Min-Heap**; its lowest floor is the smallest number of the upper half.
    - The two roots touch head-to-head at the center!
    - When a new number pours in, it must first be filtered through the left reservoir and bubble its maximum to the right reservoir. If the right reservoir overflows, its smallest element flows back to the left. The water level is maintained such that the left reservoir always holds either the exact same number of elements or exactly 1 more than the right.
- **3.2 The Naive Bottleneck & Redundant Computation:**
  - Maintaining a sorted list via insertion sort takes $O(N)$ time per insertion $\implies O(N^2)$ across $N$ operations.
  - Re-sorting on every median query takes $O(N \log N)$ per query.
  - Dual heaps reduce insertion to $O(\log N)$ and median lookup to $O(1)$.
- **3.3 The Breakthrough Insight & Mathematical Invariant:**
  - **Ordering Invariant:** Every element in the lower half is $\le$ every element in the upper half:
    $$\max(\text{MaxHeap}) \le \min(\text{MinHeap})$$
  - **Size Balance Invariant:**
    $$0 \le \text{MaxHeap.Count} - \text{MinHeap.Count} \le 1$$
  - **Deterministic Median Formula:**
    $$\text{Median} = \begin{cases} \text{MaxHeap.Peek()} & \text{if } \text{Total Count is Odd} \\ \frac{\text{MaxHeap.Peek()} + \text{MinHeap.Peek()}}{2.0} & \text{if } \text{Total Count is Even} \end{cases}$$
- **3.4 Cursor Semantics & Invariant Partition Architecture:**
  ```text
  Dual Balanced Heap Architecture:
  +--------------------------------+       +--------------------------------+
  |    Lower Half (Max-Heap)       |  <=   |    Upper Half (Min-Heap)       |
  |    Capacity: k or k + 1        |       |    Capacity: k                 |
  |    Peak Root: max(Lower Half)  |       |    Peak Root: min(Upper Half)  |
  +--------------------------------+       +--------------------------------+
                  \                               /
                   \----- [ Median Bridge ] -----/
  ```
- **3.5 State Transition Triggers & Decision Gates:**
  - Phase 1 (Insert): Push `num` into `maxHeap`.
  - Phase 2 (Order Invariant Restoration): Pop largest from `maxHeap` and push into `minHeap`.
  - Phase 3 (Size Invariant Restoration): If `minHeap.Count > maxHeap.Count`, pop smallest from `minHeap` and return to `maxHeap`.
- **3.6 Concrete Step-by-Step State Trace:**
  - Sequence: `AddNum(1)`, `AddNum(2)`, `FindMedian()`, `AddNum(3)`, `FindMedian()`

  | Step | Operation | Action Detail | Max-Heap State | Min-Heap State | Balance Check | Computed Median |
  | :--- | :--- | :--- | :--- | :--- | :--- | :--- |
  | 1 | `AddNum(1)` | Push 1 to Max $\to$ Pop 1 to Min $\to$ Balance: Pop 1 to Max | `[1]` (Count 1) | `[]` (Count 0) | $1 - 0 = 1$ (OK) | - |
  | 2 | `AddNum(2)` | Push 2 to Max $\to$ Pop 2 to Min | `[1]` (Count 1) | `[2]` (Count 1) | $1 - 1 = 0$ (OK) | - |
  | 3 | `FindMedian()`| Even count ($1 == 1$) | `[1]` (Root 1) | `[2]` (Root 2) | - | $(1 + 2) / 2.0 = \mathbf{1.5}$ |
  | 4 | `AddNum(3)` | Push 3 to Max $\to$ Pop 3 to Min $\to$ Balance: Min count (2) > Max (1) $\implies$ Pop 2 from Min to Max | `[2, 1]` (Count 2) | `[3]` (Count 1) | $2 - 1 = 1$ (OK) | - |
  | 5 | `FindMedian()`| Odd count ($2 > 1$) | `[2, 1]` (Root 2) | `[3]` (Root 3) | - | Max.Peek() = $\mathbf{2.0}$ |

### 4. Approach & Complexity Deconstruction
- **4.1 Anchor Points & Approach Selection Criteria:**
  - **Two Heaps (Standard Industry Solution):** $O(\log N)$ insertion, $O(1)$ query. Uses standard language priority queues without complex pointer-based balancing.
  - **Self-Balancing Binary Search Tree / AVL / Red-Black Tree:** Can track median via order statistics, but requires augmented tree nodes (`node.subtreeSize`) which is rarely available in standard libraries.
- **4.2 Step-by-Step Natural Progression Flow:**
  - *Step 1: Setup & Initialization:* Construct `maxHeap` with reverse comparer and `minHeap` with natural comparer.
  - *Step 2: AddNum Routine:* Route candidate through `maxHeap`, filter top into `minHeap`, rebalance counts.
  - *Step 3: FindMedian Routine:* Check parity of heap counts; return top or average.
- **4.3 Alternative Approaches Analysis:**
  - Binary Search insertion on `List<int>`: `BinarySearch` in $O(\log N)$ time, followed by `Insert` taking $O(N)$ shift time. Median query is $O(1)$, but total insertion time is $O(N^2)$.
- **4.4 Multi-Dimensional Complexity & Trade-Off Matrix:**

| Operation | Two Heaps | Insertion-Sorted List | Augmented Order-Statistic Tree |
| :--- | :--- | :--- | :--- |
| **AddNum Time** | $O(\log N)$ | $O(N)$ (due to array shifts) | $O(\log N)$ |
| **FindMedian Time** | $O(1)$ | $O(1)$ | $O(\log N)$ or $O(1)$ with pointer |
| **Auxiliary Space** | $O(N)$ | $O(N)$ | $O(N)$ |
| **Implementation Complexity** | Low (Standard library queues) | Low | High (Custom tree rotations) |

### 5. Production C# Implementations

```csharp
// ============================================================================
// ARCHITECTURE ANCHOR: Find Median from Data Stream
// Primary: Two Balanced Heaps (Max-Heap lower half, Min-Heap upper half)
// Invariants:
//   1. Ordering: MaxHeap.Peek() <= MinHeap.Peek()
//   2. Balance: 0 <= MaxHeap.Count - MinHeap.Count <= 1
// Complexity: AddNum: O(log N), FindMedian: O(1), Space: O(N)
// ============================================================================

public class MedianFinder
{
    // maxHeap stores the lower half of sorted numbers; root is the largest of the lower half
    private readonly PriorityQueue<int, int> _maxHeap;

    // minHeap stores the upper half of sorted numbers; root is the smallest of the upper half
    private readonly PriorityQueue<int, int> _minHeap;

    public MedianFinder()
    {
        // Reverse comparer creates Max-Heap behavior
        _maxHeap = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
        // Natural comparer creates Min-Heap behavior
        _minHeap = new PriorityQueue<int, int>();
    }

    /// <summary>
    /// Adds a number to the stream while strictly maintaining dual-heap ordering and size balance invariants.
    /// </summary>
    public void AddNum(int num)
    {
        // Phase 1: Tentatively insert candidate into the lower half
        _maxHeap.Enqueue(num, num);

        // Phase 2 (Order Invariant Gate):
        // Route the largest element of lower half into upper half to maintain: max(lower) <= min(upper)
        int largestLower = _maxHeap.Dequeue();
        _minHeap.Enqueue(largestLower, largestLower);

        // Phase 3 (Size Balance Invariant Gate):
        // Ensure MaxHeap size is either equal to MinHeap (even) or 1 greater (odd)
        if (_minHeap.Count > _maxHeap.Count)
        {
            int smallestUpper = _minHeap.Dequeue();
            _maxHeap.Enqueue(smallestUpper, smallestUpper);
        }
    }

    /// <summary>
    /// Computes the running median in O(1) time by querying heap roots.
    /// </summary>
    public double FindMedian()
    {
        if (_maxHeap.Count == 0)
        {
            throw new InvalidOperationException("Stream contains no elements.");
        }

        // Odd Parity Gate: Median is precisely the root of the lower half
        if (_maxHeap.Count > _minHeap.Count)
        {
            return _maxHeap.Peek();
        }

        // Even Parity Gate: Median is the arithmetic mean of both roots
        // Defensive double cast ensures floating point division without integer truncation
        return (_maxHeap.Peek() + (double)_minHeap.Peek()) / 2.0;
    }
}
```

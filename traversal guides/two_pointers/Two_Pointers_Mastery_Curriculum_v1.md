# Two Pointers Traversal Mastery

## 1. Summary Table

| Level | Mental Model | Pointer/State | Drill Problems |
| :--- | :--- | :--- | :--- |
| **L1: Converging Pointers** | Shrinking window from both ends. Move pointer pointing to worse element. | `left` (start), `right` (end) | [LeetCode 11: Container With Most Water] (Medium), [LeetCode 167: Two Sum II] (Medium) |
| **L2: Fast/Slow Pointers** | Cycle detection or finding middle. Fast moves 2x speed of slow. | `slow` (1x), `fast` (2x) | [LeetCode 141: Linked List Cycle] (Easy), [LeetCode 876: Middle of the Linked List] (Easy) |
| **L3: Partition Pointers** | Grouping elements into regions (e.g., zeros vs non-zeros). | `read` (current), `write` (boundary) | [LeetCode 283: Move Zeroes] (Easy), [LeetCode 75: Sort Colors] (Medium) |

## L1: Converging Pointers

### Mental Model & Invariants
- **What does the state/index mean?** `left` is the smallest candidate index; `right` is the largest candidate index.
- **What region is processed?** The region OUTSIDE `[left, right]` has been fully evaluated. The region INSIDE `[left, right]` is currently being considered.
- **What is the invariant?** The optimal answer for the subarray `[left, right]` combined with previously evaluated states contains the global optimal answer. The pointer that restricts the potential max/target is moved inwards.

### Visual State Trace
Example: Two Sum II (`target = 9`), Array: `[2, 7, 11, 15]`

| Step | `left` (Val) | `right` (Val) | Sum | Choice/Action | Invariant Status |
| :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | `0` (2) | `3` (15) | 17 | `17 > 9`, move `right--` | `[0, 2]` remains to be checked |
| 2 | `0` (2) | `2` (11) | 13 | `13 > 9`, move `right--` | `[0, 1]` remains to be checked |
| 3 | `0` (2) | `1` (7) | 9 | `9 == 9`, Return `[left+1, right+1]` | Target Found |

### Code Implementation

**Problem:** Given a 1-indexed sorted array, find two numbers that add up to a specific target and return their indices. (LeetCode 167: Two Sum II)

```python
def twoSum(numbers: list[int], target: int) -> list[int]:
    # 1. Initialize converging pointers at both ends of the sorted array
    left, right = 0, len(numbers) - 1
    
    # 2. Shrink the window while pointers haven't crossed
    while left < right:
        # 3. Compute the sum of the two candidates at the boundary
        curr_sum = numbers[left] + numbers[right]
        if curr_sum == target:
            # 4. Target found — return 1-indexed positions
            return [left + 1, right + 1]
        elif curr_sum < target:
            # 5. Sum too small — move left pointer inward to increase sum
            left += 1
        else:
            # 6. Sum too large — move right pointer inward to decrease sum
            right -= 1
            
    # 7. No valid pair found (shouldn't happen per problem guarantee)
    return []
```

```csharp
public int[] TwoSum(int[] numbers, int target) {
    // 1. Initialize converging pointers at both ends of the sorted array
    int left = 0, right = numbers.Length - 1;
    
    // 2. Shrink the window while pointers haven't crossed
    while (left < right) {
        // 3. Compute the sum of the two candidates at the boundary
        int sum = numbers[left] + numbers[right];
        // 4. Target found — return 1-indexed positions
        if (sum == target) return new int[] { left + 1, right + 1 };
        // 5. Sum too small — move left inward to increase sum
        if (sum < target) left++;
        // 6. Sum too large — move right inward to decrease sum
        else right--;
    }
    // 7. No valid pair found
    return new int[0];
}
```

### ⚠️ Gotchas & Pitfalls
- **Overlapping Pointers:** Using `while left <= right` when you need strictly distinct elements (e.g., Two Sum where you can't reuse the same element). Usually, it should be `while left < right`.
- **Skipping Duplicates:** In problems like 3Sum, failing to advance `left` and `right` past duplicate values leads to duplicate triplets in the result.

## L2: Fast/Slow Pointers

### Mental Model & Invariants
- **What does the state/index mean?** `slow` tracks the baseline progression. `fast` explores ahead at double the speed.
- **What region is processed?** `slow` has traversed half the distance of `fast`. 
- **What is the invariant?** Distance between `fast` and `slow` increases by 1 each step. If a cycle exists, they must eventually point to the same node. If no cycle, `fast` reaches the end.

### Visual State Trace
Example: Cycle Detection. Cycle exists at node 2. Nodes: `3 -> 2 -> 0 -> -4 -> (back to 2)`

| Step | `slow` Node | `fast` Node | Choice/Action | Invariant Status |
| :--- | :--- | :--- | :--- | :--- |
| 0 | 3 | 3 | Initial state | Distance = 0 |
| 1 | 2 | 0 | `slow = slow.next`, `fast = fast.next.next` | `slow` at pos 1, `fast` at pos 2 |
| 2 | 0 | 2 | `slow = slow.next`, `fast = fast.next.next` | `slow` at pos 2, `fast` at pos 4 |
| 3 | -4 | -4 | `slow = slow.next`, `fast = fast.next.next` | `slow == fast`, Cycle Detected |

### Code Implementation

**Problem:** Given the head of a linked list, determine if it contains a cycle (i.e., some node's next pointer points back to a previously visited node). (LeetCode 141: Linked List Cycle)

```python
class ListNode:
    def __init__(self, x):
        self.val = x
        self.next = None

def hasCycle(head: ListNode) -> bool:
    # 1. Initialize both pointers at the head (same starting position)
    slow, fast = head, head
    
    # 2. Advance while fast can take two steps (guards against null)
    while fast and fast.next:
        # 3. Move slow by 1 step (baseline progression)
        slow = slow.next
        # 4. Move fast by 2 steps (explorer at double speed)
        fast = fast.next.next
        # 5. If they meet, the gap closed — cycle exists
        if slow == fast:
            return True
            
    # 6. fast reached the end — no cycle in the list
    return False
```

```csharp
public class ListNode {
    public int val;
    public ListNode next;
    public ListNode(int x) { val = x; next = null; }
}

public bool HasCycle(ListNode head) {
    // 1. Initialize both pointers at the head (same starting position)
    ListNode slow = head, fast = head;
    
    // 2. Advance while fast can take two steps (guards against null)
    while (fast != null && fast.next != null) {
        // 3. Move slow by 1 step (baseline progression)
        slow = slow.next;
        // 4. Move fast by 2 steps (explorer at double speed)
        fast = fast.next.next;
        
        // 5. If they meet, the gap closed — cycle exists
        if (slow == fast) return true;
    }
    // 6. fast reached the end — no cycle in the list
    return false;
}
```

### ⚠️ Gotchas & Pitfalls
- **Null Reference on Fast:** Forgetting to check `fast.next != null` before advancing `fast.next.next`, which leads to Null Reference exceptions on odd-length or even-length lists.
- **Finding Cycle Start (Floyd's):** Resetting `slow` to `head` and advancing both at 1x speed to find the entry point, but forgetting that they must meet *before* returning.

## L3: Partition Pointers

### Mental Model & Invariants
- **What does the state/index mean?** `read` scans elements sequentially. `write` bounds the "processed and kept" region.
- **What region is processed?** `[0, write - 1]` contains valid/partitioned elements. `[write, read - 1]` contains garbage/elements to skip. `[read, end]` is unexplored.
- **What is the invariant?** Every element up to `write - 1` satisfies the partition condition. 

### Visual State Trace
Example: Move Zeroes, Array: `[0, 1, 0, 3, 12]`

| Step | `read` (Val) | `write` | Array State | Choice/Action | Invariant Status |
| :--- | :--- | :--- | :--- | :--- | :--- |
| 1 | `0` (0) | 0 | `[0, 1, 0, 3, 12]` | Val is 0, skip | `[0, -1]` is valid (empty) |
| 2 | `1` (1) | 0 | `[0, 1, 0, 3, 12]` | Swap `read`/`write`, `write++` | `[0, 0]` contains `[1]` |
| 3 | `2` (0) | 1 | `[1, 0, 0, 3, 12]` | Val is 0, skip | `[0, 0]` contains `[1]` |
| 4 | `3` (3) | 1 | `[1, 0, 0, 3, 12]` | Swap `read`/`write`, `write++` | `[0, 1]` contains `[1, 3]` |
| 5 | `4` (12)| 2 | `[1, 3, 0, 0, 12]` | Swap `read`/`write`, `write++` | `[0, 2]` contains `[1, 3, 12]` |

Final Array: `[1, 3, 12, 0, 0]`

### Code Implementation

**Problem:** Given an integer array, move all zeroes to the end while maintaining the relative order of the non-zero elements, in-place. (LeetCode 283: Move Zeroes)

```python
def moveZeroes(nums: list[int]) -> None:
    # 1. Initialize write pointer at the start of the "kept" region
    write = 0
    # 2. Scan every element with the read pointer
    for read in range(len(nums)):
        # 3. If current element is non-zero, it belongs in the kept region
        if nums[read] != 0:
            # 4. Swap read/write to place non-zero at the partition boundary
            nums[write], nums[read] = nums[read], nums[write]
            # 5. Advance write — the kept region grows by one
            write += 1
```

```csharp
public void MoveZeroes(int[] nums) {
    // 1. Initialize write pointer at the start of the "kept" region
    int write = 0;
    // 2. Scan every element with the read pointer
    for (int read = 0; read < nums.Length; read++) {
        // 3. If current element is non-zero, it belongs in the kept region
        if (nums[read] != 0) {
            // 4. Swap read/write to place non-zero at the partition boundary
            int temp = nums[write];
            nums[write] = nums[read];
            nums[read] = temp;
            // 5. Advance write — the kept region grows by one
            write++;
        }
    }
}
```

### ⚠️ Gotchas & Pitfalls
- **Overwriting Data:** Blindly assigning `nums[write] = nums[read]` without swapping can destroy data if elements after `write` need to be preserved (like the zeroes).
- **Multiple Partitions (Dutch National Flag):** When dealing with 3 regions (e.g., Sort Colors), using `curr <= right` is crucial. Also, do not advance `curr` when swapping with `right` because the swapped element from `right` is unexamined.

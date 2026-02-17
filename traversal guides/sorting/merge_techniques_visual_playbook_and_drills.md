# 🔀 Merge-Techniques Mastery Playbook (Visual + Practice)

> Theme: These techniques all use the same core traversal primitive: **two (or more) cursors moving forward** while maintaining a simple invariant: *everything already emitted is correct and final*.

---

## ✅ Summary list (what you will master)
1) Merge two sorted arrays/lists into one sorted output.
2) Union of two sorted arrays (skip duplicates).
3) Intersection of two sorted arrays (emit only on equality).
4) Merge two sorted runs (bottom-up merge sort building block).
5) K-way merge (merge many sorted lists using a priority queue).
6) Merge join (database-style join on sorted keys).
7) Merge intervals (sort then scan-and-merge).

---

# 0) The shared primitive (the “merge mindset”)

## Why
When inputs are sorted (or partially ordered), you can solve many problems in linear time by advancing the cursor that is “behind.”

## What
You maintain cursors and a rule:
- Compare the **front** items.
- Emit the correct next item.
- Advance the cursor you consumed.

## How (step-by-step flow)
1) Start with two cursors at the beginning.
2) Repeat until one input finishes:
   - Decide which side has the next smallest (or next needed) element.
   - Emit it.
   - Move that cursor forward.
3) Handle leftovers.

## Visual invariant (always true)
```
Left input:  [ ... already consumed ... | leftCursor -> next ... ]
Right input: [ ... already consumed ... | rightCursor -> next ... ]
Output:      [ ... final, sorted, done ... ]

Rule: Output is always correct and never needs to be revisited.
```

## Common gotchas (remember these)
- Each loop iteration must advance at least one cursor (otherwise infinite loop).
- Always decide what to do with equal values (ties) explicitly.
- Keep your range conventions consistent (inclusive vs exclusive end indices).

---

# 1) Merge two sorted arrays/lists

## For what
Combine two sorted sequences into one sorted sequence.

## Why
This is the combine step of merge sort and a common building block in real problems.

## Where
- Merge sort.
- Combining results from two sorted sources.

## How (step-by-step flow)
1) Create an output list.
2) Set `leftPosition = 0`, `rightPosition = 0`.
3) While both positions are within bounds:
   - If `leftValue <= rightValue`, append leftValue and move leftPosition.
   - Else append rightValue and move rightPosition.
4) Append remaining items from whichever side is not finished.

## Visual
```
Left:   [1, 4, 9]
         ^
Right:  [2, 3, 10]
         ^
Output: []

Step: pick smaller front item each time
```

## Guided walkthrough (index-by-index)
Left = [1, 4, 9]
Right = [2, 3, 10]

| Step | leftPosition | leftValue | rightPosition | rightValue | Pick | Output |
|---:|---:|---:|---:|---:|:---|:---|
| 1 | 0 | 1 | 0 | 2 | 1 | [1] |
| 2 | 1 | 4 | 0 | 2 | 2 | [1,2] |
| 3 | 1 | 4 | 1 | 3 | 3 | [1,2,3] |
| 4 | 1 | 4 | 2 | 10 | 4 | [1,2,3,4] |
| 5 | 2 | 9 | 2 | 10 | 9 | [1,2,3,4,9] |
| end | 3 | (done) | 2 | 10 | leftover | [1,2,3,4,9,10] |

## Edge cases ✅
- One side empty.
- Many duplicates: using `<=` chooses left on ties (stable behavior).

## Pitfalls
- Forgetting leftover append.

### C# code
```csharp
static int[] MergeTwoSortedArrays(int[] leftSorted, int[] rightSorted)
{
    leftSorted ??= Array.Empty<int>();
    rightSorted ??= Array.Empty<int>();

    int leftPosition = 0;
    int rightPosition = 0;

    int[] merged = new int[leftSorted.Length + rightSorted.Length];
    int writePosition = 0;

    while (leftPosition < leftSorted.Length && rightPosition < rightSorted.Length)
    {
        if (leftSorted[leftPosition] <= rightSorted[rightPosition])
        {
            merged[writePosition] = leftSorted[leftPosition];
            leftPosition++;
        }
        else
        {
            merged[writePosition] = rightSorted[rightPosition];
            rightPosition++;
        }

        writePosition++;
    }

    while (leftPosition < leftSorted.Length)
    {
        merged[writePosition] = leftSorted[leftPosition];
        leftPosition++;
        writePosition++;
    }

    while (rightPosition < rightSorted.Length)
    {
        merged[writePosition] = rightSorted[rightPosition];
        rightPosition++;
        writePosition++;
    }

    return merged;
}
```

### Python code
```python
def merge_two_sorted_lists(left_sorted, right_sorted):
    left_sorted = left_sorted or []
    right_sorted = right_sorted or []

    left_position = 0
    right_position = 0
    merged = []

    while left_position < len(left_sorted) and right_position < len(right_sorted):
        if left_sorted[left_position] <= right_sorted[right_position]:
            merged.append(left_sorted[left_position])
            left_position += 1
        else:
            merged.append(right_sorted[right_position])
            right_position += 1

    merged.extend(left_sorted[left_position:])
    merged.extend(right_sorted[right_position:])
    return merged
```

---

# 2) Union of two sorted arrays (skip duplicates)

## For what
Produce a sorted list of unique values that appear in either array.

## Visual rule
```
If leftValue == rightValue: emit once, move both.
If leftValue < rightValue: emit leftValue, move left.
If rightValue < leftValue: emit rightValue, move right.
Also: avoid emitting same value twice.
```

## Guided walkthrough
Left = [1, 2, 2, 4]
Right = [2, 2, 3, 4, 4]

Expected union = [1, 2, 3, 4]

| Step | leftValue | rightValue | Action | Output |
|---:|---:|---:|:---|:---|
| 1 | 1 | 2 | left smaller → emit 1 | [1] |
| 2 | 2 | 2 | equal → emit 2 once, move both past 2s | [1,2] |
| 3 | 4 | 3 | right smaller → emit 3 | [1,2,3] |
| 4 | 4 | 4 | equal → emit 4 once, finish | [1,2,3,4] |

## Edge cases ✅
- All values duplicated heavily.
- One list is fully contained in the other.

## Pitfalls
- Emitting duplicates because you forgot to “skip equal runs.”

### C# code
```csharp
static List<int> UnionOfTwoSortedArraysUnique(int[] leftSorted, int[] rightSorted)
{
    var result = new List<int>();
    leftSorted ??= Array.Empty<int>();
    rightSorted ??= Array.Empty<int>();

    int leftPosition = 0;
    int rightPosition = 0;

    void EmitIfNew(int value)
    {
        if (result.Count == 0 || result[result.Count - 1] != value)
            result.Add(value);
    }

    while (leftPosition < leftSorted.Length && rightPosition < rightSorted.Length)
    {
        int leftValue = leftSorted[leftPosition];
        int rightValue = rightSorted[rightPosition];

        if (leftValue == rightValue)
        {
            EmitIfNew(leftValue);

            while (leftPosition < leftSorted.Length && leftSorted[leftPosition] == leftValue) leftPosition++;
            while (rightPosition < rightSorted.Length && rightSorted[rightPosition] == rightValue) rightPosition++;
        }
        else if (leftValue < rightValue)
        {
            EmitIfNew(leftValue);
            while (leftPosition < leftSorted.Length && leftSorted[leftPosition] == leftValue) leftPosition++;
        }
        else
        {
            EmitIfNew(rightValue);
            while (rightPosition < rightSorted.Length && rightSorted[rightPosition] == rightValue) rightPosition++;
        }
    }

    while (leftPosition < leftSorted.Length)
    {
        int value = leftSorted[leftPosition];
        EmitIfNew(value);
        while (leftPosition < leftSorted.Length && leftSorted[leftPosition] == value) leftPosition++;
    }

    while (rightPosition < rightSorted.Length)
    {
        int value = rightSorted[rightPosition];
        EmitIfNew(value);
        while (rightPosition < rightSorted.Length && rightSorted[rightPosition] == value) rightPosition++;
    }

    return result;
}
```

### Python code
```python
def union_sorted_unique(left_sorted, right_sorted):
    left_sorted = left_sorted or []
    right_sorted = right_sorted or []

    left_position = 0
    right_position = 0
    result = []

    def emit_if_new(value):
        if not result or result[-1] != value:
            result.append(value)

    while left_position < len(left_sorted) and right_position < len(right_sorted):
        left_value = left_sorted[left_position]
        right_value = right_sorted[right_position]

        if left_value == right_value:
            emit_if_new(left_value)
            while left_position < len(left_sorted) and left_sorted[left_position] == left_value:
                left_position += 1
            while right_position < len(right_sorted) and right_sorted[right_position] == right_value:
                right_position += 1
        elif left_value < right_value:
            emit_if_new(left_value)
            while left_position < len(left_sorted) and left_sorted[left_position] == left_value:
                left_position += 1
        else:
            emit_if_new(right_value)
            while right_position < len(right_sorted) and right_sorted[right_position] == right_value:
                right_position += 1

    while left_position < len(left_sorted):
        value = left_sorted[left_position]
        emit_if_new(value)
        while left_position < len(left_sorted) and left_sorted[left_position] == value:
            left_position += 1

    while right_position < len(right_sorted):
        value = right_sorted[right_position]
        emit_if_new(value)
        while right_position < len(right_sorted) and right_sorted[right_position] == value:
            right_position += 1

    return result
```

---

# 3) Intersection of two sorted arrays

## For what
Produce a sorted list of values that appear in both arrays.

## Visual rule
```
If leftValue == rightValue: emit it (maybe once), move both.
If leftValue < rightValue: move left.
If rightValue < leftValue: move right.
```

## Guided walkthrough
Left = [1, 2, 2, 4, 6]
Right = [2, 2, 3, 4, 4, 7]

Expected intersection (unique) = [2, 4]

| Step | leftValue | rightValue | Move | Output |
|---:|---:|---:|:---|:---|
| 1 | 1 | 2 | left smaller → left forward | [] |
| 2 | 2 | 2 | equal → emit 2, skip runs | [2] |
| 3 | 4 | 3 | right smaller → right forward | [2] |
| 4 | 4 | 4 | equal → emit 4, skip runs | [2,4] |

## Edge cases ✅
- No overlap.
- One array fully contained in the other.

## Pitfalls
- Not skipping duplicates when you want unique intersection.

### C# code (unique intersection)
```csharp
static List<int> IntersectionOfTwoSortedArraysUnique(int[] leftSorted, int[] rightSorted)
{
    var result = new List<int>();
    leftSorted ??= Array.Empty<int>();
    rightSorted ??= Array.Empty<int>();

    int leftPosition = 0;
    int rightPosition = 0;

    while (leftPosition < leftSorted.Length && rightPosition < rightSorted.Length)
    {
        int leftValue = leftSorted[leftPosition];
        int rightValue = rightSorted[rightPosition];

        if (leftValue == rightValue)
        {
            if (result.Count == 0 || result[result.Count - 1] != leftValue)
                result.Add(leftValue);

            while (leftPosition < leftSorted.Length && leftSorted[leftPosition] == leftValue) leftPosition++;
            while (rightPosition < rightSorted.Length && rightSorted[rightPosition] == rightValue) rightPosition++;
        }
        else if (leftValue < rightValue)
        {
            leftPosition++;
        }
        else
        {
            rightPosition++;
        }
    }

    return result;
}
```

### Python code
```python
def intersection_sorted_unique(left_sorted, right_sorted):
    left_sorted = left_sorted or []
    right_sorted = right_sorted or []

    left_position = 0
    right_position = 0
    result = []

    while left_position < len(left_sorted) and right_position < len(right_sorted):
        left_value = left_sorted[left_position]
        right_value = right_sorted[right_position]

        if left_value == right_value:
            if not result or result[-1] != left_value:
                result.append(left_value)

            while left_position < len(left_sorted) and left_sorted[left_position] == left_value:
                left_position += 1
            while right_position < len(right_sorted) and right_sorted[right_position] == right_value:
                right_position += 1

        elif left_value < right_value:
            left_position += 1
        else:
            right_position += 1

    return result
```

---

# 4) Merge two sorted runs (bottom-up merge sort building block)

## For what
A “run” is a sorted segment inside a larger array. Bottom-up merge sort repeatedly merges runs of size 1, then 2, then 4, and so on.

## Visual idea
```
Run size = 1:
[4][1][3][2] -> merge pairs -> [1,4][2,3]

Run size = 2:
[1,4][2,3] -> merge -> [1,2,3,4]
```

## How (step-by-step flow)
1) Choose a run size.
2) Merge adjacent runs: `[start..middle]` with `[middle+1..end]`.
3) Double run size.
4) Repeat until run size >= array length.

## Edge cases ✅
- Last run may be smaller than run size.

## Pitfalls
- Wrong run boundaries for the last chunk.

### C# code (bottom-up merge sort)
```csharp
static void BottomUpMergeSort(int[] array)
{
    if (array == null || array.Length <= 1) return;

    int[] temporaryBuffer = new int[array.Length];

    for (int runSize = 1; runSize < array.Length; runSize *= 2)
    {
        for (int startIndex = 0; startIndex < array.Length; startIndex += 2 * runSize)
        {
            int middleIndex = Math.Min(startIndex + runSize - 1, array.Length - 1);
            int endIndex = Math.Min(startIndex + 2 * runSize - 1, array.Length - 1);

            if (middleIndex >= endIndex) continue; // no right run

            MergeRange(array, temporaryBuffer, startIndex, middleIndex, endIndex);
        }
    }
}

static void MergeRange(int[] array, int[] temporaryBuffer, int startIndexInclusive, int middleIndexInclusive, int endIndexInclusive)
{
    int leftHalfIndex = startIndexInclusive;
    int rightHalfIndex = middleIndexInclusive + 1;
    int writeIndex = startIndexInclusive;

    while (leftHalfIndex <= middleIndexInclusive && rightHalfIndex <= endIndexInclusive)
    {
        if (array[leftHalfIndex] <= array[rightHalfIndex])
            temporaryBuffer[writeIndex++] = array[leftHalfIndex++];
        else
            temporaryBuffer[writeIndex++] = array[rightHalfIndex++];
    }

    while (leftHalfIndex <= middleIndexInclusive)
        temporaryBuffer[writeIndex++] = array[leftHalfIndex++];

    while (rightHalfIndex <= endIndexInclusive)
        temporaryBuffer[writeIndex++] = array[rightHalfIndex++];

    for (int index = startIndexInclusive; index <= endIndexInclusive; index++)
        array[index] = temporaryBuffer[index];
}
```

### Python code (bottom-up merge sort)
```python
def bottom_up_merge_sort(array):
    if array is None or len(array) <= 1:
        return array if array is not None else []

    temporary_buffer = [0] * len(array)
    run_size = 1

    def merge_range(start_index_inclusive, middle_index_inclusive, end_index_inclusive):
        left_half_index = start_index_inclusive
        right_half_index = middle_index_inclusive + 1
        write_index = start_index_inclusive

        while left_half_index <= middle_index_inclusive and right_half_index <= end_index_inclusive:
            if array[left_half_index] <= array[right_half_index]:
                temporary_buffer[write_index] = array[left_half_index]
                left_half_index += 1
            else:
                temporary_buffer[write_index] = array[right_half_index]
                right_half_index += 1
            write_index += 1

        while left_half_index <= middle_index_inclusive:
            temporary_buffer[write_index] = array[left_half_index]
            left_half_index += 1
            write_index += 1

        while right_half_index <= end_index_inclusive:
            temporary_buffer[write_index] = array[right_half_index]
            right_half_index += 1
            write_index += 1

        for index in range(start_index_inclusive, end_index_inclusive + 1):
            array[index] = temporary_buffer[index]

    while run_size < len(array):
        start_index = 0
        while start_index < len(array):
            middle_index = min(start_index + run_size - 1, len(array) - 1)
            end_index = min(start_index + 2 * run_size - 1, len(array) - 1)
            if middle_index < end_index:
                merge_range(start_index, middle_index, end_index)
            start_index += 2 * run_size

        run_size *= 2

    return array
```

---

# 5) K-way merge (many sorted lists)

## For what
Merge many sorted lists into one sorted list.

## Why
Two-way merge repeated pairwise can work, but a priority queue selects the smallest current head efficiently.

## Visual idea
```
List 0 head -> 1
List 1 head -> 2
List 2 head -> 0

Priority queue always gives the smallest head.
After removing a head from a list, push that list's next element.
```

## Guided walkthrough
Lists:
- [1, 4, 9]
- [2, 6]
- [0, 7, 8]

Priority queue starts with (1 from list0), (2 from list1), (0 from list2).
Output progression: 0, 1, 2, 4, 6, 7, 8, 9

## Edge cases ✅
- Some lists empty.
- Only one list provided.

## Pitfalls
- Forgetting to push the next element after popping.

### C# code (K-way merge)
```csharp
using System.Collections.Generic;

static List<int> MergeManySortedLists(List<List<int>> sortedLists)
{
    var result = new List<int>();
    if (sortedLists == null || sortedLists.Count == 0) return result;

    var priorityQueue = new PriorityQueue<(int value, int listIndex, int elementIndex), int>();

    for (int listIndex = 0; listIndex < sortedLists.Count; listIndex++)
    {
        List<int> currentList = sortedLists[listIndex] ?? new List<int>();
        if (currentList.Count > 0)
            priorityQueue.Enqueue((currentList[0], listIndex, 0), currentList[0]);
    }

    while (priorityQueue.Count > 0)
    {
        var (value, listIndex, elementIndex) = priorityQueue.Dequeue();
        result.Add(value);

        int nextElementIndex = elementIndex + 1;
        List<int> list = sortedLists[listIndex] ?? new List<int>();
        if (nextElementIndex < list.Count)
        {
            int nextValue = list[nextElementIndex];
            priorityQueue.Enqueue((nextValue, listIndex, nextElementIndex), nextValue);
        }
    }

    return result;
}
```

### Python code (K-way merge)
```python
import heapq

def merge_many_sorted_lists(sorted_lists):
    sorted_lists = sorted_lists or []

    priority_queue = []
    result = []

    for list_index, current_list in enumerate(sorted_lists):
        current_list = current_list or []
        if current_list:
            heapq.heappush(priority_queue, (current_list[0], list_index, 0))

    while priority_queue:
        value, list_index, element_index = heapq.heappop(priority_queue)
        result.append(value)

        next_element_index = element_index + 1
        current_list = sorted_lists[list_index] or []
        if next_element_index < len(current_list):
            heapq.heappush(priority_queue, (current_list[next_element_index], list_index, next_element_index))

    return result
```

---

# 6) Merge join (database-style join)

## For what
Given two lists sorted by a key, produce pairs where keys match.

## Visual rule
```
If leftKey == rightKey: output matches and advance both (or handle duplicates carefully).
If leftKey < rightKey: advance left.
If rightKey < leftKey: advance right.
```

## Guided walkthrough (simple one-to-one keys)
Left records (key, value):
- (1, "A")
- (3, "B")
- (4, "C")

Right records (key, value):
- (2, "X")
- (3, "Y")
- (4, "Z")

Expected joined output:
- key 3: ("B", "Y")
- key 4: ("C", "Z")

| Step | leftKey | rightKey | Action | Output |
|---:|---:|---:|:---|:---|
| 1 | 1 | 2 | left smaller → left forward | [] |
| 2 | 3 | 2 | right smaller → right forward | [] |
| 3 | 3 | 3 | equal → emit join, advance both | [(3,B,Y)] |
| 4 | 4 | 4 | equal → emit join, advance both | [(3,B,Y),(4,C,Z)] |

## Edge cases ✅
- No matches.
- One side empty.
- Duplicate keys: one key can match many rows; requires nested emission.

## Pitfalls
- Duplicate keys are the real difficulty: you must join all combinations for that key.

### C# code (handles duplicates)
```csharp
static List<(int key, string leftValue, string rightValue)> MergeJoinOnKey(
    List<(int key, string value)> leftSortedByKey,
    List<(int key, string value)> rightSortedByKey)
{
    var result = new List<(int key, string leftValue, string rightValue)>();
    if (leftSortedByKey == null || rightSortedByKey == null) return result;

    int leftPosition = 0;
    int rightPosition = 0;

    while (leftPosition < leftSortedByKey.Count && rightPosition < rightSortedByKey.Count)
    {
        int leftKey = leftSortedByKey[leftPosition].key;
        int rightKey = rightSortedByKey[rightPosition].key;

        if (leftKey < rightKey)
        {
            leftPosition++;
        }
        else if (leftKey > rightKey)
        {
            rightPosition++;
        }
        else
        {
            // Collect runs with the same key on both sides
            int matchKey = leftKey;

            int leftRunStart = leftPosition;
            while (leftPosition < leftSortedByKey.Count && leftSortedByKey[leftPosition].key == matchKey)
                leftPosition++;

            int rightRunStart = rightPosition;
            while (rightPosition < rightSortedByKey.Count && rightSortedByKey[rightPosition].key == matchKey)
                rightPosition++;

            // Emit all combinations
            for (int leftRunIndex = leftRunStart; leftRunIndex < leftPosition; leftRunIndex++)
            {
                for (int rightRunIndex = rightRunStart; rightRunIndex < rightPosition; rightRunIndex++)
                {
                    result.Add((matchKey, leftSortedByKey[leftRunIndex].value, rightSortedByKey[rightRunIndex].value));
                }
            }
        }
    }

    return result;
}
```

### Python code (handles duplicates)
```python
def merge_join_on_key(left_sorted_by_key, right_sorted_by_key):
    left_sorted_by_key = left_sorted_by_key or []
    right_sorted_by_key = right_sorted_by_key or []

    result = []
    left_position = 0
    right_position = 0

    while left_position < len(left_sorted_by_key) and right_position < len(right_sorted_by_key):
        left_key, left_value = left_sorted_by_key[left_position]
        right_key, right_value = right_sorted_by_key[right_position]

        if left_key < right_key:
            left_position += 1
        elif left_key > right_key:
            right_position += 1
        else:
            match_key = left_key

            left_run_start = left_position
            while left_position < len(left_sorted_by_key) and left_sorted_by_key[left_position][0] == match_key:
                left_position += 1

            right_run_start = right_position
            while right_position < len(right_sorted_by_key) and right_sorted_by_key[right_position][0] == match_key:
                right_position += 1

            for left_run_index in range(left_run_start, left_position):
                for right_run_index in range(right_run_start, right_position):
                    result.append((match_key, left_sorted_by_key[left_run_index][1], right_sorted_by_key[right_run_index][1]))

    return result
```

---

# 7) Merge intervals

## For what
Given intervals (start, end), merge overlapping intervals.

## Why
Once intervals are sorted by start, a single scan can merge them.

## How (step-by-step flow)
1) Sort intervals by start.
2) Initialize `currentMergedInterval` as the first interval.
3) For each next interval:
   - If it overlaps (next.start <= current.end), extend current.end.
   - Else push current interval to result and start a new current interval.
4) Push the last current interval.

## Visual
```
Intervals after sorting:
[1,3]  [2,6]     [8,10] [9,12]
  \______/         \_____/
Merged:
[1,6]            [8,12]
```

## Guided walkthrough
Input intervals:
- (1, 3)
- (2, 6)
- (8, 10)
- (9, 12)

Sorted is the same.

| Step | currentMerged | nextInterval | Overlap? | Action | Result |
|---:|:---|:---|:---:|:---|:---|
| 1 | (1,3) | (2,6) | Yes | extend to (1,6) | [] |
| 2 | (1,6) | (8,10) | No | output (1,6), set current=(8,10) | [(1,6)] |
| 3 | (8,10) | (9,12) | Yes | extend to (8,12) | [(1,6)] |
| end | (8,12) | - | - | output last | [(1,6),(8,12)] |

## Edge cases ✅
- Empty input.
- Single interval.
- Fully nested intervals (example: (1,10) and (2,3)).

## Pitfalls
- Sorting by end instead of start.
- Not adding the last merged interval.

### C# code
```csharp
static List<(int start, int end)> MergeIntervals(List<(int start, int end)> intervals)
{
    var result = new List<(int start, int end)>();
    if (intervals == null || intervals.Count == 0) return result;

    intervals.Sort((a, b) => a.start != b.start ? a.start.CompareTo(b.start) : a.end.CompareTo(b.end));

    (int start, int end) currentMergedInterval = intervals[0];

    for (int index = 1; index < intervals.Count; index++)
    {
        (int start, int end) nextInterval = intervals[index];

        if (nextInterval.start <= currentMergedInterval.end)
        {
            currentMergedInterval.end = Math.Max(currentMergedInterval.end, nextInterval.end);
        }
        else
        {
            result.Add(currentMergedInterval);
            currentMergedInterval = nextInterval;
        }
    }

    result.Add(currentMergedInterval);
    return result;
}
```

### Python code
```python
def merge_intervals(intervals):
    intervals = intervals or []
    if not intervals:
        return []

    intervals.sort(key=lambda interval: (interval[0], interval[1]))

    result = []
    current_start, current_end = intervals[0]

    for next_start, next_end in intervals[1:]:
        if next_start <= current_end:
            current_end = max(current_end, next_end)
        else:
            result.append((current_start, current_end))
            current_start, current_end = next_start, next_end

    result.append((current_start, current_end))
    return result
```

---

# ✅ Visual playbook (one-page cheat sheet)

## A) Two-sorted merge
```
while both not finished:
  emit smaller front
  advance that cursor
append leftovers
```

## B) Union (unique)
```
if equal: emit once, advance both (skip runs)
else emit smaller, advance that side (skip runs)
append leftovers (skipping runs)
```

## C) Intersection (unique)
```
if equal: emit, advance both (skip runs)
else advance smaller side
```

## D) K-way merge
```
priority queue contains current head of each list
pop smallest head -> emit
push next element from the same list
```

## E) Merge join (handles duplicates)
```
when keys match:
  find run on left with that key
  find run on right with that key
  output all combinations
advance both beyond the runs
```

## F) Merge intervals
```
sort by start
keep current merged interval
if overlaps: extend
else: output current and reset
output last
```

---

# 🧪 Drill-down practice set (with expected outputs)

## Drill 1 — Merge two sorted lists
Implement: `merge_two_sorted_lists`
- Input: left = [1, 3, 7], right = [2, 4, 8]
- Expected output: [1, 2, 3, 4, 7, 8]

## Drill 2 — Merge with duplicates (stability awareness)
Implement: merge that chooses left on ties
- Input: left = [2, 2, 5], right = [2, 3]
- Expected output: [2, 2, 2, 3, 5]

## Drill 3 — Union of sorted lists (unique)
Implement: `union_sorted_unique`
- Input: left = [1, 2, 2, 2, 5], right = [2, 4, 4, 5]
- Expected output: [1, 2, 4, 5]

## Drill 4 — Intersection of sorted lists (unique)
Implement: `intersection_sorted_unique`
- Input: left = [1, 2, 2, 4, 6], right = [2, 2, 3, 4, 4]
- Expected output: [2, 4]

## Drill 5 — Merge intervals
Implement: `merge_intervals`
- Input: [(1, 4), (2, 3), (6, 9), (8, 10)]
- Expected output: [(1, 4), (6, 10)]

## Drill 6 — Merge join without duplicates
Implement: `merge_join_on_key`
- Input:
  - Left:  [(1,"A"), (3,"B"), (4,"C")]
  - Right: [(2,"X"), (3,"Y"), (4,"Z")]
- Expected output:
  - [(3,"B","Y"), (4,"C","Z")]

## Drill 7 — Merge join with duplicates (hard)
Implement: `merge_join_on_key` that outputs all combinations
- Input:
  - Left:  [(3,"B1"), (3,"B2"), (4,"C")]
  - Right: [(3,"Y1"), (3,"Y2"), (5,"W")]
- Expected output (order may vary, but must contain all 4 joins for key 3):
  - (3,"B1","Y1"), (3,"B1","Y2"), (3,"B2","Y1"), (3,"B2","Y2")

## Drill 8 — K-way merge
Implement: `merge_many_sorted_lists`
- Input: [[1, 4], [0, 2, 5], [3]]
- Expected output: [0, 1, 2, 3, 4, 5]

---

## How to self-check (visual debugging)
- Print cursor positions each iteration:
  - leftPosition, rightPosition, writePosition
- Print current “front” values before deciding.
- For union/intersection and join, print when you skip a run.

If you want, I can add a second drill section that focuses on **in-place merging** (merging into the first array when it has extra space), which is a very common interview variant.

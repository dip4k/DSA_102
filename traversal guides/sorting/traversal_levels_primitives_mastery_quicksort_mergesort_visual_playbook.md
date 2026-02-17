# 🧭 Traversal Levels + Primitives Mastery (Quicksort & Mergesort)

> Focus statement:
> - ⚡ **Quicksort** = divide-and-conquer + **in-place partition traversal**. [web:213]
> - 🧬 **Mergesort** = divide-and-conquer + **merge traversal** using two cursors. [web:221]
>
> Mastery approach:
> - 🧩 **Partition boundaries** (Traversal Levels 3 and 8)
> - 🔀 **Two-list merge traversal** (Traversal Level 3)
> - 🪟 **Range recursion over subarrays** (Traversal Levels 4 and 5)

---

## ✅ Summary list
- Level 1: Single cursor movement (safe scanning).
- Level 2: Index arithmetic (reverse, midpoint, boundary math).
- Level 3: Multi-cursor traversal (partition and merge pointers).
- Level 4: Range traversal over subarrays (how to operate on a segment).
- Level 5: Recursion as traversal (divide-and-conquer range tree).
- Level 8: Advanced in-place partition (three-way partition for duplicates).

At the end you get a **visual playbook**: quick diagrams + checklists you can reuse while coding.

---

# Level 1 — Single cursor movement 🚶

## Why
Quicksort and mergesort look complex, but they are built on simple scans where a single index moves forward while respecting boundaries.

## What
A single index variable walks through a sequence.

## How (step-by-step flow)
1) Decide what the index means: “current element I am examining.”
2) Decide where it starts.
3) Decide the stopping rule.
4) Ensure you move the index every iteration.

### Visual (forward scan)
```
Array:  [ 9, 3, 7, 1 ]
Index:    0  1  2  3
Cursor:   currentIndex = 0 -> 1 -> 2 -> 3 -> stop
Stop:     when currentIndex == array.Length
```

## Where
- The scan step inside partition.
- The scan step when copying leftover items during merge.

## Edge cases ✅
- Empty array: loop should run 0 times.
- Single element: loop may run once, but should not read outside bounds.

## Common pitfalls (gotchas)
- Off-by-one (`<=` instead of `<`).
- Infinite loop (cursor not advancing).

## Things to remember
- Never read `array[currentIndex]` unless `0 <= currentIndex < array.Length`.

### C# snippet (foundation scan)
```csharp
static int CountValuesLessThanThreshold(int[] array, int thresholdValue)
{
    if (array == null || array.Length == 0) return 0; // lenient

    int count = 0;
    for (int currentIndex = 0; currentIndex < array.Length; currentIndex++)
    {
        if (array[currentIndex] < thresholdValue) count++;
    }

    return count;
}
```

### Python snippet
```python
def count_values_less_than_threshold(array, threshold_value):
    if not array:
        return 0

    count = 0
    for current_index in range(len(array)):
        if array[current_index] < threshold_value:
            count += 1

    return count
```

---

# Level 2 — Index arithmetic 🧮

## Why
Sorting algorithms are “index math engines.” Most bugs come from incorrect boundary calculations.

## What
You must be comfortable with:
- Reverse scans.
- Computing a middle index.
- Converting between range styles.

We will use **inclusive range** notation in this guide:
- A range is `[startIndexInclusive .. endIndexInclusive]`.

## How (step-by-step flow)
1) Validate the range.
   - If `startIndexInclusive >= endIndexInclusive`, the range length is 0 or 1 (already sorted).
2) Compute the middle index safely.
   - `middleIndexInclusive = startIndexInclusive + (endIndexInclusive - startIndexInclusive) / 2`
3) Create left and right halves.
   - Left: `[startIndexInclusive .. middleIndexInclusive]`
   - Right: `[middleIndexInclusive + 1 .. endIndexInclusive]`

### Visual (range split)
```
[startIndexInclusive ........ middleIndexInclusive][middleIndexInclusive+1 ........ endIndexInclusive]
```

## Where
- Mergesort splits by middle index. [web:221]
- Quicksort recurses by pivot index boundaries. [web:213]

## Edge cases ✅
- `startIndexInclusive == endIndexInclusive`: one element.
- `startIndexInclusive > endIndexInclusive`: invalid range (treat as no-op in lenient style).

## Common pitfalls
- Mixing inclusive `[start..end]` with half-open `[start..endExclusive)` inside the same implementation.
- Middle index logic that fails to shrink ranges (causes infinite recursion).

## Things to remember
- Write your range convention at the top of the file and follow it everywhere.

### C# snippet (safe middle)
```csharp
static int ComputeMiddleIndexInclusive(int startIndexInclusive, int endIndexInclusive)
{
    return startIndexInclusive + (endIndexInclusive - startIndexInclusive) / 2;
}
```

### Python snippet
```python
def compute_middle_index_inclusive(start_index_inclusive, end_index_inclusive):
    return start_index_inclusive + (end_index_inclusive - start_index_inclusive) // 2
```

---

# Level 3 — Multi-cursor traversal 👀👀

## Why
This is where sorting becomes “real.” You must coordinate multiple pointers so progress is guaranteed and invariants stay true.

## What
Two mastery primitives live here:
- 🧩 Partition boundaries traversal (two or more indices)
- 🔀 Two-list merge traversal (two input cursors + one output cursor)

---

## Primitive 1 (Level 3): Two-region partition boundaries 🧩

### For what
This is the core traversal inside quicksort: rearrange a segment so values less than the pivot value come first.

### Why
Quicksort works because partition puts one element (the pivot) into its final position and separates the problem into smaller independent ranges. [web:213]

### What
We maintain:
- `scanIndex` (walks the segment)
- `nextSmallValueIndex` (the boundary: where the next “small” value should be placed)

### How (step-by-step flow)
1) Choose a pivot value (often last element for the simplest version).
2) Set `nextSmallValueIndex = startIndexInclusive`.
3) For each `scanIndex` from `startIndexInclusive` to `endIndexInclusive - 1`:
   - If `array[scanIndex] < pivotValue`, swap `array[scanIndex]` into `nextSmallValueIndex`, then increment `nextSmallValueIndex`.
4) Swap pivot value into `nextSmallValueIndex`.
5) Return `nextSmallValueIndex` as the pivot final index.

### Visual (regions + unknown)
```
| values less than pivot | values greater or equal | unknown | pivot |
 startIndexInclusive   nextSmallValueIndex      scanIndex    endIndexInclusive
```

### Edge cases ✅
- All values less than pivot: pivot ends at end (no change after final swap).
- No values less than pivot: pivot moves to start.
- Many duplicates equal pivot: duplicates go to the “greater or equal” side, which can create unbalanced recursion.

### Pitfalls and things to remember
- `scanIndex` must always move forward.
- `nextSmallValueIndex` moves only when you place a smaller value.
- Duplicates-heavy arrays often need the Level 8 three-way partition.

### C# snippet (two-region partition)
```csharp
static int PartitionIntoTwoRegions(int[] array, int startIndexInclusive, int endIndexInclusive)
{
    if (array == null || array.Length == 0) return startIndexInclusive; // lenient

    startIndexInclusive = Math.Max(startIndexInclusive, 0);
    endIndexInclusive = Math.Min(endIndexInclusive, array.Length - 1);

    if (startIndexInclusive >= endIndexInclusive) return startIndexInclusive;

    int pivotValue = array[endIndexInclusive];

    int nextSmallValueIndex = startIndexInclusive;

    for (int scanIndex = startIndexInclusive; scanIndex < endIndexInclusive; scanIndex++)
    {
        if (array[scanIndex] < pivotValue)
        {
            (array[nextSmallValueIndex], array[scanIndex]) = (array[scanIndex], array[nextSmallValueIndex]);
            nextSmallValueIndex++;
        }
    }

    (array[nextSmallValueIndex], array[endIndexInclusive]) = (array[endIndexInclusive], array[nextSmallValueIndex]);
    return nextSmallValueIndex;
}
```

### Python snippet
```python
def partition_into_two_regions(array, start_index_inclusive, end_index_inclusive):
    if not array:
        return start_index_inclusive

    start_index_inclusive = max(start_index_inclusive, 0)
    end_index_inclusive = min(end_index_inclusive, len(array) - 1)

    if start_index_inclusive >= end_index_inclusive:
        return start_index_inclusive

    pivot_value = array[end_index_inclusive]
    next_small_value_index = start_index_inclusive

    for scan_index in range(start_index_inclusive, end_index_inclusive):
        if array[scan_index] < pivot_value:
            array[next_small_value_index], array[scan_index] = array[scan_index], array[next_small_value_index]
            next_small_value_index += 1

    array[next_small_value_index], array[end_index_inclusive] = array[end_index_inclusive], array[next_small_value_index]
    return next_small_value_index
```

---

## Primitive 2 (Level 3): Two-list merge traversal 🔀

### For what
This is the core traversal inside mergesort: combine two sorted halves into one sorted range.

### Why
Mergesort works because merging two already-sorted halves is linear time and preserves sorted order. [web:221]

### What
We maintain:
- `leftHalfIndex` for the left range
- `rightHalfIndex` for the right range
- `writeIndex` for where we place the next output value

### How (step-by-step flow)
1) Compare current left value and current right value.
2) Copy the smaller into the temporary buffer.
3) Advance the cursor you consumed.
4) Advance the write index.
5) After one side finishes, copy the remainder from the other side.
6) Copy merged results back into the original array range.

### Visual
```
Left half:   [1, 4, 9]
Right half:  [2, 3, 10]
Merge into:  [1, 2, 3, 4, 9, 10]
              ^
          writeIndex
```

### Edge cases ✅
- One side empty: copy the other side.
- Duplicate values: choose left first on ties (`<=`) to keep merge stable.

### Pitfalls and things to remember
- Forgetting the final copy-back step is a common bug.
- Off-by-one boundaries (especially `middleIndexInclusive + 1`).

### C# snippet (merge two sorted ranges)
```csharp
static void MergeTwoSortedRanges(
    int[] array,
    int[] temporaryBuffer,
    int startIndexInclusive,
    int middleIndexInclusive,
    int endIndexInclusive)
{
    int leftHalfIndex = startIndexInclusive;
    int rightHalfIndex = middleIndexInclusive + 1;
    int writeIndex = startIndexInclusive;

    while (leftHalfIndex <= middleIndexInclusive && rightHalfIndex <= endIndexInclusive)
    {
        if (array[leftHalfIndex] <= array[rightHalfIndex])
        {
            temporaryBuffer[writeIndex] = array[leftHalfIndex];
            leftHalfIndex++;
        }
        else
        {
            temporaryBuffer[writeIndex] = array[rightHalfIndex];
            rightHalfIndex++;
        }

        writeIndex++;
    }

    while (leftHalfIndex <= middleIndexInclusive)
    {
        temporaryBuffer[writeIndex] = array[leftHalfIndex];
        leftHalfIndex++;
        writeIndex++;
    }

    while (rightHalfIndex <= endIndexInclusive)
    {
        temporaryBuffer[writeIndex] = array[rightHalfIndex];
        rightHalfIndex++;
        writeIndex++;
    }

    for (int currentIndex = startIndexInclusive; currentIndex <= endIndexInclusive; currentIndex++)
        array[currentIndex] = temporaryBuffer[currentIndex];
}
```

### Python snippet
```python
def merge_two_sorted_ranges(array, temporary_buffer, start_index_inclusive, middle_index_inclusive, end_index_inclusive):
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

    for current_index in range(start_index_inclusive, end_index_inclusive + 1):
        array[current_index] = temporary_buffer[current_index]
```

---

# Level 4 — Range traversal over subarrays 🪟

## Why
Both quicksort and mergesort repeatedly operate on **subranges**. If you cannot reason about a range, recursion will break.

## What
A “subarray range” is a slice of the array identified by two indices.

We continue using inclusive ranges:
- Range = `[startIndexInclusive .. endIndexInclusive]`

## How (step-by-step flow)
1) Validate:
   - If `startIndexInclusive >= endIndexInclusive`, stop.
2) Decide split method:
   - Mergesort: split by middle index.
   - Quicksort: split by pivot index returned by partition.
3) Ensure both new ranges are strictly smaller than the original.

### Visual (quicksort split)
```
Before: [startIndex .......... endIndex]
After partition: pivotIndex is fixed
Left:  [startIndex .. pivotIndex-1]
Right: [pivotIndex+1 .. endIndex]
```

### Visual (mergesort split)
```
[startIndex .. middleIndex] and [middleIndex+1 .. endIndex]
```

## Where
- Quicksort recursion boundaries.
- Mergesort left-half and right-half recursion boundaries.

## Edge cases ✅
- Pivot index is at the start or end (one side becomes empty).
- Middle index equals start index when range length is 2 (still fine; halves shrink).

## Pitfalls
- Not shrinking a range (example: calling recursion on the same boundaries).

## Things to remember
- Always confirm the range size decreases before recursing.

---

# Level 5 — Recursion as traversal 🧠

## Why
Divide-and-conquer sorting is a traversal over a tree of ranges (each recursive call is a node in that tree).

## What
Recursion is “automatic stack management” for exploring subranges.

## How (step-by-step flow)
### Mergesort recursion
1) If range length <= 1: return.
2) Recursively sort left half.
3) Recursively sort right half.
4) Merge results.

### Quicksort recursion
1) If range length <= 1: return.
2) Partition range; pivot is now in final position.
3) Recursively sort left range.
4) Recursively sort right range.

## Where
- Core structure of both algorithms. [web:213][web:221]

## Edge cases ✅
- Very large arrays: recursion depth matters.
- Quicksort worst case: bad pivot choices can cause deep recursion. [web:213]

## Pitfalls
- Forgetting the base case.
- Incorrect boundaries in recursive calls.

## Things to remember
- When debugging recursion, print the range boundaries on entry.

---

# Level 8 — Advanced in-place partition (three-way partition) 🧹

## Why
Two-region partition struggles with many duplicates because equal-to-pivot values keep recursing.

## What
Partition into three regions:
- values less than pivot
- values equal to pivot
- values greater than pivot

## How (step-by-step flow)
We maintain three indices:
- `lessThanRegionEndIndex`
- `scanIndex`
- `greaterThanRegionStartIndex`

Algorithm:
1) If current value is less than pivot: swap into the less-than region; advance both `lessThanRegionEndIndex` and `scanIndex`.
2) If current value equals pivot: advance `scanIndex`.
3) If current value is greater than pivot: swap into the greater-than region; decrement `greaterThanRegionStartIndex` (do not advance `scanIndex` yet).

### Visual
```
| less than pivot | equal to pivot | unknown........ | greater than pivot |
  start       lessThanEnd      scanIndex     greaterThanStart        end
```

## Edge cases ✅
- All equal values: equal region becomes whole range; recursion stops quickly.
- Many duplicates: huge performance improvement compared to two-region.

## Pitfalls
- Advancing scan index after swapping with the greater-than region (you will skip unknown values).

## C# snippet
```csharp
static (int equalRegionStartIndex, int equalRegionEndIndex) PartitionIntoThreeRegions(
    int[] array,
    int startIndexInclusive,
    int endIndexInclusive)
{
    if (array == null || array.Length == 0) return (startIndexInclusive, startIndexInclusive); // lenient

    startIndexInclusive = Math.Max(startIndexInclusive, 0);
    endIndexInclusive = Math.Min(endIndexInclusive, array.Length - 1);
    if (startIndexInclusive >= endIndexInclusive) return (startIndexInclusive, endIndexInclusive);

    int pivotValue = array[startIndexInclusive];

    int lessThanRegionEndIndex = startIndexInclusive;
    int scanIndex = startIndexInclusive;
    int greaterThanRegionStartIndex = endIndexInclusive;

    while (scanIndex <= greaterThanRegionStartIndex)
    {
        if (array[scanIndex] < pivotValue)
        {
            (array[lessThanRegionEndIndex], array[scanIndex]) = (array[scanIndex], array[lessThanRegionEndIndex]);
            lessThanRegionEndIndex++;
            scanIndex++;
        }
        else if (array[scanIndex] > pivotValue)
        {
            (array[scanIndex], array[greaterThanRegionStartIndex]) = (array[greaterThanRegionStartIndex], array[scanIndex]);
            greaterThanRegionStartIndex--; // scanIndex stays
        }
        else
        {
            scanIndex++;
        }
    }

    return (lessThanRegionEndIndex, greaterThanRegionStartIndex);
}
```

## Python snippet
```python
def partition_into_three_regions(array, start_index_inclusive, end_index_inclusive):
    if not array:
        return (start_index_inclusive, start_index_inclusive)

    start_index_inclusive = max(start_index_inclusive, 0)
    end_index_inclusive = min(end_index_inclusive, len(array) - 1)
    if start_index_inclusive >= end_index_inclusive:
        return (start_index_inclusive, end_index_inclusive)

    pivot_value = array[start_index_inclusive]

    less_than_region_end_index = start_index_inclusive
    scan_index = start_index_inclusive
    greater_than_region_start_index = end_index_inclusive

    while scan_index <= greater_than_region_start_index:
        if array[scan_index] < pivot_value:
            array[less_than_region_end_index], array[scan_index] = array[scan_index], array[less_than_region_end_index]
            less_than_region_end_index += 1
            scan_index += 1
        elif array[scan_index] > pivot_value:
            array[scan_index], array[greater_than_region_start_index] = array[greater_than_region_start_index], array[scan_index]
            greater_than_region_start_index -= 1
        else:
            scan_index += 1

    return (less_than_region_end_index, greater_than_region_start_index)
```

---

# Additional mastery topics you should know 🧰

## 1) Pivot selection strategy (quicksort)
### Why
Pivot choice affects recursion depth and performance; poor pivots can degrade quicksort badly. [web:213]
### How
- Random pivot.
- Median-of-three pivot (first, middle, last).
### Pitfall
- Always using the last element as pivot can be bad for already sorted inputs.

## 2) Temporary buffer reuse (mergesort)
### Why
Allocating new temporary arrays in every merge step is slow.
### How
- Allocate one temporary buffer of the same length as the input array and reuse it for all merges.

## 3) Stability and equality handling
### Why
If you need equal values to keep their original relative order, stability matters.
### How
- Mergesort merge with `lessThanOrEqual` (`<=`) keeps stability.
- Typical in-place quicksort is not stable.

## 4) Iterative versions (stack control)
### Why
Recursion can overflow for huge inputs.
### How
- Use an explicit stack of ranges for iterative quicksort.
- Use bottom-up mergesort for iterative merges.

---

# ✅ Visual playbook (print this while coding)

## A) Partition (two regions) quick diagram
```
Pivot = last value

| values less than pivot | values greater or equal | pivot |
 startIndex         nextSmallValueIndex     scanIndex  endIndex

If array[scanIndex] < pivot:
  swap(array[nextSmallValueIndex], array[scanIndex])
  nextSmallValueIndex++
scanIndex++ always

Finally:
  swap(array[nextSmallValueIndex], pivot)
  pivotIndex = nextSmallValueIndex
```

## B) Partition (three regions) quick diagram
```
Pivot = first value (example)

| less than | equal | unknown........ | greater than |
 start    lessThanEnd   scanIndex     greaterThanStart   end

If current < pivot: swap into less-than, lessThanEnd++, scanIndex++
If current = pivot: scanIndex++
If current > pivot: swap into greater-than, greaterThanStart-- (scanIndex stays)
```

## C) Merge traversal quick diagram
```
Left  range: [startIndex .. middleIndex]
Right range: [middleIndex+1 .. endIndex]

leftHalfIndex walks left
rightHalfIndex walks right
writeIndex writes output

Repeat:
  pick smaller front item
  advance the cursor you used
Copy leftovers
Copy temporary buffer back to array
```

## D) Range recursion checklist
- Range convention written at top (inclusive or half-open).
- Base case: if range length <= 1, stop.
- Every recursion call strictly reduces range size.

## E) Debug print pack
- Quicksort partition: print `startIndexInclusive, endIndexInclusive, pivotValue, scanIndex, nextSmallValueIndex`.
- Three-way partition: print `lessThanRegionEndIndex, scanIndex, greaterThanRegionStartIndex`.
- Merge: print `leftHalfIndex, rightHalfIndex, writeIndex`.
- Recursion: print ranges on function entry.

---

## Next step
If you want, I can add:
- A traced, index-by-index mergesort merge example on an actual array range `[startIndexInclusive..endIndexInclusive]`.
- A traced quicksort recursion tree (showing how ranges shrink) using the same variable names.

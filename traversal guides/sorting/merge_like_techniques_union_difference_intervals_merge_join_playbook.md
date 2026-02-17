# 🔀 Merge-Like Techniques Mastery (Union/Difference, Merge Intervals, Merge Join)

> These techniques are all variations of the same core idea: **traverse two ordered streams with two cursors** and make progress in linear time. [web:224][web:262]

---

## ✅ Summary list
- **Union and difference of two sorted arrays**: two cursors + duplicate handling (Traversal Level 3 + Level 2 discipline). [web:275][web:262]
- **Merge intervals**: sort by start, then linear scan that merges overlaps (Traversal Level 3 “anchor + cursor”). [web:269]
- **Merge join (sort-merge join)**: two sorted relations scanned together, producing matching pairs by key (Traversal Level 3 + “consume run of equal keys”). [web:266]

---

# 0) Shared traversal primitive (the mental model)

## Why
Once inputs are sorted, you should never use nested loops to compare every pair; you can scan with two cursors and move the smaller one. [web:262]

## What
Two cursors:
- `leftIndex` for the left sorted sequence
- `rightIndex` for the right sorted sequence

## How (step-by-step flow)
1) Compare `leftValue` and `rightValue`.
2) Decide what to output.
3) Advance at least one cursor.
4) Repeat until one side ends.

## Visual
```
Left:   [ 1, 4, 7, 10 ]
          ^ leftIndex
Right:  [ 2, 4, 9 ]
          ^ rightIndex

Decision: compare current front values and advance cursor(s)
```

## Things to remember
- Progress rule: every iteration must advance `leftIndex` or `rightIndex` (or both).
- Boundary rule: never read beyond the end.

---

# 1) Union and Difference (sorted arrays)

## For what
- **Union**: all unique values that appear in either array.
- **Difference (Left minus Right)**: values that appear in left array but not in right array.

## Why
If arrays are sorted, union and difference can be computed in linear time using two cursors, similar to the merge step in mergesort. [web:275][web:224]

## Where
- Deduplication pipelines.
- Set-like operations on sorted identifiers.
- Preprocessing for two-pointer interview problems.

## Traversal levels used
- **Traversal Level 3**: two-cursor scan.
- **Traversal Level 2**: duplicate skipping is index discipline (look at previous output or previous input value).

---

## 1.1 Union of two sorted arrays (unique)

### How (step-by-step flow)
1) Initialize `leftIndex = 0`, `rightIndex = 0`.
2) While both indices are in range:
   - If values are equal: emit one value, advance both.
   - If left value is smaller: emit left value, advance left.
   - If right value is smaller: emit right value, advance right.
3) Append remaining values from the unfinished side.
4) Remove duplicates while emitting (either skip duplicates in inputs, or skip duplicates in output).

### Edge cases ✅
- One array empty: union is the other array (unique).
- Duplicates in input arrays: must be handled to keep output unique.

### Pitfalls
- Forgetting to skip duplicates can produce repeated results.
- Skipping duplicates incorrectly can skip legitimate values.

---

### Guided trace (index-by-index)
Input:
- `leftSorted = [1, 2, 2, 4, 7]`
- `rightSorted = [2, 2, 3, 7, 8]`

We will emit unique values.

| Step | leftIndex | leftValue | rightIndex | rightValue | Decision | Output |
|---:|---:|---:|---:|---:|:---|:---|
| 1 | 0 | 1 | 0 | 2 | Left smaller → emit 1, leftIndex++ | [1] |
| 2 | 1 | 2 | 0 | 2 | Equal → emit 2, advance both | [1,2] |
| 3 | 2 | 2 | 1 | 2 | Equal again → would emit 2, but we skip output duplicate | [1,2] |
| 4 | 3 | 4 | 2 | 3 | Right smaller → emit 3, rightIndex++ | [1,2,3] |
| 5 | 3 | 4 | 3 | 7 | Left smaller → emit 4, leftIndex++ | [1,2,3,4] |
| 6 | 4 | 7 | 3 | 7 | Equal → emit 7, advance both | [1,2,3,4,7] |
| 7 | end | - | 4 | 8 | Left ended → emit remainder 8 | [1,2,3,4,7,8] |

---

### C# code (union unique)
```csharp
static List<int> UnionSortedUnique(int[] leftSorted, int[] rightSorted)
{
    var result = new List<int>();
    leftSorted ??= Array.Empty<int>();
    rightSorted ??= Array.Empty<int>();

    int leftIndex = 0;
    int rightIndex = 0;

    void EmitIfNew(int value)
    {
        if (result.Count == 0 || result[^1] != value)
            result.Add(value);
    }

    while (leftIndex < leftSorted.Length && rightIndex < rightSorted.Length)
    {
        int leftValue = leftSorted[leftIndex];
        int rightValue = rightSorted[rightIndex];

        if (leftValue == rightValue)
        {
            EmitIfNew(leftValue);
            leftIndex++;
            rightIndex++;
        }
        else if (leftValue < rightValue)
        {
            EmitIfNew(leftValue);
            leftIndex++;
        }
        else
        {
            EmitIfNew(rightValue);
            rightIndex++;
        }
    }

    while (leftIndex < leftSorted.Length) EmitIfNew(leftSorted[leftIndex++]);
    while (rightIndex < rightSorted.Length) EmitIfNew(rightSorted[rightIndex++]);

    return result;
}
```

### Python code (union unique)
```python
def union_sorted_unique(left_sorted, right_sorted):
    left_sorted = left_sorted or []
    right_sorted = right_sorted or []

    result = []
    left_index = 0
    right_index = 0

    def emit_if_new(value):
        if not result or result[-1] != value:
            result.append(value)

    while left_index < len(left_sorted) and right_index < len(right_sorted):
        left_value = left_sorted[left_index]
        right_value = right_sorted[right_index]

        if left_value == right_value:
            emit_if_new(left_value)
            left_index += 1
            right_index += 1
        elif left_value < right_value:
            emit_if_new(left_value)
            left_index += 1
        else:
            emit_if_new(right_value)
            right_index += 1

    while left_index < len(left_sorted):
        emit_if_new(left_sorted[left_index])
        left_index += 1

    while right_index < len(right_sorted):
        emit_if_new(right_sorted[right_index])
        right_index += 1

    return result
```

---

## 1.2 Difference (Left minus Right)

### How (step-by-step flow)
1) Walk both arrays with two cursors.
2) If values are equal: skip it (advance both).
3) If left value is smaller: keep it (emit it), advance left.
4) If right value is smaller: advance right (because right is “behind” on matching).
5) After right ends: emit remainder of left.

### Edge cases ✅
- Right array empty: difference is all unique left values.
- Left array empty: difference is empty.

### Pitfalls
- Not skipping duplicates in left can emit duplicates.

### C# code (difference unique)
```csharp
static List<int> DifferenceSortedUnique(int[] leftSorted, int[] rightSorted)
{
    var result = new List<int>();
    leftSorted ??= Array.Empty<int>();
    rightSorted ??= Array.Empty<int>();

    int leftIndex = 0;
    int rightIndex = 0;

    void EmitIfNew(int value)
    {
        if (result.Count == 0 || result[^1] != value)
            result.Add(value);
    }

    while (leftIndex < leftSorted.Length && rightIndex < rightSorted.Length)
    {
        int leftValue = leftSorted[leftIndex];
        int rightValue = rightSorted[rightIndex];

        if (leftValue == rightValue)
        {
            leftIndex++;
            rightIndex++;
        }
        else if (leftValue < rightValue)
        {
            EmitIfNew(leftValue);
            leftIndex++;
        }
        else
        {
            rightIndex++;
        }
    }

    while (leftIndex < leftSorted.Length)
        EmitIfNew(leftSorted[leftIndex++]);

    return result;
}
```

### Python code
```python
def difference_sorted_unique(left_sorted, right_sorted):
    left_sorted = left_sorted or []
    right_sorted = right_sorted or []

    result = []
    left_index = 0
    right_index = 0

    def emit_if_new(value):
        if not result or result[-1] != value:
            result.append(value)

    while left_index < len(left_sorted) and right_index < len(right_sorted):
        left_value = left_sorted[left_index]
        right_value = right_sorted[right_index]

        if left_value == right_value:
            left_index += 1
            right_index += 1
        elif left_value < right_value:
            emit_if_new(left_value)
            left_index += 1
        else:
            right_index += 1

    while left_index < len(left_sorted):
        emit_if_new(left_sorted[left_index])
        left_index += 1

    return result
```

---

# 2) Merge Intervals (overlap merge)

## For what
Given intervals like `[start, end]`, merge overlapping ones to produce a minimal set of non-overlapping intervals.

## Why
After sorting by start, a single forward scan can maintain a “current merged interval” and merge overlaps as you go. [web:269]

## Where
- Calendar scheduling.
- Range compression.
- Event timelines and log windows.

## Traversal levels used
- **Traversal Level 2**: sorting by start is a prerequisite.
- **Traversal Level 3**: scan with an anchor (current merged interval) + cursor (next interval).

---

## How (step-by-step flow)
1) Sort intervals by `start` (ascending). [web:269]
2) Initialize `mergedStart, mergedEnd` to the first interval.
3) For each next interval `(nextStart, nextEnd)`:
   - If `nextStart <= mergedEnd`: overlap, set `mergedEnd = max(mergedEnd, nextEnd)`.
   - Else: output the current merged interval and start a new merged interval.
4) Output the last merged interval.

### Visual
```
Sorted intervals:
[1,3] [2,6] [8,10] [15,18]

Merge scan:
current = [1,3]
see [2,6] overlaps -> current becomes [1,6]
see [8,10] no overlap -> output [1,6], current=[8,10]
...
```

## Edge cases ✅
- Empty input: output empty.
- One interval: output same.
- Touching endpoints: decide whether `[1,2]` and `[2,3]` overlap; most implementations treat them as overlapping when using `nextStart <= mergedEnd`.

## Pitfalls
- Not sorting first causes incorrect results.
- Confusing strict overlap (`<`) vs non-strict overlap (`<=`).

---

## Guided trace (table)
Input:
`intervals = [(1,3), (8,10), (2,6), (15,18)]`

Step 1: sort by start:
`[(1,3), (2,6), (8,10), (15,18)]` [web:269]

| Step | Next interval | Current merged | Overlaps? | Action | Output so far |
|---:|:---:|:---:|:---:|:---|:---|
| 0 | - | (1,3) | - | Initialize | [] |
| 1 | (2,6) | (1,3) | Yes | Extend end to 6 | [] |
| 2 | (8,10) | (1,6) | No | Output (1,6), start new (8,10) | [(1,6)] |
| 3 | (15,18) | (8,10) | No | Output (8,10), start new (15,18) | [(1,6),(8,10)] |
| end | - | (15,18) | - | Output final | [(1,6),(8,10),(15,18)] |

---

## C# code (merge intervals)
```csharp
static List<(int Start, int End)> MergeIntervals(List<(int Start, int End)> intervals)
{
    var result = new List<(int Start, int End)>();
    if (intervals == null || intervals.Count == 0) return result;

    intervals.Sort((a, b) => a.Start.CompareTo(b.Start));

    int mergedStart = intervals[0].Start;
    int mergedEnd = intervals[0].End;

    for (int index = 1; index < intervals.Count; index++)
    {
        int nextStart = intervals[index].Start;
        int nextEnd = intervals[index].End;

        if (nextStart <= mergedEnd)
        {
            mergedEnd = Math.Max(mergedEnd, nextEnd);
        }
        else
        {
            result.Add((mergedStart, mergedEnd));
            mergedStart = nextStart;
            mergedEnd = nextEnd;
        }
    }

    result.Add((mergedStart, mergedEnd));
    return result;
}
```

## Python code
```python
def merge_intervals(intervals):
    if not intervals:
        return []

    intervals = sorted(intervals, key=lambda pair: pair[0])

    merged = []
    merged_start, merged_end = intervals[0]

    for index in range(1, len(intervals)):
        next_start, next_end = intervals[index]

        if next_start <= merged_end:
            merged_end = max(merged_end, next_end)
        else:
            merged.append((merged_start, merged_end))
            merged_start, merged_end = next_start, next_end

    merged.append((merged_start, merged_end))
    return merged
```

---

# 3) Merge Join (Sort-Merge Join)

## For what
Join two collections of records by a shared key (for example, `UserId`), assuming both sides are sorted by that key.

## Why
If both inputs are sorted by join key, you can perform an efficient linear scan that groups equal keys together. [web:266]

## Where
- Database query execution (sort-merge join). [web:266]
- Data engineering pipelines: joining two sorted files.
- Synchronizing two ordered event streams.

## Traversal levels used
- **Traversal Level 3**: two-cursor scan.
- **Traversal Level 3 (run consumption variant):** when keys match, you often consume a whole run of equal keys on each side.

---

## How (step-by-step flow) — inner join
Assume:
- `leftRecords` sorted by `Key`
- `rightRecords` sorted by `Key`

Steps:
1) Initialize `leftIndex = 0`, `rightIndex = 0`.
2) While both indices are in range:
   - If `leftKey < rightKey`: advance left.
   - If `leftKey > rightKey`: advance right.
   - If keys are equal:
     1) Collect the run of records on the left with that key.
     2) Collect the run of records on the right with that key.
     3) Output all pairs (Cartesian product of the two runs).
     4) Advance both indices past their runs.

### Visual
```
Left keys:   1 1 2 4 4 5
             ^ leftIndex
Right keys:  1 3 4 4 6
             ^ rightIndex

When keys match (example key = 4):
- consume all left records with key 4
- consume all right records with key 4
- output all combinations
```

## Edge cases ✅
- One side empty: output empty.
- Keys that appear multiple times on one or both sides: must produce all matching pairs for that key.
- Keys exist only on one side: skipped in inner join.

## Pitfalls
- Not consuming runs: you will miss matches when duplicates exist.
- Output explosion: if key 10 has 1,000 records on both sides, output is 1,000,000 pairs.

---

## Guided trace (run-based)
Left:
- `(Key=1, Value=L1a)`, `(Key=1, Value=L1b)`, `(Key=2, Value=L2)`, `(Key=4, Value=L4a)`

Right:
- `(Key=1, Value=R1)`, `(Key=3, Value=R3)`, `(Key=4, Value=R4a)`, `(Key=4, Value=R4b)`

| Step | leftIndex key | rightIndex key | Relation | Action |
|---:|---:|---:|:---|:---|
| 1 | 1 | 1 | Equal | Consume left run key=1 (2 records) and right run key=1 (1 record), output 2×1 pairs, advance past runs |
| 2 | 2 | 3 | Left smaller | Advance leftIndex |
| 3 | 4 | 3 | Right smaller | Advance rightIndex |
| 4 | 4 | 4 | Equal | Consume left run key=4 (1 record) and right run key=4 (2 records), output 1×2 pairs, advance past runs |
| end | - | - | - | Stop |

---

## C# code (inner merge join)
```csharp
static List<(int Key, string LeftValue, string RightValue)> MergeJoinInner(
    List<(int Key, string Value)> leftRecords,
    List<(int Key, string Value)> rightRecords)
{
    var result = new List<(int Key, string LeftValue, string RightValue)>();

    if (leftRecords == null || rightRecords == null) return result;

    int leftIndex = 0;
    int rightIndex = 0;

    while (leftIndex < leftRecords.Count && rightIndex < rightRecords.Count)
    {
        int leftKey = leftRecords[leftIndex].Key;
        int rightKey = rightRecords[rightIndex].Key;

        if (leftKey < rightKey)
        {
            leftIndex++;
        }
        else if (leftKey > rightKey)
        {
            rightIndex++;
        }
        else
        {
            int matchingKey = leftKey;

            // Collect run on the left
            int leftRunStartIndex = leftIndex;
            while (leftIndex < leftRecords.Count && leftRecords[leftIndex].Key == matchingKey)
                leftIndex++;
            int leftRunEndExclusive = leftIndex;

            // Collect run on the right
            int rightRunStartIndex = rightIndex;
            while (rightIndex < rightRecords.Count && rightRecords[rightIndex].Key == matchingKey)
                rightIndex++;
            int rightRunEndExclusive = rightIndex;

            for (int leftRunIndex = leftRunStartIndex; leftRunIndex < leftRunEndExclusive; leftRunIndex++)
            {
                for (int rightRunIndex = rightRunStartIndex; rightRunIndex < rightRunEndExclusive; rightRunIndex++)
                {
                    result.Add((matchingKey, leftRecords[leftRunIndex].Value, rightRecords[rightRunIndex].Value));
                }
            }
        }
    }

    return result;
}
```

## Python code
```python
def merge_join_inner(left_records, right_records):
    # records: list of (key, value) sorted by key
    if left_records is None or right_records is None:
        return []

    result = []
    left_index = 0
    right_index = 0

    while left_index < len(left_records) and right_index < len(right_records):
        left_key, left_value = left_records[left_index]
        right_key, right_value = right_records[right_index]

        if left_key < right_key:
            left_index += 1
        elif left_key > right_key:
            right_index += 1
        else:
            matching_key = left_key

            left_run_start = left_index
            while left_index < len(left_records) and left_records[left_index][0] == matching_key:
                left_index += 1
            left_run_end = left_index

            right_run_start = right_index
            while right_index < len(right_records) and right_records[right_index][0] == matching_key:
                right_index += 1
            right_run_end = right_index

            for left_run_index in range(left_run_start, left_run_end):
                for right_run_index in range(right_run_start, right_run_end):
                    result.append((matching_key, left_records[left_run_index][1], right_records[right_run_index][1]))

    return result
```

---

# ✅ Visual playbook (quick reference)

## A) Union (unique) decision chart
```
If leftValue == rightValue:
  emit value once; advance left and right
If leftValue < rightValue:
  emit leftValue; advance left
If leftValue > rightValue:
  emit rightValue; advance right
Then append the remainder
Skip duplicates (either by skipping input runs or skipping output repeats)
```

## B) Difference (Left minus Right) decision chart
```
If leftValue == rightValue:
  advance both (do not emit)
If leftValue < rightValue:
  emit leftValue; advance left
If leftValue > rightValue:
  advance right
Then emit remainder of left
Skip duplicates if you want unique output
```

## C) Merge intervals scan chart
```
Sort by start
currentMerged = first interval
For each next interval:
  If nextStart <= currentMergedEnd: merge (extend end)
  Else: output currentMerged; set currentMerged = next interval
Output final currentMerged
```

## D) Merge join scan chart (inner join)
```
If leftKey < rightKey: advance left
If leftKey > rightKey: advance right
If equal:
  collect left run of this key
  collect right run of this key
  output all pairs (Cartesian product)
  advance both past the runs
```

---

# 🧩 Drill-down practice set (with increasing difficulty)

> Do not try to “memorize solutions.” For each drill, write down the traversal rule first: what makes a cursor move, and what gets emitted.

## Drills: Union and difference (sorted arrays)
1) Union unique: `leftSorted=[1,1,2,5]`, `rightSorted=[2,2,3,5,6]`.
2) Difference unique (left minus right): `leftSorted=[1,2,2,2,7,9]`, `rightSorted=[2,3,9]`.
3) Difference unique (right minus left): swap inputs from drill 2.
4) Union with negative numbers: `leftSorted=[-5,-1,0,0,2]`, `rightSorted=[-3,0,1]`.
5) Intersection unique (bonus, same pattern): `leftSorted=[1,2,2,3,5]`, `rightSorted=[2,2,4,5]`.
6) Multi-set union (advanced): output duplicates as many times as they appear in either input (requires counting runs).
7) Multi-set difference (advanced): output duplicates that remain after subtracting counts (requires counting runs).

## Drills: Merge intervals
1) Basic overlaps: `[(1,3),(2,6),(8,10),(15,18)]`.
2) Touching endpoints: `[(1,2),(2,3),(3,4)]` (decide overlap rule).
3) Fully nested: `[(1,10),(2,3),(4,8)]`.
4) Same start times: `[(1,4),(1,5),(6,7)]`.
5) Reverse order input: `[(8,10),(1,3),(2,6)]`.
6) Large ranges and negatives: `[(-10,-1),(-5,0),(1,2)]`.
7) Output also the number of merged intervals plus total covered length (advanced extension).

## Drills: Merge join
1) Inner join without duplicates:
   - Left:  `[(1,'a'),(2,'b'),(4,'c')]`
   - Right: `[(2,'x'),(3,'y'),(4,'z')]`
2) Inner join with duplicates on left:
   - Left:  `[(1,'a1'),(1,'a2'),(2,'b')]`
   - Right: `[(1,'x'),(2,'y')]`
3) Inner join with duplicates on both sides:
   - Left:  `[(4,'l1'),(4,'l2')]`
   - Right: `[(4,'r1'),(4,'r2'),(4,'r3')]`
4) Left join (advanced): include unmatched left keys with `RightValue=None`.
5) Full outer join (advanced): include unmatched from both sides.
6) Join on composite key (advanced): key is `(UserId, Date)`; ensure sorting and comparisons are consistent.
7) Streaming constraint (advanced): right side is a generator; keep memory usage bounded while still handling duplicate runs.

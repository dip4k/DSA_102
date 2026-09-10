# 🔀 Quicksort & Mergesort Mastery (Curriculum 2.0)

## 🗺️ Summary Table

| Level | Mental Model | Pointer State / Invariant | Drill Problems |
| :--- | :--- | :--- | :--- |
| **Level 8: In-place Partition (Quicksort)** | Array is split into processed regions (`< pivot` and `>= pivot`) and an unknown region. | `[lo..store-1]` are `< pivot`. `[store..j-1]` are `>= pivot`. `[j..hi-1]` is unknown. `pivot = a[hi]`. | 🟢 [LeetCode 283: Move Zeroes]<br>🟡 [LeetCode 215: Kth Largest Element in an Array]<br>🟡 [LeetCode 75: Sort Colors] (3-Way) |
| **Level 4/5: Range Recursion (Quicksort)** | Divide array around pivot index `p`. Base case: `lo >= hi`. | Function `sort(lo, hi)` recursively calls `sort(lo, p-1)` and `sort(p+1, hi)`. | 🟡 [LeetCode 912: Sort an Array] |
| **Level 3: Two-Cursor Merge (Mergesort)** | Two input cursors, one output cursor. Always pick the smaller element to maintain sorted order. | `a[i]` and `a[j]` compared. Output filled up to `k-1`. `<` or `<=` logic dictates stability. | 🟢 [LeetCode 88: Merge Sorted Array]<br>🟡 [LeetCode 56: Merge Intervals] |
| **Level 4/5: Divide & Conquer (Mergesort)** | Split exactly in half, recursively sort, then merge. | `mid = lo + (hi - lo) / 2`. `sort(lo, mid)` and `sort(mid+1, hi)`. | 🟡 [LeetCode 148: Sort List]<br>🔴 [LeetCode 23: Merge k Sorted Lists] |

## ⚡ 1. Quicksort: Partitioning & Recursion

**Mental Model:** Quicksort is fundamentally a **Level 8 In-place Partition** traversal. The recursion just drives this process on smaller and smaller subarrays.

### 🧹 Level 8: In-place Partition (Lomuto)

**What does the index mean?**
*   `j`: The scanning cursor exploring the "unknown" region.
*   `store`: The boundary cursor separating elements `< pivot` from elements `>= pivot`.

**The Invariant:**
At the start of step `j`:
1.  `[lo .. store-1]` contain elements strictly `< pivot`.
2.  `[store .. j-1]` contain elements `>= pivot`.
3.  `[j .. hi-1]` are unexplored.
4.  `a[hi]` is the fixed `pivot`.

**State Transitions (Visualized):**
Array: `a = [9, 3, 7, 1, 8, 2, 5]`, `pivot = 5` (at `hi=6`), `store = 0`.

| Step | Scan `j` | Val `a[j]` | Action/Invariant Maintained | Array State | Boundary `store` |
| :---: | :---: | :---: | :--- | :--- | :---: |
| Init | - | - | Unexplored: `[0..5]` | `[9, 3, 7, 1, 8, 2, 5]` | `0` |
| 0 | `0` | `9` | `9 >= 5`. Expand `>=` region. `j++`. | `[9, 3, 7, 1, 8, 2, 5]` | `0` |
| 1 | `1` | `3` | `3 < 5`. Swap `a[j]` with `a[store]`. `store++`. | `[3, 9, 7, 1, 8, 2, 5]` | `1` |
| 2 | `2` | `7` | `7 >= 5`. Expand `>=` region. `j++`. | `[3, 9, 7, 1, 8, 2, 5]` | `1` |
| 3 | `3` | `1` | `1 < 5`. Swap `a[j]` with `a[store]`. `store++`. | `[3, 1, 7, 9, 8, 2, 5]` | `2` |
| 4 | `4` | `8` | `8 >= 5`. Expand `>=` region. `j++`. | `[3, 1, 7, 9, 8, 2, 5]` | `2` |
| 5 | `5` | `2` | `2 < 5`. Swap `a[j]` with `a[store]`. `store++`. | `[3, 1, 2, 9, 8, 7, 5]` | `3` |
| Final| - | - | Swap `pivot` at `hi` with `a[store]`. | **`[3, 1, 2, 5, 8, 7, 9]`** | `3` |

### ⚠️ Gotchas & Pitfalls
* **Off-by-one error when R=N-1:** Iterating the scanning cursor `j` all the way to `hi` instead of `hi - 1`, which incorrectly includes the pivot in the comparison loop.
* **Missing the final swap:** Forgetting to swap the `pivot` at `a[hi]` with `a[store]` after the loop, leaving the pivot in the wrong place.
* **Empty input crashes:** Accessing `a[hi]` when the array is empty or `lo >= hi`, resulting in an index out of bounds error.

### 💻 Code Implementation

<details>
<summary><b>Python Implementation</b></summary>

Problem: Sort an array of integers in-place using the quicksort algorithm (Lomuto partition scheme).
```python
def quicksort(a, lo, hi):
    # 1. Base case: if range is 1 or empty, it's already sorted
    if lo >= hi:
        return
    
    # 2. Partition the array into '< pivot' and '>= pivot' regions
    p = partition(a, lo, hi)
    
    # 3. Recursively sort the left and right subarrays
    quicksort(a, lo, p - 1)
    quicksort(a, p + 1, hi)

def partition(a, lo, hi):
    # 1. Choose the rightmost element as the pivot
    pivot = a[hi]
    # 2. Initialize the boundary pointer for elements < pivot
    store = lo
    
    # 3. Scan through the unknown region with pointer j
    for j in range(lo, hi): 
        # 4. If current element belongs in the '< pivot' region
        if a[j] < pivot:
            # Swap it into the '< pivot' region and expand the boundary
            a[store], a[j] = a[j], a[store]
            store += 1
            
    # 5. Move pivot into its correct, finalized position
    a[store], a[hi] = a[hi], a[store]
    return store
```
</details>

<details>
<summary><b>C# Implementation</b></summary>

Problem: Sort an array of integers in-place using the quicksort algorithm (Lomuto partition scheme).
```csharp
public static void QuickSort(int[] a, int lo, int hi) {
    // 1. Base case: if range is 1 or empty, it's already sorted
    if (lo >= hi) return;
    
    // 2. Partition the array into '< pivot' and '>= pivot' regions
    int p = Partition(a, lo, hi);
    
    // 3. Recursively sort the left and right subarrays
    QuickSort(a, lo, p - 1);
    QuickSort(a, p + 1, hi);
}

private static int Partition(int[] a, int lo, int hi) {
    // 1. Choose the rightmost element as the pivot
    int pivot = a[hi];
    // 2. Initialize the boundary pointer for elements < pivot
    int store = lo;
    
    // 3. Scan through the unknown region with pointer j
    for (int j = lo; j < hi; j++) { 
        // 4. If current element belongs in the '< pivot' region
        if (a[j] < pivot) {
            // Swap it into the '< pivot' region and expand the boundary
            (a[store], a[j]) = (a[j], a[store]);
            store++;
        }
    }
    // 5. Move pivot into its correct, finalized position
    (a[store], a[hi]) = (a[hi], a[store]);
    return store;
}
```
</details>

### 🇳🇱 Extended Mental Model: 3-Way Partition
When dealing with duplicates, use a 3-way partition (Dutch National Flag).
**Invariant:**
*   `[0..lt-1]` are `< pivot`
*   `[lt..i-1]` are `== pivot`
*   `[i..gt]` is unknown
*   `[gt+1..N-1]` are `> pivot`

**Level 8 Drills:**
*   🟢 [LeetCode 283: Move Zeroes]
*   🟡 [LeetCode 75: Sort Colors]
*   🟡 [LeetCode 215: Kth Largest Element in an Array]
*   🟡 [LeetCode 912: Sort an Array]

---

## 🧬 2. Mergesort: Two-Cursor Merge & Divide

**Mental Model:** Mergesort is a **Level 3 Two-Cursor Traversal** combined with a **Level 4/5 Divide & Conquer** structure. 

### 🔀 Level 3: Merge Traversal

**What does the index mean?**
*   `i`: Cursor for the remaining unexplored region in the sorted Left half.
*   `j`: Cursor for the remaining unexplored region in the sorted Right half.
*   `k`: Cursor for writing the next element into the output buffer.

**The Invariant:**
At any step:
1.  The output buffer `temp[lo..k-1]` contains the sorted combination of elements drawn from `Left[lo..i-1]` and `Right[mid+1..j-1]`.
2.  `Left[i..mid]` and `Right[j..hi]` remain unexplored.
3.  Stability dictates: if `a[i] == a[j]`, pick `a[i]` to preserve relative order.

**State Transitions (Visualized):**
Left: `[1, 4, 9]`, Right: `[2, 3, 10]`

| Step | `i` (Left) | `j` (Right) | Action/Invariant Maintained | Output `temp[lo..k]` Buffer |
| :---: | :---: | :---: | :--- | :--- |
| 1 | `0` (val `1`) | `0` (val `2`) | `1 <= 2`. Pick Left. `i++`, `k++`. | `[1]` |
| 2 | `1` (val `4`) | `0` (val `2`) | `2 < 4`. Pick Right. `j++`, `k++`. | `[1, 2]` |
| 3 | `1` (val `4`) | `1` (val `3`) | `3 < 4`. Pick Right. `j++`, `k++`. | `[1, 2, 3]` |
| 4 | `1` (val `4`) | `2` (val `10`) | `4 <= 10`. Pick Left. `i++`, `k++`. | `[1, 2, 3, 4]` |
| 5 | `2` (val `9`) | `2` (val `10`) | `9 <= 10`. Pick Left. `i++`, `k++`. | `[1, 2, 3, 4, 9]` |
| 6 | Exhausted | `2` (val `10`) | Left exhausted. Drain Right. | `[1, 2, 3, 4, 9, 10]` |

### ⚠️ Gotchas & Pitfalls
* **Integer overflow on L+R/2:** Calculating `mid = (lo + hi) / 2` can overflow if the array is massive. Always use `lo + (hi - lo) / 2`.
* **Breaking Stability:** Using `a[i] < a[j]` instead of `a[i] <= a[j]`. If you pick the right side on equality, you lose the stable sorting property!
* **Writeback Misalignment:** Forgetting that `temp` needs to be copied back to `a` exactly from `lo` to `hi`, not `0` to `len`.

### 💻 Code Implementation

<details>
<summary><b>Python Implementation</b></summary>

Problem: Sort an array of integers by dividing it in half, sorting the halves recursively, and merging them using a temporary buffer.
```python
def mergesort(a):
    # 1. Single buffer allocated once for O(N) space
    temp = [0] * len(a)
    
    def sort(lo, hi):
        # 2. Base Case: if range has 1 or fewer elements, it's sorted
        if lo >= hi: return
        
        # 3. Divide: find the midpoint without overflow
        mid = lo + (hi - lo) // 2
        # 4. Conquer: recursively sort left and right halves
        sort(lo, mid)
        sort(mid + 1, hi)
        # 5. Combine: merge the two sorted halves back together
        merge(lo, mid, hi)
        
    def merge(lo, mid, hi):
        # 1. Initialize cursors for left half (i), right half (j), and output buffer (k)
        i, j, k = lo, mid + 1, lo
        
        # 2. Compare elements from both halves while neither is exhausted
        while i <= mid and j <= hi:
            # 3. Maintain stability: if equal, pick from the left half
            if a[i] <= a[j]:
                temp[k] = a[i]
                i += 1
            else:
                temp[k] = a[j]
                j += 1
            k += 1
            
        # 4. Drain remaining unexplored elements from the left half (if any)
        while i <= mid: 
            temp[k] = a[i]
            i, k = i + 1, k + 1
            
        # 5. Drain remaining unexplored elements from the right half (if any)
        while j <= hi: 
            temp[k] = a[j]
            j, k = j + 1, k + 1
            
        # 6. Writeback phase: copy sorted elements from buffer back to original array
        for t in range(lo, hi + 1):
            a[t] = temp[t]
            
    sort(0, len(a) - 1)
```
</details>

<details>
<summary><b>C# Implementation</b></summary>

Problem: Sort an array of integers by dividing it in half, sorting the halves recursively, and merging them using a temporary buffer.
```csharp
public static void MergeSort(int[] a) {
    // 1. Single buffer allocated once for O(N) space
    int[] temp = new int[a.Length];
    Sort(a, temp, 0, a.Length - 1);
}

private static void Sort(int[] a, int[] temp, int lo, int hi) {
    // 2. Base Case: if range has 1 or fewer elements, it's sorted
    if (lo >= hi) return;
    
    // 3. Divide: find the midpoint without overflow
    int mid = lo + (hi - lo) / 2;
    // 4. Conquer: recursively sort left and right halves
    Sort(a, temp, lo, mid);
    Sort(a, temp, mid + 1, hi);
    // 5. Combine: merge the two sorted halves back together
    Merge(a, temp, lo, mid, hi);
}

private static void Merge(int[] a, int[] temp, int lo, int mid, int hi) {
    // 1. Initialize cursors for left half (i), right half (j), and output buffer (k)
    int i = lo, j = mid + 1, k = lo;
    
    // 2. Compare elements from both halves while neither is exhausted
    while (i <= mid && j <= hi) {
        // 3. Maintain stability: if equal, pick from the left half
        if (a[i] <= a[j]) temp[k++] = a[i++];
        else temp[k++] = a[j++];
    }
    
    // 4. Drain remaining unexplored elements from the left half (if any)
    while (i <= mid) temp[k++] = a[i++];
    // 5. Drain remaining unexplored elements from the right half (if any)
    while (j <= hi) temp[k++] = a[j++];
    
    // 6. Writeback phase: copy sorted elements from buffer back to original array
    for (int t = lo; t <= hi; t++) {
        a[t] = temp[t];
    }
}
```
</details>

**Level 3 & 4/5 Drills:**
*   🟢 [LeetCode 88: Merge Sorted Array]
*   🟡 [LeetCode 148: Sort List]
*   🟡 [LeetCode 56: Merge Intervals]
*   🔴 [LeetCode 23: Merge k Sorted Lists]

---

## 🧠 Quick Comparison & Tradeoffs

| Feature | ⚡ Quicksort | 🧬 Mergesort |
| :--- | :--- | :--- |
| **Time Complexity** | `O(N log N)` avg, `O(N²)` worst | `O(N log N)` guaranteed |
| **Space Complexity** | `O(log N)` recursion stack | `O(N)` auxiliary buffer array |
| **Stability** | ❌ Not stable (equal elements mix) | ✅ Stable (preserves equal order) |
| **Best Used For...** | In-memory general purpose sorting | Linked lists, external sorting, guaranteed bounds |

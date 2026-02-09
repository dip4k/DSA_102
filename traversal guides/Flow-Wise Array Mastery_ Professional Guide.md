# **🗺️ Flow-Wise Array Mastery Curriculum (Professional Grade)**

**Target Audience:** Developers transitioning into DSA mastery.

**Core Principle:** Build intuition by increasing **Complexity of State**.

* **Level 1:** One index (Physical Movement)  
* **Level 2:** One index \+ Math (Logical Mapping)  
* **Level 3:** Two indices (Dual States/Pointers)  
* **Level 4:** Window State (Ranges/Prefixes/Deques)  
* **Level 5:** Abstract Boundaries (Partitioning/Search Spaces)

# ---

**🟢 Level 1: The Physical Layer (Basic Movement)**

**Goal:** Muscle memory for accessing memory efficiently and safely.

## **1A) 🔁 Mastering the Loop: Forward, Backward, and Strides**

### **1A.1 ➡️ Forward Iteration**

**Why:** This is the default mode of CPU pre-fetching. It optimizes cache locality and is the standard for scanning, searching, or counting.

**What:** Visiting indices linearly from 0 to N-1.

**How:** Initialize index at 0; process element; increment index; stop when index equals length.

**Where:** Finding Maximum/Minimum, Linear Search, Copying arrays.

**When:** The result depends on the *current* or *past* elements, but never strictly on *future* elements (unless read-only).

**Visual:**

Index: 0 1 2 3 4

Array: \[A\]\[C\]\[E\]

^

i moves \--\>

**Python Snippet:**

Python

\# Standard forward scan  
for i in range(len(arr)):  
    current \= arr\[i\]  
    \# Process current

**C\# Snippet:**

C\#

for (int i \= 0; i \< arr.Length; i++) {  
    int current \= arr\[i\];  
    // Process current  
}

**Common Pitfalls:**

* ❌ Accessing i+1 without checking i \< N-1.  
* ❌ Modifying the list (adding/removing) while iterating forward (skips elements).

### ---

**1A.2 ⬅️ Backward Iteration**

**Why:** To prevent "Overwrite Corruption". If you write to index i+1 using the value at i in a forward loop, you overwrite the old i+1 before reading it. Backward iteration avoids this.

**What:** Visiting indices from N-1 down to 0\.

**How:** Initialize at N-1; process; decrement; stop when index \< 0\.

**Where:** Right-shifting elements, Stack-based problems (Next Greater Element from right), Dynamic Programming (optimizing space).

**When:** The computation for index i depends on the *original* value of i+1 or greater.

**Visual:**

Index: 0 1 2 3

Array: \[A\]\[C\]

^

i moves \<--

**Python Snippet:**

Python

\# range(start, stop, step) \-\> stop is exclusive (-1)  
for i in range(len(arr) \- 1, \-1, \-1):  
    current \= arr\[i\]

**C\# Snippet:**

C\#

for (int i \= arr.Length \- 1; i \>= 0; i--) {  
    int current \= arr\[i\];  
}

**Tips & Tricks:**

* 💡 **Memory Trick:** If you are "pushing" data to the right, iterate backwards. If "pulling" to the left, iterate forwards.

## ---

**1B) 📏 Boundary Control (Inclusive vs. Exclusive)**

**Why:** Off-by-one errors are the most common bug in array manipulation. A mental standard eliminates this cognitive load.

**What:** Adopting the **Half-Open Interval** standard \`)

\*\*C\# Snippet:\*\*  
\`\`\`csharp  
void ProcessRange(int arr, int L, int R) {  
    for (int i \= L; i \< R; i++) {  
        Console.WriteLine(arr\[i\]);  
    }  
}

**Common Pitfalls:**

* ❌ Using \<= R when R is the count/length.  
* ❌ Calculating length as R \- L \+ 1 when using exclusive bounds (that formula is for inclusive).

## ---

**1C) 🧩 Edge Case Handling (Empty & Single)**

**Why:** Algorithms that assume "neighbors exist" (like arr\[i+1\]) crash on small inputs.

**What:** Guard clauses at the very top of the function.

**How:** Check if n \== 0 return specific value. Check if n \== 1 if the logic requires pairs.

**Where:** Recursion base cases, Merge Sort, Sliding Window.

**When:** Before entering the main loop.

**Python Snippet:**

Python

def solve(arr):  
    if not arr: return 0      \# Empty  
    if len(arr) \== 1: return arr \# Single  
      
    \# Main logic assuming n \>= 2  
    return arr \+ arr\[1\]

**C\# Snippet:**

C\#

public int Solve(int arr) {  
    if (arr \== null |

| arr.Length \== 0) return 0;  
    if (arr.Length \== 1) return arr;  
      
    // Main logic  
    return arr \+ arr\[1\];  
}

## ---

**1D) 🧵 Simultaneous Iteration (Lockstep)**

**Why:** Comparing or merging two arrays requires moving through both at the same pace.

**What:** Using a single index i to access A\[i\] and B\[i\].

**How:** Determine the safe limit (usually min(len(A), len(B))) to avoid bounds errors.

**Where:** Vector addition, Zip operations, Hamming Distance.

**When:** Data is aligned by index.

**Visual:** A: 2 B: \[ 5, 5, 5\] ^ ^ i (lockstep)

**Python Snippet:**

Python

\# Zip is the pythonic "Lockstep"  
names \=  
scores \= 

for name, score in zip(names, scores):  
    print(f"{name}: {score}")

**C\# Snippet:**

C\#

int A \= {1, 2, 3};  
int B \= {4, 5, 6};  
int limit \= Math.Min(A.Length, B.Length);

for (int i \= 0; i \< limit; i++) {  
    Console.WriteLine(A\[i\] \+ B\[i\]);  
}

# ---

**🔵 Level 2: The Logical Layer (Index Arithmetic)**

**Goal:** Treat indices as numbers to create virtual structures (Circles, Grids).

## **2A) 🔄 Cyclic / Modular Arithmetic**

**Why:** To simulate circular buffers or handle rotations without physically moving data.

**What:** Using the modulo operator % to wrap an index back to the start.

**How:**

* **Forward:** (i \+ 1\) % N  
* **Backward:** (i \- 1 \+ N) % N (The \+N handles negative results in C\#/Java).  
  **Where:** Round-robin schedulers, Hash Table probing, Rotating arrays.  
  **When:** The problem mentions "circular", "wrap around", or "infinite repetition".

**Visual:**

Arr: (N=4)

i=3 (D). Next?

(3 \+ 1\) % 4 \= 0 (A) \-\> Wraps to start

**Python Snippet (Rotate Array Logic):**

Python

def get\_rotated\_index(i, k, n):  
    return (i \+ k) % n

**C\# Snippet:**

C\#

int GetPrevIndex(int i, int n) {  
    // Adding n ensures the result is positive before modulo  
    return (i \- 1 \+ n) % n;  
}

## ---

**2B) 🧱 2D-to-1D Mapping (Flattening)**

**Why:** Memory is physically 1D. Treat a matrix as a single sorted list for Binary Search.

**What:** Mathematical formulas to convert (row, col) ![][image1] index.

**How:**

* Index \= row \* width \+ col  
* Row \= Index / width (Integer Division)  
* Col \= Index % width (Modulo)  
  **Where:** Image processing, Matrix Binary Search, Serialization.  
  **When:** You need to flatten a grid or treat a grid as a linear sequence.

**Visual:** Matrix 2x3 (Width=3) Row 0 5 Row 1

(1, 0\) \-\> 1\*3 \+ 0 \= Index 3

**C\# Snippet (Matrix Binary Search):**

C\#

// Treat matrix as sorted array of length M\*N  
int left \= 0, right \= m \* n \- 1;  
while (left \<= right) {  
    int mid \= left \+ (right \- left) / 2;  
    int r \= mid / n; // logical row  
    int c \= mid % n; // logical col  
    if (matrix\[r\]\[c\] \== target) return true;  
    //... adjust bounds  
}

# ---

**🟠 Level 3: The Multi-View Layer (Dual Pointers)**

**Goal:** Manage two independent states to optimize time complexity from ![][image2] to ![][image3].

## **3A) 🧹 Reader/Writer (Overwrite / Compaction)**

**Why:** To modify an array **in-place** (remove duplicates, filter items) without allocating extra memory.

**What:** Two pointers moving in the same direction.

* **Reader (fast):** Scans for valid items.  
* **Writer (slow):** Indicates the position for the next valid item.  
  **How:** Loop reader from 0 to N. If arr\[reader\] is valid, copy it to arr\[writer\] and increment writer.  
  **Where:** Remove Duplicates, Move Zeroes, Remove Element.  
  **When:** The problem asks for "in-place" modification or "constant space".

**Visual:**

\[ 1, 0, 2, 0, 3 \]

W R (R finds 0, skip)

\[ 1, 0, 2, 0, 3 \]

W R (R finds 2, write 2 to W, W++, R++)

\[ 1, 2, 2, 0, 3 \]

W R

**Python Snippet (Move Zeroes):**

Python

def move\_zeroes(nums):  
    writer \= 0  
    for reader in range(len(nums)):  
        if nums\[reader\]\!= 0:  
            \# Found non-zero, write it  
            nums\[writer\], nums\[reader\] \= nums\[reader\], nums\[writer\]  
            writer \+= 1

**Common Pitfalls:**

* ❌ Forgetting to zero-out the rest of the array (if required).  
* ❌ Overwriting data that hasn't been read yet (rare in this pattern, but possible if R \< W).

## ---

**3B) 🦀 Converging Pointers (Pincer)**

**Why:** To find a pair of elements that satisfy a condition in a **sorted** array.

**What:** Left starts at 0, Right starts at N-1. They move inward.

**How:**

* Evaluate condition (e.g., Sum).  
* If Sum \< Target: Need larger value \-\> Left++.  
* If Sum \> Target: Need smaller value \-\> Right--.  
  **Where:** Two Sum II (Sorted), Container With Most Water, Valid Palindrome.  
  **When:** The array is **sorted** or the problem has a symmetric "center" logic.

**Visual:**

\[ 1, 3, 5, 8 \] Target=8

L R (1+8=9 \> 8\) \-\> R--

L R (1+5=6 \< 8\) \-\> L++

L R (3+5=8) \-\> Found

**C\# Snippet:**

C\#

public int TwoSum(int numbers, int target) {  
    int left \= 0, right \= numbers.Length \- 1;  
    while (left \< right) {  
        int sum \= numbers\[left\] \+ numbers\[right\];  
        if (sum \== target) return new int { left \+ 1, right \+ 1 };  
        else if (sum \< target) left++;  
        else right--;  
    }  
    return new int;  
}

**Common Pitfalls:**

* ❌ Using this on unsorted arrays (fails).  
* ❌ Using left \<= right when the answer involves two *distinct* indices.

## ---

**3C) 🇳🇱 3-Way Partition (Dutch National Flag)**

**Why:** To sort elements into three categories (Low, Mid, High) in a single pass ![][image3].

**What:** Three pointers: Low (end of 0s), Mid (current scanner), High (start of 2s).

**How:**

* If arr\[mid\] \== 0: Swap low and mid. Increment low, mid.  
* If arr\[mid\] \== 1: Increment mid (it's in correct place).  
* If arr\[mid\] \== 2: Swap mid and high. Decrement high. **Do NOT increment mid** (swapped value is unknown).  
  **Where:** Sort Colors, Quicksort partition.  
  **When:** Sorting items with small range of keys (0, 1, 2).

**Visual:**

\[ 0, 0, 1, 1,?,?, 2, 2 \]

L M H

**Python Snippet:**

Python

def sort\_colors(nums):  
    low, mid, high \= 0, 0, len(nums) \- 1  
    while mid \<= high:  
        if nums\[mid\] \== 0:  
            nums\[low\], nums\[mid\] \= nums\[mid\], nums\[low\]  
            low \+= 1  
            mid \+= 1  
        elif nums\[mid\] \== 1:  
            mid \+= 1  
        else: \# nums\[mid\] \== 2  
            nums\[mid\], nums\[high\] \= nums\[high\], nums\[mid\]  
            high \-= 1   
            \# Mid not incremented\!

# ---

**🟣 Level 4: The Range Layer (Sub-problems)**

**Goal:** Process groups of data efficiently using window state.

## **4A) 🪗 Variable Sliding Window (Accordion)**

**Why:** To find the longest/shortest subarray satisfying a condition in ![][image3] instead of ![][image2].

**What:** A window \`\` that expands to find a valid range and shrinks to optimize it.

**How (Standard Template):**

1. **Expand** Right pointer to include new data.  
2. **While** Condition is invalid (or valid for min-length problems):  
   * **Shrink** Left pointer (remove data).  
   * Update Global Max/Min.  
     **Where:** Longest Substring Without Repeating Characters, Max Consecutive Ones, Minimum Size Subarray Sum.  
     **When:** The condition is monotonic (adding elements always makes it "more" valid or "less" valid).

**Visual:**

\[ 3, 1, 2, 5, 1 \] Target Sum \>= 7

\[ 3, 1, 2, 5 \] Sum=11 (Valid). Optimize?

Shrink L: \[ 1, 2, 5 \] Sum=8 (Valid). Optimize?

Shrink L: \[ 2, 5 \] Sum=7 (Valid). Optimize?

**Python Snippet (Longest Substring No Repeats):**

Python

def length\_of\_longest\_substring(s):  
    seen \= set()  
    left \= 0  
    max\_len \= 0  
      
    for right in range(len(s)):  
        \# Shrink window if duplicate found  
        while s\[right\] in seen:  
            seen.remove(s\[left\])  
            left \+= 1  
              
        \# Expand window  
        seen.add(s\[right\])  
        max\_len \= max(max\_len, right \- left \+ 1)  
          
    return max\_len

**Common Pitfalls:**

* ❌ Updating the result in the wrong place (inside vs. outside the while loop).  
* ❌ Forgetting to update the window state (sums, counts) when shrinking.

## ---

**4B) 🧾 Prefix Sums (Range Query Optimization)**

**Why:** To calculate the sum of any subarray \`\` in ![][image4] time.

**What:** An auxiliary array P where P\[i\] is the sum of arr\[0...i-1\].

**How:** P\[i\] \= P\[i-1\] \+ arr\[i-1\].

**Formula:** Sum(L, R) \= P \- P\[L\].

**Where:** Range Sum Query (Immutable), Splitting array into equal sums.

**When:** You have static data and many range sum queries.

**Visual:** Arr: \[ 3, 1, 4, 2 \] Pref: \[ 0, 3, 4, 8, 10 \] (Size N+1) Sum(1, 2\) \-\> "1, 4" \-\> Pref5 \- Pref1 \= 8 \- 3 \= 5

**C\# Snippet:**

C\#

public class NumArray {  
    private int prefix;  
    public NumArray(int nums) {  
        prefix \= new int\[nums.Length \+ 1\];  
        for (int i \= 0; i \< nums.Length; i++) {  
            prefix\[i \+ 1\] \= prefix\[i\] \+ nums\[i\];  
        }  
    }  
    public int SumRange(int left, int right) {  
        return prefix\[right \+ 1\] \- prefix\[left\];  
    }  
}

## ---

**4C) 🧠 Prefix Sum \+ HashMap (Subarray Sum Equals K)**

**Why:** Variable Sliding Window fails with **negative numbers**. Prefix Sum \+ Map handles them.

**What:** If Prefix\[j\] \- Prefix\[i\] \= K, then Prefix\[j\] \- K \= Prefix\[i\]. We store Prefix\[i\] in a map to look it up later.

**How:**

1. Accumulate current\_sum.  
2. Check if (current\_sum \- K) exists in Map.  
3. Add current\_sum to Map.  
   **Where:** Subarray Sum Equals K, Contiguous Array (0s and 1s).  
   **When:** Finding subarrays with specific sum, especially with negative numbers.

**Python Snippet:**

Python

def subarray\_sum(nums, k):  
    count \= 0  
    curr\_sum \= 0  
    \# Map: {prefix\_sum : count\_of\_occurrence}  
    \# Initialize with 0:1 to handle subarrays starting from index 0  
    prefix\_map \= {0: 1}  
      
    for num in nums:  
        curr\_sum \+= num  
        \# Do we have a prefix we can chop off?  
        if (curr\_sum \- k) in prefix\_map:  
            count \+= prefix\_map\[curr\_sum \- k\]  
              
        prefix\_map\[curr\_sum\] \= prefix\_map.get(curr\_sum, 0) \+ 1  
          
    return count

# ---

**🔴 Level 5: The Abstract Layer (Search & Partition)**

**Goal:** Treat the array as a search space. Discard data without reading it.

## **5A) 🔍 Binary Search (Classic Templates)**

**Why:** Search in ![][image5] is vastly superior to ![][image3].

**What:** Halving the search space based on a comparison.

**How:** Three distinct templates based on boundary rules.

### **Template 1: Exact Match**

Used when searching for a specific value.

Python

left, right \= 0, len(nums) \- 1  
while left \<= right:  
    mid \= left \+ (right \- left) // 2  
    if nums\[mid\] \== target: return mid  
    elif nums\[mid\] \< target: left \= mid \+ 1  
    else: right \= mid \- 1  
return \-1

### **Template 2: Lower Bound (First Occurrence / Insertion Point)**

Used when duplicates exist, or finding where a value *should* go.

**Invariant:** Left will point to the first value \>= target.

Python

left, right \= 0, len(nums) \# Note: Right is N, not N-1  
while left \< right:  
    mid \= left \+ (right \- left) // 2  
    if nums\[mid\] \>= target:  
        right \= mid \# Answer is to the left (or here)  
    else:  
        left \= mid \+ 1 \# Answer must be to the right  
return left

## ---

**5B) 🧪 Binary Search on Answer Space**

**Why:** Sometimes the "Array" is implicit. We search for a **Value** (Speed, Capacity, Time) that satisfies a condition.

**What:** Searching the range \[Min\_Possible, Max\_Possible\] using a check(val) function.

**How:**

* If check(mid) is valid: Try to optimize (smaller/larger).  
* If check(mid) is invalid: Move to valid side.  
  **Where:** Koko Eating Bananas, Capacity to Ship Packages, Aggressive Cows.  
  **When:** The problem asks to "Minimize the Maximum" or "Maximize the Minimum", and the condition is Monotonic (if 5 works, 6, 7, 8... also work).

**Visual:**

Capacity: 1 2 3 4 5 6 7 8

Valid? F F F T T T T T

^

Find boundary (First T)

**Python Snippet (Koko Eating Bananas):**

Python

def min\_eating\_speed(piles, h):  
    def can\_finish(k):  
        hours \= 0  
        for p in piles:  
            hours \+= (p \+ k \- 1) // k \# Ceiling div  
        return hours \<= h

    left, right \= 1, max(piles)  
      
    while left \< right:  
        mid \= left \+ (right \- left) // 2  
        if can\_finish(mid):  
            right \= mid \# Valid, try smaller speed  
        else:  
            left \= mid \+ 1 \# Too slow, need speed  
              
    return left

**Tips & Tricks:**

* 💡 **Optimization vs Feasibility:** If the problem asks for "Minimum X", and check(mid) is True, right \= mid (keep mid as potential answer). If check(mid) is False, left \= mid \+ 1\.

#### **Works cited**

1. Arrays & Pointers \- CS 3410 \- Cornell: Computer Science, accessed February 9, 2026, [https://www.cs.cornell.edu/courses/cs3410/2024fa/notes/pointer.html](https://www.cs.cornell.edu/courses/cs3410/2024fa/notes/pointer.html)  
2. The Dutch National Flag Algorithm: Efficient Sorting in Three Categories | by Jyotsna, accessed February 9, 2026, [https://medium.com/@jyotsnay24/the-dutch-national-flag-algorithm-efficient-sorting-in-three-categories-5aabe8a35d61](https://medium.com/@jyotsnay24/the-dutch-national-flag-algorithm-efficient-sorting-in-three-categories-5aabe8a35d61)  
3. 304\. Range Sum Query 2D \- Immutable \- In-Depth Explanation, accessed February 9, 2026, [https://algo.monster/liteproblems/304](https://algo.monster/liteproblems/304)  
4. Rotate Array in Java (Left & Right) Using Reverse Algorithm ..., accessed February 9, 2026, [https://dev.to/devcorner/rotate-array-in-java-left-right-using-reverse-algorithm-clean-code-explained-58go](https://dev.to/devcorner/rotate-array-in-java-left-right-using-reverse-algorithm-clean-code-explained-58go)  
5. Array cheatsheet for coding interviews \- Tech Interview Handbook, accessed February 9, 2026, [https://www.techinterviewhandbook.org/algorithms/array/](https://www.techinterviewhandbook.org/algorithms/array/)  
6. Rotate Array Problem: A Deep Dive into a Fundamental Coding Challenge \- Medium, accessed February 9, 2026, [https://medium.com/@brandon93.w/rotate-array-problem-a-deep-dive-into-a-fundamental-coding-challenge-600b915734a5](https://medium.com/@brandon93.w/rotate-array-problem-a-deep-dive-into-a-fundamental-coding-challenge-600b915734a5)  
7. Juggling Algorithm for Array Rotation \- GeeksforGeeks, accessed February 9, 2026, [https://www.geeksforgeeks.org/dsa/juggling-algorithm-for-array-rotation/](https://www.geeksforgeeks.org/dsa/juggling-algorithm-for-array-rotation/)

[image1]: <data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABMAAAAXCAYAAADpwXTaAAAAxElEQVR4Xu2SvQ4BQRSFR0Io/BQqIXgECY0nIZEoFSrPQKFdEQ2FQil6L6Z3DrPCyZhZlWa+5Mtu7jl3d5NZYyKRzJThDJY0sHDOnL0gQ7jWobCEfR0qBbgx4SJfyF5eg5QcnMOFvffBnL2pvf+gCrfWNmxkkL2j3eH+gyJcwRu8wv0Pss897vM5L7rwAnvvQw8DeIYtDVI68ABrGgh1eDLPvpcJHOlQ4EGNdeiCn52Y78fOn3YHmxq44FFX7NVFKI/8izuhFBraw18svAAAAABJRU5ErkJggg==>

[image2]: <data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADgAAAAYCAYAAACvKj4oAAADZklEQVR4Xu2XTYhOURjHH6HIV77zHVmQ70SULCSxYIGFwlIsrHxGFm9JYiGhCDVZIBELaZAyZbJgp0QhkQghSyn8f3POGfece+/b+87MO7OZX/175z3nzr3nec7z/M99zXrpcfpKG6Tz0kFpaDzd/QyWBqSDnWC7tMxcoIekVmlkZn6Y1D/zvS640SppozTT3EOqMV9qMvfQroBktUgX/fdp0kdpTbhALJYuWx3P7CMtl55IzdJmr/vSK2nR/0sjJkoPpVnphBglPZL+ev2QZkdXmO3zc0EfpDleU/w1JJkAV/rvgS3SOathJ7ngmPTW8oEwRx+wuAXJHEk5KVWS8ZR10k9zAVTiqTZI0mNpRjrh2W8u0exsloHSbWl9Mh5BAGel7+a2vQgy+E06Yy6oALvx0n9Wo2Kup9idF9LYaNZsnnRB6peMA2u6Io1IJzybzCWntFR3SH/8ZxnDpafSc3NlFyCzd6y6uQwy10vsEgliF1lUFlphVzIG9PZRc/cgwDHxdBv052tzhpRjurnaLspqlhDgO2mcHyMogsPCq8EC2B2uXyr9ku6aK68A7ZEukP47Lk0298ytlm8fIPgHVrKOirmMHknGU1jkJ4sD5JPva8NFJeB8e/3fBEVwBEmwQPKuWZxgknHL8uZDFRRxyZyjZtun3Yopz9SdUpjnOhxxiB9bKH22fOZTKhbfn/JkwaGf6W88oKj/aoVWabHEhMIOYB48pBqnLO+ABPjef5YR+m9CZoydoiXYkalW3n/1QIC0ENXQTggwW3ZFTDJ3Dn61+KyrJcBs/2WpmEvYTivuv3ohwDeW+AhuiCtWC5ASOmBuMbuTuVoC5Pzj/1OCuVHi9yx25o5QWKLU/FXpt5VnkDOIA56DPn1bKHp9SqlYcX+TuHBk3LDO9R9gkjgpLRHBmwkBNFk+gBXSF+mExZYeCBXAAV7EaHO7Mzed8IQjo+z/a4Vk4aD4RCGcLRyUvIOG989m6Zm5ICPrzcA4iUlvzGHMvbIWf9ryL+wk7aaVV0+tYCw8j3YohYfjpPx64Fwbb+WBZcHyuXnkXt0MlYAr48hdDj+rWqXV6UQ3wSYc9qplQzoEpXHdivu00eDGmAuvdQ2DzPEqhhqWxQIwRTyg6k+lroKH7ZGWpBMNZJv1XGv00nD+AUEco6cvCwNWAAAAAElFTkSuQmCC>

[image3]: <data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADAAAAAYCAYAAAC8/X7cAAADAklEQVR4Xu2XS8hOQRjH/0IRktxyS2RB7okoLCSxYGWhWGNh5Rqrs7GwkVAK9a0kkWxcNxRZsFOikEiEEDspPL9vZj4zcy5O3peFvl/9O987z5w5zzzzzDPzSf38fww3DckbO6Cj8Uab1pg2mmaZBqbmEgtMPaaRuaEDppou+GcrBphWmu6Zrpo2e90wPTEt/tU1YbLppml2bjDGmG6bfnh9Ms1Jekh7vS3olWmut60wXVKLwAw2HTI9V9lRbCflPr4wszHpI6Yia8/ZYPos52CRmnohCHdNM7N2xj9u2pe1J+DgCdNH05LMFiCNPsgNxqABovnYP5soTNvkovvIND6xSvNNp0yDsnZYJvfOtNwQ2G767p91jDLdNz2US4sAkbms5s02zHRaLsoEgFXYlPRwqbozawuEb9OnxAzTa1VHJSYM8sI0wbfhNM4fCJ1qmC4XXfoTza+ma6ahUR/Sd3n0O4cAnFG6+r0UchE5mLXn4MQbpRPgye/1oVMN60x7/N84jfNMgskAwTmn5gCy0hSDEXEjdfaWXPqsjg0VYKdfPMgi01s1Rw4KpeOTPgQt7Cf2F3uwKv8DBCkOXi8hgmxOBmniqMoVhAm89M86Qv5PitqINCnLhmZjNuV/gAmQAWRCH2ECpZllTJE7B94rrfVtJhDnf0whF5Ad+n3+AxP4Ilet+qCaUFWaJsAS75f72K7M1mYC1H/ezwnFgxS8rrSyVVGZQuTcWdM31UeAc4EDjIOM8yKG6OIEm7SOQtX7KxxQBIbrQlP+A2n2TBUbnZMVB3tUdnCV6Z3psNKSFwgryAFVxVi56M7LDZ5QUuvej6FU1543XB2eyt2Bwv2Hu9ADuUmUaq+HdibOBo8ZJzdWfL85pvKFkKBcVP3qB1gdVqnxOsHgVCJun+TbRNU7HkNJxFlq+d+CSkXVCudGV+Hafce0Njd0kS1yB12e4l2DSnNe1fukUwjQFdVfMrsCqcZVAbVJu7YwViFXvrs5biUs727T0tzQAfxHuFX/wPl+/oSfdmCStdDzDykAAAAASUVORK5CYII=>

[image4]: <data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAACgAAAAYCAYAAACIhL/AAAACZklEQVR4Xu2WTahNURiG3xuKouuvK38TmYgB+ckAA0nuACVFmZgx9hujY2BgIt2UMsGIYooycetKN2ZKFBKJECZm8vO+fWtl7e/ste4+J7sUbz3tc75v7b3ftdf61lrAf/19mk6m+mBBU8igDzbVHLKV7CbLyKRquksrySX09kIZPEd2+UROA2QTeUBuk32BO+QZWfu7aUWLyF2y3CeCppH9ZKaLS/oQt8g6n/BSb86Ql+g2otxF8oWscjl1Sl+h4+KzyR5yGXbfKzI/bZBoG+wjaIrUSgYukM/I90TD/Imch5mKWkGehmsqGdxJ1pBrKBvUtLhP9vpE1EHyI1xzmkUeksdkbhI/Tm6iXBxXUDYonSY3yGSfWErekidknsuligbTF8mUzJ2MjTJqYnAY1kbzuaIO+QnrQUlLyDtUX6Sr/m+PjTJqYnA1eQM3/zUpR2HDuyVN1Eh5tRsjM0JMD31PNsRGGTUxWNvZGNTkVxGUNAL70p0kJoOvw7WkXgxqWesKTnTzYtg6+BHVta4Ng4fSoKpRVVm6eYCcgH29wy7XhsHKEKukr5JvyM8jrYtaaLVQa71MpcLRCqAKLKmJweyztDPIgPZRb2Az+UDOwrYrrzgCB3zCSQZVoV1LSCJV7wtkakHJ57A9OO6/2osfwUxqmOukuDqmAvIaglX8V9j0EN9hRus2BL1zFIXtTqcVudfpRfNgAfLGUml7Use0kPerONU6Lv5HpNPIPdiG36+0m42HayvaQa6jfp5OJI3SKXIs/G5FevDRQK8v2Qg7JPRy0O1LWgGOkPU+UdBC2BmgdXP/pn4BKLx5uZZqDuoAAAAASUVORK5CYII=>

[image5]: <data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEwAAAAYCAYAAABQiBvKAAAEe0lEQVR4Xu2YW6itUxTH/0Jx3DunXKLTkRe3OLkLHUIukUQp8ubwoCP3eFolSQqhyKWTBynEg8sRyipCFFEuuSQSIZS8uPv/jG9a8xvr+769N+ucY9f+17+115xzzTnmmGP8x5xbWsISFhu2N7fJjYsIO5lb58b5Yrl5snmOua+5Zbt7Cgeb6xWLFuBA5tmiavs/43DzIbX3MAg2dpz5urnBPL/hc+ZH5mGToS3sab5o7t98P9D8xvzTHCsct6lwvPmtYm34rLlt1b+jYj+lHz5ubtf0X2Deo3lEGgNuNj/VtGPou9f8wVyd+nDy7eYotfObJ7TpHQawCXt/MX82j2p3/42zzcfUdibg+5OK/l6wubvN7xVh2QXS8jvzLrVT7ADzg+Yz40FtHoftolj7MkUEZZvBFYrs6cJ55isaSM1LzD+azz5gxBvmu+aKqv1a82l1i/3mcthB5m3m7ub75hfmqqp/K/O+ZlwX9jY/No/JHWAf80vFxLumvhrFYZ8pDAE4CWddXwYl9DmMQnCu4neHqLugMPca8yxzL4X2EDHXaaI3fSByLm7+Himi7NJ/euPAsY09dYH5X1DPvkaKCW9M7Rl4/Su1HcYn388ogxKyw0iLCxWOX2OuNG8xnzd3a8YAKu6H5k2KzTP+E3Ot+bnCyUO4VZMxSAXa+5omKUbk3Nn83Qdsp2K2UpmNjBXpeGLd0QH6GfeSuUPThlFfqyd0Ne2wkxTGH1sGKPTzEYXQIriQyoYgkzoAAf7dPEGxdtajGkW/imwwx8MK209p2oi+Pv0qQGrGStlRIgQxR9SHcIciEkdVGw4bOvHaYSV9swYCjPtV4fhiE78tIIJZG6GeC0W/aqfiKByG4ziQIf0qwCYiu5W2xbg6zbqAhnAP435T7lpgIQ5DH0mr8r0GxhWHlLI+1mQcEdZ3Pcio9auAVCQliW6iFLv69KsAm7C3peucNCc+5DBOCqFlQ1emvoU4rKw1dWqaOKykyWnmj4pIWKe4GyLaQ6kI6Eeb8j0ScCFljfcU98250JmSJb9LOnSBexknw0Uw334pBFTYU1N7Qe2wshYlnpdBDQoOayDQgE0fqqimHGSOyD5k/apBpHATqA9mCNhEpZyqyNzcMXa9ph1C+PLEoerkGzEoUZNToKB2GGAtLsecdgFO4SnGa6FE0P0K0ecdW4gUZPsysJdnzrLc0WCk+ek1dlAh0e1OEMJc1DAc78MN5jsKI/pSgXYcnSfmLcm1gNOEOKlU4f3MV82nzAcUDr9abWecbv6m9nsPoqFEXgZz/6TJOPSOOTKIYCpwloQM+vHFmbmjBpdHPM9pUpX2UL+javCMYPK5jMjYWZEmOWqIQpx9dGpfZT6j/lfFLEFxIX1Zc+YgpV7W5I7zX0GlHKtbt6iWU5VrxiBIbmg4n4D5VyB0H1W3zi0UK823zcvVjj5S+U3zGm3EjSieiog9dmw0sAF0CM5iM7wbcQyOe6shmneEZjN/HzggNHnwXzuzAotdZR6ZOxYRLtLspGUJS1gg/gLy5u6utjY4wgAAAABJRU5ErkJggg==>
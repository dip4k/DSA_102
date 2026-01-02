# 📁 Week_04_Problem_Solving_Roadmap.md

## 🧭 Week 4 – Problem-Solving Roadmap: Core Patterns I

### 🌟 Overall Strategy
The goal this week is to stop seeing arrays as static lists and start seeing them as dynamic spaces we can navigate efficiently.
- **Stage 1 (Mechanics):** Practice moving pointers correctly (no off-by-one errors).
- **Stage 2 (Optimization):** Take a brute force O(N^2) solution and apply a pattern to make it O(N).
- **Stage 3 (Decision):** Given a fresh problem, decide *which* pattern applies.

---

## 🪜 Progression from Simple → Complex

### 🟢 Stage 1: Basic Applications (The "Hello World" of Patterns)
*Goal: Implement the pattern correctly on standard problems.*

1. **Two Pointers (Sorted):** Two Sum II (Input is sorted).
   - *Focus:* Why do we move left vs right?
2. **Two Pointers (In-place):** Remove Duplicates / Move Zeroes.
   - *Focus:* Difference between `read` and `write` pointers.
3. **Fixed Window:** Max Sum Subarray of size K.
   - *Focus:* Subtracting `arr[i-k]` and adding `arr[i]`.
4. **Binary Search Answer:** Square Root (Integer).
   - *Focus:* Setting `low=0`, `high=x` and checking `mid*mid`.

### 🟡 Stage 2: Variations & Constraints (Adding Logic)
*Goal: Handle edge cases and dynamic conditions.*

1. **Variable Window (Expansion):** Longest Substring Without Repeating Characters.
   - *Challenge:* Using a Hash Set to track validity; shrinking until valid.
2. **Variable Window (Contraction):** Minimum Size Subarray Sum.
   - *Challenge:* The window can shrink multiple times in one step (while loop).
3. **Container With Most Water:**
   - *Challenge:* Understanding the greedy choice (move the shorter line).
4. **Binary Search Answer:** Koko Eating Bananas.
   - *Challenge:* Writing the `canEatAll(speed)` helper function.

### 🔴 Stage 3: Mixed Concepts & Insight
*Goal: Recognizing patterns in disguise.*

1. **Subarray Product Less Than K:**
   - *Challenge:* Counting subarrays usually involves math `(R - L + 1)`.
2. **Longest Repeating Character Replacement:**
   - *Challenge:* Valid condition is `(WindowLen - MaxFreq) <= K`.
3. **Aggressive Cows / Magnetic Force:**
   - *Challenge:* Sorting first, then Binary Search on Answer.
4. **Split Array Largest Sum:**
   - *Challenge:* Binary Search on Answer where the bounds are `max(arr)` and `sum(arr)`.

---

## ⚠ Common Problem-Solving Pitfalls

### 1. The "Off-by-One" Nightmare
- **Symptom:** Your loop runs one time too few or too many.
- **Fix:** Consistent Intervals. Always use **[closed, open)** or **[closed, closed]**.
  - Sliding Window: Usually `[left, right]`. `right` is the current element being added. `left` is the start. Length is `right - left + 1`.

### 2. The "Shrink Loop" Bug
- **Symptom:** In variable sliding window, you use `if` instead of `while` to shrink.
- **Fix:** If the window becomes invalid, it might require removing *multiple* elements from the left to become valid again. Always use `while (invalid) { remove; left++; }`.

### 3. Infinite Loops in Binary Search
- **Symptom:** The program hangs on 2-element ranges.
- **Fix:**
  - If selecting `right = mid`: Loop while `left < right`.
  - If selecting `right = mid - 1`: Loop while `left <= right`.
  - Prefer the standard: `left = mid + 1` and `right = mid - 1`.

### 4. Forgetting to Sort
- **Symptom:** Two Pointers (Opposite) or Binary Search on Answer logic fails completely.
- **Fix:**
  - Opposite Pointers for Pair Sum? **Must be sorted.**
  - Placing Cows/Aggressive Cows? **Positions must be sorted.**

---

## 📌 Pattern Templates (Logic-Only)

### 🧩 Two Pointers (Opposite Direction)
```text
Initialize left = 0, right = n - 1
While left < right:
    Calculate metric (e.g., sum, area) using arr[left] and arr[right]
    If metric matches target:
        Return result
    Else If metric < target:
        Move left forward (increment)
    Else (metric > target):
        Move right backward (decrement)
Return "not found"
```

### 🪟 Sliding Window (Variable Size)
```text
Initialize left = 0, current_state = empty, max_len = 0
For right from 0 to n - 1:
    Add arr[right] to current_state
    
    While current_state is INVALID:
        Remove arr[left] from current_state
        Increment left
        
    Update max_len with (right - left + 1)
Return max_len
```

### 🔎 Binary Search on Answer (Min-Max)
```text
Initialize low = min_possible, high = max_possible, ans = high
While low <= high:
    mid = low + (high - low) / 2
    
    If Check(mid) is TRUE (Feasible):
        ans = mid       // Record valid answer
        high = mid - 1  // Try to do better (smaller)
    Else:
        low = mid + 1   // Impossible, need more resources
Return ans
```

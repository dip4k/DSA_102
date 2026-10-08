# 🧭 String ↔ Array Traversal Cheatsheet

✅ String traversal patterns are usually the same *controlled-iteration* templates as arrays—swap “element” for “character” and keep the invariant true.

---

## 🧩 Pattern mapping (String vs Array)

| 🧠 Pattern | 🔤 String: common tasks | 🔢 Array: common tasks | 🧷 Invariant (must stay true) |
|---|---|---|---|
| ↔️ **Two pointers (ends → center)** | Palindrome-style checks, symmetric comparisons, selective reverse (e.g., reverse vowels) | Reverse array, pair checks in sorted arrays, two-sum on sorted | Everything outside `[L, R]` is already processed; move pointers inward without revisiting settled parts. |
| ⏩ **Two pointers (same direction: slow/fast)** | Filter/remove chars, compress, dedupe adjacent groups | Remove elements in-place, dedupe sorted array, partitioning | `fast` explores; `slow` is the next write position; prefix `[0..slow)` stays valid. |
| 🪟 **Fixed-size sliding window (size = k)** | Metrics on all substrings of length `k` (vowel count, anagram window length) | Metrics on all subarrays of length `k` (sum/avg, distinct count, window max/min) | Window size is always exactly `k`; update by adding right item and removing left item. |
| 📏 **Variable sliding window (at most K)** | Longest substring under constraint (no repeat, ≤k distinct, ≤k “bad”) | Longest/min subarray under constraint (≤k distinct, sum constraints, etc.) | Expand `right`; while invalid, shrink `left` until valid; only record answers when window is valid. |
| 🧮 **Frequency map (global/window)** | Anagrams, distinct counts, “can construct” problems | Distinct counts, frequency constraints in windows | The map/array always reflects counts of items in the current scope (whole input or current window). |
| 🎯 **Exactly K (via AtMost trick)** | Count substrings with exactly `k` distinct chars / exactly `k` special chars | Count subarrays with exactly `k` distinct numbers | `count(exactly k) = count(at most k) − count(at most k−1)`. |

---

## ⚡ Accelerators (where strings differ more)

- ➕ Arrays often use **prefix sums** to answer range-sum queries in O(1) after O(n) preprocessing.
- 🔍 Strings often use **KMP (LPS/prefix-function)** to avoid backtracking in pattern search, and **rolling hash** to compare substrings efficiently (verify on hash hits).

---

## 🔁 For-loop vs While-loop (decision rules)

### ✅ Prefer `for` when…
- 🔢 The index advances in a uniform, one-step-per-iteration way (visit each position once).  
- 🪟 You have an “expand-right” structure (most sliding window solutions use `for right in range(n)`). 
- 🧱 You build forward tables like prefix sums / LPS / Z arrays where `i` moves deterministically from left to right. 

### ✅ Prefer `while` when…
- 🧭 Pointer movement is conditional and may skip multiple positions (skip non-alnum, skip duplicates, shrink until valid).
- ↔️ You have *two pointers that move independently* based on content (classic `left < right`). 
- 🧺 You consume a structure until empty (stack/queue/deque style loops).

---

## 🧱 Canonical templates (quick picks)

| 🧠 Pattern | Best outer loop | Inner loop | Why |
|---|---|---|---|
| ↔️ Two pointers (ends) | `while left < right` | optional “skip” `while` | Both pointers move based on content, not fixed steps. |
| ⏩ Slow/fast compaction | `for fast in ...` | none | `fast` visits each index once; `slow` advances on condition. |
| 🪟 Fixed window (k) | `for i in range(k, n)` | none | One slide per step; predictable indices. |
| 📏 Variable window (at most K) | `for right in ...` | `while invalid: left++` | Right expands once; left may move many times to restore invariant. |
| 🎯 Exactly K count | two calls of `atMost()` (each: `for right` + `while invalid`) | `while invalid` | Turn “exactly” into two standard “at most” windows. |

---

## 🧠 Pattern signals → instant choice

- 🧩 “substring/subarray with constraint” → **sliding window** (`for right` + `while invalid`). 
- 🎯 “exactly k” counting → **AtMost(k) − AtMost(k−1)**. 
- 🔎 “find pattern occurrences” → **KMP/Z/Rabin–Karp** (table-driven forward scan). 
- 🪞 “symmetric / from both ends” → **two pointers** (`while left < right`). 

# 📁 Week_04_Daily_Progress_Checklist.md

## 📅 Week 4 – Daily Progress Checklist: Core Patterns I

### 🟢 Day 1: Two Pointers
- [ ] **Read:** Week_04_Day_1_Two_Pointers_Instructional.md
- [ ] **Visualize:** Draw a sorted array and move pointers for "Two Sum".
- [ ] **Trace:** Walk through "Container With Most Water" on paper with input `[1,8,6,2,5,4,8,3,7]`.
- [ ] **Code:**
  - [ ] Remove Duplicates from Sorted Array (Easy)
  - [ ] Two Sum II (Medium)
  - [ ] Container With Most Water (Medium)
- [ ] **Reflection:** Why did we sort the array? Or why was the input already sorted?

### 🟢 Day 2: Sliding Window (Fixed Size)
- [ ] **Read:** Week_04_Day_2_Sliding_Window_Fixed_Instructional.md
- [ ] **Visualize:** Draw a window of size K sliding over an array. Calculate sum updates.
- [ ] **Trace:** Trace "Max Sum Subarray of Size K" with `k=3` on `[2, 1, 5, 1, 3, 2]`.
- [ ] **Code:**
  - [ ] Maximum Average Subarray I (Easy)
  - [ ] Find All Anagrams in a String (Medium)
- [ ] **Reflection:** Did I avoid re-calculating the sum from scratch?

### 🟡 Day 3: Sliding Window (Variable Size)
- [ ] **Read:** Week_04_Day_3_Sliding_Window_Variable_Instructional.md
- [ ] **Visualize:** Expand `right` until invalid, shrink `left` until valid.
- [ ] **Trace:** Trace "Longest Substring Without Repeating Characters" on `abcabcbb`.
- [ ] **Code:**
  - [ ] Minimum Size Subarray Sum (Medium)
  - [ ] Longest Substring Without Repeating Characters (Medium)
  - [ ] Max Consecutive Ones III (Medium)
- [ ] **Reflection:** Did I use a `while` loop for shrinking the window?

### 🔴 Day 4: Divide and Conquer
- [ ] **Read:** Week_04_Day_4_Divide_And_Conquer_Pattern_Instructional.md
- [ ] **Visualize:** Draw the recursion tree for Merge Sort on `[38, 27, 43, 3]`.
- [ ] **Trace:** Perform the "Combine" step for two sorted arrays manually.
- [ ] **Code:**
  - [ ] Sort an Array (Merge Sort Implementation) (Medium)
  - [ ] Majority Element (Divide & Conquer approach) (Easy)
- [ ] **Reflection:** How is the stack depth related to N?

### 🔴 Day 5: Binary Search as a Pattern
- [ ] **Read:** Week_04_Day_5_Binary_Search_As_A_Pattern_Instructional.md
- [ ] **Visualize:** Draw the "Answer Space" (Low to High) and the T/F feasibility check.
- [ ] **Trace:** Trace "Koko Eating Bananas" with `piles=[3,6,7,11], H=8`.
- [ ] **Code:**
  - [ ] Capacity To Ship Packages Within D Days (Medium)
  - [ ] Koko Eating Bananas (Medium)
  - [ ] Split Array Largest Sum (Hard)
- [ ] **Reflection:** Did I handle the infinite loop edge case correctly?

---

### 🔄 Weekly Integration & Reflection
- [ ] **Review:** Look at the "Concept Map" in the Summary file. Can you explain each branch?
- [ ] **Mock:** Explain "Binary Search on Answer" to a rubber duck or friend.
- [ ] **Pattern Match:** Take a random problem from LeetCode (Arrays). Can you rule out patterns?
  - "Is it sorted?" → Maybe Two Pointers or BS.
  - "Subarray sum/product?" → Maybe Sliding Window.
  - "Minimize the Maximum?" → Binary Search on Answer.
- [ ] **Rest:** Prepare for Week 5 (Hash Maps & Sets).

"""
Problem #6: Majority Element (LeetCode #169)
Difficulty: Easy | Priority: High
Governing Invariant: Boyer-Moore Pairwise Annihilation — Strict majority surplus survives 1:1 pairwise cancellations.
"""

def majority_element(nums: list[int]) -> int:
    candidate = 0
    count = 0

    for num in nums:
        if count == 0:
            candidate = num
        count += 1 if num == candidate else -1

    return candidate


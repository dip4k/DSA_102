"""
Problem #5: Longest Consecutive Sequence (LeetCode #128)
Difficulty: Medium | Priority: Core
Governing Invariant: Left Boundary Invariant — x is streak anchor <=> x - 1 not in Set.
"""

def longest_consecutive(nums: list[int]) -> int:
    if not nums:
        return 0

    num_set = set(nums)
    longest_streak = 0

    for num in num_set:
        if num - 1 not in num_set:
            current_num = num
            current_streak = 1

            while current_num + 1 in num_set:
                current_num += 1
                current_streak += 1

            longest_streak = max(longest_streak, current_streak)

    return longest_streak


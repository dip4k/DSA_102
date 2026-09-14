"""
Problem #1: Two Sum (LeetCode #1)
Difficulty: Easy | Priority: Core
Governing Invariant: Complement Lookup — nums[i] + complement = target <=> complement = target - nums[i]
"""

def two_sum(nums: list[int], target: int) -> list[int]:
    seen: dict[int, int] = {}
    for i, num in enumerate(nums):
        complement = target - num
        if complement in seen:
            return [seen[complement], i]
        seen[num] = i
    return []

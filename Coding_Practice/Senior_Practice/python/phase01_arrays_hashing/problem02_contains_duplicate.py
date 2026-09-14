"""
Problem #2: Contains Duplicate (LeetCode #217)
Difficulty: Easy | Priority: Core
Governing Invariant: Set Membership Invariant — S_k = {nums[0] ... nums[k-1]}. nums[k] in S_k => duplicate.
"""

def contains_duplicate(nums: list[int]) -> bool:
    seen: set[int] = set()
    for num in nums:
        if num in seen:
            return True
        seen.add(num)
    return False


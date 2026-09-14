"""
Problem #9: Two Sum II — Input Array Is Sorted (LeetCode #167)
Difficulty: Medium | Priority: Core
Governing Invariant: Monotonic Search-Space Pruning — sum > target => right--; sum < target => left++.
"""

def two_sum_ii(numbers: list[int], target: int) -> list[int]:
    left = 0
    right = len(numbers) - 1

    while left < right:
        current_sum = numbers[left] + numbers[right]

        if current_sum == target:
            return [left + 1, right + 1]
        elif current_sum < target:
            left += 1
        else:
            right -= 1

    return []


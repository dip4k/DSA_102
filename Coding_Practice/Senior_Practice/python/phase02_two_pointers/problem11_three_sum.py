"""
Problem #11: 3Sum (LeetCode #15)
Difficulty: Medium | Priority: Core
Governing Invariant: Fixed Anchor + Opposing Two Pointers with In-Place Deduplication.
"""

def three_sum(nums: list[int]) -> list[list[int]]:
    if len(nums) < 3:
        return []

    nums.sort()
    result: list[list[int]] = []
    n = len(nums)

    for i in range(n - 2):
        if nums[i] > 0:
            break

        if i > 0 and nums[i] == nums[i - 1]:
            continue

        left, right = i + 1, n - 1

        while left < right:
            current_sum = nums[i] + nums[left] + nums[right]

            if current_sum == 0:
                result.append([nums[i], nums[left], nums[right]])
                left += 1
                right -= 1

                while left < right and nums[left] == nums[left - 1]:
                    left += 1
                while left < right and nums[right] == nums[right + 1]:
                    right -= 1
            elif current_sum < 0:
                left += 1
            else:
                right -= 1

    return result


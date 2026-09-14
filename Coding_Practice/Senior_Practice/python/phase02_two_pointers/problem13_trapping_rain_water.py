"""
Problem #13: Trapping Rain Water (LeetCode #42)
Difficulty: Hard | Priority: Core / Flagship Anchor
Governing Invariant: If left_max < right_max, water at left is left_max - height[left].
"""

def trap(height: list[int]) -> int:
    if not height or len(height) < 3:
        return 0

    left, right = 0, len(height) - 1
    left_max, right_max = height[left], height[right]
    trapped_water = 0

    while left < right:
        if left_max < right_max:
            left += 1
            if height[left] < left_max:
                trapped_water += left_max - height[left]
            else:
                left_max = height[left]
        else:
            right -= 1
            if height[right] < right_max:
                trapped_water += right_max - height[right]
            else:
                right_max = height[right]

    return trapped_water


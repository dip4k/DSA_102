"""
Problem #10: Container With Most Water (LeetCode #11)
Difficulty: Medium | Priority: Core
Governing Invariant: Advance the pointer at the shorter wall to seek larger height.
"""

def max_area(height: list[int]) -> int:
    if not height or len(height) < 2:
        return 0

    left, right = 0, len(height) - 1
    max_water = 0

    while left < right:
        h_left, h_right = height[left], height[right]
        current_area = min(h_left, h_right) * (right - left)
        if current_area > max_water:
            max_water = current_area

        if h_left < h_right:
            left += 1
        else:
            right -= 1

    return max_water


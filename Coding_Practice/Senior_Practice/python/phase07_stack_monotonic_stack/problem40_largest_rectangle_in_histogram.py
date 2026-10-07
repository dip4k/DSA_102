def largest_rectangle_area(heights: list[int]) -> int:
    stack: list[int] = []
    max_area = 0
    n = len(heights)

    for i in range(n + 1):
        curr_h = 0 if i == n else heights[i]
        while stack and curr_h < heights[stack[-1]]:
            h = heights[stack.pop()]
            w = i if not stack else i - stack[-1] - 1
            max_area = max(max_area, h * w)
        stack.append(i)

    return max_area

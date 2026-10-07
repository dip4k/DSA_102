def max_sub_array(nums: list[int]) -> int:
    if not nums:
        return 0
    curr_max = nums[0]
    global_max = nums[0]
    for x in nums[1:]:
        curr_max = max(x, curr_max + x)
        global_max = max(global_max, curr_max)
    return global_max

def running_sum(nums: list[int]) -> list[int]:
    result = [0] * len(nums)
    if not nums:
        return result
    result[0] = nums[0]
    for i in range(1, len(nums)):
        result[i] = result[i - 1] + nums[i]
    return result

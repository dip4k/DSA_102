from collections import defaultdict

def subarray_sum(nums: list[int], k: int) -> int:
    prefix_freq: dict[int, int] = defaultdict(int)
    prefix_freq[0] = 1
    curr_prefix = 0
    count = 0

    for x in nums:
        curr_prefix += x
        target = curr_prefix - k
        count += prefix_freq[target]
        prefix_freq[curr_prefix] += 1

    return count

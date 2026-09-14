"""
Problem #7: Product of Array Except Self (LeetCode #238)
Difficulty: Medium | Priority: Core
Governing Invariant: Prefix/Suffix Decomposition — Answer[i] = Prefix[i-1] * Suffix[i+1].
"""

def product_except_self(nums: list[int]) -> list[int]:
    n = len(nums)
    result = [1] * n

    for i in range(1, n):
        result[i] = result[i - 1] * nums[i - 1]

    suffix_product = 1
    for i in range(n - 1, -1, -1):
        result[i] *= suffix_product
        suffix_product *= nums[i]

    return result


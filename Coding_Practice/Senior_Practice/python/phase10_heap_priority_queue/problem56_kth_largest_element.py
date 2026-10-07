import heapq

def find_kth_largest(nums: list[int], k: int) -> int:
    min_heap: list[int] = []
    for num in nums:
        heapq.heappush(min_heap, num)
        if len(min_heap) > k:
            heapq.heappop(min_heap)
    return min_heap[0]

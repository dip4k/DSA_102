import heapq

def k_closest(points: list[list[int]], k: int) -> list[list[int]]:
    max_heap: list[tuple[int, list[int]]] = []

    for pt in points:
        dist_sq = pt[0] * pt[0] + pt[1] * pt[1]
        heapq.heappush(max_heap, (-dist_sq, pt))
        if len(max_heap) > k:
            heapq.heappop(max_heap)

    return [pt for _, pt in max_heap]

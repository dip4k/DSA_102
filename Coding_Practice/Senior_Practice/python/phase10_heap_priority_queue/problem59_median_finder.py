import heapq

class MedianFinder:
    def __init__(self):
        # lower_half: max-heap (negated values)
        self.lower_half: list[int] = []
        # upper_half: min-heap (standard values)
        self.upper_half: list[int] = []

    def add_num(self, num: int) -> None:
        heapq.heappush(self.lower_half, -num)
        max_lower = -heapq.heappop(self.lower_half)
        heapq.heappush(self.upper_half, max_lower)

        if len(self.lower_half) < len(self.upper_half):
            min_upper = heapq.heappop(self.upper_half)
            heapq.heappush(self.lower_half, -min_upper)

    def find_median(self) -> float:
        if len(self.lower_half) > len(self.upper_half):
            return float(-self.lower_half[0])
        return (-self.lower_half[0] + self.upper_half[0]) / 2.0

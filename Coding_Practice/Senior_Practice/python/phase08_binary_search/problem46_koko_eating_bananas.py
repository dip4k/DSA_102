def min_eating_speed(piles: list[int], h: int) -> int:
    left, right = 1, max(piles)
    ans = right

    def can_finish(speed: int) -> bool:
        hours = 0
        for p in piles:
            hours += (p + speed - 1) // speed
            if hours > h:
                return False
        return hours <= h

    while left <= right:
        mid = left + (right - left) // 2
        if can_finish(mid):
            ans = mid
            right = mid - 1
        else:
            left = mid + 1

    return ans

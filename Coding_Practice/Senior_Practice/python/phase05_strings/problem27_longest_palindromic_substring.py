def longest_palindrome(s: str) -> str:
    if not s:
        return ""

    start, max_len = 0, 0

    def expand(left: int, right: int) -> int:
        while left >= 0 and right < len(s) and s[left] == s[right]:
            left -= 1
            right += 1
        return right - left - 1

    for i in range(len(s)):
        len1 = expand(i, i)
        len2 = expand(i, i + 1)
        best = max(len1, len2)
        if best > max_len:
            max_len = best
            start = i - (best - 1) // 2

    return s[start : start + max_len]

def count_substrings(s: str) -> int:
    if not s:
        return 0

    count = 0

    def count_palindromes(left: int, right: int) -> int:
        c = 0
        while left >= 0 and right < len(s) and s[left] == s[right]:
            c += 1
            left -= 1
            right += 1
        return c

    for i in range(len(s)):
        count += count_palindromes(i, i)
        count += count_palindromes(i, i + 1)

    return count

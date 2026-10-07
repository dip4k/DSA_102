def length_of_longest_substring(s: str) -> int:
    last_seen: dict[str, int] = {}
    max_len = 0
    left = 0
    for right, char in enumerate(s):
        if char in last_seen and last_seen[char] >= left:
            left = last_seen[char] + 1
        last_seen[char] = right
        max_len = max(max_len, right - left + 1)
    return max_len

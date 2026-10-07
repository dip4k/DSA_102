def character_replacement(s: str, k: int) -> int:
    counts: dict[str, int] = {}
    left = 0
    max_freq = 0
    max_window = 0

    for right, char in enumerate(s):
        counts[char] = counts.get(char, 0) + 1
        max_freq = max(max_freq, counts[char])

        while (right - left + 1) - max_freq > k:
            counts[s[left]] -= 1
            left += 1

        max_window = max(max_window, right - left + 1)

    return max_window

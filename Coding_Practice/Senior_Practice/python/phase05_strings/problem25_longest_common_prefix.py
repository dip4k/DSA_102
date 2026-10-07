def longest_common_prefix(strs: list[str]) -> str:
    if not strs:
        return ""
    for col, char in enumerate(strs[0]):
        for row in range(1, len(strs)):
            if col == len(strs[row]) or strs[row][col] != char:
                return strs[0][:col]
    return strs[0]

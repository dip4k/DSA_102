def check_inclusion(s1: str, s2: str) -> bool:
    if len(s1) > len(s2):
        return False

    s1_map = [0] * 26
    s2_map = [0] * 26
    k = len(s1)

    for i in range(k):
        s1_map[ord(s1[i]) - ord('a')] += 1
        s2_map[ord(s2[i]) - ord('a')] += 1

    matches = sum(1 for i in range(26) if s1_map[i] == s2_map[i])
    if matches == 26:
        return True

    for i in range(k, len(s2)):
        in_char = ord(s2[i]) - ord('a')
        out_char = ord(s2[i - k]) - ord('a')

        s2_map[in_char] += 1
        if s2_map[in_char] == s1_map[in_char]:
            matches += 1
        elif s2_map[in_char] == s1_map[in_char] + 1:
            matches -= 1

        s2_map[out_char] -= 1
        if s2_map[out_char] == s1_map[out_char]:
            matches += 1
        elif s2_map[out_char] == s1_map[out_char] - 1:
            matches -= 1

        if matches == 26:
            return True

    return False

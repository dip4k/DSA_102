"""
Problem #4: Group Anagrams (LeetCode #49)
Difficulty: Medium | Priority: Core
Governing Invariant: Canonical Key Partitioning — Words w_1, w_2 share the same sorted/frequency key.
"""
from collections import defaultdict

def group_anagrams(strs: list[str]) -> list[list[str]]:
    groups: dict[tuple[int, ...], list[str]] = defaultdict(list)

    for word in strs:
        count = [0] * 26
        for ch in word:
            count[ord(ch) - ord('a')] += 1
        groups[tuple(count)].append(word)

    return list(groups.values())


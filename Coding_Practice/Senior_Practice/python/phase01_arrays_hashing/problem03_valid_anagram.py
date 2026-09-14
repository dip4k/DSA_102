"""
Problem #3: Valid Anagram (LeetCode #242)
Difficulty: Easy | Priority: Core
Governing Invariant: Histogram Net Zero Balance — Sum(|freq[c]|) = 0 <=> strings are anagrams.
"""

def is_anagram(s: str, t: str) -> bool:
    if len(s) != len(t):
        return False

    freq: dict[str, int] = {}

    for c1, c2 in zip(s, t):
        freq[c1] = freq.get(c1, 0) + 1
        freq[c2] = freq.get(c2, 0) - 1

    return all(count == 0 for count in freq.values())


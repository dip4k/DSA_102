"""
Problem #8: Valid Palindrome (LeetCode #125)
Difficulty: Easy | Priority: Reinforcement
Governing Invariant: Opposing Inward Convergence — s[left] == s[right] across alphanumeric characters.
"""

def is_palindrome(s: str) -> bool:
    left, right = 0, len(s) - 1

    while left < right:
        while left < right and not s[left].isalnum():
            left += 1
        while left < right and not s[right].isalnum():
            right -= 1

        if s[left].lower() != s[right].lower():
            return False

        left += 1
        right -= 1

    return True


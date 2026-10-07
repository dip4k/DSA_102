def is_valid(s: str) -> bool:
    if len(s) % 2 != 0:
        return False

    matching = {')': '(', '}': '{', ']': '['}
    stack: list[str] = []

    for c in s:
        if c in matching:
            if not stack or stack.pop() != matching[c]:
                return False
        else:
            stack.append(c)

    return len(stack) == 0

namespace SeniorPractice.Phase07;

/// <summary>
/// Problem #36: Valid Parentheses (LeetCode #20)
/// Invariant: Stack of expected closing brackets
/// </summary>
public static class Problem36_ValidParentheses
{
    public static bool IsValid(string s)
    {
        if (string.IsNullOrEmpty(s)) return true;
        if (s.Length % 2 != 0) return false;

        var stack = new Stack<char>();
        foreach (char c in s)
        {
            if (c == '(') stack.Push(')');
            else if (c == '{') stack.Push('}');
            else if (c == '[') stack.Push(']');
            else if (stack.Count == 0 || stack.Pop() != c) return false;
        }

        return stack.Count == 0;
    }
}

namespace SeniorPractice.Phase07;

/// <summary>
/// Problem #38: Evaluate Reverse Polish Notation (LeetCode #150)
/// Invariant: Stack evaluation with integer arithmetic truncation towards zero
/// </summary>
public static class Problem38_EvaluateReversePolishNotation
{
    public static int EvalRPN(string[] tokens)
    {
        ArgumentNullException.ThrowIfNull(tokens);
        var stack = new Stack<int>();

        foreach (string token in tokens)
        {
            if (token is "+" or "-" or "*" or "/")
            {
                int b = stack.Pop();
                int a = stack.Pop();
                int res = token switch
                {
                    "+" => a + b,
                    "-" => a - b,
                    "*" => a * b,
                    "/" => a / b,
                    _ => 0
                };
                stack.Push(res);
            }
            else
            {
                stack.Push(int.Parse(token));
            }
        }

        return stack.Pop();
    }
}

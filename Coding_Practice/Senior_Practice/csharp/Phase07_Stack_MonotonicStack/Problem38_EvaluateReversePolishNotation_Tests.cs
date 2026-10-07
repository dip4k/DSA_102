namespace SeniorPractice.Phase07.Tests;

public class Problem38_EvaluateReversePolishNotation_Tests
{
    [Fact]
    public void EvalRPN_ShouldEvaluateExpressions()
    {
        Assert.Equal(9, Problem38_EvaluateReversePolishNotation.EvalRPN(new[] { "2", "1", "+", "3", "*" }));
        Assert.Equal(6, Problem38_EvaluateReversePolishNotation.EvalRPN(new[] { "4", "13", "5", "/", "+" }));
        Assert.Equal(22, Problem38_EvaluateReversePolishNotation.EvalRPN(new[] { "10", "6", "9", "3", "+", "-11", "*", "/", "*", "17", "+", "5", "+" }));
    }
}

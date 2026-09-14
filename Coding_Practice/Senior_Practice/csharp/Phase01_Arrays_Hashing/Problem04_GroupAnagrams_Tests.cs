namespace SeniorPractice.Phase01.Tests;

public class Problem04_GroupAnagrams_Tests
{
    [Fact]
    public void GroupAnagrams_StandardCase_ShouldGroupCorrectly()
    {
        string[] input = ["eat", "tea", "tan", "ate", "nat", "bat"];
        var result = Problem04_GroupAnagrams.GroupAnagrams(input);

        Assert.Equal(3, result.Count);
        var sortedGroups = result
            .Select(g => g.OrderBy(x => x).ToList())
            .OrderBy(g => g.First())
            .ToList();

        Assert.Contains(sortedGroups, g => g.SequenceEqual(new List<string> { "bat" }));
        Assert.Contains(sortedGroups, g => g.SequenceEqual(new List<string> { "nat", "tan" }));
        Assert.Contains(sortedGroups, g => g.SequenceEqual(new List<string> { "ate", "eat", "tea" }));
    }

    [Fact]
    public void GroupAnagrams_SingleEmpty_ShouldReturnSingleGroup()
    {
        string[] input = [""];
        var result = Problem04_GroupAnagrams.GroupAnagrams(input);
        Assert.Single(result);
        Assert.Equal([""], result[0]);
    }

    [Fact]
    public void GroupAnagrams_SingleChar_ShouldReturnSingleGroup()
    {
        string[] input = ["a"];
        var result = Problem04_GroupAnagrams.GroupAnagrams(input);
        Assert.Single(result);
        Assert.Equal(["a"], result[0]);
    }
}


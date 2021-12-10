using Xunit;

namespace AOC.Tests;

public class Day7Tests
{
    [Fact]
    public void Test()
    {
        var day = new Day7();
        var result = day.Run(@"16,1,2,0,4,2,7,1,2,14").Result;
        Assert.Equal(37, result.Part1);
        Assert.Equal(168, result.Part2);
    }
}
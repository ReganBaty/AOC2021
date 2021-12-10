using Xunit;

namespace AOC.Tests;

public class Day6Tests
{
    [Fact]
    public void Test()
    {
        var day = new Day6();
        var result = day.Run(@"3,4,3,1,2").Result;
        Assert.Equal(5934, result.Part1);
        Assert.Equal(26984457539, result.Part2);
    }
}
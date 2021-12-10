using Xunit;

namespace AOC.Tests;

public class Day9Tests
{
    [Fact]
    public void Test()
    {
        var day = new Day9();
        var result = day.Run(@"2199943210
3987894921
9856789892
8767896789
9899965678").Result;
        Assert.Equal(15, result.Part1);
        Assert.Equal(1134, result.Part2);
    }

}
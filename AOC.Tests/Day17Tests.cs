using Xunit;

namespace AOC.Tests;

public class Day17Tests
{
    [Fact]
    public void Test()
    {
        var day = new Day17();
        var result = day.Run(@"target area: x=20..30, y=-10..-5").Result;
        Assert.Equal(45, result.Part1);
        Assert.Equal(112, result.Part2);
    }
}
using Xunit;

namespace AOC.Tests;

public class Day2Tests
{
    [Fact]
    public void Test()
    {
        var day = new Day2();
        var result = day.Run(@"forward 5
        down 5
        forward 8
        up 3
        down 8
        forward 2").Result;
        Assert.Equal(150, result.Part1);
        Assert.Equal(900, result.Part2);
    }
}
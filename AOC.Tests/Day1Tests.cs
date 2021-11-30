using Xunit;

namespace AOC.Tests;

public class Day1Tests
{
    [Fact]
    public void Test1()
    {
        var day1 = new Day1();
        var result = day1.Run(@"199
        200
        208
        210
        200
        207
        240
        269
        260
        263").Result;
        Assert.Equal(7, result.Part1);
        Assert.Equal(5, result.Part2);
    }
}
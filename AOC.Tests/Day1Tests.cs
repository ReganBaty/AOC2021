using Xunit;

namespace AOC.Tests;

public class Day1Tests
{
    [Fact]
    public void Test1()
    {
        var day1 = new Day1();
        var result = day1.Run("l").Result;
        Assert.Equal(0, result.Day1);
    }
}
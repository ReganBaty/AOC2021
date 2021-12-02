using Xunit;

namespace AOC.Tests;

public class Day3Tests
{
    [Fact]
    public void Test()
    {
        var day = new Day3();
        var result = day.Run(@"00100
11110
10110
10111
10101
01111
00111
11100
10000
11001
00010
01010").Result;
        Assert.Equal(198, result.Part1);
        Assert.Equal(230, result.Part2);
    }
}
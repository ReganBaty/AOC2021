using Xunit;

namespace AOC.Tests;

public class Day5Tests
{
    [Fact]
    public void Test()
    {
        var day = new Day5();
        var result = day.Run(@"0,9 -> 5,9
8,0 -> 0,8
9,4 -> 3,4
2,2 -> 2,1
7,0 -> 7,4
6,4 -> 2,0
0,9 -> 2,9
3,4 -> 1,4
0,0 -> 8,8
5,5 -> 8,2").Result;
        Assert.Equal(5, result.Part1);
        Assert.Equal(12, result.Part2);
    }
}
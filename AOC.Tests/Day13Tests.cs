using Xunit;

namespace AOC.Tests;

public class Day13Tests
{
    [Fact]
    public void Test()
    {
        var day = new Day13();
        var result = day.Run(@"6,10
0,14
9,10
0,3
10,4
4,11
6,0
6,12
4,1
0,13
10,12
3,4
3,0
8,4
1,10
2,14
8,10
9,0

fold along y=7
fold along x=5").Result;
        Assert.Equal(17, result.Part1);
        Assert.Equal(0, result.Part2);
    }
}
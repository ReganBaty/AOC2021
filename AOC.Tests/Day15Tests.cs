using Xunit;

namespace AOC.Tests;

public class Day15Tests
{
    [Fact]
    public void Test()
    {
        var day = new Day15();
        var result = day.Run(@"1163751742
1381373672
2136511328
3694931569
7463417111
1319128137
1359912421
3125421639
1293138521
2311944581").Result;
        Assert.Equal(40, result.Part1);
        Assert.Equal(315, result.Part2);
    }
}
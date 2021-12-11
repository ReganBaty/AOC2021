using Xunit;

namespace AOC.Tests;

public class Day11Tests
{
    [Fact]
    public void Test()
    {
        var day = new Day11();
        var result = day.Run(@"5483143223
2745854711
5264556173
6141336146
6357385478
4167524645
2176841721
6882881134
4846848554
5283751526").Result;
        Assert.Equal(1656, result.Part1);
        Assert.Equal(195, result.Part2);
    }

}
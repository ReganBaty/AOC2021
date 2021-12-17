using Xunit;

namespace AOC.Tests;

public class Day14Tests
{
    [Fact]
    public void Test()
    {
        var day = new Day14();
        var result = day.Run(@"NNCB

CH -> B
HH -> N
CB -> H
NH -> C
HB -> C
HC -> B
HN -> C
NN -> C
BH -> H
NC -> B
NB -> B
BN -> B
BB -> N
BC -> B
CC -> N
CN -> C").Result;
        Assert.Equal(1588, result.Part1);
        Assert.Equal(2188189693529, result.Part2);
    }
}
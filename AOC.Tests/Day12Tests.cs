using Xunit;

namespace AOC.Tests;

public class Day12Tests
{
    [Fact]
    public void Test()
    {
        var day = new Day12();
        var result = day.Run(@"start-A
start-b
A-c
A-b
b-d
A-end
b-end").Result;
        Assert.Equal(10, result.Part1);
        Assert.Equal(36, result.Part2);
    }


    [Fact]
    public void Test2()
    {
        var day = new Day12();
        var result = day.Run(@"fs-end
he-DX
fs-he
start-DX
pj-DX
end-zg
zg-sl
zg-pj
pj-he
RW-he
fs-DX
pj-RW
zg-RW
start-pj
he-WI
zg-he
pj-fs
start-RW").Result;
        Assert.Equal(226, result.Part1);
        Assert.Equal(3509, result.Part2);
    }

    [Fact]
    public void Test3()
    {
        var day = new Day12();
        var result = day.Run(@"dc-end
HN-start
start-kj
dc-start
dc-HN
LN-dc
HN-end
kj-sa
kj-HN
kj-dc").Result;
        Assert.Equal(19, result.Part1);
        Assert.Equal(103, result.Part2);
    }

}
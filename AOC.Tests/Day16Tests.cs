using Xunit;

namespace AOC.Tests;

public class Day16Tests
{
    [Fact]
    public void Test()
    {
        var day = new Day16();
        var result = day.Run(@"8A004A801A8002F478").Result;
        Assert.Equal(16, result.Part1);
        Assert.Equal(0, result.Part2);
    }

    
    [Fact]
    public void Test2()
    {
        var day = new Day16();
        var result = day.Run(@"620080001611562C8802118E34").Result;
        Assert.Equal(12, result.Part1);
        Assert.Equal(0, result.Part2);
    }

    [Fact]
    public void Test3()
    {
        var day = new Day16();
        var result = day.Run(@"C0015000016115A2E0802F182340").Result;
        Assert.Equal(23, result.Part1);
        Assert.Equal(0, result.Part2);
    }
    
    [Fact]
    public void Test4()
    {
        var day = new Day16();
        var result = day.Run(@"A0016C880162017C3686B18A3D4780").Result;
        Assert.Equal(31, result.Part1);
        Assert.Equal(0, result.Part2);
    }

}
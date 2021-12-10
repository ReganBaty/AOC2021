using Xunit;

namespace AOC.Tests;

public class Day10Tests
{
    [Fact]
    public void Test()
    {
        var day = new Day10();
        var result = day.Run(@"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]").Result;
        Assert.Equal(26397, result.Part1);
        Assert.Equal(288957, result.Part2);
    }

}
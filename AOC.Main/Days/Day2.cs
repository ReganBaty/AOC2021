using System;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day2 : IDay
    {
        public const int Day = 2;
        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            int horz = 0, depthDay1 = 0, depthDay2 = 0, aim = 0;

            foreach (var commands in input.Split('\n'))
            {
                var splitCommands = commands.Split(' ');
                if (splitCommands.Length > 1)
                {
                    var command = splitCommands[0];
                    var quantity = Convert.ToInt32(splitCommands[1]);
                    switch (command)
                    {
                        case "forward":
                            horz += quantity;
                            depthDay2 += aim * quantity;
                            break;
                        case "back":
                            horz -= quantity;
                            depthDay2 -= aim * quantity;
                            break;
                        case "down":
                            depthDay1 += quantity;
                            aim += quantity;
                            break;
                        case "up":
                            depthDay1 -= quantity;
                            aim -= quantity;
                            break;
                    }
                }
            }

            return new Results(horz * depthDay1, horz * depthDay2);
        }

    }
}


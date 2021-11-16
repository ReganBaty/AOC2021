using System;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day1 : IDay
    {
        public const int Day = 1;
        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            int floor = 0;
            int position = 0;
            foreach (char c in input)
            {
                position++;
                switch (c)
                {
                    case '(':
                        floor++;
                        break;
                    case ')':
                        floor--;
                        break;
                }
            }

            return new Results(floor, position);
        }
    }
}

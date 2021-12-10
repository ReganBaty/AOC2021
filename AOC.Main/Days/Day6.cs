using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day6 : IDay
    {
        public const int Day = 6;
        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            Dictionary<int, long> fish = new();

            fish.Add(0, 0);
            fish.Add(1, 0);
            fish.Add(2, 0);
            fish.Add(3, 0);
            fish.Add(4, 0);
            fish.Add(5, 0);
            fish.Add(6, 0);
            fish.Add(7, 0);
            fish.Add(8, 0);

            foreach (var s in input.Split(",").Select(x => Convert.ToInt32(x)))
            {
                fish[s]++;
            }

            var result = 0L;

            for (int i = 0; i < 256; i++)
            {
                long newFish = fish[0];
                fish[0] = fish[1];
                fish[1] = fish[2];
                fish[2] = fish[3];
                fish[3] = fish[4];
                fish[4] = fish[5];
                fish[5] = fish[6];
                fish[6] = fish[7] + newFish;
                fish[7] = fish[8];
                fish[8] = newFish;

                if (i == 79)
                {
                    result = fish.Select(x => x.Value).Sum();
                }
            }

            return new Results(result, fish.Select(x => x.Value).Sum());
        }
    }
}


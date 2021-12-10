using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day7 : IDay
    {
        public const int Day = 7;
        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var crabPositions = input.Split(",").Select(x => Convert.ToInt32(x)).ToList();

            int leastFuelUsed = int.MaxValue;
            int sinceChange = 0;
            for (int i = 0; i < int.MaxValue; i++)
            {
                var fuelUsed = crabPositions.Sum(x => Math.Abs(x - i));

                if (fuelUsed < leastFuelUsed)
                {
                    leastFuelUsed = fuelUsed;
                    sinceChange = 0;
                }

                sinceChange++;
                if (sinceChange > 200)
                    break;
            }


            int leastFuelUsed2 = int.MaxValue;
            sinceChange = 0;
            for (int i = 0; i < int.MaxValue; i++)
            {
                var fuelUsed = crabPositions.Sum(x => GetUsed(i, x));

                if (fuelUsed < leastFuelUsed2)
                {
                    leastFuelUsed2 = fuelUsed;
                    sinceChange = 0;
                }

                sinceChange++;
                if (sinceChange > 200)
                    break;
            }


            return new Results(leastFuelUsed, leastFuelUsed2);
        }

        public int GetUsed(int place, int crabPos)
        {
            int distance = Math.Abs(place - crabPos);
            int used = 0;
            for (int i = 1; i <= distance; i++)
            {
                used += i;
            }
            return used;
        }
    }
}


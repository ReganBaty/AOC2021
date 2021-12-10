using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day5 : IDay
    {
        public const int Day = 5;
        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            int[,] map = new int[1000,1000];
            int[,] map2 = new int[1000,1000];

            var lines = input.Replace(" -> ", ",").Split().Where(x => !string.IsNullOrWhiteSpace(x));
            foreach (var l in lines)
            {
                var numbers = l.Split(',').Select(x => Convert.ToInt32(x)).ToList();

                int x1 = numbers[0];
                int y1 = numbers[1];
                int x2 = numbers[2];
                int y2 = numbers[3];

                if (x1 == x2 || y1 == y2) // Horizontal lines
                {
                    if (x1 == x2)
                    {
                        int lineLength = Math.Abs(y2 - y1);

                        for (int i = 0; Math.Abs(i) <= Math.Abs(lineLength);)
                        {
                            map[x1, y1 + i]++;
                            map2[x1, y1 + i]++;

                            if (i < y2 - y1)
                                i++;
                            else if (i == y2 - y1)
                                break;
                            else
                                i--;
                        }
                    }

                    if (y1 == y2)
                    {
                        int lineLength = Math.Abs(x2 - x1);

                        for (int i = 0; Math.Abs(i) <= Math.Abs(lineLength);)
                        {
                            map[x1 + i, y1]++;
                            map2[x1 + i, y1]++;

                            if (i < x2 - x1)
                                i++;
                            else if (i == x2 - x1)
                                break;
                            else
                                i--;
                        }
                    }
                }
                else // Diagonal lines
                {
                    var steps = Math.Abs(x2 - x1);
                    int xDirection = x1 > x2 ? -1 : 1;
                    int yDirection = y1 > y2 ? -1 : 1;

                    for (int i = 0; Math.Abs(i) <= Math.Abs(steps); i++)
                    {
                        int markX = x1 + (i * xDirection);
                        int markY = y1 + (i * yDirection); 
                        map2[markX, markY]++;
                    }
                }
            }

            var result = map.Cast<int>().Count(x => x >= 2);
            var result2 = map2.Cast<int>().Count(x => x >= 2);          

            return new Results(result, result2);
        }
    }
}


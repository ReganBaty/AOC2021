using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day13 : IDay
    {
        public const int Day = 13;

        public int PathCounter;
        public int PathCounter2;

        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var lines = input.Split('\n', '\r').Where(s => !string.IsNullOrWhiteSpace(s));

            int xSize = 0;
            int ySize = 0;

            int result = 0;

            var xys = new List<(int, int)>();

            foreach (var l in lines)
            {
                if (l[0] != 'f')
                {
                    var t = l.Split(',');

                    var x = Convert.ToInt32(t[0]);
                    var y = Convert.ToInt32(t[1]);

                    xSize = x > xSize ? x : xSize;
                    ySize = y > ySize ? y : ySize;

                    xys.Add((x, y));
                }
            }
            
            bool[,] map = new bool[xSize + 1, ySize + 1];

            foreach (var xy in xys)
            {
                map[xy.Item1, xy.Item2] = true;
            }

            foreach (var l in lines.Where(x => x[0] == 'f'))
            {
                var fold = l.Split()[2].Split('=');

                int along = Convert.ToInt32(fold[1]);
                if (fold[0] == "y")
                {
                    for (int i = 0; i <= xSize; i++)
                    {
                        for (int j = 0; j <= ySize / 2; j++)
                        {
                            map[i, j] = map[i, j] || map[i, ySize - j];
                        }
                    }
                    ySize = ySize / 2 - 1;
                }
                else
                {
                    for (int i = 0; i <= xSize; i++)
                    {
                        for (int j = 0; j <= ySize; j++)
                        {
                            map[i, j] = map[i, j] || map[xSize - i, j];
                        }
                    }
                    xSize = xSize / 2 - 1;
                }

                if (result == 0)
                {
                    for (int i = 0; i <= xSize; i++)
                    {
                        for (int j = 0; j <= ySize; j++)
                        {
                            if (map[i, j])
                                result++;
                        }
                    }
                }
            }

            for (int i = 0; i <= ySize; i++)
            {
                Console.WriteLine();
                for (int j = 0; j <= xSize; j++)
                {
                    if (map[j, i])
                        Console.Write('#');
                    else
                        Console.Write(' ');
                }
            }
            Console.WriteLine();


            return new Results(result, 0);
        }
    }
}


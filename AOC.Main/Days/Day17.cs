using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day17 : IDay
    {
        public const int Day = 17;

        public int PacketCount = 0;

        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var line = input.Substring(13).Split(',');

            var xStr = line[0].Substring(2).Split("..");
            var yStr = line[1].Substring(3).Split("..");

            var xTarget1 = Convert.ToInt32(xStr[0]);
            var xTarget2 = Convert.ToInt32(xStr[1]);
            var yTarget1 = Convert.ToInt32(yStr[0]);
            var yTarget2 = Convert.ToInt32(yStr[1]);

            var goodX = new List<int>();
            var goodY = new List<int>();
            for (int i = 1; i <= xTarget2; i++)
            {
                int x = 0;
                int xV = i;

                while (true)
                {
                    x += xV;
                    xV += xV > 0 ? -1 : xV < 0 ? 1 : 0;

                    if (x >= xTarget1 && x <= xTarget2)
                    {
                        goodX.Add(i);
                        break;
                    }
                    else if (x > xTarget2 || xV == 0)
                    {
                        break;
                    }
                }
            }

            for (int i = yTarget1; i < -yTarget1; i++)
            {
                int y = 0;
                int yV = i;

                while (true)
                {
                    y += yV;
                    yV--;

                    if (y >= yTarget1 && y <= yTarget2)
                    {
                        goodY.Add(i);
                        break;
                    }
                    else if (y < yTarget1)
                    {
                        break;
                    }
                }
            }

            int bestY = 0;
            int goodCount = 0;
            foreach (int i in goodX)
            {
                foreach (int j in goodY)
                {
                    int x = 0, y = 0;
                    int xV = i, yV = j;
                    int topY = 0;
                    bool working = false;
                    while (true)
                    {
                        x += xV;
                        y += yV;
                        xV += xV > 0 ? -1 : xV < 0 ? 1 : 0;
                        yV--;

                        if (y > topY)
                            topY = y;

                        if (x >= xTarget1 && x <= xTarget2 && y >= yTarget1 && y <= yTarget2)
                        {
                            if (!working)
                            {
                                goodCount++;
                                working = true;
                            }                        

                            if (topY > bestY)
                            {
                                bestY = topY;
                                break;
                            }
                        }
                        else if (x > xTarget2 || y < yTarget1)
                        {
                            break;
                        }
                    }
                }
            }

            return new Results(bestY, goodCount);
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day11 : IDay
    {
        public const int Day = 11;
        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var lines = input.Split('\n', '\r').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            var lineLength = lines.First().Length;
            int[] heights = new int[lines.Count * lineLength];

            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lineLength; j++)
                {
                    heights[i * lineLength + j] = int.Parse(lines[i][j].ToString());
                }
            }

            var elementCount = heights.Length;
            var results = 0;
            var result2 = 0;
            for (int i = 0;;i++)
            {
                heights = heights.Select(i => i += 1).ToArray();
                HashSet<int> flashed = new();

                var changes = false;
                do
                {
                    changes = false;
                    for (int j = 0; j < heights.Length; j++)
                    {
                        if (heights[j] > 9 && !flashed.Contains(j))
                        {
                            int abovePositon = j - lineLength;
                            bool aboveValid = abovePositon >= 0 && abovePositon < elementCount;                   

                            int belowPosition = j + lineLength;
                            bool belowValid = belowPosition >= 0 && belowPosition < elementCount;                   

                            int leftPosition = j - 1;
                            bool leftValid = j % lineLength != 0 && leftPosition >= 0 && leftPosition < elementCount;   

                            int rightPosition = j + 1;
                            bool rightValid = j % lineLength != lineLength - 1 && rightPosition >= 0 && rightPosition < elementCount;
                            
                            if (aboveValid)
                                heights[abovePositon]++;

                            if (belowValid)
                                heights[belowPosition]++;

                            if (leftValid)
                                heights[leftPosition]++;

                            if (rightValid)
                                heights[rightPosition]++;

                            if (leftValid && aboveValid)
                                heights[leftPosition - lineLength]++;

                            if (leftValid && belowValid)
                                heights[leftPosition + lineLength]++;

                            if (rightValid && aboveValid)
                                heights[rightPosition - lineLength]++;

                            if (rightValid && belowValid)
                                heights[rightPosition + lineLength]++;

                            flashed.Add(j);
                            if (i < 100)
                                results++;
                            changes = true;
                        }

                    }
                } while (changes == true);

                heights = heights.Select(i => i > 9 ? 0 : i).ToArray();

                if (flashed.Count == elementCount)
                {
                    result2 = i + 1;
                    break;
                }
            }


            return new Results(results, result2);
        }
    }
}


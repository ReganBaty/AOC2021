using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day10 : IDay
    {
        public const int Day = 10;
        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            Dictionary<char, int> points = new()
            {
                { ')', 3 },
                { ']', 57 },
                { '}', 1197 },
                { '>', 25137 },
            };

            Dictionary<char, int> points2 = new()
            {
                { ')', 1 },
                { ']', 2 },
                { '}', 3 },
                { '>', 4 },
            };

            Dictionary<char, char> openToClose = new()
            {
                { '(', ')' },
                { '[', ']' },
                { '{', '}' },
                { '<', '>' },
            };

            Dictionary<char, char> closeToOpen = new()
            {
                { ')', '(' },
                { ']', '[' },
                { '}', '{' },
                { '>', '<' },
            };

            var lines = input.Split('\n', '\r').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();
            var lines2 = lines.Select(l => l).ToList();

            var results = 0;
            foreach (var l in lines)
            {
                List<char> recentOpens = new();

                foreach (char c in l)
                {
                    if (openToClose.TryGetValue(c, out var value))
                    {
                        recentOpens.Add(c);
                    }
                    else if (closeToOpen.TryGetValue(c, out var open))
                    {
                        if (recentOpens.Last() == open)
                        {
                            recentOpens.RemoveAt(recentOpens.Count - 1);
                        }
                        else
                        {
                            results += points[c];
                            lines2.Remove(l);
                            break;
                        }
                    }
                }
            }

            List<long> totalScores = new();

            foreach (var l in lines2)
            {
                List<char> recentOpens = new();
                foreach (char c in l)
                {
                    if (openToClose.TryGetValue(c, out var value))
                    {
                        recentOpens.Add(c);
                    }
                    else if (closeToOpen.TryGetValue(c, out var open))
                    {
                        if (recentOpens.Last() == open)
                        {
                            recentOpens.RemoveAt(recentOpens.Count - 1);
                        }
                        else
                        {
                            results += points[c];
                            lines2.Remove(l);
                            break;
                        }
                    }
                }
                recentOpens.Reverse();

                List<char> added = new();

                foreach (var c in recentOpens)
                {
                    added.Add(openToClose[c]);
                }

                long totalScore = 0;

                for (int i = 0; i < added.Count; i++)
                {
                    totalScore *= 5;
                    totalScore += points2[added[i]];
                }

                totalScores.Add(totalScore);
            }

            totalScores.Sort(new LongSorter());

            return new Results(results, totalScores[totalScores.Count / 2]);
        }

        public class LongSorter : IComparer<long>
        {
            public int Compare(long c1, long c2)
            {
                return c1.CompareTo(c2);
            }
        }
    }

}


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day14 : IDay
    {
        public const int Day = 14;

        public int PathCounter;
        public int PathCounter2;

        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var lines = input.Split('\n', '\r').Where(s => !string.IsNullOrWhiteSpace(s));

            var polymer = lines.First();
            var rules = new Dictionary<string, char>();
            var currentCount = new Dictionary<string, long>();
            var points = new Dictionary<char, long>();

            var results = 0L;

            foreach (var l in lines.Skip(1))
            {
                var t = l.Split(" -> ");
                var from = t[0];
                var to = t[1];
                rules.Add(from, Convert.ToChar(to));
            }

            for (int i = 1; i < polymer.Length; i++)
            {
                var check = "" + polymer[i - 1] + polymer[i];
                currentCount[check] = currentCount.ContainsKey(check) ? currentCount[check] + 1: 1;
            }

            foreach (var c in polymer)
            {
                points[c] = points.ContainsKey(c) ? points[c] + 1 : 1;
            }

            for (int i = 0; i < 40; i++)
            {
                var originalDict = currentCount.ToDictionary(s => s.Key, s => s.Value);
                Dictionary<string, long> result = new Dictionary<string, long>();

                foreach (var c in originalDict)
                {
                    char newElem = rules[c.Key];
                    points[newElem] = points.ContainsKey(newElem) ? points[newElem] + c.Value : c.Value;

                    var pair1 = "" + c.Key[0] + rules[c.Key];
                    var pair2 = "" + rules[c.Key] + c.Key[1];

                    result[pair1] = result.ContainsKey(pair1) ? result[pair1] + c.Value : c.Value;
                    result[pair2] = result.ContainsKey(pair2) ? result[pair2] + c.Value : c.Value;
                }

                currentCount = result;

                if (i == 9)
                {
                    long max = points.Values.Max();
                    long min = points.Values.Min();

                    results = max - min;
                }
                if (i == 39)
                {
                    long max = points.Values.Max();
                    long min = points.Values.Min();

                    return new Results(results, max - min);
                }
            }

            return new Results(0, 0);
        }
    }
}


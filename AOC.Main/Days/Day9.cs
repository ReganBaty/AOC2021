using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day9 : IDay
    {
        public const int Day = 9;
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

            var results = 0;
            var elementCount = lines.Count * lineLength;
            List<int> lowPoints = new();

            for (int i = 0; i < elementCount; i++)
            {
                int abovePositon = i - lineLength;

                if (abovePositon >= 0 && abovePositon < elementCount && heights[i] >= heights[abovePositon])
                    continue;

                int belowPosition = i + lineLength;

                if (belowPosition >= 0 && belowPosition < elementCount && heights[i] >= heights[belowPosition])
                    continue;

                int leftPosition = i - 1;
                bool leftSameLine = i % lineLength != 0;

                if (leftPosition >= 0 && leftPosition < elementCount && leftSameLine && heights[i] >= heights[leftPosition])
                    continue;

                int rightPosition = i + 1;
                bool rightSameLine = i % lineLength != lineLength - 1;

                if (rightPosition >= 0 && rightPosition < elementCount && rightSameLine && heights[i] >= heights[rightPosition])
                    continue;

                results += 1 + heights[i];

                lowPoints.Add(i);
            }

            List<int> results2 = new();
            foreach (int i in lowPoints)
            {
                var checkedSet = new HashSet<int>();

                List<int> toCheck = new List<int>();

                toCheck.Add(i);

                Checker(toCheck.First(), lineLength, toCheck, checkedSet, elementCount, heights);

                results2.Add(checkedSet.Count());
            }

            results2.Sort(new IntSorter());

            var sortedBasins = results2.Take(3);
            var result3 = sortedBasins.First();
            foreach (var r in sortedBasins.Skip(1))
            {
                result3 *= r;
            }

            return new Results(results, result3);
        }

        public void Checker(int i, int lineLength, List<int> toCheck, HashSet<int> checked2, int elementCount, int[] heights)
        {

            int abovePositon = i - lineLength;

            if (abovePositon >= 0 && abovePositon < elementCount && heights[abovePositon] != 9)
            {
                if (!checked2.Contains(abovePositon))
                {
                    toCheck.Add(abovePositon);
                }
            }

            int belowPosition = i + lineLength;

            if (belowPosition >= 0 && belowPosition < elementCount && heights[belowPosition] != 9)
            {
                if (!checked2.Contains(belowPosition))
                {
                    toCheck.Add(belowPosition);
                }
            }

            int leftPosition = i - 1;
            bool leftSameLine = i % lineLength != 0;

            if (leftPosition >= 0 && leftPosition < elementCount && leftSameLine && heights[leftPosition] != 9)
            {
                if (!checked2.Contains(leftPosition))
                {
                    toCheck.Add(leftPosition);
                }
            }

            int rightPosition = i + 1;
            bool rightSameLine = i % lineLength != lineLength - 1;

            if (rightPosition >= 0 && rightPosition < elementCount && rightSameLine && heights[rightPosition] != 9)
            {
                if (!checked2.Contains(rightPosition))
                {
                    toCheck.Add(rightPosition);
                }
            }

            toCheck.Remove(i);
            checked2.Add(i);

            if (toCheck.Any())
            Checker(toCheck.First(), lineLength, toCheck, checked2, elementCount, heights);
        }

        public string SortString(string toSort)
        {

            var ordered = toSort.ToList().OrderBy(x => x);

            string blank = null;
            foreach (var c in ordered)
            {
                blank += c;
            }
            return blank;


        }
    }

    public class IntSorter : IComparer<int>
    {
        public int Compare(int c1, int c2)
        {
            return c2.CompareTo(c1);
        }
    }
}


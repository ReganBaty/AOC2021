using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day15 : IDay
    {
        public const int Day = 15;

        public int PathCounter;
        public int PathCounter2;

        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var lines = input.Split('\n', '\r').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            var distances = new Dictionary<int, int>();
            var shortestSet = new HashSet<int>();

            int length = 0;
            int height = 0;
            Node[]? array = null;
            Node[]? bigArray = null;

            for (int i = 0; i < lines.Count(); i++)
            {
                var line = lines[i];

                if (array == default)
                {
                    array = new Node[line.Length * lines.Count];
                    length = line.Length;
                    height = lines.Count;


                    array = new Node[line.Length * lines.Count];
                }

                for (int j = 0; j < line.Length; j++)
                {
                    array[i * line.Length + j] = new Node(i * line.Length + j, Convert.ToInt32(line[j].ToString()));
                }
            }

            Node startNode = array[0];
            startNode.Visit = true;
            startNode.Distance = 0;
            Node currentNode = startNode;

            distances.Add(0, 0);
            var result = 0;

            // Part1
            for (int i = 0; i < array.Length; i++)
            {
                foreach (var c in Connecting(height, length, currentNode.Position))
                {
                    var node = array[c];
                    if (!node.Visit)
                    {
                        int shortest = int.MaxValue;
                        foreach (var d in Connecting(height, length, node.Position))
                        {
                            if (array[d].Visit)
                            {
                                shortest = distances[d] < shortest ? distances[d] : shortest;
                            }
                        }

                        node.Distance = node.Weight + shortest < node.Distance ? node.Weight + shortest : node.Distance;

                        if (distances.TryGetValue(node.Position, out var value))
                        {
                            distances[node.Position] = node.Distance;
                        }
                        else
                        {
                            distances.Add(node.Position, node.Distance);

                        }
                    }
                }

                currentNode = array.Where(s => !s.Visit).OrderBy(s => s.Distance).FirstOrDefault();
                currentNode.Visit = true;

                if (currentNode.Position == array.Last().Position)
                {
                    result = currentNode.Distance;
                    break;
                }
            }



            for (int i = 0; i < lines.Count(); i++)
            {
                var line = lines[i];

                if (bigArray == default)
                {
                    length = line.Length * 5;
                    height = lines.Count * 5;
                    bigArray = new Node[length * height];
                }

                for (int j = 0; j < line.Length; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        for (int t = 0; t < 5; t++)
                        {
                            int value = (Convert.ToInt32(line[j].ToString()) + k + t);

                            while (value >= 10)
                            {
                                value -= 9;
                            }

                            int position = j + (i * length) + t * (lines.Count() * length) + (k * line.Length);

                            var n = new Node(position, value);
                            n.Connected = Connecting(height, length, position);
                            bigArray[position] = n;
                        }
                    }
                }
            }

            startNode = bigArray[0];
            startNode.Visit = true;

            startNode.Distance = 0;
            currentNode = startNode;
            shortestSet.Add(currentNode.Position);
            distances.Clear();
            distances.Add(0, 0);

            // Part2
            do
            {
                foreach (var c in currentNode.Connected)
                {
                    var node = bigArray[c];

                    if (!node.Visit)
                    {
                        int shortest = int.MaxValue;
                        foreach (var d in node.Connected)
                        {
                            if (bigArray[d].Visit)
                            {
                                shortest = distances[d] < shortest ? distances[d] : shortest;
                            }
                        }

                        node.Distance = node.Weight + shortest < node.Distance ? node.Weight + shortest : node.Distance;

                        if (distances.TryGetValue(node.Position, out var value))
                        {
                            distances[node.Position] = node.Distance;
                        }
                        else
                        {
                            distances.Add(node.Position, node.Distance);

                        }
                    }
                }

                currentNode = bigArray.Where(s => !s.Visit).OrderBy(s => s.Distance).FirstOrDefault();

                shortestSet.Add(currentNode.Position);
                currentNode.Visit = true;

                if (currentNode.Position == bigArray.Last().Position)
                {
                    return new Results(result, currentNode.Distance);
                    break;
                }


            } while (true);

            return new Results(0, 0);
        }

        public class Node
        {
            public int Position;
            public int Weight;
            public int Distance;
            public bool Visit;
            public int[] Connected;

            public Node(int position, int weight)
            {
                Position = position;
                Weight = weight;
                Distance = int.MaxValue;
            }
        }

        public int[] Connecting(int height, int length, int current)
        {

            var toReturn = new List<int>();

            int abovePositon = current - height;
            bool aboveValid = abovePositon >= 0 && abovePositon < height * length;

            int belowPosition = current + height;
            bool belowValid = belowPosition >= 0 && belowPosition < height * length;

            int leftPosition = current - 1;
            bool leftValid = current % length != 0 && leftPosition >= 0 && leftPosition < height * length;

            int rightPosition = current + 1;
            bool rightValid = current % length != length - 1 && rightPosition >= 0 && rightPosition < height * length;

            if (aboveValid)
                toReturn.Add(abovePositon);

            if (belowValid)
                toReturn.Add(belowPosition);

            if (leftValid)
                toReturn.Add(leftPosition);

            if (rightValid)
                toReturn.Add(rightPosition);

            return toReturn.ToArray();
        }
    }
}


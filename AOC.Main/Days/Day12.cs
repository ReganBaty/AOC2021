using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day12 : IDay
    {
        public const int Day = 12;

        public Dictionary<string, Cave> Caves = new Dictionary<string, Cave>();
        public int PathCounter;
        public int PathCounter2;

        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var lines = input.Split('\n', '\r').Where(s => !string.IsNullOrWhiteSpace(s));

            foreach (var l in lines)
            {
                var t = l.Split('-');
                var origin = t[0];

                if (!Caves.TryGetValue(origin, out var originCave))
                {
                    Caves.Add(origin, originCave = new Cave()
                    {
                        Name = origin,
                        Big = origin.ToUpper() == origin,
                    });
                }

                var destination = t[1];

                if (!Caves.TryGetValue(destination, out var destinationCave))
                {
                    Caves.Add(destination, destinationCave = new Cave()
                    {
                        Name = destination,
                        Big = destination.ToUpper() == destination,
                    });
                }

                originCave.Connecting.Add(destinationCave);
                destinationCave.Connecting.Add(originCave);
            }

            Explore(Caves["start"]);

            return new Results(PathCounter, PathCounter2);
        }

        public void Explore(Cave cave)
        {
            if (!cave.Big)
                cave.Visited++;

            foreach (var c in cave.Connecting)
            {
                if (c.Name == "end")
                {
                    if (Caves.All(c => c.Value.Visited < 2))
                        PathCounter++;

                    PathCounter2++;
                }
                else if (c.Name != "start")
                {
                    if (ValidToExplore(c))
                        Explore(c);
                }
            }

            if (!cave.Big)
                cave.Visited--;
        }

        public bool ValidToExplore(Cave cave)
        {
            if (cave.Big)
                return true;

            if (cave.Visited > 0 && Caves.Any(c => c.Value.Visited == 2))
                return false;

            return true;
        }

        public class Cave
        {
            public bool Big { get; set; }
            public int Visited { get; set; }
            public List<Cave> Connecting { get; set; }
            public string Name { get; set; }

            public Cave()
            {
                Connecting = new();
                Name = String.Empty;
            }
        }
    }
}


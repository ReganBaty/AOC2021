using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day4 : IDay
    {
        public const int Day = 4;
        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var lines = input.Split().Where(x => !string.IsNullOrWhiteSpace(x));
            var pickedNumbers = lines.First().Split(',').Select(x => Convert.ToInt32(x));

            var currentBoard = new Board();
            var boards = new List<Board>()
            {
                currentBoard
            };

            foreach (var s in lines.Skip(1))
            {
                if (currentBoard.BoardEntries.Count == 25)
                {
                    currentBoard = new Board();
                    boards.Add(currentBoard);
                }

                currentBoard.AddBoardEntries(s.Split().Select(x => Convert.ToInt32(x)));
            }

            var result = 0;
            var result2 = 0;
            var winners = 0;
            foreach (var n in pickedNumbers)
            {
                foreach (var b in boards)
                {
                    if (!b.Finished && b.ValueLookup.TryGetValue(n, out var entry))
                    {
                        entry.Marked = true;

                        bool winner = b.CheckWin();
                        if (winner && result == 0)
                            result = b.BoardEntries.Where(x => !x.Marked).Select(x => x.Value).Sum() * n;

                        if (winner)
                        {
                            winners++;
                            if (winners == boards.Count)
                            {
                                result2 = b.BoardEntries.Where(x => !x.Marked).Select(x => x.Value).Sum() * n;
                                break;
                            }
                        }
                    }
                }

                if (result2 != 0)
                    break;
            }

            return new Results(result, result2);

        }

        public class Board
        {
            public bool Finished { get; set; }
            public List<BoardEntry> BoardEntries { get; set; }
            public Dictionary<int, BoardEntry> ValueLookup { get; set; }

            public Board()
            {
                BoardEntries = new();
                ValueLookup = new();
            }

            public bool CheckWin()
            {
                for (int i = 0; i < 5; i++)
                {
                    int[] horz = new int[5] { 1 + (5 * i), 2 + (5 * i), 3 + (5 * i), 4 + (5 * i), 5 + (5 * i) };
                    if (BoardEntries.Where(x => horz.Contains(x.Postion)).All(x => x.Marked))
                    {
                        Finished = true;
                        return true;
                    }
                }

                for (int i = 0; i < 5; i++)
                {
                    int[] vert = new int[5] { 1 + i , 6 + i, 11 + i, 16 + i, 21 + i };
                    if (BoardEntries.Where(x => vert.Contains(x.Postion)).All(x => x.Marked))
                    {
                        Finished = true; 
                        return true;
                    }
                }

                return false;
            }

            public void AddBoardEntries(IEnumerable<int> values)
            {
                foreach (var v in values)
                {
                    var entry = new BoardEntry()
                    {
                        Value = v,
                        Postion = BoardEntries.Count + 1,
                    };

                    BoardEntries.Add(entry);
                    ValueLookup.Add(v, entry);
                }      
            }
        }

        public class BoardEntry
        {
            public int Value { get; set; }
            public bool Marked { get; set; }
            public int Postion { get; set; }

        }
    }
}


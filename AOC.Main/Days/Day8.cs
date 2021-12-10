using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day8 : IDay
    {
        public const int Day = 8;
        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var lines = input.Split('\n', '\r').Where(s => !string.IsNullOrWhiteSpace(s)).ToList();

            List<string> outputs = new();

            foreach (var s in lines)
            {
                var split = s.Split('|');
                outputs.Add(split[1]);
            }

            int one = 0;
            int four = 0;
            int seven = 0;
            int eight = 0;

            foreach (var s in outputs)
            {
                var split = s.Split();

                foreach (var t in split)
                {
                    if (t.Length == 2)
                        one++;
                    if (t.Length == 4)
                        four++;
                    if (t.Length == 3)
                        seven++;
                    if (t.Length == 7)
                        eight++;
                }
            }

            var result2 = 0D;
            foreach (var s in lines)
            {
                var split = s.Split('|');
                var digits = split[0].Split().Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
                var outputs2 = split[1].Split().Where(x => !string.IsNullOrWhiteSpace(x)).ToList();

                var oneDigit = digits.Where(i => i.Length == 2).FirstOrDefault();
                var sevenDigit = digits.Where(i => i.Length == 3).FirstOrDefault();
                var eightDigit = digits.Where(i => i.Length == 7).FirstOrDefault();
                var fourDigit = digits.Where(i => i.Length == 4).FirstOrDefault();

                var sixLengthDigits = digits.Where(x => x.Length == 6);

                var sixDigit = sixLengthDigits.Where(x => (x.Contains(oneDigit[0]) || x.Contains(oneDigit[1])) && !(x.Contains(oneDigit[0]) && x.Contains(oneDigit[1]))).First();
                var nineDigit = sixLengthDigits.Where(x => (x != sixDigit && x.Contains(fourDigit[0]) && x.Contains(fourDigit[1]) &&
                    x.Contains(fourDigit[2]) && x.Contains(fourDigit[3]))).First();
                var zeroDigit = sixLengthDigits.Where(x => x != nineDigit && x != sixDigit).First();

                var fiveLengthDigits = digits.Where(x => x.Length == 5);

                var threeDigit = fiveLengthDigits.Where(x => x.Contains(oneDigit[0]) && x.Contains(oneDigit[1])).First();
                var fiveFindChar = oneDigit.Intersect(sixDigit);
                var fiveDigit = fiveLengthDigits.Where(x =>  x != threeDigit && x.Contains(fiveFindChar.First())).FirstOrDefault();
                var twoDigit = fiveLengthDigits.Where(x => x != fiveDigit && x != threeDigit).FirstOrDefault();

                Dictionary<string, int> all = new();

                all.Add(SortString(zeroDigit), 0);
                all.Add(SortString(oneDigit), 1);
                all.Add(SortString(twoDigit), 2);
                all.Add(SortString(threeDigit), 3);
                all.Add(SortString(fourDigit), 4);
                all.Add(SortString(fiveDigit), 5);
                all.Add(SortString(sixDigit), 6);
                all.Add(SortString(sevenDigit), 7);
                all.Add(SortString(eightDigit), 8);
                all.Add(SortString(nineDigit), 9);

                for (int i = 0; i < outputs2.Count(); i++)
                {
                    var output = outputs2[i];
                    var power = outputs2.Count() - i - 1;

                    result2 += all[SortString(output)] * Math.Pow(10, power);
                }
            }


            return new Results(one + four + seven + eight, (int)result2);
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
}


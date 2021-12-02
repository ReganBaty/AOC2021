using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day3 : IDay
    {
        public const int Day = 3;
        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            var numbers = input.Split().Where(x => !string.IsNullOrWhiteSpace(x));
            var highestPower = numbers.First().Length;

            int[] gamma = new int[highestPower], epislon = new int[highestPower];
            var oxygenList = numbers.Select(x => x);
            var carbonList = numbers.Select(x => x);
            for (int i = 0; i < highestPower; i++)
            {
                int count1 = 0;
                foreach (var c in numbers)
                {
                    if (c[i] == '1')
                        count1++;
                }

                if (count1 >= numbers.Count() / 2)
                    gamma[i] = 1;
                else
                    epislon[i] = 1;

                GetValidNumbers(i, '1', '0', ref oxygenList);
                GetValidNumbers(i, '0', '1', ref carbonList);
            }

            var gammaResult = BinaryToInt(gamma);
            var epsilonResult = BinaryToInt(epislon);

            var carbonBinary = carbonList.First().Select(x => x == '1' ? 1 : 0).ToArray();
            var oxygenBinary = oxygenList.First().Select(x => x == '1' ? 1 : 0).ToArray();

            var oxysInt = BinaryToInt(oxygenBinary);
            var cosInt = BinaryToInt(carbonBinary);

            return new Results(gammaResult * epsilonResult, cosInt * oxysInt);
        }

        public static void GetValidNumbers(int i, char baseChar, char otherChar, ref IEnumerable<String> wordList)
        {
            if (wordList.Count() > 1)
            {
                int counter = 0;
                foreach (var c in wordList)
                {
                    if (c[i] == '1')
                        counter++;
                    else
                        counter--;
                }

                if (counter >= 0)
                    wordList = wordList.Where(x => x[i] == baseChar).ToList();
                else
                    wordList = wordList.Where(x => x[i] == otherChar).ToList();
            }
        }

        public static int BinaryToInt(int[] binaryArray)
        {
            int result = 0;
            int highestPower = binaryArray.Length;

            for (int i = 0; i < highestPower; i++)
            {
                result += binaryArray[i] * (int)Math.Pow(2, highestPower - i - 1);
            }

            return result;
        }
    }
}


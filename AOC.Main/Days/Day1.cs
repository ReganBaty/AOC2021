using System;
using System.Linq;
using System.Threading.Tasks;

namespace AOC
{
    public sealed class Day1 : IDay
    {
        public const int Day = 1;
        public const int WindowLength = 3;
        public async Task<Results> Run(string? input)
        {
            if (String.IsNullOrEmpty(input))
                input = await FileGrabber.LoadFile(Day);

            int singleIncreases = 0;
            int windowIncreases = 0;
            var inputs = input.Split('\n');

            int[] window = new int[WindowLength];
            window[0] = Convert.ToInt32(inputs.First());

            foreach (string s in inputs.Skip(1))
            {
                if (int.TryParse(s, out int result))
                {
                    if (result > window[0])
                        singleIncreases++;

                    if (window.All(w => w != 0))
                    {
                        int previousWindow = window.Sum();
                        int thisWindow = previousWindow - window.Last() + result;

                        if (thisWindow > previousWindow)    
                            windowIncreases++;
                    }

                    for (int i = WindowLength; i > 1; i--)
                    {
                        window[i - 1] = window[i - 2];
                    }

                    window[0] = result;
                }
            }

            return new Results(singleIncreases, windowIncreases);
        }
    }
}

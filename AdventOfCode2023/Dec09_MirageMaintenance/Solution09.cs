using AdventOfCode2023.Helpers;

namespace AdventOfCode2023.Dec09
{
    public class Solution09 : ISolution
    {
        public DateTime Date => new DateTime(2023, 12, 09);

        /// <summary>
        /// Start at plot "AAA", follow the instructions, how many steps until plot "ZZZ"?
        /// </summary>
        public long GetSolutionPartOne()
        {
            long total = 0;
            var sequences = Data09.Sequences;
            foreach (var sequence in sequences)
            {
                var numbers = sequence.GetNumberMatches().LongValues();
                var subSequences = new List<List<long>>() { numbers };
                var allDiffZero = false;
                while (!allDiffZero)
                {
                    var subSequence = new List<long>();
                    for (var i = 0; i < numbers.Count - 1; i++)
                    {
                        subSequence.Add(numbers[i + 1] - numbers[i]);
                    }
                    allDiffZero = subSequence.All(n => n == 0);
                    subSequences.Add(subSequence);
                    numbers = subSequence;
                }
                long add = 0;
                for (var j = subSequences.Count - 2; j >= 0; j--)
                {
                    var subSequence = subSequences[j];
                    add = subSequence.Last() + add;
                }
                total += add;
            }
            return total;
        }

        /// <summary>
        /// Start at all plots whose name ends in "A" simultaneously, follow the instructions, 
        /// how many steps until all current plots end in "Z" (simultaneously)?
        /// </summary>
        public long GetSolutionPartTwo()
        {
            long total = 0;
            var sequences = Data09.Sequences;
            foreach (var sequence in sequences)
            {
                var numbers = sequence.GetNumberMatches().LongValues();
                var subSequences = new List<List<long>>() { numbers };
                var allDiffZero = false;
                while (!allDiffZero)
                {
                    var subSequence = new List<long>();
                    for (var i = 0; i < numbers.Count - 1; i++)
                    {
                        subSequence.Add(numbers[i + 1] - numbers[i]);
                    }
                    allDiffZero = subSequence.All(n => n == 0);
                    subSequences.Add(subSequence);
                    numbers = subSequence;
                }
                long subtract = 0;
                for (var j = subSequences.Count - 2; j >= 0; j--)
                {
                    var subSequence = subSequences[j];
                    subtract = subSequence.First() - subtract;
                }
                total += subtract;
            }
            return total;
        }
    }
}

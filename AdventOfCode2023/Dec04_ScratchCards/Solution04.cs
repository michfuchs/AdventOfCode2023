using AdventOfCode2023.Helpers;

namespace AdventOfCode2023.Dec04
{
    public class Solution04 : ISolution
    {
        public DateTime Date => new DateTime(2023, 12, 04);

        /// <summary>
        /// Get total amount of points by winning numbers
        /// 0 winning numbers on card -> 0 points
        /// 1 winning numbers on card -> 1 points
        /// 2 winning numbers on card -> 2 points
        /// 3 winning numbers on card -> 4 points
        /// 4 winning numbers on card -> 8 points
        /// etc.
        /// </summary>
        public long GetSolutionPartOne()
        {
            var total = 0;
            var lines = Data04.Cards;
            var numLines = lines.Count;
            for (var iLine = 0; iLine < numLines; iLine++)
            {
                var line = lines[iLine];
                var winningNumbers = line[10..].Split("|")[0].GetNumberMatches().IntValues();
                var myNumbers = line[10..].Split("|")[1].GetNumberMatches().IntValues();
                var intersects = winningNumbers.Intersect(myNumbers).ToList(); // winning numbers I have
                total += intersects.Count == 0 ? 0 : (int)Math.Pow(2, intersects.Count - 1);
            }
            return total;
        }

        /// <summary>
        /// Get the total number of accumulating scratch cards due to winning subsequent cards
        /// by winning numbers in the current card.
        /// </summary>
        public long GetSolutionPartTwo()
        {
            var total = 0;
            var lines = Data04.Cards;
            var numLines = lines.Count;

            // set up a dictionary to hold the number of cards
            var dictLineCards = new Dictionary<int, int>();
            for (int i = 0; i < numLines; i++)
                dictLineCards[i] = 0;

            for (var iCard = 0; iCard < numLines; iCard++)
            {
                dictLineCards[iCard] += 1; // original card
                var multiplier = dictLineCards[iCard]; // number of cards I have of this specific card
                var line = lines[iCard];
                var winningNumbers = line[10..].Split("|")[0].GetNumberMatches().IntValues();
                var myNumbers = line[10..].Split("|")[1].GetNumberMatches().IntValues(); 
                var intersects = winningNumbers.Intersect(myNumbers).ToList(); // winning numbers I have

                for (var i = 1; i <= intersects.Count; i++)
                {
                    if (iCard + i < numLines) // can't go past end of list
                        dictLineCards[iCard + i] += multiplier; // number of subsequent cards I gain
                }

                total += multiplier; // number of cards of this specific card to total
            }
            return total;
        }

    }
}

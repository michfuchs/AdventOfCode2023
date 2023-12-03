using AdventOfCode2023.Helpers;
using System.Text.RegularExpressions;

namespace AdventOfCode2023.Dec03
{
    public class Puzzle03 : IPuzzle
    {
        public DateTime Date => new DateTime(2023, 12, 03);

        /// <summary>
        /// Sum all numbers which have symbols adjacent to them
        /// </summary>
        public int GetSolutionPartOne()
        {
            var total = 0;
            var lines = GondolaEngine.GearRatios;
            var numLines = lines.Count;
            var lineLength = lines[0].Length;
            for (var iLine = 0; iLine < numLines; iLine++)
            {
                //Console.Write($"Line {iLine}: ");
                var line = lines[iLine];
                var lineAbove = iLine == 0 ? null : lines[iLine - 1];
                var lineBelow = iLine == numLines - 1 ? null : lines[iLine + 1];
                var numberMatches = line.GetNumberMatches();
                foreach (Match numberMatch in numberMatches)
                {
                    // get number
                    var number = numberMatch.IntValue();

                    // get index of number
                    var indexOfNumber = numberMatch.Index;

                    // find adjacent symbols
                    var hasSymbolLeft = line.GetSymbolAtIndex(indexOfNumber - 1).HasValue; // same line left
                    var hasSymbolRight = line.GetSymbolAtIndex(indexOfNumber + number.ToString().Length).HasValue; // same line right
                    var hasSymbolAbove = lineAbove?.SubstringByStartEnd(indexOfNumber - 1, indexOfNumber + numberMatch.Length + 1)?.ContainsSymbol() ?? false; // line above
                    var hasSymbolBelow = lineBelow?.SubstringByStartEnd(indexOfNumber - 1, indexOfNumber + numberMatch.Length + 1)?.ContainsSymbol() ?? false; // line below

                    // sum number if a symbol is adjacent
                    if (hasSymbolLeft || hasSymbolRight || hasSymbolAbove || hasSymbolBelow)
                    {
                        total += number ?? 0;
                        //Console.Write($"{number} ");
                    }
                }
                //Console.Write(Environment.NewLine);
            }
            return total;
        }

        /// <summary>
        /// Sum the product of all numbers which are connected by an adjacent asterisk
        /// </summary>
        public int GetSolutionPartTwo()
        {
            var total = 0;
            var lines = GondolaEngine.GearRatios;
            var numLines = lines.Count;
            for (var iLine = 0; iLine < numLines; iLine++)
            {
                //Console.Write($"Line {iLine}: ");
                var line = lines[iLine];
                var lineAbove = iLine == 0 ? null : lines[iLine - 1];
                var lineBelow = iLine == numLines - 1 ? null : lines[iLine + 1];
                var asteriskIndexes = line.GetMatches("*").Indexes();
                foreach (var asteriskIndex in asteriskIndexes)
                {
                    var numbers = new List<int>();

                    // find adjacent numbers
                    numbers.AddIfNotNull(line.GetNumberAtIndex(asteriskIndex - 1)); // same line left
                    numbers.AddIfNotNull(line.GetNumberAtIndex(asteriskIndex + 1)); // same line right
                    numbers.AddRange(lineAbove.GetNumbersAtIndexes(asteriskIndex - 1, asteriskIndex + 1)); // line above
                    numbers.AddRange(lineBelow.GetNumbersAtIndexes(asteriskIndex - 1, asteriskIndex + 1));  // line below

                    // sum gear ratio
                    if (numbers.Count > 1)
                    {
                        total += numbers.Aggregate((x, y) => x * y);
                        //Console.Write($"{string.Join("*", numbers)}  ");
                    }
                }
                //Console.Write(Environment.NewLine);
            }
            return total;
        }


    }
}

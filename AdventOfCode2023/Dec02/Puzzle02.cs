using AdventOfCode2023.Helpers;

namespace AdventOfCode2023.Dec02
{
    public class Puzzle02 : IPuzzle
    {
        public DateTime Date => new DateTime(2023, 12, 02);

        /// <summary>
        /// Check for each game if it was possible with a set number of red/green/blue cubes available.
        /// If so, add game number to sum.
        /// </summary>
        public int GetSolutionPartOne()
        {
            var total = 0;
            foreach (var game in CubeConundrum.Games)
            {
                var gameNumber = game.Split(":")[0].NumbersOnly();
                var sets = game.Split(":")[1];
                var draws = sets.Split(',', ';');

                var maxRed = 0;
                var maxGreen = 0;
                var maxBlue = 0;
                foreach (var draw in draws)
                {
                    var n = draw.NumbersOnly();
                    if (draw.Contains("red") && n > maxRed) maxRed = n.Value;
                    if (draw.Contains("green") && n > maxGreen) maxGreen = n.Value;
                    if (draw.Contains("blue") && n > maxBlue) maxBlue = n.Value;
                }

                if (maxRed <= CubeConundrum.MaxRed && maxGreen <= CubeConundrum.MaxGreen && maxBlue <= CubeConundrum.MaxBlue)
                    total += gameNumber ?? 0;
            }
            return total;
        }

        /// <summary>
        /// Get the minimum number of red/green/blue cubes to produce the game result
        /// Sum up the power of each of these games
        /// </summary>
        public int GetSolutionPartTwo()
        {
            var total = 0;
            foreach (var game in CubeConundrum.Games)
            {
                var gameNumber = game.Split(":")[0].NumbersOnly();
                var sets = game.Split(":")[1];
                var draws = sets.Split(',', ';');

                var maxRed = 0;
                var maxGreen = 0;
                var maxBlue = 0;
                foreach (var draw in draws)
                {
                    var n = draw.NumbersOnly();
                    if (draw.Contains("red") && n > maxRed) maxRed = n.Value;
                    if (draw.Contains("green") && n > maxGreen) maxGreen = n.Value;
                    if (draw.Contains("blue") && n > maxBlue) maxBlue = n.Value;
                }
                total += maxRed * maxGreen * maxBlue;
            }
            return total;
        }
    }
}

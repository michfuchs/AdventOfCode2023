namespace AdventOfCode2023.Dec06
{
    public class Solution06 : ISolution
    {
        public DateTime Date => new DateTime(2023, 12, 06);

        /// <summary>
        /// Get all ways you can win the races by pressing the button the right amount of time.
        /// Multiply the ways per race to get the solution.
        /// </summary>
        public long GetSolutionPartOne()
        {
            var total = 1;
            var races = Data06.RacesOne;
            foreach (var race in races)
            {
                var winning = 0;
                for (var iTime = 0; iTime <= race.Time; iTime++)
                {
                    var distance = iTime * (race.Time - iTime);
                    if (distance > race.Distance) // better than current record - winning
                        winning++;
                }
                total *= winning;
            }
            return total;
        }

        /// <summary>
        /// Get all ways you can win the race by pressing the button the right amount of time.
        /// </summary>
        public long GetSolutionPartTwo()
        {
            long total = 0;
            var race = Data06.RaceTwo;
            for (var iTime = 0; iTime <= race.Time; iTime++)
            {
                var distance = iTime * (race.Time - iTime);
                if (distance > race.Distance) // better than current record - winning
                    total++;
            }
            return total;
        }

    }
}

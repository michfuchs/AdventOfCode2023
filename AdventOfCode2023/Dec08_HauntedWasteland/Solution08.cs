using AdventOfCode2023.Helpers;

namespace AdventOfCode2023.Dec08
{
    public class Solution08 : ISolution
    {
        public DateTime Date => new DateTime(2023, 12, 08);

        /// <summary>
        /// Start at plot "AAA", follow the instructions, how many steps until plot "ZZZ"?
        /// </summary>
        public long GetSolutionPartOne()
        {
            var total = 0;
            var dictPlots = Data08.DesertPlots.ToDictionary(d => d.Name);
            var currentPlot = dictPlots["AAA"];
            do
            {
                foreach (var c in Data08.Instructions.ToCharArray())
                {
                    total++;
                    currentPlot = c == 'L' ? dictPlots[currentPlot.Left] : dictPlots[currentPlot.Right];
                    if (currentPlot.Name == "ZZZ")
                        break;
                }
            } while (currentPlot.Name != "ZZZ");
            return total;
        }

        /// <summary>
        /// Start at all plots whose name ends in "A" simultaneously, follow the instructions, 
        /// how many steps until all current plots end in "Z" (simultaneously)?
        /// </summary>
        public long GetSolutionPartTwo()
        {
            long total = 0;
            var dictPlots = Data08.DesertPlots.ToDictionary(p => p.Name);
            var startPlots = Data08.DesertPlots.Where(d => d.IsStartPlot).ToList();
            var instructionChars = Data08.Instructions.ToCharArray();
            var cycles = new List<long>();
            var countBetweenCycles = 0;
            foreach (var startPlot in startPlots)
            {
                var cycleFound = false;
                var currentPlot = startPlot;
                while (!cycleFound)
                {
                    foreach (var c in instructionChars)
                    {
                        countBetweenCycles++;
                        currentPlot = c == 'L' ? dictPlots[currentPlot.Left] : dictPlots[currentPlot.Right];
                        if (currentPlot.IsEndPlot)
                        {
                            // The data is simplified in the way that the End plot is referring to the same plots the Start block does.
                            // So the cycle always stays the same length, instead of being a different length first, and only then repeating.
                            // It's x-x-x-x-x-... instead of y-x-x-x-x-...
                            // This means we can stop the first time an End plot is encountered
                            cycleFound = true;
                            cycles.Add(countBetweenCycles);
                            countBetweenCycles = 0;
                            break;
                        }
                    }
                } 
            }
            //for (var i = 0; i < cycles.Count(); i++)
            //    Console.WriteLine($"Cycle {i}: {cycles[i]}");

            total = MathHelper.LeastCommonMultiple(cycles); // find least common multipliers of cycle
            return total;
        }


        // Brute force... takes too long. Stopped at 255'000'000'000 steps (2.55E+11) after about 10h runtime.
        // The number of combinations when picking 6 out of 750 is 2.42285E+14, which results in an expected calculation time of ~200 days.
        // Effective solution would have taken ~53 days.
        ///// <summary>
        ///// Start at all plots whose name ends in "A" simultaneously, follow the instructions, how many steps until all current plots end in "Z" (simultaneously)?
        ///// </summary>
        //public long GetSolutionPartTwo()
        //{
        //    long total = 0;
        //    var dictPlots = Data08.DesertPlots.ToDictionary(p => p.Key);
        //    var currentPlots = Data08.DesertPlots.Where(d => d.IsStartPlot).ToList();
        //    var instructionChars = Data08.Instructions.ToCharArray();
        //    var allEndPlots = false;
        //    long lastMilestone = 0;
        //    var sw = Stopwatch.StartNew();
        //    do
        //    {
        //        foreach (var c in instructionChars)
        //        {
        //            total++;
        //            for (var i = 0; i < currentPlots.Count; i++)
        //            {
        //                var plot = currentPlots[i];
        //                currentPlots[i] = c == 'L' ? dictPlots[plot.Left] : dictPlots[plot.Right];
        //            }
        //            if (currentPlots.All(p => p.IsEndPlot))
        //            {
        //                allEndPlots = true;
        //                break;
        //            }
        //        }

        //        if (sw.ElapsedMilliseconds > lastMilestone + 5000)
        //        {
        //            lastMilestone = sw.ElapsedMilliseconds;
        //            Console.Write($"\rTotal > {Math.Floor(total / 10000000d) * 10000000:N0}...");
        //        }

        //    } while (!allEndPlots);
        //    Console.WriteLine($"Ran {sw.Elapsed.TotalMinutes} minutes.");
        //    return total;
        //}
    }
}

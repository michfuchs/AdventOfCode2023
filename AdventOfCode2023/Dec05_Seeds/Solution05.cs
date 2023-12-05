using AdventOfCode2023.Helpers;
using System.ComponentModel.DataAnnotations;

namespace AdventOfCode2023.Dec05
{
    public class Solution05 : ISolution
    {
        public DateTime Date => new DateTime(2023, 12, 05);

        private readonly List<MapLine> _seedToSoil = CreateMap(Data05.SeedToSoil);
        private readonly List<MapLine> _soilToFertilizer = CreateMap(Data05.SoilToFertilizer);
        private readonly List<MapLine> _fertilizerToWater = CreateMap(Data05.FertilizerToWater);
        private readonly List<MapLine> _waterToLight = CreateMap(Data05.WaterToLight);
        private readonly List<MapLine> _lightToTemperature = CreateMap(Data05.LightToTemperature);
        private readonly List<MapLine> _temperatureToHumidity = CreateMap(Data05.TemperatureToHumidity);
        private readonly List<MapLine> _humidityToLocation = CreateMap(Data05.HumidityToLocation);

        /// <summary>
        /// Get all locations for the 20 seeds, and return the lowest location number
        /// </summary>
        public long GetSolutionPartOne()
        {
            var lowestLocation = long.MaxValue;
            var seeds = Data05.Seeds[0].GetNumberMatches().LongValues();

            foreach (var seed in seeds)
            {
                var soil = FindDestinationInMap(_seedToSoil, seed);
                var fertilizer = FindDestinationInMap(_soilToFertilizer, soil);
                var water = FindDestinationInMap(_fertilizerToWater, fertilizer);
                var light = FindDestinationInMap(_waterToLight, water);
                var temperature = FindDestinationInMap(_lightToTemperature, light);
                var humidity = FindDestinationInMap(_temperatureToHumidity, temperature);
                var location = FindDestinationInMap(_humidityToLocation, humidity);
                if (lowestLocation > location)
                    lowestLocation = location;
            }
            return lowestLocation;
        }

        /// <summary>
        /// Get the first location which is mapped to a seed. 
        /// </summary>
        /// <remarks>
        /// Because there are so many seeds (1'975'502'102), it is now more economical to test each location, rather than each seed.
        /// From the solution in part one and after a short test (that the "winning" seed from part one is still a seed and not a range),
        /// we know that the maximum location we have to go to is 525'792'406, and chances are good the actual location number is lower.
        /// </remarks>
        public long GetSolutionPartTwo()
        {
            var lowestLocation = long.MaxValue;
            var seedsAndRanges = Data05.Seeds[0].GetNumberMatches().LongValues();
            var seedRanges = new List<MapLine>();
            // process seeds/ranges into MapLine for easier access
            for (var i = 0; i < 10; i++)
            {
                seedRanges.Add(new MapLine(seedsAndRanges[i * 2], seedsAndRanges[i * 2], seedsAndRanges[i * 2 + 1]));
            }
            // iterate over all locations
            for (var i = 78000000; i < int.MaxValue; i++)
            {
                if (i % 100000 == 0)
                    Console.Write($"\r{null, -12}Solution Part Two: Testing near location {i}..."); // show progress

                var humidity = FindSourceInMap(_humidityToLocation, i);
                var temperature = FindSourceInMap(_temperatureToHumidity, humidity);
                var light = FindSourceInMap(_lightToTemperature, temperature);
                var water = FindSourceInMap(_waterToLight, light);
                var fertilizer = FindSourceInMap(_fertilizerToWater, water);
                var soil = FindSourceInMap(_soilToFertilizer, fertilizer);
                var seed = FindSourceInMap(_seedToSoil, soil);
                if (seedRanges.Any(sr => sr.SourceStart <= seed && seed <= sr.SourceEnd))
                {
                    lowestLocation = i;
                    Console.Write("\r");
                    break;
                }
            }
            return lowestLocation;
        }

        /// <summary>
        /// Map is sufficiently specified by Source, Destination and Range.
        /// Don't try to create an actual full map (e.g. dictionary source->destination), because that will crash the memory.
        /// </summary>
        private static List<MapLine> CreateMap(List<string> map)
        {
            var list = new List<MapLine>();
            foreach (var line in map)
            {
                var numbers = line.GetNumberMatches().LongValues();
                list.Add(new MapLine(numbers[1], numbers[0], numbers[2]));
            }
            list.OrderBy(l => l.DestinationStart);
            return list;
        }

        private static long FindSourceInMap(List<MapLine> map, long destination)
        {
            foreach (var line in map)
            {
                if (line.DestinationStart <= destination && destination <= line.DestinationEnd)
                    return line.SourceStart + (destination - line.DestinationStart);
            }
            return destination; // if not mapped, has the same value as destination
        }

        private static long FindDestinationInMap(List<MapLine> map, long source)
        {
            foreach (var line in map)
            {
                if (line.SourceStart <= source && source <= line.SourceEnd)
                    return line.DestinationStart + (source - line.SourceStart);
            }
            return source; // if not mapped, has the same value as source
        }


    }
}

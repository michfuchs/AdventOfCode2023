// See https://aka.ms/new-console-template for more information
using AdventOfCode2023;

// get all puzzle classes
var puzzleTypes = typeof(Program).Assembly.GetTypes().Where(t => typeof(IPuzzle).IsAssignableFrom(t) && t != typeof(IPuzzle));
var puzzles = puzzleTypes.Select(p => Activator.CreateInstance(p) as IPuzzle).OrderBy(p => p.Date);

// iterate over puzzles classes and print results
foreach (var puzzle in puzzles)
{
    Console.WriteLine($"{puzzle.Date,-12:d}Solution Part One: {puzzle.GetSolutionPartOne()}");
    Console.WriteLine($"{null,-12}Solution Part Two: {puzzle.GetSolutionPartTwo()}");
}
Console.ReadLine();

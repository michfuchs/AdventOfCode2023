// See https://aka.ms/new-console-template for more information
using AdventOfCode2023;

// get all puzzle classes
var puzzleTypes = typeof(Program).Assembly.GetTypes().Where(t => typeof(ISolution).IsAssignableFrom(t) && t != typeof(ISolution));
var puzzles = puzzleTypes.Select(p => Activator.CreateInstance(p) as ISolution).OrderBy(p => p.Date);

// print result of most recent puzzle
var puzzle = puzzles.Last();
Console.WriteLine($"{puzzle.Date,-12:d}Solution Part One: {puzzle.GetSolutionPartOne()}                                    ");
Console.WriteLine($"{null,-12}Solution Part Two: {puzzle.GetSolutionPartTwo()}                                             ");
Console.WriteLine();
Console.WriteLine("Done. Please press any key to close the window.");
Console.ReadKey();

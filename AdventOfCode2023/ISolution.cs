using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023
{
    internal interface ISolution
    {
        DateTime Date { get; }
        long GetSolutionPartOne();
        long GetSolutionPartTwo();
    }
}

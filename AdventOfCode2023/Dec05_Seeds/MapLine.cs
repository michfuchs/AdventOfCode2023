using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Dec05
{
    public class MapLine
    {
        public long SourceStart { get; }
        public long SourceEnd { get; }
        public long DestinationStart { get; }
        public long DestinationEnd { get; }
        public long Range { get; }

        public MapLine(long source, long destination, long range)
        {
            SourceStart = source;
            DestinationStart = destination;
            Range = range;

            SourceEnd = SourceStart + range - 1;
            DestinationEnd = DestinationStart + range - 1;

        }
    }
}

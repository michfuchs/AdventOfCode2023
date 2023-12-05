using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023.Helpers
{
    public static class RegexExtension
    {
        public static List<int> Indexes(this IEnumerable<Match> matches)
        {
            return matches.Select(m => m.Index).ToList();
        }

        public static List<string> Values(this IEnumerable<Match> matches)
        {
            return matches.Select(m => m.Value).ToList();
        }

        public static List<int> IntValues(this IEnumerable<Match> matches)
        {
            return matches.Select(m => m.IntValue()).RemoveNulls();
        }

        public static List<long> LongValues(this IEnumerable<Match> matches)
        {
            return matches.Select(m => m.LongValue()).RemoveNulls();
        }

        public static int? IntValue(this Match match)
        {
            return int.TryParse(match.Value, out int intValue) ? intValue : null;
        }

        public static long? LongValue(this Match match)
        {
            return long.TryParse(match.Value, out long longValue) ? longValue : null;

        }
    }
}

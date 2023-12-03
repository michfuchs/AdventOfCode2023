using System.Text.RegularExpressions;

namespace AdventOfCode2023.Helpers
{
    public static class StringExtension
    {
        #region Search String/Pattern

        public static MatchCollection GetMatches(this string s, string search)
        {
            return s.GetMatchesOfRegex(Regex.Escape(search));
        }

        public static MatchCollection GetMatchesOfRegex(this string s, string pattern)
        {
            return Regex.Matches(s, pattern);
        }

        #endregion Search String/Pattern

        #region Numbers

        public static int? NumbersOnly(this string s)
        {
            var numbersOnly = Regex.Replace(s, "[^0-9]", "");
            if (!string.IsNullOrWhiteSpace(numbersOnly))
                return int.Parse(numbersOnly);
            return null;
        }
        
        public static bool IsNumber(this char c)
        {
            return char.IsNumber(c);
        }

        public static MatchCollection GetNumberMatches(this string s)
        {
            return s.GetMatchesOfRegex(@"\d+");
        }

        /// <summary>
        /// Check the text at index for a number
        /// </summary>
        /// <param name="s">Text potentially containing the number</param>
        /// <param name="index">Index at which the number can appear</param>
        /// <returns>If found the number, otherwise null</returns>
        public static int? GetNumberAtIndex(this string s, int index)
        {
            if (s == null || s.Length < index + 1 || index < 0) return null; // invalid arguments
            if (!s[index].IsNumber()) return null; // no number at index

            // get all numbers within the string, take the first one which matches the index
            var stringNumber = s.GetNumberMatches().FirstOrDefault(m => m.Index <= index && index <= m.Index + m.Length);
            return stringNumber?.IntValue();
        }

        /// <summary>
        /// Looks at indexes of string for numbers. If found, returns all contiguous numbers.
        /// May include digits outside the [startIndex, endIndex] range - the number just needs to overlap with the analyzed region.
        /// </summary>
        /// <example>"..123.456.." with startIndex=3, endIndex=6 
        /// --> finds 2 numbers ("..1[23.4]56..), and will return 123 and 456.
        /// </example>
        /// <param name="s">Text to search for the numbers</param>
        /// <param name="startIndex">Start of analyzed region within text</param>
        /// <param name="endIndex">End of analyzed region within text</param>
        /// <returns>A list of numbers</returns>
        public static List<int> GetNumbersAtIndexes(this string s, int startIndex, int endIndex)
        {   
            if (s == null) return new List<int>();
            var numberMatches = s.GetNumberMatches().Where(m => m.Index + m.Length > startIndex && m.Index <= endIndex);
            return numberMatches.IntValues();
        }

        #endregion Numbers

        #region Symbols

        public static bool IsSymbol(this char c)
        {
            // it's the relevant selection for Puzzle03. char.IsSymbol is not complete either, unfortunately (not matching */- etc.)
            return Regex.IsMatch(c.ToString(), @"[*\-+=/@#&%$]");
        }

        public static bool ContainsSymbol(this string s)
        {
            return s.Any(c => c.IsSymbol());
        }

        public static char? GetSymbolAtIndex(this string s, int index)
        {
            if (s == null || s.Length < index + 1 || index < 0) return null;
            if (!s[index].IsSymbol()) return null;

            return s[index];
        }

        #endregion Symbols

        #region Substring

        public static string? SubstringByStartEnd(this string s, int startIndex, int endIndex)
        {
            if (s == null) return null;
            var startInBounds = Math.Max(0, startIndex);
            var endInBounds = Math.Min(s.Length - 1, endIndex);
            return s.Substring(startInBounds, endInBounds - startInBounds);
        }

        #endregion Substring
    }
}

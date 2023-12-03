using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2023.Helpers
{
    public static class StringExtension
    {
        #region Parse

        public static int? NumbersOnly(this string s)
        {
            var numbersOnly = Regex.Replace(s, "[^0-9]", "");
            if (!string.IsNullOrWhiteSpace(numbersOnly))
                return int.Parse(numbersOnly);
            return null;
        }

        #endregion Parse


    }
}

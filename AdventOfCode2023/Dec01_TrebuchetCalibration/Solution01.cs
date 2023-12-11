namespace AdventOfCode2023.Dec01
{
    public class Solution08 : ISolution
    {
        public DateTime Date => new DateTime(2023, 12, 01);

        /// <summary>
        /// Get the first and last occurrence of a digit in a line (see TreebuchetCalibrationData). 
        /// Parse the lines for digits only. 
        /// </summary>
        public long GetSolutionPartOne()
        {
            var total = 0;
            foreach (var s in Data08.Lines)
            {
                var chars = s.ToCharArray();
                int? firstNumber = null;
                int? lastNumber = null;
                foreach (var c in chars)
                {
                    if (int.TryParse(c.ToString(), out int number))
                    {
                        if (firstNumber is null) firstNumber = number;
                        lastNumber = number;
                    }
                }
                var finalNumber = int.Parse($"{firstNumber}{lastNumber}");
                total += finalNumber;
            }
            return total;
        }

        /// <summary>
        /// Get the first and last occurrence of a digit in a line (see TreebuchetCalibrationData). 
        /// Parse the lines for digits and written numbers. 
        /// </summary>
        public long GetSolutionPartTwo()
        {
            var total = 0;

            foreach (var line in Data08.Lines)
            {
                var firstDigitAtIndex = new Dictionary<int, int>();
                var lastDigitAtIndex = new Dictionary<int, int>();

                foreach (var d in Data08.Digits)
                {
                    // parse for the "written" digits and store found indexes in a dictionary
                    var firstIndexWritten = line.IndexOf(d.Key);
                    var lastIndexWritten = line.LastIndexOf(d.Key);
                    if (firstIndexWritten >= 0) firstDigitAtIndex.Add(d.Value, firstIndexWritten);
                    if (lastIndexWritten >= 0) lastDigitAtIndex.Add(d.Value, lastIndexWritten);

                    // parse for the "number" digits, and adjust dictionary where necessary
                    var firstIndexNumber = line.IndexOf(d.Value.ToString());
                    var lastIndexNumber = line.LastIndexOf(d.Value.ToString());
                    if (firstIndexNumber >= 0) 
                    {   // update dictionary if digit has not been found yet, or digit has been found later in line
                        if (!firstDigitAtIndex.ContainsKey(d.Value) || firstDigitAtIndex.ContainsKey(d.Value) && firstDigitAtIndex[d.Value] > firstIndexNumber)
                            firstDigitAtIndex[d.Value] = firstIndexNumber;
                    }
                    if (lastIndexNumber >= 0)
                    {   // update dictionary if digit has not been found yet, or digit has been found earlier in line
                        if (!lastDigitAtIndex.ContainsKey(d.Value) || lastDigitAtIndex.ContainsKey(d.Value) && lastDigitAtIndex[d.Value] < lastIndexNumber)
                            lastDigitAtIndex[d.Value] = lastIndexNumber;
                    }
                }

                // get first digit and last digit of found results
                var finalNumber = int.Parse($"{firstDigitAtIndex.MinBy(k => k.Value).Key}{lastDigitAtIndex.MaxBy(k => k.Value).Key}");
                total += finalNumber;
            }
            return total;
        }
    }
}

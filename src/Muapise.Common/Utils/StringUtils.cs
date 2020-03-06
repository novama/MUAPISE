using System.Text.RegularExpressions;

namespace Muapise.Common.Utils
{
    public static class StringUtils
    {
        /// <summary>
        ///     Function to test if the given string corresponds to a natural number.
        ///     N = {1,2,3,4,5,6,7,8,9,10...}
        ///     Not includes 0 (zero)
        /// </summary>
        /// <param name="value">String to check</param>
        /// <returns>True if given string is a natural number</returns>
        public static bool IsNaturalNumber(this string value)
        {
            if (string.IsNullOrEmpty(value)) { return false; }

            var objNotNaturalPattern = new Regex("[^0-9]");
            var objNaturalPattern = new Regex("0*[1-9][0-9]*");
            return !objNotNaturalPattern.IsMatch(value) && objNaturalPattern.IsMatch(value);
        }
    }
}
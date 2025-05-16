using System.Text.RegularExpressions;

namespace MemeAlertsScript.WinForms.Extensions
{
    public static class StringExtensions
    {
        public static int? ParseTrailingBracketNumber(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            var match = Regex.Match(input, @"\[(\d+)\]$");

            return match.Success && int.TryParse(match.Groups[1].Value, out int value) ? value : null;
        }
    }
}

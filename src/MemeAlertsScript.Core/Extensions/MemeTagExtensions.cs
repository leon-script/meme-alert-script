using System.Text.RegularExpressions;

namespace MemeAlertsScript.Core.Extensions
{
    public static class MemeTagExtensions
    {
        public static int? ParseMemeTagNumber(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return null;
            }

            var match = Regex.Match(input, @"\[meme:(\d+)\]", RegexOptions.IgnoreCase);
            return match.Success && int.TryParse(match.Groups[1].Value, out int value) ? value : null;
        }

        public static bool HasMemeTag(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            return Regex.IsMatch(input, @"\[meme:\d+\]", RegexOptions.IgnoreCase);
        }

        public static string ToMemeTag(this int value)
        {
            return $"[meme:{value}]";
        }
    }
}

using System.Text.RegularExpressions;

namespace TwitchLeonScript.Core.Common.Extensions
{
    public static class MemeTagExtensions
    {
        public static int ParseMemeTagNumber(this string input)
        {
            var match = Regex.Match(input, @"\[tls1-meme:(\d+)\]", RegexOptions.IgnoreCase);
            return int.Parse(match.Groups[1].Value);
        }

        public static bool HasMemeTag(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            return Regex.IsMatch(input, @"\[tls1-meme:\d+\]", RegexOptions.IgnoreCase);
        }

        public static string ToMemeTag(this int value)
        {
            return $"[meme:{value}]";
        }
    }
}

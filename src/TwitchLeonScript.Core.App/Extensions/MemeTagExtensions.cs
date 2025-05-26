using System.Text.RegularExpressions;

namespace TwitchLeonScript.Core.App.Extensions
{
    public static partial class MemeTagExtensions
    {
        private const string _pattern = @"\[tls1-meme:(\d+)\]";
        private const string _simplePattern = @"\[tls1-meme:\d+\]";

        [GeneratedRegex(_pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        private static partial Regex MemeTagRegex();

        [GeneratedRegex(_simplePattern, RegexOptions.IgnoreCase | RegexOptions.Compiled)]
        private static partial Regex MemeTagSimpleRegex();

        public static int ParseMemeTagNumber(this string input)
        {
            var match = MemeTagRegex().Match(input);
            return int.Parse(match.Groups[1].Value);
        }

        public static bool HasMemeTag(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return false;
            }

            return MemeTagSimpleRegex().IsMatch(input);
        }

        public static string ToMemeTag(this int value)
        {
            return $"[meme:{value}]";
        }
    }
}

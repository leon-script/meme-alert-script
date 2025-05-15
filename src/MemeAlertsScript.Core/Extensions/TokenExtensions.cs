namespace MemeAlertsScript.Core.Extensions
{
    public static class TokenExtensions
    {
        public static string ToSecretPreview(this string? value, int visible = 6)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }

            if (value.Length <= visible * 2)
            {
                return value;
            }

            return $"{value[..visible]}...{value[^visible..]}";
        }
    }
}

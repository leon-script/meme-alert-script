using Microsoft.Extensions.Logging;

namespace MemeAlertsScript.WinForms.Logging
{
    public static class LoggingExtensions
    {
        public static LogLevel ToLogLevelOrDefault(this string? value, LogLevel defaultLevel = LogLevel.Information)
        {
            return Enum.TryParse<LogLevel>(value, ignoreCase: true, out var result)
                ? result
                : defaultLevel;
        }
    }
}

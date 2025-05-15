using Microsoft.Extensions.Logging;
using System.Text;

namespace MemeAlertsScript.WinForms.Logging
{
    public class RichTextBoxLogger : ILogger
    {
        private readonly RichTextBox _target;
        private readonly string _categoryName;
        private readonly object _lock = new();

        public RichTextBoxLogger(RichTextBox target, string categoryName)
        {
            _target = target;
            _categoryName = categoryName;
        }

        public IDisposable? BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            var message = formatter(state, exception);

            var fullLog = new StringBuilder();
            fullLog.Append($"[{timestamp}] [{logLevel}]");

            if (!string.IsNullOrEmpty(_categoryName))
            {
                fullLog.Append($" [{_categoryName}]");
            }

            fullLog.Append($": {message}");

            if (exception != null)
            {
                fullLog.Append($"\n{exception}");
            }

            var final = fullLog.ToString();

            lock (_lock)
            {
                if (_target.InvokeRequired)
                {
                    _target.Invoke(() =>
                    {
                        InsertLog(final, GetColor(logLevel));
                    });
                }
                else
                {
                    InsertLog(final, GetColor(logLevel));
                }
            }
        }

        private void InsertLog(string log, Color color)
        {
            _target.SuspendLayout();

            int currentSelectionStart = _target.SelectionStart;

            _target.SelectionStart = 0;
            _target.SelectionLength = 0;
            _target.SelectionColor = color;
            _target.SelectedText = log + Environment.NewLine;

            _target.SelectionStart = currentSelectionStart;
            _target.SelectionColor = _target.ForeColor;

            _target.ResumeLayout();
        }

        private Color GetColor(LogLevel level)
        {
            return level switch
            {
                LogLevel.Trace => Color.Gray,
                LogLevel.Debug => Color.Gray,
                LogLevel.Information => _target.ForeColor,
                LogLevel.Warning => Color.Orange,
                LogLevel.Error => Color.Red,
                LogLevel.Critical => Color.DarkRed,
                _ => _target.ForeColor
            };
        }
    }

    public class RichTextBoxLoggerProvider : ILoggerProvider
    {
        private readonly RichTextBox _target;

        public RichTextBoxLoggerProvider(RichTextBox target)
        {
            _target = target;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new RichTextBoxLogger(_target, categoryName);
        }

        public void Dispose() { }
    }
}

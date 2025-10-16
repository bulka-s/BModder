using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Documents;
using System.Windows.Input;
using System.Reflection.Metadata;

namespace BModder.UI
{
    public static class LogHelper
    {
        private static RichTextBox? _logBox;

        public static void Init(RichTextBox box)
        {
            _logBox = box;
        }

        public static void WriteLog(string message, LogType type = LogType.Info)
        {
            SolidColorBrush color = type switch
            {
                LogType.Success => new SolidColorBrush(Color.FromRgb(0x00, 0xC8, 0x53)), // green
                LogType.Error => new SolidColorBrush(Color.FromRgb(0xFF, 0x3B, 0x30)), // red
                LogType.Warn => new SolidColorBrush(Color.FromRgb(0xFF, 0xB0, 0x00)), // yellow
                _ => new SolidColorBrush(Color.FromRgb(0xB0, 0xB0, 0xB0)), // gray
            };

            _logBox?.Dispatcher.Invoke(() =>
            {
                Paragraph paragraph = new Paragraph(new Run(getPrefix(type) + message))
                {
                    Foreground = color,
                    Margin = new System.Windows.Thickness(0)
                };

                _logBox.Document.Blocks.Add(paragraph);
                _logBox.ScrollToEnd(); // auto scroll
            });
        }

        private static string getPrefix(LogType logType)
        {
            return logType switch
            {
                LogType.Success => "[SUCCESS] ",
                LogType.Error   => "[ERR]     ",
                LogType.Warn    => "[WARN]    ",
                _               => "[INFO]    "
            };
        }

        public static void ClearLogs()
        {
            _logBox?.Document.Blocks.Clear();
        }

        public enum LogType
        {
            Success,
            Error,
            Warn,
            Info
        }
    }
}

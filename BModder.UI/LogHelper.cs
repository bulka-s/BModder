using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Documents;

namespace BModder.UI
{
    public static class LogHelper
    {

        public static void WriteLog(RichTextBox box, string message, LogType type = LogType.Info)
        {
            SolidColorBrush color = type switch
            {
                LogType.Success => new SolidColorBrush(Color.FromRgb(0x00, 0xC8, 0x53)), // green
                LogType.Error => new SolidColorBrush(Color.FromRgb(0xFF, 0x3B, 0x30)), // red
                LogType.Warn => new SolidColorBrush(Color.FromRgb(0xFF, 0xB0, 0x00)), // yellow
                _ => new SolidColorBrush(Color.FromRgb(0xB0, 0xB0, 0xB0)), // gray
            };

            box.Dispatcher.Invoke(() =>
            {
                Paragraph paragraph = new Paragraph(new Run(message))
                {
                    Foreground = color,
                    Margin = new System.Windows.Thickness(0)
                };

                box.Document.Blocks.Add(paragraph);
                box.ScrollToEnd(); // auto scroll
            });
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

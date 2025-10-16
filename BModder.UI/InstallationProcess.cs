using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.IO;
using System.Windows.Shapes;

namespace BModder.UI
{
    public class InstallationProcess
    {
        public string ConfigPath { get; set; } = "";
        public string GamePath { get; set; } = "";
        public RichTextBox LogBox { get; set; }

        public InstallationProcess(RichTextBox _logBox)
        {
            LogBox = _logBox;
        }

        public void Start()
        {
            ClearLogs();
            LogHelper.WriteLog(LogBox, "Starting installation...", LogHelper.LogType.Info);

            if (!CheckAllPaths())
            {
                LogHelper.WriteLog(LogBox, "Installation aborted due to invalid paths.", LogHelper.LogType.Error);
                return;
            }

            // TODO: добавить загрузку и установку модов
            LogHelper.WriteLog(LogBox, "All paths validated. Ready to install mods.", LogHelper.LogType.Success);
        }

        private bool CheckAllPaths()
        {
            if (!CheckPath(ConfigPath, "config.json"))  
            {
                return false;
            }
            LogHelper.WriteLog(LogBox, $"Config found sucessfuly.", LogHelper.LogType.Success);

            if (!CheckPath(GamePath, "Lethal Company.exe")) return false;
            LogHelper.WriteLog(LogBox, $"Game found sucessfuly.", LogHelper.LogType.Success);

            return true;
        }

        private bool CheckPath(string path, string? targetFile = null)
        {
            path = path.Trim();

            if (string.IsNullOrWhiteSpace(path))
            {
                LogHelper.WriteLog(LogBox, $"Invalid path. (empty)", LogHelper.LogType.Error);
                return false;
            }

            if (!Directory.Exists(path))
            {
                LogHelper.WriteLog(LogBox, $"Path not found. {path}", LogHelper.LogType.Error);
                return false;
            }

            if (targetFile != null)
            {
                string fullFile = System.IO.Path.Combine(path, targetFile);
                if (!File.Exists(fullFile))
                {
                    LogHelper.WriteLog(LogBox, $"File not found. {fullFile}", LogHelper.LogType.Error);
                    return false;
                }
            }

            return true;
        }

        private void ClearLogs()
        {
            LogBox.Document.Blocks.Clear();
        }
    }
}

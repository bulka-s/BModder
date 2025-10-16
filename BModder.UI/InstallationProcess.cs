using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.IO;
using System.Windows.Shapes;
using System.Diagnostics;

namespace BModder.UI
{
    public class InstallationProcess
    {
        public string ConfigPath { get; set; } = "";
        public string GamePath { get; set; } = "";
        public bool IsCleanInstall { get; set; } = false;

        private Config? Config = null;

        private readonly static string confName = "config.json";

        public void Start()
        {
            LogHelper.ClearLogs();

            if (!InitConfig()) return;

            LogHelper.WriteLog("Starting installation...", LogHelper.LogType.Info);

            if (!CheckAllPaths())
            {
                LogHelper.WriteLog("Installation aborted due to invalid paths.", LogHelper.LogType.Error);
                return;
            }
            LogHelper.WriteLog("All paths validated. Ready to install mods.\n", LogHelper.LogType.Success);
        }
        private bool InitConfig()
        {
            string fullPath = System.IO.Path.Combine(ConfigPath, confName);

            if (!FileManager.CheckPath(ConfigPath, confName))
            {
                LogHelper.WriteLog($"Config not found. {fullPath}", LogHelper.LogType.Error);
                return false;
            }
            else
            {
                LogHelper.WriteLog($"Config found sucessfuly. {fullPath}", LogHelper.LogType.Success);
            }

            try
            {
                Config = ConfigManager.LoadConfigs(fullPath);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog($"Error loading config: {ex.Message}", LogHelper.LogType.Error);
                return false;
            }

            if (Config == null)
            {
                LogHelper.WriteLog("Couldn't load the config.", LogHelper.LogType.Error);
                return false;
            }
            if (!Config!.CheckConfig())
            {
                LogHelper.WriteLog("Incorrect config file.", LogHelper.LogType.Error);
                return false;
            }

            LogHelper.WriteLog("Config load sucessfuly!", LogHelper.LogType.Success);
            return true;
        }

        private bool CheckAllPaths()
        {
            if (!FileManager.CheckPath(GamePath, Config!.MainFile)) return false;
            LogHelper.WriteLog($"Game found sucessfuly.", LogHelper.LogType.Success);

            return true;
        }
    }
}

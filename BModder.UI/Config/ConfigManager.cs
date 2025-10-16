using System;
using System.IO;
using System.Text.Json;

namespace BModder.UI
{
    static class ConfigManager
    {
        public static Config? LoadConfigs(string jsonPath)
        {
            LogHelper.WriteLog($"Load config file...", LogHelper.LogType.Info);

            if (!File.Exists(jsonPath))
            {
                LogHelper.WriteLog($"Config file not found: {jsonPath}", LogHelper.LogType.Error);
                return null;
            }

            string json = File.ReadAllText(jsonPath);

            if (string.IsNullOrWhiteSpace(json))
            {
                LogHelper.WriteLog("Config file is empty.", LogHelper.LogType.Error);
                return null;
            }

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReadCommentHandling = JsonCommentHandling.Skip
            };

            Config? config;
            try
            {
                config = JsonSerializer.Deserialize<Config>(json, options);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog($"Failed to parse config file: {ex.Message}", LogHelper.LogType.Error);
                return null;
            }

            if (config == null)
            {
                LogHelper.WriteLog("Config deserialization returned null.", LogHelper.LogType.Error);
                return null;
            }

            if (!config.CheckConfig())
            {
                LogHelper.WriteLog("Invalid config structure or missing fields.", LogHelper.LogType.Error);
                return null;
            }

            return config;
        }
    }
}

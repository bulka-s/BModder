using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BModder.UI
{
    public class Config
    {
        [JsonPropertyName("gameName")]
        public string? GameName { get; set; }

        [JsonPropertyName("mainFile")]
        public string? MainFile { get; set; }

        [JsonPropertyName("modsPath")]
        public string? ModsPath { get; set; }

        [JsonPropertyName("mods")]
        public List<Mod>? Mods { get; set; }

        public bool CheckConfig()
        {
            if (string.IsNullOrWhiteSpace(GameName))
            {
                LogHelper.WriteLog("Game name is missing in config.", LogHelper.LogType.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(MainFile))
            {
                LogHelper.WriteLog("Main file (executable) is missing in config.", LogHelper.LogType.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(ModsPath))
            {
                LogHelper.WriteLog("Mods path is missing in config.", LogHelper.LogType.Warn);
                // можно не возвращать false, если это не критично
            }

            if (Mods == null || Mods.Count == 0)
            {
                LogHelper.WriteLog("No mods specified in config.", LogHelper.LogType.Warn);
            }

            return true;
        }
    }

    
}

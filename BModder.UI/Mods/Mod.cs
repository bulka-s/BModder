using System.Text.Json.Serialization;
using System.IO;


namespace BModder.UI
{
    public class Mod
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("modPath")]
        public string? ModPath { get; set; }

        [JsonPropertyName("downloadUrl")]
        public string? DownloadUrl { get; set; }

        public bool IsInstalled(string gamePath)
        {
            if (string.IsNullOrWhiteSpace(ModPath))
            {
                ColorConsole.WriteLineError($"The path (folder) is not specified in the \"{{Name}}\" mod.");
                return false;
            }

            string fullPath = Path.Combine(gamePath, ModPath);
            return File.Exists(fullPath);
        }

        public void PrintStatus(string gamePath)
        {
            if (IsInstalled(gamePath))
            {
                ColorConsole.WriteLineSuccess($"{Name}");
            } else
            {
                ColorConsole.WriteLineError($"{Name}");
            }
        }
       
    }
}

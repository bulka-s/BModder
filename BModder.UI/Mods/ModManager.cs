using System.Text.Json;
using System.IO;

namespace BModder.UI
{
    public static class ModManager
    {
        public static List<Mod> LoadMods(string jsonPath)
        {
            if (!File.Exists(jsonPath))
            {
                ColorConsole.WriteLineError($"File not fond: {jsonPath}");
                return new List<Mod>();
            }

            string json = File.ReadAllText(jsonPath);
            return JsonSerializer.Deserialize<List<Mod>>(json) ?? new List<Mod>();
        }

        public static void CheckMods(string gamePath, List<Mod> mods)
        {
            ColorConsole.WriteLineInfo("Checking mods:\n");

            foreach (var mod in mods)
                mod.PrintStatus(gamePath);
        }
    }
}

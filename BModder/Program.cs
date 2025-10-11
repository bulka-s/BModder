using BModder;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        Game? game = null;
        bool isOnline = true;

        isOnline = UserInput.AskYesNo("");

        while (true)
        {
            string? path = FileManager.AskGamePath();

            if (path == null)
            {
                ColorConsole.WriteLineInfo("Program ending...");
                return;
            }

            game = new Game(path);

            if (game.Validate("Lethal Company.exe"))
                break;
            
        }
        if (UserInput.AskYesNo("Perform a clean installation (remove existing mods)?", "n"))
        {
            
        }

        if (UserInput.AskYesNo("Install missing mods?", "y"))
        {
            string modsFile = "mods.json";
            var mods = ModManager.LoadMods(modsFile);

            ModManager.CheckMods(game.Path, mods);
        }
    }
}

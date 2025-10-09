using BModder;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        Game? game = null;

        while (true)
        {
            string path = FileManager.AskGamePath();

            if (path == null)
            {
                ColorConsole.WriteLineInfo("Proramm ending...");
                return;
            }

            game = new Game(path);

            if (game.Validate("Lethal Company.exe"))
                break;
            

            ColorConsole.WriteLineError("Game not found. Try again.");
        }

        string modsFile = "mods.json";
        var mods = ModManager.LoadMods(modsFile);

        ModManager.CheckMods(game.Path, mods);
    }
}

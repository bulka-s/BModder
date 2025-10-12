using BModder;
using System;
using System.IO;

class Program
{
    static async Task Main()
    {
        Game? game = null;

        string modsFile = "mods.json";
        //потом можно сделать проверку, и запросить ручной ввод, обработку ошибок и тд

        string[] menuItems = { "Online install", "Offline install" };
        int isOnline = UserInput.AskMenu(menuItems, "Choose installation method:");

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

        
        var mods = ModManager.LoadMods(modsFile);
        Installer installer = new Installer(game, mods, isOnline == 1);

        await installer.RunAsync(
            UserInput.AskYesNo("Perform a clean installation (remove existing mods)?", "n")
        );

        Console.ReadKey();
        
    }
}

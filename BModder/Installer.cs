using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BModder
{
    public class Installer
    {
        private readonly Game _game;
        private readonly List<Mod> _mods;
        private readonly bool _isOnline;

        public Installer(Game game, List<Mod> mods, bool isOnline)
        {
            _game = game;
            _mods = mods;
            _isOnline = isOnline;
        }

        public void Run(bool cleanInstall)
        {
            if (cleanInstall)
            {
                CleanMods();
            }
            else
            {
                ModManager.CheckMods(_game.Path, _mods);
            }

            if (UserInput.AskYesNo("Install missing mods?", "y"))
                InstallMissingMods();
        }

        public void CleanMods()
        {
            ColorConsole.WriteLineInfo("\nCleaning mods directory...");
            string pluginsDir = Path.Combine(_game.Path, "BepInEx", "plugins");

            if (Directory.Exists(pluginsDir))
            {
                Directory.Delete(pluginsDir, true);
                ColorConsole.WriteLineSuccess("Mods folder cleaned successfully.");
            }
            else
            {
                ColorConsole.WriteLineWarning("Mods folder not found — skipping clean.");
            }
        }

        private void InstallMissingMods()
        {
            ColorConsole.WriteLineInfo("\nInstalling missing mods...");

            foreach (var mod in _mods)
            {
                if (!mod.IsInstalled(_game.Path))
                {
                    if (_isOnline)
                        InstallModOnline(mod);
                    else
                        InstallModOffline(mod);
                }
            }
        }

        private void InstallModOnline(Mod mod)
        {
            ColorConsole.WriteLineInfo($"⬇️  Downloading {mod.Name} from {mod.DownloadUrl}...");
            // тут позже добавим логику скачивания через HttpClient
        }

        private void InstallModOffline(Mod mod)
        {
            ColorConsole.WriteLineInfo($"📦 Installing {mod.Name} from local files...");
            // здесь можно будет реализовать логику распаковки zip-архива из локальной папки
        }
    }
}

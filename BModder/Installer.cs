using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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

        public async Task RunAsync(bool cleanInstall)
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
                await InstallMissingModsAsync(_game.Path, _mods);
        }

        public void CleanMods()
        {
            ColorConsole.WriteLineInfo("\nCleaning mods directory...");
            string pluginsDir = Path.Combine(_game.Path, "BepInEx", "plugins");

            if (Directory.Exists(pluginsDir))
            {
                try
                {
                    Directory.Delete(pluginsDir, true);
                    ColorConsole.WriteLineSuccess("Mods folder cleaned successfully.");
                }
                catch (Exception ex)
                {
                    ColorConsole.WriteLineError($"Error deleting mods folder: {ex.Message}");
                }
            }
            else
            {
                ColorConsole.WriteLineWarning("Mods folder not found — skipping clean.");
            }
        }

        public static async Task InstallMissingModsAsync(string gamePath, List<Mod> mods)
        {
            string downloadsDir = Path.Combine(gamePath, "Downloads");
            Directory.CreateDirectory(downloadsDir);

            foreach (var mod in mods)
            {
                if (!mod.IsInstalled(gamePath))
                {
                    if (string.IsNullOrWhiteSpace(mod.DownloadUrl))
                    {
                        ColorConsole.WriteLineWarning($"⚠️  {mod.Name} has no download URL — skipping.");
                        continue;
                    }

                    ColorConsole.WriteLineInfo($"⬇️  Downloading {mod.Name}...");

                    string downloadPath = Path.Combine(downloadsDir, $"{mod.Name}.zip");
                    await Downloader.DownloadFileAsync(mod.DownloadUrl, downloadPath);

                    try
                    {
                        ColorConsole.WriteLineInfo($"📦  Installing {mod.Name}...");
                        ExtractSmart(downloadPath, gamePath);
                        ColorConsole.WriteLineSuccess($"✅  {mod.Name} installed successfully.");
                    }
                    catch (Exception ex)
                    {
                        ColorConsole.WriteLineError($"❌  Failed to install {mod.Name}: {ex.Message}");
                    }
                    finally
                    {
                        try
                        {
                            if (File.Exists(downloadPath))
                                File.Delete(downloadPath);
                        }
                        catch (Exception ex)
                        {
                            ColorConsole.WriteLineWarning($"⚠️  Could not delete archive {mod.Name}: {ex.Message}");
                        }
                    }
                }
            }
        }


        public static void ExtractSmart(string zipPath, string gamePath)
        {
            string tempDir = Path.Combine(gamePath, "TempExtract");
            string pluginsDir = Path.Combine(gamePath, "BepInEx", "plugins");

            Directory.CreateDirectory(tempDir);
            ZipFile.ExtractToDirectory(zipPath, tempDir, true);

            // 1. Проверяем, есть ли папка BepInEx внутри архива
            string bepInExPath = Path.Combine(tempDir, "BepInEx");
            if (Directory.Exists(bepInExPath))
            {
                CopyAll(bepInExPath, Path.Combine(gamePath, "BepInEx"));
            }
            else
            {
                // 2. Проверяем, есть ли DLL прямо в корне
                var dlls = Directory.GetFiles(tempDir, "*.dll", SearchOption.TopDirectoryOnly);
                if (dlls.Length > 0)
                {
                    CopyAll(tempDir, pluginsDir);
                }
                else
                {
                    Console.WriteLine($"⚠️  Unknown structure in {Path.GetFileName(zipPath)} — skipped.");
                }
            }

            Directory.Delete(tempDir, true);
        }

        private static void CopyAll(string sourceDir, string targetDir)
        {
            Directory.CreateDirectory(targetDir);

            foreach (string file in Directory.GetFiles(sourceDir, "*", SearchOption.AllDirectories))
            {
                string relativePath = Path.GetRelativePath(sourceDir, file);
                string destFile = Path.Combine(targetDir, relativePath);
                Directory.CreateDirectory(Path.GetDirectoryName(destFile)!);
                File.Copy(file, destFile, true);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BModder.UI
{
    public class Installer
    {
        private readonly string _gamePath;
        private readonly Config _config;
        private readonly bool _isOnline;
        public event Action<int, string>? OnProgress;

        public Installer(string gamePath, Config config, bool isOnline)
        {
            _gamePath = gamePath;
            _config = config;
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
                ModManager.CheckMods(_gamePath, _config.Mods!);
            }

            await InstallMissingModsAsync(_gamePath, _config.Mods!);
        }

        public void CleanMods()
        {
            LogHelper.WriteLog("\nCleaning mods directory...");
            string pluginsDir = Path.Combine(_gamePath, _config.ModsPath!);

            if (Directory.Exists(pluginsDir))
            {
                try
                {
                    Directory.Delete(pluginsDir, true);
                    LogHelper.WriteLog("Mods folder cleaned successfully.", LogHelper.LogType.Success);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog($"Error deleting mods folder: {ex.Message}", LogHelper.LogType.Error);
                }
            }
            else
            {
                LogHelper.WriteLog("Mods folder not found — skipping clean.", LogHelper.LogType.Info);
            }
        }

        public async Task InstallMissingModsAsync(string gamePath, List<Mod> mods)
        {
            string downloadsDir = Path.Combine(gamePath, "Downloads");
            Directory.CreateDirectory(downloadsDir);

            int oneStep = 100 / mods.Count;
            int startVal = 0;

            ReportProgress(0, "");
            foreach (var mod in mods)
            {
                if (!mod.IsInstalled(gamePath))
                {
                    if (string.IsNullOrWhiteSpace(mod.DownloadUrl))
                    {
                        LogHelper.WriteLog($"{mod.Name} has no download URL — skipping.", LogHelper.LogType.Warn);
                        ReportProgress(startVal+=oneStep, "");
                        continue;
                    }

                    LogHelper.WriteLog($"Downloading {mod.Name}...");

                    string downloadPath = Path.Combine(downloadsDir, $"{mod.Name}.zip");
                    await Downloader.DownloadFileAsync(mod.DownloadUrl, downloadPath);

                    try
                    {
                        LogHelper.WriteLog($"Installing {mod.Name}...");
                        ExtractSmart(downloadPath, gamePath);
                        LogHelper.WriteLog($"{mod.Name} installed successfully.", LogHelper.LogType.Success);
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog($"Failed to install {mod.Name}: {ex.Message}", LogHelper.LogType.Error);
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
                            LogHelper.WriteLog($"Could not delete archive {mod.Name}: {ex.Message}", LogHelper.LogType.Error);
                        }
                    }
                    ReportProgress(startVal += oneStep, "");
                }
            }
            ReportProgress(100, "");

            LogHelper.WriteLog($"============Installation complete!=============", LogHelper.LogType.Success);


        }

        public void ExtractSmart(string zipPath, string gamePath)
        {
            string tempDir = Path.Combine(gamePath, "TempExtract");
            string pluginsDir = Path.Combine(gamePath, _config.ModsPath!);

            Directory.CreateDirectory(tempDir);
            ZipFile.ExtractToDirectory(zipPath, tempDir, true);

            string bepInExPath = Path.Combine(tempDir, _config.ModsPath!.Split('/')[0]);
            if (Directory.Exists(bepInExPath))
            {
                CopyAll(bepInExPath, Path.Combine(gamePath, _config.ModsPath!.Split('/')[0]));
            }
            else
            {
                var dlls = Directory.GetFiles(tempDir, "*.dll", SearchOption.TopDirectoryOnly);
                if (dlls.Length > 0)
                {
                    CopyAll(tempDir, pluginsDir);
                }
                else
                {
                    LogHelper.WriteLog($"⚠️  Unknown structure in {Path.GetFileName(zipPath)} — skipped.", LogHelper.LogType.Warn);
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

        private void ReportProgress(int percent, string message)
        {
            OnProgress?.Invoke(percent, message);
        }
    }
}

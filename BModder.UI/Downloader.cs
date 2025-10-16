using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;

namespace BModder.UI
{
    public static class Downloader
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task DownloadFileAsync(string url, string outputPath)
        {
            try
            {
                Console.WriteLine($"Downloading: {url}");

                Directory.CreateDirectory(Path.GetDirectoryName(outputPath)!);

                using (var response = await client.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();

                    await using var fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None);

                    await response.Content.CopyToAsync(fs);
                }

                Console.WriteLine($"Downloaded to: {outputPath}");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog($"Error downloading {url}: {ex.Message}", LogHelper.LogType.Error);
            }
        }
    }
}

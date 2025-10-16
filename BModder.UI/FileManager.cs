using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BModder.UI
{
    public static class FileManager
    {
        public static bool CheckPath(string path, string targetFile)
        {
            path = path.Trim();

            if (string.IsNullOrWhiteSpace(path))
            {
                LogHelper.WriteLog($"Invalid path. (empty)", LogHelper.LogType.Error);
                return false;
            }

            if (!Directory.Exists(path))
            {
                LogHelper.WriteLog($"Path not found. {path}", LogHelper.LogType.Error);
                return false;
            }

            if (targetFile != null)
            {
                string fullFile = System.IO.Path.Combine(path, targetFile);
                if (!File.Exists(fullFile))
                {
                    LogHelper.WriteLog($"File not found. {fullFile}", LogHelper.LogType.Error);
                    return false;
                }
            }

            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BModder
{
    public class Game
    {
        public string Path { get; private set; }

        public Game(string path)
        {
            Path = path;
        }

        public bool Validate(string? exeName = null)
        {
            if (!Directory.Exists(Path))
            {
                ColorConsole.WriteLineWarning("Invalid path.");
                return false;
            }

            if (exeName != null)
            {
                string exePath = System.IO.Path.Combine(Path, exeName);
                if (!File.Exists(exePath))
                {
                    ColorConsole.WriteLineError($"\nGame not found: \"{exePath}\". Try again.");
                    return false;
                }
            }

            return true;
        }
    }
}

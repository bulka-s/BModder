using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BModder
{

    public static class FileManager
    {
        public static string AskGamePath(string message = "Enter the game path:\n>")
        {
            while (true)
            {
                ColorConsole.WriteInfo($"{message} ");
                string input = Console.ReadLine()?.Trim();

                if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase))
                {
                    return null;
                }

                if (string.IsNullOrWhiteSpace(input))
                {
                    ColorConsole.WriteLineWarning("Path cannot be empty. Try again or enter 'q' to quit.");
                    continue;
                }

                if (Directory.Exists(input) || File.Exists(input))
                {
                    ColorConsole.WriteLineSuccess("Path found!");
                    return input;
                }

                ColorConsole.WriteLineWarning("Invalid path. Try again or enter 'q' to quit.");
            }
        }
    }
}

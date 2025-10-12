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
        public static string? AskGamePath(string message = "Enter the game path:\n>")
        {
            Console.WriteLine();
            int startTop = Console.CursorTop;

            string? errorMessage = null;

            while (true)
            {
                ConsoleHelper.ClearFragment(startTop);

                if (errorMessage != null)
                    ColorConsole.WriteLineWarning(errorMessage);
                

                ColorConsole.WriteInfo($"{message} ");
                string? input = Console.ReadLine()?.Trim();

                if (string.Equals(input, "q", StringComparison.OrdinalIgnoreCase))
                {
                    return null;
                }

                if (string.IsNullOrWhiteSpace(input))
                {
                    errorMessage = "Path cannot be empty. Try again or enter 'q' to quit.";
                    continue;
                }

                if (Directory.Exists(input) || File.Exists(input))
                {
                    return input;
                }

                errorMessage = "Invalid path. Try again or enter 'q' to quit.";
            }
        }
    }
}

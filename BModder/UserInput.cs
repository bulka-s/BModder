using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace BModder
{
    public static class UserInput
    {
        public static bool AskYesNo(string message = "Continue?", string defaultChoice = "y")
        {
            defaultChoice = (defaultChoice.ToLower() == "n") ? "n" : "y";

            string choiceHint = (defaultChoice == "y") ? "[Y/n]" : "[y/N]";

            int startTop = Console.CursorTop;
            

            while (true)
            {
                ConsoleHelper.ClearFragment(startTop);
                Console.WriteLine();
                ColorConsole.WriteLineInfo($"{message} {choiceHint}: ");
                string? input = Console.ReadLine()?.Trim().ToLower();

                if (string.IsNullOrEmpty(input))
                    input = defaultChoice;

                if (input == "y" || input == "yes")
                    return true;

                if (input == "n" || input == "no")
                    return false;
            }
        }

        public static int AskMenu(string[] items, string message = "Select menu item:")
        {
            int startTop = Console.CursorTop;

            while (true)
            { 
                ConsoleHelper.ClearFragment(startTop);

                ColorConsole.WriteLineInfo($"{message}\n");
                for (int i = 0; i < items.Length; i++)
                {
                    ColorConsole.WriteLineInfo($"{i + 1} - {items[i]}");
                }

                ColorConsole.WriteInfo("\n> ");
                string? input = Console.ReadLine()?.Trim();

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= items.Length)
                    return choice;

                ColorConsole.WriteLineWarning("\nInvalid choice. Try again...");
                Thread.Sleep(1000);

            }
        }
    }
}

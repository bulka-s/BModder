using System;
using System.Collections.Generic;
using System.Linq;
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

            while (true)
            {
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
    }
}

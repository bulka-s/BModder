using System;
using System.Collections.Generic;
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

        public static int AskMenu(string message = "Select menu item:", string answ_1, string answ_2)
        {
            int choice = 0;

            while (true)
            {
                ColorConsole.WriteLineInfo($"\n{message}:");
                ColorConsole.WriteLineInfo($"1 - {message}:");
                ColorConsole.WriteLineInfo($"2 -{message}:");
                ColorConsole.WriteInfo($"> ");

                //int? temp = Console.ReadLine();

                

            }

            // по хорошему надо сделать очистку участка, при не правильном вводе
            return choice;
        }
    }
}

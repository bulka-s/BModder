using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BModder
{
    public static class ConsoleHelper
    {
        public static void ClearFragment(int startTop)
        {
            Console.SetCursorPosition(0, startTop);
            for (int i = startTop; i < Console.WindowHeight - 1; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, startTop);
        }

        public static void ClearRow(int rowPos)
        {
            if (rowPos < 0 || rowPos >= Console.WindowHeight)
                return;

            Console.SetCursorPosition(0, rowPos);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, rowPos);

           // need to fix this shiit
        }
    }
}

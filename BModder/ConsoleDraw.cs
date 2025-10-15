using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BModder
{
    static class ConsoleDraw
    {
        public static void Banner()
        {
            ColorConsole.WriteLineInfo("┌──────────────────────────────────────────────────────────────────────────────────────────────────┐");
            ColorConsole.WriteLineInfo("│                                                                                                  │");
            ColorConsole.WriteLineInfo("│        __          __   __            __   ______                                                │");
            ColorConsole.WriteLineInfo("│       / /   ___   / /_ / /_   ____ _ / /  / ____/____   ____ ___   ____   ____ _ ____   __  __   │");
            ColorConsole.WriteLineInfo("│      / /   / _ \\ / __// __ \\ / __ `// /  / /    / __ \\ / __ `__ \\ / __ \\ / __ `// __ \\ / / / /   │");
            ColorConsole.WriteLineInfo("│     / /___/  __// /_ / / / // /_/ // /  / /___ / /_/ // / / / / // /_/ // /_/ // / / // /_/ /    │");
            ColorConsole.WriteLineInfo("│    /_____/\\___/ \\__//_/ /_/ \\__,_//_/   \\____/ \\____//_/ /_/ /_// .___/ \\__,_//_/ /_/ \\__, /     │");
            ColorConsole.WriteLineInfo("│                                                                /_/ mod installer     /____/      │");
            ColorConsole.WriteLineInfo("│                                                                                                  │");
            ColorConsole.WriteLineInfo("└─────────────────────────────────────────────────────────────────────────────── by baton ─────────┘");
        }
    }
}

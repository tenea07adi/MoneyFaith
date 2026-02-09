using Abstractions.Console;
using ConsoleTools;

namespace ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IPrettyConsole prettyConsole = new PrettyConsole();
            ILifecycleConsole app = new LifecycleConsole(prettyConsole);

            app.Run();
        }
    }
}

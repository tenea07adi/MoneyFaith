namespace Abstractions.Console
{
    public interface ILifecycleConsole
    {
        public void Run();
        public void Shutdown();
        public void Restart();
    }
}
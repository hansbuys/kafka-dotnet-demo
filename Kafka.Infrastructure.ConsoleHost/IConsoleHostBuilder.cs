namespace Kafka.Infrastructure.ConsoleHost
{
    public interface IConsoleHostBuilder
    {
        IConsoleHostBuilder UseStartup<T>() where T : IStartup;
        IConsoleHostBuilder UseEnvironment(string environment);

        IConsoleHost Build();
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace Kafka.Infrastructure.ConsoleHost
{
    public interface IStartup
    {
        void ConfigureServices(IServiceCollection services);
    }
}
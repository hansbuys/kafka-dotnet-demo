using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace Kafka.Infrastructure.Testing
{
    public abstract class IoCBasedTest<T> : TestBase
    {
        public IoCBasedTest(ITestOutputHelper output) : base(output)
        {
        }

        protected abstract void AddComponents(IServiceCollection services);
        protected abstract void AddPorts(IServiceCollection services);

        protected Task<T> GetSut()
        {
            var services = new ServiceCollection();

            services.AddLogging(builder => {
                builder.SetMinimumLevel(LogLevel.Trace);
            });

            AddComponents(services);
            AddPorts(services);

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<ILoggerFactory>()
                .AddProvider(LoggerProvider);

            return serviceProvider.GetRequiredService<T>().Async();
        }
    }
}
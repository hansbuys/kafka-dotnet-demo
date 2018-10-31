using Kafka.Infrastructure.ConsoleHost;
using Kafka.Receiver.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Kafka.Receiver
{
    public class Startup : IStartup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => builder.SetMinimumLevel(LogLevel.Trace));

            services.AddComponents();
            services.AddPorts(Configuration);
        }

        public void Configure(ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
        }
    }
}

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Kafka.Infrastructure.ConsoleHost
{
    public class ConsoleHostBuilder<TService> : IConsoleHostBuilder
        where TService : IService
    {
        private const string environmentVariableName = "ENVIRONMENT";

        private Type startupType;
        private string environment;

        public IConsoleHost Build()
        {
            var env = environment ?? Environment.GetEnvironmentVariable(environmentVariableName);

            if (string.IsNullOrWhiteSpace(env))
                throw new EnvironmentException($"Environment not set! This can be set using environment variable {environmentVariableName}");

            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Environment: {0}", env);

            var services = new ServiceCollection();

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .Build();

            IServiceProvider serviceProvider = startupType != null ? 
                UseStartupClass(services, config) : 
                services.BuildServiceProvider();
            
            return new ConsoleHost<TService>(serviceProvider);
        }

        private IServiceProvider UseStartupClass(ServiceCollection services, IConfigurationRoot config)
        {
            var startup = (IStartup)Activator.CreateInstance(startupType, config);

            startup.ConfigureServices(services);

            var startupConfigure = startup.GetType().GetMethod("Configure");

            var serviceProvider = services.BuildServiceProvider();

            var startupConfigureParams = startupConfigure.GetParameters().Select(param =>
            {
                return serviceProvider.GetService(param.ParameterType);
            }).ToArray();

            startupConfigure.Invoke(startup, startupConfigureParams);
            return serviceProvider;
        }

        public IConsoleHostBuilder UseStartup<T>() where T : IStartup
        {
            startupType = typeof(T);

            return this;
        }

        public IConsoleHostBuilder UseEnvironment(string environment)
        {
            this.environment = environment;

            return this;
        }
    }
}
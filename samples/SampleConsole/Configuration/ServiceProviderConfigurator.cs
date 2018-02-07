using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TableStorage.UnsupportedTypes.SampleConsole.Configuration
{
    public class ServiceProviderConfigurator
    {
        public async Task<IServiceProvider> ConfigureTheWorldAsync()
        {
            IServiceCollection services = new ServiceCollection();

            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            var configuration = configurationBuilder.Build();

            var loggerFactory = configuration.ConfigureSerilog();

            services.AddLogging(loggerFactory);
            services.AddLogic();
            await services.AddStorageAsync(configuration);

            return services.BuildServiceProvider();
        }
    }
}

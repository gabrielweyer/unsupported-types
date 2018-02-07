using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using TableStorage.UnsupportedTypes.SampleConsole.Storage;

namespace TableStorage.UnsupportedTypes.SampleConsole.Configuration
{
    public static class ServiceCollectionExtensions
    {
        public static void AddLogic(this IServiceCollection services)
        {
            services.AddSingleton<Presenter>();
        }

        public static async Task AddStorageAsync(this IServiceCollection services, IConfigurationRoot configuration)
        {
            var tableStorageConnectionString = configuration.GetValue<string>("AzureTableStorage:ConnectionString");

            var storageAccount = CloudStorageAccount.Parse(tableStorageConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference(nameof(UnsupportedTypesTestTableEntity));

            await table.CreateIfNotExistsAsync();

            services.AddSingleton(table);
        }

        public static void AddLogging(this IServiceCollection services, ILoggerFactory loggerFactory)
        {
            services.AddSingleton(loggerFactory);
            services.AddLogging();
        }
    }
}

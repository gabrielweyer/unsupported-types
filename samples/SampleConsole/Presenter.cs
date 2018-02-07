using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using TableStorage.UnsupportedTypes.SampleConsole.Storage;

namespace TableStorage.UnsupportedTypes.SampleConsole
{
    public class Presenter
    {
        private readonly CloudTable _table;
        private readonly ILogger<Presenter> _logger;

        public Presenter(CloudTable table, ILogger<Presenter> logger)
        {
            _table = table;
            _logger = logger;
        }

        public async Task RunTheShowAsync()
        {
            var entity = new UnsupportedTypesTestTableEntity
            {
                PartitionKey = "q",
                RowKey = "w",
                VeryImportant = new Unimportant
                {
                    FirstName = "Some First Name",
                    LastName = "Some Last Name"
                }
            };

            _logger.LogInformation("Inserting {@InsertEntity}", entity);

            var insertOperation = TableOperation.InsertOrReplace(entity);
            var insertResult = await _table.ExecuteAsync(insertOperation);

            _logger.LogInformation("Insert result was {InsertStatusCode}", insertResult.HttpStatusCode);

            var retrieveOperation = TableOperation
                .Retrieve<UnsupportedTypesTestTableEntity>(entity.PartitionKey, entity.RowKey);

            var retrieveResult = await _table.ExecuteAsync(retrieveOperation);

            _logger.LogInformation("Retrieve result was {RetrieveStatusCode}", retrieveResult.HttpStatusCode);

            var retrievedEntity = retrieveResult.Result as UnsupportedTypesTestTableEntity;

            _logger.LogInformation("Retrieved {@RetrieveEntity}", retrievedEntity);
        }
    }
}

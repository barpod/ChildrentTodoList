
using ChildrenTodoList.Models;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace ChildrenTodoList.Services.CosmosDb
{
    public class TasksCosmosDbService : ITasksDbService
    {
        public readonly static string OneTimeTasksContainerName = "OneTimeTasks";
        private readonly DocumentClient _documentClient;
        private readonly IChildrenDbService _childrenDbService;
        private readonly string _dbName;

        public TasksCosmosDbService(DocumentClient documentClient, IChildrenDbService childrenDbService, IOptions<CosmosDBServiceOptions> options)
        {
            _documentClient = documentClient;
            _childrenDbService = childrenDbService;
            _dbName = options.Value.CosmosDbName;
        }
        
        public async Task<OneTimeTask> AddOneTimeTaskAsync(string childId, OneTimeTaskInput input)
        {
            var child = await _childrenDbService.GetChildAsync(childId);
            var tasksCollectionUri = UriFactory.CreateDocumentCollectionUri(_dbName, OneTimeTasksContainerName);
            var dbResponse = await _documentClient.CreateDocumentAsync(
                tasksCollectionUri,
                new { input.Name, input.Deadline, child});
            return (dynamic)dbResponse.Resource;
        }

        public async Task<OneTimeTask> GetOneTimeTaskAsync(string taskId)
        {
            var getDocUri = UriFactory.CreateDocumentUri(_dbName, OneTimeTasksContainerName, taskId);
            DocumentResponse<OneTimeTask> documentResponse = await _documentClient.ReadDocumentAsync<OneTimeTask>(
                getDocUri, new RequestOptions { PartitionKey = new PartitionKey(Undefined.Value) });
            return documentResponse.Document;
        }
    }
}

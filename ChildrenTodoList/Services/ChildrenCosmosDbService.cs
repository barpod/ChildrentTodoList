using ChildrenTodoList.Models;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents;
using System;

namespace ChildrenTodoList.Services
{
    public class ChildrenCosmosDbService : IChildrenDbService
    {
        private readonly string _dbName;
        private readonly string _containerName;
        private readonly DocumentClient _documentClient;

        public ChildrenCosmosDbService( DocumentClient documentClient)
        {
            _documentClient = documentClient;
            _dbName = "ChildrenTodoListDb";
            _containerName = "Children";
        }

        public async Task<Child> AddChildAsync(ChildInput childInput)
        {
            var childrenCollectionUri = UriFactory.CreateDocumentCollectionUri(_dbName, _containerName);
            var dbResponse = await _documentClient.CreateDocumentAsync(
                childrenCollectionUri,
                new { childInput.FirstName, childInput.LastName });
            return (dynamic)dbResponse.Resource;
        }

        public async Task<Child> GetChildAsync(string id)
        {
            var getDocUri = UriFactory.CreateDocumentUri(_dbName, _containerName, id);
            DocumentResponse<Child> documentResponse = await _documentClient.ReadDocumentAsync<Child>(
                getDocUri, new RequestOptions { PartitionKey = new PartitionKey(Undefined.Value) } );
            return documentResponse.Document;
        }
    }
}
using Azure.Cosmos;
using ChildrenTodoList.Services.CosmosDb;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChildrenTodoList.Tests
{
    public class IntegrationTestsBase
    {
        private TestServer _server;
        protected HttpClient _client;
        private IConfiguration _configuration;
        private CosmosClient _cosmosClient;
        private readonly DatabaseResponse _databaseResponse;

        [SetUp]
        public async System.Threading.Tasks.Task SetupAsync()
        {
            IConfigurationBuilder configurationBuilder = GetLocalAppSettings();
            _configuration = configurationBuilder.Build();
            await SetupDb();

            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration(configBuilder => Configure(configBuilder)));
            _client = _server.CreateClient();
        }
        private async Task SetupDb()
        {
            _cosmosClient = new CosmosClient(
                _configuration[CosmosDbConfigurationConstants.DbUri],
                _configuration[CosmosDbConfigurationConstants.DbKey]);

            var databaseResponse = await _cosmosClient.CreateDatabaseIfNotExistsAsync("ChildrenTodoListDb", 10000);

            await databaseResponse.Database.CreateContainerIfNotExistsAsync(ChildrenCosmosDbService.ChildrenContainerName , "/PartitionKey");
            await databaseResponse.Database.CreateContainerIfNotExistsAsync(TasksCosmosDbService.OneTimeTasksContainerName, "/TaskId");
        }

        public static IConfigurationBuilder GetLocalAppSettings()
        {
            return Configure(new ConfigurationBuilder());
        }

        private static IConfigurationBuilder Configure(IConfigurationBuilder configurationBuilder)
        {
            return configurationBuilder
                .SetBasePath(TestContext.CurrentContext.TestDirectory)
                .AddJsonFile("appsettings.Local.json", optional: true)
                .AddEnvironmentVariables();
        }

        [TearDown]
        public async Task TeardownAsync()
        {
            await _databaseResponse.Database.DeleteAsync();
            _cosmosClient.Dispose();
        }
    }
}

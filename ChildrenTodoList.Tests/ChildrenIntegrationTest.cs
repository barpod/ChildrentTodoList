using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Azure.Cosmos;
using ChildrenTodoList.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using NUnit.Framework;

namespace ChildrenTodoList.Tests
{
    public class ChildrenIntegrationTest
    {
        private TestServer _server;
        private HttpClient _client;
        private IConfiguration _configuration;
        private CosmosClient _cosmosClient;
        private DatabaseResponse _databaseResponse;
        private ContainerResponse _childContainerResponse;
        private ContainerResponse _todoListTaskContainerResponse;

        [SetUp]
        public async Task Setup()
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

            _databaseResponse = await _cosmosClient.CreateDatabaseIfNotExistsAsync("ChildrenTodoListDb", 10000);

            _childContainerResponse = await _databaseResponse.Database.CreateContainerIfNotExistsAsync("Children", "/PartitionKey");
            _todoListTaskContainerResponse = await _databaseResponse.Database.CreateContainerIfNotExistsAsync("TodoListTasks", "/TaskId");
        }

        [TearDown]
        public void Teardown()
        {
            _childContainerResponse.Container.DeleteContainerAsync();
            _todoListTaskContainerResponse.Container.DeleteContainerAsync();
            _cosmosClient.Dispose();
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

        [Test]
        public async Task PostShouldAddChildrenToTheDb()
        {
            var model = new ChildInput("LastName", "FirstName");
            var postedChild = await _client.PostAsync( "/api/children",
                new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json"))
                .DeserializeAsync<Child>();
            var gotChild = await _client.GetAsync($"/api/children/{postedChild.Id}").DeserializeAsync<Child>();
            Assert.AreEqual(gotChild, postedChild);
        }
    }
}
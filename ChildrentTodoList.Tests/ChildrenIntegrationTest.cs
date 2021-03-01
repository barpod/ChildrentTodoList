using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ChildrenTodoList;
using ChildrenTodoList.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ChildrentTodoList.Tests
{
    public class ChildrenIntegrationTest
    {
        private TestServer _server;
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>()
                .UseEnvironment("Local"));
            _client = _server.CreateClient();
        }

        [Test]
        [Ignore("Need to setup CosmosDB first")]
        public async Task PostShouldAddChildrenToTheDb()
        {
            var model = new Child("Id", "LastName", "FirstName");
            var json = JsonConvert.SerializeObject(model);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            object p = await _client.PostAsync("/children", stringContent);
            HttpResponseMessage httpResponseMessage = await _client.GetAsync("/children");
            string response = await httpResponseMessage.Content.ReadAsStringAsync();
            Assert.AreEqual($"[{json}]", response);
        }
    }
}
using System.Threading.Tasks;
using ChildrenTodoList.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace ChildrenTodoList.Tests
{
    public class ChildrenIntegrationTest : IntegrationTestsBase
    {
        [SetUp]
        public new async Task SetupAsync()
        {
            await SetupAsync();
        }

        [TearDown]
        public new async Task TeardownAsync()
        {
            await TeardownAsync();
        }

        [Test]
        public async Task PostShouldAddChildrenToTheDb()
        {
            var model = new ChildInput("LastName", "FirstName");
            var postedChild = await _client.PostAndDeserializeAsync<Child>("/api/children", model);
            var gotChild = await _client.GetAsync($"/api/children/{postedChild.Id}").DeserializeAsync<Child>();
            Assert.AreEqual(gotChild, postedChild);
        }

        [Test]
        public async Task GetShouldReturnAllChildren()
        {
            var postedChild1 = await _client.PostAndDeserializeAsync<Child>(
                "/api/children", new ChildInput("LastName1", "FirstName1"));
            var postedChild2 = await _client.PostAndDeserializeAsync<Child>(
                "/api/children", new ChildInput("LastName2", "FirstName2"));
            var children = await _client.GetAsync($"/api/children/").DeserializeAsync<IEnumerable<Child>>();
            CollectionAssert.AreEquivalent(new[] { postedChild1, postedChild2 }, children);
        }
    }
}
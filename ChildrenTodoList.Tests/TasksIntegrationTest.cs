using System.Threading.Tasks;
using ChildrenTodoList.Models;
using NUnit.Framework;
using System;

namespace ChildrenTodoList.Tests
{
    [TestFixture]
    public class TasksIntegrationTest : IntegrationTestsBase
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
        public async Task PostShouldAddOneTimeTaskToTheDb()
        {
            var child = await _client.PostAndDeserializeAsync<Child>("/api/children", 
                new {LastName="LastName", FirstName="FirstName" });
            var postedTask = await _client.PostAndDeserializeAsync<OneTimeTask>($"/api/tasks/onetime/{child.Id}", 
                new OneTimeTaskInput("Clean the room", new DateTimeOffset(2021, 3,4, 19, 30, 00, TimeSpan.FromHours(1))));
            var gotTask = await _client.GetAsync($"/api/tasks/onetime/{postedTask.Id}").DeserializeAsync<OneTimeTask>();
            Assert.AreEqual(postedTask, gotTask);
        }
    }
}
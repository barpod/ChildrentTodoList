using System.Collections.Generic;
using System.Linq;
using ChildrenTodoList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChildrenTodoList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoListController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TodoListController> _logger;

        public TodoListController(ILogger<TodoListController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<TodoList> Get()
        {
            return Enumerable.Empty<TodoList>();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChildrenTodoList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChildrenTodoList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoListTaskController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<TodoListTaskController> _logger;

        public TodoListTaskController(ILogger<TodoListTaskController> logger)
        {
            _logger = logger;
        }

        [HttpPost("{id}/{firstName}/{lastName}")]
        public async Task<JsonResult> PostChild(Child child)
        {
            //Child child = await _childrenDbService.AddChildAsync(childInput);
            return new JsonResult(child);
        }

        [HttpGet("{id}/{firstName}/{lastName}")]
        public IEnumerable<TodoListTask> Get(Child child)
        {
            return Enumerable.Empty<TodoListTask>();
        }
    }
}

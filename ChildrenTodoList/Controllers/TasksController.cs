using System.Threading.Tasks;
using ChildrenTodoList.Models;
using ChildrenTodoList.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChildrenTodoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> _logger;
        private readonly ITasksDbService _tasksDbService;

        public TasksController(
            ILogger<TasksController> logger,
            ITasksDbService tasksDbService)
        {
            _logger = logger;
            _tasksDbService = tasksDbService;
        }

        [HttpPost("onetime/{childId}")]
        public async Task<JsonResult> PostOneTimeTimeTaskAsync(string childId, OneTimeTaskInput input)
        {
            var oneTimeTask = await _tasksDbService.AddOneTimeTaskAsync(childId, input);
            return new JsonResult(oneTimeTask);
        }

        [HttpGet("onetime/{taskId}")]
        public async Task<JsonResult> GetOneTime(string taskId)
        {
            OneTimeTask oneTimeTask = await _tasksDbService.GetOneTimeTaskAsync(taskId);
            return new JsonResult(oneTimeTask);
        }
    }
}

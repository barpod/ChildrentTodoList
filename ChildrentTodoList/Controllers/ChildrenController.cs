using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChildrenTodoList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChildrenController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<ChildrenController> _logger;

        public ChildrenController(ILogger<ChildrenController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Child> Get()
        {
            return new List<Child>
            {
                new Child(new Guid().ToString(), "Gustaw", "Podlejski"),
                new Child(new Guid().ToString(), "Julianna", "Podlejska")
            };
        }
    }
}

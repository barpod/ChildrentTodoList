using System;
using System.Collections.Generic;
using ChildrenTodoList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChildrenTodoList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChildrenController : ControllerBase
    {
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
                new Child(Guid.NewGuid().ToString(), "Gustaw", "Podlejski"),
                new Child(Guid.NewGuid().ToString(), "Julianna", "Podlejska")
            };
        }
    }
}

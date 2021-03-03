using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChildrenTodoList.Models;
using ChildrenTodoList.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChildrenTodoList.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChildrenController : ControllerBase
    {
        private readonly ILogger<ChildrenController> _logger;
        private readonly IChildrenDbService _childrenDbService;

        public ChildrenController(ILogger<ChildrenController> logger, IChildrenDbService childrenDbService)
        {
            _logger = logger;
            _childrenDbService = childrenDbService;
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetChildAsync(String id)
        {
            Child child = await _childrenDbService.GetChildAsync(id);
            return new JsonResult(child);
        }

        [HttpGet]
        public async Task<JsonResult> GetChildrenAsync()
        {
            IEnumerable<Child> children = await _childrenDbService.GetChildrenAsync();
            return new JsonResult(children);
        }

        [HttpPost]
        public async Task<JsonResult> PostChild(ChildInput childInput)
        {
            Child child = await _childrenDbService.AddChildAsync(childInput);
            return new JsonResult(child);
        }
    }
}

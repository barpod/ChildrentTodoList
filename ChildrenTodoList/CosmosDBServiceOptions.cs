using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildrenTodoList
{
    /// <summary>
    /// Options loaded from config at startup
    /// </summary>
    public class CosmosDBServiceOptions
    {
        public string CosmosDbUri { get; set; }
        public string CosmosDbKey { get; set; }
    }
}


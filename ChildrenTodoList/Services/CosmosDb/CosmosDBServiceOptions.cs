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


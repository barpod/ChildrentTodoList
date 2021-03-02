using ChildrenTodoList.Models;

namespace ChildrenTodoList.Services
{
    public static class CosmosDbChildExtensions
    {
        public static string PartitionKey(this ChildInput child)
        {
            return child.FirstName + child.LastName;
        }
    }
}

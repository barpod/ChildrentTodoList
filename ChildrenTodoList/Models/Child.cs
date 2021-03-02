
namespace ChildrenTodoList.Models
{
    public record Child(string Id, string FirstName, string LastName);

    public record ChildInput(string FirstName, string LastName)
    {
        public Child ToChild(string id)
        {
            return new Child(id, FirstName, LastName);
        }
    }
}
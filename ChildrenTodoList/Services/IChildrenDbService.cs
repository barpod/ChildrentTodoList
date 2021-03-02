using ChildrenTodoList.Models;
using System.Threading.Tasks;

namespace ChildrenTodoList.Services
{
    public interface IChildrenDbService
    {
        Task<Child> AddChildAsync(ChildInput child);
        Task<Child> GetChildAsync(string id);
    }
}
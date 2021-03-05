using ChildrenTodoList.Models;
using System.Threading.Tasks;

namespace ChildrenTodoList.Services
{
    public interface ITasksDbService
    {
        Task<OneTimeTask> AddOneTimeTaskAsync(string childId, OneTimeTaskInput input);
        Task<OneTimeTask> GetOneTimeTaskAsync(string taskId);
    }
}

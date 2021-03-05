
using System.Collections.Generic;

namespace ChildrenTodoList.Models
{
    public record TodoList(string id, List<OneTimeTask> todoListTasks);
}
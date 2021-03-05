
using System;

namespace ChildrenTodoList.Models
{
    public record OneTimeTaskInput(string Name, DateTimeOffset Deadline);

    public record OneTimeTask(string Id, Child Child, string Name, DateTimeOffset Deadline);
}

using System.Collections.Generic;

public class TodoList
{
    public string Id { get; }
    public List<TodoListTask> TodoListTasks { get; }

    public TodoList(string id, List<TodoListTask> todoListTasks)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new System.ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
        }

        if (todoListTasks is null)
        {
            throw new System.ArgumentNullException(nameof(todoListTasks));
        }
        Id = id;
        TodoListTasks = todoListTasks;
    }
}
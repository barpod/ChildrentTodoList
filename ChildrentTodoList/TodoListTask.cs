
public class TodoListTask
{
    public string Id { get; }
    public string Name { get; }

    public TodoListTask(string id, string name)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new System.ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new System.ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
        }
        Id = id;
        Name = name;
    }

}

public class Child
{
    public string Id { get; }
    public string FirstName { get; }
    public string LastName { get; }

    public Child(string id, string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new System.ArgumentException($"'{nameof(id)}' cannot be null or whitespace.", nameof(id));
        }

        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new System.ArgumentException($"'{nameof(firstName)}' cannot be null or whitespace.", nameof(firstName));
        }

        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new System.ArgumentException($"'{nameof(lastName)}' cannot be null or whitespace.", nameof(lastName));
        }
        Id = id;
        FirstName = firstName;
        LastName = lastName;
    }
}
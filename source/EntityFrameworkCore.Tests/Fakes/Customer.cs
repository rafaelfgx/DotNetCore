using DotNetCore.Domain;

namespace DotNetCore.EntityFrameworkCore.Tests;

public class Customer : Entity<long>
{
    public Customer() { }

    public Customer(Name name, Guid code)
    {
        Name = name;
        Code = code;
    }

    public Customer(long id, Name name, Guid code)
    {
        Id = id;
        Name = name;
        Code = code;
    }

    public Name Name { get; private set; }

    public Guid Code { get; private set; }

    public IReadOnlyCollection<Phone> Phones { get; private set; } = new List<Phone>();
}

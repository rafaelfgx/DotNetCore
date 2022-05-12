namespace DotNetCore.EntityFrameworkCore.Tests;

public sealed record CustomerModel
{
    public long Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public Guid Code { get; set; }

    public IReadOnlyCollection<PhoneModel> Phones { get; set; } = new List<PhoneModel>();
}

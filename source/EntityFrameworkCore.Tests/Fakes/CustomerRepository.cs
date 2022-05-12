namespace DotNetCore.EntityFrameworkCore.Tests;

public sealed class CustomerRepository : EFRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(Context context) : base(context) { }
}

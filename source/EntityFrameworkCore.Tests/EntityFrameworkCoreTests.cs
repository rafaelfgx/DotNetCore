using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DotNetCore.EntityFrameworkCore.Tests;

[TestClass]
public class EntityFrameworkCoreTests
{
    private ICustomerRepository _customerRepository;

    private IUnitOfWork _unitOfWork;

    [TestInitialize]
    public void Initialize()
    {
        const string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Tests;Integrated Security=true;Connection Timeout=5;";

        var services = new ServiceCollection();

        services.AddDbContextPool<Context>(options => options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork, UnitOfWork<Context>>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        var provider = services.BuildServiceProvider();

        provider.GetRequiredService<Context>().Database.EnsureDeleted();

        provider.GetRequiredService<Context>().Database.EnsureCreated();

        provider.GetRequiredService<Context>().Database.Migrate();

        _unitOfWork = provider.GetRequiredService<IUnitOfWork>();

        _customerRepository = provider.GetRequiredService<ICustomerRepository>();
    }

    [TestMethod]
    public void Add()
    {
        var initialCount = _customerRepository.Count();

        var customer = new Customer(new Name(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()), Guid.NewGuid());

        _customerRepository.Add(customer);

        _unitOfWork.SaveChangesAsync().Wait();

        var finalCount = _customerRepository.Count();

        Assert.AreNotEqual(initialCount, finalCount);
    }

    [TestMethod]
    public void Update()
    {
        var customer = new Customer(new Name(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()), Guid.NewGuid());

        _customerRepository.Add(customer);

        _unitOfWork.SaveChangesAsync().Wait();

        customer = _customerRepository.Get(1L);

        var customerUpdate1 = new Customer(1L, new Name(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()), Guid.NewGuid());

        _customerRepository.Update(customerUpdate1);

        _unitOfWork.SaveChangesAsync().Wait();

        customerUpdate1 = _customerRepository.Get(1L);

        var customerUpdate2 = new Customer(1L, new Name(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()), Guid.NewGuid());

        _customerRepository.Update(customerUpdate2);

        _unitOfWork.SaveChangesAsync().Wait();

        customerUpdate2 = _customerRepository.Get(1L);

        Assert.AreNotEqual(customer.Code, customerUpdate1.Code);

        Assert.AreNotEqual(customer.Name.FirstName, customerUpdate1.Name.FirstName);

        Assert.AreNotEqual(customer.Name.LastName, customerUpdate1.Name.LastName);

        Assert.AreNotEqual(customerUpdate1.Code, customerUpdate2.Code);

        Assert.AreNotEqual(customerUpdate1.Name.FirstName, customerUpdate2.Name.FirstName);

        Assert.AreNotEqual(customerUpdate1.Name.LastName, customerUpdate2.Name.LastName);
    }

    [TestMethod]
    public void UpdatePartialPrimitive()
    {
        var customer = new Customer(new Name(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()), Guid.NewGuid());

        _customerRepository.Add(customer);

        _unitOfWork.SaveChangesAsync().Wait();

        customer = _customerRepository.Get(1L);

        var customerCode = customer.Code;

        var customerNameFirstName = customer.Name.FirstName;

        var customerNameLastName = customer.Name.LastName;

        _customerRepository.UpdatePartial(new { Id = 1L, Code = Guid.NewGuid() });

        _unitOfWork.SaveChangesAsync().Wait();

        var customerUpdate = _customerRepository.Get(1L);

        Assert.AreNotEqual(customerCode, customerUpdate.Code);

        Assert.AreEqual(customerNameFirstName, customerUpdate.Name.FirstName);

        Assert.AreEqual(customerNameLastName, customerUpdate.Name.LastName);
    }

    [TestMethod]
    public void UpdatePartialOwnedType()
    {
        var customer = new Customer(new Name(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()), Guid.NewGuid());

        _customerRepository.Add(customer);

        _unitOfWork.SaveChangesAsync().Wait();

        customer = _customerRepository.Get(1L);

        var customerCode = customer.Code;

        var customerNameFirstName = customer.Name.FirstName;

        var customerNameLastName = customer.Name.LastName;

        _customerRepository.UpdatePartial(new { Id = 1L, Name = new { LastName = Guid.NewGuid().ToString() } });

        _unitOfWork.SaveChangesAsync().Wait();

        var customerUpdate = _customerRepository.Get(1L);

        Assert.AreEqual(customerCode, customerUpdate.Code);

        Assert.AreEqual(customerNameFirstName, customerUpdate.Name.FirstName);

        Assert.AreNotEqual(customerNameLastName, customerUpdate.Name.LastName);
    }

    [TestMethod]
    public void Delete()
    {
        var initialCount = _customerRepository.Count();

        var customer = new Customer(new Name(Guid.NewGuid().ToString(), Guid.NewGuid().ToString()), Guid.NewGuid());

        _customerRepository.Add(customer);

        _unitOfWork.SaveChangesAsync().Wait();

        _customerRepository.Delete(1L);

        _unitOfWork.SaveChangesAsync().Wait();

        var finalCount = _customerRepository.Count();

        Assert.AreEqual(initialCount, finalCount);
    }
}

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security.Claims;

namespace DotNetCore.Security.Tests;

[TestClass]
public class SecurityTests
{
    private readonly ICryptographyService _cryptographyService;
    private readonly IHashService _hashService;
    private readonly IJwtService _jwtService;

    public SecurityTests()
    {
        var services = new ServiceCollection();

        services.AddSingleton<ICryptographyService>(_ => new CryptographyService(Guid.NewGuid().ToString()));
        _cryptographyService = services.BuildServiceProvider().GetService<ICryptographyService>();

        services.AddSingleton<IHashService, HashService>();
        _hashService = services.BuildServiceProvider().GetService<IHashService>();

        services.AddSingleton<IJwtService>(_ => new JwtService(new JwtSettings(Guid.NewGuid().ToString(), TimeSpan.FromHours(12))));
        _jwtService = services.BuildServiceProvider().GetService<IJwtService>();
    }

    [TestMethod]
    public void CryptographyService()
    {
        const string value = nameof(SecurityTests);

        var salt = Guid.NewGuid().ToString();

        var crypt = _cryptographyService.Encrypt(value, salt);

        var decrypt = _cryptographyService.Decrypt(crypt, salt);

        Assert.AreEqual(value, decrypt);
    }

    [TestMethod]
    public void HashService()
    {
        const string value = nameof(SecurityTests);

        var hash = _hashService.Create(value, Guid.NewGuid().ToString());

        Assert.IsNotNull(hash);
    }

    [TestMethod]
    public void JwtService()
    {
        var claims = new List<Claim> { new("sub", Guid.NewGuid().ToString()) };

        var token = _jwtService.Encode(claims);

        Assert.IsNotNull(token);
    }
}

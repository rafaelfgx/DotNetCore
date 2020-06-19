using DotNetCore.Security;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DotNetCore.Logging.Tests
{
    [TestClass]
    public class SecurityTests
    {
        private readonly ICryptographyService _cryptographyService;
        private readonly IHashService _hashService;

        public SecurityTests()
        {
            var services = new ServiceCollection();

            services.AddSingleton<ICryptographyService>(_ => new CryptographyService(Guid.NewGuid().ToString()));
            services.AddSingleton<IHashService>(_ => new HashService(10000, 128));

            _cryptographyService = services.BuildServiceProvider().GetService<ICryptographyService>();
            _hashService = services.BuildServiceProvider().GetService<IHashService>();
        }

        [TestMethod]
        public void CryptographyService()
        {
            const string value = nameof(SecurityTests);

            var crypt = _cryptographyService.Encrypt(value);

            var decrypt = _cryptographyService.Decrypt(crypt);

            Assert.AreEqual(value, decrypt);
        }

        [TestMethod]
        public void HashService()
        {
            const string value = nameof(SecurityTests);

            var hash = _hashService.Create(value, Guid.NewGuid().ToString());

            Assert.IsNotNull(hash);
        }
    }
}

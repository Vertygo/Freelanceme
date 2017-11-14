using Freelanceme.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Freelanceme.Tests.Services
{
    [TestClass]
    public class ClientServiceTest
    {
        private static ApplicationDbContext context;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("AppTest");

            context = new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}

using Freelanceme.Data.EntityFramework;
using Freelanceme.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Linq;

namespace Freelanceme.Tests.Data
{
    [TestClass]
    public class RepositoryTest
    {
        private static ApplicationDbContext context;

        [ClassInitialize]
        public static void Setup(TestContext testContext)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseInMemoryDatabase("AppTest");

            context = new ApplicationDbContext(optionsBuilder.Options);
        }

        [TestMethod]
        public async Task AddAsync_User()
        {
            var set = context.GetSet<User>();

            await set.AddAsync(new User("ivan", "milosavljevic", "ivanm", "verthygo@gmail.com"));
            await context.SaveChangesAsync();
            var item = await set.FirstOrDefaultAsync();

            Assert.IsNotNull(item);
        }
    }
}

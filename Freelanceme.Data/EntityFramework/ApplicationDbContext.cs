using Freelanceme.Security;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Freelanceme.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Freelanceme.Data.EntityFramework.Mappings;

namespace Freelanceme.Data.EntityFramework
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>, IDbContext
    {
        private readonly IConfiguration _config;

        public IDbContextTransaction BeginTransaction() =>
            Database.BeginTransaction();

        public ApplicationDbContext(IConfiguration config)
            : base(new DbContextOptions<ApplicationDbContext>()) =>
            _config = config;

        public DbSet<TEntity> GetSet<TEntity>() where TEntity : Entity =>
            Set<TEntity>();

        public Task<int> SaveAsync() =>
            SaveChangesAsync();

        public void SaveSync() =>
            SaveChanges();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(_config[Constants.CONNECTION_STRING]);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new PartnerMap());
            builder.ApplyConfiguration(new ProjectMap());
            builder.ApplyConfiguration(new TimeTrackingMap());
            builder.ApplyConfiguration(new ConfigurationMap());
        }
    }
}

using Freelanceme.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Freelanceme.Data.EntityFramework.Mappings
{
    public class ConfigurationMap : IEntityTypeConfiguration<Configuration>
    {
        public void Configure(EntityTypeBuilder<Configuration> builder)
        {
            builder.ToTable(nameof(Configuration));
        }
    }
}

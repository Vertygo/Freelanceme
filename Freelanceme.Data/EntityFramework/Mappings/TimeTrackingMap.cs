using Freelanceme.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Freelanceme.Data.EntityFramework.Mappings
{
    public class TimeTrackingMap : IEntityTypeConfiguration<TimeTracking>
    {
        public void Configure(EntityTypeBuilder<TimeTracking> builder)
        {
            builder.ToTable(nameof(TimeTracking));
        }
    }
}

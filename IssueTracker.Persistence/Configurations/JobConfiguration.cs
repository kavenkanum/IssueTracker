using IssueTracker.Domain.Entities;
using IssueTracker.Domain.Language.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace IssueTracker.Persistence.Configurations
{
    public class JobConfiguration : IEntityTypeConfiguration<Job>
    {
        public void Configure(EntityTypeBuilder<Job> builder)
        {
            builder.OwnsOne(j => j.Deadline, builder =>
            {
                builder
                    .Property(d => d.DeadlineDate)
                    .HasColumnName("Deadline");
            });
            builder.Property(j => j.AssignedUserId)
                .HasDefaultValue(Guid.Empty);
        }

        
    }
}

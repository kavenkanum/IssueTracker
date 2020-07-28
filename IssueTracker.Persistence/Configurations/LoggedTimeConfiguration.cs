using IssueTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.Persistence.Configurations
{
    public class LoggedTimeConfiguration : IEntityTypeConfiguration<LoggedTime>
    {
        public void Configure(EntityTypeBuilder<LoggedTime> builder)
        {
            builder.HasOne<Job>()
                .WithMany(j => j.TotalLoggedTime)
                .HasForeignKey(lt => lt.JobId);

            builder.HasOne<User>()
                .WithMany(u => u.LoggedTimes)
                .HasForeignKey(lt => lt.UserId);
        }
    }
}

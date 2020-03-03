using IssueTracker.Domain.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using IssueTracker.Domain;

namespace IssueTracker.Data
{
    public class IssueTrackerDbContext : ApiAuthorizationDbContext<User>
    {
        public IssueTrackerDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Comment> Comments { get; set; }


    }
}

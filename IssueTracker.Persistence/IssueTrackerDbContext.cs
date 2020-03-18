using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using IssueTracker.Domain.Entities;
using IssueTracker.Domain;

namespace IssueTracker.Persistence
{
    public class IssueTrackerDbContext : ApiAuthorizationDbContext<User>
    {
        public IssueTrackerDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Job> Jobs { get; set; }
    }

}

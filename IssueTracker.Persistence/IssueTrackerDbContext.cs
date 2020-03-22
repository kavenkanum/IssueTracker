using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using IssueTracker.Domain.Entities;
using IssueTracker.Domain;

namespace IssueTracker.Persistence
{
    public class IssueTrackerDbContext : DbContext
    {
        public IssueTrackerDbContext(
            DbContextOptions<IssueTrackerDbContext> options) : base(options)
        {
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<User> Users { get; set; }
    }

}

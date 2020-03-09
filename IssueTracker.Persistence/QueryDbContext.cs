using IssueTracker.Domain;
using IssueTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IssueTracker.Persistence
{
    public class QueryDbContext
    {
        private readonly IssueTrackerDbContext _issueTrackerDbContext;

        public QueryDbContext(IssueTrackerDbContext issueTrackerDbContext)
        {
            _issueTrackerDbContext = issueTrackerDbContext;
        }

        public IQueryable<Project> Projects => _issueTrackerDbContext.Projects.AsNoTracking();
        public IQueryable<Job> Jobs => _issueTrackerDbContext.Jobs.AsNoTracking();
    }
}

using IssueTracker.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IssueTracker.UnitTests.Queries
{
    public class QueryTestFixture : IDisposable
    {
        private IssueTrackerDbContext _issueTrackerDbContext { get; set; }
        public QueryDbContext QueryDbContext { get; private set; }

        public QueryTestFixture()
        {
            var optionsBuilder = new DbContextOptionsBuilder<IssueTrackerDbContext>();
            optionsBuilder.UseInMemoryDatabase("QueryDbContext");
            _issueTrackerDbContext = new IssueTrackerDbContext(optionsBuilder.Options);
            QueryDbContext = new QueryDbContext(_issueTrackerDbContext);

        }
        public void Dispose()
        {
            _issueTrackerDbContext.Dispose();
        }
    }
}

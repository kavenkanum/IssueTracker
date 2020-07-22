using CSharpFunctionalExtensions;
using FluentAssertions;
using IssueTracker.Persistence;
using IssueTracker.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace IssueTracker.UnitTests.Queries
{
    public class GetListOfProjectsQueryHandlerTests
    {
        private readonly QueryDbContext _context;

        public GetListOfProjectsQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.QueryDbContext;
        }

        [Fact]
        public async Task GetListOfProjects()
        {
            var sut = new GetJobQueryHandler(_context);

            var result = await sut.Handle(new GetJobQuery(1, 2) { }, CancellationToken.None);
            result.Should().BeOfType(typeof(Task<Result<JobDto>>));
        }

    }
}

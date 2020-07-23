using CSharpFunctionalExtensions;
using FluentAssertions;
using IssueTracker.Domain;
using IssueTracker.Persistence;
using IssueTracker.Queries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace IssueTracker.UnitTests.Queries
{
    using static TestFixture;
    public class GetListOfProjectsQueryHandlerTests : IClassFixture<TestFixture>
    {
        private readonly QueryDbContext _context;

        public GetListOfProjectsQueryHandlerTests(TestFixture fixture)
        {
            _context = fixture.QueryDbContext;
        }

        [Fact]
        public async Task GetListOfProjects()
        {
            var sut = new GetJobQueryHandler(_context);

            var result = await sut.Handle(new GetJobQuery(1, 2) { }, CancellationToken.None);
            result.Should().BeOfType(typeof(Result<JobDto>));
        }

        [Fact]
        public async Task ShouldReturnAllProjectsList()
        {
            var sut = new GetListOfProjectsQueryHandler(_context);

            var result = await sut.Handle(new GetListOfProjectsQuery(), CancellationToken.None);
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
        }


    }
}

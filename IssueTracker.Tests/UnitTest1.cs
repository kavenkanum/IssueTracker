using IssueTracker.Domain.Repositories;
using System;
using Xunit;
using Moq;
using IssueTracker.Domain;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace IssueTracker.Tests
{
    public class RepositoriesTests
    {

        private IssueTrackerDbContext prvGetMockIssueTrackerDbContextFeed()
        {
            var mockSet = new Mock<DbSet<Project>>();
            var mockContext = new Mock<IssueTrackerDbContext>();
            mockContext.Setup(m => m.Projects).Returns(mockSet.Object);
            return mockContext.Object;
        }

        [Fact]
        public void AddNewProject()
        {
            IssueTrackerDbContext feed = this.prvGetMockIssueTrackerDbContextFeed();
            IProjectRepository _projectRepository = new ProjectRepository(feed);
            _projectRepository.Add("Mission to the moon");
            var expected = 1;
            //var actual = _projectRepository.GetAllProjects().ToList().Count;

            Assert.Equal(expected, actual);

        }
    }
}

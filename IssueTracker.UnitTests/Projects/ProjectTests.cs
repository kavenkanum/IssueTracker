using FluentAssertions;
using IssueTracker.Domain;
using Xunit;

namespace IssueTracker.UnitTests.Projects
{

    public class ProjectTests
    {
        [Fact]
        public void ShouldCreateProject()
        {
            var project = Project.Create("Some project");

            project.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void ShouldNotCreateProjectWithEmptyName()
        {
            var project = Project.Create(string.Empty);

            project.IsSuccess.Should().BeFalse();
        }
    }
}

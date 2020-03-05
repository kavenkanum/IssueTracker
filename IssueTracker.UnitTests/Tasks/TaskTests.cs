using FluentAssertions;
using IssueTracker.Domain;
using IssueTracker.Domain.Entities;
using Xunit;

namespace IssueTracker.UnitTests.Projects
{

    public class TaskTests
    {
        [Fact]
        public void ShouldCreateTask()
        {
            var task = Task.Create("Some task");

            task.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void ShouldAddTaskToProject()
        {
            var project = Project.Create("Some project");
            var task = Task.Create("Some task");
            project.Value.AddTask(task.Value);

            task.IsSuccess.Should().BeTrue();
            project.IsSuccess.Should().BeTrue();
            project.Value.Tasks.Count.Should().Be(1);
        }

        [Fact]
        public void ShouldNotCreateTaskWithEmptyName()
        {
            var task = Task.Create(string.Empty);

            task.IsSuccess.Should().BeFalse();
        }
    }
}

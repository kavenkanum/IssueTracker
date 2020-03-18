using CSharpFunctionalExtensions;
using FluentAssertions;
using IssueTracker.Commands;
using IssueTracker.Domain;
using IssueTracker.Domain.Entities;
using IssueTracker.Domain.Repositories;
using Moq;
using System.Threading;
using Xunit;

namespace IssueTracker.UnitTests.Projects
{

    public class TaskTests
    {
        [Fact]
        public void ShouldCreateTask()
        {
            var task = Job.Create("Some task");

            task.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void ShouldAddTaskToProject()
        {
            var project = Project.Create("Some project");
            var task = Job.Create("Some task");
            project.Value.AddJob(task.Value);

            task.IsSuccess.Should().BeTrue();
            project.IsSuccess.Should().BeTrue();
            project.Value.Jobs.Count.Should().Be(1);
        }

        [Fact]
        public void ShoultAddTaskToProjectCommand()
        {
            //var projectRepositoryMock = new Mock<IProjectRepository>();
            //var project = Project.Create("Some project");
            //projectRepositoryMock.Setup(r => r.GetAsync(It.IsAny<int>()))
            //    .Returns(() => System.Threading.Tasks.Task.FromResult(Maybe<Project>.From(project.Value)));

      
            //var handler = new CreateJobCommandHandler(projectRepositoryMock.Object);

            //var createTask = new CreateJobCommand(1, "dupa");
            //var task = handler.Handle(createTask, default);

            //task.Result.IsSuccess.Should().BeTrue();
            //project.Value.Jobs.Count.Should().Be(1);
        }

        [Fact]
        public void ShouldNotCreateTaskWithEmptyName()
        {
            var task = Job.Create(string.Empty);

            task.IsSuccess.Should().BeFalse();
        }
    }
}

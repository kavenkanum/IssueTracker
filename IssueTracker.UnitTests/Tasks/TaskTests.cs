using CSharpFunctionalExtensions;
using FluentAssertions;
using IssueTracker.Commands;
using IssueTracker.Domain;
using IssueTracker.Domain.Entities;
using IssueTracker.Domain.Language;
using IssueTracker.Domain.Language.ValueObjects;
using IssueTracker.Domain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace IssueTracker.UnitTests.Projects
{

    public class TaskTests
    {
        [Fact]
        public void ShouldCreateTask()
        {
            var dateOfCreate = DateTime.Now;
            var task = Job.Create("Some task", dateOfCreate);

            task.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void ShouldAddTaskToProject()
        {
            var dateOfCreate = DateTime.Now;
            var project = Project.Create("Some project");
            var task = Job.Create("Some task", dateOfCreate);
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
            var dateOfCreate = DateTime.Now;
            var task = Job.Create(string.Empty, dateOfCreate);

            task.IsSuccess.Should().BeFalse();
        }
        [Fact]
        public void ShouldBeNewStatusInNewJob()
        {
            var currentDate = DateTime.Now;
            var job = Job.Create("Test Job", currentDate);
            var expectedStatus = Status.New;
            job.Value.Status.Should().Be(expectedStatus);
        }

        [Fact]
        public void ShouldChangeDeadlineInJob()
        {
            var currentDate = DateTime.Now;
            var newDeadline = DateTime.Now.AddDays(10);

            var deadline = Deadline.CreateOptional(newDeadline, currentDate);

            var job = Job.Create("Test Job", currentDate);
            var changedDeadline = job.Value.ChangeDeadline(deadline.Value.Value);
            changedDeadline.IsSuccess.Should().BeTrue();
            job.Value.Deadline.Should().Be(deadline.Value.Value);
        }

        [Fact]
        public void ShouldStartJob()
        {
            var currentDate = DateTime.Now;
            var job = Job.Create("Test Job", currentDate);
            job.Value.StartJob();
            var expectedStatus = Status.InProgress;
            job.Value.Status.Should().Be(expectedStatus);
        }

        [Fact]
        public void ShouldFinishJob()
        {
            var currentDate = DateTime.Now;
            var job = Job.Create("Test Job", currentDate);
            job.Value.StartJob();
            job.Value.FinishJob();
            var expectedStatus = Status.Done;
            job.Value.Status.Should().Be(expectedStatus);
        }
        [Fact]
        public void ShouldNotFinishJob()
        {
            var currentDate = DateTime.Now;
            var job = Job.Create("Test Job", currentDate);
            var result = job.Value.FinishJob();
            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void ShouldEditJobWithNullInputProperties()
        {
            var currentDate = DateTime.Now;
            var job = Job.Create("Test Job", currentDate);
            var name = "New name";
            var description = "some description of the task";
            var deadline = Deadline.CreateOptional(DateTime.Now.AddDays(10), currentDate);
            long userId = default;
            Priority priority = default;
            var result = job.Value.EditProperties(name, description, deadline.Value, userId, priority);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void ShouldNotEditJobWithEmptyName()
        {
            var currentDate = DateTime.Now;
            var job = Job.Create("Test Job", currentDate);
            var name = "";
            var description = "some description of the task";
            var deadline = Deadline.CreateOptional(DateTime.Now.AddDays(10), currentDate);
            long userId = default;
            Priority priority = default;

            var result = job.Value.EditProperties(name, description, deadline.Value, userId, priority);

            result.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void ShouldNotSetDeadline()
        {
            var currentDate = DateTime.Now;
            var newDeadline = DateTime.Now.AddDays(-1);

            var deadline = Deadline.CreateOptional(newDeadline, currentDate);

            deadline.IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void ShouldNotSetDeadlineAtTheSameDay()
        {
            var currentDate = DateTime.Now;
            var newDeadline = DateTime.Now;

            var deadline = Deadline.CreateOptional(newDeadline, currentDate);

            deadline.IsSuccess.Should().BeFalse();
        }
        [Fact]
        public void ShouldSetDeadline()
        {
            var currentDate = DateTime.Now;
            var newDeadline = DateTime.Now.AddDays(+1);

            var deadline = Deadline.CreateOptional(newDeadline, currentDate);

            deadline.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void ShouldCreatePrevJobs()
        {
            var currentDate = DateTime.Now;
            var job = Job.Create("Test Job", currentDate);
            var prevJobsId = new List<int>() { 1, 2 };
            var prevJobs = new List<StartsAfterJob>();

            foreach (var prevJobId in prevJobsId)
            {
                prevJobs.Add(StartsAfterJob.Create(prevJobId).Value);
            }
            
            prevJobs.Count.Should().Be(2);
        }

        [Fact]
        public void ShouldAddPrevJobsToJob()
        {
            var currentDate = DateTime.Now;
            var job = Job.Create("Test Job", currentDate);
            var prevJobsId = new List<int>() { 1, 2 };
            var prevJobs = new List<StartsAfterJob>();

            foreach (var prevJobId in prevJobsId)
            {
                prevJobs.Add(StartsAfterJob.Create(prevJobId).Value);
            }
            job.Value.AddPreviousJobs(prevJobs);

            job.Value.StartsAfterJobs.Count.Should().Be(2);
        }

        [Fact]
        public void CheckPrevJobsShouldSuccess()
        {
            var currentDate = DateTime.Now;
            var job1 = Job.Create("Test Job", currentDate);

            var listOfJobs = new List<Job>();
            listOfJobs.Add(job1.Value);

            var job2 = Job.Create("Test Job2", currentDate);
            var prevJobsId = new List<int>() { 11, 21 };
            var prevJobs = new List<StartsAfterJob>();

            foreach (var prevJobId in prevJobsId)
            {
                prevJobs.Add(StartsAfterJob.Create(prevJobId).Value);
            }
            job1.Value.AddPreviousJobs(prevJobs);

            var newPrevJobsId = new List<int>() { 31, 41 };
            var jobsQueue = new List<int>();
            var failureList = new List<int>();
            jobsQueue.Add(job2.Value.Id);

            var result = job2.Value.CheckPrevJobs(newPrevJobsId, jobsQueue, listOfJobs, failureList);
            result.Count.Should().Be(0);
        }


        [Fact]
        public void CheckPrevJobsShouldFail()
        {
            var currentDate = DateTime.Now;
            var job1 = Job.Create("Test Job", currentDate);

            var listOfJobs = new List<Job>();
            

            var job2 = Job.Create("Test Job2", currentDate);
            var prevJobsId = new List<int>() { 11, 21 };
            var prevJobs = new List<StartsAfterJob>();

            foreach (var prevJobId in prevJobsId)
            {
                prevJobs.Add(StartsAfterJob.Create(prevJobId).Value);
            }
            job1.Value.AddPreviousJobs(prevJobs);
            listOfJobs.Add(job1.Value);
            var newPrevJobsId = new List<int>() { 0, 11, 41 };
            var jobsQueue = new List<int>();
            var failureList = new List<int>();
            jobsQueue.Add(job2.Value.Id);

            var result = job2.Value.CheckPrevJobs(newPrevJobsId, jobsQueue, listOfJobs, failureList);
            result.Count.Should().BeGreaterThan(0);
        }
    }
}

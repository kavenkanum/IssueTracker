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
using System.Security.Claims;
using System.Threading;
using Xunit;

namespace IssueTracker.UnitTests.Projects
{

    public class JobTests
    {
        #region Create job
        [Fact]
        public void ShouldCreateJob()
        {
            var dateOfCreate = DateTime.Now;
            var job = Job.Create("Some task", dateOfCreate);

            job.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void ShouldNotCreateJobWithEmptyName()
        {
            var dateOfCreate = DateTime.Now;
            var job = Job.Create(string.Empty, dateOfCreate);

            job.IsSuccess.Should().BeFalse();
        }
        #endregion

        [Fact]
        public void ShouldAddJobToProject()
        {
            var dateOfCreate = DateTime.Now;
            var project = Project.Create("Some project");
            var job = Job.Create("Some task", dateOfCreate);
            project.Value.AddJob(job.Value);

            job.IsSuccess.Should().BeTrue();
            project.IsSuccess.Should().BeTrue();
            project.Value.Jobs.Count.Should().Be(1);
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

        #region StartJob method
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
        public void ShouldNotStartJobWhenInProgressStatus()
        {
            var currentDate = DateTime.Now;
            var job = Job.Create("Test Job", currentDate);
            job.Value.StartJob();
            //we have inprogress status now, we should not change it in progress again
            job.Value.StartJob().IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void ShouldNotStartJobWhenInDoneStatus()
        {
            var currentDate = DateTime.Now;
            var job = Job.Create("Test Job", currentDate);
            job.Value.StartJob();
            job.Value.FinishJob();
            //we have done status now, we should not change it in progress 
            job.Value.StartJob().IsSuccess.Should().BeFalse();
        }
        #endregion

        #region HinishJob method
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
        #endregion

        #region EditJob method
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
        #endregion

        #region DeleteJob method
        [Fact]
        public void ShouldDeleteJob()
        {
            //creating current user with role admin
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Bob"),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.NameIdentifier, "12345")
            };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var user = new CurrentUser(claimsPrincipal);

            //create a new job\var currentDate = DateTime.Now;
            var currentDate = DateTime.Now;
            var job = Job.Create("Test Job", currentDate);

            job.Value.Delete(user).IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void ShouldNotDeleteJob()
        {
            //creating current user with role user (has no permission to delete job)
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "Bob"),
                new Claim(ClaimTypes.Role, "user"),
                new Claim(ClaimTypes.NameIdentifier, "12345")
            };
            var identity = new ClaimsIdentity(claims);
            var claimsPrincipal = new ClaimsPrincipal(identity);
            var user = new CurrentUser(claimsPrincipal);

            //create a new job\var currentDate = DateTime.Now;
            var currentDate = DateTime.Now;
            var job = Job.Create("Test Job", currentDate);

            job.Value.Delete(user).IsSuccess.Should().BeFalse();
        }
        #endregion

        [Fact]
        public void ShouldAddComment()
        {
            var currentTime = DateTime.Now;
            var comment = Comment.Create("Some description", 5, 123, currentTime);
            var job = Job.Create("Test Job", currentTime);
            job.Value.AddComment(comment.Value);

            job.Value.Comments.Count.Should().Be(1);
        }

        #region ChangeName method
        [Fact]
        public void ShouldChangeNameInNewStatus()
        {
            var currentTime = DateTime.Now;
            var job = Job.Create("Test Job", currentTime);

            job.Value.ChangeName("new name").IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void ShouldNotChangeNameInProgressStatus()
        {
            var currentTime = DateTime.Now;
            var job = Job.Create("Test Job", currentTime);
            job.Value.StartJob();

            job.Value.ChangeName("new name").IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void ShouldNotChangeNameInDoneStatus()
        {
            var currentTime = DateTime.Now;
            var job = Job.Create("Test Job", currentTime);
            job.Value.StartJob();
            job.Value.FinishJob();

            job.Value.ChangeName("new name").IsSuccess.Should().BeFalse();
        }

        [Fact]
        public void ShouldNotChangeNameToEmptyString()
        {
            var currentTime = DateTime.Now;
            var job = Job.Create("Test Job", currentTime);

            job.Value.ChangeName("").IsSuccess.Should().BeFalse();
        }
        #endregion

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
            var listOfJobs = new List<Job>();
            var prevJobsId = new List<int>() { 1, 2 };

            job.Value.AddPreviousJobs(job.Value, prevJobsId, listOfJobs);

            job.Value.StartsAfterJobs.Count.Should().Be(2);
        }

        [Fact]
        public void ShouldNotAddPrevJobsToJob()
        {
            var currentDate = DateTime.Now;
            var job = Job.Create("Test Job", currentDate);
            var listOfJobs = new List<Job>();
            var prevJobsId = new List<int>() { 0, 2 };

            var result = job.Value.AddPreviousJobs(job.Value, prevJobsId, listOfJobs);

            result.IsSuccess.Should().BeFalse();
        }

    }
}

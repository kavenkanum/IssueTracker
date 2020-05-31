using CSharpFunctionalExtensions;
using IssueTracker.Domain.Language;
using IssueTracker.Domain.Language.ValueObjects;
using Stateless;
using Stateless.Graph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IssueTracker.Domain.Entities
{
    public class Job
    {
        private readonly StateMachine<Status, Trigger> _machine;

        private Job(string name, DateTime dateOfCreate, Status status) : this()
        {
            Name = name;
            DateOfCreate = dateOfCreate;
            Status = status;
            JobDeleted = false;
            Comments = new List<Comment>();
            StartsAfterJobs = new List<StartsAfterJob>();
        }
        public Job()
        {
            //configuration of the state machine

            _machine = new StateMachine<Status, Trigger>(() => Status, s => Status = s);

            _machine.Configure(Status.New)
                .PermitReentry(Trigger.ChangeDeadline)
                .PermitReentry(Trigger.EditProperties)
                .PermitReentry(Trigger.ChangeName)
                .PermitReentry(Trigger.ChangeDescription)
                .PermitReentry(Trigger.ChangeAssignedUser)
                .PermitReentry(Trigger.ChangePriority)
                .Permit(Trigger.StartJob, Status.InProgress);

            _machine.Configure(Status.InProgress)
                .PermitReentry(Trigger.ChangeDeadline)
                .PermitReentry(Trigger.ChangePriority)
                .Permit(Trigger.FinishJob, Status.Done);
        }

        public int Id { get; private set; }
        public int ProjectId { get; private set; }
        public string Name { get; private set; }
        public DateTime DateOfCreate { get; private set; }
        public Deadline Deadline { get; private set; }
        public string Description { get; private set; }
        public Status Status { get; private set; }
        public long AssignedUserId { get; private set; }
        public Priority Priority { get; private set; }
        public List<Comment> Comments { get; set; }
        public List<StartsAfterJob> StartsAfterJobs { get; set; }
        public bool JobDeleted { get; set; }
        public long CreatorId { get; set; }

        public static Result<Job> Create(string name, DateTime dateOfCreate)
        {
            return Result.Create(!string.IsNullOrEmpty(name), "Task name cannot be empty")
                .OnSuccess(() => new Job(name, dateOfCreate, Status.New));
        }

        public Result EditProperties(string name, string description, Maybe<Deadline> deadline, long assignedUserId, Priority priority)
        {
            if (!_machine.CanFire(Trigger.EditProperties))
                return Result.Fail("Unable to edit properties in that state (In progress / Done).");

            if (string.IsNullOrEmpty(name))
                return Result.Fail("Task name cannot be empty.");

            Name = name;
            Description = description;
            Deadline = deadline.HasValue ? deadline.Value : null;
            AssignedUserId = assignedUserId;
            Priority = priority;

            _machine.Fire(Trigger.EditProperties);
            return Result.Ok();
        }

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        public Result ChangeName(string newName)
        {
            if (!_machine.CanFire(Trigger.ChangeName))
                return Result.Fail("Unable to change name in that state (In progress / Done).");

            if (string.IsNullOrEmpty(newName))
                return Result.Fail("Task name cannot be empty.");

            Name = newName;
            _machine.Fire(Trigger.ChangeName);
            return Result.Ok();
        }

        public Result StartJob()
        {
            if (!_machine.CanFire(Trigger.StartJob))
                return Result.Fail("Unable to start job");

            _machine.Fire(Trigger.StartJob);
            return Result.Ok();
        }

        public Result FinishJob()
        {
            if (!_machine.CanFire(Trigger.FinishJob))
                return Result.Fail("Unable to finish job");

            _machine.Fire(Trigger.FinishJob);
            return Result.Ok();
        }
        public Result ChangeDeadline(Deadline newDeadline)
        {
            if (!_machine.CanFire(Trigger.ChangeDeadline))
                return Result.Fail("Unable to change deadline");

            Deadline = newDeadline;
            _machine.Fire(Trigger.ChangeDeadline);
            return Result.Ok();
        }
        public Result ChangeDescription(string newDescription)
        {
            if (!_machine.CanFire(Trigger.ChangeDescription))
                return Result.Fail("Unable to change description in that state (In progress / Done)");

            Description = newDescription;
            _machine.Fire(Trigger.ChangeDescription);
            return Result.Ok();
        }
        public Result ChangeAssignedUser(long newAssignedUserId)
        {
            if (!_machine.CanFire(Trigger.ChangeAssignedUser))
                return Result.Fail("Unable to change assigned user in that state (In progress / Done)");

            AssignedUserId = newAssignedUserId;
            _machine.Fire(Trigger.ChangeAssignedUser);
            return Result.Ok();
        }
        public Result ChangePriority(Priority newPriority)
        {
            if (!_machine.CanFire(Trigger.ChangePriority))
                return Result.Fail("Unable to change assigned user in that state (Done)");

            Priority = newPriority;
            _machine.Fire(Trigger.ChangePriority);
            return Result.Ok();
        }

        public Result Delete(CurrentUser user)
        {
            if (!user.HasPermission(Permission.DeleteJob))
                return Result.Fail("No authorization to delete job");
            JobDeleted = true;
            return Result.Ok();
        }

        public void AddPreviousJobs(List<StartsAfterJob> startsAfterJobs)
        {
            foreach (var startsAfterJob in startsAfterJobs)
            {
                StartsAfterJobs.Add(startsAfterJob);
            }
        }

        public List<int> CheckPrevJobs(List<int> startsAfter, List<int> jobsQueue, List<Job> allJobsWithPrevJobs, List<int> failureList)
        {
            //any fail will be added to the failureList -> after end of recuperation if there is anything in that list it means that we have infinite loop. 
            //List also will contain job/jobs id which cause that loop.
            foreach (var prevJobId in startsAfter)
            {
                if (!jobsQueue.Contains(prevJobId))
                {
                    jobsQueue.Add(prevJobId);
                    var prevJob = allJobsWithPrevJobs.FirstOrDefault(j => j.Id == prevJobId);

                    if (prevJob != default)
                    {
                        CheckPrevJobs(prevJob.StartsAfterJobs.Select(j => j.StartsAfterJobId).ToList(), jobsQueue, allJobsWithPrevJobs, failureList);
                    }
                }
                else
                {
                    failureList.Add(prevJobId);
                }
            }

            return failureList;
        }
    }
}

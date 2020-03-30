﻿using CSharpFunctionalExtensions;
using IssueTracker.Domain.Language;
using IssueTracker.Domain.Language.ValueObjects;
using System;
using System.Collections.Generic;

namespace IssueTracker.Domain.Entities
{
    public class Job
    {
        private Job(string name, DateTime dateOfCreate, Status status)
        {
            Name = name;
            DateOfCreate = dateOfCreate;
            Status = status;
            Comments = new List<Comment>();
        }

        public int Id { get; private set; }
        public int ProjectId { get; private set; }
        public string Name { get; private set; }
        public DateTime DateOfCreate { get; private set; }
        public Deadline Deadline { get; private set; }
        public string Description { get; private set; }
        public Status Status { get; private set; }
        public Guid AssignedUserId { get; private set; }
        public Priority Priority { get; private set; }
        public List<Comment> Comments { get; set; }
        public int StartsAfterJobId { get; set; }

        
        public static Result<Job> Create(string name, DateTime dateOfCreate)
        {
            return Result.Create(!string.IsNullOrEmpty(name), "Task name cannot be empty")
                .OnSuccess(() => new Job(name, dateOfCreate, Status.New));
        }

        public void EditProperties(string description, Deadline deadline, Guid assignedUserId, Priority priority)
        {
            Description = description;
            Deadline = deadline;
            AssignedUserId = assignedUserId;
            Priority = priority;
        }

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        public void ChangeName(string newName)
        {
            Name = newName;
        }

        public void ChangeStatus(Status newStatus)
        {
            Status = newStatus;
        }
        public Result ChangeDeadline(Deadline newDeadline)
        {
            Deadline = newDeadline;
            return Result.Ok();
        }
        public void ChangeDescription(string newDescription)
        {
            Description = newDescription;
        }
        public void ChangeAssignedUser(Guid newAssignedUserId)
        {
            AssignedUserId = newAssignedUserId;
        }
        public void ChangePriority(Priority newPriority)
        {
            Priority = newPriority;
        }

    }
}

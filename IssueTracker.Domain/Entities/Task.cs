using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;

namespace IssueTracker.Domain.Entities
{
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public enum Status
    {
        New,
        InProgress,
        Done
    }

    public class Task
    {
        private Task(string name)
        {
            Name = name;
        }

        public int Id { get; private set; }
        public int ProjectId { get; private set; }
        public string Name { get; private set; }
        public DateTime DateOfCreate { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public int AssignedUserId { get; set; }
        public Priority Priority { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public int StartsAfterTaskId { get; set; }

        
        public static Result<Task> Create(string name)
        {
            return Result.Create(!string.IsNullOrWhiteSpace(name), "Task name cannot be empty")
                .OnSuccess(() => new Task(name));
        }
    }
}

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
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfCreate { get; set; }
        public DateTime Deadline { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public int AssignedUserId { get; set; }
        public Priority Priority { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public int StartsAfterTaskId { get; set; }

    }
}

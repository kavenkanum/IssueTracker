using IssueTracker.Domain.Entities;
using System.Collections.Generic;

namespace IssueTracker.Domain
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Task> Tasks { get; set; }
    }
}

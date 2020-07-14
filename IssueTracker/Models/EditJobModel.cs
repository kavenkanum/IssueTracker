using IssueTracker.Domain.Language;
using IssueTracker.Domain.Language.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Models
{
    public class EditJobModel
    {
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long AssignedUserId { get; set; }
        public string Deadline { get; set; }
        public int Priority { get; set; }
    }
}

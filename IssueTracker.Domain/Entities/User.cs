using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace IssueTracker.Domain.Entities
{
    public class User 
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<Job> AssignedTasks { get; set; }
    }
}

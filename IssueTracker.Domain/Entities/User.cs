using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace IssueTracker.Domain.Entities
{
    public class User : IdentityUser
    {
        public List<Job> AssignedTasks { get; set; }
    }
}

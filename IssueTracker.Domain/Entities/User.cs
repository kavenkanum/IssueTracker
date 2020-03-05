using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace IssueTracker.Domain.Entities
{
    public class User : IdentityUser
    {
        public virtual List<Task> AssignedTasks { get; set; }
    }
}

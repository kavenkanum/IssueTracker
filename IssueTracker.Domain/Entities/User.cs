using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Domain.Entities
{
    public class User : IdentityUser
    {
        public virtual List<Task> AssignedTasks { get; set; }
    }
}

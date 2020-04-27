using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace IssueTracker.Domain.Language
{
    public class CurrentUser
    {
        public static IReadOnlyDictionary<string, ICollection<Permission>> dictionary = new Dictionary<string, ICollection<Permission>>()
        {
            { "admin", new List<Permission>()
            {
                Permission.DeleteJob,
                Permission.AssignUsersToJob
            }},
            { "manager", new List<Permission>()
            {
                Permission.DeleteJob,
                Permission.AssignUsersToJob,
                Permission.AddJob
            } }
        };

        private readonly ClaimsPrincipal _user;
        private readonly IReadOnlyCollection<Permission> _permissions;

        public CurrentUser(ClaimsPrincipal user)
        {
            _user = user;
            _permissions = AssignPermissions(user);
            Id = GetId(user);
        }

        public long Id { get; }
        public bool HasPermission(Permission permission) => _permissions.Contains(permission);

        private IReadOnlyCollection<Permission> AssignPermissions(ClaimsPrincipal user)
        {
            var roles = user.Claims.Where(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Select(c => c.Value);
            return dictionary.Where(e => roles.Contains(e.Key)).SelectMany(kv => kv.Value).Distinct().ToList();
        }

        private long GetId(ClaimsPrincipal user)
        {
            var userId = user.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            var success = Int64.TryParse(userId, out long result);
            if (success)
            {
                return result;
            }
            else
            {
                throw new ArgumentException("Invalid userId value");
            }
        }
    }
}

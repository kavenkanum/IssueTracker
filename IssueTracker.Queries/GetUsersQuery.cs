using IssueTracker.Domain;
using IssueTracker.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Queries
{
    public class UserDto
    {
        public UserDto(long userId, string fullName)
        {
            UserId = userId;
            FullName = fullName;
        }
        public long UserId { get; set; }
        public string FullName { get; set; }
    }
    public class GetUsersFromProjectQuery : IRequest<ICollection<UserDto>>
    {
        public GetUsersFromProjectQuery(int projectId)
        {
            ProjectId = projectId;
        }
        public int ProjectId { get; set; }
    }
    public class GetUsersFromProjectQueryHandler : IRequestHandler<GetUsersFromProjectQuery, ICollection<UserDto>>
    {
        private readonly QueryDbContext _queryDbContext;
        public GetUsersFromProjectQueryHandler(QueryDbContext queryDbContext)
        {
            _queryDbContext = queryDbContext;
        }
        public Task<ICollection<UserDto>> Handle(GetUsersFromProjectQuery request, CancellationToken cancellationToken)
        {
            var selectedUsers = _queryDbContext.Jobs.Where(j => j.ProjectId == request.ProjectId && j.AssignedUserId != 0).Select(j => j.AssignedUserId).ToList();
            var usersResult = new List<UserDto>();
            foreach (var selectedUserId in selectedUsers)
            {
                usersResult.Add(_queryDbContext.Users.Where(u => u.Id == selectedUserId).Select(u => new UserDto(u.Id, u.FullName)).First());
            }
            return Task.FromResult(usersResult.Distinct(new UserEqualityComparer()).ToList() as ICollection<UserDto>);
        }
    }

    class UserEqualityComparer : IEqualityComparer<UserDto>
    {
        public bool Equals(UserDto x, UserDto y)
        {
            return x.UserId == y.UserId;
        }

        public int GetHashCode([DisallowNull] UserDto user)
        {
            return user.UserId.GetHashCode();
        }
    }
}

using IssueTracker.Domain.Language;
using IssueTracker.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Queries
{
    public class UserToJobAssignDto
    {
        public UserToJobAssignDto(Guid userId, string fullName)
        {
            UserId = userId;
            FullName = fullName;
        }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
    }
    public class GetUsersToJobAssignQuery : IRequest<ICollection<UserToJobAssignDto>>
    {

    }
    public class GetUsersToJobAssignQueryHandler : IRequestHandler<GetUsersToJobAssignQuery, ICollection<UserToJobAssignDto>>
    {
        private readonly QueryDbContext _queryDbContext;
        private readonly CurrentUser _currentUser;
        public GetUsersToJobAssignQueryHandler(QueryDbContext queryDbContext, CurrentUser currentUser)
        {
            _queryDbContext = queryDbContext;
            _currentUser = currentUser;
        }

        public Task<ICollection<UserToJobAssignDto>> Handle(GetUsersToJobAssignQuery request, CancellationToken cancellationToken)
        {
            if (_currentUser.HasPermission(Permission.AssignUsersToJob))
            {
                var users = _queryDbContext.Users.Select(u => new UserToJobAssignDto(u.Id, u.FullName)).ToList() as ICollection<UserToJobAssignDto>;
                return Task.FromResult(users);
            }
            return _queryDbContext.Users.Where(u => u.Id == _currentUser.Id).Select(u => new UserToJobAssignDto(u.Id, u.FullName)).ToList() as ICollection<UserToJobAssignDto>;
        }
    }
}

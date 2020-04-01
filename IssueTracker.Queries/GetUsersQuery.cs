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
    public class UserDto
    {
        public UserDto(Guid userId, string fullName)
        {
            UserId = userId;
            FullName = fullName;
        }
        public Guid UserId { get; set; }
        public string FullName { get; set; }
    }
    public class GetUsersQuery : IRequest<ICollection<UserDto>>
    {

    }
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, ICollection<UserDto>>
    {
        private readonly QueryDbContext _queryDbContext;
        public GetUsersQueryHandler(QueryDbContext queryDbContext)
        {
            _queryDbContext = queryDbContext;
        }
        public Task<ICollection<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = _queryDbContext.Users.Select(u => new UserDto(u.Id, u.FullName)).ToList() as ICollection<UserDto>;
            return Task.FromResult(users);
        }
    }
}

using IssueTracker.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace IssueTracker.Queries
{
    public class AssignedUserDto
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
    }
    public class GetAssignedUserQuery : IRequest<AssignedUserDto>
    {
        public GetAssignedUserQuery(Guid userId)
        {
            UserId = userId;
        }
        public Guid UserId { get; set; }
    }

    public class GetAssignedUserQueryHandler : IRequestHandler<GetAssignedUserQuery, AssignedUserDto>
    {
        private readonly QueryDbContext _queryDbContext;
        public GetAssignedUserQueryHandler(QueryDbContext queryDbContext)
        {
            _queryDbContext = queryDbContext;
        }

        public Task<AssignedUserDto> Handle(GetAssignedUserQuery request, CancellationToken cancellationToken)
        {
            var assignedUser = _queryDbContext.Users.FirstOrDefault(u => u.UserId == request.UserId);
            return Task.FromResult(new AssignedUserDto
            {
                UserId = assignedUser.UserId,
                FullName = assignedUser.FullName
            });
        }
    }
}

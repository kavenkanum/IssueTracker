using IssueTracker.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using CSharpFunctionalExtensions;
using IssueTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.Queries
{
    public class AssignedUserDto
    {
        public long UserId { get; set; }
        public string FullName { get; set; }
    }
    public class GetAssignedUserQuery : IRequest<Result<AssignedUserDto>>
    {
        public GetAssignedUserQuery(long userId)
        {
            UserId = userId;
        }
        public long UserId { get; set; }
    }

    public class GetAssignedUserQueryHandler : IRequestHandler<GetAssignedUserQuery, Result<AssignedUserDto>>
    {
        private readonly QueryDbContext _queryDbContext;
        public GetAssignedUserQueryHandler(QueryDbContext queryDbContext)
        {
            _queryDbContext = queryDbContext;
        }

        public async Task<Result<AssignedUserDto>> Handle(GetAssignedUserQuery request, CancellationToken cancellationToken)
        {
            Maybe<User> assignedUser = await _queryDbContext.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
            return assignedUser
                .ToResult($"Unable to find user with id {request.UserId}.")
                .OnSuccess(assignedUser => new AssignedUserDto
                {
                    UserId = assignedUser.Id,
                    FullName = assignedUser.FullName
                });
        }
    }
}

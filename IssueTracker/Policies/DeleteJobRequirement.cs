using CSharpFunctionalExtensions;
using IssueTracker.Domain.Entities;
using IssueTracker.Persistence;
using IssueTracker.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Policies
{
    public class DeleteJobRequirement : IAuthorizationRequirement
    {
        public int JobId { get; set; }
        public int ProjectId { get; set; }
    }

    public class DeleteJobRequirementHandler : AuthorizationHandler<DeleteJobRequirement>
    {
        //private readonly QueryDbContext _queryDbContext;
        private readonly IMediator _mediator;
        public DeleteJobRequirementHandler(IMediator mediator)
        {
            _mediator = mediator;
            //_queryDbContext = queryDbContext;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DeleteJobRequirement requirement)
        {
            var userId = context.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            var userRole = context.User.Claims.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
            var allowed = false;

            if (userRole == "admin" || userRole == "manager")
            {
                allowed = true;
            }
            else
            {
                var jobResult = await _mediator.Send(new GetJobQuery(requirement.JobId, requirement.ProjectId));

                //var jobResult = await _queryDbContext.Jobs.FirstOrDefaultAsync(j => j.Id == requirement.JobId);
                var userAssignToJobResult = jobResult.Value.AssignedUserID.ToString();
                if (userAssignToJobResult == userId)
                {
                    allowed = true;
                }
            }

            if (allowed)
            {
                context.Succeed(requirement);
            }
            
        }
    }
}

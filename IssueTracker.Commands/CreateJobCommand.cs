using CSharpFunctionalExtensions;
using IssueTracker.Domain.Entities;
using IssueTracker.Domain.Language;
using IssueTracker.Domain.Language.ValueObjects;
using IssueTracker.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Commands
{


    #region common


    public abstract class AuthorizationCommand<TCommand, TResult, TRequirement> : IRequestHandler<TCommand, TResult>
        where TRequirement : IAuthorizationRequirement
        where TCommand : IRequest<TResult>
    {
        private readonly IAuthorizationService _authorization;
        private readonly ClaimsPrincipal _user;

        protected AuthorizationCommand(IAuthorizationService authorization)
        {
            _authorization = authorization;
        }

        protected async Task<Result> IsAuthorized(TRequirement requirement)
        {
            var result = await _authorization.AuthorizeAsync(_user, null, requirement);
            return result.Succeeded ? Result.Ok() : Result.Fail("Unauthorized");
        }
        public abstract Task<TResult> Handle(TCommand request, CancellationToken cancellationToken);
    }
    #endregion

    public class DeleteJobRequirement : IAuthorizationRequirement
    {
        public int JobId { get; set; }
    }

    public class DeleteJobRequirementHandler : AuthorizationHandler<DeleteJobRequirement>
    {
        private readonly CurrentUser currentUser;

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DeleteJobRequirement requirement)
        {
            if (currentUser.HasPermission(Permission.DeleteJob));
            {
                context.Succeed(requirement);
            }
        }
    }

    public class CreateJobCommand : IRequest<Result>
    {
        public CreateJobCommand(int projectId, string name)
        {
            ProjectId = projectId;
            Name = name;
        }
        public int ProjectId { get; }
        public string Name { get; }

        public DeleteJobRequirement Requirement => new DeleteJobRequirement() { JobId = ProjectId };
    }

    public class CreateJobCommandHandler : AuthorizationCommand<CreateJobCommand, Result, DeleteJobRequirement>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IJobRepository _jobRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateJobCommandHandler(IProjectRepository projectRepository, IJobRepository jobRepository, IDateTimeProvider dateTimeProvider)
        {
            _projectRepository = projectRepository;
            _jobRepository = jobRepository;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<Result> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            aggregateRepository.Get(7)
                .OnSuccess(aggregate => aggregate.Delete(User));


            Delete(CurrentUser user){
                if (user.HasPermission("DeleteJob") || Creator = user)
                    AggregateDeleted = true;

                else
                    "Not authorized";
            }

            return await IsAuthorized(new DeleteJobRequirement() { JobId = request.JobId })
                .OnSuccess(async () =>
                {
                    var jobResult = Job.Create(request.Name, _dateTimeProvider.GetCurrentDate());
                    var projectResult = await _projectRepository.GetAsync(request.ProjectId)
                        .ToResult($"Unable to find project with id: {request.ProjectId}");

                    return await Result.Combine(jobResult, projectResult)
                        .OnSuccess(() => projectResult.Value.AddJob(jobResult.Value))
                        .OnSuccess(() => _jobRepository.SaveAsync(jobResult.Value));
                });
        }

        public override Task<Result> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

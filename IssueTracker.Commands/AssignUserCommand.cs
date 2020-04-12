using CSharpFunctionalExtensions;
using IssueTracker.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Commands
{
    public class AssignUserCommand : IRequest<Result>
    {
        public AssignUserCommand(int jobId, Guid userId)
        {
            JobId = jobId;
            UserId = userId;
        }
        public int JobId { get; set; }
        public Guid UserId { get; set; }
    }

    public class AssignUserCommandHandler : IRequestHandler<AssignUserCommand, Result>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IUserRepository _userRepository;
        public AssignUserCommandHandler(IJobRepository jobRepository, IUserRepository userRepository)
        {
            _jobRepository = jobRepository;
            _userRepository = userRepository;
        }
        public async Task<Result> Handle(AssignUserCommand request, CancellationToken cancellationToken)
        {
            var jobResult = await _jobRepository.GetAsync(request.JobId)
                .ToResult($"No job found with id: {request.JobId}.");
            var userResult = await _userRepository.GetAsync(request.UserId)
                .ToResult($"No user found with id: {request.UserId}.");

            return Result.Combine(jobResult, userResult)
                .OnSuccess(() => jobResult.Value.ChangeAssignedUser(request.UserId))
                .OnSuccess(() => userResult.Value.AddJob(jobResult.Value));
        }
    }
}

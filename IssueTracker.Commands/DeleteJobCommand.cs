using CSharpFunctionalExtensions;
using IssueTracker.Domain.Entities;
using IssueTracker.Domain.Language;
using IssueTracker.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Commands
{
    public class DeleteJobCommand : IRequest<Result>
    {
        public DeleteJobCommand(int jobId)
        {
            JobId = jobId;
        }
        public int JobId { get; set; }
    }

    public class DeleteJobCommandHandler : IRequestHandler<DeleteJobCommand, Result>
    {
        private readonly IJobRepository _jobRepository;
        private readonly CurrentUser _currentUser;
        public DeleteJobCommandHandler(IJobRepository jobRepository, CurrentUser currentUser)
        {
            _jobRepository = jobRepository;
            _currentUser = currentUser;
        }
        public async Task<Result> Handle(DeleteJobCommand request, CancellationToken cancellationToken)
        {
            var jobResult = await _jobRepository.GetAsync(request.JobId)
                .ToResult($"Unable to find job with id: {request.JobId}");

            return await jobResult.OnSuccess(job => job.Delete(_currentUser))
                .OnSuccess(() => _jobRepository.SaveAsync(jobResult.Value));
        }
    }
}

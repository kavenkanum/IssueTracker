using CSharpFunctionalExtensions;
using IssueTracker.Domain.Entities;
using IssueTracker.Domain.Language.ValueObjects;
using IssueTracker.Domain.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Commands
{
    public class CreateJobCommand : IRequest<Result>
    {
        public CreateJobCommand(int projectId, string name)
        {
            ProjectId = projectId;
            Name = name;
        }
        public int ProjectId { get; }
        public string Name { get; }
    }

    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Result>
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
            var jobResult = Job.Create(request.Name, _dateTimeProvider.GetCurrentDate());
            var projectResult = await _projectRepository.GetAsync(request.ProjectId)
                .ToResult($"Unable to find project with id: {request.ProjectId}");

            return await Result.Combine(jobResult, projectResult)
                .OnSuccess(() => projectResult.Value.AddJob(jobResult.Value))
                .OnSuccess(() => _jobRepository.SaveAsync(jobResult.Value));
        }
    }
}

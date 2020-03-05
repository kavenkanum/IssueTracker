using CSharpFunctionalExtensions;
using IssueTracker.Domain.Entities;
using IssueTracker.Domain.Repositories;
using MediatR;
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
            //DateOfCreate
        }
        public int ProjectId { get; }
        public string Name { get; }
        //DateOfCreate
    }

    public class CreateJobCommandHandler : IRequestHandler<CreateJobCommand, Result>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IJobRepository _jobRepository;

        public CreateJobCommandHandler(IProjectRepository projectRepository, IJobRepository jobRepository)
        {
            _projectRepository = projectRepository;
            _jobRepository = jobRepository;
        }

        public async Task<Result> Handle(CreateJobCommand request, CancellationToken cancellationToken)
        {
            var jobResult = Job.Create(request.Name);
            var projectResult = await _projectRepository.GetAsync(request.ProjectId)
                .ToResult($"Unable to find project with id: {request.ProjectId}");

            return await Result.Combine(jobResult, projectResult)
                .OnSuccess(() => projectResult.Value.AddJob(jobResult.Value))
                .OnSuccess(() => _jobRepository.SaveAsync(jobResult.Value));
            //return await combinedJobProjectResult.OnSuccess(() => _jobRepository.SaveAsync(jobResult.Value));
        }
    }
}

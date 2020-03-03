using CSharpFunctionalExtensions;
using IssueTracker.Domain.Repositories;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Commands
{
    public class CreateTaskCommand : IRequest<Result>
    {
        //
        public int ProjectId { get; }
        public string Name { get; }
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Result>
    {
        private readonly IProjectRepository projectRepository;

        public async Task<Result> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var taskResult = Domain.Entities.Task.Create(request.Name);
            var projectResult = await projectRepository.GetAsync(request.ProjectId)
                .ToResult($"Unable to find project with id: {request.ProjectId}");

            return Result.Combine(taskResult, projectResult)
                .OnSuccess(() => projectResult.Value.AddTask(taskResult.Value));
        }
    }
}

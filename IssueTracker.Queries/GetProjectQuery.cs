using CSharpFunctionalExtensions;
using IssueTracker.Domain;
using IssueTracker.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Queries
{
    public class GetProjectQuery : IRequest<Result<ProjectDto>>
    {
        public GetProjectQuery(int projectId)
        {
            ProjectId = projectId;
        }
        public int ProjectId { get; set; }
    }

    public class GetProjectQueryHandler : IRequestHandler<GetProjectQuery, Result<ProjectDto>>
    {
        private readonly IProjectRepository _projectRepository;
        public GetProjectQueryHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<Result<ProjectDto>> Handle(GetProjectQuery request, CancellationToken cancellationToken)
        {
            Maybe<Project> project = await _projectRepository.GetAsync(request.ProjectId);

            return project.ToResult($"Unable to find project with id: {request.ProjectId}.")
                .OnSuccess(project => new ProjectDto(project.Name, project.Id));
        }
    }
}

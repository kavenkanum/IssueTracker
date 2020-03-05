using IssueTracker.Domain;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Queries
{
    public class ProjectDto
    {
        public int Id { get; }
        public string Name { get; }
    }
    public class GetListOfProjectsQuery : IRequest<ICollection<ProjectDto>>
    {
    }

    public class GetListOfProjectsQueryHandler : IRequestHandler<GetListOfProjectsQuery, ICollection<ProjectDto>>
    {
        private readonly IssueTrackerDbContext _issueTrackerDbContext;
        public Task<ICollection<ProjectDto>> Handle(GetListOfProjectsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_issueTrackerDbContext.Projects.Select(p => new ProjectDto()).ToList() as ICollection<ProjectDto>);
        }
    }
}

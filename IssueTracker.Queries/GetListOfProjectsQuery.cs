using IssueTracker.Domain;
using IssueTracker.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Queries
{
    public class ProjectDto
    {
        public ProjectDto(string name, int id)
        {
            Name = name;
            Id = id;
        }
        public int Id { get; }
        public string Name { get; }
    }
    public class GetListOfProjectsQuery : IRequest<ICollection<ProjectDto>>
    {
    }

    public class GetListOfProjectsQueryHandler : IRequestHandler<GetListOfProjectsQuery, ICollection<ProjectDto>>
    {
        private readonly QueryDbContext _queryDbContext;
        public GetListOfProjectsQueryHandler(QueryDbContext queryDbContext)
        {
            _queryDbContext = queryDbContext;
        }

        public Task<ICollection<ProjectDto>> Handle(GetListOfProjectsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_queryDbContext.Projects.Select(p => new ProjectDto(p.Name, p.Id)).ToList() as ICollection<ProjectDto>);
        }
    }
}

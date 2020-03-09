using CSharpFunctionalExtensions;
using IssueTracker.Domain.Language;
using IssueTracker.Domain.Repositories;
using IssueTracker.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Queries
{
    public class ProjectJobDto
    {
        public int JobId { get; set; }
        public string Name { get; set; }
        public string AssignedUserId { get; set; }
        public Status Status { get; set; }
        public DateTime Deadline { get; set; }
    }
    public class GetListOfProjectJobsQuery : IRequest<ICollection<ProjectJobDto>>
    {
        public GetListOfProjectJobsQuery(int projectId)
        {
            ProjectId = projectId;
        }
        public int ProjectId { get; set; }
    }
    public class GetListOfProjectJobsQueryHandler : IRequestHandler<GetListOfProjectJobsQuery, ICollection<ProjectJobDto>>
    {
        private readonly QueryDbContext _queryDbContext;
        public GetListOfProjectJobsQueryHandler(QueryDbContext queryDbContext)
        {
            _queryDbContext = queryDbContext;
        }

        public Task<ICollection<ProjectJobDto>> Handle(GetListOfProjectJobsQuery request, CancellationToken cancellationToken)
        {
            var jobs = (_queryDbContext.Jobs.Where(j => j.ProjectId == request.ProjectId)
                .Select(j => new ProjectJobDto()
                {
                    JobId = j.Id,
                    Name = j.Name
                })).ToList() as ICollection<ProjectJobDto>;
            return Task.FromResult(jobs);
        }
    }

}

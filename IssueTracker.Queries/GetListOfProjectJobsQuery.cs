using CSharpFunctionalExtensions;
using IssueTracker.Domain.Entities;
using IssueTracker.Domain.Language;
using IssueTracker.Domain.Language.ValueObjects;
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
        public long AssignedUserId { get; set; }
        public Status Status { get; set; }
        public Deadline Deadline { get; set; }
    }
    public class GetListOfProjectJobsQuery : IRequest<ICollection<ProjectJobDto>>
    {
        public GetListOfProjectJobsQuery(int projectId, Status status)
        {
            ProjectId = projectId;
            Status = status;
        }
        public int ProjectId { get; set; }
        public Status Status { get; set; }
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
            var jobsQuery = _queryDbContext.Jobs.Where(j => j.ProjectId == request.ProjectId);
            if (request.Status != Status.None)
            {
                jobsQuery = jobsQuery.Where(j => j.Status == request.Status);
            }

            var jobs = jobsQuery.Select(j => new ProjectJobDto()
            {
                JobId = j.Id,
                Name = j.Name,
                Status = j.Status,
                AssignedUserId = j.AssignedUserId,
                Deadline = j.Deadline
            }).ToList() as ICollection<ProjectJobDto>;

            return Task.FromResult(jobs);
        }
    }

}

using IssueTracker.Domain.Entities;
using IssueTracker.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Queries
{
    public class AvailablePrevJobDto
    {
        public AvailablePrevJobDto(int jobId, string name)
        {
            JobId = jobId;
            Name = name;
        }
        public int JobId { get; set; }
        public string Name { get; set; }
    }
    public class GetAvailablePrevJobsQuery : IRequest<ICollection<AvailablePrevJobDto>>
    {
        public GetAvailablePrevJobsQuery(int jobId)
        {
            JobId = jobId;
        }
        public int JobId { get; set; }
    }
    public class GetAvailablePrevJobsQueryHandler : IRequestHandler<GetAvailablePrevJobsQuery, ICollection<AvailablePrevJobDto>>
    {
        private readonly QueryDbContext _queryDbContext;
        public GetAvailablePrevJobsQueryHandler(QueryDbContext queryDbContext)
        {
            _queryDbContext = queryDbContext;
        }
        public Task<ICollection<AvailablePrevJobDto>> Handle(GetAvailablePrevJobsQuery request, CancellationToken cancellationToken)
        {
            var currentProjectId = _queryDbContext.Jobs.Where(j => j.Id == request.JobId).Select(j => j.ProjectId).FirstOrDefault();
            var allJobsForProject = _queryDbContext.Jobs.Include(j => j.StartsAfterJobs).Where(j => j.ProjectId == currentProjectId && j.Id != request.JobId).ToList();
            var jobsToCheck = new Queue<int>();
            jobsToCheck.Enqueue(request.JobId);
            while (jobsToCheck.Count > 0)
            {
                var jobToCheckId = jobsToCheck.Dequeue();
                foreach (var job in allJobsForProject)
                {
                    if (job.StartsAfterJobs.Where(saj => saj.StartsAfterJobId == jobToCheckId).Any())
                    {
                        allJobsForProject = allJobsForProject.Where(j => j.Id != job.Id).ToList();
                        jobsToCheck.Enqueue(job.Id);
                    }
                }
            }
            var availablePrevJobs = allJobsForProject.Select(j => new AvailablePrevJobDto(j.Id, j.Name)).ToList();

            return Task.FromResult(availablePrevJobs as ICollection<AvailablePrevJobDto>);
        }
    }
}

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
            IEnumerable<Job> newAllJobsForProject = new List<Job>();

            //we have to remove all previous jobs for current job - because if there is already some job as a prev one for current job - we cannot add the same job again
            var prevJobsForCurrentJob = _queryDbContext.Jobs.Where(j => j.Id == request.JobId).SelectMany(j => j.StartsAfterJobs);
            foreach (var prevJob in prevJobsForCurrentJob)
            {
                newAllJobsForProject = allJobsForProject.Where(j => j.Id != prevJob.StartsAfterJobId);
            }
            allJobsForProject = newAllJobsForProject.ToList();
            //we will be checking jobs from below queue, first we add current job to that list
            var jobsToCheck = new Queue<int>();
            jobsToCheck.Enqueue(request.JobId);
            while (jobsToCheck.Count > 0)
            {
                var jobToCheckId = jobsToCheck.Dequeue();
                foreach (var job in allJobsForProject)
                {
                    //we are checking if currently checked job is already in relation with others jobs (if we have to do that checked job before do another job)
                    if (job.StartsAfterJobs.Where(saj => saj.StartsAfterJobId == jobToCheckId).Any())
                    {
                        //if we find that kind of job we are removing it from list with all jobs 
                        newAllJobsForProject = allJobsForProject.Where(j => j.Id != job.Id);
                        //now we will check that removed job - if that removed job is in relation with another jobs
                        jobsToCheck.Enqueue(job.Id);
                    }
                }
            }
            //here we have already removed all jobs with relation to our current job - in allJobsForProject stays only jobs available to be marked as a previous jobs for our current job
            var availablePrevJobs = newAllJobsForProject.Select(j => new AvailablePrevJobDto(j.Id, j.Name)).ToList();

            return Task.FromResult(availablePrevJobs as ICollection<AvailablePrevJobDto>);
        }
    }
}

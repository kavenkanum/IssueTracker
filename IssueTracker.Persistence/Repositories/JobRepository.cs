using CSharpFunctionalExtensions;
using IssueTracker.Domain;
using IssueTracker.Domain.Entities;
using IssueTracker.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTracker.Persistence.Repositories
{
    public class JobRepository : IJobRepository
    {
        private readonly IssueTrackerDbContext _issueTrackerDbContext;
        public JobRepository(IssueTrackerDbContext issueTrackerDbContext)
        {
            _issueTrackerDbContext = issueTrackerDbContext;
        }
        public Result Delete(Job job)
        {
            _issueTrackerDbContext.Remove(job);
            return Result.Ok();
        }

        public async Task<Maybe<Job>> GetAsync(int jobId)
        {
            return await _issueTrackerDbContext.Jobs.Include(j => j.Comments).Include(j => j.StartsAfterJobs).FirstOrDefaultAsync(j => j.Id == jobId);
        }

        public async Task<List<Job>> GetManyAsync(int[] jobsId)
        {
            IQueryable<Job> jobsList = new List<Job>().AsQueryable();
            foreach (var jobId in jobsId)
            {
                jobsList = _issueTrackerDbContext.Jobs.Include(j => j.Comments).Include(j => j.StartsAfterJobs).Where(j => j.Id == jobId);
            }

            return await jobsList.ToListAsync();
        }

        public async Task<List<Job>> GetJobsWithPrevJobs(int projectId)
        {
            return await _issueTrackerDbContext.Jobs.Include(j => j.StartsAfterJobs).Where(j => j.StartsAfterJobs.Any() && j.ProjectId == projectId).ToListAsync();
        }

        public async Task<Result<int>> SaveAsync(Job job)
        {
            if (job.Id == default)
            {
                await _issueTrackerDbContext.AddAsync(job);
            }
            await _issueTrackerDbContext.SaveChangesAsync();
            return Result.Ok(job.Id);
        }
    }
}

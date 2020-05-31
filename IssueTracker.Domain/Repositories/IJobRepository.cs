

using CSharpFunctionalExtensions;
using IssueTracker.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IssueTracker.Domain.Repositories
{
    public interface IJobRepository
    {
        Task<Maybe<Job>> GetAsync(int jobId);
        Task<List<Job>> GetJobsWithPrevJobs(int projectId);
        Task<Result<int>> SaveAsync(Job job);
        Result Delete(Job job);

    }
}

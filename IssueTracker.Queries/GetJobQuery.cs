using CSharpFunctionalExtensions;
using IssueTracker.Domain.Entities;
using IssueTracker.Domain.Repositories;
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
    public class JobDto
    {
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AssignedUserID { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime DateOfCreate { get; set; }
    }
    public class GetJobQuery : IRequest<Result<JobDto>>
    {
        public GetJobQuery(int jobId)
        {
            JobId = jobId;
        }
        public int JobId { get; set; }
    }

    public class GetJobQueryHandler : IRequestHandler<GetJobQuery, Result<JobDto>>
    {
        private readonly QueryDbContext _queryDbContext;
        public GetJobQueryHandler(QueryDbContext queryDbContext)
        {
            _queryDbContext = queryDbContext;
        }
        public async Task<Result<JobDto>> Handle(GetJobQuery request, CancellationToken cancellationToken)
        {
            Maybe<Job> job = await _queryDbContext.Jobs.FirstOrDefaultAsync(j => j.Id == request.JobId);

            return job
                .ToResult($"Unable to find job with id {request.JobId}.")
                .OnSuccess(job => new JobDto()
                {
                    JobId = job.Id,
                    Name = job.Name,
                    Description = job.Description,
                    AssignedUserID = job.AssignedUserId,
                    Deadline = job.Deadline,
                    DateOfCreate = job.DateOfCreate
                });

        }
    }
}

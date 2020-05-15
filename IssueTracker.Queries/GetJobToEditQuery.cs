using CSharpFunctionalExtensions;
using IssueTracker.Domain.Entities;
using IssueTracker.Domain.Language;
using IssueTracker.Domain.Language.ValueObjects;
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
    public class JobToEditDto
    {
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long AssignedUserID { get; set; }
        public DateTime? Deadline { get; set; }
        public Priority Priority { get; set; }
    }
    public class GetJobToEditQuery : IRequest<Result<JobToEditDto>>
    {
        public GetJobToEditQuery(int jobId)
        {
            JobId = jobId;
        }
        public int JobId { get; set; }
    }

    public class GetJobToEditQueryHandler : IRequestHandler<GetJobToEditQuery, Result<JobToEditDto>>
    {
        private readonly QueryDbContext _queryDbContext;
        public GetJobToEditQueryHandler(QueryDbContext queryDbContext)
        {
            _queryDbContext = queryDbContext;
        }
        public async Task<Result<JobToEditDto>> Handle(GetJobToEditQuery request, CancellationToken cancellationToken)
        {
            Maybe<Job> job = await _queryDbContext.Jobs.FirstOrDefaultAsync(j => j.Id == request.JobId);

            return job
                .ToResult($"Unable to find job with id {request.JobId}.")
                .OnSuccess(job => new JobToEditDto()
                {
                    JobId = job.Id,
                    Name = job.Name,
                    Description = job.Description,
                    AssignedUserID = job.AssignedUserId,
                    Deadline = job.Deadline?.DeadlineDate,
                    Priority = job.Priority
                });
        }
    }
}

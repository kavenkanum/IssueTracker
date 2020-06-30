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
    public class JobDto
    {
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long AssignedUserID { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public DateTime DateOfCreate { get; set; }
        public Status Status { get; set; }
    }
    public class GetJobQuery : IRequest<Result<JobDto>>
    {
        public GetJobQuery(int jobId, int projectId)
        {
            JobId = jobId;
            ProjectId = projectId;
        }
        public int JobId { get; set; }
        public int ProjectId { get; set; }
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
            Maybe<Job> job = await _queryDbContext.Jobs.FirstOrDefaultAsync(j => j.Id == request.JobId && j.ProjectId == request.ProjectId);
             
            return job
                .ToResult($"Unable to find job with id {request.JobId} or project with id: {request.ProjectId} doesn't contain job with id: {request.JobId}.")
                .OnSuccess(job => new JobDto()
                {
                    JobId = job.Id,
                    Name = job.Name,
                    Description = job.Description,
                    AssignedUserID = job.AssignedUserId,
                    DeadlineDate = job.Deadline?.DeadlineDate,
                    DateOfCreate = job.DateOfCreate,
                    Status = job.Status
                });

        }
    }
}

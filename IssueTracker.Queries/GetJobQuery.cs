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
    public class JobDto
    {
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AssignedUserID { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime DateOfCreate { get; set; }
    }
    public class GetJobQuery : IRequest<JobDto>
    {
        public GetJobQuery(int jobId)
        {
            JobId = jobId;
        }
        public int JobId { get; set; }
    }

    public class GetJobQueryHandler : IRequestHandler<GetJobQuery, JobDto>
    {
        private readonly QueryDbContext _queryDbContext;
        public GetJobQueryHandler(QueryDbContext queryDbContext)
        {
            _queryDbContext = queryDbContext;
        }
        public Task<JobDto> Handle(GetJobQuery request, CancellationToken cancellationToken)
        {
            var job = _queryDbContext.Jobs.FirstOrDefault(j => j.Id == request.JobId);
            return Task.FromResult(new JobDto()
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

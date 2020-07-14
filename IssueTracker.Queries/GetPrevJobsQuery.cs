using CSharpFunctionalExtensions;
using IssueTracker.Domain.Entities;
using IssueTracker.Domain.Repositories;
using IssueTracker.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Queries
{
    public class PrevJobDto
    {
        public PrevJobDto(int prevJobId, string name)
        {
            PrevJobId = prevJobId;
            Name = name;
        }
        public int PrevJobId { get; set; }
        public string Name { get; set; }
    }
    public class GetPrevJobsQuery : IRequest<ICollection<PrevJobDto>>
    {
        public GetPrevJobsQuery(int jobId)
        {
            JobId = jobId;
        }
        public int JobId { get; set; }
    }
    public class GetPrevJobsQueryHandler : IRequestHandler<GetPrevJobsQuery, ICollection<PrevJobDto>>
    {
        private readonly QueryDbContext _queryDbContext;
        public GetPrevJobsQueryHandler(QueryDbContext queryDbContext)
        {
            _queryDbContext = queryDbContext;
        }
        public Task<ICollection<PrevJobDto>> Handle(GetPrevJobsQuery request, CancellationToken cancellationToken)
        {
            var previousJobsId = _queryDbContext.Jobs.Where(j => j.Id == request.JobId).SelectMany(j => j.StartsAfterJobs).Select(pj => pj.StartsAfterJobId).ToList();
            ICollection<PrevJobDto> listOfPrevJobs = new List<PrevJobDto>();
            foreach (var prevJobId in previousJobsId)
            {
                listOfPrevJobs.Add(new PrevJobDto(prevJobId, _queryDbContext.Jobs.FirstOrDefault(j => j.Id == prevJobId).Name));
            }

            return Task.FromResult(listOfPrevJobs);
        }
    }
}

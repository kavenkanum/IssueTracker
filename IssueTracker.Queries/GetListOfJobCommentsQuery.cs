using IssueTracker.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace IssueTracker.Queries
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreate { get; set; }
        public string UserId { get; set; }
    }
    public class GetListOfJobCommentsQuery : IRequest<ICollection<CommentDto>>
    {
        public GetListOfJobCommentsQuery(int jobId)
        {
            JobId = jobId;
        }
        public int JobId { get; set; }
    }

    public class GetListOfJobCommentQueryHandler : IRequestHandler<GetListOfJobCommentsQuery, ICollection<CommentDto>>
    {
        private readonly QueryDbContext _queryDbContext;
        public GetListOfJobCommentQueryHandler(QueryDbContext queryDbContext)
        {
            _queryDbContext = queryDbContext;
        }
        public Task<ICollection<CommentDto>> Handle(GetListOfJobCommentsQuery request, CancellationToken cancellationToken)
        {
            var job = _queryDbContext.Jobs.Include(j => j.Comments).FirstOrDefault(j => j.Id == request.JobId);
            return Task.FromResult(job.Comments.Select(c => new CommentDto
            {
                CommentId = c.Id,
                Description = c.Description,
                DateOfCreate = c.DateOfComment,
                UserId = c.UserId
            }).ToList() as ICollection<CommentDto>);
        }
    }
}

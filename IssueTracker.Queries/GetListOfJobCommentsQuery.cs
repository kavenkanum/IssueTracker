using IssueTracker.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CSharpFunctionalExtensions;
using IssueTracker.Domain.Entities;

namespace IssueTracker.Queries
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public string Description { get; set; }
        public DateTime DateOfCreate { get; set; }
        public string UserId { get; set; }
    }
    public class GetListOfJobCommentsQuery : IRequest<Result<ICollection<CommentDto>>>
    {
        public GetListOfJobCommentsQuery(int jobId)
        {
            JobId = jobId;
        }
        public int JobId { get; set; }
    }

    public class GetListOfJobCommentQueryHandler : IRequestHandler<GetListOfJobCommentsQuery, Result<ICollection<CommentDto>>>
    {
        private readonly QueryDbContext _queryDbContext;
        public GetListOfJobCommentQueryHandler(QueryDbContext queryDbContext)
        {
            _queryDbContext = queryDbContext;
        }
        public async Task<Result<ICollection<CommentDto>>> Handle(GetListOfJobCommentsQuery request, CancellationToken cancellationToken)
        {
            Maybe<Job> job = await _queryDbContext.Jobs.Include(j => j.Comments).FirstOrDefaultAsync(j => j.Id == request.JobId);
            return job.ToResult($"Unable to find job with id {request.JobId}.")
                .OnSuccess(job => job.Comments.Select(c => new CommentDto
                {
                    CommentId = c.Id,
                    Description = c.Description,
                    DateOfCreate = c.DateOfComment,
                    UserId = c.UserId
                }).ToList() as ICollection<CommentDto>);
        }
    }
}

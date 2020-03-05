using CSharpFunctionalExtensions;
using IssueTracker.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Commands
{
    public class CreateCommentCommand : IRequest<Result>
    {
        public CreateCommentCommand(int jobId, int userId, string description)
        {
            JobId = jobId;
            UserId = userId;
            //DateOfCreate
        }
        public int JobId { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        //DateOfCreate
    }
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Result>
    {
        private readonly IJobRepository _jobRepository;
        //IUserRepository
        public CreateCommentCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public Task<Result> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var jobResult = _jobRepository.GetAsync(request.JobId);

        }
    }
}

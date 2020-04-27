using CSharpFunctionalExtensions;
using IssueTracker.Domain.Entities;
using IssueTracker.Domain.Language.ValueObjects;
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
        public CreateCommentCommand(int jobId, long userId, string description)
        {
            JobId = jobId;
            UserId = userId;
            Description = description;
        }
        public int JobId { get; set; }
        public long UserId { get; set; }
        public string Description { get; set; }
    }
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Result>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDateTimeProvider _dateTimeProvider;

        public CreateCommentCommandHandler(IJobRepository jobRepository, IUserRepository userRepository, IDateTimeProvider dateTimeProvider)
        {
            _jobRepository = jobRepository;
            _userRepository = userRepository;
            _dateTimeProvider = dateTimeProvider;
        }
        public async Task<Result> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var jobResult = await _jobRepository.GetAsync(request.JobId)
                .ToResult($"Unable to find job with id: {request.JobId}");
            //change to user user in the input, controller should get current user and pass
            //it to the createcommentcommand to avoid adding comments by diffrent users than current user
            var userResult = await _userRepository.GetAsync(request.UserId)
                .ToResult($"Unable to find user with id: {request.UserId}");
            var commentResult = Comment.Create(request.Description, request.JobId, request.UserId, _dateTimeProvider.GetCurrentDate());
            //below we are checking if every above operations were succesfull -> 
            //-> if yes we are adding comment to the Job and we are assigning user to that comment ;
            return await Result.Combine(jobResult, userResult, commentResult)
                .OnSuccess(() => jobResult.Value.AddComment(commentResult.Value))
                .OnSuccess(async () => await _jobRepository.SaveAsync(jobResult.Value));
        }
    }
}

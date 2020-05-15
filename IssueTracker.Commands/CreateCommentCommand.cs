using CSharpFunctionalExtensions;
using IssueTracker.Domain.Entities;
using IssueTracker.Domain.Language;
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
    public class CreateCommentCommand : IRequest<Result<int>>
    {
        public CreateCommentCommand(int jobId,  string description)
        {
            JobId = jobId;
            Description = description;
        }
        public int JobId { get; set; }
        public string Description { get; set; }
    }
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Result<int>>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IUserRepository _userRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CurrentUser _currentUser;

        public CreateCommentCommandHandler(IJobRepository jobRepository, IUserRepository userRepository, IDateTimeProvider dateTimeProvider, CurrentUser currentUser)
        {
            _jobRepository = jobRepository;
            _userRepository = userRepository;
            _dateTimeProvider = dateTimeProvider;
            _currentUser = currentUser;
        }
        public async Task<Result<int>> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var jobResult = await _jobRepository.GetAsync(request.JobId)
                .ToResult($"Unable to find job with id: {request.JobId}");

            var commentResult = Comment.Create(request.Description, request.JobId, _currentUser.Id, _dateTimeProvider.GetCurrentDate());
            //below we are checking if every above operations were succesfull -> 
            //-> if yes we are adding comment to the Job and we are assigning user to that comment ;
            return await Result.Combine(jobResult, commentResult)
                .OnSuccess(() => jobResult.Value.AddComment(commentResult.Value))
                .OnSuccess(async () => await _jobRepository.SaveAsync(jobResult.Value));
        }
    }
}

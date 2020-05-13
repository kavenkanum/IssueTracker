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
    public class EditJobCommand : IRequest<Result>
    {
        public EditJobCommand(int jobId, string name, string description, long assignedUserId, DateTime? deadline, Priority priority)
        {
            JobId = jobId;
            Name = name;
            Description = description;
            AssignedUserId = assignedUserId;
            Deadline = deadline;
            Priority = priority;
        }
        public int JobId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public long AssignedUserId { get; set; }
        public DateTime? Deadline { get; set; }
        public Priority Priority { get; set; }
    }

    public class EditJobCommandHandler : IRequestHandler<EditJobCommand, Result>
    {
        private readonly IJobRepository _jobRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        public EditJobCommandHandler(IJobRepository jobRepository, IDateTimeProvider dateTimeProvider)
        {
            _jobRepository = jobRepository;
            _dateTimeProvider = dateTimeProvider;
        }
        public async Task<Result> Handle(EditJobCommand request, CancellationToken cancellationToken)
        {
            var jobResult = await _jobRepository.GetAsync(request.JobId)
                .ToResult($"Unable to find job with id: {request.JobId}.");

            var deadlineResult = Deadline.CreateOptional(request.Deadline.Value, _dateTimeProvider.GetCurrentDate());

            return await Result.Combine(jobResult, deadlineResult)
                .OnSuccess(() => jobResult.Value.EditProperties(request.Name, request.Description, deadlineResult.Value, request.AssignedUserId, request.Priority))
                .OnSuccess(async () => await _jobRepository.SaveAsync(jobResult.Value));
        }
    }
}

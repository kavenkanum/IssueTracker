using CSharpFunctionalExtensions;
using IssueTracker.Domain.Language;
using IssueTracker.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Commands
{
    public class ChangeJobStatusCommand : IRequest<Result>
    {
        public ChangeJobStatusCommand(int jobId, int requestedStatus)
        {
            JobId = jobId;
            RequestedStatus = (Status)requestedStatus;
        }
        public int JobId { get; set; }
        //probably i will have to change RequestedStatus to string
        public Status RequestedStatus { get; set; }
    }
    public class ChangeJobStatusCommandHandler : IRequestHandler<ChangeJobStatusCommand, Result>
    {
        private readonly IJobRepository _jobRepository;

        public ChangeJobStatusCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public async Task<Result> Handle(ChangeJobStatusCommand request, CancellationToken cancellationToken)
        {
            var jobResult = await _jobRepository.GetAsync(request.JobId)
                .ToResult($"Unable to find job with id: { request.JobId}.");

            if (request.RequestedStatus == Status.InProgress)
            {
                //if status from request is InProgress that mean user wanted to start that job so we will invoke StartJob method; invoking that method is only possible with status NEW;
                var currentJobStatus = jobResult.OnSuccess(j => j.Status);
                return await Result.Create(currentJobStatus.Value == Status.New, "Cannot start job at current job status")
                    .OnSuccess(() => jobResult.Value.StartJob())
                    .OnSuccess(async () => await _jobRepository.SaveAsync(jobResult.Value));
            }
            else if (request.RequestedStatus == Status.Done)
            {
                //if status from request is Done that mean user wanted to finish that job so we will invoke FinishJob method; invoking that method is only possible with status INPROGRESS;
                var currentJobStatus = jobResult.OnSuccess(j => j.Status);
                return await Result.Create(currentJobStatus.Value == Status.InProgress, "Cannot start job at current job status")
                    .OnSuccess(() => jobResult.Value.FinishJob())
                    .OnSuccess(() => _jobRepository.SaveAsync(jobResult.Value));
            }
            else
            {
                //requested status can only be InProgress or Done !
                return Result.CreateFailure(request.RequestedStatus != Status.InProgress && request.RequestedStatus != Status.Done, "Something went very wrong..");
            }

        }
    }
}

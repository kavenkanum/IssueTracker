using CSharpFunctionalExtensions;
using IssueTracker.Domain.Entities;
using IssueTracker.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IssueTracker.Commands
{
    public class AddPrevJobsCommand : IRequest<Result>
    {
        public AddPrevJobsCommand(int jobId, List<int> startsAfterJobsId)
        {
            JobId = jobId;
            StartsAfterJobsId = startsAfterJobsId;
        }
        public int JobId { get; set; }
        public List<int> StartsAfterJobsId { get; set; }
    }

    public class AddPrevJobsCommandHandler : IRequestHandler<AddPrevJobsCommand, Result>
    {
        private readonly IJobRepository _jobRepository;
        public AddPrevJobsCommandHandler(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }
        public async Task<Result> Handle(AddPrevJobsCommand request, CancellationToken cancellationToken)
        {
            var currentJobResult = await _jobRepository.GetAsync(request.JobId);
            if (currentJobResult.HasNoValue)
                return Result.Fail($"Cannot find job with id: {request.JobId}.");

            foreach (var startsAfterJobId in request.StartsAfterJobsId)
            {
                if (_jobRepository.GetAsync(startsAfterJobId).Result.HasNoValue)
                {
                    return Result.Fail($"Cannot find job with id: {startsAfterJobId} to assign it as a previous job.");
                }
                if (currentJobResult.Value.StartsAfterJobs.Any(j => j.StartsAfterJobId == startsAfterJobId))
                    return Result.Fail($"Cannot add previous job with id: {startsAfterJobId}, because there is already that previous job.");
            }

            var allJobsWithPrevJobs = _jobRepository.GetJobsWithPrevJobs(currentJobResult.Value.ProjectId).Result;
            var jobsQueue = new List<int>();
            var failureList = new List<int>();
            jobsQueue.Add(request.JobId);

            


            var prevJobsResult = currentJobResult.Value.CheckPrevJobs(request.StartsAfterJobsId, jobsQueue, allJobsWithPrevJobs, failureList);
            if (prevJobsResult.Any())
                return Result.Fail($"Infinite loop, at job/jobs id: {string.Join(", ", prevJobsResult.ToArray())}.");

            var startsAfterJobsResult = new List<StartsAfterJob>();

            foreach (var startsAfterJobId in request.StartsAfterJobsId)
            {
                StartsAfterJob.Create(startsAfterJobId).OnSuccess(startAfterJobResult => startsAfterJobsResult.Add(startAfterJobResult));
            }

            return await Result.Create(startsAfterJobsResult.Count == request.StartsAfterJobsId.Count, "Couldn't create one or more StartsAfterJob.")
                    .OnSuccess(() => currentJobResult.Value.AddPreviousJobs(startsAfterJobsResult))
                    .OnSuccess(() => _jobRepository.SaveAsync(currentJobResult.Value));
        }
    }
}

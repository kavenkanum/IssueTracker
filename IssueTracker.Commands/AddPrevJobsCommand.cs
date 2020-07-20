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

            var startsAfterJobIdResults = _jobRepository.GetManyAsync(request.StartsAfterJobsId.ToArray());

            if (startsAfterJobIdResults.Result.Count != request.StartsAfterJobsId.Count)
            {
                var failedJobsResult = new List<string>();
                foreach (var j in startsAfterJobIdResults.Result)
                {
                    if(request.StartsAfterJobsId.Contains(j.Id)) 
                        failedJobsResult.Add(j.Id.ToString());
                }
                var failedJobsId = string.Join(" ", failedJobsResult);
                return Result.Fail($"Cannot find job(s) with id: [{failedJobsId}] to assign it(they) as a previous job(s).");
            }
                
            foreach (var startsAfterJobId in request.StartsAfterJobsId)
            {
                if (_jobRepository.GetAsync(startsAfterJobId).Result.HasNoValue)
                {
                    return Result.Fail($"Cannot find job with id: {startsAfterJobId} to assign it as a previous job.");
                }
            }

            var allJobsWithPrevJobs = _jobRepository.GetJobsWithPrevJobs(currentJobResult.Value.ProjectId).Result;
            var result = currentJobResult.Value.AddPreviousJobs(currentJobResult.Value, request.StartsAfterJobsId, allJobsWithPrevJobs);      

            return await result.OnSuccess(() => _jobRepository.SaveAsync(currentJobResult.Value));
        }
    }
}

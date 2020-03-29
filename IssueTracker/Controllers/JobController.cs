using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using IssueTracker.Commands;
using IssueTracker.Queries;
using System.Threading.Tasks;

namespace IssueTracker.Controllers
{
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IMediator _mediator;
        public JobController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{projectId}/Jobs")]
        public async Task<IActionResult> GetJobsOfProject(int projectId)
        {
            var jobsQuery = await _mediator.Send(new GetListOfProjectJobsQuery(projectId));
            return Ok(jobsQuery);
        }
        [HttpPost]
        [Route("{projectId}/AddJob")]
        public async Task<IActionResult> AddJob(int projectId, string jobName)
        {
            var newJobResult = await _mediator.Send(new CreateJobCommand(projectId, jobName));
            return Ok(newJobResult);
        }

        [HttpGet]
        [Route("Job/{jobId}")]
        public async Task<IActionResult> GetJob(int jobId)
        {
            var jobQuery = await _mediator.Send(new GetJobQuery(jobId));
            return Ok(jobQuery);
        }
    }
}

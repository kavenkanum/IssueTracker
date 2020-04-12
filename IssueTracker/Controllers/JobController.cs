﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using IssueTracker.Commands;
using IssueTracker.Queries;
using System.Threading.Tasks;
using IssueTracker.Models;
using CSharpFunctionalExtensions;
using IssueTracker.Domain.Language;

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
            var jobsQuery = await _mediator.Send(new GetListOfProjectJobsQuery(projectId, Status.New));
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

        [HttpGet]
        [Route("Job/Edit/{jobId}")]
        public async Task<IActionResult> EditJob(int jobId)
        {
            var result = await _mediator.Send(new GetJobToEditQuery(jobId));

            return result.IsSuccess ? Ok(new EditJobModel()
            {
                JobId = result.Value.JobId,
                Name = result.Value.Name,
                Description = result.Value.Description,
                Deadline = result.Value.Deadline,
                Priority = result.Value.Priority,
                UserId = result.Value.AssignedUserID
            }) as IActionResult : NotFound();
        }

        [HttpPost]
        [Route("Job/{jobId}/Edit")]
        public async Task<IActionResult> EditJob(EditJobModel model)
        {
            var jobToEditResult = await _mediator.Send(new EditJobCommand(model.JobId, model.Name, model.Description, model.UserId, model.Deadline, model.Priority));

            return Ok(jobToEditResult);
        }

        [HttpPost]
        [Route("Job/{jobId}/AssignUser")]
        public IActionResult AssignUser(int jobId, Guid userId)
        {
            var assignUserResult = _mediator.Send(new AssignUserCommand(jobId, userId));

            return Ok(assignUserResult);
        }
    }
}

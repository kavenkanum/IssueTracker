using MediatR;
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
using Microsoft.AspNetCore.Authorization;

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
        [Route("projects/{projectId}/jobs")]
        public async Task<IActionResult> GetJobsOfProject(int projectId)
        {
            var jobsQuery = await _mediator.Send(new GetListOfProjectJobsQuery(projectId, Status.New));
            return Ok(jobsQuery);
        }
        [HttpPost]
        [Route("projects/{projectId}/jobs")]
        public async Task<IActionResult> AddJob([FromBody] AddJobModel newJob)
        {
            var newJobResult = await _mediator.Send(new CreateJobCommand(newJob.ProjectId, newJob.JobName));
            return newJobResult.IsSuccess ? Ok(newJobResult.Value) : BadRequest(newJobResult.Error) as IActionResult;
        }

        [HttpGet]
        [Route("jobs/{jobId}")]
        public async Task<IActionResult> GetJob(int jobId)
        {
            var jobQuery = await _mediator.Send(new GetJobQuery(jobId));
            return Ok(jobQuery.Value);
        }

        [HttpGet]
        [Route("jobs/{jobId}/edit")]
        public async Task<IActionResult> EditJob(int jobId)
        {
            var result = await _mediator.Send(new GetJobToEditQuery(jobId));

            return result.IsSuccess ? Ok(new EditJobModel()
            {
                JobId = result.Value.JobId,
                Name = result.Value.Name,
                Description = result.Value.Description,
                Deadline = result.Value.Deadline,
                Priority = (int)result.Value.Priority,
                AssignedUserId = result.Value.AssignedUserID
            }) as IActionResult : NotFound();
        }

        [HttpPost]
        [Route("jobs/{jobId}/edit")]
        public async Task<IActionResult> EditJob(EditJobModel model)
        {
            var jobToEditResult = await _mediator.Send(new EditJobCommand(model.JobId, model.Name, model.Description, model.AssignedUserId, model.Deadline, model.Priority));

            return Ok(jobToEditResult);
        }

        [HttpGet]
        [Route("jobs/{jobId}/edit/getUsers")]
        public async Task<IActionResult> GetUsersToAssign()
        {
            var usersToAssignQuery = await _mediator.Send(new GetUsersToJobAssignQuery());
            return Ok(usersToAssignQuery);
        }

        [HttpPost]
        [Route("jobs/{jobId}/prevJobs")]
        public async Task<IActionResult> AddPrevJobs(AddPrevJobsModel model)
        {
            var prevJobsToAdd = await _mediator.Send(new AddPrevJobsCommand(model.JobId, model.PrevJobsId.ToList()));
            return Ok(prevJobsToAdd);
        }

        [HttpGet]
        [Route("jobs/{jobId}/prevJobs")]
        public async Task<IActionResult> GetPrevJobs(int jobId)
        {
            var prevJobsResult = await _mediator.Send(new GetPrevJobsQuery(jobId));
            return Ok(prevJobsResult);
        }

        [HttpPost]
        [Route("Job/{jobId}/AssignUser")]
        public async Task<IActionResult> AssignUser(int jobId, long userId)
        {
            var assignUserResult = await _mediator.Send(new AssignUserCommand(jobId, userId));

            return Ok(assignUserResult);
        }

        //public async Task<IActionResult> DeleteJob(int jobId)
        //{
        //    //authorization to delete method in Job entity
        //    var deleteJobResult = await _mediator.Send(new DeleteJobCommand(9));
        //    return Ok(deleteJobResult);
        //}
    }

    public class AddJobModel
    {
        public int ProjectId { get; set; }
        public string JobName { get; set; }
    }

    public class AddPrevJobsModel
    {
        public int JobId { get; set; }
        public int[] PrevJobsId { get; set; }
    }
}

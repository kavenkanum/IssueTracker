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
using System.Text.Json;
using System.Text.Json.Serialization;

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
        public async Task<IActionResult> GetJobsOfProject(int projectId, int jobsStatus)
        {
            var jobsQuery = await _mediator.Send(new GetListOfProjectJobsQuery(projectId, jobsStatus));
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
        public async Task<IActionResult> GetJob(int jobId, int projectId)
        {
            var jobQuery = await _mediator.Send(new GetJobQuery(jobId, projectId));
            return jobQuery.IsSuccess ? Ok(jobQuery.Value) as IActionResult : BadRequest(jobQuery.Error);
        }

        [HttpGet]
        [Route("jobs/{jobId}/edit")]
        public async Task<IActionResult> EditJob(int jobId)
        {
            var result = await _mediator.Send(new GetJobToEditQuery(jobId));

            var deadlineAfterSerialization = result.Value.Deadline.HasValue ? result.Value.Deadline.Value.ToString("yyyy-MM-ddTHH:mm:ss") : "";

            return result.IsSuccess ? Ok(new EditJobModel()
            {
                JobId = result.Value.JobId,
                Name = result.Value.Name,
                Description = result.Value.Description,
                Deadline = deadlineAfterSerialization,
                Priority = (int)result.Value.Priority,
                AssignedUserId = result.Value.AssignedUserID
            }) as IActionResult : NotFound(); 
        }

        [HttpPut]
        [Route("jobs/{jobId}/edit")]
        public async Task<IActionResult> EditJob(EditJobModel model)
        {
            var jobToEditResult = await _mediator.Send(new EditJobCommand(model.JobId, model.Name, model.Description, model.AssignedUserId, model.Deadline, model.Priority));

            return jobToEditResult.IsSuccess ? Ok(jobToEditResult) as IActionResult : BadRequest(jobToEditResult.Error);
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
            return prevJobsToAdd.IsSuccess ? Ok(true) : BadRequest(prevJobsToAdd.Error) as IActionResult;
        }

        [HttpGet]
        [Route("jobs/{jobId}/availablePrevJobs")]
        public async Task<IActionResult> GetAvailablePrevJobs(int jobId)
        {
            var availablePrevJobsResult = await _mediator.Send(new GetAvailablePrevJobsQuery(jobId));
            return Ok(availablePrevJobsResult);
        }

        [HttpGet]
        [Route("jobs/{jobId}/prevJobs")]
        public async Task<IActionResult> GetPrevJobs(int jobId)
        {
            var prevJobsQuery = await _mediator.Send(new GetPrevJobsQuery(jobId));
            return Ok(prevJobsQuery);
        }

        [HttpPost]
        [Route("Job/{jobId}/AssignUser")]
        public async Task<IActionResult> AssignUser(int jobId, long userId)
        {
            var assignUserResult = await _mediator.Send(new AssignUserCommand(jobId, userId));

            return Ok(assignUserResult);
        }

        [HttpPatch]
        [Route("jobs/{jobId}/changeJobStatus")]
        public async Task<IActionResult> ChangeJobStatus(ChangeJobStatusModel model)
        {
            var changeJobStatusResult = await _mediator.Send(new ChangeJobStatusCommand(model.JobId, model.RequestedStatus));
            return changeJobStatusResult.IsSuccess ? Ok(true) : BadRequest(changeJobStatusResult.Error) as IActionResult;
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
    public class ChangeJobStatusModel
    {
        public int JobId { get; set; }
        public int RequestedStatus { get; set; }
    }
}

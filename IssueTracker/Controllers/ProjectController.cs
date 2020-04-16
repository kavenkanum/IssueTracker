using CSharpFunctionalExtensions;
using IssueTracker.Commands;
using IssueTracker.Domain.Language;
using IssueTracker.Policies;
using IssueTracker.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Controllers
{
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAuthorizationService _authorization;

        public ProjectController(IMediator mediator, IAuthorizationService authorization)
        {
            _mediator = mediator;
            _authorization = authorization;
        }

        //[Authorize(Policy = "IsAdmin")]
        [Route("Projects")]
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projectsResult = await _mediator.Send(new GetListOfProjectsQuery());
            return Ok(projectsResult);

            //var req = new DeleteJobRequirement
            //{
            //    JobId = 1
            //};

            //var allowed = await _authorization.AuthorizeAsync(User, "IsAdmin");
            //var allowed = await _authorization.AuthorizeAsync(User, null, req);

            //if (allowed.Succeeded)
            //{
            //    return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
            //}
            //else
            //{
            //    return Challenge();
            //}

            //var newJob = await _mediator.Send(new CreateJobCommand(3, "new task"));
            //return Ok(newJob);

        }

        [Route("Projects/AddProject")]
        [HttpPost]
        public async Task<IActionResult> AddProject(string projectName)
        {
            var newProjectResult = await _mediator.Send(new CreateProjectCommand(projectName));
            return Ok(newProjectResult);
        }
    }
}
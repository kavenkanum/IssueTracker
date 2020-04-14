using CSharpFunctionalExtensions;
using IssueTracker.Commands;
using IssueTracker.Domain.Language;
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

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Policy = "IsAdmin")]
        [Route("Projects")]
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
            //var newJob = await _mediator.Send(new CreateJobCommand(3, "new task"));
            //return Ok(newJob);
            //var projectsQuery = await _mediator.Send(new GetListOfProjectsQuery());
            //return Ok(projectsQuery);
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
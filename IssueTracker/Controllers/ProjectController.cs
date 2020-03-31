using CSharpFunctionalExtensions;
using IssueTracker.Commands;
using IssueTracker.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IssueTracker.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Route("Project/GetProjects")]
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projectsQuery = await _mediator.Send(new GetListOfProjectsQuery());
            return Ok(projectsQuery);
        }

        [Route("Project/AddProject")]
        [HttpPost]
        public async Task<IActionResult> AddProject(string projectName)
        {
            var newProjectResult = await _mediator.Send(new CreateProjectCommand(projectName));
            return Ok(newProjectResult);
        }
    }
}
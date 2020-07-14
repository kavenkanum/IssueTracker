using CSharpFunctionalExtensions;
using IssueTracker.Commands;
using IssueTracker.Domain.Language;
using IssueTracker.Policies;
using IssueTracker.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IssueTracker.Controllers
{
    [AllowAnonymous]
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
        [Route("projects")]
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


        [Route("projects/{projectId}")]
        [HttpGet]
        public async Task<IActionResult> GetProject(int projectId)
        {
            var projectQuery = await _mediator.Send(new GetProjectQuery(projectId));
            return projectQuery.IsSuccess ?
                Ok(projectQuery.Value) :
                BadRequest(projectQuery.Error) as IActionResult;
        }

        //JsonConvert.DefaultSettings = () => new JsonSerializerSettings
        //    {
        //        ContractResolver = new CamelCasePropertyNamesContractResolver()
        //    };

        [Route("projects")]
        [HttpPost]
        public async Task<IActionResult> AddProject([FromBody]AddProjectModel newProject)
        {
            var newProjectResult = await _mediator.Send(new CreateProjectCommand(newProject.Name));
            return newProjectResult.IsSuccess ?
                Ok(new { id = newProjectResult.Value }) :
                BadRequest(newProjectResult.Error) as IActionResult;
        }
    }
    public class AddProjectModel
    {
        public string Name { get; set; }
    }

}
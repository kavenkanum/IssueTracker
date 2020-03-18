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
    [Route("[controller]")]
    public class HealthMonitorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HealthMonitorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<JobDto> Index()
        {

            //var dupa2 = new CreateJobCommand(7, "jobtest3");
            //var newProject = await _mediator.Send(new CreateProjectCommand("test1"));

            //var newJob2 = await _mediator.Send(dupa2);
            var newComment = await _mediator.Send(new CreateCommentCommand(1, "db96ef56-e6cb-4678-b097-2fbfc29520df", "comment test"));
            /*return result
                .OnSuccess(() => Ok())
                .OnFailure(error => BadRequest(error));*/
            // var projectJobsQuery = await _mediator.Send(new GetListOfProjectJobsQuery(1));
            //var jobCommentsQuery = await _mediator.Send(new GetListOfJobCommentsQuery(1));
            var jobQuery = await _mediator.Send(new GetJobQuery(2));
            
            return jobQuery;
        }
    }
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.Commands;
using IssueTracker.Queries;

namespace IssueTracker.Controllers
{
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CommentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("jobs/{jobId}/comments")]
        public async Task<IActionResult> GetComments(int jobId)
        {
            var commentsQuery = await _mediator.Send(new GetListOfJobCommentsQuery(jobId));
            return Ok(commentsQuery.Value);
        }

        [HttpPost]
        [Route("jobs/{jobId}/comments")]
        public async Task<IActionResult> AddComment([FromBody]NewCommentModel newComment)
        {
            var newCommentResult = await _mediator.Send(new CreateCommentCommand(newComment.JobId, newComment.Description));
            return newCommentResult.IsSuccess ? Ok(newCommentResult.Value) : BadRequest(newCommentResult.Error) as IActionResult;
        }
    }

    public class NewCommentModel
    {
        public int JobId { get; set; }
        public string Description { get; set; }
    }
}

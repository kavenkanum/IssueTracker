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
        [Route("{jobId}/GetComments")]
        public async Task<IActionResult> GetComments(int jobId)
        {
            var commentsQuery = await _mediator.Send(new GetListOfJobCommentsQuery(jobId));
            return Ok(commentsQuery);
        }

        [HttpPost]
        [Route("{jobId}/AddComment")]
        public async Task<IActionResult> AddComment(int jobId, string userId, string description)
        {
            var newCommentResult = await _mediator.Send(new CreateCommentCommand(jobId, userId, description));
            return Ok(newCommentResult);
        }
    }
}

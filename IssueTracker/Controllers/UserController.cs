using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IssueTracker.Commands;
using IssueTracker.Queries;
using MediatR;

namespace IssueTracker.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetUsers()
        {
            var usersQuery = await _mediator.Send(new GetUsersQuery());
            return Ok(usersQuery);
        }

        [HttpGet]
        [Route("users/{userId}")]
        public async Task<IActionResult> GetAssignedUser(long userId)
        {
            var assignedUserQuery = await _mediator.Send(new GetAssignedUserQuery(userId));
            return assignedUserQuery.IsSuccess ? Ok(assignedUserQuery) : BadRequest(assignedUserQuery.Error) as IActionResult;
        }
    }
}

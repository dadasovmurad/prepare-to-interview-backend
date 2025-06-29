using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrepareToInterview.Application.Features.Commands.Users.CreateUser;
using PrepareToInterview.Application.Features.Queries.Users.CheckUserEmailExists;
using PrepareToInterview.Application.Features.Queries.Users.CheckUsernameExists;

namespace PrepareToInterview.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromForm] CreateUserCommand createUserCommand)
        {
            var response = await _mediator.Send(createUserCommand);
            return Ok(response);
        }

        [HttpGet("check-email/{email}")]
        public async Task<IActionResult> CheckEmailExists([FromRoute] string email)
        {
            var response = await _mediator.Send(new CheckUserEmailExistsQuery { Email = email });
            return Ok(response);
        }

        [HttpGet("check-username/{username}")]
        public async Task<IActionResult> CheckUsernameExists([FromRoute] string username)
        {
            var response = await _mediator.Send(new CheckUsernameExistsQuery { Username = username });
            return Ok(response);
        }
    }
}

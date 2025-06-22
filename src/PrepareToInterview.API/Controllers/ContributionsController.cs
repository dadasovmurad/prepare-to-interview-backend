using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrepareToInterview.Application.Features.Commands.Contributions.AcceptContribution;
using PrepareToInterview.Application.Features.Commands.Contributions.CreateContribution;
using PrepareToInterview.Application.Features.Commands.Users.CreateUser;

namespace PrepareToInterview.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContributionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContributionsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateContribution([FromBody] CreateContributionCommand createContributionCommand)
        {
            var response = await _mediator.Send(createContributionCommand);
            return Ok(response);
        }
        [HttpPost("approve")]
        public async Task<IActionResult> ApproveContribution([FromBody] ApproveContributionCommand approveContributionCommand)
        {
            var response = await _mediator.Send(approveContributionCommand);
            return Ok(response);
        }
    }
}

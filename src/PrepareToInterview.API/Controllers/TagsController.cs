using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrepareToInterview.Application.Features.Commands.Tags.CreateTag;
using PrepareToInterview.Application.Features.Queries.Tags.GetAllTags;

namespace PrepareToInterview.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] GetAllTagsQuery getAllTagQuery)
        {
            var response = await _mediator.Send(getAllTagQuery);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] CreateTagCommand createTagCommand)
        {
            var response = await _mediator.Send(createTagCommand);
            return Ok(response);
        }
    }
}
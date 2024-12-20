using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrepareToInterview.Application.Features.Queries.Categories.GetAllCategory;
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
    }
}
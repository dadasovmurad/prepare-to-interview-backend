using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrepareToInterview.Application.Features.Commands.Categories.CreateCategory;
using PrepareToInterview.Application.Features.Queries.Categories.GetAllCategory;
using PrepareToInterview.Application.Features.Queries.Categories.GetCategoryChildrensById;
using PrepareToInterview.Application.Features.Queries.Categories.GetCategoryHeaders;

namespace PrepareToInterview.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] GetAllCategoriesQuery getAllCategoryQuery)
        {
            var response = await _mediator.Send(getAllCategoryQuery);
            return Ok(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            var response = await _mediator.Send(createCategoryCommand);
            return Ok(response);
        }
        [HttpGet("headers")]
        public async Task<IActionResult> GetCategoryHeaders([FromQuery] GetCategoryHeadersQuery getCategoryHeadersQuery)
        {
            var response = await _mediator.Send(getCategoryHeadersQuery);
            return Ok(response);    
        }
        [HttpGet("children")]
        public async Task<IActionResult> GetCategoryChildrenById([FromQuery] GetCategoryChildrensById query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
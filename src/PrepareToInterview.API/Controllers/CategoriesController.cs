using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrepareToInterview.Application.Features.Commands.Categories.CreateCategory;
using PrepareToInterview.Application.Features.Queries.Categories.GetAllCategory;

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
    }
}

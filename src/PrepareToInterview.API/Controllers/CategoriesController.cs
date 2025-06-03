using Microsoft.AspNetCore.Mvc;
using PrepareToInterview.Application.DTOs.Category;
using PrepareToInterview.Application.Services;

namespace PrepareToInterview.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await _categoryService.GetAllAsync(pageNumber, pageSize);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] int id)
        {
            var response = await _categoryService.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryCreateDto categoryCreateDto)
        {
            var response = await _categoryService.CreateAsync(categoryCreateDto);
            return StatusCode(StatusCodes.Status201Created, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] CategoryUpdateDto categoryUpdateDto)
        {
            categoryUpdateDto.Id = id;
            var response = await _categoryService.UpdateAsync(categoryUpdateDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var response = await _categoryService.DeleteAsync(id);
            return Ok(response);
        }
    }
}
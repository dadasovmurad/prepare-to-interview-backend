using Microsoft.AspNetCore.Mvc;
using PrepareToInterview.Application.DTOs.Tag;
using PrepareToInterview.Application.Services;

namespace PrepareToInterview.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await _tagService.GetAllAsync(pageNumber, pageSize);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTagById([FromRoute] int id)
        {
            var response = await _tagService.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTag([FromBody] TagCreateDto tagCreateDto)
        {
            var response = await _tagService.CreateAsync(tagCreateDto);
            return StatusCode(StatusCodes.Status201Created, response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag([FromRoute] int id, [FromBody] TagUpdateDto tagUpdateDto)
        {
            tagUpdateDto.Id = id;
            var response = await _tagService.UpdateAsync(tagUpdateDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag([FromRoute] int id)
        {
            var response = await _tagService.DeleteAsync(id);
            return Ok(response);
        }
    }
}
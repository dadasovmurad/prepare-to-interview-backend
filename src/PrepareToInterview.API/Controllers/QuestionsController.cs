using Microsoft.AspNetCore.Mvc;
using PrepareToInterview.Application.DTOs.Question;
using PrepareToInterview.Application.Services;

namespace PrepareToInterview.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuestionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionCreateDto createQuestionDto)
        {
            var response = await _questionService.CreateAsync(createQuestionDto);
            return StatusCode(StatusCodes.Status201Created, response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllQuestions([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await _questionService.GetAllAsync(pageNumber, pageSize);
            return Ok(response);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredQuestions(
            [FromQuery] string? category = null,
            [FromQuery] string? subCategory = null,
            [FromQuery] string? difficulty = null,
            [FromQuery] string[]? tags = null,
            [FromQuery] string? term = null,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var response = await _questionService.GetFilteredQuestionsAsync(category, subCategory, difficulty, tags, term, pageNumber, pageSize);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById([FromRoute] int id)
        {
            var response = await _questionService.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateQuestion([FromBody] QuestionUpdateDto updateQuestionDto)
        {
            var response = await _questionService.UpdateAsync(updateQuestionDto);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteQuestion([FromRoute] int id)
        {
            var response = await _questionService.DeleteAsync(id);
            return Ok(response);
        }

        [HttpGet("total-count")]
        public async Task<ActionResult> GetTotalQuestionCount()
        {
            var response = await _questionService.GetTotalCountAsync();
            return Ok(response);
        }

        [HttpGet("related-questions/{questionId}")]
        public async Task<ActionResult> GetRelatedQuestions([FromRoute] int questionId)
        {
            var response = await _questionService.GetRelatedQuestionsAsync(questionId);
            return Ok(response);
        }
    }
}
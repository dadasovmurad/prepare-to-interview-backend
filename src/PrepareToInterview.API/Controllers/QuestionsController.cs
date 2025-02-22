using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrepareToInterview.Application.Features.Commands.Questions.CreateQuestion;
using PrepareToInterview.Application.Features.Commands.Questions.RemoveQuestion;
using PrepareToInterview.Application.Features.Commands.Questions.UpdateQuestion;
using PrepareToInterview.Application.Features.Queries.Questions.FilterQuestions;
using PrepareToInterview.Application.Features.Queries.Questions.GetAllQuestion;
using PrepareToInterview.Application.Features.Queries.Questions.GetByIdQuestion;
using PrepareToInterview.Application.Features.Queries.Questions.GetTotalQuestionCount;

namespace PrepareToInterview.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> CreateQuestion([FromBody] CreateQuestionCommand createQuestionCommandRequest)
        {
            var response = await _mediator.Send(createQuestionCommandRequest);
            return StatusCode(StatusCodes.Status201Created, response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllQuestions([FromQuery] GetAllQuestionsQuery getAllQuestionQuery)
        {
            var response = await _mediator.Send(getAllQuestionQuery);
            return Ok(response);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetQuestionById([FromRoute] GetByIdQuestionQuery getByIdQuestionQueryRequest)
        {
            var response = await _mediator.Send(getByIdQuestionQueryRequest);
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateQuestion(UpdateQuestionCommand updateQuestionCommandRequest)
        {
            var response = await _mediator.Send(updateQuestionCommandRequest);
            return Ok(response);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult> RemoveQuestion([FromRoute] RemoveQuestionCommand removeQuestionCommandRequest)
        {
            var response = await _mediator.Send(removeQuestionCommandRequest);
            return Ok(response);
        }
        [HttpGet("total-count")]
        public async Task<ActionResult> GetTotalQuestionCount([FromRoute] GetTotalQuestionCountQuery getTotalQuestionCountQuery)
        {
            var response = await _mediator.Send(getTotalQuestionCountQuery);
            return Ok(response);
        }
        [HttpPost("filter")]
        public async Task<IActionResult> GetFilteredQuestionsPost([FromBody] GetFilteredQuestionsQuery getFilteredQuestionsQuery)
        {
            var response = await _mediator.Send(getFilteredQuestionsQuery);
            return Ok(response);
        }
    }
}

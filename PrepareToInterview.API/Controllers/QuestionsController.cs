using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrepareToInterview.Application.Features.Commands.Question.CreateQuestion;
using PrepareToInterview.Application.Features.Commands.Question.RemoveQuestion;
using PrepareToInterview.Application.Features.Commands.Question.UpdateQuestion;
using PrepareToInterview.Application.Features.Queries.Question.GetAllQuestion;
using PrepareToInterview.Application.Features.Queries.Question.GetByIdQuestion;

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
        public async Task<IActionResult> CreateQuestion([FromBody]CreateQuestionCommand createQuestionCommandRequest)
        {
            var response = await _mediator.Send(createQuestionCommandRequest);
            return StatusCode(StatusCodes.Status201Created, response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            var response = await _mediator.Send(new GetAllQuestionQuery());
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
    }
}

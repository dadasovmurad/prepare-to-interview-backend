using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionReadRepository _questionReadRepository;
        private readonly IQuestionWriteRepository _questionWriteRepository;

        public QuestionsController(IQuestionReadRepository questionReadRepository, IQuestionWriteRepository questionWriteRepository)
        {
            _questionReadRepository = questionReadRepository;
            _questionWriteRepository = questionWriteRepository;
        }

        [HttpGet]
        public IActionResult GetQuestions()
        {
            return Ok(_questionReadRepository.GetAll());
        }

        //// POST api/questions
        //[HttpPost]
        //public IActionResult CreateQuestion([FromBody] QuestionModel question)
        //{
        //    // Your logic to add a new question
        //    return CreatedAtAction(nameof(GetQuestionById), new { id = question.Id }, question);
        //}

        //// GET api/questions/{id}
        //[HttpGet("{id}")]
        //public IActionResult GetQuestionById(int id)
        //{
        //    // Your logic to get a question by id
        //    return Ok(question);
        //}

        //// PUT api/questions/{id}
        //[HttpPut("{id}")]
        //public IActionResult UpdateQuestion(int id, [FromBody] QuestionModel question)
        //{
        //    // Your logic to update a question
        //    return NoContent();
        //}

        //// DELETE api/questions/{id}
        //[HttpDelete("{id}")]
        //public IActionResult DeleteQuestion(int id)
        //{
        //    // Your logic to delete a question
        //    return NoContent();
        //}
    }
}

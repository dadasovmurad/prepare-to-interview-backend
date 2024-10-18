using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using PrepareToInterview.Application.Abstractions.Services;
using PrepareToInterview.Application.Repositories;
using PrepareToInterview.Domain.DTOs;
using PrepareToInterview.Domain.Entities;

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
        [HttpGet]
        public async Task<IActionResult> GetQuestions()
        {
            var datas = await _questionService.GetAllAsync();
            return Ok(datas);
        }

        // POST api/questions
        //[HttpPost]
        //public async Task<IActionResult> CreateQuestion(string data)
        //{
        //    // Your logic to add a new question
        //    Question createdQuestion = new Question()
        //    {
        //        //Category = "Technical",
        //        //Content = "What is Dependency Injection in C#?",
        //        //Answer = new Answer() { Content = "Dependency Injection is a design pattern used to implement IoC, where the control of object creation is transferred from the class to the container." },
        //        //Comments = new List<Comment>() { new Comment() { Content = "Great explanation, thanks!" }, new Comment() { Content = "Could you provide an example?" } }
        //    };
        //    await _questionWriteRepository.AddAsync(createdQuestion);
        //    await _questionWriteRepository.SaveAsync();
        //    return StatusCode(201);
        //}

        // GET api/questions/{id}
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetQuestionById(string id)
        //{
        //    // Your logic to get a question by id
        //    return Ok(await _questionReadRepository.GetByIdAsync(id));
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

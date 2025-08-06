using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrepareToInterview.Application.Features.Commands.Questions.CreateQuestion;
using PrepareToInterview.Application.Features.Commands.Questions.RemoveQuestion;
using PrepareToInterview.Application.Features.Commands.Questions.UpdateQuestion;
using PrepareToInterview.Application.Features.Queries.Questions.FilterQuestions;
using PrepareToInterview.Application.Features.Queries.Questions.GetAllQuestion;
using PrepareToInterview.Application.Features.Queries.Questions.GetByIdQuestion;
using PrepareToInterview.Application.Features.Queries.Questions.GetQuestionByShortUrl;
using PrepareToInterview.Application.Features.Queries.Questions.GetRelatedQuestions;
using PrepareToInterview.Application.Features.Queries.Questions.GetTotalQuestionCount;
using PrepareToInterview.Application.Utilities.Helpers;
using PrepareToInterview.Domain.Entities;
using System.Text;

namespace PrepareToInterview.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        public QuestionsController(IMediator mediator, IConfiguration configuration)
        {
            _mediator = mediator;
            _configuration = configuration;
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
        [HttpGet("related-questions/{QuestionId}")]
        public async Task<ActionResult> GetRelatedQuestions([FromRoute] GetRelatedQuestionsQuery getReleatedQuestionsQuery)
        {
            var response = await _mediator.Send(getReleatedQuestionsQuery);
            return Ok(response);
        }
        [HttpGet("by-short-url/{ShortUrl}")]
        public async Task<IActionResult> GetByShortUrl([FromRoute] GetQuestionByShortUrlQuery getQuestionByShortUrlQuery)
        {
            var response = await _mediator.Send(getQuestionByShortUrlQuery);
            return Ok(response);
        }
        [HttpGet("test")]
        public async Task<IActionResult> Test()
        {
            var smtpSection = _configuration.GetSection("Smtp");
            var smtpHost = smtpSection["Host"];
            var smtpPort = int.Parse(smtpSection["Port"]);
            var smtpUser = smtpSection["User"];
            var smtpPass = smtpSection["Pass"];
            var subject = "Təbriklər! Töhfəniz qəbul olundu";
            var sb = new StringBuilder();
            sb.Append("<div style='font-family:sans-serif;max-width:500px;margin:auto;border:1px solid #e0e0e0;padding:24px;border-radius:8px;background:#fafcff;'>");
            sb.Append("<h2 style='color:#2e7d32;'>🎉 Təbriklər!</h2>");
            sb.Append("<p>Hörmətli <b>" + "Murad Dadashov" + "</b>,</p>");
            sb.Append("<p>Sizin <b>\"" + "Dictionary nedir ?"+ "\"</b> başlıqlı töhfəniz <span style='color:#388e3c;font-weight:bold;'>qəbul olundu</span>!</p>");
            sb.Append("<p>İcmanın inkişafına verdiyiniz töhfəyə görə təşəkkür edirik. Uğurlarınızın davamını arzulayırıq!</p>");
            sb.Append("<hr style='border:none;border-top:1px solid #e0e0e0;margin:24px 0;'>");
            sb.Append("<p style='font-size:13px;color:#888;'>Bu avtomatik göndərilmiş mesajdır. Zəhmət olmasa cavab verməyin.</p>");
            sb.Append("</div>");
            var body = sb.ToString();
            await MailHelper.SendEmailAsync(smtpHost, smtpPort, smtpUser, smtpPass, "murad.code.3355@gmail.com", subject, body);



            return Ok();
        }

        [HttpGet("test-simple")]
        public IActionResult TestSimple()
        {
            var testResponse = new
            {
                Message = "Questions Controller is working!",
                Timestamp = DateTime.UtcNow,
                Controller = "QuestionsController",
                Status = "Active",
                Environment = _configuration["ASPNETCORE_ENVIRONMENT"] ?? "Unknown",
                DatabaseConnection = _configuration.GetConnectionString("PostgreSQL") != null ? "Configured" : "Not Configured"
            };

            return Ok(testResponse);
        }
    }
}
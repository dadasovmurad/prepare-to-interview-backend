using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Application.DTOs.Answer
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int QuestionId { get; set; }
    }
} 
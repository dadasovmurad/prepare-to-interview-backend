using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Application.DTOs.Question
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Difficulty { get; set; }
        public int CategoryId { get; set; }
    }
} 
using PrepareToInterview.Domain.Enums;

namespace PrepareToInterview.Application.DTOs.Question
{
    public class QuestionUpdateDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public int CategoryId { get; set; }
        public Difficulty Difficulty { get; set; }
        public int[] TagIds { get; set; } = Array.Empty<int>();
    }
} 
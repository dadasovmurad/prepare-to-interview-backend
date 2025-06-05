namespace PrepareToInterview.Application.DTOs.Question
{
    public class QuestionCreateDto
    {
        public string Content { get; set; } = null!;
        public int CategoryId { get; set; }
        public int[] TagIds { get; set; } = Array.Empty<int>();
    }
} 
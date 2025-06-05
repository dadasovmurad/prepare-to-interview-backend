namespace PrepareToInterview.Application.DTOs.Answer
{
    public class AnswerGetByIdDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public int QuestionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
} 
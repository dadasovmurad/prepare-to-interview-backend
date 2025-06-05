namespace PrepareToInterview.Application.DTOs.Comment
{
    public class CommentCreateDto
    {
        public string Content { get; set; } = null!;
        public int QuestionId { get; set; }
    }
} 
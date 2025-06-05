namespace PrepareToInterview.Application.DTOs.Comment
{
    public class CommentListDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public int QuestionId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

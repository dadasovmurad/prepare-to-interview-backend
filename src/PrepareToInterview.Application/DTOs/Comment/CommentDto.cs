using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Application.DTOs.Comment
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int AnswerId { get; set; }
    }
} 
using PrepareToInterview.Domain.Enums;

namespace PrepareToInterview.Application.DTOs.Question
{
    public class QuestionListDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public Difficulty Difficulty { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<string> Tags { get; set; } = new List<string>();
    }
}
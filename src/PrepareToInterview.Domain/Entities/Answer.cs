using PrepareToInterview.Domain.Entities.Common;

namespace PrepareToInterview.Domain.Entities
{
    public class Answer : BaseEntity
    {
        public string Content { get; set; } // Answer text
        // Foreign Key
        public int QuestionId { get; set; }
        public Question Question { get; set; } // Navigation property
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
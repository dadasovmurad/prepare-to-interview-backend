
using PrepareToInterview.Domain.Entities.Common;

namespace PrepareToInterview.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public Guid Id { get; set; }
        public string Content { get; set; } // Comment text
        // Foreign Key
        public Guid QuestionId { get; set; }
        public Question Question { get; set; } // Navigation property
    }
}

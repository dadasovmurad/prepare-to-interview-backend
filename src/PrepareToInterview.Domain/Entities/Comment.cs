
using PrepareToInterview.Domain.Entities.Common;

namespace PrepareToInterview.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; } // Comment text
        // Foreign Key
        public int QuestionId { get; set; }
        public Question Question { get; set; } // Navigation property
    }
}

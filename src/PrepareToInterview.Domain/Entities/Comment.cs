using PrepareToInterview.Domain.Entities.Common;

namespace PrepareToInterview.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; } // Comment text
        // Foreign Key
        public int AnswerId { get; set; }
        public Answer Answer { get; set; } // Navigation property
    }
}

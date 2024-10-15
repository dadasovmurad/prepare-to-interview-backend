
using PrepareToInterview.Domain.Entities.Common;

namespace PrepareToInterview.Domain.Entities
{
    public class Answer : BaseEntities
    {
        public string Content { get; set; } // Answer text
        // Foreign Key
        public Guid QuestionId { get; set; }
        public Question Question { get; set; } // Navigation property
    }
}

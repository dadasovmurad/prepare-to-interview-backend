using PrepareToInterview.Domain.Entities.Common;
using PrepareToInterview.Domain.Enums;

namespace PrepareToInterview.Domain.Entities
{
    public class Question : BaseEntity
    {
        public string Content { get; set; }
        public Difficulty Difficulty { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public Category Category { get; set; }
        public ICollection<Answer> Answers { get; set; } // The answer to this question
        public ICollection<Comment> Comments { get; set; } // Related comments
        public ICollection<QuestionTag> QuestionTags { get; set; } // Related comments
        public AppUser User { get; set; }

    }
}
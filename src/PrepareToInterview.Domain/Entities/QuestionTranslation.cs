using PrepareToInterview.Domain.Entities.Common;

namespace PrepareToInterview.Domain.Entities
{
    public class QuestionTranslation : BaseEntity
    {
        public int QuestionId { get; set; }
        public string LanguageCode { get; set; }
        public string Content { get; set; }

        public Question Question { get; set; }
    }
}
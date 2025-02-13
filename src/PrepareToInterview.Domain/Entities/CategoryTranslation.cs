using PrepareToInterview.Domain.Entities.Common;

namespace PrepareToInterview.Domain.Entities
{
    public class CategoryTranslation : BaseEntity
    {
        public int CategoryId { get; set; }
        public string LanguageCode { get; set; }
        public string Content { get; set; }

        public Category Category { get; set; }
    }
}

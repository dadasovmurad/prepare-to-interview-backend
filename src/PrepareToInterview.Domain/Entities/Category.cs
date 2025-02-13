using PrepareToInterview.Domain.Entities.Common;

namespace PrepareToInterview.Domain.Entities
{
    public class Category : BaseEntity
    {
        public int? ParentId { get; set; }
        public Category Parent { get; set; }
        public string? IconUrl { get; set; }
        public ICollection<Category> Children { get; set; }
        public ICollection<CategoryTranslation> CategoryTranslations { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}

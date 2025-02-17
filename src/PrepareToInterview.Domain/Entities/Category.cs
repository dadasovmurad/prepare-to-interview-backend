using PrepareToInterview.Domain.Entities.Common;

namespace PrepareToInterview.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public Category Parent { get; set; }
        public string? Iconurl { get; set; }
        public string? IconName { get; set; }
        public ICollection<Category> Children { get; set; }
        //public ICollection<CategoryTranslation> CategoryTranslations { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}
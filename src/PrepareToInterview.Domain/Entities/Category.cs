using PrepareToInterview.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Domain.Entities
{
    public class Category : BaseEntity
    {
        public int? ParentId { get; set; }
        public Category Parent { get; set; }

        public ICollection<Category> Children { get; set; }
        public ICollection<CategoryTranslation> CategoryTranslations { get; set; }
        public ICollection<Question> Questions { get; set; }
    }
}

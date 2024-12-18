using PrepareToInterview.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

using PrepareToInterview.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<QuestionTag> QuestionTags { get; set; }
    }
}
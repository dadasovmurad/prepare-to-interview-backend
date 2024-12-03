using PrepareToInterview.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PrepareToInterview.Domain.Entities
{
    public class Question : BaseEntity
    {
        public string? SuitableFor { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // Foreign Key
        //public string UserId { get; set; }
        public ICollection<Answer> Answers { get; set; } // The answer to this question
        public ICollection<QuestionTranslation> QuestionTranslations { get; set; } // The answer to this question
        public ICollection<Comment> Comments { get; set; } // Related comments
        public ICollection<QuestionTag> QuestionTags { get; set; } // Related comments
    }
}
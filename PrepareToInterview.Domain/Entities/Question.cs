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
        public string Content { get; set; } // Question text
        public string Category { get; set; } // Question category (Technical, HR, etc.)
        public string SuitableFor { get; set; }

        // Foreign Key
        //public string UserId { get; set; }
        public ICollection<Answer> Answer { get; set; } // The answer to this question
        public ICollection<Comment> Comments { get; set; } // Related comments
    }
}
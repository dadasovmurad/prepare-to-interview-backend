using PrepareToInterview.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
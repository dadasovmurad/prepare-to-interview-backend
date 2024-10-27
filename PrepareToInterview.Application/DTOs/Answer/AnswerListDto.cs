using PrepareToInterview.Domain.DTOs.Answer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Domain.DTOs.Answer
{
    public class AnswerListDto 
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
    }
}
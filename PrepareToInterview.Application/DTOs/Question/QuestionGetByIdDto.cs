using PrepareToInterview.Application.DTOs.Answer;
using PrepareToInterview.Application.DTOs.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.DTOs.Question
{
    public class QuestionGetByIdDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string SuitableFor { get; set; }
        public object Answers { get; set; }
        public object Comments { get; set; }
    }
}

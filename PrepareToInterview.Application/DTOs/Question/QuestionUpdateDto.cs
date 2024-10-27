using PrepareToInterview.Application.DTOs.Answer;
using PrepareToInterview.Application.DTOs.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.DTOs.Question
{
    public class QuestionUpdateDto
    {
        public string Id { get; set; }  
        public string Content { get; set; }
        public string Category { get; set; }
        public string SuitableFor { get;set; }
        public List<AnswerUpdateDto> Answers {get; set; }
        public List<CommentUpdateDto> Comments { get; set; }    
    }
}

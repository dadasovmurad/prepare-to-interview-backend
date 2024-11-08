using PrepareToInterview.Domain.DTOs.Answer;
using PrepareToInterview.Domain.DTOs.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Domain.DTOs.Question
{
    public record QuestionListDto(Guid Id,string Content,string Category, object Answers, object Comments);
}
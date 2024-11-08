using MediatR;
using PrepareToInterview.Application.DTOs.Answer;
using PrepareToInterview.Application.DTOs.Comment;
using PrepareToInterview.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Features.Commands.Question.UpdateQuestion
{
    public class UpdateQuestionCommandRequest : IRequest<IDataResult<UpdateQuestionCommandResponse>>
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string SuitableFor { get; set; }
        public List<AnswerUpdateDto> Answers { get; set; }
        public List<CommentUpdateDto> Comments { get; set; }
    }
}

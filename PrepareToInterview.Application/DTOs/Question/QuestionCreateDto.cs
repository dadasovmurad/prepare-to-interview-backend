using PrepareToInterview.Application.DTOs.Answer;
using PrepareToInterview.Application.DTOs.Comment;

namespace PrepareToInterview.Application.DTOs.Question
{
    public class QuestionCreateDto
    {
        public string Content { get; set; }
        public string Category { get; set; }
        public string SuitableFor { get; set; }
        public List<AnswerUpdateDto> Answers { get; set; }
        public List<CommentUpdateDto> Comments { get; set; }
    }
}

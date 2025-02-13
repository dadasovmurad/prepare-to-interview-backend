using PrepareToInterview.Application.DTOs.Answer;
using PrepareToInterview.Application.DTOs.Category;
using PrepareToInterview.Application.DTOs.Comment;
using PrepareToInterview.Application.DTOs.Tag;

namespace PrepareToInterview.Application.DTOs
{
    public class QuestionGetByIdDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public CategoryDto Category { get; set; }
        public string Difficulty { get; set; }
        public IList<AnswerListDto> Answers { get; set; }
        public IList<CommentListDto> Comments { get; set; }
        public IList<TagListDto> Tags { get; set; }
    }
}

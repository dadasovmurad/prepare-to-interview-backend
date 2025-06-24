using PrepareToInterview.Application.DTOs.Answer;
using PrepareToInterview.Application.DTOs.Category;
using PrepareToInterview.Application.DTOs.Comment;
using PrepareToInterview.Application.DTOs.Tag;
using PrepareToInterview.Application.DTOs.User;
using PrepareToInterview.Domain.Enums;

namespace PrepareToInterview.Application.DTOs
{
    public class QuestionGetByIdDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Difficulty { get; set; }
        public IList<TagListDto> Tags { get; set; }
        public UserDetailsDto UserDetails { get; set; }
    }
}

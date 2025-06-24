using PrepareToInterview.Application.DTOs.User;

namespace PrepareToInterview.Application.DTOs
{
    public class QuestionListDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Difficulty { get; set; }
        public UserDetailsDto UserDetails { get; set; }
    }
}
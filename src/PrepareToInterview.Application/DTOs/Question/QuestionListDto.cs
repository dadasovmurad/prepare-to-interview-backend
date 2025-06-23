namespace PrepareToInterview.Application.DTOs
{
    public class QuestionListDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Difficulty { get; set; }
        public string ShortUrl { get; set; }
        public string UserDetailsDto { get; set; }
    }
}
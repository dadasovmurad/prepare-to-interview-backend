namespace PrepareToInterview.Domain.Entities
{
    public class QuestionTag
    {
        public Guid QuestionID { get; set; }
        public Question Question { get; set; }

        public Guid TagID { get; set; }
        public Tag Tag { get; set; }
    }
}
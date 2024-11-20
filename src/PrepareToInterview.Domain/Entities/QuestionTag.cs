namespace PrepareToInterview.Domain.Entities
{
    public class QuestionTag
    {
        public int QuestionID { get; set; }
        public Question Question { get; set; }

        public int TagID { get; set; }
        public Tag Tag { get; set; }
    }
}
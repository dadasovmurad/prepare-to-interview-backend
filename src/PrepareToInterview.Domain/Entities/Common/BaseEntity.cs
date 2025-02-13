namespace PrepareToInterview.Domain.Entities.Common
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate => DateTime.Now;
    }
}
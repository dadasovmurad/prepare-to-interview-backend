using PrepareToInterview.Domain.Entities.Common;

namespace PrepareToInterview.Domain.Entities
{
    public class User : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string? PersonalUrl { get; set; }
        public string? ImageUrl { get; set; }
        public byte[] PassKeyHash { get; set; }
        public ICollection<Contribution> Contributions { get; set; }
    }
}
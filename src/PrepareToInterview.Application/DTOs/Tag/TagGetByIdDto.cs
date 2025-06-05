namespace PrepareToInterview.Application.DTOs.Tag
{
    public class TagGetByIdDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
} 
namespace PrepareToInterview.Application.DTOs.Category
{
    public class CategoryListDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? ParentId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 
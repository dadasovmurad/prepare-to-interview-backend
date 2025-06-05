namespace PrepareToInterview.Application.DTOs.Category
{
    public class CategoryCreateDto
    {
        public string Name { get; set; } = null!;
        public int? ParentId { get; set; }
    }
} 
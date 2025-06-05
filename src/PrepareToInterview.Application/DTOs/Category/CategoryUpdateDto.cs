namespace PrepareToInterview.Application.DTOs.Category
{
    public class CategoryUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? ParentId { get; set; }
    }
} 
namespace PrepareToInterview.Application.DTOs.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public string? IconUrl { get; set; }
        public string? IconName { get; set; }
        public List<CategoryDto> Children { get; set; }
    }
}
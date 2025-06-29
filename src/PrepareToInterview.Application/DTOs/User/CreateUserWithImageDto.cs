using Microsoft.AspNetCore.Http;

namespace PrepareToInterview.Application.DTOs.User
{
    public class CreateUserWithImageDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string? PersonalUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
} 
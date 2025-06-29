using FluentValidation;
using Microsoft.AspNetCore.Http;
using PrepareToInterview.Application.Features.Commands.Users.CreateUser;

namespace PrepareToInterview.Application.Validators.User
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .MaximumLength(100).WithMessage("Full name cannot exceed 100 characters.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters.")
                .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.")
                .Matches("^[a-zA-Z0-9_]+$").WithMessage("Username can only contain letters, numbers, and underscores.");

            RuleFor(x => x.PersonalUrl)
                .MaximumLength(200).WithMessage("Personal URL cannot exceed 200 characters.")
                .Must(BeValidUrl).When(x => !string.IsNullOrEmpty(x.PersonalUrl))
                .WithMessage("Invalid URL format.");

            RuleFor(x => x.ImageFile)
                .Must(BeValidImageFile).When(x => x.ImageFile != null)
                .WithMessage("Invalid image file. Only JPG, JPEG, PNG, and GIF files up to 5MB are allowed.");
        }

        private bool BeValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }

        private bool BeValidImageFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return false;

            // Check file size (5MB max)
            if (file.Length > 5 * 1024 * 1024)
                return false;

            // Check file extension
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            
            return allowedExtensions.Contains(fileExtension);
        }
    }
} 
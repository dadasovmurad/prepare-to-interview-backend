using FluentValidation;
using PrepareToInterview.Domain.Entities;

namespace PrepareToInterview.Application.Validators
{
    public class CreateQuestionValidator : AbstractValidator<Question>
    {
        public CreateQuestionValidator()
        {

        }
    }
}
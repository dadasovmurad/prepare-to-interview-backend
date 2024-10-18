using FluentValidation;
using PrepareToInterview.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrepareToInterview.Application.Validators
{
    public class CreateQuestionValidator : AbstractValidator<Question>
    {
        public CreateQuestionValidator()
        {
            
        }
    }
}
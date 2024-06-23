using FluentValidation;
using GoatEdu.Core.DTOs.QuizDto;
using GoatEdu.Core.Interfaces;

namespace GoatEdu.Core.Validator;

public class QuizDtoValidator : AbstractValidator<QuizDto>
{
    public QuizDtoValidator()
    {
        RuleFor(x => x.Quiz1)
            .NotEmpty().WithMessage("Quiz name is required!")
            .MaximumLength(100).WithMessage("Quiz name not exceed 100 characters!")
            .Unless(x => x.Quiz1 is null);
    }
}
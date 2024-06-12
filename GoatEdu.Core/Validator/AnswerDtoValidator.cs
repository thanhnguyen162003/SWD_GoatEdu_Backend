using FluentValidation;
using GoatEdu.Core.DTOs;

namespace GoatEdu.Core.Validator;

public class AnswerDtoValidator : AbstractValidator<AnswerDto>
{
    public AnswerDtoValidator()
    {
        RuleFor(x => x.AnswerBody)
            .NotEmpty().WithMessage("Answer body is required!")
            .Unless(x => x.AnswerBody is null);
        
        RuleFor(x => x.AnswerBodyHtml)
            .NotEmpty().WithMessage("Answer body html is required!")
            .Unless(x => x.AnswerBody is null);
    }
}
using FluentValidation;
using GoatEdu.Core.DTOs.TheoryFlashcardDto;

namespace GoatEdu.Core.Validator;

public class TheoryFlashcardContentDtoValidator : AbstractValidator<TheoryFlashcardContentsDto>
{
    public TheoryFlashcardContentDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Question is required!")
            .Unless(x => x.Id is null);
        
        RuleFor(x => x.Question)
            .NotEmpty().WithMessage("Question is required!")
            .Unless(x => x.Question is null);
        
        RuleFor(x => x.Answer)
            .NotEmpty().WithMessage("Question is required!")
            .Unless(x => x.Answer is null);
    }
}
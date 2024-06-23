using FluentValidation;
using GoatEdu.Core.DTOs.QuestionInQuizDto;

namespace GoatEdu.Core.Validator;

public class QuestionQuizDtoValidator : AbstractValidator<QuestionInQuizDto>
{
    public QuestionQuizDtoValidator()
    {
        RuleFor(x => x.QuizQuestion)
            .NotEmpty().WithMessage("Question is required!")
            .Unless(x => x.QuizQuestion is null);
        
        RuleFor(x => x.QuizAnswer1)
            .NotEmpty().WithMessage("Answer 1 is required!")
            .Unless(x => x.QuizAnswer1 is null);
        
        RuleFor(x => x.QuizAnswer2)
            .NotEmpty().WithMessage("Answer 2 is required!")
            .Unless(x => x.QuizAnswer2 is null);
        
        RuleFor(x => x.QuizAnswer3)
            .NotEmpty().WithMessage("Answer 3 is required!")
            .Unless(x => x.QuizAnswer3 is null);
        
        RuleFor(x => x.QuizCorrect)
            .NotEmpty().WithMessage("Answer correct is required!")
            .Unless(x => x.QuizCorrect is null);

    }
}
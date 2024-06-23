using FluentValidation;
using GoatEdu.Core.QueriesFilter;

namespace GoatEdu.Core.Validator;

public class QuizQueryFilterValidator : AbstractValidator<QuizQueryFilter>
{
    public QuizQueryFilterValidator()
    {
        RuleFor(x => x.id)
            .NotNull().WithMessage("Id is required!")
            .Unless(x => x.id is null && x.type is null);
        
        RuleFor(x => x.type)
            .NotNull().WithMessage("Type is required!")
            .Unless(x => x.id is null && x.type is null);

    }
}
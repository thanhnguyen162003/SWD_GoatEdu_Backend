using FluentValidation;
using GoatEdu.Core.DTOs.TagDto;

namespace GoatEdu.Core.Validator;

public class TagRequestDtoValidator : AbstractValidator<TagRequestDto>
{
    public TagRequestDtoValidator()
    {
        RuleFor(x => x.TagName)
            .NotEmpty().WithMessage("Tag name is required!")
            .MaximumLength(100).WithMessage("Tag name cannot exceed 100 characters.");
    }
}
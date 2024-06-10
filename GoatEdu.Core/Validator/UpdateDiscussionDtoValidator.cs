using FluentValidation;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.Interfaces.SubjectInterfaces;

namespace GoatEdu.Core.Validator;

public class UpdateDiscussionDtoValidator : AbstractValidator<DiscussionDto>
{
    public UpdateDiscussionDtoValidator()
    {
        RuleFor(x => x.DiscussionName)
            .NotEmpty().WithMessage("Discussion name is required!")
            .MaximumLength(100).WithMessage("Discussion name cannot exceed 100 characters.")
            .Unless(x => string.IsNullOrEmpty(x.DiscussionName));

        RuleFor(x => x.Tags)
            .NotEmpty().WithMessage("Tags is required!")
            .Must(list => list.Count == 4).WithMessage("Tags must have only 4 tags!")
            .Must(list => list.Count == list.Distinct().Count()).WithMessage("Tags must not duplicate!")
            .Unless(x => x.Tags.Count > 0);
    }
}
using FluentValidation;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.TagDto;
using GoatEdu.Core.Interfaces.SubjectInterfaces;

namespace GoatEdu.Core.Validator;

public class DiscussionDtoValidator : AbstractValidator<DiscussionDto>
{
    public DiscussionDtoValidator(ISubjectRepository subjectRepository)
    {
        RuleFor(x => x.DiscussionName)
            .NotEmpty().WithMessage("Discussion name is required!")
            .MaximumLength(100).WithMessage("Discussion name cannot exceed 100 characters.")
            .Unless(x => string.IsNullOrEmpty(x.DiscussionName));

        RuleFor(x => x.SubjectId)
            .NotEmpty().WithMessage("Subject Id is required!")
            .MustAsync(async (id, cancellation) => await subjectRepository.SubjectIdExistAsync(id))
            .WithMessage("Subject Id must exist!")
            .Unless(x => x.SubjectId == null);

        RuleFor(x => x.Tags)
            .NotEmpty().WithMessage("Tags is required!")
            .Must(list => list.Count == 4).WithMessage("Tags must have only 4 tags!")
            .Must(list => list.Count == list.Distinct().Count()).WithMessage("Tags must not duplicate!")
            .Unless(x => x.Tags is null);
    }
}
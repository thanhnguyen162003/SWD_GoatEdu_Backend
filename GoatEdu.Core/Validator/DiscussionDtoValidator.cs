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
            .Unless(x => x.DiscussionName is null);

        RuleFor(x => x.SubjectId)
            .NotEmpty().WithMessage("Subject Id is required!")
            .MustAsync(async (id, cancellation) => await subjectRepository.SubjectIdExistAsync(id))
            .WithMessage("Subject Id must exist!")
            .Unless(x => x.SubjectId == null);

        RuleFor(x => x.Tags)
            .Must(list => list.Count == 4).WithMessage("Tags must have only 4 tags!")
            .Must(list => list.Count == list.Distinct().Count()).WithMessage("Tags must not duplicate!")
            .Unless(x => x.Tags.Count == 0);
        
        RuleFor(x => x.DiscussionBody)
            .NotEmpty().WithMessage("Discussion body is required!")
            .Unless(x => x.DiscussionBody is null && x.DiscussionBodyHtml is null);
        
        RuleFor(x => x.DiscussionBodyHtml)
            .NotEmpty().WithMessage("Discussion body html is required!")
            .Unless(x => x.DiscussionBody is null && x.DiscussionBodyHtml is null);
    }
}
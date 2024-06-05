using FluentValidation;
using GoatEdu.API.Request;
using GoatEdu.Core.DTOs;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoatEdu.Core.Validator;

public class DisscussionRequestDtoValidator : AbstractValidator<DiscussionRequestDto>
{
    
    public DisscussionRequestDtoValidator(GoatEduContext context)
    {
        RuleFor(x => x.DiscussionName)
            .NotEmpty().WithMessage("Discussion name is required!")
            .MaximumLength(100).WithMessage("Discussion name cannot exceed 100 characters.");

        RuleFor(x => x.SubjectId)
            .NotEmpty().WithMessage("Subject Id is required!")
            .MustAsync(async (id, cancellation) => await context.Subjects.AnyAsync(x => x.Id == id))
            .WithMessage("Subject Id must exist!");

        RuleFor(x => x.Tags)
            .NotEmpty().WithMessage("Tags is required!")
            .Must(list => list.Count == 4).WithMessage("Tags must have only 4 tags!")
            .Must(list => list.Count == list.Distinct().Count()).WithMessage("Tags must not duplicate!");
    }
}
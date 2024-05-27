using FluentValidation;
using GoatEdu.Core.DTOs;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoatEdu.Core.Validator;

public class DisscussionRequestDtoValidator : AbstractValidator<DiscussionRequestDto>
{
    
    public DisscussionRequestDtoValidator(GoatEduContext context)
    {
        RuleFor(x => x.DiscussionName)
            .NotEmpty().WithMessage("Tag name is required!")
            .MaximumLength(100).WithMessage("Tag name cannot exceed 100 characters.");

        RuleFor(x => x.SubjectId)
            .NotEmpty().WithMessage("Subject Id is required!")
            .MustAsync(async (id, cancellation) => await context.Subjects.AnyAsync(x => x.Id == id))
            .WithMessage("Subject Id must exist!");
    }
}
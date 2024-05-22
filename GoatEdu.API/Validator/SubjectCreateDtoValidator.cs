using FluentValidation;
using GoatEdu.Core.DTOs.SubjectDto;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoatEdu.Core.Validator;

public class SubjectCreateDtoValidator : AbstractValidator<SubjectDto>
{
    public SubjectCreateDtoValidator(GoatEduContext context) 
    {
        RuleFor(dto => dto.SubjectName)
            .NotEmpty().WithMessage("Subject name is required.")
            .MaximumLength(100).WithMessage("Subject name cannot exceed 100 characters.")
            .MustAsync(async (name, cancellation) => !await context.Subjects.AnyAsync(s => 
                s.SubjectName.ToLower() == name.ToLower()
            )).WithMessage("A subject with this name already exists.");

        RuleFor(dto => dto.SubjectCode)
            .NotEmpty().WithMessage("Subject code is required.")
            .MaximumLength(20).WithMessage("Subject code cannot exceed 20 characters.")
            .MustAsync(async (code, cancellation) => !await context.Subjects.AnyAsync(s => 
                s.SubjectCode.ToLower() == code.ToLower()
            )).WithMessage("A subject with this code already exists.");

         RuleFor(dto => dto.Information)
                .NotEmpty().WithMessage("Information is required.")
                .MaximumLength(500).WithMessage("Information cannot exceed 500 characters.");

           
        
    }
}
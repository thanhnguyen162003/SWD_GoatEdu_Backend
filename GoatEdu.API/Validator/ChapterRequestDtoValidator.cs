using FluentValidation;
using GoatEdu.Core.DTOs.ChapterDto;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoatEdu.Core.Validator;

public class ChapterRequestDtoValidator: AbstractValidator<ChapterDto>
{
    public ChapterRequestDtoValidator(GoatEduContext context) 
    {
        RuleFor(dto => dto.ChapterName)
            .NotEmpty().WithMessage("Chapter name is required.")
            .MaximumLength(100).WithMessage("Subject name cannot exceed 100 characters.")
            .MustAsync(async (name, cancellation) => !await context.Chapters.AnyAsync(s => 
                s.ChapterName.ToLower() == name.ToLower()
            )).WithMessage("A subject with this name already exists.");

        RuleFor(dto => dto.ChapterLevel)
            .NotEmpty().WithMessage("Chapter code is required.")
            .WithMessage("Subject code cannot exceed 20 characters.")
            .MustAsync(async (code, cancellation) => !await context.Chapters.AnyAsync(s => 
                s.ChapterLevel == code
            )).WithMessage("A subject with this code already exists.");
        
    }
}
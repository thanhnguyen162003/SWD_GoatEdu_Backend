using FluentValidation;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.ChapterDto;
using GoatEdu.Core.Interfaces.ChapterInterfaces;

namespace GoatEdu.Core.Validator;

public class ChapterDtoValidator : AbstractValidator<ChapterDto>
{
    public ChapterDtoValidator(IChapterRepository chapterRepository)
    {
        var chapterRepository1 = chapterRepository;
        
        RuleFor(dto => dto.ChapterName)
            .NotEmpty().WithMessage("Chapter name is required.")
            .MaximumLength(100).WithMessage("Chapter name cannot exceed 100 characters.")
            .MustAsync(async (name, cancellation) => !await chapterRepository1.ChapterNameExistsAsync(name))
            .WithMessage("A subject with this name already exists.");

        RuleFor(dto => dto.ChapterLevel)
            .GreaterThan(0).WithMessage("Chapter code must be greater than 0.")
            .MustAsync(async (code, cancellation) => !await chapterRepository1.ChapterLevelExistsAsync(code))
            .WithMessage("A subject with this code already exists.");
    }
}
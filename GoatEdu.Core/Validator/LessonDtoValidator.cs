using FluentValidation;
using GoatEdu.Core.DTOs.FlashcardDto;
using GoatEdu.Core.Interfaces.ChapterInterfaces;

namespace GoatEdu.Core.Validator;

public class LessonDtoValidator : AbstractValidator<LessonDto>
{
    public LessonDtoValidator(IChapterRepository chapterRepository)
    {
        RuleFor(x => x.LessonName)
            .NotEmpty().WithMessage("Lesson name is required!")
            .Unless(x => x.LessonName is null);

        RuleFor(x => x.ChapterId)
            .NotEmpty().WithMessage("Chapter Id is required!")
            .MustAsync(async (id, cancellation) => await chapterRepository.ChapterIdExistsAsync(id))
            .WithMessage("Chapter Id must exist!")
            .Unless(x => x.ChapterId is null);
        
        RuleFor(x => x.LessonBody)
            .NotEmpty().WithMessage("Lesson body is required!")
            .Unless(x => x.LessonBody is null);
        
        RuleFor(x => x.LessonMaterial)
            .NotEmpty().WithMessage("Lesson material is required!")
            .Unless(x => x.LessonMaterial is null);

        RuleFor(x => x.DisplayOrder)
            .NotEmpty().WithMessage("Dispaly order is required!")
            .Unless(x => x.DisplayOrder is null);
    }
}
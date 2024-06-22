using FluentValidation;
using GoatEdu.Core.DTOs.TheoryDto;
using GoatEdu.Core.Enumerations;
using GoatEdu.Core.Interfaces.LessonInterfaces;
using GoatEdu.Core.Validator.CustomAnnotation;

namespace GoatEdu.Core.Validator;

public class TheoryDtoValidator : AbstractValidator<TheoryDto>
{
    public TheoryDtoValidator(ILessonRepository lessonRepository)
    {
        var allowedContentTypes = new[] { ContentType.DOC, ContentType.DOCX, ContentType.PDF };

        RuleFor(x => x.TheoryName)
            .NotEmpty().WithMessage("Theory name is required!")
            .MaximumLength(100).WithMessage("Theory name cannot exceed 100 character!")
            .Unless(x => x.TheoryName is null);
        
        RuleFor(x => x.TheoryContent)
            .NotEmpty().WithMessage("Theory content is required!")
            .Unless(x => x.TheoryContent is null);
        
        RuleFor(x => x.LessonId)
            .NotEmpty().WithMessage("Lesson id is required!")
            .MustAsync(async (id, cancellationtoken) => await lessonRepository.LessonIdExistAsync(id))
            .WithMessage("Lesson id must exist!")
            .Unless(x => x.LessonId is null);
        
        RuleFor(x => x.FormFile)
            .Must(data => data.Length > 0).WithMessage("File is empty.")
            .PermittedExtensions(allowedContentTypes)
            .Unless(x => x.FormFile is null);
    }
}
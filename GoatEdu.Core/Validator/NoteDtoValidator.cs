using FluentValidation;
using GoatEdu.Core.DTOs.NoteDto;

namespace GoatEdu.Core.Validator;

public class NoteDtoValidator : AbstractValidator<NoteDto>
{
    public NoteDtoValidator()
    {
        RuleFor(dto => dto.NoteName)
        .NotEmpty().WithMessage("Note name is required!")
        .MaximumLength(100).WithMessage("Note name cannot exceed 100 characters.")
        .Unless(dto => dto.NoteName is null);
        
        RuleFor(dto => dto.NoteBody)
            .NotEmpty().WithMessage("Note body is required!")
            .Unless(dto => dto.NoteBody is null && dto.NoteBodyHtml is null );
        
        RuleFor(dto => dto.NoteBodyHtml)
            .NotEmpty().WithMessage("Note body html is required!")
            .Unless(dto => dto.NoteBody is null && dto.NoteBodyHtml is null);
    }
}
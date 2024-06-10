using FluentValidation;
using GoatEdu.Core.DTOs.NoteDto;

namespace GoatEdu.Core.Validator;

public class NoteDtoValidator : AbstractValidator<NoteDto>
{
    public NoteDtoValidator()
    {
        RuleFor(dto => dto.NoteName)
        .NotEmpty().WithMessage("Notification name is required!")
        .MaximumLength(100).WithMessage("Notifitcation name cannot exceed 100 characters.")
        .Unless(dto => string.IsNullOrWhiteSpace(dto.NoteName));
    }
}
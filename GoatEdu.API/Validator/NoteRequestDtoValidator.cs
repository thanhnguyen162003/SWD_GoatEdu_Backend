using FluentValidation;
using GoatEdu.Core.DTOs.NoteDto;
using GoatEdu.Core.DTOs.NotificationDto;

namespace GoatEdu.Core.Validator;

public class NoteRequestDtoValidator : AbstractValidator<NoteRequestDto>
{
    public NoteRequestDtoValidator()
    {
        RuleFor(dto => dto.NoteName)
            .NotEmpty().WithMessage("Notification name is required!")
            .MaximumLength(100).WithMessage("Notifitcation name cannot exceed 100 characters.");
    }
}
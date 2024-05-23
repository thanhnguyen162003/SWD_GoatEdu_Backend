using System.Data.Entity;
using FluentValidation;
using GoatEdu.Core.DTOs.NotificationDto;
using Infrastructure.Data;

namespace GoatEdu.Core.Validator;

public class NotificationRequestDtoValidator : AbstractValidator<NotificationRequestDto>
{
    public NotificationRequestDtoValidator()
    {
        RuleFor(dto => dto.NotificationName)
            .NotEmpty().WithMessage("Notification name is required!")
            .MaximumLength(100).WithMessage("Notifitcation name cannot exceed 100 characters.");

        RuleFor(dto => dto.NotificationMessage)
            .NotEmpty().WithMessage("Notification message is required!");
    }
}
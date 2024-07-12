using FluentValidation;
using GoatEdu.Core.DTOs.NotificationDto;
using GoatEdu.Core.Interfaces.UserInterfaces;

namespace GoatEdu.Core.Validator;

public class NotificationDtoValidator : AbstractValidator<NotificationDto>
{
    public NotificationDtoValidator(IUserRepository userRepository)
    {
        RuleFor(dto => dto.NotificationName)
            .NotEmpty().WithMessage("Notification name is required!")
            .MaximumLength(100).WithMessage("Notifitcation name cannot exceed 100 characters.");

        RuleFor(dto => dto.NotificationMessage)
            .NotEmpty().WithMessage("Notification message is required!");

        RuleFor(dto => dto.UserId)
            .MustAsync(async (id, cancellation) => await userRepository.IdExistAsync((Guid)id))
            .WithMessage("User Id not exist!");
    }
}
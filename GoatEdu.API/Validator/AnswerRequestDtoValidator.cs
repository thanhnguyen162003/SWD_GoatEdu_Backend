using FluentValidation;
using GoatEdu.API.Request;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GoatEdu.Core.Validator;

public class AnswerRequestDtoValidator : AbstractValidator<AnswerRequestDto>
{
    public AnswerRequestDtoValidator(GoatEduContext context)
    {
        RuleFor(dto => dto.AnswerBody)
            .NotEmpty().WithMessage("Answer body is required.");

        RuleFor(dto => dto.QuestionId)
            .NotEmpty().WithMessage("Discussion id is required.");


    }
}
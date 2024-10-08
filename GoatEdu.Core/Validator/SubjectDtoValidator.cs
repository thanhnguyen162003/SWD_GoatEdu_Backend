using FluentValidation;
using GoatEdu.Core.DTOs.SubjectDto;
using GoatEdu.Core.Interfaces.SubjectInterfaces;

namespace GoatEdu.Core.Validator;

public class SubjectDtoValidator : AbstractValidator<SubjectDto>
{
    public SubjectDtoValidator(ISubjectRepository subjectRepository)
    {
        RuleFor(dto => dto.SubjectName)
            .NotEmpty().WithMessage("Subject name is required.")
            .MaximumLength(100).WithMessage("Subject name cannot exceed 100 characters.")
            .MustAsync(async (name, cancellation) => !await subjectRepository.SubjectNameExistAsync(name))
            .WithMessage("A subject with this name already exists.")
            .Unless(dto => dto.SubjectName is null);

        RuleFor(dto => dto.SubjectCode)
            .NotEmpty().WithMessage("Subject code is required.")
            .MaximumLength(20).WithMessage("Subject code cannot exceed 20 characters.")
            .MustAsync(async (code, cancellation) => !await subjectRepository.SubjectCodeExistAsync(code))
            .WithMessage("A subject with this code already exists.")
            .Unless(dto => dto.SubjectCode is null);

        RuleFor(dto => dto.Information)
            .NotEmpty().WithMessage("Information is required.")
            .MaximumLength(500).WithMessage("Information cannot exceed 500 characters.")
            .Unless(dto => dto.Information is null);

        RuleFor(dto => dto.Class)
            .NotEmpty().WithMessage("Class is required.")
            .MaximumLength(100).WithMessage("Class cannot exceed 100 characters.")
            .Unless(dto => dto.Class is null);
    }
}
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace GoatEdu.Core.Validator.CustomAnnotation;

public static class CustomValidator 
{
    public static IRuleBuilderOptions<T, IFormFile> PermittedExtensions<T>(this IRuleBuilder<T, IFormFile> ruleBuilder, IEnumerable<string> permittedExtensions)
    {
        return ruleBuilder.Must(file =>
        {
            var contentType = file.ContentType.ToLower();
            return permittedExtensions.Any(extension => extension.Equals(contentType));
        }).WithMessage("Invalid file format.");
    }
}
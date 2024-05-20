using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.DTOs;
using GoatEdu.Core.DTOs.MailDto;

namespace GoatEdu.Core.Interfaces.MailInterfaces;

public interface IMailService
{
    Task<bool> SendUsingTemplateFromFile(string template, string title, UserMail userMail);

    Task<ResponseDto> ConfirmEmailComplete(ConfirmMailDto dto);
}
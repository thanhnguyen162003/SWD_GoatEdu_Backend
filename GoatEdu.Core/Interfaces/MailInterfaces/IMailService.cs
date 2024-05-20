using GoatEdu.Core.CustomEntities;

namespace GoatEdu.Core.Interfaces.MailInterfaces;

public interface IMailService
{
    Task<bool> SendUsingTemplateFromFile(string template, string title, UserMail userMail);
}
using FluentEmail.Core;
using GoatEdu.Core.CustomEntities;
using GoatEdu.Core.Interfaces.MailInterfaces;

namespace GoatEdu.Core.Services;

public class MailService : IMailService
{
    private readonly IFluentEmail _iFluentEmail;

    public MailService(IFluentEmail iFluentEmail)
    {
        _iFluentEmail = iFluentEmail;
    }

    public async Task<bool> SendUsingTemplateFromFile(string template, string title, UserMail userMail)
    {
        var response = await _iFluentEmail.To(userMail.Email)
            .Subject(title)
            .UsingTemplateFromFile(template, userMail, true)
            .SendAsync();

        return response.Successful;

    }

}